using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Entity.OA.ViewObject.Response
{
    public class SignLogEmp : SignLog
    {        
        public string EmpId { get; set; }
        public string EmpName { get; set; }
        public string HeadImgUrl { get; set; }
    }
}
