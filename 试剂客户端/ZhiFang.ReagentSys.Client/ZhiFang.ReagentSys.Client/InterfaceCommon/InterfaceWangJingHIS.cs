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
using ZhiFang.ReagentSys.Client.Service_WangHai_ShiJiTan;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.RBAC;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.ReagentSys.Client;

namespace ZhiFang.ReagentSys.Client
{
    public class InterfaceWangJingHIS
    {
        public string ReaBmsCenOrderDocToXMLWangJingHIS(ReaBmsCenOrderDoc orderDoc, IList<ReaBmsCenOrderDtl> listOrderDtl)
        {
            string result = "";
            string serverPath = HttpContext.Current.Server.MapPath("~/");
            string xmlOrderDocPath = serverPath + "\\BaseTableXML\\Interface\\OrderDocConfig\\WangJingHis\\BmsCenOrderDoc.xml";
            string xmlOrderDtlPath = serverPath + "\\BaseTableXML\\Interface\\OrderDocConfig\\WangJingHis\\BmsCenOrderDtl.xml";
            if (System.IO.File.Exists(xmlOrderDocPath) || System.IO.File.Exists(xmlOrderDtlPath))
            {
                StringBuilder sbXML = new StringBuilder();
                sbXML.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                sbXML.Append("<Request><MainData>");
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
                                sbXML.Append(propertyInfo.GetValue(orderDoc, null).ToString());
                        }
                        catch (System.Exception ex)
                        {
                            ZhiFang.Common.Log.Log.Error("订货单属性【" + kv.Value + "】数据转换错误：" + ex.Message);
                        }
                    }
                    sbXML.Append("</" + kv.Key + ">");
                }
                sbXML.Append("</MainData>");
                sbXML.Append("<DetailList>");
                foreach (ReaBmsCenOrderDtl orderDtl in listOrderDtl)
                {
                    sbXML.Append("<Item>");
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
                                    sbXML.Append(propertyInfo.GetValue(orderDtl, null).ToString());
                            }
                            catch (System.Exception ex)
                            {
                                ZhiFang.Common.Log.Log.Error("订货明细单属性【" + kvp.Value + "】数据转换错误：" + ex.Message);
                            }
                        }
                        sbXML.Append("</" + kvp.Key + ">");
                    }
                    sbXML.Append("</Item>");
                }
                sbXML.Append("</DetailList></Request>");
                result = sbXML.ToString();
            }
            return result;
        }

        public BaseResultData GetInterfaceResult(string strResult)
        {
            BaseResultData brd = new BaseResultData();
            if (string.IsNullOrWhiteSpace(strResult))
            {
                brd.success = false;
                brd.message = "接口返回值为空，提交订货单失败！";
                return brd;
            }
            StringReader StrStream = new StringReader(strResult);
            XDocument xml = XDocument.Load(StrStream);
            XElement xeRoot = xml.Root;//根目录
            XElement xeResultCode = xeRoot.Element("ResultCode");
            XElement xeResultContent = xeRoot.Element("ResultContent");
            brd.success = (xeResultCode.Value == "0");
            brd.code = xeResultCode.Value;
            brd.message = xeResultContent.Value;
            return brd;
        }
    }
}
