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
    /// 服务器仪器授权明细
    /// </summary>
    public class AHServerEquipLicenceBase: AHLicence
    {
        [DataMember]
        [DataDesc(CName = "系统程序SQH", ShortCode = "SYSSQH", Desc = "系统程序SQH", ContextType = SysDic.All, Length = 50)]
        public virtual string SYSSQH { get; set; }

    }
}
