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
using ZhiFang.ReagentSys.Client.Service_DongHua_YZRMYY;

namespace ZhiFang.ReagentSys.Client
{
    public class InterfaceYZRMYYDongHua
    {
        public string ReaBmsCenOrderDocToXMLYZRMYYDongHua(ReaBmsCenOrderDoc orderDoc, IList<ReaBmsCenOrderDtl> listOrderDtl, HREmployee emp)
        {
            string result = "";
            string serverPath = HttpContext.Current.Server.MapPath("~/");
            string xmlOrderDocPath = serverPath + "\\BaseTableXML\\Interface\\OrderDocConfig\\YZRMYYDongHua\\BmsCenOrderDoc.xml";
            string xmlOrderDtlPath = serverPath + "\\BaseTableXML\\Interface\\OrderDocConfig\\YZRMYYDongHua\\BmsCenOrderDtl.xml";
            if (System.IO.File.Exists(xmlOrderDocPath) || System.IO.File.Exists(xmlOrderDtlPath))
            {
                StringBuilder sbXML = new StringBuilder();
                //sbXML.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>"); //经确认不需要此信息
                sbXML.Append("<Request><Main>");
                Dictionary<string, string> dicOrderDoc = new Dictionary<string, string>();
                Dictionary<string, string> dicOrderDocDefault = new Dictionary<string, string>();
                Dictionary<string, string> dicOrderDtl = new Dictionary<string, string>();
                Dictionary<string, string> dicOrderDtlDefault = new Dictionary<string, string>();
                InterfaceCommon.GetColumnNameBySaleDocXMLFile(xmlOrderDocPath, dicOrderDoc, dicOrderDocDefault);
                InterfaceCommon.GetColumnNameBySaleDocXMLFile(xmlOrderDtlPath, dicOrderDtl, dicOrderDtlDefault);
                foreach (KeyValuePair<string, string> kv in dicOrderDoc)
                {
                    sbXML.Append("<" + kv.Key + ">");
                    if (dicOrderDocDefault.ContainsKey(kv.Key))
                        sbXML.Append(dicOrderDocDefault[kv.Key]);
                    else
                    {
                        try
                        {
                            System.Reflection.PropertyInfo propertyInfo = orderDoc.GetType().GetProperty(kv.Value);
                            if (propertyInfo != null && propertyInfo.GetValue(orderDoc, null) != null)
                            {
                                if (kv.Value.ToLower() == "deptid")
                                {
                                    IApplicationContext context = ContextRegistry.GetContext();
                                    IBHRDept IBHRDept = (IBHRDept)context.GetObject("BHRDept");
                                    HRDept dept = IBHRDept.Get(long.Parse(propertyInfo.GetValue(orderDoc, null).ToString()));
                                    if (dept != null)
                                    {
                                        if ((!string.IsNullOrEmpty(dept.MatchCode)))
                                            sbXML.Append(dept.MatchCode);
                                        else
                                            sbXML.Append(dept.StandCode);
                                    }
                                }
                                else
                                    sbXML.Append(propertyInfo.GetValue(orderDoc, null).ToString());
                            }
                        }
                        catch (System.Exception ex)
                        {
                            ZhiFang.Common.Log.Log.Error("订货单属性【" + kv.Value + "】数据转换错误：" + ex.Message);
                        }
                    }
                    sbXML.Append("</" + kv.Key + ">");
                }
                if (emp != null)
                {
                    if ((!string.IsNullOrEmpty(emp.MatchCode)))
                        sbXML.Append("<ReqUserCode>" + emp.MatchCode + "</ReqUserCode>");
                    else
                        sbXML.Append("<ReqUserCode>" + emp.StandCode + "</ReqUserCode>");
                }
                else
                    sbXML.Append("<ReqUserCode></ReqUserCode>");
                sbXML.Append("</Main>");
                foreach (ReaBmsCenOrderDtl orderDtl in listOrderDtl)
                {
                    sbXML.Append("<Detail>");
                    foreach (KeyValuePair<string, string> kvp in dicOrderDtl)
                    {
                        sbXML.Append("<" + kvp.Key + ">");
                        if (dicOrderDtlDefault.ContainsKey(kvp.Key))
                            sbXML.Append(dicOrderDtlDefault[kvp.Key]);
                        else
                        {
                            try
                            {
                                System.Reflection.PropertyInfo propertyInfo = orderDtl.GetType().GetProperty(kvp.Value);
                                if (propertyInfo != null && propertyInfo.GetValue(orderDtl, null) != null)
                                {
                                    if (kvp.Value.ToLower() == "reagoodsid")
                                    {
                                        IApplicationContext context = ContextRegistry.GetContext();
                                        IBReaGoods IBReaGoods = (IBReaGoods)context.GetObject("BReaGoods");
                                        ReaGoods reaGoods = IBReaGoods.Get(long.Parse(propertyInfo.GetValue(orderDtl, null).ToString()));
                                        if (reaGoods != null)
                                            sbXML.Append(reaGoods.MatchCode);
                                        else
                                            sbXML.Append(reaGoods.GoodsNo);
                                    }
                                    else
                                        sbXML.Append(propertyInfo.GetValue(orderDtl, null).ToString());
                                }
                            }
                            catch (System.Exception ex)
                            {
                                ZhiFang.Common.Log.Log.Error("订货明细单属性【" + kvp.Value + "】数据转换错误：" + ex.Message);
                            }
                        }
                        sbXML.Append("</" + kvp.Key + ">");
                    }
                    sbXML.Append("</Detail>");
                }
                sbXML.Append("</Request>");
                result = sbXML.ToString();
            }
            return result;
        }

