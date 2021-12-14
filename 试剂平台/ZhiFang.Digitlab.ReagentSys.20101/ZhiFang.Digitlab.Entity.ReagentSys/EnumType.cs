using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Policy;
using System.Collections;

namespace ZhiFang.Digitlab.Entity.ReagentSys
{
    public enum InterfaceType
    {
        无类型 = 0,
        供货单接口 = 1,
        订货单接口 = 2
    }

    public enum InterfaceIndex
    {
        无类型 = 0,
        四川迈克 = 1,
        北京巴瑞 = 2
    }

    public enum CodeValue
    {
        无 = 0,
        无法从Session中获取用户ID和名称= 1001,
    }
}