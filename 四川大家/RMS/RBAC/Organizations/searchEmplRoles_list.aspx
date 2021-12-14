<%@ Page Language="c#" AutoEventWireup="True"
    Inherits="OA.RBAC.Organizations.searchEmplRoles_list" Codebehind="searchEmplRoles_list.aspx.cs" %>

<html>
<head>
    <title>searchperson_list</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">

    <script language="javascript">
            function ReSelect(obj,id)
            {
                if(obj.checked)
                    obj.checked=false;
                else
                    obj.checked=true;
                //parent.parent.NextStep(id,obj.checked);
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
			    if(!confirm("全选或全不选将对当前界面所有人员进行角色操作，耗时较长，您确定吗？"))
			    {
			        //event.cancel=true;
			        return;
			    }
			    var items=document.getElementsByTagName("input");
			    
				for (i=0;i<items.length;i++)
				{
					ename = items[i];
					if (ename.type == 'checkbox' && ename!=chkall)
					{
					    if(items[i].checked!=chkall.checked)
					        items[i].checked = chkall.checked;
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
				parent.frames["fraEmplRoles"].location.href="../Roles/EmplRoles.aspx?id=" + eid;
				parent.frames["fraEmplRoles"].document.getElementById('oToolTip').style.display = '';
			}

    </script>

</head>
<body ms_positioning="GridLayout" bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0">
    <table border="1" cellspacing="0" cellpadding="2" align="left" style="font-size: 10pt;
        border-collapse: collapse;white-space: nowrap;" width="100%">
        <tr bgcolor="white">
            <td  align=center>
                员工号
            </td>
            <td>
                姓名                                                                                                                  
            </td>
            <td>
                帐号
            </td>
            <td>
                电话
            </td>
            <td>
                电子邮件
            </td>
        </tr>
        <%
            string idEmpl = "0";
            
          if (Dt.Rows.Count != 0)
          {
              idEmpl = Dt.Rows[0]["Id"].ToString();
              for (int i = 0; i < Dt.Rows.Count; i++)
              {
                  %>
                  <tr id="NM<%= Dt.Rows[i]["Id"]%>" bgcolor="white" 
					onclick="SelectEmpl('<%= Dt.Rows[i]["Id"]%>')" 
					onmouseover="this.bgColor='LemonChiffon'" 
					onmouseout="this.bgColor=''">
                  <%
                  //Response.Write("<tr bgcolor=white>");
                  Response.Write("<td align=center>");
                  Response.Write(Dt.Rows[i]["SN"]);
                  Response.Write("</td>");
                  Response.Write("<td style='Cursor:hand' onclick='SelectEmpl(" + Dt.Rows[i]["Id"] + ")'><u>" + Dt.Rows[i]["NameL"] + Dt.Rows[i]["NameF"] + "</u></td>");
                  Response.Write("<td align=center>");
                  Response.Write(Dt.Rows[i]["Account"]);
                  Response.Write("</td>");
                  Response.Write("<td>" + Dt.Rows[i]["Mobile"] + "</td>");
                  Response.Write("<td>" + Dt.Rows[i]["Email"] + "</td>");
                  Response.Write("</tr>");
              }
          }%>
    </table>
    <script language="javascript">
        SelectEmpl('<%=idEmpl %>');
    </script>
</body>
</html>
