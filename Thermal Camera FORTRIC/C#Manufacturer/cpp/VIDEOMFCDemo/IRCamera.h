#pragma once

#include <iostream>
#include <string> 
#include <vector>
#include <cstdint>

#include "json/json-forwards.h"
#include "../../sdk/StreamSDK.h"

/** IRCamera
    
	�����ǻ��������ķ�װ��������ֻ�ܰ���һ·��Ƶ����
	�����Ҫͬʱ���ʶ�·��Ƶ�������Դ������IRCameraʵ������ֱ��ʹ��StreamSDK
**/
class IRCamera
{
public:
	enum stream_type
	{
		PrimaryStream = 0,			//������
		SubsidiaryStream = 1,		//������
	};

	IRCamera(const std::string & ip,const std::string & userName = std::string(),const std::string & password = std::string());
	~IRCamera(void);

	/** �������ӣ������׳� **/
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

