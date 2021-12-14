using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Model.UiModel
{
    public class GroupItemEntity
    { //1:account(物流人员ID)  2:clientlist(已选单位) 3:clientno(所属单位)  
        public string table { get; set; }

        public List<string> itemnolist { get; set; }

        public string itemno { get; set; }

        public string labcode { get; set; }
    }
}
