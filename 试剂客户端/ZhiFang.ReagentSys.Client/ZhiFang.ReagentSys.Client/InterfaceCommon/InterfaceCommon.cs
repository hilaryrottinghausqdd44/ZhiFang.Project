using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Web;
using System.Data;
using Spring.Context;
using Spring.Context.Support;
using ZhiFang.Entity.Common;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.IBLL.RBAC;
using ZhiFang.IBLL.Base;
using ZhiFang.ServiceCommon.RBAC;
using ZhiFang.Common.Public;

namespace ZhiFang.ReagentSys.Client
{
    public class InterfaceCommon
    {
        #region 下述基础数据同步方法XML数据格式，提供给第三方程序使用

        public BaseResultData DeptSyncData(XDocument xml)
        {
            XElement xeRoot = xml.Root;//根目录
            XElement xeBody = xeRoot.Element("Body");
            IList<XElement> xeBodyChildList = xeBody.Elements("Row").ToList();
            return DeptSyncData(xeBodyChildList);
        }

        public BaseResultData DeptSyncData(IList<XElement> xeBodyChildList)
        {
            BaseResultData baseResultData = new BaseResultData();
            if (xeBodyChildList != null && xeBodyChildList.Count > 0)
            {
                IApplicationContext context = ContextRegistry.GetContext();
                IBHRDept IBHRDept = (IBHRDept)context.GetObject("BHRDept");
                Dictionary<string, object> dicFieldAndValue = new Dictionary<string, object>();
                foreach (XElement bodyChild in xeBodyChildList)
                {
                    dicFieldAndValue.Clear();
                    IList<XElement> nodeList = bodyChild.Elements().ToList();
                    if (nodeList != null && nodeList.Count > 0)
                    {
                        foreach (XElement node in nodeList)
                        {
                            dicFieldAndValue.Add(node.Name.ToString().ToLower(), node.Value);
                        }
                        if (!dicFieldAndValue.Keys.Contains("shortcode"))
                            dicFieldAndValue.Add("shortcode", dicFieldAndValue["deptcode"].ToString());
                        if (!dicFieldAndValue.Keys.Contains("standcode"))//标准代码
                            dicFieldAndValue.Add("standcode", dicFieldAndValue["deptcode"].ToString());
                        if (!dicFieldAndValue.Keys.Contains("devecode"))//开发商代码
                            dicFieldAndValue.Add("devecode", dicFieldAndValue["deptcode"].ToString());
                        if (!dicFieldAndValue.Keys.Contains("usecode"))//标准代码
                            dicFieldAndValue.Add("usecode", dicFieldAndValue["deptcode"].ToString());
                        if (!dicFieldAndValue.Keys.Contains("matchcode"))//第三方对照码
                            dicFieldAndValue.Add("matchcode", dicFieldAndValue["deptcode"].ToString());
                        baseResultData.success = IBHRDept.AddHRDeptSyncByInterface("MatchCode", dicFieldAndValue["deptcode"].ToString(), dicFieldAndValue).success;
                    }
                }
            }
            return baseResultData;
        }

        public BaseResultData EmployeeSyncData(XDocument xml)
        {          
            XElement xeRoot = xml.Root;//根目录
            XElement xeBody = xeRoot.Element("Body");
            IList<XElement> xeBodyChildList = xeBody.Elements("Row").ToList();
            return EmployeeSyncData(xeBodyChildList);
        }

        public BaseResultData EmployeeSyncData(IList<XElement> xeBodyChildList)
        {
            BaseResultData baseResultData = new BaseResultData();
            if (xeBodyChildList != null && xeBodyChildList.Count > 0)
            {
                bool IsAddAccount = ConfigHelper.GetConfigBool("IsAddAccount");
                IApplicationContext context = ContextRegistry.GetContext();
                IBHREmployee IBHREmployee = (IBHREmployee)context.GetObject("BHREmployee");
                Dictionary<string, object> dicFieldAndValue = new Dictionary<string, object>();
                foreach (XElement bodyChild in xeBodyChildList)
                {
                    dicFieldAndValue.Clear();
                    IList<XElement> nodeList = bodyChild.Elements().ToList();
                    if (nodeList != null && nodeList.Count > 0)
                    {
                        foreach (XElement node in nodeList)
                        {
                            dicFieldAndValue.Add(node.Name.ToString().ToLower(), node.Value);
                        }

                        if (!dicFieldAndValue.Keys.Contains("empcode") || dicFieldAndValue["empcode"].ToString().Trim() == "")
                        {
                            baseResultData.success = false;
                            baseResultData.message = "XML格式缺少EmpCode节点，或EmpCode节点值为空！";
                            return baseResultData;
                        }

                        if (!dicFieldAndValue.Keys.Contains("shortcode"))
                            dicFieldAndValue.Add("shortcode", dicFieldAndValue["empcode"].ToString());
                        if (!dicFieldAndValue.Keys.Contains("matchcode"))//第三方对照码
                            dicFieldAndValue.Add("matchcode", dicFieldAndValue["empcode"].ToString());
                        if (!dicFieldAndValue.Keys.Contains("standcode"))//标准代码
                            dicFieldAndValue.Add("standcode", dicFieldAndValue["empcode"].ToString());
                        baseResultData.success = IBHREmployee.AddHREmployeeSyncByInterface("MatchCode", dicFieldAndValue["empcode"].ToString(), null, dicFieldAndValue, IsAddAccount).success;
                    }
                }
            }
            return baseResultData;
        }

