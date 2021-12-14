using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhiFang.IDAO.LIIP;

namespace ZhiFang.DAO.MSSQL.LIIPStatic
{
    public class SCMsgStaticDao : IDSCMsgStaticDao
    {
        public SqlServerHelper DbHelperSQL = new SqlServerHelper();
        public DataTable Static_SCMsg_Confirm_Handle_TimelyPerc(string LabCode, string StartDate, string EndDdate, string SickTypeId, string MsgTypeCodes, int DeptType, string DId)
        {
			/*
			
CREATE PROCEDURE [dbo].[Static_SCMsg_TimelyPerc]
@LabCode  varchar(50),
@StartDate varchar(50),
@EndDate varchar(50),
@DId bigint,
@DeptType int,
@SickTypeId bigint,
@MsgTypeCode varchar(1000)
AS
BEGIN

	declare @sqlstr varchar(5000);
declare @wheresqlstr varchar(5000);
declare @groupsqlstr varchar(500);
set @wheresqlstr=' 1=1 ';
set @groupsqlstr='';
if(@StartDate is not null and @StartDate<>'')
begin
set @wheresqlstr=@wheresqlstr+' and DataAddTime>='''+@StartDate+''' ';
print(@wheresqlstr);
end
if(@EndDate is not null and @EndDate<>'')
begin
set @wheresqlstr=@wheresqlstr+' and DataAddTime<'''+@EndDate+''' ';
print(@wheresqlstr);
end

if(@LabCode is not null and @LabCode<>'')
begin
set @wheresqlstr=@wheresqlstr+' and  RecLabCode in ('+@LabCode+') ';
print(@wheresqlstr);
end

if(@SickTypeId is not null and @SickTypeId>0)
begin
set @wheresqlstr=@wheresqlstr+' and RecSickTypeID ='+Convert(varchar,@SickTypeId);
print(@wheresqlstr);
end

if(@MsgTypeCode is not null and @MsgTypeCode<>''and len(@MsgTypeCode)>0)
begin
set @wheresqlstr=@wheresqlstr+' and MsgTypeCode in ('''+@MsgTypeCode+''')';
print(@wheresqlstr);
end

if(@DeptType is null or @DeptType=0)
begin
if(@DId is not null and @DId>0)
begin
set @wheresqlstr=@wheresqlstr+' and RecDeptID ='+Convert(varchar,@DId);
end
set @groupsqlstr=' SC_Msg.RecDeptID,SC_Msg.RecDeptName ';
print(@wheresqlstr);
set @sqlstr='select 
(select count(*) from SC_Msg as a where a.RecDeptID=SC_Msg.RecDeptID and (a.HandleFlag=0 or a.HandleFlag is null) and '+@wheresqlstr+' ) as UnHandleFlagCount,
(select count(*) from SC_Msg as a where a.RecDeptID=SC_Msg.RecDeptID and a.HandleFlag=1  and '+@wheresqlstr+' ) as HandleFlagCount,
(select count(*) from SC_Msg as a where a.RecDeptID=SC_Msg.RecDeptID and a.ConfirmDateTime<=a.RequireConfirmTime and a.ConfirmDateTime is not null and a.HandleFlag=1 and '+@wheresqlstr+' ) as DelayConfirmCount,
(select count(*) from SC_Msg as a where a.RecDeptID=SC_Msg.RecDeptID and a.ConfirmDateTime>a.RequireConfirmTime and a.ConfirmDateTime is not null and a.HandleFlag=1 and '+@wheresqlstr+') as ConfirmCount,
(select count(*) from SC_Msg as a where a.RecDeptID=SC_Msg.RecDeptID and a.ConfirmDateTime is not null and a.HandleFlag=1 and '+@wheresqlstr+' ) as ConfirmAllCount,
(select count(*) from SC_Msg as a where a.RecDeptID=SC_Msg.RecDeptID and a.HandlingDateTime<=a.RequireHandleTime and a.HandlingDateTime is not null and a.RequireHandleTime is not null and a.HandleFlag=1 and '+@wheresqlstr+') as DelayHandleCount,
(select count(*) from SC_Msg as a where a.RecDeptID=SC_Msg.RecDeptID and a.HandlingDateTime>a.RequireHandleTime and a.HandlingDateTime is not null and a.RequireHandleTime is not null and a.HandleFlag=1 and '+@wheresqlstr+') as HandleCount,
(select count(*) from SC_Msg as a where a.RecDeptID=SC_Msg.RecDeptID and a.HandlingDateTime is not null and a.RequireHandleTime is not null and a.HandleFlag=1 and '+@wheresqlstr+') as HandleAllCount,'+@groupsqlstr+' from SC_Msg
where HandleFlag=1 and '+@wheresqlstr+'
group by '+@groupsqlstr+'
order by '+@groupsqlstr
 exec (@sqlstr);
    print(@sqlstr);
end

if(@DeptType is not null and @DeptType>0)
begin
if(@DId is not null and @DId>0)
begin
set @wheresqlstr=@wheresqlstr+' and RecDistrictID ='+@DId;
end
set @groupsqlstr=' SC_Msg.RecDistrictID,SC_Msg.RecDistrictName ';
print(@wheresqlstr);

set @sqlstr='select 
(select count(*) from SC_Msg as a where a.RecDistrictID=SC_Msg.RecDistrictID and (a.HandleFlag=0 or a.HandleFlag is null) and '+@wheresqlstr+' ) as UnHandleFlagCount,
(select count(*) from SC_Msg as a where a.RecDistrictID=SC_Msg.RecDistrictID and a.HandleFlag=1  and '+@wheresqlstr+' ) as HandleFlagCount,
(select count(*) from SC_Msg as a where a.RecDistrictID=SC_Msg.RecDistrictID and a.ConfirmDateTime<=a.RequireConfirmTime and a.ConfirmDateTime is not null and a.HandleFlag=1 and '+@wheresqlstr+' ) as DelayConfirmCount,
(select count(*) from SC_Msg as a where a.RecDistrictID=SC_Msg.RecDistrictID and a.ConfirmDateTime>a.RequireConfirmTime and a.ConfirmDateTime is not null and a.HandleFlag=1 and '+@wheresqlstr+') as ConfirmCount,
(select count(*) from SC_Msg as a where a.RecDistrictID=SC_Msg.RecDistrictID and a.ConfirmDateTime is not null and a.HandleFlag=1 and '+@wheresqlstr+' ) as ConfirmAllCount,
(select count(*) from SC_Msg as a where a.RecDistrictID=SC_Msg.RecDistrictID and a.HandlingDateTime<=a.RequireHandleTime and a.HandlingDateTime is not null and a.RequireHandleTime is not null and a.HandleFlag=1 and '+@wheresqlstr+') as DelayHandleCount,
(select count(*) from SC_Msg as a where a.RecDistrictID=SC_Msg.RecDistrictID and a.HandlingDateTime>a.RequireHandleTime and a.HandlingDateTime is not null and a.RequireHandleTime is not null and a.HandleFlag=1 and '+@wheresqlstr+') as HandleCount,
(select count(*) from SC_Msg as a where a.RecDistrictID=SC_Msg.RecDistrictID and a.HandlingDateTime is not null and a.RequireHandleTime is not null and a.HandleFlag=1 and '+@wheresqlstr+') as HandleAllCount,
'+@groupsqlstr+' from SC_Msg
where HandleFlag=1 and '+@wheresqlstr+'
group by '+@groupsqlstr+'
order by '+@groupsqlstr
 exec (@sqlstr);
    print(@sqlstr);

end


END
GO
			*/
            List<DbParameter> dbplist = new List<DbParameter>();
            dbplist.Add(new SqlParameter("@LabCode", SqlDbType.VarChar, 50) { Value = LabCode });
            dbplist.Add(new SqlParameter("@StartDate", SqlDbType.VarChar, 50) { Value = StartDate });
            dbplist.Add(new SqlParameter("@EndDate", SqlDbType.VarChar, 50) { Value = EndDdate });
            dbplist.Add(new SqlParameter("@DId", SqlDbType.BigInt, 20) { Value = DId });
            dbplist.Add(new SqlParameter("@DeptType", SqlDbType.Int, 10) { Value = DeptType });
            dbplist.Add(new SqlParameter("@SickTypeId", SqlDbType.BigInt, 20) { Value = SickTypeId });
            dbplist.Add(new SqlParameter("@MsgTypeCode", SqlDbType.VarChar, 1000) { Value = string.IsNullOrEmpty(MsgTypeCodes) ? "" : "" + string.Join("','", MsgTypeCodes.Split(',')) + "" });

            DataSet ds = DbHelperSQL.ExecDataSetStoredProcedure("Static_SCMsg_TimelyPerc", dbplist.ToArray());
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }

