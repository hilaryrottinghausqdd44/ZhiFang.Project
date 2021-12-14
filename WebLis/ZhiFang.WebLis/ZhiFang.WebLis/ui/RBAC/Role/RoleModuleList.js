/**获取角色列表服务地址**/
var GetRolesUrl = Shell.util.Path.rootPath + "/ServiceWCF/RBACService.svc/GetRoles",
    /**获取模块树服务地址**/
    GetModuleTreeGirdUrl = Shell.util.Path.rootPath + "/ServiceWCF/RBACService.svc/RBAC_GetModuleTreeGird",
    /**新增角色模块服务地址**/
    SaveRoleModuleUrl = Shell.util.Path.rootPath + "/ServiceWCF/RBACService.svc/RBAC_SaveRoleModule",
    /**角色模块服务地址**/
    GetRoleModuleUrl = Shell.util.Path.rootPath + "/ServiceWCF/RBACService.svc/RBAC_GetRoleModuleByRoleId",
    tmproleid, tmpmoduleid, tmpmodulepid
$(function () {
    $("#Tree").treegrid({
        toolbar: "#toolbar",
        lines: true,
        url: GetModuleTreeGirdUrl + "?guid=" + generateMixed(10),
        method: 'get',
        animate: true,
        idField: 'ID',
        treeField: 'CName',
        singleSelect: false,
        fit: true,               //网格自动撑满
        fitColumns: true,        //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动。
        columns: [[
            { field: 'cb', checkbox: 'true' },
            { title: 'ID', field: 'ID', width: 100, hidden: false },
            { title: 'SN', field: 'SN', width: 100, hidden: true },
            { title: '模块名称', field: 'CName', width: 200 },
            { title: '模块编码', field: 'ModuleCode', width: 150, align: 'right' },
            { title: '模块地址', field: 'URL' }
        ]],
        loadFilter: function (data) {
            if (data.success) {
                var result = eval("(" + data.ResultDataValue + ")");
                return result;
            } else {
                $.messager.alert("读取模块树错误！");
            }
        },
        onSelect: function (rowData) {
            //if (rowData.ID != tmpmodulepid && rowData.ID != tmpmoduleid) {
            //    tmpmoduleid = rowData.ID; 
            //    var rowlist = $("#Tree").treegrid('getChildren', rowData.ID);

            //    for (var i = 0; i < rowlist.length; i++) {
            //        $("#Tree").datagrid('checkRow', rowlist[i].ID);
            //    }
            //    var prow = $("#Tree").treegrid('getParent', rowData.ID);
            //    if (prow) {
            //        $("#Tree").datagrid('checkRow', prow.ID);
            //        tmppid = prow.ID;
            //    }
            //}
        },
        onUnselect: function (rowData) {
            //var rowlist = $("#Tree").treegrid('getChildren', rowData.ID);
            //for (var i = 0; i < rowlist.length; i++) {
            //    $("#Tree").datagrid('uncheckRow', rowlist[i].ID);
            //}
        }
    });
    $("#dgrole").datagrid({
        singleSelect: true,
        fit: true,
        border: false,
        pagination: false,
        rownumbers: true,
        collapsible: false,
        idField: 'ID',
        url: GetRolesUrl,
        method: 'get',
        striped: true,
        sortName: "SN",
        sortOrder: "asc",
        columns: [[
            { field: 'ID', title: 'ID', hidden: true },
            { field: 'SN', title: '编码', width: '50%', hidden: true },
            { field: 'CName', title: '中文名称', width: '50%' }
        ]],
        loadFilter: function (data) {
            if (data.success) {
                var result = eval("(" + data.ResultDataValue + ")");
                return { total: result.length || 0, rows: result || [] };
            }
            else {
                return { total: 0, rows: [] };
            }
        },
        onSelect: function (rowIndex, rowData) {
            reloadtreegrid(rowData.ID);
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

function refresh() {
    var node = $("#dgrole").datagrid('getSelected');
    if (!node) {
        $.messager.alert("提示", "请选择角色!");
        return;
    }
    reloadtreegrid(node.ID);
}
function save() {
    var node = $("#dgrole").datagrid('getSelected');
    if (!node) {
        $.messager.alert("提示", "请选择角色!");
    }
    var module = $("#Tree").treegrid('getSelections');
    var Modulelist = [];
    if (module) {
        for (var i = 0; i < module.length; i++) {
            //Modulelist.push(module[i].ID + "|" + module[i].SN)
            Modulelist.push(module[i].ID);
        }
    }
    $.ajax({
        type: 'post',
        contentType: 'application/json',
        url: SaveRoleModuleUrl,
        dataType: 'json',
        data: Shell.util.JSON.encode({ RoleId: node.ID, Modulelist: Modulelist }),
        async: false,
        success: function (data) {
            if (data.success == true) {
                $.messager.alert("提示", "保存成功!", 'info');
                refresh();
            } else {
                $.messager.alert('提示', '获取角色模块数据失败！失败信息：' + data.ErrorInfo,'warning');
            }
        }
    });
}
function reloadtreegrid(roleid) {
    $('#Tree').datagrid('unselectAll');
    tmproleid = roleid;
    $.ajax({
        type: 'get',
        contentType: 'application/json',
        url: GetRoleModuleUrl + '?RoleId=' + roleid,
        dataType: 'json',
        async: false,
        success: function (data) {
            if (data.success == true) {
                var result = eval("(" + data.ResultDataValue + ")");
                if (result) {
                    for (var i = 0; i < result.length; i++) {

                        $("#Tree").datagrid('selectRow', result[i].ModuleId);
                        //var rows = $("#Tree").treegrid('getRows');
                        //for (var j = 0; j < rows.length; j++) {
                        //    if (rows[j].ID == result[i].ModuleId) {
                        //        $("#Tree").treegrid('checkRow', j);
                        //        break;
                        //    }
                        //}
                    }
                }
            } else {
                $.messager.alert('提示', '获取角色模块数据失败！失败信息：' + data.ErrorInfo);
            }
        }
    });
}

