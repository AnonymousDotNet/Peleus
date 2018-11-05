

using Peleus.Utility;

namespace Peleus
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = string.Format("http://www.yiparts.com/autoparts/cylinder_head_gasket/");
            var source = Http.Downloader.Download(url);
            if (!string.IsNullOrEmpty(source))
            {
                Html.HtmlDocument doc = new Html.HtmlDocument();
                doc.LoadHtml(source);
                var name = XpathUtil.GetText(doc.DocumentNode, "");
            }
            
        }
    }
}
