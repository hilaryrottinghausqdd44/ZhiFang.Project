using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Collections;
using System.Xml;
using System.Text;

using System.Security.AccessControl;

using ECDS.Common;
using ZhiFang.WebLisService.WL.IBLL;

namespace ZhiFang.WebLisService.WL.Common
{
    //字段列表程序
    public class ReportData
    {

        /// <summary>
        /// 获取对照数据的配置文件所在的目录,如 .\xml\\ContrastData\\
        /// 如果目录不存在,则创建
        /// 如果出现异常,抛出异常,并返回null
        /// </summary>
        /// <returns></returns>
        public static string getContrastDataDiskPath()
        {

            string path = null;
            try
            {
                path = ZhiFang.WebLisService.Util.Tools.getSystemPath();
                path += "xml\\ContrastData\\";
                if (System.IO.Directory.Exists(path) == false)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception("获取系统启动目录出错,或者创建目录" + path + "不成功!", ex);
            }
            return path;
        }

        /// <summary>
        /// 获取对照数据的配置文件名称
        /// ,如 .\xml\\ContrastData\\[用户登录帐号].XML
        /// </summary>
        /// <returns></returns>
        public static string getContrastDataDiskFileName(string userName)
        {
            string path = null;
            try
            {
                path = ReportData.getContrastDataDiskPath();
                if (path == null)
                    return path;
                path += userName.ToString() + ".xml";
            }
            catch
            {
                throw;
            }
            return path;
        }

        /// <summary>
        /// 获取某个单位的某个表的对照配置属性
        /// ,如 .\xml\\ContrastData\\[用户登录帐号].XML
        /// </summary>
        /// <returns></returns>
        public static Hashtable getContrastTableAttr(string userName, string tableName)
        {
            Hashtable returnValue = new Hashtable();
            string path = null;
            try
            {
                path = ReportData.getContrastDataDiskFileName(userName);
                if (System.IO.File.Exists(path))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(path);
                    string xPath = "//Table[@ename='" + tableName + "']";
                    XmlNodeList nodelist = doc.SelectNodes(xPath);
                    if (nodelist.Count > 0)
                    {
                        XmlNode node = nodelist[0];
                        return clsCommon.GetXMLData.getXmlNodeAttributes(node);
                    }
                }
            }
            catch
            {
                throw;
            }
            return returnValue;
        }

