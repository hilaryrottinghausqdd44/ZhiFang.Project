using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.WeiXin.Entity;
using ZhiFang.WeiXin.IDAO;

namespace ZhiFang.WeiXin.DAO.NHB
{
    public class OSDoctorBonusAttachmentDao : BaseDaoNHB<OSDoctorBonusAttachment, long>, IDOSDoctorBonusAttachmentDao
    {
        public int DeleteByBobjectID(long bobjectID)
        {
            int result = 0;
            result = this.DeleteByHql("FROM OSDoctorBonusAttachment  where BobjectID=" + bobjectID);
            return result;
        }
    }
}