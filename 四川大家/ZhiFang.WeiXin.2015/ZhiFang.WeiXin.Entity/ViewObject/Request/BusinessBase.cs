using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity.ViewObject.Request
{
    [DataContract]
    public class BusinessBase : BaseEntity
    {
        protected string _StatusName;
        #region
        /// <summary>
        /// 对此业务实体操作时的描述
        /// </summary>
        [DataMember]
        public virtual string OperationMemo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [DataDesc(CName = "状态名称", ShortCode = "StatusName", Desc = "状态名称", ContextType = SysDic.All, Length = 8)]
        public virtual string StatusName { get; set; }

        #endregion
    }
}
