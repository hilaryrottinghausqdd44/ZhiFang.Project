using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity.ViewObject.Response
{
    [DataContract]
    [DataDesc(CName = "检测项目产品分类VO", ClassCName = "OSItemProductClassTreeLinkVO", ShortCode = "OSItemProductClassTreeLinkVO", Desc = "检测项目产品分类VO")]
    public class OSItemProductClassTreeLinkVO : OSItemProductClassTreeLink
    {
        #region Member Variables

        protected string _itemProductClassTreeCName;
        protected string _itemCName;

        #endregion

        #region Constructors

        public OSItemProductClassTreeLinkVO() { }

        #endregion

        #region Public Properties
        [DataMember]
        [DataDesc(CName = "项目分类名称", ShortCode = "ItemProductClassTreeCName", Desc = "项目分类名称", ContextType = SysDic.All, Length = 50)]
        public virtual string ItemProductClassTreeCName
        {
            get { return _itemProductClassTreeCName; }
            set { _itemProductClassTreeCName = value; }
        }
        [DataMember]
        [DataDesc(CName = "项目名称", ShortCode = "ItemCName", Desc = "项目名称", ContextType = SysDic.All, Length = 50)]
        public virtual string ItemCName
        {
            get { return _itemCName; }
            set { _itemCName = value; }
        }
        #endregion
    }
}
