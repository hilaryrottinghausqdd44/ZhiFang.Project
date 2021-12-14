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
using ZhiFang.WebLis.Class;

namespace ZhiFang.WebLis.Modules
{
	/// <summary>
	/// Module ��ժҪ˵����
	/// </summary>
	public partial class Module : ZhiFang.WebLis.Class.BasePage
	{	
		protected int thisModuleID=0;
		DataSet ModuleTemDS=new DataSet();
		protected string TemName="";
		protected string imageSelected="htmlicon.gif";
		protected Boolean NeedRefresh=false;

		/// <summary>
		/// ģ������У�ģ���Ψһ��ʶ
		/// </summary>
		private int RBACModuleID=6;
		private User user;
        public ZhiFang.WebLis.Class.ModuleManager bm = new ModuleManager();
		protected void Page_Load(object sender, System.EventArgs e)
		{
            string id = Request.QueryString["ModuleID"];
			if(id==null)
				return;
			lblMSG.Text="";
			NeedRefresh=false;
			string UserId=base.ReadCookies("ZhiFangUser");
			user=new User(UserId);
			if(user.EmplID!=0)
			{
				//Owner.Checked=false;
				//Owner.Enabled=false;
			}
			else
				Owner.Enabled=true;

            RBACModuleID = Int32.Parse(id);
			try
			{
				if(id!=null)
				{
					thisModuleID=Convert.ToInt32(id);
					buttDelete.Enabled=true;
					buttModify.Enabled=true;					
				}
				if(!IsPostBack&&thisModuleID!=0)
				{
					initModule(thisModuleID);					
				}
				if(IsPostBack&&Request.Form["hOperate"]!=null)
				{
                    Model.Modules m = new Model.Modules() { ButtonsTheme = "~" + thisModuleID, ID = thisModuleID };
                    int iCount = bm.Update(m);
                    if (Request.Form["hOperate"].ToString() == "���İ�ť")
                    {
                        initModule(thisModuleID);
                    }
				}
			}
			catch(Exception ex)
			{
				lblMSG.Text="�������Ӧ��Ϊ����";
				lblMSG.ToolTip=ex.ToString();
			}
			ModuleID.Text=id;
            TemName = "~" + ModuleID.Text;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            
		}

		#region Web ������������ɵĴ���
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
			//
            //this.Load += new System.EventHandler(this.Page_Load);
            //base.OnInit(e);
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

		protected void buttCreate_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(user.EmplID!=0)
					Owner.Checked=false;
				//NewsWebService.Module md=new NewsWebService.Module();
				//md.ModuleID=thisModuleID;
				int sysOwner=0;
				string UserID="";
				if(!Owner.Checked)
				{
					if(base.CheckCookies("UserID"))
                        UserID = base.ReadCookies("UserID");
					User u=new User(UserID);
					sysOwner=u.EmplID;
				}

                int ModuleType = (int)ZhiFang.WebLis.Class.ModuleManager.Type.Main;
				if(Type.SelectedValue=="1")
                    ModuleType = (int)ZhiFang.WebLis.Class.ModuleManager.Type.Menu;

				imageSelected=Request.Form["imgPickPic"].ToString().Trim();

                if (ModuleCode.Text.IndexOf("'") >= 0 || ModuleCode.Text.IndexOf(" ") >= 0
                    || CName.Text.IndexOf("'") >= 0 || CName.Text.IndexOf(" ") >= 0)
                {
                    lblMSG.Text = "ģ���Ż����Ʋ��Ϸ�";
                    return;
                }

