using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.MsSql.Digitlab8
{
    //B_SampleTypeControl

    public partial class B_SampleTypeControl : IDSampleTypeControl
    {
        DBUtility.IDBConnection idb;
        public B_SampleTypeControl(string dbsourceconn)
        {
            idb = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public B_SampleTypeControl()
        {
            idb = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.LisDB());
        }
        D_LogInfo d_log = new D_LogInfo();
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.SampleTypeControl model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into B_SampleTypeControl(");
            strSql.Append("SampleTypeControlNo,SampleTypeNo,ControlLabNo,ControlSampleTypeNo,UseFlag");
            strSql.Append(") values (");
            strSql.Append("@SampleTypeControlNo,@SampleTypeNo,@ControlLabNo,@ControlSampleTypeNo,@UseFlag");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@SampleTypeControlNo", SqlDbType.Char,50) ,            
                        new SqlParameter("@SampleTypeNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@ControlLabNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ControlSampleTypeNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@UseFlag", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.SampleTypeControlNo;
            parameters[1].Value = model.SampleTypeNo;
            parameters[2].Value = model.ControlLabNo;
            parameters[3].Value = model.ControlSampleTypeNo;
            parameters[4].Value = model.UseFlag;
            if (idb.ExecuteNonQuery(strSql.ToString(), parameters) > 0)
            {
                return d_log.OperateLog("SampleType", "", "", DateTime.Now, 1);
            }
            else
                return -1;
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.SampleTypeControl model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update B_SampleTypeControl set ");

            strSql.Append(" SampleTypeControlNo = @SampleTypeControlNo , ");
            strSql.Append(" SampleTypeNo = @SampleTypeNo , ");
            strSql.Append(" ControlLabNo = @ControlLabNo , ");
            strSql.Append(" ControlSampleTypeNo = @ControlSampleTypeNo , ");
            strSql.Append(" UseFlag = @UseFlag  ");
            strSql.Append(" where SampleTypeControlNo=@SampleTypeControlNo  ");

            SqlParameter[] parameters = {
			            	
                           
            new SqlParameter("@SampleTypeControlNo", SqlDbType.Char,50) ,            	
                           
            new SqlParameter("@SampleTypeNo", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@ControlLabNo", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@ControlSampleTypeNo", SqlDbType.Int,4) ,            	
                        	
                        	
                           
            new SqlParameter("@UseFlag", SqlDbType.Int,4)             	
              
            };




            if (model.SampleTypeControlNo != null)
            {
                parameters[0].Value = model.SampleTypeControlNo;
            }



            if (model.SampleTypeNo != null)
            {
                parameters[1].Value = model.SampleTypeNo;
            }



            if (model.ControlLabNo != null)
            {
                parameters[2].Value = model.ControlLabNo;
            }



            if (model.ControlSampleTypeNo != null)
            {
                parameters[3].Value = model.ControlSampleTypeNo;
            }







            if (model.UseFlag != null)
            {
                parameters[4].Value = model.UseFlag;
            }


            if (idb.ExecuteNonQuery(strSql.ToString(), parameters) > 0)
            {
                return d_log.OperateLog("SampleType", "", "", DateTime.Now, 1);
            }
            else
                return -1;
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string SampleTypeControlNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from B_SampleTypeControl ");
            strSql.Append(" where SampleTypeControlNo=@SampleTypeControlNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@SampleTypeControlNo", SqlDbType.Char,50)};
            parameters[0].Value = SampleTypeControlNo;


            return idb.ExecuteNonQuery(strSql.ToString(), parameters);

        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from B_SampleTypeControl ");
            strSql.Append(" where ID in (" + Idlist + ")  ");
            return idb.ExecuteNonQuery(strSql.ToString());

        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.SampleTypeControl GetModel(string SampleTypeControlNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, SampleTypeControlNo, SampleTypeNo, ControlLabNo, ControlSampleTypeNo, DTimeStampe, AddTime, UseFlag  ");
            strSql.Append("  from B_SampleTypeControl ");
            strSql.Append(" where SampleTypeControlNo=@SampleTypeControlNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@SampleTypeControlNo", SqlDbType.Char,50)};
            parameters[0].Value = SampleTypeControlNo;


            ZhiFang.Model.SampleTypeControl model = new ZhiFang.Model.SampleTypeControl();
            DataSet ds = idb.ExecuteDataSet(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                model.SampleTypeControlNo = ds.Tables[0].Rows[0]["SampleTypeControlNo"].ToString();
                if (ds.Tables[0].Rows[0]["SampleTypeNo"].ToString() != "")
                {
                    model.SampleTypeNo = int.Parse(ds.Tables[0].Rows[0]["SampleTypeNo"].ToString());
                }
                model.ControlLabNo = ds.Tables[0].Rows[0]["ControlLabNo"].ToString();
                if (ds.Tables[0].Rows[0]["ControlSampleTypeNo"].ToString() != "")
                {
                    model.ControlSampleTypeNo = ds.Tables[0].Rows[0]["ControlSampleTypeNo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DTimeStampe"].ToString() != "")
                {
                    model.DTimeStampe = DateTime.Parse(ds.Tables[0].Rows[0]["DTimeStampe"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AddTime"].ToString() != "")
                {
                    model.AddTime = DateTime.Parse(ds.Tables[0].Rows[0]["AddTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UseFlag"].ToString() != "")
                {
                    model.UseFlag = int.Parse(ds.Tables[0].Rows[0]["UseFlag"].ToString());
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
            strSql.Append(" FROM B_SampleTypeControl ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return idb.ExecuteDataSet(strSql.ToString());
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
            strSql.Append(" FROM B_SampleTypeControl ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return idb.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 根据实体获取DataSet
        /// </summary>
        public DataSet GetList(ZhiFang.Model.SampleTypeControl model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM B_SampleTypeControl where 1=1 ");

            if (model.SampleTypeControlNo != null)
            {
                strSql.Append(" and SampleTypeControlNo='" + model.SampleTypeControlNo + "' ");
            }

            if (model.SampleTypeNo != -1)
            {
                strSql.Append(" and SampleTypeNo=" + model.SampleTypeNo + " ");
            }

            if (model.ControlLabNo != null)
            {
                strSql.Append(" and ControlLabNo='" + model.ControlLabNo + "' ");
            }

            if (model.ControlSampleTypeNo != null)
            {
                strSql.Append(" and ControlSampleTypeNo=" + model.ControlSampleTypeNo + " ");
            }

            return idb.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 获取总记录
        /// </summary>
        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_SampleTypeControl ");
            string strCount = idb.ExecuteScalar(strSql.ToString());
            if (strCount != null && strCount.Trim() != "")
            {
                return Convert.ToInt32(strCount.Trim());
            }
            else
            {
                return 0;
            }
        }
        public int GetTotalCount(ZhiFang.Model.SampleTypeControl model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_SampleTypeControl where 1=1 ");

            if (model.SampleTypeControlNo != null)
            {
                strSql.Append(" and SampleTypeControlNo='" + model.SampleTypeControlNo + "' ");
            }

            if (model.SampleTypeNo != null)
            {
                strSql.Append(" and SampleTypeNo=" + model.SampleTypeNo + " ");
            }

            if (model.ControlLabNo != null)
            {
                strSql.Append(" and ControlLabNo='" + model.ControlLabNo + "' ");
            }

            if (model.ControlSampleTypeNo != null)
            {
                strSql.Append(" and ControlSampleTypeNo=" + model.ControlSampleTypeNo + " ");
            }

            if (model.DTimeStampe != null)
            {
                strSql.Append(" and DTimeStampe='" + model.DTimeStampe + "' ");
            }

            if (model.AddTime != null)
            {
                strSql.Append(" and AddTime='" + model.AddTime + "' ");
            }

            if (model.UseFlag != null)
            {
                strSql.Append(" and UseFlag=" + model.UseFlag + " ");
            }
            string strCount = idb.ExecuteScalar(strSql.ToString());
            if (strCount != null && strCount.Trim() != "")
            {
                return Convert.ToInt32(strCount.Trim());
            }
            else
            {
                return 0;
            }
        }


        public bool Exists(string SampleTypeControlNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from B_SampleTypeControl ");
            strSql.Append(" where SampleTypeControlNo ='" + SampleTypeControlNo + "'");
            string strCount = idb.ExecuteScalar(strSql.ToString());
            if (strCount != null && strCount.Trim() != "" && strCount.Trim() != "0")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Exists(System.Collections.Hashtable ht)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from B_SampleTypeControl where 1=1 ");
            if (ht.Count > 0)
            {
                foreach (System.Collections.DictionaryEntry item in ht)
                {
                    strSql.Append(" and " + item.Key.ToString().Trim() + "='" + item.Value + "' ");
                }
                string strCount = idb.ExecuteScalar(strSql.ToString());
                if (strCount != null && strCount.Trim() != "" && strCount.Trim() != "0")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public int GetMaxId()
        {
            return idb.GetMaxID("SampleTypeControlNo", "B_SampleTypeControl");
        }

        public DataSet GetList(int Top, ZhiFang.Model.SampleTypeControl model, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM B_SampleTypeControl ");


            if (model.SampleTypeControlNo != null)
            {

                strSql.Append(" and SampleTypeControlNo='" + model.SampleTypeControlNo + "' ");
            }

            if (model.SampleTypeNo != null)
            {
                strSql.Append(" and SampleTypeNo=" + model.SampleTypeNo + " ");
            }

            if (model.ControlLabNo != null)
            {

                strSql.Append(" and ControlLabNo='" + model.ControlLabNo + "' ");
            }

            if (model.ControlSampleTypeNo != null)
            {
                strSql.Append(" and ControlSampleTypeNo=" + model.ControlSampleTypeNo + " ");
            }

            if (model.DTimeStampe != null)
            {

                strSql.Append(" and DTimeStampe='" + model.DTimeStampe + "' ");
            }

            if (model.AddTime != null)
            {

                strSql.Append(" and AddTime='" + model.AddTime + "' ");
            }

            if (model.UseFlag != null)
            {
                strSql.Append(" and UseFlag=" + model.UseFlag + " ");
            }

            strSql.Append(" order by " + filedOrder);
            return idb.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetAllList()
        {
            return GetList("");
        }



        #region IDataBase<SampleTypeControl> 成员
        public int AddUpdateByDataSet(DataSet ds)
        {
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    int count = 0;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = ds.Tables[0].Rows[i];
                        if (this.Exists(ds.Tables[0].Rows[i]["SampleTypeControlNo"].ToString().Trim()))
                        {
                            count += this.UpdateByDataRow(dr);
                        }
                        else
                            count += this.AddByDataRow(dr);
                    }
                    if (count == ds.Tables[0].Rows.Count)
                        return 1;
                    else
                        return 0;
                }
                catch
                {
                    return 0;
                }
            }
            else
                return 1;
        }
        public int AddByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into B_SampleTypeControl (");
                strSql.Append("SampleTypeControlNo,SampleTypeNo,ControlLabNo,ControlSampleTypeNo,AddTime,UseFlag");
                strSql.Append(") values (");
                strSql.Append("'" + dr["SampleTypeControlNo"].ToString().Trim() + "','" + dr["SampleTypeNo"].ToString().Trim() + "','" + dr["ControlLabNo"].ToString().Trim() + "','" + dr["ControlSampleTypeNo"].ToString().Trim() + "','" + DateTime.Parse(dr["AddTime"].ToString().Trim()) + "','" + dr["UseFlag"].ToString().Trim() + "'");
                strSql.Append(") ");
                return idb.ExecuteNonQuery(strSql.ToString());
            }
            catch
            {
                return 0;
            }
        }
        public int UpdateByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update B_SampleTypeControl set ");

                strSql.Append(" SampleTypeNo = '" + dr["SampleTypeNo"].ToString().Trim() + "' , ");
                strSql.Append(" ControlLabNo = '" + dr["ControlLabNo"].ToString().Trim() + "' , ");
                strSql.Append(" ControlSampleTypeNo = '" + dr["ControlSampleTypeNo"].ToString().Trim() + "' , ");
                strSql.Append(" UseFlag = '" + dr["UseFlag"].ToString().Trim() + "'  ");
                strSql.Append(" where SampleTypeControlNo='" + dr["SampleTypeControlNo"].ToString().Trim() + "' ");

                return idb.ExecuteNonQuery(strSql.ToString());
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region IDSampleTypeControl 成员


        public bool CheckIncludeLabCode(List<string> l, string LabCode)
        {
            throw new NotImplementedException();
        }

        public bool CheckIncludeCenterCode(List<string> l, string LabCode)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDSampleTypeControl 成员


        public DataSet GetLabCodeNo(string LabCode, List<string> CenterNoList)
        {
            throw new NotImplementedException();
        }

        public DataSet GetCenterNo(string LabCode, List<string> LabPrimaryNoList)
        {
            throw new NotImplementedException();
        }

        #endregion


        public DataSet GetListByPage(Model.SampleTypeControl model, int nowPageNum, int nowPageSize)
        {
            throw new NotImplementedException();
        }

        public DataSet B_lab_GetListByPage(Model.SampleTypeControl model, int nowPageNum, int nowPageSize)
        {
            throw new NotImplementedException();
        }
    }
}

