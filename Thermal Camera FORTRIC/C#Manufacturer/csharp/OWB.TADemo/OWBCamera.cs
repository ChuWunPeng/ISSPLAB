using System;
using System.Collections.Generic;
using System.Text;
using SDK;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;

namespace OWB.TADemo
{
    public class OWBCamera
    {
        #region Const
        private readonly static int[] VERSION_3_4_0_0 = new int[] { 3, 4, 0, 0 };

        public static int HEADERLENGTH = 16;
        private const int PORT = 10080;
        private const string URL_ADMIN_INFO = "/admin/info";
        private const string URL_SENSOR_DIMENSION = "/sensor/dimension";
        private const string URL_STREAM_VIDEO_PRI = "/stream/video/pri";
        private const string URL_STREAM_TAG_VALUES = "/stream/tag/values";
        private const string URL_ISP_STREAM_INSTRUMENT_STREAMING = "/isp/instrument/streaming";
        private const string URL_TAG = "/tag/values";
        private const string URL_ISP_T = @"/isp/t?x={0}&y={1}";
        private const string URL_ISP_INSTRUMENT_JCONFIG = "/isp/instrument/jconfig";
        private const string URL_ISP_INSTRUMENT_OBJECTS_SPOTS = @"/isp/instrument/objects/points";
        private const string URL_ISP_INSTRUMENT_OBJECTS_REGIONS = @"/isp/instrument/objects/regions";
        private const string URL_ISP_INSTRUMENT_OBJECTS_LINES = @"/isp/instrument/objects/lines";
        private const string URL_ISP_INSTRUMENT_OBJECTS_SPOTITEM = @"/isp/instrument/objects/points/{0}";
        private const string URL_ISP_INSTRUMENT_OBJECTS_REGIONITEM = @"/isp/instrument/objects/regions/{0}";
        private const string URL_ISP_INSTRUMENT_OBJECTS_LINEITEM = @"/isp/instrument/objects/lines/{0}";
        private const string URL_ADMIN_BOOT_ID = "/admin/boot-id";
        private const string URL_PERI_SERIALPORT = "/peri/serial-port";
        private const string URL_OSD_SNAPSHOT = "/osd/snapshot";
        private const string URL_FILE_CACHE = "/file/cache";
        private const string URL_SENSOR_COMMAND = @"/sensor/command";
        private const string URL_SENSOR_INFO = @"/sensor/info";
        #endregion

        #region Fields
        private IntPtr _HStream;
        private IntPtr _HCb;
        private IntPtr _Decoder;

        private bool _IsConnected;
        private bool _IsSupportCompensationDistance;

        private string _IP;
        private string _UserName;
        private string _Password;

        private byte[] _EncryptPassword;

        private string _BootID;

        private int[] _FirmwareVersion;
        private int _Width;
        private int _Height;

        private Image _ChannelImage;
        private Dictionary<string, object> _TagMap;

        private StreamSDK.streamsdk_cb_image_grabber _ImageGrabberCallback;
        private StreamSDK.streamsdk_cb_grabber _StreamGrabberCallback;

        private object CameraObject = new object();
        #endregion

        #region Constructors
        public OWBCamera()
        {
            InitProperties();

            IP = string.Empty;
            UserName = string.Empty;
            Password = string.Empty;

            ImageGrabberCallback = GrabberH264;
            StreamGrabberCallback = GrabberStream;
        }
        #endregion

        #region Properties

        public IntPtr HStream
        {
            get { return _HStream; }
            private set { _HStream = value; }
        }

        public IntPtr HCb
        {
            get { return _HCb; }
            set { _HCb = value; }
        }

        public IntPtr Decoder
        {
            get { return _Decoder; }
            set { _Decoder = value; }
        }

        public bool IsConnected
        {
            get { return _IsConnected; }
            private set { _IsConnected = value; }
        }

        public string IP
        {
            get { return _IP; }
            private set { _IP = value; }
        }

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        private byte[] EncryptPassword
        {
            get { return _EncryptPassword; }
            set { _EncryptPassword = value; }
        }

        private string BootID
        {
            get { return _BootID; }
            set { _BootID = value; }
        }

