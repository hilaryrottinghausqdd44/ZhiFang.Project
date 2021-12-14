var judge = 0;
var errors = 0;
$(function () {
    $('#dg').datagrid({
        loadMsg: '数据加载中...',
        toolbar: "#toolbar",
        fitColumns: true,
        singleSelect: false,
        border: false,
        fit: true,
        pagination: true,
        pageList: [10, 20, 50, 100, 500],
        rownumbers: true,
        collapsible: false,
        idField: 'BWeiXinAccount_Id',
        url: Shell.util.Path.rootPath + '/ServerWCF/WeiXinAppService.svc/ST_UDTO_SearchBWeiXinAccountByHQL',
        method: 'get',
        striped: true,
        columns: [[
                { field: 'BWeiXinAccount_Id', hidden: true },
                { field: 'cb', checkbox: 'true' },
                { field: 'BWeiXinAccount_WeiXinAccount', title: '微信账号', width: '19%' },
                { field: 'BWeiXinAccount_UserName', title: '用户名', width: '13%' },
                { field: 'BWeiXinAccount_SexID', title: '性别', width: '10%', formatter: function (value) {
                    if (value == 0) {
                        value = "女";
                    } else if (value == 1) {
                        value = "男";
                    } else if (value == 2) { 
                        value="未知"
                    }
                    return value;
                }
                },
                { field: 'BWeiXinAccount_MobileCode', title: '手机号码', width: '13%' },
                { field: 'BWeiXinAccount_Language', title: '语言', width: '10%' },
                { field: 'BWeiXinAccount_Comment', title: '描述', width: '13%' },

                {
                    field: 'BWeiXinAccount_LoginInputPasswordFlag', title: '是否记住密码', width: '11%', formatter: function (value) {
                        if (value == "false") {
                            value = "否";
                        } else if (value == "true") {
                            value = "是";
                        }
                        return value;
                    }
                },
               {
                   field: 'Operation', title: '操作', width: '6%', formatter: function (value, row, index) {
                       var edit = "<a href='#' data-options='iconCls:icon-edit' onclick='edit(" + index + "," + value + ")'>修改</a>";
                       return edit;
                   }
               }
        ]],
        queryParams: {
            isPlanish: true
            //limit:10
            //            fields: "WeiXinUserID,BProvince_Name,BProvince_EName,BProvince_SName,BProvince_Shortcode,BProvince_PinYinZiTou,BProvince_Comment,BProvince_IsUse,BProvince_BCountry_Id"
            //            // fields: 'BProvince_Id,BProvince_Name,BProvince_BCountry_Id'
        },

        loadFilter: function (data) {
            var result = eval("(" + data.ResultDataValue + ")") || [];
            return { total: result.count || 0, rows: result.list || []} || [];
        }
    })
//        $("#txtBWeiXinAccount_CountryName").combobox({
//            url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBCountryByHQL?isPlanish=' + true,
//            method: 'get',
//            valueField: 'BCountry_Id',
//            textField: 'BCountry_Name',
//            loadFilter: function (data) {
//                var result = eval("(" + data.ResultDataValue + ")");
//                return result.list || [];
//            }
//        })
//        $("#txtBWeiXinAccount_ProvinceName").combobox({
//            url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBProvinceByHQL?isPlanish=' + true,
//            method: 'get',
//            valueField: 'BProvince_Id',
//            textField: 'BProvince_Name',
//            loadFilter: function (data) {
//                var result = eval("(" + data.ResultDataValue + ")");
//                return result.list || [];
//            }
//        })
//        $("#txtBWeiXinAccount_CityName").combobox({
//            url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBCityByHQL?isPlanish=' + true,
//            method: 'get',
//            valueField: 'BCity_Id',
//            textField: 'BCity_Name',
//            loadFilter: function (data) {
//                var result = eval("(" + data.ResultDataValue + ")");
//                return result.list || [];
//            }
//        })
})
function doSearch(value) {
    //  $('#txtSearchKey').searchbox('disable');
    var SearchKey = $("#txtSearchKey").searchbox("getValue");

    $('#dg').datagrid({
        url: Shell.util.Path.rootPath + '/ServerWCF/WeiXinAppService.svc/ST_UDTO_SearchBWeiXinAccountByHQL?where=' + SearchKey,
        loadFilter: function (data) {
            var result = eval("(" + data.ResultDataValue + ")") || [];
            return { total: result.count || 0, rows: result.list || []} || [];
        },
         queryParams: {
            isPlanish: true,
            where:SearchKey
        }
        
    });

}
function add() {
    $('#dlg').dialog('open').dialog('setTitle', '新增');

    $('#fm').form('clear');
    $('#ddlBWeiXinAccount_LoginInputPasswordFlag').combobox('select', '是');
   // $("#txtBWeiXinAccount_CityName").combobox('select', row.BCity_Name);
    judge = 'add';
}
function edit(index, value) {
    judge = 'edit';
    var rowData = $("#dg").datagrid('getRows')[index];

    $('#fm').form('load', rowData)
    if ($("#ddlBWeiXinAccount_SexID").combobox('getValue') == 0)
    {
        $("#ddlBWeiXinAccount_SexID").combobox('setValue','女');
    } 
    else if ($("#ddlBWeiXinAccount_SexID").combobox('getValue') == 1) 
    {
        $("#ddlBWeiXinAccount_SexID").combobox('setValue','男');
    } 
    else if ($("#ddlBWeiXinAccount_SexID").combobox('getValue') == 2) 
    {
        $("#ddlBWeiXinAccount_SexID").combobox('setValue','未知');
    };
    //var abs = $("#ddlBAccountType_IsUse").combobox('getValue');
    if ($("#ddlBWeiXinAccount_LoginInputPasswordFlag").combobox('getValue') == 'false') {
        $("#ddlBWeiXinAccount_LoginInputPasswordFlag").combobox('setValue','否');
    } else {
        $("#ddlBWeiXinAccount_LoginInputPasswordFlag").combobox('setValue','是');
    }
    $('#dlg').dialog('open').dialog('setTitle', '修改');

}
function save() {
    if (judge == 'add') {
        //   $('#save').linkbutton('disable');
        var r = '';
        r += '{"entity":{';

        var BWeiXinAccount_WeiXinAccount = $('#txtBWeiXinAccount_WeiXinAccount').textbox('getValue');
        if (BWeiXinAccount_WeiXinAccount) {
            r += '"WeiXinAccount":"' + BWeiXinAccount_WeiXinAccount + '",';
        } else {
            errors += 1;
        }
        var BWeiXinAccount_UserName = $('#txtBWeiXinAccount_UserName').textbox("getValue");
//        if (BWeiXinAccount_UserName) {
//            var BProvince_DataTimeStamp = $('#txtBProvince_DataTimeStamp').datetimebox("getValue");
//            r += '"BCountry":{"DataTimeStamp":"' + BProvince_DataTimeStamp + '","Id":"' + BProvince_BCountry_Id + '"},';
        //        }
        if (BWeiXinAccount_UserName) {
            r += '"UserName":"' + BWeiXinAccount_UserName + '",';
        }
        var BWeiXinAccount_SexID = $('#ddlBWeiXinAccount_SexID').combobox("getValue");
        if (BWeiXinAccount_SexID == '女') {
            BWeiXinAccount_SexID = 0;
            r += '"BWeiXinAccount_SexID":' + BWeiXinAccount_SexID + ',';
        }
        if (BWeiXinAccount_SexID == '否') {
            BWeiXinAccount_SexID = 1;
            r += '"BWeiXinAccount_SexID":' + BWeiXinAccount_SexID + ',';
        }
        if (BWeiXinAccount_SexID == '未知') {
            BWeiXinAccount_SexID = 2;
            r += '"BWeiXinAccount_SexID":' + BWeiXinAccount_SexID + ',';
        }
        var BWeiXinAccount_CountryName = $('#txtBWeiXinAccount_CountryName').textbox('getValue');
        if (BWeiXinAccount_CountryName) {
            r += '"CountryName":"' + BWeiXinAccount_CountryName + '",';
        } 
        var BWeiXinAccount_ProvinceName = $('#txtBWeiXinAccount_ProvinceName').textbox('getValue');
        if (BWeiXinAccount_ProvinceName) {
            r += '"ProvinceName":"' + BWeiXinAccount_ProvinceName + '",';
        }
        var BWeiXinAccount_CityName = $('#txtBWeiXinAccount_CityName').textbox('getValue');
        if (BWeiXinAccount_CityName) {
            r += '"CityName":"' + BWeiXinAccount_CityName + '",';
        }
        var BWeiXinAccount_MobileCode = $('#txtBWeiXinAccount_MobileCode').textbox('getValue');
        if (BWeiXinAccount_MobileCode) {
            r += '"MobileCode":"' + BWeiXinAccount_MobileCode + '",';
        }
        var BWeiXinAccount_Language = $('#txtBWeiXinAccount_Language').textbox('getValue');
        if (BWeiXinAccount_Language) {
            r += '"Language":"' + BWeiXinAccount_Language + '",';
        }
        var BWeiXinAccount_LoginInputPasswordFlag = $('#ddlBWeiXinAccount_LoginInputPasswordFlag').combobox('getValue');
        if (BWeiXinAccount_LoginInputPasswordFlag == '是') {
            BWeiXinAccount_LoginInputPasswordFlag = 1;
            r += '"LoginInputPasswordFlag":' + BWeiXinAccount_LoginInputPasswordFlag + '';
        }
        if (BWeiXinAccount_LoginInputPasswordFlag == '否') {
            BWeiXinAccount_LoginInputPasswordFlag = 0;
            r += '"LoginInputPasswordFlag":' + BWeiXinAccount_LoginInputPasswordFlag + '';
        }
        var BWeiXinAccount_Comment = $('#txtBWeiXinAccount_Comment').textbox('getValue');
        if (BWeiXinAccount_Comment) {
            r += '"Comment":"' + BWeiXinAccount_Comment + '",';
        }
        r += '}}';
        if (errors > 0) {
            $.messager.alert('提示', '请检查输入值的完整性');
            errors = 0;
        } else {
            $.ajax({
                type: 'post',
                contentType: 'application/json',
                url: Shell.util.Path.rootPath + '/ServerWCF/WeiXinAppService.svc/ST_UDTO_AddBWeiXinAccount',
                data: r,
                dataType: 'json',
                success: function (data) {
                    $('#save').linkbutton('enable');
                    $('#dg').datagrid('load');
                    $('#dlg').dialog('close');
                }
            })
        }
    } else if (judge == 'edit') {
        // $('#save').linkbutton('disable');
        var r = '';
        r += '{"entity":{';
        var BWeiXinAccount_WeiXinAccount = $('#txtBWeiXinAccount_WeiXinAccount').textbox('getValue');
        if (BWeiXinAccount_WeiXinAccount) {
            r += '"WeiXinAccount":"' + BWeiXinAccount_WeiXinAccount + '",';
        } else {
            errors += 1;
        }
        var BWeiXinAccount_UserName = $('#txtBWeiXinAccount_UserName').textbox("getValue");
        //        if (BWeiXinAccount_UserName) {
        //            var BProvince_DataTimeStamp = $('#txtBProvince_DataTimeStamp').datetimebox("getValue");
        //            r += '"BCountry":{"DataTimeStamp":"' + BProvince_DataTimeStamp + '","Id":"' + BProvince_BCountry_Id + '"},';
        //        }
        if (BWeiXinAccount_UserName) {
            r += '"UserName":"' + BWeiXinAccount_UserName + '",';
        }
        var BWeiXinAccount_SexID = $('#ddlBWeiXinAccount_SexID').combobox("getValue");
        if (BWeiXinAccount_SexID == '女') {
            BWeiXinAccount_SexID = 0;
            r += '"BWeiXinAccount_SexID":' + BWeiXinAccount_SexID + ',';
        }
        if (BWeiXinAccount_SexID == '否') {
            BWeiXinAccount_SexID = 1;
            r += '"BWeiXinAccount_SexID":' + BWeiXinAccount_SexID + ',';
        }
        if (BWeiXinAccount_SexID == '未知') {
            BWeiXinAccount_SexID = 2;
            r += '"BWeiXinAccount_SexID":' + BWeiXinAccount_SexID + ',';
        }
        var BWeiXinAccount_CountryName = $('#txtBWeiXinAccount_CountryName').textbox('getValue');
        if (BWeiXinAccount_CountryName) {
            r += '"CountryName":"' + BWeiXinAccount_CountryName + '",';
        }
        var BWeiXinAccount_ProvinceName = $('#txtBWeiXinAccount_ProvinceName').textbox('getValue');
        if (BWeiXinAccount_ProvinceName) {
            r += '"ProvinceName":"' + BWeiXinAccount_ProvinceName + '",';
        }
        var BWeiXinAccount_CityName = $('#txtBWeiXinAccount_CityName').textbox('getValue');
        if (BWeiXinAccount_CityName) {
            r += '"CityName":"' + BWeiXinAccount_CityName + '",';
        }
        var BWeiXinAccount_MobileCode = $('#txtBWeiXinAccount_MobileCode').textbox('getValue');
        if (BWeiXinAccount_MobileCode) {
            r += '"MobileCode":"' + BWeiXinAccount_MobileCode + '",';
        }
        var BWeiXinAccount_Language = $('#txtBWeiXinAccount_Language').textbox('getValue');
        if (BWeiXinAccount_Language) {
            r += '"Language":"' + BWeiXinAccount_Language + '",';
        }
        var BWeiXinAccount_LoginInputPasswordFlag = $('#ddlBWeiXinAccount_LoginInputPasswordFlag').combobox('getValue');
        if (BWeiXinAccount_LoginInputPasswordFlag == '是') {
            BWeiXinAccount_LoginInputPasswordFlag = 1;
            r += '"LoginInputPasswordFlag":' + BWeiXinAccount_LoginInputPasswordFlag + '';
        }
        if (BWeiXinAccount_LoginInputPasswordFlag == '否') {
            BWeiXinAccount_LoginInputPasswordFlag = 0;
            r += '"LoginInputPasswordFlag":' + BWeiXinAccount_LoginInputPasswordFlag + '';
        }
        var BWeiXinAccount_Comment = $('#txtBWeiXinAccount_Comment').textbox('getValue');
        if (BWeiXinAccount_Comment) {
            r += '"Comment":"' + BWeiXinAccount_Comment + '",';
        }

        r += '},"fields":"Id,WeiXinAccount,UserName,SexID,CountryName,ProvinceName,CityName,Language,LoginInputPasswordFlag,Comment"}';
        if (errors > 0) {
            $.messager.alert('提示', '请检查输入值的完整性');
            errors = 0;
        } else {
            $.ajax({
                type: 'post',
                contentType: 'application/json',
                url: Shell.util.Path.rootPath + '/ServerWCF/WeiXinAppService.svc/ST_UDTO_UpdateBWeiXinAccountByField',
                data: r,
                dataType: 'json',
                success: function (data) {
                    // $('#save').linkbutton('enable');
                    $('#dg').datagrid('load');
                    $('#dlg').dialog('close');
                }
            })
        }
    }
}
function del() {
    var delCount = 0;
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
                        url: Shell.util.Path.rootPath + '/ServerWCF/WeiXinAppService.svc/ST_UDTO_DelBWeiXinAccount?Id=' + rows[i].BWeiXinAccount_WeiXinUserID,
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
function update() {
    $('#dg').datagrid('load');
    $('#fm').form('clear')
    $('#txtSearchKey').searchbox("clear");
}