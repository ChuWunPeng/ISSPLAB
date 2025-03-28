using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace SDK
{
    public static class RestSDK
    {
        [DllImport("restc.dll", EntryPoint = "restc_set_thread_num")]
        public static extern void Rest_set_thread_num(int num);

        [DllImport("restc.dll", EntryPoint = "restc_cleanup")]
        public static extern void Rest_cleanup();

        [DllImport("restc.dll", EntryPoint = "restc_get_version", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Rest_get_version(uint def, string buf, int bsz);

        [DllImport("restc.dll", EntryPoint = "restc_create_headers", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Rest_create_headers();

        [DllImport("restc.dll", EntryPoint = "restc_destroy_headers", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Rest_destroy_headers(IntPtr header);

        [DllImport("restc.dll", EntryPoint = "restc_add_header", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Rest_add_header(IntPtr header, string key, string value);

        [DllImport("restc.dll", EntryPoint = "restc_get_header_num")]
        public static extern int Rest_get_header_num(IntPtr header);

        [DllImport("restc.dll", EntryPoint = "restc_get_header", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Rest_get_header(IntPtr header, int idx, string key, int ksz, string value, int vsz);

        [DllImport("restc.dll", EntryPoint = "restc_find_header")]
        public static extern int Rest_find_header(IntPtr header, string key, string value, int vsz);
        /// <summary>
        /// 创建rest连接
        /// </summary>
        /// <param name="host">rest服务端host</param>
        /// <param name="port_num">rest服务端端口号</param>
        /// <returns>连接指针</returns>
        [DllImport("restc.dll", EntryPoint = "restc_create_connection", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Rest_create_connection(string host, ushort port_num);
        /// <summary>
        /// 销毁rest连接
        /// </summary>
        /// <param name="restConn">连接指针</param>
        /// <returns></returns>
        [DllImport("restc.dll", EntryPoint = "restc_destroy_connection", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Rest_destroy_connection(IntPtr restConn);
        /// <summary>
        /// 设置rest身份认证
        /// </summary>
        /// <param name="user">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [DllImport("restc.dll", EntryPoint = "restc_set_authroization", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Rest_set_authroization(string user, byte[] password);
        /// <summary>
        /// 设置请求超时时间
        /// </summary>
        /// <param name="restConn">连接指针</param>
        /// <param name="timeout">超时时间（单位ms）</param>
        /// <returns></returns>
        [DllImport("restc.dll", EntryPoint = "restc_set_timeout", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Rest_set_timeout(IntPtr restConn, int timeout);
        /// <summary>
        /// 发送get请求
        /// </summary>
        /// <param name="restConn">连接指针</param>
        /// <param name="url">请求资源</param>
        /// <param name="header">请求头</param>
        /// <param name="buf">请求正文</param>
        /// <param name="bufsz">正文长度</param>
        /// <param name="length">响应长度</param>
        /// <returns>响应状态码</returns>
        [DllImport("restc.dll", EntryPoint = "restc_get", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Rest_get(IntPtr restConn, string url, IntPtr header, byte[] buf, uint bufsz, ref uint length);
        /// <summary>
        /// 发送put请求
        /// </summary>
        /// <param name="restConn">连接指针</param>
        /// <param name="url">请求资源</param>
        /// <param name="header">请求头</param>
        /// <param name="buf">请求正文</param>
        /// <param name="bufsz">正文长度</param>
        /// <param name="length">响应长度</param>
        /// <returns>响应状态码</returns>
        [DllImport("restc.dll", EntryPoint = "restc_put", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Rest_put(IntPtr restConn, string url, IntPtr header, byte[] buf, uint bufsz, ref uint length);
        /// <summary>
        /// 发送post请求
        /// </summary>
        /// <param name="restConn">连接指针</param>
        /// <param name="url">请求资源</param>
        /// <param name="header">请求头</param>
        /// <param name="buf">请求正文</param>
        /// <param name="bufsz">正文长度</param>
        /// <param name="length">响应长度</param>
        /// <returns>响应状态码</returns>
        [DllImport("restc.dll", EntryPoint = "restc_post", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Rest_post(IntPtr restConn, string url, IntPtr header, byte[] buf, uint bufsz, ref uint length);
        /// <summary>
        /// 发送delete请求
        /// </summary>
        /// <param name="restConn">连接指针</param>
        /// <param name="url">请求资源</param>
        /// <param name="header">请求头</param>
        /// <param name="buf">请求正文</param>
        /// <param name="bufsz">正文长度</param>
        /// <param name="length">响应长度</param>
        /// <returns>响应状态码</returns>
        [DllImport("restc.dll", EntryPoint = "restc_del", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Rest_del(IntPtr restConn, string url, IntPtr header, byte[] buf, uint bufsz, ref uint length);
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="restConn">连接指针</param>
        /// <param name="url">请求资源</param>
        /// <param name="header">请求头</param>
        /// <param name="filePath">文件名</param>
        /// <param name="buf">请求正文</param>
        /// <param name="bufsz">正文长度</param>
        /// <param name="length">响应长度</param>
        /// <returns>响应状态码</returns>
        [DllImport("restc.dll", EntryPoint = "restc_post_file", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Rest_post_file(IntPtr restConn, string url, IntPtr header, string filePath, byte[] buf, uint bufsz, ref uint length);
        /// <summary>
        /// 上传流
        /// </summary>
        /// <param name="restConn">连接指针</param>
        /// <param name="url">请求资源</param>
        /// <param name="header">请求头</param>
        /// <param name="send_buf">字符串流</param>
        /// <param name="send_sz">字符串流长度</param>
        /// <param name="reply_buf">响应正文</param>
        /// <param name="reply_sz">响应长度</param>
        /// <returns>响应状态码</returns>
        [DllImport("restc.dll", EntryPoint = "restc_post_buffer", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Rest_post_buffer(IntPtr restConn, string url, IntPtr header, string send_buf, uint send_sz, byte[] reply_buf, ref uint reply_sz);
        /// <summary>
        /// get请求
        /// </summary>
        /// <param name="ip">rest服务端IP地址</param>
        /// <param name="port">rest服务端端口号</param>
        /// <param name="userName">rest身份认证用户名</param>
        /// <param name="password">rest身份认证密码</param>
        /// <param name="url">请求资源</param>
        /// <param name="buf">请求正文</param>
        /// <returns>响应状态码</returns>
        public static int Get(string ip, ushort port, string url, ref byte[] buf, ref uint length)
        {
            int statusCode = 0;
            try
            {
                IntPtr restConn = Rest_create_connection(ip, port);

                Rest_set_timeout(restConn, 1000);
                length = 1024;
                statusCode = Rest_get(restConn, url, IntPtr.Zero, buf, (uint)buf.Length, ref length);
                string bufStr = OWBString.BytesToString(buf, Encoding.UTF8);
                if (bufStr.Contains("sc"))
                {
                    OWBTypes.OWBStatusCode sc = OWBJson.parse<OWBTypes.OWBStatusCode>(bufStr);
                    statusCode = sc.SC;
                }
                Rest_destroy_connection(restConn);
            }
            catch
            {

            }
            return statusCode;
        }
        /// <summary>
        /// put请求
        /// </summary>
        /// <param name="ip">rest服务端IP地址</param>
        /// <param name="port">rest服务端端口号</param>
        /// <param name="userName">rest身份认证用户名</param>
        /// <param name="password">rest身份认证密码</param>
        /// <param name="url">请求资源</param>
        /// <param name="buf">请求正文</param>
        /// <returns>响应状态码</returns>
        public static int Put(string ip, ushort port, string url, ref byte[] buf, ref uint length)
        {
            int statusCode = 0;
            try
            {
                IntPtr restConn = Rest_create_connection(ip, port);
                Rest_set_timeout(restConn, 1000);
                length = (uint)buf.Length;
                statusCode = Rest_put(restConn, url, IntPtr.Zero, buf, (uint)buf.Length, ref length);
                string bufStr = OWBString.BytesToString(buf, Encoding.UTF8);
                if (bufStr.Contains("sc"))
                {
                    OWBTypes.OWBStatusCode sc = OWBJson.parse<OWBTypes.OWBStatusCode>(bufStr);
                    statusCode = sc.SC;
                }
                Rest_destroy_connection(restConn);
            }
            catch
            {

            }
            return statusCode;
        }
        /// <summary>
        /// post请求
        /// </summary>
        /// <param name="ip">rest服务端IP地址</param>
        /// <param name="port">rest服务端端口号</param>
        /// <param name="userName">rest身份认证用户名</param>
        /// <param name="password">rest身份认证密码</param>
        /// <param name="url">请求资源</param>
        /// <param name="buf">请求正文</param>
        /// <returns>响应状态码</returns>
        public static int Post(string ip, ushort port, string url, ref byte[] buf, ref uint length)
        {
            int statusCode = 0;
            try
            {
                IntPtr restConn = Rest_create_connection(ip, port);
                Rest_set_timeout(restConn, 1000);
                statusCode = Rest_post(restConn, url, IntPtr.Zero, buf, (uint)buf.Length, ref length);
                string bufStr = OWBString.BytesToString(buf, Encoding.UTF8);
                if (bufStr.Contains("sc"))
                {
                    OWBTypes.OWBStatusCode sc = OWBJson.parse<OWBTypes.OWBStatusCode>(bufStr);
                    statusCode = sc.SC;
                }
                Rest_destroy_connection(restConn);
            }
            catch
            {

            }
            return statusCode;
        }
        /// <summary>
        /// delete请求
        /// </summary>
        /// <param name="ip">rest服务端IP地址</param>
        /// <param name="port">rest服务端端口号</param>
        /// <param name="userName">rest身份认证用户名</param>
        /// <param name="password">rest身份认证密码</param>
        /// <param name="url">请求资源</param>
        /// <param name="buf">请求正文</param>
        /// <returns>响应状态码</returns>
        public static int Delete(string ip, ushort port, string url, ref byte[] buf, ref uint length)
        {
            int statusCode = 0;
            try
            {
                IntPtr restConn = Rest_create_connection(ip, port);
                Rest_set_timeout(restConn, 1000);
                length = 1024;
                statusCode = Rest_del(restConn, url, IntPtr.Zero, buf, 1024, ref length);
                string bufStr = OWBString.BytesToString(buf, Encoding.UTF8);
                if (bufStr.Contains("sc"))
                {
                    OWBTypes.OWBStatusCode sc = OWBJson.parse<OWBTypes.OWBStatusCode>(bufStr);
                    statusCode = sc.SC;
                }
                Rest_destroy_connection(restConn);
            }
            catch
            {

            }
            return statusCode;
        }
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="ip">rest服务端IP地址</param>
        /// <param name="port">rest服务端端口号</param>
        /// <param name="userName">rest身份认证用户名</param>
        /// <param name="password">rest身份认证密码</param>
        /// <param name="url">请求资源</param>
        /// <param name="filePath">上传文件名</param>
        /// <param name="buf">请求正文</param>
        /// <returns>响应状态码</returns>
        public static int Postfile(string ip, ushort port, string url, string filePath, ref byte[] buf, ref uint length)
        {
            int statusCode = 0;
            try
            {
                IntPtr restConn = Rest_create_connection(ip, port);
                Rest_set_timeout(restConn, 10000);
                length = 1024;
                statusCode = Rest_post_file(restConn, url, IntPtr.Zero, filePath, buf, 1024, ref length);
                string bufStr = OWBString.BytesToString(buf, Encoding.UTF8);
                if (bufStr.Contains("sc"))
                {
                    OWBTypes.OWBStatusCode sc = OWBJson.parse<OWBTypes.OWBStatusCode>(bufStr);
                    statusCode = sc.SC;
                }
                Rest_destroy_connection(restConn);
            }
            catch
            {

            }
            return statusCode;
        }
        /// <summary>
        /// 上传流
        /// </summary>
        /// <param name="ip">rest服务端IP地址</param>
        /// <param name="port">rest服务端端口号</param>
        /// <param name="userName">rest身份认证用户名</param>
        /// <param name="password">rest身份认证密码</param>
        /// <param name="url">请求资源</param>
        /// <param name="send_buf">上传字符串流</param>
        /// <param name="buf">请求正文</param>
        /// <returns>响应状态码</returns>
        public static int Postbuffer(string ip, ushort port, string url, string send_buf, ref byte[] buf, ref uint length)
        {
            int statusCode = 0;
            try
            {
                IntPtr restConn = Rest_create_connection(ip, port);
                Rest_set_timeout(restConn, 10000);
                length = 1024;
                statusCode = Rest_post_buffer(restConn, url, IntPtr.Zero, send_buf, (uint)send_buf.Length, buf, ref length);
                string bufStr = OWBString.BytesToString(buf, Encoding.UTF8);
                if (bufStr.Contains("sc"))
                {
                    OWBTypes.OWBStatusCode sc = OWBJson.parse<OWBTypes.OWBStatusCode>(bufStr);
                    statusCode = sc.SC;
                }
                Rest_destroy_connection(restConn);
            }
            catch
            {

            }
            return statusCode;
        }
    }
}
