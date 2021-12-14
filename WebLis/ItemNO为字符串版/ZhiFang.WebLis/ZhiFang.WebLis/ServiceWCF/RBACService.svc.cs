using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using ZhiFang.WebLis.ServerContract;
using ZhiFang.WebLis.Class;
using System.Xml;
using System.Web.UI.WebControls;
using ZhiFang.Common.Public;
using ZhiFang.Common.Log;
using Newtonsoft.Json;
using System.Web;

namespace ZhiFang.WebLis.ServiceWCF
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class RBACService : IRBACService
    {
        protected XmlNodeList ModuleList = null;
        StringBuilder result = new StringBuilder();
        StringBuilder sb = new StringBuilder();

        #region IRBACService 成员
        public List<ZhiFang.Model.tree> RBAC_GetModuleTreeByCookie()
        {
            try
            {

                List<ZhiFang.Model.tree> moduletree = new List<ZhiFang.Model.tree>();
                var aaa = HttpContext.Current.Request;
                string cookiesName = ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser");

                string pwd = ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangPwd");
                User u = new User(cookiesName);
                ModuleManager m = new ModuleManager();
                u.SetModulesList();
                XmlDocument xmlstrmodules1 = m.GetModulesXml(u.ModulesList);
                string xmlStr = xmlstrmodules1.InnerXml;

                XmlDocument xmlstrmodules = new XmlDocument();
                //GetXml.SingleLoginWS singleLoginWS = new GetXml.SingleLoginWS();
                SingleLogin.SingleLoginWS singleLoginWS = new SingleLogin.SingleLoginWS();
                string descript = "";
                string userID = "";
                string xmlNew = "";
                string isHasService = ZhiFang.Common.Public.ConfigHelper.GetConfigString("IsHasService");
                if (isHasService == "1")
                {
                    Log.Info("传入的xml:" + xmlStr);
                    try
                    {
                        xmlNew = singleLoginWS.GetMergeCWAndWeblisModulesXML(cookiesName, pwd, xmlStr, out descript, out userID);

                    }
                    catch (Exception ee)
                    {
                        Log.Info("获取XML出错:" + descript);
                    }
                    Log.Info("返回的xml:" + xmlNew);
                }
                if (isHasService == "1" && !String.IsNullOrEmpty(xmlNew))
                {
                    xmlstrmodules.LoadXml(xmlNew);
                    Cookie.CookieHelper.Write("UserID", userID);
                    Log.Info("descriptdescriptdescript:" + descript);
                }
                else
                {
                    xmlstrmodules = xmlstrmodules1;
                    if (xmlstrmodules.InnerText.IndexOf("SC") >= 0)
                    {
                        XmlNode nodeLst = xmlstrmodules.SelectSingleNode("//TREENODES/treenode[@ModuleCode='SC']");

                        nodeLst.RemoveAll();
                        nodeLst.ParentNode.RemoveChild(nodeLst);
                    }
                    Log.Info("获取xml为空!");
                }

                if (xmlstrmodules != null && xmlstrmodules.OuterXml.Trim() != "")
                {
                    //更新时间
                    XmlAttribute UpdateTime = xmlstrmodules.CreateAttribute("UpdateTime");
                    UpdateTime.Value = DateTime.Now.ToString();
                    xmlstrmodules.DocumentElement.Attributes.Append(UpdateTime);
                    moduletree = bindTreeView(xmlstrmodules.DocumentElement);
                }
                else
                {
                    return null;// "无模块信息！";

                }
                return moduletree;

            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(e.ToString());
                return null;
            }
        }

        private List<ZhiFang.Model.tree> bindTreeView(XmlNode document)
        {
            List<ZhiFang.Model.tree> moduletree = new List<ZhiFang.Model.tree>();
            foreach (XmlNode node in document.ChildNodes)
            {
                ZhiFang.Model.tree modulesubtree = new Model.tree();
                modulesubtree.text = node.Attributes["Text"].Value;
                modulesubtree.state = "open";
                string NavigateUrl = node.Attributes["NavigateUrl"].Value;
                string ImageUrl = " ../images/icons/zhi1.gif";
                string Para = "";
                string SN = "00";
                if (node.Attributes["Para"] != null)
                {
                    Para = node.Attributes["Para"].Value;
                }
                if (node.Attributes["SN"] != null)
                {
                    SN = node.Attributes["SN"].Value;
                }
                modulesubtree.attributes = "NavigateUrl:'" + NavigateUrl + "',ImageUrl:'" + ImageUrl + "',Para:'" + Para + "',SN:'" + SN + "'";
                if (node.ChildNodes.Count > 0)
                {
                    modulesubtree.children = bindTreeView(node);
                }
                moduletree.Add(modulesubtree);
            }

            return moduletree;
        }


        #endregion
    }
}
