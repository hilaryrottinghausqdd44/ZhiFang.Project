using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhiFang.DAO.MSSQL
{
    public class SqlServerHelper
    {
        #region Private Instance Fields

        private SqlConnection DBConn;
        private SqlTransaction DBTran;
        private bool inTransaction;
        private string ConnectionStr;

        #endregion

        #region Public Instance Constructors

        public SqlServerHelper()
        {
            string connStr = GetADODataBaseSettings(ZhiFang.Common.Public.ConfigHelper.GetDataBaseSettings("databaseSettings", "db.connectionString"));
            this.ConnectionStr = connStr;
            DBConn = new SqlConnection(connStr);
        }
        public SqlServerHelper(string connName)
        {
            if (string.IsNullOrEmpty(connName))
            {
                string connStr = GetADODataBaseSettings(ZhiFang.Common.Public.ConfigHelper.GetDataBaseSettings("databaseSettings", "db.connectionString"));
                this.ConnectionStr = connStr;
                DBConn = new SqlConnection(connStr);
            }
            else
            {
                ConnectionStringSettings cc = ConfigurationManager.ConnectionStrings[connName];
                if (cc == null)
                {
                    throw new Exception("config文件connectionStrings节点中不存在数据连接名称" + connName);
                }
                string connStr = cc.ConnectionString;
                this.ConnectionStr = connStr;
                DBConn = new SqlConnection(connStr);
            }
        }

        /// <summary>
        /// 根据databaseSettings数据库链接配置，得到ADO数据链接字符串
        /// </summary>
        /// <param name="connectString"></param>
        /// <returns></returns>
        private string GetADODataBaseSettings(string connectString)
        {
            string result = "";
            if (!string.IsNullOrEmpty(connectString))
            {
                string[] strList = connectString.Split(';');
                foreach (string tempStr in strList)
                {
                    if (!string.IsNullOrEmpty(tempStr))
                    {
                        string[] strArray = tempStr.Split('=');
                        if (strArray[0].ToUpper() == "SERVER")
                            result += "data source=" + tempStr.Remove(0, tempStr.IndexOf("=") + 1) + ";";
                        else if (strArray[0].ToUpper() == "DATABASE")
                            result += "initial catalog=" + tempStr.Remove(0, tempStr.IndexOf("=") + 1) + ";";
                        else if (strArray[0].ToUpper() == "UID")
                            result += "user id=" + tempStr.Remove(0, tempStr.IndexOf("=") + 1) + ";";
                        else if (strArray[0].ToUpper() == "PWD")
                            result += "password=" + tempStr.Remove(0, tempStr.IndexOf("=") + 1) + ";";
                    }
                }
            }
            return result;
        }

        #endregion

        #region Public Instance Properties

        public  string ConnectionString
        {
            get
            {
                return this.ConnectionStr;
            }
        }

        public  ConnectionState ConnectionState
        {
            get
            {
                return DBConn.State;
            }
        }

        public SqlConnection Connection
        {
            get
            {
                return DBConn;
            }
        }

        #endregion

        #region Open and Close DataBase

        protected void Open()
        {
            try
            {
                if (DBConn.State != ConnectionState.Open)
                {
                    DBConn.Open();
                }
            }
            catch (Exception ex)
            {
                throw new SQLErrorException("打开数据库连接失败，连接字符串" + ConnectionStr + ",错误：" + ex);
            }
        }

        protected void Close()
        {
            try
            {
                if (DBConn.State != ConnectionState.Closed)
                {
                    DBConn.Close();
                }
            }
            catch (Exception ex)
            {
                throw new SQLErrorException("关闭数据库连接失败," + ex);
            }
        }

        #endregion

        #region Test DataBase Connection

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Message"></param>
        /// <returns></returns>
        public  bool TestConnection(out string strErr)
        {
            strErr = string.Empty;
            try
            {
                if (DBConn.State != ConnectionState.Open)
                {
                    DBConn.Open();
                }
                return true;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return false;
            }
            finally
            {
                if (DBConn.State != ConnectionState.Closed)
                {
                    DBConn.Close();
                }
            }
        }

        /// <summary>
        /// 测试数据库连接,true:连接成功；false：连接失败并引发DBErrorException异常
        /// </summary>
        /// <returns></returns>
        public  bool TestConnection()
        {
            try
            {
                if (DBConn.State != ConnectionState.Open)
                {
                    DBConn.Open();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new DBErrorException("SQLHelper.cs->TestConnection()->数据库连接失败，连接参数为" + DBConn.ConnectionString + ",错误信息：" + ex.Message, ex);
            }
            finally
            {
                if (DBConn.State != ConnectionState.Closed)
                {
                    DBConn.Close();
                }
            }
        }

        #endregion

        #region Use Transaction

        public  void BeginTransaction()
        {
            try
            {
                DBTran = DBConn.BeginTransaction();
                inTransaction = true;
            }
            catch (Exception ex)
            {
                inTransaction = false;
                throw new SQLErrorException("开始事务失败," + ex);
            }
        }

        public  void CommitTransaction()
        {
            try
            {
                if (inTransaction)
                {
                    DBTran.Commit();
                    inTransaction = false;
                }
            }
            catch (Exception ex)
            {
                inTransaction = false;
                throw new SQLErrorException("提交事务失败," + ex);
            }
        }

        public  void RollbackTransaction()
        {
            try
            {
                if (inTransaction)
                {
                    DBTran.Rollback();
                    inTransaction = false;
                }
            }
            catch (Exception ex)
            {
                inTransaction = false;
                throw new SQLErrorException("回滚事务失败," + ex);
            }
        }

        #endregion

        #region 执行Sql，返回DataReader

        public  IDataReader ExecuteReader(string strSql)
        {
            IDataReader myReader = null;
            try
            {
                SqlCommand cmd = new SqlCommand(strSql, DBConn);
                this.Open();
                myReader = (IDataReader)cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw new SQLErrorException("SQLHelper.cs->ExecuteReader()->执行sql语句错误，Sql语句：" + strSql + ",错误：" + ex.Message, ex);
            }
            finally
            {
                this.Close();
            }
            return myReader;
        }

        #endregion

        #region 执行Sql，返回DataSet

        /// <summary>
        /// 执行SQL语句，返回DataSet对象,引发SQLErrorException异常
        /// </summary>
        /// <param name="strSql">要执行的SQL语句格式:"select * from where Column1='{0}' and Column2='{1}'</param>
        /// <returns></returns>
        public  DataSet ExecuteDataSet(string strSql)
        {
            DataSet ds = new DataSet();
            try
            {
                this.Open();
                SqlDataAdapter command = new SqlDataAdapter(strSql, DBConn);
                command.Fill(ds, "ds");
            }
            catch (Exception ex)
            {
                throw new SQLErrorException("SQLHelper.cs->ExecuteDataSet()->执行sql语句错误，Sql语句：" + strSql + ",错误：" + ex.Message, ex);
            }
            finally
            {
                this.Close();
            }
            return ds;
        }

        #endregion

        #region 执行Sql，返回影响行数

        /// <summary>
        /// 执行SQL语句，返回影响的行数，主要针对Insert，Update，Delete三种操作
        /// </summary>
        /// <param name="strSql">要执行的SQL语句格式:"select * from where Column1='{0}' and Column2='{1}'"</param>
        /// <returns>返回影响的行数</returns>
        public  int ExecuteNonQuery(string strSql)
        {
            int rowNum = 0;
            try
            {
                this.Open();
                SqlCommand cmd = new SqlCommand(strSql, DBConn);
                rowNum = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new SQLErrorException("SQLHelper.cs->ExecCount()->执行sql语句错误，Sql语句：" + strSql + ",错误：" + ex.Message, ex);
            }
            finally
            {
                this.Close();
            }
            return rowNum;
        }

        #endregion

        #region 返回数据库表的记录数

        /// <summary>
        /// 返回数据库表的记录数
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="whereClause">查询条件,格式：column1>1 and column2=111 </param>
        /// <returns>记录数</returns>
        public  int ExecCount(string tableName, string whereClause)
        {
            int i = 0;
            if (whereClause.Trim().Length != 0)
            {
                whereClause = " where " + whereClause;
            }
            string strSql = "select count(*) from " + tableName + whereClause;
            try
            {
                this.Open();
                SqlCommand cmd = new SqlCommand(strSql, DBConn);
                object obj = cmd.ExecuteScalar();
                if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                {
                    return 0;
                }
                else
                {
                    i = (int)obj;
                }
            }
            catch (Exception ex)
            {
                throw new SQLErrorException("SQLHelper.cs->ExecuteDataSet()->执行sql语句错误，Sql语句：" + strSql + ",错误：" + ex.Message, ex);
            }
            finally
            {
                this.Close();
            }
            return i;

        }

        #endregion

        #region 返回查询语句的第一行第一列

        /// <summary>
        /// 返回查询语句的第一行第一列
        /// </summary>
        /// <param name="SQL">要执行的SQL语句格式:"update table where Column1='{0}' and Column2='{1}'"</param>
        /// <returns>返回第一行第一列</returns>
        public  string ExecuteScalar(string strSql)
        {
            DataSet ds = new DataSet();
            try
            {
                this.Open();
                SqlCommand cmd = new SqlCommand(strSql, DBConn);
                object obj = cmd.ExecuteScalar();
                if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                {
                    return null;
                }
                else
                {
                    return obj.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new SQLErrorException("SQLHelper.cs->ExecuteScalar()->执行sql语句错误，Sql语句：" + strSql + ",错误：" + ex.Message, ex);
            }
            finally
            {
                this.Close();
            }
        }
        #endregion

        #region 获取数据库表结构信息

        /// <summary>
        /// 获取数据库表结构信息
        /// </summary>
        /// <param name="strSql">查询第一条记录</param>
        /// <returns>包含表结构信息的数据集</returns>
        public  DataSet GetDataTableSchema(string strSql)
        {
            if (strSql == null)
            {
                return null;
            }
            if (strSql.Trim().Equals(""))
            {
                return null;
            }
            DataSet ds = new DataSet();
            try
            {
                Open();
                SqlDataAdapter SQLAdapter = new SqlDataAdapter(strSql, DBConn);
                SQLAdapter.FillSchema(ds, SchemaType.Mapped);
            }
            catch (Exception ex)
            {
                throw new SQLErrorException("SQLHelper.cs->GetDataTableSchema()->获取表结构信息出错，Sql语句：" + strSql + ",错误：" + ex.Message, ex);
            }
            finally
            {
                Close();
            }
            return ds;
        }

        #endregion

        #region 分页返回数据集

        /// <summary>
        /// 执行SQL语句，返回指定行数的数据集
        /// </summary>
        /// <param name="strSql">要执行的SQL语句，格式:"select * from where Column1='{0}' and Column2='{1}'"</param>
        /// <param name="nowPageNum"></param>
        /// <param name="nowPageSize"></param>
        /// <param name="realRowNum"></param>
        /// <returns></returns>
        public  DataSet ExecSQLToMultiPages(string strSql, int nowPageNum, int nowPageSize, out int realRowNum)
        {

            DataSet ds = null;

            if (nowPageNum < 1)
            {
                nowPageNum = 1;
            }
            realRowNum = 0;
            try
            {
                Open();
                SqlDataAdapter SQLAdapter = new SqlDataAdapter(strSql, DBConn);
                ds = new DataSet();

                //得到成功添加到DataSet中的行数
                realRowNum = SQLAdapter.Fill(ds);

                if (realRowNum < (nowPageNum * nowPageSize))
                {
                    nowPageNum = Convert.ToInt32((realRowNum - 1) / nowPageSize) + 1;
                }

                ds = new DataSet();
                //填充指定行数的数据集到DataSet
                SQLAdapter.Fill(ds, (nowPageNum - 1) * nowPageSize, nowPageSize, "Table1");

            }
            catch (Exception ex)
            {
                throw new SQLErrorException("SQLHelper.cs->ExecSQLToMultiPages()->分页获取数据集出错，Sql语句：" + strSql + ",错误：" + ex.Message, ex);
            }
            finally
            {
                Close();
            }

            return ds;
        }

        #endregion

        #region 批量执行Sql语句

        /// <summary>
        /// 
        /// </summary>
        /// <param name="alSQL"></param>
        public  void BatchUpdateWithoutTransaction(System.Collections.ArrayList alSQL)
        {
            if (alSQL == null)
            {
                return;
            }
            try
            {
                this.Open();

                SqlCommand SQLComm = new SqlCommand();
                SQLComm.Connection = DBConn;

                for (int i = 0; i < alSQL.Count; i++)
                {
                    try
                    {
                        SQLComm.CommandText = alSQL[i].ToString();
                        SQLComm.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new SQLErrorException("SQLHelper.cs->BatchUpdateWithoutTransaction()->批量执行Sql出错：" + ex.Message, ex);
                    }
                }
            }
            finally
            {
                this.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="alSQL"></param>
        public  void BatchUpdateWithTransaction(System.Collections.ArrayList alSQL)
        {
            if (alSQL == null)
            {
                return;
            }

            this.Open();
            this.BeginTransaction();

            SqlCommand SQLComm = new SqlCommand();
            SQLComm.Connection = DBConn;
            SQLComm.Transaction = DBTran;

            for (int i = 0; i < alSQL.Count; i++)
            {
                try
                {
                    SQLComm.CommandText = alSQL[i].ToString();
                    SQLComm.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    this.RollbackTransaction();
                    this.Close();
                    throw new SQLErrorException("SQLHelper.cs->BatchUpdateWithTransaction()->批量执行Sql出错：" + ex.Message, ex);

                }
            }
            this.CommitTransaction();
            this.Close();
        }

        #endregion

        #region 执行存储过程

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SQLStr"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public  int ExecStoredProcedure(string cmdText, params DbParameter[] cmdParms)
        {
            this.Open();
            int i;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.DBConn;

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = cmdText;
                if (cmdParms != null)
                {
                    foreach (SqlParameter parm in cmdParms)
                    {
                        cmd.Parameters.Add(parm);
                    }
                }
                i = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new SQLErrorException("SQLHelper.cs->ExecStoredProcedure()->执行存储过程错误，名称：" + cmdText + ",错误：" + ex.Message, ex);
            }
            finally
            {
                this.Close();
            }
            return 0;
        }

        /// <summary>
        /// 执行存储过程，并返回结果
        /// OleDbParameter[] para = new OleDbParameter[14];
        /// para[0] = new OleDbParameter("@parameter1", OleDbType.VarChar, 50);
        /// para[0].Direction = ParameterDirection.Input;
        /// para[0].Value ="";
        /// </summary>
        /// <param name="cmdText">存储过程名称</param>
        /// <param name="commandParameters">参数列表</param>
        /// <returns>返回的数据集</returns>
        public  DataSet ExecDataSetStoredProcedure(string cmdText, DbParameter[] cmdParms)
        {
            this.Open();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.DBConn;

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = cmdText;
                if (cmdParms != null)
                {
                    foreach (SqlParameter parm in cmdParms)
                    {
                        cmd.Parameters.Add(parm);
                    }
                }

                SqlDataAdapter command = new SqlDataAdapter(cmd);
                command.Fill(ds, "ds");
            }
            catch (Exception ex)
            {
                throw new SQLErrorException("SQLHelper.cs->ExecDataSetStoredProcedure()->执行存储过程错误，名称：" + cmdText + ",错误：" + ex.Message, ex);
            }
            finally
            {
                this.Close();
            }
            return ds;
        }

        #endregion


    }
    /// <summary>
    /// Sql语句执行错误
    /// </summary>
    [global::System.Serializable]
    public class SQLErrorException : ApplicationException
    {
        public SQLErrorException()
        {

        }
        public SQLErrorException(string message) : base(message)
        {

        }
        public SQLErrorException(string message, Exception inner) : base(message, inner)
        {

        }
        protected SQLErrorException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {

        }

    }
    [global::System.Serializable]
    public class DBErrorException : ApplicationException
    {
        public DBErrorException() { }
        public DBErrorException(string message)
            : base(message)
        {

        }
        public DBErrorException(string message, Exception inner)
            : base(message, inner)
        {

        }
        protected DBErrorException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {

        }

    }
}
