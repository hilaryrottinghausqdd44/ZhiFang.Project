<%@ Page language="c#" AutoEventWireup="False" Inherits="Zhifang.Utilities.Query.Admin.UIConfigLeft" Codebehind="UIConfigLeft.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>UIConfigLeft</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../css/ioffice.css" type="text/css" rel="stylesheet">

    <script language="javascript">
		<!--
			var currentSelect = null;
			var lastObj = null;
			function LeftTableInfo(obj)
			{				
				var str = new String();
				str = obj.title;
				str = str.substring(0, str.length-1);
				
				//===增加高亮=====
				currentSelect = obj;
				obj.style.backgroundColor = "skyblue";
				if(lastObj!=null && lastObj != obj)
				{
					lastObj.style.backgroundColor = "white";
				}
				lastObj = obj;
				//=======End======
				
				window.parent.frames["Content"].document.location.href = "QueryConfig.aspx?<%=Request.ServerVariables["Query_String"]%>&TableName=" + str;
				return false;
			}
			
			function TopTableInfo(obj)
			{
				var str = new String();
				str = obj.title;
				str = str.substring(0, str.length-1);
				
				//===增加高亮=====
				currentSelect = obj;
				obj.style.backgroundColor = "skyblue";
				if(lastObj!=null && lastObj != obj)
				{
					lastObj.style.backgroundColor = "white";
				}
				lastObj = obj;
				//=======End======
				
				window.parent.frames["Content"].document.location.href = "TopConfig.aspx?<%=Request.ServerVariables["Query_String"]%>&TableName=" + str;
				return false;
			}
			
			function InputTableInfo(obj)
			{
				var str = new String();
				str = obj.title;
				str = str.substring(0, str.length-1);
				
				//===增加高亮=====
				currentSelect = obj;
				obj.style.backgroundColor = "skyblue";
				if(lastObj!=null && lastObj != obj)
				{
					lastObj.style.backgroundColor = "white";
				}
				lastObj = obj;
				//=======End======
				
				window.parent.frames["Content"].document.location.href = "InputConfig.aspx?<%=Request.ServerVariables["Query_String"]%>&TableName=" + str;
				return false;
			}
			
			function ChildDisplayInfo(obj)
			{
				var str = new String();
				str = obj.title;
				str = str.substring(0, str.length-1);
				
				//===增加高亮=====
				currentSelect = obj;
				obj.style.backgroundColor = "skyblue";
				if(lastObj!=null && lastObj != obj)
				{
					lastObj.style.backgroundColor = "white";
				}
				lastObj = obj;
				//=======End======
				
				window.parent.frames["Content"].document.location.href = "InputAssessorKeyColumns.aspx?<%=Request.ServerVariables["Query_String"]%>&TableName=" + str;
				return false;
			}
			
			//在界面配置时选定的高亮显示
			
			function ModulesNavigate(obj)
			{
				var str = new String();
				str = obj.title;
				str = str.substring(0, str.length-1);
				
				//===增加高亮=====
				currentSelect = obj;
				obj.style.backgroundColor = "skyblue";
				if(lastObj!=null && lastObj != obj)
				{
					lastObj.style.backgroundColor = "white";
				}
				lastObj = obj;
				//=======End======
				
				window.parent.frames["Content"].document.location.href = "ModulesNavigate.aspx?<%=Request.ServerVariables["Query_String"]%>&TableName=" + str;
				return false;
			}
			
			
			function DataLink(obj)
			{
				var str = new String();
				str = obj.title;
				str = str.substring(0, str.length-1);
				
				//===增加高亮=====
				currentSelect = obj;
				obj.style.backgroundColor = "skyblue";
				if(lastObj!=null && lastObj != obj)
				{
					lastObj.style.backgroundColor = "white";
				}
				lastObj = obj;
				//=======End======
				
				window.parent.frames["Content"].document.location.href = "DataNavigate.aspx?<%=Request.ServerVariables["Query_String"]%>&TableName=" + str;
				return false;
			}
			
			//浏览
			function DataBrowse(obj)
			{
				var str = new String();
				str = obj.title;
				str = str.substring(0, str.length-1);
				
				//===增加高亮=====
				currentSelect = obj;
				obj.style.backgroundColor = "skyblue";
				if(lastObj!=null && lastObj != obj)
				{
					lastObj.style.backgroundColor = "white";
				}
				lastObj = obj;
				//=======End======
				
				window.parent.frames["Content"].document.location.href = "BrowseConfig.aspx?<%=Request.ServerVariables["Query_String"]%>&TableName=" + str;
				return false;
			}
			
			//数据视图
			function DataView(obj)
			{
				var str = new String();
				str = obj.title;
				str = str.substring(0, str.length-1);
				
				//===增加高亮=====
				currentSelect = obj;
				obj.style.backgroundColor = "skyblue";
				if(lastObj!=null && lastObj != obj)
				{
					lastObj.style.backgroundColor = "white";
				}
				lastObj = obj;
				//=======End======
				
				window.parent.frames["Content"].document.location.href = "ViewConfig.aspx?<%=Request.ServerVariables["Query_String"]%>&TableName=" + str;
				return false;
			}
			
		//-->
    </script>

