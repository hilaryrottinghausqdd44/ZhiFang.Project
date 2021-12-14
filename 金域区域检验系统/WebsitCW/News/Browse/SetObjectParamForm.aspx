<%@ Page validateRequest="false" enableEventValidation="false" Language="c#"
    AutoEventWireup="True" Inherits="OA.News.Browse.SetObjectParamForm" Codebehind="SetObjectParamForm.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>��ӻ�����Ӧ��ϵͳ</title>
    <!-- �����Ų��ᵯ���´��� -->
    <base target="_self">
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">

    <link href="../../WebControlLib/CSS/WebControlDefault.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" src= ="../news/manage/htmledit/Dialog/dialog.js"></script>

    <script language="javascript" type="text/javascript">
			//�ﶨ�¼�
			if(window.attachEvent)
			{
				window.attachEvent("onload", window_onload);
			}
			else if(window.addEventListener)
			{
				window.addEventListener('load', window_onload, false);
			}
			
			
			function window_onload()
			{
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
				//��ʾ����
				showParamsToForm(url)
				return;
			}
			
			//body��onload����:��ʾ����(�ӿ�ܵ�URL��ȡ,Ȼ��ֽ�ɲ���)
			function showParamsToForm(url)
			{
				//url = convertESCToHtml(url);
				//alert(url);
				if(url == null)
				{
					return;
				}
				//�� url ��ȡ �����������
				var splitUrl = url.split("?");
				var splitParams = splitUrl[1].split("&");//ȡ�������б����
				//�������
				var systemName = "";//��Ӧ��Ӧ��ϵͳ����
				var tableName = "";//��Ӧ�����ݿ��(ģ�������)
				var pageSize = "";//ÿҳ��ʾ�ļ�¼��
				var modalName = "";//ģ������
				var modalID = "";//ģ���ʶ(������ܵ�ID��Name)
				var relationModalName = "";//��������ģ������
				var relationFieldName = "";//�������ֶ�����
				var primaryFieldName = "";//��Ӧ��������ֶ�����
				var relationFieldValue = "";//�������ֶ�����
				var selectFields = "";//ѡ����ֶ�(�������б�����ʾ���ֶ�)
				var whereSQL = "";//�̶��Ĳ�ѯ����
				var sql = "";//�����Ĳ�ѯ���(��������˱����,��ֻ���б����,����������ѯ�йصĲ�������������)
				for(var i=0;i<splitParams.length;i++)
				{
					var splitParam = splitParams[i].split("=");//��ֵ�ǰ��������
					var paramName = splitParam[0];
					var paramValue = splitParams[i].replace(paramName + "=", "");
					if(paramName == "systemName") systemName = paramValue;//��Ӧ��Ӧ��ϵͳ����
					else if(paramName == "tableName") tableName = paramValue;
					else if(paramName == "pageSize") pageSize = paramValue;
					else if(paramName == "modalName") modalName = paramValue;
					else if(paramName == "modalID") modalID = paramValue;
					else if(paramName == "relationModalName") relationModalName = paramValue;
					else if(paramName == "relationFieldName") relationFieldName = paramValue;
					else if(paramName == "primaryFieldName") primaryFieldName = paramValue;
					else if(paramName == "relationFieldValue") relationFieldValue = paramValue;
					else if(paramName == "selectFields") selectFields = paramValue;
					else if(paramName == "whereSQL") whereSQL = paramValue;
					else if(paramName == "sql") sql = paramValue;
				}
				//��ʾ��ǰѡ���Ӧ��ϵͳ����
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
					if(obj.options[i].value == tableName)
					{
						obj.options[i].selected = true;
						break;
					}      
				}
				//��ʾ��¼��
				var obj = document.getElementById("txtRows");
				obj.value = pageSize;
				//ģ������
				var obj = document.getElementById("txtModalName");
				obj.value = modalName;
				//ģ���ʶ
				var obj = document.getElementById("txtModalID");
				obj.value = modalID;
				//��������ģ������
				var obj = document.getElementById("txtMainModalName");
				obj.value = relationModalName;
				//�������ֶ�����
				var obj = document.getElementById("txtRelationField");
				obj.value = relationFieldName;
				//��Ӧ��������ֶ�����
				var obj = document.getElementById("txtPrimaryField");
				obj.value = primaryFieldName;
				//ѡ����ֶ�
				var obj = document.getElementById("txtSelectField");
				obj.value = selectFields;
				//�̶��Ĳ�ѯ����
				//ת��
				//whereSQL = convertESCToHtml(whereSQL);
				var obj = document.getElementById("txtWhere");
				obj.value = whereSQL;
				//�����Ĳ�ѯ���
				//ת��
				//sql = convertESCToHtml(sql);
				var obj = document.getElementById("txtSQL");
				obj.value = sql;
				return;
			}


			//��ʾӦ��ϵͳ�µ����ݱ�
			function ShowTableListFromSystemName(obj)
			{
				//ȡ��Ӧ��ϵͳ����
				var objID = document.getElementById("ddlAppSystem");
				var systemName = objID.value;
				if((systemName == "") || (systemName == null))
				{
					return;
				}
				//ȡ��Ӧ��ϵͳ�µ����б�
				var tableList = OA.News.Browse.SetObjectParamForm.getTableListFromSystemName(systemName, false).value;
				var tableListDesc = OA.News.Browse.SetObjectParamForm.getTableListFromSystemName(systemName, true).value;
				var objTable = document.getElementById("ddlTable");
				objTable.length = 0;//����ձ��б�
				for(var i=0;i<tableList.length;i++)
				{
					objTable.options.add(new Option(tableListDesc[i], tableList[i]));//Ϊ�ؼ������
				}
				return;
			}
			
		function convertESCToHtml(esc)
		{
			var ret = esc;
			//ת��
			ret = ret.replace(new RegExp("&amp;" , "gm"), "&");
			ret = ret.replace(new RegExp("&quot;" , "gm"), "\"");
			ret = ret.replace(new RegExp("&apos;" , "gm"), "'");
			ret = ret.replace(new RegExp("&gt;" , "gm"), ">");
			ret = ret.replace(new RegExp("&lt;" , "gm"), "<");
			ret = ret.replace(new RegExp("&nbsp;" , "gm"), " ");
			ret = ret.replace(new RegExp("%20" , "gm"), " ");
			ret = ret.replace(new RegExp("EqualTo" , "gm"), "=");
			return ret;
		}
		function convertHtmlToESC(html)
		{
			var ret = html;
			//ת��
			ret = ret.replace(new RegExp("&" , "gm"), "&amp;");
			ret = ret.replace(new RegExp("\"" , "gm"), "&quot;");
			ret = ret.replace(new RegExp("'" , "gm"), "&apos;");
			ret = ret.replace(new RegExp(">" , "gm"), "&gt;");
			ret = ret.replace(new RegExp("<" , "gm"), "&lt;");
			ret = ret.replace(new RegExp("=" , "gm"), "EqualTo");
			return ret;
		}

			
			
    </script>

    <!-- ѡ���ֶε����ð�ť -->

    <script language="JavaScript" event="onclick" for="btnSelectField">
			var objSystem = document.getElementById("ddlAppSystem");
			systemName = objSystem.value;
			if(systemName == "")
			{
				alert("����ѡ��Ӧ��ϵͳ!");
				ddlAppSystem.focus();
				return;
			}
			//ȡ������
			var objTable = document.getElementById("ddlTable");
			tableName = objTable.value;
			if(tableName == "")
			{
				alert("����ѡ���!");
				ddlTable.focus();
				return;
			}
			//�̶��Ĳ�ѯ����
			//txtSelectField.value = tableName;
    </script>

    <!-- ��ѯ���������ð�ť -->

    <script language="JavaScript" event="onclick" for="btnSetQueryCondition">
			var objSystem = document.getElementById("ddlAppSystem");
			var systemName = objSystem.value;
			if(systemName == "")
			{
				alert("����ѡ��Ӧ��ϵͳ!");
				ddlAppSystem.focus();
				return;
			}
			//ȡ������
			var objTable = document.getElementById("ddlTable");
			var tableName = objTable.value;
			if(tableName == "")
			{
				alert("����ѡ���!");
				ddlTable.focus();
				return;
			}
			//�̶��Ĳ�ѯ����
			var url = "../../News/Browse/SetQueryConditionForm.aspx?systemName=" + systemName + "&tableName=" + tableName;
            var ret = window.showModalDialog(url,'','dialogWidth:100px;dialogHeight:100px;help:no;scroll:both;status:no');
			if(ret != null)
			{
				//alert(ret);
				//ret = convertESCToHtml(ret);
				var objWhere = document.getElementById("txtWhere");
				objWhere.value = ret;
			}
    </script>

    <!-- ȷ����ť -->

    <script language="JavaScript" event="onclick" for="Ok">
			//��ȷ����ťʹ��
			//ȡӦ��ϵͳ
			var objSystem = document.getElementById("ddlAppSystem");
			systemName = objSystem.value;
			if(systemName == "")
			{
				self.close();
				return;
			}
			//Ĭ��ģ������ΪӦ��ϵͳ����
			if(txtModalName.value == "")
				txtModalName.value = systemName;
			//Ĭ��ģ���ʶΪӦ��ϵͳ����
			if(txtModalID.value == "")
				txtModalID.value = systemName;
			//ƴURL
			url = "../../News/Browse/BrowseAppSystemDataForm.aspx?systemName=" + systemName;
			//ȡ������
			var objTable = document.getElementById("ddlTable");
			tableName = objTable.value;
			if(tableName != "")
			{
				url += "&tableName=" + tableName;
			}
			//��¼��
			pageSize = txtRows.value;
			url += "&pageSize=" + pageSize;
			//ģ������
			modalName = txtModalName.value;
			url += "&modalName=" + modalName;
			if(modalName == "")
			{
				alert("������ģ������!");
				txtModalName.focus();
				return;
			}
			//ģ���ʶ
			if(txtModalID.value == "")
				txtModalID.value = modalName;
			modalID = txtModalID.value;
			url += "&modalID=" + modalID;
			//��������ģ������
			relationModalName = txtMainModalName.value;
			url += "&relationModalName=" + relationModalName;
			//�������ֶ�����
			relationFieldName = txtRelationField.value;
			url += "&relationFieldName=" + relationFieldName;
			//��Ӧ��������ֶ�����
			primaryFieldName = txtPrimaryField.value;
			url += "&primaryFieldName=" + primaryFieldName;
			//�������ֶ�����
			relationFieldValue = "";
			url += "&relationFieldValue=" + relationFieldValue;
			//ѡ����ֶ�
			selectFields = txtSelectField.value;
			url += "&selectFields=" + selectFields;
			//�̶��Ĳ�ѯ����
			whereSQL = txtWhere.value;
			//����ѯ����ת��
			//whereSQL = convertHtmlToESC(whereSQL);
			url += "&whereSQL=" + whereSQL;
			//�����Ĳ�ѯ���
			sql = txtSQL.value;
			//ת��
			//sql = convertHtmlToESC(sql);
			url += "&sql=" + sql;
			//��ܵĲ���
			sScrolling = "both"
			sFrameBorder = "0";
			sMarginHeight = "0";
			sMarginWidth = "0";
			sWidth = "300";
			sHeight = "200";
			var insertIFRAME = "<iframe src=\"" + url + "\" id='" +modalID + "' name='" + modalID+"' scrolling='"+sScrolling+"' frameborder='"+sFrameBorder+"' marginheight='"+sMarginHeight+"' marginwidth='"+sMarginWidth+"' width='"+sWidth+"' height='"+sHeight+"'></iframe>";
			//alert(insertIFRAME);
			dialogArguments.insertHTML(insertIFRAME);
			window.returnValue = insertIFRAME;
			self.close();
			return;
    </script>

