using System;
using System.Xml;
using System.Collections;

namespace ZhiFang.WebLisService.clsCommon
{
	/// <summary>
	/// TableRelation ��ժҪ˵����
	/// </summary>
	public class TableRelation
	{
		public TableRelation()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}



        /// <summary>
        /// ��ȡ���ݿ�ı�֮������������ļ�����
        /// ���������ж��ļ��Ƿ���ڣ�����ֱ�ӽ��ļ����Ʒ���
        /// ��D:\WebSite\QMSData\shared\Configuration\ConfigurationData\1\���ݿ�����\TablesRelation.xml
        /// </summary>
        /// <returns>�ļ�����</returns>
        public static string getTableRelationDiskFileName(string dbName)
        {
            string path = TableConfig.getTableDiskPath(dbName);
            path += @"\TablesRelation.xml";
            //�ļ��Ƿ����
            //if(System.IO.File.Exists(path) == false) path = null;
            return path;
        }



		public static XmlDocument makeTableRelationForeignKey(XmlNode parentTableNode, string xPath, XmlDocument docRelation)
		{
			if(parentTableNode.Name != "Table")
				return docRelation;
			//ȡ����������
			string parentTableName = TableConfig.getTableNameFromTableNode(parentTableNode);
			//������ǰ���µ��ӱ�
			string xPathChildTable = "/tr/Table";
			xPath = xPath + "[@EName='" + parentTableName + "']" + xPathChildTable;
			XmlNodeList nodelist = docRelation.SelectNodes(xPath);
			foreach(XmlNode node in nodelist)
			{
				//�������������
				string xPathPK = "./tr/td";
				XmlNodeList nodelistPK = parentTableNode.SelectNodes(xPathPK);
				foreach(XmlNode nodePK in nodelistPK)
				{
					string fieldName = nodePK.Attributes.GetNamedItem("ColumnEName").InnerXml;
					string fieldType = nodePK.Attributes.GetNamedItem("ColumnType").InnerXml;
					XmlNode nodeTD = docRelation.CreateNode(XmlNodeType.Element, "td", "");
					//���ֶ�����
					XmlAttribute attr = docRelation.CreateAttribute("ColumnEName");
					attr.InnerXml = parentTableName + "_" + fieldName;//�������,Ĭ����ǰ�ӱ������
					nodeTD.Attributes.Append(attr);
					//���ֶ�����
					attr = docRelation.CreateAttribute("ColumnType");
					attr.InnerXml = fieldType;
					nodeTD.Attributes.Append(attr);
					//�ӹ������ֶ�����
					attr = docRelation.CreateAttribute("RelationEName");
					attr.InnerXml = fieldName;//�������ֶ�����
					nodeTD.Attributes.Append(attr);
					//��λ������������λ��
					XmlNodeList nodelistFK = node.SelectNodes("./tr");
					if(nodelistFK.Count == 1)
						nodelistFK[0].AppendChild(nodeTD);
				}
				//�ݹ�ȡ������
				docRelation = makeTableRelationForeignKey(node, xPath, docRelation);
			}
			return docRelation;
		}





		/// <summary>
		/// �������ݿ����ƴ��������Ĺ�����ϵ
		/// </summary>
		/// <param name="dbName">���ݿ�����</param>
		/// <returns></returns>
		public static string makeTableRelationFromTablesConfig(string dbName)
		{
			//ȡ�����ݿ������ļ�
			string configFileName = TableConfig.getTableConfigDiskFileName(dbName);
			//ȡ�����ݿ�ı���������ļ�
            string relationFileName = TableRelation.getTableRelationDiskFileName(dbName);
			//���´����µı�����ļ�
			XmlDocument docRelation = new XmlDocument();
			//����XML����������
            XmlDeclaration declarationRelation = docRelation.CreateXmlDeclaration("1.0", "utf-8", "");
			docRelation.AppendChild(declarationRelation);
			//����һ����Ԫ��
			XmlElement elementRoot = docRelation.CreateElement("Tables");            
			XmlDocument doc = new XmlDocument();
			doc.Load(configFileName);
			string xPath = "/Tables/Table";
			XmlNodeList nodelist = doc.SelectNodes(xPath);
			//�ӱ��������ֶεĶ���
			foreach(XmlNode node in nodelist)
			{
				//�Ӹ�Ԫ��
				docRelation.AppendChild(makeTableRelationFromTableNode(elementRoot, node, docRelation));
			}
			//�������ɵ�����,���ø���������:Ҫһ��һ�����������,�������ò���ȷ
			xPath = "/Tables/Table";
			nodelist = docRelation.SelectNodes(xPath);
			foreach(XmlNode node in nodelist)
			{
				//ȡ������
				docRelation = makeTableRelationForeignKey(node, xPath, docRelation);
			}
			docRelation.Save(relationFileName);
			return docRelation.OuterXml;
		}


