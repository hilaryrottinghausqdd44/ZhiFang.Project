using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.Oracle.weblis
{
    //b_lab_GenderType		
    public partial class B_Lab_GenderType : BaseDALLisDB, IDLab_GenderType
    {
        public B_Lab_GenderType(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public B_Lab_GenderType()
        {
        }
        D_LogInfo d_log = new D_LogInfo();
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.Lab_GenderType model)
        {

            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.LabCode != null)
            {
                strSql1.Append("LabCode,");
                strSql2.Append("'" + model.LabCode + "',");
            }
            if (model.LabGenderNo != null)
            {
                strSql1.Append("LabGenderNo,");
                strSql2.Append("" + model.LabGenderNo + ",");
            }
            if (model.CName != null)
            {
                strSql1.Append("CName,");
                strSql2.Append("'" + model.CName + "',");
            }
            if (model.ShortCode != null)
            {
                strSql1.Append("ShortCode,");
                strSql2.Append("'" + model.ShortCode + "',");
            }
            if (model.Visible != null)
            {
                strSql1.Append("Visible,");
                strSql2.Append("" + model.Visible + ",");
            }
            if (model.DispOrder != null)
            {
                strSql1.Append("DispOrder,");
                strSql2.Append("" + model.DispOrder + ",");
            }
            if (model.HisOrderCode != null)
            {
                strSql1.Append("HisOrderCode,");
                strSql2.Append("'" + model.HisOrderCode + "',");
            }
            if (model.AddTime != null)
            {
                strSql1.Append("AddTime,");
                strSql2.Append(" to_date('" + model.AddTime.ToString() + "','YYYY-MM-DD HH24:MI:SS'),");
            }
            if (model.StandCode != null)
            {
                strSql1.Append("StandCode,");
                strSql2.Append("'" + model.StandCode + "',");
            }
            if (model.ZFStandCode != null)
            {
                strSql1.Append("ZFStandCode,");
                strSql2.Append("'" + model.ZFStandCode + "',");
            }
            if (model.UseFlag != null)
            {
                strSql1.Append("UseFlag,");
                strSql2.Append("" + model.UseFlag + ",");
            }
            strSql1.Append("DTimeStampe,");
            strSql2.Append("Systimestamp,");

            strSql.Append("insert into b_lab_GenderType(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            if (DbHelperSQL.ExecuteNonQuery(strSql.ToString()) > 0)
            {
                return d_log.OperateLog("GenderType", "", "", DateTime.Now, 1);
            }
            else
                return -1;

            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("insert into b_lab_GenderType(");
            //strSql.Append("LabCode,LabGenderNo,CName,ShortCode,Visible,DispOrder,HisOrderCode,StandCode,ZFStandCode,UseFlag");
            //strSql.Append(") values (");
            //strSql.Append("@LabCode,@LabGenderNo,@CName,@ShortCode,@Visible,@DispOrder,@HisOrderCode,@StandCode,@ZFStandCode,@UseFlag");
            //strSql.Append(") ");

            //SqlParameter[] parameters = {
            //            new SqlParameter("@LabCode", SqlDbType.VarChar,100) ,            
            //            new SqlParameter("@LabGenderNo", SqlDbType.Int,4) ,            
            //            new SqlParameter("@CName", SqlDbType.VarChar,10) ,            
            //            new SqlParameter("@ShortCode", SqlDbType.VarChar,10) ,            
            //            new SqlParameter("@Visible", SqlDbType.Int,4) ,            
            //            new SqlParameter("@DispOrder", SqlDbType.Int,4) ,            
            //            new SqlParameter("@HisOrderCode", SqlDbType.VarChar,20) ,            
            //            new SqlParameter("@StandCode", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@ZFStandCode", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@UseFlag", SqlDbType.Int,4)             
              
            //};

            //parameters[0].Value = model.LabCode;
            //parameters[1].Value = model.LabGenderNo;
            //parameters[2].Value = model.CName;
            //parameters[3].Value = model.ShortCode;
            //parameters[4].Value = model.Visible;
            //parameters[5].Value = model.DispOrder;
            //parameters[6].Value = model.HisOrderCode;
            //parameters[7].Value = model.StandCode;
            //parameters[8].Value = model.ZFStandCode;
            //parameters[9].Value = model.UseFlag;
            //if (DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters) > 0)
            //{
            //    return d_log.OperateLog("GenderType", "", "", DateTime.Now, 1);
            //}
            //else
            //    return -1;
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.Lab_GenderType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update b_lab_GenderType set ");
            if (model.CName != null)
            {
                strSql.Append("CName='" + model.CName + "',");
            }
            else
            {
                strSql.Append("CName= null ,");
            }
            if (model.ShortCode != null)
            {
                strSql.Append("ShortCode='" + model.ShortCode + "',");
            }
            else
            {
                strSql.Append("ShortCode= null ,");
            }
            if (model.Visible != null)
            {
                strSql.Append("Visible=" + model.Visible + ",");
            }
            else
            {
                strSql.Append("Visible= null ,");
            }
            if (model.DispOrder != null)
            {
                strSql.Append("DispOrder=" + model.DispOrder + ",");
            }
            else
            {
                strSql.Append("DispOrder= null ,");
            }
            if (model.HisOrderCode != null)
            {
                strSql.Append("HisOrderCode='" + model.HisOrderCode + "',");
            }
            else
            {
                strSql.Append("HisOrderCode= null ,");
            }
            if (model.AddTime != null)
            {
                strSql.Append(" AddTime=to_date('" + model.AddTime.ToString() + "','YYYY-MM-DD HH24:MI:SS'),");
            }
            if (model.StandCode != null)
            {
                strSql.Append("StandCode='" + model.StandCode + "',");
            }
            else
            {
                strSql.Append("StandCode= null ,");
            }
            if (model.ZFStandCode != null)
            {
                strSql.Append("ZFStandCode='" + model.ZFStandCode + "',");
            }
            else
            {
                strSql.Append("ZFStandCode= null ,");
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
            strSql.Append(" where LabCode='" + model.LabCode + "' and LabGenderNo=" + model.LabGenderNo + "  ");
            if (DbHelperSQL.ExecuteNonQuery(strSql.ToString()) > 0)
            {
                return d_log.OperateLog("GenderType", "", "", DateTime.Now, 1);
            }
            else
                return -1;

            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("update b_lab_GenderType set ");

            //strSql.Append(" LabCode = @LabCode , ");
            //strSql.Append(" LabGenderNo = @LabGenderNo , ");
            //strSql.Append(" CName = @CName , ");
            //strSql.Append(" ShortCode = @ShortCode , ");
            //strSql.Append(" Visible = @Visible , ");
            //strSql.Append(" DispOrder = @DispOrder , ");
            //strSql.Append(" HisOrderCode = @HisOrderCode , ");
            //strSql.Append(" StandCode = @StandCode , ");
            //strSql.Append(" ZFStandCode = @ZFStandCode , ");
            //strSql.Append(" UseFlag = @UseFlag  ");
            //strSql.Append(" where LabCode=@LabCode and LabGenderNo=@LabGenderNo  ");

            //SqlParameter[] parameters = {
			            	
                           
            //new SqlParameter("@LabCode", SqlDbType.VarChar,100) ,            	
                           
            //new SqlParameter("@LabGenderNo", SqlDbType.Int,4) ,            	
                           
            //new SqlParameter("@CName", SqlDbType.VarChar,10) ,            	
                           
            //new SqlParameter("@ShortCode", SqlDbType.VarChar,10) ,            	
                           
            //new SqlParameter("@Visible", SqlDbType.Int,4) ,            	
                           
            //new SqlParameter("@DispOrder", SqlDbType.Int,4) ,            	
                           
            //new SqlParameter("@HisOrderCode", SqlDbType.VarChar,20) ,            	
                        	
                        	
                           
            //new SqlParameter("@StandCode", SqlDbType.VarChar,50) ,            	
                           
            //new SqlParameter("@ZFStandCode", SqlDbType.VarChar,50) ,            	
                           
            //new SqlParameter("@UseFlag", SqlDbType.Int,4)             	
              
            //};




            //if (model.LabCode != null)
            //{
            //    parameters[0].Value = model.LabCode;
            //}



            //if (model.LabGenderNo != null)
            //{
            //    parameters[1].Value = model.LabGenderNo;
            //}



            //if (model.CName != null)
            //{
            //    parameters[2].Value = model.CName;
            //}



            //if (model.ShortCode != null)
            //{
            //    parameters[3].Value = model.ShortCode;
            //}



            //if (model.Visible != null)
            //{
            //    parameters[4].Value = model.Visible;
            //}



            //if (model.DispOrder != null)
            //{
            //    parameters[5].Value = model.DispOrder;
            //}



            //if (model.HisOrderCode != null)
            //{
            //    parameters[6].Value = model.HisOrderCode;
            //}







            //if (model.StandCode != null)
            //{
            //    parameters[7].Value = model.StandCode;
            //}



            //if (model.ZFStandCode != null)
            //{
            //    parameters[8].Value = model.ZFStandCode;
            //}



            //if (model.UseFlag != null)
            //{
            //    parameters[9].Value = model.UseFlag;
            //}


            //if (DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters) > 0)
            //{
            //    return d_log.OperateLog("GenderType", "", "", DateTime.Now, 1);
            //}
            //else
            //    return -1;
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string LabCode, int LabGenderNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from b_lab_GenderType ");
            strSql.Append(" where LabCode='"+LabCode+"' and LabGenderNo="+LabGenderNo+" ");
            //strSql.Append(" where LabCode=@LabCode and LabGenderNo=@LabGenderNo ");
            //SqlParameter[] parameters = {
            //        new SqlParameter("@LabCode", SqlDbType.VarChar,100),
            //        new SqlParameter("@LabGenderNo", SqlDbType.Int,4)};
            //parameters[0].Value = LabCode;
            //parameters[1].Value = LabGenderNo;


            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());

        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int DeleteList(string GenderIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from b_lab_GenderType ");
            strSql.Append(" where ID in (" + GenderIDlist + ")  ");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());

        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.Lab_GenderType GetModel(string LabCode, int LabGenderNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select GenderID, LabCode, LabGenderNo, CName, ShortCode, Visible, DispOrder, HisOrderCode, AddTime, StandCode, ZFStandCode, UseFlag  ");
            strSql.Append("  from b_lab_GenderType ");
            strSql.Append(" where LabCode='" + LabCode + "' and LabGenderNo=" + LabGenderNo + " ");
            //strSql.Append(" where LabCode=@LabCode and LabGenderNo=@LabGenderNo ");
            //SqlParameter[] parameters = {
            //        new SqlParameter("@LabCode", SqlDbType.VarChar,100),
            //        new SqlParameter("@LabGenderNo", SqlDbType.Int,4)};
            //parameters[0].Value = LabCode;
            //parameters[1].Value = LabGenderNo;


            ZhiFang.Model.Lab_GenderType model = new ZhiFang.Model.Lab_GenderType();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {

                if (ds.Tables[0].Rows[0]["GenderID"].ToString() != "")
                {
                    model.GenderID = int.Parse(ds.Tables[0].Rows[0]["GenderID"].ToString());
                }

                model.LabCode = ds.Tables[0].Rows[0]["LabCode"].ToString();

                if (ds.Tables[0].Rows[0]["LabGenderNo"].ToString() != "")
                {
                    model.LabGenderNo = int.Parse(ds.Tables[0].Rows[0]["LabGenderNo"].ToString());
                }

                model.CName = ds.Tables[0].Rows[0]["CName"].ToString();

                model.ShortCode = ds.Tables[0].Rows[0]["ShortCode"].ToString();

                if (ds.Tables[0].Rows[0]["Visible"].ToString() != "")
                {
                    model.Visible = int.Parse(ds.Tables[0].Rows[0]["Visible"].ToString());
                }

                if (ds.Tables[0].Rows[0]["DispOrder"].ToString() != "")
                {
                    model.DispOrder = int.Parse(ds.Tables[0].Rows[0]["DispOrder"].ToString());
                }

                model.HisOrderCode = ds.Tables[0].Rows[0]["HisOrderCode"].ToString();

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
            strSql.Append(" FROM b_lab_GenderType ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }


        /// <summary>
        /// 根据实体获取DataSet
        /// </summary>
        public DataSet GetList(ZhiFang.Model.Lab_GenderType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM b_lab_GenderType where 1=1 ");

            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }


            if (model.LabGenderNo != 0)
            {
                strSql.Append(" and LabGenderNo=" + model.LabGenderNo + " ");
            }

            if (model.CName != null)
            {
                strSql.Append(" and CName='" + model.CName + "' ");
            }

            if (model.ShortCode != null)
            {
                strSql.Append(" and ShortCode='" + model.ShortCode + "' ");
            }

            if (model.DispOrder != null)
            {
                strSql.Append(" and DispOrder=" + model.DispOrder + " ");
            }

            if (model.HisOrderCode != null)
            {
                strSql.Append(" and HisOrderCode='" + model.HisOrderCode + "' ");
            }

            if (model.StandCode != null)
            {
                strSql.Append(" and StandCode='" + model.StandCode + "' ");
            }

            if (model.ZFStandCode != null)
            {
                strSql.Append(" and ZFStandCode='" + model.ZFStandCode + "' ");
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
        public DataSet GetListByLike(ZhiFang.Model.Lab_GenderType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  b_lab_GenderType.*,concat(concat('(',CONCAT(LabGenderNo,')')),CName) as LabGenderNoAndName  ");
            strSql.Append(" FROM b_lab_GenderType where 1=1 ");
            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }

            string strLike = "";
            if (model.SearchLikeKey != null)
            {
                strLike = " and (LabGenderNo like '%" + model.SearchLikeKey + "%' or CName like '%" + model.SearchLikeKey + "%' or ShortCode like '%" + model.SearchLikeKey + "%') ";
            }
            strSql.Append(strLike);

            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 获取总记录
        /// </summary>
        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM b_lab_GenderType ");
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
        public int GetTotalCount(ZhiFang.Model.Lab_GenderType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM b_lab_GenderType where 1=1 ");
            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode = '" + model.LabCode + "'");
            }
            string strLike = "";
            if (model != null && model.SearchLikeKey != null)
            {
                strLike = " and (LabGenderNo like '%" + model.SearchLikeKey + "%' or CName like '%" + model.SearchLikeKey + "%' or ShortCode like '%" + model.SearchLikeKey + "%') ";
            }
            strSql.Append(strLike);
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

        /// <summary>
        /// 利用标识列分页
        /// </summary>
        /// <param name="model">model=null时为中心字典表分页；model!=null时为中心--实验室 对照关系分页</param>
        /// <param name="nowPageNum">第几页</param>
        /// <param name="nowPageSize">每页多少行</param>
        /// <returns>DataSet</returns>
        public DataSet GetListByPage(ZhiFang.Model.Lab_GenderType model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            string strLike = "";
            if (model.SearchLikeKey != null)
            {
                strLike = " and (LabGenderNo like '%" + model.SearchLikeKey + "%' or CName like '%" + model.SearchLikeKey + "%' or ShortCode like '%" + model.SearchLikeKey + "%') ";
            }
            strSql.Append("select * from b_lab_GenderType where 1=1  ");
            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }
            strSql.Append(" " + strLike + " and  ROWNUM <= '" + nowPageSize + "' and GenderID not in ");
            strSql.Append("(select GenderID from b_lab_GenderType where 1=1 and ROWNUM <= '" + (nowPageSize * nowPageNum) + "' ");
            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }
            strSql.Append(" " + strLike + " ) order by " + model.OrderField + "  ");
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public bool Exists(string LabCode, int LabGenderNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from b_lab_GenderType ");
            strSql.Append(" where LabCode='"+LabCode+"' and LabGenderNo="+LabGenderNo+" ");
            //SqlParameter[] parameters = {
            //        new SqlParameter("@LabCode", SqlDbType.VarChar,100),
            //        new SqlParameter("@LabGenderNo", SqlDbType.Int,4)};
            //parameters[0].Value = LabCode;
            //parameters[1].Value = LabGenderNo;


            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString());
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
            strSql.Append("select count(1) from b_lab_GenderType where 1=1 ");
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
            return DbHelperSQL.GetMaxID("LabCode,LabGenderNo", "b_lab_GenderType");
        }

        public DataSet GetList(int Top, ZhiFang.Model.Lab_GenderType model, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
           
            strSql.Append(" * ");
            strSql.Append(" FROM b_lab_GenderType ");



            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }


            if (model.LabGenderNo != 0)
            {
                strSql.Append(" and LabGenderNo='" + model.LabGenderNo + "' ");
            }


            if (model.CName != null)
            {
                strSql.Append(" and CName='" + model.CName + "' ");
            }


            if (model.ShortCode != null)
            {
                strSql.Append(" and ShortCode='" + model.ShortCode + "' ");
            }


            if (model.DispOrder != null)
            {
                strSql.Append(" and DispOrder='" + model.DispOrder + "' ");
            }


            if (model.HisOrderCode != null)
            {
                strSql.Append(" and HisOrderCode='" + model.HisOrderCode + "' ");
            }


            if (model.StandCode != null)
            {
                strSql.Append(" and StandCode='" + model.StandCode + "' ");
            }


            if (model.ZFStandCode != null)
            {
                strSql.Append(" and ZFStandCode='" + model.ZFStandCode + "' ");
            }
           
            strSql.Append(" and ROWNUM <= '" + Top + "'");
          
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
                        if (this.Exists(ds.Tables[0].Rows[i]["LabCode"].ToString().Trim(), int.Parse(ds.Tables[0].Rows[i]["LabGenderNo"].ToString().Trim())))
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
                strSql.Append("insert into b_lab_GenderType (");
                strSql.Append("LabCode,LabGenderNo,CName,ShortCode,Visible,DispOrder,HisOrderCode,StandCode,ZFStandCode,UseFlag");
                strSql.Append(") values (");

                if (dr.Table.Columns["LabCode"] != null && dr.Table.Columns["LabCode"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["LabCode"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["LabGenderNo"] != null && dr.Table.Columns["LabGenderNo"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["LabGenderNo"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["CName"] != null && dr.Table.Columns["CName"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["CName"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["ShortCode"] != null && dr.Table.Columns["ShortCode"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["ShortCode"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["Visible"] != null && dr.Table.Columns["Visible"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["Visible"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["DispOrder"] != null && dr.Table.Columns["DispOrder"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["DispOrder"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["HisOrderCode"] != null && dr.Table.Columns["HisOrderCode"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["HisOrderCode"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["StandCode"] != null && dr.Table.Columns["StandCode"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["StandCode"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["ZFStandCode"] != null && dr.Table.Columns["ZFStandCode"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["ZFStandCode"].ToString().Trim() + "', ");
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
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.b_lab_GenderType.AddByDataRow 同步数据时异常：", ex);
                return 0;
            }
        }
        public int UpdateByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update b_lab_GenderType set ");


                if (dr.Table.Columns["CName"] != null && dr.Table.Columns["CName"].ToString().Trim() != "")
                {
                    strSql.Append(" CName = '" + dr["CName"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["ShortCode"] != null && dr.Table.Columns["ShortCode"].ToString().Trim() != "")
                {
                    strSql.Append(" ShortCode = '" + dr["ShortCode"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["Visible"] != null && dr.Table.Columns["Visible"].ToString().Trim() != "")
                {
                    strSql.Append(" Visible = '" + dr["Visible"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["DispOrder"] != null && dr.Table.Columns["DispOrder"].ToString().Trim() != "")
                {
                    strSql.Append(" DispOrder = '" + dr["DispOrder"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["HisOrderCode"] != null && dr.Table.Columns["HisOrderCode"].ToString().Trim() != "")
                {
                    strSql.Append(" HisOrderCode = '" + dr["HisOrderCode"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["StandCode"] != null && dr.Table.Columns["StandCode"].ToString().Trim() != "")
                {
                    strSql.Append(" StandCode = '" + dr["StandCode"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["ZFStandCode"] != null && dr.Table.Columns["ZFStandCode"].ToString().Trim() != "")
                {
                    strSql.Append(" ZFStandCode = '" + dr["ZFStandCode"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["UseFlag"] != null && dr.Table.Columns["UseFlag"].ToString().Trim() != "")
                {
                    strSql.Append(" UseFlag = '" + dr["UseFlag"].ToString().Trim() + "' , ");
                }


                int n = strSql.ToString().LastIndexOf(",");
                strSql.Remove(n, 1);
                strSql.Append(" where LabCode='" + dr["LabCode"].ToString().Trim() + "' and  GenderID='" + dr["GenderID"].ToString().Trim() + "' andand LabGenderNo='" + dr["LabGenderNo"].ToString().Trim() + "'  ");

                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.b_lab_GenderType .UpdateByDataRow同步数据时异常：", ex);
                return 0;
            }
        }



        /// <summary>
        /// 根据实验室字典性别的名称获取实验室性别的编码
        /// </summary>
        /// <param name="LabCode">送检单位</param>
        /// <param name="LabCname">性别的名称</param>
        /// <returns>性别的编码</returns>
        public DataSet GetLabCodeNo(string LabCode, List<string> LabCname)
        {
            DataSet ds = new DataSet();
            try
            {
                string listNames = "";
                for (int i = 0; i < LabCname.Count; i++)
                {
                    if (listNames.Trim() == "")
                        listNames = "'" + LabCname[i].Trim() + "'";
                    else
                        listNames += "," + "'" + LabCname[i].Trim() + "'";
                }
                string strSql = "select LabGenderNo from b_lab_GenderType where LabCode='" + LabCode.Trim() + "' and CName in (" + listNames + ")";
                ZhiFang.Common.Log.Log.Info("ZhiFang.DAL.Oracle.weblis.b_lab_GenderType.GetLabCodeNo:" + strSql);
                ds = DbHelperSQL.ExecuteDataSet(strSql);
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.b_lab_GenderType.GetLabCodeNo异常->", ex);
            }
            return ds;
        }
    }
}

