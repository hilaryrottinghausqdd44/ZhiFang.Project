using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IDAO;

namespace ZhiFang.Digitlab.DAO.NHB
{
    public class MEMicroDSTValueDao : BaseDaoNHB<MEMicroDSTValue, long>, IDMEMicroDSTValueDao
    {
        /// <summary>
        /// 允许传入同一检验单的多个药敏结果Id
        /// </summary>
        /// <param name="strIds"></param>
        /// <param name="deleteFlag"></param>
        /// <returns></returns>
        public bool UpdateDeleteFlag(string strIds, bool deleteFlag)
        {
            int result = 0;
            if (!String.IsNullOrEmpty(strIds))
            {
                result = this.UpdateByHql(" update MEMicroDSTValue memicrodstvalue set memicrodstvalue.DeleteFlag=" + deleteFlag + " where memicrodstvalue.Id in(" + strIds + ")");
            }
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