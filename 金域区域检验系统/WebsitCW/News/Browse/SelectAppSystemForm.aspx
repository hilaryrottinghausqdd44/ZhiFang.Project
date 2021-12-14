<%@ Page Language="c#" AutoEventWireup="True"
    Inherits="OA.News.Browse.SelectAppSystemForm" Codebehind="SelectAppSystemForm.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>选择应用系统</title>
    <base target="_self">
    <!-- 这样才不会弹出新窗口 -->
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../../WebControlLib/CSS/WebControlDefault.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" src="../news/manage/htmledit/Dialog/dialog.js"></script>

    <script language="javascript" type="text/javascript">
			//取框架的URL
			oSelection = dialogArguments.eWebEditor.document.selection.createRange();
			sRangeType = dialogArguments.eWebEditor.document.selection.type;
			var url;
			if (sRangeType == "Control")
			{
				if (oSelection.item(0).tagName == "IFRAME")
				{
					oControl = oSelection.item(0);
					url = oControl.src;
				}
			}
			
			//body的onload调用
			function showParamsToForm()
			{
				if(url == null)
				{
					return;
				}
				//从 url 中取 参数配置情况
				var splitUrl = url.split("?");
				var splitParams = splitUrl[1].split("&");//取到参数列表并拆分
				var systemName;
				var tableName;
				var pageSize;
				for(var i=0;i<splitParams.length;i++)
				{
					var splitParam = splitParams[i].split("=");//拆分当前参数参数
					var paramName = splitParam[0];
					var paramValue = splitParam[1];
					if(paramName == "systemName")
						systemName = paramValue;
					else if(paramName == "tableName")
						tableName = paramValue;
					else if(paramName == "pageSize")
						pageSize = paramValue;
				}
				//alert(systemName);
				//alert(pageSize);
				//var splitParam = splitParams[0].split("=");//拆分第一个参数
				//var systemName = splitParam[1];//取到第一个参数的名称
				var obj = document.getElementById("ddlAppSystem");
				var num = obj.options.length; 
				for(var i=0; i<num; i++)
				{
					if(obj.options[i].text == systemName)
					{
						obj.options[i].selected = true;
						break;
					}      
				}
				//取应用系统下的表列表
				ShowTableListFromSystemName(obj);
				//显示当前选择的表名称
				var obj = document.getElementById("ddlTable");
				var num = obj.options.length; 
				for(var i=0; i<num; i++)
				{
					if(obj.options[i].text == tableName)
					{
						obj.options[i].selected = true;
						break;
					}      
				}
				//记录数
				var objRows = document.getElementById("txtRows");
				objRows.value = pageSize;
			}

			function ShowTableListFromSystemName(obj)
			{
				//取到应用系统名称
				var objID = document.getElementById("ddlAppSystem");
				var systemName = objID.value;
				//取该应用系统下的所有表
				var tableList = OA.News.Browse.SelectAppSystemForm.getTableListFromSystemName(systemName).value;
				var objTable = document.getElementById("ddlTable");
				objTable.length = 0;//先清空表列表
				for(var i=0;i<tableList.length;i++)
				{
					objTable.options.add(new Option(tableList[i], tableList[i]));//为控件添加项
				}

			}
    </script>

    <script language="JavaScript" event="onclick" for="Ok">
			//取应用系统
			var objSystem = document.getElementById("ddlAppSystem");
			systemName = objSystem.value;
			if(systemName == "")
			{
				self.close();
				return;
			}
			url = "../../News/Browse/BrowseAppSystemDataForm.aspx?systemName=" + systemName;
			//取表名称
			var objTable = document.getElementById("ddlTable");
			tableName = objTable.value;
			if(tableName != "")
			{
				url += "&tableName=" + tableName;
			}
			//行数
			pageSize = txtRows.value;
			url += "&pageSize=" + pageSize;
			sScrolling = "both"
			sFrameBorder = "0";
			sMarginHeight = "0";
			sMarginWidth = "0";
			sWidth = "300";
			sHeight = "200";
			dialogArguments.insertHTML("<iframe src='"+url+"' scrolling='"+sScrolling+"' frameborder='"+sFrameBorder+"' marginheight='"+sMarginHeight+"' marginwidth='"+sMarginWidth+"' width='"+sWidth+"' height='"+sHeight+"'></iframe>");
			window.returnValue = null;
			self.close();
			return;
    </script>

</head>
<body ms_positioning="GridLayout" onload="showParamsToForm()">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" cellspacing="1" cellpadding="1" width="448" border="1">
        <tr>
            <td colspan="2">
                <asp:Label ID="lblTitile" runat="server" Visible="False">标题</asp:Label>
            </td>
        </tr>
        <tr>
            <td nowrap align="right" width="1%">
                <asp:Label ID="lblNewsCatagory" runat="server">应用系统名称</asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlAppSystem" runat="server" Width="100%">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td nowrap align="right" width="1%">
                <asp:Label ID="Label2" runat="server">表名称</asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlTable" runat="server" Width="100%">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td nowrap align="right" width="1%">
                <asp:Label ID="Label1" runat="server">每页显示记录数</asp:Label>
            </td>
            <td align="left">
                <input id="txtRows" maxlength="4" size="10" name="txtRows" value="10" onkeypress="event.returnValue=IsDigit();">
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <font face="宋体">
                    <input id="Ok" type="submit" value="  确定  " name="Ok"></font>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
