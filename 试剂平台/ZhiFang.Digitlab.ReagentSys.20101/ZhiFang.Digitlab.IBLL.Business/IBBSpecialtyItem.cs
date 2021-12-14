

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.IBLL.Business
{
	/// <summary>
	///
	/// </summary>
	public  interface IBBSpecialtyItem : IBGenericManager<BSpecialtyItem>
	{
         /// <summary>
        /// 通过专业ID获取专业下的项目列表
        /// </summary>
        /// <param name="specialtyID"></param>
        /// <returns></returns>
       IList<BSpecialtyItem> SearchSpecialtyItemByItem(long specialtyID);
	}
}