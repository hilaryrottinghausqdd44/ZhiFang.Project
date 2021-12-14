<%@ Page Language="c#" validateRequest="false" enableEventValidation="false" AutoEventWireup="True"
    Inherits="OA.Encryption.DigitSign" Codebehind="DigitSign.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>DigitSign</title>
    <meta content="True" name="vs_showGrid">
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">

    <script language="javascript" id="clientEventHandlersJS">
		<!--

		function window_onload() {
		
		}
		function selectFile()
		{
			r=window.showModalDialog('selectModuleImage.aspx?path=data/','','dialogWidth:588px;dialogHeight:618px;help:no;scroll:auto;status:no');
			if (r == '' || typeof(r) == 'undefined')
			{
				return;
			}
			else
			{
				document.all["txtFile"].value=r;
				document.all["txtFile"].disabled=false;
				Form1.submit();
			}
		}
		function btnSignClick()
		{
			//document.forms[0].SignTxt.value = "Here is the signing text!";
			var sInfo = ThCrypt.SignFile(document.forms[0].txtFile.value);
			document.forms[0].SignValue.value = sInfo;
		}

		function NameFile()
		{
			var name;
			name=window.prompt('请输入要保存的名称','文件名xxx');
			if(name!=''&& name!=null)
			{
				document.forms[0].TextBoxSignedFile.value=name;
				//Form1.submit();
				return true;
			}
			else
				return false;
		}
		//-->
    </script>

    <script for="buttSave" event="onclick">
			if(document.all["txtFile"].value==''|| document.forms[0].SignValue.value=='')
			{
				alert('还没有选定文件或未签名');
				return false;
			}
			return NameFile();
    </script>

</head>
<body language="javascript" onload="return window_onload()">
    <form id="Form1" method="post" runat="server">
    <p>
        <table id="Table1" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
            width: 616px; border-bottom: 1px solid; height: 242px" cellspacing="1" cellpadding="1"
            width="616" border="0">
            <tr>
                <td style="width: 82px">
                    <p align="center">
                        <img src="../Images/icons/0033_a.gif"></p>
                </td>
                <td style="width: 277px">
                    <strong><font face="宋体">数字签名演示</font></strong>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 82px">
                    <font face="宋体">原文件</font>
                </td>
                <td style="width: 277px">
                    <font face="宋体">
                        <input style="width: 280px; height: 22px" type="file" size="27" id="txtFile"></font>
                </td>
                <td>
                    <input style="width: 152px; height: 25px" onclick="selectFile()" type="button" value="签名后上载文件...">
                </td>
            </tr>
            <tr>
                <td style="width: 82px; height: 107px">
                    <font face="宋体">签名结果</font>
                </td>
                <td style="width: 277px; height: 107px">
                    <font face="宋体">
                        <textarea id="SignValue" style="width: 280px; height: 109px" name="SignValue" rows="6"
                            cols="32" runat="server"></textarea></font>
                </td>
                <td style="height: 107px">
                    <p>
                        <font face="宋体">
                            <input style="width: 96px; height: 24px" type="button" value="进行签名" id="buttSign"
                                onclick="btnSignClick()"></font></p>
                    <p>
                        <font face="宋体"></font>&nbsp;</p>
                </td>
            </tr>
            <tr>
                <td style="width: 82px">
                    <font face="宋体"></font>
                </td>
                <td style="width: 277px">
                    <font face="宋体">
                        <p>
                            <font face="宋体">
                                <asp:Button ID="buttSave" name="buttSave" runat="server" Text="保存结果" OnClick="buttSave_Click">
                                </asp:Button></font></p>
                    </font>
                </td>
                <td>
                    <font face="宋体"></font>
                </td>
            </tr>
            <tr>
                <td style="width: 82px" colspan="3">
                    <font face="宋体">
                        <asp:Label ID="LabelMessage" runat="server" BorderStyle="Dotted" BorderWidth="1px"
                            Height="32px" Width="472px">Label</asp:Label></font>
                </td>
            </tr>
        </table>
    </p>
    <table id="Table2" cellspacing="1" cellpadding="1" width="300" border="0">
        <tr>
            <td>
                <asp:TextBox ID="TextBoxSignedFile" runat="server" Width="0px"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server" Height="152px" Width="0px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
    </table>
    </form>
    <object id="ThCrypt" height="1" width="1" classid="clsid:0D17F212-7827-4588-B0E9-827E356CDF5A"
        name="ThCrypt" visible="false" viewastext>
        <param name="_Version" value="65536">
        <param name="_ExtentX" value="26">
        <param name="_ExtentY" value="26">
        <param name="_StockProps" value="0">
    </object>
</body>
</html>
