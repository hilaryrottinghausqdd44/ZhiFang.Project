using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.Oracle.weblis
{
    //D_LabDownLoadInfo

    public partial class D_LabDownLoadInfo :BaseDALLisDB, IDLabDownLoadInfo
    {
        public D_LabDownLoadInfo(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public D_LabDownLoadInfo()
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
        }
   
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.LabDownLoadInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.TableName != null)
            {
                strSql1.Append("TableName,");
                strSql2.Append("'" + model.TableName + "',");
            }
            if (model.ServerTime != null)
            {
                strSql1.Append("ServerTime,");
                strSql2.Append("" + model.ServerTime + ",");
            }
            if (model.LocalTime != null)
            {
                strSql1.Append("LocalTime,");
                strSql2.Append("'" + model.LocalTime + "',");
            }
            if (model.Status != null)
            {
                strSql1.Append("Status,");
                strSql2.Append("" + model.Status + ",");
            }
            if (model.DownLoadCount != null)
            {
                strSql1.Append("DownLoadCount,");
                strSql2.Append("" + model.DownLoadCount + ",");
            }
            if (model.MsgRemark != null)
            {
                strSql1.Append("MsgRemark,");
                strSql2.Append("'" + model.MsgRemark + "',");
            }
                strSql1.Append("DTimeStampe,");
                strSql2.Append("sysdate+ '1.1234',");
            
            if (model.AddTime != null)
            {
                strSql1.Append("AddTime,");
                strSql2.Append(" to_date('" + model.AddTime.ToString() + "','YYYY-MM-DD HH24:MI:SS'),");
            }
            if (model.UseFlag != null)
            {
                strSql1.Append("UseFlag,");
                strSql2.Append("" + model.UseFlag + ",");
            }
            strSql.Append("insert into D_LabDownLoadInfo(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());

            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("insert into D_LabDownLoadInfo(");
            //strSql.Append("UseFlag,TableName,ServerTime,LocalTime,Status,DownLoadCount,MsgRemark");
            //strSql.Append(") values (");
            //strSql.Append("@UseFlag,@TableName,@ServerTime,@LocalTime,@Status,@DownLoadCount,@MsgRemark");
            //strSql.Append(") ");

            //SqlParameter[] parameters = {
            //            new SqlParameter("@UseFlag", SqlDbType.Int,4) ,            
            //            new SqlParameter("@TableName", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@ServerTime", SqlDbType.Int) ,            
            //            new SqlParameter("@LocalTime", SqlDbType.DateTime) ,            
            //            new SqlParameter("@Status", SqlDbType.Int,4) ,            
            //            new SqlParameter("@DownLoadCount", SqlDbType.Int,4) ,            
            //            new SqlParameter("@MsgRemark", SqlDbType.VarChar,500) ,            
              
            //};

            //parameters[0].Value = model.UseFlag;
            //parameters[1].Value = model.TableName;
            //parameters[2].Value = model.ServerTime;
            //parameters[3].Value = model.LocalTime;
            //parameters[4].Value = model.Status;
            //parameters[5].Value = model.DownLoadCount;
            //parameters[6].Value = model.MsgRemark;
            //return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.LabDownLoadInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update D_LabDownLoadInfo set ");
            if (model.ServerTime != null)
            {
                strSql.Append("ServerTime=" + model.ServerTime + ",");
            }
            else
            {
                strSql.Append("ServerTime= null ,");
            }
            if (model.LocalTime != null)
            {
                strSql.Append("LocalTime='" + model.LocalTime + "',");
            }
            else
            {
                strSql.Append("LocalTime= null ,");
            }
            if (model.Status != null)
            {
                strSql.Append("Status=" + model.Status + ",");
            }
            else
            {
                strSql.Append("Status= null ,");
            }
            if (model.DownLoadCount != null)
            {
                strSql.Append("DownLoadCount=" + model.DownLoadCount + ",");
            }
            else
            {
                strSql.Append("DownLoadCount= null ,");
            }
            if (model.MsgRemark != null)
            {
                strSql.Append("MsgRemark='" + model.MsgRemark + "',");
            }
            else
            {
                strSql.Append("MsgRemark= null ,");
            }
            if (model.AddTime != null)
            {
                strSql.Append(" AddTime=to_date('" + model.AddTime.ToString() + "','YYYY-MM-DD HH24:MI:SS'),");
            }
            if (model.UseFlag != null)
            {
                strSql.Append("UseFlag=" + model.UseFlag + ",");
            }
            else
            {
                strSql.Append("UseFlag= null ,");
            }
            strSql.Append("DTimeStampe = sysdate+ '1.1234',");

            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Id=" + model.Id + "");

            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());

            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("update D_LabDownLoadInfo set ");
            //strSql.Append(" UseFlag = @UseFlag , ");
            //strSql.Append(" ServerTime = @ServerTime , ");
            //strSql.Append(" LocalTime = @LocalTime , ");
            //strSql.Append(" Status = @Status , ");
            //strSql.Append(" DownLoadCount = @DownLoadCount , ");
            //strSql.Append(" MsgRemark = @MsgRemark ");
            //strSql.Append(" where TableName=@TableName  ");

            //SqlParameter[] parameters = {
            //new SqlParameter("@UseFlag", SqlDbType.Int,4) ,  
            //new SqlParameter("@TableName", SqlDbType.VarChar,50) ,   
            //new SqlParameter("@ServerTime", SqlDbType.Int) ,    
            //new SqlParameter("@LocalTime", SqlDbType.DateTime) ,     
            //new SqlParameter("@Status", SqlDbType.Int,4) ,            
            //new SqlParameter("@DownLoadCount", SqlDbType.Int,4) ,     
            //new SqlParameter("@MsgRemark", SqlDbType.VarChar,500) , 
            //};
            //if (model.UseFlag != null)
            //{
            //    parameters[0].Value = model.UseFlag;
            //}
            //if (model.TableName != null)
            //{
            //    parameters[1].Value = model.TableName;
            //}
            //if (model.ServerTime != null)
            //{
            //    parameters[2].Value = model.ServerTime;
            //}
            //if (model.LocalTime != null)
            //{
            //    parameters[3].Value = model.LocalTime;
            //}
            //if (model.Status != null)
            //{
            //    parameters[4].Value = model.Status;
            //}
            //if (model.DownLoadCount != null)
            //{
            //    parameters[5].Value = model.DownLoadCount;
            //}
            //if (model.MsgRemark != null)
            //{
            //    parameters[6].Value = model.MsgRemark;
            //}
            //return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string TableName)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from D_LabDownLoadInfo ");
            strSql.Append(" where TableName=@TableName ");
            SqlParameter[] parameters = {
					new SqlParameter("@TableName", SqlDbType.VarChar,50)};
            parameters[0].Value = TableName;


            return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);

        }



        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.LabDownLoadInfo GetModel(string TableName)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, UseFlag, TableName, ServerTime, LocalTime, Status, DownLoadCount, MsgRemark, DTimeStampe, AddTime  ");
            strSql.Append("  from D_LabDownLoadInfo ");
            strSql.Append(" where TableName=@TableName ");
            SqlParameter[] parameters = {
					new SqlParameter("@TableName", SqlDbType.VarChar,50)};
            parameters[0].Value = TableName;


            ZhiFang.Model.LabDownLoadInfo model = new ZhiFang.Model.LabDownLoadInfo();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UseFlag"].ToString() != "")
                {
                    model.UseFlag = int.Parse(ds.Tables[0].Rows[0]["UseFlag"].ToString());
                }
                model.TableName = ds.Tables[0].Rows[0]["TableName"].ToString();
                if (ds.Tables[0].Rows[0]["ServerTime"].ToString() != "")
                {
                    model.ServerTime = int.Parse(ds.Tables[0].Rows[0]["ServerTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LocalTime"].ToString() != "")
                {
                    model.LocalTime = DateTime.Parse(ds.Tables[0].Rows[0]["LocalTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Status"].ToString() != "")
                {
                    model.Status = int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DownLoadCount"].ToString() != "")
                {
                    model.DownLoadCount = int.Parse(ds.Tables[0].Rows[0]["DownLoadCount"].ToString());
                }
                model.MsgRemark = ds.Tables[0].Rows[0]["MsgRemark"].ToString();
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
            strSql.Append(" FROM D_LabDownLoadInfo ");
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
            strSql.Append(" * ");
            strSql.Append(" FROM D_LabDownLoadInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
                strSql.Append(" and ROWNUM <= '" + Top + "'");
            }
            else
            {
                strSql.Append(" where ROWNUM <= '" + Top + "'");
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 根据实体获取DataSet
        /// </summary>
        public DataSet GetList(ZhiFang.Model.LabDownLoadInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM D_LabDownLoadInfo where 1=1 ");

            if (model.TableName != null && model.TableName.Trim() != "")
            {
                strSql.Append(" and TableName='" + model.TableName + "' ");
            }

            if (model.ServerTime != null)
            {
                strSql.Append(" and ServerTime='" + model.ServerTime + "' ");
            }

            if (model.LocalTime != null)
            {
                strSql.Append(" and LocalTime='" + model.LocalTime + "' ");
            }

            if (model.Status != null)
            {
                if (model.Status == 2)
                {
                    strSql.Append(" and ( Status=0 or Status=1 ) ");
                }
                else
                {
                    strSql.Append(" and Status=" + model.Status + " ");
                }
            }

            if (model.DownLoadCount != null)
            {
                strSql.Append(" and DownLoadCount=" + model.DownLoadCount + " ");
            }

            if (model.MsgRemark != null && model.MsgRemark.Trim() != "")
            {
                strSql.Append(" and MsgRemark='" + model.MsgRemark + "' ");
            }

            if (model.DTimeStampe != null)
            {
                strSql.Append(" and DTimeStampe='" + model.DTimeStampe + "' ");
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 获取总记录
        /// </summary>
        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM D_LabDownLoadInfo ");
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
        public int GetTotalCount(ZhiFang.Model.LabDownLoadInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM D_LabDownLoadInfo where 1=1 ");

            if (model.UseFlag != null)
            {
                strSql.Append(" and UseFlag=" + model.UseFlag + " ");
            }

            if (model.TableName != null)
            {
                strSql.Append(" and TableName='" + model.TableName + "' ");
            }

            if (model.ServerTime != null)
            {
                strSql.Append(" and ServerTime='" + model.ServerTime + "' ");
            }

            if (model.LocalTime != null)
            {
                strSql.Append(" and LocalTime='" + model.LocalTime + "' ");
            }

            if (model.Status != null)
            {
                strSql.Append(" and Status=" + model.Status + " ");
            }

            if (model.DownLoadCount != null)
            {
                strSql.Append(" and DownLoadCount=" + model.DownLoadCount + " ");
            }

            if (model.MsgRemark != null)
            {
                strSql.Append(" and MsgRemark='" + model.MsgRemark + "' ");
            }

            if (model.DTimeStampe != null)
            {
                strSql.Append(" and DTimeStampe='" + model.DTimeStampe + "' ");
            }

            if (model.AddTime != null)
            {
                strSql.Append(" and AddTime=to_date('" + model.AddTime.ToString() + "','YYYY-MM-DD HH24:MI:SS'),");
             
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
        public DataSet GetListByPage(ZhiFang.Model.LabDownLoadInfo model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            if (model != null && model.LabCode != null)
            {
                strSql.Append(" select   * from D_LabDownLoadInfo left join D_LabDownLoadInfoControl on D_LabDownLoadInfo.TableName=D_LabDownLoadInfoControl.TableName ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and D_LabDownLoadInfoControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append("where  ROWNUM <= '" + nowPageSize + "' and TableName not in ( ");
                strSql.Append("select  TableName from  D_LabDownLoadInfo left join D_LabDownLoadInfoControl on D_LabDownLoadInfo.TableName=D_LabDownLoadInfoControl.TableName where 1=1 ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and D_LabDownLoadInfoControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append(" and ROWNUM <= '" + (nowPageSize * nowPageNum) + "' ) order by D_LabDownLoadInfo.TableName ");
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
            else
            {
                strSql.Append("select   * from D_LabDownLoadInfo where  ROWNUM <= '" + nowPageSize + "' and TableName not in  ");
                strSql.Append("(select TableName from D_LabDownLoadInfo where  1=1 and ROWNUM <= '" + (nowPageSize * nowPageNum) + "' ) order by TableName  ");
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
        }

        public bool Exists(string TableName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from D_LabDownLoadInfo ");
            strSql.Append(" where TableName ='" + TableName + "'");
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
        
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("TableName", "D_LabDownLoadInfo");
        }

        public DataSet GetList(int Top, ZhiFang.Model.LabDownLoadInfo model, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
     
            strSql.Append(" * ");
            strSql.Append(" FROM D_LabDownLoadInfo ");


            if (model.UseFlag != null)
            {
                strSql.Append(" and UseFlag=" + model.UseFlag + " ");
            }

            if (model.TableName != null)
            {

                strSql.Append(" and TableName='" + model.TableName + "' ");
            }

            if (model.ServerTime != null)
            {

                strSql.Append(" and ServerTime='" + model.ServerTime + "' ");
            }

            if (model.LocalTime != null)
            {

                strSql.Append(" and LocalTime='" + model.LocalTime + "' ");
            }

            if (model.Status != null)
            {
                strSql.Append(" and Status=" + model.Status + " ");
            }

            if (model.DownLoadCount != null)
            {
                strSql.Append(" and DownLoadCount=" + model.DownLoadCount + " ");
            }

            if (model.MsgRemark != null)
            {

                strSql.Append(" and MsgRemark='" + model.MsgRemark + "' ");
            }

            if (model.DTimeStampe != null)
            {

                strSql.Append(" and DTimeStampe='" + model.DTimeStampe + "' ");
            }

            if (model.AddTime != null)
            {
               strSql.Append(" and AddTime=to_date('" + model.AddTime.ToString() + "','YYYY-MM-DD HH24:MI:SS'),");
            }
           
                strSql.Append(" and ROWNUM <= '" + Top + "'");
           
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }




        #region IDLabDownLoadInfo 成员


        public string GetMaxDTimeStampe()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select max(convert(int,ServerTime)) as DTimeStampe from D_LabDownLoadInfo");
            return DbHelperSQL.ExecuteScalar(strSql.ToString());
        }

        #endregion

        #region IDataBase<LabDownLoadInfo> 成员


        public DataSet GetAllList()
        {
            return GetList("");
        }

        #endregion

        #region IDataBase<LabDownLoadInfo> 成员
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
                        if (this.Exists(ds.Tables[0].Rows[i]["TableName"].ToString().Trim()))
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
                strSql.Append("insert into D_LabDownLoadInfo (");
                strSql.Append("UseFlag,TableName,ServerTime,LocalTime,Status,DownLoadCount,MsgRemark,");
                strSql.Append(") values (");
                if (dr.Table.Columns["UseFlag"] != null && dr.Table.Columns["UseFlag"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["UseFlag"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["TableName"] != null && dr.Table.Columns["TableName"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["TableName"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["ServerTime"] != null && dr.Table.Columns["ServerTime"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["ServerTime"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["LocalTime"] != null && dr.Table.Columns["LocalTime"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + DateTime.Parse(dr["LocalTime"].ToString().Trim()) + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["Status"] != null && dr.Table.Columns["Status"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["Status"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["DownLoadCount"] != null && dr.Table.Columns["DownLoadCount"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["DownLoadCount"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["MsgRemark"] != null && dr.Table.Columns["MsgRemark"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["MsgRemark"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                strSql.Append(") ");
                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.D_LabDownLoadInfo.AddByDataRow 同步数据时异常：", ex);
                return 0;
            }
        }
        public int UpdateByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update D_LabDownLoadInfo set ");

                if (dr.Table.Columns["UseFlag"] != null && dr.Table.Columns["UseFlag"].ToString().Trim() != "")
                {
                    strSql.Append(" UseFlag = '" + dr["UseFlag"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["ServerTime"] != null && dr.Table.Columns["ServerTime"].ToString().Trim() != "")
                {
                    strSql.Append(" ServerTime = '" + dr["ServerTime"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["LocalTime"] != null && dr.Table.Columns["LocalTime"].ToString().Trim() != "")
                {
                    strSql.Append(" LocalTime = '" + DateTime.Parse(dr["LocalTime"].ToString().Trim()) + "' , ");
                }

                if (dr.Table.Columns["Status"] != null && dr.Table.Columns["Status"].ToString().Trim() != "")
                {
                    strSql.Append(" Status = '" + dr["Status"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["DownLoadCount"] != null && dr.Table.Columns["DownLoadCount"].ToString().Trim() != "")
                {
                    strSql.Append(" DownLoadCount = '" + dr["DownLoadCount"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["MsgRemark"] != null && dr.Table.Columns["MsgRemark"].ToString().Trim() != "")
                {
                    strSql.Append(" MsgRemark = '" + dr["MsgRemark"].ToString().Trim() + "' , ");
                }

                int n = strSql.ToString().LastIndexOf(",");
                strSql.Remove(n, 1);
                strSql.Append(" where TableName='" + dr["TableName"].ToString().Trim() + "' ");

                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.D_LabDownLoadInfo .UpdateByDataRow同步数据时异常：", ex);
                return 0;
            }
        }
        #endregion
    }
}

