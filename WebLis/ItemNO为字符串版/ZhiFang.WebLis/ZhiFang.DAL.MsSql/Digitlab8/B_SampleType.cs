using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.MsSql.Digitlab8
{
    //B_SampleType

    public partial class B_SampleType : IDSampleType, IDBatchCopy, IDGetListByTimeStampe
    {
        DBUtility.IDBConnection idb;
        public B_SampleType(string dbsourceconn)
        {
            idb = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public B_SampleType()
        {
            idb = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.LisDB());
        }
        D_LogInfo d_log = new D_LogInfo();
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.SampleType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into B_SampleType(");
            strSql.Append("code_3,StandCode,ZFStandCode,UseFlag,SampleTypeNo,CName,ShortCode,Visible,DispOrder,HisOrderCode,code_1,code_2");
            strSql.Append(") values (");
            strSql.Append("@code_3,@StandCode,@ZFStandCode,@UseFlag,@SampleTypeNo,@CName,@ShortCode,@Visible,@DispOrder,@HisOrderCode,@code_1,@code_2");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@code_3", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@StandCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ZFStandCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@UseFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@SampleTypeNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@CName", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ShortCode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@Visible", SqlDbType.Int,4) ,            
                        new SqlParameter("@DispOrder", SqlDbType.Int,4) ,            
                        new SqlParameter("@HisOrderCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@code_1", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@code_2", SqlDbType.NVarChar,50)             
              
            };

            parameters[0].Value = model.code_3;
            parameters[1].Value = model.StandCode;
            parameters[2].Value = model.ZFStandCode;
            parameters[3].Value = model.UseFlag;
            parameters[4].Value = model.SampleTypeNo;
            parameters[5].Value = model.CName;
            parameters[6].Value = model.ShortCode;
            parameters[7].Value = model.Visible;
            parameters[8].Value = model.DispOrder;
            parameters[9].Value = model.HisOrderCode;
            parameters[10].Value = model.code_1;
            parameters[11].Value = model.code_2;
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
        public int Update(ZhiFang.Model.SampleType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update B_SampleType set ");

            strSql.Append(" code_3 = @code_3 , ");
            strSql.Append(" StandCode = @StandCode , ");
            strSql.Append(" ZFStandCode = @ZFStandCode , ");
            strSql.Append(" UseFlag = @UseFlag , ");
            strSql.Append(" SampleTypeNo = @SampleTypeNo , ");
            strSql.Append(" CName = @CName , ");
            strSql.Append(" ShortCode = @ShortCode , ");
            strSql.Append(" Visible = @Visible , ");
            strSql.Append(" DispOrder = @DispOrder , ");
            strSql.Append(" HisOrderCode = @HisOrderCode , ");
            strSql.Append(" code_1 = @code_1 , ");
            strSql.Append(" code_2 = @code_2  ");
            strSql.Append(" where SampleTypeNo=@SampleTypeNo  ");

            SqlParameter[] parameters = {
			            	
                           
            new SqlParameter("@code_3", SqlDbType.NVarChar,50) ,            	
                        	
                        	
                           
            new SqlParameter("@StandCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@ZFStandCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@UseFlag", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@SampleTypeNo", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@CName", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@ShortCode", SqlDbType.VarChar,10) ,            	
                           
            new SqlParameter("@Visible", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@DispOrder", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@HisOrderCode", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@code_1", SqlDbType.NVarChar,50) ,            	
                           
            new SqlParameter("@code_2", SqlDbType.NVarChar,50)             	
              
            };




            if (model.code_3 != null)
            {
                parameters[0].Value = model.code_3;
            }







            if (model.StandCode != null)
            {
                parameters[1].Value = model.StandCode;
            }



            if (model.ZFStandCode != null)
            {
                parameters[2].Value = model.ZFStandCode;
            }



            if (model.UseFlag != null)
            {
                parameters[3].Value = model.UseFlag;
            }



            if (model.SampleTypeNo != null)
            {
                parameters[4].Value = model.SampleTypeNo;
            }



            if (model.CName != null)
            {
                parameters[5].Value = model.CName;
            }



            if (model.ShortCode != null)
            {
                parameters[6].Value = model.ShortCode;
            }



            if (model.Visible != null)
            {
                parameters[7].Value = model.Visible;
            }



            if (model.DispOrder != null)
            {
                parameters[8].Value = model.DispOrder;
            }



            if (model.HisOrderCode != null)
            {
                parameters[9].Value = model.HisOrderCode;
            }



            if (model.code_1 != null)
            {
                parameters[10].Value = model.code_1;
            }



            if (model.code_2 != null)
            {
                parameters[11].Value = model.code_2;
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
        public int Delete(int SampleTypeNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from B_SampleType ");
            strSql.Append(" where SampleTypeNo=@SampleTypeNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@SampleTypeNo", SqlDbType.Int,4)};
            parameters[0].Value = SampleTypeNo;


            return idb.ExecuteNonQuery(strSql.ToString(), parameters);

        }

        public int DeleteList(string IDList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from B_SampleType ");
            strSql.Append(" where SampleTypeNo in ("+IDList+") ");
            return idb.ExecuteNonQuery(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.SampleType GetModel(int SampleTypeNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SampleTypeID, code_3, DTimeStampe, AddTime, StandCode, ZFStandCode, UseFlag, SampleTypeNo, CName, ShortCode, Visible, DispOrder, HisOrderCode, code_1, code_2  ");
            strSql.Append("  from B_SampleType ");
            strSql.Append(" where SampleTypeNo=@SampleTypeNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@SampleTypeNo", SqlDbType.Int,4)};
            parameters[0].Value = SampleTypeNo;


            ZhiFang.Model.SampleType model = new ZhiFang.Model.SampleType();
            DataSet ds = idb.ExecuteDataSet(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["SampleTypeID"].ToString() != "")
                {
                    model.SampleTypeID = int.Parse(ds.Tables[0].Rows[0]["SampleTypeID"].ToString());
                }
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
                if (ds.Tables[0].Rows[0]["SampleTypeNo"].ToString() != "")
                {
                    model.SampleTypeNo = int.Parse(ds.Tables[0].Rows[0]["SampleTypeNo"].ToString());
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
                model.code_2 = ds.Tables[0].Rows[0]["code_2"].ToString();

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
            strSql.Append(" FROM B_SampleType ");
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
            strSql.Append(" FROM B_SampleType ");
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
        public DataSet GetList(ZhiFang.Model.SampleType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM B_SampleType where 1=1 ");

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


            if (model.SampleTypeNo != 0)
            {
                strSql.Append(" and SampleTypeNo=" + model.SampleTypeNo + " ");
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

            if (model.code_2 != null)
            {
                strSql.Append(" and code_2='" + model.code_2 + "' ");
            }
            return idb.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 获取总记录
        /// </summary>
        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_SampleType ");
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
        public int GetTotalCount(ZhiFang.Model.SampleType model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            strSql.Append("select count(*) FROM B_SampleType where 1=1 ");
            if (model != null)
            {        
                if (model.SampleTypeNo != -1)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or SampleTypeNo like '%" + model.SampleTypeNo + "%' ");
                    else
                        strWhere.Append(" and ( SampleTypeNo like '%" + model.SampleTypeNo + "%' ");
                }
                if (model.CName != null)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or CName like '%" + model.CName + "%' ");
                    else
                        strWhere.Append(" and ( CName like '%" + model.CName + "%' ");
                }
                if (model.ShortCode != null)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or ShortCode like '%" + model.ShortCode + "%' ");
                    else
                        strWhere.Append(" and ( ShortCode like '%" + model.ShortCode + "%' ");
                }
                if (strWhere.Length > 0)
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
        public DataSet GetListByPage(ZhiFang.Model.SampleType model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            if (model != null)
            {
                if (model.SampleTypeNo != -1)
                {
                    strWhere.Append(" and ( B_SampleType.SampleTypeNo like '%" + model.SampleTypeNo + "%'  ");
                }
                if (model.CName != null)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or B_SampleType.CName like '%" + model.CName + "%'  ");
                    else
                        strWhere.Append(" and ( B_SampleType.CName like '%" + model.CName + "%'  ");
                }
                if (model.ShortCode != null)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or B_SampleType.ShortCode like '%" + model.ShortCode + "%'  ");
                    else
                        strWhere.Append(" and ( B_SampleType.ShortCode like '%" + model.ShortCode + "%'  ");
                }
                if (strWhere.Length > 0)
                    strWhere.Append(" ) ");
            }
            if (model != null && model.LabCode != null)
            {
                strSql.Append(" select top " + nowPageSize + "  * from B_SampleType left join B_SampleTypeControl on B_SampleType.SampleTypeNo=B_SampleTypeControl.SampleTypeNo ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and B_SampleTypeControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append("where SampleTypeID not in ( ");
                strSql.Append("select top " + (nowPageSize * nowPageNum) + " SampleTypeID from  B_SampleType left join B_SampleTypeControl on B_SampleType.SampleTypeNo=B_SampleTypeControl.SampleTypeNo ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and B_SampleTypeControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append(" " + strWhere.ToString() + " order by B_SampleType.SampleTypeID ) " + strWhere.ToString() + " order by B_SampleType.SampleTypeID ");
                return idb.ExecuteDataSet(strSql.ToString());
            }
            else
            {
                strSql.Append("select top " + nowPageSize + "  * from B_SampleType where SampleTypeID not in  ");
                strSql.Append("(select top " + (nowPageSize * nowPageNum) + " SampleTypeID from B_SampleType where 1=1 " + strWhere.ToString() + " order by " + model.OrderField + ") " + strWhere.ToString() + " order by " + model.OrderField + "  ");
                return idb.ExecuteDataSet(strSql.ToString());
            }
        }

        public bool Exists(int SampleTypeNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from B_SampleType ");
            strSql.Append(" where SampleTypeNo ='" + SampleTypeNo + "'");
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

        public bool Exists(System.Collections.Hashtable ht)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from B_SampleType where 1=1 ");
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

        public bool CopyToLab(List<string> lst)
        {
            System.Collections.ArrayList arrySql = new System.Collections.ArrayList();
            string LabTableName = "B_SampleType";
            LabTableName = "B_Lab_" + LabTableName.Substring(LabTableName.IndexOf("_") + 1, LabTableName.Length - (LabTableName.IndexOf("_") + 1));
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSqlControl = new StringBuilder();
            string TableKey = "SampleTypeNo";
            string TableKeySub = TableKey;
            if (TableKey.ToLower().Contains("no"))
            {
                TableKeySub = TableKey.Substring(0, TableKey.ToLower().IndexOf("no"));
            }
            try
            {
                for (int i = 0; i < lst.Count; i++)
                {
                    strSql.Append("insert into " + LabTableName + "( LabCode,");
                    strSql.Append(" code_3 , StandCode , ZFStandCode , UseFlag , LabSampleTypeNo , CName , ShortCode , Visible , DispOrder , HisOrderCode , code_1 , code_2 ");
                    strSql.Append(") select '" + lst[i].Trim() + "' as LabCode, ");
                    strSql.Append("code_3,StandCode,ZFStandCode,UseFlag,SampleTypeNo,CName,ShortCode,Visible,DispOrder,HisOrderCode,code_1,code_2");
                    strSql.Append(" from B_SampleType ");

                    strSqlControl.Append("insert into B_SampleTypeControl ( ");
                    strSqlControl.Append(" " + TableKeySub + "ControlNo," + TableKey + ",ControlLabNo,Control" + TableKey + ",UseFlag ");
                    strSqlControl.Append(")  select ");
                    strSqlControl.Append("  '" + lst[i].Trim() + "'+'_'+convert(varchar(50)," + TableKey + ")+'_'+convert(varchar(50), " + TableKey + ") as " + TableKeySub + "ControlNo," + TableKey + ",'" + lst[i].Trim() + "' as ControlLabNo," + TableKey + ",UseFlag ");
                    strSqlControl.Append(" from B_SampleType ");

                    arrySql.Add(strSql.ToString());
                    arrySql.Add(strSqlControl.ToString());

                    strSql = new StringBuilder();
                    strSqlControl = new StringBuilder();

                }

                idb.BatchUpdateWithTransaction(arrySql);
                d_log.OperateLog("SampleType", "", "", DateTime.Now, 1);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public int GetMaxId()
        {
            return idb.GetMaxID("SampleTypeNo", "B_SampleType");
        }

        public DataSet GetList(int Top, ZhiFang.Model.SampleType model, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM B_SampleType ");


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

            if (model.SampleTypeNo != null)
            {
                strSql.Append(" and SampleTypeNo=" + model.SampleTypeNo + " ");
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

            if (model.code_2 != null)
            {

                strSql.Append(" and code_2='" + model.code_2 + "' ");
            }

            strSql.Append(" order by " + filedOrder);
            return idb.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetAllList()
        {
            return GetList("");
        }

        #region IDGetListByTimeStampe 成员

        public DataSet GetListByTimeStampe(string LabCode, DateTime dTimeStampe)
        {
            DataSet dsAll = new DataSet();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *,'" + LabCode + "' as LabCode,SampleTypeNo as LabSampleTypeNo from B_SampleType where 1=1 ");
            if (dTimeStampe != null && dTimeStampe != DateTime.Parse("0001-1-1 0:00:00"))
            {
                strSql.Append(" and convert(datetime,DTimeStampe) > '" + dTimeStampe + "' ");
            }
            DataTable dtServer = idb.ExecuteDataSet(strSql.ToString()).Tables[0];
            dtServer.TableName = "ServerDatas";

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("select *,LabSampleTypeNo as SampleTypeNo from B_Lab_SampleType where 1=1 ");
            if (LabCode.Trim() != "")
            {
                strSql2.Append(" and LabCode= '" + LabCode + "' ");
            }
            if (dTimeStampe != null && dTimeStampe != DateTime.Parse("0001-1-1 0:00:00"))
            {
                strSql2.Append(" and convert(datetime,DTimeStampe) > '" + dTimeStampe + "' ");
            }
            DataTable dtLab = idb.ExecuteDataSet(strSql2.ToString()).Tables[0];
            dtLab.TableName = "LabDatas";

            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("select * from B_SampleTypeControl where 1=1 ");
            if (LabCode.Trim() != "")
            {
                strSql3.Append(" and ControlLabNo= '" + LabCode + "' ");
            }
            if (dTimeStampe != null && dTimeStampe != DateTime.Parse("0001-1-1 0:00:00"))
            {
                strSql3.Append(" and convert(datetime,DTimeStampe) > '" + dTimeStampe + "' ");
            }
            DataTable dtControl = idb.ExecuteDataSet(strSql3.ToString()).Tables[0];
            dtControl.TableName = "ControlDatas";

            dsAll.Tables.Add(dtServer.Copy());
            dsAll.Tables.Add(dtLab.Copy());
            dsAll.Tables.Add(dtControl.Copy());
            return dsAll;
        }

        #endregion

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
                        if (this.Exists(int.Parse(ds.Tables[0].Rows[i]["SampleTypeNo"].ToString().Trim())))
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
                strSql.Append("insert into B_SampleType (");
                strSql.Append("code_3,StandCode,ZFStandCode,UseFlag,SampleTypeNo,CName,ShortCode,Visible,DispOrder,HisOrderCode,code_1,code_2");
                strSql.Append(") values (");
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
                if (dr.Table.Columns["SampleTypeNo"] != null && dr.Table.Columns["SampleTypeNo"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["SampleTypeNo"].ToString().Trim() + "', ");
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
                    strSql.Append(" '" + dr["code_1"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["code_2"] != null && dr.Table.Columns["code_2"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["code_2"].ToString().Trim() + "' ");
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
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Digitlab8.B_SampleType.AddByDataRow 同步数据时异常：", ex);
                return 0;
            }
        }
        public int UpdateByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update B_SampleType set ");

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
                    strSql.Append(" code_1 = '" + dr["code_1"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["code_2"] != null && dr.Table.Columns["code_2"].ToString().Trim() != "")
                {
                    strSql.Append(" code_2 = '" + dr["code_2"].ToString().Trim() + "'  ");
                }

                int n = strSql.ToString().LastIndexOf(",");
                strSql.Remove(n, 1);
                strSql.Append(" where SampleTypeNo='" + dr["SampleTypeNo"].ToString().Trim() + "' ");

                return idb.ExecuteNonQuery(strSql.ToString());
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Digitlab8.B_SampleType .UpdateByDataRow同步数据时异常：", ex);
                return 0;
            }
        }
        public int DeleteByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                if (dr.Table.Columns["SampleTypeNo"] != null && dr.Table.Columns["SampleTypeNo"].ToString().Trim() != "")
                {
                    strSql.Append("delete from B_SampleType ");
                    strSql.Append(" where SampleTypeNo='" + dr["SampleTypeNo"].ToString().Trim() + "' ");
                    return idb.ExecuteNonQuery(strSql.ToString());
                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.digitlab8.SampleType.DeleteByDataRow同步数据时异常：", ex);
                return 0;
            }
        }

        #region IDGetListByTimeStampe 成员

        public DataSet GetListByTimeStampe(string LabCode, int dTimeStampe)
        {
            DataSet dsAll = new DataSet();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *,'" + LabCode + "' as LabCode,SampleTypeNo as LabSampleTypeNo from B_SampleType where 1=1 ");
            if (dTimeStampe != -999999)
            {
                strSql.Append(" and convert(int,DTimeStampe) > '" + dTimeStampe + "' ");
            }
            DataTable dtServer = idb.ExecuteDataSet(strSql.ToString()).Tables[0];
            dtServer.TableName = "ServerDatas";

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("select *,LabSampleTypeNo as SampleTypeNo from B_Lab_SampleType where 1=1 ");
            if (LabCode.Trim() != "")
            {
                strSql2.Append(" and LabCode= '" + LabCode + "' ");
            }
            if (dTimeStampe != -999999)
            {
                strSql2.Append(" and convert(int,DTimeStampe) > '" + dTimeStampe + "' ");
            }
            DataTable dtLab = idb.ExecuteDataSet(strSql2.ToString()).Tables[0];
            dtLab.TableName = "LabDatas";

            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("select * from B_SampleTypeControl where 1=1 ");
            if (LabCode.Trim() != "")
            {
                strSql3.Append(" and ControlLabNo= '" + LabCode + "' ");
            }
            if (dTimeStampe != -999999)
            {
                strSql3.Append(" and convert(int,DTimeStampe) > '" + dTimeStampe + "' ");
            }
            DataTable dtControl = idb.ExecuteDataSet(strSql3.ToString()).Tables[0];
            dtControl.TableName = "ControlDatas";

            dsAll.Tables.Add(dtServer.Copy());
            dsAll.Tables.Add(dtLab.Copy());
            dsAll.Tables.Add(dtControl.Copy());
            return dsAll;
        }

        #endregion


        public DataSet GetSampleTypeByColorName(string colorName)
        {
            throw new NotImplementedException();
        }


        public bool IsExist(string labCodeNo)
        {
            throw new NotImplementedException();
        }

        public bool DeleteByLabCode(string LabCodeNo)
        {
            throw new NotImplementedException();
        }


        public int Add(List<Model.SampleType> modelList)
        {
            throw new NotImplementedException();
        }


        public int Update(List<Model.SampleType> modelList)
        {
            throw new NotImplementedException();
        }
    }
}

