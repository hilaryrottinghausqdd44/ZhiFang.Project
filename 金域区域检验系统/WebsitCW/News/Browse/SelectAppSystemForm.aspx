<%@ Page Language="c#" AutoEventWireup="True"
    Inherits="OA.News.Browse.SelectAppSystemForm" Codebehind="SelectAppSystemForm.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>ѡ��Ӧ��ϵͳ</title>
    <base target="_self">
    <!-- �����Ų��ᵯ���´��� -->
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../../WebControlLib/CSS/WebControlDefault.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" src="../news/manage/htmledit/Dialog/dialog.js"></script>

    <script language="javascript" type="text/javascript">
			//ȡ��ܵ�URL
			oSelection = dialogArguments.eWebEditor.document.selection.createRange();
			sRangeType = dialogArguments.eWebEditor.document.selection.type;
			var url;
			if (sRangeType == "Control")
			{
				if (oSelection.item(0).tagName == "IFRAME")
				{
					oControl = oSelection.item(0);
					url = oControl.src;
				}
			}
			
			//body��onload����
			function showParamsToForm()
			{
				if(url == null)
				{
					return;
				}
				//�� url ��ȡ �����������
				var splitUrl = url.split("?");
				var splitParams = splitUrl[1].split("&");//ȡ�������б����
				var systemName;
				var tableName;
				var pageSize;
				for(var i=0;i<splitParams.length;i++)
				{
					var splitParam = splitParams[i].split("=");//��ֵ�ǰ��������
					var paramName = splitParam[0];
					var paramValue = splitParam[1];
					if(paramName == "systemName")
						systemName = paramValue;
					else if(paramName == "tableName")
						tableName = paramValue;
					else if(paramName == "pageSize")
						pageSize = paramValue;
				}
				//alert(systemName);
				//alert(pageSize);
				//var splitParam = splitParams[0].split("=");//��ֵ�һ������
				//var systemName = splitParam[1];//ȡ����һ������������
				var obj = document.getElementById("ddlAppSystem");
				var num = obj.options.length; 
				for(var i=0; i<num; i++)
				{
					if(obj.options[i].text == systemName)
					{
						obj.options[i].selected = true;
						break;
					}      
				}
				//ȡӦ��ϵͳ�µı��б�
				ShowTableListFromSystemName(obj);
				//��ʾ��ǰѡ��ı�����
				var obj = document.getElementById("ddlTable");
				var num = obj.options.length; 
				for(var i=0; i<num; i++)
				{
					if(obj.options[i].text == tableName)
					{
						obj.options[i].selected = true;
						break;
					}      
				}
				//��¼��
				var objRows = document.getElementById("txtRows");
				objRows.value = pageSize;
			}

			function ShowTableListFromSystemName(obj)
			{
				//ȡ��Ӧ��ϵͳ����
				var objID = document.getElementById("ddlAppSystem");
				var systemName = objID.value;
				//ȡ��Ӧ��ϵͳ�µ����б�
				var tableList = OA.News.Browse.SelectAppSystemForm.getTableListFromSystemName(systemName).value;
				var objTable = document.getElementById("ddlTable");
				objTable.length = 0;//����ձ��б�
				for(var i=0;i<tableList.length;i++)
				{
					objTable.options.add(new Option(tableList[i], tableList[i]));//Ϊ�ؼ������
				}

			}
    </script>

    <script language="JavaScript" event="onclick" for="Ok">
			//ȡӦ��ϵͳ
			var objSystem = document.getElementById("ddlAppSystem");
			systemName = objSystem.value;
			if(systemName == "")
			{
				self.close();
				return;
			}
			url = "../../News/Browse/BrowseAppSystemDataForm.aspx?systemName=" + systemName;
			//ȡ������
			var objTable = document.getElementById("ddlTable");
			tableName = objTable.value;
			if(tableName != "")
			{
				url += "&tableName=" + tableName;
			}
			//����
			pageSize = txtRows.value;
			url += "&pageSize=" + pageSize;
			sScrolling = "both"
			sFrameBorder = "0";
			sMarginHeight = "0";
			sMarginWidth = "0";
			sWidth = "300";
			sHeight = "200";
			dialogArguments.insertHTML("<iframe src='"+url+"' scrolling='"+sScrolling+"' frameborder='"+sFrameBorder+"' marginheight='"+sMarginHeight+"' marginwidth='"+sMarginWidth+"' width='"+sWidth+"' height='"+sHeight+"'></iframe>");
			window.returnValue = null;
			self.close();
			return;
    </script>

</head>
<body ms_positioning="GridLayout" onload="showParamsToForm()">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" cellspacing="1" cellpadding="1" width="448" border="1">
        <tr>
            <td colspan="2">
                <asp:Label ID="lblTitile" runat="server" Visible="False">����</asp:Label>
            </td>
        </tr>
        <tr>
            <td nowrap align="right" width="1%">
                <asp:Label ID="lblNewsCatagory" runat="server">Ӧ��ϵͳ����</asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlAppSystem" runat="server" Width="100%">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td nowrap align="right" width="1%">
                <asp:Label ID="Label2" runat="server">������</asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlTable" runat="server" Width="100%">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td nowrap align="right" width="1%">
                <asp:Label ID="Label1" runat="server">ÿҳ��ʾ��¼��</asp:Label>
            </td>
            <td align="left">
                <input id="txtRows" maxlength="4" size="10" name="txtRows" value="10" onkeypress="event.returnValue=IsDigit();">
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <font face="����">
                    <input id="Ok" type="submit" value="  ȷ��  " name="Ok"></font>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
