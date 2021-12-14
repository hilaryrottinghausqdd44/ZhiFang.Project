using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Tools;
using System.Runtime.Serialization;
namespace ZhiFang.Model
{
    [Serializable]
    public class ReportFormFull_ReportItem_WeiJiZhi:ReportFormFull
    {
		/// <summary>
		/// 项目名称
		/// </summary>
		public string TESTITEMNAME { get; set; }
		/// <summary>
		/// 项目缩写
		/// </summary>
		public string TESTITEMSNAME { get; set; }
		
		/// <summary>
		/// 申请项目编号
		/// </summary>
		public string PARITEMNO { get; set; }
		/// <summary>
		/// 项目编号
		/// </summary>
		public string ITEMNO { get; set; }
		/// <summary>
		/// 原始结果
		/// </summary>
		public string ORIGINALVALUE { get; set; }
		/// <summary>
		/// 报告结果
		/// </summary>
		public string REPORTVALUE { get; set; }
		/// <summary>
		/// 描述
		/// </summary>
		public string ORIGINALDESC { get; set; }
		/// <summary>
		/// 报告描述
		/// </summary>
		public string REPORTDESC { get; set; }
		/// <summary>
		/// 状态
		/// </summary>
		public string STATUSNO { get; set; }
		/// <summary>
		/// 仪器号
		/// </summary>
		public string EQUIPNO { get; set; }
		/// <summary>
		/// 是否修改
		/// </summary>
		public string MODIFIED { get; set; }
		/// <summary>
		/// 参考值范围
		/// </summary>
		public string REFRANGE { get; set; }
		/// <summary>
		/// 测定日期
		/// </summary>
		public DateTime? ITEMDATE { get; set; }
		/// <summary>
		/// 测定时间
		/// </summary>
		public DateTime? ITEMTIME { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string ISMATCH { get; set; }
		/// <summary>
		/// 结果描述
		/// </summary>
		public string RESULTSTATUS { get; set; }
		/// <summary>
		/// 检测日期
		/// </summary>
		public DateTime? TESTITEMDATETIME { get; set; }
		/// <summary>
		/// 结果
		/// </summary>
		public string REPORTVALUEALL { get; set; }
		/// <summary>
		/// 申请项目
		/// </summary>
		public string PARITEMNAME { get; set; }
		/// <summary>
		/// 申请项目缩写
		/// </summary>
		public string PARITEMSNAME { get; set; }
		/// <summary>
		/// 排序
		/// </summary>
		public string DISPORDER { get; set; }
		/// <summary>
		/// 项目序号
		/// </summary>
		public string ITEMORDER { get; set; }
		/// <summary>
		/// 单位
		/// </summary>
		public string UNIT { get; set; }
		/// <summary>
		/// 自定义1
		/// </summary>
		public string ZDY1 { get; set; }
		/// <summary>
		/// 自定义2
		/// </summary>
		public string ZDY2 { get; set; }
		/// <summary>
		/// 自定义3
		/// </summary>
		public string ZDY3 { get; set; }
		/// <summary>
		/// 自定义4
		/// </summary>
		public string ZDY4 { get; set; }
		/// <summary>
		/// 自定义5
		/// </summary>
		public string ZDY5 { get; set; }
		/// <summary>
		/// HIS项目编码
		/// </summary>
		public string HISORDERNO { get; set; }
		public string ItemRESULTSTATUS { get; set; }
	}
}