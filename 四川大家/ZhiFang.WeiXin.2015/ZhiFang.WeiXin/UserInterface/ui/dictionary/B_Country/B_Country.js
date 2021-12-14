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
        idField: 'BCountry_Id',
        url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBCountryByHQL',
        method: 'get',
        striped: true,
        columns: [[
                { field: 'BCountry_Id', hidden: true },
                { field: 'cb', checkbox: 'true' },
                { field: 'BCountry_Name', title: '名称', width: '10%' },
                { field: 'BCountry_EName', title: '英文名称', width: '10%' },
                { field: 'BCountry_Code', title: '代码', width: '10%' },
                { field: 'BCountry_SName', title: '简称', width: '10%' },
                { field: 'BCountry_Shortcode', title: '快捷码', width: '10%' },
                { field: 'BCountry_PinYinZiTou', title: '拼音字头', width: '10%' },
                { field: 'BCountry_Comment', title: '备注', width: '20%' },
                {
                    field: 'BCountry_IsUse', title: '是否显示', width: '10%', formatter: function (value) {
                        if (value == 0) {
                            value = "否";
                        } else {
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
        url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBCountryByHQL?fields=' + SearchKey,
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
    $('#ddlBCountry_IsUse').combobox('select', '是');
    judge = 'add';
}
function edit(index, value) {
    judge = 'edit';
    var rowData = $("#dg").datagrid('getRows')[index];

    $('#fm').form('load', rowData)
    //var abs = $("#ddlBAccountType_IsUse").combobox('getValue');
    if ($("#ddlBCountry_IsUse").combobox('getValue') == 'false') {
        $("#ddlBCountry_IsUse").combobox('setValue', '否');
    } else {
        $("#ddlBCountry_IsUse").combobox('setValue', '是');
    }
    $('#dlg').dialog('open').dialog('setTitle', '修改');
    
}
function save() {
    if (judge == 'add') {
       // $('#save').linkbutton('disable');
        var r = '';
        r += '{"entity":{';

        var BCountry_Name = $('#txtBCountry_Name').textbox('getValue');
        if (BCountry_Name) {
            r += '"Name":"' + BCountry_Name + '",';
        } else {
            errors+=1;
         }
        var BCountry_EName = $('#txtBCountry_EName').textbox('getValue');
        if (BCountry_EName) {
            r += '"EName":"' + BCountry_EName + '",';
        } else {
            errors += 1;
        }

        var BCountry_Code = $('#txtBCountry_Code').textbox('getValue');
        if (BCountry_Code) {
            r += '"Code":"' + BCountry_Code + '",';
        }
        var BCountry_SName = $('#txtBCountry_SName').textbox('getValue');
        if (BCountry_SName) {
            r += '"SName":"' + BCountry_SName + '",';
        }
        var BCountry_Shortcode = $('#txtBCountry_Shortcode').textbox('getValue');
        if (BCountry_Shortcode) {
            r += '"Shortcode":"' + BCountry_Shortcode + '",';
        }
        var BCountry_PinYinZiTou = $('#txtBCountry_PinYinZiTou').textbox('getValue');
        if (BCountry_PinYinZiTou) {
            r += '"PinYinZiTou":"' + BCountry_PinYinZiTou + '",';
        }
        var BCountry_Comment = $('#txtBCountry_Comment').textbox('getValue');
        if (BCountry_Comment) {
            r += '"Comment":"' + BCountry_Comment + '",';
        }
        var BCountry_IsUse = $('#ddlBCountry_IsUse').combobox('getValue');
        if (BCountry_IsUse == '是') {
            BCountry_IsUse = 1;
            r += '"IsUse":' + BCountry_IsUse + ',';
        }
        if (BCountry_IsUse == '否') {
            BCountry_IsUse = 0;
            r += '"IsUse":' + BCountry_IsUse + ',';
        }
       
        r += '}}';
        if (errors > 0) {
            $.messager.alert('提示', '请检查输入值的完整性');
            errors = 0;
        } else {
            $.ajax({
                type: 'post',
                contentType: 'application/json',
                url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_AddBCountry',
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

        var BCountry_Id = $('#txtBCountry_Id').textbox('getValue');
        if (BCountry_Id) {
            r += '"Id":"' + BCountry_Id + '",';
        }
        var BCountry_Name = $('#txtBCountry_Name').textbox('getValue');
        if (BCountry_Name) {
            r += '"Name":"' + BCountry_Name + '",';
        } else {
            errors += 1;
        }
        var BCountry_EName = $('#txtBCountry_EName').textbox('getValue');
        if (BCountry_EName) {
            r += '"EName":"' + BCountry_EName + '",';
        } else {
            errors += 1;
        }
        var BCountry_Code = $('#txtBCountry_Code').textbox('getValue');
        if (BCountry_Code) {
            r += '"Code":"' + BCountry_Code + '",';
        }
        var BCountry_SName = $('#txtBCountry_SName').textbox('getValue');
        if (BCountry_SName) {
            r += '"SName":"' + BCountry_SName + '",';
        }
        var BCountry_Shortcode = $('#txtBCountry_Shortcode').textbox('getValue');
        if (BCountry_Shortcode) {
            r += '"Shortcode":"' + BCountry_Shortcode + '",';
        }
        var BCountry_PinYinZiTou = $('#txtBCountry_PinYinZiTou').textbox('getValue');
        if (BCountry_PinYinZiTou) {
            r += '"PinYinZiTou":"' + BCountry_PinYinZiTou + '",';
        }
        var BCountry_Comment = $('#txtBCountry_Comment').textbox('getValue');
        if (BCountry_Comment) {
            r += '"Comment":"' + BCountry_Comment + '",';
        }
        var BCountry_IsUse = $('#ddlBCountry_IsUse').combobox('getValue');
        if (BCountry_IsUse == '是') {
            BCountry_IsUse = 1;
            r += '"IsUse":' + BCountry_IsUse + '';
        }
        if (BCountry_IsUse == '否') {
            BCountry_IsUse = 0;
            r += '"IsUse":' + BCountry_IsUse + '';
        }
       
        r += '},"fields":"Id,Name,EName,SName,Code,Shortcode,PinYinZiTou,Comment,IsUse"}';
        if (errors > 0) {
            $.messager.alert('提示', '请检查输入值的完整性');
            errors = 0;
        } else {
            $.ajax({
                type: 'post',
                contentType: 'application/json',
                url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_UpdateBCountryByField',
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
                        url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_DelBCountry?Id=' + rows[i].BCountry_Id,
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