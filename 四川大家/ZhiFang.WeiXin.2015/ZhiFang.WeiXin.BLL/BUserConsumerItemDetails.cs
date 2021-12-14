using ZhiFang.BLL.Base;
using System;
using System.Collections.Generic;
using ZhiFang.WeiXin.Entity;
using ZhiFang.WeiXin.IBLL;
using ZhiFang.WeiXin.Entity.Statistics;
using System.IO;
using ZhiFang.Entity.Base;
using System.Data;
using ZhiFang.WeiXin.Common;
using System.Reflection;
using ZhiFang.WeiXin.IDAO;
using System.Linq;

namespace ZhiFang.WeiXin.BLL
{
    /// <summary>
    ///
    /// </summary>
    public class BUserConsumerItemDetails : BaseBLL<UserConsumerItemDetails>, ZhiFang.WeiXin.IBLL.IBUserConsumerItemDetails
    {
        IBBParameter IBBParameter { get; set; }

        #region 统计数据及Excel/PDF导出
        /// <summary>
        /// 查询项目统计报表数据
        /// </summary>
        /// <param name="searchEntity"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public EntityList<UserConsumerItemDetails> SearchUserConsumerItemDetails(UserConsumerFormSearch searchEntity, int page, int count)
        {
            EntityList<UserConsumerItemDetails> tempEntityList = new EntityList<UserConsumerItemDetails>();
            tempEntityList.list = new List<UserConsumerItemDetails>();
            IList<UserConsumerItemDetails> tempList2 = new List<UserConsumerItemDetails>();
            IList<UserConsumerItemDetails> tempList = ((IDUserConsumerItemDetailsDao)base.DBDao).SearchUserConsumerItemDetailsList(searchEntity, "", page, count);
            var groudByList = tempList.GroupBy(p => p.GroupingKey);
            foreach (var groupList in groudByList)
            {
                UserConsumerItemDetails detail = new UserConsumerItemDetails();
                detail = groupList.ElementAt(0);
                detail.GroupingKey = groupList.Key;
                //数量(开单医生对某套餐项目的开单数量)
                detail.OrderFormCount = groupList.Count();
                //医生收入(折扣价格 * 数量)
                double? price = 0;
                for (int i = 0; i < groupList.Count(); i++)
                {
                    price = price + groupList.ElementAt(i).Price;
                }
                detail.Price = price;
                tempList2.Add(detail);
            }
            if (tempList2.Count > 0)
            {
                tempList2 = tempList2.OrderByDescending(s => s.DataAddTime).ThenBy(s => s.HospitalCName).ToList();
                //分页处理
                if (count > 0 && count < tempList.Count)
                {
                    int startIndex = count * (page - 1);
                    int endIndex = count;
                    var list = tempList2.Skip(startIndex).Take(endIndex);
                    if (list != null)
                    {
                        list = list.OrderByDescending(s => s.DataAddTime).ThenBy(s => s.HospitalCName);
                        tempList2 = list.ToList();
                    }
                }
            }
            tempEntityList.list = tempList2;
            tempEntityList.count = tempList2.Count;
            return tempEntityList;
        }
        /// <summary>
        /// 获取项目统计报表Excel导出文件
        /// </summary>
        /// <param name="searchEntity"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public FileStream GetExportExcelUserConsumerItemDetails(UserConsumerFormSearch searchEntity, ref string fileName)
        {
            FileStream fileStream = null;
            EntityList<UserConsumerItemDetails> atemCount = new EntityList<UserConsumerItemDetails>();
            atemCount = this.SearchUserConsumerItemDetails(searchEntity, 0, 0);
            fileName = "";
            string excelFilePathPath = "";
            fileStream = CreateExcelUserConsumerItemDetails(ref excelFilePathPath, ref fileName, atemCount.list);
            return fileStream;
        }
        /// <summary>
        /// 获取项目统计报表Excel转PDF的文件
        /// </summary>
        /// <param name="searchEntity"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public BaseResultDataValue GetUserConsumerItemExcelToPdfFile(UserConsumerFormSearch searchEntity, ref string fileName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            FileStream fileStream = null;
            EntityList<UserConsumerItemDetails> tempList = new EntityList<UserConsumerItemDetails>();
            tempList = this.SearchUserConsumerItemDetails(searchEntity, 0, 0);
            fileName = "项目统计报表.xlsx";
            string excelFilePath = "";
            fileStream = CreateExcelUserConsumerItemDetails(ref excelFilePath, ref fileName, tempList.list);
            fileName = "项目统计报表.pdf";

