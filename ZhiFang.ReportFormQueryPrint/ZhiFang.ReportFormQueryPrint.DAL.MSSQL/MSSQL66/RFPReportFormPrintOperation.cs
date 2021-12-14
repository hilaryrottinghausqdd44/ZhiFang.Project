using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.DBUtility;
using System.Data.SqlClient;
using System.Data;
using ZhiFang.ReportFormQueryPrint.Model;
using ZhiFang.ReportFormQueryPrint.Common;

namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66
{
    /// <summary>
	/// 数据访问类SampleType。
	/// </summary>
    public class RFPReportFormPrintOperation : IDRFPReportFormPrintOperation
    {
        public RFPReportFormPrintOperation()
        { }
        #region  成员方法
        public int Add(Model.RFPReportFormPrintOperation t)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (t.LabID != null)
            {
                strSql1.Append("LabID ,");
                strSql2.Append("'" + t.LabID + "',");
            }
            if (t.BobjectID != null)
            {
                strSql1.Append("BobjectID ,");
                strSql2.Append("'" + t.BobjectID + "',");
            }
            if (t.ReceiveDate != null)
            {
                strSql1.Append("ReceiveDate ,");
                strSql2.Append("'" + t.ReceiveDate + "',");
            }
            if (t.SectionNo != null)
            {
                strSql1.Append("SectionNo ,");
                strSql2.Append("'" + t.SectionNo + "',");
            }
            if (t.TestTypeNo != null)
            {
                strSql1.Append("TestTypeNo,");
                strSql2.Append("'" + t.TestTypeNo + "',");
            }
            if (t.SampleNo != null && t.SampleNo.Length > 0)
            {
                strSql1.Append("SampleNo,");
                strSql2.Append("'" + t.SampleNo + "',");
            }
            if (t.Station != null)
            {
                strSql1.Append("Station,");
                strSql2.Append(t.Station + ",");
            }
            if (t.EmpID != null)
            {
                strSql1.Append("EmpID,");
                strSql2.Append("'" + t.EmpID + "',");
            }
            if (t.EmpName != null)
            {
                strSql1.Append("EmpName,");
                strSql2.Append("'" + t.EmpName + "',");
            }
            if (t.DeptId != null)
            {
                strSql1.Append("DeptId,");
                strSql2.Append(t.DeptId + ",");
            }
            if (t.DeptName != null)
            {
                strSql1.Append("DeptName,");
                strSql2.Append(t.DeptName + ",");
            }
            if (t.Type != null)
            {
                strSql1.Append("Type,");
                strSql2.Append("'" + t.Type + "',");
            }
            if (t.TypeName != null)
            {
                strSql1.Append("TypeName ,");
                strSql2.Append("'" + t.TypeName + "',");
            }
            if (t.BusinessModuleCode != null)
            {
                strSql1.Append("BusinessModuleCode ,");
                strSql2.Append("'" + t.BusinessModuleCode + "',");
            }
            if (t.Memo != null)
            {
                strSql1.Append("Memo ,");
                strSql2.Append("'" + t.Memo + "',");
            }
            if (t.DispOrder != null)
            {
                strSql1.Append("DispOrder ,");
                strSql2.Append("'" + t.DispOrder + "',");
            }
            if (t.IsUse != null)
            {
                strSql1.Append("IsUse ,");
                strSql2.Append("'" + t.IsUse + "',");
            }
            if (t.CreatorID != null)
            {
                strSql1.Append("CreatorID ,");
                strSql2.Append("'" + t.CreatorID + "',");
            }
            if (t.CreatorName != null)
            {
                strSql1.Append("CreatorName ,");
                strSql2.Append("'" + t.CreatorName + "',");
            }
            strSql1.Append("RFPOperationID,");
            strSql2.Append(GUIDHelp.GetGUIDInt() + ",");
            strSql1.Append("DataAddTime");
            strSql2.Append("'" + DateTime.Now + "'");
            strSql.Append("insert into RFP_ReportFormPrint_Operation(");
            strSql.Append(strSql1.ToString());
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString());
            strSql.Append(")");
            ZhiFang.Common.Log.Log.Debug("RFP_ReportFormPrint_Operation.Add sql = " + strSql.ToString());
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        public DataSet GetList(string strWhere)
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(Model.RFPReportFormPrintOperation t)
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            throw new NotImplementedException();
        }

        public int GetMaxId()
        {
            throw new NotImplementedException();
        }

        public int Update(Model.RFPReportFormPrintOperation t)
        {
            throw new NotImplementedException();
        }

        #endregion  成员方法
    }
}
