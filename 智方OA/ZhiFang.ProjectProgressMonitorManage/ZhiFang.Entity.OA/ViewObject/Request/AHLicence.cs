using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.OA.ViewObject.Request
{
    [DataContract]
    /// <summary>
    /// 单站点授权/服务器授权/服务器仪器授权
    /// </summary>
    public class AHLicence : BaseEntity
    {
        
            [DataMember]
        [DataDesc(CName = "是否是新申请", ShortCode = "ISNewApply", Desc = "是否是新申请", ContextType = SysDic.All, Length = 1)]
        public virtual bool ISNewApply { get; set; }
        [DataMember]
        [DataDesc(CName = "授权类型名称", ShortCode = "LicenceTypeName", Desc = "授权类型名称", ContextType = SysDic.All, Length = 50)]
        public virtual string LicenceTypeName { get; set; }
        [DataMember]
        [DataDesc(CName = "流程状态名称", ShortCode = "StatusName", Desc = "流程状态名称", ContextType = SysDic.All, Length = 50)]
        public virtual string StatusName { get; set; }

        [DataMember]
        [DataDesc(CName = "流程状态名称2(授权中,特批授权中,授权完成)", ShortCode = "TwoStatusName", Desc = "流程状态名称2", ContextType = SysDic.All, Length = 50)]
        public virtual string TwoStatusName { get; set; }
        
        [DataMember]
        [DataDesc(CName = "有效期状态状态Id", ShortCode = "LicenceStatusId", Desc = "有效期状态状态Id", ContextType = SysDic.All, Length = 50)]
        public virtual int LicenceStatusId { get; set; }

        [DataMember]
        [DataDesc(CName = "有效期状态名称", ShortCode = "LicenceStatusName", Desc = "有效期状态名称", ContextType = SysDic.All, Length =50)]
        public virtual string LicenceStatusName { get; set; }

        /// <summary>
        /// (计算)天数=授权结束日期-服务器当前日期
        /// </summary>
        [DataMember]
        [DataDesc(CName = "(计算)天数", ShortCode = "CalcDays", Desc = "(计算)天数", ContextType = SysDic.All, Length = 50)]
        public virtual int CalcDays { get; set; }

        [DataMember]
        [DataDesc(CName = "授权操作备注", ShortCode = "OperationMemo", Desc = "授权操作备注", ContextType = SysDic.All, Length = 500)]
        public virtual string OperationMemo { get; set; }
        
    }
}
