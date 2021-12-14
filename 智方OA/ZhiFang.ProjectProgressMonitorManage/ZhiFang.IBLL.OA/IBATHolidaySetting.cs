
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.OA;
using ZhiFang.IBLL.Base;


namespace ZhiFang.IBLL.OA
{
    /// <summary>
    ///
    /// </summary>
    public interface IBATHolidaySetting : IBGenericManager<ATHolidaySetting>
    {
        IList<ATHolidaySetting> SearchATHolidaySettingByYearAndMonth(int year, int month, string dateCode);
        bool DelATHolidaySettingByIdStr(string idStr);
        /// <summary>
        /// 新增时需要验证是否已经保存在数据库里
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        BaseResultDataValue AddAndValidation(ATHolidaySetting entity);
        /// <summary>
        /// 获取某年的某一月的日期范围内共有多少个工作日天数(工资日)
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        int GetWorkDaysOfOneMonth(int year, int month, string startDay,string endDay);
        /// <summary>
        /// 获取某年的某一月的日期范围内共有多少个节假日天数(包括星期六星期天)
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        int GetHolidaysOfOneMonth(int year, int month, string startDay, string endDay);
        /// <summary>
        /// 获取某年的某一月份的所有工作日期的集合
        /// </summary>
        /// <param name="pYear"></param>
        /// <param name="pMonth"></param>
        /// <returns></returns>
       Dictionary<string, DayOfWeek> GetAllWorkDaysOfOneMonth(int pYear, int pMonth);
        /// <summary>
        /// 获取某年的某一月份的所有日期集合
        /// </summary>
        /// <param name="pYear"></param>
        /// <param name="pMonth"></param>
        /// <returns></returns>
        Dictionary<string, DayOfWeek> GetAllDatesOfOneMonth(int pYear, int pMonth);
        /// <summary>
        /// 授权截止日期节假日顺延处理
        /// </summary>
        /// <returns></returns>
        BaseResultDataValue GetLicenceEndDate(string endDate);
        /// <summary>
        /// 获取某段日期范围内的所有日期
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
       Dictionary<string, DayOfWeek> GetSomeDatesOfDates(DateTime startDate, DateTime endDate);
        /// <summary>
        /// 获取某个日期范围内所有工作日期的集合
        /// </summary>
        /// <param name="pYear"></param>
        /// <param name="pMonth"></param>
        /// <returns></returns>
        List<string> GetAllWorkDaysOfDates(DateTime dtStart, DateTime dtEnd);
    }
}