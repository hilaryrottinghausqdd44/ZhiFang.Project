

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
	public  interface IBBSampleType : IBGenericManager<BSampleType>
	{
        BaseResultTree GetBSampleTypeTree(); 
        /// <summary>
        /// 判断父样本类型是否包含子样本类型
        /// </summary>
        /// <param name="pSampleTypeID">父样本类型ID</param>
        /// <param name="cSampleTypeID">子样本类型ID</param>
        /// <returns>bool&lt;bool&gt;</returns>
        bool _judgeIsContainSampletype(long pSampleTypeID, long cSampleTypeID);
	}
}