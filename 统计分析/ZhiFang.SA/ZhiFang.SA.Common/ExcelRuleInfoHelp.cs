using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Newtonsoft.Json.Linq;
using NPOI.SS.Formula.Eval;
using NPOI.SS.UserModel;


namespace ZhiFang.SA.Common
{
    public class ExcelRuleInfoHelp
    {
        public static JObject GetExcelRuleInfo(IList<string> listCell)
        {
            JObject jresult = new JObject();
            //ReportTitle
            string jTitle = "";
            //PageHeader
            string jHeader = "";
            //Data
            string jData = "";
            //PageFooter
            string jPageFooter = "";

            if (listCell != null)
            {
                //页眉填充单元格
                var lTitle = listCell.Where(p => p.Contains("{T|") && p.LastIndexOf("}") == p.Length - 1);
                if (lTitle != null)
                    jTitle= string.Join("&", lTitle.ToArray());

                //数据标题栏填充单元格
                var lHeader = listCell.Where(p => p.Contains("[") && p.LastIndexOf("]") == p.Length - 1);
                if (lHeader != null)
                    jHeader= string.Join("&", lHeader.ToArray());

                //列表数据填充单元格
                var lData = listCell.Where(p => p.Contains("{D|") && p.LastIndexOf("}") == p.Length - 1);
                if (lData != null)
                    jData= string.Join("&", lData.ToArray());
                //页脚填充单元格
                var lPageFooter = listCell.Where(p => p.Contains("{F|") && p.LastIndexOf("}") == p.Length - 1);
                if (lPageFooter != null)
                    jPageFooter= string.Join("&", lPageFooter.ToArray());
            }
            jresult.Add("ReportTitle", jTitle.ToString());
            jresult.Add("PageHeader", jHeader.ToString());
            jresult.Add("Data", jData.ToString());
            jresult.Add("PageFooter", jPageFooter.ToString());

            return jresult;
        }
        public static IWorkbook ReadTemplateIWorkbook(Stream fs)
        {
            //IWorkbook wb;
            try
            {
                return WorkbookFactory.Create(fs);
            }
            catch (Exception ex)
            {
                throw new Exception("读取Excel文件出错" + ex.Message);
            }
        }
        public static ISheet ReadExcelTemplateSheet(Stream fs)
        {
            IWorkbook wb;
            try
            {
                wb = WorkbookFactory.Create(fs);
                //Excel文件的第二个Sheet
                return wb.GetSheetAt(1);
            }
            catch (Exception ex)
            {
                throw new Exception("读取Excel文件出错" + ex.Message);
            }
        }
        public static IList<string> ReadExcelMoudleFile(Stream fs)
        {
            IList<string> listCell = new List<string>();
            IWorkbook wb;
            try
            {
                wb = WorkbookFactory.Create(fs);
                //Excel文件的第二个Sheet
                ISheet sheet = wb.GetSheetAt(1);
                listCell = GetExcelMoudleFile(sheet, 0, true);
            }
            catch (Exception ex)
            {
                throw new Exception("读取Excel文件出错" + ex.Message);
            }
            return listCell;
        }
        public static IList<string> ReadExcelMoudleFile(string path)
        {
            IList<string> listCell = new List<string>();
            IWorkbook wb;
            try
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    wb = WorkbookFactory.Create(fileStream);
                }
                //Excel文件的第二个Sheet
                ISheet sheet = wb.GetSheetAt(1);
                listCell = GetExcelMoudleFile(sheet, 0, true);

                //var excelfile = new ExcelQueryFactory(strFileName);
                ////用另一种方法
                //var tsheet = excelfile.Worksheet(1);

            }
            catch (Exception ex)
            {
                throw new Exception("读取Excel文件出错(" + path + ")：" + ex.Message);
            }
            return listCell;
        }
        public static IList<string> GetExcelMoudleFile(ISheet sheet, int HeaderRowIndex, bool needHeader)
        {
            IList<string> table = new List<string>();
            IRow headerRow;

            try
            {
                int rowCount = sheet.LastRowNum;
                for (int i = 0; i <= rowCount; i++)
                {
                    headerRow = sheet.GetRow(i);
                    if (headerRow != null)
                    {
                        int cellCount = headerRow.LastCellNum;
                        for (int j = 0; j <= cellCount; j++)
                        {
                            ICell cell = headerRow.GetCell(j);
                            if (cell != null)
                            {
                                string str = MyGetCellValue(cell);
                                if (!string.IsNullOrEmpty(str))
                                {
                                    str = str.Replace(",", "，");
                                    table.Add(i.ToString() + "," + j.ToString() + "," + str);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Log.Info("读取Excel文件出错：" + ex.Message);
                throw new Exception(ex.Message);
            }
            return table;
        }
        public static string MyGetCellValue(ICell cell)
        {
            string strValue = "";
            //ZhiFang.Common.Log.Log.Debug("cell.CellType:"+ cell.CellType);
            switch (cell.CellType)
            {
                case CellType.String:
                    string str = cell.StringCellValue;
                    if (str != null && str.Length > 0)
                    {
                        strValue = str.ToString().Trim();
                    }
                    break;
                case CellType.Numeric:
                    if (DateUtil.IsCellDateFormatted(cell))
                    {
                        strValue = DateTime.FromOADate(cell.NumericCellValue).ToString();
                    }
                    else
                    {
                        strValue = Convert.ToDouble(cell.NumericCellValue).ToString();
                    }
                    break;
                case CellType.Boolean:
                    strValue = Convert.ToString(cell.BooleanCellValue);
                    break;
                case CellType.Error:
                    strValue = ErrorEval.GetText(cell.ErrorCellValue);
                    break;
                case CellType.Formula:
                    switch (cell.CachedFormulaResultType)
                    {
                        case CellType.String:
                            string strFORMULA = cell.StringCellValue;
                            if (strFORMULA != null && strFORMULA.Length > 0)
                            {
                                strValue = strFORMULA.ToString().Trim();
                            }
                            break;
                        case CellType.Numeric:
                            strValue = Convert.ToString(cell.NumericCellValue);
                            break;
                        case CellType.Boolean:
                            strValue = Convert.ToString(cell.BooleanCellValue);
                            break;
                        case CellType.Error:
                            strValue = ErrorEval.GetText(cell.ErrorCellValue);
                            break;
                        default:
                            strValue = "";
                            break;
                    }
                    break;
                default:
                    strValue = "";
                    break;
            }
            return strValue;
        }
    }
}
