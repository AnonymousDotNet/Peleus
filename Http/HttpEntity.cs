using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Http
{
    public class HttpEntity
    {

        /// <summary>
        /// 
        /// </summary>
        public Uri uri;

        /// <summary>
        /// http request header - Referer
        /// </summary>
        public string referer;

        /// <summary>
        /// 保存cookie的容器
        /// </summary>
        public CookieContainer cookieContainer;

        /// <summary>
        /// 代理服务器设置
        /// </summary>
        public WebProxy proxy;

        /// <summary>
        /// 请求响应和获取网络流的超时时限(秒)
        /// </summary>
        public int timeout;

        /// <summary>
        /// 是否超时
        /// </summary>
        public bool expired;

        /// <summary>
        /// 响应的url
        /// </summary>
        public Uri responseUri;

        /// <summary>
        /// 资源内容的类型
        /// </summary>
        public string contentType;

        /// <summary>
        /// 浏览器请求头
        /// </summary>
        public string userAgent;

        /// <summary>
        /// 浏览器伪装X-Forwarded-For
        /// </summary>
        public string forWarded;

        /// <summary>
        /// 响应的字符编码(the character set of the response)
        /// </summary>
        //public string responseCharset;

        /// <summary>
        /// 服务器的编码
        /// </summary>
        public Encoding encoding;

        /// <summary>
        /// 是否只下载文本类型的流
        /// </summary>
        public bool textLimited;

        /// <summary>
        /// 指定编码(specified encoding)
        /// </summary>
        public Encoding specifiedEncoding;

        /// <summary>
        /// 
        /// </summary>
        public byte[] data;

        /// <summary>
        /// 
        /// </summary>
        public string source;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        public HttpEntity(Uri uri)
            : this(uri, null, null, 0)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="encoding"></param>
        public HttpEntity(Uri uri, Encoding encoding)
            : this(uri, encoding, null, 0)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="cookieContainer"></param>
        public HttpEntity(Uri uri, CookieContainer cookieContainer)
            : this(uri, null, cookieContainer, 0)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="timeout">秒</param>
        public HttpEntity(Uri uri, int timeout)
            : this(uri, null, null, timeout)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="userAgent"></param>
        /// <param name="forWarded"></param>
        public HttpEntity(Uri uri, string userAgent, string forWarded)
            : this(uri, null, null, 0)
        {
            this.userAgent = userAgent;
            this.forWarded = forWarded;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="encoding"></param>
        /// <param name="cookieContainer"></param>
        /// <param name="timeout">秒</param>
        public HttpEntity(Uri uri, Encoding encoding, CookieContainer cookieContainer, int timeout)
        {
            if (encoding == null)
            {
                this.encoding = Encoding.Default;
            }
            else
            {
                this.specifiedEncoding = encoding;
            }

            this.uri = uri;
            this.cookieContainer = cookieContainer;
            this.timeout = timeout;
            this.data = new byte[0];
            //proxy = new WebProxy();
            //proxy.Address = new Uri("http://proxy.domain.com:1010");
            //proxy.Credentials = new NetworkCredential("id", "password");
        }


        /// <summary>
        /// 生成请求
        /// </summary>
        /// <returns></returns>
        public HttpWebRequest GetRequest()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri.AbsoluteUri);

            request.Accept = "*/*";
            request.Headers.Add("Accept-Language", "zh-cn");
            request.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.Referer = referer;

            if (!string.IsNullOrEmpty(userAgent))
            {
                request.ServicePoint.Expect100Continue = false;
                request.KeepAlive = false;
                request.UserAgent = userAgent;
            }
            if (!string.IsNullOrEmpty(forWarded))
            {
                request.Headers.Add("X-Forwarded-For", forWarded);
            }
            if (timeout > 0)
            {
                request.Timeout = timeout * 1000;
                request.ReadWriteTimeout = timeout * 1000;
            }
            if (cookieContainer != null)
            {
                request.CookieContainer = cookieContainer;
            }
            if (proxy != null)
            {
                request.UseDefaultCredentials = true;
                request.Proxy = proxy;
            }

            return request;
        }

        /// <summary>
        /// 获取属性
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public bool SetResponse(HttpWebResponse response)
        {
            responseUri = response.ResponseUri;
            contentType = response.ContentType;
            if (!textLimited || contentType.StartsWith("text/") || contentType.EndsWith("script"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// byte[] -> string
        /// </summary>
        public void GetSource()
        {
            source = encoding.GetString(data);
        }
    }
}
