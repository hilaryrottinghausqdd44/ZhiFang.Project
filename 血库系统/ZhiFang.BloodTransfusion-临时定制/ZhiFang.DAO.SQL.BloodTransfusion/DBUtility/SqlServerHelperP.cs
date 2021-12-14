using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZhiFang.Common.Public;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.IDAO.Base;

namespace ZhiFang.DAO.SQL.BloodTransfusion.DBUtility
{
    /// <summary>
    /// 数据访问类，可用于访问不同数据库
    /// </summary>
    public class SqlServerHelperP
    {
        public SqlServerHelperP()
        {
        }
        public SqlServerHelperP(string connectionString)
        {
            ConnectionString = connectionString;
        }
        //默认边试剂客户端的数据库
        public   string ConnectionString="";

        public  string IsSQLLog = "0";

        public  void WriteSQLLog(string logInfo)
        {
            if (IsSQLLog == "1")
                ZhiFang.Common.Log.Log.Info(logInfo);
        }
        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public  object GetSingle(string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }
        public  IDbConnection GetDataConnection()
        {
            
            DbConnection dbConnection = new SqlConnection(ConnectionString);
            dbConnection.Open();
            return dbConnection;
        }

        public  bool TestDataBaseConnection()
        {
            bool resultBool = false;
            try
            {
                
                using (DbConnection dbConnection = new SqlConnection(ConnectionString))
                {
                    dbConnection.Open();
                    resultBool = (dbConnection.State == ConnectionState.Open);
                    dbConnection.Close();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return resultBool;
        }

        public  DataSet QuerySql(string strSQL)
        {
            
            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                DataSet dataSet = new DataSet();
                try
                {
                    dbConnection.Open();
                    WriteSQLLog(strSQL);
                    SqlDataAdapter command = new SqlDataAdapter(strSQL, dbConnection);
                    command.Fill(dataSet);
                }
                catch (Exception ex)
                {
                    ZhiFang.Common.Log.Log.Info("SQL错误：" + strSQL + ": " + ex.Message);
                    //throw new Exception(ex.Message);
                }
                return dataSet;
            }
        }
        public DataSet SelectSql(string strSQL)
        {

            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                DataSet dataSet = new DataSet();
                try
                {
                    dbConnection.Open();
                    WriteSQLLog(strSQL);
                    SqlDataAdapter command = new SqlDataAdapter(strSQL, dbConnection);
                    command.Fill(dataSet);
                }
                catch (Exception ex)
                {
                    ZhiFang.Common.Log.Log.Info("SQL错误：" + strSQL + ": " + ex.Message);
                    //throw new Exception(ex.Message);
                }
                return dataSet;
            }
        }
        public  DataSet ObtainSql(string strSQL)
        {
            
            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                DataSet dataSet = new DataSet();
                try
                {
                    dbConnection.Open();
                    WriteSQLLog(strSQL);
                    SqlDataAdapter command = new SqlDataAdapter(strSQL, dbConnection);
                    command.Fill(dataSet);
                }
                catch (Exception ex)
                {
                    ZhiFang.Common.Log.Log.Info("SQL错误：" + strSQL + ": " + ex.Message);
                    //throw new Exception(ex.Message);
                }
                return dataSet;
            }
        }
        public  DataSet QuerySqlContainPrimaryKey(string strSQL)
        {
            
            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                DataSet dataSet = new DataSet();
                try
                {
                    dbConnection.Open();
                    WriteSQLLog(strSQL);
                    //SqlDataAdapter command = new SqlDataAdapter(strSQL, dbConnection);
                    SqlDataAdapter command = new SqlDataAdapter();
                    command.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    command.SelectCommand = new SqlCommand(strSQL, dbConnection);
                    command.Fill(dataSet);
                    //command.FillSchema(dataSet, SchemaType.Mapped);
                    //command.FillSchema(dataSet, SchemaType.Source);
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    ZhiFang.Common.Log.Log.Info("SQL错误：" + strSQL + ": " + ex.Message);
                    //throw new Exception(ex.Message);
                }
                return dataSet;
            }
        }

        public  bool QueryDataIsExistBySql(string strSQL)
        {
            bool resultBool = false;
            
            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                DataSet dataSet = new DataSet();
                try
                {
                    dbConnection.Open();
                    WriteSQLLog(strSQL);
                    SqlDataAdapter command = new SqlDataAdapter(strSQL, dbConnection);
                    command.Fill(dataSet);
                    resultBool = (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0);
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    ZhiFang.Common.Log.Log.Info("SQL错误：" + strSQL + ": " + ex.Message);
                    //throw new Exception(ex.Message);
                }
                return resultBool;
            }
        }

        public  bool ExecuteSql(string strSQL)
        {
            bool result = true;
            
            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(strSQL, dbConnection))
                {
                    try
                    {
                        dbConnection.Open();
                        WriteSQLLog(strSQL);
                        int rowCount = command.ExecuteNonQuery();
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        ZhiFang.Common.Log.Log.Info("SQL错误：" + strSQL + ": " + ex.Message);
                        result = false;
                    }
                }
            }
            return result;
        }

        public  bool ExecuteSql(string strSQL, Dictionary<string, object> listByteSQL)
        {
            
            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(strSQL, dbConnection))
                {
                    try
                    {
                        dbConnection.Open();
                        WriteSQLLog(strSQL);
                        foreach (KeyValuePair<string, object> kvp in listByteSQL)
                        {
                            command.Parameters.Add(kvp.Key, SqlDbType.Image).Value = kvp.Value;
                            //command.Parameters.Add(kvp.Key, kvp.Value);
                        }
                        int rowCount = command.ExecuteNonQuery();
                        return (rowCount > 0);
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        ZhiFang.Common.Log.Log.Info("SQL错误：" + strSQL + ": " + ex.Message);
                        //throw new Exception(ex.Message);
                        return false;
                    }
                }
            }
        }

        public  bool ExecuteSqlByPara(string strSQL, Dictionary<string, object> listPara)
        {
            
            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(strSQL, dbConnection))
                {
                    try
                    {
                        dbConnection.Open();
                        WriteSQLLog(strSQL);
                        foreach (KeyValuePair<string, object> kvp in listPara)
                        {
                            command.Parameters.AddWithValue(kvp.Key, kvp.Value);
                        }
                        int rowCount = command.ExecuteNonQuery();
                        return (rowCount > 0);
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        //throw new Exception(ex.Message);
                        return false;
                    }
                }
            }
        }

        public  bool ExecuteSqlBool(string strSQL)
        {
            
            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(strSQL, dbConnection))
                {
                    try
                    {
                        dbConnection.Open();
                        WriteSQLLog(strSQL);
                        int rowCount = command.ExecuteNonQuery();
                        return (rowCount > 0);
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        ZhiFang.Common.Log.Log.Info("SQL错误：" + strSQL + ": " + ex.Message);
                        //throw new Exception(ex.Message);
                        return false;
                    }
                }
            }
        }

        public  bool ExecuteSqlList(List<string> listSQL)
        {
            bool resultBool = false;
            
            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();
                if (dbConnection.State == ConnectionState.Open)
                {
                    SqlTransaction transaction;
                    transaction = dbConnection.BeginTransaction();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = dbConnection;
                        command.Transaction = transaction;
                        string errorSql = "";
                        try
                        {
                            foreach (string strSQL in listSQL)
                            {
                                command.CommandText = strSQL;
                                WriteSQLLog(strSQL);
                                errorSql = strSQL;
                                command.ExecuteNonQuery();
                            }
                            transaction.Commit();
                            resultBool = true;
                        }
                        catch (System.Data.SqlClient.SqlException ex)
                        {
                            ZhiFang.Common.Log.Log.Info("事务提交失败：" + errorSql + " Error:" + ex.Message);
                            transaction.Rollback();
                            //throw new Exception(ex.Message);
                        }
                    }
                }
            }
            return resultBool;
        }//

        public  bool ExecuteSqlList(List<string> listSQL, List<Dictionary<string, byte[]>> listByteSQL)
        {
            bool resultBool = false;
            
            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();
                if (dbConnection.State == ConnectionState.Open)
                {
                    SqlTransaction transaction;
                    transaction = dbConnection.BeginTransaction();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = dbConnection;
                        command.Transaction = transaction;
                        string errorSql = "";
                        try
                        {
                            foreach (string strSQL in listSQL)
                            {
                                command.CommandText = strSQL;
                                WriteSQLLog(strSQL);
                                errorSql = strSQL;
                                command.ExecuteNonQuery();
                            }
                            transaction.Commit();
                            resultBool = true;
                        }
                        catch (System.Data.SqlClient.SqlException ex)
                        {
                            ZhiFang.Common.Log.Log.Info("事务提交失败：" + errorSql + " Error:" + ex.Message);
                            transaction.Rollback();
                            //throw new Exception(ex.Message);
                        }
                    }
                }
            }
            return resultBool;
        }//

        public  DataSet ExecuteProcedure(string storedProcName, IDataParameter[] parameters)
        {
            
            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();
                DataSet dataSet = new DataSet();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(dbConnection, storedProcName, parameters);
                sqlDA.Fill(dataSet);
                dbConnection.Close();
                return dataSet;
            }
        }

        public  DataSet ExecuteProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
        {
            
            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                DataSet dataSet = new DataSet();
                dbConnection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(dbConnection, storedProcName, parameters);
                sqlDA.Fill(dataSet, tableName);
                dbConnection.Close();
                return dataSet;
            }
        }
        /// <summary>
        /// 构建 SqlCommand 对象(用来返回一个结果集，而不是一个整数值)
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand</returns>
        private  SqlCommand BuildQueryCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter parameter in parameters)
            {
                if (parameter != null)
                {
                    // 检查未分配值的输出参数,将其分配以DBNull.Value.
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    command.Parameters.Add(parameter);
                }
            }
            return command;
        }
    }
}
