
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.BloodTransfusion;
using ZhiFang.Entity.Base;
using ZhiFang.IDAO.NHB.BloodTransfusion;
using ZhiFang.BloodTransfusion.Common;
using ZhiFang.Entity.RBAC;
using ZhiFang.IDAO.RBAC;

namespace ZhiFang.BLL.BloodTransfusion
{
    /// <summary>
    ///
    /// </summary>
    public class BBloodBPreForm : BaseBLL<BloodBPreForm, string>, ZhiFang.IBLL.BloodTransfusion.IBBloodBPreForm
    {
        IDBloodBInItemDao IDBloodBInItemDao { get; set; }
        IDBloodBPreItemDao IDBloodBPreItemDao { get; set; }
        IDBloodBagABOCheckDao IDBloodBagABOCheckDao { get; set; }
        IDBloodBagProcessDao IDBloodBagProcessDao { get; set; }
        IDBloodBagProcessTypeDao IDBloodBagProcessTypeDao { get; set; }
        IDPUserDao IDPUserDao { get; set; }

        public EntityList<BReqComplexOfInInfoVO> SearchBReqComplexOfInInfoVOByBReqFormID(string reqFormId, string sort, int page, int limit)
        {
            bool isMerge = true;

            EntityList<BReqComplexOfInInfoVO> entityList = new EntityList<BReqComplexOfInInfoVO>();
            entityList.list = new List<BReqComplexOfInInfoVO>();

            if (string.IsNullOrEmpty(reqFormId))
            {
                return entityList;
            }

            //临时人员集合信息
            IList<PUser> puserList = new List<PUser>();
            //按申请主单号获取交叉配血主单信息
            IList<BloodBPreForm> bpreFormList = ((IDBloodBPreFormDao)base.DBDao).GetListByHQL("bloodbpreform.BloodBReqForm.Id='" + reqFormId + "'");
            foreach (BloodBPreForm bpreForm in bpreFormList)
            {
                //交叉配血血袋信息
                IList<BloodBPreItem> bpreItemList = IDBloodBPreItemDao.GetListByHQL("bloodbpreitem.BloodBPreForm.Id='" + bpreForm.Id + "'");

                //通过交叉配血明细的血袋信息获取入库的血袋信息
                foreach (BloodBPreItem bpreItem in bpreItemList)
                {
                    BReqComplexOfInInfoVO inInfoVO = new BReqComplexOfInInfoVO();
                    #region 交叉配血主单信息
                    inInfoVO.BPreFormId = bpreForm.Id;
                    inInfoVO.BPOperatorID = bpreForm.OperatorID.ToString();
                    inInfoVO.Bloodstyle = bpreItem.Bloodstyle;
                    PUser puser = null;
                    var tempPUserList = puserList.Where(p => p.Id == bpreForm.OperatorID);
                    if (tempPUserList != null && tempPUserList.Count() > 0) puser = tempPUserList.ElementAt(0);
                    if (puser == null)
                    {
                        puser = IDPUserDao.Get(bpreForm.OperatorID);
                        if (puser != null)
                        {
                            puserList.Add(puser);
                        }
                    }
                    if (puser != null)
                    {
                        inInfoVO.BPOperator = puser.CName;
                    }
                    #endregion
                    #region 交叉配血血袋信息
                    inInfoVO.BPreItemId = bpreItem.Id;
                    inInfoVO.BBagCode = bpreItem.BBagCode;
                    inInfoVO.PCode = bpreItem.PCode;
                    inInfoVO.B3Code = bpreItem.B3Code;

                    inInfoVO.BPreDate = bpreItem.BPreDate;
                    #endregion
                    #region 对应的入库血袋信息
                    BloodBInItem inItem = bpreItem.BloodBInItem;
                    if (inItem != null)
                    {
                        inInfoVO.BInDate = inItem.BInDate;
                        if (inItem.BloodBInForm != null)
                        {
                            inInfoVO.InOperatorID = inItem.BloodBInForm.OperatorID.ToString();
                            PUser puser2 = null;
                            var tempPUserList2 = puserList.Where(p => p.Id == inItem.BloodBInForm.OperatorID);
                            if (tempPUserList2 != null && tempPUserList2.Count() > 0) puser2 = tempPUserList2.ElementAt(0);
                            if (puser2 == null)
                            {
                                puser2 = IDPUserDao.Get(inItem.BloodBInForm.OperatorID);
                                if (puser2 != null)
                                {
                                    puserList.Add(puser2);
                                }
                            }
                            if (puser2 != null)
                            {
                                inInfoVO.InOperator = puser2.CName;
                            }
                        }
                        else
                        {
                            ZhiFang.Common.Log.Log.Debug("入库明细ID:" + inItem.Id + ",未获取到入库主单信息");
                        }
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug("交叉配血明细ID:" + bpreItem.Id + ",未获取到入库血袋信息");
                    }
                    #endregion
                    #region 入库血袋的血袋复检信息
                    if (inItem != null)
                    {
                        //IList<BloodBagABOCheck> bagABOCheckList = IDBloodBagABOCheckDao.GetListByHQL("");
                        //血袋复检主键等于入库明细Id
                        BloodBagABOCheck bagABOCheck = IDBloodBagABOCheckDao.Get(inItem.Id);
                        if (bagABOCheck != null)
                        {
                            //BloodBagABOCheck bagABOCheck = bagABOCheckList[0];
                            if (bagABOCheck.ReCheckTime.HasValue)
                                inInfoVO.ReCheckTime = bagABOCheck.ReCheckTime.Value.ToString();
                            inInfoVO.ReCheckID = bagABOCheck.ReCheckID.ToString();
                            PUser puser3 = null;
                            if (!string.IsNullOrEmpty(bagABOCheck.ReCheckID))
                            {
                                var tempPUserList3 = puserList.Where(p => p.Id == int.Parse(bagABOCheck.ReCheckID));
                                if (tempPUserList3 != null && tempPUserList3.Count() > 0) puser3 = tempPUserList3.ElementAt(0);
                                if (puser3 == null)
                                {
                                    puser3 = IDPUserDao.Get(int.Parse(bagABOCheck.ReCheckID));
                                    if (puser3 != null)
                                    {
                                        puserList.Add(puser3);
                                    }
                                }
                                if (puser3 != null)
                                {
                                    inInfoVO.ReCheck = puser3.CName;
                                }
                            }
                        }
                        else
                        {
                            ZhiFang.Common.Log.Log.Debug("入库明细ID:" + inItem.Id + ",未获取到血袋复检信息");
                        }
                    }
                    #endregion
                    #region 交叉配血的血袋加工信息
                    IList<BloodBagProcess> bagProcessList = IDBloodBagProcessDao.GetListByHQL("bloodbagprocess.BloodBPreItem.Id='" + bpreItem.Id + "'");
                    //加工方式存在多行时,按加工方式作行显示
                    if (bagProcessList.Count > 0)
                    {
                        if (isMerge == true)
                        {
                            string ptCName = "";
                            foreach (BloodBagProcess bagProcess in bagProcessList)
                            {
                                if (bagProcess != null && bagProcess.BloodBagProcessType != null)
                                {
                                    ptCName = ptCName + bagProcess.BloodBagProcessType.CName + ";";
                                }
                            }
                            inInfoVO.PTCName = ptCName;
                            entityList.list.Add(inInfoVO);
                        }
                        else
                        {
                            //按行显示
                            foreach (BloodBagProcess bagProcess in bagProcessList)
                            {
                                BReqComplexOfInInfoVO inInfoVO2 = ClassMapperHelp.GetMapper<BReqComplexOfInInfoVO, BReqComplexOfInInfoVO>(inInfoVO);
                                if (bagProcess != null && bagProcess.BloodBagProcessType != null)
                                {
                                    inInfoVO2.ProcessId = bagProcess.Id.ToString();
                                    inInfoVO2.PTNo = bagProcess.BloodBagProcessType.Id;
                                    inInfoVO2.PTCName = bagProcess.BloodBagProcessType.CName;
                                }
                                entityList.list.Add(inInfoVO2);
                            }
                        }
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug("交叉配血明细ID:" + bpreItem.Id + ",未获取到血袋加工信息");
                        entityList.list.Add(inInfoVO);
                    }
                    #endregion
                }

            }
            entityList.count = entityList.list.Count();

            return entityList;
        }

    }
}