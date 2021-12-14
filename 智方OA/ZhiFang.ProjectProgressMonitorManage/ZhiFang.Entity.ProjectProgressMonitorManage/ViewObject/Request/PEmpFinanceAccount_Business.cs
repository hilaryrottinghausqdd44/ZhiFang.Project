using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ProjectProgressMonitorManage.ViewObject.Request
{
    [DataContract]
    public class PEmpFinanceAccount_Business : BaseEntity
    {
        #region
        /// <summary>
        /// 该员工是否已经存在员工财务账户
        /// </summary>
        [DataMember]
        public virtual bool IsExist { get; set; }
        #endregion
    }
}
