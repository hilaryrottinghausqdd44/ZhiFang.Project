using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Xml;


namespace ZhiFang.WebLisService.clsCommon
{
    /// <summary>
    /// 存放所有数据类型转换的方法：主要是我们自己定义的数据类型之间的转换
    /// </summary>
    public class ConvertData
    {
        public ConvertData()
        {
        }


        /// <summary>
        /// 将byte[]转换成字符串
        /// </summary>
        /// <param name="byteData"></param>
        /// <returns></returns>
        public static byte[] convertStringToBytes(string data)
        {
            if (data == null)
                return null;
            byte[] returnValue = System.Text.Encoding.UTF8.GetBytes(data);
            return returnValue;
        }



        /// <summary>
        /// 将字符串转换成byte[]
        /// </summary>
        /// <param name="byteData"></param>
        /// <returns></returns>
        public static string convertBytesToString(byte[] byteData)
        {
            if (byteData == null)
                return null;
            string returnValue = System.Text.Encoding.UTF8.GetString(byteData, 0, byteData.Length); ;
            return returnValue;
        }



        /// <summary>
        /// 转换哈希表的键和值
        /// </summary>
        /// <param name="hashData"></param>
        /// <returns></returns>
        public static Hashtable convertHashKeyAndValue(Hashtable hashData)
        {
            Hashtable returnValue = new Hashtable();
            System.Collections.IDictionaryEnumerator myEnumerator = hashData.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                if (returnValue[myEnumerator.Value] == null)
                    returnValue.Add(myEnumerator.Value, myEnumerator.Key);
            }
            return returnValue;
        }


