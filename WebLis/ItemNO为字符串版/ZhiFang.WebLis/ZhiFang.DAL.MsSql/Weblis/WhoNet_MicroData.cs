using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.MsSql.Weblis
{
    //WhoNet_MicroData

    public partial class WhoNet_MicroData : BaseDALLisDB, IWhoNet_MicroData
    {

        D_LogInfo d_log = new D_LogInfo();


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.WhoNet_MicroData model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into WhoNet_MicroData(");
            strSql.Append("LabID,MicroID,FomID,date_data,organism,org_type,origin,ESBL,comment,DataAddTime,DataUpDateTime,beta_lact");
            strSql.Append(") values (");
            strSql.Append("@LabID,@MicroID,@FomID,@date_data,@organism,@org_type,@origin,@ESBL,@comment,@DataAddTime,@DataUpDateTime,@beta_lact");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@LabID", SqlDbType.BigInt,8) ,            
                        new SqlParameter("@MicroID", SqlDbType.BigInt,8) ,            
                        new SqlParameter("@FomID", SqlDbType.BigInt,8) ,            
                        new SqlParameter("@date_data", SqlDbType.DateTime) ,            
                        new SqlParameter("@organism", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@org_type", SqlDbType.VarChar,1) ,            
                        new SqlParameter("@origin", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ESBL", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@comment", SqlDbType.VarChar,30) ,            
                        new SqlParameter("@DataAddTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@DataUpDateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@beta_lact", SqlDbType.VarChar,50)            
              
            };

            parameters[0].Value = model.LabID;
            parameters[1].Value = model.MicroID;
            parameters[2].Value = model.FomID;
            parameters[3].Value = model.date_data;
            parameters[4].Value = model.organism;
            parameters[5].Value = model.org_type;
            parameters[6].Value = model.origin;
            parameters[7].Value = model.ESBL;
            parameters[8].Value = model.comment;
            parameters[9].Value = model.DataAddTime;
            parameters[10].Value = model.DataUpDateTime;
            parameters[11].Value = model.beta_lact;

            if (DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters) > 0)
            {
                return d_log.OperateLog("WhoNet_MicroData", "", "", DateTime.Now, 1);
            }
            else
                return -1;

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.WhoNet_MicroData model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update WhoNet_MicroData set ");

            strSql.Append(" LabID = @LabID , ");
            strSql.Append(" MicroID = @MicroID , ");
            strSql.Append(" FomID = @FomID , ");
            strSql.Append(" date_data = @date_data , ");
            strSql.Append(" organism = @organism , ");
            strSql.Append(" org_type = @org_type , ");
            strSql.Append(" origin = @origin , ");
            strSql.Append(" ESBL = @ESBL , ");
            strSql.Append(" comment = @comment , ");
            strSql.Append(" DataAddTime = @DataAddTime , ");
            strSql.Append(" DataUpDateTime = @DataUpDateTime , ");
            strSql.Append(" beta_lact = @beta_lact  ");
            strSql.Append(" where MicroID=@MicroID  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@LabID", SqlDbType.BigInt,8) ,            
                        new SqlParameter("@MicroID", SqlDbType.BigInt,8) ,            
                        new SqlParameter("@FomID", SqlDbType.BigInt,8) ,            
                        new SqlParameter("@date_data", SqlDbType.DateTime) ,            
                        new SqlParameter("@organism", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@org_type", SqlDbType.VarChar,1) ,            
                        new SqlParameter("@origin", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ESBL", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@comment", SqlDbType.VarChar,30) ,            
                        new SqlParameter("@DataAddTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@DataUpDateTime", SqlDbType.DateTime) ,                    
                        new SqlParameter("@beta_lact", SqlDbType.VarChar,50)
            };

            parameters[0].Value = model.LabID;
            parameters[1].Value = model.MicroID;
            parameters[2].Value = model.FomID;
            parameters[3].Value = model.date_data;
            parameters[4].Value = model.organism;
            parameters[5].Value = model.org_type;
            parameters[6].Value = model.origin;
            parameters[7].Value = model.ESBL;
            parameters[8].Value = model.comment;
            parameters[9].Value = model.DataAddTime;
            parameters[10].Value = model.DataUpDateTime;
            parameters[11].Value = model.beta_lact;
            if (DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters) > 0)
            {
                return d_log.OperateLog("WhoNet_MicroData", "", "", DateTime.Now, 1);
            }
            else
                return -1;
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(long MicroID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from WhoNet_MicroData ");
            strSql.Append(" where MicroID=@MicroID ");
            SqlParameter[] parameters = {
					new SqlParameter("@MicroID", SqlDbType.BigInt,8)			};
            parameters[0].Value = MicroID;


            return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.WhoNet_MicroData GetModel(long MicroID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select LabID, MicroID, FomID, date_data, organism, org_type, origin, ESBL, comment, DataAddTime, DataUpDateTime, DataTimeStamp, beta_lact  ");
            strSql.Append("  from WhoNet_MicroData ");
            strSql.Append(" where MicroID=@MicroID ");
            SqlParameter[] parameters = {
					new SqlParameter("@MicroID", SqlDbType.BigInt,8)			};
            parameters[0].Value = MicroID;


            ZhiFang.Model.WhoNet_MicroData model = new ZhiFang.Model.WhoNet_MicroData();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["LabID"].ToString() != "")
                {
                    model.LabID = long.Parse(ds.Tables[0].Rows[0]["LabID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MicroID"].ToString() != "")
                {
                    model.MicroID = long.Parse(ds.Tables[0].Rows[0]["MicroID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FomID"].ToString() != "")
                {
                    model.FomID = long.Parse(ds.Tables[0].Rows[0]["FomID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["date_data"].ToString() != "")
                {
                    model.date_data = DateTime.Parse(ds.Tables[0].Rows[0]["date_data"].ToString());
                }
                model.organism = ds.Tables[0].Rows[0]["organism"].ToString();
                model.org_type = ds.Tables[0].Rows[0]["org_type"].ToString();
                model.origin = ds.Tables[0].Rows[0]["origin"].ToString();
                model.ESBL = ds.Tables[0].Rows[0]["ESBL"].ToString();
                model.comment = ds.Tables[0].Rows[0]["comment"].ToString();
                if (ds.Tables[0].Rows[0]["DataAddTime"].ToString() != "")
                {
                    model.DataAddTime = DateTime.Parse(ds.Tables[0].Rows[0]["DataAddTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DataUpDateTime"].ToString() != "")
                {
                    model.DataUpDateTime = DateTime.Parse(ds.Tables[0].Rows[0]["DataUpDateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DataTimeStamp"].ToString() != "")
                {
                    model.DataTimeStamp = DateTime.Parse(ds.Tables[0].Rows[0]["DataTimeStamp"].ToString());
                }
                model.beta_lact = ds.Tables[0].Rows[0]["beta_lact"].ToString();

                return model;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM WhoNet_MicroData ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM WhoNet_MicroData ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }



        #region IDataBase<WhoNet_MicroData> 成员

        public int GetMaxId()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 根据实体获取DataSet
        /// </summary>
        public DataSet GetList(Model.WhoNet_MicroData model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM WhoNet_MicroData where 1=1 ");
            if (model.LabID != null)
            {
                strSql.Append(" and LabID=" + model.LabID + " ");
            }
            if (model.MicroID != null)
            {
                strSql.Append(" and MicroID=" + model.MicroID + " ");
            }
            if (model.FomID != null)
            {
                strSql.Append(" and FomID=" + model.FomID + "");
            }
            if (model.date_data != null)
            {
                strSql.Append(" and date_data='" + model.date_data + "' ");
            }
            if (model.org_type != null && model.org_type != "")
            {
                strSql.Append(" and org_type='" + model.org_type + "' ");
            }
            if (model.origin != null && model.origin != "")
            {
                strSql.Append(" and origin='" + model.origin + "' ");
            }
            if (model.ESBL != null && model.origin != "")
            {
                strSql.Append(" and ESBL='" + model.ESBL + "' ");
            }
            if (model.comment != null && model.origin != "")
            {
                strSql.Append(" and comment='" + model.comment + "' ");
            }
            if (model.DataAddTime != null)
            {
                strSql.Append(" and DataAddTime='" + model.DataAddTime + "' ");
            }
            if (model.DataUpDateTime != null)
            {
                strSql.Append(" and DataUpDateTime='" + model.DataUpDateTime + "' ");
            }
            if (model.DataTimeStamp != null)
            {
                strSql.Append(" and DataTimeStamp='" + model.DataTimeStamp + "' ");
            }
            if (model.beta_lact != null && model.beta_lact != "")
            {
                strSql.Append(" and beta_lact='" + model.beta_lact + "' ");
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetList(int Top, Model.WhoNet_MicroData t, string filedOrder)
        {
            throw new NotImplementedException();
        }

        public DataSet GetAllList()
        {
            throw new NotImplementedException();
        }

        public int AddUpdateByDataSet(DataSet ds)
        {
            throw new NotImplementedException();
        }

        public int GetTotalCount()
        {
            throw new NotImplementedException();
        }

        public int GetTotalCount(Model.WhoNet_MicroData t)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

