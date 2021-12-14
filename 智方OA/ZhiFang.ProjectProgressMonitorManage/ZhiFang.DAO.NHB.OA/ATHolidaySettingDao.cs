using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.OA;
using ZhiFang.IDAO.OA;

namespace ZhiFang.DAO.NHB.OA
{
    public class ATHolidaySettingDao : BaseDaoNHB<ATHolidaySetting, long>, IDATHolidaySettingDao
    {
        /// <summary>
        /// 删除已经设置好的节假日信息
        /// </summary>
        /// <param name="strIds"></param>
        /// <param name="isUse"></param>
        /// <returns></returns>
        public bool DeleteByIdStr(string idStr)
        {
            bool result = true;
            if (!String.IsNullOrEmpty(idStr))
            {
                string hql = "FROM ATHolidaySetting atholidaysetting where atholidaysetting.Id in (" + idStr + ")";
                int counts = this.DeleteByHql(hql);
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