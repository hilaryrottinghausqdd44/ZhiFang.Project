using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.Oracle.weblis
{
    //B_TestItemControl
    public partial class B_TestItemControl : BaseDALLisDB, IDTestItemControl, IDBatchCopy
    {
        public B_TestItemControl(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public B_TestItemControl()
        {
        }
        D_LogInfo d_log = new D_LogInfo();
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.TestItemControl model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.ItemControlNo != null)
            {
                strSql1.Append("ItemControlNo,");
                strSql2.Append("'" + model.ItemControlNo + "',");
            }
            if (model.ItemNo != null)
            {
                strSql1.Append("ItemNo,");
                strSql2.Append("'" + model.ItemNo + "',");
            }
            if (model.ControlLabNo != null)
            {
                strSql1.Append("ControlLabNo,");
                strSql2.Append("'" + model.ControlLabNo + "',");
            }
            if (model.ControlItemNo != null)
            {
                strSql1.Append("ControlItemNo,");
                strSql2.Append("'" + model.ControlItemNo + "',");
            }
            strSql1.Append("DTimeStampe,");
            strSql2.Append("Systimestamp,");

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
            strSql.Append("insert into B_TestItemControl(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");

            int rows = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            if (rows > 0)
            {
                return d_log.OperateLog("TestItem", "", "", DateTime.Now, 1);

            }
            else
            {
                return -1;
            }

            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("insert into B_TestItemControl(");
            //strSql.Append("ItemControlNo,ItemNo,ControlLabNo,ControlItemNo");
            //strSql.Append(") values (");
            //strSql.Append("@ItemControlNo,@ItemNo,@ControlLabNo,@ControlItemNo");
            //strSql.Append(") ");
            ////strSql.Append(";select @@IDENTITY");		
            //SqlParameter[] parameters = {
            //            new SqlParameter("@ItemControlNo", SqlDbType.Char,50) ,            
            //            new SqlParameter("@ItemNo", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@ControlLabNo", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@ControlItemNo", SqlDbType.VarChar,50)            

            //};

            //parameters[0].Value = model.ItemControlNo;
            //parameters[1].Value = model.ItemNo;
            //parameters[2].Value = model.ControlLabNo;
            //parameters[3].Value = model.ControlItemNo;
            //int rows = DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);
            //if (rows > 0)
            //{
            //    return d_log.OperateLog("TestItem", "", "", DateTime.Now, 1);

            //}
            //else
            //{
            //    return -1;
            //}
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.TestItemControl model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update B_TestItemControl set ");
            if (model.ItemNo != null)
            {
                strSql.Append("ItemNo='" + model.ItemNo + "',");
            }
            if (model.ControlLabNo != null)
            {
                strSql.Append("ControlLabNo='" + model.ControlLabNo + "',");
            }
            if (model.ControlItemNo != null)
            {
                strSql.Append("ControlItemNo='" + model.ControlItemNo + "',");
            }
            if (model.AddTime != null)
            {
                strSql.Append("AddTime=to_date('" + model.AddTime.ToString() + "','YYYY-MM-DD HH24:MI:SS'),");
            }
            if (model.UseFlag != null)
            {
                strSql.Append("UseFlag=" + model.UseFlag + ",");
            }

            strSql.Append("DTimeStampe = Systimestamp,");

            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where ItemNo='" + model.ItemNo + "' and ControlLabNo='" + model.ControlLabNo + "' ");
            int rows = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            if (rows > 0)
            {
                return d_log.OperateLog("TestItem", "", "", DateTime.Now, 1);

            }
            else
            {
                return -1;
            }

            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("update B_TestItemControl set ");

            //strSql.Append(" ItemNo = @ItemNo , ");
            //strSql.Append(" ControlLabNo = @ControlLabNo , ");
            //strSql.Append(" ControlItemNo = @ControlItemNo , ");
            //strSql.Append(" UseFlag = @UseFlag  ");
            //strSql.Append(" where ItemNo=@ItemNo and ControlLabNo=@ControlLabNo ");

            //SqlParameter[] parameters = {                     
            //            new SqlParameter("@ItemNo", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@ControlLabNo", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@ControlItemNo", SqlDbType.VarChar,50) ,          
            //            new SqlParameter("@UseFlag", SqlDbType.Int,4)             

            //};



            //if (model.ItemNo != null)
            //{
            //    parameters[0].Value = model.ItemNo;
            //}

            //if (model.ControlLabNo != null)
            //{
            //    parameters[1].Value = model.ControlLabNo;
            //}

            //if (model.ControlItemNo != null)
            //{
            //    parameters[2].Value = model.ControlItemNo;
            //}

            //if (model.UseFlag != null)
            //{
            //    parameters[3].Value = model.UseFlag;
            //}

            //int rows = DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);
            //if (rows > 0)
            //{
            //    return d_log.OperateLog("TestItem", "", "", DateTime.Now, 1);

            //}
            //else
            //{
            //    return -1;
            //}
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from B_TestItemControl ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
};
            parameters[0].Value = Id;


            return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);

        }

        public int Delete(string ItemControlNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from B_TestItemControl ");
            strSql.Append(" where ItemControlNo='" + ItemControlNo + "' ");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from B_TestItemControl ");
            strSql.Append(" where ID in (" + Idlist + ")  ");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());

        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.TestItemControl GetModel(string ItemNo, string LabCode, string LabItemNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, ItemControlNo, ItemNo, ControlLabNo, ControlItemNo, DTimeStampe, AddTime, UseFlag  ");
            strSql.Append("  from B_TestItemControl ");
            strSql.Append(" where ItemNo=@ItemNo and ControlLabNo=@ControlLabNo");
            SqlParameter[] parameters = {          
                        new SqlParameter("@ItemNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ControlLabNo", SqlDbType.VarChar,50)       
              
            };

            parameters[0].Value = ItemNo;
            parameters[1].Value = LabCode;


            ZhiFang.Model.TestItemControl model = new ZhiFang.Model.TestItemControl();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                model.ItemControlNo = ds.Tables[0].Rows[0]["ItemControlNo"].ToString();
                model.ItemNo = ds.Tables[0].Rows[0]["ItemNo"].ToString();
                model.ControlLabNo = ds.Tables[0].Rows[0]["ControlLabNo"].ToString();
                model.ControlItemNo = ds.Tables[0].Rows[0]["ControlItemNo"].ToString();

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
            strSql.Append(" FROM B_TestItemControl ");
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
            strSql.Append(" FROM B_TestItemControl ");
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
        public DataSet GetList(ZhiFang.Model.TestItemControl model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM B_TestItemControl where 1=1 ");

            if (model.ItemControlNo != null)
            {
                strSql.Append(" and ItemControlNo='" + model.ItemControlNo + "' ");
            }

            if (model.ItemNo != null)
            {
                strSql.Append(" and ItemNo='" + model.ItemNo + "' ");
            }

            if (model.ControlLabNo != null)
            {
                strSql.Append(" and ControlLabNo='" + model.ControlLabNo + "' ");
            }

            if (model.ControlItemNo != null)
            {
                strSql.Append(" and ControlItemNo='" + model.ControlItemNo + "' ");
            }

            if (model.UseFlag != null)
            {
                strSql.Append(" and UseFlag=" + model.UseFlag + " ");
            }
            Common.Log.Log.Info(strSql.ToString());
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 获取总记录的数量
        /// </summary>
        /// <returns></returns>
        public int GetTotalCount(ZhiFang.Model.TestItemControl model)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select count(*) FROM B_TestItemControl where 1=1 ");
                if (model.ItemControlNo != null)
                {
                    strSql.Append(" and ItemControlNo='" + model.ItemControlNo + "' ");
                }

                if (model.ItemNo != null)
                {
                    strSql.Append(" and ItemNo='" + model.ItemNo + "' ");
                }

                if (model.ControlLabNo != null)
                {
                    strSql.Append(" and ControlLabNo='" + model.ControlLabNo + "' ");
                }

                if (model.ControlItemNo != null)
                {
                    strSql.Append(" and ControlItemNo='" + model.ControlItemNo + "' ");
                }

                if (model.UseFlag != null)
                {
                    strSql.Append(" and UseFlag=" + model.UseFlag + " ");
                }
                Common.Log.Log.Info(strSql.ToString());
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
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error(e.ToString());
                return 0;
            }
        }
        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_TestItemControl");
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
        public bool Exists(string ItemControlNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from B_TestItemControl ");
            strSql.Append(" where ItemControlNo ='" + ItemControlNo + "'");
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
            strSql.Append("select count(1) from B_TestItemControl where 1=1 ");
            if (ht.Count > 0)
            {
                foreach (System.Collections.DictionaryEntry item in ht)
                {
                    strSql.Append(" and " + item.Key.ToString().Trim() + "='" + item.Value + "' ");
                }
                string strCount = DbHelperSQL.ExecuteScalar(strSql.ToString());
                DbHelperSQL.Dispose();
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

        #region IDBatchCopy 成员

        public bool CopyToLab(List<string> lst)
        {
            System.Collections.ArrayList arrySql = new System.Collections.ArrayList();
            int countflag = 0;
            StringBuilder strSql = new StringBuilder();
            if (lst.Count > 0)
            {
                for (int i = 0; i < lst.Count; i++)
                {
                    strSql.Append("insert into B_TestItemControl (ItemControlNo,ItemNo,ControlLabNo,ControlItemNo,UseFlag) ");
                    strSql.Append(" select '" + lst[i].Trim() + "'+'_'+ItemNo+'_'+ItemNo as ItemControlNo,ItemNo,'" + lst[i].Trim() + "' as ControlLabNo,ItemNo,UseFlag from B_TestItem ");

                    arrySql.Add(strSql.ToString());
                    if (DbHelperSQL.ExecuteNonQuery(strSql.ToString()) > 0)
                        countflag++;
                }
            }
            if (countflag == lst.Count)
                return true;
            else
                return false;
        }

        #endregion

        #region IDataBase<TestItemControl> 成员

        public int GetMaxId()
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(int Top, Model.TestItemControl t, string filedOrder)
        {
            throw new NotImplementedException();
        }

        public DataSet GetAllList()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDataBase<TestItemControl> 成员
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
                        if (this.Exists(ds.Tables[0].Rows[i]["ItemControlNo"].ToString().Trim()))
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
                strSql.Append("insert into B_TestItemControl (");
                strSql.Append("ItemControlNo,ItemNo,ControlLabNo,ControlItemNo,UseFlag");
                strSql.Append(") values (");
                if (dr.Table.Columns["ItemControlNo"] != null && dr.Table.Columns["ItemControlNo"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["ItemControlNo"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" '" + DBNull.Value + "', ");
                }
                if (dr.Table.Columns["ItemNo"] != null && dr.Table.Columns["ItemNo"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["ItemNo"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" '" + DBNull.Value + "', ");
                }
                if (dr.Table.Columns["ControlLabNo"] != null && dr.Table.Columns["ControlLabNo"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["ControlLabNo"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" '" + DBNull.Value + "', ");
                }
                if (dr.Table.Columns["ControlItemNo"] != null && dr.Table.Columns["ControlItemNo"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["ControlItemNo"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" '" + DBNull.Value + "', ");
                }

                if (dr.Table.Columns["UseFlag"] != null && dr.Table.Columns["UseFlag"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["UseFlag"].ToString().Trim() + "' ");
                }
                else
                {
                    strSql.Append(" '" + DBNull.Value + "' ");
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
                strSql.Append("update B_TestItemControl set ");


                if (dr.Table.Columns["ItemNo"] != null && dr.Table.Columns["ItemNo"].ToString().Trim() != "")
                {
                    strSql.Append(" ItemNo = '" + dr["ItemNo"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["ControlLabNo"] != null && dr.Table.Columns["ControlLabNo"].ToString().Trim() != "")
                {
                    strSql.Append(" ControlLabNo = '" + dr["ControlLabNo"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["ControlItemNo"] != null && dr.Table.Columns["ControlItemNo"].ToString().Trim() != "")
                {
                    strSql.Append(" ControlItemNo = '" + dr["ControlItemNo"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["UseFlag"] != null && dr.Table.Columns["UseFlag"].ToString().Trim() != "")
                {
                    strSql.Append(" UseFlag = '" + dr["UseFlag"].ToString().Trim() + "' , ");
                }
                int n = strSql.ToString().LastIndexOf(",");
                strSql.Remove(n, 1);
                strSql.Append(" where ItemControlNo='" + dr["ItemControlNo"].ToString().Trim() + "' ");

                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region IDBatchCopy 成员


        public int DeleteByDataRow(DataRow dr)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDTestItemControl 成员


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
                string strSql = "select id,itemcontrolno,itemno,controllabno,controlitemno,addtime,useflag from B_TestItemControl where ControlLabNo='" + LabCode.Trim() + "' and ControlItemNo in (" + strItemNos + ")";
                try
                {

                }
                catch (Exception)
                {

                    throw;
                }
                DataSet ds = DbHelperSQL.ExecuteDataSet(strSql);
                DbHelperSQL.Dispose();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count == count)
                        b = true;
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.B_TestItemControl.CheckIncludeLabCode异常->", ex);
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
                string strSql = "select id,itemcontrolno,itemno,controllabno,controlitemno,addtime,useflag from B_TestItemControl where ControlLabNo='" + LabCode.Trim() + "' and ItemNo in (" + strItemNos + ")";
                DataSet ds = DbHelperSQL.ExecuteDataSet(strSql);
                DbHelperSQL.Dispose();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count == count)
                        b = true;
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.B_TestItemControl.CheckIncludeCenterCode异常->", ex);
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
                string strSql = "select ItemNo,ControlLabNo,ControlItemNo from B_TestItemControl where ControlLabNo='" + LabCode.Trim() + "' and ItemNo in (" + strItemNos + ")";
                Common.Log.Log.Info("GetLabCodeNo:" + strSql);
                ds = DbHelperSQL.ExecuteDataSet(strSql);
                DbHelperSQL.Dispose();
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.B_TestItemControl.GetLabCodeNo异常->", ex);
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
                string strSql = "select ItemNo,ControlLabNo,ControlItemNo from B_TestItemControl where ControlLabNo='" + LabCode.Trim() + "' and ControlItemNo in (" + strItemNos + ")";
                ds = DbHelperSQL.ExecuteDataSet(strSql);
                DbHelperSQL.Dispose();
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.B_TestItemControl.GetCenterNo异常->", ex);
            }
            return ds;
        }

        #endregion

        #region IDTestItemControl 成员


        public DataSet GetLabItemCodeMapListByNRequestLabCodeAndFormNo(string LabCode, string NRequestFormNo)
        {
            DataSet ds = new DataSet();
            try
            {
                string strSql = "SELECT  B_Lab_TestItem.CName, B_TestItemControl.ControlLabNo, B_TestItemControl.ControlItemNo, B_Lab_TestItem.Color, B_TestItemControl.ItemNo,B_Lab_TestItem.price FROM         B_Lab_TestItem INNER JOIN B_TestItemControl ON B_Lab_TestItem.ItemNo = B_TestItemControl.ControlItemNo AND B_Lab_TestItem.LabCode = B_TestItemControl.ControlLabNo";
                strSql = strSql + " where (B_TestItemControl.ItemNo in (select ParItemNo from nrequestitem where  nrequestitem.nrequestformno=" + NRequestFormNo + ")or B_TestItemControl.ItemNo in (select CombiItemNo from nrequestitem where  nrequestitem.nrequestformno=" + NRequestFormNo + ")) ";
                strSql = strSql + "and B_TestItemControl.ControlLabNo='" + LabCode + "' ";
                ds = DbHelperSQL.ExecuteDataSet(strSql);
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Weblis.B_TestItemControl.GetLabItemCodeMapListByNRequestLabCodeAndFormNo--strSql:" + strSql);
                DbHelperSQL.Dispose();
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Weblis.B_TestItemControl.GetLabItemCodeMapListByNRequestLabCodeAndFormNo异常->", ex);
            }
            return ds;
        }

        #endregion


        #region TestItem 字典
        #region 中心字典对照
        /// <summary>
        /// 利用标识列分页
        /// </summary>
        /// <param name="model">model=null时为中心字典表分页；model!=null时为中心--实验室 对照关系分页</param>
        /// <param name="nowPageNum">第几页</param>
        /// <param name="nowPageSize">每页多少行</param>
        /// <returns>DataSet</returns>
        public DataSet GetListByPage(ZhiFang.Model.TestItemControl model, int nowPageNum, int nowPageSize)
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
                        strWhere.Append(" or TestItem.ItemNo like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( TestItem.ItemNo like '%" + model.SearchLikeKey + "%'  ");
                }
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or TestItem.CName like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( TestItem.CName like '%" + model.SearchLikeKey + "%'  ");
                }
                //if (model.SearchLikeKey != null)
                //{
                //    if (strWhere.Length > 0)
                //        strWhere.Append(" or TestItem.EName like '%" + model.SearchLikeKey + "%'  ");
                //    else
                //        strWhere.Append(" and ( TestItem.EName like '%" + model.SearchLikeKey + "%'  ");
                //}
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or TestItem.ShortName like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( TestItem.ShortName like '%" + model.SearchLikeKey + "%'  ");
                }
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or TestItem.ShortCode like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( TestItem.ShortCode like '%" + model.SearchLikeKey + "%'  ");
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
                strSql.Append("select *from ( select *from TestItem where 1=1 and ItemNo in (select ItemNo From B_TestItemControl where 1=1 and B_TestItemControl.ControlLabNo ='" + model.ControlLabNo + "' " + strWhere.ToString() + " )) a left join B_TestItemControl on B_TestItemControl.ItemNo =a.ItemNo where  B_TestItemControl.ControlLabNo='" + model.ControlLabNo + "'");
            //未对照
            else if (model.ControlState == "2")
                strSql.Append("select *from ( select *from TestItem where 1=1 and ItemNo not in (select ItemNo From B_TestItemControl where 1=1 and B_TestItemControl.ControlLabNo ='" + model.ControlLabNo + "')   " + strWhere.ToString() + " ) a left join B_TestItemControl on B_TestItemControl.ItemNo =a.ItemNo where  B_TestItemControl.ControlLabNo='" + model.ControlLabNo + "'");
            //全部
            else if (model.ControlState == "0")
                strSql.Append("select *from ( select *from TestItem where 1=1 " + strWhere.ToString() + ") a left join B_TestItemControl on B_TestItemControl.ItemNo =a.ItemNo where  B_TestItemControl.ControlLabNo='" + model.ControlLabNo + "' ");
            else
                strSql.Append(" select *From TestItem where 1=2");
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
        public DataSet B_lab_GetListByPage(ZhiFang.Model.TestItemControl model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            if (model != null)
            {
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or b_lab_TestItem.ItemNo like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( b_lab_TestItem.ItemNo like '%" + model.SearchLikeKey + "%'  ");
                }
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or b_lab_TestItem.CName like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( b_lab_TestItem.CName like '%" + model.SearchLikeKey + "%'  ");
                }
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or b_lab_TestItem.EName like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( b_lab_TestItem.EName like '%" + model.SearchLikeKey + "%'  ");
                }
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or b_lab_TestItem.ShortName like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( b_lab_TestItem.ShortName like '%" + model.SearchLikeKey + "%'  ");
                }
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or b_lab_TestItem.ShortCode like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( b_lab_TestItem.ShortCode like '%" + model.SearchLikeKey + "%'  ");
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
                strSql.Append("select a.*,B_TestItemControl.ItemNo as CenterItemNo,b.CName CenterCName from ( select b_lab_TestItem.*,b_lab_TestItem.itemno as LabItemNo from b_lab_TestItem where 1=1 and ItemNo in (select ControlItemNo From B_TestItemControl where 1=1 and B_TestItemControl.ControlLabNo ='" + model.ControlLabNo + "'" + strWhere.ToString() + " ) and  b_lab_TestItem.LabCode='" + model.ControlLabNo + "' " + strWhere.ToString() + "  ) a left join B_TestItemControl on B_TestItemControl.ControlItemNo =a.ItemNo and  B_TestItemControl.ControlLabNo='" + model.ControlLabNo + "' left join TestItem b on b.ItemNo = B_TestItemControl.ItemNo ");
            //未对照
            else if (model.ControlState == "2")
                strSql.Append("select a.*,B_TestItemControl.ItemNo as CenterItemNo,b.CName CenterCName from (select b_lab_TestItem.*,b_lab_TestItem.itemno as LabItemNo from b_lab_TestItem where 1=1 and ItemNo not in (select ControlItemNo From B_TestItemControl where 1=1 and B_TestItemControl.ControlLabNo ='" + model.ControlLabNo + "' ) and  b_lab_TestItem.LabCode='" + model.ControlLabNo + "' " + strWhere.ToString() + "  ) a left join B_TestItemControl on B_TestItemControl.ControlItemNo =a.ItemNo and  B_TestItemControl.ControlLabNo='" + model.ControlLabNo + "' left join TestItem b on b.ItemNo = B_TestItemControl.ItemNo");
            //全部 
            else if (model.ControlState == "0")
                strSql.Append("select a.*,B_TestItemControl.ItemNo as CenterItemNo,b.CName CenterCName from (select b_lab_TestItem.*,b_lab_TestItem.itemno as LabItemNo from b_lab_TestItem where 1=1 and b_lab_TestItem.LabCode='" + model.ControlLabNo + "' " + strWhere.ToString() + "  ) a left join B_TestItemControl on B_TestItemControl.ControlItemNo =a.ItemNo and  B_TestItemControl.ControlLabNo='" + model.ControlLabNo + "' left join TestItem b on b.ItemNo = B_TestItemControl.ItemNo");
            else
                strSql.Append(" select * From b_lab_TestItem where 1=2");
            //


            Common.Log.Log.Info("方法名GetListByPage,参数model,nowPageNum,nowPageSize：" + strSql.ToString());
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
        #endregion

        #region 结果项目字典对照关系表
        /// <summary>
        /// 利用标识列分页
        /// 结果项目字典对照关系表 此对照表用于报告下载的时候 ganwh add 2015-9-6
        /// </summary>
        /// <param name="model">model=null时为中心字典表分页；model!=null时为中心--实验室 对照关系分页</param>
        /// <param name="nowPageNum">第几页</param>
        /// <param name="nowPageSize">每页多少行</param>
        /// <returns>DataSet</returns>
        public DataSet B_lab_GetResultListByPage(ZhiFang.Model.ResultTestItemControl model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            if (model != null)
            {
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or b_lab_TestItem.ItemNo like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( b_lab_TestItem.ItemNo like '%" + model.SearchLikeKey + "%'  ");
                }
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or b_lab_TestItem.CName like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( b_lab_TestItem.CName like '%" + model.SearchLikeKey + "%'  ");
                }
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or b_lab_TestItem.EName like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( b_lab_TestItem.EName like '%" + model.SearchLikeKey + "%'  ");
                }
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or b_lab_TestItem.ShortName like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( b_lab_TestItem.ShortName like '%" + model.SearchLikeKey + "%'  ");
                }
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or b_lab_TestItem.ShortCode like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( b_lab_TestItem.ShortCode like '%" + model.SearchLikeKey + "%'  ");
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
                strSql.Append("select *,B_ResultTestItemControl.ItemNo as CenterItemNo,b.CName CenterCName from ( select *,b_lab_TestItem.itemno as LabItemNo from b_lab_TestItem where 1=1 and ItemNo in (select ControlItemNo From B_ResultTestItemControl where 1=1 and B_ResultTestItemControl.ControlLabNo ='" + model.ControlLabNo + "'" + strWhere.ToString() + " ) and  b_lab_TestItem.LabCode='" + model.ControlLabNo + "' " + strWhere.ToString() + "  ) a left join B_ResultTestItemControl on B_ResultTestItemControl.ControlItemNo =a.ItemNo and  B_ResultTestItemControl.ControlLabNo='" + model.ControlLabNo + "' left join TestItem b on b.ItemNo = B_ResultTestItemControl.ItemNo ");
            //未对照
            else if (model.ControlState == "2")
                strSql.Append("select *,B_ResultTestItemControl.ItemNo as CenterItemNo,b.CName CenterCName from (select *,b_lab_TestItem.itemno as LabItemNo from b_lab_TestItem where 1=1 and ItemNo not in (select ControlItemNo From B_ResultTestItemControl where 1=1 and B_ResultTestItemControl.ControlLabNo ='" + model.ControlLabNo + "' ) and  b_lab_TestItem.LabCode='" + model.ControlLabNo + "' " + strWhere.ToString() + "  ) a left join B_ResultTestItemControl on B_ResultTestItemControl.ControlItemNo =a.ItemNo and  B_ResultTestItemControl.ControlLabNo='" + model.ControlLabNo + "' left join TestItem b on b.ItemNo = B_ResultTestItemControl.ItemNo");
            //全部 
            else if (model.ControlState == "0")
                strSql.Append("select *,B_ResultTestItemControl.ItemNo as CenterItemNo,b.CName CenterCName from (select *,b_lab_TestItem.itemno as LabItemNo from b_lab_TestItem where 1=1 and b_lab_TestItem.LabCode='" + model.ControlLabNo + "' " + strWhere.ToString() + "  ) a left join B_ResultTestItemControl on B_ResultTestItemControl.ControlItemNo =a.ItemNo and  B_ResultTestItemControl.ControlLabNo='" + model.ControlLabNo + "' left join TestItem b on b.ItemNo = B_ResultTestItemControl.ItemNo");
            else
                strSql.Append(" select * From b_lab_TestItem where 1=2");
            //


            Common.Log.Log.Info("方法名GetListByPage,参数model,nowPageNum,nowPageSize：" + strSql.ToString());
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
        #endregion
        #endregion


        public bool IsExist(string labCodeNo)
        {
            throw new NotImplementedException();
        }

        public bool DeleteByLabCode(string LabCodeNo)
        {
            throw new NotImplementedException();
        }
    }
}

