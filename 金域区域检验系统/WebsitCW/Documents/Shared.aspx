<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.Documents.Shared" Codebehind="Shared.aspx.cs" %>

<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>�����ļ���</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Microsoft FrontPage 4.0" name="GENERATOR">
    <meta content="FrontPage.Editor.Document" name="ProgId">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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
					ChooseEmpl()
						
					break;
				case 'Department':
					PostModules.txtRoleTypeName.value="��ѡ��..";
					PostModules.txtRoleType.value="1";
					ChooseDept()
						
					//window.open('ChooseDepartment.aspx?Id=0','','width=580px,height=460px,resizable=yes,left=' + (screen.availWidth-740)/2 + ',top=' + (screen.availHeight-540)/2 );	
					break;
				case 'Position':
					PostModules.txtRoleTypeName.value="��ѡ��..";
					PostModules.txtRoleType.value="2";
					ChoosePosi()
						
					break;
				case 'Post':
				PostModules.txtRoleTypeName.value="��ѡ��..";
					PostModules.txtRoleType.value="3";
					ChoosePost()
					break;
			}
    </script>

    <script language="javascript" event="onpropertychange" for="FolderName">
		
		if(window.document.all['ShareFolderName'].value!=window.document.all['FolderName'].value)
		{
			window.document.all['Button1'].disabled=false;
		}
		else
		{
			window.document.all['Button1'].disabled=true;
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
	result = window.showModalDialog('../../RBAC/Organizations/searchperson.aspx','','dialogWidth=30;dialogHeight=30;status=no;scroll=no');
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
	result = window.showModalDialog('../RBAC/Roles/ChooseDepartment.aspx','','dialogWidth=30;dialogHeight=32;status=no;scroll=no');
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
	result = window.showModalDialog('../RBAC/Roles/ChoosePosition.aspx','','dialogWidth=30;dialogHeight=20;status=no;scroll=yes');
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
	result = window.showModalDialog('../RBAC/Roles/ChoosePost.aspx','','dialogWidth=30;dialogHeight=20;status=no;scroll=yes');
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
function AddShareFolder()
{
	if(PostModules.FolderName.value=="")
	{
		alert("��������Ϊ�գ�")	
		return false;
	}
	window.open('ShareConfig.aspx?FolderID='+'<%=ShareFolderID%>','','width=520px,height=340px,resizable=yes,left=' + (screen.availWidth-740)/2 + ',top=' + (screen.availHeight-540)/2 );
}

function window_onload() {

window.frames["frmAllModules"].location='FolderAccess.aspx?Folder=<%=Folder.Replace("\\","\\\\")%>';

}

//-->
    </script>

</head>
<body leftmargin="0" topmargin="0" onload="return window_onload()">
    <form id="PostModules" name="PostModules" method="post" runat="server">
    <input type="text" name="ShareFolderName" id="ShareFolderName" style="width: 0px"
        width="0" height="0">
    <table height="404" width="768" bgcolor="#ffffff">
        <tr bgcolor="lightgrey" height="40">
            <td valign="middle" colspan="2" height="38">
                <b><font color="red">
                    <asp:Image ID="Image1" runat="server" ImageAlign="AbsMiddle" ImageUrl="../images/icons/0060_a.gif">
                    </asp:Image>���ù����ļ���
                    <asp:Label ID="LblWrong" runat="server"></asp:Label></font></b>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f0f0f0" colspan="2" height="43">
                <p align="left">
                    *������
                    <asp:TextBox ID="FolderName" runat="server"></asp:TextBox>
                    <asp:Button ID="Button1" runat="server" Text="�� ��" Enabled="False" OnClick="Button1_Click">
                    </asp:Button>(����ֻ���޸Ĺ������)</p>
                <p align="left">
                    &nbsp;��ע��
                    <asp:TextBox ID="FolderDesr" runat="server" Width="280px"></asp:TextBox><input onclick="AddShareFolder();"
                        type="button" value="���ù���"></p>
            </td>
        </tr>
        <tr>
            <td valign="top" align="left" width="996" bgcolor="#f0f0f0" colspan="2" height="100%">
                <iframe id="frmAllModules" name="frmAllModules" src="FolderAccess.aspx" frameborder="0"
                    width="100%" height="100%"></iframe>
                &nbsp;
                <iframe id="SaveAccess" name="SaveAccess" src="AdvancedSaveAccess.aspx" frameborder="0"
                    width="0" height="0"></iframe>
            </td>
        </tr>
    </table>
    <p>
        &nbsp;
        <table id="Table1" style="font-size: 12px" cellspacing="1" cellpadding="1" width="480"
            border="0" height="111">
            <tr>
                <td>
                    <span class="style4">�ϴ��ļ�:&nbsp;�ϴ�Ȩ�ޣ��ܹ��ڴ�Ŀ¼�ϴ��ļ� </span>
                </td>
            </tr>
            <tr>
                <td>
                    <span class="style4">����Ŀ¼: ������Ŀ¼Ȩ�ޣ����ڴ�Ŀ¼�´�����Ŀ¼</span>
                </td>
            </tr>
            <tr>
                <td>
                    <span class="style4">������: ������Ȩ�ޣ����Ը��Ĵ�Ŀ¼�µ��ļ������ļ��е�����</span>
                </td>
            </tr>
            <tr>
                <td>
                    <span class="style4">ɾ��: ɾ��Ȩ�ޣ�����ɾ����Ŀ¼�µ��ļ������ļ���</span>
                </td>
            </tr>
        </table>

        <script language="javascript">
				window.document.all['ShareFolderName'].value='<%=SharedName%>';
				//alert(window.document.all['ShareFolderName'].value)
				
				function RoleEdit(id)
				{
				
					window.open ('RoleEdit.aspx?id=' + id,'RoleEdit','width=500,height=400,scrollbars=0,top=170,left=200');
				}
        </script>

    </p>
    </form>
</body>
</html>
