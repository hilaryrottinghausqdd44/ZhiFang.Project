using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client.ViewObject.Response
{
    public class StatisticalAnalysisBmsInDocVO
    {
        /// <summary>
        /// 总数量
        /// </summary>
        public double Count { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// 总价格
        /// </summary>
        public double SumTotal { get; set; }

        /// <summary>
        /// 客户端货品ID
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public long ReaGoodsId { get; set; }

        /// <summary>
        /// 客户端货品名称
        /// </summary>
        public string ReaGoodsName { get; set; }

        /// <summary>
        /// 平台货品编码
        /// </summary>
        public string GoodsNo { get; set; }

        /// <summary>
        /// 平台货品名称（预留）
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// 货品单位ID
        /// </summary>
        public long GoodsUnitID { get; set; }

        /// <summary>
        /// 货品单位名称
        /// </summary>
        public string GoodsUnit { get; set; }

        /// <summary>
        /// 供应商平台机构编码
        /// </summary>
        public string ReaServerCompCode { get; set; }

        /// <summary>
        /// 供应商平台机构名称
        /// </summary>
        public string ReaServerCompName { get; set; }

        /// <summary>
        /// 日期时间类型
        /// </summary>
        public string DateTimeType { get; set; }

        /// <summary>
        /// 日期时间
        /// </summary>
        public string DateTime { get; set; }

        /// <summary>
        /// 实验室机构层级
        /// </summary>
        public string CenOrgLevel { get; set; }

        /// <summary>
        /// 实验室机构名称
        /// </summary>
        public string CenOrgName { get; set; }

        /// <summary>
        /// 实验室机构ID（平台机构表）
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public long CenOrgId { get; set; }

        /// <summary>
        /// 实验室机构编号（平台机构表）
        /// </summary>
        public string CenOrgNo { get; set; }

    }
}
