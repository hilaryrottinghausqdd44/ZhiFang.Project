<%@ Page language="c#" Codebehind="Config.aspx.cs" AutoEventWireup="True" Inherits="Labweb.Admin.Config" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Config</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<LINK href="style.css" type="text/css" rel="stylesheet">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" id="clientEventHandlersJS">
		function ButtSelectFile_onclick(returnid,style,path,pageSize) 
		{
			r=window.showModalDialog('../PopupSelectImageFile.aspx?path=' 
				+ path +'&pageSize='
				+ pageSize + '&style='
				+ style,'','dialogWidth:588px;dialogHeight:618px;help:no;scroll:no;status:no');
			if (r == '' || typeof(r) == 'undefined'||typeof(r)=='object')
			{
				return;
			}
			else
			{
				document.all[returnid].value=r;
			}	
		
		}
		function sendData()
		{
			Form1.TextBoxPic.value=document.all['txtXML'].value;
			Form1.ss.src=document.all['txtXML'].value;
		}   
	    
		function window_onload() {
			Form1.ss.src=Form1.TextBoxPic.value;
		}
		
		function checkSubmit()
		{
			//修改其他配置 
			//如 <img src=/labeweb/** 改为 <img src=/Newlabeweb/**
			//如 <treenode NavigateUrl=/labeweb/** 改为 <treenode NavigateUrl=/Newlabeweb/**
			Form1.WebConfigOthers.value=="0";
			if(Form1.TextBoxId.value!=Form1.TextBoxId.title)
			{
				Form1.WebVDChanged.value="1";
				
					
				if(Form1.TextBoxId.value=="/")
				{
					var changeOthers=confirm("从虚拟目录改为站点运行后，将不能再改回为虚拟目录运行；\n"
						+"请再次确认您一定要修改吗？\n\n"
						+"程序有可能要运行几分钟...");
					if(!changeOthers)
						return false;
				}
				else
				{
					var change=confirm("网站标识发生改变,这个网站标识同时是虚拟目录的名称；\n"
						+"请先把网站的虚拟目录改为["+Form1.TextBoxId.value + "]\n"
						+"您知道把虚拟目录从["+Form1.TextBoxId.title+"]改为["+Form1.TextBoxId.value+"]的后果吗？\n\n"
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
		function FileMngr()
		{
			window.open("../tools/XPathAnalyzer/contentpane1.aspx","_blank");
		}
		</script>
		<script language="javascript" event="onpropertychange" for="TextBoxId">
		
			var CheckRegExp = /^[A-Za-z](([_\.\-]?[a-zA-Z0-9]+)*)$/;
			
			var string = Form1.TextBoxId.value;
			if(string.match(CheckRegExp)==null&&string!="/")
				Form1.ButtonSave.disabled=true;
			else
				Form1.ButtonSave.disabled=false;
			//if(Form1.TextBoxId.value==""
			//	||Form1.TextBoxId.value.indexOf(" ")>-1
			//	||Form1.TextBoxId.value.indexOf("")>-1)
		</script>
	    <style type="text/css">
            #Form1
            {
                height: 276px;
            }
        </style>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgcolor="#eefff9" language="javascript" onload="return window_onload()">
		<form id="Form1" method="post" runat="server" onsubmit="return checkSubmit()">
			<asp:label id="Label1" style="Z-INDEX: 101; LEFT: 56px; POSITION: absolute; TOP: 72px" runat="server"
				CssClass="Text">网站名称</asp:label>
			<asp:label id="LabelMSG" 
                style="Z-INDEX: 108; LEFT: 39px; POSITION: absolute; TOP: 315px" runat="server"
				Width="224px" ForeColor="Red" BorderStyle="Dotted" BorderWidth="1px" CssClass="Text"></asp:label>
            <asp:textbox id="TextBoxName" style="Z-INDEX: 102; LEFT: 128px; POSITION: absolute; TOP: 72px; width: 380px;"
				runat="server" CssClass="Text"></asp:textbox><asp:label id="Label2" 
                style="Z-INDEX: 103; LEFT: 209px; POSITION: absolute; TOP: 107px" runat="server"
				CssClass="Text">-修改为-</asp:label>
            <asp:textbox id="TextBoxId" style="Z-INDEX: 104; LEFT: 287px; POSITION: absolute; TOP: 101px; width: 74px;"
				runat="server" CssClass="Text"></asp:textbox><asp:label id="Label3" 
                style="Z-INDEX: 105; LEFT: 38px; POSITION: absolute; TOP: 202px; right: 836px; height: 16px; width: 80px;" runat="server"
				CssClass="Text">数据库连接</asp:label><asp:label id="Label4" style="Z-INDEX: 109; LEFT: 168px; POSITION: absolute; TOP: 32px" runat="server"
				CssClass="Biaoti">网站配置</asp:label>
			<INPUT name="txtXML" id="txtXML" style="Z-INDEX: 113; LEFT: 128px; WIDTH: 0px; POSITION: absolute; TOP: 344px; HEIGHT: 0px"
				type="text" onpropertychange="javascript:sendData()" class="text">&nbsp; <INPUT id="WebVDChanged" style="Z-INDEX: 116; LEFT: 64px; POSITION: absolute; TOP: 456px"
				type="hidden" value="0" runat="server"> <INPUT id="WebConfigOthers" style="Z-INDEX: 117; LEFT: 232px; POSITION: absolute; TOP: 456px"
				type="hidden" value="0" runat="server"> &nbsp;<p>
                <asp:label id="Label5" 
                    style="Z-INDEX: 105; LEFT: 71px; POSITION: absolute; TOP: 136px" runat="server"
				CssClass="Text">授权码</asp:label>
                <asp:textbox id="TextBoxSrc0" style="Z-INDEX: 106; LEFT: 128px; POSITION: absolute; TOP: 133px; width: 386px;"
				runat="server" CssClass="Text"></asp:textbox>
                <asp:textbox id="TextBoxId0" style="Z-INDEX: 104; LEFT: 128px; POSITION: absolute; TOP: 104px; width: 74px;"
				runat="server" CssClass="Text"></asp:textbox><asp:label id="Label6" 
                    style="Z-INDEX: 103; LEFT: 56px; POSITION: absolute; TOP: 104px" runat="server"
				CssClass="Text">网站标识</asp:label>
            </p>
            <asp:button id="ButtonSave" style="Z-INDEX: 107; LEFT: 39px; POSITION: absolute; TOP: 275px"
				runat="server" Text="保存修改" CssClass="Text" onclick="ButtonSave_Click"></asp:button>
            <asp:label id="Label7" 
                style="Z-INDEX: 105; LEFT: 56px; POSITION: absolute; TOP: 166px; right: 834px;" runat="server"
				CssClass="Text">网站地址</asp:label>
            <asp:textbox id="TextBoxSrc" style="Z-INDEX: 103; LEFT: 128px; POSITION: absolute; TOP: 160px; width: 386px;"
				runat="server" CssClass="Text" contentEditable="false"></asp:textbox>
            
            <asp:TextBox ID="TextBoxConnStr" runat="server" Width="141px" 
                style="Z-INDEX: 106; LEFT: 128px; POSITION: absolute; TOP: 196px; width: 386px;"></asp:TextBox>
		</form>
	</body>
</HTML>
