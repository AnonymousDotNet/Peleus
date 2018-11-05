using Html;

namespace Peleus.Utility
{
    class HtmlUtil
    {
        /// <summary>
        /// 把真正的图片链接赋值给src
        /// </summary>
        /// <param name="source"></param>
        /// <param name="attribute">图片链接属性</param>
        /// <returns></returns>
        public static string SetImgHtml(string source, string attribute)
        {
            try
            {
                HtmlDocument htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(source);
                HtmlNodeCollection hnc = htmlDoc.DocumentNode.ChildNodes;
                SetNode(hnc, attribute);
                source = htmlDoc.DocumentNode.InnerHtml;


            }
            catch { }
            return source;
        }

        /// <summary>
        /// 重新设置img的src属性值
        /// </summary>
        /// <param name="hnc"></param>
        /// <param name="text"></param>
        public static void SetNode(HtmlNodeCollection hnc, string text)
        {
            foreach (HtmlNode item in hnc)
            {
                if (item.Name == "img")
                {
                    string src = "";
                    string original = "";
                    foreach (var attribute in item.Attributes)
                    {
                        string name = attribute.Name;
                        if (name == "src")
                        {
                            src = attribute.Value;
                        }
                        if (name == text)
                        {
                            original = attribute.Value;
                        }
                    }
                    if (!string.IsNullOrEmpty(src) && !string.IsNullOrEmpty(original))
                    {
                        item.Attributes["src"].Value = original;
                    }
                }
                if (item.HasChildNodes)
                {
                    SetNode(item.ChildNodes, text);
                }
            }
        }
    }
}
