using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.Base;
using ZhiFang.WeiXin.Entity;

namespace  ZhiFang.WeiXin.IDAO
{
	public interface IDOSDoctorBonusOperationDao : IDBaseDao<OSDoctorBonusOperation, long>
	{
        int DeleteByBobjectID(long bobjectID);

    } 
}