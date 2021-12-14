<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Organizations.chooseDept" Codebehind="chooseDept.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>职位选择</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="/ioffice.css" type="text/css" rel="stylesheet">

    <script language="javascript">
		<!--
			function GetSelection()
			{
				var result, ename;
				result = '';
				
				for (i=0;i<chooseDept.lstPosition.options.length;i++)
				{
					result = result + chooseDept.lstPosition.options[i].value +'|'+ chooseDept.lstPosition.options[i].text +',';
				}
				
				/*
				for (i=0;i<document.all.length;i++)
				{
					ename = document.all[i].name;
					if (typeof(ename) != 'undefined')
					{
						if (ename.indexOf('chk_')==0)
						{
							if (document.all[i].checked)
								result = result + ename.substring(4) + ',';
						}
					}
				}
				*/
				if (result.indexOf(',') > 0)
				{
					result = result.substring(0, result.length-1);
				}
				return result;
			}
			
			function delPosition()
			{
				if (chooseDept.lstPosition.length > 0)
				{
					if (chooseDept.lstPosition.selectedIndex != -1)
						chooseDept.lstPosition.remove(chooseDept.lstPosition.selectedIndex);
					else
					{
						alert("请选择一个职位!");
						chooseDept.lstPosition.focus();
					}						
				}
			}
			
			function getPosition()
			{
				var r, i, id, name;
				r = window.showModalDialog ('chooseDeptandpos.aspx','','dialogWidth=25;dialogHeight=20;resizable=no;scroll=no;status=no');
				if (typeof(r) != 'undefined' && r != 'undefined' && r != '')
				{
					var item;

					item = window.document.createElement("OPTION");
					item.text = "";
					item.value = "";
					
					var mrv = r.split(",");
					for (var i=0;i<mrv.length;i++)
					{
						var rv = mrv[i].split("|");
							
						if (rv.length == 4);
						{	
							if (item.text == "")
							{
								if(typeof(rv[3])!='undefined')
								item.text = rv[1]+"("+rv[3]+")";
								else
								item.text=rv[1];
							}	
							else
								item.text = rv[1] + "("+ item.text +")"+rv[3];	
								
							if (item.value == "")
							{
								if(typeof(rv[3])!='undefined')
								item.value = rv[0] + ";" + rv[2];
								else
								item.value=rv[0];
								
							}		
																							
						}												
					}
					var Exist=false;
					for(var i=0;i<chooseDept.lstPosition.options.length;i++)
					{
						var myItem=chooseDept.lstPosition.options[i]
						
						if (myItem.value==item.value)
							Exist=true;
					}			
					if(!Exist)
						chooseDept.lstPosition.add(item);	
					
								
				}				
			}				
		//-->
    </script>

</head>
<body leftmargin="0" topmargin="0" rightmargin="0" ms_positioning="GridLayout">
    <form id="chooseDept" method="post" runat="server">
    <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
        <tr>
            <td bgcolor="#ccccff" height="28">
                &nbsp;<font color="black"><b>职位列表</b></font>
            </td>
        </tr>
    </table>
    <br>
    <div style="overflow: auto; width: 100%; height: 250px">
        <table style="border-collapse: collapse" bordercolor="gray" cellspacing="0" cellpadding="3"
            width="100%" align="center" border="0">
            <tr>
                <td align="center" width="100%">
                    <select style="width: 95%; height: 192px" size="12" name="lstPosition">
                        <%=DeptAndPosi%>
                    </select>
                </td>
                <td valign="top" width="5">
                    <input onclick="getPosition();" type="button" value=" 添加 ">
                    <input onclick="delPosition();" type="button" value=" 删除 ">
                </td>
            </tr>
        </table>
        <p align="center">
            <input onclick="window.returnValue=GetSelection();window.close();" type="button"
                value=" 确定 ">
            <input onclick="window.close();window.returnValue='-1';" type="button" value=" 取消 ">
        </p>
    </div>
    </form>
</body>
</html>