        public string ReaBmsInDocToXMLYZRMYYDongHua(ReaBmsInDoc inDoc, IList<ReaBmsInDtl> inDtlList, HREmployee emp)
        {
            string strXML = "";
            if (inDoc != null && inDtlList != null && inDtlList.Count > 0)
            {
                string serverPath = HttpContext.Current.Server.MapPath("~/");
                string xmlInDoc = serverPath + "\\BaseTableXML\\Interface\\SaleDocConfig\\YZRMYYDongHua\\ReaBmsInDoc.xml";
                string xmlInDtl = serverPath + "\\BaseTableXML\\Interface\\SaleDocConfig\\YZRMYYDongHua\\ReaBmsInDtl.xml";
                if (System.IO.File.Exists(xmlInDoc) || System.IO.File.Exists(xmlInDtl))
                {
                    StringBuilder sbXML = new StringBuilder();
                    //sbXML.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>"); 
                    sbXML.Append("<Request><Main>");
                    Dictionary<string, string> dicInDoc = new Dictionary<string, string>();
                    Dictionary<string, string> dicInDocDefault = new Dictionary<string, string>();
                    Dictionary<string, string> dicInDtl = new Dictionary<string, string>();
                    Dictionary<string, string> dicInDtlDefault = new Dictionary<string, string>();
                    InterfaceCommon.GetColumnNameBySaleDocXMLFile(xmlInDoc, dicInDoc, dicInDocDefault);
                    InterfaceCommon.GetColumnNameBySaleDocXMLFile(xmlInDtl, dicInDtl, dicInDtlDefault);
                    foreach (KeyValuePair<string, string> kv in dicInDoc)
                    {
                        sbXML.Append("<" + kv.Key + ">");
                        if (dicInDocDefault.ContainsKey(kv.Key))
                            sbXML.Append(dicInDocDefault[kv.Key]);
                        else
                        {
                            try
                            {
                                System.Reflection.PropertyInfo propertyInfo = inDoc.GetType().GetProperty(kv.Value);
                                if (propertyInfo != null && propertyInfo.GetValue(inDoc, null) != null)
                                {
                                    if (kv.Value.ToLower() == "deptid")
                                    {
                                        IApplicationContext context = ContextRegistry.GetContext();
                                        IBHRDept IBHRDept = (IBHRDept)context.GetObject("BHRDept");
                                        HRDept dept = IBHRDept.Get(long.Parse(propertyInfo.GetValue(inDoc, null).ToString()));
                                        if (dept != null)
                                        {
                                            if ((!string.IsNullOrEmpty(dept.MatchCode)))
                                                sbXML.Append(dept.MatchCode);
                                            else
                                                sbXML.Append(dept.StandCode);
                                        }
                                    }
                                    else
                                        sbXML.Append(propertyInfo.GetValue(inDoc, null).ToString());
                                }
                            }
                            catch (System.Exception ex)
                            {
                                ZhiFang.Common.Log.Log.Error("入库单属性【" + kv.Value + "】数据转换错误：" + ex.Message);
                            }
                        }
                        sbXML.Append("</" + kv.Key + ">");
                    }
                    if (emp != null)
                    {
                        if ((!string.IsNullOrEmpty(emp.MatchCode)))
                            sbXML.Append("<CreateUserCode>" + emp.MatchCode + "</CreateUserCode>");
                        else
                            sbXML.Append("<CreateUserCode>" + emp.StandCode + "</CreateUserCode>");
                    }
                    else
                        sbXML.Append("<CreateUserCode></CreateUserCode>");
                    sbXML.Append("</Main>");
                    foreach (ReaBmsInDtl inDtl in inDtlList)
                    {
                        sbXML.Append("<Detail>");
                        foreach (KeyValuePair<string, string> kvp in dicInDtl)
                        {
                            sbXML.Append("<" + kvp.Key + ">");
                            if (dicInDtlDefault.ContainsKey(kvp.Key))
                                sbXML.Append(dicInDtlDefault[kvp.Key]);
                            else
                            {
                                try
                                {
                                    System.Reflection.PropertyInfo propertyInfo = inDtl.GetType().GetProperty(kvp.Value);
                                    if (propertyInfo != null && propertyInfo.GetValue(inDtl, null) != null)
                                    {
                                        if (kvp.Value.ToLower() == "reagoodsid")
                                        {
                                            IApplicationContext context = ContextRegistry.GetContext();
                                            IBReaGoods IBReaGoods = (IBReaGoods)context.GetObject("BReaGoods");
                                            ReaGoods reaGoods = IBReaGoods.Get(long.Parse(propertyInfo.GetValue(inDtl, null).ToString()));
                                            if (reaGoods != null)
                                                sbXML.Append(reaGoods.MatchCode);
                                            else
                                                sbXML.Append(reaGoods.GoodsNo);
                                        }
                                        else
                                            sbXML.Append(propertyInfo.GetValue(inDtl, null).ToString());
                                    }
                                }
                                catch (System.Exception ex)
                                {
                                    ZhiFang.Common.Log.Log.Error("入库单明细单属性【" + kvp.Value + "】数据转换错误：" + ex.Message);
                                }
                            }
                            sbXML.Append("</" + kvp.Key + ">");
                        }
                        sbXML.Append("</Detail>");
                    }
                    sbXML.Append("</Request>");
                    strXML = sbXML.ToString();
                }
            }//if
            return strXML;
        }

