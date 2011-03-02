/*
  HardwareSerial.h - Improved HardwareSerial library for Arduino
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

#ifndef HardwareSerial_h
#define HardwareSerial_h

#include <inttypes.h>

#include "Print.h"

// Define the amount of tolerance upon which x2 will be enabled, in percent
#define BAUD_TOL 2

// Both buffers must be powers of 2, up to 128 bytes
// Keep in mind the memory limits of the MCU!!
#define USART_RX_BUFFER_SIZE   16
#define USART_TX_BUFFER_SIZE   32

// There were other ways of implementing the ring buffers that I thought of,
// that avoid defining 2 separate struct's for the same thing:
//
//  * allocating memory for the buffers using malloc() at runtime
//      not sure how 'dynamic' memory management will work on the micro
//  * using separate variables, not packing the buffers into structs
//      structs are much neater
//  * defining a single macro for buffer size
//      both buffers would need to have the same size
struct rx_ring_buffer {
    uint8_t buffer[USART_RX_BUFFER_SIZE];
    volatile uint8_t head;
    volatile uint8_t tail;
};

struct tx_ring_buffer {
    uint8_t buffer[USART_TX_BUFFER_SIZE];
    volatile uint8_t head;
    volatile uint8_t tail;
};


class HardwareSerial : public Print
{
  private:
    rx_ring_buffer *_rx_buffer;
    tx_ring_buffer *_tx_buffer;

  public:
    HardwareSerial(rx_ring_buffer *, tx_ring_buffer *);
    void begin(unsigned long);
    void write(uint8_t);
    void flush(void);
    uint8_t available(void);
    uint8_t read(void);

};

extern HardwareSerial Serial;

#endif
