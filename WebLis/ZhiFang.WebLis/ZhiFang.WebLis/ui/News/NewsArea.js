var btnType;
var errors = 0;
var delCount = 0;
var searchurl = Shell.util.Path.rootPath + '/ServiceWCF/NewsService.svc/ST_UDTO_SearchNNewsAreaByHQL';
var addurl = Shell.util.Path.rootPath + '/ServiceWCF/NewsService.svc/ST_UDTO_AddNNewsArea';
var updateurl = Shell.util.Path.rootPath + '/ServiceWCF/NewsService.svc/ST_UDTO_UpdateNNewsArea';
var delurl = Shell.util.Path.rootPath + '/ServiceWCF/NewsService.svc/ST_UDTO_DelNNewsArea';
var searchcurl = Shell.util.Path.rootPath + '/ServiceWCF/NewsService.svc/ST_UDTO_SearchNNewsAreaClientLinkByHQL';
var addcurl = Shell.util.Path.rootPath + '/ServiceWCF/NewsService.svc/ST_UDTO_AddNNewsAreaClientLink';
var delcurl = Shell.util.Path.rootPath + '/ServiceWCF/NewsService.svc/ST_UDTO_DelNNewsAreaClientLink';
var searchcUnselectlisturl = Shell.util.Path.rootPath + '/ServiceWCF/NewsService.svc/ST_UDTO_SearchUnSelectListNNewsAreaClientLink';
$(function () {
    $("#dg").datagrid({
        toolbar: "#toolbar",
        singleSelect: true,
        fit: true,
        border: false,
        pagination: true,
        rownumbers: true,
        collapsible: false,
        idField: 'NewsAreaId',
        url: searchurl,
        method: 'get',
        striped: true,
        pageSize: 50, //每页显示的记录条数，默认为10           
        pageList: [10, 50, 100, 200, 500], //可以设置每页记录条数的列表           
        beforePageText: '第', //页数文本框前显示的汉字           
        afterPageText: '页    共 {pages} 页',
        displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
        columns: [[
             //{ field: 'cb', checkbox: 'true' },
             { field: 'NewsAreaId', title: 'ID', hidden: true },
             { field: 'CName', title: '中文名称', width: '20%' },
             { field: 'SName', title: '简称', width: '20%' },
             { field: 'ShortCode', title: '简码', width: '20%' },
             { field: 'DispOrder', title: '序号', width: '20%' },
             { field: 'Memo', title: '描述', hidden: true },
             {
                 field: 'IsUse', title: '是否在用',
                 formatter: function (value, row, index) {
                     if (value == 0) {
                         return row.IsUse = '否';
                     }
                     if (value == 1) {
                         return row.IsUse = '是';
                     }
                 },
                 styler: function (value, row, index) {
                     if (value == '0') {
                         return 'background-color:red;';
                     }
                     //if (value=='1') {
                     //    return 'background-color:#FFF2CC;';
                     //}
                     return "";
                 }
             }

        ]],
        loadFilter: function (data) {
            var result = eval("(" + data.ResultDataValue + ")");
            return { total: result.total || 0, rows: result.rows || [] };
        },
        onClickRow: function (rowIndex, rowData) {
            var AreaId = rowData.NewsAreaId;
            $('#dgc').datagrid({
                url: searchcurl,
                queryParams: {
                    where: " NewsAreaId = " + AreaId + ""
                },
                loadFilter: function (data) {
                    var result = eval("(" + data.ResultDataValue + ")");
                    return { total: result.total || 0, rows: result.rows || [] };
                },
                onBeforeLoad: function () {

                },
                onLoadSuccess: function (data) {

                }
            });
        }


    });

    $("#dgc").datagrid({
        toolbar: "#toolbarc",
        singleSelect: false,
        fit: true,
        border: false,
        pagination: true,
        rownumbers: true,
        collapsible: false,
        idField: 'NewsAreaClientLinkId',
        //url: searchcurl,
        method: 'get',
        striped: true,
        pageSize: 100, //每页显示的记录条数，默认为10           
        pageList: [100, 200, 500], //可以设置每页记录条数的列表           
        beforePageText: '第', //页数文本框前显示的汉字           
        afterPageText: '页    共 {pages} 页',
        displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
        columns: [[
             { field: 'cb', checkbox: 'true' },
             { field: 'NewsAreaClientLinkId', title: 'ID', hidden: true },
             { field: 'ClientNo', title: '编号', width: '20%' },
             { field: 'ClientName', title: '名称', width: '80%' }
        ]],
        loadFilter: function (data) {
            var result = eval("(" + data.ResultDataValue + ")");
            return { total: result.total || 0, rows: result.rows || [] };
        }
    });


    $("#dgunc").datagrid({
        toolbar: "#toolbarunc",
        singleSelect: false,
        fit: true,
        border: false,
        pagination: true,
        rownumbers: true,
        collapsible: false,
        idField: 'ClIENTNO',
        //url: searchcUnselectlisturl,
        method: 'get',
        striped: true,
        pageSize: 100, //每页显示的记录条数，默认为10           
        pageList: [100, 200, 500], //可以设置每页记录条数的列表           
        beforePageText: '第', //页数文本框前显示的汉字           
        afterPageText: '页    共 {pages} 页',
        displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
        columns: [[
             { field: 'cb', checkbox: 'true' },
             { field: 'ClIENTNO', title: '编码', hidden: false },
             { field: 'CNAME', title: '中文名称', width: '80%' }

        ]],
        loadFilter: function (data) {
            var result = eval("(" + data.ResultDataValue + ")");
            return { total: result.total || 0, rows: result.rows || [] };
        },
        onClickRow: function (rowIndex, rowData) {

        }
    });

})

