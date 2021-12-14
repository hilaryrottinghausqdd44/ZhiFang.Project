using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Threading;
using System.Xml;
using ZhiFang.WebLis.Class;

namespace ZhiFang.WebLis.Modules
{
	/// <summary>
	/// ModuleList ��ժҪ˵����
	/// </summary>
    public partial class ModuleList : ZhiFang.WebLis.Class.BasePage
	{
		protected System.Web.UI.WebControls.TreeView TreeView1;
		protected int UserID=0;
		
		protected XmlDocument xd=null;
		protected void Page_Load(object sender, System.EventArgs e)
		{
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            ModuleManager m = new ModuleManager();
            xd = new XmlDocument();
            if (base.CheckCookies("ZhiFangUser"))
            {
                Class.User u = new Class.User(base.ReadCookies("ZhiFangUser"));
                xd = m.GetModulesXml(u.ModulesList);


//			string xmlFile=Server.MapPath(".") + "\\xml\\"+"1"+"\\" + u.UserID + ".xml";
//
//			if(IsPostBack||!File.Exists(xmlFile)||Request.QueryString["Refresh"]!=null)
//			{
//				DirectoryInfo d;
//				string FilePath=xmlFile.Substring(0,xmlFile.LastIndexOf("\\"));
//				if(!Directory.Exists(FilePath))
//					d=Directory.CreateDirectory(FilePath);
//				xd.Save(xmlFile);
//			}
            }
		}
		#region Web ������������ɵĴ���
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    
		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
			//ss
		}

		
	}
}
