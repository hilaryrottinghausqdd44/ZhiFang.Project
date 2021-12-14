

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.IBLL.Base;
using ZhiFang.WeiXin.Entity;

namespace ZhiFang.WeiXin.IBLL
{
	/// <summary>
	///
	/// </summary>
	public  interface IBSampleType : IBGenericManager<SampleType>
	{
        BaseResultDataValue SampleTypeCopyAll(List<string> LabCodeList, int OverRideType);
        BaseResultDataValue SampleTypeCopy(List<string> SampleTypeNolist, List<string> LabCodeList, int OverRideType);
        bool RemoveAndControl(long id);
    }
}