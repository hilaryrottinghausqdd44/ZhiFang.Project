using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Entity.OA.ViewObject.Response
{
    public class SignLogEmpList
    {        
        public string WeekInfo { get; set; }

        public bool IsWorkDay { get; set; }

        public string ATEventDateCode { get; set; }

        public IList<SignLog> SignLogL { get; set; }
    }   
}
