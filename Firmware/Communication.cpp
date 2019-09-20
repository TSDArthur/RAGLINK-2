#include "Communication.h"

uint8_t USART0_Pack[COMM_PACK_MAX_SIZE + 2] = {0x00};
uint8_t USART0_PackSize = 0;
uint8_t USART1_Pack[COMM_PACK_MAX_SIZE + 2] = {0x00};
uint8_t USART1_PackSize = 0;
uint8_t USART2_Pack[COMM_PACK_MAX_SIZE + 2] = {0x00};
uint8_t USART2_PackSize = 0;
uint8_t USART3_Pack[COMM_PACK_MAX_SIZE + 2] = {0x00};
uint8_t USART3_PackSize = 0;
uint8_t COMM_Connect_State = COMM_STATE0;

void Communication_USART1_Send(uint8_t *str)
{
    Serial1.write((const char *)str);
    return;
}

void Communication_USART2_Send(uint8_t *str)
{
    Serial2.write((const char *)str);
    return;
}

void Communication_USART3_Send(uint8_t *str)
{
    Serial3.write((const char *)str);
    return;
}

bool Communication_HasReaded()
{
    bool retValue = false;
    while (Serial.available() > 0)
    {
        uint8_t tmp = (uint8_t)Serial.read();
        //If start symbol meets or pack is too long.
        if (tmp == COMM_START_SYMBOL || USART0_PackSize > COMM_PACK_MAX_SIZE)
        {
            USART0_PackSize = 0;
        }
        USART0_Pack[USART0_PackSize++] = tmp;
        //If end symbol meets.
        if (tmp == COMM_END_SYMBOL)
        {
            USART0_Pack[USART0_PackSize] = '\0';
            retValue = true;
            return retValue;
        }
    }
    return retValue;
}

bool Communication_Valid()
{
    bool retValue = true;
    retValue = retValue && (USART0_PackSize > 2);
    retValue = retValue && (USART0_Pack[0] == COMM_START_SYMBOL);
    retValue = retValue && (USART0_Pack[USART0_PackSize - 1] == COMM_END_SYMBOL);
    return retValue;
}

bool Communication_RepeatTimes(uint8_t symbol, uint8_t times)
{
    bool retValue = false;
    uint8_t counter = 0;
    for (int i = 1; i < USART0_PackSize - 1; i++)
    {
        if (USART0_Pack[i] == symbol)
        {
            counter++;
        }
        else
        {
            retValue = false;
            return retValue;
        }
    }
    if (counter == times)
    {
        retValue = true;
    }
    else
    {
        retValue = false;
    }
    return retValue;
}

void Communication_SendSpecialData(uint8_t count)
{
    Serial.write(COMM_START_SYMBOL);
    for (uint8_t i = 0; i < count; i++)
    {
        Serial.write(COMM_SPEC_SYMBOL);
        delay(COMM_CHAR_TIME);
    }
    Serial.write(COMM_END_SYMBOL);
    return;
}

bool Communication_WaitForSpecialDataResponse(uint8_t specialChar, uint8_t repeatTimes, uint8_t timeout)
{
    bool retValue = false;
    int waitCounter = 0;
    do
    {
        if (Communication_HasReaded() && Communication_Valid() && Communication_RepeatTimes(specialChar, repeatTimes))
        {
            retValue = true;
            return retValue;
        }
        else
        {
            waitCounter++;
            if (waitCounter > timeout)
            {
                retValue = false;
                return retValue;
            }
        }
        delay(COMM_WAIT_TIME);
    } while (true);
    return retValue;
}

void Communication_DoEvents()
{
    static int state3WaitCounter = 0;
    if (Communication_Valid() && Communication_RepeatTimes(COMM_SPEC_SYMBOL, COMM_RESET_SPEC_NUM))
    {
        COMM_Connect_State = COMM_STATE0;
        Device_Reset();
    }
    //State: Wait for connecting
    if (COMM_Connect_State == COMM_STATE0)
    {
        //Write Connect Symbol
        Communication_SendSpecialData(COMM_CONNECT_SPEC_NUM);
        //Wait Response
        if (Communication_WaitForSpecialDataResponse(COMM_SPEC_SYMBOL, COMM_CONNECT_SPEC_NUM, COMM_TIMEOUT))
        {
            COMM_Connect_State = COMM_STATE1;
            return;
        }
        return;
    }
    //State: Sending
    else if (COMM_Connect_State == COMM_STATE1)
    {
        //Serial.write(Core_DevicesToSend.devicesToSendCount);
        //If devices available
        if (!Core_DevicesToSend.devicesToSendCount)
        {
            COMM_Connect_State = COMM_STATE2;
            return;
        }
        //Send devices data
        int8_t sendPackSize = 0;
        for (uint8_t i = Core_DevicesToSend.sendStart; i <= Core_DevicesToSend.sendEnd; i++)
        {
            if (sendPackSize >= COMM_PACK_MAX_SIZE - 4)
            {
                sendPackSize = 0;
                Serial.write(COMM_END_SYMBOL);
                //Wait continue
                if (!Communication_WaitForSpecialDataResponse(COMM_SPEC_SYMBOL, COMM_CONTINUE_SPEC_NUM, COMM_TIMEOUT))
                {
                    COMM_Connect_State = COMM_STATE0;
                    return;
                }
                delay(COMM_WAIT_TIME);
            }
            if (!sendPackSize)
            {
                Serial.write(COMM_START_SYMBOL);
                sendPackSize++;
            }
            if (Core_GetIfDeviceToSend(i))
            {
                Serial.write(i);
                Serial.write(Device_GetState(i));
                sendPackSize += 2;
                delay(COMM_CHAR_TIME);
            }
            if (i == Core_DevicesToSend.sendEnd)
            {
                Serial.write(COMM_END_SYMBOL);
                //Wait continue
                if (!Communication_WaitForSpecialDataResponse(COMM_SPEC_SYMBOL, COMM_CONTINUE_SPEC_NUM, COMM_TIMEOUT))
                {
                    COMM_Connect_State = COMM_STATE0;
                    return;
                }
                break;
            }
        }
        //Send data trans end
        Communication_SendSpecialData(COMM_TRANSEND_SPEC_NUM);
        //Change state to wait response
        COMM_Connect_State = COMM_STATE2;
        delay(COMM_WAIT_TIME);
        return;
    }
    //State: Wait response
    else if (COMM_Connect_State == COMM_STATE2)
    {
        if (Communication_HasReaded() && Communication_Valid())
        {
            state3WaitCounter = 0;
            if (Communication_RepeatTimes(COMM_SPEC_SYMBOL, COMM_TRANSEND_SPEC_NUM))
            {
                COMM_Connect_State = COMM_STATE1;
                return;
            }
            if (Communication_RepeatTimes(COMM_SPEC_SYMBOL, COMM_CLEAR_DEVICES_STATE_NUM))
            {
                Device_Reset();
            }
            Core_StreamEvents(USART0_Pack, USART0_PackSize);
            Communication_SendSpecialData(COMM_CONTINUE_SPEC_NUM);
        }
        else
        {
            delay(COMM_WAIT_TIME);
            state3WaitCounter++;
            if (state3WaitCounter > COMM_TIMEOUT)
            {
                Serial.flush();
                state3WaitCounter = 0;
                COMM_Connect_State = COMM_STATE0;
                return;
            }
        }
        return;
    }
}
