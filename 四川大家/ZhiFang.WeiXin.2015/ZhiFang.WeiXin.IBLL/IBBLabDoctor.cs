

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
	public  interface IBBLabDoctor : IBGenericManager<BLabDoctor>
	{
        EntityList<BLabDoctorVO> BLabDoctorAndControl(string labCode,int controlType,int page,int limit,string where);
        bool RemoveAndControl(long id);
        void LabDoctorCopyAll(string originalLabCode,List<string> LabCodeList,int OverRideType);
        void LabDoctorCopy(string originalLabCode, List<string> ItemNoList, List<string> LabCodeList,int OverRideType);
    }
}