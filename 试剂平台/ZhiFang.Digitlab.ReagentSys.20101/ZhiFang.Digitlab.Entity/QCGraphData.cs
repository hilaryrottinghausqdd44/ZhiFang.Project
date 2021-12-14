using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using ZhiFang.Common.Public;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ZhiFang.Digitlab.Entity
{
    [DataContract]
    [DataDesc(CName = "质控图数据", ClassCName = "QCGraphData", ShortCode = "QCGraphData", Desc = "自定义质控图数据")]
    public class QCGraphData : BaseEntity
    { 
        [DataMember]
        [DataDesc(CName = "质控图数据列表", ShortCode = "QCGraphCustomDataList", Desc = "质控图数据列表")]
        public virtual IList<QCGraphCustomData> QCGraphCustomDataList { get; set; }

        [DataMember]
        [DataDesc(CName = "项目时效数据列表", ShortCode = "QCGraphItemTimeDataList", Desc = "项目时效数据列表")]
        public virtual IList<QCGraphItemTimeData> QCGraphItemTimeDataList { get; set; }

        [DataMember]
        [DataDesc(CName = "线的列表(质控物名称)", ShortCode = "LineNameList", Desc = "线的列表(质控物名称)")]
        public virtual IList<string> LineNameList { get; set; }

        [DataMember]
        [DataDesc(CName = "总批次列表", ShortCode = "TotalBatchList", Desc = "总批次列表")]
        public virtual IList<string> TotalBatchList { get; set; }

        [DataMember]
        [DataDesc(CName = "日期列表", ShortCode = "DateList", Desc = "日期列表")]
        public virtual IList<string> DateList { get; set; }
    }

    [DataContract]
    [DataDesc(CName = "质控图数据", ClassCName = "QCGraphCustomData", ShortCode = "QCGraphCustomData", Desc = "自定义质控图数据")]
    public class QCGraphCustomData : BaseEntity
    {
        public QCGraphCustomData() { }

        [DataMember]
        [DataDesc(CName = "X轴数据(批次)", ShortCode = "XDateData", Desc = "X轴数据(批次)", ContextType = SysDic.All, Length = 40)]
        public virtual string XData { get; set; }
        
        [DataMember]
        [DataDesc(CName = "X轴数据(日期)", ShortCode = "XDateData", Desc = "X轴数据(日期)", ContextType = SysDic.All, Length = 40)]
        public virtual string XDateData { get; set; }

        [DataMember]
        [DataDesc(CName = "Y轴数据(归一后的质控值)", ShortCode = "YData", Desc = "Y轴数据(归一后的质控值)", ContextType = SysDic.All, Length = 40)]
        public virtual string YData { get; set; }

        [DataMember]
        [DataDesc(CName = "线名称(质控物名称)", ShortCode = "LineName", Desc = "线名称(质控物名称)", ContextType = SysDic.All, Length = 40)]
        public virtual string LineName { get; set; }

        [DataMember]
        [DataDesc(CName = "线信息(正态分布图使用)", ShortCode = "LineName", Desc = "线名称(质控物名称)", ContextType = SysDic.All, Length = 40)]
        public virtual string LineInfo { get; set; }

        [DataMember]
        [DataDesc(CName = "总批次", ShortCode = "TotalBatch", Desc = "总批次", ContextType = SysDic.All, Length = 40)]
        public virtual string TotalBatch { get; set; }

        [DataMember]
        [DataDesc(CName = "质控数据", ShortCode = "ZKSJ", Desc = "质控数据")]
        public virtual QCDataCustom QCDValue { get; set; } 
    }

    [DataContract]
    [DataDesc(CName = "质控图项目时效数据", ClassCName = "QCGraphItemTimeData", ShortCode = "QCGraphItemTimeData", Desc = "自定义质控图项目时效数据")]
    public class QCGraphItemTimeData : BaseEntity
    {
        [DataMember]
        [DataDesc(CName = "X轴数据", ShortCode = "XDateData", Desc = "X轴数据", ContextType = SysDic.All, Length = 40)]
        public virtual string XData { get; set; }

        [DataMember]
        [DataDesc(CName = "X轴数据(日期)", ShortCode = "XDateData", Desc = "X轴数据(日期)", ContextType = SysDic.All, Length = 40)]
        public virtual string XDateData { get; set; }

        [DataMember]
        [DataDesc(CName = "质控项目时效", ShortCode = "ZKXMSX", Desc = "质控项目时效")]
        public virtual QCItemTime QCItemTime { get; set; } 
    }
}

