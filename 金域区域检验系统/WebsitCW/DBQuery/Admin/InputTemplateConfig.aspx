<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Admin.InputTemplateConfig" validateRequest="false" enableEventValidation="false" Codebehind="InputTemplateConfig.aspx.cs" %>
<%@ Import Namespace="System.Xml" %>
<%@ Import Namespace="System.IO" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>InputTemplateConfig</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<style>
			#pagebody
			{
				margin:0;
				margin-bottom:10;
				height:100%;
				width:1020;
			}
			#Select_Temp
			{
				padding:0 0 0 0;
				float:left;
				clear:left
			}
			#Edit_temp
			{
				
				float:left;
				width:640;
				height:700;
				
			}
			#Data_temp
			{
				
				float:left;
				height:700;
				width:50;
				margin:0;				
			}
		</style>
		<script language="javascript">
		//var strpara = '<%=Request.ServerVariables["Query_String"]%>';
		var strpara = 'db=<%=Request.QueryString["db"]%>&name=<%=Request.QueryString["name"]%>&TableName=<%=Request.QueryString["TableName"]%>&DefaultValue=<%=Request.QueryString["DefaultValue"]%>';    //读去当前页面接受的参数
		//alert(strpara);
		function ok(obj)
		{
			opener.document.getElementById("dropListTemplate").value=obj;
			window.close();
		}
		function no(obj)
		{
			var confirm=window.confirm("真的要删除该模板吗？一旦删除不可能恢复\n如果有其他系统正在使用该模板，该模板将失效\n\n确定将执行删除\n\nYes to Delete Permanently");
			if(confirm)
				document.location.href='InputTemplateConfig.aspx?Delete=1&DelValue=' + escape(obj)+'&' + strpara;
		}
		function ClearAll()
		{
			frames['eWebEditor1'].document.execCommand("SelectAll");
			frames['eWebEditor1'].document.execCommand("Cut");
		}
		function swapIMG()
		{
			if(document.getElementById("Select_Temp").style.display=="none")
			{
					document.getElementById("Select_Temp").style.display="";
					document.getElementById("show1").style.display="none";
					document.getElementById("Edit_temp").style.width=640;
			}
			else if(document.getElementById("show1").style.display=="none")
			{	
			
					document.getElementById("Select_Temp").style.display="none";
					document.getElementById("show1").style.display="";
					document.getElementById("Edit_temp").style.width=790;
					
			}
		}

		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
		<div id="pagebody">
	<table id="PositionTable">
		<tr>
		    <td valign="top">
			<div id="Select_Temp">
				
				<div style="BACKGROUND-COLOR: #0099cc;width:100">
				<IMG id=ImgSwap width="24" height="16"  src="../../main/images/tiao/short.jpg" style="CURSOR: hand; POSITION: absolute" border="0" onclick="swapIMG()">
				</div>
				<span>
				<BR>
							<%if(fileNamePath!=null){%>
									
								<TABLE id="Table2" cellSpacing="1" cellPadding="1" border="1" style="BORDER-COLLAPSE: collapse;"  borderColor="#4199d8">
									<%
									for(int i=0;i<fileNamePath.Length;i++){
									string myDefault=Path.GetFileNameWithoutExtension(fileNamePath[i].ToString());
									
									%>
									<TR>
										<TD><INPUT type="button" value="选择" onclick="ok('<%=myDefault%>');"></TD>
										<TD noWrap 
										<%if(myDefault==DefaultValue){%>bgcolor="skyblue"<%}%>
										<% else if(DefaultValue==""){%>bgcolor="blue"<%}%>
										><a href='InputTemplateConfig.aspx?DefaultValue=<%=myDefault%>&db=<%=Request.QueryString["db"]%>&name=<%=Request.QueryString["name"]%>&TableName=<%=Request.QueryString["TableName"]%>'><%=myDefault%></a></TD>
										<TD><IMG src="../images/icons/0014_b.gif" style="cursor:hand" onclick="no('<%=myDefault%>')"></TD>
									</TR>
									<%}%>
									
								</TABLE>
							<%}%><br>
							<IMG id="BCancel" onmouseover="this.style.border='#ccccff thin outset';" onclick="parent.window.close();opener.window.location.href=opener.window.location;"
											onmouseout="this.style.border='#ccccff 0px outset';" height="24" src="../image/middle/cancel.jpg" width="79" border="0">
					</span>		
			</div>
			</td><td>
			<div style="display:none;float:left;clear:right" id="show1">
			<IMG  width="24" height="16"  src="../../main/images/tiao/short.jpg" style="CURSOR: hand; POSITION: absolute" border="0" onclick="swapIMG()">
			</div>
			<div id="Edit_temp">
				<table style="WIDTH: 100%; HEIGHT: 100%">
								<tr>
									<td colSpan="2">
									
									<IFRAME id="eWebEditor1" style="WIDTH: 100%; HEIGHT: 100%" src="../htmledit/ewebeditor.asp?id=content1&style=standard_light&<%=Request.ServerVariables["Query_String"]%>"
											frameBorder="0" width="100%" scrolling="no" height="100%"></IFRAME>
											
											<INPUT id=content1 type=hidden 
            value="<%=content%>" name=content1 
            >
									</td>
								</tr>
								<tr height="40">
									<td style="HEIGHT: 40px" vAlign="middle">&nbsp;<asp:button id="Button1" runat="server" Text="保存"></asp:button><asp:textbox id="TextBox1" runat="server"></asp:textbox>
									<input type="button" value="清除" onclick="ClearAll();"</td>
									<td style="HEIGHT: 31px"></td>
								</tr>
								<TR height="40">
									<TD><asp:label id="lblMessage" runat="server" BorderColor="#8080FF" BorderWidth="1px" Width="430px" ForeColor="#9966ff" Font-Bold="true"></asp:label></TD>
									<TD></TD>
								</TR>
							</table>
							<P><INPUT type="hidden"></P>
			</div>
			</td><td>
			<div id="Data_temp">
				
			<iframe src='../SelectData.aspx?&<%=Request.ServerVariables["Query_String"]%>' frameborder=0  height=100% width="210px"/>
			</div>
			<div  id="img_2">
			
			
			</div>
			</td>
		</tr>
	</table>
			</div>
		</form>
	</body>
</HTML>