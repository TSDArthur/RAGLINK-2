#include "Arduino.h"
#include "Devices.h"
#include "Communication.h"
#include "Core.h"
#include "FirmwareInfo.h"
#include "avr/wdt.h"
#include <Wire.h>

void setup()
{
	wdt_enable(WDTO_2S);
	Serial.begin(COMM_SERIAL_BAUD);
}

void loop()
{
	Core_DoEvents();
	wdt_reset();
}