        /// <summary>
        /// 获取某个单位的某个表的对照数据
        /// ,如 .\xml\\ContrastData\\[用户登录帐号].XML
        /// </summary>
        /// <returns></returns>
        public static Hashtable getContrastData(string userName, string tableName)
        {
            Hashtable returnValue = new Hashtable();
            string path = null;
            try
            {
                path = ReportData.getContrastDataDiskFileName(userName);
                if (System.IO.File.Exists(path))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(path);
                    string xPath = "//Table[@ename='" + tableName + "']/item";
                    XmlNodeList nodelist = doc.SelectNodes(xPath);
                    foreach (XmlNode node in nodelist)
                    {
                        Hashtable hashAttr = clsCommon.GetXMLData.getXmlNodeAttributes(node);
                        if (hashAttr["guid"] == null)
                            continue;
                        if (hashAttr["value"] == null)
                            continue;
                        string fieldName = hashAttr["guid"].ToString();
                        string fieldValue = hashAttr["value"].ToString();
                        if (returnValue[fieldName] == null)
                        {
                            returnValue.Add(fieldName, fieldValue);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            return returnValue;
        }

        /// <summary>
        /// 获取系统XML配置文件所在的目录,如 .\xml\\SysXML\\
        /// 如果目录不存在,则创建
        /// 如果出现异常,抛出异常,并返回null
        /// </summary>
        /// <returns></returns>
        public static string getSysXmlDiskPath()
        {
            string path = null;
            try
            {
                path = ZhiFang.WebLisService.Util.Tools.getSystemPath();
                path += "xml\\SysXML\\";
                if (System.IO.Directory.Exists(path) == false)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception("获取系统启动目录出错,或者创建目录" + path + "不成功!", ex);
            }
            return path;
        }

        /// <summary>
        /// 获取系统XML配置文件的字段配置文件名称
        /// ,如 .\xml\\SysXML\\FieldList.XML
        /// </summary>
        /// <returns></returns>
        public static string getFieldListXMLDiskFileName()
        {
            string path = null;
            try
            {
                path = ReportData.getSysXmlDiskPath();
                if (path == null)
                    return path;
                path += "FieldList.xml";
            }
            catch
            {
                throw;
            }
            return path;
        }

        /// <summary>
        /// 将某个目录授权给某个帐号设置正常访问权限:可以读写等
        /// </summary>
        /// <param name="path"></param>
        /// <param name="userAccount"></param>
        public static void setPathFullControlRight(string path, string userAccount)
        {
            //给目录设置权限
            if (System.IO.Directory.Exists(path))
            {
                bool ok;
                //目录信息
                DirectoryInfo pathInfo = new DirectoryInfo(path);
                DirectorySecurity power = pathInfo.GetAccessControl(AccessControlSections.Access);
                //继承权限权限
                InheritanceFlags iFlags = new InheritanceFlags();
                iFlags = InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit;
                //创建权限规则
                FileSystemAccessRule rule = new FileSystemAccessRule(userAccount, FileSystemRights.FullControl, iFlags, PropagationFlags.None, AccessControlType.Allow);
                //添加权限
                power.ModifyAccessRule(AccessControlModification.Add, rule, out ok);
                //将权限授权给目录
                try
                {
                    pathInfo.SetAccessControl(power);
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// 获取系统XML配置文件所在的目录,如 .\xml\\ExportModal\\
        /// 如果目录不存在,则创建
        /// 如果出现异常,抛出异常,并返回null
        /// </summary>
        /// <returns></returns>
        public static string getExportModalDiskPath()
        {
            string path = null;
            try
            {
                path = ZhiFang.WebLisService.Util.Tools.getSystemPath();
                path += "xml\\ExportModal\\";
                if (System.IO.Directory.Exists(path) == false)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                //给Everyone帐号设置正常访问权限:可以读写等
                ReportData.setPathFullControlRight(path, "Everyone");

            }
            catch (System.Exception ex)
            {
                throw new Exception("获取系统启动目录出错,或者创建目录" + path + "不成功!", ex);
            }
            return path;
        }

        /// <summary>
        /// 获取系统XML配置文件的字段配置文件名称
        /// ,如 .\xml\\ExportModal\\[用户登录帐号].XML
        /// 在本方法中将该文件的属性设置为正常,以便以后能修改
        /// </summary>
        /// <returns></returns>
        public static string getExportModalDiskFileName(string userID)
        {
            string path = null;
            try
            {
                path = ReportData.getExportModalDiskPath();
                if (path == null)
                    return path;
                path += userID.ToString() + ".xml";
            }
            catch
            {
                throw;
            }
            return path;
        }

        /// <summary>
        /// 从数据库里获取某个表的字段名称列表(直接按字段的名称返回,不转换为大小写)
        /// 返回队列:按次序顺序返回
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Queue getTableColumnNameQueue(string tableName)
        {
            //查询一条记录
            string sqlModal = "SELECT * FROM \"{0}\" WHERE {1}";
            string sql = "";
            sql = string.Format(sqlModal, tableName, "1>2");
            DataSet ds = WL.BLL.DataConn.CreateDB().ExecDS(sql);
            DataTable dtColumn = ds.Tables[0];
            //取当前数据库表的字典到哈希表
            Queue hashColumn = new Queue();
            for (int i = 0; i < dtColumn.Columns.Count; i++)
            {
                //字段名称:用大写
                string fieldName = dtColumn.Columns[i].ColumnName;
                hashColumn.Enqueue(fieldName);
            }
            return hashColumn;
        }

        /// <summary>
        /// 根据InterfaceConfig.xml里的配置情况,和视图ReportFormView的配置情况
        /// 生成字段名称和描述的对照关系到XML文件FieldList.xml中,用表名称ReportFormView区别
        /// </summary>
        /// <returns></returns>
        public static string makeFieldListXML()
        {
            Hashtable hashFieldListALL = new Hashtable();
            //源文件
            string sourcFile = ReportData.getSysXmlDiskPath() + "InterfaceConfig.xml";
            if (System.IO.File.Exists(sourcFile))
            {
                //取字段定义情况到哈希表
                hashFieldListALL = clsCommon.GetXMLData.getXmlNodeTowAttribute(sourcFile, "//datafield", "ename", "cname");
                //转换成大写
                hashFieldListALL = clsCommon.Tools.convertHashKeyToUpper(hashFieldListALL);
            }
            XmlDocument doc = new XmlDocument();
            //目标文件
            string destFile = ReportData.getFieldListXMLDiskFileName();
            if (System.IO.File.Exists(destFile) == false)
            {
                //重新创建新的表统计配置文件
                //加入XML的声明段落
                XmlDeclaration declarationDoc = doc.CreateXmlDeclaration("1.0", "utf-8", "");
                doc.AppendChild(declarationDoc);
                //创建一个根元素
                XmlElement elementRoot = doc.CreateElement("Tables");
                doc.AppendChild(elementRoot);
                doc.Save(destFile);
            }
            doc.Load(destFile);
            //判断是否已经有改表的字段的定义,有的话则先删除
            string tableName = "ReportFormView";
            string xPath = "//Table[@ename='" + tableName + "']";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            foreach (XmlNode xmlNode in nodelist)
            {
                XmlNode delNode = xmlNode.ParentNode;
                delNode.RemoveChild(xmlNode);
            }
            //保存
            doc.Save(destFile);
            //创建表结点
            XmlNode tableNode = doc.CreateNode(XmlNodeType.Element, "Table", "");
            //表结点的属性
            XmlAttribute attr = doc.CreateAttribute("ename");
            attr.InnerXml = tableName;
            tableNode.Attributes.Append(attr);
            attr = doc.CreateAttribute("cname");
            attr.InnerXml = "报告上传视图";
            tableNode.Attributes.Append(attr);
            //取视图定义的字段列表()
            Queue queueColumn = WL.Common.ReportData.getTableColumnNameQueue(tableName);
            System.Collections.IEnumerator enumerator = queueColumn.GetEnumerator();
            while (enumerator.MoveNext())
            {
                string fieldName = enumerator.Current.ToString();
                string fieldNameUpper = fieldName.ToUpper();
                string fieldDesc = fieldName;
                if (hashFieldListALL[fieldNameUpper] != null)
                {
                    fieldDesc = hashFieldListALL[fieldNameUpper].ToString();
                }
                else
                {
                    //否则不生成
                    continue;
                }
                //字段结点
                //标题
                XmlNode fieldNode = doc.CreateNode(XmlNodeType.Element, "datafield", "");
                XmlAttribute attrField = doc.CreateAttribute("ename");
                attrField.InnerXml = fieldName;
                fieldNode.Attributes.Append(attrField);
                attrField = doc.CreateAttribute("cname");
                attrField.InnerXml = fieldDesc;
                fieldNode.Attributes.Append(attrField);
                //加到表结点作为子结点
                tableNode.AppendChild(fieldNode);
            }
            xPath = "/Tables";
            nodelist = doc.SelectNodes(xPath);
            if (nodelist.Count > 0)
            {
                //加到根结点下
                nodelist[0].AppendChild(tableNode);
            }
            //保存
            doc.Save(destFile);
            return doc.OuterXml;
        }

        /// <summary>
        /// 获取视图ReportFormView的字段名称列表,按定义的顺序返回队列
        /// </summary>
        /// <returns></returns>
        public static Queue getReportFormViewFieldNameQueue()
        {
            Queue returnValue = new Queue();
            //目标文件
            string destFile = ReportData.getFieldListXMLDiskFileName();
            if (System.IO.File.Exists(destFile))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(destFile);
                string tableName = "ReportFormView";
                string xPath = "//Table[@ename='" + tableName + "']/datafield";
                XmlNodeList nodelist = doc.SelectNodes(xPath);
                foreach (XmlNode node in nodelist)
                {
                    Hashtable hashAttr = clsCommon.GetXMLData.getXmlNodeAttributes(node);
                    if (hashAttr["ename"] == null)
                        continue;
                    string fieldName = hashAttr["ename"].ToString();
                    string fieldDesc = fieldName;
                    if (hashAttr["cname"] != null)
                        fieldDesc = hashAttr["cname"].ToString();
                    returnValue.Enqueue(fieldName);
                }
            }
            return returnValue;
        }

        /// <summary>
        /// 获取视图ReportFormView的字段名称,字段描述列表
        /// </summary>
        /// <returns></returns>
        public static Hashtable getReportFormViewFieldNameDesc()
        {
            Hashtable returnValue = new Hashtable();
            //目标文件
            string destFile = ReportData.getFieldListXMLDiskFileName();
            if (System.IO.File.Exists(destFile))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(destFile);
                string tableName = "ReportFormView";
                string xPath = "//Table[@ename='" + tableName + "']/datafield";
                XmlNodeList nodelist = doc.SelectNodes(xPath);
                foreach (XmlNode node in nodelist)
                {
                    Hashtable hashAttr = clsCommon.GetXMLData.getXmlNodeAttributes(node);
                    if (hashAttr["ename"] == null)
                        continue;
                    string fieldName = hashAttr["ename"].ToString();
                    string fieldDesc = fieldName;
                    if (hashAttr["cname"] != null)
                        fieldDesc = hashAttr["cname"].ToString();
                    if (returnValue[fieldName] == null)
                        returnValue.Add(fieldName, fieldDesc);
                }
            }
            return returnValue;
        }

        /// <summary>
        /// 获取某个用户的导出ＥＸＣＥＬ模板名称列表
        /// 返回【模板名称，对应的视图或表名称】
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static SortedList getExportModalNameList(string userName)
        {
            SortedList returnValue = new SortedList();
            //目标文件
            string destFile = ReportData.getExportModalDiskFileName(userName);
            if (System.IO.File.Exists(destFile))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(destFile);
                string xPath = "//Table";
                XmlNodeList nodelist = doc.SelectNodes(xPath);
                foreach (XmlNode node in nodelist)
                {
                    Hashtable hashAttr = clsCommon.GetXMLData.getXmlNodeAttributes(node);
                    if (hashAttr["cname"] == null)
                        continue;
                    string modalName = hashAttr["cname"].ToString();
                    string tableName = modalName;
                    if (hashAttr["ename"] != null)
                        tableName = hashAttr["ename"].ToString();
                    if (returnValue[modalName] == null)
                        returnValue.Add(modalName, tableName);
                }
            }
            return returnValue;
        }

        /// <summary>
        /// 生成某个单位某个导出模板的选择字段SQL表达式
        /// </summary>
        /// <param name="userID">用户帐号</param>
        /// <param name="modalName">模板名称</param>
        /// <returns></returns>
        public static string makeExportModalSelectSQL(string userName, string modalName)
        {
            string selectFieldSQL = "";
            string selectFieldModal = "\"{0}\" AS \"{1}\"";
            //目标文件
            string destFile = ReportData.getExportModalDiskFileName(userName);
            if (System.IO.File.Exists(destFile))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(destFile);
                string xPath = "//Table[@cname='" + modalName + "']/datafield";
                XmlNodeList nodelist = doc.SelectNodes(xPath);
                foreach (XmlNode node in nodelist)
                {
                    Hashtable hashAttr = clsCommon.GetXMLData.getXmlNodeAttributes(node);
                    if (hashAttr["ename"] == null)
                        continue;
                    string fieldName = hashAttr["ename"].ToString();
                    string fieldDesc = fieldName;
                    if (hashAttr["cname"] != null)
                        fieldDesc = hashAttr["cname"].ToString();
                    if (selectFieldSQL != "")
                        selectFieldSQL += ",";
                    selectFieldSQL += string.Format(selectFieldModal, fieldName, fieldDesc);
                }
            }
            return selectFieldSQL;
        }

        /// <summary>
        /// 生成某个单位某个导出模板的选择字段SortedList[字段名称,字段描述]
        /// </summary>
        /// <param name="userID">用户帐号</param>
        /// <param name="modalName">模板名称</param>
        /// <returns></returns>
        public static SortedList getExportModalFieldNameDescSortedList(string userName, string modalName)
        {
            SortedList returnValue = new SortedList();
            //目标文件
            string destFile = ReportData.getExportModalDiskFileName(userName);
            if (System.IO.File.Exists(destFile))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(destFile);
                string xPath = "//Table[@cname='" + modalName + "']/datafield";
                XmlNodeList nodelist = doc.SelectNodes(xPath);
                foreach (XmlNode node in nodelist)
                {
                    Hashtable hashAttr = clsCommon.GetXMLData.getXmlNodeAttributes(node);
                    if (hashAttr["ename"] == null)
                        continue;
                    string fieldName = hashAttr["ename"].ToString();
                    string fieldDesc = fieldName;
                    if (hashAttr["cname"] != null)
                        fieldDesc = hashAttr["cname"].ToString();
                    if (returnValue[fieldName] == null)
                        returnValue.Add(fieldName, fieldDesc);
                }
            }
            return returnValue;
        }

        /// <summary>
        /// 添加模板
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="tableName">模板对应的表名称</param>
        /// <param name="modalName"></param>
        /// <param name="listBoxRight"></param>
        public static void addExportModal(string userName, string tableName, string modalName, ListBox listBoxRight)
        {
            //先删除
            ReportData.deleteExportModal(userName, modalName);
            //目标文件
            string destFile = ReportData.getExportModalDiskFileName(userName);
            XmlDocument doc = new XmlDocument();
            string xPath = "";
            XmlNodeList nodelist = null;
            if (System.IO.File.Exists(destFile))
            {
                doc.Load(destFile);
                xPath = "//Table[@cname='" + modalName + "']";
                nodelist = doc.SelectNodes(xPath);
                foreach (XmlNode node in nodelist)
                {
                    XmlNode delNode = node.ParentNode;
                    delNode.RemoveChild(node);
                }
                doc.Save(destFile);
            }
            else
            {
                //创建XML文件
                //加入XML的声明段落
                XmlDeclaration declarationDoc = doc.CreateXmlDeclaration("1.0", "utf-8", "");
                doc.AppendChild(declarationDoc);
                //创建一个根元素
                XmlElement elementRoot = doc.CreateElement("Tables");
                doc.AppendChild(elementRoot);
                doc.Save(destFile);
            }
            //创建模板结点(表结点)
            XmlNode tableNode = doc.CreateNode(XmlNodeType.Element, "Table", "");
            //表结点的属性
            XmlAttribute attr = doc.CreateAttribute("ename");
            attr.InnerXml = tableName;
            tableNode.Attributes.Append(attr);
            attr = doc.CreateAttribute("cname");
            attr.InnerXml = modalName;
            tableNode.Attributes.Append(attr);
            for (int i = 0; i < listBoxRight.Items.Count; i++)
            {
                string fieldName = listBoxRight.Items[i].Value;
                string fieldDesc = listBoxRight.Items[i].Text;
                //字段结点
                //标题
                XmlNode fieldNode = doc.CreateNode(XmlNodeType.Element, "datafield", "");
                XmlAttribute attrField = doc.CreateAttribute("ename");
                attrField.InnerXml = fieldName;
                fieldNode.Attributes.Append(attrField);
                attrField = doc.CreateAttribute("cname");
                attrField.InnerXml = fieldDesc;
                fieldNode.Attributes.Append(attrField);
                //加到表结点作为子结点
                tableNode.AppendChild(fieldNode);
            }
            xPath = "/Tables";
            nodelist = doc.SelectNodes(xPath);
            if (nodelist.Count > 0)
            {
                //加到根结点下
                nodelist[0].AppendChild(tableNode);
            }
            //保存
            doc.Save(destFile);
        }

        /// <summary>
        /// 修改模板
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="modalName"></param>
        /// <param name="listBoxRight"></param>
        public static void editExportModal(string userName, string tableName, string modalName, ListBox listBoxRight)
        {
            //先删除
            ReportData.deleteExportModal(userName, modalName);
            //后添加
            ReportData.addExportModal(userName, tableName, modalName, listBoxRight);
        }

        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="modalName"></param>
        public static void deleteExportModal(string userName, string modalName)
        {
            //目标文件
            string destFile = ReportData.getExportModalDiskFileName(userName);
            if (System.IO.File.Exists(destFile))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(destFile);
                string xPath = "//Table[@cname='" + modalName + "']";
                XmlNodeList nodelist = doc.SelectNodes(xPath);
                foreach (XmlNode node in nodelist)
                {
                    XmlNode delNode = node.ParentNode;
                    delNode.RemoveChild(node);
                }
                doc.Save(destFile);
            }
        }

