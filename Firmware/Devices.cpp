#include "Devices.h"

uint8_t Devices_I2C_Addr = 0x00;
uint8_t Devices_Type[DEVICE_SUM] = {DEVICE_WITHOUT_INIT};
uint8_t Devices_Parameters[DEVICE_SUM] = {DEVICE_WITHOUT_INIT};
uint8_t Devices_State[DEVICE_SUM] = {DEVICE_STATE1};
uint32_t Devices_Duration[DEVICE_SUM] = {0};

uint8_t Device_ID2HW(uint8_t fw_id)
{
    int retValue = 0;
    //0-15 -> A0-A15 ADC
    if (fw_id <= 15)
    {
        retValue = fw_id + A0;
        return retValue;
    }
    //16-27 -> 2-13 PWM
    else if (fw_id <= 27)
    {
        retValue = fw_id - 14;
        return retValue;
    }
    //28-55 -> 22-49 IO
    else if(fw_id <= 55)
    {
        retValue = fw_id - 6;
        return retValue;
    }
    return retValue;
}

uint8_t Device_HW2ID(uint8_t hw_id)
{
    int retValue = 0;
    if (hw_id >= A0 && hw_id <= A15)
    {
        retValue = hw_id - A0;
        return retValue;
    }
    else if (hw_id >= 2 && hw_id <= 13)
    {
        retValue = hw_id + 14;
        return retValue;
    }
    else if (hw_id >= 22 && hw_id <= 49)
    {
        retValue = hw_id + 6;
        return retValue;
    }
    return retValue;
}

void Device_Reset()
{
    memset(Devices_Type, DEVICE_WITHOUT_INIT, sizeof(Devices_Type));
    memset(Devices_State, DEVICE_STATE1, sizeof(Devices_State));
    memset(Devices_Duration, 0, sizeof(Devices_Duration));
    memset(Devices_Parameters, DEVICE_WITHOUT_INIT, sizeof(Devices_Parameters));
    return;
}

void Device_Setup(uint8_t setupType, uint8_t parameter)
{
    if (setupType == DEVICE_BUTTON_AUTOLOCK || setupType == DEVICE_BUTTON_AUTORESET)
    {
        pinMode(Device_ID2HW(parameter), INPUT_PULLUP);
        Devices_Type[parameter] = setupType;
        Devices_State[parameter] = DEVICE_STATE1;
        Devices_Duration[parameter] = 0;
        Devices_Parameters[parameter] = DEVICE_NO_ACTIVE;
    }
    else if (setupType == DEVICE_DIGITAL_INPUT || setupType == DEVICE_ANALOG_INPUT)
    {
        pinMode(Device_ID2HW(parameter), INPUT);
        Devices_Type[parameter] = setupType;
        Devices_Parameters[parameter] = DEVICE_NO_ACTIVE;
    }
    else if (setupType == DEVICE_DIGITAL_OUTPUT || setupType == DEVICE_ANALOG_OUTPUT)
    {
        pinMode(Device_ID2HW(parameter), OUTPUT);
        digitalWrite(Device_ID2HW(parameter), 0);
        Devices_Type[parameter] = setupType;
        Devices_Parameters[parameter] = DEVICE_NO_ACTIVE;
    }
    else if (setupType == DEVICE_USART1_DEVICE || setupType == DEVICE_USART2_DEVICE || setupType == DEVICE_USART3_DEVICE)
    {
        switch (setupType)
        {
        case DEVICE_USART1_DEVICE:
        {
            switch (parameter)
            {
            case DEVICE_USART_BAUD_9600:
                Serial1.begin(9600);
                break;
            case DEVICE_USART_BAUD_19200:
                Serial1.begin(19200);
                break;
            case DEVICE_USART_BAUD_38400:
                Serial1.begin(38400);
                break;
            case DEVICE_USART_BAUD_57600:
                Serial1.begin(57600);
                break;
            case DEVICE_USART_BAUD_74880:
                Serial1.begin(74880);
                break;
            case DEVICE_USART_BAUD_115200:
                Serial1.begin(115200);
                break;
            }
            Devices_Type[DEVICE_USART1_DEVICE_ID] = setupType;
            Devices_Parameters[DEVICE_USART1_DEVICE_ID] = parameter;
            break;
        }
        case DEVICE_USART2_DEVICE:
        {
            switch (parameter)
            {
            case DEVICE_USART_BAUD_9600:
                Serial2.begin(9600);
                break;
            case DEVICE_USART_BAUD_19200:
                Serial2.begin(19200);
                break;
            case DEVICE_USART_BAUD_38400:
                Serial2.begin(38400);
                break;
            case DEVICE_USART_BAUD_57600:
                Serial2.begin(57600);
                break;
            case DEVICE_USART_BAUD_74880:
                Serial2.begin(74880);
                break;
            case DEVICE_USART_BAUD_115200:
                Serial2.begin(115200);
                break;
            }
            Devices_Type[DEVICE_USART2_DEVICE_ID] = setupType;
            Devices_Parameters[DEVICE_USART2_DEVICE_ID] = parameter;
            break;
        }
        case DEVICE_USART3_DEVICE:
        {
            switch (parameter)
            {
            case DEVICE_USART_BAUD_9600:
                Serial3.begin(9600);
                break;
            case DEVICE_USART_BAUD_19200:
                Serial3.begin(19200);
                break;
            case DEVICE_USART_BAUD_38400:
                Serial3.begin(38400);
                break;
            case DEVICE_USART_BAUD_57600:
                Serial3.begin(57600);
                break;
            case DEVICE_USART_BAUD_74880:
                Serial3.begin(74880);
                break;
            case DEVICE_USART_BAUD_115200:
                Serial3.begin(115200);
                break;
            }
            Devices_Type[DEVICE_USART3_DEVICE_ID] = setupType;
            Devices_Parameters[DEVICE_USART3_DEVICE_ID] = parameter;
            break;
        }
        }
    }
    else if (setupType == DEVICE_I2C_DEVICE)
    {
        Wire.beginTransmission(parameter);
        Devices_Type[DEVICE_I2C_DEVICE_ID] = setupType;
        Devices_Parameters[DEVICE_I2C_DEVICE_ID] = parameter;
    }
    return;
}

