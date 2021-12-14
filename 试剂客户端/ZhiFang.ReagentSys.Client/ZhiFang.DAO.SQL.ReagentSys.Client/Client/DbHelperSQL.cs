using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.DAO.SQL.ReagentSys.Client.Client
{
    /// <summary>
    /// ADO.NET与试剂客户端数据库的数据访问基础类
    /// </summary>
    public class DbHelperSQL : SqlServerHelper
    {
        public DbHelperSQL()
        {
            ConnectionString = DBUtility.PubConstant.GetADODataBaseSettings(Common.Public.ConfigHelper.GetDataBaseSettings("databaseSettings", "db.connectionString"));
        }
        public DbHelperSQL(string connectionString)
        {
            ConnectionString = connectionString;
        }
        public new static void SetConnectionString()
        {
            ConnectionString = DBUtility.PubConstant.GetADODataBaseSettings(Common.Public.ConfigHelper.GetDataBaseSettings("databaseSettings", "db.connectionString"));
        }
        public static string GetConnectionString()
        {
            return ConnectionString;
        }
    }
}
