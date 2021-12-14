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
        idField: 'BProvince_Id',
        url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBProvinceByHQL',
        method: 'get',
        striped: true,
        columns: [[
                { field: 'BProvince_Id', hidden: true },
                { field: 'cb', checkbox: 'true' },
                { field: 'BProvince_Name', title: '名称', width: '13%' },
                { field: 'BProvince_EName', title: '英文名称', width: '13%' },
                { field: 'BProvince_SName', title: '简称', width: '13%' },
                { field: 'BProvince_Shortcode', title: '快捷码', width: '13%' },
                { field: 'BProvince_PinYinZiTou', title: '拼音字头', width: '13%' },
                { field: 'BProvince_Comment', title: '描述', width: '13%' },

                {
                    field: 'BProvince_IsUse', title: '是否使用', width: '11%', formatter: function (value) {
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
            isPlanish: true,
            fields: "BProvince_Id,BProvince_Name,BProvince_EName,BProvince_SName,BProvince_Shortcode,BProvince_PinYinZiTou,BProvince_Comment,BProvince_IsUse,BProvince_BCountry_Id"
           // fields: 'BProvince_Id,BProvince_Name,BProvince_BCountry_Id'
        },

        loadFilter: function (data) {
            var result = eval("(" + data.ResultDataValue + ")") || [];
            return { total: result.count || 0, rows: result.list || []} || [];
        }
    })
    $("#txtBProvince_BCountry_Id").combobox({
        url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBCountryByHQL?isPlanish='+true,
        method: 'get',
        valueField: 'BCountry_Id',
        textField: 'BCountry_Name',
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
        url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBProvinceByHQL?fields=' + SearchKey,
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
    $('#ddlBProvince_IsUse').combobox('select', '是');
    judge = 'add';
}
function edit(index, value) {
    judge = 'edit';
    var rowData = $("#dg").datagrid('getRows')[index];

    $('#fm').form('load', rowData)
    //var abs = $("#ddlBAccountType_IsUse").combobox('getValue');
    if ($("#ddlBProvince_IsUse").combobox('getValue') == 'false') {
        $("#ddlBProvince_IsUse").combobox('setValue', '否');
    } else {
        $("#ddlBProvince_IsUse").combobox('setValue', '是');
    }
    $('#dlg').dialog('open').dialog('setTitle', '修改');
    
}
function save() {
    if (judge == 'add') {
     //   $('#save').linkbutton('disable');
        var r = '';
        r += '{"entity":{';

        var BProvince_Name = $('#txtBProvince_Name').textbox('getValue');
        if (BProvince_Name) {
            r += '"Name":"' + BProvince_Name + '",';
        }else {
            errors+=1;
        }
        var BProvince_BCountry_Id = $('#txtBProvince_BCountry_Id').numberbox("getValue");
        if (BProvince_BCountry_Id) {
            var BProvince_DataTimeStamp = $('#txtBProvince_DataTimeStamp').datetimebox("getValue");
            r += '"BCountry":{"DataTimeStamp":"' + BProvince_DataTimeStamp + '","Id":"' + BProvince_BCountry_Id + '"},';
        }
        var BProvince_CountryID = $('#txtBProvince_BCountry_Id').numberbox("getValue");
        if (BProvince_CountryID) {
            r += '"CountryID":"' + BProvince_CountryID + '",';
        }
        var BProvince_EName = $('#txtBProvince_EName').textbox('getValue');
        if (BProvince_EName) {
            r += '"EName":"' + BProvince_EName + '",';
        } else {
            errors += 1;
        }
        var BProvince_SName = $('#txtBProvince_SName').textbox('getValue');
        if (BProvince_SName) {
            r += '"SName":"' + BProvince_SName + '",';
        }
        var BProvince_Shortcode = $('#txtBProvince_Shortcode').textbox('getValue');
        if (BProvince_Shortcode) {
            r += '"Shortcode":"' + BProvince_Shortcode + '",';
        }
        var BProvince_PinYinZiTou = $('#txtBProvince_PinYinZiTou').textbox('getValue');
        if (BProvince_PinYinZiTou) {
            r += '"PinYinZiTou":"' + BProvince_PinYinZiTou + '",';
        }
        var BProvince_Comment = $('#txtBProvince_Comment').textbox('getValue');
        if (BProvince_Comment) {
            r += '"Comment":"' + BProvince_Comment + '",';
        }
        var BProvince_IsUse = $('#ddlBProvince_IsUse').combobox('getValue');
        if (BProvince_IsUse == '是') {
            BProvince_IsUse = 1;
             r += '"IsUse":' + BProvince_IsUse + '';
        }
        if (BProvince_IsUse == '否') {
            BProvince_IsUse = 0;
             r += '"IsUse":' + BProvince_IsUse + '';
        }
      
        r += '}}';
        if (errors > 0) {
            $.messager.alert('提示', '请检查输入值的完整性');
            errors = 0;
        } else {
            $.ajax({
                type: 'post',
                contentType: 'application/json',
                url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_AddBProvince',
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

        var BProvince_Id = $('#txtBProvince_Id').numberbox("getValue");
        if (BProvince_Id == '') {
            errors += 1;
        } else {
            r += '"Id":"' + BProvince_Id + '",';
           }

        var BProvince_BCountry_Id = $('#txtBProvince_BCountry_Id').combobox("getValue");
        if (BProvince_BCountry_Id == '') {
            errors += 1;
        } else {
            r += '"CountryID":"' + BProvince_BCountry_Id + '",';
        }
        var BProvince_Name = $('#txtBProvince_Name').textbox('getValue');
        if (BProvince_Name) {
            r += '"Name":"' + BProvince_Name + '",';
        }else{
            errors+=1;
        }
        var BProvince_EName = $('#txtBProvince_EName').textbox('getValue');
        if (BProvince_EName) {
            r += '"EName":"' + BProvince_EName + '",';
        }else{
            errors+=1;
        }
        var BProvince_SName = $('#txtBProvince_SName').textbox('getValue');
        if (BProvince_SName) {
            r += '"SName":"' + BProvince_SName + '",';
        }
        var BProvince_Shortcode = $('#txtBProvince_Shortcode').textbox('getValue');
        if (BProvince_Shortcode) {
            r += '"Shortcode":"' + BProvince_Shortcode + '",';
        }
        var BProvince_PinYinZiTou = $('#txtBProvince_PinYinZiTou').textbox('getValue');
        if (BProvince_PinYinZiTou) {
            r += '"PinYinZiTou":"' + BProvince_PinYinZiTou + '",';
        }
        var BProvince_Comment = $('#txtBProvince_Comment').textbox('getValue');
        if (BProvince_Comment) {
            r += '"Comment":"' + BProvince_Comment + '",';
        }
        var BProvince_IsUse = $('#ddlBProvince_IsUse').combobox('getValue');
        if (BProvince_IsUse == '是') {
            BProvince_IsUse = 1;
             r += '"IsUse":' + BProvince_IsUse + '';
        }
        if (BProvince_IsUse == '否') {
            BProvince_IsUse = 0;
             r += '"IsUse":' + BProvince_IsUse + '';
        }
       
        r += '},"fields":"Id,Name,EName,SName,Shortcode,PinYinZiTou,Comment,IsUse"}';
        if (errors > 0) {
            $.messager.alert('提示', '请检查输入值的完整性');
            errors = 0;
        } else {
            $.ajax({
                type: 'post',
                contentType: 'application/json',
                url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_UpdateBProvinceByField',
                data: r,
                dataType: 'json',
                success: function (data) {
                   // $('#save').linkbutton('enable');
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
                        url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_DelBProvince?Id=' + rows[i].BProvince_Id,
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