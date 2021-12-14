<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModuleAddressSelect.aspx.cs" Inherits="OA.ModuleManage.ModuleAddressSelect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>模块地址选择</title>
    <link href="style.css" rel="stylesheet">
    <script type="text/javascript">
		//子窗体对父窗体的赋值操作
        function setValue(tag,id,name)
        {
            //tag 
            if(id != null && name != null)
            {
                if(tag == 0)
                {                    
                    $('txtname').value = name;
                    $('txtvalue').value = id;
                    var pam1 = new Array;
                    pam1 = id.split("=");
                    $('txtrpamemater').value = pam1[1];
                    var rpam = $('txtreturnpam').value;
                    if(rpam == "" || rpam.length== 0)
                    {
                       $('txtreturnpam').value = pam1[0];
                    }
                    if(name == "个人文件夹" || name == "共享文件夹" || name == "公共文件夹")
                   {
                      document.getElementById("newid").innerText = "你选择的是:" + $('txttmpname').value;
                   }
                   else
                   {
                      document.getElementById("newid").innerText = "你选择的是:" + $('txttmpname').value + "  " + name;
                   } 
                                             
                }
            }
        }        
       
        
        //打开子窗体       
        function showUserDialog(tag,strurl,listurl,listname,moreurl,mid,rpam,upam)
        {  
           $('txtmid').value = mid;//模块编号          
           $('txtmoreurl').value = moreurl;//更新链接        
           $('txtlisturl').value = listurl;//先将所选择的连接地址赋给一个textbox
           $('txttmpname').value = listname;
           $('txtname').value = "";//清空再次选择前的textbox
           $('txtvalue').value = "";  
           strurl = strurl + "?moreurl="+escape(moreurl); 
           //alert('tag=' + tag + ',strurl=' + strurl +',listurl=' + listurl+',listname=' + listname+',moreurl=' + moreurl);
           window.showModalDialog(strurl,this,'dialogWidth:500px;dialogHeight:500px;');
        }
        var result = "";
        //选择已选项目返回
        function SelectTxt()
        {
           var url = $('txtlisturl').value;
           //链接更多
           var tmpmoreurl = $('txtmoreurl').value;          
           //返回参数左边
           var pam1 = $('txtreturnpam').value;
           //返回参数右边
           var pam2 = $('txtrpamemater').value;
           var par = pam1 + '=' + pam2;
           if(pam2 == "" || pam2.length == 0)
           {
              alert('请选择模块参数条件!');
           }
           else
           {              
              result = url;
              if(par == "个人文件夹" || par == "共享文件夹")
              {
                 result = url;              
              }
              else if(par == "公共文件夹")
              {
                 result = url  + "?folder="+escape('\0');
              }
              else
              {
                 if(url.indexOf("?") > 0)
                 {
                    result = url + "&" + par;
                 }
                 else
                 {
                    result = url  + "?" + par;
                 }
              }
              if(tmpmoreurl.length > 0)
              {
                 result = result + "&moreurl=" + tmpmoreurl;
              }
               if(result.length > 0)
               {
                  window.returnValue = result;
			      window.close();	
		       }
		       else
		       {
			      alert('请选择相应参数');
		       }
             		 		   
           }          
        }       
        function $(s)
        {
            return document.getElementById?document.getElementById(s):document.all[s];
        }
       
    </script>

    <style type="text/css">
        .hideClass
        {
            display: none;
        }
    </style>
    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <div>    
        <asp:TextBox ID="txtmid" CssClass="hideClass" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtname" CssClass="hideClass" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtvalue" CssClass="hideClass" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtmoreurl" CssClass="hideClass" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtlisturl" CssClass="hideClass" runat="server"></asp:TextBox>
        <asp:TextBox ID="txttmpname" CssClass="hideClass" runat="server"></asp:TextBox>
        <table bordercolor="#003366" cellspacing="0" bordercolordark="#ffffff" cellpadding="0"
            width="100%" align="center" bgcolor="#fcfcfc" bordercolorlight="#aecdd5" border="1">
            <tbody>
                <tr valign="top">
                    <td align="center" width="100%">
                        <asp:DataGrid ID="myDataGrid" runat="server" PageSize="15" Font-Size="Smaller" BorderWidth="1px"
                            CellPadding="3" BorderStyle="None" BackColor="White" AutoGenerateColumns="False"
                            BorderColor="#A7C4F7" Width="100%" 
                            onitemdatabound="myDataGrid_ItemDataBound">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
                            <ItemStyle ForeColor="#330099" BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Size="Smaller" Font-Bold="True" ForeColor="Black" BackColor="#990000">
                            </HeaderStyle>
                            <FooterStyle ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
                            <Columns>
                                <asp:BoundColumn DataField="id" Visible="False" SortExpression="编号" ReadOnly="true"
                                    HeaderText="编号">
                                    <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn SortExpression="名称" HeaderText="名称">
                                    <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container, "DataItem.name") %>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="选择" HeaderText="选择">
                                    <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>
                                        <a href="#" onclick="showUserDialog(0,'<%# DataBinder.Eval(Container, "DataItem.popurl") %>','<%# DataBinder.Eval(Container, "DataItem.url") %>','<%# DataBinder.Eval(Container, "DataItem.name") %>','<%# DataBinder.Eval(Container, "DataItem.moreurl") %>','<%# DataBinder.Eval(Container, "DataItem.id") %>','<%# DataBinder.Eval(Container, "DataItem.ReturnPam") %>','<%# DataBinder.Eval(Container, "DataItem.UnionPam") %>');">
                                            选择条件</a>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </td>
                </tr>
                 <tr>
                    <td id="newid" style="color: #ff3300">
                    </td>
                </tr>
               <tr>
                    <td>
                        接收参数:<asp:TextBox runat="server" contentEditable="false" Width="50px" ID="txtreturnpam"></asp:TextBox>=<asp:TextBox
                            runat="server" ID="txtrpamemater" Width="70%"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <input
                            type="button" id="btnSub" class="buttonstyle" onclick="SelectTxt();" value="选 择" />&nbsp;&nbsp;<input
                                type="button" id="btncancel" class="buttonstyle" value="关 闭" onclick="window.close();">
                    </td>
                </tr>
              
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