function del() {
    var rows = $('#dg').datagrid('getSelections');
    if (rows.length == 0) {
        $.messager.alert("提示", "请勾选需要禁用的数据!")
        return;
    } else {
        $.messager.confirm('提示', '确定要禁用?', function (r) {
            if (r) {
                for (var i = 0; i < rows.length; i++) {
                    $.ajax({
                        type: 'get',
                        contentType: 'application/json',
                        url: delurl + '?id=' + rows[i].NewsAreaId,
                        dataType: 'json',
                        async: false,
                        success: function (data) {
                            if (data.success == true) {
                                //$.messager.alert('提示', '删除数据成功！');
                                //$('#dg').datagrid('reload');
                                delCount++;
                            } else {
                                $.messager.alert('提示', '禁用数据失败！失败信息：' + data.ErrorInfo);
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
    //$('#txtNewsAreaId').textbox('enable');
    $('#dlg').dialog({
        title: "新增",
        modal: true,
        toolbar: $('#dlgButtons')
    }).dialog('open');
    $('#fm').form('clear');
    $("#ddlIsUse").combobox('select', "1");

    btnType = 'add';
}
function edit() {
    var rowData = $("#dg").datagrid('getSelected');
    if (rowData) {
        $('#fm').form('load', rowData)

        $('#dlg').dialog({
            title: "修改",
            modal: true,
            toolbar: $('#dlgButtons')
        }).dialog('open');
        //$('#txtNewsAreaId').textbox('disable');
        btnType = 'edit';
    }
    else {
        $.messager.alert("提示", "请选择需要编辑的数据!")
        return;
    }

}
function refresh() {
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
            url: searchurl,
            queryParams: {
                where: " CName like '%" + SearchKey + "%' or SName like '%" + SearchKey + "%' or ShortCode like '%" + SearchKey + "%'"
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
    var entity = {};
    if (btnType == "edit") {
        $("#save").one('click', function (event) {
            event.preventDefault();

            $(this).prop('disabled', true);
        });

        var NewsAreaId = $("#txtNewsAreaId").val();

        if (NewsAreaId) {
            entity.NewsAreaId = NewsAreaId;
        }

        var SName = $("#txtSName").val();

        if (SName) {
            entity.SName = SName;
        }

        var CName = $("#txtCName").val();
        if (CName == "") {
            errors += 1;
        }
        if (CName) {
            entity.CName = CName;
        }
        var txtMemo = $("#txtMemo").val();
        if (txtMemo) {
            entity.Memo = txtMemo;
        }
        var DispOrder = $("#txtDispOrder").val();//int
        if (DispOrder) {
            entity.DispOrder = DispOrder;
        }
        var Visible = $("#ddlIsUse").combobox('getValue');//int
        if (Visible == "是") {
            Visible = 1;
        }
        else if (Visible == "否") {
            Visible = 0;
        }
        if (Visible >= 0) {
            entity.IsUse = Visible;
        }
        var ShortCode = $("#txtShortCode").val();

        if (ShortCode) {
            entity.ShortCode = ShortCode;
        }
        if (errors > 0) {
            $.messager.alert('提示', '请输入值的完整性', 'warning');
            errors = 0;
        } else {
            $.ajax({
                type: 'post',
                contentType: 'application/json',
                url: updateurl,
                data: Shell.util.JSON.encode({ entity: entity }),
                dataType: 'json',
                success: function (data) {
                    if (data.success == true) {
                        $('#dg').datagrid('load');
                        $('#dg').datagrid('unselectAll');
                        $('#fm').form('clear');
                        $('#dlg').dialog('close');
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

        var SName = $("#txtSName").val();

        if (SName) {
            entity.SName = SName;
        }

        var CName = $("#txtCName").val();
        if (CName == "") {
            errors += 1;
        }
        if (CName) {
            entity.CName = CName;
        }
        var txtMemo = $("#txtMemo").val();
        if (txtMemo) {
            entity.Memo = txtMemo;
        }
        var DispOrder = $("#txtDispOrder").val();//int
        if (DispOrder) {
            entity.DispOrder = DispOrder;
        }
        var Visible = $("#ddlIsUse").combobox('getValue');//int
        if (Visible == "是") {
            Visible = 1;
        }
        else if (Visible == "否") {
            Visible = 0;
        }
        if (Visible >= 0) {
            entity.IsUse = Visible;
        }
        var ShortCode = $("#txtShortCode").val();

        if (ShortCode) {
            entity.ShortCode = ShortCode;
        }
        if (errors > 0) {
            $.messager.alert('提示', '请输入值的完整性', 'warning');
            errors = 0;
        } else {
            $.ajax({
                type: 'post',
                contentType: 'application/json',
                url: addurl,
                data: Shell.util.JSON.encode({ entity: entity }),
                dataType: "json",
                success: function (data) {
                    if (data.success == true) {
                        // $.messager.alert('提示', '插入数据成功！');
                        $('#dg').datagrid('load');
                        $('#dg').datagrid('unselectAll');
                        $('#fm').form('clear');
                        $('#dlg').dialog('close');
                    } else {
                        $.messager.alert('提示', '插入数据失败！失败信息：' + data.msg);
                    }
                }
            });
        }
    }

}
function cancel() {
    btnType = "";
    $('#fm').form('clear');
}

function refreshc() {
    $('#dgc').datagrid('load');
}

function addc() {
    var rows = $('#dg').datagrid('getSelections');
    if (rows.length == 0) {
        $.messager.alert("提示", "请选择区域数据!")
        return;
    }
    else {
        $('#dlgc').dialog({
            title: "添加客户",
            modal: true
        }).dialog('open');
        $('#dgunc').datagrid({
            url: searchcUnselectlisturl,
            queryParams: {
                NewsAreaId: rows[0].NewsAreaId
            },
            loadFilter: function (data) {
                var result = eval("(" + data.ResultDataValue + ")");
                return { total: result.total || 0, rows: result.rows || [] };
            }
        });
    }
}

function delc() {
    var rows = $('#dgc').datagrid('getSelections');
    if (rows.length == 0) {
        $.messager.alert("提示", "请选择要删除的数据!")
        return;
    }
    else {
        for (var i = 0; i < rows.length; i++) {
            $.ajax({
                type: 'get',
                contentType: 'application/json',
                url: delcurl + '?id=' + rows[i].NewsAreaClientLinkId,
                dataType: 'json',
                async: false,
                success: function (data) {
                    if (data.success == true) {
                        //$.messager.alert('提示', '删除数据成功！');
                        //$('#dg').datagrid('reload');
                        
                    } else {
                        $.messager.alert('提示', '禁用数据失败！失败信息：' + data.ErrorInfo);
                    }
                }
            });
        }
        $('#dgc').datagrid('clearSelections');
        $('#dgc').datagrid('reload');
    }
}

function savec() {
    
    $("#save").one('click', function (event) {
        event.preventDefault();
        $(this).prop('disabled', true);
    });

    var rows = $('#dgunc').datagrid('getSelections');
    if (rows.length == 0) {
        $.messager.alert("提示", "请选择要添加的数据!")
        return;
    }
    else {
        var dgrows = $('#dg').datagrid('getSelections');
        if (dgrows.length == 0) {
            $.messager.alert("提示", "请选择区域数据!")
            return;
        }
        for (var i = 0; i < rows.length; i++)
        {
            var entity = {};
            entity.NewsAreaId = dgrows[0].NewsAreaId;
            entity.NewsAreaName = dgrows[0].NewsAreaName;
            entity.ClientNo = rows[i].ClIENTNO;
            entity.ClientName = rows[i].CNAME;
            $.ajax({
                type: 'post',
                contentType: 'application/json',
                url: addcurl,
                data: Shell.util.JSON.encode({ entity: entity }),
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.success == true) {
                        // $.messager.alert('提示', '插入数据成功！');
                        $('#dgc').datagrid('reload');
                        $('#dgc').datagrid('unselectAll');
                    } else {
                        $.messager.alert('提示', '插入数据失败！失败信息：' + data.ErrorInfo);
                    }
                }
            });            
        }
        $('#dgunc').datagrid('reload');
        $('#dgunc').datagrid('unselectAll');
        $('#dlgc').dialog('close');
    }
}

function dguncSearch() {
    var rows = $('#dg').datagrid('getSelections');
    var SH;
    var SearchKey = $("#dguncSearchKey").val();
    if (SH == 0) {
        $('#dguncSearchKey').searchbox('disable');
    } else {
        var where = "";
        if (SearchKey != "")
        {
            where=" CName like '%" + SearchKey + "%' or ClientNo like '%" + SearchKey + "%' or ShortCode like '%" + SearchKey + "%'"
        }
        $('#dguncSearchKey').searchbox('enable');
        $('#dgunc').datagrid({
            url: searchcUnselectlisturl,
            queryParams: {
                NewsAreaId: rows[0].NewsAreaId,
                where:where
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
                $('#dguncSearchKey').searchbox("clear");
            }
        });
    }
}