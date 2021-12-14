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
        collapsible: false,
        idField: 'SectionNo',
        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetPubDict?tableName=' + "PGroup",
        //url: 'data.txt',
        method: 'get',
        striped: true,
        columns: [[
             { field: 'cb', checkbox: 'true' },
                { field: 'SectionNo', title: '项目编码', width: '15%' },
                { field: 'CName', title: '中文名称', width: '24%' },
                { field: 'ShortName', title: '简称', width: '24%' },
                { field: 'ShortCode', title: '简码', width: '24%' },
               
                { field: 'SectionType', title: '专业类型', hidden: true, formatter: function (value, row, index) {
                         if (value == 0) {
                             return row.SectionType = '生化、免疫';
                             
                         }
                         if (value == 1) {
                             
                             return row.SectionType = '微生物';
                         }
                         if (value == 2) {
                             return row.SectionType = '生化、免疫(图)';
                             
                         }
                         if (value == 3) {
                             return row.SectionType = '微生物(图)';
                            
                         }
                         if (value == 4) {
                             return row.SectionType = '细胞学';
                            
                         }
                         if (value == 5) {
                             return row.SectionType = 'Fish检测';
                            
                         }
                         if (value == 6) {
                             return row.SectionType = '院感检测';
                             
                         }
                         if (value == 7) {
                             return row.SectionType = '染色体检测';
                            
                         }
                         if (value == 8) {
                             return row.SectionType = '病理检测';
                            
                         }
                     } },
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
    $('#txtSectionNo').numberbox({
      
        validType: 'length[0,10]',
        onChange: function (newValue, oldValue) {
        var SectionNo = $('#txtSectionNo').numberbox('getValue');
        if (btnType == 'add') {
            if (SectionNo.length > 10)
            {
                $.ajax({
                    success: function () {
                        $('#txtSectionNo').numberbox('clear');
                           
                    }
                })
                   
            } else {
                $.ajax({
                    type: 'get',
                    contentType: 'application/json',
                    url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetPubDict',
                    data: { filerValue: newValue.trim(), tablename: "PGroup", precisequery: "SectionNo" },
                    //filerValue:newValue.trim(),tablename:"TestItem"
                    dataType: "json",
                    success: function (data) {
                        if (data.success) {
                            var data = eval('(' + data.ResultDataValue + ')'),
                           total = data.total || 0;
                            if (total) {
                                $('#txtSectionNo').numberbox('clear');
                                    
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
                async: false,
                dataType: 'json',
                data: {
                    DicTable: "PGroup",
                    LabCodeNo: LABCODENO
                },
                success: function (data) {

                    $.ajax({
                        type: 'get',
                        contentType: 'application/json',
                        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/CopyAllToLabs',
                        async: false,
                        dataType: 'json',
                        data: {
                            DicTable: "PGroup",
                            toLab: LABCODENO
                        },
                        success: function (data) {
                            if (data.success == true) {
                                j += 1;
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
            dataType: 'json',
            data: {
                DicTable: "PGroup",
                toLab: LABCODENO
            },
            success: function (data) {
                if (data.success == true) {
                    j += 1;
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
    var n = 0;
    var rLABCODENO = $('#txtlabNo').combogrid('getValues');
    if (rLABCODENO) {
        for (var i = 0; i < rLABCODENO.length; i++) {
            LABCODENO = rLABCODENO[i];
            $.ajax({
                type: 'get',
                contentType: 'application/json',
                url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/ExistLabsData',
                async: false,
                dataType: 'json',
                data: {
                    DicTable: "PGroup",
                    LabCodeNo: LABCODENO
                },
                success: function (data) {
                    if (data.success == true) {
                        j = j + 1;
                        successData(true)
                        return 0;
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
                        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/DeletePGroupModelByID?SectionNo=' + rows[i].SectionNo,
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
    $('#ddlSuperGroupNo').combobox({

        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetPubDict?tableName=SuperGroup&fields=SuperGroupNo,CName',
        method: 'get',
        valueField: 'SuperGroupNo',
        textField: 'CName',
        loadFilter: function (data) {
            var result = eval("(" + data.ResultDataValue + ")");
            return result.rows||[];
        }

    });
    $('#txtSectionNo').textbox('enable');
    $('#dlg').dialog('open').dialog('setTitle', '新增');
    $('#fm').form('clear');
   
    $("#ddlVisible").combobox('select', '是');
    $('#ddlSectionType').combobox('select', '生化、免疫')

  
}
function edit(index, value) {
    btnType = 'edit';
    $('#txtSectionNo').textbox('disable');
    $('#ddlSuperGroupNo').combobox({

        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetPubDict?tableName=SuperGroup&fields=SuperGroupNo,CName',
        method: 'get',
        valueField: 'SuperGroupNo',
        textField: 'CName',
        loadFilter: function (data) {
            var result = eval("(" + data.ResultDataValue + ")");
            return result.rows;
        }

    });
    var rowData = $("#dg").datagrid('getRows')[index];
    $('#fm').form('load', rowData)

    $('#dlg').dialog('open').dialog('setTitle', '修改');
  
   
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
                tableName: 'PGroup'
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
    if (btnType == "edit") {
        $("#save").one('click', function (event) {
            event.preventDefault();

            $(this).prop('disabled', true);
        });
        var r = '';
        r += '{"jsonentity":{';
        var SectionNo = $("#txtSectionNo").val();
        if (SectionNo == "") {
            errors += 1;
        }
        if (SectionNo) {
            r += '"SectionNo":' + SectionNo + ',';
        }
        var SuperGroupNo = $("#ddlSuperGroupNo").combobox('getValue');//int
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
        var SectionType = $("#ddlSectionType").combobox('getValue');//int
        if (SectionType == "生化、免疫")
        {
            SectionType = 0;
        }
        if (SectionType == "微生物") {
            SectionType = 1;
        }
        if (SectionType == "生化、免疫(图)") {
            SectionType = 2;
        }
        if (SectionType == "微生物(图)") {
            SectionType = 3;
        }
        if (SectionType == "细胞学") {
            SectionType = 4;
        }
        if (SectionType == "Fish检测") {
            SectionType = 5;
        }
        if (SectionType == "院感检测") {
            SectionType = 6;
        }
        if (SectionType == "染色体检测") {
            SectionType = 7;
        }
        if (SectionType == "病理检测") {
            SectionType = 8;
        }
        
        if (SectionType == "") {

        }
        else 
        {
            r += '"SectionType":' + SectionType + ',';
        }
        var CName = $("#txtCName").val();
        if (CName == "") {
            errors += 1;
        }
        if (CName) {
            r += '"CName":"' + CName + '",';
        }
        var SectionDesc = $("#txtSectionDesc").val();
        if (SectionDesc) {
            r += '"SectionDesc":"' + SectionDesc + '",';
        }
        var DispOrder = $("#txtDispOrder").val();//int
        if (DispOrder) {
            r += '"DispOrder":' + DispOrder + ',';
        }
        var Visible = $("#ddlVisible").combobox("getValue");//int
        if (Visible == "是") {
            Visible = 1;
        }
        else if (Visible == "否") {
            Visible = 0;
        }
        if (Visible == "") {

        }
        else {
            r += '"Visible":' + Visible + ',';
        }
        
        var ShortCode = $("#txtShortCode").val();
        if (ShortCode == "") {
            errors += 1;
        }
        if (ShortCode) {
            r += '"ShortCode":"' + ShortCode + '",';
        }
        r += '}}'
        if (errors > 0) {
            $.messager.alert('提示', '请输入值的完整性', 'warning');
            errors = 0;
        } else {
            $.ajax({
                type: 'post',
                contentType: 'application/json',
                url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/UpdatePGroupModelByID',
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
            })
        }
    }
    else if (btnType == 'add') {
        $("#save").one('click', function (event) {
            event.preventDefault();

            $(this).prop('disabled', true);
        });
        var r = '';
        r += '{"jsonentity":{';
        var SectionNo = $("#txtSectionNo").val();
        if (SectionNo == "") {
            errors += 1;
        }
        if (SectionNo) {
            r += '"SectionNo":' + SectionNo + ',';
        }
        var SuperGroupNo = $("#ddlSuperGroupNo").combobox('getValue');//int
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
        var SectionType = $("#ddlSectionType").combobox('getValue');//int
        if (SectionType == "生化、免疫") {
            SectionType = 0;
        }
        if (SectionType == "微生物") {
            SectionType = 1;
        }
        if (SectionType == "生化、免疫(图)") {
            SectionType = 2;
        }
        if (SectionType == "微生物(图)") {
            SectionType = 3;
        }
        if (SectionType == "细胞学") {
            SectionType = 4;
        }
        if (SectionType == "Fish检测") {
            SectionType = 5;
        }
        if (SectionType == "院感检测") {
            SectionType = 6;
        }
        if (SectionType == "染色体检测") {
            SectionType = 7;
        }
        if (SectionType == "病理检测") {
            SectionType = 8;
        }
        if (SectionType!='') {
            r += '"SectionType":' + SectionType + ',';
        }
        var CName = $("#txtCName").val();
        if (CName == "") {
            errors += 1;
        }
        if (CName) {
            r += '"CName":"' + CName + '",';
        }
        var SectionDesc = $("#txtSectionDesc").val();
        if (SectionDesc) {
            r += '"SectionDesc":"' + SectionDesc + '",';
        }
        var DispOrder = $("#txtDispOrder").val();//int
        if (DispOrder) {
            r += '"DispOrder":' + DispOrder + ',';
        }
        var Visible = $("#ddlVisible").combobox('getValue');//int
        if (Visible == "是") {
            Visible = 1;
        }
        else if (Visible == "否") {
            Visible = 0;
        }
        if (Visible>=0) {
            r += '"Visible":' + Visible + ',';
        }
        var ShortCode = $("#txtShortCode").val();
        if (ShortCode == "") {
            errors += 1;
        }
        if (ShortCode) {
            r += '"ShortCode":"' + ShortCode + '",';
        }
        r += '}}'
       // var r = '{"jsonentity":{"SectionNo":"' + SectionNo + '","SuperGroupNo":"' + SuperGroupNo + '","CName":"' + CName + '","Visible":"' + Visible + '","ShortCode":"' + ShortCode + '","SectionDesc":"' + SectionDesc + '","SectionType":"' + SectionType + '","DispOrder":"' + DispOrder + '","ShortName":"' + ShortName + '"}}';
        if (errors > 0) {
            $.messager.alert('提示', '请输入值的完整性', 'warning');
            errors = 0;
        } else {
            $.ajax({
                type: 'post',
                contentType: 'application/json',
                url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/AddPGroupModel',
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