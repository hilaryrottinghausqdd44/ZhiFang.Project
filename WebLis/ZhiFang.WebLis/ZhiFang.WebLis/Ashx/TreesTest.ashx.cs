using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Data;
using System.Text;
using System.Xml;
using System.Web.UI.WebControls;
using ZhiFang.WebLis.Class;
using ZhiFang.Common.Public;
using ZhiFang.Common.Log;

namespace ZhiFang.WebLis.Ashx
{
    /// <summary>
    /// TreesTest 的摘要说明
    /// </summary>
    public class TreesTest : IHttpHandler
    {
        protected XmlNodeList ModuleList = null;
        StringBuilder result = new StringBuilder();
        StringBuilder sb = new StringBuilder();

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                ZhiFang.Common.Log.Log.Info("权限树显示!");
                string cookiesName = ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser");
                //string cookiesName = "1001";
                string pwd = ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangPwd");
                User u = new User(cookiesName);
                ModuleManager m = new ModuleManager();
                ZhiFang.Common.Log.Log.Info("@@@1");
                //有BUG存在，因为要指定不同的域
                //string xmlFile=Server.MapPath("../../RBAC/Modules/") + "\\xml\\"+"1"+"\\" + u.UserID + ".xml";
                u.SetModulesList();
                ZhiFang.Common.Log.Log.Info("@@@2");
                ZhiFang.Common.Log.Log.Info("u.ModulesList.count:" + u.ModulesList.Count);
                XmlDocument xmlstrmodules1 = m.GetModulesXml(u.ModulesList);
                ZhiFang.Common.Log.Log.Info("@@@3");
                string xmlStr = xmlstrmodules1.InnerXml;
                ZhiFang.Common.Log.Log.Info("@@@4");
                XmlDocument xmlstrmodules = new XmlDocument();
                GetXml.SingleLoginWS singleLoginWS = new GetXml.SingleLoginWS();
                string descript = "";
                string userID = "";
                if (ConfigHelper.GetConfigString("IsHasService") == "1")
                {
                    string xmlNew = singleLoginWS.GetXMLToMergeCWAndWeblisModules(cookiesName, pwd, xmlStr, out descript, out userID);
                    xmlstrmodules.LoadXml(xmlNew);
                    if (xmlNew != "")
                    {
                        Cookie.CookieHelper.Write("UserID", userID);
                        Log.Info(descript);
                    }
                    else
                    {
                        XmlNode nodeLst = xmlstrmodules.SelectSingleNode("//TREENODES/treenode[@ModuleCode='SC']");
                        nodeLst.RemoveAll();
                        nodeLst.ParentNode.RemoveChild(nodeLst);
                        Log.Info("获取xml出错：" + descript);
                    }
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
                }
                if (xmlstrmodules != null && xmlstrmodules.OuterXml.Trim() != "")
                {
                    //更新时间
                    XmlAttribute UpdateTime = xmlstrmodules.CreateAttribute("UpdateTime");
                    UpdateTime.Value = DateTime.Now.ToString();
                    xmlstrmodules.DocumentElement.Attributes.Append(UpdateTime);
                    bindTreeView(xmlstrmodules.DocumentElement);
                    xmlstrmodules = null;
                    //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "", "parent.document.getElementById(\"MainList\").src=\"www.sina.com.cn\"", true);
                }
                else
                {
                    XmlDocument xdLatest = new XmlDocument();
                    xdLatest.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><Root Latest=\"" + DateTime.Now.ToString() + "\"></Root>");

                }

            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Info(DateTime.Now.ToString() + ex.Message + ex.StackTrace);
            }
            //string cookiesName = "1001";
            //ZhiFang.WebLis.Class.User u = new ZhiFang.WebLis.Class.User(cookiesName);
            //u.SetModulesList();
            //ModuleManager m = new ModuleManager();
            //XmlDocument xmlstrmodules = m.GetModulesXml(u.ModulesList);

            //bindTreeView(xmlstrmodules.DocumentElement);


            string content = result.ToString();

            context.Response.Write(content);
        }


        private void bindTreeView(XmlNode document)
        {
            result.Append(sb.ToString());
            sb.Clear();
            sb.Append("["); 
            foreach (XmlNode node in document.ChildNodes)
            {
                string text = node.Attributes["Text"].Value;//文本                
                string url = node.Attributes["NavigateUrl"].Value;//URL
                sb.Append("{\"id\":\"" + url + "\",\"text\":\"" + text + "\",\"state\":\"open\",\"Target\":\"MainList\",\"attributes\":{\"NavigateUrl\":\"" + url + "\",\"ImageUrl\":\" "+ node.Attributes["ImageUrl"].Value + "\"}");

                if (node.ChildNodes.Count > 0)
                {
                    sb.Append(",\"children\":");
                    bindTreeView(node);
                    result.Append(sb.ToString());
                    sb.Clear();                    
                    
                }
                result.Append(sb.ToString());
                sb.Clear();
                sb.Append("},");   
            }
            sb = sb.Remove(sb.Length - 1, 1);           
            sb.Append("]");            
            result.Append(sb.ToString());
            sb.Clear();   
        }
        
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}