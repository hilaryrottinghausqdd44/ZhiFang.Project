var btnType;
var errors = 0;
var delCount = 0;
var selecttype = "all";
var delCount = 0;
var tmpitemdg = [];
//列表全部数据
var listColors;
var searchurl = Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetAllItemColorDict';
var searchitemurl = Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetItemListByColor';
var setitemcolorurl = Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/SetItemColorByItemNoList';



var addcurl = Shell.util.Path.rootPath + '/ServiceWCF/NewsService.svc/ST_UDTO_AddNNewsAreaClientLink';
var delcurl = Shell.util.Path.rootPath + '/ServiceWCF/NewsService.svc/ST_UDTO_DelNNewsAreaClientLink';
var searchallitemurl = Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetItemListByColor';
$(function () {
    $("#dg").datagrid({
        toolbar: "#toolbar",
        singleSelect: true,
        fit: true,
        border: false,
        pagination: true,
        rownumbers: true,
        collapsible: false,
        idField: 'ColorID',
        url: searchurl,
        method: 'get',
        striped: true,
        pageSize: 50, //每页显示的记录条数，默认为10           
        pageList: [10, 50, 100, 200, 500], //可以设置每页记录条数的列表           
        beforePageText: '第', //页数文本框前显示的汉字           
        afterPageText: '页    共 {pages} 页',
        displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
        columns: [[
            { field: 'chk', checkbox: true, hidden: true },
            { field: 'ColorID', title: '颜色编号', width: 150 },
            { field: 'ColorName', title: '颜色名', width: 150 },
            {
                field: 'ColorValue', title: '颜色值', width: 150,
                formatter: function (value, row) {
                    var colValue = '<div style="display:inline;float:left;margin:3px;background-color:' +
                        row.ColorValue + ';width:12px;height:12px;"' + '></div>' + row.ColorValue;

                    return colValue;
                }
            }
        ]],
        onBeforeLoad: function (param) {
            if (param.page == 0) return false;
        },
        loadFilter: function (data) {
            if (data.success) {
                var list = eval('(' + data.ResultDataValue + ')'),
                    result = {};
                result.total = list.total || 0;
                result.rows = list.rows || [];
                listColors = (!listColors) ? list.rows || [] : listColors;
                return result;
            }
        },
        onClickRow: function (index, row) {
            var colorvalue = row.ColorName;
            $('#dgitem').datagrid({
                url: searchitemurl,
                queryParams: {
                    colorvalue: colorvalue
                },
                loadFilter: function (data) {
                    if (data.success) {
                        var result = eval("(" + data.ResultDataValue + ")");
                        return { total: result.total || 0, rows: result.rows || [] };
                    }
                    else {
                        $.messager.alert('提示', '颜色查询失败！错误原因:' + data.ErrorInfo, 'error');
                        return { total: 0, rows: [] };
                    }
                },
                onBeforeLoad: function () {

                },
                onLoadSuccess: function (data) {

                }
            });
        }
    });

    $("#dgitem").datagrid({
        toolbar: "#toolbaritem",
        singleSelect: false,
        fit: true,
        border: false,
        pagination: true,
        rownumbers: true,
        collapsible: false,
        idField: 'ItemNo',
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
            { field: 'ItemNo', title: '编号', width: '50%' },
            { field: 'CName', title: '名称', width: '50%' }
        ]],
        loadFilter: function (data) {
            if (data.success) {
                var result = eval("(" + data.ResultDataValue + ")");
                return { total: result.total || 0, rows: result.rows || [] };
            }
            else {
                $.messager.alert('提示', '项目查询失败！错误原因:' + data.ErrorInfo, 'error');
                return { total: 0, rows: [] };
            }
        }
    });

    $("#dgallitem").datagrid({
        toolbar: "#toolbarallitem",
        singleSelect: false,
        fit: true,
        border: false,
        pagination: true,
        rownumbers: true,
        collapsible: false,
        idField: 'ItemNo',
        //url: searchallitemurl,
        method: 'get',
        striped: true,
        pageSize: 100, //每页显示的记录条数，默认为10           
        pageList: [100, 200, 500], //可以设置每页记录条数的列表           
        beforePageText: '第', //页数文本框前显示的汉字           
        afterPageText: '页    共 {pages} 页',
        displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
        columns: [[
            { field: 'cb', checkbox: 'true' },
            { field: 'ItemNo', title: '编号', width: '30%' },
            { field: 'CName', title: '名称', width: '30%' },
            { field: 'Color', title: '颜色', width: '30%' }

        ]],
        loadFilter: function (data) {
            if (data.success) {
                var result = eval("(" + data.ResultDataValue + ")");
                return { total: result.total || 0, rows: result.rows || [] };
            }
            else {
                $.messager.alert('提示', '项目查询失败！错误原因:' + data.ErrorInfo, 'error');
                return { total: 0, rows: [] };
            }
        },
        onDblClickRow: function (index, row) {
            addselectitem(row);
        }
    });

    $("#dgselectitem").datagrid({
        toolbar: "#toolbarselectitem",
        singleSelect: false,
        fit: true,
        border: false,
        pagination: false,
        rownumbers: true,
        collapsible: false,
        idField: 'ItemNo',
        //url: searchallitemurl,
        //method: 'get',
        striped: true,
        //pageSize: 100, //每页显示的记录条数，默认为10           
        //pageList: [100, 200, 500], //可以设置每页记录条数的列表           
        //beforePageText: '第', //页数文本框前显示的汉字           
        //afterPageText: '页    共 {pages} 页',
        //displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
        columns: [[
            { field: 'cb', checkbox: 'true' },
            { field: 'ItemNo', title: '编号', width: '50%' },
            { field: 'CName', title: '名称', width: '50%' }

        ]],
        loadFilter: function (data) {
            var result = data;
            return { total: data.length || 0, rows: data || [] };
        },
        onClickRow: function (rowIndex, rowData) {

        }
    });

    $('#dlgitem').window({
        onClose: function () {
            $("#dgallitem").datagrid('clearChecked');
            $("#dgallitem").datagrid('clearSelections');
            $("#dgallitem").datagrid('reload');
            
            $("#dgselectitem").datagrid('clearChecked');
            $("#dgselectitem").datagrid('clearSelections');
            tmpitemdg = [];
            $("#dgselectitem").datagrid({ data: tmpitemdg });
        }
    })
})
function refresh() {
    $('#dg').datagrid('load');
    $('#fm').form('clear')
    $('#txtSearchKey').searchbox("clear");
}
function doSearch() {
    var SH;
    var SearchKey = $("#txtSearchKey").val();

    if (!SearchKey) {
        $('#dg').datagrid('loadData', {
            success: true,
            ResultDataValue: Shell.util.JSON.encode({ total: listColors.length, rows: listColors })
        });
    } else {
        var filterRows = [],
            length = listColors.length;

        SearchKey = SearchKey.trim();
        for (var i = 0; i < length; i++) {
            if ((listColors[i]['ColorID']).toString().indexOf(SearchKey) > -1 || (listColors[i]['ColorName'] && (listColors[i]['ColorName']).toString().indexOf(SearchKey) > -1)) {
                filterRows.push(listColors[i]);
            }
        }
        $('#dg').datagrid('loadData', {
            success: true,
            ResultDataValue: Shell.util.JSON.encode({ total: filterRows.length, rows: filterRows })
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
function refreshitem() {
    $('#dgitem').datagrid('load');
}
function additem() {
    var rows = $('#dg').datagrid('getSelections');
    if (rows.length == 0) {
        $.messager.alert("提示", "请选择具体的颜色!")
        return;
    }
    else {
        $('#dlgitem').window({
            title: "添加项目",
            modal: true
        }).window('open');
        allitem("all");
    }
}

// function delc() {
//     var rows = $('#dgitem').datagrid('getSelections');
//     if (rows.length == 0) {
//         $.messager.alert("提示", "请选择要删除的数据!")
//         return;
//     }
//     else {
//         for (var i = 0; i < rows.length; i++) {
//             $.ajax({
//                 type: 'get',
//                 contentType: 'application/json',
//                 url: delcurl + '?id=' + rows[i].NewsAreaClientLinkId,
//                 dataType: 'json',
//                 async: false,
//                 success: function (data) {
//                     if (data.success == true) {
//                         //$.messager.alert('提示', '删除数据成功！');
//                         //$('#dg').datagrid('reload');

//                     } else {
//                         $.messager.alert('提示', '禁用数据失败！失败信息：' + data.ErrorInfo);
//                     }
//                 }
//             });
//         }
//         $('#dgitem').datagrid('clearSelections');
//         $('#dgitem').datagrid('reload');
//     }
// }

function savec() {

    $("#save").one('click', function (event) {
        event.preventDefault();
        $(this).prop('disabled', true);
    });

    var rows = $('#dgallitem').datagrid('getSelections');
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
        for (var i = 0; i < rows.length; i++) {
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
                        $('#dgitem').datagrid('reload');
                        $('#dgitem').datagrid('unselectAll');
                    } else {
                        $.messager.alert('提示', '插入数据失败！失败信息：' + data.ErrorInfo);
                    }
                }
            });
        }
        $('#dgallitem').datagrid('reload');
        $('#dgallitem').datagrid('unselectAll');
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
        if (SearchKey != "") {
            where = " CName like '%" + SearchKey + "%' or ClientNo like '%" + SearchKey + "%' or ShortCode like '%" + SearchKey + "%'"
        }
        $('#dguncSearchKey').searchbox('enable');
        $('#dgunc').datagrid({
            url: searchcUnselectlisturl,
            queryParams: {
                NewsAreaId: rows[0].NewsAreaId,
                where: where
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

function allitem(flag) {
    selecttype = flag;
    var key = $("#dgitemSearchKey").val();
    var where = "1=1";
    if (key) {
        if (!isNaN(key)) {
            where = " ItemNo =" + key + " or CName like '%" + key + "%' "
        }
        else {
            where = "  CName like '%" + key + "%' "
        }
    }
    $('#dgallitem').datagrid('clearSelections');
    if (flag == "all") {
        $('#dgallitem').datagrid({
            url: searchallitemurl,
            queryParams: {
                colorvalue: "all",
                where: where
            }
        });
    }

    if (flag == "un") {
        $('#dgallitem').datagrid({
            url: searchallitemurl,
            queryParams: {
                where: where
            }
        });
    }

    if (flag == "ed") {
        $('#dgallitem').datagrid({
            url: searchallitemurl,
            queryParams: {
                colorvalue: "allset",
                where: where
            }
        });
    }
}
function delitem() {
    var rows = $("#dgitem").datagrid('getSelections');
    if (rows.length == 0) {
        $.messager.alert("提示", "请选择要删除的项目！");
        return;
    }
    else {
        $.messager.confirm('删除项目', '确定要从当前颜色中删除所选项目？', function (r) {
            if (r) {
                var ItemNoList = [];
                for (var i = 0; i < rows.length; i++) {
                    ItemNoList.push(rows[i].ItemNo);
                }
                $.ajax({
                    type: 'post',
                    contentType: 'application/json',
                    data: Shell.util.JSON.encode({ itemnolist: ItemNoList }),
                    url: setitemcolorurl,
                    dataType: 'json',
                    async: false,
                    success: function (data) {
                        if (data.success == true) {
                            $.messager.alert('提示', '删除数据成功！');
                            $('#dgitem').datagrid('reload');
                        } else {
                            $.messager.alert('提示', '删除数据失败！失败信息：' + data.ErrorInfo);
                        }
                    }
                });
            }
        });
    }
}
function dgitemSearch() {
    allitem(selecttype);
}
function addselectitem(row) {
    if (tmpitemdg && tmpitemdg.length > 0) {
        for (var i = 0; i < tmpitemdg.length; i++) {
            if (tmpitemdg[i].ItemNo == row.ItemNo) {
                return;
            }
        }
    }
    tmpitemdg.push(
        {
            ItemNo: row.ItemNo,
            CName: row.CName
        }
    );
    $("#dgselectitem").datagrid({ data: tmpitemdg });

}
function clearselectitem() {
    tmpitemdg = [];
    $("#dgselectitem").datagrid({ data: tmpitemdg });
}
function addselectitemlist() {
    var rows = $('#dgallitem').datagrid('getSelections');
    if (rows.length == 0) {
        $.messager.alert("提示", "请选择要添加的项目!")
        return;
    }
    else {
        for (var i = 0; i < rows.length; i++) {
            addselectitem(rows[i]);
        }
    }
}
function delselectitem() {
    var rows = $("#dgselectitem").datagrid('getSelections');
    if (rows && rows.length == 0) {
        $.messager.alert("提示", "请选择要删除的项目！");
        return;
    }
    else {
        var selectrows = $("#dgselectitem").datagrid('getRows');
        for (var i = selectrows.length - 1; i >= 0; i--) {
            for (var j = 0; j < rows.length; j++) {
                if (selectrows[i].ItemNo == rows[j].ItemNo) {
                    $("#dgselectitem").datagrid('deleteRow', i);
                    break;
                }
            }
        }
    }
}
function saveselectitem() {
    var rows = $("#dgselectitem").datagrid('getRows');
    var rowscolor = $('#dg').datagrid('getSelections');
    if (!rowscolor || rowscolor.length <= 0) {
        $.messager.alert("提示", "请选择具体的颜色!")
        return;
    }
    if (rows && rows.length > 0) {
        var ItemNoList = [];
        for (var i = 0; i < rows.length; i++) {
            ItemNoList.push(rows[i].ItemNo);
        }
        $.ajax({
            type: 'post',
            contentType: 'application/json',
            data: Shell.util.JSON.encode({ itemnolist: ItemNoList, colorvalue: rowscolor[0].ColorName }),
            url: setitemcolorurl,
            dataType: 'json',
            async: false,
            success: function (data) {
                if (data.success == true) {
                    $.messager.alert('提示', '项目设置颜色成功！');
                    $('#dlgitem').window('close');
                    $('#dgitem').datagrid('reload');
                } else {
                    $.messager.alert('提示', '项目设置颜色失败！失败信息：' + data.ErrorInfo);
                }
            }
        });
    }
}