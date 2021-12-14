<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModulePreview.aspx.cs" Inherits="OA.ModuleManage.ModulePreview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>[]模块预览</title>

    <script language="javascript" type="text/javascript">
// <!CDATA[

        function window_onload() {

        }
        var ModuleName = '<%=Request.QueryString["ModuleName"] %>';
        var ModuleUrl = '<%=Request.QueryString["ModuleUrl"] %>';
        var DataSelecturl = '<%=Request.QueryString["DataSelecturl"] %>';
        var DataPara = '<%=Request.QueryString["DataPara"] %>';
        

        function SelectPara() {
            var id = window.showModalDialog(DataSelecturl, this, 'dialogWidth:500px;dialogHeight:580px;');
            if (id) {
                if (id.indexOf(",") > 0)
                    id = id.substr(0, id.indexOf(","));
                var PreviewUrl = ModuleUrl + "?" + DataPara + "=" + id;
                //alert(PreviewUrl);
                document.frames["frmPreview"].location.href = ModuleUrl + "?" + DataPara + "=" + id;//可能是问号或&号
            }
        }
        

// ]]>
    </script>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    
        <table border="1" cellpadding="0" cellspacing="0" style="height:100%;width:100%">
            <tr>
                <td>
                    <input id="Button1" type="button" value="选择其他条目数据" onclick="SelectPara()" /> <%=Request.QueryString["ModuleName"] %>模块预览程序</td>
            </tr>
            <tr>
                <td><iframe id="frmPreview" name="frmPreview" 
                src="<%=Request.QueryString["ModuleUrl"] %>" 
                frameborder="1" scrolling="auto"  width="100%" height="600"></iframe>&nbsp</td>
            </tr>
        </table>
    
  
    </form>
</body>
</html>
