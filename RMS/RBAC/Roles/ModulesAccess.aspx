<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>

<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Roles.ModulesAccess" Codebehind="ModulesAccess.aspx.cs" %>

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
			strUrl="AccessModules.aspx?txtRoleType=" + PostModules.txtRoleType.value;
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
			
			var hAccess=window.frames['frmAccess'].document.all["hAccess"];
			if(typeof(hAccess)=='undefined')
			{
				alert('还要选择模块');
				return false;
			}
			if(hAccess.value=="")
			{
				alert('没有选定任何访问权限');
				return false;
			}
			var CheckModules;
			
			CheckModules=window.frames['frmAllModules'].document.all["TreeView1"].getChildren();
			PostModules.hArrayModuleID.value="";
			PostModules.hArrayModuleIDRemoved.value="";
			collectModules(CheckModules)
			
			if(PostModules.hArrayModuleID.value=="")
			{
				alert('没有选定任何模块');
				return false;
			}
			
			var sAccess="";
			for(var i=0;i<=parseInt(hAccess.value,10);i++)
			{
				if(window.frames['frmAccess'].document.all["checkbox" + i].checked)
					sAccess="1" + sAccess;
				else
					sAccess="0" + sAccess;
			}
			
			var strUrl="";
			strUrl="SaveAccess.aspx?hArrayModuleID=" + PostModules.hArrayModuleID.value;
			strUrl +="&hArrayModuleIDRemoved=" + PostModules.hArrayModuleIDRemoved.value;
			strUrl +="&txtRoleType=" + PostModules.txtRoleType.value;
			strUrl= strUrl + "&txtRoleID=" + PostModules.txtRoleID.value;
			strUrl= strUrl + "&txtRoleName=" + PostModules.txtRoleName.value;
			strUrl= strUrl + "&hAccess=" + sAccess;
			
			
			
			//alert(strUrl);
			//return false;
			window.frames['frmSaveAllModules'].location=strUrl;
			
			PostModules.buttSave.value="正在保存...";
			PostModules.buttSave.disabled=true;
			
			return false;
		}
		function collectModules(TreeNodes)
		{
			if(TreeNodes!=null&&typeof(TreeNodes) != "undefined")
			{
				for(var i=0;i<	TreeNodes.length;i++)
				{
					var currentChild;
					currentChild = TreeNodes[i];
					if(currentChild.getAttribute("Checked"))
						PostModules.hArrayModuleID.value +="," + currentChild.getAttribute("NodeData");
					else
						PostModules.hArrayModuleIDRemoved.value +="," + currentChild.getAttribute("NodeData");
					
					var childNodes;
					childNodes=currentChild.getChildren();
					collectModules(childNodes);
				}
			}
				//添加的模块
			if(PostModules.hArrayModuleID.value.substring(0,1)==",")
				PostModules.hArrayModuleID.value=PostModules.hArrayModuleID.value.substring(1,PostModules.hArrayModuleID.value.length);
				
			//删除的模块
			if(PostModules.hArrayModuleIDRemoved.value.substring(0,1)==",")
				PostModules.hArrayModuleIDRemoved.value=PostModules.hArrayModuleIDRemoved.value.substring(1,PostModules.hArrayModuleIDRemoved.value.length);
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
		{	PostModules.txtRoleTypeName.value='按岗位分配';
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
    <table height="404" width="768" bgcolor="#ffffff">
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
                    <iewc:Toolbar ID="Toolbar1" runat="server" BorderWidth="0px" Width="480px" BorderColor="Transparent"
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
                </p>
            </td>
        </tr>
        <tr>
            <td width="237" align="left" valign="top" bgcolor="#f0f0f0" height="288">
                <iframe id="frmAllModules" name="frmAllModules" src="AccessModules.aspx" frameborder="0"
                    width="238" height="100%"></iframe>
            </td>
            <td width="519" height="288" align="left" valign="top" bgcolor="#f0f0f0">
                <table width="114" border="0">
                    <tr>
                        <td height="142" colspan="3" valign="top">
                            <table id="Table1" width="214" border="0">
                                <tr bgcolor="#ffffff">
                                    <td valign="middle" align="right" width="56" bgcolor="#f0f0f0">
                                        范围
                                    </td>
                                    <td valign="middle" align="left" width="107" bgcolor="#f0f0f0">
                                        <asp:TextBox ID="txtRoleTypeName" runat="server" Enabled="true" Width="95px" contentEditable="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle" align="right" width="56" bgcolor="#f0f0f0">
                                        名称
                                    </td>
                                    <td valign="middle" align="left" bgcolor="#f0f0f0">
                                        <asp:TextBox ID="txtRoleName" runat="server" Width="96px" contentEditable="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr bgcolor="#ffffff">
                                    <td valign="middle" align="right" width="56" bgcolor="#f0f0f0">
                                        唯一ID
                                    </td>
                                    <td valign="middle" align="left" bgcolor="#f0f0f0">
                                        <asp:TextBox ID="txtRoleID" runat="server" Width="97px" contentEditable="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr bgcolor="#ffffff">
                                    <td colspan="2" align="center" valign="middle" bgcolor="#f0f0f0">
                                        <input id="buttSave" onclick="return SaveModules();" type="button" value="保存" name="buttSave">
                                    </td>
                                </tr>
                                <tr>
                                    <td height="32" colspan="2" align="right" valign="middle" bgcolor="#f0f0f0">
                                        <div id="Label1">
                                        </div>
                                        <asp:TextBox ID="txtRoleType" runat="server" Width="0px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td height="4" colspan="3">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr align="left">
            <td height="98" colspan="2" valign="top" bgcolor="#f0f0f0">
                <iframe id="frmAccess" frameborder="0" width="100%" height="100" src="AccessConfig.aspx">
                </iframe>
            </td>
        </tr>
        <tr bgcolor="white" height="30">
            <td align="right" bgcolor="#f0f0f0" colspan="2" height="46">
                <p align="center">
                    <iframe id="frmSaveAllModules" name="frmSaveAllModules" frameborder="0" width="0"
                        height="0"></iframe>
                </p>
            </td>
        </tr>
        </TBODY></table>

    <script language="javascript">
				function RoleEdit(id)
				{
					window.open ('RoleEdit.aspx?id=' + id,'RoleEdit','width=500,height=400,scrollbars=0,top=170,left=200');
				}
				
    </script>

    &nbsp;
    <input id="hArrayModuleID" type="hidden" name="hArrayModuleID">
    <input id="hArrayModuleIDRemoved" type="hidden" name="hArrayModuleIDRemoved">
    </form>
</body>
</html>
