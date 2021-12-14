<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Roles.ModulesOperate" Codebehind="ModulesOperate.aspx.cs" %>

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
					PostModules.txtRoleTypeName.value="��ѡ��..";
					PostModules.txtRoleType.value="0";
					if(ChooseEmpl())
						firSubmit();
					break;
				case 'Department':
					PostModules.txtRoleTypeName.value="��ѡ��..";
					PostModules.txtRoleType.value="1";
					if(ChooseDept())
						firSubmit();
					//window.open('ChooseDepartment.aspx?Id=0','','width=580px,height=460px,resizable=yes,left=' + (screen.availWidth-740)/2 + ',top=' + (screen.availHeight-540)/2 );	
					break;
				case 'Position':
					PostModules.txtRoleTypeName.value="��ѡ��..";
					PostModules.txtRoleType.value="2";
					if(ChoosePosi())
						firSubmit();
					//window.open('personadd.aspx?Id=0','','width=580px,height=460px,resizable=yes,left=' + (screen.availWidth-740)/2 + ',top=' + (screen.availHeight-540)/2 );	
					break;
				case 'Post':
				PostModules.txtRoleTypeName.value="��ѡ��..";
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
				alert('û��ѡ��Ա�������ţ�ְλ���λ');
				return false;
			}	
			var strUrl="";
			strUrl="OperateModules.aspx?txtRoleType=" + PostModules.txtRoleType.value;
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
				alert('û��ѡ��Ա�������ţ�ְλ���λ');
				return false;
			}
			
			var hAccess=window.frames['frmAccess'].document.all["hAccess"];
			if(typeof(hAccess)=='undefined')
			{
				alert('��Ҫѡ��ģ��');
				return false;
			}
			//if(hAccess.value=="")
		//	{
			//	alert('û��ѡ���κη���Ȩ��');
			//	return false;
			//}
			var CheckModules;
			
			CheckModules=window.frames['frmAllModules'].document.all["TreeView1"].getChildren();
			PostModules.hArrayModuleID.value="";
			PostModules.hArrayModuleIDRemoved.value="";
			collectModules(CheckModules)
			
			if(PostModules.hArrayModuleID.value=="")
			{
				alert('û��ѡ���κ�ģ��');
				return false;
			}			
			var showButton=window.frames['frmAccess'].document.all["showButton"];
			if(showButton.value=="")
			{
				alert('û��ѡ���κ�ģ��');
				return false;
			}
			var sAccess="";		
			
			for(var i=0;i<parseInt(window.frames['frmAccess'].document.all["hAccess"].value,10);i++)
			{
				if(window.frames['frmAccess'].document.all["checkbox" + i].checked)
					sAccess="1" + sAccess;
				else
					sAccess="0" + sAccess;
			}
			
			var strUrl="";
			strUrl="SaveOperateButtons.aspx?hArrayModuleID=" + PostModules.hArrayModuleID.value;
			strUrl +="&hArrayModuleIDRemoved=" + PostModules.hArrayModuleIDRemoved.value;
			strUrl +="&txtRoleType=" + PostModules.txtRoleType.value;
			strUrl= strUrl + "&txtRoleID=" + PostModules.txtRoleID.value;
			strUrl= strUrl + "&txtRoleName=" + PostModules.txtRoleName.value;
			strUrl= strUrl + "&hAccess=" + sAccess;
			strUrl=strUrl+"&TemName="+window.frames['frmAccess'].document.all["showButton"].value;
			
			
			
			//alert(strUrl);
			//return false;
			window.frames['frmSaveAllModules'].location=strUrl;
			
			PostModules.buttSave.value="���ڱ���...";
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
				//��ӵ�ģ��
			if(PostModules.hArrayModuleID.value.substring(0,1)==",")
				PostModules.hArrayModuleID.value=PostModules.hArrayModuleID.value.substring(1,PostModules.hArrayModuleID.value.length);
				
			//ɾ����ģ��
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
					alert('û��ѡ��Ա�������ţ�ְλ���λ');
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
		{	PostModules.txtRoleTypeName.value='��Ա������';
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
		{	PostModules.txtRoleTypeName.value='�����ŷ���';
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
		{	PostModules.txtRoleTypeName.value='��ְλ����';
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
		{	PostModules.txtRoleTypeName.value='����λ����';
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
    <table bgcolor="#ffffff">
        <tr bgcolor="lightgrey" height="40">
            <td valign="middle" colspan="2" height="38">
                <b><font color="red">
                    <asp:Image ID="Image1" runat="server" ImageUrl="../../images/icons/0060_a.gif" ImageAlign="AbsMiddle">
                    </asp:Image>ģ�幦�ܵķ��ʿ���Ȩ��</font></b>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f0f0f0" colspan="2" height="43">
                <p align="left">
                    <iewc:Toolbar ID="Toolbar1" runat="server" BackColor="#F0F0F0" BorderColor="Transparent"
                        Width="480px" BorderWidth="0px" BorderStyle="Outset">
                        <iewc:ToolbarButton Text="&amp;nbsp;����λ����" ImageUrl="../../images/icons/0037_b.gif"
                            DefaultStyle="display;border:solid 1px skyblue;" ID="Post" HoverStyle="border:solid 1px red;">
                        </iewc:ToolbarButton>
                        <iewc:ToolbarButton Text="&amp;nbsp;��ְλ����" ImageUrl="../../images/icons/0063_b.gif"
                            DefaultStyle="display;border:solid 1px skyblue;" ID="Position" HoverStyle="border:solid 1px red;">
                        </iewc:ToolbarButton>
                        <iewc:ToolbarButton Text="&amp;nbsp;�����ŷ���" ImageUrl="../../images/icons/0022_b.gif"
                            DefaultStyle="display;border:solid 1px skyblue;" ID="Department" HoverStyle="border:solid 1px red;">
                        </iewc:ToolbarButton>
                        <iewc:ToolbarButton Text="&amp;nbsp;��Ա������" ImageUrl="../../images/icons/0074_b.gif"
                            DefaultStyle="display;border:solid 1px skyblue;" ID="Employee" HoverStyle="border:solid 1px red;">
                        </iewc:ToolbarButton>
                    </iewc:Toolbar>
                </p>
            </td>
        </tr>
        <tr>
            <td valign="top" align="left" width="237" bgcolor="#f0f0f0" height="189">
                <iframe id="frmAllModules" name="frmAllModules" src="AccessModules.aspx" frameborder="0"
                    width="238" height="100%"></iframe>
            </td>
            <td valign="top" align="left" width="519" bgcolor="#f0f0f0" height="189">
                <table width="114" border="0">
                    <tr>
                        <td valign="top" colspan="3" height="142">
                            <table id="Table1" width="214" border="0">
                                <tr bgcolor="#ffffff">
                                    <td valign="middle" align="right" width="56" bgcolor="#f0f0f0">
                                        ��Χ
                                    </td>
                                    <td valign="middle" align="left" width="107" bgcolor="#f0f0f0">
                                        <asp:TextBox ID="txtRoleTypeName" runat="server" Enabled="true" Width="95px" contentEditable="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle" align="right" width="56" bgcolor="#f0f0f0">
                                        ����
                                    </td>
                                    <td valign="middle" align="left" bgcolor="#f0f0f0">
                                        <asp:TextBox ID="txtRoleName" runat="server" Width="96px" contentEditable="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr bgcolor="#ffffff">
                                    <td valign="middle" align="right" width="56" bgcolor="#f0f0f0">
                                        ΨһID
                                    </td>
                                    <td valign="middle" align="left" bgcolor="#f0f0f0">
                                        <asp:TextBox ID="txtRoleID" runat="server" Width="97px" contentEditable="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr bgcolor="#ffffff">
                                    <td valign="middle" align="center" bgcolor="#f0f0f0" colspan="2">
                                        <input id="buttSave" onclick="return SaveModules();" type="button" value="����" name="buttSave" disabled="disabled">
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle" align="right" bgcolor="#f0f0f0" colspan="2" height="32">
                                        <div id="Label1">
                                        </div>
                                        <asp:TextBox ID="txtRoleType" runat="server" Width="0px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" height="4">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                &nbsp;
            </td>
        </tr>
        <tr align="left">
            <td valign="top" bgcolor="#f0f0f0" colspan="2" height="119">
                <iframe id="frmAccess" src="OperateButtons.aspx" frameborder="0" width="100%" height="200">
                </iframe>
            </td>
        </tr>
        <tr bgcolor="white" height="0">
            <td align="right" bgcolor="#f0f0f0" colspan="2" height="0">
                <iframe id="frmSaveAllModules" name="frmSaveAllModules" frameborder="0" width="0"
                    height="0"></iframe>
            </td>
        </tr>
    </table>

    <script language="javascript">
				function RoleEdit(id)
				{
					window.open ('RoleEdit.aspx?id=' + id,'RoleEdit','width=500,height=400,scrollbars=0,top=170,left=200');
				}
    </script>

    <input id="hArrayModuleID" type="hidden" name="hArrayModuleID">
    <input id="hArrayModuleIDRemoved" type="hidden" name="hArrayModuleIDRemoved">
    </form>
</body>
</html>
