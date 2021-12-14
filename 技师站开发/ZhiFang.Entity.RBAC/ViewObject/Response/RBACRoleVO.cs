using Newtonsoft.Json;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.RBAC.ViewObject.Response
{
    /// <summary>
	/// 
	/// </summary>
    [DataContract]
    [DataDesc(CName = "待选角色", ClassCName = "RBACRoleVO", ShortCode = "待选角色", Desc = "角色权限")]
    public class RBACRoleVO
    {
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "主键ID", ShortCode = "Id", Desc = "主键ID", ContextType = SysDic.Number, Length = 8)]
        public virtual long? Id { get; set; }
        [DataMember]
        [DataDesc(CName = "名称", ShortCode = "CName", Desc = "名称", ContextType = SysDic.All, Length = 50)]
        public virtual string CName { get; set; }
    }
}
