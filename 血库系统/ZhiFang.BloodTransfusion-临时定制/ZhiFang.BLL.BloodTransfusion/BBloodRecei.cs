
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
    public class BBloodRecei : BaseBLL<BloodRecei, string>, ZhiFang.IBLL.BloodTransfusion.IBBloodRecei
    {
        public EntityList<BloodRecei> SearchBloodReceiListByBReqVO(string reqFormId, BloodBReqFormVO bReqVO, string sort, int page, int limit)
        {
            EntityList<BloodRecei> entityList = new EntityList<BloodRecei>();
            //先按申请单号获取
            if (!string.IsNullOrEmpty(reqFormId))
            {
                string hql = string.Format("bloodrecei.BloodBReqForm.Id='{0}'", reqFormId);
                entityList = ((IDBloodReceiDao)base.DBDao).GetListByHQL(hql, sort, page, limit);
            }
            if (entityList.count <= 0)
            {
                StringBuilder strbHql = new StringBuilder();
                strbHql.Append(" 1=1 ");
                //if (!string.IsNullOrEmpty(bReqVO.AdmID))
                //    strbHql.Append(" and bloodrefuse.AdmID='" + bReqVO.AdmID + "'");
                if (!string.IsNullOrEmpty(bReqVO.PatNo))
                    strbHql.Append(" and bloodrecei.PatNo='" + bReqVO.PatNo + "'");
                if (!string.IsNullOrEmpty(bReqVO.CName))
                    strbHql.Append(" and bloodrecei.CName='" + bReqVO.CName + "'");
                if (!string.IsNullOrEmpty(bReqVO.Sex))
                    strbHql.Append(" and bloodrecei.Sex='" + bReqVO.Sex + "'");
                if (bReqVO.DeptNo.HasValue)
                    strbHql.Append(" and bloodrecei.DeptNo=" + bReqVO.DeptNo + "");
                if (!string.IsNullOrEmpty(bReqVO.Bed))
                    strbHql.Append(" and bloodrecei.Bed='" + bReqVO.Bed + "'");
                ZhiFang.Common.Log.Log.Debug("(输血申请综合查询)按申请信息获取到相应的样本信息.HQL:" + strbHql.ToString());
                entityList = ((IDBloodReceiDao)base.DBDao).GetListByHQL(strbHql.ToString(), sort, page, limit);
                if (entityList.count <= 0) return entityList;
            }

            //样本状态处理
            /***
             * 状态分为接收及拒收;
             * 如果拒收原因编号(refuseNo)/拒收处理编号(refuseDisposeNo)信息为空,表示该样本单为接收;
             * 如果拒收原因编号(refuseNo)/拒收处理编号(refuseDisposeNo)信息不为空,表示该样本单为拒收;
             */
            for (int i = 0; i < entityList.list.Count; i++)
            {
                if (entityList.list[i].Bloodrefuse == null)
                {
                    entityList.list[i].StatusCName = "接收";
                }
                else
                {
                    entityList.list[i].StatusCName = "拒收";
                }
            }
            return entityList;
        }
    }
}
