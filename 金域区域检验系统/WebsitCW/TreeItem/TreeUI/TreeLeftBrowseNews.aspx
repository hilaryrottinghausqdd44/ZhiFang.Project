<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TreeLeftBrowseNews.aspx.cs"
    Inherits="OA.TreeItem.TreeUI.TreeLeftBrowseNews" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
  <script type="text/javascript">
    //得到名称为posy的cookie值
    function GetCookie(name)
    { 
        var arg = name + "="; 
        var alen = arg.length; 
        var clen = document.cookie.length; 
        var i = 0; 
        while(i < clen)
        { 
            var j = i + alen; 
            //判断是否是所指定的key
            if(document.cookie.substring(i,j) == arg) 
            return getCookieVal(j); 
            i = document.cookie.indexOf("   ",i)+1; 
            if(i == 0)
            break;    
        } 
        return null; 
    } 
    //得到key的值value 
    function getCookieVal(offset)
    { 
        var endstr = document.cookie.indexOf(";",offset); 
        if (endstr == -1) 
        endstr = document.cookie.length; 
        return unescape(document.cookie.substring(offset,endstr)); 
    } 
    //设置当前滑动坐标值到缓存
    function SetCookie(name,value)
     {        
       document.cookie = name + "=" + escape(value) 
     }
   </script>


</head>
<body onload="document.documentElement.scrollTop=GetCookie('posy')" onunload="SetCookie('posy',document.documentElement.scrollTop)">
    <form id="formhidden" runat="server" method="post">
    <input id="btn_click" type="hidden" runat="server" name="btn_click" />
    <table height="226" cellpadding="0" cellspacing="0" style="width: 797px">
        <tr>
            <td valign="top" width="350px"> 
                 <asp:TreeView ID="tvMenu" runat="server" ImageSet="Custom" ShowLines="true" Width="100%"
                        ExpandDepth="0" Target="middle">
                        <ParentNodeStyle Font-Bold="False" />
                        <HoverNodeStyle Font-Underline="false" ForeColor="Purple" />
                        <SelectedNodeStyle Font-Underline="true" HorizontalPadding="0px" VerticalPadding="0px" />
                        <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="DarkBlue" HorizontalPadding="5px"
                            NodeSpacing="0px" VerticalPadding="0px" />
                    </asp:TreeView>          
            </td>     
        <td  valign="top">   
            <iframe name="treeframe" scrolling="AUTO" src="TreeBrowseNews.aspx" frameborder="YES" width="600" height="550"
                 >
        </td>
        </tr>
    </table>
    </form>
</body>
</html>