		public static XmlNode makeTableRelationFromTableNode(XmlNode elementParent, XmlNode tableNode, XmlDocument docRelation)
		{
			//�ҵ�ǰ�����µ��ӱ�
			if(tableNode.Name != "Table")
				return elementParent;
			//����XML�������
			XmlNode nodeRelation = docRelation.CreateNode(XmlNodeType.Element,tableNode.Name, "");
			string tableName = tableNode.Attributes.GetNamedItem("EName").InnerXml;
			XmlAttribute attr = docRelation.CreateAttribute("EName");
			attr.InnerXml = tableName;
			nodeRelation.Attributes.Append(attr);
			//��tr
			XmlNode nodeTR = docRelation.CreateNode(XmlNodeType.Element, "tr", "");
			//�Ҹñ������
			string xPath = "./tr/td[@KeyIndex='Yes']";
			XmlNodeList nodelist = tableNode.SelectNodes(xPath);
			foreach(XmlNode node in nodelist)
			{
				string fieldName = node.Attributes.GetNamedItem("ColumnEName").InnerXml;
				string fieldTypeXML = node.Attributes.GetNamedItem("ColumnType").InnerXml;
				string fieldDefault = node.Attributes.GetNamedItem("ColumnDefault").InnerXml;
				XmlNode nodeTD = docRelation.CreateNode(XmlNodeType.Element, "td", "");
				//���ֶ�����
				attr = docRelation.CreateAttribute("ColumnEName");
				attr.InnerXml = fieldName;
				nodeTD.Attributes.Append(attr);
				//���ֶ�����(��֮ǰҪ����ת��)
				attr = docRelation.CreateAttribute("ColumnType");
                FieldType fieldType = ConvertData.convertXmlFieldType(fieldTypeXML);
				if(fieldDefault == "�Զ����()")
				{
					if(fieldType != FieldType.GUID)
						fieldType = FieldType.IDENTITY;
				}
				attr.InnerXml = fieldType.ToString();
				nodeTD.Attributes.Append(attr);
				//�ӹ������ֶ�����
				attr = docRelation.CreateAttribute("RelationEName");
				attr.InnerXml = "";//����������ֶ�û�й����ֶ�
				nodeTD.Attributes.Append(attr);
				//�ӱ����������
				nodeTR.AppendChild(nodeTD);
			}
			//�Ӹñ���ӱ�
			xPath = "./tr/Table";
			XmlNodeList nodelistChildTable = tableNode.SelectNodes(xPath);
			foreach(XmlNode childTableNode in nodelistChildTable)
			{
				//�õݹ鷽��ȡ�ӱ�
				nodeTR = makeTableRelationFromTableNode(nodeTR, childTableNode, docRelation);
			}
			nodeRelation.AppendChild(nodeTR);
			//�ӱ�����
			elementParent.AppendChild(nodeRelation);
			return elementParent;
		}




