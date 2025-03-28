#include "StdAfx.h"

#include "json/json.h"
#include "RestCmd.h"
#include "IRCamera.h"

static unsigned short CMD_PORT = 10080;
static unsigned short STREAM_PORT = 10081;
static const char * RawPalette[3] = {"Grey","Iron","Rainbow"};

IRCamera::IRCamera(const std::string & ip,const std::string & userName,const std::string & password)
	:_IP(ip),_UserName(userName),_Password(password)
{
	_HStream = NULL;
	_HDecoder = NULL;

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

	/** ��������״̬����һ���϶̵ĳ�ʱ **/
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

void IRCamera::StartH264Stream(stream_type streamType,streamsdk_cb_image_grabber image_grabber,void * user)
{
	if(_HStream)
		throw std::runtime_error("IRCamera can create only one stream in this Example");


	int max_packet_size = 0;
	
	std::string video_path;
	switch(streamType)
	{
	case PrimaryStream:
		video_path = "/video/pri";
		break;
	case SubsidiaryStream:
		video_path = "/video/sub";
		break;
	default:
		throw std::runtime_error("Invalid stream_type");
	}
	
	streamsdk_st_decoder_param dp;

	try
	{
		/** ��ȡ�����յı���֡��������С�������С�Ļ����յ����ݿ��ܻ���� **/
		RestCmd rc(_IP,CMD_PORT);

		Json::Value jv;	
		rc.restGet(("/stream" + video_path).c_str(),jv);
		max_packet_size = jv["max-packet-size"].asInt();  

		/** ���÷��ؽ�����ͼƬ��С�����ﱣ���������еĳߴ���ͬ **/
		dp.dec_w = jv["width"].asInt();
		dp.dec_h = jv["height"].asInt();

		/** �����ÿ֡ͼƬ����ɫ�ռ����� **/
		dp.pix_type = STREAMSDK_PIX_BGR;
	}
	catch(std::exception & )
	{
		throw std::runtime_error("Fail to get max-packet-size");
	}

	/** Ϊ���������շ�����Դ,�õ�_HStream��� **/
	if(streamsdk_create_stream((char*)_IP.c_str(),STREAM_PORT,&_HStream) != STREAMSDK_EC_OK)
		throw std::runtime_error("streamsdk_create_stream failure");


	/** ���������� **/
	if(streamsdk_create_h264_decoder(_HStream,&dp,&_HDecoder) != STREAMSDK_EC_OK)
	{
		streamsdk_destroy_stream(_HStream);
		_HStream = NULL;
		throw std::runtime_error("streamsdk_create_h264_decoder failure");
	}

	/** ��ʼ������,���뱾��֡��������С **/
	streamsdk_start_stream(_HStream,(char *)video_path.c_str(),max_packet_size,NULL,NULL);
	/** ��ʼ����,�����ûص����� **/
	streamsdk_start_h264_decoder(_HDecoder,image_grabber,user);
}

void
IRCamera::StartRawStream(streamsdk_cb_grabber grabber,void * user)
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
	
	/** ��ʼȫ���������գ������ûص����� **/
	streamsdk_start_stream(_HStream,"/video/raw",max_packet_size,grabber,user);
}

void IRCamera::StopStream()
{
	if (_HDecoder!=NULL)
	{
		streamsdk_stop_h264_decoder(_HDecoder);
		streamsdk_destroy_h264_decoder(_HDecoder);
		_HDecoder = NULL;
	}

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

float IRCamera::GetTemperature(int x,int y)
{
	RestCmd rc(_IP,CMD_PORT);

	std::ostringstream oss;
	oss <<"/isp/t?x="<<x<<"&y="<<y;

	Json::Value jv;
	rc.restGet(oss.str(),jv);
	return jv["t"].asFloat();
}

void IRCamera::GetPaletteList(std::vector<std::string> & plt_list )
{
	plt_list.clear();

	RestCmd rc(_IP,CMD_PORT);

	Json::Value jv;
	rc.restGet("/isp/t-ray/presets",jv);
	for(int i = 0;i < (int)jv.size();i++)
	{
		plt_list.push_back(jv[i].asString());
	}
}

std::string IRCamera::GetCurrentPalette()
{
	RestCmd rc(_IP,CMD_PORT);

	Json::Value jv;
	rc.restGet("/isp/t-ray/plt",jv);
	return jv["name"].asString();
}

void IRCamera::SetCurrentPalette(const std::string & plt,bool inverse)
{
	RestCmd rc(_IP,CMD_PORT);

	Json::Value jv;
	jv["inverse"] = inverse;
	jv["name"] = plt;
	rc.restPut("/isp/t-ray/plt",jv,NULL);
}


void IRCamera::AutoFocus()
{
	RestCmd rc(_IP,CMD_PORT);

	Json::Value jv;
	rc.restPost("/isp/af",jv,NULL);
}


char* IRCamera::SnapRaw()
{
	RestCmd rc(_IP,CMD_PORT);

	Json::Value jv;
	rc.restGet("/isp/snapshot",jv);
	std::string path =  jv["path"].asCString();
	char* buf = new char[1024000];
	rc.restGetBin(path,buf);
	return buf;
}

char* IRCamera::SnapH264()
{
	RestCmd rc(_IP,CMD_PORT);

	Json::Value jv;
	rc.restGet("/osd/snapshot",jv);
	std::string path =  jv["path"].asCString();
	char* buf = new char[1024000];
	rc.restGetBin(path,buf);
	return buf;
}