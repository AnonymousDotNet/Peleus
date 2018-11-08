using System;

namespace Peleus.Cache
{
    public class DataCache
    {
        /// <summary>
        /// 唯一标识ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
        public int FId { get; set; }

        /// <summary>
        /// 中文名称
        /// </summary>
        public string CN { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string EN { get; set; }

        /// <summary>
        /// 缩写
        /// </summary>
        public string Abbr { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        public string PostCode { get; set; }

        /// <summary>
        /// 统计用区划代码
        /// </summary>
        public string ZCode { get; set; }

        /// <summary>
        /// 创建时间，时间可以为空的话用DateTime?
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间,同上
        /// </summary>
        public DateTime UpdateTime { get; set; }

        //----

        /// <summary>
        /// 通话代码
        /// </summary>
        public string Callcode { get; set; }

        //------

        /// <summary>
        /// 时区
        /// </summary>
        public int TimeZone { get; set; }

        //--------

        /// <summary>
        /// 别名
        /// </summary>
        public string Alias { get; set; }
    }
}
