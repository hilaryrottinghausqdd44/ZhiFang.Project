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
        idField: 'BAntiBiotic_Id',
        url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBAntiBioticByHQL',
        method: 'get',
        striped: true,
        columns: [[
                { field: 'BAntiBiotic_Id', hidden: true },
                { field: 'cb', checkbox: 'true' },
                { field: 'BAntiBiotic_CName', title: '名称', width: '10%' },
                { field: 'BAntiBiotic_EName', title: '英文名称', width: '10%' },
                { field: 'BAntiBiotic_ShortName', title: '简称', width: '10%' },
                { field: 'BAntiBiotic_ShortCode', title: '快捷码', width: '10%' },
                { field: 'BAntiBiotic_AntiUnit', title: '抗生素单位', width: '10%' },
                { field: 'BAntiBiotic_AntiNote', title: '抗生素文本', width: '10%' },
                {
                    field: 'BAntiBiotic_Visible', title: '是否显示', width: '10%', formatter: function (value) {
                        if (value == "false") {
                            value = "否";
                        } else if (value == "true") {
                            value = "是";
                        }
                        return value;
                    }
                },
                { field: 'BAntiBiotic_DispOrder', title: '显示次序', width: '10%' },
               { field: 'BAntiBiotic_AntiTypeNo', title: '抗生素编号', width: '10%' },

                {
                    field: 'Operation', title: '操作', width: '6%', formatter: function (value, row, index) {
                        var edit = "<a href='#' data-options='iconCls:icon-edit' onclick='edit(" + index + "," + value + ")'>修改</a>";
                        return edit;
                    }
                }
        ]],
        queryParams: {
            isPlanish: true
           // fields: "BAntiBiotic_Id,BAntiBiotic_CName,BAntiBiotic_EName,BAntiBiotic_AntiType_No,BAntiBiotic_ShortName,BAntiBiotic_ShortCode,BAntiBiotic_AntiUnit,BAntiBiotic_AntiNote,BAntiBiotic_Visible,BAntiBiotic_DispOrder,BAntiBiotic_BAntiType_Id"
        },

        loadFilter: function (data) {
            var result = eval("(" + data.ResultDataValue + ")") || [];
            return { total: result.count || 0, rows: result.list || []} || [];
        }
    })