        public static bool saveConstrastDataToXMLFile(string userName, string tableName, string guid, string cname, string contrastValue)
        {
            //目标文件
            string destFile = ReportData.getContrastDataDiskFileName(userName);
            XmlDocument doc = new XmlDocument();
            if (System.IO.File.Exists(destFile) == false)
            {
                //文件不存在,则先创建
                //创建XML文件
                //加入XML的声明段落
                XmlDeclaration declarationDoc = doc.CreateXmlDeclaration("1.0", "utf-8", "");
                doc.AppendChild(declarationDoc);
                //创建一个根元素
                XmlElement elementRoot = doc.CreateElement("Tables");
                doc.AppendChild(elementRoot);
                doc.Save(destFile);
            }
            //重新加载XML文件
            doc.Load(destFile);
            //先判断是否存在改表的配置
            string xPath = "//Table[@ename='" + tableName + "']";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            if (nodelist.Count < 1)
            {
                //创建表结点
                //创建模板结点(表结点)
                XmlNode tableNode = doc.CreateNode(XmlNodeType.Element, "Table", "");
                //表结点的属性
                //表名称
                XmlAttribute attr = doc.CreateAttribute("ename");
                attr.InnerXml = tableName;
                tableNode.Attributes.Append(attr);
                string tableDesc = "";
                string pkFieldName = "";
                //string refFieldName = "";
                switch (tableName.ToLower())
                {
                    case "testitem":
                        tableDesc = "项目名称";
                        pkFieldName = "ItemNo";
                        //refFieldName = "";
                        break;
                    default:
                        tableDesc = tableName;
                        pkFieldName = "id";
                        //refFieldName = "name";
                        break;
                }
                //表描述
                attr = doc.CreateAttribute("cname");
                attr.InnerXml = tableDesc;
                tableNode.Attributes.Append(attr);
                //表的主键字段
                attr = doc.CreateAttribute("pkFieldName");
                attr.InnerXml = pkFieldName;
                tableNode.Attributes.Append(attr);
                xPath = "/Tables";
                nodelist = doc.SelectNodes(xPath);
                if (nodelist.Count > 0)
                {
                    //加到根结点下
                    nodelist[0].AppendChild(tableNode);
                }
                //保存
                doc.Save(destFile);
            }
            //重新定位到该表
            xPath = "//Table[@ename='" + tableName + "']";
            nodelist = doc.SelectNodes(xPath);
            if (nodelist.Count < 1)
            {
                return false;
            }
            xPath = "./item[@guid='" + guid + "']";
            XmlNodeList nodelistChild = nodelist[0].SelectNodes(xPath);
            if (nodelistChild.Count < 1)
            {
                //做添加
                if (contrastValue == "")
                    return true;
                //创建对照项目结点
                XmlNode nodeItem = doc.CreateNode(XmlNodeType.Element, "item", "");
                //主键
                XmlAttribute attrItem = doc.CreateAttribute("guid");
                attrItem.InnerXml = guid;
                nodeItem.Attributes.Append(attrItem);
                //原来的值
                attrItem = doc.CreateAttribute("cname");
                attrItem.InnerXml = cname;
                nodeItem.Attributes.Append(attrItem);
                //对照值
                attrItem = doc.CreateAttribute("value");
                attrItem.InnerXml = contrastValue;
                nodeItem.Attributes.Append(attrItem);
                //加到XML
                nodelist[0].AppendChild(nodeItem);
            }
            else
            {
                if (contrastValue == "")
                {
                    //做删除
                    XmlNode delNode = nodelistChild[0].ParentNode;
                    delNode.RemoveChild(nodelistChild[0]);
                }
                else
                {
                    //做修改cname
                    nodelistChild[0].Attributes["cname"].InnerXml = cname;
                    nodelistChild[0].Attributes["value"].InnerXml = contrastValue;
                }
            }
            doc.Save(destFile);
            return true;
        }

