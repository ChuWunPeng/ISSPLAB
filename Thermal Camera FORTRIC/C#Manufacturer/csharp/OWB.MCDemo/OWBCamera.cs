using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using SDK;
using System.Security.Cryptography;

namespace OWB.MCDemo
{
    public class OWBCamera
    {
        #region Const
        public static int HEADERLENGTH = 16;
        private const int PORT = 10080;
        private const string URL_ADMIN_INFO = "/admin/info";
        private const string URL_ADMIN_REBOOT = "/admin/reboot";
        private const string URL_SENSOR_DIMENSION = "/sensor/dimension";
        private const string URL_STREAM_VIDEO_RAW = "/stream/video/raw";
        private const string URL_STREAM_VIDEO_PRI = "/stream/video/pri";
        private const string URL_STREAM_VIDEO_SUB = "/stream/video/sub";
        private const string URL_OSD_TRAY_PRESETS = "/isp/t-ray/presets";
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
        private const string URL_ISP_INSTRUMENT_OBJECTS_REGIONS = "/isp/instrument/objects/regions/globalRegion";
        private const string URL_ADMIN_USERS = "/admin/users";
        private const string URL_ADMIN_USER_OPERATE = "/user/{0}";
        private const string URL_ADMIN_BOOT_ID = "/admin/boot-id";

        private const string URL_ISP_AGCMODE = "/isp/agc-mode";
        private const string URL_SENSOR_CALI = "/sensor/cali";
        private const string URL_SENSOR_CALIMODE = "/sensor/cali-mode";
        private const string URL_SENSOR_CALIMODES = "/sensor/cali-modes";
        private const string URL_ISP_JCONFIG = "/isp/jconfig";

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

        private Image _ChannelImage;
        private ushort[,] _RawValues;
        private float[,] _TValues;

        private StreamSDK.streamsdk_cb_grabber _GrabberCallback;
        private StreamSDK.streamsdk_cb_image_grabber _ImageGrabberCallback;
        #endregion

        #region Constructors
        public OWBCamera()
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

        public Image ChannelImage
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
                PutManualCaliMode("manual");
                PutPltVisible(false);
                PutMaxMinVisible(false);
                PutEmissivityVisible(false);
                PutTimeVisible(false);
                PutUnitVisible(false);
                IsConnected = true;
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

