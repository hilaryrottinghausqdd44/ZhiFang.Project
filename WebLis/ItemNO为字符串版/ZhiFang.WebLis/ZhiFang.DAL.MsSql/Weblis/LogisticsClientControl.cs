using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;
using System.Data;
using ZhiFang.DAL.MsSql;
using System.Data.SqlClient;
using System.Collections;

namespace ZhiFang.DAL.MsSql.Weblis
{
    public partial class BusinessLogicClientControl : BaseDALLisDB, IDBusinessLogicClientControl
    {
         public BusinessLogicClientControl(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
         public BusinessLogicClientControl()
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.BusinessLogicClientControl  model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into BusinessLogicClientControl(");
            strSql.Append("Account,ClientNo");
            strSql.Append(") values (");
            strSql.Append("@Account,@ClientNo");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@Account", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ClientNo", SqlDbType.VarChar,50) ,            
              
            };

            parameters[0].Value = model.Account;
            parameters[1].Value = model.ClientNo;
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.BusinessLogicClientControl  model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BusinessLogicClientControl set ");

            strSql.Append(" Account = @Account , ");
            strSql.Append(" ClientNo = @ClientNo , ");
            strSql.Append(" where Account=@Account and ClientNo=@ClientNo  ");

            SqlParameter[] parameters = {
			            	
                           
            new SqlParameter("@Account", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@ClientNo", SqlDbType.VarChar,50) ,            	
                        	
              
            };




            if (model.Account != null)
            {
                parameters[0].Value = model.Account;
            }



            if (model.ClientNo != null)
            {
                parameters[1].Value = model.ClientNo;
            }




            return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string Account, string ClientNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from BusinessLogicClientControl ");
            strSql.Append(" where Account=@Account and ClientNo=@ClientNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@Account", SqlDbType.VarChar,50),
					new SqlParameter("@ClientNo", SqlDbType.VarChar,50)			};
            parameters[0].Value = Account;
            parameters[1].Value = ClientNo;


