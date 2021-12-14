using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ZhiFang.Digitlab.Entity
{
    /// <summary>
    /// 微生物检验详细结果
    /// </summary>
    public class MicroStepInfo
    {
        /// <summary>
        /// 细菌通道号
        /// </summary>
        [DataMember]
        public string MicroChannel{get;set;}   
        
        
        /// <summary>
        /// 细菌描述
        /// </summary>
        [DataMember]
        public string MicroDesc{get;set;}


        /// <summary>
        /// 微生物药敏结果列表
        /// </summary>
        [DataMember]
        public IList<MicroSuscResult> MicroSuscResultList { get; set; }
    
    }
}
