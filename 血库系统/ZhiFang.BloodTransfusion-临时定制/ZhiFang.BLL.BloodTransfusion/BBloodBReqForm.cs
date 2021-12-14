
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.BloodTransfusion;
using ZhiFang.IDAO.NHB.BloodTransfusion;
using ZhiFang.Entity.Base;
using System.Data;
using System.IO;
using ZhiFang.BloodTransfusion.Common;
using ZhiFang.IDAO.RBAC;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.RBAC;

namespace ZhiFang.BLL.BloodTransfusion
{
    /// <summary>
    ///医嘱申请不能按就诊类型,申请类型,科室,医生进行联合查询,因为相关数据项都是空的多
    /// </summary>
    public class BBloodBReqForm : BaseBLL<BloodBReqForm, string>, ZhiFang.IBLL.BloodTransfusion.IBBloodBReqForm
    {
        IDBloodBReqTypeDao IDBloodBReqTypeDao { get; set; }
        IDBloodUseTypeDao IDBloodUseTypeDao { get; set; }
        IBDepartment IBDepartment { get; set; }
        IDDepartmentDao IDDepartmentDao { get; set; }
        IDDoctorDao IDDoctorDao { get; set; }
        IDBloodstyleDao IDBloodstyleDao { get; set; }
        IBSCOperation IBSCOperation { get; set; }
        IBBloodBReqItem IBBloodBReqItem { get; set; }
        IBBloodBReqItemResult IBBloodBReqItemResult { get; set; }
        IBBloodLargeUseItem IBBloodLargeUseItem { get; set; }
        IBBloodBReqFormResult IBBloodBReqFormResult { get; set; }
        IBBParameter IBBParameter { get; set; }
        IDBloodClassDao IDBloodClassDao { get; set; }

        #region 医生站
        #region 定制查询

        //获取到新增保存申请单号值
        private string GetAddBloodBReqFormId()
        {
            string maxNoStr = IBBParameter.GetAddBloodBReqFormId(4);
            if (!string.IsNullOrEmpty(maxNoStr))
            {
                BloodBReqForm req = ((IDBloodBReqFormDao)base.DBDao).Get(maxNoStr);
                if (req != null) maxNoStr = IBBParameter.GetAddBloodBReqFormId(4);
            }
            return maxNoStr;
        }
        private string getBReqTypeCName(int id)
        {
            string cName = "";
            if (CacheDictClass.BloodBReqTypeList == null || CacheDictClass.BloodBReqTypeList.Count <= 0)
                CacheDictClass.BloodBReqTypeList = IDBloodBReqTypeDao.LoadAll();

            var tempList = CacheDictClass.BloodBReqTypeList.Where(p => p.Id == id);
            if (tempList != null && tempList.Count() > 0)
                cName = tempList.ElementAt(0).CName;
            return cName;
        }
        private string getUseTypeCName(string id)
        {
            string cName = "";
            if (CacheDictClass.BloodUseTypeList == null || CacheDictClass.BloodUseTypeList.Count <= 0)
                CacheDictClass.BloodUseTypeList = IDBloodUseTypeDao.LoadAll();

            var tempList = CacheDictClass.BloodUseTypeList.Where(p => p.Id == id);
            if (tempList != null && tempList.Count() > 0)
                cName = tempList.ElementAt(0).CName;
            return cName;
        }
        private string getDeptCName(int id)
        {
            string cName = "";
            if (CacheDictClass.DepartmentList == null || CacheDictClass.DepartmentList.Count <= 0)
                CacheDictClass.DepartmentList = IDDepartmentDao.LoadAll();

            var tempList = CacheDictClass.DepartmentList.Where(p => p.Id == id);
            if (tempList != null && tempList.Count() > 0)
                cName = tempList.ElementAt(0).CName;
            return cName;
        }
        private string getDoctorCName(int id)
        {
            string cName = "";
            if (CacheDictClass.DoctorList == null || CacheDictClass.DoctorList.Count <= 0)
                CacheDictClass.DoctorList = IDDoctorDao.LoadAll();

            var tempList = CacheDictClass.DoctorList.Where(p => p.Id == id);
            if (tempList != null && tempList.Count() > 0)
                cName = tempList.ElementAt(0).CName;
            return cName;
        }
        public IList<BloodBReqForm> SearchBloodBReqFormListByHql(string where, string sort, int page, int limit)
        {
            IList<BloodBReqForm> entityList = new List<BloodBReqForm>();
            entityList = ((IDBloodBReqFormDao)base.DBDao).GetListByHQL(where, sort, page, limit).list;
            for (int i = 0; i < entityList.Count; i++)
            {
                if (entityList[i].BReqTypeID.HasValue)
                    entityList[i].BReqTypeCName = getBReqTypeCName(entityList[i].BReqTypeID.Value);
                if (entityList[i].UseTypeID.HasValue)
                    entityList[i].UseTypeCName = getUseTypeCName(entityList[i].UseTypeID.Value.ToString());
                if (entityList[i].DeptNo.HasValue)
                    entityList[i].DeptCName = getDeptCName(entityList[i].DeptNo.Value);
                if (entityList[i].DoctorNo.HasValue)
                    entityList[i].DoctorCName = getDoctorCName(entityList[i].DoctorNo.Value);
            }
            return entityList;
        }
        public EntityList<BloodBReqForm> SearchBloodBReqFormEntityListByHql(string where, string sort, int page, int limit)
        {
            EntityList<BloodBReqForm> entityList = new EntityList<BloodBReqForm>();
            entityList = ((IDBloodBReqFormDao)base.DBDao).GetListByHQL(where, sort, page, limit);
            for (int i = 0; i < entityList.list.Count; i++)
            {
                if (entityList.list[i].BReqTypeID.HasValue)
                    entityList.list[i].BReqTypeCName = getBReqTypeCName(entityList.list[i].BReqTypeID.Value);
                if (entityList.list[i].UseTypeID.HasValue)
                    entityList.list[i].UseTypeCName = getUseTypeCName(entityList.list[i].UseTypeID.Value.ToString());
                if (entityList.list[i].DeptNo.HasValue)
                    entityList.list[i].DeptCName = getDeptCName(entityList.list[i].DeptNo.Value);
                if (entityList.list[i].DoctorNo.HasValue)
                    entityList.list[i].DoctorCName = getDoctorCName(entityList.list[i].DoctorNo.Value);
                if (entityList.list[i].BreqStatusID.HasValue)
                    entityList.list[i].DoctorCName = getDoctorCName(entityList.list[i].DoctorNo.Value);
            }
            return entityList;
        }

        public IList<BloodBReqForm> SearchBloodBReqFormListByJoinHql(string where, string sort, int page, int limit)
        {
            IList<BloodBReqForm> entityList = new List<BloodBReqForm>();
            entityList = ((IDBloodBReqFormDao)base.DBDao).SearchBloodBReqFormListByJoinHql(where, sort, page, limit);
            return entityList;
        }
        public EntityList<BloodBReqForm> SearchBloodBReqFormEntityListByJoinHql(string where, string sort, int page, int limit)
        {
            EntityList<BloodBReqForm> entityList = new EntityList<BloodBReqForm>();
            entityList = ((IDBloodBReqFormDao)base.DBDao).SearchBloodBReqFormEntityListByJoinHql(where, sort, page, limit);
            return entityList;
        }
        #endregion

