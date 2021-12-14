using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.Web;
using System.IO;
using ZhiFang.Common.Public;
using System.Collections;

namespace ZhiFang.Common.Public
{
    public class ExportExcel
    {        
        /// <summary>
        /// 不安装excel组件实现导出excel功能
        /// </summary>
        /// <param name="dv"></param>
        /// <param name="dic"></param>
        /// <param name="reportTitle">excel标题</param>
        public static string CreateExcel(DataView dv, Dictionary<string, string> dic, string reportTitle)
        {
            try
            {
                string filepath = "";
                string strModelPath = Common.Public.ConfigHelper.GetConfigString("ReportTemplateDir");
                string strBasePath = GetFilePath.GetPhysicsFilePath(strModelPath + "/");
                string guid = GUIDHelp.GetGUIDString();
                filepath = strBasePath + "\\EXCEL\\" + guid + ".xls";

                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }

                StreamWriter rw = File.CreateText(filepath);
                rw.WriteLine("<?xml version=\"1.0\"?>");
                rw.WriteLine("<?mso-application progid=\"Excel.Sheet\"?>");
                rw.WriteLine("<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\" xmlns:o=\"urn:schemas-microsoft-com:office:office\" xmlns:x=\"urn:schemas-microsoft-com:office:excel\" xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\" xmlns:html=\"http://www.w3.org/TR/REC-html40\">");
                rw.WriteLine("<DocumentProperties xmlns=\"urn:schemas-microsoft-com:office:office\">");
                rw.WriteLine("<Created>1996-12-17T01:32:42Z</Created>");
                rw.WriteLine("<LastSaved>2000-11-18T06:53:49Z</LastSaved>");
                rw.WriteLine("<Version>11.8107</Version>");
                rw.WriteLine("</DocumentProperties>");
                rw.WriteLine("<OfficeDocumentSettings xmlns=\"urn:schemas-microsoft-com:office:office\">");
                rw.WriteLine("<RemovePersonalInformation/>");
                rw.WriteLine("</OfficeDocumentSettings>");
                rw.WriteLine("<ExcelWorkbook xmlns=\"urn:schemas-microsoft-com:office:excel\">");
                rw.WriteLine("<WindowHeight>4530</WindowHeight>");
                rw.WriteLine("<WindowWidth>8505</WindowWidth>");
                rw.WriteLine("<WindowTopX>480</WindowTopX>");
                rw.WriteLine("<WindowTopY>120</WindowTopY>");
                rw.WriteLine("<AcceptLabelsInFormulas/>");
                rw.WriteLine("<ProtectStructure>False</ProtectStructure>");
                rw.WriteLine("<ProtectWindows>False</ProtectWindows>");
                rw.WriteLine("</ExcelWorkbook>");
                rw.WriteLine("<Styles>");
                rw.WriteLine("<Style ss:ID=\"Default\" ss:Name=\"Normal\">");
                rw.WriteLine("<Alignment ss:Vertical=\"Bottom\"/>");
                rw.WriteLine("<Borders/>");
                rw.WriteLine("<Font ss:FontName=\"宋体\" x:CharSet=\"134\" ss:Size=\"12\"/>");
                rw.WriteLine("<Interior/>");
                rw.WriteLine("<NumberFormat/>");
                rw.WriteLine("<Protection/>");
                rw.WriteLine("</Style>");
                rw.WriteLine("<Style ss:ID=\"s24\">");
                rw.WriteLine("<Font ss:FontName=\"宋体\" x:CharSet=\"134\" ss:Size=\"9\"/>");
                rw.WriteLine("<Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Bottom\"/>");
                rw.WriteLine(" <Borders>");
                rw.WriteLine(" <Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>");
                rw.WriteLine("<Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>");
                rw.WriteLine("<Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>");
                rw.WriteLine("<Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>");
                rw.WriteLine("</Borders>");
                rw.WriteLine("<NumberFormat ss:Format=\"@\"/>");
                rw.WriteLine("</Style>");
                rw.WriteLine("<Style ss:ID=\"s25\">");
                rw.WriteLine("<Font ss:FontName=\"宋体\" x:CharSet=\"134\" ss:Size=\"9\"/>");
                rw.WriteLine("<Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Bottom\"/>");
                rw.WriteLine("<Borders>");
                rw.WriteLine(" <Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>");
                rw.WriteLine(" <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>");
                rw.WriteLine(" <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>");
                rw.WriteLine(" <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>");
                rw.WriteLine("</Borders>");
                rw.WriteLine("</Style>");
                rw.WriteLine("<Style ss:ID=\"s26\">");
                rw.WriteLine("<Font ss:FontName=\"宋体\" x:CharSet=\"134\" ss:Size=\"9\"/>");
                rw.WriteLine("<Alignment ss:Horizontal=\"Right\" ss:Vertical=\"Bottom\"/>");
                rw.WriteLine("</Style>");
                rw.WriteLine("<Style ss:ID=\"s27\">");
                rw.WriteLine("<Font ss:FontName=\"宋体\" x:CharSet=\"134\" ss:Size=\"9\"/>");
                rw.WriteLine("<Alignment ss:Horizontal=\"Left\" ss:Vertical=\"Bottom\"/>");
                rw.WriteLine("</Style>");
                rw.WriteLine("<Style ss:ID=\"m24861836\">");
                rw.WriteLine("<Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Center\"/>");
                rw.WriteLine("<Borders/>");
                rw.WriteLine("<Font ss:FontName=\"宋体\" x:CharSet=\"134\" ss:Size=\"20\" ss:Bold=\"1\"/>");
                rw.WriteLine("<NumberFormat/>");
                rw.WriteLine("<Protection ss:Protected=\"0\"/>");
                rw.WriteLine("</Style>");
                rw.WriteLine("<Style ss:ID=\"s28\">");
                rw.WriteLine("<Font ss:FontName=\"宋体\" x:CharSet=\"134\" ss:Size=\"9\"/>");
                rw.WriteLine("<Alignment ss:Horizontal=\"Left\" ss:Vertical=\"Bottom\"/>");
                rw.WriteLine("<NumberFormat ss:Format=\"@\"/>");
                rw.WriteLine("</Style>");

                rw.WriteLine("<Style ss:ID=\"s50\">");
                rw.WriteLine("<Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Center\"/>");
                rw.WriteLine("<Borders/>");
                rw.WriteLine("<Font ss:FontName=\"宋体\" x:CharSet=\"134\" ss:Size=\"12\" ss:Bold=\"1\"/>");
                rw.WriteLine("<NumberFormat/>");
                rw.WriteLine("<Protection ss:Protected=\"0\"/>");
                rw.WriteLine("</Style>");

                rw.WriteLine("</Styles>");
                //sheet
                rw.WriteLine("<Worksheet ss:Name=\"Sheet1\">");
                rw.WriteLine("<Table x:FullColumns=\"1\" x:FullRows=\"1\" ss:DefaultColumnWidth=\"75\" ss:DefaultRowHeight=\"14.25\">");


                //int cou = 1;
                //设置表头
                rw.WriteLine("<Row ss:Height=\"31.5\">");
                rw.WriteLine("<Cell ss:MergeAcross=\"" + (dic.Count - 1) + "\" ss:StyleID=\"m24861836\"><Data ss:Type=\"String\">" + reportTitle + "</Data></Cell>");
                rw.WriteLine("</Row>");

                //设置列名
                rw.WriteLine("<Row>");
                foreach (string value in dic.Values)
                {
                    rw.WriteLine("<Cell ss:StyleID=\"s50\"><Data ss:Type=\"String\">" + value + "</Data></Cell>");
                }
                rw.WriteLine("</Row>");

                //
                //取得表格中的数据
                //
                foreach (DataRowView dr in dv)
                {
                    rw.WriteLine("<Row>");
                    //表格单元赋值
                    foreach (string key in dic.Keys)
                    {
                        string colvalue = dr[key].ToString();
                        rw.WriteLine("<Cell ss:StyleID=\"s25\"><Data ss:Type=\"String\">" + colvalue + "</Data></Cell>");
                    }

                    rw.WriteLine("</Row>");

                }


                //设置表尾

                rw.WriteLine("</Table>");
                rw.WriteLine("<WorksheetOptions xmlns=\"urn:schemas-microsoft-com:office:excel\">");
                rw.WriteLine("<Selected/>");
                rw.WriteLine("<ProtectObjects>False</ProtectObjects>");
                rw.WriteLine("<ProtectScenarios>False</ProtectScenarios>");
                rw.WriteLine("</WorksheetOptions>");
                rw.WriteLine("</Worksheet>");

                //rw.WriteLine("<Worksheet ss:Name=\"Sheet2\">");
                //rw.WriteLine("<Table ss:ExpandedColumnCount=\"0\" ss:ExpandedRowCount=\"0\" x:FullColumns=\"1\" x:FullRows=\"1\" ss:DefaultColumnWidth=\"54\" ss:DefaultRowHeight=\"14.25\"/>");
                //rw.WriteLine("<WorksheetOptions xmlns=\"urn:schemas-microsoft-com:office:excel\">");
                //rw.WriteLine("<Selected/>");
                //rw.WriteLine("<ProtectObjects>False</ProtectObjects>");
                //rw.WriteLine("<ProtectScenarios>False</ProtectScenarios>");
                //rw.WriteLine("</WorksheetOptions>");
                //rw.WriteLine("</Worksheet>");

                //rw.WriteLine("<Worksheet ss:Name=\"Sheet3\">");
                //rw.WriteLine("<Table ss:ExpandedColumnCount=\"0\" ss:ExpandedRowCount=\"0\" x:FullColumns=\"1\" x:FullRows=\"1\" ss:DefaultColumnWidth=\"54\" ss:DefaultRowHeight=\"14.25\"/>");
                //rw.WriteLine("<WorksheetOptions xmlns=\"urn:schemas-microsoft-com:office:excel\">");
                //rw.WriteLine("<Selected/>");
                //rw.WriteLine("<ProtectObjects>False</ProtectObjects>");
                //rw.WriteLine("<ProtectScenarios>False</ProtectScenarios>");
                //rw.WriteLine("</WorksheetOptions>");
                //rw.WriteLine("</Worksheet>");
                //sheet
                rw.WriteLine("</Workbook>");
                rw.Flush();
                rw.Close();

                //System.IO.FileInfo file = new System.IO.FileInfo(filepath);

                //HttpContext.Current.Response.Clear();
                //HttpContext.Current.Response.Charset = "utf-8";
                //HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                //// 添加头信息，为"文件下载/另存为"对话框指定默认文件名
                //HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpContext.Current.Server.UrlEncode(file.Name));
                //// 添加头信息，指定文件大小，让浏览器能够显示下载进度
                //HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString());

                //// 指定返回的是一个不能被客户端读取的流，必须被下载
                //HttpContext.Current.Response.ContentType = "application/ms-excel";

                //// 把文件流发送到客户端
                //HttpContext.Current.Response.WriteFile(file.FullName);
                
                //// 停止页面的执行
                //HttpContext.Current.Response.End();

                return filepath;
            }
            catch (Exception ex)
            {
                Log.Log.Error("导出Excel异常（ZhiFang.Common.Public.ExportExcel.CreateExcel）-->", ex);
                return "";
            }

        }

