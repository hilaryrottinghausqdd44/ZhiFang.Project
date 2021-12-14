using System;
using System.Xml;
using System.Collections;

namespace ZhiFang.WebLisService.clsCommon
{
	/// <summary>
	/// TableRelation 的摘要说明。
	/// </summary>
	public class TableRelation
	{
		public TableRelation()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}



        /// <summary>
        /// 获取数据库的表之间关联的配置文件名称
        /// 本方法不判断文件是否存在！！！直接将文件名称返回
        /// 如D:\WebSite\QMSData\shared\Configuration\ConfigurationData\1\数据库名称\TablesRelation.xml
        /// </summary>
        /// <returns>文件名称</returns>
        public static string getTableRelationDiskFileName(string dbName)
        {
            string path = TableConfig.getTableDiskPath(dbName);
            path += @"\TablesRelation.xml";
            //文件是否存在
            //if(System.IO.File.Exists(path) == false) path = null;
            return path;
        }



		public static XmlDocument makeTableRelationForeignKey(XmlNode parentTableNode, string xPath, XmlDocument docRelation)
		{
			if(parentTableNode.Name != "Table")
				return docRelation;
			//取到主表名称
			string parentTableName = TableConfig.getTableNameFromTableNode(parentTableNode);
			//遍历当前表下的子表
			string xPathChildTable = "/tr/Table";
			xPath = xPath + "[@EName='" + parentTableName + "']" + xPathChildTable;
			XmlNodeList nodelist = docRelation.SelectNodes(xPath);
			foreach(XmlNode node in nodelist)
			{
				//遍历主表的主键
				string xPathPK = "./tr/td";
				XmlNodeList nodelistPK = parentTableNode.SelectNodes(xPathPK);
				foreach(XmlNode nodePK in nodelistPK)
				{
					string fieldName = nodePK.Attributes.GetNamedItem("ColumnEName").InnerXml;
					string fieldType = nodePK.Attributes.GetNamedItem("ColumnType").InnerXml;
					XmlNode nodeTD = docRelation.CreateNode(XmlNodeType.Element, "td", "");
					//加字段名称
					XmlAttribute attr = docRelation.CreateAttribute("ColumnEName");
					attr.InnerXml = parentTableName + "_" + fieldName;//外键名称,默认在前加表的名称
					nodeTD.Attributes.Append(attr);
					//加字段类型
					attr = docRelation.CreateAttribute("ColumnType");
					attr.InnerXml = fieldType;
					nodeTD.Attributes.Append(attr);
					//加关联的字段名称
					attr = docRelation.CreateAttribute("RelationEName");
					attr.InnerXml = fieldName;//关联的字段名称
					nodeTD.Attributes.Append(attr);
					//定位到插入的外键的位置
					XmlNodeList nodelistFK = node.SelectNodes("./tr");
					if(nodelistFK.Count == 1)
						nodelistFK[0].AppendChild(nodeTD);
				}
				//递归取到主键
				docRelation = makeTableRelationForeignKey(node, xPath, docRelation);
			}
			return docRelation;
		}





		/// <summary>
		/// 根据数据库名称创建表与表的关联关系
		/// </summary>
		/// <param name="dbName">数据库名称</param>
		/// <returns></returns>
		public static string makeTableRelationFromTablesConfig(string dbName)
		{
			//取该数据库配置文件
			string configFileName = TableConfig.getTableConfigDiskFileName(dbName);
			//取该数据库的表关联配置文件
            string relationFileName = TableRelation.getTableRelationDiskFileName(dbName);
			//重新创建新的表关联文件
			XmlDocument docRelation = new XmlDocument();
			//加入XML的声明段落
            XmlDeclaration declarationRelation = docRelation.CreateXmlDeclaration("1.0", "utf-8", "");
			docRelation.AppendChild(declarationRelation);
			//创建一个根元素
			XmlElement elementRoot = docRelation.CreateElement("Tables");            
			XmlDocument doc = new XmlDocument();
			doc.Load(configFileName);
			string xPath = "/Tables/Table";
			XmlNodeList nodelist = doc.SelectNodes(xPath);
			//加表及其主键字段的定义
			foreach(XmlNode node in nodelist)
			{
				//加根元素
				docRelation.AppendChild(makeTableRelationFromTableNode(elementRoot, node, docRelation));
			}
			//遍历生成的主键,设置各个表的外键:要一层一层的往下设置,这样设置才正确
			xPath = "/Tables/Table";
			nodelist = docRelation.SelectNodes(xPath);
			foreach(XmlNode node in nodelist)
			{
				//取到主键
				docRelation = makeTableRelationForeignKey(node, xPath, docRelation);
			}
			docRelation.Save(relationFileName);
			return docRelation.OuterXml;
		}


