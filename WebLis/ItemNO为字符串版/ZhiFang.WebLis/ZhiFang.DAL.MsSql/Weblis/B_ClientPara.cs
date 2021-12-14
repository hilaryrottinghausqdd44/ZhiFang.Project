using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;
using ZhiFang.Model;

namespace ZhiFang.DAL.MsSql.Weblis
{
    public class B_ClientPara : BaseDALLisDB,IDBClientPara
    {
        public B_ClientPara(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public B_ClientPara()
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
        }

        // <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.B_ClientPara model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.LabID != null)
            {
                strSql1.Append("LabID,");
                strSql2.Append("" + model.LabID + ",");
            }
            if (model.ParameterID != null)
            {
                strSql1.Append("ParameterID,");
                strSql2.Append("" + model.ParameterID + ",");
            }
            if (model.PDictId != null)
            {
                strSql1.Append("PDictId,");
                strSql2.Append("" + model.PDictId + ",");
            }
            if (model.Name != null)
            {
                strSql1.Append("Name,");
                strSql2.Append("'" + model.Name + "',");
            }
            if (model.SName != null)
            {
                strSql1.Append("SName,");
                strSql2.Append("'" + model.SName + "',");
            }
            if (model.ParaType != null)
            {
                strSql1.Append("ParaType,");
                strSql2.Append("'" + model.ParaType + "',");
            }
            if (model.ParaNo != null)
            {
                strSql1.Append("ParaNo,");
                strSql2.Append("'" + model.ParaNo + "',");
            }
            if (model.ParaValue != null)
            {
                strSql1.Append("ParaValue,");
                strSql2.Append("'" + model.ParaValue + "',");
            }
            if (model.ParaDesc != null)
            {
                strSql1.Append("ParaDesc,");
                strSql2.Append("'" + model.ParaDesc + "',");
            }
            if (model.Shortcode != null)
            {
                strSql1.Append("Shortcode,");
                strSql2.Append("'" + model.Shortcode + "',");
            }
            if (model.DispOrder != null)
            {
                strSql1.Append("DispOrder,");
                strSql2.Append("" + model.DispOrder + ",");
            }
            if (model.PinYinZiTou != null)
            {
                strSql1.Append("PinYinZiTou,");
                strSql2.Append("'" + model.PinYinZiTou + "',");
            }
            if (model.IsUse != null)
            {
                strSql1.Append("IsUse,");
                strSql2.Append("" + (model.IsUse ? 1 : 0) + ",");
            }
            if (model.IsUserSet != null)
            {
                strSql1.Append("IsUserSet,");
                strSql2.Append("" + (model.IsUserSet ? 1 : 0) + ",");
            }
            if (model.DataAddTime != null)
            {
                strSql1.Append("DataAddTime,");
                strSql2.Append("'" + model.DataAddTime + "',");
            }
            if (model.DataUpdateTime != null)
            {
                strSql1.Append("DataUpdateTime,");
                strSql2.Append("'" + model.DataUpdateTime + "',");
            }
            if (model.DataTimeStamp != null)
            {
                strSql1.Append("DataTimeStamp,");
                strSql2.Append("" + model.DataTimeStamp + ",");
            }
            strSql.Append("insert into B_ClientPara(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            int rows = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            return rows;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Model.B_ClientPara model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update B_ClientPara set ");
            if (model.LabID != null)
            {
                strSql.Append("LabID=" + model.LabID + ",");
            }
            if (model.PDictId != null)
            {
                strSql.Append("PDictId=" + model.PDictId + ",");
            }
            else
            {
                strSql.Append("PDictId= null ,");
            }
            if (model.Name != null)
            {
                strSql.Append("Name='" + model.Name + "',");
            }
            else
            {
                strSql.Append("Name= null ,");
            }
            if (model.SName != null)
            {
                strSql.Append("SName='" + model.SName + "',");
            }
            else
            {
                strSql.Append("SName= null ,");
            }
            if (model.ParaType != null)
            {
                strSql.Append("ParaType='" + model.ParaType + "',");
            }
            else
            {
                strSql.Append("ParaType= null ,");
            }
            if (model.ParaNo != null)
            {
                strSql.Append("ParaNo='" + model.ParaNo + "',");
            }
            else
            {
                strSql.Append("ParaNo= null ,");
            }
            if (model.ParaValue != null)
            {
                strSql.Append("ParaValue='" + model.ParaValue + "',");
            }
            else
            {
                strSql.Append("ParaValue= null ,");
            }
            if (model.ParaDesc != null)
            {
                strSql.Append("ParaDesc='" + model.ParaDesc + "',");
            }
            else
            {
                strSql.Append("ParaDesc= null ,");
            }
            if (model.Shortcode != null)
            {
                strSql.Append("Shortcode='" + model.Shortcode + "',");
            }
            else
            {
                strSql.Append("Shortcode= null ,");
            }
            if (model.DispOrder != null)
            {
                strSql.Append("DispOrder=" + model.DispOrder + ",");
            }
            else
            {
                strSql.Append("DispOrder= null ,");
            }
            if (model.PinYinZiTou != null)
            {
                strSql.Append("PinYinZiTou='" + model.PinYinZiTou + "',");
            }
            else
            {
                strSql.Append("PinYinZiTou= null ,");
            }
            if (model.IsUse != null)
            {
                strSql.Append("IsUse=" + (model.IsUse ? 1 : 0) + ",");
            }
            else
            {
                strSql.Append("IsUse= null ,");
            }
            if (model.IsUserSet != null)
            {
                strSql.Append("IsUserSet=" + (model.IsUserSet ? 1 : 0) + ",");
            }
            else
            {
                strSql.Append("IsUserSet= null ,");
            }
            if (model.DataAddTime != null)
            {
                strSql.Append("DataAddTime='" + model.DataAddTime + "',");
            }
            else
            {
                strSql.Append("DataAddTime= null ,");
            }
            if (model.DataUpdateTime != null)
            {
                strSql.Append("DataUpdateTime='" + model.DataUpdateTime + "',");
            }
            else
            {
                strSql.Append("DataUpdateTime= null ,");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where ParameterID=" + model.ParameterID + " ");
            int rowsAffected = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            return rowsAffected;
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select LabID,ParameterID,PDictId,Name,SName,ParaType,ParaNo,ParaValue,ParaDesc,Shortcode,DispOrder,PinYinZiTou,IsUse,IsUserSet,DataAddTime,DataUpdateTime,DataTimeStamp ");
            strSql.Append(" FROM B_ClientPara ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" LabID,ParameterID,PDictId,Name,SName,ParaType,ParaNo,ParaValue,ParaDesc,Shortcode,DispOrder,PinYinZiTou,IsUse,IsUserSet,DataAddTime,DataUpdateTime,DataTimeStamp ");
            strSql.Append(" FROM B_ClientPara ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.ParameterID desc");
            }
            strSql.Append(")AS Row, T.*  from B_ClientPara T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }       

        public int Delete(long ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from B_ClientPara ");
            strSql.Append(" where ParameterID=" + ID + " ");
            int rowsAffected = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            return rowsAffected;
        }

        public int Delete(long clientNo, string paraCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from B_ClientPara ");
            strSql.Append(" where LabID=" + clientNo + " and ParaNo='" + paraCode + "' ");
            int rowsAffected = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            return rowsAffected;
        }

        public DataSet SearchGroupByParaNo(string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Name,ParaNo");
            strSql.Append(" FROM B_ClientPara  ");
            if (where.Trim() != "")
            {
                strSql.Append(" where " + where);
            }
            strSql.Append(" group by ParaNo,Name  ");
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public List<Model.B_ClientPara> SearchByParaNo(string paraNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM B_ClientPara  ");
            if (paraNo.Trim() != "")
            {
                strSql.Append(" where paraNo='" + paraNo + "' ");
            }
            DataSet ds= DbHelperSQL.ExecuteDataSet(strSql.ToString());
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                return DataTableToList(ds.Tables[0]);
            }
            return null;
        }

        public List<Model.B_ClientPara> SearchBBClientParaGroupByName(string name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Name,ParaNo");
            strSql.Append(" FROM B_ClientPara where 1=1 ");
            if (name.Trim() != "")
            {
                strSql.Append(" and Name like '%" + name + "%' ");
            }
            strSql.Append(" group by ParaNo,Name  ");
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString());
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                return DataTableToList(ds.Tables[0]);
            }
            return null;
        }

