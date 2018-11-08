
using System.Data;
using Peleus.Utility;
using System.Configuration;
using System.Data.SqlClient;
using SqlUtil;
using System;
using Html;

namespace Peleus
{
    class Program
    {
        static void Main(string[] args)
        {
            //var url = string.Format("http://gx.189.cn/mall/detail/html/gjdmqh.html");
            //var source = Http.Downloader.Download(url);
            //if (!string.IsNullOrEmpty(source))
            //{
            //    Html.HtmlDocument doc = new Html.HtmlDocument();
            //    doc.LoadHtml(source);
            //    var name = XpathUtil.GetText(doc.DocumentNode, "");
            //}


            //统计局
            string url = string.Format("http://www.stats.gov.cn/tjsj/tjbz/tjyqhdmhcxhfdm/2017/index.html");


            var source = Http.Downloader.Download(url);
            if (!string.IsNullOrEmpty(source))
            {
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(source);

                //var name_Provinces = XpathUtil.GetTexts(doc.DocumentNode, "//tr[@class='provincetr']/td/a", true, "|");
                var name_Provinces = XpathUtil.GetHtmls(doc.DocumentNode, "//tr[@class='provincetr']/td/a");

                var name_Provinces_Url = XpathUtil.GetAttributes(doc.DocumentNode, "//tr[@class='provincetr']/td/a", "href");

                
                foreach (var item in name_Provinces)
                {
                    if (!string.IsNullOrEmpty(item))
                    {

                    }
                }
            }







            string sql = "";

            string con = ConfigurationManager.AppSettings["Database"];//获取配置文件里的数据库地址信息，这里默认获取当前应用的配置文件，不能新建一个配置文件就能使用，当然好像也可以用新增的配置文件只是没有了解过，就暂时先这么写了。

            //SqlConnection database = MsSqlUtil.Connect(Config.DB);//这个跟上面的是一样的，只是用了封装好的DLL。
            var id = SqlClient.GetData(sql);
        }

    }
}
