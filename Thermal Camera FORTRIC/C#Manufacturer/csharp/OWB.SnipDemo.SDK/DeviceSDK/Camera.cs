using System;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;

namespace OWB.SnipDemo.SDK
{
    internal class Camera
    {
        public const float KelvinCelsiurZero = 273.15F;
        public const float KelvinCelsiurTwenty = 293.15F;
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
        private const string URL_ISP_INSTRUMENT_OBJECTS_SPOTS = @"/isp/instrument/objects/points";
        private const string URL_ISP_INSTRUMENT_OBJECTS_REGIONS = @"/isp/instrument/objects/regions";
        private const string URL_ISP_INSTRUMENT_OBJECTS_LINES = @"/isp/instrument/objects/lines";
        private const string URL_ISP_INSTRUMENT_OBJECTS_SPOTITEM = @"/isp/instrument/objects/points/{0}";
        private const string URL_ISP_INSTRUMENT_OBJECTS_REGIONITEM = @"/isp/instrument/objects/regions/{0}";
        private const string URL_ISP_INSTRUMENT_OBJECTS_LINEITEM = @"/isp/instrument/objects/lines/{0}";
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

        private const string URL_SENSOR_CALI = "/sensor/cali";

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
        private string _VideoMode;
        private int _CurrentBitrate;

        private string[] _Lens;
        private int _LensIndex;
        private TemperatureRange[] _TRanges;
        private int _TRangeIndex;
        private Dictionary<int, List<int>> _LensTRangesMap;

        private Image _ChannelImage;
        private ushort[,] _RawValues;
        private float[,] _TValues;
        private float[] _LUT;
        private int _From;
        private int _To;

        private float _reflectedTemperature;
        private float _atmosphericTemperature;
        private float _distance;
        private float _emissivity;
        private float _relativeHumidity;
        private float _externalOpticsTemperature;
        private float _externalOpticsTransmission;

        private StreamSDK.streamsdk_cb_grabber _GrabberCallback;
        private StreamSDK.streamsdk_cb_image_grabber _ImageGrabberCallback;
        #endregion

        #region Constructors
        public Camera()
        {
            InitProperties();

            IP = string.Empty;
            UserName = string.Empty;
            Password = string.Empty;

            VideoMode = string.Empty;
            CurrentBitrate = 0;
            GrabberCallback = GrabberRaw;
            ImageGrabberCallback = GrabberH264;
        }
        #endregion

        #region Properties

        public IntPtr HStream
        {
            get { return _HStream; }
            private set { _HStream = value; }
        }

        public IntPtr HRecordStream
        { get; private set; }

        public IntPtr Decoder
        {
            get { return _Decoder; }
            set { _Decoder = value; }
        }

        public IntPtr RecordDecoder { get; private set; }

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

        public string Model { get; set; }

        public string SeriesNo { get; set; }

        private string CameraTag { get; set; }

        public RadiationType RadiationType { get; set; }

        public float Emissivity => _emissivity;

        public float ReflectedTemperature => _reflectedTemperature;

        public float AtmosphericTemperature => _atmosphericTemperature;

        public float Distance => _distance;

        public float RelativeHumidity => _relativeHumidity;

        public float ExternalOpticsTemperature => _externalOpticsTemperature;

        public float ExternalOpticsTransmission => _externalOpticsTransmission;

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

        public string VideoMode
        {
            get { return _VideoMode; }
            set { _VideoMode = value; }
        }

        public int CurrentBitrate
        {
            get { return _CurrentBitrate; }
            set { _CurrentBitrate = value; }
        }

        public string[] Lens
        {
            get { return _Lens; }
            set { _Lens = value; }
        }

