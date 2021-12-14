<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Roles.RapidAccess" Codebehind="RapidAccess.aspx.cs" %>

<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>PersonList</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <meta content="Microsoft FrontPage 4.0" name="GENERATOR">
    <meta content="FrontPage.Editor.Document" name="ProgId">
    <style>
        TABLE
        {
            font-weight: normal;
            font-size: 14px;
            color: #000000;
            text-decoration: none;
        }
    </style>

    <script language="javascript" event="onbuttonclick" for="Toolbar1">
			switch (event.srcNode.getAttribute('Id'))
			{
				case 'Employee':
					PostModules.txtRoleTypeName.value="请选择..";
					PostModules.txtRoleType.value="0";
					if(ChooseEmpl())
						firSubmit();
					break;
				case 'Department':
					PostModules.txtRoleTypeName.value="请选择..";
					PostModules.txtRoleType.value="1";
					if(ChooseDept())
						firSubmit();
					//window.open('ChooseDepartment.aspx?Id=0','','width=580px,height=460px,resizable=yes,left=' + (screen.availWidth-740)/2 + ',top=' + (screen.availHeight-540)/2 );	
					break;
				case 'Position':
					PostModules.txtRoleTypeName.value="请选择..";
					PostModules.txtRoleType.value="2";
					if(ChoosePosi())
						firSubmit();
					//window.open('personadd.aspx?Id=0','','width=580px,height=460px,resizable=yes,left=' + (screen.availWidth-740)/2 + ',top=' + (screen.availHeight-540)/2 );	
					break;
				case 'Post':
				PostModules.txtRoleTypeName.value="请选择..";
					PostModules.txtRoleType.value="3";
					if(ChoosePost())
						firSubmit();
					//window.open('personadd.aspx?Id=0','','width=580px,height=460px,resizable=yes,left=' + (screen.availWidth-740)/2 + ',top=' + (screen.availHeight-540)/2 );	
					break;
			}
    </script>

    <script language="javascript">
	<!--
		var sAccess="";
			var hArrayModuleID="";
		
		function firSubmit()
		{
			if(PostModules.txtRoleType.value==""
				||PostModules.txtRoleTypeName.value==""
				||PostModules.txtRoleID.value==""
				||PostModules.txtRoleName.value=="")
			{
				alert('没有选择员工，部门，职位或岗位');
				return false;
			}	
			var strUrl="";
			strUrl="RapidAccessModules.aspx?txtRoleType=" + PostModules.txtRoleType.value;
			strUrl= strUrl + "&txtRoleID=" + PostModules.txtRoleID.value;
			strUrl= strUrl + "&txtRoleName=" + PostModules.txtRoleName.value;
			window.frames['frmAllModules'].location=strUrl;
			return false;
			
		}
		
		function SaveModules()
		{
			
			if(PostModules.txtRoleType.value==""
				||PostModules.txtRoleTypeName.value==""
				||PostModules.txtRoleID.value==""
				||PostModules.txtRoleName.value=="")
			{
				alert('没有选择员工，部门，职位或岗位');
				return false;
			}
			
			//var hAccess=window.frames['frmAccess'].document.all["hAccess"];
			//if(typeof(hAccess)=='undefined')
			//{
			//	alert('还要选择模块');
			//	return false;
			//}
			//if(hAccess.value=="")
			//{
			//	alert('没有选定任何访问权限');
			//	return false;
			//}
			var CheckModules;
			
			
			//CheckModules=window.frames['frmAllModules'].document.all["TreeView1"].getChildren();
			//PostModules.hArrayModuleID.value="";
			//PostModules.hArrayModuleIDRemoved.value="";
			collectModules(CheckModules)
			
			if(hArrayModuleID=="")
			{
				alert('没有选定任何模块');
				return false;
			}
			
			
			
			
			var strUrl="";
			strUrl="AdvancedSaveAccess.aspx?hArrayModuleID=" + hArrayModuleID;
			strUrl +="&hArrayModuleIDRemoved=" ;
			strUrl +="&txtRoleType=" + PostModules.txtRoleType.value;
			strUrl= strUrl + "&txtRoleID=" + PostModules.txtRoleID.value;
			strUrl= strUrl + "&txtRoleName=" + PostModules.txtRoleName.value;
			strUrl= strUrl + "&hAccess=" + sAccess;
			
			window.frames['SaveAccess'].location=strUrl;
		
			//PostModules.buttSave.value="正在保存...";
			//PostModules.buttSave.disabled=true;
			 sAccess="";
			 hArrayModuleID="";
			return false;
		}
		function collectModules()
		{
			for(var i=0;i<window.frames['frmAllModules'].document.all['lstModules'].options.length;i++)
			{
				
				var MyItem=window.frames['frmAllModules'].document.all['lstModules'].options[i];		
				
				hArrayModuleID+=MyItem.text +",";
				sAccess+=MyItem.value +",";
			}
			hArrayModuleID=hArrayModuleID.substring(0,hArrayModuleID.length-1);
			sAccess=sAccess.substring(0,sAccess.length-1);
			
		}
		function reloadAll()
		{
			if(PostModules.txtRoleType.value==""
					||PostModules.txtRoleTypeName.value==""
					||PostModules.txtRoleID.value==""
					||PostModules.txtRoleName.value=="")
				{
					alert('没有选择员工，部门，职位或岗位');
					return false;
				}	
			window.frames['frmAllModules'].location.reload();
		}
		
		
	//-->
    </script>

    <script language="javascript" id="clientEventHandlersJS">
