var judge = 0;
var errors = 0;
$(function () {
    $('#dg').datagrid({
        loadMsg: '数据加载中...',
        toolbar: "#toolbar",
        fitColumns: true,
        singleSelect: false,
        border: false,
        fit: true,
        pagination: true,
        pageList: [10, 20, 50, 100, 500],
        rownumbers: true,
        collapsible: false,
        idField: 'BAntiType_Id',
        url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBAntiTypeByHQL',
        method: 'get',
        striped: true,
        columns: [[
                { field: 'BAntiType_Id', hidden: true },
                { field: 'cb', checkbox: 'true' },
                { field: 'BAntiType_CName', title: '名称', width: '30%' },
                { field: 'BAntiType_ShortCode', title: '快捷码', width: '30%' },
                { field: 'BAntiType_DispOrder', title: '显示次序', width: '30%' },
                {
                    field: 'Operation', title: '操作', width: '6%', formatter: function (value, row, index) {
                        var edit = "<a href='#' data-options='iconCls:icon-edit' onclick='edit(" + index + "," + value + ")'>修改</a>";
                        return edit;
                    }
                }
        ]],
        queryParams: {
            isPlanish: true
        },
        loadFilter: function (data) {
            var result = eval("(" + data.ResultDataValue + ")") || [];
            return { total: result.count || 0, rows: result.list || []} || [];
        }
    })
})
function doSearch(value) {
    //  $('#txtSearchKey').searchbox('disable');
    var SearchKey = $("#txtSearchKey").searchbox("getValue");

    $('#dg').datagrid({
        url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBAntiTypeByHQL?fields=' + SearchKey,
        loadFilter: function (data) {
            var result = eval("(" + data.ResultDataValue + ")") || [];
            return { total: result.total || 0, rows: result.rows || []} || [];
        }
        //onLoadSuccess: function (data) {
        //    $('#txtSearchKey').searchbox('enable');
        //}
    });

}
function add() {
    $('#dlg').dialog('open').dialog('setTitle', '新增');
   // $('#ddlVisible').combobox('select', '是');
    $('#fm').form('clear');
    judge = 'add';
}
function edit(index, value) {
    judge = 'edit';
    var rowData = $("#dg").datagrid('getRows')[index];
    $('#fm').form('load', rowData)
    $('#dlg').dialog('open').dialog('setTitle', '修改');
   
}
function save() {
    if (judge == 'add') {
       // $('#save').linkbutton('disable');
        var r = '';
        r += '{"entity":{';

        var BAntiType_CName = $('#txtBAntiType_CName').textbox('getValue');
        if (BAntiType_CName) {
            r += '"CName":"' + BAntiType_CName + '",';
        } else {
        errors += 1;
        }

        var BAntiType_ShortCode = $('#txtBAntiType_ShortCode').textbox('getValue');
        if (BAntiType_ShortCode) {
            r += '"ShortCode":"' + BAntiType_ShortCode + '",';
        }

        var BAntiType_Disporder = $('#txtBAntiType_Disporder').numberbox("getValue");
        if (BAntiType_Disporder) {
            r += '"DispOrder":' + BAntiType_Disporder + ',';
        }
       
        r += '}}';
        if (errors > 0) {
            $.messager.alert('提示', '请检查输入值的完整性');
            errors = 0;
        } else {
            $.ajax({
                type: 'post',
                contentType: 'application/json',
                url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_AddBAntiType',
                data: r,
                dataType: 'json',
                success: function (data) {
                    $('#save').linkbutton('enable');
                    $('#dg').datagrid('load');
                    $('#dlg').dialog('close');
                }
            })
        }
    } else if (judge == 'edit') {
        $('#save').linkbutton('disable');
        var r = '';
        r += '{"entity":{';
        var BAntiType_Id = $('#txtBAntiType_Id').textbox("getValue");
        if (BAntiType_Id == '') {
            errors += 1;
        } else {
            r += '"Id":"' + BAntiType_Id + '",';
        }
        var BAntiType_CName = $('#txtBAntiType_CName').textbox('getValue');
        if (BAntiType_CName) {
            r += '"CName":"' + BAntiType_CName + '",';
        } else {
            errors += 1;
        }

        var BAntiType_ShortCode = $('#txtBAntiType_ShortCode').textbox('getValue');
        if (BAntiType_ShortCode) {
            r += '"ShortCode":"' + BAntiType_ShortCode + '",';
        }

        var BAntiType_Disporder = $('#txtBAntiType_Disporder').numberbox("getValue");
        if (BAntiType_Disporder) {
            r += '"DispOrder":' + BAntiType_Disporder + '';
        }

        r += '},"fields":"Id,CName,ShortCode,DispOrder"}';
        if (errors > 0) {
            $.messager.alert('提示', '请检查输入值的完整性');
            errors = 0;
        } else {
            $.ajax({
                type: 'post',
                contentType: 'application/json',
                url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_UpdateBAntiTypeByField',
                data: r,
                dataType: 'json',
                success: function (data) {
                    $('#save').linkbutton('enable');
                    $('#dg').datagrid('load');
                    $('#dlg').dialog('close');
                }
            })
        }
    }
}
function del() {
    var delCount = 0;
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
                        url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_DelBAntiType?Id=' + rows[i].BAntiType_Id,
                        dataType: 'json',
                        async: false,
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
                    $('#dg').datagrid('reload');//因为getSelections会记忆选过的记录，所以要清空一下
                }

            }
        });
    }
}
function update() {
    $('#dg').datagrid('load');
    $('#fm').form('clear')
    $('#txtSearchKey').searchbox("clear");
}