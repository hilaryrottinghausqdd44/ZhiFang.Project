using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.MsSql.Digitlab8  
{
	 	//B_TestItemControl
    public partial class B_TestItemControl : IDTestItemControl,IDBatchCopy
    {
        DBUtility.IDBConnection idb;
        public B_TestItemControl(string dbsourceconn)
        {
            idb = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public B_TestItemControl()
        {
            idb = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.LisDB());
        }
        D_LogInfo d_log = new D_LogInfo();
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.TestItemControl model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into B_TestItemControl(");
            strSql.Append("ItemControlNo,ItemNo,ControlLabNo,ControlItemNo");
            strSql.Append(") values (");
            strSql.Append("@ItemControlNo,@ItemNo,@ControlLabNo,@ControlItemNo");
            strSql.Append(") ");
            //strSql.Append(";select @@IDENTITY");		
            SqlParameter[] parameters = {
			            new SqlParameter("@ItemControlNo", SqlDbType.Char,50) ,            
                        new SqlParameter("@ItemNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ControlLabNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ControlItemNo", SqlDbType.VarChar,50)            
              
            };

            parameters[0].Value = model.ItemControlNo;
            parameters[1].Value = model.ItemNo;
            parameters[2].Value = model.ControlLabNo;
            parameters[3].Value = model.ControlItemNo;
            int rows = idb.ExecuteNonQuery(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return d_log.OperateLog("TestItem", "", "", DateTime.Now, 1);
               
            }
            else
            {
                return -1;
            }
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.TestItemControl model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update B_TestItemControl set ");

            strSql.Append(" ItemNo = @ItemNo , ");
            strSql.Append(" ControlLabNo = @ControlLabNo , ");
            strSql.Append(" ControlItemNo = @ControlItemNo , ");
            strSql.Append(" UseFlag = @UseFlag  ");
            strSql.Append(" where ItemNo=@ItemNo and ControlLabNo=@ControlLabNo ");

            SqlParameter[] parameters = {                     
                        new SqlParameter("@ItemNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ControlLabNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ControlItemNo", SqlDbType.VarChar,50) ,          
                        new SqlParameter("@UseFlag", SqlDbType.Int,4)             
              
            };
            
            

            if (model.ItemNo != null)
            {
                parameters[0].Value = model.ItemNo;
            }

            if (model.ControlLabNo != null)
            {
                parameters[1].Value = model.ControlLabNo;
            }

            if (model.ControlItemNo != null)
            {
                parameters[2].Value = model.ControlItemNo;
            }

            if (model.UseFlag != null)
            {
                parameters[3].Value = model.UseFlag;
            }

            int rows = idb.ExecuteNonQuery(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return d_log.OperateLog("TestItem", "", "", DateTime.Now, 1);
              
            }
            else
            {
                return -1;
            }
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from B_TestItemControl ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
};
            parameters[0].Value = Id;


            return idb.ExecuteNonQuery(strSql.ToString(), parameters);
            
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from B_TestItemControl ");
            strSql.Append(" where ID in (" + Idlist + ")  ");
            return idb.ExecuteNonQuery(strSql.ToString());
           
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.TestItemControl GetModel(string ItemNo, string LabCode, string LabItemNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, ItemControlNo, ItemNo, ControlLabNo, ControlItemNo, DTimeStampe, AddTime, UseFlag  ");
            strSql.Append("  from B_TestItemControl ");
            strSql.Append(" where ItemNo=@ItemNo and ControlLabNo=@ControlLabNo");
            SqlParameter[] parameters = {          
                        new SqlParameter("@ItemNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ControlLabNo", SqlDbType.VarChar,50)       
              
            };

            parameters[0].Value = ItemNo;
            parameters[1].Value = LabCode;


            ZhiFang.Model.TestItemControl model = new ZhiFang.Model.TestItemControl();
            DataSet ds = idb.ExecuteDataSet(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                model.ItemControlNo = ds.Tables[0].Rows[0]["ItemControlNo"].ToString();
                model.ItemNo = ds.Tables[0].Rows[0]["ItemNo"].ToString();
                model.ControlLabNo = ds.Tables[0].Rows[0]["ControlLabNo"].ToString();
                model.ControlItemNo = ds.Tables[0].Rows[0]["ControlItemNo"].ToString();
           
                if (ds.Tables[0].Rows[0]["AddTime"].ToString() != "")
                {
                    model.AddTime = DateTime.Parse(ds.Tables[0].Rows[0]["AddTime"].ToString());
                }
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
            strSql.Append(" FROM B_TestItemControl ");
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
            strSql.Append(" FROM B_TestItemControl ");
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
        public DataSet GetList(ZhiFang.Model.TestItemControl model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM B_TestItemControl where 1=1 ");

            if (model.ItemControlNo != null)
            {
                strSql.Append(" and ItemControlNo='" + model.ItemControlNo + "' ");
            }

            if (model.ItemNo != null)
            {
                strSql.Append(" and ItemNo=" + model.ItemNo + " ");
            }

            if (model.ControlLabNo != null)
            {
                strSql.Append(" and ControlLabNo='" + model.ControlLabNo + "' ");
            }

            if (model.ControlItemNo != null)
            {
                strSql.Append(" and ControlItemNo=" + model.ControlItemNo + " ");
            }

            return idb.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 获取总记录的数量
        /// </summary>
        /// <returns></returns>
        public int GetTotalCount(ZhiFang.Model.TestItemControl model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_TestItemControl where 1=1 ");
            if (model.ItemControlNo != null)
            {
                strSql.Append(" and ItemControlNo='" + model.ItemControlNo + "' ");
            }

            if (model.ItemNo != null)
            {
                strSql.Append(" and ItemNo=" + model.ItemNo + " ");
            }

            if (model.ControlLabNo != null)
            {
                strSql.Append(" and ControlLabNo='" + model.ControlLabNo + "' ");
            }

            if (model.ControlItemNo != null)
            {
                strSql.Append(" and ControlItemNo=" + model.ControlItemNo + " ");
            }

            if (model.DTimeStampe != null)
            {
                strSql.Append(" and DTimeStampe='" + model.DTimeStampe + "' ");
            }

            if (model.AddTime != null)
            {
                strSql.Append(" and AddTime='" + model.AddTime + "' ");
            }

            if (model.UseFlag != null)
            {
                strSql.Append(" and UseFlag=" + model.UseFlag + " ");
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
        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_TestItemControl");
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
        public bool Exists(string ItemControlNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from B_TestItemControl ");
            strSql.Append(" where ItemControlNo ='" + ItemControlNo + "'");
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
            strSql.Append("select count(1) from B_TestItemControl where 1=1 ");
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

        #region IDBatchCopy 成员

        public bool CopyToLab(List<string> lst)
        {
            System.Collections.ArrayList arrySql = new System.Collections.ArrayList();
            int countflag = 0;
            StringBuilder strSql = new StringBuilder();
            if (lst.Count > 0)
            {
                for (int i = 0; i < lst.Count; i++)
                {
                    strSql.Append("insert into B_TestItemControl (ItemControlNo,ItemNo,ControlLabNo,ControlItemNo,UseFlag) ");
                    strSql.Append(" select '" + lst[i].Trim() + "'+'_'+ItemNo+'_'+ItemNo as ItemControlNo,ItemNo,'" + lst[i].Trim() + "' as ControlLabNo,ItemNo,UseFlag from B_TestItem ");

                    arrySql.Add(strSql.ToString());
                    if (idb.ExecuteNonQuery(strSql.ToString()) > 0)
                        countflag++;
                }
            }
            if (countflag == lst.Count)
                return true;
            else
                return false;
        }

        #endregion

        

        #region IDataBase<TestItemControl> 成员

        public int GetMaxId()
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(int Top, Model.TestItemControl t, string filedOrder)
        {
            throw new NotImplementedException();
        }

        public DataSet GetAllList()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDataBase<TestItemControl> 成员
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
                        if (this.Exists(ds.Tables[0].Rows[i]["ItemControlNo"].ToString().Trim()))
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
                strSql.Append("insert into B_TestItemControl (");
                strSql.Append("ItemControlNo,ItemNo,ControlLabNo,ControlItemNo,AddTime,UseFlag");
                strSql.Append(") values (");
                strSql.Append("'" + dr["ItemControlNo"].ToString().Trim() + "','" + dr["ItemNo"].ToString().Trim() + "','" + dr["ControlLabNo"].ToString().Trim() + "','" + dr["ControlItemNo"].ToString().Trim() + "','" + DateTime.Parse(dr["AddTime"].ToString().Trim()) + "','" + dr["UseFlag"].ToString().Trim() + "'");
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
                strSql.Append("update B_TestItemControl set ");

                strSql.Append(" ItemNo = '" + dr["ItemNo"].ToString().Trim() + "' , ");
                strSql.Append(" ControlLabNo = '" + dr["ControlLabNo"].ToString().Trim() + "' , ");
                strSql.Append(" ControlItemNo = '" + dr["ControlItemNo"].ToString().Trim() + "' , ");
                strSql.Append(" UseFlag = '" + dr["UseFlag"].ToString().Trim() + "'  ");
                strSql.Append(" where ItemControlNo='" + dr["ItemControlNo"].ToString().Trim() + "' ");

                return idb.ExecuteNonQuery(strSql.ToString());
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region IDBatchCopy 成员


        public int DeleteByDataRow(DataRow dr)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDTestItemControl 成员


        public bool CheckIncludeLabCode(List<string> l, string LabCode)
        {
            throw new NotImplementedException();
        }

        public bool CheckIncludeCenterCode(List<string> l, string LabCode)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDTestItemControl 成员


        public DataSet GetLabCodeNo(string LabCode, List<string> CenterNoList)
        {
            throw new NotImplementedException();
        }

        public DataSet GetCenterNo(string LabCode, List<string> LabPrimaryNoList)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDTestItemControl 成员


        public DataSet GetLabItemCodeMapListByNRequestLabCodeAndFormNo(string LabCode, string NRequestFormNo)
        {
            throw new NotImplementedException();
        }

        #endregion
        #region 结果项目字典对照关系表
        /// <summary>
        /// 利用标识列分页
        /// 结果项目字典对照关系表 此对照表用于报告下载的时候 ganwh add 2015-9-6
        /// </summary>
        /// <param name="model">model=null时为中心字典表分页；model!=null时为中心--实验室 对照关系分页</param>
        /// <param name="nowPageNum">第几页</param>
        /// <param name="nowPageSize">每页多少行</param>
        /// <returns>DataSet</returns>
        public DataSet B_lab_GetResultListByPage(ZhiFang.Model.ResultTestItemControl model, int nowPageNum, int nowPageSize)
        {
            return new DataSet();
        }
        #endregion

        public DataSet GetListByPage(Model.TestItemControl model, int nowPageNum, int nowPageSize)
        {
            throw new NotImplementedException();
        }

        public DataSet B_lab_GetListByPage(Model.TestItemControl model, int nowPageNum, int nowPageSize)
        {
            throw new NotImplementedException();
        }


        public int Delete(string ItemControlNo)
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
    }
}

