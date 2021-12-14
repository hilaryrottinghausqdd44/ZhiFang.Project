using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity.ViewObject.Request
{
    [DataContract]
    public class OSDoctorBonusBase : BaseEntity
    {
        protected string _OSUConsumerFormIDStr;
        #region
        /// <summary>
        /// 医生奖金结算记录的用户消费单ID字符串,如(1233,12223,23333)
        /// </summary>
        [DataMember]
        [DataDesc(CName = "医生奖金结算记录的用户消费单ID字符串", ShortCode = "OSUConsumerFormIDStr", Desc = "医生奖金结算记录的用户消费单ID字符串", ContextType = SysDic.All, Length =Int32.MaxValue)]
        public virtual string OSUConsumerFormIDStr { get; set; }

        #endregion
    }
}