        public int[] FirmwareVersion
        {
            get { return _FirmwareVersion; }
            set { _FirmwareVersion = value; }
        }

        public int Width
        {
            get { return _Width; }
            set { _Width = value; }
        }

        public int Height
        {
            get { return _Height; }
            set { _Height = value; }
        }

        public Image ChannelImage
        {
            get { return _ChannelImage; }
            set { _ChannelImage = value; }
        }

        public Dictionary<string, object> TagMap
        {
            get { return _TagMap; }
            set { _TagMap = value; }
        }

        public StreamSDK.streamsdk_cb_image_grabber ImageGrabberCallback
        {
            get { return _ImageGrabberCallback; }
            set { _ImageGrabberCallback = value; }
        }

        public StreamSDK.streamsdk_cb_grabber StreamGrabberCallback
        {
            get { return _StreamGrabberCallback; }
            set { _StreamGrabberCallback = value; }
        }
        #endregion

        #region Methods

        private void InitProperties()
        {
            HStream = IntPtr.Zero;
            HCb = IntPtr.Zero;
            Decoder = IntPtr.Zero;

            IsConnected = false;

            BootID = string.Empty;

            Width = 80;
            Height = 80;

            _IsSupportCompensationDistance = false;
        }

        public bool LoginCamera(string ipAddress, string userName, string password)
        {
            IP = ipAddress;
            UserName = userName;
            Password = password;

            if (IsConnected)
            {
                LogoutCamera();
            }
            InitProperties();

            byte[] buf = new byte[1024];
            byte[] passwordBytes = new byte[0];
            uint length = 0;
            if (!OWBTypes.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_ADMIN_INFO, ref buf, ref length)))
            {
                return false;
            }
            try
            {
                OWBTypes.AdminInfo adminInfo = OWBJson.parse<OWBTypes.AdminInfo>(OWBString.BytesToString(buf, Encoding.UTF8));
                FirmwareVersion = adminInfo.Device_FW.Version.Clone() as int[];
            }
            catch
            {
                FirmwareVersion = new int[4];
            }

