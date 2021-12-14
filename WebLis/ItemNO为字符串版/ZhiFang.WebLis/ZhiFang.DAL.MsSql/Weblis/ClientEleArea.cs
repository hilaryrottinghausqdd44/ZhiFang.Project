using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.MsSql.Weblis
{
    //ClientEleArea

    public partial class ClientEleArea : IDClientEleArea, IDBatchCopy, IDGetListByTimeStampe
    {
        DBUtility.IDBConnection idb;
        public ClientEleArea(string dbsourceconn)
        {
            idb = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public ClientEleArea()
        {
            idb = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.LisDB());
        }
        D_LogInfo d_log = new D_LogInfo();
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.ClientEleArea model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ClientEleArea(");
            strSql.Append("AreaID,AreaCName,AreaShortName,ClientNo");
            strSql.Append(") values (");
            strSql.Append("@AreaID,@AreaCName,@AreaShortName,@ClientNo");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@AreaID", SqlDbType.Int,4) ,            
                        new SqlParameter("@AreaCName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@AreaShortName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ClientNo", SqlDbType.Int,4) ,            
              
            };

            parameters[0].Value = model.AreaID;
            parameters[1].Value = model.AreaCName;
            parameters[2].Value = model.AreaShortName;
            parameters[3].Value = model.ClientNo;

            return idb.ExecuteNonQuery(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.ClientEleArea model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ClientEleArea set ");

            strSql.Append(" AreaCName = @AreaCName , ");
            strSql.Append(" AreaShortName = @AreaShortName , ");
            strSql.Append(" ClientNo = @ClientNo ");
            strSql.Append(" where AreaID=@AreaID  ");

            SqlParameter[] parameters = {			               
            new SqlParameter("@AreaID", SqlDbType.Int,4) , 
            new SqlParameter("@AreaCName", SqlDbType.VarChar,50) ,  
            new SqlParameter("@AreaShortName", SqlDbType.VarChar,50) ,   
            new SqlParameter("@ClientNo", SqlDbType.Int,4) ,           
            };


            if (model.AreaID != null)
            {
                parameters[0].Value = model.AreaID;
            }

            if (model.AreaCName != null)
            {
                parameters[1].Value = model.AreaCName;
            }

            if (model.AreaShortName != null)
            {
                parameters[2].Value = model.AreaShortName;
            }

            if (model.ClientNo != null)
            {
                parameters[3].Value = model.ClientNo;
            }

            return idb.ExecuteNonQuery(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int AreaID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ClientEleArea ");
            strSql.Append(" where AreaID=@AreaID ");
            SqlParameter[] parameters = {
					new SqlParameter("@AreaID", SqlDbType.Int,4)};
            parameters[0].Value = AreaID;


            return idb.ExecuteNonQuery(strSql.ToString(), parameters);

        }



        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.ClientEleArea GetModel(int AreaID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AreaID, AreaCName, AreaShortName, ClientNo, AddTime  ");
            strSql.Append("  from ClientEleArea ");
            strSql.Append(" where AreaID=@AreaID ");
            SqlParameter[] parameters = {
					new SqlParameter("@AreaID", SqlDbType.Int,4)};
            parameters[0].Value = AreaID;


            ZhiFang.Model.ClientEleArea model = new ZhiFang.Model.ClientEleArea();
            DataSet ds = idb.ExecuteDataSet(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {

                if (ds.Tables[0].Rows[0]["AreaID"].ToString() != "")
                {
                    model.AreaID = int.Parse(ds.Tables[0].Rows[0]["AreaID"].ToString());
                }

                model.AreaCName = ds.Tables[0].Rows[0]["AreaCName"].ToString();

                model.AreaShortName = ds.Tables[0].Rows[0]["AreaShortName"].ToString();

                if (ds.Tables[0].Rows[0]["ClientNo"].ToString() != "")
                {
                    model.ClientNo = int.Parse(ds.Tables[0].Rows[0]["ClientNo"].ToString());
                }

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
            strSql.Append(" FROM ClientEleArea ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return idb.ExecuteDataSet(strSql.ToString());
        }


        /// <summary>
        /// 根据实体获取DataSet
        /// </summary>
        public DataSet GetList(ZhiFang.Model.ClientEleArea model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ClientEleArea where 1=1 ");



            if (model.AreaID != 0)
            {

                strSql.Append(" and AreaID=" + model.AreaID + " ");
            }

            if (model.AreaCName != null)
            {

                strSql.Append(" and AreaCName='" + model.AreaCName + "' ");
            }

            if (model.AreaShortName != null)
            {

                strSql.Append(" and AreaShortName='" + model.AreaShortName + "' ");
            }

            if (model.ClientNo != null)
            {

                strSql.Append(" and ClientNo=" + model.ClientNo + " ");
            }
            return idb.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetListLike(ZhiFang.Model.ClientEleArea model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *,AreaID as LabNo,convert(varchar(100),AreaID)+'_'+AreaCName as LabNo_Value,AreaCName+'('+convert(varchar(100),AreaID)+')' as LabNoAndName_Text ");
            strSql.Append(" FROM ClientEleArea  ");
            if (model.AreaCName != null)
            {
                strSql.Append(" where 1=2 ");
                strSql.Append(" or AreaCName like '%" + model.AreaCName + "%' ");
            }

            if (model.AreaID != 0)
            {
                if (strSql.ToString().IndexOf("where 1=2") < 0)
                {
                    strSql.Append(" where 1=2 ");
                }
                strSql.Append(" or AreaID like '%" + model.AreaID + "%' ");
            }

            if (model.AreaShortName != null)
            {
                if (strSql.ToString().IndexOf("where 1=2") < 0)
                {
                    strSql.Append(" where 1=2 ");
                }
                strSql.Append(" or AreaShortName like '%" + model.AreaShortName + "%' ");
            }

            return idb.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 获取总记录
        /// </summary>
        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM ClientEleArea ");
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
        public int GetTotalCount(ZhiFang.Model.ClientEleArea model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM ClientEleArea where 1=1 ");
            if (model != null)
            {
                string strWhere = "";
                if (model.SearchLikeKey != null && model.SearchLikeKey.Trim().Length > 0)
                {
                    strWhere = " and (AreaID like '%" + model.SearchLikeKey.Trim() + "%' or AreaCName like '%" + model.SearchLikeKey.Trim() + "%' or AreaShortName like '%" + model.SearchLikeKey.Trim() + "%') ";
                }
                strSql.Append(strWhere.ToString());
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

        /// <summary>
        /// 利用标识列分页
        /// </summary>
        /// <param name="model">model=null时为中心字典表分页；model!=null时为中心--实验室 对照关系分页</param>
        /// <param name="nowPageNum">第几页</param>
        /// <param name="nowPageSize">每页多少行</param>
        /// <returns>DataSet</returns>
        public DataSet GetListByPage(ZhiFang.Model.ClientEleArea model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            try
            {
                string strWhere = "";
                if (model.SearchLikeKey != null && model.SearchLikeKey.Trim().Length > 0)
                {
                    strWhere = " and (ClientEleArea.AreaID like '%" + model.SearchLikeKey.Trim() + "%' or ClientEleArea.AreaCName like '%" + model.SearchLikeKey.Trim() + "%' or ClientEleArea.AreaShortName like '%" + model.SearchLikeKey.Trim() + "%') ";
                }
                strSql.Append("select top " + nowPageSize + "  * from ClientEleArea LEFT JOIN ClientEle on ClientEleArea.ClientNo=ClientEle.ClientNo where ClientEleArea.AreaID not in  ");
                strSql.Append("(select top " + (nowPageSize * nowPageNum) + " ClientEleArea.AreaID from ClientEleArea LEFT JOIN ClientEle on ClientEleArea.ClientNo=ClientEle.ClientNo where 1=1 " + strWhere + " order by  ClientEleArea." + model.OrderField + " desc ) " + strWhere + " order by  ClientEleArea." + model.OrderField + " desc ");
                //ZhiFang.Common.Log.Log.Info(strSql.ToString());
                return idb.ExecuteDataSet(strSql.ToString());
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Weblis.ClientEleArea.GetListByPage出错，sql=" + strSql.ToString(), ex);
                return null;
            }

        }

        public List<ZhiFang.Model.ClientEleArea> DataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.ClientEleArea> modelList = new List<ZhiFang.Model.ClientEleArea>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.ClientEleArea model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.ClientEleArea();
                    if (dt.Columns.Contains("AreaID") && dt.Rows[n]["AreaID"].ToString() != "")
                    {
                        model.AreaID = int.Parse(dt.Rows[n]["AreaID"].ToString());
                    }
                    if (dt.Columns.Contains("CNAME") && dt.Rows[n]["CNAME"].ToString() != "")
                    {
                        model.ClientName = dt.Rows[n]["CNAME"].ToString();
                    }
                    if (dt.Columns.Contains("AreaCName") && dt.Rows[n]["AreaCName"].ToString() != "")
                    {
                        model.AreaCName = dt.Rows[n]["AreaCName"].ToString();
                    }
                    if (dt.Columns.Contains("AreaShortName") && dt.Rows[n]["AreaShortName"].ToString() != "")
                    {
                        model.AreaShortName = dt.Rows[n]["AreaShortName"].ToString();
                    }
                    if (dt.Columns.Contains("ClientNo") && dt.Rows[n]["ClientNo"].ToString() != "")
                    {
                        model.ClientNo = Convert.ToInt64(dt.Rows[n]["ClientNo"].ToString());
                    }
                    if (dt.Columns.Contains("AddTime") && dt.Rows[n]["AddTime"].ToString() != "")
                    {
                        model.AddTime = DateTime.Parse(dt.Rows[n]["AddTime"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        public bool Exists(int AreaID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ClientEleArea ");
            strSql.Append(" where AreaID ='" + AreaID + "'");
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
            strSql.Append("select count(1) from ClientEleArea where 1=1 ");
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
            string LabTableName = "ClientEleArea";
            LabTableName = "B_Lab_" + LabTableName.Substring(LabTableName.IndexOf("_") + 1, LabTableName.Length - (LabTableName.IndexOf("_") + 1));
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSqlControl = new StringBuilder();
            string TableKey = "AreaID";
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
                    strSql.Append(" LabAreaID , AreaCName , AreaShortName , ClientNo ,");
                    strSql.Append(") select '" + lst[i].Trim() + "' as LabCode, ");
                    strSql.Append("AreaID,AreaCName,AreaShortName,ClientNo,");
                    strSql.Append(" from ClientEleArea ");

                    strSqlControl.Append("insert into ClientEleAreaControl ( ");
                    strSqlControl.Append(" " + TableKeySub + "ControlNo," + TableKey + ",ControlLabNo,Control" + TableKey + ",UseFlag ");
                    strSqlControl.Append(")  select ");
                    strSqlControl.Append("  '" + lst[i].Trim() + "'+'_'+convert(varchar(50)," + TableKey + ")+'_'+convert(varchar(50), " + TableKey + ") as " + TableKeySub + "ControlNo," + TableKey + ",'" + lst[i].Trim() + "' as ControlLabNo," + TableKey + ",UseFlag ");
                    strSqlControl.Append(" from ClientEleArea ");

                    arrySql.Add(strSql.ToString());
                    arrySql.Add(strSqlControl.ToString());

                    strSql = new StringBuilder();
                    strSqlControl = new StringBuilder();
                }

                idb.BatchUpdateWithTransaction(arrySql);
                d_log.OperateLog("ientEleArea", "", "", DateTime.Now, 1);

                return true;
            }
            catch
            {
                return false;
            }

        }

        public int GetMaxId()
        {
            return idb.GetMaxID("AreaID", "ClientEleArea");
        }

        public DataSet GetList(int Top, ZhiFang.Model.ClientEleArea model, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM ClientEleArea ");



            if (model.AreaID != 0)
            {
                strSql.Append(" and AreaID='" + model.AreaID + "' ");
            }


            if (model.AreaCName != null)
            {
                strSql.Append(" and AreaCName='" + model.AreaCName + "' ");
            }


            if (model.AreaShortName != null)
            {
                strSql.Append(" and AreaShortName='" + model.AreaShortName + "' ");
            }


            if (model.ClientNo != null)
            {
                strSql.Append(" and ClientNo='" + model.ClientNo + "' ");
            }

            strSql.Append(" order by " + filedOrder);
            return idb.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetAllList()
        {
            return GetList("");
        }

        #region IDGetListByTimeStampe 成员

        public DataSet GetListByTimeStampe(string LabCode, int dTimeStampe)
        {
            DataSet dsAll = new DataSet();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *,'" + LabCode + "' as LabCode,AreaID as LabAreaID from ClientEleArea where 1=1 ");
            if (dTimeStampe != -999999)
            {
                strSql.Append(" and convert(int,DTimeStampe) > '" + dTimeStampe + "' ");
            }
            DataTable dtServer = idb.ExecuteDataSet(strSql.ToString()).Tables[0];
            dtServer.TableName = "ServerDatas";

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("select *,LabAreaID as AreaID from B_Lab_ientEleArea where 1=1 ");
            if (LabCode.Trim() != "")
            {
                strSql2.Append(" and LabCode= '" + LabCode.Trim() + "' ");
            }
            if (dTimeStampe != -999999)
            {
                strSql2.Append(" and convert(int,DTimeStampe) > '" + dTimeStampe + "' ");
            }
            DataTable dtLab = idb.ExecuteDataSet(strSql2.ToString()).Tables[0];
            dtLab.TableName = "LabDatas";

            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("select * from ClientEleAreaControl where 1=1 ");
            if (LabCode.Trim() != "")
            {
                strSql3.Append(" and ControlLabNo= '" + LabCode.Trim() + "' ");
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

                        if (this.Exists(int.Parse(ds.Tables[0].Rows[i]["AreaID"].ToString().Trim())))
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
                strSql.Append("insert into ClientEleArea (");
                strSql.Append("AreaID,AreaCName,AreaShortName,ClientNo,");
                strSql.Append(") values (");

                if (dr.Table.Columns["AreaID"] != null && dr.Table.Columns["AreaID"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["AreaID"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["AreaCName"] != null && dr.Table.Columns["AreaCName"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["AreaCName"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["AreaShortName"] != null && dr.Table.Columns["AreaShortName"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["AreaShortName"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["ClientNo"] != null && dr.Table.Columns["ClientNo"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["ClientNo"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                int n = strSql.ToString().LastIndexOf(",");
                strSql.Remove(n, 1);
                strSql.Append(") ");
                return idb.ExecuteNonQuery(strSql.ToString());
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Digitlab8.ClientEleArea.AddByDataRow 同步数据时异常：", ex);
                return 0;
            }
        }
        public int UpdateByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update ClientEleArea set ");


                if (dr.Table.Columns["AreaCName"] != null && dr.Table.Columns["AreaCName"].ToString().Trim() != "")
                {
                    strSql.Append(" AreaCName = '" + dr["AreaCName"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["AreaShortName"] != null && dr.Table.Columns["AreaShortName"].ToString().Trim() != "")
                {
                    strSql.Append(" AreaShortName = '" + dr["AreaShortName"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["ClientNo"] != null && dr.Table.Columns["ClientNo"].ToString().Trim() != "")
                {
                    strSql.Append(" ClientNo = '" + dr["ClientNo"].ToString().Trim() + "' , ");
                }


                int n = strSql.ToString().LastIndexOf(",");
                strSql.Remove(n, 1);
                strSql.Append(" where AreaID='" + dr["AreaID"].ToString().Trim() + "' ");

                return idb.ExecuteNonQuery(strSql.ToString());
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Digitlab8.ClientEleArea .UpdateByDataRow同步数据时异常：", ex);
                return 0;
            }
        }




        #region IDBatchCopy 成员


        public int DeleteByDataRow(DataRow dr)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDClientEleArea 成员


        public DataSet GetPClientNoList(string sclientno)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT     dbo.ClientEleArea.ClientNo FROM  dbo.ClientEleArea INNER JOIN    dbo.CLIENTELE ON dbo.ClientEleArea.AreaID = dbo.CLIENTELE.AreaID  WHERE     (dbo.CLIENTELE.ClIENTNO=" + sclientno + ") ");

                return idb.ExecuteDataSet(strSql.ToString());
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Weblis.GetPClientNoList异常：", e);
                return null;
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

