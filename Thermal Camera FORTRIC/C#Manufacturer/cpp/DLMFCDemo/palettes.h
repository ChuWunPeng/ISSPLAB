#pragma once
#include <cstdint>

typedef uint16_t RawValue;
void convertRawToBGR(const RawValue * value,int width,int height,BYTE * bgr_buf, uint8_t palette[256][3]);

extern uint8_t Grey[256][3];
extern uint8_t Iron[256][3];
extern uint8_t Rainbow[256][3];
