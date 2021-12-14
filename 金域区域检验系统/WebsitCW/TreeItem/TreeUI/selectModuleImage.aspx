<%@ Page language="c#" Codebehind="selectModuleImage.aspx.cs" AutoEventWireup="true" Inherits="TreeItem.TreeUI.selectModuleImage" %>
<%@ Import Namespace="System.IO" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
	<head runat="server">
		<title>PublishImage</title>
		<script id="clientEventHandlersJS" type="text/javascript">
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
		</script>
	</head>
	<body>
		<input id="buttConfirm" type="button" value="确定" onclick="ReturnImg()" title="" disabled />&nbsp; 
		请选择要显示的图片
		<table width="300" border="0" cellspacing="5" cellpadding="1">
			<tr>
				<%
			int i=0;
			foreach (FileInfo dirFile in dirInfo.GetFiles())
			{i++;
			%>
				<td><input type="radio" id="RadioImg<%=i%>" name="RadioImg" value="<%=dirFile.Name%>"
					onclick="javascript:setimg('<%=dirFile.Name%>')" title="<%=dirFile.Name%>" /></td>
				<td style="BORDER-RIGHT: #99cccc 1px solid; BORDER-TOP: #99cccc 1px solid; BORDER-LEFT: #99cccc 1px solid; BORDER-BOTTOM: #99cccc 1px solid"><img alt="" src="../../App_Themes/Images/icons/<%=dirFile.Name%>" 
				style="BORDER-RIGHT: #ff6633 1px dotted; BORDER-TOP: #ff6633 1px dotted; BORDER-LEFT: #ff6633 1px dotted; BORDER-BOTTOM: #ff6633 1px dotted"
				title="<%=dirFile.Name%>" /></td>
				<%
			if((double)i/8==(int)i/8)
				Response.Write("</tr><tr>");
			}%>
			</tr>
		</table>
	</body>
</html>