                string outDesc = "";
                if (bm.CheckModuleCode(ModuleCode.Text.Trim()))
                {
                    Model.Modules m = new Model.Modules()
                    {
                        PID = thisModuleID,
                        ModuleCode = ModuleCode.Text.Trim(),
                        CName = CName.Text.Trim(),
                        EName = EName.Text.Trim(),
                        SName = SName.Text.Trim(),
                        Type = ModuleType,
                   Image="../../images/icons/" + imageSelected,URL=URL.Text.Trim(),Para=Para.Text.Trim(),Descr=Descr.Text.Trim(),Owner=sysOwner,ButtonsTheme=Theme};
                    int iResult=bm.Add(m,tbRefModuleID.Text.Trim(),RBLBLocation.SelectedValue,out outDesc);
                

                if (iResult > 0)
                {
                    lblMSG.Text = "����ģ��ɹ�";
                    buttDelete.Enabled = false;
                    buttModify.Enabled = false;
                     ModuleCode.Text= m.ModuleCode;

                    string TemButName = "~" + m.ID;
                               TemButName = "~" + m.ID;
                    TempName.Text = TemButName;
                    TemName = TempName.Text;
                    NeedRefresh = true;
                    initModule(m);
                
                }
                else
                {
                    lblMSG.Text = "����ģ��ʧ��:" + outDesc;
                    //TemName = "~" + thisModuleID;ban
                }
                }
                else
                {
                    lblMSG.Text = "����ģ��ʧ��:ģ����'"+ModuleCode.Text.Trim()+"'�Ѵ��ڣ�";
                }
			}
			catch( Exception ex)
			{
				lblMSG.Text="����ģ�����";
				lblMSG.ToolTip=ex.ToString();
                //TemName = "~" + thisModuleID;
			}
		}

		protected void buttModify_Click(object sender, System.EventArgs e)
		{
            if (ModuleCode.Text.IndexOf("'") >= 0 || ModuleCode.Text.IndexOf(" ") >= 0
                    || CName.Text.IndexOf("'") >= 0 || CName.Text.IndexOf(" ") >= 0)
            {
                lblMSG.Text = "ģ���Ż����Ʋ��Ϸ�";
                return;
            }
			int sysOwner=0;

			int ModuleType=(int)ModuleManager.Type.Main;
			if(Type.SelectedValue=="1")
                ModuleType = (int)ModuleManager.Type.Menu;
				

			//if(chkTopModule.Checked)
			//	thisModuleID=0;

			imageSelected=Request.Form["imgPickPic"].ToString().Trim();

			try
            {
                TemName = "~" + thisModuleID;
				
				TempName.Text=TemName;
				
                //bool result=md.Modify(CName.Text,EName.Text,SName.Text,ModuleType,
                //    "../../images/icons/" + imageSelected,URL.Text,Para.Text,Descr.Text,sysOwner,TemName);
                string outDesc="";
                Model.Modules m = new Model.Modules()
                {
                    PID = thisModuleID,
                    ModuleCode = ModuleCode.Text.Trim(),
                    CName = CName.Text.Trim(),
                    EName = EName.Text.Trim(),
                    SName = SName.Text.Trim(),
                    Type = ModuleType,
                   Image="../../images/icons/" + imageSelected,URL=URL.Text.Trim(),Para=Para.Text.Trim(),Descr=Descr.Text.Trim(),Owner=sysOwner,ButtonsTheme=Theme,
                    ID = int.Parse(ModuleID.Text.Trim())
                };

                int iResult = bm.Modify(m,this.tbRefModuleID.Text.Trim(),RBLBLocation.SelectedValue,out outDesc);
                if (iResult>0)
				{
					lblMSG.Text +=":�޸�ģ��ɹ�";
				}
				else
					lblMSG.Text +=":�޸�ģ��ʧ��:" + outDesc;
			}
			catch(Exception ex)
			{
				buttDelete.Enabled=false;
				buttModify.Enabled=false;
				lblMSG.Text +=":�޸�ģ�����," + ex.Message;
				lblMSG.ToolTip +=":��ѡ��ģ��";
			}
			
		
		}

		protected void buttDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				bool deleted=bm.Del(int.Parse(this.ModuleID.Text));
				if(deleted)
				{
					lblMSG.Text="ɾ���ɹ�";
					buttDelete.Enabled=false;
					buttModify.Enabled=false;
				}
			
				else
					lblMSG.Text="ɾ��ʧ��";
			}
			catch(Exception ex)
			{
				lblMSG.Text="ɾ������:����Ϣ:";
				lblMSG.ToolTip=ex.ToString();
			}
			finally
			{

			}

		}
        public void initModule(int moduleID)
        {
            IBLL.Common.BaseDictionary.IBModules ibm = BLLFactory.BLLFactory<IBLL.Common.BaseDictionary.IBModules>.GetBLL("Modules");
            this.initModule(ibm.GetModel(moduleID));
        }
		private void initModule(Model.Modules m)
		{
			try
			{
                TemName = "~" + m.ID;
                TempName.Text = "~" + m.ID; 
				CName.Text=m.CName;
				EName.Text=m.EName;
				SName.Text=m.SName;
                try
                {
                    ModuleCode.Text = m.ModuleCode;
                    tbRefModuleID.Text = m.ID.ToString();
                    tbRefModuleName.Text = CName.Text;
                }
                catch { }

				if(m.Owner==0)
				{
					Owner.Checked=true;
				}
				else
				{
					Owner.Checked=false;
				}
				string moduleType=m.Type.ToString();
				if(moduleType.Trim().ToUpper()=="MENU")
				{
					Type.Items[1].Selected=true;
				}
				else
				{
					Type.Items[0].Selected=true;
				}
				imageSelected=(m.Image==null)?"htmlicon.gif":m.Image;
				if(imageSelected.IndexOf("/")>=0)
					imageSelected=imageSelected.Substring(imageSelected.LastIndexOf("/")+1);

				ModuleImage.Src="../../images/icons/" + imageSelected;

                URL.Text = m.URL;
				Para.Text=m.Para;
				Descr.Text=m.Descr;

                string RootPath = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath; ;
				if(RootPath.Substring(RootPath.Length-1)=="/")
					RootPath=RootPath.Remove(RootPath.Length-1,1);
				string AbsPath=RootPath+"/MODULES/ModuleRun.aspx?ModuleID=" + m.ID;
                string RlvPath = "/MODULES/ModuleRun.aspx?ModuleID=" + m.ID;
				string ExpPath="��";
				if(URL.Text.Trim()!="#"&&URL.Text.Trim()!="")
				{
					ExpPath=URL.Text;
					ExpPath=ExpPath.Replace("../","");
					if(ExpPath.Substring(0,1)!="/")
						ExpPath="/" + ExpPath;
                    ExpPath = RootPath + ExpPath;
					ExpPath +="?RBACModuleID=" + m.ID;
					if(Para.Text.Trim()!="")
						ExpPath +="&" + Para.Text;
				}

				LabelAbsPath.Text=AbsPath;
				LabelRlvPath.Text=RlvPath;
				LabelRealPath.Text=ExpPath;


			}
			catch (Exception ex)
			{
				lblMSG.Text="������������ϸ��Ϣ��ʾ";
				lblMSG.ToolTip=ex.ToString();

			}
			
		}		
	}
}
