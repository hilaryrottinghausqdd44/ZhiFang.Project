using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data;
using System.IO;
using System.Web;

namespace ZhiFang.WeiXin.Common
{
    public class TransDataToXML
    {
        /// <summary>
        /// 将DataTable转换成Xml文件
        /// </summary>
        /// <param name="dt">要转换的DataTable</param>
        /// <param name="recordsstr">记录节点</param>
        /// <returns></returns>
        public static XmlDocument TransformDTIntoXML(DataTable dt,  string recordsstr)
        {
            try
            {
                XmlDocument xd = new XmlDocument();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    XmlElement xrecordsstr = xd.CreateElement(recordsstr);

                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        XmlElement xc = xd.CreateElement(dt.Columns[j].ColumnName);
                        //xc.InnerText = HttpUtility.HtmlEncode(dt.Rows[i][j].ToString());
                        xc.InnerText = dt.Rows[i][j].ToString();
                        xrecordsstr.AppendChild(xc);
                    }
                    xd.AppendChild(xrecordsstr);
                }
                return xd;
            }
            catch (Exception e)
            {
                return new XmlDocument();
            }
        }
        /// <summary>
        /// 将DataTable转换成Xml文件
        /// </summary>
        /// <param name="dt">要转换的DataTable</param>
        /// <param name="rootstr">根节点</param>
        /// <param name="recordsstr">记录节点</param>
        /// <returns></returns>
        public static XmlDocument TransformDTIntoXML(DataTable dt, string rootstr, string recordsstr)
        {
            try
            {
                XmlDocument xd = new XmlDocument();
                XmlElement xroot=xd.CreateElement(rootstr);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    XmlElement xrecordsstr=xd.CreateElement(recordsstr);

                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        XmlElement xc = xd.CreateElement(dt.Columns[j].ColumnName);
                        //xc.InnerText = HttpUtility.HtmlEncode(dt.Rows[i][j].ToString());
                        xc.InnerText = dt.Rows[i][j].ToString();
                        xrecordsstr.AppendChild(xc);
                    }
                    xroot.AppendChild(xrecordsstr);
                }
                xd.AppendChild(xroot);
                return xd;
            }
            catch (Exception e)
            {
                return new XmlDocument();
            }
        }
        /// <summary>
        /// 合并两个XML
        /// </summary>
        /// <param name="x1">XML1</param>
        /// <param name="x2">XML2</param>
        /// <param name="rootstr">根节点名称</param>
        /// <returns>XmlDocument</returns>
        public static XmlDocument MergeXML(XmlDocument x1, XmlDocument x2, string rootstr)
        {
            try
            {
                XmlDocument xd = new XmlDocument();
                XmlNode xroot = xd.CreateElement(rootstr);

                if (x1.FirstChild.Name == rootstr && x2.FirstChild.Name == rootstr)
                {

                    for (int i = 0; i < x1.DocumentElement.ChildNodes.Count; i++)
                    {
                        XmlNode xroot1 = xd.ImportNode(x1.DocumentElement.ChildNodes[i], true);

                        xroot.AppendChild(xroot1);
                    }
                    for (int i = 0; i < x2.DocumentElement.ChildNodes.Count; i++)
                    {
                        XmlNode xroot2 = xd.ImportNode(x2.DocumentElement.ChildNodes[i], true);
                        xroot.AppendChild(xroot2);
                    }
                }
                xd.AppendChild(xroot);
                return xd;
            }
            catch (Exception e)
            {
                return new XmlDocument();
            }
        }
        /// <summary>
        /// 将DataTable转换成Xml文件
        /// </summary>
        /// <param name="dt">要转换的DataTable</param>
        /// <param name="strXMLName">生成xml的路径</param>
        /// <returns>成功true</returns>
        public static bool TransformDTIntoXML(DataTable dt, string strXMLName, string rootstr, string recordsstr)
        {
            try
            {
                XmlTextWriter xtw = new XmlTextWriter(strXMLName, null);
                xtw.WriteStartDocument();
                xtw.WriteStartElement(rootstr);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    xtw.WriteStartElement(recordsstr);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        xtw.WriteStartElement(dt.Columns[j].ColumnName);
                        //xtw.WriteString(HttpUtility.HtmlEncode(dt.Rows[i][j].ToString()));
                        xtw.WriteString(dt.Rows[i][j].ToString());
                        xtw.WriteEndElement();
                    }
                    xtw.WriteEndElement();
                }
                xtw.WriteEndElement();
                xtw.Close();
                return true;
            }
            catch (Exception e)
            {
                e = null;
                return false;
            }
        }
        /**/
        /// <summary>
        /// 将DataTable对象转换成XML字符串
        /// </summary>
        /// <param name="dt">DataTable对象</param>
        /// <returns>XML字符串</returns>
        public static string CDataToXml(DataTable dt)
        {
            if (dt != null)
            {
                MemoryStream ms = null;
                XmlTextWriter XmlWt = null;
                try
                {
                    ms = new MemoryStream();
                    //根据ms实例化XmlWt
                    XmlWt = new XmlTextWriter(ms, Encoding.Unicode);
                    //获取ds中的数据
                    dt.WriteXml(XmlWt);
                    int count = (int)ms.Length;
                    byte[] temp = new byte[count];
                    ms.Seek(0, SeekOrigin.Begin);
                    ms.Read(temp, 0, count);
                    //返回Unicode编码的文本
                    UnicodeEncoding ucode = new UnicodeEncoding();
                    string returnValue = ucode.GetString(temp).Trim();
                    return returnValue;
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    //释放资源
                    if (XmlWt != null)
                    {
                        XmlWt.Close();
                        ms.Close();
                        ms.Dispose();
                    }
                }
            }
            else
            {
                return "";
            }
        }
        /**/
        /// <summary>
        /// 将DataSet对象中指定的Table转换成XML字符串
        /// </summary>
        /// <param name="ds">DataSet对象</param>
        /// <param name="tableIndex">DataSet对象中的Table索引</param>
        /// <returns>XML字符串</returns>
        public static string CDataToXml(DataSet ds, int tableIndex)
        {
            if (tableIndex != -1)
            {
                return CDataToXml(ds.Tables[tableIndex]);
            }
            else
            {
                return CDataToXml(ds.Tables[0]);
            }
        }
        /**/
        /// <summary>
        /// 将DataSet对象转换成XML字符串
        /// </summary>
        /// <param name="ds">DataSet对象</param>
        /// <returns>XML字符串</returns>
        public static string CDataToXml(DataSet ds)
        {
            return CDataToXml(ds, -1);
        }
        /**/
        /// <summary>
        /// 将DataView对象转换成XML字符串
        /// </summary>
        /// <param name="dv">DataView对象</param>
        /// <returns>XML字符串</returns>
        public static string CDataToXml(DataView dv)
        {
            return CDataToXml(dv.Table);
        }
        /**/
        /// <summary>
        /// 将DataSet对象数据保存为XML文件
        /// </summary>
        /// <param name="dt">DataSet</param>
        /// <param name="xmlFilePath">XML文件路径</param>
        /// <returns>bool值</returns>
        public static bool CDataToXmlFile(DataTable dt, string xmlFilePath)
        {
            if ((dt != null) && (!string.IsNullOrEmpty(xmlFilePath)))
            {
                string path = xmlFilePath;
                MemoryStream ms = null;
                XmlTextWriter XmlWt = null;
                try
                {
                    ms = new MemoryStream();
                    //根据ms实例化XmlWt
                    XmlWt = new XmlTextWriter(ms, Encoding.Unicode);
                    //获取ds中的数据
                    dt.WriteXml(XmlWt);
                    int count = (int)ms.Length;
                    byte[] temp = new byte[count];
                    ms.Seek(0, SeekOrigin.Begin);
                    ms.Read(temp, 0, count);
                    //返回Unicode编码的文本
                    UnicodeEncoding ucode = new UnicodeEncoding();
                    //写文件
                    StreamWriter sw = new StreamWriter(path);
                    sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                    sw.WriteLine(ucode.GetString(temp).Trim());
                    sw.Close();
                    return true;
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    //释放资源
                    if (XmlWt != null)
                    {
                        XmlWt.Close();
                        ms.Close();
                        ms.Dispose();
                    }
                }
            }
            else
            {
                return false;
            }
        }
        /**/
        /// <summary>
        /// 将DataSet对象中指定的Table转换成XML文件
        /// </summary>
        /// <param name="ds">DataSet对象</param>
        /// <param name="tableIndex">DataSet对象中的Table索引</param>
        /// <param name="xmlFilePath">xml文件路径</param>
        /// <returns>bool]值</returns>
        public static bool CDataToXmlFile(DataSet ds, int tableIndex, string xmlFilePath)
        {
            if (tableIndex != -1)
            {
                return CDataToXmlFile(ds.Tables[tableIndex], xmlFilePath);
            }
            else
            {
                return CDataToXmlFile(ds.Tables[0], xmlFilePath);
            }
        }
        /**/
        /// <summary>
        /// 将DataSet对象转换成XML文件
        /// </summary>
        /// <param name="ds">DataSet对象</param>
        /// <param name="xmlFilePath">xml文件路径</param>
        /// <returns>bool]值</returns>
        public static bool CDataToXmlFile(DataSet ds, string xmlFilePath)
        {
            return CDataToXmlFile(ds, -1, xmlFilePath);
        }
        public static bool CDataSetToXmlFile(DataSet ds, string xmlFilePath)
        {
            try
            {
                string strElementName = "Row";
                if (ds.Tables[0].TableName.Trim() != "")
                    strElementName = ds.Tables[0].TableName.Trim();
                //初始化一个xml实例  
                XmlDocument XmlDoc = new XmlDocument();
                XmlNode xmlnode = XmlDoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
                XmlDoc.AppendChild(xmlnode);
                //创建xml的根节点  
                XmlElement rootElement = XmlDoc.CreateElement("Rows");
                //将根节点加入到xml文件中（AppendChild）  
                XmlDoc.AppendChild(rootElement);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    XmlElement xmlRow = XmlDoc.CreateElement(strElementName);
                    rootElement.AppendChild(xmlRow);
                    foreach (DataColumn col in ds.Tables[0].Columns)
                    {
                        XmlElement xmlCol = XmlDoc.CreateElement(col.ColumnName);

                        if (dr[col].ToString() != null && dr[col].ToString() != "")
                        {
                            xmlCol.InnerText = dr[col].ToString();
                        }
                        xmlRow.AppendChild(xmlCol);
                    }
                }
                XmlDoc.Save(xmlFilePath);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /**/
        /// <summary>
        /// 将DataView对象转换成XML文件
        /// </summary>
        /// <param name="dv">DataView对象</param>
        /// <param name="xmlFilePath">xml文件路径</param>
        /// <returns>bool]值</returns>
        public static bool CDataToXmlFile(DataView dv, string xmlFilePath)
        {
            return CDataToXmlFile(dv.Table, xmlFilePath);
        }
    }


    /**/
    /// <summary>
    /// XML形式的字符串、XML文江转换成DataSet、DataTable格式
    /// </summary>
    public class XmlToData
    {
        /**/
        /// <summary>
        /// 将Xml内容字符串转换成DataSet对象
        /// </summary>
        /// <param name="xmlStr">Xml内容字符串</param>
        /// <returns>DataSet对象</returns>
        public static DataSet CXmlToDataSet(string xmlStr)
        {
            if (!string.IsNullOrEmpty(xmlStr))
            {
                StringReader StrStream = null;
                XmlTextReader Xmlrdr = null;
                try
                {
                    DataSet ds = new DataSet();
                    //读取字符串中的信息
                    StrStream = new StringReader(xmlStr);
                    //获取StrStream中的数据
                    Xmlrdr = new XmlTextReader(StrStream);
                    //ds获取Xmlrdr中的数据                
                    ds.ReadXml(Xmlrdr);
                    return ds;
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    //释放资源
                    if (Xmlrdr != null)
                    {
                        Xmlrdr.Close();
                        StrStream.Close();
                        StrStream.Dispose();
                    }
                }
            }
            else
            {
                return null;
            }
        }
        /**/
        /// <summary>
        /// 将Xml字符串转换成DataTable对象
        /// </summary>
        /// <param name="xmlStr">Xml字符串</param>
        /// <param name="tableIndex">Table表索引</param>
        /// <returns>DataTable对象</returns>
        public static DataTable CXmlToDatatTable(string xmlStr, int tableIndex)
        {
            return CXmlToDataSet(xmlStr).Tables[tableIndex];
        }
        /**/
        /// <summary>
        /// 将Xml字符串转换成DataTable对象
        /// </summary>
        /// <param name="xmlStr">Xml字符串</param>
        /// <returns>DataTable对象</returns>
        public static DataTable CXmlToDatatTable(string xmlStr)
        {
            return CXmlToDataSet(xmlStr).Tables[0];
        }
        /**/
        /// <summary>
        /// 读取Xml文件信息,并转换成DataSet对象
        /// </summary>
        /// <remarks>
        /// DataSet ds = new DataSet();
        /// ds = CXmlFileToDataSet("/XML/upload.xml");
        /// </remarks>
        /// <param name="xmlFilePath">Xml文件地址</param>
        /// <returns>DataSet对象</returns>
        public static DataSet CXmlFileToDataSet(string xmlFilePath)
        {
            if (!string.IsNullOrEmpty(xmlFilePath))
            {
                string path = HttpContext.Current.Server.MapPath(xmlFilePath);
                StringReader StrStream = null;
                XmlTextReader Xmlrdr = null;
                try
                {
                    XmlDocument xmldoc = new XmlDocument();
                    //根据地址加载Xml文件
                    xmldoc.Load(path);

                    DataSet ds = new DataSet();
                    //读取文件中的字符流
                    StrStream = new StringReader(xmldoc.InnerXml);
                    //获取StrStream中的数据
                    Xmlrdr = new XmlTextReader(StrStream);
                    //ds获取Xmlrdr中的数据
                    ds.ReadXml(Xmlrdr);
                    return ds;
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    //释放资源
                    if (Xmlrdr != null)
                    {
                        Xmlrdr.Close();
                        StrStream.Close();
                        StrStream.Dispose();
                    }
                }
            }
            else
            {
                return null;
            }
        }
        /**/
        /// <summary>
        /// 读取Xml文件信息,并转换成DataTable对象
        /// </summary>
        /// <param name="xmlFilePath">xml文江路径</param>
        /// <param name="tableIndex">Table索引</param>
        /// <returns>DataTable对象</returns>
        public static DataTable CXmlToDataTable(string xmlFilePath, int tableIndex)
        {
            return CXmlFileToDataSet(xmlFilePath).Tables[tableIndex];
        }
        /**/
        /// <summary>
        /// 读取Xml文件信息,并转换成DataTable对象
        /// </summary>
        /// <param name="xmlFilePath">xml文江路径</param>
        /// <returns>DataTable对象</returns>
        public static DataTable CXmlToDataTable(string xmlFilePath)
        {
            return CXmlFileToDataSet(xmlFilePath).Tables[0];
        }
        // 相应C#代码： 
        private string ConvertDataTableToXML(DataTable xmlDS)
        {
            MemoryStream stream = null;
            XmlTextWriter writer = null;
            try
            {
                stream = new MemoryStream();
                writer = new XmlTextWriter(stream, Encoding.Default);
                xmlDS.WriteXml(writer);
                int count = (int)stream.Length;
                byte[] arr = new byte[count];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(arr, 0, count);
                UTF8Encoding utf = new UTF8Encoding();
                return utf.GetString(arr).Trim();
            }
            catch
            {
                return String.Empty;
            }
            finally
            {
                if (writer != null) writer.Close();
            }
        }

        private DataSet ConvertXMLToDataSet(string xmlData)
        {
            StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                DataSet xmlDS = new DataSet();
                stream = new StringReader(xmlData);
                reader = new XmlTextReader(stream);
                xmlDS.ReadXml(reader);
                return xmlDS;
            }
            catch (Exception ex)
            {
                string strTest = ex.Message;
                return null;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

        }
            /*
            /// <summary>
            /// 将DataTable转换成Xml文件,支持分页
            /// </summary>
            /// <param name="dt">要转换的DataTable</param>
            /// <param name="strXMLName">生成xml的路径</param>
            /// <param name="intPageSize">分页页面尺寸</param>
            /// <returns>成功true</returns>
            private int TransformDTIntoXML(DataTable dt, string strXMLName, int intPageSize)
            {
                try
                {
                    int pno = dt.Rows.Count / intPageSize;
                    if (dt.Rows.Count % intPageSize > 0)
                    {
                        pno++;
                    }
                    int countstart = 0;
                    int countend = dt.Rows.Count;
                    int nowcount = dt.Rows.Count;
                    for (int k = 0; k < pno; k++)
                    {
                        XmlTextWriter xtw = new XmlTextWriter(processFileName(k, strXMLName), null);
                        int page = k + 1;
                        if (page * intPageSize < nowcount)
                        {
                            countstart = (page - 1) * intPageSize;
                            countend = page * intPageSize;
                        }
                        else if ((page == 1) && (page * intPageSize > nowcount))
                        {
                            countstart = 0;
                            countend = nowcount;
                        }
                        else
                        {
                            countstart = (page - 1) * intPageSize;
                            countend = nowcount;
                        }
                        xtw.WriteStartDocument();
                        xtw.WriteStartElement("records");
                        for (int i = countstart; i < countend; i++)
                        {
                            xtw.WriteStartElement("record");
                            xtw.WriteStartElement("RecordSort");
                            xtw.WriteString((i + 1).ToString());
                            xtw.WriteEndElement();
                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                xtw.WriteStartElement(dt.Columns[j].ColumnName);
                                xtw.WriteString(dt.Rows[i][j].ToString());
                                xtw.WriteEndElement();
                            }
                            xtw.WriteEndElement();
                        }
                        xtw.WriteStartElement("pagecontrol");
                        xtw.WriteStartElement("recordcount");
                        xtw.WriteString(Convert.ToString(nowcount));
                        xtw.WriteEndElement();
                        xtw.WriteStartElement("firstpage");
                        xtw.WriteString(strXMLName.Replace(".xml", ".html").Substring(strXMLName.Replace(".xml", ".html").LastIndexOf('\\') + 1, strXMLName.Replace(".xml", ".html").Length - strXMLName.Replace(".xml", ".html").LastIndexOf('\\') - 1));
                        xtw.WriteEndElement();
                        xtw.WriteStartElement("previouspage");
                        string aaa = previousFileName(k, strXMLName);
                        xtw.WriteString(aaa.Substring(aaa.Replace(".xml", ".html").LastIndexOf('\\') + 1, aaa.Replace(".xml", ".html").Length - aaa.Replace(".xml", ".html").LastIndexOf('\\') - 1));
                        xtw.WriteEndElement();
                        xtw.WriteStartElement("nextpage");
                        aaa = nextFileName(k, strXMLName, pno);
                        xtw.WriteString(aaa.Substring(aaa.Replace(".xml", ".html").LastIndexOf('\\') + 1, aaa.Replace(".xml", ".html").Length - aaa.Replace(".xml", ".html").LastIndexOf('\\') - 1));
                        xtw.WriteEndElement();
                        xtw.WriteStartElement("lastpage");
                        aaa = processFileNameHtml(pno - 1, strXMLName.Replace(".xml", ".html"));
                        xtw.WriteString(aaa.Substring(aaa.Replace(".xml", ".html").LastIndexOf('\\') + 1, aaa.Replace(".xml", ".html").Length - aaa.Replace(".xml", ".html").LastIndexOf('\\') - 1));
                        xtw.WriteEndElement();
                        xtw.WriteStartElement("nowpage");
                        xtw.WriteString(Convert.ToString(k + 1));
                        xtw.WriteEndElement();
                        xtw.WriteStartElement("current");
                        aaa = processFileNameHtml(k, strXMLName.Replace(".xml", ".html"));
                        xtw.WriteString(aaa.Substring(aaa.Replace(".xml", ".html").LastIndexOf('\\') + 1, aaa.Replace(".xml", ".html").Length - aaa.Replace(".xml", ".html").LastIndexOf('\\') - 1));
                        xtw.WriteEndElement();
                        xtw.WriteEndElement();
                        xtw.WriteEndElement();
                        xtw.Close();
                    }
                    return pno;
                }
                catch (Exception e)
                {
                    e = null;
                    return -1;
                }
            }
            /// <summary>
            /// 将DataTable转换成Xml文件,支持分页
            /// </summary>
            /// <param name="dt">要转换的DataTable</param>
            /// <param name="strXMLName">生成xml的路径</param>
            /// <param name="intPageSize">分页页面尺寸</param>
            /// <returns>成功true</returns>
            private bool TransformDTIntoXML(DataTable dtitem, DataTable dtfrom, string strHTMLName, string strXSLTName, int intPageSize)
            {
                try
                {
                    int pno = dtitem.Rows.Count / intPageSize;
                    if (dtitem.Rows.Count % intPageSize > 0)
                    {
                        pno++;
                    }
                    int countstart = 0;
                    int countend = dtitem.Rows.Count;
                    int nowcount = dtitem.Rows.Count;
                    dtitem.Columns.Add("RecordSort", typeof(string));
                    for (int i = 0; i < dtitem.Rows.Count; i++)
                    {
                        dtitem.Rows[i]["RecordSort"] = (i + 1).ToString();
                    }
                    DataTable tmpdti = new DataTable();
                    DataTable tmpdtf = new DataTable();
                    tmpdtf = dtfrom.Copy();
                    tmpdtf.Columns.Add("ReportTime", typeof(string));
                    tmpdtf.Rows[0]["ReportTime"] = DateTime.Now.ToShortDateString() + "--" + DateTime.Now.ToShortTimeString();
                    tmpdti = dtitem.Clone();
                    for (int k = 0; k < pno; k++)
                    {
                        XmlDocument xtw = new XmlDocument();

                        int page = k + 1;
                        if (page * intPageSize < nowcount)
                        {
                            countstart = (page - 1) * intPageSize;
                            countend = page * intPageSize;
                        }
                        else if ((page == 1) && (page * intPageSize > nowcount))
                        {
                            countstart = 0;
                            countend = nowcount;
                        }
                        else
                        {
                            countstart = (page - 1) * intPageSize;
                            countend = nowcount;
                        }
                        for (int i = countstart; i < countend; i++)
                        {
                            tmpdti.ImportRow(dtitem.Rows[i]);
                        }
                        tmpdti.TableName = "dti";
                        tmpdtf.TableName = "dtf";
                        DataSet ds = new DataSet();
                        ds.Tables.Add(tmpdti);
                        ds.Tables.Add(tmpdtf);
                        xtw.LoadXml(ds.GetXml());


                        //xtw.GetElementById("NewDataSet").AppendChild(

                        XmlNode root = xtw.SelectSingleNode("NewDataSet");//查找<bookstore>
                        XmlElement pagecontrol = xtw.CreateElement("pagecontrol");

                        XmlElement recordcount = xtw.CreateElement("recordcount");
                        recordcount.InnerText = Convert.ToString(nowcount);
                        pagecontrol.AppendChild(recordcount);

                        XmlElement firstpage = xtw.CreateElement("firstpage");
                        firstpage.InnerText = strHTMLName.Replace(".xml", ".html").Substring(strHTMLName.Replace(".xml", ".html").LastIndexOf('\\') + 1, strHTMLName.Replace(".xml", ".html").Length - strHTMLName.Replace(".xml", ".html").LastIndexOf('\\') - 1);
                        pagecontrol.AppendChild(firstpage);

                        XmlElement previouspage = xtw.CreateElement("previouspage");
                        string aaa = previousFileName(k, strHTMLName);
                        previouspage.InnerText = aaa.Substring(aaa.Replace(".xml", ".html").LastIndexOf('\\') + 1, aaa.Replace(".xml", ".html").Length - aaa.Replace(".xml", ".html").LastIndexOf('\\') - 1);
                        pagecontrol.AppendChild(previouspage);

                        XmlElement nextpage = xtw.CreateElement("nextpage");
                        aaa = nextFileName(k, strHTMLName, pno);
                        nextpage.InnerText = aaa.Substring(aaa.Replace(".xml", ".html").LastIndexOf('\\') + 1, aaa.Replace(".xml", ".html").Length - aaa.Replace(".xml", ".html").LastIndexOf('\\') - 1);
                        pagecontrol.AppendChild(nextpage);

                        XmlElement lastpage = xtw.CreateElement("lastpage");
                        aaa = processFileNameHtml(pno - 1, strHTMLName.Replace(".xml", ".html"));
                        lastpage.InnerText = aaa.Substring(aaa.Replace(".xml", ".html").LastIndexOf('\\') + 1, aaa.Replace(".xml", ".html").Length - aaa.Replace(".xml", ".html").LastIndexOf('\\') - 1);
                        pagecontrol.AppendChild(lastpage);

                        XmlElement nowpage = xtw.CreateElement("nowpage");
                        nowpage.InnerText = Convert.ToString(k + 1);
                        pagecontrol.AppendChild(nowpage);

                        XmlElement current = xtw.CreateElement("current");
                        aaa = processFileNameHtml(k, strHTMLName.Replace(".xml", ".html"));
                        current.InnerText = aaa.Substring(aaa.Replace(".xml", ".html").LastIndexOf('\\') + 1, aaa.Replace(".xml", ".html").Length - aaa.Replace(".xml", ".html").LastIndexOf('\\') - 1);
                        pagecontrol.AppendChild(current);

                        root.AppendChild(pagecontrol);
                        xtw.AppendChild(root);
                        TransformXMLIntoHtml(xtw, strHTMLName, strXSLTName);
                    }
                    return true;
                }
                catch (Exception e)
                {
                    e = null;
                    return false;
                }
            }*/
        
    }
}