        public int LensIndex
        {
            get { return _LensIndex; }
            set
            {
                if (_LensIndex != value)
                {
                    bool found = false;
                    List<int> tRanges = LensTRangesMap[value];
                    for (int i = 0; i < tRanges.Count; i++)
                    {
                        if (tRanges[i] == TRangeIndex)
                        {
                            found = true;
                        }
                    }
                    if (found)
                    {
                        SensorJConfig tmpValue = new SensorJConfig();
                        tmpValue.Selected_Lens = value + 1;
                        tmpValue.Selected_T_Range = TRangeIndex + 1;
                        if (PutSensorJConfig(tmpValue))
                        {
                            _LensIndex = value;
                        }
                    }
                    else
                    {
                        SensorJConfig tmpValue = new SensorJConfig();
                        tmpValue.Selected_Lens = value + 1;
                        tmpValue.Selected_T_Range = tRanges[0] + 1;
                        if (PutSensorJConfig(tmpValue))
                        {
                            _LensIndex = value;
                            _TRangeIndex = tRanges[0];
                        }
                    }
                }
            }
        }

        public TemperatureRange[] TRanges
        {
            get { return _TRanges; }
            set { _TRanges = value; }
        }

        public int TRangeIndex
        {
            get { return _TRangeIndex; }
            set
            {
                if (_TRangeIndex != value)
                {
                    SensorJConfig tmpValue = new SensorJConfig();
                    tmpValue.Selected_Lens = LensIndex + 1;
                    tmpValue.Selected_T_Range = value + 1;
                    if (PutSensorJConfig(tmpValue))
                    {
                        _TRangeIndex = value;
                    }
                }
            }
        }

        private Dictionary<int, List<int>> LensTRangesMap
        {
            get { return _LensTRangesMap; }
            set { _LensTRangesMap = value; }
        }

        private Image ChannelImage
        {
            get { return _ChannelImage; }
            set { _ChannelImage = value; }
        }

        private ushort[,] RawValues
        {
            get { return _RawValues; }
            set { _RawValues = value; }
        }

        public float[,] TValues
        {
            get { return _TValues; }
            set { _TValues = value; }
        }

        public float[] LUT
        {
            get { return _LUT; }
            set { _LUT = value; }
        }


        public ushort[] ADArray { get; set; }

        public float[] TArray { get; set; }

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

        public StreamSDK.streamsdk_cb_image_grabber ImageGrabberCallback
        {
            get { return _ImageGrabberCallback; }
            set { _ImageGrabberCallback = value; }
        }

        public StreamSDK.streamsdk_cb_image_grabber RecordGrabberCallback { get; set; }
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

