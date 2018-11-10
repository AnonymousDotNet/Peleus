using Html;
using Peleus.Utility;
using System;
using System.Collections.Generic;

namespace Peleus.Script
{
    class Countries_Zone
    {
        public void Download()
        {
            string url = string.Format("http://gx.189.cn/mall/detail/html/gjdmqh.html");
            string source = Http.Downloader.Download(url);
            if (!string.IsNullOrEmpty(source))
            {
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(source);

                string Xpath_Countries = "";
                string CN = "";
                string EN = "";
                string Abbr = "";
                string CallCode = "";
                int TimeZone;//这里需要转换成decimal(2, 1)类型
                DateTime CreateTime;

                for (int i = 3; i < 195; i++)
                {
                    Xpath_Countries = string.Format("//body/table[1]/tr[{0}]/td", i);
                    
                    EN = XpathUtil.GetText(doc.DocumentNode, string.Format(Xpath_Countries + "[1]"));
                    CN = XpathUtil.GetText(doc.DocumentNode, string.Format(Xpath_Countries + "[2]"));
                    Abbr = XpathUtil.GetText(doc.DocumentNode, string.Format(Xpath_Countries + "[3]"));
                    CallCode = XpathUtil.GetText(doc.DocumentNode, string.Format(Xpath_Countries + "[4]"));
                    TimeZone = int.Parse(XpathUtil.GetText(doc.DocumentNode, string.Format(Xpath_Countries + "[5]")));

                    CreateTime = DateTime.Now;
                    Cache.Countries_Zone countries_Zone = new Cache.Countries_Zone();
                    countries_Zone.CN = CN;
                    countries_Zone.EN = EN;
                    countries_Zone.Abbr = Abbr;
                    countries_Zone.Callcode = CallCode;
                    countries_Zone.TimeZone = TimeZone;

                }
                //var name_Provinces_Url = XpathUtil.GetAttributes(doc.DocumentNode, "//tr[@class='provincetr']/td/a", "href");
            }
        }
        public void Sql_Counties()
        {

        }
    }
}
