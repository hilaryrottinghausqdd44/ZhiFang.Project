using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ZhiFang.Digitlab.Entity
{
    /// <summary>
    /// 微生物药敏结果
    /// </summary>
    public class MicroSuscResult
    {
        /// <summary>
        /// 抗生素通道号
        /// </summary>
        [DataMember]
        public string AntiChannel { get; set; }

        /// <summary>
        ///药敏结果 
        /// </summary>
        [DataMember]
        public string Suscept { get; set; }


        /// <summary>
        /// 数值结果
        /// </summary>
        [DataMember]
        public string SusQuan { get; set; }

        /// <summary>
        /// 数值符号
        /// </summary>
        [DataMember]
        public string SusDesc { get; set; }

        /// <summary>
        /// 抗生素类型
        /// </summary>
        [DataMember]
        public string AntiGroupType { get; set; }

        /// <summary>
        /// 专业描述
        /// </summary>
        [DataMember]
        public string ExpertDesc { get; set; }

        /// <summary>
        /// 细菌结果描述
        /// </summary>
        [DataMember]
        public string MicroResultDesc { get; set; }

        /// <summary>
        /// 抗生素描述
        /// </summary>
        [DataMember]
        public string AntiDesc { get; set; }


    }
}
