/*************************************
Author: douss
Project: 四川大家NC试剂接口
Date: 2021-05-18
Modify: 
*************************************/
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.InterfaceFactory;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.IDAO.Base;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using ZhiFang.Common.Public;
using ZhiFang.ReagentSys.Client.Common;
using ZhiFang.Common.Log;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using ZhiFang.IBLL.RBAC;
using ZhiFang.Entity.RBAC;
using ZhiFang.IDAO.RBAC;
using System.Xml;
using ZhiFang.ServiceCommon.RBAC;

namespace NC.SCDJ
{
    public class PlugInClass : DllInterface
    {
        #region 同步货品

        /// <summary>
        /// 调用NC接口获取试剂货品信息并保存
        /// </summary>
        /// <param name="ht"></param>
        /// <returns></returns>
        public BaseResultBool GetReaGoodsByInterface(Hashtable ht)
        {
            Log.Info("进入到定制插件NC.SCDJ，方法GetReaGoodsByInterface，执行调用NC接口同步货品信息开始");
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IApplicationContext context = ContextRegistry.GetContext();
                IBReaGoods IBReaGoods = (IBReaGoods)context.GetObject("BReaGoods");
                IDReaGoodsDao IDReaGoodsDao = (IDReaGoodsDao)context.GetObject("ReaGoodsDao");
                IBBDict IBBDict = (IBBDict)context.GetObject("BBDict");
                IDReaCenOrgDao IDReaCenOrgDao = (IDReaCenOrgDao)context.GetObject("ReaCenOrgDao");
                IBReaGoodsOrgLink IBReaGoodsOrgLink = (IBReaGoodsOrgLink)context.GetObject("BReaGoodsOrgLink");

                //获取所有货品
                IList<ReaGoods> allReaGoodsList = IBReaGoods.SearchListByHQL("");
                //NC的公司编码
                int PK_CORP = int.Parse(ht["MatchCodeDept"].ToString());
                long LabID = long.Parse(ht["LabID"].ToString());
                //写入到修改操作记录表使用变量
                string fields = "Id,CName,EName,ShortCode,ProdGoodsNo,ProdOrgName,UnitName,InvType,UnitMemo,IsMed,TestCount,TS,RegistNo,RegistNoInvalidDate,ProdEara,StorageType,NetGoodsNo,Visible,DataUpdateTime,Price";
                string[] arrFields = fields.Split(',');

                //供应商ID，客户端调用有值，平台调用为0
                long supplierId = long.Parse(ht["SupplierId"].ToString());
                //获取最大的时间戳
                string ts = ((ht["TS"] != null && ht["TS"].ToString() != "") ? ht["TS"].ToString() : IDReaGoodsDao.GetMaxTS());
                if (ts == "0")
                {
                    ts = "2010-01-01";
                }
                else
                {
                    ts = DateTime.Parse(ts).ToString("yyyy-MM-dd HH:mm:ss");
                }

                ReaCenOrg reaCenOrgGH = null;
                Log.Info("请求NC接口服务开始");
                string url = GetInterfaceUrl(4) + "/api/Domain/univer/dbbus/search";
                Log.Info("地址：" + url);
                string data = "";
                if (supplierId == 0)
                {
                    data = "{\"source\":\"greatmaster\",\"tag\":\"djjy_cpda\",\"ts\":\"" + ts + "\",\"pk_corp\":\"" + PK_CORP + "\"}";
                    Log.Info("参数data(平台)：" + data);
                }
                else
                {
                    reaCenOrgGH = IDReaCenOrgDao.Get(supplierId);
                    if (reaCenOrgGH == null)
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "根据供货商ID=" + supplierId + "未能获取到供货商信息，请检查！";
                        return baseResultBool;
                    }
                    Log.Info("供货商物资对照码=" + reaCenOrgGH.MatchCode);
                    data = "{\"source\":\"greatmaster\",\"tag\":\"djjy_cpda_dhf\",\"ts\":\"" + ts + "\",\"pk_corp\":\"" + reaCenOrgGH.MatchCode + "\",\"dhfcode\":\"" + PK_CORP + "\"}";
                    Log.Info("参数data(客户端)：" + data);
                }
                System.Collections.Specialized.NameValueCollection VarPost = new System.Collections.Specialized.NameValueCollection();
                VarPost.Add("data", data);
                string resultStr = WebRequestHelp.WebClientPost(url, VarPost);
                Log.Info("请求NC接口服务结束，NC接口返回：" + resultStr);

                if (string.IsNullOrWhiteSpace(resultStr))
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "NC返回货品信息为空，无法保存";
                    return baseResultBool;
                }

                //StreamReader streamReader = new StreamReader(@"D:\Test\json2.txt", Encoding.UTF8);
                //string resultStr = streamReader.ReadToEnd();
                //streamReader.Close();

                //解析平台返回的json，转换到对象List里
                List<NcGoods> tempNcGoodsList = new List<NcGoods>();
                JObject jo = (JObject)JsonConvert.DeserializeObject(resultStr);
                string result = jo["result"].ToString();
                if (result != "")
                {
                    //去除JSON字符串中的回车，换行符，制表符
                    result = result.Replace("\n", "").Replace("\t", "").Replace("\r", "");
                    JObject jobj = (JObject)JsonConvert.DeserializeObject(result);

                    if (jobj["code"].ToString() != "0")
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "NC返回错误信息："+ jobj["msg"].ToString();
                        return baseResultBool;
                    }

