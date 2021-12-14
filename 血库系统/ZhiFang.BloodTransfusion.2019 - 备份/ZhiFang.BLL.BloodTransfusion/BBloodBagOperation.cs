
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.BloodTransfusion;
using ZhiFang.Entity.Base;
using ZhiFang.IDAO.NHB.BloodTransfusion;

namespace ZhiFang.BLL.BloodTransfusion
{
    /// <summary>
    ///
    /// </summary>
    public class BBloodBagOperation : BaseBLL<BloodBagOperation>, ZhiFang.IBLL.BloodTransfusion.IBBloodBagOperation
    {
        IBBloodBOutForm IBBloodBOutForm { get; set; }
        IBBloodBOutItem IBBloodBOutItem { get; set; }
        IBBloodBagOperationDtl IBBloodBagOperationDtl { get; set; }
        IDBloodBReqFormDao IDBloodBReqFormDao { get; set; }
        IDBloodstyleDao IDBloodstyleDao { get; set; }
        IDBloodBOutItemDao IDBloodBOutItemDao { get; set; }
        IDBDictDao IDBDictDao { get; set; }
        IDBloodTransFormDao IDBloodTransFormDao { get; set; }

        #region 公共部分
        /// <summary>
        /// 血袋保存时验证
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private BaseResultDataValue EditVerificationBloodBagOperation(ref BloodBagOperation entity, string saveType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = true;

            if (entity.BloodBReqForm == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数申请单信息(BloodBReqForm)为空!";
                return brdv;
            }
            if (entity.BloodBOutForm == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数发血单信息(BloodBOutForm)为空!";
                return brdv;
            }
            if (entity.BloodBOutItem == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数发血明细信息(BloodBOutItem)为空!";
                return brdv;
            }
            if (entity.Bloodstyle == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数血袋信息(Bloodstyle)为空!";
                return brdv;
            }
            if (!entity.DeptID.HasValue)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数所属科室编码(DeptID)为空!";
                return brdv;
            }
            if (string.IsNullOrEmpty(entity.BBagCode))
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数血袋号(BBagCode)为空!";
                return brdv;
            }

            BloodBReqForm bloodBReqForm = IDBloodBReqFormDao.Get(entity.BloodBReqForm.Id);
            if (bloodBReqForm == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "获取申请单号为" + entity.BloodBReqForm.Id + "的信息为空!";
                return brdv;
            }
            entity.BloodBReqForm = bloodBReqForm;

            BloodBOutForm bloodBOutForm = IBBloodBOutForm.Get(entity.BloodBOutForm.Id);
            if (bloodBOutForm == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "获取发血单号为" + entity.BloodBOutForm.Id + "的信息为空!";
                return brdv;
            }
            entity.BloodBOutForm = bloodBOutForm;

            BloodBOutItem bloodBOutItem = IBBloodBOutItem.Get(entity.BloodBOutItem.Id);
            if (bloodBOutItem == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "获取发血明细号为" + entity.BloodBOutItem.Id + "的信息为空!";
                return brdv;
            }
            entity.BloodBOutItem = bloodBOutItem;

            Bloodstyle bloodstyle = IDBloodstyleDao.Get(entity.Bloodstyle.Id);
            if (bloodstyle == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "获取血袋号为" + entity.Bloodstyle.Id + "的信息为空!";
                return brdv;
            }
            entity.Bloodstyle = bloodstyle;

            //判断是否已经进行过登记
            if (saveType == "add")
            {
                StringBuilder outDtlHql = new StringBuilder();
                outDtlHql.Append(" bloodboutitem.Id='" + entity.BloodBOutItem.Id + "'");
                string info = "";
                if (entity.BagOperTypeID == long.Parse(BloodBagOperationType.交接登记.Key))
                {
                    outDtlHql.Append(" and bloodboutitem.HandoverCompletion=" + int.Parse(HandoverCompletion.交接完成.Key) + "");
                    info = "已完成交接登记!";
                }
                else if (entity.BagOperTypeID == long.Parse(BloodBagOperationType.回收登记.Key))
                {
                    outDtlHql.Append(" and bloodboutitem.RecoverCompletion=" + int.Parse(RecoverCompletion.回收完成.Key) + "");
                    info = "已完成回收登记!";
                }
                IList<BloodBOutItem> outDtlList = IDBloodBOutItemDao.GetListByHQL(outDtlHql.ToString());
                if (outDtlList.Count > 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "血袋号为" + entity.Bloodstyle.Id + "," + info;
                    return brdv;
                }
            }

