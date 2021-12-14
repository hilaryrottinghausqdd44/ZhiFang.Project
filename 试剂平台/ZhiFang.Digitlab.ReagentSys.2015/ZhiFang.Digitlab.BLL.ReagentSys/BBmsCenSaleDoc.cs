using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.Digitlab.IBLL;
using ZhiFang.Digitlab.IDAO;
using ZhiFang.Digitlab.IBLL.ReagentSys;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Common.Public;
using ZhiFang.Common.Log;
using ZhiFang.Digitlab.IDAO.ReagentSys;
using ZhiFang.Digitlab.IBLL.RBAC;
using ZhiFang.Digitlab.IBLL.Business;
using Newtonsoft.Json.Linq;

namespace ZhiFang.Digitlab.BLL.ReagentSys
{
    /// <summary>
    ///
    /// </summary>
    public class BBmsCenSaleDoc : BaseBLL<BmsCenSaleDoc>, ZhiFang.Digitlab.IBLL.ReagentSys.IBBmsCenSaleDoc
    {
        //ZhiFang.Digitlab.IBLL.Business.IBBLaboratory IBBLaboratory { get; set; }
        IBGoods IBGoods { get; set; }
        IBCenOrg IBCenOrg { get; set; }
        IBCenOrgCondition IBCenOrgCondition { get; set; }
        IBBmsCenSaleDtl IBBmsCenSaleDtl { get; set; }
        IBBmsCenSaleDtlBarCode IBBmsCenSaleDtlBarCode { get; set; }
        IBBmsCenOrderDoc IBBmsCenOrderDoc { get; set; }
        IBHRDept IBHRDept { get; set; }
        IBBSampleOperate IBBSampleOperate { get; set; }

        public BaseResultData AddBmsCenSaleDoc(string jsonEntity)
        {
            BaseResultData baseresultdata = new BaseResultData();
            JObject jsonObject = JObject.Parse(jsonEntity);
            JToken tokenSaleDoc = jsonObject.SelectToken("SaleDocInfo");
            string labNo = jsonObject.SelectToken("LabNo").ToString();
            string compNo = jsonObject.SelectToken("CompNo").ToString();
            string saleDocNo = jsonObject.SelectToken("SaleDocNo").ToString();
            baseresultdata = AddBmsCenSaleDoc(jsonEntity, saleDocNo, labNo, compNo);
            return baseresultdata;
        }

        public BaseResultData AddBmsCenSaleDoc(string jsonEntity, string saleDocNo, string labNo, string compNo)
        {
            BaseResultData baseresultdata = new BaseResultData();
            CenOrg compOrg = null;
            BaseResultDataValue brdvComp = IBCenOrg.JudgeOrgIsExist(compNo, ref compOrg);
            if (!brdvComp.success)
            {
                baseresultdata.success = false;
                baseresultdata.code = "";
                baseresultdata.message = brdvComp.ErrorInfo;
                return baseresultdata;
            }
            CenOrg labOrg = null;
            BaseResultDataValue brdvLab = IBCenOrg.JudgeOrgIsExist(labNo, ref labOrg);
            if (!brdvLab.success)
            {
                baseresultdata.success = false;
                baseresultdata.code = "";
                baseresultdata.message = brdvLab.ErrorInfo;
                return baseresultdata;
            }
            IList<BmsCenSaleDoc> listSaleDoc = this.SearchListByHQL(" bmscensaledoc.SaleDocNo=\'" + saleDocNo + "\'" + " and bmscensaledoc.Comp.Id=" + compOrg.Id.ToString());
            if (listSaleDoc == null || listSaleDoc.Count == 0)
            {
                BmsCenSaleDoc entitySaleDoc = JsonSerializer.JsonDotNetDeserializeObjectIgnoreNull<BmsCenSaleDoc>(jsonEntity);
                entitySaleDoc.Comp = compOrg;
                entitySaleDoc.Lab = labOrg;
                if (string.IsNullOrWhiteSpace(entitySaleDoc.CompanyName))
                    entitySaleDoc.CompanyName = compOrg.CName;
                if (string.IsNullOrWhiteSpace(entitySaleDoc.LabName))
                    entitySaleDoc.LabName = labOrg.CName;
                IList<BmsCenSaleDtl> listSaleDtl = entitySaleDoc.BmsCenSaleDtlList;
                if (listSaleDtl != null && listSaleDtl.Count > 0)
                {
                    foreach (BmsCenSaleDtl saleDtl in listSaleDtl)
                    {
                        Goods goods = null;
                        BaseResultDataValue brdvGoods = IBGoods.JudgeGoodsIsExist(compOrg.Id.ToString(), labOrg.Id.ToString(), saleDtl.GoodsNo, ref goods);
                        if (!brdvGoods.success)
                        {
                            baseresultdata.success = false;
                            baseresultdata.code = "";
                            baseresultdata.message = brdvGoods.ErrorInfo;
                            return baseresultdata;
                        }
                        saleDtl.Goods = goods;
                        EidtSaleDtlGoodsInfo(saleDtl);
                    }
                    EidtSaleDocGoodsInfo(entitySaleDoc);
                    this.Entity = entitySaleDoc;
                    this.Add();
                }
            }
            else
            {
                baseresultdata.success = false;
                baseresultdata.code = "";
                baseresultdata.message = "供货号为【" + saleDocNo + "】的供单已存在！";
            }
            return baseresultdata;
        }

