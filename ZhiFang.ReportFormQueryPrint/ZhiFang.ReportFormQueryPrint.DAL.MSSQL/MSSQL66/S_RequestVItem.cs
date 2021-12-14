using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.DBUtility;
using System.Data;
using ZhiFang.ReportFormQueryPrint.Model;

namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66
{
    /// <summary>
    /// 数据访问类TestItem。
    /// </summary>
    public class S_RequestVItem : IDS_RequestVItem
    {
        public int Add(Model.S_RequestVItem t)
        {
            throw new NotImplementedException();
        }

        public int Delete(int ItemNo)
        {
            throw new NotImplementedException();
        }

        public bool Exists(int ItemNo)
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(string strWhere, string fields)
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Convert(varchar(10),ReceiveDate,21) as ReceiveDate,SectionNo,TestTypeNo,SampleNo,ParItemNo,ItemNo,OrgValue");
            strSql.Append(",ReportValue,OrgDesc,ReportDesc,ReportText,ReportImage,RefRange,EquipNo,Modified,ItemDate,ItemTime,ResultStatus");
            strSql.Append(",IsPrint,PrintOrder,GraphFile,IsFile,GraphFileName,GraphFileTime,isFileToServer");
            strSql.Append(" FROM S_RequestVItem ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            ZhiFang.Common.Log.Log.Debug("S_RequestVItem.GetList.sql:"+strSql.ToString());
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetList(Model.S_RequestVItem t)
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            throw new NotImplementedException();
        }

        

        public DataSet GetListLike(Model.TestItem model)
        {
            throw new NotImplementedException();
        }

        public int GetMaxId()
        {
            throw new NotImplementedException();
        }

        public Model.TestItem GetModel(int ItemNo)
        {
            throw new NotImplementedException();
        }

        public int Update(Model.S_RequestVItem t)
        {
            throw new NotImplementedException();
        }

        public DataSet GetListByReportPublicationID(string ReportPublicationID)
        {
            string[] p = ReportPublicationID.Split(';');
            if (p.Length >= 4)
            {
                try
                {
                    DataSet ds = this.GetList(" ReceiveDate='" + p[0] + "' and SectionNo=" + p[1] + " and TestTypeNo=" + p[2] + " and SampleNo='" + p[3] + "' ");
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ds.Tables[0].Columns.Add("FilePath");
                        ds.Tables[0].Columns.Add("GraphNo");
                        ds.Tables[0].Columns.Add("GraphName");                        
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            if (ds.Tables[0].Rows[i]["GraphFileName"] != DBNull.Value)
                            {
                                ds.Tables[0].Rows[i]["FilePath"] = @"D:\检验之星图形数据\" + ds.Tables[0].Rows[i]["GraphFileName"].ToString(); 
                                ZhiFang.Common.Log.Log.Debug("S_RequestVItem.GetListByReportPublicationID.GraphFileName:" + ds.Tables[0].Rows[i]["FilePath"].ToString());
                            }
                        }
                        for (int i = ds.Tables[0].Rows.Count - 1; i >= 0; i--)
                        {
                            if (ds.Tables[0].Rows[i]["GraphFileName"] == DBNull.Value)
                            {
                                ds.Tables[0].Rows.RemoveAt(i);
                            }
                        }
                        return ds;
                    }
                    return null;
                }
                catch (Exception e)
                {
                    ZhiFang.Common.Log.Log.Error("MSSQL66.GetListByReportPublicationID,异常：" + e.ToString());
                    return null;
                }
            }
            else
            {
                ZhiFang.Common.Log.Log.Error("MSSQL66.GetListByReportPublicationID,参数错误ReportPublicationID：" + ReportPublicationID);
                return null;
            }
        }
    }
}

