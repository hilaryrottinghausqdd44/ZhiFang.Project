using System;
using System.Collections.Generic;
using System.Linq;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.NHB.ReagentSys.Client;

namespace ZhiFang.DAO.NHB.ReagentSys.Client
{
    public class ReaBmsReqDocDao : BaseDaoNHB<ReaBmsReqDoc, long>, IDReaBmsReqDocDao
    {
        /// <summary>
        /// 批量更新申请单的状态
        /// </summary>
        /// <param name="idStr"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool UpdateStatusByIdStr(string idStr, int status)
        {
            bool result = true;
            if (!String.IsNullOrEmpty(idStr))
            {
                string hql = string.Format("update ReaBmsReqDoc reabmsreqdoc set reabmsreqdoc.Status={0} where reabmsreqdoc.Id in ({1})", status, idStr);
                int counts = this.UpdateByHql(hql);
                if (counts > 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            else
            {
                result = false;
            }
            return result;
        }
    }
}