using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.DAO.SQL.WebAssist.HisInterface
{
    /// <summary>
    /// ADO.NET与HIS数据库的数据访问基础类
    /// </summary>
    public class DbHelperSQL : SqlServerHelper
    {
        //默认的数据库连接
        public new static string ConnectionString = DBUtility.PubConstant.GetADODataBaseSettings(Common.Public.ConfigHelper.GetDataBaseSettings("hisdatabaseSettings", "db.connectionString"));
        
        public DbHelperSQL()
        {
            ConnectionString = DBUtility.PubConstant.GetADODataBaseSettings(Common.Public.ConfigHelper.GetDataBaseSettings("hisdatabaseSettings", "db.connectionString"));
        }
        public DbHelperSQL(string connectionString)
        {
            ConnectionString = connectionString;
        }
        public static new void SetConnectionString()
        {
            ConnectionString = DBUtility.PubConstant.GetADODataBaseSettings(Common.Public.ConfigHelper.GetDataBaseSettings("hisdatabaseSettings", "db.connectionString"));
        }
        public static string GetConnectionString()
        {
            return ConnectionString;
        }
    }
}
