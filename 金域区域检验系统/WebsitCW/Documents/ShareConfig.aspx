<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>

<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.Documents.ShareConfig" Codebehind="ShareConfig.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>权限控制</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <style>
        TABLE
        {
            font-weight: normal;
            font-size: 12px;
            color: #000000;
            text-decoration: none;
        }
    </style>

    <script language="javascript" event="onbuttonclick" for="Toolbar1">
			switch (event.srcNode.getAttribute('Id'))
			{
				case 'Employee':
					Form1.txtRoleTypeName.value="请选择..";
					Form1.txtRoleType.value="0";
					if(ChooseEmpl())
						firSubmit();
					break;
				case 'Department':
					Form1.txtRoleTypeName.value="请选择..";
					Form1.txtRoleType.value="1";
					if(ChooseDept())
						firSubmit();
					//window.open('ChooseDepartment.aspx?Id=0','','width=580px,height=460px,resizable=yes,left=' + (screen.availWidth-740)/2 + ',top=' + (screen.availHeight-540)/2 );	
					break;
				case 'Position':
					Form1.txtRoleTypeName.value="请选择..";
					Form1.txtRoleType.value="2";
					if(ChoosePosi())
						firSubmit();
					//window.open('personadd.aspx?Id=0','','width=580px,height=460px,resizable=yes,left=' + (screen.availWidth-740)/2 + ',top=' + (screen.availHeight-540)/2 );	
					break;
				case 'Post':
				Form1.txtRoleTypeName.value="请选择..";
					Form1.txtRoleType.value="3";
					if(ChoosePost())
						firSubmit();
					//window.open('personadd.aspx?Id=0','','width=580px,height=460px,resizable=yes,left=' + (screen.availWidth-740)/2 + ',top=' + (screen.availHeight-540)/2 );	
					break;
			}
    </script>

    <script language="javascript" id="clientEventHandlersJS1">
