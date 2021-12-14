<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TreeType.aspx.cs" Inherits="TreeItem.TreeUI.TreeType" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="../Css/mainstyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">              
        function $(s)
        {
            return document.getElementById?document.getElementById(s):document.all[s];
        }
        function check()
        {
			if(document.Form1.txtproname.value == null || document.Form1.txtproname.value == "")
			{
				alert('请输入名称!');
				document.Form1.txtproname.focus();
				return false;
			}   
            if(document.Form1.txtdes.value == null || document.Form1.txtdes.value == "")
		    {
		       alert('请输入描述!');
		       document.Form1.txtremark.focus();
		       return false;
		    }   
           return true;
        }
    </script>

</head>
<body>
    <form id="Form1" runat="server">
    <div id="Main">
        <div id="Wrap">
            <div class="Inner">
                <!--主要内容-->
                <div class="ToolBar">
                    <label>
                        名称:<asp:Literal ID="litid" runat="server" Visible="False"></asp:Literal>
                        <asp:TextBox ID="txtproname" runat="server" MaxLength="50"></asp:TextBox>                        
                        链接:<asp:TextBox ID="txturl" runat="server"></asp:TextBox>
                        图片:<asp:TextBox ID="txtpic" runat="server"></asp:TextBox>
                        描述:<asp:TextBox ID="txtdes" runat="server" MaxLength="100"></asp:TextBox>
                    </label>
                    <asp:Button ID="btnAdd" runat="server" Text="增 加" CssClass="buttom" 
                        onclick="btnAdd_Click"></asp:Button>
                    <asp:Button ID="btnUpdate" runat="server" Text="保 存" CssClass="buttom" 
                        Visible="False" onclick="btnUpdate_Click">
                    </asp:Button>
                    <asp:Button ID="btncancel" runat="server" Text="取 消" CssClass="buttom" 
                        Visible="False" onclick="btncancel_Click">
                    </asp:Button>
                </div>
                <asp:GridView ID="GridView1" Width="98%" GridLines="None" CellSpacing="1" CellPadding="0"
                    CssClass="DataTable" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand"
                    OnRowDataBound="GridView1_RowDataBound" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging"
                    OnSelectedIndexChanged="GridView1_SelectedIndexChanged" PageSize="20">
                    <HeaderStyle HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateField HeaderText="编号">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="labdid" Text='<%# DataBinder.Eval(Container.DataItem,"TreeNodeTypeID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="名称">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="labname" Text='<%# DataBinder.Eval(Container.DataItem,"TreeNodeTypeName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="链接">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="laburl" Text='<%# DataBinder.Eval(Container.DataItem,"url") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="图片">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="labpic" Text='<%# DataBinder.Eval(Container.DataItem,"pic") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="描述">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="labdes" Text='<%# DataBinder.Eval(Container.DataItem,"TreeNodeTypeDesc") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnupdate" runat="server" CausesValidation="False" CommandName="Up"
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem,"TreeNodeTypeID") %>'>编辑</asp:LinkButton>
                                <asp:LinkButton ID="lbtnDelete" runat="server" CausesValidation="False" CommandName="Del"
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem,"TreeNodeTypeID") %>'>删除</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
