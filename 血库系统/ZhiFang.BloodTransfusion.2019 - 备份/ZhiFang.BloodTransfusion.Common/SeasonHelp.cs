using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.BloodTransfusion.Common
{
    public static class SeasonHelp
    {
        /// <summary>
        /// 1 第一季度  2 第二季度 3 第三季度 4 第四季度
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private static int _getSeason(DateTime date)
        {
            int season = 0;
            int month = date.Month;
            switch (month)
            {
                case 0:
                case 1:
                case 2:
                    season = 1;
                    break;
                case 3:
                case 4:
                case 5:
                    season = 2;
                    break;
                case 6:
                case 7:
                case 8:
                    season = 3;
                    break;
                case 9:
                case 10:
                case 11:
                    season = 4;
                    break;
                default:
                    break;
            }
            return season;
        }
        public static string GetSeason(DateTime date)
        {
            string season = string.Format("{0}年{1}季度", date.Year, _getSeason(date));
            return season;
        }
    }
}
