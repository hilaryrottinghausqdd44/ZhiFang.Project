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
        idField: 'BAccountType_Id',
        url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBAccountTypeByHQL',
        method: 'get',
        striped: true,
        columns: [[
        //BAccountType_Id
                { field: 'BAccountType_LabID', hidden: true },
                { field: 'BAccountType_Id', hidden: true },
                { field: 'AccountTypeID', hidden: true },
                { field: 'cb', checkbox: 'true' },
                { field: 'BAccountType_Name', title: '名称', width: '12%' },
                { field: 'BAccountType_EName', title: '英文名称', width: '12%' },
                { field: 'BAccountType_SName', title: '简称', width: '12%' },
                { field: 'BAccountType_Shortcode', title: '快捷码', width: '12%' },
                { field: 'BAccountType_PinYinZiTou', title: '拼音字头', width: '12%' },
                { field: 'BAccountType_Comment', title: '描述', width: '19%' },
                { field: 'BAccountType_DataAddTime', hidden:true },
                { field: 'BAccountType_DataTimeStamp', hidden: true },
                {
                    field: 'BAccountType_IsUse', title: '是否使用', width: '12%', formatter: function (value) {
                        if (value == 'false') {
                            value = "否";
                        } else if(value=='true'){
                            value = "是";
                        }
                        return value;
                    }
                },

                {
                    field: 'Operation', title: '操作', width: '5.5%', formatter: function (value, row, index) {
                        var edit = "<a href='#' data-options='iconCls:icon-edit' onclick='edit(" + index + "," + value + ")'>修改</a>";
                        return edit;
                    }
                }
        ]],
        queryParams: {
		isPlanish:true
	    },

        loadFilter: function (data) {
            var result = eval("(" + data.ResultDataValue + ")")||[];
            return { total: result.count || 0, rows: result.list || [] }||[];
        }
    })
})
function doSearch(value) {
  //  $('#txtSearchKey').searchbox('disable');
    var SearchKey = $("#txtSearchKey").searchbox("getValue");
    $('#dg').datagrid({
        url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBAccountTypeByHQL?fields=' + SearchKey,
        loadFilter: function (data) {
            var result = eval("(" + data.ResultDataValue + ")")||[];
            return { total: result.count || 0, rows: result.list || [] }||[];
        }
       
    });
   
}
function add() {
    $('#dlg').dialog('open').dialog('setTitle', '新增');

    $('#fm').form('clear');
    $('#ddlBAccountType_IsUse').combobox('select', '是');
    judge = 'add';
}
function edit(index, value) {
    judge = 'edit';
    var rowData = $("#dg").datagrid('getRows')[index];

    $('#fm').form('load', rowData)
    //var abs = $("#ddlBAccountType_IsUse").combobox('getValue');
    if ($("#ddlBAccountType_IsUse").combobox('getValue') == 'false') {
        $("#ddlBAccountType_IsUse").combobox('setValue', '否');
    } else {
        $("#ddlBAccountType_IsUse").combobox('setValue', '是');
               }
    $('#dlg').dialog('open').dialog('setTitle', '修改');
    
   
}
function save() {
    if (judge == 'add') {
       // $('#save').linkbutton('disable');
        var r = '';
        r += '{"entity":{';

        var BAccountType_Name = $('#txtBAccountType_Name').textbox('getValue');
        if (BAccountType_Name == '') {
            errors += 1;
        } else {
            r += '"Name":"' + BAccountType_Name + '",';
        }
        var BAccountType_EName = $('#txtBAccountType_EName').textbox('getValue');
        if (BAccountType_EName == '') {
            errors += 1;
        } else {
            r += '"EName":"' + BAccountType_EName + '",';
        }
        var BAccountType_SName = $('#txtBAccountType_SName').textbox('getValue');
        if (BAccountType_SName == '') {
            errors += 1;
        } else {
            r += '"SName":"' + BAccountType_SName + '",';
        }
        var BAccountType_Shortcode = $('#txtBAccountType_Shortcode').textbox('getValue');
        if (BAccountType_Shortcode) {
            r += '"Shortcode":"' + BAccountType_Shortcode + '",';
        }
        var BAccountType_PinYinZiTou = $('#txtBAccountType_PinYinZiTou').textbox('getValue');
        if (BAccountType_PinYinZiTou) {
            r += '"PinYinZiTou":"' + BAccountType_PinYinZiTou + '",';
        }
        var BAccountType_Comment = $('#txtBAccountType_Comment').textbox('getValue');
        if (BAccountType_Comment) {
            r += '"Comment":"' + BAccountType_Comment + '",';
        }
        var BAccountType_IsUse = $('#ddlBAccountType_IsUse').combobox('getValue');
        if (BAccountType_IsUse == '是') {
            BAccountType_IsUse = 1;
            r += '"IsUse":' + BAccountType_IsUse + ',';
        } else if (BAccountType_IsUse == '否') {
            BAccountType_IsUse = 0;
            r += '"IsUse":' + BAccountType_IsUse + ',';
        }
               
        r += '}}';
        if (errors > 0) {
            $.messager.alert('提示', '请检查输入值的完整性');
            errors = 0;
        } else {
            $.ajax({
                type: 'post',
                contentType: 'application/json',
                url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_AddBAccountType',
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
        var BAccountType_Id = $('#txtBAccountType_Id').textbox("getValue");
        if (BAccountType_Id) {
            r += '"Id":"' + BAccountType_Id + '",';
        }
        var BAccountType_Name = $('#txtBAccountType_Name').textbox('getValue');
        if (BAccountType_Name == '') {
            errors += 1;
        } else {
            r += '"Name":"' + BAccountType_Name + '",';
        }
        var BAccountType_EName = $('#txtBAccountType_EName').textbox('getValue');
        if (BAccountType_EName == '') {
            errors += 1;
        } else {
            r += '"EName":"' + BAccountType_EName + '",';
        }
        var BAccountType_SName = $('#txtBAccountType_SName').textbox('getValue');
        if (BAccountType_SName == '') {
            errors += 1;
        } else {
            r += '"SName":"' + BAccountType_SName + '",';
        }
        var BAccountType_Shortcode = $('#txtBAccountType_Shortcode').textbox('getValue');
        if (BAccountType_Shortcode) {
            r += '"Shortcode":"' + BAccountType_Shortcode + '",';
        }
        var BAccountType_PinYinZiTou = $('#txtBAccountType_PinYinZiTou').textbox('getValue');
        if (BAccountType_PinYinZiTou) {
            r += '"PinYinZiTou":"' + BAccountType_PinYinZiTou + '",';
        }
        var BAccountType_Comment = $('#txtBAccountType_Comment').textbox('getValue');
        if (BAccountType_Comment) {
            r += '"Comment":"' + BAccountType_Comment + '",';
        }
        var BAccountType_IsUse = $('#ddlBAccountType_IsUse').combobox('getValue');
        if (BAccountType_IsUse == '是') {
            BAccountType_IsUse = 1;
            r += '"IsUse":' + BAccountType_IsUse + '';
        }else if (BAccountType_IsUse == '否') {
            BAccountType_IsUse = 0;
            r += '"IsUse":' + BAccountType_IsUse + '';
        }

        r += '},"fields":"Id,Name,EName,SName,Shortcode,PinYinZiTou,Comment,IsUse"}';
        if (errors > 0) {
            $.messager.alert('提示','请检查输入值的完整性');
            errors = 0;
        } else {
            $.ajax({
                type: 'post',
                contentType: 'application/json',
                url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_UpdateBAccountTypeByField',
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
    var delCount=0;
   var rows = $('#dg').datagrid('getSelections');
   // var AccountTypeID=
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
                        url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_DelBAccountType?Id=' + rows[i].BAccountType_Id,
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