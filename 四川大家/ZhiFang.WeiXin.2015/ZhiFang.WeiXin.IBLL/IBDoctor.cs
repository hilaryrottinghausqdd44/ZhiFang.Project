

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IBLL.Base;
using ZhiFang.WeiXin.Entity;

namespace ZhiFang.WeiXin.IBLL
{
	/// <summary>
	///
	/// </summary>
	public  interface IBDoctor : IBGenericManager<Doctor>
	{
        void DoctorCopyAll(List<string> labCodeList,int type);
        void DoctorCopy( List<string> itemNoList, List<string> labCodeList, int type);
        bool RemoveAndControl(long id);
    }
}