var btnType;
var errors = 0;
var delCount = 0;
var selectS = 0;
$(function () {
    $("#dg").datagrid({
        loadMsg: '数据加载中...',
        toolbar: "#toolbar",
        fitColumns: true,
        singleSelect: false,
        border: false,
        fit: true,
        pagination: true,
        rownumbers: true,
        collapsible: false,
        idField: 'ItemNo',
        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetPubDict?tableName=' + "TestItem",
        // url:'data.txt',
        method: 'get',
        striped: true,
        pageSize: 50, //每页显示的记录条数，默认为10           
        pageList: [50, 100, 200, 500], //可以设置每页记录条数的列表           
        beforePageText: '第', //页数文本框前显示的汉字           
        afterPageText: '页    共 {pages} 页',
        displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
        columns: [[
            { field: 'cb', checkbox: 'true' },
            { field: 'ItemNo', title: '项目编码', width: setWidth(0.1) },
            { field: 'CName', title: '中文名称', width: setWidth(0.2) },
            { field: 'EName', title: '英文名称', width: setWidth(0.2) },
            { field: 'ShortName', title: '简称', width: setWidth(0.1) },
            { field: 'ShortCode', title: '简码', width: setWidth(0.1) },
            {
                field: 'IsCombiItem', title: '是否组套', width: setWidth(0.05), align: 'center',
                formatter: function (value, row, index) {
                    var txt = "否";
                    if (row.IsCombiItem && row.IsCombiItem == 1) {
                        txt = "是";
                    }
                    return txt;
                }
            },
            {
                field: 'Operation', title: '操作', width: setWidth(0.20),
                align: 'center',
                formatter: function (value, row, index) {
                    var edit = "<a href='#' data-options='iconCls:icon-edit' onclick='edit(" + index + "," + value + ")'>修改</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href='#' data-options='iconCls:icon-edit' onclick='ShowPItemList(" + row.ItemNo + ")'>父组套</a>";
                    if (row.IsCombiItem && row.IsCombiItem == 1) {
                        edit = edit + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href='#' data-options='iconCls:icon-edit' onclick='ShowSubItemList(" + row.ItemNo + ")'>组套内项目</a>";
                    }
                    return edit;
                }
            }
        ]],


        loadFilter: function (data) {
            var result = eval("(" + data.ResultDataValue + ")");
            return { total: result.total || 0, rows: result.rows || [] };
        }
        //onSelect: function (rowIndex, rowData) {
        //    //$('#fm').form('load', rowData)
        //},

    });

    $('#txtItemNo').numberbox({
        validType: 'length[0,10]',
        onChange: function (newValue, oldValue) {
            var ItemNo = $('#txtItemNo').numberbox('getValue');
            if (btnType == 'add') {
                if (ItemNo.length > 10) {
                    $.ajax({
                        success: function () {
                            $('#txtItemNo').numberbox('clear');

                        }
                    })

                } else {
                    $.ajax({
                        type: 'get',
                        contentType: 'application/json',
                        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetPubDict',
                        data: { filerValue: newValue.trim(), tablename: "TestItem", precisequery: "ItemNo" },
                        //filerValue:newValue.trim(),tablename:"TestItem"
                        dataType: "json",
                        success: function (data) {
                            if (data.success) {
                                var data = eval('(' + data.ResultDataValue + ')'),
                                    total = data.total || 0;
                                if (total) {
                                    $('#txtItemNo').numberbox('clear');

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
        panelWidth: 200,
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
    $('#txtColor').combobox({
        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetAllItemColorDict',
        method: 'GET',
        valueField: 'ColorID',
        textField: 'ColorName',
        loadFilter: function (data) {
            var result = eval("(" + data.ResultDataValue + ")"); //eval()把字符串转换成JSON格式
            return result.rows || [];
        }


    });

    $("#copy").hide();
    $("#txtPrice").textbox('textbox').bind('keyup', function (e) {
        $("#txtPrice").textbox('setValue', $(this).val());
    });
    $("#txtLowPrice").textbox('textbox').bind('keyup', function (e) {
        $("#txtLowPrice").textbox('setValue', $(this).val());
    });

    $("#dgItemList").datagrid({
        //toolbar: "#toolbarItemList",
        singleSelect: false,
        fit: true,
        border: false,
        pagination: false,
        rownumbers: true,
        collapsible: false,
        idField: 'ItemNo',
        //url: searchcUnselectlisturl,
        method: 'get',
        striped: true,
        //pageSize: 100, //每页显示的记录条数，默认为10           
        //pageList: [100, 200, 500], //可以设置每页记录条数的列表           
        //beforePageText: '第', //页数文本框前显示的汉字           
        //afterPageText: '页    共 {pages} 页',
        //displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
        columns: [[
            { field: 'ItemNo', title: '编码', hidden: false },
            { field: 'CName', title: '中文名称', width: '80%' }

        ]],
        onClickRow: function (rowIndex, rowData) {

        }
    });
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
                    DicTable: "TestItem",
                    LabCodeNo: LABCODENO
                },
                success: function (data) {
                    if (selectS == 'a') {
                        var SearchKey = $("#txtSearchKey").searchbox("getText");
                        $.ajax({
                            type: 'get',
                            contentType: 'application/json',
                            url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/BatchCopyItemsToLab',
                            // data: r,
                            async: false,
                            dataType: 'json',
                            data: {
                                ItemKey: SearchKey,
                                LabCodeNo: LABCODENO
                            },
                            success: function (data) {
                                if (data.success == true) {
                                    j = j + 1;
                                    if (j == rLABCODENO.length) {
                                        $.messager.alert('提示', '复制成功！');
                                    }
                                }
                            } //success
                        }); //ajax
                    } else {
                        $.ajax({
                            type: 'get',
                            contentType: 'application/json',
                            url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/CopyAllToLabs',
                            // data: r,
                            async: false,
                            dataType: 'json',
                            data: {
                                DicTable: "TestItem",
                                LabCodeNo: LABCODENO
                            },
                            success: function (data) {
                                if (data.success == true) {
                                    j = j + 1;
                                    if (j == rLABCODENO.length) {
                                        $.messager.alert('提示', '复制成功！');
                                    }
                                }
                            } //success
                        }); //ajax
                    }
                } //success
            }); //ajax
        }
    } //if

}
function show_confirm2() {
    var j = 0;

    var rLABCODENO = $('#txtlabNo').combogrid('getValues');

    for (var i = 0; i < rLABCODENO.length; i++) {
        LABCODENO = rLABCODENO[i];
        if (selectS == 'a') {
            var SearchKey = $("#txtSearchKey").searchbox("getText");
            $.ajax({
                type: 'get',
                contentType: 'application/json',
                url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/BatchCopyItemsToLab',
                // data: r,
                async: false,
                dataType: 'json',
                data: {
                    ItemKey: SearchKey,
                    LabCodeNo: LABCODENO
                },
                success: function (data) {
                    if (data.success == true) {
                        j = j + 1;
                        if (j == rLABCODENO.length) {
                            $.messager.alert('提示', '复制成功！');
                        }
                    }
                } //success
            }); //ajax
        } else {
            $.ajax({
                type: 'get',
                contentType: 'application/json',
                url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/CopyAllToLabs',
                // data: r,
                dataType: 'json',
                data: {
                    DicTable: "TestItem",
                    LabCodeNo: LABCODENO
                },
                success: function (data) {
                    if (data.success == true) {
                        j = j + 1;
                        if (j == rLABCODENO.length) {
                            $.messager.alert('提示', '复制成功！');
                        }
                    }
                } //success
            }); //ajax
        }
    }

}
//复制保存按钮的过渡函数
function successData() {

    show_confirm()

}
function successData2() {

    show_confirm2()

}
//复制保存按钮
function labsave() {
    var j = 0;
    var n = 0;
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
                    DicTable: "TestItem",
                    LabCodeNo: LABCODENO
                },
                success: function (data) {
                    if (data.success == true) {
                        successData(true)
                        j = j + 1;

                    } else if (data.success == false) {
                        n = n + 1;
                        if (n == rLABCODENO.length) {
                            successData2(true)
                        }
                    }
                } //第一个success
            }); //第一个ajax  
            if (j > 0) {
                return;
            }
        } //for

    } //第一个if

}

function selectsave() {
    var j = 0;
    var SearchKey = $("#txtSearchKey").searchbox("getValue"),
        labs = $('#txtlabNo').combogrid('getValues') || [];

    if (labs.length == 0) {
        $.messager.alert('提示', '请选择目标实验室', 'info');
        return;
    }
    var r = '';
    var str = $("#dg").datagrid('getSelections');
    if (str.length == 0) {
        $.messager.confirm('确认', '确定把当前中心表所有数据复制到目标实验室吗？', function (btn) {
            if (btn) {
                var opt = $('#dg').datagrid('options');
                opt.loadMsg = '正在执行当前操作...';
                $('#dg').datagrid('loading');
                var rLABCODENO = $('#txtlabNo').combogrid('getValues') || [],
                    length = rLABCODENO.length || 0,
                    listLabs = '';
                if (length) {
                    listLabs = rLABCODENO[0];
                    for (var i = 1; i < length; i++) {
                        listLabs += ',' + rLABCODENO[i];
                    }
                    $.ajax({
                        type: 'get',
                        contentType: 'application/json',
                        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/CopyAllToLabs',
                        // data: r,
                        dataType: 'json',
                        data: {
                            DicTable: "TestItem",
                            toLab: listLabs
                        },
                        success: function (data) {
                            if (data.success == true) {
                                $.messager.alert('提示', '复制成功！');
                            } else {
                                $.messager.alert('提示', '复制失败！');
                            }
                            $('#dg').datagrid('loaded');
                        },
                        error: function (data) {
                            $('#dg').datagrid('loaded');
                            $.messager.alert('提示信息', data.ErrorInfo, 'error');
                        }
                    });
                }
                opt.loadMsg = '数据加载...';
            }
        });
    } else {
        $.messager.confirm('确认', '确定把当前勾选的数据复制到目标实验室吗？', function (btn) {
            if (btn) {
                var opt = $('#dg').datagrid('options');
                opt.loadMsg = '正在执行当前操作...';
                $('#dg').datagrid('loading');
                for (var i = 0; i < str.length; i++) {

                    // r += str[i].ItemNo';
                    if (r == '') {
                        r += "'" + str[i].ItemNo;
                    }
                    else
                        r += "','" + str[i].ItemNo;

                }
                r += "'";
                var rLABCODENO = $('#txtlabNo').combogrid('getValues') || [];

                for (var i = 0; i < rLABCODENO.length; i++) {
                    LABCODENO = rLABCODENO[i];


                    $.ajax({
                        type: 'get',
                        contentType: 'application/json',
                        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/BatchCopyItemsToLab',
                        // data: r,
                        async: false,
                        dataType: 'json',
                        data: {
                            ItemNos: r,
                            LabCodeNo: LABCODENO
                        },
                        success: function (data) {
                            if (data.success == true) {
                                j = j + 1;
                                if (j == rLABCODENO.length) {
                                    $.messager.alert('提示', '复制成功！');
                                }
                            } else {
                                $.messager.alert('提示', '复制失败！');
                            }
                        },
                        error: function (data) {
                            $.messager.alert('提示信息', data.ErrorInfo, 'error');
                        }
                    }); //ajax
                }
                $('#dg').datagrid('loaded');
                $('#dg').datagrid('clearSelections');
                opt.loadMsg = '数据加载...';
            }
        });
    }
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
                        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/DeleteTestItemModelByID?ItemNo=' + rows[i].ItemNo,
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
                    $('#dg').datagrid('reload'); //因为getSelections会记忆选过的记录，所以要清空一下
                }

            }
        });
    }
}
function setWidth(percent) {
    return document.body.clientWidth * percent;
}
function add() {
    btnType = 'add';
    $('#txtItemNo').numberbox('enable');
    $('#dlg').dialog({ modal: true });
    $('#dlg').dialog('open').dialog('setTitle', '新增');

    $('#fm').form('clear');

    $("#ddlSecretgrade").combobox('select', '保密');
    $("#ddlIsCalc").combobox('select', '否');
    $("#ddlPrec").combobox('select', '0');
    $("#ddlVisible").combobox('select', '是');
    $("#ddlCuegrade").combobox('select', '普通');
    $("#ddlIsDoctorItem").combobox('select', '否');
    $("#ddlIschargeItem").combobox('select', '否');
    $("#ddlIsCombiItem").combobox('select', '否');
    $("#IsProfile").combobox('select', '否');
    $("#ddlSuperGroupNo").combobox({
        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetPubDict?tableName=SuperGroup&fields=SuperGroupNo,CName',

        method: 'get',
        valueField: 'SuperGroupNo',
        textField: 'CName',
        loadFilter: function (data) {
            var result = eval("(" + data.ResultDataValue + ")");
            return result.rows;
        }

    });


}
function edit(index, value) {
    $("#ddlSuperGroupNo").combobox({
        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetPubDict?tableName=SuperGroup&fields=SuperGroupNo,CName',

        method: 'get',
        valueField: 'SuperGroupNo',
        textField: 'CName',
        loadFilter: function (data) {
            var result = eval("(" + data.ResultDataValue + ")");
            return result.rows;
        }

    });
    btnType = 'edit';
    var rowData = $("#dg").datagrid('getRows')[index];
    $('#fm').form('load', rowData)

    $('#dlg').dialog('open').dialog('setTitle', '修改');
    $('#txtItemNo').numberbox('disable');
    // $('#txtItemNo').numberbox("readonly", true);
    var IsCombiItem = $('#ddlIsCombiItem').combobox('getValue');
    if (IsCombiItem == '1') {
        IsShowCombiItem(IsCombiItem);
    }

}
function save() {
    if (btnType == 'edit') {

        //        $("#save").one('click', function (event) {
        //            event.preventDefault();

        //            $(this).prop('disabled', true);
        //        });
        var r = '';
        r += '{"jsonentity":{';
        var ItemNo = $("#txtItemNo").val();
        if (ItemNo == "") {

            errors += 1;
        }
        if (ItemNo) {
            r += '"ItemNo":' + ItemNo + ',';
        }
        var Secretgrade = $("#ddlSecretgrade").combobox('getValue');
        if (Secretgrade == "保密") {
            Secretgrade = 1;
        }
        else if (Secretgrade == "不保密") {
            Secretgrade = 0;
        }
        if (Secretgrade == "") {
        } else {
            r += '"Secretgrade":' + Secretgrade + ',';
        }
        var CName = $("#txtCName").val();
        if (CName == "") {

            errors += 1;
        }
        if (CName) {
            r += '"CName":"' + CName + '",';
        }
        var Unit = $("#txtUnit").val();
        if (Unit) {
            r += '"Unit":"' + Unit + '",';
        }
        var EName = $("#txtEName").val();
        if (EName == "") {

            errors += 1;
        }
        if (EName) {
            r += '"EName":"' + EName + '",';
        }
        var IsCalc = $("#ddlIsCalc").combobox('getValue');
        if (IsCalc == "是") {
            IsCalc = 1;
        }
        else if (IsCalc == "否") {
            IsCalc = 0;
        }
        if (IsCalc == "") {
        } else {
            r += '"IsCalc":' + IsCalc + ',';
        }
        var ShortName = $("#txtShortName").val();
        if (ShortName == "") {

            errors += 1;
        }
        if (ShortName) {
            r += '"ShortName":"' + ShortName + '",';
        }
        var Prec = $("#ddlPrec").combobox('getValue');
        if (Prec) {
            r += '"Prec":' + Prec + ',';
        }
        var ShortCode = $("#txtShortCode").val();
        if (ShortCode) {
            r += '"ShortCode":"' + ShortCode + '",';
        }
        var OrderNo = $("#txtOrderNo").val();
        if (OrderNo) {
            r += '"OrderNo":"' + OrderNo + '",';
        }
        var Price = $("#txtPrice").val();
        if (Price) {
            r += '"Price":' + Price + ',';
        }
        var LowPrice = $("#txtLowPrice").val();
        if (LowPrice) {
            r += '"LowPrice":' + LowPrice + ',';
        }
        var Visible = $("#ddlVisible").combobox('getValue');
        if (Visible == "是") {
            Visible = 1;
        }
        else if (Visible == "否") {
            Visible = 0;
        }
        if (Visible == "") {
        } else {
            r += '"Visible":' + Visible + ',';
        }
        var Cuegrade = $("#ddlCuegrade").combobox('getValue');
        if (Cuegrade == "特殊") {
            Cuegrade = 1;
        }
        else if (Cuegrade == "普通") {
            Cuegrade = 0;
        }
        if (Cuegrade == "") {
        } else {
            r += '"Cuegrade":' + Cuegrade + ',';
        }
        var IsDoctorItem = $("#ddlIsDoctorItem").combobox('getValue');
        if (IsDoctorItem == "是") {
            IsDoctorItem = 1;
        }
        else if (IsDoctorItem == "否") {
            IsDoctorItem = 0;
        }
        if (IsDoctorItem == "") {
        } else {
            r += '"IsDoctorItem":' + IsDoctorItem + ',';
        }
        var IschargeItem = $("#ddlIschargeItem").combobox('getValue');
        if (IschargeItem == "是") {
            IschargeItem = 1;
        }
        else if (IschargeItem == "否") {
            IschargeItem = 0;
        }
        if (IschargeItem == "") {
        } else {
            r += '"IschargeItem":' + IschargeItem + ',';
        }
        var DiagMethod = $("#txtDiagMethod").val();
        if (DiagMethod) {
            r += '"DiagMethod":"' + DiagMethod + '",';
        }
        var DispOrder = $("#txtDisOrder").val();
        if (DispOrder) {
            r += '"DispOrder":' + DispOrder + ',';
        }
        var Color = $("#txtColor").combobox('getText');
        if (Color) {
            r += '"Color":"' + Color + '",';
        }
        var ItemDesc = $("#txtItemDesc").val();
        if (ItemDesc) {
            r += '"ItemDesc":"' + ItemDesc + '",';
        }
        var FWorkLoad = $("#txtFWorkLoad").val();
        if (FWorkLoad) {
            r += '"FWorkLoad":"' + FWorkLoad + '",';
        }
        var IsCombiItem = $("#ddlIsCombiItem").combobox('getValue');
        if (IsCombiItem == "是") {
            IsCombiItem = 1;
        }
        else if (IsCombiItem == "否") {
            IsCombiItem = 0;
        }
        if (IsCombiItem == "") {
        } else {
            r += '"IsCombiItem":' + IsCombiItem + ',';
        }

        var IsProfile = $("#IsProfile").combobox('getValue');
        IsProfile = IsProfile == "否" ? 0 : 1;
        r += '"IsProfile":' + IsProfile + ',';

        var SuperGroupNo = $("#ddlSuperGroupNo").combobox('getValue', 'SuperGroupNo');
        if (SuperGroupNo == "") {
        } else {
            r += '"SuperGroupNo":' + SuperGroupNo + ',';
        }

        // var r = '{"jsonentity":{"ItemNo":' + ItemNo + ',"Secretgrade":' + Secretgrade + ',"CName":"' + CName + '","Unit":"' + Unit + '","EName":"' + EName + '","IsCalc":' + IsCalc + ',"ShortName":"' + ShortName + '","Prec":' + Prec + ',"ShortCode":"' + ShortCode + '","OrderNo":"' + OrderNo + '","Visible":' + Visible + ',"Cuegrade":' + Cuegrade + ',"IsDoctorItem":' + IsDoctorItem + ',"IschargeItem":' + IschargeItem + ',"DiagMethod":"' + DiagMethod + '","DispOrder":' + DispOrder + ',"Color":"' + Color + '","ItemDesc":"' + ItemDesc + '","Price":"' + Price + '","FWorkLoad":"' + FWorkLoad + '","SuperGroupNo":' + SuperGroupNo + ',"IsCombiItem":' + IsCombiItem + '}}'
        r += '}}'
        if (errors > 0) {
            $.messager.alert('提示', '请检查输入值的完整性', 'warning');
            errors = 0;
        } else {
            $.ajax({
                type: 'post',
                contentType: 'application/json',
                url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/UpdateTestItemModelByID',
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
    } else if (btnType == 'add') {
        //        $("#save").one('click', function (event) {
        //            event.preventDefault();

        //            $(this).prop('disabled', true);
        //        });
        var r = '';
        r += '{"jsonentity":{';
        var ItemNo = $("#txtItemNo").val();

        if (ItemNo) {
            r += '"ItemNo":' + ItemNo + ',';

        } else {
            errors += 1;
        }
        var Secretgrade = $("#ddlSecretgrade").combobox('getValue');
        if (Secretgrade == "保密") {
            Secretgrade = 1;
        }
        else if (Secretgrade == "不保密") {
            Secretgrade = 0;
        }
        if (Secretgrade == "") {
        } else {
            r += '"Secretgrade":' + Secretgrade + ',';
        }
        var CName = $("#txtCName").val();
        if (CName == "") {

            errors += 1;
        }
        if (CName) {
            r += '"CName":"' + CName + '",';
        }
        var Unit = $("#txtUnit").val();
        if (Unit) {
            r += '"Unit":"' + Unit + '",';
        }
        var EName = $("#txtEName").val();
        if (EName == "") {

            errors += 1;
        }
        if (EName) {
            r += '"EName":"' + EName + '",';
        }
        var IsCalc = $("#ddlIsCalc").combobox('getValue');
        if (IsCalc == "是") {
            IsCalc = 1;
        }
        else if (IsCalc == "否") {
            IsCalc = 0;
        }
        if (IsCalc == "") {
        } else {
            r += '"IsCalc":' + IsCalc + ',';
        }
        var ShortName = $("#txtShortName").val();
        if (ShortName == "") {

            errors += 1;
        }
        if (ShortName) {
            r += '"ShortName":"' + ShortName + '",';
        }
        var Prec = $("#ddlPrec").combobox('getValue');
        if (Prec) {
            r += '"Prec":' + Prec + ',';
        }
        var ShortCode = $("#txtShortCode").val();
        if (ShortCode) {
            r += '"ShortCode":"' + ShortCode + '",';
        }
        var OrderNo = $("#txtOrderNo").val();
        if (OrderNo) {
            r += '"OrderNo":"' + OrderNo + '",';
        }
        var Price = $("#txtPrice").val();
        if (Price) {
            r += '"Price":' + Price + ',';
        }
        var Visible = $("#ddlVisible").combobox('getValue');
        if (Visible == "是") {
            Visible = 1;
        }
        else if (Visible == "否") {
            Visible = 0;
        }
        if (Visible == "") {
        } else {
            r += '"Visible":' + Visible + ',';
        }
        var Cuegrade = $("#ddlCuegrade").combobox('getValue');
        if (Cuegrade == "特殊") {
            Cuegrade = 1;
        }
        else if (Cuegrade == "普通") {
            Cuegrade = 0;
        }
        if (Cuegrade == "") {
        } else {
            r += '"Cuegrade":' + Cuegrade + ',';
        }
        var IsDoctorItem = $("#ddlIsDoctorItem").combobox('getValue');
        if (IsDoctorItem == "是") {
            IsDoctorItem = 1;
        }
        else if (IsDoctorItem == "否") {
            IsDoctorItem = 0;
        }
        if (IsDoctorItem == "") {
        } else {
            r += '"IsDoctorItem":' + IsDoctorItem + ',';
        }
        var IschargeItem = $("#ddlIschargeItem").combobox('getValue');
        if (IschargeItem == "是") {
            IschargeItem = 1;
        }
        else if (IschargeItem == "否") {
            IschargeItem = 0;
        }
        if (IschargeItem == "") {
        } else {
            r += '"IschargeItem":' + IschargeItem + ',';
        }
        var DiagMethod = $("#txtDiagMethod").val();
        if (DiagMethod) {
            r += '"DiagMethod":"' + DiagMethod + '",';
        }
        var DispOrder = $("#txtDisOrder").val();
        if (DispOrder) {
            r += '"DispOrder":' + DispOrder + ',';
        }
        var Color = $("#txtColor").combobox('getText');
        if (Color) {
            r += '"Color":' + Color + ',';
        }
        var ItemDesc = $("#txtItemDesc").val();
        if (ItemDesc) {
            r += '"ItemDesc":"' + ItemDesc + '",';
        }
        var FWorkLoad = $("#txtFWorkLoad").val();
        if (FWorkLoad) {
            r += '"FWorkLoad":"' + FWorkLoad + '",';
        }
        var IsCombiItem = $("#ddlIsCombiItem").combobox('getValue');
        if (IsCombiItem == "是") {
            IsCombiItem = 1;
        }
        else if (IsCombiItem == "否") {
            IsCombiItem = 0;
        }
        if (IsCombiItem == "") {
        } else {
            r += '"IsCombiItem":' + IsCombiItem + ',';
        }
        var SuperGroupNo = $("#ddlSuperGroupNo").combobox('getValue', 'SuperGroupNo');
        if (SuperGroupNo == "") {
        } else {
            r += '"SuperGroupNo":' + SuperGroupNo + ',';
        }

        var IsProfile = $("#IsProfile").combobox('getValue');
        IsProfile = IsProfile == "否" ? 0 : 1;
        r += '"IsProfile":' + IsProfile + ',';

        // var r = '{"jsonentity":{"ItemNo":' + ItemNo + ',"Secretgrade":' + Secretgrade + ',"CName":"' + CName + '","Unit":"' + Unit + '","EName":"' + EName + '","IsCalc":' + IsCalc + ',"ShortName":"' + ShortName + '","Prec":' + Prec + ',"ShortCode":"' + ShortCode + '","OrderNo":"' + OrderNo + '","Visible":' + Visible + ',"Cuegrade":' + Cuegrade + ',"IsDoctorItem":' + IsDoctorItem + ',"IschargeItem":' + IschargeItem + ',"DiagMethod":"' + DiagMethod + '","DispOrder":' + DispOrder + ',"Color":"' + Color + '","ItemDesc":"' + ItemDesc + '","Price":"' + Price + '","FWorkLoad":"' + FWorkLoad + '","SuperGroupNo":' + SuperGroupNo + ',"IsCombiItem":' + IsCombiItem + '}}'
        r += '}}'

        if (errors > 0) {
            $.messager.alert('提示', '请检查输入值的完整性', 'warning');
            errors = 0;
        } else {
            $.ajax({
                type: 'post',
                contentType: 'application/json',
                url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/AddTestItemModel',
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
function update() {
    $('#dg').datagrid('load');
    $('#fm').form('clear')
    $('#txtSearchKey').searchbox("clear");
}
function batchsetcolor() {
    var SN = Shell.util.Path.getRequestParams()["SN"];
    parent.parent.OpenWindowFuc('批量设置项目颜色', Math.floor(window.screen.width * 0.9), Math.floor(window.screen.height * 0.7), Shell.util.Path.rootPath + '/ui/dictionary/CenterDict/ItemDict/grid/ItemColorSet.html', SN);
}
function doSearch(value) {

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
                tableName: 'TestItem'
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
                // $('#txtSearchKey').searchbox("clear");
            }
        });
    }
}
//组套搜索
function dlgSearch() {
    var filerValue = $("#txtDlg").val();
    filerValue = encodeURI(filerValue);

    $('#dgwest').datagrid('options').pageNumber = 1;
    $('#dgwest').datagrid('getPager').pagination({ pageNumber: 1 });
    $('#dgwest').datagrid('options').url = Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetPubDict?filerValue=' + filerValue;
    $('#dgwest').datagrid('reload');

    // var SearchKey = $("#txtDlg").val();


}
function IsShowCombiItem(newValue) {

    if (newValue == "1") {
        $('#dlg1').dialog({ modal: true });
        $('#dlg1').dialog('open').dialog('setTitle', '组套项目');
        $('#dgwest').datagrid({
            method: 'get',
            striped: true,
            fit: true,
            border: false,
            rownumbers: true,
            url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetPubDict',
            singleSelect: true,
            onDblClickRow: function (index, row) {
                $('#dgeast').datagrid('insertRow', {
                    index: 0,
                    row: row
                });
                $('#dgwest').datagrid('deleteRow', index);
            },
            columns: [[
                { field: 'ItemNoName', title: '组套外项目', width: setWidth(0.225) }
            ]],
            queryParams: {
                tableName: 'GroupItem',
                selectedflag: '1',

                ItemNo: $('#txtItemNo').numberbox('getValue')
            },
            loadFilter: function (data) {
                var result = eval("(" + data.ResultDataValue + ")");
                return { total: result.total, rows: result.rows };
            }
        });
        $('#dgeast').datagrid({
            striped: true,
            fit: true,
            border: false,
            url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetPubDict',
            method: 'get',
            rownumbers: true,
            singleSelect: true,
            onDblClickRow: function (index, row) {
                $('#dgwest').datagrid('insertRow', {
                    index: 0,
                    row: row
                });
                $('#dgeast').datagrid('deleteRow', index);
            },
            columns: [[
                { field: 'ItemNoName', title: '组套内项目', width: setWidth(0.225) }
            ]],
            queryParams: {
                tableName: 'GroupItem',
                selectedflag: '2',
                // fields: 'ItemNo'

                ItemNo: $('#txtItemNo').numberbox('getValue')
            },
            loadFilter: function (data) {
                var result = eval("(" + data.ResultDataValue + ")");
                return { total: result.total, rows: result.rows };
            }
        });
    } else {
        $('#dlg1').dialog('close');
    }

}
function leftAllMove() {
    var rows = $('#dgeast').datagrid('getRows') || [],
        oldRows = $('#dgwest').datagrid('getRows') || [],
        newRows = oldRows ? rows.concat(oldRows) : rows,
        data = { success: true, ResultDataValue: Shell.util.JSON.encode({ total: newRows.length, rows: newRows }) };

    $('#dgwest').datagrid('loadData', data);
    $('#dgeast').datagrid('loadData', { success: true, ResultDataValue: Shell.util.JSON.encode({ total: 0, rows: [] }) });

}
function rightAllMove() {

    var rows = $('#dgwest').datagrid('getRows') || [],
        oldRows = $('#dgeast').datagrid('getRows') || [],
        newRows = oldRows ? rows.concat(oldRows) : rows,
        data = { success: true, ResultDataValue: Shell.util.JSON.encode({ total: newRows.length, rows: newRows }) };

    $('#dgeast').datagrid('loadData', data);
    $('#dgwest').datagrid('loadData', { success: true, ResultDataValue: Shell.util.JSON.encode({ total: 0, rows: [] }) });

}
function leftMove() {
    var r1 = $("#dgeast").datagrid('getSelected');
    var r1Index = $("#dgeast").datagrid('getRowIndex', r1);
    if (r1 != null) {
        $('#dgwest').datagrid('insertRow', {
            index: 0,
            row: r1
        });
        $('#dgeast').datagrid('deleteRow', r1Index);
    } else {
        $.messager.alert('提示', '请选择一项', 'warning');
    }
}
function rightMove() {
    var r2 = $("#dgwest").datagrid('getSelected');
    var r2Index = $("#dgwest").datagrid('getRowIndex', r2);
    if (r2 != null) {
        $('#dgeast').datagrid('insertRow', {
            index: 0,
            row: r2
        });
        $('#dgwest').datagrid('deleteRow', r2Index);
    } else {
        $.messager.alert('提示', '请选择一项', 'warning');
    }
}
//组套保存
function GIsave() {

    $("#save1").one('click', function (event) {
        event.preventDefault();

        $(this).prop('disabled', true);
    });
    var addData = '',
        itemNO = $('#txtItemNo').numberbox('getValue'),
        rows = $('#dgeast').datagrid('getRows'),
        length = rows ? rows.length : 0,
        ItemNo;
    if (length >= 0) {
        addData += '{"' + "jsonentity" + '":{"' + "itemno" + '":' + itemNO + ',"' + "itemnolist" + '":[';
        if (length == 0) {
            addData += 0;
        } else {
            ItemNo = rows[0].ItemNoName.split(")")[0].split("(")[1];
            ItemNo = parseInt(ItemNo);
            addData += ItemNo;
        }
        for (var i = 1; i < length; i++) {
            ItemNo = rows[i].ItemNoName.split(")")[0].split("(")[1];
            ItemNo = parseInt(ItemNo);
            addData += ',' + ItemNo;
        }
        addData += ']}}';


        $.ajax({
            type: 'post',
            contentType: 'application/json',
            url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/UpdateGroupItemModelByID',
            data: addData,
            dataType: 'json',
            success: function (data) {
                if (data.success == true) {

                    $('#dgeast').datagrid('load');
                    $('#dgwest').datagrid('load');
                    $('#dlg1').dialog('close');
                } else {
                    $.messager.alert('提示', '修改数据失败！失败信息：' + data.msg);
                }
            }
        });
    }
}

function ShowPItemList(pitemno) {
    if (!pitemno) {
            $.messager.alert("提示", "请选择数据!")
            return;
    }
    $('#ItemList').dialog({
        title: "项目列表",
        modal: true
    }).dialog("open");
    $('#dgItemList').datagrid({
        url: "../../../../../ServiceWCF/DictionaryService.svc/GetParTestItemByItemNo",
        queryParams: {
            ItemNo: pitemno
        },
        method:"GET",
        loadFilter: function (data) {
            if (data.success) {
                var result = eval("(" + data.ResultDataValue + ")");
                return { total: 10000 || 0, rows: result || [] };
            }
            else {
                $.messager.alert("错误信息", data.ErrorInfo ,"error");
                return { "total": 0, "rows": [] };
            }
        }
    });
}
function ShowSubItemList(subitemno) {
    if (!subitemno) {
        $.messager.alert("提示", "请选择数据!")
        return;
    }
    $('#ItemList').dialog({
        title: "项目列表",
        modal: true
    }).dialog("open");
    $('#dgItemList').datagrid({
        url: "../../../../../ServiceWCF/DictionaryService.svc/GetSubTestItemByItemNo",
        queryParams: {
            ItemNo: subitemno
        },
        method: "GET",
        loadFilter: function (data) {
            if (data.success) {
                var result = eval("(" + data.ResultDataValue + ")");
                return { total: 10000 || 0, rows: result || [] };
            }
            else {
                $.messager.alert("错误信息", data.ErrorInfo, "error");
                return { "total": 0, "rows": [] };
            }
        }
    });
}