<!--
function ChooseEmpl()
{
	var result;
	result = window.showModalDialog('../RBAC/Organizations/searchperson.aspx','','dialogWidth=30;dialogHeight=30;status=no;scroll=no');
	if (result != 'undefined' && typeof(result)!='undefined')
	{
		var rv = result.split("|");
		if (rv.length == 2)
		{	Form1.txtRoleTypeName.value='按员工分配';
			Form1.txtRoleName.value = rv[1];
			Form1.txtRoleID.value = rv[0];
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
		{	Form1.txtRoleTypeName.value='按部门分配';
			Form1.txtRoleName.value = rv[1];
			Form1.txtRoleID.value = rv[0];
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
		{	Form1.txtRoleTypeName.value='按职位分配';
			Form1.txtRoleName.value = rv[1];
			Form1.txtRoleID.value = rv[0];
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
		{	Form1.txtRoleTypeName.value='按岗位位分配';
			Form1.txtRoleName.value = rv[1];
			Form1.txtRoleID.value = rv[0];
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
		function firSubmit()
		{
			
			window.frames['ShareAccess'].location='ShareAccess.aspx?FolderID='+'<%=FolderID%>'+'&txtRoleID='+window.document.all["txtRoleID"].value+'&txtRoleType='+window.document.all["txtRoleType"].value;
		}
		function SaveAccess1()
		{
				var sAccess="";
				for(var i=0;i<4;i++)
				{
					if(window.frames['ShareAccess'].document.all["checkbox" + i].checked)
						sAccess="1" + sAccess;
					else
						sAccess="0" + sAccess;
				}
				//sAccess="0"+sAccess
				
				Form1.hAccess.value=sAccess;
				var url='SaveShareAccess.aspx?FolderID='+'<%=FolderID%>';
				url=url+'&txtRoleID='+window.document.all["txtRoleID"].value;
				url=url+'&txtRoleType='+window.document.all["txtRoleType"].value;
				url=url+'&Access='+sAccess;
				window.frames['SaveAccess'].location=url;
				document.all['buttSaveAccess'].disabled=true;
		}		
//-->
    </script>

    <script language="javascript" id="clientEventHandlersJS">
<!--
function window_onload() {
	
	
}
//-->
    </script>

</head>
<body language="javascript" bgcolor="#f0f0f0" onload="return window_onload()" ms_positioning="GridLayout">
    <form id="Form1" name="Form1" method="post" runat="server">
    &nbsp;

    <script language="javascript">
			
			
			
    </script>

    <input style="z-index: 101; left: 104px; width: 152px; position: absolute; top: 480px;
        height: 21px" type="hidden" value="dDwtMTg0NDQ2ODMwODs7PgRSEB6/Hb1/xYakqFY0CHjaRAvF"
        name="__VIEWSTATE">
    <input id="hAccess" style="z-index: 102; left: 408px; position: absolute; top: 480px"
        type="hidden" name="hAccess">
    <input id="txtRoleType" style="z-index: 102; left: 256px; position: absolute; top: 480px"
        type="hidden" name="hAccess">
    <table id="Table2" style="z-index: 103; left: 8px; width: 560px; position: absolute;
        top: 16px; height: 320px" cellspacing="1" cellpadding="1" border="0">
        <tr>
            <td style="width: 76px; height: 62px" align="center">
                <img src="../images/icons/0041_a.gif" align="absBottom">
            </td>
            <td style="height: 62px">
                设置共享文档：
            </td>
        </tr>
        <tr>
            <td style="width: 76px; height: 40px" align="center">
                选择角色：
            </td>
            <td style="width: 148px; height: 40px">
                <iewc:Toolbar ID="Toolbar1" runat="server" BorderStyle="Outset" BackColor="#F0F0F0"
                    BorderColor="Transparent" Width="480px" BorderWidth="0px">
                    <iewc:ToolbarButton ID="Post" Text="&amp;nbsp;按岗位分配" HoverStyle="border:solid 1px red;"
                        DefaultStyle="display;border:solid 1px skyblue;" ImageUrl="../images/icons/0037_b.gif">
                    </iewc:ToolbarButton>
                    <iewc:ToolbarButton ID="Position" Text="&amp;nbsp;按职位分配" HoverStyle="border:solid 1px red;"
                        DefaultStyle="display;border:solid 1px skyblue;" ImageUrl="../images/icons/0063_b.gif">
                    </iewc:ToolbarButton>
                    <iewc:ToolbarButton ID="Department" Text="&amp;nbsp;按部门分配" HoverStyle="border:solid 1px red;"
                        DefaultStyle="display;border:solid 1px skyblue;" ImageUrl="../images/icons/0022_b.gif">
                    </iewc:ToolbarButton>
                    <iewc:ToolbarButton ID="Employee" Text="&amp;nbsp;按员工分配" HoverStyle="border:solid 1px red;"
                        DefaultStyle="display;border:solid 1px skyblue;" ImageUrl="../images/icons/0074_b.gif">
                    </iewc:ToolbarButton>
                </iewc:Toolbar>
            </td>
        </tr>
        <tr>
            <td style="height: 65px" align="center">
            </td>
            <td style="height: 65px">
                <asp:TextBox ID="txtRoleTypeName" runat="server" contentEditable="false" Width="95px" Enabled="true"></asp:TextBox><asp:TextBox
                    ID="txtRoleName" runat="server" contentEditable="false" Width="96px"></asp:TextBox><asp:TextBox
                        ID="txtRoleID" runat="server" contentEditable="false" Width="97px" OnTextChanged="txtRoleID_TextChanged"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="height: 110px" align="center">
                共享权限：
            </td>
            <td style="height: 110px">
                <iframe id="ShareAccess" src="ShareAccess.aspx?FolderID=<%=Request.QueryString["FolderID"]%>&txtRoleID=<%=Request.QueryString["RoleID"]%>&txtRoleType=<%=Request.QueryString["txtRoleType"]%>"
                    frameborder="0" height="100" style="width: 100%; height: 109px"></iframe>
            </td>
        </tr>
        <tr>
            <td style="width: 76px" align="center">
            </td>
            <td align="center">
                <input type="button" value="确 定" id="buttSaveAccess" onclick="SaveAccess1();">&nbsp;
            </td>
        </tr>
    </table>
    <iframe id="Save" name="SaveAccess" frameborder="0" width="0" height="0"></iframe>

    <script language="javascript">
			window.document.all["txtRoleTypeName"].value='按<%=Request.QueryString["TypeName"]%>分配';
			window.document.all["txtRoleName"].value='<%=Request.QueryString["Name"]%>';
			window.document.all["txtRoleID"].value='<%=Request.QueryString["RoleID"]%>';
			window.document.all["txtRoleType"].value='<%=Request.QueryString["txtRoleType"]%>';
    </script>

    </form>
</body>
</html>
