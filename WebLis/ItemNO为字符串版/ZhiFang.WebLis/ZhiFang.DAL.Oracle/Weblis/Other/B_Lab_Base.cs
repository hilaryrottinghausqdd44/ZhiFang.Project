using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL.Other;
using System.Data;

namespace ZhiFang.DAL.Oracle.weblis
{
    public class B_Lab_Base : BaseDALLisDB, IDB_Lab_Base
    {

        public DataSet GetLabNo(string tableName, List<string> labCname, string SourceOrgID, string str)
        {
            DataSet ds = new DataSet();
            try
            {
                string listNames = "";
                string strSql = "";
                for (int i = 0; i < labCname.Count; i++)
                {
                    if (listNames.Trim() == "")
                        listNames = "'" + labCname[i].Trim() + "'";
                    else
                        listNames += "," + "'" + labCname[i].Trim() + "'";
                }
                if (str != "ItemNo")
                {
                    strSql = "select Lab" + str + " from " + tableName + " where LabCode='" + SourceOrgID.Trim() + "' and CName in (" + listNames + ")";
                }
                else
                {
                    strSql = "select " + str + " from " + tableName + " where LabCode='" + SourceOrgID.Trim() + "' and CName in (" + listNames + ")";
                }
                ZhiFang.Common.Log.Log.Info("ZhiFang.DAL.Oracle.weblis.Other.B_Lab_Base.GetLabNo:" + strSql);
                ds = DbHelperSQL.ExecuteDataSet(strSql);
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.Other.B_Lab_Base.GetLabNo异常->", ex);
            }
            return ds;
        }

        //Control
        public DataSet GetCentNo(string tableName, List<string> labNo, string SourceOrgID, string str)
        {
            DataSet ds = new DataSet();
            try
            {
                string strItemNos = "";
                for (int i = 0; i < labNo.Count; i++)
                {
                    if (strItemNos.Trim() == "")
                        strItemNos = "'" + labNo[i].Trim() + "'";
                    else
                        strItemNos += "," + "'" + labNo[i].Trim() + "'";
                }
                string strSql = "select " + str + ",Control" + str + " from " + tableName + " where ControlLabNo='" + SourceOrgID.Trim() + "' and Control" + str + " in (" + strItemNos + ")";
                ZhiFang.Common.Log.Log.Info("ZhiFang.DAL.Oracle.weblis.Other.B_Lab_Base.GetCentNo:" + strSql);
                ds = DbHelperSQL.ExecuteDataSet(strSql);
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.Other.B_Lab_Base.GetCentNo异常->", ex);
            }
            return ds;
        }




        public bool CheckCenterNo(string tableName, List<string> labNo, string SourceOrgID, string str)
        {
            bool b = false;
            try
            {
                string strItemNos = "";
                int count = 0;
                for (int i = 0; i < labNo.Count; i++)
                {
                    if (strItemNos.Trim() == "")
                    {
                        strItemNos = "'" + labNo[i].Trim() + "'";
                        count++;
                    }
                    else
                    {
                        if (!strItemNos.Contains(labNo[i].Trim()))
                        {
                            strItemNos += "," + "'" + labNo[i].Trim() + "'";
                            count++;
                        }
                    }
                }
                string strSql = "select " + str + " from " + tableName + " where ControlLabNo='" + SourceOrgID.Trim() + "' and Control" + str + " in (" + strItemNos + ")";
                ZhiFang.Common.Log.Log.Info("ZhiFang.DAL.Oracle.weblis.Other.B_Lab_Base.CheckCenterNo:" + strSql);
                DataSet ds = DbHelperSQL.ExecuteDataSet(strSql);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count == count)
                        b = true;
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.Other.B_Lab_Base.CheckCenterNo异常->", ex);
            }
            return b;
        }


