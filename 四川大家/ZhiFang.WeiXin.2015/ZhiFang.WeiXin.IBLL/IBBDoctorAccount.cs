

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.WeiXin.Entity;
using ZhiFang.WeiXin.Entity.ViewObject.Response;

namespace ZhiFang.WeiXin.IBLL
{
	/// <summary>
	///
	/// </summary>
	public  interface IBBDoctorAccount : ZhiFang.IBLL.Base.IBGenericManager<BDoctorAccount>
	{
        bool CheckBDoctorAccountByWeiXinUserID(long WeiXinUserID, out long DoctorAccountID, out string HospitalCode, out long HospitalID, out string HospitalName, out string Name, out string BonusPercent, out long AreaID,out long DoctorAccountType);

        void GetDoctorBankInfo(OSDoctorChargeInfoVO doctorChargeVO, long DoctorAccountID);
    }
}