using ZhiFang.BLL.Base;
using System;
using System.Collections.Generic;
using ZhiFang.WeiXin.IDAO;
using ZhiFang.WeiXin.Entity;
using System.Data;
using System.Reflection;
using ZhiFang.WeiXin.Common;
using System.IO;
using ZhiFang.WeiXin.Entity.Statistics;
using ZhiFang.Entity.Base;
using ZhiFang.WeiXin.IBLL;
using System.Linq;

namespace ZhiFang.WeiXin.BLL
{
    /// <summary>
    ///
    /// </summary>
    public class BFinanceIncome : BaseBLL<FinanceIncome>, ZhiFang.WeiXin.IBLL.IBFinanceIncome
    {
        IBBParameter IBBParameter { get; set; }

        #region 统计数据及Excel/PDF导出
        /// <summary>
        /// 查询财务收入报表数据
        /// </summary>
        /// <param name="searchEntity"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public EntityList<FinanceIncome> SearchFinanceIncomeList(UserConsumerFormSearch searchEntity, int page, int count)
        {
            EntityList<FinanceIncome> tempEntityList = new EntityList<FinanceIncome>();
            tempEntityList.list = new List<FinanceIncome>();
            IList<FinanceIncome> tempList = ((IDFinanceIncomeDao)base.DBDao).SearchFinanceIncomeList(searchEntity, "", page, count);
            tempEntityList.count = tempList.Count;
            if (tempList.Count > 0)
            {
                tempList = tempList.OrderBy(s => s.BillingDate).ThenBy(s => s.UserName).ToList();
                //分页处理
                if (count > 0 && count < tempList.Count)
                {
                    int startIndex = count * (page - 1);
                    int endIndex = count;
                    var list = tempList.Skip(startIndex).Take(endIndex);
                    if (list != null)
                    {
                        list = list.OrderBy(s => s.BillingDate).ThenBy(s => s.UserName);
                        tempList = list.ToList();
                    }
                }
            }

            foreach (var tempEntity in tempList)
            {
                if (tempEntity.Price.HasValue && tempEntity.AdvicePrice.HasValue&& tempEntity.AdvicePrice.Value>0)
                {
                    double rate = (tempEntity.AdvicePrice.Value / tempEntity.Price.Value) ;
                    tempEntity.AdvicePriceRate = Math.Round(rate, 2);
                }
                tempEntityList.list.Add(tempEntity);
            }
            return tempEntityList;
        }
        /// <summary>
        /// 获取财务收入报表Excel导出文件
        /// </summary>
        /// <param name="searchEntity"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public FileStream GetExportExcelFinanceIncome(UserConsumerFormSearch searchEntity, ref string fileName)
        {
            FileStream fileStream = null;
            EntityList<FinanceIncome> atemCount = new EntityList<FinanceIncome>();
            atemCount = this.SearchFinanceIncomeList(searchEntity, 0, 0);
            fileName = "";
            string excelFilePathPath = "";
            fileStream = CreateExcelFinanceIncome(ref excelFilePathPath, ref fileName, atemCount.list);
            return fileStream;
        }
        /// <summary>
        /// 获取财务收入报表Excel转PDF的文件
        /// </summary>
        /// <param name="searchEntity"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public BaseResultDataValue GetFinanceIncomeExcelToPdfFile(UserConsumerFormSearch searchEntity, ref string fileName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            FileStream fileStream = null;
            EntityList<FinanceIncome> tempList = new EntityList<FinanceIncome>();
            tempList = this.SearchFinanceIncomeList(searchEntity, 0, 0);
            fileName = "财务收入报表信息.xlsx";
            string excelFilePath = "";
            fileStream = CreateExcelFinanceIncome(ref excelFilePath, ref fileName, tempList.list);

            fileName = "财务收入报表.pdf";

