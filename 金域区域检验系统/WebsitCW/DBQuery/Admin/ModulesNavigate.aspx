<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Admin.ModulesNavigate" Codebehind="ModulesNavigate.aspx.cs" %>
<%@ Import Namespace="System.Xml"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>???????ù???</title>
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
				if (confirm('??????Ҫɾ???˹???????'))
				{
					FormDelmoban.delGroup.value=groupName;
					FormDelmoban.delID.value=id;
					FormDelmoban.submit();
				}
			}
			
			
			function DelGroup(groupName)
			{
				if (confirm('??????Ҫɾ???˷???????'))
				{
					FormDelmoban.delGroup.value=groupName;
					FormDelmoban.delID.value="";
					FormDelmoban.submit();
				}
			}
			function EditGroup(groupName)
			{
				alert("?˹?????ʱ????ʹ??..");
			}
			
		
			function Editmoban(groupName,Text,NavigateUrl,ImageUrl)
				{
				
				if(Text!='')
				FormDelmoban.TextBoxText.value=Text;
				FormDelmoban.MID.value=Text;
				FormDelmoban.TextBoxNavigateUrl.value=NavigateUrl;
				FormDelmoban.txtXML.value=ImageUrl;
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
				alert('û??ѡ?????࣡????û??Ҫѡ???ķ???,??ѡ????????')
				return false
			}
			if(FormDelmoban.TextBoxText.value.length==0)
			{
				alert('???Ʋ???Ϊ?գ?')
				return false
			}
		</script>
		<script language="javascript" for="ButtonModify" event="onclick">
			if(FormDelmoban.drpGroup.selectedIndex<0)
			{
				alert('û??ѡ?????࣡????û??Ҫѡ???ķ???,??ѡ????????')
				return false
			}
			if(FormDelmoban.TextBoxText.value.length==0)
				{
					alert('???Ʋ???Ϊ?գ?')
					return false
				}
		</script>
		<script language="javascript" for="ButtonGroupAdd" event="onclick">
			if(FormDelmoban.TextBoxNewGroup.value.length==0)
			{
				alert('??????Ҫ????????????')
				return false
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="Gr">
		<form id="FormDelmoban" onsubmit="return VerifyFormEmployee()" method="post" runat="server">
			<TABLE id="Table1" style=""
				cellSpacing="1" cellPadding="1" width="406" border="1">
				
					<TR  height="30">
					<TD align="center"><B>????????</B>
					</TD>
					<TD>
						<asp:checkbox id="chkopen" runat="server"  Text="?Ƿ????õ???"></asp:checkbox> &nbsp;&nbsp;<asp:checkbox id="NavigateOpen" runat="server"  Text="????"></asp:checkbox>&nbsp;&nbsp;&nbsp;&nbsp;<asp:button id="btnSave" Runat="server" Text="????" onclick="btnSave_Click"></asp:button>
					</TD>
				</TR>
				
				<TR bgColor="steelblue" height="30">
					<TD align="center"><IMG src="../images/icons/0078_a.gif" align="absBottom">
					</TD>
					<TD><B>????????</B>
						<asp:Label id="LabelMsg" runat="server" ForeColor="Red"></asp:Label>
					</TD>
				</TR>
				<tr>
					<td align="center">????</td>
					<td><asp:DropDownList ID="drpGroup" Runat="server"></asp:DropDownList><FONT face="????">&nbsp;
						</FONT>
						<asp:Button id="ButtonGroupAdd" runat="server" Text="????????" onclick="ButtonGroupAdd_Click"></asp:Button>&nbsp;&nbsp;
						<asp:TextBox id="TextBoxNewGroup" runat="server" Width="128px" BackColor="#E0E0E0"></asp:TextBox></td>
				</tr>
				<TR>
					<TD style="WIDTH: 71px" align="center"><asp:label id="Label2" runat="server">????</asp:label></TD>
					<TD><FONT face="????"><asp:textbox id="TextBoxText" runat="server" Width="184px"></asp:textbox>&nbsp;<INPUT onclick="return ButtSelectModule_onclick()" type="button" value="ѡ??ģ??"></FONT></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 71px" align="center"><asp:label id="Label3" runat="server">??ַ</asp:label></TD>
					<TD><asp:textbox id="TextBoxNavigateUrl" runat="server" Width="184px"></asp:textbox></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 71px" align="center"><asp:label id="Label1" runat="server">????ʱ???ݲ???</asp:label></TD>
					<TD><asp:textbox id="canshu" runat="server" Width="184px"></asp:textbox></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 71px" align="center"><asp:label id="Label5" runat="server">?ù??ܷ??ʵ?ַ</asp:label></TD>
					<TD><asp:textbox id="URL" runat="server" Width="184px"></asp:textbox></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 71px" align="center"><asp:label id="Label4" runat="server">ͼƬ??ַ</asp:label></TD>
					<TD><asp:textbox id="txtXML" runat="server" Width="184px"></asp:textbox><FONT face="????">&nbsp;</FONT><INPUT onclick="return ButtSelectFile_onclick()" type="button" value="ѡ??ͼƬ"><FONT face="????">&nbsp;</FONT></TD>
				</TR>
				<TR>
					<TD align="center" colSpan="2"><asp:button id="ButtonAdd" runat="server" Text="????" onclick="ButtonAdd_Click"></asp:button><FONT face="????">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						</FONT>
						<asp:button id="ButtonModify" runat="server" Text="?޸?" onclick="ButtonModify_Click"></asp:button></TD>
				</TR>
			</TABLE>
			<table 
				cellSpacing="1" cellPadding="1"  border="1">
				<TR bgColor="#e0e0e0">
					<TD noWrap align="center">ͼƬ</TD>
					<TD noWrap align="center">????</TD>
					<TD noWrap align="center">??ַ</TD>
					<TD noWrap align="center">?޸?</TD>
					<TD noWrap align="center">ɾ??</TD>
					<TD noWrap align="center">????</TD>
				</TR>
				
				<%  
					if(CurrentNodeChilds!=null)
					{
						foreach(XmlNode node1 in CurrentNodeChilds)
						{
						
												
							
							chilNode = null;		
							string tablename = Request.QueryString["TableName"].ToString().Trim();
							string[]  sname = tablename.Split('/');	
							int m = sname.Length;
							string tempType;
							if(m>1)
							{
								tempType = sname[m-1].Trim();
							}
							else
							{
								tempType = tablename;
							}
						    chilNodeList = storage.DocumentElement.SelectNodes("treenode/treenode[@NodeData='RemoteTools' and @Text='"+node1.Attributes["Text"].Value+"']" );					
							foreach(XmlNode xn in chilNodeList)
							{
								XmlNode xnn = xn.SelectSingleNode("treenode[@TableEName='"+tempType+"']");
								if(xnn!=null)
								{
									chilNode = xnn;
									break;
								}

							}
						
						%>
							<tr bgcolor=#ccccff><td colspan=3><%=node1.Attributes["Text"].Value%></td>
								<td nowrap><a href="#" onclick="javascript:EditGroup('<%=node1.Attributes.GetNamedItem("Text").InnerXml%>')">??????????</a></td>
								<% if (chilNode!=null)
								{ %>
								<td nowrap><%if(chilNode.ChildNodes.Count == 0){%><a href="#" onclick="javascript:DelGroup('<%=node1.Attributes.GetNamedItem("Text").InnerXml%>')">ɾ??????</a><%}%>&nbsp;</td>
								<%
								}
								%>
								<td><img src="../images/icons/0048_b.gif"></td>
							</tr>
	
							<%
							if(chilNode!=null)
							{
							int i=0;
							foreach(XmlNode node in chilNode.ChildNodes)
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
								<td><a href="#" onclick="javascript:Editmoban('<%=node1.Attributes.GetNamedItem("Text").InnerXml%>','<%=node.Attributes.GetNamedItem("Text").InnerXml%>','<%=node.Attributes.GetNamedItem("NavigateUrl").InnerXml.Replace("\\","\\\\")%>','<%=node.Attributes.GetNamedItem("ImageUrl").InnerXml%>')">?޸?</a></td>
								<td><a href="#" onclick="javascript:Delmoban('<%=node1.Attributes.GetNamedItem("Text").InnerXml%>','<%=node.Attributes.GetNamedItem("Text").InnerXml%>')">ɾ??</a></td>
								<td><img src="../images/icons/0048_b.gif"></td>
								</tr>
							<%i++;}%>
						<%}
						  }
					}%>
					
					
			</table>
			<input id=delID type=hidden value="<%=newID%>" name=delID> <input id="MID" type="hidden" name="MID">
			<input id=delGroup type=hidden value="<%=newID%>" name=delGroup>
		</form>
	</body>
</HTML>
