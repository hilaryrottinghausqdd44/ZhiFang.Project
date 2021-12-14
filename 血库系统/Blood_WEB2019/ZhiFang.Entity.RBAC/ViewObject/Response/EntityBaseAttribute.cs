using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Entity.RBAC.ViewObject.Response
{

    #region 预置条件
    /// <summary>
    /// 预置条件的实体对象的基本属性
    /// </summary>
    public class EntityBaseAttribute
    {
        /// <summary>
        /// 属性显示名称
        /// </summary>
        public string CName { get; set; }
        /// <summary>
        /// 属性编码
        /// </summary>
        public string InteractionField { get; set; }
        /// <summary>
        /// 属性值类型
        /// </summary>    
        public string ValueType { get; set; }
    }

    #endregion
}
