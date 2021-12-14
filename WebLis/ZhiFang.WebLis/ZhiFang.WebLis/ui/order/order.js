/**
 * @OverView 订单列表
 * Created by Administrator on 2015/1/29.
 */

//修改备注按钮
var editSwitch = true;
var LabCode;
//程序入口
$(function () {
    $.ajax(
    {
        type: "GET",
        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetClientListByRBAC?page=1&rows=1000&fields=CLIENTELE.ClIENTNO',
        success: function (data) {
            LabCode = data[0].ClIENTNO
        }
    }
   )

    $('#lbnConfirm').linkbutton('disable');
    getCookieValue('ZhiFangUserPosition');
    //初始化项目列表
    $('#tbItems').datagrid({
        method: 'GET',
        loadMsg: '数据加载...',
        fitColumns: true,
        singleSelect: true,
        striped: true,
        fit: true,
        border: false,
        columns: itemColumns(),
        onBeforeLoad: function (param) {
            if (param.page == 0) return false;
        },
        loadFilter: function (data) {
            if (data.success) {
                var list = eval('(' + data.ResultDataValue + ')') || {},
                    result = {};
                result.total = list.total || 0;
                result.rows = list.rows || [];
                return result;
            }
        }
    });

    //初始化条码列表
    $('#tbBarcode').datagrid({
        title: '条码列表',
        method: 'GET',
        loadMsg: '数据加载...',
        fitColumns: true,
        singleSelect: true,
        striped: true,
        fit: true,
        border: false,
        columns: barcodeColumns(),
        onBeforeLoad: function (param) {
            if (param.page == 0) return false;
        },
        loadFilter: function (data) {
            if (data.success) {
                var list = eval('(' + data.ResultDataValue + ')') || {},
                    result = {};
                result.total = list.total || 0;
                result.rows = list.rows || [];
                return result;
            }
        },
        onLoadSuccess: function (data) {
            if (data.total) {
                var BarCodeFormNo = data.rows[0].BarCodeFormNo;
                $('#tbBarcode').datagrid('selectRow', 0);
                $('#tbItems').datagrid('options').url = Shell.util.Path.rootPath + '/ServiceWCF/NRequestFromService.svc/GetNrequestItemByBarCodeFormNo';
                $('#tbItems').datagrid('load', { BarCodeFormNo: BarCodeFormNo });
            }
        },
        onClickRow: function (index, row) {
            var BarCodeFormNo = row.BarCodeFormNo;
            $('#tbItems').datagrid('loadData', { success: true, ResultDataValue: "{total: 0, rows: []}" });
            $('#tbItems').datagrid('load', { BarCodeFormNo: BarCodeFormNo });
        }
    });

    //初始化订单列表
    $('#tbOrder').datagrid({
        title: '订单列表',
        // url:Shell.util.Path.rootPath+'/ServiceWCF/NRequestFromService.svc/GetOrderList',
        method: 'POST',
        loadMsg: '数据加载...',
        fitColumns: true,
        singleSelect: false,
        striped: true,
        fit: true,
        border: false,
        columns: orderColumns(),
        onBeforeLoad: function (param) {
            if (param.page == 0) return false;
        },
        loadFilter: function (data) {
            if (data.success) {
                var list = eval('(' + data.ResultDataValue + ')') || {},
                    result = {};
                result.total = list.total || 0;
                result.rows = list.rows || [];
                return result;
            }
        },
        onLoadSuccess: function (data) {
            if (data.total) {
                var orderNO = data.rows[0].OrderNo,
                    remarks = data.rows[0].Note;
                $('#tbOrder').datagrid('selectRow', 0);
                $('#txtRemarks').val(remarks);
                $('#tbBarcode').datagrid('options').url = Shell.util.Path.rootPath + '/ServiceWCF/NRequestFromService.svc/GetBarCodeByOrderNo';
                $('#tbBarcode').datagrid('load', { OrderNo: orderNO });
            }
        },
        onClickRow: function (index, row) {
            var orderNO = row.OrderNo,
                remarks = row.Note;
            $('#tbOrder').datagrid('unselectAll');
            $('#tbOrder').datagrid('selectRow', index);
            $('#txtRemarks').val(remarks);
            //先清空项目列表和条码列表
            $('#tbItems').datagrid('loadData', { success: true, ResultDataValue: "{total: 0, rows: []}" });
            $('#tbBarcode').datagrid('loadData', { success: true, ResultDataValue: "{total: 0, rows: []}" });

            $('#tbBarcode').datagrid('load', { OrderNo: orderNO });
        }
    });

    //判断时间
    $('#endDate').datebox({
        onSelect: function (date) {
            var startDate = $('#startDate').datebox('getValue'),
                endDate = $('#endDate').datebox('getValue');
            if (endDate < startDate) {
                $('#endDate').datebox('setValue', '');
                $.messager.alert('提示', '不能小于开始时间', 'info');
            }
        }
    });
    $('#startDate').datebox('setValue', setDate(new Date()));
    $('#endDate').datebox('setValue', setDate(new Date()));

    //创建订单数据列
    function orderColumns() {
        var columns = [
            [
                { field: 'chk', checkbox: true, hidden: true },
                { field: 'OrderNo', title: '订单号', width: fixWidth(0.2) },
                { field: 'CreateMan', title: '创单人', width: fixWidth(0.1) },
                { field: 'CreateDate', title: '创单日期', width: fixWidth(0.1),
                    formatter: function (value, row) {
                        var value = Shell.util.Date.toString(value, true);
                        return value;
                    }
                },
                { field: 'SampleNum', title: '样本数量', width: fixWidth(0.1) },
                { field: 'Status', title: '状态', width: fixWidth(0.08),
                    formatter: function (value, row) {
                        var statusCN = (value == 1) ? '未打印' : '已打印';
                        return statusCN;
                    }, styler: function (value, row) {
                        var color = (value == 2) ? 'black' : 'red';
                        return 'Color:' + color;
                    }
                },
                { field: 'IsConfirm', title: '是否确认', width: fixWidth(0.1),
                    formatter: function (value, row) {
                        var ConfirmCN = (value == 1) ? '已确认' : '未确认';
                        return ConfirmCN;
                    }, styler: function (value, row) {
                        var color = (value == 1) ? 'black' : 'red';
                        return 'Color:' + color;
                    }
                },
                { field: 'Note', title: '备注', hidden: true }
            ]
        ];
        return columns;
    }

    //创建条码数据列
    function barcodeColumns() {
        var columns = [
            [
                { field: 'chk', checkbox: true, hidden: true },
                { field: 'BarCode', title: '中心条码', width: fixWidth(0.2) },
                { field: 'CName', title: '姓名', width: fixWidth(0.2) },
                { field: 'CollectDate', title: '采样时间', width: fixWidth(0.2),
                    formatter: function (value, row) {
                        var value = Shell.util.Date.toString(value, false);
                        return value;
                    }
                },
                { field: 'OperDate', title: '开单时间', width: fixWidth(0.2),
                    formatter: function (value, row) {
                        var value = Shell.util.Date.toString(value, false);
                        return value;
                    }
                }
            ]
        ];
        return columns;
    }

    //创建项目数据列
    function itemColumns() {
        var columns = [
            [
                { field: 'chk', checkbox: true, hidden: true },
                { field: 'ItemNo', title: '项目编号', width: fixWidth(0.1) },
                { field: 'CName', title: '项目名称', width: fixWidth(0.2) },
                { field: 'SampleTypeName', title: '采样类型', width: fixWidth(0.1) },
                { field: 'CheckMethodName', title: '检测方法', width: fixWidth(0.1) },
                { field: 'ItemUnit', title: '项目单位', width: fixWidth(0.2) },
                { field: 'Price', title: '项目价格', width: fixWidth(0.1) }
            ]
        ];
        return columns;
    }

    //设置列宽
    function fixWidth(percent) {
        return document.body.clientWidth * percent;
    }

    //日期转换
    function setDate(date) {
        var day = date.getDate() > 9 ? date.getDate() : '0' + date.getDate(),
            a = date.getMonth(),
            month = (date.getMonth() + 1) > 9 ? date.getMonth() + 1 : '0' + (date.getMonth() + 1);
        return date.getFullYear() + '-' + month + '-' + day;
    }

    //获取cookie值
    function getCookieValue(name) {
        var key = name + '=',
            cookies = document.cookie;

        var startSet = cookies.indexOf(key);
        if (cookies.indexOf(key) > -1) {
            var startSet = cookies.indexOf(key),
                    endSet = cookies.indexOf(';', startSet),
                    keyValue = '';
            if (endSet == -1) {
                keyValue = cookies.substring(startSet);
            } else {
                keyValue = cookies.substring(startSet, endSet);
            }
            if (keyValue.indexOf('LogisticsOfficer') > -1) {
                $('#lbnConfirm').linkbutton('enable');
            }
        }
    }
    searchOrder();
   
   
});

