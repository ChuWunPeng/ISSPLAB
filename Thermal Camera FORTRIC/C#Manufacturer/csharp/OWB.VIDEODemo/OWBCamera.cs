using SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OWB.VIDEODemo
{
    public class OWBCamera
    {
        #region Const
        public static int HEADERLENGTH = 16;
        public const int LUT_SIZE = 65536;
        public const float ZERO_T = 273.15F;
        private const int PORT = 10080;
        private const string URL_ADMIN_INFO = "/admin/info";
        private const string URL_ADMIN_REBOOT = "/admin/reboot";
        private const string URL_SENSOR_DIMENSION = "/sensor/dimension";
        private const string URL_STREAM_VIDEO_RAW = "/stream/video/raw";
        private const string URL_STREAM_VIDEO_PRI = "/stream/video/pri";
        private const string URL_STREAM_VIDEO_SUB = "/stream/video/sub";
        private const string URL_OSD_TRAY_PRESETS = "/isp/t-ray/presets";
        private const string URL_CAPTURE_MODES = "/capture/modes";
        private const string URL_CAPTURE_MODE = "/capture/mode";
        private const string URL_CAPTURE = "/capture/{0}";
        private const string URL_ISP_TRAY_CUSTOMS = "/isp/t-ray/customs";
        private const string URL_OSD_TRAY_PLT = "/isp/t-ray/plt";
        private const string URL_OSD_LAYOUT_CBAR = "/osd/layout/cbar";
        private const string URL_OSD_LAYOUT_TITLE = "/osd/layout/title";
        private const string URL_OSD_LAYOUT_TIME = "/osd/layout/time";
        private const string URL_OSD_LAYOUT_UNIT = "/osd/layout/unit";
        private const string URL_OSD_LAYOUT_EMISSIVITY = "/osd/layout/emissivity";
        private const string URL_ISP_T = @"/isp/t?x={0}&y={1}";
        private const string URL_ISP_SNAPSHOT = "/isp/snapshot";
        private const string URL_T_SNAPSHOT = "/isp/t-snapshot";
        private const string URL_OSD_SNAPSHOT = "/osd/snapshot";
        private const string URL_ISP_AF = "/isp/af";
        private const string URL_PERI = "/peri/ptz/af";
        private const string URL_ISP_INSTRUMENT_OBJECTS_REGIONS = "/isp/instrument/objects/regions/globalRegion";
        private const string URL_ISP_INSTRUMENT_OBJECTS_REGION = "/isp/instrument/objects/regions/{0}";
        private const string URL_ISP_INSTRUMENT_JCONFIG = "/isp/instrument/jconfig";
        private const string URL_ADMIN_USERS = "/admin/users";
        private const string URL_ADMIN_USER_OPERATE = "/user/{0}";
        private const string URL_ADMIN_BOOT_ID = "/admin/boot-id";
        private const string URL_PERI_FOCUS = @"/peri/focus?op={0}&step={1}";
        private const string URL_SENSOR_LENS = "/sensor/lens";
        private const string URL_SENSOR_T_RANGE = "/sensor/t-range";
        private const string URL_SENSOR_J_CONFIG = "/sensor/jconfig";
        private const string URL_SENSOR_LUTS = @"/sensor/luts";
        private const string URL_SENSOR_CURRENT_LUT_INDEX = @"/sensor/lut";
        private const string URL_SENSOR_LUT_TABLE = @"/sensor/luts/{0}?list";

        protected object CameraLock = new object();
        protected object CameraRawLock = new object();
        protected object CameraH264Lock = new object();
        #endregion

        #region Fields
        private IntPtr _HStream;
        private IntPtr _Decoder;

        private bool _IsConnected;
        private bool _IsPlay;

        private string _IP;
        private string _UserName;
        private string _Password;

        private byte[] _EncryptPassword;

        private string _BootID;

        private int[] _FirmwareVersion;
        private int _Width;
        private int _Height;
        private int _CurrentBitrate;

        private ushort[,] _RawValues;
        private float[,] _TValues;
        private float[] _LUT;
        private int _From;
        private int _To;

        private StreamSDK.streamsdk_cb_grabber _GrabberCallback;
        #endregion

        #region Constructors
        public OWBCamera()
        {
            InitProperties();

            IP = string.Empty;
            UserName = string.Empty;
            Password = string.Empty;
            CurrentBitrate = 0;
            GrabberCallback = GrabberRaw;
        }
        #endregion

        #region Properties

        public IntPtr HStream
        {
            get { return _HStream; }
            private set { _HStream = value; }
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

        public bool IsPlay
        {
            get { return _IsPlay; }
            private set { _IsPlay = value; }
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

        public int CurrentBitrate
        {
            get { return _CurrentBitrate; }
            set { _CurrentBitrate = value; }
        }

        public ushort[,] RawValues
        {
            get { return _RawValues; }
            set { _RawValues = value; }
        }

        public float[,] TValues
        {
            get { return _TValues; }
            set { _TValues = value; }
        }

        private float[] LUT
        {
            get { return _LUT; }
            set { _LUT = value; }
        }


        private int From
        {
            get { return _From; }
            set { _From = value; }
        }

        private int To
        {
            get { return _To; }
            set { _To = value; }
        }

        private StreamSDK.streamsdk_cb_grabber GrabberCallback
        {
            get { return _GrabberCallback; }
            set { _GrabberCallback = value; }
        }
        #endregion

        #region Methods

        private void InitProperties()
        {
            HStream = IntPtr.Zero;
            Decoder = IntPtr.Zero;

            IsConnected = false;
            IsPlay = false;

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
            EncryptPassword = Encoding.UTF8.GetBytes(Password);

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
                IsConnected = true;
                UpdateFactoryLUT();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void LogoutCamera()
        {
            StopStream();
            IsConnected = false;
        }

        private bool CreateStream()
        {
            StreamSDK.streamsdk_set_thread_pool_size(4);
            IntPtr hStream = IntPtr.Zero;
            if (StreamSDK.streamsdk_create_stream(IP, (ushort)(PORT + 1), ref hStream) != StreamSDK.STREAMSDK_EC_OK)
            {
                return false;
            }
            HStream = hStream;
            return true;
        }

        public bool StartStream()
        {
            if(CreateStream())
            {
                int max_packet_size = 0;

                byte[] buf = new byte[1024];
                uint length = 0;
                if (!OWBTypes.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_STREAM_VIDEO_RAW, ref buf, ref length)))
                {
                    return false;
                }
                try
                {
                    OWBTypes.StreamVideoRaw streamVideoRaw = OWBJson.parse<OWBTypes.StreamVideoRaw>(OWBString.BytesToString(buf, Encoding.UTF8));
                    max_packet_size = streamVideoRaw.Max_Packet_Size;
                }
                catch
                {
                    return false;
                }


                if (StreamSDK.streamsdk_start_stream(HStream, StreamSDK.URL_STREAM_VIDEO_RAW, max_packet_size, null, IntPtr.Zero) != StreamSDK.STREAMSDK_EC_OK)
                {
                    return false;
                }
                StreamSDK.streamsdk_set_stream_grabber(HStream, GrabberCallback, IntPtr.Zero);

                return true;
            }
            return false;
        }

        public void StopStream()
        {
            if (Decoder != IntPtr.Zero)
            {
                StreamSDK.streamsdk_stop_h264_decoder(Decoder);
                StreamSDK.streamsdk_destroy_h264_decoder(Decoder);
                Decoder = IntPtr.Zero;
            }
            if (HStream != IntPtr.Zero)
            {
                StreamSDK.streamsdk_stop_stream(HStream);
                StreamSDK.streamsdk_destroy_stream(HStream);
                HStream = IntPtr.Zero;
            }
            IsPlay = false;
        }

        public void UpdateFactoryLUT()
        {
            lock (CameraLock)
            {
                int index = 0;
                byte[] buf = new byte[1024];
                byte[] passwordBytes = new byte[0];
                uint length = 0;
                if (OWBTypes.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_SENSOR_CURRENT_LUT_INDEX, ref buf, ref length)))
                {
                    index = OWBJson.parse<int>(OWBString.BytesToString(buf, Encoding.UTF8));
                    OWBTypes.SensorLUTItem[] sensorLUTs = null;
                    if (OWBTypes.CheckStatus(RestSDK.Get(IP, (ushort)PORT, string.Format(URL_SENSOR_LUT_TABLE, index), ref buf, ref length)))
                    {
                        sensorLUTs = OWBJson.parse<OWBTypes.SensorLUTItem[]>(OWBString.BytesToString(buf, Encoding.UTF8));
                    }
                    LUT = new float[LUT_SIZE];
                    int from = sensorLUTs[0].R;
                    int to = sensorLUTs[0].R;
                    for (int i = 0; i < sensorLUTs.Length - 1 && sensorLUTs[i].R < sensorLUTs[i + 1].R; i++)
                    {
                        int tmpFrom = sensorLUTs[i].R;
                        int tmpTo = sensorLUTs[i + 1].R;
                        to = tmpTo;
                        float fromTemp = sensorLUTs[i].T;
                        float toTemp = sensorLUTs[i + 1].T;
                        for (int j = tmpFrom; j <= tmpTo; j++)
                        {
                            LUT[j] = fromTemp + (toTemp - fromTemp) * (j - tmpFrom) / (tmpTo - tmpFrom);
                        }
                    }

                    for (int i = 0; i < from; i++)
                    {
                        LUT[i] = LUT[from];
                    }
                    for (int i = to + 1; i < LUT_SIZE; i++)
                    {
                        LUT[i] = LUT[to];
                    }
                    From = from;
                    To = to;
                }
            }
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

        private void GrabberRaw(int error, ref StreamSDK.streamsdk_st_buffer buffer, IntPtr user_data)
        {
            if (error != StreamSDK.STREAMSDK_EC_OK)
            {
                RawValues = null;
                return;
            }

            int width = Width;
            int height = Height;
            if (buffer.buf_size != 2 * width * height)
            {
                return;
            }

            try
            {
                ushort[,] values = new ushort[width, height];
                unsafe
                {
                    ushort* sPtr = (ushort*)(buffer.buf_ptr);
                    int i = 0;
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            values[x, y] = sPtr[i++];
                        }
                    }
                }
                RawValues = values;
            }
            catch { }
        }

        public bool GetTemperatureFrame(out float[,] values)
        {
            lock (CameraRawLock)
            {
                values = null;

                if (RawValues != null && RawValues.GetLength(0) == Width && RawValues.GetLength(1) == Height)
                {
                    values = new float[Width, Height];
                    for (int i = 0; i < Width; i++)
                    {
                        for (int j = 0; j < Height; j++)
                        {
                            if (RawValues[i, j] < From)
                            {
                                values[i, j] = LUT[From];
                            }
                            else if (RawValues[i, j] > To)
                            {
                                values[i, j] = LUT[To];
                            }
                            else
                            {
                                values[i, j] = LUT[RawValues[i, j]];
                            }
                        }
                    }

                    return true;
                }
                return false;
            }
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
