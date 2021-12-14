<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Organizations.positionlist" Codebehind="positionlist.aspx.cs" %>

<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>PersonList</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio 7.0">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="../../Includes/CSS/ioffice.css" type="text/css" rel="stylesheet">

    <script language="javascript" for="Toolbar1" event="onbuttonclick">
			switch (event.srcNode.getAttribute('Id'))
			{
				case 'new':
					window.open('positionadd.aspx?RBACModuleID=<%=RBACModuleID%>','','width=370px,height=350px,resizable=yes,left=' + (screen.availWidth-620)/2 + ',top=' + (screen.availHeight-470)/2 );	
					break;
				
				
				case 'viewposition':
				if(SelEmpl=='')
					{
						alert('请选择职位！');
						return false
					}
					else
						
					window.open('positionadd.aspx?id='+SelEmpl+'&RBACModuleID=<%=RBACModuleID%>','','width=370px,height=350px,resizable=yes,left=' + (screen.availWidth-620)/2 + ',top=' + (screen.availHeight-470)/2 );
					break;
				case 'delposition':
				if(SelEmpl=='')
					{
						alert('请选择职位！');
						return false
					}
					else		
					{
						DelPosi(SelEmpl)
					}
					break;  				
				
			}
			
    </script>

    <script language="javascript">
		var SelEmpl = '';
			
			function SelectEmpl(eid)
			{
				
				if (SelEmpl != '')
				{
					document.all['NM'+SelEmpl].style.backgroundColor = '';
					document.all['NM'+SelEmpl].style.color = '';
				}
				
				SelEmpl = eid;				
				document.all['NM'+eid].style.backgroundColor = 'gold';
				document.all['NM'+SelEmpl].style.color = 'black';
			}
			function Editposition(eid)
			{
				if(eid!='')
				
					window.open('positionadd.aspx?Id=' + eid,'','width=370px,height=350px,resizable=yes,left=' + (screen.availWidth-740)/2 + ',top=' + (screen.availHeight-540)/2 );
			}
			function DelPosi(id)
			{
				if (confirm('您真的要删除职位吗？\n\n'+
					'删除该职位，分配给该职位的模块功能将不在该职位的控制范围！\n\n'+
					'员工不再拥有该职位！'))			
				{			
					FormDelPosi.delID.value=id;
					FormDelPosi.submit();
				}
			}
			
    </script>

</head>
<body topmargin="0" leftmargin="0" onselectstart="return false">
    <table border="0" width="100%" align="center" cellspacing="0" cellpadding="0" style="color: white;
        border-collapse: collapse" bgcolor="steelblue">
        <tr height="30">
            <td width="1%" nowrap>
                &nbsp;<img src="../../images/icons/0019_a.gif" align="absBottom">&nbsp;
            </td>
            <td>
                <b>职位设置</b>
            </td>
            <td align="right">
            </td>
        </tr>
    </table>
    <iewc:Toolbar ID="Toolbar1" runat="server" BackColor="White" BorderColor="Blue" BorderStyle="Double"
        Width="100%">
        <iewc:ToolbarButton ID="new" ImageUrl="../../images/icons/0013_b.gif" DefaultStyle="display;border:solid 1px white;"
            HoverStyle="border:solid 1px red;" Text="&amp;nbsp;添加职位" Enabled="true"></iewc:ToolbarButton>
        <iewc:ToolbarButton ImageUrl="../../images/icons/0016_b.gif" DefaultStyle="display;border:solid 1px white;"
            HoverStyle="border:solid 1px red;" Text="&amp;nbsp;修改职位信息" ID="viewposition"
            Enabled="true"></iewc:ToolbarButton>
        <iewc:ToolbarButton ImageUrl="../../images/icons/0014_b.gif" DefaultStyle="display;border:solid 1px white;"
            HoverStyle="border:solid 1px red;" Text="&amp;nbsp;删除职位" ID="delposition" Enabled="true">
        </iewc:ToolbarButton>
        <iewc:ToolbarButton ID="PrevPage" DefaultStyle="border:solid 1px white;" HoverStyle="border:solid 1px red;"
            Text="上一页"></iewc:ToolbarButton>
        <iewc:ToolbarButton ID="NextPage" DefaultStyle="border:solid 1px white;" HoverStyle="border:solid 1px red;"
            Text="下一页"></iewc:ToolbarButton>
    </iewc:Toolbar>
    <br>

    <script language="javascript">
		
    </script>

    <table border="0" width="95%" cellspacing="0" cellpadding="2" align="center" style="border-collapse: collapse">
    </table>
    <table border="1" width="90%" cellspacing="0" cellpadding="2" align="center" style="border-collapse: collapse">
        <tr bgcolor="#e0e0e0">
            <td align="center" width="35%" nowrap>
                职位名称
            </td>
            <td align="center" width="15%" nowrap>
                职位级别
            </td>
            <td align="left" width="50%" nowrap>
                说明
            </td>
        </tr>
        <%for (int k = 0; k < Dt.Rows.Count; k++)
          {%>
        <tr id="NM<%=Dt.Rows[k]["Id"].ToString()%>" bgcolor="white" onclick="SelectEmpl('<%=Dt.Rows[k]["Id"]%>')"
            <%if(modify){%> ondblclick="Editposition('<%=Convert.IsDBNull(Dt.Rows[k]["Id"])?"":Dt.Rows[k]["Id"]%>')"
            <%}%> onmouseover="this.bgColor='LemonChiffon'" onmouseout="this.bgColor=''">
            <td>
                <%=Dt.Rows[k]["CName"]%>
            </td>
            <td>
                <%=Dt.Rows[k]["Grade"]%>
            </td>
            <td>
                <%=Dt.Rows[k]["Descr"]%>
            </td>
        </tr>
        <%}%>
    </table>
    <form id="FormDelPosi" name="FormDelPosi" method="post" runat="server">
    <input id="delID" name="delID" type="hidden" value=""></form>
    <iframe name="datafrm" width="0" height="0" src="../library/blank.htm"></iframe>
    <br>
</body>
</html>