        /// <summary>
        /// 不安装excel组件实现导出excel功能
        /// </summary>
        /// <param name="dv"></param>
        /// <param name="str"></param>
        public void CreateExcel(DataView dv, string str)
        {
            XmlDocument doc = null;
            if (doc == null)
            {
                doc = new XmlDocument();
                doc.Load(HttpContext.Current.Server.MapPath("../xml/LabEmgencyQuery.xml"));
                if (doc == null)
                {
                    Log.Log.Error("实验室查询失败，加载LabEmgencyQuery.xml文件失败");
                    return;
                }
            }

            string filepath = HttpContext.Current.Server.MapPath(".") + "\\" + "Emgency.xls";
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }

            XmlNodeList xnList = doc.DocumentElement.SelectNodes("//Columns/column");

            StreamWriter rw = File.CreateText(filepath);
            rw.WriteLine("<?xml version=\"1.0\"?>");
            rw.WriteLine("<?mso-application progid=\"Excel.Sheet\"?>");
            rw.WriteLine("<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\" xmlns:o=\"urn:schemas-microsoft-com:office:office\" xmlns:x=\"urn:schemas-microsoft-com:office:excel\" xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\" xmlns:html=\"http://www.w3.org/TR/REC-html40\">");
            rw.WriteLine("<DocumentProperties xmlns=\"urn:schemas-microsoft-com:office:office\">");
            rw.WriteLine("<Created>1996-12-17T01:32:42Z</Created>");
            rw.WriteLine("<LastSaved>2000-11-18T06:53:49Z</LastSaved>");
            rw.WriteLine("<Version>11.8107</Version>");
            rw.WriteLine("</DocumentProperties>");
            rw.WriteLine("<OfficeDocumentSettings xmlns=\"urn:schemas-microsoft-com:office:office\">");
            rw.WriteLine("<RemovePersonalInformation/>");
            rw.WriteLine("</OfficeDocumentSettings>");
            rw.WriteLine("<ExcelWorkbook xmlns=\"urn:schemas-microsoft-com:office:excel\">");
            rw.WriteLine("<WindowHeight>4530</WindowHeight>");
            rw.WriteLine("<WindowWidth>8505</WindowWidth>");
            rw.WriteLine("<WindowTopX>480</WindowTopX>");
            rw.WriteLine("<WindowTopY>120</WindowTopY>");
            rw.WriteLine("<AcceptLabelsInFormulas/>");
            rw.WriteLine("<ProtectStructure>False</ProtectStructure>");
            rw.WriteLine("<ProtectWindows>False</ProtectWindows>");
            rw.WriteLine("</ExcelWorkbook>");
            rw.WriteLine("<Styles>");
            rw.WriteLine("<Style ss:ID=\"Default\" ss:Name=\"Normal\">");
            rw.WriteLine("<Alignment ss:Vertical=\"Bottom\"/>");
            rw.WriteLine("<Borders/>");
            rw.WriteLine("<Font ss:FontName=\"宋体\" x:CharSet=\"134\" ss:Size=\"12\"/>");
            rw.WriteLine("<Interior/>");
            rw.WriteLine("<NumberFormat/>");
            rw.WriteLine("<Protection/>");
            rw.WriteLine("</Style>");
            rw.WriteLine("<Style ss:ID=\"s24\">");
            rw.WriteLine("<Font ss:FontName=\"宋体\" x:CharSet=\"134\" ss:Size=\"9\"/>");
            rw.WriteLine("<Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Bottom\"/>");
            rw.WriteLine(" <Borders>");
            rw.WriteLine(" <Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>");
            rw.WriteLine("<Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>");
            rw.WriteLine("<Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>");
            rw.WriteLine("<Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>");
            rw.WriteLine("</Borders>");
            rw.WriteLine("<NumberFormat ss:Format=\"@\"/>");
            rw.WriteLine("</Style>");
            rw.WriteLine("<Style ss:ID=\"s25\">");
            rw.WriteLine("<Font ss:FontName=\"宋体\" x:CharSet=\"134\" ss:Size=\"9\"/>");
            rw.WriteLine("<Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Bottom\"/>");
            rw.WriteLine("<Borders>");
            rw.WriteLine(" <Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>");
            rw.WriteLine(" <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>");
            rw.WriteLine(" <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>");
            rw.WriteLine(" <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>");
            rw.WriteLine("</Borders>");
            rw.WriteLine("</Style>");
            rw.WriteLine("<Style ss:ID=\"s26\">");
            rw.WriteLine("<Font ss:FontName=\"宋体\" x:CharSet=\"134\" ss:Size=\"9\"/>");
            rw.WriteLine("<Alignment ss:Horizontal=\"Right\" ss:Vertical=\"Bottom\"/>");
            rw.WriteLine("</Style>");
            rw.WriteLine("<Style ss:ID=\"s27\">");
            rw.WriteLine("<Font ss:FontName=\"宋体\" x:CharSet=\"134\" ss:Size=\"9\"/>");
            rw.WriteLine("<Alignment ss:Horizontal=\"Left\" ss:Vertical=\"Bottom\"/>");
            rw.WriteLine("</Style>");
            rw.WriteLine("<Style ss:ID=\"m24861836\">");
            rw.WriteLine("<Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Center\"/>");
            rw.WriteLine("<Borders/>");
            rw.WriteLine("<Font ss:FontName=\"宋体\" x:CharSet=\"134\" ss:Size=\"20\" ss:Bold=\"1\"/>");
            rw.WriteLine("<NumberFormat/>");
            rw.WriteLine("<Protection ss:Protected=\"0\"/>");
            rw.WriteLine("</Style>");
            rw.WriteLine("<Style ss:ID=\"s28\">");
            rw.WriteLine("<Font ss:FontName=\"宋体\" x:CharSet=\"134\" ss:Size=\"9\"/>");
            rw.WriteLine("<Alignment ss:Horizontal=\"Left\" ss:Vertical=\"Bottom\"/>");
            rw.WriteLine("<NumberFormat ss:Format=\"@\"/>");
            rw.WriteLine("</Style>");

