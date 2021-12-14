using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.WeiXin.Entity;
using ZhiFang.WeiXin.IDAO;
using ZhiFang.DAO.NHB.Base;

namespace ZhiFang.WeiXin.DAO.NHB
{
    public class OSUserConsumerFormDao : BaseDaoNHB<OSUserConsumerForm, long>, IDOSUserConsumerFormDao
    {
        /// <summary>
        /// 更新用户消费单与医生奖金记录的关系
        /// </summary>
        /// <param name="idStr"></param>
        /// <param name="doctorBonusID"></param>
        /// <returns></returns>
        public bool OSDoctorBonusIByIdStr(string idStr, string doctorBonusID)
        {
            bool result = true;
            if (String.IsNullOrEmpty(idStr)) return true;
            if (doctorBonusID == "") doctorBonusID = null;
            string sql = "update OSUserConsumerForm set OSDoctorBonusID=" + doctorBonusID + "  where Id in (" + idStr + ")";
            int counts = this.UpdateByHql(sql);
            result = counts > 0 ? true : false;
            return result;
        }
        /// <summary>
        /// 依医生奖金结算记录的id更新用户消费单的状态
        /// </summary>
        /// <param name="bonusIDStr"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool UpdateStatusByOSDoctorBonusIDStr(string bonusIDStr, string status)
        {
            bool result = true;
            if (String.IsNullOrEmpty(bonusIDStr)) return true;
            string sql = "update OSUserConsumerForm set Status=" + status + "  where OSDoctorBonusID in (" + bonusIDStr + ")";
            int counts = this.UpdateByHql(sql);
            result = counts > 0 ? true : false;
            return result;
        }
        /// <summary>
        /// 依医生奖金结算记录的id清空用户消费单的结算记录关联
        /// </summary>
        /// <param name="bonusIDStr"></param>
        /// <returns></returns>
        public bool UpdateOSDoctorBonusIDByOSDoctorBonusIDStr(string bonusIDStr)
        {
            bool result = true;
            if (String.IsNullOrEmpty(bonusIDStr)) return true;
            string sql = "update OSUserConsumerForm set  OSDoctorBonusID=NULL" + "  where OSDoctorBonusID in (" + bonusIDStr + ")";
            int counts = this.UpdateByHql(sql);
            result = counts > 0 ? true : false;
            return result;
        }
    }
}