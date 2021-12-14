<%@ Page validateRequest="false" enableEventValidation="false" Language="c#"
    AutoEventWireup="True" Inherits="OA.News.Browse.SetObjectParamForm" Codebehind="SetObjectParamForm.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>添加或设置应用系统</title>
    <!-- 这样才不会弹出新窗口 -->
    <base target="_self">
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">

    <link href="../../WebControlLib/CSS/WebControlDefault.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" src= ="../news/manage/htmledit/Dialog/dialog.js"></script>

    <script language="javascript" type="text/javascript">
			//帮定事件
			if(window.attachEvent)
			{
				window.attachEvent("onload", window_onload);
			}
			else if(window.addEventListener)
			{
				window.addEventListener('load', window_onload, false);
			}
			
			
			function window_onload()
			{
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
				//显示参数
				showParamsToForm(url)
				return;
			}
			
			//body的onload调用:显示参数(从框架的URL提取,然后分解成参数)
			function showParamsToForm(url)
			{
				//url = convertESCToHtml(url);
				//alert(url);
				if(url == null)
				{
					return;
				}
				//从 url 中取 参数配置情况
				var splitUrl = url.split("?");
				var splitParams = splitUrl[1].split("&");//取到参数列表并拆分
				//定义参数
				var systemName = "";//对应的应用系统名称
				var tableName = "";//对应的数据库表(模块的主表)
				var pageSize = "";//每页显示的记录数
				var modalName = "";//模块名称
				var modalID = "";//模块标识(用做框架的ID和Name)
				var relationModalName = "";//关联的主模块名称
				var relationFieldName = "";//关联的字段名称
				var primaryFieldName = "";//对应的主表的字段名称
				var relationFieldValue = "";//关联的字段内容
				var selectFields = "";//选择的字段(在数据列表中显示的字段)
				var whereSQL = "";//固定的查询条件
				var sql = "";//完整的查询语句(如果定义了本语句,则只运行本语句,别的设置与查询有关的参数都不起作用)
				for(var i=0;i<splitParams.length;i++)
				{
					var splitParam = splitParams[i].split("=");//拆分当前参数参数
					var paramName = splitParam[0];
					var paramValue = splitParams[i].replace(paramName + "=", "");
					if(paramName == "systemName") systemName = paramValue;//对应的应用系统名称
					else if(paramName == "tableName") tableName = paramValue;
					else if(paramName == "pageSize") pageSize = paramValue;
					else if(paramName == "modalName") modalName = paramValue;
					else if(paramName == "modalID") modalID = paramValue;
					else if(paramName == "relationModalName") relationModalName = paramValue;
					else if(paramName == "relationFieldName") relationFieldName = paramValue;
					else if(paramName == "primaryFieldName") primaryFieldName = paramValue;
					else if(paramName == "relationFieldValue") relationFieldValue = paramValue;
					else if(paramName == "selectFields") selectFields = paramValue;
					else if(paramName == "whereSQL") whereSQL = paramValue;
					else if(paramName == "sql") sql = paramValue;
				}
				//显示当前选择的应用系统名称
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
					if(obj.options[i].value == tableName)
					{
						obj.options[i].selected = true;
						break;
					}      
				}
				//显示记录数
				var obj = document.getElementById("txtRows");
				obj.value = pageSize;
				//模块名称
				var obj = document.getElementById("txtModalName");
				obj.value = modalName;
				//模块标识
				var obj = document.getElementById("txtModalID");
				obj.value = modalID;
				//关联的主模块名称
				var obj = document.getElementById("txtMainModalName");
				obj.value = relationModalName;
				//关联的字段名称
				var obj = document.getElementById("txtRelationField");
				obj.value = relationFieldName;
				//对应的主表的字段名称
				var obj = document.getElementById("txtPrimaryField");
				obj.value = primaryFieldName;
				//选择的字段
				var obj = document.getElementById("txtSelectField");
				obj.value = selectFields;
				//固定的查询条件
				//转义
				//whereSQL = convertESCToHtml(whereSQL);
				var obj = document.getElementById("txtWhere");
				obj.value = whereSQL;
				//完整的查询语句
				//转义
				//sql = convertESCToHtml(sql);
				var obj = document.getElementById("txtSQL");
				obj.value = sql;
				return;
			}


			//显示应用系统下的数据表
			function ShowTableListFromSystemName(obj)
			{
				//取到应用系统名称
				var objID = document.getElementById("ddlAppSystem");
				var systemName = objID.value;
				if((systemName == "") || (systemName == null))
				{
					return;
				}
				//取该应用系统下的所有表
				var tableList = OA.News.Browse.SetObjectParamForm.getTableListFromSystemName(systemName, false).value;
				var tableListDesc = OA.News.Browse.SetObjectParamForm.getTableListFromSystemName(systemName, true).value;
				var objTable = document.getElementById("ddlTable");
				objTable.length = 0;//先清空表列表
				for(var i=0;i<tableList.length;i++)
				{
					objTable.options.add(new Option(tableListDesc[i], tableList[i]));//为控件添加项
				}
				return;
			}
			
		function convertESCToHtml(esc)
		{
			var ret = esc;
			//转义
			ret = ret.replace(new RegExp("&amp;" , "gm"), "&");
			ret = ret.replace(new RegExp("&quot;" , "gm"), "\"");
			ret = ret.replace(new RegExp("&apos;" , "gm"), "'");
			ret = ret.replace(new RegExp("&gt;" , "gm"), ">");
			ret = ret.replace(new RegExp("&lt;" , "gm"), "<");
			ret = ret.replace(new RegExp("&nbsp;" , "gm"), " ");
			ret = ret.replace(new RegExp("%20" , "gm"), " ");
			ret = ret.replace(new RegExp("EqualTo" , "gm"), "=");
			return ret;
		}
		function convertHtmlToESC(html)
		{
			var ret = html;
			//转义
			ret = ret.replace(new RegExp("&" , "gm"), "&amp;");
			ret = ret.replace(new RegExp("\"" , "gm"), "&quot;");
			ret = ret.replace(new RegExp("'" , "gm"), "&apos;");
			ret = ret.replace(new RegExp(">" , "gm"), "&gt;");
			ret = ret.replace(new RegExp("<" , "gm"), "&lt;");
			ret = ret.replace(new RegExp("=" , "gm"), "EqualTo");
			return ret;
		}

			
			
    </script>

    <!-- 选择字段的设置按钮 -->

    <script language="JavaScript" event="onclick" for="btnSelectField">
			var objSystem = document.getElementById("ddlAppSystem");
			systemName = objSystem.value;
			if(systemName == "")
			{
				alert("请先选择应用系统!");
				ddlAppSystem.focus();
				return;
			}
			//取表名称
			var objTable = document.getElementById("ddlTable");
			tableName = objTable.value;
			if(tableName == "")
			{
				alert("请先选择表!");
				ddlTable.focus();
				return;
			}
			//固定的查询条件
			//txtSelectField.value = tableName;
    </script>

    <!-- 查询条件的设置按钮 -->

    <script language="JavaScript" event="onclick" for="btnSetQueryCondition">
			var objSystem = document.getElementById("ddlAppSystem");
			var systemName = objSystem.value;
			if(systemName == "")
			{
				alert("请先选择应用系统!");
				ddlAppSystem.focus();
				return;
			}
			//取表名称
			var objTable = document.getElementById("ddlTable");
			var tableName = objTable.value;
			if(tableName == "")
			{
				alert("请先选择表!");
				ddlTable.focus();
				return;
			}
			//固定的查询条件
			var url = "../../News/Browse/SetQueryConditionForm.aspx?systemName=" + systemName + "&tableName=" + tableName;
            var ret = window.showModalDialog(url,'','dialogWidth:100px;dialogHeight:100px;help:no;scroll:both;status:no');
			if(ret != null)
			{
				//alert(ret);
				//ret = convertESCToHtml(ret);
				var objWhere = document.getElementById("txtWhere");
				objWhere.value = ret;
			}
    </script>

    <!-- 确定按钮 -->

    <script language="JavaScript" event="onclick" for="Ok">
			//给确定按钮使用
			//取应用系统
			var objSystem = document.getElementById("ddlAppSystem");
			systemName = objSystem.value;
			if(systemName == "")
			{
				self.close();
				return;
			}
			//默认模块名称为应用系统名称
			if(txtModalName.value == "")
				txtModalName.value = systemName;
			//默认模块标识为应用系统名称
			if(txtModalID.value == "")
				txtModalID.value = systemName;
			//拼URL
			url = "../../News/Browse/BrowseAppSystemDataForm.aspx?systemName=" + systemName;
			//取表名称
			var objTable = document.getElementById("ddlTable");
			tableName = objTable.value;
			if(tableName != "")
			{
				url += "&tableName=" + tableName;
			}
			//记录数
			pageSize = txtRows.value;
			url += "&pageSize=" + pageSize;
			//模块名称
			modalName = txtModalName.value;
			url += "&modalName=" + modalName;
			if(modalName == "")
			{
				alert("请输入模块名称!");
				txtModalName.focus();
				return;
			}
			//模块标识
			if(txtModalID.value == "")
				txtModalID.value = modalName;
			modalID = txtModalID.value;
			url += "&modalID=" + modalID;
			//关联的主模块名称
			relationModalName = txtMainModalName.value;
			url += "&relationModalName=" + relationModalName;
			//关联的字段名称
			relationFieldName = txtRelationField.value;
			url += "&relationFieldName=" + relationFieldName;
			//对应的主表的字段名称
			primaryFieldName = txtPrimaryField.value;
			url += "&primaryFieldName=" + primaryFieldName;
			//关联的字段内容
			relationFieldValue = "";
			url += "&relationFieldValue=" + relationFieldValue;
			//选择的字段
			selectFields = txtSelectField.value;
			url += "&selectFields=" + selectFields;
			//固定的查询条件
			whereSQL = txtWhere.value;
			//将查询条件转义
			//whereSQL = convertHtmlToESC(whereSQL);
			url += "&whereSQL=" + whereSQL;
			//完整的查询语句
			sql = txtSQL.value;
			//转义
			//sql = convertHtmlToESC(sql);
			url += "&sql=" + sql;
			//框架的参数
			sScrolling = "both"
			sFrameBorder = "0";
			sMarginHeight = "0";
			sMarginWidth = "0";
			sWidth = "300";
			sHeight = "200";
			var insertIFRAME = "<iframe src=\"" + url + "\" id='" +modalID + "' name='" + modalID+"' scrolling='"+sScrolling+"' frameborder='"+sFrameBorder+"' marginheight='"+sMarginHeight+"' marginwidth='"+sMarginWidth+"' width='"+sWidth+"' height='"+sHeight+"'></iframe>";
			//alert(insertIFRAME);
			dialogArguments.insertHTML(insertIFRAME);
			window.returnValue = insertIFRAME;
			self.close();
			return;
    </script>

