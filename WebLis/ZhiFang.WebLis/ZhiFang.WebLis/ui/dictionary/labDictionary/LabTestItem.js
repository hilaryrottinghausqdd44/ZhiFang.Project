//保存类型
var btnType;
//实验室编号
var LabCode;
//已存在对照关系的记录
var contrastList = [];
(function ($) {
    $.fn.numberbox.defaults.filter = function (e) {
        var opts = $(this).numberbox('options');
        var s = $(this).numberbox('getText');
        if (e.which == 45) {	//-
            return (s.indexOf('-') == -1 ? true : false);
        }
        var c = String.fromCharCode(e.which);
        if (c == opts.decimalSeparator) {
            return (s.indexOf(c) == -1 ? true : false);
        } else if (c == opts.groupSeparator) {
            return true;
        } else if ((e.which >= 48 && e.which <= 57 && e.ctrlKey == false && e.shiftKey == false) || e.which == 0 || e.which == 8) {
            return true;
        } else if (e.ctrlKey == true && (e.which == 99 || e.which == 118)) {
            return true;
        } else {
            return false;
        }
    }
})(jQuery);
//程序入口
$(function () {

    var getParams = Shell.util.Path.getRequestParams();
    LabCode = parseInt(getParams.labCode);
    //初始化实验室字典表
    $('#tbTestItem').datagrid({
        title: '实验室项目字典表',
        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetPubDict',
        queryParams: {
            tablename: "B_Lab_TestItem",
            labcode: LabCode
        },
        method: 'GET',
        loadMsg: '数据加载...',
        rownumbers: true,
        pagination: true,
        fitColumns: true,
        singleSelect: false,
        striped: true,
        fit: true,
        toolbar: '#topBars',
        columns: createColumns(),
        pageSize: 50, //每页显示的记录条数，默认为10           
        pageList: [50, 100, 200, 500], //可以设置每页记录条数的列表           
        beforePageText: '第', //页数文本框前显示的汉字           
        afterPageText: '页    共 {pages} 页',
        displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
        loadFilter: function (data) {
            if (data.success) {
                var list = eval('(' + data.ResultDataValue + ')'),
                    result = {};
                result.total = list.total || 0;
                result.rows = list.rows || [];

                var len = result.rows.length;
                for (var i = 0; i < len; i++) {
                    if (result.rows[i].AddTime) {
                        result.rows[i].AddTime = result.rows[i].AddTime.replace(/T/g, ' ');
                    }
                }
                return result;
            } else {
                return { rows: [], total: 0 };
            }
        },
        onBeforeLoad: function (param) {
            if (param.page == 0) return false;
        }
    });

    //初始化组套外项目表
    $('#outItem').datagrid({
        title: '组套外项目',
        method: 'GET',
        loadMsg: '数据加载...',
        fitColumns: true,
        singleSelect: true,
        rownumbers: true,
        striped: true,
        fit: true,
        border: false,
        columns: itemColumns(),
        loadFilter: function (data) {
            if (data.success) {
                var list = eval('(' + data.ResultDataValue + ')') || [],
                    length = list.rows.length || 0,
                    result = {};

                //                for (var i = 0; i < length; i++) {
                //                    list.rows[i].ItemNoName = '(' + list.rows[i].ItemNo + ')' + list.rows[i].CName;
                //                }
                result.total = list.total || 0;
                result.rows = list.rows || [];
                return result;
            }
        },
        onDblClickRow: function (index, row) {
            $('#inItem').datagrid('insertRow', {
                index: 0,
                row: row
            });
            $('#outItem').datagrid('deleteRow', index);
        }
    });

    //初始化组套内项目表
    $('#inItem').datagrid({
        title: '组套内项目',
        method: 'GET',
        loadMsg: '数据加载...',
        fitColumns: true,
        rownumbers: true,
        singleSelect: true,
        striped: true,
        fit: true,
        border: false,
        columns: itemColumns(),
        loadFilter: function (data) {
            if (data.success) {
                var list = eval('(' + data.ResultDataValue + ')') || [],
                    length = list.rows.length || 0,
                    result = {};

                //                for (var i = 0; i < length; i++) {
                //                    list.rows[i].ItemNoName = '(' + list.rows[i].ItemNo + ')' + list.rows[i].CName;
                //                }
                result.total = list.total || 0;
                result.rows = list.rows || [];
                return result;
            }
        },
        onDblClickRow: function (index, row) {
            $('#outItem').datagrid('insertRow', {
                index: 0,
                row: row
            });
            $('#inItem').datagrid('deleteRow', index);
        }
    });

    //loadData();
    $('#ItemNo').textbox({
        onChange: function (newValue, oldValue) {
            if (btnType == 'add' && newValue.length <= 10)
                askService('validate', null, { filerValue: newValue.trim(), labcode: LabCode, tablename: "B_Lab_TestItem", precisequery: "ItemNo" }); //验证主键的唯一性
        }
    });

    $('#copyBar').hide();
    $('#objectLab').combogrid({
        panelWidth: 200,
        method: 'GET',
        rownumbers: true,
        multiple: true,
        fitColumns: true,
        striped: true,
        fit: true,
        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetPubDict?tablename=CLIENTELE&fields=ClIENTNO,CNAME',
        loadFilter: function (data) {
            var list = eval('(' + data.ResultDataValue + ')') || {}, //eval()把字符串转换成JSON格式
                result = {};
            result.rows = list.rows || [];
            result.total = list.total || 0;
            return result;
        },
        idField: 'ClIENTNO',
        textField: 'CNAME',
        columns: [[
            { field: 'chk', checkbox: true, hidden: false },
            { field: 'ClIENTNO', title: '实验室编号', width: fixWidth(0.2), hidden: true },
            { field: 'CNAME', title: '实验室名称', width: fixWidth(0.2) },
        ]]
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

});

//设置列宽
function fixWidth(percent) {
    return document.body.clientWidth * percent;
}

//加载数据
function loadData() {
    var getParams = Shell.util.Path.getRequestParams();
    LabCode = parseInt(getParams.labCode);
    $('#txtSearch').searchbox('setValue', null);
    $('#tbTestItem').datagrid('options').url = Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetPubDict';
    $('#tbTestItem').datagrid('load', {
        tablename: "B_Lab_TestItem",
        labcode: LabCode,
        time: new Date().getTime()
    });
}

//创建组套数据列
function itemColumns() {
    var columns = [
        [
            { field: 'ItemNoName', width: fixWidth(0.3) }
        ]
    ];
    return columns;
}

//创建数据列
function createColumns() {
    var columns = [
        [
            { field: 'chk', checkbox: true, hidden: false },
            { field: 'AddTime', title: '通过时间', width: 130, sortable: true },
            { field: 'ItemNo', title: '项目编码', width: 100, sortable: true },
            { field: 'CName', title: '中文名称', width: 150, sortable: true },
            { field: 'EName', title: '英文名称', width: 100, sortable: true },
            //{ field: 'ShortName', title: '简称', width: 50,sortable:true },
            //{ field: 'ShortCode', title: '简码', width: 50,sortable:true },
            { field: 'MarketPrice', title: '市场价格', width: 50, sortable: true },
            { field: 'GreatMasterPrice', title: '实际价格', width: 50, sortable: true },
            { field: 'Price', title: '折扣价格', width: 50, sortable: true },
            { field: 'BonusPercent', title: '咨询费', width: 50, sortable: true },
            {
                field: 'UseFlag', title: '是否在用', width: 60, sortable: true,
                formatter: function (value, row) {
                    var useCName = row.UseFlag ? '已启用' : '已禁用';
                    return useCName;
                },
                styler: function (value, row) {
                    var color = row.UseFlag ? 'black' : 'red';
                    return 'Color:' + color;
                }
            },
            {
                field: 'IsCombiItem', title: '是否是组套', width: 60, sortable: true,
                formatter: function (value, row) {
                    var useCName = row.IsCombiItem ? '是' : '否';
                    return useCName;
                },
                styler: function (value, row) {
                    var color = row.IsCombiItem ? 'black' : 'red';
                    return 'Color:' + color;
                }
            },
            { field: 'ControlStatus', title: '对照状态', width: 30, sortable: true }, //数据库中不存在此字段名
            {
                field: 'opt', title: '操作', align: 'right', width: 150, align: 'center',
                formatter: function (value, row, index) {
                    var useFlag = row.UseFlag ? '禁用' : '启用';
                    var edit = '<a href="javascript:void(0)"  data-options="iconCls:icon-edit" style="margin-right: 10px" onclick="editRow(' + index + ')">修改</a>';
                    var disable = '<a href="javascript:void(0)"  data-options="iconCls:icon-edit" onclick="disableRecord(' + index + ')">' + useFlag + '</a>';

                    var price = '<a href="javascript:void(0)"  data-options="iconCls:icon-edit" style="margin-right: 10px" onclick="editPrice(' + index + ')">价格</a>';
                    var sub = ""
                    if (row.IsCombiItem && row.IsCombiItem == 1) {
                        sub = "<a href='#' data-options='iconCls:icon-edit' style='margin-right: 10px' onclick='ShowSubItemList(" + row.ItemNo + ")'>组套内项目</a>";
                    }
                    var par = "<a href='#' data-options='iconCls:icon-edit' style='margin-right: 10px' onclick='ShowPItemList(" + row.ItemNo + ")'>父组套</a>";
                    return price + edit + par + sub + disable;
                }
            }
        ]
    ];
    return columns;
}

//设置下拉列表框的默认值
function setCombobox(row) {

    //是否计算项目下拉列表框
    $('#IsCalc').combobox({
        valueField: 'IsCalc',
        textField: 'text',
        editable: false,
        panelHeight: 50,
        data: [
            { IsCalc: 0, text: '否' },
            { IsCalc: 1, text: '是' }
        ],
        onLoadSuccess: function () {
            if (row.IsCalc) {
                $(this).combobox('select', row.IsCalc);
            } else {
                $(this).combobox('select', 0);
            }
        }
    });

    //精度下拉列表框
    $('#Prec').combobox({
        valueField: 'Prec',
        textField: 'text',
        editable: false,
        data: [
            { Prec: 0, text: 0 },
            { Prec: 1, text: 1 },
            { Prec: 2, text: 2 },
            { Prec: 3, text: 3 },
            { Prec: 4, text: 4 },
            { Prec: 5, text: 5 },
            { Prec: 6, text: 6 },
            { Prec: 7, text: 7 },
            { Prec: 8, text: 8 },
            { Prec: 9, text: 9 },
            { Prec: 10, text: 10 }
        ],
        onLoadSuccess: function () {
            if (row.Prec) {
                $(this).combobox('select', row.Prec);
            } else {
                $(this).combobox('select', 0);
            }
        }
    });

    //是否是组合项目下拉列表框
    $('#IsProfile').combobox({
        valueField: 'IsProfile',
        textField: 'text',
        editable: false,
        panelHeight: 50,
        data: [
            { IsProfile: 0, text: '否' },
            { IsProfile: 1, text: '是' }
        ],
        onLoadSuccess: function () {
            if (row.IsProfile) {
                $(this).combobox('select', row.IsProfile);
            } else {
                $(this).combobox('select', 0);
            }
        }
    });

    //是否显示下拉列表框
    $('#Visible').combobox({
        valueField: 'Visible',
        textField: 'text',
        editable: false,
        panelHeight: 50,
        data: [
            { Visible: 0, text: '否' },
            { Visible: 1, text: '是' }
        ],
        onLoadSuccess: function () {
            if (row.Visible) {
                $(this).combobox('select', row.Visible);
            } else {
                $(this).combobox('select', 0);
            }
        }
    });

    //提示等级下拉列表框
    $('#Cuegrade').combobox({
        valueField: 'Cuegrade',
        textField: 'text',
        editable: false,
        panelHeight: 50,
        data: [
            { Cuegrade: 0, text: '普通' },
            { Cuegrade: 1, text: '特殊' }
        ],
        onLoadSuccess: function () {
            if (row.Cuegrade) {
                $(this).combobox('select', row.Cuegrade);
            } else {
                $(this).combobox('select', 0);
            }
        }
    });

    //是否是医嘱项目下拉列表框
    $('#IsDoctorItem').combobox({
        valueField: 'IsDoctorItem',
        textField: 'text',
        editable: false,
        panelHeight: 50,
        data: [
            { IsDoctorItem: 0, text: '否' },
            { IsDoctorItem: 1, text: '是' }
        ],
        onLoadSuccess: function () {
            if (row.IsDoctorItem) {
                $(this).combobox('select', row.IsDoctorItem);
            } else {
                $(this).combobox('select', 1);
            }
        }
    });

    //是否是收费项目下拉列表框
    $('#IschargeItem').combobox({
        valueField: 'IschargeItem',
        textField: 'text',
        editable: false,
        panelHeight: 50,
        data: [
            { IschargeItem: 0, text: '否' },
            { IschargeItem: 1, text: '是' }
        ],
        onLoadSuccess: function () {
            if (row.IschargeItem) {
                $(this).combobox('select', row.IschargeItem);
            } else {
                $(this).combobox('select', 1);
            }
        }
    });

    //保密等级下拉列表框
    $('#Secretgrade').combobox({
        valueField: 'Secretgrade',
        textField: 'text',
        editable: false,
        panelHeight: 50,
        data: [
            { Secretgrade: 0, text: '不保密' },
            { Secretgrade: 1, text: '保密' }
        ],
        onLoadSuccess: function () {
            if (row.Secretgrade) {
                $(this).combobox('select', row.Secretgrade);
            } else {
                $(this).combobox('select', 0);
            }
        }
    });

    //实验室
    $('#Color').combobox({
        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetAllItemColorDict',
        method: 'GET',
        valueField: 'ColorID',
        textField: 'ColorName',
        loadFilter: function (data) {
            data = eval('(' + data.ResultDataValue + ')').rows || []; //eval()把字符串转换成JSON格式
            return data;
        },
        onLoadSuccess: function () {
            var data = $(this).combobox('getData'),
                length = data.length;
            if (row.Color !== null && row.Color !== undefined) {
                for (var i = 0; i < length; i++) {
                    if (row.Color == data[i].ColorName) {
                        $(this).combobox('select', data[i].ColorID);
                        break;
                    }
                }
            } else {
                $(this).combobox('select', data[0].ColorID); //默认第一项的值
            }
        },
        filter: function (q, row) {
            var opts = $(this).combobox('options');
            return row[opts.textField].indexOf(q) > -1; //返回true,则显示出来
        }

    });

    //是否是组套项目下拉列表框
    $('#IsCombiItem').combobox({
        valueField: 'IsCombiItem',
        textField: 'text',
        editable: false,
        panelHeight: 50,
        data: [
            { IsCombiItem: 0, text: '否' },
            { IsCombiItem: 1, text: '是' }
        ],
        onLoadSuccess: function () {
            if (row.IsCombiItem) {
                $(this).combobox('select', row.IsCombiItem);
                if (row.IsCombiItem === 1) {
                    $('#dlgItem').dialog('open').dialog('setTitle', '组套项目');
                    $('#dlgItem').window('center');
                    $('#dlgSearch').searchbox('setValue', null);
                    var selectFlag = btnType === 'add' ? 0 : 1;
                    //加载组套外数据
                    $('#outItem').datagrid('options').url = Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetPubDict';
                    $('#outItem').datagrid('load', {
                        tableName: 'B_Lab_GroupItem',
                        itemno: $('#ItemNo').textbox('getValue'),
                        labcode: LabCode,
                        selectedflag: selectFlag,
                        time: new Date().getTime()
                    });
                    //加载组套内数据
                    $('#inItem').datagrid('options').url = Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetPubDict';
                    $('#inItem').datagrid('load', {
                        tableName: 'B_Lab_GroupItem',
                        itemno: $('#ItemNo').textbox('getValue'),
                        labcode: LabCode,
                        selectedflag: '2',
                        time: new Date().getTime()
                    });
                }
            } else {
                $(this).combobox('select', 0);
            }
        },
        onSelect: function (record) {
            if (record.IsCombiItem === 1) {
                $('#dlgItem').dialog({ modal: true });
                $('#dlgItem').dialog('open').dialog('setTitle', '组套项目');
                $('#dlgItem').window('center');
                $('#dlgSearch').searchbox('setValue', null);
                var selectFlag = btnType === 'add' ? 0 : 1;
                //加载组套外数据
                $('#outItem').datagrid('options').url = Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetPubDict';
                $('#outItem').datagrid('load', {
                    tableName: 'B_Lab_GroupItem',
                    itemno: $('#ItemNo').textbox('getValue'),
                    labcode: LabCode,
                    selectedflag: selectFlag,
                    time: new Date().getTime()
                });
                //加载组套内数据
                $('#inItem').datagrid('options').url = Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetPubDict';
                $('#inItem').datagrid('load', {
                    tableName: 'B_Lab_GroupItem',
                    itemno: $('#ItemNo').textbox('getValue'),
                    labcode: LabCode,
                    selectedflag: '2',
                    time: new Date().getTime()
                });
            }
        }
    });

    //检验大组下拉列表框
    $('#LabSuperGroupNo').combobox({
        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetPubDict?tableName=B_Lab_SuperGroup&fields=LabSuperGroupNo,CName' + '&labcode=' + LabCode,
        method: 'GET',
        valueField: 'LabSuperGroupNo',
        textField: 'CName',
        panelHeight: 150,
        loadFilter: function (data) {
            data = eval('(' + data.ResultDataValue + ')').rows || []; //eval()把字符串转换成JSON格式
            return data;
        },
        onLoadSuccess: function () {
            var data = $(this).combobox('getData');
            if (data.length > 0) {
                if (row.LabSuperGroupNo !== null && row.LabSuperGroupNo !== undefined)
                    $(this).combobox('select', row.LabSuperGroupNo);
                else
                    $(this).combobox('select', data[0].LabSuperGroupNo); //默认第一项的值
            }
        },
        filter: function (q, row) {
            var opts = $(this).combobox('options');
            return row[opts.textField].indexOf(q) > -1; //返回true,则显示出来
        }
    });

    //是否使用下拉列表框
    $('#UseFlag').combobox({
        valueField: 'UseFlag',
        textField: 'text',
        editable: false,
        panelHeight: 50,
        data: [
            { UseFlag: 0, text: '否' },
            { UseFlag: 1, text: '是' }
        ],
        onLoadSuccess: function () {
            if (row.UseFlag) {
                $(this).combobox('select', row.UseFlag);
            } else {
                $(this).combobox('select', 0);
            }
        }
    });

    //体检标记下拉列表框
    $('#PhysicalFlag').combobox({
        valueField: 'PhysicalFlag',
        textField: 'text',
        editable: false,
        panelHeight: 50,
        data: [
            { PhysicalFlag: 0, text: '否' },
            { PhysicalFlag: 1, text: '是' }
        ],
        onLoadSuccess: function () {
            if (row.PhysicalFlag) {
                $(this).combobox('select', row.PhysicalFlag);
            } else {
                $(this).combobox('select', 0);
            }
        }
    });
}

//$.ajax()请求服务
function askService(serviceType, entity, where) {
    var localData = {},
        serviceParam = {}, //请求服务参数
        async = serviceType == 'delete' ? false : true; //删除操作，同步执行$.ajax方法

    if (serviceType == 'add' || serviceType == 'edit' || serviceType == 'updateItem') {
        serviceParam.data = entity;
    }
    serviceParam = setService(serviceType, serviceParam, where); //配置服务参数
    $.ajax({
        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/' + serviceParam.serviceName,
        data: serviceParam.data,
        dataType: 'json',
        type: serviceParam.type,
        timeout: 10000,
        async: async,
        contentType: 'application/json', //不加这个会出现错误
        success: function (data) {
            if (data.success) {
                switch (serviceType) {
                    case 'add': $('#tbTestItem').datagrid('reload');
                        break;
                    case 'edit': $('#tbTestItem').datagrid('reload'); break;
                    case 'delete':
                        $('#tbTestItem').datagrid('load', { tablename: "B_Lab_TestItem", labcode: LabCode, time: new Date().getTime() });
                        $('#txtSearch').searchbox('setValue', null); //增删改成功后，重新加载数据请求
                        break;

                    case 'search':
                        $('#tbTestItem').datagrid('loadData', data);
                        break;
                    case 'validate':
                        var data = eval('(' + data.ResultDataValue + ')'),
                            total = data.total || 0;
                        if (total)
                            $.messager.alert('提示', '数据库已存在此编号！不能重复插入', 'info');
                        break;
                    case 'copyAll':
                    case 'copy':
                        $('#tbTestItem').datagrid('loaded');
                        $.messager.alert('提示', '复制成功！', 'info');
                        break;
                }
            } else {
                $('#tbTestItem').datagrid('loaded');
                var ErrorInfo = data.ErrorInfo;
                (ErrorInfo.indexOf('此项已对照') > -1) ? contrastList.push(where.labItemNo) : null;
            }
        },
        error: function (data) {
            $('#tbTestItem').datagrid('loaded');
            $.messager.alert('提示信息', data.ErrorInfo, 'error');
        }
    });

}

//配置服务参数
function setService(serviceType, serviceParam, where) {

    //数据请求方式（GET,POST）
    switch (serviceType) {
        case 'search':
            serviceParam.serviceName = 'GetPubDict'; //查询数据服务名
            serviceParam.type = 'GET'; //数据请求方式GET
            serviceParam.data = { Id: where }; //发送到服务器的数据,根据ID号查询（这里基本没用，没人记得ID号）
            break;
        case 'delete':
            serviceParam.serviceName = 'DeleteLabTestItemModelByID'; //删除数据服务名DeleteLabTestItemModelByID

            serviceParam.type = 'GET'; //数据请求方式GET
            serviceParam.data = where; //发送到服务器的数据，根据ID号进行删除
            break;
        case 'add':
            serviceParam.serviceName = 'AddLabTestItemModel'; //增加数据服务名
            serviceParam.type = 'POST'; //数据请求方式GET
            break;
        case 'edit':
            serviceParam.serviceName = 'UpdateLabTestItemModelByID'; //编辑数据服务名UpdateReportGroupModelByID
            serviceParam.type = 'POST'; //数据请求方式GET
            break;
        case 'updateItem':
            serviceParam.serviceName = 'UpdateLabGroupItemModelByID'; //修改组套关系服务名UpdateGroupItemModelByID
            serviceParam.type = 'POST'; //数据请求方式GET
            break;
        case 'validate':
            serviceParam.serviceName = 'GetPubDict'; //修改组套关系服务名UpdateGroupItemModelByID
            serviceParam.type = 'GET'; //数据请求方式GET
            serviceParam.data = where; //发送到服务器的数据，根据ID号进行删除
            break;
        case 'copy':
            serviceParam.serviceName = 'BatchCopyItemsToLab';
            serviceParam.type = 'GET';
            serviceParam.data = where;
            break;
        case 'copyAll':
            serviceParam.serviceName = 'CopyAllToLabs';
            serviceParam.type = 'GET';
            serviceParam.data = where;
            break;
    }

    return serviceParam;
}

//获取数据实体（增加，编辑）,形如：{entity:{ID:1,name:'张珊'}}
function getEntity(getType) {
    var result = '';
    result += '{"jsonentity":{'; //构造符合格式的字符串

    var ItemNo = $('#ItemNo').textbox('getValue');
    if (!ItemNo)
        return 1;
    result += '"LabItemNo":"' + ItemNo + '",';

    var LabCode = $('#LabCode').textbox('getValue');
    if (!LabCode)
        return 1;
    result += '"LabCode":' + LabCode + ',';

    var CName = $('#CName').textbox('getValue');
    if (!CName)
        return 1;
    result += '"CName":"' + CName + '",'

    var Unit = $('#Unit').textbox('getValue');
    if (Unit)
        result += '"Unit":"' + Unit + '",';

    var EName = $('#EName').textbox('getValue');
    if (EName)
        result += '"EName":"' + EName + '",';

    var IsCalc = $('#IsCalc').combobox('getValue');
    result += '"IsCalc":' + IsCalc + ',';

    var ShortName = $('#ShortName').textbox('getValue');
    if (!ShortName)
        return 1;
    result += '"ShortName":"' + ShortName + '",';

    var Prec = $('#Prec').combobox('getValue');
    result += '"Prec":' + Prec + ',';

    var ShortCode = $('#ShortCode').textbox('getValue');
    if (!ShortCode)
        return 1;
    result += '"ShortCode":"' + ShortCode + '",';

    var OrderNo = $('#OrderNo').textbox('getValue');
    if (OrderNo)
        result += '"OrderNo":"' + OrderNo + '",';

    var StandardCode = $('#StandardCode').textbox('getValue');
    if (StandardCode)
        result += '"StandardCode":"' + StandardCode + '",';

    var IsProfile = $('#IsProfile').combobox('getValue');
    result += '"IsProfile":' + IsProfile + ',';

    var Price = $('#Price').numberbox('getValue');
    if (Price)
        result += '"Price":"' + Price + '",';

    var MarketPrice = $('#MarketPrice').numberbox('getValue');
    if (MarketPrice)
        result += '"MarketPrice":"' + MarketPrice + '",';

    var BonusPercent = $('#BonusPercent').numberbox('getValue');
    if (BonusPercent)
        result += '"BonusPercent":"' + BonusPercent + '",';

    var GreatMasterPrice = $('#GreatMasterPrice').numberbox('getValue');
    if (GreatMasterPrice)
        result += '"GreatMasterPrice":"' + GreatMasterPrice + '",';

    var Visible = $('#Visible').combobox('getValue');
    result += '"Visible":' + Visible + ',';

    var Cuegrade = $('#Cuegrade').combobox('getValue');
    result += '"Cuegrade":' + Cuegrade + ',';

    var IsDoctorItem = $('#IsDoctorItem').combobox('getValue');
    result += '"IsDoctorItem":' + IsDoctorItem + ',';

    var IschargeItem = $('#IschargeItem').combobox('getValue');
    result += '"IschargeItem":' + IschargeItem + ',';

    var DiagMethod = $('#DiagMethod').textbox('getValue');
    if (DiagMethod)
        result += '"DiagMethod":"' + DiagMethod + '",';

    var DispOrder = $('#DispOrder').textbox('getValue');
    if (DispOrder)
        result += '"DispOrder":"' + DispOrder + '",';

    var Secretgrade = $('#Secretgrade').combobox('getValue');
    result += '"Secretgrade":' + Secretgrade + ',';

    var Color = $('#Color').combobox('getText');
    if (Color)
        result += '"Color":"' + Color + '",';

    var ItemDesc = $('#ItemDesc').textbox('getValue');
    if (ItemDesc)
        result += '"ItemDesc":"' + ItemDesc + '",';

    var FWorkLoad = $('#FWorkLoad').textbox('getValue');
    if (FWorkLoad)
        result += '"FWorkLoad":"' + FWorkLoad + '",';

    var IsCombiItem = $('#IsCombiItem').combobox('getValue');
    result += '"IsCombiItem":' + IsCombiItem + ',';

    var LabSuperGroupNo = $('#LabSuperGroupNo').combobox('getValue');
    if (LabSuperGroupNo)
        result += '"LabSuperGroupNo":' + LabSuperGroupNo + ',';

    var UseFlag = $('#UseFlag').combobox('getValue');
    result += '"UseFlag":' + UseFlag + ',';

    var PhysicalFlag = $('#PhysicalFlag').combobox('getValue');
    result += '"PhysicalFlag":' + PhysicalFlag;


    result += '}}';
    return result.replace(/\\/g, '\\\\');
}

//构造组套数据
function setData() {
    var addData = '',
        itemNO = $('#ItemNo').textbox('getValue'),
        rows = $('#inItem').datagrid('getRows'),
        length = rows ? rows.length : 0;

    if (length >= 0) {
        addData += '{"' + "jsonentity" + '":{"' + "itemno" + '":"' + itemNO + '","' + "labcode" + '":' + LabCode + ',"' + "itemnolist" + '":[';
        var ItemNo;
        if (length == 0) {
            addData += 0;
        } else {
            ItemNo = rows[0].ItemNoName.split("(")[1].split(")")[0];
            //ItemNo = parseInt(ItemNo);
            addData += ItemNo;
        }
        for (var i = 1; i < length; i++) {
            ItemNo = rows[i].ItemNoName.split("(")[1].split(")")[0];
            //ItemNo = parseInt(ItemNo);
            addData += ',' + ItemNo;
        }
        addData += ']}}';

        askService('updateItem', addData, null);
    }
}

//刷新
function refresh() {
    $('#tbTestItem').datagrid('load', { tablename: "B_Lab_TestItem", labcode: LabCode, time: new Date().getTime() });
    $('#txtSearch').searchbox('setValue', null);
}

//新增
function addRow() {
    btnType = 'add';
    var maxHeight = document.body.clientHeight,
        height = (maxHeight - 5) > 550 ? 550 : (maxHeight - 5);

    $('#dlg').dialog({ modal: true, height: height });
    $('#dlg').dialog('open').dialog('setTitle', '新增');

    $('#dlg').dialog('center');
    $('#frm').form('clear');
    $('#LabCode').textbox('setValue', LabCode);
    $('#ItemNo').textbox('enable');
    setCombobox([]); //设置下拉框
}

/**
 * 修改价格
 * @param {Object} index
 * @author Jcall
 * @version 2017-05-22
 */
function editPrice(index) {
    var curData = $('#tbTestItem').datagrid('getData'), //返回当前页加载完毕的数据
        curRow = curData.rows[index];
    if (curRow) {
        $('#edit-price-div').dialog({ modal: true });
        $('#edit-price-div').dialog('open').dialog('setTitle', '修改价格');
        $('#edit-price-div').window('center');
        $("#Edit_LabCode").next().hide();
        $("#Edit_LabItemNo").next().hide();
        $('#edit-price-div-form').form('clear');
        $('#edit-price-div-form').form('load', {
            Edit_LabCode: curRow.LabCode,
            Edit_LabItemNo: curRow.LabItemNo,
            Edit_MarketPrice: curRow.MarketPrice,
            Edit_GreatMasterPrice: curRow.GreatMasterPrice,
            Edit_Price: curRow.Price
        }); //form表加载数据
    }
}
/**
 * 修改价格确认保存
 * @author Jcall
 * @version 2017-05-22
 */
function onEditPriceSave() {
    var MarketPrice = $('#Edit_MarketPrice').numberbox('getValue') || 0,
        GreatMasterPrice = $('#Edit_GreatMasterPrice').numberbox('getValue') || 0,
        Price = $('#Edit_Price').numberbox('getValue') || 0,
        entityStr = [];

    entityStr.push('"LabCode":"' + $('#Edit_LabCode').numberbox('getValue') + '"');
    entityStr.push('"LabItemNo":"' + $('#Edit_LabItemNo').numberbox('getValue') + '"');
    entityStr.push('"MarketPrice":"' + MarketPrice + '"');
    entityStr.push('"GreatMasterPrice":"' + GreatMasterPrice + '"');
    entityStr.push('"Price":"' + Price + '"');

    entityStr = entityStr.join(",");
    entityStr = '{"jsonentity":{' + entityStr + '}}';

    $.ajax({
        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/UpdateLabTestItemModelByID',
        data: entityStr,
        dataType: 'json',
        type: 'post',
        timeout: 30000,
        async: false,
        contentType: 'application/json',
        success: function (data) {
            if (data.success) {
                $('#edit-price-div').dialog('close');
                //列表数据更新
                $('#tbTestItem').datagrid("reload");
            } else {
                $.messager.alert('提示信息', data.ErrorInfo, 'error');
            }
        },
        error: function (data) {
            $.messager.alert('提示信息', data.ErrorInfo, 'error');
        }
    });
}
/**
 * 修改价格确认取消
 * @author Jcall
 * @version 2017-05-22
 */
function onEditPriceCancel() {
    $('#edit-price-div').dialog('close');
}

//修改
function editRow(index) {
    var curData = $('#tbTestItem').datagrid('getData'), //返回当前页加载完毕的数据
        curRow = curData.rows[index];
    if (curRow) {
        btnType = 'edit';
        var maxHeight = document.body.clientHeight,
            height = (maxHeight - 5) > 500 ? 500 : (maxHeight - 5);
        $('#dlg').dialog({ modal: true, height: height });
        $('#dlg').dialog('open').dialog('setTitle', '修改');
        $('#dlg').window('center');
        $('#frm').form('clear');
        $('#frm').form('load', curRow); //form表加载数据
        $('#ItemNo').textbox('disable');
        setCombobox(curRow); //设置下拉框
    }
}

//删除
function deleteRow() {
    var rows = $('#tbTestItem').datagrid('getSelections') || [],
        length = rows.length || 0;

    if (length == 0) {
        $.messager.alert('提示信息', '请选择记录', 'info');
    } else {
        $.messager.confirm('确认', '确认删除吗？', function (btn) {
            if (btn) {
                for (var i = 0; i < length; i++) {
                    askService('delete', null, { labItemNo: rows[i]['LabItemNo'], labcode: LabCode });
                }
                $('#tbTestItem').datagrid('clearSelections'); //因为getSelections会记忆选过的记录，所以要清空一下
                if (contrastList.length) {
                    var records = contrastList[0];
                    for (var i = 1; i < contrastList.length; i++) {
                        records += ',' + contrastList[i];
                        if ((i + 1) % 5 === 0) {
                            records += '\n';
                        }
                    }
                    $.messager.alert('提示', records + '已存在对照关系,不能删除！', 'info');
                    contrastList = [];
                }
            }
        });
    }
}

//批量禁用
function disableRecords() {
    var rows = $('#tbTestItem').datagrid('getSelections'),
        index;

    if (rows.length == 0) {
        $.messager.alert('提示信息', '请选择记录', 'info');
    } else {
        $.messager.confirm('确认', '确认禁用所选记录吗？', function (btn) {
            if (btn) {
                for (var i = 0; i < rows.length; i++) {
                    if (rows[i]['UseFlag']) {
                        index = $('#tbTestItem').datagrid('getRowIndex', rows[i]);
                        disableOrEnable(index, 0);
                    }
                }
                $('#tbTestItem').datagrid('clearSelections'); //因为getSelections会记忆选过的记录，所以要清空一下
            }
        });
    }
}

//批量启用
function enableRecords() {
    var rows = $('#tbTestItem').datagrid('getSelections'),
        index;

    if (rows.length == 0) {
        $.messager.alert('提示信息', '请选择记录', 'info');
    } else {
        $.messager.confirm('确认', '确认启用所选记录吗？', function (btn) {
            if (btn) {
                for (var i = 0; i < rows.length; i++) {
                    if (!rows[i]['UseFlag']) {
                        index = $('#tbTestItem').datagrid('getRowIndex', rows[i]);
                        disableOrEnable(index, 1);
                    }
                }
                $('#tbTestItem').datagrid('clearSelections'); //因为getSelections会记忆选过的记录，所以要清空一下
            }
        });
    }
}

//复制
function copyToLab() {
    $('#copyBar').toggle();
}

//复制保存
function copyRecords() {
    var rows = $('#objectLab').combogrid('getValues'),
        length = rows.length || 0,
        itemNos = $('#tbTestItem').datagrid('getSelections'),
        itemLength = itemNos.length || 0,
        listItemNo = '',
        objectLab = '';

    if (!length) {
        $.messager.alert('提示', '请选择目标实验室', 'info');
        return;
    } else {
        if (itemLength) {
            $.messager.confirm('确认', '确定把当前勾选的数据复制到目标实验室吗？', function (btn) {
                if (btn) {
                    var opt = $('#tbTestItem').datagrid('options');
                    opt.loadMsg = '正在执行当前操作...';
                    $('#tbTestItem').datagrid('loading');
                    listItemNo = itemNos[0]['ItemNo'];
                    for (var i = 1; i < itemLength; i++) {
                        listItemNo += ',' + itemNos[i]['ItemNo'];
                    }
                    for (var j = 0; j < length; j++) {
                        objectLab = rows[j];
                        askService('copy', null, { ItemNos: listItemNo, fromLabCodeNo: LabCode, LabCodeNo: objectLab });
                    }
                    $('#tbTestItem').datagrid('clearSelections');
                    opt.loadMsg = '数据加载...';
                }
            });
        } else {
            $.messager.confirm('确认', '确定把当前实验室的数据复制到目标实验室吗？', function (btn) {
                if (btn) {
                    var opt = $('#tbTestItem').datagrid('options');
                    opt.loadMsg = '正在执行当前操作...';
                    $('#tbTestItem').datagrid('loading');
                    objectLab = rows[0];
                    for (var i = 1; i < length; i++) {
                        objectLab += ',' + rows[i];
                    }
                    askService('copyAll', null, { DicTable: 'Lab_TestItem', fromLab: LabCode, toLab: objectLab });
                    opt.loadMsg = '数据加载...';
                }
            });
        }
    }
}
//单个禁用/启用
function disableRecord(index) {
    var curData = $('#tbTestItem').datagrid('getData'), //返回当前页加载完毕的数据
        curRow = curData.rows[index],
        useFlag = curRow.UseFlag ? 0 : 1,
        newText = useFlag ? '启用' : '禁用';

    $.messager.confirm('确认', '确认' + newText + '吗？', function (btn) {
        if (btn) {
            disableOrEnable(index, useFlag);
        }
    });

}

//禁用/启用方法
function disableOrEnable(index, UseFlag) {
    var curData = $('#tbTestItem').datagrid('getData'), //返回当前页加载完毕的数据
        curRow = curData.rows[index],
        useFlag = UseFlag,
        result = '';

    result += '{"jsonentity":{'; //构造符合格式的字符串
    result += '"LabItemNo":"' + curRow.ItemNo + '",';
    result += '"LabCode":' + LabCode + ',';
    if (!curRow.CName && curRow.CName !== 0) {
    } else { result += '"CName":"' + curRow.CName + '",'; }

    if (!curRow.Unit && curRow.Unit !== 0) {
    } else { result += '"Unit":"' + curRow.Unit + '",'; }

    if (!curRow.EName && curRow.EName !== 0) { } else {
        result += '"EName":"' + curRow.EName + '",';
    }

    if (!curRow.IsCalc && curRow.IsCalc !== 0) { } else {
        result += '"IsCalc":' + curRow.IsCalc + ',';
    }
    if (!curRow.ShortName && curRow.ShortName !== 0) { } else {
        result += '"ShortName":"' + curRow.ShortName + '",';
    }

    if (!curRow.Prec && curRow.Prec !== 0) { } else {
        result += '"Prec":' + curRow.Prec + ',';
    }

    if (!curRow.ShortCode && curRow.ShortCode !== 0) { } else {
        result += '"ShortCode":"' + curRow.ShortCode + '",';
    }

    if (!curRow.OrderNo && curRow.OrderNo !== 0) { } else {
        result += '"OrderNo":"' + curRow.OrderNo + '",';
    }

    if (!curRow.StandardCode && curRow.StandardCode !== 0) { } else {
        result += '"StandardCode":"' + curRow.StandardCode + '",';
    }

    if (!curRow.IsProfile && curRow.IsProfile !== 0) { } else {
        result += '"IsProfile":' + curRow.IsProfile + ',';
    }

    if (!curRow.Price && curRow.Price !== 0) { } else {
        result += '"Price":"' + curRow.Price + '",';
    }

    if (!curRow.MarketPrice && curRow.MarketPrice !== 0) { } else {
        result += '"MarketPrice":"' + curRow.MarketPrice + '",';
    }

    if (!curRow.GreatMasterPrice && curRow.GreatMasterPrice !== 0) { } else {
        result += '"GreatMasterPrice":"' + curRow.GreatMasterPrice + '",';
    }

    if (!curRow.Visible && curRow.Visible !== 0) { } else {
        result += '"Visible":' + curRow.Visible + ',';
    }

    if (!curRow.Cuegrade && curRow.Cuegrade !== 0) { } else {
        result += '"Cuegrade":' + curRow.Cuegrade + ',';
    }

    if (!curRow.IsDoctorItem && curRow.IsDoctorItem !== 0) { } else {
        result += '"IsDoctorItem":' + curRow.IsDoctorItem + ',';
    }

    if (!curRow.IschargeItem && curRow.IschargeItem !== 0) { } else {
        result += '"IschargeItem":' + curRow.IschargeItem + ',';
    }

    if (!curRow.DiagMethod && curRow.DiagMethod !== 0) { } else {
        result += '"DiagMethod":"' + curRow.DiagMethod + '",';
    }

    if (!curRow.DispOrder && curRow.DispOrder !== 0) { } else {
        result += '"DispOrder":"' + curRow.DispOrder + '",';
    }

    if (!curRow.Secretgrade && curRow.Secretgrade !== 0) { } else {
        result += '"Secretgrade":' + curRow.Secretgrade + ',';
    }

    if (!curRow.Color && curRow.Color !== 0) { } else {
        result += '"Color":"' + curRow.Color + '",';
    }

    if (!curRow.ItemDesc && curRow.ItemDesc !== 0) { } else {
        result += '"ItemDesc":"' + curRow.ItemDesc + '",';
    }

    if (!curRow.FWorkLoad && curRow.FWorkLoad !== 0) { } else {
        result += '"FWorkLoad":"' + curRow.FWorkLoad + '",';
    }

    if (!curRow.IsCombiItem && curRow.IsCombiItem !== 0) { } else {
        result += '"IsCombiItem":' + curRow.IsCombiItem + ',';
    }

    if (!curRow.LabSuperGroupNo && curRow.LabSuperGroupNo !== 0) { } else {
        result += '"LabSuperGroupNo":' + curRow.LabSuperGroupNo + ',';
    }
    if (!curRow.PhysicalFlag && curRow.PhysicalFlag !== 0) { } else {
        result += '"PhysicalFlag":' + curRow.PhysicalFlag + ',';
    }

    result += '"UseFlag":' + useFlag;

    result += '}}';

    askService('edit', result, null);
}

//查询
function search(value) {
    $('#tbTestItem').datagrid('load', { filerValue: value.trim(), labcode: LabCode, tablename: "B_Lab_TestItem", time: new Date().getTime() });
}

//保存
function save() {
    var entity;
    entity = getEntity(btnType);
    if (entity > 0) {
        $.messager.alert('警告', '请检查输入值的完整性', 'warning');
        return;
    }
    askService(btnType, entity); //请求服务器
    $('#dlg').dialog('close');
}

//取消
function cancel() {
    $('#dlg').dialog('close');
}

//全部左移
function leftAllMove() {
    var rows = $('#inItem').datagrid('getRows') || [],
        oldRows = $('#outItem').datagrid('getRows') || [],
        newRows = oldRows ? rows.concat(oldRows) : rows,
        data = { success: true, ResultDataValue: Shell.util.JSON.encode({ total: newRows.length, rows: newRows }) };

    $('#outItem').datagrid('loadData', data);
    $('#inItem').datagrid('loadData', { success: true, ResultDataValue: Shell.util.JSON.encode({ total: 0, rows: [] }) });

}

//左移
function leftMove() {
    var rows = $('#inItem').datagrid('getSelections'),
        length = rows.length,
        index;
    for (var i = 0; i < length; i++) {
        index = $('#inItem').datagrid('getRowIndex', rows[i]);
        $('#outItem').datagrid('insertRow', {
            index: 0,
            row: rows[i]
        });
        $('#inItem').datagrid('deleteRow', index);
    }
    $('#inItem').datagrid('clearSelections');
}

//全部右移
function rightAllMove() {
    var rows = $('#outItem').datagrid('getRows'),
        oldRows = $('#inItem').datagrid('getRows'),
        newRows = oldRows ? rows.concat(oldRows) : rows,
        data = { success: true, ResultDataValue: Shell.util.JSON.encode({ total: newRows.length, rows: newRows }) };

    $('#inItem').datagrid('loadData', data);
    $('#outItem').datagrid('loadData', { success: true, ResultDataValue: Shell.util.JSON.encode({ total: 0, rows: [] }) });

}

//右移
function rightMove() {
    var rows = $('#outItem').datagrid('getSelections'),
        length = rows.length,
        index;
    for (var i = 0; i < length; i++) {
        index = $('#outItem').datagrid('getRowIndex', rows[i]);
        $('#inItem').datagrid('insertRow', {
            index: 0,
            row: rows[i]
        });
        $('#outItem').datagrid('deleteRow', index);
    }
    $('#outItem').datagrid('clearSelections');
}

//组套查询
function searchItem(value) {

    if (btnType === 'add') {
        $('#outItem').datagrid('load', {
            tableName: 'B_Lab_GroupItem',
            labcode: LabCode,
            selectedflag: '0',
            filerValue: value.trim(),
            time: new Date().getTime()
        });
    } else {
        $('#outItem').datagrid('load', {
            tableName: 'B_Lab_GroupItem',
            itemno: $('#ItemNo').textbox('getValue'),
            labcode: LabCode,
            selectedflag: '1',
            filerValue: value.trim(),
            time: new Date().getTime()
        });
    }
}

//组套保存
function dlgSave() {
    setData();
    $('#dlgItem').dialog('close');
}

//组套取消
function dlgCancel() {
    $('#dlgItem').dialog('close');
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
        url: "../../../ServiceWCF/DictionaryService.svc/GetLabParTestItemByItemNo",
        queryParams: {
            ItemNo: pitemno,
            LabCode: LabCode
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
        url: "../../../ServiceWCF/DictionaryService.svc/GetLabSubTestItemByItemNo",
        queryParams: {
            ItemNo: subitemno,
            LabCode: LabCode
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