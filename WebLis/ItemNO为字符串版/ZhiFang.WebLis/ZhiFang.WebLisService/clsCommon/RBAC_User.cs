using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml;
using ZhiFang.BLLFactory;
using ZhiFang.Common.Log;
using ZhiFang.IBLL.Common.BaseDictionary;

namespace ZhiFang.WebLisService.clsCommon
{
    class RBAC_User

    {
        public RBAC_User()
        {

        }
        public RBAC_User(string Account)
        {
            WSRBAC_Service.WSRbacSoapClient rbac = new WSRBAC_Service.WSRbacSoapClient();
            string userinfo = rbac.getUserInfo(Account);
            DataSet ds = new DataSet();
            try
            {
                Log.Info("userinfo:" + userinfo);
                ds = ZhiFang.Common.Public.XmlToData.CXmlToDataSet(userinfo);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Columns.Contains("ID"))
                    {
                        EmplID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString().Trim());
                    }
                    if (ds.Tables[0].Columns.Contains("NameL"))
                    {
                        NameL = ds.Tables[0].Rows[0]["NameL"].ToString().Trim();
                    }
                    if (ds.Tables[0].Columns.Contains("NameF"))
                    {
                        NameF = ds.Tables[0].Rows[0]["NameF"].ToString().Trim();
                    }
                    if (ds.Tables[0].Columns.Contains("Sex"))
                    {
                        Sex = ds.Tables[0].Rows[0]["Sex"].ToString().Trim();
                    }
                    if (ds.Tables[0].Columns.Contains("Country"))
                    {
                        Country = ds.Tables[0].Rows[0]["Country"].ToString().Trim();
                    }
                    if (ds.Tables[0].Columns.Contains("MaritalStatus"))
                    {
                        MaritalStatus = ds.Tables[0].Rows[0]["MaritalStatus"].ToString().Trim();
                    }
                    if (ds.Tables[0].Columns.Contains("EmplName"))
                    {
                        EmplName = ds.Tables[0].Rows[0]["EmplName"].ToString().Trim();
                    }
                    if (ds.Tables[0].Columns.Contains("Account"))
                    {
                        this.Account = Account;
                    }
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(e.ToString());
                throw e;
            }
        }
        public int EmplID { set; get; }
        public string NameL { set; get; }
        public string NameF { set; get; }
        public string Sex { set; get; }
        public string Country { set; get; }
        public string MaritalStatus { set; get; }
        public string EmplName { set; get; }
        public string Account { set; get; }
        public XmlDocument Organizations { set; get; }
        public SortedList<string, string[]> OrganizationsList { set; get; }
        public List<string> ModulesList { set; get; }
        public List<string> PostList { set; get; }
        public static bool CheckAccount(string Account)
        {
            WSRBAC_Service.WSRbacSoapClient rbac = new WSRBAC_Service.WSRbacSoapClient();
            string userinfo = rbac.getUserInfo(Account);
            if (userinfo.Trim().Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private string _companyname;
        public string CompanyName
        {
            get { return ZhiFang.Common.Public.ConfigHelper.GetConfigString("CompanyName"); }
            set { _companyname = value; }
        }
        public void SetModulesList()
        {
            WSRBAC_Service.WSRbacSoapClient rbac = new WSRBAC_Service.WSRbacSoapClient();
            string userinfo = rbac.getUserInfo(Account);
            XmlDocument xd = new XmlDocument();
            string xmlstr = rbac.GetPermissibilityRunModules(this.Account);
            xd.LoadXml(xmlstr);
            List<string> a = new List<string>();
            SetModulesListByXml(xd.GetElementsByTagName("TREENODES").Item(0), ref a);
            this.ModulesList = a;
        }
        public void SetdeptListByXml(XmlNode a, ref List<string> deptslist)
        {
            foreach (XmlNode xn in a.ChildNodes)
            {
                deptslist.Add(xn.Attributes["SN"].Value);
                if (xn.ChildNodes.Count > 0)
                {
                    SetdeptListByXml(xn, ref deptslist);
                }
            }
        }
        private void SetModulesListByXml(XmlNode a, ref List<string> moduleslist)
        {
            foreach (XmlNode xn in a.ChildNodes)
            {
                moduleslist.Add(xn.Attributes["ModuleCode"].Value);
                if (xn.ChildNodes.Count > 0)
                {
                    SetModulesListByXml(xn, ref moduleslist);
                }
            }
        }

        public void GetPostList()
        {
            WSRBAC_Service.WSRbacSoapClient rbac = new WSRBAC_Service.WSRbacSoapClient();
            string postlist = rbac.getPostInfoListByUser(Account);
            List<string> l = new List<string>();
            if (postlist != null && postlist.Trim() != "")
            {
                foreach (string a in postlist.Split(','))
                {
                    l.Add(a);
                }
            }
            this.PostList = l;
        }

        public static List<string> GetPostList(string Account)
        {
            WSRBAC_Service.WSRbacSoapClient rbac = new WSRBAC_Service.WSRbacSoapClient();
            string postlist = rbac.getPostInfoListByUser(Account);
            List<string> l = new List<string>();
            if (postlist != null && postlist.Trim() != "")
            {
                foreach (string a in postlist.Split(','))
                {
                    l.Add(a);
                }
            }
            return l;
        }

        public DataSet GetClientListByPost(string ClienteleLikeKey, int p)
        {
            if (this.PostList == null)
            {
                this.GetPostList();
            }
            DataSet ds = new DataSet();
            IBLL.Common.BaseDictionary.IBCLIENTELE clientele = BLLFactory<IBCLIENTELE>.GetBLL();
            IBBusinessLogicClientControl iblcc = BLLFactory<IBBusinessLogicClientControl>.GetBLL();
            if (this.PostList.Exists(l => { if (l.ToUpper() == "WEBLISADMIN") return true; else return false; }))
            {
                if (p > 0)
                {
                    ds = clientele.GetList(p, new ZhiFang.Model.CLIENTELE() { ClienteleLikeKey = ClienteleLikeKey });
                }
                else
                {
                    ds = clientele.GetList(new ZhiFang.Model.CLIENTELE() { ClienteleLikeKey = ClienteleLikeKey });
                }
            }
            else
            {
                if (this.PostList.Exists(l => { if (l.ToUpper() == "WEBLISAPPLYINPUT") return true; else return false; }))
                {
                    if (p > 0)
                    {
                        ds = iblcc.GetClientList_DataSet(p, new ZhiFang.Model.BusinessLogicClientControl() { Account = this.Account.Trim(), ClienteleLikeKey = ClienteleLikeKey, Flag = 0, SelectedFlag = true });
                    }
                    else
                    {
                        ds = iblcc.GetClientList_DataSet(new ZhiFang.Model.BusinessLogicClientControl() { Account = this.Account.Trim(), ClienteleLikeKey = ClienteleLikeKey, Flag = 0, SelectedFlag = true });
                    }
                }
                else
                {
                    if (this.PostList.Exists(l => { if (l.ToUpper() == "LOGISTICSOFFICER") return true; else return false; }))
                    {
                        if (p > 0)
                        {
                            ds = iblcc.GetClientList_DataSet(p, new ZhiFang.Model.BusinessLogicClientControl() { Account = this.Account.Trim(), ClienteleLikeKey = ClienteleLikeKey, Flag = 0, SelectedFlag = true });
                        }
                        else
                        {
                            ds = iblcc.GetClientList_DataSet(new ZhiFang.Model.BusinessLogicClientControl() { Account = this.Account.Trim(), ClienteleLikeKey = ClienteleLikeKey, Flag = 0, SelectedFlag = true });
                        }
                    }
                }
            }
            return ds;
        }
        public void GetOrganizationsList()
        {
            IBBusinessLogicClientControl iblcc = BLLFactory<IBBusinessLogicClientControl>.GetBLL();
            ZhiFang.Common.Log.Log.Info("初始化WeblisOrgId:Account =" + this.Account.Trim() + ", SelectedFlag = true, Flag = 1");
            DataSet tmpds = iblcc.GetList(new ZhiFang.Model.BusinessLogicClientControl() { Account = this.Account.Trim(), SelectedFlag = true, Flag = 1 });
            if (tmpds != null && tmpds.Tables.Count > 0 && tmpds.Tables[0].Rows.Count > 0)
            {
                ZhiFang.Common.Log.Log.Info("初始化WeblisOrgId:tmpds.Tables.Count =" + tmpds.Tables.Count.ToString().Trim() + ", tmpds.Tables[0].Rows.Count=" + tmpds.Tables[0].Rows.Count.ToString().Trim());
                this.OrganizationsList = new SortedList<string, string[]>();
                foreach (DataRow dr in tmpds.Tables[0].Rows)
                {
                    ZhiFang.Common.Log.Log.Info("初始化WeblisOrgId:ClientNo =" + dr["ClientNo"].ToString().Trim() + ", CName=" + dr["CName"].ToString().Trim());
                    this.OrganizationsList.Add(dr["ClientNo"].ToString().Trim(), new string[2] { dr["ClientNo"].ToString().Trim(), dr["CName"].ToString().Trim() });
                }
            }
        }
    }
}
