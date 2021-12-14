using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Tools;

namespace ZhiFang.Model.UiModel
{

    public class SampleTypeBarCodeInfo
    {
        public SampleTypeBarCodeInfo() { }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string CName { get; set; }

        public string BarCode { get; set; }

        [JsonConverter(typeof(JsonConvertClass))]
        public DateTime ReceiveDate { get; set; }


        [JsonConverter(typeof(JsonConvertClass))]
        public DateTime CollectDate { get; set; }

        public string BarCodeFormNo { get; set; }

        [JsonConverter(typeof(JsonConvertClass))]
        public DateTime OperDate { get; set; }
    }
}
