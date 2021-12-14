using System;
using System.Collections.Generic;
using System.Text;
using NPOI.SS.Util;
using NPOI.HSSF.Util;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using System.Data;
using NPOI.HSSF.UserModel;

namespace ZhiFang.ReagentSys.Client.Common
{
    public class NPOIExcelToExporHelp : MyNPOIHelper
    {
        public static string GetSaveExcelPath(long labId, string subDir)
        {
            string basePath = System.AppDomain.CurrentDomain.BaseDirectory + "\\ExcelExport\\";
            string savePath = "";
            if (labId > 0)
                savePath = labId.ToString() + "\\";
            if (!string.IsNullOrEmpty(subDir))
                savePath = savePath + subDir;
            savePath = basePath + savePath;
            if (!Directory.Exists(savePath))
                Directory.CreateDirectory(savePath);
            return savePath;
        }
        /// <summary>
        /// DataTable导出到Excel文件
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        /// <param name="path">保存位置及保存文件</param>
        public static FileStream ExportDatatoExcel(DataTable dtSource, string strHeaderText, string path, Dictionary<string, short> cellFontStyleList)
        {
            FileStream fs = null;
            try
            {
                string[] temp = path.Split('.');
                if (temp[temp.Length - 1] == "xls" && dtSource.Columns.Count < 256 && dtSource.Rows.Count < 65536)
                {
                    using (MemoryStream ms = ExportDTHelp(dtSource, strHeaderText, cellFontStyleList))
                    {
                        using (fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                        {
                            byte[] data = ms.ToArray();
                            fs.Write(data, 0, data.Length);
                            fs.Flush();
                        }
                    }
                }
                else
                {
                    if (temp[temp.Length - 1] == "xls")
                        path = path + "x";
                    fs = ExportDTByDataTable(dtSource, strHeaderText, path, cellFontStyleList);
                }
            }
            catch (Exception ee)
            {
                //Common.Log.Log.Error("DataTable导出到Excel文件失败:" + ee.StackTrace);
                return null;
            }
            return fs;
        }
        /// <summary>
        /// DataTable导出到Excel的MemoryStream
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        public static MemoryStream ExportDTHelp(DataTable dtSource, string strHeaderText, Dictionary<string, short> cellFontStyleList)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            if (string.IsNullOrEmpty(strHeaderText))
                strHeaderText = "sheet1";
            HSSFSheet sheet = workbook.CreateSheet(strHeaderText) as HSSFSheet;
            HSSFCellStyle dateStyle = workbook.CreateCellStyle() as HSSFCellStyle;
            HSSFDataFormat format = workbook.CreateDataFormat() as HSSFDataFormat;
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");
            int rowIndex = ExportDTByDataTable(dtSource, strHeaderText, ref workbook, sheet, dateStyle, 0, cellFontStyleList);
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                return ms;
            }
        }
        private static int ExportDTByDataTable(DataTable dtSource, string strHeaderText, ref HSSFWorkbook workbook, HSSFSheet sheet, HSSFCellStyle dateStyle, int firstRow, Dictionary<string, short> cellFontStyleList)
        {
            //取得列宽
            int[] arrColWidth = new int[dtSource.Columns.Count];
            foreach (DataColumn item in dtSource.Columns)
            {
                arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
            }
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                for (int j = 0; j < dtSource.Columns.Count; j++)
                {
                    int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                    if (intTemp > arrColWidth[j])
                    {
                        arrColWidth[j] = intTemp;
                    }
                }
            }

