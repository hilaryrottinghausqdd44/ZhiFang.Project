using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{

    [DataContract]
    public class SampleItem
    {

        /// <summary>
        /// 与仪器通讯交互的实体对象
        /// </summary>
        [DataMember]
        public string ItemValue { get; set; }

        [DataMember]
        public string TestType { get; set; }

        [DataMember]
        public string ValueDate { get; set; }

        /// <summary>
        /// itemcode 对应Equipitem表中的Channel
        /// </summary>
        [DataMember]
        public string ItemCode { get; set; }

        [DataMember]
        public string ItemSValue { get; set; }

        /// <summary>
        /// 仪器微生物检验详细结果列表
        /// </summary>
        [DataMember]
        public IList<MicroStepInfo> MicroStepInfoList { get; set; }
    }
}