<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.Main.Desktop.RemoteTools" Codebehind="RemoteTools.aspx.cs" %>

<%@ Import Namespace="System.Xml" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>���������</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../../Includes/CSS/ioffice.css" type="text/css" rel="stylesheet">

    <script language="javascript">
			function VerifyFormEmployee()
			{
				return true;
			}	
			
			function ButtSelectFileBig_onclick() 
			{
				var r;
				var r=window.showModalDialog('../../DBQuery/PopupSelectImageFile.aspx?path=' 
				+ '../images/icons/'  +'&pageSize='
				+ 8 + '&style=list','','dialogWidth:588px;dialogHeight:618px;help:no;scroll:no;status:no');
				if (r == '' || typeof(r) == 'undefined'||typeof(r)=='object')
				{
					return;
				}
				else
				{
					r=r.substr(r.indexOf(".."));
					var bgObject = document.getElementById("txtXML");
					bgObject.value=r;
				}
			}
			
			function ButtSelectFile_onclick() 
			{
				//var r;
				//r=window.open('../../library/XPathAnalyzer/ContentPane.aspx','','width=550px,height=500px,scrollbars=yes,resizable=yes,left=' + (screen.availWidth-620)/2 + ',top=' + (screen.availHeight-470)/2 );	
				//if (r != '' && typeof(r) != 'undefined'&&typeof(r)!='object')
				//{
				//document.all['txtXML'].value=r;
				//}
				
				var r;
				var r=window.showModalDialog('../../DBQuery/PopupSelectImageFile.aspx?path=' 
				+ '../main/images/icons/'  +'&pageSize='
				+ 8 + '&style=list','','dialogWidth:588px;dialogHeight:618px;help:no;scroll:no;status:no');
				if (r == '' || typeof(r) == 'undefined'||typeof(r)=='object')
				{
					return;
				}
				else
				{
					r=r.substr(r.indexOf(".."));
					r="../" + r;
					var bgObject = document.getElementById("txtXML");
					bgObject.value=r;
					//Form1.buttonBackGround.value=r.substr(r.lastIndexOf("/")+1);
				}
			}
			
			function ButtSelectModule_onclick()
			{
				var r;
				var r=window.showModalDialog('../../RBAC/Modules/SelectModuleDialog.aspx','','dialogWidth:588px;dialogHeight:618px;help:no;scroll:no;status:no');
				if (r == '' || typeof(r) == 'undefined'||typeof(r)=='object')
				{
					return;
				}
				else
				{
					var returns=r.split("\v");
					document.all["TextBoxText"].value=returns[0];
					document.all["TextBoxNavigateUrl"].value=returns[1];
				}
			}
			var sel;
			function Delmoban(groupName,id)
			{
				if (confirm('�����Ҫɾ���˹�����'))
				{
					FormDelmoban.delGroup.value=groupName;
					FormDelmoban.delID.value=id;
					FormDelmoban.submit();
				}
			}
			
			
			function DelGroup(groupName)
			{
				if (confirm('�����Ҫɾ���˷�����'))
				{
					FormDelmoban.delGroup.value=groupName;
					FormDelmoban.delID.value="";
					FormDelmoban.submit();
				}
			}
			function EditGroup(groupName)
			{
				alert("�˹�����ʱ����ʹ��..");
			}
			
		
			function Editmoban(groupName,Text,NavigateUrl,ImageUrl)
				{
				
				if(Text!='')
				FormDelmoban.TextBoxText.value=Text;
				FormDelmoban.MID.value=Text;
				FormDelmoban.TextBoxNavigateUrl.value=NavigateUrl;
				FormDelmoban.txtXML.value=ImageUrl;
				document.all['ButtonModify'].disabled=false;
			
				}
			
			var SelEmpl = '';
			
			function SelectGroupTool(groupName,eid)
			{
				if (SelEmpl != '')
				{
					document.all['NM'+SelEmpl].style.backgroundColor = '';
					document.all['NM'+SelEmpl].style.color = '';
				}
				
				SelEmpl =groupName +'_'+ eid;				
				document.all['NM'+SelEmpl].style.backgroundColor = 'gold';
				document.all['NM'+SelEmpl].style.color = 'black';
				
				
						
			}
    </script>

    <script language="javascript" for="ButtonAdd" event="onclick">
			if(FormDelmoban.drpGroup.selectedIndex<0)
			{
				alert('û��ѡ����࣡���û��Ҫѡ��ķ���,��ѡ��������')
				return false
			}
			if(FormDelmoban.TextBoxText.value.length==0)
			{
				alert('���Ʋ���Ϊ�գ�')
				return false
			}
    </script>

    <script language="javascript" for="ButtonModify" event="onclick">
			if(FormDelmoban.drpGroup.selectedIndex<0)
			{
				alert('û��ѡ����࣡���û��Ҫѡ��ķ���,��ѡ��������')
				return false
			}
			if(FormDelmoban.TextBoxText.value.length==0)
				{
					alert('���Ʋ���Ϊ�գ�')
					return false
				}
    </script>

    <script language="javascript" for="ButtonGroupAdd" event="onclick">
			if(FormDelmoban.TextBoxNewGroup.value.length==0)
			{
				alert('������Ҫ������������')
				return false
			}
    </script>

