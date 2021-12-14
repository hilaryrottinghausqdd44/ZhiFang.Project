var btnType;
var errors = 0;
var delCount = 0;
$(function () {
    $("#dg").datagrid({
        toolbar: "#toolbar",
        singleSelect: false,
        fit: true,
        fitColumns: true,
        border: false,
        pagination: true,
        rownumbers: true,
        collapsible: false,
        idField: 'ClIENTNO',
        //url: getRoot() + '/ServiceWCF/DictionaryService.svc/GetBill',
        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetPubDict?tableName=' + "CLIENTELE",
      //  url: 'data.txt',
        method: 'get',
        striped: true,
        columns: [[
               { field: 'cb', checkbox: 'true' },
                { field: 'ClIENTNO', title: '项目编码', width: '15%' },
                { field: 'CNAME', title: '中文名称', width: '24%' },
                 { field: 'ENAME', title: '英文名称', width: '24%' },
                { field: 'SHORTCODE', title: '简码', width: '24%' },
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
    $('#txtClIENTNO').numberbox({
      
        validType: 'length[0,10]',
        onChange: function (newValue, oldValue) {
            var ItemNo = $('#txtClIENTNO').numberbox('getValue');
        if (btnType == 'add') {
            if (ItemNo.length > 10) {
                $.ajax({
                    success: function () {
                        $('#txtClIENTNO').numberbox('clear');

                    }
                })

            } else {
                $.ajax({
                    type: 'get',
                    contentType: 'application/json',
                    url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetPubDict',
                    data: { filerValue: newValue.trim(), tablename: "CLIENTELE", precisequery: "ClIENTNO" },
                    //filerValue:newValue.trim(),tablename:"TestItem"
                    dataType: "json",
                    success: function (data) {
                        if (data.success) {
                            var data = eval('(' + data.ResultDataValue + ')'),
                           total = data.total || 0;
                            if (total) {
                                $('#txtClIENTNO').numberbox('clear');

                                $.messager.alert('提示', '数据库已存在此编号！不能重复插入', 'info');
                            }

                        }

                    }
                });
            }
        }
    }
    });
    $('#ddlclientarea').combobox({
        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetPubDict?tableName=ClientEleArea&fields=AreaID,AreaCName',
        method: 'get',
        valueField: 'AreaID',
        textField: 'AreaCName',
        loadFilter: function (data) {
            var result = eval("(" + data.ResultDataValue + ")");
            return result.rows||[];
        }

    })
    $('#ddlbmanno').combobox({
        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetPubDict?tableName=BUSINESSMAN&fields=BMANNO,CNAME',
        method: 'get',
        valueField: 'BMANNO',
        textField: 'CNAME',
        loadFilter: function (data) {
            var result = eval("(" + data.ResultDataValue + ")");
            return result.rows||[];
        }
    })
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
                        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/DeleteCLIENTELEModelByID?clinetNo=' + rows[i].ClIENTNO,
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
    $('#txtClIENTNO').textbox('enable');
    $('#dlg').dialog('open').dialog('setTitle', '新增');
    $('#fm').form('clear');
   
    $("#ddlISUSE").combobox('select', '是');

    btnType = 'add';
}
function edit(index, value) {
    btnType = 'edit';
    var rowData = $("#dg").datagrid('getRows')[index];
    $('#fm').form('load', rowData)

    $('#dlg').dialog('open').dialog('setTitle', '修改');
   // $('#txtClIENTNO').textbox("readonly", true);
    $('#txtClIENTNO').textbox('disable');
   
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
                tableName: 'CLIENTELE'
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
    if (btnType == 'edit')
    {
        $("#save").one('click', function (event) {
            event.preventDefault();

            $(this).prop('disabled', true);
        });
        var r = '';
        r += '{"jsonentity":{';

        var ClIENTNO = $("#txtClIENTNO").val();//int
        if (ClIENTNO == "") {
            errors += 1;
        }
        if (ClIENTNO) {
            r += '"ClIENTNO":' + ClIENTNO + ',';
        }
        var CNAME = $("#txtCNAME").val();
        if (CNAME == "") {
            errors += 1;
        }
        if (CNAME) {
            r += '"CNAME":"' + CNAME + '",';
        }
        var ENAME = $("#txtENAME").val();
        if (ENAME) {
            r += '"ENAME":"' + ENAME + '",';
        }
        var SHORTCODE = $("#txtSHORTCODE").val();
        if (SHORTCODE == "") {
            errors += 1;
        }
        if (SHORTCODE) {
            r += '"SHORTCODE":"' + SHORTCODE + '",';
        }
        var LINKMAN = $("#txtLINKMAN").val();
        if (LINKMAN) {
            r += '"LINKMAN":"' + LINKMAN + '",';
        }
        var LinkManPosition = $("#txtLinkManPosition").val();
        if (LinkManPosition) {
            r += '"LinkManPosition":"' + LinkManPosition + '",';
        }
        var PHONENUM1 = $("#txtPHONENUM1").val();
        if (PHONENUM1) {
            r += '"PHONENUM1":"' + PHONENUM1 + '",';
        }
        var PHONENUM2 = $("#txtPHONENUM2").val();
        if (PHONENUM2) {
            r += '"PHONENUM2":"' + PHONENUM2 + '",';
        }
        var ADDRESS = $("#txtADDRESS").val();
        if (ADDRESS) {
            r += '"ADDRESS":"' + ADDRESS + '",';
        }
        var EMAIL = $("#txtEMAIL").val();
        if (EMAIL) {
            r += '"EMAIL":"' + EMAIL + '",';
        }
        var GroupName = $("#txtGroupName").val();
        if (GroupName) {
            r += '"GroupName":"' + GroupName + '",';
        }
        var MAILNO = $("#txtMAILNO").val();
        if (MAILNO) {
            r += '"MAILNO":"' + MAILNO + '",';
        }
        var bmanno = $("#ddlbmanno").combobox('getValue');//int
        if (bmanno) {
            r += '"bmanno":' + bmanno + ',';
        }
        var PRINCIPAL = $("#txtPRINCIPAL").val();
        if (PRINCIPAL) {
            r += '"PRINCIPAL":"' + PRINCIPAL + '",';
        }
        var FaxNo = $("#txtFaxNo").val();
        if (FaxNo) {
            r += '"FaxNo":"' + FaxNo + '",';
        }
        var AutoFax = $("#txtAutoFax").val();//int
        if (AutoFax) {
            r += '"AutoFax":' + AutoFax + ',';
        }
        var CZDY1 = $("#txtCZDY1").val();
        if (CZDY1) {
            r += '"CZDY1":"' + CZDY1 + '",';
        }
        var CZDY2 = $("#txtCZDY2").val();
        if (CZDY2) {
            r += '"CZDY2":"' + CZDY2 + '",';
        }
        var CZDY3 = $("#txtCZDY3").val();
        if (CZDY3) {
            r += '"CZDY3":"' + CZDY3 + '",';
        }
        var CZDY4 = $("#txtCZDY4").val();
        if (CZDY4) {
            r += '"CZDY4":"' + CZDY4 + '",';
        }
        var CZDY5 = $("#txtCZDY5").val();
        if (CZDY5) {
            r += '"CZDY5":"' + CZDY5 + '",';
        }
        var CZDY6 = $("#txtCZDY6").val();
        if (CZDY6) {
            r += '"CZDY6":"' + CZDY6 + '",';
        }
        var clientarea = $("#ddlclientarea").combobox('getValue');

        if (clientarea) {
            r += '"AreaID":"' + clientarea + '",';
        }
        var RelationName = $("#txtRelationName").val();
        if (RelationName) {
            r += '"RelationName":"' + RelationName + '",';
        }
        var WebLisSourceOrgId = $("#txtWebLisSourceOrgId").val();
        if (WebLisSourceOrgId) {
            r += '"WebLisSourceOrgId":"' + WebLisSourceOrgId + '",';
        }
        var ISUSE = $("#ddlISUSE").combobox('getValue');//int
        if (ISUSE == "是") {
            ISUSE = 1;
        }
        else if (ISUSE == "否") {
            ISUSE = 0;
        }
        if (ISUSE) {
            r += '"ISUSE":' + ISUSE + ',';
        }
        var romark = $("#txtromark").val();
        if (romark) {
            r += '"romark":"' + romark + '",';
        }
        r += '}}';
        if (errors > 0) {
            $.messager.alert('提示', '请检查输入值的完整性', 'warning');
            errors = 0;
        } else {
            $.ajax({

                type: 'post',
                contentType: 'application/json',
                url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/UpdateCLIENTELEModelByID',
                data: r,
                dataType: 'json',
                success: function (data) {
                    if (data.success == true) {
                        $('#dg').datagrid('load');
                        $('#dlg').dialog('close');
                        $('#dg').datagrid('unselectAll');

                    } else {

                        $.messager.alert('提示', '修改数据失败!失败信息:' + data.msg);
                    }
                }
            })
        }
    } else if (btnType == 'add') {
        // alert("ni ");

        $("#save").one('click', function (event) {
            event.preventDefault();

            $(this).prop('disabled', true);
        });
        var r = '';
        r += '{"jsonentity":{';

        var ClIENTNO = $("#txtClIENTNO").val();//int
        if (ClIENTNO == "") {
            errors += 1;
        }
        if (ClIENTNO) {
            r += '"ClIENTNO":' + ClIENTNO + ',';
        }
        var CNAME = $("#txtCNAME").val();
        if (CNAME == "") {
            errors += 1;
        }
        if (CNAME) {
            r += '"CNAME":"' + CNAME + '",';
        }
        var ENAME = $("#txtENAME").val();
        if (ENAME) {
            r += '"ENAME":"' + ENAME + '",';
        }
        var SHORTCODE = $("#txtSHORTCODE").val();
        if (SHORTCODE == "") {
            errors += 1;
        }
        if (SHORTCODE) {
            r += '"SHORTCODE":"' + SHORTCODE + '",';
        }
        var LINKMAN = $("#txtLINKMAN").val();
        if (LINKMAN) {
            r += '"LINKMAN":"' + LINKMAN + '",';
        }
        var LinkManPosition = $("#txtLinkManPosition").val();
        if (LinkManPosition) {
            r += '"LinkManPosition":"' + LinkManPosition + '",';
        }
        var PHONENUM1 = $("#txtPHONENUM1").val();
        if (PHONENUM1) {
            r += '"PHONENUM1":"' + PHONENUM1 + '",';
        }
        var PHONENUM2 = $("#txtPHONENUM2").val();
        if (PHONENUM2) {
            r += '"PHONENUM2":"' + PHONENUM2 + '",';
        }
        var ADDRESS = $("#txtADDRESS").val();
        if (ADDRESS) {
            r += '"ADDRESS":"' + ADDRESS + '",';
        }
        var EMAIL = $("#txtEMAIL").val();
        if (EMAIL) {
            r += '"EMAIL":"' + EMAIL + '",';
        }
        var GroupName = $("#txtGroupName").val();
        if (GroupName) {
            r += '"GroupName":"' + GroupName + '",';
        }
        var MAILNO = $("#txtMAILNO").val();
        if (MAILNO) {
            r += '"MAILNO":"' + MAILNO + '",';
        }
        var bmanno = $("#ddlbmanno").combobox('getValue');//int
        if (bmanno) {
            r += '"bmanno":' + bmanno + ',';
        }
        var PRINCIPAL = $("#txtPRINCIPAL").val();
        if (PRINCIPAL) {
            r += '"PRINCIPAL":"' + PRINCIPAL + '",';
        }
        var FaxNo = $("#txtFaxNo").val();
        if (FaxNo) {
            r += '"FaxNo":"' + FaxNo + '",';
        }
        var AutoFax = $("#txtAutoFax").val();//int
        if (AutoFax) {
            r += '"AutoFax":' + AutoFax + ',';
        }
        var CZDY1 = $("#txtCZDY1").val();
        if (CZDY1) {
            r += '"CZDY1":"' + CZDY1 + '",';
        }
        var CZDY2 = $("#txtCZDY2").val();
        if (CZDY2) {
            r += '"CZDY2":"' + CZDY2 + '",';
        }
        var CZDY3 = $("#txtCZDY3").val();
        if (CZDY3) {
            r += '"CZDY3":"' + CZDY3 + '",';
        }
        var CZDY4 = $("#txtCZDY4").val();
        if (CZDY4) {
            r += '"CZDY4":"' + CZDY4 + '",';
        }
        var CZDY5 = $("#txtCZDY5").val();
        if (CZDY5) {
            r += '"CZDY5":"' + CZDY5 + '",';
        }
        var CZDY6 = $("#txtCZDY6").val();
        if (CZDY6) {
            r += '"CZDY6":"' + CZDY6 + '",';
        }
        var clientarea = $("#ddlclientarea").combobox('getValue');
       
        if (clientarea) {
            r += '"AreaID":"' + clientarea + '",';
        }
        var RelationName = $("#txtRelationName").val();
        if (RelationName) {
            r += '"RelationName":"' + RelationName + '",';
        }
        var WebLisSourceOrgId = $("#txtWebLisSourceOrgId").val();
        if (WebLisSourceOrgId) {
            r += '"WebLisSourceOrgId":"' + WebLisSourceOrgId + '",';
        }
        var ISUSE = $("#ddlISUSE").combobox('getValue');//int
        if (ISUSE == "是") {
            ISUSE = 1;
        }
        else if (ISUSE == "否") {
            ISUSE = 0;
        }
        if (ISUSE) {
            r += '"ISUSE":' + ISUSE + ',';
        }
        var romark = $("#txtromark").val();
        if (romark) {
            r += '"romark":"' + romark + '",';
        }
        r += '}}';
       // var r = '{"jsonentity":{"ClIENTNO":"' + ClIENTNO + '","CNAME":"' + CNAME + '","ENAME":"' + ENAME + '","SHORTCODE":"' + SHORTCODE + '","LINKMAN":"' + LINKMAN + '","LinkManPosition":"' + LinkManPosition + '","PHONENUM1":"' + PHONENUM1 + '","PHONENUM2":"' + PHONENUM2 + '","ADDRESS":"' + ADDRESS + '","EMAIL":"' + EMAIL + '","Groupname":"' + Groupname + '","MAILNO":"' + MAILNO + '","bmanno":"' + bmanno + '","PRINCIPAL":"' + PRINCIPAL + '","FaxNo":"' + FaxNo + '","AutoFax":"' + AutoFax + '","CZDY1":"' + CZDY1 + '","CZDY2":"' + CZDY2 + '","CZDY3":"' + CZDY3 + '","CZDY4":"' + CZDY4 + '","CZDY5":"' + CZDY5 + '","CZDY6":"' + CZDY6 + '","clientarea":"' + clientarea + '","RelationName":"' + RelationName + '","WebLisSourceOrgID":"' + WebLisSourceOrgID + '","ISUSE":"' + ISUSE + '","romark":"' + romark + '"}}';
        if (errors > 0) {
            $.messager.alert('提示', '请检查输入值的完整性', 'warning');
            errors = 0;
        } else {
           // alert("ni ")
            $.ajax({
                
                type: 'post',
                contentType: 'application/json',
                url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/AddCLIENTELEModel',
                data: r,
                dataType: 'json',
                success: function (data) {
                    if (data.success == true) {
                        $('#dg').datagrid('load');
                        $('#dlg').dialog('close');
                        $('#dg').datagrid('unselectAll');
                    } else {
                        $.messager.alert('提示', '插入数据失败！失败信息:' + data.msg);
                    }
                }
            })
        }
    }
}