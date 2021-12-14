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


namespace ZhiFang.Tools
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

        public string[] EcxelToGridView()
        {
            OleDbConnection connection = new OleDbConnection(this.connectionString);
            connection.Open();
            object[] restrictions = new object[4];
            restrictions[3] = "Table";
            System.Data.DataTable oleDbSchemaTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, restrictions);
            string[] strArray = new string[oleDbSchemaTable.Rows.Count];
            for (int i = 0; i < oleDbSchemaTable.Rows.Count; i++)
            {
                strArray[i] = oleDbSchemaTable.Rows[(oleDbSchemaTable.Rows.Count - i) - 1]["TABLE_NAME"].ToString();
            }
            connection.Close();
            List<string> list = new List<string>();
            list.Clear();
            return strArray;
        }

        public DataSet GetExcelDataSet(string commandString)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.OleDb");
            string sqlstr="";
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

        public static bool CreateExcel(System.Data.DataTable source, string fileName)
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

        public static bool CreateExcel(System.Data.DataTable mainSource, System.Data.DataTable childSource, string fileName)
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

        public static bool CreateCsv(string context, string strName)
        {
            try
            {
                StreamWriter sw = new StreamWriter(new FileStream(strName , FileMode.Create), Encoding.GetEncoding("GB2312"));
                sw.Write(context);
                sw.Close();
                return true;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("生成CVS异常！异常信息：" + ex.ToString() + "--'" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "'");
                return false;
            }
        }

        /// <summary>
        /// 2015-11-12 增加导出CSV功能
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="fileName"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static bool DataTableToCSV(System.Data.DataTable dt, string fileName, string title)
        {
            try
            {
                string columnName = "";

                FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate);

                //FileStream fs1 = File.Open(file, FileMode.Open, FileAccess.Read);

                StreamWriter sw = new StreamWriter(new BufferedStream(fs), System.Text.Encoding.Default);

                if (!string.IsNullOrEmpty(title))
                {
                    title = title.Substring(0, title.Length - 1) + "\n";
                    sw.Write(title);
                    sw.Write("  " + "\n");
                }

                for (int i = 0; i < dt.Columns.Count; i++)
                {

                    columnName += dt.Columns[i].ColumnName + "\t"; //栏位：自动跳到下一单元格

                }

                columnName = columnName.Substring(0, columnName.Length - 1) + "\n";

                sw.Write(columnName);

                foreach (DataRow row in dt.Rows)
                {

                    string line = "";

                    for (int i = 0; i < dt.Columns.Count; i++)
                    {

                        line += row[i].ToString().Trim() + "\t"; //内容：自动跳到下一单元格

                    }

                    line = line.Substring(0, line.Length - 1) + "\n";

                    sw.Write(line);

                }

                sw.Close();
                fs.Close();
                return true;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("生成CSV异常！异常信息：" + ex.ToString() + "--'" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "'");
                return false;
                throw (new Exception("生成CSV异常！异常信息：" + ex.ToString() + "--'" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "'"));
            }
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="dataSet">需要导出Excel的DataSet</param>
        /// <param name="fileName">导出的路径</param>
        public static bool ExportToExcel(DataSet dataSet, string fileName)
        {
            try
            {
                if (dataSet.Tables.Count == 0)
                {
                    throw new Exception("DataSet中没有任何可导出的表。");
                }

                Application excelApplication = new Application();
                excelApplication.DisplayAlerts = false;

                //Excel.Workbook workbook = excelApplication.Workbooks.Add("aaa");
                Workbook workbook = excelApplication.Workbooks.Add(System.Reflection.Missing.Value);

                foreach (System.Data.DataTable dt in dataSet.Tables)
                {
                    Worksheet lastWorksheet = (Worksheet)workbook.Worksheets.get_Item(workbook.Worksheets.Count);
                    Worksheet newSheet = (Worksheet)workbook.Worksheets.Add(Type.Missing, lastWorksheet, Type.Missing, Type.Missing);

                    newSheet.Name = dt.TableName;

                    for (int col = 0; col < dt.Columns.Count; col++)
                    {
                        newSheet.Cells[1, col + 1] = dt.Columns[col].ColumnName;
                    }

                    for (int row = 0; row < dt.Rows.Count; row++)
                    {
                        for (int col = 0; col < dt.Columns.Count; col++)
                        {
                            newSheet.Cells[row + 2, col + 1] = dt.Rows[row][col].ToString();
                        }
                    }
                }

                ((Worksheet)workbook.Worksheets.get_Item(1)).Delete();
                ((Worksheet)workbook.Worksheets.get_Item(1)).Delete();
                ((Worksheet)workbook.Worksheets.get_Item(1)).Delete();
                ((Worksheet)workbook.Worksheets.get_Item(1)).Activate();

                try
                {
                    workbook.SaveAs(
                        fileName,
                        XlFileFormat.xlExcel7,
                        Type.Missing,
                            Type.Missing,
                            Type.Missing,
                            Type.Missing,
                            XlSaveAsAccessMode.xlNoChange,
                            Type.Missing,
                            Type.Missing,
                            Type.Missing,
                            Type.Missing);

                    // workbook.Close(true, fileName, System.Reflection.Missing.Value);
                }
                catch (Exception e)
                {
                    throw e;
                }

                excelApplication.Quit();
                return true;
            }
            catch(Exception e)
            {
                ZhiFang.Common.Log.Log.Error("生成Excel文件错误！：" + e.ToString());
                return false;
            }
        }

        public static DataSet ReadXMLByWebLis(string path)
        {
            XmlDocument xdo = new XmlDocument();
            System.Data.DataTable dt = new System.Data.DataTable();
            xdo.Load(path);

           
            XmlNode xd = xdo.ChildNodes[2].ChildNodes[3].ChildNodes[0];
            //if(xd.Name
            XmlNodeList xnl = xd.ChildNodes;
            int sub = 1;
            XmlNode xn = xd.ChildNodes[0];
            while (xn.Name != "Row")
            {                
                xn = xd.ChildNodes[sub];
                sub++;
            }
            foreach(XmlNode a in xn.ChildNodes)
            {
                dt.Columns.Add(a.ChildNodes[0].InnerText,typeof(string));
            }
            for (int i = sub; i < xnl.Count; i++)
            {
                DataRow dr = dt.NewRow();
                for (int j = 0; j < xnl[i].ChildNodes.Count; j++)
                {
                    dr[j] = xnl[i].ChildNodes[j].InnerText;
                }
                dt.Rows.Add(dr);
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            return ds;
        }
        public static DataSet ReadXMLByWebLis_DataSet(string path)
        {            
            DataSet ds = new DataSet();
            ds.ReadXml(path);
            return ds;
        }

        public static void OutputExcel1(DataSet dataSet, string fileName)
        {
            //dv为要输出到Excel的数据，str为标题名称
            //GC.Collect();

            Application excel = new Application();
            _Workbook xBk = excel.Workbooks.Add(true);
            _Worksheet xSt = (_Worksheet)xBk.ActiveSheet;
            int rowIndex = 4;
            int colIndex = 1;

            //
            //取得标题
            //
            foreach (DataColumn col in dataSet.Tables[0].Columns)
            {
                colIndex++;
                excel.Cells[rowIndex, colIndex] = col.ColumnName;
                xSt.get_Range(excel.Cells[rowIndex, colIndex], excel.Cells[rowIndex, colIndex]).HorizontalAlignment = XlVAlign.xlVAlignCenter;//设置标题格式为居中对齐
            }
        }

        public void OutputExcel(DataSet dataSet, string str)
        {
            //dv为要输出到Excel的数据，str为标题名称
            GC.Collect();
            Application excel = new Application();
            _Workbook xBk = excel.Workbooks.Add(true);
            _Worksheet xSt = (_Worksheet)xBk.ActiveSheet;
            int rowIndex = 4;
            int colIndex = 1;

            //
            //取得标题
            //
            foreach (DataColumn col in dataSet.Tables[0].Columns)
            {
                colIndex++;
                excel.Cells[rowIndex, colIndex] = col.ColumnName;
                xSt.get_Range(excel.Cells[rowIndex, colIndex], excel.Cells[rowIndex, colIndex]).HorizontalAlignment = XlVAlign.xlVAlignCenter;//设置标题格式为居中对齐
            }

            //
            //取得表格中的数据
            //
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                rowIndex++;
                colIndex = 1;
                foreach (DataColumn col in dataSet.Tables[0].Columns)
                {
                    colIndex++;
                    if (col.DataType == System.Type.GetType("System.DateTime"))
                    {
                        excel.Cells[rowIndex, colIndex] = (Convert.ToDateTime(row[col.ColumnName].ToString())).ToString("yyyy-MM-dd");
                        xSt.get_Range(excel.Cells[rowIndex, colIndex], excel.Cells[rowIndex, colIndex]).HorizontalAlignment = XlVAlign.xlVAlignCenter;//设置日期型的字段格式为居中对齐
                    }
                    else
                        if (col.DataType == System.Type.GetType("System.String"))
                        {
                            excel.Cells[rowIndex, colIndex] = "'" + row[col.ColumnName].ToString();
                            xSt.get_Range(excel.Cells[rowIndex, colIndex], excel.Cells[rowIndex, colIndex]).HorizontalAlignment = XlVAlign.xlVAlignCenter;//设置字符型的字段格式为居中对齐
                        }
                        else
                        {
                            excel.Cells[rowIndex, colIndex] = row[col.ColumnName].ToString();
                        }
                }
            }
            //
            //加载一个合计行
            //
            int rowSum = rowIndex + 1;
            int colSum = 2;
            excel.Cells[rowSum, 2] = "合计";
            xSt.get_Range(excel.Cells[rowSum, 2], excel.Cells[rowSum, 2]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            //
            //设置选中的部分的颜色
            //
            xSt.get_Range(excel.Cells[rowSum, colSum], excel.Cells[rowSum, colIndex]).Select();
            xSt.get_Range(excel.Cells[rowSum, colSum], excel.Cells[rowSum, colIndex]).Interior.ColorIndex =19;//设置为浅黄色，共计有56种
            //
            //取得整个报表的标题
            //
            excel.Cells[2, 2] = str;
            //
            //设置整个报表的标题格式
            //
            xSt.get_Range(excel.Cells[2, 2], excel.Cells[2, 2]).Font.Bold = true;
            xSt.get_Range(excel.Cells[2, 2], excel.Cells[2, 2]).Font.Size = 22;
            //
            //设置报表表格为最适应宽度
            //
            xSt.get_Range(excel.Cells[4, 2], excel.Cells[rowSum, colIndex]).Select();
            xSt.get_Range(excel.Cells[4, 2], excel.Cells[rowSum, colIndex]).Columns.AutoFit();
            //
            //设置整个报表的标题为跨列居中
            //
            xSt.get_Range(excel.Cells[2, 2], excel.Cells[2, colIndex]).Select();
            xSt.get_Range(excel.Cells[2, 2], excel.Cells[2, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenterAcrossSelection;
            //
            //绘制边框
            //
            xSt.get_Range(excel.Cells[4, 2], excel.Cells[rowSum, colIndex]).Borders.LineStyle = 1;
            xSt.get_Range(excel.Cells[4, 2], excel.Cells[rowSum, 2]).Borders[XlBordersIndex.xlEdgeLeft].Weight = XlBorderWeight.xlThick;//设置左边线加粗
            xSt.get_Range(excel.Cells[4, 2], excel.Cells[4, colIndex]).Borders[XlBordersIndex.xlEdgeTop].Weight= XlBorderWeight.xlThick;//设置上边线加粗
            xSt.get_Range(excel.Cells[4, colIndex], excel.Cells[rowSum, colIndex]).Borders[XlBordersIndex.xlEdgeRight].Weight = XlBorderWeight.xlThick;//设置右边线加粗
            xSt.get_Range(excel.Cells[rowSum, 2], excel.Cells[rowSum, colIndex]).Borders[XlBordersIndex.xlEdgeBottom].Weight = XlBorderWeight.xlThick;//设置下边线加粗
            //
            //显示效果
            //
            excel.Visible = true;

            //xSt.Export(Server.MapPath(".")+"\\"+this.xlfile.Text+".xls",SheetExportActionEnum.ssExportActionNone,Microsoft.Office.Interop.OWC.SheetExportFormat.ssExportHTML);xBk.SaveCopyAs(Server.MapPath(".")+"\\"+this.xlfile.Text+".xls");

            //ds = null;
            xBk.Close(false, null, null);

            excel.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xBk);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xSt);
            xBk = null;
            excel = null;
            xSt = null;
            GC.Collect();
            //string path = Server.MapPath(this.xlfile.Text + ".xls");

            //System.IO.FileInfo file = new System.IO.FileInfo(path);
            //Response.Clear();
            //Response.Charset = "GB2312";
            //Response.ContentEncoding = System.Text.Encoding.UTF8;
            //// 添加头信息，为"文件下载/另存为"对话框指定默认文件名
            //Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(file.Name));
            //// 添加头信息，指定文件大小，让浏览器能够显示下载进度
            //Response.AddHeader("Content-Length", file.Length.ToString());

            //// 指定返回的是一个不能被客户端读取的流，必须被下载
            //Response.ContentType = "application/ms-excel";

            //// 把文件流发送到客户端
            //Response.WriteFile(file.FullName);
            //// 停止页面的执行

            //Response.End();
        }
    }
}

