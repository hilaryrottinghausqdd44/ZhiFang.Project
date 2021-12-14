using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.OA.ViewObject.Response
{
    [DataContract]
    public class ApplyAHServerLicence
    {
        [DataMember]
        [DataDesc(CName = "PClientLRNo1", ShortCode = "PClientLRNo1", Desc = "客户原主服务器授权申请号")]
        public virtual string PClientLRNo1 { get; set; }
        [DataMember]
        [DataDesc(CName = "PClientLRNo2", ShortCode = "PClientLRNo2", Desc = "客户原备服务器授权申请号")]
        public virtual string PClientLRNo2 { get; set; }
        [DataMember]
        [DataDesc(CName = "LRNoIsRepeat", ShortCode = "LRNoIsRepeat", Desc = "客户原授权申请号与当前授权申请号是否一致")]
        public virtual bool LRNoIsIdentical { get; set; }

        [DataMember]
        [DataDesc(CName = "SQH", ShortCode = "SQH", Desc = "授权号")]
        public virtual string SQH { get; set; }

        [DataMember]
        [DataDesc(CName = "授权申请主要信息", ShortCode = "AHServerLicence", Desc = "授权申请主要信息")]
        public virtual AHServerLicence AHServerLicence { get; set; }

        [DataMember]
        [DataDesc(CName = "服务器程序授权申请的程序类型", ShortCode = "LicenceProgramTypeList", Desc = "服务器程序授权申请的程序类型")]
        public virtual IList<LicenceProgramType> LicenceProgramTypeList { get; set; }

        [DataMember]
        [DataDesc(CName = "程序授权申请信息", ShortCode = "ApplyProgramInfoList", Desc = "程序授权申请信息")]
        public virtual IList<ApplyProgramInfo> ApplyProgramInfoList { get; set; }

        [DataMember]
        [DataDesc(CName = "服务器程序授权明细", ShortCode = "AHServerProgramLicenceList", Desc = "服务器程序授权明细")]
        public virtual IList<AHServerProgramLicence> AHServerProgramLicenceList { get; set; }

        [DataMember]
        [DataDesc(CName = "服务器仪器授权明细", ShortCode = "AHServerEquipLicenceList", Desc = "服务器仪器授权明细")]
        public virtual IList<AHServerEquipLicence> AHServerEquipLicenceList { get; set; }
    }
}
