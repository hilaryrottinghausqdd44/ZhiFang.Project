using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LIIP;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LIIP
{
    /// <summary>
    ///
    /// </summary>
    public interface IBSCIMInfomationContent : IBGenericManager<SCIMInfomationContent>
    {
        BaseResultBool SCIMInfomationContentReBack(string EmpId, string EmpName, long IMID);
        BaseResultBool SCIMInfomationContentReRead(string EmpId, string EmpName, long IMID);
    }
}