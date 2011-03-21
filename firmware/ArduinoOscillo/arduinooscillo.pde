// defines for setting and clearing register bits
#ifndef cbi
#define cbi(sfr, bit) (_SFR_BYTE(sfr) &= ~_BV(bit))
#endif
#ifndef sbi
#define sbi(sfr, bit) (_SFR_BYTE(sfr) |= _BV(bit))
#endif

//
// pins assignment
//
#define BUILTINLED 13
#define PWM_GENERATOR 11
#define ledPin 13    // LED connected to digital pin 13

//
// globals
//
unsigned char triggerVoltage = 0;
unsigned char lastADC = 0;
unsigned char triggered = 0;
unsigned int DataRemaining = 1500;
unsigned char channel=0;
unsigned char numChannels=1;

//
// Commands
//
#define CMD_IDLE 0
#define CMD_RESET 175
#define CMD_PING '?'
#define CMD_READ_ADC_TRACE 170
#define CMD_READ_BIN_TRACE 171

void StartAnalogRead(uint8_t pin)
{
        // set the analog reference (high two bits of ADMUX) and select the
        // channel (low 4 bits).  this also sets ADLAR (left-adjust result)
        // to 0 (the default).
        ADMUX = (DEFAULT << 6) | (pin & 0x07);
  
#if defined(__AVR_ATmega1280__)
        // the MUX5 bit of ADCSRB selects whether we're reading from channels
        // 0 to 7 (MUX5 low) or 8 to 15 (MUX5 high).
        ADCSRB = (ADCSRB & ~(1 << MUX5)) | (((pin >> 3) & 0x01) << MUX5);
#endif
        // without a delay, we seem to read from the wrong channel
        //delay(1);

        // start the conversion
        sbi(ADCSRA, ADSC);
}

unsigned int EndAnalogRead()
{
        uint8_t low, high;

        // ADSC is cleared when the conversion finishes
        while (bit_is_set(ADCSRA, ADSC));

        // we have to read ADCL first; doing so locks both ADCL
        // and ADCH until ADCH is read.  reading ADCL second would
        // cause the results of each conversion to be discarded,
        // as ADCL and ADCH would be locked when it completed.
        low = ADCL;
        high = ADCH;

        // combine the two bytes
        return (high << 8) | low;
}


void setup() 
{
  // set prescale to 16
  sbi(ADCSRA,ADPS2) ;
  cbi(ADCSRA,ADPS1) ;
  cbi(ADCSRA,ADPS0) ;

  pinMode( PWM_GENERATOR, OUTPUT );
  analogWrite(PWM_GENERATOR, 128);
  
  Serial.begin(115200);
  
  for(int i=2;i<8;i++)
  {
    pinMode(i, INPUT);      // sets the digital pin 7 as input
  }

  
//  Serial.begin(1000000);
//  Serial.begin(153600);
//  Serial.begin(9600);  
}

unsigned char command = 0;

void ProcessSerialCommand( byte in )
{
  if ( in == CMD_PING )
  {
    Serial.print( 79, BYTE ) ;
    Serial.print( 67, BYTE ) ;
    Serial.print( triggerVoltage, BYTE ) ;
    Serial.print( DataRemaining>>8, BYTE ) ;
    Serial.print( DataRemaining&0xff, BYTE ) ;
    for (int i=0;i<2;i++)
    {
      Serial.print( triggerVoltage, BYTE ) ;
    }
  } 
  else if ( in == CMD_RESET ) 
  {
    command = CMD_IDLE;
    Serial.print( "OK" ) ;    
  } 
  else if ( in == CMD_READ_ADC_TRACE )
  {
    while( Serial.available() < 8);

    triggerVoltage = Serial.read();
    DataRemaining = Serial.read()<<8;
    DataRemaining |= Serial.read();
 
    numChannels = Serial.read();
    channel = 0;
  
    analogWrite(PWM_GENERATOR, Serial.read());
  
    for (int i=0;i<4;i++)
    {
      Serial.read();
    }
    
    Serial.write( 85 );
  
    //get a fresher value for lastADC
    StartAnalogRead(0);
    lastADC = (unsigned char)(EndAnalogRead()>>2);    

    triggered = 0;     
    StartAnalogRead(0);    
    
    digitalWrite(ledPin, HIGH);
    command = CMD_READ_ADC_TRACE;
  }
  else if ( in == CMD_READ_BIN_TRACE )
  {
    while( Serial.available() < 4);
    triggerVoltage = Serial.read();
    DataRemaining = Serial.read()<<8;
    DataRemaining |= Serial.read();

    analogWrite(9, 64);
    analogWrite(10, 128);
    analogWrite(11, 192);
    
    triggered = 0;     
    digitalWrite(ledPin, HIGH);
    command = CMD_READ_BIN_TRACE;
    Serial.write( 85 );
  }
}

void loop() 
{
  if (Serial.available() > 0) 
  {
    ProcessSerialCommand( Serial.read() );
  }
  
  if ( command == CMD_READ_ADC_TRACE )
  {
    unsigned char v = (unsigned char)(EndAnalogRead()>>2);
    
    if ( triggered == 0  )
    {
      StartAnalogRead(0);

      if ( ((v >= triggerVoltage) && ( lastADC < triggerVoltage )) || (triggerVoltage == 0) )
      {
        triggered = 1;
        digitalWrite(ledPin, LOW);
        channel = 0;
      }
    }
    else
    {
      StartAnalogRead(channel++);
      if ( channel == numChannels )
        channel=0;
      
      Serial.write(v);
      
      DataRemaining--;
      if ( DataRemaining == 0 )
      {
        command = CMD_IDLE;
      }
    }

    lastADC = v;
  }
  else if ( command == CMD_READ_BIN_TRACE )
  {
    unsigned char v = PIND>>2;  // remove tx/rx lines

    if ( triggered == 0  )
    {
      if ((lastADC & triggerVoltage) ==0)
      {
        if ((v & triggerVoltage) == triggerVoltage)
        {
          Serial.write(v);        
          triggered = 1;
          digitalWrite(ledPin, LOW);
        }
      }
    }
    else
    {
      Serial.write(v);

      DataRemaining--;
      if ( DataRemaining == 0 )
      {
        command = CMD_IDLE;
      }
    }

    lastADC = v;
  }

}

