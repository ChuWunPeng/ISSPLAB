#pragma once

#include <iostream>
#include <string> 
#include <vector>
#include <cstdint>

#include "json/json-forwards.h"
#include "../../sdk/StreamSDK.h"

/** IRCamera
    
	热像仪基本操作的封装，这个封闭只能包含一路视频流，
	如果需要同时访问多路视频流，可以创建多个IRCamera实例或者直接使用StreamSDK
**/
class IRCamera
{
public:
	enum stream_type
	{
		PrimaryStream = 0,			//主码流
		SubsidiaryStream = 1,		//子码流
	};

	IRCamera(const std::string & ip,const std::string & userName = std::string(),const std::string & password = std::string());
	~IRCamera(void);

	/** 测试连接，不会抛出 **/
	bool IsAlive();

	const std::string & getIP() const { return _IP;}
	int getWidth() const { return _CameraWidth; }
	int getHeight() const { return _CameraHeight; }

	/* StartH264Stream
	     创建主码流和子码流
	   StartRawStream
		 创建全辐射流

		 注意：一个IRCamera实例只管理至多一个视频流对像

	 * 异常：
	   成员函数调用会有std::runtime_error异常抛出
	   JSON解释错误会抛出Json::Exception

	 * 线程：
	   	线程不安全，StartStream后，要调用StopStream，才能再次StartStream
	**/
	void StartH264Stream(stream_type streamType,streamsdk_cb_image_grabber,void * user = NULL);
	void StartRawStream(streamsdk_cb_grabber,void * user = NULL);
	void StopStream();
	bool IsStreaming();
	
	//bool PutStreamVideoBitrate(int value);
	//void GetStreamVideoBitrate();

	/* GetPaletteList 
	      获取设备内置调色板列表
     

	 * 异常：
	   成员函数调用会有std::runtime_error异常抛出
	   JSON解释错误会抛出Json::Exception

	 * 线程安全：
	   	线程安全
	 */
	void GetPaletteList(std::vector<std::string> & );

	/* GetCurrentPalette 
	      获取设备当前调色板
     

	 * 异常：
	   成员函数调用会有std::runtime_error异常抛出
	   JSON解释错误会抛出Json::Exception

	 * 线程安全：
	   	线程安全
	 */
	std::string GetCurrentPalette();

	/* GetCurrentPalette 
	      设置设备当前调色板
     

	 * 异常：
	   成员函数调用会有std::runtime_error异常抛出
	   JSON解释错误会抛出Json::Exception

	 * 线程安全：
	   	线程安全
	 */
	void SetCurrentPalette(const std::string & plt,bool inverse = false);
	
	/* GetTemperature 
	      获取图像平面上某点的温度（摄氏）
		  x,y为传感器像素坐标（GetWidth,GetHeight)
     

	 * 异常：
	   成员函数调用会有std::runtime_error异常抛出
	   JSON解释错误会抛出Json::Exception

	 * 线程安全：
	   	线程安全
	 */
	float GetTemperature(int x,int y);

	/*  AutoFocus 
		  设备执行一次自动对焦

	 * 异常：
	   成员函数调用会有std::runtime_error异常抛出
	   JSON解释错误会抛出Json::Exception

	 * 线程安全：
	   	线程安全
	 */
	void AutoFocus();

	/*  SnapRaw 
		  获取全辐射快照

	 * 异常：
	   成员函数调用会有std::runtime_error异常抛出
	   JSON解释错误会抛出Json::Exception

	 * 线程安全：
	   	线程安全
	 */
	char* SnapRaw();

	/*  SnapH264 
		  获取H264快照

	 * 异常：
	   成员函数调用会有std::runtime_error异常抛出
	   JSON解释错误会抛出Json::Exception

	 * 线程安全：
	   	线程安全
	 */
	char* SnapH264();
private:

	std::string _IP;
	std::string _UserName;
	std::string _Password;
	int _CameraWidth;
	int _CameraHeight;
	std::string _VideoMode;

	//int _CurrentBitrate;

	streamsdk_cb_grabber _GrabberRawCallback;
	streamsdk_cb_image_grabber _GrabberImageCallback;

	HSTREAM _HStream;
	HDECODER _HDecoder;
};

