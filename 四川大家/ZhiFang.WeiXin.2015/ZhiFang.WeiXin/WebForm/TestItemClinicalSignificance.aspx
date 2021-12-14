<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestItemClinicalSignificance.aspx.cs" Inherits="ZhiFang.WeiXin.TestItemClinicalSignificance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">    
    <div>
    <asp:Label ID="labItemName" runat="server" Text="" ForeColor="#0066CC" Font-Bold="True"></asp:Label>
    </div>
    <div>
    <div id="DivBaseSignificance" runat="server">【临床意义】</div>
    <asp:Label ID="LabBaseSignificance" runat="server" Text=""></asp:Label>
    </div>
    <div>
    <div id="DivHighSignificance" runat="server">偏高</div>
    <asp:Label ID="LabHighSignificance" runat="server" Text=""></asp:Label>
    </div>
    <div>
    <div id="DivLowSignificance" runat="server">偏低</div>
    <asp:Label ID="LabLowSignificance" runat="server" Text=""></asp:Label>
    </div>
    <div>
    <div id="DivSuperHighSignificance" runat="server">超高</div>
    <asp:Label ID="LabSuperHighSignificance" runat="server" Text=""></asp:Label>
    </div>
    <div>
    <div id="DivSuperLowSignificance" runat="server">超低</div>
    <asp:Label ID="LabSuperLowSignificance" runat="server" Text=""></asp:Label>
    </div>
    <div>
    <div id="DivMorphologySignificance" runat="server">【病理形态】</div>
    <asp:Label ID="LabMorphologySignificance" runat="server" Text=""></asp:Label>
    </div>
    <div>
    <div id="DivOtherSignificance" runat="server">【其它】</div>
    <asp:Label ID="LabOtherSignificance" runat="server" Text=""></asp:Label>
    </div>
    <div>
    <div id="DivComment" runat="server">【注意事项】</div>
    <asp:Label ID="LabComment" runat="server" Text=""></asp:Label>
    </div>
    </form>
</body>
</html>