</head>
<body ms_positioning="GridLayout">
    <form id="Form1" name="Form1" method="post" runat="server">
    <table>
        <tr>
            <td>
                上部查询显示配置信息
            </td>
        </tr>
        <tr>
            <td>
                <table id="tableTopInfo" style="border-right: #0099cc 1px solid; border-top: #0099cc 1px solid;
                    border-left: #0099cc 1px solid; border-bottom: #0099cc 1px solid" runat="server">
                    <tr>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                左边折叠显示配置信息
            </td>
        </tr>
        <tr>
            <td>
                <table id="tableLeftInfo" style="border-right: #0099cc 1px solid; border-top: #0099cc 1px solid;
                    border-left: #0099cc 1px solid; border-bottom: #0099cc 1px solid" runat="server">
                    <tr>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                录入配置信息
            </td>
        </tr>
        <tr>
            <td>
                <table id="tableInputInfo" style="border-right: #0099cc 1px solid; border-top: #0099cc 1px solid;
                    border-left: #0099cc 1px solid; border-bottom: #0099cc 1px solid" runat="server">
                    <tr>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                在子表录入时显示
            </td>
        </tr>
        <tr>
            <td>
                <table id="tableChildDisplay" style="border-right: #0099cc 1px solid; border-top: #0099cc 1px solid;
                    border-left: #0099cc 1px solid; border-bottom: #0099cc 1px solid" runat="server">
                    <tr>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                配置导航信息
            </td>
        </tr>
        <tr>
            <td>
                <table id="ModulesNavigate" style="border-right: #0099cc 1px solid; border-top: #0099cc 1px solid;
                    border-left: #0099cc 1px solid; border-bottom: #0099cc 1px solid" runat="server">
                    <tr>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                数据连接配置信息
            </td>
        </tr>
        <tr>
            <td>
                <table id="DataLink" style="border-right: #0099cc 1px solid; border-top: #0099cc 1px solid;
                    border-left: #0099cc 1px solid; border-bottom: #0099cc 1px solid" runat="server">
                    <tr>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                浏览配置信息
            </td>
        </tr>
        <tr>
            <td>
                <table id="tableDataBrowse" style="border-right: #0099cc 1px solid; border-top: #0099cc 1px solid;
                    border-left: #0099cc 1px solid; border-bottom: #0099cc 1px solid" runat="server">
                    <tr>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                视图配置信息
            </td>
        </tr>
        <tr>
            <td>
                <table id="tableDataView" style="border-right: #0099cc 1px solid; border-top: #0099cc 1px solid;
                    border-left: #0099cc 1px solid; border-bottom: #0099cc 1px solid" runat="server">
                    <tr>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
