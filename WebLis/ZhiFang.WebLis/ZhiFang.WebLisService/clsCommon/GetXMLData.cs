using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Collections;

namespace ZhiFang.WebLisService.clsCommon
{
    public class GetXMLData
    {
        public GetXMLData()
        {
        }



        /// <summary>
        /// 取某个XML文档/文件在满足某个查询条件下的所有记录的两个属性值
        /// 将这两个属性值分别作为哈希表的key和value
        /// 以便以后可以通过哈希表中取值，比如name和value属性
        /// 如果解析XML出错，返回null
        /// </summary>
        /// <param name="xmlFileName">XML文档或文件</param>
        /// <param name="xPathString">查询条件，如果为空，则默认为"//item"</param>
        /// <param name="keyAttrName">作为哈希表的key的属性名称</param>
        /// <param name="valueAttrName">作为哈希表的value的属性名称</param>
        /// <returns>哈希表</returns>
        public static System.Collections.Hashtable getXmlNodeTowAttribute(string xmlFileName, string xPathString, string keyAttrName, string valueAttrName)
        {
            System.Collections.Hashtable hashTable = new Hashtable();
            try
            {
                XmlDocument doc = new XmlDocument();
                if (System.IO.File.Exists(xmlFileName))
                {
                    doc.Load(xmlFileName);//文件
                }
                else if (System.IO.File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + xmlFileName))
                {
                    doc.Load(System.AppDomain.CurrentDomain.BaseDirectory + xmlFileName);//文件
                }
                else
                {
                    doc.LoadXml(xmlFileName);  //文档
                }
                //查询条件，如果为空，则默认为"//item"
                if (xPathString == "") xPathString = "//item";
                XmlNodeList nodelist = doc.SelectNodes(xPathString);
                foreach (XmlNode node in nodelist)
                {
                    if (node.Attributes.Count > 0)
                    {
                        object hashKey = node.Attributes.GetNamedItem(keyAttrName).Value;//key属性
                        object hashValue = node.Attributes.GetNamedItem(valueAttrName).Value;//value属性
                        if (hashTable[hashKey] == null)
                            hashTable.Add(hashKey, hashValue);
                    }
                }
                //释放变量
                nodelist = null;
                doc = null;
            }
            catch//(System.Exception ex)
            {
                //DrmsMessageBox.Show("读取文件 " + xmlFileName + " 出错！", ex);
                return null;
            }
            return hashTable;
        }



