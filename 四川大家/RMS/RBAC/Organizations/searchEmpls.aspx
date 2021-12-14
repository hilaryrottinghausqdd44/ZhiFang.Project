<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Organizations.searchEmpls" Codebehind="searchEmpls.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>查找用户</title>
    <meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <meta http-equiv="Content-Language" content="zh-cn">
    <link href="../../Include/CSS/ioffice.css" type="text/css" rel="stylesheet">

    <script language="javascript">
			var Unchoose=true;
			function List()
			{
			    window.frames['fraUserList'].location = 'searchEmpls_list.aspx?dept=' + lstDepts.value + '&search=dept';
				Unchoose=false;
			}
			function Search()
			{
			    window.frames['fraUserList'].location = 'searchEmpls_list.aspx?multiple=1&' + queryString + '&condition=' + txtKeyWord.value + '&search=condition&dept=' + lstDepts.value;
				Unchoose=false;
            }
            function enterSearch() {
                if (event.keyCode == 13) {
                    Search();
                }
            }
            var roleTypeID = '<%=Request.QueryString["txtRoleID"] %>';
            var queryString = '<%=Request.ServerVariables["query_string"].ToString()%>';
            
			function NextStep(objID,boolCheck) {
			    parent.document.getElementById('oToolTip').style.display = '';
			    AddRemove(objID, boolCheck);
			    var ret = OA.RBAC.Organizations.searchEmpls.changeRole(roleTypeID, objID, boolCheck);
			    parent.document.getElementById('oToolTip').style.display = 'none';
			}
			function window_onload() {
			    window.frames['fraUserList'].location = 'searchEmpls_list.aspx?multiple=1&' + queryString;
			    parent.document.getElementById('oToolTip').style.display = 'none';
			}
			function AddRemove(objID, boolCheck) {
			
			}

    </script>

</head>
<body bgcolor="#f0f0f0" onload="return window_onload()">
    <table height="100%" cellspacing="0" width="100%" border="0">
        
        <tr height="1%">
            <td nowrap align="center">
                &nbsp;选择部门&nbsp;
            </td>
            <td>
                <select id="lstDepts" style="width: 250px" align="top" name="lstDepts">
                    <option value="-1" selected>======请选择======</option>
                    <%
                        Response.Write(selectOption);
                    %>
                </select>
            </td>
            <tr height="1%">
                <td nowrap align="center">
                    模糊查询
                </td>
                <td>
                    <input id="txtKeyWord" type="text" size="15" name="txtKeyWord" onkeydown="enterSearch()">
                    <input onclick="Search()" type="button" value=" 查找 ">
                    (输入姓名或用账号名)
                </td>
            </tr>
            
            <tr height="280">
                <td valign="top" align="left" colspan="2">
                    <iframe id="fraUserList" width="95%" height="100%" align="left"></iframe>
                </td>
            </tr>
            <tr height="1%" style="visibility:hidden">
                <td align="center" colspan="2">
                    <input type="button" value=" 确定 " onclick="NextStep()">
                    <input type="button" value=" 取消 " onclick="window.parent.close()">
                </td>
            </tr>
    </table>
</body>
</html>
