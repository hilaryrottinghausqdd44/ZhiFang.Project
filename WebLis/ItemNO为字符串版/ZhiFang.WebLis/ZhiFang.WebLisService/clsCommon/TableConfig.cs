using System;
using System.Xml;
using System.Collections;
using System.Configuration;
using System.Data;

namespace ZhiFang.WebLisService.clsCommon
{
    /// <summary>
    /// 用来解析TablesConfig定义的数据库、表结构的类
    /// 并解析对应的数据存储文件TablesData
    /// </summary>
    public class TableConfig
    {
        public static string DataBaseDiskPath = "";//数据库定义XML文件所在的磁盘目录

        public TableConfig()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }




        /// <summary>
        /// 合并AppSystemData类里的查询条件
        /// </summary>
        /// <param name="appSystemData"></param>
        /// <returns></returns>
        public static string mergeALLConditionSQL(AppSystemData appSystemData)
        {
            string conditionSQLALL = appSystemData.ForeignKeySQL;//先加外键
            if (appSystemData.WhereSQL != "")
            {
                if (conditionSQLALL == "")
                    conditionSQLALL = appSystemData.WhereSQL;
                else
                {
                    string whereModal = "({0}) AND ({1})";
                    conditionSQLALL = string.Format(whereModal, conditionSQLALL, appSystemData.WhereSQL);
                }
            }
            return conditionSQLALL;
        }




        /// <summary>
        /// 取一个表的查询条件：包括其主表的条件
        /// </summary>
        /// <param name="queryTableNode"></param>
        /// <returns></returns>
        public static string getQueryConditionScriptFromQueryTableNode(XmlNode queryTableNode, string dbName)
        {
            string tableName = TableConfig.getTableNameFromQueryNode(queryTableNode);
            //取该表的配置信息
            TableConfigInfo tableConfigInfo = TableConfig.getTableConfigInfoFromDatabaseName(dbName, tableName);
            //取一个表的查询条件
            string xPath = "./DFD/Condition";
            XmlNodeList nodelistCondition = queryTableNode.SelectNodes(xPath);
            string sqlCondition = "";//所有的查询条件：Condition与Condition之间是或者的关系
            foreach (XmlNode nodeCondition in nodelistCondition)
            {
                xPath = "./Match";
                XmlNodeList nodelistMatch = nodeCondition.SelectNodes(xPath);
                string fieldCondition = "";//一个Condition条件，里面的Match之间是与的关系
                foreach (XmlNode nodeMatch in nodelistMatch)
                {
                    //
                    if (nodeMatch.Attributes.GetNamedItem("field") == null)
                        continue;
                    string fieldName = nodeMatch.Attributes.GetNamedItem("field").InnerXml;
                    string fieldValue = nodeMatch.Attributes.GetNamedItem("const").InnerXml;
                    string fieldComp = nodeMatch.Attributes.GetNamedItem("comp").InnerXml;
                    //字段的类型
                    FieldType fieldType = FieldType.NONE;
                    if (tableConfigInfo.FieldNameType[fieldName] != null)
                    {
                        fieldType = (FieldType)tableConfigInfo.FieldNameType[fieldName];
                    }
                    //生成SQL查询条件语句
                    string matchCondition = "";
                    if ((fieldName == "true") && (fieldValue == "true") && (fieldValue == "true"))//查true(内部规定)
                        matchCondition = "1=1";
                    else if ((fieldName == "false") && (fieldValue == "false") && (fieldValue == "false"))//查false(内部规定)
                        matchCondition = "1=0";
                    else //正常的查询条件做查询条件转换
                        matchCondition = ConvertData.convertQueryConditionToDatabaseCondition(tableName, fieldName, fieldType, fieldComp, fieldValue, tableConfigInfo.dataBaseLoginInfo.DatabaseType);
                    if (fieldCondition != "")
                    {
                        if (matchCondition != "")
                            fieldCondition += " AND ";
                    }
                    fieldCondition += matchCondition;
                }
                if ((sqlCondition != "") && (fieldCondition != ""))
                {
                    //不同一个条件下是或的关系
                    sqlCondition += "  OR  ";
                }
                //加一个条件
                sqlCondition += fieldCondition;
            }
            return sqlCondition;
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
                        fieldValue = TableConfig.convertFieldValueToDatabase(fieldValue, fieldType, databaseType);
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
                        fieldValue = TableConfig.convertFieldValueToDatabase(fieldValue, fieldType, databaseType);
                        dbCondition = fieldName + dbCondition + fieldValue;
                    }
                    break;
                case "gt"://大于
                    dbCondition = " > ";
                    fieldValue = TableConfig.convertFieldValueToDatabase(fieldValue, fieldType, databaseType);
                    dbCondition = fieldName + dbCondition + fieldValue;
                    break;
                case "ge"://大于等于
                    dbCondition = " >= ";
                    fieldValue = TableConfig.convertFieldValueToDatabase(fieldValue, fieldType, databaseType);
                    dbCondition = fieldName + dbCondition + fieldValue;
                    break;
                case "lt"://小于
                    dbCondition = " < ";
                    fieldValue = TableConfig.convertFieldValueToDatabase(fieldValue, fieldType, databaseType);
                    dbCondition = fieldName + dbCondition + fieldValue;
                    break;
                case "le"://小于等于
                    dbCondition = " <= ";
                    fieldValue = TableConfig.convertFieldValueToDatabase(fieldValue, fieldType, databaseType);
                    dbCondition = fieldName + dbCondition + fieldValue;
                    break;
                case "include"://包含  //bug 有问题 发现2009-03-12, 包含多个
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
        public static string getQueryConditionScriptFromQueryTableNode_1(XmlNode queryTableNode, string dbName)
        {
            string tableName = TableConfig.getTableNameFromQueryNode(queryTableNode);
            //取该表的配置信息
            TableConfigInfo tableConfigInfo = TableConfig.getTableConfigInfoFromDatabaseName(dbName, tableName);
            //取一个表的查询条件

            //生成条件 数据过滤条件+数据分组条件+查询条件+准SQL+SQL
            //DFD1 and DFD2 and …..DFDn
            //DFD1=(C11 OR C12 OR …C1n)
            //……
            //C11=(Match1 and Match2 and … Matchn)


            string xPathAllDFDs = "./DFD";
            XmlNodeList nodelistDFDs = queryTableNode.SelectNodes(xPathAllDFDs);
            string sqlDFD = "";
            foreach (XmlNode nodeDFD in nodelistDFDs)
            {
                string sqlCondition = "";//所有的查询条件：Condition与Condition之间是或者的关系 DFD(and) Match(and)
                string xPath = "Condition";
                XmlNodeList nodelistCondition = nodeDFD.SelectNodes(xPath);
                foreach (XmlNode nodeCondition in nodelistCondition)
                {
                    xPath = "./Match";
                    XmlNodeList nodelistMatch = nodeCondition.SelectNodes(xPath);
                    string fieldCondition = "";//一个Condition条件，里面的Match之间是与的关系
                    foreach (XmlNode nodeMatch in nodelistMatch)
                    {
                        string matchCondition = "";

                        XmlNode MatchType = nodeMatch.Attributes.GetNamedItem("Type");
                        if (MatchType != null && MatchType.InnerXml == "SQL")
                        {
                            if (nodeMatch.InnerXml != "")
                                matchCondition = nodeMatch.InnerXml;

                            matchCondition = matchCondition.Replace("&amp;", "&");

                            matchCondition = matchCondition.Replace("&gt;", ">");
                            matchCondition = matchCondition.Replace("&lt;", "<");
                            matchCondition = matchCondition.Replace("&quot;", "\"\"");
                            matchCondition = matchCondition.Replace("&apos;", "''");

                        }
                        else
                        {
                            if (nodeMatch.Attributes.GetNamedItem("field") == null)
                                continue;
                            string fieldName = nodeMatch.Attributes.GetNamedItem("field").InnerXml;
                            string fieldValue = nodeMatch.Attributes.GetNamedItem("const").InnerXml;
                            string fieldComp = nodeMatch.Attributes.GetNamedItem("comp").InnerXml;
                            //字段的类型
                            FieldType fieldType = FieldType.NONE;
                            if (tableConfigInfo.FieldNameType[fieldName] != null)
                            {
                                fieldType = (FieldType)tableConfigInfo.FieldNameType[fieldName];
                            }
                            //生成SQL查询条件语句
                            if ((fieldName == "true") && (fieldValue == "true") && (fieldValue == "true"))//查true(内部规定)
                                matchCondition = "1=1";
                            else if ((fieldName == "false") && (fieldValue == "false") && (fieldValue == "false"))//查false(内部规定)
                                matchCondition = "1=0";
                            else //正常的查询条件做查询条件转换
                                matchCondition = TableConfig.convertQueryConditionToDatabaseCondition(tableName, fieldName, fieldType, fieldComp, fieldValue, tableConfigInfo.dataBaseLoginInfo.DatabaseType);
                        }
                        if (matchCondition == "" || matchCondition == "()")
                            continue;

                        fieldCondition += " AND (" + matchCondition + ")";
                    }

                    if (fieldCondition.Length > 5)//不同condition条件下是或(or)的关系(去掉最后一个AND)
                    {
                        fieldCondition = fieldCondition.Substring(5);
                        sqlCondition += " OR (" + fieldCondition + ")";
                    }
                }
                if (sqlCondition.Length > 4)//不同DFD条件下是与(and)的关系(去掉上次中的(OR)
                {
                    sqlCondition = sqlCondition.Substring(4);
                    sqlDFD += " AND (" + sqlCondition + ")";
                }
            }

            if (sqlDFD.Length > 5)
                sqlDFD = sqlDFD.Substring(5);

            return sqlDFD;
        }
		

        /// <summary>
        /// 将要查询的表的叶子结点转换成标准的SQL查询条件语句：
        /// 只生成条件的那一部分，即在SQL语句中WHERE后面的条件
        /// </summary>
        /// <param name="leafQueryNode">查询条件的根结点</param>
        /// <returns></returns>
        public static string makeQueryConditionScriptFromQueryNode(XmlNode leafQueryNode, string dbName)
        {

            if (leafQueryNode.Name != "Table")
                return null;
            //取要查询的表的名称
            string tableName = TableConfig.getTableNameFromQueryNode(leafQueryNode);
            if (tableName == "")
                return null;
            string parentTableName = "";
            XmlNode parentQueryNode = leafQueryNode.ParentNode;
            if (parentQueryNode.Name == "Table")
                parentTableName = TableConfig.getTableNameFromTableNode(parentQueryNode);
            TableConfigInfo tableConfigInfo = TableConfig.getTableConfigInfoFromDatabaseName(dbName, tableName);
            //取一个表的查询条件
            string sqlCondition = TableConfig.getQueryConditionScriptFromQueryTableNode_1(leafQueryNode, dbName);
            if (sqlCondition != "")
                sqlCondition = "(" + sqlCondition + ")";
            //递归
            if (parentQueryNode.Name == "Table")
            {
                string sqlConditionParent = TableConfig.makeQueryConditionScriptFromQueryNode(parentQueryNode, dbName);
                if ((sqlConditionParent != "") && (sqlConditionParent != null))
                {
                    if (sqlCondition != "")
                        sqlCondition += " AND ";
                    sqlCondition += sqlConditionParent;
                }
            }
            return sqlCondition;
        }

        public static Array Redim(Array origArray, int desiredSize)
        {
            Type t = origArray.GetType().GetElementType();
            Array newArray = Array.CreateInstance(t, desiredSize);
            Array.Copy(origArray, 0, newArray, 0, Math.Min(origArray.Length, desiredSize));
            return newArray;
        }
        /// <summary>
        /// 汇总字段
        /// </summary>
        /// <param name="queryTableNode"></param>
        /// <param name="dbName"></param>
        /// <returns>返回　sum(1),sum(2)</returns>
        public static string makeQuerySumFieldsFromQueryNode(XmlNode queryTableNode, string dbName,out string[] FieldSumListAsBefore,out string[] FieldList)
        {

            string tableName = TableConfig.getTableNameFromQueryNode(queryTableNode);
            //取该表的配置信息
            TableConfigInfo tableConfigInfo = TableConfig.getTableConfigInfoFromDatabaseName(dbName, tableName);
            //取一个表的查询条件
            string xPath = "Analysis/Field";
            XmlNodeList nodelistCondition = queryTableNode.SelectNodes(xPath);
            string sqlOrderBy = "";//所有的查询条件：Condition与Condition之间是或者的关系
            string[] fieldNameList = new string[0];
            FieldSumListAsBefore = new string[0];
            if (nodelistCondition != null && nodelistCondition.Count > 0)
            {
                int dimFieldNames=0;
                foreach (XmlNode nodeOrderByField in nodelistCondition)
                {
                    string fieldName = nodeOrderByField.Attributes.GetNamedItem("EName").Value;
                    string orderModeDesc = nodeOrderByField.Attributes.GetNamedItem("Rule").Value;
                    sqlOrderBy += "," + orderModeDesc;
                    dimFieldNames++;
                    FieldSumListAsBefore = (string[])Redim(FieldSumListAsBefore, dimFieldNames);
                    FieldSumListAsBefore[dimFieldNames - 1] = fieldName;

                    fieldNameList = (string[])Redim(fieldNameList, dimFieldNames);
                    fieldNameList[dimFieldNames - 1] = orderModeDesc;

                }
                if (sqlOrderBy.Length > 0)
                    sqlOrderBy = sqlOrderBy.Substring(1);
            }
            FieldList = fieldNameList;

            return sqlOrderBy;
        }

        

        /// <summary>
        /// 取排序方式：
        /// 即在SQL语句中ORDER BY后面的条件
        /// </summary>
        /// <param name="leafQueryNode">查询条件的根结点</param>
        /// <returns></returns>
        public static string makeQueryOrderByScriptFromQueryNode(XmlNode queryTableNode, string dbName)
        {

            string tableName = TableConfig.getTableNameFromQueryNode(queryTableNode);
            //取该表的配置信息
            TableConfigInfo tableConfigInfo = TableConfig.getTableConfigInfoFromDatabaseName(dbName, tableName);
            //取一个表的查询条件
            string xPath = "DFD/Orderby/Field";
            XmlNodeList nodelistCondition = queryTableNode.SelectNodes(xPath);
            string sqlOrderBy = "";//所有的查询条件：Condition与Condition之间是或者的关系
            if (nodelistCondition != null && nodelistCondition.Count > 0)
            {
                string fieldNameModal = "[{0}].[{1}] {2}";
                foreach (XmlNode nodeOrderByField in nodelistCondition)
                {
                    string fieldName = nodeOrderByField.Attributes.GetNamedItem("EName").Value;
                    string orderMode = "asc";
                    string orderModeDesc = nodeOrderByField.Attributes.GetNamedItem("Sort").Value;
                    if (orderModeDesc == "1")
                        orderMode = "desc";
                    sqlOrderBy += "," + string.Format(fieldNameModal, tableName, fieldName, orderMode);
                }
                if (sqlOrderBy.Length > 0)
                    sqlOrderBy = sqlOrderBy.Substring(1);
            }
            //如果查询条件为空，则默认按主关键字倒序排序
            if (sqlOrderBy == "")
            {
                System.Collections.IDictionaryEnumerator myEnumerator = tableConfigInfo.ForeignKey.GetEnumerator();
                string fieldNameModal = "[{0}].[{1}] {2}";
                while (myEnumerator.MoveNext())
                {
                    string fieldName = myEnumerator.Key.ToString();
                    string orderMode = "desc";
                    if (sqlOrderBy != "")
                        sqlOrderBy += ",";
                    sqlOrderBy += string.Format(fieldNameModal, tableName, fieldName, orderMode);
                }
                myEnumerator = tableConfigInfo.PrimaryKey.GetEnumerator();
                while (myEnumerator.MoveNext())
                {
                    string fieldName = myEnumerator.Key.ToString();
                    string orderMode = "desc";
                    if (sqlOrderBy != "")
                        sqlOrderBy += ",";
                    sqlOrderBy += string.Format(fieldNameModal, tableName, fieldName, orderMode);
                }
            }
            return sqlOrderBy;
        }


