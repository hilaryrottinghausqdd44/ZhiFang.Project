using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.Oracle.weblis
{
    //B_Lab_SampleType
    public partial class B_Lab_SampleType : BaseDALLisDB, IDLab_SampleType
    {

        public B_Lab_SampleType(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public B_Lab_SampleType()
        {
        }
        D_LogInfo d_log = new D_LogInfo();
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.Lab_SampleType model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.LabCode != null)
            {
                strSql1.Append("LabCode,");
                strSql2.Append("'" + model.LabCode + "',");
            }
            if (model.LabSampleTypeNo != null)
            {
                strSql1.Append("LabSampleTypeNo,");
                strSql2.Append("" + model.LabSampleTypeNo + ",");
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
            strSql1.Append("DTimeStampe,");
            strSql2.Append("sysdate+ '1.1234',");

            if (model.AddTime != null)
            {
                strSql1.Append("AddTime,");
                strSql2.Append("to_date('" + model.AddTime.ToString() + "','YYYY-MM-DD HH24:MI:SS'),");
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
            if (model.code_1 != null)
            {
                strSql1.Append("code_1,");
                strSql2.Append("'" + model.code_1 + "',");
            }
            if (model.code_2 != null)
            {
                strSql1.Append("code_2,");
                strSql2.Append("'" + model.code_2 + "',");
            }
            if (model.code_3 != null)
            {
                strSql1.Append("code_3,");
                strSql2.Append("'" + model.code_3 + "',");
            }
            strSql.Append("insert into B_Lab_SampleType(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            //strSql.Append(";select @@IDENTITY");
            if (DbHelperSQL.ExecuteNonQuery(strSql.ToString()) > 0)
            {
                return d_log.OperateLog("SampleType", "", "", DateTime.Now, 1);
            }
            else
                return -1;

            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("insert into B_Lab_SampleType(");
            //strSql.Append("LabCode,LabSampleTypeNo,CName,ShortCode,Visible,DispOrder,HisOrderCode,StandCode,ZFStandCode,UseFlag");
            //strSql.Append(") values (");
            //strSql.Append("@LabCode,@LabSampleTypeNo,@CName,@ShortCode,@Visible,@DispOrder,@HisOrderCode,@StandCode,@ZFStandCode,@UseFlag");
            //strSql.Append(") ");

            //SqlParameter[] parameters = {
            //            new SqlParameter("@LabCode", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@LabSampleTypeNo", SqlDbType.Int,4) ,            
            //            new SqlParameter("@CName", SqlDbType.VarChar,20) ,            
            //            new SqlParameter("@ShortCode", SqlDbType.VarChar,10) ,            
            //            new SqlParameter("@Visible", SqlDbType.Int,4) ,            
            //            new SqlParameter("@DispOrder", SqlDbType.Int,4) ,            
            //            new SqlParameter("@HisOrderCode", SqlDbType.VarChar,20) ,            
            //            new SqlParameter("@StandCode", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@ZFStandCode", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@UseFlag", SqlDbType.Int,4)             

            //};

            //parameters[0].Value = model.LabCode;
            //parameters[1].Value = model.LabSampleTypeNo;
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
            //    return d_log.OperateLog("SampleType", "", "", DateTime.Now, 1);
            //}
            //else
            //    return -1;
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.Lab_SampleType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update B_Lab_SampleType set ");
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
                strSql.Append("AddTime=to_date('" + model.AddTime.ToString() + "','YYYY-MM-DD HH24:MI:SS'),");
            }
            else
            {
                strSql.Append("AddTime= null ,");
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
            if (model.code_1 != null)
            {
                strSql.Append("code_1='" + model.code_1 + "',");
            }
            else
            {
                strSql.Append("code_1= null ,");
            }
            if (model.code_2 != null)
            {
                strSql.Append("code_2='" + model.code_2 + "',");
            }
            else
            {
                strSql.Append("code_2= null ,");
            }
            if (model.code_3 != null)
            {
                strSql.Append("code_3='" + model.code_3 + "',");
            }
            else
            {
                strSql.Append("code_3= null ,");
            }
            strSql.Append("DTimeStampe = sysdate+ '1.1234',");
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where LabCode='" + model.LabCode + "' and LabSampleTypeNo=" + model.LabSampleTypeNo + "  ");

            if (DbHelperSQL.ExecuteNonQuery(strSql.ToString()) > 0)
            {
                return d_log.OperateLog("SampleType", "", "", DateTime.Now, 1);
            }
            else
                return -1;

            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("update B_Lab_SampleType set ");

            //strSql.Append(" LabCode = @LabCode , ");
            //strSql.Append(" LabSampleTypeNo = @LabSampleTypeNo , ");
            //strSql.Append(" CName = @CName , ");
            //strSql.Append(" ShortCode = @ShortCode , ");
            //strSql.Append(" Visible = @Visible , ");
            //strSql.Append(" DispOrder = @DispOrder , ");
            //strSql.Append(" HisOrderCode = @HisOrderCode , ");
            //strSql.Append(" StandCode = @StandCode , ");
            //strSql.Append(" ZFStandCode = @ZFStandCode , ");
            //strSql.Append(" UseFlag = @UseFlag  ");
            //strSql.Append(" where LabCode=@LabCode and LabSampleTypeNo=@LabSampleTypeNo  ");

            //SqlParameter[] parameters = {


            //new SqlParameter("@LabCode", SqlDbType.VarChar,50) ,            	

            //new SqlParameter("@LabSampleTypeNo", SqlDbType.Int,4) ,            	

            //new SqlParameter("@CName", SqlDbType.VarChar,20) ,            	

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



            //if (model.LabSampleTypeNo != null)
            //{
            //    parameters[1].Value = model.LabSampleTypeNo;
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
            //    return d_log.OperateLog("SampleType", "", "", DateTime.Now, 1);
            //}
            //else
            //    return -1;
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string LabCode, int LabSampleTypeNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from B_Lab_SampleType ");
            strSql.Append(" where LabCode='" + LabCode + "' and LabSampleTypeNo='" + LabSampleTypeNo + "' ");
            //SqlParameter[] parameters = {
            //        new SqlParameter("@LabCode", SqlDbType.VarChar,50),
            //        new SqlParameter("@LabSampleTypeNo", SqlDbType.Int,4)};
            //parameters[0].Value = LabCode;
            //parameters[1].Value = LabSampleTypeNo;


            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());

        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int DeleteList(string SampleTypeIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from B_Lab_SampleType ");
            strSql.Append(" where ID in (" + SampleTypeIDlist + ")  ");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());

        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.Lab_SampleType GetModel(string LabCode, int LabSampleTypeNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SampleTypeID, LabCode, LabSampleTypeNo, CName, ShortCode, Visible, DispOrder, HisOrderCode, DTimeStampe, AddTime, StandCode, ZFStandCode, UseFlag  ");
            strSql.Append("  from B_Lab_SampleType ");
            strSql.Append(" where LabCode='" + LabCode + "' and LabSampleTypeNo=" + LabSampleTypeNo + " ");
            //strSql.Append(" where LabCode=@LabCode and LabSampleTypeNo=@LabSampleTypeNo ");
            //SqlParameter[] parameters = {
            //        new SqlParameter("@LabCode", SqlDbType.VarChar,50),
            //        new SqlParameter("@LabSampleTypeNo", SqlDbType.Int,4)};
            //parameters[0].Value = LabCode;
            //parameters[1].Value = LabSampleTypeNo;


            ZhiFang.Model.Lab_SampleType model = new ZhiFang.Model.Lab_SampleType();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["SampleTypeID"].ToString() != "")
                {
                    model.SampleTypeID = int.Parse(ds.Tables[0].Rows[0]["SampleTypeID"].ToString());
                }
                model.LabCode = ds.Tables[0].Rows[0]["LabCode"].ToString();
                if (ds.Tables[0].Rows[0]["LabSampleTypeNo"].ToString() != "")
                {
                    model.LabSampleTypeNo = ds.Tables[0].Rows[0]["LabSampleTypeNo"].ToString();
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
            strSql.Append(" FROM B_Lab_SampleType ");
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

            strSql.Append("select * FROM B_Lab_SampleType ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
                if (Top > 0)
                    strSql.Append(" and ROWNUM <= '" + Top + "'");
            }
            else if (Top > 0)
            {
                strSql.Append(" where ROWNUM <= '" + Top + "'");
            }
            if (filedOrder.Trim() != "")
                strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 根据实体获取DataSet
        /// </summary>
        public DataSet GetList(ZhiFang.Model.Lab_SampleType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM B_Lab_SampleType where 1=1 ");

            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }


            if (model.LabSampleTypeNo != null)
            {
                strSql.Append(" and LabSampleTypeNo='" + model.LabSampleTypeNo + " '");
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
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
        public DataSet GetListByLike(ZhiFang.Model.Lab_SampleType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select B_Lab_SampleType.* ,concat(concat('(',CONCAT(LabSampleTypeNo,')')),cname) as LabSampleTypeNoAndName ");
            strSql.Append(" FROM B_Lab_SampleType where 1=1 ");
            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }
            if (model.CName != null)
            {
                strSql.Append(" and ( CName like '%" + model.CName + "%' ");
            }
            if (model.LabSampleTypeNo != null)
            {
                strSql.Append(" or LabSampleTypeNo like '%" + model.LabSampleTypeNo + "%' ");
            }
            if (model.ShortCode != null)
            {
                strSql.Append(" or ShortCode like '%" + model.ShortCode + "%' ");
            }

            if (strSql.ToString().IndexOf("like") >= 0)
                strSql.Append(" ) ");
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 获取总记录
        /// </summary>
        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_Lab_SampleType ");
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
        public int GetTotalCount(ZhiFang.Model.Lab_SampleType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_Lab_SampleType where 1=1 ");
            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }
            string strLike = "";
            if (model.SearchLikeKey != null)
            {
                strLike = " and (LabSampleTypeNo like '%" + model.SearchLikeKey + "%' or CName like '%" + model.SearchLikeKey + "%' or ShortCode like '%" + model.SearchLikeKey + "%') ";
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
        public DataSet GetListByPage(ZhiFang.Model.Lab_SampleType model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  * from B_Lab_SampleType where 1=1  ");

            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }
            string strLike = "";
            if (model.SearchLikeKey != null)
            {
                strLike = " and (LabSampleTypeNo like '%" + model.SearchLikeKey + "%' or CName like '%" + model.SearchLikeKey + "%' or ShortCode like '%" + model.SearchLikeKey + "%') ";
            }
            strSql.Append("and  ROWNUM <= '" + nowPageSize + "' and SampleTypeID not in ");
            strSql.Append("(select SampleTypeID from B_Lab_SampleType where 1=1 and ROWNUM <= '" + (nowPageSize * nowPageNum) + "' " + strLike + "  ");

            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }
            strSql.Append(" ) " + strLike + " order by " + model.OrderField + "  ");
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public bool Exists(string LabCode, int LabSampleTypeNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from B_Lab_SampleType");
            strSql.Append(" where LabCode='" + LabCode + "' and LabSampleTypeNo=" + LabSampleTypeNo + " ");
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
            strSql.Append("select count(1) from B_Lab_SampleType where 1=1 ");
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
            return DbHelperSQL.GetMaxID("LabCode,LabSampleTypeNo", "B_Lab_SampleType");
        }

        public DataSet GetList(int Top, ZhiFang.Model.Lab_SampleType model, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");

            strSql.Append(" * ");
            strSql.Append(" FROM B_Lab_SampleType ");


            if (model.LabCode != null)
            {

                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }

            if (model.LabSampleTypeNo != null)
            {
                strSql.Append(" and LabSampleTypeNo=" + model.LabSampleTypeNo + " ");
            }

            if (model.CName != null)
            {

                strSql.Append(" and CName='" + model.CName + "' ");
            }

            if (model.ShortCode != null)
            {

                strSql.Append(" and ShortCode='" + model.ShortCode + "' ");
            }

            if (model.Visible != null)
            {
                strSql.Append(" and Visible=" + model.Visible + " ");
            }

            if (model.DispOrder != null)
            {
                strSql.Append(" and DispOrder=" + model.DispOrder + " ");
            }

            if (model.HisOrderCode != null)
            {

                strSql.Append(" and HisOrderCode='" + model.HisOrderCode + "' ");
            }

            if (model.DTimeStampe != null)
            {

                strSql.Append(" and DTimeStampe='" + model.DTimeStampe + "' ");
            }

            if (model.AddTime != null)
            {
                strSql.Append(" and AddTime=to_date('" + model.AddTime.ToString() + "','YYYY-MM-DD HH24:MI:SS'),");
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

            strSql.Append(" and ROWNUM <= '" + Top + "'");

            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetAllList()
        {
            return GetList("");
        }



        #region IDataBase<Lab_SampleType> 成员
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
                        if (this.Exists(ds.Tables[0].Rows[i]["LabCode"].ToString().Trim(), int.Parse(ds.Tables[0].Rows[i]["LabSampleTypeNo"].ToString().Trim())))
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
                strSql.Append("insert into B_Lab_SampleType (");
                strSql.Append("LabCode,LabSampleTypeNo,CName,ShortCode,Visible,DispOrder,HisOrderCode,StandCode,ZFStandCode,UseFlag");
                strSql.Append(") values (");
                if (dr.Table.Columns["LabCode"] != null && dr.Table.Columns["LabCode"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["LabCode"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["LabSampleTypeNo"] != null && dr.Table.Columns["LabSampleTypeNo"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["LabSampleTypeNo"].ToString().Trim() + "', ");
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
                ZhiFang.Common.Log.Log.Error("同步数据时错误：", ex);
                return 0;
            }
        }
        public int UpdateByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update B_Lab_SampleType set ");


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
                    strSql.Append(" UseFlag = '" + dr["UseFlag"].ToString().Trim() + "',  ");
                }
                int n = strSql.ToString().LastIndexOf(",");
                strSql.Remove(n, 1);
                strSql.Append(" where LabCode='" + dr["LabCode"].ToString().Trim() + "' and LabSampleTypeNo='" + dr["LabSampleTypeNo"].ToString().Trim() + "' ");

                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("同步数据时错误：", ex);
                return 0;
            }
        }

        #endregion

        /// <summary>
        /// 根据实验室端的名称查找对应的实验室端的编码
        /// </summary>
        /// <param name="LabCode"></param>
        /// <param name="LabCnameList">实验室对应的名称</param>
        /// <returns>实验室端的编码</returns>
        public DataSet GetLabCodeNo(string LabCode, List<string> LabCnameList)
        {
            DataSet ds = new DataSet();
            try
            {
                string listNames = "";
                for (int i = 0; i < LabCnameList.Count; i++)
                {
                    if (listNames.Trim() == "")
                        listNames = "'" + LabCnameList[i].Trim() + "'";
                    else
                        listNames += "," + "'" + LabCnameList[i].Trim() + "'";
                }
                string strSql = "select LabSampleTypeNo from B_Lab_SampleType where LabCode='" + LabCode.Trim() + "' and CName in (" + listNames + ")";
                ZhiFang.Common.Log.Log.Info("ZhiFang.DAL.Oracle.weblis.B_Lab_SampleType.GetLabCodeNo:" + strSql);
                ds = DbHelperSQL.ExecuteDataSet(strSql);
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.B_Lab_SampleType.GetLabCodeNo异常->", ex);
            }
            return ds;
        }
    }
}

