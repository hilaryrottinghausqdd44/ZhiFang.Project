using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhiFang.Entity.ReagentSys.Client.ViewObject.Response
{
    /// <summary>
    /// 客户端授权机构初始化授权进度VO
    /// </summary>
    public class LicenseGuideVO
    {
        public LicenseGuideVO() { }
        public virtual string Id { get; set; }
        public virtual string CName { get; set; }
        public virtual int Sataus { get; set; }
        public virtual string SatausName { get; set; }
        public virtual string Memo { get; set; }
        public virtual int DispOrder { get; set; }
    }
}
