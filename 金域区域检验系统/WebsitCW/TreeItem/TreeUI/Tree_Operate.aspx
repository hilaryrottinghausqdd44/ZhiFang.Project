<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" CodeBehind="Tree_Operate.aspx.cs" Inherits="TreeItem.TreeUI.tree_Operate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
<html>
<head runat="server">
    <title>树操作</title>
    <link href="../Css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">              
        function $(s)
        {
            return document.getElementById?document.getElementById(s):document.all[s];
        }
        //选择图片
        function selectTreeImage()
        {
	        r = window.showModalDialog('selectModuleImage.aspx','','dialogWidth:600px;dialogHeight:618px;help:no;scroll:auto;status:no');
	        if (r == '' || typeof(r) == 'undefined')
	        {
		        return;
	        }
	        else
	        {
	            var url = "~/App_Themes/Images/icons/" + r;
		        //document.all("selectImage").src = url;
		        //divimage.style.display = "";
		        $('txtimageselect').value = url;
	        }
        }
        //选择链接地址
        function selectUrl()
        {
	        r = window.showModalDialog('selectModuleImage.aspx','','dialogWidth:600px;dialogHeight:618px;help:no;scroll:auto;status:no');
	        if (r == '' || typeof(r) == 'undefined')
	        {
		        return;
	        }
	        else
	        {
		        $('txturl').value = r;
	        }
        }
        //选择模块(单表)链接
        function SelectModuleUrl()
        {        	
	        var r;
	        var r=window.showModalDialog('../../RBAC/Modules/SelectModuleDialog.aspx','','dialogWidth:588px;dialogHeight:618px;help:no;scroll:no;status:no');
	        if (r == '' || typeof(r) == 'undefined'|| typeof(r) == 'object')
	        {
		        return;
	        }
	        else
	        {
		        var returns=r.split("\v");
		        $('txturl').value = "../.."+returns[1].substr(5);
	        }
        }
        //验证判断
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
      //删除此节点的二次确认 
      function JudgeConfirm(flag)
      {
         //有子节点
         if(flag == '1')
         {
             var tmp = confirm('真的要删除该节点及其子节点吗?');
             if(tmp == true)
             {
                 document.getElementById('btntmp').click();
             }
         }
         else
         {
             document.getElementById('btntmp').click();
         }
         
      }
      //加载时光标指向名称
      function NodeNameSelect()
      {
      if(fromtableA.style.display != "none"){
           document.getElementById('txtName').focus();
          }
         
      }
      //点高级时显示再点隐藏
      function seniorclick()
      {
         var o = seniortable.style.display;
         if(o == "none")
         {
             seniortable.style.display = "";
         }
         else
         {
             seniortable.style.display = "none";
         }
      }   
      function Getdisplay_nono()
      {
       fromtableA.style.display = "";//btntable
       btntable.style.display="";
      }
    </script>

    <style type="text/css">
        #newtype
        {
            height: 9px;
        }
    </style>
