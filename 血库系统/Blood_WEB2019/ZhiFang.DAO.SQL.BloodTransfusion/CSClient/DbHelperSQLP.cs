using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.DAO.SQL.BloodTransfusion.CSClient
{
    /// <summary>
    /// ADO.NET与LIS数据库的数据访问基础类
    /// </summary>
    public class DbHelperSQLP : DBUtility.SqlServerHelperP
    {
        public DbHelperSQLP()
        {
            ConnectionString = DBUtility.PubConstant.GetADODataBaseSettings(Common.Public.ConfigHelper.GetDataBaseSettings("csreadatabaseSettings", "db.connectionString"));
        }
        public DbHelperSQLP(string connectionString)
        {
            ConnectionString = connectionString;
        }
        public static string GetConnectionString()
        {
            return DBUtility.PubConstant.GetADODataBaseSettings(Common.Public.ConfigHelper.GetDataBaseSettings("csreadatabaseSettings", "db.connectionString"));
        }
    }
}
