<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Roles.OperateButtons" Codebehind="OperateButtons.aspx.cs" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>按钮控制</title>
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
var showButton;
function ChooseTemplate()
{
	r = window.showModalDialog ('ChooseButtonTheme.aspx','','dialogWidth=25;dialogHeight=20;resizable=no;scroll=no;status=no');
	if(typeof(r)!='undefined')
	{
		document.all['showButton'].value=r;	
		location='OperateButtons.aspx?ModuleID=<%=Request.QueryString["ModuleID"]%>&TemName='+r;
	}
	
}
function window_onload() {
		
		try{
		    <%if (TemName!="")
		    {
			    for(int i=0;i<AccessBit.Length;i++)
			    {
				    %>
				    document.all['checkbox' + <%=i%>].checked=<%=(AccessBit[i].ToString().ToLower())%>;
				    <%
				    if(AccessBit[i])
				    {	%>
						    var tdColor=document.all['tdd'];
						    tdColor.style.backgroundColor=document.all['checkbox'+ <%=i%>].title;
					    <%
				    }
				    if(OperateAbilities.Length>i && OperateAbilities[i])
				    {
				        %>
				          document.all['checkbox' + <%=i%>].checked=true;  
				        <%
				    }
			    }
		    }
		    %>
		    //
		    var unDisabledChecks=0;
		     parent.document.all['buttSaveButtons'].disabled=false;
		    <%if (TemName!=""&&ButAccessBit.Length!=0&&dt.Rows.Count!=0)
		    {
		    %>
		        unDisabledChecks++;
		    <%
			    for(int i=0;i<((dt.Rows.Count>ButAccessBit.Length)?ButAccessBit.Length:dt.Rows.Count);i++)
			    {
				    %>
				    if(<%=(ButAccessBit[i].ToString().ToLower())%>)
				    {
					    document.all['checkbox' + <%=i%>].disabled=false;
					    unDisabledChecks++;
				    }
				    else
					    document.all['checkbox' + <%=i%>].disabled=true;
    					
				    if(!<%=(bRoleSetThisModule.ToString().ToLower())%>)
				    {
				        document.all['checkbox' + <%=i%>].disabled=true;
				        unDisabledChecks--;
				    }
				    <%
				    if(OperateAbilities.Length>i && OperateAbilities[i])
				    {
				    %>
				        //document.all['checkbox' + <%=i%>].disabled=false;
				        document.all['checkbox' + <%=i%>].checked=true;
				        document.all['checkbox' + <%=i%>].disabled=true;
				        document.all['checkbox' + <%=i%>].style.border="solid 2px " + document.all['checkbox' + <%=i%>].title;
				        
				    <%
				    }
			    }
		    }
    		
		    %>
		    if(unDisabledChecks<=1)
		            parent.document.all['buttSaveButtons'].disabled=true;
		}
		catch(e)
		{
		}
		//
		
}


//-->
    </script>

</head>
<body ms_positioning="GridLayout" bgcolor="#f0f0f0" language="javascript" onload="return window_onload()" style="margin: 0px; padding: 0px">
    <form id="Form1" name="Form1" method="post" runat="server">
    <table id="Table1" style="z-index: 101; left: 8px;">
        <%
            int i = 0;
            if (dt.Rows.Count != 0)
            {			
        %>
        <tr height="30">
            <%
							
                for (i = 0; i < dt.Rows.Count; i++)
                {							
            %>
            <td>
                <input type="checkbox" title="<%=dt.Rows[i]["OperateColor"]%>" disabled onclick="checkColor(<%=i%>,'<%=dt.Rows[i]["OperateColor"]%>')"
                    name="checkbox<%=i%>" value="<%=dt.Rows[i]["ID"]%>"><input type="button" value="<%=dt.Rows[i]["OperateName"]%>"
                        title="<%=dt.Rows[i]["SN"]%>">
            </td>
            <%	
            if ((int)(i + 1) / 6 == (float)(i + 1) / 6)
                Response.Write("</TR><TR>");
                    }	
					
            %>
            <td rowspan="2">
            
            <input id="hAccess" type="hidden" name="hAccess" value="<%=dt.Rows.Count%>">
    <input id="showButton" name="showButton" type="text" disabled style="width: 0px;
        height: 0px" value="<%=Request.QueryString["TemName"]%>">
    <%
        if (Request.QueryString["ModuleID"] != null)
        {
            if (absolute)
            {%>
    <input id="ChooseT" onclick="ChooseTemplate();" style="width: 0px; height: 0px" type="button"
        value="选择模版">
    <%}
            else
            {%>
    <%}

            if (TemName == "" && !absolute)
            {%>
    <font color="red">
        <label>
            您没有权限<%=TemName%></label></font>
    <%	}
            }%>
            
    <script language="javascript">
		<%if(TemName!=""){%>
			Form1.showButton.value='<%=TemName%>';
		<%}%>
			function checkColor(CheckBoxIndex,AccessColor){
				var tdColor=document.all['tdd'];
				
				for(var i=0;i<<%=dt.Rows.Count%>;i++)
				{
				
					if(document.all['checkbox'+ i].checked)
					{		
										
						tdColor.style.backgroundColor=document.all['checkbox'+ i].title;
						
					}
				}				
				
			}
    </script>
            
            </td>
        </tr>
        <tr height="30">
            <td id="tdd" colspan="<%=i%>" style="padding-right: 10px; padding-left: 13px; filter: progid:DXImageTransform.Microsoft.Alpha(
							style=1,opacity=10,finishOpacity=80,startX=0,finishX=100,startY=0,finishY=100);
                color: #DAA520; background-color: skyblue" align="right">
            </td>
        </tr>
        <%
         }
        %>
    </table>
        <%=Saved%>
        <% 
            //int ii=0;
            //foreach (bool eachBool in OperateAbilities)
            //{
            //    ii++;
            //    Response.Write(ii.ToString() +":"+ eachBool.ToString() + ". ");
            //}
            //Response.Write("</br>");
            //ii = 0;
            //foreach (bool eachBool in ButAccessBit)
            //{
            //    ii++;
            //    Response.Write(ii.ToString() + ":" + eachBool.ToString() + ". ");
            //}
            
            //Response.Write("</br>");
            //ii = 0;
            //foreach (bool eachBool in AccessBit)
            //{
            //    ii++;
            //    Response.Write(ii.ToString() + ":" + eachBool.ToString() + ". ");
            //}
            
            
        %>
    </form>

    

</body>
</html>