            rw.WriteLine("<Style ss:ID=\"s50\">");
            rw.WriteLine("<Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Center\"/>");
            rw.WriteLine("<Borders/>");
            rw.WriteLine("<Font ss:FontName=\"宋体\" x:CharSet=\"134\" ss:Size=\"12\" ss:Bold=\"1\"/>");
            rw.WriteLine("<NumberFormat/>");
            rw.WriteLine("<Protection ss:Protected=\"0\"/>");
            rw.WriteLine("</Style>");

            rw.WriteLine("</Styles>");
            //sheet
            rw.WriteLine("<Worksheet ss:Name=\"Sheet1\">");
            rw.WriteLine("<Table x:FullColumns=\"1\" x:FullRows=\"1\" ss:DefaultColumnWidth=\"75\" ss:DefaultRowHeight=\"14.25\">");


            //int cou = 1;
            ////设置表头
            //rw.WriteLine("<Row ss:Height=\"31.5\">");
            //rw.WriteLine("<Cell ss:MergeAcross=\"7\" ss:StyleID=\"m24861836\"><Data ss:Type=\"String\">消息统计分析</Data></Cell>");
            //rw.WriteLine("</Row>");

            //设置列名
            rw.WriteLine("<Row>");
            foreach (XmlNode xn in xnList)
            {
                rw.WriteLine("<Cell ss:StyleID=\"s50\"><Data ss:Type=\"String\">" + xn.Attributes.GetNamedItem("columncname").InnerText + "</Data></Cell>");
            }
            rw.WriteLine("</Row>");

