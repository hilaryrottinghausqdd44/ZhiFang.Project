<%@ Import Namespace="Microsoft.Web.UI.WebControls" %>
<%@ Register TagPrefix="mytab" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>

<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Modules.ModuleRole" Codebehind="ModuleRole.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>模块管理子页面</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <link href="../../Includes/CSS/ioffice.css" type="text/css" rel="stylesheet">

    <script language="javascript" id="clientEventHandlersJS">
		var SaveType;
		function RemoveRole( RoleType, RoleID)
		{
			
			window.location='ModuleRole.aspx?ModuleID=<%=thisModuleID%>&RoleType='+RoleType+'&RoleID='+RoleID+'&del=yes';
		}
		function saveall()
		{
			SaveModulesBut();
			//SaveModules();
		}
		function SaveModulesBut()
		{			
			// 取得权限
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
			
			var Access="";
			for(var i=0;i<=parseInt(hAccess.value,10);i++)
			{
				if(window.frames['frmAccess'].document.all["checkbox" + i].checked)
					Access="1" + Access;
				else
					Access="0" + Access;
			}
			//
			var hAccess=window.frames['frmOperate'].document.all["hAccess"];
			if(typeof(hAccess)=='undefined')
			{
				alert('还要选择模块');
				return false;
			}
			
			//var CheckModules;			
			//var showButton=window.frames['frmOperate'].document.all["showButton"];
			//if(showButton.value=="")
			//{
			//	alert('没有选定任何模板');
			//	return false;
			//}
			var sAccess="";		
			
			for(var i=0;i<parseInt(window.frames['frmOperate'].document.all["hAccess"].value,10);i++)
			{
				if(window.frames['frmOperate'].document.all["checkbox" + i].checked)
					sAccess="1" + sAccess;
				else
					sAccess="0" + sAccess;
			}		
			
			var strUrl="";
			var ModuleIDRemoved="";
			strUrl="Saveall.aspx?hArrayModuleID=<%=thisModuleID%>";
			strUrl +="&hArrayModuleIDRemoved=" + ModuleIDRemoved;
			strUrl +="&txtRoleType="+SaveType ;
			strUrl= strUrl + "&txtRoleID=" + theform.txtRoleID.value;
			strUrl= strUrl + "&txtRoleName=" + theform.RoleType.value;
			strUrl= strUrl + "&hAccess=" + sAccess;
			strUrl= strUrl + "&xAccess=" + Access;
			strUrl=strUrl+"&TemName="+window.frames['frmOperate'].document.all["showButton"].value;
			
			window.frames['frmSaveAllModules'].location=strUrl;
			
			//theform.buttSave.value="正在保存...";
			//theform.buttSave.disabled=true;
			
			return false;
		}
		function SaveModules()
		{		
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
			
			var sAccess="";
			for(var i=0;i<=parseInt(hAccess.value,10);i++)
			{
				if(window.frames['frmAccess'].document.all["checkbox" + i].checked)
					sAccess="1" + sAccess;
				else
					sAccess="0" + sAccess;
			}
			
			var strUrl="";
			var RemoveModule="";
			strUrl="../Roles/SaveAccess.aspx?hArrayModuleID=<%=thisModuleID%>";
			strUrl +="&hArrayModuleIDRemoved=" + RemoveModule;
			strUrl +="&txtRoleType="+SaveType;
			strUrl= strUrl + "&txtRoleID=" + theform.txtRoleID.value;
			strUrl= strUrl + "&txtRoleName=" + theform.RoleType.value;
			strUrl= strUrl + "&hAccess=" + sAccess;
			window.frames['frmSaveAllModules'].location=strUrl;
			
			
			
			return false;
		}
<!--


