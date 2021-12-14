<%@ Import Namespace="OA.Common"%>
<%@ Page language="c#" Codebehind="Comments.aspx.cs" AutoEventWireup="true" Inherits="OA.Browse.Comments" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title><%=System.Configuration.ConfigurationSettings.AppSettings["MyTitle"]%>-评论信息</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<style type="text/css"></style>
		<script id="clientEventHandlersJS" language="javascript">
			<!--

			function selPage_onchange() {
			window.open("?id=<%=id%>&startpage="+ selPage.options(selPage.selectedIndex).value,"_self");
			}

			//-->
		</script>
	</HEAD>
	<body>
		<H2 align="center"><%
		Response.Write("<a href=\"eachNews.aspx?id=" + id + "\">");
		Response.Write(newsTitle);
		Response.Write("</a>");
		if(newsTitle=="")
		{
			Response.Write("没有新闻内容");
			Response.End();
		}
		%></H2>
		<div align="center"><iframe name=ifrmNavigate src="Navigate.aspx?id=<%=id%>" height="25" width="664" frameborder="0"></iframe>
		</div>
		<table id="Table1" style="WIDTH: 664px; HEIGHT: 112px" height="116" cellSpacing="1" cellPadding="1"
			width="664" align="center" border="0" class="tdBlack1px">
			<tr>
				<td width="732" colSpan="4" height="26" align="right">
					<%if (iCount>0)
				{
					if (iRealBegins>1)
					{
						Response.Write("<a href=\"?id=" + id + "&startpage=" + (iRealBegins-1).ToString() + "\"> &lt;&lt;&lt;</a>");
					}
					%>
					第
					<select id="selPage" language="javascript" onclick="return selPage_onclick()" onchange="return selPage_onchange()">
						<%
						int i=0;
						for(i=1;i<=Convert.ToInt32((iCount-1)/iPageSize)+1;i++)
						{
							Response.Write("<OPTION ");
							if(i==iRealBegins)
								{Response.Write("selected");}
							Response.Write(" value=\"" + i.ToString()  + "\">" + i.ToString());
							Response.Write("</OPTION>");
						}
						
					%>
					</select>页/共有<font color="red"><%=(Convert.ToInt32((iCount-1)/iPageSize)+1).ToString()%></font>页
					<%
					if (iRealBegins<Convert.ToInt32((iCount-1)/iPageSize)+1)
					{ 
						Response.Write("<a href=\"?id=" + id + "&startpage=" + (iRealBegins+1).ToString() + "\"> &gt;&gt;&gt;</a>");
						
					}
				}
				%>
				</td>
			</tr>
			<%
		if (iCount==0)
		{
			Response.Write("<tr><td>没有人发表过评论</td></tr>");
		}
		for(int i=0;i<dt.Rows.Count;i++)
		{
      %>
			<tr bgColor="#aadfaa">
				<td height="22"><%=(iCount-(iRealBegins-1)*iPageSize-i).ToString()%></td>
				<td style="WIDTH: 143px" height="22">评论人:<%=dt.Rows[i]["username"].ToString()%></td>
				<td noWrap height="22">ip地址:<%=dt.Rows[i]["userip"].ToString()%></td>
				<td noWrap height="22">发表时间<%=dt.Rows[i]["dateandtime"].ToString()%></td>
			</tr>
			<tr>
				<td colSpan="4" height="30">
					<BLOCKQUOTE dir="ltr" style="MARGIN-RIGHT: 0px">
						<P><%NewsFunction f=new NewsFunction();
					Response.Write(f.disHtml(dt.Rows[i]["content"].ToString()));%></P>
					</BLOCKQUOTE>
				</td>
			</tr>
			<% }%>
		</table>
		<table width="664" border="0" align="center" class="tdBlack1px" style="WIDTH: 664px; HEIGHT: 214px">
			<tr>
				<td height="200">
					<iframe width="100%" height="100%" frameborder="0" src="addComments.aspx?id=<%=id%>" style="WIDTH: 100.18%; HEIGHT: 112.01%" scrolling=no>
					</iframe>
				</td>
			</tr>
		</table>
	</body>
</HTML>