        #region 医嘱申请
        public BaseResultDataValue AddBloodBReqFormAndDtl(BloodBReqForm entity, SysCurUserInfo curDoctor, IList<BloodBReqItem> addBreqItemList, IList<BloodBReqFormResult> addResultList, long empID, string empName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            string maxNoStr = GetAddBloodBReqFormId();
            if (!string.IsNullOrEmpty(maxNoStr))
            {
                entity.Id = maxNoStr;
            }
            //保存验证
            if (addBreqItemList == null || addBreqItemList.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数addBreqItemList为空!";
                return brdv;
            }
            ZhiFang.Common.Log.Log.Info("申请单号为:" + entity.Id + ",新增保存处理开始!");
            if (!entity.BreqStatusID.HasValue)
                entity.BreqStatusID = int.Parse(BreqFormStatus.医嘱暂存.Key);
            entity.BreqStatusName = BreqFormStatus.GetStatusDic()[entity.BreqStatusID.ToString()].Name;
            entity.Visible = true;
            entity.Postflag = 1;//CS设计用到:提交的时候，直接更新=1
            if (entity.BReqFormFlag < 0)
                entity.BReqFormFlag = int.Parse(BReqFormFlag.未受理.Key);

            //24小时内申请总量计算
            IList<BloodLargeUseItem> useItemList = new List<BloodLargeUseItem>();
            if (entity.BreqStatusID.Value == long.Parse(BreqFormStatus.提交申请.Key))
            {
                //entity.Postflag = 1;
                entity.ReqTime = DateTime.Now;
                entity.ApplyTime = DateTime.Now;
            }
            //紧急用血(抢救用血)时,在点击提交申请后,前台自动将"提交申请"状态更新为"审批完成"
            if (entity.BreqStatusID.Value == long.Parse(BreqFormStatus.提交申请.Key) || entity.BreqStatusID.Value == long.Parse(BreqFormStatus.审批完成.Key))
            {
                useItemList = GetAllBloodLargeUseItemOf24Hours(ref entity, addBreqItemList, null);
            }
            entity.PrintTotal = 0;
            if (entity.CheckCompleteFlag == true)
            {
                entity.CheckCompleteTime = DateTime.Now;
            }
            this.Entity = entity;
            brdv.success = this.Add();
            if (brdv.success == false)
            {
                brdv.ErrorInfo = "新增医嘱申请信息失败!";
                return brdv;
            }
            brdv = IBBloodBReqItem.AddBReqItemList(entity, addBreqItemList);
            if (brdv.success == false) return brdv;
            //检验录入结果保存
            brdv = IBBloodBReqFormResult.AddBReqFormResultList(entity, addResultList);
            if (brdv.success == false) return brdv;
            //病人对应的LIS结果保存
            brdv = IBBloodBReqItemResult.AddBReqItemResultOfReqForm(entity);
            if (brdv.success == false) return brdv;

            //24小时内医嘱申请总量处理
            if (useItemList != null && useItemList.Count > 0)
            {
                brdv = IBBloodLargeUseItem.AddBloodLargeUseItemList(entity, useItemList);
                string reviewTips = _getReviewTips(entity);
                brdv.ErrorInfo = reviewTips;
            }

            if (entity.BreqStatusID.Value != long.Parse(BreqFormStatus.医嘱暂存.Key))
            {
                AddSCOperation(entity, empID, empName, entity.BreqStatusID.Value, entity.Memo);
            }
            ZhiFang.Common.Log.Log.Info("申请单号为:" + entity.Id + ",新增保存处理结束,处理结果为:" + brdv.success + "!");
            return brdv;
        }
        /// <summary>
        /// 医生确认提交后提示需要哪些角色进行审批操作
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private string _getReviewTips(BloodBReqForm entity)
        {
            string reviewTips = "";
            if (entity.BreqStatusID.Value == long.Parse(BreqFormStatus.提交申请.Key))
            {
                if (entity.ReqTotal < 800)
                {
                    reviewTips = string.Format("申请单已经提交,需要{0}进行审批!请提醒相关人员进行审批操作!", "[上级医生]");
                }
                else if (entity.ReqTotal >= 800 && entity.ReqTotal < 1600)
                {
                    reviewTips = string.Format("申请单已经提交,需要{0}进行审批!请提醒相关人员进行审批操作!", "[上级医生][科主任]");
                }
                else if (entity.ReqTotal >= 1600)
                {
                    reviewTips = string.Format("申请单已经提交,需要{0}进行审批!请提醒相关人员进行审批操作!", "[上级医生][科主任][医务科]");
                }
                else
                {
                    reviewTips = "";
                }
            }
            if (!string.IsNullOrEmpty(reviewTips))
                ZhiFang.Common.Log.Log.Info("申请单号为:" + entity.Id + "," + reviewTips);
            return reviewTips;
        }
        /// <summary>
        /// 定制修改医嘱申请主单信息及申请明细信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tempArray"></param>
        /// <param name="addBreqItemList"></param>
        /// <param name="editBreqItemList"></param>
        /// <param name="addResultList"></param>
        /// <param name="editResultList"></param>
        /// <returns></returns>
        public BaseResultBool EditBloodBReqFormAndDtlByField(ref BloodBReqForm entity, SysCurUserInfo curDoctor, string[] tempArray, IList<BloodBReqItem> addBreqItemList, IList<BloodBReqItem> editBreqItemList, IList<BloodBReqFormResult> addResultList, IList<BloodBReqFormResult> editResultList, long empID, string empName)
        {
            BaseResultBool brdv = new BaseResultBool();

            BloodBReqForm oldEntity = ((IDBloodBReqFormDao)base.DBDao).Get(entity.Id);
            List<string> tmpa = tempArray.ToList();
            entity.BreqStatusName = BreqFormStatus.GetStatusDic()[entity.BreqStatusID.ToString()].Name;
            //先判断当前医嘱是否为医嘱审批完成,如果不是医嘱作废,不能再修改该医嘱单
            if (oldEntity.CheckCompleteFlag == true && entity.BreqStatusID.Value != long.Parse(BreqFormStatus.提交申请.Key))
            {
                brdv.success = false;
                brdv.ErrorInfo = "用血申请单ID：" + entity.Id + ",的医嘱审批完成,不能操作！";
                return brdv;
            }
            if (!EditBloodBReqFormStatusCheck(entity, oldEntity, tmpa, empID, empName))
            {
                brdv.success = false;
                brdv.ErrorInfo = "用血申请单ID：" + entity.Id + "的状态为：" + BreqFormStatus.GetStatusDic()[oldEntity.BreqStatusID.ToString()].Name + "！";
                return brdv;
            }
            ZhiFang.Common.Log.Log.Info("申请单号为:" + entity.Id + ",编辑保存处理开始!");
            if (entity.BreqStatusID.Value == long.Parse(BreqFormStatus.提交申请.Key))
            {
                entity.ReqTime = DateTime.Now;
                entity.ApplyTime = DateTime.Now;
            }
            //24小时内申请总量计算 
            IList<BloodLargeUseItem> useItemList = new List<BloodLargeUseItem>();
            //紧急用血时,在点击提交申请后,前台自动将"提交申请"状态更新为"审批完成"
            if (oldEntity.BreqStatusID.Value == long.Parse(BreqFormStatus.医嘱暂存.Key) && (entity.BreqStatusID.Value == long.Parse(BreqFormStatus.提交申请.Key) || entity.BreqStatusID.Value == long.Parse(BreqFormStatus.审批完成.Key)))
            {
                useItemList = GetAllBloodLargeUseItemOf24Hours(ref entity, addBreqItemList, editBreqItemList);
                string reqTotalStr = "ReqTotal=" + entity.ReqTotal + " ";
                if (tmpa.IndexOf(reqTotalStr) < 0)
                    tmpa.Add(reqTotalStr);
            }
            //医嘱申请是否审批完成处理
            EditCheckBloodsReqTotalIsComplete(ref entity, oldEntity, tmpa);

            this.Entity = entity;
            tempArray = tmpa.ToArray();
            if (this.Update(tempArray))
            {
                BaseResultDataValue brdv2 = new BaseResultDataValue();
                if (addBreqItemList != null) brdv2 = IBBloodBReqItem.AddBReqItemList(entity, addBreqItemList);
                if (brdv2.success == false)
                {
                    brdv.success = brdv2.success;
                    brdv.ErrorInfo = brdv2.ErrorInfo;
                    return brdv;
                }
                //更新医嘱申请血制品
                if (editBreqItemList != null) brdv2 = IBBloodBReqItem.EditBReqItemList(entity, editBreqItemList);
                if (brdv2.success == false)
                {
                    brdv.success = brdv2.success;
                    brdv.ErrorInfo = brdv2.ErrorInfo;
                    return brdv;
                }
                //新增医嘱申请对应的LIS检验结果
                if (addResultList != null) brdv2 = IBBloodBReqFormResult.AddBReqFormResultList(entity, addResultList);
                if (brdv2.success == false)
                {
                    brdv.success = brdv2.success;
                    brdv.ErrorInfo = brdv2.ErrorInfo;
                    return brdv;
                }
                //更新医嘱申请对应的LIS检验结果
                if (editResultList != null) brdv2 = IBBloodBReqFormResult.EditBReqFormResultList(entity, editResultList);
                if (brdv2.success == false)
                {
                    brdv.success = brdv2.success;
                    brdv.ErrorInfo = brdv2.ErrorInfo;
                    return brdv;
                }

                //24小时内医嘱申请总量处理
                if (useItemList != null && useItemList.Count > 0)
                {
                    brdv2 = IBBloodLargeUseItem.AddBloodLargeUseItemList(entity, useItemList);
                    if (brdv2.success == false)
                    {
                        brdv.success = brdv2.success;
                        brdv.ErrorInfo = brdv2.ErrorInfo;
                        return brdv;
                    }
                    string reviewTips = _getReviewTips(entity);
                    brdv.ErrorInfo = reviewTips;
                }
                if (entity.BreqStatusID.Value != long.Parse(BreqFormStatus.医嘱暂存.Key))
                    AddSCOperation(entity, empID, empName, entity.BreqStatusID.Value, entity.Memo);
            }
            else
            {
                brdv.success = false;
                brdv.ErrorInfo = "用血申请单ID：" + entity.Id + "更新失败！";
            }
            ZhiFang.Common.Log.Log.Info("申请单号为:" + entity.Id + ",编辑保存处理结束,处理结果为:" + brdv.success + "!");
            return brdv;
        }
        bool EditBloodBReqFormStatusCheck(BloodBReqForm entity, BloodBReqForm serverEntity, List<string> tmpa, long empID, string empName)
        {
            #region 暂存
            if (entity.BreqStatusID.ToString() == BreqFormStatus.医嘱暂存.Key)
            {
                if (serverEntity.BreqStatusID.ToString() != BreqFormStatus.医嘱暂存.Key && serverEntity.BreqStatusID.ToString() != BreqFormStatus.上级审核退回.Key)
                {
                    return false;
                }
                if (!entity.ApplyID.HasValue)
                {
                    entity.ApplyID = empID;
                    entity.ApplyName = empName;

                }
                if (!entity.ApplyTime.HasValue)
                {
                    entity.ReqTime = DateTime.Now;
                    entity.ApplyTime = DateTime.Now;
                }
                string itemStr = "BreqStatusName='" + entity.BreqStatusName + "'";
                if (tmpa.IndexOf(itemStr) < 0)
                    tmpa.Add(itemStr);
            }
            #endregion

            #region 提交申请
            if (entity.BreqStatusID.ToString() == BreqFormStatus.提交申请.Key)
            {
                //审核应用时,可以先编辑保存状态为已申请的申请单
                if (serverEntity.BreqStatusID.ToString() != BreqFormStatus.医嘱暂存.Key && serverEntity.BreqStatusID.ToString() != BreqFormStatus.提交申请.Key && serverEntity.BreqStatusID.ToString() != BreqFormStatus.上级审核退回.Key)
                {
                    return false;
                }
                if (!entity.ApplyID.HasValue)
                {
                    entity.ApplyID = empID;
                    entity.ApplyName = empName;
                }
                //entity.Postflag = 1;
                entity.ReqTime = DateTime.Now;
                entity.ApplyTime = DateTime.Now;
                //tmpa.Add("Postflag=" + entity.Postflag + " ");
                tmpa.Add("ApplyID=" + entity.ApplyID + " ");
                tmpa.Add("ApplyName='" + entity.ApplyName + "'");
                //申请开单时间
                tmpa.Add("ReqTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                //申请时间
                tmpa.Add("ApplyTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("SeniorID=null");
                tmpa.Add("SeniorName=null");
                tmpa.Add("SeniorTime=null");
                string itemStr = "BreqStatusName='" + entity.BreqStatusName + "'";
                if (tmpa.IndexOf(itemStr) < 0)
                    tmpa.Add(itemStr);
            }
            #endregion

            #region 上级审核
            if (entity.BreqStatusID.ToString() == BreqFormStatus.上级审核通过.Key)
            {
                if (serverEntity.BreqStatusID.ToString() != BreqFormStatus.提交申请.Key && serverEntity.BreqStatusID.ToString() != BreqFormStatus.科主任审核退回.Key)
                {
                    return false;
                }
                if (!entity.SeniorID.HasValue)
                {
                    entity.SeniorID = empID;
                    entity.SeniorName = empName;
                }
                entity.SeniorTime = DateTime.Now;
                tmpa.Add("SeniorID=" + entity.SeniorID + " ");
                tmpa.Add("SeniorName='" + entity.SeniorName + "'");
                tmpa.Add("SeniorTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("SeniorMemo='" + entity.SeniorMemo + "'");
                string itemStr = "BreqStatusName='" + entity.BreqStatusName + "'";
                if (tmpa.IndexOf(itemStr) < 0)
                    tmpa.Add(itemStr);
            }

            if (entity.BreqStatusID.ToString() == BreqFormStatus.上级审核退回.Key)
            {
                if (serverEntity.BreqStatusID.ToString() != BreqFormStatus.提交申请.Key && serverEntity.BreqStatusID.ToString() != BreqFormStatus.科主任审核退回.Key)
                {
                    return false;
                }
                if (!entity.SeniorID.HasValue)
                {
                    entity.SeniorID = empID;
                    entity.SeniorName = empName;
                }

                tmpa.Add("SeniorID=" + entity.SeniorID + " ");
                tmpa.Add("SeniorName='" + entity.SeniorName + "'");
                tmpa.Add("SeniorTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("SeniorMemo='" + entity.SeniorMemo + "'");
                string itemStr = "BreqStatusName='" + entity.BreqStatusName + "'";
                if (tmpa.IndexOf(itemStr) < 0)
                    tmpa.Add(itemStr);
            }
            #endregion

            #region 科主任审核
            if (entity.BreqStatusID.ToString() == BreqFormStatus.科主任审核通过.Key)
            {
                if (serverEntity.BreqStatusID.ToString() != BreqFormStatus.上级审核通过.Key && serverEntity.BreqStatusID.ToString() != BreqFormStatus.医务处审批退回.Key)
                {
                    return false;
                }
                if (!entity.DirectorID.HasValue)
                {
                    entity.DirectorID = empID;
                    entity.DirectorName = empName;
                }

                entity.DirectorTime = DateTime.Now;
                tmpa.Add("DirectorID=" + entity.DirectorID + " ");
                tmpa.Add("DirectorName='" + entity.DirectorName + "'");
                tmpa.Add("DirectorTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("DirectorMemo='" + entity.DirectorMemo + "'");
                string itemStr = "BreqStatusName='" + entity.BreqStatusName + "'";
                if (tmpa.IndexOf(itemStr) < 0)
                    tmpa.Add(itemStr);
            }

            if (entity.BreqStatusID.ToString() == BreqFormStatus.科主任审核退回.Key)
            {
                if (serverEntity.BreqStatusID.ToString() != BreqFormStatus.上级审核通过.Key && serverEntity.BreqStatusID.ToString() != BreqFormStatus.医务处审批退回.Key)
                {
                    return false;
                }
                entity.DirectorID = empID;
                entity.DirectorName = empName;
                entity.DirectorTime = DateTime.Now;
                tmpa.Add("DirectorID=" + entity.DirectorID + " ");
                tmpa.Add("DirectorName='" + entity.DirectorName + "'");
                tmpa.Add("DirectorTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("DirectorMemo='" + entity.DirectorMemo + "'");
                string itemStr = "BreqStatusName='" + entity.BreqStatusName + "'";
                if (tmpa.IndexOf(itemStr) < 0)
                    tmpa.Add(itemStr);
            }
            #endregion

            #region 医务处审批
            if (entity.BreqStatusID.ToString() == BreqFormStatus.医务处审批通过.Key)
            {
                if (serverEntity.BreqStatusID.ToString() != BreqFormStatus.科主任审核通过.Key)
                {
                    return false;
                }
                if (!entity.MedicalID.HasValue)
                {
                    entity.MedicalID = empID;
                    entity.MedicalName = empName;
                }
                entity.MedicalTime = DateTime.Now;
                tmpa.Add("MedicalID=" + entity.MedicalID + " ");
                tmpa.Add("MedicalName='" + entity.MedicalName + "'");
                tmpa.Add("MedicalTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("MedicalMemo='" + entity.MedicalMemo + "'");
                string itemStr = "BreqStatusName='" + entity.BreqStatusName + "'";
                if (tmpa.IndexOf(itemStr) < 0)
                    tmpa.Add(itemStr);
            }

            if (entity.BreqStatusID.ToString() == BreqFormStatus.医务处审批退回.Key)
            {
                if (serverEntity.BreqStatusID.ToString() != BreqFormStatus.科主任审核通过.Key)
                {
                    return false;
                }
                if (!entity.MedicalID.HasValue)
                {
                    entity.MedicalID = empID;
                    entity.MedicalName = empName;
                }
                entity.MedicalTime = DateTime.Now;
                tmpa.Add("MedicalID=" + entity.MedicalID + " ");
                tmpa.Add("MedicalName='" + entity.MedicalName + "'");
                tmpa.Add("MedicalTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("MedicalMemo='" + entity.MedicalMemo + "'");
                string itemStr = "BreqStatusName='" + entity.BreqStatusName + "'";
                if (tmpa.IndexOf(itemStr) < 0)
                    tmpa.Add(itemStr);
            }
            #endregion

            #region 作废
            if (entity.BreqStatusID.ToString() == BreqFormStatus.医嘱作废.Key)
            {
                if (serverEntity.BreqStatusID.ToString() == BreqFormStatus.医嘱作废.Key)
                {
                    return false;
                }
                //医嘱申请为输血审核受理通过的,不能进行作废处理serverEntity.CheckCompleteFlag == true ||
                if (serverEntity.BReqFormFlag.ToString() == BReqFormFlag.受理通过.Key.ToString())
                {
                    return false;
                }
                if (!entity.MedicalID.HasValue)
                {
                    entity.ObsoleteID = empID;
                    entity.ObsoleteName = empName;
                }

                entity.ObsoleteTime = DateTime.Now;
                tmpa.Add("ObsoleteID=" + entity.ObsoleteID + " ");
                tmpa.Add("ObsoleteName='" + entity.ObsoleteName + "'");
                tmpa.Add("ObsoleteTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("ObsoleteMemo='" + entity.ObsoleteMemo + "'");
                tmpa.Add("ObsoleteMemoId=" + entity.ObsoleteMemoId + "");
                string itemStr = "BreqStatusName='" + entity.BreqStatusName + "'";
                if (tmpa.IndexOf(itemStr) < 0)
                    tmpa.Add(itemStr);
            }
            #endregion

            return true;
        }
        /// <summary>
        /// 24小时内的同一病人的医嘱申请信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private IList<BloodBReqForm> GetBloodBReqFormListOf24Hours(BloodBReqForm entity)
        {
            IList<BloodBReqForm> reqList = new List<BloodBReqForm>();

            if (!entity.ApplyTime.HasValue || entity.ApplyTime.Value != DateTime.Now) entity.ApplyTime = DateTime.Now;
            DateTime sdate = entity.ApplyTime.Value.AddHours(-24);
            string noInStatusStr = BreqFormStatus.医嘱暂存.Key + "," + BreqFormStatus.提交申请.Key + "," + BreqFormStatus.上级审核退回.Key + "," + BreqFormStatus.医嘱作废.Key;
            string hqlWhere = string.Format(" bloodbreqform.Visible=1 and bloodbreqform.PatNo='{0}' and bloodbreqform.CName='{1}' and bloodbreqform.ApplyTime>='{2}' and bloodbreqform.ApplyTime<='{3}' and bloodbreqform.BreqStatusID not in ({4}) and bloodbreqform.Id!='{5}'", entity.PatNo, entity.CName, sdate.ToString("yyyy-MM-dd HH:mm:ss"), entity.ApplyTime.Value.ToString("yyyy-MM-dd HH:mm:ss"), noInStatusStr, entity.Id);
            ZhiFang.Common.Log.Log.Debug(string.Format("申请单号为:{0};获取患者24小时内用血申请信息的查询条件为:{1}", entity.Id, hqlWhere));
            reqList = ((IDBloodBReqFormDao)base.DBDao).GetListByHQL(hqlWhere);
            return reqList;
        }
        private IList<BloodLargeUseItem> GetAllBloodLargeUseItemOf24Hours(ref BloodBReqForm entity, IList<BloodBReqItem> addBreqItemList, IList<BloodBReqItem> editBreqItemList)
        {
            IList<BloodLargeUseItem> useItemList = new List<BloodLargeUseItem>();
            IList<BloodClass> rloodClassList = IDBloodClassDao.LoadAll();
            IList<Bloodstyle> bloodsList = IDBloodstyleDao.LoadAll();
            Dictionary<int, List<Bloodstyle>> dicBloods = bloodsList.GroupBy(p => new
            {
                p.Id,
                p.BloodScale
            }).ToDictionary(g => g.Key.Id, g => g.ToList());

            double reqTotal = 0;
            /***
             * 先先判断当前申请单有没有参与大量用血计算的血制品
             * (1)如果有,就查找包含当前申请单及24小时内大量用血申请单进行计算;
             * (2)如果没有,当前申请单就不需要进行大量用血申请的审批,只需要上级医生进行审核,该申请单就审批完成;
             */
            //计算当前用血申请单的申请总量数
            if (addBreqItemList != null) GetReqTotalByReqDtlList(entity, rloodClassList, dicBloods, addBreqItemList, ref reqTotal);
            if (editBreqItemList != null) GetReqTotalByReqDtlList(entity, rloodClassList, dicBloods, editBreqItemList, ref reqTotal);

            BloodLargeUseItem largeUseItem1 = new BloodLargeUseItem();
            largeUseItem1.Visible = true;
            largeUseItem1.LUFID = entity.Id;
            largeUseItem1.BReqFormID = entity.Id;
            useItemList.Add(largeUseItem1);

            //24小时内的同一病人的医嘱申请信息
            if (reqTotal > 0)
            {
                ZhiFang.Common.Log.Log.Debug("申请单号为:" + entity.Id + ",包含有参与大量用血计算的血制品,申请单当次用血申请总量:" + reqTotal);
                IList<BloodBReqForm> req24List = GetBloodBReqFormListOf24Hours(entity);
                foreach (var reqFormOf24 in req24List)
                {
                    GetBloodLargeUseItemOf24Hours(rloodClassList, ref entity, ref reqTotal, ref useItemList, reqFormOf24, dicBloods);
                }
            }
            else
            {
                ZhiFang.Common.Log.Log.Debug("申请单号为:" + entity.Id + ",不包含有参与大量用血计算的血制品,不需要进行大量用血申请的审批,只需要上级医生进行审核,该申请单就审批完成;");
            }
            entity.ReqTotal = reqTotal;
            ZhiFang.Common.Log.Log.Debug("申请单号为:" + entity.Id + ",用血申请总量:" + entity.ReqTotal);
            return useItemList;
        }
        private void GetBloodLargeUseItemOf24Hours(IList<BloodClass> rloodClassList, ref BloodBReqForm curEntity, ref double reqTotal, ref IList<BloodLargeUseItem> useItemList, BloodBReqForm reqFormOf24, Dictionary<int, List<Bloodstyle>> dicBloods)
        {
            string hqlWhere = "bloodbreqitem.BReqFormID='" + reqFormOf24.Id + "'";
            IList<BloodBReqItem> breqItemList = IBBloodBReqItem.SearchListByHQL(hqlWhere);
            GetReqTotalByReqDtlList(curEntity, rloodClassList, dicBloods, breqItemList, ref reqTotal);

            BloodLargeUseItem largeUseItem1 = new BloodLargeUseItem();
            largeUseItem1.Visible = true;
            largeUseItem1.LUFID = curEntity.Id;
            largeUseItem1.BReqFormID = reqFormOf24.Id;
            useItemList.Add(largeUseItem1);
        }

        private void GetReqTotalByReqDtlList(BloodBReqForm curEntity, IList<BloodClass> rloodClassList, Dictionary<int, List<Bloodstyle>> dicBloods, IList<BloodBReqItem> breqItemList, ref double reqTotal)
        {
            foreach (var reqItem in breqItemList)
            {
                if (!reqItem.BloodNo.HasValue) continue;

                Bloodstyle bloodstyle = dicBloods[reqItem.BloodNo.Value][0];
                #region 判断血制品是否参与大量用血计算
                bool islargeUse = true;
                islargeUse = GetIslargeUse(curEntity, rloodClassList, reqItem, bloodstyle);
                if (!islargeUse) continue;
                #endregion
                if (bloodstyle != null && reqItem.BReqCount.HasValue && bloodstyle.BloodScale.HasValue)
                {
                    double reqTotal2 = reqItem.BReqCount.Value * bloodstyle.BloodScale.Value;
                    ZhiFang.Common.Log.Log.Info(string.Format("血制品名称为:{0},用血申请量为:{1},换算系数为:{2},参与大量用血计算,换算后用血量为:{3}", bloodstyle.CName, reqItem.BReqCount.Value, bloodstyle.BloodScale.Value, reqTotal2));
                    reqTotal += reqTotal2;
                }
            }
        }
        /// <summary>
        /// 判断血制品是否参与大量用血计算
        /// </summary>
        /// <param name="rloodClassList"></param>
        /// <param name="reqItem"></param>
        /// <param name="bloodstyle"></param>
        /// <returns></returns>
        private bool GetIslargeUse(BloodBReqForm curEntity, IList<BloodClass> rloodClassList, BloodBReqItem reqItem, Bloodstyle bloodstyle)
        {
            bool islargeUse = true;
            string curId = reqItem.BReqFormID;
            if (string.IsNullOrEmpty(curId))
                curId = curEntity.Id;
            string bcNo = "";
            if (bloodstyle.BloodClass != null)
            {
                bcNo = bloodstyle.BloodClass.Id;
            }
            ZhiFang.Common.Log.Log.Info(string.Format("用血申请单号为:{0},用血申请明细Id为:{1},血制品名称为:{2},用血申请量为:{3},所属血液分类编码为:{4},判断血制品是否参与大量用血计算:", curId, reqItem.Id, bloodstyle.CName, reqItem.BReqCount.Value, bcNo));
            //血制品没有维护所属血液分类信息
            if (bloodstyle.BloodClass == null)
            {
                ZhiFang.Common.Log.Log.Info(string.Format("血制品名称为:{0},未维护其所属血液分类,不参与大量用血计算!", bloodstyle.CName));
                return false;
            }
            //血制品没有维护换算系数信息
            if (islargeUse == true && !bloodstyle.BloodScale.HasValue)
            {
                ZhiFang.Common.Log.Log.Info(string.Format("血制品名称为:{0},换算系数为空,不参与大量用血计算!", bloodstyle.CName));
                return false;
            }
            //血液分类信息为空
            if (rloodClassList == null || rloodClassList.Count <= 0)
            {
                ZhiFang.Common.Log.Log.Info(string.Format("血液分类信息为空,血制品名称为:{0},不参与大量用血计算!", bloodstyle.CName));
                return false;
            }

            var tempList = rloodClassList.Where(p => p.Id == bloodstyle.BloodClass.Id && p.Visible == true);
            if (tempList != null && tempList.Count() > 0)
            {
                BloodClass bloodClass = tempList.ElementAt(0);
                //是否参与大量用血计算
                if (bloodClass.IslargeUse != "1")
                {
                    ZhiFang.Common.Log.Log.Info(string.Format("血制品名称为:{0},所属血液分类为:{1},是否参与大量用血计算值为:{2},不参与大量用血计算!", bloodstyle.CName, bloodClass.CName, bloodClass.IslargeUse));
                    return false;
                }
            }
            else
            {
                //血制品维护有血液分类关系,但对应的血液分类信息已经被物理删除
                ZhiFang.Common.Log.Log.Info(string.Format("血制品名称为:{0},所属血液分类编码为:{1},查无此血液分类信息,不参与大量用血计算!", bloodstyle.CName, bcNo));
                return false;
            }

            return islargeUse;
        }
        /// <summary>
        /// 按医嘱申请单号进行医嘱确认提交操作
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tempArray"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        public BaseResultBool EditBloodBReqFormOfConfirmApplyByReqFormId(BloodBReqForm entity, string[] tempArray, long empID, string empName)
        {
            BaseResultBool brdv = new BaseResultBool();
            if (!entity.ApplyTime.HasValue || entity.ApplyTime.Value != DateTime.Now)
            {
                entity.ReqTime = DateTime.Now;
                entity.ApplyTime = DateTime.Now;
            }
            entity.BreqStatusName = BreqFormStatus.GetStatusDic()[entity.BreqStatusID.ToString()].Name;
            BloodBReqForm oldEntity = ((IDBloodBReqFormDao)base.DBDao).Get(entity.Id);
            List<string> tmpa = tempArray.ToList();
            if (!EditBloodBReqFormStatusCheck(entity, oldEntity, tmpa, empID, empName))
            {
                brdv.success = false;
                brdv.ErrorInfo = "用血申请单ID：" + entity.Id + "的状态为：" + BreqFormStatus.GetStatusDic()[oldEntity.BreqStatusID.ToString()].Name + "！";
                return brdv;
            }
            ZhiFang.Common.Log.Log.Info("申请单号为:" + entity.Id + ",编辑保存(医嘱确认提交)处理开始");
            //24小时内申请总量计算
            IList<BloodLargeUseItem> useItemList = new List<BloodLargeUseItem>();
            if (entity.BreqStatusID.Value == long.Parse(BreqFormStatus.提交申请.Key))
            {
                entity.ReqTime = DateTime.Now;
                entity.ApplyTime = DateTime.Now;
            }

            bool hasReqTotal = false;
            //紧急用血时,在点击提交申请后,前台自动将"提交申请"状态更新为"审批完成"
            if (oldEntity.BreqStatusID.Value == long.Parse(BreqFormStatus.医嘱暂存.Key) && (entity.BreqStatusID.Value == long.Parse(BreqFormStatus.提交申请.Key) || entity.BreqStatusID.Value == long.Parse(BreqFormStatus.审批完成.Key)))
            {
                hasReqTotal = true;
            }
            else if (entity.BreqStatusID.Value == long.Parse(BreqFormStatus.提交申请.Key))
            {
                hasReqTotal = true;
            }

            if (hasReqTotal == true)
            {
                IList<BloodBReqItem> breqItemList = IBBloodBReqItem.SearchListByHQL("bloodbreqitem.BReqFormID='" + entity.Id + "'");
                useItemList = GetAllBloodLargeUseItemOf24Hours(ref entity, breqItemList, null);
                string reqTotalStr = "ReqTotal=" + entity.ReqTotal + " ";
                if (tmpa.IndexOf(reqTotalStr) < 0)
                    tmpa.Add(reqTotalStr);
            }

            //医嘱申请是否审批完成处理
            EditCheckBloodsReqTotalIsComplete(ref entity, oldEntity, tmpa);

            string itemStr = "BreqStatusName='" + entity.BreqStatusName + "'";
            if (tmpa.IndexOf(itemStr) < 0)
                tmpa.Add(itemStr);

            this.Entity = entity;
            tempArray = tmpa.ToArray();
            if (this.Update(tempArray))
            {
                //24小时内医嘱申请总量处理
                if (useItemList != null && useItemList.Count > 0)
                {
                    BaseResultDataValue brdv2 = new BaseResultDataValue();
                    brdv2 = IBBloodLargeUseItem.AddBloodLargeUseItemList(entity, useItemList);
                    if (brdv2.success == false)
                    {
                        brdv.success = brdv2.success;
                        brdv.ErrorInfo = brdv2.ErrorInfo;
                        return brdv;
                    }
                    string reviewTips = _getReviewTips(entity);
                    brdv.ErrorInfo = reviewTips;
                }
                if (entity.BreqStatusID.Value != long.Parse(BreqFormStatus.医嘱暂存.Key))
                    AddSCOperation(entity, empID, empName, entity.BreqStatusID.Value, entity.Memo);
            }
            else
            {
                brdv.success = false;
                brdv.ErrorInfo = "用血申请单ID：" + entity.Id + "更新失败！";
            }
            ZhiFang.Common.Log.Log.Info("申请单号为:" + entity.Id + ",编辑保存(医嘱确认提交)处理结束,处理结果为:" + brdv.success + "!");
            return brdv;
        }
        /// <summary>
        /// 判断及处理用血申请是否审批完成
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="oldEntity"></param>
        /// <param name="tmpa"></param>
        public void EditCheckBloodsReqTotalIsComplete(ref BloodBReqForm entity, BloodBReqForm oldEntity, List<string> tmpa)
        {
            double reqTotal = 0;
            if (entity.ReqTotal.HasValue) reqTotal = entity.ReqTotal.Value;
            if (reqTotal <= 0 && oldEntity.ReqTotal.HasValue) reqTotal = oldEntity.ReqTotal.Value;
            if (!entity.ReqTotal.HasValue) entity.ReqTotal = reqTotal;

            if (entity.BreqStatusID.Value == long.Parse(BreqFormStatus.上级审核通过.Key))
            {
                if (entity.ReqTotal < 800)
                {
                    entity.CheckCompleteFlag = true;
                }
            }
            else if (entity.BreqStatusID.Value == long.Parse(BreqFormStatus.科主任审核通过.Key))
            {
                if (entity.ReqTotal >= 800 && entity.ReqTotal < 1600)
                {
                    entity.CheckCompleteFlag = true;
                }
            }
            else if (entity.BreqStatusID.Value == long.Parse(BreqFormStatus.医务处审批通过.Key))
            {
                entity.CheckCompleteFlag = true;
            }
            else if (entity.BreqStatusID.Value == long.Parse(BreqFormStatus.审批完成.Key))
            {
                //抢救用血情况
                entity.CheckCompleteFlag = true;
            }

            if (entity.CheckCompleteFlag == true)
            {
                entity.CheckCompleteFlag = true;
                entity.CheckCompleteTime = DateTime.Now;
                entity.Postflag = 1;
                if (tmpa.IndexOf("Postflag=1") < 0)
                    tmpa.Add("Postflag=1");
                string itemStr1 = "CheckCompleteFlag=" + (entity.CheckCompleteFlag == true ? 1 : 0) + "";
                if (tmpa.IndexOf(itemStr1) < 0)
                    tmpa.Add(itemStr1);
                string itemStr2 = "CheckCompleteTime='" + entity.CheckCompleteTime.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                if (tmpa.IndexOf(itemStr2) < 0)
                    tmpa.Add(itemStr2);
            }
        }
        private void AddSCOperation(BloodBReqForm entity, long empID, string empName, long status, string memo)
        {
            SCOperation sco = new SCOperation();
            long id = 0;
            long.TryParse(entity.Id, out id);
            if (id <= 0) return;

            sco.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
            sco.LabID = entity.LabID;
            sco.BobjectID = id;
            sco.CreatorID = empID;
            sco.CreatorName = empName;
            sco.BusinessModuleCode = "BloodBReqForm";
            sco.Memo = memo;
            sco.IsUse = true;
            sco.Type = status;
            sco.DataUpdateTime = DateTime.Now;
            sco.TypeName = BreqFormStatus.GetStatusDic()[status.ToString()].Name;
            IBSCOperation.Entity = sco;
            IBSCOperation.Add();
        }
        #endregion

        #region 医嘱打印
        private DataSet SearchReportDataSet(string id, ref BloodBReqForm reqForm)
        {
            DataSet dataSet = new DataSet();
            //reqForm = this.Get(id);
            IList<BloodBReqForm> entityList = ((IDBloodBReqFormDao)base.DBDao).SearchBloodBReqFormListByJoinHql("bloodbreqform.Id='" + id + "'", "", -1, -1);
            if (entityList != null && entityList.Count > 0)
                reqForm = ClassMapperHelp.GetMapper<BloodBReqForm, BloodBReqForm>(entityList[0]);
            IList<BloodBReqItem> dtlList = new List<BloodBReqItem>();
            IList<BloodBReqFormResult> resultList = new List<BloodBReqFormResult>();
            if (reqForm != null)
            {
                dtlList = IBBloodBReqItem.SearchBloodBReqItemListByJoinHql("bloodbreqitem.BReqFormID='" + id + "'", "", "", "", -1, -1);
                resultList = IBBloodBReqFormResult.SearchBloodBReqFormResultListByJoinHql("bloodbreqformresult.BReqFormID='" + id + "'", "", "", -1, -1);
                dataSet.DataSetName = "ZhiFang.BloodTransfusion";
            }
            //血制品信息加入申请主单信息自定义项里
            StringBuilder strb = new StringBuilder();
            dtlList = dtlList.OrderBy(p => p.BloodOrder).ToList();
            foreach (var reqItem in dtlList)
            {
                strb.Append(reqItem.BloodCName);
                strb.Append(reqItem.BReqCount);
                strb.Append(reqItem.BloodUnitCName + ";");
            }
            reqForm.BloodListStr = strb.ToString();
            reqForm.BUseTimeTypeCName = (reqForm.BUseTimeTypeID == 1 ? "是" : "否");
            List<BloodBReqForm> docList = new List<BloodBReqForm>();
            if (reqForm.ApplyTime.HasValue) reqForm.ApplyTime = DateTime.Parse(reqForm.ApplyTime.Value.ToString("yyyy-MM-dd HH: mm:ss"));

            docList.Add(reqForm);
            DataTable docDt = ReportBTemplateHelp.ToDataTable<BloodBReqForm>(docList, null);
            docDt.TableName = "BloodBReqForm";
            dataSet.Tables.Add(docDt);

            DataTable dtDtlBReqItem = ReportBTemplateHelp.ToDataTable<BloodBReqItem>(dtlList, null);
            if (dtDtlBReqItem != null)
            {
                dtDtlBReqItem.TableName = "BloodBReqItem";
                dataSet.Tables.Add(dtDtlBReqItem);
            }
            resultList = resultList.OrderBy(p => p.BTestItemNo).ToList();
            DataTable resultListList = ReportBTemplateHelp.ToDataTable<BloodBReqFormResult>(resultList, null);
            if (resultListList != null)
            {
                resultListList.TableName = "BloodBReqFormResult";
                dataSet.Tables.Add(resultListList);
            }

            return dataSet;
        }
        public Stream SearchPdfReportOfTypeById(string reaReportClass, string id, long labID, string labCName, long empID, string empName, string breportType, string frx, ref string fileName)
        {
            Stream stream = null;
            BloodBReqForm reqForm = null;
            DataSet dataSet = SearchReportDataSet(id, ref reqForm);

            if (reqForm == null)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取用血申请单清单数据信息为空!");
            }
            if (dataSet.Tables["BloodBReqItem"].Rows.Count <= 0)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取用血申请单清单血制品信息为空!");
            }

            IList<BloodBReqFormResult> resultList = IBBloodBReqFormResult.SearchBloodBReqFormResultListByJoinHql("bloodbreqformresult.BReqFormID='" + id + "'", "", "", -1, -1);
            fileName = reqForm.Id + ".pdf";
            if (reaReportClass == ReaReportClass.Frx.Key)
            {
                //获取Frx模板
                if (string.IsNullOrEmpty(frx))
                    frx = "医嘱申请单.frx";

                stream = FrxToPdfReportHelp.SavePdfReport(dataSet, reqForm.LabID, fileName, FrxToImageReportHelp.PublicTemplateDir, BTemplateType.GetStatusDic()[BTemplateType.医嘱申请.Key].Name, frx, false);
            }
            return stream;
        }
        public BaseResultBool SearchBusinessReportOfPDFJSById(string reaReportClass, string id, long labID, string labCName, long empID, string empName, string breportType, string frx, ref string fileName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            Stream stream = null;
            BloodBReqForm reqForm = null;
            DataSet dataSet = SearchReportDataSet(id, ref reqForm);

            if (reqForm == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "获取用血申请单清单数据信息为空!";
                return tempBaseResultBool;
            }
            if (dataSet.Tables["BloodBReqItem"].Rows.Count <= 0)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "获取用血申请单清单血制品信息为空!";
                return tempBaseResultBool;
            }

            IList<BloodBReqFormResult> resultList = IBBloodBReqFormResult.SearchBloodBReqFormResultListByJoinHql("bloodbreqformresult.BReqFormID='" + id + "'", "", "", -1, -1);
            fileName = reqForm.Id + ".pdf";
            if (reaReportClass == ReaReportClass.Frx.Key)
            {
                //获取Frx模板
                if (string.IsNullOrEmpty(frx))
                    frx = "医嘱申请单.frx";

                stream = FrxToPdfReportHelp.SavePdfReport(dataSet, reqForm.LabID, fileName, FrxToImageReportHelp.PublicTemplateDir, BTemplateType.GetStatusDic()[BTemplateType.医嘱申请.Key].Name, frx, false);
                if (stream != null)
                {
                    tempBaseResultBool.success = true;
                    tempBaseResultBool.BoolInfo = fileName;
                }
                stream.Close();
            }
            return tempBaseResultBool;
        }
        public BaseResultDataValue SearchImageReportToBase64String(string reaReportClass, string id, long labID, string labCName, long empID, string empName, string breportType, string frx, ref string fileName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            string base64String = null;
            BloodBReqForm reqForm = null;
            DataSet dataSet = SearchReportDataSet(id, ref reqForm);

            if (reqForm == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "获取用血申请单清单数据信息为空!";
                return brdv;
            }
            if (dataSet.Tables["BloodBReqItem"].Rows.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "获取用血申请单清单血制品信息为空!";
                return brdv;
            }

            IList<BloodBReqFormResult> resultList = IBBloodBReqFormResult.SearchBloodBReqFormResultListByJoinHql("bloodbreqformresult.BReqFormID='" + id + "'", "", "", -1, -1);
            fileName = reqForm.Id + ".png";
            if (reaReportClass == ReaReportClass.Frx.Key)
            {
                //获取Frx模板
                if (string.IsNullOrEmpty(frx))
                    frx = "医嘱申请单.frx";

                base64String = FrxToImageReportHelp.GetImageReportToBase64String(dataSet, reqForm.LabID, fileName, FrxToImageReportHelp.PublicTemplateDir, BTemplateType.GetStatusDic()[BTemplateType.医嘱申请.Key].Name, frx, false);
                //ZhiFang.Common.Log.Log.Debug("base64String:" + base64String);
                if (string.IsNullOrEmpty(base64String))
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "获取用血申请单清单的报告信息为空!";
                    return brdv;
                }
                brdv.ResultDataValue = "{data:" + "\"" + "data:image/png;base64," + base64String + "\"" + "}";
            }
            return brdv;
        }
        public BaseResultBool DeleteBloodBReqForm(string reqFormId)
        {
            BaseResultBool brdv = new BaseResultBool();
            BloodBReqForm oldEntity = ((IDBloodBReqFormDao)base.DBDao).Get(reqFormId);
            if (oldEntity == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "用血申请单ID：" + reqFormId + ",不存在!";
                return brdv;
            }
            //审批完成后,用血申请单只能进行作废处理,不能进行物理删除;
            if (oldEntity.CheckCompleteFlag == true)
            {
                brdv.success = false;
                brdv.ErrorInfo = "用血申请单ID：" + reqFormId + ",已审批完成,不允许删除!";
                return brdv;
            }
            //HIS数据标志为"上传成功"
            if (oldEntity.ToHisFlag.HasValue && oldEntity.ToHisFlag.Value.ToString() == BreqFormReqToHisFlag.上传成功.Key)
            {
                brdv.success = false;
                brdv.ErrorInfo = "用血申请单ID：" + reqFormId + ",数据已返回HIS成功,不允许删除!";
                return brdv;
            }
            //其他不能删除的判断条件:输血科已受理?

            //删除医嘱申请主单的相关业务数据
            IBSCOperation.DeleteByHql(" from SCOperation scoperation where scoperation.BobjectID ='" + reqFormId + "'");
            IBBloodBReqItemResult.DeleteByHql(" from BloodBReqItemResult bloodbreqitemresult where bloodbreqitemresult.BReqFormID ='" + reqFormId + "'");
            IBBloodBReqFormResult.DeleteByHql(" from BloodBReqFormResult bloodbreqformresult where bloodbreqformresult.BReqFormID ='" + reqFormId + "'");
            IBBloodLargeUseItem.DeleteByHql(" from BloodLargeUseItem bloodlargeuseitem where bloodlargeuseitem.LUFID ='" + reqFormId + "'");
            IBBloodBReqItem.DeleteByHql(" from BloodBReqItem bloodbreqitem where bloodbreqitem.BReqFormID ='" + reqFormId + "'");
            //删除医嘱申请主单信息
            this.Entity = oldEntity;
            brdv.success = this.Remove();

            return brdv;
        }
        public BaseResultBool UpdateBloodBReqFormPrintTotalById(string id)
        {
            BaseResultBool brdv = new BaseResultBool();
            bool result = ((IDBloodBReqFormDao)base.DBDao).UpdateBloodBReqFormPrintTotalById(id);
            if (result == false)
            {
                brdv.success = false;
                brdv.ErrorInfo = "用血申请单ID：" + id + ",更新打印总数失败!";
                return brdv;
            }

            return brdv;
        }
        #endregion

        #endregion

        #region 护士站
        public EntityList<BloodBReqForm> SearchBloodBReqFormListByBBagCodeAndHql(string wardId, string where, string scanCodeField, string bbagCode, string sort, int page, int limit)
        {
            EntityList<BloodBReqForm> entityList = new EntityList<BloodBReqForm>();
            //获取到传入病区及其所有的下级科室编码信息
            if (!string.IsNullOrEmpty(wardId))
            {
                IList<Department> deptList = IBDepartment.GetChildList(int.Parse(wardId));
                if (deptList.Count > 0)
                {
                    StringBuilder deptStr = new StringBuilder();
                    //deptStr.Append(" 1=1 ");
                    foreach (Department item in deptList)
                    {
                        if (deptStr.ToString().Length > 0)
                            deptStr.Append(" or bloodbreqform.DeptNo=" + item.Id);
                        else
                            deptStr.Append(" bloodbreqform.DeptNo=" + item.Id);
                    }
                    if (string.IsNullOrEmpty(where))
                        where = " 1=1 ";
                    where = where + " and (" + deptStr.ToString() + ")";
                    ZhiFang.Common.Log.Log.Debug("wardId;" + wardId + ",where:" + where.ToString());
                }
            }

            if (string.IsNullOrEmpty(bbagCode))
            {
                entityList = this.SearchBloodBReqFormEntityListByHql(where, sort, page, limit);
            }
            else
            {
                entityList = ((IDBloodBReqFormDao)base.DBDao).SearchBloodBReqFormListByBBagCodeAndHql(where, scanCodeField, bbagCode, sort, page, limit);
                if(entityList.list==null) entityList.list=new List<BloodBReqForm>();

                for (int i = 0; i < entityList.list.Count; i++)
                {
                    if (entityList.list[i] == null) continue;

                    if (entityList.list[i].BReqTypeID.HasValue)
                        entityList.list[i].BReqTypeCName = getBReqTypeCName(entityList.list[i].BReqTypeID.Value);
                    if (entityList.list[i].UseTypeID.HasValue)
                        entityList.list[i].UseTypeCName = getUseTypeCName(entityList.list[i].UseTypeID.Value.ToString());
                    if (entityList.list[i].DeptNo.HasValue)
                        entityList.list[i].DeptCName = getDeptCName(entityList.list[i].DeptNo.Value);
                    if (entityList.list[i].DoctorNo.HasValue)
                        entityList.list[i].DoctorCName = getDoctorCName(entityList.list[i].DoctorNo.Value);
                    if (entityList.list[i].BreqStatusID.HasValue)
                        entityList.list[i].DoctorCName = getDoctorCName(entityList.list[i].DoctorNo.Value);
                }
            }

            return entityList;
        }
        #endregion


        #region 按申请信息建立病区与科室关系
        public BaseResultBool AddWarpAndDept()
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            IList<Department> tempList = IBDepartment.SearchListByHQL("(department.ParentID is null or department.ParentID<=0)");
            if (tempList.Count > 0)
            {
                StringBuilder strb = new StringBuilder();
                //strb.Append(" 1=1 (");
                //foreach (Department dept in tempList)
                //{
                //    strb.Append(" or bloodbreqform.DeptNo=" + dept.Id);
                //}
                //strb.Append(") ");
                IList<WarpAndDeptVO> ovList = ((IDBloodBReqFormDao)base.DBDao).GetWarpAndDeptList(strb.ToString(), "", -1, -1);
                IList<Department> deptList = IDDepartmentDao.LoadAll();
                foreach (Department dept in tempList)
                {
                    //
                    var tempList2 = ovList.Where(p => p.DeptNo == dept.Id);
                    Department warp = null;
                    if (tempList2.Count() > 0)
                    {
                        WarpAndDeptVO vo = tempList2.ElementAt(0);
                        if (!string.IsNullOrEmpty(vo.WardNo))
                        {
                            var warpList = deptList.Where(p => p.Id == int.Parse(vo.WardNo));
                            if (warpList.Count() > 0)
                                warp = warpList.ElementAt(0);
                        }
                        else if (!string.IsNullOrEmpty(vo.HisWardNo))
                        {
                            var warpList = deptList.Where(p => p.Code1 == vo.HisWardNo || p.Code2 == vo.HisWardNo || p.Code3 == vo.HisWardNo || p.Code4 == vo.HisWardNo || p.Code5 == vo.HisWardNo);
                            if (warpList.Count() > 0)
                                warp = warpList.ElementAt(0);
                        }
                    }
                    if (warp != null)
                    {
                        //建立病区科室上下级关系
                        ZhiFang.Common.Log.Log.Info("建立病区科室上下级关系,病区编码为:" + warp.Id + ",科室编码为:" + dept.Id);
                        dept.ParentID = warp.Id;
                        IDDepartmentDao.Save(dept);
                    }
                }
            }

            return tempBaseResultBool;
        }
        #endregion
    }
}