        public BaseResultData ReaCenOrgSyncData(XDocument xml)
        {            
            XElement xeRoot = xml.Root;//根目录
            XElement xeBody = xeRoot.Element("Body");
            IList<XElement> xeBodyChildList = xeBody.Elements("Row").ToList();
            return ReaCenOrgSyncData( xeBodyChildList);
        }

        public BaseResultData ReaCenOrgSyncData(IList<XElement> xeBodyChildList)
        {
            BaseResultData baseResultData = new BaseResultData();
            if (xeBodyChildList != null && xeBodyChildList.Count > 0)
            {
                IApplicationContext context = ContextRegistry.GetContext();
                IBReaCenOrg IBReaCenOrg = (IBReaCenOrg)context.GetObject("BReaCenOrg");
                Dictionary<string, object> dicFieldAndValue = new Dictionary<string, object>();
                foreach (XElement bodyChild in xeBodyChildList)
                {
                    dicFieldAndValue.Clear();
                    IList<XElement> nodeList = bodyChild.Elements().ToList();
                    if (nodeList != null && nodeList.Count > 0)
                    {
                        foreach (XElement node in nodeList)
                        {
                            dicFieldAndValue.Add(node.Name.ToString().ToLower(), node.Value);
                        }
                        if (!dicFieldAndValue.Keys.Contains("matchcode"))
                            dicFieldAndValue.Add("matchcode", dicFieldAndValue["reacompcode"].ToString());
                        baseResultData.success = IBReaCenOrg.AddReaCenOrgSyncByInterface("MatchCode", dicFieldAndValue["reacompcode"].ToString(), dicFieldAndValue).success;
                    }
                }
            }
            return baseResultData;
        }

        public BaseResultData ReaGoodsSyncData(XDocument xml)
        {   
            XElement xeRoot = xml.Root;//根目录
            XElement xeBody = xeRoot.Element("Body");
            IList<XElement> xeBodyChildList = xeBody.Elements("Row").ToList();
            return ReaGoodsSyncData(xeBodyChildList);    
        }

        public BaseResultData ReaGoodsSyncData(IList<XElement> xeBodyChildList)
        {
            BaseResultData baseResultData = new BaseResultData();
            if (xeBodyChildList != null && xeBodyChildList.Count > 0)
            {
                IApplicationContext context = ContextRegistry.GetContext();
                IBReaGoods IBReaGoods = (IBReaGoods)context.GetObject("BReaGoods");
                
                Dictionary<string, object> dicFieldAndValue = new Dictionary<string, object>();
                foreach (XElement bodyChild in xeBodyChildList)
                {
                    dicFieldAndValue.Clear();
                    IList<XElement> nodeList = bodyChild.Elements().ToList();
                    if (nodeList != null && nodeList.Count > 0)
                    {
                        foreach (XElement node in nodeList)
                        {
                            dicFieldAndValue.Add(node.Name.ToString().ToLower(), node.Value);
                        }
                        if (!dicFieldAndValue.Keys.Contains("reagoodsno"))
                            dicFieldAndValue.Add("reagoodsno", dicFieldAndValue["matchcode"].ToString());
                        else
                            dicFieldAndValue["reagoodsno"] = dicFieldAndValue["matchcode"].ToString();

                        baseResultData.success = IBReaGoods.AddReaGoodsSyncByInterface("MatchCode", dicFieldAndValue["matchcode"].ToString(), dicFieldAndValue).success;

                    }
                }
            }
            return baseResultData;
        }

