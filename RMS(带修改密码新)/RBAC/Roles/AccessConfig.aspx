<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Roles.AccessConfig" Codebehind="AccessConfig.aspx.cs" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>权限控制</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <style>
        table
        {
            font-size: 12px;
            font-weight: normal;
            color: #000000;
            text-decoration: none;
        }
    </style>

    <script id="clientEventHandlersJS" language="javascript">
<!--

function window_onload() {
    var obj = parent.document.getElementById("buttSave");
    if(obj != null)
    {
	    if(!<%=SaveBut.ToString().ToLower()%>)
	    {
	        obj.disabled = true;
	        obj.title = "您现在没有权限！";
		    //parent.document.all['buttSave'].disabled=true;
		    //parent.document.all['buttSave'].title='您现在没有权限！';
	    }
	    else
	    {
	        obj.disabled = false;
	        obj.title = "请继续分配模块！";
		    //parent.document.all['buttSave'].disabled=false;
		    //parent.document.all['buttSave'].title='请继续分配模块！';
	    }
	}
	
	<%for(int i=0;i<AccessBit.Length;i++)
	{
		Response.Write("////" + AccessBit[i]);
		if(AccessBit[i])
		{	%>
				document.all['checkbox' + <%=i%>].checked=<%=(AccessBit[i].ToString().ToLower())%>;
				document.all['hAccess'].value=<%=i%>;
				var tdColor=document.all['tdd'];
				tdColor.style.backgroundColor=document.all['checkbox'+ <%=i%>].title;
			<%
		}
		else
		{
			%>
				document.all['checkbox' + <%=i%>].checked=<%=(AccessBit[i].ToString().ToLower())%>;
				document.all['checkbox'+ <%=i%>].style.borderTopStyle='solid';
				document.all['checkbox'+ <%=i%>].style.borderTopWidth=2;
				document.all['checkbox'+ <%=i%>].style.borderTopColor='red';
			<%
		}
	}
	%>
	//
	<%
		for(int i=0;i<(dt.Rows.Count<AccessBitControl.Length?dt.Rows.Count:AccessBitControl.Length);i++)
		{
			
			if(AccessBitControl[i])
			{	%>					
					document.all['checkbox' + '<%=i%>'].disabled=false;
				<%
			}
			else
			{
				%>
					document.all['checkbox' + '<%=i%>'].disabled=true;				
					document.all['checkbox' + '<%=i%>'].checked=false;
				<%
			}
		}
	
	%>
	//
}


//-->
    </script>

