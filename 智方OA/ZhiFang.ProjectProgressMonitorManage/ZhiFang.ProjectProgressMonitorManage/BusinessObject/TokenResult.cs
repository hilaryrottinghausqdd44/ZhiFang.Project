using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZhiFang.ProjectProgressMonitorManage
{
    public class TokenResult : ErrResult
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
    }
    public class JSAPITokenResult : ErrResult
    {
        public string ticket { get; set; }
        public int expires_in { get; set; }
    }
}