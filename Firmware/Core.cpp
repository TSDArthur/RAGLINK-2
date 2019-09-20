#include "Core.h"

DevicesToSendInfo Core_DevicesToSend;

void Core_GetDevicesToSendInfo(DevicesToSendInfo &devicesToSend)
{
    devicesToSend.devicesToSendCount = 0;
    devicesToSend.sendStart = -1;
    devicesToSend.sendEnd = -1;
    for (uint8_t i = 0; i < DEVICE_SUM; i++)
    {
        if (Core_GetIfDeviceToSend(i))
        {
            if (devicesToSend.sendStart == -1)
            {
                devicesToSend.sendStart = i;
            }
            devicesToSend.sendEnd = i;
            devicesToSend.devicesToSendCount++;
        }
    }
    return;
}

bool Core_GetIfDeviceToSend(uint8_t deviceID)
{
    bool retValue = false;
    retValue |= (Devices_Type[deviceID] == DEVICE_BUTTON_AUTOLOCK);
    retValue |= (Devices_Type[deviceID] == DEVICE_BUTTON_AUTORESET);
    retValue |= (Devices_Type[deviceID] == DEVICE_DIGITAL_INPUT);
    retValue |= (Devices_Type[deviceID] == DEVICE_ANALOG_INPUT);
    return retValue;
}

bool Core_StreamEvents(uint8_t *str, uint8_t packSize)
{
    bool retValue = true;
    for (uint8_t i = 1; i < packSize - 1;)
    {
        if (str[i] == COMM_SET_SYMBOL)
        {
            Device_Setup((uint8_t)str[i + 2], (uint8_t)str[i + 1]);
            i += 3;
        }
        else
        {
            Device_SetState((uint8_t)str[i], (uint8_t)str[i + 1]);
            i += 2;
        }
    }
    return retValue;
}

void Core_DoEvents()
{
    //Get available devices
    Core_GetDevicesToSendInfo(Core_DevicesToSend);
    //Communication
    Communication_DoEvents();
    //delay(CORE_INV_TIME);
}
