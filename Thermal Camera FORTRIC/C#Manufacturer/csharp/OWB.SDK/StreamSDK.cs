using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace SDK
{
    public static class StreamSDK
    {
        public const int STREAMSDK_CREATE_STREAM_FAIL = 1000;
        public const int STREAMSDK_START_STREAM_FAIL = 1001;

        public const string URL_STREAM_VIDEO_RAW = @"/video/raw";
        public const string URL_STREAM_VIDEO_PRI = @"/video/pri";
        public const string URL_STREAM_VIDEO_SUB = @"/video/sub";

        public const int STREAMSDK_EC_OK = 0;
        public const int STREAMSDK_EC_FAIL = 1;
        public const int STREAMSDK_EC_TIMEOUT = 2;
        public const int STREAMSDK_EC_LIMIT = 3;

        public enum streamsdk_enum_pix_type
        {
            STREAMSDK_PIX_RGB = 0,
            STREAMSDK_PIX_Y420 = 1,
            STREAMSDK_PIX_GRAY = 2,
            STREAMSDK_PIX_BGR = 3,
        }

        public const int STREAMSDK_PIX_BGR = 0;

        public struct streamsdk_st_buffer
        {
            public IntPtr buf_ptr;
            public uint buf_size;
            public uint buf_pts;
        };

        public struct streamsdk_st_image
        {
            public IntPtr img_ptr;
            public int img_w;
            public int img_h;
            public int img_linesize;
            public int img_pts;
            public int img_type;
        };

        public struct streamsdk_st_decoder_param
        {
            public int dec_w;
            public int dec_h;
            public int pix_type;			/* enum pix_type */
        };

        public struct streamsdk_st_recorder_param
        {
            public int rec_w;
            public int rec_h;
            public float fps;
        };

        public struct streamsdk_st_event
        {
            public string event_;
            public string id;
            public int pts;
            public double value;
        };

        public delegate void streamsdk_cb_grabber(int error, ref streamsdk_st_buffer buffer, IntPtr user_data);
        public delegate void streamsdk_cb_image_grabber(int error, ref streamsdk_st_image image, IntPtr user_data);
        public delegate void streamsdk_cb_event_grabber(int error, ref streamsdk_st_event event_, IntPtr user_data);

        // 获取StreamSDK版本号
        [DllImport("StreamSDK.dll", EntryPoint = "streamsdk_get_version", CallingConvention = CallingConvention.Cdecl)]
        public static extern int streamsdk_get_version([MarshalAs(UnmanagedType.LPArray, SizeConst = 4)] int[] ver_no);		/* version number is an array of 4 integers */

        [DllImport("StreamSDK.dll", EntryPoint = "streamsdk_get_version_str", CallingConvention = CallingConvention.Cdecl)]
        public static extern int streamsdk_get_version_str(StringBuilder ver_info);

        [DllImport("StreamSDK.dll", EntryPoint = "streamsdk_set_thread_pool_size")]
        public static extern int streamsdk_set_thread_pool_size(int num);			/*Call before any operations initiated*/

        /// <summary>
        /// 创建数据流
        /// </summary>
        /// <param name="link">IP地址</param>
        /// <param name="port_no">端口号</param>
        /// <param name="h">数据流指针</param>
        /// <returns></returns>
        [DllImport("StreamSDK.dll", EntryPoint = "streamsdk_create_stream", CallingConvention = CallingConvention.Cdecl)]
        public static extern int streamsdk_create_stream(string link, ushort port_no, ref IntPtr h);
        /// <summary>
        /// 销毁数据流
        /// </summary>
        /// <param name="h">数据流指针</param>
        /// <returns></returns>
        [DllImport("StreamSDK.dll", EntryPoint = "streamsdk_destroy_stream", CallingConvention = CallingConvention.Cdecl)]
        public static extern int streamsdk_destroy_stream(IntPtr h);
        /// <summary>
        /// 开启数据流
        /// </summary>
        /// <param name="h">数据流指针</param>
        /// <param name="path">数据流资源路径 1.raw;2.pri;3.sub</param>
        /// <param name="max_packet_size">最大允许码率</param>
        /// <param name="grabber">数据流回调函数</param>
        /// <param name="user_data">用于回调函数参数的自定义数据</param>
        /// <returns></returns>
        [DllImport("StreamSDK.dll", EntryPoint = "streamsdk_start_stream", CallingConvention = CallingConvention.Cdecl)]
        public static extern int streamsdk_start_stream(IntPtr h, string path, int max_packet_size, streamsdk_cb_grabber grabber, IntPtr user_data);
        /// <summary>
        /// 关闭数据流
        /// </summary>
        /// <param name="h">数据流指针</param>
        /// <returns></returns>
        [DllImport("StreamSDK.dll", EntryPoint = "streamsdk_stop_stream", CallingConvention = CallingConvention.Cdecl)]
        public static extern int streamsdk_stop_stream(IntPtr h);
        /// <summary>
        /// 用于设置数据流回调函数
        /// </summary>
        /// <param name="h">数据流指针</param>
        /// <param name="grabber">数据流回调函数</param>
        /// <param name="user_data">用于回调函数参数的自定义数据</param>
        /// <returns></returns>
        [DllImport("StreamSDK.dll", EntryPoint = "streamsdk_set_stream_grabber")]
        public static extern int streamsdk_set_stream_grabber(IntPtr h, streamsdk_cb_grabber grabber, IntPtr user_data);

        /// <summary>
        /// 创建H264视频解码器
        /// </summary>
        /// <param name="h">数据流指针</param>
        /// <param name="param">解码器参数</param>
        /// <param name="hd">解码器指针</param>
        /// <returns></returns>
        [DllImport("StreamSDK.dll", EntryPoint = "streamsdk_create_h264_decoder")]
        public static extern int streamsdk_create_h264_decoder(IntPtr h, ref streamsdk_st_decoder_param param, ref IntPtr hd);
        /// <summary>
        /// 销毁H264视频解码器
        /// </summary>
        /// <param name="hd">解码器指针</param>
        /// <returns></returns>
        [DllImport("StreamSDK.dll", EntryPoint = "streamsdk_destroy_h264_decoder")]
        public static extern int streamsdk_destroy_h264_decoder(IntPtr hd);
        /// <summary>
        /// 开启H264视频解码器
        /// </summary>
        /// <param name="hd">解码器指针</param>
        /// <param name="grabber">解码器回调函数</param>
        /// <param name="user_data">用于回调函数参数的自定义数据</param>
        /// <returns></returns>
        [DllImport("StreamSDK.dll", EntryPoint = "streamsdk_start_h264_decoder")]
        public static extern int streamsdk_start_h264_decoder(IntPtr hd, streamsdk_cb_image_grabber grabber, IntPtr user_data);
        /// <summary>
        /// 停止H264视频解码器
        /// </summary>
        /// <param name="hd">解码器指针</param>
        /// <returns></returns>
        [DllImport("StreamSDK.dll", EntryPoint = "streamsdk_stop_h264_decoder")]
        public static extern int streamsdk_stop_h264_decoder(IntPtr hd);
        /// <summary>
        /// 用于设置H264解码器回调函数
        /// </summary>
        /// <param name="hd">解码器指针</param>
        /// <param name="grabber">解码器回调函数</param>
        /// <param name="user_data">用于回调函数参数的自定义数据</param>
        /// <returns></returns>
        [DllImport("StreamSDK.dll", EntryPoint = "streamsdk_set_image_grabber")]
        public static extern int streamsdk_set_image_grabber(IntPtr hd, streamsdk_cb_image_grabber grabber, IntPtr user_data);

        //recorder operations
        [DllImport("StreamSDK.dll", EntryPoint = "streamsdk_create_mp4_recorder")]
        public static extern int streamsdk_create_mp4_recorder(IntPtr h, string filename, ref streamsdk_st_recorder_param param, ref IntPtr hr);

        [DllImport("StreamSDK.dll", EntryPoint = "streamsdk_destroy_mp4_recorder")]
        public static extern int streamsdk_destroy_mp4_recorder(IntPtr hr);

        [DllImport("StreamSDK.dll", EntryPoint = "streamsdk_start_mp4_recorder")]
        public static extern int streamsdk_start_mp4_recorder(IntPtr hr);

        [DllImport("StreamSDK.dll", EntryPoint = "streamsdk_stop_mp4_recorder")]
        public static extern int streamsdk_stop_mp4_recorder(IntPtr hr);

        //event operations
        [DllImport("StreamSDK.dll", EntryPoint = "streamsdk_create_event_decoder")]
        public static extern int streamsdk_create_event_decoder(IntPtr h, ref IntPtr hd);

        [DllImport("StreamSDK.dll", EntryPoint = "streamsdk_destroy_event_decoder")]
        public static extern int streamsdk_destroy_event_decoder(IntPtr hd);

        [DllImport("StreamSDK.dll", EntryPoint = "streamsdk_start_event_decoder")]
        public static extern int streamsdk_start_event_decoder(IntPtr hd, streamsdk_cb_event_grabber grabber, IntPtr user_data);

        [DllImport("StreamSDK.dll", EntryPoint = "streamsdk_stop_event_decoder")]
        public static extern int streamsdk_stop_event_decoder(IntPtr hd);

        [DllImport("StreamSDK.dll", EntryPoint = "streamsdk_set_event_grabber")]
        public static extern int streamsdk_set_event_grabber(IntPtr hd, streamsdk_cb_event_grabber grabber, IntPtr user_data);
    }
}