<!--
function ChooseEmpl()
{
	var result;
	result = window.showModalDialog('../Organizations/searchperson.aspx','','dialogWidth=30;dialogHeight=30;status=no;scroll=no');
	if (result != 'undefined' && typeof(result)!='undefined')
	{
		var rv = result.split("|");
		if (rv.length == 2)
		{	PostModules.txtRoleTypeName.value='按员工分配';
			PostModules.txtRoleName.value = rv[1];
			PostModules.txtRoleID.value = rv[0];
			return true;
		}
		else
		{
			return false;
		}
	}
	else
	{
		return false;
	}
}
function ChooseDept()
{
	var result;
	result = window.showModalDialog('ChooseDepartment.aspx','','dialogWidth=30;dialogHeight=32;status=no;scroll=no');
	if (result != 'undefined' && typeof(result)!='undefined')
	{
		var rv = result.split("|");
		if (rv.length == 2)
		{	PostModules.txtRoleTypeName.value='按部门分配';
			PostModules.txtRoleName.value = rv[1];
			PostModules.txtRoleID.value = rv[0];
			return true;
		}
		else
		{
			return false;
		}
	}
	else
	{
		return false;
	}
}
function ChoosePosi()
{
	var result;
	result = window.showModalDialog('ChoosePosition.aspx','','dialogWidth=30;dialogHeight=20;status=no;scroll=yes');
	if (result != 'undefined' && typeof(result)!='undefined')
	{
		var rv = result.split("|");
		if (rv.length == 2)
		{	PostModules.txtRoleTypeName.value='按职位分配';
			PostModules.txtRoleName.value = rv[1];
			PostModules.txtRoleID.value = rv[0];
			return true;
		}
		else
		{
			return false;
		}
	}
	else
	{
		return false;
	}
}
function ChoosePost()
{
	var result;
	result = window.showModalDialog('ChoosePost.aspx','','dialogWidth=30;dialogHeight=20;status=no;scroll=yes');
	if (result != 'undefined' && typeof(result)!='undefined')
	{
		var rv = result.split("|");
		if (rv.length == 2)
		{	PostModules.txtRoleTypeName.value='按岗位位分配';
			PostModules.txtRoleName.value = rv[1];
			PostModules.txtRoleID.value = rv[0];
			return true;
		}
		else
		{
			return false;
		}
	}
	else
	{
		return false;
	}
}
//-->
    </script>

