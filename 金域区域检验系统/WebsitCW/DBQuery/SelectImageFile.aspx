<%@ Import Namespace="System.IO" %>
<%@ Page language="c#" AutoEventWireup="True" Inherits="Labweb.SelectImageFile" Codebehind="SelectImageFile.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>PublishImage</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<LINK rel="stylesheet" type="text/css" href="css/ioffice.css">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script id="clientEventHandlersJS" language="javascript">
		function ReturnImg(img) {
			if(img=="")
			{
				alert("请选择一个图片");
				return;
			}
			var path='<%=root%>' + '/DBQuery/<%=Request.QueryString["path"]%>/';
			path=path.replace('//','/');
			
			parent.window.returnValue = path +  img;
			parent.window.close();
		}

		function deleteTemplet(fileName)
		{
			if(confirm('真的要删除这个文件吗？\r\n一旦删除，该文件将不可恢复，确定没有指定过该图片吗？'))
			{
				document.all['Action'].value='删除';
				document.all['FileName'].value=fileName;
				Form1.submit();
			}
			return false;
		}
		
		function doTrans(imgObj) 
		{
			//imgObj=document.all[imgObj1];
			imgObj.filters[0].opacity=98;
		}
		
		
		</script>
	</HEAD>
	<body language="javascript" bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" >
		<iframe frameborder="0" src="UploadImageFile.aspx?path=<%=Request.QueryString["path"]%>" style="WIDTH: 100%; HEIGHT: 56px"></iframe>
		<form id="Form1" method="post" runat="server">
		请选择图片文件<br>
		<TABLE BORDER="1" CELLSPACING="5" CELLPADDING="1" width="90%">
			<TR>
				<%
			int pageSize=8;
			if(Request.QueryString["pageSize"]!=null)
				pageSize=Convert.ToInt32(Request.QueryString["pageSize"]);
			int i=0;
			if(dirInfo!=null)
			{
				foreach (FileInfo dirFile in dirInfo.GetFiles())
				{i++;
				%>
					<TD align="left" valign="top">
						<table BORDER="0" CELLSPACING="0" CELLPADDING="0" width="20" align=left height="60">
							<tr>
								<td align=center valign=top><IMG style="CURSOR:hand;" src="images/icons/0014_b.gif" onclick="deleteTemplet('<%=dirFile.Name%>')"></td>
								<td align=left valign=top><input style="CURSOR:hand;"  type="button" value="删除" onclick="deleteTemplet('<%=dirFile.Name%>')"></td>
							</tr>
							<tr>
								<td colspan=2><IMG id="a<%=i%>" style="cursor:hand; FILTER: progid:DXImageTransform.Microsoft.Alpha( style=0,opacity=35);" 
								src="<%Response.Write(Request.QueryString["path"]+dirFile.Name);%>" 
								title="<%Response.Write(Request.QueryString["path"]+dirFile.Name);%>" 
								onmouseover="this.border=0;this.filters[0].opacity=98;" 
								onmouseout="this.border=0; this.filters[0].opacity=35;" border="0" 
								onclick="ReturnImg('<%=dirFile.Name%>')"></td>
							</tr>
						</table>
					</td>
					<%
				if((double)i/pageSize==(int)i/pageSize)
					Response.Write("</tr><tr>");
				}
			}
				%>
			</TR>
		</TABLE>
		<input name="Action" type="hidden" value="">
		<input name="FileName" type="hidden" value="">
		<input name="PicsName" type="hidden" value="">
     </form>
	
	</body>
</HTML>