        public string ReaBmsCenSaleDocConfirmToXMLYZRMYYDongHua(ReaBmsCenSaleDoc saleDoc, HREmployee emp)
        {
            string strXML = "";
            if (saleDoc != null)
            {
                StringBuilder sbXML = new StringBuilder();
                //sbXML.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                sbXML.Append("<Request>");
                sbXML.Append("<InistrfNo>" + saleDoc.OtherDocNo + "</InistrfNo>");
                sbXML.Append("<ReceiveDate>" + DateTime.Now.ToString("yyyy-MM-dd") + "</ReceiveDate>");
                sbXML.Append("<ReceiveTime>" + DateTime.Now.ToString("HH:mm:ss") + "</ReceiveTime>");
                if ((!string.IsNullOrEmpty(emp.MatchCode)))
                    sbXML.Append("<ReceiveUserCode>" + emp.MatchCode + "</ReceiveUserCode>");
                else
                    sbXML.Append("<ReceiveUserCode>" + emp.StandCode + "</ReceiveUserCode>");
                sbXML.Append("<ReceiveUserName>" + emp.CName + "</ReceiveUserName>");
                sbXML.Append("</Request>");
                strXML = sbXML.ToString();
            }
            return strXML;
        }

        public BaseResultData ReaBmsCenOrderDocPost(string xmlOrderDoc, ref string resultOrderDocID, ref string resultOrderDocNo)
        {
            BaseResultData brd = new BaseResultData();  
            PUB0015Soap pub0015Soap = new Service_DongHua_YZRMYY.PUB0015SoapClient();
            HIPServiceRequest hipRequest = new HIPServiceRequest();
            hipRequest.Body = new HIPServiceRequestBody();
            hipRequest.Body.input1 = "MES009";
            hipRequest.Body.input2 = xmlOrderDoc;
            ZhiFang.Common.Log.Log.Info("提交订货单接口服务参数值：input1---" + hipRequest.Body.input1 + ", input2-- - " + hipRequest.Body.input2);
            ZhiFang.Common.Log.Log.Info("开始调用提交订货单接口！");
            HIPServiceResponse hipResponse = pub0015Soap.HIPService(hipRequest);
            string serviceResult = hipResponse.Body.HIPServiceResult;
            ZhiFang.Common.Log.Log.Info("调用提交订货单接口返回值：" + serviceResult);
            ZhiFang.Common.Log.Log.Info("结束调用提交订货单接口！");
            if (!string.IsNullOrEmpty(serviceResult))
            {
                StringReader StrStream = new StringReader(serviceResult);
                XDocument xml = XDocument.Load(StrStream);
                XElement xeRoot = xml.Root;//根目录
                XElement xeReturnCode = xeRoot.Element("ReturnCode");
                XElement xeReturnDesc = xeRoot.Element("ReturnDesc");
                XElement xeResult = xeRoot.Element("Result");
                if (xeReturnCode != null && xeReturnCode.Value == "0")
                {
                    resultOrderDocID = xeResult.Element("ReqId").Value;
                    resultOrderDocNo = xeResult.Element("ReqNo").Value;
                }
                else
                {
                    brd.success = false;
                    brd.message = "接口返回值" + xeReturnCode.Value + "，提交订货单失败！";
                    ZhiFang.Common.Log.Log.Info("接口返回值" + xeReturnCode.Value + "，提交订货单失败：" + xeReturnDesc.Value);
                }               
            }
            else
            {
                brd.success = false;
                brd.message = "接口返回值为空，提交订货单失败！";
                ZhiFang.Common.Log.Log.Info("提交订货单失败，接口服务HIPService返回值为空！参数值：input1---" + hipRequest.Body.input1 + ", input2-- - " + hipRequest.Body.input2);
            }
            return brd;
        }

