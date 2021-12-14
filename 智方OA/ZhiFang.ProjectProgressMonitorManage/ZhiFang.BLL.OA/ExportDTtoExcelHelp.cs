using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using ZhiFang.ProjectProgressMonitorManage.Common;
using System.Reflection;
using ZhiFang.Entity.Base;

namespace ZhiFang.BLL.OA
{
    public class ExportDTtoExcelHelp 
    {
        #region ToDataTable
        public static DataTable ExportExcelOfAllMonthLogToDataTable<T>(IList<T> items)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                Type t = NPOIExportDTtoExcel.GetCoreType(prop.PropertyType);
                string columnName = prop.Name;
                #region DataTable的列转换为导出的中文显示名称
                switch (prop.Name)
                {
                    case "EmpID":
                        columnName = "员工Id";
                        break;
                    case "EmpNo":
                        columnName = "编号";
                        break;
                    case "EmpName":
                        columnName = "姓名";
                        break;
                    case "DeptName":
                        columnName = "部门";
                        break;
                    case "IsFullAttendance":
                        columnName = "是否全勤";
                        break;
                    case "JobLeaveDays":
                        columnName = "事假";
                        break;
                    case "AbsenteeismDays":
                        columnName = "旷工";
                        break;
                    case "FillCardsDays":
                        columnName = "补签打卡";
                        break;
                    case "SignInDays":
                        columnName = "实际签到";
                        break;
                    case "SignOutDays":
                        columnName = "实际签退";
                        break;
                    case "SignInCount":
                        columnName = "总签到次数";
                        break;
                    case "LateCount":
                        columnName = "迟到次数";
                        break;
                    case "SignOutCount":
                        columnName = "总签退次数";
                        break;
                    case "LeaveEarlyCount":
                        columnName = "早退次数";
                        break;

                    case "EntryAbsenceDays":
                        columnName = "入职缺勤";
                        break;
                    case "LeavingAbsenceDays":
                        columnName = "离职缺勤";
                        break;
                    case "SickLeaveDays":
                        columnName = "病假";
                        break;
                    case "MarriageLeaveDays":
                        columnName = "婚假";
                        break;
                    case "MaternityLeaveDays":
                        columnName = "产假";
                        break;
                    case "CareLeaveDays":
                        columnName = "护理假";
                        break;
                    case "BereavementLeaveDays":
                        columnName = "丧假";
                        break;
                    case "AdjustTheBreakDays":
                        columnName = "调休";
                        break;
                    case "AnnualLeaveDays":
                        columnName = "年假";
                        break;

                    case "EgressDays":
                        columnName = "外出";
                        break;
                    case "TripDays":
                        columnName = "出差";
                        break;
                    case "OvertimeDays":
                        columnName = "加班";
                        break;
                    case "TravelHoliday":
                        columnName = "出差存休";
                        break;
                    case "NotPunch":
                        columnName = "未打卡";
                        break;

                    case "DaysOfAbsence":
                        columnName = "缺勤天数";
                        break;
                    case "WagesDays":
                        columnName = "工资日";
                        break;
                    case "CompanyDays":
                        columnName = "公司日";
                        break;
                    default:
                        break;
                }
                #endregion
                if (!String.IsNullOrEmpty(columnName))
                    tb.Columns.Add(columnName, t);
            }

