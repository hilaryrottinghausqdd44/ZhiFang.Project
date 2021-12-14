using Spring.Data.NHibernate.Generic.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using ZhiFang.Entity.RBAC;
using ZhiFang.IDAO.Base;

namespace ZhiFang.DAO.NHB.Base
{
    public class BTDMacroCommandDao : HibernateDaoSupport, IDBTDMacroCommandDao
    {
        #region IDBaseDao<BTDMacroCommand,long> 成员

        public static Dictionary<string, BTDMacroCommand> DicBTDMacroCommand = new Dictionary<string, BTDMacroCommand> {
                {"$YearFirstDay$", new BTDMacroCommand() { Id = 1, LabID = 0, CName = "本年第一天", EName = "YearFirstDay", MacroCode = "$YearFirstDay$", ClassCode = DateTime.Today.Year + "-01" + "-01", TypeName = "日期类型", TypeCode = "DateTime" }},

                {"$YearLastDay$", new BTDMacroCommand() { Id = 2, LabID = 0, CName = "本年最后一天", EName = "YearLastDay", MacroCode = "$YearLastDay$", ClassCode = DateTime.Today.Year + "-12" + "-31", TypeName = "日期类型", TypeCode = "DateTime" }},

                {"$QuarterFirstDay$", new BTDMacroCommand() { Id = 3, LabID = 0, CName = "本季度第一天", EName = "QuarterFirstDay", MacroCode = "$QuarterFirstDay$", ClassCode = DateTime.Now.AddMonths(0 - (DateTime.Now.Month - 1) % 3).AddDays(1 - DateTime.Now.Day).ToShortDateString(), TypeName = "日期类型", TypeCode = "DateTime" }},

                {"$QuarterLastDay$", new BTDMacroCommand() { Id = 4, LabID = 0, CName = "本季度最后一天", EName = "QuarterLastDay", MacroCode = "$QuarterLastDay$", ClassCode = DateTime.Now.AddMonths(0 - (DateTime.Now.Month - 1) % 3).AddDays(1 - DateTime.Now.Day).AddMonths(3).AddDays(-1).ToShortDateString(), TypeName = "日期类型", TypeCode = "DateTime" }},

                {"$MonthFirstDay$", new BTDMacroCommand() { Id = 5, LabID = 0, CName = "本月第一天", EName = "MonthFirstDay", MacroCode = "$MonthFirstDay$", ClassCode = DateTime.Now.AddDays(1 - DateTime.Now.Day).ToShortDateString(), TypeName = "日期类型", TypeCode = "DateTime" }},

                {"$MonthLastDay$", new BTDMacroCommand() { Id = 6, LabID = 0, CName = "本月最后一天", EName = "MonthLastDay", MacroCode = "$MonthLastDay$", ClassCode = DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1).AddDays(-1).ToShortDateString(), TypeName = "日期类型", TypeCode = "DateTime" }},

                {"$WeekFirstDay$", new BTDMacroCommand() { Id = 7, LabID = 0, CName = "本周第一天", EName = "WeekFirstDay", MacroCode = "$WeekFirstDay$", ClassCode = DateTime.Now.AddDays(1 - Convert.ToInt32(DateTime.Now.DayOfWeek.ToString("d"))).ToShortDateString(), TypeName = "日期类型", TypeCode = "DateTime" }},

                {"$WeekLastDay$", new BTDMacroCommand() { Id = 8, LabID = 0, CName = "本周最后一天", EName = "WeekLastDay", MacroCode = "$WeekLastDay$", ClassCode = DateTime.Now.AddDays(1 - Convert.ToInt32(DateTime.Now.DayOfWeek.ToString("d"))).AddDays(6).ToShortDateString(), TypeName = "日期类型", TypeCode = "DateTime" }},

                {"$TheDay:", new BTDMacroCommand() { Id = 9, LabID = 0, CName = "本天", EName = "TheDay", MacroCode = "$TheDay:+0$", ClassCode = DateTime.Today.ToString("yyyy-MM-dd"), TypeName = "日期类型", TypeCode = "DateTimeDynamic" }},

