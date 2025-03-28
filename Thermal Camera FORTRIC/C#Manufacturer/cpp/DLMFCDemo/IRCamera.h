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

	/* StartH264Stream
	     ������������������
	   StartRawStream
		 ����ȫ������

		 ע�⣺һ��IRCameraʵ��ֻ��������һ����Ƶ������

	 * �쳣��
	   ��Ա�������û���std::runtime_error�쳣�׳�
	   JSON���ʹ�����׳�Json::Exception

	 * �̣߳�
	   	�̲߳���ȫ��StartStream��Ҫ����StopStream�������ٴ�StartStream
	**/
	void StartH264Stream(stream_type streamType,streamsdk_cb_image_grabber,void * user = NULL);
	void StartRawStream(streamsdk_cb_grabber,void * user = NULL);
	void StopStream();
	bool IsStreaming();
	
	//bool PutStreamVideoBitrate(int value);
	//void GetStreamVideoBitrate();

	/* GetPaletteList 
	      ��ȡ�豸���õ�ɫ���б�
     

	 * �쳣��
	   ��Ա�������û���std::runtime_error�쳣�׳�
	   JSON���ʹ�����׳�Json::Exception

	 * �̰߳�ȫ��
	   	�̰߳�ȫ
	 */
	void GetPaletteList(std::vector<std::string> & );

	/* GetCurrentPalette 
	      ��ȡ�豸��ǰ��ɫ��
     

	 * �쳣��
	   ��Ա�������û���std::runtime_error�쳣�׳�
	   JSON���ʹ�����׳�Json::Exception

	 * �̰߳�ȫ��
	   	�̰߳�ȫ
	 */
	std::string GetCurrentPalette();

	/* GetCurrentPalette 
	      �����豸��ǰ��ɫ��
     

	 * �쳣��
	   ��Ա�������û���std::runtime_error�쳣�׳�
	   JSON���ʹ�����׳�Json::Exception

	 * �̰߳�ȫ��
	   	�̰߳�ȫ
	 */
	void SetCurrentPalette(const std::string & plt,bool inverse = false);
	
	/* GetTemperature 
	      ��ȡͼ��ƽ����ĳ����¶ȣ����ϣ�
		  x,yΪ�������������꣨GetWidth,GetHeight)
     

	 * �쳣��
	   ��Ա�������û���std::runtime_error�쳣�׳�
	   JSON���ʹ�����׳�Json::Exception

	 * �̰߳�ȫ��
	   	�̰߳�ȫ
	 */
	float GetTemperature(int x,int y);

	/*  AutoFocus 
		  �豸ִ��һ���Զ��Խ�

	 * �쳣��
	   ��Ա�������û���std::runtime_error�쳣�׳�
	   JSON���ʹ�����׳�Json::Exception

	 * �̰߳�ȫ��
	   	�̰߳�ȫ
	 */
	void AutoFocus();

	/*  SnapRaw 
		  ��ȡȫ�������

	 * �쳣��
	   ��Ա�������û���std::runtime_error�쳣�׳�
	   JSON���ʹ�����׳�Json::Exception

	 * �̰߳�ȫ��
	   	�̰߳�ȫ
	 */
	char* SnapRaw();

	/*  SnapH264 
		  ��ȡH264����

	 * �쳣��
	   ��Ա�������û���std::runtime_error�쳣�׳�
	   JSON���ʹ�����׳�Json::Exception

	 * �̰߳�ȫ��
	   	�̰߳�ȫ
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