            BootID = GetAdminBootID();
            EncryptPassword = Encoding.Default.GetBytes(Password);
            RestSDK.Rest_set_authroization(userName, EncryptPassword);
            if (!OWBTypes.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_SENSOR_DIMENSION, ref buf, ref length)))
            {
                return false;
            }
            try
            {
                OWBTypes.SensorDimension sensorDimension = OWBJson.parse<OWBTypes.SensorDimension>(OWBString.BytesToString(buf, Encoding.UTF8));
                Width = sensorDimension.W;
                Height = sensorDimension.H;
            }
            catch
            {
                return false;
            }

            if (GetTempCoreFirmwareVersion(out string versionStr))
            {
                int[] version = StringToVersion(versionStr);
                if (CompareVersion(version, VERSION_3_4_0_0) >= 0)
                {
                    _IsSupportCompensationDistance = true;
                }
            }

            if (CreateStream())
            {
                IsConnected = true;
                return true;
            }
            return false;
        }

        public void LogoutCamera()
        {
            StopStream();
            IsConnected = false;
        }

        public bool CreateStream()
        {
            StreamSDK.streamsdk_set_thread_pool_size(4);
            IntPtr hStream = IntPtr.Zero;
            if (StreamSDK.streamsdk_create_stream(IP, (ushort)(PORT + 1), ref hStream) != StreamSDK.STREAMSDK_EC_OK)
            {
                return false;
            }
            HStream = hStream;
            IntPtr hcb = IntPtr.Zero;
            if (StreamSDK.streamsdk_create_stream(IP, (ushort)(PORT + 1), ref hcb) != StreamSDK.STREAMSDK_EC_OK)
            {
                return false;
            }
            HCb = hcb;
            return true;
        }

        public bool StartStream()
        {
            int max_packet_size = 0;
            #region 视频流
            byte[] buf = new byte[1024];
            uint length = 0;
            int width = Width;
            int height = Height;
            if (!OWBTypes.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_STREAM_VIDEO_PRI, ref buf, ref length)))
            {
                return false;
            }
            try
            {
                OWBTypes.StreamVideoPri streamVideoPri = OWBJson.parse<OWBTypes.StreamVideoPri>(OWBString.BytesToString(buf, Encoding.UTF8));
                max_packet_size = streamVideoPri.Max_Packet_Size;
                width = streamVideoPri.Width;
                height = streamVideoPri.Height;
            }
            catch
            {
                return false;
            }

            if (StreamSDK.streamsdk_start_stream(HStream, StreamSDK.URL_STREAM_VIDEO_PRI, max_packet_size, null, IntPtr.Zero) != StreamSDK.STREAMSDK_EC_OK)
            {
                return false;
            }

            IntPtr dec = IntPtr.Zero;
            StreamSDK.streamsdk_st_decoder_param dp = new StreamSDK.streamsdk_st_decoder_param();
            dp.dec_w = width;
            dp.dec_h = height;
            dp.pix_type = (int)StreamSDK.streamsdk_enum_pix_type.STREAMSDK_PIX_BGR;
            StreamSDK.streamsdk_create_h264_decoder(HStream, ref dp, ref dec);
            Decoder = dec;
            StreamSDK.streamsdk_start_h264_decoder(Decoder, ImageGrabberCallback, IntPtr.Zero);
            #endregion

            #region 数据流
            if (!OWBTypes.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_STREAM_TAG_VALUES, ref buf, ref length)))
            {
                return false;
            }
            try
            {
                OWBTypes.MaxPackageSize maxPackageSize = OWBJson.parse<OWBTypes.MaxPackageSize>(OWBString.BytesToString(buf, Encoding.UTF8));
                max_packet_size = maxPackageSize.Max_Packet_Size;
            }
            catch
            {
                return false;
            }
            #endregion
            if (StreamSDK.streamsdk_start_stream(HCb, URL_TAG, max_packet_size, null, IntPtr.Zero) != StreamSDK.STREAMSDK_EC_OK)
            {
                return false;
            }
            StreamSDK.streamsdk_set_stream_grabber(HCb, StreamGrabberCallback, IntPtr.Zero);
            return true;
        }

        private void StopStream()
        {
            StreamSDK.streamsdk_stop_h264_decoder(Decoder);
            StreamSDK.streamsdk_destroy_h264_decoder(Decoder);
            if (HStream != IntPtr.Zero)
            {
                StreamSDK.streamsdk_stop_stream(HStream);
                StreamSDK.streamsdk_destroy_stream(HStream);
                HStream = IntPtr.Zero;
            }
            if (HCb != IntPtr.Zero)
            {
                StreamSDK.streamsdk_stop_stream(HCb);
                StreamSDK.streamsdk_destroy_stream(HCb);
                HCb = IntPtr.Zero;
            }
        }

        public string GetIspTItem(Point p)
        {
            string temperature = "0.0 ℃";
            byte[] buf = new byte[1024];
            uint length = 0;
            try
            {
                if (!OWBTypes.CheckStatus(RestSDK.Get(IP, (ushort)PORT, string.Format(URL_ISP_T, p.X, p.Y), ref buf, ref length)))
                {
                    return temperature;
                }
                OWBTypes.IspTItem ispTItem = OWBJson.parse<OWBTypes.IspTItem>(OWBString.BytesToString(buf, Encoding.UTF8));
                temperature = ispTItem.T.ToString("F1") + " ℃";
            }
            catch
            { }
            return temperature;
        }

        public OWBTypes.InstrumentJconfig GetInstrumentJconfig()
        {
            OWBTypes.InstrumentJconfig instrumentJconfig = new OWBTypes.InstrumentJconfig();
            byte[] buf = new byte[1024];
            uint length = 0;
            try
            {
                if (!OWBTypes.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_ISP_INSTRUMENT_JCONFIG, ref buf, ref length)))
                {
                    return instrumentJconfig;
                }
                instrumentJconfig = OWBJson.parse<OWBTypes.InstrumentJconfig>(OWBString.BytesToString(buf, Encoding.UTF8));
            }
            catch
            { }
            return instrumentJconfig;
        }

        public bool PutInstrumentJconfig(OWBTypes.InstrumentJconfig obj)
        {
            string objStr = OWBJson.stringify(obj);
            byte[] buf = new byte[1024];
            OWBString.StringToBytes(objStr, objStr.Length, Encoding.UTF8).CopyTo(buf, 0);
            uint length = 0;
            if (!OWBTypes.CheckStatus(RestSDK.Put(IP, (ushort)PORT, URL_ISP_INSTRUMENT_JCONFIG, ref buf, ref length)))
            {
                return false;
            }
            return true;
        }

        public OWBTypes.SerialPort GetSerialport()
        {
            OWBTypes.SerialPort serialPort = new OWBTypes.SerialPort();
            byte[] buf = new byte[1024];
            uint length = 0;
            try
            {
                if (!OWBTypes.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_PERI_SERIALPORT, ref buf, ref length)))
                {
                    return serialPort;
                }
                serialPort = OWBJson.parse<OWBTypes.SerialPort>(OWBString.BytesToString(buf, Encoding.UTF8));
            }
            catch
            { }
            return serialPort;
        }

        public bool PutSerialport(OWBTypes.SerialPort obj)
        {
            string objStr = OWBJson.stringify(obj);
            byte[] buf = new byte[1024];
            OWBString.StringToBytes(objStr, objStr.Length, Encoding.UTF8).CopyTo(buf, 0);
            uint length = 0;
            if (!OWBTypes.CheckStatus(RestSDK.Put(IP, (ushort)PORT, URL_PERI_SERIALPORT, ref buf, ref length)))
            {
                return false;
            }
            return true;
        }

        public List<MarkerItem> GetMarkerItems()
        {
            List<MarkerItem> markerItemList = new List<MarkerItem>();
            byte[] buf = new byte[1024];
            uint length = 0;
            if (OWBTypes.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_ISP_INSTRUMENT_OBJECTS_SPOTS, ref buf, ref length)))
            {
                try
                {
                    string[] tempLabels = OWBJson.parse<string[]>(OWBString.BytesToString(buf, Encoding.UTF8));
                    for (int i = 0; i < tempLabels.Length; i++)
                    {
                        markerItemList.Add(new MarkerItem(tempLabels[i], MarkerType.Spot));
                    }
                }
                catch { }
            }
            if (OWBTypes.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_ISP_INSTRUMENT_OBJECTS_REGIONS, ref buf, ref length)))
            {
                try
                {
                    string[] tempLabels = OWBJson.parse<string[]>(OWBString.BytesToString(buf, Encoding.UTF8));
                    for (int i = 0; i < tempLabels.Length; i++)
                    {
                        markerItemList.Add(new MarkerItem(tempLabels[i], MarkerType.Region));
                    }
                }
                catch { }
            }
            if (OWBTypes.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_ISP_INSTRUMENT_OBJECTS_LINES, ref buf, ref length)))
            {
                try
                {
                    string[] tempLabels = OWBJson.parse<string[]>(OWBString.BytesToString(buf, Encoding.UTF8));
                    for (int i = 0; i < tempLabels.Length; i++)
                    {
                        markerItemList.Add(new MarkerItem(tempLabels[i], MarkerType.Line));
                    }
                }
                catch { }
            }
            return markerItemList;
        }

        public bool PutModbus(string markerName, MarkerType markerType, OWBTypes.ModbusSlot modbus)
        {
            string url = string.Empty;
            switch (markerType)
            {
                case MarkerType.Spot:
                    url = string.Format(URL_ISP_INSTRUMENT_OBJECTS_SPOTITEM, markerName);
                    break;
                case MarkerType.Region:
                    url = string.Format(URL_ISP_INSTRUMENT_OBJECTS_REGIONITEM, markerName);
                    break;
                case MarkerType.Line:
                    url = string.Format(URL_ISP_INSTRUMENT_OBJECTS_LINEITEM, markerName);
                    break;
            }
            string objStr = OWBJson.stringify(modbus);
            byte[] buf = new byte[1024];
            OWBString.StringToBytes(objStr, objStr.Length, Encoding.UTF8).CopyTo(buf, 0);
            uint length = 0;
            if (!OWBTypes.CheckStatus(RestSDK.Put(IP, (ushort)PORT, url, ref buf, ref length)))
            {
                return false;
            }
            return true;
        }

        public bool PutAlarm(string markerName, MarkerType markerType, OWBTypes.Alarm alarm)
        {
            string url = string.Empty;
            switch (markerType)
            {
                case MarkerType.Spot:
                    url = string.Format(URL_ISP_INSTRUMENT_OBJECTS_SPOTITEM, markerName);
                    break;
                case MarkerType.Region:
                    url = string.Format(URL_ISP_INSTRUMENT_OBJECTS_REGIONITEM, markerName);
                    break;
                case MarkerType.Line:
                    url = string.Format(URL_ISP_INSTRUMENT_OBJECTS_LINEITEM, markerName);
                    break;
            }
            string objStr = OWBJson.stringify(alarm);
            byte[] buf = new byte[1024];
            OWBString.StringToBytes(objStr, objStr.Length, Encoding.UTF8).CopyTo(buf, 0);
            uint length = 0;
            if (!OWBTypes.CheckStatus(RestSDK.Put(IP, (ushort)PORT, url, ref buf, ref length)))
            {
                return false;
            }
            return true;
        }

        private string[] GetLimitProps(OWBTypes.Marker marker)
        {
            List<string> limitProps = new List<string>();
            if (marker!=null)
            {
                if (!marker.EmissionVisible)
                {
                    limitProps.Add("emissivity");
                }
                if (!marker.DistanceVisible)
                {
                    limitProps.Add("distance");
                }
            }
            return limitProps.ToArray();
        }

        public bool PutPoint(string markerName, OWBTypes.SpotMarker pointMarker)
        {
            string objStr = OWBJson.serialize(pointMarker, GetLimitProps(pointMarker));
            byte[] buf = new byte[1024];
            OWBString.StringToBytes(objStr, objStr.Length, Encoding.UTF8).CopyTo(buf, 0);
            uint length = 0;
            if (!OWBTypes.CheckStatus(RestSDK.Put(IP, (ushort)PORT, string.Format(URL_ISP_INSTRUMENT_OBJECTS_SPOTITEM, markerName), ref buf, ref length)))
            {
                return false;
            }
            return true;
        }

        public OWBTypes.SpotMarker GetPoint(string markerName)
        {
            OWBTypes.SpotMarker pointMarker = null;
            byte[] buf = new byte[1024];
            uint length = 0;
            try
            {
                if (!OWBTypes.CheckStatus(RestSDK.Get(IP, (ushort)PORT, string.Format(URL_ISP_INSTRUMENT_OBJECTS_SPOTITEM, markerName), ref buf, ref length)))
                {
                    return pointMarker;
                }
                pointMarker = OWBJson.deserialize<OWBTypes.SpotMarker>(OWBString.BytesToString(buf, Encoding.UTF8));
            }
            catch
            { }
            return pointMarker;
        }

        public bool DelPoint(string markerName)
        {
            byte[] buf = new byte[0];
            uint length = 0;
            if (!OWBTypes.CheckStatus(RestSDK.Delete(IP, (ushort)PORT, string.Format(URL_ISP_INSTRUMENT_OBJECTS_SPOTITEM, markerName), ref buf, ref length)))
            {
                return false;
            }
            return true;
        }

        public bool PutRegion(string markerName, OWBTypes.PolygonMarker regionMarker)
        {
            string objStr = OWBJson.serialize(regionMarker, GetLimitProps(regionMarker));
            byte[] buf = new byte[1024];
            OWBString.StringToBytes(objStr, objStr.Length, Encoding.UTF8).CopyTo(buf, 0);
            uint length = 0;
            if (!OWBTypes.CheckStatus(RestSDK.Put(IP, (ushort)PORT, string.Format(URL_ISP_INSTRUMENT_OBJECTS_REGIONITEM, markerName), ref buf, ref length)))
            {
                return false;
            }
            return true;
        }

        public OWBTypes.PolygonMarker GetRegion(string markerName)
        {
            OWBTypes.PolygonMarker regionMarker = null;
            byte[] buf = new byte[1024];
            uint length = 0;
            try
            {
                if (!OWBTypes.CheckStatus(RestSDK.Get(IP, (ushort)PORT, string.Format(URL_ISP_INSTRUMENT_OBJECTS_REGIONITEM, markerName), ref buf, ref length)))
                {
                    return regionMarker;
                }
                regionMarker = OWBJson.deserialize<OWBTypes.PolygonMarker>(OWBString.BytesToString(buf, Encoding.UTF8));
            }
            catch
            { }
            return regionMarker;
        }

        public bool DelRegion(string markerName)
        {
            byte[] buf = new byte[0];
            uint length = 0;
            if (!OWBTypes.CheckStatus(RestSDK.Delete(IP, (ushort)PORT, string.Format(URL_ISP_INSTRUMENT_OBJECTS_REGIONITEM, markerName), ref buf, ref length)))
            {
                return false;
            }
            return true;
        }

        public bool GetMask(byte[] mask, out string path)
        {
            path = string.Empty;
            byte[] buf = new byte[1024];
            uint length = 0;
            if (OWBTypes.CheckStatus(RestSDK.Postbuffer(IP, (ushort)PORT, URL_FILE_CACHE, Encoding.Default.GetString(mask), ref buf, ref length)))
            {
                try
                {
                    Dictionary<string, object> pathMap = new Dictionary<string, object>();
                    pathMap = OWBJson.parse(OWBString.BytesToString(buf, Encoding.UTF8));
                    path = Convert.ToString(pathMap["path"]);
                }
                catch
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public bool PutMask(string markerName, OWBTypes.MaskMarker maskMarker)
        {
            string objStr = OWBJson.serialize(maskMarker, GetLimitProps(maskMarker));
            byte[] buf = new byte[1024];
            OWBString.StringToBytes(objStr, objStr.Length, Encoding.UTF8).CopyTo(buf, 0);
            uint length = 0;
            if (!OWBTypes.CheckStatus(RestSDK.Put(IP, (ushort)PORT, string.Format(URL_ISP_INSTRUMENT_OBJECTS_REGIONITEM, markerName), ref buf, ref length)))
            {
                return false;
            }
            return true;
        }

        public OWBTypes.MaskMarker GetMask(string markerName)
        {
            OWBTypes.MaskMarker maskMarker = null;
            byte[] buf = new byte[1024];
            uint length = 0;
            try
            {
                if (!OWBTypes.CheckStatus(RestSDK.Get(IP, (ushort)PORT, string.Format(URL_ISP_INSTRUMENT_OBJECTS_REGIONITEM, markerName), ref buf, ref length)))
                {
                    return maskMarker;
                }
                maskMarker = OWBJson.deserialize<OWBTypes.MaskMarker>(OWBString.BytesToString(buf, Encoding.UTF8));
            }
            catch
            { }
            return maskMarker;
        }

        public bool PutLine(string markerName, OWBTypes.LineMarker lineMarker)
        {
            string objStr = OWBJson.serialize(lineMarker, GetLimitProps(lineMarker));
            byte[] buf = new byte[1024];
            OWBString.StringToBytes(objStr, objStr.Length, Encoding.UTF8).CopyTo(buf, 0);
            uint length = 0;
            if (!OWBTypes.CheckStatus(RestSDK.Put(IP, (ushort)PORT, string.Format(URL_ISP_INSTRUMENT_OBJECTS_LINEITEM, markerName), ref buf, ref length)))
            {
                return false;
            }
            return true;
        }

        public OWBTypes.LineMarker GetLine(string markerName)
        {
            OWBTypes.LineMarker lineMarker = null;
            byte[] buf = new byte[1024];
            uint length = 0;
            try
            {
                if (!OWBTypes.CheckStatus(RestSDK.Get(IP, (ushort)PORT, string.Format(URL_ISP_INSTRUMENT_OBJECTS_LINEITEM, markerName), ref buf, ref length)))
                {
                    return lineMarker;
                }
                lineMarker = OWBJson.deserialize<OWBTypes.LineMarker>(OWBString.BytesToString(buf, Encoding.UTF8));
            }
            catch
            { }
            return lineMarker;
        }

        public bool DelLine(string markerName)
        {
            byte[] buf = new byte[0];
            uint length = 0;
            if (!OWBTypes.CheckStatus(RestSDK.Delete(IP, (ushort)PORT, string.Format(URL_ISP_INSTRUMENT_OBJECTS_LINEITEM, markerName), ref buf, ref length)))
            {
                return false;
            }
            return true;
        }

        private void GrabberH264(int error, ref StreamSDK.streamsdk_st_image image, IntPtr user_data)
        {
            lock (CameraObject)
            {
                if (error != StreamSDK.STREAMSDK_EC_OK)
                {
                    return;
                }

                int width = image.img_w;
                int height = image.img_h;
                if (image.img_h != height || image.img_w != width)
                {
                    return;
                }

                Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
                int stride = bmpData.Stride;
                int offset = stride - width * 3;

                unsafe
                {
                    byte* dPtr = (byte*)(bmpData.Scan0);
                    byte* sPtr = (byte*)image.img_ptr;
                    int i = 0;
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            dPtr[0] = sPtr[i++];
                            dPtr[1] = sPtr[i++];
                            dPtr[2] = sPtr[i++];
                            dPtr += 3;
                        }
                        dPtr += offset;
                    }
                }
                bitmap.UnlockBits(bmpData);
                ChannelImage = bitmap;
            }
        }

        private void GrabberStream(int error, ref StreamSDK.streamsdk_st_buffer buf, IntPtr user_data)
        {
            byte[] buffer = new byte[buf.buf_size];
            unsafe
            {
                byte* dPtr = (byte*)(buf.buf_ptr);
                for (int i = 0; i < buf.buf_size; i++)
                {
                    buffer[i] = dPtr[i];
                }
            }
            try
            {
                Dictionary<string, object> tagValueMap = OWBJson.parse(OWBString.BytesToString(buffer, Encoding.UTF8));
                TagMap = tagValueMap;
            }
            catch
            { }
        }

        private string GetAdminBootID()
        {
            string bootID = string.Empty;
            byte[] buf = new byte[1024];
            byte[] passwordBytes = new byte[0];
            uint length = 0;
            if (!OWBTypes.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_ADMIN_BOOT_ID, ref buf, ref length)))
            {
                return bootID;
            }
            bootID = OWBJson.parse<string>(OWBString.BytesToString(buf, Encoding.UTF8));
            return bootID;
        }

        public byte[] Snapshot()
        {
            string url = URL_OSD_SNAPSHOT;
            byte[] buf = new byte[1024];
            uint length = 0;
            if (OWBTypes.CheckStatus(RestSDK.Get(IP, (ushort)PORT, url, ref buf, ref length)))
            {
                OWBTypes.FileCache fileCache = OWBJson.parse<OWBTypes.FileCache>(OWBString.BytesToString(buf, Encoding.UTF8));
                if (fileCache != null)
                {
                    buf = new byte[1024000];
                    if (OWBTypes.CheckStatus(RestSDK.Get(IP, (ushort)PORT, fileCache.Path, ref buf, ref length)))
                    {
                        byte[] refBuf = new byte[length + HEADERLENGTH];
                        Array.Copy(buf, refBuf, length + HEADERLENGTH);
                        return refBuf;
                    }
                }
            }
            return null;
        }

        public Bitmap CreateBitmap(byte[] buffer)
        {
            using (BinaryReader br = new BinaryReader(new MemoryStream(buffer)))
            {
                try
                {
                    br.BaseStream.Position = 2;
                    int width = (int)IPAddress.HostToNetworkOrder(br.ReadInt16());
                    int height = (int)IPAddress.HostToNetworkOrder(br.ReadInt16());
                    int depth = br.ReadByte();
                    int type = br.ReadByte();
                    uint linesize = br.ReadUInt32();
                    switch (type)
                    {
                        case 1:
                            br.BaseStream.Position = 16;
                            int framesize = height * width * 3 / 2;
                            int imgsize = width * height;
                            byte[] rgb = new byte[3 * imgsize];
                            return ConvertYUV2RGB(br.ReadBytes(framesize), rgb, width, height);
                    }
                }
                catch
                {

                }
            }
            return null;
        }

        static double[,] YUV2RGB_CONVERT_MATRIX = new double[3, 3] { { 1, 0, 1.4022 }, { 1, -0.3456, -0.7145 }, { 1, 1.771, 0 } };
        private Bitmap ConvertYUV2RGB(byte[] yuvFrame, byte[] rgbFrame, int width, int height)
        {
            int uIndex = width * height;
            int vIndex = uIndex + ((width * height) >> 2);
            int gIndex = width * height;
            int bIndex = gIndex * 2;

            int temp = 0;

            Bitmap bm = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {

                    temp = (int)(yuvFrame[y * width + x] + (yuvFrame[vIndex + (y / 2) * (width / 2) + x / 2] - 128) * YUV2RGB_CONVERT_MATRIX[0, 2]);
                    rgbFrame[y * width + x] = (byte)(temp < 0 ? 0 : (temp > 255 ? 255 : temp));

                    temp = (int)(yuvFrame[y * width + x] + (yuvFrame[uIndex + (y / 2) * (width / 2) + x / 2] - 128) * YUV2RGB_CONVERT_MATRIX[1, 1] + (yuvFrame[vIndex + (y / 2) * (width / 2) + x / 2] - 128) * YUV2RGB_CONVERT_MATRIX[1, 2]);
                    rgbFrame[gIndex + y * width + x] = (byte)(temp < 0 ? 0 : (temp > 255 ? 255 : temp));

                    temp = (int)(yuvFrame[y * width + x] + (yuvFrame[uIndex + (y / 2) * (width / 2) + x / 2] - 128) * YUV2RGB_CONVERT_MATRIX[2, 1]);
                    rgbFrame[bIndex + y * width + x] = (byte)(temp < 0 ? 0 : (temp > 255 ? 255 : temp));
                    Color c = Color.FromArgb(rgbFrame[y * width + x], rgbFrame[gIndex + y * width + x], rgbFrame[bIndex + y * width + x]);
                    bm.SetPixel(x, y, c);
                }
            }
            return bm;
        }
        #endregion

        public bool CompensationDistance(float value)
        {
            lock (CameraObject)
            {
                if (_IsSupportCompensationDistance)
                {
                    byte[] data = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)(value * 10)));
                    string dataStr = OWBString.BytesToHexString(data, "x2", string.Empty);

                    SensorCommand sensorCommand = new SensorCommand
                    {
                        CMD0 = "00",
                        CMD1 = "14",
                        Data = dataStr
                    };
                    SensorData sensorData = new SensorData();

                    string objStr = OWBJson.stringify(sensorCommand);
                    byte[] buf = Encoding.Default.GetBytes(objStr);
                    uint length = (uint)buf.Length;
                    int result = RestSDK.Post(IP, PORT, URL_SENSOR_COMMAND, ref buf, ref length);
                    if (result == OWBTypes.OK)
                    {
                        string bufStr = OWBString.BytesToString(buf, Encoding.UTF8);

                        sensorData = OWBJson.parse<SensorData>(bufStr);
                        return OWBString.HexStringToBytes(sensorData.Status, string.Empty)[0] == 0x00;
                    }
                }
                return false;
            }
        }

        public bool GetTempCoreFirmwareVersion(out string versionStr)
        {
            lock (CameraObject)
            {
                byte[] buf = new byte[1024];
                uint length = 0;

                versionStr = string.Empty;
                int result = RestSDK.Get(IP, PORT, URL_SENSOR_INFO, ref buf, ref length);
                if (result == OWBTypes.OK)
                {
                    string bufStr = OWBString.BytesToString(buf, Encoding.UTF8);

                    SensorVersionInfo sensorVersionInfo = OWBJson.parse<SensorVersionInfo>(bufStr);

                    versionStr = sensorVersionInfo.Version;
                }

                return result == OWBTypes.OK;
            }
        }

        private int[] StringToVersion(string versionStr)
        {
            string[] splits = versionStr.Split('.');
            int[] result = new int[splits.Length];

            for (int i = 0; i < splits.Length; i++)
            {
                result[i] = Convert.ToInt32(splits[i]);
            }
            return result;
        }

        private int CompareVersion(int[] version1, int[] version2)
        {
            int i = 0;
            for (; i < version1.Length && i < version2.Length; i++)
            {
                if (version1[i] > version2[i])
                {
                    return 1;
                }
                else if (version1[i] < version2[i])
                {
                    return -1;
                }
            }

            if (i < version1.Length)
            {
                return 1;
            }
            else if (i < version2.Length)
            {
                return -1;
            }

            return 0;
        }
    }
}
