using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Xml;
using System.Web;
using Excel;

namespace ZhiFang.SA.Common
{
    public class ExcelHelper
    {
        public string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\\WorkSpace\\MyDocument\\Samples2.xlsx;Extended Properties=\"Excel 8.0;HDR=YES;\"";

        public ExcelHelper(string Path, string Flag)
        {
            if (Path.Trim().Length > 0)
            {
                this.connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Path + ";Extended Properties=\"Excel 8.0;HDR=" + Flag + ";\"";
            }
        }

        public DataSet GetExcelDataSet(string commandString)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.OleDb");
            string sqlstr = "";
            if (commandString == null || commandString.Trim() == "")
            {
                OleDbConnection connection1 = new OleDbConnection
                {
                    ConnectionString = this.connectionString
                };
                connection1.Open();
                object[] restrictions = new object[4];
                restrictions[3] = "TABLE";
                System.Data.DataTable oleDbSchemaTable = connection1.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, restrictions);
                sqlstr = "select * from [" + (string)oleDbSchemaTable.Rows[0]["TABLE_NAME"] + "]";
            }
            else
            {
                sqlstr = commandString;
            }
            DbDataAdapter adapter = factory.CreateDataAdapter();
            DbCommand command = factory.CreateCommand();
            command.CommandText = sqlstr;
            DbConnection connection = factory.CreateConnection();
            connection.ConnectionString = this.connectionString;
            command.Connection = connection;
            adapter.SelectCommand = command;
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            connection.Close();
            return dataSet;
        }


        public static bool CreateExcelXML(System.Data.DataTable source, string fileName)
        {
            bool resuleBool = false;
            System.IO.StreamWriter excelDoc;
            excelDoc = new System.IO.StreamWriter(fileName);
            const string startExcelXML = @"<xml version> <Workbook " +
                  "xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\" " +
                  " xmlns:o=\"urn:schemas-microsoft-com:office:office\"  " +
                  "xmlns:x=\"urn:schemas-    microsoft-com:office:" +
                  "excel\"  xmlns:ss=\"urn:schemas-microsoft-com:" +
                  "office:spreadsheet\">  <Styles>  " +
                  "<Style ss:ID=\"Default\" ss:Name=\"Normal\">  " +
                  "<Alignment ss:Vertical=\"Bottom\"/>  <Borders/>" +
                  "  <Font/>  <Interior/>  <NumberFormat/>" +
                  "  <Protection/>  </Style>  " +
                  "<Style ss:ID=\"BoldColumn\">  <Font " +
                  "x:Family=\"Swiss\" ss:Bold=\"1\"/>  </Style>  " +
                  "<Style     ss:ID=\"StringLiteral\">  <NumberFormat" +
                  " ss:Format=\"@\"/>  </Style>  <Style " +
                  "ss:ID=\"Decimal\">  <NumberFormat " +
                  "ss:Format=\"0.0000\"/>  </Style>  " +
                  "<Style ss:ID=\"Integer\">  <NumberFormat " +
                  "ss:Format=\"0\"/>  </Style>  <Style " +
                  "ss:ID=\"DateLiteral\">  <NumberFormat " +
                  "ss:Format=\"mm/dd/yyyy;@\"/>  </Style>  " +
                  "</Styles>  ";
            const string endExcelXML = "</Workbook>";

            int rowCount = 0;
            int sheetCount = 1;

            excelDoc.Write(startExcelXML);
            excelDoc.Write("<Worksheet ss:Name=\"Sheet" + sheetCount + "\">");
            excelDoc.Write("<Table>");
            excelDoc.Write("<Row>");
            for (int x = 0; x < source.Columns.Count; x++)
            {
                excelDoc.Write("<Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\">");
                excelDoc.Write(source.Columns[x].ColumnName);
                excelDoc.Write("</Data></Cell>");
            }
            excelDoc.Write("</Row>");
            foreach (DataRow x in source.Rows)
            {
                rowCount++;
                //if the number of rows is > 64000 create a new page to continue output
                if (rowCount == 64000)
                {
                    rowCount = 0;
                    sheetCount++;
                    excelDoc.Write("</Table>");
                    excelDoc.Write(" </Worksheet>");
                    excelDoc.Write("<Worksheet ss:Name=\"Sheet" + sheetCount + "\">");
                    excelDoc.Write("<Table>");
                }
                excelDoc.Write("<Row>"); //ID=" + rowCount + "
                for (int y = 0; y < source.Columns.Count; y++)
                {
                    System.Type rowType;
                    rowType = x[y].GetType();
                    switch (rowType.ToString())
                    {
                        case "System.String":
                            string XMLstring = x[y].ToString();
                            XMLstring = XMLstring.Trim();
                            XMLstring = XMLstring.Replace("&", "&");
                            XMLstring = XMLstring.Replace(">", ">");
                            XMLstring = XMLstring.Replace("<", "<");
                            excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\">" +
                                           "<Data ss:Type=\"String\">");
                            excelDoc.Write(XMLstring);
                            excelDoc.Write("</Data></Cell>");
                            break;
                        case "System.DateTime":
                            //Excel has a specific Date Format of YYYY-MM-DD followed by  
                            //the letter 'T' then hh:mm:sss.lll Example 2005-01-31T24:01:21.000
                            //The Following Code puts the date stored in XMLDate 
                            //to the format above
                            DateTime XMLDate = (DateTime)x[y];
                            string XMLDatetoString = ""; //Excel Converted Date
                            XMLDatetoString = XMLDate.Year.ToString() +
                                 "-" +
                                 (XMLDate.Month < 10 ? "0" +
                                 XMLDate.Month.ToString() : XMLDate.Month.ToString()) +
                                 "-" +
                                 (XMLDate.Day < 10 ? "0" +
                                 XMLDate.Day.ToString() : XMLDate.Day.ToString()) +
                                 "T" +
                                 (XMLDate.Hour < 10 ? "0" +
                                 XMLDate.Hour.ToString() : XMLDate.Hour.ToString()) +
                                 ":" +
                                 (XMLDate.Minute < 10 ? "0" +
                                 XMLDate.Minute.ToString() : XMLDate.Minute.ToString()) +
                                 ":" +
                                 (XMLDate.Second < 10 ? "0" +
                                 XMLDate.Second.ToString() : XMLDate.Second.ToString()) +
                                 ".000";
                            excelDoc.Write("<Cell ss:StyleID=\"DateLiteral\">" +
                                         "<Data ss:Type=\"DateTime\">");
                            excelDoc.Write(XMLDatetoString);
                            excelDoc.Write("</Data></Cell>");
                            break;
                        case "System.Boolean":
                            excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\">" +
                                        "<Data ss:Type=\"String\">");
                            excelDoc.Write(x[y].ToString());
                            excelDoc.Write("</Data></Cell>");
                            break;
                        case "System.Int16":
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            excelDoc.Write("<Cell ss:StyleID=\"Integer\">" +
                                    "<Data ss:Type=\"Number\">");
                            excelDoc.Write(x[y].ToString());
                            excelDoc.Write("</Data></Cell>");
                            break;
                        case "System.Decimal":
                        case "System.Double":
                            excelDoc.Write("<Cell ss:StyleID=\"Decimal\">" +
                                  "<Data ss:Type=\"Number\">");
                            excelDoc.Write(x[y].ToString());
                            excelDoc.Write("</Data></Cell>");
                            break;
                        case "System.DBNull":
                            excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\">" +
                                  "<Data ss:Type=\"String\">");
                            excelDoc.Write("");
                            excelDoc.Write("</Data></Cell>");
                            break;
                        //default:
                        //    throw (new Exception(rowType.ToString() + " not handled."));
                    }
                }
                excelDoc.Write("</Row>");
            }
            excelDoc.Write("</Table>");
            excelDoc.Write(" </Worksheet>");
            excelDoc.Write(endExcelXML);
            excelDoc.Close();
            resuleBool = true;
            return resuleBool;
        }

        public static bool CreateExcelXML(System.Data.DataTable mainSource, System.Data.DataTable childSource, string fileName)
        {
            bool resuleBool = false;
            System.IO.StreamWriter excelDoc;
            excelDoc = new System.IO.StreamWriter(fileName);
            try
            {
                try
                {
                    const string startExcelXML = @"<xml version> <Workbook " +
                          "xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\" " +
                          " xmlns:o=\"urn:schemas-microsoft-com:office:office\"  " +
                          "xmlns:x=\"urn:schemas-    microsoft-com:office:" +
                          "excel\"  xmlns:ss=\"urn:schemas-microsoft-com:" +
                          "office:spreadsheet\">  <Styles>  " +
                          "<Style ss:ID=\"Default\" ss:Name=\"Normal\">  " +
                          "<Alignment ss:Vertical=\"Bottom\"/>  <Borders/>" +
                          "  <Font/>  <Interior/>  <NumberFormat/>" +
                          "  <Protection/>  </Style>  " +
                          "<Style ss:ID=\"BoldColumn\">  <Font " +
                          "x:Family=\"Swiss\" ss:Bold=\"1\"/>  </Style>  " +
                          "<Style     ss:ID=\"StringLiteral\">  <NumberFormat" +
                          " ss:Format=\"@\"/>  </Style>  <Style " +
                          "ss:ID=\"Decimal\">  <NumberFormat " +
                          "ss:Format=\"0.0000\"/>  </Style>  " +
                          "<Style ss:ID=\"Integer\">  <NumberFormat " +
                          "ss:Format=\"0\"/>  </Style>  <Style " +
                          "ss:ID=\"DateLiteral\">  <NumberFormat " +
                          "ss:Format=\"mm/dd/yyyy;@\"/>  </Style>  " +
                          "</Styles>  ";
                    const string endExcelXML = "</Workbook>";
                    excelDoc.Write(startExcelXML);
                    WriteExcelXML(excelDoc, mainSource, 1);
                    WriteExcelXML(excelDoc, childSource, 2);
                    excelDoc.Write(endExcelXML);
                    resuleBool = true;
                }
                catch (Exception ex)
                {
                    ZhiFang.Common.Log.Log.Info("导出Excel错误：" + ex.Message);
                }
            }
            finally
            {
                excelDoc.Close();
            }
            return resuleBool;
        }

        public static bool WriteExcelXML(System.IO.StreamWriter excelDoc, System.Data.DataTable source, int sheetCount)
        {
            int rowCount = 0;
            excelDoc.Write("<Worksheet ss:Name=\"Sheet" + sheetCount + "\">");
            excelDoc.Write("<Table>");
            excelDoc.Write("<Row>");
            for (int x = 0; x < source.Columns.Count; x++)
            {
                excelDoc.Write("<Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\">");
                excelDoc.Write(source.Columns[x].ColumnName);
                excelDoc.Write("</Data></Cell>");
            }
            excelDoc.Write("</Row>");
            foreach (DataRow x in source.Rows)
            {
                rowCount++;
                //if the number of rows is > 64000 create a new page to continue output
                if (rowCount == 64000)
                {
                    rowCount = 0;
                    sheetCount++;
                    excelDoc.Write("</Table>");
                    excelDoc.Write(" </Worksheet>");
                    excelDoc.Write("<Worksheet ss:Name=\"Sheet" + sheetCount + "\">");
                    excelDoc.Write("<Table>");
                }
                excelDoc.Write("<Row>"); //ID=" + rowCount + "
                for (int y = 0; y < source.Columns.Count; y++)
                {
                    System.Type rowType;
                    rowType = x[y].GetType();
                    switch (rowType.ToString())
                    {
                        case "System.String":
                            string XMLstring = x[y].ToString();
                            XMLstring = XMLstring.Trim();
                            XMLstring = XMLstring.Replace("&", "&");
                            XMLstring = XMLstring.Replace(">", ">");
                            XMLstring = XMLstring.Replace("<", "<");
                            excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\">" +
                                           "<Data ss:Type=\"String\">");
                            excelDoc.Write(XMLstring);
                            excelDoc.Write("</Data></Cell>");
                            break;
                        case "System.DateTime":
                            //Excel has a specific Date Format of YYYY-MM-DD followed by  
                            //the letter 'T' then hh:mm:sss.lll Example 2005-01-31T24:01:21.000
                            //The Following Code puts the date stored in XMLDate 
                            //to the format above
                            DateTime XMLDate = (DateTime)x[y];
                            string XMLDatetoString = ""; //Excel Converted Date
                            XMLDatetoString = XMLDate.Year.ToString() +
                                 "-" +
                                 (XMLDate.Month < 10 ? "0" +
                                 XMLDate.Month.ToString() : XMLDate.Month.ToString()) +
                                 "-" +
                                 (XMLDate.Day < 10 ? "0" +
                                 XMLDate.Day.ToString() : XMLDate.Day.ToString()) +
                                 "T" +
                                 (XMLDate.Hour < 10 ? "0" +
                                 XMLDate.Hour.ToString() : XMLDate.Hour.ToString()) +
                                 ":" +
                                 (XMLDate.Minute < 10 ? "0" +
                                 XMLDate.Minute.ToString() : XMLDate.Minute.ToString()) +
                                 ":" +
                                 (XMLDate.Second < 10 ? "0" +
                                 XMLDate.Second.ToString() : XMLDate.Second.ToString()) +
                                 ".000";
                            excelDoc.Write("<Cell ss:StyleID=\"DateLiteral\">" +
                                         "<Data ss:Type=\"DateTime\">");
                            excelDoc.Write(XMLDatetoString);
                            excelDoc.Write("</Data></Cell>");
                            break;
                        case "System.Boolean":
                            excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\">" +
                                        "<Data ss:Type=\"String\">");
                            excelDoc.Write(x[y].ToString());
                            excelDoc.Write("</Data></Cell>");
                            break;
                        case "System.Int16":
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            excelDoc.Write("<Cell ss:StyleID=\"Integer\">" +
                                    "<Data ss:Type=\"Number\">");
                            excelDoc.Write(x[y].ToString());
                            excelDoc.Write("</Data></Cell>");
                            break;
                        case "System.Decimal":
                        case "System.Double":
                            excelDoc.Write("<Cell ss:StyleID=\"Decimal\">" +
                                  "<Data ss:Type=\"Number\">");
                            excelDoc.Write(x[y].ToString());
                            excelDoc.Write("</Data></Cell>");
                            break;
                        case "System.DBNull":
                            excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\">" +
                                  "<Data ss:Type=\"String\">");
                            excelDoc.Write("");
                            excelDoc.Write("</Data></Cell>");
                            break;
                        //default:
                        //    throw (new Exception(rowType.ToString() + " not handled."));
                    }
                }
                excelDoc.Write("</Row>");
            }
            excelDoc.Write("</Table>");
            excelDoc.Write(" </Worksheet>");
            return false;
        }

        public static bool CreateExcelByNPOI(System.Data.DataTable mainSource, string fileName, string filePathName)
        {
            return MyNPOIHelper.ExportDTtoExcel(mainSource, fileName, filePathName);
        }

        public static bool CreateExcelByNPOI(System.Data.DataTable mainSource, System.Data.DataTable childSource, string fileName, string filePathName)
        {
            return MyNPOIHelper.ExportDTtoExcel(mainSource, childSource, fileName, filePathName);
        }
    }
}

