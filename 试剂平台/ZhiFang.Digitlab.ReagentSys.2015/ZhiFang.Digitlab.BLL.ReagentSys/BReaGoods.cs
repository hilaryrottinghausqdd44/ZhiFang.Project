using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;

namespace ZhiFang.Digitlab.BLL.ReagentSys
{
    /// <summary>
    ///
    /// </summary>
    public class BReaGoods : BaseBLL<ReaGoods>, ZhiFang.Digitlab.IBLL.ReagentSys.IBReaGoods
    {

        IBLL.ReagentSys.IBReaCenOrg IBReaCenOrg { get; set; }

        public BaseResultDataValue CheckGoodsExcelFormat(string excelFilePath, string serverPath)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string xmlGoods = serverPath + "\\BaseTableXML\\ReaGoods.xml";
            if (System.IO.File.Exists(xmlGoods))
            {
                IList<string> dicColumn = ExcelDataCommon.GetRequiredFieldByXml(xmlGoods);
                Dictionary<string, Type> dicType = ExcelDataCommon.GetFieldTypeByXml<Goods>(xmlGoods);
                baseResultDataValue.success = MyNPOIHelper.CheckExcelFile(excelFilePath, dicColumn, dicType);
                if (!baseResultDataValue.success)
                {
                    baseResultDataValue.ErrorInfo = "Error001";
                    baseResultDataValue.ResultDataValue = System.IO.Path.GetFileName(excelFilePath);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "货品表导入配置信息不存在！";
                ZhiFang.Common.Log.Log.Info("货品表导入配置信息不存在！");
            }
            return baseResultDataValue;
        }//

