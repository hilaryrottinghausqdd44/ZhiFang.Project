using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.MsSql.Weblis
{
    //SampleType

    public partial class SampleType : BaseDALLisDB, IDSampleType, IDBatchCopy, IDGetListByTimeStampe
    {
        public SampleType(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public SampleType()
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
        }
        D_LogInfo d_log = new D_LogInfo();
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.SampleType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SampleType(");
            strSql.Append("SampleTypeNo,CName,ShortCode,Visible,DispOrder,HisOrderCode");
            strSql.Append(") values (");
            strSql.Append("@SampleTypeNo,@CName,@ShortCode,@Visible,@DispOrder,@HisOrderCode");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@SampleTypeNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@CName", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ShortCode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@Visible", SqlDbType.Int,4) ,            
                        new SqlParameter("@DispOrder", SqlDbType.Int,4) ,            
                        new SqlParameter("@HisOrderCode", SqlDbType.VarChar,20)             
              
            };

            parameters[0].Value = model.SampleTypeNo;
            parameters[1].Value = model.CName;
            parameters[2].Value = model.ShortCode;
            parameters[3].Value = model.Visible;
            parameters[4].Value = model.DispOrder;
            parameters[5].Value = model.HisOrderCode;
            if (DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters) > 0)
            {
                return d_log.OperateLog("SampleType", "", "", DateTime.Now, 1);
            }
            else
                return -1;
        }


        public int Add(List<ZhiFang.Model.SampleType> modelList)
        {
            string strsqls = "";
            foreach (var model in modelList)
            {
                StringBuilder strSql = new StringBuilder();
                StringBuilder strSql1 = new StringBuilder();
                StringBuilder strSql2 = new StringBuilder();
                if (model.SampleTypeNo != null)
                {
                    strSql1.Append("SampleTypeNo,");
                    strSql2.Append("'" + model.SampleTypeNo + "',");
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
                    strSql2.Append("'" + model.Visible + "',");
                }
                if (model.DispOrder != null)
                {
                    strSql1.Append("DispOrder,");
                    strSql2.Append("'" + model.DispOrder + "',");
                }
                if (model.HisOrderCode != null)
                {
                    strSql1.Append("HisOrderCode,");
                    strSql2.Append("'" + model.HisOrderCode + "',");
                }

                strSql.Append("insert into SampleType(");
                strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
                strSql.Append(")");
                strSql.Append(" values (");
                strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
                strSql.Append(")");

                if (strsqls == "")
                {
                    strsqls = strSql.ToString();
                }
                else
                {
                    strsqls += ";" + strSql.ToString();
                }
            }
            return DbHelperSQL.ExecuteNonQuery(strsqls);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.SampleType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SampleType set ");

            strSql.Append(" SampleTypeNo = @SampleTypeNo , ");
            strSql.Append(" CName = @CName , ");
            strSql.Append(" ShortCode = @ShortCode , ");
            strSql.Append(" Visible = @Visible , ");
            strSql.Append(" DispOrder = @DispOrder , ");
            strSql.Append(" HisOrderCode = @HisOrderCode  ");
            strSql.Append(" where SampleTypeNo=@SampleTypeNo  ");

            SqlParameter[] parameters = {
			               
            new SqlParameter("@SampleTypeNo", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@CName", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@ShortCode", SqlDbType.VarChar,10) ,            	
                           
            new SqlParameter("@Visible", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@DispOrder", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@HisOrderCode", SqlDbType.VarChar,20)             	
              
            };


            if (model.SampleTypeNo != null)
            {
                parameters[0].Value = model.SampleTypeNo;
            }



            if (model.CName != null)
            {
                parameters[1].Value = model.CName;
            }



            if (model.ShortCode != null)
            {
                parameters[2].Value = model.ShortCode;
            }



            if (model.Visible != null)
            {
                parameters[3].Value = model.Visible;
            }



            if (model.DispOrder != null)
            {
                parameters[4].Value = model.DispOrder;
            }



            if (model.HisOrderCode != null)
            {
                parameters[5].Value = model.HisOrderCode;
            }


            if (DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters) > 0)
            {
                return d_log.OperateLog("SampleType", "", "", DateTime.Now, 1);
            }
            else
                return -1;
        }


        public int Update(List<ZhiFang.Model.SampleType> modelList)
        {
            string strsqls = "";
            foreach (var model in modelList)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update SampleType set ");


                if (model.SampleTypeNo != null)
                {
                    strSql.Append("SampleTypeNo='" + model.SampleTypeNo + "',");
                }

                if (model.CName != null)
                {
                    strSql.Append("CName='" + model.CName + "',");
                }
                if (model.ShortCode != null)
                {
                    strSql.Append("ShortCode='" + model.ShortCode + "',");
                }
                if (model.DispOrder != null)
                {
                    strSql.Append("DispOrder='" + model.DispOrder + "',");
                }
                if (model.HisOrderCode != null)
                {
                    strSql.Append("HisOrderCode='" + model.HisOrderCode + "',");
                }

                int n = strSql.ToString().LastIndexOf(",");
                strSql.Remove(n, 1);
                strSql.Append(" where SampleTypeNo=" + model.SampleTypeNo + " ");

                if (strsqls == "")
                {
                    strsqls = strSql.ToString();
                }
                else
                {
                    strsqls += ";" + strSql.ToString();
                }
            }
            return DbHelperSQL.ExecuteNonQuery(strsqls);

        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int SampleTypeNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SampleType ");
            strSql.Append(" where SampleTypeNo=@SampleTypeNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@SampleTypeNo", SqlDbType.Int,4)};
            parameters[0].Value = SampleTypeNo;


            return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);

        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SampleType ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());

        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.SampleType GetModel(int SampleTypeNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SampleTypeNo, CName, ShortCode, Visible, DispOrder, HisOrderCode  ");
            strSql.Append("  from SampleType ");
            strSql.Append(" where SampleTypeNo=@SampleTypeNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@SampleTypeNo", SqlDbType.Int,4)};
            parameters[0].Value = SampleTypeNo;


            ZhiFang.Model.SampleType model = new ZhiFang.Model.SampleType();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
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
            strSql.Append(" FROM SampleType ");
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
            strSql.Append(" FROM SampleType ");
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
        public DataSet GetList(ZhiFang.Model.SampleType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SampleType where 1=1 ");


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
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 获取总记录
        /// </summary>
        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM SampleType ");
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
        public int GetTotalCount(ZhiFang.Model.SampleType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM SampleType where 1=1 ");
            string likesql = "";
            if (model.SearchLikeKey != null && model.SearchLikeKey.Trim() != "")
            {
                likesql = " and  (SampleTypeNo like '%" + model.SearchLikeKey + "%' or CNAME like '%" + model.SearchLikeKey + "%'  or HisOrderCode like '%" + model.SearchLikeKey + "%'  or SHORTCODE like '%" + model.SearchLikeKey + "%') ";
            }
            strSql.Append(likesql);
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
        public DataSet GetListByPage(ZhiFang.Model.SampleType model, int nowPageNum, int nowPageSize)
        {
            string likesql = "";
            StringBuilder strSql = new StringBuilder();
            if (model != null && model.LabCode != null)
            {

                if (model.SearchLikeKey != null && model.SearchLikeKey.Trim() != "")
                {
                    likesql = " and  (SampleType.SampleTypeNo like '%" + model.SearchLikeKey + "%' or CNAME like '%" + model.SearchLikeKey + "%'  or HisOrderCode like '%" + model.SearchLikeKey + "%'  or SHORTCODE like '%" + model.SearchLikeKey + "%') ";
                }
                string strOrderBy = "";
                if (model.OrderField == "SampleTypeID")
                {
                    strOrderBy = "SampleType.SampleTypeNo";
                }
                else if (model.OrderField.ToLower().IndexOf("control") >= 0)
                {
                    strOrderBy = "B_SampleTypeControl." + model.OrderField;
                }
                else
                {
                    strOrderBy = "SampleType." + model.OrderField;
                }
                strSql.Append(" select top " + nowPageSize + "  * from SampleType left join B_SampleTypeControl on SampleType.SampleTypeNo=B_SampleTypeControl.SampleTypeNo ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and B_SampleTypeControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append("where 1=1 " + likesql + " and SampleType.SampleTypeNo not in ( ");
                strSql.Append("select top " + (nowPageSize * nowPageNum) + " SampleType.SampleTypeNo from  SampleType left join B_SampleTypeControl on SampleType.SampleTypeNo=B_SampleTypeControl.SampleTypeNo ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and B_SampleTypeControl.ControlLabNo='" + model.LabCode + "'");
                }
                strSql.Append(" where 1=1 " + likesql + " order by " + strOrderBy + " desc ) order by " + strOrderBy + " desc ");
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
            else
            {

                if (model.SearchLikeKey != null)
                {
                    likesql = " and  (SampleTypeNo like '%" + model.SearchLikeKey + "%' or CNAME like '%" + model.SearchLikeKey + "%'  or HisOrderCode like '%" + model.SearchLikeKey + "%'  or SHORTCODE like '%" + model.SearchLikeKey + "%') ";
                }
                strSql.Append("select top " + nowPageSize + "  * from SampleType where SampleTypeNo not in  ");
                strSql.Append("(select top " + (nowPageSize * nowPageNum) + " SampleTypeNo from SampleType  where 1=1 " + likesql + "  order by " + model.OrderField + " desc ) ");
                strSql.Append(likesql);
                strSql.Append(" order by " + model.OrderField + " desc ");
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
        }

        public bool Exists(int SampleTypeNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SampleType ");
            strSql.Append(" where SampleTypeNo ='" + SampleTypeNo + "'");
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

        public bool Exists(System.Collections.Hashtable ht)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SampleType where 1=1 ");
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

        public bool CopyToLab(List<string> lst)
        {
            System.Collections.ArrayList arrySql = new System.Collections.ArrayList();
            string LabTableName = "SampleType";
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
                    string str = GetControlItems(lst[i].Trim());

                    strSql.Append("insert into " + LabTableName + "( LabCode,");
                    strSql.Append(" LabSampleTypeNo , CName , ShortCode , Visible , DispOrder , HisOrderCode ");
                    strSql.Append(") select '" + lst[i].Trim() + "' as LabCode, ");
                    strSql.Append("SampleTypeNo,CName,ShortCode,Visible,DispOrder,HisOrderCode");
                    strSql.Append(" from SampleType ");
                    if (str.Trim() != "")
                        strSql.Append(" where SampleTypeNo not in (" + str + ")");

                    strSqlControl.Append("insert into B_SampleTypeControl ( ");
                    strSqlControl.Append(" " + TableKeySub + "ControlNo," + TableKey + ",ControlLabNo,Control" + TableKey + ",UseFlag ");
                    strSqlControl.Append(")  select ");
                    strSqlControl.Append("  '" + lst[i].Trim() + "'+'_'+convert(varchar(50)," + TableKey + ")+'_'+convert(varchar(50), " + TableKey + ") as " + TableKeySub + "ControlNo," + TableKey + ",'" + lst[i].Trim() + "' as ControlLabNo," + TableKey + ",'1' ");
                    strSqlControl.Append(" from SampleType ");
                    if (str.Trim() != "")
                        strSqlControl.Append(" where SampleTypeNo not in (" + str + ")");

                    arrySql.Add(strSql.ToString());
                    arrySql.Add(strSqlControl.ToString());

                    strSql = new StringBuilder();
                    strSqlControl = new StringBuilder();

                }

                DbHelperSQL.BatchUpdateWithTransaction(arrySql);
                //d_log.OperateLog("SampleType", "", "", DateTime.Now, 1);   
                return true;
            }
            catch
            {
                return false;
            }

        }

        public string GetControlItems(string strLabCode)
        {
            string str = "";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SampleTypeNo from B_SampleTypeControl where ControlLabNo=" + strLabCode);
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (str == "")
                        str = "'" + dr["SampleTypeNo"].ToString().Trim() + "'";
                    else
                        str += ",'" + dr["SampleTypeNo"].ToString().Trim() + "'";
                }
            }
            return str;
        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("SampleTypeNo", "SampleType");
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
            strSql.Append(" FROM SampleType ");


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
                        if (this.Exists(int.Parse(ds.Tables[0].Rows[i]["SampleTypeNo"].ToString().Trim())))
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
                strSql.Append("insert into SampleType (");
                strSql.Append("SampleTypeNo,CName,ShortCode,Visible,DispOrder,HisOrderCode");
                strSql.Append(") values (");
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
                    strSql.Append(" '" + dr["HisOrderCode"].ToString().Trim() + "' ");
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
                strSql.Append("update SampleType set ");


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
                int n = strSql.ToString().LastIndexOf(",");
                strSql.Remove(n, 1);
                strSql.Append(" where SampleTypeNo='" + dr["SampleTypeNo"].ToString().Trim() + "' ");

                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("同步数据时错误：", ex);
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
                    strSql.Append("delete from SampleType ");
                    strSql.Append(" where SampleTypeNo='" + dr["SampleTypeNo"].ToString().Trim() + "' ");
                    return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.weblis.SampleType.DeleteByDataRow同步数据时异常：", ex);
                return 0;
            }
        }

        #region IDGetListByTimeStampe 成员

        public DataSet GetListByTimeStampe(string LabCode, int dTimeStampe)
        {
            DataSet dsAll = new DataSet();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *,'" + LabCode + "' as LabCode,SampleTypeNo as LabSampleTypeNo from SampleType where 1=1 ");
            if (dTimeStampe != -999999)
            {
                strSql.Append(" and convert(int,DTimeStampe) > '" + dTimeStampe + "' ");
            }
            DataTable dtServer = DbHelperSQL.ExecuteDataSet(strSql.ToString()).Tables[0];
            dtServer.TableName = "ServerDatas";

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("select *,LabSampleTypeNo as SampleTypeNo from B_Lab_mpleType where 1=1 ");
            if (LabCode.Trim() != "")
            {
                strSql2.Append(" and LabCode= '" + LabCode + "' ");
            }
            if (dTimeStampe != -999999)
            {
                strSql2.Append(" and convert(int,DTimeStampe) > '" + dTimeStampe + "' ");
            }
            DataTable dtLab = DbHelperSQL.ExecuteDataSet(strSql2.ToString()).Tables[0];
            dtLab.TableName = "LabDatas";

            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("select * from SampleTypeControl where 1=1 ");
            if (LabCode.Trim() != "")
            {
                strSql3.Append(" and ControlLabNo= '" + LabCode + "' ");
            }
            if (dTimeStampe != -999999)
            {
                strSql3.Append(" and convert(int,DTimeStampe) > '" + dTimeStampe + "' ");
            }
            DataTable dtControl = DbHelperSQL.ExecuteDataSet(strSql3.ToString()).Tables[0];
            dtControl.TableName = "ControlDatas";

            dsAll.Tables.Add(dtServer.Copy());
            dsAll.Tables.Add(dtLab.Copy());
            dsAll.Tables.Add(dtControl.Copy());
            return dsAll;
        }

        #endregion

        public DataSet GetSampleTypeByColorName(string colorName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select dbo.SampleType.SampleTypeNo,CName,ColorName,ColorValue ");
            strSql.Append(" FROM dbo.SampleType INNER JOIN ");
            strSql.Append(" dbo.ItemColorAndSampleTypeDetail ON dbo.SampleType.SampleTypeNo = dbo.ItemColorAndSampleTypeDetail.SampleTypeNo ");
            strSql.Append(" INNER JOIN dbo.ItemColorDict ON dbo.ItemColorAndSampleTypeDetail.ColorId = dbo.ItemColorDict.ColorID ");

            if (colorName.Trim() != "")
            {
                strSql.Append(" where  ColorName='" + colorName + "' ");
            }
            strSql.Append(" order by DispOrder ");
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }


        public bool IsExist(string labCodeNo)
        {
            bool result = false;
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select COUNT(1) from B_Lab_SampleType ");
            strSql.Append(" where LabCode=" + labCodeNo + " ");

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append(" select COUNT(1) from B_SampleTypeControl ");
            strSql2.Append(" where ControlLabNo=" + labCodeNo + " ");

            if (DbHelperSQL.Exists(strSql.ToString()))
            {
                result = true;
            }
            return result;
        }

        public bool DeleteByLabCode(string LabCodeNo)
        {
            bool result = false;

            StringBuilder strSql = new StringBuilder();
            strSql.Append(" delete from B_Lab_SampleType ");
            strSql.Append(" where LabCode=" + LabCodeNo + " ");

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append(" delete from B_SampleTypeControl ");
            strSql2.Append(" where ControlLabNo=" + LabCodeNo + " ");


            int i = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            int j = DbHelperSQL.ExecuteNonQuery(strSql2.ToString());
            if (i > 0 || j > 0)
                result = true;
            return result;
        }
    }
}

