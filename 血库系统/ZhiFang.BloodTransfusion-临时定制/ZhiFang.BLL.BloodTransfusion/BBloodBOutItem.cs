
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.Entity.Base;
using ZhiFang.IDAO.NHB.BloodTransfusion;
using ZhiFang.Entity.RBAC;
using ZhiFang.IDAO.RBAC;

namespace ZhiFang.BLL.BloodTransfusion
{
    /// <summary>
    ///
    /// </summary>
    public class BBloodBOutItem : BaseBLL<BloodBOutItem, string>, ZhiFang.IBLL.BloodTransfusion.IBBloodBOutItem
    {
        IDBloodTransFormDao IDBloodTransFormDao { get; set; }
        IDBloodBagOperationDtlDao IDBloodBagOperationDtlDao { get; set; }
        IDPUserDao IDPUserDao { get; set; }

        #region 血制品交接登记
        public EntityList<BloodBOutItem> SearchBloodBOutItemOfHandoverByBReqVOHQL(BloodBReqFormVO bReqVO, string where, string sort, int page, int limit)
        {
            EntityList<BloodBOutItem> entityList = new EntityList<BloodBOutItem>();
            StringBuilder strbHql = new StringBuilder();
            strbHql.Append(" 1=1 ");
            if (!string.IsNullOrEmpty(where))
                strbHql.Append(" and " + where + " ");
            if (!string.IsNullOrEmpty(bReqVO.AdmID))
                strbHql.Append(" and bloodboutform.BloodBReqForm.AdmID='" + bReqVO.AdmID + "'");
            if (!string.IsNullOrEmpty(bReqVO.PatNo))
                strbHql.Append(" and bloodboutform.BloodBReqForm.PatNo='" + bReqVO.PatNo + "'");
            if (!string.IsNullOrEmpty(bReqVO.CName))
                strbHql.Append(" and bloodboutform.BloodBReqForm.CName='" + bReqVO.CName + "'");
            if (!string.IsNullOrEmpty(bReqVO.Sex))
                strbHql.Append(" and bloodboutform.BloodBReqForm.Sex='" + bReqVO.Sex + "'");
            if (bReqVO.DeptNo.HasValue)
                strbHql.Append(" and bloodboutform.BloodBReqForm.DeptNo=" + bReqVO.DeptNo.Value + "");
            if (!string.IsNullOrEmpty(bReqVO.Bed))
                strbHql.Append(" and bloodboutform.BloodBReqForm.Bed='" + bReqVO.Bed + "'");
            if (!string.IsNullOrEmpty(bReqVO.AgeALL))
                strbHql.Append(" and bloodboutform.BloodBReqForm.AgeALL='" + bReqVO.AgeALL + "'");

            //交接登记完成度
            strbHql.Append(" and bloodboutitem.HandoverCompletion!=" + HandoverCompletion.交接完成.Key);

            ZhiFang.Common.Log.Log.Debug("(血制品交接登记)根据申请单信息获取未交接登记完成的发血明细信息.HQL:" + strbHql.ToString());
            entityList = ((IDBloodBOutItemDao)base.DBDao).SearchBloodBOutItemOfHandoverByBReqVOHQL(strbHql.ToString(), sort, page, limit);

            return entityList;
        }
        public EntityList<BloodBOutItem> SearchBloodBOutItemOfHandoverByBBagCodeHQL(string where, string scanCodeField, string bagCode, string sort, int page, int limit, ref BaseResultDataValue tempBaseResultDataValue)
        {
            EntityList<BloodBOutItem> entityList = new EntityList<BloodBOutItem>();
            if (string.IsNullOrEmpty(bagCode))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "传入的血袋号参数(bagCode)不能为空!";
                return entityList;
            }
            if (string.IsNullOrEmpty(scanCodeField)) scanCodeField = "BBagCode";