		/// <summary>
		/// �ӹ�������ȡһ��������ؼ��֣����أ������ؼ��ֶ����ƣ��ֶ����ͣ��������ַ���
        /// ���ع�ϣ��
		/// </summary>
        /// <param name="dbName">���ݿ�����</param>
        /// <param name="tableName">������</param>
        /// <returns>��������ļ������ڣ�����null</returns>
        public static System.Collections.Hashtable getPrimaryKeyHashTableFromDatabaseName(string dbName, string tableName)
		{
			//ȡ��Ĺ�����ϵ,�ӹ����������ļ���ȡ
            string relationFileName = TableRelation.getTableRelationDiskFileName(dbName);
			if(System.IO.File.Exists(relationFileName) == false)//�����ļ�������
				return null;
            Hashtable returnValue = new Hashtable();
            //�����е����
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
        /// �ӹ�������ȡһ��������ؼ��֣����أ������ؼ��ֶ����ƣ��ֶ����ͣ��������ַ���
        /// ���ع�ϣ��
        /// </summary>
        /// <param name="systemName">Ӧ��ϵͳ����</param>
        /// <param name="tableName">������</param>
        /// <returns>��������ļ������ڣ�����null</returns>
        public static System.Collections.Hashtable getPrimaryKeyHashTableFromSystemName(string systemName, string tableName)
        {
            //ȡ��Ĺ�����ϵ,�ӹ����������ļ���ȡ
            string dbName = TableConfig.getDatabaseNameFromSystemName(systemName);
            return TableRelation.getPrimaryKeyHashTableFromDatabaseName(dbName, tableName);
        }



        /// <summary>
        /// �ӱ�����������ļ���ȡ������ؼ��֣������� + ����
        /// ���ض�ջ
        /// </summary>
        /// <param name="dbName">���ݿ�����</param>
        /// <param name="tableName">������</param>
        /// <returns>��������ļ������ڣ�����null</returns>
        public static Stack getPrimaryKeyStackFromDatabaseName(string dbName, string tableName)
        {
            //ȡ��ṹ�����ļ�����
            string tableRelationFileName = TableRelation.getTableRelationDiskFileName(dbName);
            if (tableRelationFileName == null)//�ļ�������
                return null;
            //�ȶ�λ����ǰ��������������ֶ�
            string xPath = "//Table[@EName='" + tableName + "']/tr/td";
            XmlDocument doc = new XmlDocument();
            doc.Load(tableRelationFileName);
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            Stack returnValue = new Stack();
            foreach (XmlNode node in nodelist)
            {
                //ȡ�ñ����������
                string fieldName = node.Attributes.GetNamedItem("ColumnEName").InnerXml;
                returnValue.Push(fieldName);
            }
            return returnValue;
        }


        /// <summary>
        /// �ӱ�����������ļ���ȡ������ؼ��֣������� + ����
        /// ���ض�ջ
        /// </summary>
        /// <param name="systemName">Ӧ��ϵͳ����</param>
        /// <param name="tableName">������</param>
        /// <returns>��������ļ������ڣ�����null</returns>
        public static Stack getPrimaryKeyStackFromSystemName(string systemName, string tableName)
        {
            string dbName = TableConfig.getDatabaseNameFromSystemName(systemName);
            return TableRelation.getPrimaryKeyStackFromDatabaseName(dbName, tableName);
        }



        /// <summary>
        /// �ӱ�����������ļ���ȡ������ؼ��֣������� + ����
        /// ��������ArrayList
        /// </summary>
        /// <param name="dbName">���ݿ�����</param>
        /// <param name="tableName">������</param>
        /// <returns>��������ļ������ڣ�����null</returns>
        public static ArrayList getPrimaryKeyArrayListFromDatabaseName(string dbName, string tableName)
        {
            //ȡ��ṹ�����ļ�����
            string tableRelationFileName = TableRelation.getTableRelationDiskFileName(dbName);
            if (tableRelationFileName == null)//�ļ�������
                return null;
            //�ȶ�λ����ǰ��������������ֶ�
            string xPath = "//Table[@EName='" + tableName + "']/tr/td";
            XmlDocument doc = new XmlDocument();
            doc.Load(tableRelationFileName);
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            ArrayList returnValue = new ArrayList();
            foreach (XmlNode node in nodelist)
            {
                //ȡ�ñ����������
                string fieldName = node.Attributes.GetNamedItem("ColumnEName").InnerXml;
                returnValue.Add(fieldName);
            }
            return returnValue;
        }


        /// <summary>
        /// �ӱ�����������ļ���ȡ������ؼ��֣������� + ����
        /// �����ַ�����string[]
        /// </summary>
        /// <param name="dbName">���ݿ�����</param>
        /// <param name="tableName">������</param>
        /// <returns>��������ļ������ڣ�����null</returns>
        public static string[] getPrimaryKeyArrayFromDatabaseName(string dbName, string tableName)
        {
            //ȡ��ṹ�����ļ�����
            string tableRelationFileName = TableRelation.getTableRelationDiskFileName(dbName);
            if (tableRelationFileName == null)//�ļ�������
                return null;
            //�ȶ�λ����ǰ��������������ֶ�
            string xPath = "//Table[@EName='" + tableName + "']/tr/td";
            XmlDocument doc = new XmlDocument();
            doc.Load(tableRelationFileName);
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            string[] returnValue = new string[nodelist.Count];
            int i = 0;
            foreach (XmlNode node in nodelist)
            {
                //ȡ�ñ����������
                string fieldName = node.Attributes.GetNamedItem("ColumnEName").InnerXml;
                returnValue[i++] = fieldName;
            }
            return returnValue;
        }




        /// <summary>
        /// �������ݿ����ƣ��ӹ��������ļ�����ȡһ����������ؼ���
        /// ���ع�ϣ��(�ֶ����ƣ�������������ֶ�����)
        /// </summary>
        /// <param name="dbName">���ݿ�����</param>
        /// <param name="tableName">������</param>
        /// <returns>��������ļ������ڣ�����null</returns>
        public static System.Collections.Hashtable getForeignKeyHashTableFromDatabaseName(string dbName, string tableName)
        {
            //ȡ��Ĺ�����ϵ,�ӹ����������ļ���ȡ
            string relationFileName = TableRelation.getTableRelationDiskFileName(dbName);
            if (System.IO.File.Exists(relationFileName) == false)//�����ļ�������
                return null;
            Hashtable returnValue = new Hashtable();
            //�����е����
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
        /// �������ݿ����ƣ��ӹ��������ļ�����ȡһ����������ؼ���
        /// ���ع�ϣ��(�ֶ����ƣ��ֶ�����ö������Enum�����ַ���)
        /// </summary>
        /// <param name="dbName">���ݿ�����</param>
        /// <param name="tableName">������</param>
        /// <returns>��������ļ������ڣ�����null</returns>
        public static System.Collections.Hashtable getForeignKeyNameTypeEnumHashTableFromDatabaseName(string dbName, string tableName)
        {
            //ȡ��Ĺ�����ϵ,�ӹ����������ļ���ȡ
            string relationFileName = TableRelation.getTableRelationDiskFileName(dbName);
            if (System.IO.File.Exists(relationFileName) == false)//�����ļ�������
                return null;
            Hashtable returnValue = new Hashtable();
            //�����е����
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
        /// �������ݿ����ƣ��ӹ��������ļ�����ȡһ����������ؼ���
        /// ���ع�ϣ��(�ֶ����ƣ��ֶ����ͣ���FieldType����)
        /// </summary>
        /// <param name="dbName">���ݿ�����</param>
        /// <param name="tableName">������</param>
        /// <returns>��������ļ������ڣ�����null</returns>
        public static System.Collections.Hashtable getForeignKeyNameTypeHashTableFromDatabaseName(string dbName, string tableName)
        {
            //ȡ��Ĺ�����ϵ,�ӹ����������ļ���ȡ
            string relationFileName = TableRelation.getTableRelationDiskFileName(dbName);
            if (System.IO.File.Exists(relationFileName) == false)//�����ļ�������
                return null;
            Hashtable returnValue = new Hashtable();
            //�����е����
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
        /// �������ݿ����ƣ��ӹ��������ļ�����ȡһ����������ؼ���
        /// ���ض�ջ
        /// </summary>
        /// <param name="dbName">���ݿ�����</param>
        /// <param name="tableName">������</param>
        /// <returns>��������ļ������ڣ�����null</returns>
        public static System.Collections.Stack getForeignKeyStackFromDatabaseName(string dbName, string tableName)
        {
            //ȡ��Ĺ�����ϵ,�ӹ����������ļ���ȡ
            string relationFileName = TableRelation.getTableRelationDiskFileName(dbName);
            if (System.IO.File.Exists(relationFileName) == false)//�����ļ�������
                return null;
            Stack returnValue = new Stack();
            //�����е����
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
        /// �������ݿ����ƣ��ӹ��������ļ�����ȡһ����������ؼ���
        /// ��������ArrayList
        /// </summary>
        /// <param name="dbName">���ݿ�����</param>
        /// <param name="tableName">������</param>
        /// <returns>��������ļ������ڣ�����null</returns>
        public static System.Collections.ArrayList getForeignKeyArrayListFromDatabaseName(string dbName, string tableName)
        {
            //ȡ��Ĺ�����ϵ,�ӹ����������ļ���ȡ
            string relationFileName = TableRelation.getTableRelationDiskFileName(dbName);
            if (System.IO.File.Exists(relationFileName) == false)//�����ļ�������
                return null;
            ArrayList returnValue = new ArrayList();
            //�����е����
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
        /// �������ݿ����ƣ��ӹ��������ļ�����ȡһ����������ؼ���
        /// �����ַ�����
        /// </summary>
        /// <param name="dbName">���ݿ�����</param>
        /// <param name="tableName">������</param>
        /// <returns>��������ļ������ڣ�����null</returns>
        public static string[] getForeignKeyArrayFromDatabaseName(string dbName, string tableName)
        {
            //ȡ��Ĺ�����ϵ,�ӹ����������ļ���ȡ
            string relationFileName = TableRelation.getTableRelationDiskFileName(dbName);
            if (System.IO.File.Exists(relationFileName) == false)//�����ļ�������
                return null;
            //�����е����
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
