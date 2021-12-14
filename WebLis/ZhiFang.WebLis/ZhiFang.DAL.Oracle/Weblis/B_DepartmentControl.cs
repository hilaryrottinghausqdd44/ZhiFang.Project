using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.Oracle.weblis
{
    //B_DepartmentControl

    public partial class B_DepartmentControl : IDDepartmentControl
    {
        DBUtility.IDBConnection idb;
        public B_DepartmentControl(string dbsourceconn)
        {
            idb = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public B_DepartmentControl()
        {
            idb = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.LisDB());
        }

        // DepartmentControlNo         
        D_LogInfo d_log = new D_LogInfo();
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.DepartmentControl model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.DepartmentControlNo != null)
            {
                strSql1.Append("DepartmentControlNo,");
                strSql2.Append("'" + model.DepartmentControlNo + "',");
            }
            if (model.DeptNo != null)
            {
                strSql1.Append("DeptNo,");
                strSql2.Append("" + model.DeptNo + ",");
            }
            if (model.ControlLabNo != null)
            {
                strSql1.Append("ControlLabNo,");
                strSql2.Append("'" + model.ControlLabNo + "',");
            }
            if (model.ControlDeptNo != null)
            {
                strSql1.Append("ControlDeptNo,");
                strSql2.Append("" + model.ControlDeptNo + ",");
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
            strSql.Append("insert into B_DepartmentControl(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");

            if (idb.ExecuteNonQuery(strSql.ToString()) > 0)
            {
                return d_log.OperateLog("rtmentControl", "", "", DateTime.Now, 1);
            }
            else
                return -1;

            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("insert into B_DepartmentControl(");
            //strSql.Append("DepartmentControlNo,DeptNo,ControlLabNo,ControlDeptNo,UseFlag");
            //strSql.Append(") values (");
            //strSql.Append("@DepartmentControlNo,@DeptNo,@ControlLabNo,@ControlDeptNo,@UseFlag");
            //strSql.Append(") ");

            //SqlParameter[] parameters = {
            //            new SqlParameter("@DepartmentControlNo", SqlDbType.Char,50) ,            
            //            new SqlParameter("@DeptNo", SqlDbType.Int,4) ,            
            //            new SqlParameter("@ControlLabNo", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@ControlDeptNo", SqlDbType.Int,4) ,            
            //            new SqlParameter("@UseFlag", SqlDbType.Int,4)             

            //};

            //parameters[0].Value = model.DepartmentControlNo;
            //parameters[1].Value = model.DeptNo;
            //parameters[2].Value = model.ControlLabNo;
            //parameters[3].Value = model.ControlDeptNo;
            //parameters[4].Value = model.UseFlag;
            //if (idb.ExecuteNonQuery(strSql.ToString(), parameters) > 0)
            //{
            //    return d_log.OperateLog("rtmentControl", "", "", DateTime.Now, 1);
            //}
            //else
            //    return -1;
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.DepartmentControl model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update B_DepartmentControl set ");
            if (model.DeptNo != null)
            {
                strSql.Append("DeptNo=" + model.DeptNo + ",");
            }
            if (model.ControlLabNo != null)
            {
                strSql.Append("ControlLabNo='" + model.ControlLabNo + "',");
            }
            if (model.ControlDeptNo != null)
            {
                strSql.Append("ControlDeptNo=" + model.ControlDeptNo + ",");
            }
            if (model.AddTime != null)
            {
                strSql.Append(" AddTime =to_date('" + model.AddTime.ToString() + "','YYYY-MM-DD HH24:MI:SS'),");
            }
            if (model.UseFlag != null)
            {
                strSql.Append("UseFlag=" + model.UseFlag + ",");
            }
            else
            {
                strSql.Append("UseFlag= null ,");
            }
            strSql.Append("DTimeStampe = Systimestamp,");
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where DepartmentControlNo='" + model.DepartmentControlNo + "'  ");
            if (idb.ExecuteNonQuery(strSql.ToString()) > 0)
            {
                return d_log.OperateLog("rtmentControl", "", "", DateTime.Now, 1);
            }
            else
                return -1;

            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("update B_DepartmentControl set ");

            //strSql.Append(" DepartmentControlNo = @DepartmentControlNo , ");
            //strSql.Append(" DeptNo = @DeptNo , ");
            //strSql.Append(" ControlLabNo = @ControlLabNo , ");
            //strSql.Append(" ControlDeptNo = @ControlDeptNo , ");
            //strSql.Append(" UseFlag = @UseFlag  ");
            //strSql.Append(" where DepartmentControlNo=@DepartmentControlNo  ");

            //SqlParameter[] parameters = {


            //new SqlParameter("@DepartmentControlNo", SqlDbType.Char,50) ,            	

            //new SqlParameter("@DeptNo", SqlDbType.Int,4) ,            	

            //new SqlParameter("@ControlLabNo", SqlDbType.VarChar,50) ,            	

            //new SqlParameter("@ControlDeptNo", SqlDbType.Int,4) ,            	



            //new SqlParameter("@UseFlag", SqlDbType.Int,4)             	

            //};




            //if (model.DepartmentControlNo != null)
            //{
            //    parameters[0].Value = model.DepartmentControlNo;
            //}



            //if (model.DeptNo != null)
            //{
            //    parameters[1].Value = model.DeptNo;
            //}



            //if (model.ControlLabNo != null)
            //{
            //    parameters[2].Value = model.ControlLabNo;
            //}



            //if (model.ControlDeptNo != null)
            //{
            //    parameters[3].Value = model.ControlDeptNo;
            //}







            //if (model.UseFlag != null)
            //{
            //    parameters[4].Value = model.UseFlag;
            //}


            //if (idb.ExecuteNonQuery(strSql.ToString(), parameters) > 0)
            //{
            //    return d_log.OperateLog("rtmentControl", "", "", DateTime.Now, 1);
            //}
            //else
            //    return -1;
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string DepartmentControlNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from B_DepartmentControl ");
            strSql.Append(" where DepartmentControlNo='" + DepartmentControlNo + "'");
            return idb.ExecuteNonQuery(strSql.ToString());

            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("delete from B_DepartmentControl ");
            //strSql.Append(" where DepartmentControlNo=@DepartmentControlNo ");
            //SqlParameter[] parameters = {
            //        new SqlParameter("@DepartmentControlNo", SqlDbType.Char,50)			};
            //parameters[0].Value = DepartmentControlNo;


            //return idb.ExecuteNonQuery(strSql.ToString(), parameters);

        }



        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.DepartmentControl GetModel(string DepartmentControlNo)
        {

            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("select Id, DepartmentControlNo, DeptNo, ControlLabNo, ControlDeptNo, AddTime, UseFlag  ");
            //strSql.Append("  from B_DepartmentControl ");
            //strSql.Append(" where DepartmentControlNo=@DepartmentControlNo ");
            //SqlParameter[] parameters = {
            //        new SqlParameter("@DepartmentControlNo", SqlDbType.Char,50)			};
            //parameters[0].Value = DepartmentControlNo;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(" Id,DepartmentControlNo,DeptNo,ControlLabNo,ControlDeptNo,DTimeStampe,AddTime,UseFlag ");
            strSql.Append(" from B_DepartmentControl ");
            strSql.Append(" where 1=1 and rownum <= '1' and DepartmentControlNo='" + DepartmentControlNo + "'");

            ZhiFang.Model.DepartmentControl model = new ZhiFang.Model.DepartmentControl();
            DataSet ds = idb.ExecuteDataSet(strSql.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {

                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }

                model.DepartmentControlNo = ds.Tables[0].Rows[0]["DepartmentControlNo"].ToString();

                if (ds.Tables[0].Rows[0]["DeptNo"].ToString() != "")
                {
                    model.DeptNo = int.Parse(ds.Tables[0].Rows[0]["DeptNo"].ToString());
                }

                model.ControlLabNo = ds.Tables[0].Rows[0]["ControlLabNo"].ToString();

                if (ds.Tables[0].Rows[0]["ControlDeptNo"].ToString() != "")
                {
                    model.ControlDeptNo = int.Parse(ds.Tables[0].Rows[0]["ControlDeptNo"].ToString());
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
            strSql.Append(" FROM B_DepartmentControl ");
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
            strSql.Append(" * ");
            strSql.Append(" FROM B_DepartmentControl ");
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
            return idb.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 根据实体获取DataSet
        /// </summary>
        public DataSet GetList(ZhiFang.Model.DepartmentControl model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM B_DepartmentControl where 1=1 ");

            if (model.DepartmentControlNo != null)
            {
                strSql.Append(" and DepartmentControlNo='" + model.DepartmentControlNo + "' ");
            }

            if (model.DeptNo != -1 && model.DeptNo != 0)
            {
                strSql.Append(" and DeptNo=" + model.DeptNo + " ");
            }

            if (model.ControlLabNo != null)
            {
                strSql.Append(" and ControlLabNo='" + model.ControlLabNo + "' ");
            }

            if (model.ControlDeptNo != -1)
            {
                strSql.Append(" and ControlDeptNo=" + model.ControlDeptNo + " ");
            }
            return idb.ExecuteDataSet(strSql.ToString());
        }
        public DataSet GetListByLike(ZhiFang.Model.DepartmentControl model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DepartmentControlNoAndName.*,concat(concat('(',CONCAT(DepartmentControlNo,')')),cname) as DepartmentControlNoAndName ");
            strSql.Append(" FROM B_DepartmentControl where 1=1 ");
            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }

            if (model.DepartmentControlNo != null)
            {
                strSql.Append(" or DepartmentControlNo like '%" + model.DepartmentControlNo + "%' ");
            }

            if (model.DeptNo != null)
            {
                strSql.Append(" or DeptNo like '%" + model.DeptNo + "%' ");
            }

            if (model.ControlLabNo != null)
            {
                strSql.Append(" or ControlLabNo like '%" + model.ControlLabNo + "%' ");
            }

            if (model.ControlDeptNo != null)
            {
                strSql.Append(" or ControlDeptNo like '%" + model.ControlDeptNo + "%' ");
            }

            if (model.UseFlag != null)
            {
                strSql.Append(" or UseFlag like '%" + model.UseFlag + "%' ");
            }
            return idb.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 获取总记录
        /// </summary>
        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_DepartmentControl ");
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
        public int GetTotalCount(ZhiFang.Model.DepartmentControl model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            strSql.Append("select count(*) FROM B_DepartmentControl  where 1=1  ");
            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode = '" + model.LabCode + "'");
            }
            if (model != null)
            {

                if (model.DepartmentControlNo != null)
                {
                    if (strWhere.Length == 0)
                        strWhere.Append(" and ( DepartmentControlNo like '%" + model.DepartmentControlNo + "%' ");
                    else
                        strWhere.Append(" or DepartmentControlNo like '%" + model.DepartmentControlNo + "%' ");
                }
                if (strWhere.Length != 0)
                    strWhere.Append(" ) ");
            }
            strSql.Append(strWhere.ToString());
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

        /// <summary>
        /// 利用标识列分页
        /// </summary>
        /// <param name="model">model=null时为中心字典表分页；model!=null时为中心--实验室 对照关系分页</param>
        /// <param name="nowPageNum">第几页</param>
        /// <param name="nowPageSize">每页多少行</param>
        /// <returns>DataSet</returns> 
        public DataSet GetListByPage(ZhiFang.Model.DepartmentControl model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            if (model != null)
            {

                if (model.DepartmentControlNo != null)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or DepartmentControlNo like '%" + model.DepartmentControlNo + "%'  ");
                    else
                        strWhere.Append(" and ( DepartmentControlNo like '%" + model.DepartmentControlNo + "%'  ");
                }
                if (strWhere.Length > 0)
                    strWhere.Append(" ) ");
            }
            strSql.Append("select  * from B_DepartmentControl where 1=1  ");
            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }
            strSql.Append(" " + strWhere.ToString() + " and  ROWNUM <= '" + nowPageSize + "' and DepartmentControlNo not in ");
            strSql.Append("(select  DepartmentControlNo from B_DepartmentControl where 1=1 and RowNum <='" + (nowPageSize * nowPageNum - nowPageSize) + "' ");
            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }
            strSql.Append(" " + strWhere.ToString() + " ) order by DepartmentControlNo  ");
            return idb.ExecuteDataSet(strSql.ToString());
        }

        public bool Exists(string DepartmentControlNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from B_DepartmentControl ");
            strSql.Append(" where DepartmentControlNo='" + DepartmentControlNo + "' ");
            //SqlParameter[] parameters = {
            //        new SqlParameter("@DepartmentControlNo", SqlDbType.Char,50)			};
            //parameters[0].Value = DepartmentControlNo;


            DataSet ds = idb.ExecuteDataSet(strSql.ToString());
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0 && ds.Tables[0].Rows[0][0].ToString().Trim() != "0")
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
            strSql.Append("select count(1) from B_DepartmentControl where 1=1 ");
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
            return idb.GetMaxID("DepartmentControlNo", "B_DepartmentControl");
        }

        public DataSet GetList(int Top, ZhiFang.Model.DepartmentControl model, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(" * ");
            strSql.Append(" FROM B_DepartmentControl where 1=1 ");



            if (model.DepartmentControlNo != null)
            {
                strSql.Append(" and DepartmentControlNo='" + model.DepartmentControlNo + "' ");
            }


            if (model.DeptNo != null)
            {
                strSql.Append(" and DeptNo='" + model.DeptNo + "' ");
            }


            if (model.ControlLabNo != null)
            {
                strSql.Append(" and ControlLabNo='" + model.ControlLabNo + "' ");
            }


            if (model.ControlDeptNo != null)
            {
                strSql.Append(" and ControlDeptNo='" + model.ControlDeptNo + "' ");
            }
            strSql.Append(" and ROWNUM <= '" + Top + "'");

            strSql.Append(" order by " + filedOrder);
            return idb.ExecuteDataSet(strSql.ToString());
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
                        if (this.Exists(ds.Tables[0].Rows[i]["DepartmentControlNo"].ToString().Trim()))
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
                strSql.Append("insert into B_DepartmentControl (");
                strSql.Append("DepartmentControlNo,DeptNo,ControlLabNo,ControlDeptNo,UseFlag");
                strSql.Append(") values (");

                if (dr.Table.Columns["DepartmentControlNo"] != null && dr.Table.Columns["DepartmentControlNo"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["DepartmentControlNo"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["DeptNo"] != null && dr.Table.Columns["DeptNo"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["DeptNo"].ToString().Trim() + "', ");
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

                if (dr.Table.Columns["ControlDeptNo"] != null && dr.Table.Columns["ControlDeptNo"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["ControlDeptNo"].ToString().Trim() + "', ");
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
                return idb.ExecuteNonQuery(strSql.ToString());
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Digitlab8.B_DepartmentControl.AddByDataRow 同步数据时异常：", ex);
                return 0;
            }
        }
        public int UpdateByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update B_DepartmentControl set ");


                if (dr.Table.Columns["DeptNo"] != null && dr.Table.Columns["DeptNo"].ToString().Trim() != "")
                {
                    strSql.Append(" DeptNo = '" + dr["DeptNo"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["ControlLabNo"] != null && dr.Table.Columns["ControlLabNo"].ToString().Trim() != "")
                {
                    strSql.Append(" ControlLabNo = '" + dr["ControlLabNo"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["ControlDeptNo"] != null && dr.Table.Columns["ControlDeptNo"].ToString().Trim() != "")
                {
                    strSql.Append(" ControlDeptNo = '" + dr["ControlDeptNo"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["UseFlag"] != null && dr.Table.Columns["UseFlag"].ToString().Trim() != "")
                {
                    strSql.Append(" UseFlag = '" + dr["UseFlag"].ToString().Trim() + "' , ");
                }


                int n = strSql.ToString().LastIndexOf(",");
                strSql.Remove(n, 1);
                strSql.Append(" where LabCode='" + dr["LabCode"].ToString().Trim() + "' and  DepartmentControlNo='" + dr["DepartmentControlNo"].ToString().Trim() + "'  ");

                return idb.ExecuteNonQuery(strSql.ToString());
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Digitlab8.B_DepartmentControl .UpdateByDataRow同步数据时异常：", ex);
                return 0;
            }
        }



        
             #region Department 字典
        #region 中心字典对照
        /// <summary>
        /// 利用标识列分页
        /// </summary>
        /// <param name="model">model=null时为中心字典表分页；model!=null时为中心--实验室 对照关系分页</param>
        /// <param name="nowPageNum">第几页</param>
        /// <param name="nowPageSize">每页多少行</param>
        /// <returns>DataSet</returns>
        //public DataSet GetListByPage(ZhiFang.Model.DepartmentControl model, int nowPageNum, int nowPageSize)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    StringBuilder strWhere = new StringBuilder();
        //    if (model != null)
        //    {
        //        //if (model.ControlLabNo != null)
        //        //{
        //        //    if (strWhere.Length > 0)
        //        //        strWhere.Append(" or B_DepartmentControl.ControlLabNo = '" + model.ControlLabNo + "'  ");
        //        //    else
        //        //        strWhere.Append(" and ( B_DepartmentControl.ControlLabNo = '" + model.ControlLabNo + "'  ");
        //        //}
        //        if (model.SearchLikeKey != null && model.SearchLikeKey != "")
        //        {
        //            if (strWhere.Length > 0)
        //                strWhere.Append(" or Department.DeptNo like '%" + model.SearchLikeKey + "%'  ");
        //            else
        //                strWhere.Append(" and ( Department.DeptNo like '%" + model.SearchLikeKey + "%'  ");
        //        }
        //        if (model.SearchLikeKey != null && model.SearchLikeKey != "")
        //        {
        //            if (strWhere.Length > 0)
        //                strWhere.Append(" or Department.CName like '%" + model.SearchLikeKey + "%'  ");
        //            else
        //                strWhere.Append(" and ( Department.CName like '%" + model.SearchLikeKey + "%'  ");
        //        }
        //        //if (model.SearchLikeKey != null)
        //        //{
        //        //    if (strWhere.Length > 0)
        //        //        strWhere.Append(" or Department.EName like '%" + model.SearchLikeKey + "%'  ");
        //        //    else
        //        //        strWhere.Append(" and ( Department.EName like '%" + model.SearchLikeKey + "%'  ");
        //        //}
        //        if (model.SearchLikeKey != null && model.SearchLikeKey != "")
        //        {
        //            if (strWhere.Length > 0)
        //                strWhere.Append(" or Department.ShortName like '%" + model.SearchLikeKey + "%'  ");
        //            else
        //                strWhere.Append(" and ( Department.ShortName like '%" + model.SearchLikeKey + "%'  ");
        //        }
        //        if (model.SearchLikeKey != null && model.SearchLikeKey != "")
        //        {
        //            if (strWhere.Length > 0)
        //                strWhere.Append(" or Department.ShortCode like '%" + model.SearchLikeKey + "%'  ");
        //            else
        //                strWhere.Append(" and ( Department.ShortCode like '%" + model.SearchLikeKey + "%'  ");
        //        }
        //        if (strWhere.Length > 0)
        //            strWhere.Append(" ) ");
        //    }
        //    string strOrderBy = "";
        //    // strOrderBy = "B_TestItemControl." + model.OrderField;

        //    //if ((nowPageSize + nowPageNum) != 0)
        //    //中心
        //    //已对照
        //    if (model.ControlState == "1")
        //        strSql.Append("select *from ( select *from Department where 1=1 and DeptNo in (select DeptNo From B_DepartmentControl where 1=1 and B_DepartmentControl.ControlLabNo ='" + model.ControlLabNo + "' " + strWhere.ToString() + " )) a left join B_DepartmentControl on B_DepartmentControl.DeptNo =a.DeptNo and  B_DepartmentControl.ControlLabNo='" + model.ControlLabNo + "'");
        //    //未对照
        //    else if (model.ControlState == "2")
        //        strSql.Append("select *from ( select *from Department where 1=1 and DeptNo not in (select DeptNo From B_DepartmentControl where 1=1 and B_DepartmentControl.ControlLabNo ='" + model.ControlLabNo + "')   " + strWhere.ToString() + " ) a left join B_DepartmentControl on B_DepartmentControl.DeptNo =a.DeptNo and  B_DepartmentControl.ControlLabNo='" + model.ControlLabNo + "'");
        //    //全部
        //    else if (model.ControlState == "0")
        //        strSql.Append("select *from ( select *from Department where 1=1 " + strWhere.ToString() + ") a left join B_DepartmentControl on B_DepartmentControl.DeptNo =a.DeptNo and  B_DepartmentControl.ControlLabNo='" + model.ControlLabNo + "' ");
        //    else
        //        strSql.Append(" select *From Department where 1=2");
        //    //


        //    Common.Log.Log.Info("方法名GetListByPage,参数model,nowPageNum,nowPageSize：" + strSql.ToString());
        //    return idb.ExecuteDataSet(strSql.ToString());





        //}
        #endregion

        #region 实验室字典对照
        /// <summary>
        /// 利用标识列分页
        /// </summary>
        /// <param name="model">model=null时为中心字典表分页；model!=null时为中心--实验室 对照关系分页</param>
        /// <param name="nowPageNum">第几页</param>
        /// <param name="nowPageSize">每页多少行</param>
        /// <returns>DataSet</returns>
        public DataSet B_lab_GetListByPage(ZhiFang.Model.DepartmentControl model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            if (model != null)
            {
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or b_lab_Department.labDeptNo like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( b_lab_Department.labDeptNo like '%" + model.SearchLikeKey + "%'  ");
                }
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or b_lab_Department.CName like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( b_lab_Department.CName like '%" + model.SearchLikeKey + "%'  ");
                }
                //if (model.SearchLikeKey != null)
                //{
                //    if (strWhere.Length > 0)
                //        strWhere.Append(" or b_lab_Department.EName like '%" + model.SearchLikeKey + "%'  ");
                //    else
                //        strWhere.Append(" and ( b_lab_Department.EName like '%" + model.SearchLikeKey + "%'  ");
                //}
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or b_lab_Department.ShortName like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( b_lab_Department.ShortName like '%" + model.SearchLikeKey + "%'  ");
                }
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or b_lab_Department.ShortCode like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( b_lab_Department.ShortCode like '%" + model.SearchLikeKey + "%'  ");
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
                strSql.Append("select b.DeptNo,a.*,b.CName CenterCName from ( select *from b_lab_Department where 1=1 and labDeptNo in (select ControlDeptNo From B_DepartmentControl where 1=1 and B_DepartmentControl.ControlLabNo ='" + model.ControlLabNo + "'" + strWhere.ToString() + " ) and  b_lab_Department.LabCode='" + model.ControlLabNo + "' " + strWhere.ToString() + "  ) a left join B_DepartmentControl on B_DepartmentControl.ControlDeptNo =a.labDeptNo and  B_DepartmentControl.ControlLabNo='" + model.ControlLabNo + "' left join Department b on b.DeptNo =B_DepartmentControl.DeptNo ");
            //未对照
            else if (model.ControlState == "2")
                strSql.Append("select b.DeptNo,a.*,b.CName CenterCName from (select *from b_lab_Department where 1=1 and labDeptNo not in (select ControlDeptNo From B_DepartmentControl where 1=1 and B_DepartmentControl.ControlLabNo ='" + model.ControlLabNo + "' ) and  b_lab_Department.LabCode='" + model.ControlLabNo + "' " + strWhere.ToString() + "  ) a left join B_DepartmentControl on B_DepartmentControl.ControlDeptNo =a.labDeptNo and  B_DepartmentControl.ControlLabNo='" + model.ControlLabNo + "' left join Department b on b.DeptNo =B_DepartmentControl.DeptNo ");
            //全部 
            else if (model.ControlState == "0")
                strSql.Append("select b.DeptNo,a.*,b.CName CenterCName from (select *from b_lab_Department where 1=1 and b_lab_Department.LabCode='" + model.ControlLabNo + "' " + strWhere.ToString() + "  ) a left join B_DepartmentControl on B_DepartmentControl.ControlDeptNo =a.labDeptNo and  B_DepartmentControl.ControlLabNo='" + model.ControlLabNo + "' left join Department b on b.DeptNo =B_DepartmentControl.DeptNo ");
            else
                strSql.Append(" select *From b_lab_Department where 1=2");
            //


            Common.Log.Log.Info("方法名GetListByPage,参数model,nowPageNum,nowPageSize：" + strSql.ToString());
            return idb.ExecuteDataSet(strSql.ToString());
        }
        #endregion
        #endregion
    }
}
