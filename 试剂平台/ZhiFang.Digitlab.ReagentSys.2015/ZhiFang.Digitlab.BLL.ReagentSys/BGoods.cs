using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.Digitlab.IDAO; 
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Common.Public;

namespace ZhiFang.Digitlab.BLL.ReagentSys
{
    /// <summary>
    ///
    /// </summary>
    public class BGoods : BaseBLL<Goods>, ZhiFang.Digitlab.IBLL.ReagentSys.IBGoods
    {
        ZhiFang.Digitlab.IBLL.ReagentSys.IBCenOrg IBCenOrg { get; set; }

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
                string xmlGoods = serverPath + "\\BaseTableXML\\Goods.xml";
                if (System.IO.File.Exists(xmlGoods))
                {
                    DataSet dataSet = new DataSet();
                    dataSet.Tables.Add(dt);
                    //BLaboratory laboratory = IBBLaboratory.Get(Int64.Parse(labID));
                    CenOrg labEntity = IBCenOrg.Get(Int64.Parse(labID));
                    CenOrg compEntity = IBCenOrg.Get(Int64.Parse(compID));
                    CenOrg prodEntity = null;
                    if (!string.IsNullOrWhiteSpace(prodID) && Int64.Parse(prodID) > 0)
                        prodEntity = IBCenOrg.Get(Int64.Parse(prodID));
                    IList<string> listPrimaryKey = new List<string>();
                    Dictionary<string, string> dicDefaultValue = new Dictionary<string, string>();
                    Dictionary<string, string> dicColumn = ExcelDataCommon.GetColumnNameByDataSet(dataSet, xmlGoods, listPrimaryKey, dicDefaultValue);
                    if (listPrimaryKey.Count > 0)
                    {
                        if (prodEntity != null)
                            baseResultDataValue = _AddGoodsDataTable(dt, labEntity, compEntity, prodEntity, dicColumn, listPrimaryKey, dicDefaultValue);
                        else
                            baseResultDataValue = _AddGoodsDataTable(dt, labEntity, compEntity, dicColumn, listPrimaryKey, dicDefaultValue);
                        baseResultDataValue.success = MyNPOIHelper.GetInputExcelFileState(excelFilePath, dt);
                        if (baseResultDataValue.success)
                        {
                            baseResultDataValue.ErrorInfo = baseResultDataValue.ResultDataValue;
                            baseResultDataValue.ResultDataValue = System.IO.Path.GetFileName(excelFilePath);
                        }
                    }
                    else
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "产品表导入对照表没有设置唯一键！";
                    }
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "产品表导入配置信息不存在！";
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "产品表数据信息为空！";
            }
            if (!baseResultDataValue.success)
                ZhiFang.Common.Log.Log.Info(baseResultDataValue.ErrorInfo);
            return baseResultDataValue;
        }//

        public BaseResultDataValue CheckGoodsExcelFormat(string excelFilePath, string serverPath)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string xmlGoods = serverPath + "\\BaseTableXML\\Goods.xml";
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
                baseResultDataValue.ErrorInfo = "产品表导入配置信息不存在！";
                ZhiFang.Common.Log.Log.Info("产品表导入配置信息不存在！");
            }
            return baseResultDataValue;
        }//

        public BaseResultDataValue _AddGoodsDataTable(DataTable dataTable, CenOrg labEntity, CenOrg compEntity, CenOrg prodEntity, Dictionary<string, string> dicColumn, IList<string> listPrimaryKey, Dictionary<string, string> dicDefaultValue)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string prodColumn = "-9999";
            Dictionary<string, CenOrg> dicProd = new Dictionary<string, CenOrg>();
            dicProd.Add(prodColumn, prodEntity);
            baseResultDataValue = _AddGoodsData(dataTable, labEntity, compEntity, prodColumn, dicProd, dicColumn, listPrimaryKey, dicDefaultValue);
            return baseResultDataValue;
        }

        public BaseResultDataValue _AddGoodsDataTable(DataTable dataTable, CenOrg labEntity, CenOrg compEntity, Dictionary<string, string> dicColumn, IList<string> listPrimaryKey, Dictionary<string, string> dicDefaultValue)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            string prodColumn = "";
            if (dicColumn.Values.Contains("ProdNo"))
                prodColumn = dicColumn.FirstOrDefault(q => q.Value == "ProdNo").Key;
            if (string.IsNullOrWhiteSpace(prodColumn))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "产品表配置文件没有配置产品机构编码信息！";
                ZhiFang.Common.Log.Log.Info(baseResultDataValue.ErrorInfo);
                return baseResultDataValue;
            }
            Dictionary<string, long> dicMain = new Dictionary<string, long>();
            Dictionary<string, CenOrg> dicProd = new Dictionary<string, CenOrg>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                dataRow["ExcelRowInputFlag"] = 0;
                dataRow["ExcelRowInputInfo"] = "导入成功";
                string prodNo = "";
                CenOrg cenorg = null;
                if (dataRow[prodColumn] != null)
                {
                    prodNo = dataRow[prodColumn].ToString();
                    if (dicProd.Keys.Contains(prodNo))
                        continue;
                    IList<CenOrg> listCenOrg = IBCenOrg.SearchListByHQL(" cenorg.OrgNo=\'" + prodNo + "\'");
                    if (listCenOrg != null && listCenOrg.Count > 0)
                    {
                        cenorg = listCenOrg[0];
                        dicProd.Add(prodNo, cenorg);
                    }
                    else
                    {
                        //baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "导入失败：根据机构编码【" + prodNo + "】找不到对应的机构信息！";
                        dataRow["ExcelRowInputFlag"] = -1;
                        dataRow["ExcelRowInputInfo"] = baseResultDataValue.ErrorInfo; 
                        ZhiFang.Common.Log.Log.Info(baseResultDataValue.ErrorInfo);
                        continue;
                        //return baseResultDataValue;
                    }
                }
                if (string.IsNullOrWhiteSpace(prodNo))
                {
                    //baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "导入失败：Excel中【" + prodColumn + "】列存在为空的信息，请补充完整！";
                    dataRow["ExcelRowInputFlag"] = -2;
                    dataRow["ExcelRowInputInfo"] = baseResultDataValue.ErrorInfo;
                    ZhiFang.Common.Log.Log.Info(baseResultDataValue.ErrorInfo);
                    continue;
                    //return baseResultDataValue;
                }
            }
            dicColumn.Remove(prodColumn);
            baseResultDataValue = _AddGoodsData(dataTable, labEntity, compEntity, prodColumn, dicProd, dicColumn, listPrimaryKey, dicDefaultValue);
            return baseResultDataValue;
        }

        public BaseResultDataValue _AddGoodsData(DataTable dataTable, CenOrg labEntity, CenOrg compEntity, string prodColumn, Dictionary<string, CenOrg> dicProd, Dictionary<string, string> dicColumn, IList<string> listPrimaryKey, Dictionary<string, string> dicDefaultValue)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            int isExistCount = 0;
            int isErrorCount = 0;
            int isSuccCount = 0;
            Dictionary<string, long> dicMain = new Dictionary<string, long>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                dataRow["ExcelRowInputFlag"] = 0;
                dataRow["ExcelRowInputInfo"] = "导入成功";
                CenOrg prodEntity = null;
                if (dicProd.Count == 1)
                    prodEntity = dicProd.Values.First();
                else
                {
                    string prodNo = "";
                    if (dataRow[prodColumn] != null)
                        prodNo = dataRow[prodColumn].ToString();
                    if (dicProd.Keys.Contains(prodNo))
                        prodEntity = dicProd[prodNo];
                    else
                    {
                        //baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "导入失败：根据机构编码【" + prodNo + "】找不到对应的机构信息！";
                        ZhiFang.Common.Log.Log.Info(baseResultDataValue.ErrorInfo);
                        isErrorCount++;
                        dataRow["ExcelRowInputFlag"] = -1;
                        dataRow["ExcelRowInputInfo"] = baseResultDataValue.ErrorInfo;
                        continue;
                        //return baseResultDataValue;
                    }
                }
                string keyValue = "";
                string keyHQL = "";
                foreach (string strKey in listPrimaryKey)
                {
                    if (!string.IsNullOrEmpty(dataRow[strKey].ToString()))
                    {
                        keyValue += "_" + dataRow[strKey].ToString().Trim();
                        if (string.IsNullOrEmpty(keyHQL))
                            keyHQL += " goods." + dicColumn[strKey] + "=\'" + dataRow[strKey].ToString().Trim() + "\'";
                        else
                            keyHQL += " and " + " goods." + dicColumn[strKey] + "=\'" + dataRow[strKey].ToString().Trim() + "\'";
                    }
                }
                if ((!string.IsNullOrEmpty(keyValue)) && (!string.IsNullOrEmpty(keyHQL)))
                {
                    if (dicMain.ContainsKey(keyValue))
                    {//导入的信息中存在主键列相同的记录
                        isSuccCount++;
                        continue;
                    }
                    keyHQL += " and " + " goods.CenOrg.Id=" + labEntity.Id.ToString() +
                        " and " + " goods.Comp.Id=" + compEntity.Id.ToString() +
                        " and " + " goods.Prod.Id=" + prodEntity.Id.ToString();
                    IList<Goods> listGoods = null;
                    dicMain.Add(keyValue, 0);
                    listGoods = this.SearchListByHQL(keyHQL);
                    if (listGoods != null && listGoods.Count > 0)
                    {
                        dicMain[keyValue] = listGoods[0].Id;
                        isExistCount++;
                        dataRow["ExcelRowInputFlag"] = 1;
                        dataRow["ExcelRowInputInfo"] = "未导入：该产品信息已经存在";
                        ZhiFang.Common.Log.Log.Info("未导入：产品信息已经存在！产品名称为：" + listGoods[0].CName + " 编码为：" + listGoods[0].GoodsNo);
                    }
                    else
                    {
                        Goods goods = ExcelDataCommon.AddExcelDataToDataBase<Goods>(dataRow, dicColumn, dicDefaultValue);
                        if (goods != null)
                        {
                            goods.CenOrg = labEntity;
                            goods.Comp = compEntity;
                            goods.Prod = prodEntity;
                            goods.DataAddTime = DateTime.Now;
                            goods.DataUpdateTime = DateTime.Now;
                            goods.PinYinZiTou = ZhiFang.Common.Public.StringPlus.Chinese2Spell.GetPinYinZiTou(goods.CName);
                            goods.Visible = 1;
                            this.Entity = goods;
                            if (this.Add())
                            {
                                dicMain[keyValue] = goods.Id;
                                isSuccCount++;
                            }
                            else
                            {
                                isErrorCount++;
                                dataRow["ExcelRowInputFlag"] = -2;
                                dataRow["ExcelRowInputInfo"] = "导入失败：产品信息保存失败";
                                ZhiFang.Common.Log.Log.Info("导入失败：产品信息保存失败！");
                            }
                        }
                    }
                }
            }
            baseResultDataValue.ResultDataValue = string.Format("共需导入货品信息{0}条，其中：导入成功{1}条，导入失败{3}条，未导入货品{2}条！", dataTable.Rows.Count, isSuccCount, isExistCount, isErrorCount);
            return baseResultDataValue;
        }

        public EntityList<Goods> EditBaronGetGoods(long compID, long cenOrgID, string jsonGoods)
        {
            EntityList<Goods> entityListGoods = new EntityList<Goods>();
            IList<Goods> returnListGoods = new List<Goods>();
            CenOrg comp = IBCenOrg.Get(compID);
            CenOrg cenOrg = IBCenOrg.Get(cenOrgID);
            if (comp != null && cenOrg != null)
            {
                IList<Goods> listGoods = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<IList<Goods>>(jsonGoods);
                if (listGoods != null && listGoods.Count > 0)
                {
                    IList<Goods> listLocalGoods = this.SearchListByHQL(" goods.CenOrg.Id = " + cenOrgID.ToString() + " and goods.Comp.Id = " + compID.ToString());
                    if (listLocalGoods != null && listLocalGoods.Count > 0)
                    {
                        foreach (Goods goods in listGoods)
                        {
                            IList<Goods> tempList = listLocalGoods.Where(p => p.ProdGoodsNo == goods.ProdGoodsNo).ToList();
                            if (tempList != null && tempList.Count > 0)
                            {
                                tempList[0].ProdGoodsNo = goods.ProdGoodsNo;
                                tempList[0].GoodsNo = goods.ProdGoodsNo;
                                tempList[0].CompGoodsNo = goods.ProdGoodsNo;
                                tempList[0].CName = goods.CName;
                                tempList[0].EName = goods.EName;
                                tempList[0].ShortCode = goods.ShortCode;
                                tempList[0].UnitMemo = goods.UnitMemo;
                                tempList[0].UnitName = goods.UnitName;
                                tempList[0].Price = goods.Price;
                                tempList[0].Equipment = goods.Equipment;
                                tempList[0].GoodsClassType = goods.GoodsClassType;
                                goods.DataUpdateTime = DateTime.Now;
                                this.Entity = tempList[0];
                                this.Edit();
                                returnListGoods.Add(this.Entity);
                                listLocalGoods.Remove(tempList[0]);
                            }
                            else
                            {
                                goods.Comp = comp;
                                goods.CenOrg = cenOrg;
                                goods.GoodsSource = 1;
                                goods.GoodsNo = goods.ProdGoodsNo;
                                goods.CompGoodsNo = goods.ProdGoodsNo;
                                goods.PinYinZiTou = ZhiFang.Common.Public.StringPlus.Chinese2Spell.GetPinYinZiTou(goods.CName);
                                goods.Visible = 1;
                                goods.DataUpdateTime = DateTime.Now;
                                goods.DataAddTime = DateTime.Now;
                                this.Entity = goods;
                                this.Add();
                                returnListGoods.Add(this.Entity);
                            }
                        }
                        foreach (Goods goods in listLocalGoods)
                        {
                            goods.Visible = 0;
                            this.Entity = goods;
                            this.Edit();
                        }
                    }
                    else
                    {
                        foreach (Goods goods in listGoods)
                        {
                            goods.Comp = comp;
                            goods.CenOrg = cenOrg;
                            goods.GoodsSource = 1;
                            goods.GoodsNo = goods.ProdGoodsNo;
                            goods.CompGoodsNo = goods.ProdGoodsNo;
                            goods.PinYinZiTou = ZhiFang.Common.Public.StringPlus.Chinese2Spell.GetPinYinZiTou(goods.CName);
                            goods.Visible = 1;
                            goods.DataUpdateTime = DateTime.Now;
                            goods.DataAddTime = DateTime.Now;
                            this.Entity = goods;
                            this.Add();
                        }
                        returnListGoods = listGoods;
                    }
                }
                if (returnListGoods != null && returnListGoods.Count > 0)
                {
                    entityListGoods.count = returnListGoods.Count;
                    entityListGoods.list = returnListGoods;
                }
            }
            else
                ZhiFang.Common.Log.Log.Info("");
            return entityListGoods;
        }

        public BaseResultDataValue CopyGoodsByID(string listID, long compId, long cenOrgId, int goodsNoType)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            CenOrg comp = IBCenOrg.Get(compId);
            CenOrg cenOrg = IBCenOrg.Get(cenOrgId);
            if (comp != null && cenOrg != null)
            {
                IList<string> IDList = listID.Split(',').ToList();
                //int AllCount = IDList.Count;
                //int SCount = 0;
                //int FCount = 0;
                foreach (string id in IDList)
                {
                    Goods goods = this.Get(long.Parse(id));
                    if (goods != null)
                    {
                        string prodId = goods.Prod != null ? goods.Prod.Id.ToString() : "";
                        bool isExist = JudgeGoodsIsExist(compId.ToString(), cenOrgId.ToString(), goods.GoodsNo);
                        if (!isExist)
                        {
                            Goods addGood = new Goods();
                            addGood.GoodsNo = goods.GoodsNo;
                            if (goodsNoType == 1)
                                addGood.CompGoodsNo = goods.GoodsNo;
                            else
                                addGood.CompGoodsNo = goods.CompGoodsNo;
                            addGood.ProdGoodsNo = goods.ProdGoodsNo;
                            addGood.CName = goods.CName;
                            addGood.EName = goods.EName;
                            addGood.ShortCode = goods.ShortCode;
                            addGood.GoodsClass = goods.GoodsClass;
                            addGood.GoodsClassType = goods.GoodsClassType;
                            addGood.UnitName = goods.UnitName;
                            addGood.UnitMemo = goods.UnitMemo;
                            addGood.StorageType = goods.StorageType;
                            addGood.GoodsDesc = goods.GoodsDesc;
                            addGood.ApproveDocNo = goods.ApproveDocNo;
                            addGood.Standard = goods.Standard;
                            addGood.RegistNo = goods.RegistNo;
                            addGood.RegistDate = goods.RegistDate;
                            addGood.RegistNoInvalidDate = goods.RegistNoInvalidDate;
                            addGood.Purpose = goods.Purpose;
                            addGood.Constitute = goods.Constitute;
                            addGood.License = goods.License;
                            addGood.GoodsPicture = goods.GoodsPicture;
                            addGood.DispOrder = goods.DispOrder;
                            addGood.Visible = goods.Visible;
                            addGood.Price = goods.Price;
                            addGood.BiddingNo = goods.BiddingNo;
                            addGood.ProdEara = goods.ProdEara;
                            addGood.ProdOrgName = goods.ProdOrgName;
                            addGood.ZX1 = goods.ZX1;
                            addGood.ZX2 = goods.ZX2;
                            addGood.ZX3 = goods.ZX3;
                            addGood.Equipment = goods.Equipment;
                            addGood.CenOrgConfirm = 0;
                            addGood.CompConfirm = 1;
                            addGood.DataUpdateTime = goods.DataUpdateTime;
                            addGood.IsPrintBarCode = goods.IsPrintBarCode;
                            addGood.IsRegister = goods.IsRegister;
                            addGood.BarCodeMgr = goods.BarCodeMgr;
                            addGood.SuitableType = goods.SuitableType;
                            addGood.IsPrintBarCode = goods.IsPrintBarCode;
                            addGood.PinYinZiTou = goods.PinYinZiTou;
                            addGood.Prod = goods.Prod;
                            addGood.Comp = comp;
                            addGood.CenOrg = cenOrg;
                            addGood.DataAddTime = DateTime.Now;
                            this.Entity = addGood;
                            this.Add();
                            //if (this.Add())
                            //    SCount++;
                            //else
                            //    FCount++;
                        }
                    }
                }
                //if (AllCount == SCount)
                //    baseResultDataValue.ErrorInfo = "共需新增" + AllCount.ToString() + "条数据，新增成功" + SCount.ToString() + "条数据！";
                //else if (FCount > 0)
                //{
                //    baseResultDataValue.ErrorInfo = "共需新增" + AllCount.ToString() + "条数据，新增成功" + SCount.ToString() + "条数据，" +
                //        "新增失败" + SCount.ToString() + "条数据！";
                //}
            }
            else
            {
                baseResultDataValue.success = false;
                if (comp == null)
                    baseResultDataValue.ErrorInfo = "BLL错误信息：供应商不能为空！ID：" + compId.ToString();
                else if (cenOrg == null)
                    baseResultDataValue.ErrorInfo = "BLL错误信息：下级机构不能为空！ID：" + cenOrg.ToString();
            }
            return baseResultDataValue;
        }

        //private bool _judgeGoodsIsExist(string compId, string cenOrgId, string prodId, string prodGoodsNo)
        //{
        //    string where = " goods.Comp.Id=" + compId + " and goods.CenOrg.Id=" + cenOrgId;
        //    if (!string.IsNullOrEmpty(prodId))
        //        where += " and goods.Prod.Id=" + prodId;
        //    if (!string.IsNullOrEmpty(prodGoodsNo))
        //        where += " and goods.ProdGoodsNo=\'" + prodGoodsNo + "\'";
        //    IList<Goods> listGoods = this.SearchListByHQL(where);
        //    return (listGoods != null && listGoods.Count > 0);
        //}

        public bool JudgeGoodsIsExist(string compId, string cenOrgId, string goodsNo)
        {
            string where = " goods.Comp.Id=" + compId + " and goods.CenOrg.Id=" + cenOrgId;
            if (!string.IsNullOrEmpty(goodsNo))
                where += " and goods.GoodsNo=\'" + goodsNo + "\'";
            IList<Goods> listGoods = this.SearchListByHQL(where);
            return (listGoods != null && listGoods.Count > 0);
        }


        public BaseResultDataValue JudgeGoodsIsExist(string compId, string labId, string goodsNo, ref Goods goods)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string where = " goods.Comp.Id=" + compId + " and goods.CenOrg.Id=" + labId;
            if (!string.IsNullOrEmpty(goodsNo))
                where += " and goods.GoodsNo=\'" + goodsNo + "\'";
            IList<Goods> listGoods = this.SearchListByHQL(where);
            if (listGoods != null && listGoods.Count > 0)
            {
                if (listGoods.Count == 1)
                {
                    goods = listGoods[0];
                }
                else if (listGoods.Count > 1)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "根据产品编码【" + goodsNo + "】找到多个对应的产品信息，请联系管理员解决！";
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "根据产品编码【" + goodsNo + "】找不到对应的产品信息，请联系管理员维护！";
            }
            return baseResultDataValue;

        }

        public DataSet GetGoodsInfoByID(string idList, string where, string sort, string xmlPath)
        {
            EntityList<Goods> entityList = null;
            if (string.IsNullOrEmpty(idList) && string.IsNullOrEmpty(where))
                return null;
            else
            {
                if (!string.IsNullOrEmpty(idList))
                    entityList = this.SearchListByHQL(" goods.Id in (" + idList + ")", sort, 0, 0);
                else if (!string.IsNullOrEmpty(where))
                    entityList = this.SearchListByHQL(where, sort, 0, 0);
                if (entityList != null && entityList.count > 0)
                    return CommonRS.GetListObjectToDataSet<Goods>(entityList.list, xmlPath);
                else
                    return null;
            }
        }


        public EntityList<Goods> SearchGoodsByCompID(string labCenOrgID, string compCenOrgID, string where, string sort, int page, int limit)
        {
            EntityList<Goods> entityList = new EntityList<Goods>();

            if ((where != null) && (where.Length > 0))
                where = " and (" + where + ") ";
            string inSQL = "select GoodsNo from Goods goods where goods.CenOrg.Id=" + labCenOrgID + " and goods.Comp.Id=" + compCenOrgID;

            if ((sort != null) && (sort.Length > 0))
            {
                entityList = this.SearchListByHQL("goods.CenOrg.Id=" + compCenOrgID + " and goods.Visible=1 " + where +
                     " and goods.GoodsNo not in (" + inSQL + ")", sort, page, limit);
            }
            else
            {
                entityList = this.SearchListByHQL("goods.CenOrg.Id=" + compCenOrgID + " and goods.Visible=1 " + where +
                     " and goods.GoodsNo not in (" + inSQL + ")", page, limit);
            }
            return entityList;
        }

        //public EntityList<Goods> SearchGoodsByCompID(string labCenOrgID, string compCenOrgID, string where, string sort, int page, int limit)
        //{
        //    EntityList<Goods> entityList = new EntityList<Goods>();
        //    IList<Goods> listChild = null;
        //    if ((where != null) && (where.Length > 0))
        //        where = " and (" + where + ") ";
        //    listChild = this.SearchListByHQL("goods.CenOrg.Id="+labCenOrgID+" and goods.Comp.Id="+compCenOrgID);
        //    if (listChild != null && listChild.Count > 0)
        //    {
        //        StringBuilder sb = new StringBuilder();
        //        IList<string> strList = new List<string>();
        //        foreach (var goods in listChild)
        //        {
        //            sb.Append(",\'" + goods.GoodsNo+"\'");
        //            strList.Add(goods.GoodsNo);
        //        }
        //        if (sb.Length > 0)
        //        {
        //            string listNo = sb.ToString().Remove(0, 1);
        //            if ((sort != null) && (sort.Length > 0))
        //            {
        //                entityList = this.SearchListByHQL("goods.CenOrg.Id=" + compCenOrgID + " and goods.Visible=1 " + where +
        //                     " and goods.GoodsNo not in (" + listNo + ")", sort, page, limit);
        //            }
        //            else
        //            {
        //                entityList = this.SearchListByHQL("goods.CenOrg.Id=" + compCenOrgID + " and goods.Visible=1 " + where +
        //                     " and goods.GoodsNo not in (" + listNo + ")", page, limit);
        //            }
        //        }
        //    }
        //    else
        //        entityList = this.SearchListByHQL("goods.CenOrg.Id=" + compCenOrgID + " and goods.Visible=1 ", page, limit);

        //    return entityList;
        //}

        //public EntityList<Goods> SearchGoodsByCompID(string labCenOrgID, string compCenOrgID)
        //{
        //    EntityList<Goods> listGoods = new EntityList<Goods>();
        //    IList<Goods> listAll = this.SearchListByHQL("goods.CenOrg.Id=" + compCenOrgID + " and goods.Visible=1");
        //    IList<Goods> listChild = this.SearchListByHQL("goods.CenOrg.Id=" + labCenOrgID + " and goods.Comp.Id=" + compCenOrgID);

        //    if (listAll != null && listAll.Count > 0)
        //    {
        //        if (listChild != null && listChild.Count > 0)
        //        {
        //            IList<string> strList = new List<string>();
        //            foreach (var goods in listChild)
        //                strList.Add(goods.GoodsNo);
        //            listGoods.list = listAll.Where(p => !strList.Contains(p.GoodsNo)).ToList();
        //        }
        //        else
        //            listGoods.list = listAll;
        //    }
        //    listGoods.count = listGoods.list != null ? listGoods.list.Count : 0;
        //    return listGoods;
        //}

        public BaseResultData EditGoodsDownloadFlagByLabID(string labID, string startDate, string endDate)
        {
            BaseResultData brd = new BaseResultData();
            string strHQL = " goods.CenOrg.Id=" + labID + " and goods.DataUpdateTime>=\'" + startDate + "\'" + " and goods.DataUpdateTime<=\'" + endDate + "\'";
            IList<Goods> listGoods = this.SearchListByHQL(strHQL);
            if (listGoods != null && listGoods.Count > 0)
            {
                int succCount = 0;
                int failCount = 0;
                foreach (Goods goods in listGoods)
                {
                    //if (goods.DownloadFlag != 1)
                    goods.DownloadFlag = 1;
                    goods.DataUpdateTime = DateTime.Now;
                    this.Entity = goods;
                    if (this.Edit())
                        succCount++;
                    else
                        failCount++;
                }
                brd.message = string.Format("共需更新{0}条货品下载标志，其中：更新成功{1}条，更新失败{2}条。", listGoods.Count, succCount, failCount);
                brd.success = (failCount == 0);
            }
            else
                brd.message = "查询不到任何要更新的货品信息！";
            return brd;
        }
    }
}