using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Xml;
using System.Xml.Linq;
using System.Reflection;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaSale;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaConfirm;
using Newtonsoft.Json.Linq;
using ZhiFang.ReagentSys.Client.Common;

namespace ZhiFang.BLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public class BReaBmsCenSaleDoc : BaseBLL<ReaBmsCenSaleDoc>, ZhiFang.IBLL.ReagentSys.Client.IBReaBmsCenSaleDoc
    {
        IBReaBmsCenSaleDtl IBReaBmsCenSaleDtl { get; set; }
        IBReaReqOperation IBReaReqOperation { get; set; }
        IDReaGoodsBarcodeOperationDao IDReaGoodsBarcodeOperationDao { get; set; }
        IDReaBmsCenSaleDtlConfirmDao IDReaBmsCenSaleDtlConfirmDao { get; set; }
        IDCenOrgDao IDCenOrgDao { get; set; }
        IBReaBmsCenOrderDoc IBReaBmsCenOrderDoc { get; set; }
        IBReaBmsCenOrderDtl IBReaBmsCenOrderDtl { get; set; }
        IDReaGoodsOrgLinkDao IDReaGoodsOrgLinkDao { get; set; }
        IDReaCenOrgDao IDReaCenOrgDao { get; set; }
        IBReaCenOrg IBReaCenOrg { get; set; }
        IBReaGoods IBReaGoods { get; set; }
        IBReaGoodsOrgLink IBReaGoodsOrgLink { get; set; }
        IBReaGoodsBarcodeOperation IBReaGoodsBarcodeOperation { get; set; }

        #region 客户端实验室供货单处理
        public BaseResultBool EditBmsCenSaleDocTotalPrice(long saleDocID, long delDtlId)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            IList<ReaBmsCenSaleDtl> dtlList = IBReaBmsCenSaleDtl.SearchListByHQL("reabmscensaledtl.Id!=" + delDtlId + " and reabmscensaledtl.SaleDocID=" + saleDocID);
            ReaBmsCenSaleDoc doc = this.Get(saleDocID);
            double? totalPrice = dtlList.Sum(p => p.SumTotal);
            doc.TotalPrice = totalPrice;
            this.Entity = doc;
            baseResultBool.success = this.Edit();

            return baseResultBool;
        }
        public void GetReaCompCode(ref ReaBmsCenSaleDoc entity)
        {
            if (string.IsNullOrEmpty(entity.ReaCompCode) && entity.CompID.HasValue)
            {
                IList<ReaCenOrg> tempList = IDReaCenOrgDao.GetListByHQL(string.Format("reacenorg.LabID={0} and reacenorg.Id={1}", entity.LabID, entity.CompID.Value));
                if (tempList != null && tempList.Count() == 1)
                    entity.ReaCompCode = tempList[0].OrgNo.Value.ToString();
            }
            if (string.IsNullOrEmpty(entity.ReaCompCode) && !string.IsNullOrEmpty(entity.ReaServerCompCode))
            {
                IList<ReaCenOrg> tempList = IDReaCenOrgDao.GetListByHQL(string.Format("reacenorg.LabID={0} and reacenorg.PlatformOrgNo='{1}'", entity.LabID, entity.ReaServerCompCode));
                if (tempList != null && tempList.Count() == 1)
                    entity.ReaCompCode = tempList[0].OrgNo.Value.ToString();
            }
            if (!entity.ReaCompID.HasValue)
                entity.ReaCompID = entity.CompID;
            if (string.IsNullOrEmpty(entity.ReaCompanyName))
                entity.ReaCompanyName = entity.CompanyName;
        }
        public BaseResultDataValue AddReaBmsCenSaleDocAndDtl(ReaBmsCenSaleDoc entity, IList<ReaBmsCenSaleDtl> dtAddList, long empID, string empName, bool isValid)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "entity为空!";
                return baseResultDataValue;
            }
            if (dtAddList == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "供货明细信息为空!";
                return baseResultDataValue;
            }
            if (isValid)
            {
                BaseResultBool tempBaseResultBool = IBReaBmsCenSaleDtl.EditSaleDtlListOfValid(dtAddList);
                if (tempBaseResultBool.success == false)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = tempBaseResultBool.ErrorInfo;
                    return baseResultDataValue;
                }
            }
            //供货方的信息处理
            GetReaCompCode(ref entity);

            if (entity.Status <= 0) entity.Status = int.Parse(ReaBmsCenSaleDocAndDtlStatus.确认提交.Key);
            entity.StatusName = ReaBmsCenSaleDocAndDtlStatus.GetStatusDic()[entity.Status.ToString()].Name;

            entity.SaleDocNo = GetSaleDocNo();
            entity.UserID = empID;
            entity.UserName = empName;
            entity.OperDate = DateTime.Now;
            if (!entity.TotalPrice.HasValue && dtAddList != null)
                entity.TotalPrice = dtAddList.Sum(p => p.SumTotal);
            this.Entity = entity;
            if (this.Add())
            {
                BaseResultBool tempBaseResultBool2 = IBReaBmsCenSaleDtl.AddSaleDtlList(this.Entity, dtAddList, empID, empName);
                if (tempBaseResultBool2.success == false)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = tempBaseResultBool2.ErrorInfo;
                    return baseResultDataValue;
                }
                AddReaReqOperation(this.Entity, empID, empName);
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "新增供货单失败！";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue AddReaBmsCenSaleDocAndDtl(IList<ReaBmsCenSaleDtlVO> dtAddList, long empID, string empName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (this.Entity == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "Entity为空!";
                return baseResultDataValue;
            }
            if (dtAddList == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "供货明细信息为空!";
                return baseResultDataValue;
            }
            //如果是从平台提取新增,先验证该供就商的当前供货单是否已存在
            if (this.Entity.CenSaleDocID.HasValue)
            {
                IList<ReaBmsCenSaleDoc> tempList = this.SearchListByHQL("reabmscensaledoc.CenSaleDocID=" + this.Entity.CenSaleDocID.Value);
                if (tempList.Count > 0)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = string.Format("供货单号为{0},已被提取到客户端,请不要重复提取!", this.Entity.SaleDocNo);
                    return baseResultDataValue;
                }
            }
            BaseResultBool tempBaseResultBool = IBReaBmsCenSaleDtl.EditSaleDtlListOfVOValid(dtAddList);
            if (tempBaseResultBool.success == false)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = tempBaseResultBool.ErrorInfo;
                return baseResultDataValue;
            }

            if (this.Entity.Status == 0) this.Entity.Status = int.Parse(ReaBmsCenSaleDocAndDtlStatus.审核通过.Key);
            this.Entity.StatusName = ReaBmsCenSaleDocAndDtlStatus.GetStatusDic()[this.Entity.Status.ToString()].Name;

            this.Entity.SaleDocNo = GetSaleDocNo();
            this.Entity.UserID = empID;
            this.Entity.UserName = empName;
            this.Entity.OperDate = DateTime.Now;

            this.Entity.CheckerID = empID;
            this.Entity.Checker = empName;
            if (!this.Entity.CheckTime.HasValue)
                this.Entity.CheckTime = DateTime.Now;
            if (this.Add())
            {
                tempBaseResultBool = IBReaBmsCenSaleDtl.AddDtlListOfVO(this.Entity, dtAddList, empID, empName);
                if (tempBaseResultBool.success == false)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = tempBaseResultBool.ErrorInfo;
                    return baseResultDataValue;
                }
                //从平台提取导入的供货单
                if (baseResultDataValue.success == true && this.Entity.CenSaleDocID.HasValue)
                {
                    //回写更新平台供货单信息(前台处理)
                }
                AddReaReqOperation(this.Entity, empID, empName);
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "新增供货单失败！";
            }
            return baseResultDataValue;
        }
        public BaseResultBool UpdateReaBmsCenSaleDocAndDt(ReaBmsCenSaleDoc entity, string[] tempArray, IList<ReaBmsCenSaleDtl> dtAddList, IList<ReaBmsCenSaleDtl> dtEditList, string dtlFields, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();

            if (dtAddList != null && dtAddList.Count > 0)
            {
                tempBaseResultBool = IBReaBmsCenSaleDtl.EditSaleDtlListOfValid(dtAddList);
                if (tempBaseResultBool.success == false) return tempBaseResultBool;
            }
            if (dtEditList != null && dtEditList.Count > 0)
            {
                tempBaseResultBool = IBReaBmsCenSaleDtl.EditSaleDtlListOfValid(dtEditList);
                if (tempBaseResultBool.success == false) return tempBaseResultBool;
            }
            if (entity.Status > 0)
                entity.StatusName = ReaBmsCenSaleDocAndDtlStatus.GetStatusDic()[entity.Status.ToString()].Name;
            ReaBmsCenSaleDoc serverEntity = this.Get(entity.Id);

            List<string> tmpa = tempArray.ToList();
            if (!EditReaBmsCenSaleDocStatusCheck(entity, serverEntity, tmpa, empID, empName))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "供货单ID：" + entity.Id + "的状态为：" + ReaBmsCenSaleDocAndDtlStatus.GetStatusDic()[serverEntity.Status.ToString()].Name + "！";
                return tempBaseResultBool;
            }
            //如果是取消审核操作,需要判断供货明细的条码是否已被打印过,如果已打印过,不能取消审核
            if (entity.Status.ToString() == ReaBmsCenSaleDocAndDtlStatus.取消审核.Key && serverEntity.Status.ToString() == ReaBmsCenSaleDocAndDtlStatus.审核通过.Key)
            {
                //批条码
                IList<ReaBmsCenSaleDtl> tempDtlList = IBReaBmsCenSaleDtl.SearchListByHQL("reabmscensaledtl.SaleDocID=" + entity.Id + " and reabmscensaledtl.PrintCount>0 and reabmscensaledtl.BarCodeType=" + ReaGoodsBarCodeType.批条码.Key);
                if (tempDtlList.Count > 0)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "供货单ID：" + entity.Id + "供货明细的条码已被打印,不能取消审核！";
                    return tempBaseResultBool;
                }
                //盒条码
                string boxBarCodeHql = string.Format("reagoodsbarcodeoperation.PrintCount>0 and reagoodsbarcodeoperation.BDocID={0} and reagoodsbarcodeoperation.BarcodeCreatType={1} and reagoodsbarcodeoperation.OperTypeID={2}", entity.Id, long.Parse(ReaGoodsBarcodeOperationSerialType.条码生成.Key), long.Parse(ReaGoodsBarcodeOperType.供货.Key));
                IList<ReaGoodsBarcodeOperation> boxDtlList = IDReaGoodsBarcodeOperationDao.GetListByHQL(boxBarCodeHql);
                if (boxDtlList.Count > 0)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "供货单ID：" + entity.Id + "供货明细的条码已被打印,不能取消审核！";
                    return tempBaseResultBool;
                }
            }

            try
            {
                tmpa.Add("StatusName='" + entity.StatusName + "'");
                tempArray = tmpa.ToArray();
                this.Entity = entity;

                //保存供单之前，校验供货数量，修改订单状态和已供数量、未供数量
                tempBaseResultBool = IBReaBmsCenSaleDtl.CompareAndGetOrderDtlQty(this.Entity, dtAddList, dtEditList);
                if (tempBaseResultBool.success == false) return tempBaseResultBool;

                if (dtAddList != null && dtAddList.Count > 0) tempBaseResultBool = IBReaBmsCenSaleDtl.AddSaleDtlList(this.Entity, dtAddList, empID, empName);
                if (dtEditList != null && dtEditList.Count > 0) tempBaseResultBool = IBReaBmsCenSaleDtl.UpdateSaleDtlList(this.Entity, dtEditList, dtlFields, empID, empName);
                if (tempBaseResultBool.success == false) return tempBaseResultBool;

                tempBaseResultBool.success = this.Update(tempArray);//this.Edit();
                if (tempBaseResultBool.success)
                {
                    AddReaReqOperation(this.Entity, empID, empName);
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "保存供货单失败!";
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "供货单编辑保存失败:错误信息:" + ex.Message;
                //ZhiFang.Common.Log.Log.Error(tempBaseResultBool.ErrorInfo);
                throw ex;
            }
            return tempBaseResultBool;
        }
        bool EditReaBmsCenSaleDocStatusCheck(ReaBmsCenSaleDoc entity, ReaBmsCenSaleDoc serverEntity, List<string> tmpa, long empID, string empName)
        {
            entity.DataUpdateTime = DateTime.Now;

            #region 暂存
            if (entity.Status.ToString() == ReaBmsCenSaleDocAndDtlStatus.暂存.Key)
            {
                if (serverEntity.Status.ToString() != ReaBmsCenSaleDocAndDtlStatus.暂存.Key && serverEntity.Status.ToString() != ReaBmsCenSaleDocAndDtlStatus.取消提交.Key && serverEntity.Status.ToString() != ReaBmsCenSaleDocAndDtlStatus.取消审核.Key)
                {
                    return false;
                }
                entity.UserID = empID;
                entity.UserName = empName;
                entity.OperDate = DateTime.Now;
                entity.CheckerID = null;
                entity.Checker = "";
                entity.CheckTime = null;

                tmpa.Add("UserID=" + empID + " ");
                tmpa.Add("UserName='" + empName + "'");
                tmpa.Add("CheckerID=null");
                tmpa.Add("Checker=null");
                tmpa.Add("CheckTime=null");
            }
            #endregion

            #region 确认提交
            if (entity.Status.ToString() == ReaBmsCenSaleDocAndDtlStatus.确认提交.Key)
            {
                if (serverEntity.Status.ToString() != ReaBmsCenSaleDocAndDtlStatus.暂存.Key && serverEntity.Status.ToString() != ReaBmsCenSaleDocAndDtlStatus.取消提交.Key && serverEntity.Status.ToString() != ReaBmsCenSaleDocAndDtlStatus.取消审核.Key)
                {
                    return false;
                }
                entity.UserID = empID;
                entity.UserName = empName;
                entity.OperDate = DateTime.Now;
                entity.CheckerID = null;
                entity.Checker = "";
                entity.CheckTime = null;

                tmpa.Add("UserID=" + entity.UserID + " ");
                tmpa.Add("UserName='" + entity.UserName + "'");
                if (!entity.OperDate.HasValue) tmpa.Add("OperDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("CheckerID=null");
                tmpa.Add("Checker=null");
                tmpa.Add("CheckTime=null");
                tmpa.Add("DataUpdateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            }
            #endregion

            #region 取消提交
            if (entity.Status.ToString() == ReaBmsCenSaleDocAndDtlStatus.取消提交.Key)
            {
                //&& serverEntity.Status.ToString() != ReaBmsCenSaleDocAndDtlStatus.取消审核.Key
                if (serverEntity.Status.ToString() != ReaBmsCenSaleDocAndDtlStatus.确认提交.Key)
                {
                    return false;
                }
                entity.UserID = empID;
                entity.UserName = empName;
                entity.OperDate = DateTime.Now;
                entity.CheckerID = null;
                entity.Checker = "";
                entity.CheckTime = null;

                tmpa.Add("UserID=" + entity.UserID + " ");
                tmpa.Add("UserName='" + entity.UserName + "'");
                if (!entity.OperDate.HasValue) tmpa.Add("OperDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("CheckerID=null");
                tmpa.Add("Checker=null");
                tmpa.Add("CheckTime=null");
                tmpa.Add("DataUpdateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            }
            #endregion

            if (entity.Status.ToString() == ReaBmsCenSaleDocAndDtlStatus.审核通过.Key)
            {
                if (serverEntity.Status.ToString() != ReaBmsCenSaleDocAndDtlStatus.确认提交.Key && serverEntity.Status.ToString() != ReaBmsCenSaleDocAndDtlStatus.取消审核.Key)
                {
                    return false;
                }
                entity.UserID = empID;
                entity.UserName = empName;
                entity.OperDate = DateTime.Now;
                entity.CheckerID = empID;
                entity.Checker = empName;
                entity.CheckTime = DateTime.Now;

                tmpa.Add("CheckerID=" + entity.CheckerID + " ");
                tmpa.Add("Checker='" + entity.Checker + "'");
                tmpa.Add("CheckTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("DataUpdateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            }

            if (entity.Status.ToString() == ReaBmsCenSaleDocAndDtlStatus.取消审核.Key)
            {
                if (serverEntity.Status.ToString() != ReaBmsCenSaleDocAndDtlStatus.确认提交.Key && serverEntity.Status.ToString() != ReaBmsCenSaleDocAndDtlStatus.审核通过.Key)
                {
                    return false;
                }
                //如果供货明细的条码已打印过,不能审核退回
                entity.UserID = empID;
                entity.UserName = empName;
                entity.OperDate = DateTime.Now;
                entity.CheckerID = null;
                entity.Checker = "";
                entity.CheckTime = null;

                tmpa.Add("UserID=" + entity.UserID + " ");
                tmpa.Add("UserName='" + entity.UserName + "'");
                if (!entity.OperDate.HasValue) tmpa.Add("OperDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("CheckerID=null");
                tmpa.Add("Checker=null");
                tmpa.Add("CheckTime=null");
                tmpa.Add("DataUpdateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            }

            if (entity.Status.ToString() == ReaBmsCenSaleDocAndDtlStatus.部分验收.Key || entity.Status.ToString() == ReaBmsCenSaleDocAndDtlStatus.全部验收.Key)
            {
                //&& serverEntity.Status.ToString() != ReaBmsCenSaleDocAndDtlStatus.已上传.Key
                if (serverEntity.Status.ToString() != ReaBmsCenSaleDocAndDtlStatus.审核通过.Key && serverEntity.Status.ToString() != ReaBmsCenSaleDocAndDtlStatus.部分验收.Key)
                {
                    return false;
                }
                entity.UserID = empID;
                entity.UserName = empName;
                entity.OperDate = DateTime.Now;

                tmpa.Add("UserID=" + entity.UserID + " ");
                tmpa.Add("UserName='" + entity.UserName + "'");
                if (!entity.OperDate.HasValue) tmpa.Add("OperDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("DataUpdateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            }
            return true;
        }
        public BaseResultBool EditReaBmsCenSaleDocAndDtlOfConfirm(ReaBmsCenSaleDoc entity, IList<ReaBmsCenSaleDtl> editDtlSaleList, long empID, string empName)
        {
            entity.StatusName = ReaBmsCenSaleDocAndDtlStatus.GetStatusDic()[entity.Status.ToString()].Name;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            entity.OperDate = DateTime.Now;
            entity.UserID = empID;
            entity.UserName = empName;
            this.Entity = entity;
            tempBaseResultBool.success = this.Edit();
            if (tempBaseResultBool.success) AddReaReqOperation(entity, empID, empName);
            return tempBaseResultBool;
        }
        #endregion

        #region PDF清单及统计
        public Stream SearchPdfReportOfTypeById(string reaReportClass, long id, long labID, string labCName, string breportType, string frx, ref string pdfFileName)
        {
            Stream stream = null;
            ReaBmsCenSaleDoc saleDoc = this.Get(id);
            if (saleDoc == null)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取供货清单PDF数据信息为空!");
            }
            IList<ReaBmsCenSaleDtl> dtlList = IBReaBmsCenSaleDtl.SearchListByHQL("reabmscensaledtl.SaleDocID=" + id);
            if (dtlList == null || dtlList.Count <= 0)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取供货清单PDF的明细信息为空!");
            }
            //获取订货单所属供应商的发票信息
            SearchReaCompInfo(saleDoc);

            pdfFileName = saleDoc.SaleDocNo + ".pdf";
            //string milliseconds = "";
            if (reaReportClass == ReaReportClass.Frx.Key)
            {
                //Stopwatch watch = new Stopwatch();
                ////开始计时
                //watch.Start();
                stream = CreatePdfReportOfFrxById(saleDoc, dtlList, frx);
                //watch.Stop();
                //milliseconds = watch.ElapsedMilliseconds.ToString();
                //ZhiFang.Common.Log.Log.Debug("FrxToPDF.Milliseconds:" + milliseconds);
            }
            else if (reaReportClass == ReaReportClass.Excel.Key)
            {
                //Stopwatch watch = new Stopwatch();
                ////开始计时
                //watch.Start();
                string excelFileFullDir = "";
                //获取供货单模板
                if (string.IsNullOrEmpty(frx))
                    frx = "供货清单.xlsx";
                string fileExt = frx.Substring(frx.LastIndexOf("."));
                string excelFile = saleDoc.SaleDocNo.ToString() + fileExt;
                Stream stream2 = null;
                ExportExcelCommand excelCommand = ExportDataToExcelHelp.CreateExportExcelCommand("", "");
                stream2 = ExportDataToExcelHelp.ExportDataToXSSFSheet<ReaBmsCenSaleDoc, ReaBmsCenSaleDtl>(saleDoc, dtlList, excelCommand, breportType, saleDoc.LabID, frx, excelFile, ref excelFileFullDir);
                stream2.Close();
                string pdfFullDir = "";

                bool result = ExcelToPdfReportHelp.ExcelToPDF(excelFileFullDir, breportType, saleDoc.LabID, pdfFileName, ref pdfFullDir);
                if (result)
                {
                    stream = PdfReportHelp.GetReportPDF(pdfFullDir);
                    //watch.Stop();
                    //milliseconds = watch.ElapsedMilliseconds.ToString();
                }
                //ZhiFang.Common.Log.Log.Debug("ExcelToPDF.Milliseconds:" + milliseconds);
            }

            return stream;
        }

        private void SearchReaCompInfo(ReaBmsCenSaleDoc saleDoc)
        {
            ReaCenOrg reaCenOrg = null;
            if (saleDoc.ReaCompID.HasValue)
                reaCenOrg = IDReaCenOrgDao.Get(saleDoc.ReaCompID.Value);
            if (reaCenOrg != null)
            {
                saleDoc.CompBankName = reaCenOrg.BankName;
                saleDoc.CompBankAccount = reaCenOrg.BankAccount;
                saleDoc.CompAddress = reaCenOrg.Address;
                saleDoc.CompTel = reaCenOrg.Tel;
                saleDoc.CompTel1 = reaCenOrg.Tel1;
                saleDoc.CompHotTel = reaCenOrg.HotTel;
                saleDoc.CompHotTel1 = reaCenOrg.HotTel1;
                saleDoc.CompFox = reaCenOrg.Fox;
                saleDoc.CompContact = reaCenOrg.Contact;
                saleDoc.CompEmail = reaCenOrg.Email;
            }
        }

        private Stream CreatePdfReportOfFrxById(ReaBmsCenSaleDoc saleDoc, IList<ReaBmsCenSaleDtl> dtlList, string frx)
        {
            Stream stream = null;
            DataSet dataSet = new DataSet();
            dataSet.DataSetName = "ZhiFang.ReagentSys.Client";

            List<ReaBmsCenSaleDoc> docList = new List<ReaBmsCenSaleDoc>();
            docList.Add(saleDoc);
            DataTable docDt = ReportBTemplateHelp.ToDataTable<ReaBmsCenSaleDoc>(docList, null);
            docDt.TableName = "Rea_BmsCenSaleDoc";
            dataSet.Tables.Add(docDt);

            DataTable dtDtl = ReportBTemplateHelp.ToDataTable<ReaBmsCenSaleDtl>(dtlList, null);
            if (dtDtl != null)
            {
                dtDtl.TableName = "Rea_BmsCenSaleDtl";
                dataSet.Tables.Add(dtDtl);
            }
            //获取供货清单Frx模板
            //string parentPath = ReportBTemplateHelp.GetSaveBTemplatePath(this.Entity.LabID, "供货清单");
            string pdfName = saleDoc.SaleDocNo.ToString() + ".pdf";
            //如果当前实验室还没有维护供货单报表模板,默认使用公共的供货单模板
            if (string.IsNullOrEmpty(frx))
                frx = "供货清单.frx";
            ExportExcelCommand excelCommand = ExportDataToExcelHelp.CreateExportExcelCommand("", "");
            ExcelReportHelp.CreateEECDataTable(excelCommand, ref dataSet);
            stream = FrxToPdfReportHelp.SavePdfReport(dataSet, saleDoc.LabID, pdfName, FrxToPdfReportHelp.PublicTemplateDir, BTemplateType.GetStatusDic()[BTemplateType.供货清单.Key].Name, frx, false);

            return stream;
        }
        public Stream SearchExcelReportOfExcelById(long id, long labID, string labCName, string breportType, string frx, ref string fileName)
        {
            Stream stream = null;
            ReaBmsCenSaleDoc saleDoc = this.Get(id);
            if (saleDoc == null)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取供货清单数据信息为空!");
            }
            IList<ReaBmsCenSaleDtl> dtlList = IBReaBmsCenSaleDtl.SearchListByHQL("reabmscensaledtl.SaleDocID=" + id);
            if (dtlList == null || dtlList.Count <= 0)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取供货清单明细信息为空!");
            }
            //获取供货清单所属供应商的发票信息
            SearchReaCompInfo(saleDoc);

            //获取供货单模板
            if (string.IsNullOrEmpty(frx))
                frx = "供货清单.xlsx";
            string saveFullPath = "";
            string fileExt = frx.Substring(frx.LastIndexOf("."));
            string excelFile = saleDoc.SaleDocNo.ToString() + fileExt;
            ExportExcelCommand excelCommand = ExportDataToExcelHelp.CreateExportExcelCommand("", "");
            stream = ExportDataToExcelHelp.ExportDataToXSSFSheet<ReaBmsCenSaleDoc, ReaBmsCenSaleDtl>(saleDoc, dtlList, excelCommand, breportType, saleDoc.LabID, frx, excelFile, ref saveFullPath);
            fileName = saleDoc.ReaCompanyName + "供货信息" + fileExt;
            return stream;
        }
        #endregion

        #region 公共部分
        /// <summary>
        /// 添加供货操作记录
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        private void AddReaReqOperation(ReaBmsCenSaleDoc entity, long empID, string empName)
        {
            if (entity.Status.ToString() == ReaBmsCenSaleDocAndDtlStatus.暂存.Key) return;

            ReaReqOperation sco = new ReaReqOperation();
            sco.BobjectID = entity.Id;
            if (empID > 0)
                sco.CreatorID = empID;
            if (empName != null && empName.Trim() != "")
                sco.CreatorName = empName;
            sco.BusinessModuleCode = "ReaBmsCenSaleDoc";
            if (!string.IsNullOrEmpty(entity.Memo))
                sco.Memo = entity.Memo;
            sco.IsUse = true;
            sco.Type = entity.Status;
            sco.TypeName = ReaBmsCenSaleDocAndDtlStatus.GetStatusDic()[entity.Status.ToString()].Name;
            IBReaReqOperation.Entity = sco;
            IBReaReqOperation.Add();
        }
        private string GetSaleDocNo()
        {
            StringBuilder strb = new StringBuilder();
            strb.Append(DateTime.Now.ToString("yyMMdd"));
            Random ran = new Random();
            int randKey = ran.Next(0, 999);
            strb.Append(randKey.ToString().PadLeft(3, '0'));//左补零
            strb.Append(DateTime.Now.ToString("HHmmssfff"));
            return strb.ToString();
        }
        #endregion

        #region 客户端与平台同一数据库

        #region 订单转供单
        /// <summary>
        /// 订单转供单(同一数据库)
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        public BaseResultDataValue AddReaBmsCenSaleDocOfOrderToSupply(long orderId, long labID, long empID, string empName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            //获取订单信息
            ReaBmsCenOrderDoc orderDoc = IBReaBmsCenOrderDoc.Get(orderId);
            if (orderDoc.Status.ToString() != ReaBmsOrderDocStatus.供应商确认.Key && orderDoc.IOFlag.ToString() != ReaBmsOrderDocIOFlag.供应商确认.Key && orderDoc.Status.ToString() != ReaBmsOrderDocStatus.订单转供货.Key)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "订单ID：" + orderDoc.Id + "的状态为：" + ReaBmsOrderDocStatus.GetStatusDic()[orderDoc.Status.ToString()].Name + ",数据标志为:" + ReaBmsOrderDocIOFlag.GetStatusDic()[orderDoc.IOFlag.ToString()].Name + "！";
                return baseResultDataValue;
            }

            //供货状态=终止供货、全部供货的，不可再进行转供单操作
            if (orderDoc.SupplyStatus.ToString() == ReaBmsOrderDocSupplyStatus.终止供货.Key || orderDoc.SupplyStatus.ToString() == ReaBmsOrderDocSupplyStatus.全部供货.Key)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "订单ID：" + orderDoc.Id + "的供货状态为：" + ReaBmsOrderDocStatus.GetStatusDic()[orderDoc.Status.ToString()].Name + ",不可再进行订单转供单操作！";
                return baseResultDataValue;
            }

            //是否需要验证订单的供货方的机构所属平台编码是否当前供应商的所属机构平台编码?

            //获取订单明细信息：未供数量>0的试剂，可以生成到供货单里
            IList<ReaBmsCenOrderDtl> orderDtlList = IBReaBmsCenOrderDtl.SearchListByHQL("reabmscenorderdtl.UnSupplyQty is not null and reabmscenorderdtl.UnSupplyQty>0 and reabmscenorderdtl.OrderDocID=" + orderDoc.Id);
            if (orderDtlList.Count <= 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "订单的订货明细信息为空,不能进行订单转供货单!";
                return baseResultDataValue;
            }

            //订单转供货处理
            ReaBmsCenSaleDoc saleDoc = AddOfOrderToSupplyGetSaleDoc(orderDoc, labID, empID, empName);
            IList<ReaBmsCenSaleDtl> saleDtlList = AddOfOrderToSupplyGetSaleDtl(orderDoc, saleDoc, orderDtlList, empID, empName);
            //供货保存
            saleDoc.TotalPrice = saleDtlList.Sum(p => p.SumTotal);
            baseResultDataValue = AddReaBmsCenSaleDocAndDtl(saleDoc, saleDtlList, empID, empName, false);
            if (baseResultDataValue.success == false)
            {
                return baseResultDataValue;
            }
            //订单状态及数据标志更新,添加订单操作记录
            if (baseResultDataValue.success)
                baseResultDataValue = EditReaBmsCenOrderDocAndDt(orderDoc, orderDtlList, empID, empName);
            return baseResultDataValue;
        }
        /// <summary>
        /// 订单转供货时,封装供货总单信息
        /// </summary>
        /// <param name="orderDoc"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        private ReaBmsCenSaleDoc AddOfOrderToSupplyGetSaleDoc(ReaBmsCenOrderDoc orderDoc, long labID, long empID, string empName)
        {
            ReaBmsCenSaleDoc entity = new ReaBmsCenSaleDoc();
            entity.LabID = labID;
            if (entity.Status <= 0) entity.Status = int.Parse(ReaBmsCenSaleDocAndDtlStatus.暂存.Key);
            entity.StatusName = ReaBmsCenSaleDocAndDtlStatus.GetStatusDic()[entity.Status.ToString()].Name;
            entity.IOFlag = int.Parse(ReaBmsCenSaleDocIOFlag.未提取.Key);

            entity.SaleDocNo = GetSaleDocNo();
            entity.UserID = empID;
            entity.UserName = empName;
            entity.OperDate = DateTime.Now;
            entity.OrderDocID = orderDoc.Id;

            entity.OrderDocNo = orderDoc.OrderDocNo;
            if (orderDoc.UrgentFlag.HasValue)
                entity.UrgentFlag = orderDoc.UrgentFlag.Value;
            entity.CenSaleDocID = orderDoc.Id;
            entity.DeptName = orderDoc.DeptName;
            //数据来源
            entity.Source = int.Parse(ReaBmsCenSaleDocSource.供应商.Key);

            //订货方(实验室)
            entity.LabcID = orderDoc.LabcID;
            entity.LabcName = orderDoc.LabcName;
            entity.ReaServerLabcCode = orderDoc.ReaServerLabcCode;
            //供应商
            entity.CompID = orderDoc.CompID;
            entity.CompanyName = orderDoc.CompanyName;
            entity.ReaServerCompCode = orderDoc.ReaServerCompCode;
            entity.ReaCompID = orderDoc.CompID;
            entity.ReaCompanyName = orderDoc.CompanyName;

            entity.ReaCompCode = orderDoc.ReaCompCode;
            entity.LabOrderDocID = orderDoc.LabOrderDocID;
            if (!entity.LabOrderDocID.HasValue)
                entity.LabOrderDocID = orderDoc.Id;
            return entity;
        }
        /// <summary>
        /// 订单转供货时,封装供货明细信息
        ///  1）可以多次进行“订单转供单”的操作；
        ///  2）将未供数量不为0的试剂，全部生成到供货单；
        ///  3）供货状态=终止供货、全部供货的，不可再进行转供单操作；
        /// </summary>
        /// <param name="orderDoc"></param>
        /// <param name="saleDoc"></param>
        /// <param name="orderDtlList"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        private IList<ReaBmsCenSaleDtl> AddOfOrderToSupplyGetSaleDtl(ReaBmsCenOrderDoc orderDoc, ReaBmsCenSaleDoc saleDoc, IList<ReaBmsCenOrderDtl> orderDtlList, long empID, string empName)
        {
            IList<ReaBmsCenSaleDtl> saleDtlList = new List<ReaBmsCenSaleDtl>();
            foreach (var orderDtl in orderDtlList)
            {
                ReaBmsCenSaleDtl saleDtl = new ReaBmsCenSaleDtl();
                saleDtl.PrintCount = 0;
                saleDtl.IOFlag = saleDoc.IOFlag;

                saleDtl.SaleDocID = saleDoc.Id;
                saleDtl.SaleDocNo = saleDoc.SaleDocNo;
                saleDtl.SaleDtlNo = GetSaleDocNo();
                if (saleDtl.Status <= 0) saleDtl.Status = saleDoc.Status;
                saleDtl.StatusName = ReaBmsCenSaleDocAndDtlStatus.GetStatusDic()[saleDoc.Status.ToString()].Name;

                saleDtl.ProdID = orderDtl.ProdID;
                saleDtl.ProdOrgName = orderDtl.ProdOrgName;
                saleDtl.GoodsQty = orderDtl.UnSupplyQty;//供货数默认=未供数量
                saleDtl.Price = orderDtl.Price;
                saleDtl.SumTotal = orderDtl.Price * orderDtl.GoodsQty;

                saleDtl.LabcGoodsLinkID = orderDtl.LabcGoodsLinkID;
                saleDtl.CompGoodsLinkID = orderDtl.CompGoodsLinkID;
                saleDtl.ReaServerCompCode = orderDoc.ReaServerCompCode;
                saleDtl.ReaCompID = orderDoc.ReaCompID;
                saleDtl.ReaCompanyName = orderDoc.ReaCompanyName;

                saleDtl.ReaGoodsID = orderDtl.ReaGoodsID;
                saleDtl.GoodsName = orderDtl.GoodsName;
                saleDtl.ReaGoodsName = orderDtl.ReaGoodsName;
                saleDtl.GoodsUnit = orderDtl.GoodsUnit;
                saleDtl.UnitMemo = orderDtl.UnitMemo;

                saleDtl.IsPrintBarCode = orderDtl.IsPrintBarCode;
                saleDtl.BarCodeType = orderDtl.BarCodeType;
                saleDtl.ReaGoodsNo = orderDtl.ReaGoodsNo;
                saleDtl.ProdGoodsNo = orderDtl.ProdGoodsNo;
                saleDtl.CenOrgGoodsNo = orderDtl.CenOrgGoodsNo;
                saleDtl.GoodsNo = orderDtl.GoodsNo;
                saleDtl.GoodsSort = orderDtl.GoodsSort;

                saleDtl.LabOrderDtlID = orderDtl.LabOrderDtlID;
                if (!saleDtl.LabOrderDtlID.HasValue)
                    saleDtl.LabOrderDtlID = orderDtl.Id;

                saleDtlList.Add(saleDtl);
            }
            return saleDtlList;
        }
        /// <summary>
        /// 订单转供货单后,需要更新订单状态及数据标志更新,添加订单的操作记录
        /// </summary>
        /// <param name="orderDoc">orderDoc为当前事务从数据库获取,可以直接修改其实体属性,其修改的值会在事务自动更新</param>
        /// <param name="orderDtlList">orderDtlList为当前事务从数据库获取,可以直接修改其实体属性,其修改的值会在事务自动更新</param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        private BaseResultDataValue EditReaBmsCenOrderDocAndDt(ReaBmsCenOrderDoc orderDoc, IList<ReaBmsCenOrderDtl> orderDtlList, long empID, string empName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            orderDoc.Status = int.Parse(ReaBmsOrderDocStatus.订单转供货.Key);
            orderDoc.StatusName = ReaBmsOrderDocStatus.GetStatusDic()[orderDoc.Status.ToString()].Name;
            orderDoc.IOFlag = int.Parse(ReaBmsOrderDocIOFlag.订单转供货.Key);
            foreach (var orderDtl in orderDtlList)
            {
                if (orderDoc.IOFlag.HasValue)
                    orderDtl.IOFlag = orderDoc.IOFlag.Value;
            }
            IBReaBmsCenOrderDoc.AddReaReqOperation(orderDoc, empID, empName);
            return baseResultDataValue;
        }
        #endregion

        #region 客户端提取供货单
        public BaseResultBool EditReaBmsCenSaleDocOfExtract(long saleDocId, string reaServerCompCode, string saleDocNo, string reaServerLabcCode, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            //第一种,直接按选择的供货单进行提取
            if (saleDocId > 0)
            {
                tempBaseResultBool = this.EditReaBmsCenSaleDocOfExtractBySaleDocId(saleDocId, reaServerCompCode, saleDocNo, reaServerLabcCode, empID, empName);
            }
            else
            {
                tempBaseResultBool = this.EditReaBmsCenSaleDocOfExtractBySaleDocNo(reaServerCompCode, saleDocNo, reaServerLabcCode, empID, empName);
            }
            return tempBaseResultBool;
        }
        /// <summary>
        /// 按选择的供货单Id进行提取
        /// </summary>
        /// <param name="saleDocId"></param>
        /// <param name="reaCompID"></param>
        /// <param name="reaServerCompCode"></param>
        /// <param name="saleDocNo"></param>
        /// <param name="reaServerLabcCode"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        public BaseResultBool EditReaBmsCenSaleDocOfExtractBySaleDocId(long saleDocId, string reaServerCompCode, string saleDocNo, string reaServerLabcCode, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            ReaBmsCenSaleDoc saleDoc = this.Get(saleDocId);
            if (saleDoc == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "获取供货单ID为:" + saleDocId + "的供货信息为空！";
                return tempBaseResultBool;
            }
            if (saleDoc.Status.ToString() != ReaBmsCenSaleDocAndDtlStatus.审核通过.Key && (saleDoc.IOFlag.ToString() != ReaBmsCenSaleDocIOFlag.未提取.Key || saleDoc.IOFlag.ToString() != ReaBmsCenSaleDocIOFlag.部分提取.Key))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "供货单ID：" + saleDoc.Id + "的状态为：" + ReaBmsOrderDocStatus.GetStatusDic()[saleDoc.Status.ToString()].Name + ",数据标志为:" + ReaBmsOrderDocIOFlag.GetStatusDic()[saleDoc.IOFlag.ToString()].Name + "！";
                return tempBaseResultBool;
            }
            tempBaseResultBool = EditReaBmsCenSaleDocAndDtlOfExtract(saleDoc, reaServerCompCode, saleDocNo, reaServerLabcCode, empID, empName);

            return tempBaseResultBool;
        }
        /// <summary>
        /// 按选择的供供应商+供货单号提取
        /// </summary>
        /// <param name="reaCompID"></param>
        /// <param name="reaServerCompCode"></param>
        /// <param name="saleDocNo"></param>
        /// <param name="reaServerLabcCode"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        public BaseResultBool EditReaBmsCenSaleDocOfExtractBySaleDocNo(string reaServerCompCode, string saleDocNo, string reaServerLabcCode, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (string.IsNullOrEmpty(reaServerCompCode))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "传入参数供货方所属机构平台编码（reaServerCompCode）为空！";
                return tempBaseResultBool;
            }
            if (string.IsNullOrEmpty(saleDocNo))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "传入参数供货单号（saleDocNo）为空！";
                return tempBaseResultBool;
            }
            if (string.IsNullOrEmpty(reaServerLabcCode))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "传入参数订货方所属机构平台编码（reaServerLabcCode）为空！";
                return tempBaseResultBool;
            }
            IList<ReaBmsCenSaleDoc> tempDocList = this.SearchListByHQL(string.Format(" reabmscensaledoc.ReaServerCompCode='{0}' and reabmscensaledoc.SaleDocNo='{1}' and reabmscensaledoc.ReaServerLabcCode='{2}'  and reabmscensaledoc.Status={3} and (reabmscensaledoc.IOFlag={4} or reabmscensaledoc.IOFlag={5})", reaServerCompCode, saleDocNo, reaServerLabcCode, ReaBmsCenSaleDocAndDtlStatus.审核通过.Key, ReaBmsCenSaleDocIOFlag.未提取.Key, ReaBmsCenSaleDocIOFlag.部分提取.Key));

            if (tempDocList.Count != 1)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "供货方所属机构平台编码为：" + reaServerCompCode + ",订货方所属机构平台编码为：" + reaServerLabcCode + ",供货单号为：" + saleDocNo + ",符合提取条件的供货单记录为:" + tempDocList.Count + "条！";
                return tempBaseResultBool;
            }

            //获取提取供货单的供货明细
            ReaBmsCenSaleDoc saleDoc = tempDocList[0];
            if (saleDoc.Status.ToString() != ReaBmsCenSaleDocAndDtlStatus.审核通过.Key && (saleDoc.IOFlag.ToString() != ReaBmsCenSaleDocIOFlag.未提取.Key || saleDoc.IOFlag.ToString() != ReaBmsCenSaleDocIOFlag.部分提取.Key))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "供货单ID：" + saleDoc.Id + "的状态为：" + ReaBmsOrderDocStatus.GetStatusDic()[saleDoc.Status.ToString()].Name + ",数据标志为:" + ReaBmsOrderDocIOFlag.GetStatusDic()[saleDoc.IOFlag.ToString()].Name + "！";
                return tempBaseResultBool;
            }

            tempBaseResultBool = EditReaBmsCenSaleDocAndDtlOfExtract(saleDoc, reaServerCompCode, saleDocNo, reaServerLabcCode, empID, empName);
            if (tempBaseResultBool.success)
            {
                tempBaseResultBool.BoolInfo = "" + saleDoc.Id;
            }
            return tempBaseResultBool;
        }
        /// <summary>
        /// 匹配对照供货明细及更新供货单及明细的状态及数据标志
        /// </summary>
        /// <param name="saleDoc"></param>
        /// <param name="reaCompID"></param>
        /// <param name="reaServerCompCode"></param>
        /// <param name="saleDocNo"></param>
        /// <param name="reaServerLabcCode"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        public BaseResultBool EditReaBmsCenSaleDocAndDtlOfExtract(ReaBmsCenSaleDoc saleDoc, string reaServerCompCode, string saleDocNo, string reaServerLabcCode, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();

            IList<ReaBmsCenSaleDtl> saleDtlList = IBReaBmsCenSaleDtl.SearchListByHQL(string.Format(" reabmscensaledtl.SaleDocID={0}", saleDoc.Id));

            if (saleDtlList.Count <= 0)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "供货方所属机构平台编码为：" + reaServerCompCode + ",订货方所属机构平台编码为：" + reaServerLabcCode + ",供货单号为：" + saleDocNo + ",符合提取条件的供货单供货明细记录为空！";
                return tempBaseResultBool;
            }
            foreach (var saleDtl in saleDtlList)
            {
                //saleDtl.LabcGoodsLinkID = tempLink.ElementAt(0).Id;
                saleDtl.Status = int.Parse(ReaBmsCenSaleDocAndDtlStatus.供货提取.Key);
                saleDtl.StatusName = ReaBmsCenSaleDocAndDtlStatus.GetStatusDic()[saleDtl.Status.ToString()].Name;
                saleDtl.IOFlag = int.Parse(ReaBmsCenSaleDocIOFlag.已提取.Key);
                IBReaBmsCenSaleDtl.Entity = saleDtl;

            }
            if (tempBaseResultBool.success == false)
                return tempBaseResultBool;

            //更新供货单状态及数据标志
            saleDoc.Status = int.Parse(ReaBmsCenSaleDocAndDtlStatus.供货提取.Key);
            saleDoc.StatusName = ReaBmsCenSaleDocAndDtlStatus.GetStatusDic()[saleDoc.Status.ToString()].Name;
            saleDoc.IOFlag = int.Parse(ReaBmsCenSaleDocIOFlag.已提取.Key);
            if (string.IsNullOrEmpty(saleDoc.ReaServerLabcCode))
            {
                saleDoc.ReaServerLabcCode = reaServerLabcCode;
            }
            this.Entity = saleDoc;
            this.Edit();

            AddReaReqOperation(saleDoc, empID, empName);
            return tempBaseResultBool;
        }

        #endregion

        public BaseResultBool EditReaBmsCenSaleDocAndDtlOfConfirm(ReaBmsCenSaleDoc entity, long empID, string empName)
        {
            entity.StatusName = ReaBmsCenSaleDocAndDtlStatus.GetStatusDic()[entity.Status.ToString()].Name;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            entity.OperDate = DateTime.Now;
            entity.UserID = empID;
            entity.UserName = empName;
            this.Entity = entity;
            tempBaseResultBool.success = this.Edit();
            if (tempBaseResultBool.success) AddReaReqOperation(entity, empID, empName);
            return tempBaseResultBool;
        }
        public BaseResultBool SearchLocalReaSaleDocOfConfirmBySaleDocNo(string reaServerCompCode, string saleDocNo, string reaServerLabcCode, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            tempBaseResultBool.BoolFlag = false;
            IList<ReaBmsCenSaleDoc> tempDocList = this.SearchListByHQL(string.Format(" reabmscensaledoc.ReaServerCompCode='{0}' and reabmscensaledoc.SaleDocNo='{1}' and reabmscensaledoc.ReaServerLabcCode='{2}'", reaServerCompCode, saleDocNo, reaServerLabcCode));

            if (tempDocList.Count <= 0)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.BoolFlag = true;
                tempBaseResultBool.ErrorInfo = "供货方所属机构平台编码为：" + reaServerCompCode + ",订货方所属机构平台编码为：" + reaServerLabcCode + ",供货单号为：" + saleDocNo + ",供货记录为空！";
                return tempBaseResultBool;
            }
            // 
            var tempList = tempDocList.Where(p => p.Status == int.Parse(ReaBmsCenSaleDocAndDtlStatus.审核通过.Key) || p.Status == int.Parse(ReaBmsCenSaleDocAndDtlStatus.供货提取.Key) || p.Status == int.Parse(ReaBmsCenSaleDocAndDtlStatus.部分验收.Key));
            if (tempList.Count() != 1)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "供货方所属机构平台编码为：" + reaServerCompCode + ",订货方所属机构平台编码为：" + reaServerLabcCode + ",供货单号为：" + saleDocNo + ",供货记录数为:" + tempList.Count() + "！";
                return tempBaseResultBool;
            }
            tempBaseResultBool.BoolInfo = tempList.ElementAt(0).Id.ToString();

            //if (tempList.ElementAt(0).Status == int.Parse(ReaBmsCenSaleDocAndDtlStatus.审核通过.Key)) {
            //    tempBaseResultBool = EditReaBmsCenSaleDocAndDtlOfExtract(tempList.ElementAt(0), reaServerCompCode, saleDocNo, reaServerLabcCode, empID, empName);
            //}
            return tempBaseResultBool;

        }
        #endregion

        #region 接口写入供货单
        public BaseResultDataValue AddReaBmsCenSaleDocByInterface(string saleDocXML, CenOrg cenOrg, HREmployee emp, ref ReaBmsCenSaleDoc reaSaleDoc, ref IList<ReaBmsCenSaleDtl> reaSaleDtlList)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            StringReader StrStream = null;
            try
            {
                StrStream = new StringReader(saleDocXML);
                XDocument xd = XDocument.Load(StrStream);
                XElement xeRoot = xd.Root;//根目录
                XElement xeBody = xeRoot.Element("Body");
                IList<XElement> xeBodyChild = xeBody.Elements("Master").ToList();//获取供货单列表
                if (xeBodyChild != null && xeBodyChild.Count > 0)
                {
                    if (reaSaleDtlList == null)
                        reaSaleDtlList = new List<ReaBmsCenSaleDtl>();
                    Dictionary<string, ReaGoods> listGoodsNo = new Dictionary<string, ReaGoods>();
                    int orgNo = IDReaCenOrgDao.GetMaxOrgNo();
                    foreach (XElement xeSaleDoc in xeBodyChild)
                    {
                        XElement xeSaleDtl = xeSaleDoc.Element("Detail");//获取供货单子单SaleDtl信息
                        if (xeSaleDtl == null)//获取子单失败或子单信息为空
                            throw new Exception("供货单数据格式错误：供货单明细数据不存在！");
                        IList<XElement> xeSaleDocNodeList = xeSaleDoc.Elements().ToList();
                        ReaBmsCenSaleDoc saleDoc = new ReaBmsCenSaleDoc();
                        XElement saleDtlNode = null;
                        ZhiFang.Common.Log.Log.Info("开始供货单属性赋值！");
                        foreach (XElement saleDocNode in xeSaleDocNodeList)
                        {
                            if (saleDocNode.Name != "Detail")
                            {
                                try
                                {
                                    string propertyName = saleDocNode.Name.ToString();
                                    if (propertyName.ToLower() == "compcode")//兼容原来的文档
                                        propertyName = "ReaCompCode";
                                    System.Reflection.PropertyInfo propertyInfo = saleDoc.GetType().GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                                    if (propertyInfo != null && saleDocNode.Value != null)
                                        propertyInfo.SetValue(saleDoc, ExcelDataCommon.DataConvert(propertyInfo, saleDocNode.Value), null);
                                }
                                catch (Exception ex)
                                {
                                    ZhiFang.Common.Log.Log.Info("供货单属性赋值失败：" + saleDocNode.Name.ToString() + "---" + saleDocNode.Value.ToString() + "。 Error：" + ex.Message);
                                }
                                if (saleDocNode.Name.ToString().ToUpper() == "SALEDOCNO")
                                {
                                    IList<ReaBmsCenSaleDoc> listReaBmsCenSaleDoc = this.SearchListByHQL(" reabmscensaledoc.SaleDocNo=\'" + saleDocNode.Value.ToString() + "\'");
                                    if (listReaBmsCenSaleDoc != null && listReaBmsCenSaleDoc.Count > 0)
                                        throw new Exception("单号为【" + saleDocNode.Value.ToString() + "】的信息已经存在！");
                                }
                            }
                            else
                                saleDtlNode = saleDocNode;
                        }//foreach
                        ZhiFang.Common.Log.Log.Info("结束供货单属性赋值！");
                        saleDoc.LabID = emp.LabID;
                        saleDoc.LabcID = cenOrg.Id;
                        saleDoc.LabcName = cenOrg.CName;
                        saleDoc.ReaLabcCode = cenOrg.OrgNo.ToString();
                        saleDoc.Status = int.Parse(ReaBmsCenSaleDocAndDtlStatus.审核通过.Key);
                        saleDoc.Source = int.Parse(ReaBmsCenSaleDocSource.供应商.Key);
                        saleDoc.IOFlag = int.Parse(ReaBmsCenSaleDocIOFlag.已提取.Key);
                        saleDoc.OtherDocNo = saleDoc.SaleDocNo;//保存第三方供货单号
                        saleDoc.Memo = "供货单接口写入";
                        string employeeID = emp.Id.ToString();
                        if (!string.IsNullOrEmpty(employeeID))
                            saleDoc.UserID = long.Parse(employeeID);
                        saleDoc.UserName = emp.CName;
                        saleDoc.OperDate = DateTime.Now;
                        saleDoc.DataAddTime = DateTime.Now;
                        saleDoc.DataUpdateTime = saleDoc.DataAddTime;
                        if ((string.IsNullOrEmpty(saleDoc.CompanyName)) && (!string.IsNullOrEmpty(saleDoc.ReaCompanyName)))
                            saleDoc.CompanyName = saleDoc.ReaCompanyName;
                        if ((string.IsNullOrEmpty(saleDoc.ReaCompanyName)) && (!string.IsNullOrEmpty(saleDoc.CompanyName)))
                            saleDoc.ReaCompanyName = saleDoc.CompanyName;
                        this.Entity = saleDoc;

                        ReaCenOrg reaCenOrg = null;
                        if (saleDoc.ReaCompCode != null && saleDoc.ReaCompCode.Length > 0)
                            IBReaCenOrg.AddReaCenOrgSyncByInterface(saleDoc, ref reaCenOrg, ref orgNo);
                        ZhiFang.Common.Log.Log.Info("供货单新增操作！");
                        if (this.Add())
                            reaSaleDoc = saleDoc;
                        IList<XElement> xeSaleDtlList = saleDtlNode.Elements("Row").ToList();//获取根目录下子目录列表
                        Dictionary<string, ReaCenOrg> dicReaCenOrg = new Dictionary<string, ReaCenOrg>();
                        if (xeSaleDtlList != null && xeSaleDtlList.Count > 0)
                        {
                            foreach (XElement dtlNode in xeSaleDtlList)
                            {
                                IList<XElement> saleDtlList = dtlNode.Elements().ToList();
                                ReaBmsCenSaleDtl saleDtl = new ReaBmsCenSaleDtl();
                                ZhiFang.Common.Log.Log.Info("开始供货明细单属性赋值！");
                                foreach (XElement dtl in saleDtlList)
                                {
                                    try
                                    {
                                        string propertyName = dtl.Name.ToString();
                                        if (propertyName.ToLower() == "matchcode")//兼容原来的文档
                                            propertyName = "GoodsNo";
                                        if (propertyName.ToLower() == "compcode")//兼容原来的文档
                                            propertyName = "ReaCompCode";
                                        System.Reflection.PropertyInfo propertyInfo = saleDtl.GetType().GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                                        if (propertyInfo != null && dtl.Value != null)
                                            propertyInfo.SetValue(saleDtl, ExcelDataCommon.DataConvert(propertyInfo, dtl.Value), null);
                                    }
                                    catch (Exception ex)
                                    {
                                        ZhiFang.Common.Log.Log.Info("供货子单属性赋值失败：" + dtl.Name.ToString() + "---" + dtl.Value.ToString() + "。 Error：" + ex.Message);
                                    }
                                }
                                ZhiFang.Common.Log.Log.Info("结束供货明细单属性赋值！");
                                saleDtl.LabID = saleDoc.LabID;
                                saleDtl.Status = int.Parse(ReaBmsCenSaleDocAndDtlStatus.审核通过.Key);
                                saleDtl.DataAddTime = DateTime.Now;
                                saleDtl.DataUpdateTime = saleDtl.DataAddTime;
                                saleDtl.SaleDocID = saleDoc.Id;
                                saleDtl.SaleDocNo = saleDoc.SaleDocNo;
                                saleDtl.ReaGoodsNo = saleDtl.GoodsNo;
                                if (string.IsNullOrEmpty(saleDtl.LotNo))
                                    throw new Exception("货品批号【LotNo】信息不能为空！");
                                //if (!saleDtl.InvalidDate.HasValue)
                                //throw new Exception("货品有效期【InvalidDate】信息不能为空！");
                                if ((!string.IsNullOrEmpty(saleDtl.ReaGoodsName)) && (string.IsNullOrEmpty(saleDtl.GoodsName)))
                                    saleDtl.GoodsName = saleDtl.ReaGoodsName;
                                ReaCenOrg reaCenOrgDtl = null;
                                if (saleDtl.ReaCompCode != null && saleDtl.ReaCompCode.Length > 0)
                                {
                                    if (!dicReaCenOrg.ContainsKey(saleDtl.ReaCompCode))
                                    {
                                        try
                                        {
                                            IBReaCenOrg.AddReaCenOrgSyncByInterface(saleDtl, ref reaCenOrgDtl, ref orgNo);
                                            dicReaCenOrg.Add(saleDtl.ReaCompCode, reaCenOrgDtl);
                                        }
                                        catch (Exception ex)
                                        {
                                            ZhiFang.Common.Log.Log.Info("AddReaCenOrgSyncByInterface错误：" + ex.Message);
                                        }
                                    }
                                    else
                                        reaCenOrgDtl = dicReaCenOrg[saleDtl.ReaCompCode];
                                }
                                if (reaCenOrgDtl == null)
                                    reaCenOrgDtl = reaCenOrg;

                                if (reaCenOrgDtl != null)
                                {
                                    saleDtl.ProdOrgNo = reaCenOrgDtl.MatchCode;
                                    saleDtl.ProdOrgName = reaCenOrgDtl.CName;
                                    saleDtl.ReaCompID = reaCenOrgDtl.Id;
                                }
                                IBReaBmsCenSaleDtl.Entity = saleDtl;
                                if (!listGoodsNo.ContainsKey(saleDtl.GoodsNo))//通过接口数据自动增加相应的货品信息
                                {
                                    try 
                                    {
                                        BaseResultData brd = null;
                                        IList<ReaGoods> listReaGoods = new List<ReaGoods>();
                                        listReaGoods.Add(_getReaGoodsBySaleDtl(saleDtl));

                                        ReaGoods reaGoods = null;
                                        brd = IBReaGoods.SaveReaGoodsByMatchInterface(listReaGoods, emp.Id, emp.CName, ref reaGoods);
                                        if (reaCenOrgDtl != null)
                                            reaGoods.ReaCompCode = reaCenOrgDtl.MatchCode;
                                        listGoodsNo.Add(saleDtl.GoodsNo, reaGoods);
                                        if (!brd.success)
                                            ZhiFang.Common.Log.Log.Error("保存物资接口机构货品信息:" + brd.message);
                                        else if (listGoodsNo.ContainsKey(saleDtl.GoodsNo) && listGoodsNo[saleDtl.GoodsNo] != null)
                                        {
                                            saleDtl.ReaGoodsID = listGoodsNo[saleDtl.GoodsNo].Id;
                                            saleDtl.GoodsID = saleDtl.ReaGoodsID;
                                        }
                                        ReaGoodsOrgLink reaGoodsOrgLink = null;
                                        brd = IBReaGoodsOrgLink.SaveReaGoodsOrgLinkByMatchInterface(listReaGoods, reaCenOrgDtl, emp.Id, emp.CName, ref reaGoodsOrgLink);
                                        if (!brd.success)
                                            ZhiFang.Common.Log.Log.Error("保存物资接口供货货品关系信息:" + brd.message);
                                        else if (reaGoodsOrgLink != null)
                                            saleDtl.CompGoodsLinkID = reaGoodsOrgLink.Id;
                                    }
                                    catch (Exception ex)
                                    {
                                        ZhiFang.Common.Log.Log.Info("通过接口数据自动增加相应的货品信息错误：" + ex.Message);
                                    }
                                }
                                else if (listGoodsNo.ContainsKey(saleDtl.GoodsNo) && listGoodsNo[saleDtl.GoodsNo] != null)
                                {
                                    saleDtl.ReaGoodsID = listGoodsNo[saleDtl.GoodsNo].Id;
                                    saleDtl.GoodsID = saleDtl.ReaGoodsID;
                                }
                                ZhiFang.Common.Log.Log.Info("供货明细单新增操作！");
                                if (IBReaBmsCenSaleDtl.Add())
                                {
                                    reaSaleDtlList.Add(IBReaBmsCenSaleDtl.Entity);
                                    try 
                                    {
                                        if (listGoodsNo.ContainsKey(saleDtl.GoodsNo))
                                            _getReaGoodsBarcodeOperation(saleDoc, saleDtl, listGoodsNo[saleDtl.GoodsNo], emp.Id, emp.CName);
                                    }
                                    catch (Exception ex)
                                    {
                                        ZhiFang.Common.Log.Log.Info("通过接口数据自动增加相应的货品信息错误：" + ex.Message);
                                    }
                                }

                            }
                        }
                        else//获取子单失败或子单信息为空
                        {
                            ZhiFang.Common.Log.Log.Error("供货单数据格式错误：供货单明细数据不存在！");
                            throw new Exception("供货单数据格式错误：供货单明细数据不存在！");
                        }
                    }//foreach
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("供货单数据格式错误：供货单数据不存在！");
                    throw new Exception("供货单数据格式错误：供货单数据不存在！");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("供货单数据存储错误：" + ex.Message);
            }
            finally
            {
                //释放资源
                if (StrStream != null)
                {
                    StrStream.Close();
                    StrStream.Dispose();
                }
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue AddReaBmsCenSaleDocByInterface(IList<XElement> saleDocList, IList<XElement> saleDtlList, CenOrg cenOrg, HREmployee emp, ref ReaBmsCenSaleDoc reaSaleDoc, ref IList<ReaBmsCenSaleDtl> reaSaleDtlList, bool isSave)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (saleDocList != null && saleDocList.Count > 0)
                {
                    if (reaSaleDtlList == null)
                        reaSaleDtlList = new List<ReaBmsCenSaleDtl>();
                    Dictionary<string, ReaGoods> listGoodsNo = new Dictionary<string, ReaGoods>();
                    int orgNo = IDReaCenOrgDao.GetMaxOrgNo();
                    foreach (XElement xeSaleDoc in saleDocList)
                    {
                        IList<XElement> xeSaleDocNodeList = xeSaleDoc.Elements().ToList();
                        ReaBmsCenSaleDoc saleDoc = new ReaBmsCenSaleDoc();
                        foreach (XElement saleDocNode in xeSaleDocNodeList)
                        {
                            try
                            {
                                string propertyName = saleDocNode.Name.ToString();
                                if (propertyName.ToLower() == "compcode")//兼容原来的文档
                                    propertyName = "ReaCompCode";
                                System.Reflection.PropertyInfo propertyInfo = saleDoc.GetType().GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                                if (propertyInfo != null && saleDocNode.Value != null)
                                    propertyInfo.SetValue(saleDoc, ExcelDataCommon.DataConvert(propertyInfo, saleDocNode.Value), null);
                            }
                            catch (Exception ex)
                            {
                                ZhiFang.Common.Log.Log.Info("供货单属性赋值失败：" + saleDocNode.Name.ToString() + "---" + saleDocNode.Value.ToString() + "。 Error：" + ex.Message);
                            }
                            if (saleDocNode.Name.ToString().ToUpper() == "SALEDOCNO")
                            {
                                IList<ReaBmsCenSaleDoc> listReaBmsCenSaleDoc = this.SearchListByHQL(" reabmscensaledoc.SaleDocNo=\'" + saleDocNode.Value.ToString() + "\'");
                                if (listReaBmsCenSaleDoc != null && listReaBmsCenSaleDoc.Count > 0)
                                    throw new Exception("单号为【" + saleDocNode.Value.ToString() + "】的信息已经存在！");
                            }
                        }//foreach
                        saleDoc.LabID = emp.LabID;
                        saleDoc.LabcID = cenOrg.Id;
                        saleDoc.LabcName = cenOrg.CName;
                        saleDoc.ReaLabcCode = cenOrg.OrgNo.ToString();
                        saleDoc.Status = int.Parse(ReaBmsCenSaleDocAndDtlStatus.审核通过.Key);
                        saleDoc.Source = int.Parse(ReaBmsCenSaleDocSource.供应商.Key);
                        saleDoc.IOFlag = int.Parse(ReaBmsCenSaleDocIOFlag.已提取.Key);
                        saleDoc.OtherDocNo = saleDoc.SaleDocNo;//保存第三方供货单号
                        saleDoc.Memo = "供货单接口写入";
                        string employeeID = emp.Id.ToString();
                        if (!string.IsNullOrEmpty(employeeID))
                            saleDoc.UserID = long.Parse(employeeID);
                        saleDoc.UserName = emp.CName;
                        saleDoc.OperDate = DateTime.Now;
                        saleDoc.DataAddTime = DateTime.Now;
                        saleDoc.DataUpdateTime = saleDoc.DataAddTime;
                        if ((string.IsNullOrEmpty(saleDoc.CompanyName)) && (!string.IsNullOrEmpty(saleDoc.ReaCompanyName)))
                            saleDoc.CompanyName = saleDoc.ReaCompanyName;
                        if ((string.IsNullOrEmpty(saleDoc.ReaCompanyName)) && (!string.IsNullOrEmpty(saleDoc.CompanyName)))
                            saleDoc.ReaCompanyName = saleDoc.CompanyName;
                        this.Entity = saleDoc;

                        ReaCenOrg reaCenOrg = null;
                        if (saleDoc.ReaCompCode != null && saleDoc.ReaCompCode.Length > 0)
                            IBReaCenOrg.AddReaCenOrgSyncByInterface(saleDoc, ref reaCenOrg, ref orgNo);

                        if (isSave)
                        {
                            if (this.Add())
                                reaSaleDoc = saleDoc;
                        }
                        else
                            reaSaleDoc = saleDoc;
                        Dictionary<string, ReaCenOrg> dicReaCenOrg = new Dictionary<string, ReaCenOrg>();
                        if (saleDtlList != null && saleDtlList.Count > 0)
                        {
                            foreach (XElement dtlNode in saleDtlList)
                            {
                                IList<XElement> dtlList = dtlNode.Elements().ToList();
                                ReaBmsCenSaleDtl saleDtl = new ReaBmsCenSaleDtl();
                                foreach (XElement dtl in dtlList)
                                {
                                    try
                                    {
                                        string propertyName = dtl.Name.ToString();
                                        if (propertyName.ToLower() == "matchcode")//兼容原来的文档
                                            propertyName = "GoodsNo";
                                        if (propertyName.ToLower() == "compcode")//兼容原来的文档
                                            propertyName = "ReaCompCode";
                                        System.Reflection.PropertyInfo propertyInfo = saleDtl.GetType().GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                                        if (propertyInfo != null && dtl.Value != null)
                                            propertyInfo.SetValue(saleDtl, ExcelDataCommon.DataConvert(propertyInfo, dtl.Value), null);
                                    }
                                    catch (Exception ex)
                                    {
                                        ZhiFang.Common.Log.Log.Info("供货子单属性赋值失败：" + dtl.Name.ToString() + "---" + dtl.Value.ToString() + "。 Error：" + ex.Message);
                                    }
                                }
                                saleDtl.LabID = saleDoc.LabID;
                                saleDtl.Status = int.Parse(ReaBmsCenSaleDocAndDtlStatus.审核通过.Key);
                                saleDtl.DataAddTime = DateTime.Now;
                                saleDtl.DataUpdateTime = saleDtl.DataAddTime;
                                saleDtl.SaleDocID = saleDoc.Id;
                                saleDtl.SaleDocNo = saleDoc.SaleDocNo;
                                saleDtl.ReaGoodsNo = saleDtl.GoodsNo;
                                if ((!string.IsNullOrEmpty(saleDtl.ReaGoodsName)) && (string.IsNullOrEmpty(saleDtl.GoodsName)))
                                    saleDtl.GoodsName = saleDtl.ReaGoodsName;
                                ReaCenOrg reaCenOrgDtl = null;
                                if (saleDtl.ReaCompCode != null && saleDtl.ReaCompCode.Length > 0)
                                {
                                    if (!dicReaCenOrg.ContainsKey(saleDtl.ReaCompCode))
                                    {
                                        IBReaCenOrg.AddReaCenOrgSyncByInterface(saleDtl, ref reaCenOrgDtl, ref orgNo);
                                        dicReaCenOrg.Add(saleDtl.ReaCompCode, reaCenOrgDtl);
                                    }
                                    else
                                        reaCenOrgDtl = dicReaCenOrg[saleDtl.ReaCompCode];
                                }
                                if (reaCenOrgDtl == null)
                                    reaCenOrgDtl = reaCenOrg;

                                if (reaCenOrgDtl != null)
                                {
                                    saleDtl.ProdOrgNo = reaCenOrgDtl.MatchCode;
                                    saleDtl.ProdOrgName = reaCenOrgDtl.CName;
                                    saleDtl.ReaCompID = reaCenOrgDtl.Id;
                                }
                                IBReaBmsCenSaleDtl.Entity = saleDtl;
                                if (!listGoodsNo.ContainsKey(saleDtl.GoodsNo))//通过接口数据自动增加相应的货品信息
                                {

                                    BaseResultData brd = null;
                                    IList<ReaGoods> listReaGoods = new List<ReaGoods>();
                                    listReaGoods.Add(_getReaGoodsBySaleDtl(saleDtl));

                                    ReaGoods reaGoods = null;
                                    brd = IBReaGoods.SaveReaGoodsByMatchInterface(listReaGoods, emp.Id, emp.CName, ref reaGoods);
                                    if (reaCenOrgDtl != null)
                                        reaGoods.ReaCompCode = reaCenOrgDtl.MatchCode;
                                    listGoodsNo.Add(saleDtl.GoodsNo, reaGoods);
                                    if (!brd.success)
                                        ZhiFang.Common.Log.Log.Error("保存物资接口机构货品信息:" + brd.message);
                                    else if (listGoodsNo.ContainsKey(saleDtl.GoodsNo) && listGoodsNo[saleDtl.GoodsNo] != null)
                                    {
                                        saleDtl.ReaGoodsID = listGoodsNo[saleDtl.GoodsNo].Id;
                                        saleDtl.GoodsID = saleDtl.ReaGoodsID;
                                    }
                                    ReaGoodsOrgLink reaGoodsOrgLink = null;
                                    brd = IBReaGoodsOrgLink.SaveReaGoodsOrgLinkByMatchInterface(listReaGoods, reaCenOrgDtl, emp.Id, emp.CName, ref reaGoodsOrgLink);
                                    if (!brd.success)
                                        ZhiFang.Common.Log.Log.Error("保存物资接口供货货品关系信息:" + brd.message);
                                    else if (reaGoodsOrgLink != null)
                                        saleDtl.CompGoodsLinkID = reaGoodsOrgLink.Id;
                                }
                                else if (listGoodsNo.ContainsKey(saleDtl.GoodsNo) && listGoodsNo[saleDtl.GoodsNo] != null)
                                {
                                    saleDtl.ReaGoodsID = listGoodsNo[saleDtl.GoodsNo].Id;
                                    saleDtl.GoodsID = saleDtl.ReaGoodsID;
                                }
                                if (isSave)
                                {
                                    if (IBReaBmsCenSaleDtl.Add())
                                    {
                                        reaSaleDtlList.Add(IBReaBmsCenSaleDtl.Entity);
                                        if (listGoodsNo.ContainsKey(saleDtl.GoodsNo))
                                            _getReaGoodsBarcodeOperation(saleDoc, saleDtl, listGoodsNo[saleDtl.GoodsNo], emp.Id, emp.CName);
                                    }
                                }
                                else
                                    reaSaleDtlList.Add(IBReaBmsCenSaleDtl.Entity);
                            }
                        }
                        else//获取子单失败或子单信息为空
                        {
                            ZhiFang.Common.Log.Log.Error("供货单数据格式错误：供货单明细数据不存在！");
                            throw new Exception("供货单数据格式错误：供货单明细数据不存在！");
                        }
                    }//foreach
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("供货单数据格式错误：供货单数据不存在！");
                    throw new Exception("供货单数据格式错误：供货单数据不存在！");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("供货单数据存储错误：" + ex.Message);
                throw ex;
            }
            return baseResultDataValue;
        }

        public BaseResultData AddReaBmsCenSaleDocByInterface(DataSet dsSaleDoc, DataSet dsSaleDtl, ref ReaBmsCenSaleDoc reaSaleDoc, ref IList<ReaBmsCenSaleDtl> reaSaleDtlList)
        {
            BaseResultData baseResultData = new BaseResultData();
            try
            {
                foreach (DataColumn dcDoc in dsSaleDoc.Tables[0].Columns)
                {
                    ZhiFang.Common.Log.Log.Info("供货单视图字段：" + dcDoc.ColumnName);
                    System.Reflection.PropertyInfo propertyInfo = reaSaleDoc.GetType().GetProperty(dcDoc.ColumnName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    if (propertyInfo != null && dsSaleDoc.Tables[0].Rows[0][dcDoc.ColumnName] != null)
                    { 
                        propertyInfo.SetValue(reaSaleDoc, ExcelDataCommon.DataConvert(propertyInfo, dsSaleDoc.Tables[0].Rows[0][dcDoc.ColumnName]), null);
                        ZhiFang.Common.Log.Log.Info("供货单视图字段：" + dcDoc.ColumnName + ",值：" + dsSaleDoc.Tables[0].Rows[0][dcDoc.ColumnName]);
                    }
                }
                reaSaleDoc.Status = int.Parse(ReaBmsCenSaleDocAndDtlStatus.暂存.Key);
                reaSaleDoc.Source = int.Parse(ReaBmsCenSaleDocSource.供应商.Key);
                reaSaleDoc.IOFlag = int.Parse(ReaBmsCenSaleDocIOFlag.已提取.Key);
                reaSaleDoc.OtherDocNo = reaSaleDoc.SaleDocNo;//保存第三方供货单号
                reaSaleDoc.Memo = "供货单视图导入";
                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (!string.IsNullOrEmpty(employeeID))
                    reaSaleDoc.UserID = long.Parse(employeeID);
                reaSaleDoc.UserName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                reaSaleDoc.OperDate = DateTime.Now;
                reaSaleDoc.DataAddTime = DateTime.Now;
                reaSaleDoc.DataUpdateTime = DateTime.Now;
                Dictionary<string, ReaCenOrg> dicReaCenOrg = new Dictionary<string, ReaCenOrg>();
                foreach (DataRow drSaleDtl in dsSaleDtl.Tables[0].Rows)
                {
                    ReaBmsCenSaleDtl saleDtl = new ReaBmsCenSaleDtl();
                    saleDtl.SaleDocID = reaSaleDoc.Id;
                    saleDtl.Status = 0;
                    saleDtl.DataAddTime = DateTime.Now;
                    saleDtl.DataUpdateTime = DateTime.Now;
                    foreach (DataColumn dcDtl in dsSaleDtl.Tables[0].Columns)
                    {
                        ZhiFang.Common.Log.Log.Info("供货单明细视图字段：" + dcDtl.ColumnName);
                        if (dcDtl.ColumnName.ToLower() == "goodsno")//货品ID
                        {
                            ZhiFang.Common.Log.Log.Info("GoodsNo：" + drSaleDtl[dcDtl.ColumnName]);
                            saleDtl.ReaGoodsNo = drSaleDtl[dcDtl.ColumnName].ToString();
                            IList<ReaGoods> listReaGoods = IBReaGoods.SearchListByHQL(" reagoods.MatchCode=\'" + drSaleDtl[dcDtl.ColumnName] + "\'");
                            if (listReaGoods != null && listReaGoods.Count > 0)
                            {
                                saleDtl.GoodsID = listReaGoods[0].Id;
                                saleDtl.ReaGoodsID = listReaGoods[0].Id;
                                saleDtl.GoodsName = listReaGoods[0].CName;
                                saleDtl.ReaGoodsName = listReaGoods[0].CName;
                                saleDtl.ReaGoodsNo = listReaGoods[0].ReaGoodsNo;
                                saleDtl.GoodsNo = listReaGoods[0].GoodsNo;
                                saleDtl.ProdGoodsNo = listReaGoods[0].ProdGoodsNo;
                                saleDtl.CenOrgGoodsNo = listReaGoods[0].ReaGoodsNo;
                            }
                            ZhiFang.Common.Log.Log.Info("ReaGoodsID：" + saleDtl.ReaGoodsID.ToString());
                        }
                        else if (dcDtl.ColumnName.ToLower() == "reacompcode")//供应商
                        {
                            ReaCenOrg reaCenOrg = null;
                            string reaCompCode = drSaleDtl[dcDtl.ColumnName] != null ? drSaleDtl[dcDtl.ColumnName].ToString() : "";
                            ZhiFang.Common.Log.Log.Info("ReaCompCode：" + reaCompCode);
                            if (!dicReaCenOrg.ContainsKey(reaCompCode))
                            {
                                IList<ReaCenOrg> listReaCenOrg = IBReaCenOrg.SearchListByHQL(" reacenorg.MatchCode=\'" + reaCompCode + "\'");
                                if (listReaCenOrg != null && listReaCenOrg.Count > 0)
                                {
                                    reaCenOrg = listReaCenOrg[0];
                                    dicReaCenOrg.Add(reaCompCode, reaCenOrg);
                                }
                            }
                            else
                                reaCenOrg = dicReaCenOrg[reaCompCode];
                            if (reaCenOrg != null)
                            {
                                saleDtl.ReaCompID = reaCenOrg.Id;
                                saleDtl.ProdOrgNo = ((int)reaCenOrg.OrgNo).ToString();
                                saleDtl.ReaCompanyName = reaCenOrg.CName;
                                ZhiFang.Common.Log.Log.Info("ReaCompID：" + saleDtl.ReaCompID.ToString());
                            }
                        }
                        else if (dcDtl.ColumnName.ToLower() != "goodsid" &&
                            dcDtl.ColumnName.ToLower() != "reagoodsid" &&
                            dcDtl.ColumnName.ToLower() != "goodsname" &&
                            dcDtl.ColumnName.ToLower() != "reagoodsname" &&
                            dcDtl.ColumnName.ToLower() != "reagoodsno" &&
                            dcDtl.ColumnName.ToLower() != "goodsno" &&
                            dcDtl.ColumnName.ToLower() != "prodgoodsno" &&
                            dcDtl.ColumnName.ToLower() != "cenorggoodsno" &&
                            dcDtl.ColumnName.ToLower() != "prodorgno" &&
                            dcDtl.ColumnName.ToLower() != "reacompid" &&
                            dcDtl.ColumnName.ToLower() != "reacompanyname"
                            
                            )
                        {
                            try
                            {
                                System.Reflection.PropertyInfo propertyInfo = saleDtl.GetType().GetProperty(dcDtl.ColumnName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                                if (propertyInfo != null && drSaleDtl[dcDtl.ColumnName] != null)
                                {
                                    propertyInfo.SetValue(saleDtl, ExcelDataCommon.DataConvert(propertyInfo, drSaleDtl[dcDtl.ColumnName]), null);
                                    ZhiFang.Common.Log.Log.Info("供货单明细视图字段：" + dcDtl.ColumnName + ",值：" + drSaleDtl[dcDtl.ColumnName]);
                                }
                            }
                            catch (Exception ex)
                            {
                                ZhiFang.Common.Log.Log.Error(dcDtl.ColumnName + "列数据【" + drSaleDtl[dcDtl.ColumnName] + "】转换错误：" + ex.Message);
                            }
                        }
                    }
                    reaSaleDtlList.Add(saleDtl);
                }//foreach
            }
            catch (Exception ex)
            {
                throw new Exception("供货单数据存储错误：" + ex.Message);
            }
            return baseResultData;
        }

        private ReaGoods _getReaGoodsBySaleDtl(ReaBmsCenSaleDtl saleDtl)
        {
            ReaGoods reaGoods = new ReaGoods();
            if (saleDtl.GoodsID != null)
                reaGoods.Id = (long)saleDtl.GoodsID;
            reaGoods.LabID = saleDtl.LabID;
            reaGoods.CName = saleDtl.GoodsName;
            reaGoods.GoodsNo = saleDtl.GoodsNo;
            if (saleDtl.Price != null)
                reaGoods.Price = (double)saleDtl.Price;

            reaGoods.UnitName = saleDtl.GoodsUnit;
            reaGoods.UnitMemo = saleDtl.UnitMemo;
            /// reaGoods.GoodsClass = saleDtl.GoodsClass;
            ///reaGoods.GoodsClassType = saleDtl.GoodsClassType;
            reaGoods.StorageType = saleDtl.StorageType;
            reaGoods.ApproveDocNo = saleDtl.ApproveDocNo;
            reaGoods.RegistNo = saleDtl.RegisterNo;
            reaGoods.RegistNoInvalidDate = saleDtl.RegisterInvalidDate;
            reaGoods.GoodsSort = saleDtl.GoodsSort;
            reaGoods.ProdGoodsNo = saleDtl.ProdGoodsNo;
            reaGoods.ProdOrgName = saleDtl.ProdOrgName;
            reaGoods.ReaGoodsNo = saleDtl.ReaGoodsNo;
            reaGoods.ReaCompanyName = saleDtl.ReaCompanyName;
            reaGoods.MatchCode = saleDtl.ReaGoodsNo;
            reaGoods.BarCodeMgr = (int)saleDtl.BarCodeType;
            return reaGoods;
        }

        public BaseResultDataValue AddReaBmsCenSaleDocAndDtl(ReaBmsCenSaleDoc saleDoc, IList<ReaBmsCenSaleDtl> listSaleDtl, long empID, string empName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (saleDoc == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "供货主单信息不能为空!";
                return baseResultDataValue;
            }
            if (listSaleDtl == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "供货子单信息不能为空!";
                return baseResultDataValue;
            }

            if (saleDoc.Status <= 0) saleDoc.Status = int.Parse(ReaBmsCenSaleDocAndDtlStatus.确认提交.Key);
            saleDoc.StatusName = ReaBmsCenSaleDocAndDtlStatus.GetStatusDic()[saleDoc.Status.ToString()].Name;

            saleDoc.UserID = empID;
            saleDoc.UserName = empName;
            saleDoc.OperDate = DateTime.Now;
            if (!saleDoc.TotalPrice.HasValue && listSaleDtl != null)
                saleDoc.TotalPrice = listSaleDtl.Sum(p => p.SumTotal);
            this.Entity = saleDoc;
            if (this.Add())
            {
                int dispOrder = 1;
                foreach (var saleDtl in listSaleDtl)
                {
                    saleDtl.LabID = saleDoc.LabID;
                    saleDtl.SaleDocID = saleDoc.Id;
                    saleDtl.Status = saleDoc.Status;
                    saleDtl.StatusName = saleDoc.StatusName;
                    saleDtl.SaleDocNo = saleDoc.SaleDocNo;
                    saleDtl.DataUpdateTime = DateTime.Now;
                    saleDtl.DispOrder = dispOrder;
                    IBReaBmsCenSaleDtl.Entity = saleDtl;
                    IBReaBmsCenSaleDtl.Add();
                    dispOrder++;
                }
                AddReaReqOperation(this.Entity, empID, empName);
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "新增供货单失败！";
            }
            return baseResultDataValue;
        }
        private ReaGoodsBarcodeOperation _getReaGoodsBarcodeOperation(ReaBmsCenSaleDoc saleDoc, ReaBmsCenSaleDtl saleDtl, ReaGoods reaGoods, long empID, string empName)
        {
            if (saleDtl == null || reaGoods == null) return null;
            if ((reaGoods.BarCodeMgr != int.Parse(ReaGoodsBarCodeType.盒条码.Key)) ||
                (saleDtl.BarCodeType != int.Parse(ReaGoodsBarCodeType.盒条码.Key))) return null;

            ReaGoodsBarcodeOperation barcode = new ReaGoodsBarcodeOperation();
            barcode.LabID = saleDtl.LabID;
            barcode.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
            barcode.BDocID = saleDtl.SaleDocID;
            barcode.BDocNo = saleDtl.SaleDocNo;
            barcode.BDtlID = saleDtl.Id;
            barcode.BarcodeCreatType = long.Parse(ReaGoodsBarcodeOperationSerialType.条码生成.Key);
            barcode.OperTypeID = long.Parse(ReaGoodsBarcodeOperType.供货.Key);
            barcode.OperTypeName = ReaGoodsBarcodeOperType.GetStatusDic()[barcode.OperTypeID.ToString()].Name;
            barcode.Visible = true;
            barcode.CreaterID = empID;
            barcode.CreaterName = empName;
            barcode.DataUpdateTime = DateTime.Now;
            barcode.GoodsID = saleDtl.GoodsID;
            barcode.ScanCodeGoodsID = saleDtl.GoodsID;
            barcode.GoodsCName = saleDtl.GoodsName;
            barcode.GoodsUnit = saleDtl.GoodsUnit;
            barcode.GoodsLotID = saleDtl.GoodsLotID;
            barcode.LotNo = saleDtl.LotNo;
            barcode.ReaCompanyID = saleDoc.ReaCompID;
            barcode.CompanyName = saleDoc.CompanyName;
            barcode.GoodsQty = saleDtl.GoodsQty;
            barcode.UnitMemo = saleDtl.UnitMemo;
            barcode.DispOrder = 1;
            barcode.PUsePackSerial = barcode.Id.ToString();

            barcode.ReaGoodsNo = saleDtl.ReaGoodsNo;
            barcode.ProdGoodsNo = saleDtl.ProdGoodsNo;
            barcode.CenOrgGoodsNo = saleDtl.CenOrgGoodsNo;
            barcode.GoodsNo = saleDtl.GoodsNo;
            barcode.ReaCompCode = saleDoc.ReaCompCode;

            barcode.GoodsSort = saleDtl.GoodsSort;
            barcode.CompGoodsLinkID = saleDtl.CompGoodsLinkID;
            barcode.BarCodeType = reaGoods.BarCodeMgr;
            if (!barcode.MinBarCodeQty.HasValue) barcode.MinBarCodeQty = reaGoods.GonvertQty;
            if (barcode.MinBarCodeQty <= 0) barcode.MinBarCodeQty = 1;
            barcode.ScanCodeGoodsID = reaGoods.Id;
            barcode.OverageQty = barcode.MinBarCodeQty;
            barcode.UsePackSerial = saleDtl.LotSerial;
            barcode.UsePackQRCode = saleDtl.LotQRCode;
            barcode.OtherPackSerial = saleDtl.LotSerial;
            barcode.Memo = "第三方系统条码;" + barcode.Memo;
            IDReaGoodsBarcodeOperationDao.SaveByEntity(barcode);
            return barcode;
        }

        /// <summary>
        /// 货品-盒条码明细合并
        /// </summary>
        /// <param name="reaSaleDoc"></param>
        /// <param name="reaSaleDtlList"></param>
        /// <param name="saleDtlList"></param>
        /// <returns></returns>
        public BaseResultDataValue AddReaBmsCenSaleDtlMerge(ref ReaBmsCenSaleDoc reaSaleDoc, ref IList<ReaBmsCenSaleDtl> reaSaleDtlList)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            IList<ReaBmsCenSaleDtl> saleDtlList = new List<ReaBmsCenSaleDtl>();
            ZhiFang.Common.Log.Log.Info("开始合并供货单的盒条码数据！");
            //获取到当前供货单的盒条码信息
            IList<ReaGoodsBarcodeOperation> barcodeList = IDReaGoodsBarcodeOperationDao.GetListByHQL("reagoodsbarcodeoperation.BDocID=" + reaSaleDoc.Id);
            if (reaSaleDtlList != null && reaSaleDtlList.Count > 0)
            { 
                int groupbyType = 1;//可以参数控制，暂时没有设置参数
                if (groupbyType == 1)//按GoodId分组
                {
                    var lisItem = from bmsCenSaleDtl in reaSaleDtlList
                                  group bmsCenSaleDtl by
                                      new
                                      {
                                          GoodId = bmsCenSaleDtl.GoodsID,
                                      } into g
                                  select new
                                  {
                                      GoodId = g.Key.GoodId,
                                      TotalCount = g.Sum(c => c.GoodsQty),
                                      TotalPrice = g.Sum(c => c.Price * c.GoodsQty),
                                      GoodsCount = g.Count()
                                  };
                    foreach (var item in lisItem)
                    {
                        if (item.GoodsCount > 1)
                        {
                            IList<ReaBmsCenSaleDtl> tempList = reaSaleDtlList.Where(p => p.GoodsID == item.GoodId).ToList();
                            tempList[0].GoodsQty = item.TotalCount;
                            tempList[0].SumTotal = item.TotalPrice;
                            tempList[0].GoodsSerial = "";
                            tempList[0].LotSerial = "";
                            saleDtlList.Add(tempList[0]);
                            //更新合并前的供货明细货品对应的盒条码的供货明细Id为tempList[0]的Id
                            if (barcodeList != null && barcodeList.Count > 0)
                                _updateBarcodeOperation(barcodeList, tempList, tempList[0]);
                        }
                    }
                }
                else
                {
                    var lisItem = from bmsCenSaleDtl in reaSaleDtlList
                                  group bmsCenSaleDtl by
                                      new
                                      {
                                          GoodsNo = bmsCenSaleDtl.GoodsNo,
                                          GoodsUnit = bmsCenSaleDtl.GoodsUnit,
                                          GoodsLotNo = bmsCenSaleDtl.LotNo
                                      } into g
                                  select new
                                  {
                                      GoodsNo = g.Key.GoodsNo,
                                      GoodsUnit = g.Key.GoodsUnit,
                                      GoodsLotNo = g.Key.GoodsLotNo,
                                      TotalCount = g.Sum(c => c.GoodsQty),
                                      TotalPrice = g.Sum(c => c.Price * c.GoodsQty),
                                      GoodsCount = g.Count()
                                  };
                    foreach (var item in lisItem)
                    {
                        if (item.GoodsCount > 1)
                        {
                            IList<ReaBmsCenSaleDtl> tempList = reaSaleDtlList.Where(p => p.GoodsNo == item.GoodsNo && p.GoodsUnit == item.GoodsUnit && p.LotNo == item.GoodsLotNo).ToList();
                            tempList[0].GoodsQty = item.TotalCount;
                            tempList[0].SumTotal = item.TotalPrice;
                            tempList[0].GoodsSerial = "";
                            tempList[0].LotSerial = "";
                            saleDtlList.Add(tempList[0]);
                            //更新合并前的供货明细货品对应的盒条码的供货明细Id为tempList[0]的Id
                            if (barcodeList != null && barcodeList.Count > 0)
                                _updateBarcodeOperation(barcodeList, tempList, tempList[0]);
                        }
                    }
                }
            }
            if (saleDtlList != null && saleDtlList.Count > 0)
            {
                foreach (var saleDtl in saleDtlList)
                {
                    IBReaBmsCenSaleDtl.Entity = saleDtl;
                    IBReaBmsCenSaleDtl.Edit();
                    IBReaBmsCenSaleDtl.DeleteByHql("From ReaBmsCenSaleDtl reabmscensaledtl " +
                        " where reabmscensaledtl.LabID=" + saleDtl.LabID +
                        " and reabmscensaledtl.SaleDocID = " + saleDtl.SaleDocID +
                        " and reabmscensaledtl.GoodsID = " + saleDtl.GoodsID +
                        " and reabmscensaledtl.Id <> " + saleDtl.Id);
                    //this.DBDao.UpdateByHql(" update ReaGoodsBarcodeOperation reagoodsbarcodeoperation " +
                    //                       " set reagoodsbarcodeoperation.BDtlID=" + saleDtl.Id + 
                    //                       " where reagoodsbarcodeoperation.BDocID=" + saleDtl.SaleDocID +
                    //                       " and reagoodsbarcodeoperation.GoodsID=" + saleDtl.GoodsID);
                }     
            }
            ZhiFang.Common.Log.Log.Info("结束合并供货单的盒条码数据！");
            return baseResultDataValue;
        }

        /// <summary>
        /// 更新合并前的供货明细货品对应的盒条码的供货明细Id为saleDtl的Id
        /// </summary>
        /// <param name="barcodeList">供货单的所有盒条码集合</param>
        /// <param name="tempList">需要合并的供货明细集合</param>
        private void _updateBarcodeOperation(IList<ReaGoodsBarcodeOperation> barcodeList, IList<ReaBmsCenSaleDtl> tempList, ReaBmsCenSaleDtl saleDtl)
        {
            foreach (var saleDtl2 in tempList)
            {
                var tempBarcodeList = barcodeList.Where(p => p.BDtlID == saleDtl2.Id);
                if (tempBarcodeList != null && tempBarcodeList.Count() > 0)
                {
                    foreach (var barcode in tempBarcodeList)
                    {
                        barcode.BDtlID = saleDtl.Id;
                        IDReaGoodsBarcodeOperationDao.Update(barcode);
                    }
                }
            }
        }
        #endregion
        
        #region 客户端与平台不在同一数据库--客户端部分
        public BaseResultDataValue AddSaleDocAndDtlOfPlatformExtract(string labcCode, string compCode, long saleDocId, string saleDocNo, long empID, string empName, JObject jresult)
        {
            BaseResultDataValue baseresultdata = new BaseResultDataValue();
            //供货主单信息
            ReaBmsCenSaleDoc saleDoc = JsonHelper.JsonToObject<ReaBmsCenSaleDoc>(jresult, "saleDoc");
            //供货明细信息
            IList<ReaBmsCenSaleDtl> saleDtlList = JsonHelper.JsonToObjectList<ReaBmsCenSaleDtl>(jresult, "saleDtlList");
            //供货货品盒条码信息
            IList<ReaGoodsBarcodeOperation> operationList = JsonHelper.JsonToObjectList<ReaGoodsBarcodeOperation>(jresult, "barcodeOperationlList");

            if (saleDoc == null)
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "转换供货主单信息为空!";
                return baseresultdata;
            }
            if (saleDtlList == null || saleDtlList.Count < 0)
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "转换供货明细信息为空!";
                return baseresultdata;
            }

            //订货方所属机构平台编码
            string reaServerLabcCode = saleDoc.ReaServerLabcCode;
            //供货方所属机构平台编码
            string reaServerCompCode = saleDoc.ReaServerCompCode;
            //供货单数据来源转换处理
            saleDoc.Source = int.Parse(ReaBmsCenSaleDocSource.供应商.Key);
            if (!saleDoc.LabOrderDocID.HasValue)
                saleDoc.LabOrderDocID = saleDoc.Id;
            //供货单的状态转换处理
            if (saleDoc.Status <= 0) saleDoc.Status = int.Parse(ReaBmsCenSaleDocAndDtlStatus.供货提取.Key);
            saleDoc.StatusName = ReaBmsCenSaleDocAndDtlStatus.GetStatusDic()[saleDoc.Status.ToString()].Name;
            //供货单的标志转换处理
            saleDoc.IOFlag = int.Parse(ReaBmsCenSaleDocIOFlag.已提取.Key);

            #region 客户端的订货方机构信息
            IList<CenOrg> labcCenOrgList = IDCenOrgDao.GetListByHQL("cenorg.Visible=1 and cenorg.OrgNo=" + reaServerLabcCode);
            if (labcCenOrgList == null || labcCenOrgList.Count < 0)
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "获取供货单的订货方机构信息(CenOrg)信息为空!";
                ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                return baseresultdata;
            }
            else if (labcCenOrgList.Count > 1)
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "获取供货单的订货方机构信息的机构(CenOrg)信息存在多个!";
                ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                return baseresultdata;
            }
            //供货方机构信息
            CenOrg labcCenOrg = labcCenOrgList[0];
            saleDoc.LabID = labcCenOrg.LabID;
            //订货方转换处理:订货方取cenorg的OrgNo及CName
            saleDoc.LabcID = labcCenOrg.Id;
            saleDoc.ReaLabcCode = labcCenOrg.OrgNo.ToString();
            saleDoc.LabcName = labcCenOrg.CName;

            saleDoc.ReaServerLabcCode = labcCenOrg.OrgNo.ToString();
            ZhiFang.Common.Log.Log.Debug("客户端的订货方机构信息Id(LabID):" + labcCenOrg.LabID);
            ZhiFang.Common.Log.Log.Debug("客户端的订货方机构信息Id(CenOrg):" + labcCenOrg.Id);
            #endregion

            #region 客户端的供货方信息
            //客户端的供货方信息
            IList<ReaCenOrg> compReaCenOrgList = IDReaCenOrgDao.GetListByHQL(string.Format("reacenorg.Visible=1 and reacenorg.PlatformOrgNo={0} and reacenorg.LabID={1}", reaServerCompCode, labcCenOrg.LabID));
            if (compReaCenOrgList == null || compReaCenOrgList.Count < 0)
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "获取客户端的供货方信息(ReaCenOrg)为空!";
                ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                return baseresultdata;
            }
            else if (compReaCenOrgList.Count > 1)
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "获取客户端的供货方信息(ReaCenOrg)存在多个!";
                ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                return baseresultdata;
            }
            //客户端的供货方信息
            ReaCenOrg compCenOrg = compReaCenOrgList[0];
            //供应商转换处理
            saleDoc.CompID = compCenOrg.Id;
            saleDoc.CompanyName = compCenOrg.CName;
            saleDoc.ReaCompID = compCenOrg.Id;
            saleDoc.ReaCompanyName = compCenOrg.CName;
            if (compCenOrg.PlatformOrgNo.HasValue)
                saleDoc.ReaServerCompCode = compCenOrg.PlatformOrgNo.ToString();
            #endregion

            #region 客户端的供货方货品信息
            IList<ReaGoodsOrgLink> orglinkList = IDReaGoodsOrgLinkDao.GetListByHQL(string.Format("reagoodsorglink.Visible=1 and reagoodsorglink.CenOrg.Id={0} and reagoodsorglink.CenOrg.OrgType={1} and reagoodsorglink.LabID={2}", compCenOrg.Id, ReaCenOrgType.供货方.Key, labcCenOrg.LabID));
            //供货明细货品的供货方货品关系集合
            IList<ReaGoodsOrgLink> saleDtllinkList = new List<ReaGoodsOrgLink>();
            if (orglinkList == null || orglinkList.Count < 0)
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "获取客户端的所属供货方货品信息(ReaGoodsOrgLink)为空!";
                ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                return baseresultdata;
            }
            //供货明细货品转换处理
            for (int i = 0; i < saleDtlList.Count; i++)
            {
                ReaBmsCenSaleDtl saleDtl = saleDtlList[i];

                //判断是否存在供货商货品编码
                if (string.IsNullOrEmpty(saleDtl.CenOrgGoodsNo))
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "供货明细的货品为:" + saleDtl.ReaGoodsName + ",供货商货品编码为空!";
                    ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                    return baseresultdata;
                }

                //供货明细的货品转换为客户端的供货方货品信息
                ReaGoodsOrgLink orglink = null;
                var tempOrgLinkList = orglinkList.Where(p => p.CenOrgGoodsNo == saleDtl.CenOrgGoodsNo);
                if (tempOrgLinkList == null || tempOrgLinkList.Count() <= 0)
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "(供货明细)供货明细的供应商货品编码为:" + saleDtl.CenOrgGoodsNo + ",货品平台编码为:" + saleDtl.GoodsNo + ",的供货方货品关系不存在!";
                    ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                    return baseresultdata;
                }
                else if (tempOrgLinkList.Count() > 1)
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "(供货明细)供货明细的供应商货品编码为:" + saleDtl.CenOrgGoodsNo + ",条码类型为:" + saleDtl.BarCodeType + ",的供货方货品关系存在多个,请重新维护再后再提取!";
                    ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                    return baseresultdata;
                }
                else
                {
                    //供货方货品关系信息
                    orglink = tempOrgLinkList.ElementAt(0);
                    if (!saleDtllinkList.Contains(orglink))
                        saleDtllinkList.Add(orglink);
                }
                if (orglink == null)
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "(供货明细)供货明细的供应商货品编码为:" + saleDtl.CenOrgGoodsNo + "转换为平台供货商的订货方货品信息为空!";
                    ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                    return baseresultdata;
                }
                GetSaleDtl(labcCenOrg, orglink, saleDoc, ref saleDtl);
                saleDtlList[i] = saleDtl;
            }
            #endregion

            #region 供货货品的盒条码货品信息转换处理
            if (operationList != null && operationList.Count > 0)
            {
                for (int i = 0; i < operationList.Count; i++)
                {
                    ReaGoodsBarcodeOperation operation = operationList[i];
                    ReaGoodsOrgLink orglink = null;
                    var tempOrgLinkList = saleDtllinkList.Where(p => p.CenOrgGoodsNo == operation.CenOrgGoodsNo);
                    if (tempOrgLinkList == null || tempOrgLinkList.Count() <= 0)
                    {
                        baseresultdata.success = false;
                        baseresultdata.ErrorInfo = "(货品条码)供货明细的供应商货品编码为:" + operation.CenOrgGoodsNo + ",货品平台编码为:" + operation.GoodsNo + ",的供货方货品关系不存在!";
                        ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                        return baseresultdata;
                    }
                    else if (tempOrgLinkList.Count() > 1)
                    {
                        baseresultdata.success = false;
                        baseresultdata.ErrorInfo = "(货品条码)供货明细的供应商货品编码为:" + operation.CenOrgGoodsNo + ",条码类型为:" + operation.BarCodeType + ",的供货方货品关系存在多个,请重新维护再后再提取!";
                        ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                        return baseresultdata;
                    }
                    else
                    {
                        //供货方货品关系信息
                        orglink = tempOrgLinkList.ElementAt(0);
                    }
                    if (orglink == null)
                    {
                        baseresultdata.success = false;
                        baseresultdata.ErrorInfo = "(货品条码)供货明细的供应商货品编码为:" + operation.CenOrgGoodsNo + "转换为平台供货商的订货方货品信息为空!";
                        ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                        return baseresultdata;
                    }
                    ReaBmsCenSaleDtl saleDtl = saleDtlList.Where(p => p.Id == operation.BDtlID).ElementAt(0);
                    GetBarcodeOperation(labcCenOrg, orglink, saleDoc, saleDtl, empID, empName, ref operation);
                    operationList[i] = operation;
                }
            }
            #endregion

            this.Entity = saleDoc;
            bool result = this.Add();
            if (!result)
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "保存供货主单信息失败!";
                return baseresultdata;
            }
            //供货明细保存
            baseresultdata = IBReaBmsCenSaleDtl.AddDtlListOfPlatformExtract(ref saleDtlList, empID, empName);
            if (!baseresultdata.success)
                return baseresultdata;

            if (operationList != null && operationList.Count > 0)
            {
                baseresultdata = IBReaGoodsBarcodeOperation.AddBarcodeOperationListOfPlatformExtract(saleDtlList, operationList, empID, empName);
                if (!baseresultdata.success)
                    return baseresultdata;
            }
            //供货主单操作记录
            AddReaReqOperation(saleDoc, empID, empName);
            return baseresultdata;
        }
        //将平台供货明细的货品信息转换为客户端的供货方货品信息
        private void GetSaleDtl(CenOrg labcCenOrg, ReaGoodsOrgLink orglink, ReaBmsCenSaleDoc saleDoc, ref ReaBmsCenSaleDtl saleDtl)
        {
            saleDtl.LabID = labcCenOrg.LabID;
            saleDtl.SaleDocID = saleDoc.Id;
            saleDtl.SaleDocNo = saleDoc.OrderDocNo;
            saleDtl.IOFlag = saleDoc.IOFlag;
            saleDtl.Status = saleDoc.Status;
            saleDtl.StatusName = saleDoc.StatusName;
            saleDtl.DataUpdateTime = DateTime.Now;
            saleDtl.BarCodeType = orglink.BarCodeType;
            saleDtl.IsPrintBarCode = orglink.IsPrintBarCode;
            saleDtl.ProdOrgNo = orglink.ProdGoodsNo;
            saleDtl.ReaGoodsID = orglink.ReaGoods.Id;
            saleDtl.ReaGoodsName = orglink.ReaGoods.CName;
            saleDtl.GoodsID = orglink.ReaGoods.Id;
            saleDtl.GoodsName = orglink.ReaGoods.CName;
            saleDtl.ReaGoodsNo = orglink.ReaGoods.ReaGoodsNo;
            saleDtl.GoodsNo = orglink.ReaGoods.GoodsNo;
            saleDtl.GoodsUnit = orglink.ReaGoods.UnitName;
            saleDtl.UnitMemo = orglink.ReaGoods.UnitMemo;
            saleDtl.GoodsSort = orglink.ReaGoods.GoodsSort;
            //供应商货品机构关系ID
            saleDtl.CompGoodsLinkID = orglink.Id;
            saleDtl.LabcGoodsLinkID = orglink.Id;
            saleDtl.DeleteFlag = 0;
            //实验室货品机构关系ID
            //orderDtl.LabcGoodsLinkID = orglink.ReaGoods.CName;
        }
        //将平台供货明细的货品信息转换为客户端的供货方货品信息
        private void GetBarcodeOperation(CenOrg labcCenOrg, ReaGoodsOrgLink orglink, ReaBmsCenSaleDoc saleDoc, ReaBmsCenSaleDtl saleDtl, long empID, string empName, ref ReaGoodsBarcodeOperation operation)
        {
            operation.LabID = labcCenOrg.LabID;
            operation.BDocID = saleDoc.Id;
            operation.BDocNo = saleDoc.SaleDocNo;
            operation.BDtlID = saleDtl.Id;
            operation.BarCodeType = orglink.BarCodeType;
            //供应商货品机构关系ID
            operation.CompGoodsLinkID = orglink.Id;
            operation.BarcodeCreatType = long.Parse(ReaGoodsBarcodeOperationSerialType.条码生成.Key);
            operation.OperTypeID = long.Parse(ReaGoodsBarcodeOperType.供货.Key);
            operation.OperTypeName = ReaGoodsBarcodeOperType.GetStatusDic()[operation.OperTypeID.ToString()].Name;
            operation.Visible = true;
            operation.CreaterID = empID;
            operation.CreaterName = empName;
            operation.DataUpdateTime = DateTime.Now;
            operation.GoodsID = saleDtl.GoodsID;
            operation.ScanCodeGoodsID = saleDtl.GoodsID;
            operation.GoodsCName = saleDtl.GoodsName;
            operation.GoodsUnit = saleDtl.GoodsUnit;
            operation.GoodsLotID = saleDtl.GoodsLotID;
            operation.LotNo = saleDtl.LotNo;
            operation.ReaCompanyID = saleDoc.ReaCompID;
            operation.CompanyName = saleDoc.CompanyName;
            operation.GoodsQty = saleDtl.GoodsQty;
            operation.UnitMemo = saleDtl.UnitMemo;
            operation.PUsePackSerial = operation.Id.ToString();
            operation.ReaGoodsNo = saleDtl.ReaGoodsNo;
            operation.ProdGoodsNo = saleDtl.ProdGoodsNo;
            operation.CenOrgGoodsNo = saleDtl.CenOrgGoodsNo;
            operation.GoodsNo = saleDtl.GoodsNo;
            operation.ReaCompCode = saleDoc.ReaCompCode;
            operation.GoodsSort = saleDtl.GoodsSort;
            operation.BarCodeType = orglink.ReaGoods.BarCodeMgr;
            if (!operation.MinBarCodeQty.HasValue) operation.MinBarCodeQty = orglink.ReaGoods.GonvertQty;
            if (operation.MinBarCodeQty <= 0) operation.MinBarCodeQty = 1;
            operation.ScanCodeGoodsID = orglink.ReaGoods.Id;
            operation.OverageQty = operation.MinBarCodeQty;
            if (string.IsNullOrEmpty(operation.SysPackSerial))
                operation.SysPackSerial = operation.UsePackQRCode;
            if (string.IsNullOrEmpty(operation.SysPackSerial))
                operation.SysPackSerial = operation.UsePackSerial;
        }
        #endregion

        #region 客户端与平台不在同一数据库--平台部分
        public BaseResultDataValue GetPlatformSaleDocAndDtlToClient(string labcCode, string compCode, long saleDocId, string saleDocNo, ref JObject jPostData)
        {
            BaseResultDataValue baseresultdata = new BaseResultDataValue();

            #region 验证判断
            //验证机构信息
            if (string.IsNullOrEmpty(saleDocNo))
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "供货单号(saleDocNo)为空!";
                ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                return baseresultdata;
            }
            if (string.IsNullOrEmpty(labcCode))
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "订货方所属机构平台编码(labcCode)为空!";
                ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                return baseresultdata;
            }
            if (string.IsNullOrEmpty(compCode))
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "供货方所属机构平台编码(compCode)为空!";
                ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                return baseresultdata;
            }
            #endregion

            ReaBmsCenSaleDoc saleDoc = this.Get(saleDocId);
            //供货单是否存在
            if (saleDoc == null)
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "供货单号为:" + saleDocNo + ",获取供货信息为空!";
                ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                return baseresultdata;
            }
            //判断供货单是否可以被提取
            if (saleDoc.Status != int.Parse(ReaBmsCenSaleDocAndDtlStatus.审核通过.Key))
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "供货单号为:" + saleDocNo + ",当前供货状态为:" + ReaBmsCenSaleDocAndDtlStatus.GetStatusDic()[saleDoc.Status.ToString()].Name + ",不能提取!";
                ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                return baseresultdata;
            }
            IList<ReaBmsCenSaleDtl> saleDtlList = IBReaBmsCenSaleDtl.SearchListByHQL("reabmscensaledtl.SaleDocID=" + saleDoc.Id);
            if (saleDtlList == null || saleDtlList.Count < 0)
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "供货单号为:" + saleDocNo + ",获取供货货品信息为空!";
                ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                return baseresultdata;
            }
            //供货货品信息是否需要校验

            //供货货品相关的盒条码信息
            string boxBarCodeHql = string.Format("reagoodsbarcodeoperation.BDocID={0} and reagoodsbarcodeoperation.BarcodeCreatType={1} and reagoodsbarcodeoperation.OperTypeID={2}", saleDoc.Id, long.Parse(ReaGoodsBarcodeOperationSerialType.条码生成.Key), long.Parse(ReaGoodsBarcodeOperType.供货.Key));
            IList<ReaGoodsBarcodeOperation> operationList = IDReaGoodsBarcodeOperationDao.GetListByHQL(boxBarCodeHql);

            //封装信息
            ReaBmsCenSaleDoc saleDoc2 = ClassMapperHelp.GetMapper<ReaBmsCenSaleDoc, ReaBmsCenSaleDoc>(saleDoc);
            saleDoc2.DataTimeStamp = null;
            //string saleDocStr = JsonHelper.ObjectToJson(saleDoc2);
            //ZhiFang.Common.Log.Log.Debug("saleDocStr:" + saleDocStr);

            JObject jsaleDoc = JsonHelper.GetPropertyInfo<ReaBmsCenSaleDoc>(saleDoc2);
            jPostData.Add(ZFPlatformHelp.供货总单.Key, jsaleDoc);

            #region 供货明细封装
            JArray jdtlList = new JArray();
            foreach (ReaBmsCenSaleDtl saleDtl in saleDtlList)
            {
                //判断是否存在供货商货品编码
                if (string.IsNullOrEmpty(saleDtl.CenOrgGoodsNo))
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "供货明细的货品为:" + saleDtl.ReaGoodsName + ",供货商货品编码为空!";
                    ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                    return baseresultdata;
                }
                if (!saleDtl.GoodsQty.HasValue || saleDtl.GoodsQty <= 0)
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "供货明细的货品为:" + saleDtl.ReaGoodsName + ",供货数为:" + saleDtl.GoodsQty + "!";
                    ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                    return baseresultdata;
                }
                ReaBmsCenSaleDtl saleDtl2 = ClassMapperHelp.GetMapper<ReaBmsCenSaleDtl, ReaBmsCenSaleDtl>(saleDtl);
                saleDtl2.DataTimeStamp = null;
                //判断是否存在平台货品编码
                if (string.IsNullOrEmpty(saleDtl2.GoodsNo))
                {
                    saleDtl2.GoodsNo = saleDtl2.CenOrgGoodsNo;
                }
                JObject jorderDtl = JsonHelper.GetPropertyInfo<ReaBmsCenSaleDtl>(saleDtl2);
                jdtlList.Add(jorderDtl);
            }
            //string saleDtlListStr = JsonHelper.ObjectToJson(jdtlList);
            //ZhiFang.Common.Log.Log.Debug("saleDtlList:" + saleDtlListStr);
            jPostData.Add(ZFPlatformHelp.供货明细单.Key, jdtlList);
            #endregion

            #region 供货明细货品的盒条码信息封装
            JArray jBarcodeList = new JArray();
            foreach (ReaGoodsBarcodeOperation barcodeOperation in operationList)
            {
                ReaGoodsBarcodeOperation barcodeOperation2 = ClassMapperHelp.GetMapper<ReaGoodsBarcodeOperation, ReaGoodsBarcodeOperation>(barcodeOperation);
                barcodeOperation2.DataTimeStamp = null;
                //判断是否存在平台货品编码
                if (string.IsNullOrEmpty(barcodeOperation2.GoodsNo))
                {
                    barcodeOperation2.GoodsNo = barcodeOperation2.CenOrgGoodsNo;
                }
                JObject jBarcode = JsonHelper.GetPropertyInfo<ReaGoodsBarcodeOperation>(barcodeOperation2);
                jBarcodeList.Add(jBarcode);
            }
            //string barcodeOperationlListStr = JsonHelper.ObjectToJson(jBarcodeList);
            //ZhiFang.Common.Log.Log.Debug("barcodeOperationlList:" + barcodeOperationlListStr);
            jPostData.Add(ZFPlatformHelp.供货条码.Key, jBarcodeList);
            #endregion

            return baseresultdata;
        }

        public BaseResultDataValue UpdatePlatformSaleDocAndDtlToClient(string labcCode, string compCode, long saleDocId, string saleDocNo)
        {
            BaseResultDataValue baseresultdata = new BaseResultDataValue();
            #region 验证判断
            //验证机构信息
            if (string.IsNullOrEmpty(saleDocNo))
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "供货单号(saleDocNo)为空!";
                ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                return baseresultdata;
            }
            if (string.IsNullOrEmpty(labcCode))
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "订货方所属机构平台编码(labcCode)为空!";
                ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                return baseresultdata;
            }
            if (string.IsNullOrEmpty(compCode))
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "供货方所属机构平台编码(compCode)为空!";
                ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                return baseresultdata;
            }
            #endregion
            ReaBmsCenSaleDoc saleDoc = this.Get(saleDocId);
            //供货单是否存在
            if (saleDoc == null)
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "供货单号为:" + saleDocNo + ",获取供货信息为空!";
                ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                return baseresultdata;
            }
            //判断供货单是否可以被提取
            if (saleDoc.Status == int.Parse(ReaBmsCenSaleDocAndDtlStatus.供货提取.Key))
            {
                baseresultdata.success = true;
                baseresultdata.ErrorInfo = "供货单号为:" + saleDocNo + ",当前供货状态为:" + ReaBmsCenSaleDocAndDtlStatus.GetStatusDic()[saleDoc.Status.ToString()].Name + ",请不要重复提交!";
                ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                return baseresultdata;
            }
            IList<ReaBmsCenSaleDtl> saleDtlList = IBReaBmsCenSaleDtl.SearchListByHQL("reabmscensaledtl.SaleDocID=" + saleDoc.Id);
            if (saleDtlList == null || saleDtlList.Count < 0)
            {
                baseresultdata.success = true;
                baseresultdata.ErrorInfo = "供货单号为:" + saleDocNo + ",获取供货货品信息为空!";
                ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                return baseresultdata;
            }

            saleDoc.IOFlag = int.Parse(ReaBmsCenSaleDocIOFlag.已提取.Key);
            saleDoc.Status = int.Parse(ReaBmsCenSaleDocAndDtlStatus.供货提取.Key);
            saleDoc.StatusName = ReaBmsCenSaleDocAndDtlStatus.GetStatusDic()[saleDoc.Status.ToString()].Name;
            saleDoc.OperDate = DateTime.Now;
            foreach (var saleDtl in saleDtlList)
            {
                saleDtl.IOFlag = saleDoc.IOFlag;
                saleDtl.Status = saleDtl.Status;
                saleDtl.StatusName = saleDtl.StatusName;

                List<string> tmpaDtl = new List<string>();
                tmpaDtl.Add("Id=" + saleDtl.Id + " ");
                tmpaDtl.Add("IOFlag=" + saleDtl.IOFlag + " ");
                tmpaDtl.Add("Status=" + saleDtl.Status + " ");
                tmpaDtl.Add("StatusName='" + saleDtl.StatusName + "'");

                string[] tempArrayDtl = tmpaDtl.ToArray();
                IBReaBmsCenSaleDtl.Entity = saleDtl;
                IBReaBmsCenSaleDtl.Update(tempArrayDtl);//this.Edit();
            }

            List<string> tmpa = new List<string>();
            tmpa.Add("Id=" + saleDoc.Id + " ");
            tmpa.Add("OperDate='" + saleDoc.OperDate + "'");
            tmpa.Add("IOFlag=" + saleDoc.IOFlag + " ");
            tmpa.Add("Status=" + saleDoc.Status + " ");
            tmpa.Add("StatusName='" + saleDoc.StatusName + "'");

            string[] tempArray = tmpa.ToArray();
            this.Entity = saleDoc;
            baseresultdata.success = this.Update(tempArray);//this.Edit();
            if (baseresultdata.success)
            {
                AddReaReqOperation(saleDoc, -1, "");
            }
            else
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "更新供货单状态及提取标志失败!";
            }
            return baseresultdata;
        }
        #endregion
    }
}