</head>
<body ms_positioning="Gr">
    <form id="FormDelmoban" onsubmit="return VerifyFormEmployee()" method="post" runat="server">
    <table id="Table1" style="" cellspacing="1" cellpadding="1" width="406" border="1">
        <tr bgcolor="steelblue" height="30">
            <td align="center">
                <img src="../images/icons/0078_a.gif" align="absBottom">
            </td>
            <td>
                <b>���������</b>
                <asp:Label ID="LabelMsg" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
                ����
            </td>
            <td>
                <asp:DropDownList ID="drpGroup" runat="server">
                </asp:DropDownList>
                <font face="����">&nbsp; </font>
                <asp:Button ID="ButtonGroupAdd" runat="server" Text="��������" OnClick="ButtonGroupAdd_Click">
                </asp:Button>&nbsp;&nbsp;
                <asp:TextBox ID="TextBoxNewGroup" runat="server" Width="128px" BackColor="#E0E0E0"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 71px" align="center">
                <asp:Label ID="Label2" runat="server">����</asp:Label>
            </td>
            <td>
                <font face="����">
                    <asp:TextBox ID="TextBoxText" runat="server" Width="184px"></asp:TextBox>&nbsp;<input
                        onclick="return ButtSelectModule_onclick()" type="button" value="ѡ��ģ��"></font>
            </td>
        </tr>
        <tr>
            <td style="width: 71px" align="center">
                <asp:Label ID="Label3" runat="server">��ַ</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBoxNavigateUrl" runat="server" Width="184px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 71px" align="center">
                <asp:Label ID="Label4" runat="server">ͼƬ��ַ</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtXML" runat="server" Width="184px"></asp:TextBox><font face="����">&nbsp;</font><input
                    onclick="return ButtSelectFile_onclick()" type="button" value="ѡ��ͼƬ"><font face="����">&nbsp;</font>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="ButtonAdd" runat="server" Text="���" OnClick="ButtonAdd_Click"></asp:Button><font
                    face="����">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </font>
                <asp:Button ID="ButtonModify" runat="server" Text="�޸�"></asp:Button>
            </td>
        </tr>
    </table>
    <table cellspacing="1" cellpadding="1" border="1">
        <tr bgcolor="#e0e0e0">
            <td nowrap align="center">
                ͼƬ
            </td>
            <td nowrap align="center">
                ����
            </td>
            <td nowrap align="center">
                ��ַ
            </td>
            <td nowrap align="center">
                �޸�
            </td>
            <td nowrap align="center">
                ɾ��
            </td>
            <td nowrap align="center">
                ����
            </td>
        </tr>
        <%  
					
            foreach (XmlNode node1 in CurrentNodeChilds)
            {
        %>
        <tr bgcolor="#ccccff">
            <td colspan="3">
                <%=node1.Attributes.GetNamedItem("Text").InnerXml%>
            </td>
            <td nowrap>
                <a href="#" onclick="javascript:EditGroup('<%=node1.Attributes.GetNamedItem("Text").InnerXml%>')">
                    ����������</a>
            </td>
            <td nowrap>
                <%if (node1.ChildNodes.Count == 0)
                  {%><a href="#" onclick="javascript:DelGroup('<%=node1.Attributes.GetNamedItem("Text").InnerXml%>')">ɾ������</a><%}%>&nbsp;
            </td>
            <td>
                <img src="../images/icons/0048_b.gif">
            </td>
        </tr>
        <%
            int i = 0;
            foreach (XmlNode node in node1.ChildNodes)
            {%>
        <tr id="NM<%=node1.Attributes.GetNamedItem("Text").InnerXml%>_<%=i%>" bgcolor="white"
            onclick="SelectGroupTool('<%=node1.Attributes.GetNamedItem("Text").InnerXml%>','<%=i%>')"
            <%if(true)
						{%> ondblclick="javascript:Editmoban('<%=node1.Attributes.GetNamedItem("Text").InnerXml%>','<%=node.Attributes.GetNamedItem("Text").InnerXml%>','<%=node.Attributes.GetNamedItem("NavigateUrl").InnerXml.Replace("\\","\\\\")%>','<%=node.Attributes.GetNamedItem("ImageUrl").InnerXml%>')"
            <%}%> onmouseover="this.bgColor='LemonChiffon'" onmouseout="this.bgColor=''"
            title="<%=node.Attributes.GetNamedItem("Text").InnerXml%>">
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
                <a href="#" onclick="javascript:Editmoban('<%=node1.Attributes.GetNamedItem("Text").InnerXml%>','<%=node.Attributes.GetNamedItem("Text").InnerXml%>','<%=node.Attributes.GetNamedItem("NavigateUrl").InnerXml.Replace("\\","\\\\")%>','<%=node.Attributes.GetNamedItem("ImageUrl").InnerXml%>')">
                    �޸�</a>
            </td>
            <td>
                <a href="#" onclick="javascript:Delmoban('<%=node1.Attributes.GetNamedItem("Text").InnerXml%>','<%=node.Attributes.GetNamedItem("Text").InnerXml%>')">
                    ɾ��</a>
            </td>
            <td>
                <img src="../images/icons/0048_b.gif">
            </td>
        </tr>
        <%i++;
                        }%>
        <%}%>
    </table>
    <input id="delID" type="hidden" value="<%=newID%>" name="delID">
    <input id="MID" type="hidden" name="MID">
    <input id="delGroup" type="hidden" value="<%=newID%>" name="delGroup">
    </form>
</body>
</html>
