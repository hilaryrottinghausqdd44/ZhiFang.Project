<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TreeItem._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <script type="text/javascript">
       function openchild(subWindow,subWidth,subHeight)
		{
			var receiver = window.showModalDialog(subWindow,window,"dialogWidth=" + subWidth + "px;dialogHeight=" + subHeight + "px;help:no;status:no");
			if(receiver)
			{
				document.getElementById('txttest').value = receiver;				
			}
			else
			{
				//alert('没有接收到父窗体的值');
			}
		}
		function kan()
		{
		   var pam = document.getElementById('txttest').value
		   if(pam.length > 0)
		   {
		      var url = "treeui/treeshow.aspx?ids="+pam;
		      window.open(url);
		   }
		   else
		   {
		      alert('请选择参数');
		   }
		}
        function $(s)
        {
            return document.getElementById?document.getElementById(s):document.all[s];
        }
    </script>
</head>
<body>
    <form id="form1" method="post" runat="server">
    
    <a href="TreeUI/Index.aspx" target="_blank">树维护</a> <a href="TreeUI/TreeMove.aspx" target="_blank">树移动</a> <a href="TreeUI/TreeType.aspx" target="_blank">树类型</a> <a href="Test/testuc.aspx" target="_blank">树示例</a>
    
    
    <br /><br />
    <input type="text" id="txttest" />
    <input type="button" id="btnchange" onclick="openchild('TreeUI/TreeSelect.aspx',600,500);" value="选择" />
    <input type="button" id="btnkan" onclick="kan();" value="浏览" />
    <br />
    <asp:TextBox runat="server" ID="txtf"></asp:TextBox>
    <asp:TextBox runat="server" ID="txts"></asp:TextBox>
    <asp:Button runat="server" ID="btnjudge" Text="对比" onclick="btnjudge_Click"/>
    <br />
    <iframe name="leftFrame" scrolling="AUTO" style="width:200px;height:600px" src="<%=transurl%>" frameborder="YES" bordercolor="#FFE6BF" borderColorDark="#ffffff" bgColor="#fff3e1" borderColorLight="#ffb766">
	<iframe name="rightframe" frameborder="YES" style="width:600px;height:600px" scrolling="AUTO" bordercolor="#ffffff">
	<iframe id="eWebEditor1" src="/oa2008/includes/eWebEditor/ewebeditor.htm?id=content1&style=standard500"
                        frameborder="0" width="100%" scrolling="no" height="100%"></iframe>
                        <input id="content1" type="hidden" name="content1">
                        <asp:Button runat="server" ID="btntest" Text="test" 
        onclick="btntest_Click" />
    </form>
</body>
</html>
