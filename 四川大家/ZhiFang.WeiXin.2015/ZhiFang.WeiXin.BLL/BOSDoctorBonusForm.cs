using ZhiFang.BLL.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.WeiXin.IDAO;
using ZhiFang.WeiXin.Entity;
using ZhiFang.WeiXin.Entity.ViewObject.Response;
using ZhiFang.WeiXin.DAO.NHB;
using ZhiFang.Entity.Base;
using ZhiFang.WeiXin.IBLL;
using System.Data;
using System.Reflection;
using ZhiFang.WeiXin.Common;
using System.IO;

namespace ZhiFang.WeiXin.BLL
{
    /// <summary>
    ///
    /// </summary>
    public class BOSDoctorBonusForm : BaseBLL<OSDoctorBonusForm>, ZhiFang.WeiXin.IBLL.IBOSDoctorBonusForm
    {
        IDOSUserConsumerFormDao IDOSUserConsumerFormDao { get; set; }
        IDOSDoctorBonusDao IDOSDoctorBonusDao { get; set; }
        IDOSDoctorBonusOperationDao IDOSDoctorBonusOperationDao { get; set; }
        IDOSDoctorBonusAttachmentDao IDOSDoctorBonusAttachmentDao { get; set; }
        IDBDoctorAccountDao IDBDoctorAccountDao { get; set; }
        IDBWeiXinAccountDao IDBWeiXinAccountDao { get; set; }
        IBBParameter IBBParameter { get; set; }

        SysWeiXinPayToUser.PayToUser PayToUserfunc;
        public SysWeiXinPayToUser.PayToUser PayToUserFunc
        {
            get
            {
                return PayToUserfunc;
            }
            set
            {
                PayToUserfunc = value;
            }
        }/// <summary>
         /// 通过结算周期获取医生奖金结算单及结算记录明细的结算申请数据
         /// 当前结算申请月的上个月未进行过结算申请的数据(用户消费单记录的状态为消费成功并且用户消费单记录的医生奖金结算记录ID为空)也要获取
         /// </summary>
         /// <param name="bonusFormRound"></param>
         /// <returns></returns>
        public OSDoctorBonusApply SearchSettlementApplyInfoByBonusFormRound(string bonusFormRound, long empID, string empName)
        {
            if (String.IsNullOrEmpty(bonusFormRound)) return null;
            OSDoctorBonusApply applyEntity = new OSDoctorBonusApply();
            applyEntity.BonusFormRound = bonusFormRound;
            IList<OSDoctorBonusForm> tempList = DBDao.GetListByHQL("BonusFormRound='" + bonusFormRound + "'");
            if (tempList.Count > 0)
            {
                applyEntity.IsSettlement = true;
                applyEntity.OSDoctorBonusForm = tempList[0];
            }
            else
            {
                applyEntity.IsSettlement = false;
                IList<OSDoctorBonus> bonusApplyList = new List<OSDoctorBonus>();
                #region 当前结算申请月的数据查询条件
                DateTime curFirstDate = Convert.ToDateTime(bonusFormRound + "-01");
                int curDays = DateTime.DaysInMonth(curFirstDate.Year, curFirstDate.Month);
                string curStartDate = curFirstDate.ToString("yyyy-MM-dd");
                string curEndDate = curFirstDate.AddDays(curDays - 1).ToString("yyyy-MM-dd");
                string curHqlStr = "(DataAddTime>='" + curStartDate + "' and DataAddTime<='" + curEndDate + " 23:59:59')";
                #endregion
                #region 当前结算申请月的上一月未进行过结算申请的查询条件
                //取得结算月的上个月最后一天 
                DateTime perLastDate = curFirstDate.AddDays(-1);
                string perStartDate = perLastDate.ToString("yyyy-MM-01");
                //取得结算月的上个月最后一天 
                string perEndDate = perLastDate.ToString("yyyy-MM-dd");
                string perHqlStr = "((OSDoctorBonusID is null or OSDoctorBonusID='') and DataAddTime>='" + perStartDate + "' and DataAddTime<='" + perEndDate + " 23:59:59')";
                #endregion
                string sort = " order by DoctorAccountID asc,DataAddTime asc ";
                string hqlStr = "(Status = " + OSUserConsumerFormStatus.消费成功.Key.ToString() + " and IsUse = 1) and (" + perHqlStr + " or " + curHqlStr + ")" + sort;
                ZhiFang.Common.Log.Log.Debug("医生奖金结算" + bonusFormRound + ",hqlStr:" + hqlStr);
                IList<OSUserConsumerForm> tempConsumerList = IDOSUserConsumerFormDao.GetListByHQL(hqlStr);
                if (tempConsumerList.Count > 0)
                {
                    //系统默认的医生的咨询费比率
                    string sysBonusPercent = (string)IBBParameter.GetCache(BParameterParaNoClass.BonusPercent.Key.ToString());
                    OSDoctorBonusForm applyBonusForm = new OSDoctorBonusForm();
                    //yyMMddHHmmssfff+结算年月
                    applyBonusForm.IsUse = true;
                    applyBonusForm.Status = long.Parse(OSDoctorBonusFormStatus.暂存.Key);
                    applyBonusForm.BonusFormRound = bonusFormRound;
                    applyBonusForm.BonusApplyManID = empID;
                    applyBonusForm.BonusApplyManName = empName;
                    applyBonusForm.BonusApplytTime = DateTime.Now;
                    //开单数量
                    applyBonusForm.OrderFormCount = tempConsumerList.Count;
                    //总结算金额
                    double? totalAmount = 0;
                    //总开单金额
                    double? totalOrderFormAmount = 0;

                    #region 医生账户信息ID滤重
                    StringBuilder strbweiXin = new StringBuilder();
                    IList<BWeiXinAccount> weiXinAccountList = new List<BWeiXinAccount>();

                    StringBuilder strbDctor = new StringBuilder();
                    IList<BDoctorAccount> doctorAccountList = new List<BDoctorAccount>();
                    var accountIDLists = tempConsumerList.GroupBy(p => new { p.DoctorAccountID, p.WeiXinUserID }).Where(x => x.Count() > 0).ToList();
                    foreach (var accountID in accountIDLists)
                    {
                        if (accountID.Key.DoctorAccountID.HasValue && !strbDctor.ToString().Contains(accountID.Key.DoctorAccountID + ","))
                            strbDctor.Append(accountID.Key.DoctorAccountID + ",");

                        if (accountID.Key.WeiXinUserID.HasValue && !strbweiXin.ToString().Contains(accountID.Key.WeiXinUserID + ","))
                            strbweiXin.Append(accountID.Key.WeiXinUserID + ",");
                    }
                    if (!String.IsNullOrEmpty(strbDctor.ToString()))
                    {
                        doctorAccountList = IDBDoctorAccountDao.GetListByHQL("Id in(" + strbDctor.ToString().TrimEnd(',') + ")");
                    }

                    if (!String.IsNullOrEmpty(strbweiXin.ToString()))
                    {
                        weiXinAccountList = IDBWeiXinAccountDao.GetListByHQL("Id in(" + strbweiXin.ToString().TrimEnd(',') + ")");
                    }
                    #endregion
                    #region 每一个医生帐号在该自然月的用户消费单信息处理
                    //医生账户信息ID分组
                    var doctorAccountIDList = tempConsumerList.GroupBy(p => p.DoctorAccountID);
                    foreach (var consumerFormList in doctorAccountIDList)
                    {
                        OSDoctorBonus bonus = new OSDoctorBonus();
                        bonus.OSDoctorBonusFormID = applyBonusForm.Id;
                        bonus.Status = long.Parse(OSDoctorBonusFormStatus.暂存.Key);
                        //开单数量
                        bonus.OrderFormCount = consumerFormList.Count();
                        bonus.BonusFormRound = applyBonusForm.BonusFormRound;
                        //结算单号
                        bonus.BonusFormCode = "";// this.CreateDetailsBonusFormCode(applyBonusForm.BonusFormCode, consumerFormList.Key.Value.ToString());
                        bonus.DoctorAccountID = consumerFormList.Key;
                        bonus.DoctorName = consumerFormList.ElementAt(0).DoctorName;
                        if (consumerFormList.ElementAt(0).WeiXinUserID.HasValue)
                        {
                            bonus.WeiXinUserID = consumerFormList.ElementAt(0).WeiXinUserID.Value;
                            var weiXinAccountList2 = weiXinAccountList.Where(a => a.Id == bonus.WeiXinUserID.Value);
                            if (weiXinAccountList2.Count() > 0)
                            {
                                bonus.MobileCode = weiXinAccountList2.ElementAt(0).MobileCode;
                                bonus.IDNumber = weiXinAccountList2.ElementAt(0).IDNumber;
                            }
                        }
                        else
                        {
                            bonus.MobileCode = "";
                            bonus.IDNumber = "";
                        }
                        var doctorList2 = doctorAccountList.Where(a => a.Id == consumerFormList.Key.Value);
                        //医生默认信息
                        if (doctorList2.Count() > 0)
                        {
                            //bonus.Percent = doctorList2.ElementAt(0).BonusPercent;
                            bonus.BankID = doctorList2.ElementAt(0).BankID;
                            bonus.BankAccount = doctorList2.ElementAt(0).BankAccount;
                            bonus.BankAddress = doctorList2.ElementAt(0).BankAddress;
                        }


                        //奖金金额
                        double? amount = 0;
                        //开单金额
                        double? orderFormAmount = 0;
                        //医生奖金结算记录的用户消费单ID字符串,在新增保存时用以更新奖金记录与用户消费单的关联
                        StringBuilder oSUCID = new StringBuilder();
                        for (int i = 0; i < consumerFormList.Count(); i++)
                        {
                            //咨询费
                            if (consumerFormList.ElementAt(i).AdvicePrice.HasValue)
                                amount = amount + consumerFormList.ElementAt(i).AdvicePrice;
                            //实际金额
                            if (consumerFormList.ElementAt(i).Price.HasValue)
                                orderFormAmount = orderFormAmount + consumerFormList.ElementAt(i).Price;
                            oSUCID.Append(consumerFormList.ElementAt(i).Id + ",");
                        }
                        bonus.OSUConsumerFormIDStr = oSUCID.ToString().TrimEnd(',');
                        bonus.Amount = Math.Round(amount.Value, 2);
                        bonus.OrderFormAmount = Math.Round(orderFormAmount.Value, 2);
                        //医生结算比率值=医生的奖金金额/(医生的开单金额*100)*10000
                        if (bonus.OrderFormAmount.HasValue && bonus.OrderFormAmount.Value > 0)
                            bonus.Percent = (bonus.Amount / (bonus.OrderFormAmount * 100)) * 10000;

                        if (!bonus.Percent.HasValue && doctorList2.Count() > 0)
                        {
                            bonus.Percent = doctorList2.ElementAt(0).BonusPercent;
                        }
                        if (!bonus.Percent.HasValue && !String.IsNullOrEmpty(sysBonusPercent))
                        {
                            //取系统参数设置的默认计算比率值
                            double sysPercent = 0;
                            if (double.TryParse(sysBonusPercent, out sysPercent))
                            {
                                bonus.Percent = sysPercent;
                            }
                        }
                        if (bonus.Percent.HasValue) bonus.Percent = Math.Round(bonus.Percent.Value, 2);
                        //2017-11-15:奖金默认发放方式修改为微信支付
                        bonus.PaymentMethod = long.Parse(OSDoctorBonusPaymentMethod.微信支付.Key);
                        bonusApplyList.Add(bonus);

                        totalAmount = totalAmount + amount;
                        totalOrderFormAmount = totalOrderFormAmount + orderFormAmount;
                    }
                    applyBonusForm.Amount = Math.Round(totalAmount.Value, 2);
                    applyBonusForm.OrderFormAmount = Math.Round(totalOrderFormAmount.Value, 2);
                    #endregion

                    //医生数量
                    applyBonusForm.DoctorCount = bonusApplyList.Count();
                    applyEntity.OSDoctorBonusForm = applyBonusForm;
                    applyEntity.OSDoctorBonusList = bonusApplyList;
                }
            }
            return applyEntity;
        }

