<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModelAdd.aspx.cs" Inherits="ZhiFang.WebLis.ReportPrint.ModelAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>模板添加</title>
    <link href="../Css/Default.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="../js/Tools.js"></script>
</head>
<body>
    <form id="form1" runat="server" onsubmit="return checkform('TxtBox_ModelName=模板名称！,TxtBox_ModelItemCount=项目数！,TxtBox_ModelPaperSize=纸张大小！');">
    <div>
    <table width="100%" cellspacing="1" cellpadding="0" border="0" style="background-color:#0099cc;margin-top:10px">
    <tr style="background-color:#FFFFFF">
    <td align="center" colspan="2"><div style="font-size:20px; font-weight:bold; margin:10px">模板<asp:Label ID="Label1" runat="server" Text="添加"></asp:Label></div> </td>
    </tr>
    <tr style="background-color:#FFFFFF" ><td align="right" width="20%" style="padding:10px">模板名称:</td><td style="padding:10px"><asp:TextBox ID="TxtBox_ModelName" runat="server"></asp:TextBox>
        </td></tr>
    <tr style="background-color:#FFFFFF"><td align="right" style="padding:10px">模板存放地址:</td><td style="padding:10px"><asp:TextBox ID="TxtBox_ModelAddress" runat="server"></asp:TextBox></td></tr>   
    <tr style="background-color:#FFFFFF"><td align="right" style="padding:10px">项目或图片数:</td><td style="padding:10px"><asp:TextBox ID="TxtBox_ModelItemCount" runat="server"></asp:TextBox></td></tr>
    <tr style="background-color:#FFFFFF"><td align="right" style="padding:10px">纸张大小:</td><td style="padding:10px"><asp:TextBox ID="TxtBox_ModelPaperSize" runat="server"></asp:TextBox></td></tr>
    <tr style="background-color:#FFFFFF"><td align="right" style="padding:10px">模板描述:</td><td style="padding:10px">
        <asp:TextBox ID="TxtBox_ModelDesc" runat="server" Height="57px" 
            TextMode="MultiLine" Width="300px" ></asp:TextBox></td></tr>
    <tr style="background-color:#FFFFFF"><td align="right" style="padding:10px">套打标志:</td><td style="padding:10px">
        <asp:DropDownList ID="DDL_ModelBatchFlag" runat="server">
        <asp:ListItem Selected=True Value="0">否</asp:ListItem>
        <asp:ListItem Value="1">是</asp:ListItem>
        </asp:DropDownList>
        </td></tr>
        <tr style="background-color:#FFFFFF"><td align="right" style="padding:10px">是否带图:</td><td style="padding:10px">
        <asp:DropDownList ID="DDL_ImageFlag" runat="server">
        <asp:ListItem Selected=True Value="0">否</asp:ListItem>
        <asp:ListItem Value="1">是</asp:ListItem>
        </asp:DropDownList>
        </td></tr>
        <tr style="background-color:#FFFFFF"><td align="right" style="padding:10px">是否有抗生素:</td><td style="padding:10px">
        <asp:DropDownList ID="DDL_AntiFlag" runat="server">
        <asp:ListItem Selected=True Value="0">否</asp:ListItem>
        <asp:ListItem Value="1">是</asp:ListItem>
        </asp:DropDownList>
        </td></tr>
         <tr style="background-color:#FFFFFF"><td align="right" style="padding:10px">模板文件:</td><td style="padding:10px">
             <asp:FileUpload ID="FileUpload1" runat="server" />
             </td></tr>
             <tr><td colspan="2" align="center" valign="top" style="background-color:#FFFFFF;padding:10px">
                 <asp:Button ID="Button1" runat="server" Text="确定" onclick="Button1_Click" />&nbsp;&nbsp;<input type="button" value="关闭" onclick="window.close();" /></td></tr>
    </table>
    </div>
    </form>
</body>
</html>
