var btnType;
var errors = 0;
var delCount = 0;
var searchurl = Shell.util.Path.rootPath + '/ServiceWCF/NewsService.svc/ST_UDTO_SearchNNewsByApproval';
var approvalurl = Shell.util.Path.rootPath + '/ServiceWCF/NewsService.svc/ST_UDTO_NNewsApproval';
$(function () {
    $("#dg").datagrid({
        toolbar: "#toolbar",
        singleSelect: false,
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
function update() {
    $('#dg').datagrid('load');
    $('#txtSearchKey').searchbox("clear");
}

function ContentReLoad() {
    //$('#btnsearch').click();
    $('#dg').datagrid('reload');
}
function Approval() {
    var rows = $('#dg').datagrid('getSelections');
    if (rows.length == 0) {
        $.messager.alert("提示", "请勾选需要审批的数据!")
        return;
    } else {
        $.messager.confirm('提示', '确定要审批通过?', function (r) {
            if (r) {
                var id = [];

                for (var i = 0; i < rows.length; i++) {
                    id.push(rows[i].FileID);
                }
                //for (var i = 0; i < rows.length; i++) {
                $.ajax({
                    type: 'get',
                    contentType: 'application/json',
                    url: approvalurl + '?idlist=' + id.join(',') + '&ApprovalFlag=true',
                    dataType: 'json',
                    async: false,
                    success: function (data) {
                        if (data.success == true) {
                            //$.messager.alert('提示', '删除数据成功！');
                            //$('#dg').datagrid('reload');
                            delCount++;
                        } else {
                            $.messager.alert('提示', '审批数据失败！失败信息：' + data.msg);
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
function UnApproval() {
    var rows = $('#dg').datagrid('getSelections');
    if (rows.length == 0) {
        $.messager.alert("提示", "请勾选需要审批退回的数据!")
        return;
    } else {
        $('#txtMemo').textbox('clear');
        $('#dlg').dialog({
            title: "审批退回",
            modal: true,
            toolbar: $('#dlgButtons')
        }).dialog('open');
    }
}
function UnApprovalAction() {
    var rows = $('#dg').datagrid('getSelections');
    if (rows.length == 0) {
        $.messager.alert("提示", "请勾选需要审批退回的数据!")
        return;
    } else {
        $.messager.confirm('提示', '确定要审批退回通过?', function (r) {
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
                    url: approvalurl + '?idlist=' + id.join(',') + '&Memo=' + memo + '&ApprovalFlag=false',
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
                })

                //}

            }
        });
    }
}