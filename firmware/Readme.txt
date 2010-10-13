Important!!

The current implementation of the serial libraries in teh Arduino is quite poor and will limit the baudrate to 115200bps.

In order to break this limit you need to substitute the HardwareSerial.* files in your arduino-0018\hardware\arduino\cores\arduino\ directory with the optimized ones ones you will find in this directory.

Thanks.