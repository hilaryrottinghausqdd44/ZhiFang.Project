using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Newtonsoft.Json.Linq;
using Spring.Context;
using Spring.Context.Support;
using ZhiFang.Common.Public;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.ServiceCommon.RBAC;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.IBLL.RBAC;
using System.Xml;
using System.Reflection;
using ZhiFang.Common.Log;

namespace ZhiFang.ReagentSys.Client
{
    public class InterfaceDataConvert
    {
        public void SaleDocDataConvert<T, T1>(ReaBmsCenSaleDoc saleDoc, IList<ReaBmsCenSaleDtl> saleDtlList, long storageID, ref T doc, ref IList<T1> dtlList)
        {
            ZhiFang.Common.Log.Log.Info("开始供货单转入库单！");
            //ZhiFang.Common.Log.Log.Debug("saleDoc:" + JObject.FromObject(saleDoc).ToString());
            //ZhiFang.Common.Log.Log.Debug("saleDtlList:" + JArray.FromObject(saleDtlList).ToString());
            //ZhiFang.Common.Log.Log.Debug("storageID:" + storageID);
            ZhiFang.Common.Log.Log.Debug("提取信息.SaleDoc供货商名称:" + saleDoc.CompanyName + ",供货商ID:" + saleDoc.CompID);
            ZhiFang.Common.Log.Log.Debug("提取信息.SaleDoc本地供货商名称:" + saleDoc.ReaCompanyName + ",本地供货商ID:" + saleDoc.ReaCompID);
            IApplicationContext context = ContextRegistry.GetContext();
            IBReaCenOrg IBReaCenOrg = (IBReaCenOrg)context.GetObject("BReaCenOrg");
            IBReaGoods IBReaGoods = (IBReaGoods)context.GetObject("BReaGoods");
            IBReaGoodsOrgLink IBReaGoodsOrgLink = (IBReaGoodsOrgLink)context.GetObject("BReaGoodsOrgLink");

            //入库明细需要用到机构货品的强关联
            IList<ReaGoods> reaGoodsList = new List<ReaGoods>();
            //入库明细需要用到供货商信息
            IList<ReaCenOrg> reaCenOrgList = new List<ReaCenOrg>();
            for (int i = 0; i < saleDtlList.Count; i++)
            {
                #region 供货明细非空验证
                string prodOrgNo = saleDtlList[i].ProdOrgNo;
                if (string.IsNullOrEmpty(prodOrgNo))
                {
                    ZhiFang.Common.Log.Log.Info("提取信息.供货明细单号:" + saleDtlList[i].SaleDtlNo + ",供应商编码(ProdOrgNo)为空!");
                    //continue;
                }
                string reaGoodsNo = saleDtlList[i].ReaGoodsNo;
                string cenOrgGoodsNo = saleDtlList[i].CenOrgGoodsNo;
                if (cenOrgGoodsNo == null || cenOrgGoodsNo.Trim() == "")
                    cenOrgGoodsNo = reaGoodsNo;
                string goodsUnit = saleDtlList[i].GoodsUnit;
                if (string.IsNullOrEmpty(reaGoodsNo))
                {
                    ZhiFang.Common.Log.Log.Info("提取信息.供货明细单号:" + saleDtlList[i].SaleDtlNo + ",货品编码(ReaGoodsNo)为空!");
                    continue;
                }
                if (string.IsNullOrEmpty(cenOrgGoodsNo))
                {
                    ZhiFang.Common.Log.Log.Info("提取信息.供货明细单号:" + saleDtlList[i].SaleDtlNo + ",供应商货品编码(CenOrgGoodsNo)为空!");
                    continue;
                }
                if (string.IsNullOrEmpty(goodsUnit))
                {
                    ZhiFang.Common.Log.Log.Info("提取信息.供货明细单号:" + saleDtlList[i].SaleDtlNo + ",包装单位(GoodsUnit)为空!");
                    continue;
                }
                #endregion
                 
                #region 供货明细的供应商信息处理
                ReaCenOrg reaCenOrg = null;
                if (saleDtlList[i].ReaCompID == null || saleDtlList[i].ReaCompID <= 0)
                {
                    string hql1 = string.Format(" reacenorg.Visible=1 and reacenorg.OrgType={0} and reacenorg.MatchCode='{1}'", ReaCenOrgType.供货方.Key, prodOrgNo);
                    ZhiFang.Common.Log.Log.Info("根据prodOrgNo【" + prodOrgNo + "】获取供应商信息!");
                    //供应商编码
                    IList<ReaCenOrg> tempReaCenOrgList = IBReaCenOrg.SearchListByHQL(hql1);
                    if (tempReaCenOrgList.Count > 0)
                    {
                        reaCenOrg = tempReaCenOrgList[0];
                    }
                }
                else if (saleDtlList[i].ReaCompID != null )
                {
                    ZhiFang.Common.Log.Log.Info("根据ReaCompID【" + saleDtlList[i].ReaCompID + "】获取供应商信息!");
                    reaCenOrg = IBReaCenOrg.Get(saleDtlList[i].ReaCompID.Value);
                }

                if (reaCenOrg != null)
                {
                    saleDtlList[i].ReaCompID = reaCenOrg.Id;
                    saleDtlList[i].ReaCompanyName = reaCenOrg.CName;
                    saleDtlList[i].ReaServerCompCode = reaCenOrg.PlatformOrgNo.ToString();
                    if (!reaCenOrgList.Contains(reaCenOrg))
                        reaCenOrgList.Add(reaCenOrg);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("提取信息.供货明细单号:" + saleDtlList[i].SaleDtlNo + ",找不到对应的供应商信息!");
                    continue;
                }
                #endregion

                #region 供货明细的货品信息处理
                ReaGoods reaGoods = null;
                if (!saleDtlList[i].ReaGoodsID.HasValue || saleDtlList[i].ReaGoodsID <= 0)
                {
                    string hql1 = string.Format(" reagoods.Visible=1 and reagoods.ReaGoodsNo='{0}' and reagoods.MatchCode='{1}' and reagoods.UnitName='{2}'", reaGoodsNo, cenOrgGoodsNo, goodsUnit);
                    IList<ReaGoods> tempReaGoodsList = IBReaGoods.SearchListByHQL(hql1);
                    if (tempReaGoodsList.Count > 0)
                    {
                        reaGoods = tempReaGoodsList[0];
                    }
                }
                else if (saleDtlList[i].ReaGoodsID.HasValue)
                {
                    reaGoods = IBReaGoods.Get(saleDtlList[i].ReaGoodsID.Value);
                }
                if (reaGoods != null)
                {
                    saleDtlList[i].GoodsID = reaGoods.Id;
                    saleDtlList[i].ReaGoodsID = reaGoods.Id;
                    saleDtlList[i].ReaGoodsName = reaGoods.CName;
                    saleDtlList[i].GoodsSort = reaGoods.GoodsSort;
                    saleDtlList[i].GoodsName = reaGoods.CName;
                    saleDtlList[i].GoodsNo = reaGoods.GoodsNo;
                    saleDtlList[i].BarCodeType = reaGoods.BarCodeMgr;
                    saleDtlList[i].IsPrintBarCode = reaGoods.IsPrintBarCode;

                    if (string.IsNullOrEmpty(saleDtlList[i].ReaGoodsNo))
                        saleDtlList[i].ReaGoodsNo = reaGoods.ReaGoodsNo;
                    if (string.IsNullOrEmpty(saleDtlList[i].ProdGoodsNo))
                        saleDtlList[i].ProdGoodsNo = reaGoods.ProdGoodsNo;
                    if (string.IsNullOrEmpty(saleDtlList[i].GoodsUnit))
                        saleDtlList[i].GoodsUnit = reaGoods.UnitName;
                    if (string.IsNullOrEmpty(saleDtlList[i].UnitMemo))
                        saleDtlList[i].UnitMemo = reaGoods.UnitMemo;
                    if (string.IsNullOrEmpty(saleDtlList[i].StorageType))
                        saleDtlList[i].StorageType = reaGoods.StorageType;
                    if (string.IsNullOrEmpty(saleDtlList[i].RegisterNo))
                        saleDtlList[i].RegisterNo = reaGoods.RegistNo;
                    if (!saleDtlList[i].RegisterInvalidDate.HasValue)
                        saleDtlList[i].RegisterInvalidDate = reaGoods.RegistNoInvalidDate;
                    if (!reaGoodsList.Contains(reaGoods))
                        reaGoodsList.Add(reaGoods);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("提取信息.供货明细单号:" + saleDtlList[i].SaleDtlNo + ",机构货品信息为空!");
                    continue;
                }
                #endregion

                #region 供货商货品关系处理
                ReaGoodsOrgLink reaGoodsOrgLink = null;
                if (!saleDtlList[i].CompGoodsLinkID.HasValue || saleDtlList[i].CompGoodsLinkID <= 0)
                {
                    string hql1 = string.Format(" reagoodsorglink.Visible=1 and reagoodsorglink.ReaGoods.Id={0} and reagoodsorglink.CenOrg.Id={1}", reaGoods.Id, reaCenOrg.Id);
                    IList<ReaGoodsOrgLink> tempReaGoodsOrgLinkList = IBReaGoodsOrgLink.SearchListByHQL(hql1);
                    if (tempReaGoodsOrgLinkList.Count > 0)
                    {
                        reaGoodsOrgLink = tempReaGoodsOrgLinkList[0];
                    }
                }
                else if (saleDtlList[i].CompGoodsLinkID.HasValue)
                {
                    reaGoodsOrgLink = IBReaGoodsOrgLink.Get(saleDtlList[i].CompGoodsLinkID.Value);
                }

                if (reaGoodsOrgLink != null)
                {
                    saleDtlList[i].CompGoodsLinkID = reaGoodsOrgLink.Id;
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("提取信息.供货明细单号:" + saleDtlList[i].SaleDtlNo + ",获取供货商货品关系信息为空!");
                }
                #endregion
            }
            if (doc.GetType().Name == "ReaBmsCenSaleDocConfirm")
            {
                ReaBmsCenSaleDocConfirm docConfirm = (ReaBmsCenSaleDocConfirm)(object)doc;
                IList<ReaBmsCenSaleDtlConfirm> dtlConfirmList = (IList<ReaBmsCenSaleDtlConfirm>)(object)dtlList;
                SaleDataConvertConfirm(saleDoc, saleDtlList, ref docConfirm, ref dtlConfirmList);
            }
            else if (doc.GetType().Name == "ReaBmsInDoc")
            {
                ReaBmsInDoc inDoc = (ReaBmsInDoc)(object)doc;
                IList<ReaBmsInDtl> inDtlList = (IList<ReaBmsInDtl>)(object)dtlList;
                SaleDataConvertIn(saleDoc, saleDtlList, reaCenOrgList, reaGoodsList, storageID, ref inDoc, ref inDtlList);
            }
            ZhiFang.Common.Log.Log.Info("结束供货单转入库单！");
        }
        /// <summary>
        /// 将提取的供货信息转换为验收信息
        /// </summary>
        /// <param name="saleDoc"></param>
        /// <param name="saleDtlList"></param>
        /// <param name="docConfirm"></param>
        /// <param name="dtlConfirmList"></param>
        public void SaleDataConvertConfirm(ReaBmsCenSaleDoc saleDoc, IList<ReaBmsCenSaleDtl> saleDtlList, ref ReaBmsCenSaleDocConfirm docConfirm, ref IList<ReaBmsCenSaleDtlConfirm> dtlConfirmList)
        {
            CopySaleDocToDocConfirm(saleDoc, ref docConfirm);
            foreach (ReaBmsCenSaleDtl saleDtl in saleDtlList)
            {
                ZhiFang.Common.Log.Log.Debug("提取信息.货品名称:" + saleDtl.ReaGoodsName + ",货品ID:" + saleDtl.ReaGoodsID);
                //获取供货明细的供货商关系信息
                if (!saleDtl.CompGoodsLinkID.HasValue || saleDtl.CompGoodsLinkID < 0)
                {

                }
                ReaBmsCenSaleDtlConfirm dtlConfirm = new ReaBmsCenSaleDtlConfirm();
                CopySaleDtlToDtlConfirm(saleDtl, ref dtlConfirm);
                dtlConfirmList.Add(dtlConfirm);
            }
            docConfirm.TotalPrice = dtlConfirmList.Sum(p => p.SumTotal);
        }
        private void CopySaleDocToDocConfirm(ReaBmsCenSaleDoc saleDoc, ref ReaBmsCenSaleDocConfirm docConfirm)
        {
            string employeeID = Cookie.CookieHelper.Read(DicCookieSession.EmployeeID);
            string empName = Cookie.CookieHelper.Read(DicCookieSession.EmployeeName);
            docConfirm.SaleDocConfirmNo = this.GetDocNo();
            docConfirm.CompID = saleDoc.ReaCompID;
            docConfirm.CompanyName = saleDoc.CompanyName;
            docConfirm.ZX1 = saleDoc.ZX1;
            docConfirm.ZX2 = saleDoc.ZX2;
            docConfirm.ZX3 = saleDoc.ZX3;
            docConfirm.Memo = saleDoc.Memo;
            docConfirm.InvoiceNo = saleDoc.InvoiceNo;
            docConfirm.OtherDocNo = saleDoc.OtherDocNo;
            docConfirm.TotalPrice = saleDoc.TotalPrice;
            docConfirm.DeleteFlag = 0;
            docConfirm.ReaCompID = saleDoc.ReaCompID;
            docConfirm.ReaCompanyName = saleDoc.ReaCompanyName;
            docConfirm.ReaServerCompCode = saleDoc.ReaServerCompCode;
            docConfirm.ReaCompCode = saleDoc.ReaCompCode;
            docConfirm.ReaServerLabcCode = saleDoc.ReaServerLabcCode;
            docConfirm.AcceptTime = DateTime.Now;
            docConfirm.DataUpdateTime = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(employeeID))
            {
                docConfirm.AccepterID = long.Parse(employeeID);
            }
            docConfirm.AccepterName = empName;
        }
        private void CopySaleDtlToDtlConfirm(ReaBmsCenSaleDtl saleDtl, ref ReaBmsCenSaleDtlConfirm dtlConfirm)
        {
            string employeeID = Cookie.CookieHelper.Read(DicCookieSession.EmployeeID);
            string empName = Cookie.CookieHelper.Read(DicCookieSession.EmployeeName);
            dtlConfirm.SaleDtlConfirmNo = this.GetDocNo();
            dtlConfirm.ProdGoodsNo = saleDtl.ProdGoodsNo;
            dtlConfirm.ProdID = saleDtl.ProdID;
            dtlConfirm.ProdOrgName = saleDtl.ProdOrgName;
            dtlConfirm.GoodsName = saleDtl.GoodsName;
            dtlConfirm.GoodsID = saleDtl.GoodsID;
            dtlConfirm.GoodsUnit = saleDtl.GoodsUnit;
            dtlConfirm.UnitMemo = saleDtl.UnitMemo;
            dtlConfirm.StorageType = saleDtl.StorageType;
            dtlConfirm.TempRange = saleDtl.TempRange;
            if (saleDtl.GoodsQty.HasValue)
                dtlConfirm.GoodsQty = saleDtl.GoodsQty.Value;
            dtlConfirm.Price = saleDtl.Price;
            dtlConfirm.SumTotal = dtlConfirm.GoodsQty * dtlConfirm.Price;// saleDtl.SumTotal;
            dtlConfirm.TaxRate = saleDtl.TaxRate;
            dtlConfirm.LotNo = saleDtl.LotNo;
            dtlConfirm.ProdDate = saleDtl.ProdDate;
            dtlConfirm.InvalidDate = saleDtl.InvalidDate;
            dtlConfirm.BiddingNo = saleDtl.BiddingNo;
            dtlConfirm.ApproveDocNo = saleDtl.ApproveDocNo;
            dtlConfirm.RegisterInvalidDate = saleDtl.RegisterInvalidDate;
            dtlConfirm.LotSerial = saleDtl.LotSerial;
            dtlConfirm.SysLotSerial = saleDtl.SysLotSerial;
            dtlConfirm.LotQRCode = saleDtl.LotQRCode;
            dtlConfirm.GoodsSerial = saleDtl.GoodsSerial;
            dtlConfirm.RegisterNo = saleDtl.RegisterNo;
            dtlConfirm.AcceptCount = 0;
            dtlConfirm.RefuseCount = 0;
            dtlConfirm.ReaGoodsID = saleDtl.ReaGoodsID;
            dtlConfirm.ReaGoodsName = saleDtl.ReaGoodsName;
            dtlConfirm.GoodsNo = saleDtl.GoodsNo;
            dtlConfirm.LabcGoodsLinkID = saleDtl.LabcGoodsLinkID;
            dtlConfirm.CompGoodsLinkID = saleDtl.CompGoodsLinkID;
            dtlConfirm.BarCodeMgr = int.Parse(saleDtl.BarCodeType.ToString());
            dtlConfirm.ReaCompID = saleDtl.ReaCompID;
            dtlConfirm.ReaCompanyName = saleDtl.ReaCompanyName;

            dtlConfirm.ProdGoodsNo = saleDtl.ProdGoodsNo;
            dtlConfirm.ReaGoodsNo = saleDtl.ReaGoodsNo;
            dtlConfirm.CenOrgGoodsNo = saleDtl.CenOrgGoodsNo;
            dtlConfirm.GoodsSort = saleDtl.GoodsSort;
            dtlConfirm.BarCodeType = saleDtl.BarCodeType;
            dtlConfirm.ReaServerCompCode = saleDtl.ReaServerCompCode;
            dtlConfirm.DataUpdateTime = DateTime.Now;
            dtlConfirm.OtherDtlNo = saleDtl.OtherDtlNo;
            //提取第三方数据的供货明细盒条码信息(使用分号隔开)
            dtlConfirm.OtherSerialNoStr = saleDtl.OtherSerialNoStr;
            if (dtlConfirm.BarCodeType == int.Parse(ReaGoodsBarCodeType.批条码.Key))
            {
                dtlConfirm.LotSerial = saleDtl.OtherSerialNoStr;
                //dtlConfirm.SysLotSerial = saleDtl.OtherSerialNoStr;
                dtlConfirm.LotQRCode = saleDtl.OtherSerialNoStr;
            }
        }
        /// <summary>
        /// 将提取的供货信息转换为入库信息
        /// </summary>
        /// <param name="saleDoc"></param>
        /// <param name="saleDtlList"></param>
        /// <param name="inDoc"></param>
        /// <param name="inDtlList"></param>
        public void SaleDataConvertIn(ReaBmsCenSaleDoc saleDoc, IList<ReaBmsCenSaleDtl> saleDtlList, IList<ReaCenOrg> reaCenOrgList, IList<ReaGoods> reaGoodsList, long storageID, ref ReaBmsInDoc inDoc, ref IList<ReaBmsInDtl> inDtlList)
        {
            CopySaleDocToInDoc(saleDoc, ref inDoc);
            IApplicationContext context = ContextRegistry.GetContext();
            IBReaGoods IBReaGoods = (IBReaGoods)context.GetObject("BReaGoods");

            IBReaStorage IBReaStorage = (IBReaStorage)context.GetObject("BReaStorage");
            ReaStorage reaStorage = IBReaStorage.Get(storageID);
            if (reaStorage == null)
            {
                //默认入库到第一个总库
                ZhiFang.Common.Log.Log.Debug("匹配第三方传入的库房信息(storageID)为:" + storageID + ",系统库房信息不存在,系统默认入库到试剂系统的第一个总库房:");
                IList<ReaStorage> reaStorageList = IBReaStorage.LoadAll();
                if (reaStorageList != null && reaStorageList.Count > 0)
                {
                    var tempList = reaStorageList.Where(p => p.Visible == true && p.IsMainStorage == true).OrderBy(p => p.DispOrder);
                    if (tempList != null && tempList.Count() > 0)
                    {
                        reaStorage = tempList.ElementAt(0);
                    }
                    else
                    {
                        reaStorage = reaStorageList.OrderBy(p => p.DispOrder).ElementAt(0);
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug("系统库房信息未设置总库房信息,提取的入库库房未能与试剂系统对照成功!");
                }
            }
            foreach (ReaBmsCenSaleDtl saleDtl in saleDtlList)
            {
                ZhiFang.Common.Log.Log.Debug("提取信息.货品名称:" + saleDtl.ReaGoodsName + ",货品ID:" + saleDtl.ReaGoodsID);
                ReaBmsInDtl inDtl = new ReaBmsInDtl();
                inDtl.InDocID = inDoc.Id;
                inDtl.InDocNo = inDoc.InDocNo;
                if (saleDtl.ReaCompID.HasValue)
                {
                    var tempList1 = reaCenOrgList.Where(p => p.Id == saleDtl.ReaCompID.Value);
                    if (tempList1 != null && tempList1.Count() > 0)
                    {
                        inDtl.ReaCompCode = tempList1.ElementAt(0).OrgNo.ToString();
                    }
                }
                if (saleDtl.ReaGoodsID.HasValue)
                {
                    var tempList2 = reaGoodsList.Where(p => p.Id == saleDtl.ReaGoodsID.Value);
                    if (tempList2 != null && tempList2.Count() > 0)
                    {
                        inDtl.ReaGoods = tempList2.ElementAt(0);
                    }
                }
                CopySaleDtlToInDtl(IBReaGoods, saleDtl, reaStorage, inDoc, ref inDtl);
                inDtlList.Add(inDtl);
            }
            inDoc.TotalPrice = inDtlList.Sum(p => p.SumTotal);
        }
        private void CopySaleDocToInDoc(ReaBmsCenSaleDoc saleDoc, ref ReaBmsInDoc inDoc)
        {
            string employeeID = Cookie.CookieHelper.Read(DicCookieSession.EmployeeID);
            string empName = Cookie.CookieHelper.Read(DicCookieSession.EmployeeName);
            inDoc.InDocNo = this.GetDocNo();
            inDoc.LabID = saleDoc.LabID;
            inDoc.InvoiceNo = saleDoc.InvoiceNo;
            inDoc.OtherDocNo = saleDoc.OtherDocNo;
            inDoc.TotalPrice = saleDoc.TotalPrice;
            inDoc.ZX1 = saleDoc.ZX1;
            inDoc.ZX2 = saleDoc.ZX2;
            inDoc.ZX3 = saleDoc.ZX3;

            inDoc.Memo = saleDoc.Memo;
            inDoc.OperDate = DateTime.Now;
            inDoc.DataUpdateTime = DateTime.Now;
            inDoc.Visible = true;
            if (!string.IsNullOrWhiteSpace(employeeID))
            {
                inDoc.UserID = long.Parse(employeeID);
                inDoc.CreaterID = long.Parse(employeeID);
            }
            inDoc.UserName = empName;
            inDoc.CreaterName = empName;
            if (!inDoc.DeptID.HasValue)
            {
                string deptID = Cookie.CookieHelper.Read(DicCookieSession.HRDeptID);
                string deptName = Cookie.CookieHelper.Read(DicCookieSession.HRDeptName);
                if (!string.IsNullOrWhiteSpace(deptID))
                {
                    inDoc.DeptID = long.Parse(deptID);
                    inDoc.DeptName = deptName;
                }
            }
            inDoc.SaleDocID = saleDoc.Id;
            inDoc.SaleDocNo = saleDoc.SaleDocNo;
            inDoc.SourceType = long.Parse(ReaBmsInSourceType.供货入库.Key);
            inDoc.Status = int.Parse(ReaBmsInDocStatus.待继续入库.Key);
            if (inDoc.Status > 0)
                inDoc.StatusName = ReaBmsInDocStatus.GetStatusDic()[inDoc.Status.ToString()].Name;
            inDoc.InType = long.Parse(ReaBmsInDocInType.验货入库.Key);
            if (inDoc.InType.HasValue)
                inDoc.InTypeName = ReaBmsInDocInType.GetStatusDic()[inDoc.InType.Value.ToString()].Name;
        }
        private void CopySaleDtlToInDtl(IBReaGoods IBReaGoods, ReaBmsCenSaleDtl saleDtl, ReaStorage reaStorage, ReaBmsInDoc inDoc, ref ReaBmsInDtl inDtl)
        {
            string employeeID = Cookie.CookieHelper.Read(DicCookieSession.EmployeeID);
            string empName = Cookie.CookieHelper.Read(DicCookieSession.EmployeeName);
            inDtl.InDocID = inDoc.Id;
            inDtl.InDocNo = inDoc.InDocNo;
            inDtl.InDtlNo = this.GetDocNo();
            inDtl.LabID = saleDtl.LabID;
            inDtl.SaleDtlID = saleDtl.Id;
            inDtl.ReaCompanyID = saleDtl.ReaCompID;
            inDtl.CompanyName = saleDtl.ReaCompanyName;
            inDtl.GoodsID = saleDtl.GoodsID;
            inDtl.GoodsCName = saleDtl.GoodsName;
            inDtl.LotSerial = saleDtl.LotSerial;

            inDtl.SysLotSerial = saleDtl.SysLotSerial;
            inDtl.LotQRCode = saleDtl.LotQRCode;
            inDtl.UnitMemo = saleDtl.UnitMemo;
            inDtl.GoodsUnit = saleDtl.GoodsUnit;
            inDtl.GoodsQty = saleDtl.GoodsQty;

            inDtl.Price = saleDtl.Price;
            inDtl.SumTotal = inDtl.GoodsQty * inDtl.Price;// saleDtl.SumTotal;
            inDtl.TaxRate = saleDtl.TaxRate;
            inDtl.LotNo = saleDtl.LotNo;
            inDtl.GoodsUnit = saleDtl.GoodsUnit;

            inDtl.GoodsNo = saleDtl.GoodsNo;
            inDtl.CompGoodsLinkID = saleDtl.CompGoodsLinkID;
            inDtl.ReaServerCompCode = saleDtl.ReaServerCompCode;
            inDtl.ProdDate = saleDtl.ProdDate;
            inDtl.InvalidDate = saleDtl.InvalidDate;

            inDtl.ProdGoodsNo = saleDtl.ProdGoodsNo;
            inDtl.ReaGoodsNo = saleDtl.ReaGoodsNo;
            inDtl.CenOrgGoodsNo = saleDtl.CenOrgGoodsNo;
            //inDtl.ReaCompCode = saleDtl.ReaCompCode;
            inDtl.GoodsSort = saleDtl.GoodsSort;
            inDtl.BarCodeType = saleDtl.BarCodeType;

            inDtl.OtherDtlNo = saleDtl.OtherDtlNo;
            inDtl.GoodsSerial = saleDtl.GoodsSerial;
            inDtl.ZX1 = saleDtl.ZX1;
            inDtl.ZX2 = saleDtl.ZX2;
            inDtl.ZX3 = saleDtl.ZX3;
            if (saleDtl.ReaGoodsID.HasValue && IBReaGoods != null)
            {
                inDtl.ReaGoods = IBReaGoods.Get(saleDtl.ReaGoodsID.Value);
                if (inDtl.ReaGoods == null && !saleDtl.GoodsID.HasValue)
                    inDtl.ReaGoods = IBReaGoods.Get(saleDtl.GoodsID.Value);
            }
            inDtl.DataUpdateTime = DateTime.Now;
            inDtl.Visible = true;
            if (!string.IsNullOrWhiteSpace(employeeID))
            {
                inDtl.CreaterID = long.Parse(employeeID);
            }
            inDtl.CreaterName = empName;
            //提取第三方数据的供货明细盒条码信息(使用分号隔开)
            inDtl.OtherSerialNoStr = saleDtl.OtherSerialNoStr;
            //if (inDtl.BarCodeType == int.Parse(ReaGoodsBarCodeType.批条码.Key))
            //{
            //    inDtl.LotSerial = saleDtl.OtherSerialNoStr;
            //    //dtlConfirm.SysLotSerial = saleDtl.OtherSerialNoStr;
            //    inDtl.LotQRCode = saleDtl.OtherSerialNoStr;
            //}
            if (!string.IsNullOrEmpty(saleDtl.OtherSerialNoStr) && string.IsNullOrEmpty(inDtl.LotSerial))
                inDtl.LotSerial = saleDtl.OtherSerialNoStr;

            if (!string.IsNullOrEmpty(saleDtl.OtherSerialNoStr) && string.IsNullOrEmpty(inDtl.LotQRCode))
                inDtl.LotQRCode = saleDtl.OtherSerialNoStr;           
            //库房货架处理
            if (reaStorage != null)
            {
                inDtl.StorageID = reaStorage.Id;
                inDtl.StorageName = reaStorage.CName;
            }
        }
        private string GetDocNo()
        {
            StringBuilder strb = new StringBuilder();
            strb.Append(DateTime.Now.ToString("yyMMdd"));
            //Random ran = new Random();
            Random ran = new Random(Guid.NewGuid().GetHashCode());//使用GUID的随机6位，普通的在毫秒内会重现重复的随机数，导致单号一样。
            int randKey = ran.Next(0, 999);
            strb.Append(randKey.ToString().PadLeft(3, '0'));//左补零
            strb.Append(DateTime.Now.ToString("HHmmssfff"));

            ZhiFang.Common.Log.Log.Info("动态生成单号=" + strb.ToString());
            return strb.ToString();
        }

        public BaseResultDataValue GetReaBmsSaleDocCovertJson<T, T1>(ReaBmsCenSaleDoc saleDoc, IList<ReaBmsCenSaleDtl> saleDtlList, string mainFields, string childFields, long storageID)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            ParseObjectProperty pop = new ParseObjectProperty(mainFields);
            try
            {
                T docEntity = System.Activator.CreateInstance<T>();
                IList<T1> dtlEntityList = new List<T1>();
                SaleDocDataConvert(saleDoc, saleDtlList, storageID, ref docEntity, ref dtlEntityList);
                string strDoc = pop.GetSingleObjectPlanish<T>(docEntity);
                pop.ParaStr = childFields;
                string strDtl = pop.GetObjectListPlanish<T1>(dtlEntityList);
                JObject jsonObject = JObject.Parse(strDtl);
                string strList = jsonObject["list"].ToString();
                brdv.ResultDataValue = "{ \"Master\":" + strDoc + ",\"Detail\":" + strList + "}";
                ZhiFang.Common.Log.Log.Info("接口获取供货单转换Json串：" + brdv.ResultDataValue);
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "序列化错误：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return brdv;
        }

        public BaseResultDataValue GetReaBmsSaleDocJson<T, T1>(T saleDoc, IList<T1> saleDtlList, string mainFields, string childFields)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            ParseObjectProperty pop = new ParseObjectProperty(mainFields);
            try
            {
                string strDoc = pop.GetSingleObjectPlanish<T>(saleDoc);
                pop.ParaStr = childFields;
                string strDtl = pop.GetObjectListPlanish<T1>(saleDtlList);
                JObject jsonObject = JObject.Parse(strDtl);
                string strList = jsonObject["list"].ToString();
                brdv.ResultDataValue = "{ \"Master\":" + strDoc + ",\"Detail\":" + strList + "}";
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "序列化错误：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return brdv;
        }

        public BaseResultData ConvertReaBmsInDtlList(ReaBmsOutDoc outDoc, IList<ReaBmsOutDtl> outDtlList, ref IList<ReaBmsInDtl> inDtlList)
        {
            BaseResultData baseResultData = new BaseResultData();
            if (inDtlList == null)
                inDtlList = new List<ReaBmsInDtl>();
            IApplicationContext context = ContextRegistry.GetContext();
            IBReaBmsInDtl IBReaBmsInDtl = (IBReaBmsInDtl)context.GetObject("BReaBmsInDtl");
            IBReaBmsQtyDtl IBReaBmsQtyDtl = (IBReaBmsQtyDtl)context.GetObject("BReaBmsQtyDtl");
            IBReaBmsQtyDtlOperation IBReaBmsQtyDtlOperation = (IBReaBmsQtyDtlOperation)context.GetObject("BReaBmsQtyDtlOperation");
            IBReaGoods IBReaGoods = (IBReaGoods)context.GetObject("BReaGoods");

            StringBuilder outDtlIDStr = new StringBuilder();
            StringBuilder qtyDtlIDStr = new StringBuilder();
            foreach (var outDtl in outDtlList)
            {
                outDtlIDStr.Append(outDtl.Id);
                outDtlIDStr.Append(",");
                ZhiFang.Common.Log.Log.Info("出库确认后调用退库接口.outDtl.Id:" + outDtl.Id + ",outDtl.QtyDtlID:" + outDtl.QtyDtlID);
                if (string.IsNullOrEmpty(outDtl.QtyDtlID))
                {
                    continue;
                }
                qtyDtlIDStr.Append(outDtl.QtyDtlID);
                qtyDtlIDStr.Append(",");
            }
            if (qtyDtlIDStr.Length <= 0)
            {
                baseResultData.success = false;
                baseResultData.message = "出库确认后调用退库接口.获取出库明细对应的库存记录信息为空!";
                ZhiFang.Common.Log.Log.Info(baseResultData.message);
                return baseResultData;
            }

            IList<ReaBmsQtyDtl> qtyDtlList = IBReaBmsQtyDtl.SearchListByHQL("reabmsqtydtl.Id in (" + qtyDtlIDStr.ToString().TrimEnd(',') + ")");
            string qtyOperationHql = "reabmsqtydtloperation.BDocID=" + outDoc.Id + " and reabmsqtydtloperation.BDtlID in (" + outDtlIDStr.ToString().TrimEnd(',') + ") and reabmsqtydtloperation.QtyDtlID in (" + qtyDtlIDStr.ToString().TrimEnd(',') + ")";
            IList<ReaBmsQtyDtlOperation> qtyDtlOperationList = IBReaBmsQtyDtlOperation.SearchListByHQL(qtyOperationHql);
            if (qtyDtlList.Count <= 0)
            {
                baseResultData.success = false;
                baseResultData.message = "出库确认后调用退库接口.获取出库明细对应的库存记录信息为空!";
                ZhiFang.Common.Log.Log.Info(baseResultData.message);
                return baseResultData;
            }
            if (qtyDtlOperationList.Count <= 0)
            {
                baseResultData.success = false;
                baseResultData.message = "出库确认后调用退库接口.获取出库明细对应的库存变化操作记录信息为空!";
                ZhiFang.Common.Log.Log.Info(baseResultData.message);
                return baseResultData;
            }

            StringBuilder inDtlIDStr = new StringBuilder();
            foreach (var qtyDtl in qtyDtlList)
            {
                inDtlIDStr.Append(qtyDtl.InDtlID);
                inDtlIDStr.Append(",");
            }
            //找出当次出库确认的库存货品对应的入库明细信息
            IList<ReaBmsInDtl> inDtlList2 = IBReaBmsInDtl.SearchListByHQL("reabmsindtl.Id in (" + inDtlIDStr.ToString().TrimEnd(',') + ")");
            if (inDtlList2.Count <= 0)
            {
                baseResultData.success = false;
                baseResultData.message = "出库确认后调用退库接口.获取出库明细对应的入库明细记录信息为空!";
                ZhiFang.Common.Log.Log.Info(baseResultData.message);
                return baseResultData;
            }

            foreach (var inDtl in inDtlList2)
            {
                double goodsQty = 0;
                ReaBmsInDtl inDtl2 = Common.ClassMapperHelp.GetMapper<ReaBmsInDtl, ReaBmsInDtl>(inDtl);

                #region 出库退库数转换处理
                var tempQtyList2 = qtyDtlList.Where(p => p.InDtlID == inDtl.Id).ToList();
                foreach (var qtyDtl2 in tempQtyList2)
                {
                    var tempQtyOperationList = qtyDtlOperationList.Where(p => p.QtyDtlID == qtyDtl2.Id);
                    foreach (var qtyOperation2 in tempQtyOperationList)
                    {
                        ZhiFang.Common.Log.Log.Info("inDtl.ReaGoods.Id:" + inDtl.ReaGoods.Id + ",qtyOperation2.GoodsID.Value:" + qtyOperation2.GoodsID.Value);
                        double goodsQty2 = 0;
                        if (inDtl.ReaGoods.Id == qtyOperation2.GoodsID.Value)
                        {
                            ZhiFang.Common.Log.Log.Info("inDtl.Id:" + inDtl.Id + ",qtyDtl.Id:" + qtyDtl2.Id + ",ReaBmsQtyDtlOperation.GoodsQty:" + qtyOperation2.GoodsQty.Value);
                            goodsQty2 = qtyOperation2.GoodsQty.Value;
                        }
                        else
                        {
                            //将库存变化操作记录变动数按入库明细的包装单位进行转换
                            ReaGoods reaGoods = IBReaGoods.Get(qtyOperation2.GoodsID.Value);
                            ZhiFang.Common.Log.Log.Info("ReaBmsQtyDtlOperation.GonvertQty:" + reaGoods.GonvertQty + ",inDtl.ReaGoods.GonvertQty:" + inDtl.ReaGoods.GonvertQty);
                            if (reaGoods.GonvertQty <= inDtl.ReaGoods.GonvertQty && inDtl.ReaGoods.GonvertQty > 0)
                            {
                                goodsQty2 = qtyOperation2.GoodsQty.Value / inDtl.ReaGoods.GonvertQty;
                            }
                            else if (reaGoods.GonvertQty > inDtl.ReaGoods.GonvertQty && inDtl.ReaGoods.GonvertQty > 0)
                            {
                                goodsQty2 = qtyOperation2.GoodsQty.Value * inDtl.ReaGoods.GonvertQty;
                            }
                            else
                            {
                                goodsQty2 = qtyOperation2.GoodsQty.Value;
                            }
                        }
                        if (goodsQty2 < 0)
                            goodsQty2 = -goodsQty2;
                        goodsQty = goodsQty + goodsQty2;
                    }
                }
                #endregion

                ZhiFang.Common.Log.Log.Info("inDtl.Id:" + inDtl2.Id + ",原入库数为:" + inDtl2.GoodsQty + ",现出库确认后的退库数为:" + goodsQty);
                inDtl2.GoodsQty = goodsQty;
                inDtl2.SumTotal = inDtl2.GoodsQty * inDtl2.Price;
                ZhiFang.Common.Log.Log.Info("inDtl.Id:" + inDtl2.Id + ",inDtl.GoodsQty:" + inDtl2.GoodsQty + ",inDtl.SumTotal:" + inDtl2.SumTotal);
                inDtlList.Add(inDtl2);
            }
            return baseResultData;
        }

        public BaseResultData DisposeReaBmsOutDtlList(ReaBmsOutDoc outDoc, ref IList<ReaBmsOutDtl> outDtlList)
        {
            BaseResultData baseResultData = new BaseResultData();
            IApplicationContext context = ContextRegistry.GetContext();
            IBReaBmsQtyDtl IBReaBmsQtyDtl = (IBReaBmsQtyDtl)context.GetObject("BReaBmsQtyDtl");
            IBReaBmsQtyDtlOperation IBReaBmsQtyDtlOperation = (IBReaBmsQtyDtlOperation)context.GetObject("BReaBmsQtyDtlOperation");
            ZhiFang.Common.Log.Log.Info("接口调用---出库单号：" + outDoc.OutDocNo);
            string qtyOperationHql = "reabmsqtydtloperation.BDocID=" + outDoc.Id;
            IList<ReaBmsQtyDtlOperation> qtyDtlOperationList = IBReaBmsQtyDtlOperation.SearchListByHQL(qtyOperationHql);
            if (qtyDtlOperationList.Count <= 0)
            {
                baseResultData.success = false;
                baseResultData.message = "出库确认后调用退库接口.获取出库明细对应的库存变化操作记录信息为空!";
                ZhiFang.Common.Log.Log.Info(baseResultData.message);
                return baseResultData;
            }
            IList<ReaBmsOutDtl> tempOutDtlList = new List<ReaBmsOutDtl>();
            foreach (var outDtl in outDtlList)
            {
                ZhiFang.Common.Log.Log.Info("接口调用---出库明细单ID：" + outDtl.Id);
                IList<ReaBmsQtyDtlOperation> listQtyDtlOper = qtyDtlOperationList.Where(p => p.BDtlID == outDtl.Id).ToList();
                if (listQtyDtlOper != null)
                {
                    ZhiFang.Common.Log.Log.Info("接口调用---出库明细单对应库存记录数：" + listQtyDtlOper.Count);
                    if (listQtyDtlOper.Count > 1)
                    {
                        outDtl.GoodsQty = (double)listQtyDtlOper[0].ChangeCount;
                        outDtl.ReqGoodsQty = outDtl.GoodsQty;
                        outDtl.SumTotal = outDtl.GoodsQty * outDtl.Price;
                        outDtl.QtyDtlID = listQtyDtlOper[0].QtyDtlID.ToString();
                        for (int i = 1; i < listQtyDtlOper.Count; i++)
                        {
                            ReaBmsOutDtl tempOutDtl = TransObject<ReaBmsOutDtl, ReaBmsOutDtl>.Trans(outDtl);
                            tempOutDtl.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                            tempOutDtl.GoodsQty = (double)listQtyDtlOper[i].ChangeCount;
                            tempOutDtl.ReqGoodsQty = tempOutDtl.GoodsQty;
                            tempOutDtl.SumTotal = outDtl.GoodsQty * outDtl.Price;
                            tempOutDtl.QtyDtlID = listQtyDtlOper[i].QtyDtlID.ToString();
                            tempOutDtlList.Add(tempOutDtl);
                        }
                    }
                    else if (listQtyDtlOper.Count == 1)
                        outDtl.QtyDtlID = listQtyDtlOper[0].QtyDtlID.ToString();
                }
            }//foreach
            outDtlList = outDtlList.Union(tempOutDtlList).ToList();
            return baseResultData;
        }


        public BaseResultData ConvertReaBmsInDtlList(IList<ReaBmsOutDtl> outDtlList, ref IList<ReaBmsInDtl> inDtlList)
        {
            BaseResultData baseResultData = new BaseResultData();
            IApplicationContext context = ContextRegistry.GetContext();
            IBReaBmsInDtl IBReaBmsInDtl = (IBReaBmsInDtl)context.GetObject("BReaBmsInDtl");
            IBReaBmsQtyDtl IBReaBmsQtyDtl = (IBReaBmsQtyDtl)context.GetObject("BReaBmsQtyDtl");
            StringBuilder qtyDtlIDStr = new StringBuilder();
            foreach (var outDtl in outDtlList)
            {
                ZhiFang.Common.Log.Log.Info("出库确认后调用退库接口.outDtl.Id:" + outDtl.Id + ",outDtl.QtyDtlID:" + outDtl.QtyDtlID);
                if (string.IsNullOrEmpty(outDtl.QtyDtlID))
                {
                    continue;
                }
                qtyDtlIDStr.Append(outDtl.QtyDtlID);
                qtyDtlIDStr.Append(",");
            }

            IList<ReaBmsQtyDtl> qtyDtlList = IBReaBmsQtyDtl.SearchListByHQL("reabmsqtydtl.Id in (" + qtyDtlIDStr.ToString().TrimEnd(',') + ")");
            if (qtyDtlList.Count <= 0)
            {
                baseResultData.success = false;
                baseResultData.message = "出库确认后调用退库接口.获取出库明细对应的库存记录信息为空!";
                ZhiFang.Common.Log.Log.Info(baseResultData.message);
                return baseResultData;
            }
            StringBuilder inDtlIDStr = new StringBuilder();
            foreach (var qtyDtl in qtyDtlList)
            {
                inDtlIDStr.Append(qtyDtl.InDtlID);
                inDtlIDStr.Append(",");
            }
            //找出当次出库确认的库存货品对应的入库明细信息
            inDtlList = IBReaBmsInDtl.SearchListByHQL("reabmsindtl.Id in (" + inDtlIDStr.ToString().TrimEnd(',') + ")");
            if (inDtlList.Count <= 0)
            {
                baseResultData.success = false;
                baseResultData.message = "出库确认后调用退库接口.获取出库明细对应的入库明细记录信息为空!";
                ZhiFang.Common.Log.Log.Info(baseResultData.message);
                return baseResultData;
            }
            return baseResultData;
        }

        public BaseResultData ConvertInDtlListToInDocList(IList<ReaBmsInDtl> inDtlList, ref Dictionary<long, KeyValuePair<ReaBmsInDoc, IList<ReaBmsInDtl>>> dic)
        {
            BaseResultData baseResultData = new BaseResultData();
            IList<long> longDocID = new List<long>();
            if (dic == null)
                dic = new Dictionary<long, KeyValuePair<ReaBmsInDoc, IList<ReaBmsInDtl>>>();
            if (inDtlList != null && inDtlList.Count > 0)
            {
                IApplicationContext context = ContextRegistry.GetContext();
                IBReaBmsInDoc IBReaBmsInDoc = (IBReaBmsInDoc)context.GetObject("BReaBmsInDoc");
                foreach (ReaBmsInDtl inDtl in inDtlList)
                {
                    if (dic.ContainsKey(inDtl.InDocID))
                    {
                        dic[inDtl.InDocID].Value.Add(inDtl);
                    }
                    else
                    {
                        ReaBmsInDoc inDoc = IBReaBmsInDoc.Get(inDtl.InDocID);
                        KeyValuePair<ReaBmsInDoc, IList<ReaBmsInDtl>> kv = new KeyValuePair<ReaBmsInDoc, IList<ReaBmsInDtl>>(inDoc, new List<ReaBmsInDtl> { inDtl });
                        dic.Add(inDtl.InDocID, kv);
                    }
                }
            }
            return baseResultData;
        }

        #region 解析入库接口的xml转换为入库单和入库明细对象

        /// <summary>
        /// 解析xml，转换为ReaBmsInDtl和ReaBmsInDtl对象
        /// </summary>
        /// <param name="xmlData">xml</param>
        /// <param name="doc">入库单</param>
        /// <param name="dtlList">入库单明细</param>
        /// <returns></returns>
        public void ConvertReaBmsInDocAndDtlByXml(string xmlData, ref ReaBmsInDoc doc, ref IList<ReaBmsInDtl> dtlList, ref HREmployee emp)
        {
            Log.Info("解析xml转换为入库单和入库明细对象开始");
            BaseResultData baseResultData = new BaseResultData();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlData);
            XmlNode nodeMaster = xmlDoc.SelectSingleNode("//Body/Master");
            if (nodeMaster == null)
            {
                throw new Exception("XML格式错误！未找到主单信息！");
            }
            XmlNodeList nodeDetail = xmlDoc.SelectNodes("//Body/Master/Detail/Row");
            if (nodeDetail == null || nodeDetail.Count == 0)
            {
                throw new Exception("XML格式错误！未找到明细信息！");
            }
            //获取员工信息，并验证
            if (nodeMaster["UserCode"] == null || nodeMaster["UserCode"].InnerText.Trim() == "")
            {
                throw new Exception("[UserCode]操作人员编码不能为空！");
            }
            string userCode = nodeMaster["UserCode"].InnerText;           
            //转换为智方的员工ID，获取机构编码
            InterfaceCommon ic = new InterfaceCommon();
            IApplicationContext context = ContextRegistry.GetContext();
            IBHREmployee IBHREmployee = (IBHREmployee)context.GetObject("BHREmployee");
            var empList = IBHREmployee.SearchListByHQL(string.Format("MatchCode='{0}'", userCode));
            if (empList.Count == 0 || empList[0].IsUse == null || !empList[0].IsUse.Value || empList[0].IsEnabled == 0)
            {
                throw new Exception("员工信息不存在或未启用！无法保存！");
            }
            emp = empList[0];
            CenOrg cenOrg = null;
            baseResultData = ic.GetCurUserCenOrg(emp, ref cenOrg);
            if (!baseResultData.success)
            {
                throw new Exception(baseResultData.message);
            }
            //转换部门，如果传过来的部门不为空，优先取传过来的部门
            if (nodeMaster["DeptCode"] == null || nodeMaster["DeptCode"].InnerText.Trim() == "")
            {
                throw new Exception("[DeptCode]科室编码不能为空！");
            }
            string deptCode = nodeMaster["DeptCode"].InnerText;
            if (!string.IsNullOrWhiteSpace(deptCode))
            {
                IBHRDept IBHRDept = (IBHRDept)context.GetObject("BHRDept");
                var deptList = IBHRDept.SearchListByHQL(string.Format("MatchCode='{0}'", deptCode));
                if (deptList.Count > 0)
                {
                    emp.HRDept = deptList[0];
                }
            }

            Log.Info("入库主单属性赋值开始！");
            ReaBmsInDoc reaBmsInDoc = new ReaBmsInDoc();//System.Activator.CreateInstance<ReaBmsInDoc>();
            foreach (XmlNode childNode in nodeMaster.ChildNodes)
            {
                if (childNode.Name != "Detail")
                {
                    try
                    {
                        PropertyInfo pi = reaBmsInDoc.GetType().GetProperty(childNode.Name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                        if (pi == null) continue;
                        if (!String.IsNullOrEmpty(childNode.InnerXml.Trim()))
                        {
                            pi.SetValue(reaBmsInDoc, DataConvert(pi, childNode.InnerXml), null);
                            Log.Info(string.Format("{0}={1}", childNode.Name, childNode.InnerXml));
                        }
                    }
                    catch (Exception e1)
                    {
                        string errorInfo = "入库单主单属性赋值失败：" + childNode.Name + "=" + childNode.InnerXml + "。 Error：" + e1.Message;
                        Log.Error(errorInfo);
                        throw new Exception(errorInfo);
                    }
                }
            }
            reaBmsInDoc.InDocNo = (string.IsNullOrWhiteSpace(reaBmsInDoc.InDocNo) ? GetDocNo() : reaBmsInDoc.InDocNo);
            reaBmsInDoc.LabID = emp.LabID;
            if (reaBmsInDoc.InType != int.Parse(ReaBmsInDocInType.验货入库.Key))
            {
                reaBmsInDoc.InType = int.Parse(ReaBmsInDocInType.验货入库.Key);
            }
            reaBmsInDoc.InTypeName = ReaBmsInDocInType.GetStatusDic()[reaBmsInDoc.InType.Value.ToString()].Name;
            reaBmsInDoc.SourceType = long.Parse(ReaBmsInSourceType.验货入库.Key);
            if (reaBmsInDoc.Status != int.Parse(ReaBmsInDocStatus.已入库.Key))
            {
                reaBmsInDoc.Status = int.Parse(ReaBmsInDocStatus.已入库.Key);
            }
            reaBmsInDoc.StatusName = ReaBmsInDocStatus.GetStatusDic()[ReaBmsInDocStatus.已入库.Key].Name;
            reaBmsInDoc.OperDate = (reaBmsInDoc.OperDate == null ? DateTime.Now : reaBmsInDoc.OperDate);
            reaBmsInDoc.Visible = true;
            reaBmsInDoc.DataAddTime = (reaBmsInDoc.DataAddTime == null ? DateTime.Now : reaBmsInDoc.DataAddTime);
            reaBmsInDoc.DataUpdateTime = DateTime.Now;
            reaBmsInDoc.UserID = emp.Id;
            reaBmsInDoc.UserName = (string.IsNullOrWhiteSpace(reaBmsInDoc.UserName) ? emp.CName : reaBmsInDoc.UserName);
            reaBmsInDoc.CreaterID = emp.Id;
            reaBmsInDoc.CreaterName = reaBmsInDoc.UserName;
            reaBmsInDoc.DeptID = emp.HRDept.Id;
            reaBmsInDoc.DeptName = emp.HRDept.CName;
            doc = reaBmsInDoc;
            Log.Info("入库主单属性赋值结束！");


            Log.Info("入库明细属性赋值开始！");
            IBReaCenOrg IBReaCenOrg = (IBReaCenOrg)context.GetObject("BReaCenOrg");
            IBReaGoods IBReaGoods = (IBReaGoods)context.GetObject("BReaGoods");
            IBReaGoodsOrgLink IBReaGoodsOrgLink= (IBReaGoodsOrgLink)context.GetObject("BReaGoodsOrgLink");
            IBReaStorage IBReaStorage = (IBReaStorage)context.GetObject("BReaStorage");
            if (dtlList == null)
            {
                dtlList = new List<ReaBmsInDtl>();
            }
            foreach (XmlNode node in nodeDetail)
            {
                ReaBmsInDtl reaBmsInDtl = new ReaBmsInDtl();
                foreach (XmlNode childNode in node.ChildNodes)
                {
                    try
                    {
                        PropertyInfo pi = reaBmsInDtl.GetType().GetProperty(childNode.Name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                        if (pi == null) continue;
                        if (!String.IsNullOrEmpty(childNode.InnerXml.Trim()))
                        {
                            pi.SetValue(reaBmsInDtl, DataConvert(pi, childNode.InnerXml), null);
                            Log.Info(string.Format("{0}={1}", childNode.Name, childNode.InnerXml));
                        }
                    }
                    catch (Exception e1)
                    {
                        string errorInfo = "入库单明细属性赋值失败：" + childNode.Name + "=" + childNode.InnerXml + "。 Error：" + e1.Message;
                        Log.Error(errorInfo);
                        throw new Exception(errorInfo);
                    }
                }

                #region 非空验证
                if (string.IsNullOrWhiteSpace(reaBmsInDtl.LotQRCode))
                {
                    throw new Exception("[LotQRCode]二维批条码不能为空！无法保存！");
                }
                if (string.IsNullOrWhiteSpace(reaBmsInDtl.ReaCompCode))
                {
                    throw new Exception("[ReaCompCode]供应商编码不能为空！无法保存！");
                }
                if (string.IsNullOrWhiteSpace(reaBmsInDtl.GoodsNo))
                {
                    throw new Exception("[GoodsNo]货品编码不能为空！无法保存！");
                }
                if (string.IsNullOrWhiteSpace(reaBmsInDtl.StorageNo))
                {
                    throw new Exception("[StorageNo]库房编码不能为空！无法保存！");
                }
                #endregion
                
                #region 供应商信息获取
                var orgList = IBReaCenOrg.SearchListByHQL(string.Format("MatchCode='{0}'", reaBmsInDtl.ReaCompCode));
                if (orgList.Count > 0)
                {
                    Log.Info(string.Format("[ReaCompCode]供应商编码转换：第三方编码={0}，转换为智方编码={1}", reaBmsInDtl.ReaCompCode, orgList[0].PlatformOrgNo));
                    reaBmsInDtl.ReaCompCode = orgList[0].PlatformOrgNo.Value.ToString();
                    reaBmsInDtl.ReaCompanyID = orgList[0].Id;
                    reaBmsInDtl.CompanyName = orgList[0].CName;
                }
                else
                {
                    throw new Exception("根据对照码未获取到供应商信息！无法保存！");
                }
                #endregion

                #region 货品ID信息、供应商货品关系ID获取
                var goodsList = IBReaGoods.SearchListByHQL(string.Format("MatchCode='{0}'", reaBmsInDtl.GoodsNo));
                if (goodsList.Count > 0)
                {
                    Log.Info(string.Format("[GoodsNo]货品编码转换：第三方编码={0}，转换为智方编码={1}", reaBmsInDtl.GoodsNo, goodsList[0].ReaGoodsNo));
                    reaBmsInDtl.GoodsID = goodsList[0].Id;
                    reaBmsInDtl.GoodsNo = goodsList[0].ReaGoodsNo;
                    reaBmsInDtl.ReaGoodsNo= goodsList[0].ReaGoodsNo;
                    reaBmsInDtl.ReaGoods = goodsList[0];

                    //供应商货品关系ID获取
                    var linkList = IBReaGoodsOrgLink.SearchListByHQL(string.Format("GoodsID={0} and OrgID={1}", reaBmsInDtl.GoodsID, reaBmsInDtl.ReaCompanyID));
                    if (linkList.Count > 0)
                    {
                        reaBmsInDtl.CompGoodsLinkID = linkList[0].Id;
                    }
                    else
                    {
                        //自动创建供应商货品关系
                        IList<ReaGoods> listReaGoods = new List<ReaGoods>();
                        goodsList[0].ReaCompCode = orgList[0].MatchCode;//当前填充供应商的MatchCode字段
                        listReaGoods.Add(goodsList[0]);
                        ReaGoodsOrgLink reaGoodsOrgLink = null;
                        baseResultData = IBReaGoodsOrgLink.SaveReaGoodsOrgLinkByMatchInterface(listReaGoods, orgList[0], emp.Id, emp.CName, ref reaGoodsOrgLink);
                        if (baseResultData.success)
                        {
                            reaBmsInDtl.CompGoodsLinkID = reaGoodsOrgLink.Id;
                        }
                        else
                        {
                            throw new Exception(baseResultData.message);
                        }
                    }
                }
                else
                {
                    throw new Exception("根据对照码未获取到货品信息！无法保存！");
                }
                #endregion

                #region 库房获取
                var storageList = IBReaStorage.SearchListByHQL(string.Format("MatchCode='{0}'", reaBmsInDtl.StorageNo));
                if (storageList.Count > 0)
                {
                    Log.Info(string.Format("[StorageNo]库房编码转换：第三方编码={0}，转换为智方编码={1}", reaBmsInDtl.StorageNo, storageList[0].Id));
                    reaBmsInDtl.StorageID = storageList[0].Id;
                    reaBmsInDtl.StorageName = storageList[0].CName;
                }
                else
                {
                    throw new Exception("根据对照码未获取到库房信息！无法保存！");
                }
                #endregion

                reaBmsInDtl.InDocNo = reaBmsInDoc.InDocNo;
                reaBmsInDtl.InDtlNo = (string.IsNullOrWhiteSpace(reaBmsInDtl.InDtlNo) ? GetDocNo() : reaBmsInDtl.InDtlNo);
                reaBmsInDtl.LabID = emp.LabID;
                reaBmsInDtl.InDocID = reaBmsInDoc.Id;
                reaBmsInDtl.SumTotal = reaBmsInDtl.GoodsQty * reaBmsInDtl.Price;
                reaBmsInDtl.CreaterID = emp.Id;
                reaBmsInDtl.CreaterName = emp.CName;
                reaBmsInDtl.DataAddTime = DateTime.Now;
                reaBmsInDtl.DataUpdateTime = DateTime.Now;
                dtlList.Add(reaBmsInDtl);
            }
            Log.Info("入库明细属性赋值结束！");

            Log.Info("解析xml转换为入库单和入库明细对象结束");
        }

        private object DataConvert(System.Reflection.PropertyInfo propertyInfo, object dataColumnValue)
        {
            object resultStr = null;
            Type type = propertyInfo.PropertyType;
            string columnValue = dataColumnValue.ToString();
            if (!string.IsNullOrEmpty(columnValue))
            {
                columnValue = columnValue.Trim();
                if (type == typeof(int) || type == typeof(int?))
                {
                    resultStr = int.Parse(columnValue);
                }
                else if (type == typeof(Int64) || type == typeof(Int64?))
                {
                    resultStr = Int64.Parse(columnValue);
                }
                else if (type == typeof(double) || type == typeof(double?))
                {
                    resultStr = double.Parse(columnValue);
                }
                else if ((type == typeof(DateTime)) || (type == typeof(DateTime?)))
                {
                    resultStr = DateTime.Parse(columnValue);
                }
                else if (type == typeof(Boolean))
                {
                    resultStr = Boolean.Parse(columnValue);
                }
                else if (dataColumnValue is BaseEntity)
                    resultStr = dataColumnValue;
                else
                    resultStr = columnValue;
            }
            return resultStr;
        }

        #endregion

        #region 更新LIS订单数据标志服务，解析xml转换为订单对象

        public void ConvertReaBmsInDocAndDtlByXml(string xmlData, ref ReaBmsCenOrderDoc doc)
        {
            Log.Info("解析xml转换为订单对象开始");
            BaseResultData baseResultData = new BaseResultData();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlData);
            XmlNode nodeMaster = xmlDoc.SelectSingleNode("//Body/Master");
            if (nodeMaster == null)
            {
                throw new Exception("XML格式错误！未找到订单信息！");
            }
            if (nodeMaster["OrderDocID"] == null || nodeMaster["OrderDocID"].InnerText.Trim() == "")
            {
                throw new Exception("[OrderDocID]订单唯一标识不能为空！");
            }
            if (nodeMaster["IOFlag"] == null || nodeMaster["IOFlag"].InnerText.Trim() == "")
            {
                throw new Exception("[IOFlag]数据上传标志不能为空！");
            }
            if (nodeMaster["IOFlag"].InnerText.Trim() != "2" && nodeMaster["IOFlag"].InnerText.Trim() != "3")
            {
                throw new Exception("[IOFlag]数据上传标志值范围只能为2或3！");
            }
            IApplicationContext context = ContextRegistry.GetContext();
            IBReaBmsCenOrderDoc IBReaBmsCenOrderDoc = (IBReaBmsCenOrderDoc)context.GetObject("BReaBmsCenOrderDoc");

            string orderDocID = nodeMaster["OrderDocID"].InnerText.Trim();
            doc = IBReaBmsCenOrderDoc.Get(long.Parse(orderDocID));
            if (doc != null)
            {
                doc.IOFlag = int.Parse(nodeMaster["IOFlag"].InnerText.Trim());
            }
        }

        #endregion

    }
}