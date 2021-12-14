using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.MsSql.Weblis
{
    //B_DoctorControl

    public partial class B_DoctorControl : IDDoctorControl
    {
        DBUtility.IDBConnection idb;
        public B_DoctorControl(string dbsourceconn)
        {
            idb = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public B_DoctorControl()
        {
            idb = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.LisDB());
        }
        D_LogInfo d_log = new D_LogInfo();
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.DoctorControl model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into B_DoctorControl(");
            strSql.Append("DoctorControlNo,DoctorNo,ControlLabNo,ControlDoctorNo,UseFlag");
            strSql.Append(") values (");
            strSql.Append("@DoctorControlNo,@DoctorNo,@ControlLabNo,@ControlDoctorNo,@UseFlag");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@DoctorControlNo", SqlDbType.Char,50) ,            
                        new SqlParameter("@DoctorNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@ControlLabNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ControlDoctorNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@UseFlag", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.DoctorControlNo;
            parameters[1].Value = model.DoctorNo;
            parameters[2].Value = model.ControlLabNo;
            parameters[3].Value = model.ControlDoctorNo;
            parameters[4].Value = model.UseFlag;
            if (idb.ExecuteNonQuery(strSql.ToString(), parameters) > 0)
            {
                return d_log.OperateLog("Doctor", "", "", DateTime.Now, 1);
            }
            else
                return -1;
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.DoctorControl model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update B_DoctorControl set ");

            strSql.Append(" DoctorControlNo = @DoctorControlNo , ");
            strSql.Append(" DoctorNo = @DoctorNo , ");
            strSql.Append(" ControlLabNo = @ControlLabNo , ");
            strSql.Append(" ControlDoctorNo = @ControlDoctorNo , ");
            strSql.Append(" UseFlag = @UseFlag  ");
            strSql.Append(" where DoctorControlNo=@DoctorControlNo  ");

            SqlParameter[] parameters = {
			            	
                           
            new SqlParameter("@DoctorControlNo", SqlDbType.Char,50) ,            	
                           
            new SqlParameter("@DoctorNo", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@ControlLabNo", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@ControlDoctorNo", SqlDbType.Int,4) ,            	
                        	
                        	
                           
            new SqlParameter("@UseFlag", SqlDbType.Int,4)             	
              
            };




            if (model.DoctorControlNo != null)
            {
                parameters[0].Value = model.DoctorControlNo;
            }



            if (model.DoctorNo != null)
            {
                parameters[1].Value = model.DoctorNo;
            }



            if (model.ControlLabNo != null)
            {
                parameters[2].Value = model.ControlLabNo;
            }



            if (model.ControlDoctorNo != null)
            {
                parameters[3].Value = model.ControlDoctorNo;
            }







            if (model.UseFlag != null)
            {
                parameters[4].Value = model.UseFlag;
            }


            if (idb.ExecuteNonQuery(strSql.ToString(), parameters) > 0)
            {
                return d_log.OperateLog("Doctor", "", "", DateTime.Now, 1);
            }
            else
                return -1;
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string DoctorControlNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from B_DoctorControl ");
            strSql.Append(" where DoctorControlNo=@DoctorControlNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@DoctorControlNo", SqlDbType.Char,50)};
            parameters[0].Value = DoctorControlNo;


            return idb.ExecuteNonQuery(strSql.ToString(), parameters);

        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from B_DoctorControl ");
            strSql.Append(" where ID in (" + Idlist + ")  ");
            return idb.ExecuteNonQuery(strSql.ToString());

        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.DoctorControl GetModel(string DoctorControlNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, DoctorControlNo, DoctorNo, ControlLabNo, ControlDoctorNo, DTimeStampe, AddTime, UseFlag  ");
            strSql.Append("  from B_DoctorControl ");
            strSql.Append(" where DoctorControlNo=@DoctorControlNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@DoctorControlNo", SqlDbType.Char,50)};
            parameters[0].Value = DoctorControlNo;


            ZhiFang.Model.DoctorControl model = new ZhiFang.Model.DoctorControl();
            DataSet ds = idb.ExecuteDataSet(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                model.DoctorControlNo = ds.Tables[0].Rows[0]["DoctorControlNo"].ToString();
                if (ds.Tables[0].Rows[0]["DoctorNo"].ToString() != "")
                {
                    model.DoctorNo = int.Parse(ds.Tables[0].Rows[0]["DoctorNo"].ToString());
                }
                model.ControlLabNo = ds.Tables[0].Rows[0]["ControlLabNo"].ToString();
                if (ds.Tables[0].Rows[0]["ControlDoctorNo"].ToString() != "")
                {
                    model.ControlDoctorNo = int.Parse(ds.Tables[0].Rows[0]["ControlDoctorNo"].ToString());
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
            strSql.Append(" FROM B_DoctorControl ");
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
            strSql.Append(" FROM B_DoctorControl ");
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
        public DataSet GetList(ZhiFang.Model.DoctorControl model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM B_DoctorControl where 1=1 ");

            if (model.DoctorControlNo != null)
            {
                strSql.Append(" and DoctorControlNo='" + model.DoctorControlNo + "' ");
            }

            if (model.DoctorNo != -1)
            {
                strSql.Append(" and DoctorNo=" + model.DoctorNo + " ");
            }

            if (model.ControlLabNo != null)
            {
                strSql.Append(" and ControlLabNo='" + model.ControlLabNo + "' ");
            }

            if (model.ControlDoctorNo != -1)
            {
                strSql.Append(" and ControlDoctorNo=" + model.ControlDoctorNo + " ");
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
            strSql.Append("select count(*) FROM B_DoctorControl ");
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
        public int GetTotalCount(ZhiFang.Model.DoctorControl model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_DoctorControl where 1=1 ");

            if (model.DoctorControlNo != null)
            {
                strSql.Append(" and DoctorControlNo='" + model.DoctorControlNo + "' ");
            }

            if (model.DoctorNo != null)
            {
                strSql.Append(" and DoctorNo=" + model.DoctorNo + " ");
            }

            if (model.ControlLabNo != null)
            {
                strSql.Append(" and ControlLabNo='" + model.ControlLabNo + "' ");
            }

            if (model.ControlDoctorNo != null)
            {
                strSql.Append(" and ControlDoctorNo=" + model.ControlDoctorNo + " ");
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


        public bool Exists(string DoctorControlNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from B_DoctorControl ");
            strSql.Append(" where DoctorControlNo ='" + DoctorControlNo + "'");
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
            return idb.GetMaxID("DoctorControlNo", "B_DoctorControl");
        }

        public DataSet GetList(int Top, ZhiFang.Model.DoctorControl model, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM B_DoctorControl ");


            if (model.DoctorControlNo != null)
            {

                strSql.Append(" and DoctorControlNo='" + model.DoctorControlNo + "' ");
            }

            if (model.DoctorNo != null)
            {
                strSql.Append(" and DoctorNo=" + model.DoctorNo + " ");
            }

            if (model.ControlLabNo != null)
            {

                strSql.Append(" and ControlLabNo='" + model.ControlLabNo + "' ");
            }

            if (model.ControlDoctorNo != null)
            {
                strSql.Append(" and ControlDoctorNo=" + model.ControlDoctorNo + " ");
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



        #region IDataBase<DoctorControl> 成员
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
                        if (this.Exists(ds.Tables[0].Rows[i]["DoctorControlNo"].ToString().Trim()))
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
                strSql.Append("insert into B_DoctorControl (");
                strSql.Append("DoctorControlNo,DoctorNo,ControlLabNo,ControlDoctorNo,AddTime,UseFlag");
                strSql.Append(") values (");
                strSql.Append("'" + dr["DoctorControlNo"].ToString().Trim() + "','" + dr["DoctorNo"].ToString().Trim() + "','" + dr["ControlLabNo"].ToString().Trim() + "','" + dr["ControlDoctorNo"].ToString().Trim() + "','" + DateTime.Parse(dr["AddTime"].ToString().Trim()) + "','" + dr["UseFlag"].ToString().Trim() + "'");
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
                strSql.Append("update B_DoctorControl set ");

                strSql.Append(" DoctorNo = '" + dr["DoctorNo"].ToString().Trim() + "' , ");
                strSql.Append(" ControlLabNo = '" + dr["ControlLabNo"].ToString().Trim() + "' , ");
                strSql.Append(" ControlDoctorNo = '" + dr["ControlDoctorNo"].ToString().Trim() + "' , ");
                strSql.Append(" UseFlag = '" + dr["UseFlag"].ToString().Trim() + "'  ");
                strSql.Append(" where DoctorControlNo='" + dr["DoctorControlNo"].ToString().Trim() + "' ");

                return idb.ExecuteNonQuery(strSql.ToString());
            }
            catch
            {
                return 0;
            }
        }
        #endregion


        #region Doctor 字典
        #region 中心字典对照
        /// <summary>
        /// 利用标识列分页
        /// </summary>
        /// <param name="model">model=null时为中心字典表分页；model!=null时为中心--实验室 对照关系分页</param>
        /// <param name="nowPageNum">第几页</param>
        /// <param name="nowPageSize">每页多少行</param>
        /// <returns>DataSet</returns>
        public DataSet GetListByPage(ZhiFang.Model.DoctorControl model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            if (model != null)
            {
                //if (model.ControlLabNo != null)
                //{
                //    if (strWhere.Length > 0)
                //        strWhere.Append(" or B_DoctorControl.ControlLabNo = '" + model.ControlLabNo + "'  ");
                //    else
                //        strWhere.Append(" and ( B_DoctorControl.ControlLabNo = '" + model.ControlLabNo + "'  ");
                //}
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or Doctor.DoctorNo like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( Doctor.DoctorNo like '%" + model.SearchLikeKey + "%'  ");
                }
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or Doctor.CName like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( Doctor.CName like '%" + model.SearchLikeKey + "%'  ");
                }
                //if (model.SearchLikeKey != null)
                //{
                //    if (strWhere.Length > 0)
                //        strWhere.Append(" or Doctor.EName like '%" + model.SearchLikeKey + "%'  ");
                //    else
                //        strWhere.Append(" and ( Doctor.EName like '%" + model.SearchLikeKey + "%'  ");
                //}
                //if (model.SearchLikeKey != null)
                //{
                //    if (strWhere.Length > 0)
                //        strWhere.Append(" or Doctor.ShortName like '%" + model.SearchLikeKey + "%'  ");
                //    else
                //        strWhere.Append(" and ( Doctor.ShortName like '%" + model.SearchLikeKey + "%'  ");
                //}
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or Doctor.ShortCode like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( Doctor.ShortCode like '%" + model.SearchLikeKey + "%'  ");
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
                strSql.Append("select *from ( select *from Doctor where 1=1 and DoctorNo in (select DoctorNo From B_DoctorControl where 1=1 and B_DoctorControl.ControlLabNo ='" + model.ControlLabNo + "' " + strWhere.ToString() + " )) a left join B_DoctorControl on B_DoctorControl.DoctorNo =a.DoctorNo and  B_DoctorControl.ControlLabNo='" + model.ControlLabNo + "'");
            //未对照
            else if (model.ControlState == "2")
                strSql.Append("select *from ( select *from Doctor where 1=1 and DoctorNo not in (select DoctorNo From B_DoctorControl where 1=1 and B_DoctorControl.ControlLabNo ='" + model.ControlLabNo + "')   " + strWhere.ToString() + " ) a left join B_DoctorControl on B_DoctorControl.DoctorNo =a.DoctorNo and  B_DoctorControl.ControlLabNo='" + model.ControlLabNo + "'");
            //全部
            else if (model.ControlState == "0")
                strSql.Append("select *from ( select *from Doctor where 1=1 " + strWhere.ToString() + ") a left join B_DoctorControl on B_DoctorControl.DoctorNo =a.DoctorNo and  B_DoctorControl.ControlLabNo='" + model.ControlLabNo + "' ");
            else
                strSql.Append(" select *From Doctor where 1=2");
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
        public DataSet B_lab_GetListByPage(ZhiFang.Model.DoctorControl model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            if (model != null)
            {
                if (model.SearchLikeKey != null && model.SearchLikeKey!="")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or b_lab_Doctor.labDoctorNo like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( b_lab_Doctor.labDoctorNo like '%" + model.SearchLikeKey + "%'  ");
                }
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or b_lab_Doctor.CName like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( b_lab_Doctor.CName like '%" + model.SearchLikeKey + "%'  ");
                }
                //if (model.SearchLikeKey != null)
                //{
                //    if (strWhere.Length > 0)
                //        strWhere.Append(" or b_lab_Doctor.EName like '%" + model.SearchLikeKey + "%'  ");
                //    else
                //        strWhere.Append(" and ( b_lab_Doctor.EName like '%" + model.SearchLikeKey + "%'  ");
                //}
                //if (model.SearchLikeKey != null)
                //{
                //    if (strWhere.Length > 0)
                //        strWhere.Append(" or b_lab_Doctor.ShortName like '%" + model.SearchLikeKey + "%'  ");
                //    else
                //        strWhere.Append(" and ( b_lab_Doctor.ShortName like '%" + model.SearchLikeKey + "%'  ");
                //}
                if (model.SearchLikeKey != null && model.SearchLikeKey != "")
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or b_lab_Doctor.ShortCode like '%" + model.SearchLikeKey + "%'  ");
                    else
                        strWhere.Append(" and ( b_lab_Doctor.ShortCode like '%" + model.SearchLikeKey + "%'  ");
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
                strSql.Append("select *,b.CName CenterCName from ( select *from b_lab_Doctor where 1=1 and labDoctorNo in (select ControlDoctorNo From B_DoctorControl where 1=1 and B_DoctorControl.ControlLabNo ='" + model.ControlLabNo + "'" + strWhere.ToString() + " ) and  b_lab_Doctor.LabCode='" + model.ControlLabNo + "' " + strWhere.ToString() + "  ) a left join B_DoctorControl on B_DoctorControl.ControlDoctorNo =a.labDoctorNo and  B_DoctorControl.ControlLabNo='" + model.ControlLabNo + "' left join Doctor b on b.DoctorNo = B_DoctorControl.DoctorNo ");
            //未对照
            else if (model.ControlState == "2")
                strSql.Append("select *,b.CName CenterCName from (select *from b_lab_Doctor where 1=1 and labDoctorNo not in (select ControlDoctorNo From B_DoctorControl where 1=1 and B_DoctorControl.ControlLabNo ='" + model.ControlLabNo + "' ) and  b_lab_Doctor.LabCode='" + model.ControlLabNo + "' " + strWhere.ToString() + "  ) a left join B_DoctorControl on B_DoctorControl.ControlDoctorNo =a.labDoctorNo and  B_DoctorControl.ControlLabNo='" + model.ControlLabNo + "' left join Doctor b on b.DoctorNo = B_DoctorControl.DoctorNo ");
            //全部 
            else if (model.ControlState == "0")
                strSql.Append("select *,b.CName CenterCName from (select *from b_lab_Doctor where 1=1 and b_lab_Doctor.LabCode='" + model.ControlLabNo + "' " + strWhere.ToString() + "  ) a left join B_DoctorControl on B_DoctorControl.ControlDoctorNo =a.labDoctorNo and  B_DoctorControl.ControlLabNo='" + model.ControlLabNo + "'  left join Doctor b on b.DoctorNo = B_DoctorControl.DoctorNo ");
            else
                strSql.Append(" select *From b_lab_Doctor where 1=2");
            //


            Common.Log.Log.Info("方法名GetListByPage,参数model,nowPageNum,nowPageSize：" + strSql.ToString());
            return idb.ExecuteDataSet(strSql.ToString());
        }
        #endregion
        #endregion
    }
}

