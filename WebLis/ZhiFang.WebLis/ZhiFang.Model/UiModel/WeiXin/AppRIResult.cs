using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Model.UiModel.WeiXin
{
    public class AppRIResult
    {
        /// <summary>
        /// 检验项目
        /// </summary>
         public string ItemsName{get;set;}//:"检验项目",
        /// <summary>
         /// 结果
        /// </summary>
		 public string Result{get;set;}//:"结果",
         /// <summary>
         /// 结果
         /// </summary>
         public string ResultDesc { get; set; }//:"结果",
        /// <summary>
         /// 单位
        /// </summary>
		 public string Unit{get;set;}//:"单位",
        /// <summary>
         /// 参考值
        /// </summary>
		 public string ReferenceValue{get;set;}//:"参考值",
        /// <summary>
         /// 项目ID，用于以后查看项目临床意义
        /// </summary>
		 public string ItemId{get;set;}//:"项目ID，用于以后查看项目临床意义"
         /// <summary>
         /// 结果高低值（RESULTSTATUS）
         /// </summary>
         public string ResultStatus { get; set; }//:"结果高低值（RESULTSTATUS）"
         /// <summary>
         /// 简码（ShortCode）
         /// </summary>
         public string ShortCode { get; set; }

    }
}
