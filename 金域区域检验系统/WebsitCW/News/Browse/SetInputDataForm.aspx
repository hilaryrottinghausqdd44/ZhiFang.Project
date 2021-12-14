<%@ Page Language="c#" AutoEventWireup="True"
    Inherits="OA.News.Browse.SetInputDataForm" Codebehind="SetInputDataForm.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="InputFormWebControl" Src="../../WebControlLib/InputFormWebControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>数据录入或修改</title>
    <base target="_self">
    <!-- 这样才不会弹出新窗口 -->
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../../WebControlLib/CSS/WebControlDefault.css" type="text/css" rel="stylesheet">

    <script language="javascript" type="text/javascript">
				//帮定事件
			if(window.attachEvent)
			{
				window.attachEvent("onload", iframeAutoFit);
			}
			else if(window.addEventListener)
			{
				window.addEventListener('load', iframeAutoFit, false);
			}
			

			//自动调整框架的大小为页面的大小
			function iframeAutoFit()
			{
				try
				{
					if(window!=parent)
					{
						var a = parent.document.getElementsByTagName("IFRAME");
						for(var i=0; i<a.length; i++) 
						{
							if(a[i].contentWindow==window)
							{
								//自适应高度
								var h1=0, h2=0, d=document, dd=d.documentElement;
								a[i].parentNode.style.height = a[i].offsetHeight +"px";
								a[i].style.height = "10px";

								if(dd && dd.scrollHeight) h1=dd.scrollHeight;
								if(d.body) h2=d.body.scrollHeight;
								var h=Math.max(h1, h2);
								if(h < 40)
									h = 40;

								if(document.all) {h += 16;}//4
								if(window.opera) {h += 12;}//1
								a[i].style.height = a[i].parentNode.style.height = h +"px";
								
								//自适应宽度
								var W1=0, W2=0, d=document, dd=d.documentElement;
								//a[i].parentNode.style.width = a[i].offsetWidth +"px";
								//a[i].style.width = "10px";

								if(dd && dd.scrollWidth) W1=dd.scrollWidth;
								if(d.body) W2=d.body.scrollWidth;
								var W=Math.max(W1, W2);

								if(document.all) {W += 24;}
								if(window.opera) {W += 21;}
								//a[i].style.width = a[i].parentNode.style.width = W +"px";
							}
						}              
					}
				}
				catch (ex){}
			}

			//重新调整窗口的大小使窗口的大小正好能容下整个页面,并将窗口居中显示(主要是重新设置高度和窗口的Top)
			function resetWindowSize()
			{
				//alert(window.dialogHeight);
				var allFrame = window.parent.document.body.getElementsByTagName("IFRAME");
				if(allFrame.length != 1)
					return;
				var mainFrame = allFrame[0];
				var h1=0, h2=0, d=document, dd=d.documentElement;
				//自适应高度
				mainFrame.parentNode.style.height = mainFrame.offsetHeight +"px";
				if(dd && dd.scrollHeight) h1=dd.scrollHeight;
				if(d.body) h2=d.body.scrollHeight;
				var h=Math.max(h1, h2);
				h += 48;
				window.dialogHeight = h + "px";
				//自适应宽度
				var w1=0,w2=0;
				mainFrame.parentNode.style.width = mainFrame.offsetWidth +"px";
				if(dd && dd.scrollWidth) w1=dd.scrollWidth;
				if(d.body) w2=d.body.scrollWidth;
				var w=Math.max(w1, w2);
				w += 24;
				window.dialogWidth = w + "px";
				//左上角位置
				var top = (window.screen.height - h) / 2;
				window.dialogTop = top + "px";
				//左边的位置
				var left = (window.screen.width - w) / 2;
				window.dialogLeft = left + "px";
			}
	
    </script>

</head>
<body ms_positioning="GridLayout" onload="resetWindowSize()">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" cellspacing="1" cellpadding="1" width="100%" border="0">
        <tr>
            <td>
                <asp:Label ID="lblTitile" runat="server" Visible="False">标题</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <uc1:InputFormWebControl id="InputFormWebControl1" runat="server">
                </uc1:InputFormWebControl>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