            return brdv;
        }

        private BaseResultDataValue EditBloodBOutItem(BloodBagOperation entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = true;

            if (entity.BloodBOutItem.DataTimeStamp == null)
            {
                entity.BloodBOutItem = IBBloodBOutItem.Get(entity.BloodBOutItem.Id);
            }
            if (entity.BagOperTypeID == long.Parse(BloodBagOperationType.交接登记.Key))
            {
                entity.BloodBOutItem.HandoverCompletion = int.Parse(HandoverCompletion.交接完成.Key);
            }
            else if (entity.BagOperTypeID == long.Parse(BloodBagOperationType.领用确认登记.Key))
            {
                entity.BloodBOutItem.ConfirmCompletion = int.Parse(ConfirmCompletion.全部领用.Key);
            }
            else if (entity.BagOperTypeID == long.Parse(BloodBagOperationType.回收登记.Key))
            {
                entity.BloodBOutItem.RecoverCompletion = int.Parse(RecoverCompletion.回收完成.Key);
            }
            IBBloodBOutItem.Entity = entity.BloodBOutItem;
            bool result = IBBloodBOutItem.Edit();
            if (!result)
            {
                brdv.success = false;
                brdv.ErrorInfo = "更新血袋发血明细的" + BloodBagOperationType.GetStatusDic()[entity.BagOperTypeID.ToString()].Name + ",登记完成度保存失败!";
                return brdv;
            }
            return brdv;
        }
        private BaseResultDataValue EditBloodBOutFormOfHandover(BloodBagOperation entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = true;

            if (entity.BloodBOutForm.DataTimeStamp == null)
            {
                entity.BloodBOutForm = IBBloodBOutForm.Get(entity.BloodBOutForm.Id);
            }
            StringBuilder outDtlHql = new StringBuilder();
            outDtlHql.Append(" bloodboutitem.BloodBOutForm.Id='" + entity.BloodBOutForm.Id + "'");
            outDtlHql.Append(" and (bloodboutitem.HandoverCompletion is null or bloodboutitem.HandoverCompletion!=" + int.Parse(HandoverCompletion.交接完成.Key)+") ");
            outDtlHql.Append(" and bloodboutitem.Id!='" + entity.BloodBOutItem.Id + "'");
            IList<BloodBOutItem> outDtlList = IDBloodBOutItemDao.GetListByHQL(outDtlHql.ToString());
            ZhiFang.Common.Log.Log.Debug(string.Format("发血单号为:{0},获取交接登记完成度未完成的记录为:{1}", entity.BloodBOutForm.Id, outDtlList.Count));
            if (outDtlList == null || outDtlList.Count <= 0)
            {
                entity.BloodBOutForm.HandoverCompletion = int.Parse(HandoverCompletion.交接完成.Key);
            }
            else
            {
                entity.BloodBOutForm.HandoverCompletion = int.Parse(HandoverCompletion.部分交接.Key);
            }

            IBBloodBOutForm.Entity = entity.BloodBOutForm;
            bool result = IBBloodBOutForm.Edit();
            if (!result)
            {
                brdv.success = false;
                brdv.ErrorInfo = "更新血袋发血主单的" + BloodBagOperationType.GetStatusDic()[entity.BagOperTypeID.ToString()].Name + ",交接完成度保存失败!";
                return brdv;
            }
            return brdv;
        }
        #endregion

        #region 血制品交接登记
        public BaseResultDataValue AddBloodBagOperationAndDtlOfHandover(BloodBagOperation entity, IList<BloodBagOperationDtl> bloodBagOperationDtlList, long empID, string empName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = true;

            if (!entity.BagOperTypeID.HasValue)
                entity.BagOperTypeID = long.Parse(BloodBagOperationType.交接登记.Key);
            entity.BagOperTime = DateTime.Now;
            entity.DataUpdateTime = DateTime.Now;
            entity.IsVisible = true;

            brdv = EditVerificationBloodBagOperation(ref entity, "add");
            if (!brdv.success)
            {
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo);
                return brdv;
            }

            if (!entity.BagOperResultID.HasValue)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数交接记录结果ID(BagOperResultID)为空!";
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo);
                return brdv;
            }
            if (string.IsNullOrEmpty(entity.BBagCode) && entity.BloodBOutItem != null)
            {
                entity.BBagCode = entity.BloodBOutItem.BBagCode;
            }
            if (string.IsNullOrEmpty(entity.PCode) && entity.BloodBOutItem != null)
            {
                entity.PCode = entity.BloodBOutItem.Pcode;
            }
            this.Entity = entity;
            bool result = this.Add();
            if (!result)
            {
                brdv.success = false;
                brdv.ErrorInfo = "血制品交接登记保存失败!";
                return brdv;
            }
            //交接记录项登记
            brdv = IBBloodBagOperationDtl.AddDtlListOfHandover(entity, bloodBagOperationDtlList, empID, empName);
            if (!brdv.success)
            {
                //return brdv;
                throw new Exception(brdv.ErrorInfo);
            }

            //更新发血明细记录的交接登记完成度
            entity.BloodBOutItem.HandoverCompletion = int.Parse(HandoverCompletion.交接完成.Key);
            brdv = EditBloodBOutItem(entity);
            if (!brdv.success)
            {
                //return brdv;
                throw new Exception(brdv.ErrorInfo);
            }

            //更新发血主单的交接登记完成度
            //entity.BloodBOutItem.HandoverCompletion = int.Parse(HandoverCompletion.交接完成.Key);
            brdv = EditBloodBOutFormOfHandover(entity);
            if (!brdv.success)
            {
                //return brdv;
                throw new Exception(brdv.ErrorInfo);
            }

            return brdv;
        }
        public EntityList<BloodBagOperation> SearchBloodBagOperationAndDtlOfHandoverByHQL(string where, string sort, int page, int count)
        {
            EntityList<BloodBagOperation> tempEntityList = new EntityList<BloodBagOperation>();
            tempEntityList = this.SearchListByHQL(where, sort, page, count);
            if (tempEntityList.count <= 0) return tempEntityList;

            for (int i = 0; i < tempEntityList.list.Count; i++)
            {
                long id = tempEntityList.list[i].Id;
                IList<BloodBagOperationDtl> dtlList = IBBloodBagOperationDtl.SearchListByHQL("bloodbagoperationdtl.BloodBagOperation.Id=" + id);
                for (int j = 0; j < dtlList.Count; j++)
                {
                    string code = dtlList[j].BDict.BDictType.DictTypeCode;
                    if (code.ToLower() == "bloodappearance")
                    {
                        tempEntityList.list[i].BloodAppearance = dtlList[j];//血袋外观信息
                    }
                    else if (code.ToLower() == "bloodintegrity")
                    {
                        tempEntityList.list[i].BloodIntegrity = dtlList[j];//血袋完整性
                    }
                }
            }

            return tempEntityList;
        }
        public BloodBagHandoverVO GetBloodBagHandoverVO(long id)
        {
            BloodBagHandoverVO vo = new BloodBagHandoverVO();
            vo.BloodBagHandover = this.Get(id);
            if (vo.BloodBagHandover != null)
            {
                IList<BloodBagOperationDtl> dtlList = IBBloodBagOperationDtl.SearchListByHQL("bloodbagoperationdtl.BloodBagOperation.Id=" + id);
                for (int j = 0; j < dtlList.Count; j++)
                {
                    string code = dtlList[j].BDict.BDictType.DictTypeCode;
                    if (code.ToLower() == "bloodappearance")
                    {
                        vo.BloodAppearance = dtlList[j];//血袋外观信息
                    }
                    else if (code.ToLower() == "bloodintegrity")
                    {
                        vo.BloodIntegrity = dtlList[j];//血袋完整性
                    }
                }
            }
            return vo;
        }
        public BaseResultBool EditBloodBagHandoverVO(BloodBagHandoverVO entity)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (entity == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "传入的参数(entity)为空!";
                return tempBaseResultBool;
            }
            if (entity.BloodBagHandover == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "传入的参数(BloodBagHandover)为空!";
                return tempBaseResultBool;
            }

            if (entity.BloodAppearance == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "传入的参数(BloodAppearance)为空!";
                return tempBaseResultBool;
            }
            if (!entity.BloodBagHandover.BagOperTime.HasValue)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "传入的参数(登记时间)为空!";
                return tempBaseResultBool;
            }
            if (entity.BloodAppearance.BDict == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "传入的参数(BloodAppearance.BDict)为空!";
                return tempBaseResultBool;
            }
            entity.BloodAppearance.BDict = IDBDictDao.Get(entity.BloodAppearance.BDict.Id);

            if (entity.BloodIntegrity == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "传入的参数(BloodIntegrity)为空!";
                return tempBaseResultBool;
            }
            if (entity.BloodIntegrity.BDict == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "传入的参数(BloodIntegrity.BDict)为空!";
                return tempBaseResultBool;
            }
            entity.BloodIntegrity.BDict = IDBDictDao.Get(entity.BloodIntegrity.BDict.Id);

            //获取服务器的血袋接收登记当前信息
            BloodBagOperation bloodBagHandover = this.Get(entity.BloodBagHandover.Id);
            if (bloodBagHandover == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "不存在血袋接收登记信息!";
                return tempBaseResultBool;
            }
            //血袋外观信息
            BloodBagOperationDtl bloodAppearance = IBBloodBagOperationDtl.Get(entity.BloodAppearance.Id);
            if (bloodAppearance == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "不存在血袋外观信息!";
                return tempBaseResultBool;
            }
            //血袋完整性
            BloodBagOperationDtl bloodIntegrity = IBBloodBagOperationDtl.Get(entity.BloodIntegrity.Id);
            if (bloodIntegrity == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "不存在血袋完整性信息!";
                return tempBaseResultBool;
            }

            //更新血袋接收登记时间
            bloodBagHandover.BagOperTime = entity.BloodBagHandover.BagOperTime;
            this.Entity = bloodBagHandover;
            this.Edit();

            //更新血袋外观信息
            bloodAppearance.BDict = entity.BloodAppearance.BDict;
            IBBloodBagOperationDtl.Entity = bloodAppearance;
            IBBloodBagOperationDtl.Edit();

            //更新血袋完整性信息
            bloodIntegrity.BDict = entity.BloodIntegrity.BDict;
            IBBloodBagOperationDtl.Entity = bloodIntegrity;
            IBBloodBagOperationDtl.Edit();

            return tempBaseResultBool;
        }
        #endregion

        #region 血袋回收登记
        public BaseResultDataValue AddBloodBagOperationListOfRecycle(IList<BloodBagOperation> bloodBagOperationList, bool isHasTrans, long empID, string empName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = true;

            //先进行验证及实体赋值处理
            for (int i = 0; i < bloodBagOperationList.Count; i++)
            {
                BloodBagOperation entity = bloodBagOperationList[i];
                //entity.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                if (!entity.BagOperTypeID.HasValue)
                    entity.BagOperTypeID = long.Parse(BloodBagOperationType.回收登记.Key);
                brdv = EditVerificationBloodBagOperation(ref entity, "add");
                if (!brdv.success)
                {
                    ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo);
                    return brdv;
                }
                //是否输血过程记录登记后才能血袋回收登记
                if (isHasTrans == true)
                {
                    IList<BloodTransForm> transList = IDBloodTransFormDao.GetListByHQL("bloodtransform.BloodBOutItem.Id='" + entity.BloodBOutItem.Id + "' and bloodtransform.Bloodstyle.Id='" + entity.Bloodstyle.Id + "'");
                    if (transList == null || transList.Count <= 0)
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = "血袋号为" + entity.BBagCode + ",未进行输血过程登记,不能进行血袋回收!";
                        return brdv;
                    }
                }
                bloodBagOperationList[i] = entity;
            }
            #region 血袋回收登记
            foreach (BloodBagOperation entity in bloodBagOperationList)
            {
                if (!entity.BagOperTypeID.HasValue)
                    entity.BagOperTypeID = long.Parse(BloodBagOperationType.回收登记.Key);
                entity.BagOperTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                entity.IsVisible = true;
                if (string.IsNullOrEmpty(entity.BBagCode) && entity.BloodBOutItem != null)
                {
                    entity.BBagCode = entity.BloodBOutItem.BBagCode;
                }
                if (string.IsNullOrEmpty(entity.PCode) && entity.BloodBOutItem != null)
                {
                    entity.PCode = entity.BloodBOutItem.Pcode;
                }
                this.Entity = entity;
                bool result = this.Add();
                if (!result)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "血袋回收登记保存失败!";
                    //break;
                    throw new Exception(brdv.ErrorInfo);
                }
                //更新发血明细记录的回收登记登记完成度
                entity.BloodBOutItem.RecoverCompletion = int.Parse(RecoverCompletion.回收完成.Key);
                brdv = EditBloodBOutItem(entity);
                if (!brdv.success)
                {
                    throw new Exception(brdv.ErrorInfo);
                }
            }
            if (!brdv.success)
            {
                throw new Exception(brdv.ErrorInfo);
                //return brdv;
            }
            #endregion
            #region 处理发血主单的回收登记登记完成度
            //对血袋回收登记集合按发血主单分组
            var groupByList = bloodBagOperationList.GroupBy(p => p.BloodBOutForm);
            foreach (var groupBy in groupByList)
            {
                BloodBOutForm outForm = groupBy.Key;
                StringBuilder outDtlHql = new StringBuilder();
                outDtlHql.Append(" bloodboutitem.BloodBOutForm.Id='" + outForm.Id + "'");
                outDtlHql.Append(" and (bloodboutitem.RecoverCompletion is null or bloodboutitem.RecoverCompletion!=" + RecoverCompletion.回收完成.Key+") ");
                foreach (BloodBagOperation item in groupBy)
                {
                    outDtlHql.Append(" and bloodboutitem.Id!='" + item.BloodBOutItem.Id + "'");
                }
                ZhiFang.Common.Log.Log.Debug("发血单号为:" + outForm.Id + ",获取血袋回收登记发血明细查询条件为:" + outDtlHql.ToString());
                IList<BloodBOutItem> outDtlList = IDBloodBOutItemDao.GetListByHQL(outDtlHql.ToString());
                ZhiFang.Common.Log.Log.Debug(string.Format("发血单号为:{0},获取回收登记完成度未完成的记录为:{1}", outForm.Id, outDtlList.Count));
                if (outDtlList == null || outDtlList.Count <= 0)
                {
                    outForm.RecoverCompletion = int.Parse(RecoverCompletion.回收完成.Key);
                }
                else
                {
                    outForm.RecoverCompletion = int.Parse(RecoverCompletion.部分回收.Key);
                }
                IBBloodBOutForm.Entity = outForm;
                bool result = IBBloodBOutForm.Edit();
                if (!result)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "更新发血单号为:" + outForm.Id + "的血袋回收登记完成度失败!";
                    ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo);
                    throw new Exception(brdv.ErrorInfo);
                    //break;
                    //return brdv;
                }
            }
            #endregion
            if (!brdv.success)
            {
                throw new Exception(brdv.ErrorInfo);
                //return brdv;
            }
            return brdv;
        }
        #endregion

        #region 输血申请综合查询
        public EntityList<BloodBagOperationVO> SearchBloodBagOperationVOOfByBReqFormID(string reqFormId, string sort, int page, int limit)
        {
            EntityList<BloodBagOperationVO> entityList = new EntityList<BloodBagOperationVO>();
            entityList.list = new List<BloodBagOperationVO>();

            StringBuilder hqlWhere = new StringBuilder();
            hqlWhere.Append(" bloodbagoperation.BloodBReqForm.Id='" + reqFormId + "' ");
            hqlWhere.Append(" and (bloodbagoperation.BagOperTypeID=" + BloodBagOperationType.交接登记.Key + " or bloodbagoperation.BagOperTypeID=" + BloodBagOperationType.回收登记.Key + ")");
            EntityList<BloodBagOperation> tempEntityList = this.SearchListByHQL(hqlWhere.ToString(), sort, page, limit);
            if (tempEntityList.count <= 0) return entityList;

            //按血袋分组
            //var groupByList = tempEntityList.list.GroupBy(p => p.Bloodstyle);
            //惟一标识血袋：BBagCode + PCode
            var groupByList = tempEntityList.list.GroupBy(p => new
            {
                p.Bloodstyle,
                p.BBagCode,
                p.PCode
            });
            foreach (var groupBy in groupByList)
            {
                BloodBagOperationVO vo = new BloodBagOperationVO();
                vo.Bloodstyle = groupBy.Key.Bloodstyle;
                vo.BBagCode = groupBy.Key.BBagCode;
                vo.PCode = groupBy.Key.PCode;
                vo.BloodBOutItem = groupBy.ElementAt(0).BloodBOutItem;
                if (vo.BloodBOutItem != null)
                {
                    vo.B3Code = vo.BloodBOutItem.B3Code;
                }
                ZhiFang.Common.Log.Log.Error("输血申请综合查询：血制品接收及回收分组信息：" + vo.Bloodstyle.CName + ",BBagCode:" + vo.BBagCode + ",PCode:" + vo.PCode);
                vo.BloodBReqForm = groupBy.ElementAt(0).BloodBReqForm;
                foreach (BloodBagOperation item in groupBy)
                {
                    if (item.BagOperTypeID == long.Parse(BloodBagOperationType.交接登记.Key))
                    {
                        vo.BloodBagHandover = item;
                    }
                    else if (item.BagOperTypeID == long.Parse(BloodBagOperationType.回收登记.Key))
                    {
                        vo.BloodBagRecover = item;
                    }
                }
                entityList.list.Add(vo);
            }
            entityList.count = entityList.list.Count;

            return entityList;
        }
        #endregion

    }
}