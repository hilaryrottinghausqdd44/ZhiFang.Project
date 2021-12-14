using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.MsSql.Digitlab8  
{
	 	//B_Lab_GroupItem

    public partial class B_Lab_GroupItem : IDLab_GroupItem
    {
        DBUtility.IDBConnection idb;
        public B_Lab_GroupItem(string dbsourceconn)
        {
            idb = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public B_Lab_GroupItem()
        {
            idb = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.LisDB());
        }
        D_LogInfo d_log = new D_LogInfo();
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.Lab_GroupItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into B_Lab_GroupItem(");
            strSql.Append("PItemNo,ItemNo,LabCode");
            strSql.Append(") values (");
            strSql.Append("@PItemNo,@ItemNo,@LabCode");
            strSql.Append(") ");
            //		
            SqlParameter[] parameters = {
			            new SqlParameter("@PItemNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ItemNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@LabCode", SqlDbType.VarChar,50)             
              
            };

            parameters[0].Value = model.PItemNo;
            parameters[1].Value = model.ItemNo;
            parameters[2].Value = model.LabCode;
            return idb.ExecuteNonQuery(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.Lab_GroupItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update B_Lab_GroupItem set ");

            strSql.Append(" PItemNo = @PItemNo , ");
            strSql.Append(" ItemNo = @ItemNo , ");
            strSql.Append(" LabCode = @LabCode  ");
            strSql.Append(" where PItemNo=@PItemNo and ItemNo=@ItemNo and LabCode=@LabCode  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@Id", SqlDbType.Int,4) ,            
                        new SqlParameter("@PItemNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ItemNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@LabCode", SqlDbType.VarChar,50)             
              
            };

            if (model.Id != null)
            {
                parameters[0].Value = model.Id;
            }

            if (model.PItemNo != null)
            {
                parameters[1].Value = model.PItemNo;
            }

            if (model.ItemNo != null)
            {
                parameters[2].Value = model.ItemNo;
            }

            if (model.LabCode != null)
            {
                parameters[3].Value = model.LabCode;
            }

            return idb.ExecuteNonQuery(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string PItemNo, string ItemNo, string LabCode)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from B_Lab_GroupItem ");
            strSql.Append(" where PItemNo=@PItemNo and ItemNo=@ItemNo and LabCode=@LabCode ");
            SqlParameter[] parameters = {
					new SqlParameter("@PItemNo", SqlDbType.VarChar,50),
					new SqlParameter("@ItemNo", SqlDbType.VarChar,50),
					new SqlParameter("@LabCode", SqlDbType.VarChar,50)};
            parameters[0].Value = PItemNo;
            parameters[1].Value = ItemNo;
            parameters[2].Value = LabCode;


            return idb.ExecuteNonQuery(strSql.ToString(), parameters);
           
        }
        public int Delete(ZhiFang.Model.Lab_GroupItem model, string flag)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from B_Lab_GroupItem where 1=1 ");
            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }
            if (model.PItemNo != null)
            {
                strSql.Append(" and PItemNo='" + model.PItemNo + "' ");
            }
            if (model.ItemNo != null)
            {
                if (flag == "0")
                    strSql.Append(" and ItemNo not in (" + model.ItemNo + ") ");
                else if (flag == "1")
                    strSql.Append(" and ItemNo='" + model.ItemNo + "' ");
                else
                    strSql.Append(" and ItemNo='" + model.ItemNo + "' ");
            }
            return idb.ExecuteNonQuery(strSql.ToString());
            
        }
        public int DeleteAll(string LabCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from B_Lab_GroupItem ");
            strSql.Append(" where  LabCode=@LabCode ");
            SqlParameter[] parameters = {
					new SqlParameter("@LabCode", SqlDbType.VarChar,50)};
            parameters[0].Value = LabCode;
            return idb.ExecuteNonQuery(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.Lab_GroupItem GetModel(string PItemNo, string ItemNo, string LabCode)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, PItemNo, ItemNo, LabCode  ");
            strSql.Append("  from B_Lab_GroupItem ");
            strSql.Append(" where PItemNo=@PItemNo and ItemNo=@ItemNo and LabCode=@LabCode ");
            SqlParameter[] parameters = {
					new SqlParameter("@PItemNo", SqlDbType.VarChar,50),
					new SqlParameter("@ItemNo", SqlDbType.VarChar,50),
					new SqlParameter("@LabCode", SqlDbType.VarChar,50)};
            parameters[0].Value = PItemNo;
            parameters[1].Value = ItemNo;
            parameters[2].Value = LabCode;


            ZhiFang.Model.Lab_GroupItem model = new ZhiFang.Model.Lab_GroupItem();
            DataSet ds = idb.ExecuteDataSet(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                model.PItemNo = ds.Tables[0].Rows[0]["PItemNo"].ToString();
                model.ItemNo = ds.Tables[0].Rows[0]["ItemNo"].ToString();
                model.LabCode = ds.Tables[0].Rows[0]["LabCode"].ToString();

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
            strSql.Append(" FROM B_Lab_GroupItem ");
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
            strSql.Append(" FROM B_Lab_GroupItem ");
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
        public DataSet GetList(ZhiFang.Model.Lab_GroupItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM B_Lab_GroupItem where 1=1 ");

            if (model.PItemNo != null)
            {
                strSql.Append(" and PItemNo='" + model.PItemNo + "' ");
            }

            if (model.ItemNo != null)
            {
                strSql.Append(" and ItemNo='" + model.ItemNo + "' ");
            }

            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }
            return idb.ExecuteDataSet(strSql.ToString());
        }


        #region IDataBase<Lab_GroupItem> 成员

        public int GetMaxId()
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(int Top, Model.Lab_GroupItem t, string filedOrder)
        {
            throw new NotImplementedException();
        }

        public DataSet GetAllList()
        {
            throw new NotImplementedException();
        }

        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_Lab_GroupItem");
            return Convert.ToInt32(idb.ExecuteScalar(strSql.ToString()));
        }

        public int GetTotalCount(Model.Lab_GroupItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_Lab_GroupItem where 1=1");
            if (model.PItemNo != null)
            {
                strSql.Append(" and PItemNo='" + model.PItemNo + "' ");
            }

            if (model.ItemNo != null)
            {
                strSql.Append(" and ItemNo='" + model.ItemNo + "' ");
            }

            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }
            return Convert.ToInt32(idb.ExecuteScalar(strSql.ToString()));
        }
        #endregion

        public bool Exists(string PItemNo, string ItemNo, string LabCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from B_Lab_GroupItem ");
            strSql.Append(" where PItemNo ='" + PItemNo + "' and ItemNo ='" +  ItemNo + "' and LabCode ='" +  LabCode + "' ");
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
            strSql.Append("select count(1) from B_Lab_GroupItem where 1=1 ");
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

        #region IDataBase<Lab_GroupItem> 成员
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
                        if (this.Exists(ds.Tables[0].Rows[i]["PItemNo"].ToString().Trim(), ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim(), ds.Tables[0].Rows[i]["LabCode"].ToString().Trim()))
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
                strSql.Append("insert into B_Lab_GroupItem (");
                strSql.Append("PItemNo,ItemNo,LabCode,AddTime");
                strSql.Append(") values (");
                strSql.Append("'" + dr["PItemNo"].ToString().Trim() + "','" + dr["ItemNo"].ToString().Trim() + "','" + dr["LabCode"].ToString().Trim() + "','" + dr["AddTime"].ToString().Trim() + "'");
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
                strSql.Append("update B_Lab_GroupItem set ");

                strSql.Append(" where PItemNo='" + dr["PItemNo"].ToString().Trim() + "' and ItemNo='" + dr["ItemNo"].ToString().Trim() + "' and LabCode='" + dr["LabCode"].ToString().Trim() + "' ");

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


