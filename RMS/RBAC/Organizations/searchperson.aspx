<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Organizations.searchperson" Codebehind="searchperson.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>�����û�</title>
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
				window.frames['fraUserList'].location = 'searchperson_List.aspx?dept=' + lstDepts.value+'&search=dept';
				Unchoose=false;
			}
			function Search()
			{
				window.frames['fraUserList'].location = 'searchperson_List.aspx?condition=' + txtKeyWord.value+'&search=condition&dept=' + lstDepts.value;
				Unchoose=false;
            }
            function enterSearch() {
                if (event.keyCode == 13) {
                    Search();
                }
            }
			function NextStep()
			{
				var r;
				if(Unchoose!=true)
				{
					if (window.frames['fraUserList'].window.GetSelection()!="")
					{
						r = window.frames['fraUserList'].window.GetSelection();
						if (r == '')
						{
							alert ('�����б���ѡ��һ���û���');
							return;
						}
						window.parent.returnValue = r;
						window.parent.close();
					}
					else
					{
						alert('��ѡ����Ա��');
					}
				}
				else
				{
					alert('��ѡ����Ա!');
				}
				
			}
    </script>

</head>
<body bgcolor="#f0f0f0">
    <table height="100%" cellspacing="0" width="100%" border="0">
        <tr bgcolor="slategray" height="30">
            <td colspan="2" style="height: 30px">
                <font color="white" size="3">&nbsp;&nbsp;<b>�����û�</b></font>
            </td>
        </tr>
        <tr>
            <td style="height: 6px" nowrap align="center">
            </td>
            <td style="height: 6px">
            </td>
        </tr>
        <tr height="1%">
            <td nowrap align="center">
                &nbsp;ѡ����&nbsp;
            </td>
            <td>
                <select id="lstDepts" style="width: 250px" align="top" name="lstDepts">
                    <option value="-1" selected>======��ѡ��======</option>
                    <%
                        Response.Write(selectOption);
                    %>
                </select>
            </td>
            <tr height="1%">
                <td nowrap align="center">
                    ģ����ѯ
                </td>
                <td>
                    <input id="txtKeyWord" type="text" size="15" name="txtKeyWord" onkeydown="enterSearch()">
                    <input onclick="Search()" type="button" value=" ���� ">
                    (�������������˺���)
                </td>
            </tr>
            <tr height="6">
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr height="180">
                <td valign="top" align="left" colspan="2">
                    <iframe id="fraUserList" width="95%" height="100%" align="left"></iframe>
                </td>
            </tr>
            <tr height="40">
                <td align="center" colspan="2">
                    <input type="button" value=" ȷ�� " onclick="NextStep()">
                    <input type="button" value=" ȡ�� " onclick="window.parent.close()">
                </td>
            </tr>
    </table>
</body>
</html>
