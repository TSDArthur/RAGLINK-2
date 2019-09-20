#ifndef __COMMUNICATION_H
#define __COMMUNICATION_H
#include "Arduino.h"
#include "Devices.h"
#include "Core.h"
#include "FirmwareInfo.h"
#include "string.h"
#include "String.h"
#include "HardwareSerial.h"
#include "Wire.h"
#define COMM_SERIAL_BAUD 115200
//
#define COMM_START_SYMBOL 0x7f
#define COMM_SPEC_SYMBOL 0x7e
#define COMM_SET_SYMBOL 0x7d
#define COMM_END_SYMBOL 0x7c
#define COMM_CONNECT_SPEC_NUM 2
#define COMM_TRANSEND_SPEC_NUM 3
#define COMM_CONTINUE_SPEC_NUM 4
#define COMM_RESET_SPEC_NUM 5
#define COMM_CLEAR_DEVICES_STATE_NUM 6
#define COMM_STATE0 0
#define COMM_STATE1 1
#define COMM_STATE2 2
#define COMM_PACK_MAX_SIZE 32
//
#define COMM_CHAR_TIME 1
#define COMM_WAIT_TIME 20
#define COMM_TIMEOUT 100
extern uint8_t USART0_Pack[COMM_PACK_MAX_SIZE + 2];
extern uint8_t USART0_PackSize;
extern uint8_t USART1_Pack[COMM_PACK_MAX_SIZE + 2];
extern uint8_t USART1_PackSize;
extern uint8_t USART2_Pack[COMM_PACK_MAX_SIZE + 2];
extern uint8_t USART2_PackSize;
extern uint8_t USART3_Pack[COMM_PACK_MAX_SIZE + 2];
extern uint8_t USART3_PackSize;
extern uint8_t COMM_Connect_State;
void Communication_USART1_Send(uint8_t *str);
void Communication_USART2_Send(uint8_t *str);
void Communication_USART3_Send(uint8_t *str);
bool Communication_HasReaded();
bool Communication_Valid();
bool Communication_RepeatTimes(uint8_t symbol, uint8_t count);
void Communication_SendSpecialData(uint8_t count);
void Communication_DoEvents();
#endif
