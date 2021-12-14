<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TemplateList.aspx.cs" Inherits="ZhiFang.WebLis.ReportPrint.TemplateList" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>模版列表</title>
        <script src="../jquery-easyui-1.3/jquery-1.7.2.min.js" type="text/javascript"></script>
        <script src="../jquery-easyui-1.3/jquery.easyui.min.js" type="text/javascript"></script>
        <script src="../jquery-easyui-1.3/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
        <link href="../jquery-easyui-1.3/themes/default/easyui.css" rel="stylesheet" type="text/css" />
        <link href="../jquery-easyui-1.3/themes/icon.css" rel="stylesheet" type="text/css" />
        <script language="javascript" type="text/javascript">
        </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div style="font-size:30px;font-weight:bold;text-align:center" >模版列表</div>
    <table class="easyui-datagrid" title="Basic DataGrid" style="width:1000px;height:600px;margin:100px;padding-left:100px"
    data-options="singleSelect:true,fitColumns:true,url:'../Ashx/ReportSystem.ashx',method:'get', rowStyler: function(index,row){if (index%2 >0){return 'background-color:#C1E0FF;';}}">
    <thead>
    <tr>
    <th data-options="field:'PrintFormatName',width:100">模板名称</th>
    <th data-options="field:'PintFormatAddress',width:150">模板存放地址</th>
    <th data-options="field:'PintFormatFileName',width:180">模板文件名</th>
    <th data-options="field:'ItemParaLineNum',width:50,align:'center'">项目数</th>
    <th data-options="field:'PaperSize',width:100,align:'center'">纸张大小</th>
    <th data-options="field:'PrintFormatDesc',width:200">模板描述</th>
    <th data-options="field:'BatchPrint',width:100,align:'center',formatter:formatBatch">套打标志</th>
    </tr>
    </thead>
    </table>
    </div>
    </form>
    <script>
        function formatBatch(val, row) {
            if (val == 0) {
                return '是';
            } else {
                return  '否';
            }
        }
</script>
</body>
</html>
