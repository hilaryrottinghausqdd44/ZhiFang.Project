
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.WeiXin.IDAO;
using ZhiFang.WeiXin.Entity;
using ZhiFang.WeiXin.Entity.ViewObject.Response;
using ZhiFang.BLL.Base;

namespace ZhiFang.WeiXin.BLL
{
	/// <summary>
	///
	/// </summary>
	public  class BBDoctorAccount : ZhiFang.BLL.Base.BaseBLL<BDoctorAccount>, ZhiFang.WeiXin.IBLL.IBBDoctorAccount
	{
        ZhiFang.WeiXin.IBLL.IBBDict IBBDict;
        public bool CheckBDoctorAccountByWeiXinUserID(long WeiXinUserID, out long DoctorAccountID, out string HospitalCode, out long HospitalID, out string HospitalName, out string Name, out string BonusPercent, out long AreaID, out long DoctorAccountType)
        {
            DoctorAccountID = 0;
            HospitalCode = null;
            HospitalID = 0;
            HospitalName = null;
            Name = null;
            BonusPercent = null;
            AreaID = 0;
            DoctorAccountType = 1;
            var objectlist = DBDao.GetListByHQL(" WeiXinUserID=" + WeiXinUserID );
            if (objectlist.Count > 0)
            {
                DoctorAccountID = objectlist[0].Id;
                HospitalCode = objectlist[0].HospitalCode;
                HospitalID = objectlist[0].HospitalID;
                HospitalName = objectlist[0].HospitalName;
                Name = objectlist[0].Name;                
                BonusPercent = (objectlist[0].BonusPercent.HasValue)? objectlist[0].BonusPercent.ToString() : "0";
                AreaID = objectlist[0].AreaID;
                HospitalCode = objectlist[0].HospitalCode;
                DoctorAccountType = objectlist[0].DoctorAccountType.HasValue? objectlist[0].DoctorAccountType.Value:1;
                return true;
            }
            return false;
        }

        public void GetDoctorBankInfo(OSDoctorChargeInfoVO doctorChargeVO, long DoctorAccountID)     
        {
            BDoctorAccount doctorAccount = this.Get(DoctorAccountID);
            if (doctorAccount != null)
            {
                doctorChargeVO.Id = doctorAccount.Id;
                doctorChargeVO.DN = doctorAccount.Name;
                doctorChargeVO.BA = doctorAccount.BankAccount;
                doctorChargeVO.BID = doctorAccount.BankID;
                if (doctorAccount.BankID != null && doctorAccount.BankID > 0)
                {
                    BDict dict = IBBDict.Get((long)doctorAccount.BankID);
                    doctorChargeVO.BN = (dict == null ? "" : dict.CName);
                }
            }
        }
    }
}