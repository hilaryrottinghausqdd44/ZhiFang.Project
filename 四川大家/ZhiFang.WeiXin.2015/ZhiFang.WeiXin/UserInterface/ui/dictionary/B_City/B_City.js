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
        idField: 'BCity_Id',
        url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBCityByHQL',
        method: 'get',
        striped: true,
        columns: [[
                { field: 'BCity_Id', hidden: true },
                { field: 'cb', checkbox: 'true' },
                { field: 'BCity_Name', title: '名称', width: '13%' },
                { field: 'BCity_EName', title: '英文名称', width: '13%' },
                { field: 'BCity_SName', title: '简称', width: '13%' },
                { field: 'BCity_Shortcode', title: '快捷码', width: '13%' },
                { field: 'BCity_PinYinZiTou', title: '拼音字头', width: '13%' },
                { field: 'BCity_Comment', title: '描述', width: '13%' },

                {
                    field: 'BCity_IsUse', title: '是否使用', width: '12%', formatter: function (value) {
                        if (value == 'false') {
                            value = "否";
                        } else if(value=='true'){
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
            isPlanish: true,
            fields: "BCity_Id,BCity_Name,BCity_EName,BCity_SName,BCity_PinYinZiTou,BCity_Shortcode,BCity_Shortcode,BCity_Comment,BCity_IsUse,BCity_BProvince_Id"
          
        },

        loadFilter: function (data) {
            var result = eval("(" + data.ResultDataValue + ")") || [];
            return { total: result.count || 0, rows: result.list || []} || [];
        }
    })
    $("#txtBCity_BProvince_Id").combobox({
        url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBProvinceByHQL?isPlanish=' + true,
        method: 'get',
        valueField: 'BProvince_Id',
        textField: 'BProvince_Name',
        loadFilter: function (data) {
            var result = eval("(" + data.ResultDataValue + ")") || [];
            return result.list || [];
        }
    })
})
function doSearch(value) {
    //  $('#txtSearchKey').searchbox('disable');
    var SearchKey = $("#txtSearchKey").searchbox("getValue");

    $('#dg').datagrid({
        url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBCityByHQL?fields=' + SearchKey,
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
    $('#ddlBCity_IsUse').combobox('select', '是');
    judge = 'add';
}
function edit(index, value) {
    judge = 'edit';
    var rowData = $("#dg").datagrid('getRows')[index];
    $('#fm').form('load', rowData)
    if ($("#ddlBCity_IsUse").combobox('getValue') == 'false') {
        $("#ddlBCity_IsUse").combobox('setValue', '否');
    } else {
        $("#ddlBCity_IsUse").combobox('setValue', '是');
    }
    $('#dlg').dialog('open').dialog('setTitle', '修改');
   
}
function save() {
    if (judge == 'add') {
       // $('#save').linkbutton('disable');
        var r = '';
        r += '{"entity":{';
        var BCity_Name = $('#txtBCity_Name').textbox('getValue');
        if (BCity_Name) {
            r += '"Name":"' + BCity_Name + '",';
        } else {
        errors += 1;
        }
        var BCity_BProvince_Id = $('#txtBCity_BProvince_Id').combobox("getValue");
        if (BCity_BProvince_Id) {
            var BCity_DataTimeStamp = $('#txtBCity_DataTimeStamp').datetimebox("getValue");
            r += '"BProvince":{"DataTimeStamp":"' + BCity_DataTimeStamp + '","Id":' + BCity_BProvince_Id + '},';
        }
        var BCity_BProvince_Id = $('#txtBCity_BProvince_Id').combobox("getValue");
        if (BCity_BProvince_Id) {
            r += '"ProvinceID":"' + BCity_BProvince_Id + '",';
        }
        var BCity_EName = $('#txtBCity_EName').textbox('getValue');
        if (BCity_EName) {
            r += '"EName":"' + BCity_EName + '",';
        } else {
            errors += 1;
        }
        var BCity_SName = $('#txtBCity_SName').textbox('getValue');
        if (BCity_SName) {
            r += '"SName":"' + BCity_SName + '",';
        }
        var BCity_Shortcode = $('#txtBCity_Shortcode').textbox('getValue');
        if (BCity_Shortcode) {
            r += '"Shortcode":"' + BCity_Shortcode + '",';
        }
        var BCity_PinYinZiTou = $('#txtBCity_PinYinZiTou').textbox('getValue');
        if (BCity_PinYinZiTou) {
            r += '"PinYinZiTou":"' + BCity_PinYinZiTou + '",';
        }
        var BCity_Comment = $('#txtBCity_Comment').textbox('getValue');
        if (BCity_Comment) {
            r += '"Comment":"' + BCity_Comment + '",';
        }
        var BCity_IsUse = $('#ddlBCity_IsUse').combobox('getValue');
        if (BCity_IsUse == '是') {
            BCity_IsUse = 1;
            r += '"IsUse":' + BCity_IsUse + ',';
        }
        if (BCity_IsUse == '否') {
            BCity_IsUse = 0;
            r += '"IsUse":' + BCity_IsUse + ',';
        }
       
        r += '}}';
        if (errors > 0) {
            $.messager.alert('提示', '请检查输入值的完整性');
            errors = 0;
        } else {
            $.ajax({
                type: 'post',
                contentType: 'application/json',
                url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_AddBCity',
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

        var BCity_Id = $('#txtBCity_Id').numberbox("getValue");
        if (BCity_Id == '') {
            errors += 1;
        } else {
            r += '"Id":"' + BCity_Id + '",';
        }
        var BCity_BProvince_Id = $('#txtBCity_BProvince_Id').combobox("getValue");
        if (BCity_BProvince_Id) {

            r += '"ProvinceID":"' + BCity_BProvince_Id + '",';
        }
        var BCity_Name = $('#txtBCity_Name').textbox('getValue');
        if (BCity_Name) {
            r += '"Name":"' + BCity_Name + '",';
        } else {
            errors += 1;
        }
        var BCity_EName = $('#txtBCity_EName').textbox('getValue');
        if (BCity_EName) {
            r += '"EName":"' + BCity_EName + '",';
        } else {
            errors += 1;
        }
        var BCity_SName = $('#txtBCity_SName').textbox('getValue');
        if (BCity_SName) {
            r += '"SName":"' + BCity_SName + '",';
        }
        var BCity_Shortcode = $('#txtBCity_Shortcode').textbox('getValue');
        if (BCity_Shortcode) {
            r += '"Shortcode":"' + BCity_Shortcode + '",';
        }
        var BCity_PinYinZiTou = $('#txtBCity_PinYinZiTou').textbox('getValue');
        if (BCity_PinYinZiTou) {
            r += '"PinYinZiTou":"' + BCity_PinYinZiTou + '",';
        }
        var BCity_Comment = $('#txtBCity_Comment').textbox('getValue');
        if (BCity_Comment) {
            r += '"Comment":"' + BCity_Comment + '",';
        }
        var BCity_IsUse = $('#ddlBCity_IsUse').combobox('getValue');
        if (BCity_IsUse == '是') {
            BCity_IsUse = 1;
            r += '"IsUse":' + BCity_IsUse + '';
        }
        if (BCity_IsUse == '否') {
            BCity_IsUse = 0;
            r += '"IsUse":' + BCity_IsUse + '';
        }

        r += '},"fields":"Id,Name,SName,EName,Shortcode,PinYinZiTou,Comment,IsUse,BProvince"}';
        if (errors > 0) {
            $.messager.alert('提示', '请检查输入值的完整性');
            errors = 0;
        } else {
            $.ajax({
                type: 'post',
                contentType: 'application/json',
                url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_UpdateBCityByField',
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
                        url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_DelBCity?Id=' + rows[i].BCity_Id,
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