        public BaseResultData ReaGoodsSyncData(IList<XElement> xeBodyChildList, ref IList<ReaCenOrg> listReaCenOrg, ref IList<ReaGoods> listReaGoods)
        {
            BaseResultData baseResultData = new BaseResultData();
            if (xeBodyChildList != null && xeBodyChildList.Count > 0)
            {
                IApplicationContext context = ContextRegistry.GetContext();
                IBReaGoods IBReaGoods = (IBReaGoods)context.GetObject("BReaGoods");
                Dictionary<string, object> dicFieldAndValue = new Dictionary<string, object>();
                Dictionary<string, ReaCenOrg> dicCompCode = new Dictionary<string, ReaCenOrg>();
                foreach (XElement bodyChild in xeBodyChildList)
                {
                    dicFieldAndValue.Clear();
                    ReaGoods reaGoods = new ReaGoods();
                    ReaCenOrg reaCenOrg = new ReaCenOrg();
                    IList<XElement> nodeList = bodyChild.Elements().ToList();
                    if (nodeList != null && nodeList.Count > 0)
                    {
                        foreach (XElement node in nodeList)
                        {
                            dicFieldAndValue.Add(node.Name.ToString().ToLower(), node.Value);
                        }
                        if (!dicFieldAndValue.Keys.Contains("reagoodsno"))
                            dicFieldAndValue.Add("reagoodsno", dicFieldAndValue["matchcode"].ToString());
                        else
                            dicFieldAndValue["reagoodsno"] = dicFieldAndValue["matchcode"].ToString();
                        baseResultData.success = IBReaGoods.AddReaGoodsSyncByInterface("MatchCode", dicFieldAndValue["matchcode"].ToString(), dicFieldAndValue, ref reaCenOrg, ref reaGoods).success;
                        listReaGoods.Add(reaGoods);
                        if ((!string.IsNullOrEmpty(reaCenOrg.MatchCode)) && (!dicCompCode.ContainsKey(reaCenOrg.MatchCode)))
                            dicCompCode.Add(reaCenOrg.MatchCode, reaCenOrg);
                    }
                }
                foreach (KeyValuePair<string, ReaCenOrg> kv in dicCompCode)
                    listReaCenOrg.Add(kv.Value);
            }
            return baseResultData;
        }

        /// <summary>
        /// 同步货品表
        /// 1. 检查并同步供应商，供应商不存在自动新增
        /// 2. 保存品牌（厂商）
        /// 3. 保存货品
        /// 4. 保存供应商-货品关系
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="emp"></param>
        /// <returns></returns>
        public BaseResultData ReaGoodsSyncData(XDocument xml, HREmployee emp)
        {
            XElement xeRoot = xml.Root;//根目录
            XElement xeBody = xeRoot.Element("Body");
            IList<XElement> xeBodyChildList = xeBody.Elements("Row").ToList();
            return ReaGoodsSyncData(xeBodyChildList, emp);
        }

        public BaseResultData ReaGoodsSyncData(IList<XElement> xeBodyChildList, HREmployee emp)
        {
            BaseResultData baseResultData = new BaseResultData();
            if (xeBodyChildList != null && xeBodyChildList.Count > 0)
            {
                IApplicationContext context = ContextRegistry.GetContext();
                IBReaGoods IBReaGoods = (IBReaGoods)context.GetObject("BReaGoods");
                IBReaCenOrg IBReaCenOrg = (IBReaCenOrg)context.GetObject("BReaCenOrg");
                IBReaGoodsOrgLink IBReaGoodsOrgLink = (IBReaGoodsOrgLink)context.GetObject("BReaGoodsOrgLink");
                IBBDict IBBDict = (IBBDict)context.GetObject("BBDict");

                Dictionary<string, object> dicFieldAndValue = new Dictionary<string, object>();
                foreach (XElement bodyChild in xeBodyChildList)
                {
                    dicFieldAndValue.Clear();
                    IList<XElement> nodeList = bodyChild.Elements().ToList();
                    if (nodeList != null && nodeList.Count > 0)
                    {
                        foreach (XElement node in nodeList)
                        {
                            dicFieldAndValue.Add(node.Name.ToString().ToLower(), node.Value);
                            ZhiFang.Common.Log.Log.Info(string.Format("货品属性：{0}={1}", node.Name, node.Value));
                        }
                        if (!dicFieldAndValue.Keys.Contains("reagoodsno"))
                            dicFieldAndValue.Add("reagoodsno", dicFieldAndValue["matchcode"].ToString());
                        else
                            dicFieldAndValue["reagoodsno"] = dicFieldAndValue["matchcode"].ToString();

                        #region 检查并同步供应商（供应商不存在自动新增）
                        string reaCompCode = dicFieldAndValue["reacompcode"].ToString().Trim();
                        if (reaCompCode == "")
                        {
                            baseResultData.success = false;
                            baseResultData.message = "供应商编码不能为空！";
                            return baseResultData;
                        }
                        var cenOrgList = IBReaCenOrg.SearchListByHQL(string.Format("MatchCode='{0}'", reaCompCode));
                        if (cenOrgList.Count() <= 0)
                        {
                            baseResultData = IBReaCenOrg.AddReaCenOrgSyncByInterface("MatchCode", reaCompCode, new Dictionary<string, object> {
                                        { "CName", dicFieldAndValue["reacompanyname"].ToString() },
                                        { "MatchCode", reaCompCode }
                                });
                        }
                        if (!baseResultData.success)
                        {
                            baseResultData.message = "供应商信息保存失败！";
                            return baseResultData;
                        }
                        #endregion

                        #region 保存品牌（厂商）
                        BDict bDict = null;
                        string prodOrgNo = dicFieldAndValue["prodorgno"].ToString().Trim();
                        string prodOrgName = dicFieldAndValue["prodorgname"].ToString().Trim();
                        if (prodOrgNo != "" && prodOrgName != "")
                        {
                            IBBDict.SaveProdOrgByInterface(prodOrgNo, new Dictionary<string, object> {
                                { "CName", prodOrgName }
                            }, ref bDict);
                        }
                        #endregion

                        #region 保存货品信息
                        if (bDict != null)
                        {
                            //货品信息里填充厂商ID
                            if (!dicFieldAndValue.Keys.Contains("prodid"))
                            {
                                dicFieldAndValue.Add("prodid", bDict.Id);
                            }
                            else
                            {
                                dicFieldAndValue["prodid"] = bDict.Id.ToString();
                            }
                        }
                        baseResultData = IBReaGoods.AddReaGoodsSyncByInterface("MatchCode", dicFieldAndValue["matchcode"].ToString(), dicFieldAndValue);
                        if (!baseResultData.success)
                        {
                            baseResultData.message = "货品信息保存失败！";
                            return baseResultData;
                        }
                        #endregion

                        #region 保存供应商-货品关系表
                        if (emp != null)
                        {
                            IList<ReaGoods> listReaGoods = new List<ReaGoods>();
                            var goodsList = IBReaGoods.SearchListByHQL(string.Format("MatchCode='{0}'", dicFieldAndValue["matchcode"].ToString()));
                            goodsList[0].ReaCompCode = reaCompCode;
                            listReaGoods.Add(goodsList[0]);
                            baseResultData = IBReaGoodsOrgLink.SaveReaGoodsOrgLinkByMatchInterface(listReaGoods, emp.Id, emp.CName);
                            if (!baseResultData.success)
                            {
                                baseResultData.message = "供货货品关系保存失败！" + baseResultData.message;
                                return baseResultData;
                            }
                        }
                        #endregion

                    }
                }
            }
            return baseResultData;
        }

