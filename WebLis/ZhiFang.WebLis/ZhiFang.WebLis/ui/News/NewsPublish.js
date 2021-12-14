var btnType;
var errors = 0;
var delCount = 0;
var searchurl = Shell.util.Path.rootPath + '/ServiceWCF/NewsService.svc/ST_UDTO_SearchNNewsByPublish';
var Publishurl = Shell.util.Path.rootPath + '/ServiceWCF/NewsService.svc/ST_UDTO_NNewsPublish';
var searchareaurl = Shell.util.Path.rootPath + '/ServiceWCF/NewsService.svc/ST_UDTO_SearchNNewsAreaLinkByHQL';
var searchcUnselectlisturl = Shell.util.Path.rootPath + '/ServiceWCF/NewsService.svc/ST_UDTO_SearchUnSelectListNNewsAreaLink';
var addareaurl = Shell.util.Path.rootPath + '/ServiceWCF/NewsService.svc/ST_UDTO_AddNNewsAreaLink';
var delareaurl = Shell.util.Path.rootPath + '/ServiceWCF/NewsService.svc/ST_UDTO_DelNNewsAreaLink';
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
             { field: 'cb', checkbox: 'true' },
             { field: 'FileID', title: 'ID', hidden: true },
             { field: 'Title', title: '标题', width: '30%' },
             { field: 'No', title: '编号', width: '5%' },
             { field: 'ContentType', title: 'TypeID', hidden: true },
             { field: 'ContentTypeName', title: '类型', width: '5%' },
             { field: 'Status', title: 'StatusID', hidden: true },
             {
                 field: 'StatusName', title: '状态', width: '5%',
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
             { field: 'ApprovalName', title: '审批人', width: '3%' },
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
             { field: 'PublisherId', title: '发布人ID', hidden: true },
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
                 field: 'Operation', title: '查看',
                 formatter: function (value, row, index) {
                     var a = '<a href="javascript: parent.OpenWindowFuc(\'新增内容\',' + Math.floor(window.screen.width * 0.9) + ',' + Math.floor(window.screen.height * 0.7) + ',\'../ui/News/NewsInfo.html?NewsID=' + row.FileID + '\',\'\')" class="ope-save" >查看</a> ';
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

        ]],
        loadFilter: function (data) {
            var result = eval("(" + data.ResultDataValue + ")");
            return { total: result.total || 0, rows: result.rows || [] };
        },
        onClickRow: function (rowIndex, rowData) {
            var NewsId = rowData.FileID;
            $('#dgarea').datagrid({
                url: searchareaurl,
                queryParams: {
                    where: " NewsId = " + NewsId + ""
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

    $("#dgarea").datagrid({
        toolbar: "#toolbararea",
        singleSelect: false,
        fit: true,
        border: false,
        pagination: true,
        rownumbers: true,
        collapsible: false,
        idField: 'NewsAreaLinkId',
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
              { field: 'NewsAreaLinkId', title: 'ID', hidden: true },
             { field: 'NewsAreaName', title: '区域名称', width: '80%' }

        ]],
        loadFilter: function (data) {
            var result = eval("(" + data.ResultDataValue + ")");
            return { total: result.total || 0, rows: result.rows || [] };
        },
        onClickRow: function (rowIndex, rowData) {

        }
    });

    $("#dgunarea").datagrid({
        toolbar: "#toolbarunarea",
        singleSelect: false,
        fit: true,
        border: false,
        pagination: true,
        rownumbers: true,
        collapsible: false,
        idField: 'NewsAreaId',
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
             { field: 'NewsAreaId', title: 'Id', hidden: true },
             { field: 'CName', title: '中文名称', width: '80%' }

        ]],
        loadFilter: function (data) {
            var result = eval("(" + data.ResultDataValue + ")");
            return { total: result.total || 0, rows: result.rows || [] };
        },
        onClickRow: function (rowIndex, rowData) {

        }
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
function update() {
    $('#dg').datagrid('load');
    $('#txtSearchKey').searchbox("clear");
}

function ContentReLoad() {
    //$('#btnsearch').click();
    $('#dg').datagrid('reload');
}
function Publish() {
    var rows = $('#dg').datagrid('getSelections');
    if (rows.length == 0) {
        $.messager.alert("提示", "请勾选需要发布的数据!")
        return;
    } else {
        $.messager.confirm('提示', '确定要发布?', function (r) {
            if (r) {
                var id = [];

                for (var i = 0; i < rows.length; i++) {
                    id.push(rows[i].FileID);
                }
                //for (var i = 0; i < rows.length; i++) {
                $.ajax({
                    type: 'get',
                    contentType: 'application/json',
                    url: Publishurl + '?idlist=' + id.join(',') + '&PublishFlag=true',
                    dataType: 'json',
                    async: false,
                    success: function (data) {
                        if (data.success == true) {
                            //$.messager.alert('提示', '删除数据成功！');
                            //$('#dg').datagrid('reload');
                            delCount++;
                        } else {
                            $.messager.alert('提示', '发布新闻失败！失败信息：' + data.msg);
                        }
                    }
                })

                //}
                $('#dg').datagrid('clearSelections');
                $('#dg').datagrid('reload');//因为getSelections会记忆选过的记录，所以要清空一下
            }
        });
    }
}
function UnPublish() {
    var rows = $('#dg').datagrid('getSelections');
    if (rows.length == 0) {
        $.messager.alert("提示", "请勾选需要发布撤销的数据!")
        return;
    } else {
        $('#txtMemo').textbox('clear');
        $('#dlg').dialog({
            title: "发布撤销",
            modal: true,
            toolbar: $('#dlgButtons')
        }).dialog('open');
    }
}
function UnPublishAction() {
    var rows = $('#dg').datagrid('getSelections');
    if (rows.length == 0) {
        $.messager.alert("提示", "请勾选需要取消发布的数据!")
        return;
    } else {
        $.messager.confirm('提示', '确定要取消发布?', function (r) {
            if (r) {
                var id = [];

                for (var i = 0; i < rows.length; i++) {
                    id.push(rows[i].FileID);
                }
                var memo = $('#txtMemo').val();
                //for (var i = 0; i < rows.length; i++) {
                $.ajax({
                    type: 'get',
                    contentType: 'application/json',
                    url: Publishurl + '?idlist=' + id.join(',') + '&Memo=' + memo + '&PublishFlag=false',
                    dataType: 'json',
                    async: false,
                    success: function (data) {
                        if (data.success == true) {
                            $('#dlg').dialog('close');                           
                            $('#dg').datagrid('clearSelections');
                            $('#dg').datagrid('reload');//因为getSelections会记忆选过的记录，所以要清空一下
                        } else {
                            $.messager.alert('提示', '审批退回数据失败！失败信息：' + data.msg);
                        }
                    }
                });
                
            }
        });
    }
}

function addarea() {
    var rows = $('#dg').datagrid('getSelections');
    if (rows.length == 0) {
        $.messager.alert("提示", "请选择新闻数据!")
        return;
    }
    else {
        $('#dlgarea').dialog({
            title: "添加区域",
            modal: true
        }).dialog('open');
        $('#dgunarea').datagrid({
            url: searchcUnselectlisturl,
            queryParams: {
                NewsId: rows[0].FileID
            },
            loadFilter: function (data) {
                var result = eval("(" + data.ResultDataValue + ")");
                return { total: result.total || 0, rows: result.rows || [] };
            }
        });
    }
}

function savearea() {

    $("#save").one('click', function (event) {
        event.preventDefault();
        $(this).prop('disabled', true);
    });

    var rows = $('#dgunarea').datagrid('getSelections');
    if (rows.length == 0) {
        $.messager.alert("提示", "请选择要添加的数据!")
        return;
    }
    else {
        var dgrows = $('#dg').datagrid('getSelections');
        if (dgrows.length == 0) {
            $.messager.alert("提示", "请选择新闻数据!")
            return;
        }
        for (var i = 0; i < dgrows.length; i++) {

            for (var j = 0; j < rows.length; j++) {
                var entity = {};
                entity.NewsId = dgrows[i].FileID;
                entity.NewsIdName = dgrows[i].Title;
                entity.NewsAreaId = rows[j].NewsAreaId;
                entity.NewsAreaName = rows[j].CName;

                $.ajax({
                    type: 'post',
                    contentType: 'application/json',
                    url: addareaurl,
                    data: Shell.util.JSON.encode({ entity: entity }),
                    dataType: "json",
                    async: false,
                    success: function (data) {
                        if (data.success == true) {
                            // $.messager.alert('提示', '插入数据成功！');
                            $('#dgarea').datagrid('load');
                            $('#dgarea').datagrid('unselectAll');
                        } else {
                            $.messager.alert('提示', '插入数据失败！失败信息：' + data.ErrorInfo);
                        }
                    }
                });
            }
        }
        $('#dlgarea').dialog('close');
    }
    $('#dgunarea').datagrid('load');
    $('#dgunarea').datagrid('unselectAll');

}

function delarea() {

    $("#save").one('click', function (event) {
        event.preventDefault();
        $(this).prop('disabled', true);
    });

    var rows = $('#dgarea').datagrid('getSelections');
    if (rows.length == 0) {
        $.messager.alert("提示", "请选择要删除的数据!")
        return;
    }
    else {
        for (var i = 0; i < rows.length; i++) {
            $.ajax({
                type: 'get',
                contentType: 'application/json',
                url: delareaurl + '?id=' + rows[i].NewsAreaLinkId,
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
        $('#dgarea').datagrid('clearSelections');
        $('#dgarea').datagrid('reload');
    }
}

function refreshunarea() {
    $('#dgunarea').datagrid('load');
}

function refresharea() {
    $('#dgarea').datagrid('load');
}