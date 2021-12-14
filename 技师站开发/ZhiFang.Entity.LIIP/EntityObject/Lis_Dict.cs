using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LIIP
{
    #region LisDict

    /// <summary>
    /// SCMsgType object for NHibernate mapped table 'SC_MsgType'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "Lis字典表", ClassCName = "LisDict", ShortCode = "LisDict", Desc = "Lis字典表")]
    public class LisDict
    {
        #region Public Properties

        [DataMember]
        [DataDesc(CName = "Id", ShortCode = "Id", Desc = "Id", ContextType = SysDic.All, Length = 100)]
        public virtual string Id { get; set; }

        [DataMember]
        [DataDesc(CName = "字典名称", ShortCode = "DictName", Desc = "字典名称", ContextType = SysDic.All, Length = 200)]
        public virtual string DictName { get; set; }

        [DataMember]
        [DataDesc(CName = "名称", ShortCode = "CName", Desc = "名称", ContextType = SysDic.All, Length = 200)]
        public virtual string CName { get; set; }

        [DataMember]
        [DataDesc(CName = "Lis代码", ShortCode = "LisCode", Desc = "LisCode", ContextType = SysDic.All, Length = 50)]
        public virtual string LisCode { get; set; }

        [DataMember]
        [DataDesc(CName = "His代码", ShortCode = "HisCode", Desc = "HisCode", ContextType = SysDic.All, Length = 50)]
        public virtual string HisCode { get; set; }

        [DataMember]
        [DataDesc(CName = "次序", ShortCode = "DispOrder", Desc = "DispOrder", ContextType = SysDic.All, Length = 50)]
        public virtual string DispOrder { get; set; }

        #endregion
    }
    #endregion
}