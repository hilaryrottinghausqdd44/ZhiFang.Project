<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OADetailDataInputUserControl.ascx.cs" Inherits="OA.DataUserControlLib.OADetailDataInputUserControl" %>
    
    
<link href="../App_Themes/zh-cn/DataUserControl.css" rel="stylesheet" type="text/css" />
<table id="tableParent" runat="server" border="1" cellpadding="1" cellspacing="1" width="100%">
    <tr>
        <td width="1%" bgcolor="#f1e3ff" nowrap>
            <asp:Label ID="lblTitle" runat="server" CssClass="LabelTitle" Visible="True" Font-Bold="True">控件的标题</asp:Label>
            <!-- 每行显示的字段个数 -->
            <asp:Label ID="lblRepeatColumns" runat="server" Text="每行显示字段数:"></asp:Label>
            <asp:LinkButton ID="btnRepeatColumns1" runat="server" CommandName="RepeatColumns" OnClick="btnSelectLinkButtonClick">1</asp:LinkButton>
            <asp:LinkButton ID="btnRepeatColumns2" runat="server" CommandName="RepeatColumns" OnClick="btnSelectLinkButtonClick">2</asp:LinkButton>
            <asp:LinkButton ID="btnRepeatColumns3" runat="server" CommandName="RepeatColumns" OnClick="btnSelectLinkButtonClick">3</asp:LinkButton>
            <asp:LinkButton ID="btnRepeatColumns4" runat="server" CommandName="RepeatColumns" OnClick="btnSelectLinkButtonClick">4</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Table ID="tableDetailDataInput" runat="server" GridLines="Both" CssClass="GridView" Width="100%" EnableViewState="false">
            </asp:Table>
        </td>
    </tr>
    <tr>
        <td>
        </td>
    </tr>
    <tr>
        <td align="center">
            <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" Enabled="false"></asp:Button>
         </td>
    </tr>
</table>
<%--    
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <Triggers> 
                    <asp:PostBackTrigger ControlID="btnSave" /> 
                    <asp:PostBackTrigger ControlID="btnRepeatColumns1" /> 
                    <asp:PostBackTrigger ControlID="btnRepeatColumns2" /> 
                    <asp:PostBackTrigger ControlID="btnRepeatColumns3" /> 
                    <asp:PostBackTrigger ControlID="btnRepeatColumns4" /> 
                </Triggers>
            </asp:UpdatePanel>
--%>