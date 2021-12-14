using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.LIIP;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LIIP
{
    /// <summary>
    ///
    /// </summary>
    public interface IBWXMessageSendTask : IBGenericManager<WXMessageSendTask>
    {
        bool Add_WXMessageSendOut(long id, long peopleId, string empID, string empName);
    }
}