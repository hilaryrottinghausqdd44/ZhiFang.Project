<%@ Page Language="c#" AutoEventWireup="True"
    Inherits="OA.RBAC.Roles.chooseDeptandpos" Codebehind="ChooseDepartment.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>选择部门</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio 7.0">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/ioffice.css" type="text/css" rel="stylesheet">

    <script language="javascript">
		<!--		
			function GetSelection()
			{														
				var result;			
					result = chooseDeptandpos.Dept.value	
					
			
				return result;
				
			}
		//-->
    </script>

    <style type="text/css">
        .style1
        {
            color: #FF0000;
            font-size: x-small;
        }
        .style2
        {
            color: #0000CC;
            font-size: x-small;
        }
    </style>

</head>
<body ms_positioning="GridLayout" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="chooseDeptandpos" method="post" runat="server">
    <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td bgcolor="#ccccff" height="30">
                &nbsp;<strong>&nbsp;&nbsp;部门选择</strong>
            </td>
        </tr>
    </table>
    <table width="90%" border="0" bordercolor="gray" align="center" cellpadding="3" cellspacing="0">
        <tr>
            <td>
                选择部门：<br />
                <select id="Dept" style="width: 332px; height: 232px;" name="Dept" size="6" 
                    multiple="multiple">
                    <%
                        Response.Write(selectOption);
                    %>
                </select>
                <br>
            </td>
        </tr>
    </table>
    <div align="center">
        <input type="button" value=" 确定 " onclick="window.returnValue=GetSelection();window.close();">
        <input type="button" value=" 取消 " onclick="window.close();window.returnValue='';">
        <br />
    </div>
        <span class="style1">注：</span><span class="style2">部门权限将启用继承关系(2009-7),最下级部门及员工继承上级部门权限；<br />
&nbsp;&nbsp;&nbsp; 请选择一个部门，进行权限分配；最顶级部门的权限将赋予全体员工。</span></form>
</body>
</html>
