//保存类型
var btnType;
//列表全部数据
var listColors;

//程序入口
$(function () {
    //初始化颜色字典表
    $('#tbColor').datagrid({
        title: '颜色字典',
        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetAllItemColorDict',
        method: 'GET',
        loadMsg: '数据加载...',
        rownumbers: true,
        //pagination:true,
        fitColumns: true,
        singleSelect: false,
        striped: true,
        fit: true,
        toolbar: '#topBars',
        columns: createColumns(),
        onBeforeLoad: function (param) {
            if (param.page == 0) return false;
        },
        loadFilter: function (data) {
            if (data.success) {
                var list = eval('(' + data.ResultDataValue + ')'),
                    result = {};
                result.total = list.total || 0;
                result.rows = list.rows || [];
                listColors = (!listColors) ? list.rows || [] : listColors;
                return result;
            }
        }
    });

    //初始化取色器
    initColPick();
});

//设置列宽
function fixWidth(percent) {
    return document.body.clientWidth * percent;
}

//创建数据列
function createColumns() {
    var columns = [
        [
            {field: 'chk', checkbox: true, hidden: false},
            {field: 'ColorID', title: '颜色编号', width: fixWidth(0.2)},
            {field: 'ColorName', title: '颜色名', width: fixWidth(0.2)},
            {
                field: 'ColorValue', title: '颜色值', width: fixWidth(0.2),
                formatter: function (value, row) {
                    var colValue = '<div style="display:inline;float:left;margin:3px;background-color:' +
                        row.ColorValue + ';width:12px;height:12px;"' + '></div>' + row.ColorValue;

                    return colValue;
                }
            },

            {
                field: 'opt', title: '操作', align: 'right', width: fixWidth(0.1), align: 'center',
                formatter: function (value, row, index) {
                    var edit = '<a href="javascript:void(0)"  data-options="iconCls:icon-edit" style="margin-right: 10px" onclick="editRow(' + index + ')">修改</a>';
                    return edit;
                }
            }
        ]
    ];
    return columns;
}

//初始化取色器
function initColPick() {
    $('#ColorValue').colpick({
        layout: 'full',//'rgbhex,hex
        submit: 0,
        colorScheme: 'light',
        onChange: function (hsb, hex, rgb, el, bySetColor) {
            $(el).css('border-color', '#' + hex);
            // Fill the text box just if the color was set using the picker, and not the colpickSetColor function.
            if (!bySetColor) $(el).val(hex);
        }
    }).keyup(function () {
        $(this).colpickSetColor(this.value);
    });
}

//设置下拉列表框的默认值
function setCombobox(row) {

    //是否显示下拉列表框
    $('#Visible').combobox({
        valueField: 'Visible',
        textField: 'text',
        editable: false,
        panelHeight: 50,
        data: [
            {Visible: 0, text: '是'},
            {Visible: 1, text: '否'}
        ],
        onLoadSuccess: function () {
            if (row.Visible) {
                $(this).combobox('select', row.Visible);
            } else {
                $(this).combobox('select', 0);
            }
        }
    });

    //是否使用下拉列表框
    $('#UseFlag').combobox({
        valueField: 'UseFlag',
        textField: 'text',
        editable: false,
        panelHeight: 50,
        data: [
            {UseFlag: 0, text: '否'},
            {UseFlag: 1, text: '是'}
        ],
        onLoadSuccess: function () {
            if (row.UseFlag) {
                $(this).combobox('select', row.UseFlag);
            } else {
                $(this).combobox('select', 0);
            }
        }
    });

}

//$.ajax()请求服务
function askService(serviceType, entity, where) {
    var localData = {},
        serviceParam = {},//请求服务参数
        async = serviceType == 'delete' ? false : true;//删除操作，同步执行$.ajax方法

    if (serviceType == 'add' || serviceType == 'edit') {
        serviceParam.data = entity;
    }
    serviceParam = setService(serviceType, serviceParam, where);//配置服务参数
    $.ajax({
        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/' + serviceParam.serviceName,
        data: serviceParam.data,
        dataType: 'json',
        type: serviceParam.type,
        timeout: 10000,
        async: async,
        contentType: 'application/json',//不加这个会出现错误
        success: function (data) {
            if (data.success) {
                switch (serviceType) {
                    case'add':
                    case'edit':
                    case'delete':
                        $('#tbColor').datagrid('load');
                        $('#txtSearch').searchbox('setValue', null);//增删改成功后，重新加载数据请求
                        break;

                    case'search':
                        $('#tbColor').datagrid('loadData', data);
                        break;
                }
            }
        },
        error: function (data) {
            $.messager.alert('提示信息', data.ErrorInfo, 'error');

        }
    });

}

