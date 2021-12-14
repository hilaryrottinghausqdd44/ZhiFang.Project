using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.OA;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.OA
{
	public interface IDATHolidaySettingDao : IDBaseDao<ATHolidaySetting, long>
	{
        /// <summary>
        /// 删除已经设置好的节假日信息
        /// </summary>
        /// <param name="strIds"></param>
        /// <param name="isUse"></param>
        /// <returns></returns>
       bool DeleteByIdStr(string idStr);

    } 
}