            return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);

        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from BusinessLogicClientControl ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());

        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.BusinessLogicClientControl  GetModel(string Account, string ClientNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, Account, ClientNo, AddTime  ");
            strSql.Append("  from BusinessLogicClientControl ");
            strSql.Append(" where Account=@Account and ClientNo=@ClientNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@Account", SqlDbType.VarChar,50),
					new SqlParameter("@ClientNo", SqlDbType.VarChar,50)			};
            parameters[0].Value = Account;
            parameters[1].Value = ClientNo;


            ZhiFang.Model.BusinessLogicClientControl  model = new ZhiFang.Model.BusinessLogicClientControl ();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {

                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }

                model.Account = ds.Tables[0].Rows[0]["Account"].ToString();

                model.ClientNo = ds.Tables[0].Rows[0]["ClientNo"].ToString();

                if (ds.Tables[0].Rows[0]["AddTime"].ToString() != "")
                {
                    model.AddTime = DateTime.Parse(ds.Tables[0].Rows[0]["AddTime"].ToString());
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
            strSql.Append(" FROM BusinessLogicClientControl ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 根据实体获取DataSet
        /// </summary>
        public DataSet GetList(ZhiFang.Model.BusinessLogicClientControl  model)
        {
            StringBuilder strSql = new StringBuilder();
            if (model.SelectedFlag != false)
            {
                strSql.Append("select dbo.BusinessLogicClientControl.Account, '('+Convert(varchar(50),CLIENTELE.ClientNo)+')'+CLIENTELE.CName as ClientNo_CName ,dbo.CLIENTELE.* ");
                strSql.Append(" FROM dbo.BusinessLogicClientControl INNER JOIN  dbo.CLIENTELE ON dbo.BusinessLogicClientControl.ClientNo = dbo.CLIENTELE.ClIENTNO where 1=1 ");
                if (model.Account != null)
                {
                    strSql.Append(" and dbo.BusinessLogicClientControl.Account='" + model.Account + "' ");
                }

                if (model.ClientNo != null)
                {
                    strSql.Append(" and dbo.BusinessLogicClientControl.ClientNo='" + model.ClientNo + "' ");
                }
            }
            else
            {
                strSql.Append("select '('+Convert(varchar(50),CLIENTELE.ClientNo)+')'+CLIENTELE.CName as ClientNo_CName , dbo.CLIENTELE.* ");
                strSql.Append(" FROM CLIENTELE where 1=1 ");
                if (model.Account != null)
                {
                    strSql.Append(" and CLIENTELE.ClientNo not in (select ClientNo from  BusinessLogicClientControl where Account='" + model.Account + "') ");
                }
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
        public DataSet GetListLike(ZhiFang.Model.BusinessLogicClientControl  model)
		{
            throw new NotImplementedException();
		}

        /// <summary>
        /// 获取总记录
        /// </summary>
        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM BusinessLogicClientControl ");
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
        public int GetTotalCount(ZhiFang.Model.BusinessLogicClientControl  model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM BusinessLogicClientControl where 1=1 ");

            if (model.Account != null)
            {
                strSql.Append(" and Account='" + model.Account + "' ");
            }
            if (model.ClientNo != null)
            {
                strSql.Append(" and ClientNo='" + model.ClientNo + "' ");
            }

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
        public DataSet GetListByPage(ZhiFang.Model.BusinessLogicClientControl  model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            if (model != null )
            {
                string sql = "";
                if (model.Account != null && model.Account.Trim()!="")
                {
                    sql += " and  BusinessLogicClientControl.Account='" + model.Account.Trim() + "'";
                }
                if (model.ClientNo != null && model.ClientNo != "")
                {
                    sql += " and  BusinessLogicClientControl.ClientNo='" + model.ClientNo.Trim() + "'";
                }
                strSql.Append("select top " + nowPageSize + "  dbo.BusinessLogicClientControl.Account, dbo.CLIENTELE.* from  dbo.BusinessLogicClientControl INNER JOIN  dbo.CLIENTELE ON dbo.BusinessLogicClientControl.ClientNo = dbo.CLIENTELE.ClIENTNO where BusinessLogicClientControl.Account+'_'+BusinessLogicClientControl.ClientNo not in  ");
                strSql.Append("(select top " + (nowPageSize * nowPageNum - nowPageSize) + " BusinessLogicClientControl.Account+'_'+BusinessLogicClientControl.ClientNo from  dbo.BusinessLogicClientControl INNER JOIN   dbo.CLIENTELE ON dbo.BusinessLogicClientControl.ClientNo = dbo.CLIENTELE.ClIENTNO where 1=2 " + sql + "  order by BusinessLogicClientControl.Account,BusinessLogicClientControl.ClientNo)");
                strSql.Append(sql);
                strSql.Append(" order by BusinessLogicClientControl.Account,BusinessLogicClientControl.ClientNo  ");
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
            else
            {
                strSql.Append("select top " + nowPageSize + "  dbo.BusinessLogicClientControl.Account, dbo.CLIENTELE.*  from dbo.BusinessLogicClientControl INNER JOIN  dbo.CLIENTELE ON dbo.BusinessLogicClientControl.ClientNo = dbo.CLIENTELE.ClIENTNO where BusinessLogicClientControl.Account+'_'+BusinessLogicClientControl.ClientNo not in  ");
                strSql.Append("(select top " + (nowPageSize * nowPageNum - nowPageSize) + "BusinessLogicClientControl.Account+'_'+BusinessLogicClientControl.ClientNo from dbo.BusinessLogicClientControl INNER JOIN   dbo.CLIENTELE ON dbo.BusinessLogicClientControl.ClientNo = dbo.CLIENTELE.ClIENTNO order by BusinessLogicClientControl.Account,BusinessLogicClientControl.ClientNo) order by BusinessLogicClientControl.Account,BusinessLogicClientControl.ClientNo  ");
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
        }        

        public bool CopyToLab(List<string> lst)
        {
            System.Collections.ArrayList arrySql = new System.Collections.ArrayList();
            string LabTableName = "BusinessLogicClientControl";
            LabTableName = "B_Lab_" + LabTableName.Substring(LabTableName.IndexOf("_") + 1, LabTableName.Length - (LabTableName.IndexOf("_") + 1));
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSqlControl = new StringBuilder();
            string TableKey = "Account,ClientNo";
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
                    strSql.Append(" LabAccount , LabClientNo ,");
                    strSql.Append(") select '" + lst[i].Trim() + "' as LabCode, ");
                    strSql.Append("Account,ClientNo,");
                    strSql.Append(" from BusinessLogicClientControl ");

                    strSqlControl.Append("insert into BusinessLogicClientControlControl ( ");
                    strSqlControl.Append(" " + TableKeySub + "ControlNo," + TableKey + ",ControlLabNo,Control" + TableKey + ",UseFlag ");
                    strSqlControl.Append(")  select ");
                    strSqlControl.Append("  '" + lst[i].Trim() + "'+'_'+convert(varchar(50)," + TableKey + ")+'_'+convert(varchar(50), " + TableKey + ") as " + TableKeySub + "ControlNo," + TableKey + ",'" + lst[i].Trim() + "' as ControlLabNo," + TableKey + ",UseFlag ");
                    strSqlControl.Append(" from BusinessLogicClientControl ");

                    arrySql.Add(strSql.ToString());
                    arrySql.Add(strSqlControl.ToString());

                    strSql = new StringBuilder();
                    strSqlControl = new StringBuilder();
                }

                DbHelperSQL.BatchUpdateWithTransaction(arrySql);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("Account,ClientNo", "BusinessLogicClientControl");
        }

        public DataSet GetList(int Top, ZhiFang.Model.BusinessLogicClientControl  model, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM BusinessLogicClientControl ");



            if (model.Account != null)
            {
                strSql.Append(" and Account='" + model.Account + "' ");
            }


            if (model.ClientNo != null)
            {
                strSql.Append(" and ClientNo='" + model.ClientNo + "' ");
            }

            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetAllList()
        {
            return GetList("");
        }

        #region IDBusinessLogicClientControl 成员

        public bool Exists(string Account, string ClientNo)
        {
            throw new NotImplementedException();
        }

        public bool Add(List<Model.BusinessLogicClientControl> l)
        {
            try
            {
                ArrayList al = new ArrayList();
                al.Add(" delete from BusinessLogicClientControl where Account='" + l.ElementAt(0).Account + "' ");
                if (l != null && l.Count > 0)
                {
                    foreach (Model.BusinessLogicClientControl lcc_m in l)
                    {
                        if (lcc_m.Account != null && lcc_m.Account.Trim() != "" && lcc_m.ClientNo != null && lcc_m.ClientNo.Trim() != "")
                        {
                            StringBuilder strSql = new StringBuilder();
                            strSql.Append("insert into BusinessLogicClientControl(");
                            strSql.Append("Account,ClientNo");
                            strSql.Append(") values (");
                            strSql.Append("'" + lcc_m.Account + "','" + lcc_m.ClientNo + "' ");
                            strSql.Append(") ");
                            al.Add(strSql.ToString());
                        }
                    }
                    DbHelperSQL.BatchUpdateWithTransaction(al);
                    return true;
                }
                else
                {
                    DbHelperSQL.BatchUpdateWithTransaction(al);
                    return true;
                }
            }
            catch (Exception e)
            {
                Common.Log.Log.Error(e.ToString());
                return false;
            }
        }

        #endregion

        #region IDataBase<BusinessLogicClientControl> 成员


        public int AddUpdateByDataSet(DataSet ds)
        {
            throw new NotImplementedException();
        }

        #endregion

        
    }
}
