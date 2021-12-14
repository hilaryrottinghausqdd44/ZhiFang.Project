using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity.ViewObject.Request
{
    [DataContract]
    public class BLabTestItemVO : BLabTestItem
    {       
        #region
        /// <summary>
        /// 细项
        /// </summary>
        [DataMember]
        public virtual List<BLabGroupItemVO> LabSubTestItem { get; set; }

        #endregion
    }
}
