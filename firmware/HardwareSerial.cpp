/*
  HardwareSerial.cpp - Improved HardwareSerial library for Arduino
  Copyright (c) 2009 Kiril Zyapkov.  All right reserved.

  This library is free software; you can redistribute it and/or
  modify it under the terms of the GNU Lesser General Public
  License as published by the Free Software Foundation; either
  version 2.1 of the License, or (at your option) any later version.

  This library is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
  Lesser General Public License for more details.

  You should have received a copy of the GNU Lesser General Public
  License along with this library; if not, write to the Free Software
  Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
  
*/
#include <avr/io.h>
#include <avr/interrupt.h>

#include "HardwareSerial.h"

#define USART_RX_BUFFER_MASK (USART_RX_BUFFER_SIZE-1)
#define USART_TX_BUFFER_MASK (USART_TX_BUFFER_SIZE-1)

#if ( USART_RX_BUFFER_SIZE & USART_RX_BUFFER_MASK )
#    error RX buffer size is not a power of 2
#endif
#if ( USART_TX_BUFFER_SIZE & USART_TX_BUFFER_MASK )
#    error TX buffer size is not a power of 2
#endif

#ifndef BAUD_TOL
#   define BAUD_TOL 2
#endif

// both buffers are static global vars in this file. interrupt handlers
// need to access them
static rx_ring_buffer rx_buffer;
static tx_ring_buffer tx_buffer;

/**
 * Initialize the USART module with the BAUD rate predefined
 * in HardwareSerial.h
 *
 * This may implement passing addresses of registers, like the HardwareSerial
 * class from the Arduino library, but that's not necessary at this point.
 *
 */
HardwareSerial::HardwareSerial(rx_ring_buffer *rx_buffer_ptr, tx_ring_buffer *tx_buffer_ptr) {

    _rx_buffer = rx_buffer_ptr;
    _tx_buffer = tx_buffer_ptr;

    // Allocate memory for the rx and tx buffers
    // That is, if the structs contain pointers to the actual buffer
    // arrays. I'm going for defining 2 separate struct's and allocating them
    // statically.
//    _rx_buffer->buffer = (uint8_t *)malloc(USART_RX_BUFFER_SIZE);
//    _tx_buffer->buffer = (uint8_t *)malloc(USART_TX_BUFFER_SIZE);
}

void HardwareSerial::begin(unsigned long baud) {
    // taken from <util/setbaud.h>
    uint8_t use2x = 0;
    uint16_t ubbr =  (F_CPU + 8UL * baud) / (16UL * baud) - 1UL;
    if ( (100 * (F_CPU)) > (16 * (ubbr + 1) * (100 * ubbr + ubbr * BAUD_TOL)) ) {
        use2x = 1;
        ubbr = (F_CPU + 4UL * baud) / (8UL * baud) - 1UL;
    }

    UBRR0L = ubbr & 0xff;
    UBRR0H = ubbr >> 8;
    if (use2x) {
        UCSR0A |= (1 << U2X0);
    } else {
        UCSR0A &= ~(1 << U2X0);
    }

    // Flush buffers
    _tx_buffer->head = _tx_buffer->tail = 0;
    _rx_buffer->head = _rx_buffer->tail = 0;

    UCSR0B |= (1<<TXEN0);  // Enable Transmitter
    UCSR0B |= (1<<RXEN0);  // Enable Reciever
    UCSR0B |= (1<<RXCIE0); // Enable Rx Complete Interrupt
}

void HardwareSerial::write(uint8_t data) {
    // Calculate new head position
    uint8_t tmp_head = (_tx_buffer->head + 1) & USART_TX_BUFFER_MASK;

    // Block until there's room in the buffer
    // XXX: this may block forever if someone externally disabled the transmitter
    //      or the DRE interrupt and there's data in the buffer. Careful!
    while (tmp_head == _tx_buffer->tail);

    // Advance the head, store the data
    _tx_buffer->buffer[tmp_head] = data;
    _tx_buffer->head = tmp_head;

    UCSR0B |= (1<<UDRIE0); // Enable Data Register Empty interrupt
}

void HardwareSerial::flush(void) {
    // Not sure if disabling interrupts is needed here, but let's be on
    // the safe side
    // disable interrupts
    uint8_t oldSREG = SREG;
    cli();
    _rx_buffer->head = _rx_buffer->tail;
    // Re-enable interrupts
    SREG = oldSREG;
}

/**
 * Returns the number of bytes in the RX buffer
 *
 */
uint8_t HardwareSerial::available(void) {
    //    return (_rx_buffer->head + USART_RX_BUFFER_SIZE - _rx_buffer->tail) % USART_RX_BUFFER_SIZE;
    // stupid compiler ...
    uint16_t tmp = (_rx_buffer->head + USART_RX_BUFFER_SIZE - _rx_buffer->tail);
    tmp %= USART_RX_BUFFER_SIZE;
    return (uint8_t)tmp;
}

/**
 * Returns a byte from the RX buffer, or NULL if none are available
 */
uint8_t HardwareSerial::read(void) {
    uint8_t tmp_tail, tmp_data;

    // disable interrupts
    uint8_t oldSREG = SREG;
    cli();
    if (_rx_buffer->head == _rx_buffer->tail) {
        // Better that than block the code waiting for data. Users should call
        // available() first
        tmp_data = -1;
    } else {
        tmp_tail = (_rx_buffer->tail +1) & USART_RX_BUFFER_MASK;
        tmp_data = _rx_buffer->buffer[tmp_tail];
        _rx_buffer->tail = tmp_tail;
    }
    // Re-enable interrupts
    SREG = oldSREG;

    return tmp_data;
}

/**
 * Receive handler
 */
ISR(USART_RX_vect) {
    uint8_t data = UDR0;
    uint8_t tmp_head = (rx_buffer.head + 1) & USART_RX_BUFFER_MASK;
    if (tmp_head == rx_buffer.tail) {
        // buffer overflow! for now, the strategy is to discard a byte off
        // the queue, so that the fresh data can be written. Probably an overflow
        // flag should be raised somewhere ...
        // http://c2.com/cgi/wiki?CircularBuffer
        rx_buffer.tail = (rx_buffer.tail + 1) & USART_RX_BUFFER_MASK;
    }
    rx_buffer.buffer[tmp_head] = data;
    rx_buffer.head = tmp_head;
}

/**
 * Data Register Empty Handler
 */
ISR(USART_UDRE_vect) {
    if (tx_buffer.head == tx_buffer.tail) {
        // Buffer is empty, disable the interrupt
        UCSR0B &= ~(1<<UDRIE0);
    } else {
        tx_buffer.tail = (tx_buffer.tail + 1) & USART_TX_BUFFER_MASK;
        UDR0 = tx_buffer.buffer[tx_buffer.tail];
    }
}

HardwareSerial Serial(&rx_buffer, &tx_buffer);
 
