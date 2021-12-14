<%@ Page Language="c#" AutoEventWireup="True"
    Inherits="OA.RBAC.Organizations.searchperson_list" Codebehind="searchperson_list.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>searchperson_list</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">

    <script language="javascript">
            function ReSelect(obj)
            {
                obj.previousSibling.firstChild.checked=true;
                parent.NextStep();
            }
			function SelUser()
			{
			<%
				if (Request.QueryString["multiple"] + "" != "1")
				{
			%>
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
			<%
				}
			%>
			}
			
			function SelAll(chkall)
			{
				for (i=0;i<document.all.length;i++)
				{
					ename = document.all[i].name;
					if (typeof(ename) != 'undefined')
					{
						if (ename.indexOf('chk_')==0)
						{
							document.all[i].checked = chkall.checked;
							/*
							if (ename != event.srcElement.name)
							{
								if (document.all[i].checked)
									document.all[i].checked = false;
							}
							*/
						}
					}
				}			
			}
			
			function GetSelection()
			{
				var result, ename;
				result = '';
				for (i=0;i<document.all.length;i++)
				{
					ename = document.all[i].name;
					if (typeof(ename) != 'undefined')
					{
						if (ename.indexOf('selectEmp')==0)
						{
							if (document.all[i].checked)
							{
								result = result + document.all[i].value;
								return result;
							}
						}
					}
				}
				if (result.indexOf(',') > 0)
				{
					result = result.substring(0, result.length-1);
				}
				return result;
			}
    </script>

</head>
<body ms_positioning="GridLayout" bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0">
    <table border="1" cellspacing="0" cellpadding="2" align="left" style="font-size: 10pt;
        border-collapse: collapse" width="100%">
        <tr bgcolor="white">
            <td>
            </td>
            <td>
                姓名
            </td>
            <td>
                电话
            </td>
            <td>
                电子邮件
            </td>
        </tr>
        <%if (Dt.Rows.Count != 0)
          {

              for (int i = 0; i < Dt.Rows.Count; i++)
              {
                  Response.Write("<tr bgcolor=white>");
                  Response.Write("<td>");
                  Response.Write("<input  type=\"radio\" name=\"selectEmp\" id=\"selectEmp\" value=\"" + Dt.Rows[i]["Id"] + "|" + Dt.Rows[i]["NameL"] + Dt.Rows[i]["NameF"] + "\">");
                  Response.Write("</td>");
                  Response.Write("<td style='Cursor:hand' onclick='ReSelect(this)'><u>" + Dt.Rows[i]["NameL"] + Dt.Rows[i]["NameF"] + "</u></td>");
                  Response.Write("<td>" + Dt.Rows[i]["Mobile"] + "</td>");
                  Response.Write("<td>" + Dt.Rows[i]["Email"] + "</td>");
                  Response.Write("</tr>");
              }
          }%>
    </table>
    <font face="宋体"></font>
</body>
</html>