        /// <summary>
        /// 转换哈希表的键值为大写
        /// </summary>
        /// <param name="hashData"></param>
        /// <returns></returns>
        public static Hashtable convertHashKeyToUpper(Hashtable hashData)
        {
            Hashtable returnValue = new Hashtable();
            System.Collections.IDictionaryEnumerator myEnumerator = hashData.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                string key = myEnumerator.Key.ToString().ToUpper();
                if (returnValue[key] == null)
                    returnValue.Add(key, myEnumerator.Value);
            }
            return returnValue;
        }


        /// <summary>
        /// 将一个字符串转换为指定长度的字符串,如果超过指定长度,截掉后面的字符,用".."来代替
        /// </summary>
        /// <param name="oldString">要转换的字符串</param>
        /// <param name="maxLength">转换的字符串的最大长度,应该大于等于2</param>
        /// <returns>转换后的字符串:如果超过指定长度,截掉后面的字符,用".."来代替</returns>
        public static string convertStringToMaxLength(string oldString, int maxLength)
        {
            if (maxLength < 2)
                maxLength = 2;
            System.Text.Encoding en = System.Text.Encoding.GetEncoding("GB2312");
            String newString = oldString;
            newString = Regex.Replace(newString, "(&)[Nn][Bb][Ss][Pp](;)", "");
            bool bLong = false;
            while (en.GetByteCount(newString) > maxLength * 2)
            {
                newString = newString.Substring(0, newString.Length - 1);
                bLong = true;
            }
            if (bLong)
            {
                //最后为两个字符ASCII
                if (en.GetByteCount(newString.Substring(newString.Length - 2)) == 2)
                    newString = newString.Substring(0, newString.Length - 2) + "..";

                //最后为两个汉字UNICODE
                else if (en.GetByteCount(newString.Substring(newString.Length - 2)) == 4)
                    newString = newString.Substring(0, newString.Length - 1) + "..";

                //最后为一个字符ASCII＋一个汉字UNICODE
                else if (en.GetByteCount(newString.Substring(newString.Length - 2)) == 3)
                {
                    if (en.GetByteCount(newString.Substring(newString.Length - 1)) == 1)
                        newString = newString.Substring(0, newString.Length - 1) + ".";
                    else if (en.GetByteCount(newString.Substring(newString.Length - 1)) == 2)
                        newString = newString.Substring(0, newString.Length - 1) + "..";

                }
            }
            return newString;
        }




        /// <summary>
        /// 将SqlDataReader转换成DataTable
        /// 转换完成后关闭SqlDataReader
        /// </summary>
        /// <param name="reader">要转换的SqlDataReader</param>
        /// <returns></returns>
        public static DataTable convertDataReaderToDataTable(IDataReader reader)
        {
            DataTable dt = new DataTable();
            try
            {
                //动态添加列

                for(int column=0; column<reader.FieldCount;column ++)
                {
                    dt.Columns.Add(reader.GetName(column), reader.GetFieldType(column));
                }
                //添加数据 
                dt.BeginLoadData();
                while (reader.Read())
                {
                    object[] row = new object[reader.FieldCount];
                    reader.GetValues(row);
                    dt.LoadDataRow(row, true);
                    row = null;
                }
                dt.EndLoadData();
                reader.Close();
            }
            catch
            {
                throw;
            }
            return dt;
        }



        /// <summary>
        /// 转换自定义的存储过程参数方向为系统数据的参数方向
        /// </summary>
        /// <param name="paramDirection"></param>
        /// <returns></returns>
        public static System.Data.ParameterDirection convertParamDirection(ParamDirection paramDirection)
        {
            System.Data.ParameterDirection pd = ParameterDirection.Input;
            switch (paramDirection)
            {
                case ParamDirection.Input:
                    pd = ParameterDirection.Input;
                    break;
                case ParamDirection.InputOutput:
                    pd = ParameterDirection.InputOutput;
                    break;
                case ParamDirection.Output:
                    pd = ParameterDirection.Output;
                    break;
                case ParamDirection.ReturnValue:
                    pd = ParameterDirection.ReturnValue;
                    break;
            }
            return pd;
        }




        /// <summary>
        /// 将FieldType的枚举类型（字符串），转换成FieldType
        /// </summary>
        /// <param name="fieldTypeEnum"></param>
        /// <returns></returns>
        public static FieldType convertToFieldType(string fieldTypeEnum)
        {
            FieldType fieldType = FieldType.NONE;
            switch (fieldTypeEnum)
            {
                case "BLOB":
                    fieldType = FieldType.BLOB;
                    break;
                case "Char":
                    fieldType = FieldType.Char;
                    break;
                case "CLOB":
                    fieldType = FieldType.CLOB;
                    break;
                case "Date":
                    fieldType = FieldType.Date;
                    break;
                case "DateTime":
                    fieldType = FieldType.DateTime;
                    break;
                case "Float":
                    fieldType = FieldType.Float;
                    break;
                case "GUID":
                    fieldType = FieldType.GUID;
                    break;
                case "IDENTITY":
                    fieldType = FieldType.IDENTITY;
                    break;
                case "Integer":
                    fieldType = FieldType.Integer;
                    break;
                case "Long":
                    fieldType = FieldType.Long;
                    break;
                case "NONE":
                    fieldType = FieldType.NONE;
                    break;
                case "Number":
                    fieldType = FieldType.Number;
                    break;
                case "Time":
                    fieldType = FieldType.Time;
                    break;
                case "Varchar":
                    fieldType = FieldType.Varchar;
                    break;
            }
            return fieldType;
        }



        /// <summary>
        /// 转换字段类型
        /// </summary>
        /// <param name="hashTableFieldNameTypeXML"></param>
        /// <returns></returns>
        public static Hashtable convertFieldTypeHashtable(Hashtable hashTableFieldNameTypeXML)
        {
            Hashtable hashTableFieldNameType = new Hashtable();
            System.Collections.IDictionaryEnumerator myEnumerator = hashTableFieldNameTypeXML.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                string fieldTypeXML = myEnumerator.Value.ToString();
                FieldType fieldType = ConvertData.convertXmlFieldType(fieldTypeXML);
                hashTableFieldNameType.Add(myEnumerator.Key, fieldType);
            }
            return hashTableFieldNameType;
        }



        /// <summary>
        /// 将XML定义的字段类型转换成系统定义的字段类型，即FieldType枚举类型
        /// </summary>
        /// <param name="xmlFieldType"></param>
        /// <returns></returns>
        public static FieldType convertXmlFieldType(string xmlFieldType)
        {
            FieldType fieldType = FieldType.NONE;
            switch (xmlFieldType)
            {
                case "0": //字符
                    fieldType = FieldType.Varchar;
                    break;
                case "1": //数字
                    fieldType = FieldType.Float;
                    break;
                case "2": //日期
                    fieldType = FieldType.Date;
                    break;
                case "3": //文件
                    fieldType = FieldType.File;
                    break;
                case "4": //新闻
                    fieldType = FieldType.News;
                    break;
                case "Integer":
                    fieldType = FieldType.Integer;
                    break;
                case "Long":
                    fieldType = FieldType.Long;
                    break;
                case "IDENTITY":
                    fieldType = FieldType.IDENTITY;
                    break;
                case "Float":
                    fieldType = FieldType.Float;
                    break;
                case "Date":
                    fieldType = FieldType.Date;
                    break;
                case "Time":
                    fieldType = FieldType.Time;
                    break;
                case "DateTime":
                    fieldType = FieldType.DateTime;
                    break;
                case "Char":
                    fieldType = FieldType.Char;
                    break;
                case "Varchar":
                    fieldType = FieldType.Varchar;
                    break;
                case "CLOB":
                    fieldType = FieldType.CLOB;
                    break;
                case "BLOB":
                    fieldType = FieldType.BLOB;
                    break;
                case "Number":
                    fieldType = FieldType.Number;
                    break;
                default:
                    fieldType = FieldType.Varchar;
                    break;
            }
            return fieldType;
        }


        /// <summary>
        /// 将SQL Server数据库定义的数据类型为XML配制文件里使用的字段类型
        /// </summary>
        /// <param name="fieldType"></param>
        /// <returns></returns>
        public static string convertSQLServerFieldTypeToXML(string fieldType)
        {
            string fieldTypeXMl = "0";//默认是字符型
            switch (fieldType)
            {
                case "decimal":
                case "float":
                case "real":
                case "numeric":
                case "smallint":
                case "int":
                case "bigint":
                case "smallmoney":
                case "money":
                case "tinyint":
                    fieldTypeXMl = "1";//数字
                    break;
                case "datetime":
                case "smalldatetime":
                case "date":
                case "time":
                    fieldTypeXMl = "2";//日期
                    break;
                case "binary":
                case "image":
                case "ntext":
                case "text":
                    fieldTypeXMl = "3";//文件
                    break;
                //case "":
                //    fieldTypeXMl = "4";//新闻
                //    break;
            }
            return fieldTypeXMl;
        }


        /// <summary>
        /// 将配置文件TablesConfig.XML定义的数据类型转换成SQL Server
        /// </summary>
        /// <param name="fieldType"></param>
        /// <returns></returns>
        public static string convertFieldTypeToSQLServer(FieldType fieldType)
        {
            string sqlFieldType = "";
            switch (fieldType)
            {
                case FieldType.Integer:
                    sqlFieldType = "[int]";
                    break;
                case FieldType.Long:
                    sqlFieldType = "[bigint]";
                    break;
                case FieldType.IDENTITY:
                    sqlFieldType = "[bigint] IDENTITY(1,1)";
                    break;
                case FieldType.Float:
                    sqlFieldType = "[float]";
                    break;
                case FieldType.Date:
                    sqlFieldType = "[datetime]";
                    break;
                case FieldType.Time:
                    sqlFieldType = "[datetime]";
                    break;
                case FieldType.DateTime:
                    sqlFieldType = "[datetime]";
                    break;
                case FieldType.Char:
                    sqlFieldType = "[char](20)";
                    break;
                case FieldType.Varchar:
                    sqlFieldType = "[varchar](200)";
                    break;
                case FieldType.CLOB:
                    sqlFieldType = "[ntext]";
                    break;
                case FieldType.BLOB:
                    sqlFieldType = "[ntext]";
                    break;
                case FieldType.Number:
                    sqlFieldType = "[bigint]";
                    break;
                default:
                    sqlFieldType = "[varchar](200)";
                    break;
            }
            return sqlFieldType;
        }




        /// <summary>
        /// 我们定义的数据类型转换成SQL Server：生成主关键字的脚本时使用
        /// </summary>
        /// <param name="fieldType"></param>
        /// <returns></returns>
        public static string convertFieldTypeToSQLServerPrimaryKey(FieldType fieldType)
        {
            string sqlFieldType = "";
            switch (fieldType)
            {
                case FieldType.Integer:
                    sqlFieldType = "[int] ";
                    break;
                case FieldType.Long:
                    sqlFieldType = "[bigint] ";
                    break;
                case FieldType.IDENTITY:
                    sqlFieldType = "[bigint] IDENTITY(1,1) ";
                    break;
                case FieldType.Float:
                    sqlFieldType = "[float]";
                    break;
                case FieldType.Date:
                    sqlFieldType = "[datetime]";
                    break;
                case FieldType.Time:
                    sqlFieldType = "[datetime]";
                    break;
                case FieldType.DateTime:
                    sqlFieldType = "[datetime]";
                    break;
                case FieldType.Char:
                    sqlFieldType = "[char](20)";
                    break;
                case FieldType.Varchar:
                    sqlFieldType = "[varchar](200)";
                    break;
                case FieldType.CLOB:
                    sqlFieldType = "[ntext]";
                    break;
                case FieldType.BLOB:
                    sqlFieldType = "[ntext]";
                    break;
                case FieldType.Number:
                    sqlFieldType = "[bigint]";
                    break;
                default:
                    sqlFieldType = "[varchar](200)";
                    break;
            }
            return sqlFieldType;
        }


        /// <summary>
        /// 转换配置文件配置的数据库类型为我们实际存储的数据库的类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static DatabaseType convertDatabaseType(string type)
        {
            DatabaseType databaseType = DatabaseType.SQLServer;
            switch (type)
            {
                case "0":
                    databaseType = DatabaseType.XML;
                    break;
                case "1":
                    databaseType = DatabaseType.SQLServer;
                    break;
                case "2":
                    databaseType = DatabaseType.Oracle;
                    break;
                case "3":
                    databaseType = DatabaseType.Access;
                    break;
                case "4":
                    databaseType = DatabaseType.DB2;
                    break;
                case "5":
                    databaseType = DatabaseType.Excel;
                    break;
                case "6":
                    databaseType = DatabaseType.Sybase;
                    break;
                case "7":
                    databaseType = DatabaseType.OTHER;
                    break;
            }
            return databaseType;
        }




        /// <summary>
        /// 根据数据库的类型、字段的类型，查询的条件，生成一个数据库查询条件语句
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <param name="fieldType">字段类型</param>
        /// <param name="queryCondition">查询条件</param>
        /// <param name="fieldValue">查询的内容</param>
        /// <param name="databaseType">数据库的类型</param>
        /// <returns></returns>
        public static string convertQueryConditionToDatabaseCondition(string tableName, string fieldName, FieldType fieldType, string queryCondition, string fieldValue, DatabaseType databaseType)
        {
            string fieldNameModal = "[{0}].[{1}]";
            fieldName = string.Format(fieldNameModal, tableName, fieldName);
            //转换字段的内容格式
            string dbCondition = "";
            switch (queryCondition)
            {
                case "equal"://等于
                    dbCondition = " = ";
                    if (fieldValue == "") //查空串
                    {
                        dbCondition = fieldName + " IS NULL OR " + fieldName + "=''";
                    }
                    else
                    {
                        //转换内容
                        fieldValue = ConvertData.convertFieldValueToDatabase(fieldValue, fieldType, databaseType);
                        dbCondition = fieldName + dbCondition + fieldValue;
                    }
                    break;
                case "ne"://不等于
                    dbCondition = " <> ";
                    if (fieldValue == "") //查空串
                    {
                        dbCondition = fieldName + " IS NOT NULL OR " + fieldName + "<>''";
                    }
                    else
                    {
                        //转换内容
                        fieldValue = ConvertData.convertFieldValueToDatabase(fieldValue, fieldType, databaseType);
                        dbCondition = fieldName + dbCondition + fieldValue;
                    }
                    break;
                case "gt"://大于
                    dbCondition = " > ";
                    fieldValue = ConvertData.convertFieldValueToDatabase(fieldValue, fieldType, databaseType);
                    dbCondition = fieldName + dbCondition + fieldValue;
                    break;
                case "ge"://大于等于
                    dbCondition = " >= ";
                    fieldValue = ConvertData.convertFieldValueToDatabase(fieldValue, fieldType, databaseType);
                    dbCondition = fieldName + dbCondition + fieldValue;
                    break;
                case "lt"://小于
                    dbCondition = " < ";
                    fieldValue = ConvertData.convertFieldValueToDatabase(fieldValue, fieldType, databaseType);
                    dbCondition = fieldName + dbCondition + fieldValue;
                    break;
                case "le"://小于等于
                    dbCondition = " <= ";
                    fieldValue = ConvertData.convertFieldValueToDatabase(fieldValue, fieldType, databaseType);
                    dbCondition = fieldName + dbCondition + fieldValue;
                    break;
                case "include"://包含
                    dbCondition = " LIKE ";//SQLServer 
                    if (databaseType == DatabaseType.Oracle)
                        dbCondition = " $ ";//ORACLE
                    fieldValue = "'%" + fieldValue + "%'";
                    dbCondition = fieldName + dbCondition + fieldValue;
                    if (fieldValue == "")
                    {
                        dbCondition += " OR " + fieldName + " IS NULL ";
                    }
                    break;
                case "noinclude"://不包含
                    dbCondition = " LIKE ";//SQLServer 
                    if (databaseType == DatabaseType.Oracle)
                        dbCondition = " $ ";//ORACLE
                    fieldValue = "'%" + fieldValue + "%'";
                    dbCondition = fieldName + dbCondition + fieldValue;
                    if (fieldValue == "")
                    {
                        dbCondition += " OR " + fieldName + " IS NULL ";
                    }
                    break;
                case "includein"://包含于
                    dbCondition = "=";
                    break;
                case "begin"://开始于
                    if (fieldValue == "")
                    {
                        dbCondition = fieldName + "='' OR " + fieldName + " IS NULL ";
                    }
                    else
                    {
                        dbCondition = "left(" + fieldName + "," + fieldValue.Length.ToString() + ") = '" + fieldValue + "'";
                    }
                    break;
                case "end"://结束于
                    dbCondition = "right(" + fieldName + "," + fieldValue.Length.ToString() + ") = '" + fieldValue + "'";
                    break;
                default:
                    break;
            }
            if (dbCondition != "")
                dbCondition = "(" + dbCondition + ")";
            return dbCondition;
        }



        /// <summary>
        /// 转换字段内容为数据库格式
        /// </summary>
        /// <param name="fieldValue"></param>
        /// <param name="fieldType"></param>
        /// <param name="databaseType"></param>
        /// <returns></returns>
        public static string convertFieldValueToDatabase(string fieldValue, FieldType fieldType, DatabaseType databaseType)
        {
            switch (fieldType)
            {
                case FieldType.Integer:
                case FieldType.Long:
                case FieldType.Number:
                case FieldType.IDENTITY:
                case FieldType.Float:
                    //数值型，直接返回
                    break;
                default:
                    //字符型或日期型，加单引号
                    fieldValue = "'" + fieldValue + "'";
                    break;
            }
            return fieldValue;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="opidString"></param>
        /// <returns></returns>
        public static OPID convertToOPID(string opidString)
        {
            OPID opid = OPID.NONE;
            switch (opidString)
            {
                case "INSERT":
                    opid = OPID.INSERT;
                    break;
                case "UPDATE":
                    opid = OPID.UPDATE;
                    break;
                case "DELETE":
                    opid = OPID.DELETE;
                    break;
                case "SELECT":
                    opid = OPID.SELECT;
                    break;
                case "VIEW":
                    opid = OPID.VIEW;
                    break;
                case "NONE":
                    opid = OPID.NONE;
                    break;
            }
            return opid;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string convertHtmlToESC(string html)
        {
            string ret = html;
            //转义
            ret = ret.Replace("&", "&amp;");
            ret = ret.Replace("\"", "&quot;");
            ret = ret.Replace("'", "&apos;");
            ret = ret.Replace(">", "&gt;");
            ret = ret.Replace("<", "&lt;");
            return ret;
        }
  

        /// <summary>
        /// 
        /// </summary>
        /// <param name="esc"></param>
        /// <returns></returns>
        public static string convertESCToHtml(string esc)
        {
            string ret = esc;
            //转义
            ret = ret.Replace("&quot;", "\"");
            ret = ret.Replace("&apos;", "'");
            ret = ret.Replace("&gt;", ">");
            ret = ret.Replace("&lt;", "<");
            ret = ret.Replace("%20", " ");//空格
            ret = ret.Replace("&nbsp;", " ");//空格
            ret = ret.Replace("&amp;", "&");//要最后转，这样可以保证二次转义的正确性，比如输入的是&lt;会先转义为&amp;lt;
            return ret;
        }


        /// <summary>
        /// 将哈希表转换成SortedList:可以将Key和Value一致
        /// </summary>
        /// <param name="hashTable"></param>
        /// <returns></returns>
        public static System.Collections.SortedList convertHashTableToSortList(System.Collections.Hashtable hashTable)
        {
            return ConvertData.convertHashTableToSortList(hashTable, true);
        }



        /// <summary>
        /// 将哈希表转换成SortedList:可以将Key和Value颠倒过来
        /// </summary>
        /// <param name="hashTable"></param>
        /// <param name="isKeyValue">Key和Value是否一致,=true为一致,否则颠倒过来</param>
        /// <returns></returns>
        public static System.Collections.SortedList convertHashTableToSortList(System.Collections.Hashtable hashTable, bool isKeyValue)
        {
            SortedList sortedList = new SortedList();
            System.Collections.IDictionaryEnumerator myEnumerator = hashTable.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                if (isKeyValue)
                {
                    if (sortedList[myEnumerator.Key] == null)
                        sortedList.Add(myEnumerator.Key, myEnumerator.Value);
                }
                else
                {
                    if (sortedList[myEnumerator.Value] == null)
                        sortedList.Add(myEnumerator.Value, myEnumerator.Key);
                }
            }
            return sortedList;
        }




        /// <summary>
        /// 将一个DataTable转换成XML，格式为：
        /// <items>
        ///		<item guid=primaryKey_Value>
        ///			<field1>value1</field1>
        ///			<field2>value2</field2>
        ///			...
        ///			<fieldn>valuen</fieldn>
        ///		</item>
        /// </items>
        /// </summary>
        /// <param name="dt">数据库存放的表</param>
        /// <param name="primaryKey">主键名称，生成时作为ITEM的属性</param>
        /// <returns></returns>
        public static string convertDataTableToXML(DataTable dt, string primaryKey)
        {
            string contentXML = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n";
            contentXML += "<items>\r\n";
            for (int rowCount = 0; rowCount < dt.Rows.Count; rowCount++)
            {
                string rowXML = "", guid = "";
                for (int colCount = 0; colCount < dt.Columns.Count; colCount++)
                {
                    string fieldName = dt.Columns[colCount].ToString().Trim();
                    string fieldValue = dt.Rows[rowCount][colCount].ToString().Trim();
                    rowXML += "\t\t<" + fieldName + ">" + fieldValue + "</" + fieldName + ">\r\n";
                    if (primaryKey.ToLower() == fieldName.ToLower())
                    {
                        guid = fieldValue;
                    }
                }
                rowXML = "\t<item guid=\"" + guid + "\">\r\n" + rowXML + "\t</item>\r\n";
                contentXML += rowXML;
            }
            contentXML += "</items>\r\n";
            return contentXML;
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static string convertDataSetToXML(DataSet ds,string[] FieldSumListAsBefore,string[] FieldSumValues)
        {
            string contentXML = "<?xml version=\"1.0\" encoding=\"uft-8\"?>\r\n";
            contentXML += "<Tables>\r\n";
            //contentXML += "\t<table EName=\"" + "101" + "\">\r\n";
            contentXML += "\t<Table EName=\"" + ds.Tables[0].TableName + "\"";
            if (FieldSumListAsBefore != null && FieldSumValues != null)
            {
                for (int iCol = 0; iCol < FieldSumValues.Length; iCol++)
                {
                    contentXML += " " + FieldSumListAsBefore[iCol] + "_Sum=\"" + FieldSumValues[iCol] + "\"";
                }
            }
            contentXML += ">\r\n";

            for (int rowCount = 0; rowCount < ds.Tables[0].Rows.Count; rowCount++)
            {
                string rowXML = "";
                for (int colCount = 0; colCount < ds.Tables[0].Columns.Count; colCount++)
                {
                    string fieldName = ds.Tables[0].Columns[colCount].ToString();
                    string fieldValue = ds.Tables[0].Rows[rowCount][colCount].ToString();

                    //格式日期数据到yyyy-MM-dd 2009-03-06 lizj///////////////////////////////////////////////////
                    if (ds.Tables[0].Columns[colCount].DataType == Type.GetType("System.DateTime"))
                    {
                        if (!Convert.IsDBNull(ds.Tables[0].Rows[rowCount][colCount]) && fieldValue.Trim()!="")
                        {
                            fieldValue = Convert.ToDateTime(fieldValue).ToString("yyyy-MM-dd");
                        }
                    }
                    ////////////////////////////////////////////////////////////////////////////////////////////
                    //转义
                    fieldValue = fieldValue.Replace("&", "&amp;");
                    fieldValue = fieldValue.Replace("\"", "&quot;");
                    fieldValue = fieldValue.Replace(">", "&gt;");
                    fieldValue = fieldValue.Replace("<", "&lt;");
                    rowXML += "\t\t\t<td Column=\"" + fieldName + "\">" + fieldValue + "</td>\r\n";
                }
                rowXML = "\t\t<tr>\r\n" + rowXML + "\n\t\t</tr>\r\n";
                contentXML += rowXML;
            }
            contentXML += "\t</Table>\r\n";
            contentXML += "</Tables>\r\n";
            return contentXML;
        }



        /// <summary>
        /// 将一个DataTable转换成XML，但如果是CLOB字段则不生成格式为：
        /// <items>
        ///		<item guid=primaryKey_Value>
        ///			<field1>value1</field1>
        ///			<field2>value2</field2>
        ///			...
        ///			<fieldn>valuen</fieldn>
        ///		</item>
        /// </items>
        /// </summary>
        /// <param name="dt">数据库存放的表</param>
        /// <param name="primaryKey">主键名称，生成时作为ITEM的属性</param>
        /// <returns></returns>
        public static string convertDataTableToXML_OA(DataTable dt, string primaryKey)
        {
            string contentXML = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n";
            contentXML += "<Tables>\r\n";
            //contentXML += "\t<table EName=\"" + "101" + "\">\r\n";
            contentXML += "\t<Table EName=\"" + dt.TableName + "\">\r\n";
            for (int rowCount = 0; rowCount < dt.Rows.Count; rowCount++)
            {
                string rowXML = "", guid = "";
                for (int colCount = 0; colCount < dt.Columns.Count; colCount++)
                {
                    string fieldName = dt.Columns[colCount].ToString().Trim();
                    string fieldValue = dt.Rows[rowCount][colCount].ToString().Trim();
                    //大字段的不生成
                    if (fieldValue.StartsWith("<?xml version="))
                        fieldValue = "";
                    else if (fieldValue.IndexOf("\"") != -1)
                        fieldValue = "";
                    else if (fieldValue.IndexOf(">") != -1)
                        fieldValue = "";
                    rowXML += "\t\t\t<td Column=\"" + fieldName + "\">" + fieldValue + "</td>\r\n";
                    if (primaryKey.ToLower() == fieldName.ToLower())
                    {
                        guid = fieldValue;
                    }
                }
                rowXML = "\t\t<tr>\r\n" + rowXML + "\n\t\t</tr>\r\n";
                contentXML += rowXML;
            }
            contentXML += "\t</Table>\r\n";
            contentXML += "</Tables>\r\n";
            //System.Console.WriteLine(contentXML);
            return contentXML;
        }




        /// <summary>
        /// 将DataCollection转换成我们操纵数据的SQLData类
        /// </summary>
        /// <param name="TableEName"></param>
        /// <param name="DataCollection"></param>
        /// <returns></returns>
        public static SQLData convertDataCollectionToSQLData(string TableEName, string[] dataCollection)
        {
            SQLData sqlData = DataTag.initSQLData();
            sqlData.TableName = TableEName;
            //将该表的字段和内容转换到SQLData相应的参数表中
            for (int i = 0; i < dataCollection.Length; i++)
            {
                string fieldNameValue = dataCollection[i].ToString().Trim();
                if (fieldNameValue.Length <= 0) continue;
                if (fieldNameValue.StartsWith("$$Desig_")) continue;
                string[] splitArray = fieldNameValue.Split(new char[] { '=' });
                if (splitArray.Length > 1)
                {
                    object hashKey = splitArray[0];//字段名称
                    object hashValue = splitArray[1];//字段内容
                    sqlData.SqlParams.Add(hashKey, hashValue);
                }

            }
            return sqlData;
        }




        /// <summary>
        /// 将DataCollection转换成我们操纵数据的SQLData类
        /// </summary>
        /// <param name="TableEName"></param>
        /// <param name="DataCollection"></param>
        /// <returns></returns>
        public SQLData convertXmlNodeDataToSQLData(string TableEName, string[] dataCollection)
        {
            SQLData sqlData = DataTag.initSQLData();
            sqlData.TableName = TableEName;
            //将该表的字段和内容转换到SQLData相应的参数表中
            for (int i = 0; i < dataCollection.Length; i++)
            {
                string fieldNameValue = dataCollection[i].ToString().Trim();
                if (fieldNameValue.Length <= 0) continue;
                if (fieldNameValue.StartsWith("$$Desig_")) continue;
                string[] splitArray = fieldNameValue.Split(new char[] { '=' });
                if (splitArray.Length > 1)
                {
                    object hashKey = splitArray[0];//字段名称
                    object hashValue = splitArray[1];//字段内容
                    sqlData.SqlParams.Add(hashKey, hashValue);
                }

            }
            return sqlData;
        }



        /// <summary>
        /// 将HTML中使用到的字段控件转换为配置文件指定的控件
        /// 如果有已经报送的内容XML，则将内容取出，放到相应的控件的属性值中：一般是在修改已经报送过的信息才有
        /// 将报送XML的内容ReportXML更新到模版的HTML(即DocHTML)相应控件的属性值中
        /// 返回新的HTML
        /// 在报送中使用：显示历史报送的记录的详细情况
        /// 如果出现异常，返回null
        /// </summary>
        /// <param name="sourceHTML">显示的元数据类型模版HTML</param>
        /// <param name="sourceXML">报送数据的实际情况</param>
        /// <returns></returns>
        public static string convertModalHtmlToCanEdit(string sourceHTML, string sourceXML)
        {
            string xmlHeader = "<?xml version=\"1.0\" encoding=\"UTF-8\"?> \n";
            string newHTML = sourceHTML;

            try
            {
                //存放元数据类型模版里插入的字段的编辑风格，即用什么控件进行编辑，定义文件为FieldStyle.xml
                //取字段编辑对应的ActiveX控件的classid,装载html时，用指定的控件id代替原来的classid
                System.Collections.Hashtable hashTableFieldStyle = GetXMLData.getXmlNodeTowAttribute("..\\objdefine\\FieldStyle.xml", "//item", "name", "value");
                //XML定义的所有字段名称和内容
                System.Collections.Hashtable hashTablePublicFields = new Hashtable(); ;
                System.Collections.Hashtable hashTableSchemaFields = new Hashtable(); ;
                if ((sourceXML != "") && (sourceXML != null))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(sourceXML);
                    XmlNodeList nodeList = doc.SelectNodes("//doc");
                    if (nodeList.Count > 0)
                    {
                        //取到XML定义的所有公共字段名称和内容
                        hashTablePublicFields = GetXMLData.getXmlNodeNameAndValue(nodeList[0]);
                    }
                    nodeList = doc.SelectNodes("//datapackage");
                    if (nodeList.Count > 0)
                    {
                        //取到XML定义的所有Schema字段名称和内容
                        hashTableSchemaFields = GetXMLData.getXmlNodeNameAndValue(nodeList[0]);
                    }
                }
                //遍历HTML，取各个OBJECT的内容
                string[,] objectArray = Tools.getObjectFromHTML(sourceHTML, "<OBJECT ", "</OBJECT>");
                int recNum = objectArray.Length / (objectArray.GetUpperBound(1) + 1);
                //更改OBJECT 的 classid
                for (int i = 0; i < recNum; i++)
                {
                    //将object转成XML
                    string oldObjectHTML = objectArray[i, 2];
                    string objectXML = xmlHeader + oldObjectHTML;
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(objectXML);
                    //先取字段名称和字段编辑的风格名称
                    string fieldName = "";
                    string fieldStyle = "";
                    string objectType = "";
                    XmlNodeList nodeList = doc.SelectNodes("//CUSTOMPARAM");
                    foreach (XmlNode node in nodeList)
                    {
                        for (int j = 0; j < node.ChildNodes.Count; j++)
                        {
                            string nodeName = node.ChildNodes[j].Name;
                            string nodeValue = node.ChildNodes[j].InnerXml;
                            switch (nodeName.ToLower())
                            {
                                case "fieldname":
                                    fieldName = nodeValue;
                                    break;
                                case "fieldstyle":
                                    fieldStyle = nodeValue;
                                    break;
                                case "type":
                                    objectType = nodeValue;
                                    break;
                            }
                        }
                        break;//只取第一个，也只有一个
                    }
                    //取到要更改的新值
                    string fieldValueNew = "";
                    if (fieldName != "")
                    {
                        object hashKey = fieldName;
                        if (objectType == "_PUBLICFIELD")
                        {
                            if (hashTablePublicFields[hashKey] != null)
                                fieldValueNew = hashTablePublicFields[hashKey].ToString();
                        }
                        else
                        {
                            if (hashTableSchemaFields[hashKey] != null)
                                fieldValueNew = hashTableSchemaFields[hashKey].ToString();
                        }
                    }
                    string classIdNew = "";
                    if (fieldStyle != "")
                    {
                        object hashKey = fieldStyle;
                        if (hashTableFieldStyle[hashKey] != null)
                            classIdNew = "clsid:" + hashTableFieldStyle[hashKey].ToString();
                    }
                    //然后遍历OBJECT，更改classid
                    nodeList = doc.SelectNodes("//OBJECT");
                    foreach (XmlNode node in nodeList)
                    {
                        //遍历属性，更新classid
                        if (node.Attributes.Count > 0)
                        {
                            node.Attributes.GetNamedItem("classid").InnerXml = classIdNew;//更改
                        }
                        break;//只取第一个，也只有一个
                    }
                    //然后遍历OBJECT的PARAM，更改输入的字段内容
                    nodeList = doc.SelectNodes("./OBJECT/PARAM");
                    foreach (XmlNode node in nodeList)
                    {
                        //遍历属性，更新classid
                        if (node.Attributes.Count > 0)
                        {
                            string name = node.Attributes.GetNamedItem("NAME").Value;
                            if (name.ToLower() == "textvalue")
                            {
                                node.Attributes.GetNamedItem("VALUE").InnerXml = fieldValueNew;//更改
                                break;
                            }
                        }
                    }
                    //取到新的OBJECT  oldHTML
                    string objectString = doc.OuterXml;
                    int loc = objectString.IndexOf("<OBJECT ");
                    //去掉XML的头
                    objectString = objectString.Substring(loc);
                    newHTML = newHTML.Replace(oldObjectHTML, objectString);
                }
            }
            catch//(System.Exception ex)
            {
                //DrmsMessageBox.Show(ex.Message);
                return null;
            }


            return newHTML;
        }



        /// <summary>
        /// 转码程序
        /// </summary>
        /// <param name="srcString">要转换的原码的字符串</param>
        /// <param name="encodeSrc">原来的编码</param>
        /// <param name="encodeDst">目标编码</param>
        /// <returns>返回转换后的字符串</returns>
        public static string convertCoding(string srcString, Encoding encodeSrc, Encoding encodeDst)
        {
            //将转换的字符串转为byte[]
            byte[] convertSrc = encodeSrc.GetBytes(srcString);
            //转换编码
            byte[] convertDst = Encoding.Convert(encodeSrc, encodeDst, convertSrc);
            //将转换的结果byte[]转换为string
            //存放目标的char[]
            char[] dstChars = new char[encodeDst.GetCharCount(convertDst, 0, convertDst.Length)];
            //转换到目标char[]
            encodeDst.GetChars(convertDst, 0, convertDst.Length, dstChars, 0);
            //结果：将char[]转换为string
            return new string(dstChars);
        }


        /// <summary>
        /// 转换为统计分类项的统计类别
        /// </summary>
        /// <param name="typeXML"></param>
        /// <returns></returns>
        public static GroupByType convertToGroupByType(string typeXML)
        {
            GroupByType type = GroupByType.NONE;
            switch (typeXML)
            {
                case "Year":
                    type = GroupByType.Year;
                    break;
                case "Quarter":
                    type = GroupByType.Quarter;
                    break;
                case "Month":
                    type = GroupByType.Month;
                    break;
                case "Week":
                    type = GroupByType.Week;
                    break;
                case "Day":
                    type = GroupByType.Day;
                    break;
            }
            return type;
        }


        /// <summary>
        /// 转换统计图的类型
        /// </summary>
        /// <param name="configValue"></param>
        /// <returns></returns>
        public static ChartTypeSTAT convertToChartTypeSTAT(string configValue)
        {
            ChartTypeSTAT chartTypeSTAT = ChartTypeSTAT.Combo;
            switch (configValue)
            {
                case "Combo":
                    chartTypeSTAT = ChartTypeSTAT.Combo;
                    break;
                case "ComboHorizontal":
                    chartTypeSTAT = ChartTypeSTAT.ComboHorizontal;
                    break;
                case "Pies":
                    chartTypeSTAT = ChartTypeSTAT.Pies;
                    break;
                case "Ring":
                    chartTypeSTAT = ChartTypeSTAT.Ring;
                    break;
                case "Column":
                    chartTypeSTAT = ChartTypeSTAT.Column;
                    break;
                case "Area":
                    chartTypeSTAT = ChartTypeSTAT.Area;
                    break;
                case "AreaLine":
                    chartTypeSTAT = ChartTypeSTAT.AreaLine;
                    break;
                case "ComboSideBySide":
                    chartTypeSTAT = ChartTypeSTAT.ComboSideBySide;
                    break;
                case "Spline":
                    chartTypeSTAT = ChartTypeSTAT.Spline;
                    break;
            }
            return chartTypeSTAT;
        }



        /// <summary>
        /// 转换统计图的图象输出格式
        /// </summary>
        /// <param name="configValue"></param>
        /// <returns></returns>
        public static ImageFormatSTAT convertToImageFormatSTAT(string configValue)
        {
            ImageFormatSTAT imageFormatSTAT = ImageFormatSTAT.Pdf;
            switch (configValue)
            {
                case "Bmp":
                    imageFormatSTAT = ImageFormatSTAT.Bmp;
                    break;
                case "Jpg":
                    imageFormatSTAT = ImageFormatSTAT.Jpg;
                    break;
                case "Pdf":
                    imageFormatSTAT = ImageFormatSTAT.Pdf;
                    break;
                case "Png":
                    imageFormatSTAT = ImageFormatSTAT.Png;
                    break;
                case "Tif":
                    imageFormatSTAT = ImageFormatSTAT.Tif;
                    break;
            }
            return imageFormatSTAT;
        }


        /// <summary>
        /// 转换统计图的阴影效果
        /// </summary>
        /// <param name="configValue"></param>
        /// <returns></returns>
        public static ChartShadingEffectSTAT convertToChartShadingEffectSTAT(string configValue)
        {
            ChartShadingEffectSTAT chartShadingEffectSTAT = ChartShadingEffectSTAT.Three;
            switch (configValue)
            {
                case "One":
                    chartShadingEffectSTAT = ChartShadingEffectSTAT.One;
                    break;
                case "Two":
                    chartShadingEffectSTAT = ChartShadingEffectSTAT.Two;
                    break;
                case "Three":
                    chartShadingEffectSTAT = ChartShadingEffectSTAT.Three;
                    break;
                case "Four":
                    chartShadingEffectSTAT = ChartShadingEffectSTAT.Four;
                    break;
                case "Five":
                    chartShadingEffectSTAT = ChartShadingEffectSTAT.Five;
                    break;
            }
            return chartShadingEffectSTAT;
        }



        /// <summary>
        /// 转换主表的外键条件为哈希表：条件和条件之间必须用大写的 “ AND ” 分隔
        /// 要转换的字符穿不能含有“@”
        /// </summary>
        /// <param name="primaryKeySQL"></param>
        /// <returns></returns>
        public static Hashtable convertPrimaryKeySQLToHashtable(string primaryKeySQL)
        {
            string splitString = primaryKeySQL.Replace(" AND ", "@");
            Hashtable returnValue = new Hashtable();
            string[] conditionArray = splitString.Split(new char[] { '@' });
            for (int i = 0; i < conditionArray.Length; i++)
            {
                string[] condition = conditionArray[i].Split(new char[]{'='});
                if (condition.Length != 2)
                    continue;
                string fieldName = condition[0];
                if (fieldName == "")
                    continue;
                string fieldValue = condition[1];
                if (fieldValue == "")
                    continue;
                if (returnValue[fieldName] == null)
                    returnValue.Add(fieldName, fieldValue);
            }
            return returnValue;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="hashPrimaryTableSQLPK"></param>
        /// <param name="hashChildTablePK"></param>
        /// <returns></returns>
        public static string convertPrimaryTablePKtoChildTableForeignKeySQL(Hashtable hashPrimaryTableSQLPK, Hashtable hashChildTablePK)
        {
            string foreignKeySQL = "";
            string conditionModal = "{0}={1}";
            System.Collections.IDictionaryEnumerator myEnumerator = hashChildTablePK.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                string primaryFieldName = myEnumerator.Key.ToString();
                string relationFieldName = myEnumerator.Value.ToString();
                if (hashPrimaryTableSQLPK[relationFieldName] != null)
                {
                    //找到关联的字段名称
                    string conditionValue = hashPrimaryTableSQLPK[relationFieldName].ToString();
                    if (foreignKeySQL != "")
                        foreignKeySQL += " AND ";
                    foreignKeySQL += string.Format(conditionModal, primaryFieldName, conditionValue);
                }
            }
            return foreignKeySQL;
        }



        /// <summary>
        /// 转换默认函数为具体的值
        /// 函数：自动编号()、登录者姓名()、登录者部门()要单独转换
        /// </summary>
        /// <param name="defaultValueFunction">默认值函数名称</param>
        /// <returns></returns>
        public static string convertDefaultValue(string defaultValueFunction)
        {
            string defaultValue = "";
            switch (defaultValueFunction)
            {
                case "当前日期()":
                    defaultValue = System.DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                    break;
                case "当前月()":
                    DateTime dMonth = DateTime.Now;
                    defaultValue = dMonth.Month.ToString();
                    break;
                case "当前周()":
                    int days = DateTime.Now.Day;
                    int lastWeek = (int)DateTime.Now.AddDays(-days).DayOfWeek + 1;
                    if (lastWeek < 7)
                    {
                        days += lastWeek;
                    }
                    defaultValue = ((days - 1) / 7 + 1).ToString();
                    break;
                case "当前年()":
                    DateTime dYear = DateTime.Now;
                    defaultValue = dYear.Year.ToString();
                    break;
                case "当前日()":
                    DateTime currDay = DateTime.Now;
                    defaultValue = currDay.Day.ToString();
                    break;
                case "当前星期()":
                    DateTime currWeek = DateTime.Now;
                    int weekday = int.Parse(currWeek.DayOfWeek.ToString("D"));
                    switch(weekday)
                    {
                        case  0:
                           defaultValue = "日";
                            break;
                        case  1:
                           defaultValue = "一";
                            break;
                        case  2:
                           defaultValue = "二";
                            break;
                        case  3:
                           defaultValue = "三";
                            break;
                        case  4:
                           defaultValue = "四";
                            break;
                        case  5:
                           defaultValue = "五";
                            break;
                        case  6:
                           defaultValue = "六";
                            break;
                    }
                    break;
                case "自动编号()":
                    defaultValue = "";
                    break;
                case "登录者姓名()":
                    //UserId = Request.Cookies["UserID"].Value;
                    //user = new User(Convert.ToInt32(UserId));
                    //defaultValue = user.EmployeeName;
                    break;
                case "登录者部门()":
                    //UserId = Request.Cookies["UserID"].Value;
                    //user = new User(Convert.ToInt32(UserId));

                    //if (user.EmployeeID > 0)
                    //{
                    //    string[] Departments = user.getDeptPosi();
                    //    if (Departments.Length > 0)
                    //    {
                    //        string[] myDepartment = Departments[0].Split(",".ToCharArray());
                    //        Department department = new Department();
                    //        department.Id = Int32.Parse(myDepartment[0]);
                    //        defaultValue = department.CName;
                    //    }
                    //}
                    //else
                    //{
                    //    string sql = "select Id,SN,CNAME FROM HR_DEPARTMENTS";
                    //    SqlServerDB myDB = new SqlServerDB();
                    //    sql += " where len(SN)=2";
                    //    DataSet SubDept = myDB.ExecDS(sql);
                    //    if (SubDept != null && SubDept.Tables.Count == 1 && SubDept.Tables[0].Rows.Count > 0)
                    //    {
                    //        defaultValue = SubDept.Tables[0].Rows[0]["CNAME"].ToString();
                    //    }
                    //}
                    break;
                default:
                    defaultValue = defaultValueFunction;
                    break;
            }
            return defaultValue;
        }




        /// <summary>
        /// 根据字段的数据类型，生成数据库的数据格式
        /// 如：
        /// 是字符则用单引号括起来
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="primaryKeyType"></param>
        /// <returns></returns>
        public static string convertFieldValue(string oldValue, FieldType fieldType)
        {
            string newValue = oldValue;
            switch (fieldType)
            {
                case FieldType.IDENTITY://整型
                case FieldType.Integer://整型
                case FieldType.Long://整型
                case FieldType.Number://整型
                    newValue = oldValue;
                    break;
                default:
                    newValue = "'" + oldValue + "'";
                    break;
            }
            return newValue;
        }  
    
    }
}
