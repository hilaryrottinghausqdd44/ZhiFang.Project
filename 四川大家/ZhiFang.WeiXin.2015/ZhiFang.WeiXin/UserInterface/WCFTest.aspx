<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WCFTest.aspx.cs" Inherits="ZhiFang.WeiXin.UserInterface.WCFTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WCFTest</title>
    <link href="easyui/demo/demo.css" rel="stylesheet" type="text/css" />
    <link href="easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="easyui/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <script src="easyui/jquery.min.js" type="text/javascript"></script>
    <script src="easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="easyui/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script language="javascript" charset="UTF-8" type="text/javascript">
        $(function () {
            $('#btn').bind('click', function () {
                $.ajax({
                    type: "POST",
                    url: "../DictionaryService.svc/ST_UDTO_AddBCountry",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: "{\"entity\": { \"Name\": \"111\",\"Code\":\"222\"}}",
                    success: function (data) {
                        if (data.BoolFlag) {
                            $.messager.alert('提示', '保存成功！', 'info');
                            $('#NRequestFormList').datagrid('reload');
                        }
                        else {
                            $.messager.alert('提示', '保存失败！ErrorInfo:' + data.ErrorInfo, 'error');
                        }
                    }
                });
            });

            $('#btn1').bind('click', function () {
                $.ajax({
                    type: "POST",
                    url: "../WeiXinAppService.svc/ST_UDTO_GetSearchAccountReportFormListById",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: "{\"ReportFormIndexIdList\": \"5450453722890199621\" }",
                    success: function (data) {
                        if (data.BoolFlag) {
                            $.messager.alert('提示', '保存成功！', 'info');
                            $('#NRequestFormList').datagrid('reload');
                        }
                        else {
                            $.messager.alert('提示', '保存失败！ErrorInfo:' + data.ErrorInfo, 'error');
                        }
                    }
                });
            });

            $('#userlist').datagrid({
                url: '../ServiceWCF/NRequestFromService.svc/GetNRequestFromListByRBAC?guid=' + generateMixed(10)
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click" />
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
    <asp:Button ID="Button2" runat="server" Text="Button" onclick="Button2_Click" />
    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
    <asp:Button ID="Button3" runat="server" Text="Button" onclick="Button3_Click" />
    <div id="p" class="easyui-panel" title="My Panel"     
        style="width:500px;height:150px;padding:10px;background:#fafafa;"   
        data-options="iconCls:'icon-save',closable:true,    
                collapsible:true,minimizable:true,maximizable:true">   
    <p>国家名称：<input id="CountryName" class="easyui-textbox" style="width:300px"/> </p>   
    <p>国家代码：<input id="CountryCode" class="easyui-textbox" style="width:300px"/></p>
    <p><a id="btn" href="#" class="easyui-linkbutton">easyui</a>  </p>
    <p><a id="btn1" href="#" class="easyui-linkbutton">easyui</a>  </p>
    <p><a id="A1" href="#" class="easyui-linkbutton">easyui</a>  </p>   
</div> 
<div id="Div1" class="easyui-panel" title="My Panel"     
        style="width:500px;height:350px;padding:10px;background:#fafafa;"   
        data-options="iconCls:'icon-save',closable:true,    
                collapsible:true,minimizable:true,maximizable:true">   
   
    <p><a id="A2" href="#" class="easyui-linkbutton">easyui</a>  </p> 
    <table id="userlist" class="easyui-datagrid" style="width:400px;height:250px"   
        data-options="fitColumns:true,singleSelect:true">   
    <thead>   
        <tr>   
            <th data-options="field:'code',width:100">编码</th>   
            <th data-options="field:'name',width:100">名称</th>   
            <th data-options="field:'price',width:100,align:'right'">价格</th>   
            
        </tr>   
    </thead>   
</table>
</div>
    </form>
</body>
</html>
