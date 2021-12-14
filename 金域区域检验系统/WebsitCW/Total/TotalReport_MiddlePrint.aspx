<%@ Page language="c#" Codebehind="TotalReport_MiddlePrint.aspx.cs" AutoEventWireup="false" Inherits="JSCCL.Orders.EQA_Orders_MiddlePrint" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>统计报表打印</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../Css/style.css" rel="stylesheet">
		<script type="text/javascript">	
		
        function $(s)
        {
            return document.getElementById?document.getElementById(s):document.all[s];
        }	
         function Print(cmdid,cmdexecopt)
			{
				//setUnShowPageTags();
				//document.all("printmenu").style.display="none";
				
				document.body.focus();
				
				try
				{
					var WebBrowser = '<OBJECT ID="WebBrowser1" WIDTH=0 HEIGHT=0 CLASSID="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2"></OBJECT>'; 

					document.body.insertAdjacentHTML('BeforeEnd', WebBrowser); 
					
					WebBrowser1.ExecWB(cmdid,cmdexecopt,null,null); 
					WebBrowser1.outerHTML = ""; 
					
					if(cmdid==6 && cmdexecopt==6)
					{
						alert('打印完成');
					}
				}
				catch(e){}
				finally
				{
					//document.all("printmenu").style.display="";
					//setShowPageTags();
				}
			}
			//加载页面
			function LoadPage()
			{
				var tmpparent = window.opener;
				document.getElementById('show').innerHTML = tmpparent.GetDataGridShow();
			}	
		</script>
		<style> @media Print { .prt { VISIBILITY: hidden }}
		</style>
	</head>
	<body onload="LoadPage();" MS_POSITIONING="GridLayout">
		<DIV class="prt" id="printmenu" align="left"><INPUT class="prt" onclick="javascript:Print(6,1);" type="button" value="打印" name="button_print">
			<INPUT class="prt" onclick="javascript:Print(7,1);" type="button" value="打印预览" name="button_setup">
			<INPUT class="prt" onclick="javascript:Print(8,1);" type="button" value="打印页面设置" name="button_show">
			<BR>
		</DIV>
		<form id="Form1" method="post" runat="server">
			<div id="show">
			</div>
		</form>
	</body>
</html>