        /// <summary>
        /// 取某个XML文档在满足某个查询条件下的所有记录的两个属性值
        /// 将这两个属性值分别作为哈希表的key和value
        /// 以便以后可以通过哈希表中取值，比如name和value属性
        /// 如果解析XML出错，返回null
        /// </summary>
        /// <param name="xmlFileName">XML文档或文件</param>
        /// <param name="xPathString">查询条件，如果为空，则默认为"//item"</param>
        /// <param name="keyAttrName">作为哈希表的key的属性名称</param>
        /// <param name="valueAttrName">作为哈希表的value的属性名称</param>
        /// <returns>哈希表</returns>
        public static System.Collections.Hashtable getXmlXPathTowAttribute(string xmlString, string xPathString, string keyAttrName, string valueAttrName)
        {
            System.Collections.Hashtable hashTable = new Hashtable();
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlString);  //文档
                //查询条件，如果为空，则默认为"//item"
                if (xPathString == "") xPathString = "//item";
                XmlNodeList nodelist = doc.SelectNodes(xPathString);
                foreach (XmlNode node in nodelist)
                {
                    if (node.Attributes.Count > 0)
                    {
                        object hashKey = node.Attributes.GetNamedItem(keyAttrName).Value;//key属性
                        object hashValue = node.Attributes.GetNamedItem(valueAttrName).Value;//value属性
                        if (hashTable[hashKey] == null)
                            hashTable.Add(hashKey, hashValue);
                    }
                }
                //释放变量
                nodelist = null;
                doc = null;
            }
            catch//(System.Exception ex)
            {
                //DrmsMessageBox.Show("解析XML出错：\n " + xmlString, ex);
                return null;
            }
            return hashTable;
        }



        /// <summary>
        /// 从Schema取定义的字段名称和字段类型
        /// 并存入到哈希表中，其中key为字段名称，value为类型
        /// </summary>
        /// <param name="schemaXSD"></param>
        /// <returns>哈希表</returns>
        public static System.Collections.Hashtable getSchemaFieldNameType(string schemaXSD)
        {
            string xPathString;
            System.Collections.Hashtable fieldNameTitleTable;

            //加载XSD文件
            XmlDocument docTest = new XmlDocument();
            docTest.LoadXml(schemaXSD);
            XmlNamespaceManager nsMgr = new XmlNamespaceManager(new NameTable());
            nsMgr.AddNamespace("xsd", "http://www.w3.org/2001/XMLSchema");
            xPathString = "/xsd:schema/xsd:element";
            XmlNodeList objectNodelist = docTest.DocumentElement.SelectNodes(xPathString, nsMgr);
            int countXSD = objectNodelist.Count;
            //创建对应的哈希表
            fieldNameTitleTable = new Hashtable();
            //从第二条记录取起，因为第一条是跟（ROOT）,并添加到菜单中
            for (int i = 1; i < countXSD; i++)
            {
                string fieldName = objectNodelist[i].Attributes.GetNamedItem("name").Value;//字段名称
                XmlNodeList objectNodelist1 = objectNodelist[i].SelectNodes("xsd:complexType/xsd:attribute", nsMgr);
                for (int j = 0; j < objectNodelist1.Count; j++)
                {
                    string displayName = objectNodelist1[j].Attributes.GetNamedItem("name").Value;
                    if (displayName == "ElementTypeNameRiserved")
                    {
                        string fieldTitle = objectNodelist1[j].Attributes.GetNamedItem("fixed").Value;//字段类型
                        //添加到哈希表
                        object hashKey = new object();
                        object hashValue = new object();
                        hashKey = fieldName;
                        hashValue = fieldTitle;
                        if (fieldNameTitleTable[hashKey] == null)
                            fieldNameTitleTable.Add(hashKey, hashValue);
                    }
                }
            }
            //释放变量
            objectNodelist = null;
            docTest = null;
            return fieldNameTitleTable;
        }



        /// <summary>
        /// 从Schema取定义的字段名称和字段标题
        /// 并存入到哈希表中，其中key为字段名称，value为标题
        /// </summary>
        /// <param name="schemaXSD"></param>
        /// <returns>哈希表</returns>
        public static System.Collections.Hashtable getSchemaFieldNameTitle(string schemaXSD)
        {
            string xPathString;
            System.Collections.Hashtable fieldNameTitleTable;

            //加载XSD文件
            XmlDocument docTest = new XmlDocument();
            docTest.LoadXml(schemaXSD);
            XmlNamespaceManager nsMgr = new XmlNamespaceManager(new NameTable());
            nsMgr.AddNamespace("xsd", "http://www.w3.org/2001/XMLSchema");
            xPathString = "/xsd:schema/xsd:element";
            XmlNodeList objectNodelist = docTest.DocumentElement.SelectNodes(xPathString, nsMgr);
            int countXSD = objectNodelist.Count;
            //创建对应的哈希表
            fieldNameTitleTable = new Hashtable();
            //从第二条记录取起，因为第一条是跟（ROOT）,并添加到菜单中
            for (int i = 1; i < countXSD; i++)
            {
                string fieldName = objectNodelist[i].Attributes.GetNamedItem("name").Value;//字段名称
                XmlNodeList objectNodelist1 = objectNodelist[i].SelectNodes("xsd:complexType/xsd:attribute", nsMgr);
                for (int j = 0; j < objectNodelist1.Count; j++)
                {
                    string displayName = objectNodelist1[j].Attributes.GetNamedItem("name").Value;
                    if (displayName == "displayName")
                    {
                        string fieldTitle = objectNodelist1[j].Attributes.GetNamedItem("fixed").Value;//字段标题
                        //添加到哈希表
                        object hashKey = new object();
                        object hashValue = new object();
                        hashKey = fieldName;
                        hashValue = fieldTitle;
                        if (fieldNameTitleTable[hashKey] == null)
                            fieldNameTitleTable.Add(hashKey, hashValue);
                    }
                }
            }
            //释放变量
            objectNodelist = null;
            docTest = null;
            return fieldNameTitleTable;
        }




        /// <summary>
        /// 根据搜索路径，从XML取某个元素的值。比如Name。
        /// </summary>
        /// <param name="xmlString"></param>
        /// <param name="xPathString"></param>
        /// <param name="elementName"></param>
        /// <returns></returns>
        public static string getElementValue(string xmlString, string xPathString, string elementName)
        {
            string retString = "";

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlString);
                XmlNodeList nodelist = doc.SelectNodes(xPathString);
                foreach (XmlNode node in nodelist)
                {
                    if (node.HasChildNodes)
                    {
                        for (int i = 0; i < node.ChildNodes.Count; i++)
                        {
                            if (node.ChildNodes[i].Name == elementName)
                            {
                                retString = node.ChildNodes[i].InnerXml;
                                return retString;
                            }
                        }
                    }
                }
                //释放变量
                nodelist = null;
                doc = null;
            }
            catch//(System.Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
            }
            return retString;
        }



        /// <summary>
        /// 根据搜索路径，从XML取某个元素的值。比如Name。
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="xPathString"></param>
        /// <param name="elementName"></param>
        /// <returns></returns>
        public static string getElementValueFromFile(string fileName, string xPathString, string elementName)
        {
            string retString = "";

            try
            {
                XmlDocument doc = new XmlDocument();
                if (System.IO.File.Exists(fileName))
                    doc.Load(fileName);
                else
                    doc.LoadXml(fileName);
                XmlNodeList nodelist = doc.SelectNodes(xPathString);
                foreach (XmlNode node in nodelist)
                {
                    if (node.HasChildNodes)
                    {
                        for (int i = 0; i < node.ChildNodes.Count; i++)
                        {
                            if (node.ChildNodes[i].Name == elementName)
                            {
                                retString = node.ChildNodes[i].InnerXml;
                                return retString;
                            }
                        }
                    }
                }
                //释放变量
                nodelist = null;
                doc = null;
            }
            catch//(System.Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
            }
            return retString;
        }


        /// <summary>
        /// 根据搜索路径，从XML取某个属性的值。比如GUID。
        /// </summary>
        /// <param name="xmlString"></param>
        /// <param name="xPathString"></param>
        /// <param name="attrName"></param>
        /// <returns></returns>
        public static string getAttrValue(string xmlString, string xPathString, string attrName)
        {
            string retString = "";

            try
            {
                if ((xmlString == "") || (xmlString == null))
                    return retString;
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlString);
                XmlNodeList nodelist = doc.SelectNodes(xPathString);
                foreach (XmlNode node in nodelist)
                {
                    if (node.Attributes.Count > 0)
                        retString = node.Attributes[attrName].Value;
                    if (retString != "")
                        break;
                }
                //释放变量
                nodelist = null;
                doc = null;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return retString;
        }




        /// <summary>
        /// 取某个XML文档在满足某个查询条件下的所有记录的两个node值
        /// 将这两个值分别作为哈希表的key和value
        /// 以便以后可以通过哈希表中取值，比如name和value属性
        /// 如果解析XML出错，返回null
        /// </summary>
        /// <param name="xmlFileName">XML文档或文件</param>
        /// <param name="xPathString">查询条件，如果为空，则默认为"//item"</param>
        /// <param name="keyAttrName">作为哈希表的key的属性名称</param>
        /// <param name="valueAttrName">作为哈希表的value的属性名称</param>
        /// <returns>哈希表</returns>
        public static System.Collections.Hashtable getXmlTowNodeValue(string xmlFileName, string xPathString, string keyAttrName, string valueAttrName)
        {
            System.Collections.Hashtable hashTable = new Hashtable();
            try
            {
                XmlDocument doc = new XmlDocument();
                if (System.IO.File.Exists(xmlFileName))
                {
                    doc.Load(xmlFileName);//文件
                }
                else if (System.IO.File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + xmlFileName))
                {
                    doc.Load(System.AppDomain.CurrentDomain.BaseDirectory + xmlFileName);//文件
                }
                else
                {
                    doc.LoadXml(xmlFileName);  //文档
                }
                //查询条件，如果为空，则默认为"//item"
                if (xPathString == "") xPathString = "//item";
                XmlNodeList nodelist = doc.SelectNodes(xPathString);
                foreach (XmlNode node in nodelist)
                {
                    string saveKey = "";
                    string saveValue = "";
                    for (int i = 0; i < node.ChildNodes.Count; i++)
                    {
                        string nodeName = node.ChildNodes[i].Name;
                        string nodeValue = node.ChildNodes[i].InnerXml;
                        if (nodeName == keyAttrName)
                            saveKey = nodeValue;
                        else if (nodeName == valueAttrName)
                            saveValue = nodeValue;
                    }
                    if (saveKey != "") //取到内容
                    {
                        object hashKey = saveKey;//key属性
                        object hashValue = saveValue;//value属性
                        if (hashTable[hashKey] == null)
                            hashTable.Add(hashKey, hashValue);
                    }
                }
                //释放变量
                nodelist = null;
                doc = null;
            }
            catch//(System.Exception ex)
            {
                //DrmsMessageBox.Show("读取文件 " + xmlFileName + " 出错！", ex);
                return null;
            }
            return hashTable;
        }




        /// <summary>
        /// 从某个XML的结点中取其所有的子结点的名称和内容
        /// 将结果存放到哈希表中，其中：
        /// key：结点名称(字段名称)
        /// value：结点内容（字段的内容）；如果该内容用cdata包起来。则将其去掉
        /// </summary>
        /// <param name="xmlNode"></param>
        /// <returns></returns>
        public static System.Collections.Hashtable getXmlNodeNameAndValue(XmlNode xmlNode)
        {
            System.Collections.Hashtable hashTable = new Hashtable();
            for (int i = 0; i < xmlNode.ChildNodes.Count; i++)
            {
                object hashKey = new object();
                object hashValue = new object();
                hashKey = xmlNode.ChildNodes[i].Name.ToUpper();
                string fieldValue = xmlNode.ChildNodes[i].InnerXml;
                string startCdata = "<![CDATA[";
                string endCdata = "]]>";
                if (fieldValue.StartsWith(startCdata) && (fieldValue.EndsWith(endCdata)))
                {
                    fieldValue = fieldValue.Replace(startCdata, "");
                    fieldValue = fieldValue.Replace(endCdata, "");
                }
                hashValue = fieldValue;
                if (hashTable[hashKey] == null)
                    hashTable.Add(hashKey, hashValue);
            }
            return hashTable;
        }



        /// <summary>
        /// 从某个XML的结点中取其所有的子结点的名称和内容
        /// 将结果存放到哈希表中，其中：
        /// key：结点名称(字段名称)
        /// value：结点内容（字段的内容）；如果该内容用cdata包起来。则将其去掉
        /// </summary>
        /// <param name="xmlNode"></param>
        /// <returns></returns>
        public static System.Collections.Hashtable getXmlNodeAttributes(XmlNode xmlNode)
        {
            System.Collections.Hashtable hashTable = new Hashtable();
            for (int i = 0; i < xmlNode.Attributes.Count; i++)
            {
                object hashKey = xmlNode.Attributes[i].Name;
                object hashValue = xmlNode.Attributes[i].Value;
                if (hashTable[hashKey] == null)
                    hashTable.Add(hashKey, hashValue);
            }
            return hashTable;
        }


        public static XmlNode getRootNode(XmlNode xmlNode)
        {
            if (xmlNode.ParentNode != null)
                return GetXMLData.getRootNode(xmlNode.ParentNode);
            return xmlNode;
        }



    
    
    
    
    
    
    
    }



}
