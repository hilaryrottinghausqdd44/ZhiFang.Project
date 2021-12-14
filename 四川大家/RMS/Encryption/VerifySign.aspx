<%@ Page Language="c#" validateRequest="false" enableEventValidation="false" AutoEventWireup="True"
    Inherits="OA.Encryption.VerifySign" Codebehind="VerifySign.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>VerifySign</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">

    <script language="javascript" id="clientEventHandlersJS">
	<!--

		function window_onload() {
			document.forms[0].imgPassed.style.display="none";
			document.forms[0].imgRefused.style.display="none";
		}

		function selectFile(path)
		{
			/*
			r=window.showModalDialog('selectModuleImage.aspx?path=' + path,'','dialogWidth:588px;dialogHeight:618px;help:no;scroll:auto;status:no');
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
			*/
		}
		function selectFileSignedFile(path)
		{
			r=window.showModalDialog('selectModuleImage.aspx?path=' + path,'','dialogWidth:588px;dialogHeight:618px;help:no;scroll:auto;status:no');
			if (r == '' || typeof(r) == 'undefined')
			{
				return;
			}
			else
			{
				document.all["txtSignFile"].value=r;
				document.all["txtSignFile"].disabled=false;
				Form1.submit();
			}
		}
		
		function selectFilePublicKey(path)
		{
			r=window.showModalDialog('selectModuleImage.aspx?path=' + path,'','dialogWidth:588px;dialogHeight:618px;help:no;scroll:auto;status:no');
			if (r == '' || typeof(r) == 'undefined')
			{
				return;
			}
			else
			{
				document.all["txtPublicKey"].value=r;
				document.all["txtPublicKey"].disabled=false;
				Form1.submit();
			}
		}
		
		function btnVerifyClick()
		{
			document.forms[0].imgPassed.style.display="none";
			document.forms[0].imgRefused.style.display="";
			document.forms[0].imgDefault.style.display="none";
			
			var sInfo = ThCrypt.VerifyFile(document.forms[0].txtFile.value,document.forms[0].SignValue.value,document.forms[0].publicKey.value);
			if (sInfo==1)
			{
				document.forms[0].VResult.value = "签名有效!";
				document.forms[0].imgPassed.style.display="";
				document.forms[0].imgRefused.style.display="none";
				document.forms[0].imgDefault.style.display="none";
			}
			else if(sInfo==0)
				document.forms[0].VResult.value = "签名无效!";
			else if(sInfo==-1)
				document.forms[0].VResult.value = "参数不全!";
			else if(sInfo==-2)
				document.forms[0].VResult.value = "证书不可用!";
			else
				document.forms[0].VResult.value = "无法验证!";

		}
		
		function viewFile(id,path)
		{
			if(document.all[id]!=null)
			{
				if(document.all[id].value=='')
				{
					alert('请先选择文件');
					return;
				}
					
				window.open(path + document.all[id].value,'_blank');
			}
		}
	//-->
    </script>

</head>
<body language="javascript" onload="return window_onload()">
    <form id="Form1" method="post" runat="server">
    <p>
        <table id="Table1" style="width: 600px; height: 200px" cellspacing="1" cellpadding="1"
            width="600" border="0">
            <tr>
                <td style="width: 85px; height: 46px">
                    <p align="center">
                        <img src="../Images/icons/0060_a.gif"></p>
                </td>
                <td style="width: 284px; height: 46px">
                    <strong>签名验证</strong>
                </td>
                <td style="height: 46px">
                    <input style="display: none; width: 208px; height: 25px" onclick="selectFile('data/')"
                        type="button" value="下载签名原文件...">
                </td>
            </tr>
            <tr>
                <td style="width: 85px; height: 47px">
                    <font face="宋体">签名结果</font>
                </td>
                <td style="width: 284px; height: 47px">
                    <p>
                        <textarea id="SignValue" style="width: 280px; height: 109px" name="SignValue" rows="6"
                            cols="32" runat="server"></textarea>&nbsp;</p>
                </td>
                <td style="height: 47px">
                    <font face="宋体">
                        <input style="width: 96px; height: 25px" onclick="selectFileSignedFile('SignedFile/')"
                            type="button" value="选择文件...">&nbsp;<input type="button" value="查看文件..." onclick="viewFile('txtSignFile','SignedFile/')"
                                style="width: 104px; height: 25px"></font>
                </td>
            </tr>
            <tr>
                <td style="width: 85px; height: 42px">
                    <font face="宋体">公钥证书</font>
                </td>
                <td style="width: 284px; height: 42px">
                    <font face="宋体">
                        <textarea id="publicKey" style="width: 280px; height: 109px" name="SignValue" rows="6"
                            cols="32" runat="server"></textarea></font>
                </td>
                <td style="height: 42px">
                    <font face="宋体">
                        <input style="width: 96px; height: 25px" onclick="selectFilePublicKey('PublicKey/')"
                            type="button" value="选择文件...">&nbsp;<input type="button" value="查看文件..." onclick="viewFile('txtPublicKey','PublicKey/')"
                                style="width: 107px; height: 25px"></font>
                </td>
            </tr>
            <tr>
                <td style="width: 85px; height: 24px">
                    <font face="宋体">原文件</font>&nbsp;
                </td>
                <td style="width: 284px; height: 24px" colspan="2">
                    <font face="宋体">
                        <input id="txtFile" style="width: 504px; height: 25px" type="file" size="64" name="txtFile"
                            runat="server"></font>
                </td>
            </tr>
            <tr>
                <td style="width: 85px">
                    <p align="center">
                        <img src="../Images/icons/0060_b.gif"></p>
                </td>
                <td style="width: 284px">
                    <input id="buttVerify" onclick="btnVerifyClick();" type="button" value="验证" name="buttVerify"><font
                        face="宋体">&nbsp;<input id="VResult" style="border-right: #0066ff 1px solid; border-top: #0066ff 1px solid;
                            border-left: #0066ff 1px solid; width: 224px; border-bottom: #0066ff 1px solid;
                            height: 20px" type="text" size="32"></font>
                </td>
                <td>
                    <font face="宋体"></font>
                </td>
            </tr>
            <tr>
                <td style="width: 85px" nowrap colspan="2">
                    <img id="imgDefault" src="Images/zhifang.gif"><img id="imgPassed" src="Images/passed.gif"><img
                        id="imgRefused" src="Images/unpassed.gif">
                </td>
                <td>
                    <font face="宋体"></font>
                </td>
            </tr>
            <tr>
                <td style="width: 85px" colspan="2">
                    <asp:TextBox ID="TextBox1" runat="server" Height="152px" Width="0px" TextMode="MultiLine"></asp:TextBox>
                </td>
                <td>
                    <p>
                        <input id="txtSignFile" style="width: 0px; height: 0px; background-color: #d5d5ff"
                            type="text" size="41" name="txtFile" runat="server"></p>
                    <p>
                        <input id="txtPublicKey" style="width: 0px; height: 0px; background-color: #d5d5ff"
                            type="text" size="41" name="txtFile" runat="server"></p>
                </td>
            </tr>
        </table>
    </p>
    </form>
    <object id="ThCrypt1" height="1" width="1" classid="clsid:0D17F212-7827-4588-B0E9-827E356CDF5A"
        name="ThCrypt" visible="false">
        <param name="_Version" value="65536">
        <param name="_ExtentX" value="26">
        <param name="_ExtentY" value="26">
        <param name="_StockProps" value="0">
    </object>
</body>
</html>
