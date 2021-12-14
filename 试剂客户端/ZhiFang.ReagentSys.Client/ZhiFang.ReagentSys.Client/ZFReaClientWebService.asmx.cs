using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Data;
using System.Web.Services;
using ZhiFang.Entity.RBAC;
using ZhiFang.Entity.Common;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using Newtonsoft.Json.Linq;
using Spring.Context;
using Spring.Context.Support;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.IBLL.RBAC;
using ZhiFang.IBLL.Base;
using ZhiFang.Common.Public;

namespace ZhiFang.ReagentSys.Client
{
    /// <summary>
    /// ZFReaClientWebService 的摘要说明
    /// 试剂客户端对外接口服务
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class ZFReaClientWebService : System.Web.Services.WebService
    {
        [WebMethod]
        BaseResultData RS_ReaOrderDocConfirm(string orderDocNo)
        {
            ZhiFang.Common.Log.Log.Info("接口服务名称: RS_ReaOrderDocConfirm");
            BaseResultData baseResultData = new BaseResultData();
            try
            {

            }
            catch (Exception ex)
            {
                baseResultData.success = false;
                baseResultData.message = "数据同步错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Info(baseResultData.message);
            }
            return baseResultData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        public BaseResultData RS_ReaSaleDocCreate(string data)
        {
            ZhiFang.Common.Log.Log.Info("接口服务名称: RS_ReaSaleDocCreate");
            return ReaSaleDocCreate(data, false);
        }

        [WebMethod(EnableSession = true)]
        public string RS_ReaSaleDocCreate_Ex(string data)
        {
            ZhiFang.Common.Log.Log.Info("接口服务名称: RS_ReaSaleDocCreate_Ex");
            BaseResultData brd = ReaSaleDocCreate(data, false);
            return ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(brd);
        }

        [WebMethod(EnableSession = true)]
        public BaseResultData RS_ReaSaleDocCreateConfirm(string data)
        {
            ZhiFang.Common.Log.Log.Info("接口服务名称: RS_ReaSaleDocCreateConfirm");
            return ReaSaleDocCreate(data, true);
        }

        /// <summary>
        /// 入库接口服务：直接写入到入库单和入库明细表里
        /// </summary>
        /// <param name="xmlData">入库单和明细的xml数据</param>
        /// <returns>{"success":true,"code":"","message":""}</returns>
        [WebMethod(EnableSession = true, Description = "入库接口服务")]
        public BaseResultData RS_ReaInDocCreate(string xmlData)
        {
            ZhiFang.Common.Log.Log.Info("入库接口执行开始，方法名称=RS_ReaInDocCreate，入参xmlData=" + xmlData);
            BaseResultData baseResultData = new BaseResultData();

            #region 校验xml格式和验证token
            if (string.IsNullOrWhiteSpace(xmlData))
            {
                baseResultData.success = false;
                baseResultData.message = "接口服务参数xmlData值不能为空！";
                return baseResultData;
            }
            StringReader StrStream = new StringReader(xmlData);
            XDocument xml = XDocument.Load(StrStream);
            baseResultData = BaseDictSyncDataAuthentication(xml);
            if (!baseResultData.success)
            {
                return baseResultData;
            }
            string appkey = "";
            string timestamp = "";
            string token = "";
            string version = "";
            string dictType = "";
            BaseDictSyncDataPara(xml, ref appkey, ref timestamp, ref token, ref version, ref dictType);
            baseResultData = ParaIsNullValidate(appkey, timestamp, xmlData, token, version);
            if (!baseResultData.success)
            {
                return baseResultData;
            }
            baseResultData = UserAuthentication(appkey, timestamp, token);
            if (!baseResultData.success)
            {
                return baseResultData;
            }
            #endregion

            try
            {
                IApplicationContext context = ContextRegistry.GetContext();
                IBReaBmsInDoc IBReaBmsInDoc = (IBReaBmsInDoc)context.GetObject("BReaBmsInDoc");
                IBReaGoodsBarcodeOperation IBReaGoodsBarcodeOperation = (IBReaGoodsBarcodeOperation)context.GetObject("BReaGoodsBarcodeOperation");
                InterfaceDataConvert interfaceDataConvert = new InterfaceDataConvert();

                //将xml解析为入库单和入库明细对象
                HREmployee emp = null;
                ReaBmsInDoc inDoc = null;
                IList<ReaBmsInDtl> inDtlList = null;
                interfaceDataConvert.ConvertReaBmsInDocAndDtlByXml(xmlData, ref inDoc, ref inDtlList, ref emp);

                if (inDoc != null && inDtlList != null)
                {
                    //调用之前的方法，保存入库单和入库明细到数据库
                    BaseResultDataValue baseResultDataValue = IBReaBmsInDoc.AddInDocAndInDtlList(inDoc, inDtlList, emp.Id, emp.CName);
                    if (baseResultDataValue.success)
                    {
                        //货品属性：是否打印条码=否，不调用智方内部规则生成条码，但需要将第三方的条码信息保存到条码操作表中（盒条码、不存在大小包装单位转换）
                        baseResultDataValue = IBReaGoodsBarcodeOperation.AddReaGoodsBarcodeOperationByHRPInterface(inDtlList, emp.Id, emp.CName);
                    }

                    if (!baseResultDataValue.success)
                    {
                        baseResultData.success = false;
                        baseResultData.message = baseResultDataValue.ErrorInfo;
                    }
                }
                else
                {
                    baseResultData.success = false;
                    baseResultData.message = "入库单或入库明细为空，无法保存！";
                }
            }
            catch (Exception ex)
            {
                baseResultData.success = false;
                baseResultData.message = "接口异常：" + ex.Message;
            }

            ZhiFang.Common.Log.Log.Info("入库接口执行结束，返回：" + baseResultData.success + "|" + baseResultData.message);
            return baseResultData;
        }

        /// <summary>
        /// 更新LIS订单数据标志服务（赣南医学附属第一医院 - HRP接口）
        /// 1、HRP对订单进行审核，LIS订单数据标志置状态：供应商确认
        /// 2、HRP对订单进行审核退回，LIS订单数据标志置状态：取消确认
        /// </summary>
        /// <param name="xmlData"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = true, Description = "更新LIS订单数据标志服务")]
        public BaseResultData RS_UpdateOrderDocIOFlag(string xmlData)
        {
            ZhiFang.Common.Log.Log.Info("更新LIS订单数据标志服务执行开始，方法名称=RS_UpdateOrderDocIOFlag，入参xmlData=" + xmlData);
            BaseResultData baseResultData = new BaseResultData();

            #region 校验xml格式和验证token
            if (string.IsNullOrWhiteSpace(xmlData))
            {
                baseResultData.success = false;
                baseResultData.message = "接口服务参数xmlData值不能为空！";
                return baseResultData;
            }
            StringReader StrStream = new StringReader(xmlData);
            XDocument xml = XDocument.Load(StrStream);
            baseResultData = BaseDictSyncDataAuthentication(xml);
            if (!baseResultData.success)
            {
                return baseResultData;
            }
            string appkey = "";
            string timestamp = "";
            string token = "";
            string version = "";
            string dictType = "";
            BaseDictSyncDataPara(xml, ref appkey, ref timestamp, ref token, ref version, ref dictType);
            baseResultData = ParaIsNullValidate(appkey, timestamp, xmlData, token, version);
            if (!baseResultData.success)
            {
                return baseResultData;
            }
            baseResultData = UserAuthentication(appkey, timestamp, token);
            if (!baseResultData.success)
            {
                return baseResultData;
            }
            #endregion

            try
            {
                IApplicationContext context = ContextRegistry.GetContext();
                IBReaBmsCenOrderDoc IBReaBmsCenOrderDoc = (IBReaBmsCenOrderDoc)context.GetObject("BReaBmsCenOrderDoc");
                InterfaceDataConvert interfaceDataConvert = new InterfaceDataConvert();

                ReaBmsCenOrderDoc reaBmsCenOrderDoc = null;
                interfaceDataConvert.ConvertReaBmsInDocAndDtlByXml(xmlData, ref reaBmsCenOrderDoc);

                if (reaBmsCenOrderDoc == null)
                {
                    baseResultData.success = false;
                    baseResultData.message = "根据主单唯一标识未能在LIS试剂系统中获取到订单信息！";
                }
                else
                {
                    IBReaBmsCenOrderDoc.Entity = reaBmsCenOrderDoc;
                    IBReaBmsCenOrderDoc.Edit();
                }

            }
            catch (Exception ex)
            {
                baseResultData.success = false;
                baseResultData.message = "接口异常：" + ex.Message;
            }

            ZhiFang.Common.Log.Log.Info("更新LIS订单数据标志服务执行结束，返回：" + baseResultData.success + "|" + baseResultData.message);
            return baseResultData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appkey"></param>
        /// <param name="timestamp"></param>
        /// <param name="token"></param>
        /// <param name="goodsNo"></param>
        /// <param name="lastModifyTime"></param>
        /// <param name="fieldList"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        [WebMethod]
        public BaseResultData RS_QueryReaGoodsStock(string appkey, string timestamp, string token, string goodsNo, string lastModifyTime, string fieldList, string version)
        {
            ZhiFang.Common.Log.Log.Info("接口服务名称: RS_QueryReaGoodsStock");
            BaseResultData baseResultData = new BaseResultData();
            try
            {
                DateTime dt = DateTime.Now;
                if ((!string.IsNullOrWhiteSpace(lastModifyTime)) && (!DateTime.TryParse(lastModifyTime, out dt)))
                {
                    baseResultData.success = false;
                    baseResultData.message = "lastModifyTime参数值日期格式不正确：" + lastModifyTime;
                    return baseResultData;
                }
                //baseResultData = UserAuthentication(appkey, timestamp, token);
                //if (!baseResultData.success)
                //return baseResultData;
                IApplicationContext context = ContextRegistry.GetContext();
                IBReaBmsQtyDtl IBReaBmsQtyDtl = (IBReaBmsQtyDtl)context.GetObject("BReaBmsQtyDtl");
                string dataBody = IBReaBmsQtyDtl.QueryReaGoodsStockXML(goodsNo, lastModifyTime, fieldList, "");
                StringBuilder strXml = new StringBuilder();
                strXml.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                strXml.Append("<Response>");
                strXml.Append("<Header>");
                strXml.Append("<ResponseTime>" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "</ResponseTime>");
                strXml.Append("</Header>");
                strXml.Append("<Body>");
                strXml.Append(dataBody);
                strXml.Append("</Body>");
                strXml.Append("</Response>");
                baseResultData.data = strXml.ToString();
            }
            catch (Exception ex)
            {
                baseResultData.success = false;
                baseResultData.message = "获取货品库存数据错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Info(baseResultData.message);
            }
            return baseResultData;
        }

        [WebMethod]
        public string RS_QueryReaGoodsStock_Ex(string appkey, string timestamp, string token, string goodsNo, string lastModifyTime, string fieldList, string version)
        {
            ZhiFang.Common.Log.Log.Info("接口服务名称: RS_QueryReaGoodsStock_Ex");
            BaseResultData brd = RS_QueryReaGoodsStock(appkey, timestamp, token, goodsNo, lastModifyTime, fieldList, version);
            return ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(brd);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="appkey"></param>
        /// <param name="timestamp"></param>
        /// <param name="token"></param>
        /// <param name="goodsNo"></param>
        /// <param name="lastModifyTime"></param>
        /// <param name="fieldList"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        [WebMethod]
        public BaseResultData RS_QueryReaGoodsInfo(string appkey, string timestamp, string token, string goodsNo, string lastModifyTime, string fieldList, string version)
        {
            ZhiFang.Common.Log.Log.Info("接口服务名称: RS_QueryReaGoodsInfo");
            BaseResultData baseResultData = new BaseResultData();
            try
            {
                DateTime dt = DateTime.Now;
                if ((!string.IsNullOrWhiteSpace(lastModifyTime)) && (!DateTime.TryParse(lastModifyTime, out dt)))
                {
                    baseResultData.success = false;
                    baseResultData.message = "lastModifyTime参数值日期格式不正确：" + lastModifyTime;
                    return baseResultData;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBReaGoodsOrgLink IBReaGoodsOrgLink = (IBReaGoodsOrgLink)context.GetObject("BReaGoodsOrgLink");
                string dataBody = IBReaGoodsOrgLink.QueryReaGoodsXML(goodsNo, lastModifyTime, fieldList, "");
                StringBuilder strXml = new StringBuilder();
                strXml.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                strXml.Append("<Response>");
                strXml.Append("<Header>");
                strXml.Append("<ResponseTime>" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "</ResponseTime>");
                strXml.Append("</Header>");
                strXml.Append("<Body>");
                strXml.Append(dataBody);
                strXml.Append("</Body>");
                strXml.Append("</Response>");
                baseResultData.data = strXml.ToString();
            }
            catch (Exception ex)
            {
                baseResultData.success = false;
                baseResultData.message = "获取货品库存数据错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Info(baseResultData.message);
            }
            return baseResultData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appkey"></param>
        /// <param name="timestamp"></param>
        /// <param name="token"></param>
        /// <param name="goodsNo"></param>
        /// <param name="lastModifyTime"></param>
        /// <param name="fieldList"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        [WebMethod]
        public BaseResultData RS_QueryReaOrgGoodsInfo(string appkey, string timestamp, string token, string goodsNo, string orgNo, string lastModifyTime, string fieldList, string version)
        {
            ZhiFang.Common.Log.Log.Info("接口服务名称: RS_QueryReaOrgGoodsInfo");
            BaseResultData baseResultData = new BaseResultData();
            try
            {
                DateTime dt = DateTime.Now;
                if ((!string.IsNullOrWhiteSpace(lastModifyTime)) && (!DateTime.TryParse(lastModifyTime, out dt)))
                {
                    baseResultData.success = false;
                    baseResultData.message = "lastModifyTime参数值日期格式不正确：" + lastModifyTime;
                    return baseResultData;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBReaGoodsOrgLink IBReaGoodsOrgLink = (IBReaGoodsOrgLink)context.GetObject("BReaGoodsOrgLink");
                string dataBody = IBReaGoodsOrgLink.QueryReaOrgGoodsXML(goodsNo, orgNo, lastModifyTime, fieldList, "");
                StringBuilder strXml = new StringBuilder();
                strXml.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                strXml.Append("<Response>");
                strXml.Append("<Header>");
                strXml.Append("<ResponseTime>" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "</ResponseTime>");
                strXml.Append("</Header>");
                strXml.Append("<Body>");
                strXml.Append(dataBody);
                strXml.Append("</Body>");
                strXml.Append("</Response>");
                baseResultData.data = strXml.ToString();
            }
            catch (Exception ex)
            {
                baseResultData.success = false;
                baseResultData.message = "获取货品库存数据错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Info(baseResultData.message);
            }
            return baseResultData;
        }

        /// <summary>
        /// 基础字典同步服务
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = true, Description = "基础字典同步服务")]
        public BaseResultData RS_BaseDictSync(string data)
        {
            ZhiFang.Common.Log.Log.Info("基础字典同步开始，接口服务名称: RS_BaseDictSync");
            BaseResultData baseResultData = new BaseResultData();
            StringReader StrStream = null;
            if (string.IsNullOrWhiteSpace(data))
            {
                baseResultData.success = false;
                baseResultData.message = "参数Data值不能为空！";
                ZhiFang.Common.Log.Log.Info(baseResultData.message);
                return baseResultData;
            }
            ZhiFang.Common.Log.Log.Info(data);
            CenOrg cenOrg = null;
            HREmployee emp = null;
            InterfaceCommon ic = new InterfaceCommon();
            baseResultData = ic.UserLogin(ref emp, ref cenOrg);
            if (!baseResultData.success)
                return baseResultData;
            try
            {
                StrStream = new StringReader(data);
                XDocument xml = XDocument.Load(StrStream);
                //校验第三方软件传递基础数据的格式
                baseResultData = BaseDictSyncDataAuthentication(xml);
                if (baseResultData.success)
                {
                    string appkey = "";
                    string timestamp = "";
                    string token = "";
                    string version = "";
                    string dictType = "";
                    BaseDictSyncDataPara(xml, ref appkey, ref timestamp, ref token, ref version, ref dictType);
                    baseResultData = ParaIsNullValidate(appkey, timestamp, data, token, version);
                    if (!baseResultData.success)
                        return baseResultData;
                    baseResultData = UserAuthentication(appkey, timestamp, token);
                    if (!baseResultData.success)
                        return baseResultData;
                    baseResultData = BaseDictSyncData(xml, dictType, emp);
                }               
            }
            catch (Exception ex)
            {
                baseResultData.success = false;
                baseResultData.message = "数据同步错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Info(baseResultData.message);
            }
            finally
            {
                //释放资源
                if (StrStream != null)
                {
                    StrStream.Close();
                    StrStream.Dispose();
                }
            }
            ZhiFang.Common.Log.Log.Info("基础字典同步结束，返回：" + baseResultData.success + "|" + baseResultData.message);
            return baseResultData;
        }

        [WebMethod]
        public string RS_BaseDictSync_Ex(string data)
        {
            ZhiFang.Common.Log.Log.Info("接口服务名称: RS_BaseDictSync_Ex");
            BaseResultData brd = RS_BaseDictSync(data);
            return ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(brd);
        }

        private BaseResultData ReadUserCenOrgInfo(ref ReaCenOrg reaCenOrg)
        {
            BaseResultData baseResultData = new BaseResultData();
            string employeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            if (string.IsNullOrEmpty(employeeID))
            {
                baseResultData.success = false;
                baseResultData.message = "无法获取当前用户的ID信息,请重新登录！";
                return baseResultData;
            }
            IApplicationContext context = ContextRegistry.GetContext();
            IBHREmployee IBHREmployee = (IBHREmployee)context.GetObject("BHREmployee");
            IBReaCenOrg IBReaCenOrg = (IBReaCenOrg)context.GetObject("IBReaCenOrg");
            HREmployee hrEmployee = IBHREmployee.Get(Int64.Parse(employeeID));
            IList<ReaCenOrg> listCenOrg = null;
            if (hrEmployee.HRDept != null && (!string.IsNullOrEmpty(hrEmployee.HRDept.UseCode)))
            {
                listCenOrg = IBReaCenOrg.SearchListByHQL(" reacenorg.OrgNo=\'" + hrEmployee.HRDept.UseCode + "\'");
                if (listCenOrg != null && listCenOrg.Count > 0)
                {
                    if (listCenOrg.Count == 1)
                    {
                        reaCenOrg = listCenOrg[0];
                        //orgNo = reaCenOrg.ShortCode;
                        //orgName = reaCenOrg.CName;
                        //if (string.IsNullOrEmpty(orgNo))
                        //{
                        //    baseResultData.success = false;
                        //    baseResultData.ErrorInfo = "本机构的代码信息没有维护，请联系管理员！";
                        //    return baseResultData;
                        //}
                    }
                }
                else
                {
                    baseResultData.success = false;
                    baseResultData.message = "找不到当前登陆者的机构信息！";
                    ZhiFang.Common.Log.Log.Info(baseResultData.message);
                    return baseResultData;
                }
            }
            else
            {
                baseResultData.success = false;
                baseResultData.message = "当前登陆者的机构编码信息不存在！";
                ZhiFang.Common.Log.Log.Info(baseResultData.message);
                return baseResultData;
            }
            return baseResultData;
        }

        /// <summary>
        /// 校验第三方软件传递基础数据的格式
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private BaseResultData BaseDictSyncDataAuthentication(XDocument xml)
        {
            BaseResultData baseResultData = new BaseResultData();
            XElement xeRoot = xml.Root;//根目录
            if (xeRoot == null)
            {
                baseResultData.success = false;
                baseResultData.message = "参数Data值：XML数据没有根节点！";
                ZhiFang.Common.Log.Log.Error(baseResultData.message);
                return baseResultData;
            }
            XElement xeHead = xeRoot.Element("Header");
            if (xeHead == null)
            {
                baseResultData.success = false;
                baseResultData.message = "参数Data值：XML数据没有Head数据节点！";
                ZhiFang.Common.Log.Log.Error(baseResultData.message);
                return baseResultData;
            }
            if (!xeHead.HasElements)
            {
                baseResultData.success = false;
                baseResultData.message = "参数Data值：XML数据节点Header没有数据！";
                ZhiFang.Common.Log.Log.Error(baseResultData.message);
                return baseResultData;
            }
            XElement xeBody = xeRoot.Element("Body");
            if (xeBody == null)
            {
                baseResultData.success = false;
                baseResultData.message = "参数Data值：XML数据没有Body数据节点！";
                ZhiFang.Common.Log.Log.Error(baseResultData.message);
                return baseResultData;
            }
            //IList<XElement> xeBodyChild = xeBody.Elements("Row").ToList();
            //if (xeBodyChild == null || xeBodyChild.Count == 0)
            //{
            //    baseResultData.success = false;
            //    baseResultData.message = "参数Data值：XML数据节点Body没有数据！";
            //    return baseResultData;
            //}
            return baseResultData;
        }

        /// <summary>
        /// 解析XML接口数据，获取参数appkey, timestamp, token, version, dictType的值
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="appkey"></param>
        /// <param name="timestamp"></param>
        /// <param name="token"></param>
        /// <param name="version"></param>
        /// <param name="dictType"></param>
        private void BaseDictSyncDataPara(XDocument xml, ref string appkey, ref string timestamp, ref string token, ref string version, ref string dictType)
        {
            XElement xeRoot = xml.Root;//根目录           
            XElement xeHead = xeRoot.Element("Header");
            appkey = xeHead.Element("Appkey") != null ? xeHead.Element("Appkey").Value : "";
            timestamp = xeHead.Element("Timestamp") != null ? xeHead.Element("Timestamp").Value : "";
            token = xeHead.Element("Token") != null ? xeHead.Element("Token").Value : "";
            version = xeHead.Element("Version") != null ? xeHead.Element("Version").Value : "";
            dictType = xeHead.Element("DictType") != null ? xeHead.Element("DictType").Value : "";
            ZhiFang.Common.Log.Log.Info("Appkey:" + appkey + ",Timestamp:" + timestamp + ",Token:" + token + ",Version:" + version + ",DictType:" + dictType);
        }

        /// <summary>
        /// 同步科室、员工、机构、货品、库房基础数据
        /// </summary>
        /// <param name="xml">XML数据</param>
        /// <param name="dictType">同步基础数据的类型，例如：DEPT 代表科室</param>
        /// <returns></returns>
        private BaseResultData BaseDictSyncData(XDocument xml, string dictType, HREmployee emp)
        {
            BaseResultData baseResultData = new BaseResultData();
            if (string.IsNullOrEmpty(dictType))
            {
                baseResultData.success = false;
                baseResultData.message = "参数DictType不能为空！";
                ZhiFang.Common.Log.Log.Info(baseResultData.message);
                return baseResultData;
            }
            InterfaceCommon interfaceCommon = new InterfaceCommon();
            if (dictType.ToUpper() == "DEPT")
                baseResultData = interfaceCommon.DeptSyncData(xml);
            else if (dictType.ToUpper() == "EMPLOYEE")
                baseResultData = interfaceCommon.EmployeeSyncData(xml);
            else if (dictType.ToUpper() == "REACENORG")
                baseResultData = interfaceCommon.ReaCenOrgSyncData(xml);
            else if (dictType.ToUpper() == "REAGOODS")
                baseResultData = interfaceCommon.ReaGoodsSyncData(xml, emp);
            else if (dictType.ToUpper() == "REASTORAGE")
                baseResultData = interfaceCommon.ReaStorageSyncData(xml);
            else if (dictType.ToUpper() == "REAORGGOODS")
                baseResultData = interfaceCommon.ReaOrgGoodsSyncData(xml, emp);

            return baseResultData;
        }

        /// <summary>
        /// AppKey验证，验证用户的合法性
        /// </summary>
        /// <param name="appkey"></param>
        /// <param name="timestamp"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private BaseResultData UserAuthentication(string appkey, string timestamp, string token)
        {
            BaseResultData baseResultData = new BaseResultData();
            ZhiFang.Common.Log.Log.Info("UserAuthentication 开始");
            Dictionary<string, string> dicAppKey = new Dictionary<string, string>();

            string isCheckToken = ConfigHelper.GetConfigString("IsCheckToken");
            ZhiFang.Common.Log.Log.Info("读取Web.Config参数IsCheckToken，是否验证token=" + isCheckToken);
            if (isCheckToken != "" && isCheckToken != "0")
            {
                if (string.IsNullOrWhiteSpace(token))
                {
                    baseResultData.success = false;
                    baseResultData.message = "参数token不能为空！";
                    ZhiFang.Common.Log.Log.Info(baseResultData.message);
                    return baseResultData;
                }
                string appSecret = "950df74b-ad5b-430f-8c20-bff65de3f04a";
                string xmlAppKey = HttpContext.Current.Server.MapPath("~/") + "\\BaseTableXML\\Interface\\Appkey.xml";
                if (System.IO.File.Exists(xmlAppKey))
                {
                    GetColumnNameByXMLFile(xmlAppKey, ref dicAppKey);
                }
                else
                    ZhiFang.Common.Log.Log.Info("Appkey.xml文件不存在：" + baseResultData.message);
                if (dicAppKey.Keys.Contains(appkey))
                    appSecret = dicAppKey[appkey];
                ZhiFang.Common.Log.Log.Info("AppSecret:" + appSecret);
                //判断AppKey是否存在
                //如果存在，取出appSecret，接口调用方ID
                //计算Token
                string token_server = ZhiFang.Common.Public.SecurityHelp.NetMd5EncryptStr32(timestamp + appSecret);
                ZhiFang.Common.Log.Log.Info("token_server：" + token_server + "   token：" + token);
                if (token_server.ToUpper() != token.ToUpper())
                {
                    baseResultData.success = false;
                    baseResultData.code = ((int)InterfaceCodeValue.token无效访问被拒绝).ToString();
                    baseResultData.message = "token无效，访问被拒绝！";
                    ZhiFang.Common.Log.Log.Info(baseResultData.message);
                    return baseResultData;
                }
            }
            else
            {
                ZhiFang.Common.Log.Log.Info("token不验证");
            }

            string isCheckIntervalTime = ConfigHelper.GetConfigString("IsCheckIntervalTime");
            ZhiFang.Common.Log.Log.Info("读取Web.Config参数IsCheckIntervalTime，是否验证调用者和服务器的间隔时间=" + isCheckIntervalTime);
            if (!(isCheckIntervalTime == "0"))
            {
                if (string.IsNullOrWhiteSpace(timestamp))
                {
                    baseResultData.success = false;
                    baseResultData.message = "参数timestamp不能为空！";
                    ZhiFang.Common.Log.Log.Info(baseResultData.message);
                    return baseResultData;
                }
                int intervalTimeValue = 600;
                string tempIntervalTimeValue = ConfigHelper.GetConfigString("IntervalTimeValue");
                if (tempIntervalTimeValue != null && tempIntervalTimeValue.Trim().Length > 0)
                    if (!int.TryParse(tempIntervalTimeValue, out intervalTimeValue))
                        intervalTimeValue = 600;
                DateTime clientTime = DateTime.ParseExact(timestamp, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
                DateTime servertTime = DateTime.Now;
                TimeSpan span = servertTime - clientTime;
                int interval = System.Math.Abs((int)span.TotalSeconds);
                if (interval > intervalTimeValue)//600秒，客户端和服务器时间间隔大于600秒，访问被拒绝
                {
                    baseResultData.success = false;
                    baseResultData.code = ((int)InterfaceCodeValue.客户端时间和服务器时间间隔相差过大).ToString();
                    baseResultData.message = "客户端时间和服务器时间间隔相差过大，访问被拒绝！";
                    ZhiFang.Common.Log.Log.Info(baseResultData.code + "：" + baseResultData.message);
                    return baseResultData;
                }
            }
            else
            {
                ZhiFang.Common.Log.Log.Info("有效间隔时间不验证");
            }
            ZhiFang.Common.Log.Log.Info("UserAuthentication 结束");
            return baseResultData;
        }

        /// <summary>
        /// 验证参数appkey, timestamp, data, token是否为空
        /// </summary>
        /// <param name="appkey"></param>
        /// <param name="timestamp"></param>
        /// <param name="data"></param>
        /// <param name="token"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        private BaseResultData ParaIsNullValidate(string appkey, string timestamp, string data, string token, string version)
        {
            ZhiFang.Common.Log.Log.Info("ParaIsNullValidate 开始");
            BaseResultData baseResultData = new BaseResultData();
            if (string.IsNullOrEmpty(appkey))
            {
                baseResultData.success = false;
                baseResultData.message = "参数appkey不能为空！";
                ZhiFang.Common.Log.Log.Info(baseResultData.message);
            }
            else
            if (string.IsNullOrEmpty(timestamp))
            {
                //baseResultData.success = false;
                //baseResultData.message = "参数timestamp不能为空！";
                //ZhiFang.Common.Log.Log.Info(baseResultData.message);
            }
            else
            if (string.IsNullOrEmpty(data))
            {
                baseResultData.success = false;
                baseResultData.message = "参数data不能为空！";
                ZhiFang.Common.Log.Log.Info(baseResultData.message);
            }
            else
            if (string.IsNullOrEmpty(token))
            {
                //baseResultData.success = false;
                //baseResultData.message = "参数token不能为空！";
                //ZhiFang.Common.Log.Log.Info(baseResultData.message);
            }
            if (!baseResultData.success)
            {
                baseResultData.code = ((int)InterfaceCodeValue.必填参数信息为空).ToString();
                ZhiFang.Common.Log.Log.Info(baseResultData.code + "：" + baseResultData.message);
            }
            ZhiFang.Common.Log.Log.Info("ParaIsNullValidate 结束");
            return baseResultData;
        }

        private BaseResultData JudageRequiredFieldInIsNull(JToken jsonToken, string requiredField, string docName, int docType, bool isArray)
        {
            BaseResultData baseResultData = new BaseResultData();
            ZhiFang.Common.Log.Log.Info("JudageRequiredFieldInIsNull 开始");
            IList<string> listField = requiredField.Split(',').ToList();
            bool isNull = false;
            string nullFieldName = "";//有空值的字段列表
            bool isVlueNull = false;//数组里的必填字段是否有空值的标志
            foreach (string field in listField)
            {
                JToken tokenField = null;
                if (isArray)
                {
                    var listValue = jsonToken.Select(p => p[field]).ToList();
                    foreach (var value in listValue)
                    {
                        if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                        {
                            isVlueNull = true;
                            break;
                        }
                    }
                }
                else
                {
                    tokenField = jsonToken.SelectToken(field);
                    isVlueNull = (tokenField == null || string.IsNullOrWhiteSpace(tokenField.ToString()));
                }
                if (isVlueNull)
                {
                    isNull = true;
                    if (nullFieldName == "")
                        nullFieldName = field;
                    else
                        nullFieldName += "," + field;
                }
            }
            if (isNull)
            {
                baseResultData.success = false;
                if (docType == 1)
                    baseResultData.code = ((int)InterfaceCodeValue.订货单必填字段信息为空).ToString();
                else if (docType == 2)
                    baseResultData.code = ((int)InterfaceCodeValue.供货单必填字段信息为空).ToString();
                baseResultData.message = docName + "必填字段信息为空！字段名称：" + nullFieldName;
                ZhiFang.Common.Log.Log.Info(baseResultData.code + "：" + baseResultData.message);
            }
            ZhiFang.Common.Log.Log.Info("JudageRequiredFieldInIsNull 结束");
            return baseResultData;
        }

        private void GetColumnNameByXMLFile(string fieldXmlPath, ref Dictionary<string, string> dic)
        {
            DataSet ds = ZhiFang.Common.Public.XmlToData.XmlFileToDataSet(fieldXmlPath);
            foreach (DataRow dataRow in ds.Tables[0].Rows)
            {
                if (!string.IsNullOrEmpty(dataRow["AppKey"].ToString()))
                {
                    dic.Add(dataRow["AppKey"].ToString().Trim(), dataRow["AppSecret"].ToString().Trim());
                    ZhiFang.Common.Log.Log.Info("获取Appkey信息：AppKey=" + dataRow["AppKey"].ToString().Trim() + "，AppSecret=" + dataRow["AppSecret"].ToString().Trim());
                }
            }
        }

        private BaseResultData ReaSaleDocCreate(string data, bool isAutoConfirm)
        {
            BaseResultData baseResultData = new BaseResultData();
            StringReader StrStream = null;
            if (string.IsNullOrWhiteSpace(data))
            {
                baseResultData.success = false;
                baseResultData.message = "接口服务参数Data值不能为空！";
                return baseResultData;
            }
            ZhiFang.Common.Log.Log.Info("RS_ReaSaleDocCreate方法Data参数值：" + data);
            CenOrg cenOrg = null;
            HREmployee emp = null;
            InterfaceCommon ic = new InterfaceCommon();
            baseResultData = ic.UserLogin(ref emp, ref cenOrg);
            if (!baseResultData.success)
                return baseResultData;
            try
            {
                StrStream = new StringReader(data);
                XDocument xml = XDocument.Load(StrStream);
                baseResultData = BaseDictSyncDataAuthentication(xml);
                if (baseResultData.success)
                {
                    string appkey = "";
                    string timestamp = "";
                    string token = "";
                    string version = "";
                    string dictType = "";
                    BaseDictSyncDataPara(xml, ref appkey, ref timestamp, ref token, ref version, ref dictType);
                    baseResultData = ParaIsNullValidate(appkey, timestamp, data, token, version);
                    if (!baseResultData.success)
                        return baseResultData;
                    baseResultData = UserAuthentication(appkey, timestamp, token);
                    if (!baseResultData.success)
                        return baseResultData;
                    IApplicationContext context = ContextRegistry.GetContext();
                    IBReaBmsCenSaleDoc IBSaleDoc = (IBReaBmsCenSaleDoc)context.GetObject("BReaBmsCenSaleDoc");
                    IBReaBmsCenSaleDtl IBSaleDtl = (IBReaBmsCenSaleDtl)context.GetObject("BReaBmsCenSaleDtl");
                    ReaBmsCenSaleDoc reaSaleDoc = null;
                    IList<ReaBmsCenSaleDtl> reaSaleDtlList = null;
                    ZhiFang.Common.Log.Log.Info("开始解析供货单数据！");
                    BaseResultDataValue brdv = IBSaleDoc.AddReaBmsCenSaleDocByInterface(data, cenOrg, emp, ref reaSaleDoc, ref reaSaleDtlList);
                    baseResultData.success = brdv.success;
                    baseResultData.message = brdv.ErrorInfo;
                    if (baseResultData.success)
                    {
                        ZhiFang.Common.Log.Log.Info("解析供货单数据成功！");
                        if (reaSaleDoc != null && reaSaleDtlList != null)
                        {
                            IBSaleDoc.AddReaBmsCenSaleDtlMerge(ref reaSaleDoc, ref reaSaleDtlList);//盒条码合并数据
                            reaSaleDtlList = IBSaleDtl.SearchListByHQL(" reabmscensaledtl.SaleDocID=" + reaSaleDoc.Id);//盒条码合并数据数据后重新明细获取数据
                            IBReaBmsInDoc IBReaBmsInDoc = (IBReaBmsInDoc)context.GetObject("BReaBmsInDoc");
                            InterfaceDataConvert ifdc = new InterfaceDataConvert();
                            ReaBmsInDoc inDoc = new ReaBmsInDoc();
                            IList<ReaBmsInDtl> inDtlList = new List<ReaBmsInDtl>();
                            ifdc.SaleDocDataConvert(reaSaleDoc, reaSaleDtlList, 0, ref inDoc, ref inDtlList);
                            if (inDoc != null && inDtlList != null)
                                IBReaBmsInDoc.AddInDocAndInDtlList(inDoc, inDtlList, emp.Id, emp.CName);
                            else
                                ZhiFang.Common.Log.Log.Info("供货单转换的入库单或入库子单为空！");
                        }
                        else
                            ZhiFang.Common.Log.Log.Info("接口数据转换的供货单或供货子单为空！");
                        if (isAutoConfirm)
                            ReaSaleDocConfirm(reaSaleDoc, emp);
                    }
                    else
                        ZhiFang.Common.Log.Log.Info(baseResultData.message);
                }
            }
            catch (Exception ex)
            {
                baseResultData.success = false;
                baseResultData.message = "供货单数据错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Info(baseResultData.message);
            }
            finally
            {
                //释放资源
                if (StrStream != null)
                {
                    StrStream.Close();
                    StrStream.Dispose();
                }
            }
            return baseResultData;
        }

        private BaseResultData ReaSaleDocConfirm(ReaBmsCenSaleDoc reaSaleDoc, HREmployee emp)
        {
            BaseResultData baseResultData = new BaseResultData();
            try
            {
                baseResultData = InterfaceCommon.GetThirdSaleDocType();
                if (!baseResultData.success)
                {
                    baseResultData.success = baseResultData.success;
                    baseResultData.message = baseResultData.message;
                    return baseResultData;
                }
                if (string.IsNullOrEmpty(baseResultData.data))
                {
                    baseResultData.success = false;
                    baseResultData.message = "获取不到第三方接口配置信息！";
                    return baseResultData;
                }
                string userType = baseResultData.data.ToLower();

                if (userType == "bjwjyy_his") //北京望京医院--东华物资
                {

                }
                else if (userType == "yzrmyy_donghua") //鄞州人民医院--东华物资
                {
                    InterfaceYZRMYYDongHua yzrmyyDongHua = new InterfaceYZRMYYDongHua();
                    baseResultData = yzrmyyDongHua.ReaBmsCenSaleDocConfirm(reaSaleDoc, emp);
                }
            }
            catch (Exception ex)
            {
                baseResultData.success = false;
                baseResultData.message = "入库单接收确认接口服务错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Info(baseResultData.message);
            }
            return baseResultData;
        }

    }
}