            VideoMode = StreamSDK.URL_STREAM_VIDEO_RAW;
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
            if (!RequestUtil.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_ADMIN_INFO, ref buf, ref length)))
            {
                return false;
            }
            try
            {
                AdminInfo adminInfo = OWBJson.Parse<AdminInfo>(OWBString.BytesToString(buf, Encoding.UTF8));
                FirmwareVersion = adminInfo.Device_FW.Version.Clone() as int[];
                Model = adminInfo.Device_Model;
                SeriesNo = adminInfo.Device_SN0;
                CameraTag = adminInfo.Device_Tag0;
                RadiationType = GetRadiation();
            }
            catch
            {
                FirmwareVersion = new int[4];
            }

            BootID = GetAdminBootID();
            EncryptPassword = Encoding.UTF8.GetBytes(Password);

            RestSDK.Rest_set_authroization(userName, EncryptPassword);

            if (!RequestUtil.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_SENSOR_DIMENSION, ref buf, ref length)))
            {
                return false;
            }
            try
            {
                SensorDimension sensorDimension = OWBJson.Parse<SensorDimension>(OWBString.BytesToString(buf, Encoding.UTF8));
                Width = sensorDimension.W;
                Height = sensorDimension.H;
                IsConnected = true;
                RefreshCameraInfo();
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

        private void RefreshCameraInfo()
        {
            SensorLens[] sensorLens = GetSensorLens();
            Lens = new string[sensorLens.Length - 1];
            for (int i = 1; i < sensorLens.Length; i++)
            {
                Lens[i - 1] = sensorLens[i].Model;
            }
            SensorT_Range[] sensorT_Ranges = GetSensorT_Range();
            TRanges = new TemperatureRange[sensorT_Ranges.Length - 1];
            for (int i = 1; i < sensorT_Ranges.Length; i++)
            {
                TRanges[i - 1] = new TemperatureRange(sensorT_Ranges[i].Low, sensorT_Ranges[i].High);
                TRanges[i - 1].Tag = i - 1;
            }
            SensorLUT[] sensorLUTs = GetSensorLUTs();
            LensTRangesMap = new Dictionary<int, List<int>>();
            for (int i = 1; i < sensorLUTs.Length; i++)
            {
                int lensIndex = sensorLUTs[i].Lens - 1;
                int temperatureRangeIndex = sensorLUTs[i].T_Range - 1;
                if (!LensTRangesMap.ContainsKey(lensIndex))
                {
                    LensTRangesMap[lensIndex] = new List<int>();
                }

                LensTRangesMap[lensIndex].Add(temperatureRangeIndex);
            }
            SensorJConfig sensorJConfig = GetSensorJConfig();
            LensIndex = sensorJConfig.Selected_Lens - 1;
            TRangeIndex = sensorJConfig.Selected_T_Range - 1;
            UpdateFactoryLUT(out ushort[] adArray, out float[] tArray);
            ADArray = adArray;
            TArray = tArray;
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

        public bool StartStream(DeviceSDKType.LoginType loginType, DeviceSDKType.StreamType streamType)
        {
            if (CreateStream())
            {
                if (loginType == DeviceSDKType.LoginType.RAW)
                {
                    VideoMode = StreamSDK.URL_STREAM_VIDEO_RAW;
                }
                else
                {
                    if (streamType == DeviceSDKType.StreamType.PRI)
                    {
                        VideoMode = StreamSDK.URL_STREAM_VIDEO_PRI;
                    }
                    else
                    {
                        VideoMode = StreamSDK.URL_STREAM_VIDEO_SUB;
                    }
                }
                if (StartStream())
                {
                    IsPlay = true;
                    return true;
                }
            }
            return false;
        }

        private bool StartStream()
        {
            int max_packet_size = 0;

            byte[] buf = new byte[1024];
            uint length = 0;
            switch (VideoMode)
            {
                case StreamSDK.URL_STREAM_VIDEO_RAW:
                    if (!RequestUtil.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_STREAM_VIDEO_RAW, ref buf, ref length)))
                    {
                        return false;
                    }
                    try
                    {
                        StreamVideoRaw streamVideoRaw = OWBJson.Parse<StreamVideoRaw>(OWBString.BytesToString(buf, Encoding.UTF8));
                        max_packet_size = streamVideoRaw.Max_Packet_Size;
                    }
                    catch
                    {
                        return false;
                    }
                    break;
                case StreamSDK.URL_STREAM_VIDEO_PRI:
                    if (!RequestUtil.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_STREAM_VIDEO_PRI, ref buf, ref length)))
                    {
                        return false;
                    }
                    try
                    {
                        StreamVideoPri streamVideoPri = OWBJson.Parse<StreamVideoPri>(OWBString.BytesToString(buf, Encoding.UTF8));
                        max_packet_size = streamVideoPri.Max_Packet_Size;
                        CurrentBitrate = streamVideoPri.Bitrate;
                    }
                    catch
                    {
                        return false;
                    }
                    break;
                case StreamSDK.URL_STREAM_VIDEO_SUB:
                    if (!RequestUtil.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_STREAM_VIDEO_SUB, ref buf, ref length)))
                    {
                        return false;
                    }
                    try
                    {
                        StreamVideoSub streamVideoSub = OWBJson.Parse<StreamVideoSub>(OWBString.BytesToString(buf, Encoding.UTF8));
                        max_packet_size = streamVideoSub.Max_Packet_Size;
                        CurrentBitrate = streamVideoSub.Bitrate;
                    }
                    catch
                    {
                        return false;
                    }
                    break;
            }

            if (StreamSDK.streamsdk_start_stream(HStream, VideoMode, max_packet_size, null, IntPtr.Zero) != StreamSDK.STREAMSDK_EC_OK)
            {
                return false;
            }

            if (VideoMode == StreamSDK.URL_STREAM_VIDEO_RAW)
            {
                StreamSDK.streamsdk_set_stream_grabber(HStream, GrabberCallback, IntPtr.Zero);
            }
            else
            {
                IntPtr dec = IntPtr.Zero;
                StreamSDK.streamsdk_st_decoder_param dp = new StreamSDK.streamsdk_st_decoder_param();
                dp.dec_w = Width;
                dp.dec_h = Height;
                dp.pix_type = (int)StreamSDK.streamsdk_enum_pix_type.STREAMSDK_PIX_BGR;
                StreamSDK.streamsdk_create_h264_decoder(HStream, ref dp, ref dec);
                Decoder = dec;
                StreamSDK.streamsdk_start_h264_decoder(Decoder, ImageGrabberCallback, IntPtr.Zero);
            }
            return true;
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

        public void UpdateFactoryLUT(out ushort[] adArray, out float[] tArray)
        {
            lock (CameraLock)
            {
                adArray = new ushort[1024];
                tArray = new float[1024];
                int index = 0;
                byte[] buf = new byte[1024];
                byte[] passwordBytes = new byte[0];
                uint length = 0;
                if (RequestUtil.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_SENSOR_CURRENT_LUT_INDEX, ref buf, ref length)))
                {
                    index = OWBJson.Parse<int>(OWBString.BytesToString(buf, Encoding.UTF8));
                    SensorLUTItem[] sensorLUTs = null;
                    if (RequestUtil.CheckStatus(RestSDK.Get(IP, (ushort)PORT, string.Format(URL_SENSOR_LUT_TABLE, index), ref buf, ref length)))
                    {
                        sensorLUTs = OWBJson.Parse<SensorLUTItem[]>(OWBString.BytesToString(buf, Encoding.UTF8));
                    }
                    adArray = new ushort[sensorLUTs.Length];
                    tArray = new float[sensorLUTs.Length];
                    LUT = new float[LUT_SIZE];
                    int from = sensorLUTs[0].R;
                    int to = sensorLUTs[0].R;

                    for (int i = 0; i < sensorLUTs.Length; i++)
                    {
                        adArray[i] = sensorLUTs[i].R;
                        tArray[i] = sensorLUTs[i].T + KelvinCelsiurZero;
                    }

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
                    InstrumentJconfig instrumentJconfig = GetInstrumentJconfig();

                    _reflectedTemperature = instrumentJconfig.Reflection_T + KelvinCelsiurZero;
                    _atmosphericTemperature = instrumentJconfig.Atmosphere_T + KelvinCelsiurZero;
                    _distance = instrumentJconfig.Distance;
                    _emissivity = instrumentJconfig.Emission;
                    _relativeHumidity = instrumentJconfig.RH;
                    _externalOpticsTemperature = instrumentJconfig.Lens_T + KelvinCelsiurZero;
                    _externalOpticsTransmission = instrumentJconfig.Lens_Transmission;
                }
            }
        }

        public InstrumentJconfig GetInstrumentJconfig()
        {
            InstrumentJconfig instrumentJconfig = new InstrumentJconfig();
            byte[] buf = new byte[1024];
            uint length = 0;
            try
            {
                if (!RequestUtil.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_ISP_INSTRUMENT_JCONFIG, ref buf, ref length)))
                {
                    return instrumentJconfig;
                }
                instrumentJconfig = OWBJson.Parse<InstrumentJconfig>(OWBString.BytesToString(buf, Encoding.UTF8));
            }
            catch
            { }
            return instrumentJconfig;
        }

        public bool PutInstrumentJconfig(InstrumentJconfig obj)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            string objStr = OWBJson.Stringify(obj);
            byte[] buf = new byte[1024];
            OWBString.StringToBytes(objStr, objStr.Length, Encoding.UTF8).CopyTo(buf, 0);
            uint length = 0;
            if (!RequestUtil.CheckStatus(RestSDK.Put(IP, (ushort)PORT, URL_ISP_INSTRUMENT_JCONFIG, ref buf, ref length)))
            {
                return false;
            }

            RefreshCameraInfo();

            return true;
        }

        public void RefreshStream(DeviceSDKType.LoginType loginType, DeviceSDKType.StreamType streamType)
        {
            if (IsConnected)
            {
                StopStream();
                StartStream(loginType, streamType);
            }
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

        public bool GetRawFrame(out ushort[,] values)
        {
            lock (CameraRawLock)
            {
                values = null;

                if (RawValues != null && RawValues.GetLength(0) == Width && RawValues.GetLength(1) == Height)
                {
                    values = RawValues;
                    return true;
                }
                return false;
            }
        }

        private void GrabberH264(int error, ref StreamSDK.streamsdk_st_image image, IntPtr user_data)
        {
            if (error != StreamSDK.STREAMSDK_EC_OK)
            {
                return;
            }

            if (image.img_h != Height || image.img_w != Width)
            {
                return;
            }

            try
            {
                using (Bitmap bitmap = new Bitmap(Width, Height, PixelFormat.Format24bppRgb))
                {
                    BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
                    int stride = bmpData.Stride;
                    int offset = stride - Width * 3;

                    unsafe
                    {
                        byte* dPtr = (byte*)(bmpData.Scan0);
                        byte* sPtr = (byte*)image.img_ptr;
                        int i = 0;
                        for (int y = 0; y < Height; y++)
                        {
                            for (int x = 0; x < Width; x++)
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
                    ChannelImage = bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), bitmap.PixelFormat);
                }
            }
            catch
            {

            }
        }

        public bool GetH264Frame(out Image image)
        {
            lock (CameraH264Lock)
            {
                image = ChannelImage;
                return true;
            }
        }

        private string GetAdminBootID()
        {
            string bootID = string.Empty;
            byte[] buf = new byte[1024];
            byte[] passwordBytes = new byte[0];
            uint length = 0;
            if (!RequestUtil.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_ADMIN_BOOT_ID, ref buf, ref length)))
            {
                return bootID;
            }
            bootID = OWBJson.Parse<string>(OWBString.BytesToString(buf, Encoding.UTF8));
            return bootID;
        }

        public string[] GetPalettes()
        {
            string[] palettes = null;
            byte[] buf = new byte[1024];
            uint length = 0;
            if (!RequestUtil.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_OSD_TRAY_PRESETS, ref buf, ref length)))
            {
                return palettes;
            }
            try
            {
                string[] paletteDefaults = new string[0];
                string[] paletteCustoms = new string[0];
                paletteDefaults = OWBJson.Parse<string[]>(OWBString.BytesToString(buf, Encoding.UTF8));
                if (RequestUtil.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_ISP_TRAY_CUSTOMS, ref buf, ref length)))
                {
                    paletteCustoms = OWBJson.Parse<string[]>(OWBString.BytesToString(buf, Encoding.UTF8));
                }
                palettes = new string[paletteDefaults.Length + paletteCustoms.Length];
                for (int i = 0; i < paletteDefaults.Length; i++)
                {
                    palettes[i] = paletteDefaults[i];
                }
                for (int i = 0; i < paletteCustoms.Length; i++)
                {
                    palettes[paletteDefaults.Length + i] = paletteCustoms[i];
                }
            }
            catch
            {

            }
            return palettes;
        }

        public string GetCurrentPlt()
        {
            string pltName = string.Empty;
            byte[] buf = new byte[1024];
            uint length = 0;
            if (!RequestUtil.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_OSD_TRAY_PLT, ref buf, ref length)))
            {
                return pltName;
            }
            try
            {
                OsdTrayPlt plt = OWBJson.Parse<OsdTrayPlt>(OWBString.BytesToString(buf, Encoding.UTF8));
                pltName = plt.Name;
            }
            catch
            {

            }
            return pltName;
        }

        public bool PutCurrentPlt(string plt)
        {
            OsdTrayPlt obj = new OsdTrayPlt();
            obj.Name = plt;
            obj.Inverse = false;
            string objStr = OWBJson.Stringify(obj);
            byte[] buf = new byte[1024];
            OWBString.StringToBytes(objStr, objStr.Length, Encoding.UTF8).CopyTo(buf, 0);
            uint length = 0;
            if (!RequestUtil.CheckStatus(RestSDK.Put(IP, (ushort)PORT, URL_OSD_TRAY_PLT, ref buf, ref length)))
            {
                return false;
            }
            return true;
        }

        public void PostCali()
        {
            byte[] buf = new byte[0];
            uint length = 0;
            RestSDK.Post(IP, (ushort)PORT, URL_SENSOR_CALI, ref buf, ref length);
        }

        public void PostNearFocus()
        {
            byte[] buf = new byte[0];
            uint length = 0;
            RestSDK.Post(IP, (ushort)PORT, string.Format(URL_PERI_FOCUS, "near", 10), ref buf, ref length);
        }

        public void PostFarFocus()
        {
            byte[] buf = new byte[0];
            uint length = 0;
            RestSDK.Post(IP, (ushort)PORT, string.Format(URL_PERI_FOCUS, "far", 10), ref buf, ref length);
        }

        public void PostIspAF()
        {
            byte[] buf = new byte[0];
            uint length = 0;
            RestSDK.Post(IP, (ushort)PORT, URL_ISP_AF, ref buf, ref length);
        }

        public SensorLens[] GetSensorLens()
        {
            SensorLens[] sensorLens = null;
            byte[] buf = new byte[1024];
            uint length = 0;
            try
            {
                if (!RequestUtil.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_SENSOR_LENS, ref buf, ref length)))
                {
                    return sensorLens;
                }
                sensorLens = OWBJson.Parse<SensorLens[]>(OWBString.BytesToString(buf, Encoding.UTF8));
            }
            catch
            { }
            return sensorLens;
        }

        public SensorT_Range[] GetSensorT_Range()
        {
            SensorT_Range[] sensorT_Range = null;
            byte[] buf = new byte[1024];
            uint length = 0;
            try
            {
                if (!RequestUtil.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_SENSOR_T_RANGE, ref buf, ref length)))
                {
                    return sensorT_Range;
                }
                sensorT_Range = OWBJson.Parse<SensorT_Range[]>(OWBString.BytesToString(buf, Encoding.UTF8));
            }
            catch
            { }
            return sensorT_Range;
        }

        public SensorLUT[] GetSensorLUTs()
        {
            SensorLUT[] sensorLUTs = null;
            byte[] buf = new byte[1024];
            uint length = 0;
            try
            {
                if (!RequestUtil.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_SENSOR_LUTS, ref buf, ref length)))
                {
                    return sensorLUTs;
                }
                sensorLUTs = OWBJson.Parse<SensorLUT[]>(OWBString.BytesToString(buf, Encoding.UTF8));
            }
            catch
            { }
            return sensorLUTs;
        }

        public SensorJConfig GetSensorJConfig()
        {
            SensorJConfig sensorJConfig = null;
            byte[] buf = new byte[1024];
            uint length = 0;
            try
            {
                if (!RequestUtil.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_SENSOR_J_CONFIG, ref buf, ref length)))
                {
                    return sensorJConfig;
                }
                sensorJConfig = OWBJson.Parse<SensorJConfig>(OWBString.BytesToString(buf, Encoding.UTF8));
            }
            catch
            { }
            return sensorJConfig;
        }

        public bool PutSensorJConfig(SensorJConfig sensorJConfig)
        {
            string objStr = OWBJson.Stringify(sensorJConfig);
            byte[] buf = new byte[1024];
            OWBString.StringToBytes(objStr, objStr.Length, Encoding.UTF8).CopyTo(buf, 0);
            uint length = 0;
            if (!RequestUtil.CheckStatus(RestSDK.Put(IP, (ushort)PORT, URL_SENSOR_J_CONFIG, ref buf, ref length)))
            {
                return false;
            }
            return true;
        }

        public ushort[,] Snip()
        {
            byte[] buf = new byte[1024];
            uint length = 0;
            if (RequestUtil.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_ISP_SNAPSHOT, ref buf, ref length)))
            {
                FileCache fileCache = OWBJson.Parse<FileCache>(OWBString.BytesToString(buf, Encoding.UTF8));
                buf = new byte[1024000];
                if (RequestUtil.CheckStatus(RestSDK.Get(IP, (ushort)PORT, fileCache.Path, ref buf, ref length)))
                {
                    ushort[] values = new ushort[(length - 16) / 2] ;

                    for (int i = 16; i < length - 1; i += 2)
                    {
                        ushort u16 = (ushort)((buf[i + 1] << 8) + buf[i]);
                        values[(i - 16) / 2] = u16;
                    }

                    ushort[,] rawValues = new ushort[Width, Height];
                    for (int y = 0; y < Height; y++)
                    {
                        for (int x = 0; x < Width; x++)
                        {
                            rawValues[x, y] = values[x + y * Width];
                        }
                    }

                    return rawValues;
                }
            }
            return null;
        }

        public Image ToImage(string paletteKey, ushort[,] ADValues)
        {
            if (ADValues == null)
            {
                return null;
            }

            int width = ADValues.GetLength(0);
            int height = ADValues.GetLength(1);
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            int stride = bmpData.Stride;
            int offset = stride - width * 3;
            unsafe
            {
                byte* dPtr = (byte*)(bmpData.Scan0);

                ushort min = ushort.MaxValue;
                ushort max = ushort.MinValue;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (ADValues[x, y] > max)
                        {
                            max = ADValues[x, y];
                        }
                        if (ADValues[x, y] < min)
                        {
                            min = ADValues[x, y];
                        }
                    }
                }
                int span = max - min;
                if (span < 1)
                {
                    span = 1;
                }

                if (!Constants.Palettes.ContainsKey(paletteKey))
                {
                    return null;
                }
                Color[] palette = Constants.Palettes[paletteKey];
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        int diff = ADValues[x, y] - min;
                        if (diff < 0)
                        {
                            diff = 0;
                        }
                        if (diff > span)
                        {
                            diff = span;
                        }
                        byte index = Convert.ToByte(diff * (palette.Length - 1) / span);

                        if (index < 0)
                        {
                            index = 0;
                        }
                        if (index > palette.Length - 1)
                        {
                            index = Convert.ToByte(palette.Length - 1);
                        }

                        dPtr[0] = palette[index].B;
                        dPtr[1] = palette[index].G;
                        dPtr[2] = palette[index].R;
                        dPtr += 3;
                    }
                    dPtr += offset;
                }
            }
            bitmap.UnlockBits(bmpData);
            return bitmap;
        }

        public List<Marker> GetMarkerItems()
        {
            List<Marker> markerList = new List<Marker>();
            byte[] buf = new byte[1024];
            uint length = 0;
            if (RequestUtil.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_ISP_INSTRUMENT_OBJECTS_SPOTS, ref buf, ref length)))
            {
                try
                {
                    string[] tempLabels = OWBJson.Parse<string[]>(OWBString.BytesToString(buf, Encoding.UTF8));
                    for (int i = 0; i < tempLabels?.Length; i++)
                    {
                        SpotMarker spotMarker = GetPoint(tempLabels[i]);
                        markerList.Add(spotMarker);
                    }
                }
                catch { }
            }
            if (RequestUtil.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_ISP_INSTRUMENT_OBJECTS_REGIONS, ref buf, ref length)))
            {
                try
                {
                    string[] tempLabels = OWBJson.Parse<string[]>(OWBString.BytesToString(buf, Encoding.UTF8));
                    for (int i = 0; i < tempLabels?.Length; i++)
                    {
                        PolygonMarker regionMarker = GetRegion(tempLabels[i]);
                        markerList.Add(regionMarker);
                    }
                }
                catch { }
            }
            if (RequestUtil.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_ISP_INSTRUMENT_OBJECTS_LINES, ref buf, ref length)))
            {
                try
                {
                    string[] tempLabels = OWBJson.Parse<string[]>(OWBString.BytesToString(buf, Encoding.UTF8));
                    for (int i = 0; i < tempLabels?.Length; i++)
                    {
                        LineMarker lineMarker = GetLine(tempLabels[i]);
                        markerList.Add(lineMarker);
                    }
                }
                catch { }
            }
            return markerList;
        }

        public SpotMarker GetPoint(string markerName)
        {
            SpotMarker pointMarker = null;
            byte[] buf = new byte[1024];
            uint length = 0;
            try
            {
                if (!RequestUtil.CheckStatus(RestSDK.Get(IP, (ushort)PORT, string.Format(URL_ISP_INSTRUMENT_OBJECTS_SPOTITEM, markerName), ref buf, ref length)))
                {
                    return pointMarker;
                }
                pointMarker = OWBJson.Deserialize<SpotMarker>(OWBString.BytesToString(buf, Encoding.UTF8));
            }
            catch
            { }
            return pointMarker;
        }

        public LineMarker GetLine(string markerName)
        {
            LineMarker lineMarker = null;
            byte[] buf = new byte[1024];
            uint length = 0;
            try
            {
                if (!RequestUtil.CheckStatus(RestSDK.Get(IP, (ushort)PORT, string.Format(URL_ISP_INSTRUMENT_OBJECTS_LINEITEM, markerName), ref buf, ref length)))
                {
                    return lineMarker;
                }
                lineMarker = OWBJson.Deserialize<LineMarker>(OWBString.BytesToString(buf, Encoding.UTF8));
            }
            catch
            { }
            return lineMarker;
        }

        public PolygonMarker GetRegion(string markerName)
        {
            PolygonMarker regionMarker = null;
            byte[] buf = new byte[1024];
            uint length = 0;
            try
            {
                if (!RequestUtil.CheckStatus(RestSDK.Get(IP, (ushort)PORT, string.Format(URL_ISP_INSTRUMENT_OBJECTS_REGIONITEM, markerName), ref buf, ref length)))
                {
                    return regionMarker;
                }
                regionMarker = OWBJson.Deserialize<PolygonMarker>(OWBString.BytesToString(buf, Encoding.UTF8));
            }
            catch
            { }
            return regionMarker;
        }

        #endregion

        #region private methods

        private RadiationType GetRadiation()
        {
            RadiationType result = RadiationType.SB;
            char value = GetTag(CameraTag, 36);
            if (value == '1')
            {
                result = RadiationType.SH;
            }
            return result;
        }

        private char GetTag(string s, int index)
        {
            if (s != null && s.Length > index)
            {
                return s[index];
            }
            return 'n';
        }

        #endregion
    }

    public struct TemperatureRange
    {
        public const double Epsilon = 1E-6;
        #region Fields
        public float RangeMax;
        public float RangeMin;
        public object Tag;
        public static TemperatureRange Default = new TemperatureRange(-20.0F, 150F);
        #endregion

        #region Constructors
        public TemperatureRange(float rangeMin, float rangeMax)
        {
            RangeMin = rangeMin;
            RangeMax = rangeMax;
            Tag = null;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return RangeMin.ToString() + "~" + RangeMax.ToString();
        }

        public static bool operator ==(TemperatureRange t1, TemperatureRange t2)
        {
            return Math.Abs(t1.RangeMin - t2.RangeMin) < Epsilon && Math.Abs(t1.RangeMax - t2.RangeMax) < Epsilon;
        }

        public static bool operator !=(TemperatureRange t1, TemperatureRange t2)
        {
            return !(t1 == t2);
        }

        public override bool Equals(object obj)
        {
            if (obj is TemperatureRange)
            {
                return this == (TemperatureRange)obj;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return RangeMin.GetHashCode() + RangeMax.GetHashCode();
        }
        #endregion
    }
}
