using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Model.UiModel
{
    public class LogisticsEntity
    {
        //1:account(物流人员ID)  2:clientlist(已选单位) 3:clientno(所属单位)  
        public string Account { get; set; }

        public List<string> ClientList { get; set; }

        public string ClientNo { get; set; }
    }
}