            //
            //取得表格中的数据
            //
            foreach (DataRowView dr in dv)
            {
                rw.WriteLine("<Row>");
                //表格单元赋值

                foreach (XmlNode xn in xnList)
                {
                    string colname = xn.Attributes.GetNamedItem("columnename").InnerText;
                    string colvalue = dr[colname].ToString();

                    if (colname == "ReceiveDate")
                    {
                        colvalue = Convert.ToDateTime(dr["ReceiveDate"].ToString()).ToString("yyyy-MM-dd");
                    }

                    if (colname == "ReportValueAll")
                    {
                        colvalue = dr["ReportValueAll"].ToString() + "[" + dr["Unit"].ToString() + "]";
                    }

                    if (colname == "EmgencyTime")
                    {
                        if (dr["EmgencyTime"].ToString() != "")
                        {
                            colvalue = Convert.ToDateTime(dr["EmgencyTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        else
                        {
                            colvalue = "";
                        }
                    }

                    if (colname == "MsgHandleTime")
                    {
                        if (dr["MsgHandleTime"].ToString() != "")
                        {
                            colvalue = Convert.ToDateTime(dr["MsgHandleTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        else
                        {
                            colvalue = "";
                        }
                    }

                    if (colname == "MsgReceiveTime")
                    {
                        if (dr["MsgReceiveTime"].ToString() != "")
                        {
                            colvalue = Convert.ToDateTime(dr["MsgReceiveTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        else
                        {
                            colvalue = "";
                        }
                    }
                    rw.WriteLine("<Cell ss:StyleID=\"s25\"><Data ss:Type=\"String\">" + colvalue + "</Data></Cell>");
                }

                rw.WriteLine("</Row>");

            }


            //设置表尾

            rw.WriteLine("</Table>");
            rw.WriteLine("<WorksheetOptions xmlns=\"urn:schemas-microsoft-com:office:excel\">");
            rw.WriteLine("<Selected/>");
            rw.WriteLine("<ProtectObjects>False</ProtectObjects>");
            rw.WriteLine("<ProtectScenarios>False</ProtectScenarios>");
            rw.WriteLine("</WorksheetOptions>");
            rw.WriteLine("</Worksheet>");

            //rw.WriteLine("<Worksheet ss:Name=\"Sheet2\">");
            //rw.WriteLine("<Table ss:ExpandedColumnCount=\"0\" ss:ExpandedRowCount=\"0\" x:FullColumns=\"1\" x:FullRows=\"1\" ss:DefaultColumnWidth=\"54\" ss:DefaultRowHeight=\"14.25\"/>");
            //rw.WriteLine("<WorksheetOptions xmlns=\"urn:schemas-microsoft-com:office:excel\">");
            //rw.WriteLine("<Selected/>");
            //rw.WriteLine("<ProtectObjects>False</ProtectObjects>");
            //rw.WriteLine("<ProtectScenarios>False</ProtectScenarios>");
            //rw.WriteLine("</WorksheetOptions>");
            //rw.WriteLine("</Worksheet>");

            //rw.WriteLine("<Worksheet ss:Name=\"Sheet3\">");
            //rw.WriteLine("<Table ss:ExpandedColumnCount=\"0\" ss:ExpandedRowCount=\"0\" x:FullColumns=\"1\" x:FullRows=\"1\" ss:DefaultColumnWidth=\"54\" ss:DefaultRowHeight=\"14.25\"/>");
            //rw.WriteLine("<WorksheetOptions xmlns=\"urn:schemas-microsoft-com:office:excel\">");
            //rw.WriteLine("<Selected/>");
            //rw.WriteLine("<ProtectObjects>False</ProtectObjects>");
            //rw.WriteLine("<ProtectScenarios>False</ProtectScenarios>");
            //rw.WriteLine("</WorksheetOptions>");
            //rw.WriteLine("</Worksheet>");
            //sheet
            rw.WriteLine("</Workbook>");
            rw.Flush();
            rw.Close();

            System.IO.FileInfo file = new System.IO.FileInfo(filepath);

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            // 添加头信息，为"文件下载/另存为"对话框指定默认文件名
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpContext.Current.Server.UrlEncode(file.Name));
            // 添加头信息，指定文件大小，让浏览器能够显示下载进度
            HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString());

            // 指定返回的是一个不能被客户端读取的流，必须被下载
            HttpContext.Current.Response.ContentType = "application/ms-excel";

            // 把文件流发送到客户端
            HttpContext.Current.Response.WriteFile(file.FullName);
            
            // 停止页面的执行
            HttpContext.Current.Response.End();

        }

    }
}
