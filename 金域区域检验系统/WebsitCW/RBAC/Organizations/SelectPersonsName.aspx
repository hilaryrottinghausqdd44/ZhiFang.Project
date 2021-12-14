<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Organizations.SelectPersonsName" Codebehind="SelectPersonsName.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>SelectPersons</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">

    <script language="javascript">
			
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
						result +=  '|' + document.all[i].value ;
						//return result;
					}
				}
			}
		}
		if (result.indexOf('|') > -1){
		    result = result.substring(1);
		}
		var PersonsEachTime = '<%=Request.QueryString["PersonsEachTime"] %>';
		//alert(result);
		if (PersonsEachTime == '1') {
		    if (result.indexOf('|') >= 0) {
		        alert('只能选择一人');
		        return true;
		    }
		    
		}
		window.parent.returnValue = result;
		window.parent.close();
		
	}
    </script>

</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <font face="宋体"></font>
    <table height="100%" cellspacing="0" width="100%" border="0">
        <tr bgcolor="slategray" height="30">
            <td style="height: 30px" colspan="2">
                <font color="white" size="3">&nbsp;&nbsp;<b>查找<%=UserType %></b></font>
            </td>
        </tr>
        <tr>
            <td style="height: 6px" nowrap align="center">
            </td>
            <td style="height: 6px">
            </td>
        </tr>
        <tr height="1%">
            <td style="height: 0.02%" nowrap align="center">
                &nbsp;选择部门&nbsp;
            </td>
            <td style="height: 0.02%">
                <asp:DropDownList ID="lstDepts" runat="server" Width="224px">
                </asp:DropDownList>
            </td>
            <tr height="1%">
                <td nowrap align="center">
                    模糊查询
                </td>
                <td>
                    <input id="txtKeyWord" type="text" size="15" name="txtKeyWord" runat="server">
                    <input id="Search" type="button" value=" 查找 " name="Button2" runat="server" onserverclick="Search_ServerClick">
                    (输入姓名或用账号名)
                </td>
            </tr>
            <tr height="40">
                <td align="left">
                    <input onclick="SelectAll()" type="button" value=" 全选 ">
                </td><td>
                    <input onclick="GetSelection()" type="button" value=" 确定 ">
                    <input onclick="window.parent.close()" type="button" value=" 取消 ">
                </td>
            </tr>
            <tr height="180">
                <td valign="top" align="left" colspan="2">
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
                        <%if (DtE.Rows.Count != 0)
                          {

                              for (int i = 0; i < DtE.Rows.Count; i++)
                              {
                                  Response.Write("<tr bgcolor=white>");
                                  Response.Write("<td>");
                                  Response.Write("<input  type=\"checkbox\" name=\"selectEmp\" id=\"selectEmp\" value=\"" + DtE.Rows[i]["NameL"] + DtE.Rows[i]["NameF"] + "\">");
                                  Response.Write("</td>");
                                  Response.Write("<td>" + DtE.Rows[i]["NameL"] + DtE.Rows[i]["NameF"] + "</td>");
                                  Response.Write("<td>" + DtE.Rows[i]["Mobile"] + "</td>");
                                  Response.Write("<td>" + DtE.Rows[i]["Email"] + "</td>");
                                  Response.Write("</tr>");
                              }
                          }%>
                    </table>
                </td>
            </tr>
            <tr height="40">
                <td align="center" colspan="2">
                    <input onclick="SelectAll()" type="button" value=" 全选 ">
                    <input onclick="GetSelection()" type="button" value=" 确定 ">
                    <input onclick="window.parent.close()" type="button" value=" 取消 ">
                </td>
            </tr>
    </table>
    </form>

    <script language="javascript" defer>
        //Response.Write(Request.ServerVariables["FunctionString"].ToString())
        var strIN = '<%Response.Write(Request.QueryString["FunctionString"]);%>';
			//alert(strIN); 
			var strIN = strIN.split('=');
			
			if(strIN.length>1)
			{
				var strNames = strIN[1];
				strNames = strNames.split('|');
				for(var i=0;i<strNames.length;i++)
				{
					InitCheckBox(strNames[i]);
				}
			}
			function InitCheckBox(pname)
			{
				for (var i=0;i<document.all.length;i++)
				{
					ename = document.all[i].name;
					if (typeof(ename) != 'undefined')
					{
						if (ename.indexOf('selectEmp')==0)
						{
							if (document.all[i].value==pname)
							{
								document.all[i].checked = true;
							}
						}
					}

				}
				return 0;
			}
			function SelectAll()
			{
				for (var i=0;i<document.all.length;i++)
				{
					ename = document.all[i].name;
					if (typeof(ename) != 'undefined')
					{
						if (ename.indexOf('selectEmp')==0)
						{
							
							if(!document.all[i].checked)
							{
								document.all[i].checked = true;
							}
						}
					}

				}
			}
    </script>

</body>
</html>
