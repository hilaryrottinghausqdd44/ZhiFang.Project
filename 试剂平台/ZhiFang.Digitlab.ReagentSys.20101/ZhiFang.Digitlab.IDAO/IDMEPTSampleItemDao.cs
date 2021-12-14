using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.IDAO
{
	public interface IDMEPTSampleItemDao : IDBaseDao<MEPTSampleItem, long>
	{
        /// <summary>
        /// 根据检验小组ID查找样本单中属于该小组的项目
        /// </summary>
        /// <param name="meptSampleFormID">样本单ID</param>
        /// <param name="gmGroupID">检验小组ID</param>
        /// <returns>IList&lt;MEPTSampleItem&gt;</returns>
        IList<MEPTSampleItem> SearchMEPTSampleItemByGMGroupID(long meptSampleFormID, long gmGroupID);
	} 
}