using System;
using System.Collections.Generic;
using System.Text;

namespace ZhiFang.Common.Dictionary
{
    public class DBUtilityType
    {
        /// <summary>
        /// 数据库驱动类型，根据需要进行选择
        /// </summary>
        public enum DBDriverType
        {
            /// <summary>
            /// Sql Server专用驱动
            /// </summary>
            mssql,
            /// <summary>
            /// 通用数据库驱动，可连接Sql Server，Oracle等
            /// </summary>
            oledb,
            /// <summary>
            /// 通用数据库驱动，可连接Sysbase，DB2，MySql等
            /// </summary>
            odbc,
            /// <summary>
            /// SyBase专用启动，由SyBase公司提供
            /// </summary>
            sysbase,
            /// <summary>
            /// Oracle专用驱动，由Oracle公司提供
            /// </summary>
            orale
        }

        /// <summary>
        /// 数据库类型枚举，根据实际数据库类型进行选择
        /// </summary>
        public enum DataBaseType
        {
            /// <summary>
            /// Sql Server数据库，包括Sql 2000，Sql2005，Sql2008等
            /// </summary>
            sqlserver,
            /// <summary>
            /// Oracle数据库
            /// </summary>
            oracle,
            /// <summary>
            /// Sysbase数据库
            /// </summary>
            sybase,
            /// <summary>
            /// DB2数据库
            /// </summary>
            db2,
            /// <summary>
            /// MySql数据库
            /// </summary>
            mssql,
            /// <summary>
            /// Access数据库
            /// </summary>
            access
        }
    }
}