//Lodop页面打印
function winLodopPrint(list,preview){
    var lodop = Shell.util.Print.getLodopObj("报告单打印");
        //printConfigInfo = getPrintConfigInfo(),
        //intOrient = parseInt(printConfigInfo.orientationType),
        //strPageName = printConfigInfo.paperType;

   //lodop.SET_PRINT_PAGESIZE(intOrient,0,0,strPageName);//方向 1:纵;2:横

    for(var i=0;i<list.length;i++){
        lodop.NEWPAGE();
        //lodop.ADD_PRINT_IMAGE(0,0,"100%","100%","<img border='0' src='" + Shell.util.Path.rootPath + "/" + list[i] + "'width='100%'/>");
        //lodop.ADD_PRINT_IMAGE(0,0,"100%","100%","URL:" + Shell.util.Path.rootPath + "/" + list[i]);
       lodop.ADD_PRINT_IMAGE(0, 0, "100%", "100%", '<img src="' + Shell.util.Path.rootPath + '/' + list[i] + '" style="width:100%;border:0;"/>');
        lodop.SET_PRINT_STYLEA(0,"Stretch",2);//按原图比例(不变形)缩放模式
    }

    //预览打印/直接打印
    if(preview){
        lodop.PREVIEWB();
    }else{
        lodop.PRINT();
    }
    var rows = $('#tbOrder').datagrid('getSelections') || [];
     askService('confirmPrint',null,{OrderNo:rows[0]['OrderNo']});
}