</head>
<body ms_positioning="GridLayout" bgcolor="#f0f0f0" language="javascript" onload="return window_onload()">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" style="z-index: 101; left: 8px; width: 576px; position: absolute;
        top: 8px" height="42">
        <tr height="40">
            <% 
                int i = 0;

                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
            %>
            <td height="12">
                <table>
                    <tr height="15">
                        <td nowrap>
                            <input id="checkbox<%=i%>" name="checkbox<%=i%>" title="<%=dr["OperateColor"]%>"
                                type="checkbox" disabled onclick="checkColor(<%=i%>,'<%=dr["OperateColor"]%>')"
                                ondblclick="enableCheckBox(<%=i%>)"><%=dr["CName"]%>
                        </td>
                    </tr>
                    <tr height="15">
                        <td nowrap>
                            <%=i%>、<%=dr["SN"]%>
                        </td>
                    </tr>
                </table>
            </td>
            <% 
                i++;
                    }
                }
                else
                {
                    Response.Write("过期，请重新登录");
                }
            %>
        </tr>
        <tr height="30">
            <td id="tdd" colspan="<%=i%>" style="padding-right: 10px; padding-left: 13px; filter: progid:DXImageTransform.Microsoft.Alpha(
						style=1,opacity=10,finishOpacity=80,startX=0,finishX=100,startY=0,finishY=100);
                color: #DAA520; background-color: skyblue" align="right">
            </td>
        </tr>
    </table>

    <script language="javascript">
			
			function enableCheckBox(CheckBoxIndex){
				document.all['checkbox'+ CheckBoxIndex].checked=false;
				document.all['checkbox'+ CheckBoxIndex].style.borderTopStyle='none';
				document.all['checkbox'+ CheckBoxIndex].style.borderTopWidth=0;
				document.all['checkbox'+ CheckBoxIndex].style.borderTopColor='white';
			}
			function checkColor(CheckBoxIndex,AccessColor){
				var tdColor=document.all['tdd'];
				
				for(var i=0;i<<%=dt.Rows.Count%>;i++)
				{
					if(document.all['checkbox'+ i].checked)
					{
						document.all['hAccess'].value=i;
						tdColor.style.backgroundColor=document.all['checkbox'+ i].title;
						
					}
				}
				
				
				if(document.all['checkbox'+ CheckBoxIndex].checked)
				{
					//集连前面的权限
					for(var i=0;i<=CheckBoxIndex;i++)
					{
						if(document.all['checkbox'+ i].disabled!=true)
						{
							document.all['checkbox'+ i].checked=true;
							document.all['checkbox'+ i].style.borderTopStyle='none';
							document.all['checkbox'+ i].style.borderTopWidth=0;
							document.all['checkbox'+ i].style.borderTopColor='white';
						}
					}
					
				}
				else
				{
					for(var i=<%=(i-1)%>;i>0;i--)
					{	if(document.all['checkbox'+ i].checked)
							break;
						else
						{
							if(document.all['checkbox'+ i].disabled!=true)
							{
								document.all['checkbox'+ i].checked=false;
								document.all['checkbox'+ i].style.borderTopStyle='none';
								document.all['checkbox'+ i].style.borderTopWidth=0;
								document.all['checkbox'+ i].style.borderTopColor='white';
							}
						}
					}
					if(CheckBoxIndex==0)
					{
						document.all['checkbox'+ CheckBoxIndex].checked=true;
						tdColor.style.backgroundColor=document.all['checkbox'+ CheckBoxIndex].title;
						document.all['hAccess'].value=CheckBoxIndex;
						
						//取消全部权限
						for(var i=<%=(i-1)%>;i>0;i--)
						{	
							if(document.all['checkbox'+ i].disabled!=true)
							{
								document.all['checkbox'+ i].checked=false;
								document.all['checkbox'+ i].style.borderTopStyle='none';
								document.all['checkbox'+ i].style.borderTopWidth=0;
								document.all['checkbox'+ i].style.borderTopColor='white';
							}
							
						}
						alert('这个权限不能取消!\n\n如果要取消，请从角色中去除');
						
					}	
					else
					{
						//if(document.all['checkbox'+ i].disabled!=true)
						//{
							document.activeElement.style.borderTopStyle='dotted';
							document.activeElement.style.borderTopWidth=2;
							document.activeElement.style.borderTopColor='red';
						//}
						
					}
					var boolNextChecked=false;
					for(var i=CheckBoxIndex;i<<%=i%>;i++)
					{
						if(document.all['checkbox'+ i].checked)
						{
							boolNextChecked=true;
						}
					}
					
					if(boolNextChecked&&CheckBoxIndex!=0)
					{
						if(document.all['checkbox'+ CheckBoxIndex].disabled!=true)
						{
							document.all['checkbox'+ CheckBoxIndex].style.borderTopStyle='solid';
							document.all['checkbox'+ CheckBoxIndex].style.borderTopWidth=2;
							document.all['checkbox'+ CheckBoxIndex].style.borderTopColor='red';
						}
					}
					else
					{
						if(document.all['checkbox'+ CheckBoxIndex].disabled!=true)
						{
							document.all['checkbox'+ CheckBoxIndex].style.borderTopStyle='none';
							document.all['checkbox'+ CheckBoxIndex].style.borderTopWidth=0;
							document.all['checkbox'+ CheckBoxIndex].style.borderTopColor='white';
						}
					}
						
				}
			}
			
			
    </script>

    <input id="hAccess" type="hidden" name="hAccess" value="">
    </form>
</body>
</html>
