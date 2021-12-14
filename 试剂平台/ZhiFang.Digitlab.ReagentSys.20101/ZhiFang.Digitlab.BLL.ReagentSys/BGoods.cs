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
                    CenOrg prodEntity = IBCenOrg.Get(Int64.Parse(prodID));
                    IList<string> listPrimaryKey = new List<string>();
                    Dictionary<string, string> dicColumn = ExcelDataCommon.GetColumnNameByDataSet(dataSet, xmlGoods, listPrimaryKey);

                    baseResultDataValue = _AddGoodsData(dt, labEntity, compEntity, prodEntity, dicColumn, listPrimaryKey);
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "产品表导入配置信息不存在！";
                    ZhiFang.Common.Log.Log.Info("产品表导入配置信息不存在！");
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "产品表数据信息为空！";
                ZhiFang.Common.Log.Log.Info("产品表数据信息为空！");
            }
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

        public BaseResultDataValue _AddGoodsData(DataSet dataSet, CenOrg labEntity, CenOrg compEntity, CenOrg prodEntity, Dictionary<string, string> dicColumn, IList<string> listPrimaryKey)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            DataTable dataTable = dataSet.Tables[0];
            baseResultDataValue = _AddGoodsData(dataTable, labEntity, compEntity, prodEntity, dicColumn, listPrimaryKey);
            return baseResultDataValue;
        }

        public BaseResultDataValue _AddGoodsData(DataTable dataTable, CenOrg labEntity, CenOrg compEntity, CenOrg prodEntity, Dictionary<string, string> dicColumn, IList<string> listPrimaryKey)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            Dictionary<string, long> dicMain = new Dictionary<string, long>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                if (listPrimaryKey.Count > 0)
                {
                    string keyValue = "";
                    string keyHQL = "";
                    foreach (string strKey in listPrimaryKey)
                    {
                        if (!string.IsNullOrEmpty(dataRow[strKey].ToString()))
                        {
                            keyValue += "_" + dataRow[strKey].ToString().Trim();
                            keyHQL += " and " + " goods." + dicColumn[strKey] + "=\'" + dataRow[strKey].ToString().Trim() + "\'";
                        }
                    }
                    if ((!string.IsNullOrEmpty(keyValue)) && (!string.IsNullOrEmpty(keyHQL)))
                    {
                        if (!dicMain.ContainsKey(keyValue))
                        {
                            keyHQL = keyHQL.Remove(0, 4);
                            keyHQL += " and " + " goods.CenOrg.Id=" + labEntity.Id.ToString() +
                                " and " + " goods.Comp.Id=" + compEntity.Id.ToString() +
                                " and " + " goods.Prod.Id=" + prodEntity.Id.ToString();
                            IList<Goods> listGoods = null;
                            dicMain.Add(keyValue, 0);
                            listGoods = this.SearchListByHQL(keyHQL);
                            if (listGoods != null && listGoods.Count > 0)
                            {
                                dicMain[keyValue] = listGoods[0].Id;
                            }
                            else
                            {
                                Goods Goods = ExcelDataCommon.AddExcelDataToDataBase<Goods>(dataRow, dicColumn);
                                if (Goods != null)
                                {
                                    Goods.CenOrg = labEntity;
                                    Goods.Comp = compEntity;
                                    Goods.Prod = prodEntity;
                                    Goods.DataUpdateTime = DateTime.Now;
                                    Goods.Visible = 1;
                                    //if (!string.IsNullOrEmpty(SessionHelper.GetSessionValue(DicCookieSession.EmployeeID)))
                                    //    Goods. = Int64.Parse(SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                                    //Goods.UserName = SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                                    this.Entity = Goods;
                                    if (this.Add())
                                        dicMain[keyValue] = Goods.Id;
                                    else
                                        ZhiFang.Common.Log.Log.Info("产品表Goods保存失败！");
                                }
                            }
                        }
                    }
                }
                else
                    ZhiFang.Common.Log.Log.Info("产品表导入对照表没有设置唯一键！");
            }
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
                                goods.Visible = 1;
                                goods.DataUpdateTime = DateTime.Now;
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
                            goods.Visible = 1;
                            goods.DataUpdateTime = DateTime.Now;
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

        public BaseResultDataValue CopyGoodsByID(string listID, long compId, long cenOrgId)
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
                    Goods good = this.Get(long.Parse(id));
                    if (good != null)
                    {
                        string prodId = good.Prod != null ? good.Prod.Id.ToString() : "";
                        bool isExist = _judgeGoodsIsExist(compId.ToString(), cenOrgId.ToString(), good.GoodsNo);
                        if (!isExist)
                        {
                            Goods addGood = new Goods();
                            addGood.GoodsNo = good.GoodsNo;
                            addGood.CompGoodsNo = good.CompGoodsNo;
                            addGood.ProdGoodsNo = good.ProdGoodsNo;
                            addGood.CName = good.CName;
                            addGood.EName = good.EName;
                            addGood.ShortCode = good.ShortCode;
                            addGood.GoodsClass = good.GoodsClass;
                            addGood.GoodsClassType = good.GoodsClassType;
                            addGood.UnitName = good.UnitName;
                            addGood.UnitMemo = good.UnitMemo;
                            addGood.StorageType = good.StorageType;
                            addGood.GoodsDesc = good.GoodsDesc;
                            addGood.ApproveDocNo = good.ApproveDocNo;
                            addGood.Standard = good.Standard;
                            addGood.RegistNo = good.RegistNo;
                            addGood.RegistDate = good.RegistDate;
                            addGood.RegistNoInvalidDate = good.RegistNoInvalidDate;
                            addGood.Purpose = good.Purpose;
                            addGood.Constitute = good.Constitute;
                            addGood.License = good.License;
                            addGood.GoodsPicture = good.GoodsPicture;
                            addGood.DispOrder = good.DispOrder;
                            addGood.Visible = good.Visible;
                            addGood.Price = good.Price;
                            addGood.BiddingNo = good.BiddingNo;
                            addGood.ProdEara = good.ProdEara;
                            addGood.ProdOrgName = good.ProdOrgName;
                            addGood.ZX1 = good.ZX1;
                            addGood.ZX2 = good.ZX2;
                            addGood.ZX3 = good.ZX3;
                            addGood.Equipment = good.Equipment;
                            addGood.CenOrgConfirm = 0;
                            addGood.CompConfirm = 1;
                            addGood.DataUpdateTime = good.DataUpdateTime;
                            addGood.IsPrintBarCode = good.IsPrintBarCode;
                            addGood.IsRegister = good.IsRegister;
                            addGood.BarCodeMgr = good.BarCodeMgr;
                            addGood.SuitableType = good.SuitableType;
                            addGood.IsPrintBarCode = good.IsPrintBarCode;
                            addGood.Prod = good.Prod;
                            addGood.Comp = comp;
                            addGood.CenOrg = cenOrg;
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

        private bool _judgeGoodsIsExist(string compId, string cenOrgId, string goodsNo)
        {
            string where = " goods.Comp.Id=" + compId + " and goods.CenOrg.Id=" + cenOrgId;
            if (!string.IsNullOrEmpty(goodsNo))
                where += " and goods.GoodsNo=\'" + goodsNo + "\'";
            IList<Goods> listGoods = this.SearchListByHQL(where);
            return (listGoods != null && listGoods.Count > 0);
        }

        public DataSet GetGoodsInfoByID(string idList, string where, string xmlPath)
        {
            IList<Goods> listGoods = null;
            if (string.IsNullOrEmpty(idList) && string.IsNullOrEmpty(where))
                return null;
            else
            {
                if (!string.IsNullOrEmpty(idList))
                    listGoods = this.SearchListByHQL(" goods.Id in (" + idList + ")");
                else if (!string.IsNullOrEmpty(where))
                    listGoods = this.SearchListByHQL(where);
                return CommonRS.GetListObjectToDataSet<Goods>(listGoods, xmlPath);
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
    }
}