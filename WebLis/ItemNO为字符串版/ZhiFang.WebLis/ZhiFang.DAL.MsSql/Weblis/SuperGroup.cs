using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.MsSql.Weblis
{
    //SuperGroup

    public partial class SuperGroup : BaseDALLisDB, IDSuperGroup, IDBatchCopy, IDGetListByTimeStampe
    {
        public SuperGroup(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public SuperGroup()
        {
        }
        D_LogInfo d_log = new D_LogInfo();
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.SuperGroup model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SuperGroup(");
            strSql.Append("SuperGroupNo,CName,ShortName,ShortCode,Visible,DispOrder,ParentNo");
            strSql.Append(") values (");
            strSql.Append("@SuperGroupNo,@CName,@ShortName,@ShortCode,@Visible,@DispOrder,@ParentNo");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@SuperGroupNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@CName", SqlDbType.VarChar,40) ,            
                        new SqlParameter("@ShortName", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@ShortCode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@Visible", SqlDbType.Int,4) ,            
                        new SqlParameter("@DispOrder", SqlDbType.Int,4) ,            
                        new SqlParameter("@ParentNo", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.SuperGroupNo;
            parameters[1].Value = model.CName;
            parameters[2].Value = model.ShortName;
            parameters[3].Value = model.ShortCode;
            parameters[4].Value = model.Visible;
            parameters[5].Value = model.DispOrder;
            parameters[6].Value = model.ParentNo;
            if (DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters) > 0)
            {
                return d_log.OperateLog("SuperGroup", "", "", DateTime.Now, 1);
            }
            else
                return -1;
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.SuperGroup model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SuperGroup set ");

            strSql.Append(" SuperGroupNo = @SuperGroupNo , ");
            strSql.Append(" CName = @CName , ");
            strSql.Append(" ShortName = @ShortName , ");
            strSql.Append(" ShortCode = @ShortCode , ");
            strSql.Append(" Visible = @Visible , ");
            strSql.Append(" DispOrder = @DispOrder , ");
            strSql.Append(" ParentNo = @ParentNo  ");
            strSql.Append(" where SuperGroupNo=@SuperGroupNo  ");

            SqlParameter[] parameters = {
			               
            new SqlParameter("@SuperGroupNo", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@CName", SqlDbType.VarChar,40) ,            	
                           
            new SqlParameter("@ShortName", SqlDbType.VarChar,10) ,            	
                           
            new SqlParameter("@ShortCode", SqlDbType.VarChar,10) ,            	
                           
            new SqlParameter("@Visible", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@DispOrder", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@ParentNo", SqlDbType.Int,4)             	
              
            };


            if (model.SuperGroupNo != null)
            {
                parameters[0].Value = model.SuperGroupNo;
            }



            if (model.CName != null)
            {
                parameters[1].Value = model.CName;
            }



            if (model.ShortName != null)
            {
                parameters[2].Value = model.ShortName;
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



            if (model.ParentNo != null)
            {
                parameters[6].Value = model.ParentNo;
            }


            if (DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters) > 0)
            {
                return d_log.OperateLog("SuperGroup", "", "", DateTime.Now, 1);
            }
            else
                return -1;
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int SuperGroupNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SuperGroup ");
            strSql.Append(" where SuperGroupNo=@SuperGroupNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@SuperGroupNo", SqlDbType.Int,4)};
            parameters[0].Value = SuperGroupNo;


            return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);

        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SuperGroup ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());

        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.SuperGroup GetModel(int SuperGroupNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SuperGroupNo, CName, ShortName, ShortCode, Visible, DispOrder, ParentNo  ");
            strSql.Append("  from SuperGroup ");
            strSql.Append(" where SuperGroupNo=@SuperGroupNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@SuperGroupNo", SqlDbType.Int,4)};
            parameters[0].Value = SuperGroupNo;


            ZhiFang.Model.SuperGroup model = new ZhiFang.Model.SuperGroup();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["SuperGroupNo"].ToString() != "")
                {
                    model.SuperGroupNo = int.Parse(ds.Tables[0].Rows[0]["SuperGroupNo"].ToString());
                }
                model.CName = ds.Tables[0].Rows[0]["CName"].ToString();
                model.ShortName = ds.Tables[0].Rows[0]["ShortName"].ToString();
                model.ShortCode = ds.Tables[0].Rows[0]["ShortCode"].ToString();
                if (ds.Tables[0].Rows[0]["Visible"].ToString() != "")
                {
                    model.Visible = int.Parse(ds.Tables[0].Rows[0]["Visible"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DispOrder"].ToString() != "")
                {
                    model.DispOrder = int.Parse(ds.Tables[0].Rows[0]["DispOrder"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ParentNo"].ToString() != "")
                {
                    model.ParentNo = int.Parse(ds.Tables[0].Rows[0]["ParentNo"].ToString());
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
			strSql.Append(" FROM SuperGroup ");
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
            strSql.Append(" FROM SuperGroup ");
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
        public DataSet GetList(ZhiFang.Model.SuperGroup model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SuperGroup where 1=1 ");


            if (model.SuperGroupNo != 0)
            {
                strSql.Append(" and SuperGroupNo=" + model.SuperGroupNo + " ");
            }

            if (model.CName != null)
            {
                strSql.Append(" and CName='" + model.CName + "' ");
            }

            if (model.ShortName != null)
            {
                strSql.Append(" and ShortName='" + model.ShortName + "' ");
            }

            if (model.ShortCode != null)
            {
                strSql.Append(" and ShortCode='" + model.ShortCode + "' ");
            }

            if (model.DispOrder != null)
            {
                strSql.Append(" and DispOrder=" + model.DispOrder + " ");
            }

            if (model.ParentNo != null)
            {
                strSql.Append(" and ParentNo=" + model.ParentNo + " ");
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 获取总记录
        /// </summary>
        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM SuperGroup ");
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
        public int GetTotalCount(ZhiFang.Model.SuperGroup model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM SuperGroup where 1=1 ");
            string strLike = "";
            if (model.SearchLikeKey != null)
            {
                strLike = " and (SuperGroupNo like '%" + model.SearchLikeKey + "%' or CName like '%" + model.SearchLikeKey + "%' or ShortName like '%" + model.SearchLikeKey + "%' or ShortCode like '%" + model.SearchLikeKey + "%') ";
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
        /// 利用主键
        /// </summary>
        /// <param name="model">model=null时为中心字典表分页；model!=null时为中心--实验室 对照关系分页</param>
        /// <param name="nowPageNum">第几页</param>
        /// <param name="nowPageSize">每页多少行</param>
        /// <returns>DataSet</returns>
        public DataSet GetListByPage(ZhiFang.Model.SuperGroup model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            string strLike = "";
            
            if (model != null && model.LabCode != null)
            {
                if (model.SearchLikeKey != null)
                {
                    strLike = " and (SuperGroup.SuperGroupNo like '%" + model.SearchLikeKey + "%' or CName like '%" + model.SearchLikeKey + "%' or ShortName like '%" + model.SearchLikeKey + "%' or ShortCode like '%" + model.SearchLikeKey + "%') ";
                }
                string strOrderBy = "";
                if (model.OrderField == "SuperGroupID")
                {
                    strOrderBy = "SuperGroup.SuperGroupNo";
                }
                else if (model.OrderField.ToLower().IndexOf("control") >= 0)
                {
                    strOrderBy = "B_SuperGroupControl." + model.OrderField;
                }
                else
                {
                    strOrderBy = "SuperGroup." + model.OrderField;
                }
                strSql.Append(" select top " + nowPageSize + "  * from SuperGroup left join B_SuperGroupControl on SuperGroup.SuperGroupNo=B_SuperGroupControl.SuperGroupNo ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and B_SuperGroupControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append("where SuperGroup.SuperGroupNo not in ( ");
                strSql.Append("select top " + (nowPageSize * nowPageNum) + " SuperGroup.SuperGroupNo from  SuperGroup left join B_SuperGroupControl on SuperGroup.SuperGroupNo=B_SuperGroupControl.SuperGroupNo ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and B_SuperGroupControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append("where 1=1 " + strLike + " order by " + strOrderBy + " desc ) " + strLike + " order by " + strOrderBy + " desc ");
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
            else
            {
                if (model.SearchLikeKey != null)
                {
                    strLike = " and (SuperGroupNo like '%" + model.SearchLikeKey + "%' or CName like '%" + model.SearchLikeKey + "%' or ShortName like '%" + model.SearchLikeKey + "%' or ShortCode like '%" + model.SearchLikeKey + "%') ";
                }
                strSql.Append("select top " + nowPageSize + "  * from SuperGroup where SuperGroupNo not in  ");
                strSql.Append("(select top " + (nowPageSize * nowPageNum) + " SuperGroupNo from SuperGroup where 1=1 " + strLike + " order by " + model.OrderField + " desc ) " + strLike + " order by " + model.OrderField + " desc ");
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
        }

        public bool Exists(int SuperGroupNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SuperGroup ");
            strSql.Append(" where SuperGroupNo ='" + SuperGroupNo + "'");
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
            strSql.Append("select count(1) from SuperGroup where 1=1 ");
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
            string LabTableName = "SuperGroup";
            LabTableName = "B_Lab_" + LabTableName.Substring(LabTableName.IndexOf("_") + 1, LabTableName.Length - (LabTableName.IndexOf("_") + 1));
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSqlControl = new StringBuilder();
            string TableKey = "SuperGroupNo";
            string TableKeySub = TableKey;
            if (TableKey.ToLower().Contains("no"))
            {
                TableKeySub = TableKey.Substring(0, TableKey.ToLower().IndexOf("no"));
            }
            try
            {
                for (int i = 0; i < lst.Count; i++)
                {
                    string str = this.GetControlItems(lst[i].Trim());
                    strSql.Append("insert into " + LabTableName + "( LabCode,");
                    strSql.Append(" LabSuperGroupNo , CName , ShortName , ShortCode , Visible , DispOrder , ParentNo ");
                    strSql.Append(") select '" + lst[i].Trim() + "' as LabCode, ");
                    strSql.Append("SuperGroupNo,CName,ShortName,ShortCode,Visible,DispOrder,ParentNo");
                    strSql.Append(" from SuperGroup ");
                    if (str.Trim() != "")
                        strSql.Append(" where SuperGroupNo not in (" + str + ")");

                    strSqlControl.Append("insert into B_SuperGroupControl ( ");
                    strSqlControl.Append(" " + TableKeySub + "ControlNo," + TableKey + ",ControlLabNo,Control" + TableKey + ",UseFlag ");
                    strSqlControl.Append(")  select ");
                    strSqlControl.Append("  '" + lst[i].Trim() + "'+'_'+convert(varchar(50)," + TableKey + ")+'_'+convert(varchar(50), " + TableKey + ") as " + TableKeySub + "ControlNo," + TableKey + ",'" + lst[i].Trim() + "' as ControlLabNo," + TableKey + ",'1' ");
                    strSqlControl.Append(" from SuperGroup ");
                    if (str.Trim() != "")
                        strSqlControl.Append(" where SuperGroupNo not in (" + str + ")");

                    arrySql.Add(strSql.ToString());
                    arrySql.Add(strSqlControl.ToString());

                    strSql = new StringBuilder();
                    strSqlControl = new StringBuilder();

                }

                DbHelperSQL.BatchUpdateWithTransaction(arrySql);
                //d_log.OperateLog("SuperGroup", "", "", DateTime.Now, 1);
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
            strSql.Append("select SuperGroupNo from B_SuperGroupControl where ControlLabNo=" + strLabCode);
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (str == "")
                        str = "'" + dr["SuperGroupNo"].ToString().Trim() + "'";
                    else
                        str += ",'" + dr["SuperGroupNo"].ToString().Trim() + "'";
                }
            }
            return str;
        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("SuperGroupNo", "SuperGroup");
        }

        public DataSet GetList(int Top, ZhiFang.Model.SuperGroup model, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM SuperGroup ");


            if (model.SuperGroupNo != null)
            {
                strSql.Append(" and SuperGroupNo=" + model.SuperGroupNo + " ");
            }

            if (model.CName != null)
            {

                strSql.Append(" and CName='" + model.CName + "' ");
            }

            if (model.ShortName != null)
            {

                strSql.Append(" and ShortName='" + model.ShortName + "' ");
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

            if (model.ParentNo != null)
            {
                strSql.Append(" and ParentNo=" + model.ParentNo + " ");
            }

            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetAllList()
        {
            return GetList("");
        }

        public System.Data.DataSet GetParentSuperGroupNolist()
        {
            DataSet dsAll = new DataSet();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from SuperGroup where 1=1 ");
            strSql.Append(" and ParentNo is null ");
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
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
                        if (this.Exists(int.Parse(ds.Tables[0].Rows[i]["SuperGroupNo"].ToString().Trim())))
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
                strSql.Append("insert into SuperGroup (");
                strSql.Append("SuperGroupNo,CName,ShortName,ShortCode,Visible,DispOrder");
                strSql.Append(") values (");
                if (dr.Table.Columns["SuperGroupNo"] != null && dr.Table.Columns["SuperGroupNo"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["SuperGroupNo"].ToString().Trim() + "', ");
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
                if (dr.Table.Columns["ShortName"] != null && dr.Table.Columns["ShortName"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["ShortName"].ToString().Trim() + "', ");
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
                //if (dr.Table.Columns["ParentNo"] != null && dr.Table.Columns["ParentNo"].ToString().Trim() != "")
                //{
                //    strSql.Append(" '" + dr["ParentNo"].ToString().Trim() + "', ");
                //}
                //else
                //{
                //    strSql.Append(" null, ");
                //}
                int n = strSql.ToString().LastIndexOf(",");
                strSql.Remove(n, 1);
                strSql.Append(") ");
                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Digitlab2009.SuperGroup.AddByDataRow 同步数据时异常：", ex);
                return 0;
            }
        }
        public int UpdateByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update SuperGroup set ");

                if (dr.Table.Columns["CName"] != null && dr.Table.Columns["CName"].ToString().Trim() != "")
                {
                    strSql.Append(" CName = '" + dr["CName"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["ShortName"] != null && dr.Table.Columns["ShortName"].ToString().Trim() != "")
                {
                    strSql.Append(" ShortName = '" + dr["ShortName"].ToString().Trim() + "' , ");
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

                //if (dr.Table.Columns["ParentNo"] != null && dr.Table.Columns["ParentNo"].ToString().Trim() != "")
                //{
                //    strSql.Append(" ParentNo = '" + dr["ParentNo"].ToString().Trim() + "' , ");
                //}

                int n = strSql.ToString().LastIndexOf(",");
                strSql.Remove(n, 1);
                strSql.Append(" where SuperGroupNo='" + dr["SuperGroupNo"].ToString().Trim() + "' ");

                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Digitlab2009.SuperGroup .UpdateByDataRow同步数据时异常：", ex);
                return 0;
            }
        }
        public int DeleteByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                if (dr.Table.Columns["SuperGroupNo"] != null && dr.Table.Columns["SuperGroupNo"].ToString().Trim() != "")
                {
                    strSql.Append("delete from SuperGroup ");
                    strSql.Append(" where SuperGroupNo='" + dr["SuperGroupNo"].ToString().Trim() + "' ");
                    return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.weblis.SuperGroup.DeleteByDataRow同步数据时异常：", ex);
                return 0;
            }
        }
        #region IDGetListByTimeStampe 成员

        public DataSet GetListByTimeStampe(string LabCode, int dTimeStampe)
        {
            DataSet dsAll = new DataSet();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *,'" + LabCode + "' as LabCode,SuperGroupNo as LabSuperGroupNo from SuperGroup where 1=1 ");
            if (dTimeStampe != -999999)
            {
                strSql.Append(" and convert(int,DTimeStampe) > '" + dTimeStampe + "' ");
            }
            DataTable dtServer = DbHelperSQL.ExecuteDataSet(strSql.ToString()).Tables[0];
            dtServer.TableName = "ServerDatas";

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("select *,LabSuperGroupNo as SuperGroupNo from B_Lab_perGroup where 1=1 ");
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
            strSql3.Append("select * from SuperGroupControl where 1=1 ");
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
         public System.Collections.Generic.List<Model.SuperGroup> GetListModel()
        {
            System.Collections.Generic.List<Model.SuperGroup> list=new System.Collections.Generic.List<Model.SuperGroup>();
            DataTable dt = GetList("").Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                Model.SuperGroup sup = new Model.SuperGroup();
                sup.SuperGroupID =int.Parse( row["SuperGroupID"].ToString());
                sup.SuperGroupNo = int.Parse(row["SuperGroupNo"].ToString());
                sup.CName = row["CName"].ToString();
                list.Add(sup);
            }
            return list;
 
        }



         public bool IsExist(string labCodeNo)
         {
             bool result = false;
             StringBuilder strSql = new StringBuilder();
             strSql.Append(" select COUNT(1) from b_lab_SuperGroup ");
             strSql.Append(" where LabCode=" + labCodeNo + " ");

             StringBuilder strSql2 = new StringBuilder();
             strSql2.Append(" select COUNT(1) from B_SuperGroupControl ");
             strSql2.Append(" where ControlLabNo=" + labCodeNo + " ");

             if (DbHelperSQL.Exists(strSql.ToString()) )
             {
                 result = true;
             }
             return result;
         }

         public bool DeleteByLabCode(string LabCodeNo)
         {
             bool result = false;

             StringBuilder strSql = new StringBuilder();
             strSql.Append(" delete from b_lab_SuperGroup ");
             strSql.Append(" where LabCode=" + LabCodeNo + " ");

             StringBuilder strSql2 = new StringBuilder();
             strSql2.Append(" delete from B_SuperGroupControl ");
             strSql2.Append(" where ControlLabNo=" + LabCodeNo + " ");


             int i = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
             int j = DbHelperSQL.ExecuteNonQuery(strSql2.ToString());
             if (i > 0 || j > 0)
                 result = true;
             return result;
         }
    }
}


