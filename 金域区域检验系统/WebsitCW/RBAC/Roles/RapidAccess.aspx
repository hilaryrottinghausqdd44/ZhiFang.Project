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
		var sAccess="";
			var hArrayModuleID="";
		
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
				alert('û��ѡ��Ա�������ţ�ְλ���λ');
				return false;
			}
			
			//var hAccess=window.frames['frmAccess'].document.all["hAccess"];
			//if(typeof(hAccess)=='undefined')
			//{
			//	alert('��Ҫѡ��ģ��');
			//	return false;
			//}
			//if(hAccess.value=="")
			//{
			//	alert('û��ѡ���κη���Ȩ��');
			//	return false;
			//}
			var CheckModules;
			
			
			//CheckModules=window.frames['frmAllModules'].document.all["TreeView1"].getChildren();
			//PostModules.hArrayModuleID.value="";
			//PostModules.hArrayModuleIDRemoved.value="";
			collectModules(CheckModules)
			
			if(hArrayModuleID=="")
			{
				alert('û��ѡ���κ�ģ��');
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
		
			//PostModules.buttSave.value="���ڱ���...";
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
		{	PostModules.txtRoleTypeName.value='����λλ����';
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
                    </asp:Image>ģ�幦�ܵķ��ʿ���Ȩ��</font></b>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f0f0f0" colspan="2" height="43">
                <p align="left">
                    <iewc:Toolbar ID="Toolbar1" runat="server" BorderWidth="0px" Width="480px" BorderColor="Transparent"
                        BackColor="#F0F0F0" BorderStyle="Outset">
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
                    <input id="buttSave" onclick="return SaveModules();" type="button" value="����" name="buttSave">
                    <asp:TextBox ID="txtRoleTypeName" runat="server" Enabled="true" Width="95px" contentEditable="false"></asp:TextBox>
                    <asp:TextBox ID="txtRoleName" runat="server" Width="96px" contentEditable="false"></asp:TextBox>
                    <asp:TextBox ID="txtRoleID" runat="server" Width="97px" contentEditable="false"></asp:TextBox>
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
        <table id="Table1" cellspacing="1" cellpadding="1" width="300" border="0" style="font-size: 12px">
            <tr>
                <td>
                    <span class="style4">Re: ֻ��Ȩ�ޣ������г���ģ�� <span class="style2">*</span>��СȨ�� </span>
                </td>
            </tr>
            <tr>
                <td>
                    <span class="style4">S : ����Ȩ�ޣ��ܶ�ȡ��ģ�����Ϣ</span>
                </td>
            </tr>
            <tr>
                <td>
                    <span class="style4">Ru: ����Ȩ�ޣ��������д�ģ�飬�������������</span>
                </td>
            </tr>
            <tr>
                <td>
                    <span class="style4">C : �����¼�ģ��</span>
                </td>
            </tr>
            <tr>
                <td>
                    <span class="style4">M : �޸ı�ģ������ƣ���ַ��ͼƬ�������Ϣ</span>
                </td>
            </tr>
            <tr>
                <td>
                    <span class="style4">D : ɾ����ģ��</span>
                </td>
            </tr>
            <tr>
                <td>
                    <span class="style4">RD: �ٷ���Ȩ�ޣ����԰Ѹ�ģ���ٷַ�</span>
                </td>
            </tr>
            <tr>
                <td>
                    <span class="style4">A : ��ȫ����Ȩ��</span>
                </td>
            </tr>
            <tr>
                <td>
                    <p class="style4">
                        ˵����ѡ�������к����Ǿܾ����ܾ�Ȩ������; �����һ��ѡ��Ȩ��������Ȩ�޿��Լ̳�</p>
                </td>
            </tr>
        </table>

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
