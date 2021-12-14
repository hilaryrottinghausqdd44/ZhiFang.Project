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
    [DataDesc(CName = "质控数据统计返回对象", ClassCName = "QCDataStatistics", ShortCode = "QCDataStatistics", Desc = "质控数据统计返回对象")]
    public class QCDataStatistics //: QCDStat
    {
        public QCDataStatistics(long equipID, string equipName, string equipSName, int equipDispOrder,
                                long qcMatID, string qcMatName, string qcMatSName, string manu, string concLevel, string qcMatLotNo, int qcMatDispOrder,
                                long qcItemID, string qcItemName, string qcItemSName, int qcItemDispOrder, int qcItemPrecision, byte[] qcItemDataTimeStamp, double? ccv,
                                DateTime? qcItemTimeBeginDate, DateTime? qcItemTimeEndDate,
                                double? target, double? sd, double? cv,
                                double? calcTarget, double? calcSD,
                                bool qcdValueIsUse, int isControl, string reportValue, DateTime? receiveTime, int qcDataLotNo,
                                long empID, string empName, byte[] empDataTimeStamp
        )
        {
            //仪器相关
            this.EquipID = equipID;
            this.EquipName = equipName;
            this.EquipSName = equipSName;
            this.EquipDispOrder = equipDispOrder;
            //质控物相关
            this.QCMatID = qcMatID;
            this.QCMatName = qcMatName;
            this.QCMatSName = qcMatSName;
            this.Manu = manu;
            this.ConcLevel = concLevel;
            this.QCMatLotNo = qcMatLotNo;
            this.QCMatDispOrder = qcMatDispOrder;
            //质控项目相关
            this.QCItemID = qcItemID;
            this.QCItemName = qcItemName;
            this.QCItemSName = qcItemSName;
            this.QCItemDispOrder = qcItemDispOrder;
            this.QCItemPrecision = qcItemPrecision;
            this.QCItemDataTimeStamp = qcItemDataTimeStamp;
            this.CCV = ccv == null ? null : ccv.Value.ToString();
            //质控项目时效相关
            this.BeginDate = qcItemTimeBeginDate;
            this.EndDate = qcItemTimeEndDate;
            this.QCTarget = target == null ? null : target.Value.ToString();
            this.QCSD = sd == null ? null : sd.Value.ToString();
            this.QCCV = cv == null ? null : cv.Value.ToString();
            this.TTarget = calcTarget == null ? null : calcTarget.Value.ToString();
            this.TSD = calcSD == null ? null : calcSD.Value.ToString();
            //质控数据相关
            this.QCDValueIsUse = qcdValueIsUse;
            this.IsControl = isControl;
            this.ReportValue = reportValue;
            this.ReceiveTime = receiveTime;
            this.QCDataLotNo = qcDataLotNo;
            //员工相关
            this.EmpID = empID;
            this.EmpName = empName;
            this.EmpDataTimeStamp = empDataTimeStamp;
        }

        public QCDataStatistics(long id, long equipID, string equipName, string equipSName, int equipDispOrder,
                                long qcMatID, string qcMatName, string qcMatSName, string manu, string concLevel, string qcMatLotNo, int qcMatDispOrder,
                                long qcItemID, string qcItemName, string qcItemSName, int qcItemDispOrder, int qcItemPrecision, byte[] qcItemDataTimeStamp, double? ccv,
                                DateTime? qcItemTimeBeginDate, DateTime? qcItemTimeEndDate,
                                double? target, double? sd, double? cv,
                                double? calcTarget, double? calcSD, double? calcCV,
                                int qcCount, int skCount, int warningCount, int noUseCount, int zkCount,double? qcRValue,double? qcMValue,string qcComment,
                                int sd1Count, int sd2Count, int sd3Count,
                                long empID, string empName, byte[] empDataTimeStamp,
                                int yearID, int monthID
        )
        {
            this.Id = id;
            //仪器相关
            this.EquipID = equipID;
            this.EquipName = equipName;
            this.EquipSName = equipSName;
            this.EquipDispOrder = equipDispOrder;
            //质控物相关
            this.QCMatID = qcMatID;
            this.QCMatName = qcMatName;
            this.QCMatSName = qcMatSName;
            this.Manu = manu;
            this.ConcLevel = concLevel;
            this.QCMatLotNo = qcMatLotNo;
            this.QCMatDispOrder = qcMatDispOrder;
            //质控项目相关
            this.QCItemID = qcItemID;
            this.QCItemName = qcItemName;
            this.QCItemSName = qcItemSName;
            this.QCItemDispOrder = qcItemDispOrder;
            this.QCItemPrecision = qcItemPrecision;
            this.QCItemDataTimeStamp = qcItemDataTimeStamp;
            this.CCV = ccv == null ? null : ccv.Value.ToString();
            //质控项目时效相关
            this.BeginDate = qcItemTimeBeginDate;
            this.EndDate = qcItemTimeEndDate;
            this.QCTarget = target == null ? null : target.Value.ToString();
            this.QCSD = sd == null ? null : sd.Value.ToString();
            this.QCCV = cv == null ? null : cv.Value.ToString();
            this.TTarget = calcTarget == null ? null : calcTarget.Value.ToString();
            this.TSD = calcSD == null ? null : calcSD.Value.ToString();
            this.TCV = calcCV == null ? null : calcCV.Value.ToString();
            //统计相关
            this.QCCount = qcCount;
            this.SKCount = skCount;
            this.WarningCount = warningCount;
            this.NoUseCount = noUseCount;
            this.ZKCount = zkCount;
            this.QCRValue = qcRValue == null ? null : qcRValue.Value.ToString();
            this.QCMValue = qcMValue == null ? null : qcMValue.Value.ToString();
            this.QCComment = qcComment;
            this.SD1Count = sd1Count;
            this.SD2Count = sd2Count;
            this.SD3Count = sd3Count;
            this.YearID = yearID;
            this.MonthID = monthID;
            //员工相关
            this.EmpID = empID;
            this.EmpName = empName;
            this.EmpDataTimeStamp = empDataTimeStamp;
        }

        public QCDataStatistics() { }

        //仪器
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "仪器ID", ShortCode = "ZKYIId", Desc = "主键ID", ContextType = SysDic.Number, Length = 8)]
        public virtual long EquipID { get; set; }
        [DataMember]
        [DataDesc(CName = "仪器名称", ShortCode = "ZKYIMC", Desc = "仪器名称", ContextType = SysDic.All, Length = 40)]
        public virtual string EquipName { get; set; }
        [DataMember]
        [DataDesc(CName = "仪器简称", ShortCode = "EquipSName", Desc = "仪器简称", ContextType = SysDic.All, Length = 40)]
        public virtual string EquipSName { get; set; }
        [DataMember]
        [DataDesc(CName = "仪器显示次序", ShortCode = "EquipDispOrder", Desc = "仪器显示次序", ContextType = SysDic.DateTime)]
        public virtual int EquipDispOrder { get; set; }

        //质控物
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "质控物ID", ShortCode = "ZKWId", Desc = "主键ID", ContextType = SysDic.Number, Length = 8)]
        public virtual long QCMatID { get; set; }
        [DataMember]
        [DataDesc(CName = "质控物名称", ShortCode = "ZKWMC", Desc = "质控物名称", ContextType = SysDic.All, Length = 40)]
        public virtual string QCMatName { get; set; }
        [DataMember]
        [DataDesc(CName = "质控物简称", ShortCode = "QCMatSName", Desc = "质控物简称", ContextType = SysDic.All, Length = 40)]
        public virtual string QCMatSName { get; set; }
        [DataMember]
        [DataDesc(CName = "质控物显示次序", ShortCode = "QCMatDispOrder", Desc = "质控物显示次序", ContextType = SysDic.Number, Length = 4)]
        public virtual int QCMatDispOrder { get; set; }
        [DataMember]
        [DataDesc(CName = "质控物厂家", ShortCode = "CJ", Desc = "质控物厂家", ContextType = SysDic.NText, Length = 40)]
        public virtual string Manu { get; set; }
        [DataMember]
        [DataDesc(CName = "浓度水平", ShortCode = "NDSP", Desc = "质控物浓度水平", ContextType = SysDic.NText, Length = 20)]
        public virtual string ConcLevel { get; set; }
        [DataMember]
        [DataDesc(CName = "质控物批号", ShortCode = "QCMatLotNo", Desc = "质控物批号", ContextType = SysDic.NText, Length = 40)]
        public virtual string QCMatLotNo { get; set; }

        //质控项目
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "质控项目ID", ShortCode = "ZKXMId", Desc = "主键ID", ContextType = SysDic.Number, Length = 8)]
        public virtual long QCItemID { get; set; }
        [DataMember]
        [DataDesc(CName = "质控项目名称", ShortCode = "ZKXMMC", Desc = "质控项目名称", ContextType = SysDic.All, Length = 40)]
        public virtual string QCItemName { get; set; }
        [DataMember]
        [DataDesc(CName = "质控项目简称", ShortCode = "ZKXMJC", Desc = "质控项目简称", ContextType = SysDic.All, Length = 40)]
        public virtual string QCItemSName { get; set; }
        [DataMember]
        [DataDesc(CName = "质控项目显示次序", ShortCode = "QCItemDispOrder", Desc = "质控项目显示次序", ContextType = SysDic.DateTime)]
        public virtual int QCItemDispOrder { get; set; }
        [DataMember]
        [DataDesc(CName = "精度", ShortCode = "Precision", Desc = "精度", ContextType = SysDic.All, Length = 4)]
        public virtual int QCItemPrecision { get; set; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "质控项目时间戳", ShortCode = "ZKXMSJC", Desc = "质控项目时间戳", ContextType = SysDic.All, Length = 8)]
        public virtual byte[] QCItemDataTimeStamp { get; set; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "允许CV", ShortCode = "CCV", Desc = "允许CV", ContextType = SysDic.All, Length = 8)]
        public virtual string CCV { get; set; }

        //质控数据相关
        [DataMember]
        [DataDesc(CName = "是否在控", ShortCode = "IsControl", Desc = "是否在控", ContextType = SysDic.Number, Length = 8)]
        public virtual int IsControl { get; set; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "质控时间", ShortCode = "ZKSJ", Desc = "质控时间", ContextType = SysDic.DateTime)]
        public virtual DateTime? ReceiveTime { get; set; }
        [DataMember]
        [DataDesc(CName = "质控数据是否使用", ShortCode = "IsUse", Desc = "质控数据是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool QCDValueIsUse { get; set; }
        [DataMember]
        [DataDesc(CName = "质控结果", ShortCode = "ZKJG", Desc = "质控结果", ContextType = SysDic.All, Length = 40)]
        public virtual string ReportValue { get; set; }
        [DataMember]
        [DataDesc(CName = "质控批次", ShortCode = "ZKPC", Desc = "质控批次", ContextType = SysDic.Number, Length = 4)]
        public virtual int QCDataLotNo { get; set; }

        //质控项目时效
        [DataMember]
        [DataDesc(CName = "计算靶值", ShortCode = "CalcTarget", Desc = "计算靶值", ContextType = SysDic.Number, Length = 8)]
        public virtual string CalcTarget { get; set; }
        [DataMember]
        [DataDesc(CName = "计算标准差", ShortCode = "CalcSD", Desc = "计算标准差", ContextType = SysDic.Number, Length = 8)]
        public virtual string CalcSD { get; set; }
        [DataMember]
        [DataDesc(CName = "计算变异系数", ShortCode = "CalcCV", Desc = "计算变异系数", ContextType = SysDic.Number, Length = 8)]
        public virtual string CalcCV { get; set; }

        //员工相关
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "员工ID", ShortCode = "EmpID", Desc = "员工ID", ContextType = SysDic.Number, Length = 8)]
        public virtual long EmpID { get; set; }
        [DataMember]
        [DataDesc(CName = "员工名称", ShortCode = "EmpName", Desc = "员工名称", ContextType = SysDic.NText)]
        public virtual string EmpName { get; set; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "员工时间戳", ShortCode = "EmpDataTimeStamp", Desc = "员工时间戳", ContextType = SysDic.All)]
        public virtual byte[] EmpDataTimeStamp { get; set; }

        //质控统计对象相关字段(QCDStat)
        [DataMember]
        [DataDesc(CName = "Id", ShortCode = "Id", Desc = "Id", ContextType = SysDic.All, Length = 4)]
        public virtual long Id { get; set; }

        [DataMember]
        [DataDesc(CName = "YearID", ShortCode = "YearID", Desc = "YearID", ContextType = SysDic.All, Length = 4)]
        public virtual int? YearID { get; set; }

        [DataMember]
        [DataDesc(CName = "MonthID", ShortCode = "MonthID", Desc = "MonthID", ContextType = SysDic.All, Length = 4)]
        public virtual int? MonthID { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "开始日期", ShortCode = "BeginDate", Desc = "开始日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? BeginDate { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "截止日期", ShortCode = "EndDate", Desc = "截止日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? EndDate { get; set; }

        [DataMember]
        [DataDesc(CName = "靶值", ShortCode = "QCTarget", Desc = "靶值", ContextType = SysDic.All, Length = 8)]
        public virtual string QCTarget { get; set; }

        [DataMember]
        [DataDesc(CName = "标准差", ShortCode = "QCSD", Desc = "标准差", ContextType = SysDic.All, Length = 8)]
        public virtual string QCSD { get; set; }

        [DataMember]
        [DataDesc(CName = "变异系数", ShortCode = "QCCV", Desc = "变异系数", ContextType = SysDic.All, Length = 8)]
        public virtual string QCCV { get; set; }

        [DataMember]
        [DataDesc(CName = "计算靶值", ShortCode = "TTarget", Desc = "计算靶值", ContextType = SysDic.All, Length = 8)]
        public virtual string TTarget { get; set; }

        [DataMember]
        [DataDesc(CName = "计算标准差", ShortCode = "TSD", Desc = "计算标准差", ContextType = SysDic.All, Length = 8)]
        public virtual string TSD { get; set; }

        [DataMember]
        [DataDesc(CName = "计算变异系数", ShortCode = "TCV", Desc = "计算变异系数", ContextType = SysDic.All, Length = 8)]
        public virtual string TCV { get; set; }

        [DataMember]
        [DataDesc(CName = "质控总数", ShortCode = "QCCount", Desc = "质控总数", ContextType = SysDic.All, Length = 4)]
        public virtual int? QCCount { get; set; }

        [DataMember]
        [DataDesc(CName = "失控数", ShortCode = "SKCount", Desc = "失控数", ContextType = SysDic.All, Length = 4)]
        public virtual int? SKCount { get; set; }

        [DataMember]
        [DataDesc(CName = "警告数", ShortCode = "WarningCount", Desc = "警告数", ContextType = SysDic.All, Length = 4)]
        public virtual int? WarningCount { get; set; }

        [DataMember]
        [DataDesc(CName = "不使用数", ShortCode = "NoUseCount", Desc = "不使用数", ContextType = SysDic.All, Length = 4)]
        public virtual int? NoUseCount { get; set; }

        [DataMember]
        [DataDesc(CName = "在控数", ShortCode = "ZKCount", Desc = "在控数", ContextType = SysDic.All, Length = 4)]
        public virtual int? ZKCount { get; set; }

        [DataMember]
        [DataDesc(CName = "极差", ShortCode = "QCRValue", Desc = "极差", ContextType = SysDic.All, Length = 8)]
        public virtual string QCRValue { get; set; }

        [DataMember]
        [DataDesc(CName = "中位数", ShortCode = "QCMValue", Desc = "中位数", ContextType = SysDic.All, Length = 8)]
        public virtual string QCMValue { get; set; }

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "QCComment", Desc = "备注", ContextType = SysDic.All, Length = 500)]
        public virtual string QCComment { get; set; }

        [DataMember]
        [DataDesc(CName = "SD1倍数量", ShortCode = "SD1Count", Desc = "SD1倍数量", ContextType = SysDic.All, Length = 4)]
        public virtual int? SD1Count { get; set; }

        [DataMember]
        [DataDesc(CName = "SD2倍数量", ShortCode = "SD2Count", Desc = "SD2倍数量", ContextType = SysDic.All, Length = 4)]
        public virtual int? SD2Count { get; set; }

        [DataMember]
        [DataDesc(CName = "SD3倍数量", ShortCode = "SD3Count", Desc = "SD3倍数量", ContextType = SysDic.All, Length = 4)]
        public virtual int? SD3Count { get; set; }
        
        /// <summary>
        /// 归一处理之后的值
        /// </summary>
        public virtual double? UnifyStandard { get; set; }

        /// <summary>
        /// 是否是空数据
        /// </summary>
        [DataMember]
        [DataDesc(CName = "是否是空数据", ShortCode = "isEmptyData", Desc = "是否是空数据", ContextType = SysDic.All, Length = 4)]
        public virtual bool isEmptyData { get; set; }
    }
}
