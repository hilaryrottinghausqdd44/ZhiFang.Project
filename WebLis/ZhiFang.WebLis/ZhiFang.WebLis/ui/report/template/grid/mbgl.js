var rows = [];
var url;
//按钮类型
var btnType;
//默认页码
var delCount = 0;
var p;
$(function () {
   
    $('#dg').datagrid({
       // tools: '#myTool',
        loadMsg: '数据加载中...',
        fit: true,
        fitColumns: true,
        title:'报告模板管理',
        toolbar:"#toolbar",
        singleSelect: false,
        border:false,
        pagination: true,
        rownumbers: true,
        collapsible: false,
        idField:'Id',
        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetAllReportModelManage',
        method: 'get',
        striped: true,
        columns:[[
                { field: 'cb', checkbox: 'true' },
                { field: 'Id', title: 'ID', hidden: true },
                { field: 'PrintFormatName', width: setWidth(0.2),title: '模板名称' },
                { field: 'PintFormatAddress', width: setWidth(0.2), title: '模板存放地址' },
                { field: 'PintFormatFileName', width: setWidth(0.1), title: '模板文件名' },
                { field: 'ItemParaLineNum', width: setWidth(0.1), title: '项目或图片数' },
                { field: 'PaperSize', width: setWidth(0.1), title: '纸张大小' },
                { field: 'PrintFormatDesc', width: setWidth(0.1), title: '模板描述' },
                {
                    field: 'BatchPrint', width: setWidth(0.1), title: '套打标志', formatter: function (value, row, index) {
                        if (value==0) {
                          return  row.BatchPrint = '否';
                        } else {
                           return row.BatchPrint = '是';
                        }
                    }
                },
                { field: 'AntiFlag', hidden: true },
                { field: 'ImageFlag', hidden: true},
                { field: 'LabCode', hidden: true },
                {
                    field: 'Operation', title: '操作', width: setWidth(0.05), formatter: function (value, row, index) {
                       // rows.push(row);
                        var edit = "<a href='#' data-options='iconCls:icon-edit' onclick='editRow(" + index + "," + value + ")'>修改</a>";
                       
                        return edit;
                    }
                }
        ]],
        loadFilter: function (data) {
            var result = eval("(" + data.ResultDataValue + ")");
            return { total: result.total||0, rows: result.rows||[] };
        }
    });
  
   
   
  

});
    //设置列宽
    function setWidth(percent) {
         return document.body.clientWidth * percent;
    }
    //更新页面
    function updateuser() {
        var currentTime = new Date().getTime();
        
        $('#dg').datagrid('load', {time: currentTime });
        $("#FileUpload1").filebox("clear");
      
}
    //删除数据
    function delRow() {
       
    var rows = $('#dg').datagrid('getSelections');
    if (rows.length == 0) {
        $.messager.alert("提示", "请勾选需要删除的数据!")
        return;
    } else {
        $.messager.confirm('提示', '你确定要删除么?', function (r) {
            if (r) {
               for (var i = 0; i < rows.length; i++) {
                $.ajax({
                    type: 'get',
                    contentType: 'application/json',
                    url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/DelReportModel?id=' + rows[i].Id,
                    dataType: 'json',
                    async:false,
                    success: function (data) {
                        if (data.success == true) {
                            //$.messager.alert('提示', '删除数据成功！');
                            //$('#dg').datagrid('reload');
                            delCount++;
                        } else {
                            $.messager.alert('提示', '删除数据失败！失败信息：' + data.msg);
                        }
                    }
               })

               }
               if (delCount > 0) {
                   //alert('成功删除' + delCount + '条记录');
                   delCount = 0;
                   $('#dg').datagrid('clearSelections');
                   var currentTime = new Date().getTime();
                   $('#dg').datagrid('reload', { time: currentTime });//因为getSelections会记忆选过的记录，所以要清空一下
               }
                
            }
        });
    }
}
    //增加数据
    function addRow() {
       // $("#FileUpload1").filebox("clear");
       // $("#FileUpload1").file("enable");
        $("#PintFormatFileName").textbox("disable")
        

        $('#dlg').dialog('open').dialog('setTitle', '增加');
        //$("#FileUpload1").file("clear");
        $('#fm').form('clear');
        $("#cbBatchPrint").combobox('select', '否');
        $("#ImageFlag").combobox('select', '否');
        $("#test").combobox('select', '否');
    btnType = 'add';
}
    //编辑数据
    function editRow(index, value) {
        $('#fm').form('clear');
        btnType = 'edit';
        $("#PintFormatFileName").textbox("disable")
        var rowData = $("#dg").datagrid('getRows')[index];
        $('#fm').form('load', rowData)

        $('#dlg').dialog('open').dialog('setTitle', '修改');

        $("#ImageFlag").combobox('select', '否');
       // $("#FileUpload1").file("disable");
       // $("#FileUpload1").file("clear");
       
}
//保存数据
   
    function save() {     
        var errors = 0;
      
        if (btnType == 'edit') {
            
        var Id = $('#Id').val();  
        var PrintFormatName = $('#PrintFormatName').val();
        if (PrintFormatName == "") {
           
            errors += 1;
        }
        var PintFormatAddress = $('#PintFormatAddress').val();
        if (PintFormatAddress == "") {
           
            errors += 1;
        }
        var PintFormatFileName = $('#PintFormatFileName').val();
       
        var ItemParaLineNum = $('#ItemParaLineNum').val();
        if (ItemParaLineNum == "") {
           
            errors += 1;
        }
        var PaperSize = $('#PaperSize').val();
        if (PaperSize == "") {
          
            errors += 1;
        }
        var PrintFormatDesc = $('#PrintFormatDesc').val();
        var BatchPrint = $('#cbBatchPrint').combobox('getValue');
        if (BatchPrint == "是")
        {
           BatchPrint = 1;
        }
        else if (BatchPrint == "否")
        {
           BatchPrint = 0;
        }     
        var r = '{"jsonentity":{"Id":"' + Id + '","PrintFormatName":"' + PrintFormatName + '","PintFormatAddress":"' + PintFormatAddress + '","PintFormatFileName":"' + PintFormatFileName + '","ItemParaLineNum":"' + ItemParaLineNum + '","PaperSize":"' + PaperSize + '","PrintFormatDesc":"' + PrintFormatDesc + '","BatchPrint":"' + BatchPrint + '"}}'
        if (errors > 0) {
            $.messager.alert('提示', '请检查输入值的完整性', 'warning');
        } else {
            
            $.ajax({
                type: 'post',
                contentType: 'application/json',
                url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/UpdateReportModelByID',
                data: r,
                dataType: 'json',
                success: function (data) {
                    if (data.success == true) {
                        var currentTime = new Date().getTime();
                        $('#dg').datagrid('load', { time: currentTime });
                        $('#dlg').dialog('close');
                        $("#FileUpload1").filebox("clear");
                    } else {
                        $.messager.alert('提示', '修改数据失败！失败信息：' + data.msg);
                    }
                }
            });
        }
    }
        else {
            var str = $("#FileUpload1").val();
          //  var str = $("#FileUpload1").filebox("getValue");
            var splited = str.split('\\');
            var laststr = splited[splited.length - 1];
            $("#PintFormatFileName").textbox("setValue", laststr);
       var PrintFormatName = $('#PrintFormatName').val();
        if (PrintFormatName=="") {
           
            errors += 1;
        }
        var PintFormatAddress = $('#PintFormatAddress').val();
        if (PintFormatAddress=="")
        {
            errors += 1;
        }
        var PintFormatFileName = $('#PintFormatFileName').val();
        if (PintFormatFileName=="") {
            errors += 1;
        }
        var ItemParaLineNum = $('#ItemParaLineNum').val();
        if (ItemParaLineNum=="") {
            errors += 1;
        }
        var PaperSize = $('#PaperSize').val();
        if (PaperSize=="") {
            errors += 1;
        }
        var PrintFormatDesc = $('#PrintFormatDesc').val();
        var BatchPrint = $('#cbBatchPrint').combobox('getValue');
        if (BatchPrint == "是") {
           BatchPrint = 1;
        }
        else if (BatchPrint == "否") {
           BatchPrint = 0;
        }
        
        var r = '{ "PrintFormatName": "' + PrintFormatName + '","PintFormatAddress": "' + PintFormatAddress + '","PintFormatFileName": "' + PintFormatFileName + '","ItemParaLineNum": "' + ItemParaLineNum + '","PaperSize": "' + PaperSize + '","PrintFormatDesc": "' + PrintFormatDesc + '","BatchPrint": "' + BatchPrint + '"}'
        if (errors > 0) {
            $.messager.alert('提示', '请检查输入值的完整性', 'warning');
            
        } else{
           // var str = $("#FileUpload1").file("getValue");
           // var str = $("#FileUpload1").filebox("getValue");
            var str = $("#FileUpload1").val();
            var splits = str.split(".");
           
            var lastname= splits[1];
           
            if (!(lastname.toUpperCase() == "XSL" || lastname.toUpperCase() == "XSLT" || lastname.toUpperCase() == "FRX" || lastname.toUpperCase() == "FR3")) {
               // $("#FileUpload1").file("clear");
                $.messager.alert('提示', '模板格式不正确，请传以下格式的模板文件.XSL.XSLT.FRX.FR3!');
               // $("#FileUpload1").filebox("clear");
                $("#PintFormatFileName").textbox("clear");
            } else {
                $('#FileUploadForm').form('submit', {

                    url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/AddReportModel',
                    onSubmit: function (param) {

                        param.jsonentity = r;
                        return true;
                    },
                    success: function (data) {
                        //data = Shell.util.JSON.decode(data);
                        //if (data.success == true) {
                            var currentTime = new Date().getTime();
                            $('#dg').datagrid('load', { time: currentTime });
                            $('#dlg').dialog('close');
                           // $("#FileUpload1").filebox("clear");
                        //} else {
                        //    $.messager.alert('提示', '新增数据失败！失败信息：' + data.msg);
                        //}
                    }
                })
            }
        }
    }
    //请求服务器

}
   
    //根据模板名称搜索数据
    function doSearch(value) {
    var itemkey = value;
    $('#dg').datagrid('options').pageNumber = 1;
    $('#dg').datagrid('getPager').pagination({ pageNumber: 1 });
    $('#dg').datagrid('options').url = Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetAllReportModelManage?itemkey=' + itemkey;
    var currentTime = new Date().getTime();
    $('#dg').datagrid('reload', { time: currentTime });
    }
  