using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.OA.ViewObject.Response
{
    [DataContract]
    [DataDesc(CName = "ApplyProgramInfo", ShortCode = "ApplyProgramInfo", Desc = "主程序授权申请信息")]
    public class ApplyProgramInfo
    {
        [DataMember]
        [DataDesc(CName = "LicenceTypeId", ShortCode = "LicenceTypeId", Desc = "当前申请授权类型")]
        public virtual long? LicenceTypeId { get; set; }
        [DataMember]
        [DataDesc(CName = "LicenceTypeName", ShortCode = "LicenceTypeName", Desc = "授权类型")]
        public virtual string LicenceTypeName { get; set; }
        [DataMember]
        [DataDesc(CName = "NodeTableCounts", ShortCode = "NodeTableCounts", Desc = "当前授权站点数")]
        public virtual int NodeTableCounts { get; set; }

        [DataMember]
        [DataDesc(CName = "ProgramName", ShortCode = "ProgramName", Desc = "授权程序")]
        public virtual string ProgramName { get; set; }

        [DataMember]
        [DataDesc(CName = "ProgramID", ShortCode = "ProgramID", Desc = "授权程序Id")]
        public virtual long? ProgramID { get; set; }

        [DataMember]
        [DataDesc(CName = "SQH", ShortCode = "SQH", Desc = "授权号")]
        public virtual string SQH { get; set; }

        [DataMember]
        [DataDesc(CName = "LicenceDate", ShortCode = "LicenceDate", Desc = "截至日期")]
        public virtual DateTime? LicenceDate { get; set; }

        [DataMember]
        [DataDesc(CName = "PreNodeTableCounts", ShortCode = "PreNodeTableCounts", Desc = "上次授权站点数")]
        public virtual int PreNodeTableCounts { get; set; }
        [DataMember]
        [DataDesc(CName = "PreLicenceDate", ShortCode = "PreLicenceDate", Desc = "上次授权截至日期")]
        public virtual DateTime? PreLicenceDate { get; set; }

        [DataMember]
        [DataDesc(CName = "PreLicenceTypeId", ShortCode = "PreLicenceTypeId", Desc = "上次授权类型Id")]
        public virtual long? PreLicenceTypeId { get; set; }

        [DataMember]
        [DataDesc(CName = "PreLicenceTypeName", ShortCode = "PreLicenceTypeName", Desc = "上次授权类型名称")]
        public virtual string PreLicenceTypeName { get; set; }

        /// <summary>
        /// (计算)天数=授权结束日期-服务器当前日期
        /// </summary>
        [DataMember]
        [DataDesc(CName = "(计算)天数", ShortCode = "CalcDays", Desc = "(计算)天数", ContextType = SysDic.All, Length = 50)]
        public virtual int CalcDays { get; set; }
        [DataMember]
        [DataDesc(CName = "有效期状态状态Id", ShortCode = "LicenceStatusId", Desc = "有效期状态状态Id", ContextType = SysDic.All, Length = 50)]
        public virtual int LicenceStatusId { get; set; }

        [DataMember]
        [DataDesc(CName = "有效期状态", ShortCode = "LicenceStatusName", Desc = "有效期状态名称", ContextType = SysDic.All, Length = 50)]
        public virtual string LicenceStatusName { get; set; }
       
    }
}