            StringBuilder strbHql = new StringBuilder();
            strbHql.Append(string.Format("bloodboutitem.{0}='{1}'", scanCodeField, bagCode));
            strbHql.Append(" and bloodboutitem.OutFlag=" + BloodBOutItemOutFlag.出库.Key + "");
            if (!string.IsNullOrEmpty(where))
                strbHql.Append(" and " + where);
            string strHqlWhere = strbHql.ToString();
            //交接登记完成度
            //strbHql.Append(" and bloodboutitem.HandoverCompletion!="+ HandoverCompletion.交接完成.Key);
            ZhiFang.Common.Log.Log.Debug("((血制品交接登记)根据血袋号获取未交接登记完成的发血明细信息.HQL:" + strbHql.ToString());
            EntityList<BloodBOutItem> entityList2 = ((IDBloodBOutItemDao)base.DBDao).SearchBloodBOutItemOfHandoverByBBagCodeHQL(strHqlWhere, sort, page, limit);
            if (entityList2.count > 1)
            {
                ZhiFang.Common.Log.Log.Info("血袋号为:" + bagCode + ",获取血袋发血记录数为:" + entityList2.count + "!");
            }
            else if (entityList2.count <= 0)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "血袋号为:" + bagCode + ",获取血袋发血记录信息为空!";
                ZhiFang.Common.Log.Log.Error(tempBaseResultDataValue.ErrorInfo);
                return entityList;
            }
            