//配置服务参数
function setService(serviceType, serviceParam, where) {

    //数据请求方式（GET,POST）
    switch (serviceType) {
        case'search':
            serviceParam.serviceName = 'GetPubDict';//查询数据服务名
            serviceParam.type = 'GET';//数据请求方式GET
            serviceParam.data = {Id: where};//发送到服务器的数据,根据ID号查询（这里基本没用，没人记得ID号）
            break;
        case'delete':
            serviceParam.serviceName = 'DeleteItemColorDictByID';//删除数据服务名
            serviceParam.type = 'GET';//数据请求方式GET
            serviceParam.data = where;//发送到服务器的数据，根据ID号进行删除
            break;
        case'add':
            serviceParam.serviceName = 'AddItemColorDict';//增加数据服务名
            serviceParam.type = 'POST';//数据请求方式GET
            break;
        case'edit':
            serviceParam.serviceName = 'UpdateItemColorDictByID';//编辑数据服务名UpdateReportGroupModelByID
            serviceParam.type = 'POST';//数据请求方式GET
            break;
    }

    return serviceParam;
}

//获取数据实体
function getEntity(getType) {
    var result = '';
    result += '{"jsonentity":{';//构造符合格式的字符串

    if (getType == 'edit') {
        var ColorID = $('#ColorID').val().trim();
        result += '"ColorID":' + ColorID + ',';
    }


    var ColorValue = $('#ColorValue').val().trim();
    if (!ColorValue)
        return 1;
    result += '"ColorValue":"#' + ColorValue + '",';


    var ColorName = $('#ColorName').val().trim();
    if (!ColorName)
        return 1;
    result += '"ColorName":"' + ColorName + '"';

    result += '}}';
    return result;
}

//刷新
function refresh() {
    $('#tbColor').datagrid('load');
    $('#txtSearch').searchbox('setValue', null);
}

//新增
function addRow() {
    btnType = 'add';
    $('#dlg').dialog('open').dialog('setTitle', '新增');
    $('#dlg').window('center');
    $('#frm').form('clear');
}

//修改
function editRow(index) {
    var curData = $('#tbColor').datagrid('getData'),//返回当前页加载完毕的数据
        curRow = curData.rows[index];
    if (curRow) {
        btnType = 'edit';
        $('#dlg').dialog('open').dialog('setTitle', '修改');
        $('#dlg').window('center');
        $('#frm').form('clear');
        $('#frm').form('load', curRow);//form表加载数据
    }
}

//删除
function deleteRow() {
    var rows = $('#tbColor').datagrid('getSelections') || [],
        length = rows.length || 0;

    if (length == 0) {
        $.messager.alert('提示信息', '请选择记录', 'info');
    } else {
        $.messager.confirm('确认', '确认删除吗？', function (btn) {
            if (btn) {
                for (var i = 0; i < length; i++) {
                    askService('delete', null, {ColorID: rows[i]['ColorID']});
                }
                $('#tbColor').datagrid('clearSelections');//因为getSelections会记忆选过的记录，所以要清空一下
            }
        });
    }
}

//查询
function search(value) {
    if (!value) {
        $('#tbColor').datagrid('loadData', {
            success: true,
            ResultDataValue: Shell.util.JSON.encode({total: listColors.length, rows: listColors})
        });
    } else {
        var filterRows = [],
            length = listColors.length;

        value = value.trim();
        for (var i = 0; i < length; i++) {
            if ((listColors[i]['ColorID']).toString().indexOf(value) > -1 || (listColors[i]['ColorName'] && (listColors[i]['ColorName']).toString().indexOf(value) > -1)) {
                filterRows.push(listColors[i]);
            }
        }
        $('#tbColor').datagrid('loadData', {
            success: true,
            ResultDataValue: Shell.util.JSON.encode({total: filterRows.length, rows: filterRows})
        });
    }
}

//保存
function save() {
    var entity;
    entity = getEntity(btnType);
    if (entity > 0) {
        $.messager.alert('警告', '请检查输入值的完整性', 'warning');
        return;
    }
    askService(btnType, entity);//请求服务器
    $('#dlg').dialog('close');
}

//取消
function cancel() {
    $('#dlg').dialog('close');
}

