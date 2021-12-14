<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.Main.Desktop.DesktopConfig" Codebehind="DesktopConfig.aspx.cs" %>

<%@ Import Namespace="System.Xml" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Desktop</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../../Includes/CSS/ioffice.css" type="text/css" rel="stylesheet">

    <script language="javascript" id="clientEventHandlersJS">
		<!--

		function buttRemoteToolBox_onclick() {
			//window.open('RemoteTools.aspx','','width=550px,height=500px,scrollbars=yes,resizable=yes,left=' + (screen.availWidth-620)/2 + ',top=' + (screen.availHeight-470)/2 );	
			var r;
			var r=window.showModalDialog('../../PopupSelectDialog.aspx?main/desktop/RemoteTools.aspx','','dialogWidth:588px;dialogHeight:618px;help:no;scroll:no;status:no');
			if (r == '' || typeof(r) == 'undefined'||typeof(r)=='object')
			{
				document.location.href=document.location.href;
			}
		}

		function buttLocalToolBox_onclick() {
			window.open('LocalTools.aspx','','width=550px,height=500px,scrollbars=yes,resizable=yes,left=' + (screen.availWidth-620)/2 + ',top=' + (screen.availHeight-470)/2 );	
		}

		var id="";
		var status=0;
		var topY=120;
		var leftX=240;

		var beginPosition='left';
		var endPostion='left';
		function beginDrag(position,i,txt)
		{
			beginPosition=position;
			id=i;
			status=1;
			
			hover.style.top=event.screenY-topY;
			hover.style.left=event.screenX-leftX;
			hover.innerHTML=txt;
			hover.style.display="";
		}

		function move()
		{
			if(status==1)
			{
				hover.style.top=event.screenY-topY;
				hover.style.left=event.screenX-leftX;
			}
		}

		function endDrag(position,i)
		{
			endPostion=position;
			hover.style.display="none";
			if(status==1)
				location='DesktopConfig.aspx?sub=drag&a=' + beginPosition
				+ '&b=' + endPostion + '&x=' + id + '&y=' + i + '&RBACModuleID=<%=Request.QueryString["RBACModuleID"]%>';
			status=0;
			
		}

		function stop()
		{
			hover.style.display="none";
			status=0;
		}
		
		function buttAdd_onclick() {
			r=window.showModalDialog('AddDesktopItem.aspx','','width=550px,height=500px,scrollbars=no,resizable=yes,left=' + (screen.availWidth-620)/2 + ',top=' + (screen.availHeight-470)/2 );	
			if(typeof(r) != 'undefined' && r != 'undefined' && r != ''&&r!=-1)
			{
				var rv = r.split(",");
				document.all['sub'].value='addnode';
				document.all['position'].value=rv[0];				
				document.all['style'].value=rv[1];				
				document.all['size'].value=rv[2];
				
				document.all['Name'].value=rv[3];
				
				document.all['ParentId'].value=rv[4];
				
				document.all['tooltip'].value=rv[5];
				
				document.all['apara'].value=rv[6];
				
				document.all['ImageUrl'].value=rv[7];
				
				document.all['DataFrom'].value=rv[8];
				
				document.all['desktop'].value=rv[9];
				
				window.Form1.submit();
				//location='DesktopConfig.aspx?' + r;
			}
		}
		function Deletenode(node,position)
		{
			document.all['deletenode'].value=node;
			document.all['position'].value=position;
			document.all['sub'].value='deletenode';
			Form1.submit();
		}
		//-->
    </script>