        public BaseResultDataValue AddBmsCenSaleDtlBarCodeList(long SaleDtlID, string SaleDtlBarCodeIDList, IList<BmsCenSaleDtlBarCode> listBmsCenSaleDtlBarCode)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                BmsCenSaleDtl BmsCenSaleDtl = IBBmsCenSaleDtl.Get(SaleDtlID);
                if (BmsCenSaleDtl != null)
                {
                    if (!string.IsNullOrEmpty(SaleDtlBarCodeIDList))
                    {
                        string[] listID = SaleDtlBarCodeIDList.Split(',');
                        foreach (string id in listID)
                        {
                            IBBmsCenSaleDtlBarCode.Remove(Int64.Parse(id));
                        }
                    }
                    if (listBmsCenSaleDtlBarCode != null && listBmsCenSaleDtlBarCode.Count > 0)
                    {
                        foreach (BmsCenSaleDtlBarCode bmsCenSaleDtlBarCode in listBmsCenSaleDtlBarCode)
                        {
                            bmsCenSaleDtlBarCode.BmsCenSaleDtl = BmsCenSaleDtl;
                            IBBmsCenSaleDtlBarCode.Entity = bmsCenSaleDtlBarCode;
                            IBBmsCenSaleDtlBarCode.Add();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ReadBmsCenSaleDocDataFormExcel(string labID, string compID, string excelFilePath, string serverPath)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            DataSet dataSet = ExcelDataCommon.GetDataSetByExcelFile(excelFilePath);
            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                string xmlBmsCenSaleDoc = serverPath + "\\BaseTableXML\\BmsCenSaleDoc.xml";
                string xmlBmsCenSaleDtl = serverPath + "\\BaseTableXML\\BmsCenSaleDtl.xml";
                if (System.IO.File.Exists(xmlBmsCenSaleDoc) && System.IO.File.Exists(xmlBmsCenSaleDtl))
                {

                    CenOrg labEntity = IBCenOrg.Get(Int64.Parse(labID)); //获取实验室实体
                    CenOrg compEntity = IBCenOrg.Get(Int64.Parse(compID));//获取供应商实体
                    IList<string> listPrimaryKey = new List<string>();
                    Dictionary<string, string> dicColumn = ExcelDataCommon.GetColumnNameByDataSet(dataSet, xmlBmsCenSaleDoc, listPrimaryKey, null);
                    IList<string> listPrimaryKeyDtl = new List<string>();
                    Dictionary<string, string> dicColumnDtl = ExcelDataCommon.GetColumnNameByDataSet(dataSet, xmlBmsCenSaleDtl, listPrimaryKeyDtl, null);

                    baseResultDataValue = _AddBmsCenSaleDocData(dataSet, labEntity, compEntity, dicColumn, listPrimaryKey, dicColumnDtl, listPrimaryKeyDtl);
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "供货单总表和明细表导入配置信息不存在！";
                    ZhiFang.Common.Log.Log.Info("供货单总表BmsCenSaleDoc.xml和明细表BmsCenSaleDtl.xml导入配置信息不存在！");
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "供货单表数据信息为空！";
                ZhiFang.Common.Log.Log.Info("供货单表数据信息为空！");
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue _AddBmsCenSaleDocData(DataSet dataSet, CenOrg labEntity, CenOrg compEntity, Dictionary<string, string> dicColumn, IList<string> listPrimaryKey, Dictionary<string, string> dicColumnDtl, IList<string> listPrimaryKeyDtl)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            DataTable dataTable = dataSet.Tables[0];
            Dictionary<string, long> dicMain = new Dictionary<string, long>();
            Dictionary<string, long> dicMainDtl = new Dictionary<string, long>();
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
                            keyValue += "_" + dataRow[strKey].ToString();
                            keyHQL += " and " + " bmscensaledoc." + dicColumn[strKey] + "=\'" + dataRow[strKey].ToString() + "\'";
                        }
                    }
                    if ((!string.IsNullOrEmpty(keyValue)) && (!string.IsNullOrEmpty(keyHQL)))
                    {
                        if (!dicMain.ContainsKey(keyValue))
                        {
                            keyHQL = keyHQL.Remove(0, 4);
                            IList<BmsCenSaleDoc> listBmsCenSaleDoc = null;
                            dicMain.Add(keyValue, 0);
                            listBmsCenSaleDoc = this.SearchListByHQL(keyHQL);
                            if (listBmsCenSaleDoc != null && listBmsCenSaleDoc.Count > 0)
                            {
                                dicMain[keyValue] = listBmsCenSaleDoc[0].Id;
                                _AddBmsCenSaleDtlData(dataRow, this.Get(dicMain[keyValue]), dicColumnDtl, listPrimaryKeyDtl, dicMainDtl);
                            }
                            else
                            {
                                BmsCenSaleDoc bmsCenSaleDoc = ExcelDataCommon.AddExcelDataToDataBase<BmsCenSaleDoc>(dataRow, dicColumn, null);
                                if (bmsCenSaleDoc != null)
                                {
                                    bmsCenSaleDoc.Lab = labEntity;
                                    bmsCenSaleDoc.Comp = compEntity;
                                    bmsCenSaleDoc.CompanyName = compEntity.CName;
                                    if (!string.IsNullOrEmpty(SessionHelper.GetSessionValue(DicCookieSession.EmployeeID)))
                                        bmsCenSaleDoc.UserID = Int64.Parse(SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                                    bmsCenSaleDoc.UserName = SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                                    this.Entity = bmsCenSaleDoc;
                                    if (this.Add())
                                    {
                                        dicMain[keyValue] = bmsCenSaleDoc.Id;
                                        _AddBmsCenSaleDtlData(dataRow, this.Entity, dicColumnDtl, listPrimaryKeyDtl, dicMainDtl);
                                    }
                                    else
                                        ZhiFang.Common.Log.Log.Info("平台供货总单表BmsCenSaleDoc保存失败！");
                                }
                            }
                        }
                        else
                            _AddBmsCenSaleDtlData(dataRow, this.Get(dicMain[keyValue]), dicColumnDtl, listPrimaryKeyDtl, dicMainDtl);
                    }
                }
                else
                    ZhiFang.Common.Log.Log.Info("平台供货单表导入对照表没有设置唯一键！");
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue _AddBmsCenSaleDtlData(DataRow dataRow, BmsCenSaleDoc bmsCenSaleDoc, Dictionary<string, string> dicColumn, IList<string> listPrimaryKey, Dictionary<string, long> dicMain)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (listPrimaryKey.Count > 0)
            {
                string keyValue = "";
                string keyHQL = "";
                foreach (string strKey in listPrimaryKey)
                {
                    if (!string.IsNullOrEmpty(dataRow[strKey].ToString()))
                    {
                        keyValue += "_" + dataRow[strKey].ToString();
                        keyHQL += " and " + " bmscensaledtl." + dicColumn[strKey] + "=\'" + dataRow[strKey].ToString() + "\'";
                    }
                }
                if ((!string.IsNullOrEmpty(keyValue)) && (!string.IsNullOrEmpty(keyHQL)))
                {
                    if (!dicMain.ContainsKey(keyValue))
                    {
                        keyHQL = keyHQL.Remove(0, 4);
                        IList<BmsCenSaleDtl> listBmsCenSaleDtl = null;
                        dicMain.Add(keyValue, 0);
                        listBmsCenSaleDtl = IBBmsCenSaleDtl.SearchListByHQL(keyHQL);
                        if (listBmsCenSaleDtl != null && listBmsCenSaleDtl.Count > 0)
                        {
                            dicMain[keyValue] = listBmsCenSaleDtl[0].Id;
                        }
                        else
                        {
                            BmsCenSaleDtl bmsCenSaleDtl = ExcelDataCommon.AddExcelDataToDataBase<BmsCenSaleDtl>(dataRow, dicColumn, null);
                            if (bmsCenSaleDtl != null)
                            {
                                bmsCenSaleDtl.BmsCenSaleDoc = bmsCenSaleDoc;
                                bmsCenSaleDtl.Goods = _getGoodsByDataRow(dataRow);
                                IBBmsCenSaleDtl.Entity = bmsCenSaleDtl;
                                if (IBBmsCenSaleDtl.Add())
                                {
                                    dicMain[keyValue] = bmsCenSaleDtl.Id;
                                }
                                else
                                    ZhiFang.Common.Log.Log.Info("平台供货明细表BmsCenSaleDtl保存失败！");
                            }
                        }
                    }
                }
            }
            else
                ZhiFang.Common.Log.Log.Info("平台供货明细表导入对照表没有设置唯一键！");
            return baseResultDataValue;
        }

        protected Goods _getGoodsByDataRow(DataRow dataRow)
        {
            Goods goods = null;
            IList<Goods> listGoods = IBGoods.SearchListByHQL(" goods.GoodsNo=\'" + dataRow["产品编号"].ToString() + "\'");
            if (listGoods != null && listGoods.Count > 0)
            {
                goods = listGoods[0];
            }
            return goods;
        }

        /// <summary>
        /// 订货单自动转供货单
        /// </summary>
        /// <param name="bmsCenOrderDocID">订货单ID</param>
        /// <returns></returns>
        public BaseResultDataValue AddBmsCenSaleDocByOrderDoc(long bmsCenOrderDocID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                BmsCenOrderDoc bmsCenOrderDoc = IBBmsCenOrderDoc.Get(bmsCenOrderDocID);
                if (bmsCenOrderDoc != null && bmsCenOrderDoc.BmsCenOrderDtlList != null && bmsCenOrderDoc.BmsCenOrderDtlList.Count > 0)
                {
                    BmsCenSaleDoc bmsCenSaleDoc = new BmsCenSaleDoc();
                    bmsCenSaleDoc.BmsCenOrderDoc = bmsCenOrderDoc;  //2017-09-28
                    bmsCenSaleDoc.OrderDocNo = bmsCenOrderDoc.OrderDocNo;  //2017-09-28
                    bmsCenSaleDoc.Lab = bmsCenOrderDoc.Lab;
                    bmsCenSaleDoc.LabName = bmsCenOrderDoc.LabName;
                    bmsCenSaleDoc.Comp = bmsCenOrderDoc.Comp;
                    bmsCenSaleDoc.CompanyName = bmsCenOrderDoc.CompanyName;
                    bmsCenSaleDoc.UrgentFlag = bmsCenOrderDoc.UrgentFlag;
                    bmsCenSaleDoc.Status = 0;//临时状态
                    bmsCenSaleDoc.OperDate = DateTime.Now;
                    bmsCenSaleDoc.Memo = "订单自动转供货单";
                    bmsCenSaleDoc.Source = 1;//平台(供应商)
                    bmsCenSaleDoc.DataAddTime = DateTime.Now;
                    bmsCenSaleDoc.TotalPrice = bmsCenOrderDoc.TotalPrice;//总价赋值
                    if (!string.IsNullOrEmpty(SessionHelper.GetSessionValue(DicCookieSession.EmployeeID)))
                        bmsCenSaleDoc.UserID = Int64.Parse(SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                    bmsCenSaleDoc.UserName = SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);

                    this.Entity = bmsCenSaleDoc;
                    if (this.Add())
                    {
                        foreach (BmsCenOrderDtl bmsCenOrderDtl in bmsCenOrderDoc.BmsCenOrderDtlList)
                        {
                            BmsCenSaleDtl bmsCenSaleDtl = new BmsCenSaleDtl();
                            bmsCenSaleDtl.BmsCenSaleDoc = this.Entity;
                            bmsCenSaleDtl.Goods = bmsCenOrderDtl.Goods;
                            bmsCenSaleDtl.ProdGoodsNo = bmsCenOrderDtl.ProdGoodsNo;
                            bmsCenSaleDtl.Prod = bmsCenOrderDtl.Prod;
                            bmsCenSaleDtl.ProdOrgName = bmsCenOrderDtl.ProdOrgName;
                            bmsCenSaleDtl.GoodsName = bmsCenOrderDtl.GoodsName;
                            bmsCenSaleDtl.GoodsUnit = bmsCenOrderDtl.GoodsUnit;
                            bmsCenSaleDtl.UnitMemo = bmsCenOrderDtl.UnitMemo;
                            bmsCenSaleDtl.GoodsQty = bmsCenOrderDtl.GoodsQty;
                            bmsCenSaleDtl.Price = bmsCenOrderDtl.Goods.Price;
                            bmsCenSaleDtl.ShortCode = bmsCenOrderDtl.Goods.ShortCode;
                            bmsCenSaleDtl.SaleDtlNo = bmsCenOrderDtl.OrderDtlNo;//Jcall新增于2017-03-13，用于拷贝订单的明细单号
                            if (bmsCenSaleDtl.GoodsQty > 0 && bmsCenSaleDtl.Price > 0)
                                bmsCenSaleDtl.SumTotal = bmsCenSaleDtl.GoodsQty * bmsCenSaleDtl.Price;
                            bmsCenSaleDtl.DataAddTime = DateTime.Now;
                            if (bmsCenOrderDtl.Goods != null)
                                bmsCenSaleDtl.BarCodeMgr = bmsCenOrderDtl.Goods.BarCodeMgr;
                            IBBmsCenSaleDtl.Entity = bmsCenSaleDtl;
                            IBBmsCenSaleDtl.Add();
                        }
                    }
                    if (!string.IsNullOrEmpty(SessionHelper.GetSessionValue(DicCookieSession.EmployeeID)))
                        bmsCenOrderDoc.ConfirmID = Int64.Parse(SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                    bmsCenOrderDoc.Confirm = SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                    bmsCenOrderDoc.ConfirmTime = DateTime.Now;
                    IBBmsCenOrderDoc.Entity = bmsCenOrderDoc;
                    IBBmsCenOrderDoc.Edit();
                    //EditBmsCenSaleDocTotalPrice(bmsCenSaleDoc.Id);
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：AddBmsCenSaleDocByOrderDoc函数的参数bmsCenOrderDoc或此参数的BmsCenOrderDtlList属性为空!";
                    ZhiFang.Common.Log.Log.Info(baseResultDataValue.ErrorInfo);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Info(baseResultDataValue.ErrorInfo);
                throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue AddConfirmSaleDocByIDList(string listID, int isSplit, int isTemp)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string info = "BLL---" + this.GetType().Name + "---【AddConfirmSaleDocByIDList】";
            if ((listID != null) && (listID.Length > 0))
            {
                IList<string> docIDList = listID.Split(',').ToList();
                foreach (string docID in docIDList)
                {
                    BmsCenSaleDoc bmsCenSaleDoc = this.Get(long.Parse(docID));
                    if (bmsCenSaleDoc != null && bmsCenSaleDoc.BmsCenSaleDtlList != null && bmsCenSaleDoc.BmsCenSaleDtlList.Count > 0)
                    {
                        if (isSplit == 0)
                        {
                            baseResultDataValue = AddSplitBmsCenSaleDtl(bmsCenSaleDoc, 0);
                            bmsCenSaleDoc.IsSplit = 1;
                        }
                        if (baseResultDataValue.success)
                            baseResultDataValue = EditConfirmBmsCenSaleDoc(bmsCenSaleDoc, "", "", SessionHelper.GetSessionValue(DicCookieSession.EmployeeID), SessionHelper.GetSessionValue(DicCookieSession.EmployeeName), "", "", null, null, isTemp);
                    }
                    else
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "错误信息：无法根据ID【" + docID + "】获取供货单实体或供货单不存在子单！";
                        ZhiFang.Common.Log.Log.Info(info + baseResultDataValue.ErrorInfo);
                    }
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：listID参数不能为空！";
                ZhiFang.Common.Log.Log.Info(info + baseResultDataValue.ErrorInfo);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue AddConfirmSaleDocByID(string docID, string invoiceNo, string accepterMemo, string accepterID, string accepterName, string secAccepterID, string secAccepterName, IList<BmsCenSaleDtl> listNoConfirm, int isSplit, int isTemp)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string info = "BLL---" + this.GetType().Name + "---【AddConfirmSaleDocByID】";
            BmsCenSaleDoc bmsCenSaleDoc = this.Get(long.Parse(docID));
            if (bmsCenSaleDoc != null && bmsCenSaleDoc.BmsCenSaleDtlList != null && bmsCenSaleDoc.BmsCenSaleDtlList.Count > 0)
            {
                if (isSplit == 0)
                {
                    baseResultDataValue = AddSplitBmsCenSaleDtl(bmsCenSaleDoc, listNoConfirm, null, 0);
                    bmsCenSaleDoc.IsSplit = 1;//1为已拆分
                }
                if (baseResultDataValue.success)
                    baseResultDataValue = EditConfirmBmsCenSaleDoc(bmsCenSaleDoc, invoiceNo, accepterMemo, accepterID, accepterName, secAccepterID, secAccepterName, listNoConfirm, null, isTemp);
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：无法根据ID【" + docID + "】获取供货单实体或供货单不存在子单！";
                ZhiFang.Common.Log.Log.Info(info + baseResultDataValue.ErrorInfo);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue AddConfirmSaleDocByID(string docID, string invoiceNo, string accepterMemo, string accepterID, string accepterName, string secAccepterID, string secAccepterName, IList<BmsCenSaleDtl> listNoConfirm, IList<BmsCenSaleDtl> listBatchBarCodeConfirm, int isSplit, int isTemp)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string info = "BLL---" + this.GetType().Name + "---【AddConfirmSaleDocByID】";
            BmsCenSaleDoc bmsCenSaleDoc = this.Get(long.Parse(docID));
            if (bmsCenSaleDoc != null && bmsCenSaleDoc.BmsCenSaleDtlList != null && bmsCenSaleDoc.BmsCenSaleDtlList.Count > 0)
            { 
                if (isSplit == 0)
                {
                    baseResultDataValue = AddSplitBmsCenSaleDtl(bmsCenSaleDoc, 0);
                    bmsCenSaleDoc.IsSplit = 1;//1为已拆分
                }
                if (baseResultDataValue.success)
                    baseResultDataValue = EditConfirmBmsCenSaleDoc(bmsCenSaleDoc, invoiceNo, accepterMemo, accepterID, accepterName, secAccepterID, secAccepterName, listNoConfirm, listBatchBarCodeConfirm, isTemp);
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：无法根据ID【" + docID + "】获取供货单信息或供货单不存在产品信息！";
                ZhiFang.Common.Log.Log.Info(info + baseResultDataValue.ErrorInfo);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue EditConfirmSaleDocByIDAndDtlIDList(string saleDocID, string saleDtlIDList)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            BmsCenSaleDoc saleDoc = this.Get(long.Parse(saleDocID));
            if (saleDoc != null)
            {
                saleDoc.Status = 1;
                this.Entity = saleDoc;
                baseResultDataValue.success = this.Edit();
                if (baseResultDataValue.success && (!string.IsNullOrEmpty(saleDtlIDList)))
                {
                    IList<string> listID = saleDtlIDList.Split(',').ToList();
                    foreach (string id in listID)
                    {
                        BmsCenSaleDtl saleDtl = IBBmsCenSaleDtl.Get(long.Parse(id));
                        if (saleDtl != null)
                        {
                            saleDtl.MixSerial = "";
                            IBBmsCenSaleDtl.Entity = saleDtl;
                            IBBmsCenSaleDtl.Edit();
                        }
                    }
                }
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 供货单验收
        /// 此功能只针对已经拆分的供货单
        /// </summary>
        /// <param name="saleDocID">供货单ID</param>
        /// <param name="listNoConfirm">盒条码--不需验收的明细列表</param>
        /// <param name="listConfirm">批条码或无条码--需验收的明细列表</param>
        /// <param name="invoiceNo">发票号</param>
        /// <param name="accepterMemo">验收备注</param>
        /// <param name="accepterID">主验收人ID</param>
        /// <param name="accepterName">主验收人</param>
        /// <param name="secAccepterID">次验收人ID</param>
        /// <param name="secAccepterName">次主验收人</param>
        /// <param name="isTemp">是否暂存.1为暂存;否则,不暂存直接确认验收</param>
        /// <returns></returns>
        public BaseResultDataValue EditConfirmBmsCenSaleDoc(long saleDocID, string invoiceNo, string accepterMemo, string accepterID, string accepterName, string secAccepterID, string secAccepterName, IList<BmsCenSaleDtl> listNoConfirm, IList<BmsCenSaleDtl> listBatchBarCodeConfirm, int isTemp)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            BmsCenSaleDoc bmsCenSaleDoc = this.Get(saleDocID);
            if (bmsCenSaleDoc != null && bmsCenSaleDoc.BmsCenSaleDtlList != null && bmsCenSaleDoc.BmsCenSaleDtlList.Count > 0)
            {
                baseResultDataValue = EditConfirmBmsCenSaleDoc(bmsCenSaleDoc, invoiceNo, accepterMemo, accepterID, accepterName, secAccepterID, secAccepterName, listNoConfirm, listBatchBarCodeConfirm, isTemp);
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 供货单验收
        /// 此功能只针对已经拆分的供货单
        /// </summary>
        /// <param name="bmsCenSaleDoc">供货单实体</param>
        /// <param name="listNoConfirm">盒条码--不需验收的明细列表</param>
        /// <param name="listConfirm">批条码或无条码--需验收的明细列表</param>
        /// <param name="invoiceNo">发票号</param>
        /// <param name="accepterMemo">验收备注</param>
        /// <param name="accepterID">主验收人ID</param>
        /// <param name="accepterName">主验收人</param>
        /// <param name="secAccepterID">次验收人ID</param>
        /// <param name="secAccepterName">次主验收人</param>
        /// <param name="isTemp">是否暂存.1为暂存;否则,不暂存直接确认验收</param>
        /// <returns></returns>
        public BaseResultDataValue EditConfirmBmsCenSaleDoc(BmsCenSaleDoc bmsCenSaleDoc, string invoiceNo, string accepterMemo, string accepterID, string accepterName, string secAccepterID, string secAccepterName, IList<BmsCenSaleDtl> listNoConfirm, IList<BmsCenSaleDtl> listBatchBarCodeConfirm, int isTemp)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            bool isAccepterError = false;
            IList<BmsCenSaleDtl> listBoxBarCode = bmsCenSaleDoc.BmsCenSaleDtlList.Where(p => p.Goods.BarCodeMgr == 1).ToList();
            if (listBoxBarCode != null && listBoxBarCode.Count > 0)
            {
                isAccepterError = (listNoConfirm != null && listNoConfirm.Count > 0);
                foreach (BmsCenSaleDtl bmsCenSaleDtl in listBoxBarCode)
                {
                    if (listNoConfirm != null && listNoConfirm.Count > 0)
                    {
                        IList<BmsCenSaleDtl> tempList = listNoConfirm.Where(p => p.Id == bmsCenSaleDtl.Id).ToList();
                        if (tempList != null && tempList.Count > 0)
                        {
                            if (isTemp != 1)//不暂存
                            {
                                bmsCenSaleDtl.AccepterErrorMsg = tempList[0].AccepterErrorMsg;
                                bmsCenSaleDtl.MixSerial = "";
                                bmsCenSaleDtl.AcceptCount = 0;
                            }
                        }
                        else
                            bmsCenSaleDtl.AcceptCount = bmsCenSaleDtl.GoodsQty;
                    }
                    else
                        bmsCenSaleDtl.AcceptCount = bmsCenSaleDtl.GoodsQty;
                    IBBmsCenSaleDtl.Entity = bmsCenSaleDtl;
                    IBBmsCenSaleDtl.Edit();
                }
            }
            IList<BmsCenSaleDtl> listBatchBarCode = bmsCenSaleDoc.BmsCenSaleDtlList.Where(p => p.Goods.BarCodeMgr != 1).ToList();
            if (listBatchBarCode != null && listBatchBarCode.Count > 0 && listBatchBarCodeConfirm != null && listBatchBarCodeConfirm.Count > 0)
            {
                foreach (BmsCenSaleDtl bmsCenSaleDtl in listBatchBarCode)
                {
                    IList<BmsCenSaleDtl> tempList = listBatchBarCodeConfirm.Where(p => p.Id == bmsCenSaleDtl.Id).ToList();
                    if (tempList != null && tempList.Count > 0)
                    {
                        bmsCenSaleDtl.AcceptCount = tempList[0].AcceptCount;
                        IBBmsCenSaleDtl.Entity = bmsCenSaleDtl;
                        IBBmsCenSaleDtl.Edit();
                        isAccepterError = (bmsCenSaleDtl.AcceptCount > 0 && bmsCenSaleDtl.AcceptCount != bmsCenSaleDtl.GoodsQty);
                    }
                }
            }
            string strConfirm = "供货单验收";
            if (isTemp == 1)
                strConfirm = "供货单暂存";
            else
            {
                bmsCenSaleDoc.Status = 1;//1为验收状态
                if (!string.IsNullOrWhiteSpace(accepterID))
                {
                    bmsCenSaleDoc.AccepterID = long.Parse(accepterID);
                    bmsCenSaleDoc.AccepterName = accepterName;
                }
                if (!string.IsNullOrWhiteSpace(secAccepterID))
                {
                    bmsCenSaleDoc.SecAccepterID = long.Parse(secAccepterID);
                    bmsCenSaleDoc.SecAccepterName = secAccepterName;
                }
                if (!string.IsNullOrWhiteSpace(invoiceNo))
                    bmsCenSaleDoc.InvoiceNo = invoiceNo;
                if (!string.IsNullOrWhiteSpace(accepterMemo))
                    bmsCenSaleDoc.AccepterMemo = accepterMemo;
                bmsCenSaleDoc.IsAccepterError = isAccepterError;//有未验收的产品子单信息
                bmsCenSaleDoc.AccepterTime = DateTime.Now;
            }
            this.Entity = bmsCenSaleDoc;
            if (this.Edit())
                IBBSampleOperate.AddObjectOperate(bmsCenSaleDoc, "BmsCenSaleDoc", "BmsCenSaleDocConfirm", strConfirm);
            return baseResultDataValue;
        }

        /// <summary>
        /// 供货单取消验收并撤销拆分,取消后为已审核状态Status=2 Add 2017-03-24
        /// 此功能需要验证撤销人账号信息
        /// </summary>
        /// <param name="docID"></param>
        /// <param name="reason"></param>
        /// <param name="accepterAccount"></param>
        /// <param name="empID"></param>
        /// <returns></returns>
        public BaseResultDataValue EditConfirmSaleDocCancel(string docID, string reason, string accepterAccount, long secAccepterID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            BmsCenSaleDoc bmsCenSaleDoc = this.Get(long.Parse(docID));
            if (bmsCenSaleDoc != null)
            {
                if (bmsCenSaleDoc.IOFlag != 1)
                {
                    IList<BmsCenSaleDtl> listDtl = bmsCenSaleDoc.BmsCenSaleDtlList.Where(p => p.IOFlag == 1).ToList();
                    if (listDtl != null && listDtl.Count > 0)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "错误信息：该供货单已有部分产品入库，请先取消入库再尝试！";
                    }
                    else
                    {
                        string empID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                        if (bmsCenSaleDoc.AccepterID == long.Parse(empID))
                        {
                            if (bmsCenSaleDoc.SecAccepterID == secAccepterID)
                            {
                                //主逻辑
                                baseResultDataValue = EditConfirmSaleDocCancelByID(bmsCenSaleDoc.Id, reason);
                            }
                            else
                            {
                                baseResultDataValue.success = false;
                                baseResultDataValue.ErrorInfo = "错误信息：该供货单供应确认账号错误，请重新输入！";
                            }
                        }
                        else
                        {
                            baseResultDataValue.success = false;
                            baseResultDataValue.ErrorInfo = "错误信息：该供货单主验收人不是当前登录用户，请使用主验收人账号登录系统后再操作！";
                        }
                    }
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：该供货单已全部入库，请先取消入库再尝试！";
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：无法根据供货单ID找到供货单信息！";
            }
            if (!string.IsNullOrEmpty(baseResultDataValue.ErrorInfo))
                ZhiFang.Common.Log.Log.Error(baseResultDataValue.ErrorInfo);
            return baseResultDataValue;
        }

        /// <summary>
        /// 供货单取消验收并撤销拆分,取消后为已审核状态Status=2
        /// </summary>
        /// <param name="docID">供货单ID</param>
        /// <param name="reason">取消原因</param>
        /// <returns></returns>
        public BaseResultDataValue EditConfirmSaleDocCancelByID(long docID, string reason)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            BmsCenSaleDoc bmsCenSaleDoc = this.Get(docID);
            bmsCenSaleDoc.Status = (int)SaleDocStatus.已审核;
            bmsCenSaleDoc.AccepterID = null;
            bmsCenSaleDoc.AccepterName = null;
            bmsCenSaleDoc.SecAccepterID = null;
            bmsCenSaleDoc.SecAccepterName = null;
            bmsCenSaleDoc.AccepterTime = null;
            bmsCenSaleDoc.AccepterMemo = null;
            bmsCenSaleDoc.IsAccepterError = false;
            this.Entity = bmsCenSaleDoc;
            if (this.Edit())
            {
                if (bmsCenSaleDoc.IsSplit == (int)SaleDocIsSplit.已拆分)
                    baseResultDataValue = EditSplitSaleDocCancelByID(docID, reason);
                IBBSampleOperate.AddObjectOperate(bmsCenSaleDoc, "BmsCenSaleDoc", "BmsCenSaleDocConfirmCancel", reason);
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：供货单取消验收失败！";
            }
            if (!string.IsNullOrEmpty(baseResultDataValue.ErrorInfo))
                ZhiFang.Common.Log.Log.Error(baseResultDataValue.ErrorInfo);
            return baseResultDataValue;
        }

        /// <summary>
        /// 撤销供货单明细拆分
        /// </summary>
        /// <param name="docID">供货单ID</param>
        /// <param name="reason">撤销原因</param>
        /// <returns></returns>
        public BaseResultDataValue EditSplitSaleDocCancelByID(long docID, string reason)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (string.IsNullOrEmpty(reason))
                reason = "供货单拆分撤销";
            else
                reason = "供货单拆分撤销---" + reason;
            BmsCenSaleDoc bmsCenSaleDoc = this.Get(docID);
            if (bmsCenSaleDoc.IsSplit == 1)
            {
                bmsCenSaleDoc.IsSplit = 0;
                this.Entity = bmsCenSaleDoc;
                if (this.Edit())
                {
                    IList<string> listID = new List<string>();
                    foreach (BmsCenSaleDtl dtl in bmsCenSaleDoc.BmsCenSaleDtlList)
                    {
                        Goods goods = dtl.Goods;
                        if (goods != null && goods.BarCodeMgr == 1)
                        {
                            string dtlID = "";
                            dtlID = dtl.PSaleDtlID > 0 ? dtl.PSaleDtlID.ToString() : dtl.GoodsSerial;
                            if (listID.IndexOf(dtlID) < 0)
                            {
                                listID.Add(dtlID);
                            }
                        }
                    }
                    for (int i = bmsCenSaleDoc.BmsCenSaleDtlList.Count - 1; i >= 0; i--)
                    {
                        Goods goods = bmsCenSaleDoc.BmsCenSaleDtlList[i].Goods;
                        if (goods != null && goods.BarCodeMgr == 1)
                        {
                            if (listID.IndexOf(bmsCenSaleDoc.BmsCenSaleDtlList[i].Id.ToString()) < 0)
                            {
                                bmsCenSaleDoc.BmsCenSaleDtlList[i].BmsCenSaleDoc = null;
                                IBBmsCenSaleDtl.Entity = bmsCenSaleDoc.BmsCenSaleDtlList[i];
                                IBBmsCenSaleDtl.Remove();
                                bmsCenSaleDoc.BmsCenSaleDtlList.Remove(bmsCenSaleDoc.BmsCenSaleDtlList[i]);
                            }
                            else
                            {
                                BmsCenSaleDtl bmsCenSaleDtl = bmsCenSaleDoc.BmsCenSaleDtlList[i];
                                bmsCenSaleDtl.GoodsQty = bmsCenSaleDtl.DtlCount;
                                bmsCenSaleDtl.AcceptCount = 0;
                                bmsCenSaleDtl.AccepterErrorMsg = null;
                                bmsCenSaleDtl.DispOrder = 0;
                                bmsCenSaleDtl.MixSerial = null;
                                bmsCenSaleDtl.PSaleDtlID = 0;
                                IBBmsCenSaleDtl.Entity = bmsCenSaleDtl;
                                IBBmsCenSaleDtl.Edit();
                            }
                        }
                        else
                        {
                            if (bmsCenSaleDoc.BmsCenSaleDtlList[i].MixSerial == bmsCenSaleDoc.BmsCenSaleDtlList[i].Id.ToString())
                                bmsCenSaleDoc.BmsCenSaleDtlList[i].MixSerial = "";
                            IBBmsCenSaleDtl.Entity = bmsCenSaleDoc.BmsCenSaleDtlList[i];
                            IBBmsCenSaleDtl.Edit();
                        }
                    }
                    IBBSampleOperate.AddObjectOperate(bmsCenSaleDoc, "BmsCenSaleDoc", "BmsCenSaleDocSplitCancel", reason);
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：供货单拆分撤销失败！";
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：供货单未拆分，不能撤销！";
            }
            if (!string.IsNullOrEmpty(baseResultDataValue.ErrorInfo))
                ZhiFang.Common.Log.Log.Error(baseResultDataValue.ErrorInfo);
            return baseResultDataValue;

        }

        //2017-05-06 Add
        /// <summary>
        /// 供货单明细拆分
        /// </summary>
        /// <param name="docID">供货单ID</param>
        /// <param name="splitType">拆分类型(默认为0，为0时判断拆分标志，已拆分的不做拆分；否则，不判断拆分标志，强制拆分)</param>
        /// <param name="compatibleType">兼容类型，0为兼容旧流程，1为新流程（主要判断条码打印标志）</param>
        /// <returns></returns>
        public BaseResultDataValue AddSplitSaleDocByID(string docID, int splitType, int compatibleType)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string info = "BLL---" + this.GetType().Name + "---【AddSplitSaleDocByID】";
            BmsCenSaleDoc bmsCenSaleDoc = this.Get(long.Parse(docID));
            bool isSplit = true;
            if (splitType == 0)
                isSplit = bmsCenSaleDoc.IsSplit == 1 ? false : true;
            if (!isSplit)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：该供货单已经拆分！";
                ZhiFang.Common.Log.Log.Info(info + baseResultDataValue.ErrorInfo);
                return baseResultDataValue;
            }

            if (bmsCenSaleDoc != null && bmsCenSaleDoc.BmsCenSaleDtlList != null && bmsCenSaleDoc.BmsCenSaleDtlList.Count > 0)
            {
                if (isSplit)
                {
                    baseResultDataValue = AddSplitBmsCenSaleDtl(bmsCenSaleDoc, compatibleType);
                    if (baseResultDataValue.success)
                    {
                        bmsCenSaleDoc.IsSplit = 1;//1为已经拆分
                        this.Entity = bmsCenSaleDoc;
                        this.Edit();
                    }
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：无法根据ID【" + docID + "】获取供货单信息或供货单不存在产品信息！";
                ZhiFang.Common.Log.Log.Info(info + baseResultDataValue.ErrorInfo);
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 生成混合条码号
        /// </summary>
        /// <param name="bmsCenSaleDtl"></param>
        /// <returns></returns>
        private string _getMixSerial(BmsCenSaleDtl bmsCenSaleDtl)
        {
            string mixSerial = "";
            string prodGoodsNo = "";
            string lotNo = "";
            string invalidDate = "";
            string saleDtlId = "";
            string prodOrgNo = "";
            string compOrgNo = "";
            try
            {
                if (!string.IsNullOrEmpty(bmsCenSaleDtl.ProdGoodsNo))
                    prodGoodsNo = bmsCenSaleDtl.ProdGoodsNo;
                if (!string.IsNullOrEmpty(bmsCenSaleDtl.LotNo))
                    lotNo = bmsCenSaleDtl.LotNo;
                if (bmsCenSaleDtl.InvalidDate != null)
                    invalidDate = ((DateTime)bmsCenSaleDtl.InvalidDate).ToString("yyyy-MM-dd");
                //if (!string.IsNullOrEmpty(bmsCenSaleDtl.SaleDocNo))
                    saleDtlId = bmsCenSaleDtl.Id.ToString();
                if (bmsCenSaleDtl.Prod != null)
                    prodOrgNo = bmsCenSaleDtl.Prod.OrgNo.ToString();
                if (bmsCenSaleDtl.Goods != null && bmsCenSaleDtl.Goods.Comp != null)
                    compOrgNo = bmsCenSaleDtl.Goods.Comp.OrgNo.ToString();
                mixSerial = "ZFRP|1|" + prodOrgNo + "|" + prodGoodsNo + "|" +
                  lotNo + "|" + invalidDate + "|" + compOrgNo + "|" + saleDtlId;
            }
            catch (Exception ex)
            {
                string hint = "";
                if (string.IsNullOrEmpty(bmsCenSaleDtl.ProdGoodsNo))
                    hint = "供货明细单：厂商产品编号为空！";
                if (string.IsNullOrEmpty(bmsCenSaleDtl.LotNo))
                    hint = "供货明细单：产品批号为空！";
                if (bmsCenSaleDtl.InvalidDate == null)
                    hint = "供货明细单：产品有效期为空！";
                if (string.IsNullOrEmpty(bmsCenSaleDtl.SaleDocNo))
                    hint = "供货明细单：供货单号为空！";
                if (bmsCenSaleDtl.Prod == null)
                    hint = "供货明细单：厂商为空！";
                if (bmsCenSaleDtl.Goods == null)
                    hint = "供货明细单：产品为空！";
                else if (bmsCenSaleDtl.Goods.Comp == null)
                    hint = "供货明细单：产品的供应商为空！";
                throw new Exception(hint + " Error:" + ex.Message);
            }
            return mixSerial;
        }

        /// <summary>
        /// 供货单条码拆分
        /// </summary>
        /// <param name="bmsCenSaleDoc">供货单</param>
        /// <param name="barCodeCount">条码拆分总数</param>
        /// <param name="compatibleType">兼容类型，为兼容旧流程而加的参数</param>
        /// <returns></returns>
        public BaseResultDataValue AddSplitBmsCenSaleDtl(BmsCenSaleDoc bmsCenSaleDoc, int compatibleType)
        {
            //明细拆分说明：
            //混合条码规则：ZFRP|1|prodOrgNo|prodGoodsNo|lotNo|invalidDate|compOrgNo|saleDtlId|当前序号|明细数量
            //例如：明细中有一条A试剂，数量=10，那么当前序号就是1-10，明细数量就是10
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (bmsCenSaleDoc != null && bmsCenSaleDoc.BmsCenSaleDtlList != null && bmsCenSaleDoc.BmsCenSaleDtlList.Count > 0)
            {
                IList<BmsCenSaleDtl> listBmsCenSaleDtl = bmsCenSaleDoc.BmsCenSaleDtlList.OrderBy(p => p.DataAddTime).ThenBy(p => p.GoodsName).ToList();
                foreach (BmsCenSaleDtl bmsCenSaleDtl in listBmsCenSaleDtl)
                {
                    if (string.IsNullOrEmpty(bmsCenSaleDtl.LotNo) || bmsCenSaleDtl.InvalidDate == null || bmsCenSaleDtl.InvalidDate < DateTime.Parse("1900-01-02") || bmsCenSaleDtl.GoodsQty < 0)
                    {
                        baseResultDataValue.success = false;
                        throw new Exception("该供货单的明细中存在“产品批号、有效期、数量”为空的产品，请先维护供货单产品明细信息！");
                    }
                }
                int allCount = 0; //明细总序号
                foreach (BmsCenSaleDtl bmsCenSaleDtl in listBmsCenSaleDtl)
                {
                    //供货单明细中产品条码类型为【盒条码】是做拆分；否则，不拆分（BarCodeMgr为1表示盒条码）
                    if (bmsCenSaleDtl.Goods.BarCodeMgr == 1)
                    {
                        bmsCenSaleDtl.SaleDocNo = bmsCenSaleDoc.SaleDocNo;
                        bmsCenSaleDtl.SumTotal = bmsCenSaleDtl.Price;
                        //count拆分后子单序号,主要为生成条码所用。例如：某明细单需拆分为10个条码子单，序号为1-10
                        int count = 0;
                        int goodsQty = (int)bmsCenSaleDtl.GoodsQty; //产品数量
                        int splitAllCount = goodsQty;
                        string mixSerial = _getMixSerial(bmsCenSaleDtl);//生成混合条码
                        for (int i = 0; i < goodsQty; i++)
                        {
                            allCount++;
                            count = count + 1;
                            BmsCenSaleDtl newBmsCenSaleDtl = null;
                            if (i == 0)
                                newBmsCenSaleDtl = bmsCenSaleDtl;
                            else
                                newBmsCenSaleDtl = new BmsCenSaleDtl();
                            newBmsCenSaleDtl.SaleDtlNo = bmsCenSaleDtl.SaleDtlNo;
                            newBmsCenSaleDtl.SaleDocNo = bmsCenSaleDtl.SaleDocNo;
                            newBmsCenSaleDtl.ProdGoodsNo = bmsCenSaleDtl.ProdGoodsNo;
                            newBmsCenSaleDtl.ProdOrgName = bmsCenSaleDtl.ProdOrgName;
                            newBmsCenSaleDtl.GoodsName = bmsCenSaleDtl.GoodsName;
                            newBmsCenSaleDtl.GoodsUnit = bmsCenSaleDtl.GoodsUnit;
                            newBmsCenSaleDtl.UnitMemo = bmsCenSaleDtl.UnitMemo;
                            newBmsCenSaleDtl.ShortCode = bmsCenSaleDtl.ShortCode;
                            newBmsCenSaleDtl.Price = bmsCenSaleDtl.Price;
                            newBmsCenSaleDtl.SumTotal = bmsCenSaleDtl.SumTotal;
                            newBmsCenSaleDtl.TaxRate = bmsCenSaleDtl.TaxRate;
                            newBmsCenSaleDtl.LotNo = bmsCenSaleDtl.LotNo;
                            newBmsCenSaleDtl.ProdDate = bmsCenSaleDtl.ProdDate;
                            newBmsCenSaleDtl.InvalidDate = bmsCenSaleDtl.InvalidDate;
                            newBmsCenSaleDtl.BiddingNo = bmsCenSaleDtl.BiddingNo;
                            newBmsCenSaleDtl.IOFlag = bmsCenSaleDtl.IOFlag;
                            newBmsCenSaleDtl.PackSerial = bmsCenSaleDtl.PackSerial;
                            newBmsCenSaleDtl.LotSerial = bmsCenSaleDtl.LotSerial;
                            newBmsCenSaleDtl.ZX1 = bmsCenSaleDtl.ZX1;
                            newBmsCenSaleDtl.ZX2 = bmsCenSaleDtl.ZX2;
                            newBmsCenSaleDtl.ZX3 = bmsCenSaleDtl.ZX3;
                            newBmsCenSaleDtl.RegisterNo = bmsCenSaleDtl.RegisterNo;
                            newBmsCenSaleDtl.RegisterInvalidDate = bmsCenSaleDtl.RegisterInvalidDate;
                            newBmsCenSaleDtl.BarCodeMgr = bmsCenSaleDtl.BarCodeMgr;//条码类型
                            newBmsCenSaleDtl.TempRange = bmsCenSaleDtl.TempRange;//温度范围
                            newBmsCenSaleDtl.StorageType = bmsCenSaleDtl.StorageType;//储存条件
                            newBmsCenSaleDtl.ApproveDocNo = bmsCenSaleDtl.ApproveDocNo;//批准文号
                            newBmsCenSaleDtl.BmsCenSaleDoc = bmsCenSaleDtl.BmsCenSaleDoc;
                            newBmsCenSaleDtl.Prod = bmsCenSaleDtl.Prod;
                            newBmsCenSaleDtl.Goods = bmsCenSaleDtl.Goods;
                            //此行以上是同属性赋值
                            newBmsCenSaleDtl.GoodsQty = 1;
                            newBmsCenSaleDtl.DtlCount = goodsQty; //明细总数 
                            newBmsCenSaleDtl.PSaleDtlID = bmsCenSaleDtl.Id;
                            newBmsCenSaleDtl.GoodsSerial = bmsCenSaleDtl.Id.ToString();
                            newBmsCenSaleDtl.DispOrder = allCount;
                            if (compatibleType == 0)
                                newBmsCenSaleDtl.MixSerial = mixSerial + "|" + count.ToString() + '|' + splitAllCount;
                            else
                            {
                                if (bmsCenSaleDtl.Goods.IsPrintBarCode == 1) 
                                    newBmsCenSaleDtl.MixSerial = mixSerial + "|" + count.ToString() + '|' + splitAllCount;
                            }
                            IBBmsCenSaleDtl.Entity = newBmsCenSaleDtl;
                            if (i == 0)
                                IBBmsCenSaleDtl.Edit();
                            else
                                IBBmsCenSaleDtl.Add();
                        }
                    }
                    else //凡不是盒条码的产品，执行以下流程
                    {
                        bmsCenSaleDtl.SaleDocNo = bmsCenSaleDoc.SaleDocNo;
                        bmsCenSaleDtl.DtlCount = bmsCenSaleDtl.GoodsQty; //明细总数 
                        if (bmsCenSaleDtl.Goods.BarCodeMgr == 0 && bmsCenSaleDtl.Goods.IsPrintBarCode == 1 && string.IsNullOrEmpty(bmsCenSaleDtl.MixSerial))//BarCodeMgr=0为批条码
                            bmsCenSaleDtl.MixSerial = bmsCenSaleDtl.Id.ToString();//当产品条码类型为批条码、需打印条码、混合条码为空时，重新赋值
                        IBBmsCenSaleDtl.Entity = bmsCenSaleDtl;
                        IBBmsCenSaleDtl.Edit();
                    }
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误：供货单或供货单明细信息为空或不存在！";
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 供货单条码拆分
        /// </summary>
        /// <param name="bmsCenSaleDoc">供货单</param>
        /// <param name="listSaleDtlError">盒条码---有异常的供货子单（产品）列表</param>
        /// <param name="listSaleDtlNoBarCode">非盒条码或无条码---子单列表</param>
        /// <param name="compatibleType">兼容类型，为兼容旧流程而加的参数</param>
        /// <returns></returns>
        public BaseResultDataValue AddSplitBmsCenSaleDtl(BmsCenSaleDoc bmsCenSaleDoc, IList<BmsCenSaleDtl> listNoConfirm, IList<BmsCenSaleDtl> listBatchBarCodeConfirm, int compatibleType)
        {
            //明细拆分说明：
            //混合条码规则：ZFRP|1|prodOrgNo|prodGoodsNo|lotNo|invalidDate|compOrgNo|saleDtlId|当前序号|明细数量
            //例如：明细中有一条A试剂，数量=10，那么当前序号就是1-10，明细数量就是10
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (bmsCenSaleDoc != null && bmsCenSaleDoc.BmsCenSaleDtlList != null && bmsCenSaleDoc.BmsCenSaleDtlList.Count > 0)
            {
                IList<BmsCenSaleDtl> listBmsCenSaleDtl = bmsCenSaleDoc.BmsCenSaleDtlList.OrderBy(p => p.DataAddTime).ThenBy(p => p.GoodsName).ToList();
                int allCount = 0; //明细总序号
                foreach (BmsCenSaleDtl bmsCenSaleDtl in listBmsCenSaleDtl)
                {
                    if (string.IsNullOrEmpty(bmsCenSaleDtl.LotNo) || bmsCenSaleDtl.InvalidDate == null || bmsCenSaleDtl.InvalidDate < DateTime.Parse("1900-01-02") || bmsCenSaleDtl.GoodsQty < 0)
                    {
                        baseResultDataValue.success = false;
                        throw new Exception("该供货单的明细中存在“产品批号、有效期、数量”为空的产品，请先维护供货单明细信息！");
                    }
                }
                foreach (BmsCenSaleDtl bmsCenSaleDtl in listBmsCenSaleDtl)
                {
                    //供货单明细中产品条码类型为【盒条码】是做拆分；否则，不拆分（BarCodeMgr为1表示盒条码）
                    if (bmsCenSaleDtl.Goods.BarCodeMgr == 1)
                    {
                        string errorInfo = "";
                        double acceptCount = bmsCenSaleDtl.GoodsQty;//验收数量默认为产品数量
                        if (listNoConfirm != null && listNoConfirm.Count > 0)
                        {
                            IList<BmsCenSaleDtl> tempList = listNoConfirm.Where(p => p.Id == bmsCenSaleDtl.Id).ToList();
                            if (tempList != null && tempList.Count > 0)
                            {
                                errorInfo = tempList[0].AccepterErrorMsg;
                                acceptCount = tempList[0].AcceptCount;
                            }
                        }
                        bmsCenSaleDtl.SaleDocNo = bmsCenSaleDoc.SaleDocNo;
                        bmsCenSaleDtl.SumTotal = bmsCenSaleDtl.Price;
                        
                        //count拆分后子单序号,主要为生成条码所用。例如：某明细单需拆分为10个条码子单，序号为1-10
                        int count = 0;
                        int goodsQty = (int)bmsCenSaleDtl.GoodsQty;
                        string mixSerial = _getMixSerial(bmsCenSaleDtl);//生成混合条码
                        for (int i = 0; i < goodsQty; i++)
                        {
                            allCount++;
                            count = count + 1;
                            BmsCenSaleDtl newBmsCenSaleDtl = null;
                            if (i == 0)
                                newBmsCenSaleDtl = bmsCenSaleDtl;
                            else
                                newBmsCenSaleDtl = new BmsCenSaleDtl();
                            newBmsCenSaleDtl.SaleDtlNo = bmsCenSaleDtl.SaleDtlNo;
                            newBmsCenSaleDtl.SaleDocNo = bmsCenSaleDtl.SaleDocNo;
                            newBmsCenSaleDtl.ProdGoodsNo = bmsCenSaleDtl.ProdGoodsNo;
                            newBmsCenSaleDtl.ProdOrgName = bmsCenSaleDtl.ProdOrgName;
                            newBmsCenSaleDtl.GoodsName = bmsCenSaleDtl.GoodsName;
                            newBmsCenSaleDtl.GoodsUnit = bmsCenSaleDtl.GoodsUnit;
                            newBmsCenSaleDtl.UnitMemo = bmsCenSaleDtl.UnitMemo;
                            newBmsCenSaleDtl.ShortCode = bmsCenSaleDtl.ShortCode;
                            newBmsCenSaleDtl.Price = bmsCenSaleDtl.Price;
                            newBmsCenSaleDtl.SumTotal = bmsCenSaleDtl.SumTotal;
                            newBmsCenSaleDtl.TaxRate = bmsCenSaleDtl.TaxRate;
                            newBmsCenSaleDtl.LotNo = bmsCenSaleDtl.LotNo;
                            newBmsCenSaleDtl.ProdDate = bmsCenSaleDtl.ProdDate;
                            newBmsCenSaleDtl.InvalidDate = bmsCenSaleDtl.InvalidDate;
                            newBmsCenSaleDtl.BiddingNo = bmsCenSaleDtl.BiddingNo;
                            newBmsCenSaleDtl.IOFlag = bmsCenSaleDtl.IOFlag;
                            newBmsCenSaleDtl.PackSerial = bmsCenSaleDtl.PackSerial;
                            newBmsCenSaleDtl.LotSerial = bmsCenSaleDtl.LotSerial;
                            newBmsCenSaleDtl.ZX1 = bmsCenSaleDtl.ZX1;
                            newBmsCenSaleDtl.ZX2 = bmsCenSaleDtl.ZX2;
                            newBmsCenSaleDtl.ZX3 = bmsCenSaleDtl.ZX3;
                            newBmsCenSaleDtl.RegisterNo = bmsCenSaleDtl.RegisterNo;
                            newBmsCenSaleDtl.RegisterInvalidDate = bmsCenSaleDtl.RegisterInvalidDate;                            
                            newBmsCenSaleDtl.BarCodeMgr = bmsCenSaleDtl.BarCodeMgr;//条码类型
                            newBmsCenSaleDtl.TempRange = bmsCenSaleDtl.TempRange;//温度范围
                            newBmsCenSaleDtl.StorageType = bmsCenSaleDtl.StorageType;//储存条件
                            newBmsCenSaleDtl.ApproveDocNo = bmsCenSaleDtl.ApproveDocNo;//批准文号
                            newBmsCenSaleDtl.BmsCenSaleDoc = bmsCenSaleDtl.BmsCenSaleDoc;
                            newBmsCenSaleDtl.Prod = bmsCenSaleDtl.Prod;
                            newBmsCenSaleDtl.Goods = bmsCenSaleDtl.Goods;
                            //此行以上是同属性赋值
                            newBmsCenSaleDtl.GoodsQty = 1; //
                            newBmsCenSaleDtl.DtlCount = goodsQty; //明细总数
                            newBmsCenSaleDtl.PSaleDtlID = bmsCenSaleDtl.Id;
                            newBmsCenSaleDtl.GoodsSerial = bmsCenSaleDtl.Id.ToString();
                            newBmsCenSaleDtl.AcceptCount = newBmsCenSaleDtl.GoodsQty;//2017-08-01
                            newBmsCenSaleDtl.DispOrder = allCount;
                            if (compatibleType == 0)
                                newBmsCenSaleDtl.MixSerial = mixSerial + "|" + count.ToString() + "|" + acceptCount;
                            else
                            {
                                if (bmsCenSaleDtl.Goods.IsPrintBarCode == 1) //2017-07-21 add
                                    newBmsCenSaleDtl.MixSerial = mixSerial + "|" + count.ToString() + "|" + acceptCount;
                            }      
                            //验收异常信息
                            if (goodsQty > acceptCount && (!string.IsNullOrEmpty(errorInfo)))
                            {
                                if (i > acceptCount)
                                {
                                    newBmsCenSaleDtl.AcceptCount = 0;//2017-08-01
                                    newBmsCenSaleDtl.AccepterErrorMsg = errorInfo;
                                    newBmsCenSaleDtl.MixSerial = "";
                                }
                            }
                            IBBmsCenSaleDtl.Entity = newBmsCenSaleDtl;
                            if (i == 0)
                                IBBmsCenSaleDtl.Edit();
                            else
                                IBBmsCenSaleDtl.Add();
                        }
                    }
                    else //凡不是盒条码的产品，执行以下流程
                    {
                        if (listBatchBarCodeConfirm != null && listBatchBarCodeConfirm.Count > 0)
                        {
                            IList<BmsCenSaleDtl> tempList = listBatchBarCodeConfirm.Where(p => p.Id == bmsCenSaleDtl.Id).ToList();
                            if (tempList != null && tempList.Count > 0)
                            {
                                bmsCenSaleDtl.AcceptCount = tempList[0].AcceptCount;
                            }
                        }
                        bmsCenSaleDtl.SaleDocNo = bmsCenSaleDoc.SaleDocNo;
                        bmsCenSaleDtl.DtlCount = bmsCenSaleDtl.GoodsQty; 
                        if (bmsCenSaleDtl.Goods.BarCodeMgr == 0 && bmsCenSaleDtl.Goods.IsPrintBarCode == 1 && string.IsNullOrEmpty(bmsCenSaleDtl.MixSerial))//BarCodeMgr=0为批条码
                            bmsCenSaleDtl.MixSerial = bmsCenSaleDtl.Id.ToString();//当产品条码类型为批条码、需打印条码、混合条码为空时，重新赋值
                        IBBmsCenSaleDtl.Entity = bmsCenSaleDtl;
                        IBBmsCenSaleDtl.Edit();
                    }
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误：供货单或供货单明细信息为空或不存在！";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue JudgeIsSameOrg(int secAccepterType, string docID, string secAccepterAccount, string secAccepterPwd, RBACUser rbacUser)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            BmsCenSaleDoc bmsCenSaleDoc = this.Get(long.Parse(docID));
            int flagSameDocOrg = 0;
            if (bmsCenSaleDoc != null && bmsCenSaleDoc.Comp != null)
            {

                //select Dept where Comp.OrgNo+''=Dept.UseCode
                IList<HRDept> listHRDept = IBHRDept.SearchListByHQL(" hrdept.UseCode=\'" + bmsCenSaleDoc.Comp.OrgNo + "\'");
                if (listHRDept != null && listHRDept.Count == 1)
                {
                    if (listHRDept[0].Id == rbacUser.HREmployee.HRDept.Id)
                        flagSameDocOrg = 1;
                    else
                    {
                        flagSameDocOrg = -1;
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "错误信息：次验收人不属于供货单所属的供应商！";
                    }
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：未找到供货单所属供应商的机构信息！";
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：供货单或者供货单所属供应商不存在！";
            }
            if (secAccepterType == 2)
            {
                if (flagSameDocOrg == -1 || flagSameDocOrg == 0)
                    return baseResultDataValue;
            }
            else
            {
                bool isSameSessionOrg = (SessionHelper.GetSessionValue(DicCookieSession.HRDeptID) == rbacUser.HREmployee.HRDept.Id.ToString());
                if (secAccepterType == 3)
                {
                    if (!(flagSameDocOrg == 1 || isSameSessionOrg))
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "错误信息：次验收人必须属于供应商或本机构！";
                        return baseResultDataValue;
                    }
                }
                else if (!isSameSessionOrg)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：主验收人和次验收人不属于同一机构！";
                    return baseResultDataValue;
                }
            }
            string strPassWord = SecurityHelp.MD5Encrypt(secAccepterPwd, SecurityHelp.PWDMD5Key);
            bool tempBool = (rbacUser.Account == secAccepterAccount) && (rbacUser.PWD == strPassWord) && (!rbacUser.AccLock);
            if (!tempBool)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：次验收人登录密码错误！";
                return baseResultDataValue;
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 供货单审核验证
        /// </summary>
        /// <param name="saleDoc">供货单实体</param>
        /// <param name="validateType">验证类型</param>
        /// <returns></returns>
        public BaseResultDataValue ValidateCheckSaleDoc(BmsCenSaleDoc saleDoc, int validateType)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (string.IsNullOrWhiteSpace(saleDoc.SaleDocNo))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "供货单号不能为空，请补录供货单号！";
                return baseResultDataValue;
            }
            IList<BmsCenSaleDtl> listSaleDtl = saleDoc.BmsCenSaleDtlList;
            if (listSaleDtl != null && listSaleDtl.Count > 0)
            {
                if (validateType == 0 || validateType == 1)//旧验证模式和先拆分供货单，再做审核
                {
                    IList<BmsCenSaleDtl> list = listSaleDtl.Where(p => p.LotNo == null || p.LotNo.Trim() == "" ||
                        p.InvalidDate == null || p.InvalidDate < DateTime.Parse("1900-01-02") || p.GoodsQty <= 0 ||
                        (p.Goods.BarCodeMgr != 2 && (p.MixSerial == null || p.MixSerial.Trim() == ""))).ToList();
                    if (list != null && list.Count > 0)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "供货单中存在批号、日期、混合条码为空或数量为0的产品信息！";
                    }
                }
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 供货单验证
        /// </summary>
        /// <param name="docID">供货单</param>
        /// <param name="validateType">验证类型（此参数为兼容旧流程而设置）</param>
        /// <returns></returns>
        public BaseResultDataValue EditCheckSaleDocByID(string idList, int validateType)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (!string.IsNullOrEmpty(idList))
            {
                IList<string> listID = idList.Split(',').ToList();
                foreach (string docID in listID)
                {
                    BmsCenSaleDoc saleDoc = this.Get(long.Parse(docID));
                    baseResultDataValue = ValidateCheckSaleDoc(saleDoc, validateType);
                    if (baseResultDataValue.success)
                    {
                        saleDoc.Status = 2;//2为审核状态
                        if (!string.IsNullOrEmpty(SessionHelper.GetSessionValue(DicCookieSession.EmployeeID)))
                            saleDoc.CheckerID = long.Parse(SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                        saleDoc.Checker = SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                        saleDoc.CheckTime = DateTime.Now;
                        this.Entity = saleDoc;
                        baseResultDataValue.success = this.Edit();
                    }
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：供货单ID不能为空！";
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 供货单逻辑删除
        /// </summary>
        /// <param name="idList">供货单ID</param>
        /// <param name="deleteFlag">删除标志（0或null为正常单子,1为逻辑删除）</param>
        /// <returns></returns>
        public BaseResultDataValue EditSaleDocDeleteFlagByID(string idList, int deleteFlag)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (!string.IsNullOrEmpty(idList))
            {
                IList<string> list = idList.Split(',').ToList();
                foreach (string id in list)
                {
                    BmsCenSaleDoc saleDoc = this.Get(long.Parse(id));
                    saleDoc.DeleteFlag = deleteFlag;
                    this.Entity = saleDoc;
                    if (this.Edit())
                    {
                        if (deleteFlag == 1)
                            IBBSampleOperate.AddObjectOperate(saleDoc, "BmsCenSaleDoc", "LogicalDelete", "逻辑删除");
                    }
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "参数idList为空！";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue AddBmsCenSaleDocDataByMaiKe(string jsonData, string serverPath, CenOrg cenOrg)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string xmlSaleDoc = serverPath + "\\BaseTableXML\\SaleDocInterface\\MaiKe\\BmsCenSaleDoc.xml";
            string xmlSaleDtl = serverPath + "\\BaseTableXML\\SaleDocInterface\\MaiKe\\BmsCenSaleDtl.xml";
            string xmlGoods = serverPath + "\\BaseTableXML\\SaleDocInterface\\MaiKe\\Goods.xml";
            if (System.IO.File.Exists(xmlSaleDoc) && System.IO.File.Exists(xmlSaleDtl) && System.IO.File.Exists(xmlGoods))
            {
                Dictionary<string, string> dicSaleDoc = new Dictionary<string, string>();
                Dictionary<string, string> dicSaleDtl = new Dictionary<string, string>();
                Dictionary<string, string> dicGoods = new Dictionary<string, string>();
                ExcelDataCommon.GetColumnNameBySaleDocXMLFile(xmlSaleDoc, dicSaleDoc);
                ExcelDataCommon.GetColumnNameBySaleDocXMLFile(xmlSaleDtl, dicSaleDtl);
                ExcelDataCommon.GetColumnNameBySaleDocXMLFile(xmlGoods, dicGoods);
                if (dicSaleDoc.Count > 0 && dicSaleDtl.Count > 0 && dicGoods.Count > 0)
                {
                    JObject jsonObject = JObject.Parse(jsonData);
                    try
                    {
                        jsonObject = JObject.Parse(jsonObject["result"].ToString());
                        string isSucc = jsonObject["result"].ToString();
                        if (isSucc == "1")
                        {
                            JArray jsonArray = (JArray)jsonObject["data"];
                            if (jsonArray != null && jsonArray.Count > 0)
                            {
                                baseResultDataValue = JudgeGoodsIsExist(jsonArray, dicGoods, cenOrg);
                                if (baseResultDataValue.success)
                                    baseResultDataValue = AddBmsCenSaleDocDataByJArray(jsonArray, dicSaleDoc, dicSaleDtl, dicGoods, cenOrg);
                            }
                            else
                            {
                                baseResultDataValue.success = false;
                                baseResultDataValue.ErrorInfo = "调用第三方供货单接口获取不到对应的供货单！";
                            }
                        }
                        else
                        {
                            baseResultDataValue.success = false;
                            baseResultDataValue.ErrorInfo = "调用第三方供货单接口失败！接口返回信息：" + jsonData;
                            ZhiFang.Common.Log.Log.Error(baseResultDataValue.ErrorInfo);
                        }
                    }
                    catch (Exception ex)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "调用第三方供货单接口失败！" + "<br>" + "错误原因：" + ex.Message + "<br>" + "接口返回信息：" + jsonData;
                        ZhiFang.Common.Log.Log.Error(baseResultDataValue.ErrorInfo);
                        throw new Exception(baseResultDataValue.ErrorInfo);
                    }
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "无法获取字段对照信息或对照信息出错！";
                }
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue AddBmsCenSaleDocDataByJArray(JArray jsonArray, Dictionary<string, string> dicSaleDoc, Dictionary<string, string> dicSaleDtl, Dictionary<string, string> dicGoods, CenOrg cenOrg)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (jsonArray != null && jsonArray.Count > 0)
            {
                string saleDocKey = "SaleDocNo";
                Dictionary<string, BmsCenSaleDoc> dicSaleDocNo = new Dictionary<string, BmsCenSaleDoc>();
                Dictionary<string, CenOrg> dicLabNo = new Dictionary<string, CenOrg>();
                string goodsQtyMemo = "";
                foreach (JObject jo in jsonArray)
                {
                    string saleDocNo = jo[dicSaleDoc[saleDocKey]].ToString();
                    CenOrg lab = null;
                    string labNo = jo["labno"].ToString();
                    if (string.IsNullOrEmpty(labNo))
                    {
                        throw new Exception("供货单信息中的实验室编码LabNo为空，请联系管理员维护！");
                    }
                    if (dicLabNo.ContainsKey(labNo))
                        lab = dicLabNo[labNo];
                    else
                    {
                        IList<CenOrg> listCenOrg = IBCenOrg.SearchListByHQL(" cenorg.OrgNo=\'" + labNo + "\'");
                        if (listCenOrg != null && listCenOrg.Count > 0)
                        {
                            if (listCenOrg.Count == 1)
                            {
                                lab = listCenOrg[0];
                                dicLabNo.Add(labNo, lab);
                                if (!IBCenOrgCondition.ValidateUpperAndLowerLevel(cenOrg.Id, lab.Id))
                                    throw new Exception("【" + cenOrg.CName + "】不是实验室【" + lab.CName + "】（机构编号：【" + labNo + "】）的供应商，请联系系统管理员！");
                            }
                            else
                                throw new Exception("根据实验室编码【" + labNo + "】找到多个对应的机构信息，请联系管理员解决！");
                        }
                        else
                            throw new Exception("根据实验室编码【" + labNo + "】找不到对应的机构信息，请联系管理员维护！");
                    }
                    BmsCenSaleDoc saleDoc = null;
                    if (dicSaleDocNo.ContainsKey(saleDocNo))
                        saleDoc = dicSaleDocNo[saleDocNo];
                    else
                    {
                        saleDoc = new BmsCenSaleDoc();
                        foreach (KeyValuePair<string, string> kv in dicSaleDoc)
                        {
                            System.Reflection.PropertyInfo propertyInfo = saleDoc.GetType().GetProperty(kv.Key);
                            if (propertyInfo != null && jo[kv.Value] != null)
                                propertyInfo.SetValue(saleDoc, ExcelDataCommon.DataConvert(propertyInfo, jo[kv.Value]), null);
                        }
                        saleDoc.Status = 0;
                        saleDoc.Source = 1;//平台(供应商)
                        saleDoc.Memo = "供货单接口导入";
                        saleDoc.Comp = cenOrg;
                        saleDoc.CompanyName = cenOrg.CName;
                        saleDoc.Lab = lab;
                        saleDoc.LabName = lab.CName;
                        saleDoc.OperDate = DateTime.Now;
                        saleDoc.DataAddTime = DateTime.Now;
                        saleDoc.UserName = SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                        if (!string.IsNullOrEmpty(SessionHelper.GetSessionValue(DicCookieSession.EmployeeID)))
                            saleDoc.UserID = long.Parse(SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                        this.Entity = saleDoc;
                        this.Add();
                        dicSaleDocNo.Add(saleDocNo, saleDoc);
                    }
                    BmsCenSaleDtl saleDtl = new BmsCenSaleDtl();
                    string dtlMemo = "";
                    foreach (KeyValuePair<string, string> kv in dicSaleDtl)
                    {
                        System.Reflection.PropertyInfo propertyInfo = saleDtl.GetType().GetProperty(kv.Key);
                        if (propertyInfo != null && jo[kv.Value] != null)
                        {
                            if (propertyInfo.Name != "GoodsQty")
                                propertyInfo.SetValue(saleDtl, ExcelDataCommon.DataConvert(propertyInfo, jo[kv.Value]), null);
                            else
                            {
                                if (jo[kv.Value] != null)
                                {
                                    double goodsQty = double.Parse(jo[kv.Value].ToString());
                                    saleDtl.GoodsQty = (int)Math.Floor(goodsQty);
                                    if (goodsQty - saleDtl.GoodsQty > 0)
                                    {
                                        dtlMemo = "【GoodsName】原始数量为：" + goodsQty.ToString() + "，存储值：" + saleDtl.GoodsQty.ToString();
                                    }
                                }
                            }
                        }
                    }//foreach
                    saleDtl.BmsCenSaleDoc = saleDoc;
                    saleDtl.SaleDocNo = saleDoc.SaleDocNo;
                    saleDtl.Goods = AddGoodsByGoodNo(cenOrg, lab, jo, dicGoods);
                    if (saleDtl.Goods != null)
                    {
                        saleDtl.GoodsNo = saleDtl.Goods.GoodsNo;
                        saleDtl.ShortCode = saleDtl.Goods.ShortCode;
                        saleDtl.Price = saleDtl.Goods.Price;
                        saleDtl.SumTotal = saleDtl.Goods.Price * saleDtl.GoodsQty;
                        saleDtl.RegisterNo = saleDtl.Goods.RegistNo;
                        saleDtl.RegisterInvalidDate = saleDtl.Goods.RegistNoInvalidDate;
                        saleDtl.GoodsName = saleDtl.Goods.CName;
                        saleDtl.GoodsUnit = saleDtl.Goods.UnitName;
                        saleDtl.UnitMemo = saleDtl.Goods.UnitMemo;
                        saleDtl.BiddingNo = saleDtl.Goods.BiddingNo;
                        saleDtl.ApproveDocNo = saleDtl.Goods.ApproveDocNo;
                        saleDtl.StorageType = saleDtl.Goods.StorageType;
                        saleDtl.BarCodeMgr = saleDtl.Goods.BarCodeMgr;
                    }
                    if (dtlMemo != "")
                    {
                        saleDtl.Memo = dtlMemo.Replace("GoodsName", saleDtl.GoodsName);
                        if (saleDtl.Goods != null)
                            saleDtl.Memo = "货品编号:" + saleDtl.Goods.GoodsNo + "，" + "货品编号(厂):" + saleDtl.Goods.ProdGoodsNo + "，" + saleDtl.Memo;
                        if (goodsQtyMemo == "")
                            goodsQtyMemo = saleDtl.Memo;
                        else
                            goodsQtyMemo = "<br>" + saleDtl.Memo;
                    }
                    saleDtl.DataAddTime = DateTime.Now;
                    if (string.IsNullOrWhiteSpace(saleDtl.LotNo))
                        saleDtl.LotNo = "-";
                    if (saleDtl.InvalidDate == null)
                        saleDtl.InvalidDate = DateTime.ParseExact("20500101", "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                    IBBmsCenSaleDtl.Entity = saleDtl;
                    IBBmsCenSaleDtl.Add();

                    saleDoc.TotalPrice += saleDtl.SumTotal;
                    this.Entity = saleDoc;
                    this.Edit();
                }//foreach

                if (goodsQtyMemo != "")
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "下列试剂产品的数量出现小数位，请手工处理：<br>" + goodsQtyMemo;
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "获取的供货单信息无法转换数组或数组为空！";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue AddBmsCenSaleDocDataByJArrayBaron(string saleDocNo, JArray jsonArray, CenOrg compCenOrg, CenOrg labCenOrg, string otherOrderDocNo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (jsonArray != null && jsonArray.Count > 0)
            {
                try
                {
                    double totalPrice = 0;
                    BmsCenSaleDoc saleDoc = new BmsCenSaleDoc();
                    saleDoc.SaleDocNo = saleDocNo;
                    saleDoc.BmsCenOrderDoc = IBBmsCenOrderDoc.GetOrderDocByOtherOrderDocNo(otherOrderDocNo);
                    if (saleDoc.BmsCenOrderDoc != null)
                        saleDoc.OrderDocNo = saleDoc.BmsCenOrderDoc.OrderDocNo;
                    saleDoc.OtherOrderDocNo = otherOrderDocNo;
                    saleDoc.Status = 2;
                    saleDoc.Source = 1;//平台(供应商)
                    saleDoc.IsSplit = 1;
                    saleDoc.Memo = "供货单接口导入";
                    saleDoc.Comp = compCenOrg;
                    saleDoc.CompanyName = compCenOrg.CName;
                    saleDoc.Lab = labCenOrg;
                    saleDoc.LabName = labCenOrg.CName;
                    saleDoc.OperDate = DateTime.Now;
                    saleDoc.DataAddTime = DateTime.Now;
                    saleDoc.UserName = SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                    if (!string.IsNullOrEmpty(SessionHelper.GetSessionValue(DicCookieSession.EmployeeID)))
                        saleDoc.UserID = long.Parse(SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                    IList<string> listNoExistGoods = new List<string>();
                    Dictionary<string, long> listGoodsNoID = new Dictionary<string, long>();
                    IList<BmsCenSaleDtl> listBmsCenSaleDtl = new List<BmsCenSaleDtl>(); 
                    foreach (string jo in jsonArray)
                    {
                        if (!string.IsNullOrEmpty(jo))
                        {
                            IList<string> listStr = jo.Split('|').ToList();
                            if (listStr.Count >= 3)
                            {
                                string goodsNo = "";
                                string goodsLotNo = listStr[1];
                                if (string.IsNullOrWhiteSpace(goodsLotNo))
                                    goodsLotNo = "-";
                                string invalidDate = listStr[2];
                                if (string.IsNullOrWhiteSpace(invalidDate))
                                    invalidDate = "20500101";
                                if (listStr[0].IndexOf("https://m.roche-diag.cn/barcode?num=074") >= 0)
                                {
                                    if (listStr.Count >= 5)
                                        goodsNo = listStr[4];
                                    else
                                    {
                                        goodsNo = listStr[0];
                                        int index = ("https://m.roche-diag.cn/barcode?num=074").Length;
                                        goodsNo = goodsNo.Substring(index, goodsNo.Length - index);
                                        goodsNo = goodsNo.Substring(0, 11);
                                    }
                                }
                                else if (listStr[0].IndexOf("http://xz.rf-bj.com:801/barcode?num") >= 0)
                                {
                                    if (listStr.Count >= 6)
                                        goodsNo = listStr[5];
                                    else
                                    {
                                        goodsNo = listStr[0];
                                        int index = ("http://xz.rf-bj.com:801/barcode?num=").Length;
                                        goodsNo = goodsNo.Substring(index, goodsNo.Length - index);
                                        //goodsNo = goodsNo.Substring(0, goodsNo.Length);
                                    }
                                }
                                else
                                {
                                    baseResultDataValue.success = false;
                                    baseResultDataValue.ErrorInfo = "不支持该类型的条码格式："+ jo.ToString();
                                    ZhiFang.Common.Log.Log.Info(baseResultDataValue.ErrorInfo);
                                    return baseResultDataValue;
                                }

                                string hqlWhere = " goods.CenOrg.Id=" + labCenOrg.Id.ToString() +
                                    " and goods.Comp.Id=" + compCenOrg.Id.ToString() +
                                    " and goods.GoodsNo=\'" + goodsNo + "\'"+
                                    " and goods.Visible=1";
                                IList<Goods> listGood = IBGoods.SearchListByHQL(hqlWhere);
                                if (listGood != null && listGood.Count == 1)
                                {
                                    BmsCenSaleDtl saleDtl = new BmsCenSaleDtl();
                                    saleDtl.BmsCenSaleDoc = saleDoc;
                                    saleDtl.SaleDocNo = saleDoc.SaleDocNo;
                                    saleDtl.Goods = listGood[0];
                                    saleDtl.LotNo = goodsLotNo;
                                    saleDtl.InvalidDate = DateTime.ParseExact(invalidDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                                    saleDtl.GoodsName = listGood[0].CName;
                                    saleDtl.GoodsUnit = listGood[0].UnitName;
                                    saleDtl.UnitMemo = listGood[0].UnitMemo;
                                    saleDtl.MixSerial = jo;
                                    saleDtl.GoodsSerial = jo;
                                    saleDtl.BarCodeMgr = listGood[0].BarCodeMgr;
                                    saleDtl.GoodsQty = 1;
                                    saleDtl.Price = listGood[0].Price;
                                    saleDtl.SumTotal = saleDtl.GoodsQty * saleDtl.Price;
                                    saleDtl.DataAddTime = DateTime.Now;
                                    saleDtl.Prod = listGood[0].Prod;
                                    saleDtl.ProdOrgName = listGood[0].ProdOrgName;
                                    saleDtl.ProdGoodsNo = listGood[0].ProdGoodsNo;
                                    saleDtl.StorageType = listGood[0].StorageType;
                                    saleDtl.BiddingNo = listGood[0].BiddingNo;
                                    saleDtl.RegisterNo = listGood[0].RegistNo;
                                    saleDtl.RegisterInvalidDate = listGood[0].RegistNoInvalidDate;
                                    //IBBmsCenSaleDtl.Entity = saleDtl;
                                    //IBBmsCenSaleDtl.Add();
                                    totalPrice += saleDtl.SumTotal;
                                    listBmsCenSaleDtl.Add(saleDtl);
                                    if (!listGoodsNoID.ContainsKey(goodsNo))
                                    {
                                        listGoodsNoID.Add(goodsNo, saleDtl.Id);
                                        saleDtl.PSaleDtlID = saleDtl.Id;
                                    }
                                    else
                                        saleDtl.PSaleDtlID = listGoodsNoID[goodsNo];

                                }
                                else if (listGood == null || listGood.Count == 0)
                                {
                                    if (!listNoExistGoods.Contains(goodsNo))
                                    {
                                        listNoExistGoods.Add(goodsNo);
                                        baseResultDataValue.success = false;
                                        if (baseResultDataValue.ErrorInfo == "")
                                            baseResultDataValue.ErrorInfo = "产品编号为【" + goodsNo + "】的试剂不存在，请维护相关试剂信息！";
                                        else
                                            baseResultDataValue.ErrorInfo += "," + "<br>" + "产品编号为【" + goodsNo + "】的试剂不存在，请维护相关试剂信息！";
                                        ZhiFang.Common.Log.Log.Info("产品编号为：" + listStr[0] + "的试剂不存在或存在多条记录！");
                                        //return baseResultDataValue;
                                    }                                                             
                                }
                                else if (listGood.Count > 1)
                                {
                                    if (!listNoExistGoods.Contains(goodsNo))
                                    {
                                        listNoExistGoods.Add(goodsNo);
                                        baseResultDataValue.success = false;
                                        if (baseResultDataValue.ErrorInfo == "")
                                            baseResultDataValue.ErrorInfo = "产品编号为【" + goodsNo + "】的试剂存在多条记录，请确认相关试剂信息！";
                                        else
                                            baseResultDataValue.ErrorInfo += "," + "<br>" + "产品编号为【" + goodsNo + "】的试剂存在多条记录，请确认相关试剂信息！";
                                        ZhiFang.Common.Log.Log.Info("产品编号为：" + listStr[0] + "的试剂不存在或存在多条记录！");
                                        //return baseResultDataValue;
                                    }
                                }
                            }
                        }
                    }//foreach
                    if ((listNoExistGoods == null ||listNoExistGoods.Count == 0) && listBmsCenSaleDtl.Count > 0)
                    {
                        foreach (BmsCenSaleDtl saleDtl in listBmsCenSaleDtl)
                        {
                            IBBmsCenSaleDtl.Entity = saleDtl;
                            IBBmsCenSaleDtl.Add();
                        }
                        saleDoc.TotalPrice = totalPrice;
                        this.Entity = saleDoc;
                        this.Add();
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "通过接口获取的供货单数据信息错误：" + ex.Message;
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "获取的供货单信息无法转换数组或数组为空！";
                ZhiFang.Common.Log.Log.Info(baseResultDataValue.ErrorInfo);
            }
            return baseResultDataValue;
        }

        private BaseResultDataValue JudgeGoodsIsExist(JArray jsonArray, Dictionary<string, string> dicGoods, CenOrg cenOrg)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (jsonArray != null && jsonArray.Count > 0)
            {
                Dictionary<string, BmsCenSaleDoc> dicSaleDocNo = new Dictionary<string, BmsCenSaleDoc>();
                Dictionary<string, CenOrg> dicLabNo = new Dictionary<string, CenOrg>();
                try
                {
                    string strGoods = "";
                    foreach (JObject jo in jsonArray)
                    {
                        CenOrg lab = null;
                        string labNo = jo["labno"].ToString();
                        if (string.IsNullOrEmpty(labNo))
                        {
                            throw new Exception("接口供货单信息中的实验室编码LabNo为空，请联系管理员维护！");
                        }
                        if (dicLabNo.ContainsKey(labNo))
                            lab = dicLabNo[labNo];
                        else
                        {
                            IList<CenOrg> listCenOrg = IBCenOrg.SearchListByHQL(" cenorg.OrgNo=\'" + labNo + "\'");
                            if (listCenOrg != null && listCenOrg.Count > 0)
                            {
                                if (listCenOrg.Count == 1)
                                {
                                    lab = listCenOrg[0];
                                    dicLabNo.Add(labNo, lab);
                                    if (!IBCenOrgCondition.ValidateUpperAndLowerLevel(cenOrg.Id, lab.Id))
                                        throw new Exception("【" + cenOrg.CName + "】不是实验室【" + lab.CName + "】（机构编号：【" + labNo + "】）的供应商，请联系系统管理员！");
                                }
                                else
                                    throw new Exception("根据实验室编码【" + labNo + "】找到多个对应的机构信息，请联系管理员解决！");
                            }
                            else
                                throw new Exception("根据实验室编码【" + labNo + "】找不到对应的机构信息，请联系管理员维护！");
                        }
                        string goodsNo = jo[dicGoods["GoodsNo"]].ToString();
                        string goodsName = jo[dicGoods["CName"]].ToString();
                        string prodGoodsNo = jo[dicGoods["ProdGoodsNo"]].ToString();
                        IList<Goods> listGood = IBGoods.SearchListByHQL(" goods.CenOrg.Id=" + lab.Id.ToString() +
                            " and goods.Comp.Id=" + cenOrg.Id.ToString() + " and goods.GoodsNo=\'" + goodsNo + "\'");
                        if (listGood == null || listGood.Count == 0)
                        {
                            if (string.IsNullOrEmpty(strGoods))
                                strGoods = "货品编号:" + goodsNo + "，" + "货品编号(厂):" + prodGoodsNo + "，" + goodsName;
                            else
                                strGoods += "," + "<br>" + "货品编号:" + goodsNo + "，" + "货品编号(厂):" + prodGoodsNo + "，" + goodsName;
                        }
                    }//foreach
                    if (!string.IsNullOrEmpty(strGoods))
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "下列货品信息不存在，请先维护对应的货品信息：" + "<br>" + strGoods;
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误：" + ex.Message;
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "获取的供货单信息无法转换数组或数组为空！";
            }
            return baseResultDataValue;
        }

        private Goods AddGoodsByGoodNo(CenOrg cenOrg, CenOrg comp, JObject jo, Dictionary<string, string> dicGoods)
        {
            Goods goods = null;
            string goodsNo = jo[dicGoods["GoodsNo"]].ToString();
            IList<Goods> listGood = IBGoods.SearchListByHQL(" goods.CenOrg.Id=" + comp.Id.ToString() +
                " and goods.Comp.Id=" + cenOrg.Id.ToString() + " and goods.GoodsNo=\'" + goodsNo + "\'");
            if (listGood != null && listGood.Count > 0)
            {
                goods = listGood[0];
            }
            else
            {
                goods = new Goods();
                goods.CenOrg = comp;
                goods.Comp = cenOrg;
                goods.GoodsNo = goodsNo;
                goods.CompConfirm = 1;
                goods.CenOrgConfirm = 1;
                foreach (KeyValuePair<string, string> kv in dicGoods)
                {
                    System.Reflection.PropertyInfo propertyInfo = goods.GetType().GetProperty(kv.Key);
                    if (propertyInfo != null && jo[kv.Value] != null)
                        propertyInfo.SetValue(goods, ExcelDataCommon.DataConvert(propertyInfo, jo[kv.Value]), null);
                }//foreach
                goods.ZX3 = "通过接口自动导入";
                goods.DataUpdateTime = DateTime.Now;
                goods.PinYinZiTou = ZhiFang.Common.Public.StringPlus.Chinese2Spell.GetPinYinZiTou(goods.CName);
                goods.Visible = 1;
                IBGoods.Entity = goods;
                IBGoods.Add();
            }
            return goods;
        }

        public BaseResultDataValue EditBmsCenSaleDocTotalPrice(long docID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            BmsCenSaleDoc saleDoc = this.Get(docID);
            if (saleDoc != null && saleDoc.BmsCenSaleDtlList != null && saleDoc.BmsCenSaleDtlList.Count > 0)
            {
                double totalPrice = 0;
                foreach (BmsCenSaleDtl saleDtl in saleDoc.BmsCenSaleDtlList)
                {
                    totalPrice = totalPrice + saleDtl.Price * saleDtl.GoodsQty;
                    //totalPrice = totalPrice + saleDtl.SumTotal;
                }
                saleDoc.TotalPrice = totalPrice;
                this.Entity = saleDoc;
                baseResultDataValue.success = this.Edit();
            }
            return baseResultDataValue;
        }

        public bool UpdateBatchIsAccountInput(string saleDocIDStr, int isAccountInput)
        {
            bool result = true;
            if (String.IsNullOrEmpty(saleDocIDStr))
            {
                return result;
            }
            string hql = "update BmsCenSaleDoc bmscensaledoc set bmscensaledoc.IsAccountInput=" + (isAccountInput == 1 ? 1 : 0) + " where bmscensaledoc.Id in (" + saleDocIDStr + ")";
            try
            {
                int counts = ((IDBmsCenSaleDocDao)base.DBDao).UpdateByHql(hql);
                if (counts > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
                throw ex;
            }
            return result;
        }

        public BaseResultDataValue AddBmsCenSaleDocDataByBaron(string saleDocNo, string jsonData, CenOrg compCenOrg, CenOrg labCenOrg, string otherOrderDocNo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            JObject jsonObject = JObject.Parse(jsonData);
            try
            {
                string isSucc = jsonObject["success"].ToString();
                if (isSucc.ToLower() == "true")
                {
                    JArray jsonArray = (JArray)jsonObject["data"];
                    if (jsonArray != null && jsonArray.Count > 0)
                    {
                        baseResultDataValue = AddBmsCenSaleDocDataByJArrayBaron(saleDocNo, jsonArray, compCenOrg, labCenOrg, otherOrderDocNo);
                    }
                    else
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "通过接口找不到单号为【"+saleDocNo+"】的供货单！";
                        ZhiFang.Common.Log.Log.Info(baseResultDataValue.ErrorInfo);
                    }
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "供货单接口查询失败！接口数据：" + jsonData;
                    ZhiFang.Common.Log.Log.Error("供货单接口查询失败！接口数据：" + jsonData);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "JSON错误：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public EntityList<BmsCenSaleDoc> StatBmsCenSaleDocTotalPrice(string listID)
        {
            EntityList<BmsCenSaleDoc> entityList = new EntityList<BmsCenSaleDoc>();
            IList<BmsCenSaleDoc> listSaleDoc = this.SearchListByHQL(" bmscensaledoc.Id in (" + listID + ")");
            if (listSaleDoc != null && listSaleDoc.Count > 0)
            {
                IList<BmsCenSaleDtl> listSaleDtl = IBBmsCenSaleDtl.SearchListByHQL(" bmscensaledtl.BmsCenSaleDoc.Id in (" + listID + ")");
                if (listSaleDtl != null && listSaleDtl.Count > 0)
                {
                    var lisItem = from bmsCenSaleDtl in listSaleDtl
                                  group bmsCenSaleDtl by
                                      new
                                      {
                                          Id = bmsCenSaleDtl.BmsCenSaleDoc.Id
                                      } into g
                                  select new
                                  {
                                      SaleDocId = g.Key.Id,
                                      TotalPrice = g.Sum(c => c.GoodsQty * c.Price)
                                  };
                    foreach (var item in lisItem)
                    {
                        IList<BmsCenSaleDoc> tempList = listSaleDoc.Where(p => p.Id == item.SaleDocId).ToList();
                        if (tempList != null && tempList.Count > 0)
                            tempList[0].TotalPrice = item.TotalPrice;
                    }
                }
                entityList.count = listSaleDoc.Count;
                entityList.list = listSaleDoc;
            }
            return entityList;
        }

        public EntityList<BmsCenSaleDtl> StatBmsCenSaleDtlGoodsQty(string beginDate, string endDate, string listStatus, string compID, string labID, string goodID, string goodLotNo, string groupbyType, string sort)
        {
            EntityList<BmsCenSaleDtl> entityList = new EntityList<BmsCenSaleDtl>();
            IList<BmsCenSaleDtl> listSaleDtl = new List<BmsCenSaleDtl>();
            string hqlWhere = " bmscensaledtl.BmsCenSaleDoc.OperDate>=\'" + beginDate + "\'" +
                 " and bmscensaledtl.BmsCenSaleDoc.OperDate<\'" + endDate + "\'";
            hqlWhere += " and (bmscensaledtl.BmsCenSaleDoc.DeleteFlag=0 or bmscensaledtl.BmsCenSaleDoc.DeleteFlag is null)";
            if (!string.IsNullOrEmpty(compID))
                hqlWhere += " and bmscensaledtl.BmsCenSaleDoc.Comp.Id=" + compID.ToString();
            if (!string.IsNullOrEmpty(labID))
                hqlWhere += " and bmscensaledtl.BmsCenSaleDoc.Lab.Id=" + labID.ToString();
            if (!string.IsNullOrEmpty(goodID))
                hqlWhere += " and bmscensaledtl.Goods.Id=" + goodID.ToString();
            if (!string.IsNullOrEmpty(goodLotNo))
                hqlWhere += " and bmscensaledtl.LotNo=\'" + goodLotNo + "\'";
            if (!string.IsNullOrEmpty(listStatus))
                hqlWhere += " and bmscensaledtl.BmsCenSaleDoc.Status in (" + listStatus + ")";
            ZhiFang.Common.Log.Log.Info("Stat HQL Start!");
            EntityList<BmsCenSaleDtl> entityListBmsCenSaleDtl = IBBmsCenSaleDtl.SearchListByHQL(hqlWhere, sort, 0, 0);
            IList<BmsCenSaleDtl> listBmsCenSaleDtl = entityListBmsCenSaleDtl.list;
            ZhiFang.Common.Log.Log.Info("Stat HQL End!");
            ZhiFang.Common.Log.Log.Info("Stat 合并开始!");
            if (listBmsCenSaleDtl != null && listBmsCenSaleDtl.Count > 0)
            {
                if (groupbyType == "1")//按GoodId分组
                {
                    var lisItem = from bmsCenSaleDtl in listBmsCenSaleDtl
                                  group bmsCenSaleDtl by
                                      new
                                      {
                                          GoodId = bmsCenSaleDtl.Goods.Id,
                                      } into g
                                  select new
                                  {
                                      GoodId = g.Key.GoodId,
                                      TotalCount = g.Sum(c => c.GoodsQty),
                                      TotalPrice = g.Sum(c => c.Price * c.GoodsQty)
                                  };
                    foreach (var item in lisItem)
                    {
                        IList<BmsCenSaleDtl> tempList = listBmsCenSaleDtl.Where(p => p.Goods.Id == item.GoodId).ToList();
                        tempList[0].GoodsQty = item.TotalCount;
                        tempList[0].SumTotal = item.TotalPrice;
                        listSaleDtl.Add(tempList[0]);
                    }
                }
                else//按SaleDocId、GoodId、GoodLotNo分组
                {
                    var lisItem = from bmsCenSaleDtl in listBmsCenSaleDtl
                                  group bmsCenSaleDtl by
                                      new
                                      {
                                          SaleDocId = bmsCenSaleDtl.BmsCenSaleDoc.Id,
                                          GoodId = bmsCenSaleDtl.Goods.Id,
                                          GoodLotNo = bmsCenSaleDtl.LotNo
                                      } into g
                                  select new
                                  {
                                      SaleDocId = g.Key.SaleDocId,
                                      GoodId = g.Key.GoodId,
                                      GoodLotNo = g.Key.GoodLotNo,
                                      TotalCount = g.Sum(c => c.GoodsQty),
                                      TotalPrice = g.Sum(c => c.Price * c.GoodsQty)
                                  };
                    foreach (var item in lisItem)
                    {
                        IList<BmsCenSaleDtl> tempList = listBmsCenSaleDtl.Where(p => p.BmsCenSaleDoc.Id == item.SaleDocId && p.Goods.Id == item.GoodId && p.LotNo == item.GoodLotNo).ToList();
                        tempList[0].GoodsQty = item.TotalCount;
                        tempList[0].SumTotal = item.TotalPrice;
                        listSaleDtl.Add(tempList[0]);
                    }
                }
            }
            ZhiFang.Common.Log.Log.Info("Stat 合并结束!");
            entityList.list = listSaleDtl;
            entityList.count = listSaleDtl.Count;
            return entityList;
        }

        public EntityList<BmsCenSaleDtl> StatBmsCenSaleDtlGoodsQty(string beginDate, string endDate, string listStatus, string compID, string labID, string goodID, string goodLotNo, string groupbyType, int page, int limit, string sort)
        {
            int allCount = 0;
            EntityList<BmsCenSaleDtl> entityList = new EntityList<BmsCenSaleDtl>();
            entityList = StatBmsCenSaleDtlGoodsQty(beginDate, endDate, listStatus, compID, labID, goodID, goodLotNo, groupbyType, sort);
            if (entityList != null && entityList.count > 0 && entityList.list != null)
            {
                ZhiFang.Common.Log.Log.Info("Stat 分页开始!");
                allCount = entityList.count;
                if (page > 0 && limit > 0)
                {
                    entityList.list = entityList.list.Skip((page-1) * limit).Take(limit).ToList();
                }
            
            }
            ZhiFang.Common.Log.Log.Info("Stat 分页结束!");
            entityList.count = allCount;
            return entityList;
        }

        private void EidtSaleDocGoodsInfo(BmsCenSaleDoc saleDoc)
        {
            if (saleDoc != null && saleDoc.Comp != null)
            {
                if (string.IsNullOrWhiteSpace(saleDoc.CompanyName))
                    saleDoc.CompanyName = saleDoc.Comp.CName;

                if (string.IsNullOrWhiteSpace(saleDoc.CompAddress))
                    saleDoc.CompAddress = saleDoc.Comp.Address;

                if (string.IsNullOrWhiteSpace(saleDoc.CompContact))
                    saleDoc.CompContact = saleDoc.Comp.Contact;

                if (string.IsNullOrWhiteSpace(saleDoc.CompTel))
                    saleDoc.CompTel = saleDoc.Comp.Tel;

                if (string.IsNullOrWhiteSpace(saleDoc.CompHotTel))
                    saleDoc.CompHotTel = saleDoc.Comp.HotTel;

                if (string.IsNullOrWhiteSpace(saleDoc.CompFox))
                    saleDoc.CompFox = saleDoc.Comp.Fox;

                if (string.IsNullOrWhiteSpace(saleDoc.CompEmail))
                    saleDoc.CompEmail = saleDoc.Comp.Email;

                if (string.IsNullOrWhiteSpace(saleDoc.CompWebAddress))
                    saleDoc.CompWebAddress = saleDoc.Comp.WebAddress;

            }

            if (saleDoc != null && saleDoc.Lab != null)
            {
                if (string.IsNullOrWhiteSpace(saleDoc.LabName))
                    saleDoc.LabName = saleDoc.Lab.CName;

                if (string.IsNullOrWhiteSpace(saleDoc.LabAddress))
                    saleDoc.LabAddress = saleDoc.Lab.Address;

                if (string.IsNullOrWhiteSpace(saleDoc.LabContact))
                    saleDoc.LabContact = saleDoc.Lab.Contact;

                if (string.IsNullOrWhiteSpace(saleDoc.LabTel))
                    saleDoc.LabTel = saleDoc.Lab.Tel;

                if (string.IsNullOrWhiteSpace(saleDoc.LabHotTel))
                    saleDoc.LabHotTel = saleDoc.Lab.HotTel;

                if (string.IsNullOrWhiteSpace(saleDoc.LabFox))
                    saleDoc.LabFox = saleDoc.Lab.Fox;

                if (string.IsNullOrWhiteSpace(saleDoc.LabEmail))
                    saleDoc.LabEmail = saleDoc.Lab.Email;

                if (string.IsNullOrWhiteSpace(saleDoc.LabWebAddress))
                    saleDoc.LabWebAddress = saleDoc.Lab.WebAddress;
            }
        }

        private void EidtSaleDtlGoodsInfo(BmsCenSaleDtl saleDtl)
        {
            if (saleDtl != null && saleDtl.Goods != null)
            {
                if (string.IsNullOrWhiteSpace(saleDtl.GoodsName))//产品名称
                    saleDtl.GoodsName = saleDtl.Goods.CName;

                if (string.IsNullOrWhiteSpace(saleDtl.GoodsNo))//产品编码
                    saleDtl.GoodsNo = saleDtl.Goods.GoodsNo;

                if (string.IsNullOrWhiteSpace(saleDtl.GoodsUnit))//包装单位
                    saleDtl.GoodsUnit = saleDtl.Goods.UnitName;

                if (string.IsNullOrWhiteSpace(saleDtl.UnitMemo))//包装单位描述
                    saleDtl.UnitMemo = saleDtl.Goods.UnitMemo;

                if (string.IsNullOrWhiteSpace(saleDtl.StorageType))//储藏条件
                    saleDtl.StorageType = saleDtl.Goods.StorageType;

                if (string.IsNullOrWhiteSpace(saleDtl.RegisterNo))//注册证编号
                    saleDtl.RegisterNo = saleDtl.Goods.RegistNo;

                if (string.IsNullOrWhiteSpace(saleDtl.BiddingNo))//招标号
                    saleDtl.BiddingNo = saleDtl.Goods.BiddingNo;

                if (string.IsNullOrWhiteSpace(saleDtl.ApproveDocNo))//批准文号
                    saleDtl.ApproveDocNo = saleDtl.Goods.ApproveDocNo;

                if (string.IsNullOrWhiteSpace(saleDtl.ProdGoodsNo))//厂商产品编号
                    saleDtl.ProdGoodsNo = saleDtl.Goods.ProdGoodsNo;

                if (string.IsNullOrWhiteSpace(saleDtl.ProdOrgName))//厂商产品名称
                    saleDtl.ProdOrgName = saleDtl.Goods.ProdOrgName;

                if (saleDtl.Prod == null)//厂商产品名称
                    saleDtl.Prod = saleDtl.Goods.Prod;

                if (saleDtl.Goods.Prod != null)
                {
                    if (string.IsNullOrWhiteSpace(saleDtl.ProdGoodsNo))//厂商产品编号
                        saleDtl.ProdGoodsNo = saleDtl.Goods.Prod.OrgNo.ToString();

                    if (string.IsNullOrWhiteSpace(saleDtl.ProdOrgName))//厂商产品名称
                        saleDtl.ProdOrgName = saleDtl.Goods.Prod.CName;
                }
            }
        }

        public BaseResultDataValue EidtSaleDocAcceptFlag(long saleDocID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            BmsCenSaleDoc saleDoc = this.Get(saleDocID);
            if (saleDoc != null && saleDoc.BmsCenSaleDtlList != null)
            {
                IList<BmsCenSaleDtl> listBmsCenSaleDtl = saleDoc.BmsCenSaleDtlList.Where(p => p.AcceptFlag != 1).ToList();
                if (listBmsCenSaleDtl == null || listBmsCenSaleDtl.Count == 0)
                {
                    saleDoc.Status = 1;
                    this.Entity = saleDoc;
                    this.Edit();
                }
            }
            return baseResultDataValue;
        }

        public DataSet GetBmsCenSaleDtlInfoByID(string idList, string where, string sort, string xmlPath)
        {
            EntityList<BmsCenSaleDtl> entityList = null;
            if (string.IsNullOrEmpty(idList) && string.IsNullOrEmpty(where))
                return null;
            else
            {
                if (!string.IsNullOrEmpty(idList))
                    entityList = IBBmsCenSaleDtl.SearchListByHQL(" bmscensaledtl.BmsCenSaleDoc.Id in (" + idList + ")", sort, 0, 0);
                else if (!string.IsNullOrEmpty(where))
                    entityList = IBBmsCenSaleDtl.SearchListByHQL(where, sort, 0, 0);
                if (entityList != null && entityList.count > 0)
                    return CommonRS.GetListObjectToDataSet<BmsCenSaleDtl>(entityList.list, xmlPath);
                else
                    return null;
            }
        }

        public BaseResultDataValue AddBmsCenSaleDocCopyBySaleDocID(long saleDocID, IList<BmsCenSaleDtl> listSaleDtl)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BmsCenSaleDoc saleDoc = this.Get(saleDocID);
            if (saleDoc == null && saleDoc.BmsCenSaleDtlList !=null && saleDoc.BmsCenSaleDtlList.Count>0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "无法获取供货单信息";
                return brdv;
            }
            if (saleDoc.Status != 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "只有临时状态的供货单才能进行此操作";
                return brdv;
            }
            if (saleDoc.IsSplit != 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "只有未拆分的供货单才能进行此操作";
                return brdv;
            }
            BmsCenSaleDoc NewSaleDoc = CommonRS.EntityCopy<BmsCenSaleDoc>(saleDoc, new string[] { "Id", "DataTimeStamp", "BmsCenSaleDtlList" });
            IList<BmsCenSaleDtl> listBmsCenSaleDtl = new List<BmsCenSaleDtl>();
            double sumPrice = 0;
            double newSumPrice = 0;
            foreach (BmsCenSaleDtl saleDtl in saleDoc.BmsCenSaleDtlList)
            {
                if (listSaleDtl != null)
                {
                    IList<BmsCenSaleDtl> tempList = listSaleDtl.Where(p => p.Id == saleDtl.Id).ToList();
                    if (tempList != null && tempList.Count > 0)
                    {
                        BmsCenSaleDtl newSaleDtl = CommonRS.EntityCopy<BmsCenSaleDtl>(saleDtl, new string[] { "Id", "DataTimeStamp" });
                        newSaleDtl.BmsCenSaleDoc = NewSaleDoc;
                        newSaleDtl.SaleDocNo += "-拆";
                        newSaleDtl.DataAddTime = DateTime.Now;
                        newSaleDtl.DataUpdateTime = null;
                        newSaleDtl.GoodsQty = tempList[0].GoodsQty;
                        newSaleDtl.SumTotal = newSaleDtl.GoodsQty * newSaleDtl.Price;
                        newSumPrice += newSaleDtl.SumTotal;
                        listBmsCenSaleDtl.Add(newSaleDtl);
                        saleDtl.GoodsQty = saleDtl.GoodsQty - tempList[0].GoodsQty;
                        saleDtl.SumTotal = saleDtl.GoodsQty * saleDtl.Price;
                    }
                }
                sumPrice += saleDtl.SumTotal;
            }
            NewSaleDoc.BmsCenSaleDtlList = listBmsCenSaleDtl;
            NewSaleDoc.TotalPrice = newSumPrice;
            NewSaleDoc.SaleDocNo += "-拆";
            NewSaleDoc.OperDate = DateTime.Now;
            NewSaleDoc.UserName = SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
            if (!string.IsNullOrEmpty(SessionHelper.GetSessionValue(DicCookieSession.EmployeeID)))
                NewSaleDoc.UserID = long.Parse(SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
            NewSaleDoc.DataAddTime = DateTime.Now;
            NewSaleDoc.DataUpdateTime = null;
            this.Entity = NewSaleDoc;
            if (this.Add())
            {
                saleDoc.TotalPrice = sumPrice;
                this.Entity = saleDoc;  
                if (!this.Edit())
                    throw new Exception("新增供货单信息失败！");
            }
            else
            {
                brdv.success = false;
                brdv.ErrorInfo = "新增供货单信息失败！";
            }
            return brdv;
        }

        public BaseResultData EditExtractFlagSaleDocByIDAndDtlIDList(string saleDocID, string saleDtlIDList)
        {
            BaseResultData baseResultData = new BaseResultData();
            BmsCenSaleDoc saleDoc = this.Get(long.Parse(saleDocID));
            if (saleDoc == null)
            {
                baseResultData.success = false;
                baseResultData.message = "根据平台供货单ID获取不到供货单信息，请确认ID数据是否正确！";
                ZhiFang.Common.Log.Log.Info(baseResultData.message);
                return baseResultData;
            }
            saleDoc.IOFlag = 1;
            this.Entity = saleDoc;
            baseResultData.success = this.Edit();
            if (baseResultData.success && (!string.IsNullOrEmpty(saleDtlIDList)))
            {
                IList<string> listID = saleDtlIDList.Split(',').ToList();
                foreach (string id in listID)
                {
                    BmsCenSaleDtl saleDtl = IBBmsCenSaleDtl.Get(long.Parse(id));
                    if (saleDtl != null)
                    {
                        saleDtl.IOFlag = 1;
                        IBBmsCenSaleDtl.Entity = saleDtl;
                        IBBmsCenSaleDtl.Edit();
                    }
                    else
                        throw new Exception("根据平台供货单明细ID获取不到供货单明细信息，请确认ID【"+ id + "】是否正确！");
                }
            }
            return baseResultData;
        }

    }
}