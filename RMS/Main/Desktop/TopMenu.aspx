<%@ Page language="c#" Codebehind="TopMenu.aspx.cs" AutoEventWireup="false" Inherits="OA.Main.Desktop.TopMenu" %>
<%@ Import Namespace="System.Xml"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RemoteTools</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Includes/CSS/ioffice.css" type="text/css" rel="stylesheet">
		<script language="javascript">
			function preview()
			{
				window.open('../topmenu.aspx','','width=700px,height=300px,resizable=yes,');			
			}
				
			function VerifyFormEmployee()
			{
				if(FormDelmoban.TextBoxText.value.length==0)
				{
					alert('名称不能为空！')
					return false
				}
			
			}	
			
			function buttSelectTarget_onclick() 
			{
				r=window.showModalDialog('selectTarget.aspx','','width=200px,height=200px,resizable=yes,left=' + (screen.availWidth-620)/2 + ',top=' + (screen.availHeight-470)/2 );	
				FormDelmoban.TextBoxPlace.value=r;

			}
			
			function ButtSelectFile_onclick() 
			{
				var r;
				r=window.open('../../library/XPathAnalyzer/ContentPane.aspx','','width=550px,height=500px,scrollbars=yes,resizable=yes,left=' + (screen.availWidth-620)/2 + ',top=' + (screen.availHeight-470)/2 );	
				if (r != '' && typeof(r) != 'undefined'&&typeof(r)!='object')
				{
				document.all['txtXML'].value=r;
				}
			}
			var sel;
			function Delmoban(id)
			{
				if (confirm('您真的要删除此工具吗？'))
				{
				
					FormDelmoban.delID.value=id;
					FormDelmoban.submit();
				}
			}
		
			function Editmoban(Text,NavigateUrl,ImageUrl,Target)
				{
				
				if(Text!='')
				FormDelmoban.TextBoxText.value=Text;
				FormDelmoban.MID.value=Text;
				FormDelmoban.TextBoxNavigateUrl.value=NavigateUrl;
				FormDelmoban.txtXML.value=ImageUrl;
				FormDelmoban.TextBoxPlace.value=Target;
				document.all['ButtonModify'].disabled=false;
			
				}
			
			var SelEmpl = '';
			
			function SelectEmpl(eid)
			{
				if (SelEmpl != '')
				{
					document.all['NM'+SelEmpl].style.backgroundColor = '';
					document.all['NM'+SelEmpl].style.color = '';
				}
				
				SelEmpl = eid;				
				document.all['NM'+SelEmpl].style.backgroundColor = 'gold';
				document.all['NM'+SelEmpl].style.color = 'black';
			}
		</script>
	</HEAD>
	<body>
		<form id="FormDelmoban" onsubmit="return VerifyFormEmployee()" method="post" runat="server">
			<TABLE id="Table1" style="WIDTH: 456px; BORDER-COLLAPSE: collapse; HEIGHT: 200px" cellSpacing="1"
				cellPadding="1" width="456" border="1">
				<TR bgColor="steelblue" height="30">
					<TD noWrap width="1%">&nbsp;&nbsp; <IMG src="../../images/icons/0019_a.gif" align="absBottom">
					</TD>
					<TD><B>顶部定制</B>
					</TD>
				</TR>
				<TR bgColor="#e0e0e0">
					<TD style="WIDTH: 71px" align="center"><FONT face="宋体">项目</FONT></TD>
					<TD align="center"><FONT face="宋体">输入参数</FONT></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 71px" align="center"><asp:label id="Label2" runat="server">名称</asp:label></TD>
					<TD><FONT face="宋体"><asp:textbox id="TextBoxText" runat="server" Width="392px"></asp:textbox></FONT></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 71px" align="center"><asp:label id="Label3" runat="server">地址</asp:label></TD>
					<TD><asp:textbox id="TextBoxNavigateUrl" runat="server" Width="392px"></asp:textbox></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 71px" align="center"><asp:label id="Label4" runat="server">图片地址</asp:label></TD>
					<TD><asp:textbox id="txtXML" runat="server" Width="344px"></asp:textbox><INPUT style="WIDTH: 48px; HEIGHT: 22px" onclick="return ButtSelectFile_onclick()" type="button"
							value="选择"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 71px" align="center"><FONT face="宋体"><asp:label id="Label5" runat="server">窗口位置</asp:label></FONT></TD>
					<TD align="center"><asp:textbox id="TextBoxPlace" runat="server" Width="341px"></asp:textbox><INPUT style="WIDTH: 46px; HEIGHT: 22px" onclick="return buttSelectTarget_onclick()" type="button"
							value="选择"></TD>
				</TR>
				<TR>
					<TD align="center" colSpan="2"><asp:button id="ButtonAdd" runat="server" Width="38px" Text="添加"></asp:button><FONT face="宋体">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						</FONT>
						<asp:button id="ButtonModify" runat="server" Text="修改"></asp:button></TD>
				</TR>
			</TABLE>
			<TABLE style="WIDTH: 456px; BORDER-COLLAPSE: collapse; HEIGHT: 20px" cellSpacing="1" cellPadding="1"
				width="456" border="1">
				<TR bgColor="#e0e0e0">
					<TD noWrap align="center">图片</TD>
					<TD noWrap align="center">名称</TD>
					<TD noWrap align="center">地址</TD>
					<TD noWrap align="center">修改</TD>
					<TD noWrap align="center">删除</TD>
				</TR>
				<%  int i=0;
					foreach(XmlNode node in CurrentNodeChilds)
					{
					%>
						<tr id="NM<%=i%>" bgcolor="white" 
						onclick="SelectEmpl('<%=i%>')" 
						<%if(true)
						{%>
							ondblclick="javascript:Editmoban('<%=node.Attributes.GetNamedItem("Text").InnerXml%>','<%=node.Attributes.GetNamedItem("NavigateUrl").InnerXml%>','<%=node.Attributes.GetNamedItem("ImageUrl").InnerXml%>','<%=node.Attributes.GetNamedItem("Target").InnerXml%>')"
						<%}%>
						onmouseover="this.bgColor='LemonChiffon'"
						onmouseout="this.bgColor=''" title="<%=node.Attributes.GetNamedItem("Text").InnerXml%>">
							<td><img src='<%=node.Attributes.GetNamedItem("ImageUrl").InnerXml%>'/></td>
							<td nowrap><%=node.Attributes.GetNamedItem("Text").InnerXml%></td>
							<td><%=node.Attributes.GetNamedItem("NavigateUrl").InnerXml%></td>
							<td><a href="#" onclick="javascript:Editmoban('<%=node.Attributes.GetNamedItem("Text").InnerXml%>','<%=node.Attributes.GetNamedItem("NavigateUrl").InnerXml%>','<%=node.Attributes.GetNamedItem("ImageUrl").InnerXml%>','<%=node.Attributes.GetNamedItem("Target").InnerXml%>')">修改</a></td>
							<td><a href="#" onclick="javascript:Delmoban('<%=node.Attributes.GetNamedItem("Text").InnerXml%>')">删除</a></td>
							</tr>
					<%i++;}%>
			</TABLE>
			<input id=delID type=hidden value="<%=newID%>" name=delID> 
			<input id="MID" type="hidden" name="MID">
		</form>
	</body>
</HTML>
