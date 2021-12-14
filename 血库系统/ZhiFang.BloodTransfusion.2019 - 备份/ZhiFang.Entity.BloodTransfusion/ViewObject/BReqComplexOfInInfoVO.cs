using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
    /// <summary>
    /// 输血申请综合查询:入库信息VO
    /// 包含入库血袋信息,血袋的交叉配血信息,血袋的加工业务信息
    /// </summary>
    [DataContract]
    public class BReqComplexOfInInfoVO
    {
        public BReqComplexOfInInfoVO() { }

        /// <summary>
        /// 交叉配血主单Id
        /// </summary>
        [DataMember]
        public virtual string BPreFormId { get; set; }
        /// <summary>
        /// 交叉配血明细Id
        /// </summary>
        [DataMember]
        public virtual string BPreItemId { get; set; }
        /// <summary>
        /// 血袋号
        /// </summary>
        [DataMember]
        public virtual string BBagCode { get; set; }
        /// <summary>
        /// 产品码
        /// </summary>
        [DataMember]
        public virtual string PCode { get; set; }
        /// <summary>
        /// 惟一码
        /// </summary>
        [DataMember]
        public virtual string B3Code { get; set; }

        [DataMember]
        [DataDesc(CName = "Bloodstyle", ShortCode = "Bloodstyle", Desc = "Bloodstyle")]
        public virtual Bloodstyle Bloodstyle { get; set; }
        /// <summary>
        /// 交叉配血时间:在配血明细表 Blood_bpreitem
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual DateTime? BPreDate { get; set; }
        /// <summary>
        /// 交叉配血人Id，在配血主单表 Blood_bpreForm(OperatorID)
        /// </summary>
        [DataMember]
        public virtual string BPOperatorID { get; set; }
        /// <summary>
        /// 交叉配血人
        /// </summary>
        [DataMember]
        public virtual string BPOperator { get; set; }

        /// <summary>
        /// 入库时间
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual DateTime? BInDate { get; set; }

        /// <summary>
        /// 入库操作者ID(OperatorID):入库人员，在入库主单表
        /// </summary>
        [DataMember]
        public virtual string InOperatorID { get; set; }
        /// <summary>
        /// 入库操作者:入库人员
        /// </summary>
        [DataMember]
        public virtual string InOperator { get; set; }

        /// <summary>
        /// 血袋复检人ID ，在Blood_BagABOCheck 血袋血型复核表
        /// </summary>
        [DataMember]
        public virtual string ReCheckID { get; set; }
        /// <summary>
        /// 血袋复检人
        /// </summary>
        [DataMember]
        public virtual string ReCheck { get; set; }
        /// <summary>
        /// 血袋复检时间在Blood_BagABOCheck 血袋血型复核表
        /// </summary>
        [DataMember]
        public virtual string ReCheckTime { get; set; }

        /// <summary>
        /// 加工明细ID
        /// </summary>
        [DataMember]
        public virtual string ProcessId { get; set; }
        /// <summary>
        /// 加工类型编码 Blood_BagProcess 、字典表 Blood_BagProcessType
        /// </summary>
        [DataMember]
        public virtual string PTNo { get; set; }
        /// <summary>
        /// 加工类型
        /// </summary>
        [DataMember]
        public virtual string PTCName { get; set; }


    }
}