                {"$TheMonth:", new BTDMacroCommand() { Id = 10, LabID = 0, CName = "本月第一天", EName = "TheMonth", MacroCode = "$TheMonth:+0$", ClassCode = DateTime.Now.AddDays(1 - DateTime.Now.Day).ToString("yyyy-MM-dd"), TypeName = "日期类型", TypeCode = "DateTimeDynamic" }},

                {"$TheYear:", new BTDMacroCommand() { Id = 11, LabID = 0, CName = "本年第一天", EName = "TheYear", MacroCode = "$TheYear:+0$", ClassCode = DateTime.Today.Year + "-01" + "-01", TypeName = "日期类型", TypeCode = "DateTimeDynamic" }},

                {"$GetDay:", new BTDMacroCommand() { Id = 12, LabID = 0, CName = "获取日期中的日部分", EName = "GetDay", MacroCode = "$GetDay:P1$", ClassCode = "day(current_date())", TypeName = "数字类型", TypeCode = "IntDynamic" }},

                {"$GetMonth:", new BTDMacroCommand() { Id = 13, LabID = 0, CName = "获取日期中的月部分", EName = "GetMonth", MacroCode = "$GetMonth:P1$", ClassCode = "month(current_date())", TypeName = "数字类型", TypeCode = "IntDynamic" }},

                {"$GetYear:", new BTDMacroCommand() { Id = 14, LabID = 0, CName = "获取日期中的年部分", EName = "GetYear", MacroCode = "$GetYear:P1$", ClassCode = "year(current_date())", TypeName = "数字类型", TypeCode = "IntDynamic" }},

                {"$AddDay:", new BTDMacroCommand() { Id = 15, LabID = 0, CName = "为日期追加天数", EName = "AddDay", MacroCode = "$AddDay:+0,P1$", ClassCode = "current_date()+0", TypeName = "日期类型", TypeCode = "DateTimeDynamic" }},

                {"$AddMonth:", new BTDMacroCommand() { Id = 16, LabID = 0, CName = "为日期追加月数", EName = "AddMonth", MacroCode = "$AddMonth:+0,P1$", ClassCode = "add_months(current_date(),0)", TypeName = "日期类型", TypeCode = "DateTimeDynamic" }},

                {"$UserAccount$", new BTDMacroCommand() { Id = 9, LabID = 0, CName = "当前用户账号", EName = "UserAccount", MacroCode = "$UserAccount$", ClassCode = ZhiFang.Common.Public.Cookie.Get(DicCookieSession.UserAccount), TypeName = "用户身份类型", TypeCode = "StrUserInfo" }},

                {"$UserId$", new BTDMacroCommand() { Id = 9, LabID = 0, CName = "当前用户Id", EName = "UserId", MacroCode = "$UserId$", ClassCode = ZhiFang.Common.Public.Cookie.Get(DicCookieSession.UserID), TypeName = "用户身份类型", TypeCode = "IntUserInfo" }},

                {"$UserDeptId$", new BTDMacroCommand() { Id = 9, LabID = 0, CName = "当前用户所属单位Id", EName = "UserDeptId", MacroCode = "$UserDeptId$", ClassCode = ZhiFang.Common.Public.Cookie.Get(DicCookieSession.HRDeptID), TypeName = "用户身份类型", TypeCode = "IntUserInfo" }},
                };

