<%@ Register TagPrefix="uc1" TagName="DataListWebControl" Src="../../WebControlLib/DataListWebControl.ascx" %>

<%@ Page validateRequest="false" enableEventValidation="false" Language="c#"
    AutoEventWireup="True" Inherits="OA.News.Browse.BrowseAppSystemDataForm" Codebehind="BrowseAppSystemDataForm.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>设置应用程序</title>
    <link href="../../WebControlLib/CSS/WebControlDefault.css" type="text/css" rel="Stylesheet"/>

    <script type="text/jscript">
			//自动调整框架的大小为页面的大小
			function iframeAutoFit()
			{
				//延迟
				//setTimeout("alert('aaa');",2000);
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
								//最低的高度
								if(h < 100)
									h = 100;

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

			function getParamValueFromUrl(url, getParamName)
			{
				//从 url 中取 参数配置情况
				var splitUrl = url.split("?");
				var splitParams = splitUrl[1].split("&");//取到参数列表并拆分
				for(var i=0;i<splitParams.length;i++)
				{
					var splitParam = splitParams[i].split("=");//拆分当前参数参数
					var paramName = splitParam[0];
					var paramValue = splitParams[i].replace(paramName + "=", "");
					if(paramName == getParamName)
						return paramValue;
				}
				return "";
			}

			//返回关联的子模块名称列表
			function makeAndResetIframeUrl(selectFieldName, selectFieldValue, iframeID)
			{
				//alert(selectFieldName);
				iframeUrl = "";
				var allFrame = window.parent.document.body.getElementsByTagName("IFRAME");
				var selectIFRAME = 0;//保存选中的框架ID
				for (var i=0; i < allFrame.length; i++)
				{
					//找到框架
					if(allFrame[i].id == iframeID)
					{
						iframeUrl = window.parent.document.frames(i).location.href;
						//取对应的主表的字段名称
						var primaryFieldName = getParamValueFromUrl(iframeUrl, "primaryFieldName");
						if(primaryFieldName == "")
							return;
						//不是关联的字段
						if(primaryFieldName != selectFieldName)
						{
							return;
						}
						selectIFRAME = i;
						break;
					}
				}
				if (iframeUrl == "")
				{
					return "";
				}
				
				//从 url 中取 参数配置情况
				var splitUrl = iframeUrl.split("?");
				var splitParams = splitUrl[1].split("&");//取到参数列表并拆分
				//定义参数
				var systemName = "";//对应的应用系统名称
				var tableName = "";//对应的数据库表(模块的主表)
				var pageSize = "";//每页显示的记录数
				var modalName = "";//模块名称
				var modalID = "";//模块标识
				var relationModalName = "";//关联的主模块名称
				var relationFieldName = "";//关联的字段名称
				var primaryFieldName = "";//关联的主表的字段名称
				var relationFieldValue = "";//关联的字段内容
				var selectFields = "";//选择的字段(在数据列表中显示的字段)
				var whereSQL = "";//固定的查询条件
				var sql = "";//完整的查询语句(如果定义了本语句,则只运行本语句,别的设置与查询有关的参数都不起作用)
				for(var i=0;i<splitParams.length;i++)
				{
					var splitParam = splitParams[i].split("=");//拆分当前参数参数
					var paramName = splitParam[0];
					//var paramValue = splitParam[1];
					var paramValue = splitParams[i].replace(paramName + "=", "");
					if(paramName == "systemName") systemName = paramValue;//对应的应用系统名称
					else if(paramName == "tableName") tableName = paramValue;
					else if(paramName == "pageSize") pageSize = paramValue;
					else if(paramName == "modalName") modalName = paramValue;
					else if(paramName == "modalID") modalID = paramValue;
					else if(paramName == "relationModalName") relationModalName = paramValue;
					else if(paramName == "primaryFieldName") primaryFieldName = paramValue;
					else if(paramName == "relationFieldName") relationFieldName = paramValue;
					else if(paramName == "relationFieldValue") relationFieldValue = paramValue;
					else if(paramName == "selectFields") selectFields = paramValue;
					else if(paramName == "whereSQL") whereSQL = paramValue;
					else if(paramName == "sql") sql = paramValue;
				}
				//当前选择的关联的内容
				relationFieldValue = selectFieldValue;
				//页面名称
				var pageName = "../../News/Browse/BrowseAppSystemDataForm.aspx";
				//参数列表
				var paramUrl = "systemName=" + systemName + "&amp;tableName=" + tableName + "&amp;pageSize=" + pageSize + "&amp;modalName=" + modalName + "&amp;modalID=" + modalID + "&amp;relationModalName=" + relationModalName + "&amp;relationFieldName=" + relationFieldName + "&amp;primaryFieldName=" + primaryFieldName + "&amp;relationFieldValue=" + relationFieldValue +  "&amp;selectFields=" + selectFields + "&amp;whereSQL=" + whereSQL + "&amp;sql=" + sql;
				//框架的URL
				var newUrl = pageName + "?" + paramUrl;
				//把URL设回去
				window.parent.document.frames(selectIFRAME).location.href = newUrl;
				return relationModalName;
			}
			 
			function AfterCellClick(selectFieldName, selectFieldValue)
			{
				//alert(selectFieldName);
				//取当前的框架的id(因为只刷新跟当前选择内容有关联的框架)
				var currentId = window.frameElement.name;
				var allFrame = window.parent.document.body.getElementsByTagName("IFRAME");
				for (var i=0; i < allFrame.length; i++)
				{
					//框架的id
					var id = allFrame[i].id;
					if(currentId != id)//不是当前选中的框架,继续下一个框架
					{
						continue;
					}
					//alert("当前模块为:" + currentId);
					//取url
					var url = allFrame[i].src;
					//alert(url);
					//取关联的子模块名称
					var relationModalName = getParamValueFromUrl(url, "relationModalName");
					if(relationModalName == "")
						continue;
					//alert(relationModalName);
					//拆分
					var relModalList = relationModalName.split(",");
					//alert(relModalList.length);
					for(var j=0;j<relModalList.length; j++)
					{
						var relationIframeID = relModalList[j];
						//alert("刷新的关联的子模块为:" + relationIframeID);
						var url = makeAndResetIframeUrl(selectFieldName, selectFieldValue, relationIframeID);
					}
				}
			}
			
    </script>

</head>
<body onload="iframeAutoFit()">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" cellspacing="1" cellpadding="1" width="100%" border="0">
        <tr>
            <td>
                <uc1:DataListWebControl ID="DataListWebControl1" runat="server">
                </uc1:DataListWebControl>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