            if (baseResultDataValue.success)
            {
                try
                {
                    string parentPath = (string)IBBParameter.GetCache(BParameterParaNoClass.ExcelExportSavePath.Key.ToString());
                    if (String.IsNullOrEmpty(parentPath))
                    {
                        parentPath = "ExcelExport\\";
                    }
                    parentPath = parentPath + "UserConsumerItemDetails\\TempPDFFile\\" + DateTime.Now.ToString("yyMMdd") + "\\";
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
                    ZhiFang.Common.Log.Log.Error("GetUserConsumerItemExcelToPdfFile：" + ex.Message);
                    throw new Exception(ex.Message);
                }
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 生成项目统计报表的Excel文件
        /// </summary>
        /// <param name="excelFilePath"></param>
        /// <param name="fileName"></param>
        /// <param name="tempList"></param>
        /// <returns></returns>
        private FileStream CreateExcelUserConsumerItemDetails(ref string excelFilePath, ref string fileName, IList<UserConsumerItemDetails> tempList)
        {
            FileStream fileStream = null;
            if (tempList != null && tempList.Count > 0)
            {
                DataTable dtSource = null;
                dtSource = this.ExcelUserConsumerItemDetailsToDataTable<UserConsumerItemDetails>(tempList);
                string strHeaderText = "项目统计报表";
                fileName = "项目统计报表.xlsx";
                excelFilePath = "";
                string basePath = "";
                //一级保存路径
                basePath = (string)IBBParameter.GetCache(BParameterParaNoClass.ExcelExportSavePath.Key.ToString());
                if (String.IsNullOrEmpty(basePath))
                {
                    basePath = "ExcelExport\\";
                }
                basePath = basePath + "UserConsumerItemDetails\\TempExcelFile\\" + DateTime.Now.ToString("yyMMdd") + "\\";
                excelFilePath = basePath + GUIDHelp.GetGUIDLong() + ".xlsx";

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
                    ZhiFang.Common.Log.Log.Error("Excel导出项目统计报表失败:" + ee.Message);
                    throw ee;
                }
            }
            return fileStream;
        }
        private DataTable ExcelUserConsumerItemDetailsToDataTable<T>(IList<T> list)
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
                    case "DoctorName":
                        columnName = "开单医生";
                        break;
                    case "HospitalCName":
                        columnName = "所属医院";
                        break;
                    case "AreaCName":
                        columnName = "区域";
                        break;
                    case "ItemCName":
                        columnName = "套餐名称";
                        break;
                    case "OrderFormCount":
                        columnName = "数量";
                        break;
                    case "MarketPrice":
                        columnName = "市场价格";
                        break;
                    case "GreatMasterPrice":
                        columnName = "大家价格";
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
                tb.Columns["开单医生"].SetOrdinal(0);
                tb.Columns["所属医院"].SetOrdinal(1);

                tb.Columns["区域"].SetOrdinal(2);
                tb.Columns["套餐名称"].SetOrdinal(3);
                tb.Columns["数量"].SetOrdinal(4);
                tb.Columns["市场价格"].SetOrdinal(5);

                tb.Columns["大家价格"].SetOrdinal(6);
                tb.Columns["收入"].SetOrdinal(7);
                //tb.Columns["咨询费率"].SetOrdinal(8);
                //tb.Columns["咨询费"].SetOrdinal(9);
                //排序
                tb.DefaultView.Sort = "所属医院 asc,开单医生 asc";
                tb = tb.DefaultView.ToTable();
            }
            return tb;
        }

        #endregion

    }
}