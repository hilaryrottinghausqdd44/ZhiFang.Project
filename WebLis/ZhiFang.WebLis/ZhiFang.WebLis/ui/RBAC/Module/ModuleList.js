/**获取模块列表服务地址**/
var GetModuleTreeListUrl = Shell.util.Path.rootPath + "/ServiceWCF/RBACService.svc/RBAC_GetModuleTreeAll",
    /**获取下属模块列表服务地址**/
    GetSubModuleListUrl = Shell.util.Path.rootPath + "/ServiceWCF/RBACService.svc/RBAC_GetModuleList",
    /**新增模块服务地址**/
    AddModuleUrl = Shell.util.Path.rootPath + "/ServiceWCF/RBACService.svc/RBAC_ADDModule",
    /**删除模块服务地址**/
    DelDeptUrl = Shell.util.Path.rootPath + "/ServiceWCF/RBACService.svc/RBAC_DelModule",
    btnType = "add"
$(function () {
    $("#Tree").tree({
        lines: true,
        url: GetModuleTreeListUrl + "?guid=" + generateMixed(10),
        method: 'get',
        animate: true,
        onClick: function (node) {
            if (node.Target) {
                $("#PId").val(node.id);
                doSearch(node.Target);
            }
        }
        //,loadFilter: function (data) {
        //    if (data.success) {
        //        var result = eval("(" + data.ResultDataValue + ")");
        //        return result;
        //    } else {
        //        $.messager.alert("读取部门树错误！");
        //    }
        //}
    });
    $("#TreeExpandAll").bind('click', function () {
        $("#Tree").tree('expandAll');
    });
    $("#TreeCollapseAll").bind('click', function () {
        $("#Tree").tree('collapseAll');
    });
    $("#TreeReload").bind('click', function () {
        $("#Tree").tree('reload');
    });
    $("#dg").datagrid({
        toolbar: "#toolbar",
        singleSelect: true,
        fit: true,
        border: false,
        pagination: true,
        rownumbers: true,
        collapsible: false,
        idField: 'ID',
        url: GetSubModuleListUrl,
        method: 'get',
        striped: true,
        pageSize: 50, //每页显示的记录条数，默认为10           
        pageList: [10, 50, 100, 200, 500], //可以设置每页记录条数的列表           
        beforePageText: '第', //页数文本框前显示的汉字           
        afterPageText: '页    共 {pages} 页',
        displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
        sortName: "SN",
        sortOrder: "asc",
        columns: [[
            //{ field: 'cb', checkbox: 'true' },
            { field: 'ID', title: 'ID', hidden: true },
            { field: 'SN', title: '序号', width: '20%' },
            { field: 'CName', title: '中文名称', width: '20%' },
            { field: 'ModuleCode', title: '简码', width: '20%' },
            { field: 'URL', title: '地址', width: '20%' }

        ]],
        loadFilter: function (data) {
            var result = eval("(" + data.ResultDataValue + ")");
            return { total: result.total || 0, rows: result.rows || [] };
        }
    });
});
function generateMixed(n) {
    var chars = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'];
    var res = "";
    for (var i = 0; i < n; i++) {
        var id = Math.ceil(Math.random() * 35);
        res += chars[id];
    }
    return res;
}

function doSearch(PSN) {
    $('#dg').datagrid({
        url: GetSubModuleListUrl,
        queryParams: {
            sn: PSN
        },
        loadFilter: function (data) {
            var result = eval("(" + data.ResultDataValue + ")");
            return { total: result.total || 0, rows: result.rows || [] };
        }
    });
}

function add() {
    var node = $("#Tree").tree('getSelected');
    if (!node) {
        $.messager.alert("提示", "请选择父级节点!");
    }
    else {
        $('#dlg').dialog({
            title: "新增",
            modal: true,
            toolbar: $('#dlgButtons')
        }).dialog('open');

        $("#PDept").combobox('select', node.tid);
        btnType = 'add';
    }
}

function save() {
    var entity = {};
    var errors = 0;
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

        var CName = $("#txtCName").val();
        if (CName == "") {
            errors += 1;
        }
        if (CName) {
            entity.CName = CName;
        }
        $("#dg").datagrid();
        var pid = $("#PId").val();
        if (pid == "") {
            errors += 1;
        }
        if (pid) {
            entity.PID = pid;
        }
        var txtCode = $("#txtCode").val();
        if (txtCode) {
            entity.EName = txtCode;
            entity.SName = txtCode;
            entity.ModuleCode = txtCode;
        }

        var Url = $("#txtURL").val();
        if (Url == "") {
            errors += 1;
        }
        if (Url) {
            entity.URL = Url;
        }
        if (errors > 0) {
            $.messager.alert('提示', '请输入值的完整性', 'warning');
            errors = 0;
        } else {
            $.ajax({
                type: 'post',
                contentType: 'application/json',
                url: AddModuleUrl,
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
function del() {
    var rows = $('#dg').datagrid('getSelections');
    if (rows.length == 0) {
        $.messager.alert("提示", "请勾选需要删除的数据!")
        return;
    } else {
        $.messager.confirm('提示', '确定要删除?', function (r) {
            if (r) {
                $.ajax({
                    type: 'get',
                    contentType: 'application/json',
                    url: DelDeptUrl + '?Id=' + rows[0].ID,
                    dataType: 'json',
                    async: false,
                    success: function (data) {
                        if (data.success == true) {
                            $.messager.alert('提示', '删除成功!');
                            $('#dg').datagrid('clearSelections');
                            $('#dg').datagrid('reload');//因为getSelections会记忆选过的记录，所以要清空一下
                        } else {
                            $.messager.alert('提示', '删除数据失败！失败信息：' + data.ErrorInfo);
                        }
                    }
                });
            }
        });
    }
}