        public BaseResultData ReaStorageSyncData(XDocument xml)
        {
            XElement xeRoot = xml.Root;//根目录
            XElement xeBody = xeRoot.Element("Body");
            IList<XElement> xeBodyChildList = xeBody.Elements("Row").ToList();
            return ReaStorageSyncData(xeBodyChildList);         
        }

        public BaseResultData ReaStorageSyncData(IList<XElement> xeBodyChildList)
        {
            BaseResultData baseResultData = new BaseResultData();
            if (xeBodyChildList != null && xeBodyChildList.Count > 0)
            {
                IApplicationContext context = ContextRegistry.GetContext();
                IBReaStorage IBReaStorage = (IBReaStorage)context.GetObject("BReaStorage");
                Dictionary<string, object> dicFieldAndValue = new Dictionary<string, object>();
                foreach (XElement bodyChild in xeBodyChildList)
                {
                    dicFieldAndValue.Clear();
                    IList<XElement> nodeList = bodyChild.Elements().ToList();
                    if (nodeList != null && nodeList.Count > 0)
                    {
                        foreach (XElement node in nodeList)
                        {
                            dicFieldAndValue.Add(node.Name.ToString().ToLower(), node.Value);
                        }
                        if (!dicFieldAndValue.Keys.Contains("matchcode"))
                            dicFieldAndValue.Add("matchcode", dicFieldAndValue["storagecode"].ToString());
                        baseResultData.success = IBReaStorage.AddReaStorageSyncByInterface("MatchCode", dicFieldAndValue["storagecode"].ToString(), dicFieldAndValue).success;
                    }
                }
            }
            return baseResultData;
        }

        public BaseResultData ReaOrgGoodsSyncData(XDocument xml, HREmployee emp)
        {
            XElement xeRoot = xml.Root;//根目录
            XElement xeBody = xeRoot.Element("Body");
            IList<XElement> xeBodyChildList = xeBody.Elements("Row").ToList();
            return ReaOrgGoodsSyncData(xeBodyChildList, emp);
        }

