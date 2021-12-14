using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client.ViewObject.Statistics
{
    //消耗比对分析的是将试剂实际使用量与仪器检测对用的试剂理论消耗量进行比对，同时可以详细知道试剂出库后具体消耗在什么项目、什么检测类型上
    /// <summary>
    /// 试剂理论消耗量/消耗比对分析统计VO
    /// </summary>
    public class ConsumptionComparisonAnalysisVO
    {
        public ConsumptionComparisonAnalysisVO() { }

        [DataMember]
        public virtual string StartDate { get; set; }
        [DataMember]
        public virtual string EndDate { get; set; }
        public virtual string TestEquipID { get; set; }
        /// <summary>
        /// Lis仪器编码
        /// </summary>
        public virtual string EquipCode { get; set; }
        /// <summary>
        /// 仪器名称
        /// </summary>
        public virtual string EquipCName { get; set; }
        public virtual string GoodsId { get; set; }
        public virtual string ReaGoodsNo { get; set; }
        public virtual string GoodsCName { get; set; }
        public virtual string GoodsSName { get; set; }
        public virtual string GoodsUnit { get; set; }
        public virtual string UnitMemo { get; set; }
        /// <summary>
        /// 试剂单价(元)
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double Price { get; set; }
        /// <summary>
        /// 实际使用量：实际使用量/实际消耗量（试剂的仪器使用量）
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double TestEquipOutCount { get; set; }
        /// <summary>
        /// 试剂成本（实际使用金额）
        /// 试剂成本=实际使用量*试剂单价
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double TestEquipOutAmount { get; set; }
        /// <summary>
        /// 消耗比：同一仪器相同试剂的消耗比
        /// 消耗比=实际使用量/该试剂的理论消耗量之和*100%
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double ConsumptionPercentSum { get; set; }

        /// <summary>
        /// 消耗占比：同一仪器相同试剂的不同项目的消耗占比
        /// 消耗占比=实际测试数/单位包装检测量/(同仪器同试剂不同项目的)理论消耗量总和
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double ConsumptionPercent { get; set; }
        public virtual string TestItemID { get; set; }
        /// <summary>
        /// Lis项目编码
        /// </summary>
        public virtual string LisTestItemCode { get; set; }
        /// <summary>
        /// 项目简称
        /// </summary>
        public virtual string LisTestItemSName { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public virtual string LisTestItemCName { get; set; }
        /// <summary>
        /// LIS检测类型
        /// </summary>
        public virtual string TestType { get; set; }
        /// <summary>
        /// LIS检测类型名称
        /// </summary>
        public virtual string TestTypeName { get; set; }
        /// <summary>
        /// 实际测试数：同仪器同试剂的不同项目不同检测类型的实际测试数
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double DetectionQuantity { get; set; }
        /// <summary>
        /// 实际测试总数：同仪器同试剂的不同项目的实际测试总数，不区分检测类型
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double DetectionQuantitySum { get; set; }
        /// <summary>
        /// 单位包装检测量
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double UnitTestCount { get; set; }
        /// <summary>
        /// 理论消耗量：同仪器同试剂的不同项目不同检测类型的理论消耗量
        /// 理论消耗量=实际测试数/单位包装检测量
        /// </summary>
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double TheoreticalConsumption { get; set; }
        /// <summary>
        /// 理论消耗总量：同一仪器同一试剂的不同项目的理论消耗量总和，不区分检测类型
        /// </summary>
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double TheoreticalConsumptionSum { get; set; }
        /// <summary>
        /// 理论消耗金额：同仪器同试剂的不同项目不同检测类型的理论消耗金额
        /// 理论消耗金额=试剂单价*实际测试数/单位包装检测量
        /// </summary>
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double TheoreticalConsumptionAmount { get; set; }

        /// <summary>
        /// 成本占比:
        /// 成本比=成本/收入*100%，即试剂成本比=试剂成本/项目总收入*100%，也就是成本占收入的比
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double CostRatio { get; set; }
        /// <summary>
        /// 成本利润率
        /// 试剂成本利润率=（项目总收入-试剂成本）/试剂成本*100%
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double CostMargin { get; set; }
        /// <summary>
        /// (项目)毛利率
        /// (项目)毛利率= 毛利/收入*100%=（总收入-成本）/收入*100%，
        /// 即检验毛利=（项目总收入-试剂成本）/项目总收入
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double GrossProfitMargin { get; set; }
        /// <summary>
        /// 额外消耗
        /// 额外消耗=实际使用量*单位包装检测量-实际检测数
        /// 额外消耗=实际消耗量-理论消耗总量（同仪器相同试剂不同项目的理论消耗量之和）
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double ExtraConsumption { get; set; }
        /// <summary>
        /// 额外消耗比
        /// 额外消耗比=(实际消耗量-理论消耗量 )/理论消耗量*100%
        /// 也就是额外消耗比=消耗比-1
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double ExtraConsumptionRatio { get; set; }
        /// <summary>
        /// 检验项目单价（项目收费单价）:取试剂系统的检验项目的单价
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double TestItemPrice { get; set; }
        /// <summary>
        /// 项目收入(检验收入）：同一仪器相同试剂的不同项目,只对常规的检测进行计算；
        /// 检验收入= 实际检测数*项目收费单价
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double TestItemIncome { get; set; }
        /// <summary>
        /// 项目总收入（检验总收入）：同一仪器相同试剂的不同项目的检验收入之和,只对常规的检测进行计算；
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double TestItemIncomeSum { get; set; }

    }
}
