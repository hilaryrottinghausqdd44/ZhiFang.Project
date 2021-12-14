using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.Common.Public;

namespace ZhiFang.Digitlab.Entity.ReagentSys
{
    public class TimerCheckOrderDoc : System.Timers.Timer
    {
        public long OrderDocID { get; set; }
    }

}