function window_onload() {
	

}
function ChooseRole()
{
	var ChooseType;
	var RadioCollection=document.all["R1"];
	for(var i=0;i<RadioCollection.length;i++)
	{
		if(RadioCollection[i].checked)
			ChooseType=RadioCollection[i].value;
	}
	
	
	switch (ChooseType)
	{
		case 'employee':
			ChooseEmpl();
			theform.txtRoleType.value='0';
			firSubmit();
			break;
		case 'department':
			ChooseDept();
			theform.txtRoleType.value='1';
			firSubmit();
			break;
		case 'position':
			ChoosePosi();
			theform.txtRoleType.value='2';
			firSubmit();
			break;
		case 'post':
			ChoosePost();
			theform.txtRoleType.value='3';
			firSubmit();
			break;
		default:
			alert('请选择一种角色' + typeof(ChooseType));
			break;
		
	}
					
}
function ChooseEmpl()
{
	var result;
	result = window.showModalDialog('../Organizations/searchperson.aspx','','dialogWidth=30;dialogHeight=30;status=no;scroll=no');
	if (result != 'undefined' && typeof(result)!='undefined')
	{
		var rv = result.split("|");
		if (rv.length == 2)
		{	
			//PostModules.txtRoleTypeName.value='按员工分配';
			theform.txtRoleName.value = rv[1];
			theform.txtRoleID.value = rv[0];
			theform.RoleType.value=rv[1];
			RoleLabel.innerHTML="<label>按员工分配：</label>";
			SaveType='0';
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
	result = window.showModalDialog('../Roles/ChooseDepartment.aspx','','dialogWidth=30;dialogHeight=32;status=no;scroll=no');
	if (result != 'undefined' && typeof(result)!='undefined')
	{
		var rv = result.split("|");
		if (rv.length == 2)
		{	//PostModules.txtRoleTypeName.value='按部门分配';
			theform.txtRoleName.value = rv[1];
			theform.txtRoleID.value = rv[0];
			theform.RoleType.value=rv[1];
			RoleLabel.innerHTML="<label>按部门分配：</label>";
			SaveType='1';
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
	result = window.showModalDialog('../Roles/ChoosePosition.aspx','','dialogWidth=30;dialogHeight=20;status=no;scroll=yes');
	if (result != 'undefined' && typeof(result)!='undefined')
	{
		var rv = result.split("|");
		if (rv.length == 2)
		{	
			theform.txtRoleName.value = rv[1];
			theform.txtRoleID.value = rv[0];
			theform.RoleType.value=rv[1];
			RoleLabel.innerHTML="<label>按职位分配：</label>";
			SaveType='2';
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
	result = window.showModalDialog('../Roles/ChoosePost.aspx','','dialogWidth=30;dialogHeight=20;status=no;scroll=yes');
	if (result != 'undefined' && typeof(result)!='undefined')
	{
		var rv = result.split("|");
		if (rv.length == 2)
		{	
			theform.txtRoleName.value = rv[1];
			theform.txtRoleID.value = rv[0];
			theform.RoleType.value=rv[1];
			RoleLabel.innerHTML="<label>按岗位分配：</label>";
			SaveType='3';
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
			if(theform.txtRoleType.value==""				
				||theform.txtRoleID.value==""
				||theform.txtRoleName.value=="")
			{
				alert('没有选择员工，部门，职位或岗位');
				return false;
			}	
			var strUrl="";
			strUrl="../Roles/AccessConfig.aspx?txtRoleType=" + theform.txtRoleType.value;
			strUrl= strUrl + "&txtRoleID=" + theform.txtRoleID.value;
			strUrl= strUrl + "&ModuleID=<%=thisModuleID%>";
			//strUrl= strUrl + "&txtRoleName=" + PostModules.txtRoleName.value;
			window.frames['frmAccess'].location=strUrl;
			
			var strUrl1="";
			strUrl1="../Roles/OperateButtons.aspx?txtRoleType=" + theform.txtRoleType.value;
			strUrl1= strUrl1 + "&ModuleID=<%=thisModuleID%>";
			strUrl1= strUrl1 + "&txtRoleID=" + theform.txtRoleID.value;
			strUrl1= strUrl1 + "&TemName=<%=TemName%>";
			window.frames['frmOperate'].location=strUrl1;
			
			//window.frames['frmAccess'].location=strUrl;
			return false;
			
		}

function IsValidate()
{
	
	var temname=theform.CName.value;
	if(temname.length==0)
	{
		alert('模块名不能为空！');
		return false;
	}
	if(temname.indexOf('\'')>-1)
	{
		alert('模块名不能包含单引号!');
		return false;
	}
	//	
}

function selectImage()
{
	r=window.showModalDialog('selectModuleImage.aspx','','dialogWidth:588px;dialogHeight:618px;help:no;scroll:auto;status:no');
	if (r == '' || typeof(r) == 'undefined')
	{
		return;
	}
	else
	{
		document.all("ModuleImage").src="../../images/icons/" + r;
		document.all("selectImg").title=r;
		document.all("imgPickPic").value= r;
		//document.all("buttPickPic").title=r;
		
	}
}
//-->
    </script>

    <link href="../../Includes/CSS/ioffice.css" type="text/css" rel="stylesheet">
</head>
<body language="javascript" bottommargin="0" leftmargin="0" topmargin="4" onload="return window_onload()"
    rightmargin="0" ms_positioning="GridLayout">
    <form id="theform" name="theform" onsubmit="return IsValidate()" method="post" runat="server">
    <h4>
        <img id="ModuleImage" height="16" src="../../images/icons/htmlicon.gif" width="16"
            border="0" runat="server">
        模块管理步骤</h4>
    模块名称:<asp:TextBox ID="CName" Style="z-index: 101" runat="server" BorderStyle="None"></asp:TextBox>
    <asp:Label ID="lblMSG" runat="server"></asp:Label><input id="imgPickPic" type="hidden"
        value="<%=imageSelected%>" name="imgPickPic">
    <hr>
    <table style="height: 232px" cellspacing="0" cellpadding="0" border="0">
        <tr>
            <td valign="top">
                <mytab:TabStrip ID="tsVert" Style="font-weight: bold; font-size: x-small" runat="server"
                    TargetID="mpVert" Width="100%" TabDefaultStyle="height:90;border:solid 1px black;background:#dddddd;padding-left:10px;padding-right:10px;padding-top:0px;padding-bottom:0px"
                    TabHoverStyle="color:red" TabSelectedStyle="border:solid 1px black;border-bottom:none;background:white;padding-left:0px;padding-right:0px;padding-top:10px;padding-bottom:10px"
                    SepDefaultStyle="border-bottom:solid 1px #000000;">
                    <mytab:Tab Text="一:描述"></mytab:Tab>
                    <mytab:TabSeparator></mytab:TabSeparator>
                    <mytab:Tab Text="二:选择具体角色"></mytab:Tab>
                    <mytab:TabSeparator></mytab:TabSeparator>
                    <mytab:Tab Text="三:设置访问权限"></mytab:Tab>
                    <mytab:TabSeparator></mytab:TabSeparator>
                    <mytab:Tab Text="四:设置操作权限"></mytab:Tab>
                    <mytab:TabSeparator></mytab:TabSeparator>
                    <mytab:Tab Text="五:设置时段"></mytab:Tab>
                    <mytab:TabSeparator DefaultStyle="height:100%;"></mytab:TabSeparator>
                </mytab:TabStrip>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <mytab:MultiPage ID="mpVert" Style="border-right: #000000 1px solid; padding-right: 5px;
                    border-top: #000000 0px solid; padding-left: 5px; padding-bottom: 5px; border-left: #000000 1px solid;
                    padding-top: 5px; border-bottom: #000000 1px solid" runat="server" Width="648px"
                    Height="308px">
                    <mytab:PageView>
                        <div>
                            &nbsp;</div>
                        <table style="font-size: 10pt" width="503" bgcolor="#ffffff">
                            <tr bgcolor="white">
                                <td style="width: 98px" align="right" width="98" bgcolor="#f0f0f0" height="25">
                                    功能描述
                                </td>
                                <td colspan="3" align="right" width="409" bgcolor="#f0f0f0" colspan="3" height="25">
                                    <p align="left">
                                        <asp:TextBox ID="Descr" runat="server" Width="400px" Height="80px" TextMode="MultiLine"
                                            BorderStyle="None"></asp:TextBox></p>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    按岗位总览
                                </td>
                                <td>
                                    按职位总览
                                </td>
                                <td>
                                    按部门总览
                                </td>
                                <td>
                                    按员工总览
                                </td>
                            </tr>
                            <tr style="font-size: 10pt; font-family: 宋体">
                                <%=DelRoles%>
                            </tr>
                        </table>
                    </mytab:PageView>
                    <mytab:PageView>
                        <div>
                            &nbsp;</div>
                        <table width="100%" bgcolor="#FFFFFF">
                            <tr bgcolor="white">
                                <td align="right" bgcolor="#F0F0F0" width="100">
                                    <p align="right">
                                        <font size="2">
                                            <input type="radio" value="post" checked name="R1"></font></p>
                                </td>
                                <td bgcolor="#F0F0F0">
                                    <font size="2">指定岗位</font>
                                </td>
                                <td rowspan="5" bgcolor="#F0F0F0" width="450" valign="top" nowrap>
                                    <div id="RoleLabel">
                                    </div>
                                    <input id="RoleType" type="text" name="RoleType">
                                </td>
                            </tr>
                            <tr>
                                <td align="right" bgcolor="#F0F0F0">
                                    <p align="right">
                                        <font size="2">
                                            <input type="radio" value="position" name="R1"></font>
                                </td>
                                <td bgcolor="#F0F0F0">
                                    <font size="2">指定职位</font>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" bgcolor="#F0F0F0">
                                    <p align="right">
                                        <font size="2">
                                            <input type="radio" value="department" name="R1"></font>
                                </td>
                                <td bgcolor="#F0F0F0">
                                    <font size="2">指定部门</font>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" bgcolor="#F0F0F0">
                                    <p align="right">
                                        <font size="2">
                                            <input type="radio" value="employee" name="R1"></font>
                                </td>
                                <td bgcolor="#F0F0F0">
                                    <font size="2">指定员工</font>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" bgcolor="#F0F0F0" colspan="2">
                                    <input type="button" value="选择" onclick="ChooseRole();">
                                </td>
                            </tr>
                        </table>
                    </mytab:PageView>
                    <mytab:PageView>
                        <table width="100%" style="clip: rect(auto auto auto 50px); height: 85px" cellspacing="0"
                            cellpadding="0">
                            <tr>
                                <td valign="top">
                                    <iframe id="frmAccess" frameborder="0" width="100%" height="100%" src="../Roles/AccessConfig.aspx"
                                        scrolling="no"></iframe>
                                </td>
                            </tr>
                        </table>
                    </mytab:PageView>
                    <mytab:PageView>
                        <table width="100%" style="clip: rect(auto auto auto 50px); height: 200px" cellspacing="0"
                            cellpadding="0">
                            <tr>
                                <td valign="top" style="height: 200px">
                                    <iframe id="frmOperate" frameborder="0" width="100%" height="100%" src="../Roles/OperateButtons.aspx">
                                    </iframe>
                                </td>
                            </tr>
                        </table>
                    </mytab:PageView>
                    <mytab:PageView>
                        <table width="100%" style="clip: rect(auto auto auto 50px); height: 260px" cellspacing="0"
                            cellpadding="0">
                            <tr>
                                <td valign="top" style="height: 260px">
                                    <iframe id="frmValidity" frameborder="0" width="100%" height="100%" src="../Tools/BeginEndTime.aspx">
                                    </iframe>
                                </td>
                            </tr>
                        </table>
                    </mytab:PageView>
                </mytab:MultiPage>
            </td>
        </tr>
    </table>
    <table width="660">
        <tr>
            <td align="center">
                <font color="red">本功能有问题，请不要使用它配置权限</font>
                <input onclick="saveall();" id="buttSave" type="button" value="确定" disabled><label id="Label1"></label>
            </td>
        </tr>
        <tr bgcolor="white" height="0">
            <td align="right" bgcolor="#f0f0f0" colspan="2" height="0">
                <iframe id="frmSaveAllModules" name="frmSaveAllModules" frameborder="0" width="0"
                    height="0"></iframe>
            </td>
        </tr>
    </table>
    <br>
    <input id="txtRoleName" style="display: none" type="text" name="txtRoleName"><input
        id="txtRoleType" style="display: none" type="text" name="txtRoleType"><input id="txtRoleID"
            style="display: none" type="text" name="txtRoleID">

    <script language="javascript">
		//var strUrl1="";
		//	strUrl1="../Roles/OperateButtons.aspx?txtRoleType=" + theform.txtRoleType.value;
		//	strUrl1= strUrl1 + "&ModuleID=<%=thisModuleID%>";
		//	strUrl1= strUrl1 + "&txtRoleID=" + theform.txtRoleID.value;
		//	strUrl1= strUrl1 + "&TemName=<%=TemName%>";
		//	window.frames['frmOperate'].location=strUrl1;
    </script>

    </form>
</body>
</html>
