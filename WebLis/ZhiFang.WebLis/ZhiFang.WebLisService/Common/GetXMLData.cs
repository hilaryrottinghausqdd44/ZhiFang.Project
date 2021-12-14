using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Collections;

namespace ZhiFang.WebLisService.Common
{
    public class GetXMLData
    {
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
                hashKey = xmlNode.ChildNodes[i].Name;
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

    }
}