<%@ Page language="c#" AutoEventWireup="True" Inherits="Labweb.UploadImageFile" Codebehind="UploadImageFile.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>UploadImg</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<LINK rel="stylesheet" type="text/css" href="css/ioffice.css">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script id="clientEventHandlersJS" language="javascript">
<!--

function __doBeforeSubmit2()
{
	var bSucceeded = true;
	var strDir = trim_string(frmContent.fileUpload.value);

	if (strDir.length==0)
	{
		frmContent.fileUpload.style.borderColor = "red";
		bSucceeded = false;
	}
	if (!bSucceeded)
	{
		event.returnValue = false;
		event.cancelBubble = true;
	}
	else
	{
		event.returnValue = false;
		event.cancelBubble = true;
		frmContent.action.value = "UploadFile";
		frmContent.submit();
	}
}
function trim_string(strOriginalValue) 
{
	var ichar, icount;
	var strValue = strOriginalValue;
	ichar = strValue.length - 1;
	icount = -1;
	while (strValue.charAt(ichar)==' ' && ichar > icount)
		--ichar;
	if (ichar!=(strValue.length-1))
		strValue = strValue.slice(0,ichar+1);
	ichar = 0;
	icount = strValue.length - 1;
	while (strValue.charAt(ichar)==' ' && ichar < icount)
		++ichar;
	if (ichar!=0)
		strValue = strValue.slice(ichar,strValue.length);
	return strValue;
}


//-->
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" topmargin="0">
		<form id="frmContent" name="frmContent" method="post" encType="multipart/form-data" runat="server">
			说明：只能上传图片格式文件<br>
			上载文件：<INPUT class="btnFile" id="fileUpload" style="BORDER-RIGHT: #cccccc 1px solid; BORDER-TOP: #cccccc 1px solid; BORDER-LEFT: #cccccc 1px solid; WIDTH: 344px; BORDER-BOTTOM: #cccccc 1px solid; HEIGHT: 22px"
				type="file" size="38" name="fileUpload" runat="server"> <INPUT id="imgButton" onmouseover="javascript:this.src='images/btnOK_over.gif'" onclick="javascript:__doBeforeSubmit2();"
				onmouseout="javascript:this.src='images/btnOK.gif'" type="image" alt="上载文件" src="images/btnOK.gif" align="absMiddle" name="imgButton"
				language="javascript">
			<asp:Label id="lblMessage" style="Z-INDEX: 101; LEFT: 16px; POSITION: absolute; TOP: 32px"
				runat="server" ForeColor="Red" Width="104px"></asp:Label>
		</form>
	</body>
</HTML>
