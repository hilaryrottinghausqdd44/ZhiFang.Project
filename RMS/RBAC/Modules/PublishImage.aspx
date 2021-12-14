<%@ Import Namespace="System.IO" %>

<%@ Page Language="c#" AutoEventWireup="True" Inherits="ortronics.Config.PublishImage" Codebehind="PublishImage.aspx.cs" %>

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
    <iframe frameborder="0" src="UploadImg.aspx" style="width: 640px; height: 25px">
    </iframe>
    <input id="buttConfirm" type="button" value="确定" onclick="ReturnImg()" title="" disabled>&nbsp;
    请选择要显示的图片
    <table width="300" border="0" cellspacing="5" cellpadding="1">
        <tr>
            <%
                int i = 0;
                foreach (FileInfo dirFile in dirInfo.GetFiles())
                {
                    i++;
            %>
            <td>
                <input type="radio" id="RadioImg<%=i%>" name="RadioImg" value="<%=dirFile.Name%>"
                    onclick="javascript:setimg('<%=dirFile.Name%>')" title="<%=dirFile.Name%>">
            </td>
            <td style="border-right: #99cccc 1px solid; border-top: #99cccc 1px solid; border-left: #99cccc 1px solid;
                border-bottom: #99cccc 1px solid">
                <img src="../products/images/<%=dirFile.Name%>" style="border-right: #ff6633 1px dotted;
                    border-top: #ff6633 1px dotted; border-left: #ff6633 1px dotted; border-bottom: #ff6633 1px dotted"
                    title="<%=dirFile.Name%>">
            </td>
            <%
                if ((double)i / 4 == (int)i / 4)
                    Response.Write("</tr><tr>");
            }%>
        </tr>
    </table>
</body>
</html>
