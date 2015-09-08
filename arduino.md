# Arduino based oscilloscipe #

## Features ##
  * No need of extra hardware to get the basic functionality
  * Max freq 7 KHz, enough for hobbyists
  * up to 4 channels (at a lower sample rate 7/4 KHz )
  * 8bit vertical resolution
  * Variable Trigger voltage on Channel 0
  * Can sample data for as long as you need

## quick start ##

  1. [Flash your Arduino with the firmware that comes in the zip file.](FlashingFirmwareInZipFile.md)
  1. Use the analog inputs from #0 to #3
    * note that the trigger only monitors the analog channel #0.
  1. Launch the app
  1. In the menu, go to "File" and then click on "Arduino"


## notes ##

  * The Arduino adc reads values from 0 to 5 volts, it cannot read negative voltages out of the box

## Greetings ##
> Thanks to Kiril Zyapkov for his Improved HardwareSerial library.