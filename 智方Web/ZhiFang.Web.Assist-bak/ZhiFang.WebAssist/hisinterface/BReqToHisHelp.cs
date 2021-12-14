using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using ZhiFang.WebAssist.Common;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.WebAssist;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.WebAssist.hisinterface
{
    /// <summary>
    /// 用血申请与HIS交互辅助类
    /// </summary>
    public static class BReqToHisHelp
    {
        //正在与HIS交互的业务单的Id集合
        public static IList<string> ToHisCurIdList = new List<string>();

    }

}