        /// <summary>
        /// 根据表的配置信息，生成创建表结构的SQLServer脚本
        /// </summary>
        /// <param name="tableConfigInfo">表的配置信息</param>
        /// <returns></returns>
        public static string makeCreateTableSQLServerScript(TableConfigInfo tableConfigInfo)
        {
            //生成的SQL脚本
            string sql = "";
            if ((tableConfigInfo.TableName == "") || (tableConfigInfo.TableName == null))
                return sql;
            //命令后缀
            string GO = "\n";//\nGO\n
            //脚本模板定义
            //定义字段描述设置的格式，参数次序：描述、表、字段
            string addDescriptioSqlModal = "exec sp_addextendedproperty N'MS_Description', N'{0}', N'user', N'dbo', N'table', N'{1}', N'column', N'{2}'" + GO;
            //设置外来关键字，格式为：表名、外键名称、字段名称
            string addForeignKeySqlModal = "ALTER TABLE [dbo].[{0}] ADD CONSTRAINT FK_{0}_{2}_{4} FOREIGN KEY ({1}) REFERENCES [dbo].[{2}] ({3})" + GO;
            //设置主关键字，格式为：表名、主键名称、字段名称
            string addPrimaryKeySqlModal = "ALTER TABLE [dbo].[{0}] ADD CONSTRAINT [{1}] PRIMARY KEY  CLUSTERED ({2})  ON [PRIMARY]" + GO;
            //创建表结构的脚本，格式为：表的名称、字段列表
            string createTableSqlModal = "CREATE TABLE  [dbo].[{0}] (\n{1})" + GO;

            //具体的SQL脚本
            string addDescriptioSQL = "";//设置描述
            string addForeignKeySQL = "";//设置外键
            string addPrimaryKeySQL = "";//设置主键
            string createTableSQL = "";  //创建表
            string fieldListSQL = "";//字段列表
            string foreignKeyList = "";//外键的列表，以后生成主键要用 
            string foreignKeyRelationList = "";//与外键关联的字段，以后生成外键要用 

            //当前操作的表名称
            string tableName = tableConfigInfo.TableName;


            //取外键：外键的字段加到“字段列表”，同时生成设置外键的脚本
            string foreignKeyName = "";
            System.Collections.IEnumerator enumeratorFK = tableConfigInfo.ForeignKeyStack.GetEnumerator();//定义外键要有次序
            while (enumeratorFK.MoveNext())
            {
                foreignKeyName = enumeratorFK.Current.ToString();
                if (tableConfigInfo.ForeignKeyRelation[foreignKeyName] == null)
                {
                    //没有定义关联字段，应该有错
                    continue;
                }
                string relationName = tableConfigInfo.ForeignKeyRelation[foreignKeyName].ToString();
                FieldType fieldType = (FieldType)tableConfigInfo.ForeignKeyType[foreignKeyName];
                //外键不能是自增的，换为长整形
                if (fieldType == FieldType.IDENTITY)
                    fieldType = FieldType.Long;
                string foreignKeyFieldType = ConvertData.convertFieldTypeToSQLServer(fieldType);
                if (foreignKeyList != "")
                    foreignKeyList += ",";
                if (fieldListSQL != "")
                    fieldListSQL += ",\n";
                if (foreignKeyRelationList != "")
                    foreignKeyRelationList += ",";
                foreignKeyList += foreignKeyName;
                foreignKeyRelationList += relationName;
                //定义关键字的字段格式：（字段名称，类型）
                string foreignFieldSqlModal = "\t[{0}] {1} NOT NULL";
                fieldListSQL += string.Format(foreignFieldSqlModal, foreignKeyName, foreignKeyFieldType);//加字段、类型
            }
            if (foreignKeyRelationList != "")
                addForeignKeySQL = string.Format(addForeignKeySqlModal, tableName, foreignKeyList, tableConfigInfo.PrimaryTable, foreignKeyRelationList, foreignKeyName);
            //加主键
            System.Collections.IDictionaryEnumerator myEnumeratorPK = tableConfigInfo.PrimaryKeyType.GetEnumerator();
            string primaryKeyFieldName = foreignKeyList;
            string fieldNamePK = "";
            while (myEnumeratorPK.MoveNext())
            {
                fieldNamePK = myEnumeratorPK.Key.ToString();
                //定义关键字的字段格式：（字段名称，类型）
                string primaryFieldSqlModal = "\t[{0}] {1} NOT NULL";
                //取当前表的主关键字类型
                FieldType fieldType = (FieldType)myEnumeratorPK.Value;
                string primaryKeyFieldType = ConvertData.convertFieldTypeToSQLServer(fieldType);
                if (fieldListSQL != "")
                    fieldListSQL += ",\n";
                fieldListSQL += string.Format(primaryFieldSqlModal, fieldNamePK, primaryKeyFieldType);
                if (primaryKeyFieldName != "")
                    primaryKeyFieldName += ",";
                primaryKeyFieldName += fieldNamePK;
            }
            //设置主关键字，格式为：表名、主键名称、字段名称
            addPrimaryKeySQL = string.Format(addPrimaryKeySqlModal, tableName, "PK_" + tableName + "_" + fieldNamePK, primaryKeyFieldName);
            //取字段列表，同时生成描述
            System.Collections.IDictionaryEnumerator myEnumerator = tableConfigInfo.FieldNameDesc.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                string fieldName = myEnumerator.Key.ToString();//名称
                string fieldDesc = myEnumerator.Value.ToString();//描述
                addDescriptioSQL += string.Format(addDescriptioSqlModal, fieldDesc, tableName, fieldName);
                myEnumeratorPK = tableConfigInfo.PrimaryKeyType.GetEnumerator();
                bool isAdd = false;
                while (myEnumeratorPK.MoveNext())
                {
                    if (fieldName == myEnumeratorPK.Key.ToString())
                    {
                        isAdd = true;
                        break;
                    }
                }
                //主键：字段已经加了
                if (isAdd)
                    continue;
                string fieldSql = "\t[" + fieldName + "]";
                FieldType fieldType = (FieldType)tableConfigInfo.FieldNameType[myEnumerator.Key];//字段类型
                string fieldTypeSQL = ConvertData.convertFieldTypeToSQLServer(fieldType);
                fieldSql += " " + fieldTypeSQL;
                if (fieldListSQL != "")
                    fieldListSQL += ",\n";
                fieldListSQL += fieldSql;
            }
            //删除表的脚本
            string dropTableSqlModal = "if exists (select 1 from  sysobjects where  id = object_id('{0}') and  type = 'U') drop table [{0}]" + GO;
            string dropTableSql = string.Format(dropTableSqlModal, tableName);
            //创建表结构的脚本
            if (fieldListSQL == "")
                return null;
            createTableSQL = string.Format(createTableSqlModal, tableName, fieldListSQL);
            sql = createTableSQL + addForeignKeySQL + addPrimaryKeySQL + addDescriptioSQL;
            //System.Console.WriteLine(sql);
            return sql;
        }


        /// <summary>
        /// 生成某个数据库下某个表及其子表的创建表结构的脚本
        /// 使用递归进行遍历
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string makeTableConfigSQLServerScript(string dbName, string tableName)
        {
            //生成的SQL脚本
            string sql = "";
            string configFileName = TableConfig.getTableConfigDiskFileName(dbName);
            if ((configFileName == "") || (configFileName == null))
                return sql;
            //文件不存在
            if (System.IO.File.Exists(configFileName) == false)
                return sql;
            //取表的配置信息
            TableConfigInfo tableConfigInfo = TableConfig.getTableConfigInfoFromDatabaseName(dbName, tableName);
            //生成当前表的脚本
            sql = TableConfig.makeCreateTableSQLServerScript(tableConfigInfo);
            //在配置文件定位到当前表的子表
            XmlDocument doc = new XmlDocument();
            doc.Load(configFileName);
            string xPath = "//Table[@EName='" + tableName + "']/tr/Table";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            //子表的脚本：使用递归方法
            string sqlChild = "";
            foreach (XmlNode node in nodelist)
            {
                //存在子表
                string childTable = node.Attributes.GetNamedItem("EName").InnerXml;
                sqlChild += TableConfig.makeTableConfigSQLServerScript(dbName, childTable);
            }
            return sql + sqlChild;
        }




        /// <summary>
        /// 生成一个记录插入数据的INSERT 脚本
        /// </summary>
        /// <param name="tableConfigInfo"></param>
        /// <returns></returns>
        public static string makeInsertRecordSQLServerScript(TableConfigInfo tableConfigInfo)
        {
            string insertSQL = "";
            string fieldList = "";//字段列表
            string valueList = "";//值列表
            //查看有没有外键：有的话先加外键的值
            //遍历数据字段
            System.Collections.IDictionaryEnumerator myEnumerator = tableConfigInfo.ForeignKey.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                string fieldName = tableConfigInfo.ForeignKeyRelation[myEnumerator.Key].ToString();
                if (myEnumerator.Value == null)
                    continue;
                string fieldValue = myEnumerator.Value.ToString();
                FieldType fieldType = (FieldType)tableConfigInfo.ForeignKeyType[myEnumerator.Key];
                if (fieldList != "")
                {
                    fieldList += ",";
                    valueList += ",";
                }
                //外键的模式
                fieldList += "[" + fieldName + "]";
                valueList += ConvertData.convertFieldValue(fieldValue, fieldType);
            }
            //遍历数据字段
            myEnumerator = tableConfigInfo.FieldNameValue.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                string fieldName = myEnumerator.Key.ToString();
                string fieldValue = myEnumerator.Value.ToString();
                //有内容
                if ((fieldValue != "") && (fieldValue != null))
                {
                    if (fieldList != "")
                    {
                        fieldList += ",";
                        valueList += ",";
                    }
                    //字段名称
                    fieldList += "[" + fieldName + "]";
                    //字段类型
                    FieldType fieldType = FieldType.NONE;
                    if (tableConfigInfo.FieldNameType[fieldName] != null)
                    {
                        fieldType = (FieldType)tableConfigInfo.FieldNameType[fieldName];
                        fieldValue = ConvertData.convertFieldValue(fieldValue, fieldType);
                    }
                    valueList += fieldValue;

                }
            }
            if (fieldList != "")//有字段
            {

                //定义SQL插入数据脚本模版
                string insertSqlModal = "INSERT INTO [{0}] ({1}) VALUES({2})";
                insertSQL = string.Format(insertSqlModal, tableConfigInfo.TableName, fieldList, valueList);
            }
            return insertSQL;
        }


        /// <summary>
        /// 将一个TABLE结点下的所有数据，包括子表：生成INSERT INTO table 脚本
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="tableNode"></param>
        /// <returns></returns>
        public static string makeInsertSQLServerScriptFromTableNode(string dbName, XmlNode tableNode)
        {
            string sql = "";
            //传进来的XmlNode结点应该是根目录下的一个Table结点
            if (tableNode.Name != "Table")
            {
                return null;
            }
            //取到表的名称
            string tableName = tableNode.Attributes.GetNamedItem("EName").InnerXml;
            //取表的配置信息
            TableConfigInfo tableConfigInfo = TableConfig.getTableConfigInfoFromDatabaseName(dbName, tableName);
            //遍历该结点（表）下的记录
            string xPath = "./tr";
            XmlNodeList nodelistTR = tableNode.SelectNodes(xPath);
            foreach (XmlNode trNode in nodelistTR)
            {
                xPath = "./td";
                //取到该记录
                tableConfigInfo.FieldNameValue = (Hashtable)TableConfig.getRecordDataFromXmlNodeTR(trNode).Clone();
                //取外键的值
                if (tableConfigInfo.PrimaryTable != "")
                {
                    if (trNode.ParentNode.ParentNode != null)
                    {
                        //
                    }

                }
                //生成一个Record脚本
                string insertRecord = TableConfig.makeInsertRecordSQLServerScript(tableConfigInfo);
                sql += insertRecord + "\nGO\n";
                //查看有没有子表
                xPath = "./Table";
                XmlNodeList nodelistChild = trNode.SelectNodes(xPath);
                foreach (XmlNode nodeChildTable in nodelistChild)
                {
                    sql += makeInsertSQLServerScriptFromTableNode(dbName, nodeChildTable);
                }
            }
            return sql;
        }


        /// <summary>
        /// 从数据库配置信息里生成数据库连接串
        /// </summary>
        /// <param name="dataBaseLoginInfo"></param>
        /// <returns></returns>
        public static string getDataBaseConnectionString(DataBaseLoginInfo dataBaseLoginInfo)
        {
            string connectionString = "";
            switch (dataBaseLoginInfo.DatabaseType)
            {
                case DatabaseType.SQLServer:
                    string connStringSQLServerModal = "server={0};uid={1};pwd={2};database={3};";
                    connectionString = string.Format(connStringSQLServerModal, dataBaseLoginInfo.IP, dataBaseLoginInfo.UserName, dataBaseLoginInfo.PassWord, dataBaseLoginInfo.DatabaseName);
                    break;
                case DatabaseType.Oracle:
                    string connStringOracleModal = "Data Source=(DESCRIPTION =(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT = {1})))(CONNECT_DATA=(SERVICE_NAME={2})));User Id={3};Password={4};";
                    connectionString = string.Format(connStringOracleModal, dataBaseLoginInfo.IP, dataBaseLoginInfo.TCP, dataBaseLoginInfo.DatabaseName, dataBaseLoginInfo.UserName, dataBaseLoginInfo.PassWord);
                    break;
                default:
                    string connStringDefaultModal = "server={0};uid={1};pwd={2};database={3};";
                    connectionString = string.Format(connStringDefaultModal, dataBaseLoginInfo.IP, dataBaseLoginInfo.UserName, dataBaseLoginInfo.PassWord, dataBaseLoginInfo.DatabaseName);
                    break;
            }
            return connectionString;
        }


        /// <summary>
        /// 取某个表的主表
        /// </summary>
        /// <param name="xmlConfigFileName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getParentTable(string xmlConfigFileName, string tableName)
        {
            if ((xmlConfigFileName == "") || (xmlConfigFileName == null))
                return null;
            //文件不存在
            if (System.IO.File.Exists(xmlConfigFileName) == false)
                return null;
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlConfigFileName);
            //先定位到当前表
            Hashtable hashtable = new Hashtable();
            string xPath = "//Table[@EName='" + tableName + "']";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            if (nodelist.Count == 1)
            {
                //找到当前表，判断是否有主表，有则取主表信息返回
                if (nodelist[0].ParentNode.ParentNode != null)
                {
                    XmlNode node = nodelist[0].ParentNode.ParentNode;
                    if (node.Name == "Table")
                    {
                        string parentTableName = node.Attributes.GetNamedItem("EName").InnerXml;
                        string tablePrimaryKey = getPrimaryKeyName(xmlConfigFileName, parentTableName); ;
                        if (hashtable[tableName] == null)
                            hashtable.Add(tableName, tablePrimaryKey);
                    }
                }

            }
            return hashtable;
        }


        /// <summary>
        /// 取某个表的主表
        /// </summary>
        /// <param name="xmlConfigFileName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string getParentTableName(string xmlConfigFileName, string tableName)
        {
            string parentTableName = "";
            if ((xmlConfigFileName == "") || (xmlConfigFileName == null))
                return parentTableName;
            //文件不存在
            if (System.IO.File.Exists(xmlConfigFileName) == false)
                return parentTableName;
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlConfigFileName);
            //先定位到当前表
            string xPath = "//Table[@EName='" + tableName + "']";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            if (nodelist.Count == 1)
            {
                //找到当前表，判断是否有主表，有则取主表信息返回
                if (nodelist[0].ParentNode.ParentNode != null)
                {
                    XmlNode node = nodelist[0].ParentNode.ParentNode;
                    if (node.Name == "Table")
                    {
                        //取到主表名称
                        parentTableName = node.Attributes.GetNamedItem("EName").InnerXml;
                    }
                }

            }
            return parentTableName;
        }


        /// <summary>
        /// 从配置文件中,取某个应用系统下的所有表的名称和描述:返回哈希表【表名称，表描述】
        /// </summary>
        /// <param name="systemName"></param>
        /// <returns></returns>
        public static System.Collections.Hashtable getAllTableNameDescFromSystemName(string systemName)
        {
            Hashtable hashtable = new Hashtable();
            string configFileName = TableConfig.getAppSystemConfigDiskFileName(systemName);
            if (configFileName == null)
                return hashtable;
            XmlDocument doc = new XmlDocument();
            doc.Load(configFileName);
            string xPath = "//Table";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            foreach (XmlNode node in nodelist)
            {
                string tableName = node.Attributes.GetNamedItem("EName").InnerXml;
                string tableDesc = node.Attributes.GetNamedItem("TableCName").InnerXml;
                if (hashtable[tableName] == null)
                    hashtable.Add(tableName, tableDesc);
            }
            return hashtable;
        }


        /// <summary>
        /// 从应用系统配置文件中,取某个应用系统下的所有表的名称和描述:返回哈希表【表描述，表名称】
        /// </summary>
        /// <param name="systemName"></param>
        /// <returns></returns>
        public static System.Collections.Hashtable getAllTableDescNameFromSystemName(string systemName)
        {
            Hashtable hashtable = new Hashtable();
            string configFileName = TableConfig.getAppSystemConfigDiskFileName(systemName);
            if (configFileName == null)
                return hashtable;
            XmlDocument doc = new XmlDocument();
            doc.Load(configFileName);
            string xPath = "//Table";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            foreach (XmlNode node in nodelist)
            {
                string tableName = node.Attributes.GetNamedItem("EName").InnerXml;
                string tableDesc = node.Attributes.GetNamedItem("TableCName").InnerXml;
                if (hashtable[tableDesc] == null)
                    hashtable.Add(tableDesc, tableName);
            }
            return hashtable;
        }


        /// <summary>
        /// 从配置文件中,取某个数据库下的所有表的名称和描述:SortedList
        /// </summary>
        /// <param name="systemName"></param>
        /// <returns></returns>
        public static System.Collections.SortedList getAllTableNameDescSortListFromSystemName(string systemName)
        {
            //取定义的字段名称和描述列表
            Hashtable hashtable = TableConfig.getAllTableNameDescFromSystemName(systemName);
            //排序
            System.Collections.SortedList sortedList = new SortedList();
            System.Collections.IDictionaryEnumerator myEnumerator = hashtable.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                sortedList.Add(myEnumerator.Key, myEnumerator.Value);
            }
            return sortedList;
        }






        /// <summary>
        /// 从配置文件中,取某个数据库下的所有表的名称和描述:返回哈希表
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public static System.Collections.Hashtable getAllTableNameDescFromDatabaseName(string dbName)
        {
            Hashtable hashtable = new Hashtable();
            string configFileName = TableConfig.getTableConfigDiskFileName(dbName);
            if (configFileName == null)
                return hashtable;
            XmlDocument doc = new XmlDocument();
            doc.Load(configFileName);
            string xPath = "//Table";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            foreach (XmlNode node in nodelist)
            {
                string tableName = node.Attributes.GetNamedItem("EName").InnerXml;
                string tableDesc = node.Attributes.GetNamedItem("TableCName").InnerXml;
                if (hashtable[tableName] == null)
                    hashtable.Add(tableName, tableDesc);
            }
            return hashtable;
        }



        /// <summary>
        /// 从配置文件中,取某个数据库下的所有表的名称和描述:返回哈希表
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public static System.Collections.Hashtable getAllTableDescNameFromDatabaseName(string dbName)
        {
            Hashtable hashtable = new Hashtable();
            string configFileName = TableConfig.getTableConfigDiskFileName(dbName);
            if (configFileName == null)
                return hashtable;
            XmlDocument doc = new XmlDocument();
            doc.Load(configFileName);
            string xPath = "//Table";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            foreach (XmlNode node in nodelist)
            {
                string tableName = node.Attributes.GetNamedItem("EName").InnerXml;
                string tableDesc = node.Attributes.GetNamedItem("TableCName").InnerXml;
                if (hashtable[tableDesc] == null)
                    hashtable.Add(tableDesc, tableName);
            }
            return hashtable;
        }




        /// <summary>
        /// 从配置文件中,取某个数据库下的所有表的名称和描述:SortedList
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public static System.Collections.SortedList getAllTableNameDescSortedListFromDatabaseName(string dbName)
        {
            //取定义的字段名称和描述列表
            Hashtable hashtable = TableConfig.getAllTableNameDescFromDatabaseName(dbName);
            //排序
            System.Collections.SortedList sortedList = new SortedList();
            System.Collections.IDictionaryEnumerator myEnumerator = hashtable.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                sortedList.Add(myEnumerator.Key, myEnumerator.Value);
            }
            return sortedList;
        }



        /// <summary>
        /// 从配置文件中,取某个数据库下的所有表的名称
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public static System.Collections.Stack getAllTableNameFromDatabaseName(string dbName)
        {
            Stack returnValue = new Stack();
            string configFileName = TableConfig.getTableConfigDiskFileName(dbName);
            if (configFileName == null)
                return returnValue;
            XmlDocument doc = new XmlDocument();
            doc.Load(configFileName);
            string xPath = "//Table";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            foreach (XmlNode node in nodelist)
            {
                string tableName = node.Attributes.GetNamedItem("EName").InnerXml;
                returnValue.Push(tableName);
            }
            return returnValue;
        }



        /// <summary>
        /// 取定义的表的字段名称、字段描述，放到哈希表
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static System.Collections.Hashtable getTableFieldNameDesc(string xmlConfigFileName, string tableName)
        {
            if ((xmlConfigFileName == "") || (xmlConfigFileName == null))
                return null;
            //文件不存在
            if (System.IO.File.Exists(xmlConfigFileName) == false)
                return null;
            Hashtable hashtable = new Hashtable();
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlConfigFileName);
            string xPath = "//Table[@EName='" + tableName + "']/tr/td";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            foreach (XmlNode node in nodelist)
            {
                string fieldName = node.Attributes.GetNamedItem("ColumnEName").InnerXml;//名称
                string fieldDesc = node.Attributes.GetNamedItem("ColumnCName").InnerXml;//描述
                if (hashtable[fieldName] == null)
                    hashtable.Add(fieldName, fieldDesc);
            }
            return hashtable;
        }





        /// <summary>
        /// 取定义的表的字段名称、字段描述，放到哈希表
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static System.Collections.Hashtable getTableFieldNameDescFromSystemName(string systemName, string tableName)
        {
            Hashtable hashtable = new Hashtable();
            string xmlConfigFileName = TableConfig.getAppSystemConfigDiskFileName(systemName);
            if ((xmlConfigFileName == "") || (xmlConfigFileName == null))
                return hashtable;
            //文件不存在
            if (System.IO.File.Exists(xmlConfigFileName) == false)
                return hashtable;
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlConfigFileName);
            string xPath = "//Table[@EName='" + tableName + "']/tr/td";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            foreach (XmlNode node in nodelist)
            {
                string fieldName = node.Attributes.GetNamedItem("ColumnEName").InnerXml;//名称
                string fieldDesc = node.Attributes.GetNamedItem("ColumnCName").InnerXml;//描述
                if (hashtable[fieldName] == null)
                    hashtable.Add(fieldName, fieldDesc);
            }
            return hashtable;
        }





        /// <summary>
        /// 取定义的表的字段名称、字段描述，放到哈希表
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static System.Collections.Hashtable getTableFieldDescNameFromSystemName(string systemName, string tableName)
        {
            Hashtable hashtable = new Hashtable();
            string xmlConfigFileName = TableConfig.getAppSystemConfigDiskFileName(systemName);
            if ((xmlConfigFileName == "") || (xmlConfigFileName == null))
                return hashtable;
            //文件不存在
            if (System.IO.File.Exists(xmlConfigFileName) == false)
                return hashtable;
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlConfigFileName);
            string xPath = "//Table[@EName='" + tableName + "']/tr/td";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            foreach (XmlNode node in nodelist)
            {
                string fieldName = node.Attributes.GetNamedItem("ColumnEName").InnerXml;//名称
                string fieldDesc = node.Attributes.GetNamedItem("ColumnCName").InnerXml;//描述
                if (hashtable[fieldDesc] == null)
                    hashtable.Add(fieldDesc, fieldName);
            }
            return hashtable;
        }



        /// <summary>
        /// 取定义的表的字段描述、字段名称，放到哈希表
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static System.Collections.Hashtable getTableFieldDescName(string xmlConfigFileName, string tableName)
        {
            if ((xmlConfigFileName == "") || (xmlConfigFileName == null))
                return null;
            //文件不存在
            if (System.IO.File.Exists(xmlConfigFileName) == false)
                return null;
            Hashtable hashtable = new Hashtable();
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlConfigFileName);
            string xPath = "//Table[@EName='" + tableName + "']/tr/td";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            foreach (XmlNode node in nodelist)
            {
                string fieldName = node.Attributes.GetNamedItem("ColumnEName").InnerXml;//名称
                string fieldDesc = node.Attributes.GetNamedItem("ColumnCName").InnerXml;//描述
                if (hashtable[fieldDesc] == null)
                    hashtable.Add(fieldDesc, fieldName);
            }
            return hashtable;
        }



        /// <summary>
        /// 从配置文件里取定义的表的字段名称、字段类型FieldType，放到哈希表
        /// </summary>
        /// <param name="xmlConfigFileName">配置文件</param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static System.Collections.Hashtable getTableFieldNameType(string xmlConfigFileName, string tableName)
        {
            Hashtable hashtable = new Hashtable();
            if ((xmlConfigFileName == "") || (xmlConfigFileName == null))
                return hashtable;
            //文件不存在
            if (System.IO.File.Exists(xmlConfigFileName) == false)
                return hashtable;
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlConfigFileName);
            string xPath = "//Table[@EName='" + tableName + "']/tr/td";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            foreach (XmlNode node in nodelist)
            {
                string fieldName = node.Attributes.GetNamedItem("ColumnEName").InnerXml;//名称
                string fieldTypeXML = node.Attributes.GetNamedItem("ColumnType").InnerXml;//类型
                FieldType fieldType = ConvertData.convertXmlFieldType(fieldTypeXML);
                if (hashtable[fieldName] == null)
                    hashtable.Add(fieldName, fieldType);
            }
            return hashtable;
        }



        /// <summary>
        /// 从“应用系统”配置文件里取定义的表的字段名称、字段类型FieldType，放到哈希表
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static System.Collections.Hashtable getTableFieldNameTypeFromSystemName(string systemName, string tableName)
        {
            //取该应用的配置文件名称
            string systemConfigFileName = TableConfig.getAppSystemConfigDiskFileName(systemName);
            return TableConfig.getTableFieldNameType(systemConfigFileName, tableName);
        }



        /// <summary>
        /// 从“数据库”配置文件里取定义的表的字段名称、字段类型FieldType，放到哈希表
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static System.Collections.Hashtable getTableFieldNameTypeFromDataBaseName(string dbName, string tableName)
        {
            //取该数据库的配置文件名称
            string dbConfigFileName = TableConfig.getTableConfigDiskFileName(dbName);
            return TableConfig.getTableFieldNameType(dbConfigFileName, tableName);
        }





        /// <summary>
        /// 从配置文件里取定义的表的字段描述、字段类型FieldType，放到哈希表
        /// </summary>
        /// <param name="xmlConfigFileName">配置文件</param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static System.Collections.Hashtable getTableFieldDescType(string xmlConfigFileName, string tableName)
        {
            Hashtable hashtable = new Hashtable();
            if ((xmlConfigFileName == "") || (xmlConfigFileName == null))
                return hashtable;
            //文件不存在
            if (System.IO.File.Exists(xmlConfigFileName) == false)
                return hashtable;
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlConfigFileName);
            string xPath = "//Table[@EName='" + tableName + "']/tr/td";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            foreach (XmlNode node in nodelist)
            {
                string fieldName = node.Attributes.GetNamedItem("ColumnEName").InnerXml;//名称
                string fieldDesc = node.Attributes.GetNamedItem("ColumnCName").InnerXml;//描述
                string fieldTypeXML = node.Attributes.GetNamedItem("ColumnType").InnerXml;//类型
                FieldType fieldType = ConvertData.convertXmlFieldType(fieldTypeXML);
                if (hashtable[fieldDesc] == null)
                    hashtable.Add(fieldDesc, fieldType);
            }
            return hashtable;
        }



        /// <summary>
        /// 从“应用系统”配置文件里取定义的表的字段描述、字段类型FieldType，放到哈希表
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static System.Collections.Hashtable getTableFieldDescTypeFromSystemName(string systemName, string tableName)
        {
            //取该应用的配置文件名称
            string systemConfigFileName = TableConfig.getAppSystemConfigDiskFileName(systemName);
            return TableConfig.getTableFieldDescType(systemConfigFileName, tableName);
        }



        /// <summary>
        /// 从“数据库”配置文件里取定义的表的字段描述、字段类型FieldType，放到哈希表
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static System.Collections.Hashtable getTableFieldDescTypeFromDataBaseName(string dbName, string tableName)
        {
            //取该数据库的配置文件名称
            string dbConfigFileName = TableConfig.getTableConfigDiskFileName(dbName);
            return TableConfig.getTableFieldDescType(dbConfigFileName, tableName);
        }





        /// <summary>
        /// 从配置文件里取某个表的主关键字
        /// </summary>
        /// <param name="configFileName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string getPrimaryKeyName(string configFileName, string tableName)
        {
            string fieldName = "";
            if (configFileName == null)
                return fieldName;
            XmlDocument doc = new XmlDocument();
            doc.Load(configFileName);
            string xPath = "//Table[@EName='" + tableName + "']/tr/td[@KeyIndex='Yes']";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            if (nodelist.Count == 1)
            {
                fieldName = nodelist[0].Attributes.GetNamedItem("ColumnEName").Value;
            }
            return fieldName;
        }


        /// <summary>
        /// 从配置文件里取某个表的主关键字
        /// </summary>ColumnDefault
        /// <param name="configFileName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string getPrimaryKeyDefaultValue(string configFileName, string tableName)
        {
            string defaultValue = "";
            XmlDocument doc = new XmlDocument();
            doc.Load(configFileName);
            string xPath = "//Table[@EName='" + tableName + "']/tr/td[@KeyIndex='Yes']";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            if (nodelist.Count == 1)
            {
                defaultValue = nodelist[0].Attributes.GetNamedItem("ColumnDefault").Value;
            }
            return defaultValue;
        }


        /// <summary>
        /// 从配置文件里取某个表的主关键字
        /// </summary>
        /// <param name="configFileName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string getPrimaryKeyType(string configFileName, string tableName)
        {
            string fieldType = "";
            XmlDocument doc = new XmlDocument();
            doc.Load(configFileName);
            string xPath = "//Table[@EName='" + tableName + "']/tr/td[@KeyIndex='Yes']";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            if (nodelist.Count == 1)
            {
                fieldType = nodelist[0].Attributes.GetNamedItem("ColumnType").Value;
            }
            return fieldType;
        }


        /// <summary>
        /// 取所有的应用系统列表:返回哈希表(系统名称,数据库名称)
        /// </summary>
        /// <param name="onlyGetDB">如果为true则只取数据库类型为数据库的</param>
        /// <returns></returns>
        public static System.Collections.Hashtable getSystemList(bool onlyGetDB)
        {
            string systemFileName = TableConfig.getSystemDiskFileName();
            if (System.IO.File.Exists(systemFileName) == false)
                return null;
            //取所有的数据库列表
            System.Collections.Hashtable dbHashTable = TableConfig.getDatabaseList(onlyGetDB);
            XmlDocument doc = new XmlDocument();
            doc.Load(systemFileName);
            //遍历所有的应用系统
            string xPath = "//dbSetting";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            System.Collections.Hashtable hashtable = new Hashtable();
            foreach (XmlNode node in nodelist)
            {
                string systemName = node.Attributes["ModuleName"].InnerXml;
                string dbName = node.Attributes["DatabaseName"].InnerXml;
                if (dbHashTable[dbName] != null)
                {
                    if (hashtable[systemName] == null)
                    {
                        hashtable.Add(systemName, dbName);
                    }
                }
            }
            return hashtable;
        }


        /// <summary>
        /// 取所有的应用系统列表:返回SortedList(系统名称,数据库名称)
        /// </summary>
        /// <param name="onlyGetDB">如果为true则只取数据库类型为数据库的</param>
        /// <returns></returns>
        public static System.Collections.SortedList getSystemSortedList(bool onlyGetDB)
        {
            SortedList ret = new SortedList();
            System.Collections.Hashtable dbHashtable = TableConfig.getSystemList(onlyGetDB);
            System.Collections.IDictionaryEnumerator myEnumerator = dbHashtable.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                ret.Add(myEnumerator.Key, myEnumerator.Value);
            }
            return ret;
        }



        /// <summary>
        /// 取所有的数据库列表:
        /// </summary>
        /// <param name="onlyGetDB">如果为true则只取数据库类型为数据库的 </param>
        /// <returns></returns>
        public static System.Collections.SortedList getDatabaseSortedList(bool onlyGetDB)
        {
            SortedList ret = new SortedList();
            System.Collections.Hashtable dbHashtable = TableConfig.getDatabaseList(onlyGetDB);
            System.Collections.IDictionaryEnumerator myEnumerator = dbHashtable.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                ret.Add(myEnumerator.Key, myEnumerator.Value);
            }
            return ret;
        }


        /// <summary>
        /// 取所有的数据库列表:
        /// </summary>
        /// <param name="onlyGetDB">如果为true则只取数据库类型为数据库的 </param>
        /// <returns></returns>
        public static System.Collections.Hashtable getDatabaseList(bool onlyGetDB)
        {
            //返回的哈希表
            System.Collections.Hashtable dbHashtable = new Hashtable();
            string dbConfigFileName = TableConfig.getDatabaseDiskFileName();
            if (System.IO.File.Exists(dbConfigFileName) == false)
                return null;
            XmlDocument doc = new XmlDocument();
            doc.Load(dbConfigFileName);
            //遍历所有的数据库
            string xPath = "//dbSetting";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            foreach (XmlNode node in nodelist)
            {
                string dbName = node.Attributes["DatabaseName"].InnerXml;
                if (node.ChildNodes.Count > 0)
                {
                    System.Collections.Hashtable hash = clsCommon.GetXMLData.getXmlNodeNameAndValue(node);
                    if (hash["DatabaseType"] == null)
                        continue;
                    if (hash["DatabaseType"].ToString() == "0")
                        continue;
                    if (dbHashtable[dbName] == null)
                    {
                        dbHashtable.Add(dbName, dbName);
                    }
                }
            }
            return dbHashtable;
        }



        /// <summary>
        /// 获取数据库的配置文件所在的目录：
        /// 如D:\WebSite\QMSData\shared\Configuration\ConfigurationData\1
        /// </summary>
        /// <returns></returns>
        public static string getDatabaseDiskPath()
        {
            string path = ConfigurationSettings.AppSettings["SharedDirectory"];
            path += @"\Configuration\ConfigurationData\1";
            return path;
        }


        /// <summary>
        /// 获取数据库的配置文件名称：
        /// 如D:\WebSite\QMSData\shared\Configuration\ConfigurationData\1\DatabaseCollection.xml
        /// 如果文件不存在，返回null
        /// </summary>
        /// <returns>文件名称</returns>
        public static string getDatabaseDiskFileName()
        {
            string path = getDatabaseDiskPath();
            path += @"\DatabaseCollection.xml";
            //文件是否存在
            if (System.IO.File.Exists(path) == false)
                path = null;
            return path;
        }


        /// <summary>
        /// 从配置文件app.config获取获取共享目录配置项（OneTableSystemFilesLocation）的值
        /// </summary>
        /// <returns></returns>
        public static string getLocalFilePathFromWebConfig()
        {
            string path = "";
            if (ConfigurationSettings.AppSettings["OneTableSystemFilesLocation"] != null)
            {
                path = ConfigurationSettings.AppSettings["OneTableSystemFilesLocation"];
            }
            return path;
        }



        /// <summary>
        /// 获取新闻类数据存放的服务器目录：
        /// 如D:\WebSite\QMSData\AllFiles\1\OneTableSystemFilesLocation\NewsTemp\
        /// </summary>
        /// <returns></returns>
        public static string getNewsSaveDiskPath()
        {
            string path = getLocalFilePathFromWebConfig();
            if ((path == "") || (path == null))
                return null;
            if (System.IO.Directory.Exists(path) == false)
                System.IO.Directory.CreateDirectory(path);
            path += "\\1";
            if (System.IO.Directory.Exists(path) == false)
                System.IO.Directory.CreateDirectory(path);
            path += "\\OneTableSystemFilesLocation";
            if (System.IO.Directory.Exists(path) == false)
                System.IO.Directory.CreateDirectory(path);
            path += "\\NewsTemp";
            if (System.IO.Directory.Exists(path) == false)
                System.IO.Directory.CreateDirectory(path);
            path += "\\";
            return path;
        }



        /// <summary>
        /// 获取新闻类数据存放的服务器目录：
        /// 如D:\WebSite\QMSData\AllFiles\1\OneTableSystemFilesLocation\Temp\
        /// 同时自动创建目录
        /// </summary>
        /// <returns></returns>
        public static string getUpLoadFileDiskPath()
        {
            string path = getLocalFilePathFromWebConfig();
            if ((path == "") || (path == null))
                return null;
            if (System.IO.Directory.Exists(path) == false)
                System.IO.Directory.CreateDirectory(path);
            path += "\\1";
            if (System.IO.Directory.Exists(path) == false)
                System.IO.Directory.CreateDirectory(path);
            path += "\\OneTableSystemFilesLocation";
            if (System.IO.Directory.Exists(path) == false)
                System.IO.Directory.CreateDirectory(path);
            path += "\\Temp";
            if (System.IO.Directory.Exists(path) == false)
                System.IO.Directory.CreateDirectory(path);
            path += "\\";
            return path;
        }





        /// <summary>
        /// 从配置文件app.config获取获取共享目录配置项（SharedDirectory）的值
        /// </summary>
        /// <returns></returns>
        public static string getSharePathFromWebConfig()
        {
            string path = "";
            if (ConfigurationSettings.AppSettings["SharedDirectory"] != null)
                path = ConfigurationSettings.AppSettings["SharedDirectory"];
            return path;
        }



        /// <summary>
        /// 获取应用系统的配置文件所在的目录：
        /// 如D:\WebSite\QMSData\shared\Configuration\ConfigurationXml\1
        /// </summary>
        /// <returns></returns>
        public static string getSystemDiskPath()
        {
            string path = getSharePathFromWebConfig();
            path += @"\Configuration\ConfigurationXml\1";
            return path;
        }


        /// <summary>
        /// 获取应用系统的配置文件所在的目录：
        /// 如D:\WebSite\QMSData\shared\Configuration\ConfigurationXml\1
        /// </summary>
        /// <returns></returns>
        public static string getSystemDiskPathFromSystemName(string systemName)
        {
            string path = getSharePathFromWebConfig();
            path += @"\Configuration\ConfigurationXml\1\" + systemName;
            return path;
        }


        /// <summary>
        /// 获取应用系统的配置文件名称：
        /// 如D:\WebSite\QMSData\shared\Configuration\ConfigurationXml\1\DatabaseConfig.xml
        /// 如果文件不存在，返回null
        /// </summary>
        /// <returns></returns>
        public static string getSystemDiskFileName()
        {
            string path = getSystemDiskPath();
            path += @"\DatabaseConfig.xml";
            //文件是否存在
            if (System.IO.File.Exists(path) == false)
                path = null;
            return path;
        }


        /// <summary>
        /// 获取应用系统的配置文件名称：
        /// 如D:\WebSite\QMSData\shared\Configuration\ConfigurationXml\1\[应用系统名称]\TablesConfig.xml
        /// 如果文件不存在，返回null
        /// </summary>
        /// <returns></returns>
        public static string getSystemConfigDiskFileName(string systemName)
        {
            string path = getSystemDiskPathFromSystemName(systemName);
            path += @"\TablesConfig.xml";
            //文件是否存在
            if (System.IO.File.Exists(path) == false)
                path = null;
            return path;
        }



        /// <summary>
        /// 获取数据字段的配置文件名称：
        /// 如D:\WebSite\QMSData\shared\Configuration\ConfigurationXml\1\Dictionaries.xml
        /// </summary>
        /// <returns></returns>
        public static string getDictionaryDiskFileName()
        {
            string path = getSystemDiskPath();
            path += @"\Dictionaries.xml";
            //文件是否存在
            //if (System.IO.File.Exists(path) == false)path = null;
            return path;
        }


        /// <summary>
        /// 取统计配置文件名称(本文件为系统通用统计设计)
        /// 如D:\WebSite\QMSData\shared\Configuration\ConfigurationXml\1\应用程序名称\style.xml
        /// </summary>
        /// <returns></returns>
        public static string getCssStyleDiskFileName(string systemName)
        {
            string path = TableConfig.getSystemDiskPath();
            path += "\\" + systemName;
            path += @"\style.xml";
            return path;
        }


        /// <summary>
        /// 取统计配置文件名称(本文件为系统通用统计设计)
        /// 如D:\WebSite\QMSData\shared\Configuration\ConfigurationXml\1\DateTimeFuncType.xml
        /// </summary>
        /// <returns></returns>
        public static string getDateTimeFuncTypeDiskFileName()
        {
            string path = getSystemDiskPath();
            path += @"\DateTimeFuncType.xml";
            return path;
        }


        /// <summary>
        /// 取统计配置文件名称(本文件为该应用系统的单独统计设计)
        /// 如D:\WebSite\QMSData\shared\Configuration\ConfigurationXml\1\应用系统名称\TablesConfig.xml
        /// </summary>
        /// <param name="systemName">应用系统名称</param>
        /// <returns></returns>
        public static string getAppSystemConfigDiskFileName(string systemName)
        {
            string path = TableConfig.getSystemDiskPath();
            path += "\\" + systemName;
            path += @"\TablesConfig.xml";
            if (System.IO.File.Exists(path) == false)
                return null;
            return path;
        }


        /// <summary>
        /// 根据系统名称，从应用系统的配置文件获取对应的数据库名称(只返回第一个)
        /// </summary>
        /// <returns></returns>
        public static string getDatabaseNameFromSystemName(string systemName)
        {
            string dbName = "";
            try
            {
                //取应用系统的配置文件
                string systemFileName = getSystemDiskFileName();
                XmlDocument doc = new XmlDocument();
                doc.Load(systemFileName);
                string xPath = "//dbSetting[@ModuleName='" + systemName + "']";
                XmlNodeList nodelist = doc.SelectNodes(xPath);
                if (nodelist.Count >= 1)
                {
                    dbName = nodelist[0].Attributes.GetNamedItem("DatabaseName").InnerXml;
                }
            }
            catch
            {
                throw;
            }
            return dbName;
        }




        /// <summary>
        /// 根据数据库名称，从应用系统的配置文件获取对应的系统名称(只返回第一个)
        /// </summary>
        /// <returns></returns>
        public static string getSystemNameFromDatabaseName(string dbName)
        {
            string systemName = "";
            try
            {
                //取应用系统的配置文件
                string systemFileName = getSystemDiskFileName();
                XmlDocument doc = new XmlDocument();
                doc.Load(systemFileName);
                string xPath = "//dbSetting[@DatabaseName='" + dbName + "']";
                XmlNodeList nodelist = doc.SelectNodes(xPath);
                if (nodelist.Count >= 1)
                {
                    systemName = nodelist[0].Attributes.GetNamedItem("ModuleName").InnerXml;
                }
            }
            catch
            {
                throw;
            }
            return systemName;
        }



        /// <summary>
        /// 获取数据表结构的配置文件所在的目录
        /// 如D:\WebSite\QMSData\shared\Configuration\ConfigurationData\1\数据库名称
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public static string getTableDiskPath(string dbName)
        {
            string path = getDatabaseDiskPath();
            path += @"\" + dbName;
            return path;
        }


        /// <summary>
        /// 从数据库配制文件中，获取表结构的配置文件名称
        /// 如D:\WebSite\QMSData\shared\Configuration\ConfigurationData\1\数据库名称\TablesConfig.xml
        /// </summary>
        /// <returns>如果文件不存在，返回null</returns>
        public static string getTableConfigDiskFileName(string dbName)
        {
            string path = getTableDiskPath(dbName);
            path += @"\TablesConfig.xml";
            //文件是否存在
            if (System.IO.File.Exists(path) == false)
                path = null;
            return path;
        }



        /// <summary>
        /// 获取表数据的文件名称
        /// 如D:\WebSite\QMSData\shared\Configuration\ConfigurationData\1\数据库名称\TablesData.xml
        /// </summary>
        /// <returns>如果文件不存在，返回null</returns>
        public static string getTableDataDiskFileName(string dbName)
        {
            string path = getTableDiskPath(dbName);
            path += @"\TablesData.xml";
            //文件是否存在
            if (System.IO.File.Exists(path) == false)
                path = null;
            return path;
        }


        /// <summary>
        /// 根据数据库名称，从数据库配置文件中获取对应数据库的参数设置
        /// </summary>
        /// <returns></returns>
        public static DataBaseLoginInfo getDatabaseInfoFromDatabaseName(string dbName)
        {
            DataBaseLoginInfo tagDataBaseLoginInfo = DataTag.initDataBaseLoginInfo();
            try
            {
                string databaseFileName = getDatabaseDiskFileName();
                XmlDocument doc = new XmlDocument();
                doc.Load(databaseFileName);
                string xPath = "//dbSetting[@DatabaseName='" + dbName + "']";
                XmlNodeList nodelist = doc.SelectNodes(xPath);
                if (nodelist.Count == 1)
                {
                    //数据库的连接参数
                    System.Collections.Hashtable hashTable = GetXMLData.getXmlNodeNameAndValue(nodelist[0]);
                    tagDataBaseLoginInfo.IP = hashTable["RemoteServer"].ToString();
                    tagDataBaseLoginInfo.DatabaseName = hashTable["InitCatalog"].ToString();
                    tagDataBaseLoginInfo.UserName = hashTable["UserID"].ToString();
                    tagDataBaseLoginInfo.PassWord = hashTable["Password"].ToString();
                    tagDataBaseLoginInfo.TCP = hashTable["RemoteServer"].ToString();
                    tagDataBaseLoginInfo.DefaultTableSpace = hashTable["InitCatalog"].ToString();
                    //数据库类型
                    string dbType = hashTable["DatabaseType"].ToString();
                    tagDataBaseLoginInfo.DatabaseType = ConvertData.convertDatabaseType(dbType);
                    //数据库连接串
                    tagDataBaseLoginInfo.ConnectionString = TableConfig.getDataBaseConnectionString(tagDataBaseLoginInfo);
                }
            }
            catch
            {
                throw;
            }
            return tagDataBaseLoginInfo;
        }


        /// <summary>
        /// 根据应用系统的名称，取数据库的配置信息：
        /// 1。根据系统名称取数据库名称
        /// 2。根据数据库名称，从数据库配置文件中获取对应数据库的参数设置
        /// </summary>
        /// <returns></returns>
        public static DataBaseLoginInfo getDatabaseInfoFromSystemName(string systemName)
        {
            string dbName = TableConfig.getDatabaseNameFromSystemName(systemName);
            return TableConfig.getDatabaseInfoFromDatabaseName(dbName);
        }


        /// <summary>
        /// 取一个表的所有字段的数据字典
        /// </summary>
        /// <param name="xmlConfigFileName">应用系统或数据库的配置文件TableConfig.XML</param>
        /// <param name="tableName">表名称</param>
        /// <returns>只返回有数据字典的字段</returns>
        public static Hashtable getTableFieldDictionary(string xmlConfigFileName, string tableName)
        {
            //字段名称,字段描述
            Hashtable hashTableFieldNameDesc = TableConfig.getFieldTwoAttrFromTableConfigFile(xmlConfigFileName, tableName, "ColumnEName", "ColumnCName");
            Hashtable hashTableFieldNameDictionary = new Hashtable();
            System.Collections.IDictionaryEnumerator myEnumerator = hashTableFieldNameDesc.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                string fieldName = myEnumerator.Key.ToString();
                Queue queueDict = TableConfig.getFieldDictionary(xmlConfigFileName, tableName, fieldName);
                if(queueDict.Count > 0)//有定义的数据字典
                    hashTableFieldNameDictionary.Add(fieldName, queueDict);
            }
            return hashTableFieldNameDictionary;
        }






        /// <summary>
        /// 从配置文件tableConfig.xml里取字段的数据字典
        /// </summary>
        /// <param name="tableConfigFileName"></param>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static System.Collections.Queue getFieldDictionary(string tableConfigFileName, string tableName, string fieldName)
        {
            System.Collections.Queue queueDictionary = new Queue();
            if (tableConfigFileName == null)
                return queueDictionary;
            XmlDocument doc = new XmlDocument();
            doc.Load(tableConfigFileName);
            string xPath = "//Table[@EName='" + tableName + "']/tr/td[@ColumnEName='" + fieldName + "']/Dictionary";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            if (nodelist.Count == 1)
            {
                string dataSource = nodelist[0].Attributes.GetNamedItem("DataSource").InnerXml;
                string dataSourceName = nodelist[0].Attributes.GetNamedItem("DataSourceName").InnerXml.Trim();
                switch (dataSource)
                {
                    case "Local"://本地字典表
                        queueDictionary = TableConfig.getDictionaryQueueFromName(dataSourceName);
                        break;
                    case "Fixed"://固定内容，个项目之间用“:”分隔
                        string[] dict = dataSourceName.Split(new char[] { ':' });
                        for (int i = 0; i < dict.Length; i++)
                        {
                            queueDictionary.Enqueue(dict[i]);
                        }
                        break;
                }
            }
            return queueDictionary;
        }


        /// <summary>
        /// 取表定义的字段列表
        /// </summary>
        /// <param name="tableConfigInfo"></param>
        /// <param name="tableConfigFileName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static TableConfigInfo getTableConfigInfoFieldList(TableConfigInfo tableConfigInfo, string tableConfigFileName, string tableName, bool isGetDictionary)
        {
            //在配置文件中定义到该表定义的字段
            string xPath = "//Table[@EName='" + tableName + "']/tr/td";
            XmlDocument doc = new XmlDocument();
            doc.Load(tableConfigFileName);
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            //取字段列表
            foreach (XmlNode node in nodelist)
            {
                string fieldName = node.Attributes.GetNamedItem("ColumnEName").InnerXml;//字段名称
                string fieldDesc = node.Attributes.GetNamedItem("ColumnCName").InnerXml;//字段描述
                string fieldTypeXML = node.Attributes.GetNamedItem("ColumnType").InnerXml;//字段类型
                //将类型转换成我们自己定义的FieldType类型
                FieldType fieldType = ConvertData.convertXmlFieldType(fieldTypeXML);
                //是保存到当前表
                //字段描述
                if (tableConfigInfo.FieldNameDesc[fieldName] == null)
                    tableConfigInfo.FieldNameDesc.Add(fieldName, fieldDesc);
                //字段类型
                if (tableConfigInfo.FieldNameType[fieldName] == null)
                    tableConfigInfo.FieldNameType.Add(fieldName, fieldType);
                //字段内容
                if (tableConfigInfo.FieldNameValue[fieldName] == null)
                    tableConfigInfo.FieldNameValue.Add(fieldName, null);
                //取字段的字典
                if (isGetDictionary)
                {
                    if (tableConfigInfo.FieldNameDictionary[fieldName] == null)
                    {
                        System.Collections.Queue queueDictionary = TableConfig.getFieldDictionary(tableConfigFileName, tableName, fieldName);
                        tableConfigInfo.FieldNameDictionary.Add(fieldName, queueDictionary);
                    }
                }
            }
            return tableConfigInfo;
        }




        /// <summary>
        /// 取表定义的字段列表
        /// </summary>
        /// <param name="tableConfigInfo"></param>
        /// <param name="tableConfigFileName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static System.Collections.Hashtable getSystemTableConfigInputFieldList(string systemName, string tableName)
        {
            Hashtable hashInputField = new Hashtable();
            //取应用系统对应的表配置文件名
            string systemConfigFileName = TableConfig.getSystemConfigDiskFileName(systemName);
            if (systemConfigFileName == null)
            {
                return hashInputField;
            }
            //在配置文件中定义到该表定义的字段
            string xPath = "//Table[@EName='" + tableName + "']/tr/td";
            XmlDocument doc = new XmlDocument();
            doc.Load(systemConfigFileName);
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            //取字段列表
            foreach (XmlNode node in nodelist)
            {
                XmlNodeList nodelistInput = node.SelectNodes("./Input");
                if (nodelistInput.Count != 1)
                    continue;
                string isInput = nodelistInput[0].Attributes.GetNamedItem("DisplayOnInput").InnerXml.ToLower();
                if ((isInput == "yes") || (isInput == "true"))
                {
                    string fieldName = node.Attributes.GetNamedItem("ColumnEName").InnerXml;//字段名称
                    string fieldDesc = node.Attributes.GetNamedItem("ColumnCName").InnerXml;//字段描述
                    if (hashInputField[fieldName] == null)
                        hashInputField.Add(fieldName, fieldDesc);
                }
            }
            return hashInputField;
        }





        /// <summary>
        /// 从配置文件中获取一个表结点的所有外键和类型（使用递归方法）
        /// 类型要根据定义的默认值进行转换：只转换定义为“自动编号()”的
        /// </summary>
        /// <param name="tableNode">配置文件表结点</param>
        /// <param name="foreignKeyTypeTable">保存外键的哈希表</param>
        /// <returns></returns>
        public static Hashtable getForeignKeyType(XmlNode tableNode, Hashtable foreignKeyTypeTable)
        {
            //定位到主表
            XmlNode parentTableNode = tableNode.ParentNode.ParentNode;
            if (parentTableNode != null)
            {
                if (parentTableNode.Name == "Table")
                {
                    //定位到主表的主关键字
                    string xPath = "./tr/td[@KeyIndex='Yes']";
                    XmlNodeList nodelist = parentTableNode.SelectNodes(xPath);
                    if (nodelist.Count == 1)
                    {
                        string fieldName = nodelist[0].Attributes.GetNamedItem("ColumnEName").Value;
                        string fieldTypeXML = nodelist[0].Attributes.GetNamedItem("ColumnType").Value;
                        FieldType fieldType = ConvertData.convertXmlFieldType(fieldTypeXML);
                        //主键的默认值
                        string defaultValue = nodelist[0].Attributes.GetNamedItem("ColumnDefault").Value;
                        if (defaultValue == "自动编号()")
                        {
                            if (fieldType != FieldType.GUID)
                                fieldType = FieldType.IDENTITY;
                        }

                        if (foreignKeyTypeTable[fieldName] == null)//外键一般不重复
                            foreignKeyTypeTable.Add(fieldName, fieldType);
                        else //如果重复，则该外键的名称，其默认规则如下：主表的名称_主表的主键名称
                        {
                            string parentTableName = TableConfig.getTableNameFromTableNode(parentTableNode);
                            fieldName = parentTableName + "_" + fieldName;
                            foreignKeyTypeTable.Add(fieldName, fieldType);
                        }
                    }
                    //递归
                    foreignKeyTypeTable = getForeignKeyType(parentTableNode, foreignKeyTypeTable);
                }
            }
            return foreignKeyTypeTable;
        }



        /// <summary>
        /// 从数据文件中获取一个表结点的所有外键的值（使用递归方法）
        /// </summary>
        /// <param name="tableNode">配置文件表结点</param>
        /// <param name="foreignKeyTypeTable">保存外键的哈希表</param>
        /// <returns></returns>
        public static Hashtable getForeignKeyValueFromParentTableNode(XmlNode tableNode, Hashtable foreignKeyTable, Hashtable foreignKeyRelationTable)
        {
            //定位到主表
            XmlNode parentTableNode = TableConfig.getParentTableNode(tableNode);
            if (parentTableNode == null)
                return foreignKeyTable;
            //主记录的关联内容
            XmlNode childTableNode = parentTableNode.FirstChild; ;
            if (childTableNode.Name == "tr")
            {
                //遍历所有的外键
                System.Collections.IDictionaryEnumerator myEnumerator = foreignKeyTable.GetEnumerator();
                while (myEnumerator.MoveNext())
                {
                    string fieldName = foreignKeyRelationTable[myEnumerator.Key].ToString();
                    //定位
                    string xPath = "./td[@Column='" + fieldName + "']";
                    XmlNodeList nodelist = childTableNode.SelectNodes(xPath);
                    if (nodelist.Count == 1)
                    {
                        //字段的内容
                        foreignKeyTable[myEnumerator.Key] = nodelist[0].InnerXml;
                        break;
                    }
                }
            }
            //定位到主表
            parentTableNode = TableConfig.getParentTableNode(parentTableNode);
            if (parentTableNode != null)
            {
                //递归
                foreignKeyTable = getForeignKeyValue(parentTableNode, foreignKeyTable);
            }
            return foreignKeyTable;
        }



        /// <summary>
        /// 从数据文件中获取一个表结点的所有外键的值（使用递归方法）
        /// </summary>
        /// <param name="tableNode">配置文件表结点</param>
        /// <param name="foreignKeyTypeTable">保存外键的哈希表</param>
        /// <returns></returns>
        public static Hashtable getForeignKeyValue(XmlNode tableNode, Hashtable foreignKeyTable)
        {
            //主记录的关联内容从子表里取
            XmlNode childTableNode = tableNode.FirstChild; ;
            if (childTableNode.Name == "tr")
            {
                //遍历所有的外键
                System.Collections.IDictionaryEnumerator myEnumerator = foreignKeyTable.GetEnumerator();
                while (myEnumerator.MoveNext())
                {
                    string fieldName = myEnumerator.Key.ToString();
                    //定位
                    string xPath = "./td[@Column='" + fieldName + "']";
                    XmlNodeList nodelist = childTableNode.SelectNodes(xPath);
                    if (nodelist.Count == 1)
                    {
                        //字段的内容
                        foreignKeyTable[myEnumerator.Key] = nodelist[0].InnerXml;
                        break;
                    }
                }
            }
            //定位到主表
            XmlNode parentTableNode = TableConfig.getParentTableNode(tableNode);
            if (parentTableNode != null)
            {
                //递归
                foreignKeyTable = getForeignKeyValue(parentTableNode, foreignKeyTable);
            }
            return foreignKeyTable;
        }


        /// <summary>
        /// 从表关联的配置文件中取表：主表名称、主键字段、外键
        /// 从表关联的配置文件中取一个表所有的外来关键字及其类型：将外来关键字的值初始化为null
        /// </summary>
        /// <param name="tableConfigInfo"></param>
        /// <param name="dbName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static TableConfigInfo getPrimaryKeyAndForeignKey(TableConfigInfo tableConfigInfo, string dbName, string tableName)
        {
            //取表结构配置文件名称
            string tableRelationFileName = TableRelation.getTableRelationDiskFileName(dbName);
            if (tableRelationFileName == null)//文件不存在
                return tableConfigInfo;
            //先定位到当前表定义的主键构成字段
            string xPath = "//Table[@EName='" + tableName + "']/tr/td";
            XmlDocument doc = new XmlDocument();
            doc.Load(tableRelationFileName);
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            foreach (XmlNode node in nodelist)
            {
                //取该表的所有外键
                string fieldName = node.Attributes.GetNamedItem("ColumnEName").InnerXml;
                string fieldTypeXML = node.Attributes.GetNamedItem("ColumnType").InnerXml;
                string fieldRelation = node.Attributes.GetNamedItem("RelationEName").InnerXml;
                FieldType fieldType = ConvertData.convertXmlFieldType(fieldTypeXML);
                if (fieldRelation == "")
                {
                    //是主键字段
                    if (tableConfigInfo.PrimaryKey[fieldName] == null)
                    {
                        tableConfigInfo.PrimaryKey.Add(fieldName, null);
                        tableConfigInfo.PrimaryKeyType.Add(fieldName, fieldType);
                    }
                }
                else
                {
                    //是外键
                    if (tableConfigInfo.ForeignKey[fieldName] == null)
                    {
                        tableConfigInfo.ForeignKey.Add(fieldName, null);
                        tableConfigInfo.ForeignKeyType.Add(fieldName, fieldType);
                        tableConfigInfo.ForeignKeyRelation.Add(fieldName, fieldRelation);
                        tableConfigInfo.ForeignKeyStack.Push(fieldName);
                    }
                }
            }
            return tableConfigInfo;
        }


        /// <summary>
        /// 根据数据库名称，表的名称：获取该表的配置信息
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static TableConfigInfo getTableConfigInfoFromDatabaseName(string dbName, string tableName)
        {
            //初始化数据
            TableConfigInfo tableConfigInfo = DataTag.initTableConfigInfo();
            try
            {
                //取数据库的配置信息
                tableConfigInfo.dataBaseLoginInfo = TableConfig.getDatabaseInfoFromDatabaseName(dbName);
                //取表结构配置文件名称
                string tableConfigFileName = getTableConfigDiskFileName(dbName);
                if (tableConfigFileName == null)//文件不存在
                    return tableConfigInfo;
                //取表的名称
                tableConfigInfo.TableName = tableName;
                //取表定义的字段：字段描述、字段类型、字段内容（初始化为null）、字段的字典等
                tableConfigInfo = TableConfig.getTableConfigInfoFieldList(tableConfigInfo, tableConfigFileName, tableConfigInfo.TableName, false);
                //取所有的主键字段和外键的定义情况
                tableConfigInfo = TableConfig.getPrimaryKeyAndForeignKey(tableConfigInfo, dbName, tableName);
                //主表名称
                tableConfigInfo.PrimaryTable = TableConfig.getParentTableName(tableConfigFileName, tableName);
            }
            catch
            {
                throw;
            }
            return tableConfigInfo;
        }


        /// <summary>
        /// 根据数据库名称，表的名称：获取该表的配置信息
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static TableConfigInfo getTableConfigInfoAndDictionaryFromDatabaseName(string dbName, string tableName)
        {
            //初始化数据
            TableConfigInfo tableConfigInfo = DataTag.initTableConfigInfo();
            try
            {
                //取数据库的配置信息
                tableConfigInfo.dataBaseLoginInfo = TableConfig.getDatabaseInfoFromDatabaseName(dbName);
                //取表结构配置文件名称
                string tableConfigFileName = getTableConfigDiskFileName(dbName);
                if (tableConfigFileName == null)//文件不存在
                    return tableConfigInfo;
                //取表的名称
                tableConfigInfo.TableName = tableName;
                //取表定义的字段：字段描述、字段类型、字段内容（初始化为null）、字段的字典等
                tableConfigInfo = TableConfig.getTableConfigInfoFieldList(tableConfigInfo, tableConfigFileName, tableConfigInfo.TableName, true);
                //取所有的主键字段和外键的定义情况
                tableConfigInfo = TableConfig.getPrimaryKeyAndForeignKey(tableConfigInfo, dbName, tableName);
                //主表名称
                tableConfigInfo.PrimaryTable = TableConfig.getParentTableName(tableConfigFileName, tableName);
            }
            catch
            {
                throw;
            }
            return tableConfigInfo;
        }




        /// <summary>
        /// 根据数据库名称，表的名称：获取该表的配置信息
        /// </summary>
        /// <param name="systemName">系统名称</param>
        /// <param name="tableName">表名称</param>
        /// <returns></returns>
        public static TableConfigInfo getTableConfigInfoFromSystemName(string systemName, string tableName)
        {
            string dbName = TableConfig.getDatabaseNameFromSystemName(systemName);
            return TableConfig.getTableConfigInfoFromDatabaseName(dbName, tableName);
        }




        /// <summary>
        /// 将TR结点（一条记录）的内容保存到哈希表
        /// </summary>
        /// <param name="trNode"></param>
        /// <returns></returns>
        public static System.Collections.Hashtable getRecordDataFromXmlNodeTR(XmlNode trNode)
        {
            System.Collections.Hashtable hashTable = new Hashtable();
            string xPath = "./td";
            XmlNodeList nodelistTD = trNode.SelectNodes(xPath);
            foreach (XmlNode nodeTD in nodelistTD)
            {
                string fieldName = nodeTD.Attributes.GetNamedItem("Column").InnerXml;
                string fieldValue = nodeTD.InnerXml;
                //将数据保存到TableConfigInfo中
                if (hashTable[fieldName] == null)
                {
                    //该字段存在
                    hashTable.Add(fieldName, fieldValue);
                }
            }
            return hashTable;
        }


        /// <summary>
        /// 取当前表结点的表的名称
        /// </summary>
        /// <param name="tableNodeSource"></param>
        /// <returns></returns>
        public static string getTableNameFromTableNode(XmlNode tableNodeSource)
        {
            string tableName = "";
            if (tableNodeSource.Name == "Table")
            {
                if (tableNodeSource.Attributes.GetNamedItem("EName") != null)
                    tableName = tableNodeSource.Attributes.GetNamedItem("EName").InnerXml;
                else if (tableNodeSource.Attributes.GetNamedItem("TableEName") != null)
                    tableName = tableNodeSource.Attributes.GetNamedItem("TableEName").InnerXml;
            }
            return tableName;
        }


        /// <summary>
        /// 取当前查询的表结点的表名称
        /// </summary>
        /// <param name="queryNode"></param>
        /// <returns></returns>
        public static string getTableNameFromQueryNode(XmlNode queryNode)
        {
            string tableName = "";
            if (queryNode.Name == "Table")
                tableName = queryNode.Attributes.GetNamedItem("TableEName").InnerXml;
            return tableName;
        }


        /// <summary>
        /// 取数据结点的叶子表结点：只取第一个结点下的子表
        /// </summary>
        /// <param name="tableNodeSource"></param>
        /// <returns></returns>
        public static XmlNode getLeafQueryNode(XmlNode queryNode)
        {
            XmlNode leafQueryNode = queryNode;
            string xPath = "./Table";
            XmlNodeList nodelist = queryNode.SelectNodes(xPath);
            if (nodelist.Count > 0)
            {
                //用递归方法取叶子结点
                leafQueryNode = getLeafQueryNode(nodelist[0]);
            }
            return leafQueryNode;
        }




        /// <summary>
        /// 取数据结点的叶子表结点：只取第一个结点下的子表
        /// </summary>
        /// <param name="tableNodeSource"></param>
        /// <returns></returns>
        public static XmlNode getLeafTableNode(XmlNode tableNode)
        {
            XmlNode leafTableNode = tableNode;
            string xPath = "./tr/Table";
            XmlNodeList nodelist = tableNode.SelectNodes(xPath);
            if (nodelist.Count > 0)
            {
                //用递归方法取叶子结点
                leafTableNode = getLeafTableNode(nodelist[0]);
            }
            return leafTableNode;
        }



        /// <summary>
        /// 取表结点的父表结点：如果不存在父表，则返回null
        /// </summary>
        /// <param name="tableNodeSource"></param>
        /// <returns></returns>
        public static XmlNode getParentTableNode(XmlNode tableNodeSource)
        {
            XmlNode parentTableNode = null;
            if (tableNodeSource.Name != "Table")
                return parentTableNode;
            //找到当前表，判断是否有主表，有则取主表信息返回
            if (tableNodeSource.ParentNode == null)
            {
                return parentTableNode;
            }
            if (tableNodeSource.ParentNode.ParentNode == null)
            {
                return parentTableNode;
            }
            XmlNode node = tableNodeSource.ParentNode.ParentNode;
            if (node.Name == "Table")
            {
                //取到主表结点
                parentTableNode = node;
            }
            return parentTableNode;
        }



        /// <summary>
        /// 根据应用系统的名称，和TABLE结点的表名称：获取对应表的配置信息
        /// 并取该表结点的父亲结点的主关键字，作为外键使用，并将父表的内容保存到哈希表中
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="tableNodeSource"></param>
        /// <returns></returns>
        public static TableConfigInfo getTableConfigInfoAndForeignKeyValue(string systemName, XmlNode tableNodeSource)
        {
            //将系统名称转换成数据库
            string dbName = TableConfig.getDatabaseNameFromSystemName(systemName);
            TableConfigInfo tableConfigInfo = TableConfig.getTableConfigInfoAndForeignKeyValueFromDatabaseName(dbName, tableNodeSource);
            return tableConfigInfo;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="tableNodeSource"></param>
        /// <param name="hashForeignKeyALL"></param>
        /// <param name="foreignKeyRelationALL"></param>
        /// <returns></returns>
        public static System.Collections.Hashtable getForeignKeyValueFromTableNode(string dbName, XmlNode tableNodeSource, Hashtable hashForeignKeyALL, Hashtable foreignKeyRelationALL)
        {
            XmlNode parentTableNode = TableConfig.getParentTableNode(tableNodeSource);
            if (parentTableNode == null)//没有父表
                return hashForeignKeyALL;
            string parentTableName = TableConfig.getTableNameFromTableNode(parentTableNode);
            if (tableNodeSource.Name != "Table")
                return hashForeignKeyALL;
            string tableName = TableConfig.getTableNameFromTableNode(tableNodeSource);
            //取表的主表主键列表
            //System.Collections.Hashtable hashParentTablePrimaryKey = TableRelation.getPrimaryKeyFromTableRelation(dbName, parentTableName);
            //取当前表的外键列表
            System.Collections.Hashtable hashForeignKeyParent = TableRelation.getForeignKeyHashTableFromDatabaseName(dbName, parentTableName);
            //遍历子表的外来关键字
            System.Collections.IDictionaryEnumerator myEnumerator = foreignKeyRelationALL.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                //				if(hashParentTablePrimaryKey[myEnumerator.Value] == null)
                //					continue;
                //主表字段
                string fieldNameParent = myEnumerator.Value.ToString();
                //子表字段
                string fieldNameChild = myEnumerator.Key.ToString();
                XmlNode parentNode = null;
                if (hashForeignKeyParent[fieldNameParent] != null)//说明字段名称来自祖父表
                {
                    //真实的字段名称
                    fieldNameParent = hashForeignKeyParent[fieldNameParent].ToString();
                    parentNode = parentTableNode.ParentNode;
                }
                else
                {
                    parentNode = tableNodeSource.ParentNode;
                }
                string xPath = "./td[@Column='" + fieldNameParent + "']";
                XmlNodeList nodelist = parentNode.SelectNodes(xPath);
                if (nodelist.Count == 1)
                {
                    string fieldValue = nodelist[0].InnerXml;
                    if (hashForeignKeyALL[fieldNameChild] == null)
                    {
                        hashForeignKeyALL[fieldNameChild] = fieldValue;//保存内容
                        //break;//一次只查一个外键（一个表只有一个）
                    }
                }
            }
            TableConfigInfo tableConfigInfoParent = TableConfig.getTableConfigInfoFromDatabaseName(dbName, parentTableName);
            if (tableConfigInfoParent.PrimaryTable != "")
            {
                //递归
                hashForeignKeyALL = TableConfig.getForeignKeyValueFromTableNode(dbName, parentTableNode, hashForeignKeyALL, foreignKeyRelationALL);
            }
            return hashForeignKeyALL;
        }



        /// <summary>
        /// 根据应用系统的名称，和TABLE结点的表名称：获取对应表的配置信息
        /// 并取该表结点的父亲结点的主关键字，作为外键使用，并将父表的内容保存到哈希表中
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="tableNodeSource"></param>
        /// <returns></returns>
        public static TableConfigInfo getTableConfigInfoAndForeignKeyValueFromDatabaseName(string dbName, XmlNode leafTableNode)
        {
            //取该数据库的配置信息
            DataBaseLoginInfo dataBaseLoginInfo = TableConfig.getDatabaseInfoFromDatabaseName(dbName);
            //取该XML叶子结点的表名称（该表就是操作的表）
            string tableName = TableConfig.getTableNameFromTableNode(leafTableNode);
            //取表的配置信息（含主表的配置信息）
            TableConfigInfo tableConfigInfo = TableConfig.getTableConfigInfoFromDatabaseName(dbName, tableName);
            //用递归取所有外键的值
            if (tableConfigInfo.ForeignKey.Count > 0)
                tableConfigInfo.ForeignKey = TableConfig.getForeignKeyValueFromTableNode(dbName, leafTableNode, tableConfigInfo.ForeignKey, tableConfigInfo.ForeignKeyRelation);
            return tableConfigInfo;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Hashtable getDateTimeFuncTypeNameValue()
        {
            Hashtable hashDTFuncTypeNameValue = new Hashtable();
            string dtFuncTypeFileName = TableConfig.getDateTimeFuncTypeDiskFileName();
            //配置文件是否存在
            if (System.IO.File.Exists(dtFuncTypeFileName))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(dtFuncTypeFileName);
                string xPath = "//Function";
                XmlNodeList nodelist = doc.SelectNodes(xPath);
                foreach (XmlNode node in nodelist)
                {
                    string funcName = node.Attributes.GetNamedItem("name").InnerXml;
                    string funcValue = node.Attributes.GetNamedItem("value").InnerXml;
                    if (hashDTFuncTypeNameValue[funcName] == null)
                        hashDTFuncTypeNameValue.Add(funcName, funcValue);
                }
            }
            //如果没有定义,默认为日
            if (hashDTFuncTypeNameValue.Count == 0)
                hashDTFuncTypeNameValue.Add("日", "Day");
            return hashDTFuncTypeNameValue;
        }



        /// <summary>
        /// 从XML中获取字典表的内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static System.Collections.SortedList getDictionaryListFromID(string id)
        {
            string xPath = "//Table[@ID='" + id + "']/tr/td";
            return TableConfig.getDictionaryListFromXPath(xPath);
        }
      

        /// <summary>
        /// 从XML中获取字典表的内容
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static System.Collections.SortedList getDictionaryListFromName(string name)
        {
            string xPath = "//Table[@Name='" + name + "']/tr/td";
            return TableConfig.getDictionaryListFromXPath(xPath);
        }


        /// <summary>
        /// 从XML中获取字典表的内容
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static System.Collections.Queue getDictionaryQueueFromName(string name)
        {
            string xPath = "//Table[@Name='" + name + "']/tr/td";
            return TableConfig.getDictionaryQueueFromXPath(xPath);
        }


        /// <summary>
        /// 取某个字典的内容,根据xPath取:xPath = "//Table[@Name='" + name + "']/tr/td";
        /// 或xPath = "//Table[@ID='" + id + "']/tr/td";
        /// </summary>
        /// <param name="xPath"></param>
        /// <returns></returns>
        public static System.Collections.SortedList getDictionaryListFromXPath(string xPath)
        {
            string tableStatFileName = TableConfig.getDictionaryDiskFileName();
            if (System.IO.File.Exists(tableStatFileName) == false)
                return null;
            SortedList statChartList = new SortedList();
            XmlDocument doc = new XmlDocument();
            doc.Load(tableStatFileName);
            //遍历所有的统计图
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            System.Collections.Hashtable chartSortHashtable = new Hashtable();
            foreach (XmlNode node in nodelist)
            {
                string charName = node.InnerXml;
                if (chartSortHashtable[charName] == null)
                {
                    chartSortHashtable.Add(charName, charName);
                    statChartList.Add(charName, charName);
                }
            }
            return statChartList;
        }


        /// <summary>
        /// 往字典表里加数据,如果字典配置文件里没有该字典,则要自动创建
        /// 如:
        /// D:\WebSite\QMSData\shared\Configuration\ConfigurationXml\1\Dictionaries.xml
        /// </summary>
        /// <param name="dictName">字典的名称,如:统计图分类,对应ID</param>
        /// <param name="dictDesc">字典的描述名称,如:统计图分类,对应Name</param>
        /// <param name="hashDictData">字典表里的数据项</param>
        public static void saveDictionary(string dictName, string dictDesc, Hashtable hashDictData)
        {
            string tableStatFileName = TableConfig.getDictionaryDiskFileName();
            XmlDocument doc = new XmlDocument();
            //判断字典配置文件是否存在
            if (System.IO.File.Exists(tableStatFileName) == false)
            {
                //文件不存在,则创建
                //加入XML的声明段落
                XmlDeclaration declarationDoc = doc.CreateXmlDeclaration("1.0", "utf-8", "");
                doc.AppendChild(declarationDoc);
                //创建一个根元素
                XmlElement elementRoot = doc.CreateElement("Tables");
                doc.AppendChild(elementRoot);
                doc.Save(tableStatFileName);
            }
            //判断该字典表的数据是否存在,如果不存在,则创建
            doc.Load(tableStatFileName);
            string xPath = "//Table[@ID='" + dictName + "']";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            if (nodelist.Count < 1)
            {
                //创建表结点
                XmlNode tableNode = doc.CreateNode(XmlNodeType.Element, "Table", "");
                //表结点的属性
                XmlAttribute attr = doc.CreateAttribute("ID");
                attr.InnerXml = dictName;
                tableNode.Attributes.Append(attr);
                attr = doc.CreateAttribute("Name");
                attr.InnerXml = dictDesc;
                tableNode.Attributes.Append(attr);
                //tr结点
                XmlNode trNode = doc.CreateNode(XmlNodeType.Element, "tr", "");
                tableNode.AppendChild(trNode);
                //挂接到根节点下
                xPath = "/Tables";
                nodelist = doc.SelectNodes(xPath);
                if (nodelist.Count > 0)
                {
                    //加到根结点下
                    nodelist[0].AppendChild(tableNode);
                }
                //保存
                doc.Save(tableStatFileName);
            }
            //重新定位到该表
            xPath = "//Table[@ID='" + dictName + "']";
            nodelist = doc.SelectNodes(xPath);
            if (nodelist.Count < 1)
                return;//没有该表,返回(一般不会出现这种情况)
            XmlNode nodeTableSave = nodelist[0];
            xPath = "./tr";
            nodelist = nodeTableSave.SelectNodes(xPath);
            if (nodelist.Count < 1)
            {
                //没有tr结点,则加上
                XmlNode trNode = doc.CreateNode(XmlNodeType.Element, "tr", "");
                nodeTableSave.AppendChild(trNode);
            }
            XmlNode nodeTrSave = nodelist[0];
            //保存原来的字典列表
            Hashtable hashDictDataOLD = new Hashtable();
            xPath = "./td";
            XmlNodeList nodelistTD = nodeTrSave.SelectNodes(xPath);
            foreach (XmlNode node in nodelistTD)
            {
                string dictValue = node.InnerXml;
                if (hashDictDataOLD[dictValue] == null)
                    hashDictDataOLD.Add(dictValue, dictValue);
            }
            //遍历传进来的字典,如果没有该项数据则加上
            System.Collections.IDictionaryEnumerator myEnumerator = hashDictData.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                string dictValue = myEnumerator.Key.ToString();
                if (dictValue == "")
                    continue;
                if (hashDictDataOLD[dictValue] == null)
                {
                    //td结点
                    XmlNode tdNode = doc.CreateNode(XmlNodeType.Element, "td", "");
                    tdNode.InnerXml = dictValue;
                    nodeTrSave.AppendChild(tdNode);
                }
            }
            //保存
            doc.Save(tableStatFileName);
        }


        /// <summary>
        /// 取某个字典的内容,根据xPath取:xPath = "//Table[@Name='" + name + "']/tr/td";
        /// 或xPath = "//Table[@ID='" + id + "']/tr/td";
        /// </summary>
        /// <param name="xPath"></param>
        /// <returns></returns>
        public static System.Collections.Queue getDictionaryQueueFromXPath(string xPath)
        {
            string tableStatFileName = TableConfig.getDictionaryDiskFileName();
            if (System.IO.File.Exists(tableStatFileName) == false)
                return null;
            Queue statChartList = new Queue();
            XmlDocument doc = new XmlDocument();
            doc.Load(tableStatFileName);
            //遍历所有的统计图
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            System.Collections.Hashtable chartSortHashtable = new Hashtable();
            foreach (XmlNode node in nodelist)
            {
                string charName = node.InnerXml;
                if (chartSortHashtable[charName] == null)
                {
                    chartSortHashtable.Add(charName, charName);
                    statChartList.Enqueue(charName);
                }
            }
            return statChartList;
        }


        /// <summary>
        /// 生成应用系统下的某个表的脚本
        /// </summary>
        /// <param name="systemConfigFileName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string getParentTableSelectFieldListSQLScript(string systemConfigFileName, string tableName)
        {
            string selectFieldsSQL = "";
            //取该应用的配置文件名称
            XmlDocument doc = new XmlDocument();
            doc.Load(systemConfigFileName);
            string xPath = "//Table[@EName='" + tableName + "']/tr/td/Query[@DisplayInChild='Yes']";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            foreach (XmlNode node in nodelist)
            {
                XmlNode fieldNode = node.ParentNode;
                string fieldName = fieldNode.Attributes.GetNamedItem("ColumnEName").InnerXml;
                string fieldDesc = fieldNode.Attributes.GetNamedItem("ColumnCName").InnerXml;
                if (selectFieldsSQL != "")
                    selectFieldsSQL += ",";
                string selectFieldsSQLModal = "[{0}].[{1}] AS [{2}]";
                selectFieldsSQL += string.Format(selectFieldsSQLModal, tableName, fieldName, fieldDesc);
            }
            return selectFieldsSQL;
        }


        /// <summary>
        /// 从数据库的配置文件,生成应用系统下的某个表的脚本
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string getTableSelectFieldsFromDatabaseDefine(string systemName, string tableName)
        {
            string selectFieldsSQLModal = "[{0}].[{1}] AS [{2}]";
            string selectFieldsSQL = "";
            //取该应用对应的的数据库配置文件名称
            string dbName = TableConfig.getDatabaseNameFromSystemName(systemName);
            string dbConfigFileName = TableConfig.getTableConfigDiskFileName(dbName);
            if (dbConfigFileName == null)
                return null;
            XmlDocument doc = new XmlDocument();
            doc.Load(dbConfigFileName);
            string xPath = "//Table[@EName='" + tableName + "']/tr/td";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            foreach (XmlNode node in nodelist)
            {
                string fieldName = node.Attributes.GetNamedItem("ColumnEName").InnerXml;
                string fieldDesc = node.Attributes.GetNamedItem("ColumnCName").InnerXml;
                if (selectFieldsSQL != "")
                    selectFieldsSQL += ",";
                selectFieldsSQL += string.Format(selectFieldsSQLModal, tableName, fieldName, fieldDesc);
            }
            return selectFieldsSQL;
        }



        /// <summary>
        /// 从应用系统的配置文件,取某个表的数据列表的字段描述和显示的长度
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getTableFieldDescLengthFromAppSystem(string systemName, string tableName)
        {
            Hashtable hashTable = new Hashtable();
            //取该应用的配置文件名称
            string systemConfigFileName = TableConfig.getAppSystemConfigDiskFileName(systemName);
            if (systemConfigFileName == null)
                return null;
            XmlDocument doc = new XmlDocument();
            doc.Load(systemConfigFileName);
            string xPath = "//Table[@EName='" + tableName + "']/tr/td/Query";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            foreach (XmlNode node in nodelist)
            {
                XmlNode fieldNode = node.ParentNode;
                //string fieldName = fieldNode.Attributes.GetNamedItem("ColumnEName").InnerXml;
                string fieldDesc = fieldNode.Attributes.GetNamedItem("ColumnCName").InnerXml;
                string fieldDispLength = node.Attributes.GetNamedItem("DisplayLength").InnerXml;
                if (fieldDispLength == "") fieldDispLength = "0";
                if (hashTable[fieldDesc] == null)
                {
                    hashTable.Add(fieldDesc, fieldDispLength);
                }
            }
            return hashTable;
        }



        /// <summary>
        /// 从应用系统的配置文件,取某个表的数据列表的字段描述和显示的长度
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getTableFieldNameLengthFromAppSystem(string systemName, string tableName)
        {
            Hashtable hashTable = new Hashtable();
            //取该应用的配置文件名称
            string systemConfigFileName = TableConfig.getAppSystemConfigDiskFileName(systemName);
            if (systemConfigFileName == null)
                return hashTable;
            XmlDocument doc = new XmlDocument();
            doc.Load(systemConfigFileName);
            string xPath = "//Table[@EName='" + tableName + "']/tr/td/Query";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            foreach (XmlNode node in nodelist)
            {
                XmlNode fieldNode = node.ParentNode;
                string fieldName = fieldNode.Attributes.GetNamedItem("ColumnEName").InnerXml;
                //string fieldDesc = fieldNode.Attributes.GetNamedItem("ColumnCName").InnerXml;
                string fieldDispLength = node.Attributes.GetNamedItem("DisplayLength").InnerXml;
                if (fieldDispLength == "") fieldDispLength = "0";
                if (hashTable[fieldName] == null)
                {
                    hashTable.Add(fieldName, fieldDispLength);
                }
            }
            return hashTable;
        }


        /// <summary>
        /// 从应用系统的配置文件,取某个表的数据列表的字段名称和处理功能
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getTableFieldNameGridViewFunctionFromAppSystem(string systemName, string tableName)
        {
            Hashtable hashTable = new Hashtable();
            //取该应用的配置文件名称
            string systemConfigFileName = TableConfig.getAppSystemConfigDiskFileName(systemName);
            if (systemConfigFileName == null)
                return hashTable;
            XmlDocument doc = new XmlDocument();
            doc.Load(systemConfigFileName);
            string xPath = "//Table[@EName='" + tableName + "']/tr/td/Query";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            foreach (XmlNode node in nodelist)
            {
                XmlNode fieldNode = node.ParentNode;
                string fieldName = fieldNode.Attributes.GetNamedItem("ColumnEName").InnerXml;
                string functionOnQuery = node.Attributes.GetNamedItem("FunctionOnQuery").InnerXml;
                if (functionOnQuery == "")
                    continue;
                if (hashTable[fieldName] == null)
                {
                    hashTable.Add(fieldName, functionOnQuery);
                }
            }
            return hashTable;
        }




        /// <summary>
        /// 从应用系统的配置文件,取某个表的详细信息的字段名称和处理功能
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getTableFieldNameInputFunctionFromAppSystem(string systemName, string tableName)
        {
            Hashtable hashTable = new Hashtable();
            //取该应用的配置文件名称
            string systemConfigFileName = TableConfig.getAppSystemConfigDiskFileName(systemName);
            if (systemConfigFileName == null)
                return hashTable;
            XmlDocument doc = new XmlDocument();
            doc.Load(systemConfigFileName);
            string xPath = "//Table[@EName='" + tableName + "']/tr/td/Input";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            foreach (XmlNode node in nodelist)
            {
                XmlNode fieldNode = node.ParentNode;
                string fieldName = fieldNode.Attributes.GetNamedItem("ColumnEName").InnerXml;
                string functionName = node.Attributes.GetNamedItem("FunctionOnInput").InnerXml;
                if (functionName == "")
                    continue;
                //转换为HTML
                functionName = ConvertData.convertESCToHtml(functionName);
                if (hashTable[fieldName] == null)
                {
                    hashTable.Add(fieldName, functionName);
                }
            }
            return hashTable;
        }



        /// <summary>
        /// 从应用系统的配置文件,取某个表的必须录入的字段，返回：字段名称、字段类型FieldType
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getNoAllowNullFieldNameTypeFromSystemName(string systemName, string tableName)
        {
            Hashtable hashTable = new Hashtable();
            //取该应用的配置文件名称
            string systemConfigFileName = TableConfig.getAppSystemConfigDiskFileName(systemName);
            if ((systemConfigFileName == null) || (systemConfigFileName == ""))
                return hashTable;
            XmlDocument doc = new XmlDocument();
            doc.Load(systemConfigFileName);
            string xPath = "//Table[@EName='" + tableName + "']/tr/td[@AllowNull='No']";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            foreach (XmlNode node in nodelist)
            {
                string fieldName = node.Attributes.GetNamedItem("ColumnEName").InnerXml;
                string fieldTypeXML = node.Attributes.GetNamedItem("ColumnType").InnerXml;
                FieldType fieldType = ConvertData.convertXmlFieldType(fieldTypeXML);
                if (hashTable[fieldName] == null)
                {
                    hashTable.Add(fieldName, fieldType);
                }
            }
            return hashTable;
        }



        /// <summary>
        /// 取某个应用系统的主表(只取第一个定义的表)的名称
        /// </summary>
        /// <param name="systemName"></param>
        /// <returns></returns>
        public static string getFirstTableNameFromSystemName(string systemName)
        {
            string tableName = "";
            //取该应用的配置文件名称
            string systemConfigFileName = TableConfig.getAppSystemConfigDiskFileName(systemName);
            if (systemConfigFileName == null)
                return null;
            XmlDocument doc = new XmlDocument();
            doc.Load(systemConfigFileName);
            string xPath = "//Table";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            if (nodelist.Count > 0)
            {
                tableName = nodelist[0].Attributes.GetNamedItem("EName").InnerXml;
            }
            return tableName;
        }



        /// <summary>
        /// 从数据结构的定义文件中取定义的字段的两个属性值返回，保存到哈希表
        /// </summary>
        /// <param name="xmlConfigFileName">配置文件名称：包括数据库的配置文件和应用系统的配置文件，如D:\\WebSite\\QMSData\\Shared\\Configuration\\ConfigurationXml\\1\\ZF单机定单\\TablesConfig.xml</param>
        /// <param name="tableName">要取的表结构定义的表名称</param>
        /// <param name="keyFieldName">做为Key的属性</param>
        /// <param name="valueFieldName">做为Value的属性</param>
        /// <returns></returns>
        public static System.Collections.Hashtable getFieldTwoAttrFromTableConfigFile(string xmlConfigFileName, string tableName, string keyFieldName, string valueFieldName)
        {
            Hashtable hashtable = new Hashtable();
            if ((xmlConfigFileName == "") || (xmlConfigFileName == null))
                return hashtable;
            //文件不存在
            if (System.IO.File.Exists(xmlConfigFileName) == false)
                return hashtable;
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlConfigFileName);
            string xPath = "//Table[@EName='" + tableName + "']/tr/td";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            foreach (XmlNode node in nodelist)
            {
                if (node.Attributes.GetNamedItem(keyFieldName) == null)
                    break;
                if (node.Attributes.GetNamedItem(valueFieldName) == null)
                    break;
                string hashKey = node.Attributes.GetNamedItem(keyFieldName).InnerXml;//名称
                string hashValue = node.Attributes.GetNamedItem(valueFieldName).InnerXml;//描述
                if (hashtable[hashKey] == null)
                    hashtable.Add(hashKey, hashValue);
            }
            return hashtable;
        }


        /// <summary>
        /// 按定义字段的显示次序进行字段排序
        /// </summary>
        /// <param name="hashTableFieldNameDispOrder"></param>
        /// <returns></returns>
        public static Queue getTableFieldDispOrderQueue(string systemName, string tableName)
        {
                //应用系统的配置文件：数据结构定义
                string xmlConfigFileName = TableConfig.getSystemConfigDiskFileName(systemName);
            if ((xmlConfigFileName == "") || (xmlConfigFileName == null))
                return null;
            //文件不存在
            if (System.IO.File.Exists(xmlConfigFileName) == false)
                return null;
            //取系统使用的字段
            Hashtable hashSystemSelectFieldName = TableConfig.getFieldParamInputFromSystemName(systemName, tableName);
            Queue queueFieldNameDispOrder = new Queue();
            Hashtable hashtable = new Hashtable();
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlConfigFileName);
            string xPath = "//Table[@EName='" + tableName + "']/tr/td";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            foreach (XmlNode node in nodelist)
            {
                string fieldName = node.Attributes.GetNamedItem("ColumnEName").InnerXml;
                //没有该字段
                if (hashSystemSelectFieldName[fieldName] == null)
                    continue;
                //配制的字段属性
                Hashtable hashIsSelectField = (Hashtable)hashSystemSelectFieldName[fieldName];
                if (hashIsSelectField["DisplayOnInput"] != null)
                {
                    //不显示
                    if (hashIsSelectField["DisplayOnInput"].ToString().ToLower() != "yes")
                        continue;
                }
                string fieldDispOrder = node.Attributes.GetNamedItem("DisplayOrder").InnerXml;
                if (hashtable[fieldName] == null)
                {
                    hashtable.Add(fieldName, fieldDispOrder);
                    queueFieldNameDispOrder.Enqueue(fieldName);
                }
            }
            return queueFieldNameDispOrder;
        }



        /// <summary>
        /// 取某个表显示到子表的字段
        /// </summary>
        /// <param name="systemConfigFileName"></param>
        /// <param name="systemName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string getParentTableDisplayOnChildFieldFromSystemName(string systemConfigFileName, string systemName, string tableName)
        {
            string parentSelectFieldSQL = "";
            string parentTableName = TableConfig.getParentTableName(systemConfigFileName, tableName);
            if ((parentTableName != "") && (parentTableName != null))
            {
                string selectFieldsSQLModal = "[{0}].[{1}] AS [{2}]";
                XmlDocument doc = new XmlDocument();
                doc.Load(systemConfigFileName);
                string xPath = "//Table[@EName='" + parentTableName + "']/tr/td/Query[@DisplayInChild='Yes']";
                XmlNodeList nodelist = doc.SelectNodes(xPath);
                foreach (XmlNode node in nodelist)
                {
                    XmlNode fieldNode = node.ParentNode;
                    string fieldName = fieldNode.Attributes.GetNamedItem("ColumnEName").InnerXml;
                    string fieldDesc = fieldNode.Attributes.GetNamedItem("ColumnCName").InnerXml;
                    if (parentSelectFieldSQL != "")
                        parentSelectFieldSQL += ",";
                    parentSelectFieldSQL += string.Format(selectFieldsSQLModal, parentTableName, fieldName, fieldDesc);
                }
                //递归:取其父表的选择的向下继承字段
                parentTableName = TableConfig.getParentTableName(systemConfigFileName, parentTableName);
                if ((parentTableName != "") && (parentTableName != null))
                {
                    parentSelectFieldSQL += TableConfig.getParentTableDisplayOnChildFieldFromSystemName(systemConfigFileName, systemName, parentTableName);
                }
            }
            return parentSelectFieldSQL;
        }



        /// <summary>
        /// 从应用系统的配置文件,取某个表显示到数据列表中的字段
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string getTableSelectFieldsFromAppSystem11(string systemName, string tableName)
        {
            string selectFieldsSQLModal = "[{0}].[{1}] AS [{2}]";
            string selectFieldsSQL = "";
            //取该应用的配置文件名称
            string systemConfigFileName = TableConfig.getAppSystemConfigDiskFileName(systemName);
            if (systemConfigFileName == null)
                return null;
            XmlDocument doc = new XmlDocument();
            doc.Load(systemConfigFileName);
            //string xPath = "//Table[@EName='" + tableName + "']/tr/td/Query[@DisplayInChild='Yes']";'
            //取主关键字
            string id = TableConfig.getPrimaryKeyName(systemConfigFileName, tableName);
            if ((id != null) && (id != ""))
                selectFieldsSQL += string.Format(selectFieldsSQLModal, tableName, id, id);
            string xPath = "//Table[@EName='" + tableName + "']/tr/td/Query";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            if (nodelist.Count == 0)
            {
                //应用系统没有配置，从数据库配置文件中取
                string dbName = TableConfig.getDatabaseNameFromSystemName(systemName);
                systemConfigFileName = TableConfig.getTableConfigDiskFileName(dbName);
                if (systemConfigFileName == null)
                    return null;
                doc.Load(systemConfigFileName);
                xPath = "//Table[@EName='" + tableName + "']/tr/td/Query";
                nodelist = doc.SelectNodes(xPath);
            }
            foreach (XmlNode node in nodelist)
            {
                XmlNode fieldNode = node.ParentNode;
                string fieldName = fieldNode.Attributes.GetNamedItem("ColumnEName").InnerXml;
                string fieldDesc = fieldNode.Attributes.GetNamedItem("ColumnCName").InnerXml;
                if (selectFieldsSQL != "")
                    selectFieldsSQL += ",";
                selectFieldsSQL += string.Format(selectFieldsSQLModal, tableName, fieldName, fieldDesc);
            }
            //父表的选择的字段(向下继承的字段)
            //			string parentTableName = TableConfig.getParentTableName(systemConfigFileName, tableName);
            //			if((parentTableName != "") && (parentTableName != null))
            //			{
            //				string selectFieldsSQLParent = getParentTableSelectFieldListSQLScript(systemConfigFileName, parentTableName);
            //				if((selectFieldsSQLParent != "") && (selectFieldsSQLParent != null))
            //				{
            //					if(selectFieldsSQL == "")
            //						selectFieldsSQL = selectFieldsSQLParent;
            //					else
            //						selectFieldsSQL = selectFieldsSQLParent + "," + selectFieldsSQL;
            //				}
            //			}
            return selectFieldsSQL;
        }



        /// <summary>
        /// 从应用系统的配置文件,取某个表显示到数据列表中的字段
        /// 包括从主表中带下来的字段
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string getTableSelectFieldFromSystemName(string systemName, string tableName)
        {
            string selectFieldsSQLModal = "[{0}].[{1}] AS [{2}]";
            string selectFieldsSQL = "";
            //取该应用的配置文件名称
            string systemConfigFileName = TableConfig.getAppSystemConfigDiskFileName(systemName);
            if (systemConfigFileName == null)
                return "";
            XmlDocument doc = new XmlDocument();
            doc.Load(systemConfigFileName);
            //string xPath = "//Table[@EName='" + tableName + "']/tr/td/Query[@DisplayInChild='Yes']";'
            //取主关键字
            Hashtable hashPK = TableRelation.getPrimaryKeyHashTableFromSystemName(systemName, tableName);
            Stack stackPK = TableRelation.getPrimaryKeyStackFromSystemName(systemName, tableName);
            System.Collections.IEnumerator enumerator = stackPK.GetEnumerator();
            while (enumerator.MoveNext())
            {
                string pkName = enumerator.Current.ToString();
                if (selectFieldsSQL != "")
                    selectFieldsSQL += ",";
                selectFieldsSQL += string.Format(selectFieldsSQLModal, tableName, pkName, pkName);
            }
            //取其父表的选择的向下继承字段
            //string parentSelectFieldSQL = TableConfig.getParentTableDisplayOnChildFieldFromSystemName(systemConfigFileName, systemName, tableName);
            //测试用
            string parentSelectFieldSQL = "";
            if (parentSelectFieldSQL != "")
            {
                if (selectFieldsSQL == "")
                    selectFieldsSQL = parentSelectFieldSQL;
                else
                    selectFieldsSQL = selectFieldsSQL + "," + parentSelectFieldSQL;
            }
            string xPath = "//Table[@EName='" + tableName + "']/tr/td/Query";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            foreach (XmlNode node in nodelist)
            {
                XmlNode fieldNode = node.ParentNode;
                string fieldName = fieldNode.Attributes.GetNamedItem("ColumnEName").InnerXml;
                //主键已经加过
                if (hashPK[fieldName] != null)
                    continue;
                string fieldDesc = fieldNode.Attributes.GetNamedItem("ColumnCName").InnerXml;
                if (selectFieldsSQL != "")
                    selectFieldsSQL += ",";
                selectFieldsSQL += string.Format(selectFieldsSQLModal, tableName, fieldName, fieldDesc);
            }
            return selectFieldsSQL;
        }





        /// <summary>
        /// 从配置文件中（可以是应用系统的配置文件、也可以是数据库的配置文件），获取字段的扩展属性定义情况
        /// 比如：用来查询的字段的情况等
        /// 如果出现异常，返回null
        /// </summary>
        /// <param name="configFileName"></param>
        /// <param name="xPath">//Table[@EName='" + tableName + "']/tr/td/Query</param>
        /// <returns></returns>
        public static Hashtable getTableConfigFieldExpandAttrFromXPath(string configFileName, string xPath)
        {
            Hashtable returnValue = new Hashtable();
            if (configFileName == null)
                return returnValue;
            if (System.IO.File.Exists(configFileName) == false)
                return returnValue;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(configFileName);
                XmlNodeList nodelist = doc.SelectNodes(xPath);
                foreach (XmlNode node in nodelist)
                {
                    //取字段名称
                    string fieldName = node.ParentNode.Attributes["ColumnEName"].InnerXml;
                    //取字段的基本参数定义情况
                    Hashtable hashFieldInfo = GetXMLData.getXmlNodeAttributes(node);
                    if (returnValue[fieldName] == null)
                        returnValue.Add(fieldName, hashFieldInfo);
                }
                //释放
                doc = null;
            }
            catch
            {
                throw;
            }
            return returnValue;
        }


        /// <summary>
        /// 根据应用系统的配置文件，获取查询字段的定义情况
        /// 返回哈希表，格式格式为：（字段名称，字段定义情况哈希表）
        /// “字段定义情况哈希表”：Table/tr/td/InputQuery的所有属性定义情况
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getFieldParamInputQueryFromSystemName(string systemName, string tableName)
        {
            string systemConfigFileName = TableConfig.getAppSystemConfigDiskFileName(systemName);
            string xPath = "//Table[@EName='" + tableName + "']/tr/td/InputQuery";
            return TableConfig.getTableConfigFieldExpandAttrFromXPath(systemConfigFileName, xPath);
        }


       
        
        /// <summary>
        /// 根据应用系统的配置文件，获取“数据列表”字段的定义情况
        /// 返回哈希表，格式格式为：（字段名称，字段定义情况哈希表）
        /// “字段定义情况哈希表”：Table/tr/td/Query的所有属性定义情况
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getFieldParamQueryFromSystemName(string systemName, string tableName)
        {
            string systemConfigFileName = TableConfig.getAppSystemConfigDiskFileName(systemName);
            string xPath = "//Table[@EName='" + tableName + "']/tr/td/Query";
            return TableConfig.getTableConfigFieldExpandAttrFromXPath(systemConfigFileName, xPath);
        }


        /// <summary>
        /// 从查询字段中取“允许汇总”的字段
        /// </summary>
        /// <param name="hashSelectField"></param>
        /// <returns></returns>
        public static Hashtable getFieldParamQueryAllowSumFromQueryFieldHashtable(Hashtable hashSelectField)
        {
            Hashtable returnValue = new Hashtable();
            System.Collections.IDictionaryEnumerator myEnumerator = hashSelectField.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                Hashtable hashQueryFieldInfo = (Hashtable)myEnumerator.Value;
                if (hashQueryFieldInfo["AllowSum"] != null)
                {
                    if (hashQueryFieldInfo["AllowSum"].ToString().ToLower() == "yes")
                    {
                        string fieldName = myEnumerator.Key.ToString();
                        returnValue.Add(fieldName, fieldName);
                    }
                }
            }
            return returnValue;
        }


        /// <summary>
        /// 根据应用系统的配置文件，获取“数据列表”的“允许汇总”字段
        /// 返回哈希表，格式格式为：（字段名称，字段名称）
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getFieldParamQueryAllowSumFromSystemName(string systemName, string tableName)
        {
            //应用系统定义的显示的字段
            Hashtable hashSelectField = TableConfig.getFieldParamQueryFromSystemName(systemName, tableName);
            return TableConfig.getFieldParamQueryAllowSumFromQueryFieldHashtable(hashSelectField);
        }


        /// <summary>
        /// 根据应用系统的配置文件，获取录入字段的定义情况
        /// 返回哈希表，格式格式为：（字段名称，字段定义情况哈希表）
        /// “字段定义情况哈希表”：Table/tr/td/Input的所有属性定义情况
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getFieldParamInputFromSystemName(string systemName, string tableName)
        {
            string systemConfigFileName = TableConfig.getAppSystemConfigDiskFileName(systemName);
            string xPath = "//Table[@EName='" + tableName + "']/tr/td/Input";
            return TableConfig.getTableConfigFieldExpandAttrFromXPath(systemConfigFileName, xPath);
        }


        /// <summary>
        /// 根据应用系统的配置文件，获取录入字段的显示的行数(整数)
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getFieldNameInputColumnHeightFromSystemName(string systemName, string tableName)
        {
            Hashtable returnValue = new Hashtable();
            Hashtable hashFieldInput = TableConfig.getFieldParamInputFromSystemName(systemName, tableName);
            System.Collections.IDictionaryEnumerator myEnumerator = hashFieldInput.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                string fieldName = myEnumerator.Key.ToString();
                Hashtable hashInputParam = (Hashtable)myEnumerator.Value;
                if (hashInputParam["ColumnHeight"] != null)
                {
                    string fieldColHeight = hashInputParam["ColumnHeight"].ToString();
                    int columnHeight = 1;
                    try
                    {
                        columnHeight = int.Parse(fieldColHeight);
                    }
                    catch
                    {
                        columnHeight = 1;
                    }
                    returnValue.Add(fieldName, columnHeight);
                }
            }
            return returnValue;
        }

        
        /// <summary>
        /// 根据应用系统的配置文件，获取查询字段的定义情况
        /// 返回哈希表，格式格式为：（字段名称，字段定义情况哈希表）
        /// “字段定义情况哈希表”：Table/tr/td/Brief的所有属性定义情况
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getFieldParamBriefFromSystemName(string systemName, string tableName)
        {
            string systemConfigFileName = TableConfig.getAppSystemConfigDiskFileName(systemName);
            string xPath = "//Table[@EName='" + tableName + "']/tr/td/Brief";
            return TableConfig.getTableConfigFieldExpandAttrFromXPath(systemConfigFileName, xPath);
        }





        /// <summary>
        /// 根据应用系统的配置文件，获取查询字段的定义情况
        /// 返回哈希表，格式格式为：（字段名称，字段定义情况哈希表）
        /// “字段定义情况哈希表”：Table/tr/td/Dictionary的所有属性定义情况
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getFieldParamDictionaryFromSystemName(string systemName, string tableName)
        {
            string systemConfigFileName = TableConfig.getAppSystemConfigDiskFileName(systemName);
            string xPath = "//Table[@EName='" + tableName + "']/tr/td/Dictionary";
            return TableConfig.getTableConfigFieldExpandAttrFromXPath(systemConfigFileName, xPath);
        }




        /// <summary>
        /// 根据数据库的配置文件，获取查询字段的定义情况
        /// 返回哈希表，格式格式为：（字段名称，字段定义情况哈希表）
        /// “字段定义情况哈希表”：Table/tr/td/InputQuery的所有属性定义情况
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getFieldParamInputQueryFromDatabaseName(string dbName, string tableName)
        {
            string dbConfigFileName = TableConfig.getTableConfigDiskFileName(dbName);
            string xPath = "//Table[@EName='" + tableName + "']/tr/td/InputQuery";
            return TableConfig.getTableConfigFieldExpandAttrFromXPath(dbConfigFileName, xPath);
        }




        /// <summary>
        /// 根据数据库的配置文件，获取查询字段的定义情况
        /// 返回哈希表，格式格式为：（字段名称，字段定义情况哈希表）
        /// “字段定义情况哈希表”：Table/tr/td/Query的所有属性定义情况
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getFieldParamQueryFromDatabaseName(string dbName, string tableName)
        {
            string dbConfigFileName = TableConfig.getTableConfigDiskFileName(dbName);
            string xPath = "//Table[@EName='" + tableName + "']/tr/td/Query";
            return TableConfig.getTableConfigFieldExpandAttrFromXPath(dbConfigFileName, xPath);
        }


        /// <summary>
        /// 根据数据库的配置文件，获取查询字段的定义情况
        /// 返回哈希表，格式格式为：（字段名称，字段定义情况哈希表）
        /// “字段定义情况哈希表”：Table/tr/td/Input的所有属性定义情况
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getFieldParamInputFromDatabaseName(string dbName, string tableName)
        {
            string dbConfigFileName = TableConfig.getTableConfigDiskFileName(dbName);
            string xPath = "//Table[@EName='" + tableName + "']/tr/td/Input";
            return TableConfig.getTableConfigFieldExpandAttrFromXPath(dbConfigFileName, xPath);
        }


        /// <summary>
        /// 根据数据库的配置文件，获取查询字段的定义情况
        /// 返回哈希表，格式格式为：（字段名称，字段定义情况哈希表）
        /// “字段定义情况哈希表”：Table/tr/td/Brief的所有属性定义情况
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getFieldParamBriefFromDatabaseName(string dbName, string tableName)
        {
            string dbConfigFileName = TableConfig.getTableConfigDiskFileName(dbName);
            string xPath = "//Table[@EName='" + tableName + "']/tr/td/Brief";
            return TableConfig.getTableConfigFieldExpandAttrFromXPath(dbConfigFileName, xPath);
        }




        /// <summary>
        /// 根据数据库的配置文件，获取查询字段的定义情况
        /// 返回哈希表，格式格式为：（字段名称，字段定义情况哈希表）
        /// “字段定义情况哈希表”：Table/tr/td/Dictionary的所有属性定义情况
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getFieldParamDictionaryFromDatabaseName(string dbName, string tableName)
        {
            string dbConfigFileName = TableConfig.getTableConfigDiskFileName(dbName);
            string xPath = "//Table[@EName='" + tableName + "']/tr/td/Dictionary";
            return TableConfig.getTableConfigFieldExpandAttrFromXPath(dbConfigFileName, xPath);
        }




        /// <summary>
        /// 根据数据库的配置文件，获取“浏览”字段的定义情况
        /// 返回哈希表，格式格式为：（字段名称，字段定义情况哈希表）
        /// “字段定义情况哈希表”：Table/tr/td/Browse的所有属性定义情况
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getFieldParamBrowseFromDatabaseName(string dbName, string tableName)
        {
            string dbConfigFileName = TableConfig.getTableConfigDiskFileName(dbName);
            string xPath = "//Table[@EName='" + tableName + "']/tr/td/Browse";
            return TableConfig.getTableConfigFieldExpandAttrFromXPath(dbConfigFileName, xPath);
        }




        /// <summary>
        /// 根据数据库的配置文件，获取“视图”字段的定义情况
        /// 返回哈希表，格式格式为：（字段名称，字段定义情况哈希表）
        /// “字段定义情况哈希表”：Table/tr/td/View的所有属性定义情况
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getFieldParamViewFromDatabaseName(string dbName, string tableName)
        {
            string dbConfigFileName = TableConfig.getTableConfigDiskFileName(dbName);
            string xPath = "//Table[@EName='" + tableName + "']/tr/td/View";
            return TableConfig.getTableConfigFieldExpandAttrFromXPath(dbConfigFileName, xPath);
        }



        /// <summary>
        /// 根据“应用系统”的配置文件，获取“视图”字段的定义情况
        /// 返回哈希表，格式格式为：（字段名称，字段定义情况哈希表）
        /// “字段定义情况哈希表”：Table/tr/td/View的所有属性定义情况
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getFieldParamViewFromSystemName(string systemName, string tableName)
        {
            string systemConfigFileName = TableConfig.getAppSystemConfigDiskFileName(systemName);
            string xPath = "//Table[@EName='" + tableName + "']/tr/td/View";
            return TableConfig.getTableConfigFieldExpandAttrFromXPath(systemConfigFileName, xPath);
        }



        /// <summary>
        /// 从配置文件中（可以是应用系统的配置文件、也可以是数据库的配置文件），获取所有字段的基本属性定义情况
        /// 返回的数据格式：（字段名称，该字段的所有基本属性的定义情况）
        /// 注意：不包括扩展属性的定义情况
        /// </summary>
        /// <param name="configFileName"></param>
        /// <param name="xPath">//Table[@EName='" + tableName + "']/tr/td</param>
        /// <returns></returns>
        public static Hashtable getTableConfigFieldAttrFromXPath(string configFileName, string xPath)
        {
            Hashtable returnValue = new Hashtable();
            if (configFileName == null)
                return returnValue;
            if (System.IO.File.Exists(configFileName) == false)
                return returnValue;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(configFileName);
                XmlNodeList nodelist = doc.SelectNodes(xPath);
                foreach (XmlNode node in nodelist)
                {
                    //取字段的基本参数定义情况
                    Hashtable hashFieldInfo = GetXMLData.getXmlNodeAttributes(node);
                    if (hashFieldInfo["ColumnEName"] == null)
                        continue;
                    string fieldName = hashFieldInfo["ColumnEName"].ToString();
                    if (returnValue[fieldName] == null)
                        returnValue.Add(fieldName, hashFieldInfo);
                }
                //释放
                doc = null;
            }
            catch
            {
                throw;
            }
            return returnValue;
        }



        /// <summary>
        /// 根据应用系统的配置文件，获取所有基本字段的定义情况
        /// 返回哈希表，格式格式为：（字段名称，所有字段的基本属性定义情况哈希表）
        /// 注意：不包括扩展属性的定义情况
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getFieldAttrAllFromSystemName(string systemName, string tableName)
        {
            //取该应用的配置文件名称
            string systemConfigFileName = TableConfig.getAppSystemConfigDiskFileName(systemName);
            string xPath = "//Table[@EName='" + tableName + "']/tr/td";
            return TableConfig.getTableConfigFieldAttrFromXPath(systemConfigFileName, xPath);
        }



        /// <summary>
        /// 根据数据库的配置文件，获取所有基本字段的定义情况
        /// 返回哈希表，格式格式为：（字段名称，所有字段的基本属性定义情况哈希表）
        /// 注意：不包括扩展属性的定义情况
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getFieldAttrAllFromDatabaseName(string dbName, string tableName)
        {
            //取该数据库的配置文件名称
            string dbConfigFileName = TableConfig.getTableConfigDiskFileName(dbName);
            string xPath = "//Table[@EName='" + tableName + "']/tr/td";
            return TableConfig.getTableConfigFieldAttrFromXPath(dbConfigFileName, xPath);
        }



        /// <summary>
        /// 从配置文件中（可以是应用系统的配置文件、也可以是数据库的配置文件），获取某个表的基本属性定义情况
        /// 返回的数据格式：（表名称，该表的所有基本属性的定义情况）
        /// 必须只有唯一的一个表满足才返回其参数
        ///  xPath = "//Table[@EName='" + tableName + "']";
        ///  
        /// </summary>
        /// <param name="configFileName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getTableConfigTableAttrFromConfigFileName(string configFileName, string tableName)
        {
            Hashtable returnValue = new Hashtable();
            if (configFileName == null)
                return returnValue;
            if (System.IO.File.Exists(configFileName) == false)
                return returnValue;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(configFileName);
                string xPath = "//Table[@EName='" + tableName + "']";
                XmlNodeList nodelist = doc.SelectNodes(xPath);
                if(nodelist.Count == 1)
                    returnValue = GetXMLData.getXmlNodeAttributes(nodelist[0]);
                //释放
                doc = null;
            }
            catch
            {
                throw;
            }
            return returnValue;
        }



        /// <summary>
        /// 从应用系统的配置文件中，取一个表的所有定义的属性（参数），返回哈希表（参数名称，参数值），都是字符串
        /// 必须只有唯一的一个表满足才返回其参数
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getTableAttrAllFromSystemName(string systemName, string tableName)
        {
            //取该应用的配置文件名称
            string systemConfigFileName = TableConfig.getAppSystemConfigDiskFileName(systemName);
            return TableConfig.getTableConfigTableAttrFromConfigFileName(systemConfigFileName, tableName);
        }



        /// <summary>
        /// 从数据库的配置文件中，取一个表的所有定义的属性（参数），返回哈希表（参数名称，参数值），都是字符串
        /// 必须只有唯一的一个表满足才返回其参数
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getTableAttrAllFromDatabaseName(string dbName, string tableName)
        {
            //取该数据库的配置文件名称
            string dbConfigFileName = TableConfig.getTableConfigDiskFileName(dbName);
            return TableConfig.getTableConfigTableAttrFromConfigFileName(dbConfigFileName, tableName);
        }




        /// <summary>
        /// 从配置文件中（可以是应用系统的配置文件、也可以是数据库的配置文件），获取某个表的所有子表
        /// 返回的数据格式哈希表Hashtable：（子表名称，子表描述）
        ///  xPath = "//Table[@EName='" + tableName + "']/tr/Table";
        ///  
        /// </summary>
        /// <param name="configFileName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getChildTableFromConfigFileName(string configFileName, string tableName)
        {
            Hashtable returnValue = new Hashtable();
            if (configFileName == null)
                return returnValue;
            if (System.IO.File.Exists(configFileName) == false)
                return returnValue;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(configFileName);
                string xPath = "//Table[@EName='" + tableName + "']/tr/Table";
                XmlNodeList nodelist = doc.SelectNodes(xPath);
                foreach(XmlNode node in nodelist)
                {
                    string childTableName = node.Attributes["EName"].InnerXml;
                    string childTableDesc = node.Attributes["TableCName"].InnerXml;
                    if (returnValue[childTableName] == null)
                        returnValue.Add(childTableName, childTableDesc);
                }
                //释放
                doc = null;
            }
            catch
            {
                throw;
            }
            return returnValue;
        }




        /// <summary>
        /// 从应用系统的配置文件，获取某个表的所有子表
        /// 返回的数据格式哈希表Hashtable：（子表名称，子表描述）
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getChildTableFromSystemName(string systemName, string tableName)
        {
            //取该应用的配置文件名称
            string systemConfigFileName = TableConfig.getAppSystemConfigDiskFileName(systemName);
            return TableConfig.getChildTableFromConfigFileName(systemConfigFileName, tableName);
        }



        /// <summary>
        /// 从数据库的配置文件，获取某个表的所有子表
        /// 返回的数据格式哈希表Hashtable：（子表名称，子表描述）
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getChildTableFromDatabaseName(string dbName, string tableName)
        {
            //取该应用的配置文件名称
            string dbConfigFileName = TableConfig.getTableConfigDiskFileName(dbName);
            return TableConfig.getChildTableFromConfigFileName(dbConfigFileName, tableName);
        }


        /// <summary>
        /// 从配置文件中（可以是应用系统的配置文件、也可以是数据库的配置文件），获取某个表的所有子表
        /// 返回的数据格式队列Queue：（子表名称）
        ///  xPath = "//Table[@EName='" + tableName + "']/tr/Table";
        ///  
        /// </summary>
        /// <param name="configFileName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Queue getChildTableQueueFromConfigFileName(string configFileName, string tableName)
        {
            Queue returnValue = new Queue();
            if (configFileName == null)
                return returnValue;
            if (System.IO.File.Exists(configFileName) == false)
                return returnValue;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(configFileName);
                string xPath = "//Table[@EName='" + tableName + "']/tr/Table";
                XmlNodeList nodelist = doc.SelectNodes(xPath);
                foreach (XmlNode node in nodelist)
                {
                    string childTableName = node.Attributes["EName"].InnerXml;
                    returnValue.Enqueue(childTableName);
                }
                //释放
                doc = null;
            }
            catch
            {
                throw;
            }
            return returnValue;
        }



        /// <summary>
        /// 从应用系统的配置文件，获取某个表的所有子表
        /// 返回的数据格式队列Queue：（子表名称）
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Queue getChildTableQueueFromSystemName(string systemName, string tableName)
        {
            //取该应用的配置文件名称
            string systemConfigFileName = TableConfig.getAppSystemConfigDiskFileName(systemName);
            return TableConfig.getChildTableQueueFromConfigFileName(systemConfigFileName, tableName);
        }



        /// <summary>
        /// 从数据库的配置文件，获取某个表的所有子表
        /// 返回的数据格式队列Queue：（子表名称）
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Queue getChildTableQueueFromDatabaseName(string dbName, string tableName)
        {
            //取该数据库的配置文件名称
            string dbConfigFileName = TableConfig.getTableConfigDiskFileName(dbName);
            return TableConfig.getChildTableQueueFromConfigFileName(dbConfigFileName, tableName);
        }



        /// <summary>
        /// 根据表的名称，从应用系统配置文件中取其对应的表描述名称
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string getAppSystemTableDescFromTableName(string systemName, string tableName)
        {
            string returnValue = "";
            //取该应用的配置文件名称
            string systemConfigFileName = TableConfig.getAppSystemConfigDiskFileName(systemName);
            if (systemConfigFileName == null)
                return returnValue;
            //根据表的描述名称。定位到该表
            string xPath = "//Table[@EName='" + tableName + "']";
            XmlDocument doc = new XmlDocument();
            doc.Load(systemConfigFileName);
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            if (nodelist.Count == 1)
            {
                XmlNode node = nodelist[0];
                if (node.Name == "Table")
                {
                    //取到表名称
                    returnValue = node.Attributes.GetNamedItem("TableCName").InnerXml;
                }
            }
            return returnValue;
        }



        /// <summary>
        /// 根据应用系统名称，取其帮助正文(尚未实现)
        /// </summary>
        /// <param name="systemName"></param>
        /// <returns></returns>
        public static string getHelpContentFromSystemName(string systemName, string tableName)
        {
            string returnValue = "";
            //取该应用的配置文件名称
            string systemConfigFileName = TableConfig.getAppSystemConfigDiskFileName(systemName);
            if (systemConfigFileName == null)
                return returnValue;
            //根据表的描述名称。定位到该表
            string xPath = "//Table[@EName='" + tableName + "']";
            XmlDocument doc = new XmlDocument();
            doc.Load(systemConfigFileName);
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            if (nodelist.Count == 1)
            {
                XmlNode node = nodelist[0];
                if (node.Name == "HelpDoc")
                {
                    //取到表名称
                    returnValue = node.Attributes.GetNamedItem("HelpContent").InnerXml;
                }
            }
            return returnValue;
        }


        /// <summary>
        /// 根据数据库名称，从数据库配制文件中取表的字段列表
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getDatabaseConfigFieldNameDescFromDatabaseName(string dbName, string tableName)
        {
            string xmlConfigFileName = TableConfig.getTableConfigDiskFileName(dbName);
            return TableConfig.getTableFieldNameDesc(xmlConfigFileName, tableName);
        }

        /// <summary>
        /// 根据应用系统名称，从数据库配制文件中取表的字段列表
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getDatabaseConfigFieldNameDescFromSystemName(string systemName, string tableName)
        {
            string dbName = TableConfig.getDatabaseNameFromSystemName(systemName);
            return TableConfig.getDatabaseConfigFieldNameDescFromDatabaseName(dbName, tableName);
        }




        /// <summary>
        /// 根据字段的基本信息，生成一个字段XML结点
        /// 创建一个字段的XmlNode结点
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="hashFieldInfo">数据库里定义的字段信息哈希表</param>
        /// <returns></returns>
        public static XmlNode createFieldXmlNode(XmlDocument xmlDoc, Hashtable hashFieldInfo)
        {
            if (hashFieldInfo["字段名称"] == null)
            {
                return null;
            }
            //字段名称
            string fieldName = hashFieldInfo["字段名称"].ToString();
            //字段描述
            string fieldDesc = "";
            if (hashFieldInfo["字段说明"] != null)
            {
                fieldDesc = hashFieldInfo["字段说明"].ToString();
            }
            //如果没有描述名称，则默认为字段名称
            if (fieldDesc == "")
                fieldDesc = fieldName;
            //字段类型
            string fieldTypeXml = "-1";
            string fieldType = "";
            if (hashFieldInfo["字段类型"] != null)
            {
                fieldType = hashFieldInfo["字段类型"].ToString();
                fieldTypeXml = ConvertData.convertSQLServerFieldTypeToXML(fieldType.ToLower());
            }
            //允许空
            string allowNull = "Yes";
            if (hashFieldInfo["允许空"] != null)
            {
                if (hashFieldInfo["允许空"].ToString() == "是")
                    allowNull = "Yes";
                else
                    allowNull = "No";
            }
            //默认值
            string defaultValue = "";
            if (hashFieldInfo["默认值"] != null)
            {
                defaultValue = hashFieldInfo["默认值"].ToString();
            }
            //小数位数
            string precision = "0";
            if (hashFieldInfo["小数位数"] != null)
            {
                precision = hashFieldInfo["小数位数"].ToString();
            }
            if (precision == "")
                precision = "0";
            XmlAttribute xmlAttr = null;
            XmlNode xmlNewNode = xmlDoc.CreateNode(XmlNodeType.Element, "td", "");
            xmlNewNode.InnerXml = "<Query DisplayOnQuery='No' ColumnHeight='1' FunctionOnQuery='' DisplayLength='' ColumnWidth='100%'>" +
                "</Query>" +
                "<Input DisplayOnInput='Yes' ColumnHeight='1' FunctionOnInput=''>" +
                "</Input>" +
                "<Brief DisplayOnBrief='No' DisplayLength='33'>" +
                "</Brief>";

            xmlAttr = xmlDoc.CreateAttribute("ColumnCName");
            xmlAttr.Value = fieldDesc;
            xmlNewNode.Attributes.Append(xmlAttr);

            xmlAttr = xmlDoc.CreateAttribute("ColumnEName");
            xmlAttr.Value = fieldName;
            xmlNewNode.Attributes.Append(xmlAttr);

            xmlAttr = xmlDoc.CreateAttribute("ColumnAbrvName");
            xmlAttr.Value = "Abr";
            xmlNewNode.Attributes.Append(xmlAttr);

            xmlAttr = xmlDoc.CreateAttribute("ColumnType");
            xmlAttr.Value = fieldTypeXml;
            xmlNewNode.Attributes.Append(xmlAttr);

            xmlAttr = xmlDoc.CreateAttribute("ColumnPrecision");
            xmlAttr.Value = precision;
            xmlNewNode.Attributes.Append(xmlAttr);

            xmlAttr = xmlDoc.CreateAttribute("ColumnDefault");
            xmlAttr.Value = defaultValue;
            xmlNewNode.Attributes.Append(xmlAttr);

            xmlAttr = xmlDoc.CreateAttribute("ReadOnly");
            xmlAttr.Value = "No";
            xmlNewNode.Attributes.Append(xmlAttr);

            xmlAttr = xmlDoc.CreateAttribute("KeyIndex");
            xmlAttr.Value = "No";
            xmlNewNode.Attributes.Append(xmlAttr);

            xmlAttr = xmlDoc.CreateAttribute("DisplayOrder");
            xmlAttr.Value = "1";
            xmlNewNode.Attributes.Append(xmlAttr);

            //==================新增加的Index索引===========
            xmlAttr = xmlDoc.CreateAttribute("Index");
            xmlAttr.Value = "No";
            xmlNewNode.Attributes.Append(xmlAttr);

            //==============是否允许为空==============
            xmlAttr = xmlDoc.CreateAttribute("AllowNull");
            xmlAttr.Value = allowNull;
            xmlNewNode.Attributes.Append(xmlAttr);
            return xmlNewNode;
        }



        /// <summary>
        /// 判断一个应用系统,是否同屏录入数据
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static bool isInputDataWithDetailInfo(string systemName, string tableName)
        {
            bool isWithDetailData = false;//是否同屏录入
            Hashtable hashAppFieldList =  TableConfig.getTableAttrAllFromSystemName(systemName, tableName);
            if(hashAppFieldList["SubsIuputShow"] != null)
            {
                string xmlValue = hashAppFieldList["SubsIuputShow"].ToString().ToLower();
                if ((xmlValue == "yes") || (xmlValue == "true") || (xmlValue == "1"))
                    isWithDetailData = true;
            }
            return isWithDetailData;

        }



        /// <summary>
        /// 从配置文件中(style.xml),获取应用程序的样式文件
        /// 具体的样式文件保存在D:\WebSiteOA\DBQuery\css\Style\
        /// </summary>
        /// <param name="systemName"></param>
        /// <returns></returns>
        public static string getCssFile(string systemName)
        {
            string cssFile = "";
            string cssConfigFileName = TableConfig.getCssStyleDiskFileName(systemName);
            if (System.IO.File.Exists(cssConfigFileName))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(cssConfigFileName);
                string xPath = "/StyleConfig";
                XmlNodeList nodelist = doc.SelectNodes(xPath);
                if (nodelist.Count >= 1)
                {
                    Hashtable hashData = GetXMLData.getXmlNodeNameAndValue(nodelist[0]);
                    if (hashData["File"] != null)
                    {
                        cssFile = hashData["File"].ToString();
                        cssFile = "../DBQuery/" + cssFile;
                    }
                }
            }
            return cssFile;
        }




    }





}