        public bool StartStream(LoginType loginType, StreamType streamType)
        {
            if (CreateStream())
            {
                if (loginType == LoginType.RAW)
                {
                    VideoMode = StreamSDK.URL_STREAM_VIDEO_RAW;
                }
                else
                {
                    if (streamType == StreamType.PRI)
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
                    break;
                case StreamSDK.URL_STREAM_VIDEO_PRI:
                    if (!OWBTypes.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_STREAM_VIDEO_PRI, ref buf, ref length)))
                    {
                        return false;
                    }
                    try
                    {
                        OWBTypes.StreamVideoPri streamVideoPri = OWBJson.parse<OWBTypes.StreamVideoPri>(OWBString.BytesToString(buf, Encoding.UTF8));
                        max_packet_size = streamVideoPri.Max_Packet_Size;
                        CurrentBitrate = streamVideoPri.Bitrate;
                    }
                    catch
                    {
                        return false;
                    }
                    break;
                case StreamSDK.URL_STREAM_VIDEO_SUB:
                    if (!OWBTypes.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_STREAM_VIDEO_SUB, ref buf, ref length)))
                    {
                        return false;
                    }
                    try
                    {
                        OWBTypes.StreamVideoSub streamVideoSub = OWBJson.parse<OWBTypes.StreamVideoSub>(OWBString.BytesToString(buf, Encoding.UTF8));
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

        public void RefreshStream(LoginType loginType, StreamType streamType)
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

        public bool PutStreamVideoPri(int value)
        {
            OWBTypes.StreamVideoPri_BitRate obj = new OWBTypes.StreamVideoPri_BitRate();
            obj.BitRate = value;
            string objStr = OWBJson.stringify(obj);
            byte[] buf = new byte[1024];
            OWBString.StringToBytes(objStr, objStr.Length, Encoding.UTF8).CopyTo(buf, 0);
            uint length = 0;
            if (!OWBTypes.CheckStatus(RestSDK.Put(IP, (ushort)PORT, URL_STREAM_VIDEO_PRI, ref buf, ref length)))
            {
                return false;
            }
            return true;
        }

        public bool PutStreamVideoSub(int value)
        {
            OWBTypes.StreamVideoSub_BitRate obj = new OWBTypes.StreamVideoSub_BitRate();
            obj.BitRate = value;
            string objStr = OWBJson.stringify(obj);
            byte[] buf = new byte[1024];
            OWBString.StringToBytes(objStr, objStr.Length, Encoding.UTF8).CopyTo(buf, 0);
            uint length = 0;
            if (!OWBTypes.CheckStatus(RestSDK.Put(IP, (ushort)PORT, URL_STREAM_VIDEO_SUB, ref buf, ref length)))
            {
                return false;
            }
            return true;
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

        public bool PostReBoot()
        {
            bool flag = false;
            byte[] buf = new byte[1024];
            byte[] passwordBytes = new byte[0];
            uint length = 0;
            if (OWBTypes.CheckStatus(RestSDK.Post(IP, (ushort)PORT, URL_ADMIN_REBOOT, ref buf, ref length)))
            {
                flag = true;
            }
            flag = false;
            BootID = GetAdminBootID();
            return flag;
        }

        public Dictionary<string, object> GetUserList()
        {
            Dictionary<string, object> userList = new Dictionary<string, object>();
            byte[] buf = new byte[1024];
            uint length = 0;
            if (!OWBTypes.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_ADMIN_USERS, ref buf, ref length)))
            {
                return userList;
            }
            try
            {
                userList = OWBJson.parse(OWBString.BytesToString(buf, Encoding.UTF8));
            }
            catch
            {

            }
            return userList;
        }

        public bool UpdateUser(string userName, string password, string userGroup)
        {
            OWBTypes.UserProperty obj = new OWBTypes.UserProperty();
            obj.UserGroup = userGroup;
            obj.Password = Convert.ToBase64String(Encoding.UTF8.GetBytes(OWBString.GenerateMD5(password)));
            string objStr = OWBJson.stringify(obj);
            byte[] buf = new byte[1024];
            OWBString.StringToBytes(objStr, objStr.Length, Encoding.UTF8).CopyTo(buf, 0);
            uint length = 0;
            if (!OWBTypes.CheckStatus(RestSDK.Put(IP, (ushort)PORT, string.Format(URL_ADMIN_USER_OPERATE, userName), ref buf, ref length)))
            {
                return false;
            }
            return true;
        }

        public bool DeleteUser(string userName)
        {
            byte[] buf = new byte[1024];
            uint length = 0;
            if (!OWBTypes.CheckStatus(RestSDK.Delete(IP, (ushort)PORT, string.Format(URL_ADMIN_USER_OPERATE, userName), ref buf, ref length)))
            {
                return false;
            }
            return true;
        }

