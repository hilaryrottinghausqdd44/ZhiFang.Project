using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.MsSql.Weblis
{
    //B_SickTypeControl		
    public partial class B_SickTypeControl : BaseDALLisDB, IDSickTypeControl
    {
        public B_SickTypeControl(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public B_SickTypeControl()
        {
        }
        D_LogInfo d_log = new D_LogInfo();
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.SickTypeControl model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into B_SickTypeControl(");
            strSql.Append("SickTypeControlNo,SickTypeNo,ControlLabNo,ControlSickTypeNo,UseFlag");
            strSql.Append(") values (");
            strSql.Append("@SickTypeControlNo,@SickTypeNo,@ControlLabNo,@ControlSickTypeNo,@UseFlag");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@SickTypeControlNo", SqlDbType.Char,50) ,            
                        new SqlParameter("@SickTypeNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@ControlLabNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ControlSickTypeNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@UseFlag", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.SickTypeControlNo;
            parameters[1].Value = model.SickTypeNo;
            parameters[2].Value = model.ControlLabNo;
            parameters[3].Value = model.ControlSickTypeNo;
            parameters[4].Value = model.UseFlag;
            if (DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters) > 0)
            {
                return d_log.OperateLog("SickType", "", "", DateTime.Now, 1);
            }
            else
                return -1;
        }
        
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.SickTypeControl model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update B_SickTypeControl set ");

            strSql.Append(" SickTypeControlNo = @SickTypeControlNo , ");
            strSql.Append(" SickTypeNo = @SickTypeNo , ");
            strSql.Append(" ControlLabNo = @ControlLabNo , ");
            strSql.Append(" ControlSickTypeNo = @ControlSickTypeNo , ");
            strSql.Append(" UseFlag = @UseFlag  ");
            strSql.Append(" where SickTypeControlNo=@SickTypeControlNo  ");

            SqlParameter[] parameters = {
			            	
                           
            new SqlParameter("@SickTypeControlNo", SqlDbType.Char,50) ,            	
                           
            new SqlParameter("@SickTypeNo", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@ControlLabNo", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@ControlSickTypeNo", SqlDbType.Int,4) ,            	
                        	
                        	
                           
            new SqlParameter("@UseFlag", SqlDbType.Int,4)             	
              
            };




            if (model.SickTypeControlNo != null)
            {
                parameters[0].Value = model.SickTypeControlNo;
            }



            if (model.SickTypeNo != null)
            {
                parameters[1].Value = model.SickTypeNo;
            }



            if (model.ControlLabNo != null)
            {
                parameters[2].Value = model.ControlLabNo;
            }



            if (model.ControlSickTypeNo != null)
            {
                parameters[3].Value = model.ControlSickTypeNo;
            }







            if (model.UseFlag != null)
            {
                parameters[4].Value = model.UseFlag;
            }


            if (DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters) > 0)
            {
                return d_log.OperateLog("SickType", "", "", DateTime.Now, 1);
            }
            else
                return -1;
        }
        
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string SickTypeControlNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from B_SickTypeControl ");
            strSql.Append(" where SickTypeControlNo=@SickTypeControlNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@SickTypeControlNo", SqlDbType.Char,50)};
            parameters[0].Value = SickTypeControlNo;


            return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);

        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from B_SickTypeControl ");
            strSql.Append(" where ID in (" + Idlist + ")  ");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());

        }
        
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.SickTypeControl GetModel(string SickTypeControlNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, SickTypeControlNo, SickTypeNo, ControlLabNo, ControlSickTypeNo, DTimeStampe, AddTime, UseFlag  ");
            strSql.Append("  from B_SickTypeControl ");
            strSql.Append(" where SickTypeControlNo=@SickTypeControlNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@SickTypeControlNo", SqlDbType.Char,50)};
            parameters[0].Value = SickTypeControlNo;


            ZhiFang.Model.SickTypeControl model = new ZhiFang.Model.SickTypeControl();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                model.SickTypeControlNo = ds.Tables[0].Rows[0]["SickTypeControlNo"].ToString();
                if (ds.Tables[0].Rows[0]["SickTypeNo"].ToString() != "")
                {
                    model.SickTypeNo = int.Parse(ds.Tables[0].Rows[0]["SickTypeNo"].ToString());
                }
                model.ControlLabNo = ds.Tables[0].Rows[0]["ControlLabNo"].ToString();
                if (ds.Tables[0].Rows[0]["ControlSickTypeNo"].ToString() != "")
                {
                    model.ControlSickTypeNo = int.Parse(ds.Tables[0].Rows[0]["ControlSickTypeNo"].ToString());
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
            strSql.Append(" FROM B_SickTypeControl ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
        
        /// <summary>
        /// 根据实体获取DataSet
        /// </summary>
        public DataSet GetList(ZhiFang.Model.SickTypeControl model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM B_SickTypeControl where 1=1 ");

            if (model.SickTypeControlNo != null)
            {
                strSql.Append(" and SickTypeControlNo='" + model.SickTypeControlNo + "' ");
            }

            if (model.SickTypeNo != -1)
            {
                strSql.Append(" and SickTypeNo=" + model.SickTypeNo + " ");
            }

            if (model.ControlLabNo != null)
            {
                strSql.Append(" and ControlLabNo='" + model.ControlLabNo + "' ");
            }

            if (model.ControlSickTypeNo != -1)
            {
                strSql.Append(" and ControlSickTypeNo=" + model.ControlSickTypeNo + " ");
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 获取总记录
        /// </summary>
        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_SickTypeControl ");
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
        public int GetTotalCount(ZhiFang.Model.SickTypeControl model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_SickTypeControl where 1=1 ");

            if (model.SickTypeControlNo != null)
            {
                strSql.Append(" and SickTypeControlNo='" + model.SickTypeControlNo + "' ");
            }

            if (model.SickTypeNo != null)
            {
                strSql.Append(" and SickTypeNo=" + model.SickTypeNo + " ");
            }

            if (model.ControlLabNo != null)
            {
                strSql.Append(" and ControlLabNo='" + model.ControlLabNo + "' ");
            }

            if (model.ControlSickTypeNo != null)
            {
                strSql.Append(" and ControlSickTypeNo=" + model.ControlSickTypeNo + " ");
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
        
        public bool Exists(string SickTypeControlNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from B_SickTypeControl ");
            strSql.Append(" where SickTypeControlNo ='" + SickTypeControlNo + "'");
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
            strSql.Append("select count(1) from B_SickTypeControl where 1=1 ");
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
            return DbHelperSQL.GetMaxID("SickTypeControlNo", "B_SickTypeControl");
        }

        public DataSet GetList(int Top, ZhiFang.Model.SickTypeControl model, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM B_SickTypeControl ");


            if (model.SickTypeControlNo != null)
            {

                strSql.Append(" and SickTypeControlNo='" + model.SickTypeControlNo + "' ");
            }

            if (model.SickTypeNo != null)
            {
                strSql.Append(" and SickTypeNo=" + model.SickTypeNo + " ");
            }

            if (model.ControlLabNo != null)
            {

                strSql.Append(" and ControlLabNo='" + model.ControlLabNo + "' ");
            }

            if (model.ControlSickTypeNo != null)
            {
                strSql.Append(" and ControlSickTypeNo=" + model.ControlSickTypeNo + " ");
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
                        if (this.Exists(ds.Tables[0].Rows[i]["SickTypeControlNo"].ToString().Trim()))
                        {
                            count += this.UpdateByDataRow(dr);
                        }
                        else
                        {
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
                strSql.Append("insert into B_SickTypeControl (");
                strSql.Append("SickTypeControlNo,SickTypeNo,ControlLabNo,ControlSickTypeNo,UseFlag");
                strSql.Append(") values (");

                if (dr.Table.Columns["SickTypeControlNo"] != null && dr.Table.Columns["SickTypeControlNo"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["SickTypeControlNo"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["SickTypeNo"] != null && dr.Table.Columns["SickTypeNo"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["SickTypeNo"].ToString().Trim() + "', ");
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

                if (dr.Table.Columns["ControlSickTypeNo"] != null && dr.Table.Columns["ControlSickTypeNo"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["ControlSickTypeNo"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["UseFlag"] != null && dr.Table.Columns["UseFlag"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["UseFlag"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                int n = strSql.ToString().LastIndexOf(",");
                strSql.Remove(n, 1);
                strSql.Append(") ");
                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Weblis.B_SickTypeControl.AddByDataRow 同步数据时异常：", ex);
                return 0;
            }
        }
        public int UpdateByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update B_SickTypeControl set ");


                if (dr.Table.Columns["SickTypeNo"] != null && dr.Table.Columns["SickTypeNo"].ToString().Trim() != "")
                {
                    strSql.Append(" SickTypeNo = '" + dr["SickTypeNo"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["ControlLabNo"] != null && dr.Table.Columns["ControlLabNo"].ToString().Trim() != "")
                {
                    strSql.Append(" ControlLabNo = '" + dr["ControlLabNo"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["ControlSickTypeNo"] != null && dr.Table.Columns["ControlSickTypeNo"].ToString().Trim() != "")
                {
                    strSql.Append(" ControlSickTypeNo = '" + dr["ControlSickTypeNo"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["UseFlag"] != null && dr.Table.Columns["UseFlag"].ToString().Trim() != "")
                {
                    strSql.Append(" UseFlag = '" + dr["UseFlag"].ToString().Trim() + "' , ");
                }


                int n = strSql.ToString().LastIndexOf(",");
                strSql.Remove(n, 1);
                strSql.Append(" where SickTypeControlNo='" + dr["SickTypeControlNo"].ToString().Trim() + "' ");

                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Weblis.B_SickTypeControl .UpdateByDataRow同步数据时异常：", ex);
                return 0;
            }
        }



        #region IDSickTypeControl 成员



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
                string strSql = "select * from B_SickTypeControl where ControlLabNo='" + LabCode.Trim() + "' and ControlSickTypeNo in (" + strItemNos + ")";
                DataSet ds = DbHelperSQL.ExecuteDataSet(strSql);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count == count)
                        b = true;
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Weblis.B_SickTypeControl.CheckIncludeLabCode异常->", ex);
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
                string strSql = "select * from B_SickTypeControl where ControlLabNo='" + LabCode.Trim() + "' and SickTypeNo in (" + strItemNos + ")";
                DataSet ds = DbHelperSQL.ExecuteDataSet(strSql);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count == count)
                        b = true;
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Weblis.B_SickTypeControl.CheckIncludeCenterCode异常->", ex);
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
                string strSql = "select SickTypeNo,ControlLabNo,ControlSickTypeNo from B_SickTypeControl where ControlLabNo='" + LabCode.Trim() + "' and SickTypeNo in (" + strItemNos + ")";
                ds = DbHelperSQL.ExecuteDataSet(strSql);
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Weblis.B_SickTypeControl.GetLabCodeNo异常->", ex);
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
                string strSql = "select SickTypeNo,ControlLabNo,ControlSickTypeNo from B_SickTypeControl where ControlLabNo='" + LabCode.Trim() + "' and ControlSickTypeNo in (" + strItemNos + ")";
                ds = DbHelperSQL.ExecuteDataSet(strSql);
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Weblis.B_SickTypeControl.GetCenterNo异常->", ex);
            }
            return ds;
        }

        #endregion



        #region SickType 字典
        #region 中心字典对照
        /// <summary>
        /// 利用标识列分页
        /// </summary>
        /// <param name="model">model=null时为中心字典表分页；model!=null时为中心--实验室 对照关系分页</param>
        /// <param name="nowPageNum">第几页</param>
        /// <param name="nowPageSize">每页多少行</param>
        /// <returns>DataSet</returns>
        public DataSet GetListByPage(ZhiFang.Model.SickTypeControl model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            if (model != null)
            {
                //if (model.ControlLabNo != null)
                //{
                //    if (strWhere.Length > 0)
                //        strWhere.Append(" or B_SickTypeControl.ControlLabNo = '" + model.ControlLabNo + "'  ");
                //    else
                //        strWhere.Append(" and ( B_SickTypeControl.ControlLabNo = '" + model.ControlLabNo + "'  ");
                //}
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or SickType.SickTypeNo like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( SickType.SickTypeNo like '%" + model.SearchLikeKey + "%'  ");
                }
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or SickType.CName like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( SickType.CName like '%" + model.SearchLikeKey + "%'  ");
                }
                //if (model.SearchLikeKey != null)
                //{
                //    if (strWhere.Length > 0)
                //        strWhere.Append(" or SickType.EName like '%" + model.SearchLikeKey + "%'  ");
                //    else
                //        strWhere.Append(" and ( SickType.EName like '%" + model.SearchLikeKey + "%'  ");
                //}
                //if (model.SearchLikeKey != null)
                //{
                //    if (strWhere.Length > 0)
                //        strWhere.Append(" or SickType.ShortName like '%" + model.SearchLikeKey + "%'  ");
                //    else
                //        strWhere.Append(" and ( SickType.ShortName like '%" + model.SearchLikeKey + "%'  ");
                //}
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or SickType.ShortCode like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( SickType.ShortCode like '%" + model.SearchLikeKey + "%'  ");
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
                strSql.Append("select *from ( select *from SickType where 1=1 and SickTypeNo in (select SickTypeNo From B_SickTypeControl where 1=1 and B_SickTypeControl.ControlLabNo ='" + model.ControlLabNo + "' " + strWhere.ToString() + " )) a left join B_SickTypeControl on B_SickTypeControl.SickTypeNo =a.SickTypeNo and  B_SickTypeControl.ControlLabNo='" + model.ControlLabNo + "'");
            //未对照
            else if (model.ControlState == "2")
                strSql.Append("select *from ( select *from SickType where 1=1 and SickTypeNo not in (select SickTypeNo From B_SickTypeControl where 1=1 and B_SickTypeControl.ControlLabNo ='" + model.ControlLabNo + "')   " + strWhere.ToString() + " ) a left join B_SickTypeControl on B_SickTypeControl.SickTypeNo =a.SickTypeNo and  B_SickTypeControl.ControlLabNo='" + model.ControlLabNo + "'");
            //全部
            else if (model.ControlState == "0")
                strSql.Append("select *from ( select *from SickType where 1=1 " + strWhere.ToString() + ") a left join B_SickTypeControl on B_SickTypeControl.SickTypeNo =a.SickTypeNo and  B_SickTypeControl.ControlLabNo='" + model.ControlLabNo + "' ");
            else
                strSql.Append(" select *From SickType where 1=2");
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
        public DataSet B_lab_GetListByPage(ZhiFang.Model.SickTypeControl model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            if (model != null)
            {
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or b_lab_SickType.labSickTypeNo like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( b_lab_SickType.labSickTypeNo like '%" + model.SearchLikeKey + "%'  ");
                }
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or b_lab_SickType.CName like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( b_lab_SickType.CName like '%" + model.SearchLikeKey + "%'  ");
                }
                //if (model.SearchLikeKey != null)
                //{
                //    if (strWhere.Length > 0)
                //        strWhere.Append(" or b_lab_SickType.EName like '%" + model.SearchLikeKey + "%'  ");
                //    else
                //        strWhere.Append(" and ( b_lab_SickType.EName like '%" + model.SearchLikeKey + "%'  ");
                //}
                //if (model.SearchLikeKey != null)
                //{
                //    if (strWhere.Length > 0)
                //        strWhere.Append(" or b_lab_SickType.ShortName like '%" + model.SearchLikeKey + "%'  ");
                //    else
                //        strWhere.Append(" and ( b_lab_SickType.ShortName like '%" + model.SearchLikeKey + "%'  ");
                //}
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or b_lab_SickType.ShortCode like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( b_lab_SickType.ShortCode like '%" + model.SearchLikeKey + "%'  ");
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
                strSql.Append("select *,b.CName CenterCName from ( select *from b_lab_SickType where 1=1 and labSickTypeno in (select ControlSickTypeNo From B_SickTypeControl where 1=1 and B_SickTypeControl.ControlLabNo ='" + model.ControlLabNo + "'" + strWhere.ToString() + " ) and  b_lab_SickType.LabCode='" + model.ControlLabNo + "' " + strWhere.ToString() + "  ) a left join B_SickTypeControl on B_SickTypeControl.ControlSickTypeNo =a.labSickTypeNo and  B_SickTypeControl.ControlLabNo='" + model.ControlLabNo + "' left join SickType b on b.SickTypeNo = B_SickTypeControl.SickTypeNo");
            //未对照
            else if (model.ControlState == "2")
                strSql.Append("select *,b.CName CenterCName from (select *from b_lab_SickType where 1=1 and labSickTypeno not in (select ControlSickTypeNo From B_SickTypeControl where 1=1 and B_SickTypeControl.ControlLabNo ='" + model.ControlLabNo + "' ) and  b_lab_SickType.LabCode='" + model.ControlLabNo + "' " + strWhere.ToString() + "  ) a left join B_SickTypeControl on B_SickTypeControl.ControlSickTypeNo =a.labSickTypeNo and  B_SickTypeControl.ControlLabNo='" + model.ControlLabNo + "' left join SickType b on b.SickTypeNo = B_SickTypeControl.SickTypeNo");
            //全部 
            else if (model.ControlState == "0")
                strSql.Append("select *,b.CName CenterCName from (select *from b_lab_SickType where 1=1 and b_lab_SickType.LabCode='" + model.ControlLabNo + "' " + strWhere.ToString() + "  ) a left join B_SickTypeControl on B_SickTypeControl.ControlSickTypeNo =a.labSickTypeNo and  B_SickTypeControl.ControlLabNo='" + model.ControlLabNo + "' left join SickType b on b.SickTypeNo = B_SickTypeControl.SickTypeNo ");
            else
                strSql.Append(" select *From b_lab_SickType where 1=2");
            //


            Common.Log.Log.Info("方法名GetListByPage,参数model,nowPageNum,nowPageSize：" + strSql.ToString());
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
        #endregion
        #endregion
    }
}