        public BaseResultData ReaGoodsCancellingStocks(ReaBmsInDoc inDoc, IList<ReaBmsInDtl> inDtlList, HREmployee emp)
        {
            BaseResultData brd = new BaseResultData();
            try
            {
                if (inDoc == null || inDtlList == null || inDtlList.Count == 0)
                {
                    brd.success = false;
                    brd.message = "出库确认后调用退供应商接口,转换之后的入库主单与明细信息为空,不调用退库接口!";
                    ZhiFang.Common.Log.Log.Info(brd.message);
                    return brd;
                }
                ZhiFang.Common.Log.Log.Info("开始调用退供应商接口！");
                string xmlPara = "";
                try
                {
                    xmlPara = ReaBmsInDocToXMLYZRMYYDongHua(inDoc, inDtlList, emp);
                }
                catch (System.Exception ex)
                {
                    brd.success = false;
                    brd.message = "物资退供应商接口HIPService参数转换失败：" + ex.Message;
                    ZhiFang.Common.Log.Log.Info(brd.message);
                    return brd;
                }
                PUB0015Soap pub0015Soap = new Service_DongHua_YZRMYY.PUB0015SoapClient();
                HIPServiceRequest hipRequest = new HIPServiceRequest();
                hipRequest.Body = new HIPServiceRequestBody();
                hipRequest.Body.input1 = "MES012";
                hipRequest.Body.input2 = xmlPara;
                ZhiFang.Common.Log.Log.Info("退供应商接口服务参数值：input1---" + hipRequest.Body.input1 + ", input2-- - " + hipRequest.Body.input2);
                HIPServiceResponse hipResponse = pub0015Soap.HIPService(hipRequest);
                string serviceResult = hipResponse.Body.HIPServiceResult;
                ZhiFang.Common.Log.Log.Info("退供应商接口服务返回值：" + serviceResult);
                if (!string.IsNullOrEmpty(serviceResult))
                {
                    StringReader StrStream = new StringReader(serviceResult);
                    XDocument xml = XDocument.Load(StrStream);
                    XElement xeRoot = xml.Root;//根目录
                    XElement xeReturnCode = xeRoot.Element("ReturnCode");
                    XElement xeReturnDesc = xeRoot.Element("ReturnDesc");
                    XElement xeResult = xeRoot.Element("Result");
                    if (xeReturnCode != null && xeReturnCode.Value == "0")
                    {
                        brd.success = false;
                        brd.message = "退供应商接口服务返回值" + xeReturnCode.Value + "，提交订货单失败！";
                        ZhiFang.Common.Log.Log.Info("退供应商接口服务返回值" + xeReturnCode.Value + "，提交订货单失败：" + xeReturnDesc.Value);
                    }
                }
                else
                {
                    brd.success = false;
                    brd.message = "接口返回值为空，退供应商接口服务调用失败！";
                    ZhiFang.Common.Log.Log.Info("退供应商接口服务调用失败，接口服务HIPService返回值为空！参数值：input1---" + hipRequest.Body.input1 + ", input2-- - " + hipRequest.Body.input2);
                }
                ZhiFang.Common.Log.Log.Info("结束调用退供应商接口！");
            }
            catch (System.Exception ex)
            {
                brd.success = false;
                brd.message = "物资退库同步接口drawsBackStorage调用失败：" + ex.Message;
                ZhiFang.Common.Log.Log.Info(brd.message);
            }
            return brd;
        }

