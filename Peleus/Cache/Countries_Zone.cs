using System;

namespace Peleus.Cache
{
    class Countries_Zone
    {
        /// <summary>
        /// 唯一标识ID
        /// </summary>
        public int Id { get; set; }

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
        /// 电话代码
        /// </summary>
        public string Callcode { get; set; }

        /// <summary>
        /// 时区
        /// </summary>
        public int TimeZone { get; set; }

        /// <summary>
        /// 创建时间，时间可以为空的话用DateTime?
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间,同上
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
