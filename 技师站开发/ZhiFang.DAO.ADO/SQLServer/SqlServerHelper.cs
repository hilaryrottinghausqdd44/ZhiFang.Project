using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Runtime.Serialization;


namespace ZhiFang.LabStar.DAO.ADO
{
    public class SqlServerHelper
    {
        public static string IsSQLLog = "1";

        public static string DigitlabConnectStr = ZhiFang.Common.Public.ConfigHelper.GetDataConnectionString("Digitlab_ConnectStr");
        public static string LabStarGraphConnectStr = ZhiFang.Common.Public.ConfigHelper.GetDataConnectionString("LabStarGraph_ConnectStr");

        public static void WriteSQLLog(string logInfo)
        {
            if (IsSQLLog == "1")
                ZhiFang.LabStar.Common.LogHelp.Info(logInfo);
        }

        public static DbConnection GetDataConnection(string connectString)
        {
            DbConnection dbConnection = new SqlConnection(connectString);
            dbConnection.Open();
            return dbConnection;
        }

        public static bool TestDataBaseConnection(string connectString)
        {
            bool resultBool = false;
            try
            {
                using (DbConnection dbConnection = new SqlConnection(connectString))
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

        public static DataSet QuerySql(string strSQL, string connectString)
        {
            using (SqlConnection dbConnection = new SqlConnection(connectString))
            {
                DataSet dataSet = new DataSet();
                try
                {
                    dbConnection.Open();
                    WriteSQLLog(strSQL);
                    SqlDataAdapter command = new SqlDataAdapter(strSQL, dbConnection);
                    command.Fill(dataSet);
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    ZhiFang.LabStar.Common.LogHelp.Info("SQL错误：" + strSQL + ": " + ex.Message);
                    //throw new Exception(ex.Message);
                }
                return dataSet;
            }
        }

        public static DataSet QuerySqlContainPrimaryKey(string strSQL, string connectString)
        {
            using (SqlConnection dbConnection = new SqlConnection(connectString))
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
                    ZhiFang.LabStar.Common.LogHelp.Info("SQL错误：" + strSQL + ": " + ex.Message);
                    //throw new Exception(ex.Message);
                }
                return dataSet;
            }
        }

        public static bool QueryDataIsExistBySql(string strSQL, string connectString)
        {
            bool resultBool = false;
            using (SqlConnection dbConnection = new SqlConnection(connectString))
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
                    ZhiFang.LabStar.Common.LogHelp.Info("SQL错误：" + strSQL + ": " + ex.Message);
                    //throw new Exception(ex.Message);
                }
                return resultBool;
            }
        }

        public static BaseResult ExecuteSql(string strSQL, string connectString)
        {
            BaseResult baseResult = new BaseResult();
            using (SqlConnection dbConnection = new SqlConnection(connectString))
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
                        ZhiFang.LabStar.Common.LogHelp.Info("SQL错误：" + strSQL + ": " + ex.Message);
                        //throw new Exception(ex.Message);
                        baseResult.success = false;
                        baseResult.ErrorInfo = ex.Message;
                    }
                }
            }
            return baseResult;
        }

        public static bool ExecuteSql(string strSQL, string connectString, Dictionary<string, object> listByteSQL)
        {
            using (SqlConnection dbConnection = new SqlConnection(connectString))
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
                        ZhiFang.LabStar.Common.LogHelp.Info("SQL错误：" + strSQL + ": " + ex.Message);
                        //throw new Exception(ex.Message);
                        return false;
                    }
                }
            }
        }

        public static bool ExecuteSqlByPara(string strSQL, string connectString, Dictionary<string, object> listPara)
        {
            using (SqlConnection dbConnection = new SqlConnection(connectString))
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
                    catch //(System.Data.SqlClient.SqlException ex)
                    {
                        //throw new Exception(ex.Message);
                        return false;
                    }
                }
            }
        }

        public static bool ExecuteSqlBool(string strSQL, string connectString)
        {
            using (SqlConnection dbConnection = new SqlConnection(connectString))
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
                        ZhiFang.LabStar.Common.LogHelp.Info("SQL错误：" + strSQL + ": " + ex.Message);
                        //throw new Exception(ex.Message);
                        return false;
                    }
                }
            }
        }

        public static bool ExecuteSqlList(List<string> listSQL, string connectString)
        {
            bool resultBool = false;
            using (SqlConnection dbConnection = new SqlConnection(connectString))
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
                            ZhiFang.LabStar.Common.LogHelp.Info("事务提交失败：" + errorSql + " Error:" + ex.Message);
                            transaction.Rollback();
                            //throw new Exception(ex.Message);
                        }
                    }
                }
            }
            return resultBool;
        }//

        public static bool ExecuteSqlList(List<string> listSQL, List<Dictionary<string, byte[]>> listByteSQL, string connectString)
        {
            bool resultBool = false;
            using (SqlConnection dbConnection = new SqlConnection(connectString))
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
                            ZhiFang.LabStar.Common.LogHelp.Info("事务提交失败：" + errorSql + " Error:" + ex.Message);
                            transaction.Rollback();
                            //throw new Exception(ex.Message);
                        }
                    }
                }
            }
            return resultBool;
        }//

        public static bool ExecuteProcedure(string storedProcName, IDataParameter[] paramenters, string connectString)
        {
            bool resultBool = false;
            using (SqlConnection dbConnection = new SqlConnection(connectString))
            {
                dbConnection.Open();
                if (dbConnection.State == ConnectionState.Open)
                {
                    try
                    {
                        SqlCommand command = new SqlCommand(storedProcName, dbConnection);
                        command.CommandType = CommandType.StoredProcedure;
                        foreach (SqlParameter parameter in paramenters)
                        {
                            command.Parameters.Add(parameter);
                        }
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        ZhiFang.LabStar.Common.LogHelp.Error("存储过程【" + storedProcName + "】执行失败：" + ex.Message);
                        throw new Exception(ex.Message);
                    }
                }
            }
            return resultBool;
        }//
    }

    [DataContract]
    public class BaseResult
    {
        private bool successFlag = true;
        private string errorInfo = "";
        [DataMember]
        public bool success
        {
            get { return successFlag; }
            set { successFlag = value; }
        }
        [DataMember]
        public string ErrorInfo
        {
            get { return errorInfo; }
            set { errorInfo = value; }
        }
    }
}

