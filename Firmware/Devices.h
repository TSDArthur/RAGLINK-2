#ifndef __DEVICES_H
#define __DEVICES_H
#include "Arduino.h"
#include "Core.h"
#include "FirmwareInfo.h"
#include "String.h"
#include "HardwareSerial.h"
#include "Wire.h"
#define SERIAL_TX_BUFFER_SIZE 128
#define SERIAL_RX_BUFFER_SIZE 128
#define DEVICE_WITHOUT_INIT 0
#define DEVICE_BUTTON_AUTOLOCK 1
#define DEVICE_BUTTON_AUTORESET 2
#define DEVICE_DIGITAL_INPUT 3
#define DEVICE_DIGITAL_OUTPUT 4
#define DEVICE_ANALOG_INPUT 5
#define DEVICE_ANALOG_OUTPUT 6
#define DEVICE_USART1_DEVICE 7
#define DEVICE_USART2_DEVICE 8
#define DEVICE_USART3_DEVICE 9
#define DEVICE_I2C_DEVICE 10
#define DEVICE_USART1_TX 18
#define DEVICE_USART1_RX 19
#define DEVICE_USART2_TX 16
#define DEVICE_USART2_RX 17
#define DEVICE_USART3_TX 14
#define DEVICE_USART3_RX 15
#define DEVICE_USART_BAUD_9600 0
#define DEVICE_USART_BAUD_19200 1
#define DEVICE_USART_BAUD_38400 2
#define DEVICE_USART_BAUD_57600 3
#define DEVICE_USART_BAUD_74880 4
#define DEVICE_USART_BAUD_115200 5
#define DEVICE_I2C_SDA 20
#define DEVICE_I2C_SCL 21
#define DEVICE_SUM 60
#define DEVICE_IO_SUM 55
#define DEVICE_USART1_DEVICE_ID 56
#define DEVICE_USART2_DEVICE_ID 57
#define DEVICE_USART3_DEVICE_ID 58
#define DEVICE_I2C_DEVICE_ID 59
//
#define DEVICE_NO_ACTIVE 0
#define DEVICE_ACTIVE 1
//
#define DEVICE_STATE1 0
#define DEVICE_STATE2 1
#define DEVICE_STATE3 2
#define DEVICE_DURATION 5
extern uint8_t Devices_I2C_Addr;
extern uint8_t Devices_Type[DEVICE_SUM];
extern uint8_t Devices_Parameters[DEVICE_SUM];
extern uint8_t Devices_State[DEVICE_SUM];
extern uint32_t Devices_Duration[DEVICE_SUM];
uint8_t Device_ID2HW(uint8_t id);
uint8_t Device_HW2ID(uint8_t hw_id);
void Device_Reset();
void Device_Setup(uint8_t setupType, uint8_t parameter);
uint8_t Device_GetState(uint8_t deviceID);
void Device_SetState(uint8_t deviceID, uint8_t value);
#endif