            int rowIndex = firstRow;
            bool isHeader = false;
            if (rowIndex == 0)
            {
                isHeader = true;
                HSSFRow tableRow = sheet.CreateRow(0) as HSSFRow;
                tableRow.HeightInPoints = 25;
                tableRow.CreateCell(0).SetCellValue(strHeaderText);

                HSSFCellStyle tableStyle = workbook.CreateCellStyle() as HSSFCellStyle;
                tableStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                HSSFFont tableFont = workbook.CreateFont() as HSSFFont;
                tableFont.FontHeightInPoints = 20;
                tableFont.Boldweight = 700;
                tableStyle.SetFont(tableFont);

                tableRow.GetCell(0).CellStyle = tableStyle;

                sheet.AddMergedRegion(new Region(0, 0, 0, dtSource.Columns.Count - 1));
                //headerRow.Dispose();
                rowIndex = rowIndex + 1;
            }
            else if (rowIndex == firstRow)
                isHeader = true;

            HSSFRow headerRow = sheet.CreateRow(rowIndex) as HSSFRow;
            HSSFCellStyle headStyle = workbook.CreateCellStyle() as HSSFCellStyle;
            headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            headStyle.BorderBottom = BorderStyle.Thin;
            headStyle.BorderLeft = BorderStyle.Thin;
            headStyle.BorderRight = BorderStyle.Thin;
            headStyle.BorderTop = BorderStyle.Thin;
            HSSFFont font = workbook.CreateFont() as HSSFFont;
            font.FontHeightInPoints = 11;
            font.Boldweight = 700;
            headStyle.SetFont(font);

            #region 填充列头，样式

