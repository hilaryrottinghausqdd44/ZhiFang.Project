using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace ZhiFang.WebLisService.clsCommon
{
    public class GetData
    {
        public GetData()
        {
        }



        /// <summary>
        /// 从DataTable中取指定两个字段的所有数据，返回格式为哈希表
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="keyFieldName">用做哈希表的Key的字段名称</param>
        /// <param name="valueFieldName">用做哈希表的Value的字段名称</param>
        /// <returns>哈希表</returns>
        public static Hashtable getTowFieldDataFromDataTable(DataTable dt, string keyFieldName, string valueFieldName)
        {
            Hashtable returnValue = new Hashtable();
            //遍历DataTable
            for (int row = 0; row < dt.Rows.Count;  row++)
            {
                //取到一条记录
                Hashtable hashRow = GetData.getRowDataFromDataTable(dt, row);
                //判断是否有用做哈希表Key的字段和是否有用做哈希表Value的字段
                if ((hashRow[keyFieldName] != null) && (hashRow[valueFieldName] != null))
                {
                    //有用做哈希表键值的字段，取到其内容
                    string keyFieldValue = hashRow[keyFieldName].ToString();
                    string valueFieldValue = hashRow[valueFieldName].ToString();
                    //保存到哈希表
                    if (returnValue[keyFieldValue] == null)
                        returnValue.Add(keyFieldValue, valueFieldValue);
                }
            }
            return returnValue;
        }


        /// <summary>
        /// 从DataTable中取某一行的数据返回，格式为：字段名称、字段内容
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="rowIndex">行号：从0开始</param>
        /// <returns>哈希表</returns>
        public static Hashtable getRowDataFromDataTable(DataTable dt, int rowIndex)
        {
            Hashtable returnValue = new Hashtable();
            if (dt.Rows.Count > rowIndex)
            {
                for(int col=0;col<dt.Columns.Count;col++)
                {
                    string fieldName = dt.Columns[col].ColumnName;
                    string fieldValue = dt.Rows[rowIndex][col].ToString();
                    if (returnValue[fieldName] == null)
                        returnValue.Add(fieldName, fieldValue);
                }
            }
            return returnValue;
        }


        /// <summary>
        /// 从URL获取AppSystemData参数
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static AppSystemData getAppSystemDataFromURL(string url)
        {
            AppSystemData appSystemData = DataTag.initAppSystemData();
            Hashtable hashParaALL = clsCommon.Tools.getParaFromURL(url);
            if (hashParaALL["para"] != null)
            {
                string para = hashParaALL["para"].ToString();
                //拆分参数
                Hashtable hashPara = clsCommon.Tools.splitParaFromURL(para);
                if (hashPara["systemName"] != null)
                    appSystemData.SystemName = hashPara["systemName"].ToString();
                if (hashPara["dbName"] != null)
                    appSystemData.DataBaseName = hashPara["dbName"].ToString();
                else
                    appSystemData.DataBaseName = clsCommon.TableConfig.getDatabaseNameFromSystemName(appSystemData.SystemName);
                if (hashPara["tableName"] != null)
                    appSystemData.TableName = hashPara["tableName"].ToString();
                else
                    appSystemData.TableName = clsCommon.TableConfig.getFirstTableNameFromSystemName(appSystemData.SystemName);

            }
            //取表名称
            if (appSystemData.TableName == "")
            {
                if (hashParaALL["TableEName"] != null)
                {
                    //表名称
                    appSystemData.TableName = hashParaALL["TableEName"].ToString();
                }
            }
            //取应用系统名称
            if (appSystemData.SystemName == "")
            {
                if (hashParaALL["name"] != null)
                {
                    //应用系统名称
                    appSystemData.SystemName = hashParaALL["name"].ToString();
                    //数据库名称
                    appSystemData.DataBaseName = clsCommon.TableConfig.getDatabaseNameFromSystemName(appSystemData.SystemName);
                    if (appSystemData.TableName == "")
                        appSystemData.TableName = clsCommon.TableConfig.getFirstTableNameFromSystemName(appSystemData.SystemName);
                }
            }
            //取固定查询条件
            if (hashParaALL["UDSQL"] != null)
            {
                appSystemData.WhereSQL = hashParaALL["UDSQL"].ToString();
            }
            //浏览页面动态参数:比如传入的查询条件,修改纪录的主键等,即自定义的查询条件
            string pkSQL = "";
            if (hashParaALL["pkSQL"] != null)
            {
                pkSQL = hashParaALL["pkSQL"].ToString();
                //Page.Response.Write(pkSQL);
            }
            else if (hashParaALL["SQL"] != null)
            {
                pkSQL = hashParaALL["SQL"].ToString();
            }
            appSystemData.ForeignKeySQL = pkSQL;//动态的查询条件
            //样式文件
            if (hashParaALL["style"] != null)
            {
                //取指定的样式
                appSystemData.CSSFile = hashParaALL["style"].ToString();
            }
            else
            {
                //取默认的样式
                appSystemData.CSSFile = TableConfig.getCssFile(appSystemData.SystemName);
            }
            //排序
            if (hashParaALL["OrderBy"] != null)
            {
                appSystemData.OrderBySQL = hashParaALL["OrderBy"].ToString();
            }
            //行数
            string Rows = "";
            if (hashParaALL["Rows"] != null)
            {
                Rows = hashParaALL["Rows"].ToString();
                if (Rows != "")
                {
                    try
                    {
                        appSystemData.PageSize = int.Parse(Rows);
                    }
                    catch
                    {
                    }
                }
            }
            else
            {
                //列数
                string Cols = "";
                if (hashParaALL["Cols"] != null)
                {
                    Cols = hashParaALL["Cols"].ToString();
                    if (Cols != "")
                    {
                        try
                        {
                            appSystemData.PageSize = int.Parse(Cols);
                        }
                        catch
                        {
                        }
                    }
                }
            }
            //分页方式:-1 不分页,0 全部页,1 只要第一页,2\3
            string Pages = "";
            if (hashParaALL["Pages"] != null)
            {
                Pages = hashParaALL["Pages"].ToString();
                if (Pages != "")
                {
                    try
                    {
                        int pageMode = int.Parse(Pages);
                        appSystemData.PageMode = pageMode;
                    }
                    catch
                    {
                    }
                }
            }
            return appSystemData;
        }



        /// <summary>
        /// 获取某年某月的天数
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>该月份的天数</returns>
        public static int getYearMonthDays(int year, int month)
        {
            //算法:取该月份的下一个月份的第一天,然后减去一天,得到该月份的最后一天
            month++;
            if (month == 13)
            {
                year++;
                month = 1;
            }
            //取该年月的下一个月的第一天(1日)
            string date = year.ToString() + "-" + month.ToString() + "-01";
            //转换成日期
            DateTime dt = Convert.ToDateTime(date);
            //取到该月的最后一天(下一个月的第一天的前一天)
            dt = dt.AddDays(-1);
            return dt.Day;
        }



        /// <summary>
        /// 获取某年某月的星期数
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>星期数</returns>
        public static int getYearMonthWeeks(int year, int month)
        {
            //取该年月的第一天(1日)
            string dateFirst = year.ToString() + "-" + month.ToString() + "-01";
            //转换成日期
            DateTime dtFirst = Convert.ToDateTime(dateFirst);
            //取到该年月的第一天是星期几
            int weekFirst = int.Parse(dtFirst.DayOfWeek.ToString("D"));
            //取该月的最后一天
            int lastDay = getYearMonthDays(year, month);
            string dateLast = year.ToString() + "-" + month.ToString() + "-" + lastDay.ToString();
            //转换成日期
            DateTime dtLast = Convert.ToDateTime(dateLast);
            //取到该年月的最后一天是星期几
            int weekLast = int.Parse(dtLast.DayOfWeek.ToString("D"));
            //取该月份的整星期数
            int weeks = lastDay / 7;
            if (weeks * 7 < lastDay)
                weeks++;
            //如果最后一天的星期小于第一天,则星期数加1
            if (weekLast < weekFirst)
                weeks++;
            return weeks;

        }



        /// <summary>
        /// 获取某一天所在的月份的星期数
        /// </summary>
        /// <param name="dt">日期型</param>
        /// <returns>星期数</returns>
        public static int getYearMonthWeeksByDateTime(DateTime dt)
        {
            return GetData.getYearMonthWeeks(dt.Year, dt.Month);
        }



        /// <summary>
        /// 获取某一天所在的月份的星期序号(如,1,2...)
        /// </summary>
        /// <param name="dt">日期型</param>
        /// <returns>所在的星期序号</returns>
        public static int getDayInWeeks(DateTime dt)
        {
            int year = dt.Year;  //年份
            int month = dt.Month;//月份

            //取到日子
            int day = dt.Day;
            //取到当天是星期几
            int weekLast = int.Parse(dt.DayOfWeek.ToString("D"));
            //该月的第一天
            string dateFirst = year.ToString() + "-" + month.ToString() + "-01";
            //转换成日期
            DateTime dtFirst = Convert.ToDateTime(dateFirst);
            //取到该年月的第一天是星期几
            int weekFirst = int.Parse(dtFirst.DayOfWeek.ToString("D"));

            //取该月份的整星期数
            int weeks = day / 7;
            if (weeks * 7 < day)
                weeks++;
            //如果改日期的星期小于第一天,则星期数加1
            if (weekLast < weekFirst)
                weeks++;
            return weeks;

        }


    }
}
