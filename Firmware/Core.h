#ifndef __CORE_H
#define __CORE_H
#include "Arduino.h"
#include "Devices.h"
#include "Communication.h"
#include "FirmwareInfo.h"
#define CORE_INV_TIME 20
struct DevicesToSendInfo
{
	int8_t devicesToSendCount;
	int8_t sendStart;
	int8_t sendEnd;
};
extern DevicesToSendInfo Core_DevicesToSend;
void Core_GetDevicesToSendInfo(DevicesToSendInfo &devicesToSend);
bool Core_GetIfDeviceToSend(uint8_t deviceID);
bool Core_StreamEvents(uint8_t *str, uint8_t packSize);
void Core_DoEvents();
#endif