		public static XmlNode makeTableRelationFromTableNode(XmlNode elementParent, XmlNode tableNode, XmlDocument docRelation)
		{
			//找当前表结点下的子表
			if(tableNode.Name != "Table")
				return elementParent;
			//创建XML关联结点
			XmlNode nodeRelation = docRelation.CreateNode(XmlNodeType.Element,tableNode.Name, "");
			string tableName = tableNode.Attributes.GetNamedItem("EName").InnerXml;
			XmlAttribute attr = docRelation.CreateAttribute("EName");
			attr.InnerXml = tableName;
			nodeRelation.Attributes.Append(attr);
			//加tr
			XmlNode nodeTR = docRelation.CreateNode(XmlNodeType.Element, "tr", "");
			//找该表的主键
			string xPath = "./tr/td[@KeyIndex='Yes']";
			XmlNodeList nodelist = tableNode.SelectNodes(xPath);
			foreach(XmlNode node in nodelist)
			{
				string fieldName = node.Attributes.GetNamedItem("ColumnEName").InnerXml;
				string fieldTypeXML = node.Attributes.GetNamedItem("ColumnType").InnerXml;
				string fieldDefault = node.Attributes.GetNamedItem("ColumnDefault").InnerXml;
				XmlNode nodeTD = docRelation.CreateNode(XmlNodeType.Element, "td", "");
				//加字段名称
				attr = docRelation.CreateAttribute("ColumnEName");
				attr.InnerXml = fieldName;
				nodeTD.Attributes.Append(attr);
				//加字段类型(加之前要进行转换)
				attr = docRelation.CreateAttribute("ColumnType");
                FieldType fieldType = ConvertData.convertXmlFieldType(fieldTypeXML);
				if(fieldDefault == "自动编号()")
				{
					if(fieldType != FieldType.GUID)
						fieldType = FieldType.IDENTITY;
				}
				attr.InnerXml = fieldType.ToString();
				nodeTD.Attributes.Append(attr);
				//加关联的字段名称
				attr = docRelation.CreateAttribute("RelationEName");
				attr.InnerXml = "";//定义的主键字段没有关联字段
				nodeTD.Attributes.Append(attr);
				//加表的主键定义
				nodeTR.AppendChild(nodeTD);
			}
			//加该表的子表
			xPath = "./tr/Table";
			XmlNodeList nodelistChildTable = tableNode.SelectNodes(xPath);
			foreach(XmlNode childTableNode in nodelistChildTable)
			{
				//用递归方法取子表
				nodeTR = makeTableRelationFromTableNode(nodeTR, childTableNode, docRelation);
			}
			nodeRelation.AppendChild(nodeTR);
			//加表属性
			elementParent.AppendChild(nodeRelation);
			return elementParent;
		}




		/// <summary>
		/// 从关联表中取一个表的主关键字，返回：（主关键字段名称，字段类型），都是字符型
        /// 返回哈希表
		/// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <param name="tableName">表名称</param>
        /// <returns>如果配置文件不存在，返回null</returns>
        public static System.Collections.Hashtable getPrimaryKeyHashTableFromDatabaseName(string dbName, string tableName)
		{
			//取表的关联关系,从关联的配置文件里取
            string relationFileName = TableRelation.getTableRelationDiskFileName(dbName);
			if(System.IO.File.Exists(relationFileName) == false)//配置文件不存在
				return null;
            Hashtable returnValue = new Hashtable();
            //找所有的外键
			string xPath = "//Table[@EName='" + tableName + "']/tr/td";//[@RelationEName='']
			XmlDocument doc = new XmlDocument();
			doc.Load(relationFileName);
			XmlNodeList nodelist = doc.SelectNodes(xPath);
			foreach(XmlNode node in nodelist)
			{
                string fieldName = node.Attributes.GetNamedItem("ColumnEName").InnerXml;
                string fieldTypeXML = node.Attributes.GetNamedItem("ColumnType").InnerXml;
				if(returnValue[fieldName] == null)
                    returnValue.Add(fieldName, fieldTypeXML);
			}
			return returnValue;
		}


