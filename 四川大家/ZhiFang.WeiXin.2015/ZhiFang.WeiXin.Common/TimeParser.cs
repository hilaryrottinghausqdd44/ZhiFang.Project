using System;
using System.Collections.Generic;
using System.Text;

namespace ZhiFang.WeiXin.Common
{
    public class TimeParser
    {
        /// <summary>
        /// 把秒转换成分钟
        /// </summary>
        /// <returns></returns>
        public static int SecondToMinute(int Second)
        {
            decimal mm = (decimal)((decimal)Second / (decimal)60);
            return Convert.ToInt32(Math.Ceiling(mm));           
        }

        #region 返回某年某月最后一天
        /// <summary>
        /// 返回某年某月最后一天
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>日</returns>
        public static int GetMonthLastDate(int year, int month)
        {
            DateTime lastDay = new DateTime(year, month, new System.Globalization.GregorianCalendar().GetDaysInMonth(year, month));
            int Day = lastDay.Day;
            return Day;
        }
        #endregion

        #region 返回时间差
        public static string DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            string dateDiff = null;
            try
            {
                //TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
                //TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
                //TimeSpan ts = ts1.Subtract(ts2).Duration();
                TimeSpan ts = DateTime2 - DateTime1;
                if (ts.Days >=1)
                {
                    dateDiff = DateTime1.Month.ToString() + "月" + DateTime1.Day.ToString() + "日";
                }
                else
                {
                    if (ts.Hours > 1)
                    {
                        dateDiff = ts.Hours.ToString() + "小时前";
                    }
                    else
                    {
                        dateDiff = ts.Minutes.ToString() + "分钟前";
                    }
                }
            }
            catch
            { }
            return dateDiff;
        }
        #endregion
        #region 返回毫秒
        public static string getTowTimeString(DateTime dtBegin, DateTime dtEnd)
        {
            string str = "";
            TimeSpan span = (TimeSpan)(dtEnd - dtBegin);
            if (span.Days > 0)
            {
                str = str + span.Days.ToString() + "天";
            }
            if (span.Hours > 0)
            {
                str = str + span.Hours.ToString() + "小时";
            }
            if (span.Minutes > 0)
            {
                str = str + span.Minutes.ToString() + "分钟";
            }
            if (span.Seconds > 0)
            {
                str = str + span.Seconds.ToString() + "秒";
            }
            if ((span.Milliseconds > 0) && (str == ""))
            {
                str = str + span.Milliseconds.ToString() + "毫秒";
            }
            return str;
        }
        #endregion

    }
}
