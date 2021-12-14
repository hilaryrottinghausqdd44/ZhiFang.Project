using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.OA;
using ZhiFang.Entity.OA;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.IBLL.OA;
using ZhiFang.IBLL.ProjectProgressMonitorManage;

namespace ZhiFang.BLL.OA
{
    /// <summary>
    ///
    /// </summary>
    public class BAHServerProgramLicence : BaseBLL<AHServerProgramLicence>, ZhiFang.IBLL.OA.IBAHServerProgramLicence
    {
        public int DeleteByStrId(string strId)
        {
            int result = 0;
            result = ((IDAHServerProgramLicenceDao)base.DBDao).DeleteByStrId(strId);
            return result;
        }

        /// <summary>
        /// 获取上一次的授权程序申请信息
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="Order"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public EntityList<AHServerProgramLicence> SearchPreListByHQL(string strHqlWhere, string order, int page, int count)
        {
            EntityList<AHServerProgramLicence> list = new EntityList<AHServerProgramLicence>();
            list = ((IDAHServerProgramLicenceDao)base.DBDao).SearchPreListByHQL(strHqlWhere, order, page, count);
            return list;
        }
    }
}