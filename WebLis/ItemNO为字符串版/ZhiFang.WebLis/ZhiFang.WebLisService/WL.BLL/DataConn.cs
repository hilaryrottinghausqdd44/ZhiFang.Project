using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;

using DBUtility;
using ECDS.Common.Config;
using ECDS.Common.SysInfo;


namespace ZhiFang.WebLisService.WL.BLL
{
    public class DataConn
    {

        #region 公用属性

        /// <summary>
        /// 返回默认数据库连接名称
        /// </summary>
        public static string DefaultConnName
        {
            get
            {
                return AppDefaultValue.DefaultConnName;
            }
        }

        #endregion
        /// <summary>
        /// 调用底层获取数据连接
        /// </summary
        /// <returns></returns>
        public static IDBConnection CreateDB()
        {
            //取默认配置
            return CreateDB(AppDefaultValue.DefaultConnName);
        }


        /// <summary>
        /// 调用底层获取数据连接
        /// </summary>
        /// <returns></returns>
        public static IDBConnection CreateDB(string connname)
        {
            AppConfigInfo aci = AppConfigInfo.Instance();
            ConnProperty connStr = aci.GetConnProperty(connname);
            DBUtility.IDBConnection dbconn = DBFactory.GetDBConnetion(connStr.ConnString, connStr.DBDriverType);
            return dbconn;
        }


        /// <summary>
        /// 检查数据库连接
        /// </summary>
        /// <returns></returns>
        public static bool CheckConn()
        {
            bool TestConn = false;
            try
            {
                TestConn =  CreateDB().TestConnection();
            }
            catch
            {
                TestConn = false;
            }
            return TestConn;
        }

        /// <summary>
        /// 检查数据库连接
        /// </summary>
        /// <returns></returns>
        public static bool CheckConn(string connname)
        {
            return CreateDB(connname).TestConnection();
        }

        /// <summary>
        /// 从DataTable中取某一行的数据返回，格式为：字段名称、字段内容
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="rowIndex">行号：从0开始</param>
        /// <returns>哈希表</returns>
        public static Hashtable GetRowDataFromDataTable(DataTable dt, int rowIndex)
        {
            Hashtable returnValue = new Hashtable();
            if (dt.Rows.Count > rowIndex)
            {
                for (int col = 0; col < dt.Columns.Count; col++)
                {
                    string fieldName = dt.Columns[col].ColumnName;
                    string fieldValue = dt.Rows[rowIndex][col].ToString();
                    if (returnValue[fieldName] == null)
                        returnValue.Add(fieldName, fieldValue);
                }
            }
            return returnValue;
        }
    }
}
