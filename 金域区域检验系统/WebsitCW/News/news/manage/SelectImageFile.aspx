<%@ Import Namespace="System.IO" %>

<%@ Page Language="c#" AutoEventWireup="True"
    Inherits="Labweb.SelectImageFile11" Codebehind="SelectImageFile.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>PublishImage</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <link rel="stylesheet" type="text/css" href="admin/ioffice.css">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">

    <script id="clientEventHandlersJS" language="javascript">
		function ReturnImg(img) {
			if(img=="")
			{
				alert("请选择一个图片");
				return;
			}
			var path='..' + '/<%=Request.QueryString["path"]%>/';
			path=path.replace('//','/');
			
			parent.window.returnValue = path +  img;
			parent.window.close();
		}

		function deleteTemplet(fileName)
		{
			if(confirm('真的要删除这个文件吗？\r\n一旦删除，该文件将不可恢复，确定没有指定过该图片吗？'))
			{
				document.all['Action'].value='删除';
				document.all['FileName'].value=fileName;
				Form1.submit();
			}
			return false;
		}
		
		function doTrans(imgObj) 
		{
			//imgObj=document.all[imgObj1];
			imgObj.filters[0].opacity=98;
		}
		
		
    </script>

</head>
<body language="javascript" bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0">
    <iframe frameborder="0" src="UploadImageFile.aspx?path=<%=Request.QueryString["path"]%>"
        style="width: 100%; height: 56px"></iframe>
    <form id="Form1" method="post" runat="server">
    请选择图片文件<br>
    <table border="1" cellspacing="5" cellpadding="1" width="90%">
        <tr>
            <%
                int pageSize = 8;
                if (Request.QueryString["pageSize"] != null)
                    pageSize = Convert.ToInt32(Request.QueryString["pageSize"]);
                int i = 0;
                if (dirInfo != null)
                {
                    foreach (FileInfo dirFile in dirInfo.GetFiles())
                    {
                        i++;
            %>
            <td align="left" valign="top">
                <table border="0" cellspacing="0" cellpadding="0" width="20" align="left" height="60">
                    <tr>
                        <td align="center" valign="top">
                            <img style="cursor: hand;" src="../../../images/icons/0014_b.gif" onclick="deleteTemplet('<%=dirFile.Name%>')">
                        </td>
                        <td align="left" valign="top">
                            <input style="cursor: hand;" type="button" value="删除" onclick="deleteTemplet('<%=dirFile.Name%>')">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <img id="a<%=i%>" style="cursor: hand; filter: progid:DXImageTransform.Microsoft.Alpha( style=0,opacity=35);"
                                src="<%Response.Write(Request.QueryString["path"]+dirFile.Name);%>" title="<%Response.Write(Request.QueryString["path"]+dirFile.Name);%>"
                                onmouseover="this.border=0;this.filters[0].opacity=98;" onmouseout="this.border=0; this.filters[0].opacity=35;"
                                border="0" onclick="ReturnImg('<%=dirFile.Name%>')">
                        </td>
                    </tr>
                </table>
            </td>
            <%
                if ((double)i / pageSize == (int)i / pageSize)
                    Response.Write("</tr><tr>");
                }
            }
            %>
        </tr>
    </table>
    <input name="Action" type="hidden" value="">
    <input name="FileName" type="hidden" value="">
    <input name="PicsName" type="hidden" value="">
    </form>
</body>
</html>
