
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
    public class BBloodBOutForm : BaseBLL<BloodBOutForm, string>, ZhiFang.IBLL.BloodTransfusion.IBBloodBOutForm
    {
        IDBloodBOutItemDao IDBloodBOutItemDao { get; set; }

        public EntityList<BloodBReqFormVO> SearchBloodBReqFormVOOfHandoverByHQL(string where, string sort, int page, int limit)
        {
            EntityList<BloodBReqFormVO> entityList = new EntityList<BloodBReqFormVO>();
            StringBuilder strbHql = new StringBuilder();
            strbHql.Append(" 1=1 ");
            if (!string.IsNullOrEmpty(where))
                strbHql.Append(" and " + where);
            strbHql.Append(" and bloodboutform.HandoverCompletion!=" + HandoverCompletion.交接完成.Key);
            entityList = ((IDBloodBOutFormDao)base.DBDao).SearchBloodBReqFormVOOfHandoverByHQL(strbHql.ToString(), sort, page, limit);
            return entityList;
        }
        public EntityList<BloodBOutForm> SearchBloodBOutFormOfHandoverByBReqVOHQL(BloodBReqFormVO bReqVO, string where, string sort, int page, int limit)
        {
            EntityList<BloodBOutForm> entityList = new EntityList<BloodBOutForm>();
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
            strbHql.Append(" and bloodboutform.HandoverCompletion!=" + HandoverCompletion.交接完成.Key);

            ZhiFang.Common.Log.Log.Debug("(血制品交接登记)根据申请信息获取未交接登记完成的出库主单信息.HQL:" + strbHql.ToString());

            entityList = ((IDBloodBOutFormDao)base.DBDao).SearchBloodBOutFormOfLeftJoinByHQL(strbHql.ToString(), sort, page, limit);

            for (int i = 0; i < entityList.list.Count; i++)
            {
                IList<BloodBOutItem> tempList = IDBloodBOutItemDao.GetListByHQL("bloodboutitem.BloodBOutForm.Id='" + entityList.list[i].Id + "'");
                if (tempList != null) entityList.list[i].DtlTotal = tempList.Count();
            }
            return entityList;
        }
        public EntityList<BloodBOutForm> SearchBloodBOutFormOfBloodTransByHQL(string strHqlWhere, string sort, int page, int limit)
        {
            EntityList<BloodBOutForm> entityList = new EntityList<BloodBOutForm>();
            //StringBuilder strbHql = new StringBuilder();
            //strbHql.Append(" 1=1 ");
            //输血过程登记完成度
            //strbHql.Append("bloodboutform.CourseCompletion!=" + CourseCompletion.登记完成.Key);

            entityList = ((IDBloodBOutFormDao)base.DBDao).SearchBloodBOutFormOfLeftJoinByHQL(strHqlWhere, sort, page, limit);

            return entityList;
        }
        public BaseResultBool EditBOutCourseCompletionByOutId(string id, string updateValue, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (string.IsNullOrEmpty(id))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "传入的发血单号（id）为空！";
                return tempBaseResultBool;
            }
            if (string.IsNullOrEmpty(updateValue))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "传入的输血登记完成度（updateValue）为空！";
                return tempBaseResultBool;
            }
            BloodBOutForm outForm = this.Get(id);
            if (outForm == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "获取发血单号（id）的信息为空！";
                return tempBaseResultBool;
            }

            if (updateValue == CourseCompletion.登记完成.Key)
            {
                outForm.CourseCompletion = int.Parse(CourseCompletion.登记完成.Key);
            }
            else
            {
                outForm.CourseCompletion = int.Parse(CourseCompletion.已登记.Key);
            }
            StringBuilder outDtlHql = new StringBuilder();
            outDtlHql.Append(" bloodboutitem.BloodBOutForm.Id='" + outForm.Id + "'");
            IList<BloodBOutItem> outDtlList = IDBloodBOutItemDao.GetListByHQL(outDtlHql.ToString());
            foreach (var item in outDtlList)
            {
                item.CourseCompletion = outForm.CourseCompletion;
                bool result2 = IDBloodBOutItemDao.Update(item);
                if (!result2)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "更新发血明细单号为:" + item.Id + "的输血登记完成度失败!";
                    return tempBaseResultBool;
                }
            }

            this.Entity = outForm;
            bool result = this.Edit();
            if (!result)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "更新发血单号为:" + outForm.Id + "的输血登记完成度失败!";
            }
            return tempBaseResultBool;
        }

        public BaseResultBool EditBOutCourseCompletionByEndBloodOper(BloodBOutForm updateEntity, string updateValue, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (updateEntity==null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "传入的发血单号（id）为空！";
                return tempBaseResultBool;
            }
            if (string.IsNullOrEmpty(updateValue))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "传入的输血登记完成度（updateValue）为空！";
                return tempBaseResultBool;
            }
            BloodBOutForm serverEntity = this.Get(updateEntity.Id);
            if (serverEntity == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "获取发血单号（id）的信息为空！";
                return tempBaseResultBool;
            }

            if (updateValue == CourseCompletion.登记完成.Key)
            {
                serverEntity.CourseCompletion = int.Parse(CourseCompletion.登记完成.Key);
            }
            else
            {
                serverEntity.CourseCompletion = int.Parse(CourseCompletion.已登记.Key);
            }
            StringBuilder outDtlHql = new StringBuilder();
            outDtlHql.Append(" bloodboutitem.BloodBOutForm.Id='" + serverEntity.Id + "'");
            IList<BloodBOutItem> outDtlList = IDBloodBOutItemDao.GetListByHQL(outDtlHql.ToString());
            foreach (var item in outDtlList)
            {
                item.CourseCompletion = serverEntity.CourseCompletion;
                bool result2 = IDBloodBOutItemDao.Update(item);
                if (!result2)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "更新发血明细单号为:" + item.Id + "的输血登记完成度失败!";
                    return tempBaseResultBool;
                }
            }

            serverEntity.EndBloodOperId = empID;
            serverEntity.EndBloodOperName = empName;
            serverEntity.EndBloodOperTime =DateTime.Now;
            serverEntity.EndBloodReason = updateEntity.EndBloodReason;
            serverEntity.BDEndBReason= updateEntity.BDEndBReason;
            this.Entity = serverEntity;
            bool result = this.Edit();
            if (!result)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "更新发血单号为:" + serverEntity.Id + "的输血登记完成度失败!";
            }
            return tempBaseResultBool;
        }
    }
}