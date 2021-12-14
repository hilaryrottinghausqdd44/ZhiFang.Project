<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QueryConfigFunctions.aspx.cs" Inherits="OA.DBQuery.Admin.QueryConfigFunctions" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" width="100%" height="100%">
        <tr>
            <td width="10%"><input type="button" value="确认"  onclick="JAVAScript:window.returnValue=form1.txtFunctions.value;window.close();"/></td>
            <td colspan="2">
                <textarea style="width:100%" rows="3" id="txtFunctions"><%=Request.QueryString["FunctionString1"].Replace("$1$", "&amp;")%></textarea>
            </td>
        </tr>
        <tr>
            <td>说明:<br />(供复制参考)
            </td>
            <td colspan="2"><textarea style="width:100%" rows="6">功能说明||字段中文名称1,中文名称2,中文名称3|true|程序.aspx?a=1&b=2|onclick||中,中,80%,80%|窗口名称
一|二|三|四|五|六|七|八|九.....(示例):||姓名,合同编号|true|/OA/news/browse/HomepageData.aspx?ID=234|onclick||中,中,80%,80%|frmTarget
三：输入字段名称（字段描述性名称，单表字段里的中文名称）
五：打开程序地址:1../../news/browse/HomepageData.aspx
八：打开窗口的大小
九：打开窗口的位置，如frmTarget,如果为空，或未找到frmTarget,则弹出新窗口,窗口大小取八的设置</textarea>
            <%  //生成序列号||合同编号,项目类别,用户名称|true|asdf.aspx|onclick|
                    //=Request.ServerVariables["Query_String"].Substring(Request.ServerVariables["Query_String"].IndexOf("db="))%>
            </td>
            
        </tr>
        <tr height="95%">
            <td colspan="3" height="95%">
                <iframe name="ifrmRule" width="100%" height="400" src="InputFunctionString.aspx?<%=Request.ServerVariables["Query_String"]%><%////.Substring(Request.ServerVariables["Query_String"].IndexOf("db=")) %>"></iframe>
            </td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>
