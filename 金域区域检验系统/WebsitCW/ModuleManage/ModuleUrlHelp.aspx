<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModuleUrlHelp.aspx.cs" Inherits="OA.ModuleManage.ModuleUrlHelp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <p>
        <b>插入链接(Link)或框架(iFrame)是的配置说明</b></p>
    <p>
        1</p>
    <p>
        2</p>
    <p>
        3</p>
    <p>
        4</p>
    <p>
        5 刷新其他窗口配置说明：<br />
&nbsp;&nbsp; 
        frm1,/2008/doc/NewsText.aspx,NewsID,{新闻编号:29};[height=500:width=600:status=yes:toolbar=no:menubar=no:location=no],/2008/doc/NewsText.aspx?sf=p&amp;i=p,NewsID,{新闻标题};</p>
    <p>
        &nbsp; 格式： 同界面1(frmID1),同界面1url,Name1,{string1}; 
        同界面2(frmID2),同界面2url,Name2,{string2}; [_Blank],弹出界面url,ID1,{string1}<br />
&nbsp;注意：如果frmID1=frmID2, 则判断只打开一个窗口。</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
