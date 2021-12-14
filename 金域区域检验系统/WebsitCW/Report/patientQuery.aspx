<%@ Import Namespace="System" %>
<%@ Import Namespace="System.Text" %>
<%@ Import Namespace="System.Xml" %>

<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.Report.patientQuery" Codebehind="patientQuery.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>patientQuery</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table width="100%" border="0">
        <tbody>
            <tr>
                <td align="center" colspan="4">
                    ������Ϣ����
                </td>
            </tr>
            <tr>
                <td align="right" width="14%">
                    ������
                </td>
                <td width="21%">
                    <font face="����">
                        <asp:TextBox ID="ybids" runat="server"></asp:TextBox></font>
                </td>
                <td align="right" width="14%" colspan="2">
                </td>
            </tr>
            <tr>
                <td align="right" width="14%">
                    ����
                </td>
                <td width="21%">
                    <font face="����">
                        <asp:TextBox ID="uNames" runat="server"></asp:TextBox></font>
                </td>
                <td align="right" width="14%" colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right" width="14%">
                    �Ա�
                </td>
                <td width="21%">
                    <font face="����">
                        <asp:DropDownList ID="uSexs" runat="server">
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="��">��</asp:ListItem>
                            <asp:ListItem Value="Ů">Ů</asp:ListItem>
                        </asp:DropDownList>
                    </font>
                </td>
                <td align="right" width="14%" colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right" width="14%">
                    ��������
                </td>
                <td width="21%">
                    <font face="����"></font>
                </td>
                <td align="right" width="14%" colspan="2">
                </td>
            </tr>
            <tr>
                <td align="right" width="14%">
                    סַ
                </td>
                <td width="21%">
                    <font face="����"></font>
                </td>
                <td align="right" width="14%" colspan="2">
                </td>
            </tr>
            <tr>
                <td align="right" width="14%">
                    �ʱ�&nbsp;
                </td>
                <td width="21%">
                    <font face="����"></font>
                </td>
                <td align="right" width="14%" colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right" width="14%">
                    ��ϵ��ʽ&nbsp;
                </td>
                <td width="21%">
                    <font face="����"></font>
                </td>
                <td align="right" width="14%" colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right" width="14%" rowspan="3">
                    �ͼ쵥λ&nbsp;
                </td>
                <td colspan="3" rowspan="3">
                    <font face="����"></font>
                </td>
            </tr>
            <tr>
            </tr>
            <tr>
            </tr>
            <tr>
                <td align="right" width="14%">
                    &nbsp;
                </td>
                <td align="right" width="21%" colspan="3">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right" width="14%">
                    &nbsp;
                </td>
                <td width="21%">
                </td>
                <td align="right" width="14%">
                    &nbsp;
                </td>
                <td width="51%">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right">
                    &nbsp;�ٴ����
                </td>
                <td>
                    <font face="����"></font>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right">
                    �����Ŀ
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
                <td align="right">
                    �������
                </td>
                <td>
                    <font face="����"></font>
                </td>
                <td>
                    &nbsp;
                    <asp:Button ID="okBtn" runat="server" Text="�� ��" OnClick="okBtn_Click"></asp:Button>
                </td>
                <td>
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
                    �������
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <table height="100%" cellspacing="1" cellpadding="1" width="100%" bgcolor="#000000"
                        border="0">
                        <tbody>
                            <tr bgcolor="#ffffff" width>
                                <td align="center">
                                    ������
                                </td>
                                <td align="center">
                                    &nbsp;����
                                </td>
                                <td align="center">
                                    &nbsp;�Ա�
                                </td>
                                <td align="center">
                                    &nbsp;��������
                                </td>
                                <td align="center">
                                    &nbsp;������������
                                </td>
                                <td align="center">
                                    &nbsp;�����Ŀ
                                </td>
                            </tr>
                            <%
						  
                                if (xn != null)
                                {
                                    XmlNodeList xmlList = xn.ChildNodes;

                                    foreach (XmlNode xn1 in xmlList)
                                    {
                                        XmlNode selXmlNode = xn1.SelectSingleNode("td[@ Column='ybid']");
                                        XmlNode uNameXmlNode = xn1.SelectSingleNode("td[@ Column='uname']");
                                        XmlNode SEX = xn1.SelectSingleNode("td[@ Column='sex']");

                                        if (selXmlNode != null && SEX != null)
                                        {
                                            string ybid = selXmlNode.InnerText.ToString().Trim();
                                            string username = uNameXmlNode.InnerText.ToString().Trim();
                                            string Sexs = "";
                                            if (uSexs.SelectedItem.ToString().Trim() != "")
                                            {
                                                Sexs = SEX.InnerText.ToString().Trim();
                                            }
                                            if (ybid == ybids.Text.ToString().Trim() || username == uNames.Text.ToString().Trim() || Sexs == uSexs.SelectedItem.ToString().Trim())
                                            {
									     
														
                            %>
                            <tr bgcolor="#ffffff">
                                <td>
                                    &nbsp;<%=ybid%>
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
                                    XmlNode sss = xn1.SelectSingleNode("td[@ Column='sex']");
                                    string sexxs = "";
                                    if (sss != null)
                                    {
                                        sexxs = sss.InnerText.ToString().Trim();
                                    }
                                %>
                                <td>
                                    &nbsp;<%=sexxs%>
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
                                <%
                                    XmlNode bbjzDate = xn1.SelectSingleNode("td[@ Column='bbjzDate']");
                                    string bbjzDates = "";
                                    if (bbjzDate != null)
                                    {
                                        bbjzDates = bbjzDate.InnerText.ToString().Trim();
                                    }
                                %>
                                <td>
                                    &nbsp;<%=bbjzDates%>
                                </td>
                                <%
                                    XmlNode EName41 = xn1.SelectSingleNode("td[@ Column='EName41']");
                                    string EName41s = "";
                                    if (EName41 != null)
                                    {
                                        EName41s = EName41.InnerText.ToString().Trim();
                                    }
                                %>
                                <td>
                                    &nbsp;<%=EName41s%>
                                </td>
                            </tr>
                            <%
                                }
                                    }
                                }
                            }
                            %>
                            <tr bgcolor="#ffffff">
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
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right" colspan="4">
                    <a href="http://www.digitlab.com.cn/cqpcr/RBAC/MODULES/ModuleRun.aspx?ModuleID=487">
                        ����</a>
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
