var errors = 0;
var btnType;
var delCount = 0;
$(function () {
    $("#dg").datagrid({
        toolbar: "#toolbar",
        singleSelect: false,
        fit: true,
        fitColumns: true,
        border: false,
        pagination: true,
        rownumbers: true,
        collapsible: false,
        idField: 'AreaID',
        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetPubDict?tableName=' + "ClientEleArea",
       // url: 'data.txt',
        method: 'get',
        striped: true,
        columns: [[
                 { field: 'cb', checkbox: 'true' },
                { field: 'AreaID', title: '编码', width: '15%' },
                { field: 'AreaCName', title: '中文名称', width: '24%' },
                { field: 'AreaShortName', title: '简称', width: '24%' },
                {
                    field: 'ClientName', title: '区域总医院', width: '24%'
                },
                {
                    field: 'Operation', title: '操作', width: '8%', formatter: function (value, row, index) {
                        var edit = "<a href='#' data-options='iconCls:icon-edit' onclick='edit(" + index + "," + value + ")'>修改</a>";
                        return edit;
                    }
                }
        ]],
        loadFilter: function (data) {
            var result = eval("(" + data.ResultDataValue + ")");
            return { total: result.total || 0, rows: result.rows || [] };
        }
       
    })
    var p = $('#dg').datagrid('getPager');
    $(p).pagination({

        pageSize: 10, //每页显示的记录条数，默认为10           
        pageList: [10, 50, 100, 200, 500], //可以设置每页记录条数的列表           
        beforePageText: '第', //页数文本框前显示的汉字           
        afterPageText: '页    共 {pages} 页',
        displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录'
    });
    $('#txtAreaID').numberbox({
        validType: 'length[0,10]',
        onChange: function (newValue, oldValue) {
            var ItemNo = $('#txtAreaID').numberbox('getValue');
            if (btnType == 'add') {
                if (ItemNo.length > 10) {
                    $.ajax({
                        success: function () {
                            $('#txtAreaID').numberbox('clear');

                        }
                    })

                } else {
                    $.ajax({
                        type: 'get',
                        contentType: 'application/json',
                        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetPubDict',
                        data: { filerValue: newValue.trim(), tablename: "ClientEleArea", precisequery: "AreaID" },
                        //filerValue:newValue.trim(),tablename:"TestItem"
                        dataType: "json",
                        success: function (data) {
                            if (data.success) {
                                var data = eval('(' + data.ResultDataValue + ')'),
                               total = data.total || 0;
                                if (total) {
                                    $('#txtAreaID').numberbox('clear');

                                    $.messager.alert('提示', '数据库已存在此编号！不能重复插入', 'info');
                                }

                            }

                        }
                    });
                }
            }
        }
    });
    $('#ddlClientNo').combobox({
        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetPubDict?tableName=CLIENTELE&fields=ClIENTNO,CNAME',
        method: 'get',
        valueField: 'ClIENTNO',
        textField: 'CNAME',
        loadFilter: function (data) {
            var result = eval("(" + data.ResultDataValue + ")");
            return result.rows || [];
        }
    });
   
    $("#txtlabNo").combogrid({
        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetPubDict?tablename=CLIENTELE&fields=ClIENTNO,CNAME',
        panelWidth: 400,
        striped: true,
        rownumbers: true,
        multiple: true,
        singleSelect: false,
        method: 'get',
        idField: 'ClIENTNO',
        valueField: 'ClIENTNO',
        textField: 'CNAME',
        loadFilter: function (data) {
            var result = eval("(" + data.ResultDataValue + ")");

            return { total: result.total || 0, rows: result.rows || [] };
        },
        columns: [[
               { field: 'cb', checkbox: "true" },
               { field: 'CNAME', width: "80%", title: "实验室名称" }
        ]],

        filter: function (q, row) {
            var opts = $(this).combogrid('options');
            return row[opts.textField].indexOf(q) > -1;
        }


    });

    $("#copy").hide();

})
function del() {

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
                        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/DeleteClientEleAreaModelByID?areaID=' + rows[i].AreaID,
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
function add() {
    $('#txtAreaID').textbox('enable');
    $('#dlg').dialog('open').dialog('setTitle', '新增');
   
    $('#fm').form('clear');
   // $("#ddlVisible").combobox('select', '是');
   
    btnType = 'add';
}
function edit(index, value) {
    btnType = 'edit';
    var rowData = $("#dg").datagrid('getRows')[index];
    $('#fm').form('load', rowData)

    $('#dlg').dialog('open').dialog('setTitle', '修改');
    
    $('#txtAreaID').textbox('disable');
   
   
}
function update() {
    $('#dg').datagrid('load');
    $('#fm').form('clear')
    $('#txtSearchKey').searchbox("clear");
}
function doSearch() {
    
    var SH;
    var SearchKey = $("#txtSearchKey").val();
    if (SH == 0) {
        $('#txtSearchKey').searchbox('disable');
    } else {
        $('#txtSearchKey').searchbox('enable');
        $('#dg').datagrid({
            url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetPubDict',
            queryParams: {
                filerValue: SearchKey,
                tableName: 'ClientEleArea'
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
function save() {
    if (btnType == "edit")
    {
        $("#save").one('click', function (event) {
            event.preventDefault();
            
            $(this).prop('disabled', true);
        });
        var r = '';
        r += '{"jsonentity":{';
        var AreaID = $("#txtAreaID").val();
        if (AreaID == "") {
            errors += 1;
        }
        if (AreaID) {
            r += '"AreaID":' + AreaID + ',';
        }
        var AreaCName = $("#txtAreaCName").val();
        if (AreaCName == "") {
            errors += 1;
        }
        if (AreaCName) {
            r += '"AreaCName":"' + AreaCName + '",';
        }
        var AreaShortName = $("#txtAreaShortName").val();
        if (AreaShortName == "") {
            errors += 1;
        }
        if (AreaShortName) {
            r += '"AreaShortName":"' + AreaShortName + '",';
        }
        var ClientNo = $("#ddlClientNo").combobox('getValue');
        if (ClientNo) {
            r += '"ClientNo":' + ClientNo + ',';
        }
       // var r = '{"jsonentity":{"AreaID":"' + AreaID + '","AreaCName":"' + AreaCName + '","AreaShortName":"' + AreaShortName + '","ClientEle":"' + ClientEle + '"}}';
        r += '}}'
        if (errors > 0) {
            $.messager.alert('提示', '请检查输入值的完整性', 'warning');
            errors = 0;
        } else {
            $.ajax({

                type: 'post',
                contentType: 'application/json',
                url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/UpdateClientEleAreaModelByID',
                data: r,
                dataType: 'json',
                success: function (data) {
                    if (data.success == true) {
                        //$('#save').linkbutton('disable');
                        $('#dg').datagrid('load');
                        $('#dlg').dialog('close');
                        $('#dg').datagrid('unselectAll');

                    } else {
                        $.messager.alert('提示', '修改数据失败!失败信息:' + data.msg);
                    }
                }
            });
        }

    } else if (btnType == 'add')
    {

        $("#save").one('click', function (event) {
            event.preventDefault();

            $(this).prop('disabled', true);
        });
        var r = '';
        r += '{"jsonentity":{';
        var AreaID = $("#txtAreaID").val();
        if (AreaID == "") {
            errors += 1;
        }
        if (AreaID) {
            r += '"AreaID":' + AreaID + ',';
        }
        var AreaCName = $("#txtAreaCName").val();
        if (AreaCName == "") {
            errors += 1;
        }
        if (AreaCName) {
            r += '"AreaCName":"' + AreaCName + '",';
        }
        var AreaShortName = $("#txtAreaShortName").val();
        if (AreaShortName == "") {
            errors += 1;
        }
        if (AreaShortName) {
            r += '"AreaShortName":"' + AreaShortName + '",';
        }
        var ClientNo = $("#ddlClientNo").combobox('getValue');
        if (ClientNo) {
            r += '"ClientNo":' + ClientNo + ',';
        }
        // var r = '{"jsonentity":{"AreaID":"' + AreaID + '","AreaCName":"' + AreaCName + '","AreaShortName":"' + AreaShortName + '","ClientEle":"' + ClientEle + '"}}';
        r += '}}'
        if (errors > 0) {
            $.messager.alert('提示', '请检查输入值的完整性', 'warning');
            errors = 0;
        } else {
            $.ajax({
                type: 'post',
                contentType: 'application/json',
                // url: Shell.util.Path.rootPath + '',
                url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/AddClientEleAreaModel',
                data: r,
                dataType: "json",
                success: function (data) {
                    if (data.success == true) {
                        // $.messager.alert('提示', '插入数据成功！');
                        $('#dg').datagrid('load');
                        $('#dlg').dialog('close');
                        $('#dg').datagrid('unselectAll');
                    } else {
                        $.messager.alert('提示', '插入数据失败！失败信息：' + data.msg);
                    }
                }
            });
        }
    }
}