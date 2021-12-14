
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.OA;
using ZhiFang.Entity.OA;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;

namespace ZhiFang.BLL.OA
{
    /// <summary>
    ///节假日设置
    /// </summary>
    public class BATHolidaySetting : BaseBLL<ATHolidaySetting>, ZhiFang.IBLL.OA.IBATHolidaySetting
    {
        public IList<ATHolidaySetting> SearchATHolidaySettingByYearAndMonth(int year, int month, string dateCode)
        {
            IList<ATHolidaySetting> tempList = new List<ATHolidaySetting>();
            if (year >= 0 && month >= 0)
            {
                StringBuilder strb = new StringBuilder();
                strb.Append(" Year=" + year + " and  Month=" + month);
                if (!String.IsNullOrEmpty(dateCode))
                    strb.Append(" and  DateCode='" + dateCode + "'");
                strb.Append(" order by DateCode ASC");
                tempList = DBDao.GetListByHQL(strb.ToString());
            }
            return tempList;
        }
        public bool DelATHolidaySettingByIdStr(string idStr)
        {
            bool result = true;
            if (!String.IsNullOrEmpty(idStr))
                result = ((IDATHolidaySettingDao)base.DBDao).DeleteByIdStr(idStr);
            return result;
        }
        /// <summary>
        /// 新增时需要验证是否已经保存在数据库里
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public BaseResultDataValue AddAndValidation(ATHolidaySetting entity)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            IList<ATHolidaySetting> tempList = this.SearchATHolidaySettingByYearAndMonth(entity.Year, entity.Month, entity.DateCode.ToString());
            if (tempList.Count > 0)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + entity.DateCode.ToString() + "已经存在!";
                ZhiFang.Common.Log.Log.Error("节假日新增设置错误:" + entity.DateCode.ToString() + "已经存在!");
            }
            else
            {
                tempBaseResultDataValue.success = this.Add();
            }
            return tempBaseResultDataValue;
        }

        #region 某月工作日天数及节假日天数的计算处理
        /// <summary>
        /// 获取某年的某一月共有多少个工作日天数(工资日)
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public int GetWorkDaysOfOneMonth(int year, int month, string startDay, string endDay)
        {
            int workDays = 0;
            Dictionary<string, int> tempDays = GetWorkDaysAndHolidaysOfOneMonth(year, month, startDay, endDay);
            workDays = tempDays["workDays"];
            return workDays;
        }
        /// <summary>
        /// 获取某年的某一月共有多少个节假日天数(包括星期六星期天)
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public int GetHolidaysOfOneMonth(int year, int month, string startDay, string endDay)
        {
            int holiday = 0;
            Dictionary<string, int> tempDays = GetWorkDaysAndHolidaysOfOneMonth(year, month, startDay, endDay);
            holiday = tempDays["holidays"];
            return holiday;
        }
        /// <summary>
        /// 获取某年的某一月份的默认工作日天数(星期一到星期五的总天数)
        /// 及默认的节假日天数(星期六及星期天的总天数)
        /// </summary>
        /// <param name="pYear"></param>
        /// <param name="pMonth"></param>
        /// <returns></returns>
        public Dictionary<string, int> GetDefaultWorkDaysAndWeekDaysOneMonth(int pYear, int pMonth, string startDay, string endDay)
        {
            int vMax = DateTime.DaysInMonth(pYear, pMonth);
            var vItems = new Dictionary<string, int>();
            string perDate = pYear + "-" + pMonth;
            if (String.IsNullOrEmpty(startDay))
                startDay = "01";
            DateTime dt = Convert.ToDateTime(perDate + "- " + startDay);// 该月份的开始日
            int days = DateTime.DaysInMonth(pYear, pMonth);     // 获得该月总共多少天
            int endDays = days;
            if (String.IsNullOrEmpty(endDay))
                endDays = days;
            else
            {
                endDays = Int32.Parse(endDay);
            }
            //节假日的总天数
            int holidays = 0;
            int startIndex = Int32.Parse(startDay) - 1;

            for (int i = startIndex; i < endDays; i++)
            {
                string tempDate = dt.ToString("MM-dd");
                //固定节假日处理,每年的01-01,05-01,10-01为固定的默认节假日
                if (tempDate == "01-01" || tempDate == "05-01" || tempDate == "10-01")
                {
                    holidays++;
                }
                else
                {
                    // 判断是否为周六，周日，是则记录天数。
                    switch (dt.DayOfWeek)
                    {
                        case DayOfWeek.Saturday:
                            holidays++;
                            break;
                        case DayOfWeek.Sunday:
                            holidays++;
                            break;
                    }
                }
                dt = dt.AddDays(1);
            }
            // 工作日的总天数
            int workDays = days - holidays;

            vItems.Add("workDays", workDays);
            vItems.Add("holidays", holidays);
            return vItems;
        }

        /// <summary>
        /// 获取某年的某一月共有多少个工作日天数和该月有多少个节假日天数
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public Dictionary<string, int> GetWorkDaysAndHolidaysOfOneMonth(int year, int month, string startDay, string endDay)
        {
            int addWorkDays = 0;//需要补加回的工作天数
            int addHolidays = 0;//需要补加回的节假日天数
            int subWorkDays = 0;//需要补减掉的工作天数
            int subHolidays = 0;//需要补减掉的节假日天数
            int workDays = 0;//该月实际的工作日
            int holidays = 0;//该月实际的节假日
            var vItems = new Dictionary<string, int>();
            //先获取该月所有的节假日设置信息
            IList<ATHolidaySetting> holidaySetting = SearchATHolidaySettingByYearAndMonth(year, month, null);

            //获取默认的工作日和默认的节假日
            Dictionary<string, int> tempDefaultDays = GetDefaultWorkDaysAndWeekDaysOneMonth(year, month, startDay, endDay);
            workDays = tempDefaultDays["workDays"];
            holidays = tempDefaultDays["holidays"];

            if (holidaySetting.Count > 0)
            {
                foreach (var holidaySet in holidaySetting)
                {
                    //SettingType：1、工作日(星期一到星期五)改节假日，2节假日改工作日
                    switch (holidaySet.SettingType)
                    {
                        case 1:
                            addHolidays = addHolidays + 1;
                            subWorkDays = subWorkDays + 1;
                            break;
                        case 2:
                            addWorkDays = addWorkDays + 1;
                            subHolidays = subHolidays + 1;
                            break;
                        default:
                            break;
                    }
                }
            }
            //实际的工作日天数=默认的工作日天数-需要补减掉的工作天数+需要补加回的工作天数;
            workDays = workDays - subWorkDays + addWorkDays;

            vItems.Add("workDays", workDays);
            vItems.Add("holidays", holidays);
            return vItems;
        }
        /// <summary>
        /// 获取某年的某一月份的所有工作日期的集合
        /// </summary>
        /// <param name="pYear"></param>
        /// <param name="pMonth"></param>
        /// <returns></returns>
        public Dictionary<string, DayOfWeek> GetAllWorkDaysOfOneMonth(int pYear, int pMonth)
        {
            int vMax = DateTime.DaysInMonth(pYear, pMonth);
            var vItems = new Dictionary<string, DayOfWeek>();
            string perDate = pYear + "-" + pMonth;
            DateTime dt = Convert.ToDateTime(perDate + "- 01");// 月份的第一天
            //先获取该月所有的节假日设置信息
            IList<ATHolidaySetting> holidaySetting = SearchATHolidaySettingByYearAndMonth(pYear, pMonth, null);

            bool isAdd = true;
            for (int i = 1; i <= vMax; i++)
            {
                isAdd = true;
                string tempDate = dt.ToString("MM-dd");
                //考勤设置的节假日改工作日
                if (holidaySetting.Count > 0)
                {
                    var holidaySet = holidaySetting.Where(a => DateTime.Parse(a.DateCode.ToString()).ToString("yyyy-MM-dd") == dt.ToString("yyyy-MM-dd"));
                    if (holidaySet.Count() > 0 && holidaySet.ElementAt(0).SettingType == 1)
                    {
                        //ZhiFang.Common.Log.Log.Debug("考勤设置工作日改节假日:" + dt.ToString("yyyy-MM-dd"));
                        isAdd = false;
                    }
                    else if (holidaySet.Count() > 0 && holidaySet.ElementAt(0).SettingType == 2)
                    {
                        //ZhiFang.Common.Log.Log.Debug("考勤设置节假日改工作日:" + dt.ToString("yyyy-MM-dd"));
                        isAdd = true;
                    }
                    else
                    { // 判断是否为周六，周日
                        switch (dt.DayOfWeek)
                        {
                            case DayOfWeek.Saturday:
                                isAdd = false;
                                break;
                            case DayOfWeek.Sunday:
                                isAdd = false;
                                break;
                            default:
                                isAdd = true;
                                break;
                        }
                    }
                }
                ////固定节假日处理,每年的01-01,05-01,10-01为固定的默认节假日
                else if (tempDate == "01-01" || tempDate == "05-01" || tempDate == "10-01")
                {
                    //ZhiFang.Common.Log.Log.Debug("固定节假日:" + dt.ToString("yyyy-MM-dd"));
                    isAdd = false;
                }
                else
                {
                    // 判断是否为周六，周日
                    switch (dt.DayOfWeek)
                    {
                        case DayOfWeek.Saturday:
                            isAdd = false;
                            break;
                        case DayOfWeek.Sunday:
                            isAdd = false;
                            break;
                        default:
                            isAdd = true;
                            break;
                    }
                }
                if (isAdd == true)
                {
                    vItems.Add(dt.ToString("yyyy-MM-dd"), dt.DayOfWeek);
                    //ZhiFang.Common.Log.Log.Debug("工作日期:" + dt.ToString("yyyy-MM-dd"));
                }
                dt = dt.AddDays(1);
            }
            return vItems;
        }
        /// <summary>
        /// 获取某个日期范围内所有工作日期的集合
        /// </summary>
        /// <param name="pYear"></param>
        /// <param name="pMonth"></param>
        /// <returns></returns>
        public List<string> GetAllWorkDaysOfDates(DateTime dtStart, DateTime dtEnd)
        {
            TimeSpan ts = dtEnd - dtStart;
            int vMax = 0;
            double days = ts.TotalDays;
            //ZhiFang.Common.Log.Log.Debug("TotalDays:" + days);
            if (days > 0 && days < 1)
            {
                days = 1;
            }
            vMax = (int)Math.Round(days);
            var vItems = new List<string>();
            //ZhiFang.Common.Log.Log.Debug("dtEnd:" + dtEnd + ",dtStart:" + dtStart + ",Days:" + vMax);
            DateTime dt = dtStart;
            //先获取该月所有的节假日设置信息
            StringBuilder strb = new StringBuilder();
            strb.Append(" DateCode<='" + dtEnd.ToString("yyyy-MM-dd") + "'" + " and DateCode>='" + dtStart.ToString("yyyy-MM-dd") + "'");
            strb.Append(" order by DateCode ASC");
            IList<ATHolidaySetting> holidaySetting = DBDao.GetListByHQL(strb.ToString());

            bool isAdd = true;
            for (int i = 1; i <= vMax; i++)
            {
                isAdd = true;
                string tempDate = dt.ToString("MM-dd");
                //ZhiFang.Common.Log.Log.Debug("tempDate:" + tempDate);
                //考勤设置的节假日改工作日
                if (holidaySetting.Count > 0)
                {
                    var holidaySet = holidaySetting.Where(a => DateTime.Parse(a.DateCode.ToString()).ToString("yyyy-MM-dd") == dt.ToString("yyyy-MM-dd"));
                    if (holidaySet.Count() > 0 && holidaySet.ElementAt(0).SettingType == 1)
                    {
                        //ZhiFang.Common.Log.Log.Debug("考勤设置工作日改节假日:" + dt.ToString("yyyy-MM-dd"));
                        isAdd = false;
                    }
                    else if (holidaySet.Count() > 0 && holidaySet.ElementAt(0).SettingType == 2)
                    {
                        //ZhiFang.Common.Log.Log.Debug("考勤设置节假日改工作日:" + dt.ToString("yyyy-MM-dd"));
                        isAdd = true;
                    }
                    else
                    { // 判断是否为周六，周日
                        switch (dt.DayOfWeek)
                        {
                            case DayOfWeek.Saturday:
                                isAdd = false;
                                break;
                            case DayOfWeek.Sunday:
                                isAdd = false;
                                break;
                            default:
                                isAdd = true;
                                break;
                        }
                    }
                }
                ////固定节假日处理,每年的01-01,05-01,10-01为固定的默认节假日
                else if (tempDate == "01-01" || tempDate == "05-01" || tempDate == "10-01")
                {
                    //ZhiFang.Common.Log.Log.Debug("固定节假日:" + dt.ToString("yyyy-MM-dd"));
                    isAdd = false;
                }
                else
                {
                    // 判断是否为周六，周日
                    switch (dt.DayOfWeek)
                    {
                        case DayOfWeek.Saturday:
                            isAdd = false;
                            break;
                        case DayOfWeek.Sunday:
                            isAdd = false;
                            break;
                        default:
                            isAdd = true;
                            break;
                    }
                }
                if (isAdd == true)
                {
                    vItems.Add(dt.ToString("yyyy-MM-dd"));
                }
                dt = dt.AddDays(1);
            }
            return vItems;
        }
        /// <summary>
        /// 获取某年的某一月份的所有日期的集合
        /// </summary>
        /// <param name="pYear"></param>
        /// <param name="pMonth"></param>
        /// <returns>Dictionary<string, bool>:bool为true时是工作日</returns>
        public Dictionary<string, bool> GetAllDates(int pYear, int pMonth, string startDay, string endDay)
        {
            int vMax = DateTime.DaysInMonth(pYear, pMonth);
            var vItems = new Dictionary<string, bool>();
            bool isWorkDay = true;
            string perDate = pYear + "-" + pMonth;
            if (String.IsNullOrEmpty(startDay))
                startDay = "01";
            DateTime dt = Convert.ToDateTime(perDate + "- " + startDay);// 该月份的开始日
            int days = DateTime.DaysInMonth(pYear, pMonth);     // 获得该月总共多少天
            int endDays = days;
            if (String.IsNullOrEmpty(endDay))
                endDays = days;
            else
            {
                endDays = Int32.Parse(endDay);
            }
            int startIndex = Int32.Parse(startDay) - 1;
            //先获取该月所有的节假日设置信息
            IList<ATHolidaySetting> holidaySetting = SearchATHolidaySettingByYearAndMonth(pYear, pMonth, null);
            for (int i = startIndex; i < endDays; i++)
            {
                isWorkDay = true;
                string tempDate = dt.ToString("MM-dd");
                //考勤设置的节假日改工作日
                if (holidaySetting.Count > 0)
                {
                    var holidaySet = holidaySetting.Where(a => DateTime.Parse(a.DateCode.ToString()).ToString("yyyy-MM-dd") == dt.ToString("yyyy-MM-dd"));
                    if (holidaySet.Count() > 0 && holidaySet.ElementAt(0).SettingType == 1)
                    {
                        isWorkDay = false;
                    }
                    else if (holidaySet.Count() > 0 && holidaySet.ElementAt(0).SettingType == 2)
                    {
                        isWorkDay = true;
                    }
                    else
                    { // 判断是否为周六，周日
                        switch (dt.DayOfWeek)
                        {
                            case DayOfWeek.Saturday:
                                isWorkDay = false;
                                break;
                            case DayOfWeek.Sunday:
                                isWorkDay = false;
                                break;
                            default:
                                isWorkDay = true;
                                break;
                        }
                    }
                }
                ////固定节假日处理,每年的01-01,05-01,10-01为固定的默认节假日
                else if (tempDate == "01-01" || tempDate == "05-01" || tempDate == "10-01")
                {
                    isWorkDay = false;
                }
                else
                {
                    // 判断是否为周六，周日
                    switch (dt.DayOfWeek)
                    {
                        case DayOfWeek.Saturday:
                            isWorkDay = false;
                            break;
                        case DayOfWeek.Sunday:
                            isWorkDay = false;
                            break;
                        default:
                            isWorkDay = true;
                            break;
                    }
                }
                vItems.Add(dt.ToString("yyyy-MM-dd"), isWorkDay);
                dt = dt.AddDays(1);
            }
            return vItems;
        }
        /// <summary>
        /// 获取某年的某一月份的所有日期集合
        /// </summary>
        /// <param name="pYear"></param>
        /// <param name="pMonth"></param>
        /// <returns></returns>
        public Dictionary<string, DayOfWeek> GetAllDatesOfOneMonth(int pYear, int pMonth)
        {
            int vMax = DateTime.DaysInMonth(pYear, pMonth);
            var vItems = new Dictionary<string, DayOfWeek>();
            string perDate = pYear + "-" + pMonth;
            DateTime dt = Convert.ToDateTime(perDate + "- 01");// 月份的第一天
            for (int i = 1; i < vMax; i++)
            {
                vItems.Add(dt.ToString("yyyy-MM-dd"), dt.DayOfWeek);
                dt = dt.AddDays(1);
            }
            return vItems;
        }

        #endregion
        #region 获取某段日期范围内的所有日期
        /// <summary> 
        /// 获取某段日期范围内的所有日期，以数组形式返回  
        /// </summary>  
        /// <param name="dt1">开始日期</param>  
        /// <param name="dt2">结束日期</param>  
        /// <returns></returns>  
        public List<DateTime> GetAllDays(DateTime dt1, DateTime dt2)
        {
            List<DateTime> listDays = new List<DateTime>();
            DateTime dtDay = new DateTime();
            for (dtDay = dt1; dtDay.CompareTo(dt2) <= 0; dtDay = dtDay.AddDays(1))
            {
                listDays.Add(dtDay);
            }
            return listDays;
        }
        /// <summary>
        /// 获取某段日期范围内的所有日期
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public Dictionary<string, DayOfWeek> GetSomeDatesOfDates(DateTime startDate, DateTime endDate)
        {
            var vItems = new Dictionary<string, DayOfWeek>();
            for (DateTime temp = startDate; temp <= endDate; temp = temp.AddDays(1))
            {
                vItems.Add(temp.ToString("yyyy-MM-dd"), temp.DayOfWeek);
            }
            return vItems;
        }
        #endregion

        #region 判断某个日期是否在某段日期范围内
        /// <summary> 
        /// 判断某个日期是否在某段日期范围内，返回布尔值
        /// </summary> 
        /// <param name="dt">要判断的日期</param> 
        /// <param name="dt1">开始日期</param> 
        /// <param name="dt2">结束日期</param> 
        /// <returns></returns>  
        public bool IsInDate(DateTime dt, DateTime dt1, DateTime dt2)
        {
            return dt.CompareTo(dt1) >= 0 && dt.CompareTo(dt2) <= 0;
        }
        #endregion

        #region 获取某段日期范围内的所有日期
        /// <summary>
        /// 获取某段日期范围内的所有日期，以字符串形式返回 
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public string GetDate(DateTime startDate, DateTime endDate)
        {
            string result = string.Empty;
            for (DateTime temp = startDate; temp <= endDate; temp = temp.AddDays(1))
            {
                if (result == string.Empty)
                {
                    result = temp.ToString("yyyy-MM-dd");
                }
                else
                {
                    result += "," + temp.ToString("yyyy-MM-dd");
                }
            }
            return result;
        }
        #endregion

        /// <summary>
        /// 授权截止日期节假日顺延处理
        /// 并且顺延的工作日是每周的周三
        /// </summary>
        /// <returns></returns>
        public BaseResultDataValue GetLicenceEndDate(string endDate)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            tempBaseResultDataValue.success = true;
            if (String.IsNullOrEmpty(endDate))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ResultDataValue = "{ \"EndDate\":\"" + "\"" + "}";
                tempBaseResultDataValue.ErrorInfo = "授权截止日期为空";
            }
            if (tempBaseResultDataValue.success)
            {
                DateTime dt = Convert.ToDateTime(endDate);
                endDate = dt.ToString("yyyy-MM-dd");
                //传入日期的月份的所有日期
                int curpYear = dt.Year;
                int curpMonth = dt.Month;
                Dictionary<string, bool> curDates = GetAllDates(curpYear, curpMonth, dt.ToString("dd"), null);
                //传入日期是否是工作日并且是周三
                bool isWorkDay = curDates[endDate];
                if (isWorkDay == true && dt.DayOfWeek == DayOfWeek.Wednesday)
                {
                    isWorkDay = true;
                }
                else
                {
                    //是工作日但不是周三
                    isWorkDay = false;
                }
                #region 传入日期不是工作日
                //合并传入日期月的所有日期(从传入日期起)及传入日期下一个月的所有日期集合
                if (isWorkDay == false)
                {
                    //传入日期的下一个月
                    DateTime nextMonth = dt.AddMonths(1);
                    int nextpYear = nextMonth.Year;
                    int nextpMonth = nextMonth.Month;
                    Dictionary<string, bool> nextDates = GetAllDates(nextpYear, nextpMonth, null, null);
                    var query = curDates.Union(nextDates);
                    //Dictionary<string, bool> items2 = curDates.Concat(nextDates).ToDictionary(k => k.Key, v => v.Value);
                    foreach (var item in query)
                    {
                        if (item.Value == true)
                        {
                            DateTime tempDate = Convert.ToDateTime(item.Key);
                            //是工作日并且为周三
                            if (tempDate.DayOfWeek == DayOfWeek.Wednesday)
                            {
                                endDate = item.Key;
                                break;
                            }
                        }
                    }
                }
                #endregion
                tempBaseResultDataValue.ResultDataValue = "{ \"EndDate\":\"" + endDate + "\"" + "}";
            }
            return tempBaseResultDataValue;
        }
    }
}