</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" cellspacing="1" cellpadding="1" width="100%" border="1">
        <tr>
            <td style="height: 27px" nowrap align="right" width="1%">
                <asp:Label ID="lblNewsCatagory" runat="server">应用系统名称</asp:Label>
            </td>
            <td style="height: 27px">
                <asp:DropDownList ID="ddlAppSystem" runat="server" Width="100%">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td nowrap align="right" width="1%">
                <asp:Label ID="Label8" runat="server">表名称</asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlTable" runat="server" Width="100%">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td nowrap align="right" width="1%">
                <asp:Label ID="lblModalName" runat="server" Width="100%">模块名称</asp:Label>
            </td>
            <td align="left">
                <input id="txtModalName" style="width: 598px; height: 22px" size="94" name="txtModalName">
            </td>
        </tr>
        <tr>
            <td nowrap align="right" width="1%">
                <asp:Label ID="lblModalID" runat="server" Width="100%">模块标识</asp:Label>
            </td>
            <td align="left">
                <input id="txtModalID" style="width: 598px; height: 22px" size="94" name="txtModalID">
            </td>
        </tr>
        <tr>
            <td nowrap align="right" width="1%">
                <asp:Label ID="lblMainModalName" runat="server" Width="100%">关联子模块名称</asp:Label>
            </td>
            <td align="left">
                <input id="txtMainModalName" style="width: 598px; height: 22px" size="94" name="txtMainModalName">
            </td>
        </tr>
        <tr>
            <td nowrap align="right" width="1%">
                <asp:Label ID="Label4" runat="server" Width="100%">关联字段</asp:Label>
            </td>
            <td align="left">
                <input id="txtRelationField" style="width: 598px; height: 22px" size="94" name="txtRelationField">
            </td>
        </tr>
        <tr>
            <td nowrap align="right" width="1%">
                <asp:Label ID="Label2" runat="server" Width="100%">对应主表的字段</asp:Label>
            </td>
            <td align="left">
                <input id="txtPrimaryField" style="width: 598px; height: 22px" size="94" name="txtPrimaryField">
            </td>
        </tr>
        <tr>
            <td nowrap align="right" width="1%">
                <asp:Label ID="Label5" runat="server" Width="100%">显示的字段</asp:Label>
            </td>
            <td align="left">
                <textarea id="txtSelectField" style="width: 510px; height: 54px" name="txtSelectField"
                    rows="3" cols="61"></textarea>
                <input id="btnSelectField" style="width: 82px; height: 30px" type="button" value="  设置  "
                    name="btnSelectField">
            </td>
        </tr>
        <tr>
            <td style="height: 75px" nowrap align="right" width="1%">
                <asp:Label ID="Label6" runat="server" Width="100%">固定的查询条件</asp:Label>
            </td>
            <td valign="middle" align="left" width="100%">
                <textarea id="txtWhere" style="width: 510px; height: 54px" name="txtWhere" rows="3"
                    cols="61"></textarea>
                <input id="btnSetQueryCondition" style="width: 82px; height: 30px" type="button"
                    value="  设置  " name="btnSetQueryCondition">
            </td>
        </tr>
        <tr>
            <td style="height: 76px" nowrap align="right" width="1%">
                <asp:Label ID="Label7" runat="server" Width="100%">完整的查询语句</asp:Label>
            </td>
            <td style="height: 76px" align="left">
                <textarea id="txtSQL" style="width: 598px; height: 96px" name="txtSQL" rows="6" cols="72"></textarea>
            </td>
        </tr>
        <tr>
            <td nowrap align="right" width="1%">
                <asp:Label ID="Label1" runat="server">每页显示记录个数</asp:Label>
            </td>
            <td align="left">
                <input onkeypress="event.returnValue=IsDigit();" id="txtRows" maxlength="4" size="10"
                    value="10" name="txtRows">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <input id="Ok" type="submit" value="  确定  " name="Ok">
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
