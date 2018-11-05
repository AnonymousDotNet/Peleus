using System;

namespace Peleus.Utility
{
    class JsonUtil
    {

        #region Json一长串时间转换
        public static DateTime ParseJsonTime(long dateVal)
        {
            DateTime dt = new DateTime(1970, 1, 1);
            dt = dt.AddSeconds(dateVal);
            dt = dt.AddHours(8);
            return dt;
        }

        public static DateTime ParseJsonTime(long dateVal, int type)
        {
            DateTime timeStamp = new DateTime(1970, 1, 1);
            long t = dateVal * 10000 + timeStamp.Ticks;
            DateTime dt = new DateTime(t);
            return dt.AddHours(8);
        }
        #endregion

        /// <summary> 
        /// 将c# DateTime时间格式转换为Unix时间戳格式
        /// </summary> 
        /// <param name="time"> 时间 </param> 
        /// <returns> double </returns> 
        public static double ConvertDateTimeInt(System.DateTime time)
        {
            double intResult = 0;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            intResult = (time - startTime).TotalSeconds;
            return intResult;
        }
    }
}
