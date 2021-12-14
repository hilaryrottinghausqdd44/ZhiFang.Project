

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.IBLL.Base;
using ZhiFang.WeiXin.Entity;
using ZhiFang.WeiXin.Entity.ViewObject.Response;

namespace ZhiFang.WeiXin.IBLL
{
	/// <summary>
	///
	/// </summary>
	public  interface IBBLabSickType : IBGenericManager<BLabSickType>
	{
        bool RemoveLabSickTypeAndControl(long id);
        BaseResultDataValue BLabSickTypeCopyAll(string sourceLabCode, List<string> labCodeList, int OverRideType);
        BaseResultDataValue BLabSickTypeCopy(string sourceLabCode, List<string> labCodeList, List<string> ItemNoList, int OverRideType);
        EntityList<BLabSickTypeVO> BLabSickTypeAndControl(string labCode, int type, int page, int limit, string where);
    }
}