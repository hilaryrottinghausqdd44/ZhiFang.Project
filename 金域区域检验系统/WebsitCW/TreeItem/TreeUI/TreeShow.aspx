<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TreeShow.aspx.cs" Inherits="TreeItem.TreeUI.TreeShow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>选择树</title>
    <script src="../../Includes/js/JsModuleDist.js" type="text/javascript"></script>
    <script type="text/javascript">
     //树节点编号,树节点名称,树节点类型
     function GetTreeNodeIdAndName(treenodeid,treenodename,treenodetypeid,isrefreshframe)
     {
        //alert(treenodeid+','+treenodename+','+treenodetypeid);
        $('txttreenodeid').value = treenodeid;
        $('txttreenodename').value = treenodename;
        $('txttreenodetypeid').value = treenodetypeid;
        var pamlist = new Array;
        pamlist = treenodetypeid.split("#");
        //pamlist:节点类型编号#节点类型名称#输出值
        var pam1 = $('txtModuleArgPrv').value;//1
        var unionname3 = '节点编号,节点名称';//3
        var unionnamevalue4 = treenodeid+','+treenodename;//4       
        if(pamlist[0])
        {
           unionname3 = unionname3 + ',节点类型编号';
           unionnamevalue4 = unionnamevalue4+','+pamlist[0];
        } 
        if(pamlist[1])
        {
           unionname3 = unionname3 + ',节点类型名称';
           unionnamevalue4 = unionnamevalue4+','+pamlist[1];
        } 
        if(pamlist[3])
        {
           unionname3 = unionname3 + ',输出值1';
           unionnamevalue4 = unionnamevalue4+','+pamlist[3];
        }       
        if(pamlist[4])
        {
           unionname3 = unionname3 + ',输出值2';
           unionnamevalue4 = unionnamevalue4+','+pamlist[4];
        }
        if(pamlist[5])
        {
           unionname3 = unionname3 + ',输出值3';
           unionnamevalue4 = unionnamevalue4+','+pamlist[5];
        }        
        //alert(unionname3 +'<br>'+unionnamevalue4);
        var outpam5 = $('txtOutPutParaUrl').value;//5  
        //alert(pam1 + '||' + outpam5);
        //是否刷新输出框架
        if(isrefreshframe == "1")
        {        
            if(unionname3.length > 0 && outpam5.length > 0 && pamlist[3].length > 0)
            {   
               ModuleDist(pam1,'',unionname3,unionnamevalue4,outpam5,'','',false);
            }
        }
        else
        {        
           //根据链接地址刷新指定框架
           if(pamlist[2].length > 0)
           {
               var txtframe = $('txtrefreshframe').value
               //刷新指定的框架
               if(txtframe.length > 0)
               {
                  window.parent.frames[txtframe].location = pamlist[2]; 
               }
               else               
               {
                  alert('请指定要刷新的框架');
               }                        
           }
           else
           {  //当链接地址为空时
              var txtframe = $('txtrefreshframe').value
               //刷新指定的框架 根据新闻标题
               if(txtframe.length > 0)
               {                  
                  //根据标题在指定框架下打个新闻
                  window.parent.frames[txtframe].location = "../../modulemanage/ModuleParameterAdd.aspx?title="+treenodename; 
               } 
               else               
               {
                  alert('请指定要刷新的框架');
               }      
           }
        }
     }       
     function $(s)
    {
        return document.getElementById?document.getElementById(s):document.all[s];
    }
    </script>
    <style type="text/css">
        .hideClass
        {
            display: none;
        }       
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:TextBox ID="txttreenodeid" CssClass="hideClass" runat="server" ToolTip="选中树节点编号"></asp:TextBox>
    <asp:TextBox ID="txttreenodename" CssClass="hideClass" runat="server" ToolTip="选中树节点名称"></asp:TextBox>
    <asp:TextBox ID="txttreenodetypeid" CssClass="hideClass" runat="server" ToolTip="选中树节点类型"></asp:TextBox>
    
    <asp:TextBox ID="txtModuleArgPrv" CssClass="hideClass" runat="server" ToolTip="模块编号,模块名称"></asp:TextBox>
    <asp:TextBox ID="txtOutPutParaUrl" CssClass="hideClass" runat="server" ToolTip="输出参数"></asp:TextBox>
    <asp:TextBox ID="txtrefreshframe" CssClass="hideClass" runat="server" ToolTip="刷新框架值"></asp:TextBox>
    </div>
    </form>
</body>
</html>
