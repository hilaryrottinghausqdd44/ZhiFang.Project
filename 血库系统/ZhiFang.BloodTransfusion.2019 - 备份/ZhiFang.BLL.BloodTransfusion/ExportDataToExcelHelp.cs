using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using ZhiFang.BloodTransfusion.Common;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.BLL.BloodTransfusion
{
    public class ExportDataToExcelHelp
    {
        /// <summary>
        /// 获取当前Excel填充的公共填充数据项信息
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static ExportExcelCommand CreateExportExcelCommand(string startDate, string endDate)
        {
            ExportExcelCommand eec = new ExportExcelCommand();
            eec.EEC_StartDate = startDate;
            eec.EEC_EndDate = endDate;
            eec.EEC_EmployeeName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
            eec.EEC_DeptName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.HRDeptName);
            eec.EEC_LabName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(Entity.Base.SysPublicSet.SysDicCookieSession.LabName);
            //ZhiFang.Common.Log.Log.Error("CreateExportExcelCommand.部门名称:" + eec.EEC_DeptName);
            return eec;
        }
        public static Stream ExportDataToXSSFSheet<Doc, Dtl>(Doc docModel, IList<Dtl> dtlList, ExportExcelCommand excelCommand, string breportType, long labId, string frx, string excelFile, ref string saveFullPath)
        {
            FileStream stream = null;
            string fileName = excelFile;
            string savePath = NPOIExcelToExporHelp.GetSaveExcelPath(labId, breportType);
            saveFullPath = savePath + "\\" + fileName;
            string templateFullDir = ReportBTemplateHelp.GetBTemplateFullDir(labId, ExcelReportHelp.PublicTemplateDir, breportType, frx);
            if (string.IsNullOrEmpty(templateFullDir))
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取Excel模板信息为空!");
            }
            Stream fsTemplate = File.Open(templateFullDir, FileMode.Open, FileAccess.Read);
            try
            {
                IWorkbook wbReslut;
                //Excel模板Sheet
                ISheet templateSheet;
                //IWorkbook wbReslut;
                ISheet resultSheet;
                //string sheetName = templateSheet.SheetName;
                if (frx.Contains(".xlsx"))
                {
                    wbReslut = ExcelRuleInfoHelp.ReadTemplateIWorkbook(fsTemplate) as XSSFWorkbook;
                    templateSheet = wbReslut.GetSheetAt(1) as XSSFSheet;
                    // ((XSSFSheet)wbReslut.GetSheetAt(1)).CopySheet(wbReslut.GetSheetAt(1).SheetName, true);
                    resultSheet = wbReslut.CreateSheet() as XSSFSheet;
                }
                else
                {
                    wbReslut = ExcelRuleInfoHelp.ReadTemplateIWorkbook(fsTemplate) as HSSFWorkbook;
                    templateSheet = wbReslut.GetSheetAt(1) as HSSFSheet;
                    resultSheet = wbReslut.CreateSheet() as HSSFSheet;
                }
                SetResultSheet(ref templateSheet, ref resultSheet);

                int headRowIndex = 0;//标题行的行索引
                //明细填充内容格式行的列信息（列索引,填充列实体属性名称）
                Dictionary<int, string> dtlEntityProps = new Dictionary<int, string>();
                //明细填充合计格式行的列信息（列索引,填充列实体属性名称）
                Dictionary<int, string> sumEntityProps = new Dictionary<int, string>();
                //Excel模板填充行分类<模板行索引,行所属分类>
                Dictionary<int, string> templateRows = GetTemplateRows<Dtl>(templateSheet, ref dtlEntityProps, ref sumEntityProps, ref headRowIndex);
                int curRowIndex = 0;//当前填充行的行索引
                IRow curRow;//当前填充行信息
                IRow sourceRow;//模板行信息
                Dimension currentDimension;
                bool isMergeCell = false;
                //模板Excel里的所属图片集合信息
                List<PicturesInfo> picListAll = NpoiExtendOfPictures.GetAllPictureInfos(templateSheet);
                //ZhiFang.Common.Log.Log.Debug("picListAll.Count:" + picListAll.Count());
                #region 标题行顶部的行信息填充
                if (headRowIndex > 0)
                {
                    for (int i = 0; i < headRowIndex; i++)
                    {
                        curRowIndex = i;
                        sourceRow = templateSheet.GetRow(curRowIndex);

                        if (frx.Contains(".xlsx"))
                        {
                            curRow = resultSheet.CreateRow(curRowIndex) as XSSFRow;
                        }
                        else
                        {
                            curRow = resultSheet.CreateRow(curRowIndex) as HSSFRow;
                        }
                        CreateRow(resultSheet, curRow, sourceRow, curRowIndex, true);
                        //CreatePicture(wbReslut, resultSheet, curRowIndex, picListAll, frx);
                        //isMergeCell = ExcelExtension.IsMergeCell(templateSheet, curRowIndex, 0, out currentDimension);
                        //if (isMergeCell)
                        //{
                        //    //List<PicturesInfo> picList = NpoiExtendOfPictures.GetAllPictureInfos(templateSheet, currentDimension.FirstRowIndex, currentDimension.LastRowIndex, currentDimension.FirstColumnIndex, currentDimension.LastColumnIndex, true);
                        //    //ZhiFang.Common.Log.Log.Debug("picList.Count:" + picList.Count());
                        //}
                    }
                }
                #endregion
                #region  标题行填充
                curRowIndex = headRowIndex;
                sourceRow = templateSheet.GetRow(curRowIndex);
                string headRowValue = sourceRow.GetCell(0).StringCellValue;

                ICellStyle cellStyle;
                if (headRowValue.Length >= 4 && headRowValue.Substring(0, 4).Equals("{H|}"))
                {
                    headRowValue = headRowValue.Substring(4);
                }
                if (frx.Contains(".xlsx"))
                {
                    XSSFFont cellFont = wbReslut.CreateFont() as XSSFFont;
                    cellStyle = wbReslut.CreateCellStyle() as XSSFCellStyle;
                    cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                    cellFont.FontHeightInPoints = 20;
                    cellFont.Boldweight = 700;
                    cellStyle.SetFont(cellFont);
                    curRow = resultSheet.CreateRow(curRowIndex) as XSSFRow;
                    curRow.HeightInPoints = 25;
                    curRow.CreateCell(0).SetCellValue(headRowValue);
                    curRow.GetCell(0).CellStyle = cellStyle;
                }
                else
                {
                    cellStyle = wbReslut.CreateCellStyle() as HSSFCellStyle;
                    cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                    HSSFFont cellFont = wbReslut.CreateFont() as HSSFFont;
                    cellFont.FontHeightInPoints = 20;
                    cellFont.Boldweight = 700;
                    cellStyle.SetFont(cellFont);
                    curRow = resultSheet.CreateRow(curRowIndex) as HSSFRow;
                    curRow.CreateCell(0).SetCellValue(headRowValue);
                    curRow.GetCell(0).CellStyle = cellStyle;
                }
                if (headRowValue.Contains("{H|") && headRowValue.Contains("}"))
                {
                    ExcelReportHelp.SetDocCellValueOfHead<Doc>(docModel, curRow, headRowValue);
                }
                isMergeCell = ExcelExtension.IsMergeCell(templateSheet, curRowIndex, 0, out currentDimension);
                if (isMergeCell)
                {
                    CellRangeAddress region = new CellRangeAddress(currentDimension.FirstRowIndex, currentDimension.LastRowIndex, currentDimension.FirstColumnIndex, currentDimension.LastColumnIndex);
                    if (frx.Contains(".xlsx"))
                    {
                        (resultSheet as XSSFSheet).AddMergedRegion(region);
                    }
                    else
                    {
                        (resultSheet as HSSFSheet).AddMergedRegion(region);
                    }
                }
                #endregion
                //页眉区域填充内容行填充
                var tRows = templateRows.Where(p => p.Value == "T").OrderBy(p => p.Key);
                if (tRows.Count() > 0) CreateTitleRow<Doc>(templateSheet, resultSheet, tRows, docModel, excelCommand, frx, ref curRow, ref curRowIndex);
                // 明细列表标题内容行填充
                var cRows = templateRows.Where(p => p.Value == "C").OrderBy(p => p.Key);
                if (cRows.Count() > 0) CreateCDtlRow<Dtl>(templateSheet, resultSheet, cRows, frx, ref curRow, ref curRowIndex);
                //明细列表内容行填充
                IEnumerable<KeyValuePair<int, string>> dRows = templateRows.Where(p => p.Value == "D").OrderBy(p => p.Key);
                if (dRows.Count() > 0) CreateDtlRow<Dtl>(templateSheet, resultSheet, dRows, dtlList, dtlEntityProps, excelCommand, frx, ref curRow, ref curRowIndex);
                //明细列表合计行填充--未实现
                var sRows = templateRows.Where(p => p.Value == "S").OrderBy(p => p.Key);
                if (sRows.Count() > 0) CreateSumDtlRow<Dtl>(templateSheet, resultSheet, sRows, dtlList, sumEntityProps, frx, ref curRow, ref curRowIndex);

                #region 补充明细内容空白行
                if (dtlList.Count < 5)
                {
                    var sRowIndex = dRows.ElementAt(0).Key + 1;
                    sourceRow = templateSheet.GetRow(sRowIndex);
                    for (int rowIndex = 0; rowIndex < 5; rowIndex++)
                    {
                        //需要考虑是否存在合计行
                        curRowIndex = sRows.Count() > 0 ? curRowIndex + 2 : curRowIndex + 1;
                        if (frx.Contains(".xlsx"))
                        {
                            curRow = resultSheet.CreateRow(curRowIndex) as XSSFRow;
                        }
                        else
                        {
                            curRow = resultSheet.CreateRow(curRowIndex) as HSSFRow;
                        }
                        CreateRow(resultSheet, curRow, sourceRow, curRowIndex, true);
                    }
                }
                #endregion
                //页脚区域填充
                var fRows = templateRows.Where(p => p.Value == "F").OrderBy(p => p.Key);
                if (fRows.Count() > 0) CreateFooterRow<Doc>(templateSheet, resultSheet, fRows, docModel, excelCommand, frx, ref curRow, ref curRowIndex);

                using (var st = new FileStream(saveFullPath, FileMode.Create, FileAccess.ReadWrite))
                {
                    //删除原来的Sheet
                    if (frx.Contains(".xlsx"))
                    {
                        ((XSSFWorkbook)wbReslut).RemoveAt(1);//模板Sheet
                        ((XSSFWorkbook)wbReslut).RemoveAt(0);//样式Sheet
                    }
                    else
                    {
                        ((HSSFWorkbook)wbReslut).RemoveAt(1);
                        ((HSSFWorkbook)wbReslut).RemoveAt(0);
                    }
                    wbReslut.Write(st);
                }
                stream = new FileStream(saveFullPath, FileMode.Open, FileAccess.Read);
            }
            catch (Exception ex)
            {
                fsTemplate.Close();
                ZhiFang.Common.Log.Log.Error("错误信息:" + ex.StackTrace);
                throw ex;
            }
            fsTemplate.Close();
            return stream;
        }
        private static void SetResultSheet(ref ISheet templateSheet, ref ISheet resultSheet)
        {
            //ZhiFang.Common.Log.Log.Error("IWorkbook.picList.Count:" + picList2.Count);
            resultSheet.Header.Center = templateSheet.Header.Center;
            resultSheet.Header.Left = templateSheet.Header.Left;
            resultSheet.Header.Left = templateSheet.Header.Right;
            resultSheet.Footer.Center = templateSheet.Header.Center;
            resultSheet.Footer.Left = templateSheet.Header.Left;
            resultSheet.Footer.Left = templateSheet.Header.Right;
        }
        /// <summary>
        /// 页眉区域填充内容行填充
        /// </summary>
        private static void CreateTitleRow<Doc>(ISheet templateSheet, ISheet resultSheet, IEnumerable<KeyValuePair<int, string>> tRows, Doc docModel, ExportExcelCommand excelCommand, string frx, ref IRow curRow, ref int curRowIndex)
        {
            foreach (var item in tRows)
            {
                curRowIndex = item.Key;
                if (frx.Contains(".xlsx"))
                    curRow = resultSheet.CreateRow(curRowIndex) as XSSFRow;
                else
                    curRow = resultSheet.CreateRow(curRowIndex) as HSSFRow;
                IRow sourceRow = templateSheet.GetRow(item.Key);
                CreateRow(resultSheet, curRow, sourceRow, curRowIndex, true);
                int cellCount = curRow.LastCellNum;
                #region  for ICell
                for (int cellIndex = 0; cellIndex < cellCount; cellIndex++)
                {
                    ICell cell = curRow.GetCell(cellIndex);
                    if (cell == null) continue;
                    string cellValue = ExcelRuleInfoHelp.MyGetCellValue(cell);
                    if (string.IsNullOrEmpty(cellValue))
                    {
                        cell.SetCellValue("");
                        continue;
                    }
                    if (cellValue.Equals("{T|}"))
                    {
                        cell.SetCellValue("");
                    }
                    else if (cellValue.Length >= 4 && cellValue.Substring(0, 4).Equals("{T|}"))
                    {
                        //带有文字说明的页眉单元格
                        cell.SetCellValue(cellValue.Substring(4));
                    }
                    else if (cellValue.Contains("{T|") && cellValue.LastIndexOf("}") == cellValue.Length - 1)
                    {
                        //带有填充字段的页眉单元格
                        ExcelReportHelp.SetDocCellValueOfTitle<Doc>(docModel, excelCommand, curRow.GetCell(cellIndex), cellValue);
                    }
                }
                #endregion
                //CreatePicture(wbReslut, resultSheet, curRowIndex, picListAll, frx);
                //isMergeCell = ExcelExtension.IsMergeCell(templateSheet, curRowIndex, 0, out currentDimension);
                //if (isMergeCell)
                //{
                //    ZhiFang.Common.Log.Log.Debug("{T|}.FirstRowIndex:" + currentDimension.FirstRowIndex + ",LastRowIndex:" + currentDimension.LastRowIndex + ",FirstColumnIndex:" + currentDimension.FirstColumnIndex + ",LastColumnIndex:" + currentDimension.LastColumnIndex);

                //    if (frx.Contains(".xlsx"))
                //    {
                //        (resultSheet as XSSFSheet).AddMergedRegion(new CellRangeAddress(currentDimension.FirstRowIndex, currentDimension.LastRowIndex, currentDimension.FirstColumnIndex, currentDimension.LastColumnIndex));
                //    }
                //    else
                //    {
                //        (resultSheet as HSSFSheet).AddMergedRegion(new Region(currentDimension.FirstRowIndex, currentDimension.FirstColumnIndex, currentDimension.LastRowIndex, currentDimension.LastColumnIndex));
                //    }
                //}
            }
        }
        /// <summary>
        /// 明细列表标题内容行填充
        /// </summary>
        private static void CreateCDtlRow<Dtl>(ISheet templateSheet, ISheet resultSheet, IEnumerable<KeyValuePair<int, string>> cRows, string frx, ref IRow curRow, ref int curRowIndex)
        {
            foreach (var item in cRows)
            {
                curRowIndex = item.Key;
                if (frx.Contains(".xlsx"))
                    curRow = resultSheet.CreateRow(curRowIndex) as XSSFRow;
                else
                    curRow = resultSheet.CreateRow(curRowIndex) as HSSFRow;
                IRow sourceRow = templateSheet.GetRow(item.Key);
                CreateRow(resultSheet, curRow, sourceRow, curRowIndex, true);
                int cellCount = curRow.LastCellNum;
                #region  for ICell
                for (int cellIndex = 0; cellIndex < cellCount; cellIndex++)
                {
                    ICell cell = curRow.GetCell(cellIndex);
                    if (cell == null) continue;
                    string cellValue = ExcelRuleInfoHelp.MyGetCellValue(cell);
                    if (string.IsNullOrEmpty(cellValue))
                    {
                        cell.SetCellValue("");
                        continue;
                    }
                    if (cellValue.IndexOf('[') == 0 && cellValue.LastIndexOf("]") == cellValue.Length - 1)
                    {
                        cellValue = cellValue.Substring(1, cellValue.Length - 2);
                        curRow.GetCell(cellIndex).SetCellValue(cellValue);
                    }
                    //设置列宽:取模板原列宽
                    resultSheet.SetColumnWidth(cellIndex, templateSheet.GetColumnWidth(cellIndex));
                }
                #endregion
            }
        }
        /// <summary>
        /// 明细列表内容行填充
        /// </summary>
        private static void CreateDtlRow<Dtl>(ISheet templateSheet, ISheet resultSheet, IEnumerable<KeyValuePair<int, string>> dRows, IList<Dtl> dtlList, Dictionary<int, string> dtlEntityProps, ExportExcelCommand excelCommand, string frx, ref IRow curRow, ref int curRowIndex)
        {
            //获取明细列表填充规格行的下一行(模板行)
            IRow sourceRow = templateSheet.GetRow(dRows.ElementAt(0).Key);// + 1
            for (int rowIndex = 0; rowIndex < dtlList.Count; rowIndex++)
            {
                curRowIndex = curRowIndex + 1;
                if (frx.Contains(".xlsx"))
                {
                    curRow = resultSheet.CreateRow(curRowIndex) as XSSFRow;
                }
                else
                {
                    curRow = resultSheet.CreateRow(curRowIndex) as HSSFRow;
                }
                CreateRow(resultSheet, curRow, sourceRow, curRowIndex, true);
                int cellCount = curRow.LastCellNum;
                #region  for ICell
                string entityProperty = "";
                for (int cellIndex = 0; cellIndex < cellCount; cellIndex++)
                {
                    ICell cell = curRow.GetCell(cellIndex);
                    if (cell == null) continue;
                    //cell.SetCellValue("");
                    dtlEntityProps.TryGetValue(cellIndex, out entityProperty);
                    if (!string.IsNullOrEmpty(entityProperty))
                        entityProperty = entityProperty.Trim();
                    //ZhiFang.Common.Log.Log.Debug("entityProperty:" + entityProperty + "");
                    if (entityProperty == "DispOrder")
                    {
                        cell.SetCellValue(rowIndex + 1);
                    }
                    else if (!string.IsNullOrEmpty(entityProperty))
                    {
                        string cellValue = "";
                        //ZhiFang.Common.Log.Log.Debug("entityProperty:" + entityProperty + ",");
                        ExcelReportHelp.SetDtlCellValue<Dtl>(dtlList[rowIndex], excelCommand, cell, entityProperty, ref cellValue);
                    }
                    else
                    {
                        cell.SetCellValue("");
                    }
                }
                #endregion
            }
        }
        /// <summary>
        /// 合计区域内容行填充
        /// </summary>
        private static void CreateSumDtlRow<Dtl>(ISheet templateSheet, ISheet resultSheet, IEnumerable<KeyValuePair<int, string>> sRows, IList<Dtl> dtlList, Dictionary<int, string> sumEntityProps, string frx, ref IRow curRow, ref int curRowIndex)
        {
            string entityProperty = "";
            PropertyInfo[] props = ExcelReportHelp.GetProperties<Dtl>();
            //获取明细列表合计行模板信息
            IRow sourceRow = templateSheet.GetRow(sRows.ElementAt(0).Key);// + 1
            curRowIndex = curRowIndex + 1;
            if (frx.Contains(".xlsx"))
            {
                curRow = resultSheet.CreateRow(curRowIndex) as XSSFRow;
            }
            else
            {
                curRow = resultSheet.CreateRow(curRowIndex) as HSSFRow;
            }
            CreateRow(resultSheet, curRow, sourceRow, curRowIndex, true);
            int cellCount = curRow.LastCellNum;
            #region for ICell
            for (int cellIndex = 0; cellIndex < cellCount; cellIndex++)
            {
                ICell cell = curRow.GetCell(cellIndex);
                if (cell == null) continue;
                string cellValue = ExcelRuleInfoHelp.MyGetCellValue(cell);
                if (string.IsNullOrEmpty(cellValue))
                {
                    //cell.SetCellValue("");
                    continue;
                }
                entityProperty = "";
                if (cellValue.Contains("{S|") && cellValue.Contains("}") && cellValue.IndexOf("{S|Sum_") > -1)
                {
                    string cellValue2 = cellValue.Replace("Sum_", "");
                    entityProperty = ExcelReportHelp.GetDtlEntityProperty<Dtl>(cellValue2);
                }
                if (string.IsNullOrEmpty(entityProperty)) continue;

                //对明细数据项求和
                var propertyInfo = props.Where(p => p.Name == entityProperty);
                if (propertyInfo == null || propertyInfo.Count() <= 0) continue;
                double sumResult = 0;
                for (int i = 0; i < dtlList.Count - 1; i++)
                {
                    object entityValue = propertyInfo.ElementAt(0).GetValue(dtlList[i], null);
                    double tempSum = 0;
                    if (Double.TryParse(entityValue.ToString(), out tempSum))
                    {
                        sumResult = sumResult + tempSum;
                    }
                }
                sumResult = Math.Round(sumResult, 2);
                cellValue = cellValue.Replace("{S|Sum_" + entityProperty + "}", sumResult.ToString());
                cell.SetCellValue(cellValue);
            }
            #endregion
        }
        /// <summary>
        /// 页脚区域内容行填充
        /// </summary>
        private static void CreateFooterRow<Doc>(ISheet templateSheet, ISheet resultSheet, IEnumerable<KeyValuePair<int, string>> fRows, Doc docModel, ExportExcelCommand excelCommand, string frx, ref IRow curRow, ref int curRowIndex)
        {
            foreach (var item in fRows)
            {
                curRowIndex = curRowIndex + 1;
                if (frx.Contains(".xlsx"))
                    curRow = resultSheet.CreateRow(curRowIndex) as XSSFRow;
                else
                    curRow = resultSheet.CreateRow(curRowIndex) as HSSFRow;
                IRow sourceRow = templateSheet.GetRow(item.Key);
                //curRow = sourceRow;
                CreateRow(resultSheet, curRow, sourceRow, curRowIndex, true);
                int cellCount = curRow.LastCellNum;
                #region  for ICell
                for (int cellIndex = 0; cellIndex < cellCount; cellIndex++)
                {
                    ICell cell = curRow.GetCell(cellIndex);
                    if (cell == null) continue;
                    string cellValue = ExcelRuleInfoHelp.MyGetCellValue(cell);
                    if (string.IsNullOrEmpty(cellValue))
                    {
                        cell.SetCellValue("");
                        continue;
                    }
                    if (cellValue.Equals("{F|}"))
                    {
                        //空的页脚单元格
                        cell.SetCellValue("");
                    }
                    else if (cellValue.Length >= 4 && cellValue.Substring(0, 4).Equals("{F|}"))
                    {
                        //带有文字说明的页脚单元格
                        cell.SetCellValue(cellValue.Substring(4));
                    }
                    else if (cellValue.Contains("{F|") && cellValue.LastIndexOf("}") == cellValue.Length - 1)
                    {
                        //带有填充字段的页脚单元格
                        ExcelReportHelp.SetDocCellValueOfFooter<Doc>(docModel, excelCommand, curRow.GetCell(cellIndex), cellValue);
                    }

                }
                #endregion
                //CreatePicture(wbReslut, resultSheet, curRowIndex, picListAll, frx);
            }
        }
        private static void CreatePicture(IWorkbook wbReslut, ISheet resultSheet, int curRowIndex, List<PicturesInfo> picListAll, string frx)
        {
            foreach (PicturesInfo shape in picListAll)
            {
                if (shape.MinRow == curRowIndex)// && curRowIndex <= shape.MaxRow
                {
                    int pictureIdx = wbReslut.AddPicture(shape.PictureData, PictureType.PNG);
                    IDrawing patriarch = resultSheet.CreateDrawingPatriarch();
                    if (frx.Contains(".xlsx"))
                    {
                        //XSSFClientAnchor anchor1 = new XSSFClientAnchor(0, 0, 0, 0, shape.MinCol, shape.MinRow, shape.MaxCol, shape.MaxRow);
                        XSSFClientAnchor anchor1 = new XSSFClientAnchor(shape.Dx1, shape.Dy1, shape.Dx2, shape.Dy2, shape.MinCol, shape.MinRow, shape.MaxCol, shape.MaxRow);
                        //不移动不要调整大小
                        //anchor1.AnchorType = AnchorType.DontMoveAndResize;
                        XSSFPicture picture = (XSSFPicture)patriarch.CreatePicture(anchor1, pictureIdx);
                        //picture.Resize();
                    }
                    else
                    {
                        //第四步：设置锚点 （在起始单元格的X坐标0-1023，Y的坐标0-255，在终止单元格的X坐标0-1023，Y的坐标0-255，起始单元格行数，列数，终止单元格行数，列数）
                        IClientAnchor anchor = patriarch.CreateAnchor(0, 0, 0, 0, shape.MinCol, shape.MinRow, shape.MaxCol, shape.MaxRow);
                        //第五步：创建图片
                        IPicture picture = patriarch.CreatePicture(anchor, pictureIdx);
                    }
                }
            }
        }
        /// <summary>
        /// 获取填充Excel模板信息
        /// </summary>
        public static Dictionary<int, string> GetTemplateRows<Dtl>(ISheet sheetTemplate, ref Dictionary<int, string> dtlEntityProps, ref Dictionary<int, string> sumEntityProps, ref int headRowIndex)
        {
            Dictionary<int, string> templateRows = new Dictionary<int, string>();
            IRow curRow;
            int rowCount = sheetTemplate.LastRowNum;
            //ZhiFang.Common.Log.Log.Error("sheetTemplate.LastRowNum:" + rowCount);
            #region 填充单元格处理
            string type = "";
            for (int rowIndex = 1; rowIndex <= rowCount; rowIndex++)
            {
                curRow = sheetTemplate.GetRow(rowIndex);
                if (curRow != null)
                {
                    int cellCount = curRow.LastCellNum;
                    #region  for ICell
                    for (int cellIndex = 0; cellIndex < cellCount; cellIndex++)
                    {
                        ICell cell = curRow.GetCell(cellIndex);
                        if (cell == null) continue;
                        string cellValue = ExcelRuleInfoHelp.MyGetCellValue(cell);
                        if (string.IsNullOrEmpty(cellValue)) continue;

                        if (cellValue.Contains("{H|") && cellValue.Contains("}"))
                        {
                            type = "H";//头部区域
                            headRowIndex = rowIndex;
                            break;
                        }
                        if (cellValue.Contains("{T|") && cellValue.Contains("}"))
                        {
                            type = "T";//页头区域
                            break;
                        }
                        else if (cellValue.IndexOf('[') == 0 && cellValue.LastIndexOf("]") == cellValue.Length - 1)
                        {
                            type = "C";//明细数据项区域标题栏
                            break;
                        }
                        else if (cellValue.Contains("{D|") && cellValue.Contains("}"))
                        {
                            type = "D";//明细数据项区域
                            string entityProperty = "";
                            if (cellValue.IndexOf("EEC_") > 0)//公共填充数据项
                            {
                                entityProperty = ExcelReportHelp.GetDtlEntityProperty<ExportExcelCommand>(cellValue);
                            }
                            else
                            {
                                entityProperty = ExcelReportHelp.GetDtlEntityProperty<Dtl>(cellValue); ;
                            }
                            if (!dtlEntityProps.ContainsKey(cellIndex))
                                dtlEntityProps.Add(cellIndex, entityProperty);
                        }
                        else if (cellValue.Contains("{S|") && cellValue.Contains("}") && cellValue.IndexOf("{S|Sum_") > -1)
                        {
                            type = "S";//明细数据项合计区域
                            cellValue = cellValue.Replace("Sum_", "");
                            string entityProperty = ExcelReportHelp.GetDtlEntityProperty<Dtl>(cellValue);
                            if (!sumEntityProps.ContainsKey(cellIndex) && !string.IsNullOrEmpty(entityProperty))
                                sumEntityProps.Add(cellIndex, entityProperty);
                        }
                        else if (cellValue.Contains("{F|") && cellValue.Contains("}"))
                        {
                            type = "F";//页脚区域
                        }
                        else
                        {
                            type = "";
                        }
                    }
                    #endregion
                    if (!templateRows.ContainsKey(rowIndex))
                        templateRows.Add(rowIndex, type);
                }
            }
            #endregion
            return templateRows;
        }
        public static void CreateRow(ISheet sheet, IRow curRow, IRow sourceRow, int curRowIndex, bool isMergedCell)
        {
            int sourceCellCount = sourceRow.Cells.Count;
            if (sourceRow.RowStyle != null)
                curRow.RowStyle = sourceRow.RowStyle;
            curRow.Height = sourceRow.Height;//复制行高
            curRow.HeightInPoints = sourceRow.HeightInPoints;

            int firstColIndex = -1; //记录每行的合并单元格起始位置
            int lastColIndex = -1;
            ICell sourceCell = null;
            ICell targetCell = null;
            for (int curColIndex = sourceRow.FirstCellNum; curColIndex < sourceRow.LastCellNum; curColIndex++)
            {
                sourceCell = sourceRow.GetCell(curColIndex);
                if (sourceCell == null) continue;

                targetCell = curRow.CreateCell(curColIndex);
                targetCell.CellStyle = sourceCell.CellStyle;//赋值单元格格式  
                //CopyCellStyle(sourceCell.CellStyle, targetCell.CellStyle);
                string scellValue = ExcelRuleInfoHelp.MyGetCellValue(sourceCell);
                targetCell.SetCellValue(scellValue);

                if (isMergedCell == false) continue;

                //以下为复制模板行的单元格合并格式
                if (sourceCell.IsMergedCell)
                {
                    if (firstColIndex < 0)
                        firstColIndex = curColIndex;
                    else if (firstColIndex >= 0 && sourceCellCount == curColIndex + 1)
                    {
                        lastColIndex = curColIndex;
                        //ZhiFang.Common.Log.Log.Debug("sourceCell.IsMergedCell:curRowIndex:" + curRowIndex + ",lastColIndex:" + lastColIndex+ ",scellValue:"+ scellValue);
                        sheet.AddMergedRegion(new CellRangeAddress(curRowIndex, curRowIndex, firstColIndex, lastColIndex));
                        firstColIndex = -1;
                    }
                }
                else
                {
                    if (firstColIndex >= 0)
                    {
                        lastColIndex = curColIndex - 1;
                        //ZhiFang.Common.Log.Log.Debug("firstColIndex >= 0:curRowIndex:" + curRowIndex + ",curColIndex - 1:" + lastColIndex + ",scellValue:" + scellValue);
                        sheet.AddMergedRegion(new CellRangeAddress(curRowIndex, curRowIndex, firstColIndex, lastColIndex));
                        firstColIndex = -1;
                    }
                }
            }
        }
    }
}
