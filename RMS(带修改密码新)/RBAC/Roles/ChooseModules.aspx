<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Roles.ChooseModules" Codebehind="ChooseModules.aspx.cs" %>

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
        #frmSaveAllModules
        {
            width: 124px;
        }
    </style>

    <script language="javascript" event="onbuttonclick" for="Toolbar1">
			switch (event.srcNode.getAttribute('Id'))
			{
				case 'Employee':
					PostModules.txtRoleTypeName.value="??ѡ??..";
					PostModules.txtRoleType.value="0";
					if(ChooseEmpl())
						firSubmit();
					break;
				case 'Department':
					PostModules.txtRoleTypeName.value="??ѡ??..";
					PostModules.txtRoleType.value="1";
					if(ChooseDept())
						firSubmit();
					//window.open('ChooseDepartment.aspx?Id=0','','width=580px,height=460px,resizable=yes,left=' + (screen.availWidth-740)/2 + ',top=' + (screen.availHeight-540)/2 );	
					break;
				case 'Position':
					PostModules.txtRoleTypeName.value="??ѡ??..";
					PostModules.txtRoleType.value="2";
					if(ChoosePosi())
						firSubmit();
					//window.open('personadd.aspx?Id=0','','width=580px,height=460px,resizable=yes,left=' + (screen.availWidth-740)/2 + ',top=' + (screen.availHeight-540)/2 );	
					break;
				case 'Post':
				PostModules.txtRoleTypeName.value="??ѡ??..";
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
				alert('û??ѡ??Ա???????ţ?ְλ????λ');
				return false;
			}	
			var strUrl="";
			strUrl="AllModules.aspx?txtRoleType=" + PostModules.txtRoleType.value;
			strUrl= strUrl + "&txtRoleID=" + PostModules.txtRoleID.value;
			strUrl= strUrl + "&txtRoleName=" + PostModules.txtRoleName.value;
			window.frames['frmAllModules'].location = strUrl;
			window.status = "???ڶ?ȡȨ?ޣ????Ժ?......";
			return false;
			
		}
		
		function SaveModules() {

		    document.getElementById('oToolTip').style.display = '';
			if(PostModules.txtRoleType.value==""
				||PostModules.txtRoleTypeName.value==""
				||PostModules.txtRoleID.value==""
				||PostModules.txtRoleName.value=="")
			{
			    alert('û??ѡ??Ա???????ţ?ְλ????λ');
			    document.getElementById('oToolTip').style.display = 'none';
				return false;
			}
			
			var CheckModules;
			CheckModules=window.frames['frmAllModules'].document.all["TreeView1"].getChildren();
			PostModules.hArrayModuleID.value="";
			PostModules.hArrayModuleIDRemoved.value="";
			collectModules(CheckModules)
			
			
			
			var strUrl="";
			strUrl="SaveAllModules.aspx?hArrayModuleID=" + PostModules.hArrayModuleID.value;
			strUrl +="&hArrayModuleIDRemoved=" + PostModules.hArrayModuleIDRemoved.value;
			strUrl +="&txtRoleType=" + PostModules.txtRoleType.value;
			strUrl= strUrl + "&txtRoleID=" + PostModules.txtRoleID.value;
			strUrl= strUrl + "&txtRoleName=" + PostModules.txtRoleName.value;
			
			window.frames['frmSaveAllModules'].location=strUrl;
			
			PostModules.buttSave.value="???ڱ???...";
			PostModules.buttSave.disabled=true;
			document.getElementById('oToolTip').style.display = 'none';
			return false;
		}
		function collectModules(TreeNodes) {
		    //alert(PostModules.hArrayModuleID.value);debugger;
			if(TreeNodes!=null&&typeof(TreeNodes) != "undefined")
			{
				for(var i=0;i<	TreeNodes.length;i++)
				{
					var currentChild;
					currentChild = TreeNodes[i];
					//alert(PostModules.hArrayModuleID.value);
					//&& currentChild.getAttribute("Text").indexOf("<b>")<0  ?̳?ģ??Ҳ????
					if (currentChild.getAttribute("Checked"))
						PostModules.hArrayModuleID.value +="," + currentChild.getAttribute("NodeData");
					else
						PostModules.hArrayModuleIDRemoved.value +="," + currentChild.getAttribute("NodeData");
					
					var childNodesSub;
					childNodesSub = currentChild.getChildren();
					collectModules(childNodesSub);
				}
			}
				//???ӵ?ģ??
			if(PostModules.hArrayModuleID.value.substring(0,1)==",")
				PostModules.hArrayModuleID.value=PostModules.hArrayModuleID.value.substring(1,PostModules.hArrayModuleID.value.length);
				
			//ɾ????ģ??
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
					alert('û??ѡ??Ա???????ţ?ְλ????λ');
					return false;
				}	
			window.frames['frmAllModules'].location.reload();
		}


		function SaveButtons() {

		    var hAccess = window.frames['frmAccess'].document.all["hAccess"];
		    if (typeof (hAccess) == 'undefined' || PostModules.hModuleIDSelected.value=='') {
		        alert('??ѡ??ģ??');
		        return false;
		    }
		    var CheckModules;

		    var showButton = window.frames['frmAccess'].document.all["showButton"];
		    if (showButton.value == "") {
		        alert('û?в?????ť??Ȩ??');
		        return false;
		    }
		    var sAccess = "";

		    for (var i = 0; i < parseInt(window.frames['frmAccess'].document.all["hAccess"].value, 10); i++) {
		        if (window.frames['frmAccess'].document.all["checkbox" + i].checked)
		            sAccess = "1" + sAccess;
		        else
		            sAccess = "0" + sAccess;
		    }

		    var strUrl = "";
		    strUrl = "SaveOperateButtons.aspx?hArrayModuleID=" + PostModules.hModuleIDSelected.value;
		    strUrl += "&txtRoleType=" + PostModules.txtRoleType.value;
		    strUrl = strUrl + "&txtRoleID=" + PostModules.txtRoleID.value;
		    strUrl = strUrl + "&txtRoleName=" + PostModules.txtRoleName.value;
		    strUrl = strUrl + "&hAccess=" + sAccess;
		    strUrl = strUrl + "&TemName=" + window.frames['frmAccess'].document.all["showButton"].value;



		    //alert(strUrl);
		    //return false;
		    window.frames['frmSaveAllModules'].location = strUrl;

		    PostModules.buttSave.value = "???ڱ???...";
		    PostModules.buttSave.disabled = true;

		    return false;
		}
		function collectModulesButtons(TreeNodes) {
		    if (TreeNodes != null && typeof (TreeNodes) != "undefined") {
		        for (var i = 0; i < TreeNodes.length; i++) {
		            var currentChild;
		            currentChild = TreeNodes[i];
		            if (currentChild.getAttribute("Checked"))
		                PostModules.hArrayModuleID.value += "," + currentChild.getAttribute("NodeData");
		            else
		                PostModules.hArrayModuleIDRemoved.value += "," + currentChild.getAttribute("NodeData");

		            var childNodes;
		            childNodes = currentChild.getChildren();
		            collectModulesButtons(childNodes);
		        }
		    }
		    //???ӵ?ģ??
		    if (PostModules.hArrayModuleID.value.substring(0, 1) == ",")
		        PostModules.hArrayModuleID.value = PostModules.hArrayModuleID.value.substring(1, PostModules.hArrayModuleID.value.length);

		    //ɾ????ģ??
		    if (PostModules.hArrayModuleIDRemoved.value.substring(0, 1) == ",")
		        PostModules.hArrayModuleIDRemoved.value = PostModules.hArrayModuleIDRemoved.value.substring(1, PostModules.hArrayModuleIDRemoved.value.length);
		}
		
	//-->
    </script>

    <script language="javascript" id="clientEventHandlersJS">
