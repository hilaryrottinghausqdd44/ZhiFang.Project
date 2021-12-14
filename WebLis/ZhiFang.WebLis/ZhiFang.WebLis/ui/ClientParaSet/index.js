var btnType;
var errors = 0;
var delCount = 0;
$(function () {
    $('#dg').datagrid({
        toolbar: "#toolbar",
        width: 290,
        remoteSort: false,
        singleSelect: true,
        nowrap: false,
        fit: true,
        fitColumns: true,
        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/SearchBBClientParaGroupByName?Name=',
        method: 'get',
        columns: [[
            { field: 'Name', title: '参数名', width: 80 },
            { field: 'ParaNo', title: '参数编码', width: 100, sortable: true },
            { field: 'ParaDesc', title: '参数编码', width: 100, hidden: true }
        ]],
        loadFilter: function (data) {
            var result = eval("(" + data.ResultDataValue + ")");
            return { total: result.total || 0, rows: result.rows || [] };
        },
        onLoadSuccess: function (data) {
            $('#dg').datagrid('selectRow', 0);
            //doSearchsub();
        },
        onSelect: function (data) {
            doSearchsub();
        },
    });
    $('#dgsub').datagrid({
        toolbar: "#toolbarsub",
        width: 290,
        remoteSort: false,
        singleSelect: false,
        nowrap: false,
        fit: true,
        fitColumns: true,
        method: 'get',
        columns: [[
            { field: 'cb', checkbox: 'true' },
            { field: 'ParaValue', title: '参数值', width: 180 },
            { field: 'LabName', title: '实验室', width: 300, sortable: true }
        ]],
        loadFilter: function (data) {
            var result = eval("(" + data.ResultDataValue + ")");
            return { total: result.total || 0, rows: result.rows || [] };
        }
    });
    $('#ddlLab').combobox({
        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetPubDict?tableName=' + "CLIENTELE&page=0&rows=10000",
        method: 'GET',
        valueField: 'ClIENTNO',
        textField: 'CNAME',
        loadFilter: function (data) {
            var result = eval("(" + data.ResultDataValue + ")");
            //alert(data.ResultDataValue);
            if (result.total > 0) {
                result.rows[0].selected = true;
            }
            return result.rows;
        }
    });
})
function doSearch() {

    var SH;
    var SearchKey = $("#txtSearchKey").val();
    if (SH == 0) {
        $('#txtSearchKey').searchbox('disable');
    } else {
        $('#txtSearchKey').searchbox('enable');
        $('#dg').datagrid({
            url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/SearchBBClientParaGroupByName',
            queryParams: {
                Name: SearchKey
            },
            loadFilter: function (data) {
                var result = eval("(" + data.ResultDataValue + ")");
                return { total: result.total || 0, rows: result.rows || [] };
            },
            onBeforeLoad: function () {
                SH = 0;
            },
            onLoadSuccess: function (data) {
                SH = 1;
                $('#txtSearchKey').searchbox("clear");
            }
        });
    }
}
function update() {
    $('#dg').datagrid('load');
    $('#fm').form('clear')
    $('#txtSearchKey').searchbox("clear");
}
function doSearchsub() {
    var SH;
    var SearchKey = $("#txtSearchKeysub").val();
    //alert(SearchKey);
    if (SH == 0) {
        $('#txtSearchKeysub').searchbox('disable');
    } else {
        $('#txtSearchKeysub').searchbox('enable');
        $('#dgsub').datagrid({
            url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/SearchBBClientParaByParaNoAndLabIDAndLabName',
            queryParams: {
                LabName: SearchKey,
                ParaNo: $('#dg').datagrid('getSelections')[0].ParaNo
            },
            loadFilter: function (data) {
                if (data.success) {
                    if (data.ResultDataValue != "") {
                        var result = eval("(" + data.ResultDataValue + ")");
                        return { total: result.total || 0, rows: result.rows || [] };
                    }
                    else {
                        alert("查询错误！未找到相关数据！");
                        $('#txtSearchKeysub').searchbox("clear");
                    }
                }
                else {
                    alert("查询错误！未找到相关数据！");
                    $('#txtSearchKeysub').searchbox("clear");
                }
            },
            onBeforeLoad: function () {
                SH = 0;
            },
            onLoadSuccess: function (data) {
                SH = 1;
                $('#txtSearchKeysub').searchbox("clear");
            }
        });
    }
}
function updatesub() {
    $('#dgsub').datagrid('load');
    $('#fm').form('clear')
    $('#txtSearchKeysub').searchbox("clear");

}
function del() {

    var rows = $('#dgsub').datagrid('getSelections');
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
                        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/DeleteBBClientPara?Id=' + rows[i].ParameterID,
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
                    $('#dg').datagrid('reload');
                    $('#dgsub').datagrid('clearSelections');
                    $('#dgsub').datagrid('reload');//因为getSelections会记忆选过的记录，所以要清空一下
                }

            }
        });
    }
}
function add() {
    $('#txtGenderNo').textbox('enable');
    $('#dlg').dialog('open').dialog('setTitle', '新增');
    $('#fm').form('clear');
    var row = $('#dg').datagrid('getSelected')
    if (row) {
        $('#TxtName').textbox('setValue', row.Name);
        $('#TxtParaNo').textbox('setValue', row.ParaNo);
        $('#TxtParaDesc').textbox('setValue', row.ParaDesc);
    }
    $("#ddlVisible").combobox('select', '是');

    btnType = 'add';
}
function save() {
    if (btnType == 'edit') {
        $("#save").one('click', function (event) {
            event.preventDefault();

            $(this).prop('disabled', true);
        });
        var r = '';
        r += '{"jsonentity":{';
        var GenderNo = $("#txtGenderNo").val();
        if (GenderNo == "") {
            errors += 1;
        }
        if (GenderNo) {
            r += '"GenderNo":' + GenderNo + ',';
        }

        var CName = $("#txtCName").val();
        if (CName == "") {
            errors += 1;
        }
        if (CName) {
            r += '"CName":"' + CName + '",';
        }
        var ShortCode = $("#txtShortCode").val();
        if (ShortCode == "") {
            errors += 1;
        }
        if (ShortCode) {
            r += '"ShortCode":"' + ShortCode + '",';
        }
        var DispOrder = $("#txtDispOrder").val();
        if (DispOrder) {
            r += '"DispOrder":' + DispOrder + ',';
        }
        var HisOrderCode = $("#txtHisOrderCode").val();
        if (HisOrderCode) {
            r += '"HisOrderCode":"' + HisOrderCode + '",';
        }
        var Visible = $("#txtVisible").val();
        if (Visible == "是") {
            Visible = 1;
        }
        else if (Visible == "否") {
            Visible = 0;
        }
        if (Visible) {
            r += '"Visible":' + Visible + ',';
        }
        // var r = '{"jsonentity":{"DistrictNo":"' + DistrictNo + '","ShortName":"' + ShortName + '","CName":"' + CName + '","ShortCode":"' + ShortCode + '","DispOrder":"' + DispOrder + '","HisOrderCode":"' + HisOrderCode + '","Visible":"' + Visible + '"}}';
        r += '}}'
        if (errors > 0) {

            $.messager.alert('提示', '请检查输入值的完整性', 'warning');
            errors = 0;
        } else {

            $.ajax({
                type: 'post',
                contentType: 'application/json',
                url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/UpdateGenderTypeModelByID',
                data: r,
                dataType: 'json',
                success: function (data) {
                    if (data.success == true) {

                        $('#dg').datagrid('load');
                        $('#dlg').dialog('close');
                        $('#dg').datagrid('unselectAll');
                    } else {
                        $.messager.alert('提示', '修改数据失败！失败信息：' + data.msg);
                    }
                }
            })
        }

    }
    else if (btnType == 'add') {
        $("#save").one('click', function (event) {
            event.preventDefault();

            $(this).prop('disabled', true);
        });
        var r = '';
        r += '{"jsonentity":{';
        var LabID = $("#ddlLab").combobox('getValue');
        if (LabID) {
            r += '"LabID":' + LabID + ',';
        }
        else {
            errors++;
        }
        var Name = $("#TxtName").val();
        if (Name) {
            r += '"Name":"' + Name + '",';
        }
        var ParaNo = $("#TxtParaNo").val();

        if (ParaNo) {
            r += '"ParaNo":"' + ParaNo + '",';
        }
        var ParaValue = $("#TxtParaValue").val();
        if (ParaValue) {
            r += '"ParaValue":"' + ParaValue + '",';
        }
        var ParaDesc = $("#TxtParaDesc").val();
        if (ParaDesc) {
            r += '"ParaDesc":"' + ParaDesc + '"';
        }
        r += '}}'
        if (errors > 0) {

            $.messager.alert('提示', '请检查输入值的完整性', 'warning');
            errors = 0;
        } else {
            $.ajax({
                type: 'post',
                contentType: 'application/json',
                url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/AddBBClientPara',
                data: r,
                dataType: "json",
                success: function (data) {
                    if (data.success == true) {
                        // $.messager.alert('提示', '插入数据成功！');
                        $('#dg').datagrid('load');
                        $('#dgsub').datagrid('load');
                        $('#dlg').dialog('close');
                        $('#dg').datagrid('unselectAll');
                        $('#dgsub').datagrid('unselectAll');
                    } else {
                        $.messager.alert('提示', '插入数据失败！失败信息：' + data.msg);
                    }
                }
            });
        }
    }

}