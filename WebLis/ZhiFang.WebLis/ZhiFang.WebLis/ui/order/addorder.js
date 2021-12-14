//当外面传过来的地址有where则，搜索条件以及按钮会自动隐藏，时间段传输分为两个字段传输。
$(function () {
    $("#dg1").datagrid({
        loadMsg: '数据加载中...',
        title: '条码列表',
        fitColumns: true,
        fit: true,
        toolbar: "#toolbar",
        checkOnSelect: false,
        selectOnCheck: false,
        singleSelect: false,
        border: false,
        cache: false,
       // url: Shell.util.Path.rootPath + '/ServiceWCF/NRequestFromService.svc/GetBarCodeListByWhere',
        method: 'post',
        striped: true,
        columns: [[
                { field: 'cb', checkbox: 'true' },
                { field: 'BarCode', width: "24.5%", title: '中心条码' },
                { field: 'CName', width: "24.5%", title: '姓名' },
                { field: 'CollectDate', width: "25%", title: '采样时间', formatter: function (value) {
                    value = Shell.util.Date.toString(value);
                    return value;
                }
                },
                {
                    field: 'OperDate', width: "25%", title: '开单时间', formatter: function (value) {
                        value = Shell.util.Date.toString(value);
                        return value;
                    }
                },
                { field: 'BarCodeFormNo', hidden: true }
        ]],
        //        queryParm      
        loadFilter: function (data) {
            var result = eval("(" + data.ResultDataValue + ")") || 0;
            return { total: result.total || 0, rows: result.rows || [] };
        },
        onSelect: function (rowIndex, rowData) {
            //$('#dg2').datagrid('loadData', { total: 0, rows: [] });
            $('#dg2').datagrid({
                url: Shell.util.Path.rootPath + '/ServiceWCF/NRequestFromService.svc/GetNrequestItemByBarCodeFormNo',
                queryParams: {
                    BarCodeFormNo: rowData.BarCodeFormNo
                }
            })
        },
        onLoadSuccess: function (data) {

            if (data.total == 0) {
                //$('#dg2').datagrid('loadData', { total: 0, rows: [] });
            } else {
                $('#dg1').datagrid("selectRow", 0);
            }
        },
        onClickRow: function (rowIndex, rowData) {
            $('#dg1').datagrid("clearSelections");
            $('#dg1').datagrid("selectRow", rowIndex);
        }
    });
    $('#btnsave').bind('click', function () {
        save();
    });
    $("#dg2").datagrid({
        loadMsg: '数据加载中...',
        fitColumns: true,
        fit: true,
        //  collapsible:true,
        title: '项目列表',
        singleSelect: false,
        border: false,
        // cache: false,
        method: 'get',
        striped: true,
        columns: [[
                { field: 'ItemNo', width: "16.5%", title: '项目编码' },
                { field: 'CName', width: "17%", title: '项目名称' },
                { field: 'SampleTypeName', width: "17%", title: '样本类型' },
                { field: 'CheckMethodName', width: "17%", title: '检测方法' },
                { field: 'ItemUnit', width: "17%", title: '项目单位' },
                { field: 'Price', width: "17%", title: '项目价格' }
        ]],
        loadFilter: function (data) {
            var result = eval("(" + data.ResultDataValue + ")") || 0;
            return { total: result.total || 0, rows: result.rows || [] };
        }
    })
    $("#txtClientNo").combobox({
        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetClientListByRBAC?page=1&rows=1000&fields=CLIENTELE.CNAME,CLIENTELE.ClIENTNO',
        method: 'GET',
        valueField: 'ClIENTNO',
        textField: 'CNAME',
        loadFilter: function (data) {

            if (data.length > 0) {
                data[0].selected = true;
            }
            return data;
        },
        filter: function (q, row) {
            var opts = $(this).combobox('options');
            return row[opts.textField].indexOf(q) > -1;
        },
        onLoadSuccess: function (data) {
            //if (data.length > 0) {
            //    data[1].selected = true;
            //}
            $(this).combobox('select', data[0].ClIENTNO);
            btnsearch();
        }
    });
    var datetimenow = new Date();
    var date_e = datetimenow.getFullYear() + "-" + (datetimenow.getMonth() + 1) + "-" + datetimenow.getDate();
    var date_s = datetimenow.getFullYear() + '-' + (datetimenow.getMonth() + 1) + '-' + datetimenow.getDate() + ' ' + datetimenow.getHours() + ':' + datetimenow.getMinutes() + ':' + datetimenow.getSeconds();
    // var date = Shell.util.Date.toString(datetimenow, true);
    $('#txtOperateStartDateTime').datebox('setValue', date_e);
    $('#txtOperateEndDateTime').datebox('setValue', date_e);
    $('#orderTime').textbox('setValue', date_s)
    
});
//当where有值时
function wheres() {
    var url = location.search;//获取url中"?"符后的字串
    if (url.indexOf("?") == -1) {
        return {}
    } else {
        $("#toolbar").hide();
        var str = url.substr(1),
            strs = params.split("&"),
            len = strs.length,
            r = '';
        for (var i = 0; i < len; i++) {
            var arrl = strs[i].replace('=', '":"')
            // var arr = arrl[i].split("=");
            if (i == 0) {
                r += arrl;
            } else {
                r += '","' + arrl;

            }
        }

        qwert(r)
    }
}
function qwert(r) {   
    var params = '{"jsonentity":{"'+r+'"}}';
    $.ajax({
        type: 'post',
        contentType: 'application/json',
        url: Shell.util.Path.rootPath + '/ServiceWCF/NRequestFromService.svc/GetBarCodeListByWhere',
        data: params,
        dataType: 'json',
        success: function (data) {
            if (data.success == true) {
                $('#dg1').datagrid('loadData', { success: true, ResultDataValue: data.ResultDataValue });
            } else {
                $.messager.alert('提示', '数据加载失败！');
            }
        }
    });
}
//搜索按钮
function btnsearch() {
    $('#btnsearch').linkbutton('disable');
    var datetimenow = new Date();
    var date = Shell.util.Date.toString(datetimenow, true);
   // var r1 = '';
    var r = '';
   // r += '{';
    r += '{"jsonentity":{';
    var ClientNo = $('#txtClientNo').combobox('getValue');
    if (ClientNo) {      
        r += '"ClientNo":' + ClientNo + ',';
    }
    var ClientName = $('#txtClientNo').combobox('getText');
    if (ClientName) {
        r += '"ClientName":"' + ClientName + '",';
    }
    var SelectDoctor = $('#txtSelectDoctor').textbox('getValue');
    if (SelectDoctor) {
        r += '"DoctorName":"' + SelectDoctor + '",';
    }
    var CName = $('#txtCName').textbox('getValue');
    if (CName) {
        r += '"CName":"' + CName + '",';
    }
    var PatNo = $('#txtPatNo').textbox('getValue');
    if (PatNo) {
        r += '"PatNo":"' + PatNo + '",';
    }
    var OperateStartDateTime = $('#txtOperateStartDateTime').datebox('getValue');    
    var OperateEndDateTime = $('#txtOperateEndDateTime').datebox('getValue');
    if (OperateStartDateTime > OperateEndDateTime) {
        
        $('#txtOperateStartDateTime').datebox('setValue',date);
        $('#txtOperateEndDateTime').datebox('setValue', date);
    }
    if (OperateStartDateTime) {
        r += '"OperDateStart":"' + OperateStartDateTime + '",';
    }
    if (OperateEndDateTime) {
        r += '"OperDateEnd":"' + OperateEndDateTime + '",';
      
    }
    var CollectStartDate = $('#txtCollectStartDate').datebox('getValue');   
    var CollectEndDate = $('#txtCollectEndDate').datebox('getValue');
    if (CollectStartDate > CollectEndDate)
    {
        CollectStartDate = "";
        CollectEndDate = "";
        $('#txtCollectStartDate').datebox('clear');
        $('#txtCollectEndDate').datebox('clear');
    }
    if (CollectStartDate) {
        r += '"CollectDateStart":"' + CollectStartDate + '",';
    }
    if (CollectEndDate) {
        r += '"CollectEndDate":"' + CollectEndDate + '",';
    }
    r += '}}';

    $.ajax({
        type: 'post',
        contentType: 'application/json',
        url: Shell.util.Path.rootPath + '/ServiceWCF/NRequestFromService.svc/GetBarCodeListByWhere',
        data: r,
        dataType: 'json',
        success: function (data) {
            if (data.success == true) {
                $('#dg1').datagrid('loadData', { success: true, ResultDataValue: data.ResultDataValue });
                $('#dg1').datagrid('options').url = Shell.util.Path.rootPath + '/ServiceWCF/NRequestFromService.svc/GetBarCodeListByWhere';
                $('#btnsearch').linkbutton('enable');
            } else {
                $.messager.alert('提示', '数据加载失败！');
                $('#btnsearch').linkbutton('enable');
            }
        }
    });
}
function save() {
    $('#btnsave').linkbutton('disable');
    var error = 0;
    var r = '';
    r += '{"jsonentity":{';
    var BarCodeFormNoList = '[';
    var rows = $("#dg1").datagrid("getChecked");
    for (var i = 0; i < rows.length; i++) {
        if (i == 0) {
            BarCodeFormNoList += rows[i].BarCodeFormNo;
        } else {
            BarCodeFormNoList += ',' + rows[i].BarCodeFormNo;
        }
    };
    BarCodeFormNoList += ']';
    if (BarCodeFormNoList != '[]') {
        r += '"BarCodeFormNoList":' + BarCodeFormNoList + ',';
    }
    var OrderNo = $("#orderNo").textbox('getValue');
    if (OrderNo) {
        r += '"OrderNo":"' + OrderNo + '",';
    }
    var CreateMan = $("#orderMan").textbox('getValue');
    if (CreateMan) {
        r += '"CreateMan":"' + CreateMan + '",';
    }
    var CreateDate = $("#orderTime").textbox('getValue');
    if (CreateDate) {
        r += '"CreateDate":"' + CreateDate + '",';
    }
    // var Note = $("#textarea").textbox('getValue');
    //var Note = $('#textarea').textbox('getValue');
    var Note = $("#textarea").val();
    if (Note) {
        r += '"Note":"' + Note + '",';
    }
    var LabCode = $("#txtClientNo").combobox("getValue");
    if (LabCode) {
        r += '"LabCode":' + LabCode + ',';
    } 
    r += '}}'
   
    if (LabCode == '' && BarCodeFormNoList == '[]') {
        $.messager.alert('提示', '请填写送检单位和勾选中心条码复选框！');
    }
    else {
        $.ajax({
            type: 'post',
            contentType: 'application/json',
            url: Shell.util.Path.rootPath + '/ServiceWCF/NRequestFromService.svc/AddOrder',
            data: r,
            dataType: 'json',
            success: function (data) {
                if (data.success == true) {
                    $('#btnsave').linkbutton('enable');
                    $.messager.alert('提示', '保存成功');
                } else {
                    $.messager.alert('提示', '保存数据失败！失败信息：' + data.msg);
                    $('#btnsave').linkbutton('enable');
                }
            }
        });
    }

}
function getCookie(c_name) {
    if (document.cookie.length > 0) {
        c_start = document.cookie.indexOf(c_name + "=")
        if (c_start != -1) {
            c_start = c_start + c_name.length + 1
            c_end = document.cookie.indexOf(";", c_start)
            if (c_end == -1) c_end = document.cookie.length
            return unescape(document.cookie.substring(c_start, c_end))
        }
    }
    return ""
}
function checkCookie() {
    wheres();
    username = getCookie('ZhiFangUser')
    if (username != null && username != "")
    { $("#orderMan").textbox('setValue', username) } 
}
