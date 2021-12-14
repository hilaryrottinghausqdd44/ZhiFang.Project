<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Admin.SelectPage" Codebehind="SelectPage.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>选择帮助系统页面</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../../news/Main.css">
		<style> td{font-size:13px}
		</style>
		<script>
		function PubPrev(eid)
			{
				if(eid!='')
					
					window.open('../../news/browse/eachnews.aspx?id='+eid,'','width=680px,height=620px,scrollbars=yes,resizable=yes,left=' + (screen.availWidth-620)/2 + ',top=0' );	
			}
		function SelectEmpl(eid,title)
			{
			/*
				if (SelEmpl != '')
				{
					document.all['NM'+SelEmpl].style.backgroundColor = '';
					document.all['NM'+SelEmpl].style.color = '';
				}
				
				SelEmpl = eid;		
				document.all['NM'+eid].style.backgroundColor = 'gold';
				document.all['NM'+SelEmpl].style.color = 'black';
				//FormDelUser.delID.value=eid;
				*/
				opener.document.all.txt_para.value=title;
				opener.document.all.hpara.value=eid;
				window.close();
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<table border="1" width="98%" cellspacing="0" cellpadding="2" align="center" style="BORDER-COLLAPSE:collapse">
			<tr bgcolor="#e0e0e0">
				<td align="center" nowrap width="16"><img src="../../news/publish/icons/0000_b.gif" align="absBottom"></td>
				<td align="center" nowrap width="16"><font color="#a20010">NO.</font></td>
				
				<td align="center" nowrap width="60%"><font color="#a20010">标题</font></td>
				<td align="center" nowrap width="130"><font color="#a20010">作者</font></td>
				<td align="center" nowrap width="65"><font color="#a20010">创建时间</font></td>
				
			</tr>
			<%int i1;
		for( i1=0;i1<dt.Rows.Count;i1++)
		{
		    DateTime dateTimeValue,dateTimeValue2;
			dateTimeValue2=DateTime.Now;
			dateTimeValue = Convert.ToDateTime(dt.Rows[i1]["buildtime"]);
            System.TimeSpan subtractTime=(dateTimeValue2-dateTimeValue);
            int days=Convert.ToInt32(subtractTime.TotalDays);
      %>
			<tr id="NM<%=dt.Rows[i1]["id"].ToString()%>" bgcolor="#eefff9"
						onclick="javascript:SelectEmpl('<%=dt.Rows[i1]["id"].ToString()%>','<%=dt.Rows[i1]["title"].ToString()%>')" 
						<%if(true)
						{%>
							ondblclick="javascript:SelectEmpl('<%=dt.Rows[i1]["id"].ToString()%>','<%=dt.Rows[i1]["title"].ToString()%>')"
						<%}%>
		 		onmouseover="javascript:this.bgColor='LemonChiffon'" 
		 		onmouseout="this.bgColor=''" title="点击标题可预览详细内容，点击可选中此条，可进行修改、删除">
		 		
						    <td align=center nowrap><img src="../../news/publish/icons/0000_b.gif" align="absBottom"></td>
						    <td align=center nowrap></td>

							
							<td align=left><a href="javascript:PubPrev(<%=dt.Rows[i1]["id"].ToString()%>,<%=dt.Rows[i1]["title"].ToString()%>)"><%=dt.Rows[i1]["title"].ToString()%></a></td>	
							<td align=center><%=dt.Rows[i1]["writer"].ToString()%></td>
							<td align=center nowrap><%=Convert.ToDateTime(dt.Rows[i1]["buildtime"]).ToShortDateString()%></td>
							
							</tr>
					<%}%>
		</table>
	</body>
</HTML>
