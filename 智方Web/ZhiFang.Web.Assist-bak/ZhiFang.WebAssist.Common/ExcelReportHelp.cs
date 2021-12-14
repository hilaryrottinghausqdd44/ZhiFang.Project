using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Eval;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace ZhiFang.WebAssist.Common
{
    public class ExcelReportHelp
    {
        /// <summary>
        /// 公共模板目录名称
        /// </summary>
        public static string PublicTemplateDir = "Excel模板";
        /// <summary>
        /// 公共填充数据项前缀部分
        /// </summary>
        public static string ExcelCommandPre = "EEC_";

        public static void CreateEECDataTable(ExportExcelCommand excelCommand, ref DataSet dataSet)
        {
            IList<ExportExcelCommand> eecList = new List<ExportExcelCommand>();
            eecList.Add(excelCommand);
            DataTable eecDt = ReportBTemplateHelp.ToDataTable<ExportExcelCommand>(eecList, null);
            eecDt.TableName = "ExportExcelCommand";
            dataSet.Tables.Add(eecDt);
        }
        #region 填充Excel单元格
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Doc">主单</typeparam>
        /// <param name="excelCommand">公共实体</param>
        /// <param name="curCellValue">当前单元格值</param>
        /// <param name="cellType">规则名称</param>
        /// <param name="separator">分隔符</param>
        /// <returns></returns>
        public static void SetDocCellValuOfHTF<Doc>(Doc docModel, ExportExcelCommand excelCommand, ICell cell, string cellValue, string cellType, string separator)
        {
            if (cellValue.IndexOf(separator) < 0)
            {
                if (cellType == "T")
                {
                    SetDocCellValueOfTitle<Doc>(docModel, excelCommand, cell, cellValue);
                }
                else if (cellType == "F")
                {
                    SetDocCellValueOfFooter<Doc>(docModel, excelCommand, cell, cellValue);
                }
            }
            else
            {
                string[] splitChar = new string[] { separator };//分隔符  
                string[] array = cellValue.Split(splitChar, StringSplitOptions.None);
                string newCellValue = "";
                foreach (var cellValue2 in array)
                {
                    newCellValue += ChangCellValue<Doc>(docModel, excelCommand, cell, cellType, cellValue2);
                }
                cell.SetCellValue(newCellValue);
            }
        }
        public static string ChangCellValue<Doc>(Doc docModel, ExportExcelCommand excelCommand, ICell cell, string cellType, string cellValue)
        {
            //填充的实体属性
            int start = cellValue.IndexOf("{" + cellType + "|");
            int end = cellValue.IndexOf("}");
            string entityProperty = "";
            //if (start >= 0 && end >= 0)
            //    entityProperty = cellValue.Substring(start, end).Trim();

            if (!string.IsNullOrEmpty(cellValue) && start >= 0 && end >= 0)
            {
                int length = end - start;
                entityProperty = cellValue.Substring(start, length).Trim();
            }
            entityProperty = entityProperty.Replace("{" + cellType + "|", "");
            entityProperty = entityProperty.Replace("}", "");
            //ZhiFang.Common.Log.Log.Debug("entityProperty:" + entityProperty);
            if (string.IsNullOrEmpty(entityProperty))
                return cellValue;

            PropertyInfo[] props = null;
            if (entityProperty.IndexOf(ExcelReportHelp.ExcelCommandPre) > -1)
            {
                //公共填充数据项
                props = GetProperties<ExportExcelCommand>();
            }
            else
            {
                //业务实体
                props = GetProperties<Doc>();
            }
            var propertyInfo = props.Where(p => p.Name == entityProperty);
            if (propertyInfo != null && propertyInfo.Count() == 1)
            {
                object entityValue = null;
                if (entityProperty.IndexOf(ExcelReportHelp.ExcelCommandPre) > -1)
                {
                    entityValue = propertyInfo.ElementAt(0).GetValue(excelCommand, null);
                }
                else
                {
                    if (docModel != null)
                        entityValue = propertyInfo.ElementAt(0).GetValue(docModel, null);
                }
                if (entityValue != null)
                    cellValue = cellValue.Replace(entityProperty, entityValue.ToString());
                cellValue = cellValue.Replace("{" + cellType + "|", "");
                cellValue = cellValue.Replace("}", "");
            }
            return cellValue;
        }

        /// <summary>
        /// 获取明细填充规格行的列字段
        /// </summary>
        /// <typeparam name="Dtl"></typeparam>
        /// <param name="cellValue"></param>
        /// <returns></returns>
        public static string GetDtlEntityProperty<entity>(string cellValue)
        {
            //将*{*|*}*先按|分割
            string[] arrData = cellValue.Split('|');
            if (arrData.Length <= 0) return "";
            //再将|*}*按}分割
            string[] arrData2 = arrData[arrData.Length - 1].TrimEnd('}').Trim().Split('}');
            if (arrData2.Length <= 0) return "";
            //填充的实体属性
            string entityProperty = arrData2[0].TrimEnd('}').Trim();
            if (entityProperty == "DispOrder") return entityProperty;

            PropertyInfo[] props = GetProperties<entity>();
            var propertyInfo = props.Where(p => p.Name == entityProperty);

            if (propertyInfo != null && propertyInfo.Count() == 1)
            {
                return entityProperty;
            }
            return "";
        }
        /// <summary>
        /// 明细实体的填充单元格处理
        /// </summary>
        /// <typeparam name="Doc"></typeparam>
        /// <param name="docModel"></param>
        /// <param name="cellValue"></param>
        /// <param name="templateSheet"></param>
        public static string GetEntityValue<Dtl>(Dtl dtlModel, string entityProperty)
        {
            PropertyInfo[] props = GetProperties<Dtl>();
            var propertyInfo = props.Where(p => p.Name == entityProperty);

            if (propertyInfo != null && propertyInfo.Count() == 1)
            {
                object entityValue = propertyInfo.ElementAt(0).GetValue(dtlModel, null);
                if (entityValue == null)
                    return "";
                return entityValue.ToString();
            }
            return "";
        }
        /// <summary>
        /// 主单实体的填充单元格处理
        /// </summary>
        /// <typeparam name="Doc"></typeparam>
        /// <param name="docModel"></param>
        /// <param name="cellValue"></param>
        /// <param name="templateSheet"></param>
        public static void SetDocCellValueOfHead<Doc>(Doc docModel, ExportExcelCommand excelCommand, IRow curRow, string cellValue)
        {
            //填充的实体属性
            int start = cellValue.IndexOf("{H|");
            int end = cellValue.IndexOf("}");
            string entityProperty = "";
            //if (start >= 0 && end >= 0)
            //    entityProperty = cellValue.Substring(start, end).Trim();

            if (!string.IsNullOrEmpty(cellValue) && start >= 0 && end >= 0)
            {
                int length = end - start;
                entityProperty = cellValue.Substring(start, length).Trim();
            }
            entityProperty = entityProperty.Replace("{H|", "");
            entityProperty = entityProperty.Replace("}", "");
            //ZhiFang.Common.Log.Log.Debug("entityProperty:" + entityProperty);
            if (string.IsNullOrEmpty(entityProperty))
            {
                cellValue = cellValue.Replace("{H|", "");
                cellValue = cellValue.Replace("}", "");
                SetCellValueOfPropertyType(curRow.GetCell(0), "String", cellValue);
                return;
            }

            PropertyInfo[] props = null;
            if (entityProperty.IndexOf(ExcelReportHelp.ExcelCommandPre) > -1)
            {
                //公共填充数据项
                props = GetProperties<ExportExcelCommand>();
            }
            else
            {
                //业务实体
                props = GetProperties<Doc>();
            }

            // PropertyInfo[] props = GetProperties<Doc>();
            var propertyInfo = props.Where(p => p.Name == entityProperty);
            if (propertyInfo != null && propertyInfo.Count() == 1)
            {
                object entityValue = null;
                if (entityProperty.IndexOf(ExcelReportHelp.ExcelCommandPre) > -1)
                {
                    entityValue = propertyInfo.ElementAt(0).GetValue(excelCommand, null);
                }
                else
                {
                    if (docModel != null)
                        entityValue = propertyInfo.ElementAt(0).GetValue(docModel, null);
                }
                if (entityValue != null)
                    cellValue = cellValue.Replace(entityProperty, entityValue.ToString());

                cellValue = cellValue.Replace("{H|", "");
                cellValue = cellValue.Replace("}", "");
                string propertyType = propertyInfo.ElementAt(0).PropertyType.Name;
                GetPropertyType(propertyInfo.ElementAt(0).PropertyType, ref propertyType);
                SetCellValueOfPropertyType(curRow.GetCell(0), propertyType, cellValue);
            }
        }
        /// <summary>
        /// 主单实体的填充单元格处理
        /// </summary>
        /// <typeparam name="Doc">填充的主单实体</typeparam>
        /// <param name="docModel">填充的主单实体信息</param>
        /// <typeparam name="excelCommand">公共填充数据项实体信息</typeparam>
        /// <param name="cellValue"></param>
        /// <param name="templateSheet"></param>
        public static void SetDocCellValueOfTitle<Doc>(Doc docModel, ExportExcelCommand excelCommand, ICell cell, string cellValue)
        {
            //填充的实体属性
            int start = cellValue.IndexOf("{T|");
            int end = cellValue.IndexOf("}");
            //ZhiFang.Common.Log.Log.Debug("cellValue:" + cellValue);
            //ZhiFang.Common.Log.Log.Debug("cellValue.start:" + start + ",end:" + end + ",Length:" + cellValue.Length);

            string entityProperty = "";
            if (!string.IsNullOrEmpty(cellValue) && start >= 0 && end >= 0)
            {
                int length = end - start;
                entityProperty = cellValue.Substring(start, length).Trim();
            }

            entityProperty = entityProperty.Replace("{T|", "");
            entityProperty = entityProperty.Replace("}", "");
            //ZhiFang.Common.Log.Log.Debug("entityProperty:" + entityProperty);
            if (string.IsNullOrEmpty(entityProperty))
                return;

            PropertyInfo[] props = null;
            if (entityProperty.IndexOf(ExcelReportHelp.ExcelCommandPre) > -1)
            {
                //公共填充数据项
                props = GetProperties<ExportExcelCommand>();
            }
            else
            {
                //业务实体
                props = GetProperties<Doc>();
            }
            var propertyInfo = props.Where(p => p.Name == entityProperty);
            if (propertyInfo != null && propertyInfo.Count() == 1)
            {
                object entityValue = null;
                if (entityProperty.IndexOf(ExcelReportHelp.ExcelCommandPre) > -1)
                {
                    entityValue = propertyInfo.ElementAt(0).GetValue(excelCommand, null);
                }
                else
                {
                    if (docModel != null)
                        entityValue = propertyInfo.ElementAt(0).GetValue(docModel, null);
                }
                if (entityValue != null)
                    cellValue = cellValue.Replace(entityProperty, entityValue.ToString());
                cellValue = cellValue.Replace("{T|", "");
                cellValue = cellValue.Replace("}", "");

                //当前实体属性值为空并且单元格值等于实体属性名称
                if (entityValue == null && cellValue == entityProperty)
                {
                    cell.SetCellValue("");
                    return;
                }
                string propertyType = propertyInfo.ElementAt(0).PropertyType.Name;
                GetPropertyType(propertyInfo.ElementAt(0).PropertyType, ref propertyType);
                SetCellValueOfPropertyType(cell, propertyType, cellValue);
            }
        }
        /// <summary>
        /// 明细实体的填充单元格处理
        /// </summary>
        /// <typeparam name="Dtl"></typeparam>
        /// <typeparam name="excelCommand">公共填充数据项实体信息</typeparam>
        /// <param name="dtlModel"></param>
        /// <param name="cell"></param>
        /// <param name="entityProperty"></param>
        /// <param name="cellValue"></param>
        public static void SetDtlCellValue<Dtl>(Dtl dtlModel, ExportExcelCommand excelCommand, ICell cell, string entityProperty, ref string cellValue)
        {
            PropertyInfo[] props = null;
            if (entityProperty.IndexOf(ExcelReportHelp.ExcelCommandPre) > -1)//公共填充数据项
            {
                props = GetProperties<ExportExcelCommand>();
            }
            else//业务实体
            {
                props = GetProperties<Dtl>();
            }
            var propertyInfo = props.Where(p => p.Name == entityProperty);
            if (propertyInfo != null && propertyInfo.Count() == 1)
            {
                object entityValue = null;
                if (entityProperty.IndexOf(ExcelReportHelp.ExcelCommandPre) > -1)
                {
                    entityValue = propertyInfo.ElementAt(0).GetValue(excelCommand, null);
                }
                else
                {
                    if (dtlModel != null)
                        entityValue = propertyInfo.ElementAt(0).GetValue(dtlModel, null);
                }
                string propertyType = propertyInfo.ElementAt(0).PropertyType.Name;
                //string cellValue = "";
                if (entityValue != null)
                    cellValue = entityValue.ToString();
                else
                    cellValue = "";
                GetPropertyType(propertyInfo.ElementAt(0).PropertyType, ref propertyType);
                SetCellValueOfPropertyType(cell, propertyType, cellValue);
            }
        }
        /// <summary>
        /// 主单实体的填充单元格处理
        /// </summary>
        /// <typeparam name="Doc"></typeparam>
        /// <param name="docModel"></param>
        /// <typeparam name="excelCommand">公共填充数据项实体信息</typeparam>
        /// <param name="cellValue"></param>
        /// <param name="templateSheet"></param>
        public static void SetDocCellValueOfFooter<Doc>(Doc docModel, ExportExcelCommand excelCommand, ICell cell, string cellValue)
        {
            //填充的实体属性
            int start = cellValue.IndexOf("{F|");
            int end = cellValue.IndexOf("}");
            string entityProperty = "";
            //if (start >= 0 && end >= 0)
            //    entityProperty = cellValue.Substring(start, end).Trim();

            if (!string.IsNullOrEmpty(cellValue) && start >= 0 && end >= 0)
            {
                int length = end - start;
                entityProperty = cellValue.Substring(start, length).Trim();
            }
            entityProperty = entityProperty.Replace("{F|", "");
            entityProperty = entityProperty.Replace("}", "");
            //ZhiFang.Common.Log.Log.Debug("entityProperty:" + entityProperty);
            if (string.IsNullOrEmpty(entityProperty))
                return;

            PropertyInfo[] props = null;
            if (entityProperty.IndexOf(ExcelReportHelp.ExcelCommandPre) > -1)//公共填充数据项
            {
                props = GetProperties<ExportExcelCommand>();
            }
            else//业务实体
            {
                props = GetProperties<Doc>();
            }
            var propertyInfo = props.Where(p => p.Name == entityProperty);

            if (propertyInfo != null && propertyInfo.Count() == 1)
            {
                object entityValue = null;
                if (entityProperty.IndexOf(ExcelReportHelp.ExcelCommandPre) > -1)
                {
                    entityValue = propertyInfo.ElementAt(0).GetValue(excelCommand, null);
                }
                else
                {
                    if (docModel != null)
                        entityValue = propertyInfo.ElementAt(0).GetValue(docModel, null);
                }
                if (entityValue != null)
                    cellValue = cellValue.Replace(entityProperty, entityValue.ToString());
                cellValue = cellValue.Replace("{F|", "");
                cellValue = cellValue.Replace("}", "");
                //当前实体属性值为空并且单元格值等于实体属性名称
                if (entityValue == null && cellValue == entityProperty)
                {
                    cell.SetCellValue("");
                    return;
                }

                string propertyType = propertyInfo.ElementAt(0).PropertyType.Name;
                GetPropertyType(propertyInfo.ElementAt(0).PropertyType, ref propertyType);
                SetCellValueOfPropertyType(cell, propertyType, cellValue);
            }
        }
        public static void GetPropertyType(Type type, ref string propertyType)
        {
            //ZhiFang.Common.Log.Log.Debug("propertyType:" + propertyInfo.ElementAt(0).PropertyType.ToString());
            if (type.ToString() == "System.Nullable`1[System.DateTime]")
            {
                propertyType = "DateTime";
            }
            else if (type.ToString() == "System.Nullable`1[System.Int16]")
            {
                propertyType = "Int16";
            }
            else if (type.ToString() == "System.Nullable`1[System.Int32]")
            {
                propertyType = "Int32";
            }
            else if (type.ToString() == "System.Nullable`1[System.Int64]")
            {
                propertyType = "Int64";
            }
            else if (type.ToString() == "System.Nullable`1[System.Decimal]")
            {
                propertyType = "Decimal";
            }
            else if (type.ToString() == "System.Nullable`1[System.Double]")
            {
                propertyType = "Double";
            }
            else if (type.ToString() == "System.Nullable`1[System.Boolean]")
            {
                propertyType = "Boolean";
            }
        }
        public static void SetCellValueOfPropertyType(ICell cell, string propertyType, string cellValue)
        {
            switch (propertyType)
            {
                case "String": //字符串类型
                    cell.SetCellValue(cellValue);
                    break;
                case "DateTime": //日期类型
                    DateTime dateV;
                    if (DateTime.TryParse(cellValue, out dateV))
                        cell.SetCellValue(dateV);
                    else if (!string.IsNullOrEmpty(cellValue))
                        cell.SetCellValue(cellValue);
                    else
                        cell.SetCellValue("");
                    //cell.CellStyle = dateStyle; //格式化显示
                    break;
                case "Boolean": //布尔型
                    bool boolV = false;
                    if (bool.TryParse(cellValue, out boolV))
                        cell.SetCellValue(boolV);
                    else
                        cell.SetCellValue(cellValue);
                    break;
                case "Int16": //整型
                    int intV16 = 0;
                    if (int.TryParse(cellValue, out intV16))
                        cell.SetCellValue(intV16);
                    else
                        cell.SetCellValue(cellValue);
                    break;
                case "Int32":
                    int intV32 = 0;
                    if (int.TryParse(cellValue, out intV32))
                        cell.SetCellValue(intV32);
                    else
                        cell.SetCellValue(cellValue);
                    break;
                case "Int64":
                    long intV64 = 0;
                    if (long.TryParse(cellValue, out intV64))
                        cell.SetCellValue(intV64);
                    else
                        cell.SetCellValue(cellValue);
                    break;
                case "Byte":
                    int intV = 0;
                    if (int.TryParse(cellValue, out intV))
                        cell.SetCellValue(intV);
                    else
                        cell.SetCellValue(cellValue);
                    break;
                case "Decimal": //浮点型
                case "Double":
                    double doubV = 0;
                    if (double.TryParse(cellValue, out doubV))
                        cell.SetCellValue(doubV);
                    else
                        cell.SetCellValue(cellValue);
                    break;
                case "DBNull": //空值处理
                    cell.SetCellValue("");
                    break;
                default:
                    if (DateTime.TryParse(cellValue, out dateV))
                        cell.SetCellValue(dateV);
                    else
                        cell.SetCellValue(cellValue);
                    break;
            }
        }
        public static PropertyInfo[] GetProperties<T>()
        {
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            return props;
        }
        #endregion
        #region Default Columns       
        /// <summary>
        /// 库存查询导出Excel列信息
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetReaBmsQtyDtlColumns()
        {
            Dictionary<string, string> columns = new Dictionary<string, string>();
            columns.Add("CompanyName", "所属供应商");
            columns.Add("StorageName", "库房");
            columns.Add("PlaceName", "货架");
            columns.Add("ReaGoodsNo", "产品编号");
            columns.Add("GoodsName", "试剂名称");
            columns.Add("LotNo", "货品批号");
            columns.Add("GoodsUnit", "包装单位");
            columns.Add("UnitMemo", "单位描述");
            columns.Add("Price", "均价");
            columns.Add("GoodsQty", "库存数量");
            columns.Add("SumTotal", "总计金额");
            columns.Add("Memo", "备注");
            return columns;
        }
        /// <summary>
        /// 库存效期导出Excel列信息
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetValidityWarningColumns()
        {
            Dictionary<string, string> columns = new Dictionary<string, string>();
            columns.Add("CompanyName", "所属供应商");
            columns.Add("StorageName", "库房");
            columns.Add("PlaceName", "货架");
            columns.Add("ReaGoodsNo", "产品编号");
            columns.Add("GoodsName", "试剂名称");
            columns.Add("LotNo", "货品批号");
            columns.Add("GoodsUnit", "包装单位");
            columns.Add("UnitMemo", "单位描述");
            columns.Add("ProdDate", "生产日期");
            columns.Add("InvalidDate", "有效期至");
            //columns.Add("Price", "均价");
            columns.Add("GoodsQty", "库存数量");
            columns.Add("SumTotal", "总计金额");
            columns.Add("Memo", "备注");
            return columns;
        }
        /// <summary>
        /// 库存预警导出Excel列信息
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetStockWarningColumns()
        {
            Dictionary<string, string> columns = new Dictionary<string, string>();
            columns.Add("CompanyName", "所属供应商");
            columns.Add("StorageName", "库房");
            columns.Add("PlaceName", "货架");
            columns.Add("ReaGoodsNo", "产品编号");
            columns.Add("GoodsName", "试剂名称");
            columns.Add("LotNo", "货品批号");
            columns.Add("GoodsUnit", "包装单位");
            columns.Add("UnitMemo", "单位描述");
            columns.Add("ProdDate", "生产日期");
            columns.Add("InvalidDate", "有效期至");
            columns.Add("StoreLower", "库存下限");
            columns.Add("StoreUpper", "库存上限");
            columns.Add("MonthlyUsage", "理论月用量");
            columns.Add("ComparisonValue", "预警比较值");
            columns.Add("GoodsQty", "库存数量");
            columns.Add("SumTotal", "总计金额");
            columns.Add("Memo", "备注");
            return columns;
        }

        #endregion
        #region ToDataTable
        public static DataTable ExportExcelToDataTable<T>(IList<T> tempList, Dictionary<string, string> columns)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                Type t = NPOIExcelToExporHelp.GetCoreType(prop.PropertyType);
                if (columns.ContainsKey(prop.Name))
                {
                    tb.Columns.Add(columns[prop.Name], t);
                }
            }
            List<object> values = new List<object>();
            foreach (T model in tempList)
            {
                values.Clear();
                for (int i = 0; i < props.Length; i++)
                {
                    if (columns.ContainsKey(props[i].Name))
                    {
                        values.Add(props[i].GetValue(model, null));
                    }
                }
                if (values.Count > 0)
                    tb.Rows.Add(values.ToArray());
            }
            //列显示次序处理
            int ordinal = 0;
            foreach (var column in columns)
            {
                tb.Columns[column.Value].SetOrdinal(ordinal);
                ordinal++;
            }
            //if (tb != null)
            //{
            //    //排序
            //    tb.DefaultView.Sort = "库房 asc,试剂名称 asc";
            //    tb = tb.DefaultView.ToTable();
            //}
            return tb;
        }

        #endregion
    }
}