        /// <summary>
        /// 获取要报告数据的存放路径,如:report\年\月\日
        /// </summary>
        /// <returns></returns>
        public static string getReportDataSaveDiskPath()
        {
            //报告的统一路径
            string path = BaseFunction.getReportConfigPath();
            //报告的发送日期
            System.DateTime date = System.DateTime.Now;
            path += date.Year.ToString() + "\\" + date.Month.ToString() + "\\" + date.Day.ToString() + "\\";
            if (System.IO.Directory.Exists(path) == false)
            {
                System.IO.Directory.CreateDirectory(path);
            }
            return path;
        }
        /// <summary>
        /// 获取要报告数据的存放路径,如:report\年\月\日
        /// </summary>
        /// <returns></returns>
        public static string getReportDataSaveDiskPath(string ReceiveDate)
        {
            //报告的统一路径
            string path = BaseFunction.getReportConfigPath();
            //报告的发送日期
            if (!string.IsNullOrEmpty(ReceiveDate))
            {
                path += Convert.ToDateTime(ReceiveDate).Year.ToString() + "\\" + Convert.ToDateTime(ReceiveDate).Month.ToString() + "\\" + Convert.ToDateTime(ReceiveDate).Day.ToString() + "\\";
            }
            if (System.IO.Directory.Exists(path) == false)
            {
                System.IO.Directory.CreateDirectory(path);
            }
            return path;
        }

