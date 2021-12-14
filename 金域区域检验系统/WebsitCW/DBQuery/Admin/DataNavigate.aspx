<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Admin.DataNavigate" Codebehind="DataNavigate.aspx.cs" %>
<%@ Import Namespace="System.Xml"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>导航配置管理</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Includes/CSS/ioffice.css" type="text/css" rel="stylesheet">
		<script language="javascript">
			function VerifyFormEmployee()
			{
				return true;
			}	
			
			function ButtSelectFileBig_onclick() 
			{
				var r;
				var r=window.showModalDialog('../../DBQuery/PopupSelectImageFile.aspx?path=' 
				+ '../images/icons/'  +'&pageSize='
				+ 8 + '&style=list','','dialogWidth:588px;dialogHeight:618px;help:no;scroll:no;status:no');
				if (r == '' || typeof(r) == 'undefined'||typeof(r)=='object')
				{
					return;
				}
				else
				{
					r=r.substr(r.indexOf(".."));
					var bgObject = document.getElementById("txtXML");
					bgObject.value=r;
				}
			}
			
			function ButtSelectFile_onclick() 
			{
				//var r;
				//r=window.open('../../library/XPathAnalyzer/ContentPane.aspx','','width=550px,height=500px,scrollbars=yes,resizable=yes,left=' + (screen.availWidth-620)/2 + ',top=' + (screen.availHeight-470)/2 );	
				//if (r != '' && typeof(r) != 'undefined'&&typeof(r)!='object')
				//{
				//document.all['txtXML'].value=r;
				//}
				
				var r;
				var r=window.showModalDialog('../../DBQuery/PopupSelectImageFile.aspx?path=' 
				+ '../main/images/icons/'  +'&pageSize='
				+ 8 + '&style=list','','dialogWidth:588px;dialogHeight:618px;help:no;scroll:no;status:no');
				if (r == '' || typeof(r) == 'undefined'||typeof(r)=='object')
				{
					return;
				}
				else
				{
					r=r.substr(r.indexOf(".."));
					r="../" + r;
					var bgObject = document.getElementById("txtXML");
					bgObject.value=r;
					//Form1.buttonBackGround.value=r.substr(r.lastIndexOf("/")+1);
				}
			}
			
			function ButtSelectModule_onclick()
			{
				var r;
				var r=window.showModalDialog('../../RBAC/Modules/SelectModuleDialog.aspx','','dialogWidth:588px;dialogHeight:618px;help:no;scroll:no;status:no');
				if (r == '' || typeof(r) == 'undefined'||typeof(r)=='object')
				{
					return;
				}
				else
				{
					var returns=r.split("\v");
					document.all["TextBoxText"].value=returns[0];
					document.all["TextBoxNavigateUrl"].value=returns[1];
				}
			}
			var sel;
			function Delmoban(groupName,id)
			{
				if (confirm('您真的要删除此工具吗？'))
				{
					FormDelmoban.delGroup.value=groupName;
					FormDelmoban.delID.value=id;
					FormDelmoban.submit();
				}
			}
			
			
			function DelGroup(groupName)
			{
				if (confirm('您真的要删除此分类吗？'))
				{
					FormDelmoban.delGroup.value=groupName;
					FormDelmoban.delID.value="";
					FormDelmoban.submit();
				}
			}
			function EditGroup(groupName)
			{
				alert("此功能暂时不可使用..");
			}
			
		
			function Editmoban(groupName,Text,NavigateUrl,ImageUrl,DataLinkName,FiledCName,FiledEName,FiledValue)
				{
				
				if(Text!='')
				FormDelmoban.TextBoxText.value=Text;
				FormDelmoban.MID.value=Text;
				FormDelmoban.TextBoxNavigateUrl.value=NavigateUrl;
				FormDelmoban.txtXML.value=ImageUrl;
				FormDelmoban.DataLinkName.value=DataLinkName;
				FormDelmoban.FiledCName.value=FiledCName;
				FormDelmoban.FiledEName.value=FiledEName;
				FormDelmoban.FiledValue.value=FiledValue;
				
				document.all['ButtonModify'].disabled=false;
			
				}
			
			var SelEmpl = '';
			
			function SelectGroupTool(groupName,eid)
			{
				if (SelEmpl != '')
				{
					document.all['NM'+SelEmpl].style.backgroundColor = '';
					document.all['NM'+SelEmpl].style.color = '';
				}
				
				SelEmpl =groupName +'_'+ eid;				
				document.all['NM'+SelEmpl].style.backgroundColor = 'gold';
				document.all['NM'+SelEmpl].style.color = 'black';
			}
		</script>
		<script language="javascript" for="ButtonAdd" event="onclick">
			if(FormDelmoban.drpGroup.selectedIndex<0)
			{
				alert('没有选择分类！如果没有要选择的分类,请选创建分类')
				return false
			}
		   if(FormDelmoban.DataLinkName.value.length==0)
			{
				alert('连接名称不能为空！')
				return false
			}
			
			 if(FormDelmoban.FiledCName.value.length==0)
			{
				alert('字段中文名称不能为空！')
				return false
			}
			
			 if(FormDelmoban.FiledEName.value.length==0)
			{
				alert('字段英文名称不能为空！')
				return false
			}
			
			 if(FormDelmoban.FiledValue.value.length==0)
			{
				alert('字段不能为空！')
				return false
			}
			if(FormDelmoban.TextBoxText.value.length==0)
			{
				alert('名称不能为空！')
				return false
			}
		</script>
		<script language="javascript" for="ButtonModify" event="onclick">
			if(FormDelmoban.drpGroup.selectedIndex<0)
			{
				alert('没有选择分类！如果没有要选择的分类,请选创建分类')
				return false
			}
			if(FormDelmoban.TextBoxText.value.length==0)
				{
					alert('名称不能为空！')
					return false
				}
		</script>
		<script language="javascript" for="ButtonGroupAdd" event="onclick">
			if(FormDelmoban.TextBoxNewGroup.value.length==0)
			{
				alert('请输入要创建分类名称')
				return false
			}
		</script>
		
		<script language="javascript" for="newField" event="onclick">
			var r=window.showModalDialog('DataSelectFiled.aspx?<%=Request.ServerVariables["Query_String"]%>','','dialogWidth:458px;dialogHeight:528px;help:no;scroll:auto;status:no');
			if (r == '' || typeof(r) == 'undefined')
			{
				return false;
			}
			var returnvalueStr = r.split(":");
			
			window.document.FormDelmoban.FiledEName.value= returnvalueStr[0] ;
			window.document.FormDelmoban.FiledCName.value= returnvalueStr[1] ;
			__doPostBack('newField','');
			
		</script>
		
		<script language="javascript" for="BtnFiledValue" event="onclick">
			var r=window.showModalDialog('DataSelectValue.aspx?<%=Request.ServerVariables["Query_String"]%>&FiledEName='+document.FormDelmoban.FiledEName.value,document.FormDelmoban.FiledEName.value,'dialogWidth:458px;dialogHeight:528px;help:no;scroll:auto;status:no');
			if (r == '' || typeof(r) == 'undefined')
			{
				return false;
			}
			
			
			window.document.FormDelmoban.FiledValue.value= r ;
		
			__doPostBack('BtnFiledValue','');
			
		</script>
	</HEAD>
	<body MS_POSITIONING="Gr">
		<form id="FormDelmoban" onsubmit="return VerifyFormEmployee()" method="post" runat="server">
			<TABLE id="Table1" style=""
				cellSpacing="1" cellPadding="1" width="406" border="1">
				
					<TR  height="30">
					<TD align="center"><B>数据导航</B>
					</TD>
					<TD>
						<asp:checkbox id="chkopen" runat="server"  Text="是否启用数据连接导航"></asp:checkbox> &nbsp;&nbsp;&nbsp;&nbsp;<asp:button id="btnSave" Runat="server" Text="保存" onclick="btnSave_Click"></asp:button>
					</TD>
				</TR>
				
				<TR bgColor="steelblue" height="30">
					<TD align="center"><IMG src="../images/icons/0078_a.gif" align="absBottom">
					</TD>
					<TD><B>数据导航</B>
						<asp:Label id="LabelMsg" runat="server" ForeColor="Red"></asp:Label>
					</TD>
				</TR>
				<tr>
					<td align="center">分类</td>
					<td><asp:DropDownList ID="drpGroup" Runat="server"></asp:DropDownList><FONT face="宋体">&nbsp;
						</FONT>
						<asp:Button id="ButtonGroupAdd" runat="server" Text="新增分类" onclick="ButtonGroupAdd_Click"></asp:Button>&nbsp;&nbsp;
						<asp:TextBox id="TextBoxNewGroup" runat="server" Width="128px" BackColor="#E0E0E0"></asp:TextBox></td>
				</tr>
				<TR>
					<TD style="WIDTH: 71px" align="center"><asp:label id="Label6" runat="server">连接名称</asp:label></TD>
					<TD><FONT face="宋体"><asp:textbox id="DataLinkName" runat="server"  Width="184px"></asp:textbox>&nbsp;</FONT></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 71px" align="center"><asp:label id="Label7" runat="server">连接字段</asp:label></TD>
					<TD><FONT face="宋体">
					<asp:textbox id="FiledCName" ReadOnly=True runat="server" Width="100px"></asp:textbox><asp:textbox id="FiledEName" ReadOnly=True runat="server" Width="84px"></asp:textbox>&nbsp;<INPUT id="newField" name="newField"  type="button" value="选择数据字段"></FONT></TD>
					
				</TR>
				<TR>
					<TD style="WIDTH: 71px" align="center"><asp:label id="Label8" runat="server">字段值</asp:label></TD>
					<TD><FONT face="宋体"><asp:textbox id="FiledValue" runat="server" Width="184px"></asp:textbox>&nbsp;<INPUT id="BtnFiledValue" name="BtnFiledValue"  type="button" value="选择字段数值"></FONT></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 71px" align="center"><asp:label id="Label2" runat="server">模块名称</asp:label></TD>
					<TD><FONT face="宋体"><asp:textbox id="TextBoxText" runat="server" Width="184px"></asp:textbox>&nbsp;<INPUT onclick="return ButtSelectModule_onclick()" type="button" value="选择模块"></FONT></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 71px" align="center"><asp:label id="Label3" runat="server">地址</asp:label></TD>
					<TD><asp:textbox id="TextBoxNavigateUrl" runat="server" Width="184px"></asp:textbox></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 71px" align="center"><asp:label id="Label1" runat="server">访问时传递参数</asp:label></TD>
					<TD><asp:textbox id="canshu" runat="server" Width="184px"></asp:textbox></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 71px" align="center"><asp:label id="Label5" runat="server">该功能访问地址</asp:label></TD>
					<TD><asp:textbox id="URL" runat="server" Width="184px"></asp:textbox></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 71px" align="center"><asp:label id="Label4" runat="server">图片地址</asp:label></TD>
					<TD><asp:textbox id="txtXML" runat="server" Width="184px"></asp:textbox><FONT face="宋体">&nbsp;</FONT><INPUT onclick="return ButtSelectFile_onclick()" type="button" value="选择图片"><FONT face="宋体">&nbsp;</FONT></TD>
				</TR>
				<TR>
					<TD align="center" colSpan="2"><asp:button id="ButtonAdd" runat="server" Text="添加" onclick="ButtonAdd_Click"></asp:button><FONT face="宋体">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						</FONT>
						<asp:button id="ButtonModify" runat="server" Text="修改" onclick="ButtonModify_Click"></asp:button></TD>
				</TR>
			</TABLE>
			<table 
				cellSpacing="1" cellPadding="1"  border="1">
				<TR bgColor="#e0e0e0">
					<TD noWrap align="center">图片</TD>
					<TD noWrap align="center">名称</TD>
					<TD noWrap align="center">地址</TD>
					<TD noWrap align="center">修改</TD>
					<TD noWrap align="center">删除</TD>
					<TD noWrap align="center">排序</TD>
				</TR>
				
				<%  
					
					foreach(XmlNode node1 in CurrentNodeChilds)
					{
					%>
						<tr bgcolor=#ccccff><td colspan=3><%=node1.Attributes.GetNamedItem("Text").InnerXml%></td>
							<td nowrap><a href="#" onclick="javascript:EditGroup('<%=node1.Attributes.GetNamedItem("Text").InnerXml%>')">分类重命名</a></td>
							<td nowrap><%if(node1.ChildNodes.Count==0){%><a href="#" onclick="javascript:DelGroup('<%=node1.Attributes.GetNamedItem("Text").InnerXml%>')">删除分类</a><%}%>&nbsp;</td>
							<td><img src="../images/icons/0048_b.gif"></td>
						</tr>
						<%
						int i=0;
						foreach(XmlNode node in node1.ChildNodes)
						{%>
						<tr id="NM<%=node1.Attributes.GetNamedItem("Text").InnerXml%>_<%=i%>" bgcolor="white" 
						onclick="SelectGroupTool('<%=node1.Attributes.GetNamedItem("Text").InnerXml%>','<%=i%>')" 
						<%if(true)
						{%>
							ondblclick="javascript:Editmoban('<%=node1.Attributes.GetNamedItem("Text").InnerXml%>','<%=node.Attributes.GetNamedItem("Text").InnerXml%>','<%=node.Attributes.GetNamedItem("NavigateUrl").InnerXml.Replace("\\","\\\\")%>','<%=node.Attributes.GetNamedItem("ImageUrl").InnerXml%>')"
						<%}%>
						onmouseover="this.bgColor='LemonChiffon'"
						onmouseout="this.bgColor=''" title="<%=node.Attributes.GetNamedItem("Text").InnerXml%>">
							<td><img src='<%=node.Attributes.GetNamedItem("ImageUrl").InnerXml%>'/></td>
							<td nowrap><%=node.Attributes.GetNamedItem("Text").InnerXml%></td>
							<td><%=node.Attributes.GetNamedItem("NavigateUrl").InnerXml%></td>
							<td><a href="#" onclick="javascript:Editmoban('<%=node1.Attributes.GetNamedItem("Text").InnerXml%>','<%=node.Attributes.GetNamedItem("Text").InnerXml%>','<%=node.Attributes.GetNamedItem("NavigateUrl").InnerXml.Replace("\\","\\\\")%>','<%=node.Attributes.GetNamedItem("ImageUrl").InnerXml%>','<%=node.Attributes.GetNamedItem("DataLinkName").InnerXml%>','<%=node.Attributes.GetNamedItem("FiledCName").InnerXml%>','<%=node.Attributes.GetNamedItem("FiledEName").InnerXml%>','<%=node.Attributes.GetNamedItem("FiledValue").InnerXml%>')">修改</a></td>
							<td><a href="#" onclick="javascript:Delmoban('<%=node1.Attributes.GetNamedItem("Text").InnerXml%>','<%=node.Attributes.GetNamedItem("Text").InnerXml%>')">删除</a></td>
							<td><img src="../images/icons/0048_b.gif"></td>
							</tr>
						<%i++;}%>
					<%}%>
					
					
			</table>
			<input id=delID type=hidden value="<%=newID%>" name=delID> <input id="MID" type="hidden" name="MID">
			<input id=delGroup type=hidden value="<%=newID%>" name=delGroup>
		</form>
	</body>
</HTML>
