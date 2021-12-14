<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchCaiWuExcel_DaJia.aspx.cs"
    Inherits="ZhiFang.WebLis.ExportExcel.SearchCaiWuExcel_DaJia" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../js/Tools.js"></script>
    <link href="../CSS/Default.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function getInfo(id) {
            this.bgColor = '#3F7885';
            var str = ZhiFang.WebLis.ExportExcel.SearchCaiWuExcel_DaJia.SearchData(id);
            if (str.value != "") {
                var vv = str.value.split(';');
                document.getElementById("cid").value = vv[0];
                document.getElementById("yid").value = vv[1];
                document.getElementById("clientName").value = vv[2];
                document.getElementById("Selcstatus").value = vv[3];
                document.getElementById("txtcremark").value = vv[4];
                document.getElementById("txtccreatedate").value = vv[5];
            }

        }
        function reset() {
            document.getElementById("AccountDate").value = "";
            document.getElementById("txtClient").value = "";
            document.getElementById("Scstatus").value = "";
        }
        function downExcel(cid, type) {
            document.getElementById("cidKey").value = cid;
            document.getElementById("ExcelType").value = type;
            var o = document.getElementById('linkDownLoadExcel');
            o.click();
        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 51%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <input type="hidden" id="cidKey" value="" runat="server" />
    <input type="hidden" id="ExcelType" value="" runat="server" />
    <asp:LinkButton ID="linkDownLoadExcel" Style="display: none" runat="server" OnClick="linkDownLoadExcel_Click">
    </asp:LinkButton>
    <div>
        <table style="width: 100%; height: 100%">
            <tr>
                <td align="center">
                    <div style="font-size: 15px; font-weight: bold; margin: 10px">
                        对账月:
                        <input id="AccountDate" name="AccountDate" type="text" style="width: 112px;" runat="server" />&nbsp;&nbsp;
                        客户名称:
                        <input id="txtClient" name="txtClient" type="text" style="width: 116px;" runat="server" />&nbsp;&nbsp;
                        确认状态:
                        <select id="Scstatus" runat="server" style="width: 119px">
                            <option></option>
                            <option>未确认</option>
                            <option>确认无误</option>
                            <option>确认有误</option>
                        </select>
                    </div>
                </td>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="butnSearch" runat="server" Text="查询" OnClick="butnSearch_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="button" value="重置" onclick="reset();" />
                </td>
            </tr>
            <tr>
                <td>
                    <div style="margin-top: 0px">
                        <asp:DataList ID="DataList1" runat="server" Width="100%" Height="100%" Style="margin-top: 0px">
                            <HeaderTemplate>
                                <table class="tablehead" width="100%">
                                    <tr>
                                        <td width="60" align="center">
                                            序号
                                        </td>
                                        <td width="80" align="center">
                                            对帐月
                                        </td>
                                        <td width="100" align="center">
                                            客户名称
                                        </td>
                                        <td width="100" align="center">
                                            确认状态
                                        </td>
                                        <td width="100" align="center">
                                            生成日期
                                        </td>
                                        <td width="60" align="center">
                                            下载
                                        </td>
                                        <td width="100" align="center">
                                            项目对账
                                        </td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table width="100%" bgcolor="#ffffff" cellpadding="0" cellspacing="0" border="0"
                                    onmousemove="this.bgColor='#3F7885';this.style.color='#ffffff'" onclick="javascript:getInfo('<%#Eval("cid") %>')"
                                    onmouseout="this.bgColor='#ffffff';this.style.color='#000000'">
                                    <tr class="tabletr">
                                        <td width="60">
                                            <%#Eval("cid")%>
                                        </td>
                                        <td width="80">
                                            <%#Eval("yname")%>
                                        </td>
                                        <td width="100">
                                            <%#Eval("cclientname")%>
                                        </td>
                                        <td width="100">
                                            <%#Eval("cstatus")%>
                                        </td>
                                        <td width="100">
                                            <%#Eval("ccreatedate")%>
                                        </td>
                                        <td width="60">
                                            <a style="cursor: pointer" onclick="javascript:downExcel('<%#Eval("cid") %>','0')">下载</a>
                                        </td>
                                        <td width="100">
                                            <a style="cursor: pointer" onclick="javascript:downExcel('<%#Eval("cid") %>','1')">项目对账</a>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <table width="100%" bgcolor="#BFDBE1" cellpadding="0" cellspacing="0" border="0"
                                    onmousemove="this.bgColor='#3F7885';this.style.color='#ffffff'" onclick="javascript:getInfo('<%#Eval("cid") %>')"
                                    onmouseout="this.bgColor='#BFDBE1';this.style.color='#000000'">
                                    <tr class="tabletr">
                                        <td width="60">
                                            <%#Eval("cid")%>
                                        </td>
                                        <td width="80">
                                            <%#Eval("yname")%>
                                        </td>
                                        <td width="100">
                                            <%#Eval("cclientname")%>
                                        </td>
                                        <td width="100">
                                            <%#Eval("cstatus")%>
                                        </td>
                                        <td width="100">
                                            <%#Eval("ccreatedate")%>
                                        </td>
                                        <td width="60">
                                            <a style="cursor: pointer" onclick="javascript:downExcel('<%#Eval("cid") %>','0')">下载</a>
                                        </td>
                                        <td width="100">
                                            <a style="cursor: pointer" onclick="javascript:downExcel('<%#Eval("cid") %>','1')">项目对账</a>
                                        </td>
                                    </tr>
                                </table>
                            </AlternatingItemTemplate>
                        </asp:DataList>
                    </div>
                </td>
                <td>
                    <div>
                        <table>
                            <tr>
                                <td align="right">
                                    序号:
                                </td>
                                <td class="style1">
                                    <input id="cid" disabled type="text" runat="server" style="width: 190px; margin-left: 0px;" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    对账月:
                                </td>
                                <td class="style1">
                                    <input id="yid" disabled type="text" runat="server" style="width: 190px; margin-left: 0px;" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    客户名称:
                                </td>
                                <td class="style1">
                                    <input id="clientName" disabled type="text" runat="server" style="width: 190px; margin-left: 0px;" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    确认状态:
                                </td>
                                <td class="style1">
                                    <select id="Selcstatus" disabled runat="server" style="width: 185px">
                                        <option>未确认</option>
                                        <option>确认无误</option>
                                        <option>确认有误</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    备注:
                                </td>
                                <td class="style1">
                                    <input id="txtcremark" disabled type="text" runat="server" style="width: 186px; margin-left: 0px;" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    生成日期:
                                </td>
                                <td class="style1">
                                    <input id="txtccreatedate" disabled type="text" runat="server" style="width: 187px;
                                        margin-left: 0px;" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