</head>
<body leftmargin="0" topmargin="0">
    <form id="PostModules" name="PostModules" method="post" runat="server">
    <DIV id="oToolTip" style="DISPLAY: ;POSITION: absolute;left:200px;top:100px">
			    <div style="BORDER-RIGHT: black 2px solid; PADDING-RIGHT: 10px; BORDER-TOP: black 2px solid; PADDING-LEFT: 10px; FILTER: progid:DXImageTransform.Microsoft.Gradient(GradientType=0, StartColorStr=skyblue, EndColorStr=#FFFFFF); LEFT: 200px; PADDING-BOTTOM: 10px; FONT: 10pt tahoma; BORDER-LEFT: black 2px solid; WIDTH: 170px; PADDING-TOP: 10px; BORDER-BOTTOM: black 2px solid; TOP: 200px; HEIGHT: 120px">
				    <b>正在显示该角色的<br />权限模块逻辑结构:</b>
				    <hr style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
					    SIZE="1">
				    <label id="oToolTipContent">:::请稍候:::</label>
				    <marquee behavior=alternate style="color:red;"><b>马上呈现...！</b></marquee>
				    <div></div>
			    </div>
		    </DIV>
    <table height="504" width="768" bgcolor="#ffffff">
        <tr bgcolor="lightgrey" height="40">
            <td colspan="2" height="38" valign="middle">
                <b><font color="red">
                    <asp:Image ID="Image1" runat="server" ImageUrl="../../images/icons/0060_a.gif" ImageAlign="AbsMiddle">
                    </asp:Image>模板功能的访问控制权限</font></b>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f0f0f0" colspan="2" height="43">
                <p align="left">
                    <iewc:Toolbar ID="Toolbar1" runat="server" BorderWidth="0px" Width="420px" BorderColor="Transparent"
                        BackColor="#F0F0F0" BorderStyle="Outset">
                        <iewc:ToolbarButton Text="&amp;nbsp;按岗位分配" ImageUrl="../../images/icons/0037_b.gif"
                            DefaultStyle="display;border:solid 1px skyblue;" ID="Post" HoverStyle="border:solid 1px red;">
                        </iewc:ToolbarButton>
                        <iewc:ToolbarButton Text="&amp;nbsp;按职位分配" ImageUrl="../../images/icons/0063_b.gif"
                            DefaultStyle="display;border:solid 1px skyblue;" ID="Position" HoverStyle="border:solid 1px red;">
                        </iewc:ToolbarButton>
                        <iewc:ToolbarButton Text="&amp;nbsp;按部门分配" ImageUrl="../../images/icons/0022_b.gif"
                            DefaultStyle="display;border:solid 1px skyblue;" ID="Department" HoverStyle="border:solid 1px red;">
                        </iewc:ToolbarButton>
                        <iewc:ToolbarButton Text="&amp;nbsp;按员工分配" ImageUrl="../../images/icons/0074_b.gif"
                            DefaultStyle="display;border:solid 1px skyblue;" ID="Employee" HoverStyle="border:solid 1px red;">
                        </iewc:ToolbarButton>
                    </iewc:Toolbar>
                    <input id="buttSave" onclick="return SaveModules();" type="button" value="保存" name="buttSave">
                    <asp:TextBox ID="txtRoleTypeName" runat="server" Enabled="true" Width="85px" contentEditable="false"></asp:TextBox>
                    <asp:TextBox ID="txtRoleName" runat="server" Width="96px" contentEditable="false"></asp:TextBox>
                    <asp:TextBox ID="txtRoleID" runat="server" Width="37px" contentEditable="false"></asp:TextBox>
                    <asp:TextBox ID="txtRoleType" runat="server" Width="0px"></asp:TextBox></p>
            </td>
        </tr>
        <tr>
            <td width="996" align="left" valign="top" bgcolor="#f0f0f0" height="100%" colspan="2">
                <iframe id="frmAllModules" name="frmAllModules" src="RapidAccessModules.aspx" frameborder="0"
                    width="100%" height="100%"></iframe>
                &nbsp;
                <iframe id="SaveAccess" name="SaveAccess" src="AdvancedSaveAccess.aspx" frameborder="0"
                    width="0" height="0"></iframe>
            </td>
        </tr>
    </table>
    <p>
        <script language="javascript">
				function RoleEdit(id)
				{
					window.open ('RoleEdit.aspx?id=' + id,'RoleEdit','width=500,height=400,scrollbars=0,top=170,left=200');
				}
        </script>

    </p>
    </form>
</body>
</html>
