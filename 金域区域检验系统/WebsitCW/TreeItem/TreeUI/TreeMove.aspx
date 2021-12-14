<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TreeMove.aspx.cs" Inherits="TreeItem.TreeUI.TreeMove" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
   <script type="text/javascript" language="javascript"> 
   var flag = 1; 
  //识别不同的浏览器 
  function getTargetElement(evt) { 
 
    var elem 
    if (evt.target) 
    { 
      elem = (evt.target.nodeType == 3) ? evt.target.parentNode : evt.target 
    }  
    else  
    { 
      elem = evt.srcElement 
    } 
    return elem 
  } 

//去掉字符串左右空格
  String.prototype.Trim = function() 
 { 
    return this.replace(/(^\s*)|(\s*$)/g, ""); 
 } 
  function OnClientTreeNodeClick(evt) 
  { 
    evt = (evt) ? evt : ((window.event) ? window.event : ""); 
    if(evt == "") 
    { 
       return; 
    } 
    var obj = getTargetElement(evt); 
    if(obj.tagName) 
    { 
       var TreeView = "<%=tvMenu.ClientID %>" ;//取得TREEVIEW的客户端ID
       //判断所要选择的区间
       if(obj.tagName == "A" && obj.id.substr(0,TreeView.length) == TreeView) 
       {
            if(flag == 0)
            {//选择节点后       
               var nodeafter = obj.title;//第二次选择ID的值
               var nodestart = document.getElementById('treeid').value;       
               if(nodeafter != nodestart)
               {
                  TreeItem.TreeUI.TreeMove.MoveNode(nodestart.Trim(),nodeafter.Trim(),Getradiovalue(),GetCallresult);
               }
               else
               {
                  alert('对不起,节点没做任何移动');
               }
               dodo();
            }
            else
            {//未选节点时
               divtemp.innerText = obj.innerText;//第一次选择ID的值                
               document.getElementById('treeid').value = obj.title;                   
               document.body.onmousemove = doit;
               divtemp.style.display = "";
               flag = 0;
            }
         }      
    } 
  } 
  //回调结果
  function GetCallresult(result)
  {
      var r = result.value;
      if(r == "1")
      {
         //alert('成功');
         window.location.href = window.location.href;
      }
      if(r == "2")
      {
         alert('父节点不能移动到子节点');
      }
  } 
  //div的坐标与鼠标坐跟随
  function doit()
 {
     if(flag == 0)
     {
        //var obj = document.getElementById("divtemp");
        divtemp.style.display = "";
        divtemp.style.left = document.documentElement.scrollLeft+event.clientX+10;
        divtemp.style.top = document.documentElement.scrollTop+event.clientY+10;
      }
 }
  //隐藏DIV并置空,重新置标志为1
  function dodo()
  {
     flag = 1;
     divtemp.innerText = "";
     divtemp.style.display = "none";
  } 
  //获取RadioButtonList的选中值
   function Getradiovalue()
   {
        var result = '0';
        //取得子类个数
        var num = document.getElementById("rdbtype").rows.length;
        for(var i=0;i<num;i++)
        {
           var name = "rdbtype_"+i;//名称      
           if(document.getElementById(name).checked)  //注意checked不能写成Checked，要不然不成功
           {
                result = document.getElementById(name).value;
                break;
           } 
        }    
        return result;
  }
  </script> 
</head>
<body ondblclick="dodo();">
    <form id="form1" runat="server">
    <asp:RadioButtonList runat="server" ID="rdbtype">
      <asp:ListItem Value="2" Selected="True">节点前</asp:ListItem>
      <asp:ListItem Value="0">节点后</asp:ListItem>
      <asp:ListItem Value="1">子节点</asp:ListItem>
    </asp:RadioButtonList>
    <asp:TreeView ID="tvMenu" runat="server" Width="100%" ImageSet="Arrows" ShowLines="True">
        <ParentNodeStyle Font-Bold="False" />
        <HoverNodeStyle Font-Underline="True" ForeColor="Purple" />
        <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" VerticalPadding="0px" />
        <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="DarkBlue" HorizontalPadding="5px"
            NodeSpacing="0px" VerticalPadding="0px" />
    </asp:TreeView>
    <div id="hover" style="display: none; padding: 0px; margin: 0px; width: 10px; height: 10px;
        left: 0px; top: 0px; background-color: #fff0ff; position: absolute; z-index: 100;">
    </div>
    <div id="divtemp" style="BORDER-RIGHT: #ffffcc 1px double; BORDER-TOP: #ffffcc 1px double; DISPLAY: none; FONT-SIZE: 12pt; BORDER-LEFT: #ffffcc 1px double; COLOR: #0000ff; BORDER-BOTTOM: #ffffcc 1px double; POSITION: absolute; BACKGROUND-COLOR: #00ffff">你好</div>
    <input type="hidden" id="treeid" />
    </form>
 
    

</body>
</html>
