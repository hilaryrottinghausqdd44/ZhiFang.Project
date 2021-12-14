using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Reflection;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Spring.Context;
using Spring.Context.Support;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.RBAC;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.Common.Log;
using ReferenceServiceDemo.HRPService;

namespace ZhiFang.ReagentSys.Client
{
    /// <summary>
    /// 赣南医学附属第一医院
    /// 上海京颐HRP接口
    /// </summary>
    public class InterfaceGanNanFSDYYY
    {
        /// <summary>
        /// 调用平台服务发送
        /// </summary>
        /// <param name="callType">类型，暂时无用，因为xml里有此节点区分了</param>
        /// <param name="xmlMessage">发送的消息</param>
        /// <returns>-1失败，0成功</returns>
        public BaseResultData CallService(string callType, string xmlMessage)
        {
            BaseResultData baseResultData = new BaseResultData();
            try
            {
                Log.Info("请求服务开始，发送消息：" + xmlMessage);
                FeiyiServiceService ws = new FeiyiServiceService();
                Log.Info("URL地址：" + ws.Url);
                string responseStr = ws.UniversalInterface(xmlMessage);
                Log.Info("服务返回消息：" + responseStr);
                if (responseStr.Trim() != "")
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(responseStr);

                    string res_code = doc.SelectSingleNode("//res_code").InnerText;//0000成功;0001失败
                    string error_msg = doc.SelectSingleNode("//error_msg").InnerText;//失败原因
                    if (res_code.Trim() != "0000")
                    {
                        baseResultData.success = false;
                        baseResultData.message = "平台返回失败消息,错误信息：" + error_msg;
                        Log.Error(baseResultData.message);
                    }
                }
                else
                {
                    baseResultData.success = false;
                    baseResultData.message = "平台返回信息为空！";
                    Log.Error(baseResultData.message);
                }
            }
            catch (System.Exception ex)
            {
                baseResultData.success = false;
                baseResultData.message = "CallService请求服务异常！" + ex.Message;
                Log.Error("CallService请求服务异常->", ex);
            }
            return baseResultData;
        }

        /// <summary>
        /// 同步物资系统采购计划信息
        /// LIS系统指定采购计划后，将采购计划数据同步至HRP系统。（LIS订单上传）
        /// </summary>
        /// <param name="orderDoc"></param>
        /// <param name="listOrderDtl"></param>
        /// <param name="emp"></param>
        /// <returns></returns>
        public string GetDoSynMtrlPurchasePlanInfo(ReaBmsCenOrderDoc orderDoc, IList<ReaBmsCenOrderDtl> listOrderDtl, HRDept dept)
        {
            StringBuilder sbXml = new StringBuilder();
            sbXml.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sbXml.Append("<request>");
            sbXml.Append("<callType>DoSynMtrlPurchasePlanInfo</callType>");
            sbXml.Append("<xmlMessage>");
            sbXml.Append("<result_data>");     

            //主单
            sbXml.Append("<master>");
            sbXml.Append("<storage_id>" + dept.StandCode + "</storage_id>");//库房
            sbXml.Append("<apply_dept>" + dept.MatchCode + "</apply_dept>");//申请科室代码
            sbXml.Append("<proposer>" + orderDoc.Checker + "</proposer>");//申请人
            sbXml.Append("<planing_date>" + (orderDoc.CheckTime != null ? orderDoc.CheckTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "") + "</planing_date>");//申请日期
            sbXml.Append("<three_master_id>" + orderDoc.Id + "</three_master_id>");//lis主单唯一标识
            sbXml.Append("</master>");

            //明细
            sbXml.Append("<detail>");
            sbXml.Append("<record_list>");
            foreach (var dtl in listOrderDtl)
            {
                sbXml.Append("<record_info>");
                sbXml.Append("<mtrl_code>" + ConvertToMatchCode.ReaGoodsConvertToMatchCode(dtl.ReaGoodsID.Value) + "</mtrl_code>");
                sbXml.Append("<mtrl_name><![CDATA[" + dtl.ReaGoodsName + "]]></mtrl_name>");//物资名称
                sbXml.Append("<mtrl_spec><![CDATA[" + dtl.UnitMemo + "]]></mtrl_spec>");//规格
                sbXml.Append("<mtrl_model></mtrl_model>");//型号
                sbXml.Append("<units><![CDATA[" + dtl.GoodsUnit + "]]></units>");//单位
                sbXml.Append("<firm_id>" + ConvertToMatchCode.ProdOrgToMatchCode(dtl.ProdOrgName) + "</firm_id>");//生产商ID（对照码）
                sbXml.Append("<supplier_id>" + ConvertToMatchCode.ReaCenOrgConvertToMatchCode(orderDoc.ReaCompID.Value) + "</supplier_id>");//供应商ID（对照码）
                sbXml.Append("<real_quantity>" + dtl.ReqGoodsQty + "</real_quantity>");//申请数量
                sbXml.Append("<purchase_price>" + dtl.Price + "</purchase_price>");//采购价（单价）
                sbXml.Append("<three_detail_id>" + dtl.Id + "</three_detail_id>");//Lis明细唯一标识
                sbXml.Append("<arrival_date>" + (dtl.ArrivalTime != null ? dtl.ArrivalTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "") + "</arrival_date>");//要求到货日期
                sbXml.Append("</record_info>");
            }            
            sbXml.Append("</record_list>");
            sbXml.Append("</detail>");

            sbXml.Append("</result_data>");
            sbXml.Append("</xmlMessage>");
            sbXml.Append("</request>");

            return sbXml.ToString();
        }

        /// <summary>
        /// 同步HRP试剂状态
        /// LIS出库时调用
        /// </summary>
        /// <returns></returns>
        public string GetDoUpdateStatus(ReaBmsOutDoc outDoc, IList<ReaBmsOutDtl> outDtlList, HRDept dept)
        {
            StringBuilder sbXml = new StringBuilder();
            sbXml.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sbXml.Append("<request>");
            sbXml.Append("<callType>DoUpdateStatus</callType>");
            sbXml.Append("<xmlMessage>");
            sbXml.Append("<result_data>");
            sbXml.Append("<record_list>");
            foreach (var dtl in outDtlList)
            {
                sbXml.Append("<record_info>");
                sbXml.Append("<serial_no><![CDATA[" + dtl.LotQRCode + "]]></serial_no>");//序列号(条码号)：和库存子单表中的二维条码字段LotQRCode的值一致。
                sbXml.Append("<machine_code><![CDATA[" + dtl.TestEquipName + "]]></machine_code>");//机器码：仪器名称
                sbXml.Append("<use_indicator>2</use_indicator>");//状态：1已接收/2已使用
                sbXml.Append("<performed_by>" + dept.MatchCode + "</performed_by>");//使用/接收科室
                sbXml.Append("<opretor>" + outDoc.ConfirmName + "</opretor>");//操作人
                sbXml.Append("<opretor_time>" + (outDoc.ConfirmTime != null ? outDoc.ConfirmTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "") + "</opretor_time>");//操作时间
                sbXml.Append("</record_info>");
            }
            sbXml.Append("</record_list>");
            sbXml.Append("</result_data>");
            sbXml.Append("</xmlMessage>");
            sbXml.Append("</request>");

            return sbXml.ToString();
        }
    }
}