var btnType;
var errors = 0;
var delCount = 0;
var searchurl = Shell.util.Path.rootPath + '/ServiceWCF/NewsService.svc/ST_UDTO_SearchNNewsByDrafter';
var delurl = Shell.util.Path.rootPath + '/ServiceWCF/NewsService.svc/ST_UDTO_DelNNews';
var enableurl = Shell.util.Path.rootPath + '/ServiceWCF/NewsService.svc/ST_UDTO_EnableNNews';
$(function () {
    $("#dg").datagrid({
        toolbar: "#toolbar",
        singleSelect: true,
        fit: true,
        border: false,
        pagination: true,
        rownumbers: true,
        collapsible: false,
        idField: 'FileID',
        url: searchurl,
        method: 'get',
        striped: true,
        columns: [[
             { field: 'FileID', title: 'ID', hidden: true },
             { field: 'Title', title: '标题', width: '30%' },
             { field: 'No', title: '编号', width: '5%' },
            { field: 'ContentType', title: 'TypeID', hidden: true },
             { field: 'ContentTypeName', title: '类型', width: '5%' },
             { field: 'Status', title: 'Status', hidden: true },
             {
                 field: 'StatusName', title: '状态', width: '3%',
                 styler: function (value, row, index) {
                     if (row.Status == '5') {
                         return 'background-color:red;font-weight:bold;color:#ffffff';
                     }
                     if (row.Status == '4') {
                         return 'background-color:mediumaquamarine;font-weight:bold;color:#ffffff';
                     }
                     if (row.Status == '3') {
                         return 'background-color:coral;font-weight:bold;color:#ffffff';
                     }
                     if (row.Status == '2') {
                         return 'background-color:darkseagreen;font-weight:bold;color:#ffffff';
                     }

                     return "";
                 }
             },
             //{ field: 'DispOrder', title: '序号', width: '5%' },
             { field: 'DrafterId', title: '起草人ID', hidden: true },
             { field: 'DrafterCName', title: '起草人', width: '3%' },
             {
                 field: 'DrafterDateTime', title: '起草时间', width: '9%',
                 formatter: function (value, row, index) {
                     if (row.DrafterDateTime) {
                         return String(row.DrafterDateTime).replace("T", " ");
                     }
                     else {
                         return "";
                     }
                 }
             },
              { field: 'ApprovalId', title: '审批人ID', hidden: true },
             { field: 'ApprovalCName', title: '审批人', width: '3%' },
             {
                 field: 'ApprovalDateTime', title: '审批时间', width: '9%',
                 formatter: function (value, row, index) {
                     if (row.ApprovalDateTime) {
                         return String(row.ApprovalDateTime).replace("T", " ");
                     }
                     else {
                         return "";
                     }
                 }
             },
             { field: 'Memo', title: '审批意见', width: '5%' },
              { field: 'PublishId', title: '发布人ID', hidden: true },
             { field: 'PublisherName', title: '发布人', width: '3%' },
              { field: 'ReviseReason', title: '发布人意见', width: '5%' },
             {
                 field: 'PublisherDateTime', title: '发布时间', width: '9%',
                 formatter: function (value, row, index) {
                     if (row.PublisherDateTime) {
                         return String(row.PublisherDateTime).replace("T", " ");
                     }
                     else {
                         return "";
                     }
                 }
             },
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
                         return 'background-color:red;font-weight:bold;color:#ffffff';
                     }
                     //if (value=='1') {
                     //    return 'background-color:#FFF2CC;';
                     //}
                     return "";
                 }
             },
             {
                 field: 'Operation', title: '操作',
                 formatter: function (value, row, index) {
                     var a = '<a href="javascript: parent.OpenWindowFuc(\'新增内容\',' + Math.floor(window.screen.width * 0.9) + ',' + Math.floor(window.screen.height * 0.7) + ',\'../ui/News/NewsInfo.html?NewsID=' + row.FileID + '\',\'\')" class="ope-save" >查看</a> ';
                     if (row.Status == 1 || row.Status == 3) {
                         a += '&nbsp;&nbsp;<a href="javascript: parent.OpenWindowFucCallback(\'修改新闻\',' + Math.floor(window.screen.width * 0.9) + ',' + Math.floor(window.screen.height * 0.7) + ',\'../ui/News/EditNews.html?NewsID=' + row.FileID + '\',\'\',dgreload)" class="ope-save" >修改</a> ';
                     }
                     return a;
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
              //,{
              //    field: 'Operation', title: '操作', width: '8%', formatter: function (value, row, index) {
              //        var edit = "<a href='#' data-options='iconCls:icon-edit' onclick='edit(" + index + "," + value + ")'>修改</a>";
              //        return edit;
              //    }
              //}

        ]],
        loadFilter: function (data) {
            var result = eval("(" + data.ResultDataValue + ")");
            return { total: result.total || 0, rows: result.rows || [] };
        },
        onClickRow: function (rowIndex, rowData) {
            //edit(rowIndex);
        },
        onDblClickRow: function (rowIndex, rowData) {
            parent.OpenWindowFuc('新增内容', Math.floor(window.screen.width * 0.9), Math.floor(window.screen.height * 0.7), '../ui/News/NewsInfo.html?NewsID=' + rowData.FileID, "");
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
});
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
                where: " Title like '%" + SearchKey + "%' or DrafterCName like '%" + SearchKey + "%' "
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
function add() {
    var SN = Shell.util.Path.getRequestParams()["SN"];
    parent.OpenWindowFuc('新增新闻', Math.floor(window.screen.width * 0.9), Math.floor(window.screen.height * 0.7), '../ui/News/AddNews.html', SN);
}
function update() {
    $('#dg').datagrid('load');
    $('#txtSearchKey').searchbox("clear");
}

function ContentReLoad() {
    //$('#btnsearch').click();
    $('#dg').datagrid('reload');
}

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
                        url: delurl + '?id=' + rows[i].FileID,
                        dataType: 'json',
                        async: false,
                        success: function (data) {
                            if (data.success == true) {
                                //$.messager.alert('提示', '删除数据成功！');
                                //$('#dg').datagrid('reload');
                                delCount++;
                            } else {
                                $.messager.alert('提示', '禁用数据失败！失败信息：' + data.msg);
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
function enable() {
    var rows = $('#dg').datagrid('getSelections');
    if (rows.length == 0) {
        $.messager.alert("提示", "请勾选需要启用的数据!")
        return;
    } else {
        $.messager.confirm('提示', '确定要启用?', function (r) {
            if (r) {
                for (var i = 0; i < rows.length; i++) {
                    $.ajax({
                        type: 'get',
                        contentType: 'application/json',
                        url: enableurl + '?id=' + rows[i].FileID,
                        dataType: 'json',
                        async: false,
                        success: function (data) {
                            if (data.success == true) {
                                //$.messager.alert('提示', '删除数据成功！');
                                //$('#dg').datagrid('reload');
                                delCount++;
                            } else {
                                $.messager.alert('提示', '启用数据失败！失败信息：' + data.msg);
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

function dgreload() {
    $('#dg').datagrid('reload');
}
/*
function edit(index, value) {
    btnType = 'edit';
    var rowData = $("#dg").datagrid('getRows')[index];
    $('#fm').form('load', rowData);


}
function update() {
    $('#dg').datagrid('load');
    $('#fm').form('clear')
    $('#txtSearchKey').searchbox("clear");
}

function save() {
    var entity = {};
    if (btnType == "edit") {
        $("#save").one('click', function (event) {
            event.preventDefault();

            $(this).prop('disabled', true);
        });

        var TypeID = $("#txtId").val();

        if (TypeID) {
            entity.TypeID = TypeID;
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
*/