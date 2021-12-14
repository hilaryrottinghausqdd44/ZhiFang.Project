<%@ Page language="c#" Codebehind="digitlab_BBS_List.aspx.cs" AutoEventWireup="True" Inherits="OA.Documents.digitlab_BBS_List" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 

<html>
  <head>
    <title>论坛显示列表模式</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
  	<script type="text/javascript" src="../Includes/JS/WindowLocationSize.js"></script>
	<link rel="stylesheet" type="text/css"  href="CSS/<%=cssFile%>"/>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bottommargin="0" topmargin="0" leftmargin ="0" rightmargin="0">
		<form id="Form1" method="post" runat="server">
			<table width="100%" border="0" cellspacing="0" cellpadding="0" class="style-tableall">
				<tr>
					<td>
						<table  width="100%" class="style-titletable" border="0" cellspacing="0" cellpadding="0">
							<tr>
							    <td width="13"><div class="style-titlepic"></div></td>
								<td nowrap><div class="style-titletext"><%=classname%></div></td>
								<td><a href="<%=System.Configuration.ConfigurationSettings.AppSettings["OaBBSModuleURL"] %>/<%=morestr%>" class="style-titlemore" target="_blank"></a></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td align="left">
						<table class="style-texttable" border="0" cellspacing="0" cellpadding="0"  width="100%">
						<% 
    for (int i = 0; i < dtOANews.Rows.Count; i++)
    {
        string id = dtOANews.Rows[i]["topiciD"].ToString();												
						%>
							<tr>
							    <td width="13" nowrap>
								<div class="style-textpic"></div></td>
								<td nowrap><div ><a href="#" onClick="openWinPositionSize('<%=System.Configuration.ConfigurationSettings.AppSettings["OaBBSModuleURL"] %>/topic.aspx?topicid=<%=id%>','<%=strWinPositionSize%>' );" 
								class="style-text" title="<%=dtOANews.Rows[i]["title"].ToString()%>"><%=StringLength(dtOANews.Rows[i]["title"].ToString(), CharacterNum)%></a></div>
								</td>
							</tr>
						<%}	%>
						</table>
						
					</td>
				</tr>
			</table>
			
			
		</form>
	</body>
</HTML>