        public DataTable Static_SCMsg_Confirm_Handle_TimeRange(string LabCode, string StartDate, string EndDdate, string SickTypeId, string MsgTypeCodes, int DeptType, string DId)
        {
            string strwhere = " HandleFlag=1  and  DataAddTime<=ConfirmDateTime and  DataAddTime<=HandlingDateTime and  ConfirmDateTime<=HandlingDateTime ";
            string strsort = "RecDeptID";
            if (!string.IsNullOrEmpty(LabCode))
            {
                strwhere += $" and RecLabCode='{LabCode}' ";
            }
            if (DateTime.TryParse(StartDate, out var startdate))
            {
                strwhere += $" and DataAddTime>='{startdate.ToString("yyyy-MM-dd HH:mm:ss")}' ";
            }
            if (DateTime.TryParse(EndDdate, out var etartdate))
            {
                strwhere += $" and DataAddTime<='{etartdate.ToString("yyyy-MM-dd HH:mm:ss")}' ";
            }
            if (long.TryParse(SickTypeId, out long sicktypeid) && sicktypeid > 0)
            {
                strwhere += $" and RecSickTypeID={sicktypeid} ";
            }
            if (!string.IsNullOrEmpty(MsgTypeCodes))
            {
                strwhere += $" and MsgTypeCode in ('{string.Join("','", MsgTypeCodes.Split(','))}') ";
            }
            if (DeptType == 0)
            {
                if (long.TryParse(DId, out long did) && did > 0)
                    strwhere += $" and RecDeptID={DId} ";
            }
            else
            {
                if (long.TryParse(DId, out long did) && did > 0)
                    strwhere += $" and RecDistrictID={DId} ";
            }

            string sqlstr = $"select datediff(second, DataAddTime, ConfirmDateTime) as ConfirmDateTimeRange,datediff(second, ConfirmDateTime, HandlingDateTime) as OnfirmHandlingDateTimeRange,datediff(second, DataAddTime, HandlingDateTime) as HandlingDateTimeRange, * from SC_Msg where {strwhere} ";
            DataSet ds = DbHelperSQL.ExecuteDataSet(sqlstr);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable tmpdt = ds.Tables[0];
                DataTable dt = new DataTable();
                dt.Columns.Add("ConfirmDateTimeRangeAvg");
                dt.Columns.Add("ConfirmDateTimeRangeMid");
                dt.Columns.Add("OnfirmHandlingDateTimeRangeAvg");
                dt.Columns.Add("OnfirmHandlingDateTimeRangeMid");
                dt.Columns.Add("HandlingDateTimeRangeAvg");
                dt.Columns.Add("HandlingDateTimeRangeMid");
                dt.Columns.Add("RecDeptID");
                dt.Columns.Add("RecDeptName");
                dt.Columns.Add("RecDistrictID");
                dt.Columns.Add("RecDistrictName");
                var rows = tmpdt.Select(" 1=1 ");
                string didfield = "", dnamefield = "";
                if (DeptType == 0)
                {
                    didfield = "RecDeptID";
                    //dnamefield = "RecDeptName";
                }
                else
                {
                    didfield = "RecDistrictID";
                    // dnamefield = "RecDistrictName";
                }
                var groupkey = rows.GroupBy(a => a[didfield].ToString()).OrderBy(a => a.Key);
                groupkey.ToList().ForEach(a =>
                {
                    DataRow dr = dt.NewRow();

                    #region 确定时长计算
                    var tmprows = tmpdt.Select($" {didfield}={a.Key} ", " ConfirmDateTimeRange ");
                    dr["RecDeptID"] = tmprows[0]["RecDeptID"];
                    dr["RecDeptName"] = tmprows[0]["RecDeptName"];
                    dr["RecDistrictID"] = tmprows[0]["RecDistrictID"];
                    dr["RecDistrictName"] = tmprows[0]["RecDistrictName"];
                    ZhiFang.Common.Log.Log.Debug($"RecDeptID={ dr["RecDeptID"]},RecDeptName={dr["RecDeptName"]},RecDistrictID={dr["RecDistrictID"]},RecDistrictName={ dr["RecDistrictName"]}");
                    float sum = 0;
                    float mid = 0;
                    float avg = 0;
                    if (tmprows.Length > 0)
                    {
                        int mindex = 0;
                        if (tmprows.Length % 2 != 0)
                            mindex = (tmprows.Length + 1) / 2;
                        else
                            mindex = (tmprows.Length) / 2;

                        if (float.TryParse(tmprows.ElementAt(mindex - 1)["ConfirmDateTimeRange"].ToString(), out float tmid))
                            mid = tmid;
                        tmprows.ToList().ForEach(row =>
                        {
                            if (float.TryParse(row["ConfirmDateTimeRange"].ToString(), out float tsum))
                                sum += tsum;
                        });
                        avg = sum / tmprows.Length;
                    }
                    dr["ConfirmDateTimeRangeAvg"] = avg;
                    dr["ConfirmDateTimeRangeMid"] = mid;
                    #endregion

                    #region 处理时长计算
                    tmprows = tmpdt.Select($" {didfield}={a.Key} ", " OnfirmHandlingDateTimeRange ");
                    sum = 0;
                    mid = 0;
                    avg = 0;
                    if (tmprows.Length > 0)
                    {

                        int mindex = 0;
                        if (tmprows.Length % 2 != 0)
                            mindex = (tmprows.Length + 1) / 2;
                        else
                            mindex = (tmprows.Length) / 2;

                        if (float.TryParse(tmprows.ElementAt(mindex - 1)["OnfirmHandlingDateTimeRange"].ToString(), out float tmid))
                            mid = tmid;

                        tmprows.ToList().ForEach(row =>
                        {
                            if (float.TryParse(row["OnfirmHandlingDateTimeRange"].ToString(), out float tsum))
                                sum += tsum;
                        });
                        avg = sum / tmprows.Length;
                    }
                    dr["OnfirmHandlingDateTimeRangeAvg"] = avg;
                    dr["OnfirmHandlingDateTimeRangeMid"] = mid;
                    #endregion

                    #region 整体时长计算
                    tmprows = tmpdt.Select($" {didfield}={a.Key} ", " HandlingDateTimeRange ");
                    sum = 0;
                    mid = 0;
                    avg = 0;
                    if (tmprows.Length > 0)
                    {

                        int mindex = 0;
                        if (tmprows.Length % 2 != 0)
                            mindex = (tmprows.Length + 1) / 2;
                        else
                            mindex = (tmprows.Length) / 2;

                        if (float.TryParse(tmprows.ElementAt(mindex - 1)["HandlingDateTimeRange"].ToString(), out float tmid))
                            mid = tmid;

                        tmprows.ToList().ForEach(row =>
                        {
                            if (float.TryParse(row["HandlingDateTimeRange"].ToString(), out float tsum))
                                sum += tsum;
                        });
                        avg = sum / tmprows.Length;
                    }
                    dr["HandlingDateTimeRangeAvg"] = avg;
                    dr["HandlingDateTimeRangeMid"] = mid;
                    #endregion

                    dt.Rows.Add(dr);
                });
                return dt;
            }
            else
            {
                return null;
            }


        }