        public int GetTotalCount(string tableName, string SourceOrgID, string LabNo, string str)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM " + tableName + " where LabCode='" + SourceOrgID + "' and Lab" + str + "=" + LabNo);
            string strCount = DbHelperSQL.ExecuteScalar(strSql.ToString());
            if (strCount != null && strCount.Trim() != "")
            {
                return Convert.ToInt32(strCount.Trim());
            }
            else
            {
                return 0;
            }
        }




        public DataSet GetLabControlNo(string tableName, List<string> CenterNo, string SourceOrgID, string str)
        {
            DataSet ds = new DataSet();
            try
            {
                string strItemNos = "";
                for (int i = 0; i < CenterNo.Count; i++)
                {
                    if (strItemNos.Trim() == "")
                        strItemNos = "'" + CenterNo[i].Trim() + "'";
                    else
                        strItemNos += "," + "'" + CenterNo[i].Trim() + "'";
                }
                string strSql = "select " + str + ",Control" + str + " from " + tableName + " where ControlLabNo='" + SourceOrgID.Trim() + "' and " + str + " in (" + strItemNos + ")";
                ZhiFang.Common.Log.Log.Info("ZhiFang.DAL.Oracle.weblis.Other.B_Lab_Base.GetLabControlNo:" + strSql);
                ds = DbHelperSQL.ExecuteDataSet(strSql);
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.Other.B_Lab_Base.GetLabControlNo异常->", ex);
            }
            return ds;
        }


        public bool CheckLabNo(string tableName, List<string> CenterNo, string SourceOrgID, string str)
        {
            bool b = false;
            try
            {
                string strItemNos = "";
                int count = 0;
                for (int i = 0; i < CenterNo.Count; i++)
                {
                    if (strItemNos.Trim() == "")
                    {
                        strItemNos = "'" + CenterNo[i].Trim() + "'";
                        count++;
                    }
                    else
                    {
                        if (!strItemNos.Contains(CenterNo[i].Trim()))
                        {
                            strItemNos += "," + "'" + CenterNo[i].Trim() + "'";
                            count++;
                        }
                    }
                }
                string strSql = "select * from " + tableName + " where ControlLabNo='" + SourceOrgID.Trim() + "' and " + str + " in (" + strItemNos + ")";
                ZhiFang.Common.Log.Log.Info("ZhiFang.DAL.Oracle.weblis.Other.B_Lab_Base.CheckLabNo:" + strSql);
                DataSet ds = DbHelperSQL.ExecuteDataSet(strSql);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count == count)
                        b = true;
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.Other.B_Lab_Base.CheckLabNo异常->", ex);
            }
            return b;
        }


        public int GetTotalCenterCount(string tableName, string SourceOrgID, string LabNo, string str)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM " + tableName + " where LabCode='" + SourceOrgID + "' and LabSickTypeNo=" + str);
            string strCount = DbHelperSQL.ExecuteScalar(strSql.ToString());
            if (strCount != null && strCount.Trim() != "")
            {
                return Convert.ToInt32(strCount.Trim());
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 返回中心小组编号
        /// </summary>
        /// <param name="tableName">表明</param>
        /// <param name="SourceOrgID">医院</param>
        /// <param name="LabNo">实验室</param>
        /// <returns></returns>
        public string GetControl(string tableName, string SourceOrgID, string LabNo)
        {
            ZhiFang.Common.Log.Log.Info(String.Format("{0}--{1}--{2}", tableName, SourceOrgID, LabNo));
            string no = "";

            string strSql = "select * from  " + tableName + "  where ControlLabNo ='" + SourceOrgID + "' and  ControlSectionNo ='" + LabNo + "'";
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString());
            ZhiFang.Common.Log.Log.Info(ds.Tables[0].Rows.Count.ToString());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ZhiFang.Common.Log.Log.Info("存在SectionNo");
                no = ds.Tables[0].Rows[0]["SectionNo"].ToString();
            }
            
            return no;
        }
        /// <summary>
        /// 传入实验室号返回中心小组
        /// </summary>
        /// <param name="LabCode">实验室</param>
        /// <returns></returns>
        public DataSet GetPGroup(string LabCode)
        {
            ZhiFang.Common.Log.Log.Info(String.Format("{0}", LabCode));
            string strSql = "select SectionNo,CName from  [dbo].[PGroup] where SectionNo in (select SectionNo from [dbo].[B_PGroupControl] where ControlLabNo ='"+ LabCode +"')";
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString());
            ZhiFang.Common.Log.Log.Info(ds.Tables[0].Rows.Count.ToString());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ZhiFang.Common.Log.Log.Info("存在中心编码");
            }
            return ds;
        }
    }
}