        public BaseResultData ReaGoodsCancellingStocks(Dictionary<long, KeyValuePair<ReaBmsInDoc, IList<ReaBmsInDtl>>> dic, HREmployee emp)
        {
            BaseResultData brd = new BaseResultData();
            foreach (KeyValuePair<long, KeyValuePair<ReaBmsInDoc, IList<ReaBmsInDtl>>> kv in dic)
            {
                ReaBmsInDoc inDoc = kv.Value.Key;
                IList<ReaBmsInDtl> inDtlList = kv.Value.Value;
                brd = ReaGoodsCancellingStocks(inDoc, inDtlList, emp);
            }
            return brd;
        }

        public BaseResultData ReaBmsCenSaleDocConfirm(ReaBmsCenSaleDoc saleDoc, HREmployee emp)
        {
            BaseResultData brd = new BaseResultData();
            ZhiFang.Common.Log.Log.Info("开始调用入库单接收确认接口！");
            string strXML = ReaBmsCenSaleDocConfirmToXMLYZRMYYDongHua(saleDoc, emp);
            PUB0015Soap pub0015Soap = new Service_DongHua_YZRMYY.PUB0015SoapClient();
            HIPServiceRequest hipRequest = new HIPServiceRequest();
            hipRequest.Body = new HIPServiceRequestBody();
            hipRequest.Body.input1 = "MES013";
            hipRequest.Body.input2 = strXML;
            ZhiFang.Common.Log.Log.Info("入库单接收确认接口服务参数值：input1---" + hipRequest.Body.input1 + ", input2-- - " + hipRequest.Body.input2);
            HIPServiceResponse hipResponse = pub0015Soap.HIPService(hipRequest);
            string serviceResult = hipResponse.Body.HIPServiceResult;
            ZhiFang.Common.Log.Log.Info("入库单接收确认接口服务返回值：" + serviceResult);
            if (!string.IsNullOrEmpty(serviceResult))
            {
                StringReader StrStream = new StringReader(serviceResult);
                XDocument xml = XDocument.Load(StrStream);
                XElement xeRoot = xml.Root;//根目录
                XElement xeReturnCode = xeRoot.Element("ReturnCode");
                XElement xeReturnDesc = xeRoot.Element("ReturnDesc");
                XElement xeResult = xeRoot.Element("Result");
                if (xeReturnCode != null && xeReturnCode.Value == "0")
                {
                    ZhiFang.Common.Log.Log.Info("接口返回值" + xeReturnCode.Value + "，入库单接收确认成功：" + saleDoc.SaleDocNo + "---" + saleDoc.OtherDocNo);
                }
                else
                {
                    brd.success = false;
                    brd.message = "接口返回值" + xeReturnCode.Value + "，入库单接收确认失败！";
                    ZhiFang.Common.Log.Log.Info("接口返回值" + xeReturnCode.Value + "，入库单接收确认失败：" + xeReturnDesc.Value);
                }

            }
            else
            {
                brd.success = false;
                brd.message = "接口返回值为空，入库单接收确认失败！";
                ZhiFang.Common.Log.Log.Info("入库单接收确认失败，接口服务HIPService返回值为空！参数值：input1---" + hipRequest.Body.input1 + ", input2-- - " + hipRequest.Body.input2);
            }
            ZhiFang.Common.Log.Log.Info("结束调用入库单接收确认接口！");
            return brd;

        }
    }
}