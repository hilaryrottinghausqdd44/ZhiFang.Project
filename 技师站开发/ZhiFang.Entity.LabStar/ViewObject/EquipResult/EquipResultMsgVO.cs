using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    [DataContract]
    [DataDesc(CName = "仪器结果处理消息", ClassCName = "EquipResultMsgVO", ShortCode = "EquipResultMsgVO", Desc = "")]
    public class EquipResultMsgVO
    {
        [DataMember]
        [DataDesc(CName = "消息名称", ShortCode = "ResultMsgName", Desc = "")]
        public string ResultMsgName { get; set; }

        [DataMember]
        [DataDesc(CName = "仪器检验单ID", ShortCode = "EquipFormID", Desc = "")]
        public string EquipFormID { get; set; }

        [DataMember]
        [DataDesc(CName = "检验单ID", ShortCode = "TestFormID", Desc = "")]
        public string TestFormID { get; set; }

        [DataMember]
        [DataDesc(CName = "仪器ID", ShortCode = "EquipID", Desc = "")]
        public string EquipID { get; set; }

        [DataMember]
        [DataDesc(CName = "仪器名称", ShortCode = "EquipName", Desc = "")]
        public string EquipName { get; set; }

        [DataMember]
        [DataDesc(CName = "检验小组ID", ShortCode = "SectionID", Desc = "")]
        public string SectionID { get; set; }

        [DataMember]
        [DataDesc(CName = "检验小组名称", ShortCode = "SectionName", Desc = "")]
        public string SectionName { get; set; }

        [DataMember]
        [DataDesc(CName = "条码号", ShortCode = "BarCode", Desc = "")]
        public string BarCode { get; set; }

        [DataMember]
        [DataDesc(CName = "样本号", ShortCode = "GSampleNo", Desc = "")]
        public string GSampleNo { get; set; }

        [DataMember]
        [DataDesc(CName = "核收日期", ShortCode = "GTestDate", Desc = "")]
        public string GTestDate { get; set; }

        [DataMember]
        [DataDesc(CName = "数据处理方式", ShortCode = "DataProcessType", Desc = "")]
        public string DataProcessType { get; set; }

        [DataMember]
        [DataDesc(CName = "成功标志", ShortCode = "", Desc = "0:成功；-1:失败；-2:放弃处理")]
        public int SuccessFlag { get; set; }

        [DataMember]
        [DataDesc(CName = "成功提示信息", ShortCode = "IsSuccessName", Desc = "")]
        public string SuccessHint { get; set; }

        [DataMember]
        [DataDesc(CName = "失败原因", ShortCode = "ErrorInfo", Desc = "")]
        public string ErrorInfo { get; set; }

        [DataMember]
        [DataDesc(CName = "操作时间", ShortCode = "OperTime", Desc = "")]
        public string OperTime { get; set; }
    }
}
