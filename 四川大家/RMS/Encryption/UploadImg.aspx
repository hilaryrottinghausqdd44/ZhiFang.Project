<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.Encryption.UploadImg" Codebehind="UploadImg.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>UploadImg</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
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

</head>
<body ms_positioning="GridLayout" topmargin="0">
    <form id="frmContent" name="frmContent" method="post" enctype="multipart/form-data"
    runat="server">
    上载文件：<input class="btnFile" id="fileUpload" style="border-right: #cccccc 1px solid;
        border-top: #cccccc 1px solid; border-left: #cccccc 1px solid; width: 344px;
        border-bottom: #cccccc 1px solid; height: 22px" type="file" size="38" name="fileUpload"
        runat="server">
    <input id="imgButton" onmouseover="javascript:this.src='images/btnOK_over.gif'" onclick="javascript:__doBeforeSubmit2();"
        onmouseout="javascript:this.src='images/btnOK.gif'" type="image" alt="上载文件" src="images/btnOK.gif"
        align="absMiddle" name="imgButton" language="javascript">
    <asp:Label ID="lblMessage" Style="z-index: 101; left: 16px; position: absolute; top: 32px"
        runat="server" ForeColor="Red" Width="104px"></asp:Label>
    </form>
</body>
</html>
