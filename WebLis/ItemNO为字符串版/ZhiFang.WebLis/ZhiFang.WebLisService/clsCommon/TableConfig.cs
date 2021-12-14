using System;
using System.Xml;
using System.Collections;
using System.Configuration;
using System.Data;

namespace ZhiFang.WebLisService.clsCommon
{
    /// <summary>
    /// ��������TablesConfig��������ݿ⡢��ṹ����
    /// ��������Ӧ�����ݴ洢�ļ�TablesData
    /// </summary>
    public class TableConfig
    {
        public static string DataBaseDiskPath = "";//���ݿⶨ��XML�ļ����ڵĴ���Ŀ¼

        public TableConfig()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }




        /// <summary>
        /// �ϲ�AppSystemData����Ĳ�ѯ����
        /// </summary>
        /// <param name="appSystemData"></param>
        /// <returns></returns>
        public static string mergeALLConditionSQL(AppSystemData appSystemData)
        {
            string conditionSQLALL = appSystemData.ForeignKeySQL;//�ȼ����
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
        /// ȡһ����Ĳ�ѯ���������������������
        /// </summary>
        /// <param name="queryTableNode"></param>
        /// <returns></returns>
        public static string getQueryConditionScriptFromQueryTableNode(XmlNode queryTableNode, string dbName)
        {
            string tableName = TableConfig.getTableNameFromQueryNode(queryTableNode);
            //ȡ�ñ��������Ϣ
            TableConfigInfo tableConfigInfo = TableConfig.getTableConfigInfoFromDatabaseName(dbName, tableName);
            //ȡһ����Ĳ�ѯ����
            string xPath = "./DFD/Condition";
            XmlNodeList nodelistCondition = queryTableNode.SelectNodes(xPath);
            string sqlCondition = "";//���еĲ�ѯ������Condition��Condition֮���ǻ��ߵĹ�ϵ
            foreach (XmlNode nodeCondition in nodelistCondition)
            {
                xPath = "./Match";
                XmlNodeList nodelistMatch = nodeCondition.SelectNodes(xPath);
                string fieldCondition = "";//һ��Condition�����������Match֮������Ĺ�ϵ
                foreach (XmlNode nodeMatch in nodelistMatch)
                {
                    //
                    if (nodeMatch.Attributes.GetNamedItem("field") == null)
                        continue;
                    string fieldName = nodeMatch.Attributes.GetNamedItem("field").InnerXml;
                    string fieldValue = nodeMatch.Attributes.GetNamedItem("const").InnerXml;
                    string fieldComp = nodeMatch.Attributes.GetNamedItem("comp").InnerXml;
                    //�ֶε�����
                    FieldType fieldType = FieldType.NONE;
                    if (tableConfigInfo.FieldNameType[fieldName] != null)
                    {
                        fieldType = (FieldType)tableConfigInfo.FieldNameType[fieldName];
                    }
                    //����SQL��ѯ�������
                    string matchCondition = "";
                    if ((fieldName == "true") && (fieldValue == "true") && (fieldValue == "true"))//��true(�ڲ��涨)
                        matchCondition = "1=1";
                    else if ((fieldName == "false") && (fieldValue == "false") && (fieldValue == "false"))//��false(�ڲ��涨)
                        matchCondition = "1=0";
                    else //�����Ĳ�ѯ��������ѯ����ת��
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
                    //��ͬһ���������ǻ�Ĺ�ϵ
                    sqlCondition += "  OR  ";
                }
                //��һ������
                sqlCondition += fieldCondition;
            }
            return sqlCondition;
        }

        /// <summary>
        /// �������ݿ�����͡��ֶε����ͣ���ѯ������������һ�����ݿ��ѯ�������
        /// </summary>
        /// <param name="fieldName">�ֶ�����</param>
        /// <param name="fieldType">�ֶ�����</param>
        /// <param name="queryCondition">��ѯ����</param>
        /// <param name="fieldValue">��ѯ������</param>
        /// <param name="databaseType">���ݿ������</param>
        /// <returns></returns>
        public static string convertQueryConditionToDatabaseCondition(string tableName, string fieldName, FieldType fieldType, string queryCondition, string fieldValue, DatabaseType databaseType)
        {
            string fieldNameModal = "[{0}].[{1}]";
            fieldName = string.Format(fieldNameModal, tableName, fieldName);
            //ת���ֶε����ݸ�ʽ
            string dbCondition = "";
            switch (queryCondition)
            {
                case "equal"://����
                    dbCondition = " = ";
                    if (fieldValue == "") //��մ�
                    {
                        dbCondition = fieldName + " IS NULL OR " + fieldName + "=''";
                    }
                    else
                    {
                        //ת������
                        fieldValue = TableConfig.convertFieldValueToDatabase(fieldValue, fieldType, databaseType);
                        dbCondition = fieldName + dbCondition + fieldValue;
                    }
                    break;
                case "ne"://������
                    dbCondition = " <> ";
                    if (fieldValue == "") //��մ�
                    {
                        dbCondition = fieldName + " IS NOT NULL OR " + fieldName + "<>''";
                    }
                    else
                    {
                        //ת������
                        fieldValue = TableConfig.convertFieldValueToDatabase(fieldValue, fieldType, databaseType);
                        dbCondition = fieldName + dbCondition + fieldValue;
                    }
                    break;
                case "gt"://����
                    dbCondition = " > ";
                    fieldValue = TableConfig.convertFieldValueToDatabase(fieldValue, fieldType, databaseType);
                    dbCondition = fieldName + dbCondition + fieldValue;
                    break;
                case "ge"://���ڵ���
                    dbCondition = " >= ";
                    fieldValue = TableConfig.convertFieldValueToDatabase(fieldValue, fieldType, databaseType);
                    dbCondition = fieldName + dbCondition + fieldValue;
                    break;
                case "lt"://С��
                    dbCondition = " < ";
                    fieldValue = TableConfig.convertFieldValueToDatabase(fieldValue, fieldType, databaseType);
                    dbCondition = fieldName + dbCondition + fieldValue;
                    break;
                case "le"://С�ڵ���
                    dbCondition = " <= ";
                    fieldValue = TableConfig.convertFieldValueToDatabase(fieldValue, fieldType, databaseType);
                    dbCondition = fieldName + dbCondition + fieldValue;
                    break;
                case "include"://����  //bug ������ ����2009-03-12, �������
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
                case "noinclude"://������
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
                case "includein"://������
                    dbCondition = "=";
                    break;
                case "begin"://��ʼ��
                    if (fieldValue == "")
                    {
                        dbCondition = fieldName + "='' OR " + fieldName + " IS NULL ";
                    }
                    else
                    {
                        dbCondition = "left(" + fieldName + "," + fieldValue.Length.ToString() + ") = '" + fieldValue + "'";
                    }
                    break;
                case "end"://������
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
        /// ת���ֶ�����Ϊ���ݿ��ʽ
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
                    //��ֵ�ͣ�ֱ�ӷ���
                    break;
                default:
                    //�ַ��ͻ������ͣ��ӵ�����
                    fieldValue = "'" + fieldValue + "'";
                    break;
            }
            return fieldValue;
        }
        public static string getQueryConditionScriptFromQueryTableNode_1(XmlNode queryTableNode, string dbName)
        {
            string tableName = TableConfig.getTableNameFromQueryNode(queryTableNode);
            //ȡ�ñ��������Ϣ
            TableConfigInfo tableConfigInfo = TableConfig.getTableConfigInfoFromDatabaseName(dbName, tableName);
            //ȡһ����Ĳ�ѯ����

            //�������� ���ݹ�������+���ݷ�������+��ѯ����+׼SQL+SQL
            //DFD1 and DFD2 and ��..DFDn
            //DFD1=(C11 OR C12 OR ��C1n)
            //����
            //C11=(Match1 and Match2 and �� Matchn)


            string xPathAllDFDs = "./DFD";
            XmlNodeList nodelistDFDs = queryTableNode.SelectNodes(xPathAllDFDs);
            string sqlDFD = "";
            foreach (XmlNode nodeDFD in nodelistDFDs)
            {
                string sqlCondition = "";//���еĲ�ѯ������Condition��Condition֮���ǻ��ߵĹ�ϵ DFD(and) Match(and)
                string xPath = "Condition";
                XmlNodeList nodelistCondition = nodeDFD.SelectNodes(xPath);
                foreach (XmlNode nodeCondition in nodelistCondition)
                {
                    xPath = "./Match";
                    XmlNodeList nodelistMatch = nodeCondition.SelectNodes(xPath);
                    string fieldCondition = "";//һ��Condition�����������Match֮������Ĺ�ϵ
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
                            //�ֶε�����
                            FieldType fieldType = FieldType.NONE;
                            if (tableConfigInfo.FieldNameType[fieldName] != null)
                            {
                                fieldType = (FieldType)tableConfigInfo.FieldNameType[fieldName];
                            }
                            //����SQL��ѯ�������
                            if ((fieldName == "true") && (fieldValue == "true") && (fieldValue == "true"))//��true(�ڲ��涨)
                                matchCondition = "1=1";
                            else if ((fieldName == "false") && (fieldValue == "false") && (fieldValue == "false"))//��false(�ڲ��涨)
                                matchCondition = "1=0";
                            else //�����Ĳ�ѯ��������ѯ����ת��
                                matchCondition = TableConfig.convertQueryConditionToDatabaseCondition(tableName, fieldName, fieldType, fieldComp, fieldValue, tableConfigInfo.dataBaseLoginInfo.DatabaseType);
                        }
                        if (matchCondition == "" || matchCondition == "()")
                            continue;

                        fieldCondition += " AND (" + matchCondition + ")";
                    }

                    if (fieldCondition.Length > 5)//��ͬcondition�������ǻ�(or)�Ĺ�ϵ(ȥ�����һ��AND)
                    {
                        fieldCondition = fieldCondition.Substring(5);
                        sqlCondition += " OR (" + fieldCondition + ")";
                    }
                }
                if (sqlCondition.Length > 4)//��ͬDFD����������(and)�Ĺ�ϵ(ȥ���ϴ��е�(OR)
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
        /// ��Ҫ��ѯ�ı��Ҷ�ӽ��ת���ɱ�׼��SQL��ѯ������䣺
        /// ֻ������������һ���֣�����SQL�����WHERE���������
        /// </summary>
        /// <param name="leafQueryNode">��ѯ�����ĸ����</param>
        /// <returns></returns>
        public static string makeQueryConditionScriptFromQueryNode(XmlNode leafQueryNode, string dbName)
        {

            if (leafQueryNode.Name != "Table")
                return null;
            //ȡҪ��ѯ�ı������
            string tableName = TableConfig.getTableNameFromQueryNode(leafQueryNode);
            if (tableName == "")
                return null;
            string parentTableName = "";
            XmlNode parentQueryNode = leafQueryNode.ParentNode;
            if (parentQueryNode.Name == "Table")
                parentTableName = TableConfig.getTableNameFromTableNode(parentQueryNode);
            TableConfigInfo tableConfigInfo = TableConfig.getTableConfigInfoFromDatabaseName(dbName, tableName);
            //ȡһ����Ĳ�ѯ����
            string sqlCondition = TableConfig.getQueryConditionScriptFromQueryTableNode_1(leafQueryNode, dbName);
            if (sqlCondition != "")
                sqlCondition = "(" + sqlCondition + ")";
            //�ݹ�
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
        /// �����ֶ�
        /// </summary>
        /// <param name="queryTableNode"></param>
        /// <param name="dbName"></param>
        /// <returns>���ء�sum(1),sum(2)</returns>
        public static string makeQuerySumFieldsFromQueryNode(XmlNode queryTableNode, string dbName,out string[] FieldSumListAsBefore,out string[] FieldList)
        {

            string tableName = TableConfig.getTableNameFromQueryNode(queryTableNode);
            //ȡ�ñ��������Ϣ
            TableConfigInfo tableConfigInfo = TableConfig.getTableConfigInfoFromDatabaseName(dbName, tableName);
            //ȡһ����Ĳ�ѯ����
            string xPath = "Analysis/Field";
            XmlNodeList nodelistCondition = queryTableNode.SelectNodes(xPath);
            string sqlOrderBy = "";//���еĲ�ѯ������Condition��Condition֮���ǻ��ߵĹ�ϵ
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
        /// ȡ����ʽ��
        /// ����SQL�����ORDER BY���������
        /// </summary>
        /// <param name="leafQueryNode">��ѯ�����ĸ����</param>
        /// <returns></returns>
        public static string makeQueryOrderByScriptFromQueryNode(XmlNode queryTableNode, string dbName)
        {

            string tableName = TableConfig.getTableNameFromQueryNode(queryTableNode);
            //ȡ�ñ��������Ϣ
            TableConfigInfo tableConfigInfo = TableConfig.getTableConfigInfoFromDatabaseName(dbName, tableName);
            //ȡһ����Ĳ�ѯ����
            string xPath = "DFD/Orderby/Field";
            XmlNodeList nodelistCondition = queryTableNode.SelectNodes(xPath);
            string sqlOrderBy = "";//���еĲ�ѯ������Condition��Condition֮���ǻ��ߵĹ�ϵ
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
            //�����ѯ����Ϊ�գ���Ĭ�ϰ����ؼ��ֵ�������
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
        /// ���ݱ��������Ϣ�����ɴ�����ṹ��SQLServer�ű�
        /// </summary>
        /// <param name="tableConfigInfo">���������Ϣ</param>
        /// <returns></returns>
        public static string makeCreateTableSQLServerScript(TableConfigInfo tableConfigInfo)
        {
            //���ɵ�SQL�ű�
            string sql = "";
            if ((tableConfigInfo.TableName == "") || (tableConfigInfo.TableName == null))
                return sql;
            //�����׺
            string GO = "\n";//\nGO\n
            //�ű�ģ�嶨��
            //�����ֶ��������õĸ�ʽ�������������������ֶ�
            string addDescriptioSqlModal = "exec sp_addextendedproperty N'MS_Description', N'{0}', N'user', N'dbo', N'table', N'{1}', N'column', N'{2}'" + GO;
            //���������ؼ��֣���ʽΪ��������������ơ��ֶ�����
            string addForeignKeySqlModal = "ALTER TABLE [dbo].[{0}] ADD CONSTRAINT FK_{0}_{2}_{4} FOREIGN KEY ({1}) REFERENCES [dbo].[{2}] ({3})" + GO;
            //�������ؼ��֣���ʽΪ���������������ơ��ֶ�����
            string addPrimaryKeySqlModal = "ALTER TABLE [dbo].[{0}] ADD CONSTRAINT [{1}] PRIMARY KEY  CLUSTERED ({2})  ON [PRIMARY]" + GO;
            //������ṹ�Ľű�����ʽΪ��������ơ��ֶ��б�
            string createTableSqlModal = "CREATE TABLE  [dbo].[{0}] (\n{1})" + GO;

            //�����SQL�ű�
            string addDescriptioSQL = "";//��������
            string addForeignKeySQL = "";//�������
            string addPrimaryKeySQL = "";//��������
            string createTableSQL = "";  //������
            string fieldListSQL = "";//�ֶ��б�
            string foreignKeyList = "";//������б��Ժ���������Ҫ�� 
            string foreignKeyRelationList = "";//������������ֶΣ��Ժ��������Ҫ�� 

            //��ǰ�����ı�����
            string tableName = tableConfigInfo.TableName;


            //ȡ�����������ֶμӵ����ֶ��б���ͬʱ������������Ľű�
            string foreignKeyName = "";
            System.Collections.IEnumerator enumeratorFK = tableConfigInfo.ForeignKeyStack.GetEnumerator();//�������Ҫ�д���
            while (enumeratorFK.MoveNext())
            {
                foreignKeyName = enumeratorFK.Current.ToString();
                if (tableConfigInfo.ForeignKeyRelation[foreignKeyName] == null)
                {
                    //û�ж�������ֶΣ�Ӧ���д�
                    continue;
                }
                string relationName = tableConfigInfo.ForeignKeyRelation[foreignKeyName].ToString();
                FieldType fieldType = (FieldType)tableConfigInfo.ForeignKeyType[foreignKeyName];
                //��������������ģ���Ϊ������
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
                //����ؼ��ֵ��ֶθ�ʽ�����ֶ����ƣ����ͣ�
                string foreignFieldSqlModal = "\t[{0}] {1} NOT NULL";
                fieldListSQL += string.Format(foreignFieldSqlModal, foreignKeyName, foreignKeyFieldType);//���ֶΡ�����
            }
            if (foreignKeyRelationList != "")
                addForeignKeySQL = string.Format(addForeignKeySqlModal, tableName, foreignKeyList, tableConfigInfo.PrimaryTable, foreignKeyRelationList, foreignKeyName);
            //������
            System.Collections.IDictionaryEnumerator myEnumeratorPK = tableConfigInfo.PrimaryKeyType.GetEnumerator();
            string primaryKeyFieldName = foreignKeyList;
            string fieldNamePK = "";
            while (myEnumeratorPK.MoveNext())
            {
                fieldNamePK = myEnumeratorPK.Key.ToString();
                //����ؼ��ֵ��ֶθ�ʽ�����ֶ����ƣ����ͣ�
                string primaryFieldSqlModal = "\t[{0}] {1} NOT NULL";
                //ȡ��ǰ������ؼ�������
                FieldType fieldType = (FieldType)myEnumeratorPK.Value;
                string primaryKeyFieldType = ConvertData.convertFieldTypeToSQLServer(fieldType);
                if (fieldListSQL != "")
                    fieldListSQL += ",\n";
                fieldListSQL += string.Format(primaryFieldSqlModal, fieldNamePK, primaryKeyFieldType);
                if (primaryKeyFieldName != "")
                    primaryKeyFieldName += ",";
                primaryKeyFieldName += fieldNamePK;
            }
            //�������ؼ��֣���ʽΪ���������������ơ��ֶ�����
            addPrimaryKeySQL = string.Format(addPrimaryKeySqlModal, tableName, "PK_" + tableName + "_" + fieldNamePK, primaryKeyFieldName);
            //ȡ�ֶ��б�ͬʱ��������
            System.Collections.IDictionaryEnumerator myEnumerator = tableConfigInfo.FieldNameDesc.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                string fieldName = myEnumerator.Key.ToString();//����
                string fieldDesc = myEnumerator.Value.ToString();//����
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
                //�������ֶ��Ѿ�����
                if (isAdd)
                    continue;
                string fieldSql = "\t[" + fieldName + "]";
                FieldType fieldType = (FieldType)tableConfigInfo.FieldNameType[myEnumerator.Key];//�ֶ�����
                string fieldTypeSQL = ConvertData.convertFieldTypeToSQLServer(fieldType);
                fieldSql += " " + fieldTypeSQL;
                if (fieldListSQL != "")
                    fieldListSQL += ",\n";
                fieldListSQL += fieldSql;
            }
            //ɾ����Ľű�
            string dropTableSqlModal = "if exists (select 1 from  sysobjects where  id = object_id('{0}') and  type = 'U') drop table [{0}]" + GO;
            string dropTableSql = string.Format(dropTableSqlModal, tableName);
            //������ṹ�Ľű�
            if (fieldListSQL == "")
                return null;
            createTableSQL = string.Format(createTableSqlModal, tableName, fieldListSQL);
            sql = createTableSQL + addForeignKeySQL + addPrimaryKeySQL + addDescriptioSQL;
            //System.Console.WriteLine(sql);
            return sql;
        }


        /// <summary>
        /// ����ĳ�����ݿ���ĳ�������ӱ�Ĵ�����ṹ�Ľű�
        /// ʹ�õݹ���б���
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string makeTableConfigSQLServerScript(string dbName, string tableName)
        {
            //���ɵ�SQL�ű�
            string sql = "";
            string configFileName = TableConfig.getTableConfigDiskFileName(dbName);
            if ((configFileName == "") || (configFileName == null))
                return sql;
            //�ļ�������
            if (System.IO.File.Exists(configFileName) == false)
                return sql;
            //ȡ���������Ϣ
            TableConfigInfo tableConfigInfo = TableConfig.getTableConfigInfoFromDatabaseName(dbName, tableName);
            //���ɵ�ǰ��Ľű�
            sql = TableConfig.makeCreateTableSQLServerScript(tableConfigInfo);
            //�������ļ���λ����ǰ����ӱ�
            XmlDocument doc = new XmlDocument();
            doc.Load(configFileName);
            string xPath = "//Table[@EName='" + tableName + "']/tr/Table";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            //�ӱ�Ľű���ʹ�õݹ鷽��
            string sqlChild = "";
            foreach (XmlNode node in nodelist)
            {
                //�����ӱ�
                string childTable = node.Attributes.GetNamedItem("EName").InnerXml;
                sqlChild += TableConfig.makeTableConfigSQLServerScript(dbName, childTable);
            }
            return sql + sqlChild;
        }




        /// <summary>
        /// ����һ����¼�������ݵ�INSERT �ű�
        /// </summary>
        /// <param name="tableConfigInfo"></param>
        /// <returns></returns>
        public static string makeInsertRecordSQLServerScript(TableConfigInfo tableConfigInfo)
        {
            string insertSQL = "";
            string fieldList = "";//�ֶ��б�
            string valueList = "";//ֵ�б�
            //�鿴��û��������еĻ��ȼ������ֵ
            //���������ֶ�
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
                //�����ģʽ
                fieldList += "[" + fieldName + "]";
                valueList += ConvertData.convertFieldValue(fieldValue, fieldType);
            }
            //���������ֶ�
            myEnumerator = tableConfigInfo.FieldNameValue.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                string fieldName = myEnumerator.Key.ToString();
                string fieldValue = myEnumerator.Value.ToString();
                //������
                if ((fieldValue != "") && (fieldValue != null))
                {
                    if (fieldList != "")
                    {
                        fieldList += ",";
                        valueList += ",";
                    }
                    //�ֶ�����
                    fieldList += "[" + fieldName + "]";
                    //�ֶ�����
                    FieldType fieldType = FieldType.NONE;
                    if (tableConfigInfo.FieldNameType[fieldName] != null)
                    {
                        fieldType = (FieldType)tableConfigInfo.FieldNameType[fieldName];
                        fieldValue = ConvertData.convertFieldValue(fieldValue, fieldType);
                    }
                    valueList += fieldValue;

                }
            }
            if (fieldList != "")//���ֶ�
            {

                //����SQL�������ݽű�ģ��
                string insertSqlModal = "INSERT INTO [{0}] ({1}) VALUES({2})";
                insertSQL = string.Format(insertSqlModal, tableConfigInfo.TableName, fieldList, valueList);
            }
            return insertSQL;
        }


        /// <summary>
        /// ��һ��TABLE����µ��������ݣ������ӱ�����INSERT INTO table �ű�
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="tableNode"></param>
        /// <returns></returns>
        public static string makeInsertSQLServerScriptFromTableNode(string dbName, XmlNode tableNode)
        {
            string sql = "";
            //��������XmlNode���Ӧ���Ǹ�Ŀ¼�µ�һ��Table���
            if (tableNode.Name != "Table")
            {
                return null;
            }
            //ȡ���������
            string tableName = tableNode.Attributes.GetNamedItem("EName").InnerXml;
            //ȡ���������Ϣ
            TableConfigInfo tableConfigInfo = TableConfig.getTableConfigInfoFromDatabaseName(dbName, tableName);
            //�����ý�㣨���µļ�¼
            string xPath = "./tr";
            XmlNodeList nodelistTR = tableNode.SelectNodes(xPath);
            foreach (XmlNode trNode in nodelistTR)
            {
                xPath = "./td";
                //ȡ���ü�¼
                tableConfigInfo.FieldNameValue = (Hashtable)TableConfig.getRecordDataFromXmlNodeTR(trNode).Clone();
                //ȡ�����ֵ
                if (tableConfigInfo.PrimaryTable != "")
                {
                    if (trNode.ParentNode.ParentNode != null)
                    {
                        //
                    }

                }
                //����һ��Record�ű�
                string insertRecord = TableConfig.makeInsertRecordSQLServerScript(tableConfigInfo);
                sql += insertRecord + "\nGO\n";
                //�鿴��û���ӱ�
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
        /// �����ݿ�������Ϣ���������ݿ����Ӵ�
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
        /// ȡĳ���������
        /// </summary>
        /// <param name="xmlConfigFileName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getParentTable(string xmlConfigFileName, string tableName)
        {
            if ((xmlConfigFileName == "") || (xmlConfigFileName == null))
                return null;
            //�ļ�������
            if (System.IO.File.Exists(xmlConfigFileName) == false)
                return null;
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlConfigFileName);
            //�ȶ�λ����ǰ��
            Hashtable hashtable = new Hashtable();
            string xPath = "//Table[@EName='" + tableName + "']";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            if (nodelist.Count == 1)
            {
                //�ҵ���ǰ���ж��Ƿ�����������ȡ������Ϣ����
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
        /// ȡĳ���������
        /// </summary>
        /// <param name="xmlConfigFileName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string getParentTableName(string xmlConfigFileName, string tableName)
        {
            string parentTableName = "";
            if ((xmlConfigFileName == "") || (xmlConfigFileName == null))
                return parentTableName;
            //�ļ�������
            if (System.IO.File.Exists(xmlConfigFileName) == false)
                return parentTableName;
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlConfigFileName);
            //�ȶ�λ����ǰ��
            string xPath = "//Table[@EName='" + tableName + "']";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            if (nodelist.Count == 1)
            {
                //�ҵ���ǰ���ж��Ƿ�����������ȡ������Ϣ����
                if (nodelist[0].ParentNode.ParentNode != null)
                {
                    XmlNode node = nodelist[0].ParentNode.ParentNode;
                    if (node.Name == "Table")
                    {
                        //ȡ����������
                        parentTableName = node.Attributes.GetNamedItem("EName").InnerXml;
                    }
                }

            }
            return parentTableName;
        }


        /// <summary>
        /// �������ļ���,ȡĳ��Ӧ��ϵͳ�µ����б�����ƺ�����:���ع�ϣ�������ƣ���������
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
        /// ��Ӧ��ϵͳ�����ļ���,ȡĳ��Ӧ��ϵͳ�µ����б�����ƺ�����:���ع�ϣ���������������ơ�
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
        /// �������ļ���,ȡĳ�����ݿ��µ����б�����ƺ�����:SortedList
        /// </summary>
        /// <param name="systemName"></param>
        /// <returns></returns>
        public static System.Collections.SortedList getAllTableNameDescSortListFromSystemName(string systemName)
        {
            //ȡ������ֶ����ƺ������б�
            Hashtable hashtable = TableConfig.getAllTableNameDescFromSystemName(systemName);
            //����
            System.Collections.SortedList sortedList = new SortedList();
            System.Collections.IDictionaryEnumerator myEnumerator = hashtable.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                sortedList.Add(myEnumerator.Key, myEnumerator.Value);
            }
            return sortedList;
        }






        /// <summary>
        /// �������ļ���,ȡĳ�����ݿ��µ����б�����ƺ�����:���ع�ϣ��
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
        /// �������ļ���,ȡĳ�����ݿ��µ����б�����ƺ�����:���ع�ϣ��
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
        /// �������ļ���,ȡĳ�����ݿ��µ����б�����ƺ�����:SortedList
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public static System.Collections.SortedList getAllTableNameDescSortedListFromDatabaseName(string dbName)
        {
            //ȡ������ֶ����ƺ������б�
            Hashtable hashtable = TableConfig.getAllTableNameDescFromDatabaseName(dbName);
            //����
            System.Collections.SortedList sortedList = new SortedList();
            System.Collections.IDictionaryEnumerator myEnumerator = hashtable.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                sortedList.Add(myEnumerator.Key, myEnumerator.Value);
            }
            return sortedList;
        }



        /// <summary>
        /// �������ļ���,ȡĳ�����ݿ��µ����б������
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
        /// ȡ����ı���ֶ����ơ��ֶ��������ŵ���ϣ��
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static System.Collections.Hashtable getTableFieldNameDesc(string xmlConfigFileName, string tableName)
        {
            if ((xmlConfigFileName == "") || (xmlConfigFileName == null))
                return null;
            //�ļ�������
            if (System.IO.File.Exists(xmlConfigFileName) == false)
                return null;
            Hashtable hashtable = new Hashtable();
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlConfigFileName);
            string xPath = "//Table[@EName='" + tableName + "']/tr/td";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            foreach (XmlNode node in nodelist)
            {
                string fieldName = node.Attributes.GetNamedItem("ColumnEName").InnerXml;//����
                string fieldDesc = node.Attributes.GetNamedItem("ColumnCName").InnerXml;//����
                if (hashtable[fieldName] == null)
                    hashtable.Add(fieldName, fieldDesc);
            }
            return hashtable;
        }





