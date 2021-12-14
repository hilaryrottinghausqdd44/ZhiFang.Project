using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
    /// <summary>
    /// 与仪器通讯交互的实体对象
    /// </summary>
    [DataContract]
	public class SampleForm
	{
        /// <summary>
        /// 仪器标识
        /// </summary>
        [DataMember]
        public string EquipChannel  { get; set; }
        /// <summary>
        /// 样本号
        /// </summary>
        [DataMember]
        public string SampleNo  { get; set; }
        /// <summary>
        /// 条码号
        /// </summary>
        [DataMember]
        public string SerialNo { get; set; }
        /// <summary>
        /// 样本架位号
        /// </summary>
        [DataMember]
        public string SamPosition { get; set; }
        /// <summary>
        /// 检验时间
        /// </summary>
        [DataMember]
        public string ETestDate { get; set; }
        /// <summary>
        /// 仪器项目
        /// </summary>
        [DataMember]
        public IList<SampleItem> SampleItemList{ get; set; }
        /// <summary>
        /// 病历号
        /// </summary>
        [DataMember]
        public string PatNo { get; set; }
        /// <summary>
        /// 样本类型
        /// </summary>
         [DataMember]
        public string SampleTypeNo { get; set; }
         /// <summary>
         /// 测试类型
         /// </summary>
         [DataMember]
         public string TestTypeNo { get; set; }

        /// <summary>
         /// 仪器结果备注
        /// </summary>
         [DataMember]
         public string EResultComment { get; set; }

         /// <summary>
         /// 仪器图片结果
         /// </summary>
         [DataMember]
         public IList<EquipPic> EquipPicList { get; set; }

         /// <summary>
         /// 仪器质控数据ID
         /// </summary>
         [DataMember]
         public int? QCDataLotNo { get; set; }

         /// <summary>
         /// 仪器阳性报警时间
         /// </summary>
         [DataMember]
         public string WarnPositiveDatetime { get; set; }
	}
}