<!--
function ChooseEmpl()
{
	var result;
	result = window.showModalDialog('../Organizations/searchperson.aspx','','dialogWidth=30;dialogHeight=30;status=no;scroll=auto');
	if (result != 'undefined' && typeof(result)!='undefined')
	{
		var rv = result.split("|");
		if (rv.length == 2)
		{	PostModules.txtRoleTypeName.value='??Ա??????';
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
	result = window.showModalDialog('ChooseDepartment.aspx?MathFlag='+ Math.random(),'','dialogWidth=30;dialogHeight=32;status=no;scroll=auto');
	if (result != 'undefined' && typeof(result)!='undefined')
	{
		var rv = result.split("|");
		if (rv.length == 2)
		{	PostModules.txtRoleTypeName.value='?????ŷ???';
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
function ChoosePosi() {
    alert('ְλ????Ȩ???߼?δ??????ѡ????????ʽ????Ȩ?޷??䣬??ǰ?Ѿ?????Ȩ?޵?ְλ???ٴ?ѡ??????????');
    return false;
	var result;
	result = window.showModalDialog('ChoosePosition.aspx','','dialogWidth=30;dialogHeight=20;status=no;scroll=yes');
	if (result != 'undefined' && typeof(result)!='undefined')
	{
		var rv = result.split("|");
		if (rv.length == 2)
		{	PostModules.txtRoleTypeName.value='??ְλ????';
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
	result = window.showModalDialog('ChoosePost.aspx','','dialogWidth=30;dialogHeight=30;status=no;scroll=yes');
	if (result != 'undefined' && typeof(result)!='undefined')
	{
		var rv = result.split("|");
		if (rv.length == 2)
		{	PostModules.txtRoleTypeName.value='????λ????';
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
    <DIV id="oToolTip" style="DISPLAY: none;POSITION: absolute;left:200px;top:100px">
			    <div style="BORDER-RIGHT: black 2px solid; PADDING-RIGHT: 10px; BORDER-TOP: black 2px solid; PADDING-LEFT: 10px; FILTER: progid:DXImageTransform.Microsoft.Gradient(GradientType=0, StartColorStr=skyblue, EndColorStr=#FFFFFF); LEFT: 200px; PADDING-BOTTOM: 10px; FONT: 10pt tahoma; BORDER-LEFT: black 2px solid; WIDTH: 170px; PADDING-TOP: 10px; BORDER-BOTTOM: black 2px solid; TOP: 200px; HEIGHT: 120px">
				    <b>??????ʾ?ý?ɫ??<br />Ȩ??ģ???߼??ṹ:</b>
				    <hr style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
					    SIZE="1">
				    <label id="oToolTipContent">:::???Ժ?:::</label>
				    <div></div>
			    </div>
		    </DIV>
    <table height="100%" width="100%" bgcolor="#ffffff">
        <tr bgcolor="lightgrey">
            <td colspan="3" height="38" valign="middle">
                <b><font color="red">
                    <asp:Image ID="Image1" runat="server" ImageUrl="../../images/icons/0060_a.gif" ImageAlign="AbsMiddle">
                    </asp:Image>ָ????Ա??֯????????λ?Ƚ?ɫ??ʹ?ù???</font></b>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f0f0f0" colspan="3" height="17">
                <p align="left">
                    <iewc:Toolbar ID="Toolbar1" runat="server" BorderWidth="0px" Width="480px" BorderColor="Transparent"
                        BackColor="Transparent">
                        <iewc:ToolbarButton Text="&amp;nbsp;????λ????" ImageUrl="../../images/icons/0037_b.gif"
                            DefaultStyle="display;border:solid 1px skyblue;" ID="Post" HoverStyle="border:solid 1px red;">
                        </iewc:ToolbarButton>
                        <iewc:ToolbarButton Text="&amp;nbsp;??ְλ????" ImageUrl="../../images/icons/0063_b.gif"
                            DefaultStyle="display;border:solid 1px skyblue;" ID="Position" HoverStyle="border:solid 1px red;">
                        </iewc:ToolbarButton>
                        <iewc:ToolbarButton Text="&amp;nbsp;?????ŷ???" ImageUrl="../../images/icons/0022_b.gif"
                            DefaultStyle="display;border:solid 1px skyblue;" ID="Department" HoverStyle="border:solid 1px red;">
                        </iewc:ToolbarButton>
                        <iewc:ToolbarButton Text="&amp;nbsp;??Ա??????" ImageUrl="../../images/icons/0019_b.gif"
                            DefaultStyle="display;border:solid 1px skyblue;" ID="Employee" HoverStyle="border:solid 1px red;">
                        </iewc:ToolbarButton>
                    </iewc:Toolbar>
                </p>
            </td>
        </tr>
        <tr>
            <td valign="top" align="center" width="10%" bgcolor="#f0f0f0" colspan="2" height="392">
                <p>
                    &nbsp;</p>
                <table height="124" width="150" border="0">
                    <tr bgcolor="#ffffff">
                        <td valign="middle" align="right" width="56" bgcolor="#f0f0f0" height="37">
                            <p align="right">
                                ??Χ</p>
                        </td>
                        <td valign="middle" align="center" width="107" bgcolor="#f0f0f0" height="37">
                            <p align="left">
                                <asp:TextBox ID="txtRoleTypeName" runat="server" contentEditable="false" Width="95px" Enabled="true"></asp:TextBox></p>
                        </td>
                    </tr>
                    <tr>
                        <td valign="middle" align="right" width="56" bgcolor="#f0f0f0" height="34">
                            ????
                        </td>
                        <td class="unnamed1" valign="top" align="center" bgcolor="#f0f0f0" height="34">
                            <p align="left">
                                <asp:TextBox ID="txtRoleName" runat="server" contentEditable="false" Width="96px"></asp:TextBox></p>
                        </td>
                    </tr>
                    <tr bgcolor="#ffffff">
                        <td valign="middle" align="right" width="56" bgcolor="#f0f0f0" height="34" 
                            nowrap="nowrap">
                            <font size="2">ΨһID</font>
                        </td>
                        <td class="unnamed1" valign="top" align="center" bgcolor="#f0f0f0" height="34">
                            <p align="left">
                                <font size="2">
                                    <asp:TextBox ID="txtRoleID" runat="server" contentEditable="false" Width="97px"></asp:TextBox></font></p>
                        </td>
                    </tr>
                </table>
                <p align="center">
                    &nbsp;<input id="buttSave" type="button" value="????" onclick="return SaveModules()">&nbsp;&nbsp;
                    <input id="buttReset" type="button" value="??λ" onclick="return reloadAll()">
                    <asp:TextBox ID="txtRoleType" runat="server" Width="0px"></asp:TextBox></p>
                <p align="center">
                    <div id="Label1">
                    </div>
                    <p>
                        <font color="blue"><b>??ɫ????????ʾ??</b></font>ģ????????Ȩ?޼̳й?ϵ????
                    </p>
                    <p align="center">
                        <iframe id="frmSaveAllModules" name="frmSaveAllModules" frameborder="0"
                            height="0"></iframe>
                    </p>
                    <p align="left">
                        <input id="buttSaveButtons" onclick="return SaveButtons();" disabled type="button" value="??????????ť??Ȩ??" name="buttSaveButtons"></p>
            </td>
            <td valign="top" align="left" width="90%" bgcolor="#f0f0f0" height="95%">
                <iframe id="frmAllModules" name="frmAllModules" src="AllModules.aspx" frameborder="0"
                    width="100%" height="100%"></iframe>
            </td>
        </tr>
        <tr bgcolor="white" height="30">
            <td align="right" bgcolor="#f0f0f0" colspan="3" height="46">
                <iframe id="frmAccess" name="frmAccess" src="OperateButtons.aspx" frameborder="0" width="100%" height="120"></iframe>
            </td>
        </tr>
    </table>

    <script language="javascript">
				function RoleEdit(id)
				{
					window.open ('RoleEdit.aspx?id=' + id,'RoleEdit','width=500,height=400,scrollbars=0,top=170,left=200');
				}
    </script>

    &nbsp;
    <input id="hArrayModuleID" type="hidden" name="hArrayModuleID"><input id="hArrayModuleIDRemoved"
        type="hidden" name="hArrayModuleIDRemoved">
    <input id="hModuleIDSelected" type="hidden" name="hModuleIDSelected" />
    </form>
</body>
</html>