//    $("#txtBAntiBiotic_BAntiType_Id").combobox({
//        url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBAntiTypeByHQL?isPlanish=' + true,
//        method: 'get',
//        valueField: 'BAntiType_Id',
//        textField: 'BAntiType_CName',
//        loadFilter: function (data) {
//            var result = eval("(" + data.ResultDataValue + ")") || [];
//            return result.list || [];
//        }
//    })
})
function doSearch(value) {
    //  $('#txtSearchKey').searchbox('disable');
    var SearchKey = $("#txtSearchKey").searchbox("getValue");

    $('#dg').datagrid({
        url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBAntiBioticByHQL?fields=' + SearchKey,
        loadFilter: function (data) {
            var result = eval("(" + data.ResultDataValue + ")");
            return { total: result.total || 0, rows: result.rows || [] };
        }
        //onLoadSuccess: function (data) {
        //    $('#txtSearchKey').searchbox('enable');
        //}
    });

}
function add() {
    $('#dlg').dialog('open').dialog('setTitle', '新增');

    $('#fm').form('clear');
    $('#ddlBAntiBiotic_Visible').combobox('select', '是');
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
        var BAntiBiotic_CName = $('#txtBAntiBiotic_CName').textbox('getValue');
        if (BAntiBiotic_CName) {
            r += '"CName":"' + BAntiBiotic_CName + '",';
        } else {
           errors += 1;
        }
       var BAntiBiotic_AntiTypeNo = $('#txtBAntiBiotic_AntiTypeNo').textbox("getValue");
       if (BAntiBiotic_AntiTypeNo) {
            //var BAntiBiotic_DataTimeStamp = $('#txtBAntiBiotic_DataTimeStamp').datetimebox("getValue");

          //  r += '"BAntiType":{"DataTimeStamp":"' + BAntiBiotic_DataTimeStamp + '","Id":"' + BAntiBiotic_BAntiType_Id + '"},';
           r += '"AntiTypeNo":"' + BAntiBiotic_AntiTypeNo + '",';
        }
       

       var BAntiBiotic_EName = $('#txtBAntiBiotic_EName').textbox('getValue');
       if (BAntiBiotic_EName) {
           r += '"EName":"' + BAntiBiotic_EName + '",';
       } else {
           errors += 1;
        }
        var BAntiBiotic_ShortName = $('#txtBAntiBiotic_ShortName').textbox('getValue');
        if (BAntiBiotic_ShortName) {
            r += '"ShortName":"' + BAntiBiotic_ShortName + '",';
        }
        var BAntiBiotic_ShortCode = $('#txtBAntiBiotic_ShortCode').textbox('getValue');
        if (BAntiBiotic_ShortCode) {
            r += '"ShortCode":"' + BAntiBiotic_ShortCode + '",';
        }
        var BAntiBiotic_AntiUnit = $('#txtBAntiBiotic_AntiUnit').textbox('getValue');
        if (BAntiBiotic_AntiUnit) {
            r += '"AntiUnit":"' + BAntiBiotic_AntiUnit + '",';
        }
        var BAntiBiotic_AntiNote = $('#txtBAntiBiotic_AntiNote').textbox('getValue');
        if (BAntiBiotic_AntiNote) {
            r += '"AntiNote":"' + BAntiBiotic_AntiNote + '",';
        }
        var BAntiBiotic_Visible = $('#ddlBAntiBiotic_Visible').combobox('getValue');
        if (BAntiBiotic_Visible == '是') {
            BAntiBiotic_Visible = 1;
            r += '"Visible":' + BAntiBiotic_Visible + ',';
        }
        if (BAntiBiotic_Visible == '否') {
            BAntiBiotic_Visible = 0;
            r += '"Visible":' + BAntiBiotic_Visible + ',';
        }
       
        var BAntiBiotic_Disporder = $('#txtBAntiBiotic_Disporder').numberbox("getValue");
        if (BAntiBiotic_Disporder) {
            r += '"DispOrder":' + BAntiBiotic_Disporder + ',';
        }


        r += '}}';
        if (errors > 0) {
            $.messager.alert('提示', '请检查输入值的完整性');
            errors = 0;
        } else {
            $.ajax({
                type: 'post',
                contentType: 'application/json',
                url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_AddBAntiBiotic',
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
        //$('#save').linkbutton('disable');
        var r = '';
        r += '{"entity":{';
        var BAntiBiotic_Id = $('#txtBAntiBiotic_Id').textbox("getValue");
        if (BAntiBiotic_Id == '') {
            errors += 1;
        } else {
            r += '"Id":"' + BAntiBiotic_Id + '",';
        }
        var BAntiBiotic_CName = $('#txtBAntiBiotic_CName').textbox('getValue');
        if (BAntiBiotic_CName) {
            r += '"CName":"' + BAntiBiotic_CName + '",';
        } else {
            errors += 1;
        }
        var BAntiBiotic_EName = $('#txtBAntiBiotic_EName').textbox('getValue');
        if (BAntiBiotic_EName) {
            r += '"EName":"' + BAntiBiotic_EName + '",';
        } else {
            errors += 1;
        }
        var BAntiBiotic_ShortName = $('#txtBAntiBiotic_ShortName').textbox('getValue');
        if (BAntiBiotic_ShortName) {
            r += '"ShortName":"' + BAntiBiotic_ShortName + '",';
        }
        var BAntiBiotic_ShortCode = $('#txtBAntiBiotic_ShortCode').textbox('getValue');
        if (BAntiBiotic_ShortCode) {
            r += '"ShortCode":"' + BAntiBiotic_ShortCode + '",';
        }
        var BAntiBiotic_AntiUnit = $('#txtBAntiBiotic_AntiUnit').textbox('getValue');
        if (BAntiBiotic_AntiUnit) {
            r += '"AntiUnit":"' + BAntiBiotic_AntiUnit + '",';
        }
        var BAntiBiotic_AntiNote = $('#txtBAntiBiotic_AntiNote').textbox('getValue');
        if (BAntiBiotic_AntiNote) {
            r += '"AntiNote":"' + BAntiBiotic_AntiNote + '",';
        }
        var BAntiBiotic_Visible = $('#ddlBAntiBiotic_Visible').combobox('getValue');
        if (BAntiBiotic_Visible == '是') {
            BAntiBiotic_Visible = 1;
            r += '"Visible":' + BAntiBiotic_Visible + ',';
        }
        if (BAntiBiotic_Visible == '否') {
            BAntiBiotic_Visible = 0;
            r += '"Visible":' + BAntiBiotic_Visible + ',';
        }
       
        var BAntiBiotic_Disporder = $('#txtBAntiBiotic_Disporder').numberbox("getValue");
        if (BAntiBiotic_Disporder) {
            r += '"DispOrder":' + BAntiBiotic_Disporder + ',';
        }
        var BAntiBiotic_AntiTypeNo = $('#txtBAntiBiotic_AntiTypeNo').textbox("getValue");
        if (BAntiBiotic_AntiTypeNo) {

            r += '"AntiTypeNo":"' + BAntiBiotic_AntiTypeNo + '"';
        } 
        r += '},"fields":"Id,CName,EName,ShortName,ShortCode,AntiUnit,AntiNote,Visible,DispOrder,AntiTypeNo"}';
        if (errors > 0) {
            $.messager.alert('提示', '请检查输入值的完整性');
            errors = 0;
        } else {
            $.ajax({
                type: 'post',
                contentType: 'application/json',
                url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_UpdateBAntiBioticByField',
                data: r,
                dataType: 'json',
                success: function (data) {
                    //$('#save').linkbutton('enable');
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
                        url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_DelBAntiBiotic?Id=' + rows[i].BAntiBiotic_Id,
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