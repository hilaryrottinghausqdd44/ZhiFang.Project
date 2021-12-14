<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Themes.ChooseButton" Codebehind="ChooseButton.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>全部按钮</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio 7.0">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/ioffice.css" type="text/css" rel="stylesheet">

    <script language="javascript">		
		function checkAll()
		{			
			var Added, Deleted,all,CheckedName;
			Added="";
			Deleted="";
			var ButName='';
			var Butdesr='';
			for(var m=0;m<<%=OprDt.Rows.Count%>;m++)
			{
					if(document.all['checkbox' + m].checked)
					{
						//document.all['checkbox' + m].value=document.all['checkbox' + m].title;
						ButName+=document.all['checkbox' + m].value+',';
						Added +=document.all['checkbox' + m].title+',';
						Butdesr +=document.all['checkbox' + m].name+',';
					}
					else
					{
						//document.all['checkbox' + m].value="off";
						Deleted +=document.all['checkbox' + m].title+','
					}
				
			}
			//if(Added=="")
			//{
			//	alert('please select a role');
			//	return false;
			//}
			Added=Added.substring(0,Added.length-1);
			ButName=ButName.substring(0,ButName.length-1);
			Butdesr=Butdesr.substring(0,Butdesr.length-1);
			if(Deleted!="")
			{
				Deleted=Deleted.substring(0,Deleted.length-1);
			}
			
			all=Added+'|'+ButName+'|'+Butdesr+'|'+Deleted;
			return all;
		//	Form1.added.value=Added;
			//Form1.deleted.value=Deleted;
			
		}
		<!--
			
			
			
			
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
						if (ename.indexOf('checkbox')==0)
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
                &nbsp;<b>按钮选择</b>
            </td>
        </tr>
    </table>
    <table width="90%" border="0" bordercolor="gray" align="center" cellpadding="3" cellspacing="0">
        <tr>
            <td>
                按钮选择：
                <asp:Label ID="Label2" runat="server"></asp:Label>
                <br>
            </td>
        </tr>
    </table>
    <div style="overflow: auto">
        <table id="Table2" style="width: 497px; height: 70px; background-color: #99ccff"
            cellspacing="1" cellpadding="0" width="497" border="0" align="center">
            <tr>
                <%
                    if (OprDt != null)
                    {
                        if (OprDt.Rows.Count != 0)
                        {
                            int k = 0;
                            for (int i = 0; i < OprDt.Rows.Count; i++)
                            {
                                if (k == 5)
                                {%></tr>
            <tr>
                <%k = 0;
                                                }%>
                <td width="25%" nowrap style="background-color: #ffffff">
                    <input id="checkbox<%=i%>" type="checkbox" name="" title="<%=OprDt.Rows[i]["ID"]%>"
                        value="<%=OprDt.Rows[i]["CName"]%>"><label id="Label<%=i%>"><input type="button"
                            value="<%=OprDt.Rows[i]["CName"]%>"><br>
                            &nbsp;&nbsp;&nbsp;<%=OprDt.Rows[i]["SN"]%></label>
                </td>
                <%
                    k++;
                                            }
                                        }
                                }%>
            </tr>
        </table>
    </div>
    <div align="center">
        <input type="button" value=" 确定 " onclick="parent.window.returnValue=checkAll();parent.window.close();">
        <input type="button" value=" 取消 " onclick="parent.window.returnValue='';parent.window.close();">
    </div>

    <script language="javascript">
					var sPostIdarr='<%=EpostId%>';
					var PostIdarr=sPostIdarr.split(',');
					var button='<%=ButtonName%>';
					var arrbutton=button.split(',');		
					for(var m=0;m<<%=OprDt.Rows.Count%>;m++)
					{
						for(var n=0;n<PostIdarr.length;n++)
						{
							if(document.all['checkbox' + m].title== PostIdarr[n])
							{
								document.all['checkbox' + m].checked=true;
								document.all['checkbox' + m].name=arrbutton[n];
								//buttons.innerHTML+="<input type=button value=" +document.all['checkbox' + m].value+ ">";
							}
						}
					}								
    </script>

    </form>
</body>
</html>
