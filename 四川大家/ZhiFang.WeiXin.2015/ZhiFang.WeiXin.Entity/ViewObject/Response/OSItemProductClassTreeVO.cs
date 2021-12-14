using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity.ViewObject.Response
{
    [DataContract]
    [DataDesc(CName = "检测项目产品分类树(添加自定义属性)", ClassCName = "OSItemProductClassTreeVO", ShortCode = "OSItemProductClassTreeVO", Desc = "检测项目产品分类树(添加自定义属性)")]
    public class OSItemProductClassTreeVO : BaseEntity
    {
        //添加自定义属性
        [DataMember]
        [DataDesc(CName = "是否有子节点", ShortCode = "IsLeaf", Desc = "是否有子节点")]
        public virtual bool IsLeaf { get; set; }
    }
}
