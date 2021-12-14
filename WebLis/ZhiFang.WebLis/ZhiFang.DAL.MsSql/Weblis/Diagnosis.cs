using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.MsSql.Weblis
{
    //Diagnosis

    public partial class Diagnosis : BaseDALLisDB, IDDiagnosis, IDBatchCopy
    {
        public Diagnosis(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public Diagnosis()
        {
        }
        //D_LogInfo d_log = new D_LogInfo();
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.Diagnosis model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Diagnosis(");
            strSql.Append("DiagNo,CName,DiagDesc,ShortCode,Visible,DispOrder,HisOrderCode");
            strSql.Append(") values (");
            strSql.Append("@DiagNo,@CName,@DiagDesc,@ShortCode,@Visible,@DispOrder,@HisOrderCode");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@DiagNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@CName", SqlDbType.VarChar,40) ,            
                        new SqlParameter("@DiagDesc", SqlDbType.VarChar,250) ,            
                        new SqlParameter("@ShortCode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@Visible", SqlDbType.Int,4) ,            
                        new SqlParameter("@DispOrder", SqlDbType.Int,4) ,            
                        new SqlParameter("@HisOrderCode", SqlDbType.VarChar,20)             
              
            };

            parameters[0].Value = model.DiagNo;
            parameters[1].Value = model.CName;
            parameters[2].Value = model.DiagDesc;
            parameters[3].Value = model.ShortCode;
            parameters[4].Value = model.Visible;
            parameters[5].Value = model.DispOrder;
            parameters[6].Value = model.HisOrderCode; return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);
            //if (DbHelperSQL.ExecuteNonQuery(strSql.ToString(),parameters) > 0)
            //{
            //	  return d_log.OperateLog("Diagnosis", "", "", DateTime.Now, 1);
            //}
            //else
            //    return -1;
        }
        
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.Diagnosis model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Diagnosis set ");

            strSql.Append(" DiagNo = @DiagNo , ");
            strSql.Append(" CName = @CName , ");
            strSql.Append(" DiagDesc = @DiagDesc , ");
            strSql.Append(" ShortCode = @ShortCode , ");
            strSql.Append(" Visible = @Visible , ");
            strSql.Append(" DispOrder = @DispOrder , ");
            strSql.Append(" HisOrderCode = @HisOrderCode  ");
            strSql.Append(" where DiagNo=@DiagNo  ");

            SqlParameter[] parameters = {
			               
            new SqlParameter("@DiagNo", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@CName", SqlDbType.VarChar,40) ,            	
                           
            new SqlParameter("@DiagDesc", SqlDbType.VarChar,250) ,            	
                           
            new SqlParameter("@ShortCode", SqlDbType.VarChar,10) ,            	
                           
            new SqlParameter("@Visible", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@DispOrder", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@HisOrderCode", SqlDbType.VarChar,20)             	
              
            };


            if (model.DiagNo != null)
            {
                parameters[0].Value = model.DiagNo;
            }



            if (model.CName != null)
            {
                parameters[1].Value = model.CName;
            }



            if (model.DiagDesc != null)
            {
                parameters[2].Value = model.DiagDesc;
            }



            if (model.ShortCode != null)
            {
                parameters[3].Value = model.ShortCode;
            }



            if (model.Visible != null)
            {
                parameters[4].Value = model.Visible;
            }



            if (model.DispOrder != null)
            {
                parameters[5].Value = model.DispOrder;
            }



            if (model.HisOrderCode != null)
            {
                parameters[6].Value = model.HisOrderCode;
            }

            return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);
            //if (DbHelperSQL.ExecuteNonQuery(strSql.ToString(),parameters) > 0)
            //{
            //   return d_log.OperateLog("Diagnosis", "", "", DateTime.Now, 1);
            //}
            //else
            //    return -1;
        }
        
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int DiagNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Diagnosis ");
            strSql.Append(" where DiagNo=@DiagNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@DiagNo", SqlDbType.Int,4)};
            parameters[0].Value = DiagNo;


            return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);

        }
        
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.Diagnosis GetModel(int DiagNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DiagNo, CName, DiagDesc, ShortCode, Visible, DispOrder, HisOrderCode  ");
            strSql.Append("  from Diagnosis ");
            strSql.Append(" where DiagNo=@DiagNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@DiagNo", SqlDbType.Int,4)};
            parameters[0].Value = DiagNo;


            ZhiFang.Model.Diagnosis model = new ZhiFang.Model.Diagnosis();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["DiagNo"].ToString() != "")
                {
                    model.DiagNo = int.Parse(ds.Tables[0].Rows[0]["DiagNo"].ToString());
                }
                model.CName = ds.Tables[0].Rows[0]["CName"].ToString();
                model.DiagDesc = ds.Tables[0].Rows[0]["DiagDesc"].ToString();
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
            strSql.Append(" FROM Diagnosis ");
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
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM Diagnosis ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 根据实体获取DataSet
        /// </summary>
        public DataSet GetList(ZhiFang.Model.Diagnosis model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM Diagnosis where 1=1 ");


            if (model.DiagNo != 0)
            {
                strSql.Append(" and DiagNo=" + model.DiagNo + " ");
            }

            if (model.CName != null)
            {
                strSql.Append(" and CName='" + model.CName + "' ");
            }

            if (model.DiagDesc != null)
            {
                strSql.Append(" and DiagDesc='" + model.DiagDesc + "' ");
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
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 获取总记录
        /// </summary>
        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM Diagnosis ");
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
        public int GetTotalCount(ZhiFang.Model.Diagnosis model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM Diagnosis where 1=1 ");

            if (model.DiagNo != null)
            {
                strSql.Append(" and DiagNo=" + model.DiagNo + " ");
            }

            if (model.CName != null)
            {
                strSql.Append(" and CName='" + model.CName + "' ");
            }

            if (model.DiagDesc != null)
            {
                strSql.Append(" and DiagDesc='" + model.DiagDesc + "' ");
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
        public DataSet GetListByPage(ZhiFang.Model.Diagnosis model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            if (model != null && model.LabCode != null)
            {
                strSql.Append(" select top " + nowPageSize + "  * from Diagnosis left join DiagnosisControl on Diagnosis.DiagNo=DiagnosisControl.DiagNo ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and DiagnosisControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append("where DiagNo not in ( ");
                strSql.Append("select top " + (nowPageSize * nowPageNum) + " DiagNo from  Diagnosis left join DiagnosisControl on Diagnosis.DiagNo=DiagnosisControl.DiagNo ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and DiagnosisControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append("order by Diagnosis.DiagNo ) order by Diagnosis.DiagNo ");
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
            else
            {
                strSql.Append("select top " + nowPageSize + "  * from Diagnosis where DiagNo not in  ");
                strSql.Append("(select top " + (nowPageSize * nowPageNum) + " DiagNo from Diagnosis order by DiagNo desc) order by DiagNo desc ");
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
        }

        public bool Exists(int DiagNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Diagnosis ");
            strSql.Append(" where DiagNo ='" + DiagNo + "'");
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

        public bool CopyToLab(List<string> lst)
        {
            System.Collections.ArrayList arrySql = new System.Collections.ArrayList();
            string LabTableName = "Diagnosis";
            LabTableName = "B_Lab_" + LabTableName.Substring(LabTableName.IndexOf("_") + 1, LabTableName.Length - (LabTableName.IndexOf("_") + 1));
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSqlControl = new StringBuilder();
            string TableKey = "DiagNo";
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
                    strSql.Append(" LabDiagNo , CName , DiagDesc , ShortCode , Visible , DispOrder , HisOrderCode ");
                    strSql.Append(") select '" + lst[i].Trim() + "' as LabCode, ");
                    strSql.Append("DiagNo,CName,DiagDesc,ShortCode,Visible,DispOrder,HisOrderCode");
                    strSql.Append(" from Diagnosis ");

                    strSqlControl.Append("insert into DiagnosisControl ( ");
                    strSqlControl.Append(" " + TableKeySub + "ControlNo," + TableKey + ",ControlLabNo,Control" + TableKey + ",UseFlag ");
                    strSqlControl.Append(")  select ");
                    strSqlControl.Append("  '" + lst[i].Trim() + "'+'_'+convert(varchar(50)," + TableKey + ")+'_'+convert(varchar(50), " + TableKey + ") as " + TableKeySub + "ControlNo," + TableKey + ",'" + lst[i].Trim() + "' as ControlLabNo," + TableKey + ",UseFlag ");
                    strSqlControl.Append(" from Diagnosis ");

                    arrySql.Add(strSql.ToString());
                    arrySql.Add(strSqlControl.ToString());

                    strSql = new StringBuilder();
                    strSqlControl = new StringBuilder();

                    //d_log.OperateLog("Diagnosis", "", "", DateTime.Now, 1);
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
            return DbHelperSQL.GetMaxID("DiagNo", "Diagnosis");
        }

        public DataSet GetList(int Top, ZhiFang.Model.Diagnosis model, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM Diagnosis ");


            if (model.DiagNo != null)
            {
                strSql.Append(" and DiagNo=" + model.DiagNo + " ");
            }

            if (model.CName != null)
            {

                strSql.Append(" and CName='" + model.CName + "' ");
            }

            if (model.DiagDesc != null)
            {

                strSql.Append(" and DiagDesc='" + model.DiagDesc + "' ");
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

            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetAllList()
        {
            return GetList("");
        }
        
        #region IDDiagnosis 成员


        public int DeleteList(string DiagIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Diagnosis ");
            strSql.Append(" where DiagID in (" + DiagIDlist + ") ");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
        }

        #endregion

        #region IDBatchCopy 成员

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
                        if (this.Exists(int.Parse(ds.Tables[0].Rows[i]["DiagNo"].ToString().Trim())))
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
                strSql.Append("insert into Diagnosis (");
                strSql.Append("DiagNo,CName,DiagDesc,ShortCode,Visible,DispOrder,HisOrderCode");
                strSql.Append(") values (");
                if (dr.Table.Columns["DiagNo"] != null && dr.Table.Columns["DiagNo"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["DiagNo"].ToString().Trim() + "', ");
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
                if (dr.Table.Columns["DiagDesc"] != null && dr.Table.Columns["DiagDesc"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["DiagDesc"].ToString().Trim() + "', ");
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
                    strSql.Append(" '" + dr["HisOrderCode"].ToString().Trim() + "' ");
                }
                else
                {
                    strSql.Append(" null ");
                }
                strSql.Append(") ");
                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            }
            catch(Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.weblis.Diagnosis.AddByDataRow同步数据时异常：", ex);
                return 0;
            }
        }
        public int UpdateByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update Diagnosis set ");


                if (dr.Table.Columns["CName"] != null && dr.Table.Columns["CName"].ToString().Trim() != "")
                {
                    strSql.Append(" CName = '" + dr["CName"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["DiagDesc"] != null && dr.Table.Columns["DiagDesc"].ToString().Trim() != "")
                {
                    strSql.Append(" DiagDesc = '" + dr["DiagDesc"].ToString().Trim() + "' , ");
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
                    strSql.Append(" HisOrderCode = '" + dr["HisOrderCode"].ToString().Trim() + "'  ");
                }

                strSql.Append(" where DiagNo='" + dr["DiagNo"].ToString().Trim() + "' ");

                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            }
            catch(Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.weblis.Diagnosis.UpdateByDataRow同步数据时异常：", ex);
                return 0;
            }
        }
        public int DeleteByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                if (dr.Table.Columns["DiagNo"] != null && dr.Table.Columns["DiagNo"].ToString().Trim() != "")
                {
                    strSql.Append("delete from Diagnosis ");
                    strSql.Append(" where DiagNo='" + dr["DiagNo"].ToString().Trim() + "' ");
                    return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.weblis.Diagnosis.DeleteByDataRow同步数据时异常：", ex);
                return 0;
            }
        }

        #endregion


        public bool IsExist(string labCodeNo)
        {
            throw new NotImplementedException();
        }

        public bool DeleteByLabCode(string LabCodeNo)
        {
            throw new NotImplementedException();
        }
    }
}

