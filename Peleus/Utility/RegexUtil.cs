using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Peleus.Utility
{
    class RegexUtil
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        private static Match GetMatch(string text, string pattern)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(pattern))
            {
                return null;
            }
            Match mc = Regex.Match(text, pattern);
            if (mc.Success)
            {
                return mc;
            }
            return null;
        }

        /// <summary>
        /// 匹配正则
        /// </summary>
        /// <param name="text"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static string MatchText(string text, string pattern)
        {
            Match mc = GetMatch(text, pattern);
            if (mc != null)
            {
                return mc.Value;
            }
            return "";
        }

        /// <summary>
        /// 匹配有分组的正则
        /// </summary>
        /// <param name="text"></param>
        /// <param name="pattern"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public static string MatchText(string text, string pattern, string groupName)
        {
            if (string.IsNullOrEmpty(groupName))
            {
                return "";
            }
            Match mc = GetMatch(text, pattern);
            if (mc != null)
            {
                return mc.Groups[groupName].Value;
            }
            return "";
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="text"></param>
        /// <param name="pattern"></param>
        /// <param name="index">填写分组的index</param>
        /// <returns></returns>
        public static string MatchText(string text, string pattern, int index)
        {
            Match mc = GetMatch(text, pattern);
            if (mc != null && mc.Groups.Count > index)
            {
                return mc.Groups[index].Value;
            }
            return "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="pattern"></param>
        /// <param name="groupName"></param>
        /// <param name="isFilterRepeat"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string MatchTexts(string text, string pattern, string groupName, bool isFilterRepeat, string separator)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(pattern))
            {
                return "";
            }

            MatchCollection mcc = Regex.Matches(text, pattern);
            StringBuilder sb = new StringBuilder();
            List<string> list = new List<string>();

            foreach (Match mc in mcc)
            {
                list.Add(mc.Groups[groupName].Value);
            }
            if (isFilterRepeat)
            {
                StringUtil.FilterRepeat(list);
            }
            foreach (string s in list)
            {
                sb.AppendLine(separator + s);
            }
            if (sb.Length > 0 && !string.IsNullOrEmpty(separator))
            {
                sb.Remove(0, 1);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="pattern"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public static List<string> MatchTextCollection(string text, string pattern, string groupName)
        {
            List<string> list = new List<string>();
            MatchCollection mcc = Regex.Matches(text, pattern, RegexOptions.IgnoreCase);

            if (string.IsNullOrEmpty(groupName))
            {
                foreach (Match mc in mcc)
                {
                    list.Add(mc.Value);
                }
            }
            else
            {
                foreach (Match mc in mcc)
                {
                    list.Add(mc.Groups[groupName].Value);
                }
            }

            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="parrern"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static List<string> MatchTextCollection(string text, string parrern, int index)
        {
            List<string> list = new List<string>();
            MatchCollection mcc = Regex.Matches(text, parrern, RegexOptions.IgnoreCase);
            foreach (Match mc in mcc)
            {
                list.Add(mc.Groups[index].Value);
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="parrern"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static List<KeyValuePair<string, string>> MatchKVS(string text, string parrern, int index, int index1)
        {
            List<KeyValuePair<string, string>> kvs = new List<KeyValuePair<string, string>>();
            MatchCollection mcc = Regex.Matches(text, parrern, RegexOptions.IgnoreCase);
            foreach (Match mc in mcc)
            {
                kvs.Add(new KeyValuePair<string, string>(mc.Groups[index].Value, mc.Groups[index1].Value));
            }
            return kvs;
        }

        #region 过滤杂质
        /// <summary>
        /// 消除指定杂质
        /// </summary>
        /// <param name="text"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static string RemoveNoise(string text, string pattern)
        {
            return Regex.Replace(text, pattern, "", RegexOptions.IgnoreCase);
        }

        public static List<string> RemobeNoises(List<string> texts, string pattern)
        {
            List<string> results = new List<string>();
            foreach (string text in texts)
            {
                results.Add(RemoveNoise(text, pattern));
            }
            return results;
        }

        #region 不需要的方法
        ///// <summary>
        ///// 指定移除杂质的类型
        ///// </summary>
        //public static string RemoveNoise(string text, NoiseType noiseType)
        //{
        //    if ((noiseType & NoiseType.Comment) == NoiseType.Comment)
        //    {
        //        text = RemoveComment(text);
        //    }
        //    if ((noiseType & NoiseType.Tag) == NoiseType.Tag)
        //    {
        //        text = RemoveTag(text);
        //    }
        //    return text;
        //}

        //public static List<string> RemoveNoises(List<string> texts, NoiseType noiseType)
        //{
        //    List<string> results = new List<string>();
        //    foreach (string text in texts)
        //    {
        //        results.Add(RemoveNoise(text, noiseType));
        //    }
        //    return results;
        //}
        #endregion

        /// <summary>
        /// 消除HTML注释
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string RemoveComment(string text)
        {
            return Regex.Replace(text, @"<!--[\s\S]*?-->", "");
        }

        /// <summary>
        /// 消除HTML标签,仅仅是标签，标签包着的内容不移除
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string RemoveTag(string text)
        {
            return Regex.Replace(text, @"\<[\s\S]+?\>", "");
        }
        #endregion
    }
}

