using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDK;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Security.Cryptography;

namespace OWB.TAGDemo
{
    public class OWBCamera
    {
        #region Const
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
        #endregion

        #region Fields
        private IntPtr _HCb;

        private bool _IsConnected;

        private string _IP;
        private string _UserName;
        private string _Password;

        private byte[] _EncryptPassword;

        private string _BootID;

        private int[] _FirmwareVersion;
        private int _Width;
        private int _Height;
        
        private Dictionary<string, object> _TagMap;
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
            StreamGrabberCallback = GrabberStream;
        }
        #endregion

        #region Properties

        public IntPtr HCb
        {
            get { return _HCb; }
            set { _HCb = value; }
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

        public Dictionary<string, object> TagMap
        {
            get { return _TagMap; }
            set { _TagMap = value; }
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
            HCb = IntPtr.Zero;

            IsConnected = false;

            BootID = string.Empty;

            Width = 80;
            Height = 80;
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
            byte[] buf = new byte[1024];
            uint length = 0;
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
            if (StreamSDK.streamsdk_start_stream(HCb, URL_TAG, max_packet_size, null, IntPtr.Zero) != StreamSDK.STREAMSDK_EC_OK)
            {
                return false;
            }
            StreamSDK.streamsdk_set_stream_grabber(HCb, StreamGrabberCallback, IntPtr.Zero);
            return true;
        }

        private void StopStream()
        {
            if (HCb != IntPtr.Zero)
            {
                StreamSDK.streamsdk_stop_stream(HCb);
                StreamSDK.streamsdk_destroy_stream(HCb);
                HCb = IntPtr.Zero;
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
        #endregion
    }
}