        public BTDMacroCommandDao()
        {
            //if (DicBTDMacroCommand.Count <= 0)
            //{
            //    #region 日期类型
            //    DicBTDMacroCommand.Add("$YearFirstDay$", new BTDMacroCommand() { Id = 1, LabID = 0, CName = "本年第一天", EName = "YearFirstDay", MacroCode = "$YearFirstDay$", ClassCode = DateTime.Today.Year + "-01" + "-01", TypeName = "日期类型", TypeCode = "DateTime" });

            //    DicBTDMacroCommand.Add("$YearLastDay$", new BTDMacroCommand() { Id = 2, LabID = 0, CName = "本年最后一天", EName = "YearLastDay", MacroCode = "$YearLastDay$", ClassCode = DateTime.Today.Year + "-12" + "-31", TypeName = "日期类型", TypeCode = "DateTime" });

            //    DicBTDMacroCommand.Add("$QuarterFirstDay$", new BTDMacroCommand() { Id = 3, LabID = 0, CName = "本季度第一天", EName = "QuarterFirstDay", MacroCode = "$QuarterFirstDay$", ClassCode = DateTime.Now.AddMonths(0 - (DateTime.Now.Month - 1) % 3).AddDays(1 - DateTime.Now.Day).ToShortDateString(), TypeName = "日期类型", TypeCode = "DateTime" });

            //    DicBTDMacroCommand.Add("$QuarterLastDay$", new BTDMacroCommand() { Id = 4, LabID = 0, CName = "本季度最后一天", EName = "QuarterLastDay", MacroCode = "$QuarterLastDay$", ClassCode = DateTime.Now.AddMonths(0 - (DateTime.Now.Month - 1) % 3).AddDays(1 - DateTime.Now.Day).AddMonths(3).AddDays(-1).ToShortDateString(), TypeName = "日期类型", TypeCode = "DateTime" });

            //    DicBTDMacroCommand.Add("$MonthFirstDay$", new BTDMacroCommand() { Id = 5, LabID = 0, CName = "本月第一天", EName = "MonthFirstDay", MacroCode = "$MonthFirstDay$", ClassCode = DateTime.Now.AddDays(1 - DateTime.Now.Day).ToShortDateString(), TypeName = "日期类型", TypeCode = "DateTime" });

            //    DicBTDMacroCommand.Add("$MonthLastDay$", new BTDMacroCommand() { Id = 6, LabID = 0, CName = "本月最后一天", EName = "MonthLastDay", MacroCode = "$MonthLastDay$", ClassCode = DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1).AddDays(-1).ToShortDateString(), TypeName = "日期类型", TypeCode = "DateTime" });

            //    DicBTDMacroCommand.Add("$WeekFirstDay$", new BTDMacroCommand() { Id = 7, LabID = 0, CName = "本周第一天", EName = "WeekFirstDay", MacroCode = "$WeekFirstDay$", ClassCode = DateTime.Now.AddDays(1 - Convert.ToInt32(DateTime.Now.DayOfWeek.ToString("d"))).ToShortDateString(), TypeName = "日期类型", TypeCode = "DateTime" });

            //    DicBTDMacroCommand.Add("$WeekLastDay$", new BTDMacroCommand() { Id = 8, LabID = 0, CName = "本周最后一天", EName = "WeekLastDay", MacroCode = "$WeekLastDay$", ClassCode = DateTime.Now.AddDays(1 - Convert.ToInt32(DateTime.Now.DayOfWeek.ToString("d"))).AddDays(6).ToShortDateString(), TypeName = "日期类型", TypeCode = "DateTime" });

            //    DicBTDMacroCommand.Add("$TheDay:", new BTDMacroCommand() { Id = 9, LabID = 0, CName = "本天", EName = "TheDay", MacroCode = "$TheDay:+0$", ClassCode = DateTime.Today.ToString("yyyy-MM-dd"), TypeName = "日期类型", TypeCode = "DateTimeDynamic" });

            //    DicBTDMacroCommand.Add("$TheMonth:", new BTDMacroCommand() { Id = 10, LabID = 0, CName = "本月第一天", EName = "TheMonth", MacroCode = "$TheMonth:+0$", ClassCode = DateTime.Now.AddDays(1 - DateTime.Now.Day).ToString("yyyy-MM-dd"), TypeName = "日期类型", TypeCode = "DateTimeDynamic" });

            //    DicBTDMacroCommand.Add("$TheYear:", new BTDMacroCommand() { Id = 11, LabID = 0, CName = "本年第一天", EName = "TheYear", MacroCode = "$TheYear:+0$", ClassCode = DateTime.Today.Year + "-01" + "-01", TypeName = "日期类型", TypeCode = "DateTimeDynamic" });

            //    DicBTDMacroCommand.Add("$GetDay:", new BTDMacroCommand() { Id = 12, LabID = 0, CName = "获取日期中的日部分", EName = "GetDay", MacroCode = "$GetDay:P1$", ClassCode = "day(current_date())", TypeName = "数字类型", TypeCode = "IntDynamic" });

            //    DicBTDMacroCommand.Add("$GetMonth:", new BTDMacroCommand() { Id = 13, LabID = 0, CName = "获取日期中的月部分", EName = "GetMonth", MacroCode = "$GetMonth:P1$", ClassCode = "month(current_date())", TypeName = "数字类型", TypeCode = "IntDynamic" });

            //    DicBTDMacroCommand.Add("$GetYear:", new BTDMacroCommand() { Id = 14, LabID = 0, CName = "获取日期中的年部分", EName = "GetYear", MacroCode = "$GetYear:P1$", ClassCode = "year(current_date())", TypeName = "数字类型", TypeCode = "IntDynamic" });

            //    DicBTDMacroCommand.Add("$AddDay:", new BTDMacroCommand() { Id = 15, LabID = 0, CName = "为日期追加天数", EName = "AddDay", MacroCode = "$AddDay:+0,P1$", ClassCode = "current_date()+0", TypeName = "日期类型", TypeCode = "DateTimeDynamic" });

            //    DicBTDMacroCommand.Add("$AddMonth:", new BTDMacroCommand() { Id = 16, LabID = 0, CName = "为日期追加月数", EName = "AddMonth", MacroCode = "$AddMonth:+0,P1$", ClassCode = "add_months(current_date(),0)", TypeName = "日期类型", TypeCode = "DateTimeDynamic" });
            //    //DicBTDMacroCommand.Add("$GetYear:", new BTDMacroCommand() { Id = 12, LabID = 0, CName = "本天在当月第几天", EName = "GetYear", MacroCode = "$GetYear:+0$", ClassCode = DateTime.Today.Day.ToString(), TypeName = "数字类型", TypeCode = "DateTimeDynamic" });
            //    #endregion
            //    #region 用户身份类型
            //    DicBTDMacroCommand.Add("$UserAccount$", new BTDMacroCommand() { Id = 9, LabID = 0, CName = "当前用户账号", EName = "UserAccount", MacroCode = "$UserAccount$", ClassCode = ZhiFang.Common.Public.Cookie.Get(DicCookieSession.UserAccount), TypeName = "用户身份类型", TypeCode = "StrUserInfo" });

            //    DicBTDMacroCommand.Add("$UserId$", new BTDMacroCommand() { Id = 9, LabID = 0, CName = "当前用户Id", EName = "UserId", MacroCode = "$UserId$", ClassCode = ZhiFang.Common.Public.Cookie.Get(DicCookieSession.UserID), TypeName = "用户身份类型", TypeCode = "IntUserInfo" });

            //    DicBTDMacroCommand.Add("$UserDeptId$", new BTDMacroCommand() { Id = 9, LabID = 0, CName = "当前用户所属单位Id", EName = "UserDeptId", MacroCode = "$UserDeptId$", ClassCode = ZhiFang.Common.Public.Cookie.Get(DicCookieSession.HRDeptID), TypeName = "用户身份类型", TypeCode = "IntUserInfo" });
            //    #endregion
            //}
        }

        public int GetTotalCount()
        {
            return DicBTDMacroCommand.Count;
        }

        public Dictionary<string, BTDMacroCommand> GetObjects()
        {
            return DicBTDMacroCommand;
        }

        public BTDMacroCommand Get(string key)
        {
            var tmp = DicBTDMacroCommand.Where(a => a.Key == key);
            if (tmp != null && tmp.Count() > 0)
            {
                return tmp.ElementAt(0).Value;
            }
            else
            {
                return null;
            }
        }

        public BTDMacroCommand Load(string key)
        {
            try
            {
                return DicBTDMacroCommand[key];
            }
            catch
            {
                return null;
            }
        }

        public bool Save(string key, BTDMacroCommand value)
        {
            try
            {
                DicBTDMacroCommand.Add(key, value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(string key, BTDMacroCommand value)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string key)
        {
            try
            {
                DicBTDMacroCommand.Remove(key);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}