uint8_t Device_GetState(uint8_t deviceID)
{
    uint8_t retValue = 0;
    uint8_t pinID = Device_ID2HW(deviceID);
    if (Devices_Type[deviceID] == DEVICE_BUTTON_AUTOLOCK)
    {
        if (digitalRead(pinID) == LOW)
        {
            if (Devices_State[deviceID] == DEVICE_STATE1)
            {
                Devices_State[deviceID] = DEVICE_STATE2;
                Devices_Duration[deviceID] = 0;
            }
            else if (Devices_State[deviceID] == DEVICE_STATE2)
            {
                Devices_Duration[deviceID]++;
                if (Devices_Duration[deviceID] >= DEVICE_DURATION)
                {
                    Devices_Duration[deviceID] = 0;
                    Devices_State[deviceID] = DEVICE_STATE3;
                    retValue = DEVICE_ACTIVE;
                    return retValue;
                }
            }
            else if (Devices_State[deviceID] == DEVICE_STATE3)
            {
                retValue = DEVICE_ACTIVE;
                return retValue;
            }
        }
        else
        {
            Devices_State[deviceID] = DEVICE_STATE1;
            retValue = DEVICE_NO_ACTIVE;
            return retValue;
        }
    }
    else if (Devices_Type[deviceID] == DEVICE_BUTTON_AUTORESET)
    {
        if (digitalRead(pinID) == LOW)
        {
            if (Devices_State[deviceID] == DEVICE_STATE1)
            {
                Devices_State[deviceID] = DEVICE_STATE2;
                Devices_Duration[deviceID] = 0;
            }
            else if (Devices_State[deviceID] == DEVICE_STATE2)
            {
                Devices_Duration[deviceID]++;
                if (Devices_Duration[deviceID] >= DEVICE_DURATION)
                {
                    Devices_Duration[deviceID] = 0;
                    Devices_State[deviceID] = DEVICE_STATE3;
                    retValue = DEVICE_ACTIVE;
                    return retValue;
                }
            }
        }
        else
        {
            Devices_State[deviceID] = DEVICE_STATE1;
            retValue = DEVICE_NO_ACTIVE;
            return retValue;
        }
    }
    else if (Devices_Type[deviceID] == DEVICE_DIGITAL_INPUT)
    {
        retValue = digitalRead(pinID);
        return retValue;
    }
    else if (Devices_Type[deviceID] == DEVICE_ANALOG_INPUT)
    {
        retValue = map(analogRead(pinID), 0, 255, 0, 100);
        return retValue;
    }
    return retValue;
}

void Device_SetState(uint8_t deviceID, uint8_t value)
{
    uint8_t setValue = 0;
    uint8_t pinID = Device_ID2HW(deviceID);
    if (Devices_Type[deviceID] == DEVICE_DIGITAL_OUTPUT)
    {
        setValue = value;
        digitalWrite(pinID, setValue);
    }
    else if (Devices_Type[deviceID] == DEVICE_ANALOG_OUTPUT)
    {
        setValue = map(value, 0, 100, 0, 255);
        analogWrite(pinID, setValue);
    }
}
