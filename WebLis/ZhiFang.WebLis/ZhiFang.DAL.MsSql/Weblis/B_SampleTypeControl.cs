using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.MsSql.Weblis
{
    //B_SampleTypeControl

    public partial class B_SampleTypeControl : BaseDALLisDB, IDSampleTypeControl
    {

        public B_SampleTypeControl(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public B_SampleTypeControl()
        {
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
                        new SqlParameter("@ControlSampleTypeNo", SqlDbType.VarChar,30) ,            
                        new SqlParameter("@UseFlag", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.SampleTypeControlNo;
            parameters[1].Value = model.SampleTypeNo;
            parameters[2].Value = model.ControlLabNo;
            parameters[3].Value = model.ControlSampleTypeNo;
            parameters[4].Value = model.UseFlag;
            if (DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters) > 0)
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
                           
            new SqlParameter("@ControlSampleTypeNo", SqlDbType.VarChar,30) ,            	
                        	
                        	
                           
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


            if (DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters) > 0)
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


            return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);

        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from B_SampleTypeControl ");
            strSql.Append(" where ID in (" + Idlist + ")  ");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());

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
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString(), parameters);

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
            strSql.Append(" FROM B_SampleTypeControl ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
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
                strSql.Append(" and ControlSampleTypeNo='" + model.ControlSampleTypeNo + "' ");
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 获取总记录
        /// </summary>
        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_SampleTypeControl ");
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


        public bool Exists(string SampleTypeControlNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from B_SampleTypeControl ");
            strSql.Append(" where SampleTypeControlNo ='" + SampleTypeControlNo + "'");
            string strCount = DbHelperSQL.ExecuteScalar(strSql.ToString());
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
                string strCount = DbHelperSQL.ExecuteScalar(strSql.ToString());
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
            return DbHelperSQL.GetMaxID("SampleTypeControlNo", "B_SampleTypeControl");
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
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
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
                            System.Threading.Thread.Sleep(ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ThreadDicSynchInterval"));
                            count += this.UpdateByDataRow(dr);
                        }
                        else
                        {
                            System.Threading.Thread.Sleep(ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ThreadDicSynchInterval"));
                            count += this.AddByDataRow(dr);
                        }
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
                strSql.Append("SampleTypeControlNo,SampleTypeNo,ControlLabNo,ControlSampleTypeNo,UseFlag");
                strSql.Append(") values (");
                if (dr.Table.Columns["SampleTypeControlNo"] != null && dr.Table.Columns["SampleTypeControlNo"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["SampleTypeControlNo"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["SampleTypeNo"] != null && dr.Table.Columns["SampleTypeNo"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["SampleTypeNo"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["ControlLabNo"] != null && dr.Table.Columns["ControlLabNo"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["ControlLabNo"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["ControlSampleTypeNo"] != null && dr.Table.Columns["ControlSampleTypeNo"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["ControlSampleTypeNo"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["UseFlag"] != null && dr.Table.Columns["UseFlag"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["UseFlag"].ToString().Trim() + "' ");
                }
                else
                {
                    strSql.Append(" null ");
                }
                strSql.Append(") ");
                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
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


                if (dr.Table.Columns["SampleTypeNo"] != null && dr.Table.Columns["SampleTypeNo"].ToString().Trim() != "")
                {
                    strSql.Append(" SampleTypeNo = '" + dr["SampleTypeNo"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["ControlLabNo"] != null && dr.Table.Columns["ControlLabNo"].ToString().Trim() != "")
                {
                    strSql.Append(" ControlLabNo = '" + dr["ControlLabNo"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["ControlSampleTypeNo"] != null && dr.Table.Columns["ControlSampleTypeNo"].ToString().Trim() != "")
                {
                    strSql.Append(" ControlSampleTypeNo = '" + dr["ControlSampleTypeNo"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["UseFlag"] != null && dr.Table.Columns["UseFlag"].ToString().Trim() != "")
                {
                    strSql.Append(" UseFlag = '" + dr["UseFlag"].ToString().Trim() + "' , ");
                }
                int n = strSql.ToString().LastIndexOf(",");
                strSql.Remove(n, 1);
                strSql.Append(" where SampleTypeControlNo='" + dr["SampleTypeControlNo"].ToString().Trim() + "' ");

                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
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
            bool b = false;
            try
            {
                string strItemNos = "";
                int count = 0;
                for (int i = 0; i < l.Count; i++)
                {
                    if (strItemNos.Trim() == "")
                    {
                        strItemNos = "'" + l[i].Trim() + "'";
                        count++;
                    }
                    else
                    {
                        if (!strItemNos.Contains(l[i].Trim()))
                        {
                            strItemNos += "," + "'" + l[i].Trim() + "'";
                            count++;
                        }
                    }
                }
                string strSql = "select * from B_SampleTypeControl where ControlLabNo='" + LabCode.Trim() + "' and ControlSampleTypeNo in (" + strItemNos + ")";
                DataSet ds = DbHelperSQL.ExecuteDataSet(strSql);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count == count)
                        b = true;
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Weblis.B_SampleTypeControl.CheckIncludeLabCode异常->", ex);
            }
            return b;
        }

        public bool CheckIncludeCenterCode(List<string> l, string LabCode)
        {
            bool b = false;
            try
            {
                string strItemNos = "";
                int count = 0;
                for (int i = 0; i < l.Count; i++)
                {
                    if (strItemNos.Trim() == "")
                    {
                        strItemNos = "'" + l[i].Trim() + "'";
                        count++;
                    }
                    else
                    {
                        if (!strItemNos.Contains(l[i].Trim()))
                        {
                            strItemNos += "," + "'" + l[i].Trim() + "'";
                            count++;
                        }
                    }
                }
                string strSql = "select * from B_SampleTypeControl where ControlLabNo='" + LabCode.Trim() + "' and SampleTypeNo in (" + strItemNos + ")";
                DataSet ds = DbHelperSQL.ExecuteDataSet(strSql);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count == count)
                        b = true;
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Weblis.B_SampleTypeControl.CheckIncludeCenterCode异常->", ex);
            }
            return b;
        }

        public DataSet GetLabCodeNo(string LabCode, List<string> CenterNoList)
        {
            DataSet ds = new DataSet();
            try
            {
                string strItemNos = "";
                for (int i = 0; i < CenterNoList.Count; i++)
                {
                    if (strItemNos.Trim() == "")
                        strItemNos = "'" + CenterNoList[i].Trim() + "'";
                    else
                        strItemNos += "," + "'" + CenterNoList[i].Trim() + "'";
                }
                string strSql = "select SampleTypeNo,ControlLabNo,ControlSampleTypeNo from B_SampleTypeControl where ControlLabNo='" + LabCode.Trim() + "' and SampleTypeNo in (" + strItemNos + ")";
                ds = DbHelperSQL.ExecuteDataSet(strSql);
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Weblis.B_SampleTypeControl.GetLabCodeNo异常->", ex);
            }
            return ds;
        }

        public DataSet GetCenterNo(string LabCode, List<string> LabPrimaryNoList)
        {
            DataSet ds = new DataSet();
            try
            {
                string strItemNos = "";
                for (int i = 0; i < LabPrimaryNoList.Count; i++)
                {
                    if (strItemNos.Trim() == "")
                        strItemNos = "'" + LabPrimaryNoList[i].Trim() + "'";
                    else
                        strItemNos += "," + "'" + LabPrimaryNoList[i].Trim() + "'";
                }
                string strSql = "select SampleTypeNo,ControlLabNo,ControlSampleTypeNo from B_SampleTypeControl where ControlLabNo='" + LabCode.Trim() + "' and ControlSampleTypeNo in (" + strItemNos + ")";
                ds = DbHelperSQL.ExecuteDataSet(strSql);
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Weblis.B_SampleTypeControl.GetCenterNo异常->", ex);
            }
            return ds;
        }

        #endregion



        #region SampleType 字典
        #region 中心字典对照
        /// <summary>
        /// 利用标识列分页
        /// </summary>
        /// <param name="model">model=null时为中心字典表分页；model!=null时为中心--实验室 对照关系分页</param>
        /// <param name="nowPageNum">第几页</param>
        /// <param name="nowPageSize">每页多少行</param>
        /// <returns>DataSet</returns>
        public DataSet GetListByPage(ZhiFang.Model.SampleTypeControl model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            if (model != null)
            {
                //if (model.ControlLabNo != null)
                //{
                //    if (strWhere.Length > 0)
                //        strWhere.Append(" or B_SampleTypeControl.ControlLabNo = '" + model.ControlLabNo + "'  ");
                //    else
                //        strWhere.Append(" and ( B_SampleTypeControl.ControlLabNo = '" + model.ControlLabNo + "'  ");
                //}
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or SampleType.SampleTypeNo like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( SampleType.SampleTypeNo like '%" + model.SearchLikeKey + "%'  ");
                }
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or SampleType.CName like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( SampleType.CName like '%" + model.SearchLikeKey + "%'  ");
                }
                //if (model.SearchLikeKey != null)
                //{
                //    if (strWhere.Length > 0)
                //        strWhere.Append(" or SampleType.EName like '%" + model.SearchLikeKey + "%'  ");
                //    else
                //        strWhere.Append(" and ( SampleType.EName like '%" + model.SearchLikeKey + "%'  ");
                //}
                //if (model.SearchLikeKey != null)
                //{
                //    if (strWhere.Length > 0)
                //        strWhere.Append(" or SampleType.ShortName like '%" + model.SearchLikeKey + "%'  ");
                //    else
                //        strWhere.Append(" and ( SampleType.ShortName like '%" + model.SearchLikeKey + "%'  ");
                //}
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or SampleType.ShortCode like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( SampleType.ShortCode like '%" + model.SearchLikeKey + "%'  ");
                }
                if (strWhere.Length > 0)
                    strWhere.Append(" ) ");
            }
            string strOrderBy = "";
            // strOrderBy = "B_TestItemControl." + model.OrderField;

            //if ((nowPageSize + nowPageNum) != 0)
            //中心
            //已对照
            if (model.ControlState == "1")
                strSql.Append("select *from ( select *from SampleType where 1=1 and SampleTypeNo in (select SampleTypeNo From B_SampleTypeControl where 1=1 and B_SampleTypeControl.ControlLabNo ='" + model.ControlLabNo + "' " + strWhere.ToString() + " )) a left join B_SampleTypeControl on B_SampleTypeControl.SampleTypeNo =a.SampleTypeNo and  B_SampleTypeControl.ControlLabNo='" + model.ControlLabNo + "'");
            //未对照
            else if (model.ControlState == "2")
                strSql.Append("select *from ( select *from SampleType where 1=1 and SampleTypeNo not in (select SampleTypeNo From B_SampleTypeControl where 1=1 and B_SampleTypeControl.ControlLabNo ='" + model.ControlLabNo + "')   " + strWhere.ToString() + " ) a left join B_SampleTypeControl on B_SampleTypeControl.SampleTypeNo =a.SampleTypeNo and  B_SampleTypeControl.ControlLabNo='" + model.ControlLabNo + "'");
            //全部
            else if (model.ControlState == "0")
                strSql.Append("select *from ( select *from SampleType where 1=1 " + strWhere.ToString() + ") a left join B_SampleTypeControl on B_SampleTypeControl.SampleTypeNo =a.SampleTypeNo and  B_SampleTypeControl.ControlLabNo='" + model.ControlLabNo + "' ");
            else
                strSql.Append(" select *From SampleType where 1=2");
            //


            Common.Log.Log.Info("方法名GetListByPage,参数model,nowPageNum,nowPageSize：" + strSql.ToString());
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());





        }
        #endregion

        #region 实验室字典对照
        /// <summary>
        /// 利用标识列分页
        /// </summary>
        /// <param name="model">model=null时为中心字典表分页；model!=null时为中心--实验室 对照关系分页</param>
        /// <param name="nowPageNum">第几页</param>
        /// <param name="nowPageSize">每页多少行</param>
        /// <returns>DataSet</returns>
        public DataSet B_lab_GetListByPage(ZhiFang.Model.SampleTypeControl model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            if (model != null)
            {
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or b_lab_SampleType.LabSampleTypeNo like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( b_lab_SampleType.LabSampleTypeNo like '%" + model.SearchLikeKey + "%'  ");
                }
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or b_lab_SampleType.CName like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( b_lab_SampleType.CName like '%" + model.SearchLikeKey + "%'  ");
                }
                //if (model.SearchLikeKey != null)
                //{
                //    if (strWhere.Length > 0)
                //        strWhere.Append(" or b_lab_SampleType.EName like '%" + model.SearchLikeKey + "%'  ");
                //    else
                //        strWhere.Append(" and ( b_lab_SampleType.EName like '%" + model.SearchLikeKey + "%'  ");
                //}
                //if (model.SearchLikeKey != null)
                //{
                //    if (strWhere.Length > 0)
                //        strWhere.Append(" or b_lab_SampleType.ShortName like '%" + model.SearchLikeKey + "%'  ");
                //    else
                //        strWhere.Append(" and ( b_lab_SampleType.ShortName like '%" + model.SearchLikeKey + "%'  ");
                //}
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or b_lab_SampleType.ShortCode like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( b_lab_SampleType.ShortCode like '%" + model.SearchLikeKey + "%'  ");
                }

                if (strWhere.Length > 0)
                    strWhere.Append(" ) ");
            }
            string strOrderBy = "";
            // if (model.OrderField == "ItemID")
            // {
            // strOrderBy = "B_Lab_TestItem.ItemID";
            // }
            //if ((nowPageSize + nowPageNum) != 0)        

            //实验室
            //已对照
            if (model.ControlState == "1")
                strSql.Append("select *,b.CName CenterCName from ( select *from b_lab_SampleType where 1=1 and labSampleTypeno in (select ControlSampleTypeNo From B_SampleTypeControl where 1=1 and B_SampleTypeControl.ControlLabNo ='" + model.ControlLabNo + "'" + strWhere.ToString() + " ) and  b_lab_SampleType.LabCode='" + model.ControlLabNo + "' " + strWhere.ToString() + "  ) a left join B_SampleTypeControl on B_SampleTypeControl.ControlSampleTypeNo =a.labSampleTypeNo and  B_SampleTypeControl.ControlLabNo='" + model.ControlLabNo + "' left join SampleType b on b.SampleTypeNo = B_SampleTypeControl.SampleTypeNo ");
            //未对照
            else if (model.ControlState == "2")
                strSql.Append("select * ,b.CName CenterCName from (select *from b_lab_SampleType where 1=1 and labSampleTypeno not in (select ControlSampleTypeNo From B_SampleTypeControl where 1=1 and B_SampleTypeControl.ControlLabNo ='" + model.ControlLabNo + "' ) and  b_lab_SampleType.LabCode='" + model.ControlLabNo + "' " + strWhere.ToString() + "  ) a left join B_SampleTypeControl on B_SampleTypeControl.ControlSampleTypeNo =a.labSampleTypeNo and  B_SampleTypeControl.ControlLabNo='" + model.ControlLabNo + "' left join SampleType b on b.SampleTypeNo = B_SampleTypeControl.SampleTypeNo ");
            //全部 
            else if (model.ControlState == "0")
                strSql.Append("select * ,b.CName CenterCName from (select *from b_lab_SampleType where 1=1 and b_lab_SampleType.LabCode='" + model.ControlLabNo + "' " + strWhere.ToString() + "  ) a left join B_SampleTypeControl on B_SampleTypeControl.ControlSampleTypeNo =a.labSampleTypeNo and  B_SampleTypeControl.ControlLabNo='" + model.ControlLabNo + "' left join SampleType b on b.SampleTypeNo = B_SampleTypeControl.SampleTypeNo ");
            else
                strSql.Append(" select *From b_lab_SampleType where 1=2");
            //


            Common.Log.Log.Info("方法名GetListByPage,参数model,nowPageNum,nowPageSize：" + strSql.ToString());
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
        #endregion
        #endregion
    }
}