        public BaseResultData ReaOrgGoodsSyncData(IList<XElement> xeBodyChildList, HREmployee emp)
        {
            BaseResultData baseResultData = new BaseResultData();
            if (xeBodyChildList != null && xeBodyChildList.Count > 0)
            {
                IApplicationContext context = ContextRegistry.GetContext();
                IBReaGoodsOrgLink IBReaGoodsOrgLink = (IBReaGoodsOrgLink)context.GetObject("BReaGoodsOrgLink");
                IBReaGoods IBReaGoods = (IBReaGoods)context.GetObject("BReaGoods");
                Dictionary<string, object> dicFieldAndValue = new Dictionary<string, object>();
                IList<ReaGoods> listReaGoods = new List<ReaGoods>();
                foreach (XElement bodyChild in xeBodyChildList)
                {
                    dicFieldAndValue.Clear();
                    IList<XElement> nodeList = bodyChild.Elements().ToList();
                    if (nodeList != null && nodeList.Count > 0)
                    {
                        foreach (XElement node in nodeList)
                        {
                            dicFieldAndValue.Add(node.Name.ToString().ToLower(), node.Value);
                        }
                        ReaGoods reaGoods = null;
                        if (dicFieldAndValue.Keys.Contains("reagoodsno"))
                        {
                            IList<ReaGoods> tempList = IBReaGoods.SearchListByHQL(" reagoods.ReaGoodsNo=\'" + dicFieldAndValue["reagoodsno"].ToString() + "\'");
                            if (tempList != null && tempList.Count > 0)
                            {
                                tempList = tempList.OrderByDescending(p => p.DataAddTime).ToList();
                                reaGoods = tempList[0];
                            }
                        }
                        if (reaGoods != null)
                        {
                            if (dicFieldAndValue.Keys.Contains("reacompcode"))
                                reaGoods.ReaCompCode = dicFieldAndValue["reacompcode"].ToString();
                            if (dicFieldAndValue.Keys.Contains("memo"))
                                reaGoods.ZDYMemo = dicFieldAndValue["memo"].ToString(); ;
                            listReaGoods.Add(reaGoods);
                        }
                    }
                }
                baseResultData = IBReaGoodsOrgLink.SaveReaGoodsOrgLinkByMatchInterface(listReaGoods, emp.Id, emp.CName);
            }
            return baseResultData;
        }

        #endregion

        #region 接口登录通用方法
        public BaseResultData UserLogin()
        {
            BaseResultData baseResultData = new BaseResultData();
            string userAccount = "";
            string userPassword = "";
            string isValidate = "";
            string serverPath = HttpContext.Current.Server.MapPath("~/");
            string xmlInterfaceUser = serverPath + "\\BaseTableXML\\Interface\\InterfaceUser.xml";
            if (System.IO.File.Exists(xmlInterfaceUser))
            {
                DataSet ds = ZhiFang.Common.Public.XmlToData.XmlFileToDataSet(xmlInterfaceUser);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    userAccount = ds.Tables[0].Rows[0]["UserCode"].ToString();
                    if (ds.Tables[0].Columns.Contains("UserPassword"))
                        userPassword = ds.Tables[0].Rows[0]["UserPassword"].ToString();
                    if (ds.Tables[0].Columns.Contains("isValidate"))
                        isValidate = ds.Tables[0].Rows[0]["isValidate"].ToString();
                    if (string.IsNullOrEmpty(isValidate))
                        isValidate = "1";
                }
                else
                {
                    baseResultData.success = false;
                    baseResultData.message = "获取不到接口登录用户信息！";
                    ZhiFang.Common.Log.Log.Error(baseResultData.message + "：用户配置文件中没有配置用户相关信息");
                    return baseResultData;
                }
                HREmployee emp = null;
                baseResultData.success = RBACLogin(userAccount, userPassword, (isValidate == "1"), ref emp);
            }
            else
            {
                baseResultData.success = false;
                baseResultData.message = "获取不到接口登录用户配置信息！";
                ZhiFang.Common.Log.Log.Error(baseResultData.message + "：用户配置文件InterfaceUser不存在");
            }
            return baseResultData;
        }

