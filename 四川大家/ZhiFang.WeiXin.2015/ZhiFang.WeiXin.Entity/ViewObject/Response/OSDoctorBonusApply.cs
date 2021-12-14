using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.WeiXin.Entity.ViewObject.Request;

namespace ZhiFang.WeiXin.Entity.ViewObject.Response
{
    [DataContract]
    [DataDesc(CName = "医生奖金结算申请及审核流程过程用", ClassCName = "OSDoctorBonusApply", ShortCode = "OSDoctorBonusApply", Desc = "医生奖金结算申请及审核流程过程用")]
    public class OSDoctorBonusApply
    {
        [DataMember]
        [DataDesc(CName = "结算周期是否结算过", ShortCode = "IsSettlement", Desc = "结算周期是否结算过")]
        public virtual bool IsSettlement { get; set; }
        [DataMember]
        [DataDesc(CName = "结算周期", ShortCode = "BonusFormRound", Desc = "结算周期")]
        public virtual string BonusFormRound { get; set; }
        [DataMember]
        [DataDesc(CName = "医生奖金结算单", ShortCode = "OSDoctorBonusForm", Desc = "医生奖金结算单")]
        public virtual OSDoctorBonusForm OSDoctorBonusForm { get; set; }
        [DataMember]
        [DataDesc(CName = "单个医生奖金结算记录", ShortCode = "OSDoctorBonus", Desc = "单个医生奖金结算记录")]
        public virtual OSDoctorBonus OSDoctorBonus { get; set; }

        [DataMember]
        [DataDesc(CName = "医生奖金结算记录明细", ShortCode = "OSDoctorBonusList", Desc = "医生奖金结算记录明细")]
        public virtual IList<OSDoctorBonus> OSDoctorBonusList { get; set; }
    }
}
