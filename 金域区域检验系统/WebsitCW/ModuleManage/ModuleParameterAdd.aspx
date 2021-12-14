<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModuleParameterAdd.aspx.cs"
    Inherits="OA.ModuleManage.ModuleParameterAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>
        <%=Catagory%>-信息内容浏览</title>
    <link href="../news/Main.css" rel="stylesheet" type="text/css" />
   
</head>
<body style="overflow-y:auto;height:100%;">
    <form id="form1" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td colspan="2">
                    <%=(dt!=null&&dt.Rows.Count>0)?dt.Rows[0]["text"].ToString():""%>
                </td>
            </tr>           
        </table>   
    </form>
</body>
</html>
