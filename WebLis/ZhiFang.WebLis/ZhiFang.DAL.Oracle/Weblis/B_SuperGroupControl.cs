using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.Oracle.weblis
{
    //B_SuperGroupControl
    public partial class B_SuperGroupControl : BaseDALLisDB, IDSuperGroupControl
    {
        public B_SuperGroupControl(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public B_SuperGroupControl()
        {
        }
        D_LogInfo d_log = new D_LogInfo();
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.SuperGroupControl model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.SuperGroupControlNo != null)
            {
                strSql1.Append("SuperGroupControlNo,");
                strSql2.Append("'" + model.SuperGroupControlNo + "',");
            }
            if (model.SuperGroupNo != null)
            {
                strSql1.Append("SuperGroupNo,");
                strSql2.Append("" + model.SuperGroupNo + ",");
            }
            if (model.ControlLabNo != null)
            {
                strSql1.Append("ControlLabNo,");
                strSql2.Append("'" + model.ControlLabNo + "',");
            }
            if (model.ControlSuperGroupNo != null)
            {
                strSql1.Append("ControlSuperGroupNo,");
                strSql2.Append("" + model.ControlSuperGroupNo + ",");
            }
            strSql1.Append("DTimeStampe,");
            strSql2.Append("sysdate+ '1.1234',");

            if (model.AddTime != null)
            {
                strSql1.Append("AddTime,");
                strSql2.Append(" to_date('" + model.AddTime.ToString() + "','YYYY-MM-DD HH24:MI:SS'),");
            }
            if (model.UseFlag != null)
            {
                strSql1.Append("UseFlag,");
                strSql2.Append("" + model.UseFlag + ",");
            }
            strSql.Append("insert into B_SuperGroupControl(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            if (DbHelperSQL.ExecuteNonQuery(strSql.ToString()) > 0)
            {
                return d_log.OperateLog("SuperGroup", "", "", DateTime.Now, 1);
            }
            else
                return -1;

            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("insert into B_SuperGroupControl(");
            //strSql.Append("SuperGroupControlNo,SuperGroupNo,ControlLabNo,ControlSuperGroupNo,UseFlag");
            //strSql.Append(") values (");
            //strSql.Append("@SuperGroupControlNo,@SuperGroupNo,@ControlLabNo,@ControlSuperGroupNo,@UseFlag");
            //strSql.Append(") ");

            //SqlParameter[] parameters = {
            //            new SqlParameter("@SuperGroupControlNo", SqlDbType.Char,50) ,            
            //            new SqlParameter("@SuperGroupNo", SqlDbType.Int,4) ,            
            //            new SqlParameter("@ControlLabNo", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@ControlSuperGroupNo", SqlDbType.Int,4) ,            
            //            new SqlParameter("@UseFlag", SqlDbType.Int,4)             

            //};

            //parameters[0].Value = model.SuperGroupControlNo;
            //parameters[1].Value = model.SuperGroupNo;
            //parameters[2].Value = model.ControlLabNo;
            //parameters[3].Value = model.ControlSuperGroupNo;
            //parameters[4].Value = model.UseFlag;
            //if (DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters) > 0)
            //{
            //    return d_log.OperateLog("SuperGroup", "", "", DateTime.Now, 1);
            //}
            //else
            //    return -1;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.SuperGroupControl model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update B_SuperGroupControl set ");
            if (model.SuperGroupNo != null)
            {
                strSql.Append("SuperGroupNo=" + model.SuperGroupNo + ",");
            }
            if (model.ControlLabNo != null)
            {
                strSql.Append("ControlLabNo='" + model.ControlLabNo + "',");
            }
            if (model.ControlSuperGroupNo != null)
            {
                strSql.Append("ControlSuperGroupNo=" + model.ControlSuperGroupNo + ",");
            }
            if (model.AddTime != null)
            {
                strSql.Append("AddTime=to_date('" + model.AddTime.ToString() + "','YYYY-MM-DD HH24:MI:SS'),");
            }
            if (model.UseFlag != null)
            {
                strSql.Append("UseFlag=" + model.UseFlag + ",");
            }
            strSql.Append("DTimeStampe = sysdate+ '1.1234',");

            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where SuperGroupControlNo='" + model.SuperGroupControlNo + "'");

            if (DbHelperSQL.ExecuteNonQuery(strSql.ToString()) > 0)
            {
                return d_log.OperateLog("SuperGroup", "", "", DateTime.Now, 1);
            }
            else
                return -1;

            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("update B_SuperGroupControl set ");

            //strSql.Append(" SuperGroupControlNo = @SuperGroupControlNo , ");
            //strSql.Append(" SuperGroupNo = @SuperGroupNo , ");
            //strSql.Append(" ControlLabNo = @ControlLabNo , ");
            //strSql.Append(" ControlSuperGroupNo = @ControlSuperGroupNo , ");
            //strSql.Append(" UseFlag = @UseFlag  ");
            //strSql.Append(" where SuperGroupControlNo=@SuperGroupControlNo  ");

            //SqlParameter[] parameters = {


            //new SqlParameter("@SuperGroupControlNo", SqlDbType.Char,50) ,            	

            //new SqlParameter("@SuperGroupNo", SqlDbType.Int,4) ,            	

            //new SqlParameter("@ControlLabNo", SqlDbType.VarChar,50) ,            	

            //new SqlParameter("@ControlSuperGroupNo", SqlDbType.Int,4) ,            	



            //new SqlParameter("@UseFlag", SqlDbType.Int,4)             	

            //};




            //if (model.SuperGroupControlNo != null)
            //{
            //    parameters[0].Value = model.SuperGroupControlNo;
            //}



            //if (model.SuperGroupNo != null)
            //{
            //    parameters[1].Value = model.SuperGroupNo;
            //}



            //if (model.ControlLabNo != null)
            //{
            //    parameters[2].Value = model.ControlLabNo;
            //}



            //if (model.ControlSuperGroupNo != null)
            //{
            //    parameters[3].Value = model.ControlSuperGroupNo;
            //}







            //if (model.UseFlag != null)
            //{
            //    parameters[4].Value = model.UseFlag;
            //}


            //if (DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters) > 0)
            //{
            //    return d_log.OperateLog("SuperGroup", "", "", DateTime.Now, 1);
            //}
            //else
            //    return -1;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string SuperGroupControlNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from B_SuperGroupControl ");
            strSql.Append(" where SuperGroupControlNo='" + SuperGroupControlNo + "' ");


            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());

        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from B_SuperGroupControl ");
            strSql.Append(" where ID in (" + Idlist + ")  ");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());

        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.SuperGroupControl GetModel(string SuperGroupControlNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, SuperGroupControlNo, SuperGroupNo, ControlLabNo, ControlSuperGroupNo, DTimeStampe, AddTime, UseFlag  ");
            strSql.Append("  from B_SuperGroupControl ");
            strSql.Append(" where SuperGroupControlNo=@SuperGroupControlNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@SuperGroupControlNo", SqlDbType.Char,50)};
            parameters[0].Value = SuperGroupControlNo;


            ZhiFang.Model.SuperGroupControl model = new ZhiFang.Model.SuperGroupControl();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                model.SuperGroupControlNo = ds.Tables[0].Rows[0]["SuperGroupControlNo"].ToString();
                if (ds.Tables[0].Rows[0]["SuperGroupNo"].ToString() != "")
                {
                    model.SuperGroupNo = int.Parse(ds.Tables[0].Rows[0]["SuperGroupNo"].ToString());
                }
                model.ControlLabNo = ds.Tables[0].Rows[0]["ControlLabNo"].ToString();
                if (ds.Tables[0].Rows[0]["ControlSuperGroupNo"].ToString() != "")
                {
                    model.ControlSuperGroupNo = int.Parse(ds.Tables[0].Rows[0]["ControlSuperGroupNo"].ToString());
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
            strSql.Append(" FROM B_SuperGroupControl ");
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
            strSql.Append(" FROM B_SuperGroupControl ");
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
        public DataSet GetList(ZhiFang.Model.SuperGroupControl model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM B_SuperGroupControl where 1=1 ");

            if (model.SuperGroupControlNo != null)
            {
                strSql.Append(" and SuperGroupControlNo='" + model.SuperGroupControlNo + "' ");
            }

            if (model.SuperGroupNo != -1)
            {
                strSql.Append(" and SuperGroupNo=" + model.SuperGroupNo + " ");
            }

            if (model.ControlLabNo != null)
            {
                strSql.Append(" and ControlLabNo='" + model.ControlLabNo + "' ");
            }

            if (model.ControlSuperGroupNo != 0)
            {
                strSql.Append(" and ControlSuperGroupNo=" + model.ControlSuperGroupNo + " ");
            }
            if (model.ControlLabNo != null && model.ControlLabNo != "")
            {
                strSql.Append(" and ControlLabNo=" + model.ControlLabNo + " ");
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 获取总记录
        /// </summary>
        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_SuperGroupControl ");
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
        public int GetTotalCount(ZhiFang.Model.SuperGroupControl model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_SuperGroupControl where 1=1 ");

            if (model.SuperGroupControlNo != null)
            {
                strSql.Append(" and SuperGroupControlNo='" + model.SuperGroupControlNo + "' ");
            }

            if (model.SuperGroupNo != null)
            {
                strSql.Append(" and SuperGroupNo=" + model.SuperGroupNo + " ");
            }

            if (model.ControlLabNo != null)
            {
                strSql.Append(" and ControlLabNo='" + model.ControlLabNo + "' ");
            }

            if (model.ControlSuperGroupNo != null)
            {
                strSql.Append(" and ControlSuperGroupNo=" + model.ControlSuperGroupNo + " ");
            }

            if (model.DTimeStampe != null)
            {
                strSql.Append(" and DTimeStampe='" + model.DTimeStampe + "' ");
            }

            if (model.AddTime != null)
            {
                strSql.Append(" and AddTime=to_date('" + model.AddTime.ToString() + "','YYYY-MM-DD HH24:MI:SS'),");

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

        public bool Exists(string SuperGroupControlNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from B_SuperGroupControl ");
            strSql.Append(" where SuperGroupControlNo ='" + SuperGroupControlNo + "'");
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
            strSql.Append("select count(1) from B_SuperGroupControl where 1=1 ");
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
            return DbHelperSQL.GetMaxID("SuperGroupControlNo", "B_SuperGroupControl");
        }

        public DataSet GetList(int Top, ZhiFang.Model.SuperGroupControl model, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");

            strSql.Append(" * ");
            strSql.Append(" FROM B_SuperGroupControl ");


            if (model.SuperGroupControlNo != null)
            {

                strSql.Append(" and SuperGroupControlNo='" + model.SuperGroupControlNo + "' ");
            }

            if (model.SuperGroupNo != null)
            {
                strSql.Append(" and SuperGroupNo=" + model.SuperGroupNo + " ");
            }

            if (model.ControlLabNo != null)
            {

                strSql.Append(" and ControlLabNo='" + model.ControlLabNo + "' ");
            }

            if (model.ControlSuperGroupNo != null)
            {
                strSql.Append(" and ControlSuperGroupNo=" + model.ControlSuperGroupNo + " ");
            }

            if (model.DTimeStampe != null)
            {

                strSql.Append(" and DTimeStampe='" + model.DTimeStampe + "' ");
            }

            if (model.AddTime != null)
            {
                strSql.Append(" and AddTime=to_date('" + model.AddTime.ToString() + "','YYYY-MM-DD HH24:MI:SS'),");
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

        #region IDataBase<SuperGroupControl> 成员
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
                        if (this.Exists(ds.Tables[0].Rows[i]["SuperGroupControlNo"].ToString().Trim()))
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
                strSql.Append("insert into B_SuperGroupControl (");
                strSql.Append("SuperGroupControlNo,SuperGroupNo,ControlLabNo,ControlSuperGroupNo,UseFlag");
                strSql.Append(") values (");
                if (dr.Table.Columns["SuperGroupControlNo"] != null && dr.Table.Columns["SuperGroupControlNo"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["SuperGroupControlNo"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["SuperGroupNo"] != null && dr.Table.Columns["SuperGroupNo"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["SuperGroupNo"].ToString().Trim() + "', ");
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
                if (dr.Table.Columns["ControlSuperGroupNo"] != null && dr.Table.Columns["ControlSuperGroupNo"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["ControlSuperGroupNo"].ToString().Trim() + "', ");
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
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.B_SuperGroupControl.AddByDataRow 同步数据时异常：", ex);
                return 0;
            }
        }
        public int UpdateByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update B_SuperGroupControl set ");

                if (dr.Table.Columns["SuperGroupNo"] != null && dr.Table.Columns["SuperGroupNo"].ToString().Trim() != "")
                {
                    strSql.Append(" SuperGroupNo = '" + dr["SuperGroupNo"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["ControlLabNo"] != null && dr.Table.Columns["ControlLabNo"].ToString().Trim() != "")
                {
                    strSql.Append(" ControlLabNo = '" + dr["ControlLabNo"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["ControlSuperGroupNo"] != null && dr.Table.Columns["ControlSuperGroupNo"].ToString().Trim() != "")
                {
                    strSql.Append(" ControlSuperGroupNo = '" + dr["ControlSuperGroupNo"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["UseFlag"] != null && dr.Table.Columns["UseFlag"].ToString().Trim() != "")
                {
                    strSql.Append(" UseFlag = '" + dr["UseFlag"].ToString().Trim() + "' , ");
                }

                int n = strSql.ToString().LastIndexOf(",");
                strSql.Remove(n, 1);
                strSql.Append(" where SuperGroupControlNo='" + dr["SuperGroupControlNo"].ToString().Trim() + "' ");

                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.B_SuperGroupControl .UpdateByDataRow同步数据时异常：", ex);
                return 0;
            }
        }
        #endregion

        #region IDSuperGroupControl 成员


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
                string strSql = "select * from B_SuperGroupControl where ControlLabNo='" + LabCode.Trim() + "' and ControlSuperGroupNo in (" + strItemNos + ")";
                DataSet ds = DbHelperSQL.ExecuteDataSet(strSql);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count == count)
                        b = true;
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.B_SuperGroupControl.CheckIncludeLabCode异常->", ex);
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
                string strSql = "select * from B_SuperGroupControl where ControlLabNo='" + LabCode.Trim() + "' and SuperGroupNo in (" + strItemNos + ")";
                DataSet ds = DbHelperSQL.ExecuteDataSet(strSql);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count == count)
                        b = true;
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.B_SuperGroupControl.CheckIncludeCenterCode异常->", ex);
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
                string strSql = "select SuperGroupNo,ControlLabNo,ControlSuperGroupNo from B_SuperGroupControl where ControlLabNo='" + LabCode.Trim() + "' and SuperGroupNo in (" + strItemNos + ")";
                ds = DbHelperSQL.ExecuteDataSet(strSql);
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.B_SuperGroupControl.GetLabCodeNo异常->", ex);
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
                string strSql = "select SuperGroupNo,ControlLabNo,ControlSuperGroupNo from B_SuperGroupControl where ControlLabNo='" + LabCode.Trim() + "' and ControlSuperGroupNo in (" + strItemNos + ")";
                ds = DbHelperSQL.ExecuteDataSet(strSql);
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.B_SuperGroupControl.GetCenterNo异常->", ex);
            }
            return ds;
        }

        #endregion


        #region 中心字典对照
        /// <summary>
        /// 利用标识列分页
        /// </summary>
        /// <param name="model">model=null时为中心字典表分页；model!=null时为中心--实验室 对照关系分页</param>
        /// <param name="nowPageNum">第几页</param>
        /// <param name="nowPageSize">每页多少行</param>
        /// <returns>DataSet</returns>
        public DataSet GetListByPage(ZhiFang.Model.SuperGroupControl model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            if (model != null)
            {
                //if (model.ControlLabNo != null)
                //{
                //    if (strWhere.Length > 0)
                //        strWhere.Append(" or B_TestItemControl.ControlLabNo = '" + model.ControlLabNo + "'  ");
                //    else
                //        strWhere.Append(" and ( B_TestItemControl.ControlLabNo = '" + model.ControlLabNo + "'  ");
                //}
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or SuperGroup.SuperGroupNo like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( SuperGroup.SuperGroupNo like '%" + model.SearchLikeKey + "%'  ");
                }
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or SuperGroup.CName like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( SuperGroup.CName like '%" + model.SearchLikeKey + "%'  ");
                }
                //if (model.SearchLikeKey != null)
                //{
                //    if (strWhere.Length > 0)
                //        strWhere.Append(" or SuperGroup.EName like '%" + model.SearchLikeKey + "%'  ");
                //    else
                //        strWhere.Append(" and ( SuperGroup.EName like '%" + model.SearchLikeKey + "%'  ");
                //}
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or SuperGroup.ShortName like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( SuperGroup.ShortName like '%" + model.SearchLikeKey + "%'  ");
                }
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or SuperGroup.ShortCode like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( SuperGroup.ShortCode like '%" + model.SearchLikeKey + "%'  ");
                }
                if (strWhere.Length > 0)
                    strWhere.Append(" ) ");
            }
            string strOrderBy = "";
            strOrderBy = "B_TestItemControl." + model.OrderField;

            //if ((nowPageSize + nowPageNum) != 0)
            //中心
            //已对照
            if (model.ControlState == "1")
                strSql.Append("select *from ( select *from SuperGroup where 1=1 and SuperGroupNo in (select SuperGroupNo From B_SuperGroupControl where 1=1 and B_SuperGroupControl.ControlLabNo ='" + model.ControlLabNo + "' " + strWhere.ToString() + " )) a left join B_SuperGroupControl on B_SuperGroupControl.SuperGroupNo =a.SuperGroupNo and  B_SuperGroupControl.ControlLabNo='" + model.ControlLabNo + "'");
            //未对照
            else if (model.ControlState == "2")
                strSql.Append("select *from ( select *from SuperGroup where 1=1 and SuperGroupNo not in (select SuperGroupNo From B_SuperGroupControl where 1=1 and B_SuperGroupControl.ControlLabNo ='" + model.ControlLabNo + "')   " + strWhere.ToString() + " ) a left join B_SuperGroupControl on B_SuperGroupControl.SuperGroupNo =a.SuperGroupNo and  B_SuperGroupControl.ControlLabNo='" + model.ControlLabNo + "'");
            //全部
            else if (model.ControlState == "0")
                strSql.Append("select *from ( select *from SuperGroup where 1=1 " + strWhere.ToString() + ") a left join B_SuperGroupControl on B_SuperGroupControl.SuperGroupNo =a.SuperGroupNo and  B_SuperGroupControl.ControlLabNo='" + model.ControlLabNo + "' ");
            else
                strSql.Append(" select *From b_lab_SuperGroup where 1=2");
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
        public DataSet B_lab_GetListByPage(ZhiFang.Model.SuperGroupControl model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            if (model != null)
            {
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or b_lab_SuperGroup.LabSuperGroupNo like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( b_lab_SuperGroup.LabSuperGroupNo like '%" + model.SearchLikeKey + "%'  ");
                }
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or b_lab_SuperGroup.CName like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( b_lab_SuperGroup.CName like '%" + model.SearchLikeKey + "%'  ");
                }
                //if (model.SearchLikeKey != null)
                //{
                //    if (strWhere.Length > 0)
                //        strWhere.Append(" or b_lab_SuperGroup.EName like '%" + model.SearchLikeKey + "%'  ");
                //    else
                //        strWhere.Append(" and ( b_lab_SuperGroup.EName like '%" + model.SearchLikeKey + "%'  ");
                //}
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or b_lab_SuperGroup.ShortName like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( b_lab_SuperGroup.ShortName like '%" + model.SearchLikeKey + "%'  ");
                }
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or b_lab_SuperGroup.ShortCode like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( b_lab_SuperGroup.ShortCode like '%" + model.SearchLikeKey + "%'  ");
                }

                if (strWhere.Length > 0)
                    strWhere.Append(" ) ");
            }
            string strOrderBy = "";
            if (model.OrderField == "ItemID")
            {
                strOrderBy = "B_Lab_TestItem.ItemID";
            }
            else if (model.OrderField.ToLower().IndexOf("control") >= 0)
            {
                strOrderBy = "B_TestItemControl." + model.OrderField;
            }
            else
            {
                strOrderBy = "B_Lab_TestItem." + model.OrderField;
            }

            //if ((nowPageSize + nowPageNum) != 0)        

            //实验室
            //已对照
            if (model.ControlState == "1")
                strSql.Append("select a.*,b.SuperGroupNo,b.CName CenterCName from ( select *from b_lab_SuperGroup where 1=1 and labSuperGroupno in (select ControlSuperGroupNo From B_SuperGroupControl where 1=1 and B_SuperGroupControl.ControlLabNo ='" + model.ControlLabNo + "'" + strWhere.ToString() + " ) and  b_lab_SuperGroup.LabCode='" + model.ControlLabNo + "' " + strWhere.ToString() + "  ) a left join B_SuperGroupControl on B_SuperGroupControl.ControlSuperGroupNo =a.labSuperGroupno and  B_SuperGroupControl.ControlLabNo='" + model.ControlLabNo + "' left join SuperGroup b on b.SuperGroupNo = B_SuperGroupControl.SuperGroupNo ");
            //未对照
            else if (model.ControlState == "2")
                strSql.Append("select a.*,b.SuperGroupNo,b.CName CenterCName from (select *from b_lab_SuperGroup where 1=1 and labSuperGroupno not in (select ControlSuperGroupNo From B_SuperGroupControl where 1=1 and B_SuperGroupControl.ControlLabNo ='" + model.ControlLabNo + "' ) and  b_lab_SuperGroup.LabCode='" + model.ControlLabNo + "' " + strWhere.ToString() + "  ) a left join B_SuperGroupControl on B_SuperGroupControl.ControlSuperGroupNo =a.labSuperGroupno and  B_SuperGroupControl.ControlLabNo='" + model.ControlLabNo + "' left join SuperGroup b on b.SuperGroupNo = B_SuperGroupControl.SuperGroupNo ");
            //全部 
            else if (model.ControlState == "0")
                strSql.Append("select a.*,b.SuperGroupNo,b.CName CenterCName from (select *from b_lab_SuperGroup where 1=1 and b_lab_SuperGroup.LabCode='" + model.ControlLabNo + "' " + strWhere.ToString() + "  ) a left join B_SuperGroupControl on B_SuperGroupControl.ControlSuperGroupNo =a.labSuperGroupno and  B_SuperGroupControl.ControlLabNo='" + model.ControlLabNo + "' left join SuperGroup b on b.SuperGroupNo = B_SuperGroupControl.SuperGroupNo ");

            else
                strSql.Append(" select *From b_lab_SuperGroup where 1=2");



            Common.Log.Log.Info("方法名GetListByPage,参数model,nowPageNum,nowPageSize：" + strSql.ToString());
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
        #endregion
    }
}