        public List<Model.B_ClientPara> SearchBBClientParaByParaNoAndLabIDAndLabName(string paraNo, string labID, string labName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select dbo.B_ClientPara.*, dbo.CLIENTELE.* ");
            strSql.Append(" FROM  dbo.B_ClientPara INNER JOIN  dbo.CLIENTELE ON dbo.B_ClientPara.LabID = dbo.CLIENTELE.ClIENTNO  where 1=1 ");
            if (paraNo!=null && paraNo.Trim() != "")
            {
                strSql.Append(" and paraNo='" + paraNo + "' ");
            }
            if (labID!=null && labID.Trim() != "")
            {
                strSql.Append(" and labID=" + labID + " ");
            }
            if (labName!=null && labName.Trim() != "")
            {
                strSql.Append(" and CLIENTELE.CName like '%" + labName + "%' ");
            }
            strSql.Append(" order by DataAddTime desc  ");
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString());
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                return DataTableToList(ds.Tables[0]);
            }
            return null;
        }

        public int AddByDataRow(DataRow dr)
        {
            throw new NotImplementedException();
        }
        

        public bool Exists(long ID)
        {
            throw new NotImplementedException();
        }

        public DataSet GetListLike(Model.B_ClientPara model)
        {
            throw new NotImplementedException();
        }

        public Model.B_ClientPara GetModel(long ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(" LabID,ParameterID,PDictId,Name,SName,ParaType,ParaNo,ParaValue,ParaDesc,Shortcode,DispOrder,PinYinZiTou,IsUse,IsUserSet,DataAddTime,DataUpdateTime,DataTimeStamp ");
            strSql.Append(" from B_ClientPara ");
            strSql.Append(" where ParameterID=" + ID + " ");
            Model.B_ClientPara model = new Model.B_ClientPara();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }
        public Model.B_ClientPara Search(long ClientNo,string ParaCode)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 LabID,ParameterID,PDictId,Name,SName,ParaType,ParaNo,ParaValue,ParaDesc,Shortcode,DispOrder,PinYinZiTou,IsUse,IsUserSet,DataAddTime,DataUpdateTime,DataTimeStamp from B_ClientPara ");
            strSql.Append(" where LabId=" + ClientNo + " and ParaNo='" + ParaCode + "'");

            Model.B_ClientPara model = new Model.B_ClientPara();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        public int UpdateByDataRow(DataRow dr)
        {
            throw new NotImplementedException();
        }
        /// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Model.B_ClientPara DataRowToModel(DataRow row)
        {
            Model.B_ClientPara model = new Model.B_ClientPara();
            if (row != null)
            {
                if (row.Table.Columns.Contains("LabID") && row["LabID"] != null && row["LabID"].ToString() != "")
                {
                    model.LabID = long.Parse(row["LabID"].ToString());
                }
                if (row.Table.Columns.Contains("CNAME") && row["CNAME"] != null && row["CNAME"].ToString() != "")
                {
                    model.LabName = row["CNAME"].ToString();
                }
                if (row.Table.Columns.Contains("ParameterID") && row["ParameterID"] != null && row["ParameterID"].ToString() != "")
                {
                    model.ParameterID = long.Parse(row["ParameterID"].ToString());
                }
                if (row.Table.Columns.Contains("PDictId") && row["PDictId"] != null && row["PDictId"].ToString() != "")
                {
                    model.PDictId = long.Parse(row["PDictId"].ToString());
                }
                if (row.Table.Columns.Contains("Name") && row["Name"] != null)
                {
                    model.Name = row["Name"].ToString();
                }
                if (row.Table.Columns.Contains("SName") && row["SName"] != null)
                {
                    model.SName = row["SName"].ToString();
                }
                if (row.Table.Columns.Contains("ParaType") && row["ParaType"] != null)
                {
                    model.ParaType = row["ParaType"].ToString();
                }
                if (row.Table.Columns.Contains("ParaNo") && row["ParaNo"] != null)
                {
                    model.ParaNo = row["ParaNo"].ToString();
                }
                if (row.Table.Columns.Contains("ParaValue") && row["ParaValue"] != null)
                {
                    model.ParaValue = row["ParaValue"].ToString();
                }
                if (row.Table.Columns.Contains("ParaDesc") && row["ParaDesc"] != null)
                {
                    model.ParaDesc = row["ParaDesc"].ToString();
                }
                if (row.Table.Columns.Contains("Shortcode") && row["Shortcode"] != null)
                {
                    model.Shortcode = row["Shortcode"].ToString();
                }
                if (row.Table.Columns.Contains("DispOrder") && row["DispOrder"] != null && row["DispOrder"].ToString() != "")
                {
                    model.DispOrder = int.Parse(row["DispOrder"].ToString());
                }
                if (row.Table.Columns.Contains("PinYinZiTou") && row["PinYinZiTou"] != null)
                {
                    model.PinYinZiTou = row["PinYinZiTou"].ToString();
                }
                if (row.Table.Columns.Contains("IsUse") && row["IsUse"] != null && row["IsUse"].ToString() != "")
                {
                    if ((row["IsUse"].ToString() == "1") || (row["IsUse"].ToString().ToLower() == "true"))
                    {
                        model.IsUse = true;
                    }
                    else
                    {
                        model.IsUse = false;
                    }
                }
                if (row.Table.Columns.Contains("IsUserSet") && row["IsUserSet"] != null && row["IsUserSet"].ToString() != "")
                {
                    if ((row["IsUserSet"].ToString() == "1") || (row["IsUserSet"].ToString().ToLower() == "true"))
                    {
                        model.IsUserSet = true;
                    }
                    else
                    {
                        model.IsUserSet = false;
                    }
                }
                if (row.Table.Columns.Contains("DataAddTime") && row["DataAddTime"] != null && row["DataAddTime"].ToString() != "")
                {
                    model.DataAddTime = DateTime.Parse(row["DataAddTime"].ToString());
                }
                if (row.Table.Columns.Contains("DataUpdateTime") && row["DataUpdateTime"] != null && row["DataUpdateTime"].ToString() != "")
                {
                    model.DataUpdateTime = DateTime.Parse(row["DataUpdateTime"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.B_ClientPara> DataTableToList(DataTable dt)
        {
            List<Model.B_ClientPara> modelList = new List<Model.B_ClientPara>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.B_ClientPara model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }
    }
}