        /// <summary>
        /// ȡ����ı���ֶ����ơ��ֶ��������ŵ���ϣ��
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static System.Collections.Hashtable getTableFieldNameDescFromSystemName(string systemName, string tableName)
        {
            Hashtable hashtable = new Hashtable();
            string xmlConfigFileName = TableConfig.getAppSystemConfigDiskFileName(systemName);
            if ((xmlConfigFileName == "") || (xmlConfigFileName == null))
                return hashtable;
            //�ļ�������
            if (System.IO.File.Exists(xmlConfigFileName) == false)
                return hashtable;
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlConfigFileName);
            string xPath = "//Table[@EName='" + tableName + "']/tr/td";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            foreach (XmlNode node in nodelist)
            {
                string fieldName = node.Attributes.GetNamedItem("ColumnEName").InnerXml;//����
                string fieldDesc = node.Attributes.GetNamedItem("ColumnCName").InnerXml;//����
                if (hashtable[fieldName] == null)
                    hashtable.Add(fieldName, fieldDesc);
            }
            return hashtable;
        }





        /// <summary>
        /// ȡ����ı���ֶ����ơ��ֶ��������ŵ���ϣ��
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static System.Collections.Hashtable getTableFieldDescNameFromSystemName(string systemName, string tableName)
        {
            Hashtable hashtable = new Hashtable();
            string xmlConfigFileName = TableConfig.getAppSystemConfigDiskFileName(systemName);
            if ((xmlConfigFileName == "") || (xmlConfigFileName == null))
                return hashtable;
            //�ļ�������
            if (System.IO.File.Exists(xmlConfigFileName) == false)
                return hashtable;
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlConfigFileName);
            string xPath = "//Table[@EName='" + tableName + "']/tr/td";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            foreach (XmlNode node in nodelist)
            {
                string fieldName = node.Attributes.GetNamedItem("ColumnEName").InnerXml;//����
                string fieldDesc = node.Attributes.GetNamedItem("ColumnCName").InnerXml;//����
                if (hashtable[fieldDesc] == null)
                    hashtable.Add(fieldDesc, fieldName);
            }
            return hashtable;
        }



        /// <summary>
        /// ȡ����ı���ֶ��������ֶ����ƣ��ŵ���ϣ��
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static System.Collections.Hashtable getTableFieldDescName(string xmlConfigFileName, string tableName)
        {
            if ((xmlConfigFileName == "") || (xmlConfigFileName == null))
                return null;
            //�ļ�������
            if (System.IO.File.Exists(xmlConfigFileName) == false)
                return null;
            Hashtable hashtable = new Hashtable();
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlConfigFileName);
            string xPath = "//Table[@EName='" + tableName + "']/tr/td";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            foreach (XmlNode node in nodelist)
            {
                string fieldName = node.Attributes.GetNamedItem("ColumnEName").InnerXml;//����
                string fieldDesc = node.Attributes.GetNamedItem("ColumnCName").InnerXml;//����
                if (hashtable[fieldDesc] == null)
                    hashtable.Add(fieldDesc, fieldName);
            }
            return hashtable;
        }



        /// <summary>
        /// �������ļ���ȡ����ı���ֶ����ơ��ֶ�����FieldType���ŵ���ϣ��
        /// </summary>
        /// <param name="xmlConfigFileName">�����ļ�</param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static System.Collections.Hashtable getTableFieldNameType(string xmlConfigFileName, string tableName)
        {
            Hashtable hashtable = new Hashtable();
            if ((xmlConfigFileName == "") || (xmlConfigFileName == null))
                return hashtable;
            //�ļ�������
            if (System.IO.File.Exists(xmlConfigFileName) == false)
                return hashtable;
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlConfigFileName);
            string xPath = "//Table[@EName='" + tableName + "']/tr/td";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            foreach (XmlNode node in nodelist)
            {
                string fieldName = node.Attributes.GetNamedItem("ColumnEName").InnerXml;//����
                string fieldTypeXML = node.Attributes.GetNamedItem("ColumnType").InnerXml;//����
                FieldType fieldType = ConvertData.convertXmlFieldType(fieldTypeXML);
                if (hashtable[fieldName] == null)
                    hashtable.Add(fieldName, fieldType);
            }
            return hashtable;
        }



        /// <summary>
        /// �ӡ�Ӧ��ϵͳ�������ļ���ȡ����ı���ֶ����ơ��ֶ�����FieldType���ŵ���ϣ��
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static System.Collections.Hashtable getTableFieldNameTypeFromSystemName(string systemName, string tableName)
        {
            //ȡ��Ӧ�õ������ļ�����
            string systemConfigFileName = TableConfig.getAppSystemConfigDiskFileName(systemName);
            return TableConfig.getTableFieldNameType(systemConfigFileName, tableName);
        }



        /// <summary>
        /// �ӡ����ݿ⡱�����ļ���ȡ����ı���ֶ����ơ��ֶ�����FieldType���ŵ���ϣ��
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static System.Collections.Hashtable getTableFieldNameTypeFromDataBaseName(string dbName, string tableName)
        {
            //ȡ�����ݿ�������ļ�����
            string dbConfigFileName = TableConfig.getTableConfigDiskFileName(dbName);
            return TableConfig.getTableFieldNameType(dbConfigFileName, tableName);
        }





        /// <summary>
        /// �������ļ���ȡ����ı���ֶ��������ֶ�����FieldType���ŵ���ϣ��
        /// </summary>
        /// <param name="xmlConfigFileName">�����ļ�</param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static System.Collections.Hashtable getTableFieldDescType(string xmlConfigFileName, string tableName)
        {
            Hashtable hashtable = new Hashtable();
            if ((xmlConfigFileName == "") || (xmlConfigFileName == null))
                return hashtable;
            //�ļ�������
            if (System.IO.File.Exists(xmlConfigFileName) == false)
                return hashtable;
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlConfigFileName);
            string xPath = "//Table[@EName='" + tableName + "']/tr/td";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            foreach (XmlNode node in nodelist)
            {
                string fieldName = node.Attributes.GetNamedItem("ColumnEName").InnerXml;//����
                string fieldDesc = node.Attributes.GetNamedItem("ColumnCName").InnerXml;//����
                string fieldTypeXML = node.Attributes.GetNamedItem("ColumnType").InnerXml;//����
                FieldType fieldType = ConvertData.convertXmlFieldType(fieldTypeXML);
                if (hashtable[fieldDesc] == null)
                    hashtable.Add(fieldDesc, fieldType);
            }
            return hashtable;
        }



        /// <summary>
        /// �ӡ�Ӧ��ϵͳ�������ļ���ȡ����ı���ֶ��������ֶ�����FieldType���ŵ���ϣ��
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static System.Collections.Hashtable getTableFieldDescTypeFromSystemName(string systemName, string tableName)
        {
            //ȡ��Ӧ�õ������ļ�����
            string systemConfigFileName = TableConfig.getAppSystemConfigDiskFileName(systemName);
            return TableConfig.getTableFieldDescType(systemConfigFileName, tableName);
        }



        /// <summary>
        /// �ӡ����ݿ⡱�����ļ���ȡ����ı���ֶ��������ֶ�����FieldType���ŵ���ϣ��
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static System.Collections.Hashtable getTableFieldDescTypeFromDataBaseName(string dbName, string tableName)
        {
            //ȡ�����ݿ�������ļ�����
            string dbConfigFileName = TableConfig.getTableConfigDiskFileName(dbName);
            return TableConfig.getTableFieldDescType(dbConfigFileName, tableName);
        }





        /// <summary>
        /// �������ļ���ȡĳ��������ؼ���
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
        /// �������ļ���ȡĳ��������ؼ���
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
        /// �������ļ���ȡĳ��������ؼ���
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
        /// ȡ���е�Ӧ��ϵͳ�б�:���ع�ϣ��(ϵͳ����,���ݿ�����)
        /// </summary>
        /// <param name="onlyGetDB">���Ϊtrue��ֻȡ���ݿ�����Ϊ���ݿ��</param>
        /// <returns></returns>
        public static System.Collections.Hashtable getSystemList(bool onlyGetDB)
        {
            string systemFileName = TableConfig.getSystemDiskFileName();
            if (System.IO.File.Exists(systemFileName) == false)
                return null;
            //ȡ���е����ݿ��б�
            System.Collections.Hashtable dbHashTable = TableConfig.getDatabaseList(onlyGetDB);
            XmlDocument doc = new XmlDocument();
            doc.Load(systemFileName);
            //�������е�Ӧ��ϵͳ
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
        /// ȡ���е�Ӧ��ϵͳ�б�:����SortedList(ϵͳ����,���ݿ�����)
        /// </summary>
        /// <param name="onlyGetDB">���Ϊtrue��ֻȡ���ݿ�����Ϊ���ݿ��</param>
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
        /// ȡ���е����ݿ��б�:
        /// </summary>
        /// <param name="onlyGetDB">���Ϊtrue��ֻȡ���ݿ�����Ϊ���ݿ�� </param>
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
        /// ȡ���е����ݿ��б�:
        /// </summary>
        /// <param name="onlyGetDB">���Ϊtrue��ֻȡ���ݿ�����Ϊ���ݿ�� </param>
        /// <returns></returns>
        public static System.Collections.Hashtable getDatabaseList(bool onlyGetDB)
        {
            //���صĹ�ϣ��
            System.Collections.Hashtable dbHashtable = new Hashtable();
            string dbConfigFileName = TableConfig.getDatabaseDiskFileName();
            if (System.IO.File.Exists(dbConfigFileName) == false)
                return null;
            XmlDocument doc = new XmlDocument();
            doc.Load(dbConfigFileName);
            //�������е����ݿ�
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
        /// ��ȡ���ݿ�������ļ����ڵ�Ŀ¼��
        /// ��D:\WebSite\QMSData\shared\Configuration\ConfigurationData\1
        /// </summary>
        /// <returns></returns>
        public static string getDatabaseDiskPath()
        {
            string path = ConfigurationSettings.AppSettings["SharedDirectory"];
            path += @"\Configuration\ConfigurationData\1";
            return path;
        }


        /// <summary>
        /// ��ȡ���ݿ�������ļ����ƣ�
        /// ��D:\WebSite\QMSData\shared\Configuration\ConfigurationData\1\DatabaseCollection.xml
        /// ����ļ������ڣ�����null
        /// </summary>
        /// <returns>�ļ�����</returns>
        public static string getDatabaseDiskFileName()
        {
            string path = getDatabaseDiskPath();
            path += @"\DatabaseCollection.xml";
            //�ļ��Ƿ����
            if (System.IO.File.Exists(path) == false)
                path = null;
            return path;
        }


        /// <summary>
        /// �������ļ�app.config��ȡ��ȡ����Ŀ¼�����OneTableSystemFilesLocation����ֵ
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
        /// ��ȡ���������ݴ�ŵķ�����Ŀ¼��
        /// ��D:\WebSite\QMSData\AllFiles\1\OneTableSystemFilesLocation\NewsTemp\
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
        /// ��ȡ���������ݴ�ŵķ�����Ŀ¼��
        /// ��D:\WebSite\QMSData\AllFiles\1\OneTableSystemFilesLocation\Temp\
        /// ͬʱ�Զ�����Ŀ¼
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
        /// �������ļ�app.config��ȡ��ȡ����Ŀ¼�����SharedDirectory����ֵ
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
        /// ��ȡӦ��ϵͳ�������ļ����ڵ�Ŀ¼��
        /// ��D:\WebSite\QMSData\shared\Configuration\ConfigurationXml\1
        /// </summary>
        /// <returns></returns>
        public static string getSystemDiskPath()
        {
            string path = getSharePathFromWebConfig();
            path += @"\Configuration\ConfigurationXml\1";
            return path;
        }


        /// <summary>
        /// ��ȡӦ��ϵͳ�������ļ����ڵ�Ŀ¼��
        /// ��D:\WebSite\QMSData\shared\Configuration\ConfigurationXml\1
        /// </summary>
        /// <returns></returns>
        public static string getSystemDiskPathFromSystemName(string systemName)
        {
            string path = getSharePathFromWebConfig();
            path += @"\Configuration\ConfigurationXml\1\" + systemName;
            return path;
        }


        /// <summary>
        /// ��ȡӦ��ϵͳ�������ļ����ƣ�
        /// ��D:\WebSite\QMSData\shared\Configuration\ConfigurationXml\1\DatabaseConfig.xml
        /// ����ļ������ڣ�����null
        /// </summary>
        /// <returns></returns>
        public static string getSystemDiskFileName()
        {
            string path = getSystemDiskPath();
            path += @"\DatabaseConfig.xml";
            //�ļ��Ƿ����
            if (System.IO.File.Exists(path) == false)
                path = null;
            return path;
        }


        /// <summary>
        /// ��ȡӦ��ϵͳ�������ļ����ƣ�
        /// ��D:\WebSite\QMSData\shared\Configuration\ConfigurationXml\1\[Ӧ��ϵͳ����]\TablesConfig.xml
        /// ����ļ������ڣ�����null
        /// </summary>
        /// <returns></returns>
        public static string getSystemConfigDiskFileName(string systemName)
        {
            string path = getSystemDiskPathFromSystemName(systemName);
            path += @"\TablesConfig.xml";
            //�ļ��Ƿ����
            if (System.IO.File.Exists(path) == false)
                path = null;
            return path;
        }



        /// <summary>
        /// ��ȡ�����ֶε������ļ����ƣ�
        /// ��D:\WebSite\QMSData\shared\Configuration\ConfigurationXml\1\Dictionaries.xml
        /// </summary>
        /// <returns></returns>
        public static string getDictionaryDiskFileName()
        {
            string path = getSystemDiskPath();
            path += @"\Dictionaries.xml";
            //�ļ��Ƿ����
            //if (System.IO.File.Exists(path) == false)path = null;
            return path;
        }


        /// <summary>
        /// ȡͳ�������ļ�����(���ļ�Ϊϵͳͨ��ͳ�����)
        /// ��D:\WebSite\QMSData\shared\Configuration\ConfigurationXml\1\Ӧ�ó�������\style.xml
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
        /// ȡͳ�������ļ�����(���ļ�Ϊϵͳͨ��ͳ�����)
        /// ��D:\WebSite\QMSData\shared\Configuration\ConfigurationXml\1\DateTimeFuncType.xml
        /// </summary>
        /// <returns></returns>
        public static string getDateTimeFuncTypeDiskFileName()
        {
            string path = getSystemDiskPath();
            path += @"\DateTimeFuncType.xml";
            return path;
        }


        /// <summary>
        /// ȡͳ�������ļ�����(���ļ�Ϊ��Ӧ��ϵͳ�ĵ���ͳ�����)
        /// ��D:\WebSite\QMSData\shared\Configuration\ConfigurationXml\1\Ӧ��ϵͳ����\TablesConfig.xml
        /// </summary>
        /// <param name="systemName">Ӧ��ϵͳ����</param>
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
        /// ����ϵͳ���ƣ���Ӧ��ϵͳ�������ļ���ȡ��Ӧ�����ݿ�����(ֻ���ص�һ��)
        /// </summary>
        /// <returns></returns>
        public static string getDatabaseNameFromSystemName(string systemName)
        {
            string dbName = "";
            try
            {
                //ȡӦ��ϵͳ�������ļ�
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
        /// �������ݿ����ƣ���Ӧ��ϵͳ�������ļ���ȡ��Ӧ��ϵͳ����(ֻ���ص�һ��)
        /// </summary>
        /// <returns></returns>
        public static string getSystemNameFromDatabaseName(string dbName)
        {
            string systemName = "";
            try
            {
                //ȡӦ��ϵͳ�������ļ�
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
        /// ��ȡ���ݱ�ṹ�������ļ����ڵ�Ŀ¼
        /// ��D:\WebSite\QMSData\shared\Configuration\ConfigurationData\1\���ݿ�����
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
        /// �����ݿ������ļ��У���ȡ��ṹ�������ļ�����
        /// ��D:\WebSite\QMSData\shared\Configuration\ConfigurationData\1\���ݿ�����\TablesConfig.xml
        /// </summary>
        /// <returns>����ļ������ڣ�����null</returns>
        public static string getTableConfigDiskFileName(string dbName)
        {
            string path = getTableDiskPath(dbName);
            path += @"\TablesConfig.xml";
            //�ļ��Ƿ����
            if (System.IO.File.Exists(path) == false)
                path = null;
            return path;
        }



        /// <summary>
        /// ��ȡ�����ݵ��ļ�����
        /// ��D:\WebSite\QMSData\shared\Configuration\ConfigurationData\1\���ݿ�����\TablesData.xml
        /// </summary>
        /// <returns>����ļ������ڣ�����null</returns>
        public static string getTableDataDiskFileName(string dbName)
        {
            string path = getTableDiskPath(dbName);
            path += @"\TablesData.xml";
            //�ļ��Ƿ����
            if (System.IO.File.Exists(path) == false)
                path = null;
            return path;
        }


        /// <summary>
        /// �������ݿ����ƣ������ݿ������ļ��л�ȡ��Ӧ���ݿ�Ĳ�������
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
                    //���ݿ�����Ӳ���
                    System.Collections.Hashtable hashTable = GetXMLData.getXmlNodeNameAndValue(nodelist[0]);
                    tagDataBaseLoginInfo.IP = hashTable["RemoteServer"].ToString();
                    tagDataBaseLoginInfo.DatabaseName = hashTable["InitCatalog"].ToString();
                    tagDataBaseLoginInfo.UserName = hashTable["UserID"].ToString();
                    tagDataBaseLoginInfo.PassWord = hashTable["Password"].ToString();
                    tagDataBaseLoginInfo.TCP = hashTable["RemoteServer"].ToString();
                    tagDataBaseLoginInfo.DefaultTableSpace = hashTable["InitCatalog"].ToString();
                    //���ݿ�����
                    string dbType = hashTable["DatabaseType"].ToString();
                    tagDataBaseLoginInfo.DatabaseType = ConvertData.convertDatabaseType(dbType);
                    //���ݿ����Ӵ�
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
        /// ����Ӧ��ϵͳ�����ƣ�ȡ���ݿ��������Ϣ��
        /// 1������ϵͳ����ȡ���ݿ�����
        /// 2���������ݿ����ƣ������ݿ������ļ��л�ȡ��Ӧ���ݿ�Ĳ�������
        /// </summary>
        /// <returns></returns>
        public static DataBaseLoginInfo getDatabaseInfoFromSystemName(string systemName)
        {
            string dbName = TableConfig.getDatabaseNameFromSystemName(systemName);
            return TableConfig.getDatabaseInfoFromDatabaseName(dbName);
        }


        /// <summary>
        /// ȡһ����������ֶε������ֵ�
        /// </summary>
        /// <param name="xmlConfigFileName">Ӧ��ϵͳ�����ݿ�������ļ�TableConfig.XML</param>
        /// <param name="tableName">������</param>
        /// <returns>ֻ�����������ֵ���ֶ�</returns>
        public static Hashtable getTableFieldDictionary(string xmlConfigFileName, string tableName)
        {
            //�ֶ�����,�ֶ�����
            Hashtable hashTableFieldNameDesc = TableConfig.getFieldTwoAttrFromTableConfigFile(xmlConfigFileName, tableName, "ColumnEName", "ColumnCName");
            Hashtable hashTableFieldNameDictionary = new Hashtable();
            System.Collections.IDictionaryEnumerator myEnumerator = hashTableFieldNameDesc.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                string fieldName = myEnumerator.Key.ToString();
                Queue queueDict = TableConfig.getFieldDictionary(xmlConfigFileName, tableName, fieldName);
                if(queueDict.Count > 0)//�ж���������ֵ�
                    hashTableFieldNameDictionary.Add(fieldName, queueDict);
            }
            return hashTableFieldNameDictionary;
        }






        /// <summary>
        /// �������ļ�tableConfig.xml��ȡ�ֶε������ֵ�
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
                    case "Local"://�����ֵ��
                        queueDictionary = TableConfig.getDictionaryQueueFromName(dataSourceName);
                        break;
                    case "Fixed"://�̶����ݣ�����Ŀ֮���á�:���ָ�
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
        /// ȡ������ֶ��б�
        /// </summary>
        /// <param name="tableConfigInfo"></param>
        /// <param name="tableConfigFileName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static TableConfigInfo getTableConfigInfoFieldList(TableConfigInfo tableConfigInfo, string tableConfigFileName, string tableName, bool isGetDictionary)
        {
            //�������ļ��ж��嵽�ñ�����ֶ�
            string xPath = "//Table[@EName='" + tableName + "']/tr/td";
            XmlDocument doc = new XmlDocument();
            doc.Load(tableConfigFileName);
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            //ȡ�ֶ��б�
            foreach (XmlNode node in nodelist)
            {
                string fieldName = node.Attributes.GetNamedItem("ColumnEName").InnerXml;//�ֶ�����
                string fieldDesc = node.Attributes.GetNamedItem("ColumnCName").InnerXml;//�ֶ�����
                string fieldTypeXML = node.Attributes.GetNamedItem("ColumnType").InnerXml;//�ֶ�����
                //������ת���������Լ������FieldType����
                FieldType fieldType = ConvertData.convertXmlFieldType(fieldTypeXML);
                //�Ǳ��浽��ǰ��
                //�ֶ�����
                if (tableConfigInfo.FieldNameDesc[fieldName] == null)
                    tableConfigInfo.FieldNameDesc.Add(fieldName, fieldDesc);
                //�ֶ�����
                if (tableConfigInfo.FieldNameType[fieldName] == null)
                    tableConfigInfo.FieldNameType.Add(fieldName, fieldType);
                //�ֶ�����
                if (tableConfigInfo.FieldNameValue[fieldName] == null)
                    tableConfigInfo.FieldNameValue.Add(fieldName, null);
                //ȡ�ֶε��ֵ�
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
        /// ȡ������ֶ��б�
        /// </summary>
        /// <param name="tableConfigInfo"></param>
        /// <param name="tableConfigFileName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static System.Collections.Hashtable getSystemTableConfigInputFieldList(string systemName, string tableName)
        {
            Hashtable hashInputField = new Hashtable();
            //ȡӦ��ϵͳ��Ӧ�ı������ļ���
            string systemConfigFileName = TableConfig.getSystemConfigDiskFileName(systemName);
            if (systemConfigFileName == null)
            {
                return hashInputField;
            }
            //�������ļ��ж��嵽�ñ�����ֶ�
            string xPath = "//Table[@EName='" + tableName + "']/tr/td";
            XmlDocument doc = new XmlDocument();
            doc.Load(systemConfigFileName);
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            //ȡ�ֶ��б�
            foreach (XmlNode node in nodelist)
            {
                XmlNodeList nodelistInput = node.SelectNodes("./Input");
                if (nodelistInput.Count != 1)
                    continue;
                string isInput = nodelistInput[0].Attributes.GetNamedItem("DisplayOnInput").InnerXml.ToLower();
                if ((isInput == "yes") || (isInput == "true"))
                {
                    string fieldName = node.Attributes.GetNamedItem("ColumnEName").InnerXml;//�ֶ�����
                    string fieldDesc = node.Attributes.GetNamedItem("ColumnCName").InnerXml;//�ֶ�����
                    if (hashInputField[fieldName] == null)
                        hashInputField.Add(fieldName, fieldDesc);
                }
            }
            return hashInputField;
        }





        /// <summary>
        /// �������ļ��л�ȡһ�������������������ͣ�ʹ�õݹ鷽����
        /// ����Ҫ���ݶ����Ĭ��ֵ����ת����ֻת������Ϊ���Զ����()����
        /// </summary>
        /// <param name="tableNode">�����ļ�����</param>
        /// <param name="foreignKeyTypeTable">��������Ĺ�ϣ��</param>
        /// <returns></returns>
        public static Hashtable getForeignKeyType(XmlNode tableNode, Hashtable foreignKeyTypeTable)
        {
            //��λ������
            XmlNode parentTableNode = tableNode.ParentNode.ParentNode;
            if (parentTableNode != null)
            {
                if (parentTableNode.Name == "Table")
                {
                    //��λ����������ؼ���
                    string xPath = "./tr/td[@KeyIndex='Yes']";
                    XmlNodeList nodelist = parentTableNode.SelectNodes(xPath);
                    if (nodelist.Count == 1)
                    {
                        string fieldName = nodelist[0].Attributes.GetNamedItem("ColumnEName").Value;
                        string fieldTypeXML = nodelist[0].Attributes.GetNamedItem("ColumnType").Value;
                        FieldType fieldType = ConvertData.convertXmlFieldType(fieldTypeXML);
                        //������Ĭ��ֵ
                        string defaultValue = nodelist[0].Attributes.GetNamedItem("ColumnDefault").Value;
                        if (defaultValue == "�Զ����()")
                        {
                            if (fieldType != FieldType.GUID)
                                fieldType = FieldType.IDENTITY;
                        }

                        if (foreignKeyTypeTable[fieldName] == null)//���һ�㲻�ظ�
                            foreignKeyTypeTable.Add(fieldName, fieldType);
                        else //����ظ��������������ƣ���Ĭ�Ϲ������£����������_�������������
                        {
                            string parentTableName = TableConfig.getTableNameFromTableNode(parentTableNode);
                            fieldName = parentTableName + "_" + fieldName;
                            foreignKeyTypeTable.Add(fieldName, fieldType);
                        }
                    }
                    //�ݹ�
                    foreignKeyTypeTable = getForeignKeyType(parentTableNode, foreignKeyTypeTable);
                }
            }
            return foreignKeyTypeTable;
        }



        /// <summary>
        /// �������ļ��л�ȡһ����������������ֵ��ʹ�õݹ鷽����
        /// </summary>
        /// <param name="tableNode">�����ļ�����</param>
        /// <param name="foreignKeyTypeTable">��������Ĺ�ϣ��</param>
        /// <returns></returns>
        public static Hashtable getForeignKeyValueFromParentTableNode(XmlNode tableNode, Hashtable foreignKeyTable, Hashtable foreignKeyRelationTable)
        {
            //��λ������
            XmlNode parentTableNode = TableConfig.getParentTableNode(tableNode);
            if (parentTableNode == null)
                return foreignKeyTable;
            //����¼�Ĺ�������
            XmlNode childTableNode = parentTableNode.FirstChild; ;
            if (childTableNode.Name == "tr")
            {
                //�������е����
                System.Collections.IDictionaryEnumerator myEnumerator = foreignKeyTable.GetEnumerator();
                while (myEnumerator.MoveNext())
                {
                    string fieldName = foreignKeyRelationTable[myEnumerator.Key].ToString();
                    //��λ
                    string xPath = "./td[@Column='" + fieldName + "']";
                    XmlNodeList nodelist = childTableNode.SelectNodes(xPath);
                    if (nodelist.Count == 1)
                    {
                        //�ֶε�����
                        foreignKeyTable[myEnumerator.Key] = nodelist[0].InnerXml;
                        break;
                    }
                }
            }
            //��λ������
            parentTableNode = TableConfig.getParentTableNode(parentTableNode);
            if (parentTableNode != null)
            {
                //�ݹ�
                foreignKeyTable = getForeignKeyValue(parentTableNode, foreignKeyTable);
            }
            return foreignKeyTable;
        }



        /// <summary>
        /// �������ļ��л�ȡһ����������������ֵ��ʹ�õݹ鷽����
        /// </summary>
        /// <param name="tableNode">�����ļ�����</param>
        /// <param name="foreignKeyTypeTable">��������Ĺ�ϣ��</param>
        /// <returns></returns>
        public static Hashtable getForeignKeyValue(XmlNode tableNode, Hashtable foreignKeyTable)
        {
            //����¼�Ĺ������ݴ��ӱ���ȡ
            XmlNode childTableNode = tableNode.FirstChild; ;
            if (childTableNode.Name == "tr")
            {
                //�������е����
                System.Collections.IDictionaryEnumerator myEnumerator = foreignKeyTable.GetEnumerator();
                while (myEnumerator.MoveNext())
                {
                    string fieldName = myEnumerator.Key.ToString();
                    //��λ
                    string xPath = "./td[@Column='" + fieldName + "']";
                    XmlNodeList nodelist = childTableNode.SelectNodes(xPath);
                    if (nodelist.Count == 1)
                    {
                        //�ֶε�����
                        foreignKeyTable[myEnumerator.Key] = nodelist[0].InnerXml;
                        break;
                    }
                }
            }
            //��λ������
            XmlNode parentTableNode = TableConfig.getParentTableNode(tableNode);
            if (parentTableNode != null)
            {
                //�ݹ�
                foreignKeyTable = getForeignKeyValue(parentTableNode, foreignKeyTable);
            }
            return foreignKeyTable;
        }


        /// <summary>
        /// �ӱ�����������ļ���ȡ���������ơ������ֶΡ����
        /// �ӱ�����������ļ���ȡһ�������е������ؼ��ּ������ͣ��������ؼ��ֵ�ֵ��ʼ��Ϊnull
        /// </summary>
        /// <param name="tableConfigInfo"></param>
        /// <param name="dbName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static TableConfigInfo getPrimaryKeyAndForeignKey(TableConfigInfo tableConfigInfo, string dbName, string tableName)
        {
            //ȡ��ṹ�����ļ�����
            string tableRelationFileName = TableRelation.getTableRelationDiskFileName(dbName);
            if (tableRelationFileName == null)//�ļ�������
                return tableConfigInfo;
            //�ȶ�λ����ǰ��������������ֶ�
            string xPath = "//Table[@EName='" + tableName + "']/tr/td";
            XmlDocument doc = new XmlDocument();
            doc.Load(tableRelationFileName);
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            foreach (XmlNode node in nodelist)
            {
                //ȡ�ñ���������
                string fieldName = node.Attributes.GetNamedItem("ColumnEName").InnerXml;
                string fieldTypeXML = node.Attributes.GetNamedItem("ColumnType").InnerXml;
                string fieldRelation = node.Attributes.GetNamedItem("RelationEName").InnerXml;
                FieldType fieldType = ConvertData.convertXmlFieldType(fieldTypeXML);
                if (fieldRelation == "")
                {
                    //�������ֶ�
                    if (tableConfigInfo.PrimaryKey[fieldName] == null)
                    {
                        tableConfigInfo.PrimaryKey.Add(fieldName, null);
                        tableConfigInfo.PrimaryKeyType.Add(fieldName, fieldType);
                    }
                }
                else
                {
                    //�����
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
        /// �������ݿ����ƣ�������ƣ���ȡ�ñ��������Ϣ
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static TableConfigInfo getTableConfigInfoFromDatabaseName(string dbName, string tableName)
        {
            //��ʼ������
            TableConfigInfo tableConfigInfo = DataTag.initTableConfigInfo();
            try
            {
                //ȡ���ݿ��������Ϣ
                tableConfigInfo.dataBaseLoginInfo = TableConfig.getDatabaseInfoFromDatabaseName(dbName);
                //ȡ��ṹ�����ļ�����
                string tableConfigFileName = getTableConfigDiskFileName(dbName);
                if (tableConfigFileName == null)//�ļ�������
                    return tableConfigInfo;
                //ȡ�������
                tableConfigInfo.TableName = tableName;
                //ȡ������ֶΣ��ֶ��������ֶ����͡��ֶ����ݣ���ʼ��Ϊnull�����ֶε��ֵ��
                tableConfigInfo = TableConfig.getTableConfigInfoFieldList(tableConfigInfo, tableConfigFileName, tableConfigInfo.TableName, false);
                //ȡ���е������ֶκ�����Ķ������
                tableConfigInfo = TableConfig.getPrimaryKeyAndForeignKey(tableConfigInfo, dbName, tableName);
                //��������
                tableConfigInfo.PrimaryTable = TableConfig.getParentTableName(tableConfigFileName, tableName);
            }
            catch
            {
                throw;
            }
            return tableConfigInfo;
        }


        /// <summary>
        /// �������ݿ����ƣ�������ƣ���ȡ�ñ��������Ϣ
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static TableConfigInfo getTableConfigInfoAndDictionaryFromDatabaseName(string dbName, string tableName)
        {
            //��ʼ������
            TableConfigInfo tableConfigInfo = DataTag.initTableConfigInfo();
            try
            {
                //ȡ���ݿ��������Ϣ
                tableConfigInfo.dataBaseLoginInfo = TableConfig.getDatabaseInfoFromDatabaseName(dbName);
                //ȡ��ṹ�����ļ�����
                string tableConfigFileName = getTableConfigDiskFileName(dbName);
                if (tableConfigFileName == null)//�ļ�������
                    return tableConfigInfo;
                //ȡ�������
                tableConfigInfo.TableName = tableName;
                //ȡ������ֶΣ��ֶ��������ֶ����͡��ֶ����ݣ���ʼ��Ϊnull�����ֶε��ֵ��
                tableConfigInfo = TableConfig.getTableConfigInfoFieldList(tableConfigInfo, tableConfigFileName, tableConfigInfo.TableName, true);
                //ȡ���е������ֶκ�����Ķ������
                tableConfigInfo = TableConfig.getPrimaryKeyAndForeignKey(tableConfigInfo, dbName, tableName);
                //��������
                tableConfigInfo.PrimaryTable = TableConfig.getParentTableName(tableConfigFileName, tableName);
            }
            catch
            {
                throw;
            }
            return tableConfigInfo;
        }




        /// <summary>
        /// �������ݿ����ƣ�������ƣ���ȡ�ñ��������Ϣ
        /// </summary>
        /// <param name="systemName">ϵͳ����</param>
        /// <param name="tableName">������</param>
        /// <returns></returns>
        public static TableConfigInfo getTableConfigInfoFromSystemName(string systemName, string tableName)
        {
            string dbName = TableConfig.getDatabaseNameFromSystemName(systemName);
            return TableConfig.getTableConfigInfoFromDatabaseName(dbName, tableName);
        }




        /// <summary>
        /// ��TR��㣨һ����¼�������ݱ��浽��ϣ��
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
                //�����ݱ��浽TableConfigInfo��
                if (hashTable[fieldName] == null)
                {
                    //���ֶδ���
                    hashTable.Add(fieldName, fieldValue);
                }
            }
            return hashTable;
        }


        /// <summary>
        /// ȡ��ǰ����ı������
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
        /// ȡ��ǰ��ѯ�ı���ı�����
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
        /// ȡ���ݽ���Ҷ�ӱ��㣺ֻȡ��һ������µ��ӱ�
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
                //�õݹ鷽��ȡҶ�ӽ��
                leafQueryNode = getLeafQueryNode(nodelist[0]);
            }
            return leafQueryNode;
        }




        /// <summary>
        /// ȡ���ݽ���Ҷ�ӱ��㣺ֻȡ��һ������µ��ӱ�
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
                //�õݹ鷽��ȡҶ�ӽ��
                leafTableNode = getLeafTableNode(nodelist[0]);
            }
            return leafTableNode;
        }



        /// <summary>
        /// ȡ����ĸ����㣺��������ڸ����򷵻�null
        /// </summary>
        /// <param name="tableNodeSource"></param>
        /// <returns></returns>
        public static XmlNode getParentTableNode(XmlNode tableNodeSource)
        {
            XmlNode parentTableNode = null;
            if (tableNodeSource.Name != "Table")
                return parentTableNode;
            //�ҵ���ǰ���ж��Ƿ�����������ȡ������Ϣ����
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
                //ȡ��������
                parentTableNode = node;
            }
            return parentTableNode;
        }



        /// <summary>
        /// ����Ӧ��ϵͳ�����ƣ���TABLE���ı����ƣ���ȡ��Ӧ���������Ϣ
        /// ��ȡ�ñ���ĸ��׽������ؼ��֣���Ϊ���ʹ�ã�������������ݱ��浽��ϣ����
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="tableNodeSource"></param>
        /// <returns></returns>
        public static TableConfigInfo getTableConfigInfoAndForeignKeyValue(string systemName, XmlNode tableNodeSource)
        {
            //��ϵͳ����ת�������ݿ�
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
            if (parentTableNode == null)//û�и���
                return hashForeignKeyALL;
            string parentTableName = TableConfig.getTableNameFromTableNode(parentTableNode);
            if (tableNodeSource.Name != "Table")
                return hashForeignKeyALL;
            string tableName = TableConfig.getTableNameFromTableNode(tableNodeSource);
            //ȡ������������б�
            //System.Collections.Hashtable hashParentTablePrimaryKey = TableRelation.getPrimaryKeyFromTableRelation(dbName, parentTableName);
            //ȡ��ǰ�������б�
            System.Collections.Hashtable hashForeignKeyParent = TableRelation.getForeignKeyHashTableFromDatabaseName(dbName, parentTableName);
            //�����ӱ�������ؼ���
            System.Collections.IDictionaryEnumerator myEnumerator = foreignKeyRelationALL.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                //				if(hashParentTablePrimaryKey[myEnumerator.Value] == null)
                //					continue;
                //�����ֶ�
                string fieldNameParent = myEnumerator.Value.ToString();
                //�ӱ��ֶ�
                string fieldNameChild = myEnumerator.Key.ToString();
                XmlNode parentNode = null;
                if (hashForeignKeyParent[fieldNameParent] != null)//˵���ֶ����������游��
                {
                    //��ʵ���ֶ�����
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
                        hashForeignKeyALL[fieldNameChild] = fieldValue;//��������
                        //break;//һ��ֻ��һ�������һ����ֻ��һ����
                    }
                }
            }
            TableConfigInfo tableConfigInfoParent = TableConfig.getTableConfigInfoFromDatabaseName(dbName, parentTableName);
            if (tableConfigInfoParent.PrimaryTable != "")
            {
                //�ݹ�
                hashForeignKeyALL = TableConfig.getForeignKeyValueFromTableNode(dbName, parentTableNode, hashForeignKeyALL, foreignKeyRelationALL);
            }
            return hashForeignKeyALL;
        }



        /// <summary>
        /// ����Ӧ��ϵͳ�����ƣ���TABLE���ı����ƣ���ȡ��Ӧ���������Ϣ
        /// ��ȡ�ñ���ĸ��׽������ؼ��֣���Ϊ���ʹ�ã�������������ݱ��浽��ϣ����
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="tableNodeSource"></param>
        /// <returns></returns>
        public static TableConfigInfo getTableConfigInfoAndForeignKeyValueFromDatabaseName(string dbName, XmlNode leafTableNode)
        {
            //ȡ�����ݿ��������Ϣ
            DataBaseLoginInfo dataBaseLoginInfo = TableConfig.getDatabaseInfoFromDatabaseName(dbName);
            //ȡ��XMLҶ�ӽ��ı����ƣ��ñ���ǲ����ı�
            string tableName = TableConfig.getTableNameFromTableNode(leafTableNode);
            //ȡ���������Ϣ���������������Ϣ��
            TableConfigInfo tableConfigInfo = TableConfig.getTableConfigInfoFromDatabaseName(dbName, tableName);
            //�õݹ�ȡ���������ֵ
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
            //�����ļ��Ƿ����
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
            //���û�ж���,Ĭ��Ϊ��
            if (hashDTFuncTypeNameValue.Count == 0)
                hashDTFuncTypeNameValue.Add("��", "Day");
            return hashDTFuncTypeNameValue;
        }



        /// <summary>
        /// ��XML�л�ȡ�ֵ�������
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static System.Collections.SortedList getDictionaryListFromID(string id)
        {
            string xPath = "//Table[@ID='" + id + "']/tr/td";
            return TableConfig.getDictionaryListFromXPath(xPath);
        }
      

        /// <summary>
        /// ��XML�л�ȡ�ֵ�������
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static System.Collections.SortedList getDictionaryListFromName(string name)
        {
            string xPath = "//Table[@Name='" + name + "']/tr/td";
            return TableConfig.getDictionaryListFromXPath(xPath);
        }


        /// <summary>
        /// ��XML�л�ȡ�ֵ�������
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static System.Collections.Queue getDictionaryQueueFromName(string name)
        {
            string xPath = "//Table[@Name='" + name + "']/tr/td";
            return TableConfig.getDictionaryQueueFromXPath(xPath);
        }


        /// <summary>
        /// ȡĳ���ֵ������,����xPathȡ:xPath = "//Table[@Name='" + name + "']/tr/td";
        /// ��xPath = "//Table[@ID='" + id + "']/tr/td";
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
            //�������е�ͳ��ͼ
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
        /// ���ֵ���������,����ֵ������ļ���û�и��ֵ�,��Ҫ�Զ�����
        /// ��:
        /// D:\WebSite\QMSData\shared\Configuration\ConfigurationXml\1\Dictionaries.xml
        /// </summary>
        /// <param name="dictName">�ֵ������,��:ͳ��ͼ����,��ӦID</param>
        /// <param name="dictDesc">�ֵ����������,��:ͳ��ͼ����,��ӦName</param>
        /// <param name="hashDictData">�ֵ�����������</param>
        public static void saveDictionary(string dictName, string dictDesc, Hashtable hashDictData)
        {
            string tableStatFileName = TableConfig.getDictionaryDiskFileName();
            XmlDocument doc = new XmlDocument();
            //�ж��ֵ������ļ��Ƿ����
            if (System.IO.File.Exists(tableStatFileName) == false)
            {
                //�ļ�������,�򴴽�
                //����XML����������
                XmlDeclaration declarationDoc = doc.CreateXmlDeclaration("1.0", "utf-8", "");
                doc.AppendChild(declarationDoc);
                //����һ����Ԫ��
                XmlElement elementRoot = doc.CreateElement("Tables");
                doc.AppendChild(elementRoot);
                doc.Save(tableStatFileName);
            }
            //�жϸ��ֵ��������Ƿ����,���������,�򴴽�
            doc.Load(tableStatFileName);
            string xPath = "//Table[@ID='" + dictName + "']";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            if (nodelist.Count < 1)
            {
                //��������
                XmlNode tableNode = doc.CreateNode(XmlNodeType.Element, "Table", "");
                //���������
                XmlAttribute attr = doc.CreateAttribute("ID");
                attr.InnerXml = dictName;
                tableNode.Attributes.Append(attr);
                attr = doc.CreateAttribute("Name");
                attr.InnerXml = dictDesc;
                tableNode.Attributes.Append(attr);
                //tr���
                XmlNode trNode = doc.CreateNode(XmlNodeType.Element, "tr", "");
                tableNode.AppendChild(trNode);
                //�ҽӵ����ڵ���
                xPath = "/Tables";
                nodelist = doc.SelectNodes(xPath);
                if (nodelist.Count > 0)
                {
                    //�ӵ��������
                    nodelist[0].AppendChild(tableNode);
                }
                //����
                doc.Save(tableStatFileName);
            }
            //���¶�λ���ñ�
            xPath = "//Table[@ID='" + dictName + "']";
            nodelist = doc.SelectNodes(xPath);
            if (nodelist.Count < 1)
                return;//û�иñ�,����(һ�㲻������������)
            XmlNode nodeTableSave = nodelist[0];
            xPath = "./tr";
            nodelist = nodeTableSave.SelectNodes(xPath);
            if (nodelist.Count < 1)
            {
                //û��tr���,�����
                XmlNode trNode = doc.CreateNode(XmlNodeType.Element, "tr", "");
                nodeTableSave.AppendChild(trNode);
            }
            XmlNode nodeTrSave = nodelist[0];
            //����ԭ�����ֵ��б�
            Hashtable hashDictDataOLD = new Hashtable();
            xPath = "./td";
            XmlNodeList nodelistTD = nodeTrSave.SelectNodes(xPath);
            foreach (XmlNode node in nodelistTD)
            {
                string dictValue = node.InnerXml;
                if (hashDictDataOLD[dictValue] == null)
                    hashDictDataOLD.Add(dictValue, dictValue);
            }
            //�������������ֵ�,���û�и������������
            System.Collections.IDictionaryEnumerator myEnumerator = hashDictData.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                string dictValue = myEnumerator.Key.ToString();
                if (dictValue == "")
                    continue;
                if (hashDictDataOLD[dictValue] == null)
                {
                    //td���
                    XmlNode tdNode = doc.CreateNode(XmlNodeType.Element, "td", "");
                    tdNode.InnerXml = dictValue;
                    nodeTrSave.AppendChild(tdNode);
                }
            }
            //����
            doc.Save(tableStatFileName);
        }


        /// <summary>
        /// ȡĳ���ֵ������,����xPathȡ:xPath = "//Table[@Name='" + name + "']/tr/td";
        /// ��xPath = "//Table[@ID='" + id + "']/tr/td";
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
            //�������е�ͳ��ͼ
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
        /// ����Ӧ��ϵͳ�µ�ĳ����Ľű�
        /// </summary>
        /// <param name="systemConfigFileName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string getParentTableSelectFieldListSQLScript(string systemConfigFileName, string tableName)
        {
            string selectFieldsSQL = "";
            //ȡ��Ӧ�õ������ļ�����
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
        /// �����ݿ�������ļ�,����Ӧ��ϵͳ�µ�ĳ����Ľű�
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string getTableSelectFieldsFromDatabaseDefine(string systemName, string tableName)
        {
            string selectFieldsSQLModal = "[{0}].[{1}] AS [{2}]";
            string selectFieldsSQL = "";
            //ȡ��Ӧ�ö�Ӧ�ĵ����ݿ������ļ�����
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
        /// ��Ӧ��ϵͳ�������ļ�,ȡĳ����������б���ֶ���������ʾ�ĳ���
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getTableFieldDescLengthFromAppSystem(string systemName, string tableName)
        {
            Hashtable hashTable = new Hashtable();
            //ȡ��Ӧ�õ������ļ�����
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
        /// ��Ӧ��ϵͳ�������ļ�,ȡĳ����������б���ֶ���������ʾ�ĳ���
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getTableFieldNameLengthFromAppSystem(string systemName, string tableName)
        {
            Hashtable hashTable = new Hashtable();
            //ȡ��Ӧ�õ������ļ�����
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
        /// ��Ӧ��ϵͳ�������ļ�,ȡĳ����������б���ֶ����ƺʹ�����
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getTableFieldNameGridViewFunctionFromAppSystem(string systemName, string tableName)
        {
            Hashtable hashTable = new Hashtable();
            //ȡ��Ӧ�õ������ļ�����
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
        /// ��Ӧ��ϵͳ�������ļ�,ȡĳ�������ϸ��Ϣ���ֶ����ƺʹ�����
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getTableFieldNameInputFunctionFromAppSystem(string systemName, string tableName)
        {
            Hashtable hashTable = new Hashtable();
            //ȡ��Ӧ�õ������ļ�����
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
                //ת��ΪHTML
                functionName = ConvertData.convertESCToHtml(functionName);
                if (hashTable[fieldName] == null)
                {
                    hashTable.Add(fieldName, functionName);
                }
            }
            return hashTable;
        }



        /// <summary>
        /// ��Ӧ��ϵͳ�������ļ�,ȡĳ����ı���¼����ֶΣ����أ��ֶ����ơ��ֶ�����FieldType
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getNoAllowNullFieldNameTypeFromSystemName(string systemName, string tableName)
        {
            Hashtable hashTable = new Hashtable();
            //ȡ��Ӧ�õ������ļ�����
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
        /// ȡĳ��Ӧ��ϵͳ������(ֻȡ��һ������ı�)������
        /// </summary>
        /// <param name="systemName"></param>
        /// <returns></returns>
        public static string getFirstTableNameFromSystemName(string systemName)
        {
            string tableName = "";
            //ȡ��Ӧ�õ������ļ�����
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
        /// �����ݽṹ�Ķ����ļ���ȡ������ֶε���������ֵ���أ����浽��ϣ��
        /// </summary>
        /// <param name="xmlConfigFileName">�����ļ����ƣ��������ݿ�������ļ���Ӧ��ϵͳ�������ļ�����D:\\WebSite\\QMSData\\Shared\\Configuration\\ConfigurationXml\\1\\ZF��������\\TablesConfig.xml</param>
        /// <param name="tableName">Ҫȡ�ı�ṹ����ı�����</param>
        /// <param name="keyFieldName">��ΪKey������</param>
        /// <param name="valueFieldName">��ΪValue������</param>
        /// <returns></returns>
        public static System.Collections.Hashtable getFieldTwoAttrFromTableConfigFile(string xmlConfigFileName, string tableName, string keyFieldName, string valueFieldName)
        {
            Hashtable hashtable = new Hashtable();
            if ((xmlConfigFileName == "") || (xmlConfigFileName == null))
                return hashtable;
            //�ļ�������
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
                string hashKey = node.Attributes.GetNamedItem(keyFieldName).InnerXml;//����
                string hashValue = node.Attributes.GetNamedItem(valueFieldName).InnerXml;//����
                if (hashtable[hashKey] == null)
                    hashtable.Add(hashKey, hashValue);
            }
            return hashtable;
        }


        /// <summary>
        /// �������ֶε���ʾ��������ֶ�����
        /// </summary>
        /// <param name="hashTableFieldNameDispOrder"></param>
        /// <returns></returns>
        public static Queue getTableFieldDispOrderQueue(string systemName, string tableName)
        {
                //Ӧ��ϵͳ�������ļ������ݽṹ����
                string xmlConfigFileName = TableConfig.getSystemConfigDiskFileName(systemName);
            if ((xmlConfigFileName == "") || (xmlConfigFileName == null))
                return null;
            //�ļ�������
            if (System.IO.File.Exists(xmlConfigFileName) == false)
                return null;
            //ȡϵͳʹ�õ��ֶ�
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
                //û�и��ֶ�
                if (hashSystemSelectFieldName[fieldName] == null)
                    continue;
                //���Ƶ��ֶ�����
                Hashtable hashIsSelectField = (Hashtable)hashSystemSelectFieldName[fieldName];
                if (hashIsSelectField["DisplayOnInput"] != null)
                {
                    //����ʾ
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
        /// ȡĳ������ʾ���ӱ���ֶ�
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
                //�ݹ�:ȡ�丸���ѡ������¼̳��ֶ�
                parentTableName = TableConfig.getParentTableName(systemConfigFileName, parentTableName);
                if ((parentTableName != "") && (parentTableName != null))
                {
                    parentSelectFieldSQL += TableConfig.getParentTableDisplayOnChildFieldFromSystemName(systemConfigFileName, systemName, parentTableName);
                }
            }
            return parentSelectFieldSQL;
        }



        /// <summary>
        /// ��Ӧ��ϵͳ�������ļ�,ȡĳ������ʾ�������б��е��ֶ�
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string getTableSelectFieldsFromAppSystem11(string systemName, string tableName)
        {
            string selectFieldsSQLModal = "[{0}].[{1}] AS [{2}]";
            string selectFieldsSQL = "";
            //ȡ��Ӧ�õ������ļ�����
            string systemConfigFileName = TableConfig.getAppSystemConfigDiskFileName(systemName);
            if (systemConfigFileName == null)
                return null;
            XmlDocument doc = new XmlDocument();
            doc.Load(systemConfigFileName);
            //string xPath = "//Table[@EName='" + tableName + "']/tr/td/Query[@DisplayInChild='Yes']";'
            //ȡ���ؼ���
            string id = TableConfig.getPrimaryKeyName(systemConfigFileName, tableName);
            if ((id != null) && (id != ""))
                selectFieldsSQL += string.Format(selectFieldsSQLModal, tableName, id, id);
            string xPath = "//Table[@EName='" + tableName + "']/tr/td/Query";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            if (nodelist.Count == 0)
            {
                //Ӧ��ϵͳû�����ã������ݿ������ļ���ȡ
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
            //�����ѡ����ֶ�(���¼̳е��ֶ�)
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
        /// ��Ӧ��ϵͳ�������ļ�,ȡĳ������ʾ�������б��е��ֶ�
        /// �����������д��������ֶ�
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string getTableSelectFieldFromSystemName(string systemName, string tableName)
        {
            string selectFieldsSQLModal = "[{0}].[{1}] AS [{2}]";
            string selectFieldsSQL = "";
            //ȡ��Ӧ�õ������ļ�����
            string systemConfigFileName = TableConfig.getAppSystemConfigDiskFileName(systemName);
            if (systemConfigFileName == null)
                return "";
            XmlDocument doc = new XmlDocument();
            doc.Load(systemConfigFileName);
            //string xPath = "//Table[@EName='" + tableName + "']/tr/td/Query[@DisplayInChild='Yes']";'
            //ȡ���ؼ���
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
            //ȡ�丸���ѡ������¼̳��ֶ�
            //string parentSelectFieldSQL = TableConfig.getParentTableDisplayOnChildFieldFromSystemName(systemConfigFileName, systemName, tableName);
            //������
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
                //�����Ѿ��ӹ�
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
        /// �������ļ��У�������Ӧ��ϵͳ�������ļ���Ҳ���������ݿ�������ļ�������ȡ�ֶε���չ���Զ������
        /// ���磺������ѯ���ֶε������
        /// ��������쳣������null
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
                    //ȡ�ֶ�����
                    string fieldName = node.ParentNode.Attributes["ColumnEName"].InnerXml;
                    //ȡ�ֶεĻ��������������
                    Hashtable hashFieldInfo = GetXMLData.getXmlNodeAttributes(node);
                    if (returnValue[fieldName] == null)
                        returnValue.Add(fieldName, hashFieldInfo);
                }
                //�ͷ�
                doc = null;
            }
            catch
            {
                throw;
            }
            return returnValue;
        }


        /// <summary>
        /// ����Ӧ��ϵͳ�������ļ�����ȡ��ѯ�ֶεĶ������
        /// ���ع�ϣ����ʽ��ʽΪ�����ֶ����ƣ��ֶζ��������ϣ��
        /// ���ֶζ��������ϣ����Table/tr/td/InputQuery���������Զ������
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
        /// ����Ӧ��ϵͳ�������ļ�����ȡ�������б��ֶεĶ������
        /// ���ع�ϣ����ʽ��ʽΪ�����ֶ����ƣ��ֶζ��������ϣ��
        /// ���ֶζ��������ϣ����Table/tr/td/Query���������Զ������
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
        /// �Ӳ�ѯ�ֶ���ȡ��������ܡ����ֶ�
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
        /// ����Ӧ��ϵͳ�������ļ�����ȡ�������б��ġ�������ܡ��ֶ�
        /// ���ع�ϣ����ʽ��ʽΪ�����ֶ����ƣ��ֶ����ƣ�
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getFieldParamQueryAllowSumFromSystemName(string systemName, string tableName)
        {
            //Ӧ��ϵͳ�������ʾ���ֶ�
            Hashtable hashSelectField = TableConfig.getFieldParamQueryFromSystemName(systemName, tableName);
            return TableConfig.getFieldParamQueryAllowSumFromQueryFieldHashtable(hashSelectField);
        }


        /// <summary>
        /// ����Ӧ��ϵͳ�������ļ�����ȡ¼���ֶεĶ������
        /// ���ع�ϣ����ʽ��ʽΪ�����ֶ����ƣ��ֶζ��������ϣ��
        /// ���ֶζ��������ϣ����Table/tr/td/Input���������Զ������
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
        /// ����Ӧ��ϵͳ�������ļ�����ȡ¼���ֶε���ʾ������(����)
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
        /// ����Ӧ��ϵͳ�������ļ�����ȡ��ѯ�ֶεĶ������
        /// ���ع�ϣ����ʽ��ʽΪ�����ֶ����ƣ��ֶζ��������ϣ��
        /// ���ֶζ��������ϣ����Table/tr/td/Brief���������Զ������
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
        /// ����Ӧ��ϵͳ�������ļ�����ȡ��ѯ�ֶεĶ������
        /// ���ع�ϣ����ʽ��ʽΪ�����ֶ����ƣ��ֶζ��������ϣ��
        /// ���ֶζ��������ϣ����Table/tr/td/Dictionary���������Զ������
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
        /// �������ݿ�������ļ�����ȡ��ѯ�ֶεĶ������
        /// ���ع�ϣ����ʽ��ʽΪ�����ֶ����ƣ��ֶζ��������ϣ��
        /// ���ֶζ��������ϣ����Table/tr/td/InputQuery���������Զ������
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
        /// �������ݿ�������ļ�����ȡ��ѯ�ֶεĶ������
        /// ���ع�ϣ����ʽ��ʽΪ�����ֶ����ƣ��ֶζ��������ϣ��
        /// ���ֶζ��������ϣ����Table/tr/td/Query���������Զ������
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
        /// �������ݿ�������ļ�����ȡ��ѯ�ֶεĶ������
        /// ���ع�ϣ����ʽ��ʽΪ�����ֶ����ƣ��ֶζ��������ϣ��
        /// ���ֶζ��������ϣ����Table/tr/td/Input���������Զ������
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
        /// �������ݿ�������ļ�����ȡ��ѯ�ֶεĶ������
        /// ���ع�ϣ����ʽ��ʽΪ�����ֶ����ƣ��ֶζ��������ϣ��
        /// ���ֶζ��������ϣ����Table/tr/td/Brief���������Զ������
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
        /// �������ݿ�������ļ�����ȡ��ѯ�ֶεĶ������
        /// ���ع�ϣ����ʽ��ʽΪ�����ֶ����ƣ��ֶζ��������ϣ��
        /// ���ֶζ��������ϣ����Table/tr/td/Dictionary���������Զ������
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
        /// �������ݿ�������ļ�����ȡ��������ֶεĶ������
        /// ���ع�ϣ����ʽ��ʽΪ�����ֶ����ƣ��ֶζ��������ϣ��
        /// ���ֶζ��������ϣ����Table/tr/td/Browse���������Զ������
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
        /// �������ݿ�������ļ�����ȡ����ͼ���ֶεĶ������
        /// ���ع�ϣ����ʽ��ʽΪ�����ֶ����ƣ��ֶζ��������ϣ��
        /// ���ֶζ��������ϣ����Table/tr/td/View���������Զ������
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
        /// ���ݡ�Ӧ��ϵͳ���������ļ�����ȡ����ͼ���ֶεĶ������
        /// ���ع�ϣ����ʽ��ʽΪ�����ֶ����ƣ��ֶζ��������ϣ��
        /// ���ֶζ��������ϣ����Table/tr/td/View���������Զ������
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
        /// �������ļ��У�������Ӧ��ϵͳ�������ļ���Ҳ���������ݿ�������ļ�������ȡ�����ֶεĻ������Զ������
        /// ���ص����ݸ�ʽ�����ֶ����ƣ����ֶε����л������ԵĶ��������
        /// ע�⣺��������չ���ԵĶ������
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
                    //ȡ�ֶεĻ��������������
                    Hashtable hashFieldInfo = GetXMLData.getXmlNodeAttributes(node);
                    if (hashFieldInfo["ColumnEName"] == null)
                        continue;
                    string fieldName = hashFieldInfo["ColumnEName"].ToString();
                    if (returnValue[fieldName] == null)
                        returnValue.Add(fieldName, hashFieldInfo);
                }
                //�ͷ�
                doc = null;
            }
            catch
            {
                throw;
            }
            return returnValue;
        }



        /// <summary>
        /// ����Ӧ��ϵͳ�������ļ�����ȡ���л����ֶεĶ������
        /// ���ع�ϣ����ʽ��ʽΪ�����ֶ����ƣ������ֶεĻ������Զ��������ϣ��
        /// ע�⣺��������չ���ԵĶ������
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getFieldAttrAllFromSystemName(string systemName, string tableName)
        {
            //ȡ��Ӧ�õ������ļ�����
            string systemConfigFileName = TableConfig.getAppSystemConfigDiskFileName(systemName);
            string xPath = "//Table[@EName='" + tableName + "']/tr/td";
            return TableConfig.getTableConfigFieldAttrFromXPath(systemConfigFileName, xPath);
        }



        /// <summary>
        /// �������ݿ�������ļ�����ȡ���л����ֶεĶ������
        /// ���ع�ϣ����ʽ��ʽΪ�����ֶ����ƣ������ֶεĻ������Զ��������ϣ��
        /// ע�⣺��������չ���ԵĶ������
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getFieldAttrAllFromDatabaseName(string dbName, string tableName)
        {
            //ȡ�����ݿ�������ļ�����
            string dbConfigFileName = TableConfig.getTableConfigDiskFileName(dbName);
            string xPath = "//Table[@EName='" + tableName + "']/tr/td";
            return TableConfig.getTableConfigFieldAttrFromXPath(dbConfigFileName, xPath);
        }



        /// <summary>
        /// �������ļ��У�������Ӧ��ϵͳ�������ļ���Ҳ���������ݿ�������ļ�������ȡĳ����Ļ������Զ������
        /// ���ص����ݸ�ʽ���������ƣ��ñ�����л������ԵĶ��������
        /// ����ֻ��Ψһ��һ��������ŷ��������
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
                //�ͷ�
                doc = null;
            }
            catch
            {
                throw;
            }
            return returnValue;
        }



        /// <summary>
        /// ��Ӧ��ϵͳ�������ļ��У�ȡһ��������ж�������ԣ������������ع�ϣ���������ƣ�����ֵ���������ַ���
        /// ����ֻ��Ψһ��һ��������ŷ��������
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getTableAttrAllFromSystemName(string systemName, string tableName)
        {
            //ȡ��Ӧ�õ������ļ�����
            string systemConfigFileName = TableConfig.getAppSystemConfigDiskFileName(systemName);
            return TableConfig.getTableConfigTableAttrFromConfigFileName(systemConfigFileName, tableName);
        }



        /// <summary>
        /// �����ݿ�������ļ��У�ȡһ��������ж�������ԣ������������ع�ϣ���������ƣ�����ֵ���������ַ���
        /// ����ֻ��Ψһ��һ��������ŷ��������
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getTableAttrAllFromDatabaseName(string dbName, string tableName)
        {
            //ȡ�����ݿ�������ļ�����
            string dbConfigFileName = TableConfig.getTableConfigDiskFileName(dbName);
            return TableConfig.getTableConfigTableAttrFromConfigFileName(dbConfigFileName, tableName);
        }




        /// <summary>
        /// �������ļ��У�������Ӧ��ϵͳ�������ļ���Ҳ���������ݿ�������ļ�������ȡĳ����������ӱ�
        /// ���ص����ݸ�ʽ��ϣ��Hashtable�����ӱ����ƣ��ӱ�������
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
                //�ͷ�
                doc = null;
            }
            catch
            {
                throw;
            }
            return returnValue;
        }




        /// <summary>
        /// ��Ӧ��ϵͳ�������ļ�����ȡĳ����������ӱ�
        /// ���ص����ݸ�ʽ��ϣ��Hashtable�����ӱ����ƣ��ӱ�������
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getChildTableFromSystemName(string systemName, string tableName)
        {
            //ȡ��Ӧ�õ������ļ�����
            string systemConfigFileName = TableConfig.getAppSystemConfigDiskFileName(systemName);
            return TableConfig.getChildTableFromConfigFileName(systemConfigFileName, tableName);
        }



        /// <summary>
        /// �����ݿ�������ļ�����ȡĳ����������ӱ�
        /// ���ص����ݸ�ʽ��ϣ��Hashtable�����ӱ����ƣ��ӱ�������
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getChildTableFromDatabaseName(string dbName, string tableName)
        {
            //ȡ��Ӧ�õ������ļ�����
            string dbConfigFileName = TableConfig.getTableConfigDiskFileName(dbName);
            return TableConfig.getChildTableFromConfigFileName(dbConfigFileName, tableName);
        }


        /// <summary>
        /// �������ļ��У�������Ӧ��ϵͳ�������ļ���Ҳ���������ݿ�������ļ�������ȡĳ����������ӱ�
        /// ���ص����ݸ�ʽ����Queue�����ӱ����ƣ�
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
                //�ͷ�
                doc = null;
            }
            catch
            {
                throw;
            }
            return returnValue;
        }



        /// <summary>
        /// ��Ӧ��ϵͳ�������ļ�����ȡĳ����������ӱ�
        /// ���ص����ݸ�ʽ����Queue�����ӱ����ƣ�
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Queue getChildTableQueueFromSystemName(string systemName, string tableName)
        {
            //ȡ��Ӧ�õ������ļ�����
            string systemConfigFileName = TableConfig.getAppSystemConfigDiskFileName(systemName);
            return TableConfig.getChildTableQueueFromConfigFileName(systemConfigFileName, tableName);
        }



        /// <summary>
        /// �����ݿ�������ļ�����ȡĳ����������ӱ�
        /// ���ص����ݸ�ʽ����Queue�����ӱ����ƣ�
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Queue getChildTableQueueFromDatabaseName(string dbName, string tableName)
        {
            //ȡ�����ݿ�������ļ�����
            string dbConfigFileName = TableConfig.getTableConfigDiskFileName(dbName);
            return TableConfig.getChildTableQueueFromConfigFileName(dbConfigFileName, tableName);
        }



        /// <summary>
        /// ���ݱ�����ƣ���Ӧ��ϵͳ�����ļ���ȡ���Ӧ�ı���������
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string getAppSystemTableDescFromTableName(string systemName, string tableName)
        {
            string returnValue = "";
            //ȡ��Ӧ�õ������ļ�����
            string systemConfigFileName = TableConfig.getAppSystemConfigDiskFileName(systemName);
            if (systemConfigFileName == null)
                return returnValue;
            //���ݱ���������ơ���λ���ñ�
            string xPath = "//Table[@EName='" + tableName + "']";
            XmlDocument doc = new XmlDocument();
            doc.Load(systemConfigFileName);
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            if (nodelist.Count == 1)
            {
                XmlNode node = nodelist[0];
                if (node.Name == "Table")
                {
                    //ȡ��������
                    returnValue = node.Attributes.GetNamedItem("TableCName").InnerXml;
                }
            }
            return returnValue;
        }



        /// <summary>
        /// ����Ӧ��ϵͳ���ƣ�ȡ���������(��δʵ��)
        /// </summary>
        /// <param name="systemName"></param>
        /// <returns></returns>
        public static string getHelpContentFromSystemName(string systemName, string tableName)
        {
            string returnValue = "";
            //ȡ��Ӧ�õ������ļ�����
            string systemConfigFileName = TableConfig.getAppSystemConfigDiskFileName(systemName);
            if (systemConfigFileName == null)
                return returnValue;
            //���ݱ���������ơ���λ���ñ�
            string xPath = "//Table[@EName='" + tableName + "']";
            XmlDocument doc = new XmlDocument();
            doc.Load(systemConfigFileName);
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            if (nodelist.Count == 1)
            {
                XmlNode node = nodelist[0];
                if (node.Name == "HelpDoc")
                {
                    //ȡ��������
                    returnValue = node.Attributes.GetNamedItem("HelpContent").InnerXml;
                }
            }
            return returnValue;
        }


        /// <summary>
        /// �������ݿ����ƣ������ݿ������ļ���ȡ����ֶ��б�
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
        /// ����Ӧ��ϵͳ���ƣ������ݿ������ļ���ȡ����ֶ��б�
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
        /// �����ֶεĻ�����Ϣ������һ���ֶ�XML���
        /// ����һ���ֶε�XmlNode���
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="hashFieldInfo">���ݿ��ﶨ����ֶ���Ϣ��ϣ��</param>
        /// <returns></returns>
        public static XmlNode createFieldXmlNode(XmlDocument xmlDoc, Hashtable hashFieldInfo)
        {
            if (hashFieldInfo["�ֶ�����"] == null)
            {
                return null;
            }
            //�ֶ�����
            string fieldName = hashFieldInfo["�ֶ�����"].ToString();
            //�ֶ�����
            string fieldDesc = "";
            if (hashFieldInfo["�ֶ�˵��"] != null)
            {
                fieldDesc = hashFieldInfo["�ֶ�˵��"].ToString();
            }
            //���û���������ƣ���Ĭ��Ϊ�ֶ�����
            if (fieldDesc == "")
                fieldDesc = fieldName;
            //�ֶ�����
            string fieldTypeXml = "-1";
            string fieldType = "";
            if (hashFieldInfo["�ֶ�����"] != null)
            {
                fieldType = hashFieldInfo["�ֶ�����"].ToString();
                fieldTypeXml = ConvertData.convertSQLServerFieldTypeToXML(fieldType.ToLower());
            }
            //�����
            string allowNull = "Yes";
            if (hashFieldInfo["�����"] != null)
            {
                if (hashFieldInfo["�����"].ToString() == "��")
                    allowNull = "Yes";
                else
                    allowNull = "No";
            }
            //Ĭ��ֵ
            string defaultValue = "";
            if (hashFieldInfo["Ĭ��ֵ"] != null)
            {
                defaultValue = hashFieldInfo["Ĭ��ֵ"].ToString();
            }
            //С��λ��
            string precision = "0";
            if (hashFieldInfo["С��λ��"] != null)
            {
                precision = hashFieldInfo["С��λ��"].ToString();
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

            //==================�����ӵ�Index����===========
            xmlAttr = xmlDoc.CreateAttribute("Index");
            xmlAttr.Value = "No";
            xmlNewNode.Attributes.Append(xmlAttr);

            //==============�Ƿ�����Ϊ��==============
            xmlAttr = xmlDoc.CreateAttribute("AllowNull");
            xmlAttr.Value = allowNull;
            xmlNewNode.Attributes.Append(xmlAttr);
            return xmlNewNode;
        }



        /// <summary>
        /// �ж�һ��Ӧ��ϵͳ,�Ƿ�ͬ��¼������
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static bool isInputDataWithDetailInfo(string systemName, string tableName)
        {
            bool isWithDetailData = false;//�Ƿ�ͬ��¼��
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
        /// �������ļ���(style.xml),��ȡӦ�ó������ʽ�ļ�
        /// �������ʽ�ļ�������D:\WebSiteOA\DBQuery\css\Style\
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
