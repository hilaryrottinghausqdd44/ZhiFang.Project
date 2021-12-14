<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChineseFirstChar.aspx.cs" Inherits="OA.DBQuery.Functions.ChineseFirstChar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <style type="text/css">
    input
    {
    	border:solid 1px #989898
    }
    </style>
    <script language="javascript" type="text/javascript">
// <!CDATA[

        function returnVa() {
            parent.window.returnValue = document.all['TextBoxCC'].value;
            parent.window.close();
        }

// ]]>
    </script>
    <script language="javascript" event="onpropertychange" for="TextBoxC">
        //document.all['TextBoxCC'].value=OA.DBQuery.Functions.ChineseFirstChar.GetPYString(this.value).value;
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table>
            <tr>
                <td>
    
        <asp:Label ID="Label1" runat="server" Text="汉字："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxC" runat="server" Width="182px"></asp:TextBox>
                    <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="生成拼音字头" />
                </td>
            </tr>
            <tr>
                <td>
        生成拼音字头：</td>
                <td>
                    <asp:TextBox ID="TextBoxCC" runat="server" Width="182px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
        <input id="Button1" type="button" value="确定并返回" onclick="returnVa()"/></td>
                <asp:Table ID="Table2" runat="server">
                </asp:Table>  </tr>
        </table>
    
    </div>
    <asp:Table ID="Table1" runat="server">
    </asp:Table>
    </form>

</body>
</html>