        /// <summary>
        /// 新增医生奖金结算单申请及记录明细申请
        /// </summary>
        /// <param name="applyEntity"></param>
        /// <returns></returns>
        public BaseResultDataValue AddOSDoctorBonusFormAndDetails(OSDoctorBonusApply applyEntity, long empID, string empName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (applyEntity.OSDoctorBonusForm == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "医生奖金结算单信息(OSDoctorBonusForm)为空!";
                return brdv;
            }
            if (String.IsNullOrEmpty(applyEntity.BonusFormRound))
            {
                brdv.success = false;
                brdv.ErrorInfo = "结算周期信息为空!";
                return brdv;
            }
            IList<OSDoctorBonusForm> tempList = DBDao.GetListByHQL("BonusFormRound='" + applyEntity.BonusFormRound + "'");
            if (tempList.Count > 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "结算周期为" + applyEntity.BonusFormRound + "已存在!";
                return brdv;
            }
            try
            {
                this.Entity = applyEntity.OSDoctorBonusForm;
                //yyMMddHHmmssfff+结算年月
                this.Entity.BonusFormCode = this.CreateBonusFormCode(applyEntity.BonusFormRound);
                this.Entity.BonusFormRound = applyEntity.BonusFormRound;
                this.Entity.BonusApplyManID = empID;
                this.Entity.BonusApplyManName = empName;
                this.Entity.BonusApplytTime = DateTime.Now;
                if (this.Entity.Status.ToString() == OSDoctorBonusFormStatus.申请.Key)
                {
                    this.Entity.BonusOneReviewStartTime = DateTime.Now;
                }
                //总结算金额
                double? totalAmount = 0;
                //总开单金额
                double? totalOrderFormAmount = 0;
                if (brdv.success && applyEntity.OSDoctorBonusList != null)
                {
                    foreach (var item in applyEntity.OSDoctorBonusList)
                    {
                        if (item.Amount.HasValue)
                            totalAmount = totalAmount + item.Amount.Value;
                        if (item.OrderFormAmount.HasValue)
                            totalOrderFormAmount = totalOrderFormAmount + item.OrderFormAmount;
                    }
                    this.Entity.Amount = Math.Round(totalAmount.Value, 2);
                    this.Entity.OrderFormAmount = Math.Round(totalOrderFormAmount.Value, 2);
                }
                //处理医生数量,开单数量,结算金额,开单金额结果
                brdv.success = DBDao.Save(this.Entity);
                //brdv.success = this.Add();
                if (brdv.success && applyEntity.OSDoctorBonusList != null)
                {
                    //系统默认的医生的咨询费比率
                    string sysBonusPercent = (string)IBBParameter.GetCache(BParameterParaNoClass.BonusPercent.Key.ToString());
                    foreach (var item in applyEntity.OSDoctorBonusList)
                    {
                        if (!String.IsNullOrEmpty(item.OSUConsumerFormIDStr))
                        {

                            if (item.Amount.HasValue)
                            {
                                item.Amount = Math.Round(item.Amount.Value, 2);
                            }
                            if (item.OrderFormAmount.HasValue)
                            {
                                item.OrderFormAmount = Math.Round(item.OrderFormAmount.Value, 2);
                            }
                            if (!item.Percent.HasValue || item.Percent.Value == 0)
                            {
                                //医生结算比率值=医生的奖金金额/(医生的开单金额*100)*10000
                                if (item.OrderFormAmount.HasValue && item.OrderFormAmount.Value > 0)
                                {
                                    item.Percent = (item.Amount / (item.OrderFormAmount * 100)) * 10000;
                                    item.Percent = Math.Round(item.Percent.Value, 2);
                                }
                            }
                            if (item.Percent.HasValue == false && !String.IsNullOrEmpty(sysBonusPercent))
                            {
                                //取系统参数设置的默认计算比率值
                                double sysPercent = 0;
                                if (double.TryParse(sysBonusPercent, out sysPercent))
                                {
                                    item.Percent = Math.Round(sysPercent, 2);
                                }
                            }
                            item.OSDoctorBonusFormID = this.Entity.Id;
                            //结算单号
                            item.BonusFormCode = this.CreateDetailsBonusFormCode(this.Entity.BonusFormCode, item.DoctorAccountID.Value.ToString());
                            IDOSDoctorBonusDao.Save(item);
                            //更新用户消费单的医生奖金记录关系
                            IDOSUserConsumerFormDao.OSDoctorBonusIByIdStr(item.OSUConsumerFormIDStr.Trim(), item.Id.ToString());
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                ZhiFang.Common.Log.Log.Error("医生奖金结算申请保存失败:" + ee.StackTrace);
                brdv.ErrorInfo = "AddOSDoctorBonusFormAndDetails.Add错误！" + ee.Message;
                brdv.success = false;
            }
            if (brdv.success)
            {
                SaveAHOperation(this.Entity);
            }
            else
            {
                brdv.ErrorInfo = "AddOSDoctorBonusFormAndDetails.Add错误！";
                brdv.success = false;
            }
            return brdv;
        }
        /// <summary>
        /// 修改医生奖金结算单申请及记录明细
        /// </summary>
        /// <param name="editEntity"></param>
        /// <returns></returns>
        public BaseResultBool UpdateOSDoctorBonusFormAndDetails(OSDoctorBonusApply applyEntity, string[] tempArray, long empID, string empName)
        {
            BaseResultBool brb = new BaseResultBool();
            if (applyEntity.OSDoctorBonusForm == null)
            {
                brb.success = false;
                brb.ErrorInfo = "医生奖金结算单信息(OSDoctorBonusForm)为空";
                return brb;
            }
            OSDoctorBonusForm editEntity = applyEntity.OSDoctorBonusForm;
            //先处理医生奖金结算单状态及审核流程信息
            var tmpa = tempArray.ToList();
            //当前编辑待保存的医生奖金结算单申请保存在数据库的信息
            OSDoctorBonusForm serverEntity = new OSDoctorBonusForm();
            serverEntity = DBDao.Get(applyEntity.OSDoctorBonusForm.Id);
            List<string> tmpaUpdate = new List<string>();
            if (!StatusUpdateCheck(editEntity, serverEntity, tmpa, ref tmpaUpdate, empID, empName))
            {
                brb.ErrorInfo = "医生奖金结算单ID：" + editEntity.Id + "的状态为：" + OSDoctorBonusFormStatus.GetStatusDic()[serverEntity.Status.ToString()].Name + "！";
                ZhiFang.Common.Log.Log.Error(brb.ErrorInfo);
                brb.success = false;
                return brb;
            }
            //总结算金额
            double? totalAmount = 0;
            //StringBuilder bonusIDStr = new StringBuilder();
            if (applyEntity.OSDoctorBonusList != null)
            {
                List<string> tempList = new List<string>();
                IList<BDoctorAccount> doctorAccountList = GetBDoctorAccountList(applyEntity);

                foreach (var updateBonus in applyEntity.OSDoctorBonusList)
                {
                    totalAmount = totalAmount + updateBonus.Amount.Value;
                    tempList.Clear();

                    tempList.Add("Id=" + updateBonus.Id);
                    tempList.Add("Status=" + editEntity.Status.Value);
                    tempList.AddRange(tmpaUpdate);
                    brb = UpdateOSDoctorBonusOne(doctorAccountList, tempList, updateBonus);
                }
            }
            if (totalAmount.HasValue && totalAmount.Value > 0)
                tmpa.Add("Amount=" + totalAmount.Value);
            tempArray = tmpa.ToArray();
            brb.success = DBDao.Update(tempArray);

            if (brb.success)
            {
                SaveAHOperation(editEntity);
            }
            else
            {
                brb.ErrorInfo = "UpdateOSDoctorBonusFormAndDetails.Update错误！";
                brb.success = false;
            }
            return brb;
        }

        /// <summary>
        /// 审核流程验证处理
        /// </summary>
        /// <param name="entity">本次需要更新entity</param>
        /// <param name="serverEntity">数据库获取的entity</param>
        /// <param name="tmpa">医生奖金结算单需要更新的字段和值的集合</param>
        /// <param name="tmpaUpdate">医生奖金结算单和医生奖金结算记录共同需要更新的字段和值的集合</param>
        /// <param name="EmpID"></param>
        /// <param name="EmpName"></param>
        /// <returns></returns>
        bool StatusUpdateCheck(OSDoctorBonusForm entity, OSDoctorBonusForm serverEntity, List<string> tmpa, ref List<string> tmpaUpdate, long EmpID, string EmpName)
        {
            #region 暂存
            if (entity.Status.ToString() == OSDoctorBonusFormStatus.暂存.Key)
            {
                if (serverEntity.Status.ToString() != OSDoctorBonusFormStatus.暂存.Key && serverEntity.Status.ToString() != OSDoctorBonusFormStatus.申请.Key && serverEntity.Status.ToString() != OSDoctorBonusFormStatus.一审退回.Key)
                {
                    return false;
                }

                tmpaUpdate.Add("BonusOneReviewManID=null");
                tmpaUpdate.Add("BonusOneReviewStartTime=null");
                tmpaUpdate.Add("BonusOneReviewFinishTime=null");

                tmpa.AddRange(tmpaUpdate);
            }
            #endregion
            #region 申请
            if (entity.Status.ToString() == OSDoctorBonusFormStatus.申请.Key)
            {
                if (serverEntity.Status.ToString() != OSDoctorBonusFormStatus.暂存.Key && serverEntity.Status.ToString() != OSDoctorBonusFormStatus.一审退回.Key)
                {
                    return false;
                }
                //医生奖金结算单
                tmpa.Add("BonusApplyManID=" + EmpID);
                tmpa.Add("BonusApplyManName='" + EmpName + "'");
                tmpa.Add("BonusApplytTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                //结算处理开始时间
                tmpa.Add("BonusOneReviewStartTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("BonusOneReviewManID=null");
                tmpa.Add("BonusOneReviewManName=null");
                tmpa.Add("BonusOneReviewFinishTime=null");

                //医生奖金结算记录没有申请人信息
                tmpaUpdate.Add("BonusOneReviewStartTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpaUpdate.Add("BonusOneReviewManID=null");
                tmpaUpdate.Add("BonusOneReviewManName=null");
                tmpaUpdate.Add("BonusOneReviewFinishTime=null");
            }
            #endregion
            #region 一审通过
            if (entity.Status.ToString() == OSDoctorBonusFormStatus.一审通过.Key)
            {
                if (serverEntity.Status.ToString() != OSDoctorBonusFormStatus.申请.Key && serverEntity.Status.ToString() != OSDoctorBonusFormStatus.二审退回.Key)
                {
                    return false;
                }
                tmpaUpdate.Add("BonusOneReviewManID=" + EmpID);
                tmpaUpdate.Add("BonusOneReviewManName='" + EmpName + "'");
                //结算处理完成时间
                tmpaUpdate.Add("BonusOneReviewFinishTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                //结算审批开始时间
                tmpaUpdate.Add("BonusTwoReviewStartTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");

                tmpaUpdate.Add("BonusTwoReviewManID=null");
                tmpaUpdate.Add("BonusTwoReviewManName=null");
                tmpaUpdate.Add("BonusTwoReviewFinishTime=null");

                tmpa.AddRange(tmpaUpdate);
            }
            #endregion
            #region 一审退回
            if (entity.Status.ToString() == OSDoctorBonusFormStatus.一审退回.Key)
            {
                if (serverEntity.Status.ToString() != OSDoctorBonusFormStatus.申请.Key && serverEntity.Status.ToString() != OSDoctorBonusFormStatus.二审退回.Key)
                {
                    return false;
                }
                tmpaUpdate.Add("BonusOneReviewManID=" + EmpID);
                tmpaUpdate.Add("BonusOneReviewManName='" + EmpName + "'");
                tmpaUpdate.Add("BonusOneReviewFinishTime=null");

                //结算审批
                tmpaUpdate.Add("BonusTwoReviewManID=null");
                tmpaUpdate.Add("BonusTwoReviewManName=null");
                tmpaUpdate.Add("BonusTwoReviewStartTime=null");
                tmpaUpdate.Add("BonusTwoReviewFinishTime=null");

                tmpa.AddRange(tmpaUpdate);
            }
            #endregion
            #region 二审通过
            if (entity.Status.ToString() == OSDoctorBonusFormStatus.二审通过.Key)
            {
                if (serverEntity.Status.ToString() != OSDoctorBonusFormStatus.一审通过.Key && serverEntity.Status.ToString() != OSDoctorBonusFormStatus.检查并打款退回.Key)
                {
                    return false;
                }
                tmpaUpdate.Add("BonusTwoReviewManID=" + EmpID);
                tmpaUpdate.Add("BonusTwoReviewManName='" + EmpName + "'");
                //结算审批完成时间
                tmpaUpdate.Add("BonusTwoReviewFinishTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                //结算发放开始时间
                tmpaUpdate.Add("BonusThreeReviewStartTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");

                tmpa.AddRange(tmpaUpdate);
            }
            #endregion
            #region 二审退回
            if (entity.Status.ToString() == OSDoctorBonusFormStatus.二审退回.Key)
            {
                if (serverEntity.Status.ToString() != OSDoctorBonusFormStatus.一审通过.Key && serverEntity.Status.ToString() != OSDoctorBonusFormStatus.检查并打款退回.Key)
                {
                    return false;
                }
                tmpaUpdate.Add("BonusTwoReviewManID=" + EmpID);
                tmpaUpdate.Add("BonusTwoReviewManName='" + EmpName + "'");
                tmpaUpdate.Add("BonusTwoReviewFinishTime=null");

                //结算发放
                tmpaUpdate.Add("BonusThreeReviewManID=null");
                tmpaUpdate.Add("BonusThreeReviewManName=null");
                tmpaUpdate.Add("BonusThreeReviewStartTime=null");
                tmpaUpdate.Add("BonusThreeReviewFinishTime=null");

                tmpa.AddRange(tmpaUpdate);

            }
            #endregion
            #region 检查并打款
            if (entity.Status.ToString() == OSDoctorBonusFormStatus.检查并打款.Key)
            {
                if (serverEntity.Status.ToString() != OSDoctorBonusFormStatus.二审通过.Key && serverEntity.Status.ToString() != OSDoctorBonusFormStatus.打款异常.Key)
                {
                    return false;
                }
                //如果奖金记录里还有未打款的
                //bool result = SearchCheckIsUpdateAll(entity.Id);
                //if (result == false) return false;

                tmpaUpdate.Add("BonusThreeReviewManID=" + EmpID);
                tmpaUpdate.Add("BonusThreeReviewManName='" + EmpName + "'");
                tmpaUpdate.Add("BonusThreeReviewFinishTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");

                tmpa.AddRange(tmpaUpdate);
            }
            #endregion
            #region 打款完成
            if (entity.Status.ToString() == OSDoctorBonusFormStatus.打款完成.Key)
            {
                if (serverEntity.Status.ToString() != OSDoctorBonusFormStatus.二审通过.Key && serverEntity.Status.ToString() != OSDoctorBonusFormStatus.检查并打款.Key && serverEntity.Status.ToString() != OSDoctorBonusFormStatus.打款异常.Key)
                {
                    return false;
                }
                //如果奖金记录里还有未打款的
                bool result = SearchCheckIsUpdatePayed(entity.Id);
                if (result == false) return false;

                tmpaUpdate.Add("BonusThreeReviewManID=" + EmpID);
                tmpaUpdate.Add("BonusThreeReviewManName='" + EmpName + "'");
                tmpaUpdate.Add("BonusThreeReviewFinishTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");

                tmpa.AddRange(tmpaUpdate);
            }
            #endregion
            #region 检查并打款退回
            if (entity.Status.ToString() == OSDoctorBonusFormStatus.检查并打款退回.Key)
            {
                if (serverEntity.Status.ToString() != OSDoctorBonusFormStatus.二审通过.Key && serverEntity.Status.ToString() != OSDoctorBonusFormStatus.打款异常.Key)
                {
                    return false;
                }
                //如果医生奖金记录里已经有部分记录是检查并打款或打款完成的状态,不允许退回
                IList<OSDoctorBonus> tempList2 = IDOSDoctorBonusDao.GetListByHQL("OSDoctorBonusFormID=" + serverEntity.Id + " and (Status=" + OSDoctorBonusFormStatus.检查并打款.Key + " or Status=" + OSDoctorBonusFormStatus.打款完成.Key + ")");
                if (tempList2.Count > 0)
                {
                    return false;
                }
                tmpaUpdate.Add("BonusThreeReviewManID=" + EmpID);
                tmpaUpdate.Add("BonusThreeReviewManName='" + EmpName + "'");

                tmpaUpdate.Add("BonusThreeReviewStartTime=null");
                tmpaUpdate.Add("BonusThreeReviewFinishTime=null");

                tmpa.AddRange(tmpaUpdate);
            }
            #endregion
            return true;
        }
        #region 医生奖金记录
        /// <summary>
        ///  单个医生奖金记录检查并打款操作处理
        /// </summary>
        /// <param name="applyEntity"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        public BaseResultBool UpdateOSDoctorBonusListPayStatus(OSDoctorBonusApply applyEntity, long empID, string empName)
        {
            BaseResultBool brb = new BaseResultBool();
            if (applyEntity.OSDoctorBonusForm == null)
            {
                brb.success = false;
                brb.ErrorInfo = "医生奖金结算单信息(OSDoctorBonusForm)为空";
                return brb;
            }
            if (applyEntity.OSDoctorBonus == null)
            {
                brb.success = false;
                brb.ErrorInfo = "医生奖金结算记录信息(OSDoctorBonus)为空";
                return brb;
            }
            OSDoctorBonusForm editEntity = applyEntity.OSDoctorBonusForm;
            //当前编辑待保存的医生奖金结算单申请保存在数据库的信息
            OSDoctorBonusForm serverEntity = new OSDoctorBonusForm();

            #region 服务器医生奖金结算单的状态验证
            serverEntity = DBDao.Get(applyEntity.OSDoctorBonusForm.Id);
            if (editEntity.Status.ToString() == OSDoctorBonusFormStatus.检查并打款.Key)
            {
                if (serverEntity.Status.ToString() != OSDoctorBonusFormStatus.二审通过.Key && serverEntity.Status.ToString() != OSDoctorBonusFormStatus.检查并打款.Key && serverEntity.Status.ToString() != OSDoctorBonusFormStatus.打款异常.Key)
                {
                    brb.success = false;
                    brb.ErrorInfo = "医生奖金结算单ID：" + editEntity.Id + "的状态为：" + OSDoctorBonusFormStatus.GetStatusDic()[serverEntity.Status.ToString()].Name + "！";
                    return brb;
                }
            }
            #endregion

            #region 服务器医生奖金记录的状态验证
            //StringBuilder bonusIDStr = new StringBuilder();
            //foreach (var item in applyEntity.OSDoctorBonusList)
            //{
            //    bonusIDStr.Append(item.Id + ",");
            //}
            //IList<OSDoctorBonus> tempBonusList = IDOSDoctorBonusDao.GetListByHQL("Id in(" + bonusIDStr.ToString().TrimEnd(',') + ") and OSDoctorBonusFormID=" + editEntity.Id + " and (Status=" + OSDoctorBonusFormStatus.检查并打款.Key + " or Status=" + OSDoctorBonusFormStatus.打款完成.Key + ")");

            IList<OSDoctorBonus> tempBonusList = IDOSDoctorBonusDao.GetListByHQL("Id=" + applyEntity.OSDoctorBonus.Id + " and OSDoctorBonusFormID=" + editEntity.Id + " and (Status=" + OSDoctorBonusFormStatus.检查并打款.Key + " or Status=" + OSDoctorBonusFormStatus.打款完成.Key + ")");
            if (tempBonusList.Count > 0)
            {
                StringBuilder errorInfo = new StringBuilder();
                foreach (var bonus in tempBonusList)
                {
                    errorInfo.Append("医生为【" + bonus.DoctorName + "】的状态为：" + OSDoctorBonusFormStatus.GetStatusDic()[bonus.Status.ToString()].Name + "！" + Environment.NewLine);
                }
                brb.success = false;
                brb.ErrorInfo = errorInfo.ToString();
                return brb;
            }
            #endregion

            IList<BDoctorAccount> doctorAccountList = GetBDoctorAccountList(applyEntity);
            List<string> tempList = new List<string>();

            tempList.Add("BonusThreeReviewManID=" + empID);
            tempList.Add("BonusThreeReviewManName='" + empName + "'");
            tempList.Add("BonusThreeReviewFinishTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");

            List<string> tmpaUpdate = new List<string>();
            tmpaUpdate.Add("Id=" + applyEntity.OSDoctorBonus.Id);
            tmpaUpdate.AddRange(tempList);
            if (applyEntity.OSDoctorBonus.OrderFormAmount.HasValue && applyEntity.OSDoctorBonus.OrderFormAmount.Value > 0)
            {
                brb = UpdateOSDoctorBonusPayedOne(doctorAccountList, tmpaUpdate, applyEntity.OSDoctorBonus);
                if (!brb.success)
                {
                    return brb;
                }
            }
            else
            {
                brb.success = false;
                brb.ErrorInfo = "医生的开单金额为0!";
                return brb;
            }
            if (serverEntity.Status.ToString() != OSDoctorBonusFormStatus.检查并打款.Key && serverEntity.Status.ToString() != OSDoctorBonusFormStatus.打款完成.Key)
            {
                List<string> tmpaUpdate2 = new List<string>();
                tmpaUpdate2.Add("Id=" + editEntity.Id);
                tmpaUpdate2.Add("Status=" + editEntity.Status.Value);
                brb.success = DBDao.Update(tmpaUpdate2.ToArray());
                if (brb.success)
                    SaveAHOperation(editEntity);
            }
            return brb;
        }
        /// <summary>
        /// 医生奖金记录打款支付处理
        /// </summary>
        /// <param name="doctorAccountList"></param>
        /// <param name="tempList"></param>
        /// <param name="updateBonus"></param>
        /// <returns></returns>
        private BaseResultBool UpdateOSDoctorBonusPayedOne(IList<BDoctorAccount> doctorAccountList, List<string> tempList, OSDoctorBonus updateBonus)
        {
            BaseResultBool brb = new BaseResultBool();

            if (updateBonus.Amount.HasValue)
                tempList.Add("Amount=" + Math.Round(updateBonus.Amount.Value, 2));
            if (updateBonus.Percent.HasValue)
                tempList.Add("Percent=" + Math.Round(updateBonus.Percent.Value, 2));
            if (!updateBonus.PaymentMethod.HasValue || updateBonus.PaymentMethod.Value == long.Parse(OSDoctorBonusPaymentMethod.微信支付.Key))
            {
                #region 微信企业付款
                if (PayToUserfunc == null)
                {
                    brb.success = false;
                    brb.ErrorInfo = "微信企业付款异常！";
                    ZhiFang.Common.Log.Log.Error("BOSDoctorBonusForm.UpdateOSDoctorBonusPayedOne.异常：企业付款委托为空！");
                    return brb;
                }
                string tmpopenid = IDBWeiXinAccountDao.Get(updateBonus.WeiXinUserID.Value).WeiXinAccount;
                var result = PayToUserfunc(updateBonus.BonusCode, tmpopenid, updateBonus.DoctorName, float.Parse((updateBonus.Amount.Value * 100).ToString()), "积分发放");

                tempList.Add("PaymentMethod=" + OSDoctorBonusPaymentMethod.微信支付.Key);

                if (result != null)
                {
                    if (result["return_code"] != null)
                    {
                        if (result["return_code"].ToString().Trim() == "SUCCESS")
                        {
                            tempList.Add("ReturnCode='" + result["return_code"].ToString().Trim() + "'");
                            tempList.Add("ReturnMsg='" + result["return_msg"].ToString().Trim() + "'");
                            tempList.Add("ResultCode='" + result["result_code"].ToString().Trim() + "'");
                            if (result["result_code"].ToString().Trim() == "SUCCESS")
                            {
                                tempList.Add("PaymentNo='" + result["payment_no"].ToString().Trim() + "'");
                                tempList.Add("PaymentTime='" + result["payment_time"].ToString().Trim() + "'");
                                tempList.Add("Status=" + OSDoctorBonusFormStatus.打款完成.Key + "");
                            }
                            else
                            {
                                tempList.Add("ErrCode='" + result["err_code"].ToString().Trim() + "'");
                                tempList.Add("ErrCodeDes='" + result["err_code_des"].ToString().Trim() + "'");
                                tempList.Add("Status=" + OSDoctorBonusFormStatus.打款异常.Key + "");
                                brb.success = false;
                                brb.ErrorInfo = "微信企业付款异常！" + result["err_code_des"].ToString().Trim();
                                ZhiFang.Common.Log.Log.Error("BOSDoctorBonusForm.UpdateOSDoctorBonusPayedOne.微信企业付款异常：ErrCode:" + result["err_code"].ToString().Trim() + "@@@ErrCodeDes:" + result["err_code_des"].ToString().Trim());
                                return brb;
                            }
                        }
                        else
                        {
                            tempList.Add("ReturnCode='FAIL'");
                            tempList.Add("ReturnMsg='" + result["return_msg"].ToString().Trim() + "'");
                            tempList.Add("Status=" + OSDoctorBonusFormStatus.打款异常.Key + "");
                            brb.success = false;
                            brb.ErrorInfo = "微信企业付款异常！";
                            ZhiFang.Common.Log.Log.Error("BOSDoctorBonusForm.UpdateOSDoctorBonusPayedOne.异常：ReturnCode:" + result["return_code"].ToString().Trim());
                        }
                    }
                    else
                    {
                        tempList.Add("Status=" + OSDoctorBonusFormStatus.打款异常.Key + "");
                        brb.success = false;
                        brb.ErrorInfo = "微信企业付款异常！";
                        ZhiFang.Common.Log.Log.Error("BOSDoctorBonusForm.UpdateOSDoctorBonusPayedOne.异常：result[return_code]为空！");
                        return brb;
                    }
                }
                #endregion
            }
            else
            {
                #region 同步医生帐号银行种类,银行帐号,发放方式
                if (doctorAccountList != null)
                {
                    var doctorList2 = doctorAccountList.Where(a => a.Id == updateBonus.DoctorAccountID.Value);
                    if (doctorList2.Count() > 0)
                    {
                        updateBonus.BankID = doctorList2.ElementAt(0).BankID;
                        if (String.IsNullOrEmpty(updateBonus.BankAccount))
                            updateBonus.BankAccount = doctorList2.ElementAt(0).BankAccount;
                        if (String.IsNullOrEmpty(updateBonus.BankAccount))
                            updateBonus.BankAccount = doctorList2.ElementAt(0).BankAccount;
                        if (String.IsNullOrEmpty(updateBonus.BankAddress))
                            updateBonus.BankAddress = doctorList2.ElementAt(0).BankAddress;
                        //奖金默认发放方式
                        if (!updateBonus.PaymentMethod.HasValue)
                        {
                            updateBonus.PaymentMethod = long.Parse(OSDoctorBonusPaymentMethod.银行转账.Key);
                        }
                    }
                }
                if (updateBonus.BankID.HasValue)
                    tempList.Add("BankID=" + updateBonus.BankID.Value);
                if (!String.IsNullOrEmpty(updateBonus.BankAccount))
                    tempList.Add("BankAccount='" + updateBonus.BankAccount + "'");
                if (!String.IsNullOrEmpty(updateBonus.BankTransFormCode))
                    tempList.Add("BankTransFormCode='" + updateBonus.BankTransFormCode + "'");
                if (!String.IsNullOrEmpty(updateBonus.BankAddress))
                    tempList.Add("BankAddress='" + updateBonus.BankAddress + "'");
                tempList.Add("Status=" + updateBonus.Status.Value);
                //发放方式
                if (updateBonus.PaymentMethod.HasValue && updateBonus.PaymentMethod.Value > 0)
                    tempList.Add("PaymentMethod=" + updateBonus.PaymentMethod.Value);
                #endregion
            }
            brb.success = IDOSDoctorBonusDao.Update(tempList.ToArray());

            //如果医生奖金结算单的状态为检查并打款,需要更新用户消费单的状态为"已结算:3"
            if (brb.success && updateBonus.Status.ToString() == OSDoctorBonusFormStatus.检查并打款.Key)
            {
                IDOSUserConsumerFormDao.UpdateStatusByOSDoctorBonusIDStr(updateBonus.Id.ToString(), OSUserConsumerFormStatus.已结算.Key);
            }
            return brb;
        }
        /// <summary>
        /// 更新单个医生奖金记录
        /// </summary>
        /// <param name="doctorAccountList"></param>
        /// <param name="tempList"></param>
        /// <param name="updateBonus"></param>
        /// <returns></returns>
        private BaseResultBool UpdateOSDoctorBonusOne(IList<BDoctorAccount> doctorAccountList, List<string> tempList, OSDoctorBonus updateBonus)
        {
            BaseResultBool brb = new BaseResultBool();

            if (updateBonus.Amount.HasValue)
                tempList.Add("Amount=" + Math.Round(updateBonus.Amount.Value, 2));

            if (!updateBonus.Percent.HasValue || updateBonus.Percent.Value == 0)
            {
                //医生结算比率值=医生的奖金金额/(医生的开单金额*100)*10000
                if (updateBonus.OrderFormAmount.HasValue && updateBonus.OrderFormAmount.Value > 0)
                    updateBonus.Percent = (updateBonus.Amount / (updateBonus.OrderFormAmount * 100)) * 10000;
            }
            if (updateBonus.Percent.HasValue)
                tempList.Add("Percent=" + Math.Round(updateBonus.Percent.Value, 2));
            if (!updateBonus.PaymentMethod.HasValue || updateBonus.PaymentMethod.Value == long.Parse(OSDoctorBonusPaymentMethod.银行转账.Key))
            {
                #region 同步医生帐号银行种类,银行帐号,发放方式
                if (doctorAccountList != null)
                {
                    var doctorList2 = doctorAccountList.Where(a => a.Id == updateBonus.DoctorAccountID.Value);
                    if (doctorList2.Count() > 0)
                    {
                        updateBonus.BankID = doctorList2.ElementAt(0).BankID;
                        if (String.IsNullOrEmpty(updateBonus.BankAccount))
                            updateBonus.BankAccount = doctorList2.ElementAt(0).BankAccount;
                        if (String.IsNullOrEmpty(updateBonus.BankAccount))
                            updateBonus.BankAccount = doctorList2.ElementAt(0).BankAccount;
                        if (String.IsNullOrEmpty(updateBonus.BankAddress))
                            updateBonus.BankAddress = doctorList2.ElementAt(0).BankAddress;
                        //奖金默认发放方式
                        if (!updateBonus.PaymentMethod.HasValue)
                        {
                            updateBonus.PaymentMethod = long.Parse(OSDoctorBonusPaymentMethod.银行转账.Key);
                        }
                    }
                }
                if (updateBonus.BankID.HasValue)
                    tempList.Add("BankID=" + updateBonus.BankID.Value);
                if (!String.IsNullOrEmpty(updateBonus.BankAccount))
                    tempList.Add("BankAccount='" + updateBonus.BankAccount + "'");
                if (!String.IsNullOrEmpty(updateBonus.BankTransFormCode))
                    tempList.Add("BankTransFormCode='" + updateBonus.BankTransFormCode + "'");
                if (!String.IsNullOrEmpty(updateBonus.BankAddress))
                    tempList.Add("BankAddress='" + updateBonus.BankAddress + "'");
                //发放方式
                if (updateBonus.PaymentMethod.HasValue && updateBonus.PaymentMethod.Value > 0)
                    tempList.Add("PaymentMethod=" + updateBonus.PaymentMethod.Value);
                #endregion
            }
            brb.success = IDOSDoctorBonusDao.Update(tempList.ToArray());

            //如果医生奖金结算单的状态为检查并打款,需要更新用户消费单的状态为"已结算:3"
            if (brb.success && updateBonus.Status.ToString() == OSDoctorBonusFormStatus.检查并打款.Key)
            {
                IDOSUserConsumerFormDao.UpdateStatusByOSDoctorBonusIDStr(updateBonus.Id.ToString(), OSUserConsumerFormStatus.已结算.Key);
            }
            return brb;
        }
        /// <summary>
        /// 获取医生帐号信息
        /// </summary>
        /// <param name="applyEntity"></param>
        /// <returns></returns>
        private IList<BDoctorAccount> GetBDoctorAccountList(OSDoctorBonusApply applyEntity)
        {
            IList<BDoctorAccount> doctorAccountList = new List<BDoctorAccount>();
            if (applyEntity.OSDoctorBonusForm.Status.ToString() == OSDoctorBonusFormStatus.二审通过.Key)
            {
                StringBuilder strb = new StringBuilder();
                //医生账户信息ID滤重
                var accountIDLists = applyEntity.OSDoctorBonusList.Where(p => p.DoctorAccountID.HasValue == true).GroupBy(p => new { p.DoctorAccountID }).Where(x => x.Count() > 0).ToList();

                foreach (var accountID in accountIDLists)
                {
                    if (!strb.ToString().Contains(accountID.Key.DoctorAccountID.Value + ","))
                        strb.Append(accountID.Key.DoctorAccountID.Value + ",");
                }
                if (!String.IsNullOrEmpty(strb.ToString()))
                {
                    doctorAccountList = IDBDoctorAccountDao.GetListByHQL("Id in(" + strb.ToString().TrimEnd(',') + ")");
                }
            }
            return doctorAccountList;
        }
        /// <summary>
        /// 检查医生奖金记录里是否还有未打款
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool SearchCheckIsUpdatePayed(long id)
        {
            IList<OSDoctorBonus> tempList2 = IDOSDoctorBonusDao.GetListByHQL("OSDoctorBonusFormID=" + id + " and (Status!=" + OSDoctorBonusFormStatus.检查并打款.Key + " or Status!=" + OSDoctorBonusFormStatus.打款完成.Key + ")");
            if (tempList2.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion
        /// <summary>
        /// 物理删除医生奖金结算单并同时删除医生奖金结算记录,操作记录及附件信息
        /// 只有当医生奖金结算单状态为暂存或者一审返回时可以执行操作
        /// </summary>
        /// <param name="longOSDoctorBonusFormID">医生奖金结算单Id</param>
        /// <returns></returns>
        public BaseResultBool DelOSDoctorBonusFormAndDetails(long longOSDoctorBonusFormID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            tempBaseResultBool.BoolFlag = true;
            OSDoctorBonusForm entity = DBDao.Get(longOSDoctorBonusFormID);
            if (entity.Status.ToString() != OSDoctorBonusFormStatus.暂存.Key || entity.Status.ToString() == OSDoctorBonusFormStatus.一审退回.Key)
            {
                string info = "医生奖金结算单ID为:" + longOSDoctorBonusFormID + "的状态为:" + OSDoctorBonusFormStatus.GetStatusDic()[entity.Status.ToString()].Name + ",不能删除!";
                tempBaseResultBool.BoolFlag = false;
                tempBaseResultBool.BoolInfo = info;
                tempBaseResultBool.ErrorInfo = info;
                return tempBaseResultBool;
            }
            try
            {
                //先删除医生奖金结算记录,操作记录及附件信息
                IDOSDoctorBonusAttachmentDao.DeleteByBobjectID(longOSDoctorBonusFormID);
                IDOSDoctorBonusOperationDao.DeleteByBobjectID(longOSDoctorBonusFormID);
                StringBuilder bonusIDStr = new StringBuilder();
                IList<OSDoctorBonus> tempList = IDOSDoctorBonusDao.GetListByHQL("OSDoctorBonusFormID=" + longOSDoctorBonusFormID + "");
                if (tempList.Count > 0)
                {
                    var idList = tempList.GroupBy(p => new { p.Id }).Where(x => x.Count() > 0).ToList();
                    foreach (var doctorBonus in idList)
                    {
                        if (!bonusIDStr.ToString().Contains(doctorBonus.Key.Id + ","))
                            bonusIDStr.Append(doctorBonus.Key.Id + ",");
                    }
                }
                //删除医生奖金结算记录
                IDOSDoctorBonusDao.DeleteByOSDoctorBonusFormID(longOSDoctorBonusFormID);
                //更新清空结算相关用户消费单的医生奖金记录关系
                if (!String.IsNullOrEmpty(bonusIDStr.ToString()))
                {
                    IDOSUserConsumerFormDao.UpdateOSDoctorBonusIDByOSDoctorBonusIDStr(bonusIDStr.ToString().TrimEnd(','));
                }
                //删除医生奖金结算单
                tempBaseResultBool.success = DBDao.Delete(longOSDoctorBonusFormID);
            }
            catch (Exception ee)
            {
                string info = "医生奖金结算单ID为:" + longOSDoctorBonusFormID + "删除出错!";
                info = info + Environment.NewLine + ee.Message;
                ZhiFang.Common.Log.Log.Error(info);
                tempBaseResultBool.BoolFlag = false;
                tempBaseResultBool.BoolInfo = info;
                tempBaseResultBool.ErrorInfo = info;
            }

            return tempBaseResultBool;
        }
        /// <summary>
        /// 操作记录登记
        /// </summary>
        /// <param name="tempEntity"></param>
        private void SaveAHOperation(OSDoctorBonusForm tempEntity)
        {
            OSDoctorBonusForm entity = this.Entity;
            if (tempEntity != null)
            {
                entity = tempEntity;
            }
            if (entity.Status.ToString() != OSDoctorBonusFormStatus.暂存.Key)
            {
                OSDoctorBonusOperation operation = new OSDoctorBonusOperation();
                operation.BobjectID = entity.Id;
                string empid = Common.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = Common.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid != null && empid.Trim() != "")
                    operation.CreatorID = long.Parse(empid);
                if (empname != null && empname.Trim() != "")
                    operation.CreatorName = empname;
                operation.BusinessModuleCode = "OSDoctorBonusForm";
                operation.Memo = entity.OperationMemo;

                operation.Type = entity.Status.Value;
                operation.TypeName = OSDoctorBonusFormStatus.GetStatusDic()[entity.Status.ToString()].Name;
                IDOSDoctorBonusOperationDao.Save(operation);
            }
        }

        #region PDF预览及Excel导出
        /// <summary>
        /// PDF预览
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isPreview"></param>
        /// <param name="templetName"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public BaseResultDataValue ExcelToPdfFile(long id, bool isPreview, string templetName, ref string fileName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            OSDoctorBonusForm entity = this.Get(id);
            if (String.IsNullOrEmpty(templetName))
                templetName = "医生奖金结算单模板.xlsx";
            //baseResultDataValue = FillMaintenanceDataToExcel(entity, id, templetName);
            if (entity != null)//  
                fileName = entity.BonusApplyManName + "医生奖金结算单.pdf";

            if (baseResultDataValue.success)
            {
                try
                {
                    string parentPath = (string)IBBParameter.GetCache(BParameterParaNoClass.ExcelExportSavePath.Key.ToString());

                    parentPath = parentPath + "\\OSManagerRefundForm\\" + entity.LabID.ToString();
                    if (isPreview)
                        parentPath = parentPath + "\\TempPDFFile\\" + entity.BonusApplyManName;
                    //+"\\"+ DateTime.Parse(entity.ApplyDate.ToString()).ToString("yyyyMMdd")
                    else
                        parentPath = parentPath + "\\ExcelFile";

                    string pdfFile = parentPath + "\\" + id + ".pdf";
                    //ZhiFang.Common.Log.Log.Info("TempPDFFile：" + pdfFile);
                    if (!Directory.Exists(parentPath))
                    {
                        Directory.CreateDirectory(parentPath);
                    }
                    baseResultDataValue.success = ExcelHelp.ExcelToPDF(baseResultDataValue.ResultDataValue, pdfFile);
                    if (baseResultDataValue.success)
                        baseResultDataValue.ResultDataValue = pdfFile;
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ResultDataValue = "";
                    baseResultDataValue.ErrorInfo = ex.Message;
                    ZhiFang.Common.Log.Log.Error("ExcelToPdfFile：" + ex.Message);
                    throw new Exception(ex.Message);
                }
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 医生奖金结算单Excel导出
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public FileStream GetExportExcelOSDoctorBonusFormDetail(string where, ref string fileName)
        {
            FileStream fileStream = null;
            //医生奖金结算单
            IList<OSDoctorBonusForm> atemCount = new List<OSDoctorBonusForm>();
            EntityList<OSDoctorBonusForm> tempEntityList = new EntityList<OSDoctorBonusForm>();

            tempEntityList = DBDao.GetListByHQL(where, " DataAddTime ", 0, 0);
            if (tempEntityList != null)
            {
                atemCount = tempEntityList.list;
            }
            if (atemCount != null && atemCount.Count > 0)
            {
                DataTable dtSource = null;
                dtSource = this.ExportExcelOSDoctorBonusFormToDataTable<OSDoctorBonusForm>(atemCount);
                string strHeaderText = "医生奖金结算清单信息";
                fileName = "医生奖金结算清单信息.xlsx";

                string filePath = "", basePath = "";
                //一级保存路径
                basePath = (string)IBBParameter.GetCache(BParameterParaNoClass.ExcelExportSavePath.Key.ToString());
                if (String.IsNullOrEmpty(basePath))
                {
                    basePath = "ExcelExport";
                }
                //ATEmpSignInfoDetail为二级保存路径,作分类用
                basePath = basePath + "\\" + "ExportExcelOSDoctorBonusForm\\";
                filePath = basePath + DateTime.Now.ToString("yyMMddhhmmss") + fileName;
                try
                {
                    if (!Directory.Exists(basePath))
                        Directory.CreateDirectory(basePath);
                    //单元格字体颜色的处理
                    Dictionary<string, short> cellFontStyleList = new Dictionary<string, short>();
                    //cellFontStyleList.Add("", NPOI.HSSF.Util.HSSFColor.Red.Index);

                    fileStream = ExportDTtoExcelHelp.ExportDTtoExcellHelp(dtSource, strHeaderText, filePath, cellFontStyleList);
                    if (fileStream != null)
                    {
                        fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    }
                }
                catch (Exception ee)
                {
                    ZhiFang.Common.Log.Log.Error("导出医生奖金结算清单信息失败:" + ee.Message);
                    throw ee;
                }
            }
            return fileStream;
        }

        private DataTable ExportExcelOSDoctorBonusFormToDataTable<T>(IList<T> list)
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
                    case "BonusFormRound":
                        columnName = "结算周期";
                        break;
                    case "BonusApplyManName":
                        columnName = "申请人";
                        break;
                    case "BonusApplytTime":
                        columnName = "申请时间";
                        break;
                    case "DoctorCount":
                        columnName = "医生数量";
                        break;
                    case "OrderFormCount":
                        columnName = "开单数量";
                        break;
                    case "Amount":
                        columnName = "结算金额";
                        break;
                    case "OrderFormAmount":
                        columnName = "开单金额";
                        break;
                    case "Memo":
                        columnName = "备注";
                        break;
                    case "BonusOneReviewManName":
                        columnName = "处理人";
                        break;
                    case "BonusOneReviewStartTime":
                        columnName = "处理开始时间";
                        break;
                    case "BonusOneReviewFinishTime":
                        columnName = "处理完成时间";
                        break;
                    case "BonusTwoReviewManName":
                        columnName = "审批人";
                        break;
                    case "BonusTwoReviewStartTime":
                        columnName = "审批开始时间";
                        break;
                    case "BonusTwoReviewFinishTime":
                        columnName = "审批完成时间";
                        break;
                    case "BonusThreeReviewManName":
                        columnName = "发放人";
                        break;
                    case "BonusThreeReviewStartTime":
                        columnName = "发放开始时间";
                        break;
                    case "BonusThreeReviewFinishTime":
                        columnName = "发放完成时间";
                        break;
                    default:
                        //columnName = "";
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
                //var values = new object[tb.Columns.Count];
                for (int i = 0; i < props.Length; i++)
                {
                    //if (tb.Columns.Contains(props[i].Name))
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
                tb.Columns["结算周期"].SetOrdinal(0);
                tb.Columns["申请人"].SetOrdinal(1);

                tb.Columns["申请时间"].SetOrdinal(2);
                tb.Columns["医生数量"].SetOrdinal(3);
                tb.Columns["开单数量"].SetOrdinal(4);
                tb.Columns["结算金额"].SetOrdinal(5);

                tb.Columns["开单金额"].SetOrdinal(6);
                tb.Columns["处理人"].SetOrdinal(7);
                tb.Columns["处理开始时间"].SetOrdinal(8);
                tb.Columns["处理完成时间"].SetOrdinal(9);

                tb.Columns["审批人"].SetOrdinal(10);
                tb.Columns["审批开始时间"].SetOrdinal(11);
                tb.Columns["审批完成时间"].SetOrdinal(12);

                tb.Columns["发放人"].SetOrdinal(13);
                tb.Columns["发放开始时间"].SetOrdinal(14);
                tb.Columns["发放完成时间"].SetOrdinal(15);

                tb.Columns["备注"].SetOrdinal(16);
                //排序
                tb.DefaultView.Sort = "结算周期 asc,申请人 asc";
                tb = tb.DefaultView.ToTable();

            }
            return tb;
        }
        #endregion
        /// <summary>
        /// 医生奖金结算单的结算单号
        /// 规则:yyMMddHHmmssfff+结算年月
        /// </summary>
        /// <param name="bonusFormRound">结算周期</param>
        /// <returns></returns>
        private string CreateBonusFormCode(string bonusFormRound)
        {
            string strSubNumber = "";
            strSubNumber = NextRuleNumber.GetOSBonusFormCode();
            return strSubNumber + bonusFormRound;
        }
        /// <summary>
        /// 医生奖金结算记录的结算单号
        /// 规则:“J”+医生奖金结算表的结算单号+医生GUID
        /// </summary>
        /// <param name="doctorAccountID"></param>
        /// <returns></returns>
        private string CreateDetailsBonusFormCode(string bonusFormRound, string doctorAccountID)
        {
            string no = "J" + bonusFormRound + doctorAccountID;
            return no;
        }
    }
}