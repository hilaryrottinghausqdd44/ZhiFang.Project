using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.MsSql.Weblis
{
    //B_DistrictControl

    public partial class B_DistrictControl : IDDistrictControl
    {
        DBUtility.IDBConnection idb;
        public B_DistrictControl(string dbsourceconn)
        {
            idb = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public B_DistrictControl()
        {
            idb = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.LisDB());
        }
        D_LogInfo d_log = new D_LogInfo();
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.DistrictControl model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into B_DistrictControl(");
            strSql.Append("DistrictControlNo,DistrictNo,ControlLabNo,ControlDistrictNo,UseFlag");
            strSql.Append(") values (");
            strSql.Append("@DistrictControlNo,@DistrictNo,@ControlLabNo,@ControlDistrictNo,@UseFlag");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@DistrictControlNo", SqlDbType.Char,50) ,            
                        new SqlParameter("@DistrictNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@ControlLabNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ControlDistrictNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@UseFlag", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.DistrictControlNo;
            parameters[1].Value = model.DistrictNo;
            parameters[2].Value = model.ControlLabNo;
            parameters[3].Value = model.ControlDistrictNo;
            parameters[4].Value = model.UseFlag;
            if (idb.ExecuteNonQuery(strSql.ToString(), parameters) > 0)
            {
                return d_log.OperateLog("District", "", "", DateTime.Now, 1);
            }
            else
                return -1;
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.DistrictControl model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update B_DistrictControl set ");

            strSql.Append(" DistrictControlNo = @DistrictControlNo , ");
            strSql.Append(" DistrictNo = @DistrictNo , ");
            strSql.Append(" ControlLabNo = @ControlLabNo , ");
            strSql.Append(" ControlDistrictNo = @ControlDistrictNo , ");
            strSql.Append(" UseFlag = @UseFlag  ");
            strSql.Append(" where DistrictControlNo=@DistrictControlNo  ");

            SqlParameter[] parameters = {
			            	
                           
            new SqlParameter("@DistrictControlNo", SqlDbType.Char,50) ,            	
                           
            new SqlParameter("@DistrictNo", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@ControlLabNo", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@ControlDistrictNo", SqlDbType.Int,4) ,            	
                        	
                        	
                           
            new SqlParameter("@UseFlag", SqlDbType.Int,4)             	
              
            };




            if (model.DistrictControlNo != null)
            {
                parameters[0].Value = model.DistrictControlNo;
            }



            if (model.DistrictNo != null)
            {
                parameters[1].Value = model.DistrictNo;
            }



            if (model.ControlLabNo != null)
            {
                parameters[2].Value = model.ControlLabNo;
            }



            if (model.ControlDistrictNo != null)
            {
                parameters[3].Value = model.ControlDistrictNo;
            }







            if (model.UseFlag != null)
            {
                parameters[4].Value = model.UseFlag;
            }


            if (idb.ExecuteNonQuery(strSql.ToString(), parameters) > 0)
            {
                return d_log.OperateLog("District", "", "", DateTime.Now, 1);
            }
            else
                return -1;
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string DistrictControlNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from B_DistrictControl ");
            strSql.Append(" where DistrictControlNo=@DistrictControlNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@DistrictControlNo", SqlDbType.Char,50)};
            parameters[0].Value = DistrictControlNo;


            return idb.ExecuteNonQuery(strSql.ToString(), parameters);

        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from B_DistrictControl ");
            strSql.Append(" where ID in (" + Idlist + ")  ");
            return idb.ExecuteNonQuery(strSql.ToString());

        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.DistrictControl GetModel(string DistrictControlNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, DistrictControlNo, DistrictNo, ControlLabNo, ControlDistrictNo, DTimeStampe, AddTime, UseFlag  ");
            strSql.Append("  from B_DistrictControl ");
            strSql.Append(" where DistrictControlNo=@DistrictControlNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@DistrictControlNo", SqlDbType.Char,50)};
            parameters[0].Value = DistrictControlNo;


            ZhiFang.Model.DistrictControl model = new ZhiFang.Model.DistrictControl();
            DataSet ds = idb.ExecuteDataSet(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                model.DistrictControlNo = ds.Tables[0].Rows[0]["DistrictControlNo"].ToString();
                if (ds.Tables[0].Rows[0]["DistrictNo"].ToString() != "")
                {
                    model.DistrictNo = int.Parse(ds.Tables[0].Rows[0]["DistrictNo"].ToString());
                }
                model.ControlLabNo = ds.Tables[0].Rows[0]["ControlLabNo"].ToString();
                if (ds.Tables[0].Rows[0]["ControlDistrictNo"].ToString() != "")
                {
                    model.ControlDistrictNo = int.Parse(ds.Tables[0].Rows[0]["ControlDistrictNo"].ToString());
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
            strSql.Append(" FROM B_DistrictControl ");
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
            strSql.Append(" FROM B_DistrictControl ");
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
        public DataSet GetList(ZhiFang.Model.DistrictControl model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM B_DistrictControl where 1=1 ");

            if (model.DistrictControlNo != null)
            {
                strSql.Append(" and DistrictControlNo='" + model.DistrictControlNo + "' ");
            }

            if (model.DistrictNo != -1)
            {
                strSql.Append(" and DistrictNo=" + model.DistrictNo + " ");
            }

			if (model.ControlLabNo != null)
			{
				strSql.Append(" and ControlLabNo='" + model.ControlLabNo + "' ");
			}

            if (model.ControlDistrictNo != -1)
			{
				strSql.Append(" and ControlDistrictNo=" + model.ControlDistrictNo + " ");
			}

			//if (model.DTimeStampe != null)
			//{
			//    strSql.Append(" and DTimeStampe='" + model.DTimeStampe + "' ");
			//}

			//if (model.AddTime != null)
			//{
			//    strSql.Append(" and AddTime='" + model.AddTime + "' ");
			//}

			//if (model.UseFlag != null)
			//{
			//    strSql.Append(" and UseFlag=" + model.UseFlag + " ");
			//}
            return idb.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 获取总记录
        /// </summary>
        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_DistrictControl ");
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
        public int GetTotalCount(ZhiFang.Model.DistrictControl model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_DistrictControl where 1=1 ");

            if (model.DistrictControlNo != null)
            {
                strSql.Append(" and DistrictControlNo='" + model.DistrictControlNo + "' ");
            }

            if (model.DistrictNo != null)
            {
                strSql.Append(" and DistrictNo=" + model.DistrictNo + " ");
            }

            if (model.ControlLabNo != null)
            {
                strSql.Append(" and ControlLabNo='" + model.ControlLabNo + "' ");
            }

            if (model.ControlDistrictNo != null)
            {
                strSql.Append(" and ControlDistrictNo=" + model.ControlDistrictNo + " ");
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


        public bool Exists(string DistrictControlNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from B_DistrictControl ");
            strSql.Append(" where DistrictControlNo ='" + DistrictControlNo + "'");
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
            return idb.GetMaxID("DistrictControlNo", "B_DistrictControl");
        }

        public DataSet GetList(int Top, ZhiFang.Model.DistrictControl model, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM B_DistrictControl ");


            if (model.DistrictControlNo != null)
            {

                strSql.Append(" and DistrictControlNo='" + model.DistrictControlNo + "' ");
            }

            if (model.DistrictNo != null)
            {
                strSql.Append(" and DistrictNo=" + model.DistrictNo + " ");
            }

            if (model.ControlLabNo != null)
            {

                strSql.Append(" and ControlLabNo='" + model.ControlLabNo + "' ");
            }

            if (model.ControlDistrictNo != null)
            {
                strSql.Append(" and ControlDistrictNo=" + model.ControlDistrictNo + " ");
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



        #region IDataBase<DistrictControl> 成员
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
                        if (this.Exists(ds.Tables[0].Rows[i]["DistrictControlNo"].ToString().Trim()))
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
                strSql.Append("insert into B_DistrictControl (");
                strSql.Append("DistrictControlNo,DistrictNo,ControlLabNo,ControlDistrictNo,AddTime,UseFlag");
                strSql.Append(") values (");
                strSql.Append("'" + dr["DistrictControlNo"].ToString().Trim() + "','" + dr["DistrictNo"].ToString().Trim() + "','" + dr["ControlLabNo"].ToString().Trim() + "','" + dr["ControlDistrictNo"].ToString().Trim() + "','" + DateTime.Parse(dr["AddTime"].ToString().Trim()) + "','" + dr["UseFlag"].ToString().Trim() + "'");
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
                strSql.Append("update B_DistrictControl set ");

                strSql.Append(" DistrictNo = '" + dr["DistrictNo"].ToString().Trim() + "' , ");
                strSql.Append(" ControlLabNo = '" + dr["ControlLabNo"].ToString().Trim() + "' , ");
                strSql.Append(" ControlDistrictNo = '" + dr["ControlDistrictNo"].ToString().Trim() + "' , ");
                strSql.Append(" UseFlag = '" + dr["UseFlag"].ToString().Trim() + "'  ");
                strSql.Append(" where DistrictControlNo='" + dr["DistrictControlNo"].ToString().Trim() + "' ");

                return idb.ExecuteNonQuery(strSql.ToString());
            }
            catch
            {
                return 0;
            }
        }

        #endregion



        #region District 字典
        #region 中心字典对照
        /// <summary>
        /// 利用标识列分页
        /// </summary>
        /// <param name="model">model=null时为中心字典表分页；model!=null时为中心--实验室 对照关系分页</param>
        /// <param name="nowPageNum">第几页</param>
        /// <param name="nowPageSize">每页多少行</param>
        /// <returns>DataSet</returns>
        public DataSet GetListByPage(ZhiFang.Model.DistrictControl model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            if (model != null)
            {
                //if (model.ControlLabNo != null)
                //{
                //    if (strWhere.Length > 0)
                //        strWhere.Append(" or B_DistrictControl.ControlLabNo = '" + model.ControlLabNo + "'  ");
                //    else
                //        strWhere.Append(" and ( B_DistrictControl.ControlLabNo = '" + model.ControlLabNo + "'  ");
                //}
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or District.DistrictNo like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( District.DistrictNo like '%" + model.SearchLikeKey + "%'  ");
                }
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or District.CName like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( District.CName like '%" + model.SearchLikeKey + "%'  ");
                }
                //if (model.SearchLikeKey != null)
                //{
                //    if (strWhere.Length > 0)
                //        strWhere.Append(" or District.EName like '%" + model.SearchLikeKey + "%'  ");
                //    else
                //        strWhere.Append(" and ( District.EName like '%" + model.SearchLikeKey + "%'  ");
                //}
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or District.ShortName like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( District.ShortName like '%" + model.SearchLikeKey + "%'  ");
                }
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or District.ShortCode like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( District.ShortCode like '%" + model.SearchLikeKey + "%'  ");
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
                strSql.Append("select *from ( select *from District where 1=1 and DistrictNo in (select DistrictNo From B_DistrictControl where 1=1 and B_DistrictControl.ControlLabNo ='" + model.ControlLabNo + "' " + strWhere.ToString() + " )) a left join B_DistrictControl on B_DistrictControl.DistrictNo =a.DistrictNo and  B_DistrictControl.ControlLabNo='" + model.ControlLabNo + "'");
            //未对照
            else if (model.ControlState == "2")
                strSql.Append("select *from ( select *from District where 1=1 and DistrictNo not in (select DistrictNo From B_DistrictControl where 1=1 and B_DistrictControl.ControlLabNo ='" + model.ControlLabNo + "')   " + strWhere.ToString() + " ) a left join B_DistrictControl on B_DistrictControl.DistrictNo =a.DistrictNo and  B_DistrictControl.ControlLabNo='" + model.ControlLabNo + "'");
            //全部
            else if (model.ControlState == "0")
                strSql.Append("select *from ( select *from District where 1=1 " + strWhere.ToString() + ") a left join B_DistrictControl on B_DistrictControl.DistrictNo =a.DistrictNo and  B_DistrictControl.ControlLabNo='" + model.ControlLabNo + "' ");
            else
                strSql.Append(" select *From District where 1=2");
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
        public DataSet B_lab_GetListByPage(ZhiFang.Model.DistrictControl model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            if (model != null)
            {
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or b_lab_District.labDistrictNo like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( b_lab_District.labDistrictNo like '%" + model.SearchLikeKey + "%'  ");
                }
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or b_lab_District.CName like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( b_lab_District.CName like '%" + model.SearchLikeKey + "%'  ");
                }
                //if (model.SearchLikeKey != null)
                //{
                //    if (strWhere.Length > 0)
                //        strWhere.Append(" or b_lab_District.EName like '%" + model.SearchLikeKey + "%'  ");
                //    else
                //        strWhere.Append(" and ( b_lab_District.EName like '%" + model.SearchLikeKey + "%'  ");
                //}
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or b_lab_District.ShortName like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( b_lab_District.ShortName like '%" + model.SearchLikeKey + "%'  ");
                }
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or b_lab_District.ShortCode like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( b_lab_District.ShortCode like '%" + model.SearchLikeKey + "%'  ");
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
                strSql.Append("select *,b.CName CenterCName from ( select *from b_lab_District where 1=1 and labDistrictno in (select ControlDistrictNo From B_DistrictControl where 1=1 and B_DistrictControl.ControlLabNo ='" + model.ControlLabNo + "'" + strWhere.ToString() + " ) and  b_lab_District.LabCode='" + model.ControlLabNo + "' " + strWhere.ToString() + "  ) a left join B_DistrictControl on B_DistrictControl.ControlDistrictNo =a.labDistrictNo and  B_DistrictControl.ControlLabNo='" + model.ControlLabNo + "' left join District b on b.DistrictNo = B_DistrictControl.DistrictNo");
            //未对照
            else if (model.ControlState == "2") 
                strSql.Append("select *,b.CName CenterCName from (select *from b_lab_District where 1=1 and labDistrictno not in (select ControlDistrictNo From B_DistrictControl where 1=1 and B_DistrictControl.ControlLabNo ='" + model.ControlLabNo + "' ) and  b_lab_District.LabCode='" + model.ControlLabNo + "' " + strWhere.ToString() + "  ) a left join B_DistrictControl on B_DistrictControl.ControlDistrictNo =a.labDistrictNo and  B_DistrictControl.ControlLabNo='" + model.ControlLabNo + "' left join District b on b.DistrictNo = B_DistrictControl.DistrictNo ");
            //全部 
            else if (model.ControlState == "0")
                strSql.Append("select *,b.CName CenterCName from (select *from b_lab_District where 1=1 and b_lab_District.LabCode='" + model.ControlLabNo + "' " + strWhere.ToString() + "  ) a left join B_DistrictControl on B_DistrictControl.ControlDistrictNo =a.labDistrictNo and  B_DistrictControl.ControlLabNo='" + model.ControlLabNo + "' left join District b on b.DistrictNo = B_DistrictControl.DistrictNo");
            else
                strSql.Append(" select *From b_lab_District where 1=2");
            //


            Common.Log.Log.Info("方法名GetListByPage,参数model,nowPageNum,nowPageSize：" + strSql.ToString());
            return idb.ExecuteDataSet(strSql.ToString());
        }
        #endregion
        #endregion
    }
}

