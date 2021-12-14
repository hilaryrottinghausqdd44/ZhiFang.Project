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
        idField: 'BHospitalLevel_Id',
        url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBHospitalLevelByHQL',
        method: 'get',
        striped: true,
        columns: [[
                { field: 'BHospitalLevel_Id', hidden: true },
                { field: 'cb', checkbox: 'true' },

                { field: 'BHospitalLevel_Name', title: '名称', width: '13%' },
                { field: 'BHospitalLevel_Code', title: '代码', width: '13%' },

                { field: 'BHospitalLevel_SName', title: '简称', width: '13%' },
                { field: 'BHospitalLevel_Shortcode', title: '快捷码', width: '13%' },
                { field: 'BHospitalLevel_PinYinZiTou', title: '拼音字头', width: '13%' },
                { field: 'BHospitalLevel_Comment', title: '描述', width: '13%' },
                {
                    field: 'BHospitalLevel_IsUse', title: '是否使用', width: '12%', formatter: function (value) {
                        if (value == "false") {
                            value = "否";
                        } else if (value == "true") {
                            value = "是";
                        }
                        return value;
                    }
                },

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
        url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBHospitalLevelByHQL?fields=' + SearchKey,
        loadFilter: function (data) {
            var result = eval("(" + data.ResultDataValue + ")") || [];
            return { total: result.count || 0, rows: result.list || []} || [];
        }
        //onLoadSuccess: function (data) {
        //    $('#txtSearchKey').searchbox('enable');
        //}
    });

}
function add() {
    $('#dlg').dialog('open').dialog('setTitle', '新增');

    $('#fm').form('clear');
    $('#ddlBHospitalLevel_IsUse').combobox('select', '是');
    judge = 'add';
}
function edit(index, value) {
    judge = 'edit';
    var rowData = $("#dg").datagrid('getRows')[index];

    $('#fm').form('load', rowData)
    //var abs = $("#ddlBAccountType_IsUse").combobox('getValue');
    if ($("#ddlBHospitalLevel_IsUse").combobox('getValue') == 'false') {
        $("#ddlBHospitalLevel_IsUse").combobox('setValue', '否');
    } else {
        $("#ddlBHospitalLevel_IsUse").combobox('setValue', '是');
    }
    $('#dlg').dialog('open').dialog('setTitle', '修改');
    
}
function save() {
    if (judge == 'add') {
     //   $('#save').linkbutton('disable');
        var r = '';
        r += '{"entity":{';

        var BHospitalLevel_Name = $('#txtBHospitalLevel_Name').textbox('getValue');
        if (BHospitalLevel_Name) {
            r += '"Name":"' + BHospitalLevel_Name + '",';
        } else {
        errors += 1;
        }
        var BHospitalLevel_Code = $('#txtBHospitalLevel_Code').textbox('getValue');
        if (BHospitalLevel_Code) {
            r += '"Code":"' + BHospitalLevel_Code + '",';
        }
        var BHospitalLevel_SName = $('#txtBHospitalLevel_SName').textbox('getValue');
        if (BHospitalLevel_SName) {
            r += '"SName":"' + BHospitalLevel_SName + '",';
        }
        var BHospitalLevel_Shortcode = $('#txtBHospitalLevel_Shortcode').textbox('getValue');
        if (BHospitalLevel_Shortcode) {
            r += '"Shortcode":"' + BHospitalLevel_Shortcode + '",';
        }
        var BHospitalLevel_PinYinZiTou = $('#txtBHospitalLevel_PinYinZiTou').textbox('getValue');
        if (BHospitalLevel_PinYinZiTou) {
            r += '"PinYinZiTou":"' + BHospitalLevel_PinYinZiTou + '",';
        }
        var BHospitalLevel_Comment = $('#txtBHospitalLevel_Comment').textbox('getValue');
        if (BHospitalLevel_Comment) {
            r += '"Comment":"' + BHospitalLevel_Comment + '",';
        }
        var BHospitalLevel_IsUse = $('#ddlBHospitalLevel_IsUse').combobox('getValue');
        if (BHospitalLevel_IsUse == '是') {
            BHospitalLevel_IsUse = 1;
            r += '"IsUse":' + BHospitalLevel_IsUse + '';
        }
        if (BHospitalLevel_IsUse == '否') {
            BHospitalLevel_IsUse = 0;
            r += '"IsUse":' + BHospitalLevel_IsUse + '';
        }
       
        r += '}}';
        if (errors > 0) {
            $.messager.alert('提示', '请检查输入值的完整性');
            errors = 0;
        } else {
            $.ajax({
                type: 'post',
                contentType: 'application/json',
                url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_AddBHospitalLevel',
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
       // $('#save').linkbutton('disable');
        var r = '';
        r += '{"entity":{';
        var BHospitalLevel_Id = $('#txtBHospitalLevel_Id').numberbox("getValue");
        if (BHospitalLevel_Id == '') {
            errors += 1;
        } else {
            r += '"Id":"' + BHospitalLevel_Id + '",';
        }
        var BHospitalLevel_Name = $('#txtBHospitalLevel_Name').textbox('getValue');
        if (BHospitalLevel_Name) {
            r += '"Name":"' + BHospitalLevel_Name + '",';
        } else {
            errors += 1;
        }
        var BHospitalLevel_Code = $('#txtBHospitalLevel_Code').textbox('getValue');
        if (BHospitalLevel_Code) {
            r += '"Code":"' + BHospitalLevel_Code + '",';
        }
        var BHospitalLevel_SName = $('#txtBHospitalLevel_SName').textbox('getValue');
        if (BHospitalLevel_SName) {
            r += '"SName":"' + BHospitalLevel_SName + '",';
        }
        var BHospitalLevel_Shortcode = $('#txtBHospitalLevel_Shortcode').textbox('getValue');
        if (BHospitalLevel_Shortcode) {
            r += '"Shortcode":"' + BHospitalLevel_Shortcode + '",';
        }
        var BHospitalLevel_PinYinZiTou = $('#txtBHospitalLevel_PinYinZiTou').textbox('getValue');
        if (BHospitalLevel_PinYinZiTou) {
            r += '"PinYinZiTou":"' + BHospitalLevel_PinYinZiTou + '",';
        }
        var BHospitalLevel_Comment = $('#txtBHospitalLevel_Comment').textbox('getValue');
        if (BHospitalLevel_Comment) {
            r += '"Comment":"' + BHospitalLevel_Comment + '",';
        }
        var BHospitalLevel_IsUse = $('#ddlBHospitalLevel_IsUse').combobox('getValue');
        if (BHospitalLevel_IsUse == '是') {
            BHospitalLevel_IsUse = 1;
            r += '"IsUse":' + BHospitalLevel_IsUse + '';
        }
        if (BHospitalLevel_IsUse == '否') {
            BHospitalLevel_IsUse = 0;
            r += '"IsUse":' + BHospitalLevel_IsUse + '';
        }
        
            
       
        r += '},"fields":"Id,Name,SName,Code,Shortcode,PinYinZiTou,Comment,IsUse"}';
        if (errors > 0) {
            $.messager.alert('提示', '请检查输入值的完整性');
            errors = 0;
        } else {
            $.ajax({
                type: 'post',
                contentType: 'application/json',
                url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_UpdateBHospitalLevelByField',
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
                        url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_DelBHospitalLevel?Id=' + rows[i].BHospitalLevel_Id,
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