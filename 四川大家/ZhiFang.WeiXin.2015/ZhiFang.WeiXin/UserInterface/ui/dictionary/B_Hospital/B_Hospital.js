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
        idField: 'BHospital_Id',
        url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBHospitalByHQL',
        method: 'get',
        striped: true,
        columns: [[
                { field: 'BHospital_Id', hidden: true },
                { field: 'cb', checkbox: 'true' },
                { field: 'BHospital_HospitalCode', title: '代码', width: '10%' },
                { field: 'BHospital_Name', title: '名称', width: '10%' },
                { field: 'BHospital_EName', title: '英文名称', width: '10%' },
                { field: 'BHospital_SName', title: '简称', width: '10%' },
                { field: 'BHospital_Shortcode', title: '快捷码', width: '10%' },
                { field: 'BHospital_PinYinZiTou', title: '拼音字头', width: '10%' },
                { field: 'BHospital_Comment', title: '描述', width: '20%' },
                {
                    field: 'BHospital_IsUse', title: '是否使用', width: '10%', formatter: function (value) {
                        if (value == 'false') {
                            value = "否";
                        } else if (value == 'true') {
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
    $("#txtBHospital_IconsID").combobox({
        url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBIconsByHQL?isPlanish=' + true,
        method: 'get',
        valueField: 'BIcons_Id',
        textField: 'BIcons_Comment',
        loadFilter: function (data) {
            var result = eval("(" + data.ResultDataValue + ")");
            return result.list || [];
        }
    })
    $("#txtBHospital_HTypeID").combobox({
        url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBHospitalTypeByHQL?isPlanish=' + true,
        method: 'get',
        valueField: 'BHospitalType_Id',
        textField: 'BHospitalType_Name',
        loadFilter: function (data) {
            var result = eval("(" + data.ResultDataValue + ")");
            return result.list || [];
        }
    })
    $("#txtBHospital_LevelID").combobox({
        url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBHospitalLevelByHQL?isPlanish=' + true,
        method: 'get',
        valueField: 'BHospitalLevel_Id',
        textField: 'BHospitalLevel_Name',
        loadFilter: function (data) {
            var result = eval("(" + data.ResultDataValue + ")");
            return result.list || [];
        }
    })
})
function doSearch(value) {
    //  $('#txtSearchKey').searchbox('disable');
    var SearchKey = $("#txtSearchKey").searchbox("getValue");

    $('#dg').datagrid({
        url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBHospitalByHQL?fields=' + SearchKey,
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
    $('#ddlBHospital_IsUse').combobox('select', '是');
    judge = 'add';
}
function edit(index, value) {
    judge = 'edit';
    var rowData = $("#dg").datagrid('getRows')[index];

    $('#fm').form('load', rowData)
    //var abs = $("#ddlBAccountType_IsUse").combobox('getValue');
    if ($("#ddlBHospital_IsUse").combobox('getValue') == 'false') {
        $("#ddlBHospital_IsUse").combobox('setValue', '否');
    } else {
        $("#ddlBHospital_IsUse").combobox('setValue', '是');
    }
    $('#dlg').dialog('open').dialog('setTitle', '修改');
    
}
function save() {
    if (judge == 'add') {
       // $('#save').linkbutton('disable');
        var r = '';
        r += '{"entity":{';
        var BHospital_DataTimeStamp = $("#txtBHospital_DataTimeStamp").datetimebox("getValue");
        var BHospital_IconsID = $('#txtBHospital_IconsID').numberbox("getValue");
        if (BHospital_IconsID) {
            r += '"BIcons":{"DataTimeStamp":"' + BHospital_DataTimeStamp + '","Id":' + BHospital_IconsID + '},';
        }
        var BHospital_HTypeID = $('#txtBHospital_HTypeID').numberbox("getValue");
        if (BHospital_HTypeID) {
            r += '"BHType":{"DataTimeStamp":"' + BHospital_DataTimeStamp + '","Id":' + BHospital_HTypeID + '},';
        }
        var BHospital_LevelID = $('#txtBHospital_LevelID').numberbox("getValue");
        if (BHospital_LevelID) {
            r += '"BLevel":{"DataTimeStamp":"' + BHospital_DataTimeStamp + '","Id":' + BHospital_LevelID + '},'; 
        }
       var BHospital_HospitalCode = $('#txtBHospital_HospitalCode').textbox('getValue');
        if (BHospital_HospitalCode == '') {
            errors += 1;
        } else {
            r += '"HospitalCode":' + BHospital_HospitalCode + ',';
        }
        var BHospital_IconsID = $('#txtBHospital_IconsID').numberbox("getValue");
        if (BHospital_IconsID == '') {
            errors += 1;
        } else {
            r += '"IconsID":' + BHospital_IconsID + ',';
        }
        var BHospital_HTypeID = $('#txtBHospital_HTypeID').numberbox("getValue");
        if (BHospital_HTypeID == '') {
            errors += 1;
        } else {
            r += '"HTypeID":' + BHospital_HTypeID + ',';
        }
        var BHospital_LevelID = $('#txtBHospital_LevelID').numberbox("getValue");
        if (BHospital_LevelID == '') {
            errors += 1;
        } else {
            r += '"LevelID":' + BHospital_LevelID + ',';
        }
        var BHospital_Name = $('#txtBHospital_Name').textbox('getValue');
        if (BHospital_Name) {
            r += '"Name":"' + BHospital_Name + '",';
        } else {
        errors += 1;
        }
        var BHospital_EName = $('#txtBHospital_EName').textbox('getValue');
        if (BHospital_EName) {
            r += '"EName":"' + BHospital_EName + '",';
        } else {
            errors += 1;
        }
        var BHospital_SName = $('#txtBHospital_SName').textbox('getValue');
        if (BHospital_SName) {
            r += '"SName":"' + BHospital_SName + '",';
        }
        var BHospital_Shortcode = $('#txtBHospital_Shortcode').textbox('getValue');
        if (BHospital_Shortcode) {
            r += '"Shortcode":"' + BHospital_Shortcode + '",';
        }
        var BHospital_PinYinZiTou = $('#txtBHospital_PinYinZiTou').textbox('getValue');
        if (BHospital_PinYinZiTou) {
            r += '"PinYinZiTou":"' + BHospital_PinYinZiTou + '",';
        }
        var BHospital_Comment = $('#txtBHospital_Comment').textbox('getValue');
        if (BHospital_Comment) {
            r += '"Comment":"' + BHospital_Comment + '",';
        }
        var BHospital_IsUse = $('#ddlBHospital_IsUse').combobox('getValue');
        if (BHospital_IsUse == '是') {
            BHospital_IsUse = 1;
            r += '"IsUse":' + BHospital_IsUse + '';
        }
        if (BHospital_IsUse == '否') {
            BHospital_IsUse = 0;
            r += '"IsUse":' + BHospital_IsUse + '';
        }
       
        r += '}}';
        if (errors > 0) {
            $.messager.alert('提示', '请检查输入值的完整性');
            errors = 0;
        } else {
            $.ajax({
                type: 'post',
                contentType: 'application/json',
                url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_AddBHospital',
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
      
        var BHospital_HospitalCode = $('#txtBHospital_HospitalCode').textbox('getValue');
        if (BHospital_HospitalCode == '') {
            errors += 1;
        } else {
            r += '"HospitalCode":"' + BHospital_HospitalCode + '",';
        }
        var BHospital_IconsID = $('#txtBHospital_IconsID').numberbox("getValue");
        if (BHospital_IconsID == '') {
            errors += 1;
        } else {
            r += '"IconsID":"' + BHospital_IconsID + '",';
        }
        var BHospital_HTypeID = $('#txtBHospital_HTypeID').numberbox("getValue");
        if (BHospital_HTypeID == '') {
            errors += 1;
        } else {
            r += '"HTypeID":"' + BHospital_HTypeID + '",';
        }
        var BHospital_LevelID = $('#txtBHospital_LevelID').numberbox("getValue");
        if (BHospital_LevelID == '') {
            errors += 1;
        } else {
            r += '"LevelID":"' + BHospital_LevelID + '",';
        }
        var BHospital_Name = $('#txtBHospital_Name').textbox('getValue');
        if (BHospital_Name) {
            r += '"Name":"' + BHospital_Name + '",';
        } else {
            errors += 1;
        }
        var BHospital_EName = $('#txtBHospital_EName').textbox('getValue');
        if (BHospital_EName) {
            r += '"EName":"' + BHospital_EName + '",';
        } else {
            errors += 1;
        }
        var BHospital_SName = $('#txtBHospital_SName').textbox('getValue');
        if (BHospital_SName) {
            r += '"SName":"' + BHospital_SName + '",';
        }
        var BHospital_Shortcode = $('#txtBHospital_Shortcode').textbox('getValue');
        if (BHospital_Shortcode) {
            r += '"Shortcode":"' + BHospital_Shortcode + '",';
        }
        var BHospital_PinYinZiTou = $('#txtBHospital_PinYinZiTou').textbox('getValue');
        if (BHospital_PinYinZiTou) {
            r += '"PinYinZiTou":"' + BHospital_PinYinZiTou + '",';
        }
        var BHospital_Comment = $('#txtBHospital_Comment').textbox('getValue');
        if (BHospital_Comment) {
            r += '"Comment":"' + BHospital_Comment + '",';
        }
        var BHospital_IsUse = $('#ddlBHospital_IsUse').combobox('getValue');
        if (BHospital_IsUse == '是') {
            BHospital_IsUse = 1;
            r += '"IsUse":' + BHospital_IsUse + '';
        }
        if (BHospital_IsUse == '否') {
            BHospital_IsUse = 0;
            r += '"IsUse":' + BHospital_IsUse + '';
        }
        
        r += '},"fields":"Id,Name,EName,SName,Shortcode,PinYinZiTou,Comment,IsUse"}';
        if (errors > 0) {
            $.messager.alert('提示', '请检查输入值的完整性');
            errors = 0;
        } else {
            $.ajax({
                type: 'post',
                contentType: 'application/json',
                url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_UpdateBHospitalByField',
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
    var delCount=0;
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
                        url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_DelBHospital?Id=' + rows[i].BHospital_Id,
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