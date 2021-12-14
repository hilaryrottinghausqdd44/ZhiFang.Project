<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InputParaEdit.aspx.cs" Inherits="OA.ModuleManage.InputParaEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <style type="text/css">
        #TextArea1
        {
            width: 598px;
            height: 43px;
        }
        #TextAreaPopUrl
        {
            height: 47px;
            width: 496px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <textarea id="TextAreaPopUrl" name="S1" rows="3"></textarea><br />
        <input id="Button1" type="button" value="确定" /></div>
    </form>
    <p>
        说明</p>
    <p>
        支持三种类型</p>
    <p>
        1, 模块弹出选择条目类型</p>
    <p>
&nbsp;&nbsp;&nbsp;&nbsp; 输入方式:输入弹出选择条目的程序模块地址 , [Module]</p>
    <p>
&nbsp;&nbsp;&nbsp;&nbsp; 类似：/Oa2008/documents/default_list_popnew.aspx</p>
    <p>
        2，模块提供函数类型</p>
    <p>
&nbsp;&nbsp;&nbsp; 输入方式：NameSpace+模块+方法,关键字段,显示描述名称</p>
    <p>
&nbsp;&nbsp;&nbsp; ,返回DataSet数据集</p>
    <p>
        &nbsp;&nbsp;&nbsp; 示例:&nbsp;OA.ModuleManage.ModuleParameterEdit.Function(), Function, 
        ID,CName</p>
    <p>
        3, 模块提供的外部Web服务</p>
    <p>
        &nbsp;&nbsp;&nbsp; 输入方式：WebService地址</p>
    <p>
        &nbsp;&nbsp;&nbsp; 示例:
        
        http://localhost/WebService/ws.asms,WebService,WSClass,FunctionName(),ID,CName</p>
</body>
</html>
