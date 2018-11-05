using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Peleus.Utility
{
    class StringUtil
    {
        /// <summary>
        /// 去除重复元素
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static void FilterRepeat(List<string> list)
        {
            if (list == null)
            {
                return;
            }
            HashSet<string> hs = new HashSet<string>();
            foreach (string s in list)
            {
                hs.Add(s);
            }
            list.Clear();
            list.AddRange(hs);
        }

        //合并文本
        public static string MergeText(List<string> texts)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string s in texts)
            {
                sb.Append(s);
            }
            return sb.ToString();
        }



        #region 文本翻译\u300a\u8d64的这种格式转换函数
        /// <summary>
        /// \u300a\u8d64的这种格式文本还原
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string RestoreText(string text)
        {
            ArrayList alName = UniCodeAdd(text);
            string target = "";
            for (int i = 0; i < alName.Count; i++)
            {
                string outStr = "";
                if (alName[i].ToString() == "\\" && alName[i + 1].ToString() == "u")
                {
                    string a = "";
                    for (int n = 2; n <= 5; n++)
                    {
                        string aa = alName[i + n].ToString();

                        a += aa;
                    }
                    outStr += ConvertTo(a);
                    i = i + 5;
                }
                else
                {
                    outStr = alName[i].ToString();
                }
                target += outStr;
            }
            return target;
        }
        /// <summary>
        /// \u300a\u8d64的这种格式文本还原  保留空格
        /// </summary>
        /// <param name="text"></param>
        /// <param name="isSpace">true:保留空格</param>
        /// <returns></returns>
        public static string RestoreText(string text, bool isSpace)
        {
            ArrayList alName = UniCodeAdd(text);
            string target = "";
            for (int i = 0; i < alName.Count; i++)
            {
                string outStr = "";
                if (alName[i].ToString() == "\\" && alName[i + 1].ToString() == "u")
                {
                    string a = "";
                    for (int n = 2; n <= 5; n++)
                    {
                        string aa = alName[i + n].ToString();

                        a += aa;
                    }
                    outStr += ConvertTo(a);
                    i = i + 5;
                }
                else
                {
                    outStr = alName[i].ToString();
                    if (outStr == "" && isSpace)
                    { outStr = " "; }
                }
                target += outStr;


            }
            return target;
        }

        private static ArrayList UniCodeAdd(string soure)
        {
            string parseMark = "(?<content>.)";

            ArrayList myArray = new ArrayList();

            List<string> list = RegexUtil.MatchTextCollection(soure, parseMark, "content");
            foreach (string s in list)
            {
                myArray.Add(s.Trim());
            }
            return myArray;
        }

        private static string ConvertTo(string soure)
        {
            string outStr = "";
            try
            {
                outStr += (char)int.Parse(soure, System.Globalization.NumberStyles.HexNumber);
            }
            catch (FormatException ex)
            {
                outStr = ex.Message;
            }
            return outStr;
        }
        #endregion
    }
}
