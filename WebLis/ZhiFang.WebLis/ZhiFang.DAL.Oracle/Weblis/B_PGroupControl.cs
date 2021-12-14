using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.Oracle.weblis
{
    //B_PGroupControl

    public partial class B_PGroupControl : BaseDALLisDB, IDPGroupControl
    {
        DBUtility.IDBConnection idb;
        public B_PGroupControl(string dbsourceconn)
        {
            idb = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public B_PGroupControl()
        {
            idb = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.LisDB());
        }
        D_LogInfo d_log = new D_LogInfo();
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.PGroupControl model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.SectionControlNo != null)
            {
                strSql1.Append("SectionControlNo,");
                strSql2.Append("'" + model.SectionControlNo + "',");
            }
            if (model.AddTime != null)
            {
                strSql1.Append("AddTime,");
                strSql2.Append(" to_date('" + model.AddTime.ToString() + "','YYYY-MM-DD HH24:MI:SS'),");
            }
            strSql1.Append("DTimeStampe,");
            strSql2.Append("Systimestamp,");

            if (model.ControlLabNo != null)
            {
                strSql1.Append("ControlLabNo,");
                strSql2.Append("'" + model.ControlLabNo + "',");
            }
            if (model.SectionNo != null)
            {
                strSql1.Append("SectionNo,");
                strSql2.Append("" + model.SectionNo + ",");
            }
            if (model.ControlSectionNo != null)
            {
                strSql1.Append("ControlSectionNo,");
                strSql2.Append("" + model.ControlSectionNo + ",");
            }
            if (model.UseFlag != null)
            {
                strSql1.Append("UseFlag,");
                strSql2.Append("" + model.UseFlag + ",");
            }
            strSql.Append("insert into B_PGroupControl(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            if (idb.ExecuteNonQuery(strSql.ToString()) > 0)
            {
                return d_log.OperateLog("PGroup", "", "", DateTime.Now, 1);
            }
            else
                return -1;

            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("insert into B_PGroupControl(");
            //strSql.Append("SectionControlNo,SectionNo,ControlLabNo,ControlSectionNo,UseFlag");
            //strSql.Append(") values (");
            //strSql.Append("@SectionControlNo,@SectionNo,@ControlLabNo,@ControlSectionNo,@UseFlag");
            //strSql.Append(") ");

            //SqlParameter[] parameters = {
            //            new SqlParameter("@SectionControlNo", SqlDbType.Char,50) ,            
            //            new SqlParameter("@SectionNo", SqlDbType.Int,4) ,            
            //            new SqlParameter("@ControlLabNo", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@ControlSectionNo", SqlDbType.Int,4) ,            
            //            new SqlParameter("@UseFlag", SqlDbType.Int,4)             

            //};

            //parameters[0].Value = model.SectionControlNo;
            //parameters[1].Value = model.SectionNo;
            //parameters[2].Value = model.ControlLabNo;
            //parameters[3].Value = model.ControlSectionNo;
            //parameters[4].Value = model.UseFlag;
            //if (idb.ExecuteNonQuery(strSql.ToString(), parameters) > 0)
            //{
            //    return d_log.OperateLog("PGroup", "", "", DateTime.Now, 1);
            //}
            //else
            //    return -1;
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.PGroupControl model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update B_PGroupControl set ");
            if (model.AddTime != null)
            {

                strSql.Append(" AddTime=to_date('" + model.AddTime.ToString() + "','YYYY-MM-DD HH24:MI:SS'),");
            }
            if (model.ControlLabNo != null)
            {
                strSql.Append("ControlLabNo='" + model.ControlLabNo + "',");
            }
            if (model.SectionNo != null)
            {
                strSql.Append("SectionNo=" + model.SectionNo + ",");
            }
            if (model.ControlSectionNo != null)
            {
                strSql.Append("ControlSectionNo=" + model.ControlSectionNo + ",");
            }
            else
            {
                strSql.Append("ControlSectionNo= null ,");
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
            strSql.Append(" where SectionControlNo='" + model.SectionControlNo + "'");

            if (idb.ExecuteNonQuery(strSql.ToString()) > 0)
            {
                return d_log.OperateLog("PGroup", "", "", DateTime.Now, 1);
            }
            else
                return -1;


            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("update B_PGroupControl set ");

            //strSql.Append(" SectionControlNo = @SectionControlNo , ");
            //strSql.Append(" SectionNo = @SectionNo , ");
            //strSql.Append(" ControlLabNo = @ControlLabNo , ");
            //strSql.Append(" ControlSectionNo = @ControlSectionNo , ");
            //strSql.Append(" UseFlag = @UseFlag  ");
            //strSql.Append(" where SectionControlNo=@SectionControlNo  ");

            //SqlParameter[] parameters = {


            //new SqlParameter("@SectionControlNo", SqlDbType.Char,50) ,            	

            //new SqlParameter("@SectionNo", SqlDbType.Int,4) ,            	

            //new SqlParameter("@ControlLabNo", SqlDbType.VarChar,50) ,            	

            //new SqlParameter("@ControlSectionNo", SqlDbType.Int,4) ,            	



            //new SqlParameter("@UseFlag", SqlDbType.Int,4)             	

            //};




            //if (model.SectionControlNo != null)
            //{
            //    parameters[0].Value = model.SectionControlNo;
            //}



            //if (model.SectionNo != null)
            //{
            //    parameters[1].Value = model.SectionNo;
            //}



            //if (model.ControlLabNo != null)
            //{
            //    parameters[2].Value = model.ControlLabNo;
            //}



            //if (model.ControlSectionNo != null)
            //{
            //    parameters[3].Value = model.ControlSectionNo;
            //}







            //if (model.UseFlag != null)
            //{
            //    parameters[4].Value = model.UseFlag;
            //}


            //if (idb.ExecuteNonQuery(strSql.ToString(), parameters) > 0)
            //{
            //    return d_log.OperateLog("PGroup", "", "", DateTime.Now, 1);
            //}
            //else
            //    return -1;
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string SectionControlNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from B_PGroupControl ");
            strSql.Append(" where SectionControlNo='" + SectionControlNo + "' ");
            return idb.ExecuteNonQuery(strSql.ToString());

        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from B_PGroupControl ");
            strSql.Append(" where ID in (" + Idlist + ")  ");
            return idb.ExecuteNonQuery(strSql.ToString());

        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.PGroupControl GetModel(string SectionControlNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, SectionControlNo, SectionNo, ControlLabNo, ControlSectionNo, DTimeStampe, AddTime, UseFlag  ");
            strSql.Append("  from B_PGroupControl ");
            strSql.Append(" where SectionControlNo=@SectionControlNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@SectionControlNo", SqlDbType.Char,50)};
            parameters[0].Value = SectionControlNo;


            ZhiFang.Model.PGroupControl model = new ZhiFang.Model.PGroupControl();
            DataSet ds = idb.ExecuteDataSet(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                model.SectionControlNo = ds.Tables[0].Rows[0]["SectionControlNo"].ToString();
                if (ds.Tables[0].Rows[0]["SectionNo"].ToString() != "")
                {
                    model.SectionNo = int.Parse(ds.Tables[0].Rows[0]["SectionNo"].ToString());
                }
                model.ControlLabNo = ds.Tables[0].Rows[0]["ControlLabNo"].ToString();
                if (ds.Tables[0].Rows[0]["ControlSectionNo"].ToString() != "")
                {
                    model.ControlSectionNo = int.Parse(ds.Tables[0].Rows[0]["ControlSectionNo"].ToString());
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
            strSql.Append(" FROM B_PGroupControl ");
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
            strSql.Append(" FROM B_PGroupControl ");

            strSql.Append(" and ROWNUM <= '" + Top + "'");

            strSql.Append(" order by " + filedOrder);
            return idb.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 根据实体获取DataSet
        /// </summary>
        public DataSet GetList(ZhiFang.Model.PGroupControl model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM B_PGroupControl where 1=1 ");

            if (model.SectionControlNo != null)
            {
                strSql.Append(" and SectionControlNo='" + model.SectionControlNo + "' ");
            }

            //if(model.SectionNo !=null)
            //{
            //            strSql.Append(" and SectionNo="+model.SectionNo+" ");
            //            }
            if (model.LabCode != null)
            {
                strSql.Append(" and ControlLabNo='" + model.LabCode + "' ");
            }
            if (model.ControlLabNo != null)
            {
                strSql.Append(" and ControlLabNo='" + model.ControlLabNo + "' ");
            }

            if (model.ControlSectionNo != null)
            {
                strSql.Append(" and ControlSectionNo=" + model.ControlSectionNo + " ");
            }

            if (model.DTimeStampe != null)
            {
                strSql.Append(" and DTimeStampe='" + model.DTimeStampe + "' ");
            }

            //if(model.AddTime !=null)
            //{
            //            strSql.Append(" and AddTime='"+model.AddTime+"' ");
            //            }

            //    if(model.UseFlag !=null)
            //    {
            //                strSql.Append(" and UseFlag="+model.UseFlag+" ");
            //                }
            Common.Log.Log.Info("modelPGroupControl:" + strSql);
            return idb.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 获取总记录
        /// </summary>
        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_PGroupControl ");
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
        public int GetTotalCount(ZhiFang.Model.PGroupControl model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_PGroupControl where 1=1 ");

            if (model.SectionControlNo != null)
            {
                strSql.Append(" and SectionControlNo='" + model.SectionControlNo + "' ");
            }

            if (model.SectionNo != null)
            {
                strSql.Append(" and SectionNo=" + model.SectionNo + " ");
            }

            if (model.ControlLabNo != null)
            {
                strSql.Append(" and ControlLabNo='" + model.ControlLabNo + "' ");
            }

            if (model.ControlSectionNo != null)
            {
                strSql.Append(" and ControlSectionNo=" + model.ControlSectionNo + " ");
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


        public bool Exists(string SectionControlNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from B_PGroupControl ");
            strSql.Append(" where SectionControlNo ='" + SectionControlNo + "'");
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

        public int GetMaxId()
        {
            return idb.GetMaxID("SectionControlNo", "B_PGroupControl");
        }

        public DataSet GetList(int Top, ZhiFang.Model.PGroupControl model, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");

            strSql.Append(" * ");
            strSql.Append(" FROM B_PGroupControl ");


            if (model.SectionControlNo != null)
            {

                strSql.Append(" and SectionControlNo='" + model.SectionControlNo + "' ");
            }

            if (model.SectionNo != null)
            {
                strSql.Append(" and SectionNo=" + model.SectionNo + " ");
            }

            if (model.ControlLabNo != null)
            {

                strSql.Append(" and ControlLabNo='" + model.ControlLabNo + "' ");
            }

            if (model.ControlSectionNo != null)
            {
                strSql.Append(" and ControlSectionNo=" + model.ControlSectionNo + " ");
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
            return idb.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetAllList()
        {
            return GetList("");
        }



        #region IDataBase<PGroupControl> 成员
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
                        if (this.Exists(ds.Tables[0].Rows[i]["SectionControlNo"].ToString().Trim()))
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
                strSql.Append("insert into B_PGroupControl (");
                strSql.Append("SectionControlNo,SectionNo,ControlLabNo,ControlSectionNo,AddTime,UseFlag");
                strSql.Append(") values (");
                strSql.Append("'" + dr["SectionControlNo"].ToString().Trim() + "','" + dr["SectionNo"].ToString().Trim() + "','" + dr["ControlLabNo"].ToString().Trim() + "','" + dr["ControlSectionNo"].ToString().Trim() + "','" + DateTime.Parse(dr["AddTime"].ToString().Trim()) + "','" + dr["UseFlag"].ToString().Trim() + "'");
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
                strSql.Append("update B_PGroupControl set ");

                strSql.Append(" SectionNo = '" + dr["SectionNo"].ToString().Trim() + "' , ");
                strSql.Append(" ControlLabNo = '" + dr["ControlLabNo"].ToString().Trim() + "' , ");
                strSql.Append(" ControlSectionNo = '" + dr["ControlSectionNo"].ToString().Trim() + "' , ");
                strSql.Append(" UseFlag = '" + dr["UseFlag"].ToString().Trim() + "'  ");
                strSql.Append(" where SectionControlNo='" + dr["SectionControlNo"].ToString().Trim() + "' ");

                return idb.ExecuteNonQuery(strSql.ToString());
            }
            catch
            {
                return 0;
            }
        }
        #endregion


        #region PGroup 字典
        #region 中心字典对照
        /// <summary>
        /// 利用标识列分页
        /// </summary>
        /// <param name="model">model=null时为中心字典表分页；model!=null时为中心--实验室 对照关系分页</param>
        /// <param name="nowPageNum">第几页</param>
        /// <param name="nowPageSize">每页多少行</param>
        /// <returns>DataSet</returns>
        public DataSet GetListByPage(ZhiFang.Model.PGroupControl model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            if (model != null)
            {
                //if (model.ControlLabNo != null)
                //{
                //    if (strWhere.Length > 0)
                //        strWhere.Append(" or B_PGroupControl.ControlLabNo = '" + model.ControlLabNo + "'  ");
                //    else
                //        strWhere.Append(" and ( B_PGroupControl.ControlLabNo = '" + model.ControlLabNo + "'  ");
                //}
                if (model.Searchlikekey != null && model.Searchlikekey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or PGroup.SectionNo like '%" + model.Searchlikekey + "%'  ");
                    else
                        strWhere.Append(" and ( PGroup.SectionNo like '%" + model.Searchlikekey + "%'  ");
                }
                if (model.Searchlikekey != null && model.Searchlikekey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or PGroup.CName like '%" + model.Searchlikekey + "%'  ");
                    else
                        strWhere.Append(" and ( PGroup.CName like '%" + model.Searchlikekey + "%'  ");
                }
                //if (model.SearchLikeKey != null)
                //{
                //    if (strWhere.Length > 0)
                //        strWhere.Append(" or PGroup.EName like '%" + model.SearchLikeKey + "%'  ");
                //    else
                //        strWhere.Append(" and ( PGroup.EName like '%" + model.SearchLikeKey + "%'  ");
                //}
                if (model.Searchlikekey != null && model.Searchlikekey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or PGroup.ShortName like '%" + model.Searchlikekey + "%'  ");
                    else
                        strWhere.Append(" and ( PGroup.ShortName like '%" + model.Searchlikekey + "%'  ");
                }
                if (model.Searchlikekey != null && model.Searchlikekey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or PGroup.ShortCode like '%" + model.Searchlikekey + "%'  ");
                    else
                        strWhere.Append(" and ( PGroup.ShortCode like '%" + model.Searchlikekey + "%'  ");
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
                strSql.Append("select *from ( select *from PGroup where 1=1 and SectionNo in (select SectionNo From B_PGroupControl where 1=1 and B_PGroupControl.ControlLabNo ='" + model.ControlLabNo + "' " + strWhere.ToString() + " )) a left join B_PGroupControl on B_PGroupControl.SectionNo =a.SectionNo and  B_PGroupControl.ControlLabNo='" + model.ControlLabNo + "'");
            //未对照
            else if (model.ControlState == "2")
                strSql.Append("select *from ( select *from PGroup where 1=1 and SectionNo not in (select SectionNo From B_PGroupControl where 1=1 and B_PGroupControl.ControlLabNo ='" + model.ControlLabNo + "')   " + strWhere.ToString() + " ) a left join B_PGroupControl on B_PGroupControl.SectionNo =a.SectionNo and  B_PGroupControl.ControlLabNo='" + model.ControlLabNo + "'");
            //全部
            else if (model.ControlState == "0")
                strSql.Append("select *from ( select *from PGroup where 1=1 " + strWhere.ToString() + ") a left join B_PGroupControl on B_PGroupControl.SectionNo =a.SectionNo and  B_PGroupControl.ControlLabNo='" + model.ControlLabNo + "' ");
            else
                strSql.Append(" select *From PGroup where 1=2");
            //


            Common.Log.Log.Info("方法名GetListByPage,参数model,nowPageNum,nowPageSize：" + strSql.ToString());
            return idb.ExecuteDataSet(strSql.ToString());





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
        public DataSet B_lab_GetListByPage(ZhiFang.Model.PGroupControl model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            if (model != null)
            {
                if (model.Searchlikekey != null && model.Searchlikekey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or b_lab_PGroup.labSectionNo like '%" + model.Searchlikekey + "%'  ");
                    else
                        strWhere.Append(" and ( b_lab_PGroup.labSectionNo like '%" + model.Searchlikekey + "%'  ");
                }
                if (model.Searchlikekey != null && model.Searchlikekey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or b_lab_PGroup.CName like '%" + model.Searchlikekey + "%'  ");
                    else
                        strWhere.Append(" and ( b_lab_PGroup.CName like '%" + model.Searchlikekey + "%'  ");
                }
                //if (model.SearchLikeKey != null)
                //{
                //    if (strWhere.Length > 0)
                //        strWhere.Append(" or b_lab_PGroup.EName like '%" + model.SearchLikeKey + "%'  ");
                //    else
                //        strWhere.Append(" and ( b_lab_PGroup.EName like '%" + model.SearchLikeKey + "%'  ");
                //}
                if (model.Searchlikekey != null && model.Searchlikekey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or b_lab_PGroup.ShortName like '%" + model.Searchlikekey + "%'  ");
                    else
                        strWhere.Append(" and ( b_lab_PGroup.ShortName like '%" + model.Searchlikekey + "%'  ");
                }
                if (model.Searchlikekey != null && model.Searchlikekey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or b_lab_PGroup.ShortCode like '%" + model.Searchlikekey + "%'  ");
                    else
                        strWhere.Append(" and ( b_lab_PGroup.ShortCode like '%" + model.Searchlikekey + "%'  ");
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
                strSql.Append("select b.SectionNo,a.*,b.CName CenterCName from ( select *from b_lab_PGroup where 1=1 and labSectionNo in (select ControlSectionNo From B_PGroupControl where 1=1 and B_PGroupControl.ControlLabNo ='" + model.ControlLabNo + "'" + strWhere.ToString() + " ) and  b_lab_PGroup.LabCode='" + model.ControlLabNo + "' " + strWhere.ToString() + "  ) a left join B_PGroupControl on B_PGroupControl.ControlSectionNo =a.labSectionNo and  B_PGroupControl.ControlLabNo='" + model.ControlLabNo + "' left join PGroup b on b.SectionNo = B_PGroupControl.SectionNo ");

            //未对照
            else if (model.ControlState == "2")
                strSql.Append("select b.SectionNo,a.*,b.CName CenterCName from (select *from b_lab_PGroup where 1=1 and labSectionNo not in (select ControlSectionNo From B_PGroupControl where 1=1 and B_PGroupControl.ControlLabNo ='" + model.ControlLabNo + "' ) and  b_lab_PGroup.LabCode='" + model.ControlLabNo + "' " + strWhere.ToString() + "  ) a left join B_PGroupControl on B_PGroupControl.ControlSectionNo =a.labSectionNo and  B_PGroupControl.ControlLabNo='" + model.ControlLabNo + "' left join PGroup b on b.SectionNo = B_PGroupControl.SectionNo ");
            //全部 
            else if (model.ControlState == "0")
                strSql.Append("select b.SectionNo,a.*,b.CName CenterCName from (select *from b_lab_PGroup where 1=1 and b_lab_PGroup.LabCode='" + model.ControlLabNo + "' " + strWhere.ToString() + "  ) a left join B_PGroupControl on B_PGroupControl.ControlSectionNo =a.labSectionNo and  B_PGroupControl.ControlLabNo='" + model.ControlLabNo + "' left join PGroup b on b.SectionNo = B_PGroupControl.SectionNo ");
            else
                strSql.Append(" select *From b_lab_PGroup where 1=2");
            //


            Common.Log.Log.Info("方法名GetListByPage,参数model,nowPageNum,nowPageSize：" + strSql.ToString());
            return idb.ExecuteDataSet(strSql.ToString());
        }
        #endregion
        #endregion
    }
}

