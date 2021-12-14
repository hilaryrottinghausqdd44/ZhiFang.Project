<%@ Control Language="c#" AutoEventWireup="True" Inherits="theNews.Controls.EachTable"
    TargetSchema="http://schemas.microsoft.com/intellisense/ie5" Codebehind="EachTable.ascx.cs" %>
<p>
    <asp:DataGrid ID="DataGrid1" runat="server" Width="680px" OnSelectedIndexChanged="DataGrid1_SelectedIndexChanged">
    </asp:DataGrid></p>
<asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click"></asp:Button>
