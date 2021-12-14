

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
	public  interface IBSickType : IBGenericManager<SickType>
	{
        bool RemoveSickTypeAndControl(long id); 
        BaseResultDataValue SickTypeCopyAll(List<string> LabCodeList, int OverRideType);
        BaseResultDataValue SickTypeCopy(List<string> LabCodeList, List<string> ItemNoList, int OverRideType);
    }
}