using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.Oracle.weblis
{
    //WhoNet_AntiData

    public partial class WhoNet_AntiData : BaseDALLisDB, IWhoNet_AntiData
    {



        D_LogInfo d_log = new D_LogInfo();


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.WhoNet_AntiData model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.LabID != null)
            {
                strSql1.Append("LabID,");
                strSql2.Append("" + model.LabID + ",");
            }
            if (model.AntiID != null)
            {
                strSql1.Append("AntiID,");
                strSql2.Append("" + model.AntiID + ",");
            }
            if (model.AntiName != null)
            {
                strSql1.Append("AntiName,");
                strSql2.Append("'" + model.AntiName + "',");
            }
            if (model.TestMethod != null)
            {
                strSql1.Append("TestMethod,");
                strSql2.Append("'" + model.TestMethod + "',");
            }
            if (model.RefRange != null)
            {
                strSql1.Append("RefRange,");
                strSql2.Append("'" + model.RefRange + "',");
            }
            if (model.Suscept != null)
            {
                strSql1.Append("Suscept,");
                strSql2.Append("'" + model.Suscept + "',");
            }
            if (model.DataAddTime != null)
            {
                strSql1.Append("DataAddTime,");
                strSql2.Append(" to_date('" + model.DataAddTime.ToString() + "','YYYY-MM-DD HH24:MI:SS'),");
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
            if (model.MicroID != null)
            {
                strSql1.Append("MicroID,");
                strSql2.Append("" + model.MicroID + ",");
            }
            strSql.Append("insert into WhoNet_AntiData(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");

            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("insert into WhoNet_AntiData(");
            //strSql.Append("LabID,AntiID,AntiName,TestMethod,RefRange,Suscept,DataAddTime,DataUpdateTime,MicroID");
            //strSql.Append(") values (");
            //strSql.Append("@LabID,@AntiID,@AntiName,@TestMethod,@RefRange,@Suscept,@DataAddTime,@DataUpdateTime,@MicroID");
            //strSql.Append(") ");

            //SqlParameter[] parameters = {
            //            new SqlParameter("@LabID", SqlDbType.BigInt,8) ,            
            //            new SqlParameter("@AntiID", SqlDbType.BigInt,8) ,            
            //            new SqlParameter("@AntiName", SqlDbType.VarChar,10) ,            
            //            new SqlParameter("@TestMethod", SqlDbType.VarChar,10) ,            
            //            new SqlParameter("@RefRange", SqlDbType.VarChar,100) ,            
            //            new SqlParameter("@Suscept", SqlDbType.VarChar,8) ,            
            //            new SqlParameter("@DataAddTime", SqlDbType.DateTime) ,            
            //            new SqlParameter("@DataUpdateTime", SqlDbType.DateTime),
            //            new SqlParameter("@MicroID",SqlDbType.BigInt,8)
              
            //};
            //parameters[0].Value = model.LabID;
            //parameters[1].Value = model.AntiID;
            //parameters[2].Value = model.AntiName;
            //parameters[3].Value = model.TestMethod;
            //parameters[4].Value = model.RefRange;
            //parameters[5].Value = model.Suscept;
            //parameters[6].Value = model.DataAddTime;
            //parameters[7].Value = model.DataUpdateTime;
            //parameters[8].Value = model.MicroID;
            if (DbHelperSQL.ExecuteNonQuery(strSql.ToString()) > 0)
            {
                return d_log.OperateLog("WhoNet_AntiData", "", "", DateTime.Now, 1);
            }
            else
                return -1;

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.WhoNet_AntiData model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update WhoNet_AntiData set ");
            if (model.LabID != null)
            {
                strSql.Append("LabID=" + model.LabID + ",");
            }
            else
            {
                strSql.Append("LabID= null ,");
            }
            if (model.AntiName != null)
            {
                strSql.Append("AntiName='" + model.AntiName + "',");
            }
            else
            {
                strSql.Append("AntiName= null ,");
            }
            if (model.TestMethod != null)
            {
                strSql.Append("TestMethod='" + model.TestMethod + "',");
            }
            else
            {
                strSql.Append("TestMethod= null ,");
            }
            if (model.RefRange != null)
            {
                strSql.Append("RefRange='" + model.RefRange + "',");
            }
            else
            {
                strSql.Append("RefRange= null ,");
            }
            if (model.Suscept != null)
            {
                strSql.Append("Suscept='" + model.Suscept + "',");
            }
            else
            {
                strSql.Append("Suscept= null ,");
            }
            if (model.DataAddTime != null)
            {
                strSql.Append(" DataAddTime=to_date('" + model.DataAddTime.ToString() + "','YYYY-MM-DD HH24:MI:SS'),");
            }
            if (model.DataUpdateTime != null)
            {
                strSql.Append("DataUpdateTime='" + model.DataUpdateTime + "',");
            }
            else
            {
                strSql.Append("DataUpdateTime= null ,");
            }
            if (model.MicroID != null)
            {
                strSql.Append("MicroID=" + model.MicroID + ",");
            }
            else
            {
                strSql.Append("MicroID= null ,");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where AntiID=" + model.AntiID + " ");

            if (DbHelperSQL.ExecuteNonQuery(strSql.ToString()) > 0)
            {
                return d_log.OperateLog("WhoNet_AntiData", "", "", DateTime.Now, 1);
            }
            else
                return -1;

            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("update WhoNet_AntiData set ");

            //strSql.Append(" LabID = @LabID , ");
            //strSql.Append(" AntiID = @AntiID , ");
            //strSql.Append(" AntiName = @AntiName , ");
            //strSql.Append(" TestMethod = @TestMethod , ");
            //strSql.Append(" RefRange = @RefRange , ");
            //strSql.Append(" Suscept = @Suscept , ");
            //strSql.Append(" DataAddTime = @DataAddTime , ");
            //strSql.Append(" DataUpdateTime = @DataUpdateTime , ");

            //strSql.Append(" MicroID = @MicroID  ");
            //strSql.Append(" where AntiID=@AntiID  ");

            //SqlParameter[] parameters = {
            //            new SqlParameter("@LabID", SqlDbType.BigInt,8) ,            
            //            new SqlParameter("@AntiID", SqlDbType.BigInt,8) ,            
            //            new SqlParameter("@AntiName", SqlDbType.VarChar,10) ,            
            //            new SqlParameter("@TestMethod", SqlDbType.VarChar,10) ,            
            //            new SqlParameter("@RefRange", SqlDbType.VarChar,100) ,            
            //            new SqlParameter("@Suscept", SqlDbType.VarChar,8) ,            
            //            new SqlParameter("@DataAddTime", SqlDbType.DateTime) ,            
            //            new SqlParameter("@DataUpdateTime", SqlDbType.DateTime) ,            

            //            new SqlParameter("@MicroID", SqlDbType.BigInt,8)        
              
            //};

            //parameters[0].Value = model.LabID;
            //parameters[1].Value = model.AntiID;
            //parameters[2].Value = model.AntiName;
            //parameters[3].Value = model.TestMethod;
            //parameters[4].Value = model.RefRange;
            //parameters[5].Value = model.Suscept;
            //parameters[6].Value = model.DataAddTime;
            //parameters[7].Value = model.DataUpdateTime;

            //parameters[8].Value = model.MicroID;
            //if (DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters) > 0)
            //{
            //    return d_log.OperateLog("WhoNet_AntiData", "", "", DateTime.Now, 1);
            //}
            //else
            //    return -1;
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(long AntiID)
        {

            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("delete from WhoNet_AntiData ");
            //strSql.Append(" where AntiID=@AntiID ");
            //SqlParameter[] parameters = {
            //        new SqlParameter("@AntiID", SqlDbType.BigInt,8)			};
            //parameters[0].Value = AntiID;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from WhoNet_AntiData ");
            strSql.Append(" where AntiID=" + AntiID + " ");

            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
        }



        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.WhoNet_AntiData GetModel(long AntiID)
        {

            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("select LabID, AntiID, AntiName, TestMethod, RefRange, Suscept, DataAddTime, DataUpdateTime, DataTimeStamp  ");
            //strSql.Append("  from WhoNet_AntiData ");
            //strSql.Append(" where AntiID=@AntiID ");
            //SqlParameter[] parameters = {
            //        new SqlParameter("@AntiID", SqlDbType.BigInt,8)			};
            //parameters[0].Value = AntiID;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(" LabID,AntiID,AntiName,TestMethod,RefRange,Suscept,DataAddTime,DataUpdateTime,DataTimeStamp,MicroID ");
            strSql.Append(" from WhoNet_AntiData ");
            strSql.Append(" where AntiID=" + AntiID + " ");

            ZhiFang.Model.WhoNet_AntiData model = new ZhiFang.Model.WhoNet_AntiData();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["LabID"].ToString() != "")
                {
                    model.LabID = long.Parse(ds.Tables[0].Rows[0]["LabID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AntiID"].ToString() != "")
                {
                    model.AntiID = long.Parse(ds.Tables[0].Rows[0]["AntiID"].ToString());
                }
                model.AntiName = ds.Tables[0].Rows[0]["AntiName"].ToString();
                model.TestMethod = ds.Tables[0].Rows[0]["TestMethod"].ToString();
                model.RefRange = ds.Tables[0].Rows[0]["RefRange"].ToString();
                model.Suscept = ds.Tables[0].Rows[0]["Suscept"].ToString();
                if (ds.Tables[0].Rows[0]["DataAddTime"].ToString() != "")
                {
                    model.DataAddTime = DateTime.Parse(ds.Tables[0].Rows[0]["DataAddTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DataUpdateTime"].ToString() != "")
                {
                    model.DataUpdateTime = DateTime.Parse(ds.Tables[0].Rows[0]["DataUpdateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DataTimeStamp"].ToString() != "")
                {
                    model.DataTimeStamp = DateTime.Parse(ds.Tables[0].Rows[0]["DataTimeStamp"].ToString());
                }

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
            strSql.Append(" FROM WhoNet_AntiData ");
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
         
            strSql.Append(" * ");
            strSql.Append(" FROM WhoNet_AntiData ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
                strSql.Append(" and ROWNUM <= '" + Top + "'");
            }
            else
            {
                strSql.Append(" where ROWNUM <= '" + Top + "'");
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }



        #region IDataBase<WhoNet_AntiData> 成员

        public int GetMaxId()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 根据实体获取DataSet
        /// </summary>
        public DataSet GetList(Model.WhoNet_AntiData model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM WhoNet_AntiData where 1=1 ");
            if (model.LabID != null)
            {
                strSql.Append(" and LabID=" + model.LabID + " ");
            }
            if (model.AntiID != null)
            {
                strSql.Append(" and AntiID=" + model.AntiID + " ");
            }
            if (model.AntiName != null && model.AntiName != "")
            {
                strSql.Append(" and AntiName='" + model.AntiName + "' ");
            }
            if (model.TestMethod != null && model.TestMethod != "")
            {
                strSql.Append(" and TestMethod='" + model.TestMethod + "' ");
            }
            if (model.RefRange != null && model.RefRange != "")
            {
                strSql.Append(" and RefRange='" + model.RefRange + "' ");
            }
            if (model.Suscept != null && model.Suscept != "")
            {
                strSql.Append(" and Suscept='" + model.Suscept + "' ");
            }
            if (model.DataAddTime != null)
            {
                strSql.Append(" and DataAddTime=to_date('" + model.DataAddTime.ToString() + "','YYYY-MM-DD HH24:MI:SS'),");
               
            }
            if (model.DataUpdateTime != null)
            {
                strSql.Append(" and DataUpdateTime='" + model.DataUpdateTime + "' ");
            }
            if (model.DataTimeStamp != null)
            {
                strSql.Append(" and DataTimeStamp='" + model.DataTimeStamp + "' ");
            }
            if (model.DataUpdateTime != null)
            {
                strSql.Append(" and DataUpdateTime='" + model.DataUpdateTime + "' ");
            }
            if (model.DataTimeStamp != null)
            {
                strSql.Append(" and DataTimeStamp='" + model.DataTimeStamp + "' ");
            }
            if (model.MicroID != null)
            {
                strSql.Append(" and MicroID='" + model.MicroID + "'");
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetList(int Top, Model.WhoNet_AntiData t, string filedOrder)
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

        public int GetTotalCount(Model.WhoNet_AntiData t)
        {
            throw new NotImplementedException();
        }

        #endregion

       
        #region IDataBase<WhoNet_AntiData> 成员

        int IDataBase<Model.WhoNet_AntiData>.GetMaxId()
        {
            throw new NotImplementedException();
        }

        int IDataBase<Model.WhoNet_AntiData>.Add(Model.WhoNet_AntiData t)
        {
            throw new NotImplementedException();
        }

        int IDataBase<Model.WhoNet_AntiData>.Update(Model.WhoNet_AntiData t)
        {
            throw new NotImplementedException();
        }

        DataSet IDataBase<Model.WhoNet_AntiData>.GetList(Model.WhoNet_AntiData t)
        {
            throw new NotImplementedException();
        }

        DataSet IDataBase<Model.WhoNet_AntiData>.GetList(int Top, Model.WhoNet_AntiData t, string filedOrder)
        {
            throw new NotImplementedException();
        }

        DataSet IDataBase<Model.WhoNet_AntiData>.GetAllList()
        {
            throw new NotImplementedException();
        }

        int IDataBase<Model.WhoNet_AntiData>.AddUpdateByDataSet(DataSet ds)
        {
            throw new NotImplementedException();
        }

        int IDataBase<Model.WhoNet_AntiData>.GetTotalCount()
        {
            throw new NotImplementedException();
        }

        int IDataBase<Model.WhoNet_AntiData>.GetTotalCount(Model.WhoNet_AntiData t)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