            foreach (T item in items)
            {
                var values = new object[props.Length];

                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }
            if (tb.Columns.Contains("员工Id"))
            {
                tb.Columns.Remove("员工Id");
            }
            if (tb.Columns.Contains("是否全勤"))
            {
                tb.Columns.Remove("是否全勤");
            }
            if (tb.Columns.Contains("总签到次数"))
            {
                tb.Columns.Remove("总签到次数");
            }
            if (tb.Columns.Contains("总签退次数"))
            {
                tb.Columns.Remove("总签退次数");
            }
            if (tb.Columns.Contains("补签打卡"))
            {
                tb.Columns.Remove("补签打卡");
            }
            if (tb != null)
            {
                //排序
                tb.DefaultView.Sort = "部门 asc,姓名 asc";
                tb = tb.DefaultView.ToTable();

                //添加一行求合计
                System.Data.DataRow dataRow = tb.NewRow();
                string columnName = "";
                for (int i = 0; i < tb.Columns.Count - 1; i++)
                {
                    columnName = tb.Columns[i].ToString();
                    if (columnName != "编号" && columnName != "姓名" && columnName != "部门" && columnName != "是否全勤")
                        dataRow[i] = tb.Compute("Sum(" + columnName + ")", "true");
                }
                tb.Rows.Add(dataRow);

            }
            return tb;
        }
        public static DataTable ExportExcelOfATEmpAttendanceEventLogDetailToDataTable<T>(IList<T> items, long? searchType)
        {
            var tb = new DataTable(typeof(T).Name);
            List<string> removeList = new List<string>();
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in props)
            {
                Type t = NPOIExportDTtoExcel.GetCoreType(prop.PropertyType);
                string columnName = prop.Name;
                #region DataTable的列转换为导出的中文显示名称
                switch (prop.Name)
                {
                    case "DataAddTime":
                        columnName = "申请时间";
                        break;
                    case "EmpName":
                        columnName = "姓名";
                        break;
                    case "Account":
                        columnName = "工号";
                        break;
                    case "HRDeptCName":
                        columnName = "部门";
                        break;
                    case "HRPositionCName":
                        columnName = "职务";
                        break;
                    case "StartDateTime":
                        columnName = "开始时间";
                        break;
                    case "EndDateTime":
                        columnName = "结束时间";
                        break;
                    case "Memo":
                        if (searchType == ATTypeId.P请假)
                        {
                            columnName = "请假事由";
                        }
                        else
                        {
                            columnName = "工作内容";
                        }
                        break;
                    case "ApproveStatusName":
                        columnName = "审批状态";
                        break;
                    case "ApproveName":
                        columnName = "审批人";
                        break;
                    case "ApproveDateTime":
                        columnName = "审批时间";
                        break;
                    case "ApproveMemo":
                        columnName = "审批意见";
                        break;
                    case "EvenLength":
                        if (searchType == ATTypeId.P请假)
                        {
                            columnName = "请假天数";
                        }
                        else
                        {
                            columnName = "时长";
                        }
                        break;
                    case "EvenLengthUnit":
                        columnName = "时长单位";
                        break;
                    case "ATEventSubTypeName":
                        columnName = "考勤类型";
                        break;
                    case "EventStatPostion":
                        columnName = "始发地";
                        break;
                    case "EventDestinationPostion":
                        columnName = "目的地";
                        break;
                    case "TransportationName":
                        columnName = "交通工具";
                        break;
                    //case "ATEventDateCode":
                    //    columnName = "事件日期";
                    //    break;
                    default:
                        removeList.Add(columnName);
                        break;
                }
                #endregion
                if (!String.IsNullOrEmpty(columnName))
                    tb.Columns.Add(columnName, t);
            }

            foreach (T item in items)
            {
                var values = new object[props.Length];

                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }
            foreach (var columnName in removeList)
            {
                if (tb.Columns.Contains(columnName))
                {
                    tb.Columns.Remove(columnName);
                }
            }

            if (searchType != ATTypeId.P出差)
            {
                if (tb.Columns.Contains("始发地"))
                {
                    tb.Columns.Remove("始发地");
                }
                if (tb.Columns.Contains("目的地"))
                {
                    tb.Columns.Remove("目的地");
                }
                if (tb.Columns.Contains("交通工具"))
                {
                    tb.Columns.Remove("交通工具");
                }
            }

            if (tb != null)
            {
                //排序
                tb.DefaultView.Sort = "申请时间 desc,部门 asc, 考勤类型 asc";
                tb = tb.DefaultView.ToTable();
            }
            return tb;
        }
        public static DataTable ExportExcelOfATEmpSignInfoDetailToDataTable<T>(IList<T> items)
        {
            var tb = new DataTable(typeof(T).Name);
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            List<string> removeList = new List<string>();
            foreach (PropertyInfo prop in props)
            {
                Type t = NPOIExportDTtoExcel.GetCoreType(prop.PropertyType);
                string columnName = prop.Name;
                #region DataTable的列转换为导出的中文显示名称
                switch (prop.Name)
                {
                    case "ATEventDateCode":
                        columnName = "日期";
                        break;
                    case "WeekInfo":
                        columnName = "星期";
                        break;
                    case "EmpName":
                        columnName = "姓名";
                        break;
                    case "Account":
                        columnName = "工号";
                        break;
                    case "HRDeptCName":
                        columnName = "部门";
                        break;
                    case "HRPositionCName":
                        columnName = "职务";
                        break;
                    case "SignInTime":
                        columnName = "签到时间";
                        break;
                    case "SignInType":
                        columnName = "签到类型";
                        break;
                    case "SigninATEventLogPostionName":
                        columnName = "签到地点";
                        break;
                    case "SignInMemo":
                        columnName = "签到说明";
                        break;
                    //case "SignInIsOffsite":
                    //    columnName = "签到是否脱岗";
                    //    break;
                    case "SignOutTime":
                        columnName = "签退时间";
                        break;
                    case "SignOutType":
                        columnName = "签退类型";
                        break;
                    case "SignoutATEventLogPostionName":
                        columnName = "签退地点";
                        break;
                    case "SignOutMemo":
                        columnName = "签退说明";
                        break;
                    //case "SignOutIsOffsite":
                    //    columnName = "签退是否脱岗";
                    //    break;
                    //case "IsWorkDay":
                    //    columnName = "是否工作日";
                    //    break;
                    case "OtherInfo":
                        columnName = "其他说明";
                        break;
                    default:
                        removeList.Add(columnName);
                        break;
                }
                #endregion
                if (!String.IsNullOrEmpty(columnName))
                    tb.Columns.Add(columnName, t);
            }
            foreach (T item in items)
            {
                var values = new object[props.Length];

                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }
            foreach (var columnName in removeList)
            {
                if (tb.Columns.Contains(columnName))
                {
                    tb.Columns.Remove(columnName);
                }
            }
            if (tb != null)
            {
                tb.Columns["日期"].SetOrdinal(0);
                tb.Columns["星期"].SetOrdinal(1);

                tb.Columns["姓名"].SetOrdinal(2);
                tb.Columns["工号"].SetOrdinal(3);
                tb.Columns["部门"].SetOrdinal(4);
                tb.Columns["职务"].SetOrdinal(5);

                tb.Columns["签到地点"].SetOrdinal(6);
                tb.Columns["签到时间"].SetOrdinal(7);
                tb.Columns["签到类型"].SetOrdinal(8);
                tb.Columns["签到说明"].SetOrdinal(9);

                tb.Columns["签退地点"].SetOrdinal(10);
                tb.Columns["签退时间"].SetOrdinal(11);
                tb.Columns["签退类型"].SetOrdinal(12);
                tb.Columns["签退说明"].SetOrdinal(13);
                //tb.Columns["其他说明"].SetOrdinal(14);
                //排序
                tb.DefaultView.Sort = "日期 desc,部门 asc";
                tb = tb.DefaultView.ToTable();

            }
            return tb;
        }

        #endregion
    }
}
