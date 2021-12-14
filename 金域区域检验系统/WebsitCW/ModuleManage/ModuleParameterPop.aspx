<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModuleParameterPop.aspx.cs"
    Inherits="OA.ModuleManage.ModuleParameterPop" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">    
    <title>输入参数编辑功能</title>
    <link href="style.css" rel="stylesheet" />
    <base target="_self" />
    <script type="text/javascript">	
       
       function setParent(empID,empName)
       {
            var pa = window.opener;
            //window.dialogArguments;
            if(empID != null && empName != null)
            {
                if(pa)
                {
                    pa.setValue('0',empID,empName);
                    window.close();
                    //alert(empID + ',' + empName);
                } 
            }  
        }
        function openchild(subWindow,subWidth,subHeight)
		{
			var receiver = window.showModalDialog(subWindow,window,"dialogWidth=" + subWidth + "px;dialogHeight=" + subHeight + "px;help:no;status:no");
			if(receiver == "0")
			{
			    var mid = $('labmid').value;
				location.href = "ModuleParameterPop.aspx?mid="+mid;							
			}
			else
			{
				//alert('没有接收到父窗体的值');
			}
		}
        function $(s)
        {
            return document.getElementById?document.getElementById(s):document.all[s];
        }
         function SelectAll(spanChk)
        {
　        var oItem = spanChk.children;
　        var theBox=(spanChk.type=="checkbox")?spanChk:spanChk.children.item[0];
　        xState=theBox.checked;
　        elm=theBox.form.elements;
　        for(i=0;i<elm.length;i++)
　        if(elm[i].type=="checkbox" && elm[i].id!=theBox.id)
　        {
　　        if(elm[i].checked!=xState)
　　        elm[i].click();
　        }
        }
    </script>
     <style type="text/css">
        .hideClass
        {
            display: none;
        }
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table bordercolor="#003366" height="100%" cellspacing="0" bordercolordark="#ffffff"
            cellpadding="1" width="98%" align="center" bgcolor="#f6f6f6" bordercolorlight="#aecdd5"
            border="1">
            <tbody>
                <tr>
                    <td valign="top">
                        <table bordercolor="#003366" cellspacing="0" bordercolordark="#ffffff" cellpadding="10"
                            width="100%" align="center" bgcolor="#fcfcfc" bordercolorlight="#aecdd5" border="1">
                            <tbody>
                                 <tr>
                                     <td colspan="2">
                                        <asp:Button runat="server" ID="btnadd" CssClass="buttonstyle" Text="添 加" 
                                             onclick="btnadd_Click" />
                                        <asp:TextBox runat="server" CssClass="hideClass" ID="labmid"></asp:TextBox>
                                      
                                     </td>
                                 </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:DataGrid ID="myDataGrid" runat="server" PageSize="20" Font-Size="Smaller" BorderWidth="1px"
                                            CellPadding="4" BorderStyle="None" BackColor="White" AutoGenerateColumns="False"
                                            BorderColor="#A7C4F7" Width="100%" AllowPaging="false" 
                                            onitemcommand="myDataGrid_ItemCommand" 
                                            onitemdatabound="myDataGrid_ItemDataBound">
                                            <SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
                                            <ItemStyle ForeColor="#330099" BackColor="White"></ItemStyle>
                                            <HeaderStyle Font-Size="Smaller" Font-Bold="True" ForeColor="Black" BackColor="#990000">
                                            </HeaderStyle>
                                            <FooterStyle ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
                                            <Columns>
                                                <asp:TemplateColumn HeaderText="品名">
														<HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
														<ItemStyle HorizontalAlign="Center"></ItemStyle>
														<HeaderTemplate>
															<input id="CheckAll" type="checkbox" onclick="SelectAll(this);" />
														</HeaderTemplate>
														<ItemTemplate>
															<asp:CheckBox ID="CheckBox1" runat="server" />															
														</ItemTemplate>
													</asp:TemplateColumn>
                                                <asp:BoundColumn DataField="pamid" SortExpression="编号" Visible="False" ReadOnly="true"
                                                    HeaderText="编号">
                                                    <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundColumn>
                                              
                                                <asp:TemplateColumn SortExpression="名称" HeaderText="名称">
                                                    <HeaderStyle HorizontalAlign="Center" Width="20%" BackColor="Silver"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="dgtxtname" Width="60" Text='<%# DataBinder.Eval(Container, "DataItem.pamname") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn ItemStyle-Width="60" HeaderText="缩写">
                                                    <HeaderStyle HorizontalAlign="Center" Width="10%" BackColor="Silver"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="dgtxtspell" Width="50" Text='<%# DataBinder.Eval(Container, "DataItem.pamspell") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="" HeaderText="默认">
                                                    <HeaderStyle HorizontalAlign="Center" Width="20%" BackColor="Silver"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    <ItemTemplate>                                                       
                                                        <asp:TextBox runat="server" ID="dgtxtselect" Text='<%# DataBinder.Eval(Container, "DataItem.pamselect") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>                                              
                                                <asp:TemplateColumn SortExpression="描述" HeaderText="描述">
                                                    <HeaderStyle HorizontalAlign="Center" Width="30%" BackColor="Silver"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.pamremark") %>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="操作">
                                                    <HeaderStyle HorizontalAlign="Center" Width="10%" BackColor="Silver"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnupdate" runat="server" CommandName="Update">修改</asp:LinkButton>&nbsp;
                                                        <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete">删除</asp:LinkButton>
                                                        <asp:Label ID="labid" runat="server" Width="50" Text='<%# DataBinder.Eval(Container, "DataItem.pamid") %>'
                                                            Visible="False">
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                                        </asp:DataGrid>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:Button ID="btnSave" runat="server" CssClass="buttonstyle" Text="选  择" 
                                            onclick="btnSave_Click"></asp:Button>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
