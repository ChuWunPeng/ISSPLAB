#include "StdAfx.h"

#include "json/json.h"
#include "RestCmd.h"
#include "IRCamera.h"

static unsigned short CMD_PORT = 10080;
static unsigned short STREAM_PORT = 10081;
static float ZERO_T = 273.15F;

IRCamera::IRCamera(const std::string & ip,const std::string & userName,const std::string & password)
	:_IP(ip),_UserName(userName),_Password(password)
{
	_HStream = NULL;

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
	if(_HStream)
		throw std::runtime_error("IRCamera can create only one stream in this Example");

	if(streamsdk_create_stream((char*)_IP.c_str(),STREAM_PORT,&_HStream) != STREAMSDK_EC_OK)
		throw std::runtime_error("streamsdk_create_stream failure");

	int max_packet_size = 0;

	RestCmd rc(_IP,CMD_PORT);

	Json::Value jv;
	rc.restGet("/stream/video/raw",jv);
	max_packet_size = jv["max-packet-size"].asInt();  

	/** 开始全辐射流接收，并设置回调函数 **/
	streamsdk_start_stream(_HStream,"/video/raw",max_packet_size,grabber,user);
}

void IRCamera::StopStream()
{
	if(_HStream!= NULL)
	{
		streamsdk_stop_stream(_HStream);
		streamsdk_destroy_stream(_HStream);
		_HStream = NULL;
	}
}

bool IRCamera::IsStreaming()
{
	return _HStream != NULL;
}

void IRCamera::UpdateFactoryLUT()
{
	RestCmd rc(_IP,CMD_PORT);

	Json::Value jv;
	rc.restGet("/sensor/lut",jv);
	int index = jv.asInt();
	std::ostringstream oss;
	oss <<"/sensor/luts/"<<index<<"?list";
	rc.restGet(oss.str(),jv);
	int from = jv[0]["r"].asInt();
	int to  = jv[0]["r"].asInt();
	for(int i = 0;i < (int)jv.size()-1 && jv[i]["r"].asInt()<jv[i+1]["r"].asInt();i++)
	{
		int tmpFrom = jv[i]["r"].asInt();
		int tmpTo = jv[i+1]["r"].asInt();
		to = tmpTo;
		float fromTemp = jv[i]["t"].asFloat();
		float toTemp = jv[i+1]["t"].asFloat();
		for (int j = tmpFrom;j<=tmpTo;j++)
		{
			LUT[j] = fromTemp + (toTemp - fromTemp) * (j - tmpFrom) / (tmpTo - tmpFrom);
		}
	}
	for (int i = 0; i < from; i++)
	{
		LUT[i] = LUT[from];
	}
	for (int i = to + 1; i < 65536; i++)
	{
		LUT[i] = LUT[to];
	}
	From = from;
	To = to;
}