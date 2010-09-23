#define FASTADC 1

// defines for setting and clearing register bits
#ifndef cbi
#define cbi(sfr, bit) (_SFR_BYTE(sfr) &= ~_BV(bit))
#endif
#ifndef sbi
#define sbi(sfr, bit) (_SFR_BYTE(sfr) |= _BV(bit))
#endif


#define BUILTINLED 13

unsigned char t, f = 0;
unsigned int i;
unsigned int v,vv;

unsigned mmax = 0, mmin = 1024, mmid;

unsigned char triggerVoltage = 0;

boolean  triggerRaising = 0;

unsigned char triggered = 0;
unsigned int vals[8];
char c = 0;
unsigned int DataRemaining = 10000;
int ledPin =  13;    // LED connected to digital pin 13

unsigned char channel=0;
unsigned char numChannels=1;
unsigned char lastADC = 0;


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
#if FASTADC
  // set prescale to 16
  sbi(ADCSRA,ADPS2) ;
  cbi(ADCSRA,ADPS1) ;
  cbi(ADCSRA,ADPS0) ;
#endif

  Serial.begin(115200);
//  Serial.begin(153600);
//  Serial.begin(9600);  
}

unsigned char command = 0;

void loop() 
{
  if (Serial.available() > 0) 
  {
    // read the incoming byte:
    unsigned char in = Serial.read();
    
    if ( in == '?' )
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
    else if ( in == 175 ) //reset
    {
      command = 0;
    } 
    else if ( in == 170 )
    {
      while( Serial.available() == 0);
      triggerVoltage = Serial.read();

      while( Serial.available() == 0);
      DataRemaining = Serial.read()<<8;

      while( Serial.available() == 0);
      DataRemaining |= Serial.read();

      while( Serial.available() == 0);
      numChannels = Serial.read();
      channel = 0;

      for (int i=0;i<4;i++)
      {
        while( Serial.available() == 0);
        Serial.read();
      }
      
      Serial.print( 85, BYTE );

      
      triggered = 0;     
      command = 170;
      StartAnalogRead(0);
      digitalWrite(ledPin, HIGH);
    }
  }
  else
  {  
    if ( command == 170 )
    {
      unsigned char v = (unsigned char)(EndAnalogRead()>>2);
     
      if ( triggered == 0  )
      {
        StartAnalogRead(0);

        if ( (v >= triggerVoltage) && ( lastADC < triggerVoltage ) )
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
        
        Serial.print(v, BYTE);
        
        DataRemaining--;
        if ( DataRemaining == 0 )
        {
          command = 0;
        }
      }

      lastADC = v;

    }
    
  }
}