        public BaseResultData UserLogin(ref HREmployee emp, ref CenOrg cenOrg)
        {
            BaseResultData baseResultData = new BaseResultData();
            ZhiFang.Common.Log.Log.Info("RS_ReaSaleDocCreate方法接口用户自动登录---开始");
            string userAccount = "";
            string userPassword = "";
            string isValidate = "";
            string serverPath = HttpContext.Current.Server.MapPath("~/");
            string xmlInterfaceUser = serverPath + "\\BaseTableXML\\Interface\\InterfaceUser.xml";
            if (System.IO.File.Exists(xmlInterfaceUser))
            {
                DataSet ds = ZhiFang.Common.Public.XmlToData.XmlFileToDataSet(xmlInterfaceUser);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    userAccount = ds.Tables[0].Rows[0]["UserCode"].ToString();
                    if (ds.Tables[0].Columns.Contains("UserPassword"))
                        userPassword = ds.Tables[0].Rows[0]["UserPassword"].ToString();
                    if (ds.Tables[0].Columns.Contains("isValidate"))
                        isValidate = ds.Tables[0].Rows[0]["isValidate"].ToString();
                    if (string.IsNullOrEmpty(isValidate))
                        isValidate = "1";
                }
                else
                {
                    baseResultData.success = false;
                    baseResultData.message = "获取不到接口登录用户信息！";
                    ZhiFang.Common.Log.Log.Error(baseResultData.message + "：用户配置文件中没有配置用户相关信息");
                    return baseResultData;
                }
                baseResultData.success = RBACLogin(userAccount, userPassword, (isValidate == "1"), ref emp);
                if (emp != null)
                {
                    GetCurUserCenOrg(emp, ref cenOrg);
                    if (cenOrg == null)
                    {
                        baseResultData.success = false;
                        baseResultData.message = "无法获取接口用户机构信息！";
                        ZhiFang.Common.Log.Log.Error(baseResultData.message);
                        return baseResultData;
                    }
                }
                else
                {
                    baseResultData.success = false;
                    baseResultData.message = "无法获取接口用户信息！";
                    ZhiFang.Common.Log.Log.Error(baseResultData.message + "：根据用户账号【" + userAccount + "】获取不到用户相关信息");
                    return baseResultData;
                }
            }
            else
            {
                baseResultData.success = false;
                baseResultData.message = "获取不到接口登录用户配置信息！";
                ZhiFang.Common.Log.Log.Error(baseResultData.message+ "：用户配置文件InterfaceUser不存在");
            }
            ZhiFang.Common.Log.Log.Info("RS_ReaSaleDocCreate方法接口用户自动登录---结束");
            return baseResultData;
        }

        public bool RBACLogin(string strUserAccount, string strPassWord, bool isValidate, ref HREmployee emp)
        {
            bool tempBool = false;
            if (!isValidate)
                return true;
            IApplicationContext context = ContextRegistry.GetContext();
            IBRBACUser IBRBACUser = (IBRBACUser)context.GetObject("BRBACUser");
            IList<RBACUser> tempRBACUser = IBRBACUser.SearchRBACUserByUserAccount(strUserAccount);
            if (tempRBACUser.Count == 1)
            {
                emp = tempRBACUser[0].HREmployee;
                if (!string.IsNullOrEmpty(strPassWord))
                    strPassWord = ZhiFang.Common.Public.SecurityHelp.MD5Encrypt(strPassWord, ZhiFang.Common.Public.SecurityHelp.PWDMD5Key);
                if (tempRBACUser[0].IsUse.HasValue && tempRBACUser[0].IsUse.Value)
                {
                    if (tempRBACUser[0].HREmployee.IsUse.HasValue && tempRBACUser[0].HREmployee.IsUse.Value && tempRBACUser[0].HREmployee.IsEnabled == 1)
                    {
                        if (string.IsNullOrWhiteSpace(strPassWord))
                            tempBool = (tempRBACUser[0].Account == strUserAccount) && (!tempRBACUser[0].AccLock);
                        else
                            tempBool = (tempRBACUser[0].Account == strUserAccount) && (tempRBACUser[0].PWD == strPassWord) && (!tempRBACUser[0].AccLock);
                        if (tempBool)
                        {
                            SetUserSession(tempRBACUser[0]);
                        }
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug("员工被禁用或者逻辑删除！");
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug("员工帐号被逻辑删除！");
                }
            }
            return tempBool;
        }

        public void SetUserSession(RBACUser rbacUser)
        {
            if (rbacUser != null)
            {
                Cookie.CookieHelper.Write(SysPublicSet.SysDicCookieSession.LabID, "");
                Cookie.CookieHelper.Write(SysPublicSet.SysDicCookieSession.IsLabFlag, "");
                Cookie.CookieHelper.Write(SysPublicSet.SysDicCookieSession.LabID, rbacUser.LabID.ToString());//实验室ID
                if (rbacUser.LabID > 0)
                    Cookie.CookieHelper.Write(SysPublicSet.SysDicCookieSession.IsLabFlag, "1");
                Cookie.CookieHelper.Write(DicCookieSession.UserID, rbacUser.Id.ToString());//账户ID
                Cookie.CookieHelper.Write(DicCookieSession.UserAccount, rbacUser.Account);//账户名
                Cookie.CookieHelper.Write(DicCookieSession.UseCode, rbacUser.UseCode);//账户代码

                if (rbacUser.HREmployee != null)
                {
                    Cookie.CookieHelper.Write(DicCookieSession.EmployeeID, rbacUser.HREmployee.Id.ToString());// 员工ID
                    Cookie.CookieHelper.Write(DicCookieSession.EmployeeName, rbacUser.HREmployee.CName);// 员工姓名
                    Cookie.CookieHelper.Write(DicCookieSession.EmployeeUseCode, rbacUser.HREmployee.UseCode);// 员工代码
                    if (rbacUser.HREmployee.HRDept != null)
                    {
                        Cookie.CookieHelper.Write(DicCookieSession.HRDeptID, rbacUser.HREmployee.HRDept.Id.ToString());//部门ID
                        Cookie.CookieHelper.Write(DicCookieSession.HRDeptName, rbacUser.HREmployee.HRDept.CName);//部门名称
                        Cookie.CookieHelper.Write(DicCookieSession.HRDeptCode, rbacUser.HREmployee.HRDept.UseCode);//部门名称
                    }
                }
            }
            else
            {
                Cookie.CookieHelper.Write(SysPublicSet.SysDicCookieSession.LabID, "");//实验室ID
                Cookie.CookieHelper.Write(DicCookieSession.UserID, "");//账户ID
                Cookie.CookieHelper.Write(DicCookieSession.UserAccount, DicCookieSession.SuperUser);//账户名
                Cookie.CookieHelper.Write(DicCookieSession.UseCode, "");//账户代码
                Cookie.CookieHelper.Write(DicCookieSession.EmployeeID, "");// 员工ID
                Cookie.CookieHelper.Write(DicCookieSession.EmployeeName, DicCookieSession.SuperUserName);// 员工姓名
                Cookie.CookieHelper.Write(DicCookieSession.HRDeptID, "");//部门ID
                Cookie.CookieHelper.Write(DicCookieSession.HRDeptName, "");//部门名称            
            }
        }

        public BaseResultData GetCurUserCenOrg(HREmployee emp, ref CenOrg cenOrg)
        {
            BaseResultData baseResultDataValue = new BaseResultData();
            if (emp == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.message = "无法获取当前用户信息";
                ZhiFang.Common.Log.Log.Info(baseResultDataValue.message);
                return baseResultDataValue;
            }
            string orgNo = emp.HRDept.UseCode;
            HRDept dept = emp.HRDept;
            if (string.IsNullOrEmpty(orgNo))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.message = "当前用户所属机构的编码为空！请联系管理员维护！";
                ZhiFang.Common.Log.Log.Info(baseResultDataValue.message);
                return baseResultDataValue;
            }
            else
            {
                IApplicationContext context = ContextRegistry.GetContext();
                IBCenOrg IBCenOrg = (IBCenOrg)context.GetObject("BCenOrg");
                IList<CenOrg> listCenOrg = IBCenOrg.SearchListByHQL(" cenorg.OrgNo=\'" + orgNo + "\'");
                if (listCenOrg != null && listCenOrg.Count > 0)
                    cenOrg = listCenOrg[0];
            }
            return baseResultDataValue;
        }

        #endregion

        public static BaseResultData GetThirdSaleDocType()
        {
            BaseResultData baseResultData = new BaseResultData();
            string serverPath = HttpContext.Current.Server.MapPath("~/");
            string xmlType = serverPath + "\\BaseTableXML\\Interface\\InterfaceUser.xml";
            if (System.IO.File.Exists(xmlType))
            {
                DataSet ds = ZhiFang.Common.Public.XmlToData.XmlFileToDataSet(xmlType);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Columns.Contains("ClientCode"))
                        baseResultData.data = ds.Tables[0].Rows[0]["ClientCode"].ToString();
                }
                else
                {
                    baseResultData.success = false;
                    baseResultData.message = "获取不到第三方接口配置信息！";
                    ZhiFang.Common.Log.Log.Error(baseResultData.message + "：用户配置文件中没有第三方接口相关配置信息");
                    return baseResultData;
                }
            }
            else
            {
                baseResultData.success = false;
                baseResultData.message = "获取不到第三方接口配置信息！";
                ZhiFang.Common.Log.Log.Error(baseResultData.message + "：用户配置文件GetSaleDocType不存在");
            }
            return baseResultData;
        }

        public static BaseResultData GetThirdSaleDocDBInfo(ref string dbType, ref string dbConnectStr)
        {
            BaseResultData baseResultData = new BaseResultData();
            string serverPath = HttpContext.Current.Server.MapPath("~/");
            string xmlType = serverPath + "\\BaseTableXML\\Interface\\InterfaceUser.xml";
            if (System.IO.File.Exists(xmlType))
            {
                DataSet ds = ZhiFang.Common.Public.XmlToData.XmlFileToDataSet(xmlType);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    baseResultData.data = ds.Tables[0].Rows[0]["ClientCode"].ToString();
                    if (ds.Tables[0].Columns.Contains("ClientDBType"))
                        dbType = ds.Tables[0].Rows[0]["ClientDBType"].ToString();
                    if (ds.Tables[0].Columns.Contains("ClientDBConnectStr"))
                        dbConnectStr = ds.Tables[0].Rows[0]["ClientDBConnectStr"].ToString();
                }
                else
                {
                    baseResultData.success = false;
                    baseResultData.message = "获取不到第三方接口配置信息！";
                    ZhiFang.Common.Log.Log.Error(baseResultData.message + "：用户配置文件中没有第三方接口相关配置信息");
                    return baseResultData;
                }
            }
            else
            {
                baseResultData.success = false;
                baseResultData.message = "获取不到第三方接口配置信息！";
                ZhiFang.Common.Log.Log.Error(baseResultData.message + "：用户配置文件GetSaleDocType不存在");
            }
            return baseResultData;
        }

        public static object DataConvert(Type toType, Type formType, object dataColumnValue)
        {
            if (toType == formType || dataColumnValue == null)
                return dataColumnValue;
            object resultStr = null;
            Type type = toType;
            string columnValue = dataColumnValue.ToString();
            if ((formType == typeof(DateTime)) || (formType == typeof(DateTime?)))
                columnValue = ((DateTime)dataColumnValue).ToString("yyyy-MM-dd HH:mm:ss"); ;
            if (!string.IsNullOrEmpty(columnValue))
            {
                columnValue = columnValue.Trim();
                if (type == typeof(int) || type == typeof(int?))
                {
                    resultStr = int.Parse(columnValue);
                }
                else if (type == typeof(Int64))
                {
                    resultStr = Int64.Parse(columnValue);
                }
                else if (type == typeof(double) || type == typeof(double?))
                {
                    resultStr = double.Parse(columnValue);
                }
                else if ((type == typeof(DateTime)) || (type == typeof(DateTime?)))
                {
                    resultStr = DateTime.Parse(columnValue);
                }
                else
                    resultStr = columnValue;
            }
            return resultStr;
        }

        public static string DataConvert(object dataColumnValue)
        {
            string resultStr = "";
            if (dataColumnValue != null)
                resultStr = dataColumnValue.ToString();
            return resultStr;
        }

        public static void GetColumnNameBySaleDocXMLFile(string fieldXmlPath, Dictionary<string, string> dic)
        {
            DataSet ds = ZhiFang.Common.Public.XmlToData.XmlFileToDataSet(fieldXmlPath);
            foreach (DataRow dataRow in ds.Tables[0].Rows)
            {
                if ((!string.IsNullOrEmpty(dataRow["FieldName"].ToString())) && (!string.IsNullOrEmpty(dataRow["CompareFieldName"].ToString())))
                {
                    dic.Add(dataRow["FieldName"].ToString().Trim(), dataRow["CompareFieldName"].ToString().Trim());
                }
            }
        }

        public static void GetColumnNameBySaleDocXMLFile(string fieldXmlPath, Dictionary<string, string> dic, Dictionary<string, string> dicDefault)
        {
            DataSet ds = ZhiFang.Common.Public.XmlToData.XmlFileToDataSet(fieldXmlPath);
            foreach (DataRow dataRow in ds.Tables[0].Rows)
            {
                if ((!string.IsNullOrEmpty(dataRow["FieldName"].ToString())))
                {
                    if (!string.IsNullOrEmpty(dataRow["CompareFieldName"].ToString()) || (dataRow["IsDefault"].ToString() == "1"))
                        dic.Add(dataRow["FieldName"].ToString().Trim(), dataRow["CompareFieldName"].ToString().Trim());
                }

                if ((!string.IsNullOrEmpty(dataRow["FieldName"].ToString())) && (dataRow["IsDefault"].ToString()=="1") && (!string.IsNullOrEmpty(dataRow["DefaultValue"].ToString())))
                {
                    dicDefault.Add(dataRow["FieldName"].ToString().Trim(), dataRow["DefaultValue"].ToString().Trim());
                }
            }
        }

        public static string GetLabCodeByXMLFile(string fieldXmlPath)
        {
            string labCode = "";
            DataSet ds = ZhiFang.Common.Public.XmlToData.XmlFileToDataSet(fieldXmlPath);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                labCode = ds.Tables[0].Rows[0]["LabCode"].ToString();
            return labCode;
        }

        public static string XmlSerialize<T>(T obj)
        {
            try
            {
                using (StringWriter sw = new StringWriter())
                {
                    Type type = obj.GetType();
                    XmlSerializer serializer = new XmlSerializer(type);
                    serializer.Serialize(sw, obj);
                    sw.Close();
                    return sw.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("将实体对象转换成XML异常", ex);
            }
        }

        public static string XmlSerialize<T>(Type type, T obj)
        {
            string xmlStr = "";
            try
            {
                XmlSerializer xml = new XmlSerializer(type);
                using (MemoryStream stream = new MemoryStream())
                {

                    xml.Serialize(stream, obj);
                    xmlStr = Encoding.UTF8.GetString(stream.GetBuffer(), 0, (int)stream.Length);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("将实体对象转换成XML异常", ex);
            }
            return xmlStr;
        }

    }
}
