<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EquipsInfo.aspx.cs" Inherits="OA.QMSEquips.Config.EquipsInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>设备信息维护</title>

    <script language="javascript" type="text/javascript">
    function AddNewType(protectClass)
    {
        var urlAddType = "EquipsMait.aspx?addClass=type&protectClass="+protectClass;
        window.open(urlAddType,"addType","width=400,height=200,toolbar=no,menubar=no,scrollbars=yes,resizable=no,location=no,status=no,top="+Math.floor(window.screen.height*0.2+80)+",left="+Math.floor(window.screen.width*0.2));
    }
    function AddNewEquip()
    {
        var EquipType = document.getElementById("hiddenCurEquipType");
        if(EquipType.value == '')
        {
            alert("请先选择仪器类型");
            return;
        }
        var urlAddType = "EquipsMait.aspx?addClass=Equip&EquipType="+EquipType.value;
        window.open(urlAddType, "addType", "width=400,height=200,toolbar=no,menubar=no,scrollbars=yes,resizable=no,location=no,status=no,top=" + Math.floor(window.screen.height * 0.2 + 80) + ",left=" + Math.floor(window.screen.width * 0.2));
    }
    function addRecordProject()
    {
        var EquipType = document.getElementById("hiddenCurEquipType");
        if(EquipType.value == '')
        {
            alert("请先选择仪器类型");
            return;
        }
        var urlAddType = "EquipsMait.aspx?addClass=RecordProject&EquipType="+EquipType.value;
        window.open(urlAddType, "addType", "width=400,height=200,toolbar=no,menubar=no,scrollbars=yes,resizable=no,location=no,status=no,top=" + Math.floor(window.screen.height * 0.2 + 80) + ",left=" + Math.floor(window.screen.width * 0.2));
    }
    function showDetailOperate()
    {
        var div = document.getElementById("detailOperate");
        div.style.display = "";
        return;
    }
    function modifyForm(str)
    {
        var urlAddType = "EquipsMait.aspx?addClass=Form&proID="+str;
        window.open(urlAddType, "addForm", "width=400,height=300,toolbar=auto,menubar=no,scrollbars=yes,resizable=no,location=no,status=no,top=" + Math.floor(window.screen.height * 0.2 + 80) + ",left=" + Math.floor(window.screen.width * 0.2));
    }
    function delProgect(proId,proName)
    {
        var urlAddType = "EquipsMait.aspx?addClass=delete&proID="+proId+"&proName="+proName;
        window.open(urlAddType, "addForm", "width=220,height=120,toolbar=auto,menubar=no,scrollbars=yes,resizable=no,location=no,status=no,top=" + Math.floor(window.screen.height * 0.2 + 80) + ",left=" + Math.floor(window.screen.width * 0.2));
    }
    function getSelectValue(str)
    {
        var drop = document.getElementById(str);
        if(drop)
        {
            var va = drop.children;
            for(var i=0;i<va.length;i++)
            {            
                if(va[i].selected)
                {
                    var hiddenSelectValue = document.getElementById("hiddenSelectValue");
                    hiddenSelectValue.value = va[i].value;
                    return;
                }
            }
            hiddenSelectValue.value = "";
        }
    }
    function shouHelpDocument(newsID)
    {
        var url = "../../News/Browse/homepage.aspx?id="+newsID;
        window.open(url,"helpwindow",'top=0,left=0,toolbar=no,menubar=no,scrollbars=yes,resizable=yes,location=no,status=no')
    }
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" EnablePartialRendering="true" />
    </div>
    <table border="0">
        <tr>
            <td align="left">
                <font size="2"><b><asp:Label ID="LabelClassTitle" runat="server"></asp:Label>
                    <input id="btnAddNewType" type="button" onclick="AddNewType('<%= Request.QueryString["protectClass"]%>');" value="添加新类别" /></b></font>
                <input type="image" name="btnShowHelp" id="btnShowHelp" onclick="shouHelpDocument('<%= Request.QueryString["helpID"]%>');" src="../../App_Themes/zh-cn/Images/STAT/help.gif" alt="显示帮助" style="border-width: 0px; cursor: pointer;" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvEquipTypeInfo" Width="750px" Font-Size="Small" AutoGenerateColumns="false" runat="server" OnRowCreated="gvEquipTypeInfo_RowCreated" OnRowCommand="gvEquipTypeInfo_RowCommand" onrowdatabound="gvEquipTypeInfo_RowDataBound">
                    <AlternatingRowStyle BackColor="Gainsboro" />
                    <SelectedRowStyle BackColor="#FF9933" />
                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Height="15px" ForeColor="White" BackColor="#000084"></HeaderStyle>
                    <Columns>
                        <asp:TemplateField ItemStyle-Wrap="false">
                            <HeaderStyle Wrap="false" Width="200px" />
                            <ItemTemplate>
                                <asp:Label ID="typeEName" Text='<%# DataBinder.Eval(Container,"DataItem.equipType")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Wrap="false">
                            <HeaderStyle Wrap="false" Width="200px" />
                            <ItemTemplate>
                                <asp:TextBox ID="typeCName" Width="180px" Text='<%# DataBinder.Eval(Container,"DataItem.typeName")%>' runat="server"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle Wrap="False"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="维护大类" ItemStyle-Wrap="false">
                            <HeaderStyle Wrap="false" Width="200px" />
                            <ItemTemplate>
                                <asp:Label ID="labelProtectClass" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.protectClass")%>' runat="server"></asp:Label>
                                <asp:DropDownList ID="dropProtectClass" Width="180" runat="server"></asp:DropDownList>
                            </ItemTemplate>
                            <ItemStyle Wrap="False"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="保存" ItemStyle-Wrap="false">
                            <HeaderStyle Wrap="false" Width="50px" />
                            <ItemTemplate>
                                <font size="2">
                                    <asp:Button ID="saveModify" Text="保存" Height="22px" CommandArgument="saveModify" runat="server" />
                                </font>
                            </ItemTemplate>
                            <ItemStyle Wrap="False"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="删除" ItemStyle-Wrap="false">
                            <HeaderStyle Wrap="false" Width="50px" />
                            <ItemTemplate>
                                <font size="2">
                                    <asp:Button ID="delType" Text="删除" Height="22px" CommandArgument="delType" runat="server" />
                                </font>
                            </ItemTemplate>
                            <ItemStyle Wrap="False"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="维护" ItemStyle-Wrap="false">
                            <HeaderStyle Wrap="false" Width="50px" />
                            <ItemTemplate>
                                <font size="2">
                                    <asp:Button ID="weihuEquip" Text="维护" Height="22px" CommandArgument="weihuEquip" runat="server" />
                                </font>
                            </ItemTemplate>
                            <ItemStyle Wrap="False"></ItemStyle>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <asp:Panel ID="detailOperate" runat="server" Visible="false">
        <table border="0">
            <tr>
                <td align="left">
                    <font size="2"><b><asp:Label ID="LabelDetailTitle" runat="server"></asp:Label>
                        <input id="Button1" type="button" onclick="AddNewEquip();" value="添加新仪器" /></b></font>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvEquipsInfo" Width="750px" Font-Size="Small" AutoGenerateColumns="false" runat="server" OnRowCommand="gvEquipsInfo_RowCommand" OnRowCreated="gvEquipsInfo_RowCreated">
                        <AlternatingRowStyle BackColor="Gainsboro" />
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Height="15px" ForeColor="White" BackColor="#000084"></HeaderStyle>
                        <Columns>
                            <asp:TemplateField ItemStyle-Wrap="false">
                                <HeaderStyle Wrap="false" Width="200px" />
                                <ItemTemplate>
                                    <asp:Label ID="equipID" Text='<%# DataBinder.Eval(Container,"DataItem.equipID")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Wrap="false">
                                <HeaderStyle Wrap="false" Width="450px" />
                                <ItemTemplate>
                                    <asp:TextBox ID="equipName" Width="180px" Text='<%# DataBinder.Eval(Container,"DataItem.equipName")%>' runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="保存" ItemStyle-Wrap="false">
                                <HeaderStyle Wrap="false" Width="50px" />
                                <ItemTemplate>
                                    <font size="2">
                                        <asp:Button ID="saveEquip" Text="保存" Height="22px" CommandArgument="saveEquip" runat="server" />
                                    </font>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="删除" ItemStyle-Wrap="false">
                                <HeaderStyle Wrap="false" Width="50px" />
                                <ItemTemplate>
                                    <font size="2">
                                        <asp:Button ID="delEquip" Text="删除" Height="22px" CommandArgument="delEquip" runat="server" />
                                    </font>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <table border="0">
            <tr>
                <td align="left">
                    <font size="2"><b>当前类别项目维护
                        <input id="addNewRecordProject" type="button" onclick="addRecordProject();" value="添加新项目" /></b></font>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvRecordProject" Width="750px" Font-Size="Small" AutoGenerateColumns="false" runat="server" OnRowCommand="gvRecordProject_RowCommand" OnRowCreated="gvRecordProject_RowCreated" OnRowDataBound="gvRecordProject_RowDataBound">
                        <AlternatingRowStyle BackColor="Gainsboro" />
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Height="15px" ForeColor="White" BackColor="#000084"></HeaderStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="项目名称" ItemStyle-Wrap="false">
                                <HeaderStyle Wrap="false" Width="300px" />
                                <ItemTemplate>
                                    <asp:TextBox ID="recordProject" Text='<%# DataBinder.Eval(Container,"DataItem.recordProject")%>' runat="server"></asp:TextBox>
                                    <asp:Label ID="equipProjectID" Text='<%# DataBinder.Eval(Container,"DataItem.equipProjectID")%>' Visible="false" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="输入类型" ItemStyle-Wrap="false">
                                <HeaderStyle Wrap="false" Width="150px" />
                                <ItemTemplate>
                                    <asp:DropDownList ID="recordType" runat="server">
                                        <asp:ListItem Value="checkbox" Selected="True">复选框</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="使用表格" ItemStyle-Wrap="false">
                                <HeaderStyle Wrap="false" Width="200px" />
                                <ItemTemplate>
                                    <asp:DropDownList ID="forTable" runat="server">
                                        <asp:ListItem Value="0" Selected="True">全部维护表格</asp:ListItem>
                                        <asp:ListItem Value="1">日维护表格</asp:ListItem>
                                        <asp:ListItem Value="2">周维护表格</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="保存" ItemStyle-Wrap="false">
                                <HeaderStyle Wrap="false" Width="50px" />
                                <ItemTemplate>
                                    <font size="2">
                                        <asp:Button ID="saveRecordProject" Text="保存" Height="22px" CommandArgument="saveRecordProject" runat="server" />
                                    </font>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="删除" ItemStyle-Wrap="false">
                                <HeaderStyle Wrap="false" Width="50px" />
                                <ItemTemplate>
                                    <font size="2">
                                        <asp:Button ID="delRecordProject" Text="删除" Height="22px" CommandArgument="delRecordProject" runat="server" />
                                    </font>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
        </table>
        <table id="tableRecordProject" border="1" cellspacing="0" width="550px" style="font-size: small; font-weight: bold;" runat="server">
            <tr align="center" style="background-color: #000084; color: White;">
                <td style="width: 200px;">
                    项目名称
                </td>
                <td style="width: 50px;">
                    设置
                </td>
                <td style="width: 100px;">
                    应用类型
                </td>
                <td style="width: 100px;">
                    录入方式
                </td>
                <td style="width: 100px;">
                    删除项目
                </td>
            </tr>
        </table>
    </asp:Panel>
    <input type="hidden" id="hiddenCurEquipType" runat="server" /><!-- 存储当前仪器类别 -->
    <input type="hidden" id="hiddenSelectValue" runat="server" />
    </form>
</body>
</html>
