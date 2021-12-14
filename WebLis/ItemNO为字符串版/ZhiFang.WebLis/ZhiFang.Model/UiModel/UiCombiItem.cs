using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ZhiFang.Model.UiModel
{
    //[DataContract]
    public class UiCombiItem
    {
        public UiCombiItem() { }
       
        public string CombiItemName { get; set; }
        
        public string CombiItemNo { get; set; }

        public string Prices { get; set; }
        public List<UiCombiItemDetail> CombiItemDetailList { get; set; }
    }
}
