
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.IDAO;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.BLL.Business
{
	/// <summary>
	///
	/// </summary>
    public class BBSpecialtyItem : BaseBLL<BSpecialtyItem>, ZhiFang.Digitlab.IBLL.Business.IBBSpecialtyItem
    {
        /// <summary>
        /// 通过专业ID获取专业下的项目列表
        /// </summary>
        /// <param name="specialtyID"></param>
        /// <returns></returns>
        public IList<BSpecialtyItem> SearchSpecialtyItemByItem(long specialtyID)
        {
            return ((IDAO.IDBSpecialtyItemDao)DBDao).GetListByHQL("BSpecialty.Id=" + specialtyID, 0, 0).list;
        }
    }
}