        /// <summary>
        /// 从关联表中取一个表的主关键字，返回：（主关键字段名称，字段类型），都是字符型
        /// 返回哈希表
        /// </summary>
        /// <param name="systemName">应用系统名称</param>
        /// <param name="tableName">表名称</param>
        /// <returns>如果配置文件不存在，返回null</returns>
        public static System.Collections.Hashtable getPrimaryKeyHashTableFromSystemName(string systemName, string tableName)
        {
            //取表的关联关系,从关联的配置文件里取
            string dbName = TableConfig.getDatabaseNameFromSystemName(systemName);
            return TableRelation.getPrimaryKeyHashTableFromDatabaseName(dbName, tableName);
        }



        /// <summary>
        /// 从表关联的配置文件中取表的主关键字：表的外键 + 主键
        /// 返回堆栈
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <param name="tableName">表名称</param>
        /// <returns>如果配置文件不存在，返回null</returns>
        public static Stack getPrimaryKeyStackFromDatabaseName(string dbName, string tableName)
        {
            //取表结构配置文件名称
            string tableRelationFileName = TableRelation.getTableRelationDiskFileName(dbName);
            if (tableRelationFileName == null)//文件不存在
                return null;
            //先定位到当前表定义的主键构成字段
            string xPath = "//Table[@EName='" + tableName + "']/tr/td";
            XmlDocument doc = new XmlDocument();
            doc.Load(tableRelationFileName);
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            Stack returnValue = new Stack();
            foreach (XmlNode node in nodelist)
            {
                //取该表的主键构成
                string fieldName = node.Attributes.GetNamedItem("ColumnEName").InnerXml;
                returnValue.Push(fieldName);
            }
            return returnValue;
        }


        /// <summary>
        /// 从表关联的配置文件中取表的主关键字：表的外键 + 主键
        /// 返回堆栈
        /// </summary>
        /// <param name="systemName">应用系统名称</param>
        /// <param name="tableName">表名称</param>
        /// <returns>如果配置文件不存在，返回null</returns>
        public static Stack getPrimaryKeyStackFromSystemName(string systemName, string tableName)
        {
            string dbName = TableConfig.getDatabaseNameFromSystemName(systemName);
            return TableRelation.getPrimaryKeyStackFromDatabaseName(dbName, tableName);
        }



        /// <summary>
        /// 从表关联的配置文件中取表的主关键字：表的外键 + 主键
        /// 返回数组ArrayList
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <param name="tableName">表名称</param>
        /// <returns>如果配置文件不存在，返回null</returns>
        public static ArrayList getPrimaryKeyArrayListFromDatabaseName(string dbName, string tableName)
        {
            //取表结构配置文件名称
            string tableRelationFileName = TableRelation.getTableRelationDiskFileName(dbName);
            if (tableRelationFileName == null)//文件不存在
                return null;
            //先定位到当前表定义的主键构成字段
            string xPath = "//Table[@EName='" + tableName + "']/tr/td";
            XmlDocument doc = new XmlDocument();
            doc.Load(tableRelationFileName);
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            ArrayList returnValue = new ArrayList();
            foreach (XmlNode node in nodelist)
            {
                //取该表的主键构成
                string fieldName = node.Attributes.GetNamedItem("ColumnEName").InnerXml;
                returnValue.Add(fieldName);
            }
            return returnValue;
        }


