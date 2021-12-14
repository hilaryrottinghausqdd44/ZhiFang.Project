<%@ Import Namespace="System.IO" %>

<%@ Page Language="c#" AutoEventWireup="True"
    Inherits="OA.Encryption.selectModuleImage" Codebehind="selectModuleImage.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>PublishImage</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">

    <script id="clientEventHandlersJS" language="javascript">
<!--

function ReturnImg() {
	if(buttConfirm.title=="")
	{
		alert("请选择一个图片");
		return;
	}
	window.returnValue = buttConfirm.title;
	window.close();
}

function setimg(img)
{
	buttConfirm.title=img;
	buttConfirm.disabled=false;
}


//-->
    </script>

</head>
<body language="javascript">
    <iframe frameborder="0" src="UploadImg.aspx?path=<%=Request.QueryString["path"]%>"
        style="width: 558px; height: 56px"></iframe>
    <br>
    <input id="buttConfirm" type="button" value="确定" onclick="ReturnImg()" title="" disabled>&nbsp;
    请选择要操作的文件
    <table border="1" cellspacing="5" cellpadding="1">
        <tr>
            <%
                int i = 0;
                if (dirInfo != null)
                {
                    foreach (FileInfo dirFile in dirInfo.GetFiles())
                    {
                        i++;
            %>
            <td>
                <input type="radio" id="RadioImg<%=i%>" name="RadioImg" value="<%=dirFile.Name%>"
                    onclick="javascript:setimg('<%=dirFile.Name%>')" title="<%=dirFile.Name%>">
            </td>
            <td>
                <img style="cursor: hand" src="../Documents/images/file16.gif" title="<%=dirFile.Name%>"
                    onclick="window.returnValue = '<%=dirFile.Name%>';	window.close();">
            </td>
            <td width="300" style="cursor: hand" onclick="window.returnValue = '<%=dirFile.Name%>';	window.close();">
                <%=dirFile.Name%>
            </td>
            <%
                if ((double)i / 2 == (int)i / 2)
                    Response.Write("</tr><tr>");
                }
            }
            %>
        </tr>
    </table>
</body>
</html>
