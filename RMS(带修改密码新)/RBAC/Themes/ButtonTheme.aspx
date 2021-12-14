<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Themes.ButtonTheme" Codebehind="ButtonTheme.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>ButtonTheme</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">

    <script language="javascript">
	var ButtonName;
	var ButtonId;
	var ButtonDesr;
	var DelId;
	var ifChooseBut=false;
function ChooseButton()
{	
	var r;
	var TemName='';
	TemName='<%=DDLTemplate.SelectedValue%>	';
	r = window.showModalDialog ('ChooseButtContainer.aspx?TemName='+TemName,'','dialogWidth=45;dialogHeight=30;resizable=no;scroll=no;status=no');
	if(typeof(r)!='undefined'&&r!='')
	{	
	var arrResult=r.split('|');
	ButtId=arrResult[0];	
	ButtonName=arrResult[1];
	ButtonDesr=arrResult[2];	
	DelId=arrResult[3];
	//addButton();
	buttons.innerHTML="";	
	var arrButtonId=ButtId.split(',');
	var arrButtonName=ButtonName.split(',');
	var arrButtonDesr=ButtonDesr.split(',');
	//,CheckedName="",n=0,Addednew="";
	//for(var m=0;m<<%=OprDt.Rows.Count%>;m++)
	//{
	//		if(document.all['checkbox' + m].checked)
	//		{
	//			n++;				
			
	//			CheckedName+=document.all['checkbox' + m].value+',';				 
	//		}				
	//}	
	//if(typeof(CheckedName)!='unfined'&&CheckedName!=''&&CheckedName!=null)
	//{
		var sbutts="";
	//	CheckedName=CheckedName.substring(0,CheckedName.length-1)
	//	var arrCheckedName=CheckedName.split(',');
	
		for(var p=0;p<arrButtonId.length;p++)
		{
			if(typeof(arrButtonName[p])!='undefined'&&arrButtonName[p]!='')
			sbutts +="<td style=\"WIDTH:80px\"><input  style=\"WIDTH:100%\" type=button value=" + arrButtonName[p]+ "><br>" +
				"<input style=\"WIDTH:100%\" id=\"but_"+p+"\" type=\"text\" value=\""+arrButtonDesr[p]+"\"></td>";
		}
		buttons.innerHTML="<TABLE BORDER=0 cellspacing=\"4\"><tr>" + sbutts + "</tr></TABLE>";
		Form1.added.value=ButtId;
		Form1.OperateDesr.value=ButtonDesr;
		Form1.deleted.value=DelId;
		ifChooseBut=true;
		}
		
}
function addButton() 
{				
	buttons.innerHTML="";	
	
	var arrButtonId=ButtonId.split(',');
	var arrButtonName=ButtonName.split(',');
	var arrButtonDesr=ButtonDesr.split(',');
	//,CheckedName="",n=0,Addednew="";
	//for(var m=0;m<<%=OprDt.Rows.Count%>;m++)
	//{
	//		if(document.all['checkbox' + m].checked)
	//		{
	//			n++;				
			
	//			CheckedName+=document.all['checkbox' + m].value+',';				 
	//		}				
	//}	
	//if(typeof(CheckedName)!='unfined'&&CheckedName!=''&&CheckedName!=null)
	//{
		var sbutts="";
	//	CheckedName=CheckedName.substring(0,CheckedName.length-1)
	//	var arrCheckedName=CheckedName.split(',');
	
		for(var p=0;p<ButtId.length;p++)
		{
			sbutts +="<td style=\"WIDTH:80px\"><input  style=\"WIDTH:100%\" type=button value=" + arrButtonName[p]+ "><br>" +
				"<input style=\"WIDTH:100%\" type=\"text\" value=\""+arrButtonDesr[p]+"\"></td>";
		}
		buttons.innerHTML="<TABLE BORDER=0 cellspacing=\"4\"><tr>" + sbutts + "</tr></TABLE>";
	//}
	//Form1.addButtons.value=Addednew;
}
function Savepage()
{ 
	var Number;
	if(ifChooseBut==false)
	{
		Number=<%=ButDt.Rows.Count%>;
		Form1.deleted.value=DelId;
		Form1.added.value='<%=EpostId%>';
	}
	else
	{
		var x=ButtId.split(',');
		Number=x.length;
	}
	var addBut="";
	//alert(x.length)
	//alert()
	for(var h=0;h<Number;h++)
	{
		addBut+=document.all['but_'+h].value+',';
		//alert(document.all['but_'+h].value)
	}
	
		//Form1.added.value=ButtId;
		addBut=addBut.substring(0,addBut.length-1);
		Form1.OperateDesr.value=addBut;
		
		
		//Form1.deleted.value=DelId;
}
    </script>

    <style type="text/css">
        .style1
        {
            color: #FF0000;
            font-weight: bold;
        }
    </style>