        /// <summary>
        /// 从表关联的配置文件中取表的主关键字：表的外键 + 主键
        /// 返回字符数组string[]
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <param name="tableName">表名称</param>
        /// <returns>如果配置文件不存在，返回null</returns>
        public static string[] getPrimaryKeyArrayFromDatabaseName(string dbName, string tableName)
        {
            //取表结构配置文件名称
            string tableRelationFileName = TableRelation.getTableRelationDiskFileName(dbName);
            if (tableRelationFileName == null)//文件不存在
                return null;
            //先定位到当前表定义的主键构成字段
            string xPath = "//Table[@EName='" + tableName + "']/tr/td";
            XmlDocument doc = new XmlDocument();
            doc.Load(tableRelationFileName);
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            string[] returnValue = new string[nodelist.Count];
            int i = 0;
            foreach (XmlNode node in nodelist)
            {
                //取该表的主键构成
                string fieldName = node.Attributes.GetNamedItem("ColumnEName").InnerXml;
                returnValue[i++] = fieldName;
            }
            return returnValue;
        }




        /// <summary>
        /// 本局数据库名称，从关联配置文件中中取一个表的外来关键字
        /// 返回哈希表(字段名称，关联的主表的字段名称)
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <param name="tableName">表名称</param>
        /// <returns>如果配置文件不存在，返回null</returns>
        public static System.Collections.Hashtable getForeignKeyHashTableFromDatabaseName(string dbName, string tableName)
        {
            //取表的关联关系,从关联的配置文件里取
            string relationFileName = TableRelation.getTableRelationDiskFileName(dbName);
            if (System.IO.File.Exists(relationFileName) == false)//配置文件不存在
                return null;
            Hashtable returnValue = new Hashtable();
            //找所有的外键
            string xPath = "//Table[@EName='" + tableName + "']/tr/td";
            XmlDocument doc = new XmlDocument();
            doc.Load(relationFileName);
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            foreach (XmlNode node in nodelist)
            {
                string fieldName = node.Attributes.GetNamedItem("ColumnEName").InnerXml;
                string relationFieldName = node.Attributes.GetNamedItem("RelationEName").InnerXml;
                if (relationFieldName == "")
                    continue;
                if (returnValue[fieldName] == null)
                    returnValue.Add(fieldName, relationFieldName);
            }
            return returnValue;
        }



        /// <summary>
        /// 本局数据库名称，从关联配置文件中中取一个表的外来关键字
        /// 返回哈希表(字段名称，字段类型枚举类型Enum，即字符串)
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <param name="tableName">表名称</param>
        /// <returns>如果配置文件不存在，返回null</returns>
        public static System.Collections.Hashtable getForeignKeyNameTypeEnumHashTableFromDatabaseName(string dbName, string tableName)
        {
            //取表的关联关系,从关联的配置文件里取
            string relationFileName = TableRelation.getTableRelationDiskFileName(dbName);
            if (System.IO.File.Exists(relationFileName) == false)//配置文件不存在
                return null;
            Hashtable returnValue = new Hashtable();
            //找所有的外键
            string xPath = "//Table[@EName='" + tableName + "']/tr/td";
            XmlDocument doc = new XmlDocument();
            doc.Load(relationFileName);
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            foreach (XmlNode node in nodelist)
            {
                string fieldName = node.Attributes.GetNamedItem("ColumnEName").InnerXml;
                string relationFieldName = node.Attributes.GetNamedItem("RelationEName").InnerXml;
                if (relationFieldName == "")
                    continue;
                string fieldType = node.Attributes.GetNamedItem("ColumnType").InnerXml;
                if (returnValue[fieldName] == null)
                    returnValue.Add(fieldName, fieldType);
            }
            return returnValue;
        }



        /// <summary>
        /// 本局数据库名称，从关联配置文件中中取一个表的外来关键字
        /// 返回哈希表(字段名称，字段类型，即FieldType类型)
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <param name="tableName">表名称</param>
        /// <returns>如果配置文件不存在，返回null</returns>
        public static System.Collections.Hashtable getForeignKeyNameTypeHashTableFromDatabaseName(string dbName, string tableName)
        {
            //取表的关联关系,从关联的配置文件里取
            string relationFileName = TableRelation.getTableRelationDiskFileName(dbName);
            if (System.IO.File.Exists(relationFileName) == false)//配置文件不存在
                return null;
            Hashtable returnValue = new Hashtable();
            //找所有的外键
            string xPath = "//Table[@EName='" + tableName + "']/tr/td";
            XmlDocument doc = new XmlDocument();
            doc.Load(relationFileName);
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            foreach (XmlNode node in nodelist)
            {
                string fieldName = node.Attributes.GetNamedItem("ColumnEName").InnerXml;
                string relationFieldName = node.Attributes.GetNamedItem("RelationEName").InnerXml;
                if (relationFieldName == "")
                    continue;
                string fieldTypeEnum = node.Attributes.GetNamedItem("ColumnType").InnerXml;
                FieldType fieldType = ConvertData.convertToFieldType(fieldTypeEnum);
                if (returnValue[fieldName] == null)
                    returnValue.Add(fieldName, fieldType);
            }
            return returnValue;
        }



