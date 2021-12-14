
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
	public  class BBSampleStatusType : BaseBLL<BSampleStatusType>, ZhiFang.Digitlab.IBLL.Business.IBBSampleStatusType
	{
        public BSampleStatusType GetSampleStatusTypeByCode(string statusTypeCode)
        {
            EntityList<BSampleStatusType> tempEntityList = this.SearchListByHQL("bsamplestatustype.ShortCode=" + "'" + statusTypeCode + "'", 0, 0);
            if (tempEntityList != null && tempEntityList.count > 0)
                return tempEntityList.list[0];
            else
                return null;
        }
        public BSampleStatusType GetSampleStatusTypeByLevel(string statusTypeLevel)
        {
            EntityList<BSampleStatusType> tempEntityList = this.SearchListByHQL("bsamplestatustype.Level=" + statusTypeLevel, 0, 0);
            if (tempEntityList != null && tempEntityList.count > 0)
                return tempEntityList.list[0];
            else
                return null;
        }
	}
}