using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.WeiXin.Entity;
using ZhiFang.IBLL.Base;
using ZhiFang.WeiXin.Entity.Statistics;
using System.IO;
using ZhiFang.Entity.Base;
using ZhiFang.WeiXin.Entity.ViewObject.Response;
using ZhiFang.WeiXin.Entity.ViewObject.Request;

namespace ZhiFang.WeiXin.IBLL
{
    /// <summary>
    ///
    /// </summary>
    public interface IBOSUserConsumerForm : IBGenericManager<OSUserConsumerForm>
    {
        bool OSDoctorBonusIByIdStr(string idStr, string doctorBonusID);

        void GetDoctorUserConsumerInfo(OSDoctorChargeInfoVO doctorChargeVO, long DoctorAccountID);
        void GetDoctorUserConsumerInfoByDay(OSDoctorChargeInfoVO chargeVO, long v, int page, int limit);
        EntityList<OSUserConsumerFormVO> GetGetOSUserConsumerForm(string startDay, string endDay, string id, int page, int limit);
        BaseResultDataValue SaveOSUserConsumerForm(long NRequestFormNo, NrequestCombiItemBarCodeEntity jsonentity);
    }
}