            if (baseResultDataValue.success)
            {
                try
                {
                    string parentPath = (string)IBBParameter.GetCache(BParameterParaNoClass.ExcelExportSavePath.Key.ToString());
                    if (String.IsNullOrEmpty(parentPath))
                    {
                        parentPath = "ExcelExport\\";
                    }
                    parentPath = parentPath + "FinanceIncome\\TempPDFFile\\" + DateTime.Now.ToString("yyMMdd") + "\\";
                    string pdfFile = parentPath + GUIDHelp.GetGUIDLong() + ".pdf";
                    if (!Directory.Exists(parentPath))
                    {
                        Directory.CreateDirectory(parentPath);
                    }
                    baseResultDataValue.success = ExcelHelp.ExcelToPDF(excelFilePath, pdfFile);
                    if (baseResultDataValue.success)
                        baseResultDataValue.ResultDataValue = pdfFile;
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ResultDataValue = "";
                    baseResultDataValue.ErrorInfo = ex.Message;
                    ZhiFang.Common.Log.Log.Error("GetFinanceIncomeExcelToPdfFile：" + ex.Message);
                    throw new Exception(ex.Message);
                }
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 生成财务收入报表的Excel文件
        /// </summary>
        /// <param name="excelFilePath"></param>
        /// <param name="fileName"></param>
        /// <param name="tempList"></param>
        /// 
        /// <returns></returns>
        private FileStream CreateExcelFinanceIncome(ref string excelFilePath, ref string fileName, IList<FinanceIncome> tempList)
        {
            FileStream fileStream = null;
            if (tempList != null && tempList.Count > 0)
            {
                DataTable dtSource = null;
                dtSource = this.ExcelFinanceIncomeToDataTable<FinanceIncome>(tempList);
                string strHeaderText = "财务收入报表信息";
                fileName = "财务收入报表信息.xlsx";
                excelFilePath = "";
                string basePath = "";
                //一级保存路径
                basePath = (string)IBBParameter.GetCache(BParameterParaNoClass.ExcelExportSavePath.Key.ToString());
                if (String.IsNullOrEmpty(basePath))
                {
                    basePath = "ExcelExport\\";
                }
                basePath = basePath + "FinanceIncome\\TempExcelFile\\" + DateTime.Now.ToString("yyMMdd") + "\\";
                excelFilePath = basePath + +GUIDHelp.GetGUIDLong() + ".xlsx";

                try
                {
                    if (!Directory.Exists(basePath))
                        Directory.CreateDirectory(basePath);
                    //单元格字体颜色的处理
                    Dictionary<string, short> cellFontStyleList = new Dictionary<string, short>();
                    //cellFontStyleList.Add("", NPOI.HSSF.Util.HSSFColor.Red.Index);
                    fileStream = ExportDTtoExcelHelp.ExportDTtoExcellHelp(dtSource, strHeaderText, excelFilePath, cellFontStyleList);
                    if (fileStream != null)
                    {
                        fileStream = new FileStream(excelFilePath, FileMode.Open, FileAccess.Read);
                    }
                }
                catch (Exception ee)
                {
                    ZhiFang.Common.Log.Log.Error("Excel导出财务收入报表信息失败:" + ee.Message);
                    throw ee;
                }
            }
            return fileStream;
        }
        private DataTable ExcelFinanceIncomeToDataTable<T>(IList<T> list)
        {
            var tb = new DataTable(typeof(T).Name);
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            List<string> removeList = new List<string>();
            foreach (PropertyInfo prop in props)
            {
                Type t = ExportDTtoExcelHelp.GetCoreType(prop.PropertyType);
                string columnName = prop.Name;
                #region DataTable的列转换为导出的中文显示名称
                switch (prop.Name)
                {
                    case "UOFCode":
                        columnName = "订单编号";
                        break;
                    case "UserName":
                        columnName = "客户";
                        break;
                    case "BillingDate":
                        columnName = "开单日期";
                        break;
                    case "SamplingDate":
                        columnName = "采样日期";
                        break;
                    case "SexName":
                        columnName = "性别";
                        break;
                    case "MarketPrice":
                        columnName = "报价";
                        break;
                    case "GreatMasterPrice":
                        columnName = "大家价格";//大家价格
                        break;
                    case "Price":
                        columnName = "收入";
                        break;
                    case "AdvicePrice":
                        columnName = "咨询费";
                        break;
                    case "AdvicePriceRate":
                        columnName = "咨询费率";
                        break;
                    case "DoctorName":
                        columnName = "开单医生";
                        break;
                    case "MRefundFormCode":
                        columnName = "退费单编号";
                        break;
                    case "RefundPrice":
                        columnName = "退费金额";
                        break;
                    default:
                        removeList.Add(columnName);
                        break;
                }
                #endregion
                if (!String.IsNullOrEmpty(columnName))
                    tb.Columns.Add(columnName, t);
            }
            foreach (T item in list)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }
                tb.Rows.Add(values);
            }
            foreach (var columnName in removeList)
            {
                if (tb.Columns.Contains(columnName))
                {
                    tb.Columns.Remove(columnName);
                }
            }
            if (tb != null)
            {
                //开单医生\所属医院\区域\套餐名称\数量\报价\收入\咨询费率\咨询费（微信）
                tb.Columns["订单编号"].SetOrdinal(0);
                tb.Columns["客户"].SetOrdinal(1);
                tb.Columns["性别"].SetOrdinal(2);
                tb.Columns["报价"].SetOrdinal(3);
                tb.Columns["收入"].SetOrdinal(4);

                //tb.Columns["市场价格"].SetOrdinal(5);
                tb.Columns["开单日期"].SetOrdinal(5);
                tb.Columns["采样日期"].SetOrdinal(6);

                tb.Columns["咨询费率"].SetOrdinal(7);
                tb.Columns["咨询费"].SetOrdinal(8);
                tb.Columns["大家价格"].SetOrdinal(9);
                tb.Columns["开单医生"].SetOrdinal(10);
                //排序
                tb.DefaultView.Sort = "开单医生 asc";
                tb = tb.DefaultView.ToTable();
            }
            return tb;
        }

        #endregion

    }
}