        /// <summary>
        /// 本局数据库名称，从关联配置文件中中取一个表的外来关键字
        /// 返回堆栈
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <param name="tableName">表名称</param>
        /// <returns>如果配置文件不存在，返回null</returns>
        public static System.Collections.Stack getForeignKeyStackFromDatabaseName(string dbName, string tableName)
        {
            //取表的关联关系,从关联的配置文件里取
            string relationFileName = TableRelation.getTableRelationDiskFileName(dbName);
            if (System.IO.File.Exists(relationFileName) == false)//配置文件不存在
                return null;
            Stack returnValue = new Stack();
            //找所有的外键
            string xPath = "//Table[@EName='" + tableName + "']/tr/td";
            XmlDocument doc = new XmlDocument();
            doc.Load(relationFileName);
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            foreach (XmlNode node in nodelist)
            {
                string fieldName = node.Attributes.GetNamedItem("ColumnEName").InnerXml;
                string relationFieldName = node.Attributes.GetNamedItem("RelationEName").InnerXml;
                if (relationFieldName == "")
                    continue;
                returnValue.Push(fieldName);
            }
            return returnValue;
        }





        /// <summary>
        /// 本局数据库名称，从关联配置文件中中取一个表的外来关键字
        /// 返回数组ArrayList
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <param name="tableName">表名称</param>
        /// <returns>如果配置文件不存在，返回null</returns>
        public static System.Collections.ArrayList getForeignKeyArrayListFromDatabaseName(string dbName, string tableName)
        {
            //取表的关联关系,从关联的配置文件里取
            string relationFileName = TableRelation.getTableRelationDiskFileName(dbName);
            if (System.IO.File.Exists(relationFileName) == false)//配置文件不存在
                return null;
            ArrayList returnValue = new ArrayList();
            //找所有的外键
            string xPath = "//Table[@EName='" + tableName + "']/tr/td";//[@RelationEName='']
            XmlDocument doc = new XmlDocument();
            doc.Load(relationFileName);
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            foreach (XmlNode node in nodelist)
            {
                string fieldName = node.Attributes.GetNamedItem("ColumnEName").InnerXml;
                string relationFieldName = node.Attributes.GetNamedItem("RelationEName").InnerXml;
                if (relationFieldName == "")
                    continue;
                returnValue.Add(fieldName);
            }
            return returnValue;
        }



        /// <summary>
        /// 本局数据库名称，从关联配置文件中中取一个表的外来关键字
        /// 返回字符数组
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <param name="tableName">表名称</param>
        /// <returns>如果配置文件不存在，返回null</returns>
        public static string[] getForeignKeyArrayFromDatabaseName(string dbName, string tableName)
        {
            //取表的关联关系,从关联的配置文件里取
            string relationFileName = TableRelation.getTableRelationDiskFileName(dbName);
            if (System.IO.File.Exists(relationFileName) == false)//配置文件不存在
                return null;
            //找所有的外键
            string xPath = "//Table[@EName='" + tableName + "']/tr/td";//[@RelationEName='']
            XmlDocument doc = new XmlDocument();
            doc.Load(relationFileName);
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            string[] returnValue = new string[nodelist.Count];
            int i = 0;
            foreach (XmlNode node in nodelist)
            {
                string fieldName = node.Attributes.GetNamedItem("ColumnEName").InnerXml;
                string relationFieldName = node.Attributes.GetNamedItem("RelationEName").InnerXml;
                if (relationFieldName == "")
                    continue;
                returnValue[i++] = fieldName;
            }
            return returnValue;
        }










	}
}