        /// <summary>
        /// 上传检验报告
        /// </summary>
        /// <param name="xmlData">xml数据</param>
        /// <param name="pdfdata">pdf检验报告</param>
        /// <param name="pdfdata_td">套打pdf检验报告</param>
        /// <param name="fileData">其他文件，例如jpg,frp，word,rtf等</param>
        /// <param name="fileType">其他文件的类型</param>
        /// <param name="errormsg">错误信息</param>
        /// <returns>
        /// 0 成功
        /// 负数:失败
        ///     -1:系统出现异常
        ///     -2:数据格式不合法等
        /// </returns>
        public static int UpLoadReportDataFromBytes(byte[] xmlData, byte[] pdfdata, byte[] pdfdata_td, byte[] fileData, string fileType, out string errorMsg)
        {
            string msg = "调用方法“UpLoadReportDataFromBytes”上传检验报告!\r\n";
            Log.Info(msg);

            int result = -2;
            errorMsg = "";
            //XML文件
            if (xmlData == null)
            {
                errorMsg = "上传失败:没有数据,即xmldata数据为null";
                Log.Error(errorMsg);
                return result;
            }
            try
            {
                //报告数据存放的目录
                string reportSavePath = ReportData.getReportDataSaveDiskPath();
                //分析xml数据
                System.Text.UTF8Encoding converter = new UTF8Encoding();
                string xml = converter.GetString(xmlData);
                if (xml == "")
                {
                    errorMsg = "\r\n上传失败:没有数据,即xmldata数据为空串!";
                    Log.Error(errorMsg);
                    return result;
                }
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                string xPath = "//ReportForm";
                XmlNodeList nodelist = doc.SelectNodes(xPath);
                if (nodelist.Count != 1)
                {
                    errorMsg = "\r\n上传失败:上传的XML数据格式不符合规定!\r\nReportForm节点必须存在,且只能有一个!";
                    Log.Error(errorMsg + xml);
                    return result;
                }
                XmlNode xmlNode = nodelist[0];
                Hashtable hashReportForm = clsCommon.GetXMLData.getXmlNodeNameAndValue(xmlNode);
                //生成统一的文件名称
                string fileNameUnique = "";
                if (hashReportForm["REPORTFORMID"] == null)
                {
                    errorMsg = "\r\n上传失败:上传的XML数据格式不符合规定!没有REPORTFORMID字段!\r\n";
                    Log.Error(errorMsg + xml);
                    //应该退出程序!!!!
                    return result;
                }
                else
                {
                    fileNameUnique = hashReportForm["REPORTFORMID"].ToString();
                    if (fileNameUnique == "")
                    {
                        errorMsg = "\r\n上传失败:上传的XML数据格式不符合规定!REPORTFORMID字段的内容为空!\r\n";
                        Log.Error(errorMsg + xml);
                        //应该退出程序!!!!
                        return result;
                    }
                }
                if (fileNameUnique == "")
                    fileNameUnique = System.Guid.NewGuid().ToString();

                fileNameUnique = fileNameUnique.Replace(":", "：");
                //xml数据要保存的文件名称
                string xmlFileName = reportSavePath + fileNameUnique + ".xml";
                msg = "保存xml数据到本地文件：\r\n" + xmlFileName;
                Log.Info(msg);
                //保存xml数据到本地文件
               clsCommon.Tools.writeStringToLocalFile(xmlFileName, xml);
                //保存到数据库
                Log.Info("pdfdata：" + pdfdata);
                //保存pdf文件
                string pdfFileName = "";
                //ZhiFang.Common.Public.ConfigHelper.GetConfigString("jpgorpdf");
                if (pdfdata != null)
                {
                    if (pdfdata.Length > 0)
                    {
                        pdfFileName = reportSavePath + fileNameUnique + ".pdf";
                        msg = "保存PDF数据到本地文件：\r\n" + pdfFileName;
                        Log.Info(msg);
                        //保存到本地文件
                        clsCommon.Tools.writeBytesToLocalFile(pdfFileName, pdfdata);
                    }
                    else
                    {
                        msg = "没有PDF数据！\r\n";
                        Log.Error(msg);
                        //return result;
                    }

                }
                Log.Info("pdfdata_td：" + pdfdata_td);
                //保存pdf文件(套打)
                string pdfFileNameTD = "";
                if (pdfdata_td != null)
                {
                    if (pdfdata_td.Length > 0)
                    {
                        pdfFileNameTD = reportSavePath + "T" + fileNameUnique + ".pdf";
                        msg = "保存套打的PDF数据到本地文件：\r\n" + pdfFileNameTD;
                        Log.Info(msg);
                        //保存
                        clsCommon.Tools.writeBytesToLocalFile(pdfFileNameTD, pdfdata_td);
                    }

                }
                Log.Info("fileData：" + fileData);
                //保存其他文件
                reportSavePath = ReportData.getReportDataSaveDiskPath(hashReportForm["RECEIVEDATE"].ToString());
                string pdfFileNameOther = "";
                if (fileData != null)
                {
                    if (fileData.Length > 0)
                    {
                        pdfFileNameOther = reportSavePath + fileNameUnique + "_QT." + fileType;
                        msg = "保存其他的图片数据到本地文件：\r\n" + pdfFileNameOther;
                        Log.Info(msg);
                        //保存
                        clsCommon.Tools.writeBytesToLocalFile(pdfFileNameOther, fileData);
                    }
                }
                //保存到数据库
                result = ReportData.saveReportDataToDB(xml, reportSavePath, pdfFileName, pdfFileNameTD, pdfFileNameOther, out errorMsg);
                if (result == 0)
                {
                    if (ConfigurationManager.ConnectionStrings["for301"].ToString() == "0")
                    {
                        string xPath1 = "//ReportItem";
                        XmlNodeList nodelist1 = doc.SelectNodes(xPath1);
                        XmlNode xmlNode1 = nodelist1[0];
                        Hashtable hashReportItem = clsCommon.GetXMLData.getXmlNodeNameAndValue(xmlNode1);

                        string userName = "glbl";
                        string pwd = "123456";
                        string token = "";
                        bool flag = false;
                        try
                        {
                            //CommonService服务引用不了，所以暂时注释 ganwh 2015-09-23
                            //CommonService.CommonServiceClient comm = new CommonService.CommonServiceClient();
                            do
                            {
                                //验证用户名,密码;获取令牌
                                //token = comm.authService(userName, pwd);
                                token = "1";//CommonService服务引用不了，测试添加
                                if (token == "0")
                                {
                                    Log.Info("获取令牌失败！token=" + token);
                                }
                                else
                                {
                                    //flag = comm.checkService(token);
                                    flag = true;//CommonService服务引用不了，测试添加
                                    if (flag)
                                    {
                                        try
                                        {
                                            string ReportFormID = fileNameUnique.Replace("00：00：00", "00:00:00");
                                            string url = ConfigurationManager.ConnectionStrings["URLValue"].ToString();
                                            // string url1 = "http://202.106.73.46/weblis/ReportPrint/ReportPrint_Weblis.aspx?ReportFormTitle=Center&ReportFormID=_183883_2_1_10000000_2013-04-11 00:00:00&PrintType=A5";
                                            string url1 = url + "?ReportFormTitle=Center&ReportFormID=" + ReportFormID + "&PrintType=A5";
                                            Log.Info("报告路径：" + url1);
                                            //byte[] b = System.Text.Encoding.Default.GetBytes(url1);
                                            //url1 = Convert.ToBase64String(b);
                                            StringBuilder sb = new StringBuilder();
                                            sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                                            sb.Append("<ns2:sampcp xmlns:ns2=\"http://model.service.sampcp.sinosoft.com/\">");
                                            //服务编号
                                            //string serviceid = "s297";
                                            string serviceid = hashReportForm["ZDY7"].ToString();
                                            sb.Append("<serviceidentity>" + serviceid + "</serviceidentity>");
                                            //服务消费者名称
                                            //string consumername = "huizhen";
                                            string consumername = hashReportForm["ZDY6"].ToString();
                                            sb.Append("<consumername>" + consumername + "</consumername>");
                                            //订单号
                                            //string bid = "20130412153715534001";
                                            string bid = hashReportForm["ZDY5"].ToString();
                                            sb.Append("<bid>" + bid + "</bid>");                                            //创建时间，格式：2010-09-19T17:05:49
                                            string nowdatetime = DateTime.Now.ToString();
                                            string dtcreate = Convert.ToDateTime(nowdatetime).ToString("yyyy-MM-dd") + "T" + Convert.ToDateTime(nowdatetime).ToString("HH:mm:ss");
                                            sb.Append("<dtCreate>" + dtcreate + "</dtCreate>");
                                            sb.Append("<serviceBody>");
                                            sb.Append("<ServiceInfo>");
                                            sb.Append("<serviceState>已发报告</serviceState>");
                                            //sb.Append("<serviceUrl>"+ url1+ "</serviceUrl>");
                                            sb.Append("<serviceUrl><![CDATA[" + url1 + "]]>" + "</serviceUrl>");
                                            sb.Append("<serviceInfDesc></serviceInfDesc>");
                                            sb.Append("<serviceFile></serviceFile>");
                                            sb.Append("</ServiceInfo>");
                                            sb.Append("</serviceBody></ns2:sampcp>");
                                            Log.Info("传入的xml字符串：" + sb.ToString());
                                            //引用不了ServiceInfoService服务，所以注释 ganwh 2015-09-23
                                            //ServiceInfoService.ServiceInfoServiceClient ServiceInfoService = new WL.ServiceInfoService.ServiceInfoServiceClient();
                                            //string[] str = ServiceInfoService.recordServiceInfo(token, sb.ToString());

                                            //Log.Info("返回信息：" + str[1]);
                                        }
                                        catch (Exception e)
                                        {
                                            Log.Debug("错误信息：" + e.ToString());
                                        }
                                    }
                                }
                            } while (!flag);
                            Log.Info("认证有效");
                        }
                        catch (Exception e)
                        {
                            Log.Info("错误信息：" + e.Data);
                        }
                    }
                    errorMsg = "上传文件成功!";
                }
                else
                    errorMsg = "上传文件失败!";
            }
            catch (System.Exception ex)
            {
                result = -1;
                errorMsg = "上传数据出错:" + ex.ToString();
                Log.Error(ex.ToString());
            }
            return result;
        }


        /// <summary>
        /// 更新上传报告状态
        /// </summary>
        /// <param name="barcode"></param>
        /// <param name="weblisflag"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static bool UpdateReportStatus(string barcode, string weblisflag, out string errmsg)
        {
            errmsg = "";
            bool r = false;
            try
            {
                string sql = "update BarCodeForm set WebLisFlag='" + weblisflag + "' where BarCode='" + barcode + "'";
                WL.BLL.DataConn.CreateDB().ExecuteNonQuery(sql);
                r = true;
            }
            catch (Exception ex)
            {
                r = false;
                errmsg = ex.Message;
            }
            return r;
        }

        /// <summary>
        /// 取主键的查询表达式,即where条件
        /// </summary>
        /// <param name="hashForm"></param>
        /// <returns></returns>
        public static string getReportDataPkWhereOLD(Hashtable hashForm)
        {
            string fieldWhereModal = "\"{0}\"='{1}'";
            string pkWhere = "";
            if (pkWhere != "")
                pkWhere += " AND ";
            pkWhere += string.Format(fieldWhereModal, "RECEIVEDATE", hashForm["RECEIVEDATE"].ToString());
            if (pkWhere != "")
                pkWhere += " AND ";
            pkWhere += string.Format(fieldWhereModal, "SECTIONNO", hashForm["SECTIONNO"].ToString());
            if (pkWhere != "")
                pkWhere += " AND ";
            pkWhere += string.Format(fieldWhereModal, "TESTTYPENO", hashForm["TESTTYPENO"].ToString());
            if (pkWhere != "")
                pkWhere += " AND ";
            pkWhere += string.Format(fieldWhereModal, "SAMPLENO", hashForm["SAMPLENO"].ToString());
            return pkWhere;
        }

