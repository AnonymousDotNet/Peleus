using System;
using System.Text;

namespace Http
{
    /// <summary>
    /// 编码查找类
    /// </summary>
    public static class Detector
    {
        /// <summary>
        /// HttpWebResponse.ContentType property obtained from Http headers
        /// </summary>
        private static readonly string charsetKey = "charset=";

        /// <summary>
        /// xml file specified symbol
        /// </summary>
        private static readonly string xmlKey = "<?xml";

        /// <summary>
        /// symbol key of encoding in html source
        /// </summary>
        private static readonly string encodingKey = "encoding=\"";

        /// <summary>
        /// close keys of charset property in meta tag
        /// </summary>
        private static readonly string closeKeys = "'\";>";


        /// <summary>
        /// 获取页面的编码
        /// </summary>
        /// <param name="httpEntity"></param>
        public static void Detect(HttpEntity httpEntity)
        {
            Encoding encoding = GetCharsetEncoding(httpEntity.contentType);
            if (encoding == null)
            {
                int count = 2048;
                if (count > httpEntity.data.Length)
                {
                    count = httpEntity.data.Length;
                }
                string headText = httpEntity.encoding.GetString(httpEntity.data, 0, count);
                if (!string.IsNullOrEmpty(headText))
                {
                    encoding = GetTextEncoding(headText.ToLower());
                }
            }
            if (encoding != null)
            {
                if (httpEntity.encoding != encoding)
                {
                    httpEntity.encoding = encoding;
                }
            }
        }

        #region charset in content type
        /// <summary>
        /// 获取ContentType中charset提供的编码
        /// </summary>
        /// <param name="contentType"></param>
        /// <returns></returns>
        private static Encoding GetCharsetEncoding(string contentType)
        {
            Encoding encoding = null;
            if (contentType != null)
            {
                int charsetPos = contentType.IndexOf(charsetKey);
                if (charsetPos != -1)
                {
                    string charset = contentType.Remove(0, charsetPos + charsetKey.Length);
                    if (charset.Length > 0)
                    {
                        encoding = GetEncoding(charset);
                    }
                }
            }
            return encoding;
        }
        #endregion

        #region charset in html/xml
        /// <summary>
        /// 获取源代码meta标签中charset提供的编码
        /// </summary>
        /// <param name="headText"></param>
        /// <returns></returns>
        private static Encoding GetTextEncoding(string headText)
        {
            Encoding encoding = GetHtmlEncoding(headText);
            if (encoding == null)
            {
                encoding = GetXmlEncoding(headText);
            }
            return encoding;
        }

        /// <summary>
        /// 获取html的编码
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static Encoding GetHtmlEncoding(string text)
        {
            Encoding encoding = null;
            int charsetPos = text.IndexOf(charsetKey);
            while (charsetPos != -1)
            {
                charsetPos += charsetKey.Length;
                if (charsetPos < text.Length)
                {
                    encoding = GetEncoding(text[charsetPos]);
                    if (encoding == null)
                    {
                        //特殊情况：锘?!doctype html>\r\n<html lang=\"zh-cn\">\r\n<head>\r\n\t<meta charset=\"utf-8\">\r\n\t<title>
                        //例子：http://www.ln.chinanews.com/html/2015-01-26/1016194.html
                        if (text.Length > charsetPos + 1)
                        {
                            encoding = GetEncoding(text[charsetPos + 1]);
                        }
                        if (encoding == null)
                        {
                            encoding = GetAdvanceEncoding(text, charsetPos);
                        }

                    }
                    if (encoding != null)
                    {
                        break;
                    }
                }
                charsetPos = text.IndexOf(charsetKey, charsetPos);
            }
            return encoding;
        }

        /// <summary>
        /// 获取html编码
        /// </summary>
        /// <param name="text"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        private static Encoding GetAdvanceEncoding(string text, int pos)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = pos; i < text.Length; i++)
            {
                char c = text[i];
                if (closeKeys.IndexOf(c) != -1)
                {
                    break;
                }
                sb.Append(c);
                if (sb.Length > 15)
                {
                    return null;
                }
            }

            if (sb.Length == 0)
            {
                return null;
            }

            try
            {
                return Encoding.GetEncoding(sb.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        /// <summary>
        /// 获取xml的编码
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static Encoding GetXmlEncoding(string text)
        {
            Encoding encoding = null;
            int xmlPos = text.IndexOf(xmlKey);
            if (xmlPos != -1)
            {
                int encodingPos = text.IndexOf(encodingKey);
                if (encodingPos != -1)
                {
                    encodingPos += encodingKey.Length;
                    if (encodingPos < text.Length)
                    {
                        encoding = GetEncoding(text[encodingPos]);
                    }
                }
            }
            return encoding;
        }
        #endregion


        /// <summary>
        /// 判断编码
        /// </summary>
        /// <param name="charset"></param>
        /// <returns></returns>
        private static Encoding GetEncoding(string charset)
        {
            Encoding encoding = null;
            switch (charset.ToLower())
            {
                case "zh-cn": //charset=zh-cn
                case "gb2312": encoding = Encoding.GetEncoding("gb2312"); break;
                case "utf-8": encoding = Encoding.GetEncoding("utf-8"); break;
                case "big5": encoding = Encoding.GetEncoding("big5"); break;
                case "gbk": encoding = Encoding.GetEncoding("gbk"); break;
                case "iso-8859-1": encoding = Encoding.GetEncoding("iso-8859-1"); break;
            }
            if (encoding == null)
            {
                encoding = GetEncoding(charset[0]);
            }
            return encoding;
        }

        /// <summary>
        /// 从首字符判断编码
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private static Encoding GetEncoding(char c)
        {
            Encoding encoding = null;
            switch (char.ToLower(c))
            {
                case 'z':
                case 'g': encoding = Encoding.GetEncoding("gb2312"); break;
                case 'u': encoding = Encoding.GetEncoding("utf-8"); break;
                case 'b': encoding = Encoding.GetEncoding("big5"); break;
                case 'i': encoding = Encoding.GetEncoding("iso-8859-1"); break;
            }
            return encoding;
        }
    }
}