</head>
<body onload="NodeNameSelect()">
    <form id="form1" method="post" runat="server">
    <table cellspacing="1" cellpadding="0"  id="fromtableA"  width="95%" bgcolor="#efefef" border="1">
        <tr>
            <td align="center" bgcolor="#efefef" colspan="2" height="39">
                树模块管理<asp:Literal runat="server" ID="litid" Visible="false"></asp:Literal>
                <asp:Literal runat="server" ID="litparentid" Visible="false"></asp:Literal>
                <asp:Literal runat="server" ID="litorderid" Visible="false"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td align="right" width="10%" bgcolor="#efefef" height="34">
                类型：
            </td>
            <td align="left" bgcolor="#efefef" height="34">
                <asp:DropDownList runat="server" AutoPostBack="false" ID="drptype" OnSelectedIndexChanged="drptype_SelectedIndexChanged">
                </asp:DropDownList>
                &nbsp;<a href="TreeType.aspx" target="_blank">类型管理</a>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#efefef" height="34">
                名 称：
            </td>
            <td bgcolor="#efefef" height="34">
                <asp:TextBox ID="txtName" runat="server" Width="250px"></asp:TextBox>&nbsp;&nbsp;
                <asp:CheckBox runat="server" Visible="false" ID="chkisroot" Text="是否根节点" />
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#efefef" height="34">
                &nbsp;
            </td>
            <td bgcolor="#efefef" height="34">
                <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" ID="rdotype">
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#efefef" height="34">
                选中时图片：
            </td>
            <td bgcolor="#efefef" height="34">
                <table>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtimageselect" Width="250px" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <div id="divimage" style="display: none">
                                <img id="selectImage" alt="" src="" onclick="javascript:selectTreeImage()" height="25"
                                    width="25" border="0" />
                            </div>
                        </td>
                        <td>
                            <input type="button" id="btnll" onclick="javascript:selectTreeImage()" value="浏览" />
                            &nbsp;&nbsp;
                            <input type="button" id="btnsenior" value="高级" onclick="seniorclick();" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table cellspacing="1" id="seniortable" style="display: none" cellpadding="0" width="95%"
        bgcolor="#efefef" border="1">
        <tr>
            <td align="right" width="10%" style="border-top: 0" bgcolor="#efefef" height="34">
                链接地址：
            </td>
            <td bgcolor="#efefef" style="border-top: 0" height="34">
                <asp:TextBox ID="txturl" runat="server" Width="307px"></asp:TextBox>
                <input type="button" id="btnurlselect" onclick="SelectModuleUrl();" value="选择" />
                &nbsp;是否刷新输出框架<asp:DropDownList runat="server" ID="drprefresh">
                    <asp:ListItem Value="0">不刷新</asp:ListItem>
                    <asp:ListItem Value="1" Selected="True">刷新</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#efefef" height="34">
                参数：
            </td>
            <td bgcolor="#efefef" height="34">
                <asp:TextBox ID="txtparams" runat="server" Width="306px"></asp:TextBox><font color="red">如:输出值1#输出值2#输出值3</font>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#efefef" height="34">
                提示信息：
            </td>
            <td bgcolor="#efefef" height="34">
                <asp:TextBox ID="txttooltips" runat="server" Width="306px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#efefef" height="34">
                展开时图片：
            </td>
            <td bgcolor="#efefef" height="34">
                <asp:TextBox ID="txtimageopen" runat="server" Width="306px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#efefef" height="34">
                关闭时图片：
            </td>
            <td bgcolor="#efefef" height="34">
                <asp:TextBox ID="txtimageclose" runat="server" Width="306px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#efefef" height="34">
                数据源：
            </td>
            <td bgcolor="#efefef" height="34">
                <asp:TextBox ID="txtdatasource" runat="server" Width="306px"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table cellspacing="1" id="btntable" cellpadding="0" width="95%" bgcolor="#efefef" border="1">
        <tr>
            <td style="border-top: 0" align="center" bgcolor="#efefef" height="80">
                <font face="宋体">&nbsp;
                    <asp:Button runat="server" ID="btnDown" CssClass="buttonstyle" Text="创建节点" OnClick="btnDown_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnUpdate" runat="server" Visible="false" CssClass="buttonstyle"
                        Text=" 修 改 " OnClick="btnUpdate_Click"></asp:Button>&nbsp;&nbsp;
                    <asp:Button ID="btnDel" runat="server" Visible="false" CssClass="buttonstyle" Text=" 删  除 "
                        OnClick="btnDel_Click"></asp:Button>
                    <asp:Button runat="server" ID="btntmp" Text="过渡" Width="0px" Height="0px" CssClass="buttonstyle"
                        OnClick="btntmp_Click" />
                </font>
            </td>
        </tr>
    </table>

    </form>
</body>
</html>