//$.ajax()请求服务
function askService(serviceType, entity, where,rowIndex) {
    var localData = {},
        serviceParam = {},//请求服务参数
        async = serviceType == 'delete' ? false : true;//删除操作，同步执行$.ajax方法

    if(serviceType=='search'){
        serviceParam.data=entity;
    }
    serviceParam = setService(serviceType, serviceParam, where);//配置服务参数
    $.ajax({
        url: Shell.util.Path.rootPath + '/ServiceWCF/NRequestFromService.svc/' + serviceParam.serviceName,
        data: serviceParam.data,
        dataType: 'json',
        type: serviceParam.type,
        timeout: 10000,
        async: async,
        contentType: 'application/json',//不加这个会出现错误
        success: function (data) {
            if (data.success) {
                switch (serviceType) {
                    case'delete':
                        $('#tbOrder').datagrid('deleteRow',rowIndex);
                        $('#tbBarcode').datagrid('loadData',{success:true,ResultDataValue:'{total:0,rows:[]}'});
                        $('#tbItems').datagrid('loadData',{success:true,ResultDataValue:'{total:0,rows:[]}'});
                        break;
                    case'confirmPrint':
                        $('#tbOrder').datagrid('load');
                        break;
                    case 'search':$('#tbOrder').datagrid('loadData',{success: true, ResultDataValue: data.ResultDataValue});
                        break;
                    case'confirmOrder':
                        $('#tbOrder').datagrid('updateRow',{
                            index:rowIndex,
                            row:{IsConfirm:1}
                        });
                        break;
                    case'print':
                        var ResultDataValue=eval('('+data.ResultDataValue+')');
                        winLodopPrint(ResultDataValue,true);
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
        case'delete':
            serviceParam.serviceName = 'DelOrder';//删除数据服务名
            serviceParam.type = 'GET';//数据请求方式GET
            serviceParam.data = where;//发送到服务器的数据，根据ID号进行删除
            break;
        case'search':
            serviceParam.serviceName = 'GetOrderList';//获取数据服务名
            serviceParam.type = 'POST';//数据请求方式POST
            break;
        case'edit':
            serviceParam.serviceName = 'UpdateOrderNote';//编辑数据服务名UpdateReportGroupModelByID
            serviceParam.type = 'GET';//数据请求方式GET
            serviceParam.data = where;//发送到服务器的数据，根据ID号进行删除
            break;
        case'confirmOrder':
            serviceParam.serviceName = 'ConFirmOrder';//确认订单服务名ConFirmOrder
            serviceParam.type = 'GET';//数据请求方式GET
            serviceParam.data = where;//发送到服务器的数据，根据ID号进行删除
            break;
        case'print':
            serviceParam.serviceName = 'GetNRequestFromListByOrderNo';//打印数据服务名GetNRequestFromListByOrderNo
            serviceParam.type = 'GET';//数据请求方式GET
            serviceParam.data = where;//发送到服务器的数据，根据ID号进行删除
            break;
        case'confirmPrint':
            serviceParam.serviceName = 'ConFirmPrint';//确认打印数据服务名GetNRequestFromListByOrderNo
            serviceParam.type = 'GET';//数据请求方式GET
            serviceParam.data = where;//发送到服务器的数据，根据ID号进行删除
            break;
    }

    return serviceParam;
}

//获取数据实体（增加，编辑）,形如：{entity:{ID:1,name:'张珊'}}
function getEntity() {
    var result = '',
        orderRow = $('#tbOrder').datagrid('getSelected');

    result += '{"jsonentity":{';//构造符合格式的字符串
    result += '"OrderNo":"' + orderRow.OrderNo + '",';
    result += '"Note":"' + ($('#txtRemarks').val()).trim() + '"';
    result += '}}';
    return result;
}

//查询
function searchOrder() {
    var startDate = $('#startDate').datebox('getValue'),
        endDate = $('#endDate').datebox('getValue'),
        orderStatue = $('#orderStatue').combobox('getValue'),
        orderNO = $('#orderNO').textbox('getValue'),
        orderMan = $('#orderMan').textbox('getValue'),
        Admin = Shell.util.Cookie.getCookie('ZhiFangUserPosition');
       
       // LabCode = Shell.util.Cookie.getCookie("ZhiFangUser"),
        result='';
    
    result += '{"jsonentity":{';
    if (Admin.indexOf('WebLisAdmin') == -1) {
        result += '"LabCode":"' + LabCode + '",';
    }   
    result += '"OrderStartDate":"' + startDate + '",';
    if(orderStatue!='0')
    result += '"Status":' + orderStatue + ',';
    if(orderNO)
    result += '"OrderNo":"' + orderNO + '",';
    if(orderMan)
    result += '"CreateMan":"' + orderMan + '",';
    result += '"OrderEndDate":"' + endDate + '"';
    result += '}}';

    $('#tbItems').datagrid('loadData', {success: true, ResultDataValue: "{total: 0, rows: []}"});
    $('#tbBarcode').datagrid('loadData', {success: true, ResultDataValue: "{total: 0, rows: []}"});
    askService('search',result,null);
}

//新增
function addOrder(){
    var SN = Shell.util.Path.getRequestParams()["SN"];
    //调用main.aspx里面OpenWindowFuc方法
    parent.OpenWindowFuc('新增', Math.floor(window.screen.width * 0.8), Math.floor(window.screen.height * 0.7), '../ui/order/addorder.html', SN);
}

//修改备注
function editRemarks() {
    if (editSwitch) {
        $('#ltnRemark').css('background-color','#ADD8E6');
        document.getElementById("txtRemarks").readOnly=false;
        $('#txtRemarks').height(101.5);
        $('#txtRemarks').css('background-color','');
        $('#divBtn').show();
        editSwitch = false;
    } else {
        $('#ltnRemark').css('background-color','');
        document.getElementById("txtRemarks").readOnly=true;
        $('#txtRemarks').height(134.99);
        $('#txtRemarks').css('background-color','#E8E8E8');
        $('#divBtn').hide();
        editSwitch = true;
    }
}

//打印
function printOrder() {
    var rows = $('#tbOrder').datagrid('getSelections') || [],
        length = rows.length || 0;

    if (length == 0) {
        $.messager.alert('提示信息', '请选择记录', 'info');
    } else {
        askService('print',null,{OrderNo:rows[0]['OrderNo'],page:1,rows:1000});
       // window.open(url, "修改申请单", "width=" + Math.floor(window.screen.width * 0.9) + ",height=" + Math.floor(window.screen.height * 0.8) + ",toolbar=no,menubar=no,scrollbars=yes,resizable=yes,location=no,status=no,top=" + Math.floor(window.screen.height * 0.1) + ",left=" + Math.floor(window.screen.width * 0.05));
    }
}

//删除
function delOrder() {
    var rows = $('#tbOrder').datagrid('getSelections') || [],
        index=$('#tbOrder').datagrid('getRowIndex',rows[0]),
        length = rows.length || 0;

    if (length == 0) {
        $.messager.alert('提示信息', '请选择记录', 'info');
    } else {
        if(rows[0]['Status']==2){
            $.messager.alert('提示信息', '已打印的数据不能删除！', 'info');
            return;
        }
        $.messager.confirm('确认', '确认删除所勾选订单号下的所有数据吗？', function (btn) {
            if (btn) {
                    askService('delete', null, {OrderNo: rows[0]['OrderNo']},index);
            }
        });
    }
}

//确认
function confirm(){
    var rows = $('#tbOrder').datagrid('getSelections') || [],
        index=$('#tbOrder').datagrid('getRowIndex',rows[0]);

    if (length == 0) {
        $.messager.alert('提示信息', '请选择记录', 'info');
    } else {
        askService('confirmOrder',null,{OrderNo:rows[0]['OrderNo']},index);
    }
}

//保存
function saveOrder() {
    var orderRow = $('#tbOrder').datagrid('getSelected');
    askService('edit', null,{OrderNo:orderRow.OrderNo,Note:($('#txtRemarks').val()).trim()});//请求服务器
}
function ContentReLoad() {
    searchOrder();
   // $('#tbOrder').datagrid('reload');
} 