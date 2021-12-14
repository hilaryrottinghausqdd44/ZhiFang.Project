using System;
using System.Collections.Generic;
using System.Linq;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.Entity.LabStar.ViewObject.Request;
using ZhiFang.Entity.LabStar.ViewObject.Response;
using ZhiFang.IBLL.LabStar;
using ZhiFang.IDAO.LabStar;
using ZhiFang.LabStar.Common;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.BLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public class BLisBarCodeForm : BaseBLL<LisBarCodeForm>, IBLisBarCodeForm
    {
        #region 引用
        public SysCookieValue SysCookieValue  { get; set; }
        IBBPara IBBPara { get; set; }
        IBBParaItem IBBParaItem { get; set; }
        IDLisBarCodeItemDao IDLisBarCodeItemDao { get; set; }
        IBLisOperate IBLisOperate { get; set; }
        IDLisOrderFormDao IDLisOrderFormDao { get; set; }
        IDLisOrderItemDao IDLisOrderItemDao { get; set; }
        IDLisPatientDao IDLisPatientDao { get; set; }
        IDLBDestinationDao IDLBDestinationDao { get; set; }
        IDLBSampleTypeDao IDLBSampleTypeDao { get; set; }
        IDLBSamplingGroupDao IDLBSamplingGroupDao { get; set; }
        IDLisBarCodeRefuseRecordDao IDLisBarCodeRefuseRecordDao { get; set; }
        IDLBReportDateItemDao IDLBReportDateItemDao { get; set; }
        IDLBReportDateDescDao IDLBReportDateDescDao { get; set; }
        IDLBReportDateRuleDao IDLBReportDateRuleDao { get; set; }
        IBBPrintModel IBBPrintModel { get; set; }
        IDLBItemDao IDLBItemDao { get; set; }
        IDLBTGetMaxNoDao IDLBTGetMaxNoDao { get; set; }
        IDLBPhrasesWatchClassItemDao IDLBPhrasesWatchClassItemDao { get; set; }
        IDLBSampleItemDao IDLBSampleItemDao { get; set; }
        IDLisBarCodeFormDao IDLisBarCodeFormDao { get; set; }
        IDLBSectionDao IDLBSectionDao { get; set; }
        IDLBSectionItemDao IDLBSectionItemDao { get; set; }
        IDLisQueueDao IDLisQueueDao { get; set; }
        IBLisTestItem IBLisTestItem { get; set; }
        IDLBTranRuleHostSectionDao IDLBTranRuleHostSectionDao { get; set; }
        IDLBTranRuleDao IDLBTranRuleDao { get; set; }
        IDLBTranRuleItemDao IDLBTranRuleItemDao { get; set; }
        IDLBItemGroupDao IDLBItemGroupDao { get; set; }
        IDLisTestFormDao IDLisTestFormDao { get; set; }
        #endregion
        #region 样本条码
        /// <summary>
        /// 条码确认
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="barcodes"></param>
        /// <returns></returns>
        public BaseResultDataValue Edit_PreBarCodeAffirm(long nodetype, string barcodes,long userid,string username)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (string.IsNullOrEmpty(barcodes)) {
                brdv.success = false;
                brdv.ErrorInfo = "条码号不可为空！";
                return brdv;
            }
            //获取参数配置
            List<BPara> bParas = IBBParaItem.SelPreBParas(nodetype, "", Pre_AllModules.样本条码.Value.DefaultValue, userid.ToString(), username);
            var isCallHIS = bParas.Where(w => w.ParaNo == "Pre_OrderBarCode_DefaultPara_0061").First();//是否调用条码确认接口 
            var isAffirm = bParas.Where(w => w.ParaNo == "Pre_OrderBarCode_DefaultPara_0062").First();//调用条码确认接口成功再确认条码
            bool isCallSuccess = true;
            List<string> result = new List<string>();
            if (!string.IsNullOrEmpty(isCallHIS.ParaValue)) { 
                //调用HIS确认接口
            }
            if (string.IsNullOrEmpty(isCallHIS.ParaValue) || (!string.IsNullOrEmpty(isCallHIS.ParaValue) && isAffirm.ParaValue == "0") || (!string.IsNullOrEmpty(isCallHIS.ParaValue) && isAffirm.ParaValue == "1" && isCallSuccess))
            {
                string[] barcodearr = barcodes.Split(',');
                IList<LisBarCodeForm> lisBarCodeForms = DBDao.GetListByHQL("BarCode in ('" + string.Join("','", barcodearr) + "')");
                if (lisBarCodeForms.Count > 0)
                {
                    foreach (var form in lisBarCodeForms)
                    {
                        if (form.BarCodeStatusID == long.Parse(BarCodeStatus.生成条码.Value.Code))
                        {
                            form.BarCodeStatusID = long.Parse(BarCodeStatus.条码确认.Value.Code);
                            form.BarCodeCurrentStatus = BarCodeStatus.条码确认.Value.Name;
                            form.AffirmTime = DateTime.Now;
                            form.DataUpdateTime = DateTime.Now;
                            form.IsAffirm = 1;
                            bool formAddSuccess = DBDao.Update(form);
                            bool ItemAddSuccess = true;
                            if (formAddSuccess) {
                                IBLisOperate.AddLisOperate(form, BarCodeStatus.条码确认.Value, "条码确认", SysCookieValue);
                                IList<LisBarCodeItem> lisBarCodeItems =  IDLisBarCodeItemDao.GetListByHQL("LisBarCodeForm.Id = "+ form.Id);
                                foreach (var item in lisBarCodeItems)
                                {
                                    item.ItemStatusID = long.Parse(BarCodeStatus.生成条码.Value.Code);
                                    item.DataUpdateTime = DateTime.Now;
                                    if (!IDLisBarCodeItemDao.Update(item))
                                    {
                                        ItemAddSuccess = false;
                                    }
                                    else {
                                        IBLisOperate.AddLisOperate(item, BarCodeStatus.条码确认.Value, "条码确认", SysCookieValue);
                                    } 
                                }
                            }
                            if (formAddSuccess && ItemAddSuccess)
                            {
                                result.Add(form.BarCode + ":true");
                            }
                            else 
                            {
                                result.Add(form.BarCode + ":false");
                            }
                        }
                        else {
                            result.Add(form.BarCode + ":false");
                        }
                    }
                }
                else {
                    brdv.success = false;
                    brdv.ErrorInfo = "未找到条码样本单！";
                    return brdv;
                }
            }
            else {
                brdv.success = false;
                brdv.ErrorInfo = "条码确认失败，调用HIS接口错误！";
                return brdv;
            }
            brdv.success = true;//需要改为都成功的是true，有一个不成功就是false
            brdv.ResultDataValue = string.Join(",", result);            
            return brdv;
        }
        /// <summary>
        /// 条码作废
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="barcodes"></param>
        /// <returns></returns>
        public BaseResultDataValue Edit_PreBarCodeInvalid(long nodetype, string barcodes,bool IsEditOrder,string userid,string username)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (string.IsNullOrEmpty(barcodes))
            {
                brdv.success = false;
                brdv.ErrorInfo = "条码号不可为空！";
                return brdv;
            }
            //获取参数配置
            List<BPara> bParas = IBBParaItem.SelPreBParas(nodetype, "", Pre_AllModules.样本条码.Value.DefaultValue, userid, username);
            var isCallHIS = bParas.Where(w => w.ParaNo == "Pre_OrderBarCode_DefaultPara_0063").First();//是否调用HIS条码作废接口
            //var para0071 = bParas.Where(w => w.ParaNo == "Pre_OrderBarCode_DefaultPara_0071").First();//是否进行条码作废校验
            List<string> result = new List<string>();
            if (!string.IsNullOrEmpty(isCallHIS.ParaValue))
            {
                //调用HIS确认接口
            }
            string[] barcodearr = barcodes.Split(',');
            IList<LisBarCodeForm> lisBarCodeForms = DBDao.GetListByHQL("BarCode in ('" + string.Join("','", barcodearr) + "')");
            if (lisBarCodeForms.Count > 0)
            {
                foreach (var form in lisBarCodeForms)
                {
                    //签收之前的样本可以作废
                    if (form.BarCodeStatusID < 7 && form.InceptTime == null)
                    {
                        form.DataUpdateTime = DateTime.Now;
                        form.BarCodeFlag = -1;
                        bool formAddSuccess = DBDao.Update(form);
                        bool ItemAddSuccess = true;
                        if (formAddSuccess)
                        {
                            IBLisOperate.AddLisOperate(form, OrderFromOperateType.条码作废.Value, "条码作废", SysCookieValue);
                            IList<LisBarCodeItem> lisBarCodeItems = IDLisBarCodeItemDao.GetListByHQL("LisBarCodeForm.Id = " + form.Id);
                            List<long> orderitemid = new List<long>();
                            foreach (var item in lisBarCodeItems)
                            {
                                item.DataUpdateTime = DateTime.Now;
                                item.BarCodeItemFlag = -1;
                                if (!IDLisBarCodeItemDao.Update(item))
                                {
                                    ItemAddSuccess = false;
                                }
                                else
                                {
                                    if(!orderitemid.Contains(item.LisOrderItem.Id))
                                        orderitemid.Add(item.LisOrderItem.Id);
                                    IBLisOperate.AddLisOperate(item, OrderFromOperateType.条码作废.Value, "条码作废", SysCookieValue);
                                }
                            }
                            if (orderitemid.Count > 0 && IsEditOrder) {
                                IList<LisOrderItem> lisOrderItems = IDLisOrderItemDao.GetListByHQL("Id in ("+ string.Join(",", orderitemid) +")");
                                List<long?> formids = new List<long?>();
                                lisOrderItems.ToList().ForEach(f => { if (!formids.Contains(f.OrderFormID)) { formids.Add(f.OrderFormID); } });
                                int editcount = IDLisOrderItemDao.UpdateByHql("update LisOrderItem set ItemStatusID = 0,OrderItemExecFlag=0  where  Id in (" + string.Join(",", orderitemid)+")");
                                IDLisOrderFormDao.UpdateByHql("update LisOrderForm set OrderExecFlag=0  where OrderExecFlag=1 and  Id in (" + string.Join(",", formids) + ")");
                                if (editcount <= 0) {
                                    brdv.success = false;
                                    brdv.ErrorInfo = "医嘱单状态修改失败！";
                                    return brdv;
                                }
                            }
                        }
                        if (formAddSuccess && ItemAddSuccess)
                        {
                            result.Add(form.BarCode + ":true");
                        }
                        else
                        {
                            result.Add(form.BarCode + ":false");
                        }
                    }
                    else
                    {
                        result.Add(form.BarCode + ":false");
                    }
                }
            }
            else
            {
                brdv.success = false;
                brdv.ErrorInfo = "未找到条码样本单！";
                return brdv;
            }
            
            brdv.success = true;
            brdv.ResultDataValue = string.Join(",", result);
            return brdv;
        }

        /// <summary>
        /// 样本条码_取单凭证
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public BaseResultDataValue GetBarCodeSamppleGatherVoucherData(long nodetype, string barcode,bool? isloadtable, bool? isupdatebcitems, string modelcode)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            #region 数据查询
            string pdfurls = "";
            string[] barcodearr = barcode.Split(',');
            //查询样本单
            IList<LisBarCodeForm> lisBarCodeForms = this.SearchListByHQL("BarCode in ('" + string.Join("','", barcodearr) + "')");
            List<long> formids = new List<long>();//样本单ID
            foreach (var formentity in lisBarCodeForms)
            {
                if(!formids.Contains(formentity.Id))
                    formids.Add(formentity.Id);
            }
            //查询样本项目
            IList<LisBarCodeItem> lisBarCodeItems = IDLisBarCodeItemDao.GetListByHQL("LisBarCodeForm.Id in ("+ string.Join(",", formids) + ")");
            List<long?> itemids = new List<long?>();
            foreach (var item in lisBarCodeItems)
            {
                if (!itemids.Contains(item.BarCodesItemID))
                    itemids.Add(item.BarCodesItemID);
            }
            IList<LBItem> lBItems = IDLBItemDao.GetListByHQL("Id in (" + string.Join(",", itemids) + ")");
            #endregion
            #region 取单时间信息查询与更新样本项目表中取单时间描述字段
            if (isloadtable == null || isloadtable == true)
            {
                //取单时间_取单时间分类项目明细
                IList<LBReportDateItem> lBReportDateItems = IDLBReportDateItemDao.GetListByHQL("LBItem.Id in (" + string.Join(",", itemids) + ")");
                bool isreturnerror = false;
                if (lBReportDateItems.Count > 0) {
                    List<long> reportdataids = new List<long>();
                    foreach (var lbreportDateItem in lBReportDateItems)
                    {
                        if (!reportdataids.Contains(lbreportDateItem.LBReportDate.Id))
                            reportdataids.Add(lbreportDateItem.LBReportDate.Id);
                    }
                    //取单时间_取单时间分类明细
                    IList<LBReportDateDesc> lBReportDateDescs = IDLBReportDateDescDao.GetListByHQL("LBReportDate.Id in (" + string.Join(",", reportdataids) + ")");
                    if (lBReportDateDescs.Count > 0)
                    {
                        List<long> reportdatedescids = new List<long>();
                        foreach (var lbreportdatedescentity in lBReportDateDescs)
                        {
                            if (!reportdatedescids.Contains(lbreportdatedescentity.Id))
                                reportdatedescids.Add(lbreportdatedescentity.Id);
                        }
                        //根据取单
                        string where = "ReportDateDescID in (" + string.Join(",", reportdatedescids) + ") and '";
                        where += DateTime.Now.ToLongTimeString().ToString() + "' between convert(varchar,BeginTime,8) and  convert(varchar,EndTime,8)";
                        #region 获取星期几
                        string dt = DateTime.Today.DayOfWeek.ToString();
                        int week = 0;
                        switch (dt)
                        {
                            case "Monday":
                                week = 1;
                                break;
                            case "Tuesday":
                                week = 2;
                                break;
                            case "Wednesday":
                                week = 3;
                                break;
                            case "Thursday":
                                week = 4;
                                break;
                            case "Friday":
                                week = 5;
                                break;
                            case "Saturday":
                                week = 6;
                                break;
                            case "Sunday":
                                week = 7;
                                break;
                        }
                        #endregion
                        where += " and " + week + " between BeginWeekDay and EndWeekDay";
                        var reportDateRules = IDLBReportDateRuleDao.GetListByHQL(where);
                        if (reportDateRules.Count > 0)
                        {
                            //更新样本项目表取单时间描述字段
                            if (isupdatebcitems == true)
                            {
                                foreach (var item in lisBarCodeItems)
                                {
                                    //根据项目找到对应的取单时间描述
                                    var itemreportdateitem  = lBReportDateItems.Where(a => a.LBItem.Id == item.OrdersItemID);
                                    if (itemreportdateitem.Count() > 0) {
                                        List<LBReportDateDesc> lbreportDateDescs = new List<LBReportDateDesc>();
                                        foreach (var reportdatetimeentity in itemreportdateitem)
                                        {
                                            var reportDateDescs = lBReportDateDescs.Where(a => a.LBReportDate.Id == reportdatetimeentity.LBReportDate.Id);
                                            if (reportDateDescs.Count() > 0) {
                                                reportDateDescs.ToList().ForEach(f => { if (!lbreportDateDescs.Contains(f)) { lbreportDateDescs.Add(f); } });
                                            }
                                        }
                                        if (lbreportDateDescs.Count > 0) {
                                            //找到的取单时间描述，暂时只取第一个，如果后续有需要则重新整改
                                            foreach (var descentity in lbreportDateDescs)
                                            {
                                                var  rules = reportDateRules.Where(a => a.ReportDateDescID == descentity.Id);
                                                if(rules.Count() > 0)
                                                {
                                                    item.ReportDateDesc = descentity.ReportDateDesc;
                                                    continue;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                            isreturnerror = true;
                    }
                    else 
                        isreturnerror = true;
                }
                else 
                    isreturnerror = true;
                if (isreturnerror) {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "未查询到取单时间描述信息！";
                }
            }
            #endregion
            #region 数据转换
            List<ProofLisBarCodeFormVo> proofLisBarCodeforms = new List<ProofLisBarCodeFormVo>();
            List<ProofLisBarCodeItemVo> proofLisBarCodeItemVos = new List<ProofLisBarCodeItemVo>();
            foreach (var form in lisBarCodeForms)
            {
                ProofLisBarCodeFormVo entity = new ProofLisBarCodeFormVo();
                LisOrderForm lisOrderForm = IDLisOrderFormDao.Get(form.LisOrderForm.Id);
                ClassMapperHelp.EntityToEntity(lisOrderForm, entity);
                LisPatient lisPatient = IDLisPatientDao.Get(form.LisPatient.Id);
                ClassMapperHelp.EntityToEntity(lisPatient, entity);
                ClassMapperHelp.EntityToEntity(form, entity);
                entity.OrderFormID = lisOrderForm.Id;
                entity.PatID = lisPatient.Id;
                entity.BarCodeFormID = form.Id;
                proofLisBarCodeforms.Add(entity);
            }
            foreach (var item in lisBarCodeItems)
            {
                ProofLisBarCodeItemVo entity = new ProofLisBarCodeItemVo();
                LisOrderItem lisOrderItem = IDLisOrderItemDao.Get(item.LisOrderItem.Id);
                ClassMapperHelp.EntityToEntity(lisOrderItem, entity);
                ClassMapperHelp.EntityToEntity(item, entity);
                entity.BarCodeItemID = item.Id;
                entity.BarCodeFormID = item.LisBarCodeForm.Id;
                entity.OrderItemID = lisOrderItem.Id;
                entity.ItemName = lBItems.Where(a => a.Id == item.BarCodesItemID).First().CName;
                proofLisBarCodeItemVos.Add(entity);
            }
            #endregion
            #region 数据返回
            if (proofLisBarCodeforms.Count >0 && proofLisBarCodeItemVos.Count > 0) {
                pdfurls = IBBPrintModel.PrePrintGatherVoucherByPrintModel(proofLisBarCodeforms, proofLisBarCodeItemVos, modelcode);
            }
            if (string.IsNullOrEmpty(pdfurls))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "生成失败";
            }
            else
            {
                baseResultDataValue.success = true;
                baseResultDataValue.ResultDataValue = pdfurls;
            }
            #endregion 
            return baseResultDataValue;
        }
        public BaseResultDataValue GetPrintBarCodeCount(long nodetype, string barcodes,string userid,string username)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
           
            //获取参数配置
            List<BPara> bParas = IBBParaItem.SelPreBParas(nodetype, "", Pre_AllModules.样本条码.Value.DefaultValue, userid, username);
            var funcget = bParas.Where(w => w.ParaNo == "Pre_OrderBarCode_DefaultPara_0049").First();//是否通过函数获取数量 
            var sempleinggroupget = bParas.Where(w => w.ParaNo == "Pre_OrderBarCode_DefaultPara_0050").First();//是否获取采样组条码数量
            int printcount = 1;
            if (!string.IsNullOrEmpty(sempleinggroupget.ParaValue)) {
                //采样组获取打印份数
                if (sempleinggroupget.ParaValue == "1") {
                    var barCodeForm = this.SearchListByHQL("BarCode = '" + barcodes + "'").First();
                    var sg = IDLBSamplingGroupDao.Get(long.Parse(barCodeForm.SamplingGroupID.ToString()));
                    if (sg.PrintCount > 0) {
                        printcount = 1;
                    }
                }

            } else if (!string.IsNullOrEmpty(funcget.ParaValue)) {
                //方法获取打印份数
                if (funcget.ParaValue == "1")
                {
                        printcount = 1;
                }
            }
            baseResultDataValue.success = true;
            baseResultDataValue.ResultDataValue = printcount.ToString(); ;
            return baseResultDataValue;
        }
        /// <summary>
        /// 条码打印次数和打印状态更新
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="barcodes"></param>
        /// <returns></returns>
        public BaseResultDataValue Edit_BarCodeFromPrintStatus(long nodetype, string barcodes, string IsAffirmBarCode, string userid, string username)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            baseResultDataValue.success = true;
            //获取参数配置
            List<BPara> bParas = IBBParaItem.SelPreBParas(nodetype, "", Pre_AllModules.样本条码.Value.DefaultValue, userid, username);
            var barCodeForms = this.SearchListByHQL("BarCode = '" + barcodes + "'");
            //扫码补打时需要判断的内容
            if ((barCodeForms == null || barCodeForms.Count == 0) && IsAffirmBarCode == "1")
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "未查询到当前条码信息！";
                return baseResultDataValue;
            }
            else
            {
                var barCodeForm = barCodeForms.First();
                if (IsAffirmBarCode == "1")
                {
                    var para0066 = bParas.Where(w => w.ParaNo == "Pre_OrderBarCode_DefaultPara_0066").First();//是否开启补打条码校验
                    if (para0066.ParaValue == "1")
                    {
                        VerifyReminInfo verifyReminInfo = BarCodeFromPrintStatusEditVerify(bParas, barCodeForm);
                        if (verifyReminInfo != null && verifyReminInfo.alterMode == "2")
                        {
                            baseResultDataValue.success = false;
                            baseResultDataValue.ErrorInfo = verifyReminInfo.failureInfo;
                            return baseResultDataValue;
                        }
                    }
                    barCodeForm.PrintTime = DateTime.Now;
                    barCodeForm.PrintTimes += 1;
                    barCodeForm.DataUpdateTime = DateTime.Now;
                }
                else
                {
                    barCodeForm.BarCodeStatusID = long.Parse(BarCodeStatus.条码打印.Value.Code);
                    barCodeForm.BarCodeCurrentStatus = BarCodeStatus.条码打印.Value.Name;
                    barCodeForm.IsAffirm = 1;
                    barCodeForm.PrintTime = DateTime.Now;
                    barCodeForm.PrintTimes += 1;
                    barCodeForm.DataUpdateTime = DateTime.Now;
                }
                if (DBDao.Update(barCodeForm))
                {
                    IBLisOperate.AddLisOperate(barCodeForm, BarCodeStatus.条码打印.Value, "条码打印", SysCookieValue);
                }
                else
                {
                    baseResultDataValue.success = false;
                }
            }
           
            return baseResultDataValue;
        }
        /// <summary>
        /// 补打条码校验
        /// </summary>
        /// <param name="bParas"></param>
        /// <param name="lisBarCodeFormVO"></param>
        /// <returns></returns>
        public VerifyReminInfo BarCodeFromPrintStatusEditVerify(List<BPara> bParas, LisBarCodeForm lisBarCodeForm)
        {
            #region
            var para0067 = bParas.Where(w => w.ParaNo == "Pre_OrderBarCode_DefaultPara_0067").First();//已进行送检不能补打条码
            var para0068 = bParas.Where(w => w.ParaNo == "Pre_OrderBarCode_DefaultPara_0068").First();//已经签收的不能补打条码
            var para0069 = bParas.Where(w => w.ParaNo == "Pre_OrderBarCode_DefaultPara_0069").First();//已审核报告不能补打条码
            var para0070 = bParas.Where(w => w.ParaNo == "Pre_OrderBarCode_DefaultPara_0070").First();//已经核收的不能补打条码
           
            VerifyReminInfo verifyReminInfo = new VerifyReminInfo();
            if (para0067.ParaValue == "1" && lisBarCodeForm.BarCodeStatusID > 4 && lisBarCodeForm.SendTime != null)
            {
                verifyReminInfo.alterMode = "2";
                verifyReminInfo.failureInfo = "已进行送检的不能补打条码！";
                return verifyReminInfo;
            }
            else if (para0068.ParaValue == "1" && lisBarCodeForm.BarCodeStatusID > 6 && lisBarCodeForm.InceptTime != null)
            {
                verifyReminInfo.alterMode = "2";
                verifyReminInfo.failureInfo = "已经签收的不能补打条码！";
                return verifyReminInfo;
            }
            else if (para0069.ParaValue == "1" && lisBarCodeForm.BarCodeStatusID > 13 && lisBarCodeForm.ReportCheckTime != null)
            {
                verifyReminInfo.alterMode = "2";
                verifyReminInfo.failureInfo = "已审核报告不能补打条码！";
                return verifyReminInfo;
            }
            else if (para0070.ParaValue == "1" && lisBarCodeForm.BarCodeStatusID > 10 && lisBarCodeForm.ReceiveTime != null)
            {
                verifyReminInfo.alterMode = "2";
                verifyReminInfo.failureInfo = "已经核收的不能补打条码！";
                return verifyReminInfo;
            }
            return verifyReminInfo;
            #endregion
        }

        /// <summary>
        /// 排队信息
        /// </summary>
        /// <param name="nodetype">站点类型</param>
        /// <param name="barcode">条码号/param>
        /// <param name="patientType">叫号系统病人类型1：一般、2：优先/param>
        /// <param name="userid"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public BaseResultDataValue Add_EqueuingMachineInfo(long nodetype, string barcode, string patientType, string userid, string username)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            baseResultDataValue.success = true;
            //获取参数配置
            List<BPara> bParas = IBBParaItem.SelPreBParas(nodetype, "", Pre_AllModules.样本条码.Value.DefaultValue, userid, username);
            var Para0037 = bParas.Where(w => w.ParaNo == "Pre_OrderBarCode_DefaultPara_0037").First();//叫号系统数据更新起始位
            var Para0038 = bParas.Where(w => w.ParaNo == "Pre_OrderBarCode_DefaultPara_0038").First();//一般病人号段类型
            var Para0039 = bParas.Where(w => w.ParaNo == "Pre_OrderBarCode_DefaultPara_0039").First();//优先病人号段类型
            var barcodearr = barcode.Split(',');
            var barCodeForms = this.SearchListByHQL("BarCode = '" + string.Join("','", barcodearr) + "'");
            #region 最大号处理
            string MaxID = "";
            string QueueNoHeader = "";
            string[] BmsTypeArr = Para0037.ParaValue.Split(',');
            if (patientType == "1")
            {
                
                if (string.IsNullOrEmpty(Para0038.ParaValue)) {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "未设置一般病人号段类型！";
                    return baseResultDataValue;
                }
                QueueNoHeader = Para0038.ParaValue;
                var  maxno = LBTGetMaxNOBmsTypes.GetStatusDic().Where(a => a.Value.Code == Para0038.ParaValue);
                if (maxno == null || maxno.Count() == 0) {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "未找到流水号业务类型编码！";
                    return baseResultDataValue;
                }
                else {
                    string maxnonum = "";
                    if (BmsTypeArr.Count() > 0) {
                        foreach (var BmsTypes in BmsTypeArr)
                        {
                            var BmsType = BmsTypes.Split('-');
                            if (BmsType[0] == maxno.First().Value.Code) {
                                maxnonum = BmsType[1];
                            }
                        }
                    }
                    MaxID = Edit_GetMaxNo(long.Parse(maxno.First().Value.Id), maxno.First().Value.Code,false, maxnonum,0);
                }
            } else if(patientType == "2")
            {
                if (string.IsNullOrEmpty(Para0039.ParaValue))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "未设置优先病人号段类型！";
                    return baseResultDataValue;
                }
                QueueNoHeader = Para0039.ParaValue;
                var maxno = LBTGetMaxNOBmsTypes.GetStatusDic().Where(a => a.Value.Code == Para0039.ParaValue);
                if (maxno == null || maxno.Count() == 0)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "未找到流水号业务类型编码！";
                    return baseResultDataValue;
                }
                else
                {
                    string maxnonum = "";
                    if (BmsTypeArr.Count() > 0)
                    {
                        foreach (var BmsTypes in BmsTypeArr)
                        {
                            var BmsType = BmsTypes.Split('-');
                            if (BmsType[0] == maxno.First().Value.Code)
                            {
                                maxnonum = BmsType[1];
                            }
                        }
                    }
                    MaxID = Edit_GetMaxNo(long.Parse(maxno.First().Value.Id), maxno.First().Value.Code, false, maxnonum, 0);
                }
            }
            #endregion

            foreach (var barCodeForm in barCodeForms)
            {
                if (barCodeForm.AffirmTime != null &&  barCodeForm.IsAffirm > 0 && barCodeForm.BarCodeStatusID > 1) {
                    LisQueue lisQueue = new LisQueue();
                    lisQueue.LineDate = DateTime.Now;
                    lisQueue.QueueNo = int.Parse(MaxID);
                    lisQueue.QueueNoHeader = QueueNoHeader;
                    lisQueue.BarCodeFormID = barCodeForm.Id;
                    lisQueue.BarCode = barCodeForm.BarCode;
                    lisQueue.PatName = barCodeForm.LisPatient.CName;
                    lisQueue.HisPatNo = barCodeForm.LisPatient.HisPatNo;
                    lisQueue.OrderFormNo = barCodeForm.LisOrderForm.OrderFormNo;
                    lisQueue.ExecFlag = 0;
                    lisQueue.PatTypeID = int.Parse(barCodeForm.LisPatient.PatType);
                    if (!IDLisQueueDao.Save(lisQueue))
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "创建失败！";
                        return baseResultDataValue;
                    }
                }
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 获取最大流水号
        /// </summary>
        /// <param name="BmsTypeID"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public string Edit_GetMaxNo(long BmsTypeID, string BmsType, bool IsGlobal,string StartBit, int length)
        {
            IList<LBTGetMaxNo> lBTGetMaxNos = IDLBTGetMaxNoDao.GetListByHQL("BmsTypeID=" + BmsTypeID);
            string day = DateTime.Now.ToString("yyyyMMdd");
            string MaxID = "";
            if (string.IsNullOrEmpty(StartBit)) {
                StartBit = "1";
            }
            if (lBTGetMaxNos.Count > 0)
            {

                LBTGetMaxNo lBTGetMaxNo = lBTGetMaxNos[0];
                //全局流水号需要一直增加不需要重置
                if (lBTGetMaxNo.BmsDate.ToString("yyyyMMdd") != day && IsGlobal == false)
                {
                    lBTGetMaxNo.MaxID = StartBit;
                    lBTGetMaxNo.BmsDate = DateTime.Now;
                    lBTGetMaxNo.DataUpdateTime = DateTime.Now;
                    IDLBTGetMaxNoDao.Update(lBTGetMaxNo);
                    MaxID = lBTGetMaxNo.MaxID;
                }
                else
                {
                    string maxnum = lBTGetMaxNo.MaxID;
                    lBTGetMaxNo.MaxID = (long.Parse(maxnum) + 1).ToString();
                    lBTGetMaxNo.DataUpdateTime = DateTime.Now;
                    IDLBTGetMaxNoDao.Update(lBTGetMaxNo);
                    MaxID = lBTGetMaxNo.MaxID;
                }
            }
            else
            {
                LBTGetMaxNo lBTGetMaxNo = new LBTGetMaxNo();
                lBTGetMaxNo.BmsDate = DateTime.Now;
                lBTGetMaxNo.BmsTypeID = BmsTypeID;
                lBTGetMaxNo.BmsType = BmsType;
                lBTGetMaxNo.MaxID = StartBit;
                lBTGetMaxNo.DataAddTime = DateTime.Now;
                IDLBTGetMaxNoDao.Save(lBTGetMaxNo);
                MaxID = lBTGetMaxNo.MaxID;
            }
            if (length != 0)
            {
                MaxID = MaxID.PadLeft(length, '0');
            }
            return MaxID;
        }

        public BaseResultDataValue Edit_BarCodeFormBarCodeByBarCodeFormID(long nodetype, string barcode, string barcodeformid, string userid, string username)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (string.IsNullOrEmpty(barcode))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "条码号不允许为空！";
                return baseResultDataValue;
            }
            if (string.IsNullOrEmpty(barcodeformid))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "样本单号不允许为空！";
                return baseResultDataValue;
            }
            List<BPara> bParas = IBBParaItem.SelPreBParas(nodetype, "", Pre_AllModules.样本条码.Value.DefaultValue, userid, username);
            var Para0042 = bParas.Where(w => w.ParaNo == "Pre_OrderBarCode_DefaultPara_0042").First();//预制条码是否校验
            var Para0043 = bParas.Where(w => w.ParaNo == "Pre_OrderBarCode_DefaultPara_0043").First();//绑定预制条码时是否自动确认
            var Para0047 = bParas.Where(w => w.ParaNo == "Pre_OrderBarCode_DefaultPara_0047").First();//预制条码校验位数
            var Para0048 = bParas.Where(w => w.ParaNo == "Pre_OrderBarCode_DefaultPara_0048").First();//预制条码特殊校验位数
            LisBarCodeForm lisBarCodeForm = DBDao.Get(long.Parse(barcodeformid));
            if (!string.IsNullOrEmpty(Para0042.ParaValue) && Para0042.ParaValue == "1")
            {
                if (lisBarCodeForm.SamplingGroupID != 0 && lisBarCodeForm.SamplingGroupID != null ) {
                    var lBSamplingGroup = IDLBSamplingGroupDao.Get(long.Parse(lisBarCodeForm.SamplingGroupID.ToString()));
                    if (!string.IsNullOrEmpty(lBSamplingGroup.PrepInfo) && barcode.IndexOf(lBSamplingGroup.PrepInfo) != 0)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "条码号管头校验未通过！";
                        return baseResultDataValue;
                    }
                }
                int verifynum = 0;
                if (!string.IsNullOrEmpty(Para0048.ParaValue))
                {
                    var sgarr = Para0048.ParaValue.Split(',');
                    if (sgarr.Length > 0)
                    {
                        foreach (var sg in sgarr)
                        {
                            if (sg.Split('-')[0] == lisBarCodeForm.SamplingGroupID + "")
                            {
                                if (barcode.Length != int.Parse(sg.Split('-')[1]))
                                {
                                    baseResultDataValue.success = false;
                                    baseResultDataValue.ErrorInfo = "条码号位数校验未通过！";
                                    return baseResultDataValue;
                                }
                                verifynum++;
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(Para0047.ParaValue) && Para0047.ParaValue != "0" && verifynum == 0)
                {
                    if (barcode.Length != int.Parse(Para0047.ParaValue))
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "条码号位数校验未通过！";
                        return baseResultDataValue;
                    }
                }
            }
            lisBarCodeForm.BarCode = barcode;
            if (!string.IsNullOrEmpty(Para0043.ParaValue) && Para0043.ParaValue == "1") {
                lisBarCodeForm.BarCodeStatusID = 2;
                lisBarCodeForm.IsAffirm = 1;
                lisBarCodeForm.AffirmTime = DateTime.Now;
            }
            lisBarCodeForm.DataUpdateTime = DateTime.Now;
            if (DBDao.Update(lisBarCodeForm))
            {
                baseResultDataValue.success =  true;
            }
            else {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "更新条码号失败！";
            }
            
            return baseResultDataValue;
        }
        /// <summary>
        /// 撤销确认数据查询及校验
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="barcodes"></param>
        /// <param name="userid"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public BaseResultDataValue GetSampleAffirmDataAndVerifyByBarCode(long nodetype, string barcodes, string userid, string username)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (string.IsNullOrEmpty(barcodes))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "条码号不可为空！";
                return baseResultDataValue;
            }
            List<BPara> bParas = IBBParaItem.SelPreBParas(nodetype, "", Pre_AllModules.样本条码.Value.DefaultValue, userid, username);
            var isVerify = bParas.Where(w => w.ParaNo == "Pre_OrderBarCode_DefaultPara_0081").First();//是否开启撤销采集校验
            IList<LisBarCodeForm> lisBarCodeForms = this.SearchListByHQL("BarCode = '" + barcodes + "'");
            if (lisBarCodeForms != null && lisBarCodeForms.Count > 0)
            {
                List<LisBarCodeFormVo> lisBarCodeFormVos = this.ConvertLisBarCodeFormVo(lisBarCodeForms.ToList());
                if (isVerify.ParaValue == "1")
                {
                    string aa = "Pre_OrderBarCode_DefaultPara_0082,Pre_OrderBarCode_DefaultPara_0083,Pre_OrderBarCode_DefaultPara_0084,Pre_OrderBarCode_DefaultPara_0085,Pre_OrderBarCode_DefaultPara_0086";
                    List<BPara> verifybparas = new List<BPara>();
                    foreach (var bpara in bParas)
                    {
                        if (aa.IndexOf(bpara.ParaNo) > -1)
                        {
                            verifybparas.Add(bpara);
                        }
                    }
                    for (int i = 0; i < lisBarCodeFormVos.Count(); i++)
                    {
                        if (lisBarCodeFormVos[i].LisBarCodeForm.BarCodeStatusID < long.Parse(BarCodeStatus.条码确认.Value.Code) || lisBarCodeFormVos[i].LisBarCodeForm.AffirmTime == null)
                        {
                            List<VerifyReminInfo> verifyReminInfos = new List<VerifyReminInfo>();
                            VerifyReminInfo verifyReminInfo = new VerifyReminInfo();
                            verifyReminInfo.alterMode = "2";
                            verifyReminInfo.failureInfo = "当前样本未确认，不能撤销确认！";
                            verifyReminInfos.Add(verifyReminInfo);
                            lisBarCodeFormVos[i].failureInfo = Newtonsoft.Json.JsonConvert.SerializeObject(verifyReminInfos);
                        }
                        else {
                            lisBarCodeFormVos[i] = RevocationAffirmBarCodeFormVerify(verifybparas, lisBarCodeFormVos[i]);
                        }
                        
                    }
                }
                baseResultDataValue.success = true;
                baseResultDataValue.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(lisBarCodeFormVos);
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "未查询到样本数据！";
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 撤销确认校验
        /// </summary>
        /// <param name="bParas"></param>
        /// <param name="lisBarCodeFormVO"></param>
        /// <returns></returns>
        public LisBarCodeFormVo RevocationAffirmBarCodeFormVerify(List<BPara> bParas, LisBarCodeFormVo lisBarCodeFormVO)
        {
            #region
            List<VerifyReminInfo> verifyReminInfos = new List<VerifyReminInfo>();
            foreach (var bPara in bParas)
            {
                VerifyReminInfo verifyReminInfo = new VerifyReminInfo();
                verifyReminInfo.alterMode = "2";
                bool isreturn = false;
                if (bPara.ParaNo == "Pre_OrderBarCode_DefaultPara_0082" && bPara.ParaValue == "1" && (lisBarCodeFormVO.LisBarCodeForm.BarCodeStatusID >= long.Parse(BarCodeStatus.样本采集.Value.Code) || lisBarCodeFormVO.LisBarCodeForm.CollectTime != null))
                {
                    isreturn = true;
                    verifyReminInfo.failureInfo = "已进行采集不能撤销确认！";
                    verifyReminInfos.Add(verifyReminInfo);
                }
                else if (bPara.ParaNo == "Pre_OrderBarCode_DefaultPara_0083" && bPara.ParaValue == "1" && (lisBarCodeFormVO.LisBarCodeForm.BarCodeStatusID >= long.Parse(BarCodeStatus.样本送检.Value.Code) || lisBarCodeFormVO.LisBarCodeForm.SendTime != null))
                {
                    isreturn = true;
                    verifyReminInfo.failureInfo = "已进行送检不能撤销确认！";
                    verifyReminInfos.Add(verifyReminInfo);
                }
                else if (bPara.ParaNo == "Pre_OrderBarCode_DefaultPara_0084" && bPara.ParaValue == "1" && (lisBarCodeFormVO.LisBarCodeForm.BarCodeStatusID >= long.Parse(BarCodeStatus.样本签收.Value.Code) || lisBarCodeFormVO.LisBarCodeForm.InceptTime != null))
                {
                    isreturn = true;
                    verifyReminInfo.failureInfo = "已进行签收不能撤销确认！";
                    verifyReminInfos.Add(verifyReminInfo);
                }
                else if (bPara.ParaNo == "Pre_OrderBarCode_DefaultPara_0085" && bPara.ParaValue == "1" && (lisBarCodeFormVO.LisBarCodeForm.BarCodeStatusID >= long.Parse(BarCodeStatus.样本核收.Value.Code) || lisBarCodeFormVO.LisBarCodeForm.ReceiveTime != null))
                {
                    isreturn = true;
                    verifyReminInfo.failureInfo = "已进行核收不能撤销确认！";
                    verifyReminInfos.Add(verifyReminInfo);
                }
                else if (bPara.ParaNo == "Pre_OrderBarCode_DefaultPara_0086" && bPara.ParaValue == "1" && (lisBarCodeFormVO.LisBarCodeForm.BarCodeStatusID >= long.Parse(BarCodeStatus.报告发布.Value.Code) || lisBarCodeFormVO.LisBarCodeForm.ReportSendTime != null))
                {
                    isreturn = true;
                    verifyReminInfo.failureInfo = "已发布报告不能撤销确认！";
                    verifyReminInfos.Add(verifyReminInfo);
                }
                if (isreturn)
                {
                    lisBarCodeFormVO.failureInfo = Newtonsoft.Json.JsonConvert.SerializeObject(verifyReminInfos);
                    return lisBarCodeFormVO;
                }
            }
            return lisBarCodeFormVO;
            #endregion
        }
        /// <summary>
        /// 撤销确认
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="barcodes"></param>
        /// <param name="userid"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public BaseResultDataValue EditBarCodeFormRevocationAffirm(long nodetype, string barcodes, string userid, string username)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            List<BPara> bParas = IBBParaItem.SelPreBParas(nodetype, "", Pre_AllModules.样本条码.Value.DefaultValue, userid, username);

            string[] barcodearr = barcodes.Split(',');
            string where = "BarCode in ('" + string.Join("','", barcodearr) + "')";
            IList<LisBarCodeForm> lisBarCodeForms = this.SearchListByHQL(where).ToList();
            if (lisBarCodeForms.Count <= 0 || lisBarCodeForms == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ResultDataValue = "未查询到此条码号信息！";
                return baseResultDataValue;
            }
            List<long> formids = new List<long>();
            foreach (var form in lisBarCodeForms)
            {
                if (!formids.Contains(form.Id))
                    formids.Add(form.Id);
            }
            IList<LisBarCodeItem> lisBarCodeItems = IDLisBarCodeItemDao.GetListByHQL("LisBarCodeForm.Id in (" + string.Join(",", formids) + ")");
            List<string> resultInfo = new List<string>();
            foreach (var form in lisBarCodeForms)
            {
                long status = long.Parse(BarCodeStatus.生成条码.Value.Code);
                if (form.BarCodeStatusID > status)
                {
                    form.BarCodeStatusID = status;
                    form.BarCodeCurrentStatus = BarCodeStatus.生成条码.Value.Name;
                    form.IsAffirm = 0;
                    form.AffirmTime = null;
                    form.DataUpdateTime = DateTime.Now;
                    var barcodeitems = lisBarCodeItems.Where(a => a.LisBarCodeForm.Id == form.Id).ToList();
                    this.Entity = form;
                    if (this.Edit())
                    {
                        IBLisOperate.AddLisOperate(form, OrderFromOperateType.撤销确认.Value, "撤销确认", SysCookieValue);
                        foreach (var item in barcodeitems)
                        {
                            item.CollectTime = null;
                            item.DataUpdateTime = DateTime.Now;
                            item.ItemStatusID = status;
                            if (IDLisBarCodeItemDao.Update(item))
                            {
                                IBLisOperate.AddLisOperate(item, OrderFromOperateType.撤销确认.Value, "撤销确认", SysCookieValue);
                            }
                            else
                            {
                                baseResultDataValue.success = false;
                                baseResultDataValue.ErrorInfo = "撤销失败！";
                                return baseResultDataValue;
                            }
                        }
                        resultInfo.Add("{\"" + form.BarCode + "\":\"true\"}");
                    }
                    else
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "撤销失败！";
                        return baseResultDataValue;
                    }
                }
                else
                {
                    resultInfo.Add("{\"" + form.BarCode + "\":\"false\"}");
                }
            }
            baseResultDataValue.success = true;
            baseResultDataValue.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(resultInfo);
            return baseResultDataValue;
        }

        #endregion
        #region 样本采集
        /// <summary>
        ///  根据条码号获取样本
        /// </summary>
        /// <returns></returns>
        public List<LisBarCodeFormVo> GetSampleGatherWantDataByBarCode(long nodetype, string barcode, string userid, string username, out bool topo_isupdate)
        {
            List<LisBarCodeFormVo> lisBarCodeFormVos = new List<LisBarCodeFormVo>();
            if (string.IsNullOrEmpty(barcode))
            {
                topo_isupdate = false;
                return lisBarCodeFormVos;
            }
            topo_isupdate = true;
            #region 系统参数处理
            List<BPara> bParas = IBBParaItem.SelPreBParas(nodetype, "", Pre_AllModules.样本采集.Value.DefaultValue, userid, username);
            var sicktypes = bParas.Where(w => w.ParaNo == "Pre_OrderGether_DefaultPara_0001").First();//就诊类型
            var HISGetUrl = bParas.Where(w => w.ParaNo == "Pre_OrderGether_DefaultPara_0002").First();//核收接口地址
            var isVerify = bParas.Where(w => w.ParaNo == "Pre_OrderGether_DefaultPara_0007").First();//是否采集校验
            #endregion  
            string strwhere = "";
            string[] barcodearr = barcode.Split(',');
            strwhere = "BarCode in ('" + string.Join("','", barcodearr) + "')";
            if (!string.IsNullOrEmpty(sicktypes.ParaValue))
            {               
                strwhere += " and LisPatient.SickTypeID in (" + sicktypes.ParaValue + ")";
            }
            IList<LisBarCodeForm> lisBarCodeForms = this.SearchListByHQL(strwhere);
            if (lisBarCodeForms.Count > 0)
            {
                lisBarCodeFormVos = this.ConvertLisBarCodeFormVo(lisBarCodeForms.ToList());
                LisBarCodeFormVo lisBarCodeFormVo = lisBarCodeFormVos.First();
                #region 校验
                if (isVerify.ParaValue == "1")
                {
                    string paranos = "Pre_OrderGether_DefaultPara_0015,Pre_OrderGether_DefaultPara_0016,Pre_OrderGether_DefaultPara_0017,Pre_OrderGether_DefaultPara_0018";
                    lisBarCodeFormVos[0] = GatherBarCodeFormVerify(bParas, lisBarCodeFormVo, paranos,1,out topo_isupdate);
                }
                #endregion
            }
            //判断是否从HIS获取数据
            if (!string.IsNullOrEmpty(HISGetUrl.ParaValue))
            {

            }
            return lisBarCodeFormVos;
        }
        /// <summary>
        /// 样本采集校验和样本送检校验  modeltype：1样本采集2样本送检
        /// </summary>
        /// <param name="bParas"></param>
        /// <param name="lisBarCodeFormVO"></param>
        /// <param name="paranos"></param>
        /// <param name="modeltype"></param>
        /// <param name="topo_isupdate"></param>
        /// <returns></returns>
        public LisBarCodeFormVo GatherBarCodeFormVerify(List<BPara> bParas, LisBarCodeFormVo lisBarCodeFormVO,string paranos ,int modeltype,out bool topo_isupdate) 
        {
            topo_isupdate = true;
            List<BPara> verifybparas = new List<BPara>();
            foreach (var bpara in bParas)
            {
                if (paranos.IndexOf(bpara.ParaNo) > -1)
                {
                    verifybparas.Add(bpara);
                }
            }
            #region 校验
            var paragroups = verifybparas.GroupBy(a => a.ParaValue);
            List<VerifyReminInfo> verifyReminInfos = new List<VerifyReminInfo>();
            #region 不允许不提醒
            var verifyinfo1 = paragroups.Where(a => a.Key == "2");
            if (verifyinfo1 != null && verifyinfo1.Count() > 0)
            {
                foreach (var group in verifyinfo1.First())
                {
                    lisBarCodeFormVO.failureInfo = "";
                    LisBarCodeFormVo topo_lisBarCodeFormVO = new LisBarCodeFormVo();
                    if (modeltype == 1)
                    {
                        topo_lisBarCodeFormVO = GatherBarCodeFormVerify(group, lisBarCodeFormVO, "2");
                    }
                    else if (modeltype == 2)
                    {
                        topo_lisBarCodeFormVO = ExchangeInspectBarCodeFormVerify(group, lisBarCodeFormVO, "2");
                    }
                    if (!string.IsNullOrEmpty(topo_lisBarCodeFormVO.failureInfo))
                    {
                        topo_isupdate = false;
                        VerifyReminInfo verifyReminInfo = new VerifyReminInfo();
                        verifyReminInfo.alterMode = "2";
                        verifyReminInfo.failureInfo = lisBarCodeFormVO.failureInfo;
                        verifyReminInfos.Add(verifyReminInfo);
                        lisBarCodeFormVO.failureInfo = Newtonsoft.Json.JsonConvert.SerializeObject(verifyReminInfos);
                        return topo_lisBarCodeFormVO;
                    }
                }
            }
            #endregion
            #region 不允许但提醒
            var verifyinfo2 = paragroups.Where(a => a.Key == "1");
            if (verifyinfo2 != null && verifyinfo2.Count() > 0)
            {
                foreach (var group in verifyinfo2.First())
                {
                    lisBarCodeFormVO.failureInfo = "";
                    LisBarCodeFormVo topo_lisBarCodeFormVO = new LisBarCodeFormVo();
                    if (modeltype == 1)
                    {
                        topo_lisBarCodeFormVO = GatherBarCodeFormVerify(group, lisBarCodeFormVO, "1");
                    }
                    else if (modeltype == 2)
                    {
                        topo_lisBarCodeFormVO = ExchangeInspectBarCodeFormVerify(group, lisBarCodeFormVO, "1");
                    }
                    if (!string.IsNullOrEmpty(topo_lisBarCodeFormVO.failureInfo))
                    {
                        topo_isupdate = false;
                        VerifyReminInfo verifyReminInfo = new VerifyReminInfo();
                        verifyReminInfo.alterMode = "1";
                        verifyReminInfo.failureInfo = lisBarCodeFormVO.failureInfo;
                        verifyReminInfos.Add(verifyReminInfo);
                        lisBarCodeFormVO.failureInfo = Newtonsoft.Json.JsonConvert.SerializeObject(verifyReminInfos);
                        topo_isupdate = false;
                        return topo_lisBarCodeFormVO;
                    }
                }
            }
            #endregion
            #region 用户交互
            var verifyinfo3 = paragroups.Where(a => a.Key == "4");
            if (verifyinfo3 != null && verifyinfo3.Count() > 0)
            {
                foreach (var group in verifyinfo3.First())
                {
                    lisBarCodeFormVO.failureInfo = "";
                    LisBarCodeFormVo topo_lisBarCodeFormVO = new LisBarCodeFormVo();
                    if (modeltype == 1)
                    {
                        topo_lisBarCodeFormVO = GatherBarCodeFormVerify(group, lisBarCodeFormVO, "4");
                    }
                    else if (modeltype == 2)
                    {
                        topo_lisBarCodeFormVO = ExchangeInspectBarCodeFormVerify(group, lisBarCodeFormVO, "4");
                    }
                    if (!string.IsNullOrEmpty(topo_lisBarCodeFormVO.failureInfo))
                    {
                        topo_isupdate = false;
                        VerifyReminInfo verifyReminInfo = new VerifyReminInfo();
                        verifyReminInfo.alterMode = "4";
                        verifyReminInfo.failureInfo = topo_lisBarCodeFormVO.failureInfo;
                        verifyReminInfos.Add(verifyReminInfo);
                    }
                }
            }
            #endregion
            #region 允许并提醒
            var verifyinfo4 = paragroups.Where(a => a.Key == "3");
            if (verifyinfo4 != null && verifyinfo4.Count() > 0)
            {
                foreach (var group in verifyinfo4.First())
                {
                    lisBarCodeFormVO.failureInfo = "";
                    LisBarCodeFormVo topo_lisBarCodeFormVO = new LisBarCodeFormVo();
                    if (modeltype == 1)
                    {
                        topo_lisBarCodeFormVO = GatherBarCodeFormVerify(group, lisBarCodeFormVO, "3");
                    }
                    else if (modeltype == 2)
                    {
                        topo_lisBarCodeFormVO = ExchangeInspectBarCodeFormVerify(group, lisBarCodeFormVO, "3");
                    }
                    if (!string.IsNullOrEmpty(topo_lisBarCodeFormVO.failureInfo))
                    {
                        VerifyReminInfo verifyReminInfo = new VerifyReminInfo();
                        verifyReminInfo.alterMode = "3";
                        verifyReminInfo.failureInfo = topo_lisBarCodeFormVO.failureInfo;
                        verifyReminInfos.Add(verifyReminInfo);
                    }
                }
            }
            #endregion
            lisBarCodeFormVO.failureInfo = Newtonsoft.Json.JsonConvert.SerializeObject(verifyReminInfos);
            #endregion
            return lisBarCodeFormVO;
        }
        public LisBarCodeFormVo GatherBarCodeFormVerify(BPara bPara, LisBarCodeFormVo lisBarCodeFormVO, string status)
        {
            #region
            if (bPara.ParaNo == "Pre_OrderGether_DefaultPara_0015" && lisBarCodeFormVO.LisBarCodeForm.BarCodeStatusID <= 4 && lisBarCodeFormVO.LisBarCodeForm.CollectTime != null)
            {
                #region 处理
                if (status == "2")
                {
                    lisBarCodeFormVO.failureInfo = "未查询到符合的样本单!";
                }
                else if (status == "1")
                {
                    lisBarCodeFormVO.failureInfo = "已进行采集确认不能再次采集确认!";
                }
                else if (status == "4")
                {
                    lisBarCodeFormVO.failureInfo = "当前样本已采集确认是否再次采集确认!";
                }
                else if (status == "3")
                {
                    lisBarCodeFormVO.failureInfo = "当前样本已采集确认，再次采集确认成功！";
                }
                #endregion
            }
            else if (bPara.ParaNo == "Pre_OrderGether_DefaultPara_0016" && lisBarCodeFormVO.LisBarCodeForm.BarCodeStatusID >=5 && lisBarCodeFormVO.LisBarCodeForm.SendTime != null)
            {
                #region 处理
                if (status == "2")
                {
                    lisBarCodeFormVO.failureInfo = "未查询到符合的样本单!";
                }
                else if (status == "1")
                {

                    lisBarCodeFormVO.failureInfo = "已进行送检确认不能再次采集确认!";
                }
                else if (status == "4")
                {
                    lisBarCodeFormVO.failureInfo = "当前样本已送检确认是否再次采集确认!";
                }
                else if (status == "3")
                {
                    lisBarCodeFormVO.failureInfo = "当前样本已送检确认，再次采集确认成功！";
                }
                #endregion
            }
            else if (bPara.ParaNo == "Pre_OrderGether_DefaultPara_0017" && lisBarCodeFormVO.LisBarCodeForm.BarCodeStatusID >= 7 && lisBarCodeFormVO.LisBarCodeForm.InceptTime != null)
            {
                #region 处理
                if (status == "2")
                {
                    lisBarCodeFormVO.failureInfo = "未查询到符合的样本单!";
                }
                else if (status == "1")
                {
                    lisBarCodeFormVO.failureInfo = "已签收不能再次采集确认!";
                }
                else if (status == "4")
                {
                    lisBarCodeFormVO.failureInfo = "当前样本已签收是否再次采集确认!";
                }
                else if (status == "3")
                {
                    lisBarCodeFormVO.failureInfo = "当前样本已签收，再次采样确认成功！";
                }
                #endregion
            }
            else if (bPara.ParaNo == "Pre_OrderGether_DefaultPara_0018" && lisBarCodeFormVO.LisBarCodeForm.BarCodeStatusID >= 11 && lisBarCodeFormVO.LisBarCodeForm.ReceiveTime != null)
            {
                #region 处理
                if (status == "2")
                {
                    lisBarCodeFormVO.failureInfo = "未查询到符合的样本单!";
                }
                else if (status == "1")
                {
                    lisBarCodeFormVO.failureInfo = "已核收不能再次采集确认!";
                }
                else if (status == "4")
                {
                    lisBarCodeFormVO.failureInfo = "当前样本已核收是否再次采集确认!";
                }
                else if (status == "3")
                {
                    lisBarCodeFormVO.failureInfo = "当前样本已核收，再次采集确认成功！";
                }
                #endregion
            }
            return lisBarCodeFormVO;
            #endregion
        }
        /// <summary>
        /// 获取样本数据
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="barcodes"></param>
        /// <param name="userid"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public List<LisBarCodeFormVo> GetSampleGatherWantData(long nodetype, bool? iscallhis, string where, string barcodes, string userid, string username)
        {
            List<LisBarCodeFormVo> lisBarCodeFormVos = new List<LisBarCodeFormVo>();
            if (string.IsNullOrEmpty(barcodes) && string.IsNullOrEmpty(where)) {
                return lisBarCodeFormVos;
            }
            #region 系统参数处理
            List<BPara> bParas = IBBParaItem.SelPreBParas(nodetype,"", Pre_AllModules.样本采集.Value.DefaultValue, userid,username);
            var sicktypes = bParas.Where(w => w.ParaNo == "Pre_OrderGether_DefaultPara_0001").First();//就诊类型
            var HISGetUrl = bParas.Where(w => w.ParaNo == "Pre_OrderGether_DefaultPara_0002").First();//核收接口地址
            #endregion  
            string strwhere = "";
            if (string.IsNullOrEmpty(where))
            {
                string[] barcodearr = barcodes.Split(',');
                strwhere = "BarCode in ('" + string.Join("','", barcodearr) + "')";
                if (!string.IsNullOrEmpty(sicktypes.ParaValue))
                {
                    strwhere += " and LisPatient.SickTypeID in ('" + sicktypes.ParaValue + "')";
                }
            }
            else 
            {
                strwhere = where;
            }
            IList<LisBarCodeForm> lisBarCodeForms  = this.SearchListByHQL(strwhere);
            if (lisBarCodeForms.Count > 0)
            {
                lisBarCodeFormVos = this.ConvertLisBarCodeFormVo(lisBarCodeForms.ToList());

            }
            else if(iscallhis == null || iscallhis == true)
            {
                //判断是否从HIS获取数据
                if (!string.IsNullOrEmpty(HISGetUrl.ParaValue) && string.IsNullOrEmpty(where)) { 
                
                }
            }
            return lisBarCodeFormVos;
        }
        /// <summary>
        /// 数据类型转化
        /// </summary>
        /// <param name="lisBarCodeForms"></param>
        /// <returns></returns>
        public List<LisBarCodeFormVo> ConvertLisBarCodeFormVo(List<LisBarCodeForm> lisBarCodeForms)
        {
            List<LisBarCodeFormVo> lisBarCodeFormVos = new List<LisBarCodeFormVo>();
            if (lisBarCodeForms.Count == 0)
            {
                return lisBarCodeFormVos;
            }
            foreach (var form in lisBarCodeForms)
            {
                LisBarCodeFormVo lisBarCodeFormvo = new LisBarCodeFormVo();
                lisBarCodeFormvo.LisBarCodeForm = form;
                if (form.HospitalID != null && form.HospitalID != 0)
                {
                    IList<ZhiFang.Entity.LIIP.HREmpIdentity> hospital = LIIPHelp.RBAC_UDTO_SearchHRDeptIdentityByHQL("hrdeptidentity.TSysCode='" + DeptSystemType.院区.Key + "' and hrdeptidentity.SystemCode='ZF_PRE'").toList<ZhiFang.Entity.LIIP.HREmpIdentity>();
                    if (hospital.Count > 0)
                    {
                        var hREmps = hospital.Where(w => w.Id == form.HospitalID);
                        if (hREmps.Count() > 0)
                        {
                            lisBarCodeFormvo.HospitalName = hREmps.First().CName;
                        }
                    }
                }
                if (form.ClientID != null && form.ClientID != 0)
                {
                    IList<ZhiFang.Entity.LIIP.HREmpIdentity> client = LIIPHelp.RBAC_UDTO_SearchHRDeptIdentityByHQL("hrdeptidentity.TSysCode='" + DeptSystemType.送检客户.Key + "' and hrdeptidentity.SystemCode='ZF_PRE'").toList<ZhiFang.Entity.LIIP.HREmpIdentity>();
                    if (client.Count > 0)
                    {
                        var hREmps = client.Where(w => w.Id == form.ClientID);
                        if (hREmps.Count() > 0)
                        {
                            lisBarCodeFormvo.ClientName = hREmps.First().CName;
                        }
                    }
                }
                if (form.ExecDeptID != null && form.ExecDeptID != 0)
                {
                    IList<ZhiFang.Entity.LIIP.HREmpIdentity> execdept = LIIPHelp.RBAC_UDTO_SearchHRDeptIdentityByHQL("hrdeptidentity.TSysCode='" + DeptSystemType.执行科室.Key + "' and hrdeptidentity.SystemCode='ZF_PRE'").toList<ZhiFang.Entity.LIIP.HREmpIdentity>();
                    if (execdept.Count > 0)
                    {
                        var hREmps = execdept.Where(w => w.Id == form.ExecDeptID);
                        if (hREmps.Count() > 0)
                        {
                            lisBarCodeFormvo.ExecDeptName = hREmps.First().CName;
                        }
                    }
                }
                if (form.DestinationID != null && form.DestinationID != 0)
                {
                    LBDestination lBDestination = IDLBDestinationDao.Get(long.Parse(form.DestinationID.ToString()));
                    lisBarCodeFormvo.DestinationName = lBDestination.CName;
                }
                if (form.SampleTypeID != null && form.SampleTypeID != 0)
                {
                    LBSampleType lBSampleType = IDLBSampleTypeDao.Get(long.Parse(form.SampleTypeID.ToString()));
                    lisBarCodeFormvo.SampleTypeName = lBSampleType.CName;
                }
                lisBarCodeFormvo.OrderTypeName = "";//暂不知道从哪取
                if (form.SamplingGroupID != null && form.SamplingGroupID != 0) { 
                    lisBarCodeFormvo.SamplingGroupName = IDLBSamplingGroupDao.Get(long.Parse(form.SamplingGroupID.ToString())).CName;
                }
                lisBarCodeFormVos.Add(lisBarCodeFormvo);
            }
            return lisBarCodeFormVos;
        }
        /// <summary>
        /// 样本采集状态更新
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="barcodes"></param>
        /// <param name="lisBarCodeFormVos"></param>
        /// <param name="userid"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public List<LisBarCodeFormVo> EditBarCodeFormCollect(long nodetype, string barcodes, List<LisBarCodeFormVo> lisBarCodeFormVos, string userid,string username, bool? isConstraintUpdate = false)
        {
            List<LisBarCodeFormVo> barcodeformlist = new List<LisBarCodeFormVo>();
            List<BPara> bParas = IBBParaItem.SelPreBParas(nodetype, "", Pre_AllModules.样本采集.Value.DefaultValue, userid, username);
            var HISUrl = bParas.Where(w => w.ParaNo == "Pre_OrderGether_DefaultPara_0004").First();//HIS采样确认接口
            var isverify = bParas.Where(w => w.ParaNo == "Pre_OrderGether_DefaultPara_0007").First();//采样确认时是否校验
            var barcodestatus  = bParas.Where(w => w.ParaNo == "Pre_OrderGether_DefaultPara_0014").First();//需要补录的状态
            if (lisBarCodeFormVos == null || lisBarCodeFormVos.Count <= 0 && string.IsNullOrEmpty(barcodes)) {
                return barcodeformlist;
            }
            if (lisBarCodeFormVos == null || lisBarCodeFormVos.Count <= 0 && !string.IsNullOrEmpty(barcodes)) {
                string[] barcodearr = barcodes.Split(',');
                string where = "BarCode in ('" + string.Join("','", barcodearr) + "')";
                IList<LisBarCodeForm> lisBarCodeForms = this.SearchListByHQL(where).ToList();
                lisBarCodeFormVos = ConvertLisBarCodeFormVo(lisBarCodeForms.ToList());
            }
            
            if (!string.IsNullOrEmpty(isverify.ParaValue) && (isConstraintUpdate == null || isConstraintUpdate == false))
            {
                //采集验证
                string paranos = "Pre_OrderGether_DefaultPara_0015,Pre_OrderGether_DefaultPara_0016,Pre_OrderGether_DefaultPara_0017,Pre_OrderGether_DefaultPara_0018";
                for (int i = 0; i < lisBarCodeFormVos.Count(); i++)
                {
                    lisBarCodeFormVos[i].isConstraintUpdate = true;
                    bool isedit;
                    lisBarCodeFormVos[i] = GatherBarCodeFormVerify(bParas, lisBarCodeFormVos[i], paranos,1, out isedit);
                    lisBarCodeFormVos[i].isConstraintUpdate = isedit;
                }
            }
            List<long> formids = new List<long>();
            foreach (var form in lisBarCodeFormVos)
            {
                if (!formids.Contains(form.LisBarCodeForm.Id))
                    formids.Add(form.LisBarCodeForm.Id);
            }
            IList<LisBarCodeItem> lisBarCodeItems = IDLisBarCodeItemDao.GetListByHQL("LisBarCodeForm.Id in (" + string.Join(",", formids) + ")");
            foreach (var form in lisBarCodeFormVos)
            {
                if (isConstraintUpdate == true || form.isConstraintUpdate == true) {
                    form.LisBarCodeForm = StatusDateTimeInfoReplenish(form.LisBarCodeForm, barcodestatus.ParaValue);//状态补录
                    long status = long.Parse(BarCodeStatus.样本采集.Value.Code);
                    form.LisBarCodeForm.BarCodeStatusID = status;
                    form.LisBarCodeForm.BarCodeCurrentStatus = BarCodeStatus.样本采集.Value.Name;
                    form.LisBarCodeForm.CollectTime = DateTime.Now;
                    form.LisBarCodeForm.DataUpdateTime = DateTime.Now;
                    var barcodeitems = lisBarCodeItems.Where(a => a.LisBarCodeForm.Id == form.LisBarCodeForm.Id).ToList();
                    this.Entity = form.LisBarCodeForm;
                    if (this.Edit())
                    {
                        IBLisOperate.AddLisOperate(form.LisBarCodeForm, BarCodeStatus.样本采集.Value, "样本采集", SysCookieValue);
                        foreach (var item in barcodeitems)
                        {
                            item.CollectTime = DateTime.Now;
                            item.DataUpdateTime = DateTime.Now;
                            item.ItemStatusID = status;
                            if (IDLisBarCodeItemDao.Update(item))
                            {
                                IBLisOperate.AddLisOperate(item, BarCodeStatus.样本采集.Value, "样本采集", SysCookieValue);
                            }
                        }
                    }
                }
                barcodeformlist.Add(form);
            }
            if (!string.IsNullOrEmpty(HISUrl.ParaValue))
            {
                //调用HIS样本采集确认接口 
            }
            return barcodeformlist;
        }
        /// <summary>
        /// 根据核收条件从HIS核收数据并存入LIS库返回数据列表
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="receiveType"></param>
        /// <param name="value"></param>
        /// <param name="userid"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public List<LisBarCodeFormVo> GetBarCodeFromByCheckWhereAndAddBarCodeForm(long nodetype, string receiveType, string value, string userid, string username)
        {
            List<LisBarCodeFormVo> lisBarCodeFormVos = new List<LisBarCodeFormVo>();
            #region 系统参数处理
            List<BPara> bParas = IBBParaItem.SelPreBParas(nodetype, "", Pre_AllModules.样本采集.Value.DefaultValue, userid, username);
            var sicktypes = bParas.Where(w => w.ParaNo == "Pre_OrderGether_DefaultPara_0001").First();//就诊类型
            var HISGetUrl = bParas.Where(w => w.ParaNo == "Pre_OrderGether_DefaultPara_0002").First();//核收接口地址
            #endregion  
            string strwhere = receiveType+" = '"+ value + "'";
            if (!string.IsNullOrEmpty(sicktypes.ParaValue))
            {                
                strwhere += " and LisPatient.SickTypeID in (" + sicktypes.ParaValue + ")";
            }
            strwhere += " and BarCodeStatusID < " + BarCodeStatus.样本采集.Value.Code + " and CollectTime is null";
            IList<LisBarCodeForm> lisBarCodeForms = this.SearchListByHQL(strwhere);
            if (lisBarCodeForms != null && lisBarCodeForms.Count > 0)
            {
                lisBarCodeFormVos = ConvertLisBarCodeFormVo(lisBarCodeForms.ToList());
            }
            //判断是否从HIS获取数据
            if (!string.IsNullOrEmpty(HISGetUrl.ParaValue) && (lisBarCodeForms == null || lisBarCodeForms.Count == 0)) { 
            
            }
            return lisBarCodeFormVos;
        }
        /// <summary>
        /// 撤销采集
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="barcodes"></param>
        /// <param name="userid"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public BaseResultDataValue EditBarCodeFormRevocationCollect(long nodetype, string barcodes, string userid, string username)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            List<BPara> bParas = IBBParaItem.SelPreBParas(nodetype, "", Pre_AllModules.样本采集.Value.DefaultValue, userid, username);

            string[] barcodearr = barcodes.Split(',');
            string where = "BarCode in ('" + string.Join("','", barcodearr) + "')";
            IList<LisBarCodeForm> lisBarCodeForms = this.SearchListByHQL(where).ToList();
            if (lisBarCodeForms.Count <= 0 || lisBarCodeForms == null) {
                baseResultDataValue.success = false;
                baseResultDataValue.ResultDataValue = "未查询到此条码号信息！";
                return baseResultDataValue;
            }
            List<long> formids = new List<long>();
            foreach (var form in lisBarCodeForms)
            {
                if (!formids.Contains(form.Id))
                    formids.Add(form.Id);
            }
            IList<LisBarCodeItem> lisBarCodeItems = IDLisBarCodeItemDao.GetListByHQL("LisBarCodeForm.Id in (" + string.Join(",", formids) + ")");
            List<string> resultInfo = new List<string>();
            foreach (var form in lisBarCodeForms)
            {
                long status = long.Parse(BarCodeStatus.条码打印.Value.Code);
                if (form.BarCodeStatusID > status)
                {
                    form.BarCodeStatusID = status;
                    form.BarCodeCurrentStatus = BarCodeStatus.条码打印.Value.Name;
                    form.CollectTime = null;
                    form.DataUpdateTime = DateTime.Now;
                    var barcodeitems = lisBarCodeItems.Where(a => a.LisBarCodeForm.Id == form.Id).ToList();
                    this.Entity = form;
                    if (this.Edit())
                    {
                        IBLisOperate.AddLisOperate(form, OrderFromOperateType.撤销采集.Value, "撤销采集", SysCookieValue);
                        foreach (var item in barcodeitems)
                        {
                            item.CollectTime = null;
                            item.DataUpdateTime = DateTime.Now;
                            item.ItemStatusID = status;
                            if (IDLisBarCodeItemDao.Update(item))
                            {
                                IBLisOperate.AddLisOperate(item, OrderFromOperateType.撤销采集.Value, "撤销采集", SysCookieValue);
                            }
                            else
                            {
                                baseResultDataValue.success = false;
                                baseResultDataValue.ErrorInfo = "撤销失败！";
                                return baseResultDataValue;
                            }
                        }
                        resultInfo.Add("{\"" + form.BarCode + "\":\"true\"}");
                    }
                    else
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "撤销失败！";
                        return baseResultDataValue;
                    }
                }
                else 
                {
                    resultInfo.Add("{\""+ form .BarCode+ "\":\"false\"}");
                }
            }
            baseResultDataValue.success = true;
            baseResultDataValue.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(resultInfo);
            return baseResultDataValue;
        }
        /// <summary>
        /// 状态信息补录
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public LisBarCodeForm StatusDateTimeInfoReplenish(LisBarCodeForm entity,string status) {
            if (!string.IsNullOrEmpty(status)) {
                var statusarr = status.Split(',');
                foreach (var key in statusarr)
                {
                    if (key == BarCodeStatus.生成条码.Key)
                    {

                    } else if (key == BarCodeStatus.条码确认.Key) 
                    {
                        entity.AffirmTime = DateTime.Now;
                    }
                    else if (key == BarCodeStatus.条码打印.Key)
                    {
                        entity.PrintTime = DateTime.Now;
                    }
                    else if (key == BarCodeStatus.样本采集.Key)
                    {
                        entity.CollectTime = DateTime.Now;
                    }
                    else if (key == BarCodeStatus.样本送检.Key)
                    {
                        entity.SendTime = DateTime.Now;
                    }
                    else if (key == BarCodeStatus.样本送达.Key)
                    {
                        entity.ArriveTime = DateTime.Now;
                    }
                    else if (key == BarCodeStatus.样本签收.Key)
                    {
                        entity.InceptTime = DateTime.Now;
                    }
                    else if (key == BarCodeStatus.样本组内签收.Key)
                    {
                        entity.GroupInceptTime = DateTime.Now;
                    }
                    else if (key == BarCodeStatus.样本拒收.Key)
                    {
                        entity.RejectTime = DateTime.Now;
                    }
                    else if (key == BarCodeStatus.样本让步.Key)
                    {
                        entity.AllowTime = DateTime.Now;
                    }
                    else if (key == BarCodeStatus.样本核收.Key)
                    {
                        entity.ReceiveTime = DateTime.Now;
                    }
                    else if (key == BarCodeStatus.样本分发.Key)
                    {
                        entity.TranTime = DateTime.Now;
                    }
                    else if (key == BarCodeStatus.报告初审.Key)
                    {
                        entity.ReportConfirmTime = DateTime.Now;
                    }
                    else if (key == BarCodeStatus.报告终审.Key)
                    {
                        entity.ReportCheckTime = DateTime.Now;
                    }
                    else if (key == BarCodeStatus.报告发布.Key)
                    {
                        entity.ReportSendTime = DateTime.Now;
                    }
                    else if (key == BarCodeStatus.报告打印_技师站.Key)
                    {
                        entity.RepotPrintTimeLab = DateTime.Now;
                    }
                    else if (key == BarCodeStatus.报告打印_自助站.Key)
                    {
                        entity.RepotPrintTimeSelf = DateTime.Now;
                    }
                    else if (key == BarCodeStatus.报告打印_临床站.Key)
                    {
                        entity.RepotPrintTimeClinical = DateTime.Now;
                    }
                    else if (key == BarCodeStatus.检验终止.Key)
                    {
                        entity.AbortTime = DateTime.Now;
                    }
                }
                return entity;
            }
            else {
                return entity;
            }
        }
        /// <summary>
        /// 撤销采集数据获取并校验
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="barcodes"></param>
        /// <param name="userid"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public BaseResultDataValue GetSampleGatherDataAndVerifyByBarCode(long nodetype, string barcodes, string userid, string username)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (string.IsNullOrEmpty(barcodes))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "条码号不可为空！";
                return baseResultDataValue;
            }
            List<BPara> bParas = IBBParaItem.SelPreBParas(nodetype, "", Pre_AllModules.样本采集.Value.DefaultValue, userid, username);
            var isVerify = bParas.Where(w => w.ParaNo == "Pre_OrderGether_DefaultPara_0019").First();//是否开启撤销采集校验
            IList<LisBarCodeForm> lisBarCodeForms = this.SearchListByHQL("BarCode = '"+ barcodes+"'");
            if (lisBarCodeForms != null && lisBarCodeForms.Count > 0)
            {
                 List<LisBarCodeFormVo> lisBarCodeFormVos = this.ConvertLisBarCodeFormVo(lisBarCodeForms.ToList());
                if (isVerify.ParaValue == "1")
                {
                    string aa = "Pre_OrderGether_DefaultPara_0020,Pre_OrderGether_DefaultPara_0021,Pre_OrderGether_DefaultPara_0022,Pre_OrderGether_DefaultPara_0023";
                    List<BPara> verifybparas = new List<BPara>();
                    foreach (var bpara in bParas)
                    {
                        if (aa.IndexOf(bpara.ParaNo) > -1)
                        {
                            verifybparas.Add(bpara);
                        }
                    }
                    for (int i = 0; i < lisBarCodeFormVos.Count(); i++)
                    {
                        lisBarCodeFormVos[i] = RevocationGatherBarCodeFormVerify(verifybparas, lisBarCodeFormVos[i]);
                    }
                }
                baseResultDataValue.success = true;
                baseResultDataValue.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(lisBarCodeFormVos);
            }
            else {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "未查询到样本数据！";
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 撤销采集校验
        /// </summary>
        /// <param name="bParas"></param>
        /// <param name="lisBarCodeFormVO"></param>
        /// <returns></returns>
        public LisBarCodeFormVo RevocationGatherBarCodeFormVerify(List<BPara> bParas, LisBarCodeFormVo lisBarCodeFormVO)
        {
            #region
            List<VerifyReminInfo> verifyReminInfos = new List<VerifyReminInfo>(); 
            foreach (var bPara in bParas)
            {
                VerifyReminInfo verifyReminInfo = new VerifyReminInfo();
                verifyReminInfo.alterMode = "2";
                bool isreturn = false;
                if (bPara.ParaNo == "Pre_OrderGether_DefaultPara_0020" && bPara.ParaValue =="1" && lisBarCodeFormVO.LisBarCodeForm.BarCodeStatusID < 4 && lisBarCodeFormVO.LisBarCodeForm.CollectTime == null)
                {
                    isreturn = true;
                    verifyReminInfo.failureInfo = "未采集确认不能撤销采集确认！";
                    verifyReminInfos.Add(verifyReminInfo);
                }
                else if (bPara.ParaNo == "Pre_OrderGether_DefaultPara_0021" && bPara.ParaValue == "1" && lisBarCodeFormVO.LisBarCodeForm.BarCodeStatusID <= 5 && lisBarCodeFormVO.LisBarCodeForm.SendTime != null)
                {
                    isreturn = true;
                    verifyReminInfo.failureInfo = "已进行送检确认不能撤销采集确认！";
                    verifyReminInfos.Add(verifyReminInfo);
                }
                else if (bPara.ParaNo == "Pre_OrderGether_DefaultPara_0022" && bPara.ParaValue == "1" && lisBarCodeFormVO.LisBarCodeForm.BarCodeStatusID <= 7 && lisBarCodeFormVO.LisBarCodeForm.InceptTime != null)
                {
                    isreturn = true;
                    verifyReminInfo.failureInfo = "已签收不能撤销采集确认！";
                    verifyReminInfos.Add(verifyReminInfo);
                }
                else if (bPara.ParaNo == "Pre_OrderGether_DefaultPara_0023" && bPara.ParaValue == "1" && lisBarCodeFormVO.LisBarCodeForm.BarCodeStatusID <= 11 && lisBarCodeFormVO.LisBarCodeForm.ReceiveTime != null)
                {
                    isreturn = true;
                    verifyReminInfo.failureInfo = "已核收不能撤销采集确认！";
                    verifyReminInfos.Add(verifyReminInfo);
                }
                if (isreturn) {
                    lisBarCodeFormVO.failureInfo = Newtonsoft.Json.JsonConvert.SerializeObject(verifyReminInfos);
                    return lisBarCodeFormVO;
                }
            }
            return lisBarCodeFormVO;
            #endregion
        }

        public List<LisBarCodeFormVo> GetSampleGatherWantDataByWhere(long nodetype, string where, string userid, string username)
        {
            List<LisBarCodeFormVo> lisBarCodeFormVos = new List<LisBarCodeFormVo>();
            if (string.IsNullOrEmpty(where))
            {
                return lisBarCodeFormVos;
            }
            #region 系统参数处理
            List<BPara> bParas = IBBParaItem.SelPreBParas(nodetype, "", Pre_AllModules.样本采集.Value.DefaultValue, userid, username);
            var sicktypes = bParas.Where(w => w.ParaNo == "Pre_OrderGether_DefaultPara_0001").First();//就诊类型
            #endregion  
            string strwhere = where;
            if (!string.IsNullOrEmpty(sicktypes.ParaValue))
            {                
                strwhere += " and LisPatient.SickTypeID in (" + sicktypes.ParaValue + ")";
            }
            IList<LisBarCodeForm> lisBarCodeForms = this.SearchListByHQL(strwhere);
            if (lisBarCodeForms !=null && lisBarCodeForms.Count > 0) 
            {
                lisBarCodeFormVos = ConvertLisBarCodeFormVo(lisBarCodeForms.ToList());
            }
            return lisBarCodeFormVos;
        }
        /// <summary>
        /// 生成打包号
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="barcodes"></param>
        /// <param name="userid"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public BaseResultDataValue Edit_SampleGatherCreateCollectPackNoByBarCode(long nodetype, string barcodes, string userid, string username)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            List<LisBarCodeFormVo> lisBarCodeFormVos = new List<LisBarCodeFormVo>();
            List<BPara> bParas = IBBParaItem.SelPreBParas(nodetype, "", Pre_AllModules.样本采集.Value.DefaultValue, userid, username);
            var prefixPackNo = bParas.Where(w => w.ParaNo == "Pre_OrderGether_DefaultPara_0024").First();//打包号前缀
            string[] barcodearr = barcodes.Split(',');
            string where = "BarCode in ('" + string.Join("','", barcodearr) + "')";
            IList<LisBarCodeForm> lisBarCodeForms = this.SearchListByHQL(where);
            if (lisBarCodeForms != null && lisBarCodeForms.Count > 0)
            {
                //lisBarCodeFormVos = ConvertLisBarCodeFormVo(lisBarCodeForms.ToList());
                IList<LBTGetMaxNo> lBTGetMaxNos = IDLBTGetMaxNoDao.GetListByHQL("BmsTypeID=" + LBTGetMaxNOBmsTypes.样本采集打包号流水号.Key);
                string day = DateTime.Now.ToString("yyyyMMdd");
                string MaxID = "";
                if (lBTGetMaxNos.Count > 0)
                {
                    LBTGetMaxNo lBTGetMaxNo = lBTGetMaxNos[0];
                    if (lBTGetMaxNo.BmsDate.ToString("yyyyMMdd") != day)
                    {
                        lBTGetMaxNo.MaxID = "0";
                        lBTGetMaxNo.BmsDate = DateTime.Now;
                        lBTGetMaxNo.DataUpdateTime = DateTime.Now;
                        if (!IDLBTGetMaxNoDao.Update(lBTGetMaxNo))
                        {
                            baseResultDataValue.success = false;
                            baseResultDataValue.ErrorInfo = "生成打包号失败";
                        }
                        MaxID = lBTGetMaxNo.MaxID;
                    }
                    else
                    {
                        string maxnum = lBTGetMaxNo.MaxID;
                        lBTGetMaxNo.MaxID = (long.Parse(maxnum) + 1).ToString();
                        lBTGetMaxNo.DataUpdateTime = DateTime.Now;
                        if (!IDLBTGetMaxNoDao.Update(lBTGetMaxNo))
                        {
                            baseResultDataValue.success = false;
                            baseResultDataValue.ErrorInfo = "生成打包号失败";
                        }
                        MaxID = lBTGetMaxNo.MaxID;
                    }
                }
                else
                {
                    LBTGetMaxNo lBTGetMaxNo = new LBTGetMaxNo();
                    lBTGetMaxNo.BmsDate = DateTime.Now;
                    lBTGetMaxNo.BmsTypeID = long.Parse(LBTGetMaxNOBmsTypes.样本采集打包号流水号.Key);
                    lBTGetMaxNo.BmsType = LBTGetMaxNOBmsTypes.样本采集打包号流水号.Value.Code;
                    lBTGetMaxNo.MaxID = "0";
                    lBTGetMaxNo.DataAddTime = DateTime.Now;
                    IDLBTGetMaxNoDao.Save(lBTGetMaxNo);
                    MaxID = lBTGetMaxNo.MaxID;
                }

                string packno = "";
                if (!string.IsNullOrEmpty(prefixPackNo.ParaValue))
                {
                    packno += prefixPackNo.ParaValue;
                }
                else {
                    packno += "P";
                }
                packno += day + MaxID.PadLeft(5, '0');
                foreach (var entity in lisBarCodeForms)
                {
                    entity.CollectPackNo = packno;
                    entity.DataUpdateTime = DateTime.Now;
                    if (!DBDao.Update(entity)) {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "生成打包号失败";
                        return baseResultDataValue;
                    } 
                }
                baseResultDataValue.ResultDataValue = packno;
            }
            baseResultDataValue.success = true;
            return baseResultDataValue;
        }
        #endregion
        #region 样本送检
        public List<LisBarCodeFormVo> GetSampleExchangeInspectWantDataByBarCode(long nodetype, string barcode, string userid, string username, out bool topo_isupdate)
        {
            List<LisBarCodeFormVo> lisBarCodeFormVos = new List<LisBarCodeFormVo>();
            if (string.IsNullOrEmpty(barcode))
            {
                topo_isupdate = false;
                return lisBarCodeFormVos;
            }
            topo_isupdate = true;
            #region 系统参数处理
            List<BPara> bParas = IBBParaItem.SelPreBParas(nodetype, "", Pre_AllModules.样本流转_送检.Value.DefaultValue, userid, username);
            var sicktypes = bParas.Where(w => w.ParaNo == "Pre_OrderExchangeInspect_DefaultPara_0001").First();//就诊类型
            var HISGetUrl = bParas.Where(w => w.ParaNo == "Pre_OrderExchangeInspect_DefaultPara_0002").First();//核收接口地址
            var isVerify = bParas.Where(w => w.ParaNo == "Pre_OrderExchangeInspect_DefaultPara_0016").First();//是否采集校验
            #endregion  
            string strwhere = "";
            string[] barcodearr = barcode.Split(',');
            strwhere = "BarCode in ('" + string.Join("','", barcodearr) + "')";
            if (!string.IsNullOrEmpty(sicktypes.ParaValue))
            {
                strwhere += " and LisPatient.SickTypeID in (" + sicktypes.ParaValue + ")";
            }
            IList<LisBarCodeForm> lisBarCodeForms = this.SearchListByHQL(strwhere);
            if (lisBarCodeForms.Count > 0)
            {
                lisBarCodeFormVos = this.ConvertLisBarCodeFormVo(lisBarCodeForms.ToList());
                LisBarCodeFormVo lisBarCodeFormVo = lisBarCodeFormVos.First();
                #region 校验
                if (isVerify.ParaValue == "1")
                {
                    string paranos = "Pre_OrderExchangeInspect_DefaultPara_0029,Pre_OrderExchangeInspect_DefaultPara_0030,Pre_OrderExchangeInspect_DefaultPara_0031,Pre_OrderExchangeInspect_DefaultPara_0032,Pre_OrderExchangeInspect_DefaultPara_0033";
                    lisBarCodeFormVos[0] = GatherBarCodeFormVerify(bParas, lisBarCodeFormVo, paranos,2, out topo_isupdate);
                }
                #endregion
            }
            //判断是否从HIS获取数据
            if (!string.IsNullOrEmpty(HISGetUrl.ParaValue))
            {

            }
            return lisBarCodeFormVos;
        }
        /// <summary>
        /// 样本送检校验
        /// </summary>
        /// <param name="bPara"></param>
        /// <param name="lisBarCodeFormVO"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public LisBarCodeFormVo ExchangeInspectBarCodeFormVerify(BPara bPara, LisBarCodeFormVo lisBarCodeFormVO, string status)
        {
            #region
            if (bPara.ParaNo == "Pre_OrderExchangeInspect_DefaultPara_0029" && lisBarCodeFormVO.LisBarCodeForm.BarCodeStatusID < 3 && lisBarCodeFormVO.LisBarCodeForm.PrintTime == null)
            {
                #region 处理
                if (status == "2")
                {
                    lisBarCodeFormVO.failureInfo = "未查询到符合的样本单!";
                }
                else if (status == "1")
                {
                    lisBarCodeFormVO.failureInfo = "未进行条码打印确认不能送检确认!";
                }
                else if (status == "4")
                {
                    lisBarCodeFormVO.failureInfo = "当前样本未条码打印确认是否允许送检确认!";
                }
                else if (status == "3")
                {
                    lisBarCodeFormVO.failureInfo = "当前样本未条码打印确认，但样本送检确认成功！";
                }
                #endregion
            }
            else if (bPara.ParaNo == "Pre_OrderExchangeInspect_DefaultPara_0030" && lisBarCodeFormVO.LisBarCodeForm.BarCodeStatusID < 4 && lisBarCodeFormVO.LisBarCodeForm.CollectTime == null)
            {
                #region 处理
                if (status == "2")
                {
                    lisBarCodeFormVO.failureInfo = "未查询到符合的样本单!";
                }
                else if (status == "1")
                {

                    lisBarCodeFormVO.failureInfo = "未进行采集确认不能送检确认!";
                }
                else if (status == "4")
                {
                    lisBarCodeFormVO.failureInfo = "当前样本未进行采集确认是否允许送检!";
                }
                else if (status == "3")
                {
                    lisBarCodeFormVO.failureInfo = "当前样本未进行采集确认，但样本送检成功！";
                }
                #endregion
            }
            else if (bPara.ParaNo == "Pre_OrderExchangeInspect_DefaultPara_0031" && lisBarCodeFormVO.LisBarCodeForm.BarCodeStatusID >= 5 && lisBarCodeFormVO.LisBarCodeForm.SendTime != null)
            {
                #region 处理
                if (status == "2")
                {
                    lisBarCodeFormVO.failureInfo = "未查询到符合的样本单!";
                }
                else if (status == "1")
                {
                    lisBarCodeFormVO.failureInfo = "已进行送检确认不能送检确认!";
                }
                else if (status == "4")
                {
                    lisBarCodeFormVO.failureInfo = "当前样本已送检确认是否再次送检确认!";
                }
                else if (status == "3")
                {
                    lisBarCodeFormVO.failureInfo = "当前样本已进行送检确认，再次送检确认成功！";
                }
                #endregion
            }
            else if (bPara.ParaNo == "Pre_OrderExchangeInspect_DefaultPara_0032" && lisBarCodeFormVO.LisBarCodeForm.BarCodeStatusID >= 7 && lisBarCodeFormVO.LisBarCodeForm.InceptTime != null)
            {
                #region 处理
                if (status == "2")
                {
                    lisBarCodeFormVO.failureInfo = "未查询到符合的样本单!";
                }
                else if (status == "1")
                {
                    lisBarCodeFormVO.failureInfo = "已签收不能送检确认!";
                }
                else if (status == "4")
                {
                    lisBarCodeFormVO.failureInfo = "当前样本已签收，是否允许送检确认!";
                }
                else if (status == "3")
                {
                    lisBarCodeFormVO.failureInfo = "当前样本已经签收过，送检确认成功！";
                }
                #endregion
            }
            else if (bPara.ParaNo == "Pre_OrderExchangeInspect_DefaultPara_0033" && lisBarCodeFormVO.LisBarCodeForm.BarCodeStatusID >= 11 && lisBarCodeFormVO.LisBarCodeForm.ReceiveTime != null)
            {
                #region 处理
                if (status == "2")
                {
                    lisBarCodeFormVO.failureInfo = "未查询到符合的样本单!";
                }
                else if (status == "1")
                {
                    lisBarCodeFormVO.failureInfo = "已核收不能送检确认!";
                }
                else if (status == "4")
                {
                    lisBarCodeFormVO.failureInfo = "当前样本已核收，是否允许送检确认!";
                }
                else if (status == "3")
                {
                    lisBarCodeFormVO.failureInfo = "当前样本已经核收过，送检确认成功！";
                }
                #endregion
            }
            return lisBarCodeFormVO;
            #endregion
        }
        /// <summary>
        /// 样本送检确认
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="barcodes"></param>
        /// <param name="lisBarCodeFormVos"></param>
        /// <param name="userid"></param>
        /// <param name="username"></param>
        /// <param name="isConstraintUpdate"></param>
        /// <returns></returns>
        public List<LisBarCodeFormVo> EditExchangeInspectBarCodeForm(long nodetype, string barcodes, List<LisBarCodeFormVo> lisBarCodeFormVos, string userid, string username, string suserid, string susername, string labid, bool? isConstraintUpdate = false)
        {
            List<LisBarCodeFormVo> barcodeformlist = new List<LisBarCodeFormVo>();
            #region 参数处理
            List<BPara> bParas = IBBParaItem.SelPreBParas(nodetype, "", Pre_AllModules.样本流转_送检.Value.DefaultValue, userid, username);
            var HISUrl = bParas.Where(w => w.ParaNo == "Pre_OrderExchangeInspect_DefaultPara_0004").First();//HIS送检确认接口
            var isverify = bParas.Where(w => w.ParaNo == "Pre_OrderExchangeInspect_DefaultPara_0016").First();//送检确认时是否校验
            var barcodestatus = bParas.Where(w => w.ParaNo == "Pre_OrderExchangeInspect_DefaultPara_0028").First();//需要补录的状态
            var isCollectPackNo = bParas.Where(w => w.ParaNo == "Pre_OrderExchangeInspect_DefaultPara_0008").First();//送检确认时是否生成打包号
            if (lisBarCodeFormVos == null || lisBarCodeFormVos.Count <= 0 && string.IsNullOrEmpty(barcodes))
            {
                return barcodeformlist;
            }
            #endregion
            #region 数据查询与校验
            if (lisBarCodeFormVos == null || lisBarCodeFormVos.Count <= 0 && !string.IsNullOrEmpty(barcodes))
            {
                string[] barcodearr = barcodes.Split(',');
                string where = "BarCode in ('" + string.Join("','", barcodearr) + "')";
                IList<LisBarCodeForm> lisBarCodeForms = this.SearchListByHQL(where).ToList();
                if (lisBarCodeForms != null && lisBarCodeForms.Count > 0)
                {
                    lisBarCodeFormVos = ConvertLisBarCodeFormVo(lisBarCodeForms.ToList());
                }
                else 
                {
                    return barcodeformlist;
                }
               
            }
            if (!string.IsNullOrEmpty(isverify.ParaValue) && (isConstraintUpdate == null || isConstraintUpdate == false))
            {
                //送检确认校验
                string paranos = "Pre_OrderExchangeInspect_DefaultPara_0029,Pre_OrderExchangeInspect_DefaultPara_0030,Pre_OrderExchangeInspect_DefaultPara_0031,Pre_OrderExchangeInspect_DefaultPara_0032,Pre_OrderExchangeInspect_DefaultPara_0033";
                for (int i = 0; i < lisBarCodeFormVos.Count(); i++)
                {
                    lisBarCodeFormVos[i].isConstraintUpdate = true;
                    bool isedit;
                    lisBarCodeFormVos[i] = GatherBarCodeFormVerify(bParas, lisBarCodeFormVos[i], paranos, 2, out isedit);
                    lisBarCodeFormVos[i].isConstraintUpdate = isedit;
                }
            }
            #endregion
            #region 数据更新
            #region 生成打包号处理
            if (!string.IsNullOrEmpty(isCollectPackNo.ParaValue) && isCollectPackNo.ParaValue == "1" && !string.IsNullOrEmpty(barcodes)) {
                var collectPackNoAddress = bParas.Where(w => w.ParaNo == "Pre_OrderExchangeInspect_DefaultPara_0009").First();//打包号前缀
                var para0018 = bParas.Where(w => w.ParaNo == "Pre_OrderExchangeInspect_DefaultPara_0018").First();//根据字段内容不同生成不同的打包号
                if (!string.IsNullOrEmpty(para0018.ParaValue))
                {
                    #region 分组生成打包号
                    string[] barcodearr = barcodes.Split(',');
                    string where = "BarCode in ('" + string.Join("','", barcodearr) + "')";
                    if (!string.IsNullOrEmpty(labid))
                    {
                        where += " and LabID=" + labid;
                    }
                    string defaultwhere = "";
                    string fields = "";
                    foreach (var field in para0018.ParaValue.Split(','))
                    {
                        if (fields == "")
                        {
                            fields = field.Split('.')[1];
                        }
                        else
                        {
                            fields += "," + field.Split('.')[1];
                        }
                    }
                    var lisbarcodeForms = (this.DBDao as IDLisBarCodeFormDao).GetBarCodeFormList(fields, where);
                    if (lisbarcodeForms != null)
                    {
                        var fieldsarr = fields.Split(',');
                        foreach (var item in lisbarcodeForms)
                        {
                            //根据查询出来的值拼接where条件
                            Array dataarr = (Array)item;
                            string followwhere = "";
                            for (int i = 0; i < dataarr.Length; i++)
                            {
                                var field = fieldsarr[i];
                                if (dataarr.GetValue(i) != null && !string.IsNullOrEmpty(dataarr.GetValue(i).ToString()))
                                {
                                    followwhere += " and " + field + "='" + dataarr.GetValue(i).ToString() + "'";
                                }
                                else
                                {
                                    followwhere += " and isnull(" + field + ", '') = ''";
                                }
                            }
                            string strwhere = defaultwhere + followwhere;                           
                            var barcodearrs = (this.DBDao as IDLisBarCodeFormDao).GetBarCodeFormList("BarCode", strwhere);
                            string cpno = Edit_SampleExchangeInspectGenerateCollectPackNo(collectPackNoAddress);
                            foreach (var barcode in barcodearrs)
                            {
                                lisBarCodeFormVos.Where(a => a.LisBarCodeForm.BarCode == barcode.ToString()).ToList().ForEach(f=>f.LisBarCodeForm.CollectPackNo = cpno);
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    string cpno = Edit_SampleExchangeInspectGenerateCollectPackNo(collectPackNoAddress);
                    lisBarCodeFormVos.ForEach(f => f.LisBarCodeForm.CollectPackNo = cpno);
                }
            }
            #endregion
            List<long> formids = new List<long>();
            foreach (var form in lisBarCodeFormVos)
            {
                if (!formids.Contains(form.LisBarCodeForm.Id))
                    formids.Add(form.LisBarCodeForm.Id);
            }
            IList<LisBarCodeItem> lisBarCodeItems = IDLisBarCodeItemDao.GetListByHQL("LisBarCodeForm.Id in (" + string.Join(",", formids) + ")");
            foreach (var form in lisBarCodeFormVos)
            {
                if (isConstraintUpdate == true || form.isConstraintUpdate == true)
                {
                    form.LisBarCodeForm = StatusDateTimeInfoReplenish(form.LisBarCodeForm, barcodestatus.ParaValue);//状态补录
                    long status = long.Parse(BarCodeStatus.样本送检.Value.Code);
                    form.LisBarCodeForm.BarCodeStatusID = status;
                    form.LisBarCodeForm.BarCodeCurrentStatus = BarCodeStatus.样本送检.Value.Name;
                    form.LisBarCodeForm.SendTime = DateTime.Now;
                    form.LisBarCodeForm.DataUpdateTime = DateTime.Now;
                    var barcodeitems = lisBarCodeItems.Where(a => a.LisBarCodeForm.Id == form.LisBarCodeForm.Id).ToList();
                    this.Entity = form.LisBarCodeForm;
                    if (this.Edit())
                    {
                        if (!string.IsNullOrEmpty(susername) && !string.IsNullOrEmpty(suserid))
                        {
                            IBLisOperate.AddLisOperate(form.LisBarCodeForm, BarCodeStatus.样本送检.Value, "样本送检", SysCookieValue,susername,long.Parse(suserid));
                        }
                        else 
                        {
                            IBLisOperate.AddLisOperate(form.LisBarCodeForm, BarCodeStatus.样本送检.Value, "样本送检", SysCookieValue);
                        }
                        foreach (var item in barcodeitems)
                        {
                            item.DataUpdateTime = DateTime.Now;
                            item.ItemStatusID = status;
                            if (IDLisBarCodeItemDao.Update(item))
                            {
                                if (!string.IsNullOrEmpty(susername) && !string.IsNullOrEmpty(suserid))
                                {
                                    IBLisOperate.AddLisOperate(item, BarCodeStatus.样本送检.Value, "样本送检", SysCookieValue, susername, long.Parse(suserid));
                                }
                                else
                                {
                                    IBLisOperate.AddLisOperate(item, BarCodeStatus.样本送检.Value, "样本送检", SysCookieValue);
                                }
                            }
                        }
                    }
                }
                barcodeformlist.Add(form);
            }
            if (!string.IsNullOrEmpty(HISUrl.ParaValue))
            {
                //调用HIS样本送检确认接口 
            }
            #endregion
            return barcodeformlist;
        }
        /// <summary>
        /// 样本送检生成打包号
        /// </summary>
        /// <param name="bPara"></param>
        /// <returns></returns>
        public string Edit_SampleExchangeInspectGenerateCollectPackNo(BPara bPara) {

            IList<LBTGetMaxNo> lBTGetMaxNos = IDLBTGetMaxNoDao.GetListByHQL("BmsTypeID=" + LBTGetMaxNOBmsTypes.样本送检打包号流水号.Key);
            string day = DateTime.Now.ToString("yyyyMMdd");
            string MaxID = "";
            if (lBTGetMaxNos.Count > 0)
            {
                LBTGetMaxNo lBTGetMaxNo = lBTGetMaxNos[0];
                if (lBTGetMaxNo.BmsDate.ToString("yyyyMMdd") != day)
                {
                    lBTGetMaxNo.MaxID = "0";
                    lBTGetMaxNo.BmsDate = DateTime.Now;
                    lBTGetMaxNo.DataUpdateTime = DateTime.Now;
                    IDLBTGetMaxNoDao.Update(lBTGetMaxNo);
                    MaxID = lBTGetMaxNo.MaxID;
                }
                else
                {
                    string maxnum = lBTGetMaxNo.MaxID;
                    lBTGetMaxNo.MaxID = (long.Parse(maxnum) + 1).ToString();
                    lBTGetMaxNo.DataUpdateTime = DateTime.Now;
                    IDLBTGetMaxNoDao.Update(lBTGetMaxNo);
                    MaxID = lBTGetMaxNo.MaxID;
                }
            }
            else
            {
                LBTGetMaxNo lBTGetMaxNo = new LBTGetMaxNo();
                lBTGetMaxNo.BmsDate = DateTime.Now;
                lBTGetMaxNo.BmsTypeID = long.Parse(LBTGetMaxNOBmsTypes.样本送检打包号流水号.Key);
                lBTGetMaxNo.BmsType = LBTGetMaxNOBmsTypes.样本送检打包号流水号.Value.Code;
                lBTGetMaxNo.MaxID = "0";
                lBTGetMaxNo.DataAddTime = DateTime.Now;
                IDLBTGetMaxNoDao.Save(lBTGetMaxNo);
                MaxID = lBTGetMaxNo.MaxID;
            }
            string packno = "";
            if (!string.IsNullOrEmpty(bPara.ParaValue))
            {
                packno += bPara.ParaValue;
            }
            else
            {
                packno += "P";
            }
            packno += day + MaxID.PadLeft(5, '0');
            return packno;
        }
        /// <summary>
        /// 撤销送检
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="barcodes"></param>
        /// <param name="userid"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public BaseResultDataValue EditBarCodeFormRevocationExchangeInspect(long nodetype, string barcodes, string userid, string username)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            List<BPara> bParas = IBBParaItem.SelPreBParas(nodetype, "", Pre_AllModules.样本流转_送检.Value.DefaultValue, userid, username);
            string[] barcodearr = barcodes.Split(',');
            string where = "BarCode in ('" + string.Join("','", barcodearr) + "')";
            IList<LisBarCodeForm> lisBarCodeForms = this.SearchListByHQL(where).ToList();
            if (lisBarCodeForms.Count <= 0 || lisBarCodeForms == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ResultDataValue = "未查询到此条码号信息！";
                return baseResultDataValue;
            }
            List<long> formids = new List<long>();
            foreach (var form in lisBarCodeForms)
            {
                if (!formids.Contains(form.Id))
                    formids.Add(form.Id);
            }
            IList<LisBarCodeItem> lisBarCodeItems = IDLisBarCodeItemDao.GetListByHQL("LisBarCodeForm.Id in (" + string.Join(",", formids) + ")");
            List<string> resultInfo = new List<string>();
            foreach (var form in lisBarCodeForms)
            {
                long status = long.Parse(BarCodeStatus.样本采集.Value.Code);
                if (form.BarCodeStatusID > status)
                {
                    form.BarCodeStatusID = status;
                    form.BarCodeCurrentStatus = BarCodeStatus.样本采集.Value.Name;
                    form.SendTime = null;
                    form.DataUpdateTime = DateTime.Now;
                    var barcodeitems = lisBarCodeItems.Where(a => a.LisBarCodeForm.Id == form.Id).ToList();
                    this.Entity = form;
                    if (this.Edit())
                    {
                        IBLisOperate.AddLisOperate(form, OrderFromOperateType.撤销送检.Value, "撤销送检", SysCookieValue);
                        foreach (var item in barcodeitems)
                        {
                            item.DataUpdateTime = DateTime.Now;
                            item.ItemStatusID = status;
                  
                            if (IDLisBarCodeItemDao.Update(item))
                            {
                                IBLisOperate.AddLisOperate(item, OrderFromOperateType.撤销送检.Value, "撤销送检", SysCookieValue);
                            }
                            else
                            {
                                baseResultDataValue.success = false;
                                baseResultDataValue.ErrorInfo = "撤销失败！";
                                return baseResultDataValue;
                            }
                        }
                        resultInfo.Add("{\"" + form.BarCode + "\":\"true\"}");
                    }
                    else
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "撤销失败！";
                        return baseResultDataValue;
                    }
                }
                else
                {
                    resultInfo.Add("{\"" + form.BarCode + "\":\"false\"}");
                }
            }
            baseResultDataValue.success = true;
            baseResultDataValue.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(resultInfo);
            return baseResultDataValue;
        }
        /// <summary>
        /// 获取撤销送检数据并校验
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="barcodes"></param>
        /// <param name="userid"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public BaseResultDataValue GetSampleExchangeInspectDataAndVerifyByBarCode(long nodetype, string barcodes, string userid, string username)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (string.IsNullOrEmpty(barcodes))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "条码号不可为空！";
                return baseResultDataValue;
            }
            List<BPara> bParas = IBBParaItem.SelPreBParas(nodetype, "", Pre_AllModules.样本流转_送检.Value.DefaultValue, userid, username);
            var isVerify = bParas.Where(w => w.ParaNo == "Pre_OrderExchangeInspect_DefaultPara_0034").First();//是否开启撤销采集校验
            IList<LisBarCodeForm> lisBarCodeForms = this.SearchListByHQL("BarCode = '" + barcodes + "'");
            if (lisBarCodeForms != null && lisBarCodeForms.Count > 0)
            {
                List<LisBarCodeFormVo> lisBarCodeFormVos = this.ConvertLisBarCodeFormVo(lisBarCodeForms.ToList());
                if (isVerify.ParaValue == "1")
                {
                    string aa = "Pre_OrderExchangeInspect_DefaultPara_0035,Pre_OrderExchangeInspect_DefaultPara_0036,Pre_OrderExchangeInspect_DefaultPara_0037,Pre_OrderExchangeInspect_DefaultPara_0038";
                    List<BPara> verifybparas = new List<BPara>();
                    foreach (var bpara in bParas)
                    {
                        if (aa.IndexOf(bpara.ParaNo) > -1)
                        {
                            verifybparas.Add(bpara);
                        }
                    }
                    for (int i = 0; i < lisBarCodeFormVos.Count(); i++)
                    {
                        lisBarCodeFormVos[i] = RevocationExchangeInspectBarCodeFormVerify(verifybparas, lisBarCodeFormVos[i]);
                    }
                }
                baseResultDataValue.success = true;
                baseResultDataValue.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(lisBarCodeFormVos);
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "未查询到样本数据！";
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 撤销送检校验
        /// </summary>
        /// <param name="bParas"></param>
        /// <param name="lisBarCodeFormVO"></param>
        /// <returns></returns>
        public LisBarCodeFormVo RevocationExchangeInspectBarCodeFormVerify(List<BPara> bParas, LisBarCodeFormVo lisBarCodeFormVO)
        {
            #region
            List<VerifyReminInfo> verifyReminInfos = new List<VerifyReminInfo>();
            foreach (var bPara in bParas)
            {
                VerifyReminInfo verifyReminInfo = new VerifyReminInfo();
                verifyReminInfo.alterMode = "2";
                bool isreturn = false;
                if (bPara.ParaNo == "Pre_OrderExchangeInspect_DefaultPara_0035" && bPara.ParaValue == "1" && lisBarCodeFormVO.LisBarCodeForm.BarCodeStatusID < 5 && lisBarCodeFormVO.LisBarCodeForm.SendTime == null)
                {
                    isreturn = true;
                    verifyReminInfo.failureInfo = "未送检确认不能撤销送检确认！";
                    verifyReminInfos.Add(verifyReminInfo);
                }
                else if (bPara.ParaNo == "Pre_OrderExchangeInspect_DefaultPara_0036" && bPara.ParaValue == "1" && lisBarCodeFormVO.LisBarCodeForm.BarCodeStatusID <= 6 && lisBarCodeFormVO.LisBarCodeForm.ArriveTime != null)
                {
                    isreturn = true;
                    verifyReminInfo.failureInfo = "已进行送达确认不能撤销送检确认！";
                    verifyReminInfos.Add(verifyReminInfo);
                }
                else if (bPara.ParaNo == "Pre_OrderExchangeInspect_DefaultPara_0037" && bPara.ParaValue == "1" && lisBarCodeFormVO.LisBarCodeForm.BarCodeStatusID <= 7 && lisBarCodeFormVO.LisBarCodeForm.InceptTime != null)
                {
                    isreturn = true;
                    verifyReminInfo.failureInfo = "已签收不能撤销送检确认！";
                    verifyReminInfos.Add(verifyReminInfo);
                }
                else if (bPara.ParaNo == "Pre_OrderExchangeInspect_DefaultPara_0038" && bPara.ParaValue == "1" && lisBarCodeFormVO.LisBarCodeForm.BarCodeStatusID <= 11 && lisBarCodeFormVO.LisBarCodeForm.ReceiveTime != null)
                {
                    isreturn = true;
                    verifyReminInfo.failureInfo = "已核收不能撤销送检确认！";
                    verifyReminInfos.Add(verifyReminInfo);
                }
                if (isreturn)
                {
                    lisBarCodeFormVO.failureInfo = Newtonsoft.Json.JsonConvert.SerializeObject(verifyReminInfos);
                    return lisBarCodeFormVO;
                }
            }
            return lisBarCodeFormVO;
            #endregion
        }

        public List<LisBarCodeFormVo> GetSampleExchangeInspectWantDataByWhere(long nodetype, string where, string userid, string username, string relationForm)
        {
            List<LisBarCodeFormVo> lisBarCodeFormVos = new List<LisBarCodeFormVo>();
            if (string.IsNullOrEmpty(where))
            {
                return lisBarCodeFormVos;
            }
            #region 系统参数处理
            List<BPara> bParas = IBBParaItem.SelPreBParas(nodetype, "", Pre_AllModules.样本流转_送检.Value.DefaultValue, userid, username);
            var sicktypes = bParas.Where(w => w.ParaNo == "Pre_OrderExchangeInspect_DefaultPara_0001").First();//就诊类型
            #endregion  
            string strwhere = where;
            if (!string.IsNullOrEmpty(sicktypes.ParaValue))
            {               
                strwhere += " and lisbarcodeform.LisPatient.SickTypeID in (" + sicktypes.ParaValue + ")";
            }
            //IList<LisBarCodeForm> lisBarCodeForms = this.SearchListByHQL(strwhere);
            string hql = "select lisbarcodeform from LisBarCodeForm lisbarcodeform ";
            if (!string.IsNullOrEmpty(relationForm))
            {
                hql += "," + relationForm + " ";
            }
            var barCodeFormList = (this.DBDao as IDLisBarCodeFormDao).QueryLisBarCodeFormByHqlDao(where, "", -1, -1, hql);
            if (barCodeFormList != null && barCodeFormList.Count > 0)
            {
                lisBarCodeFormVos = ConvertLisBarCodeFormVo(barCodeFormList) ;
            }
            return lisBarCodeFormVos;
        }

        #endregion
        #region 样本送达

        public BaseResultDataValue GetSampledeliveryBarCodeFormListAndEditBarCodeForm(long nodetype, string barcodes, string userid, string username, string loginuserid, string loginusername, string fields, bool isPlanish, bool isUpdate = false)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            bool poto_isupdate ;
            var  lisBarCodeFormVOs = GetSampledeliveryBarCodeFormList(nodetype, barcodes, loginuserid, loginusername, out poto_isupdate);
            if (isUpdate && poto_isupdate)
            {
                BaseResultDataValue baseResultDataValue = EditBarCodeFormArrive(nodetype, barcodes, userid, username,loginuserid, loginusername);
                if (baseResultDataValue.success)
                {
                    lisBarCodeFormVOs[0].LisBarCodeForm.BarCodeStatusID = long.Parse(BarCodeStatus.样本送达.Value.Code);
                    lisBarCodeFormVOs[0].LisBarCodeForm.BarCodeCurrentStatus = BarCodeStatus.样本送达.Value.Name;
                }
                else 
                {
                    return baseResultDataValue;
                }
            }
            #region 数据压平
            EntityList<LisBarCodeFormVo> entityList = new EntityList<LisBarCodeFormVo>();
            if (lisBarCodeFormVOs != null && lisBarCodeFormVOs.Count > 0)
            {
                entityList.count = lisBarCodeFormVOs.Count;
                entityList.list = lisBarCodeFormVOs;
            }
            ParseObjectProperty pop = new ParseObjectProperty(fields);
            try
            {
                brdv.success = true;
                if (isPlanish)
                {
                    brdv.ResultDataValue = pop.GetObjectListPlanish<LisBarCodeFormVo>(entityList);
                }
                else
                {
                    brdv.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                }
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_PreSampledeliveryGetBarCodeForm.GetSampledeliveryBarCodeFormListAndEditBarCodeForm异常：" + ex.ToString());
            }
            #endregion
            return brdv;
        }
        public List<LisBarCodeFormVo> GetSampledeliveryBarCodeFormList(long nodetype, string barcodes, string loginuserid, string loginusername, out bool isupdate )
        {

            List<LisBarCodeFormVo> lisBarCodeFormVOs = new List<LisBarCodeFormVo>();
           
            #region 系统参数处理
            List<BPara> bParas = IBBParaItem.SelPreBParas(nodetype, "", Pre_AllModules.样本流转_送达.Value.DefaultValue, loginuserid, loginusername);
            var sicktypes = bParas.Where(w => w.ParaNo == "Pre_OrderExchangeDelivery_DefaultPara_0001").First();//就诊类型
            var HISGetUrl = bParas.Where(w => w.ParaNo == "Pre_OrderExchangeDelivery_DefaultPara_0002").First();//核收接口地址
            var isVerify = bParas.Where(w => w.ParaNo == "Pre_OrderExchangeDelivery_DefaultPara_0004").First();//是否校验
            #endregion
            #region 业务处理
            string[] barcodearr = barcodes.Split(',');
            string strwhere = "BarCode in ('"+string.Join("','", barcodearr)+ "') and BarCodeStatusID < 6 and ArriveTime is null";
            if (!string.IsNullOrEmpty(sicktypes.ParaValue))
            {
                string[] sicktypearr = sicktypes.ParaValue.Split(',');
                List<long> ids = new List<long>();
                foreach (var sicktype in sicktypearr)
                {
                    long id = long.Parse(sicktype);
                    if (!ids.Contains(id))
                        ids.Add(id);
                }
                strwhere += " and LisPatient.SickTypeID in ('" + string.Join(",", ids) + "')";
            }
            IList<LisBarCodeForm> lisBarCodeForms= this.SearchListByHQL(strwhere);
            bool topo_isupdate = true;
            if (lisBarCodeForms != null && lisBarCodeForms.Count > 0)
            {
                lisBarCodeFormVOs = this.ConvertLisBarCodeFormVo(lisBarCodeForms.ToList());
                LisBarCodeFormVo lisBarCodeFormVo = lisBarCodeFormVOs.First();
                if (isVerify.ParaValue == "1")
                {
                    string paranos = "Pre_OrderExchangeDelivery_DefaultPara_0011,Pre_OrderExchangeDelivery_DefaultPara_0012,Pre_OrderExchangeDelivery_DefaultPara_0013,Pre_OrderExchangeDelivery_DefaultPara_0014,Pre_OrderExchangeDelivery_DefaultPara_0015";
                    List<BPara> verifybparas = new List<BPara>(); 
                    foreach (var bpara in bParas)
                    {
                        if (paranos.IndexOf(bpara.ParaNo) > -1) {
                            verifybparas.Add(bpara);
                        }
                    }
                    #region 校验
                    var paragroups =  verifybparas.GroupBy(a => a.ParaValue);
                    List<VerifyReminInfo> verifyReminInfos = new List<VerifyReminInfo>();
                    #region 不允许不提醒
                    var verifyinfo1 = paragroups.Where(a => a.Key == "2");
                    if (verifyinfo1 != null && verifyinfo1.Count() > 0) {
                        foreach (var group in verifyinfo1.First())
                        {
                            
                            lisBarCodeFormVo.failureInfo = "";
                            LisBarCodeFormVo lisBarCodeFormVO = DeliveryBarCodeFormVerify(group, lisBarCodeFormVo, "2");
                            if (!string.IsNullOrEmpty(lisBarCodeFormVO.failureInfo))
                            {
                                topo_isupdate = false;
                                VerifyReminInfo verifyReminInfo = new VerifyReminInfo();
                                verifyReminInfo.alterMode = "2";
                                verifyReminInfo.failureInfo = lisBarCodeFormVO.failureInfo;
                                verifyReminInfos.Add(verifyReminInfo);
                                lisBarCodeFormVO.failureInfo = Newtonsoft.Json.JsonConvert.SerializeObject(verifyReminInfos);
                                List<LisBarCodeFormVo> lisBarCodeFormVos = new List<LisBarCodeFormVo>();
                                lisBarCodeFormVos.Add(lisBarCodeFormVO);
                                isupdate = topo_isupdate;
                                return lisBarCodeFormVos;
                            }
                        }
                    }
                    #endregion
                    #region 不允许但提醒
                    var verifyinfo2 = paragroups.Where(a => a.Key == "1");
                    if (verifyinfo2 != null && verifyinfo2.Count() > 0)
                    {
                        foreach (var group in verifyinfo2.First())
                        {
                            lisBarCodeFormVo.failureInfo = "";
                            LisBarCodeFormVo lisBarCodeFormVO = DeliveryBarCodeFormVerify(group, lisBarCodeFormVo, "1");
                            if (!string.IsNullOrEmpty(lisBarCodeFormVO.failureInfo))
                            {
                                topo_isupdate = false;
                                VerifyReminInfo verifyReminInfo = new VerifyReminInfo();
                                verifyReminInfo.alterMode = "1";
                                verifyReminInfo.failureInfo = lisBarCodeFormVO.failureInfo;
                                verifyReminInfos.Add(verifyReminInfo);
                                lisBarCodeFormVO.failureInfo = Newtonsoft.Json.JsonConvert.SerializeObject(verifyReminInfos);
                                List<LisBarCodeFormVo> lisBarCodeFormVos = new List<LisBarCodeFormVo>();
                                lisBarCodeFormVos.Add(lisBarCodeFormVO);
                                isupdate = topo_isupdate;
                                return lisBarCodeFormVos;
                            }
                        }
                    }
                    #endregion
                    #region 用户交互
                    var verifyinfo3 = paragroups.Where(a => a.Key == "4");
                    if (verifyinfo3 != null && verifyinfo3.Count() > 0)
                    {
                        foreach (var group in verifyinfo3.First())
                        {
                            lisBarCodeFormVo.failureInfo = "";
                            LisBarCodeFormVo lisBarCodeFormVO = DeliveryBarCodeFormVerify(group, lisBarCodeFormVo, "4");
                            if (!string.IsNullOrEmpty(lisBarCodeFormVO.failureInfo))
                            {
                                topo_isupdate = false;
                                VerifyReminInfo verifyReminInfo = new VerifyReminInfo();
                                verifyReminInfo.alterMode = "4";
                                verifyReminInfo.failureInfo = lisBarCodeFormVO.failureInfo;
                                verifyReminInfos.Add(verifyReminInfo);
                            }
                        }                       
                    }
                    #endregion
                    #region 允许并提醒
                    var verifyinfo4 = paragroups.Where(a => a.Key == "3");
                    if (verifyinfo4 != null && verifyinfo4.Count() > 0)
                    {
                        foreach (var group in verifyinfo4.First())
                        {
                            lisBarCodeFormVo.failureInfo = "";
                            LisBarCodeFormVo lisBarCodeFormVO = DeliveryBarCodeFormVerify(group, lisBarCodeFormVo, "3");
                            if (!string.IsNullOrEmpty(lisBarCodeFormVO.failureInfo))
                            {
                                VerifyReminInfo verifyReminInfo = new VerifyReminInfo();
                                verifyReminInfo.alterMode = "3";
                                verifyReminInfo.failureInfo = lisBarCodeFormVO.failureInfo;
                                verifyReminInfos.Add(verifyReminInfo);
                            }
                        }
                    }
                    #endregion
                    lisBarCodeFormVOs[0].failureInfo = Newtonsoft.Json.JsonConvert.SerializeObject(verifyReminInfos);
                    #endregion
                }
            }
            else {
                //判断是否从HIS获取数据
                if (!string.IsNullOrEmpty(HISGetUrl.ParaValue)) {
                }
            }
            #endregion
            isupdate = topo_isupdate;
            return lisBarCodeFormVOs;
        }
        public LisBarCodeFormVo DeliveryBarCodeFormVerify(BPara bPara,LisBarCodeFormVo lisBarCodeFormVO, string status) {
            #region
            if (bPara.ParaNo == "Pre_OrderExchangeDelivery_DefaultPara_0011" && lisBarCodeFormVO.LisBarCodeForm.BarCodeStatusID < 2 && lisBarCodeFormVO.LisBarCodeForm.AffirmTime != null && lisBarCodeFormVO.LisBarCodeForm.IsAffirm != 0)
            {
                #region 处理
                if (status == "2"  ) {
                    lisBarCodeFormVO.failureInfo = "未查询到符合的样本单!";
                }else if (status == "1") {
                    lisBarCodeFormVO.failureInfo = "未条码确认的不能送达确认!";
                } 
                else if (status == "4") {
                    lisBarCodeFormVO.failureInfo = "当前样本条码未确认是否允许送达";
                } else if(status == "3")
                {
                    lisBarCodeFormVO.failureInfo = "当前样本条码未确认，但已送达成功！";
                }
                #endregion
            }
            else if (bPara.ParaNo == "Pre_OrderExchangeDelivery_DefaultPara_0012" && lisBarCodeFormVO.LisBarCodeForm.BarCodeStatusID < 4 && lisBarCodeFormVO.LisBarCodeForm.CollectTime != null) 
            {
                #region 处理
                if (status == "2")
                {
                    lisBarCodeFormVO.failureInfo = "未查询到符合的样本单!";
                }else if (status == "1") {

                    lisBarCodeFormVO.failureInfo = "未采集确认的不能送达确认!";
                }
                else if (status == "4")
                {
                    lisBarCodeFormVO.failureInfo = "当前样本未采集确认是否允许送达";
                }
                else if (status == "3")
                {
                    lisBarCodeFormVO.failureInfo = "当前样本未采集确认，但已送达成功！";
                }
                #endregion
            }
            else if (bPara.ParaNo == "Pre_OrderExchangeDelivery_DefaultPara_0013" && lisBarCodeFormVO.LisBarCodeForm.BarCodeStatusID < 5 && lisBarCodeFormVO.LisBarCodeForm.SendTime != null)
            {
                #region 处理
                if (status == "2")
                {
                    lisBarCodeFormVO.failureInfo = "未查询到符合的样本单!";
                }
                else if (status == "1")
                {
                    lisBarCodeFormVO.failureInfo = "未送检确认的不能送达确认";
                }
                else if (status == "4")
                {
                    lisBarCodeFormVO.failureInfo = "当前样本未送检确认是否允许送达";
                }
                else if (status == "3")
                {
                    lisBarCodeFormVO.failureInfo = "当前样本未送检确认，但已送达成功！";
                }
                #endregion
            }
            else if (bPara.ParaNo == "Pre_OrderExchangeDelivery_DefaultPara_0014" && lisBarCodeFormVO.LisBarCodeForm.BarCodeStatusID >= 7 && lisBarCodeFormVO.LisBarCodeForm.InceptTime != null)
            {
                #region 处理
                if (status == "2" )
                {
                    lisBarCodeFormVO.failureInfo = "未查询到符合的样本单!";
                }
                else if (status == "1")
                {
                    lisBarCodeFormVO.failureInfo = "已签收的不能送达确认!";
                }
                else if (status == "4")
                {
                    lisBarCodeFormVO.failureInfo = "当前样本已签收是否允许送达";
                }
                else if (status == "3")
                {
                    lisBarCodeFormVO.failureInfo = "当前样本已签收，送达状态更新成功！";
                }
                #endregion
            }
            else if (bPara.ParaNo == "Pre_OrderExchangeDelivery_DefaultPara_0015" && lisBarCodeFormVO.LisBarCodeForm.BarCodeStatusID >= 11 && lisBarCodeFormVO.LisBarCodeForm.ReceiveTime != null)
            {
                #region 处理
                if (status == "2")
                {
                    lisBarCodeFormVO.failureInfo = "未查询到符合的样本单!";
                }
                else if (status == "1")
                {
                    lisBarCodeFormVO.failureInfo = "已核收的不能送达确认!";
                }
                else if (status == "4")
                {
                    lisBarCodeFormVO.failureInfo = "当前样本已核收是否允许送达";
                }
                else if (status == "3")
                {
                    lisBarCodeFormVO.failureInfo = "当前样本已核收，送达状态更新成功！";
                }
                #endregion
            }
            return lisBarCodeFormVO;
            #endregion
        }
        public BaseResultDataValue EditBarCodeFormArrive(long nodetype, string barcodes, string userid, string username, string loginuserid, string loginusername)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (string.IsNullOrEmpty(barcodes))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "条码号不可为空！";
                return baseResultDataValue;
            }
            List<BPara> bParas = IBBParaItem.SelPreBParas(nodetype, "", Pre_AllModules.样本流转_送达.Value.DefaultValue, loginuserid, loginusername);
            var barcodestatus = bParas.Where(w => w.ParaNo == "Pre_OrderExchangeDelivery_DefaultPara_0010").First();//状态补录
            var HISUrl = bParas.Where(w => w.ParaNo == "Pre_OrderExchangeDelivery_DefaultPara_0003").First();//HIS送达标记
            string[] barcodearr = barcodes.Split(',');
            IList<LisBarCodeForm> lisBarCodeForms = this.SearchListByHQL("BarCode in ('" + string.Join("','", barcodearr) + "')");
            List<long> formids = new List<long>();
            foreach (var form in lisBarCodeForms)
            {
                if (!formids.Contains(form.Id))
                    formids.Add(form.Id);
            }
            IList<LisBarCodeItem> lisBarCodeItems = IDLisBarCodeItemDao.GetListByHQL("LisBarCodeForm.Id in (" + string.Join(",", formids) + ")");
            for (int i = 0; i < lisBarCodeForms.Count; i++)
            {
                lisBarCodeForms[i] = StatusDateTimeInfoReplenish(lisBarCodeForms[i], barcodestatus.ParaValue);//状态补录
                var entity = lisBarCodeForms[i];
                long status = long.Parse(BarCodeStatus.样本送达.Value.Code);
                entity.BarCodeStatusID = status;
                entity.BarCodeCurrentStatus = BarCodeStatus.样本送达.Value.Name;
                entity.ArriveTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                var barcodeitems = lisBarCodeItems.Where(a => a.LisBarCodeForm.Id == entity.Id).ToList();
                this.Entity = entity;
                if (this.Edit())
                {
                    if (!string.IsNullOrEmpty(userid) && !string.IsNullOrEmpty(username))
                    {
                        IBLisOperate.AddLisOperate(entity, BarCodeStatus.样本送达.Value, "样本送达", SysCookieValue, username, long.Parse(userid));
                    }
                    else
                    {
                        IBLisOperate.AddLisOperate(entity, BarCodeStatus.样本送达.Value, "样本送达", SysCookieValue);
                    }

                    foreach (var item in barcodeitems)
                    {
                        item.DataUpdateTime = DateTime.Now;
                        item.ItemStatusID = status;
                        if (IDLisBarCodeItemDao.Update(item))
                        {
                            if (!string.IsNullOrEmpty(userid) && !string.IsNullOrEmpty(username))
                            {
                                IBLisOperate.AddLisOperate(item, BarCodeStatus.样本送达.Value, "样本送达", SysCookieValue, username, long.Parse(userid));
                            }
                            else
                            {
                                IBLisOperate.AddLisOperate(item, BarCodeStatus.样本送达.Value, "样本送达", SysCookieValue);
                            }
                        }
                        else
                        {
                            baseResultDataValue.success = false;
                            baseResultDataValue.ErrorInfo = "送达状态更新失败！";
                            return baseResultDataValue;
                        }
                    }
                }
                else 
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "送达状态更新失败！";
                    return baseResultDataValue;
                }
            }
            if (!string.IsNullOrEmpty(HISUrl.ParaValue))
            {
                //调用HIS样本送达确认接口 
            }

            baseResultDataValue.success = true;
            return baseResultDataValue;
        }

        #endregion
        #region 样本签收
        /// <summary>
        /// 通过查询条件获取查询列表
        /// </summary>
        /// <param name="nodetypeID"></param>
        /// <param name="where"></param>
        /// <param name="userID"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public List<LisBarCodeFormVo> GetSignForBarCodeFormListByWhere(long nodetypeID, string where, string userID, string userName, string sortFields, string relationForm)
        {
            ZhiFang.Common.Log.Log.Debug("GetSignForBarCodeFormListByWhere.入参sortFields：" + sortFields);
            #region 获取参数配置
            List<BPara> bParas = IBBParaItem.SelPreBParas(nodetypeID, "", Pre_AllModules.样本签收.Value.DefaultValue, userID, userName);
            var HaveSignForRangeDay = bParas.Where(w => w.ParaNo == "Pre_OrderSignFor_DefaultPara_0052").First();//指定默认查询已签收样本日期范围，大于0生效
            var BillingDepartmentNos = bParas.Where(w => w.ParaNo == "Pre_OrderSignFor_DefaultPara_0053").First();//开单科室编号，多个开单科室之间需要英文逗号分隔
            var DepartmentNos = bParas.Where(w => w.ParaNo == "Pre_OrderSignFor_DefaultPara_0054").First();//执行科室编号，多个科室编号之间需要英文逗号分隔
            var InspectionTeamNos = bParas.Where(w => w.ParaNo == "Pre_OrderSignFor_DefaultPara_0055").First();//检验小组编号，多个检验小组编号之间需要英文逗号分隔
            var SampleTypeNos = bParas.Where(w => w.ParaNo == "Pre_OrderSignFor_DefaultPara_0056").First();//样本类型编号，多个样本类型编号之间需要英文逗号分隔
            var SamplingGroupNos = bParas.Where(w => w.ParaNo == "Pre_OrderSignFor_DefaultPara_0057").First();//采样组编号，多个采样组之间需要英文逗号分隔
            var IsUseCollectPackNo = bParas.Where(w => w.ParaNo == "Pre_OrderSignFor_DefaultPara_0017").First();//获取是否通过打包号签收参数,1(0)|P 0:不使用，1:使用 P:打包号标识符
            #endregion
            string finalWhere = "";
            #region 查询条件
            //if (!string.IsNullOrWhiteSpace(HaveSignForRangeDay.ParaValue))
            //{
            //    //where += "";
            //}
            //if (!string.IsNullOrWhiteSpace(BillingDepartmentNos.ParaValue))
            //{
            //    where += " and LisPatient.DeptID in (" + BillingDepartmentNos.ParaValue + ")";
            //}
            //if (!string.IsNullOrWhiteSpace(DepartmentNos.ParaValue))
            //{
            //    where += " and LisPatient.ExecDeptID in (" + BillingDepartmentNos.ParaValue + ")";
            //}
            //if (!string.IsNullOrWhiteSpace(InspectionTeamNos.ParaValue))
            //{
            //    //where += " ";
            //}
            //if (!string.IsNullOrWhiteSpace(SampleTypeNos.ParaValue))
            //{
            //    where += " and SampleTypeID in (" + BillingDepartmentNos.ParaValue + ")";
            //}
            //if (!string.IsNullOrWhiteSpace(SamplingGroupNos.ParaValue))
            //{
            //    where += " and SamplingGroupID in (" + BillingDepartmentNos.ParaValue + ")";
            //}
            finalWhere = where;
            #endregion
            string hql = "select lisbarcodeform from LisBarCodeForm lisbarcodeform ";
            if (!string.IsNullOrEmpty(relationForm))
            {
                hql += "," + relationForm + " ";
            }
            var barCodeFormList = (this.DBDao as IDLisBarCodeFormDao).QueryLisBarCodeFormByHqlDao(where, sortFields, -1, -1, hql);
            List<LisBarCodeForm> lisbarcodeforms = new List<LisBarCodeForm>();
            List<long> BarCodeFormIDs = new List<long>();
            List<LisBarCodeFormVo> barCodeFormVos = new List<LisBarCodeFormVo>();
            if (barCodeFormList != null && barCodeFormList.Count > 0)
            {

                var formgroups = barCodeFormList.GroupBy(a => a.Id);
                foreach (var groups in formgroups)
                {
                    var barcodeform = groups.First();
                    BarCodeFormIDs.Add(barcodeform.Id);
                    lisbarcodeforms.Add(barcodeform);
                }
                if (!string.IsNullOrEmpty(sortFields))
                {
                    lisbarcodeforms = DBDao.GetListByHQL("Id in ('" + string.Join("','", BarCodeFormIDs) + "')", sortFields, -1, -1).list.ToList();
                }
                barCodeFormVos = ConvertLisBarCodeFormVo(lisbarcodeforms);
                if (barCodeFormVos != null && barCodeFormVos.Count > 0)
                {
                    long status = long.Parse(BarCodeStatus.样本签收.Value.Code);
                    //判断是否打包号模式，查询打包号信息
                    string isUsePackNoFlag = "";//用打包号签收标志
                    string PackNoIdentifier = "-1";//打包号标识符
                    if (!string.IsNullOrWhiteSpace(IsUseCollectPackNo.ParaValue))
                    {
                        string[] isUsePackNo = IsUseCollectPackNo.ParaValue.Split('|');
                        if (isUsePackNo.Length == 2)
                        {
                            isUsePackNoFlag = isUsePackNo[0];
                            PackNoIdentifier = isUsePackNo[1];
                        }
                    }
                    for (int i = 0; i < barCodeFormVos.Count; i++)
                    {
                        //查询所在打包号信息
                        if (isUsePackNoFlag == "1" && !string.IsNullOrWhiteSpace(barCodeFormVos[i].LisBarCodeForm.CollectPackNo))
                        {
                            long injectStatus = long.Parse(BarCodeStatus.样本拒收.Value.Code);
                            int count = DBDao.GetListCountByHQL("CollectPackNo = '" + barCodeFormVos[i].LisBarCodeForm.CollectPackNo + "'");//总数
                            int haveSignForCount = DBDao.GetListCountByHQL("CollectPackNo = '" + barCodeFormVos[i].LisBarCodeForm.CollectPackNo + "' and BarCodeStatusID=" + status);//已签收数量
                            int injectCount = DBDao.GetListCountByHQL("CollectPackNo = '" + barCodeFormVos[i].LisBarCodeForm.CollectPackNo + "' and BarCodeStatusID=" + injectStatus);//已拒收数量
                            barCodeFormVos[i].count = count;
                            barCodeFormVos[i].hasSignForCount = haveSignForCount;
                            barCodeFormVos[i].notSignForCount = injectCount;
                        }
                        //查询签收人
                        if (barCodeFormVos[i].LisBarCodeForm.BarCodeStatusID == status && barCodeFormVos[i].LisBarCodeForm.InceptTime != null)
                        {
                            var operateList = IBLisOperate.SearchListByHQL("OperateObjectID=" + barCodeFormVos[i].LisBarCodeForm.Id + " and OperateTypeID=" + BarCodeStatus.样本签收.Value.Id);
                            if (operateList != null && operateList.Count > 0)
                            {
                                barCodeFormVos[i].SignForMan = operateList.ToList()[0].OperateUser;
                            }
                        }
                    }
                }
            }
            return barCodeFormVos;
        }

        /// <summary>
        /// 通过条码号获取列表
        /// </summary>
        /// <param name="barCodes"></param>
        /// <param name="sickType"></param>
        /// <returns></returns>
        public List<LisBarCodeFormVo> GetSignForBarCodeFormListByBarCode(string barCodes, string sickType)
        {
            List<LisBarCodeFormVo> LisBarCodeFormVos = new List<LisBarCodeFormVo>();
            string[] barcodearr = barCodes.Split(',');
            string strwhere = "BarCode in ('" + string.Join("','", barcodearr) + "')";
            List<LisBarCodeForm> lisBarCodeForms = DBDao.GetListByHQL(strwhere).ToList();
            if (lisBarCodeForms == null || lisBarCodeForms.Count == 0)
            {
                //调用his
            }
            LisBarCodeFormVos = this.ConvertLisBarCodeFormVo(lisBarCodeForms);
            if (LisBarCodeFormVos != null && LisBarCodeFormVos.Count > 0)
            {
                long status = long.Parse(BarCodeStatus.样本签收.Value.Code);
                //查询签收人
                for (int i = 0; i < LisBarCodeFormVos.Count; i++)
                {
                    if (LisBarCodeFormVos[i].LisBarCodeForm.BarCodeStatusID == status && LisBarCodeFormVos[i].LisBarCodeForm.InceptTime != null)
                    {
                        var operateList = IBLisOperate.SearchListByHQL("OperateObjectID=" + LisBarCodeFormVos[i].LisBarCodeForm.Id + " and OperateTypeID=" + BarCodeStatus.样本签收.Value.Id);
                        if (operateList != null && operateList.Count > 0)
                        {
                            LisBarCodeFormVos[i].SignForMan = operateList.ToList()[0].OperateUser;
                        }
                    }

                }
            }
            return LisBarCodeFormVos;
        }
        /// <summary>
        /// 通过条码号或打包号获取打包号内所有样本列表
        /// </summary>
        /// <param name="nodetypeID"></param>
        /// <param name="barCodeOrPackNo"></param>
        /// <param name="sickType"></param>
        /// <param name="userID"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public List<LisBarCodeFormVo> GetSignForBarCodeFormListByBarCodeOrPackNo(long nodetypeID, string barCodeOrPackNo, string sickType, string userID, string userName)
        {
            //获取参数配置
            List<BPara> bParas = IBBParaItem.SelPreBParas(nodetypeID, "", Pre_AllModules.样本签收.Value.DefaultValue, userID, userName);
            var IsUseCollectPackNo = bParas.Where(w => w.ParaNo == "Pre_OrderSignFor_DefaultPara_0017").First();//获取是否通过打包号签收参数,1(0)|P 0:不使用，1:使用 P:打包号标识符
            List<LisBarCodeForm> lisBarCodeFormarr = new List<LisBarCodeForm>();
            string finalWhere = "";
            #region 判断是否打包号,拼接查询条件
            string isUsePackNoFlag = "";//用打包号签收标志
            string PackNoIdentifier = "-1";//打包号标识符
            if (!string.IsNullOrWhiteSpace(IsUseCollectPackNo.ParaValue))
            {
                string[] isUsePackNo = IsUseCollectPackNo.ParaValue.Split('|');
                if (isUsePackNo.Length == 2)
                {
                    isUsePackNoFlag = isUsePackNo[0];
                    PackNoIdentifier = isUsePackNo[1];
                }

            }
            if (barCodeOrPackNo.IndexOf(PackNoIdentifier) == 0)//是打包号
            {
                finalWhere = "CollectPackNo = '" + barCodeOrPackNo + "'";
            }
            else
            {
                finalWhere = "BarCode = '" + barCodeOrPackNo + "'";
            }
            #endregion
            lisBarCodeFormarr = DBDao.GetListByHQL(finalWhere).ToList();
            if (lisBarCodeFormarr == null || lisBarCodeFormarr.Count == 0)
            {
                //his获取数据
                if (!string.IsNullOrWhiteSpace(sickType))
                {

                }
            }
            var barCodeFormVos = ConvertLisBarCodeFormVo(lisBarCodeFormarr);
            if (barCodeFormVos != null && barCodeFormVos.Count > 0)
            {
                long status = long.Parse(BarCodeStatus.样本签收.Value.Code);
                long injectStatus = long.Parse(BarCodeStatus.样本拒收.Value.Code);
                if (barCodeOrPackNo.IndexOf(PackNoIdentifier) == 0)
                {
                    #region 是打包号
                    int count = DBDao.GetListCountByHQL("CollectPackNo = '" + barCodeOrPackNo + "'");//总数
                    int haveSignForCount = DBDao.GetListCountByHQL("CollectPackNo = '" + barCodeOrPackNo + "' and BarCodeStatusID=" + status);//已签收数量
                    int injectCount = DBDao.GetListCountByHQL("CollectPackNo = '" + barCodeOrPackNo + "' and BarCodeStatusID=" + injectStatus);//已拒收数量
                    for (int i = 0; i < barCodeFormVos.Count; i++)
                    {
                        barCodeFormVos[i].count = count;
                        barCodeFormVos[i].hasSignForCount = haveSignForCount;
                        barCodeFormVos[i].notSignForCount = injectCount;
                    }
                    #endregion
                }
                else
                {
                    #region 是条码号
                    if (!string.IsNullOrEmpty(barCodeFormVos[0].LisBarCodeForm.CollectPackNo))//打包号不为空，获取所在打包号所有列表
                    {
                        lisBarCodeFormarr = DBDao.GetListByHQL("CollectPackNo = '" + barCodeFormVos[0].LisBarCodeForm.CollectPackNo + "'").ToList();
                        barCodeFormVos = ConvertLisBarCodeFormVo(lisBarCodeFormarr);
                        int count = DBDao.GetListCountByHQL("CollectPackNo = '" + lisBarCodeFormarr[0].CollectPackNo + "'");//总数
                        int haveSignForCount = DBDao.GetListCountByHQL("CollectPackNo = '" + lisBarCodeFormarr[0].CollectPackNo + "' and BarCodeStatusID=" + status);//已签收数量
                        int injectCount = DBDao.GetListCountByHQL("CollectPackNo = '" + lisBarCodeFormarr[0].CollectPackNo + "' and BarCodeStatusID=" + injectStatus);//已拒收数量
                        for (int i = 0; i < barCodeFormVos.Count; i++)
                        {
                            barCodeFormVos[i].count = count;
                            barCodeFormVos[i].hasSignForCount = haveSignForCount;
                            barCodeFormVos[i].notSignForCount = injectCount;
                        }
                    }
                    #endregion
                }
                //查询签收人
                for (int i = 0; i < barCodeFormVos.Count; i++)
                {
                    if (barCodeFormVos[i].LisBarCodeForm.BarCodeStatusID == status && barCodeFormVos[i].LisBarCodeForm.InceptTime != null)
                    {
                        var operateList = IBLisOperate.SearchListByHQL("OperateObjectID=" + barCodeFormVos[i].LisBarCodeForm.Id + " and OperateTypeID=" + BarCodeStatus.样本签收.Value.Id);
                        if (operateList != null && operateList.Count > 0)
                        {
                            barCodeFormVos[i].SignForMan = operateList.ToList()[0].OperateUser;
                        }
                    }
                }
            }
            return barCodeFormVos;
        }

        /// <summary>
        /// 通过条码号签收
        /// </summary>
        /// <param name="nodetypeID"></param>
        /// <param name="barCodes"></param>
        /// <param name="sickType"></param>
        /// <param name="deliverier"></param>
        /// <param name="deliverierID"></param>
        /// <param name="fields"></param>
        /// <param name="isPlanish"></param>
        /// <param name="isAutoSignFor"></param>
        /// <param name="isForceSignFor"></param>
        /// <param name="userID"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public BaseResultDataValue EditSignForSampleByBarCode(long nodetypeID, string barCodes, string sickType, string deliverier, string deliverierID, string fields, bool isPlanish, bool isAutoSignFor, bool isForceSignFor, string userID, string userName, string Para_MoudleType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            #region 获取参数配置
            List<BPara> bParas = null;
            if (Para_MoudleType == "OrderDispense")
            {
                bParas = IBBParaItem.SelPreBParas(nodetypeID, "", Pre_AllModules.样本分发.Value.DefaultValue, userID, userName);
            }
            else
            {
                bParas = IBBParaItem.SelPreBParas(nodetypeID, "", Pre_AllModules.样本签收.Value.DefaultValue, userID, userName);
            }
            var WriteBackSignFor = bParas.Where(w => w.ParaNo == "Pre_" + Para_MoudleType + "_DefaultPara_0003").First();//回写签收标识接口
            var SignForAndCharge = bParas.Where(w => w.ParaNo == "Pre_" + Para_MoudleType + "_DefaultPara_0004").First();//计费接口
            var IsDistributionAndCharge = bParas.Where(w => w.ParaNo == "Pre_" + Para_MoudleType + "_DefaultPara_0005").First();//分发成功后调用计费接口，0:不调用；1:调用(签收不计费)
            var IsChargeFailRemind = bParas.Where(w => w.ParaNo == "Pre_" + Para_MoudleType + "_DefaultPara_0006").First();//计费失败是否提示
            var IsAllowChargeFailSignFor = bParas.Where(w => w.ParaNo == "Pre_" + Para_MoudleType + "_DefaultPara_0007").First();//计费失败是否允许签收
            BPara RecordDeliverier = new BPara();
            if (Para_MoudleType=="OrderSignFor")
            {
                RecordDeliverier = bParas.Where(w => w.ParaNo == "Pre_" + Para_MoudleType + "_DefaultPara_0019").First();//签收是否记录送达人
            } 
            var judgeUpdateSampleType = bParas.Where(w => w.ParaNo == "Pre_" + Para_MoudleType + "_DefaultPara_0026").First();//签收判断更新样本类型
            var updateAge = bParas.Where(w => w.ParaNo == "Pre_" + Para_MoudleType + "_DefaultPara_0025").First();//签收时更新年龄
            var IsUpdateSingleTime = bParas.Where(w => w.ParaNo == "Pre_" + Para_MoudleType + "_DefaultPara_0027").First();//签收时是否更新取单时间
            var IsRemindUrgent = bParas.Where(w => w.ParaNo == "Pre_" + Para_MoudleType + "_DefaultPara_0033").First();//急查标本签收提示 
            var barcodestatus = bParas.Where(w => w.ParaNo == "Pre_" + Para_MoudleType + "_DefaultPara_0059").First();//需要补录的状态
            var isVerify = bParas.Where(w => w.ParaNo == "Pre_" + Para_MoudleType + "_DefaultPara_0060").First();//是否校验
            #endregion
            List<LisBarCodeForm> list = new List<LisBarCodeForm>();
            string finalWhere = "1=2";
            if (!string.IsNullOrEmpty(barCodes))
            {
                string[] barcodearr = barCodes.Split(',');
                finalWhere = "BarCode in ('" + string.Join("','", barcodearr) + "')";

                if (!string.IsNullOrWhiteSpace(sickType))
                {
                    finalWhere += " and LisPatient.SickTypeId=" + sickType;
                }
            }
            ZhiFang.Common.Log.Log.Debug("finalWhere：" + finalWhere);
            var IList = DBDao.GetListByHQL(finalWhere);
            if (IList != null && IList.Count > 0)
            {
                list = IList.ToList();
            }
            if (list.Count == 0)
            {
                //调用his接口
            }
            if (list.Count > 0)
            {
                var barCodeFormVos = ConvertLisBarCodeFormVo(list);
                for (int i = 0; i < barCodeFormVos.Count; i++)
                {
                    //送达人判断
                    if (RecordDeliverier.ParaValue == "1" && (string.IsNullOrEmpty(deliverier) || string.IsNullOrEmpty(deliverierID)))//记录送达人
                    {
                        List<VerifyReminInfo> verifyReminInfos = new List<VerifyReminInfo>();
                        VerifyReminInfo verifyReminInfo = new VerifyReminInfo();
                        verifyReminInfo.alterMode = "1";
                        verifyReminInfo.failureInfo = "条码号为【" + barCodeFormVos[i].LisBarCodeForm.BarCode + "】的样本签收需要送达人，请选择送达人";
                        verifyReminInfos.Add(verifyReminInfo);
                        barCodeFormVos[i].failureInfo = Newtonsoft.Json.JsonConvert.SerializeObject(verifyReminInfos);
                    }
                    else
                    {
                        bool allowSignFor = true;//签收控制判断是否允许签收
                        #region 签收控制
                        if (isVerify.ParaValue == "1" && !isForceSignFor)//开启校验并不强制签收
                        {
                            string aa = "Pre_" + Para_MoudleType + "_DefaultPara_0043,Pre_" + Para_MoudleType + "_DefaultPara_0044,Pre_" + Para_MoudleType + "_DefaultPara_0045,Pre_" + Para_MoudleType + "_DefaultPara_0046," +
                            "Pre_" + Para_MoudleType + "_DefaultPara_0047Pre_" + Para_MoudleType + "_DefaultPara_0048,Pre_" + Para_MoudleType + "_DefaultPara_0049,Pre_" + Para_MoudleType + "_DefaultPara_0050,Pre_" + Para_MoudleType + "_DefaultPara_0051";
                            List<BPara> verifybparas = new List<BPara>();
                            foreach (var bpara in bParas)
                            {
                                if (aa.IndexOf(bpara.ParaNo) > -1)
                                {
                                    verifybparas.Add(bpara);
                                }
                            }
                            #region 校验
                            var paragroups = verifybparas.GroupBy(a => a.ParaValue);
                            List<VerifyReminInfo> verifyReminInfos = new List<VerifyReminInfo>();
                            bool isAllowAndRemind = true;//有一个不允许则不进行其他判断
                            foreach (var groups in paragroups)
                            {
                                if (!isAllowAndRemind)
                                {
                                    break;
                                }
                                if (groups.Key == "2" && isAllowAndRemind)
                                {
                                    allowSignFor = false;
                                    foreach (var group in groups)
                                    {
                                        barCodeFormVos[i].failureInfo = "";
                                        LisBarCodeFormVo lisBarCodeFormVO = SignForBarCodeFormVerify(group, barCodeFormVos[i], "2", Para_MoudleType);
                                        if (!string.IsNullOrEmpty(lisBarCodeFormVO.failureInfo))
                                        {
                                            VerifyReminInfo verifyReminInfo = new VerifyReminInfo();
                                            verifyReminInfo.alterMode = "2";
                                            verifyReminInfo.failureInfo = lisBarCodeFormVO.failureInfo;
                                            verifyReminInfos.Add(verifyReminInfo);
                                            lisBarCodeFormVO.failureInfo = Newtonsoft.Json.JsonConvert.SerializeObject(verifyReminInfos);
                                            barCodeFormVos[i] = lisBarCodeFormVO;
                                            isAllowAndRemind = false;
                                            break;
                                        }
                                    }
                                }
                                else if (groups.Key == "1" && isAllowAndRemind)
                                {
                                    allowSignFor = false;
                                    foreach (var group in groups)
                                    {
                                        barCodeFormVos[i].failureInfo = "";
                                        LisBarCodeFormVo lisBarCodeFormVO = SignForBarCodeFormVerify(group, barCodeFormVos[i], "1", Para_MoudleType);
                                        if (!string.IsNullOrEmpty(lisBarCodeFormVO.failureInfo))
                                        {
                                            VerifyReminInfo verifyReminInfo = new VerifyReminInfo();
                                            verifyReminInfo.alterMode = "1";
                                            verifyReminInfo.failureInfo = lisBarCodeFormVO.failureInfo;
                                            verifyReminInfos.Add(verifyReminInfo);
                                            lisBarCodeFormVO.failureInfo = Newtonsoft.Json.JsonConvert.SerializeObject(verifyReminInfos);
                                            barCodeFormVos[i] = lisBarCodeFormVO;
                                            isAllowAndRemind = false;
                                            break;
                                        }
                                    }
                                }
                                else if (groups.Key == "4" && isAllowAndRemind)
                                { 
                                    List<string> remindinfo = new List<string>();
                                    foreach (var group in groups)
                                    {
                                        barCodeFormVos[i].failureInfo = "";
                                        LisBarCodeFormVo lisBarCodeFormVO = SignForBarCodeFormVerify(group, barCodeFormVos[i], "4", Para_MoudleType);
                                        if (!string.IsNullOrEmpty(lisBarCodeFormVO.failureInfo))
                                        {
                                            allowSignFor = false;
                                            VerifyReminInfo verifyReminInfo = new VerifyReminInfo();
                                            verifyReminInfo.alterMode = "4";
                                            verifyReminInfo.failureInfo = lisBarCodeFormVO.failureInfo;
                                            verifyReminInfos.Add(verifyReminInfo);
                                        }
                                    }
                                }
                                else if (groups.Key == "3" && isAllowAndRemind)
                                {
                                    List<string> remindinfo = new List<string>();
                                    foreach (var group in groups)
                                    {
                                        barCodeFormVos[i].failureInfo = "";
                                        LisBarCodeFormVo lisBarCodeFormVO = SignForBarCodeFormVerify(group, barCodeFormVos[i], "3", Para_MoudleType);
                                        if (!string.IsNullOrEmpty(lisBarCodeFormVO.failureInfo))
                                        {
                                            VerifyReminInfo verifyReminInfo = new VerifyReminInfo();
                                            verifyReminInfo.alterMode = "3";
                                            verifyReminInfo.failureInfo = lisBarCodeFormVO.failureInfo;
                                            verifyReminInfos.Add(verifyReminInfo);
                                        }
                                    }
                                }
                                if (isAllowAndRemind)
                                {
                                    //3，4模式的提示信息
                                    barCodeFormVos[i].failureInfo = Newtonsoft.Json.JsonConvert.SerializeObject(verifyReminInfos);
                                }
                            }
                            #endregion
                        }
                        #endregion
                        //是否分发后才计费
                        if (IsDistributionAndCharge.ParaValue != "1")
                        {
                            //是否调用计费接口
                            if (!string.IsNullOrEmpty(SignForAndCharge.ParaValue))
                            {
                                //调用接口

                                //提示计费失败原因
                                if (IsChargeFailRemind.ParaValue == "1")
                                {

                                }
                                //如果失败是否允许签收,0失败不允许签收
                                if (IsAllowChargeFailSignFor.ParaValue == "0")
                                {

                                }
                            }
                        }
                        if (allowSignFor)
                        {
                            #region 允许签收后的具体签收操作
                            LisBarCodeForm lisBarCodeForm = StatusDateTimeInfoReplenish(barCodeFormVos[i].LisBarCodeForm, barcodestatus.ParaValue);//状态补录
                            lisBarCodeForm.InceptTime = DateTime.Now;//签收时间                                                                             
                            if (updateAge.ParaValue == "1")//更新年龄
                            {
                                if ((lisBarCodeForm.LisPatient.Age==null || lisBarCodeForm.LisPatient.Age == 0) && lisBarCodeForm.LisPatient.Birthday!=null)
                                {
                                    DateTime now = DateTime.Now;
                                    DateTime birthday = (DateTime)lisBarCodeForm.LisPatient.Birthday;
                                    int age = now.Year - birthday.Year;
                                    if (now.Month < birthday.Month || (now.Month == birthday.Month && now.Day < birthday.Day)) age--;
                                    lisBarCodeForm.LisPatient.Age = age;
                                }
                            }
                            //修改状态
                            long status = long.Parse(BarCodeStatus.样本签收.Value.Code);
                            long injectStatus = long.Parse(BarCodeStatus.样本拒收.Value.Code);
                            lisBarCodeForm.BarCodeStatusID = status;
                            lisBarCodeForm.BarCodeCurrentStatus = BarCodeStatus.样本签收.Value.Name;
                            //签收判断更新样本类型
                            if (judgeUpdateSampleType.ParaValue == "1" && (lisBarCodeForm.SampleTypeID == null || lisBarCodeForm.SampleTypeID == 0))
                            {
                                var BarCodeItemIList = IDLisBarCodeItemDao.GetListByHQL("BarCodeFormID=" + lisBarCodeForm.Id);
                                //多个item时则不赋值
                                if (BarCodeItemIList != null && BarCodeItemIList.Count == 1)
                                {
                                    var BarCodeItemList = BarCodeItemIList.ToList();
                                    var SampleItemIList = IDLBSampleItemDao.GetListByHQL("LBItem.ID=" + BarCodeItemList[0].BarCodesItemID);
                                    if (SampleItemIList != null && SampleItemIList.Count > 0)
                                    {
                                        lisBarCodeForm.SampleTypeID = SampleItemIList.ToList()[0].LBSampleType.Id;
                                    }
                                }
                            }
                            this.Entity = lisBarCodeForm;
                            bool formUpdate = this.Edit();
                            //操作记录
                            if (formUpdate)
                            {
                                //添加操作记录
                                IBLisOperate.AddLisOperate(lisBarCodeForm, BarCodeStatus.样本签收.Value, "样本签收", SysCookieValue);
                                //添加送达人
                                if (RecordDeliverier.ParaValue == "1" && (string.IsNullOrEmpty(deliverier) || string.IsNullOrEmpty(deliverierID)))
                                {
                                    IBLisOperate.AddLisOperate(lisBarCodeForm, BarCodeStatus.样本送达.Value, "样本送达", SysCookieValue, deliverier, long.Parse(deliverierID));
                                }
                                //回写签收标识
                                if (!string.IsNullOrEmpty(WriteBackSignFor.ParaValue))
                                {

                                }
                                //签收时是否更新取单时间
                                if (IsUpdateSingleTime.ParaValue == "1")
                                {

                                }
                                barCodeFormVos[i].SignForMan = userName;//签收人
                            }
                            else
                            {
                                brdv.success = false;
                                brdv.ErrorInfo = "签收失败";
                                return brdv;
                            }
                            if (isAutoSignFor)
                            {
                                //自动签收模式是否自动打印签收单
                            }
                            barCodeFormVos[i].LisBarCodeForm = lisBarCodeForm;
                            if (!string.IsNullOrEmpty(lisBarCodeForm.CollectPackNo))
                            {
                                //赋值vo打包号相关信息
                                int count = DBDao.GetListCountByHQL("CollectPackNo = '" + lisBarCodeForm.CollectPackNo + "'");//总数
                                int haveSignForCount = DBDao.GetListCountByHQL("CollectPackNo = '" + lisBarCodeForm.CollectPackNo + "' and BarCodeStatusID=" + status);//已签收数量
                                int injectCount = DBDao.GetListCountByHQL("CollectPackNo = '" + lisBarCodeForm.CollectPackNo + "' and BarCodeStatusID=" + injectStatus);//已拒收数量
                                barCodeFormVos[i].count = count;
                                barCodeFormVos[i].hasSignForCount = haveSignForCount;
                                barCodeFormVos[i].notSignForCount = injectCount;
                            }

                            //签收样本是急查时是否提示
                            if (IsRemindUrgent.ParaValue == "1" && barCodeFormVos[i].LisBarCodeForm.IsUrgent == 1)
                            {
                                List<VerifyReminInfo> verifyReminInfos = new List<VerifyReminInfo>();
                                VerifyReminInfo verifyReminInfo = new VerifyReminInfo();
                                verifyReminInfo.alterMode = "3";
                                verifyReminInfo.failureInfo = barCodeFormVos[i].LisBarCodeForm.BarCode + ":急查样本！";
                                verifyReminInfos.Add(verifyReminInfo);
                                barCodeFormVos[i].failureInfo = Newtonsoft.Json.JsonConvert.SerializeObject(verifyReminInfos);
                            }
                            #endregion
                        }

                    }
                }
                brdv.success = true;
                brdv.ResultDataValue = "";
                if (barCodeFormVos != null && barCodeFormVos.Count > 0)
                {
                    EntityList<LisBarCodeFormVo> entityList = new EntityList<LisBarCodeFormVo>();
                    entityList.count = barCodeFormVos.Count;
                    entityList.list = barCodeFormVos;
                    ParseObjectProperty pop = new ParseObjectProperty(fields);
                    try
                    {
                        if (isPlanish)
                        {
                            brdv.ResultDataValue = pop.GetObjectListPlanish<LisBarCodeFormVo>(entityList);
                        }
                        else
                        {
                            brdv.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                        }
                    }
                    catch (Exception ex)
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = "序列化错误：" + ex.Message;
                    }
                }
            }
            return brdv;
        }

        /// <summary>
        /// 签收前校验
        /// </summary>
        /// <param name="bPara"></param>
        /// <param name="lisBarCodeFormVO"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public LisBarCodeFormVo SignForBarCodeFormVerify(BPara bPara, LisBarCodeFormVo lisBarCodeFormVO, string status, string Para_MoudleType)
        {
            #region
            long SignForStatusId = long.Parse(BarCodeStatus.样本签收.Value.Code);
            long AcceptStatusId = long.Parse(BarCodeStatus.样本核收.Value.Code);
            long RefuseStatusId = long.Parse(BarCodeStatus.样本拒收.Value.Code);
            long CollectStatusId = long.Parse(BarCodeStatus.样本采集.Value.Code);
            long InspectionStatusId = long.Parse(BarCodeStatus.样本送检.Value.Code);
            long DeliveryStatusId = long.Parse(BarCodeStatus.样本送达.Value.Code);
            if (bPara.ParaNo == "Pre_" + Para_MoudleType + "_DefaultPara_0043" && lisBarCodeFormVO.LisBarCodeForm.BarCodeStatusID == SignForStatusId)
            {
                #region 处理
                if (status == "2")
                {
                    lisBarCodeFormVO.failureInfo = "样本已签收，不允许重复签收";
                }
                else if (status == "1")
                {
                    lisBarCodeFormVO.failureInfo = "样本已签收，不允许重复签收";
                }
                else if (status == "4")
                {
                    lisBarCodeFormVO.failureInfo = "样本已签收";
                }
                else if (status == "3")
                {
                    lisBarCodeFormVO.failureInfo = "样本已签收，允许重复签收且成功！";
                }
                #endregion
            }
            else if (bPara.ParaNo == "Pre_" + Para_MoudleType + "_DefaultPara_0045" && lisBarCodeFormVO.LisBarCodeForm.BarCodeStatusID == AcceptStatusId)
            {
                #region 处理
                if (status == "2")
                {
                    lisBarCodeFormVO.failureInfo = "样本已核收，不允许签收";
                }
                else if (status == "1")
                {
                    lisBarCodeFormVO.failureInfo = "样本已核收，不允许签收";
                }
                else if (status == "4")
                {
                    lisBarCodeFormVO.failureInfo = "样本已核收";
                }
                else if (status == "3")
                {
                    lisBarCodeFormVO.failureInfo = "样本已核收，允许签收且成功！";
                }
                #endregion
            }
            else if (bPara.ParaNo == "Pre_" + Para_MoudleType + "_DefaultPara_0046" && lisBarCodeFormVO.LisBarCodeForm.BarCodeStatusID == RefuseStatusId)
            {
                #region 处理
                if (status == "2")
                {
                    lisBarCodeFormVO.failureInfo = "样本已拒收，不允许签收";
                }
                else if (status == "1")
                {
                    lisBarCodeFormVO.failureInfo = "样本已拒收，不允许签收";
                }
                else if (status == "4")
                {
                    lisBarCodeFormVO.failureInfo = "样本已拒收";
                }
                else if (status == "3")
                {
                    lisBarCodeFormVO.failureInfo = "样本已拒收，允许签收且成功！";
                }
                #endregion
            }
            else if (bPara.ParaNo == "Pre_" + Para_MoudleType + "_DefaultPara_0047" && lisBarCodeFormVO.LisBarCodeForm.BarCodeStatusID < CollectStatusId)
            {
                #region 处理
                if (status == "2")
                {
                    lisBarCodeFormVO.failureInfo = "样本未采集，不允许签收";
                }
                else if (status == "1")
                {
                    lisBarCodeFormVO.failureInfo = "样本未采集，不允许签收";
                }
                else if (status == "4")
                {
                    lisBarCodeFormVO.failureInfo = "样本未采集";
                }
                else if (status == "3")
                {
                    lisBarCodeFormVO.failureInfo = "样本未采集，允许签收且成功！";
                }
                #endregion
            }
            else if (bPara.ParaNo == "Pre_" + Para_MoudleType + "_DefaultPara_0048" && lisBarCodeFormVO.LisBarCodeForm.BarCodeStatusID < InspectionStatusId)
            {
                #region 处理
                if (status == "2")
                {
                    lisBarCodeFormVO.failureInfo = "样本未送检，不允许签收";
                }
                else if (status == "1")
                {
                    lisBarCodeFormVO.failureInfo = "样本未送检，不允许签收";
                }
                else if (status == "4")
                {
                    lisBarCodeFormVO.failureInfo = "样本未送检";
                }
                else if (status == "3")
                {
                    lisBarCodeFormVO.failureInfo = "样本未送检，允许签收且成功！";
                }
                #endregion
            }
            else if (bPara.ParaNo == "Pre_" + Para_MoudleType + "_DefaultPara_0049" && lisBarCodeFormVO.LisBarCodeForm.BarCodeStatusID < DeliveryStatusId)
            {
                #region 处理
                if (status == "2")
                {
                    lisBarCodeFormVO.failureInfo = "样本未送达，不允许签收";
                }
                else if (status == "1")
                {
                    lisBarCodeFormVO.failureInfo = "样本未送达，不允许签收";
                }
                else if (status == "4")
                {
                    lisBarCodeFormVO.failureInfo = "样本未送达";
                }
                else if (status == "3")
                {
                    lisBarCodeFormVO.failureInfo = "样本未送达，允许签收且成功！";
                }
                #endregion
            }
            else if (bPara.ParaNo == "Pre_" + Para_MoudleType + "_DefaultPara_0050" && lisBarCodeFormVO.LisBarCodeForm.IsAffirm == 0)
            {
                #region 处理
                if (status == "2")
                {
                    lisBarCodeFormVO.failureInfo = "样本未确认，不允许签收";
                }
                else if (status == "1")
                {
                    lisBarCodeFormVO.failureInfo = "样本未确认，不允许签收";
                }
                else if (status == "4")
                {
                    lisBarCodeFormVO.failureInfo = "样本未确认";
                }
                else if (status == "3")
                {
                    lisBarCodeFormVO.failureInfo = "样本未确认，允许签收且成功！";
                }
                #endregion
            }
            else if (bPara.ParaNo == "Pre_" + Para_MoudleType + "_DefaultPara_0051" && lisBarCodeFormVO.LisBarCodeForm.ExecDeptID == null)
            {
                #region 处理
                if (status == "2")
                {
                    lisBarCodeFormVO.failureInfo = "样本没有执行科室，不允许签收";
                }
                else if (status == "1")
                {
                    lisBarCodeFormVO.failureInfo = "样本没有执行科室，不允许签收";
                }
                else if (status == "4")
                {
                    lisBarCodeFormVO.failureInfo = "样本没有执行科室";
                }
                else if (status == "3")
                {
                    lisBarCodeFormVO.failureInfo = "样本没有执行科室，允许签收且成功！";
                }
                #endregion
            }
            else if (bPara.ParaNo == "Pre_OrderDispense_DefaultPara_0066" && lisBarCodeFormVO.LisBarCodeForm.BarCodeStatusID < SignForStatusId)
            {
                #region 处理
                if (status == "2")
                {
                    lisBarCodeFormVO.failureInfo = "样本未签收，不允许分发";
                }
                else if (status == "1")
                {
                    lisBarCodeFormVO.failureInfo = "样本未签收，不允许分发";
                }
                else if (status == "4")
                {
                    lisBarCodeFormVO.failureInfo = "样本未签收，是否允许分发？";
                }
                else if (status == "3")
                {
                    lisBarCodeFormVO.failureInfo = "样本未签收，允许分发！";
                }
                #endregion
            }
            return lisBarCodeFormVO;
            #endregion
        }

        /// <summary>
        /// 通过打包号自动批量签收
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="packNo"></param>
        /// <param name="sickType"></param>
        /// <param name="deliverier"></param>
        /// <param name="deliverierID"></param>
        /// <param name="fields"></param>
        /// <param name="isPlanish"></param>
        /// <param name="bParas"></param>
        /// <param name="userID"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public BaseResultDataValue EditSampleSignForByPackNo(long nodetype, string packNo, string sickType, string deliverier, string deliverierID, string fields, bool isPlanish, List<BPara> bParas, string userID, string userName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = true;
            #region 配置参数
            var WriteBackSignFor = bParas.Where(w => w.ParaNo == "Pre_OrderSignFor_DefaultPara_0003").First();//回写签收标识接口
            var IsDistributionAndCharge = bParas.Where(w => w.ParaNo == "Pre_OrderSignFor_DefaultPara_0005").First();//分发成功后调用计费接口，0:不调用；1:调用(签收不计费)
            var SignForAndCharge = bParas.Where(w => w.ParaNo == "Pre_OrderSignFor_DefaultPara_0004").First();//计费接口
            var IsChargeFailRemind = bParas.Where(w => w.ParaNo == "Pre_OrderSignFor_DefaultPara_0006").First();//计费失败是否提示
            var IsAllowChargeFailSignFor = bParas.Where(w => w.ParaNo == "Pre_OrderSignFor_DefaultPara_0007").First();//计费失败是否允许签收
            var RecordDeliverier = bParas.Where(w => w.ParaNo == "Pre_OrderSignFor_DefaultPara_0019").First();//签收是否记录送达人
            var updateAge = bParas.Where(w => w.ParaNo == "Pre_OrderSignFor_DefaultPara_0025").First();//签收时更新年龄
            var judgeUpdateSampleType = bParas.Where(w => w.ParaNo == "Pre_OrderSignFor_DefaultPara_0026").First();//签收判断更新样本类型
            var IsUpdateSingleTime = bParas.Where(w => w.ParaNo == "Pre_OrderSignFor_DefaultPara_0027").First();//签收时是否更新取单时间
            var barcodestatus = bParas.Where(w => w.ParaNo == "Pre_OrderSignFor_DefaultPara_0059").First();//需要补录的状态
            var isVerify = bParas.Where(w => w.ParaNo == "Pre_OrderSignFor_DefaultPara_0060").First();//是否校验
            #endregion
            long status = long.Parse(BarCodeStatus.样本签收.Value.Code);
            long injectStatus = long.Parse(BarCodeStatus.样本拒收.Value.Code);
            List<LisBarCodeFormVo> barCodeFormVos = GetSignForBarCodeFormListByBarCodeOrPackNo(nodetype, packNo, sickType, userID, userName);
            int haveSignForCount = DBDao.GetListCountByHQL("CollectPackNo = '" + packNo + "' and BarCodeStatusID=" + status);//已签收数量
            if (barCodeFormVos != null && barCodeFormVos.Count > 0)
            {
                for (int i = 0; i < barCodeFormVos.Count; i++)
                {
                    if (barCodeFormVos[i].LisBarCodeForm.BarCodeStatusID < status)//未签收
                    {
                        //送达人判断
                        if (RecordDeliverier.ParaValue == "1" && (string.IsNullOrEmpty(deliverier) || string.IsNullOrEmpty(deliverierID)))//记录送达人
                        {
                            List<VerifyReminInfo> verifyReminInfos = new List<VerifyReminInfo>();
                            VerifyReminInfo verifyReminInfo = new VerifyReminInfo();
                            verifyReminInfo.alterMode = "1";
                            verifyReminInfo.failureInfo = "条码号为【" + barCodeFormVos[i].LisBarCodeForm.BarCode + "】的样本签收需要送达人，请选择送达人";
                            verifyReminInfos.Add(verifyReminInfo);
                            barCodeFormVos[i].failureInfo = Newtonsoft.Json.JsonConvert.SerializeObject(verifyReminInfos);
                        }
                        else
                        {
                            bool allowSignFor = true;//签收控制判断是否允许签收
                            #region 签收控制
                            if (isVerify.ParaValue == "1")//开启校验
                            {
                                string aa = "Pre_OrderSignFor_DefaultPara_0043,Pre_OrderSignFor_DefaultPara_0044,Pre_OrderSignFor_DefaultPara_0045,Pre_OrderSignFor_DefaultPara_0046," +
                                "Pre_OrderSignFor_DefaultPara_0047Pre_OrderSignFor_DefaultPara_0048,Pre_OrderSignFor_DefaultPara_0049,Pre_OrderSignFor_DefaultPara_0050,Pre_OrderSignFor_DefaultPara_0051";
                                List<BPara> verifybparas = new List<BPara>();
                                foreach (var bpara in bParas)
                                {
                                    if (aa.IndexOf(bpara.ParaNo) > -1)
                                    {
                                        verifybparas.Add(bpara);
                                    }
                                }
                                #region 校验
                                var paragroups = verifybparas.GroupBy(a => a.ParaValue);
                                List<VerifyReminInfo> verifyReminInfos = new List<VerifyReminInfo>();
                                bool isAllowAndRemind = true;//有一个不允许则不进行其他判断
                                foreach (var groups in paragroups)
                                {
                                    if (!isAllowAndRemind)
                                    {
                                        break;
                                    }
                                    if (groups.Key == "2" && isAllowAndRemind)
                                    {
                                        allowSignFor = false;
                                        foreach (var group in groups)
                                        {
                                            barCodeFormVos[i].failureInfo = "";
                                            LisBarCodeFormVo lisBarCodeFormVO = SignForBarCodeFormVerify(group, barCodeFormVos[i], "2", "OrderSignFor");
                                            if (!string.IsNullOrEmpty(lisBarCodeFormVO.failureInfo))
                                            {
                                                VerifyReminInfo verifyReminInfo = new VerifyReminInfo();
                                                verifyReminInfo.alterMode = "2";
                                                verifyReminInfo.failureInfo = lisBarCodeFormVO.failureInfo;
                                                verifyReminInfos.Add(verifyReminInfo);
                                                lisBarCodeFormVO.failureInfo = Newtonsoft.Json.JsonConvert.SerializeObject(verifyReminInfos);
                                                barCodeFormVos[i] = lisBarCodeFormVO;
                                                isAllowAndRemind = false;
                                                break;
                                            }
                                        }
                                    }
                                    else if (groups.Key == "1" && isAllowAndRemind)
                                    {
                                        allowSignFor = false;
                                        foreach (var group in groups)
                                        {
                                            barCodeFormVos[i].failureInfo = "";
                                            LisBarCodeFormVo lisBarCodeFormVO = SignForBarCodeFormVerify(group, barCodeFormVos[i], "1", "OrderSignFor");
                                            if (!string.IsNullOrEmpty(lisBarCodeFormVO.failureInfo))
                                            {
                                                VerifyReminInfo verifyReminInfo = new VerifyReminInfo();
                                                verifyReminInfo.alterMode = "1";
                                                verifyReminInfo.failureInfo = lisBarCodeFormVO.failureInfo;
                                                verifyReminInfos.Add(verifyReminInfo);
                                                lisBarCodeFormVO.failureInfo = Newtonsoft.Json.JsonConvert.SerializeObject(verifyReminInfos);
                                                barCodeFormVos[i] = lisBarCodeFormVO;
                                                isAllowAndRemind = false;
                                                break;
                                            }
                                        }
                                    }
                                    else if (groups.Key == "4" && isAllowAndRemind)
                                    {

                                        List<string> remindinfo = new List<string>();
                                        foreach (var group in groups)
                                        {
                                            barCodeFormVos[i].failureInfo = "";
                                            LisBarCodeFormVo lisBarCodeFormVO = SignForBarCodeFormVerify(group, barCodeFormVos[i], "4", "OrderSignFor");
                                            if (!string.IsNullOrEmpty(lisBarCodeFormVO.failureInfo))
                                            {
                                                VerifyReminInfo verifyReminInfo = new VerifyReminInfo();
                                                verifyReminInfo.alterMode = "4";
                                                verifyReminInfo.failureInfo = lisBarCodeFormVO.failureInfo;
                                                verifyReminInfos.Add(verifyReminInfo);
                                                allowSignFor = false;
                                            }
                                        }
                                    }
                                    else if (groups.Key == "3" && isAllowAndRemind)
                                    {
                                        List<string> remindinfo = new List<string>();
                                        foreach (var group in groups)
                                        {
                                            barCodeFormVos[i].failureInfo = "";
                                            LisBarCodeFormVo lisBarCodeFormVO = SignForBarCodeFormVerify(group, barCodeFormVos[i], "3", "OrderSignFor");
                                            if (!string.IsNullOrEmpty(lisBarCodeFormVO.failureInfo))
                                            {
                                                VerifyReminInfo verifyReminInfo = new VerifyReminInfo();
                                                verifyReminInfo.alterMode = "3";
                                                verifyReminInfo.failureInfo = string.Join(",", remindinfo);
                                                verifyReminInfos.Add(verifyReminInfo);
                                            }
                                        }
                                    }
                                    if (isAllowAndRemind)
                                    {
                                        //3，4模式的提示信息
                                        barCodeFormVos[i].failureInfo = Newtonsoft.Json.JsonConvert.SerializeObject(verifyReminInfos);
                                    }
                                }
                                #endregion
                            }
                            #endregion
                            //是否分发后才计费
                            if (IsDistributionAndCharge.ParaValue != "1")
                            {
                                //是否调用计费接口
                                if (!string.IsNullOrEmpty(SignForAndCharge.ParaValue))
                                {
                                    //调用接口

                                    //提示计费失败原因
                                    if (IsChargeFailRemind.ParaValue == "1")
                                    {

                                    }
                                    //如果失败是否允许签收,0失败不允许签收
                                    if (IsAllowChargeFailSignFor.ParaValue == "0")
                                    {

                                    }
                                }
                            }
                            #region 允许签收后的具体操作
                            if (allowSignFor)
                            {
                                barCodeFormVos[i].LisBarCodeForm = StatusDateTimeInfoReplenish(barCodeFormVos[i].LisBarCodeForm, barcodestatus.ParaValue);//状态补录
                                barCodeFormVos[i].LisBarCodeForm.InceptTime = DateTime.Now;//签收时间                                                                             
                                if (updateAge.ParaValue == "1")//更新年龄
                                {
                                    if (barCodeFormVos[i].LisBarCodeForm.LisPatient.Age == 0)
                                    {
                                        DateTime now = DateTime.Now;
                                        DateTime birthday = (DateTime)barCodeFormVos[i].LisBarCodeForm.LisPatient.Birthday;
                                        int age = now.Year - birthday.Year;
                                        if (now.Month < birthday.Month || (now.Month == birthday.Month && now.Day < birthday.Day)) age--;
                                        barCodeFormVos[i].LisBarCodeForm.LisPatient.Age = age;
                                    }
                                }
                                //修改状态
                                barCodeFormVos[i].LisBarCodeForm.BarCodeStatusID = status;
                                barCodeFormVos[i].LisBarCodeForm.BarCodeCurrentStatus = BarCodeStatus.样本签收.Value.Name;
                                //更新样本类型
                                if (judgeUpdateSampleType.ParaValue == "1" && (barCodeFormVos[i].LisBarCodeForm.SampleTypeID == null || barCodeFormVos[i].LisBarCodeForm.SampleTypeID == 0))
                                {

                                }
                                this.Entity = barCodeFormVos[i].LisBarCodeForm;
                                bool formUpdate = this.Edit();
                                //操作记录
                                if (formUpdate)
                                {
                                    haveSignForCount++;
                                    //添加操作记录
                                    IBLisOperate.AddLisOperate(barCodeFormVos[i].LisBarCodeForm, BarCodeStatus.样本签收.Value, "样本签收", SysCookieValue);
                                    //添加送达人
                                    if (RecordDeliverier.ParaValue == "1" && (string.IsNullOrEmpty(deliverier) || string.IsNullOrEmpty(deliverierID)))
                                    {
                                        IBLisOperate.AddLisOperate(barCodeFormVos[i].LisBarCodeForm, BarCodeStatus.样本送达.Value, "样本送达", SysCookieValue, deliverier, long.Parse(deliverierID));
                                    }
                                    //回写签收标识
                                    if (!string.IsNullOrEmpty(WriteBackSignFor.ParaValue))
                                    {

                                    }
                                    //签收判断更新样本类型
                                    if (judgeUpdateSampleType.ParaValue == "1")
                                    {

                                    }
                                    //签收时是否更新取单时间
                                    if (IsUpdateSingleTime.ParaValue == "1")
                                    {

                                    }
                                    barCodeFormVos[i].SignForMan = userName;
                                }
                                else
                                {
                                    brdv.success = false;
                                    brdv.ErrorInfo = "签收失败";
                                    return brdv;
                                }
                                //自动签收模式是否自动打印签收单
                            }
                            #endregion
                        }
                    }
                }
                #region 查询打包号相关信息
                int count = DBDao.GetListCountByHQL("CollectPackNo = '" + packNo + "'");//总数
                int injectCount = DBDao.GetListCountByHQL("CollectPackNo = '" + packNo + "' and BarCodeStatusID=" + injectStatus);//已拒收数量
                for (int i = 0; i < barCodeFormVos.Count; i++)
                {
                    barCodeFormVos[i].count = count;
                    barCodeFormVos[i].hasSignForCount = haveSignForCount;
                    barCodeFormVos[i].notSignForCount = injectCount;
                    //查询签收人
                    var operateList = IBLisOperate.SearchListByHQL("OperateObjectID=" + barCodeFormVos[i].LisBarCodeForm.Id + " and OperateTypeID=" + BarCodeStatus.样本签收.Value.Id);
                    if (operateList != null && operateList.Count > 0)
                    {
                        barCodeFormVos[i].SignForMan = operateList.ToList()[0].OperateUser;
                    }
                }
                #endregion
                EntityList<LisBarCodeFormVo> entityList = new EntityList<LisBarCodeFormVo>();
                entityList.count = barCodeFormVos.Count;
                entityList.list = barCodeFormVos;
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        brdv.ResultDataValue = pop.GetObjectListPlanish<LisBarCodeFormVo>(entityList);
                    }
                    else
                    {
                        brdv.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "序列化错误：" + ex.Message;
                }
            }
            return brdv;
        }

        /// <summary>
        /// 取消签收
        /// </summary>
        /// <param name="nodetypeID"></param>
        /// <param name="barCodes"></param>
        /// <param name="fields"></param>
        /// <param name="isPlanish"></param>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public BaseResultDataValue EditCancelSampleSignForOrByBarCode(long nodetypeID, string barCodes, string fields, bool isPlanish, string userId, string userName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (string.IsNullOrEmpty(barCodes))
            {
                brdv.success = false;
                brdv.ErrorInfo = "条码号不能为空";
            }
            else
            {
                brdv.success = true;
                //获取参数配置
                List<BPara> bParas = IBBParaItem.SelPreBParas(nodetypeID, "", Pre_AllModules.样本签收.Value.DefaultValue, userId, userName);
                //var CancelSampleSignForStatus = bParas.Where(w => w.ParaNo == "Pre_OrderSignFor_DefaultPara_0061").First();//取消签收后样本置为某个状态
                List<LisBarCodeFormVo> LisBarCodeFormVos = GetSignForBarCodeFormListByBarCode(barCodes, "");
                for (int i = 0; i < LisBarCodeFormVos.Count; i++)
                {
                    #region 签收前的状态
                    if (LisBarCodeFormVos[i].LisBarCodeForm.ArriveTime != null)
                    {
                        LisBarCodeFormVos[i].LisBarCodeForm.BarCodeStatusID = long.Parse(BarCodeStatus.样本送达.Value.Code);
                        LisBarCodeFormVos[i].LisBarCodeForm.BarCodeCurrentStatus = BarCodeStatus.样本送达.Value.Name;
                    }
                    else if (LisBarCodeFormVos[i].LisBarCodeForm.SendTime != null)
                    {
                        LisBarCodeFormVos[i].LisBarCodeForm.BarCodeStatusID = long.Parse(BarCodeStatus.样本送检.Value.Code);
                        LisBarCodeFormVos[i].LisBarCodeForm.BarCodeCurrentStatus = BarCodeStatus.样本送检.Value.Name;
                    }
                    else if (LisBarCodeFormVos[i].LisBarCodeForm.SendTime != null)
                    {
                        LisBarCodeFormVos[i].LisBarCodeForm.BarCodeStatusID = long.Parse(BarCodeStatus.样本送检.Value.Code);
                        LisBarCodeFormVos[i].LisBarCodeForm.BarCodeCurrentStatus = BarCodeStatus.样本送检.Value.Name;
                    }
                    else if (LisBarCodeFormVos[i].LisBarCodeForm.CollectTime != null)
                    {
                        LisBarCodeFormVos[i].LisBarCodeForm.BarCodeStatusID = long.Parse(BarCodeStatus.样本采集.Value.Code);
                        LisBarCodeFormVos[i].LisBarCodeForm.BarCodeCurrentStatus = BarCodeStatus.样本采集.Value.Name;
                    }
                    else if (LisBarCodeFormVos[i].LisBarCodeForm.PrintTime != null)
                    {
                        LisBarCodeFormVos[i].LisBarCodeForm.BarCodeStatusID = long.Parse(BarCodeStatus.条码打印.Value.Code);
                        LisBarCodeFormVos[i].LisBarCodeForm.BarCodeCurrentStatus = BarCodeStatus.条码打印.Value.Name;
                    }
                    else if (LisBarCodeFormVos[i].LisBarCodeForm.AffirmTime != null)
                    {
                        LisBarCodeFormVos[i].LisBarCodeForm.BarCodeStatusID = long.Parse(BarCodeStatus.条码确认.Value.Code);
                        LisBarCodeFormVos[i].LisBarCodeForm.BarCodeCurrentStatus = BarCodeStatus.条码确认.Value.Name;
                    }
                    else
                    {
                        LisBarCodeFormVos[i].LisBarCodeForm.BarCodeStatusID = long.Parse(BarCodeStatus.生成条码.Value.Code);
                        LisBarCodeFormVos[i].LisBarCodeForm.BarCodeCurrentStatus = BarCodeStatus.生成条码.Value.Name;
                    }
                    LisBarCodeFormVos[i].LisBarCodeForm.DataUpdateTime = DateTime.Now;
                    LisBarCodeFormVos[i].LisBarCodeForm.InceptTime = null;

                    #endregion
                    this.Entity = LisBarCodeFormVos[i].LisBarCodeForm;
                    if (this.Edit())
                    {
                        //增加取消签收操作记录
                        IBLisOperate.AddLisOperate(LisBarCodeFormVos[i].LisBarCodeForm, OrderFromOperateType.取消签收.Value, "取消签收", SysCookieValue);
                    }
                    else
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = "取消签收失败";
                        return brdv;
                    }
                }
                EntityList<LisBarCodeFormVo> entityList = new EntityList<LisBarCodeFormVo>();
                entityList.count = LisBarCodeFormVos.Count;
                entityList.list = LisBarCodeFormVos;
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        brdv.ResultDataValue = pop.GetObjectListPlanish<LisBarCodeFormVo>(entityList);
                    }
                    else
                    {
                        brdv.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "序列化错误：" + ex.Message;
                }
            }
            return brdv;
        }

        public BaseResultDataValue GetNeedPrintVoucherBarCodeFormListByBarCodeAndPara(long nodetypeID, string barCodes, string modelcode, string userId, string userName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            #region 获取参数配置
            List<BPara> bParas = IBBParaItem.SelPreBParas(nodetypeID, "", Pre_AllModules.样本签收.Value.DefaultValue, userId, userName);
            var PrintFilterConditions = bParas.Where(w => w.ParaNo == "Pre_OrderSignFor_DefaultPara_0032").First();//签收打印取单凭证过滤条件配置
            #endregion
            string finalWhere = "";
            string[] barCodesArray = barCodes.Split(',');
            finalWhere = "BarCode in ('" + string.Join("','", barCodesArray) + "')";
            if (!string.IsNullOrEmpty(PrintFilterConditions.ParaValue))
            {
                if (PrintFilterConditions.ParaValue.IndexOf("and") == 0 || PrintFilterConditions.ParaValue.IndexOf(" and") == 0)
                {
                    finalWhere += PrintFilterConditions.ParaValue;
                }
                else
                {
                    finalWhere += " and " + PrintFilterConditions.ParaValue;
                }
            }
            var ilist = DBDao.GetListByHQL(finalWhere);
            if (ilist != null && ilist.Count > 0)
            {
                List<string> barCodeList = new List<string>();
                var barCodeFormList = ilist.ToList();
                for (int i = 0; i < barCodeFormList.Count; i++)
                {
                    barCodeList.Add(barCodeFormList[i].BarCode);
                }
                string barCodesStr = string.Join(",", barCodeList);
                ZhiFang.Common.Log.Log.Error("BLisBarCodeForm.GetNeedPrintVoucherBarCodeFormListByBarCodeAndPara.符合打印条件的条码号：" + barCodesStr);
                brdv = GetBarCodeSamppleGatherVoucherData(nodetypeID, barCodesStr, true, true, modelcode);
            }
            else
            {
                brdv.ErrorInfo = "未查询到符合打印取单凭证的样本";
            }

            return brdv;
        }

        public BaseResultDataValue GetAllItemIdBySuperSectionID(string superSectionID)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            string ids = "";
            if (string.IsNullOrEmpty(superSectionID))
            {
                brdv.success = false;
                brdv.ErrorInfo = "专业大组ID不能为空！";
            }
            else
            {
                var lbSectionIList = IDLBSectionDao.GetListByHQL("SuperSectionID=" + superSectionID);
                if (lbSectionIList != null && lbSectionIList.Count > 0)
                {
                    List<long> setionIdList = new List<long>();
                    foreach (var item in lbSectionIList.ToList())
                    {
                        setionIdList.Add(item.Id);
                    }
                    var lbSectionItemIList = IDLBSectionItemDao.GetListByHQL("LBSection.Id in (" + string.Join(",", setionIdList) + ")");
                    if (lbSectionItemIList != null && lbSectionItemIList.Count > 0)
                    {
                        var lbSectionItemList = lbSectionItemIList.ToList();
                        List<long> setionItemIdList = new List<long>();
                        foreach (var item in lbSectionItemList)
                        {
                            setionItemIdList.Add(item.Id);
                        }
                        ids = string.Join(",", setionItemIdList);
                    }
                }
            }
            brdv.ResultDataValue = ids;
            return brdv;
        }

        public BaseResultDataValue GetAllItemIdBySectionID(string sectionID)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            string ids = "";
            if (string.IsNullOrEmpty(sectionID))
            {
                brdv.success = false;
                brdv.ErrorInfo = "小组ID不能为空！";
            }
            else
            {
                var lbSectionItemIList = IDLBSectionItemDao.GetListByHQL("LBSection.Id = " + sectionID);
                if (lbSectionItemIList != null && lbSectionItemIList.Count > 0)
                {
                    var lbSectionItemList = lbSectionItemIList.ToList();
                    List<long> setionItemIdList = new List<long>();
                    foreach (var item in lbSectionItemList)
                    {
                        setionItemIdList.Add(item.Id);
                    }
                    ids = string.Join(",", setionItemIdList);
                }
            }
            brdv.ResultDataValue = ids;
            return brdv;
        }
        #endregion
        #region 样本拒收
        /// <summary>
        /// 根据条件获取要拒收的列表
        /// </summary>
        /// <param name="where"></param>
        /// <param name="fields"></param>
        /// <param name="isPlanish"></param>
        /// <param name="sickTypeId"></param>
        /// <param name="sickTypeName"></param>
        /// <returns></returns>
        public BaseResultDataValue GetSampleListByWhereRefuseAccept(string where, string fields, bool isPlanish, string sickTypeId, string sickTypeName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = true;
            string finalWhere = "1=2";
            if (!string.IsNullOrWhiteSpace(where))
            {
                finalWhere = where;
            }
            List<LisBarCodeForm> BarCodeFormList = DBDao.GetListByHQL(finalWhere).ToList();
            if (BarCodeFormList.Count == 0)
            {
                if (finalWhere.Contains("BarCode"))
                {
                    //调用his接口
                }
            }
            if (BarCodeFormList != null && BarCodeFormList.Count > 0)
            {
                List<string> barcodeList = new List<string>();
                for (int i = 0; i < BarCodeFormList.Count; i++)
                {
                    barcodeList.Add(BarCodeFormList[i].BarCode);
                }
                #region 查询列表，赋值拒收信息
                List<LisBarCodeRefuseRecord> record = IDLisBarCodeRefuseRecordDao.GetListByHQL("BarCode in ('" + string.Join("','", barcodeList) + "')").ToList();
                List<LisBarCodeFormVo> lisBarCodeFormVos = ConvertLisBarCodeFormVo(BarCodeFormList);
                EntityList<LisBarCodeFormVo> entityList = new EntityList<LisBarCodeFormVo>();
                if (lisBarCodeFormVos != null && lisBarCodeFormVos.Count > 0)
                {
                    for (int i = 0; i < lisBarCodeFormVos.Count; i++)
                    {
                        for (int j = 0; j < record.Count; j++)
                        {
                            if (lisBarCodeFormVos[i].LisBarCodeForm.BarCode == record[j].BarCode)
                            {
                                lisBarCodeFormVos[i].RefuseAcceptReason = record[j].RefuseValue;
                                lisBarCodeFormVos[i].RefuseAcceptPerson = record[j].OperateUser;
                                lisBarCodeFormVos[i].RefuseAcceptPerson = record[j].RefuseOperate;
                                break;
                            }
                        }
                    }
                    entityList.count = lisBarCodeFormVos.Count;
                    entityList.list = lisBarCodeFormVos;
                }
                #endregion
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    brdv.success = true;
                    if (isPlanish)
                    {
                        brdv.ResultDataValue = pop.GetObjectListPlanish<LisBarCodeFormVo>(entityList);
                    }
                    else
                    {
                        brdv.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "序列化错误：" + ex.Message;
                }
            }
            return brdv;
        }

        /// <summary>
        /// 样本拒收具体操作
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="barcodes"></param>
        /// <param name="refuseReason"></param>
        /// <param name="handleAdvice"></param>
        /// <param name="answerPeople"></param>
        /// <param name="phoneNum"></param>
        /// <param name="refuseRemark"></param>
        /// <param name="fields"></param>
        /// <param name="isPlanish"></param>
        /// <param name="isForceReject"></param>
        /// <returns></returns>
        public BaseResultDataValue EditRefuseAcceptSample(long nodetypeID, string barcodes, string refuseReason, string handleAdvice, string answerPeople, string phoneNum, string refuseRemark, string fields, bool isPlanish, bool isForceReject, string userID, string userName, long refuseID)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (string.IsNullOrEmpty(barcodes))
            {
                brdv.success = false;
                brdv.ErrorInfo = "条码号不能为空";
                return brdv;
            }
            if (string.IsNullOrEmpty(refuseReason))
            {
                brdv.success = false;
                brdv.ErrorInfo = "请选择拒收原因";
                return brdv;
            }
            #region
            List<BPara> bParas = IBBParaItem.SelPreBParas(nodetypeID, "", Pre_AllModules.样本拒收.Value.DefaultValue, userID, userName);
            var RejectionWriteBackInterface = bParas.Where(w => w.ParaNo == "Pre_OrderRejection_DefaultPara_0001").First();//拒收回写标志接口
            var isRejectionReminder = bParas.Where(w => w.ParaNo == "Pre_OrderRejection_DefaultPara_0002").First();//拒收是否提示 
            var isAutoReject = bParas.Where(w => w.ParaNo == "Pre_OrderRejection_DefaultPara_0004").First();//扫描标签时是否自动拒收让步
            var isCallHIS = bParas.Where(w => w.ParaNo == "Pre_OrderRejection_DefaultPara_0005").First();//拒收数据不在lis数据库是否调用核收接口
            var barcodestatus = bParas.Where(w => w.ParaNo == "Pre_OrderRejection_DefaultPara_0007").First();//样本状态节点补录
            var isVerify = bParas.Where(w => w.ParaNo == "Pre_OrderRejection_DefaultPara_0008").First();//是否校验拒收信息
            #endregion
            string[] barCodeList = barcodes.Split(',');
            List<LisBarCodeForm> barcodeFormList = DBDao.GetListByHQL("BarCode in ('" + string.Join("','", barCodeList) + "')").ToList();
            if ((barcodeFormList == null || barcodeFormList.Count == 0) && isCallHIS.ParaValue == "1")
            {
                //调用his接口
            }
            List<LisBarCodeFormVo> LisBarCodeFormVos = ConvertLisBarCodeFormVo(barcodeFormList);
            if (LisBarCodeFormVos != null && LisBarCodeFormVos.Count > 0)
            {
                brdv.success = true;
                long status = long.Parse(BarCodeStatus.样本拒收.Value.Code);
                for (int i = 0; i < LisBarCodeFormVos.Count; i++)
                {
                    bool isAllowInject = true;
                    #region 拒收控制
                    if (!isForceReject)
                    {
                        if (isVerify.ParaValue == "1" && !isForceReject)//开启校验并不强制拒收
                        {
                            string aa = "Pre_OrderRejection_DefaultPara_0003";
                            List<BPara> verifybparas = new List<BPara>();
                            foreach (var bpara in bParas)
                            {
                                if (aa.IndexOf(bpara.ParaNo) > -1)
                                {
                                    verifybparas.Add(bpara);
                                }
                            }
                            #region 校验
                            var paragroups = verifybparas.GroupBy(a => a.ParaValue);
                            List<VerifyReminInfo> verifyReminInfos = new List<VerifyReminInfo>();
                            bool isAllowAndRemind = true;//有一个不允许则不进行其他判断
                            foreach (var groups in paragroups)
                            {
                                if (!isAllowAndRemind)
                                {
                                    break;
                                }
                                if (groups.Key == "2" && isAllowAndRemind)
                                {
                                    isAllowInject = false;
                                    foreach (var group in groups)
                                    {
                                        LisBarCodeFormVos[i].failureInfo = "";
                                        LisBarCodeFormVo lisBarCodeFormVO = InjectBarCodeFormVerify(group, LisBarCodeFormVos[i], "2");
                                        if (!string.IsNullOrEmpty(lisBarCodeFormVO.failureInfo))
                                        {
                                            VerifyReminInfo verifyReminInfo = new VerifyReminInfo();
                                            verifyReminInfo.alterMode = "2";
                                            verifyReminInfo.failureInfo = lisBarCodeFormVO.failureInfo;
                                            verifyReminInfos.Add(verifyReminInfo);
                                            lisBarCodeFormVO.failureInfo = Newtonsoft.Json.JsonConvert.SerializeObject(verifyReminInfos);
                                            LisBarCodeFormVos[i] = lisBarCodeFormVO;
                                            isAllowAndRemind = false;
                                            break;
                                        }
                                    }
                                }
                                else if (groups.Key == "1" && isAllowAndRemind)
                                {
                                    isAllowInject = false;
                                    foreach (var group in groups)
                                    {
                                        LisBarCodeFormVos[i].failureInfo = "";
                                        LisBarCodeFormVo lisBarCodeFormVO = InjectBarCodeFormVerify(group, LisBarCodeFormVos[i], "1");
                                        if (!string.IsNullOrEmpty(lisBarCodeFormVO.failureInfo))
                                        {
                                            VerifyReminInfo verifyReminInfo = new VerifyReminInfo();
                                            verifyReminInfo.alterMode = "1";
                                            verifyReminInfo.failureInfo = lisBarCodeFormVO.failureInfo;
                                            verifyReminInfos.Add(verifyReminInfo);
                                            lisBarCodeFormVO.failureInfo = Newtonsoft.Json.JsonConvert.SerializeObject(verifyReminInfos);
                                            LisBarCodeFormVos[i] = lisBarCodeFormVO;
                                            isAllowAndRemind = false;
                                            break;
                                        }
                                    }
                                }
                                else if (groups.Key == "4" && isAllowAndRemind)
                                {
                                    List<string> remindinfo = new List<string>();
                                    foreach (var group in groups)
                                    {
                                        LisBarCodeFormVos[i].failureInfo = "";
                                        LisBarCodeFormVo lisBarCodeFormVO = InjectBarCodeFormVerify(group, LisBarCodeFormVos[i], "4");
                                        if (!string.IsNullOrEmpty(lisBarCodeFormVO.failureInfo))
                                        {
                                            VerifyReminInfo verifyReminInfo = new VerifyReminInfo();
                                            verifyReminInfo.alterMode = "4";
                                            verifyReminInfo.failureInfo = lisBarCodeFormVO.failureInfo;
                                            verifyReminInfos.Add(verifyReminInfo);
                                            isAllowInject = false;
                                        }
                                    }
                                }
                                else if (groups.Key == "3" && isAllowAndRemind)
                                {
                                    List<string> remindinfo = new List<string>();
                                    foreach (var group in groups)
                                    {
                                        LisBarCodeFormVos[i].failureInfo = "";
                                        LisBarCodeFormVo lisBarCodeFormVO = InjectBarCodeFormVerify(group, LisBarCodeFormVos[i], "3");
                                        if (!string.IsNullOrEmpty(lisBarCodeFormVO.failureInfo))
                                        {
                                            VerifyReminInfo verifyReminInfo = new VerifyReminInfo();
                                            verifyReminInfo.alterMode = "3";
                                            verifyReminInfo.failureInfo = lisBarCodeFormVO.failureInfo;
                                            verifyReminInfos.Add(verifyReminInfo);
                                        }
                                    }
                                }
                                if (isAllowAndRemind)
                                {
                                    //3，4模式的提示信息
                                    LisBarCodeFormVos[i].failureInfo = Newtonsoft.Json.JsonConvert.SerializeObject(verifyReminInfos);
                                }
                            }
                            #endregion
                        }
                    }
                    #endregion
                    #region 允许拒收后的具体操作
                    if (isAllowInject)
                    {
                        LisBarCodeFormVos[i].LisBarCodeForm = StatusDateTimeInfoReplenish(LisBarCodeFormVos[i].LisBarCodeForm, barcodestatus.ParaValue);//状态补录
                        LisBarCodeFormVos[i].LisBarCodeForm.BarCodeStatusID = status;
                        LisBarCodeFormVos[i].LisBarCodeForm.BarCodeCurrentStatus = BarCodeStatus.样本拒收.Value.Name;
                        LisBarCodeFormVos[i].LisBarCodeForm.RejectTime = DateTime.Now;
                        LisBarCodeFormVos[i].LisBarCodeForm.DataUpdateTime = DateTime.Now;
                        this.Entity = LisBarCodeFormVos[i].LisBarCodeForm;
                        if (this.Edit())
                        {
                            //操作记录
                            IBLisOperate.AddLisOperate(LisBarCodeFormVos[i].LisBarCodeForm, BarCodeStatus.样本拒收.Value, "样本拒收", SysCookieValue);
                            //拒收原因记录表
                            LisBarCodeRefuseRecord refuseRecord = new LisBarCodeRefuseRecord();
                            refuseRecord.RefuseValue = refuseReason;
                            refuseRecord.OperateUser = userName;
                            refuseRecord.RefuseOperate = handleAdvice;
                            refuseRecord.OperateUserID = long.Parse(userID);
                            refuseRecord.Memo = refuseRemark;
                            refuseRecord.DeptTelNo = phoneNum;
                            refuseRecord.RelationUser = answerPeople;
                            if (refuseID > 0)
                            {
                                refuseRecord.RefuseID = refuseID;
                                var PhrasesWatchClassItemIList = IDLBPhrasesWatchClassItemDao.GetListByHQL("Id=" + refuseID);
                                if (PhrasesWatchClassItemIList != null && PhrasesWatchClassItemIList.Count > 0)
                                {
                                    var PhrasesWatchClassItemList = PhrasesWatchClassItemIList.ToList();
                                    refuseRecord.PhrasesWatchClassID = PhrasesWatchClassItemList[0].LBPhrasesWatchClass.Id;
                                    refuseRecord.PhrasesWatchClassItemID = PhrasesWatchClassItemList[0].Id;
                                }
                            }
                            if (!IDLisBarCodeRefuseRecordDao.Save(refuseRecord))
                            {
                                brdv.success = false;
                                brdv.ErrorInfo = "拒收失败！";
                                return brdv;
                            }
                            //回写标识
                            if (!string.IsNullOrEmpty(RejectionWriteBackInterface.ParaValue))
                            {

                            }
                            //提示信息
                            List<VerifyReminInfo> verifyReminInfos = new List<VerifyReminInfo>();
                            VerifyReminInfo verifyReminInfo = new VerifyReminInfo();
                            verifyReminInfo.alterMode = "3";
                            verifyReminInfo.failureInfo = LisBarCodeFormVos[i].LisBarCodeForm.BarCode + "拒收成功";
                            verifyReminInfos.Add(verifyReminInfo);
                            LisBarCodeFormVos[i].failureInfo = Newtonsoft.Json.JsonConvert.SerializeObject(verifyReminInfos);
                            LisBarCodeFormVos[i].RefuseAcceptAdvice = handleAdvice;
                            LisBarCodeFormVos[i].RefuseAcceptPerson = userName;
                            LisBarCodeFormVos[i].RefuseAcceptReason = refuseReason;
                        }
                        else
                        {
                            brdv.success = false;
                            brdv.ErrorInfo = "拒收失败！";
                            return brdv;
                        }
                    }
                    #endregion
                }
                EntityList<LisBarCodeFormVo> entityList = new EntityList<LisBarCodeFormVo>();
                entityList.count = LisBarCodeFormVos.Count;
                entityList.list = LisBarCodeFormVos;
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        brdv.ResultDataValue = pop.GetObjectListPlanish<LisBarCodeFormVo>(entityList);
                    }
                    else
                    {
                        brdv.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "序列化错误：" + ex.Message;
                }
            }
            else
            {
                brdv.success = false;
                brdv.ErrorInfo = "未查询到条码信息";
                return brdv;
            }
            return brdv;
        }
        /// <summary>
        /// 拒收前校验
        /// </summary>
        /// <param name="bPara"></param>
        /// <param name="lisBarCodeFormVO"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public LisBarCodeFormVo InjectBarCodeFormVerify(BPara bPara, LisBarCodeFormVo lisBarCodeFormVO, string status)
        {
            #region
            long AcceptStatusId = long.Parse(BarCodeStatus.样本核收.Value.Code);
            if (bPara.ParaNo == "Pre_OrderRejection_DefaultPara_0003" && lisBarCodeFormVO.LisBarCodeForm.BarCodeStatusID == AcceptStatusId)
            {
                #region 处理
                if (status == "2")
                {
                    lisBarCodeFormVO.failureInfo = "未查询到符合的样本单!";
                }
                else if (status == "1")
                {
                    lisBarCodeFormVO.failureInfo = "条码号为【" + lisBarCodeFormVO.LisBarCodeForm.BarCode + "】的样本已核收，不允许拒收!";
                }
                else if (status == "4")
                {
                    lisBarCodeFormVO.failureInfo = "条码号为【" + lisBarCodeFormVO.LisBarCodeForm.BarCode + "】的样本已核收确认是否允许拒收？";
                }
                else if (status == "3")
                {
                    lisBarCodeFormVO.failureInfo = "条码号为【" + lisBarCodeFormVO.LisBarCodeForm.BarCode + "】的样本已核收，但已拒收成功！";
                }
                #endregion
            }
            return lisBarCodeFormVO;
            #endregion
        }


        #endregion
        #region 样本分发
        /// <summary>
        /// 通过查询条件获取查询列表
        /// </summary>
        /// <param name="nodetypeID"></param>
        /// <param name="where"></param>
        /// <param name="userID"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public List<LisBarCodeFormVo> GetDispenseBarCodeFormListByWhere(long nodetypeID, string where, string userID, string userName, string sortFields, string relationForm)
        {
            ZhiFang.Common.Log.Log.Debug("GetSignForBarCodeFormListByWhere.入参sortFields：" + sortFields);
            #region 获取参数配置
            //List<BPara> bParas = IBBParaItem.SelPreBParas(nodetypeID, "", Pre_AllModules.样本签收.Value.DefaultValue, userID, userName);
            #endregion
            string hql = "select lisbarcodeform from LisBarCodeForm lisbarcodeform ";
            if (!string.IsNullOrEmpty(relationForm))
            {
                hql += "," + relationForm + " ";
            }
            var barCodeFormList = (this.DBDao as IDLisBarCodeFormDao).QueryLisBarCodeFormByHqlDao(where, sortFields, -1, -1, hql);
            List<LisBarCodeForm> lisbarcodeforms = new List<LisBarCodeForm>();
            List<long> BarCodeFormIDs = new List<long>();
            List<LisBarCodeFormVo> barCodeFormVos = new List<LisBarCodeFormVo>();
            if (barCodeFormList != null && barCodeFormList.Count > 0)
            {
                var formgroups = barCodeFormList.GroupBy(a => a.Id);
                foreach (var groups in formgroups)
                {
                    var barcodeform = groups.First();
                    BarCodeFormIDs.Add(barcodeform.Id);
                    lisbarcodeforms.Add(barcodeform);
                }
                if (!string.IsNullOrEmpty(sortFields))
                {
                    lisbarcodeforms = DBDao.GetListByHQL("Id in ('" + string.Join("','", BarCodeFormIDs) + "')", sortFields, -1, -1).list.ToList();
                }
                barCodeFormVos = ConvertLisBarCodeFormVo(lisbarcodeforms);
                #region 查询签收人
                long status = long.Parse(BarCodeStatus.样本签收.Value.Code);
                List<long> barCodeFormIds = new List<long>();
                EntityList<LisOperate> operateList = new EntityList<LisOperate>();
                for (int i = 0; i < barCodeFormVos.Count; i++)
                {
                    if (barCodeFormVos[i].LisBarCodeForm.BarCodeStatusID >= status && barCodeFormVos[i].LisBarCodeForm.InceptTime != null)
                    {
                        barCodeFormIds.Add(barCodeFormVos[i].LisBarCodeForm.Id);
                    }
                }
                if (barCodeFormIds.Count>0)
                {
                    operateList = IBLisOperate.SearchListByHQL("OperateObjectID in (" + string.Join(",", barCodeFormIds) + ") and OperateTypeID=" + BarCodeStatus.样本签收.Value.Id, "DataAddTime desc", -1, -1);
                    if (operateList.count > 0)
                    {
                        for (int i = 0; i < barCodeFormVos.Count; i++)
                        {
                            var operates = operateList.list.Where(a => a.OperateObjectID == barCodeFormVos[i].LisBarCodeForm.Id);
                            if (operates != null && operates.ToList().Count > 0)
                            {
                                barCodeFormVos[i].SignForMan = operateList.list[0].OperateUser;
                            }
                        }
                    }
                }
                #endregion
                barCodeFormVos = GetTestInfoByBarCode(barCodeFormVos);
            }
            return barCodeFormVos;
        }

        /// <summary>
        /// 通过条码号获取列表
        /// </summary>
        /// <param name="barCodes"></param>
        /// <param name="sickType"></param>
        /// <returns></returns>
        public List<LisBarCodeFormVo> GetDispenseBarCodeFormListByBarCode(string barCodes, string sickType)
        {
            List<LisBarCodeFormVo> LisBarCodeFormVos = new List<LisBarCodeFormVo>();
            string[] barcodearr = barCodes.Split(',');
            string strwhere = "BarCode in ('" + string.Join("','", barcodearr) + "')";
            List<LisBarCodeForm> lisBarCodeForms = DBDao.GetListByHQL(strwhere).ToList();
            if (lisBarCodeForms == null || lisBarCodeForms.Count == 0)
            {
                //调用his
            }
            //填充分发信息
            LisBarCodeFormVos = this.ConvertLisBarCodeFormVo(lisBarCodeForms);
            if (LisBarCodeFormVos != null && LisBarCodeFormVos.Count > 0)
            {
                long status = long.Parse(BarCodeStatus.样本签收.Value.Code);
                #region 查询签收人
                List<long> barCodeFormIds = new List<long>();
                EntityList<LisOperate> operateList = new EntityList<LisOperate>();
                for (int i = 0; i < LisBarCodeFormVos.Count; i++)
                {
                    if (LisBarCodeFormVos[i].LisBarCodeForm.BarCodeStatusID >= status && LisBarCodeFormVos[i].LisBarCodeForm.InceptTime != null)
                    {
                        barCodeFormIds.Add(LisBarCodeFormVos[i].LisBarCodeForm.Id);
                    }
                }
                operateList = IBLisOperate.SearchListByHQL("OperateObjectID in (" + string.Join(",", barCodeFormIds) + ") and OperateTypeID=" + BarCodeStatus.样本签收.Value.Id, "DataAddTime desc", -1, -1);
                if (operateList.count > 0)
                {
                    for (int i = 0; i < LisBarCodeFormVos.Count; i++)
                    {
                        var operates = operateList.list.Where(a => a.OperateObjectID == LisBarCodeFormVos[i].LisBarCodeForm.Id);
                        if (operates != null && operates.ToList().Count > 0)
                        {
                            LisBarCodeFormVos[i].SignForMan = operateList.list[0].OperateUser;
                        }
                    }
                }
                #endregion
                LisBarCodeFormVos = GetTestInfoByBarCode(LisBarCodeFormVos);
            }
            return LisBarCodeFormVos;
        }

        /// <summary>
        /// 根据样本单id获取所包含项目的分发信息列表
        /// </summary>
        /// <param name="barcodeFormId"></param>
        /// <returns></returns>
        public List<LisBarCodeItemVoResp> GetBarCodeItemDispenseInfoListByFormId(long barcodeFormId)
        {
            List<LisBarCodeItemVoResp> lisBarCodeItemVos = new List<LisBarCodeItemVoResp>();
            IList<LisBarCodeItem> LisBarCodeItemIList = IDLisBarCodeItemDao.GetListByHQL("BarCodeFormID=" + barcodeFormId);
            if (LisBarCodeItemIList != null && LisBarCodeItemIList.Count > 0)
            {
                List<LisBarCodeItem> LisBarCodeItemList = LisBarCodeItemIList.ToList();
                List<long> BarCodeItemIDList = new List<long>();//BarCodeItem主键id
                List<long> BarCodesItemIDList = new List<long>();//BarCodeItem项目id
                foreach (var lisBarCodeItem in LisBarCodeItemList){
                    BarCodeItemIDList.Add(lisBarCodeItem.Id);
                    BarCodesItemIDList.Add((long)lisBarCodeItem.BarCodesItemID);
                }
                //查LBItem
                IList<LBItem> LBItemList=IDLBItemDao.GetListByHQL("Id in (" + string.Join(",", BarCodesItemIDList) + ")");
                //查LisTestItem
                IList<LisTestItem> LisTestItemIList = IBLisTestItem.SearchListByHQL("LisBarCodeItem.Id in (" + string.Join(",", BarCodeItemIDList) + ")");
                foreach (var lisBarCodeItem in LisBarCodeItemList)
                {
                    LisBarCodeItemVoResp lisBarCodeItemVo = new LisBarCodeItemVoResp();
                    //lisBarCodeItemVo.BarCodeItemID = lisBarCodeItem.Id;//项目id
                    lisBarCodeItemVo.DispenseStatus = "未分发";//分发状态
                    if (lisBarCodeItem.ItemDispenseFlag == 1)
                    {
                        lisBarCodeItemVo.DispenseStatus = "已分发";//分发状态
                    }
                    lisBarCodeItemVo.ItemName = LBItemList.Where(a => a.Id == lisBarCodeItem.BarCodesItemID).ToList().First().CName;//项目名称
                    if (LisTestItemIList != null && LisTestItemIList.Count > 0)
                    {
                        var dispenseitem = LisTestItemIList.Where(a => a.LisBarCodeItem.Id == lisBarCodeItem.Id);
                        if (dispenseitem!=null && dispenseitem.ToList().Count>0)
                        {
                            lisBarCodeItemVo.SampleNo = dispenseitem.ToList().First().LisTestForm.GSampleNo;//样本号
                            lisBarCodeItemVo.SectionName = dispenseitem.ToList().First().LisTestForm.LBSection.CName;//小组名称
                        }
                    }
                    lisBarCodeItemVo.OrderItemName = lisBarCodeItem.LisBarCodeForm.LisOrderForm.ParItemCName;//医嘱项目名称
                    lisBarCodeItemVos.Add(lisBarCodeItemVo);
                }
            }
            return lisBarCodeItemVos;
        }

        /// <summary>
        /// 样本分发操作
        /// </summary>
        /// <param name="nodetypeID"></param>
        /// <param name="barCodes"></param>
        /// <param name="sickType"></param>
        /// <param name="isForceDispense"></param>
        /// <param name="userID"></param>
        /// <param name="userName"></param>
        /// <param name="isPlanish"></param>
        /// <param name="fields"></param>
        /// <param name="TestDate"></param>
        /// <param name="ruleType"></param>
        /// <returns></returns>
        public BaseResultDataValue EditDispenseSampleByBarCode(long nodetypeID, string barCodes, string sickType, bool isForceDispense, string userID, string userName, bool isPlanish, string fields, string TestDate, string ruleType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            #region 配置参数
            List<BPara> bParas = IBBParaItem.SelPreBParas(nodetypeID, "", Pre_AllModules.样本分发.Value.DefaultValue, userID, userName);
            var isVerify = bParas.Where(w => w.ParaNo == "Pre_OrderDispense_DefaultPara_0069").First();//是否校验
            var IsDistributionAndCharge = bParas.Where(w => w.ParaNo == "Pre_OrderDispense_DefaultPara_0005").First();//分发成功后调用计费接口，0:不调用；1:调用(签收不计费)
            var SignForAndCharge = bParas.Where(w => w.ParaNo == "Pre_OrderDispense_DefaultPara_0004").First();//计费接口
            var IsChargeFailRemind = bParas.Where(w => w.ParaNo == "Pre_OrderDispense_DefaultPara_0006").First();//计费失败是否提示
            //var IsAllowDispenseByChildItem = bParas.Where(w => w.ParaNo == "Pre_OrderDispense_DefaultPara_0064").First();//是否允许按子项分发
            #endregion
            string finalWhere = "1=2";
            if (!string.IsNullOrEmpty(barCodes))
            {
                string[] barcodearr = barCodes.Split(',');
                finalWhere = "BarCode in ('" + string.Join("','", barcodearr) + "')";
                ZhiFang.Common.Log.Log.Debug("finalWhere：" + finalWhere);
                List<LisBarCodeForm> list = new List<LisBarCodeForm>();
                var IList = DBDao.GetListByHQL(finalWhere);
                if (IList != null && IList.Count > 0)
                {
                    list = IList.ToList();
                }
                if (list.Count == 0)
                {
                    //调用his接口
                }
                if (list.Count > 0)
                {
                    long SignForStatusId = long.Parse(BarCodeStatus.样本签收.Value.Code);
                    long DispenseStatusId = long.Parse(BarCodeStatus.样本分发.Value.Code);
                    long AcceptStatusId = long.Parse(BarCodeStatus.样本核收.Value.Code);
                    var barCodeFormVos = ConvertLisBarCodeFormVo(list);
                    for (int i = 0; i < barCodeFormVos.Count; i++)
                    {
                        //已分发和已核收不能重复分发
                        if ((barCodeFormVos[i].LisBarCodeForm.BarCodeStatusID == DispenseStatusId && barCodeFormVos[i].LisBarCodeForm.TranTime != null) || (barCodeFormVos[i].LisBarCodeForm.BarCodeStatusID == AcceptStatusId && barCodeFormVos[i].LisBarCodeForm.ReceiveTime != null))
                        {
                            List<VerifyReminInfo> verifyReminInfos = new List<VerifyReminInfo>();
                            VerifyReminInfo verifyReminInfo = new VerifyReminInfo();
                            verifyReminInfo.alterMode = "1";
                            if (barCodeFormVos[i].LisBarCodeForm.BarCodeStatusID == DispenseStatusId)
                            {
                                verifyReminInfo.failureInfo = "样本已分发，不允许重复分发";
                            }
                            else
                            {
                                verifyReminInfo.failureInfo = "样本已核收，不允许分发";
                            }
                            verifyReminInfos.Add(verifyReminInfo);
                            barCodeFormVos[i].failureInfo = Newtonsoft.Json.JsonConvert.SerializeObject(verifyReminInfos);
                        }
                        else
                        {
                            bool allowDispense = true;//签收控制判断是否允许签收
                            #region 分发控制
                            if (isVerify.ParaValue == "1" && !isForceDispense)//开启校验并不强制签收
                            {
                                string aa = "Pre_OrderDispense_DefaultPara_0066";
                                List<BPara> verifybparas = new List<BPara>();
                                foreach (var bpara in bParas)
                                {
                                    if (aa.IndexOf(bpara.ParaNo) > -1)
                                    {
                                        verifybparas.Add(bpara);
                                    }
                                }
                                #region 校验
                                var paragroups = verifybparas.GroupBy(a => a.ParaValue);
                                List<VerifyReminInfo> verifyReminInfos = new List<VerifyReminInfo>();
                                bool isAllowAndRemind = true;//有一个不允许则不进行其他判断
                                foreach (var groups in paragroups)
                                {
                                    if (!isAllowAndRemind)
                                    {
                                        break;
                                    }
                                    if (groups.Key == "2" && isAllowAndRemind)
                                    {
                                        allowDispense = false;
                                        foreach (var group in groups)
                                        {
                                            barCodeFormVos[i].failureInfo = "";
                                            LisBarCodeFormVo lisBarCodeFormVO = SignForBarCodeFormVerify(group, barCodeFormVos[i], "2", "OrderDispense");
                                            if (!string.IsNullOrEmpty(lisBarCodeFormVO.failureInfo))
                                            {
                                                VerifyReminInfo verifyReminInfo = new VerifyReminInfo();
                                                verifyReminInfo.alterMode = "2";
                                                verifyReminInfo.failureInfo = lisBarCodeFormVO.failureInfo;
                                                verifyReminInfos.Add(verifyReminInfo);
                                                lisBarCodeFormVO.failureInfo = Newtonsoft.Json.JsonConvert.SerializeObject(verifyReminInfos);
                                                barCodeFormVos[i] = lisBarCodeFormVO;
                                                isAllowAndRemind = false;
                                                break;
                                            }
                                        }
                                    }
                                    else if (groups.Key == "1" && isAllowAndRemind)
                                    {
                                        allowDispense = false;
                                        foreach (var group in groups)
                                        {
                                            barCodeFormVos[i].failureInfo = "";
                                            LisBarCodeFormVo lisBarCodeFormVO = SignForBarCodeFormVerify(group, barCodeFormVos[i], "1", "OrderDispense");
                                            if (!string.IsNullOrEmpty(lisBarCodeFormVO.failureInfo))
                                            {
                                                VerifyReminInfo verifyReminInfo = new VerifyReminInfo();
                                                verifyReminInfo.alterMode = "1";
                                                verifyReminInfo.failureInfo = lisBarCodeFormVO.failureInfo;
                                                verifyReminInfos.Add(verifyReminInfo);
                                                lisBarCodeFormVO.failureInfo = Newtonsoft.Json.JsonConvert.SerializeObject(verifyReminInfos);
                                                barCodeFormVos[i] = lisBarCodeFormVO;
                                                isAllowAndRemind = false;
                                                break;
                                            }
                                        }
                                    }
                                    else if (groups.Key == "4" && isAllowAndRemind)
                                    {
                                        List<string> remindinfo = new List<string>();
                                        foreach (var group in groups)
                                        {
                                            barCodeFormVos[i].failureInfo = "";
                                            LisBarCodeFormVo lisBarCodeFormVO = SignForBarCodeFormVerify(group, barCodeFormVos[i], "4", "OrderDispense");
                                            if (!string.IsNullOrEmpty(lisBarCodeFormVO.failureInfo))
                                            {
                                                allowDispense = false;
                                                VerifyReminInfo verifyReminInfo = new VerifyReminInfo();
                                                verifyReminInfo.alterMode = "4";
                                                verifyReminInfo.failureInfo = lisBarCodeFormVO.failureInfo;
                                                verifyReminInfos.Add(verifyReminInfo);
                                            }
                                        }
                                    }
                                    else if (groups.Key == "3" && isAllowAndRemind)
                                    {
                                        List<string> remindinfo = new List<string>();
                                        foreach (var group in groups)
                                        {
                                            barCodeFormVos[i].failureInfo = "";
                                            LisBarCodeFormVo lisBarCodeFormVO = SignForBarCodeFormVerify(group, barCodeFormVos[i], "3", "OrderDispense");
                                            if (!string.IsNullOrEmpty(lisBarCodeFormVO.failureInfo))
                                            {
                                                VerifyReminInfo verifyReminInfo = new VerifyReminInfo();
                                                verifyReminInfo.alterMode = "3";
                                                verifyReminInfo.failureInfo = lisBarCodeFormVO.failureInfo;
                                                verifyReminInfos.Add(verifyReminInfo);
                                            }
                                        }
                                    }
                                    if (isAllowAndRemind)
                                    {
                                        //3，4模式的提示信息
                                        barCodeFormVos[i].failureInfo = Newtonsoft.Json.JsonConvert.SerializeObject(verifyReminInfos);
                                    }
                                }
                                #endregion
                            }
                            #endregion
                            /**
                            //是否分发后才计费
                            if (IsDistributionAndCharge.ParaValue == "1")
                            {
                                //是否调用计费接口
                                if (!string.IsNullOrEmpty(SignForAndCharge.ParaValue))
                                {
                                    //调用接口

                                    //提示计费失败原因
                                    if (IsChargeFailRemind.ParaValue == "1")
                                    {

                                    }
                                }
                            }*/
                            if (allowDispense)
                            {
                                //最终未分发项目列表
                                List<LisBarCodeItemVo> finalNotDispenseList = new List<LisBarCodeItemVo>();
                                //最终分发列表
                                List<LisBarCodeItemVo> finalDispenseList = new List<LisBarCodeItemVo>();
                                //获取分发规则站点所拥有小组
                                var LBTranRuleHostSectionEntityList = IDLBTranRuleHostSectionDao.GetListByHQL("HostTypeID=" + nodetypeID, "DispOrder", -1, -1);//排序
                                if (LBTranRuleHostSectionEntityList != null && LBTranRuleHostSectionEntityList.count > 0)
                                {
                                    var LBTranRuleHostSectionList = LBTranRuleHostSectionEntityList.list;
                                    //规则如果设置启用下一样本号，分发成功后需要更新分发规则
                                    List<LBTranRule> needUpdateLBTranRuleList = new List<LBTranRule>();
                                    //查询样本单下未分发项目
                                    var LisBarCodeItemIList = IDLisBarCodeItemDao.GetListByHQL("LisBarCodeForm.Id=" + barCodeFormVos[i].LisBarCodeForm.Id + " and ItemDispenseFlag=0");

                                    if (LisBarCodeItemIList != null && LisBarCodeItemIList.Count > 0)
                                    {
                                        #region 查询所有小组的规则
                                        List<long> TranRuleHostSectionIds = new List<long>();
                                        foreach (var TranRuleHostSectionItem in LBTranRuleHostSectionList)
                                        {
                                            TranRuleHostSectionIds.Add((long)TranRuleHostSectionItem.SectionID);
                                        }
                                        var LBTranRuleIList = IDLBTranRuleDao.GetListByHQL("SectionID in (" + string.Join(",", TranRuleHostSectionIds)+")");
                                        #endregion
                                        List<LisBarCodeItemVo> LisBarCodeItemVoList = new List<LisBarCodeItemVo>();//
                                        List<long> barCodesItemIDList = new List<long>();
                                        foreach (var lisBarCodeItem in LisBarCodeItemIList.ToList())
                                        {
                                            if (lisBarCodeItem.BarCodesItemID != null)
                                            {
                                                barCodesItemIDList.Add((long)lisBarCodeItem.BarCodesItemID);
                                            }
                                        }
                                        #region 构造vo，填充项目所属的所有小组信息供匹配规则时使用，项目信息
                                        var LBSectionItemIList = IDLBSectionItemDao.GetListByHQL("LBItem.Id in (" + string.Join(",", barCodesItemIDList) + ")");
                                        var LBItemIList= IDLBItemDao.GetListByHQL("Id in (" + string.Join(",", barCodesItemIDList) + ")");
                                        foreach (var lisBarCodeItem in LisBarCodeItemIList.ToList())
                                        {
                                            LisBarCodeItemVo lisBarCodeItemVo = new LisBarCodeItemVo();
                                            lisBarCodeItemVo.LisBarCodeItem = lisBarCodeItem;
                                            lisBarCodeItemVo.BarCodeItemID = lisBarCodeItem.Id;
                                            //查询项目的小组
                                            if (lisBarCodeItem.BarCodesItemID != null)
                                            {
                                                var sectionItem = LBSectionItemIList.Where(a => a.LBItem.Id == (long)lisBarCodeItem.BarCodesItemID);
                                                var lbItem= LBItemIList.Where(a => a.Id == (long)lisBarCodeItem.BarCodesItemID).ToList().First();
                                                lisBarCodeItemVo.ItemName = lbItem.CName;
                                                lisBarCodeItemVo.ItemID = lbItem.Id;
                                                if (LBSectionItemIList != null && LBSectionItemIList.Count > 0)
                                                {
                                                    //lisBarCodeItemVo.SectionId = LBSectionItemIList.ToList().First().LBSection.Id;
                                                    //lisBarCodeItemVo.SectionItemID = LBSectionItemIList.ToList().First().Id;
                                                    //lisBarCodeItemVo.ItemID = LBSectionItemIList.ToList().First().LBItem.Id;
                                                    //lisBarCodeItemVo.LBSectionItem = LBSectionItemIList.ToList().First();
                                                    lisBarCodeItemVo.tempSectionItemList = sectionItem.ToList();
                                                }
                                            }
                                            LisBarCodeItemVoList.Add(lisBarCodeItemVo);
                                        }
                                        #endregion
                                        #region 匹配规则,返回分发列表并填充未分发列表
                                        long ruleTypeId = 0;
                                        if (!string.IsNullOrEmpty(ruleType))
                                        {
                                            ruleTypeId = long.Parse(ruleType);
                                        }
                                        finalDispenseList = ItemMachtRuleAndFindNotDispenseItem(LisBarCodeItemVoList, ref finalNotDispenseList,finalDispenseList, LBTranRuleHostSectionList, barCodeFormVos[i].LisBarCodeForm, LBTranRuleIList, ruleTypeId);
                                        #endregion
                                        #region 具体分发操作
                                        var barCodeFormVo = barCodeFormVos[i];
                                        finalNotDispenseList=DispenseOperation(finalDispenseList,finalNotDispenseList,ref barCodeFormVo, TestDate);
                                        barCodeFormVos[i] = barCodeFormVo;
                                        #endregion
                                        //未分发成功列表拼接信息返回前台
                                    }
                                    //未分发项目数量为0，并且未分发
                                    if (finalNotDispenseList.Count==0 && barCodeFormVos[i].LisBarCodeForm.BarCodeStatusID < DispenseStatusId)
                                    {
                                        barCodeFormVos[i].LisBarCodeForm.BarCodeStatusID = DispenseStatusId;
                                        barCodeFormVos[i].LisBarCodeForm.TranTime = DateTime.Now;
                                        barCodeFormVos[i].LisBarCodeForm.DispenseFlag = 1;
                                        barCodeFormVos[i].LisBarCodeForm.BarCodeCurrentStatus = BarCodeStatus.样本分发.Value.Name;
                                        this.Entity = barCodeFormVos[i].LisBarCodeForm;
                                        if (this.Edit())
                                        {
                                            IBLisOperate.AddLisOperate(barCodeFormVos[i].LisBarCodeForm, BarCodeStatus.样本分发.Value, "样本分发", SysCookieValue);
                                        }
                                    }
                                    //未分发列表不为0，拼接提示消息
                                    if (finalNotDispenseList.Count>0)
                                    {
                                        List<VerifyReminInfo> verifyReminInfos = new List<VerifyReminInfo>();
                                        VerifyReminInfo verifyReminInfo = new VerifyReminInfo();
                                        verifyReminInfo.alterMode = "1";
                                        foreach (var item in finalNotDispenseList)
                                        {
                                            verifyReminInfo.failureInfo += "项目【"+ item.ItemName + "】未分发，请查看是否设置相应规则;";
                                        }
                                        verifyReminInfos.Add(verifyReminInfo);
                                        barCodeFormVos[i].failureInfo = Newtonsoft.Json.JsonConvert.SerializeObject(verifyReminInfos);
                                    }
                                }
                                else
                                {
                                    //站点未配置小组，返回页面提示
                                    brdv.success = false;
                                    brdv.ErrorInfo = "分发规则站点未配置小组！";
                                    ZhiFang.Common.Log.Log.Info("BLisBarCodeForm.EditDispenseSampleByBarCode.分发规则站点未配置小组！");
                                    return brdv;
                                }
                            }
                        }
                    }
                    brdv.success = true;
                    brdv.ResultDataValue = "";
                    if (barCodeFormVos != null && barCodeFormVos.Count > 0)
                    {
                        long status = long.Parse(BarCodeStatus.样本签收.Value.Code);
                        #region 查询签收人
                        List<long> barCodeFormIds = new List<long>();
                        EntityList<LisOperate> operateList = new EntityList<LisOperate>();
                        for (int i = 0; i < barCodeFormVos.Count; i++)
                        { 
                            if (barCodeFormVos[i].LisBarCodeForm.BarCodeStatusID >= status && barCodeFormVos[i].LisBarCodeForm.InceptTime != null)
                            {
                                barCodeFormIds.Add(barCodeFormVos[i].LisBarCodeForm.Id);
                            }
                        }
                        if (barCodeFormIds.Count>0)
                        {
                            operateList = IBLisOperate.SearchListByHQL("OperateObjectID in (" + string.Join(",", barCodeFormIds) + ") and OperateTypeID=" + BarCodeStatus.样本签收.Value.Id, "DataAddTime desc", -1, -1);
                            if (operateList.count > 0)
                            {
                                for (int i = 0; i < barCodeFormVos.Count; i++)
                                {
                                    var operates = operateList.list.Where(a => a.OperateObjectID == barCodeFormVos[i].LisBarCodeForm.Id);
                                    if (operates != null && operates.ToList().Count > 0)
                                    {
                                        barCodeFormVos[i].SignForMan = operateList.list[0].OperateUser;
                                    }
                                }
                            }
                        }
                        #endregion
                        EntityList<LisBarCodeFormVo> entityList = new EntityList<LisBarCodeFormVo>();
                        entityList.count = barCodeFormVos.Count;
                        entityList.list = barCodeFormVos;
                        ParseObjectProperty pop = new ParseObjectProperty(fields);
                        try
                        {
                            if (isPlanish)
                            {
                                brdv.ResultDataValue = pop.GetObjectListPlanish<LisBarCodeFormVo>(entityList);
                            }
                            else
                            {
                                brdv.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                            }
                        }
                        catch (Exception ex)
                        {
                            brdv.success = false;
                            brdv.ErrorInfo = "序列化错误：" + ex.Message;
                        }
                    }
                }
            }
            return brdv;
        }
        /// <summary>
        /// 将子项加入到项目列表
        /// </summary>
        /// <param name="itemVOs"></param>
        /// <param name="lisBarCodeForm"></param>
        /// <returns></returns>
        public List<LisBarCodeItemVo> GetLBSectionItemVOHaveChildItem(List<LisBarCodeItemVo> itemVOs, LisBarCodeForm lisBarCodeForm)
        {
            List<LisBarCodeItemVo> LBSectionItemVOs = new List<LisBarCodeItemVo>();
            List<LisBarCodeItemVo> itemVoList = new List<LisBarCodeItemVo>();//存是组合项目的样本单项目，为子项的vo赋值用
            List<long> itemIdList = new List<long>();//样本单项目
            foreach (var itemVo in itemVOs)
            {
                //是组合项目
                if (itemVo.LBSectionItem.LBItem.GroupType == 1)
                {
                    itemVoList.Add(itemVo);
                    itemIdList.Add(itemVo.LBSectionItem.LBItem.Id);
                }
                else
                {
                    LBSectionItemVOs.Add(itemVo);
                }
            }
            //查询组合项目包含的明细项目
            if (itemVoList.Count > 0)
            {
                var LBItemGroupIList = IDLBItemGroupDao.GetListByHQL("LBGroup.Id in (" + string.Join(",", itemIdList) + ")");//查找所有组合项目的子项
                //查询子项对应的sectionitem
                var childItemSeictionItemList = new List<LBSectionItem>();
                if (LBItemGroupIList != null && LBItemGroupIList.Count > 0)
                {
                    List<long> itemGroupItemIdList = new List<long>();
                    foreach (var item in LBItemGroupIList)
                    {
                        itemGroupItemIdList.Add(item.LBItem.Id);
                    }
                    var childItemSeictionItemIList = IDLBSectionItemDao.GetListByHQL("LBItem.Id in (" + string.Join(",", itemIdList) + ")");
                    if (childItemSeictionItemIList != null && childItemSeictionItemIList.Count > 0)
                    {
                        childItemSeictionItemList = childItemSeictionItemIList.ToList();
                    }
                }
                //构造子项对应的barcodeitemvo
                foreach (var itemVoItem in itemVoList)
                {
                    foreach (var itemGroupItem in LBItemGroupIList)
                    {
                        if (itemGroupItem.LBGroup.Id == itemVoItem.LBSectionItem.LBItem.Id)
                        {
                            LisBarCodeItemVo lisBarCodeItemVo1 = new LisBarCodeItemVo();
                            lisBarCodeItemVo1.LisBarCodeItem = itemVoItem.LisBarCodeItem;
                            foreach (var childItemSeictionItem in childItemSeictionItemList)
                            {
                                if (itemGroupItem.LBItem.Id == childItemSeictionItem.LBItem.Id)
                                {
                                    lisBarCodeItemVo1.LBSectionItem = childItemSeictionItem;
                                    continue;
                                }
                            }
                            LBSectionItemVOs.Add(lisBarCodeItemVo1);
                        }
                    }
                }
            }
            return LBSectionItemVOs;
        }

        /// <summary>
        /// 为项目匹配规则并找出不能分发的项目，返回分发列表
        /// </summary>
        /// <param name="LisBarCodeItemVoList"></param>
        /// <param name="finalNotDispenseList"></param>
        /// <param name="finalDispenseList"></param>
        /// <param name="LBTranRuleHostSectionList"></param>
        /// <param name="lisBarCodeForm"></param>
        /// <param name="LBTranRuleIList"></param>
        /// <param name="tranRuleTypeId"></param>
        /// <returns></returns>
        public List<LisBarCodeItemVo> ItemMachtRuleAndFindNotDispenseItem(List<LisBarCodeItemVo> LisBarCodeItemVoList, ref List<LisBarCodeItemVo> finalNotDispenseList,List<LisBarCodeItemVo> finalDispenseList, IList<LBTranRuleHostSection> LBTranRuleHostSectionList, LisBarCodeForm lisBarCodeForm, IList<LBTranRule> LBTranRuleIList, long tranRuleTypeId)
        {
            List<LisBarCodeItemVo> tempNotDispenseVoList = new List<LisBarCodeItemVo>();
            List<LisBarCodeItemVo> tempDispenseVoList = new List<LisBarCodeItemVo>();
            for (int i = 0; i < LisBarCodeItemVoList.Count; i++)
            {
                List<LBTranRuleHostSection> ItemLBTranRuleHostSectionList = new List<LBTranRuleHostSection>();//项目所匹配的分发小组
                foreach (var tempSectionItem in LisBarCodeItemVoList[i].tempSectionItemList)
                {
                    var LBTranRuleHostSection = LBTranRuleHostSectionList.Where(a => a.SectionID == tempSectionItem.LBSection.Id);
                    if (LBTranRuleHostSection != null && LBTranRuleHostSection.Count() > 0)
                    {
                        ItemLBTranRuleHostSectionList.Add(LBTranRuleHostSection.ToList().First());
                    }
                }
                //未找到项目的可分发小组，加入最终未分发列表
                if (ItemLBTranRuleHostSectionList.Count == 0)
                {
                    finalNotDispenseList.Add(LisBarCodeItemVoList[i]);
                    continue;
                }
                //分发小组排序，循环寻找规则
                ItemLBTranRuleHostSectionList = ItemLBTranRuleHostSectionList.OrderBy(a => a.DispOrder).ToList();
                bool isMatchRule = false;
                foreach (var dispenseSectionitem in ItemLBTranRuleHostSectionList)
                {
                    //如果匹配到规则则不用判断下边小组里的规则
                    if (isMatchRule)
                    {
                        break;
                    }
                    //筛选分发小组的规则
                    var sectionTranRuleIList = LBTranRuleIList.Where(a => a.SectionID == dispenseSectionitem.SectionID).OrderBy(a => a.DispOrder);
                    if (sectionTranRuleIList != null && sectionTranRuleIList.Count() > 0)
                    {
                        foreach (var TranRuleItem in sectionTranRuleIList.ToList())
                        {
                            //页面选择的规则类型id
                            if (tranRuleTypeId > 0 && tranRuleTypeId != TranRuleItem.LBTranRuleType.Id)
                            {
                                continue;
                            }
                            //当前时间处于规则的时间范围内
                            if (TranRuleItem.UseTimeMax != null && TranRuleItem.UseTimeMin != null)
                            {
                                DateTime.Now.ToLongTimeString();// 20:16:16
                                var a = (DateTime)TranRuleItem.UseTimeMax;
                                if (DateTime.Compare(Convert.ToDateTime(DateTime.Now.ToLongTimeString()), Convert.ToDateTime(((DateTime)TranRuleItem.UseTimeMax).ToLongTimeString())) > 0 || DateTime.Compare(Convert.ToDateTime(DateTime.Now.ToLongTimeString()), Convert.ToDateTime(((DateTime)TranRuleItem.UseTimeMin).ToLongTimeString())) < 0)
                                {
                                    continue;
                                }
                            }
                            //就诊类型
                            if (TranRuleItem.SickTypeID != null && (TranRuleItem.SickTypeID != lisBarCodeForm.LisPatient.SickTypeID))
                            {
                                continue;
                            }
                            //样本类型
                            if (TranRuleItem.SampleTypeID != null && (TranRuleItem.SampleTypeID != lisBarCodeForm.SampleTypeID))
                            {
                                continue;
                            }
                            //采样组
                            if (TranRuleItem.SamplingGroupID != null && (TranRuleItem.SamplingGroupID != lisBarCodeForm.SamplingGroupID))
                            {
                                continue;
                            }
                            //是否急查
                            if (!string.IsNullOrEmpty(TranRuleItem.UrgentState) && (lisBarCodeForm.IsUrgent == 0))
                            {
                                continue;
                            }
                            //在星期几分发
                            var week = (int)DateTime.Now.DayOfWeek;
                            week = week == 0 ? 7 : week;
                            if (!string.IsNullOrEmpty(TranRuleItem.TranWeek) && TranRuleItem.TranWeek != week.ToString())
                            {
                                continue;
                            }
                            //查询规则包含的项目
                            var LBTranRuleItemIList = IDLBTranRuleItemDao.GetListByHQL("LBTranRule.Id=" + TranRuleItem.Id);
                            if (LBTranRuleItemIList != null && LBTranRuleItemIList.Count > 0)
                            {
                                if (LBTranRuleItemIList.ToList().Where(a => a.LBItem.Id == LisBarCodeItemVoList[i].ItemID).Count() == 0)
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                continue;
                            }
                            //因为小组是和规则都是排好序的，只要匹配到则不用再判断下边的规则是否匹配
                            LisBarCodeItemVoList[i].DispenseSeictionDispOrder = dispenseSectionitem.DispOrder;
                            LisBarCodeItemVoList[i].LBTranRule = TranRuleItem;
                            LisBarCodeItemVoList[i].SectionId = (long)dispenseSectionitem.SectionID;//分发小组id
                            LisBarCodeItemVoList[i].LBSectionItem = LisBarCodeItemVoList[i].tempSectionItemList.Where(a => a.LBSection.Id == LisBarCodeItemVoList[i].SectionId).ToList().First();
                            tempDispenseVoList.Add(LisBarCodeItemVoList[i]);
                            isMatchRule = true;
                            break;
                        }
                    }
                }
                //如果没匹配到规则，加入临时未分发列表
                if (!isMatchRule)
                {
                    tempNotDispenseVoList.Add(LisBarCodeItemVoList[i]);
                }
            }
            var allowFollowVos = tempDispenseVoList.Where(a => a.LBTranRule.IsFollow);
            if (allowFollowVos!=null && allowFollowVos.Count()>0)
            {
                //临时分发列表是否需要更新为允许跟随规则
                foreach (var voItem in tempDispenseVoList)
                {
                    var vos = allowFollowVos.Where(a => a.SectionId == voItem.SectionId);
                    if (vos!=null && vos.Count()>0)
                    {
                        var vo = vos.OrderBy(a => a.LBTranRule.DispOrder).ToList().First();
                        if (vo.LBTranRule.DispOrder < voItem.LBTranRule.DispOrder)
                        {
                            voItem.LBTranRule = vo.LBTranRule;
                        }
                    }
                    finalDispenseList.Add(voItem);
                }
                foreach (var notDispenseVoItem in tempNotDispenseVoList)
                {
                    foreach (var sectionItem in notDispenseVoItem.tempSectionItemList)
                    {
                        var vos = allowFollowVos.Where(a => a.SectionId == sectionItem.LBSection.Id);
                        if (vos != null && vos.Count() > 0)
                        {
                            var vo = vos.OrderBy(a => a.LBTranRule.DispOrder).ToList().First();
                            notDispenseVoItem.LBTranRule= vo.LBTranRule;
                            notDispenseVoItem.SectionId = vo.SectionId;
                            notDispenseVoItem.DispenseSeictionDispOrder= vo.DispenseSeictionDispOrder;
                            finalDispenseList.Add(notDispenseVoItem);
                            break;
                        }
                    }
                    if (notDispenseVoItem.LBTranRule==null || string.IsNullOrEmpty(notDispenseVoItem.LBTranRule.CName))
                    {
                        finalNotDispenseList.Add(notDispenseVoItem);
                    }
                }
            }
            else
            {
                //临时未分发项目加入最终未分发列表
                foreach (var notDispenseVoItem in tempNotDispenseVoList)
                { 
                    finalNotDispenseList.Add(notDispenseVoItem); 
                }
            }
            return finalDispenseList;
        }

        /// <summary>
        /// 具体分发操作，返回最终未分发列表
        /// </summary>
        /// <param name="finalDispenseList"></param>
        /// <param name="finalNotDispenseList"></param>
        /// <param name="lisBarCodeForm"></param>
        /// <param name="TestDate"></param>
        /// <returns></returns>
        public List<LisBarCodeItemVo> DispenseOperation(List<LisBarCodeItemVo> finalDispenseList,List<LisBarCodeItemVo> finalNotDispenseList, ref LisBarCodeFormVo lisBarCodeFormVo, string TestDate)
        {
            if (finalDispenseList.Count > 0)
            {
                var SameSampleNoTranRules = finalDispenseList.Where(a => a.LBTranRule.SampleNoType == 2);
                LisBarCodeItemVo SameSampleNoItemVo = null;

                string sameSampleNo = "";
                LisTestForm lisTestForm1 = new LisTestForm();
                //分发时候有项目匹配到规则设置了“多个检验单取相同样本号”，则小于此规则排序生成的检验单样本号要取本规则生成的样本号
                if (SameSampleNoTranRules != null && SameSampleNoTranRules.Count() > 0)
                {
                    SameSampleNoItemVo = SameSampleNoTranRules.OrderBy(a => a.DispenseSeictionDispOrder).ToList().First();
                    var LBTranRule = SameSampleNoItemVo.LBTranRule;
                    sameSampleNo = GnerateSampleNo(ref LBTranRule, GnerateTestDate(SameSampleNoItemVo.LBTranRule, TestDate),ref lisTestForm1);
                }
                List<DispenseTagAndFlowSheetInfoVo> DispenseTagAndFlowSheetInfoVoList = new List<DispenseTagAndFlowSheetInfoVo>();//打印分发标签和流转单要的信息
                //按照规则主键id分组
                var groups = finalDispenseList.GroupBy(a => a.LBTranRule.Id);
                foreach (var group in groups)
                {
                    LisTestForm lisTestForm = new LisTestForm();
                    lisTestForm.SampleForm = lisBarCodeFormVo.LisBarCodeForm;
                    lisTestForm.LisOrderForm = lisBarCodeFormVo.LisBarCodeForm.LisOrderForm;
                    lisTestForm.LisPatient = lisBarCodeFormVo.LisBarCodeForm.LisPatient;
                    lisTestForm.LBSection = group.ToList().First().LBSectionItem.LBSection;
                    lisTestForm.MainStatusID = int.Parse(TestFormMainStatus.检验中.Key);
                    lisTestForm.StatusID = int.Parse(TestFormStatusID.样本分发.Key);
                    lisTestForm.ISource = int.Parse(TestFormSource.分发核收.Key);
                    LBTranRule lBTranRule = group.ToList().First().LBTranRule;//分发规则
                    #region 检测日期取值
                    //默认当天日期
                    lisTestForm.GTestDate = GnerateTestDate(lBTranRule, TestDate);
                    #endregion
                    #region 样本号的取值
                    if (SameSampleNoItemVo != null && (SameSampleNoItemVo.DispenseSeictionDispOrder < group.ToList().First().DispenseSeictionDispOrder || (SameSampleNoItemVo.LBTranRule.SectionID == group.ToList().First().LBTranRule.SectionID && SameSampleNoItemVo.LBTranRule.DispOrder < group.ToList().First().LBTranRule.DispOrder)) && sameSampleNo != "")
                    {
                        lisTestForm.GSampleNo = sameSampleNo;
                        lisTestForm.GSampleNoForOrder = lisTestForm1.GSampleNoForOrder;
                    }
                    else
                    {
                        //规则是否使用条码号作为样本号
                        if (lBTranRule.SampleNoType == 1)
                        {
                            lisTestForm.GSampleNo = lisBarCodeFormVo.LisBarCodeForm.BarCode;
                        }
                        else
                        {
                            lisTestForm.GSampleNo = GnerateSampleNo(ref lBTranRule,lisTestForm.GTestDate, ref lisTestForm);//按规则生成样本号
                        }
                    }
                    #endregion
                    //样本号为空说明样本号超出规则范围
                    if (string.IsNullOrEmpty(lisTestForm.GSampleNo))
                    {
                        foreach (var DispenseItem in group)
                        {
                            finalNotDispenseList.Add(DispenseItem);
                        }
                    }
                    else
                    {
                        if (IDLisTestFormDao.Save(lisTestForm))
                        {
                            List<string> itemNameList = new List<string>();//所有分发项目名称集合，供打印标签和流转单信息时使用
                            //检测项目存储到数据库
                            foreach (var DispenseItem in group)
                            {
                                LisTestItem lisTestItem = new LisTestItem();
                                lisTestItem.LisBarCodeItem = DispenseItem.LisBarCodeItem;
                                lisTestItem.LisOrderItem = DispenseItem.LisBarCodeItem.LisOrderItem;
                                lisTestItem.LisTestForm = lisTestForm;
                                lisTestItem.LBItem = DispenseItem.LBSectionItem.LBItem;//检验项目
                                lisTestItem.MainStatusID = lisTestForm.MainStatusID;
                                lisTestItem.StatusID = int.Parse(TestFormStatusID.样本分发.Key);
                                lisTestItem.ISource = int.Parse(TestFormSource.分发核收.Key);
                                //if (DispenseItem.PLBItem != null && !string.IsNullOrEmpty(DispenseItem.PLBItem.LBSectionItem.LBItem.CName))
                                //{
                                //    lisTestItem.PLBItem = DispenseItem.PLBItem.LBSectionItem.LBItem;//当前项目为子项，赋值父项信息
                                //}
                                lisTestItem.GTestDate = lisTestForm.GTestDate;//检测日期
                                IBLisTestItem.Entity = lisTestItem;
                                if (IBLisTestItem.Add())//添加数据时自动填充了DataAddTime
                                {
                                    //更新样本项目
                                    DispenseItem.LisBarCodeItem.ItemDispenseFlag = 1;
                                    DispenseItem.LisBarCodeItem.DataUpdateTime = DateTime.Now;
                                    IDLisBarCodeItemDao.Update(DispenseItem.LisBarCodeItem);
                                }
                                itemNameList.Add(lisTestItem.LBItem.CName);
                            }
                            #region 构造打印标签和流转单信息
                            DispenseTagAndFlowSheetInfoVo dispenseTagAndFlowSheetInfoVo = new DispenseTagAndFlowSheetInfoVo();
                            dispenseTagAndFlowSheetInfoVo.GSampleNo = lisTestForm.GSampleNo;
                            dispenseTagAndFlowSheetInfoVo.LBTranRule = lBTranRule;
                            dispenseTagAndFlowSheetInfoVo.ItemNames = string.Join(",", itemNameList);
                            DispenseTagAndFlowSheetInfoVoList.Add(dispenseTagAndFlowSheetInfoVo);
                            #endregion
                            //添加操作记录
                            IBLisOperate.AddLisOperate(lisTestForm, TestFormOperateType.样本分发.Value, "样本分发", SysCookieValue);
                            //更新用到的规则下一样本号
                            if (lBTranRule.IsUseNextNo)
                            {
                                lBTranRule.ResetTime = DateTime.Now;
                                lBTranRule.DataUpdateTime = DateTime.Now;
                                IDLBTranRuleDao.Update(lBTranRule);
                            }
                        }
                    }
                }
                if (DispenseTagAndFlowSheetInfoVoList.Count > 0)
                {
                    lisBarCodeFormVo.PrintDispenseTagAndFlowSheetInfo = Newtonsoft.Json.JsonConvert.SerializeObject(DispenseTagAndFlowSheetInfoVoList);
                }
            }
            return finalNotDispenseList;
        }
        /// <summary>
        /// 获取检测日期（分发到哪天检测）
        /// </summary>
        /// <param name="lBTranRule"></param>
        /// <returns></returns>
        public DateTime GnerateTestDate(LBTranRule lBTranRule,string TestDate) {
            //默认当天日期
            var now = DateTime.Now;
            DateTime GTestDate = Convert.ToDateTime(now.ToString("yyyy-MM-dd"));//转换成只有日期
            //页面指定的检测日期,格式为yyyy-MM-dd 00:00:00
            if (!string.IsNullOrEmpty(TestDate))
            {
                GTestDate = Convert.ToDateTime(TestDate);
            }
            else
            {
                //分发至星期几（数据库存储的是数字1-7）如果有多个，则分发到最近的那一天
                if (!string.IsNullOrEmpty(lBTranRule.TranToWeek))
                {
                    var nowweek = (int)now.DayOfWeek;
                    nowweek = nowweek == 0 ? 7 : nowweek;
                    var weeklist = lBTranRule.TranToWeek.Split(',');
                    var addDays = 8;
                    foreach (var weekitem in weeklist)
                    {
                        var diffDays = int.Parse(weekitem) - nowweek;
                        diffDays = diffDays > 0 ? diffDays : (diffDays + 7);
                        if (diffDays < addDays)
                        {
                            addDays = diffDays;
                        }
                    }
                    GTestDate = Convert.ToDateTime(now.AddDays(addDays).ToString("yyyy-MM-dd"));//转换成只有日期
                }
                //取规则的分发日期
                if (lBTranRule.TestDelayDates > 0)
                {
                    GTestDate = Convert.ToDateTime(now.AddDays(lBTranRule.TestDelayDates).ToString("yyyy-MM-dd"));
                }
            }
            return GTestDate;
        }
        /// <summary>
        /// 生成样本号
        /// </summary>
        /// <param name="lBTranRule"></param>
        /// <param name="testDate"></param>
        /// <returns></returns>
        public string GnerateSampleNo(ref LBTranRule lBTranRule,DateTime testDate,ref LisTestForm lisTestForm)
        {
            string sampleNo = "";
            //样本号前缀
            string strSampleNoRule = "";
            if (!string.IsNullOrEmpty(lBTranRule.SampleNoRule))
            {
                //如果有日期，日期必须用[]括起来
                if (lBTranRule.SampleNoRule.Contains("[") && lBTranRule.SampleNoRule.Contains("]") && lBTranRule.SampleNoRule.IndexOf('[')< lBTranRule.SampleNoRule.IndexOf(']'))
                {
                    //获取日期格式
                    string dateformat = lBTranRule.SampleNoRule.Substring(lBTranRule.SampleNoRule.IndexOf('[') +1, lBTranRule.SampleNoRule.IndexOf(']')- lBTranRule.SampleNoRule.IndexOf('[')- 1);
                    strSampleNoRule = lBTranRule.SampleNoRule.Substring(0, lBTranRule.SampleNoRule.IndexOf('[') + 1) + testDate.ToString(dateformat) + lBTranRule.SampleNoRule.Substring(lBTranRule.SampleNoRule.IndexOf(']'));
                    #region 日期格式
                    //switch (dateformat)
                    //{
                    //    case "yy":
                    //        
                    //        break;
                    //    case "yyyy":
                    //        break;
                    //    case "MM":
                    //        break;
                    //    case "dd":
                    //        break;
                    //    case "yyMM":
                    //        break;
                    //    case "yy-MM":
                    //        break;
                    //    case "yy/MM":
                    //        break;
                    //    case "MMdd":
                    //        break;
                    //    case "MM-dd":
                    //        break;
                    //    case "MM/dd":
                    //        break;
                    //    case "yyMMdd":
                    //        break;
                    //    case "yy-MM-dd":
                    //        break;
                    //    case "yy/MM/dd":
                    //        break;
                    //    case "yyyyMMdd":
                    //        break;
                    //    case "yyyy-MM-dd":
                    //        break;
                    //    case "yyyy/MM/dd":
                    //        break;

                    //    default:
                    //        break;
                    //}
                    #endregion
                }
                else
                {
                    strSampleNoRule = lBTranRule.SampleNoRule;
                }
            }
            //启用下一样本号，下一样本号字段不为空，再判断存储的 样本号能不能用
            if (lBTranRule.IsUseNextNo && !string.IsNullOrEmpty(lBTranRule.NextSampleNo))
            {
                //样本号复位
                if (!string.IsNullOrEmpty(lBTranRule.ResetType))
                {
                    if (lBTranRule.ResetTime!=null)//复位时间，使用本规则时间
                    {
                        var firstday = DateTime.Now;
                        switch (lBTranRule.ResetType)
                        {
                            case "1"://日
                                break;
                            case "2"://月
                                firstday = DateTime.Now.AddDays(1 - DateTime.Now.Day);//本月第一天
                                break;
                            case "3"://周
                                DateTime nowTime = DateTime.Now;
                                //星期一为第一天  
                                int weeknow = Convert.ToInt32(nowTime.DayOfWeek);
                                //因为是以星期一为第一天，所以要判断weeknow等于0时，要向前推6天。  
                                weeknow = (weeknow == 0 ? (7 - 1) : (weeknow - 1));
                                int daydiff = (-1) * weeknow;
                                //本周第一天  
                                firstday = nowTime.AddDays(daydiff);
                                break;
                            case "4"://季度
                                firstday = DateTime.Now.AddMonths(0 - (DateTime.Now.Month - 1) % 3);
                                break;
                            case "5"://年
                                firstday = DateTime.Now.AddMonths(1 - DateTime.Now.Month).AddDays(1 - DateTime.Now.Day);
                                break;
                            default:
                                break;
                        }
                        if (firstday.Subtract((DateTime)lBTranRule.ResetTime).Days > 0)
                        {
                            lBTranRule.NextSampleNo = "";
                        }
                    }
                }
                if (!string.IsNullOrEmpty(lBTranRule.NextSampleNo))
                {
                    //判断下一样本号是否可用
                    long NextSampleNoNum = long.Parse(lBTranRule.NextSampleNo);
                    if (NextSampleNoNum >= long.Parse(lBTranRule.SampleNoMin) && NextSampleNoNum <= long.Parse(lBTranRule.SampleNoMax))
                    {
                        var testFormList = IDLisTestFormDao.GetListByHQL("MainStatusID>=0 and LBSection.Id=" + lBTranRule.SectionID + " and GTestDate='" + testDate + "' and GSampleNo='" + strSampleNoRule+lBTranRule.NextSampleNo + "'");
                        if (testFormList==null || testFormList.Count == 0)
                        {
                            sampleNo = strSampleNoRule+lBTranRule.NextSampleNo;
                            lisTestForm.GSampleNoForOrder = LisCommonMethod.DisposeSampleNo(lBTranRule.NextSampleNo);
                            lBTranRule.NextSampleNo = (long.Parse(lBTranRule.NextSampleNo) + 1).ToString();
                            return sampleNo;
                        }
                    }
                }
            }
            //新生成样本号
            string getTestFormWhere = "MainStatusID>=0 and LBSection.Id=" + lBTranRule.SectionID + " and GTestDate='" + testDate + "'";
            if (!string.IsNullOrEmpty(strSampleNoRule))
            {
                getTestFormWhere += " and GSampleNo like '" + strSampleNoRule + "%'";
            }
            var testforms= IDLisTestFormDao.GetListByHQL(getTestFormWhere);
            if (testforms!=null && testforms.Count>0)
            {
                long GSampleNoNumValue=long.Parse(testforms.OrderByDescending(a=>a.GSampleNoForOrder).ToList().First().GSampleNo.Replace(strSampleNoRule, ""));
                GSampleNoNumValue++;
                if (GSampleNoNumValue >= long.Parse(lBTranRule.SampleNoMin) && GSampleNoNumValue <= long.Parse(lBTranRule.SampleNoMax))
                {
                    sampleNo = strSampleNoRule + LisCommonMethod.DisposeSampleNo(GSampleNoNumValue.ToString(), '-', '0', lBTranRule.SampleNoMax.Length);
                    lisTestForm.GSampleNoForOrder = LisCommonMethod.DisposeSampleNo(GSampleNoNumValue.ToString());
                }
            }
            else
            {
                //未查询到说明没使用，从最小号取
                sampleNo = strSampleNoRule + LisCommonMethod.DisposeSampleNo(lBTranRule.SampleNoMin, '-', '0', lBTranRule.SampleNoMax.Length);
                lisTestForm.GSampleNoForOrder = LisCommonMethod.DisposeSampleNo(lBTranRule.SampleNoMin);
            }
            //更新下一样本号
            if (lBTranRule.IsUseNextNo)
            {
                lBTranRule.NextSampleNo = LisCommonMethod.DisposeSampleNo((long.Parse(sampleNo.Replace(strSampleNoRule, "")) + 1).ToString(), '-', '0', lBTranRule.SampleNoMax.Length);
            }
            return sampleNo;
        }
        
        /// <summary>
        /// 分发取消
        /// </summary>
        /// <param name="nodetypeID"></param>
        /// <param name="barCodeFormIds"></param>
        /// <param name="fields"></param>
        /// <param name="isPlanish"></param>
        /// <returns></returns>
        public BaseResultDataValue EditSampleDispenseCancelByBarCodeFormId(long nodetypeID, string barCodeFormIds, string fields, bool isPlanish) {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (string.IsNullOrEmpty(barCodeFormIds))
            {
                brdv.success = false;
                brdv.ErrorInfo = "样本Id不能为空！";
                return brdv;
            }
            
            var list=this.SearchListByHQL("Id in ("+barCodeFormIds+")");
            //找出已分发的样本单
            List<long> cancelIdsList = new List<long>();
            long dispensestatus = long.Parse(BarCodeStatus.样本分发.Value.Code);
            foreach (var item in list)
            {
                if (item.BarCodeStatusID== dispensestatus)
                {
                    cancelIdsList.Add(item.Id);
                }
            }
            var barCodeFormVos = ConvertLisBarCodeFormVo(list.ToList());
            if (cancelIdsList.Count>0)
            {
                var barCodeItemList = IDLisBarCodeItemDao.GetListByHQL("ItemDispenseFlag=1 and LisBarCodeForm.Id in (" + string.Join(",", cancelIdsList) + ")");
                var testFormList = IDLisTestFormDao.GetListByHQL("MainStatusID>=0 and SampleForm.Id in (" + string.Join(",", cancelIdsList) + ")");
                foreach (var testForm in testFormList)
                {
                    testForm.MainStatusID = int.Parse(TestFormMainStatus.删除作废.Key);
                    var testItemList = IBLisTestItem.SearchListByHQL("MainStatusID>=0 and LisTestForm.Id=" + testForm.Id);
                    if (testItemList != null && testItemList.Count > 0)
                    {
                        foreach (var testItem in testItemList)
                        {
                            testItem.MainStatusID = int.Parse(TestItemMainStatus.删除作废.Key);
                            IBLisTestItem.Entity = testItem;
                            IBLisTestItem.Edit();
                        }
                    }
                    if (IDLisTestFormDao.Save(testForm))
                    {
                        IBLisOperate.AddLisOperate(testForm, TestFormOperateType.删除.Value, "检验单删除", SysCookieValue);
                    }
                    else
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = "取消分发失败";
                        return brdv;
                    }
                }
                //更新样本单项目
                foreach (var item in barCodeItemList)
                {
                    item.ItemDispenseFlag = 0;
                    item.DataUpdateTime = DateTime.Now;
                }
                //增加操作记录
                for (int i = 0; i < barCodeFormVos.Count(); i++)
                {
                    //状态改为分发前状态
                    #region 分发前的状态
                    if (barCodeFormVos[i].LisBarCodeForm.InceptTime != null)
                    {
                        barCodeFormVos[i].LisBarCodeForm.BarCodeStatusID = long.Parse(BarCodeStatus.样本签收.Value.Code);
                        barCodeFormVos[i].LisBarCodeForm.BarCodeCurrentStatus = BarCodeStatus.样本签收.Value.Name;
                    }
                    else if (barCodeFormVos[i].LisBarCodeForm.ArriveTime != null)
                    {
                        barCodeFormVos[i].LisBarCodeForm.BarCodeStatusID = long.Parse(BarCodeStatus.样本送达.Value.Code);
                        barCodeFormVos[i].LisBarCodeForm.BarCodeCurrentStatus = BarCodeStatus.样本送达.Value.Name;
                    }
                    else if (barCodeFormVos[i].LisBarCodeForm.SendTime != null)
                    {
                        barCodeFormVos[i].LisBarCodeForm.BarCodeStatusID = long.Parse(BarCodeStatus.样本送检.Value.Code);
                        barCodeFormVos[i].LisBarCodeForm.BarCodeCurrentStatus = BarCodeStatus.样本送检.Value.Name;
                    }
                    else if (barCodeFormVos[i].LisBarCodeForm.SendTime != null)
                    {
                        barCodeFormVos[i].LisBarCodeForm.BarCodeStatusID = long.Parse(BarCodeStatus.样本送检.Value.Code);
                        barCodeFormVos[i].LisBarCodeForm.BarCodeCurrentStatus = BarCodeStatus.样本送检.Value.Name;
                    }
                    else if (barCodeFormVos[i].LisBarCodeForm.CollectTime != null)
                    {
                        barCodeFormVos[i].LisBarCodeForm.BarCodeStatusID = long.Parse(BarCodeStatus.样本采集.Value.Code);
                        barCodeFormVos[i].LisBarCodeForm.BarCodeCurrentStatus = BarCodeStatus.样本采集.Value.Name;
                    }
                    else if (barCodeFormVos[i].LisBarCodeForm.PrintTime != null)
                    {
                        barCodeFormVos[i].LisBarCodeForm.BarCodeStatusID = long.Parse(BarCodeStatus.条码打印.Value.Code);
                        barCodeFormVos[i].LisBarCodeForm.BarCodeCurrentStatus = BarCodeStatus.条码打印.Value.Name;
                    }
                    else if (barCodeFormVos[i].LisBarCodeForm.AffirmTime != null)
                    {
                        barCodeFormVos[i].LisBarCodeForm.BarCodeStatusID = long.Parse(BarCodeStatus.条码确认.Value.Code);
                        barCodeFormVos[i].LisBarCodeForm.BarCodeCurrentStatus = BarCodeStatus.条码确认.Value.Name;
                    }
                    else
                    {
                        barCodeFormVos[i].LisBarCodeForm.BarCodeStatusID = long.Parse(BarCodeStatus.生成条码.Value.Code);
                        barCodeFormVos[i].LisBarCodeForm.BarCodeCurrentStatus = BarCodeStatus.生成条码.Value.Name;
                    }
                    barCodeFormVos[i].LisBarCodeForm.DataUpdateTime = DateTime.Now;
                    barCodeFormVos[i].LisBarCodeForm.TranTime = null;

                    #endregion
                    barCodeFormVos[i].LisBarCodeForm.DispenseFlag = 0;//分发标志
                    this.Entity = barCodeFormVos[i].LisBarCodeForm;
                    if (this.Edit())
                    {
                        //增加取消分发操作记录
                        IBLisOperate.AddLisOperate(barCodeFormVos[i].LisBarCodeForm, OrderFromOperateType.取消分发.Value, "取消分发", SysCookieValue);
                    }
                    else
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = "取消分发失败";
                        return brdv;
                    }
                }
            }
            
            if (barCodeFormVos != null && barCodeFormVos.Count > 0)
            {
                long status = long.Parse(BarCodeStatus.样本签收.Value.Code);
                #region 查询签收人
                List<long> barCodeFormIdList = new List<long>();
                EntityList<LisOperate> operateList = new EntityList<LisOperate>();
                for (int i = 0; i < barCodeFormVos.Count; i++)
                {
                    if (barCodeFormVos[i].LisBarCodeForm.BarCodeStatusID == status && barCodeFormVos[i].LisBarCodeForm.InceptTime != null)
                    {
                        barCodeFormIdList.Add(barCodeFormVos[i].LisBarCodeForm.Id);
                        operateList = IBLisOperate.SearchListByHQL("OperateObjectID in (" + string.Join(",", barCodeFormIds) + ") and OperateTypeID=" + BarCodeStatus.样本签收.Value.Id, "DataAddTime desc", -1, -1);
                    }
                }
                if (operateList.count > 0)
                {
                    for (int i = 0; i < barCodeFormVos.Count; i++)
                    {
                        var operates = operateList.list.Where(a => a.OperateObjectID == barCodeFormVos[i].LisBarCodeForm.Id);
                        if (operates != null && operates.ToList().Count > 0)
                        {
                            barCodeFormVos[i].SignForMan = operateList.list[0].OperateUser;
                        }
                    }
                }
                #endregion
                EntityList<LisBarCodeFormVo> entityList = new EntityList<LisBarCodeFormVo>();
                entityList.count = barCodeFormVos.Count;
                entityList.list = barCodeFormVos;
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        brdv.ResultDataValue = pop.GetObjectListPlanish<LisBarCodeFormVo>(entityList);
                    }
                    else
                    {
                        brdv.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "序列化错误：" + ex.Message;
                }
            }
            return brdv;
        }

        public BaseResultDataValue PrintDispenseTagByBarCode(long nodetypeID, string barCodes) {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (string.IsNullOrEmpty(barCodes))
            {
                brdv.success = false;
                brdv.ErrorInfo = "样本号不能为空！";
                return brdv;
            }
            string[] strbarCodes = barCodes.Split(',');
            var list = this.SearchListByHQL("BarCode in ('" + string.Join("','", strbarCodes) + ")");
            List<long> barCodeFormIdList = new List<long>();
            List<long> testFormIdList = new List<long>();
            foreach (var barCodeForm in list)
            {
                barCodeFormIdList.Add(barCodeForm.Id);
            }
            var barCodeFormVos = ConvertLisBarCodeFormVo(list.ToList());
            var testFormList = IDLisTestFormDao.GetListByHQL("MainStatusID>=0 and LisBarCodeForm.Id in (" + string.Join(",",barCodeFormIdList) + ")");
            foreach (var testForm in testFormList)
            {
                testFormIdList.Add(testForm.Id);
            }
            var testItemList = IBLisTestItem.SearchListByHQL("MainStatusID>=0 and LisTestForm.Id in (" + string.Join(",",testFormIdList) + ")");

            return brdv;
        }
        public List<LisBarCodeFormVo> GetTestInfoByBarCode(List<LisBarCodeFormVo> LisBarCodeFormVoList)
        {
            List<long> barCodeFormIdList = new List<long>();
            List<long> testFormIdList = new List<long>();
            foreach (var barCodeForm in LisBarCodeFormVoList)
            {
                barCodeFormIdList.Add(barCodeForm.LisBarCodeForm.Id);
            }
            var testFormList = IDLisTestFormDao.GetListByHQL("MainStatusID>=0 and SampleForm.Id in (" + string.Join(",", barCodeFormIdList) + ")");
            if (testFormList!=null && testFormList.Count>0)
            {
                foreach (var testForm in testFormList)
                {
                    testFormIdList.Add(testForm.Id);
                }
                var testItemList = IBLisTestItem.SearchListByHQL("MainStatusID>=0 and LisTestForm.Id in (" + string.Join(",", testFormIdList) + ")");
                for (int i = 0; i < LisBarCodeFormVoList.Count; i++)
                {
                    List<DispenseTagAndFlowSheetInfoVo> list = new List<DispenseTagAndFlowSheetInfoVo>();
                    var tempTestFormList = testFormList.Where(a => a.SampleForm.Id == LisBarCodeFormVoList[i].LisBarCodeForm.Id);
                    if (tempTestFormList != null && tempTestFormList.ToList().Count > 0)
                    {
                        foreach (var tempTestForm in tempTestFormList.ToList())
                        {
                            DispenseTagAndFlowSheetInfoVo dispenseTagAndFlowSheetInfoVo = new DispenseTagAndFlowSheetInfoVo();
                            dispenseTagAndFlowSheetInfoVo.GSampleNo = tempTestForm.GSampleNo;
                            List<string> itemNameList = new List<string>();
                            foreach (var item in testItemList.Where(a => a.LisTestForm.Id == tempTestForm.Id).ToList())
                            {
                                itemNameList.Add(item.LBItem.CName);
                            }
                            dispenseTagAndFlowSheetInfoVo.ItemNames = string.Join(",", itemNameList);
                            list.Add(dispenseTagAndFlowSheetInfoVo);
                        }
                    }
                    if (list.Count > 0)
                    {
                        //EntityList<DispenseTagAndFlowSheetInfoVo> entityList = new EntityList<DispenseTagAndFlowSheetInfoVo>();
                        //entityList.list = list;
                        //entityList.count = list.Count;
                        //string fields = "DispenseTagAndFlowSheetInfoVo_LisTestForm_GSampleNo";
                        //ParseObjectProperty pop = new ParseObjectProperty(fields);
                        //LisBarCodeFormVoList[i].PrintDispenseTagAndFlowSheetInfo = pop.GetObjectListPlanish<DispenseTagAndFlowSheetInfoVo>(entityList);
                        LisBarCodeFormVoList[i].PrintDispenseTagAndFlowSheetInfo = Newtonsoft.Json.JsonConvert.SerializeObject(list);
                    }
                }
            }
            return LisBarCodeFormVoList;
        }


        #endregion
    }
}