using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ZhiFang.Entity.Base;
using Newtonsoft.Json;

namespace ZhiFang.WeiXin.Entity.ViewObject.Response
{
    #region OSDoctorChargeInfoVO

    [DataContract]
    [DataDesc(CName = "医生结算信息VO", ClassCName = "OSDoctorChargeInfoVO", ShortCode = "OSDoctorChargeInfoVO", Desc = "医生结算信息VO")]
    public class OSDoctorChargeInfoVO
    {

        #region Public Properties

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "主键ID", ShortCode = "Id", Desc = "主键ID", ContextType = SysDic.Number, Length = 8)]
        public virtual long Id { get; set; }

        [DataMember]
        [DataDesc(CName = "医生姓名", ShortCode = "DoctorName", Desc = "医生姓名", ContextType = SysDic.All, Length = 20)]
        public virtual string DN { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "银行种类", ShortCode = "BankID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? BID { get; set; }

        [DataMember]
        [DataDesc(CName = "银行账户", ShortCode = "BankAccount", Desc = "银行账户", ContextType = SysDic.All, Length = 50)]
        public virtual string BA { get; set; }

        [DataMember]
        [DataDesc(CName = "银行名称", ShortCode = "BankAccount", Desc = "银行名称", ContextType = SysDic.All, Length = 50)]
        public virtual string BN { get; set; }

        [DataMember]
        [DataDesc(CName = "医生结算单列表", ShortCode = "OSDoctorBonusList", Desc = "医生结算单列表", ContextType = SysDic.All, Length = 50)]
        public virtual IList<OSDoctorBonusVO> OSDoctorBonusList { get; set; }

        [DataMember]
        [DataDesc(CName = "医生患者消费单列表", ShortCode = "OSUserConsumerList", Desc = "医生患者消费单列表", ContextType = SysDic.All, Length = 50)]
        public virtual IList<OSUserConsumerVO> OSUserConsumerList { get; set; }

        [DataMember]
        [DataDesc(CName = "医生患者消费单(按天)列表", ShortCode = "OSUserConsumerDayList", Desc = "医生患者消费单(按天)列表", ContextType = SysDic.All, Length = 50)]
        public virtual List<OSUserConsumerDayVO> OSUserConsumerDayList { get; set; }

        #endregion
    }


    public class OSDoctorBonusVO
    {

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "主键ID", ShortCode = "Id", Desc = "主键ID", ContextType = SysDic.Number, Length = 8)]
        public virtual long Id { get; set; }

        [DataMember]
        [DataDesc(CName = "结算单号", ShortCode = "BonusCode", Desc = "结算单号", ContextType = SysDic.All, Length = 50)]
        public virtual string BonusCode { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "费用", ShortCode = "Price", Desc = "费用", ContextType = SysDic.All, Length = 8)]
        public virtual double? Price { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "操作时间", ShortCode = "DT", Desc = "操作时间", ContextType = SysDic.DateTime)]
        public virtual DateTime? DT { get; set; }
    }

    public class OSUserConsumerDayVO
    {
        /// <summary>
        /// 日期
        /// </summary>
        [DataMember]        
        public virtual string DateTime { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [DataMember]
        public virtual int Count { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "费用", ShortCode = "Price", Desc = "费用", ContextType = SysDic.All, Length = 8)]
        public virtual double Price { get; set; }
    }

    public class OSUserConsumerVO
    {
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "主键ID", ShortCode = "Id", Desc = "主键ID", ContextType = SysDic.Number, Length = 8)]
        public virtual long Id { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医嘱单ID", ShortCode = "DOFID", Desc = "医嘱单ID", ContextType = SysDic.Number, Length = 8)]
        public virtual long? DOFID { get; set; }

        [DataMember]
        [DataDesc(CName = "消费单号", ShortCode = "CFCode", Desc = "消费单号", ContextType = SysDic.All, Length = 50)]
        public virtual string CFCode { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "费用", ShortCode = "Price", Desc = "费用", ContextType = SysDic.All, Length = 8)]
        public virtual double? Price { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "操作时间", ShortCode = "DT", Desc = "操作时间", ContextType = SysDic.DateTime)]
        public virtual DateTime? DT { get; set; }

    }

    #endregion
}