</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" cellspacing="1" cellpadding="1" width="100%" border="1">
        <tr>
            <td style="height: 27px" nowrap align="right" width="1%">
                <asp:Label ID="lblNewsCatagory" runat="server">Ӧ��ϵͳ����</asp:Label>
            </td>
            <td style="height: 27px">
                <asp:DropDownList ID="ddlAppSystem" runat="server" Width="100%">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td nowrap align="right" width="1%">
                <asp:Label ID="Label8" runat="server">������</asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlTable" runat="server" Width="100%">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td nowrap align="right" width="1%">
                <asp:Label ID="lblModalName" runat="server" Width="100%">ģ������</asp:Label>
            </td>
            <td align="left">
                <input id="txtModalName" style="width: 598px; height: 22px" size="94" name="txtModalName">
            </td>
        </tr>
        <tr>
            <td nowrap align="right" width="1%">
                <asp:Label ID="lblModalID" runat="server" Width="100%">ģ���ʶ</asp:Label>
            </td>
            <td align="left">
                <input id="txtModalID" style="width: 598px; height: 22px" size="94" name="txtModalID">
            </td>
        </tr>
        <tr>
            <td nowrap align="right" width="1%">
                <asp:Label ID="lblMainModalName" runat="server" Width="100%">������ģ������</asp:Label>
            </td>
            <td align="left">
                <input id="txtMainModalName" style="width: 598px; height: 22px" size="94" name="txtMainModalName">
            </td>
        </tr>
        <tr>
            <td nowrap align="right" width="1%">
                <asp:Label ID="Label4" runat="server" Width="100%">�����ֶ�</asp:Label>
            </td>
            <td align="left">
                <input id="txtRelationField" style="width: 598px; height: 22px" size="94" name="txtRelationField">
            </td>
        </tr>
        <tr>
            <td nowrap align="right" width="1%">
                <asp:Label ID="Label2" runat="server" Width="100%">��Ӧ������ֶ�</asp:Label>
            </td>
            <td align="left">
                <input id="txtPrimaryField" style="width: 598px; height: 22px" size="94" name="txtPrimaryField">
            </td>
        </tr>
        <tr>
            <td nowrap align="right" width="1%">
                <asp:Label ID="Label5" runat="server" Width="100%">��ʾ���ֶ�</asp:Label>
            </td>
            <td align="left">
                <textarea id="txtSelectField" style="width: 510px; height: 54px" name="txtSelectField"
                    rows="3" cols="61"></textarea>
                <input id="btnSelectField" style="width: 82px; height: 30px" type="button" value="  ����  "
                    name="btnSelectField">
            </td>
        </tr>
        <tr>
            <td style="height: 75px" nowrap align="right" width="1%">
                <asp:Label ID="Label6" runat="server" Width="100%">�̶��Ĳ�ѯ����</asp:Label>
            </td>
            <td valign="middle" align="left" width="100%">
                <textarea id="txtWhere" style="width: 510px; height: 54px" name="txtWhere" rows="3"
                    cols="61"></textarea>
                <input id="btnSetQueryCondition" style="width: 82px; height: 30px" type="button"
                    value="  ����  " name="btnSetQueryCondition">
            </td>
        </tr>
        <tr>
            <td style="height: 76px" nowrap align="right" width="1%">
                <asp:Label ID="Label7" runat="server" Width="100%">�����Ĳ�ѯ���</asp:Label>
            </td>
            <td style="height: 76px" align="left">
                <textarea id="txtSQL" style="width: 598px; height: 96px" name="txtSQL" rows="6" cols="72"></textarea>
            </td>
        </tr>
        <tr>
            <td nowrap align="right" width="1%">
                <asp:Label ID="Label1" runat="server">ÿҳ��ʾ��¼����</asp:Label>
            </td>
            <td align="left">
                <input onkeypress="event.returnValue=IsDigit();" id="txtRows" maxlength="4" size="10"
                    value="10" name="txtRows">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <input id="Ok" type="submit" value="  ȷ��  " name="Ok">
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
