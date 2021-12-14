using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.RBAC
{
    /// <summary>
    /// HIS调用时当前用户信息
    /// </summary>
    [DataContract]
    public class SysCurUserInfo
    {
        public SysCurUserInfo() { }

        /// <summary>
        /// 用户ID
        /// </summary>
        [DataMember]
        public virtual string UserId { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        [DataMember]
        public virtual string UserCName { get; set; }

        /// <summary>
        /// 对应的帐号
        /// </summary>
        [DataMember]
        public virtual string Account { get; set; }
        /// <summary>
        /// 对应的帐号密码
        /// </summary>
        [DataMember]
        public virtual string Password { get; set; }

        /// <summary>
        /// 病区Id
        /// </summary>
        [DataMember]
        public virtual string WardId { get; set; }
        /// <summary>
        /// 病区名称
        /// </summary>
        [DataMember]
        public virtual string WardCName { get; set; }
        /// <summary>
        /// 病区HIS对照码
        /// </summary>
        [DataMember]
        public virtual string HisWardId { get; set; }

        /// <summary>
        /// 科室Id
        /// </summary>
        [DataMember]
        public virtual string DeptId { get; set; }
        /// <summary>
        /// 科室
        /// </summary>
        [DataMember]
        public virtual string DeptCName { get; set; }
        /// <summary>
        /// 科室HIS对照码
        /// </summary>
        [DataMember]
        public virtual string HisDeptId { get; set; }

        /// <summary>
        /// 医生ID
        /// </summary>
        [DataMember]
        public virtual string DoctorId { get; set; }
        /// <summary>
        /// 医生姓名
        /// </summary>
        [DataMember]
        public virtual string DoctorCName { get; set; }
        /// <summary>
        /// HIS医生对照码
        /// </summary>
        [DataMember]
        public virtual string HisDoctorId { get; set; }
        /// <summary>
        /// 医生所属等级Id
        /// </summary>
        [DataMember]
        public virtual string GradeId { get; set; }
        /// <summary>
        /// 医生所属等级
        /// </summary>
        [DataMember]
        public virtual string GradeName { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "用血量审核范围下限值", ShortCode = "LowLimit ", Desc = "用血量审核范围下限值", ContextType = SysDic.All)]
        public virtual double? LowLimit { get; set; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "用血量审核范围上限值", ShortCode = "UpperLimit ", Desc = "用血量审核范围上限值", ContextType = SysDic.All)]
        public virtual double? UpperLimit { get; set; }
    }
}