</head>
<body language="javascript" bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0"
    ms_positioning="GridLayout">
    <form id="Form1" name="Form1" method="post" runat="server" onsubmit="Savepage();">
    <table id="Table1" style="z-index: 101; left: 8px; width: 483px; position: absolute;
        top: 8px; height: 209px" width="483" bgcolor="#ffffff">
        <tr bgcolor="lightgrey" height="40">
            <td width="419" colspan="4" height="36">
                <img height="32" src="../../images/icons/0003_a.gif" width="32" border="0">操作按钮模板定制
                <asp:Label ID="Label1" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td nowrap align="right" width="489" bgcolor="#f0f0f0" colspan="4" height="27">
                <p align="left">
                    <asp:DropDownList ID="DDLTemplate" runat="server" AutoPostBack="True" DataValueField="CName"
                        DataTextField="CName">
                    </asp:DropDownList>
                    <asp:Button ID="SaveTemplate" runat="server" Text="保存模板" Width="104px" 
                        OnClick="SaveTemplate_Click" Enabled="False">
                    </asp:Button>&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtNewTemplate" runat="server" Width="96px"></asp:TextBox><asp:Button
                        ID="SaveNewTemplate" runat="server" Text="保存为新模板" Width="84px" 
                        OnClick="SaveNewTemplate_Click" Enabled="False">
                    </asp:Button></p>
            </td>
        </tr>
        <tr>
            <td style="height: 87px" nowrap align="left" bgcolor="#f0f0f0" colspan="4" height="87">
                <div id="buttons">
                </div>
            </td>
        </tr>
        <tr>
            <td style="height: 25px" align="center" bgcolor="#f0f0f0" colspan="4" height="25">
                <input type="button" value="选择按钮" onclick="ChooseButton();" disabled="disabled">
            </td>
        </tr>
        <tr>
            <td style="height: 17px" align="left" bgcolor="#f0f0f0" colspan="4" height="17" 
                class="style1">
                该功能已经停止使用 2011-10-12，原因是各模块各自定义按钮</td>
        </tr>
    </table>

    <script language="javascript">
				var sPostIdarr='<%=EpostId%>';
					var PostIdarr=sPostIdarr.split(',');
					var StrBut='<%=ButDesr1%>';
					var arrBut=StrBut.split(',');
					var ButName='<%=ButName%>';
					var arrButN=ButName.split(',');
					var Cbutts="";
					for(var m=0;m<<%=ButDt.Rows.Count%>;m++)
					{
						
							//if(document.all['checkbox' + m].title== PostIdarr[n])
							//{
								//document.all['checkbox' + m].checked=true;
								Cbutts +="<td style=\"WIDTH:80px\"><input  style=\"WIDTH:100%\" type=button value=" + arrButN[m]+ "><br>" +
									"<input style=\"WIDTH:100%\" id=\"but_"+m+"\" type=\"text\" value=\""+arrBut[m]+"\"></td>";
							//}
							buttons.innerHTML="<TABLE BORDER=0 cellspacing=\"4\"><tr>" + Cbutts + "</tr></TABLE>";
						
					}			
    </script>

    <input style="display: none" type="text" name="addButtons"><input style="display: none"
        type="text" name="added">
    <input style="display: none" type="text" name="deleted">
    <input style="display: none" type="text" name="OperateDesr">
    </form>
</body>
</html>
