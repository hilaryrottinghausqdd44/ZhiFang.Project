using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.Entity.LabStar.ViewObject.Response;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.LabStar;
using ZhiFang.IDAO.LabStar;
using ZhiFang.LabStar.Common;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.BLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public class BLisOrderForm : BaseBLL<LisOrderForm>, ZhiFang.IBLL.LabStar.IBLisOrderForm
    {
        #region
        public SysCookieValue SysCookieValue { get; set; }
        IDLisPatientDao IDLisPatientDao { get; set; }
        IDLisOrderItemDao IDLisOrderItemDao { get; set; }
        IDLBItemDao IDLBItemDao { get; set; }
        IBLisOperate IBLisOperate { get; set; }
        IDLBTGetMaxNoDao IDLBTGetMaxNoDao { get; set; }
        IBBPara IBBPara { get; set; }
        IBBParaItem IBBParaItem { get; set; }
        IDLBParItemSplitDao IDLBParItemSplitDao { get; set; }
        IDLBSamplingItemDao IDLBSamplingItemDao { get; set; }
        IBLisBarCodeForm IBLisBarCodeForm { get; set; }
        IBLisBarCodeItem IBLisBarCodeItem { get; set; }
        IDLBDestinationDao IDLBDestinationDao { get; set; }
        IDLBSampleTypeDao IDLBSampleTypeDao { get; set; }
        IDLBSamplingGroupDao IDLBSamplingGroupDao { get; set; }
        IDLBTcuveteDao IDLBTcuveteDao { get; set; }
        IDLBSickTypeDao IDLBSickTypeDao { get; set; }
        IBLBDicCodeLink IBLBDicCodeLink { get; set; }
        #endregion
        #region 医嘱开单
        public bool EditOrder(string[] lisPatient, string[] lisOrderForm, IList<LisOrderItem> lisOrderItems, LisOrderForm lisOrderFormentity, string userid, string username)
        {
            bool flag = true;
            bool patient = true, form = true;
            List<bool> items = new List<bool>();
            if (lisOrderItems.Count > 0)
            {
                int itemnum = IDLisOrderItemDao.GetListCountByHQL("OrderFormID=" + lisOrderFormentity.Id);
                int delnum = 0;
                if (itemnum > 0) {
                    delnum = IDLisOrderItemDao.DeleteByHql(" From LisOrderItem lisorderitem where lisorderitem.OrderFormID=" + lisOrderFormentity.Id);
                }
                if (itemnum == 0 || (itemnum > 0 && itemnum == delnum))
                {
                    foreach (var item in lisOrderItems)
                    {
                        item.OrderFormID = lisOrderFormentity.Id;
                        item.OrderFormNo = lisOrderFormentity.OrderFormNo;
                        item.PartitionDate = DateTime.Now;
                        items.Add(IDLisOrderItemDao.Save(item));
                    }
                }
            }
            //医嘱单信息
            if (lisOrderForm != null)
            {
                form = DBDao.Update(lisOrderForm);
            }
            //病人信息
            if (lisPatient != null)
            {
                patient = IDLisPatientDao.Update(lisPatient);
            }
            if (patient & form & items.Count(a => a == false) == 0)
            {
                IBLisOperate.AddLisOperate(lisOrderFormentity, OrderFromOperateType.修改医嘱单.Value, "修改医嘱单", SysCookieValue);
                flag = true;
            }
            return flag;
        }
        /// <summary>
        /// 保存医嘱单
        /// </summary>
        /// <param name="lisPatient"></param>
        /// <param name="lisOrderForm"></param>
        /// <param name="lisOrderItems"></param>
        /// <returns></returns>
        public BaseResultDataValue Edit_AddOrder(LisPatient lisPatient, LisOrderForm lisOrderForm, IList<LisOrderItem> lisOrderItems, string username, string userid)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue() { success = false };
            bool patient = false, form = false;
            List<bool> items = new List<bool>();
            if ((lisPatient.Birthday != null && !string.IsNullOrEmpty(lisPatient.Birthday.ToString())) && (lisPatient.Age == null || lisPatient.Age == 0)) {
                string age = LisCommonMethod.GetAge(lisPatient.Birthday.ToString(), DateTime.Now.ToString());
                JObject jo = JObject.Parse(age);
                foreach (JProperty item in jo.Children())
                {
                    if (item.Name == "Age")
                    {
                        lisPatient.Age = double.Parse(item.Value.ToString());
                    }
                    else if (item.Name == "AgeUnitID")
                    {
                        lisPatient.AgeUnitID = long.Parse(item.Value.ToString());
                    }
                    else if (item.Name == "AgeUnitName")
                    {
                        lisPatient.AgeUnitName = item.Value.ToString();
                    }
                    else if (item.Name == "AgeDesc")
                    {
                        lisPatient.AgeDesc = item.Value.ToString();
                    }
                }
            }
            //保存患者信息
            patient = IDLisPatientDao.Save(lisPatient);
            if (patient)
            {
                #region 医嘱单号 当前日期加上五位流水号 00001开始
                IList<LBTGetMaxNo> lBTGetMaxNos = IDLBTGetMaxNoDao.GetListByHQL("BmsTypeID=" + LBTGetMaxNOBmsTypes.医嘱单号流水号.Key);
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
                    else {
                        string maxnum = lBTGetMaxNo.MaxID;
                        lBTGetMaxNo.MaxID = (long.Parse(maxnum) + 1).ToString();
                        lBTGetMaxNo.DataUpdateTime = DateTime.Now;
                        IDLBTGetMaxNoDao.Update(lBTGetMaxNo);
                        MaxID = lBTGetMaxNo.MaxID;
                    }
                }
                else {
                    LBTGetMaxNo lBTGetMaxNo = new LBTGetMaxNo();
                    lBTGetMaxNo.BmsDate = DateTime.Now;
                    lBTGetMaxNo.BmsTypeID = long.Parse(LBTGetMaxNOBmsTypes.医嘱单号流水号.Key);
                    lBTGetMaxNo.BmsType = LBTGetMaxNOBmsTypes.医嘱单号流水号.Value.Code;
                    lBTGetMaxNo.MaxID = "0";
                    lBTGetMaxNo.DataAddTime = DateTime.Now;
                    IDLBTGetMaxNoDao.Save(lBTGetMaxNo);
                    MaxID = lBTGetMaxNo.MaxID;
                }
                #endregion
                //IList<LBTGetMaxNo> maxid = IDLBTGetMaxNoDao.GetListByHQL("BmsTypeID=" + LBTGetMaxNOBmsTypes.医嘱单号流水号.Key);
                lisOrderForm.OrderFormNo = day + MaxID.PadLeft(5, '0');
                lisOrderForm.OperatorID = long.Parse(userid);
                lisOrderForm.OperatorName = username;
                //保存医嘱单
                lisOrderForm.PatID = lisPatient.Id;
                lisOrderForm.PartitionDate = DateTime.Now;
                lisOrderForm.OrderTime = DateTime.Now;
                form = DBDao.Save(lisOrderForm);
                if (form)
                {
                    //保存医嘱项目
                    foreach (var item in lisOrderItems)
                    {
                        item.OrderFormID = lisOrderForm.Id;
                        item.OrderFormNo = lisOrderForm.OrderFormNo;
                        item.OrderDate = DateTime.Now;
                        item.PartitionDate = DateTime.Now;
                        items.Add(IDLisOrderItemDao.Save(item));
                    }
                    if (items.Count(a => a == false) > 0)
                    {
                        ZhiFang.Common.Log.Log.Debug("BLisOrderForm._AddOrder方法 保存医嘱单项目存在失败！");
                        foreach (var item in lisOrderItems)
                        {
                            IDLisOrderItemDao.Delete(item);
                        }
                        DBDao.Delete(lisOrderForm);
                        IDLisPatientDao.Delete(lisPatient);
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug("BLisOrderForm._AddOrder方法 保存医嘱单失败！");
                    DBDao.Delete(lisOrderForm);
                    IDLisPatientDao.Delete(lisPatient);
                }

            }
            else
            {
                ZhiFang.Common.Log.Log.Debug("BLisOrderForm._AddOrder方法 保存医嘱单病人信息失败！");
                IDLisPatientDao.Delete(lisPatient);
            }
            if (patient & form & items.Count(a => a == false) == 0)
            {
                IBLisOperate.AddLisOperate(lisOrderForm, OrderFromOperateType.保存医嘱单.Value, "保存医嘱单", SysCookieValue);
                baseResultDataValue.success = true;
                baseResultDataValue.ResultDataValue = lisOrderForm.Id.ToString();
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue GetOrderList(string hisDeptNo, string patno, string sickTypeNo, string strWhere)
        {
            BaseResultDataValue brdv = new BaseResultDataValue() { success = false };
            if (string.IsNullOrEmpty(patno))
            {
                brdv.ErrorInfo = "请传入病历号(patno)";
                brdv.success = false;
                return brdv;
            }
            if (string.IsNullOrEmpty(sickTypeNo))
            {
                brdv.ErrorInfo = "请传入就诊类型(sickTypeNo)";
                brdv.success = false;
                return brdv;
            }
            List<LisOrderFormVo> lisOrderFormVos = (this.DBDao as IDLisOrderFormDao).GetOrderList(hisDeptNo, patno, sickTypeNo, strWhere);
            EntityList<LisOrderFormVo> entityList = new EntityList<LisOrderFormVo>();
            if (lisOrderFormVos != null)
            {
                entityList.count = lisOrderFormVos.Count();
                entityList.list = lisOrderFormVos;
                brdv.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(entityList);
            }
            else
            {
                entityList.count = 0;
                entityList.list = new List<LisOrderFormVo>();
                brdv.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(entityList);
            }
            brdv.success = true;
            return brdv;
        }

        public BaseResultDataValue UpdateOrder(string orderFormNo, long userid, string username)
        {
            BaseResultDataValue brdv = new BaseResultDataValue() { success = false };
            IList<LisOrderForm> lisOrderForms = DBDao.GetListByHQL("OrderFormNo = '" + orderFormNo + "'");
            if (lisOrderForms.Count > 0)
            {
                LisOrderForm lisOrderForm = lisOrderForms[0];
                if (lisOrderForm.IsAffirm != 1)
                {
                    IList<LisOrderItem> lisOrderItems = IDLisOrderItemDao.GetListByHQL("OrderFormNo = '" + orderFormNo + "'");
                    if (lisOrderItems.Count > 0)
                    {
                        lisOrderForm.CheckerID = userid;
                        lisOrderForm.CheckerName = username;
                        //string fields = "Id,IsAffirm,DataUpdateTime,DataTimeStamp";
                        lisOrderForm.IsAffirm = 1;
                        //string[] lisOrderFormFieldsarry = ZhiFang.ServiceCommon.RBAC.CommonServiceMethod.GetUpdateFieldValueStr(lisOrderForm, fields);
                        bool isok = DBDao.Update(lisOrderForm);
                        brdv.success = isok;
                        if (!isok)
                        {
                            brdv.ErrorInfo = "审核失败！";
                        }
                        else
                        {
                            IBLisOperate.AddLisOperate(lisOrderForm, OrderFromOperateType.审核医嘱单.Value, "审核医嘱单", SysCookieValue);
                        }
                    }
                    else
                    {
                        brdv.ErrorInfo = "未找到此医嘱单项目!";
                    }
                }
                else
                {
                    brdv.ErrorInfo = "此医嘱单已审核，不允许重复审核！";
                }
            }
            else
            {
                brdv.ErrorInfo = "未查询到医嘱单！";
            }
            return brdv;
        }

        public BaseResultDataValue CancelOrder(string orderFormNo, long userid, string username)
        {
            BaseResultDataValue brdv = new BaseResultDataValue() { success = false };
            IList<LisOrderForm> lisOrderForms = DBDao.GetListByHQL("OrderFormNo = '" + orderFormNo + "'");
            if (lisOrderForms.Count > 0)
            {
                LisOrderForm lisOrderForm = lisOrderForms[0];
                if (lisOrderForm.IsAffirm == 1 && lisOrderForm.OrderExecFlag != 1)
                {
                    //string fields = "Id,IsAffirm,DataUpdateTime,DataTimeStamp";
                    lisOrderForm.IsAffirm = 0;
                    //string[] lisOrderFormFieldsarry = ZhiFang.ServiceCommon.RBAC.CommonServiceMethod.GetUpdateFieldValueStr(lisOrderForm, fields);
                    bool isok = DBDao.Update(lisOrderForm);
                    brdv.success = isok;
                    if (!isok)
                    {
                        brdv.ErrorInfo = "取消审核失败！";
                    }
                    else
                    {
                        IBLisOperate.AddLisOperate(lisOrderForm, OrderFromOperateType.取消审核医嘱单.Value, "取消审核医嘱单", SysCookieValue);
                    }
                }
                else
                {
                    brdv.ErrorInfo = "此医嘱单未审核或已执行，不允许取消审核操作！";
                }
            }
            else
            {
                brdv.ErrorInfo = "未查询到医嘱单！";
            }
            return brdv;
        }

        public BaseResultDataValue DeleteOrder(string orderFormNo, long userid, string username)
        {
            BaseResultDataValue brdv = new BaseResultDataValue() { success = false };
            IList<LisOrderForm> lisOrderForms = DBDao.GetListByHQL("OrderFormNo = '" + orderFormNo + "'");
            if (lisOrderForms.Count > 0)
            {
                LisOrderForm lisOrderForm = lisOrderForms[0];
                if (lisOrderForm.IsAffirm != 1 && lisOrderForm.OrderExecFlag != 1)
                {
                    int orderitem = IDLisOrderItemDao.GetListCountByHQL("OrderFormID=" + lisOrderForm.Id);
                    int deloi = 0;
                    if (orderitem > 0) {
                        deloi = IDLisOrderItemDao.DeleteByHql(" From LisOrderItem lisorderitem where lisorderitem.OrderFormID=" + lisOrderForm.Id);
                    }
                    if ((orderitem == 0 && deloi == 0) || (orderitem > 0 && deloi > 0))
                    {
                        bool delform = DBDao.Delete(lisOrderForm.Id);
                        if (!delform)
                        {
                            brdv.ErrorInfo = "删除失败！";
                        }
                        else
                        {
                            bool delpat = IDLisPatientDao.DeleteByHQL(long.Parse(lisOrderForm.PatID.ToString()));
                            if (delpat)
                            {
                                brdv.success = delpat;
                                IBLisOperate.AddLisOperate(lisOrderForm, OrderFromOperateType.删除医嘱单.Value, "删除医嘱单", SysCookieValue);
                            }
                            else {
                                brdv.ErrorInfo = "删除失败！";
                            }
                        }
                    }
                    else {
                        brdv.ErrorInfo = "删除失败！";
                    }
                }
                else
                {
                    brdv.ErrorInfo = "此医嘱单已审核或已执行，不允许删除操作！";
                }
            }
            else
            {
                brdv.ErrorInfo = "未查询到医嘱单！";
            }
            return brdv;
        }
        #endregion
        #region 样本条码
        public BaseResultDataValue GetHISCheckData(long nodetype, string receiveType, string value, int days, long userid, string username, string labid, int nextindex)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            ZhiFang.Common.Log.Log.Debug("LabStarPreService.HISGetSampleAndGrouping.获取HIS医嘱并采样分管开始！");
            ZhiFang.Common.Log.Log.Debug("LabStarPreService.HISGetSampleAndGrouping.传入参数(nodetype：+" + nodetype + ",receiveType:" + receiveType + ",value=" + value + ",days=" + days + ",nextindex=" + nextindex + ")");
            #region 入参验证与获取配置参数
            if (string.IsNullOrEmpty(receiveType) || string.IsNullOrEmpty(value))
            {
                brdv.success = false;
                brdv.ErrorInfo = "核收条件不可为空！";
                return brdv;
            }
            if (days <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "样本过滤天数必须大于0！";
                return brdv;
            }
            //获取参数配置
            List<BPara> bParas = IBBParaItem.SelPreBParas(nodetype, "", Pre_AllModules.样本条码.Value.DefaultValue, userid.ToString(), username);
            #region 验卡
            var checkcardurl = bParas.Where(a => a.ParaNo == "Pre_OrderBarCode_DefaultPara_0027").First();
            if (!string.IsNullOrEmpty(checkcardurl.ParaValue))
            {
                value = CheckCard(nodetype, receiveType, value).ResultDataValue;
            }
            #endregion
            #endregion
            #region 调用HIS接口查询HIS医嘱信息并存储到本地
            var iscallhis = bParas.Where(a => a.ParaNo == "Pre_OrderBarCode_DefaultPara_0059").First();//是否调用HIS接口
            if (iscallhis.ParaValue == "1")
            {
                var sicktypenos = bParas.Where(a => a.ParaNo == "Pre_OrderBarCode_DefaultPara_0014").ToList();//就诊类型
                var callhisurl = bParas.Where(a => a.ParaNo == "Pre_OrderBarCode_DefaultPara_0060").ToList();//核收接口路径
                var showToolTip = bParas.Where(a => a.ParaNo == "Pre_OrderBarCode_DefaultPara_0087").ToList();//HIS核收失败是否提示
                if (!string.IsNullOrEmpty(callhisurl[0].ParaValue))
                {
                    string sicktypename = "";
                    if (!string.IsNullOrEmpty(sicktypenos[0].ParaValue))
                    {
                        var lBSickTypes = IDLBSickTypeDao.GetListByHQL("Id in (" + sicktypenos[0].ParaValue + ")");
                        foreach (var lBSickType in lBSickTypes)
                        {
                            if (sicktypename == "")
                            {
                                sicktypename = lBSickType.CName;
                            }
                            else
                            {
                                sicktypename += "," + lBSickType.CName;
                            }
                        }
                    }
                    var day = days == 0 ? 7 : days;
                    BaseResultDataValue hIsresult = this.GetHISCheckDataAndWriteLocalDB(receiveType.Split('.')[1], value, DateTime.Now.AddDays(-day).ToString(), DateTime.Now.ToString(), sicktypename, "", "", callhisurl[0].ParaValue, userid,username);
                    if (!hIsresult.success && showToolTip.First().ParaValue == "1")
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = hIsresult.ErrorInfo;
                        return brdv;
                    }
                }
            }
            #endregion
            return brdv;
        }
        /// <summary>
        /// HIS获取医嘱并采样分管
        /// </summary>
        /// <param name="nodetype">站点类型</param>
        /// <param name="receiveType">核收条件字段</param>
        /// <param name="value">核收条件值</param>
        /// <param name="days">样本过滤天数</param>
        /// <param name="nextindex">测试使用</param>
        /// <returns></returns>
        public BaseResultDataValue HISGetSampleAndGrouping(long nodetype, string receiveType, string value, int days, long userid, string username,string labid,  int nextindex, out List<BPara> paralist, out List<List<GroupingOrderItemVo>> goivlist, out List<List<LisOrderFormItemVo>> lisoftvolist, out List<LBSamplingItem> lbslilist,out List<LisBarCodeFormVo> lisbcfvolist)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            paralist = new List<BPara>();
            goivlist = new List<List<GroupingOrderItemVo>>();
            lisoftvolist = new List<List<LisOrderFormItemVo>>();
            lbslilist = new List<LBSamplingItem>();
            lisbcfvolist = new List<LisBarCodeFormVo>();
                  
            //获取参数配置
            List<BPara> bParas = IBBParaItem.SelPreBParas(nodetype,"", Pre_AllModules.样本条码.Value.DefaultValue, userid.ToString(), username);
            #region 医嘱信息查询并医嘱分组
            //查询医嘱单病人信息以及医嘱项目
            List<LBSamplingItem> lBSamplingItems = new List<LBSamplingItem>();//所有项目所属的采样组
            List<LisBarCodeForm> lisBarCodeForms = new List<LisBarCodeForm>();
            List<List<LisOrderFormItemVo>> lisOrderFormItemVos = GetOrderFormItemGroup(bParas, receiveType, value, days, nextindex,labid,out lBSamplingItems,out lisBarCodeForms);
            if ((lisOrderFormItemVos == null || lisOrderFormItemVos.Count == 0) && (lisBarCodeForms.Count == 0 || lisBarCodeForms == null)) {
                brdv.success = false;
                brdv.ErrorInfo = "未查询到医嘱项目信息！";
                return brdv;
            }
            List<LisBarCodeFormVo> barCodeFormVos = new List<LisBarCodeFormVo>();
            barCodeFormVos = ConvertLisBarCodeFormVo(lisBarCodeForms);
            #endregion
            #region 采样分管
            if (lisOrderFormItemVos != null && lisOrderFormItemVos.Count > 0) {
                List<List<LisOrderFormItemVo>> samplingGroupItemResult = new List<List<LisOrderFormItemVo>>();//原始医嘱信息
                                                                                                              //单采样组分组
                List<List<GroupingOrderItemVo>> allSamplingGroups = OrderItemGroupByOneSamplingGroup(bParas, lisOrderFormItemVos, lBSamplingItems, nextindex, out samplingGroupItemResult);
                //多采样分组
                allSamplingGroups = OrderItemGroupByMoreSamplingGroup(bParas, allSamplingGroups, samplingGroupItemResult, lBSamplingItems, nextindex, out samplingGroupItemResult);
                allSamplingGroups = CreateBarCode(bParas, allSamplingGroups, samplingGroupItemResult, lBSamplingItems, nextindex);
                paralist = bParas;
                goivlist = allSamplingGroups;
                lisoftvolist = samplingGroupItemResult;
                lbslilist = lBSamplingItems;
            }
            #endregion
            lisbcfvolist = barCodeFormVos;
            return brdv;
        }
        /// <summary>
        /// Lis获取医嘱信息并采样分管 
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="receiveType"></param>
        /// <param name="value"></param>
        /// <param name="days"></param>
        /// <param name="nextindex"></param>
        /// <returns></returns>
        public BaseResultDataValue LISGetSampleAndGrouping(long nodetype, string receiveType, string value, int days,string userid,string username,string labid, int nextindex, out List<BPara> paralist, out List<List<GroupingOrderItemVo>> goivlist, out List<List<LisOrderFormItemVo>> lisoftvolist, out List<LBSamplingItem> lbslilist, out List<LisBarCodeFormVo> lisbcfvolist)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            ZhiFang.Common.Log.Log.Debug("LabStarPreService.LISGetSampleAndGrouping.获取LIS医嘱并采样分管开始！");
            ZhiFang.Common.Log.Log.Debug("LabStarPreService.LISGetSampleAndGrouping.传入参数(nodetype：+" + nodetype + ",receiveType:" + receiveType + ",value=" + value + ",days=" + days + ",nextindex=" + nextindex + ")");
            paralist = new List<BPara>();
            goivlist = new List<List<GroupingOrderItemVo>>();
            lisoftvolist = new List<List<LisOrderFormItemVo>>();
            lbslilist = new List<LBSamplingItem>();
            lisbcfvolist = new List<LisBarCodeFormVo>();
            #region 入参验证与获取配置参数
            if (string.IsNullOrEmpty(receiveType) || string.IsNullOrEmpty(value))
            {
                brdv.success = false;
                brdv.ErrorInfo = "核收条件不可为空！";               
                return brdv;
            }
            if (days == 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "样本过滤天数不可为0！";
                return brdv;
            }
            
            //获取参数配置
            List<BPara> bParas = IBBParaItem.SelPreBParas(nodetype, "", Pre_AllModules.样本条码.Value.DefaultValue, userid, username);
            #region 验卡
            var checkcardurl = bParas.Where(a => a.ParaNo == "Pre_OrderBarCode_DefaultPara_0027").First();
            if (!string.IsNullOrEmpty(checkcardurl.ParaValue))
            {
                value = CheckCard(nodetype, receiveType, value).ResultDataValue;
            }
            #endregion
            #endregion
            #region 医嘱信息查询并医嘱分组
            //查询医嘱单病人信息以及医嘱项目
            List<LBSamplingItem> lBSamplingItems = new List<LBSamplingItem>();//所有项目所属的采样组
            List<LisBarCodeForm> lisBarCodeForms = new List<LisBarCodeForm>();
            List<List<LisOrderFormItemVo>> lisOrderFormItemVos = GetOrderFormItemGroup(bParas, receiveType, value, days, nextindex,labid, out lBSamplingItems,out lisBarCodeForms);
            if ((lisOrderFormItemVos == null || lisOrderFormItemVos.Count == 0) &&  (lisBarCodeForms == null || lisBarCodeForms.Count == 0))
            {
                brdv.success = false;
                brdv.ErrorInfo = "未查询到医嘱项目信息！";
                return brdv;
            }
            #endregion
            List<LisBarCodeFormVo> barCodeFormVos = new List<LisBarCodeFormVo>();
            barCodeFormVos = ConvertLisBarCodeFormVo(lisBarCodeForms);
            #region 采样分管
            if (lisOrderFormItemVos != null && lisOrderFormItemVos.Count > 0)
            {
                List<List<LisOrderFormItemVo>> samplingGroupItemResult = new List<List<LisOrderFormItemVo>>();//原始医嘱信息
                                                                                                              //单采样组分组
                List<List<GroupingOrderItemVo>> allSamplingGroups = OrderItemGroupByOneSamplingGroup(bParas, lisOrderFormItemVos, lBSamplingItems, nextindex, out samplingGroupItemResult);
                //多采样分组
                allSamplingGroups = OrderItemGroupByMoreSamplingGroup(bParas, allSamplingGroups, samplingGroupItemResult, lBSamplingItems, nextindex, out samplingGroupItemResult);
                #region 条码生成
                allSamplingGroups = CreateBarCode(bParas, allSamplingGroups, samplingGroupItemResult, lBSamplingItems, nextindex);
                #endregion
                paralist = bParas;
                goivlist = allSamplingGroups;
                lisoftvolist = samplingGroupItemResult;
                lbslilist = lBSamplingItems;
            }
            #endregion
            lisbcfvolist = barCodeFormVos;
            return brdv;
        }
        /// <summary>
        /// 验卡
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="receiveType"></param>
        /// <param name="card"></param>
        /// <returns></returns>
        public BaseResultDataValue CheckCard(long nodetype,string receiveType, string card) 
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            baseResultDataValue.success = true;
            baseResultDataValue.ResultDataValue = card;
            return baseResultDataValue;
        }
        /// <summary>
        /// 医嘱信息查询
        /// </summary>
        /// <param name="bParas"></param>
        /// <param name="receiveType"></param>
        /// <param name="value"></param>
        /// <param name="days"></param>
        /// <param name="nextindex"></param>
        /// <returns></returns>
        public List<List<LisOrderFormItemVo>> GetOrderFormItemGroup(List<BPara> bParas, string receiveType, string value, int days, int nextindex,string labid, out List<LBSamplingItem> lBSamplingItems,out List<LisBarCodeForm> lisBarCodeForms) 
        {
            List<LBSamplingItem> lBSamplingItemlist = new List<LBSamplingItem>();
            #region 医嘱分组参数处理    需要加上医嘱执行标记判断
            string fields = "lispatient.CName,lispatient.HisPatNo";
            var groupfields = bParas.Where(a => a.ParaNo == "Pre_OrderBarCode_DefaultPara_0013").ToList();//检验项目拆分条码依据，医嘱分组规则字段
            if (groupfields.Count > 0)
            {
                BPara DefaultPara0010 = groupfields.First();
                if (!string.IsNullOrEmpty(DefaultPara0010.ParaValue)) {
                    fields += "," + DefaultPara0010.ParaValue;
                }
            }
            //查询条件拼接
            string where = " 1=1 and IsAffirm=1 and (lisorderform.OrderExecFlag=0 or isnull(lisorderform.OrderExecFlag,'')='') ";//已审核但 未执行医嘱查询的条件
            where += " and "+ receiveType + "='" + value + "'";
            string barcodeformwhere = "1=1 and " + receiveType + "='" + value + "'";           
            if (labid != null && labid.Trim() != "")
            {
                where += " and lisorderform.LabID=" + labid;
                barcodeformwhere += " and lisorderform.LabID=" + labid;
            }
            var sicktypenos = bParas.Where(a => a.ParaNo == "Pre_OrderBarCode_DefaultPara_0014").ToList();//就诊类型选择
            var deptnos = bParas.Where(a => a.ParaNo == "Pre_OrderBarCode_DefaultPara_0018").ToList();//科室选择
            var districtids = bParas.Where(a => a.ParaNo == "Pre_OrderBarCode_DefaultPara_0019").ToList();//病区选择
            var sampleday = bParas.Where(a => a.ParaNo == "Pre_OrderBarCode_DefaultPara_0021").ToList();//样本过滤天数
            if (sicktypenos.Count > 0)
            {
                BPara DefaultPara0014 = sicktypenos.First();
                if (!string.IsNullOrEmpty(DefaultPara0014.ParaValue))
                {
                     where += " and lispatient.SickTypeID in (" + DefaultPara0014.ParaValue + ")";
                     barcodeformwhere += " and lispatient.SickTypeID in (" + DefaultPara0014.ParaValue + ")";
                }
            }
            if (deptnos.Count > 0) {
                BPara DefaultPara0018 = deptnos.First();
                if (!string.IsNullOrEmpty(DefaultPara0018.ParaValue))
                {
                    string[] paraarr = DefaultPara0018.ParaValue.Split('|');
                    List<string> ids = new List<string>();
                    foreach (var item in paraarr[1].Split(','))
                    {
                        ids.Add(item.Split('&')[0]);
                    }
                    if (paraarr[0] == "0")
                    {
                        where += " and lispatient.DeptID not in (" + string.Join(",", ids) + ")";
                        barcodeformwhere += " and lispatient.DeptID not in (" + string.Join(",", ids) + ")";
                    }
                    else if (paraarr[0] == "1")
                    {
                        where += " and lispatient.DeptID in (" + string.Join(",", ids) + ")";
                        barcodeformwhere += " and lispatient.DeptID in (" + string.Join(",", ids) + ")";
                    }                   
                }
            }
            if (districtids.Count > 0)
            {
                BPara DefaultPara0019 = districtids.First();
                if (!string.IsNullOrEmpty(DefaultPara0019.ParaValue))
                {
                    string[] paraarr = DefaultPara0019.ParaValue.Split('|');
                    List<string> ids = new List<string>();
                    foreach (var item in paraarr[1].Split(','))
                    {
                        ids.Add(item.Split('&')[0]);
                    }
                    if (paraarr[0] == "0")
                    {
                        where += " and lispatient.DistrictID not in (" + string.Join(",", ids) + ")";
                        barcodeformwhere += " and lispatient.DistrictID not in (" + string.Join(",", ids) + ")";
                    }
                    else if (paraarr[0] == "1")
                    {
                        where += " and lispatient.DistrictID in (" + string.Join(",", ids) + ")";
                        barcodeformwhere += " and lispatient.DistrictID in (" + string.Join(",", ids) + ")";
                    }
                   
                }
            }
            if (days == 0)
            {
                if (sampleday.Count > 0)
                {
                    BPara DefaultPara0021 = sampleday.First();
                    if (!string.IsNullOrEmpty(DefaultPara0021.ParaValue) && DefaultPara0021.ParaValue != "0")
                    {
                        int day = int.Parse(DefaultPara0021.ParaValue);
                        where += " and (lisorderform.OrderTime >= '" + DateTime.Now + "' and lisorderform.OrderTime <= '" + DateTime.Now.AddDays(-day) + "')";
                        barcodeformwhere += " and (lisorderform.OrderTime >= '" + DateTime.Now + "' and lisorderform.OrderTime <= '" + DateTime.Now.AddDays(-day) + "')";
                    }
                    else
                    {
                        where += " and lisorderform.OrderTime == '" + DateTime.Now + "'";
                        barcodeformwhere += " and lisorderform.OrderTime == '" + DateTime.Now + "'";
                    }
                }
            }
            else 
            {
                where += " and (lisorderform.OrderTime <= '" + DateTime.Now + "' and lisorderform.OrderTime >= '" + DateTime.Now.AddDays(-days) + "')";
                barcodeformwhere += " and (lisorderform.OrderTime <= '" + DateTime.Now + "' and lisorderform.OrderTime >= '" + DateTime.Now.AddDays(-days) + "')";
            }
            #endregion
            #region 查询已生成条码但未确认的样本单
            List<LisBarCodeForm> lisBarCodeFormarr = new List<LisBarCodeForm>();
            var orderformids  = (this.DBDao as IDLisOrderFormDao).GetOrderFormList("lisorderform.OrderFormID", barcodeformwhere, out string bwhere);
            if (orderformids != null)
            {
                List<long> ids = new List<long>();
                foreach (var formid in orderformids)
                {
                    //Array dataarr = (Array)formid;
                    long id = long.Parse(formid.ToString());
                    if (!ids.Contains(id))
                        ids.Add(id);
                }
                lisBarCodeFormarr = IBLisBarCodeForm.SearchListByHQL("IsAffirm = 0 and BarCodeFlag != -1 and LisOrderForm.Id in (" + string.Join(",", ids) + ")").ToList();
                for (int i = lisBarCodeFormarr.Count -1 ; i >= 0; i--)
                {
                    if (lisBarCodeFormarr[i].BarCodeFlag == -1) {
                        lisBarCodeFormarr.RemoveAt(i);
                    }
                }
            }
            lisBarCodeForms = lisBarCodeFormarr;
            #endregion
            #region 查询医嘱单病人信息以及医嘱项目信息，进行医嘱分组
            string defaultwhere = "";//默认条件
            //根据病人分组拆分字段查询字段值
            var lisOrderFormVos = (this.DBDao as IDLisOrderFormDao).GetOrderFormList(fields, where, out defaultwhere);
            if (lisOrderFormVos != null)
            {
                List<List<LisOrderFormItemVo>> lisOrderFormItemVos = new List<List<LisOrderFormItemVo>>();
                var fieldsarr = fields.Split(',');
               
                foreach (var orderformvo in lisOrderFormVos)
                {
                    //定义医嘱信息VO
                    List<LisOrderFormItemVo> lisOrderFormItemVo = new List<LisOrderFormItemVo>();                    
                    #region 根据查询出来的值拼接where条件
                    Array dataarr = (Array)orderformvo;
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
                            followwhere += " and isnull("+ field + ", '') = ''";
                        }
                    }
                    #endregion
                    #region 医嘱单病人信息数据查询
                    string uselesswhere = "";
                    string strwhere = defaultwhere + followwhere;
                    string formpatfields = "lisorderform.OrderFormID,lisorderform.OrderFormNo,lisorderform.PatID,lispatient.CName,lispatient.SickTypeID";
                    var OrderFormPatInfo = (this.DBDao as IDLisOrderFormDao).GetOrderFormList(formpatfields, strwhere, out uselesswhere);
                    List<long> itemids = new List<long>();
                    foreach (var info in OrderFormPatInfo)
                    {
                        Array infoarr = (Array)info;
                        itemids.Add(long.Parse(infoarr.GetValue(0).ToString()));
                        LisOrderFormItemVo orderFormPatientVo  = new LisOrderFormItemVo();
                        orderFormPatientVo.OrderFormID = long.Parse(infoarr.GetValue(0).ToString());
                        orderFormPatientVo.OrderFormNo = infoarr.GetValue(1).ToString();
                        orderFormPatientVo.PatID = long.Parse(infoarr.GetValue(2).ToString());
                        orderFormPatientVo.CName = infoarr.GetValue(3).ToString();
                        if (infoarr.GetValue(4) != null) {
                            orderFormPatientVo.SickTypeID = long.Parse(infoarr.GetValue(4).ToString());
                        }
                        lisOrderFormItemVo.Add(orderFormPatientVo);
                    }
                    //拼接查询医嘱项目条件
                    string itemwhere = "OrderFormID in (" + string.Join(",", itemids) + ")";
                    itemwhere += " and (OrderItemExecFlag=0 or isnull(OrderItemExecFlag,0)=0)";
                    itemwhere += " and (IsCancelled=0 or isnull(IsCancelled,0)=0)";
                    var para0020 = bParas.Where(a => a.ParaNo == "Pre_OrderBarCode_DefaultPara_0020").ToList();//样本类型
                    if (para0020.Count > 0)
                    {
                        BPara DefaultPara0020 = para0020.First();
                        if (!string.IsNullOrEmpty(DefaultPara0020.ParaValue))
                        {
                            string[] paraarr = DefaultPara0020.ParaValue.Split('|');
                            List<string> ids = new List<string>();
                            foreach (var sampletype in paraarr[1].Split(','))
                            {
                                ids.Add(sampletype.Split('&')[0]);
                            }
                            if (paraarr[0] == "0")
                            {
                                itemwhere += " and SampleTypeID not in (" + string.Join(",", ids) + ")";
                            }
                            else if (paraarr[0] == "1")
                            {
                                itemwhere += " and SampleTypeID in (" + string.Join(",", ids) + ")";
                            }
                        }
                    }
                    var lisOrderItems = IDLisOrderItemDao.GetListByHQL(itemwhere).ToList();//查询原始医嘱项目
                    #endregion
                    #region 医嘱项目数据查询与医嘱单标记是否分管和标记条码生成方式
                    if (lisOrderItems.Count() > 0) {
                        
                        List<long?> ordersItemIDs = new List<long?>();
                        foreach (var lisOrderItem in lisOrderItems)
                        {
                            if(!ordersItemIDs.Contains(lisOrderItem.OrdersItemID))
                                ordersItemIDs.Add(lisOrderItem.OrdersItemID);
                        }
                        EntityList<LBSamplingItem> otherlbsi = IDLBSamplingItemDao.QueryLBSamplingItemByFetchDao("lbitem.Id in (" + string.Join(",", ordersItemIDs)+")","",0,0);//查询项目采样组
                        foreach (var addsil in otherlbsi.list)
                        {
                            if (lBSamplingItemlist.Where(w=>w.LBItem.Id == addsil.LBItem.Id && w.LBSamplingGroup.Id == addsil.LBSamplingGroup.Id).Count() == 0) {
                                lBSamplingItemlist.Add(addsil);
                            }
                        }
                        lBSamplingItemlist = lBSamplingItemlist.Distinct().ToList();
                        IList<LBItem> lBItems = IDLBItemDao.GetListByHQL("Id in ("+string.Join(",", ordersItemIDs)+")");//查询检验项目信息
                       //所有医嘱项目根据医嘱单ID分组
                        var lisOrderItemsgroups = lisOrderItems.GroupBy(a => a.OrderFormID);
                        //获取根据就诊类型判断是否分管和条码生成方式的参数
                        var para0022 = bParas.Where(a => a.ParaNo == "Pre_OrderBarCode_DefaultPara_0022").ToList();//根据就诊类型区分是否分管
                        var para0023 = bParas.Where(a => a.ParaNo == "Pre_OrderBarCode_DefaultPara_0023").ToList();//根据就诊类型判断条码生成方式
                        var para0017 = bParas.Where(a => a.ParaNo == "Pre_OrderBarCode_DefaultPara_0017").ToList();//采样组过滤
                        bool sampleGroupIsShow = true;
                        List<long> sampleGroupIds = new List<long>();//参数设定的采样组
                        if (para0017.Count > 0) {
                            if (!string.IsNullOrEmpty(para0017.First().ParaValue)) {
                                var  sgarr = para0017.First().ParaValue.Split('|');
                                if (sgarr[0] == "0") {
                                    sampleGroupIsShow = false;
                                }
                                var sgarrs = sgarr[1].Split(',');
                                for (int i = 0; i < sgarrs.Count(); i++)
                                {
                                    sampleGroupIds.Add(long.Parse(sgarrs[i]));
                                }
                            }
                        }
                        foreach (var lisOrderItemsgroup in lisOrderItemsgroups)
                        {
                            #region 医嘱单标记处理
                            //过滤医嘱单
                            var orderfrom = lisOrderFormItemVo.Where(a => a.OrderFormID == lisOrderItemsgroup.Key);
                            bool isGrouping = true;//是否分组
                            if (!string.IsNullOrEmpty(para0022.First().ParaValue)) {
                                var para0022arr = para0022.First().ParaValue.Split(',');
                                for (int i = 0; i < para0022arr.Count(); i++)
                                {
                                    string[] sickTypeIsGroup= para0022arr[i].Split('&');
                                    if (orderfrom.First().SickTypeID == long.Parse(sickTypeIsGroup[0]) && sickTypeIsGroup[1] == "0") {
                                        isGrouping = false;
                                    }
                                }
                            }
                            int generationType = 2;
                            if (!string.IsNullOrEmpty(para0023.First().ParaValue))
                            {
                                var para0023arr = para0023.First().ParaValue.Split(',');
                                for (int i = 0; i < para0023arr.Count(); i++)
                                {
                                    string[] generationTypes = para0023arr[i].Split('&');
                                    if (orderfrom.First().SickTypeID == long.Parse(generationTypes[0]))
                                    {
                                        generationType = int.Parse(generationTypes[1]);
                                    }
                                }
                            }
                            orderfrom.ElementAt(0).IsGrouping = isGrouping;
                            orderfrom.ElementAt(0).SerialGenerationType = generationType;
                            List<GroupingOrderItemVo> OrderItemVos = new List<GroupingOrderItemVo>();
                            #endregion
                            #region 医嘱项目数据处理
                            if (isGrouping)
                            {
                                foreach (var lisOrderItem in lisOrderItemsgroup)
                                {
                                    LBItem lbitem = lBItems.Where(a => a.Id == lisOrderItem.OrdersItemID).First();
                                    bool IsSingleItemDispose = false;
                                    if (lbitem.GroupType == 1)
                                    {
                                        IList<LBParItemSplit> lBParItemSplits = IDLBParItemSplitDao.QueryLBParItemSplitDao("paritem.Id=" + lisOrderItem.OrdersItemID,null);
                                        //判断是否维护组合项目拆分规则如果没有维护则取组合项目
                                        if (lBParItemSplits.Count > 0)
                                        {
                                            List<long> detailitemid = new List<long>();
                                            List<GroupingOrderItemVo> splitsOrderItemVos = new List<GroupingOrderItemVo>();
                                            for (int i = 0; i < lBParItemSplits.Count(); i++)
                                            {
                                                GroupingOrderItemVo groupingOrderItemVo = new GroupingOrderItemVo();
                                                groupingOrderItemVo.OrderItemID = lisOrderItem.Id;
                                                groupingOrderItemVo.OrderFormID = long.Parse(lisOrderItem.OrderFormID.ToString());
                                                groupingOrderItemVo.OrderFormNo = lisOrderItem.OrderFormNo;
                                                groupingOrderItemVo.GroupItemID = lisOrderItem.OrdersItemID;
                                                groupingOrderItemVo.OrderItemExecFlag = lisOrderItem.OrderItemExecFlag;
                                                groupingOrderItemVo.SampleTypeID = lisOrderItem.SampleTypeID;
                                                groupingOrderItemVo.ItemStatusID = lisOrderItem.ItemStatusID;
                                                groupingOrderItemVo.OrdersItemID = lBParItemSplits[i].LBItem.Id;
                                                groupingOrderItemVo.ItemCName = lBParItemSplits[i].LBItem.CName;
                                                groupingOrderItemVo.IsAutoUnion = lBParItemSplits[i].IsAutoUnion;
                                                groupingOrderItemVo.CollectSort = lBParItemSplits[i].LBItem.CollectSort;
                                                groupingOrderItemVo.IsGroupItem = true;
                                                groupingOrderItemVo.SickTypeID = orderfrom.First().SickTypeID;
                                                groupingOrderItemVo.CollectPart = lisOrderItem.CollectPart;
                                                groupingOrderItemVo.PartitionDate = lisOrderItem.PartitionDate;
                                                groupingOrderItemVo.Charge = float.Parse(lBParItemSplits[i].LBItem.ItemCharge.ToString());
                                                groupingOrderItemVo.IsCheckFee = lisOrderItem.IsCheckFee;
                                                splitsOrderItemVos.Add(groupingOrderItemVo);
                                                detailitemid.Add(lBParItemSplits[i].LBItem.Id);
                                            }
                                            EntityList<LBSamplingItem> detailitemlbsi = IDLBSamplingItemDao.QueryLBSamplingItemByFetchDao("lbitem.Id in (" + string.Join(",", detailitemid) + ")", "", 0, 0);//查询项目采样组
                                            foreach (var addsil in detailitemlbsi.list)
                                            {
                                                if (lBSamplingItemlist.Where(w => w.LBItem.Id == addsil.LBItem.Id && w.LBSamplingGroup.Id == addsil.LBSamplingGroup.Id).Count() == 0)
                                                {
                                                    lBSamplingItemlist.Add(addsil);
                                                }
                                            }
                                            //判断是否设定采样组
                                            if (sampleGroupIds.Count > 0)
                                            {
                                                bool isadd = true;
                                                foreach (var splitItem in splitsOrderItemVos)
                                                {
                                                    bool isok = false;
                                                    lBSamplingItemlist.Where(w => w.LBItem.Id == splitItem.OrdersItemID);
                                                    if (lBSamplingItemlist.Count > 0)
                                                    {
                                                        foreach (var si in lBSamplingItemlist)
                                                        {
                                                            if (sampleGroupIds.Where(w => w == si.LBSamplingGroup.Id).Count() > 0)
                                                            {
                                                                isadd = true;
                                                                break;
                                                            }
                                                        }
                                                    }
                                                    if (!isok) 
                                                    {
                                                        isadd = false;
                                                        break;
                                                    }
                                                }
                                                if (isadd)
                                                {
                                                    OrderItemVos.AddRange(splitsOrderItemVos);
                                                }
                                            }
                                            else
                                            {
                                                OrderItemVos.AddRange(splitsOrderItemVos);
                                            }
                                        }
                                        else 
                                        {
                                            IsSingleItemDispose = true;
                                        }
                                    }
                                    else
                                    {
                                        IsSingleItemDispose = true;
                                    }
                                    if(IsSingleItemDispose)
                                    {
                                        GroupingOrderItemVo groupingOrderItemVo = new GroupingOrderItemVo();
                                        groupingOrderItemVo.OrderItemID = lisOrderItem.Id;
                                        groupingOrderItemVo.OrderFormID = long.Parse(lisOrderItem.OrderFormID.ToString());
                                        groupingOrderItemVo.OrderFormNo = lisOrderItem.OrderFormNo;
                                        groupingOrderItemVo.GroupItemID = lisOrderItem.OrdersItemID;
                                        groupingOrderItemVo.OrderItemExecFlag = lisOrderItem.OrderItemExecFlag;
                                        groupingOrderItemVo.SampleTypeID = lisOrderItem.SampleTypeID;
                                        groupingOrderItemVo.ItemStatusID = lisOrderItem.ItemStatusID;
                                        groupingOrderItemVo.OrdersItemID = lbitem.Id;
                                        groupingOrderItemVo.ItemCName = lbitem.CName;
                                        groupingOrderItemVo.IsAutoUnion = false;
                                        groupingOrderItemVo.CollectSort = lbitem.CollectSort;
                                        groupingOrderItemVo.IsGroupItem = false;
                                        groupingOrderItemVo.SickTypeID = orderfrom.First().SickTypeID;
                                        groupingOrderItemVo.PartitionDate = lisOrderItem.PartitionDate;
                                        groupingOrderItemVo.CollectPart = lisOrderItem.CollectPart;
                                        groupingOrderItemVo.Charge = float.Parse(lisOrderItem.Charge.ToString());
                                        groupingOrderItemVo.IsCheckFee = lisOrderItem.IsCheckFee;
                                        //判断是否设定采样组
                                        if (sampleGroupIds.Count > 0)
                                        {
                                            bool isadd = false;
                                            lBSamplingItemlist.Where(w=>w.LBItem.Id == groupingOrderItemVo.OrdersItemID);
                                            if (lBSamplingItemlist.Count > 0) {
                                                foreach (var si in lBSamplingItemlist)
                                                {
                                                    if (sampleGroupIds.Where(w => w == si.LBSamplingGroup.Id).Count() > 0){
                                                        isadd = true;
                                                        break;
                                                    }
                                                }
                                            }
                                            if (isadd) {
                                                OrderItemVos.Add(groupingOrderItemVo);
                                            }
                                        }
                                        else 
                                        {
                                            OrderItemVos.Add(groupingOrderItemVo);
                                        }
                                    }
                                }
                            }
                            else 
                            {
                                foreach (var lisOrderItem in lisOrderItemsgroup)
                                {
                                    LBItem lbitem = lBItems.Where(a => a.Id == lisOrderItem.OrdersItemID).First();
                                    GroupingOrderItemVo groupingOrderItemVo = new GroupingOrderItemVo();
                                    groupingOrderItemVo.OrderItemID = lisOrderItem.Id;
                                    groupingOrderItemVo.OrderFormID = long.Parse(lisOrderItem.OrderFormID.ToString());
                                    groupingOrderItemVo.OrderFormNo = lisOrderItem.OrderFormNo;
                                    groupingOrderItemVo.GroupItemID = lisOrderItem.OrdersItemID;
                                    groupingOrderItemVo.OrderItemExecFlag = lisOrderItem.OrderItemExecFlag;
                                    groupingOrderItemVo.SampleTypeID = lisOrderItem.SampleTypeID;
                                    groupingOrderItemVo.ItemStatusID = lisOrderItem.ItemStatusID;
                                    groupingOrderItemVo.OrdersItemID = lbitem.Id;
                                    groupingOrderItemVo.ItemCName = lbitem.CName;
                                    groupingOrderItemVo.IsAutoUnion = false;
                                    groupingOrderItemVo.CollectSort = lbitem.CollectSort;
                                    groupingOrderItemVo.IsGroupItem = false;
                                    groupingOrderItemVo.CollectPart = lisOrderItem.CollectPart;
                                    groupingOrderItemVo.PartitionDate = lisOrderItem.PartitionDate;
                                    groupingOrderItemVo.Charge = float.Parse(lisOrderItem.Charge.ToString());
                                    groupingOrderItemVo.IsCheckFee = lisOrderItem.IsCheckFee;
                                    //判断是否设定采样组
                                    if (sampleGroupIds.Count > 0)
                                    {
                                        bool isadd = false;
                                        lBSamplingItemlist.Where(w => w.LBItem.Id == groupingOrderItemVo.OrdersItemID);
                                        if (lBSamplingItemlist.Count > 0)
                                        {
                                            foreach (var si in lBSamplingItemlist)
                                            {
                                                if (sampleGroupIds.Where(w => w == si.LBSamplingGroup.Id).Count() > 0)
                                                {
                                                    isadd = true;
                                                    break;
                                                }
                                            }
                                        }
                                        if (isadd)
                                        {
                                            OrderItemVos.Add(groupingOrderItemVo);
                                        }
                                    }
                                    else
                                    {
                                        OrderItemVos.Add(groupingOrderItemVo);
                                    }
                                }
                            }
                            orderfrom.ElementAt(0).groupingOrderItemVos = OrderItemVos;
                            #endregion
                        }
                        lisOrderFormItemVos.Add(lisOrderFormItemVo);
                    }
                    #endregion
                }
                lBSamplingItems = lBSamplingItemlist.Distinct().ToList();
                return lisOrderFormItemVos;
            }
            else
            {
                ZhiFang.Common.Log.Log.Debug("OrderFormItemGroup.医嘱信息查询:未查询到医嘱信息！");
                lBSamplingItems = null;
                return null;
            }
            
            #endregion
        }
        /// <summary>
        /// 单采样组分管
        /// </summary>
        /// <param name="bParas"></param>
        /// <param name="originalData"></param>
        /// <param name="nextindex"></param>
        /// <returns></returns>
        public List<List<GroupingOrderItemVo>> OrderItemGroupByOneSamplingGroup(List<BPara> bParas, List<List<LisOrderFormItemVo>> originalItemData, List<LBSamplingItem> originalGroupData, int nextindex,out List<List<LisOrderFormItemVo>> outoriginalItemData) 
        {
            #region 采样分管处理
            List<List<GroupingOrderItemVo>> allgrouptem = new List<List<GroupingOrderItemVo>>();//总分管结果（压平）

            for (int i = 0; i < originalItemData.Count; i++)
            {
                List<long> orderFormIds = new List<long>();//同一个医嘱分组的所有医嘱单ID
                originalItemData[i].ForEach(f => orderFormIds.Add(f.OrderFormID));
                orderFormIds = orderFormIds.Distinct().ToList();
                for (int a = originalItemData[i].Count -1; a > -1; a--)
                {
                    LisOrderFormItemVo orderformitem = originalItemData[i][a];
                    //如果没有子项就删除数组中的医嘱信息执行下一个(因为上一步操作如果进行了采样组过滤数组中医嘱单对应的医嘱项目可能会为空)
                    if (orderformitem.groupingOrderItemVos.Count() == 0) 
                    {
                        originalItemData[i].RemoveAt(a);
                        continue;
                    }
                    //判断是否进行分管，如果不分管则直接将数据加入列表等待进行条码生成逻辑的处理
                    if (orderformitem.IsGrouping)
                    {
                        List<GroupingOrderItemVo> orderitems = orderformitem.groupingOrderItemVos;
                        orderitems.OrderBy(o => o.CollectSort);//根据检验项目表的排序字段进行排序
                        for (int b = 0; b < orderitems.Count(); b++)
                        {
                            GroupingOrderItemVo orderItemVo = orderitems[b];
                            var ItemBelongSapmleGroup = originalGroupData.Where(q => q.LBItem.Id == orderItemVo.OrdersItemID).ToList(); //项目采样组获取
                            if (ItemBelongSapmleGroup.Count == 0)
                            {
                                #region 不属于任何采样组的项目处理
                                orderItemVo.IsMoreSamplingGroup = false;
                                orderItemVo.SamplingGroupID = 0;
                                orderItemVo.SampleGroupingType = SampleGroupingType.无采样组.Key;
                                bool IsNewArr = true;//标识是否新增分管
                                foreach (var items in allgrouptem)
                                {
                                    int itemnum  = items.Where(w => w.SampleGroupingType == SampleGroupingType.无采样组.Key).Count();
                                    if (itemnum > 0)
                                    {
                                        items.Add(orderItemVo);
                                        IsNewArr = false;
                                        break;
                                        
                                    }
                                }
                                if (IsNewArr)
                                {
                                    List<GroupingOrderItemVo> newgroup = new List<GroupingOrderItemVo>();
                                    newgroup.Add(orderItemVo);
                                    allgrouptem.Add(newgroup);
                                }
                                #endregion
                            }
                            else if (ItemBelongSapmleGroup.Count == 1)
                            {
                                #region 属于一个采样组的项目处理
                                orderItemVo.IsMoreSamplingGroup = false;
                                orderItemVo.SamplingGroupID = ItemBelongSapmleGroup.First().LBSamplingGroup.Id;
                                orderItemVo.VirtualItemNo = ItemBelongSapmleGroup.First().VirtualItemNo;
                                orderItemVo.SampleGroupingType = SampleGroupingType.分管成功.Key;
                                bool IsNewArr = true;//标识是否新增分管
                                foreach (var items in allgrouptem)
                                {
                                    //是否同采样组
                                    var itemnum = items.Where(w => w.SampleGroupingType == SampleGroupingType.分管成功.Key && w.SamplingGroupID == orderItemVo.SamplingGroupID).Count();
                                    if (itemnum > 0)
                                    {
                                        bool isSameGroup = false;//是否通一个医嘱分组的
                                        foreach (var item in items)
                                        {
                                            if (orderFormIds.Where(w => w == item.OrderFormID).Count() > 0)
                                            {
                                                isSameGroup = true;
                                                break;
                                            }
                                        }
                                        if (isSameGroup) {
                                            //判断分组中是否包含当前项目
                                            if (items.Where(w => w.OrdersItemID == orderItemVo.OrdersItemID).Count() == 0)
                                            {
                                                int VirtualNo = 0;
                                                items.ForEach(f => VirtualNo += f.VirtualItemNo);
                                                VirtualNo += orderItemVo.VirtualItemNo;
                                                //判断最大虚拟采样量是否符合
                                                if (ItemBelongSapmleGroup.First().LBSamplingGroup.VirtualNo == 0 || ItemBelongSapmleGroup.First().LBSamplingGroup.VirtualNo >= VirtualNo)
                                                {
                                                    items.Add(orderItemVo);
                                                    IsNewArr = false;
                                                }
                                            }
                                        }
                                        
                                    }
                                }
                                
                                if (IsNewArr)
                                {
                                    List<GroupingOrderItemVo> newgroup = new List<GroupingOrderItemVo>();
                                    newgroup.Add(orderItemVo);
                                    allgrouptem.Add(newgroup);
                                }
                                #endregion
                            }
                            else if (ItemBelongSapmleGroup.Count > 1)
                            {
                                #region 属于多个采样组的项目暂不处理，打上多个采样组标记到下一个流程处理
                                orderItemVo.IsMoreSamplingGroup = true;
                                #endregion
                            }

                        }
                    }
                    else 
                    {
                        long sampleGroupId = 0;
                        foreach (var item in orderformitem.groupingOrderItemVos)
                        {
                            var  sampleinggroup = originalGroupData.Where(w => w.LBItem.Id == item.OrderFormID);
                            if (sampleinggroup.Count() > 0) {
                                sampleGroupId = sampleinggroup.First().LBSamplingGroup.Id;
                                break;
                            }
                        }
                        orderformitem.groupingOrderItemVos.ForEach(f => { f.IsMoreSamplingGroup = false; f.SampleGroupingType = SampleGroupingType.不参与分管.Key;f.SamplingGroupID = sampleGroupId; });
                        //如果不需要分管则直接将原始医嘱项目加入到列表中
                        allgrouptem.Add(orderformitem.groupingOrderItemVos);
                    }
                   
                }
            }
            outoriginalItemData = originalItemData;
            return allgrouptem;
            #endregion
        }
        /// <summary>
        /// 多采样组分管
        /// </summary>
        /// <param name="bParas"></param>
        /// <param name="originalSampleGroups"></param>
        /// <param name="originalItemData"></param>
        /// <param name="originalGroupData"></param>
        /// <param name="nextindex"></param>
        /// <param name="outoriginalItemData"></param>
        /// <returns></returns>
        public List<List<GroupingOrderItemVo>> OrderItemGroupByMoreSamplingGroup(List<BPara> bParas,List<List<GroupingOrderItemVo>> originalSampleGroups , List<List<LisOrderFormItemVo>> originalItemData, List<LBSamplingItem> originalGroupData, int nextindex, out List<List<LisOrderFormItemVo>> outoriginalItemData) 
        {
            #region 多采样组分管处理
            //总医嘱分组列表循环
            for (int i = 0; i < originalItemData.Count; i++)
            {
                List<long> orderformids = new List<long>();
                originalItemData[i].ForEach(f => orderformids.Add(f.OrderFormID));
                bool IsNewGroup = false;
                //单个医嘱分组循环
                for (int a = 0; a < originalItemData[i].Count; a++)
                {
                    LisOrderFormItemVo orderformitem = originalItemData[i][a];//单个医嘱信息
                    //判断是否进行分管
                    if (orderformitem.IsGrouping)
                    {
                        #region 采样分管处理
                        List<GroupingOrderItemVo> orderitems = orderformitem.groupingOrderItemVos;//医嘱项目
                        orderitems.OrderBy(o => o.CollectSort);//根据检验项目表的排序字段进行排序
                        #region 医嘱项目循环
                        for (int b = 0; b < orderitems.Count(); b++)
                        {
                            GroupingOrderItemVo orderItemVo = orderitems[b];
                            //判断是否是属于多个采样组的项目
                            if (orderItemVo.IsMoreSamplingGroup) 
                            {
                                //过滤出项目所属的采样组并根据是否缺省采样组和优先级字段进行排序，保证缺省采样组在最后一个并且优先级顺序正确
                                var ItemBelongSapmleGroup = originalGroupData.Where(q => q.LBItem.Id == orderItemVo.OrdersItemID).ToList().OrderBy(o=>o.IsDefault).ThenBy(o=>o.DispOrder); //项目采样组获取
                                foreach (var sg in ItemBelongSapmleGroup)
                                {
                                    bool IsBreak = false;
                                    //判断是否缺省采样组
                                    if (sg.IsDefault)
                                    {
                                        #region 缺省采样组处理
                                        orderItemVo.SampleGroupingType = SampleGroupingType.分管成功.Key;
                                        orderItemVo.VirtualItemNo = sg.VirtualItemNo;
                                        orderItemVo.SamplingGroupID = sg.LBSamplingGroup.Id;
                                        bool IsNewArr = true;//标识是否新增分管
                                        #region 判断原始分管列表中是否存在当前医嘱的数据
                                        foreach (var items in originalSampleGroups)
                                        {
                                            //是否同采样组
                                            var itemnum = items.Where(w => w.SampleGroupingType == SampleGroupingType.分管成功.Key && w.SamplingGroupID == orderItemVo.SamplingGroupID).Count();
                                            if (itemnum > 0)
                                            {
                                                bool isSameGroup = false;//是否同一个医嘱分组的
                                                foreach (var item in items)
                                                {
                                                    if (orderformids.Where(w => w == item.OrderFormID).Count() > 0)
                                                    {
                                                        isSameGroup = true;
                                                        break;
                                                    }
                                                }
                                                //判断分组中是否包含当前项目
                                                if (isSameGroup && items.Where(w => w.OrdersItemID == orderItemVo.OrdersItemID).Count() == 0)
                                                {
                                                    int VirtualNo = 0;
                                                    items.ForEach(f => VirtualNo += f.VirtualItemNo);
                                                    VirtualNo += sg.VirtualItemNo;
                                                    if (sg.LBSamplingGroup.VirtualNo == 0 || sg.LBSamplingGroup.VirtualNo > VirtualNo)
                                                    {
                                                        items.Add(orderItemVo);
                                                        IsNewArr = false;
                                                    }
                                                }

                                            }
                                        }
                                        #endregion
                                        //判断分管列表中是否存在当前采样组
                                        if (IsNewArr)
                                        {
                                            List<GroupingOrderItemVo> newgroup = new List<GroupingOrderItemVo>();
                                            newgroup.Add(orderItemVo);
                                            originalSampleGroups.Add(newgroup);
                                        }
                                        IsBreak = true;
                                        #endregion
                                    }
                                    else 
                                    {
                                        #region 非缺省采样组处理
                                        //判断原始分管列表中是否存在当前医嘱的数据
                                        foreach (var itemarr in originalSampleGroups)
                                        {
                                            //判断分管列表中是否存在当前采样组
                                            if (itemarr.Where(w => w.SampleGroupingType == SampleGroupingType.分管成功.Key && w.SamplingGroupID == sg.LBSamplingGroup.Id).Count() > 0)
                                            {
                                                bool isSameGroup = false;//是否同一个医嘱分组的
                                                foreach (var item in itemarr)
                                                {
                                                    if (orderformids.Where(w => w == item.OrderFormID).Count() > 0)
                                                    {
                                                        isSameGroup = true;
                                                        break;
                                                    }
                                                }
                                                if (isSameGroup && itemarr.Where(w => w.OrdersItemID == orderItemVo.OrdersItemID).Count() == 0)
                                                {
                                                    int VirtualNo = 0;
                                                    itemarr.ForEach(f => VirtualNo += f.VirtualItemNo);
                                                    VirtualNo += sg.VirtualItemNo;
                                                    //判断是否存在必须项目和是否符合最大虚拟采样量
                                                    if (((sg.MustItemID != 0 && itemarr.Where(w=>w.OrdersItemID == sg.MustItemID).Count()>0) || sg.MustItemID == 0 || sg.MustItemID == null) && (sg.LBSamplingGroup.VirtualNo == 0 || (sg.LBSamplingGroup.VirtualNo > 0 && sg.LBSamplingGroup.VirtualNo > VirtualNo))) {
                                                        orderItemVo.SampleGroupingType = SampleGroupingType.分管成功.Key;
                                                        orderItemVo.SamplingGroupID = sg.LBSamplingGroup.Id;
                                                        orderItemVo.VirtualItemNo = sg.VirtualItemNo;
                                                        itemarr.Add(orderItemVo);
                                                        IsBreak = true;
                                                    }
                                                }
                                            }
                                        }
                                        #endregion
                                    }
                                    if (IsBreak) {
                                        break;
                                    }
                                }
                                #region 有找到采样组处理
                                if (orderItemVo.SamplingGroupID  == 0) {
                                    var isadd = true;
                                    orderItemVo.SampleGroupingType = SampleGroupingType.无默认采样组.Key;
                                    orderItemVo.SamplingGroupID = 0;
                                    if (originalSampleGroups.Count > 0)
                                    {
                                        foreach (var item in originalSampleGroups)
                                        {
                                            if (item.Where(w=>w.SampleGroupingType == SampleGroupingType.无默认采样组.Key).Count() > 0) {
                                                item.Add(orderItemVo);
                                                isadd = false;
                                            }
                                        }
                                    }
                                    if (isadd) {
                                        List<GroupingOrderItemVo> newgroup = new List<GroupingOrderItemVo>();
                                        newgroup.Add(orderItemVo);
                                        originalSampleGroups.Add(newgroup);
                                    }
                                }
                                #endregion
                            }
                        }
                        #endregion 医嘱项目循环
                    }
                    #endregion 采样分管处理
                }
            }
            outoriginalItemData = originalItemData;
            return originalSampleGroups;
            #endregion
        }
        /// <summary>
        /// 条码生成
        /// </summary>
        /// <param name="bParas"></param>
        /// <param name="originalSampleGroups"></param>
        /// <param name="originalItemData"></param>
        /// <param name="originalGroupData"></param>
        /// <param name="nextindex"></param>
        /// <returns></returns>
        public List<List<GroupingOrderItemVo>> CreateBarCode(List<BPara> bParas, List<List<GroupingOrderItemVo>> originalSampleGroups, List<List<LisOrderFormItemVo>> originalItemData, List<LBSamplingItem> originalGroupData, int nextindex) 
        {
            #region 参数处理
            List<BPara> barCodeRule = new List<BPara>();
            var para0023 = bParas.Where(a => a.ParaNo == "Pre_OrderBarCode_DefaultPara_0023").First();//根据就诊类型判断条码生成方式
            var para0001 = bParas.Where(a => a.ParaNo == "Pre_OrderBarCode_DefaultPara_0001").First();//条码方式
            barCodeRule.Add(bParas.Where(a => a.ParaNo == "Pre_OrderBarCode_DefaultPara_0002").First());//条码使用固定编码值
            barCodeRule.Add(bParas.Where(a => a.ParaNo == "Pre_OrderBarCode_DefaultPara_0003").First());//条码使用采样管编码
            barCodeRule.Add(bParas.Where(a => a.ParaNo == "Pre_OrderBarCode_DefaultPara_0004").First());//条码使用采样组编码
            barCodeRule.Add(bParas.Where(a => a.ParaNo == "Pre_OrderBarCode_DefaultPara_0005").First());//条码使用日期格式
            barCodeRule.Add(bParas.Where(a => a.ParaNo == "Pre_OrderBarCode_DefaultPara_0006").First());//条码序号是否累计
            barCodeRule.Add(bParas.Where(a => a.ParaNo == "Pre_OrderBarCode_DefaultPara_0007").First());//自动条码号位数
            barCodeRule.Add(bParas.Where(a => a.ParaNo == "Pre_OrderBarCode_DefaultPara_0008").First());//条码是否采用自动编号
            barCodeRule.Add(bParas.Where(a => a.ParaNo == "Pre_OrderBarCode_DefaultPara_0009").First());//条码是否使用申请单号
            barCodeRule.Add(bParas.Where(a => a.ParaNo == "Pre_OrderBarCode_DefaultPara_0010").First());//采用申请单起始号位
            barCodeRule.Add(bParas.Where(a => a.ParaNo == "Pre_OrderBarCode_DefaultPara_0011").First());//采用申请单长度
            barCodeRule.Add(bParas.Where(a => a.ParaNo == "Pre_OrderBarCode_DefaultPara_0012").First());//使用固定后缀编码值
            barCodeRule.OrderBy(o=>o.DispOrder);
            #endregion
            #region 条码生成
            foreach (var items in originalSampleGroups)
            {
                GroupingOrderItemVo groupingOrderItemVo  = items.First();
                #region 项目处理
                if (groupingOrderItemVo.SampleGroupingType != SampleGroupingType.无采样组.Key && groupingOrderItemVo.SampleGroupingType != SampleGroupingType.无默认采样组.Key) {
                    #region 不同就诊类型条码生成方式处理
                    int barcodect = 2;//不同就诊类型条码生成方式：1.采用HIS申请单号。2.LIS生成
                    if (!string.IsNullOrEmpty(para0023.ParaValue)) {
                        for (int i = 0; i < para0023.ParaValue.Split(',').Count(); i++)
                        {
                            if (groupingOrderItemVo.SickTypeID == long.Parse(para0023.ParaValue.Split(',')[i].Split('&')[0])) {
                                barcodect = int.Parse(para0023.ParaValue.Split(',')[i].Split('&')[1]);
                                break;
                            }
                        }
                    }
                    if (barcodect == 0 || barcodect == 2)
                    {
                        //Lis生成
                        LBSamplingItem lBSamplingItem = originalGroupData.Where(w => w.LBSamplingGroup.Id == groupingOrderItemVo.SamplingGroupID).First();//所属采样组信息
                        int barcodecreatetype = 0;
                        //判断条码生成方式 0 现打，1 预制，2 以采样管设置为准
                        if (!string.IsNullOrEmpty(para0001.ParaValue) && para0001.ParaValue == "1")
                        {
                            barcodecreatetype = 1;
                        }
                        else if (!string.IsNullOrEmpty(para0001.ParaValue) && para0001.ParaValue == "2") 
                        {
                            //判断采样管标识是否预制
                            if (lBSamplingItem.LBSamplingGroup.LBTcuvete.IsPrep) {
                                barcodecreatetype = 1;
                            } 
                        }
                        #region 现打条码和预制条码处理
                        if (barcodecreatetype == 0)
                        {

                            string barcode = "";
                            foreach (var para in barCodeRule)
                            {
                                if (para.ParaNo == "Pre_OrderBarCode_DefaultPara_0002" && !string.IsNullOrEmpty(para.ParaValue))
                                {
                                    #region 条码使用固定编码值
                                    barcode += para.ParaValue;
                                    #endregion
                                }
                                else if (para.ParaNo == "Pre_OrderBarCode_DefaultPara_0003" && !string.IsNullOrEmpty(para.ParaValue)) 
                                {
                                    #region 条码使用采样管编码
                                    if (para.ParaValue == "1") {
                                        barcode += lBSamplingItem.LBSamplingGroup.LBTcuvete.SCode;
                                    }
                                    #endregion
                                }
                                else if (para.ParaNo == "Pre_OrderBarCode_DefaultPara_0004" && !string.IsNullOrEmpty(para.ParaValue))
                                {
                                    #region 条码使用采样组编码
                                    if (para.ParaValue == "1")
                                    {
                                        barcode += lBSamplingItem.LBSamplingGroup.SCode;
                                    }
                                    #endregion
                                }
                                else if (para.ParaNo == "Pre_OrderBarCode_DefaultPara_0005" && !string.IsNullOrEmpty(para.ParaValue))
                                {
                                    #region 条码使用日期格式
                                    DateTime dateTime = DateTime.Now;
                                    string y= dateTime.Year.ToString();
                                    string m = dateTime.Month.ToString();
                                    string d = dateTime.Day.ToString();
                                    if (para.ParaValue == "222") {
                                        barcode += y.Substring(2) + m.PadLeft(2, '0') + d.PadLeft(2, '0');
                                    } 
                                    else if (para.ParaValue == "122") 
                                    {
                                        barcode += y.Substring(3) + m.PadLeft(2, '0') + d.PadLeft(2, '0');
                                    }
                                    else if (para.ParaValue == "212")
                                    {
                                        m = m.Replace("10", "A").Replace("11", "B").Replace("12", "C");
                                        if (m.Length > 1) { m = m.Substring(1); }
                                        barcode += y.Substring(2) + m + d.PadLeft(2, '0');
                                    }
                                    else if (para.ParaValue == "112")
                                    {
                                        m = m.Replace("10", "A").Replace("11", "B").Replace("12", "C");
                                        if (m.Length > 1) { m = m.Substring(1); }
                                        barcode += y.Substring(3) + m + d.PadLeft(2, '0');
                                    }
                                    else if (para.ParaValue == "111")
                                    {
                                        m = m.Replace("10", "A").Replace("11", "B").Replace("12", "C");
                                        if (m.Length > 1) { m = m.Substring(1); }
                                        string[] letter = new string[]{ "A", "B", "C", "D", "E", "F", "G", "H", "R", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V" };
                                        if (int.Parse(d) > 9) {
                                            d = letter[int.Parse(d) - 10];
                                        }
                                        if (d.Length > 1) { d = d.Substring(1); }
                                        barcode += y.Substring(3) + m + d;
                                    }
                                    #endregion
                                }
                                else if (para.ParaNo == "Pre_OrderBarCode_DefaultPara_0008" && !string.IsNullOrEmpty(para.ParaValue))
                                {
                                    #region 条码是否采用自动编号
                                    if (para.ParaValue == "1") {
                                        var isadd = bParas.Where(a => a.ParaNo == "Pre_OrderBarCode_DefaultPara_0006").First();
                                        var addnum = bParas.Where(a => a.ParaNo == "Pre_OrderBarCode_DefaultPara_0007").First();
                                        string maxno = "";
                                        if (!string.IsNullOrEmpty(isadd.ParaValue))
                                        {
                                            int maxnolength = 0;
                                            if (!string.IsNullOrEmpty(addnum.ParaValue) && addnum.ParaValue != "0")
                                            {
                                                //自动条码号位数
                                                maxnolength = int.Parse(addnum.ParaValue);
                                            }
                                            //条码序号是否累计
                                            if (isadd.ParaValue == "0")
                                            {
                                                maxno =  Edit_GetMaxNo(long.Parse(LBTGetMaxNOBmsTypes.条码号当日流水号.Key), LBTGetMaxNOBmsTypes.条码号当日流水号.Value.Code, false, maxnolength);
                                            }
                                            else 
                                            {
                                                maxno = Edit_GetMaxNo(long.Parse(LBTGetMaxNOBmsTypes.条码号全局流水号.Key), LBTGetMaxNOBmsTypes.条码号全局流水号.Value.Code, true, maxnolength);
                                            }
                                        }
                                        barcode += maxno;
                                    }
                                    #endregion
                                }
                                else if (para.ParaNo == "Pre_OrderBarCode_DefaultPara_0009" && !string.IsNullOrEmpty(para.ParaValue))
                                {
                                    #region 条码是否使用申请单号
                                    if (para.ParaValue == "1") {
                                        var startpara = bParas.Where(a => a.ParaNo == "Pre_OrderBarCode_DefaultPara_0010").First();
                                        var lengthpara = bParas.Where(a => a.ParaNo == "Pre_OrderBarCode_DefaultPara_0011").First();
                                        int start = 0;
                                        int length = 0;
                                        string orderformno = groupingOrderItemVo.OrderFormNo;
                                        if (!string.IsNullOrEmpty(startpara.ParaValue))
                                        {
                                            //采用申请单起始号位
                                            start = int.Parse(startpara.ParaValue);
                                        }
                                        if (!string.IsNullOrEmpty(lengthpara.ParaValue))
                                        {
                                            //采用申请单长度
                                            length = int.Parse(lengthpara.ParaValue);
                                        }
                                        if (length > 0)
                                        {
                                            orderformno = orderformno.Substring(start, length);
                                        }
                                        else {
                                            orderformno = orderformno.Substring(start);
                                        }
                                        barcode += orderformno;
                                    }
                                    #endregion
                                }

                                else if (para.ParaNo == "Pre_OrderBarCode_DefaultPara_0012" && !string.IsNullOrEmpty(para.ParaValue))
                                {

                                    #region 使用固定后缀编码值
                                    barcode += para.ParaValue;
                                    #endregion
                                }
                            }
                            items.ForEach(f => { f.BarCode = barcode; f.IsPrep = false; });
                        }
                        else 
                        {
                            items.ForEach(f => f.IsPrep = true);
                        }
                        #endregion
                    }
                    else if(barcodect == 1)
                    {
                        //采样申请单号作为条码号
                        items.ForEach(f => { f.BarCode = f.OrderFormNo;f.IsPrep = false; });
                    }
                    #endregion
                }
                #endregion
            }

            return originalSampleGroups;
            #endregion
        }
        /// <summary>
        /// 获取最大流水号
        /// </summary>
        /// <param name="BmsTypeID"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public string Edit_GetMaxNo(long BmsTypeID, string BmsType,bool IsGlobal, int length)
        {
            IList<LBTGetMaxNo> lBTGetMaxNos = IDLBTGetMaxNoDao.GetListByHQL("BmsTypeID=" + BmsTypeID);
            string day = DateTime.Now.ToString("yyyyMMdd");
            string MaxID = "";
            if (lBTGetMaxNos.Count > 0)
            {
                
                LBTGetMaxNo lBTGetMaxNo = lBTGetMaxNos[0];
                //全局流水号需要一直增加不需要重置
                if (lBTGetMaxNo.BmsDate.ToString("yyyyMMdd") != day && IsGlobal == false)
                {
                    lBTGetMaxNo.MaxID = "1";
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
                lBTGetMaxNo.MaxID = "1";
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
        /// <summary>
        /// 保存条码样本信息以及修改医嘱单状态
        /// </summary>
        /// <param name="bParas"></param>
        /// <param name="originalSampleGroups"></param>
        /// <param name="originalItemData"></param>
        /// <param name="nextindex"></param>
        /// <returns></returns>
        public List<LisBarCodeFormVo> AddBarCodeFormAndEditOrderForm(List<BPara> bParas, List<List<GroupingOrderItemVo>> originalSampleGroups, List<List<LisOrderFormItemVo>> originalItemData, List<LBSamplingItem> originalGroupData,long userid,string username, int nextindex) 
        {          
            List<LisBarCodeFormVo> lisBarCodeFormVos = new List<LisBarCodeFormVo>();
            List<LisOrderForm> lisOrderForms = new List<LisOrderForm>();//医嘱单信息
            List<LisPatient> lisPatients = new List<LisPatient>();//病人就诊信息
            List<long> orderformids = new List<long>();//医嘱单ID
            List<long> patids = new List<long>();//病人就诊信息ID
            foreach (var item in originalItemData)
            {
                item.ForEach(f => { orderformids.Add(f.OrderFormID); patids.Add(f.PatID); });
            }
            lisOrderForms  = DBDao.GetListByHQL("Id in ("+string.Join(",", orderformids.Distinct()) +")").ToList();//医嘱单信息
            lisPatients = IDLisPatientDao.GetListByHQL("Id in (" + string.Join(",", patids.Distinct()) + ")").ToList();//病人信息
            for (int i = 0; i < originalSampleGroups.Count; i++)
            {
                var specialItems = originalSampleGroups[i].Where(w=>w.SampleGroupingType == SampleGroupingType.无采样组.Key || w.SampleGroupingType == SampleGroupingType.无默认采样组.Key);
                if (specialItems.Count() > 0)
                {
                    //拼接无采样组和没有默认采样组的数据
                    foreach (var item in originalSampleGroups[i])
                    {
                        LisBarCodeFormVo lisBarCodeFormVo = new LisBarCodeFormVo();
                        LisBarCodeForm lisBarCodeForm = new LisBarCodeForm();
                        lisBarCodeForm.LisOrderForm = lisOrderForms.Where(w => w.Id == item.OrderFormID).First();
                        lisBarCodeForm.LisPatient = lisPatients.Where(w => w.Id == lisBarCodeForm.LisOrderForm.PatID).First();
                        lisBarCodeForm.ParItemCName = item.ItemCName;
                        lisBarCodeFormVo.LisBarCodeForm = lisBarCodeForm;
                        lisBarCodeFormVo.SampleGroupingType = item.SampleGroupingType;
                        lisBarCodeFormVo.ItemId = item.OrdersItemID.ToString();
                        lisBarCodeFormVos.Add(lisBarCodeFormVo);
                    }
                }
                else 
                {                   
                    //处理分管完成的数据
                    LisOrderForm orderformentity = new LisOrderForm();
                    LisPatient lispatiententity = new LisPatient();
                    orderformentity = lisOrderForms.Where(w => w.Id == originalSampleGroups[i][0].OrderFormID).First();
                    lispatiententity = lisPatients.Where(w => w.Id == orderformentity.PatID).First();
                    var  sampleinggroup =  originalGroupData.Where(w => w.LBSamplingGroup.Id == originalSampleGroups[i][0].SamplingGroupID).First();
                    LisBarCodeFormVo lisBarCodeFormVo = new LisBarCodeFormVo();//返回到前台的VO
                    #region BarCodeForm和BarCodeItem表数据新增,更新医嘱表信息
                    LisBarCodeForm lisBarCodeForm = new LisBarCodeForm();//新增样本单的实体
                    lisBarCodeForm.LisOrderForm = new LisOrderForm() { Id = orderformentity.Id, DataTimeStamp = orderformentity.DataTimeStamp }; 
                    lisBarCodeForm.LisPatient = new LisPatient() { Id = lispatiententity.Id, DataTimeStamp = lispatiententity.DataTimeStamp };
                    lisBarCodeForm.BarCode = originalSampleGroups[i][0].BarCode;
                    lisBarCodeForm.OrderExecTime = orderformentity.OrderExecTime;
                    lisBarCodeForm.HospitalID = orderformentity.HospitalID;
                    lisBarCodeForm.ExecDeptID = orderformentity.ExecDeptID;
                    lisBarCodeForm.DestinationID = orderformentity.DestinationID;
                    lisBarCodeForm.PrintTimes = orderformentity.PrintTimes;
                    lisBarCodeForm.SamplingGroupID = originalSampleGroups[i][0].SamplingGroupID;
                    lisBarCodeForm.Color = sampleinggroup.LBSamplingGroup.LBTcuvete.Color;
                    lisBarCodeForm.SampleTypeID = originalSampleGroups[i][0].SampleTypeID;
                    lisBarCodeForm.IsUrgent = orderformentity.IsUrgent;
                    lisBarCodeForm.SampleCap = double.Parse(sampleinggroup.LBSamplingGroup.LBTcuvete.Capacity.ToString());
                    if (!string.IsNullOrEmpty(originalSampleGroups[i][0].CollectPart)) 
                    {
                        lisBarCodeForm.CollectPart = originalSampleGroups[i][0].CollectPart;
                    }
                    lisBarCodeForm.ClientID = orderformentity.ClientID;
                    lisBarCodeForm.ColorValue = sampleinggroup.LBSamplingGroup.LBTcuvete.ColorValue;
                    lisBarCodeForm.BarCodeStatusID = long.Parse(BarCodeStatus.生成条码.Value.Code);
                    lisBarCodeForm.BarCodeCurrentStatus = BarCodeStatus.生成条码.Value.Name;
                    lisBarCodeForm.IsPrep = originalSampleGroups[i][0].IsPrep ?1:0;
                    List<string> itemname = new List<string>();//样本单项目名称
                    List<LisBarCodeItem> lisBarCodeItems = new List<LisBarCodeItem>();//新增采样项目实体集合
                    lisBarCodeForm.ChargeFlag = 1;
                    float itemcharge = 0;
                    foreach (var item in originalSampleGroups[i])
                    {
                        itemname.Add(item.ItemCName);
                        itemcharge = itemcharge + item.Charge;
                        if (item.IsCheckFee == 0) {
                            lisBarCodeForm.ChargeFlag = 0;
                        }
                        var orderitementity= IDLisOrderItemDao.Get(item.OrderItemID);
                        LisBarCodeItem lisBarCodeItem = new LisBarCodeItem();
                        lisBarCodeItem.PartitionDate = item.PartitionDate;
                        lisBarCodeItem.LisBarCodeForm = lisBarCodeForm;
                        lisBarCodeItem.LisOrderItem = new LisOrderItem() { Id = orderitementity.Id, DataTimeStamp = orderitementity.DataTimeStamp };
                        lisBarCodeItem.BarCodesItemID = item.OrdersItemID;
                        if (item.IsGroupItem)
                        {
                            lisBarCodeItem.OrdersItemID = item.GroupItemID;
                            lisBarCodeItem.IsItemSplitReceive = 1;
                        }
                        else 
                        {
                            lisBarCodeItem.OrdersItemID = item.OrdersItemID;
                        }
                        lisBarCodeItem.BarCodeItemFlag = 0;
                        lisBarCodeItem.ItemStatusID = long.Parse(BarCodeStatus.生成条码.Value.Code);
                        lisBarCodeItems.Add(lisBarCodeItem);
                    }
                    lisBarCodeForm.Charge = itemcharge;
                    lisBarCodeForm.ParItemCName = string.Join(",", itemname);
                    IBLisBarCodeForm.Entity = lisBarCodeForm;
                    if (IBLisBarCodeForm.Add()) {//新增采样样本单
                        IBLisOperate.AddLisOperate(lisBarCodeForm, OrderFromOperateType.保存采样样本单.Value, "保存采样样本单", SysCookieValue);
                        foreach (var barcodeitem in lisBarCodeItems)
                        {
                            IBLisBarCodeItem.Entity = barcodeitem;
                            bool isok = IBLisBarCodeItem.Add();//新增采样项目
                            if (isok) {
                                IBLisOperate.AddLisOperate(barcodeitem, OrderFromOperateType.保存采样项目.Value, "保存采样项目", SysCookieValue);
                                IDLisOrderItemDao.UpdateByHql("update LisOrderItem set OrderItemExecFlag=1,ItemStatusID=1 where  Id=" + barcodeitem.LisOrderItem.Id);
                            }
                        }
                    }
                    #endregion
                    #region 返回数据拼接
                    lisBarCodeFormVo.LisBarCodeForm = lisBarCodeForm;
                    lisBarCodeFormVo.IsPrep = originalSampleGroups[i][0].IsPrep;
                    lisBarCodeFormVo.LisBarCodeForm.LisOrderForm = orderformentity;
                    lisBarCodeFormVo.LisBarCodeForm.LisPatient = lispatiententity;
                    lisBarCodeFormVo.PreInfo = sampleinggroup.LBSamplingGroup.PrepInfo;
                    //平台部门信息获取
                    if (lisBarCodeFormVo.LisBarCodeForm.HospitalID != null && lisBarCodeFormVo.LisBarCodeForm.HospitalID != 0)
                    {
                        IList<ZhiFang.Entity.LIIP.HREmpIdentity> hospital = LIIPHelp.RBAC_UDTO_SearchHRDeptIdentityByHQL("hrdeptidentity.TSysCode='" + DeptSystemType.院区.Key + "' and hrdeptidentity.SystemCode='ZF_PRE'").toList<ZhiFang.Entity.LIIP.HREmpIdentity>();
                        if (hospital.Count > 0)
                        {
                            var hREmps = hospital.Where(w => w.Id == lisBarCodeFormVo.LisBarCodeForm.HospitalID);
                            if (hREmps.Count() > 0)
                            {
                                lisBarCodeFormVo.HospitalName = hREmps.First().CName;
                            }
                        }
                    }
                    if (lisBarCodeFormVo.LisBarCodeForm.ClientID != null && lisBarCodeFormVo.LisBarCodeForm.ClientID != 0) {
                        IList<ZhiFang.Entity.LIIP.HREmpIdentity> client = LIIPHelp.RBAC_UDTO_SearchHRDeptIdentityByHQL("hrdeptidentity.TSysCode='" + DeptSystemType.送检客户.Key + "' and hrdeptidentity.SystemCode='ZF_PRE'").toList<ZhiFang.Entity.LIIP.HREmpIdentity>();
                        if (client.Count > 0)
                        {
                            var hREmps = client.Where(w => w.Id == lisBarCodeFormVo.LisBarCodeForm.ClientID);
                            if (hREmps.Count() > 0)
                            {
                                lisBarCodeFormVo.ClientName = hREmps.First().CName;
                            }
                        }
                    }
                    if (lisBarCodeFormVo.LisBarCodeForm.ExecDeptID != null && lisBarCodeFormVo.LisBarCodeForm.ExecDeptID != 0)
                    {
                        IList<ZhiFang.Entity.LIIP.HREmpIdentity> execdept = LIIPHelp.RBAC_UDTO_SearchHRDeptIdentityByHQL("hrdeptidentity.TSysCode='" + DeptSystemType.执行科室.Key + "' and hrdeptidentity.SystemCode='ZF_PRE'").toList<ZhiFang.Entity.LIIP.HREmpIdentity>();
                        if (execdept.Count > 0)
                        {
                            var hREmps = execdept.Where(w => w.Id == lisBarCodeFormVo.LisBarCodeForm.ExecDeptID);
                            if (hREmps.Count() > 0)
                            {
                                lisBarCodeFormVo.ExecDeptName = hREmps.First().CName;
                            }
                        }
                    }
                    if (lisBarCodeFormVo.LisBarCodeForm.DestinationID != null && lisBarCodeFormVo.LisBarCodeForm.DestinationID != 0) {
                        LBDestination  lBDestination = IDLBDestinationDao.Get(long.Parse(lisBarCodeFormVo.LisBarCodeForm.DestinationID.ToString()));
                        lisBarCodeFormVo.DestinationName = lBDestination.CName;
                    }
                    if (lisBarCodeFormVo.LisBarCodeForm.SampleTypeID != null && lisBarCodeFormVo.LisBarCodeForm.SampleTypeID != 0)
                    {
                        LBSampleType lBSampleType = IDLBSampleTypeDao.Get(long.Parse(lisBarCodeFormVo.LisBarCodeForm.SampleTypeID.ToString()));
                        lisBarCodeFormVo.SampleTypeName = lBSampleType.CName;
                    }
                    lisBarCodeFormVo.OrderTypeName = "";//暂不知道从哪取
                    lisBarCodeFormVo.SampleGroupingType = originalSampleGroups[i][0].SampleGroupingType;
                    lisBarCodeFormVo.SamplingGroupName = originalGroupData.Where(w => w.LBSamplingGroup.Id == lisBarCodeFormVo.LisBarCodeForm.SamplingGroupID).First().LBSamplingGroup.CName;
                    lisBarCodeFormVos.Add(lisBarCodeFormVo);
                    #endregion
                }
            }
            return lisBarCodeFormVos;
        }
        /// <summary>
        /// 医嘱单执行标记判断更新
        /// </summary>
        /// <param name="originalItemData"></param>
        public void Edit_OrderFormExecFlag(List<List<LisOrderFormItemVo>> originalItemData) {
            //判断医嘱项目执行标记都为1时，更新医嘱单的执行标记
            List<long> orderformids = new List<long>();//医嘱单ID
            foreach (var item in originalItemData)
            {
                item.ForEach(f => { if (!orderformids.Contains(f.OrderFormID)) { orderformids.Add(f.OrderFormID); }; });
            }
            foreach (var id in orderformids)
            {
                string where = "OrderItemExecFlag = 0 and IsCancelled = 0 and OrderFormID = " + id;
                int  count = IDLisOrderItemDao.GetListCountByHQL(where);
                if (count == 0) {
                    DBDao.UpdateByHql("update LisOrderForm set OrderExecFlag=1 where  Id=" + id);
                }
            }
        }
        public List<LisBarCodeFormVo> ConvertLisBarCodeFormVo(List<LisBarCodeForm> lisBarCodeForms) {
            List<LisBarCodeFormVo> lisBarCodeFormVos = new List<LisBarCodeFormVo>();
            if (lisBarCodeForms.Count == 0) {
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
                var lBSamplingGroup= IDLBSamplingGroupDao.Get(long.Parse(form.SamplingGroupID.ToString()));
                lisBarCodeFormvo.SamplingGroupName = lBSamplingGroup.CName;
                lisBarCodeFormvo.PreInfo = lBSamplingGroup.PrepInfo;
                lisBarCodeFormVos.Add(lisBarCodeFormvo);
            }
            return lisBarCodeFormVos;
        }

        /// <summary>
        /// 医嘱单、病人就诊信息、医嘱项目查询
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public BaseResultDataValue GetHISOrderInfo(long nodetype, string barcode)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            List<LisOrderFormPatientOrderIitemVO> lisOrders = new List<LisOrderFormPatientOrderIitemVO>();
            IList<LisBarCodeForm> lisBarCodeForms = IBLisBarCodeForm.SearchListByHQL("BarCode='" + barcode + "'");
            List<long> ids = new List<long>();
            lisBarCodeForms.ToList().ForEach(f => ids.Add(f.Id));
            IList<LisBarCodeItem> lisBarCodeItems = IBLisBarCodeItem.SearchListByHQL("LisBarCodeForm.Id in (" + string.Join(",", ids) + ")");
            foreach (var form in lisBarCodeForms)
            {
                var  lisbarcodeitemsbyformid = lisBarCodeItems.Where(a=> a.LisBarCodeForm.Id == form.Id);
                if (lisbarcodeitemsbyformid != null  && lisbarcodeitemsbyformid.Count() > 0) {
                    foreach (var item in lisbarcodeitemsbyformid)
                    {
                        LisOrderFormPatientOrderIitemVO entity = new LisOrderFormPatientOrderIitemVO();
                        entity.BarCode = form.BarCode;
                        entity.OrderFormNo = form.LisOrderForm.OrderFormNo;
                        entity.OrderTime = form.LisOrderForm.OrderTime;
                        entity.OrderExecTime = form.LisOrderForm.OrderExecTime;
                        entity.CName = form.LisPatient.CName;
                        entity.GenderName = form.LisPatient.GenderName;
                        entity.Age = form.LisPatient.Age;
                        entity.PatNo = form.LisPatient.PatNo;
                        entity.DistrictName = form.LisPatient.DistrictName;
                        entity.WardName = form.LisPatient.WardName;
                        entity.Bed = form.LisPatient.Bed;
                        entity.BarCodesItemName = IDLBItemDao.Get(long.Parse(item.BarCodesItemID.ToString())).CName;
                        entity.OrdersItemName = IDLBItemDao.Get(long.Parse(item.OrdersItemID.ToString())).CName;
                        lisOrders.Add(entity);
                    }
                }
            }
            baseResultDataValue.success = true;
            baseResultDataValue.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(lisOrders.GroupBy(a => a.BarCode));
            return baseResultDataValue;
        }
        /// <summary>
        /// 重新分组
        /// </summary>
        /// <param name="barcode"></param>
        /// <param name="nodetype"></param>
        /// <param name="receiveType"></param>
        /// <param name="value"></param>
        /// <param name="days"></param>
        /// <param name="fields"></param>
        /// <param name="isPlanish"></param>
        /// <param name="nextindex"></param>
        /// <returns></returns>
        public BaseResultDataValue GetSampleAndANewGrouping(string barcode, long nodetype, string receiveType, string value, int days,string userid,string username, string fields,string labid, bool isPlanish, int nextindex)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            #region 入参验证与获取配置参数
            if (string.IsNullOrEmpty(receiveType) || string.IsNullOrEmpty(value))
            {
                brdv.success = false;
                brdv.ErrorInfo = "核收条件不可为空！";
                return brdv;
            }
            if (days == 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "样本过滤天数不可为0！";
                return brdv;
            }
            if (!string.IsNullOrEmpty(barcode))
            {
                var baseResult = IBLisBarCodeForm.Edit_PreBarCodeInvalid(0, barcode, true,userid,username);//作废条码
                if (!baseResult.success)
                {
                    return baseResult;
                }
            }
            
            //获取参数配置
            List<BPara> bParas = IBBParaItem.SelPreBParas(nodetype, "", Pre_AllModules.样本条码.Value.DefaultValue, userid, username);
            #region 验卡
            var checkcardurl = bParas.Where(a => a.ParaNo == "Pre_OrderBarCode_DefaultPara_0027").First();
            if (!string.IsNullOrEmpty(checkcardurl.ParaValue))
            {
                value = CheckCard(nodetype, receiveType, value).ResultDataValue;
            }
            #endregion
            #endregion
            #region 医嘱信息查询并医嘱分组
            //查询医嘱单病人信息以及医嘱项目
            List<LBSamplingItem> lBSamplingItems = new List<LBSamplingItem>();//所有项目所属的采样组
            List<LisBarCodeForm> lisBarCodeForms = new List<LisBarCodeForm>();
            List<List<LisOrderFormItemVo>> lisOrderFormItemVos = GetOrderFormItemGroup(bParas, receiveType, value, days, nextindex,labid, out lBSamplingItems, out lisBarCodeForms);
            if (lisOrderFormItemVos == null || lisOrderFormItemVos.Count == 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "未查询到医嘱项目信息！";
                return brdv;
            }
            #endregion
            List<LisBarCodeFormVo> barCodeFormVos = new List<LisBarCodeFormVo>();
            barCodeFormVos = ConvertLisBarCodeFormVo(lisBarCodeForms);
            if (lisOrderFormItemVos != null && lisOrderFormItemVos.Count > 0)
            {
                #region 采样分管
                List<List<LisOrderFormItemVo>> samplingGroupItemResult = new List<List<LisOrderFormItemVo>>();//原始医嘱信息
                                                                                                              //单采样组分组
                List<List<GroupingOrderItemVo>> allSamplingGroups = OrderItemGroupByOneSamplingGroup(bParas, lisOrderFormItemVos, lBSamplingItems, nextindex, out samplingGroupItemResult);
                //多采样分组
                allSamplingGroups = OrderItemGroupByMoreSamplingGroup(bParas, allSamplingGroups, samplingGroupItemResult, lBSamplingItems, nextindex, out samplingGroupItemResult);
                #endregion
                #region 条码生成
                allSamplingGroups = CreateBarCode(bParas, allSamplingGroups, samplingGroupItemResult, lBSamplingItems, nextindex);
                #endregion
                #region BarCode表存储和标志更新
                List<LisBarCodeFormVo> lisBarCodeFormVos = AddBarCodeFormAndEditOrderForm(bParas, allSamplingGroups, samplingGroupItemResult, lBSamplingItems,long.Parse(userid),username, nextindex);
                Edit_OrderFormExecFlag(samplingGroupItemResult);

                barCodeFormVos.AddRange(lisBarCodeFormVos);
                #endregion
            }
            #region 数据压平
            EntityList<LisBarCodeFormVo> entityList = new EntityList<LisBarCodeFormVo>();
            entityList.count = barCodeFormVos.Count;
            entityList.list = barCodeFormVos;
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
            #endregion
            return brdv;
        }
        /// <summary>
        /// 已分组数据查询(已打印或未打印)
        /// </summary>
        /// <param name="where"></param>
        /// <param name="printStatus"></param>
        /// <param name="fields"></param>
        /// <param name="isPlanish"></param>
        /// <returns></returns>
        public BaseResultDataValue GetHaveToPrintBarCodeForm(string barcode, string where, bool? printStatus, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (string.IsNullOrEmpty(where) && string.IsNullOrEmpty(barcode))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询条件不可为空！";
                return baseResultDataValue;
            }
            List<long> ids = new List<long>();
            if (!string.IsNullOrEmpty(where)) {
                var  orderformids = (this.DBDao as IDLisOrderFormDao).GetOrderFormList("lisorderform.OrderFormID", where, out string bwhere);
                if (orderformids != null) {
                    foreach (var formid in orderformids)
                    {
                        long id = long.Parse(formid.ToString());
                        if (!ids.Contains(id))
                            ids.Add(id);
                    }
                }
            }
            if (ids.Count < 1 && string.IsNullOrEmpty(barcode)) {
                baseResultDataValue.success = true;
                return baseResultDataValue;
            }
            string barcodewhere = "BarCodeFlag != -1";
            if (ids.Count > 0) {
                barcodewhere += " and LisOrderForm.Id in (" + string.Join(", ", ids) + ")";
            }
            if (!string.IsNullOrEmpty(barcode)) {
                barcodewhere += " and BarCode = '" + barcode + "'";
            }
            if (printStatus != null && printStatus == true)
            {
                barcodewhere += " and IsAffirm = 1";
            }
            else if (printStatus != null && printStatus == false)
            {
                barcodewhere += " and IsAffirm = 0";
            }
            List<LisBarCodeForm> lisBarCodeFormarr = IBLisBarCodeForm.SearchListByHQL(barcodewhere).ToList();
            var barCodeFormVos = ConvertLisBarCodeFormVo(lisBarCodeFormarr);
            EntityList<LisBarCodeFormVo> entityList = new EntityList<LisBarCodeFormVo>();
            entityList.count = barCodeFormVos.Count;
            entityList.list = barCodeFormVos;
            ParseObjectProperty pop = new ParseObjectProperty(fields);
            try
            {
                baseResultDataValue.success = true;
                if (isPlanish)
                {
                    baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisBarCodeFormVo>(entityList);
                }
                else
                {
                    baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
            }
            
            return baseResultDataValue;
        }
        #endregion
        #region 调用HIS接口核收
        public BaseResultDataValue GetHISCheckDataAndWriteLocalDB(string inputType, string inputValue, string StartDate, string EndDate, string SickTypeName, string HospNo, string OrgNo, string url,long userid,string username) {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            BaseResultDataValue hIsresult = HISHelp.GetHisOrderFormAndOrderItem(inputType, inputValue, StartDate, EndDate, SickTypeName, HospNo, OrgNo, url);
            if (!hIsresult.success)
            {
                return hIsresult;
            }
            else if (hIsresult.success)
            {
                HISInterfaceHISOrderFromVO hISInterfaceHISOrderFromVO = new HISInterfaceHISOrderFromVO();
                hISInterfaceHISOrderFromVO = Newtonsoft.Json.JsonConvert.DeserializeObject<HISInterfaceHISOrderFromVO>(hIsresult.ResultDataValue);
                if (hISInterfaceHISOrderFromVO.success && hISInterfaceHISOrderFromVO.LisPatientList.Count > 0)
                {
                    bool DataFormatISOK = true;//数据格式是否正确
                    var hISInterfaceHISOrderFromVo = IBLBDicCodeLink.HISOrderFormDataDicContrast(hISInterfaceHISOrderFromVO,out DataFormatISOK);
                    if (DataFormatISOK)
                    {
                        if (this.Add_HISCheckDataWriteLocalDB(hISInterfaceHISOrderFromVo,userid,username))
                        {
                            baseResultDataValue.success = true;
                        }
                        else 
                        {
                            baseResultDataValue.success = false;
                            baseResultDataValue.ErrorInfo = "HIS核收数据写入本地失败，请检查返回数据格式！";
                        }
                    }
                    else
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "HIS核收返回数据不正确请检查是否包含医嘱单或医嘱单项目！";
                        return baseResultDataValue;
                    }
                }
                else if (!hISInterfaceHISOrderFromVO.success)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = hISInterfaceHISOrderFromVO.ErrorInfo;
                    return baseResultDataValue;
                }
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// HIS核收数据写入医嘱表与病人就诊信息表
        /// </summary>
        /// <param name="hISInterfaceVO"></param>
        /// <returns></returns>
        public bool Add_HISCheckDataWriteLocalDB(HISInterfaceHISOrderFromVO hISInterfaceVO,long userid,string username) {
            if (hISInterfaceVO.LisPatientList.Count > 0) {
                foreach (var lisPatientVO in hISInterfaceVO.LisPatientList)
                {
                    LisPatient lisPatient = ClassMapperHelp.GetMapper<LisPatient, LisPatientVO>(lisPatientVO);
                    lisPatient.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                    lisPatient.DataAddTime = DateTime.Now;

                    if (IDLisPatientDao.Save(lisPatient))
                    {
                        foreach (var lisOrderFormVO in lisPatientVO.LisOrderFormList)
                        {
                            LisOrderForm lisOrderForm = ClassMapperHelp.GetMapper<LisOrderForm, LisOrderFormVO>(lisOrderFormVO);
                            lisOrderForm.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                            lisOrderForm.DataAddTime = DateTime.Now;
                            lisOrderForm.OrderTime = DateTime.Now;
                            lisOrderForm.PatID = lisPatient.Id;
                            lisOrderForm.PartitionDate = DateTime.Now;
                            if (DBDao.Save(lisOrderForm))
                            {
                                foreach (var lisOrderItemVO in lisOrderFormVO.LisOrderItemList)
                                {
                                    LisOrderItem lisOrderItem = ClassMapperHelp.GetMapper<LisOrderItem, LisOrderItemVO>(lisOrderItemVO);
                                    lisOrderItem.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                                    lisOrderItem.OrderFormID = lisOrderForm.Id;
                                    lisOrderItem.DataAddTime = DateTime.Now;
                                    lisOrderItem.PartitionDate = DateTime.Now;
                                    if (!IDLisOrderItemDao.Save(lisOrderItem)) {
                                        return false;
                                    }
                                }
                                IBLisOperate.AddLisOperate(lisOrderForm, OrderFromOperateType.保存医嘱单.Value, "保存医嘱单", SysCookieValue);
                            }
                            else 
                            {
                                return false;
                            }
                        }
                    }
                    else 
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion

    }
}