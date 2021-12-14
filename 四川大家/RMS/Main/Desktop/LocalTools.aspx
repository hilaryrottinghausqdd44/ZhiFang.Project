<%@ Import Namespace="System.Xml" %>

<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.Main.Desktop.LocalTools" Codebehind="LocalTools.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>公用工具</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../../Includes/CSS/ioffice.css" type="text/css" rel="stylesheet">

    <script language="javascript">
			function VerifyFormEmployee()
			{
				if(FormDelmoban.TextBoxText.value.length==0)
				{
					alert('名称不能为空！')
					return false
				}
			
			}	
			
			function pass()
			{
			FormDelmoban.TextBoxNavigateUrl.value=FormDelmoban.FileFieldNavigateUrl.value;
			}
			
			function ButtSelectFile_onclick() 
			{
				var r;
				r=window.open('../../library/XPathAnalyzer/ContentPane.aspx','','width=550px,height=500px,scrollbars=yes,resizable=yes,left=' + (screen.availWidth-620)/2 + ',top=' + (screen.availHeight-470)/2 );	
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
		
			function Editmoban(Text,NavigateUrl,ImageUrl)
				{
				
				if(Text!='')
				FormDelmoban.TextBoxText.value=Text;
				FormDelmoban.MID.value=Text;
				FormDelmoban.TextBoxNavigateUrl.value=NavigateUrl;
				FormDelmoban.txtXML.value=ImageUrl;
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
<body ms_positioning="GridLayout">
    <form id="FormDelmoban" onsubmit="return VerifyFormEmployee()" method="post" runat="server">
    <table id="Table1" style="z-index: 101; left: 8px; position: absolute; top: 8px;
        border-collapse: collapse" cellspacing="1" cellpadding="1" width="300" border="1">
        <tr bgcolor="steelblue" height="30">
            <td align="center">
                <img src="../images/icons/0078_a.gif" align="absBottom">
            </td>
            <td>
                <b>公用工具</b>
            </td>
        </tr>
        <tr>
            <td style="width: 71px; height: 5px" align="center">
                <asp:Label ID="Label2" runat="server">名称</asp:Label>
            </td>
            <td style="height: 5px">
                <font face="宋体">
                    <asp:TextBox ID="TextBoxText" runat="server" Width="304px"></asp:TextBox></font>
            </td>
        </tr>
        <tr>
            <td style="width: 71px; height: 34px" align="center">
                <asp:Label ID="Label3" runat="server">地址</asp:Label>
            </td>
            <td style="height: 34px">
                <table id="Table2" cellspacing="0" cellpadding="0" width="300" border="0">
                    <tr>
                        <td style="width: 231px; height: 15px">
                            <div>
                                <font face="宋体">
                                    <input id="TextBoxNavigateUrl" style="width: 225px; height: 22px" type="text" size="30"
                                        name="TextBoxNavigateUrl"></font></div>
                        </td>
                        <td style="height: 15px">
                            <input id="FileFieldNavigateUrl" style="width: 0px; height: 22px" onpropertychange="javascript:pass() "
                                type="file" size="0" name="FileFieldNavigateUrl">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 71px" nowrap align="center">
                <asp:Label ID="Label4" runat="server">图片地址</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtXML" runat="server" Width="264px"></asp:TextBox><input onclick="return ButtSelectFile_onclick()"
                    type="button" value="选择">
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="ButtonAdd" runat="server" Text="添加" OnClick="ButtonAdd_Click"></asp:Button><font
                    face="宋体">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </font>
                <asp:Button ID="ButtonModify" runat="server" Text="修改" OnClick="ButtonModify_Click">
                </asp:Button>
            </td>
        </tr>
    </table>
    <table style="z-index: 101; left: 8px; width: 384px; position: absolute; top: 160px;
        border-collapse: collapse; height: 20px" cellspacing="1" cellpadding="1" width="384"
        border="1">
        <tr bgcolor="#e0e0e0">
            <td nowrap align="center">
                图片
            </td>
            <td nowrap align="center">
                名称
            </td>
            <td nowrap align="center">
                地址
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
						{%> ondblclick="javascript:Editmoban(
							'<%=node.Attributes.GetNamedItem("Text").InnerXml%>',
							'<%=node.Attributes.GetNamedItem("NavigateUrl").InnerXml.Replace("\\","\\\\")%>',
							'<%=node.Attributes.GetNamedItem("ImageUrl").InnerXml%>')" <%}%>
            onmouseover="this.bgColor='LemonChiffon'" onmouseout="this.bgColor=''" title="<%=node.Attributes.GetNamedItem("Text").InnerXml%>">
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
                <a href="#" onclick="javascript:Delmoban('<%=node.Attributes.GetNamedItem("Text").InnerXml%>')">
                    删除</a>
            </td>
        </tr>
        <%i++;
                    }%>
    </table>
    <input id="MID" style="z-index: 103; left: 416px; position: absolute; top: 24px"
        type="hidden" name="MID">
    <input id="delID" type="hidden" value="<%=newID%>" name="delID">
    </form>
</body>
</html>
