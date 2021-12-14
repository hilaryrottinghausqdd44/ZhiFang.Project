<%@ Page Language="c#" AutoEventWireup="True"
    Inherits="OA.News.Browse.SelectNewsCatagoryForm" Codebehind="SelectNewsCatagoryForm.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>ѡ��������Ŀ</title>
    <base target="_self">
    <!-- �����Ų��ᵯ���´��� -->
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="CSS/StatSetupA.css" type="text/css" rel="stylesheet">

    <script language="JavaScript" src="../news/manage/htmledit/Dialog/dialog.js"></script>

    <script language="javascript" type="text/javascript">
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
				var catagoryName;
				var pageSize;
				for(var i=0;i<splitParams.length;i++)
				{
					var splitParam = splitParams[i].split("=");//��ֵ�ǰ��������
					var paramName = splitParam[0];
					var paramValue = splitParam[1];
					if(paramName == "catagoryName")
						catagoryName = paramValue;
					else if(paramName == "pageSize")
						pageSize = paramValue;
				}
				//���ŷ�������
				var obj = document.getElementById("ddlNewsCatagory");
				var num = obj.options.length; 
				for(var i=0; i<num; i++)
				{
					if(obj.options[i].text == catagoryName)
					{
						obj.options[i].selected = true;
						break;
					}      
				}
				//��¼��
				var objRows = document.getElementById("txtRows");
				objRows.value = pageSize;
			}
    </script>

    <script language="JavaScript" event="onclick" for="Ok">
			//ȡ���ŷ���
			var objID = document.getElementById("ddlNewsCatagory");
			catagoryName = objID.value;
			url = "../../News/Browse/BrowseNewsForm.aspx?catagoryName=" + catagoryName;
			//����
			pageSize = txtRows.value;
			url += "&pageSize=" + pageSize;
			sScrolling = "no"
			sFrameBorder = "0";
			sMarginHeight = "0";
			sMarginWidth = "0";
			sWidth = "300";
			sHeight = "200";
			window.dialogArguments.insertHTML("<iframe src='"+url+"' scrolling='"+sScrolling+"' frameborder='"+sFrameBorder+"' marginheight='"+sMarginHeight+"' marginwidth='"+sMarginWidth+"' width='"+sWidth+"' height='"+sHeight+"'></iframe>");
			window.returnValue = null;
			self.close();
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
                <asp:Label ID="lblNewsCatagory" runat="server">������Ŀ����</asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlNewsCatagory" runat="server" Width="100%">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td nowrap align="right" width="1%">
                <asp:Label ID="Label1" runat="server">ÿҳ��ʾ���Ÿ���</asp:Label>
            </td>
            <td align="left">
                <input onkeypress="event.returnValue=IsDigit();" id="txtRows" maxlength="4" size="10"
                    name="txtRows" value="10">
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <input id="Ok" type="submit" value="  ȷ��  " name="Ok">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
