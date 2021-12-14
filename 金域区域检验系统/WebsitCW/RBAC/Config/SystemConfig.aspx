<%@ Page Language="c#" AutoEventWireup="True" Inherits="theNews.Config.SystemConfig" Codebehind="SystemConfig.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>SystemConfig</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <link href="../main/css.css" type="text/css" rel="stylesheet">
</head>
<body style="font-size: 12pt">
    <form id="Form1" name="Form1" method="post" runat="server">
    &nbsp;
    <table id="Table1" style="width: 531px" cellspacing="0" cellpadding="0" width="531"
        align="center" border="0">
        <tbody>
            <tr>
                <td valign="top" colspan="3">
                    <table id="Table6" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tbody>
                            <tr>
                                <td width="33">
                                    <img height="25" src="images/table_top_left.gif" width="33">
                                </td>
                                <td background="images/table_top_bg.gif">
                                    <span class="black_con11_bold" id="Label1">当前位置: 系统管理</span>
                                </td>
                                <td align="right" width="33">
                                    <img height="25" src="images/table_top_right.gif" width="33">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <td width="1" bgcolor="#437fcc">
                </td>
                <td style="width: 529px" valign="middle">
                    <table id="Table7" cellspacing="1" cellpadding="1" width="100%" align="center" bgcolor="#cccccc"
                        border="0">
                        <tr>
                            <td class="black_con9" style="width: 107px" align="right" width="107" bgcolor="#ffffff">
                                栏目名称：
                            </td>
                            <td nowrap bgcolor="#ffffff">
                                <input class="TextBox" id="TextBoxModuleName" style="width: 26.91%; height: 22px"
                                    size="20" name="TextBoxModuleName" runat="server">&nbsp;
                                <asp:CheckBox ID="CheckBox1" runat="server" Width="8px"></asp:CheckBox>所属
                                <asp:DropDownList ID="DDLColumns" runat="server" Width="96px" AutoPostBack="True"
                                    OnSelectedIndexChanged="DDLColumns_SelectedIndexChanged">
                                </asp:DropDownList>
                                &nbsp;
                                <asp:Button ID="buttDelete" runat="server" Text="删除" OnClick="buttDelete_Click">
                                </asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td class="black_con9" style="width: 107px; height: 26px" align="right" width="107"
                                bgcolor="#ffffff">
                                访问地址：
                            </td>
                            <td style="height: 26px" bgcolor="#ffffff">
                                <input class="TextBox" id="TextBoxObjectAddress" style="width: 99.66%; height: 22px"
                                    size="63" name="TextBoxObjectAddress" runat="server">
                            </td>
                        </tr>
                        <tr>
                            <td class="black_con9" style="width: 107px" align="right" width="107" bgcolor="#ffffff">
                                排序序号：
                            </td>
                            <td bgcolor="#ffffff">
                                <input class="TextBox" id="TextBoxSort" style="width: 26.4%; height: 22px" maxlength="10"
                                    size="12" name="TextBoxSort" runat="server" autocomplete="on">
                            </td>
                        </tr>
                        <tr>
                            <td class="black_con9" style="width: 107px" valign="top" nowrap align="right" width="107"
                                bgcolor="#ffffff">
                                栏目说明：
                            </td>
                            <td bgcolor="#ffffff">
                                <textarea class="TextBox" id="TextBoxDescription" style="width: 99.75%; height: 81px"
                                    name="TextBoxDescription" rows="5" cols="49" runat="server"></textarea>
                            </td>
                        </tr>
                    </table>
                    &nbsp;
                    <asp:Button ID="buttAdd" runat="server" Text="增加" OnClick="buttAdd_Click"></asp:Button>&nbsp;
                    <asp:Button ID="buttModify" runat="server" Text="修改"></asp:Button>&nbsp;
                </td>
                <td style="height: 205px" width="6" background="images/table_right_bg.gif">
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table id="Table3" cellspacing="0" cellpadding="0" width="100%" background="images/table_bottom_bg.gif"
                        border="0">
                        <tbody>
                            <tr>
                                <td width="35">
                                    <img height="14" src="images/table_bottom_left.gif" width="24">
                                </td>
                                <td>
                                </td>
                                <td align="right">
                                    <img height="14" src="images/table_bottom_right.gif" width="24">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                </td>
            </tr>
        </tbody>
    </table>
    <%
        if (Page.IsPostBack)
        {
    %>

    <script language="javascript">
				window.open("../../main/left.aspx","leftFrame");
    </script>

    <%}%>
    <table id="Table2" cellspacing="1" cellpadding="1" width="300" align="center" border="0">
        <tr>
            <td>
                <asp:DataGrid ID="DataGrid1" runat="server" Width="602px" ShowHeader="False" Font-Size="10pt"
                    Font-Names="宋体" HorizontalAlign="Left" OnSelectedIndexChanged="DataGrid1_SelectedIndexChanged">
                    <Columns>
                        <asp:ButtonColumn Text="选择" CommandName="Select">
                            <HeaderStyle Width="44px"></HeaderStyle>
                        </asp:ButtonColumn>
                        <asp:ButtonColumn Text="删除" CommandName="Delete">
                            <HeaderStyle Width="44px"></HeaderStyle>
                        </asp:ButtonColumn>
                    </Columns>
                </asp:DataGrid>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