            if (isHeader)
            {
                #region 列头及样式

                foreach (DataColumn column in dtSource.Columns)
                {
                    headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                    headerRow.GetCell(column.Ordinal).CellStyle = headStyle;
                    //设置列宽
                    sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 1) * 256);
                }
                //headerRow.Dispose();
                #endregion
                rowIndex = rowIndex + 1;
            }

            #endregion
            HSSFCellStyle dataRowStyle = workbook.CreateCellStyle() as HSSFCellStyle;
            dataRowStyle.BorderBottom = BorderStyle.Thin;
            dataRowStyle.BorderLeft = BorderStyle.Thin;
            dataRowStyle.BorderRight = BorderStyle.Thin;
            dataRowStyle.BorderTop = BorderStyle.Thin;
            foreach (DataRow row in dtSource.Rows)
            {
                #region 填充内容
                HSSFRow dataRow = sheet.CreateRow(rowIndex) as HSSFRow;
                foreach (DataColumn column in dtSource.Columns)
                {
                    HSSFCell newCell = dataRow.CreateCell(column.Ordinal) as HSSFCell;

                    string drValue = row[column].ToString();
                    dataRow.GetCell(column.Ordinal).CellStyle = dataRowStyle;

                    if (!String.IsNullOrEmpty(drValue) && cellFontStyleList.Count > 0)
                    {
                        foreach (var item in cellFontStyleList)
                        {
                            if (drValue.Contains(item.Key))
                            {
                                XSSFCellStyle dataCellStyle = workbook.CreateCellStyle() as XSSFCellStyle;
                                dataCellStyle.BorderBottom = BorderStyle.Thin;
                                dataCellStyle.BorderLeft = BorderStyle.Thin;
                                dataCellStyle.BorderRight = BorderStyle.Thin;
                                dataCellStyle.BorderTop = BorderStyle.Thin;
                                XSSFFont cellFont = workbook.CreateFont() as XSSFFont;
                                cellFont.Color = item.Value;
                                dataCellStyle.SetFont(cellFont);
                                newCell.CellStyle = dataCellStyle;
                                //ZhiFang.Common.Log.Log.Debug("Color:"+ item.Value);
                                break;
                            }
                        }
                    }
                    switch (column.DataType.ToString())
                    {
                        case "System.String": //字符串类型
                            newCell.SetCellValue(drValue);
                            break;
                        case "System.DateTime": //日期类型
                            DateTime dateV;
                            DateTime.TryParse(drValue, out dateV);
                            newCell.SetCellValue(dateV);

                            newCell.CellStyle = dateStyle; //格式化显示
                            break;
                        case "System.Boolean": //布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16": //整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal": //浮点型
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull": //空值处理
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }
                }

                #endregion
                rowIndex++;
            }
            return rowIndex;
        }
        private static FileStream ExportDTByDataTable(DataTable dtSource, string strHeaderText, string strFileName, Dictionary<string, short> cellFontStyleList)
        {
            int firstRow = 0;
            FileStream fs = null;
            XSSFWorkbook workbook = new XSSFWorkbook();
            if (string.IsNullOrEmpty(strHeaderText))
                strHeaderText = "sheet1";
            XSSFSheet sheet = workbook.CreateSheet(strHeaderText) as XSSFSheet;

            XSSFCellStyle dateStyle = workbook.CreateCellStyle() as XSSFCellStyle;
            XSSFDataFormat format = workbook.CreateDataFormat() as XSSFDataFormat;
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");
            dateStyle.BorderBottom = BorderStyle.Thin;
            dateStyle.BorderLeft = BorderStyle.Thin;
            dateStyle.BorderRight = BorderStyle.Thin;
            dateStyle.BorderTop = BorderStyle.Thin;
            //取得列宽
            int[] arrColWidth = new int[dtSource.Columns.Count];
            foreach (DataColumn item in dtSource.Columns)
            {
                arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
            }
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                for (int j = 0; j < dtSource.Columns.Count; j++)
                {
                    int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                    if (intTemp > arrColWidth[j])
                    {
                        arrColWidth[j] = intTemp;
                    }
                }
            }
            int rowIndex = firstRow;
            bool isHeader = false;

            if (rowIndex == 0)
            {
                isHeader = true;
                XSSFRow tableRow = sheet.CreateRow(rowIndex) as XSSFRow;
                tableRow.HeightInPoints = 25;
                tableRow.CreateCell(0).SetCellValue(strHeaderText);

                XSSFCellStyle tableStyle = workbook.CreateCellStyle() as XSSFCellStyle;
                tableStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                XSSFFont tableFont = workbook.CreateFont() as XSSFFont;
                tableFont.FontHeightInPoints = 20;
                tableFont.Boldweight = 700;
                tableStyle.SetFont(tableFont);

                tableRow.GetCell(0).CellStyle = tableStyle;
                sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, dtSource.Columns.Count - 1));
                //headerRow.Dispose();
                rowIndex = rowIndex + 1;
            }
            else if (rowIndex == firstRow)
                isHeader = true;

            XSSFRow headerRow = sheet.CreateRow(rowIndex) as XSSFRow;
            XSSFCellStyle headStyle = workbook.CreateCellStyle() as XSSFCellStyle;
            headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            headStyle.FillBackgroundColor = HSSFColor.Teal.Index;
            headStyle.BorderBottom = BorderStyle.Thin;
            headStyle.BorderLeft = BorderStyle.Thin;
            headStyle.BorderRight = BorderStyle.Thin;
            headStyle.BorderTop = BorderStyle.Thin;
            XSSFFont font = workbook.CreateFont() as XSSFFont;
            font.FontName = "宋体";
            font.FontHeightInPoints = 11;
            font.Boldweight = 700;
            headStyle.SetFont(font);

            if (isHeader)
            {
                #region 列头及样式

                foreach (DataColumn column in dtSource.Columns)
                {
                    headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                    headerRow.GetCell(column.Ordinal).CellStyle = headStyle;
                    //设置列宽
                    sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 1) * 256);
                }
                //headerRow.Dispose();
                #endregion
                rowIndex = rowIndex + 1;
            }
            //单元格的默认样式
            XSSFCellStyle defaultCellStyle = workbook.CreateCellStyle() as XSSFCellStyle;
            defaultCellStyle.BorderBottom = BorderStyle.Thin;
            defaultCellStyle.BorderLeft = BorderStyle.Thin;
            defaultCellStyle.BorderRight = BorderStyle.Thin;
            defaultCellStyle.BorderTop = BorderStyle.Thin;

            //当前单元格的样式
            XSSFCellStyle curCellStyle = workbook.CreateCellStyle() as XSSFCellStyle;
            curCellStyle.BorderBottom = BorderStyle.Thin;
            curCellStyle.BorderLeft = BorderStyle.Thin;
            curCellStyle.BorderRight = BorderStyle.Thin;
            curCellStyle.BorderTop = BorderStyle.Thin;

            ////单元格的字体样式
            XSSFFont cellFont = workbook.CreateFont() as XSSFFont;

            ZhiFang.Common.Log.Log.Debug("填充内容开始:" + DateTime.Now.ToString());
            string drValue = "";
            foreach (DataRow row in dtSource.Rows)
            {
                XSSFRow dataRow = sheet.CreateRow(rowIndex) as XSSFRow;
                #region 填充内容
                foreach (DataColumn column in dtSource.Columns)
                {
                    XSSFCell newCell = dataRow.CreateCell(column.Ordinal) as XSSFCell;
                    dataRow.GetCell(column.Ordinal).CellStyle = defaultCellStyle;
                    drValue = row[column].ToString();
                    if (!String.IsNullOrEmpty(drValue) && cellFontStyleList.Count > 0)
                    {
                        foreach (var item in cellFontStyleList)
                        {
                            if (drValue.Contains(item.Key))
                            {
                                cellFont.Color = item.Value;
                                curCellStyle.SetFont(cellFont);
                                newCell.CellStyle = curCellStyle;
                                //ZhiFang.Common.Log.Log.Debug("Color:"+ item.Value);
                                break;
                            }
                        }
                    }
                    // ZhiFang.Common.Log.Log.Debug("填充DataType:" + column.DataType.ToString());
                    switch (column.DataType.ToString())
                    {
                        case "System.String": //字符串类型
                            newCell.SetCellValue(drValue);
                            break;
                        case "System.DateTime": //日期类型
                            if (!string.IsNullOrEmpty(drValue))
                            {
                                DateTime dateV;
                                DateTime.TryParse(drValue, out dateV);
                                newCell.SetCellValue(dateV);
                                newCell.CellStyle = dateStyle; //格式化显示
                            }
                            else
                            {
                                newCell.SetCellValue("");
                                newCell.CellStyle = dateStyle; //格式化显示
                            }
                            break;
                        case "System.Boolean": //布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16": //整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            if (!string.IsNullOrEmpty(drValue))
                            {
                                int intV = 0;
                                int.TryParse(drValue, out intV);
                                newCell.SetCellValue(intV);
                            }
                            break;
                        case "System.Decimal": //浮点型
                        case "System.Double":
                            if (!string.IsNullOrEmpty(drValue))
                            {
                                double doubV = 0;
                                double.TryParse(drValue, out doubV);
                                newCell.SetCellValue(doubV);
                            }
                            break;
                        case "System.DBNull": //空值处理
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }

                }
                #endregion
                rowIndex++;
            }
            ZhiFang.Common.Log.Log.Debug("填充内容结束:" + DateTime.Now.ToString());
            if (rowIndex > 0)
            {
                ZhiFang.Common.Log.Log.Debug("写入流内容开始:" + DateTime.Now.ToString());
                using (fs = new FileStream(strFileName, FileMode.Create, FileAccess.ReadWrite))
                {
                    workbook.Write(fs);
                    //fs.Flush();
                }
                ZhiFang.Common.Log.Log.Debug("写入流内容结束:" + DateTime.Now.ToString());
            }
            return fs;
        }

        public static Type GetCoreType(Type t)
        {
            if (t != null && IsNullable(t))
            {
                if (!t.IsValueType)
                {
                    return t;
                }
                else
                {
                    return Nullable.GetUnderlyingType(t);
                }
            }
            else
            {
                return t;
            }
        }
        public static bool IsNullable(Type t)
        {
            return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

    }
}
