using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IDAO;

namespace ZhiFang.Digitlab.DAO.NHB
{
    public class MEMicroAppraisalValueDao : BaseDaoNHB<MEMicroAppraisalValue, long>, IDMEMicroAppraisalValueDao
    {
        /// <summary>
        /// 依鉴定结果Id更新培养结果的删除标志
        ///并及更新同一鉴定结果下的鉴定结果的删除标志及同一鉴定结果下的药敏结果标志
        /// </summary>
        /// <param name="id">鉴定结果Id</param>
        /// <param name="deleteFlag"></param>
        /// <returns></returns>
        public bool UpdateDeleteFlag(long id, bool deleteFlag)
        {
            int result = 0;

            result = this.UpdateByHql(" update MEMicroDSTValue memicrodstvalue set memicrodstvalue.DeleteFlag=" + deleteFlag + " where memicrodstvalue.MEMicroAppraisalValue.Id = " + id);

            result = this.UpdateByHql(" update MEMicroAppraisalValue memicroappraisalvalue set memicroappraisalvalue.DeleteFlag=" + deleteFlag + " where memicroappraisalvalue.Id = " + id);

            if (result > 0)
            {
                return true;
            }
            else
            {
                return true;
            }
        }
    }
}