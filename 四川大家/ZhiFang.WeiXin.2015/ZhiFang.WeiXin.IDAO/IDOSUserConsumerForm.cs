using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.WeiXin.Entity;
using ZhiFang.IDAO.Base;

namespace  ZhiFang.WeiXin.IDAO
{
	public interface IDOSUserConsumerFormDao : IDBaseDao<OSUserConsumerForm, long>
	{
        bool OSDoctorBonusIByIdStr(string idStr, string doctorBonusID);
        bool UpdateStatusByOSDoctorBonusIDStr(string bonusIDStr, string status);
        bool UpdateOSDoctorBonusIDByOSDoctorBonusIDStr(string bonusIDStr);

    } 
}