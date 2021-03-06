var btnType;
var errors = 0;
var delCount = 0;
$(function () {
    $("#dg").datagrid({
        toolbar: "#toolbar",
        singleSelect: false,
        fit:true,
        border: false,
        pagination: true,
        rownumbers: true,
        collapsible:false,
        idField: 'SuperGroupNo',
        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetPubDict?tableName=' + "SuperGroup",
       // url: 'data.txt',
        method: 'get',
        striped: true,
        columns: [[
                { field: 'cb', checkbox: 'true' },
                { field: 'SuperGroupNo', title: '编码', width: '15%' },
                { field: 'CName', title: '中文名称', width: '24%' },
                { field: 'ShortName', title: '简称', width: '24%' },
                { field: 'ShortCode', title: '简码', width: '24%' },
                
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
    $('#txtSuperGroupNo').numberbox({
        validType: 'length[0,10]',
        onChange: function (newValue, oldValue) {
            var SuperGroupNo = $('#txtSuperGroupNo').numberbox('getValue');
            if (btnType == 'add') {
                if (SuperGroupNo.length > 10) {
                    $.ajax({
                        success: function () {
                            $('#txtSuperGroupNo').numberbox('clear');
                           
                   
                    }
                    
                    })
                } else {
                    $.ajax({
                        type: 'get',
                        contentType: 'application/json',
                        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetPubDict',
                        data: { filerValue: newValue.trim(), tablename: "SuperGroup", precisequery: "SuperGroupNo" },
                        //filerValue:newValue.trim(),tablename:"TestItem"
                        dataType: "json",
                        success: function (data) {
                            if (data.success) {
                                var data = eval('(' + data.ResultDataValue + ')'),
                               total = data.total || 0;
                                if (total) {
                                    $('#txtSuperGroupNo').numberbox('clear');
                                    $.messager.alert('提示', '数据库已存在此编号！不能重复插入', 'info');
                                }

                            }

                        }
                    });
                }
            }
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
//复制处理
function show_confirm() {
    var j = 0;
    var r = confirm("已存在！是否需要覆盖");
    var rLABCODENO = $('#txtlabNo').combogrid('getValues');
    if (r == true) {
        for (var i = 0; i < rLABCODENO.length; i++) {
            LABCODENO = rLABCODENO[i];

            $.ajax({
                type: 'get',
                contentType: 'application/json',
                url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/DeleteByLabCode',
                // data: r,
                async: false,
                dataType: 'json',
                data: {
                    DicTable: "SuperGroup",
                    LabCodeNo: LABCODENO
                },
                success: function (data) {

                    $.ajax({
                        type: 'get',
                        contentType: 'application/json',
                        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/CopyAllToLabs',
                        // data: r,
                        async: false,
                        dataType: 'json',
                        data: {
                            DicTable: "SuperGroup",
                            toLab: LABCODENO
                        },
                        success: function (data) {
                            if (data.success == true) {
                                j++;
                                if (j == rLABCODENO.length) {
                                    $.messager.alert('提示', '复制成功！');
                                }
                            }
                        }//success
                    });//ajax

                }//success
            });//ajax
        }
    }//if

}
function show_confirm1() {
    var j = 0;
    
    var rLABCODENO = $('#txtlabNo').combogrid('getValues');
   
        for (var i = 0; i < rLABCODENO.length; i++) {
            LABCODENO = rLABCODENO[i];
            $.ajax({
                type: 'get',
                contentType: 'application/json',
                url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/CopyAllToLabs',
                // data: r,
                async: false,
                dataType: 'json',
                data: {
                    DicTable: "SuperGroup",
                    toLab: LABCODENO
                },
                success: function (data) {
                    if (data.success == true) {
                        j++;
                        if (j == rLABCODENO.length) {
                            $.messager.alert('提示', '复制成功！');
                        }
                    }
                }//success
            });//ajax
        }
   
}
//复制保存按钮的过渡函数
function successData() {
    show_confirm()
}
function successData1() {
    show_confirm1()
}
//复制保存按钮
function labsave() {
    var j = 0;
    var n=0;
    var rLABCODENO = $('#txtlabNo').combogrid('getValues');
    if (rLABCODENO) {
        for (var i = 0; i < rLABCODENO.length; i++) {
            LABCODENO = rLABCODENO[i];
            $.ajax({
                type: 'get',
                contentType: 'application/json',
                url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/ExistLabsData',
                // data: r,
                async: false,
                dataType: 'json',
                data: {
                    DicTable: "SuperGroup",
                    LabCodeNo: LABCODENO
                },
                success: function (data) {
                    if (data.success == true) {
                        j = j + 1;
                        successData(true)
                        
                    } else {
                        n = n + 1;
                        if (n == rLABCODENO.length) {
                            successData1(true)
                        }
                    }
                }//第一个success
            });//第一个ajax  
            if (j > 0) { return; }
        }//for

    }//第一个if

}
//复制按钮
function copy() {

    $("#copy").toggle();
}
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
                        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/DeleteSuperGroupModelByID?SuperGroupNo=' + rows[i].SuperGroupNo,
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
    btnType = 'add';
    $('#txtSuperGroupNo').textbox('enable');
    $('#dlg').dialog('open').dialog('setTitle', '新增');
    $('#fm').form('clear');
    $("#ddlVisible").combobox('select', '是');
   
}
function edit(index, value) {
    btnType = 'edit';
    var rowData = $("#dg").datagrid('getRows')[index];
    $('#fm').form('load', rowData)

    $('#dlg').dialog('open').dialog('setTitle', '修改');
    $('#txtSuperGroupNo').textbox('disable');
  
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
                tableName: 'SuperGroup'
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
    if (btnType == 'edit') {
        $("#save").one('click', function (event) {
            event.preventDefault();

            $(this).prop('disabled', true);
        });
        var r = '';
        r += '{"jsonentity":{';
        var SuperGroupNo = $("#txtSuperGroupNo").val();
        if (SuperGroupNo == "") {
            errors += 1;
        }
        if (SuperGroupNo) {
            r += '"SuperGroupNo":' + SuperGroupNo + ',';
        }
        var ShortName = $("#txtShortName").val();
        if (ShortName == "") {
            errors += 1;
        }
        if (ShortName) {
            r += '"ShortName":"' + ShortName + '",';
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
        var Visible = $("#ddlVisible").combobox('getValue');
        if (Visible == "是") {
            Visible = 1;
        }
        else if (Visible == "否") {
            Visible = 0;
        }
        if (Visible>=0) {
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
                url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/UpdateSuperGroupModelByID',
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
            });
        }
    } else if (btnType == "add") {

        $("#save").one('click', function (event) {
            event.preventDefault();

            $(this).prop('disabled', true);
        });
        var r = '';
        r += '{"jsonentity":{';
        var SuperGroupNo = $("#txtSuperGroupNo").val();
        if (SuperGroupNo == "") {
            errors += 1;
        }
        if (SuperGroupNo) {
            r += '"SuperGroupNo":' + SuperGroupNo + ',';
        }
        var ShortName = $("#txtShortName").val();
        if (ShortName == "") {
            errors += 1;
        }
        if (ShortName) {
            r += '"ShortName":"' + ShortName + '",';
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
        var Visible = $("#ddlVisible").combobox('getValue');
        if (Visible == "是") {
            Visible = 1;
        }
        else if (Visible == "否") {
            Visible = 0;
        }
        if (Visible>=0) {
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
                url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/AddSuperGroupModel',
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
