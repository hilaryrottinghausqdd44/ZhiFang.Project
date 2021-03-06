using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.MsSql.Digitlab8
{
    //B_Lab_SampleType

    public partial class B_Lab_SampleType : IDLab_SampleType
    {
        DBUtility.IDBConnection idb;
        public B_Lab_SampleType(string dbsourceconn)
        {
            idb = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public B_Lab_SampleType()
        {
            idb = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.LisDB());
        }
        D_LogInfo d_log = new D_LogInfo();
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.Lab_SampleType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into B_Lab_SampleType(");
            strSql.Append("code_2,code_3,StandCode,ZFStandCode,UseFlag,LabCode,LabSampleTypeNo,CName,ShortCode,Visible,DispOrder,HisOrderCode,code_1");
            strSql.Append(") values (");
            strSql.Append("@code_2,@code_3,@StandCode,@ZFStandCode,@UseFlag,@LabCode,@LabSampleTypeNo,@CName,@ShortCode,@Visible,@DispOrder,@HisOrderCode,@code_1");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@code_2", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@code_3", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@StandCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ZFStandCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@UseFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@LabCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@LabSampleTypeNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@CName", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ShortCode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@Visible", SqlDbType.Int,4) ,            
                        new SqlParameter("@DispOrder", SqlDbType.Int,4) ,            
                        new SqlParameter("@HisOrderCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@code_1", SqlDbType.NVarChar,50)             
              
            };

            parameters[0].Value = model.code_2;
            parameters[1].Value = model.code_3;
            parameters[2].Value = model.StandCode;
            parameters[3].Value = model.ZFStandCode;
            parameters[4].Value = model.UseFlag;
            parameters[5].Value = model.LabCode;
            parameters[6].Value = model.LabSampleTypeNo;
            parameters[7].Value = model.CName;
            parameters[8].Value = model.ShortCode;
            parameters[9].Value = model.Visible;
            parameters[10].Value = model.DispOrder;
            parameters[11].Value = model.HisOrderCode;
            parameters[12].Value = model.code_1;
            if (idb.ExecuteNonQuery(strSql.ToString(), parameters) > 0)
            {
                return d_log.OperateLog("SampleType", "", "", DateTime.Now, 1);
            }
            else
                return -1;
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.Lab_SampleType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update B_Lab_SampleType set ");

            strSql.Append(" code_2 = @code_2 , ");
            strSql.Append(" code_3 = @code_3 , ");
            strSql.Append(" StandCode = @StandCode , ");
            strSql.Append(" ZFStandCode = @ZFStandCode , ");
            strSql.Append(" UseFlag = @UseFlag , ");
            strSql.Append(" LabCode = @LabCode , ");
            strSql.Append(" LabSampleTypeNo = @LabSampleTypeNo , ");
            strSql.Append(" CName = @CName , ");
            strSql.Append(" ShortCode = @ShortCode , ");
            strSql.Append(" Visible = @Visible , ");
            strSql.Append(" DispOrder = @DispOrder , ");
            strSql.Append(" HisOrderCode = @HisOrderCode , ");
            strSql.Append(" code_1 = @code_1  ");
            strSql.Append(" where LabCode=@LabCode and LabSampleTypeNo=@LabSampleTypeNo  ");

            SqlParameter[] parameters = {
			            	
                           
            new SqlParameter("@code_2", SqlDbType.NVarChar,50) ,            	
                           
            new SqlParameter("@code_3", SqlDbType.NVarChar,50) ,            	
                        	
                        	
                           
            new SqlParameter("@StandCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@ZFStandCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@UseFlag", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@LabCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@LabSampleTypeNo", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@CName", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@ShortCode", SqlDbType.VarChar,10) ,            	
                           
            new SqlParameter("@Visible", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@DispOrder", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@HisOrderCode", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@code_1", SqlDbType.NVarChar,50)             	
              
            };




            if (model.code_2 != null)
            {
                parameters[0].Value = model.code_2;
            }



            if (model.code_3 != null)
            {
                parameters[1].Value = model.code_3;
            }







            if (model.StandCode != null)
            {
                parameters[2].Value = model.StandCode;
            }



            if (model.ZFStandCode != null)
            {
                parameters[3].Value = model.ZFStandCode;
            }



            if (model.UseFlag != null)
            {
                parameters[4].Value = model.UseFlag;
            }



            if (model.LabCode != null)
            {
                parameters[5].Value = model.LabCode;
            }



            if (model.LabSampleTypeNo != null)
            {
                parameters[6].Value = model.LabSampleTypeNo;
            }



            if (model.CName != null)
            {
                parameters[7].Value = model.CName;
            }



            if (model.ShortCode != null)
            {
                parameters[8].Value = model.ShortCode;
            }



            if (model.Visible != null)
            {
                parameters[9].Value = model.Visible;
            }



            if (model.DispOrder != null)
            {
                parameters[10].Value = model.DispOrder;
            }



            if (model.HisOrderCode != null)
            {
                parameters[11].Value = model.HisOrderCode;
            }



            if (model.code_1 != null)
            {
                parameters[12].Value = model.code_1;
            }


            if (idb.ExecuteNonQuery(strSql.ToString(), parameters) > 0)
            {
                return d_log.OperateLog("SampleType", "", "", DateTime.Now, 1);
            }
            else
                return -1;
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string LabCode, int LabSampleTypeNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from B_Lab_SampleType ");
            strSql.Append(" where LabCode=@LabCode and LabSampleTypeNo=@LabSampleTypeNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@LabCode", SqlDbType.VarChar,50),
					new SqlParameter("@LabSampleTypeNo", SqlDbType.Int,4)};
            parameters[0].Value = LabCode;
            parameters[1].Value = LabSampleTypeNo;


            return idb.ExecuteNonQuery(strSql.ToString(), parameters);

        }

        public int DeleteList(string IDList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from B_Lab_SampleType ");
            strSql.Append(" where LabSampleTypeNo in("+IDList+") ");
            return idb.ExecuteNonQuery(strSql.ToString());

        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.Lab_SampleType GetModel(string LabCode, int LabSampleTypeNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SampleTypeID, code_2, code_3, DTimeStampe, AddTime, StandCode, ZFStandCode, UseFlag, LabCode, LabSampleTypeNo, CName, ShortCode, Visible, DispOrder, HisOrderCode, code_1  ");
            strSql.Append("  from B_Lab_SampleType ");
            strSql.Append(" where LabCode=@LabCode and LabSampleTypeNo=@LabSampleTypeNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@LabCode", SqlDbType.VarChar,50),
					new SqlParameter("@LabSampleTypeNo", SqlDbType.Int,4)};
            parameters[0].Value = LabCode;
            parameters[1].Value = LabSampleTypeNo;


            ZhiFang.Model.Lab_SampleType model = new ZhiFang.Model.Lab_SampleType();
            DataSet ds = idb.ExecuteDataSet(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["SampleTypeID"].ToString() != "")
                {
                    model.SampleTypeID = int.Parse(ds.Tables[0].Rows[0]["SampleTypeID"].ToString());
                }
                model.code_2 = ds.Tables[0].Rows[0]["code_2"].ToString();
                model.code_3 = ds.Tables[0].Rows[0]["code_3"].ToString();
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
                model.code_1 = ds.Tables[0].Rows[0]["code_1"].ToString();

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
            strSql.Append(" FROM B_Lab_SampleType ");
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
        public DataSet GetList(ZhiFang.Model.Lab_SampleType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM B_Lab_SampleType where 1=1 ");

            if (model.code_2 != null)
            {
                strSql.Append(" and code_2='" + model.code_2 + "' ");
            }

            if (model.code_3 != null)
            {
                strSql.Append(" and code_3='" + model.code_3 + "' ");
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

            if (model.DispOrder != null)
            {
                strSql.Append(" and DispOrder=" + model.DispOrder + " ");
            }

            if (model.HisOrderCode != null)
            {
                strSql.Append(" and HisOrderCode='" + model.HisOrderCode + "' ");
            }

            if (model.code_1 != null)
            {
                strSql.Append(" and code_1='" + model.code_1 + "' ");
            }
            return idb.ExecuteDataSet(strSql.ToString());
        }
        public DataSet GetListByLike(ZhiFang.Model.Lab_SampleType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ,'('+convert(varchar(100),LabSampleTypeNo)+')'+CName as LabSampleTypeNoAndName ");
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
            return idb.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 获取总记录
        /// </summary>
        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_Lab_SampleType ");
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
        public int GetTotalCount(ZhiFang.Model.Lab_SampleType model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            strSql.Append("select count(*) FROM B_Lab_SampleType where 1=1 ");
            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode = '" + model.LabCode + "'");
            }
            if (model != null)
            {
                if (model.LabSampleTypeNo != null)
                {
                    strWhere.Append(" and ( LabSampleTypeNo like '%" + model.LabSampleTypeNo + "%' ");
                }
                if (model.CName != null)
                {
                    if (strWhere.Length == 0)
                        strWhere.Append(" and ( CName like '%" + model.CName + "%' ");
                    else
                        strWhere.Append(" or CName like '%" + model.CName + "%' ");
                }
                if (model.ShortCode != null)
                {
                    if (strWhere.Length == 0)
                        strWhere.Append(" and ( ShortCode like '%" + model.ShortCode + "%' ");
                    else
                        strWhere.Append(" or ShortCode like '%" + model.ShortCode + "%' ");
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
        public DataSet GetListByPage(ZhiFang.Model.Lab_SampleType model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            if (model.LabSampleTypeNo != null)
            {
                strWhere.Append(" and ( LabSampleTypeNo like '%" + model.LabSampleTypeNo + "%' ");
            }
            if (model.CName != null)
            {
                if (strWhere.Length == 0)
                    strWhere.Append(" and ( CName like '%" + model.CName + "%' ");
                else
                    strWhere.Append(" or CName like '%" + model.CName + "%' ");
            }
            if (model.ShortCode != null)
            {
                if (strWhere.Length == 0)
                    strWhere.Append(" and ( ShortCode like '%" + model.ShortCode + "%' ");
                else
                    strWhere.Append(" or ShortCode like '%" + model.ShortCode + "%' ");
            }
            if (strWhere.Length != 0)
                strWhere.Append(" ) ");

            strSql.Append("select top " + nowPageSize + " * from B_Lab_SampleType where 1=1  ");
            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }
            strSql.Append(" " + strWhere.ToString() + " and SampleTypeID not in ");
            strSql.Append("(select top " + (nowPageSize * nowPageNum) + " SampleTypeID from B_Lab_SampleType where 1=1  ");
            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }
            strSql.Append(" " + strWhere.ToString() + " order by SampleTypeID) order by SampleTypeID  ");
            return idb.ExecuteDataSet(strSql.ToString());
        }

        public bool Exists(string LabCode, int LabSampleTypeNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from B_Lab_SampleType ");
            strSql.Append(" where LabCode=@LabCode and LabSampleTypeNo=@LabSampleTypeNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@LabCode", SqlDbType.VarChar,50),
					new SqlParameter("@LabSampleTypeNo", SqlDbType.Int,4)};
            parameters[0].Value = LabCode;
            parameters[1].Value = LabSampleTypeNo;


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
            return idb.GetMaxID("LabCode,LabSampleTypeNo", "B_Lab_SampleType");
        }

        public DataSet GetList(int Top, ZhiFang.Model.Lab_SampleType model, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM B_Lab_SampleType ");


            if (model.code_2 != null)
            {

                strSql.Append(" and code_2='" + model.code_2 + "' ");
            }

            if (model.code_3 != null)
            {

                strSql.Append(" and code_3='" + model.code_3 + "' ");
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

            if (model.code_1 != null)
            {

                strSql.Append(" and code_1='" + model.code_1 + "' ");
            }

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
                        if (this.Exists(ds.Tables[0].Rows[i]["LabCode"].ToString().Trim(),int.Parse(ds.Tables[0].Rows[i]["LabSampleTypeNo"].ToString().Trim())))
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
                strSql.Append("insert into B_Lab_SampleType (");
                strSql.Append("code_2,code_3,StandCode,ZFStandCode,UseFlag,LabCode,LabSampleTypeNo,CName,ShortCode,Visible,DispOrder,HisOrderCode,code_1");
                strSql.Append(") values (");
                if (dr.Table.Columns["code_2"] != null && dr.Table.Columns["code_2"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["code_2"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["code_3"] != null && dr.Table.Columns["code_3"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["code_3"].ToString().Trim() + "', ");
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
                if (dr.Table.Columns["code_1"] != null && dr.Table.Columns["code_1"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["code_1"].ToString().Trim() + "' ");
                }
                else
                {
                    strSql.Append(" null ");
                }
                strSql.Append(") ");
                return idb.ExecuteNonQuery(strSql.ToString());
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Digitlab8.B_Lab_SampleType.AddByDataRow 同步数据时异常：", ex);
                return 0;
            }
        }
        public int UpdateByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update B_Lab_SampleType set ");

                if (dr.Table.Columns["code_2"] != null && dr.Table.Columns["code_2"].ToString().Trim() != "")
                {
                    strSql.Append(" code_2 = '" + dr["code_2"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["code_3"] != null && dr.Table.Columns["code_3"].ToString().Trim() != "")
                {
                    strSql.Append(" code_3 = '" + dr["code_3"].ToString().Trim() + "' , ");
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

                if (dr.Table.Columns["code_1"] != null && dr.Table.Columns["code_1"].ToString().Trim() != "")
                {
                    strSql.Append(" code_1 = '" + dr["code_1"].ToString().Trim() + "'  ");
                }

                int n = strSql.ToString().LastIndexOf(",");
                strSql.Remove(n, 1);
                strSql.Append(" where LabCode='" + dr["LabCode"].ToString().Trim() + "' and LabSampleTypeNo='" + dr["LabSampleTypeNo"].ToString().Trim() + "'  ");

                return idb.ExecuteNonQuery(strSql.ToString());
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Digitlab8.B_Lab_SampleType .UpdateByDataRow同步数据时异常：", ex);
                return 0;
            }
        }




        public DataSet GetLabCodeNo(string LabCode, List<string> LabCname)
        {
            throw new NotImplementedException();
        }
    }
}