            entityList = entityList2;
            return entityList;
        }
        public EntityList<BloodBOutItem> SearchBloodBOutItemAndBagOperDtlOfHandoverByHQL(string where, string sort, int page, int limit, ref BaseResultDataValue tempBaseResultDataValue)
        {
            EntityList<BloodBOutItem> entityList = new EntityList<BloodBOutItem>();
            StringBuilder strbHql = new StringBuilder();
            strbHql.Append(" 1=1 ");
            if (!string.IsNullOrEmpty(where))
                strbHql.Append(" and " + where);
            string strHqlWhere = strbHql.ToString();
            entityList = ((IDBloodBOutItemDao)base.DBDao).GetListByHQL(strHqlWhere, sort, page, limit);
            if (entityList.count <= 0)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "获取血袋发血记录信息为空!";
                ZhiFang.Common.Log.Log.Error(tempBaseResultDataValue.ErrorInfo);
                return entityList;
            }
            for (int i = 0; i < entityList.list.Count; i++)
            {
                //获取血袋接收登记信息
                string id = entityList.list[i].Id;
                IList<BloodBagOperationDtl> dtlList = IDBloodBagOperationDtlDao.GetListByHQL("bloodbagoperationdtl.BloodBagOperation.BloodBOutItem.Id='" + id + "'");
                for (int j = 0; j < dtlList.Count; j++)
                {
                    string code = dtlList[j].BDict.BDictType.DictTypeCode;
                    if (code.ToLower() == "bloodappearance")
                    {
                        entityList.list[i].BloodAppearance = dtlList[j];//血袋外观信息
                    }
                    else if (code.ToLower() == "bloodintegrity")
                    {
                        entityList.list[i].BloodIntegrity = dtlList[j];//血袋完整性
                    }
                }
            }
            return entityList;
        }
        #endregion

        #region 血袋回收登记
        public EntityList<BloodBOutItem> SearchBloodBOutItemOfRecycleByHQL(string strHqlWhere, string sort, int page, int limit)
        {
            EntityList<BloodBOutItem> entityList = new EntityList<BloodBOutItem>();
            StringBuilder strbHql = new StringBuilder();
            strbHql.Append(" 1=1 ");
            //回收登记完成度
            strbHql.Append(" and bloodboutitem.RecoverCompletion!=" + RecoverCompletion.回收完成.Key);
            if (!string.IsNullOrEmpty(strHqlWhere))
                strbHql.Append(" and " + strHqlWhere);
            entityList = ((IDBloodBOutItemDao)base.DBDao).SearchBloodBOutItemOfRecycleByHQL(strbHql.ToString(), sort, page, limit);
            return entityList;
        }
        public EntityList<BloodBOutItem> SearchBloodBOutItemOfRecycleByBBagCodeHQL(string where, string scanCodeField,string bagCode, string sort, int page, int limit, ref BaseResultDataValue tempBaseResultDataValue)
        {
            EntityList<BloodBOutItem> entityList = new EntityList<BloodBOutItem>();
            if (string.IsNullOrEmpty(bagCode))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "传入的血袋号参数(bagCode)不能为空!";
                return entityList;
            }
            if (string.IsNullOrEmpty(scanCodeField)) scanCodeField = "BBagCode";
            StringBuilder strbHql = new StringBuilder();

            //strbHql.Append("bloodboutitem.BBagCode='" + bagCode + "'");
            strbHql.Append(string.Format("bloodboutitem.{0}='{1}'", scanCodeField, bagCode));
            strbHql.Append(" and bloodboutitem.OutFlag=" + BloodBOutItemOutFlag.出库.Key + "");
            if (!string.IsNullOrEmpty(where))
                strbHql.Append(" and " + where);
            string strHqlWhere = strbHql.ToString();
            //回收登记完成度
            //strbHql.Append("bloodboutitem.RecoverCompletion!="+ RecoverCompletion.全部回收.Key);
            //ZhiFang.Common.Log.Log.Debug("(血袋回收登记)按血袋号获取待进行血袋回收登记的血袋信息.HQL:" + strbHql.ToString());
            EntityList<BloodBOutItem> entityList2 = ((IDBloodBOutItemDao)base.DBDao).SearchBloodBOutItemOfRecycleByBBagCodeHQL(strHqlWhere, sort, page, limit);
            if (entityList2.count > 1)
            {
                ZhiFang.Common.Log.Log.Info("血袋号为:" + bagCode + ",获取血袋发血记录数为:" + entityList2.count + "!");
            }
            else if (entityList2.count <= 0)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "血袋号为:" + bagCode + ",获取血袋发血记录信息为空!";
                ZhiFang.Common.Log.Log.Error(tempBaseResultDataValue.ErrorInfo);
                return entityList;
            }

            entityList = entityList2;
            return entityList;
        }
        #endregion

        #region 输血过程记录登记
        public EntityList<BloodBOutItem> SearchBloodBOutItemOfBloodTransByHQL(string strHqlWhere, string sort, int page, int limit)
        {
            EntityList<BloodBOutItem> entityList = new EntityList<BloodBOutItem>();
            //StringBuilder strbHql = new StringBuilder();
            //strbHql.Append(" 1=1 ");
            //输血过程登记完成度
            //strbHql.Append("bloodboutitem.CourseCompletion!=" + CourseCompletion.登记完成.Key);

            entityList = ((IDBloodBOutItemDao)base.DBDao).SearchBloodBOutItemOfRecycleByHQL(strHqlWhere, sort, page, limit);

            //找出发血血袋对应的申请主单信息及输血过程记录主单信息
            for (int i = 0; i < entityList.list.Count; i++)
            {
                StringBuilder strbHql = new StringBuilder();
                strbHql.Append(" bloodtransform.BloodBOutItem.Id='" + entityList.list[i].Id + "'");
                strbHql.Append(" and bloodtransform.Bloodstyle.Id='" + entityList.list[i].Bloodstyle.Id + "'");
                //strbHql.Append(" and bloodtransform.BBagCode='" + entityList.list[i].BBagCode + "'");
                strbHql.Append(" and bloodtransform.Visible=1");
                //输血过程记录主单信息
                IList<BloodTransForm> tempList = IDBloodTransFormDao.GetListByHQL(strbHql.ToString());
                if (tempList != null && tempList.Count > 0)
                {
                    entityList.list[i].BloodTransForm = tempList[0];
                }
            }

            return entityList;
        }
        #endregion

        #region 输血申请综合查询
        public EntityList<BloodBOutItem> SearchBloodBOutItemByBReqFormIDAndHQL(string strHqlWhere, string reqFormId, string sort, int page, int limit)
        {
            EntityList<BloodBOutItem> entityList = new EntityList<BloodBOutItem>();

            //entityList = ((IDBloodBOutItemDao)base.DBDao).GetListByHQL(strHqlWhere, sort, page, limit);
            entityList = ((IDBloodBOutItemDao)base.DBDao).SearchBloodBOutItemOfRecycleByHQL(strHqlWhere, sort, page, limit);
            //人员姓名处理
            //临时人员集合信息
            IList<PUser> puserList = new List<PUser>();
            for (int i = 0; i < entityList.list.Count; i++)
            {
                BloodBOutItem outItem = entityList.list[i];
                PUser puser = null;
                if (!string.IsNullOrEmpty(outItem.BloodBOutForm.OperatorID))
                {
                    var tempPUserList = puserList.Where(p => p.Id == long.Parse(outItem.BloodBOutForm.OperatorID));
                    if (tempPUserList != null && tempPUserList.Count() > 0) puser = tempPUserList.ElementAt(0);
                    if (puser == null)
                    {
                        puser = IDPUserDao.Get(int.Parse(outItem.BloodBOutForm.OperatorID));
                        if (puser != null)
                        {
                            puserList.Add(puser);
                        }
                    }
                }
                if (puser != null)
                {
                    entityList.list[i].BloodBOutForm.Operator = puser.CName;
                }
            }
            return entityList;
        }
        #endregion
    }
}