        public string[] GetPalettes()
        {
            string[] palettes = null;
            byte[] buf = new byte[1024];
            uint length = 0;
            if (!OWBTypes.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_OSD_TRAY_PRESETS, ref buf, ref length)))
            {
                return palettes;
            }
            try
            {
                palettes = OWBJson.parse<string[]>(OWBString.BytesToString(buf, Encoding.UTF8));
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
            if (!OWBTypes.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_OSD_TRAY_PLT, ref buf, ref length)))
            {
                return pltName;
            }
            try
            {
                OWBTypes.OsdTrayPlt plt = OWBJson.parse<OWBTypes.OsdTrayPlt>(OWBString.BytesToString(buf, Encoding.UTF8));
                pltName = plt.Name;
            }
            catch
            {

            }
            return pltName;
        }

        public bool PutCurrentPlt(string plt)
        {
            OWBTypes.OsdTrayPlt obj = new OWBTypes.OsdTrayPlt();
            obj.Name = plt;
            obj.Inverse = false;
            string objStr = OWBJson.stringify(obj);
            byte[] buf = new byte[1024];
            OWBString.StringToBytes(objStr, objStr.Length, Encoding.UTF8).CopyTo(buf, 0);
            uint length = 0;
            if (!OWBTypes.CheckStatus(RestSDK.Put(IP, (ushort)PORT, URL_OSD_TRAY_PLT, ref buf, ref length)))
            {
                return false;
            }
            return true;
        }

        public bool GetPltVisible()
        {
            bool visible = false;
            byte[] buf = new byte[1024];
            uint length = 0;
            if (!OWBTypes.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_OSD_LAYOUT_CBAR, ref buf, ref length)))
            {
                return visible;
            }
            try
            {
                OWBTypes.OSDProperty osdCbar = OWBJson.parse<OWBTypes.OSDProperty>(OWBString.BytesToString(buf, Encoding.UTF8));
                visible = osdCbar.visible;
            }
            catch
            {

            }
            return visible;
        }

        public bool PutPltVisible(bool visible)
        {
            byte[] buf = new byte[1024];
            uint length = 0;
            if (OWBTypes.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_OSD_LAYOUT_CBAR, ref buf, ref length)))
            {
                OWBTypes.OSDProperty osdCbar = OWBJson.parse<OWBTypes.OSDProperty>(OWBString.BytesToString(buf, Encoding.UTF8));
                osdCbar.visible = visible;
                string objStr = OWBJson.stringify(osdCbar);
                OWBString.StringToBytes(objStr, objStr.Length, Encoding.UTF8).CopyTo(buf, 0);
                if (OWBTypes.CheckStatus(RestSDK.Put(IP, (ushort)PORT, URL_OSD_LAYOUT_CBAR, ref buf, ref length)))
                {
                    return true;
                }
            }
            return false;
        }

        public bool GetTitleVisible()
        {
            bool visible = false;
            byte[] buf = new byte[1024];
            uint length = 0;
            if (!OWBTypes.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_OSD_LAYOUT_TITLE, ref buf, ref length)))
            {
                return visible;
            }
            try
            {
                OWBTypes.OSDProperty osdTitle = OWBJson.parse<OWBTypes.OSDProperty>(OWBString.BytesToString(buf, Encoding.UTF8));
                visible = osdTitle.visible;
            }
            catch
            {

            }
            return visible;
        }

        public bool PutTitleVisible(bool visible)
        {
            byte[] buf = new byte[1024];
            uint length = 0;
            if (OWBTypes.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_OSD_LAYOUT_TITLE, ref buf, ref length)))
            {
                OWBTypes.OSDProperty osdTitle = OWBJson.parse<OWBTypes.OSDProperty>(OWBString.BytesToString(buf, Encoding.UTF8));
                osdTitle.visible = visible;
                string objStr = OWBJson.stringify(osdTitle);
                OWBString.StringToBytes(objStr, objStr.Length, Encoding.UTF8).CopyTo(buf, 0);
                if (OWBTypes.CheckStatus(RestSDK.Put(IP, (ushort)PORT, URL_OSD_LAYOUT_TITLE, ref buf, ref length)))
                {
                    return true;
                }
            }
            return false;
        }

        public bool GetTimeVisible()
        {
            bool visible = false;
            byte[] buf = new byte[1024];
            uint length = 0;
            if (!OWBTypes.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_OSD_LAYOUT_TIME, ref buf, ref length)))
            {
                return visible;
            }
            try
            {
                OWBTypes.OSDProperty osdTime = OWBJson.parse<OWBTypes.OSDProperty>(OWBString.BytesToString(buf, Encoding.UTF8));
                visible = osdTime.visible;
            }
            catch
            {

            }
            return visible;
        }

        public bool PutTimeVisible(bool visible)
        {
            byte[] buf = new byte[1024];
            uint length = 0;
            if (OWBTypes.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_OSD_LAYOUT_TIME, ref buf, ref length)))
            {
                OWBTypes.OSDProperty osdTime = OWBJson.parse<OWBTypes.OSDProperty>(OWBString.BytesToString(buf, Encoding.UTF8));
                osdTime.visible = visible;
                string objStr = OWBJson.stringify(osdTime);
                OWBString.StringToBytes(objStr, objStr.Length, Encoding.UTF8).CopyTo(buf, 0);
                if (OWBTypes.CheckStatus(RestSDK.Put(IP, (ushort)PORT, URL_OSD_LAYOUT_TIME, ref buf, ref length)))
                {
                    return true;
                }
            }
            return false;
        }

        public bool GetUnitVisible()
        {
            bool visible = false;
            byte[] buf = new byte[1024];
            uint length = 0;
            if (!OWBTypes.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_OSD_LAYOUT_UNIT, ref buf, ref length)))
            {
                return visible;
            }
            try
            {
                OWBTypes.OSDProperty osdUnit = OWBJson.parse<OWBTypes.OSDProperty>(OWBString.BytesToString(buf, Encoding.UTF8));
                visible = osdUnit.visible;
            }
            catch
            {

            }
            return visible;
        }

        public bool PutUnitVisible(bool visible)
        {
            byte[] buf = new byte[1024];
            uint length = 0;
            if (OWBTypes.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_OSD_LAYOUT_UNIT, ref buf, ref length)))
            {
                OWBTypes.OSDProperty osdUnit = OWBJson.parse<OWBTypes.OSDProperty>(OWBString.BytesToString(buf, Encoding.UTF8));
                osdUnit.visible = visible;
                string objStr = OWBJson.stringify(osdUnit);
                OWBString.StringToBytes(objStr, objStr.Length, Encoding.UTF8).CopyTo(buf, 0);
                if (OWBTypes.CheckStatus(RestSDK.Put(IP, (ushort)PORT, URL_OSD_LAYOUT_UNIT, ref buf, ref length)))
                {
                    return true;
                }
            }
            return false;
        }

        public bool GetEmissivityVisible()
        {
            bool visible = false;
            byte[] buf = new byte[1024];
            uint length = 0;
            if (!OWBTypes.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_OSD_LAYOUT_EMISSIVITY, ref buf, ref length)))
            {
                return visible;
            }
            try
            {
                OWBTypes.OSDProperty osdEmissivity = OWBJson.parse<OWBTypes.OSDProperty>(OWBString.BytesToString(buf, Encoding.UTF8));
                visible = osdEmissivity.visible;
            }
            catch
            {

            }
            return visible;
        }

        public bool PutEmissivityVisible(bool visible)
        {
            byte[] buf = new byte[1024];
            uint length = 0;
            if (OWBTypes.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_OSD_LAYOUT_EMISSIVITY, ref buf, ref length)))
            {
                OWBTypes.OSDProperty osdEmissivity = OWBJson.parse<OWBTypes.OSDProperty>(OWBString.BytesToString(buf, Encoding.UTF8));
                osdEmissivity.visible = visible;
                string objStr = OWBJson.stringify(osdEmissivity);
                OWBString.StringToBytes(objStr, objStr.Length, Encoding.UTF8).CopyTo(buf, 0);
                if (OWBTypes.CheckStatus(RestSDK.Put(IP, (ushort)PORT, URL_OSD_LAYOUT_EMISSIVITY, ref buf, ref length)))
                {
                    return true;
                }
            }
            return false;
        }

        public bool GetMaxMinVisible()
        {
            bool visible = false;
            byte[] buf = new byte[1024];
            uint length = 0;
            if (!OWBTypes.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_ISP_INSTRUMENT_OBJECTS_REGIONS, ref buf, ref length)))
            {
                return visible;
            }
            try
            {
                OWBTypes.PolygonMarker regionMarker = OWBJson.parse<OWBTypes.PolygonMarker>(OWBString.BytesToString(buf, Encoding.UTF8));
                visible = regionMarker.Osd_visible;
            }
            catch
            {

            }
            return visible;
        }

        public bool PutMaxMinVisible(bool visible)
        {
            OWBTypes.PolygonMarker regionMarker = new OWBTypes.PolygonMarker();
            regionMarker.PointList = new OWBTypes.Pos[4];
            regionMarker.PointList[0] = new OWBTypes.Pos();
            regionMarker.PointList[0].X = 10;
            regionMarker.PointList[0].Y = 10;
            regionMarker.PointList[1] = new OWBTypes.Pos();
            regionMarker.PointList[1].X = Width - 10;
            regionMarker.PointList[1].Y = 10;
            regionMarker.PointList[2] = new OWBTypes.Pos();
            regionMarker.PointList[2].X = Width - 10;
            regionMarker.PointList[2].Y = Height - 10;
            regionMarker.PointList[3] = new OWBTypes.Pos();
            regionMarker.PointList[3].X = 10;
            regionMarker.PointList[3].Y = Height - 10;
            regionMarker.Osd_visible = visible;
            regionMarker.Emission = 0.97F;
            regionMarker.Distance = 1;
            regionMarker.ReflectionTemp = 20.0F;
            regionMarker.Label = "区域";

            string objStr = OWBJson.stringify(regionMarker);
            byte[] buf = new byte[1024];
            OWBString.StringToBytes(objStr, objStr.Length, Encoding.UTF8).CopyTo(buf, 0);
            uint length = 0;
            if (!OWBTypes.CheckStatus(RestSDK.Put(IP, (ushort)PORT, URL_ISP_INSTRUMENT_OBJECTS_REGIONS, ref buf, ref length)))
            {
                return false;
            }
            return true;
        }

        public float GetIspTItem(Point p)
        {
            float temperature = 0;
            byte[] buf = new byte[1024];
            uint length = 0;
            try
            {
                if (!OWBTypes.CheckStatus(RestSDK.Get(IP, (ushort)PORT, string.Format(URL_ISP_T, p.X, p.Y), ref buf, ref length)))
                {
                    return temperature;
                }
                OWBTypes.IspTItem ispTItem = OWBJson.parse<OWBTypes.IspTItem>(OWBString.BytesToString(buf, Encoding.UTF8));
                temperature = ispTItem.T;
            }
            catch
            { }
            return temperature;
        }

        public void PostIspAF()
        {
            byte[] buf = new byte[0];
            uint length = 0;
            RestSDK.Post(IP, (ushort)PORT, URL_ISP_AF, ref buf, ref length);
        }

        public bool PutManualCaliMode(string caliMode)
        {
            byte[] buf = new byte[1024];
            uint length = 0;
            string objStr = OWBJson.stringify(caliMode);
            OWBString.StringToBytes(objStr, objStr.Length, Encoding.UTF8).CopyTo(buf, 0);
            if (OWBTypes.CheckStatus(RestSDK.Put(IP, (ushort)PORT, URL_SENSOR_CALIMODE, ref buf, ref length)))
            {
                return true;
            }
            return false;
        }

        public string[] GetCaliModes()
        {
            string[] caliModes = new string[2];
            byte[] buf = new byte[1024];
            uint length = 0;
            if (!OWBTypes.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_SENSOR_CALIMODES, ref buf, ref length)))
            {
                return caliModes;
            }
            try
            {
                caliModes = OWBJson.parse<string[]>(OWBString.BytesToString(buf, Encoding.UTF8));
            }
            catch
            {

            }
            return caliModes;
        }

        public string GetCurrentCaliMode()
        {
            string caliMode = string.Empty;
            byte[] buf = new byte[1024];
            uint length = 0;
            if (!OWBTypes.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_SENSOR_CALIMODE, ref buf, ref length)))
            {
                return caliMode;
            }
            try
            {
                caliMode = OWBJson.parse<string>(OWBString.BytesToString(buf, Encoding.UTF8));
            }
            catch
            {

            }
            return caliMode;
        }

        public bool PutIspAGCMode()
        {
            byte[] buf = new byte[1024];
            uint length = 0;
            string objStr = OWBJson.stringify("once");
            OWBString.StringToBytes(objStr, objStr.Length, Encoding.UTF8).CopyTo(buf, 0);
            if (OWBTypes.CheckStatus(RestSDK.Put(IP, (ushort)PORT, URL_ISP_AGCMODE, ref buf, ref length)))
            {
                return true;
            }
            return false;
        }

        public void PostCali()
        {
            byte[] buf = new byte[0];
            uint length = 0;
            RestSDK.Post(IP, (ushort)PORT, URL_SENSOR_CALI, ref buf, ref length);
        }

        public bool ContrastPlus()
        {
            OWBTypes.ISPJconfig ispJconfig = GetIspJconfig();
            if (ispJconfig == null)
            {
                return false;
            }
            ispJconfig.Contrast += 0.5F;
            if (ispJconfig.Contrast > 3)
            {
                ispJconfig.Contrast = 3;
            }

            string objStr = OWBJson.stringify(ispJconfig);
            byte[] buf = new byte[1024];
            OWBString.StringToBytes(objStr, objStr.Length, Encoding.UTF8).CopyTo(buf, 0);
            uint length = 0;
            if (!OWBTypes.CheckStatus(RestSDK.Put(IP, (ushort)PORT, URL_ISP_JCONFIG, ref buf, ref length)))
            {
                return false;
            }
            return true;
        }

        public bool ContrastMinus()
        {
            OWBTypes.ISPJconfig ispJconfig = GetIspJconfig();
            if (ispJconfig == null)
            {
                return false;
            }
            ispJconfig.Contrast -= 0.5F;
            if (ispJconfig.Contrast < 0)
            {
                ispJconfig.Contrast = 0;
            }

            string objStr = OWBJson.stringify(ispJconfig);
            byte[] buf = new byte[1024];
            OWBString.StringToBytes(objStr, objStr.Length, Encoding.UTF8).CopyTo(buf, 0);
            uint length = 0;
            if (!OWBTypes.CheckStatus(RestSDK.Put(IP, (ushort)PORT, URL_ISP_JCONFIG, ref buf, ref length)))
            {
                return false;
            }
            return true;
        }

        public bool BlackLevelPlus()
        {
            OWBTypes.ISPJconfig ispJconfig = GetIspJconfig();
            if (ispJconfig == null)
            {
                return false;
            }
            ispJconfig.BlackLevel += 5;
            if (ispJconfig.BlackLevel > 30)
            {
                ispJconfig.BlackLevel = 30;
            }

            string objStr = OWBJson.stringify(ispJconfig);
            byte[] buf = new byte[1024];
            OWBString.StringToBytes(objStr, objStr.Length, Encoding.UTF8).CopyTo(buf, 0);
            uint length = 0;
            if (!OWBTypes.CheckStatus(RestSDK.Put(IP, (ushort)PORT, URL_ISP_JCONFIG, ref buf, ref length)))
            {
                return false;
            }
            return true;
        }

        public bool BlackLevelMinus()
        {
            OWBTypes.ISPJconfig ispJconfig = GetIspJconfig();
            if (ispJconfig == null)
            {
                return false;
            }
            ispJconfig.BlackLevel -= 5;
            if (ispJconfig.BlackLevel < -30)
            {
                ispJconfig.BlackLevel = -30;
            }

            string objStr = OWBJson.stringify(ispJconfig);
            byte[] buf = new byte[1024];
            OWBString.StringToBytes(objStr, objStr.Length, Encoding.UTF8).CopyTo(buf, 0);
            uint length = 0;
            if (!OWBTypes.CheckStatus(RestSDK.Put(IP, (ushort)PORT, URL_ISP_JCONFIG, ref buf, ref length)))
            {
                return false;
            }
            return true;
        }

        public OWBTypes.ISPJconfig GetIspJconfig()
        {
            OWBTypes.ISPJconfig ispJconfig = null;
            byte[] buf = new byte[1024];
            uint length = 0;
            try
            {
                if (!OWBTypes.CheckStatus(RestSDK.Get(IP, (ushort)PORT, URL_ISP_JCONFIG, ref buf, ref length)))
                {
                    return ispJconfig;
                }
                ispJconfig = OWBJson.parse<OWBTypes.ISPJconfig>(OWBString.BytesToString(buf, Encoding.UTF8));
            }
            catch
            { }
            return ispJconfig;
        }
        #endregion
    }
}