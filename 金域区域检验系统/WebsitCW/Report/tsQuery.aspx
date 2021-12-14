<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.Report.tsQuery" Codebehind="tsQuery.aspx.cs" %>

<%@ Import Namespace="System.Xml" %>
<%@ Import Namespace="System.Text" %>
<%@ Import Namespace="System" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>tsQuery</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
</head>
<body leftmargin="0" topmargin="0" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table width="800" border="0">
        <tbody>
            <tr>
                <td align="center" colspan="4">
                    特殊检索
                </td>
            </tr>
            <tr>
                <td align="right" width="14%">
                    日期
                </td>
                <td width="19%">
                    <font face="宋体">
                        <asp:TextBox ID="sTime" runat="server" Width="125px"></asp:TextBox></font>
                </td>
                <td align="right" width="10%">
                    至
                </td>
                <td width="57%">
                    <font face="宋体">
                        <asp:TextBox ID="eTime" runat="server" Width="125px"></asp:TextBox></font>
                </td>
            </tr>
            <tr>
                <td align="right" width="14%">
                    检测项目
                </td>
                <td width="19%">
                    <asp:DropDownList ID="proList" runat="server" Width="125px">
                    </asp:DropDownList>
                </td>
                <td align="right" width="10%">
                    &nbsp;
                </td>
                <td width="57%">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right" width="14%">
                    结果
                </td>
                <td width="19%">
                    <asp:DropDownList ID="result" runat="server">
                        <asp:ListItem Value="正常">正常</asp:ListItem>
                        <asp:ListItem Value="异常">异常</asp:ListItem>
                        <asp:ListItem Value="其它">其它</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="right" width="10%">
                    &nbsp;
                </td>
                <td width="57%">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right" width="14%">
                    ISCN
                </td>
                <td width="19%">
                    <asp:TextBox ID="iscn" runat="server" Width="125px"></asp:TextBox>
                </td>
                <td align="right" width="10%">
                    限制
                </td>
                <td width="57%">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right" width="14%">
                    &nbsp;
                </td>
                <td width="19%">
                    &nbsp;
                </td>
                <td align="right" width="10%">
                    &nbsp;
                </td>
                <td width="57%">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right" width="14%">
                    结果分析
                </td>
                <td colspan="3">
                    按关键词检索（检索条件: 完全一样/含有/兼有）
                </td>
            </tr>
            <tr>
                <td align="right" width="14%">
                    &nbsp;
                </td>
                <td colspan="3">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right" width="14%">
                    &nbsp;
                </td>
                <td width="19%">
                    <asp:Button ID="okBtn" runat="server" Text="确 定" OnClick="okBtn_Click"></asp:Button>&nbsp;
                </td>
                <td align="right" width="10%">
                    &nbsp;
                </td>
                <td width="57%">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right" width="14%">
                    检索结果
                </td>
                <td width="19%">
                    &nbsp;
                </td>
                <td align="right" width="10%">
                    &nbsp;
                </td>
                <td width="57%">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <table cellspacing="0" cellpadding="0" width="100%" border="1">
                        <tbody>
                            <tr>
                                <td rowspan="2">
                                    样本号
                                </td>
                                <td rowspan="2">
                                    病人姓名
                                </td>
                                <td rowspan="2">
                                    出生日期
                                </td>
                                <td rowspan="2">
                                    样品接收日期
                                </td>
                                <td colspan="5">
                                    显示内容(可选)
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    检测项目
                                </td>
                                <td>
                                    检测结果
                                </td>
                                <td>
                                    ISCN
                                </td>
                                <td>
                                    报告内容
                                </td>
                                <td>
                                    其它
                                </td>
                            </tr>
                            <%
						  
                                if (xn != null)
                                {
                                    XmlNodeList xmlList = xn.ChildNodes;

                                    foreach (XmlNode xn1 in xmlList)
                                    {
                                        XmlNode selXmlNode = xn1.SelectSingleNode("td[@ Column='ydbgDate']");
                                        if (selXmlNode != null)
                                        {
                                            DateTime bgDate = Convert.ToDateTime(selXmlNode.InnerText);
                                            if (bgDate >= Convert.ToDateTime(sTime.Text) && bgDate <= Convert.ToDateTime(eTime.Text))
                                            {
														
                            %>
                            <tr>
                                <%
                                    XmlNode ybid = xn1.SelectSingleNode("td[@ Column='ybid']");
                                    string ybids = "";
                                    if (ybid != null)
                                    {
                                        ybids = ybid.InnerText.ToString().Trim();
                                    }
                                %>
                                <td>
                                    &nbsp;<%=ybids%>
                                </td>
                                <%
                                    XmlNode uname = xn1.SelectSingleNode("td[@ Column='uname']");
                                    string unames = "";
                                    if (uname != null)
                                    {
                                        unames = uname.InnerText.ToString().Trim();
                                    }
                                %>
                                <td>
                                    &nbsp;<%=unames%>
                                </td>
                                <%
                                    XmlNode brdate = xn1.SelectSingleNode("td[@ Column='brdate']");
                                    string brdates = "";
                                    if (brdate != null)
                                    {
                                        brdates = brdate.InnerText.ToString().Trim();
                                    }
                                %>
                                <td>
                                    &nbsp;<%=brdates%>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <%
                                }
                                    }

                                }
                            }
                            %>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right" width="14%">
                    &nbsp;
                </td>
                <td width="19%">
                    &nbsp;
                </td>
                <td align="right" width="10%">
                    &nbsp;
                </td>
                <td width="57%">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
        </tbody>
    </table>
    </form>
</body>
</html>
