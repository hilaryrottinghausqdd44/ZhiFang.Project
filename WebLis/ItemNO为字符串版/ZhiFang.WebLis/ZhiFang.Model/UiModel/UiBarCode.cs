using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ZhiFang.Model.UiModel
{
    
    public class UiBarCode
    {
        public UiBarCode() { }
      
        public string BarCode { get; set; }
       
        public string SampleType { get; set; }
        public string SampleTypeName { get; set; }
        public List<string> ItemList { get; set; }
        
        public string ColorName { get; set; }
        
        public string ColorValue { get; set; }
      
       public List<SampleTypeDetail> SampleTypeDetailList { get; set; }
    }
}
