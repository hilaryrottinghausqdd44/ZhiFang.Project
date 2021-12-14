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
	/// Module 的摘要说明。
	/// </summary>
	public partial class Module : ZhiFang.WebLis.Class.BasePage
	{	
		protected int thisModuleID=0;
		DataSet ModuleTemDS=new DataSet();
		protected string TemName="";
		protected string imageSelected="htmlicon.gif";
		protected Boolean NeedRefresh=false;

		/// <summary>
		/// 模块管理中，模块的唯一标识
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
                    if (Request.Form["hOperate"].ToString() == "更改按钮")
                    {
                        initModule(thisModuleID);
                    }
				}
			}
			catch(Exception ex)
			{
				lblMSG.Text="传入参数应该为数字";
				lblMSG.ToolTip=ex.ToString();
			}
			ModuleID.Text=id;
            TemName = "~" + ModuleID.Text;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            
		}

		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
            //this.Load += new System.EventHandler(this.Page_Load);
            //base.OnInit(e);
            InitializeComponent();
            base.OnInit(e);
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
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
                    lblMSG.Text = "模块编号或名称不合法";
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
                    lblMSG.Text = "创建模块成功";
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
                    lblMSG.Text = "创建模块失败:" + outDesc;
                    //TemName = "~" + thisModuleID;ban
                }
                }
                else
                {
                    lblMSG.Text = "创建模块失败:模块编号'"+ModuleCode.Text.Trim()+"'已存在！";
                }
			}
			catch( Exception ex)
			{
				lblMSG.Text="创建模块出错";
				lblMSG.ToolTip=ex.ToString();
                //TemName = "~" + thisModuleID;
			}
		}

		protected void buttModify_Click(object sender, System.EventArgs e)
		{
            if (ModuleCode.Text.IndexOf("'") >= 0 || ModuleCode.Text.IndexOf(" ") >= 0
                    || CName.Text.IndexOf("'") >= 0 || CName.Text.IndexOf(" ") >= 0)
            {
                lblMSG.Text = "模块编号或名称不合法";
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
					lblMSG.Text +=":修改模块成功";
				}
				else
					lblMSG.Text +=":修改模块失败:" + outDesc;
			}
			catch(Exception ex)
			{
				buttDelete.Enabled=false;
				buttModify.Enabled=false;
				lblMSG.Text +=":修改模块出错," + ex.Message;
				lblMSG.ToolTip +=":请选定模块";
			}
			
		
		}

		protected void buttDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				bool deleted=bm.Del(int.Parse(this.ModuleID.Text));
				if(deleted)
				{
					lblMSG.Text="删除成功";
					buttDelete.Enabled=false;
					buttModify.Enabled=false;
				}
			
				else
					lblMSG.Text="删除失败";
			}
			catch(Exception ex)
			{
				lblMSG.Text="删除出错:　信息:";
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
				string ExpPath="无";
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
				lblMSG.Text="出错，这里有祥细信息提示";
				lblMSG.ToolTip=ex.ToString();

			}
			
		}		
	}
}
