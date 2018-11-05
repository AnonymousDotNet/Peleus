using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Http
{
    public static class Downloader
    {

        #region get source
        /// <summary>
        /// 下载源文件
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string Download(string url)
        {
            return Download(Convert(url));
        }

        /// <summary>
        /// 下载源文件
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static string Download(Uri uri)
        {
            return Download(uri, null);
        }

        /// <summary>
        /// 下载源文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Download(string url, Encoding encoding)
        {
            Uri uri = Convert(url);
            return Download(uri, encoding);
        }

        /// <summary>
        /// 下载源文件
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Download(Uri uri, Encoding encoding)
        {
            HttpEntity httpEntity = new HttpEntity(uri, encoding);
            Download(httpEntity);
            return httpEntity.source;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static string Download(Uri uri, int timeout)
        {
            HttpEntity httpEntity = new HttpEntity(uri, timeout);
            Download(httpEntity);
            return httpEntity.source;
        }
        #endregion

        #region get buffer
        /// <summary>
        /// 下载字节数组
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static byte[] GetBytes(string url)
        {
            return GetBytes(Convert(url));
        }

        /// <summary>
        /// 下载字节数组
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static byte[] GetBytes(Uri uri)
        {
            HttpEntity httpEntity = new HttpEntity(uri);
            GetBuffer(httpEntity);
            return httpEntity.data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static byte[] GetBytes(string url, int timeout)
        {
            HttpEntity httpEntity = new HttpEntity(new Uri(url));
            httpEntity.timeout = timeout;
            GetBuffer(httpEntity);
            return httpEntity.data;
        }

        #endregion


        /// <summary>
        /// 下载源文件
        /// </summary>
        /// <param name="httpEntity"></param>
        /// <returns></returns>
        public static void Download(HttpEntity httpEntity)
        {
            GetBuffer(httpEntity);
            GetSource(httpEntity);
        }

        /// <summary>
        /// 下载字节数组
        /// </summary>
        /// <param name="httpEntity"></param>
        public static void GetBuffer(HttpEntity httpEntity)
        {
            if (httpEntity.uri == null)
            {
                return;
            }

            HttpWebResponse response = null;
            Stream stream = null;
            try
            {
                HttpWebRequest request = httpEntity.GetRequest();
                response = (HttpWebResponse)request.GetResponse();
                if (httpEntity.SetResponse(response))
                {
                    stream = response.GetResponseStream();
                    httpEntity.data = Dumper.Dump(stream);
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.Timeout)
                {
                    httpEntity.expired = true;
                }
            }
            catch //(Exception ex)
            {
                //Console.WriteLine(ex);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
            }
        }

        /// <summary>
        /// 获取源文件
        /// </summary>
        /// <param name="httpEntity"></param>
        public static void GetSource(HttpEntity httpEntity)
        {
            if (httpEntity.data == null || httpEntity.data.Length == 0)
            {
                return;
            }

            if (httpEntity.specifiedEncoding != null)
            {
                httpEntity.encoding = httpEntity.specifiedEncoding;
            }
            else
            {
                Detector.Detect(httpEntity);
            }

            httpEntity.GetSource();
        }

        #region url -> uri
        /// <summary>
        /// url -> uri
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static Uri Convert(string url)
        {
            if (url == null)
            {
                return null;
            }
            try
            {
                return new Uri(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine(url + "\n" + ex);
            }
            return null;
        }
        #endregion

    }
}

