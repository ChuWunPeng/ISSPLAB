#include "StdAfx.h"

#include "json/json.h"
#include "RestCmd.h"
#include "IRCamera.h"

static unsigned short CMD_PORT = 10080;
static unsigned short STREAM_PORT = 10081;

IRCamera::IRCamera(const std::string & ip,const std::string & userName,const std::string & password)
	:_IP(ip),_UserName(userName),_Password(password)
{
	_HCb = NULL;

	RestCmd::setAuthorization(_UserName, _Password);
	RestCmd restcmd(_IP,CMD_PORT);

	try
	{
		Json::Value jv;
		//restcmd.restGet(URL_ADMIN_INFO,jv);
		restcmd.setTimeout(3000);
		restcmd.restGet("/sensor/dimension",jv);

		_CameraWidth = jv["w"].asInt();
		_CameraHeight = jv["h"].asInt();

	}
	catch (std::exception & e)
	{
		throw std::runtime_error("Fail to connect camera: " + std::string(e.what()));
	}

}


IRCamera::~IRCamera(void)
{
	StopStream();
}

bool IRCamera::IsAlive()
{
	//char * buf=new char[1024];
	RestCmd restcmd(_IP,CMD_PORT);

	/** 测试连接状态，用一个较短的超时 **/
	restcmd.setTimeout(3000);
	try
	{
		Json::Value jv;
		restcmd.restGet("/admin/boot-id",jv);
	}
	catch(std::exception & )
	{
		return false;
	}
	return true;
}

void
IRCamera::StartStream(streamsdk_cb_grabber grabber,void * user)
{
	if(_HCb)
		throw std::runtime_error("IRCamera can create only one stream in this Example");

	if(streamsdk_create_stream((char*)_IP.c_str(),STREAM_PORT,&_HCb) != STREAMSDK_EC_OK)
		throw std::runtime_error("streamsdk_create_stream failure");

	int max_packet_size = 0;
	
	RestCmd rc(_IP,CMD_PORT);
	
	Json::Value jv;
	rc.restGet("/stream/tag/values",jv);
	max_packet_size = jv["max-packet-size"].asInt();  
	
	/** 开始全辐射流接收，并设置回调函数 **/
	streamsdk_start_stream(_HCb,"/tag/values",max_packet_size,grabber,user);
}

void IRCamera::StopStream()
{
	if(_HCb!= NULL)
	{
		streamsdk_stop_stream(_HCb);
		streamsdk_destroy_stream(_HCb);
		_HCb = NULL;
	}
}

bool IRCamera::IsStreaming()
{
	return _HCb != NULL;
}