        /// <summary>
        /// 取主键的查询表达式,即where条件
        /// </summary>
        /// <param name="hashForm"></param>
        /// <returns></returns>
        public static string getReportDataPkWhere(Hashtable hashForm)
        {
            string pkFieldName = "ReportFormID".ToUpper();
            string fieldWhereModal = "\"{0}\"='{1}'";
            string pkWhere = "";
            if (hashForm[pkFieldName] != null)
            {
                string pkFieldValue = hashForm[pkFieldName].ToString().Trim();
                if (pkFieldValue != "")
                {
                    if (pkWhere != "")
                        pkWhere += " AND ";
                    pkWhere += string.Format(fieldWhereModal, pkFieldName, pkFieldValue);
                }
            }


            return pkWhere;
        }

        /// <summary>
        /// 从数据库里获取某个表的字段名称列表(转换成大写)
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getTableColumnNameList(string tableName)
        {
            string msg = "调用方法“getTableColumnNameList”从数据库里获取某个表的字段名称列表(转换成大写)!\r\n";
            Log.Info(msg);
            //查询一条记录
            string sqlModal = "SELECT * FROM \"{0}\" WHERE {1}";
            string sql = "";
            sql = string.Format(sqlModal, tableName, "1>2");
            msg = "运行SQL语句：\r\n" + sql;
            Log.Info(msg);
            DataSet ds = WL.BLL.DataConn.CreateDB().ExecDS(sql);
            DataTable dtColumn = ds.Tables[0];
            //取当前数据库表的字典到哈希表
            Hashtable hashColumn = new Hashtable();
            for (int i = 0; i < dtColumn.Columns.Count; i++)
            {
                //字段名称:用大写
                string fieldName = dtColumn.Columns[i].ColumnName.ToUpper();
                if (hashColumn[fieldName] == null)
                    hashColumn.Add(fieldName, fieldName);
            }
            msg = "成功！返回哈希表！！";
            Log.Info(msg);
            return hashColumn;
        }

