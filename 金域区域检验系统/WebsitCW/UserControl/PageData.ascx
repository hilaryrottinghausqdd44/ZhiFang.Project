<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PageData.ascx.cs" Inherits="OA.UserControl.PageData" %>
<link href="../Css/style.css" rel="stylesheet" />
<div class="PageDiv">
 <asp:LinkButton ID="hlIndex" runat="server" onclick="hlIndex_Click"
                        >首页</asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="hlPre" runat="server" onclick="hlPre_Click" 
                        >上一页</asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="hlNext" runat="server" onclick="hlNext_Click" 
                        >下一页</asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="hlLast" runat="server" onclick="hlLast_Click"  
                        >尾页</asp:LinkButton>                    
                    &nbsp;当前<asp:Label ID="lblCurrentPage" runat="server"></asp:Label>页,
                    共<asp:Label ID="lblTotalPage" runat="server"></asp:Label>页
</div>

