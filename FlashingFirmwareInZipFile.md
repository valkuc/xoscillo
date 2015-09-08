# How to flash your arduino with the XOscillo firmware #

> Note that this instructions are only for the firmware that comes in the binary distribution!

> The firmware can come as a HEX file or as a PDE file.

## Flasing a HEX file ##

> You will need to use AvrDude, if you have the arduino IDE installed you already have it!
> A typical command line to do so is the following:
    * **c:\arduino-0018\hardware\tools\avr\bin>** avrdude -c stk500 -p m168 -P com3 -b 19200 -U flash:w:c:\arduinooscillo.cpp.hex -C ..\etc\avrdude.conf

## Flasing a PDE file ##

> Note: New versions of XOscillo come with the firmware as a HEX file, if the firmware comes as a PDE please check for a new version.

> If you still want to use your current version just open the PDE with your arduino IDE, compile it and upload it onto your arduino.