        /// <summary>
        /// 保存报告到数据库
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="reportSavePath"></param>
        /// <param name="pdfFileName"></param>
        /// <param name="pdfFileNameTD"></param>
        /// <param name="pdfFileNameOther"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public static int saveReportDataToDB(string xml, string reportSavePath, string pdfFileName, string pdfFileNameTD, string pdfFileNameOther, out string errorMsg)
        {
            string msg = "调用方法“saveReportDataToDB”保存报告到数据库!\r\n";
            Log.Info(msg);

            int result = -1;
            errorMsg = "";
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                string xPath = "//ReportForm";
                XmlNodeList nodelist = doc.SelectNodes(xPath);
                //ReportForm节点必须存在,且只能有一个
                if (nodelist.Count != 1)
                {
                    errorMsg = "\r\n上传的XML数据格式不符合规定!ReportForm节点必须存在,且只能有一个\r\n";
                    Log.Error(errorMsg + xml);
                    return result;
                }
                XmlNode reportFormNodeSave = nodelist[0];
                //第一级是保存到ReportFormFull
                string tableNameForm = "ReportFormFull";
                //第二级是保存到ReportItemFull(生化，免疫),ReportMarrowFull(细胞学),ReportMicroFull（微生物）
                string tableNameItem = "ReportItemFull";
                string tableNameItemXMLData = "ReportItem";
                //字段列表
                Hashtable hashFormColumn = WL.Common.ReportData.getTableColumnNameList(tableNameForm);

                //从传入的XML数据取子表的名称，根据子表名称来确定报告项目的表名称，我们一般在原来的表名称基础上加Full,
                //字段就比原来的多加两个(REPORTFORMID,REPORTITEMID)
                xPath = "/WebReportFile";
                nodelist = doc.SelectNodes(xPath);
                if (nodelist.Count != 1)
                {
                    errorMsg = "\r\n上传的XML数据格式不符合规定!必须以WebReportFile节点作为根节点,且只能有一个\r\n";
                    Log.Error(errorMsg + xml);
                    return result;
                }
                foreach (XmlNode xmlNode in nodelist[0].ChildNodes)
                {
                    string nodeName = xmlNode.Name.ToLower();
                    if (nodeName == "reportform")
                        continue;
                    //由于子表数据可能是多个，所以只取第一个就行
                    bool isGet = false;
                    switch (nodeName)
                    {
                        case "reportitem"://生化，免疫
                            tableNameItem = "ReportItemFull";
                            tableNameItemXMLData = "ReportItem";
                            isGet = true;
                            break;
                        case "reportmarrow"://细胞学
                            tableNameItem = "ReportMarrowFull";
                            tableNameItemXMLData = "ReportMarrow";
                            isGet = true;
                            break;
                        case "reportmicro"://微生物
                            tableNameItem = "ReportMicroFull";
                            tableNameItemXMLData = "ReportMicro";
                            isGet = true;
                            break;
                    }
                    if (isGet == true)
                        break;
                }
                //字段列表
                Hashtable hashItemColumn = WL.Common.ReportData.getTableColumnNameList(tableNameItem);

                //取主表的字段名称和内容
                Hashtable hashForm = clsCommon.GetXMLData.getXmlNodeNameAndValue(reportFormNodeSave);
                //转换字段名称为大写
                hashForm = clsCommon.ConvertData.convertHashKeyToUpper(hashForm);

                //取XML数据表对应的主键(唯一索引)
                string pkWhere = ReportData.getReportDataPkWhere(hashForm);
                if (pkWhere == "")
                {
                    errorMsg = "\r\n上传的XML数据格式不符合规定!没有主键字段ReportFormID或者该字段没有内容！\r\n";
                    Log.Error(errorMsg + xml);
                    return result;
                }
                //先删除以前的数据
                string sqlModal = "DELETE FROM \"{0}\" WHERE {1}";
                string sql = "";
                //先删除子表
                sql = string.Format(sqlModal, tableNameItem, pkWhere);
                msg = "运行SQL语句删除子表（项目表）：\r\n" + sql;
                Log.Info(msg);

                WL.BLL.DataConn.CreateDB().ExecuteNonQuery(sql);
                //再删除主表
                sql = string.Format(sqlModal, tableNameForm, pkWhere);
                msg = "运行SQL语句删除主表（报告表）：\r\n" + sql;
                Log.Info(msg);

                WL.BLL.DataConn.CreateDB().ExecuteNonQuery(sql);

                //插入新数据到数据库中
                //本次上传记录的主键
                string guid = System.Guid.NewGuid().ToString();
                if (hashForm["REPORTFORMID"] == null)
                    hashForm.Add("REPORTFORMID", "");
                else
                {
                    string pkFieldValue = hashForm["REPORTFORMID"].ToString().Trim();
                    if (pkFieldValue == "")
                    {
                        //主键自动生成
                        hashForm["REPORTFORMID"] = guid;
                    }
                    else
                    {
                        //主键取传进来的
                        guid = pkFieldValue;
                    }
                }
                //加新生成的PDF文件名称等
                if (hashForm["PDFFILE"] == null)
                    hashForm.Add("PDFFILE", "");
                string basePath = System.AppDomain.CurrentDomain.BaseDirectory;
                //D:\WebLis\WebLis\WebLis\report\2009\12\23\_569_6_1_92_2009-09-15 00：00：00.pdf
                //hashForm["PDFFILE"] = pdfFileName.Replace(basePath, "");//相对路径
                if (pdfFileName != null && pdfFileName != "")
                {
                    hashForm["PDFFILE"] = pdfFileName.Substring(pdfFileName.IndexOf("report"));//相对路径
                }
                //hashForm["PDFFILE"] = pdfFileName;//绝对路径
                if (hashForm["JPGFILE"] == null)
                    hashForm.Add("JPGFILE", "");
                hashForm["JPGFILE"] = pdfFileNameOther.Replace(basePath, "");//相对路径
                //hashForm["JPGFILE"] = pdfFileNameOther;//绝对路径
                //将子表的名称保存到主表
                if (hashForm["CHILDTABLENAME"] == null)
                {
                    hashForm.Add("CHILDTABLENAME", tableNameItem);
                }
                else
                {
                    hashForm["CHILDTABLENAME"] = tableNameItem;
                }
                //先生成插入主表的SQL脚本
                string sqlForm = WL.Common.Tools.getInsertSQL(tableNameForm, hashFormColumn, hashForm);
                if (sqlForm != "")
                {
                    msg = "运行SQL语句插入主表数据（报告表）：\r\n" + sqlForm;
                    Log.Info(msg);

                    //插入主表数据
                    WL.BLL.DataConn.CreateDB().ExecuteNonQuery(sqlForm);
                }

                xPath = "//" + tableNameItemXMLData;
                nodelist = doc.SelectNodes(xPath);
                int itemID = 1;
                foreach (XmlNode xmlNode in nodelist)
                {
                    //取主表的字段名称和内容
                    Hashtable hashItem = clsCommon.GetXMLData.getXmlNodeNameAndValue(xmlNode);
                    //加外键
                    if (hashItem["REPORTFORMID"] == null)
                        hashItem.Add("REPORTFORMID", "");
                    hashItem["REPORTFORMID"] = guid;
                    //子表的ID
                    if (hashItem["REPORTITEMID"] == null || hashItem["REPORTITEMID"].ToString() == "")
                    {
                        hashItem.Add("REPORTITEMID", "");
                        hashItem["REPORTITEMID"] = itemID++;// System.Guid.NewGuid().ToString();
                    }
                    else
                    {
 
                    }
                    //先生成插入子表的SQL脚本
                    string sqlItem = WL.Common.Tools.getInsertSQL(tableNameItem, hashItemColumn, hashItem);
                    if (sqlItem != "")
                    {
                        msg = "运行SQL语句插入子表数据（项目表）：\r\n" + sqlItem;
                        Log.Info(msg);
                        //插入子表数据
                        WL.BLL.DataConn.CreateDB().ExecuteNonQuery(sqlItem);
                    }
                }

                msg = "成功！";
                Log.Info(msg);
                string isBarcode = ConfigurationManager.AppSettings["ReportIncludeImage"].ToString().Trim();
                //SERIALNO
                Log.Info("打标志开始");
                if (hashForm["SERIALNO"] != null)
                {
                    if (UpdateReportStatus(hashForm["SERIALNO"].ToString(), "10", out msg))
                    {
                        Log.Info(String.Format("打标志成功,barcode={0},weblisflag=10", hashForm["SERIALNO"].ToString()));
                    }
                    else
                    {
                        Log.Error("打标志失败");
                    }
                }
                else
                {
                    Log.Error("未找到条码数据");
                }
                string err;
                string classname = "UpLoadReportForm";
                if (System.Configuration.ConfigurationManager.AppSettings["classname"] != null && System.Configuration.ConfigurationManager.AppSettings["classname"].ToString().Trim() != "")
                {
                    classname = System.Configuration.ConfigurationManager.AppSettings["classname"].ToString().Trim();

                    IBUpLoadReportForm ibulrf = ZhiFang.BLLFactory.BLLFactory<IBUpLoadReportForm>.GetBLL(classname);
                    if (ibulrf != null)
                    {
                        if (!(ibulrf.UpLoadReportForm(xml, out err) > 0))
                        {
                            Log.Error(err + xml);
                            return -1;
                        }
                    }
                }
                //成功
                result = 0;
            }
            catch (System.Exception ex)
            {
                errorMsg = "\r\n上传失败:将数据保存到数据库是失败:\r\n" + ex.ToString();
                Log.Error(ex.ToString());
            }
            return result;
        }

        /// <summary>
        /// 下载某个文件(比如PDF文件),返回流
        /// </summary>
        /// <param name="fileName">对应下载XML数据格式的PDFFILE节点的内容</param>
        /// <returns></returns>
        public static byte[] downLoadPDFFileContent(string fileName)
        {
            string msg = "调用方法“downLoadPDFFileContent”下载某个文件(比如PDF文件),返回流";
            Log.Info(msg);

            byte[] pdfContent = null;
            string path = fileName;
            if (System.IO.File.Exists(path) == false)
            {
                //默认取上报的pdf等报表存放目录
                path = ConfigurationSettings.AppSettings["ReportConfigPath"] + fileName;
                //path = System.AppDomain.CurrentDomain.BaseDirectory + fileName;
            }
            if (System.IO.File.Exists(path))
            {
                System.IO.FileStream fileStream = new System.IO.FileStream(path, FileMode.Open);
                long fileSize = fileStream.Length;
                pdfContent = new byte[fileSize];
                fileStream.Read(pdfContent, 0, (int)fileSize);
                fileStream.Close();
            }
            else
            {
                msg = "文件“" + path + "”不存在！";
                Log.Info(msg);
            }
            return pdfContent;
        }

        /// <summary>
        /// 根据查询条件获取检验报告单ReportForm列表
        /// </summary>
        /// <param name="whereSQL">查询条件，如果是空串，则返回所有的数据，尽量不要传空串</param>
        /// <returns>XML</returns>
        public static string DownloadReportFormList(string whereSQL)
        {
            if (whereSQL == "")
                whereSQL = "2>1";
            //string sql = "select receivedate,cname,serialno,sectionno,sampleno,resultfile,printtimes,PDFFILE from reportformfull where " + whereSQL + " order by printtimes asc";
            string sql = "select * from reportformfull where " + whereSQL + " order by printtimes asc";
            Log.Info(sql);
            DataSet ds = WL.BLL.DataConn.CreateDB().ExecDS(sql);
            if (!ECDS.Common.Security.FormatTools.CheckDataSet(ds))
            {
                throw new Exception("未找到报告");
            }
            return ds.GetXml();
        }

        /// <summary>
        /// 根据查询条件获取检验报告单ReportForm和ReportItem列表
        /// <param name="whereSQL">查询条件，如果是空串，则返回所有的数据，尽量不要传空串</param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns>XML</returns>
        public static string DownloadReportFormItemList(string whereSQL, out string errorMsg)
        {

            string msg = "调用方法“DownloadReportFormItemList”根据查询条件获取检验报告单ReportForm和ReportItem列表!\r\n";
            Log.Info(msg);

            errorMsg = "";
            if (whereSQL == "")
                whereSQL = "2>1";
            //string sql = "select receivedate,cname,serialno,sectionno,sampleno,resultfile,printtimes,PDFFILE from reportformfull where " + whereSQL + " order by printtimes asc";
            string sql = "select * from reportformfull where " + whereSQL + " order by printtimes asc";
            msg = "运行SQL语句查询检验报告单：\r\n" + sql;
            Log.Info(msg);

            DataSet ds = WL.BLL.DataConn.CreateDB().ExecDS(sql);

            msg = "生成XML数据！";
            Log.Info(msg);

            //加入XML的声明段落
            XmlDocument doc = new XmlDocument();
            XmlDeclaration declarationDoc = doc.CreateXmlDeclaration("1.0", "utf-8", "");
            doc.AppendChild(declarationDoc);
            //遍历检验报告单，获取报告单的项目表名称，获取项目表内容,生产XML
            DataTable dt = ds.Tables[0];
            //创建一个根元素
            XmlElement elementRoot = doc.CreateElement("WebReportFile");

            for (int rowCount = 0; rowCount < dt.Rows.Count; rowCount++)
            {
                //创建表结点
                XmlNode tableNode = doc.CreateNode(XmlNodeType.Element, "ReportForm", "");
                string tableNameItem = "";
                string reportFormID = "";
                for (int colCount = 0; colCount < dt.Columns.Count; colCount++)
                {
                    string fieldName = dt.Columns[colCount].ToString().Trim();
                    string fieldValue = dt.Rows[rowCount][colCount].ToString().Trim();
                    //创建字段结点
                    XmlNode fieldNode = doc.CreateNode(XmlNodeType.Element, fieldName, "");
                    fieldNode.InnerXml = fieldValue;
                    tableNode.AppendChild(fieldNode);
                    if (fieldName.ToUpper() == "CHILDTABLENAME")
                    {
                        tableNameItem = fieldValue;
                    }
                    if (fieldName.ToUpper() == "REPORTFORMID")
                    {
                        reportFormID = fieldValue;
                    }
                }
                //取子表内容
                if ((reportFormID != "") && (tableNameItem != ""))
                {
                    string sqlModal = "SELECT * FROM {0} WHERE ReportFormID='{1}'";
                    string sqlChild = string.Format(sqlModal, tableNameItem, reportFormID);

                    msg = "运行SQL语句查询检验报告单的项目：\r\n" + sqlChild;
                    Log.Info(msg);

                    DataSet dsChild = WL.BLL.DataConn.CreateDB().ExecDS(sqlChild);
                    DataTable dtChild = dsChild.Tables[0];
                    for (int rowCountChild = 0; rowCountChild < dtChild.Rows.Count; rowCountChild++)
                    {
                        //创建表结点
                        XmlNode tableNodeChild = doc.CreateNode(XmlNodeType.Element, tableNameItem.Replace("Full", ""), "");
                        for (int colCountChild = 0; colCountChild < dtChild.Columns.Count; colCountChild++)
                        {
                            string fieldNameChild = dtChild.Columns[colCountChild].ToString().Trim();
                            string fieldValueChild = dtChild.Rows[rowCountChild][colCountChild].ToString().Trim();
                            //创建字段结点
                            XmlNode fieldNodeChild = doc.CreateNode(XmlNodeType.Element, fieldNameChild, "");
                            fieldNodeChild.InnerXml = fieldValueChild;
                            tableNodeChild.AppendChild(fieldNodeChild);
                        }
                        tableNode.AppendChild(tableNodeChild);
                    }

                }
                //将表节点加到根节点
                elementRoot.AppendChild(tableNode);
            }
            //加根节点到文档
            doc.AppendChild(elementRoot);

            msg = "成功！返回XML！！";
            Log.Info(msg);

            //doc.Save(@"D:\WebLis\WEBLIS2009\WEBLIS\Report\1.XML");
            return doc.OuterXml;
        }

        /// <summary>
        /// 根据ReportFormID获取检验报告单ReportForm和ReportItem列表
        /// </summary>
        /// <param name="ReportFormID"></param>
        /// <returns></returns>
        public static string DownloadReportFormItemListByReportFormID(string ReportFormID, out string errorMsg)
        {
            string msg = "调用方法“DownloadReportFormItemListByReportFormID”根据ReportFormID获取检验报告单ReportForm和ReportItem列表";
            Log.Info(msg);

            errorMsg = "";
            string whereSQL = "ReportFormID='" + ReportFormID + "'";
            return ReportData.DownloadReportFormItemList(whereSQL, out errorMsg);
        }

        /// <summary>
        /// 根据检验报告单的ID获取生化或免疫项目的数据列表XML
        /// </summary>
        /// <param name="ReportFormID">检验报告单的GUID</param>
        /// <returns></returns>
        public static string DownloadReportItemListByReportFormID(string ReportFormID)
        {
            string msg = "调用方法“DownloadReportItemListByReportFormID”根据检验报告单的ID获取生化或免疫项目的数据列表XML表";
            Log.Info(msg);

            string strSQl = "select * from ReportItemFull where  ReportFormID = '" + ReportFormID + "'";
            DataSet ds = WL.BLL.DataConn.CreateDB().ExecDS(strSQl);
            return ds.GetXml();

        }

        /// <summary>
        /// 根据检验报告单号获取检验报告PDF内容
        /// </summary>
        /// <param name="SerialNo">检验单号</param>
        /// <returns></returns>
        public static byte[] DownloadPDFFileBySerialNo(string SerialNo)
        {
            string msg = "调用方法“DownloadPDFFileBySerialNo”根据检验报告单号获取检验报告PDF内容";
            Log.Info(msg);

            string sql = "select PDFFILE from reportformfull where  SerialNo = '" + SerialNo + "'";
            string strFileName = "";

            msg = "运行SQL语句查询检验报告单：\r\n" + sql;
            Log.Info(msg);

            DataSet ds = WL.BLL.DataConn.CreateDB().ExecDS(sql);
            if (ECDS.Common.Security.FormatTools.CheckDataSet(ds))
            {
                strFileName = ds.Tables[0].Rows[0][0].ToString();
                return downLoadPDFFileContent(strFileName);
            }
            else
            {
                return null;
            }

        }

        public static byte[] DownloadReportFormBy_HisOrder_SerialNo(string whereSQL)
        {
            string strFileName = "";
            if (whereSQL == "")
                //whereSQL = "2>1";
                return null;
            //string sql = "select receivedate,cname,serialno,sectionno,sampleno,resultfile,printtimes,PDFFILE from reportformfull where " + whereSQL + " order by printtimes asc";
            string sql = "select * from reportformfull where " + whereSQL + " order by printtimes asc";
            Log.Info(sql);
            DataSet ds = WL.BLL.DataConn.CreateDB().ExecDS(sql);
            if (ECDS.Common.Security.FormatTools.CheckDataSet(ds))
            {
                strFileName = ds.Tables[0].Rows[0][0].ToString();
                return downLoadPDFFileContent(strFileName);
            }
            else
            {
                return null;
            }
        }

        ///// <summary>
        ///// 判断文件类型
        ///// </summary>
        ///// <param name="filePath">文件路径</param>
        ///// <param name="fileType">文件类型</param>
        ///// <returns>真或假</returns>
        //private static bool CheckFileType(string filePath, FileType fileType)
        //{

        //    int count = 2;
        //    byte[] buf = new byte[count];
        //    try
        //    {
        //        using (StreamReader sr = new StreamReader(filePath))
        //        {
        //            if (count != sr.BaseStream.Read(buf, 0, count))
        //            {
        //                return false;
        //            }
        //            else
        //            {
        //                if (buf == null || buf.Length < 2)
        //                {
        //                    return false;
        //                }

        //                if (fileType.ToString() == Enum.GetName(typeof(FileType), ReadByte(buf)))
        //                {
        //                    return true;
        //                }

        //                return false;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}

        ///// <summary>
        ///// 把byte[]转换为int
        ///// </summary>
        ///// <param name="bytes">byte[]</param>
        ///// <returns>int</returns>
        //private static int ReadByte(Byte[] bytes)
        //{
        //    StringBuilder str = new StringBuilder();
        //    for (int i = 0; i < bytes.Length; i++)
        //    {
        //        str.Append(bytes[i]);
        //    }

        //    return Convert.ToInt32(str.ToString());
        //}

        ///// <summary>
        ///// 文件类型
        ///// </summary>
        //public enum FileType
        //{
        //    //TXT=100115,
        //    JPG = 255216,
        //    PNG = 13780,
        //    BMP = 6677,
        //    EXE = 7790
        //}
    }
}
