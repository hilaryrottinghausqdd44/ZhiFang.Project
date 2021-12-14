using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Data.Common;

namespace ZhiFang.LabStar.DAO.ADO
{
    public class OracleHelper
    {
        public static string IsSQLLog = "1";

        public static void WriteSQLLog(string logInfo)
        {
            if (IsSQLLog == "1")
                ZhiFang.LabStar.Common.LogHelp.Info(logInfo);
        }

        public static DbConnection GetDataConnection(string connectString)
        {
            OracleConnection connection = new OracleConnection();
            //connection.ConnectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=***.***.***.***)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=***)));Persist Security Info=True;User ID=***;Password=***;";
            connection.ConnectionString = connectString;
            connection.Open();

            return connection;
        }

        public static bool TestDataBaseConnection(string connectString)
        {
            bool resultBool = false;
            try
            {
                using (OracleConnection dbConnection = new OracleConnection())
                {
                    dbConnection.ConnectionString = connectString;
                    dbConnection.Open();
                    resultBool = (dbConnection.State == ConnectionState.Open);
                    dbConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return resultBool;
        }

        public static DataSet QuerySql(string strSQL, string connectString)
        {
            using (OracleConnection dbConnection = new OracleConnection(connectString))
            {
                DataSet dataSet = new DataSet();
                try
                {
                    dbConnection.Open();
                    WriteSQLLog(strSQL);
                    OracleDataAdapter command = new OracleDataAdapter(strSQL, dbConnection);
                    command.Fill(dataSet, "ds");
                }
                catch (Exception ex)
                {
                    ZhiFang.LabStar.Common.LogHelp.Info("SQL错误：" + strSQL + ": " + ex.Message);
                    //throw new Exception(ex.Message);
                }
                return dataSet;
            }
        }

        public static bool ExecuteSql(string strSQL, string connectString)
        {
            bool result = true;
            using (OracleConnection dbConnection = new OracleConnection(connectString))
            {
                try
                {

                }
                catch (Exception ex)
                {
                    ZhiFang.LabStar.Common.LogHelp.Info("SQL错误：" + strSQL + ": " + ex.Message);
                    result = false;
                }
            }
            return result;
        }
    }
}
