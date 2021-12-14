<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.Main.TopMenu" Codebehind="TopMenu.aspx.cs" %>

<%@ Import Namespace="System.Xml" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>顶部链接</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Includes/CSS/ioffice.css" type="text/css" rel="stylesheet">

    <script language="javascript">
			
			function VerifyFormEmployee()
			{
				if(FormDelmoban.TextBoxText.value.length==0)
				{
					alert('名称不能为空！')
					return false
				}
			
			}	
			
			function buttSelectTarget_onclick() 
			{
				r=window.showModalDialog('Desktop/selectTarget.aspx','','width=200px,height=200px,resizable=yes,left=' + (screen.availWidth-620)/2 + ',top=' + (screen.availHeight-470)/2 );	
				FormDelmoban.TextBoxPlace.value=r;

			}
			
			function ButtSelectFile_onclick() 
			{
				var r;
				r=window.open('../library/XPathAnalyzer/ContentPane.aspx','','width=550px,height=500px,scrollbars=yes,resizable=yes,left=' + (screen.availWidth-620)/2 + ',top=' + (screen.availHeight-470)/2 );	
				if (r != '' && typeof(r) != 'undefined'&&typeof(r)!='object')
				{
				document.all['txtXML'].value=r;
				}
			}
			var sel;
			function Delmoban(id)
			{
				if (confirm('您真的要删除此工具吗？'))
				{
				
					FormDelmoban.delID.value=id;
					FormDelmoban.submit();
				}
			}

			function SortMoban(Text, SortType) {
			    FormDelmoban.TextBoxText.value = Text;
			    FormDelmoban.delID.value = Text;
			    FormDelmoban.SortType.value = SortType;
			    FormDelmoban.submit();
			}
		
			function Editmoban(Text,NavigateUrl,ImageUrl,Target)
				{
				
				if(Text!='')
				FormDelmoban.TextBoxText.value=Text;
				FormDelmoban.MID.value=Text;
				FormDelmoban.TextBoxNavigateUrl.value=NavigateUrl;
				FormDelmoban.txtXML.value=ImageUrl;
				FormDelmoban.TextBoxPlace.value=Target;
				document.all['ButtonModify'].disabled=false;
			
				}
			
			var SelEmpl = '';
			
			function SelectEmpl(eid)
			{
				if (SelEmpl != '')
				{
					document.all['NM'+SelEmpl].style.backgroundColor = '';
					document.all['NM'+SelEmpl].style.color = '';
				}
				
				SelEmpl = eid;				
				document.all['NM'+SelEmpl].style.backgroundColor = 'gold';
				document.all['NM'+SelEmpl].style.color = 'black';
			}
    </script>

