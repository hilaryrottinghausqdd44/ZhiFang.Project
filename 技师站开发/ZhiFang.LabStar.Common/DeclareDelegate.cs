using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ZhiFang.Common.Public;

namespace ZhiFang.LabStar.Common
{
    public delegate string SendSysMessageDelegate(object messageEntity, string messageType, string sectionID, IList<string> toEmpIDList);

    public class SysDelegateVar
    {
        public static SendSysMessageDelegate SendSysMsgDelegateVar;
    }


}
