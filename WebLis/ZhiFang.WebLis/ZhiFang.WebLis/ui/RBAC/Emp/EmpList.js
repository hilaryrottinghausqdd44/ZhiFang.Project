/**获取部门树列表服务地址**/
var GetDeptTreeListUrl = Shell.util.Path.rootPath + "/ServiceWCF/RBACService.svc/RBAC_GetDeptTree",
    /**获取部门人员列表服务地址**/
    GetDeptEmpListUrl = Shell.util.Path.rootPath + "/ServiceWCF/RBACService.svc/RBAC_GetEmpList",
    /**新增人员服务地址**/
    AddEmpUrl = Shell.util.Path.rootPath + "/ServiceWCF/RBACService.svc/RBAC_AddEmpAndAccount",
    /**删除人员服务地址**/
    DelEmpUrl = Shell.util.Path.rootPath + "/ServiceWCF/RBACService.svc/RBAC_DelEmp",
    /**获取角色列表服务地址**/
    GetRolesUrl = Shell.util.Path.rootPath + "/ServiceWCF/RBACService.svc/GetRoles",
    /**获取人员角色列表服务地址**/
    GetRolesByEmpIdUrl = Shell.util.Path.rootPath + "/ServiceWCF/RBACService.svc/GetRolesByEmpId",
    /**设置人员角色服务地址**/
    SetEmpRolesByEmpIdUrl = Shell.util.Path.rootPath + "/ServiceWCF/RBACService.svc/SetEmpRoles",
    /**设置人员密码服务地址**/
    SetEmpPwdByEmpIdUrl = Shell.util.Path.rootPath + "/ServiceWCF/RBACService.svc/SetEmpPWD",
    /**获取人员账户服务地址**/
    GetUserByEmpIdUrl = Shell.util.Path.rootPath + "/ServiceWCF/RBACService.svc/GetUserByEmpId",
    /**获取人员信息地址**/
    GetUserInfoByEmpIdUrl = Shell.util.Path.rootPath + "/ServiceWCF/RBACService.svc/GetUserInfoByEmpId",
    /**获取人员信息地址**/
    SetUserInfoByEmpIdUrl = Shell.util.Path.rootPath + "/ServiceWCF/RBACService.svc/SetUserInfoByEmpId",
    btnType = "add",
    deptid = 0;
