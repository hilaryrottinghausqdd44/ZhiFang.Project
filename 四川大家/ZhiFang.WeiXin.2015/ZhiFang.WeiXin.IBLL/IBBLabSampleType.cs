

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
	public  interface IBBLabSampleType : IBGenericManager<BLabSampleType>
	{
        bool RemoveAndControl(long BLabSampleTypeID);
        EntityList<BLabSampleTypeVO> BLabSampleTypeAndControl(string labCode, int controlType,int page,int limit,string where);
        BaseResultDataValue LabSampleTypeCopyAll(string originalLabCode,List<string> LabCodeList,int OverRideType);
        BaseResultDataValue LabSampleTypeCopy(string originalLabCode,List<string> ItemNoList, List<string> LabCodeList,int OverRideType);
    }
}