        public DataTable Static_SCMsg_AllHandleFinish_YOYTimeRange_Dept(string LabCode, int RangType, int Year, int Quarter, int Month, string SickTypeId, string MsgTypeCodes, string DId, int DataType)
        {
            string strwhere = " HandleFlag=1  ";

            if (long.TryParse(SickTypeId, out long sicktypeid) && sicktypeid > 0)
            {
                strwhere += $" and RecSickTypeID={sicktypeid} ";
            }
            if (!string.IsNullOrEmpty(MsgTypeCodes))
            {
                strwhere += $" and MsgTypeCode in ('{string.Join("','", MsgTypeCodes.Split(','))}') ";
            }
            if (long.TryParse(DId, out long did) && did > 0)
                strwhere += $" and RecDeptID={DId} ";

            if (!string.IsNullOrEmpty(LabCode))
            {
                strwhere += $" and RecLabCode='{LabCode}' ";
            }
            string daterang = "", daterangold = "";
            switch (RangType)
            {
                case 1:
                    daterang = $" and DataAddTime>='{Year}-01-01' and DataAddTime<='{Year}-12-31 23:59:59' ";
                    daterangold = $" and DataAddTime>='{Year - 1}-01-01' and DataAddTime<='{Year - 1}-12-31 23:59:59' ";
                    break;
                case 2:
                    if (Quarter == 1 || Quarter == 4)
                    {
                        daterang = $" and DataAddTime>='{Year}-{(Quarter - 1) * 3 + 1}-01' and DataAddTime<='{Year}-{(Quarter - 1) * 3 + 3}-31 23:59:59' ";
                        daterangold = $" and DataAddTime>='{Year - 1}-{(Quarter - 1) * 3 + 1}-01' and DataAddTime<='{Year - 1}-{(Quarter - 1) * 3 + 3}-31 23:59:59' ";
                    }
                    else
                    {
                        daterang = $" and DataAddTime>='{Year}-{(Quarter - 1) * 3 + 1}-01' and DataAddTime<='{Year}-{(Quarter - 1) * 3 + 3}-30 23:59:59' ";
                        daterangold = $" and DataAddTime>='{Year - 1}-{(Quarter - 1) * 3 + 1}-01' and DataAddTime<='{Year - 1}-{(Quarter - 1) * 3 + 3}-30 23:59:59' ";
                    }
                    break;
                case 3:
                    daterang = $" and DataAddTime>='{Year}-{Month}-01' and DataAddTime<='{Year}-{Month}-{DateTime.DaysInMonth(Year, Month)} 23:59:59' ";
                    daterangold = $" and DataAddTime>='{Year - 1}-{Month}-01' and DataAddTime<='{Year - 1}-{Month}-{DateTime.DaysInMonth(Year, Month)} 23:59:59' ";
                    break;
                default: return null;
            }

            if (DataType == 1)
            {
                string sqlstr = $"select count(*) as AllFinishCount,RecDeptID,RecDeptName from SC_Msg  where {strwhere + daterang} group by RecDeptID,RecDeptName order by RecDeptID";
                string sqlstr1 = $"select count(*) as AllFinishCount ,RecDeptID,RecDeptName from SC_Msg  where {strwhere + daterangold} group by RecDeptID,RecDeptName order by RecDeptID";
                ZhiFang.Common.Log.Log.Debug($"Static_SCMsg_AllHandleFinish_YOYTimeRange_Dept.datatype1.sqlstr={sqlstr}");
                ZhiFang.Common.Log.Log.Debug($"Static_SCMsg_AllHandleFinish_YOYTimeRange_Dept.datatype1.sqlstr1={sqlstr1}");
                DataSet ds = DbHelperSQL.ExecuteDataSet(sqlstr);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("AllFinishCount");
                    dt.Columns.Add("AllFinishCountOld");
                    dt.Columns.Add("YOYPrec");
                    dt.Columns.Add("RecDeptID");
                    dt.Columns.Add("RecDeptName");

                    DataSet dst = DbHelperSQL.ExecuteDataSet(sqlstr1);
                    DataTable tmpdt = ds.Tables[0];
                    for (int i = 0; i < tmpdt.Rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["AllFinishCount"] = tmpdt.Rows[i]["AllFinishCount"].ToString();
                        dr["RecDeptID"] = tmpdt.Rows[i]["RecDeptID"].ToString();
                        dr["RecDeptName"] = tmpdt.Rows[i]["RecDeptName"].ToString();
                        if (dst == null || dst.Tables.Count <= 0 || dst.Tables[0].Rows.Count <= 0)
                        {
                            dr["AllFinishCountOld"] = 0;
                            dr["YOYPrec"] = "";
                        }

                        var drs = dst.Tables[0].Select(" RecDeptID=" + dr["RecDeptID"].ToString());
                        if (drs == null || drs.Length <= 0)
                        {
                            dr["AllFinishCountOld"] = 0;
                            dr["YOYPrec"] = "";
                        }
                        else
                        {
                            dr["AllFinishCountOld"] = drs[0]["AllFinishCount"];
                            dr["YOYPrec"] = Math.Round((decimal.Parse(dr["AllFinishCount"].ToString()) - decimal.Parse(dr["AllFinishCountOld"].ToString())) / decimal.Parse(dr["AllFinishCountOld"].ToString()), 2) * 100;
                        }
                        dt.Rows.Add(dr);
                    }
                    return dt;
                }
                return null;
            }
            else
            {
                string sqlstr = $"select count(*) as AllFinishCount ,(select count(*) from SC_Msg as a  where {strwhere + daterang} and a.HandlingDateTime<=a.RequireAllFinishTime) as FinishCount,RecDeptID,RecDeptName from SC_Msg  where {strwhere + daterang} group by RecDeptID,RecDeptName order by RecDeptID";
                string sqlstr1 = $"select count(*) as AllFinishCount ,(select count(*) from SC_Msg as a  where {strwhere + daterang} and a.HandlingDateTime<=a.RequireAllFinishTime) as FinishCount,RecDeptID,RecDeptName from SC_Msg  where {strwhere + daterangold} group by RecDeptID,RecDeptName order by RecDeptID";
                ZhiFang.Common.Log.Log.Debug($"Static_SCMsg_AllHandleFinish_YOYTimeRange_Dept.datatype2.sqlstr={sqlstr}");
                ZhiFang.Common.Log.Log.Debug($"Static_SCMsg_AllHandleFinish_YOYTimeRange_Dept.datatype2.sqlstr1={sqlstr1}");
                DataSet ds = DbHelperSQL.ExecuteDataSet(sqlstr);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("AllFinishCount");
                    dt.Columns.Add("AllFinishCountOld");
                    dt.Columns.Add("FinishCount");
                    dt.Columns.Add("FinishCountOld");
                    dt.Columns.Add("FinishPrec");
                    dt.Columns.Add("FinishPrecOld");
                    dt.Columns.Add("YOYPrec");
                    dt.Columns.Add("RecDeptID");
                    dt.Columns.Add("RecDeptName");

                    DataSet dst = DbHelperSQL.ExecuteDataSet(sqlstr1);
                    DataTable tmpdt = ds.Tables[0];
                    for (int i = 0; i < tmpdt.Rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["AllFinishCount"] = tmpdt.Rows[i]["AllFinishCount"].ToString();
                        dr["FinishCount"] = tmpdt.Rows[i]["FinishCount"].ToString();
                        if (decimal.TryParse(tmpdt.Rows[i]["FinishCount"].ToString(), out decimal finishcount) && decimal.TryParse(tmpdt.Rows[i]["AllFinishCount"].ToString(), out decimal allfinishcount))
                        {
                            dr["FinishPrec"] = Math.Round(finishcount / allfinishcount, 2) * 100;
                        }
                        dr["RecDeptID"] = tmpdt.Rows[i]["RecDeptID"].ToString();
                        dr["RecDeptName"] = tmpdt.Rows[i]["RecDeptName"].ToString();
                        if (dst == null || dst.Tables.Count <= 0 || dst.Tables[0].Rows.Count <= 0)
                        {
                            dr["FinishPrecOld"] = 0;
                            dr["YOYPrec"] = "";
                        }

                        var drs = dst.Tables[0].Select(" RecDeptID=" + dr["RecDeptID"].ToString());
                        if (drs == null || drs.Length <= 0)
                        {
                            dr["FinishPrecOld"] = 0;
                            dr["YOYPrec"] = "";
                        }
                        else
                        {
                            dr["AllFinishCountOld"] = drs[0]["AllFinishCount"];
                            dr["FinishCountOld"] = drs[0]["FinishCount"];
                            if (decimal.TryParse(drs[0]["FinishCount"].ToString(), out decimal finishcountold) && decimal.TryParse(drs[0]["AllFinishCount"].ToString(), out decimal allfinishcountold))
                            {
                                dr["FinishPrecOld"] = Math.Round(finishcountold / allfinishcountold, 2) * 100;
                            }
                            dr["YOYPrec"] = Math.Round((decimal.Parse(dr["FinishPrec"].ToString()) - decimal.Parse(dr["FinishPrecOld"].ToString())) / decimal.Parse(dr["FinishPrecOld"].ToString()), 2) * 100;
                        }
                        dt.Rows.Add(dr);
                    }
                    return dt;
                }
                return null;
            }
        }

        public DataTable Static_SCMsg_AllHandleFinish_YOYTimeRange_District(string LabCode, int RangType, int Year, int Quarter, int Month, string SickTypeId, string MsgTypeCodes, string DId, int DataType)
        {
            string strwhere = " HandleFlag=1  ";

            if (long.TryParse(SickTypeId, out long sicktypeid) && sicktypeid > 0)
            {
                strwhere += $" and RecSickTypeID={sicktypeid} ";
            }
            if (!string.IsNullOrEmpty(MsgTypeCodes))
            {
                strwhere += $" and MsgTypeCode in ('{string.Join("','", MsgTypeCodes.Split(','))}') ";
            }
            if (long.TryParse(DId, out long did) && did > 0)
                strwhere += $" and RecDistrictID={DId} ";

            if (!string.IsNullOrEmpty(LabCode))
            {
                strwhere += $" and RecLabCode='{LabCode}' ";
            }
            string daterang = "", daterangold = "";
            switch (RangType)
            {
                case 1:
                    daterang = $" and DataAddTime>='{Year}-01-01' and DataAddTime<='{Year}-12-31 23:59:59' ";
                    daterangold = $" and DataAddTime>='{Year - 1}-01-01' and DataAddTime<='{Year - 1}-12-31 23:59:59' ";
                    break;
                case 2:
                    if (Quarter == 1 || Quarter == 4)
                    {
                        daterang = $" and DataAddTime>='{Year}-{(Quarter - 1) * 3 + 1}-01' and DataAddTime<='{Year}-{(Quarter - 1) * 3 + 3}-31 23:59:59' ";
                        daterangold = $" and DataAddTime>='{Year - 1}-{(Quarter - 1) * 3 + 1}-01' and DataAddTime<='{Year - 1}-{(Quarter - 1) * 3 + 3}-31 23:59:59' ";
                    }
                    else
                    {
                        daterang = $" and DataAddTime>='{Year}-{(Quarter - 1) * 3 + 1}-01' and DataAddTime<='{Year}-{(Quarter - 1) * 3 + 3}-30 23:59:59' ";
                        daterangold = $" and DataAddTime>='{Year - 1}-{(Quarter - 1) * 3 + 1}-01' and DataAddTime<='{Year - 1}-{(Quarter - 1) * 3 + 3}-30 23:59:59' ";
                    }
                    break;
                case 3:
                    daterang = $" and DataAddTime>='{Year}-{Month}-01' and DataAddTime<='{Year}-{Month}-{DateTime.DaysInMonth(Year, Month)} 23:59:59' ";
                    daterangold = $" and DataAddTime>='{Year - 1}-{Month}-01' and DataAddTime<='{Year - 1}-{Month}-{DateTime.DaysInMonth(Year, Month)} 23:59:59' ";
                    break;
                default: return null;
            }

            if (DataType == 1)
            {
                string sqlstr = $"select count(*) as AllFinishCount,RecDistrictID,RecDistrictName from SC_Msg  where {strwhere + daterang} group by RecDistrictID,RecDistrictName order by RecDistrictID";
                string sqlstr1 = $"select count(*) as AllFinishCount ,RecDistrictID,RecDistrictName from SC_Msg  where {strwhere + daterangold} group by RecDistrictID,RecDistrictName order by RecDistrictID";
                ZhiFang.Common.Log.Log.Debug($"Static_SCMsg_AllHandleFinish_YOYTimeRange_District.datatype1.sqlstr={sqlstr}");
                ZhiFang.Common.Log.Log.Debug($"Static_SCMsg_AllHandleFinish_YOYTimeRange_District.datatype1.sqlstr1={sqlstr1}");
                DataSet ds = DbHelperSQL.ExecuteDataSet(sqlstr);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("AllFinishCount");
                    dt.Columns.Add("AllFinishCountOld");
                    dt.Columns.Add("YOYPrec");
                    dt.Columns.Add("RecDistrictID");
                    dt.Columns.Add("RecDistrictName");

                    DataSet dst = DbHelperSQL.ExecuteDataSet(sqlstr1);
                    DataTable tmpdt = ds.Tables[0];
                    for (int i = 0; i < tmpdt.Rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["AllFinishCount"] = tmpdt.Rows[i]["AllFinishCount"].ToString();
                        dr["RecDistrictID"] = tmpdt.Rows[i]["RecDistrictID"].ToString();
                        dr["RecDistrictName"] = tmpdt.Rows[i]["RecDistrictName"].ToString();
                        if (dst == null || dst.Tables.Count <= 0 || dst.Tables[0].Rows.Count <= 0)
                        {
                            dr["AllFinishCountOld"] = 0;
                            dr["YOYPrec"] = "";
                        }

                        var drs = dst.Tables[0].Select(" RecDistrictID=" + dr["RecDistrictID"].ToString());
                        if (drs == null || drs.Length <= 0)
                        {
                            dr["AllFinishCountOld"] = 0;
                            dr["YOYPrec"] = "";
                        }
                        else
                        {
                            dr["AllFinishCountOld"] = drs[0]["AllFinishCount"];
                            dr["YOYPrec"] = Math.Round((decimal.Parse(dr["AllFinishCount"].ToString()) - decimal.Parse(dr["AllFinishCountOld"].ToString())) / decimal.Parse(dr["AllFinishCountOld"].ToString()), 2) * 100;
                        }
                        dt.Rows.Add(dr);
                    }
                    return dt;
                }
                return null;
            }
            else
            {
                string sqlstr = $"select count(*) as AllFinishCount ,(select count(*) from SC_Msg as a  where {strwhere + daterang} and a.HandlingDateTime<=a.RequireAllFinishTime) as FinishCount,RecDistrictID,RecDistrictName from SC_Msg  where {strwhere + daterang} group by RecDistrictID,RecDistrictName";
                string sqlstr1 = $"select count(*) as AllFinishCount ,(select count(*) from SC_Msg as a  where {strwhere + daterang} and a.HandlingDateTime<=a.RequireAllFinishTime) as FinishCount,RecDistrictID,RecDistrictName from SC_Msg  where {strwhere + daterangold} group by RecDistrictID,RecDistrictName";
                ZhiFang.Common.Log.Log.Debug($"Static_SCMsg_AllHandleFinish_YOYTimeRange_District.datatype2.sqlstr={sqlstr}");
                ZhiFang.Common.Log.Log.Debug($"Static_SCMsg_AllHandleFinish_YOYTimeRange_District.datatype2.sqlstr1={sqlstr1}");
                DataSet ds = DbHelperSQL.ExecuteDataSet(sqlstr);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("AllFinishCount");
                    dt.Columns.Add("AllFinishCountOld");
                    dt.Columns.Add("FinishCount");
                    dt.Columns.Add("FinishCountOld");
                    dt.Columns.Add("FinishPrec");
                    dt.Columns.Add("FinishPrecOld");
                    dt.Columns.Add("YOYPrec");
                    dt.Columns.Add("RecDistrictID");
                    dt.Columns.Add("RecDistrictName");

                    DataSet dst = DbHelperSQL.ExecuteDataSet(sqlstr1);
                    DataTable tmpdt = ds.Tables[0];
                    for (int i = 0; i < tmpdt.Rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["AllFinishCount"] = tmpdt.Rows[i]["AllFinishCount"].ToString();
                        dr["FinishCount"] = tmpdt.Rows[i]["FinishCount"].ToString();
                        if (decimal.TryParse(tmpdt.Rows[i]["FinishCount"].ToString(), out decimal finishcount) && decimal.TryParse(tmpdt.Rows[i]["AllFinishCount"].ToString(), out decimal allfinishcount))
                        {
                            dr["FinishPrec"] = Math.Round(finishcount / allfinishcount, 2) * 100;
                        }
                        dr["RecDistrictID"] = tmpdt.Rows[i]["RecDistrictID"].ToString();
                        dr["RecDistrictName"] = tmpdt.Rows[i]["RecDistrictName"].ToString();
                        if (dst == null || dst.Tables.Count <= 0 || dst.Tables[0].Rows.Count <= 0)
                        {
                            dr["FinishPrecOld"] = 0;
                            dr["YOYPrec"] = "";
                        }

                        var drs = dst.Tables[0].Select(" RecDistrictID=" + dr["RecDistrictID"].ToString());
                        if (drs == null || drs.Length <= 0)
                        {
                            dr["FinishPrecOld"] = 0;
                            dr["YOYPrec"] = "";
                        }
                        else
                        {
                            dr["AllFinishCountOld"] = drs[0]["AllFinishCount"];
                            dr["FinishCountOld"] = drs[0]["FinishCount"];
                            if (decimal.TryParse(drs[0]["FinishCount"].ToString(), out decimal finishcountold) && decimal.TryParse(drs[0]["AllFinishCount"].ToString(), out decimal allfinishcountold))
                            {
                                dr["FinishPrecOld"] = Math.Round(finishcountold / allfinishcountold, 2) * 100;
                            }
                            dr["YOYPrec"] = Math.Round((decimal.Parse(dr["FinishPrec"].ToString()) - decimal.Parse(dr["FinishPrecOld"].ToString())) / decimal.Parse(dr["FinishPrecOld"].ToString()), 2) * 100;
                        }
                        dt.Rows.Add(dr);
                    }
                    return dt;
                }
                return null;
            }

        }

        public DataTable Static_SCMsg_AllHandleFinish_MOMTimeRange_Dept(string LabCode, int RangType, int Year, int Quarter, int Month, string SickTypeId, string MsgTypeCodes, string DId, int DataType)
        {
            string strwhere = " HandleFlag=1  ";

            if (long.TryParse(SickTypeId, out long sicktypeid) && sicktypeid > 0)
            {
                strwhere += $" and RecSickTypeID={sicktypeid} ";
            }
            if (!string.IsNullOrEmpty(MsgTypeCodes))
            {
                strwhere += $" and MsgTypeCode in ('{string.Join("','", MsgTypeCodes.Split(','))}') ";
            }
            if (long.TryParse(DId, out long did) && did > 0)
                strwhere += $" and RecDeptID={DId} ";

            if (!string.IsNullOrEmpty(LabCode))
            {
                strwhere += $" and RecLabCode='{LabCode}' ";
            }
            string daterang = "", daterangold = "";
            switch (RangType)
            {
                case 1:
                    daterang = $" and DataAddTime>='{Year}-01-01' and DataAddTime<='{Year}-12-31 23:59:59' ";
                    daterangold = $" and DataAddTime>='{Year - 1}-01-01' and DataAddTime<='{Year - 1}-12-31 23:59:59' ";
                    break;
                case 2:
                    if (Quarter == 1)
                    {
                        daterang = $" and DataAddTime>='{Year}-01-01' and DataAddTime<='{Year}-03-31 23:59:59' ";
                        daterangold = $" and DataAddTime>='{Year - 1}-10-01' and DataAddTime<='{Year - 1}-12-31 23:59:59' ";
                    }
                    if (Quarter == 2)
                    {
                        daterang = $" and DataAddTime>='{Year}-04-01' and DataAddTime<='{Year}-06-30 23:59:59' ";
                        daterangold = $" and DataAddTime>='{Year}-01-01' and DataAddTime<='{Year}-03-31 23:59:59' ";
                    }
                    if (Quarter == 3)
                    {
                        daterang = $" and DataAddTime>='{Year}-07-01' and DataAddTime<='{Year}-09-30 23:59:59' ";
                        daterangold = $" and DataAddTime>='{Year}-04-01' and DataAddTime<='{Year}-06-30 23:59:59' ";
                    }
                    if (Quarter == 4)
                    {
                        daterang = $" and DataAddTime>='{Year}-10-01' and DataAddTime<='{Year}-12-31 23:59:59' ";
                        daterangold = $" and DataAddTime>='{Year}-07-01' and DataAddTime<='{Year}-09-30 23:59:59' ";
                    }
                    break;
                case 3:
                    if (Month == 1)
                    {
                        daterang = $" and DataAddTime>='{Year}-{Month}-01' and DataAddTime<='{Year}-{Month}-{DateTime.DaysInMonth(Year, Month)} 23:59:59' ";
                        daterangold = $" and DataAddTime>='{Year - 1}-12-01' and DataAddTime<='{Year - 1}-12-31 23:59:59' ";
                    }
                    else
                    {
                        daterang = $" and DataAddTime>='{Year}-{Month}-01' and DataAddTime<='{Year}-{Month}-{DateTime.DaysInMonth(Year, Month)} 23:59:59' ";
                        daterangold = $" and DataAddTime>='{Year}-{Month - 1}-01' and DataAddTime<='{Year}-{Month - 1}-{DateTime.DaysInMonth(Year, Month - 1)} 23:59:59' ";
                    }
                    break;
                default: return null;
            }

            if (DataType == 1)
            {
                string sqlstr = $"select count(*) as AllFinishCount,RecDeptID,RecDeptName from SC_Msg  where {strwhere + daterang} group by RecDeptID,RecDeptName order by RecDeptID";
                string sqlstr1 = $"select count(*) as AllFinishCount ,RecDeptID,RecDeptName from SC_Msg  where {strwhere + daterangold} group by RecDeptID,RecDeptName order by RecDeptID";
                ZhiFang.Common.Log.Log.Debug($"Static_SCMsg_AllHandleFinish_MOMTimeRange_Dept.datatype1.sqlstr={sqlstr}");
                ZhiFang.Common.Log.Log.Debug($"Static_SCMsg_AllHandleFinish_MOMTimeRange_Dept.datatype1.sqlstr1={sqlstr1}");
                DataSet ds = DbHelperSQL.ExecuteDataSet(sqlstr);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("AllFinishCount");
                    dt.Columns.Add("AllFinishCountOld");
                    dt.Columns.Add("MOMPrec");
                    dt.Columns.Add("RecDeptID");
                    dt.Columns.Add("RecDeptName");

                    DataSet dst = DbHelperSQL.ExecuteDataSet(sqlstr1);
                    DataTable tmpdt = ds.Tables[0];
                    for (int i = 0; i < tmpdt.Rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["AllFinishCount"] = tmpdt.Rows[i]["AllFinishCount"].ToString();
                        dr["RecDeptID"] = tmpdt.Rows[i]["RecDeptID"].ToString();
                        dr["RecDeptName"] = tmpdt.Rows[i]["RecDeptName"].ToString();
                        if (dst == null || dst.Tables.Count <= 0 || dst.Tables[0].Rows.Count <= 0)
                        {
                            dr["AllFinishCountOld"] = 0;
                            dr["MOMPrec"] = "";
                        }

                        var drs = dst.Tables[0].Select(" RecDeptID=" + dr["RecDeptID"].ToString());
                        if (drs == null || drs.Length <= 0)
                        {
                            dr["AllFinishCountOld"] = 0;
                            dr["MOMPrec"] = "";
                        }
                        else
                        {
                            dr["AllFinishCountOld"] = drs[0]["AllFinishCount"];
                            dr["MOMPrec"] = Math.Round((decimal.Parse(dr["AllFinishCount"].ToString()) - decimal.Parse(dr["AllFinishCountOld"].ToString())) / decimal.Parse(dr["AllFinishCountOld"].ToString()), 2) * 100;
                        }
                        dt.Rows.Add(dr);
                    }
                    return dt;
                }
                return null;
            }
            else
            {
                string sqlstr = $"select count(*) as AllFinishCount ,(select count(*) from SC_Msg as a  where {strwhere + daterang} and a.HandlingDateTime<=a.RequireAllFinishTime) as FinishCount,RecDeptID,RecDeptName from SC_Msg  where {strwhere + daterang} group by RecDeptID,RecDeptName order by RecDeptID";
                string sqlstr1 = $"select count(*) as AllFinishCount ,(select count(*) from SC_Msg as a  where {strwhere + daterang} and a.HandlingDateTime<=a.RequireAllFinishTime) as FinishCount,RecDeptID,RecDeptName from SC_Msg  where {strwhere + daterangold} group by RecDeptID,RecDeptName order by RecDeptID";
                ZhiFang.Common.Log.Log.Debug($"Static_SCMsg_AllHandleFinish_MOMTimeRange_Dept.datatype2.sqlstr={sqlstr}");
                ZhiFang.Common.Log.Log.Debug($"Static_SCMsg_AllHandleFinish_MOMTimeRange_Dept.datatype2.sqlstr1={sqlstr1}");
                DataSet ds = DbHelperSQL.ExecuteDataSet(sqlstr);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("AllFinishCount");
                    dt.Columns.Add("FinishCount");
                    dt.Columns.Add("FinishPrec");
                    dt.Columns.Add("FinishPrecOld");
                    dt.Columns.Add("MOMPrec");
                    dt.Columns.Add("RecDeptID");
                    dt.Columns.Add("RecDeptName");

                    DataSet dst = DbHelperSQL.ExecuteDataSet(sqlstr1);
                    DataTable tmpdt = ds.Tables[0];
                    for (int i = 0; i < tmpdt.Rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["AllFinishCount"] = tmpdt.Rows[i]["AllFinishCount"].ToString();
                        dr["FinishCount"] = tmpdt.Rows[i]["FinishCount"].ToString();
                        if (decimal.TryParse(tmpdt.Rows[i]["FinishCount"].ToString(), out decimal finishcount) && decimal.TryParse(tmpdt.Rows[i]["AllFinishCount"].ToString(), out decimal allfinishcount))
                        {
                            dr["FinishPrec"] = Math.Round(finishcount / allfinishcount, 2) * 100;
                        }
                        dr["RecDeptID"] = tmpdt.Rows[i]["RecDeptID"].ToString();
                        dr["RecDeptName"] = tmpdt.Rows[i]["RecDeptName"].ToString();
                        if (dst == null || dst.Tables.Count <= 0 || dst.Tables[0].Rows.Count <= 0)
                        {
                            dr["FinishPrecOld"] = 0;
                            dr["MOMPrec"] = "";
                        }

                        var drs = dst.Tables[0].Select(" RecDeptID=" + dr["RecDeptID"].ToString());
                        if (drs == null || drs.Length <= 0)
                        {
                            dr["FinishPrecOld"] = 0;
                            dr["MOMPrec"] = "";
                        }
                        else
                        {
                            if (decimal.TryParse(drs[0]["FinishCount"].ToString(), out decimal finishcountold) && decimal.TryParse(drs[0]["AllFinishCount"].ToString(), out decimal allfinishcountold))
                            {
                                dr["FinishPrecOld"] = Math.Round(finishcountold / allfinishcountold, 2) * 100;
                            }
                            dr["MOMPrec"] = Math.Round((decimal.Parse(dr["FinishPrec"].ToString()) - decimal.Parse(dr["FinishPrecOld"].ToString())) / decimal.Parse(dr["FinishPrecOld"].ToString()), 2) * 100;
                        }
                        dt.Rows.Add(dr);
                    }
                    return dt;
                }
                return null;
            }
        }

        public DataTable Static_SCMsg_AllHandleFinish_MOMTimeRange_District(string LabCode, int RangType, int Year, int Quarter, int Month, string SickTypeId, string MsgTypeCodes, string DId, int DataType)
        {
            string strwhere = " HandleFlag=1  ";

            if (long.TryParse(SickTypeId, out long sicktypeid) && sicktypeid > 0)
            {
                strwhere += $" and RecSickTypeID={sicktypeid} ";
            }
            if (!string.IsNullOrEmpty(MsgTypeCodes))
            {
                strwhere += $" and MsgTypeCode in ('{string.Join("','", MsgTypeCodes.Split(','))}') ";
            }
            if (long.TryParse(DId, out long did) && did > 0)
                strwhere += $" and RecDistrictID={DId} ";

            if (!string.IsNullOrEmpty(LabCode))
            {
                strwhere += $" and RecLabCode='{LabCode}' ";
            }
            string daterang = "", daterangold = "";
            switch (RangType)
            {
                case 1:
                    daterang = $" and DataAddTime>='{Year}-01-01' and DataAddTime<='{Year}-12-31 23:59:59' ";
                    daterangold = $" and DataAddTime>='{Year - 1}-01-01' and DataAddTime<='{Year - 1}-12-31 23:59:59' ";
                    break;
                case 2:
                    if (Quarter == 1)
                    {
                        daterang = $" and DataAddTime>='{Year}-01-01' and DataAddTime<='{Year}-03-31 23:59:59' ";
                        daterangold = $" and DataAddTime>='{Year - 1}-10-01' and DataAddTime<='{Year - 1}-12-31 23:59:59' ";
                    }
                    if (Quarter == 2)
                    {
                        daterang = $" and DataAddTime>='{Year}-04-01' and DataAddTime<='{Year}-06-30 23:59:59' ";
                        daterangold = $" and DataAddTime>='{Year}-01-01' and DataAddTime<='{Year}-03-31 23:59:59' ";
                    }
                    if (Quarter == 3)
                    {
                        daterang = $" and DataAddTime>='{Year}-07-01' and DataAddTime<='{Year}-09-30 23:59:59' ";
                        daterangold = $" and DataAddTime>='{Year}-04-01' and DataAddTime<='{Year}-06-30 23:59:59' ";
                    }
                    if (Quarter == 4)
                    {
                        daterang = $" and DataAddTime>='{Year}-10-01' and DataAddTime<='{Year}-12-31 23:59:59' ";
                        daterangold = $" and DataAddTime>='{Year}-07-01' and DataAddTime<='{Year}-09-30 23:59:59' ";
                    }
                    break;
                case 3:
                    if (Month == 1)
                    {
                        daterang = $" and DataAddTime>='{Year}-{Month}-01' and DataAddTime<='{Year}-{Month}-{DateTime.DaysInMonth(Year, Month)} 23:59:59' ";
                        daterangold = $" and DataAddTime>='{Year - 1}-12-01' and DataAddTime<='{Year - 1}-12-31 23:59:59' ";
                    }
                    else
                    {
                        daterang = $" and DataAddTime>='{Year}-{Month}-01' and DataAddTime<='{Year}-{Month}-{DateTime.DaysInMonth(Year, Month)} 23:59:59' ";
                        daterangold = $" and DataAddTime>='{Year}-{Month - 1}-01' and DataAddTime<='{Year}-{Month - 1}-{DateTime.DaysInMonth(Year, Month - 1)} 23:59:59' ";
                    }
                    break;
                default: return null;
            }

            if (DataType == 1)
            {
                string sqlstr = $"select count(*) as AllFinishCount,RecDistrictID,RecDistrictName from SC_Msg  where {strwhere + daterang} group by RecDistrictID,RecDistrictName order by RecDistrictID";
                string sqlstr1 = $"select count(*) as AllFinishCount ,RecDistrictID,RecDistrictName from SC_Msg  where {strwhere + daterangold} group by RecDistrictID,RecDistrictName order by RecDistrictID";
                ZhiFang.Common.Log.Log.Debug($"Static_SCMsg_AllHandleFinish_MOMTimeRange_District.datatype1.sqlstr={sqlstr}");
                ZhiFang.Common.Log.Log.Debug($"Static_SCMsg_AllHandleFinish_MOMTimeRange_District.datatype1.sqlstr1={sqlstr1}");
                DataSet ds = DbHelperSQL.ExecuteDataSet(sqlstr);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("AllFinishCount");
                    dt.Columns.Add("AllFinishCountOld");
                    dt.Columns.Add("MOMPrec");
                    dt.Columns.Add("RecDistrictID");
                    dt.Columns.Add("RecDistrictName");

                    DataSet dst = DbHelperSQL.ExecuteDataSet(sqlstr1);
                    DataTable tmpdt = ds.Tables[0];
                    for (int i = 0; i < tmpdt.Rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["AllFinishCount"] = tmpdt.Rows[i]["AllFinishCount"].ToString();
                        dr["RecDistrictID"] = tmpdt.Rows[i]["RecDistrictID"].ToString();
                        dr["RecDistrictName"] = tmpdt.Rows[i]["RecDistrictName"].ToString();
                        if (dst == null || dst.Tables.Count <= 0 || dst.Tables[0].Rows.Count <= 0)
                        {
                            dr["AllFinishCountOld"] = 0;
                            dr["MOMPrec"] = "";
                        }

                        var drs = dst.Tables[0].Select(" RecDistrictID=" + dr["RecDistrictID"].ToString());
                        if (drs == null || drs.Length <= 0)
                        {
                            dr["AllFinishCountOld"] = 0;
                            dr["MOMPrec"] = "";
                        }
                        else
                        {
                            dr["AllFinishCountOld"] = drs[0]["AllFinishCount"];
                            dr["MOMPrec"] = Math.Round((decimal.Parse(dr["AllFinishCount"].ToString()) - decimal.Parse(dr["AllFinishCountOld"].ToString())) / decimal.Parse(dr["AllFinishCountOld"].ToString()), 2) * 100;
                        }
                        dt.Rows.Add(dr);
                    }
                    return dt;
                }
                return null;
            }
            else
            {
                string sqlstr = $"select count(*) as AllFinishCount ,(select count(*) from SC_Msg as a  where {strwhere + daterang} and a.HandlingDateTime<=a.RequireAllFinishTime) as FinishCount,RecDistrictID,RecDistrictName from SC_Msg  where {strwhere + daterang} group by RecDistrictID,RecDistrictName";
                string sqlstr1 = $"select count(*) as AllFinishCount ,(select count(*) from SC_Msg as a  where {strwhere + daterang} and a.HandlingDateTime<=a.RequireAllFinishTime) as FinishCount,RecDistrictID,RecDistrictName from SC_Msg  where {strwhere + daterangold} group by RecDistrictID,RecDistrictName";
                ZhiFang.Common.Log.Log.Debug($"Static_SCMsg_AllHandleFinish_MOMTimeRange_District.datatype2.sqlstr={sqlstr}");
                ZhiFang.Common.Log.Log.Debug($"Static_SCMsg_AllHandleFinish_MOMTimeRange_District.datatype2.sqlstr1={sqlstr1}");
                DataSet ds = DbHelperSQL.ExecuteDataSet(sqlstr);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("AllFinishCount");
                    dt.Columns.Add("FinishCount");
                    dt.Columns.Add("FinishPrec");
                    dt.Columns.Add("FinishPrecOld");
                    dt.Columns.Add("MOMPrec");
                    dt.Columns.Add("RecDistrictID");
                    dt.Columns.Add("RecDistrictName");

                    DataSet dst = DbHelperSQL.ExecuteDataSet(sqlstr1);
                    DataTable tmpdt = ds.Tables[0];
                    for (int i = 0; i < tmpdt.Rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["AllFinishCount"] = tmpdt.Rows[i]["AllFinishCount"].ToString();
                        dr["FinishCount"] = tmpdt.Rows[i]["FinishCount"].ToString();
                        if (decimal.TryParse(tmpdt.Rows[i]["FinishCount"].ToString(), out decimal finishcount) && decimal.TryParse(tmpdt.Rows[i]["AllFinishCount"].ToString(), out decimal allfinishcount))
                        {
                            dr["FinishPrec"] = Math.Round(finishcount / allfinishcount, 2) * 100;
                        }
                        dr["RecDistrictID"] = tmpdt.Rows[i]["RecDistrictID"].ToString();
                        dr["RecDistrictName"] = tmpdt.Rows[i]["RecDistrictName"].ToString();
                        if (dst == null || dst.Tables.Count <= 0 || dst.Tables[0].Rows.Count <= 0)
                        {
                            dr["FinishPrecOld"] = 0;
                            dr["MOMPrec"] = "";
                        }

                        var drs = dst.Tables[0].Select(" RecDistrictID=" + dr["RecDistrictID"].ToString());
                        if (drs == null || drs.Length <= 0)
                        {
                            dr["FinishPrecOld"] = 0;
                            dr["MOMPrec"] = "";
                        }
                        else
                        {
                            if (decimal.TryParse(drs[0]["FinishCount"].ToString(), out decimal finishcountold) && decimal.TryParse(drs[0]["AllFinishCount"].ToString(), out decimal allfinishcountold))
                            {
                                dr["FinishPrecOld"] = Math.Round(finishcountold / allfinishcountold, 2) * 100;
                            }
                            dr["MOMPrec"] = Math.Round((decimal.Parse(dr["FinishPrec"].ToString()) - decimal.Parse(dr["FinishPrecOld"].ToString())) / decimal.Parse(dr["FinishPrecOld"].ToString()), 2) * 100;
                        }
                        dt.Rows.Add(dr);
                    }
                    return dt;
                }
                return null;
            }

        }
    }
}
