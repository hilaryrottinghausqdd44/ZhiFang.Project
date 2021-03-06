
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
	public  class BBSampleOperateType : BaseBLL<BSampleOperateType>, ZhiFang.Digitlab.IBLL.Business.IBBSampleOperateType
	{
        public BSampleOperateType GetSampleOperateTypeByCode(string operateTypeCode)
        {
            EntityList<BSampleOperateType> tempEntityList = this.SearchListByHQL("bsampleoperatetype.Shortcode=" + "'" + operateTypeCode + "'", 0, 0);
            if (tempEntityList != null && tempEntityList.count > 0)
                return tempEntityList.list[0];
            else
                return null;
        }   
	}
}