        public BaseResultDataValue AddGoodsDataFormExcel(string labID, string compID, string prodID, string excelFilePath, string serverPath)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            DataTable dt = MyNPOIHelper.ImportExceltoDataTable(excelFilePath);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataColumn dataColumn in dt.Columns)
                {
                    dataColumn.ColumnName = dataColumn.ColumnName.Trim();
                }
                string xmlGoods = serverPath + "\\BaseTableXML\\ReaGoods.xml";
                if (System.IO.File.Exists(xmlGoods))
                {
                    DataSet dataSet = new DataSet();
                    dataSet.Tables.Add(dt);

                    ReaCenOrg labEntity = IBReaCenOrg.Get(Int64.Parse(labID));
                    ReaCenOrg compEntity = IBReaCenOrg.Get(Int64.Parse(compID));
                    ReaCenOrg prodEntity = null;
                    if (!string.IsNullOrWhiteSpace(prodID) && Int64.Parse(prodID) > 0)
                        prodEntity = IBReaCenOrg.Get(Int64.Parse(prodID));
                    IList<string> listPrimaryKey = new List<string>();
                    Dictionary<string, string> dicDefaultValue = new Dictionary<string, string>();
                    Dictionary<string, string> dicColumn = ExcelDataCommon.GetColumnNameByDataSet(dataSet, xmlGoods, listPrimaryKey, dicDefaultValue);
                    if (prodEntity != null)
                        baseResultDataValue = _AddGoodsDataTable(dt, labEntity, prodEntity, dicColumn, listPrimaryKey, dicDefaultValue);
                    else
                        baseResultDataValue = _AddGoodsDataTable(dt, labEntity, dicColumn, listPrimaryKey, dicDefaultValue);
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "货品表导入配置信息不存在！";
                    ZhiFang.Common.Log.Log.Info("货品表导入配置信息不存在！");
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "货品表数据信息为空！";
                ZhiFang.Common.Log.Log.Info("货品表数据信息为空！");
            }
            return baseResultDataValue;
        }//

        public BaseResultDataValue _AddGoodsDataTable(DataTable dataTable, ReaCenOrg labEntity, ReaCenOrg prodEntity, Dictionary<string, string> dicColumn, IList<string> listPrimaryKey, Dictionary<string, string> dicDefaultValue)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string prodColumn = "-9999";
            Dictionary<string, ReaCenOrg> dicProd = new Dictionary<string, ReaCenOrg>();
            dicProd.Add(prodColumn, prodEntity);
            baseResultDataValue = _AddGoodsData(dataTable, labEntity, prodColumn, dicProd, dicColumn, listPrimaryKey, dicDefaultValue);
            return baseResultDataValue;
        }

        public BaseResultDataValue _AddGoodsDataTable(DataTable dataTable, ReaCenOrg labEntity, Dictionary<string, string> dicColumn, IList<string> listPrimaryKey, Dictionary<string, string> dicDefaultValue)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            string prodColumn = "";
            if (dicColumn.Values.Contains("ProdNo"))
                prodColumn = dicColumn.FirstOrDefault(q => q.Value == "ProdNo").Key;
            if (string.IsNullOrWhiteSpace(prodColumn))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "货品表配置文件没有配置货品机构编码信息！";
                ZhiFang.Common.Log.Log.Info(baseResultDataValue.ErrorInfo);
                return baseResultDataValue;
            }
            Dictionary<string, long> dicMain = new Dictionary<string, long>();
            Dictionary<string, ReaCenOrg> dicProd = new Dictionary<string, ReaCenOrg>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                string prodNo = "";
                ReaCenOrg reaCenOrg = null;
                if (dataRow[prodColumn] != null)
                {
                    prodNo = dataRow[prodColumn].ToString();
                    if (dicProd.Keys.Contains(prodNo))
                        continue;
                    IList<ReaCenOrg> listReaCenOrg = IBReaCenOrg.SearchListByHQL(" reacenorg.OrgNo=" + prodNo);
                    if (listReaCenOrg != null && listReaCenOrg.Count > 0)
                    {
                        reaCenOrg = listReaCenOrg[0];
                        dicProd.Add(prodNo, reaCenOrg);
                    }
                    else
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "根据机构编码【" + prodNo + "】找不到对应的机构信息！";
                        ZhiFang.Common.Log.Log.Info(baseResultDataValue.ErrorInfo);
                        return baseResultDataValue;
                    }
                }
                if (string.IsNullOrWhiteSpace(prodNo))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "Excel中【" + prodColumn + "】列存在为空的信息，请补充完整！";
                    ZhiFang.Common.Log.Log.Info(baseResultDataValue.ErrorInfo);
                    return baseResultDataValue;
                }
            }
            dicColumn.Remove(prodColumn);
            baseResultDataValue = _AddGoodsData(dataTable, labEntity, prodColumn, dicProd, dicColumn, listPrimaryKey, dicDefaultValue);
            return baseResultDataValue;
        }

        public BaseResultDataValue _AddGoodsData(DataTable dataTable, ReaCenOrg reaCenOrg, string prodColumn, Dictionary<string, ReaCenOrg> dicProd, Dictionary<string, string> dicColumn, IList<string> listPrimaryKey, Dictionary<string, string> dicDefaultValue)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            Dictionary<string, long> dicMain = new Dictionary<string, long>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                ReaCenOrg prodEntity = null;
                if (dicProd.Count == 1)
                    prodEntity = dicProd.Values.First();
                else
                    prodEntity = dicProd[dataRow[prodColumn].ToString()];
                if (listPrimaryKey.Count > 0)
                {
                    string keyValue = "";
                    string keyHQL = "";
                    foreach (string strKey in listPrimaryKey)
                    {
                        if (!string.IsNullOrEmpty(dataRow[strKey].ToString()))
                        {
                            keyValue += "_" + dataRow[strKey].ToString().Trim();
                            keyHQL += " and " + " reagoods." + dicColumn[strKey] + "=\'" + dataRow[strKey].ToString().Trim() + "\'";
                        }
                    }
                    if ((!string.IsNullOrEmpty(keyValue)) && (!string.IsNullOrEmpty(keyHQL)))
                    {
                        if (!dicMain.ContainsKey(keyValue))
                        {
                            keyHQL = keyHQL.Remove(0, 4);
                            keyHQL += " and " + " reagoods.Prod.Id=" + prodEntity.Id.ToString();
                            IList<ReaGoods> listGoods = null;
                            dicMain.Add(keyValue, 0);
                            listGoods = this.SearchListByHQL(keyHQL);
                            if (listGoods != null && listGoods.Count > 0)
                            {
                                dicMain[keyValue] = listGoods[0].Id;
                                ZhiFang.Common.Log.Log.Info("货品信息已经存在！货品名称为：" + listGoods[0].CName + " 编码为：" + listGoods[0].GoodsNo);
                            }
                            else
                            {
                                ReaGoods Goods = ExcelDataCommon.AddExcelDataToDataBase<ReaGoods>(dataRow, dicColumn, dicDefaultValue);
                                if (Goods != null)
                                {
                                    Goods.ReaCenOrg = reaCenOrg;
                                    Goods.Prod = prodEntity;
                                    Goods.DataAddTime = DateTime.Now;
                                    Goods.DataUpdateTime = DateTime.Now;
                                    Goods.Visible = 1;
                                    this.Entity = Goods;
                                    if (this.Add())
                                        dicMain[keyValue] = Goods.Id;
                                    else
                                        ZhiFang.Common.Log.Log.Info("货品表Goods保存失败！");
                                }
                            }
                        }
                    }
                }
                else
                    ZhiFang.Common.Log.Log.Info("货品表导入对照表没有设置唯一键！");
            }
            return baseResultDataValue;
        }


        /// <summary>
        ///产品序号=最大值+1
        /// </summary>
        public int GetMaxGoodsSort()
        {
            IList<int> list = this.DBDao.Find<int>("select max(reagoods.GoodsSort) as GoodsSort  from ReaGoods reagoods where 1=1 ");
            if (list != null && list.Count > 0)
            {
                int maxOrgNo = list[0];
                maxOrgNo = maxOrgNo + 1;
                return maxOrgNo;
            }
            else
                return 0;
        }
    }
}