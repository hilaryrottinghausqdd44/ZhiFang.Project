
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
	public  class BBOperateObjectType : BaseBLL<BOperateObjectType>, ZhiFang.Digitlab.IBLL.Business.IBBOperateObjectType
	{
        public BOperateObjectType GetBOperateObjectTypeByCode(string operateObjectTypeCode)
        {
            EntityList<BOperateObjectType> tempEntityList = this.SearchListByHQL("boperateobjecttype.Shortcode=" + "'" + operateObjectTypeCode + "'", 0, 0);
            if (tempEntityList != null && tempEntityList.count > 0)
                return tempEntityList.list[0];
            else
                return null;
        }
	}
}