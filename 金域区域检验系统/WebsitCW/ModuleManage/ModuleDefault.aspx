<%@ Page Language="c#" CodeBehind="ModuleDefault.aspx.cs" AutoEventWireup="True"
    Inherits="OA.ModuleManage.ModuleDefault" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>ModuleDefault</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="style.css" rel="stylesheet">

    <script type="text/javascript">
        function setValue(tag,id,name)
        {
            //tag 
            if(id != null && name != null)
            {
                if(tag == 0)
                {
                    $('txtdes').value = name;
                    $('txtvalue').value = id;              
                }
            }
        }
        
        function showUserDialog(tag)
        {
            var strurl = $('txtpopurl').value;
            if(strurl.length > 5)
            {
               strurl = strurl + "?moreurl="+escape($('txtmore').value);
               //var url = strurl.substring(0,strurl.length-5)+"_Pop.aspx";
               window.showModalDialog(strurl,this,'dialogWidth:500px;dialogHeight:500px;');
            }
            else
            {
               alert("请输入正确的弹出链接地址!");
               document.Form1.txtpopurl.focus();
            }
        }
         function openchild(subWindow,subWidth,subHeight)
		{
			var receiver = window.showModalDialog(subWindow,window,"dialogWidth=" + subWidth + "px;dialogHeight=" + subHeight + "px;help:no;status:no");
			if(receiver == "0")
			{
				location.href = "ModuleDefault.aspx";							
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
        function test()
        {
            var a = '我是中国人,我是沁阳的';
            var reg = new RegExp("(我是)","g");
            var newstr = a.replace(reg,"你好");
           //var newstr=a.replace(reg,"<font color=red>$1</font>");
            alert(newstr);
        }
      //鼠标放上显示数据
      function showdivByCs(value1)
      {
        var x = window.event.x;
        var y = window.event.y;
        var show = document.getElementById("ShowInfo");
        show.style.visibility = "visible";
        show.style.display="";
        show.style.top = y;
        show.style.left = x;        
        show.innerHTML = value1;
      }
       //不显示div
        function RemoveDiv()
        {
            var show = document.getElementById("ShowInfo");
            show.innerHTML = "";
            show.style.display = "none";
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
    <form id="Form1" method="post" runat="server">
    <div id="ShowInfo" style="border-right: #ffffcc 1px double; border-top: #ffffcc 1px double;
        display: none; font-size: 12pt; border-left: #ffffcc 1px double; color: #0000ff;
        border-bottom: #ffffcc 1px double; position: absolute; background-color: #00ffff">
    </div>
    <font face="宋体">
        <table bordercolor="#003366" height="100%" cellspacing="0" bordercolordark="#ffffff"
            cellpadding="1" width="100%" align="center" bgcolor="#f6f6f6" bordercolorlight="#aecdd5"
            border="1">
            <tbody>
                <tr>
                    <td>
                        <table bordercolor="#003366" height="100%" cellspacing="0" bordercolordark="#ffffff"
                            cellpadding="10" width="100%" align="center" bgcolor="#fcfcfc" bordercolorlight="#aecdd5"
                            border="1">
                            <tbody>
                                <tr>
                                    <td colspan="2" valign="top">
                                        <input type="button" class="buttonstyle" id="btnaddrecord" onclick="openchild('moduledefaultadd.aspx',600,650);"
                                            value="添加模块" />
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td align="center" colspan="2" height="100%">
                                        <asp:DataGrid ID="myDataGrid" runat="server" PageSize="20" Font-Size="Smaller" BorderWidth="1px"
                                            CellPadding="4" BorderStyle="None" BackColor="White" AutoGenerateColumns="False"
                                            BorderColor="#A7C4F7" Width="100%" AllowPaging="True" OnSelectedIndexChanged="myDataGrid_SelectedIndexChanged">
                                            <SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
                                            <ItemStyle ForeColor="#330099" BackColor="White"></ItemStyle>
                                            <HeaderStyle Font-Size="Smaller" Font-Bold="True" ForeColor="Black" BackColor="#990000">
                                            </HeaderStyle>
                                            <FooterStyle ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
                                            <Columns>
                                                <asp:TemplateColumn HeaderText="操作">
                                                    <HeaderStyle HorizontalAlign="Center" Width="10%" BackColor="Silver" Wrap="False">
                                                    </HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="False" BackColor="#D1CEFF" Font-Bold="True">
                                                    </ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnupdate" runat="server" CommandName="Update">修改</asp:LinkButton>&nbsp;
                                                        <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete">删除</asp:LinkButton>
                                                        <asp:Label ID="labid" runat="server" Width="50" Text='<%# DataBinder.Eval(Container, "DataItem.id") %>'
                                                            Visible="False">
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="id" SortExpression="编号" Visible="False" ReadOnly="true"
                                                    HeaderText="编号">
                                                    <HeaderStyle HorizontalAlign="Center" BackColor="Silver" Wrap="False"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn SortExpression="模块编号" HeaderText="模块编号">
                                                    <HeaderStyle HorizontalAlign="Center" Width="10%" BackColor="Silver"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="dglabnum" Width="60" Text='<%# DataBinder.Eval(Container, "DataItem.num") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="模块名称" HeaderText="模块名称">
                                                    <HeaderStyle HorizontalAlign="Center" Width="12%" BackColor="Silver"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="dgtxtname" Width="90px" Text='<%# DataBinder.Eval(Container, "DataItem.name") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="模块地址">
                                                    <HeaderStyle HorizontalAlign="Center" Width="8%" BackColor="Silver"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>                                                        
                                                        <asp:Label runat="server" ID="dgtxturl" ToolTip='<%# DataBinder.Eval(Container, "DataItem.url") %>' Text='模块地址' />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="更多链接地址" Visible="false" HeaderText="更多链接地址">
                                                    <HeaderStyle HorizontalAlign="Center" Width="15%" BackColor="Silver"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="dglabmore" Width="50" Text='<%# DataBinder.Eval(Container, "DataItem.moreurl") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="条目选择程序" HeaderText="条目选择程序">
                                                    <HeaderStyle HorizontalAlign="Center" Width="11%" BackColor="Silver"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="dgtxtpopurl" ToolTip='<%# DataBinder.Eval(Container, "DataItem.popurl") %>' Text="条目选择程序" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="输入参数" HeaderText="输入参数">
                                                    <HeaderStyle HorizontalAlign="Center" Width="30%" BackColor="Silver"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="dgtxtvalue" Text='<%# DataBinder.Eval(Container, "DataItem.vvalue") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="模块说明" HeaderText="模块说明">
                                                    <HeaderStyle HorizontalAlign="Center" Width="15%" BackColor="Silver"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="dgtxtdes" Text='<%# DataBinder.Eval(Container, "DataItem.description") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                                        </asp:DataGrid>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
    </font>
    </form>
</body>
</html>
