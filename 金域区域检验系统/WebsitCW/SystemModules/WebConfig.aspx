<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebConfig.aspx.cs" Inherits="OA.SystemModules.WebConfig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>网站配置</title>
<script language="javascript" type="text/javascript">
// <!CDATA[

function window_onunload() {

}
function checkSubmit()
		{
			//修改其他配置 
			//如 <img src=/labeweb/** 改为 <img src=/Newlabeweb/**
			//如 <treenode NavigateUrl=/labeweb/** 改为 <treenode NavigateUrl=/Newlabeweb/**
			Form1.WebConfigOthers.value=="0";
			if(Form1.TextBoxId.value!=Form1.TextBoxId0.value)
			{
				Form1.WebVDChanged.value="1";
				
					
				if(Form1.TextBoxId.value=="/")
				{
//					var changeOthers=confirm("从虚拟目录改为站点运行后，将不能再改回为虚拟目录运行；\n"
//						+"请再次确认您一定要修改吗？\n\n"
//						+"程序有可能要运行几分钟...");
//					if (!changeOthers)
					    alert('不支持修改为网站方式运行');
						return false;
				}
				else
				{
					var change=confirm("网站标识发生改变,这个网站标识同时是虚拟目录的名称；\n"
						+"请先把网站的虚拟目录改为["+Form1.TextBoxId.value + "]\n"
						+ "您知道把虚拟目录从[" + Form1.TextBoxId0.value + "]改为[" + Form1.TextBoxId.value + "]的后果吗？\n\n"
						+"请再次确认您一定要修改吗？\n"
						+"");
					if(!change)
						return false;
					
					var changeOthers=confirm("要修改其他配置吗？\n\n"
						+"程序有可能要运行几分钟...");
					if(changeOthers)
						Form1.WebConfigOthers.value="1";
					else
						Form1.WebConfigOthers.value="0";
				}
			}
			else
			{
				Form1.WebVDChanged.value="0";
				var change=confirm("请再次确认您一定要修改吗？\n"+"");
				if(!change)
					return false;
			}
			
			return true;
		}
function Form1_onsubmit() {

}

// ]]>
</script>
</head>
<body onunload="return window_onunload()">
	<form id="Form1" runat="server" onsubmit="return checkSubmit()">
    
    
			<asp:label id="Label1" 
                style="Z-INDEX: 101; LEFT: 56px; POSITION: absolute; TOP: 76px" runat="server"
				CssClass="Text">网站名称</asp:label>
			<asp:label id="LabelMSG" 
                style="Z-INDEX: 108; LEFT: 39px; POSITION: absolute; TOP: 315px" runat="server"
				Width="224px" ForeColor="Red" BorderStyle="Dotted" BorderWidth="1px" CssClass="Text"></asp:label>
            <asp:textbox id="TextBoxName" style="Z-INDEX: 102; LEFT: 129px; POSITION: absolute; TOP: 73px; width: 380px;"
				runat="server" CssClass="Text"></asp:textbox>
            <asp:label id="Label2" 
                style="Z-INDEX: 103; LEFT: 278px; POSITION: absolute; TOP: 104px" runat="server"
				CssClass="Text">-修改为-</asp:label>
            <asp:textbox id="TextBoxId" style="Z-INDEX: 104; LEFT: 281px; POSITION: absolute; TOP: 135px; width: 74px;"
				runat="server" CssClass="Text"></asp:textbox><asp:label id="Label3" 
                style="Z-INDEX: 105; LEFT: 38px; POSITION: absolute; TOP: 202px; right: 836px; height: 16px; width: 80px;" runat="server"
				CssClass="Text">数据库连接</asp:label><asp:label id="Label4" style="Z-INDEX: 109; LEFT: 168px; POSITION: absolute; TOP: 32px" runat="server"
				CssClass="Biaoti">网站配置</asp:label>
			<INPUT name="txtXML" id="txtXML" style="Z-INDEX: 113; LEFT: 128px; WIDTH: 0px; POSITION: absolute; TOP: 344px; HEIGHT: 0px"
				type="text" onpropertychange="javascript:sendData()" class="text">&nbsp; <INPUT id="WebVDChanged" style="Z-INDEX: 116; LEFT: 64px; POSITION: absolute; TOP: 456px"
				type="hidden" value="0" runat="server">  &nbsp;<p>
                <asp:label id="Label5" 
                    style="Z-INDEX: 105; LEFT: 70px; POSITION: absolute; TOP: 171px" runat="server"
				CssClass="Text">授权码</asp:label>
                <asp:textbox id="TextBoxAuth" style="Z-INDEX: 106; LEFT: 128px; POSITION: absolute; TOP: 168px; width: 386px;"
				runat="server" CssClass="Text"></asp:textbox>
                <asp:textbox id="TextBoxId0" style="Z-INDEX: 104; LEFT: 129px; POSITION: absolute; TOP: 136px; width: 74px;"
				runat="server" CssClass="Text"></asp:textbox>
                <asp:label id="Label6" 
                    style="Z-INDEX: 103; LEFT: 58px; POSITION: absolute; TOP: 137px" runat="server"
				CssClass="Text">网站标识</asp:label>
            </p>
            <asp:button id="ButtonSave" style="Z-INDEX: 107; LEFT: 39px; POSITION: absolute; TOP: 275px"
				runat="server" Text="保存修改" CssClass="Text" onclick="ButtonSave_Click1"></asp:button>
            <asp:label id="Label7" 
                style="Z-INDEX: 105; LEFT: 56px; POSITION: absolute; TOP: 107px;" runat="server"
				CssClass="Text">网站地址</asp:label>
            
            <asp:TextBox ID="TextBoxConnStr" runat="server" Width="141px" 
                style="Z-INDEX: 106; LEFT: 128px; POSITION: absolute; TOP: 196px; width: 386px;"></asp:TextBox>
		
            
            <asp:textbox id="TextBoxSrc0" style="Z-INDEX: 103; LEFT: 129px; POSITION: absolute; TOP: 104px; width: 143px;"
				runat="server" CssClass="Text"></asp:textbox>
            
            <asp:textbox id="TextBoxSrc" style="Z-INDEX: 103; LEFT: 346px; POSITION: absolute; TOP: 103px; width: 160px;"
				runat="server" CssClass="Text" contentEditable="false" BackColor="#D0CED5"></asp:textbox>
            
            <asp:label id="Label8" 
                style="Z-INDEX: 103; LEFT: 209px; POSITION: absolute; TOP: 136px" runat="server"
				CssClass="Text">-修改为-</asp:label>
		
            
    <asp:TextBox ID="WebConfigOthers" runat="server" style="display:none"></asp:TextBox>
    
    </form>
</body>
</html>
