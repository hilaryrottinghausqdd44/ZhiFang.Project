using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.MsSql.Weblis
{
    //B_Lab_Doctor

    public partial class B_Lab_Doctor : IDLab_Doctor
    {
        DBUtility.IDBConnection idb;
        public B_Lab_Doctor(string dbsourceconn)
        {
            idb = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public B_Lab_Doctor()
        {
            idb = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.LisDB());
        }
        D_LogInfo d_log = new D_LogInfo();
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.Lab_Doctor model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into B_Lab_Doctor(");
            strSql.Append("LabCode,LabDoctorNo,CName,ShortCode,HisOrderCode,Visible,StandCode,ZFStandCode,UseFlag");
            strSql.Append(") values (");
            strSql.Append("@LabCode,@LabDoctorNo,@CName,@ShortCode,@HisOrderCode,@Visible,@StandCode,@ZFStandCode,@UseFlag");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@LabCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@LabDoctorNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@CName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ShortCode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@HisOrderCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Visible", SqlDbType.Int,4) ,            
                        new SqlParameter("@StandCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ZFStandCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@UseFlag", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.LabCode;
            parameters[1].Value = model.LabDoctorNo;
            parameters[2].Value = model.CName;
            parameters[3].Value = model.ShortCode;
            parameters[4].Value = model.HisOrderCode;
            parameters[5].Value = model.Visible;
            parameters[6].Value = model.StandCode;
            parameters[7].Value = model.ZFStandCode;
            parameters[8].Value = model.UseFlag;
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
        public int Update(ZhiFang.Model.Lab_Doctor model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update B_Lab_Doctor set ");

            strSql.Append(" LabCode = @LabCode , ");
            strSql.Append(" LabDoctorNo = @LabDoctorNo , ");
            strSql.Append(" CName = @CName , ");
            strSql.Append(" ShortCode = @ShortCode , ");
            strSql.Append(" HisOrderCode = @HisOrderCode , ");
            strSql.Append(" Visible = @Visible , ");
            strSql.Append(" StandCode = @StandCode , ");
            strSql.Append(" ZFStandCode = @ZFStandCode , ");
            strSql.Append(" UseFlag = @UseFlag  ");
            strSql.Append(" where LabCode=@LabCode and LabDoctorNo=@LabDoctorNo  ");

            SqlParameter[] parameters = {
			            	
                           
            new SqlParameter("@LabCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@LabDoctorNo", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@CName", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@ShortCode", SqlDbType.VarChar,10) ,            	
                           
            new SqlParameter("@HisOrderCode", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@Visible", SqlDbType.Int,4) ,            	
                        	
                        	
                           
            new SqlParameter("@StandCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@ZFStandCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@UseFlag", SqlDbType.Int,4)             	
              
            };




            if (model.LabCode != null)
            {
                parameters[0].Value = model.LabCode;
            }



            if (model.LabDoctorNo != null)
            {
                parameters[1].Value = model.LabDoctorNo;
            }



            if (model.CName != null)
            {
                parameters[2].Value = model.CName;
            }



            if (model.ShortCode != null)
            {
                parameters[3].Value = model.ShortCode;
            }



            if (model.HisOrderCode != null)
            {
                parameters[4].Value = model.HisOrderCode;
            }



            if (model.Visible != null)
            {
                parameters[5].Value = model.Visible;
            }







            if (model.StandCode != null)
            {
                parameters[6].Value = model.StandCode;
            }



            if (model.ZFStandCode != null)
            {
                parameters[7].Value = model.ZFStandCode;
            }



            if (model.UseFlag != null)
            {
                parameters[8].Value = model.UseFlag;
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
        public int Delete(string LabCode, int LabDoctorNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from B_Lab_Doctor ");
            strSql.Append(" where LabCode=@LabCode and LabDoctorNo=@LabDoctorNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@LabCode", SqlDbType.VarChar,50),
					new SqlParameter("@LabDoctorNo", SqlDbType.Int,4)};
            parameters[0].Value = LabCode;
            parameters[1].Value = LabDoctorNo;


            return idb.ExecuteNonQuery(strSql.ToString(), parameters);

        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int DeleteList(string DoctorIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from B_Lab_Doctor ");
            strSql.Append(" where ID in (" + DoctorIDlist + ")  ");
            return idb.ExecuteNonQuery(strSql.ToString());

        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.Lab_Doctor GetModel(string LabCode, int LabDoctorNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DoctorID, LabCode, LabDoctorNo, CName, ShortCode, HisOrderCode, Visible, DTimeStampe, AddTime, StandCode, ZFStandCode, UseFlag  ");
            strSql.Append("  from B_Lab_Doctor ");
            strSql.Append(" where LabCode=@LabCode and LabDoctorNo=@LabDoctorNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@LabCode", SqlDbType.VarChar,50),
					new SqlParameter("@LabDoctorNo", SqlDbType.Int,4)};
            parameters[0].Value = LabCode;
            parameters[1].Value = LabDoctorNo;


            ZhiFang.Model.Lab_Doctor model = new ZhiFang.Model.Lab_Doctor();
            DataSet ds = idb.ExecuteDataSet(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["DoctorID"].ToString() != "")
                {
                    model.DoctorID = int.Parse(ds.Tables[0].Rows[0]["DoctorID"].ToString());
                }
                model.LabCode = ds.Tables[0].Rows[0]["LabCode"].ToString();
                if (ds.Tables[0].Rows[0]["LabDoctorNo"].ToString() != "")
                {
                    model.LabDoctorNo = int.Parse(ds.Tables[0].Rows[0]["LabDoctorNo"].ToString());
                }
                model.CName = ds.Tables[0].Rows[0]["CName"].ToString();
                model.ShortCode = ds.Tables[0].Rows[0]["ShortCode"].ToString();
                model.HisOrderCode = ds.Tables[0].Rows[0]["HisOrderCode"].ToString();
                if (ds.Tables[0].Rows[0]["Visible"].ToString() != "")
                {
                    model.Visible = int.Parse(ds.Tables[0].Rows[0]["Visible"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AddTime"].ToString() != "")
                {
                    model.AddTime = DateTime.Parse(ds.Tables[0].Rows[0]["AddTime"].ToString());
                }
                model.StandCode = ds.Tables[0].Rows[0]["StandCode"].ToString();
                model.ZFStandCode = ds.Tables[0].Rows[0]["ZFStandCode"].ToString();
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
            strSql.Append(" FROM B_Lab_Doctor ");
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
            strSql.Append(" FROM B_Lab_Doctor ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            if (filedOrder.Trim() != "")
                strSql.Append(" order by " + filedOrder);
            return idb.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 根据实体获取DataSet
        /// </summary>
        public DataSet GetList(ZhiFang.Model.Lab_Doctor model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM B_Lab_Doctor where 1=1 ");

            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }


            if (model.LabDoctorNo != 0)
            {
                strSql.Append(" and LabDoctorNo=" + model.LabDoctorNo + " ");
            }

            if (model.CName != null)
            {
                strSql.Append(" and CName='%" + model.CName + "%' ");
            }

            if (model.ShortCode != null)
            {
                strSql.Append(" and ShortCode='%" + model.ShortCode + "%' ");
            }

            if (model.HisOrderCode != null)
            {
                strSql.Append(" and HisOrderCode='" + model.HisOrderCode + "' ");
            }

            if (model.DTimeStampe != null)
            {
                strSql.Append(" and DTimeStampe='" + model.DTimeStampe + "' ");
            }

            if (model.StandCode != null)
            {
                strSql.Append(" and StandCode='" + model.StandCode + "' ");
            }

            if (model.ZFStandCode != null)
            {
                strSql.Append(" and ZFStandCode='" + model.ZFStandCode + "' ");
            }
            return idb.ExecuteDataSet(strSql.ToString());
        }
        public DataSet GetListByLike(ZhiFang.Model.Lab_Doctor model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ,'('+convert(varchar(100),LabDoctorNo)+')'+CName as LabDoctorNoAndName ");
            strSql.Append(" FROM B_Lab_Doctor where 1=1 ");
            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }
            if (model.CName != null)
            {
                strSql.Append(" and ( CName like '%" + model.CName + "%' ");
            }
            if (model.LabDoctorNo != 0)
            {
                strSql.Append(" or LabDoctorNo like '%" + model.LabDoctorNo + "%' ");
            }
            if (model.ShortCode != null)
            {
                strSql.Append(" or ShortCode like '%" + model.ShortCode + "%' ");
            }

            if (strSql.ToString().IndexOf("like") >= 0)
                strSql.Append(" ) ");

            return idb.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 获取总记录
        /// </summary>
        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_Lab_Doctor ");
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
        public int GetTotalCount(ZhiFang.Model.Lab_Doctor model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_Lab_Doctor where 1=1 ");
            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode = '" + model.LabCode + "'");
            }
            string strLike = "";
            if (model.SearchLikeKey != null)
            {
                strLike = " and (LabDoctorNo like '%" + model.SearchLikeKey + "%' or CName like '%" + model.SearchLikeKey + "%' or ShortCode like '%" + model.SearchLikeKey + "%') ";
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

        /// <summary>
        /// 利用标识列分页
        /// </summary>
        /// <param name="model">model=null时为中心字典表分页；model!=null时为中心--实验室 对照关系分页</param>
        /// <param name="nowPageNum">第几页</param>
        /// <param name="nowPageSize">每页多少行</param>
        /// <returns>DataSet</returns>
        public DataSet GetListByPage(ZhiFang.Model.Lab_Doctor model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            string strLike = "";
            if (model.SearchLikeKey != null)
            {
                strLike = " and (LabDoctorNo like '%" + model.SearchLikeKey + "%' or CName like '%" + model.SearchLikeKey + "%' or ShortCode like '%" + model.SearchLikeKey + "%') ";
            }
            strSql.Append("select top " + nowPageSize + " * from B_Lab_Doctor where 1=1  ");
            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }
            strSql.Append(" " + strLike + " and DoctorID not in ");
            strSql.Append("(select top " + (nowPageSize * nowPageNum) + " DoctorID from B_Lab_Doctor where 1=1  ");
            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }
            strSql.Append(" " + strLike + " order by " + model.OrderField + " desc ) order by " + model.OrderField + " desc ");



			#region
			//StringBuilder strSql = new StringBuilder();
            //strSql.Append("select top " + nowPageSize + " * from B_Lab_Doctor where 1=1  ");



            //if (model.LabCode != null)
            //{
            //    strSql.Append(" and LabCode='" + model.LabCode + "' ");
            //}
            //if (model.LabDoctorNo != 0)
            //{
            //    strSql.Append(" and LabDoctorNo=" + model.LabDoctorNo + " ");
            //}


            //if (model.CName != null)
            //{
            //    strSql.Append(" and CName='" + model.CName + "' ");
            //}


            //if (model.ShortCode != null)
            //{
            //    strSql.Append(" and ShortCode='" + model.ShortCode + "' ");
            //}


            //if (model.HisOrderCode != null)
            //{
            //    strSql.Append(" and HisOrderCode='" + model.HisOrderCode + "' ");
            //}


            //if (model.DTimeStampe != null)
            //{
            //    strSql.Append(" and DTimeStampe='" + model.DTimeStampe + "' ");
            //}


            //if (model.StandCode != null)
            //{
            //    strSql.Append(" and StandCode='" + model.StandCode + "' ");
            //}


            //if (model.ZFStandCode != null)
            //{
            //    strSql.Append(" and ZFStandCode='" + model.ZFStandCode + "' ");
            //}
            //strSql.Append("and DoctorID not in ");
            //strSql.Append("(select top " + (nowPageSize * nowPageNum) + " DoctorID from B_Lab_Doctor where 1=1  ");

            //if (model.LabCode != null)
            //{
            //    strSql.Append(" and LabCode='" + model.LabCode + "' ");
            //}
            //if (model.LabDoctorNo != 0)
            //{
            //    strSql.Append(" and LabDoctorNo=" + model.LabDoctorNo + " ");
            //}


            //if (model.CName != null)
            //{
            //    strSql.Append(" and CName='" + model.CName + "' ");
            //}


            //if (model.ShortCode != null)
            //{
            //    strSql.Append(" and ShortCode='" + model.ShortCode + "' ");
            //}


            //if (model.HisOrderCode != null)
            //{
            //    strSql.Append(" and HisOrderCode='" + model.HisOrderCode + "' ");
            //}


            //if (model.DTimeStampe != null)
            //{
            //    strSql.Append(" and DTimeStampe='" + model.DTimeStampe + "' ");
            //}


            //if (model.StandCode != null)
            //{
            //    strSql.Append(" and StandCode='" + model.StandCode + "' ");
            //}


            //if (model.ZFStandCode != null)
            //{
            //    strSql.Append(" and ZFStandCode='" + model.ZFStandCode + "' ");
            //}

			//strSql.Append(" order by DoctorID) order by DoctorID  ");
			#endregion
			return idb.ExecuteDataSet(strSql.ToString());
        }

        public bool Exists(string LabCode, int LabDoctorNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from B_Lab_Doctor ");
            strSql.Append(" where LabCode=@LabCode and LabDoctorNo=@LabDoctorNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@LabCode", SqlDbType.VarChar,50),
					new SqlParameter("@LabDoctorNo", SqlDbType.Int,4)};
            parameters[0].Value = LabCode;
            parameters[1].Value = LabDoctorNo;


            DataSet ds = idb.ExecuteDataSet(strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0 && ds.Tables[0].Rows[0][0].ToString().Trim() != "0")
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
            return idb.GetMaxID("LabCode,LabDoctorNo", "B_Lab_Doctor");
        }

        public DataSet GetList(int Top, ZhiFang.Model.Lab_Doctor model, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM B_Lab_Doctor ");


            if (model.LabCode != null)
            {

                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }

            if (model.LabDoctorNo != null)
            {
                strSql.Append(" and LabDoctorNo=" + model.LabDoctorNo + " ");
            }

            if (model.CName != null)
            {

                strSql.Append(" and CName='" + model.CName + "' ");
            }

            if (model.ShortCode != null)
            {

                strSql.Append(" and ShortCode='" + model.ShortCode + "' ");
            }

            if (model.HisOrderCode != null)
            {

                strSql.Append(" and HisOrderCode='" + model.HisOrderCode + "' ");
            }

            if (model.Visible != null)
            {
                strSql.Append(" and Visible=" + model.Visible + " ");
            }

            if (model.DTimeStampe != null)
            {

                strSql.Append(" and DTimeStampe='" + model.DTimeStampe + "' ");
            }

            if (model.AddTime != null)
            {

                strSql.Append(" and AddTime='" + model.AddTime + "' ");
            }

            if (model.StandCode != null)
            {

                strSql.Append(" and StandCode='" + model.StandCode + "' ");
            }

            if (model.ZFStandCode != null)
            {

                strSql.Append(" and ZFStandCode='" + model.ZFStandCode + "' ");
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



        #region IDataBase<Lab_Doctor> 成员
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
                        if (this.Exists(ds.Tables[0].Rows[i]["LabCode"].ToString().Trim(), int.Parse(ds.Tables[0].Rows[i]["LabDoctorNo"].ToString().Trim())))
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
                strSql.Append("insert into B_Lab_Doctor (");
                strSql.Append("LabCode,LabDoctorNo,CName,ShortCode,HisOrderCode,Visible,AddTime,StandCode,ZFStandCode,UseFlag");
                strSql.Append(") values (");
                strSql.Append("'" + dr["LabCode"].ToString().Trim() + "','" + dr["LabDoctorNo"].ToString().Trim() + "','" + dr["CName"].ToString().Trim() + "','" + dr["ShortCode"].ToString().Trim() + "','" + dr["HisOrderCode"].ToString().Trim() + "','" + dr["Visible"].ToString().Trim() + "','" + DateTime.Parse(dr["AddTime"].ToString().Trim()) + "','" + dr["StandCode"].ToString().Trim() + "','" + dr["ZFStandCode"].ToString().Trim() + "','" + dr["UseFlag"].ToString().Trim() + "'");
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
                strSql.Append("update B_Lab_Doctor set ");

                strSql.Append(" CName = '" + dr["CName"].ToString().Trim() + "' , ");
                strSql.Append(" ShortCode = '" + dr["ShortCode"].ToString().Trim() + "' , ");
                strSql.Append(" HisOrderCode = '" + dr["HisOrderCode"].ToString().Trim() + "' , ");
                strSql.Append(" Visible = '" + dr["Visible"].ToString().Trim() + "' , ");
                strSql.Append(" StandCode = '" + dr["StandCode"].ToString().Trim() + "' , ");
                strSql.Append(" ZFStandCode = '" + dr["ZFStandCode"].ToString().Trim() + "' , ");
                strSql.Append(" UseFlag = '" + dr["UseFlag"].ToString().Trim() + "'  ");
                strSql.Append(" where LabCode='" + dr["LabCode"].ToString().Trim() + "' and LabDoctorNo='" + dr["LabDoctorNo"].ToString().Trim() + "' ");

                return idb.ExecuteNonQuery(strSql.ToString());
            }
            catch
            {
                return 0;
            }
        }
        #endregion
    }
}