</head>
<body onmouseup="stop()" onmousemove="move()" onselectstart="return false;" bottommargin="0"
    leftmargin="12" topmargin="12" rightmargin="0">
    <div id="hover" style="border-right: #ffffcc 1px double; border-top: #ffffcc 1px double;
        display: none; font-size: 8pt; border-left: #ffffcc 1px double; color: #ffffff;
        border-bottom: #ffffcc 1px double; position: absolute; background-color: #0000ff">
        aaa
    </div>
    <form id="Form1" method="post" runat="server">
    <table bordercolor="#99ccff" cellspacing="15" cellpadding="0" border="1">
        <tr>
            <td align="center" valign="top">
                <img src="../../images/icons/0031_a.gif">
            </td>
            <td align="center" valign="top">
                <img src="../../images/icons/0031_a.gif">
            </td>
            <td align="center" valign="top">
                <img src="../../images/icons/0075_a.gif">
            </td>
        </tr>
        <tr>
            <td valign="top">
                <table id="left" cellspacing="0" cellpadding="0" width="200" border="0">
                    <%
                        int i = 0;
                        if (nodesLeft != null)
                        {
                            foreach (XmlNode mynode in nodesLeft)
                            {
                                i++;
                                string txt = mynode.Attributes.GetNamedItem("Text").InnerXml;
                                string node = mynode.Attributes.GetNamedItem("NodeData").InnerXml;
                    %>
                    <tr height="15">
                        <td colspan="2" onmouseup="endDrag('left','<%=node%>')" onmouseover="this.style.backgroundColor='skyblue'"
                            style="background-color: white" onmouseout="this.style.backgroundColor='white';">
                            &nbsp;
                        </td>
                    </tr>
                    <tr onmousedown="beginDrag('left','<%=node%>','<%=txt%>')">
                        <td style="border-right: #ccccff 1px solid; border-top: #ccccff 1px solid; font-size: 10pt;
                            border-left: #ccccff 1px solid; border-bottom: #ccccff 1px solid; font-family: 宋体"
                            align="center">
                            <b>
                                <%=txt%></b>
                        </td>
                        <td width="15">
                            <img onclick="Deletenode('<%=node%>','left');" style="cursor: hand" src="../Images/iButton_Close_Normal.gif">
                        </td>
                    </tr>
                    <%}
                        }%>
                </table>
            </td>
            <td valign="top">
                <table cellspacing="0" cellpadding="0" width="200" border="0">
                    <%
                        i = 0;
                        if (nodesRight != null)
                        {
                            foreach (XmlNode mynode in nodesRight)
                            {
                                i++;
                                string txt = mynode.Attributes.GetNamedItem("Text").InnerXml;
                                string node = mynode.Attributes.GetNamedItem("NodeData").InnerXml;
                    %>
                    <tr height="15">
                        <td colspan="2" onmouseup="endDrag('right','<%=node%>')" onmouseover="this.style.backgroundColor='skyblue'"
                            style="background-color: white" onmouseout="this.style.backgroundColor='white';">
                            &nbsp;
                        </td>
                    </tr>
                    <tr onmousedown="beginDrag('right','<%=node%>','<%=txt%>')">
                        <td style="border-right: #ccccff 1px solid; border-top: #ccccff 1px solid; font-size: 10pt;
                            border-left: #ccccff 1px solid; border-bottom: #ccccff 1px solid; font-family: 宋体"
                            align="center">
                            <b>
                                <%=txt%></b>
                        </td>
                        <td>
                            <img onclick="Deletenode('<%=node%>','right');" style="cursor: hand" src="../Images/iButton_Close_Normal.gif">
                        </td>
                    </tr>
                    <%}
                        }%>
                </table>
            </td>
            <td align="center" valign="top">
                <table cellspacing="0" cellpadding="0" width="150" border="0">
                    <%
                        if (nodesRemoteTools != null)
                        {
                            foreach (XmlNode mynode1 in nodesRemoteTools)
                            {%>
                    <tr bgcolor="Gainsboro">
                        <td colspan="2" align="center">
                            <b>
                                <%=mynode1.Attributes.GetNamedItem("Text").InnerXml%></b>
                        </td>
                    </tr>
                    <%foreach (XmlNode mynode in mynode1.ChildNodes)
                      {%>
                    <tr>
                        <td width="50" height="23" align="center">
                            <img src='<%=mynode.Attributes.GetNamedItem("ImageUrl").InnerXml%>'>
                        </td>
                        <td style="font-size: 10pt; font-family: 宋体" align="left">
                            <%=mynode.Attributes.GetNamedItem("Text").InnerXml%>
                        </td>
                    </tr>
                    <%}%>
                    <%}
                        }%>
                </table>
                <br>
                <input language="javascript" id="buttRemoteToolBox" onclick="return buttRemoteToolBox_onclick()"
                    type="button" value="工具箱管理">&nbsp;
                <br>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <input id="buttAdd" type="button" value="添加..." language="javascript" onclick="return buttAdd_onclick()"><font
                    face="宋体">&nbsp;&nbsp; </font>
                <input id="buttTemplate" type="button" value="保存(/新)模版" onclick="SaveNewTheme();"><input
                    type="text" id="newThemeName" name="newThemeName" style="width: 110px" runat="server">
                <input type="button" id="applytheme" name="applytheme" value="应用主题" onclick="apply();"
                    title="配置应用到所有用户模板">
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                模板主题列表：<select id="ThemeList" name="ThemeList" onchange="selecttheme()">
                    <option value="0" selected>请选择主题</option>
                    <%if (ThemeList != null)
                      {
                          foreach (XmlNode XN in ThemeList)
                          {%>
                    <option value='<%=XN.Attributes.GetNamedItem("Text").InnerXml%>'>
                        <%=XN.Attributes.GetNamedItem("Text").InnerXml%></option>
                    <%}
                      }%>
                </select>
                <input type="button" id="loadtheme" name="loadtheme" value="调出主题" style="display: none"
                    onclick="load();">&nbsp;
                <input type="button" id="deltheme" name="deltheme" value="删除主题" style="display: none"
                    onclick="del();">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
    </table>
    <input type="text" id="sub" name="sub" style="width: 0px">
    <input type="text" id="Name" name="Name" style="width: 0px">
    <input type="text" id="deletenode" name="deletenode" style="width: 0px">
    <input type="text" id="position" name="position" style="width: 0px">
    <input type="text" id="style" name="style" style="width: 0px">
    <input type="text" id="size" name="size" style="width: 0px">
    <input type="text" id="ParentId" name="ParentId" style="width: 0px">
    <input type="text" id="tooltip" name="tooltip" style="width: 0px">
    <input type="text" id="apara" name="apara" style="width: 0px">
    <input type="text" id="ImageUrl" name="ImageUrl" style="width: 0px">
    <input type="text" id="DataFrom" name="DataFrom" style="width: 0px">
    <input type="text" id="desktop" name="desktop" style="width: 0px">
    <asp:Label ID="LblMSG" runat="server" BorderWidth="1px" BorderStyle="Solid" BorderColor="#FF8080"
        Width="448px"></asp:Label>
    </form>

    <script language="javascript">
			function selecttheme()
			{
				if (document.all("ThemeList").value != "0")
				{
					document.all("loadtheme").style.display = '';
					document.all("deltheme").style.display = '';
					//document.all("applytheme").style.display='';
				}
				if (document.all("ThemeList").value == 0)
				{
					document.all("loadtheme").style.display = 'none';
					document.all("deltheme").style.display = 'none';
					//document.all("applytheme").style.display = 'none';
					//window.location = 'DeskTopDefine.aspx';
				}
			}
			
			function load()
			{
				document.all['sub'].value='loadnew';
				document.all['Name'].value=document.all("ThemeList").value;
				Form1.submit();
				//window.location = 'DesktopConfig.aspx?loadnew=yes&ThemeName=' + document.all("ThemeList").value;
			}
			
			function apply()
			{
				document.all['sub'].value='applynew';
				document.all['Name'].value=document.all("ThemeList").value;
				Form1.submit();
			}
			
			function del()
			{
				document.all['sub'].value='del';
				Form1.submit();
			}
			function SaveNewTheme()
			{
				if(document.all["newThemeName"].value=='')
				{
					alert("请输入模版名！");
					return false;
				}
				document.all['sub'].value='savenew';
				document.all['Name'].value=document.all("newThemeName").value;
				Form1.submit();
				//window.location = 'DesktopConfig.aspx?savenew=yes&ThemeName=' + document.all("newThemeName").value;
			}
    </script>

</body>
</html>