</head>
<body>
    <form id="FormDelmoban" onsubmit="return VerifyFormEmployee()" method="post" runat="server">
    <table id="Table1" style="width: 456px; border-collapse: collapse" cellspacing="1"
        cellpadding="1" width="456" border="1">
        <tr bgcolor="LightBlue" height="30">
            <td nowrap align="center">
                <img src="../images/icons/0078_a.gif" align="absBottom">
            </td>
            <td>
                <b>顶部定制</b>
            </td>
        </tr>
        <tr>
            <td style="width: 71px" align="center">
                <asp:Label ID="Label2" runat="server">名称</asp:Label>
            </td>
            <td>
                <font face="宋体">
                    <asp:TextBox ID="TextBoxText" runat="server" Width="392px"></asp:TextBox></font>
            </td>
        </tr>
        <tr>
            <td style="width: 71px" align="center">
                <asp:Label ID="Label3" runat="server">地址</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBoxNavigateUrl" runat="server" Width="392px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 71px" align="center">
                <asp:Label ID="Label4" runat="server">图片地址</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtXML" runat="server" Width="344px"></asp:TextBox><input style="width: 48px;
                    height: 22px" onclick="return ButtSelectFile_onclick()" type="button" value="选择">
            </td>
        </tr>
        <tr>
            <td style="width: 71px" align="center">
                <asp:Label ID="Label5" runat="server">窗口位置</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBoxPlace" runat="server" Width="344px"></asp:TextBox><input
                    style="width: 48px; height: 22px" onclick="return buttSelectTarget_onclick()"
                    type="button" value="选择">
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="ButtonAdd" runat="server" Width="38px" Text="添加" OnClick="ButtonAdd_Click">
                </asp:Button><font face="宋体">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </font>
                <asp:Button ID="ButtonModify" runat="server" Text="修改" OnClick="ButtonModify_Click">
                </asp:Button><font face="宋体">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </font>
                <input id="ButtonSoph" type="button" value="高级风格编写" onclick="window.open('../Dbquery/CodeXMLEditor.aspx?file=<%=TopMenuStyleFilePath %>&CreateNew=New','_self')" />
            </td>
        </tr>
    </table>
    <table style="width: 556px; border-collapse: collapse; height: 20px" cellspacing="1"
        cellpadding="1" width="556" border="1">
        <tr bgcolor="#e0e0e0">
            <td nowrap align="center" colspan="4">
                调整顺序
            </td>
            <td nowrap align="center">
                图片
            </td>
            <td nowrap align="center">
                名称
            </td>
            <td nowrap align="center">
                地址 (<%=CurrentNodeChilds.Count%>)
            </td>
            <td nowrap align="center">
                修改
            </td>
            <td nowrap align="center">
                删除
            </td>
        </tr>
        <%  int i = 0;
            foreach (XmlNode node in CurrentNodeChilds)
            {
        %>
        <tr id="NM<%=i%>" bgcolor="white" onclick="SelectEmpl('<%=i%>')" <%if(true)
						{%> ondblclick="javascript:Editmoban('<%=node.Attributes.GetNamedItem("Text").InnerXml%>','<%=node.Attributes.GetNamedItem("NavigateUrl").InnerXml%>','<%=node.Attributes.GetNamedItem("ImageUrl").InnerXml%>','<%=node.Attributes.GetNamedItem("Target").InnerXml%>')"
            <%}%> onmouseover="this.bgColor='LemonChiffon'" onmouseout="this.bgColor=''"
            title="<%=node.Attributes.GetNamedItem("Text").InnerXml%>" style="cursor:hand">
            <td>
                <img onclick="javascript:SortMoban('<%=node.Attributes.GetNamedItem("Text").InnerXml%>',0)" style="display:<%=(i==0)?"none":""%>" src='../images/icons/0048_a.gif' />
            </td>
             <td>
                <img onclick="javascript:SortMoban('<%=node.Attributes.GetNamedItem("Text").InnerXml%>',1)" style="display:<%=(i==0)?"none":""%>" src='../images/icons/0048_b.gif' />
            </td>
             <td>
                <img onclick="javascript:SortMoban('<%=node.Attributes.GetNamedItem("Text").InnerXml%>',2)"  style="display:<%=(i==CurrentNodeChilds.Count-1)?"none":""%>" src='../images/icons/0049_a.gif' />
            </td>
             <td width="40px">
                <img onclick="javascript:SortMoban('<%=node.Attributes.GetNamedItem("Text").InnerXml%>',3)" style="display:<%=(i==CurrentNodeChilds.Count-1)?"none":""%>" src='../images/icons/0049_b.gif' />
            </td>
             <td>
                <img src='<%=node.Attributes.GetNamedItem("ImageUrl").InnerXml%>' />
            </td>
            <td nowrap>
                <%=node.Attributes.GetNamedItem("Text").InnerXml%>
            </td>
            <td>
                <%=node.Attributes.GetNamedItem("NavigateUrl").InnerXml%>
            </td>
            <td>
                <a href="#" onclick="javascript:Editmoban('<%=node.Attributes.GetNamedItem("Text").InnerXml%>','<%=node.Attributes.GetNamedItem("NavigateUrl").InnerXml%>','<%=node.Attributes.GetNamedItem("ImageUrl").InnerXml%>','<%=node.Attributes.GetNamedItem("Target").InnerXml%>')">
                    修改</a>
            </td>
            <td>
                <a href="#" onclick="javascript:Delmoban('<%=node.Attributes.GetNamedItem("Text").InnerXml%>')">
                    删除</a>
            </td>
        </tr>
        <%i++;
                    }%>
    </table>
    <br />
    <font color="skyblue">图片调整说明：</font><br />
    <table style="width: 556px; border-collapse: collapse; height: 20px" cellspacing="1"
        cellpadding="1" width="556" border="1">
        <tr bgcolor="#e0e0e0">
            <td nowrap align="center" width="10%">
                <img src='../images/icons/0048_a.gif' />
            </td>
            <td nowrap align="center">
                排序到最前一个
            </td>
        </tr>
        
        <tr bgcolor="#e0e0e0">
            <td nowrap align="center">
                <img src='../images/icons/0048_b.gif' />
            </td>
            <td nowrap align="center">
                上移一位
            </td>
        </tr>
        <tr bgcolor="#e0e0e0">
            <td nowrap align="center">
                <img src='../images/icons/0049_a.gif' />
            </td>
            <td nowrap align="center">
                排序到最后一个
            </td>
        </tr>
        <tr bgcolor="#e0e0e0">
            <td nowrap align="center">
                <img src='../images/icons/0049_b.gif' />
            </td>
            <td nowrap align="center">
                下移一位
            </td>
        </tr>
      </table>
    <input id="delID" type="hidden" value="<%=newID%>" name="delID">
    <input id="MID" type="hidden" name="MID">
    <input id="SortType" type="hidden" value="" name="SortType" />
    </form>
</body>
</html>
