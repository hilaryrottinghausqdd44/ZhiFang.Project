using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.Oracle.weblis
{
    //B_FolkTypeControl
    public partial class B_FolkTypeControl : BaseDALLisDB, IDFolkTypeControl
    {
        public B_FolkTypeControl(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public B_FolkTypeControl()
        {
        }
        D_LogInfo d_log = new D_LogInfo();
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.FolkTypeControl model)
        {

            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.FolkControlNo != null)
            {
                strSql1.Append("FolkControlNo,");
                strSql2.Append("'" + model.FolkControlNo + "',");
            }
            if (model.FolkNo != null)
            {
                strSql1.Append("FolkNo,");
                strSql2.Append("" + model.FolkNo + ",");
            }
            if (model.ControlLabNo != null)
            {
                strSql1.Append("ControlLabNo,");
                strSql2.Append("'" + model.ControlLabNo + "',");
            }
            if (model.ControlFolkNo != null)
            {
                strSql1.Append("ControlFolkNo,");
                strSql2.Append("" + model.ControlFolkNo + ",");
            }
            strSql1.Append("DTimeStampe,");
            strSql2.Append("Systimestamp,");

            if (model.AddTime != null)
            {
                strSql1.Append("AddTime,");
                strSql2.Append("to_date('" + model.AddTime.ToString() + "','YYYY-MM-DD HH24:MI:SS'),");
            }
            if (model.UseFlag != null)
            {
                strSql1.Append("UseFlag,");
                strSql2.Append("" + model.UseFlag + ",");
            }
            strSql.Append("insert into B_FolkTypeControl(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            if (DbHelperSQL.ExecuteNonQuery(strSql.ToString()) > 0)
            {
                return d_log.OperateLog("FolkType", "", "", DateTime.Now, 1);
            }
            else
                return -1;

            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("insert into B_FolkTypeControl(");
            //strSql.Append("FolkControlNo,FolkNo,ControlLabNo,ControlFolkNo,UseFlag");
            //strSql.Append(") values (");
            //strSql.Append("@FolkControlNo,@FolkNo,@ControlLabNo,@ControlFolkNo,@UseFlag");
            //strSql.Append(") ");

            //SqlParameter[] parameters = {
            //            new SqlParameter("@FolkControlNo", SqlDbType.Char,50) ,            
            //            new SqlParameter("@FolkNo", SqlDbType.Int,4) ,            
            //            new SqlParameter("@ControlLabNo", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@ControlFolkNo", SqlDbType.Int,4) ,            
            //            new SqlParameter("@UseFlag", SqlDbType.Int,4)             

            //};

            //parameters[0].Value = model.FolkControlNo;
            //parameters[1].Value = model.FolkNo;
            //parameters[2].Value = model.ControlLabNo;
            //parameters[3].Value = model.ControlFolkNo;
            //parameters[4].Value = model.UseFlag;
            //if (DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters) > 0)
            //{
            //    return d_log.OperateLog("FolkType", "", "", DateTime.Now, 1);
            //}
            //else
            //    return -1;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.FolkTypeControl model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update B_FolkTypeControl set ");
            if (model.FolkNo != null)
            {
                strSql.Append("FolkNo=" + model.FolkNo + ",");
            }
            if (model.ControlLabNo != null)
            {
                strSql.Append("ControlLabNo='" + model.ControlLabNo + "',");
            }
            if (model.ControlFolkNo != null)
            {
                strSql.Append("ControlFolkNo=" + model.ControlFolkNo + ",");
            }
            if (model.AddTime != null)
            {
                strSql.Append("AddTime =to_date('" + model.AddTime.ToString() + "','YYYY-MM-DD HH24:MI:SS'),");
            }
            if (model.UseFlag != null)
            {
                strSql.Append("UseFlag=" + model.UseFlag + ",");
            }
            strSql.Append("DTimeStampe = Systimestamp,");
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where FolkControlNo='" + model.FolkControlNo + "'  ");

            if (DbHelperSQL.ExecuteNonQuery(strSql.ToString()) > 0)
            {
                return d_log.OperateLog("FolkType", "", "", DateTime.Now, 1);
            }
            else
                return -1;

            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("update B_FolkTypeControl set ");

            //strSql.Append(" FolkControlNo = @FolkControlNo , ");
            //strSql.Append(" FolkNo = @FolkNo , ");
            //strSql.Append(" ControlLabNo = @ControlLabNo , ");
            //strSql.Append(" ControlFolkNo = @ControlFolkNo , ");
            //strSql.Append(" UseFlag = @UseFlag  ");
            //strSql.Append(" where FolkControlNo=@FolkControlNo  ");

            //SqlParameter[] parameters = {


            //new SqlParameter("@FolkControlNo", SqlDbType.Char,50) ,            	

            //new SqlParameter("@FolkNo", SqlDbType.Int,4) ,            	

            //new SqlParameter("@ControlLabNo", SqlDbType.VarChar,50) ,            	

            //new SqlParameter("@ControlFolkNo", SqlDbType.Int,4) ,            	



            //new SqlParameter("@UseFlag", SqlDbType.Int,4)             	

            //};




            //if (model.FolkControlNo != null)
            //{
            //    parameters[0].Value = model.FolkControlNo;
            //}



            //if (model.FolkNo != null)
            //{
            //    parameters[1].Value = model.FolkNo;
            //}



            //if (model.ControlLabNo != null)
            //{
            //    parameters[2].Value = model.ControlLabNo;
            //}



            //if (model.ControlFolkNo != null)
            //{
            //    parameters[3].Value = model.ControlFolkNo;
            //}







            //if (model.UseFlag != null)
            //{
            //    parameters[4].Value = model.UseFlag;
            //}


            //if (DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters) > 0)
            //{
            //    return d_log.OperateLog("FolkType", "", "", DateTime.Now, 1);
            //}
            //else
            //    return -1;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string FolkControlNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from B_FolkTypeControl ");
            strSql.Append(" where FolkControlNo='" + FolkControlNo + "' ");



            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());

        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from B_FolkTypeControl ");
            strSql.Append(" where ID in (" + Idlist + ")  ");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());

        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.FolkTypeControl GetModel(string FolkControlNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, FolkControlNo, FolkNo, ControlLabNo, ControlFolkNo, DTimeStampe, AddTime, UseFlag  ");
            strSql.Append("  from B_FolkTypeControl ");
            strSql.Append(" where FolkControlNo=@FolkControlNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@FolkControlNo", SqlDbType.Char,50)};
            parameters[0].Value = FolkControlNo;


            ZhiFang.Model.FolkTypeControl model = new ZhiFang.Model.FolkTypeControl();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                model.FolkControlNo = ds.Tables[0].Rows[0]["FolkControlNo"].ToString();
                if (ds.Tables[0].Rows[0]["FolkNo"].ToString() != "")
                {
                    model.FolkNo = int.Parse(ds.Tables[0].Rows[0]["FolkNo"].ToString());
                }
                model.ControlLabNo = ds.Tables[0].Rows[0]["ControlLabNo"].ToString();
                if (ds.Tables[0].Rows[0]["ControlFolkNo"].ToString() != "")
                {
                    model.ControlFolkNo = int.Parse(ds.Tables[0].Rows[0]["ControlFolkNo"].ToString());
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
            strSql.Append(" FROM B_FolkTypeControl ");
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
            strSql.Append(" FROM B_FolkTypeControl ");
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

        /// <summary>
        /// 根据实体获取DataSet
        /// </summary>
        public DataSet GetList(ZhiFang.Model.FolkTypeControl model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM B_FolkTypeControl where 1=1 ");

            if (model.FolkControlNo != null)
            {
                strSql.Append(" and FolkControlNo='" + model.FolkControlNo + "' ");
            }

            if (model.FolkNo != -1)
            {
                strSql.Append(" and FolkNo=" + model.FolkNo + " ");
            }

            if (model.ControlLabNo != null)
            {
                strSql.Append(" and ControlLabNo='" + model.ControlLabNo + "' ");
            }

            if (model.ControlFolkNo != -1)
            {
                strSql.Append(" and ControlFolkNo=" + model.ControlFolkNo + " ");
            }

            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 获取总记录
        /// </summary>
        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_FolkTypeControl ");
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
        public int GetTotalCount(ZhiFang.Model.FolkTypeControl model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_FolkTypeControl where 1=1 ");

            if (model.FolkControlNo != null)
            {
                strSql.Append(" and FolkControlNo='" + model.FolkControlNo + "' ");
            }

            if (model.FolkNo != null)
            {
                strSql.Append(" and FolkNo=" + model.FolkNo + " ");
            }

            if (model.ControlLabNo != null)
            {
                strSql.Append(" and ControlLabNo='" + model.ControlLabNo + "' ");
            }

            if (model.ControlFolkNo != null)
            {
                strSql.Append(" and ControlFolkNo=" + model.ControlFolkNo + " ");
            }

            if (model.DTimeStampe != null)
            {
                strSql.Append(" and DTimeStampe='" + model.DTimeStampe + "' ");
            }

            if (model.AddTime != null)
            {
                strSql.Append(" and AddTime =to_date('" + model.AddTime.ToString() + "','YYYY-MM-DD HH24:MI:SS'),");
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

        public bool Exists(string FolkControlNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from B_FolkTypeControl ");
            strSql.Append(" where FolkControlNo ='" + FolkControlNo + "'");
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

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("FolkControlNo", "B_FolkTypeControl");
        }

        public DataSet GetList(int Top, ZhiFang.Model.FolkTypeControl model, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(" * ");
            strSql.Append(" FROM B_FolkTypeControl ");


            if (model.FolkControlNo != null)
            {

                strSql.Append(" and FolkControlNo='" + model.FolkControlNo + "' ");
            }

            if (model.FolkNo != null)
            {
                strSql.Append(" and FolkNo=" + model.FolkNo + " ");
            }

            if (model.ControlLabNo != null)
            {

                strSql.Append(" and ControlLabNo='" + model.ControlLabNo + "' ");
            }

            if (model.ControlFolkNo != null)
            {
                strSql.Append(" and ControlFolkNo=" + model.ControlFolkNo + " ");
            }

            if (model.DTimeStampe != null)
            {

                strSql.Append(" and DTimeStampe='" + model.DTimeStampe + "' ");
            }

            if (model.AddTime != null)
            {

                strSql.Append("and AddTime =to_date('" + model.AddTime.ToString() + "','YYYY-MM-DD HH24:MI:SS'),");
            }

            if (model.UseFlag != null)
            {
                strSql.Append(" and UseFlag=" + model.UseFlag + " ");
            }
            strSql.Append(" and ROWNUM <= '" + Top + "'");

            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetAllList()
        {
            return GetList("");
        }

        #region IDataBase<FolkTypeControl> 成员
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
                        if (this.Exists(ds.Tables[0].Rows[i]["FolkControlNo"].ToString().Trim()))
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
                strSql.Append("insert into B_FolkTypeControl (");
                strSql.Append("FolkControlNo,FolkNo,ControlLabNo,ControlFolkNo,AddTime,UseFlag");
                strSql.Append(") values (");
                strSql.Append("'" + dr["FolkControlNo"].ToString().Trim() + "','" + dr["FolkNo"].ToString().Trim() + "','" + dr["ControlLabNo"].ToString().Trim() + "','" + dr["ControlFolkNo"].ToString().Trim() + "','" + DateTime.Parse(dr["AddTime"].ToString().Trim()) + "','" + dr["UseFlag"].ToString().Trim() + "'");
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
                strSql.Append("update B_FolkTypeControl set ");

                strSql.Append(" FolkNo = '" + dr["FolkNo"].ToString().Trim() + "' , ");
                strSql.Append(" ControlLabNo = '" + dr["ControlLabNo"].ToString().Trim() + "' , ");
                strSql.Append(" ControlFolkNo = '" + dr["ControlFolkNo"].ToString().Trim() + "' , ");
                strSql.Append(" UseFlag = '" + dr["UseFlag"].ToString().Trim() + "'  ");
                strSql.Append(" where FolkControlNo='" + dr["FolkControlNo"].ToString().Trim() + "' ");

                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region IDFolkTypeControl 成员


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
                string strSql = "select * from B_FolkTypeControl where ControlLabNo='" + LabCode.Trim() + "' and ControlFolkNo in (" + strItemNos + ")";
                DataSet ds = DbHelperSQL.ExecuteDataSet(strSql);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count == count)
                        b = true;
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.B_FolkTypeControl.CheckIncludeLabCode异常->", ex);
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
                string strSql = "select * from B_FolkTypeControl where ControlLabNo='" + LabCode.Trim() + "' and FolkNo in (" + strItemNos + ")";
                DataSet ds = DbHelperSQL.ExecuteDataSet(strSql);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count == count)
                        b = true;
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.B_FolkTypeControl.CheckIncludeCenterCode异常->", ex);
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
                string strSql = "select FolkNo,ControlLabNo,ControlFolkNo from B_FolkTypeControl where ControlLabNo='" + LabCode.Trim() + "' and FolkNo in (" + strItemNos + ")";
                ds = DbHelperSQL.ExecuteDataSet(strSql);
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.B_FolkTypeControl.GetLabCodeNo异常->", ex);
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
                string strSql = "select FolkNo,ControlLabNo,ControlFolkNo from B_FolkTypeControl where ControlLabNo='" + LabCode.Trim() + "' and ControlFolkNo in (" + strItemNos + ")";
                ds = DbHelperSQL.ExecuteDataSet(strSql);
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.B_FolkTypeControl.GetCenterNo异常->", ex);
            }
            return ds;
        }

        #endregion


        #region FolkType 字典
        #region 中心字典对照
        /// <summary>
        /// 利用标识列分页
        /// </summary>
        /// <param name="model">model=null时为中心字典表分页；model!=null时为中心--实验室 对照关系分页</param>
        /// <param name="nowPageNum">第几页</param>
        /// <param name="nowPageSize">每页多少行</param>
        /// <returns>DataSet</returns>
        public DataSet GetListByPage(ZhiFang.Model.FolkTypeControl model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            if (model != null)
            {
                //if (model.ControlLabNo != null)
                //{
                //    if (strWhere.Length > 0)
                //        strWhere.Append(" or B_FolkTypeControl.ControlLabNo = '" + model.ControlLabNo + "'  ");
                //    else
                //        strWhere.Append(" and ( B_FolkTypeControl.ControlLabNo = '" + model.ControlLabNo + "'  ");
                //}
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or FolkType.FolkNo like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( FolkType.FolkNo like '%" + model.SearchLikeKey + "%'  ");
                }
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or FolkType.CName like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( FolkType.CName like '%" + model.SearchLikeKey + "%'  ");
                }
                //if (model.SearchLikeKey != null)
                //{
                //    if (strWhere.Length > 0)
                //        strWhere.Append(" or FolkType.EName like '%" + model.SearchLikeKey + "%'  ");
                //    else
                //        strWhere.Append(" and ( FolkType.EName like '%" + model.SearchLikeKey + "%'  ");
                //}
                //if (model.SearchLikeKey != null)
                //{
                //    if (strWhere.Length > 0)
                //        strWhere.Append(" or FolkType.ShortName like '%" + model.SearchLikeKey + "%'  ");
                //    else
                //        strWhere.Append(" and ( FolkType.ShortName like '%" + model.SearchLikeKey + "%'  ");
                //}
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or FolkType.ShortCode like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( FolkType.ShortCode like '%" + model.SearchLikeKey + "%'  ");
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
                strSql.Append("select *from ( select *from FolkType where 1=1 and FolkNo in (select FolkNo From B_FolkTypeControl where 1=1 and B_FolkTypeControl.ControlLabNo ='" + model.ControlLabNo + "' " + strWhere.ToString() + " )) a left join B_FolkTypeControl on B_FolkTypeControl.FolkNo =a.FolkNo and  B_FolkTypeControl.ControlLabNo='" + model.ControlLabNo + "'");
            //未对照
            else if (model.ControlState == "2")
                strSql.Append("select *from ( select *from FolkType where 1=1 and FolkNo not in (select FolkNo From B_FolkTypeControl where 1=1 and B_FolkTypeControl.ControlLabNo ='" + model.ControlLabNo + "')   " + strWhere.ToString() + " ) a left join B_FolkTypeControl on B_FolkTypeControl.FolkNo =a.FolkNo and  B_FolkTypeControl.ControlLabNo='" + model.ControlLabNo + "'");
            //全部
            else if (model.ControlState == "0")
                strSql.Append("select *from ( select *from FolkType where 1=1 " + strWhere.ToString() + ") a left join B_FolkTypeControl on B_FolkTypeControl.FolkNo =a.FolkNo and  B_FolkTypeControl.ControlLabNo='" + model.ControlLabNo + "' ");
            else
                strSql.Append(" select *From FolkType where 1=2");
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
        public DataSet B_lab_GetListByPage(ZhiFang.Model.FolkTypeControl model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            if (model != null)
            {
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or b_lab_FolkType.labFolkNo like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( b_lab_FolkType.labFolkNo like '%" + model.SearchLikeKey + "%'  ");
                }
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or b_lab_FolkType.CName like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( b_lab_FolkType.CName like '%" + model.SearchLikeKey + "%'  ");
                }
                //if (model.SearchLikeKey != null)
                //{
                //    if (strWhere.Length > 0)
                //        strWhere.Append(" or b_lab_FolkType.EName like '%" + model.SearchLikeKey + "%'  ");
                //    else
                //        strWhere.Append(" and ( b_lab_FolkType.EName like '%" + model.SearchLikeKey + "%'  ");
                //}
                //if (model.SearchLikeKey != null)
                //{
                //    if (strWhere.Length > 0)
                //        strWhere.Append(" or b_lab_FolkType.ShortName like '%" + model.SearchLikeKey + "%'  ");
                //    else
                //        strWhere.Append(" and ( b_lab_FolkType.ShortName like '%" + model.SearchLikeKey + "%'  ");
                //}
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or b_lab_FolkType.ShortCode like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( b_lab_FolkType.ShortCode like '%" + model.SearchLikeKey + "%'  ");
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
                strSql.Append("select b.FolkNo,a.*,b.CName CenterCName from ( select *from b_lab_FolkType where 1=1 and labFolkNo in (select ControlFolkNo From B_FolkTypeControl where 1=1 and B_FolkTypeControl.ControlLabNo ='" + model.ControlLabNo + "'" + strWhere.ToString() + " ) and  b_lab_FolkType.LabCode='" + model.ControlLabNo + "' " + strWhere.ToString() + "  ) a left join B_FolkTypeControl on B_FolkTypeControl.ControlFolkNo =a.labFolkNo and  B_FolkTypeControl.ControlLabNo='" + model.ControlLabNo + "' left join FolkType b on b.FolkNo = B_FolkTypeControl.FolkNo ");
            //未对照
            else if (model.ControlState == "2")
                strSql.Append("select b.FolkNo,a.*,b.CName CenterCName from (select *from b_lab_FolkType where 1=1 and labFolkNo not in (select ControlFolkNo From B_FolkTypeControl where 1=1 and B_FolkTypeControl.ControlLabNo ='" + model.ControlLabNo + "' ) and  b_lab_FolkType.LabCode='" + model.ControlLabNo + "' " + strWhere.ToString() + "  ) a left join B_FolkTypeControl on B_FolkTypeControl.ControlFolkNo =a.labFolkNo and  B_FolkTypeControl.ControlLabNo='" + model.ControlLabNo + "' left join FolkType b on b.FolkNo = B_FolkTypeControl.FolkNo ");
            //全部 
            else if (model.ControlState == "0")
                strSql.Append("select b.FolkNo,a.*,b.CName CenterCName from (select *from b_lab_FolkType where 1=1 and b_lab_FolkType.LabCode='" + model.ControlLabNo + "' " + strWhere.ToString() + "  ) a left join B_FolkTypeControl on B_FolkTypeControl.ControlFolkNo =a.labFolkNo and  B_FolkTypeControl.ControlLabNo='" + model.ControlLabNo + "' left join FolkType b on b.FolkNo = B_FolkTypeControl.FolkNo ");
            else
                strSql.Append(" select *From b_lab_FolkType where 1=2");
            //


            Common.Log.Log.Info("方法名GetListByPage,参数model,nowPageNum,nowPageSize：" + strSql.ToString());
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
        #endregion
        #endregion
    }
}

