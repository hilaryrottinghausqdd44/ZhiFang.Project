using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
    public class EquipPic
    {
        /// <summary>
        /// 图形编号
        /// </summary>
        [DataMember]
        public string GraphNo { get; set; }

        /// <summary>
        /// 图形名称
        /// </summary>
        [DataMember]
        public string GraphName { get; set; }

        /// <summary>
        ///图形数据：GraphData，即Base64编码字符串
        /// </summary>
        [DataMember]
        public string GraphData { get; set; }

        /// <summary>
        /// 图形类型：GraphType，即传入的图片的格式代码
        /// </summary>
        [DataMember]
        public string GraphType { get; set; }

        /// <summary>
        /// Base64格式的图片
        /// </summary>
        [DataMember]
        public int ReportFlag { get; set; }

        /// <summary>
        /// 图形打印次序
        /// </summary>
        [DataMember]
        public int DispOrder { get; set; }
    }
}
