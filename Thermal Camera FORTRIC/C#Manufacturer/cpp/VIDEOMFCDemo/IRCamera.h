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

	void StartStream(streamsdk_cb_grabber,void * user = NULL);
	void StopStream();
	bool IsStreaming();
	void UpdateFactoryLUT();;
	float LUT[65536];
	int From;
	int To;
private:

	std::string _IP;
	std::string _UserName;
	std::string _Password;
	int _CameraWidth;
	int _CameraHeight;

	//int _CurrentBitrate;

	streamsdk_cb_grabber _GrabberCallback;

	HSTREAM _HStream;
};

