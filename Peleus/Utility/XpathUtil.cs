using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Html;

namespace Peleus.Utility
{
    class XpathUtil
    {

        public static HtmlNode GetHtmlNode(string source)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(source);
            return htmlDoc.DocumentNode;
        }

        public static string GetHtml(HtmlNode htmlNode, string xpath)
        {
            HtmlNode hn = htmlNode.SelectSingleNode(xpath);
            return hn.InnerHtml;
        }

        public static List<string> GetHtmls(HtmlNode htmlNode, string xpath)
        {
            HtmlNodeCollection hnc = htmlNode.SelectNodes(xpath);
            List<string> list = new List<string>();
            foreach (HtmlNode hn in hnc)
            {
                list.Add(hn.InnerHtml);
            }
            return list;
        }

        /// <summary>
        /// xpath获取符合路径的第一元素
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public static string GetText(HtmlNode parentNode, string xpath)
        {
            HtmlNode hn = parentNode.SelectSingleNode(xpath);
            if (hn != null)
            {
                return hn.InnerText;
            }
            return "";
        }

        /// <summary>
        /// 使用xpath解析TAG并获取其中的文本，并合并
        /// </summary>
        /// <param name="parentNode">指定HTML节点</param>
        /// <param name="xpath">HTML的DOM路径</param>
        /// <param name="separator">分隔符，使用什么来分隔内容</param>
        /// <returns></returns>
        public static string GetTexts(HtmlNode parentNode, string xpath, bool isFilterRepeat, string separator)
        {
            HtmlNodeCollection hnc = parentNode.SelectNodes(xpath);
            StringBuilder sb = new StringBuilder();
            List<string> list = new List<string>();

            foreach (HtmlNode hn in hnc)
            {
                list.Add(hn.InnerText);
            }

            if (isFilterRepeat)
            {
                StringUtil.FilterRepeat(list);
            }

            foreach (string s in list)
            {
                sb.Append(separator + s);
            }
            if (sb.Length > 0 && !string.IsNullOrEmpty(separator))
            {
                sb.Remove(0, 1);
            }
            return sb.ToString();
        }

        /// <summary>
        /// xpath
        /// </summary>
        /// <param name="htmlNode"></param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public static List<string> GetTextCollection(HtmlNode htmlNode, string xpath)
        {
            List<string> lists = new List<string>();
            HtmlNodeCollection hnc = htmlNode.SelectNodes(xpath);
            foreach (HtmlNode hn in hnc)
            {
                lists.Add(hn.InnerText);
            }
            return lists;
        }

        public static string GetAttribute(HtmlNode htmlNode, string xpath, string attributeName)
        {
            HtmlNode hn = htmlNode.SelectSingleNode(xpath);
            if (hn != null)
            {
                return hn.GetAttributeValue(attributeName);
            }

            return "";
        }

        public static List<string> GetAttributes(HtmlNode htmlNode, string xpath, string attributeName)
        {
            List<string> lists = new List<string>();
            HtmlNodeCollection hnc = htmlNode.SelectNodes(xpath);
            if (hnc != null)
            {
                foreach (HtmlNode hn in hnc)
                {
                    if (hn.HasAttribute(attributeName))
                    {
                        lists.Add(hn.GetAttributeValue(attributeName));
                    }
                }
            }
            return lists;
        }

        /// <summary>
        /// 获取xpath定位到的HTML标签里面的特定属性值与Text值,Key 是文本，Value 属性
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetAttribAndText(HtmlNode htmlNode, string xpath, string attributeName)
        {
            HtmlNodeCollection hnc = htmlNode.SelectNodes(xpath);
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (HtmlNode hn in hnc)
            {
                dict.Add(hn.InnerText, hn.GetAttributeValue(attributeName));
            }
            return dict;
        }
    }
}
