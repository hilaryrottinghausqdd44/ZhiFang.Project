<%@ Page Language="c#" AutoEventWireup="True"
    Inherits="OA.RBAC.Organizations.chooseDeptandpos" Codebehind="chooseDeptandpos.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>ְλѡ��</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio 7.0">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/ioffice.css" type="text/css" rel="stylesheet">

    <script language="javascript">
		<!--
			
			
			function VerifyForm()
			{
				//if (chooseDeptandpos.deptvalue.value == "")
				//{
				//	alert("��ѡ���ţ�");
				//	return false;
			//	}
				
				var posi, ename;
				posi = '';
				for (i=0;i<document.all.length;i++)
				{
					ename = document.all[i].name;
					if (typeof(ename) != 'undefined')
					{
						if (ename.indexOf('chk_')==0)
						{
							if (document.all[i].checked)
								posi = posi + ename.substring(4) + ',';
						}
					}
				}
				if (posi.indexOf(',') > 0)
				{
					posi = posi.substring(0, posi.length-1);
				}
				
				//if (posi == "")
				//{
				//	alert("��ѡ��ְλ��");
				//	return false;
				//}				
				return true;
			}
			
			function SelUser()
			{
				for (i=0;i<document.all.length;i++)
				{
					ename = document.all[i].name;
					if (typeof(ename) != 'undefined')
					{
						if (ename.indexOf('chk_')==0)
						{
							if (ename != event.srcElement.name)
							{
								if (document.all[i].checked)
									document.all[i].checked = false;
							}
						}
					}
				}
			}			
				
			function GetSelection()
			{					
			
				var posi, ename;
				posi = '';
				for (i=0;i<document.all.length;i++)
				{
					ename = document.all[i].name;
					if (typeof(ename) != 'undefined')
					{
						if (ename.indexOf('chk_')==0)
						{
							if (document.all[i].checked)
								posi = posi + ename.substring(4) + ',';
						}
					}
				}
				if (posi.indexOf(',') > 0)
				{
					posi = posi.substring(0, posi.length-1);
				}
											
				var result;
				
				if(posi.length==0)
				{
					result = chooseDeptandpos.Dept.value
				}
				else
				{				
					result = chooseDeptandpos.Dept.value +"|"+  posi;
				}
				return result;
				
			}
		//-->
    </script>

</head>
<body ms_positioning="GridLayout" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="chooseDeptandpos" method="post" runat="server">
    <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td bgcolor="#ccccff" height="30">
                &nbsp;<b>ְλѡ��</b>
            </td>
        </tr>
    </table>
    <table width="90%" border="0" bordercolor="gray" align="center" cellpadding="3" cellspacing="0">
        <tr>
            <td>
                <br>
                ѡ���ţ�&nbsp;&nbsp;
                <select id="Dept" style="width: 144px" name="Dept">
                    <%
                        Response.Write(selectOption);
                    %>
                </select>
                <br>
                <br>
                <br>
            </td>
        </tr>
        <tr>
            <td>
                ѡ��ְλ��<br>
            </td>
        </tr>
    </table>
    <div style="overflow: auto;">
        <table width="90%" border="0" align="center" cellpadding="3" cellspacing="0" bgcolor="whitesmoke">
            <tr>
                <%				
								
                    if (Dt.Rows.Count != 0)
                    {
                        int n = 0;

                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {			
							
                %>
                <td>
                    <input onclick="SelUser()" type="checkbox" name="chk_<%=Dt.Rows[i]["Id"]%>|<%=Dt.Rows[i]["CName"]%>"
                        value="<%=Dt.Rows[i]["Id"]%>|<%=Dt.Rows[i]["CName"]%>"><%=Dt.Rows[i]["CName"]%>
                </td>
                <% n++;
                   if ((double)n / 3 == (int)n / 3)
                   {
                %>
            </tr>
            <tr>
                <%
							
                    }
                        }
                    }
                %>
            </tr>
        </table>
    </div>
    <div style="" align="center">
        <input type="button" value=" ȷ�� " onclick="if (VerifyForm()) {window.returnValue=GetSelection();window.close();}">
        <input type="button" value=" ȡ�� " onclick="window.close();window.returnValue='';">
    </div>
    <div style="" align="center">
        ע��Ŀǰְλ������Ȩ��ʱ��δ�벿�Ű�,���鲻Ҫ����ְλȨ�ޡ�
    </div>
    </form>
</body>
</html>