                    if (jobj["result"].ToString() != "")
                    {
                        JArray arr = JArray.Parse(jobj["result"].ToString());
                        tempNcGoodsList = arr.ToObject<List<NcGoods>>();
                    }
                }

                if (supplierId == 0)
                {
                    //根据分公司筛选，只处理当前登录人所属根部门下的货品（平台调用supplierId=0，需要过滤）
                    tempNcGoodsList = tempNcGoodsList.Where(p => p.PK_CORP == PK_CORP).ToList();
                    if (tempNcGoodsList.Count == 0)
                    {
                        Log.Info("本次没有需要同步的货品信息！直接返回成功！");
                        return baseResultBool;
                    }
                }

                //遍历货品对象列表，保存到库里
                foreach (var ncGoods in tempNcGoodsList)
                {
                    ReaGoods reaGoods = new ReaGoods();
                    var l = allReaGoodsList.Where(p => p.ReaGoodsNo == ncGoods.INVCODE).ToList();
                    if (l.Count > 0)
                    {
                        //存在，修改
                        reaGoods = ConvertToReaGoods(ncGoods, l[0], LabID, false, IBBDict);
                        IBReaGoods.Entity = reaGoods;//修改后的对象
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(reaGoods, fields);
                        ReaGoods serverReaGoods = IBReaGoods.Get(l[0].Id);//没有修改之前，库里对象值
                        if (IDReaGoodsDao.Update(tempArray))
                        {
                            //操作记录                            
                            IBReaGoods.AddSCOperation(serverReaGoods, arrFields);
                        }
                        Log.Info(string.Format("货品[{0}-{1}]在货品表中已存在，做修改处理。", reaGoods.ReaGoodsNo, reaGoods.CName));
                    }
                    else
                    {
                        //不存在，新增
                        reaGoods = ConvertToReaGoods(ncGoods, reaGoods, LabID, true, IBBDict);
                        IDReaGoodsDao.Save(reaGoods);
                        Log.Info(string.Format("货品[{0}-{1}]在货品表中不存在，做新增处理。", reaGoods.ReaGoodsNo, reaGoods.CName));
                    }

                    if (supplierId > 0)
                    {
                        //客户端调用，除货品写入外，还需要写入供应商货品关系表
                        WriteReaGoodsOrgLink(IBReaGoodsOrgLink, reaGoods, reaCenOrgGH);
                    }
                }

            }
            catch (Exception ex)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "操作异常：" + ex.Message;
                Log.Error("操作异常->", ex);
            }
            Log.Info("定制插件执行调用NC接口同步货品信息结束");
            return baseResultBool;
        }

        private ReaGoods ConvertToReaGoods(NcGoods ncGoods, ReaGoods reaGoods, long labId, bool isAdd, IBBDict IBBDict)
        {
            #region 品牌（厂商）同步
            BDict bDict = null;
            string prodOrgNo = ncGoods.INVPINPAI;
            string prodOrgName = ncGoods.INVPINPAI;
            if (!string.IsNullOrWhiteSpace(prodOrgNo))
            {
                IBBDict.SaveProdOrgByInterface(prodOrgNo, new Dictionary<string, object> {
                                { "CName", prodOrgName }
                            }, ref bDict);
            }
            #endregion

            if (isAdd)
            {
                //新增
                reaGoods.LabID = labId;
                reaGoods.ReaGoodsNo = ncGoods.INVCODE;
                reaGoods.MatchCode = ncGoods.INVCODE;
                reaGoods.DataAddTime = DateTime.Now;
                reaGoods.GonvertQty = 1;
                reaGoods.IsRegister = 1;
                reaGoods.IsPrintBarCode = 1;
                reaGoods.BarCodeMgr = 0;
                reaGoods.IsNeedPerformanceTest = true;
            }
            else
            {
                //修改
                reaGoods.DataUpdateTime = DateTime.Now;
            }
            reaGoods.CName = ncGoods.INVNAME;
            reaGoods.EName = ncGoods.FORINVNAME;
            reaGoods.ShortCode = ncGoods.INVSHORTNAME;
            reaGoods.ProdGoodsNo = ncGoods.INVMNECODE;
            reaGoods.ProdOrgName = ncGoods.INVPINPAI;
            if (bDict != null) { reaGoods.ProdID = bDict.Id; }
            reaGoods.UnitName = ncGoods.MEASNAME;
            reaGoods.InvType = ncGoods.INVTYPE;
            reaGoods.UnitMemo = ncGoods.INVSPEC;
            reaGoods.IsMed = (ncGoods.ISMED == "Y" ? true : false);
            reaGoods.TestCount = (ncGoods.ZL == null ? 0 : ncGoods.ZL.Value);
            reaGoods.TS = ncGoods.TS;
            reaGoods.RegistNo = ncGoods.VAPPROVALNUM;
            reaGoods.RegistNoInvalidDate = ncGoods.VDOTECHNICS;
            reaGoods.ProdEara = ncGoods.VPRODUCINGAREA;
            reaGoods.StorageType = ncGoods.VMEDNATURALCODE;
            reaGoods.NetGoodsNo = ncGoods.GWLS;
            reaGoods.Visible = (ncGoods.VISIBLE == "Y" ? 1 : 0);
            reaGoods.Price = (ncGoods.PRICE == null ? 0 : ncGoods.PRICE.Value);

            return reaGoods;
        }

        /// <summary>
        /// 写入供货商货品关系表
        /// </summary>
        private BaseResultBool WriteReaGoodsOrgLink(IBReaGoodsOrgLink IBReaGoodsOrgLink, ReaGoods reaGoods, ReaCenOrg reaCenOrg)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);

                ReaGoodsOrgLink reaGoodsOrgLink = new ReaGoodsOrgLink();
                var list = IBReaGoodsOrgLink.SearchListByHQL(string.Format("(reagoodsorglink.CenOrg.Id=" + reaCenOrg.Id + ") and (reagoodsorglink.ReaGoods.Id=" + reaGoods.Id + ") and (reagoodsorglink.Visible=1)"));
                if (list.Count > 0)
                {
                    //修改
                    reaGoodsOrgLink = list[0];
                    reaGoodsOrgLink.Price = reaGoods.Price;
                    reaGoodsOrgLink.DataUpdateTime = DateTime.Now;
                    reaGoodsOrgLink.ReaGoods = new ReaGoods() { Id = reaGoods.Id, DataTimeStamp = reaGoods.DataTimeStamp };

                    IBReaGoodsOrgLink.Entity = reaGoodsOrgLink;
                    string fields = "Id,Price";                    
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaGoodsOrgLink.Entity, fields);
                    baseResultBool = IBReaGoodsOrgLink.UpdateReaGoodsOrgLink(tempArray, empID, empName);                    
                }
                else
                {
                    //新增
                    reaGoodsOrgLink.BarCodeType = reaGoods.BarCodeMgr;
                    reaGoodsOrgLink.BeginTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                    reaGoodsOrgLink.CenOrg = reaCenOrg;
                    reaGoodsOrgLink.CenOrgGoodsNo = reaGoods.ReaGoodsNo;
                    reaGoodsOrgLink.IsPrintBarCode = reaGoods.IsPrintBarCode;
                    reaGoodsOrgLink.Price = reaGoods.Price;
                    reaGoodsOrgLink.ProdGoodsNo = reaGoods.ProdGoodsNo;
                    reaGoodsOrgLink.ReaGoods = new ReaGoods() { Id = reaGoods.Id, DataTimeStamp = reaGoods.DataTimeStamp };
                    reaGoodsOrgLink.Visible = 1;
                    reaGoodsOrgLink.DataAddTime = DateTime.Now;

                    IBReaGoodsOrgLink.Entity = reaGoodsOrgLink;
                    BaseResultDataValue baseResultDataValue = IBReaGoodsOrgLink.AddReaGoodsOrgLink(empID, empName);
                    if (!baseResultDataValue.success)
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = baseResultDataValue.ErrorInfo;
                    }
                }
            }
            catch (Exception ex1)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "保存供货商货品关系操作异常：" + ex1.Message;
                Log.Error("操作异常->", ex1);
            }
            return baseResultBool;
        }

        #endregion

        #region 上传订单到NC

        /// <summary>
        /// 给NC接口发送订单
        ///     1、对在智方试剂平台上，对进行“供应商确认”后的订单，进行手动上传NC；
        ///     2、将器械和非器械分开成两张单子进行上传；
        ///     3、订货方的“单据类型”调用不同的服务：销售-销售预订单服务，共建-出库申请单服务，调拨-调拨服务；
        /// 
        /// </summary>
        /// <param name="ht"></param>
        /// <returns></returns>
        public BaseResultBool SendBmsCenOrderByInterface(Hashtable ht)
        {
            Log.Info("进入到定制插件NC.SCDJ，SendBmsCenOrderByInterface，执行订单上传NC接口开始");
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IApplicationContext context = ContextRegistry.GetContext();
                IBReaGoods IBReaGoods = (IBReaGoods)context.GetObject("BReaGoods");
                IDHRDeptDao IDHRDept = (IDHRDeptDao)context.GetObject("HRDeptDao");
                IDHREmployeeDao IDHREmployeeDao = (IDHREmployeeDao)context.GetObject("HREmployeeDao");
                IDReaCenOrgDao IDReaCenOrgDao = (IDReaCenOrgDao)context.GetObject("ReaCenOrgDao");
                IBReaBmsCenOrderDoc IBReaBmsCenOrderDoc = (IBReaBmsCenOrderDoc)context.GetObject("BReaBmsCenOrderDoc");
                IDReaGoodsOrgLinkDao IDReaGoodsOrgLinkDao = (IDReaGoodsOrgLinkDao)context.GetObject("ReaGoodsOrgLinkDao");

                //NC分公司的编码
                HRDept hRDept = (HRDept)ht["HRDept"];
                string pk_corp = hRDept.MatchCode;
                Log.Info("NC分公司编码=" + pk_corp);
                //订单
                ReaBmsCenOrderDoc orderDoc = (ReaBmsCenOrderDoc)ht["ReaBmsCenOrderDoc"];
                //订单明细列表
                IList<ReaBmsCenOrderDtl> orderDtlList = (IList<ReaBmsCenOrderDtl>)ht["ReaBmsCenOrderDtlList"];
                //订货方，根据PlatformOrgNo+LabID，在试剂平台库里找
                IList<ReaCenOrg> reaCenOrgDHList = IDReaCenOrgDao.GetListByHQL(string.Format("PlatformOrgNo={0}", orderDoc.ReaServerLabcCode));
                if (reaCenOrgDHList.Count == 0)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = string.Format("根据订货方平台机构码={0}未查询到订货方信息！", orderDoc.ReaServerLabcCode);
                    Log.Error(baseResultBool.ErrorInfo);
                    return baseResultBool;
                }
                ReaCenOrg reaCenOrgDH = reaCenOrgDHList[0];
                //确认人
                HREmployee confirmEmp = IDHREmployeeDao.Get(orderDoc.ConfirmID.Value);

                //将订单里的货品信息，转换为订货方货品关系
                string cenGoodsNo = string.Join(",", orderDtlList.Select(p => p.CenOrgGoodsNo).ToArray());
                cenGoodsNo = string.Format("'{0}'", cenGoodsNo.Replace(",", "','"));
                IList<ReaGoodsOrgLink> linkList = IDReaGoodsOrgLinkDao.GetListByHQL(string.Format("reagoodsorglink.CenOrg.OrgType=1 and reagoodsorglink.Visible=1 and reagoodsorglink.CenOrg.Id={0} and CenOrgGoodsNo in ({1})", reaCenOrgDH.Id, cenGoodsNo));

                //需要将货品按照器械和非器械，分开成两张单子
                var list_Med = linkList.Where(p => p.ReaGoods.IsMed).ToList();//器械
                var list_NoMed = linkList.Where(p => !p.ReaGoods.IsMed).ToList();//非器械

                #region 生成NC需要的格式，发送订单到NC

                if (reaCenOrgDH.NextBillType == int.Parse(ReaCenOrgNextBillType.销售.Key))
                {
                    #region 订货方类型=销售，调用NC销售预订单服务
                    string url = GetInterfaceUrl(1) + "/service/XChangeServlet?account=10&receiver=" + pk_corp;
                    string xml_Med = GetSendXml_XS(pk_corp, reaCenOrgDH, hRDept, confirmEmp, orderDoc, orderDtlList, list_Med);
                    if (xml_Med.Trim() != "")
                    {
                        baseResultBool = SendOrderToNC(url, xml_Med, "销售", true, "XML");
                        if (!baseResultBool.success)
                        {
                            return baseResultBool;
                        }
                    }
                    string xml_NoMed = GetSendXml_XS(pk_corp, reaCenOrgDH, hRDept, confirmEmp, orderDoc, orderDtlList, list_NoMed);
                    if (xml_NoMed.Trim() != "")
                    {
                        baseResultBool = SendOrderToNC(url, xml_NoMed, "销售", false, "XML");
                        if (!baseResultBool.success)
                        {
                            return baseResultBool;
                        }
                    }
                    #endregion
                }
                else if (reaCenOrgDH.NextBillType == int.Parse(ReaCenOrgNextBillType.共建.Key))
                {
                    #region 订货方类型=共建，调用NC出库申请单服务
                    string url = GetInterfaceUrl(0) + "/uapws/service/nc.mk.crkorder.itfservice.IcrkOrderService";
                    string json_Med = GetSendJson_GJ(pk_corp, reaCenOrgDH, confirmEmp, orderDoc, orderDtlList, list_Med);
                    if (json_Med.Trim() != "")
                    {
                        baseResultBool = SendOrderToNC(url, json_Med, "共建", true, "JSON");
                        if (!baseResultBool.success)
                        {
                            return baseResultBool;
                        }
                    }
                    string json_NoMed = GetSendJson_GJ(pk_corp, reaCenOrgDH, confirmEmp, orderDoc, orderDtlList, list_NoMed);
                    if (json_NoMed.Trim() != "")
                    {
                        baseResultBool = SendOrderToNC(url, json_NoMed, "共建", false, "JSON");
                        if (!baseResultBool.success)
                        {
                            return baseResultBool;
                        }
                    }
                    #endregion
                }
                else if ((reaCenOrgDH.NextBillType == int.Parse(ReaCenOrgNextBillType.调拨.Key)))
                {
                    #region 订货方类型=调拨，调用NC调拨单服务
                    string url = GetInterfaceUrl(2) + "/uapws/service/nc.itf.pub.mk.ser.TranOrderMkService";
                    string json_Med = GetSendJson_DB(pk_corp, reaCenOrgDH, confirmEmp, orderDoc, orderDtlList, list_Med);
                    if (json_Med.Trim() != "")
                    {
                        baseResultBool = SendOrderToNC(url, json_Med, "调拨", true, "JSON");
                        if (!baseResultBool.success)
                        {
                            return baseResultBool;
                        }
                    }
                    string json_NoMed = GetSendJson_DB(pk_corp, reaCenOrgDH, confirmEmp, orderDoc, orderDtlList, list_NoMed);
                    if (json_NoMed.Trim() != "")
                    {
                        baseResultBool = SendOrderToNC(url, json_NoMed, "调拨", false, "JSON");
                        if (!baseResultBool.success)
                        {
                            return baseResultBool;
                        }
                    }
                    #endregion
                }

                #endregion

                string isThirdFlag = baseResultBool.success ? ReaBmsOrderDocThirdFlag.同步成功.Key : ReaBmsOrderDocThirdFlag.同步失败.Key;
                IBReaBmsCenOrderDoc.EditReaBmsCenOrderDocThirdFlag(orderDoc.Id, int.Parse(isThirdFlag));

            }
            catch (Exception ex)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "操作异常：" + ex.Message;
                Log.Error("操作异常->", ex);
            }
            Log.Info("定制插件执行订单上传NC接口结束");
            return baseResultBool;
        }

        /// <summary>
        /// 获取给NC接口发送订单的XML
        /// 销售-销售预订单服务
        /// </summary>
        /// <returns></returns>
        private string GetSendXml_XS(string pk_corp, ReaCenOrg reaCenOrgDH, HRDept hRDept, HREmployee confirmEmp, ReaBmsCenOrderDoc orderDoc, IList<ReaBmsCenOrderDtl> orderDtlList, IList<ReaGoodsOrgLink> linkList)
        {
            if (linkList.Count == 0)
            {
                return "";
            }
            StringBuilder sbXml = new StringBuilder();
            sbXml.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sbXml.Append("<ufinterface roottag=\"so_preorder\" billtype=\"38\" replace =\"Y\" receiver =\"web\" sender =\"web\" isexchange =\"Y\" filename =\"预订单表头.xml\" proc =\"add\" operation =\"req\" > ");
            sbXml.Append("<so_preorder id = \"" + ZhiFang.Common.Public.GUIDHelp.GetGUIDLong() + "\" subdoctype = \"\" >");

            #region so_preorder_head
            sbXml.Append("<so_preorder_head>");
            sbXml.Append("<vreceiptcode></vreceiptcode>");
            sbXml.Append("<ccurrencytypeid>CNY</ccurrencytypeid>");//货币单位
            sbXml.Append("<ccustomerid>" + reaCenOrgDH.MatchCode + "</ccustomerid>");//订货方维护中的 NC编码
            sbXml.Append("<cdeptid></cdeptid>");
            sbXml.Append("<coperatorid>" + confirmEmp.MatchCode + "</coperatorid>");
            sbXml.Append("<creceiptcorpid>" + reaCenOrgDH.MatchCode + "</creceiptcorpid>");
            sbXml.Append("<creceiptcustomerid>" + reaCenOrgDH.MatchCode + "</creceiptcustomerid>");
            sbXml.Append("<csalecorpid>" + hRDept.StandCode + "</csalecorpid>");//供应商销售组织编码
            sbXml.Append("<dbilldate>" + (orderDoc.ConfirmTime == null ? "" : orderDoc.ConfirmTime.Value.ToString("yyyy-MM-dd")) + "</dbilldate>");//订货日期
            sbXml.Append("<dmakedate>" + (orderDoc.ConfirmTime == null ? "" : orderDoc.ConfirmTime.Value.ToString("yyyy-MM-dd")) + "</dmakedate>");//制单日期
            sbXml.Append("<dabatedate></dabatedate>");//失效日期
            sbXml.Append("<fstatus>1</fstatus>");//订单状态,固定值：1
            sbXml.Append("<creceipttype>38</creceipttype>");//单据类型,固定值：38
            sbXml.Append("<vnote></vnote>");
            sbXml.Append("<nexchangeotobrate>1.0</nexchangeotobrate>");//折本汇率,固定值：1.0
            sbXml.Append("<pk_corp>" + pk_corp + "</pk_corp>");//NC公司编码
            sbXml.Append("<vreceiveaddress></vreceiveaddress>");
            sbXml.Append("<vdef1></vdef1>");
            sbXml.Append("<vdef2></vdef2>");
            sbXml.Append("<vdef3></vdef3>");
            sbXml.Append("<vdef4></vdef4>");
            sbXml.Append("<vdef5></vdef5>");
            sbXml.Append("<vdef6></vdef6>");
            sbXml.Append("<vdef7>" + orderDoc.OrderDocNo + "</vdef7>");//智方订货单号
            sbXml.Append("<vdef8></vdef8>");
            sbXml.Append("<vdef10></vdef10>");
            sbXml.Append("<pk_defdoc6></pk_defdoc6>");
            sbXml.Append("<ctransmodeid></ctransmodeid>");
            sbXml.Append("</so_preorder_head>");
            #endregion

            #region so_preorder_body
            sbXml.Append("<so_preorder_body>");
            for (int i = 0; i < linkList.Count; i++)
            {
                var link = linkList[i];
                var l = orderDtlList.Where(p => p.ReaGoodsNo == link.ReaGoods.ReaGoodsNo).ToList();
                var orderDtl = l[0];

                sbXml.Append("<entry>");
                sbXml.Append("<ccurrencytypeid>CNY</ccurrencytypeid>");//货币单位
                sbXml.Append("<cinventoryid>" + link.CenOrgGoodsNo + "</cinventoryid>");
                sbXml.Append("<crowno>" + (i + 1) + "</crowno>");//行号
                sbXml.Append("<cunitid>" + orderDtl.GoodsUnit + "</cunitid>");
                sbXml.Append("<dreceivedate>" + DateTime.Now.ToString("yyyy-MM-dd") + "</dreceivedate>");//计划要货日期
                sbXml.Append("<dsenddate>" + DateTime.Now.ToString("yyyy-MM-dd") + "</dsenddate>");//计划发货日期
                sbXml.Append("<ndiscountrate>100</ndiscountrate>");//整单折扣,默认值100
                sbXml.Append("<nnumber>" + orderDtl.GoodsQty + "</nnumber>");//数量
                sbXml.Append("<vdef2></vdef2>");
                sbXml.Append("<nmny></nmny>");//本币无税金额                
                sbXml.Append("<nprice></nprice>");//本币无税单价
                sbXml.Append("<nnetprice></nnetprice>");//本币无税净价
                sbXml.Append("<ntaxprice></ntaxprice>");//本币含税单价
                sbXml.Append("<ntaxrate></ntaxrate>");//税率,默认空
                sbXml.Append("<frownote></frownote>");
                sbXml.Append("<pk_corp>" + pk_corp + "</pk_corp>");
                sbXml.Append("</entry>");
            }
            sbXml.Append("</so_preorder_body>");
            #endregion

            sbXml.Append("</so_preorder>");
            sbXml.Append("</ufinterface>");
            return sbXml.ToString();
        }

        /// <summary>
        /// 获取给NC接口发送订单的XML
        /// 共建-出库申请单服务
        /// </summary>
        /// <returns></returns>
        private string GetSendJson_GJ(string pk_corp, ReaCenOrg reaCenOrgDH, HREmployee confirmEmp, ReaBmsCenOrderDoc orderDoc, IList<ReaBmsCenOrderDtl> orderDtlList, IList<ReaGoodsOrgLink> linkList)
        {
            if (linkList.Count == 0)
            {
                return "";
            }
            StringBuilder sbJson = new StringBuilder();
            sbJson.Append("{\"data\":[");

            for (int i = 0; i < linkList.Count; i++)
            {
                var link = linkList[i];
                var l = orderDtlList.Where(p => p.ReaGoodsNo == link.ReaGoods.ReaGoodsNo).ToList();
                var orderDtl = l[0];

                sbJson.Append("{");
                sbJson.Append("\"cbizid\":\"" + orderDoc.Confirm + "\",");
                sbJson.Append("\"dbilldate\":\"" + (orderDoc.ConfirmTime == null ? "" : orderDoc.ConfirmTime.Value.ToString("yyyy-MM-dd")) + "\",");//单据日期  
                sbJson.Append("\"operator\":\"" + confirmEmp.MatchCode + "\",");
                sbJson.Append("\"pk_corp\":\"" + pk_corp + "\",");
                sbJson.Append("\"pk_org_id\":\"" + pk_corp + "\",");
                sbJson.Append("\"vnote\":\"" + orderDoc.Confirm + "\",");
                sbJson.Append("\"totalnum\":\"\",");//货品总数
                sbJson.Append("\"recvnotes\":\"" + orderDoc.OrderDocNo + "\",");
                sbJson.Append("\"isentity\":\"是\",");
                sbJson.Append("\"cgphone\":\"" + reaCenOrgDH.Tel + "\",");
                sbJson.Append("\"nextaction\":\"共建\",");//后续处理方式
                sbJson.Append("\"nextbilltype\":\"共建\",");//下游单据类型 
                sbJson.Append("\"custcode\":\"" + reaCenOrgDH.MatchCode + "\",");//订货方
                sbJson.Append("\"invcode\":\"" + link.CenOrgGoodsNo + "\",");//货品编码
                sbJson.Append("\"ts\":\"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\",");
                sbJson.Append("\"planrecivedate\":\"" + DateTime.Now.ToString("yyyy-MM-dd") + "\",");
                sbJson.Append("\"applynum\":\"" + orderDtl.GoodsQty + "\"");
                sbJson.Append("}");

                if (i != orderDtlList.Count - 1)
                {
                    sbJson.Append(",");
                }
            }
            sbJson.Append("]}");
            return sbJson.ToString();
        }

        /// <summary>
        /// 获取给NC接口发送订单的XML
        /// 调拨-调拨单服务
        /// </summary>
        /// <returns></returns>
        private string GetSendJson_DB(string pk_corp, ReaCenOrg reaCenOrgDH, HREmployee confirmEmp, ReaBmsCenOrderDoc orderDoc, IList<ReaBmsCenOrderDtl> orderDtlList, IList<ReaGoodsOrgLink> linkList)
        {
            if (linkList.Count == 0)
            {
                return "";
            }

            StringBuilder sbJson = new StringBuilder();
            sbJson.Append("{\"data\":[");

            for (int i = 0; i < linkList.Count; i++)
            {
                var link = linkList[i];
                var l = orderDtlList.Where(p => p.ReaGoodsNo == link.ReaGoods.ReaGoodsNo).ToList();
                var orderDtl = l[0];

                sbJson.Append("{");
                sbJson.Append("\"bztype\":\"YBZK\",");//业务类型,固定YBZK
                sbJson.Append("\"s_head_id\":\"" + ZhiFang.Common.Public.GUIDHelp.GetGUIDLong() + "\",");
                sbJson.Append("\"pk_corp\":\"" + pk_corp + "\",");
                sbJson.Append("\"coperatorid\":\"" + confirmEmp.MatchCode + "\",");
                sbJson.Append("\"cshlddiliverdate\":\"" + DateTime.Now.ToString("yyyy-MM-dd") + "\",");//应发货日期,默认为提交日期
                sbJson.Append("\"dbilldate\":\"" + DateTime.Now.ToString("yyyy-MM-dd") + "\",");//单据日期,默认为提交日期
                sbJson.Append("\"vshldarrivedate\":\"" + DateTime.Now.ToString("yyyy-MM-dd") + "\",");//应到货日期,默认为提交日期
                sbJson.Append("\"vuserdef1\":\"" + orderDoc.OrderDocNo + "\",");//订货单号
                sbJson.Append("\"vuserdef2\":\"" + orderDoc.LabCName + "\",");//订货方名称
                sbJson.Append("\"invcode\":\"" + link.CenOrgGoodsNo + "\",");//货品编码
                sbJson.Append("\"dshldtransnum\":\"" + orderDtl.GoodsQty + "\",");//订单数量
                sbJson.Append("\"vnote\":\"\"");
                sbJson.Append("}");

                if (i != orderDtlList.Count - 1)
                {
                    sbJson.Append(",");
                }
            }
            sbJson.Append("]}");
            return sbJson.ToString();
        }

        private BaseResultBool SendOrderToNC(string url, string sendInfo, string nextBillType, bool isMed, string dataType)
        {
            BaseResultBool baseResultBool = new BaseResultBool();

            string med = (isMed ? "器械" : "非器械");
            string resultStr = "";

            Log.Info(string.Format("请求NC接口服务开始({0}/{1})", nextBillType, med));
            Log.Info("请求地址：" + url);
            Log.Info("上传NC的订单信息：" + sendInfo);

            switch (nextBillType)
            {
                case "销售":
                    resultStr = WebRequestHelp.Post(sendInfo, dataType, url, ZFPlatformHelp.TIME_OUT_MILLISECOND);

                    //解析返回值，失败需要返回
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(resultStr);
                    string resultcode = doc.SelectSingleNode("//sendresult/resultcode").InnerText;
                    string resultdescription = doc.SelectSingleNode("//sendresult/resultdescription").InnerText;
                    if (resultcode.Trim() != "1")
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "订单发送成功，NC返回错误：" + resultdescription;
                    }
                    break;
                case "共建":
                    IcrkOrderService icrkOrderService = new IcrkOrderService();
                    icrkOrderService.Url = url;
                    resultStr = icrkOrderService.insertCrkOrder(sendInfo);

                    //解析返回值，失败需要返回
                    JObject jo = (JObject)JsonConvert.DeserializeObject(resultStr);
                    string result = jo["result"].ToString();
                    string result_msg = jo["result_msg"].ToString();
                    if (result.Trim() != "1")
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "订单发送成功，NC返回错误：" + result_msg;
                    }
                    break;
                case "调拨":
                    TranOrderMkService tranOrderMkService = new TranOrderMkService();
                    tranOrderMkService.Url = url;
                    resultStr = tranOrderMkService.doCreateTranOrder(sendInfo);

                    //解析返回值，失败需要返回
                    jo = (JObject)JsonConvert.DeserializeObject(resultStr);
                    result = jo["result"].ToString();
                    result_msg = jo["result_msg"].ToString();
                    if (result.Trim() != "1")
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "订单发送成功，NC返回错误：" + result_msg;
                    }
                    break;
            }

            Log.Info(string.Format("请求NC接口服务结束({0}/{1})，NC接口返回：{2}", nextBillType, med, resultStr));
            return baseResultBool;
        }

        #endregion

        #region 获取NC出库单，写入到智方试剂平台

        /// <summary>
        /// 调用NC接口，获取出库单
        /// 写入到供货单和供货明细单表
        /// </summary>
        /// <param name="ht"></param>
        /// <returns></returns>
        public BaseResultBool GetOutOrderInfoByInterface(Hashtable ht)
        {
            Log.Info("进入到定制插件NC.SCDJ，GetOutOrderInfoByInterface，执行调用NC接口获取出库单开始");
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                long empID = long.Parse(ht["EmpID"].ToString());
                string employeeName = ht["EmployeeName"].ToString();
                string ncbillno = ht["NcBillNo"].ToString();//NC出库单号
                HRDept hRDept = (HRDept)ht["HRDept"];
                Log.Info("NC出库单号=" + ncbillno);
                Log.Info("当前供应商平台编码=" + hRDept.UseCode + "，销售组织编码=" + hRDept.StandCode + "，物资对照码=" + hRDept.MatchCode);

                IApplicationContext context = ContextRegistry.GetContext();
                IBReaGoods IBReaGoods = (IBReaGoods)context.GetObject("BReaGoods");
                IBReaCenOrg IBReaCenOrg = (IBReaCenOrg)context.GetObject("BReaCenOrg");
                IBReaBmsCenOrderDoc IBReaBmsCenOrderDoc = (IBReaBmsCenOrderDoc)context.GetObject("BReaBmsCenOrderDoc");
                IBReaBmsCenSaleDoc IBReaBmsCenSaleDoc = (IBReaBmsCenSaleDoc)context.GetObject("BReaBmsCenSaleDoc");
                IBReaBmsCenOrderDtl IBReaBmsCenOrderDtl = (IBReaBmsCenOrderDtl)context.GetObject("BReaBmsCenOrderDtl");
                IBReaBmsCenSaleDtl IBReaBmsCenSaleDtl = (IBReaBmsCenSaleDtl)context.GetObject("BReaBmsCenSaleDtl");
                IBReaGoodsOrgLink IBReaGoodsOrgLink = (IBReaGoodsOrgLink)context.GetObject("BReaGoodsOrgLink");

                #region 根据NC出库单号查询库里是否存在供货单和明细，存在则整单拒绝
                IList<ReaBmsCenSaleDoc> exitsSaleDocList = IBReaBmsCenSaleDoc.SearchListByHQL(string.Format("SaleDocNo='{0}'", ncbillno));
                IList<ReaBmsCenSaleDtl> exitsSaleDtlList = new List<ReaBmsCenSaleDtl>();
                if (exitsSaleDocList.Count > 0)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = string.Format("NC出库单号={0}的供货单已存在！", ncbillno);
                    return baseResultBool;

                    if (exitsSaleDocList[0].IOFlag == int.Parse(ReaBmsCenSaleDocIOFlag.已提取.Key))
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = string.Format("NC出库单号={0}的供货单已供货验收提取！", ncbillno);
                        return baseResultBool;
                    }
                    exitsSaleDtlList = IBReaBmsCenSaleDtl.SearchListByHQL(string.Format("SaleDocID={0}", exitsSaleDocList[0].Id));
                }
                #endregion

                #region 请求NC接口获取出库单，并解析到对象List中
                Log.Info("请求NC接口服务开始");
                string url = GetInterfaceUrl(3) + "/api/Domain/univer/dbbus/search";
                Log.Info("地址：" + url);
                string data = "{\"source\":\"greatmaster\",\"tag\":\"v_ic_ghjk\",\"ncbillno\":\"" + ncbillno + "\"}";
                Log.Info("参数data：" + data);
                System.Collections.Specialized.NameValueCollection VarPost = new System.Collections.Specialized.NameValueCollection();
                VarPost.Add("data", data);
                string resultStr = WebRequestHelp.WebClientPost(url, VarPost);
                Log.Info("请求NC接口服务结束，NC接口返回：" + resultStr);
                if (string.IsNullOrWhiteSpace(resultStr))
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "NC返回货品信息为空，无法保存";
                    return baseResultBool;
                }
                //解析平台返回的json，转换到对象List里
                List<NcOutOrder> tempNcOutOrderList = new List<NcOutOrder>();
                JObject jo = (JObject)JsonConvert.DeserializeObject(resultStr);
                string result = jo["result"].ToString();
                if (result != "")
                {
                    //去除JSON字符串中的回车，换行符，制表符
                    result = result.Replace("\n", "").Replace("\t", "").Replace("\r", "");
                    JObject jobj = (JObject)JsonConvert.DeserializeObject(result);
                    if (jobj["result"].ToString() != "")
                    {
                        JArray arr = JArray.Parse(jobj["result"].ToString());
                        tempNcOutOrderList = arr.ToObject<List<NcOutOrder>>();
                    }
                }
                if (tempNcOutOrderList.Count == 0)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "NC返回的出库单信息为空！";
                    return baseResultBool;
                }
                tempNcOutOrderList = tempNcOutOrderList.Where(p => p.GHS_ID == hRDept.MatchCode).ToList();
                if (tempNcOutOrderList.Count == 0)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "NC返回的出库单信息，根据供应商[GHS_ID=" + hRDept.MatchCode + "]过滤后，信息为空！无法保存！";
                    return baseResultBool;
                }
                #endregion

                #region 获取订货方实体对象
                var orgDHList = IBReaCenOrg.SearchListByHQL(string.Format("OrgType=1 and Visible=1 and CName='{0}'", tempNcOutOrderList[0].DHF_NAME));
                Log.Info("获取订货方，根据条件：" + string.Format("OrgType=1 and Visible=1 and CName='{0}'", tempNcOutOrderList[0].DHF_NAME) + "从表Rea_CenOrg里获取。");
                if (orgDHList.Count == 0)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = string.Format("根据订货方[DHF_NAME={0}]未能在试剂平台获取到订货方信息，无法保存！", tempNcOutOrderList[0].DHF_NAME);
                    return baseResultBool;
                }
                ReaCenOrg reaCenOrgDH = orgDHList[0];
                #endregion

                #region 遍历NC返回的货品，只要有一个货品未对照，整单拒绝
                //根据当前出库单的货品，获取库里的试剂货品
                string[] arrGoods = tempNcOutOrderList.Select(p => p.INVCODE).ToArray();
                string cenGoodsNo = string.Join(",", arrGoods);
                cenGoodsNo = "'" + cenGoodsNo.Replace(",", "','") + "'";
                IList<ReaGoodsOrgLink> linkList = IBReaGoodsOrgLink.SearchListByHQL(string.Format("reagoodsorglink.CenOrg.OrgType=1 and reagoodsorglink.Visible=1 and reagoodsorglink.CenOrg.Id={0} and CenOrgGoodsNo in ({1})", reaCenOrgDH.Id, cenGoodsNo));
                foreach (var ncOutOrder in tempNcOutOrderList)
                {
                    var currentGoods = linkList.Where(p => p.CenOrgGoodsNo == ncOutOrder.INVCODE).ToList();
                    if (currentGoods.Count == 0)
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = string.Format("NC货品编码={0}，在试剂平台未获取到对应的货品信息！请先进行货品同步和订货方货品关系设置！", ncOutOrder.INVCODE);
                        return baseResultBool;
                    }
                }
                #endregion

                #region 根据智方订单号获取订单和订单明细列表
                //智方订单号
                string zfBillNo = tempNcOutOrderList[0].ZF_BILLNO;
                if (string.IsNullOrWhiteSpace(zfBillNo))
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "字段[ZF_BILLNO]ZF订货单号，不能为空！";
                    return baseResultBool;
                }
                //根据智方订单号获取订单和订单明细
                IList<ReaBmsCenOrderDoc> orderDocList = IBReaBmsCenOrderDoc.SearchListByHQL(string.Format("OrderDocNo='{0}'", tempNcOutOrderList[0].ZF_BILLNO));
                if (orderDocList.Count == 0)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = string.Format("根据ZF订货单号[ZF_BILLNO={0}]未能在试剂平台获取到订单信息，无法保存！", tempNcOutOrderList[0].ZF_BILLNO);
                    return baseResultBool;
                }
                ReaBmsCenOrderDoc orderDoc = orderDocList[0];
                IList<ReaBmsCenOrderDtl> orderDtlList = IBReaBmsCenOrderDtl.SearchListByHQL(string.Format("OrderDocID={0}", orderDoc.Id));
                #endregion

                //保存到供货单和明细表
                Log.Info("保存供货单主单开始，NC单据号=" + ncbillno + "，ZF订货单号=" + zfBillNo);
                bool b = false;
                #region 保存供货单主单
                ReaBmsCenSaleDoc reaBmsCenSaleDoc = new ReaBmsCenSaleDoc();
                if (exitsSaleDocList.Count > 0)
                {
                    Log.Info(string.Format("NC单据号={0}的供货单已经存在，且未提取，做修改保存处理！", ncbillno));
                    reaBmsCenSaleDoc = exitsSaleDocList[0];
                    GetReaBmsCenSaleDoc(ncbillno, orderDoc, tempNcOutOrderList, reaCenOrgDH, hRDept, empID, employeeName, reaBmsCenSaleDoc, false);
                    IBReaBmsCenSaleDoc.Entity = reaBmsCenSaleDoc;
                    b = IBReaBmsCenSaleDoc.Edit();
                }
                else
                {
                    Log.Info(string.Format("NC单据号={0}的供货单不存在，做新增保存处理！", ncbillno));
                    GetReaBmsCenSaleDoc(ncbillno, orderDoc, tempNcOutOrderList, reaCenOrgDH, hRDept, empID, employeeName, reaBmsCenSaleDoc, true);
                    IBReaBmsCenSaleDoc.Entity = reaBmsCenSaleDoc;
                    b = IBReaBmsCenSaleDoc.Add();
                }
                #endregion
                if (b)
                {
                    Log.Info("保存供货单主单结束，保存成功！");
                    #region 保存供货单明细
                    Log.Info("保存供货单明细开始");
                    IList<ReaBmsCenSaleDtl> saleDtlList = new List<ReaBmsCenSaleDtl>();
                    foreach (var ncOutOrder in tempNcOutOrderList)
                    {
                        var currentGoods = linkList.Where(p => p.CenOrgGoodsNo == ncOutOrder.INVCODE).ToList();
                        ReaBmsCenSaleDtl reaBmsCenSaleDtl = new ReaBmsCenSaleDtl();
                        var exitsGoodsList = exitsSaleDtlList.Where(p => p.ReaGoodsID == currentGoods[0].ReaGoods.Id).ToList();
                        if (exitsGoodsList.Count > 0)
                        {
                            reaBmsCenSaleDtl = exitsGoodsList[0];
                            GetReaBmsCenSaleDtl(reaBmsCenSaleDoc, ncOutOrder, currentGoods[0], IBReaGoodsOrgLink, orderDtlList, reaBmsCenSaleDtl, false, reaCenOrgDH.Id);
                            IBReaBmsCenSaleDtl.Entity = reaBmsCenSaleDtl;
                            b = IBReaBmsCenSaleDtl.Edit();
                            Log.Info(string.Format("修改保存供货单明细货品：({0}){1}，保存结果：{2}", currentGoods[0].ReaGoods.ReaGoodsNo, currentGoods[0].ReaGoods.CName, b.ToString()));
                        }
                        else
                        {
                            GetReaBmsCenSaleDtl(reaBmsCenSaleDoc, ncOutOrder, currentGoods[0], IBReaGoodsOrgLink, orderDtlList, reaBmsCenSaleDtl, true, reaCenOrgDH.Id);
                            IBReaBmsCenSaleDtl.Entity = reaBmsCenSaleDtl;
                            b = IBReaBmsCenSaleDtl.Add();
                            if (b)
                            {
                                saleDtlList.Add(IBReaBmsCenSaleDtl.Entity);
                            }
                            Log.Info(string.Format("新增保存供货单明细货品：({0}){1}，保存结果：{2}", currentGoods[0].ReaGoods.ReaGoodsNo, currentGoods[0].ReaGoods.CName, b.ToString()));
                        }
                    }
                    Log.Info("保存供货单明细结束");
                    #endregion

                    //计算并更新订单里的已供数量、未供数量，更新订单供货状态
                    EditOrderDtlQty(reaBmsCenSaleDoc, saleDtlList, IBReaBmsCenSaleDoc, IBReaBmsCenSaleDtl);

                    //生成条码
                    IBReaBmsCenSaleDtl.AddCreateBarcodeInfoOfSaleDocId(reaBmsCenSaleDoc.Id, empID, employeeName);
                }
                else
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "供货单主单保存失败！";
                    Log.Info("保存供货单主单结束，保存失败！");
                }
            }
            catch (Exception ex)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "操作异常：" + ex.Message;
                Log.Error("操作异常->", ex);
            }
            Log.Info("定制插件执行调用NC接口获取出库单结束");
            return baseResultBool;
        }

        private void GetReaBmsCenSaleDoc(string ncbillno, ReaBmsCenOrderDoc orderDoc, List<NcOutOrder> tempNcOutOrderList, ReaCenOrg reaCenOrgDH, HRDept hRDeptGH, long empID, string employeeName, ReaBmsCenSaleDoc reaBmsCenSaleDoc, bool isAdd)
        {
            if (isAdd)
            {
                reaBmsCenSaleDoc.LabID = orderDoc.LabID;
                reaBmsCenSaleDoc.SaleDocNo = ncbillno;//供货单号，取NC的出库单号
                reaBmsCenSaleDoc.OtherDocNo = ncbillno;
                reaBmsCenSaleDoc.OrderDocNo = tempNcOutOrderList[0].ZF_BILLNO;
                reaBmsCenSaleDoc.OrderDocID = orderDoc.Id;
                reaBmsCenSaleDoc.Source = int.Parse(ReaBmsCenSaleDocSource.供应商.Key);

                //订货方信息
                reaBmsCenSaleDoc.LabcID = reaCenOrgDH.Id;
                reaBmsCenSaleDoc.ReaServerLabcCode = reaCenOrgDH.PlatformOrgNo.ToString();
                reaBmsCenSaleDoc.LabcName = reaCenOrgDH.CName;
                //供应商信息
                reaBmsCenSaleDoc.ReaCompID = orderDoc.ReaCompID;
                reaBmsCenSaleDoc.CompID = orderDoc.CompID;
                reaBmsCenSaleDoc.CompanyName = orderDoc.ReaCompanyName;
                reaBmsCenSaleDoc.ReaCompCode = orderDoc.ReaCompCode;
                reaBmsCenSaleDoc.ReaServerCompCode = hRDeptGH.UseCode;//此处这样简单直接取部门的平台编码UseCode；应该是根据HR_Dept.UseCode查询Cen_Org表，取OrgNo作为ReaServerCompCode；
                reaBmsCenSaleDoc.ReaCompanyName = orderDoc.ReaCompanyName;

                reaBmsCenSaleDoc.DataAddTime = DateTime.Now;
            }
            else
            {
                reaBmsCenSaleDoc.DataUpdateTime = DateTime.Now;
            }
            reaBmsCenSaleDoc.TotalPrice = tempNcOutOrderList.Sum(p => p.NMNY);//总价
            reaBmsCenSaleDoc.Status = int.Parse(ReaBmsCenSaleDocAndDtlStatus.审核通过.Key);
            reaBmsCenSaleDoc.StatusName = ReaBmsCenSaleDocAndDtlStatus.GetStatusDic()[ReaBmsCenSaleDocAndDtlStatus.审核通过.Key].Name;
            reaBmsCenSaleDoc.IOFlag = int.Parse(ReaBmsCenSaleDocIOFlag.未提取.Key);//默认=未提取，供货验收入库后改为已提取
            reaBmsCenSaleDoc.UserID = empID;
            reaBmsCenSaleDoc.UserName = employeeName;
            reaBmsCenSaleDoc.OperDate = DateTime.Now;
            reaBmsCenSaleDoc.Checker = employeeName;
            reaBmsCenSaleDoc.CheckTime = DateTime.Now;
        }

        private void GetReaBmsCenSaleDtl(ReaBmsCenSaleDoc reaBmsCenSaleDoc, NcOutOrder ncOutOrder, ReaGoodsOrgLink currentGoods, IBReaGoodsOrgLink IBReaGoodsOrgLink, IList<ReaBmsCenOrderDtl> orderDtlList, ReaBmsCenSaleDtl reaBmsCenSaleDtl, bool isAdd, long dingHuoFangID)
        {
            if (isAdd)
            {
                reaBmsCenSaleDtl.LabID = reaBmsCenSaleDoc.LabID;
                reaBmsCenSaleDtl.SaleDtlNo = GetDocNo();
                reaBmsCenSaleDtl.SaleDocID = reaBmsCenSaleDoc.Id;
                reaBmsCenSaleDtl.SaleDocNo = reaBmsCenSaleDoc.SaleDocNo;
                reaBmsCenSaleDtl.ReaCompID = reaBmsCenSaleDoc.ReaCompID;
                reaBmsCenSaleDtl.ReaCompanyName = reaBmsCenSaleDoc.CompanyName;
                reaBmsCenSaleDtl.ReaServerCompCode = reaBmsCenSaleDoc.ReaServerCompCode;
                reaBmsCenSaleDtl.ReaGoodsID = currentGoods.ReaGoods.Id;
                reaBmsCenSaleDtl.ReaGoodsName = currentGoods.ReaGoods.CName;
                reaBmsCenSaleDtl.ReaGoodsNo = currentGoods.ReaGoods.ReaGoodsNo;
                reaBmsCenSaleDtl.GoodsID = currentGoods.Id;
                reaBmsCenSaleDtl.GoodsName = currentGoods.ReaGoods.CName;
                reaBmsCenSaleDtl.GoodsNo = currentGoods.ReaGoods.ReaGoodsNo;
                reaBmsCenSaleDtl.CenOrgGoodsNo = currentGoods.ReaGoods.ReaGoodsNo;
                reaBmsCenSaleDtl.IsPrintBarCode = currentGoods.IsPrintBarCode;
                reaBmsCenSaleDtl.BarCodeType = currentGoods.ReaGoods.BarCodeMgr;                
                reaBmsCenSaleDtl.DataAddTime = DateTime.Now;

                var tempList = orderDtlList.Where(p => p.ReaGoodsID == currentGoods.Id).ToList();
                if (tempList.Count > 0)
                {
                    reaBmsCenSaleDtl.LabOrderDtlID = tempList[0].Id;
                }

                //使用订货方查询，订货方货品关系ID
                IList<ReaGoodsOrgLink> tempReaGoodsOrgLinkList = IBReaGoodsOrgLink.SearchListByHQL(string.Format(" reagoodsorglink.CenOrg.OrgType=1 and reagoodsorglink.Visible=1 and reagoodsorglink.ReaGoods.Id={0} and reagoodsorglink.CenOrg.Id={1}", currentGoods.ReaGoods.Id, dingHuoFangID));
                if (tempReaGoodsOrgLinkList.Count > 0)
                {
                    ReaGoodsOrgLink reaGoodsOrgLink = tempReaGoodsOrgLinkList[0];
                    reaBmsCenSaleDtl.CompGoodsLinkID = reaGoodsOrgLink.Id;
                }
            }
            else
            {
                reaBmsCenSaleDtl.DataUpdateTime = DateTime.Now;
            }
            
            reaBmsCenSaleDtl.Status = int.Parse(ReaBmsCenSaleDocAndDtlStatus.审核通过.Key);
            reaBmsCenSaleDtl.StatusName = ReaBmsCenSaleDocAndDtlStatus.GetStatusDic()[ReaBmsCenSaleDocAndDtlStatus.审核通过.Key].Name;
            reaBmsCenSaleDtl.IOFlag = int.Parse(ReaBmsCenSaleDocIOFlag.未提取.Key);//默认=未提取，供货验收入库后改为已提取
            reaBmsCenSaleDtl.ProdOrgName = ncOutOrder.INVPINPAI;
            reaBmsCenSaleDtl.GoodsUnit = ncOutOrder.INVSPEC;
            reaBmsCenSaleDtl.UnitMemo = ncOutOrder.MEASNAME;
            reaBmsCenSaleDtl.GoodsQty = (string.IsNullOrWhiteSpace(ncOutOrder.NOUTNUM) ? 0 : double.Parse(ncOutOrder.NOUTNUM));
            reaBmsCenSaleDtl.Price = ncOutOrder.NPRICE;
            reaBmsCenSaleDtl.SumTotal = ncOutOrder.NMNY; //(reaBmsCenSaleDtl.Price * reaBmsCenSaleDtl.GoodsQty);
            reaBmsCenSaleDtl.LotNo = ncOutOrder.VBATCHCODE;
            reaBmsCenSaleDtl.ProdDate = ncOutOrder.DPRODUCEDATE;
            reaBmsCenSaleDtl.InvalidDate = ncOutOrder.DVALIDATE;
        }

        /// <summary>
        /// 计算订单的已供数量、未供数量
        /// </summary>
        private void EditOrderDtlQty(ReaBmsCenSaleDoc reaBmsCenSaleDoc, IList<ReaBmsCenSaleDtl> saleDtlList, IBReaBmsCenSaleDoc IBReaBmsCenSaleDoc, IBReaBmsCenSaleDtl IBReaBmsCenSaleDtl)
        {
            string errorInfo = "";
            try
            {
                IApplicationContext context = ContextRegistry.GetContext();
                IDReaBmsCenOrderDocDao IDReaBmsCenOrderDocDao = (IDReaBmsCenOrderDocDao)context.GetObject("ReaBmsCenOrderDocDao");
                IDReaBmsCenOrderDtlDao IDReaBmsCenOrderDtlDao = (IDReaBmsCenOrderDtlDao)context.GetObject("ReaBmsCenOrderDtlDao");

                //获取库里当前订单对应的所有的供单（2确认提交、4审核通过、6供货提取、7部分验收、8全部验收）
                string where = string.Format("Status in (2,4,6,7,8) and OrderDocNo='{0}'", reaBmsCenSaleDoc.OrderDocNo);
                IList<ReaBmsCenSaleDoc> tempSaleDocList = IBReaBmsCenSaleDoc.SearchListByHQL(where);
                //根据供单Id，查询供单明细列表
                string saleDocIDs = string.Join(",", tempSaleDocList.Select(p => p.Id).Distinct().ToArray());
                IList<ReaBmsCenSaleDtl> tempSaleDtlList = new List<ReaBmsCenSaleDtl>();
                if (saleDocIDs.Trim() != "")
                {
                    tempSaleDtlList = IBReaBmsCenSaleDtl.SearchListByHQL(string.Format("SaleDocID in ({0})", saleDocIDs));
                    //根据货品编码分组，供货数量求和
                    tempSaleDtlList = tempSaleDtlList.GroupBy(p => new { p.ReaGoodsID }).Select(g => new ReaBmsCenSaleDtl { ReaGoodsID = g.Key.ReaGoodsID, GoodsQty = g.Sum(k => k.GoodsQty) }).ToList();
                }

                int supplyStatus = 0;//供货状态
                long orderDocID = 0;//订单表主键ID
                int suppliedQtyCount = 0;//已供数量=0的计数

                //获取订单里的货品列表
                IList<ReaBmsCenOrderDtl> tempOrderDtlList = IDReaBmsCenOrderDtlDao.GetListByHQL(string.Format("OrderDocNo='{0}'", reaBmsCenSaleDoc.OrderDocNo));
                foreach (var orderDtl in tempOrderDtlList)
                {
                    orderDocID = orderDtl.OrderDocID;
                    double GoodsQty = orderDtl.GoodsQty.Value;//订货数量
                    double SuppliedQty = 0;//已供数量
                    double UnSuppliedQty = 0;//未供数量

                    var l1 = tempSaleDtlList.Where(p => p.ReaGoodsID == orderDtl.ReaGoodsID).ToList();
                    SuppliedQty = (l1.Count() > 0 ? l1[0].GoodsQty.Value : 0);//已供数量=库里的已供数量
                    if (SuppliedQty > GoodsQty)
                    {
                        //已供数量>订货数量，中断操作，给出提示
                        errorInfo = "货品：(" + orderDtl.ReaGoodsNo + ")" + orderDtl.ReaGoodsName + "的供货数量已超过订货数量，不能保存！";
                        break;
                    }

                    //未供数量=订货数量-已供数量
                    UnSuppliedQty = GoodsQty - SuppliedQty;
                    if (UnSuppliedQty > 0)
                    {
                        //存在未供数量>0的货品，则整单的供货状态=部分供货
                        supplyStatus = int.Parse(ReaBmsOrderDocSupplyStatus.部分供货.Key);
                    }

                    //已供数量=0，则计数，如果已供数量=0的计数和订单的货品数量一致，则供货状态=未供货
                    if (SuppliedQty == 0)
                    {
                        suppliedQtyCount++;
                    }

                    //实体赋值
                    orderDtl.SuppliedQty = SuppliedQty;
                    orderDtl.UnSupplyQty = UnSuppliedQty;
                }

                if (errorInfo == "")
                {
                    if (supplyStatus == 0)
                    {
                        supplyStatus = int.Parse(ReaBmsOrderDocSupplyStatus.全部供货.Key);
                    }

                    if (suppliedQtyCount == tempOrderDtlList.Count())
                    {
                        supplyStatus = int.Parse(ReaBmsOrderDocSupplyStatus.未供货.Key);
                    }

                    //更新订单表状态、订单明细数量信息
                    IDReaBmsCenOrderDocDao.UpdateByHql(string.Format("update ReaBmsCenOrderDoc t set t.SupplyStatus={1},t.DataUpdateTime='{2}' where t.Id={0}", orderDocID, supplyStatus, DateTime.Now));
                    foreach (var orderDtl in tempOrderDtlList)
                    {
                        IDReaBmsCenOrderDtlDao.UpdateByHql(string.Format("update ReaBmsCenOrderDtl t set t.SuppliedQty={1},t.UnSupplyQty={2},t.DataUpdateTime='{3}' where t.Id={0}", orderDtl.Id, orderDtl.SuppliedQty, orderDtl.UnSupplyQty, DateTime.Now));
                    }
                }
                else
                {
                    Log.Error("更新订单状态和已供数量、未供数量出错：" + errorInfo);
                }

            }
            catch (Exception ex)
            {
                Log.Error("EditOrderDtlQty操作异常->", ex);
            }
        }

        /// <summary>
        /// 获取单号
        /// </summary>
        /// <returns></returns>
        private string GetDocNo()
        {
            StringBuilder strb = new StringBuilder();
            strb.Append(DateTime.Now.ToString("yyMMdd"));

            Random ran = new Random(Guid.NewGuid().GetHashCode());//使用GUID的随机6位，普通的在毫秒内会重现重复的随机数，导致订单号一样。
            int randKey = ran.Next(0, 999999);
            strb.Append(randKey.ToString().PadLeft(6, '0'));//左补零
            strb.Append(DateTime.Now.ToString("HHmmssfff"));

            ZhiFang.Common.Log.Log.Info("动态生成单号DocNo=" + strb.ToString());
            return strb.ToString();
        }

        #endregion

        #region 上传出库单到NC
        /// <summary>
        /// 给第三方接口发送出库单信息
        /// </summary>
        public BaseResultBool SendOutInfoByInterface(Hashtable ht)
        {
            Log.Info("进入到定制插件NC.SCDJ，SendOutInfoByInterface，执行出库单上传NC接口开始");
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IApplicationContext context = ContextRegistry.GetContext();
                //IBReaGoods IBReaGoods = (IBReaGoods)context.GetObject("BReaGoods");
                //IDHRDeptDao IDHRDept = (IDHRDeptDao)context.GetObject("HRDeptDao");
                //IDReaGoodsOrgLinkDao IDReaGoodsOrgLinkDao = (IDReaGoodsOrgLinkDao)context.GetObject("ReaGoodsOrgLinkDao");
                IDHREmployeeDao IDHREmployeeDao = (IDHREmployeeDao)context.GetObject("HREmployeeDao");
                IDReaCenOrgDao IDReaCenOrgDao = (IDReaCenOrgDao)context.GetObject("ReaCenOrgDao");
                IBReaBmsOutDoc IBReaBmsOutDoc = (IBReaBmsOutDoc)context.GetObject("BReaBmsOutDoc");
                IBReaBmsOutDtl IBReaBmsOutDtl = (IBReaBmsOutDtl)context.GetObject("BReaBmsOutDtl");
                

                //NC分公司的编码
                HRDept hRDept = (HRDept)ht["HRDept"];
                string pk_corp = hRDept.MatchCode;
                Log.Info("NC分公司编码=" + pk_corp);
                long outDocId = (long)ht["OutDocId"];

                //获取出库单
                ReaBmsOutDoc reaBmsOutDoc = (ReaBmsOutDoc)ht["ReaBmsOutDoc"];

                //获取出库单明细
                IList<ReaBmsOutDtl> tempOutDtlList = IBReaBmsOutDtl.GetListByHQL(string.Format("reabmsoutdtl.OutDocID={0} and reabmsoutdtl.ReaServerCompCode='{1}'", outDocId, hRDept.UseCode), "", 0, 0, false).list;
                if (tempOutDtlList == null || tempOutDtlList.Count == 0)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "出库单同步NC平台失败：根据出库单ID" + outDocId.ToString() + "未能获取到出库明细信息";
                    return baseResultBool;
                }
                
                //获取订货方，根据PlatformOrgNo+LabID，在试剂平台库里找
                IList<ReaCenOrg> reaCenOrgDHList = IDReaCenOrgDao.GetListByHQL(string.Format("PlatformOrgNo={0}", reaBmsOutDoc.ReaServerLabcCode));
                if (reaCenOrgDHList.Count == 0)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = string.Format("根据订货方平台机构码={0}未查询到订货方信息！", reaBmsOutDoc.ReaServerLabcCode);
                    Log.Error(baseResultBool.ErrorInfo);
                    return baseResultBool;
                }
                ReaCenOrg reaCenOrgDH = reaCenOrgDHList[0];
                //供应商确认人
                HREmployee supplierConfirmEmp = IDHREmployeeDao.Get(reaBmsOutDoc.SupplierConfirmID.Value);

                //需要将货品按照器械和非器械，分开成两张单子
                var list_Med = tempOutDtlList.Where(p => p.IsMed).ToList();//器械
                var list_NoMed = tempOutDtlList.Where(p => !p.IsMed).ToList();//非器械

                //获取要发送的xml
                string url = GetInterfaceUrl(1) + "/service/XChangeServlet?account=10&receiver=" + pk_corp;
                string xml_Med = GetSendOutInfoXml(pk_corp, reaCenOrgDH, hRDept, supplierConfirmEmp, reaBmsOutDoc, list_Med);
                if (xml_Med != "")
                {
                    baseResultBool = SendOrderToNC(url, xml_Med, "销售", true, "XML");
                    if (!baseResultBool.success)
                    {
                        return baseResultBool;
                    }
                }
                string xml_NoMed = GetSendOutInfoXml(pk_corp, reaCenOrgDH, hRDept, supplierConfirmEmp, reaBmsOutDoc, list_NoMed);
                if (xml_NoMed.Trim() != "")
                {
                    baseResultBool = SendOrderToNC(url, xml_NoMed, "销售", false, "XML");
                    if (!baseResultBool.success)
                    {
                        return baseResultBool;
                    }
                }

                //更新标记
                string isThirdFlag = baseResultBool.success ? ReaBmsOutDocThirdFlag.同步成功.Key : ReaBmsOutDocThirdFlag.同步失败.Key;
                IBReaBmsOutDoc.EditReaBmsOutDocThirdFlag(outDocId, int.Parse(isThirdFlag));

                return baseResultBool;
            }
            catch (Exception ex)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "操作异常：" + ex.Message;
                Log.Error("操作异常->", ex);
            }
            Log.Info("定制插件执行出库单上传NC接口结束");
            return baseResultBool;
        }

        /// <summary>
        /// 获取给NC接口发送出库单的XML
        /// 调用NC的销售预订单服务
        /// </summary>
        /// <returns></returns>
        private string GetSendOutInfoXml(string pk_corp, ReaCenOrg reaCenOrgDH, HRDept hRDept, HREmployee confirmEmp, ReaBmsOutDoc outDoc, IList<ReaBmsOutDtl> outDtlList)
        {
            if (outDtlList.Count == 0)
            {
                return "";
            }
            StringBuilder sbXml = new StringBuilder();
            sbXml.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sbXml.Append("<ufinterface roottag=\"so_preorder\" billtype=\"38\" replace =\"Y\" receiver =\"web\" sender =\"web\" isexchange =\"Y\" filename =\"预订单表头.xml\" proc =\"add\" operation =\"req\" > ");
            sbXml.Append("<so_preorder id = \"" + ZhiFang.Common.Public.GUIDHelp.GetGUIDLong() + "\" subdoctype = \"\" >");

            #region so_preorder_head
            sbXml.Append("<so_preorder_head>");
            sbXml.Append("<vreceiptcode></vreceiptcode>");
            sbXml.Append("<ccurrencytypeid>CNY</ccurrencytypeid>");//货币单位
            sbXml.Append("<ccustomerid>" + reaCenOrgDH.MatchCode + "</ccustomerid>");//订货方维护中的 NC编码
            sbXml.Append("<cdeptid></cdeptid>");
            sbXml.Append("<coperatorid>" + confirmEmp.MatchCode + "</coperatorid>");
            sbXml.Append("<creceiptcorpid>" + reaCenOrgDH.MatchCode + "</creceiptcorpid>");
            sbXml.Append("<creceiptcustomerid>" + reaCenOrgDH.MatchCode + "</creceiptcustomerid>");
            sbXml.Append("<csalecorpid>" + hRDept.StandCode + "</csalecorpid>");//供应商销售组织编码
            sbXml.Append("<dbilldate>" + (outDoc.ConfirmTime == null ? "" : outDoc.ConfirmTime.Value.ToString("yyyy-MM-dd")) + "</dbilldate>");//订货日期
            sbXml.Append("<dmakedate>" + (outDoc.ConfirmTime == null ? "" : outDoc.ConfirmTime.Value.ToString("yyyy-MM-dd")) + "</dmakedate>");//制单日期
            sbXml.Append("<dabatedate></dabatedate>");//失效日期
            sbXml.Append("<fstatus>1</fstatus>");//订单状态,固定值：1
            sbXml.Append("<creceipttype>38</creceipttype>");//单据类型,固定值：38
            sbXml.Append("<vnote></vnote>");
            sbXml.Append("<nexchangeotobrate>1.0</nexchangeotobrate>");//折本汇率,固定值：1.0
            sbXml.Append("<pk_corp>" + pk_corp + "</pk_corp>");//NC公司编码
            sbXml.Append("<vreceiveaddress></vreceiveaddress>");
            sbXml.Append("<vdef1></vdef1>");
            sbXml.Append("<vdef2></vdef2>");
            sbXml.Append("<vdef3></vdef3>");
            sbXml.Append("<vdef4></vdef4>");
            sbXml.Append("<vdef5></vdef5>");
            sbXml.Append("<vdef6></vdef6>");
            sbXml.Append("<vdef7>" + outDoc.OutDocNo + "</vdef7>");//智方出库单号
            sbXml.Append("<vdef8></vdef8>");
            sbXml.Append("<vdef10></vdef10>");
            sbXml.Append("<pk_defdoc6></pk_defdoc6>");
            sbXml.Append("<ctransmodeid></ctransmodeid>");
            sbXml.Append("</so_preorder_head>");
            #endregion

            #region so_preorder_body
            sbXml.Append("<so_preorder_body>");
            for (int i = 0; i < outDtlList.Count; i++)
            {
                var dtl = outDtlList[i];
                sbXml.Append("<entry>");
                sbXml.Append("<ccurrencytypeid>CNY</ccurrencytypeid>");//货币单位
                sbXml.Append("<cinventoryid>" + dtl.CenOrgGoodsNo + "</cinventoryid>");
                sbXml.Append("<crowno>" + (i + 1) + "</crowno>");//行号
                sbXml.Append("<cunitid>" + dtl.GoodsUnit + "</cunitid>");
                sbXml.Append("<dreceivedate>" + DateTime.Now.ToString("yyyy-MM-dd") + "</dreceivedate>");//计划要货日期
                sbXml.Append("<dsenddate>" + DateTime.Now.ToString("yyyy-MM-dd") + "</dsenddate>");//计划发货日期
                sbXml.Append("<ndiscountrate>100</ndiscountrate>");//整单折扣,默认值100
                sbXml.Append("<nnumber>" + dtl.GoodsQty + "</nnumber>");//数量
                sbXml.Append("<vdef2></vdef2>");
                sbXml.Append("<nmny></nmny>");//本币无税金额                
                sbXml.Append("<nprice></nprice>");//本币无税单价
                sbXml.Append("<nnetprice></nnetprice>");//本币无税净价
                sbXml.Append("<ntaxprice></ntaxprice>");//本币含税单价
                sbXml.Append("<ntaxrate></ntaxrate>");//税率,默认空
                sbXml.Append("<frownote></frownote>");
                sbXml.Append("<pk_corp>" + pk_corp + "</pk_corp>");
                sbXml.Append("</entry>");
            }
            sbXml.Append("</so_preorder_body>");
            #endregion

            sbXml.Append("</so_preorder>");
            sbXml.Append("</ufinterface>");
            return sbXml.ToString();
        }
        
        #endregion

        /// <summary>
        /// 获取NC接口地址
        /// 顺序：出库申请单服务-共建|销售预订单接口-销售|销售调拨接口-调拨|供货单服务|基础数据同步接口
        /// </summary>
        /// <param name="index">索引位置</param>
        /// <returns></returns>
        private string GetInterfaceUrl(int index)
        {
            string InterfaceUrl = ConfigHelper.GetConfigString("InterfaceUrl");
            if (InterfaceUrl.IndexOf("|") <= 0 || InterfaceUrl.Split('|').Length != 5)
            {
                throw new Exception("Web.Config文件未配置NC接口地址或配置参数[InterfaceUrl]的值错误，请检查！");
            }
            InterfaceUrl = InterfaceUrl.Split('|')[index];
            return InterfaceUrl;
        }

    }
}
