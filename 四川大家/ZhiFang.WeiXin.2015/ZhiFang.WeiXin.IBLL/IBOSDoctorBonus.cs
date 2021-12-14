using System;
using System.Collections.Generic;
using System.IO;
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
    public interface IBOSDoctorBonus : ZhiFang.IBLL.Base.IBGenericManager<OSDoctorBonus>
    {
        FileStream GetExportExcelOSDoctorBonusDetail(string where, ref string fileName);

        void GetDoctorBonusInfo(OSDoctorChargeInfoVO doctorChargeVO, long DoctorAccountID);
    }
}