tmpempid = 0;
$(function () {
    $("#Tree").tree({
        lines: true,
        url: GetDeptTreeListUrl + "?guid=" + generateMixed(10),
        method: 'get',
        animate: true,
        onClick: function (node) {
            if (node.Target) {
                deptid = node.tid;
                $("#PDeptName").textbox('setText', node.text);
                $("#PDeptId").textbox('setText', node.tid);
                doSearch();
            }
        },
        loadFilter: function (data) {
            if (data.success) {
                var result = eval("(" + data.ResultDataValue + ")");
                return result;
            } else {
                $.messager.alert("读取部门树错误！");
            }
        }
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
        url: GetDeptEmpListUrl,
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
            { field: 'SN', title: '编码', width: '10%' },
            {
                field: 'CName', title: '中文名称', width: '30%'
                // ,formatter: function (value, row, index) {
                //     var a = row.CNameL + '-' + row.NameF;
                //     return a;
                // }
            },
            { field: 'Sex', title: '性别', width: '5%' },
            { field: 'DeptID', title: '部门ID', width: '20%', hidden: true },
            { field: 'DeptCName', title: '部门名称', width: '20%' },
            {
                field: 'SetRole', title: '设置角色', width: '5%',
                formatter: function (value, row, index) {
                    var a = '<a href="javascript:SetRole(\'' + row.ID + '\')" class="ope-save" >设置</a> ';
                    return a;
                }
            },
            {
                field: 'ReSetPWD', title: '重置密码', width: '5%',
                formatter: function (value, row, index) {
                    var a = '<a href="javascript:ReSetPWD(\'' + row.ID + '\')" class="ope-save" >重置</a> ';
                    return a;
                }
            },
            {
                field: 'DelEmp', title: '删除', width: '5%',
                formatter: function (value, row, index) {
                    var a = '<a href="javascript:DelEmp(\'' + row.ID + '\')" class="ope-save" >删除</a> ';
                    return a;
                }
            }
        ]],
        loadFilter: function (data) {
            if (data.success) {
                var result = eval("(" + data.ResultDataValue + ")");
                return { total: result.total || 0, rows: result.rows || [] };
            }
            else {
                return { total: 0, rows: [] };
            }
        }
    });
    $("#dgrole").datagrid({
        toolbar: "#toolbarrole",
        singleSelect: false,
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
            { field: 'cb', checkbox: 'true' },
            { field: 'ID', title: 'ID', hidden: true },
            { field: 'SN', title: '编码', width: '30%' },
            { field: 'CName', title: '中文名称', width: '30%' },
            { field: 'EName', title: '英文名称', width: '10%' }
        ]],
        loadFilter: function (data) {
            if (data.success) {
                var result = eval("(" + data.ResultDataValue + ")");
                return { total: result.length || 0, rows: result || [] };
            }
            else {
                return { total: 0, rows: [] };
            }
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
function doSearch() {
    $('#dg').datagrid('clearSelections');
    var where = "1=1";
    if ($("#txtSearchKey").val()) {
        where += " and(  CName like '%" + $("#txtSearchKey").val() + "%' or HR_Employees.SN like '" + $("#txtSearchKey").val() + "%' )";
    }
    if (deptid != 0) {
        where += " and DeptId =" + deptid + " ";
    }
    $('#dg').datagrid({
        url: GetDeptEmpListUrl,
        queryParams: {
            wherestr: where
        },
        loadFilter: function (data) {
            if (data.success) {
                var result = eval("(" + data.ResultDataValue + ")");
                return { total: result.total || 0, rows: result.rows || [] };
            }
            else {
                return { total: 0, rows: [] };
            }
        }
    });
}
function add() {
    var node = $("#Tree").tree('getSelected');
    if (!node) {
        $.messager.alert("提示", "请选择部门节点!");
        return;
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

function edit() {
    var rows = $("#dg").datagrid('getSelected');
    if (!rows) {
        $.messager.alert("提示", "请选择人员!");
        return;
    }
    else {
        $('#dlgedit').dialog({
            title: "修改",
            modal: true,
            toolbar: $('#dlgeditButtons')
        }).dialog('open');

        btnType = 'edit';
        loaduserinfo(rows.ID);
    }

}
function loaduserinfo(empid) {
    if (empid) {
        $.ajax({
            type: 'get',
            contentType: 'application/json',
            url: GetUserInfoByEmpIdUrl + "?EmpId=" + empid,
            dataType: 'json',
            success: function (data) {
                if (data.success == true) {
                    $('#fmedit').form('clear');
                    if (data.ResultDataValue) {
                        var result = eval("(" + data.ResultDataValue + ")");
                        (result.EmpInfo.CName) ? $("#txtCName1").textbox('setValue', result.EmpInfo.CName) : $("#txtCName1").textbox('setValue', "");
                        (result.EmpInfo.SN) ? $("#txtSN1").textbox('setValue', result.EmpInfo.SN) : $("#txtSN1").textbox('setValue', "");
                        (result.EmpInfo.Birth) ? $("#brith1").textbox('setValue', result.EmpInfo.Birth.split('T')[0]) : $("#brith1").textbox('setValue', "");
                        if (result.EmpInfo.Sex == "男") {
                            $("#Sex1").combobox('setValue', 1)
                        }
                        if (result.EmpInfo.Sex == "女") {
                            $("#Sex1").combobox('setValue', 2)
                        }
                        if (result.EmpInfo.Sex == "未知") {
                            $("#Sex1").combobox('setValue', 3)
                        }
                        (result.User.Account) ? $("#txtAccount1").textbox('setValue', result.User.Account) : $("#txtAccount1").textbox('setValue', "");
                        (result.EmpInfo.Enabled) ? $("#Enabled").combobox('setValue', result.EmpInfo.Enabled) : $("#Enabled").combobox('setValue', "");
                        (result.EmpInfo.Remarks) ? $("#txtMemo1").textbox('setValue', result.EmpInfo.Remarks) : $("#txtMemo1").textbox('setValue', "");
                    }

                    // $('#dg').datagrid('load');
                    // $('#dg').datagrid('unselectAll');
                    // $('#fmedit').form('clear');
                    // $('#dlgedit').dialog('close');
                } else {
                    $.messager.alert('提示', '获取数据失败！失败信息：' + data.msg);
                }
            }
        });
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

        var CName = $("#txtSN1").val();
        if (CName == "") {
            errors += 1;
        }
        if (CName) {
            entity.CName = CName;
        }

        var SN = $("#txtCName1").val();
        if (SN == "") {
            errors += 1;
        }
        if (SN) {
            entity.SN = SN;
        }

        var brith = $("#brith1").datebox('getText');
        if (!brith || brith == "") {
            errors += 1;
        }
        entity.Birth = Shell.util.Date.toServerDate($("#brith1").datetimebox("getValue"));

        var Account = $("#txtAccount1").val();
        if (!Account || Account == "") {
            $.messager.alert('提示', '帐号不能为空！', 'warning');
            return;
        }
        entity.Account = Account;

        entity.Sex = $("#Sex1").combobox('getText');

        var txtMemo = $("#txtMemo1").val();
        if (txtMemo) {
            entity.Remarks = txtMemo;
        }
        var rows = $("#dg").datagrid('getSelected');
        if (!rows) {
            $.messager.alert("提示", "请选择人员!");
            return;
        }
        else {
            entity.ID = rows.ID;
        }
        if (errors > 0) {
            $.messager.alert('提示', '请输入值的完整性', 'warning');
            errors = 0;
        } else {
            $.ajax({
                type: 'post',
                contentType: 'application/json',
                url: SetUserInfoByEmpIdUrl,
                data: Shell.util.JSON.encode({ entity: entity }),
                dataType: 'json',
                success: function (data) {
                    if (data.success == true) {
                        $('#dg').datagrid('load');
                        $('#dg').datagrid('unselectAll');
                        $('#fmedit').form('clear');
                        $('#dlgedit').dialog('close');
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
        if (!CName || CName == "") {
            errors += 1;
        }
        entity.CName = CName;

        var pdept = $("#PDeptName").val('getValue');
        if (!pdept || pdept == "") {
            errors += 1;
        }
        entity.DeptId = deptid;

        var SN = $("#txtSN").val();
        if (!SN || SN == "") {
            errors += 1;
        }
        entity.SN = SN;

        var brith = $("#brith").datebox('getText');
        if (!brith || brith == "") {
            errors += 1;
        }
        entity.Birth = Shell.util.Date.toServerDate($("#brith").datetimebox("getValue"));

        var Account = $("#txtAccount").val();
        if (!Account || Account == "") {
            $.messager.alert('提示', '帐号不能为空！', 'warning');
            return;
        }
        entity.Account = Account;

        var txtPWD = $("#txtPWD").val();
        if (!txtPWD || txtPWD == "") {
            $.messager.alert('提示', '密码不能为空！', 'warning');
            return;
        }

        var txtDPWD = $("#txtDPWD").val();
        if (txtPWD != txtDPWD) {
            $.messager.alert('提示', '两次输入的密码不一致！', 'warning');
            return;
        }
        entity.PWD = txtPWD;

        entity.Sex = $("#Sex").combobox('getText');
        //entity.Enabled = $("#Enabled").combobox('getValue');
        var txtMemo = $("#txtMemo").val();
        if (txtMemo) {
            entity.Remarks = txtMemo;
        }

        if (errors > 0) {
            $.messager.alert('提示', '请输入值的完整性', 'warning');
            errors = 0;
        } else {
            $.ajax({
                type: 'post',
                contentType: 'application/json',
                url: AddEmpUrl,
                data: Shell.util.JSON.encode({ entity: entity }),
                dataType: "json",
                success: function (data) {
                    if (data.success == true) {
                        // $.messager.alert('提示', '插入数据成功！');
                        $('#dg').datagrid('load');
                        $('#dg').datagrid('unselectAll');
                        $('#fm').form('reset');
                        var node = $("#Tree").tree('getSelected');
                        if (node) {
                            deptid = node.tid;
                            $("#PDeptName").textbox('setText', node.text);
                            $("#PDeptId").textbox('setText', node.tid);
                        }
                        $('#dlg').dialog('close');
                    } else {
                        $.messager.alert('提示', '插入数据失败！失败信息：' + data.ErrorInfo);
                    }
                }
            });
        }
    }

}
function DelEmp(empid) {
    $.messager.confirm('提示', '确定要删除?', function (r) {
        if (r) {
            $.ajax({
                type: 'get',
                contentType: 'application/json',
                url: DelEmpUrl + '?EmpId=' + empid,
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
function SetRole(empid) {
    $('#dgrole').datagrid('clearSelections');
    tmpempid = empid;
    $.ajax({
        type: 'get',
        contentType: 'application/json',
        url: GetRolesByEmpIdUrl + '?EmpId=' + empid,
        dataType: 'json',
        async: false,
        success: function (data) {
            if (data.success == true) {
                var result = eval("(" + data.ResultDataValue + ")");
                if (result) {
                    for (var i = 0; i < result.length; i++) {
                        var rows = $("#dgrole").datagrid('getRows');
                        for (var j = 0; j < rows.length; j++) {
                            if (rows[j].ID == result[i].PostID) {
                                $("#dgrole").datagrid('checkRow', j);
                                break;
                            }
                        }
                    }
                }
                $('#dlgrole').dialog({
                    title: "设置角色",
                    modal: true,
                    toolbar: $('#dlgroleButtons')
                }).dialog('open');
            } else {
                $.messager.alert('提示', '获取角色数据失败！失败信息：' + data.ErrorInfo);
            }
        }
    });
}
function saverole() {
    var rows = $("#dgrole").datagrid('getSelections');
    var Roles = [];
    if (rows && rows.length > 0) {
        for (var j = 0; j < rows.length; j++) {
            Roles.push(rows[j].ID);
        }
    }
    $.ajax({
        type: 'get',
        contentType: 'application/json',
        url: SetEmpRolesByEmpIdUrl + '?EmpId=' + tmpempid + '&Roles=' + Roles.join(','),
        dataType: 'json',
        async: false,
        success: function (data) {
            if (data.success == true) {
                $.messager.alert('提示', '设置角色成功！');
                $('#dlgrole').dialog('close');
            } else {
                $.messager.alert('提示', '设置角色失败！失败信息：' + data.ErrorInfo);
            }
        }
    });
}
function ReSetPWD(empid) {
    tmpempid = empid;
    $('#fmpwd').form('clear');
    $.ajax({
        type: 'get',
        contentType: 'application/json',
        url: GetUserByEmpIdUrl + '?EmpId=' + tmpempid,
        dataType: 'json',
        async: false,
        success: function (data) {
            if (data.success == true) {
                var result = eval("(" + data.ResultDataValue + ")");
                if (result && result.Account) {
                    $("#txtAccountResetPwd").textbox('setText', result.Account);
                }
                $('#dlgpwd').dialog({
                    title: "设置密码",
                    modal: true,
                    toolbar: $('#dlgroleButtons')
                }).dialog('open');
            } else {
                $.messager.alert('提示', '获取当前人员账户失败！失败信息：' + data.ErrorInfo);
            }
        }
    });

}
function savepwd() {
    var txtPWD = $("#txtPWDResetPwd").val();
    if (!txtPWD || txtPWD == "") {
        $.messager.alert('提示', '密码不能为空！', 'warning');
        return;
    }

    var txtDPWD = $("#txtDPWDResetPwd").val();
    if (txtPWD != txtDPWD) {
        $.messager.alert('提示', '两次输入的密码不一致！', 'warning');
        return;
    }
    $.ajax({
        type: 'get',
        contentType: 'application/json',
        url: SetEmpPwdByEmpIdUrl + '?EmpId=' + tmpempid + '&PWD=' + txtPWD,
        dataType: 'json',
        async: false,
        success: function (data) {
            if (data.success == true) {
                $.messager.alert('提示', '设置密码成功！');
                $('#fmpwd').form('clear');
                $('#dlgpwd').dialog('close');
            } else {
                $.messager.alert('提示', '设置密码失败！失败信息：' + data.ErrorInfo);
            }
        }
    });
};