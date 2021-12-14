/**
 * OverView 就诊类型
 * Created by gwh on 14-12-15.
 */

//保存类型
var btnType;
//实验室编号
var LabCode;
//已存在对照关系的记录
var contrastList=[];

//程序入口
$(function(){

    var getParams=Shell.util.Path.getRequestParams();
    LabCode=parseInt(getParams.labCode);
    //初始化就诊类型字典表
    $('#tbSickType').datagrid({
        title:'就诊类型字典',
        url:Shell.util.Path.rootPath+'/ServiceWCF/DictionaryService.svc/GetPubDict',
        queryParams:{
            tablename:"B_Lab_SickType",
            labcode:LabCode
        },
        method:'GET',
        loadMsg:'数据加载...',
        rownumbers:true,
        pagination:true,
        fitColumns:true,
        singleSelect:false,
        striped:true,
        fit:true,
        toolbar:'#topBars',
        columns:createColumns(),
        onBeforeLoad:function(param) {
            if(param.page == 0) return false;
        },
        loadFilter:function(data){
            if(data.success){
                var list=eval('('+data.ResultDataValue+')');
                var result = {rows:[],total:0};
                result.rows = list.rows ? list.rows : result.rows;
				if(list.total)result.total = list.total ? list.total : 0;
                return result;
            }else{
            	return {rows:[],total:0};
            }
        }
    });

    $('#LabSickTypeNo').textbox({onChange:function(newValue,oldValue){
        if(btnType=='add' && newValue.length<=10)
            askService('validate',null,{precisequery:"LabSickTypeNo",filerValue:newValue,labcode:LabCode,tablename:"B_Lab_SickType"});//验证主键的唯一性
    }});
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
            {field: 'LabSickTypeNo', title: '编码', width: fixWidth(0.2) },
            {field: 'CName', title: '中文名称', width: fixWidth(0.2)},
            {field: 'ShortCode', title: '简码', width: fixWidth(0.2)},
            {field:'UseFlag',title:'是否在用',width:fixWidth(0.2),
                formatter:function(value,row){
                    var useCName=row.UseFlag?'已启用':'已禁用';
                    return useCName;
                },
                styler:function(value,row){
                    var color=row.UseFlag?'black':'red';
                    return 'Color:'+color;
                }},
            {field: 'ControlStatus', title: '对照状态', width: fixWidth(0.2)},//数据库中不存在此字段名
            {field: 'opt', title: '操作', align: 'right', width: fixWidth(0.1), align: 'center',
                formatter: function (value, row, index) {
                    var useFlag=row.UseFlag?'禁用':'启用';
                    var edit = '<a href="javascript:void(0)"  data-options="iconCls:icon-edit" style="margin-right: 10px" onclick="editRow(' + index + ')">修改</a>';
                    var disable = '<a href="javascript:void(0)"  data-options="iconCls:icon-edit" onclick="disableRecord(' + index + ')">'+useFlag+'</a>';

                    return edit+disable;
                }}
        ]
    ];
    return columns;
}

//设置下拉列表框的默认值
function setCombobox(row) {

    //是否使用下拉列表框
    $('#UseFlag').combobox({
        valueField: 'UseFlag',
        textField: 'text',
        editable:false,
        panelHeight:50,
        data: [
            { Visible: 0, text: '否' },
            { Visible: 1, text: '是' }
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
function askService(serviceType,entity, where) {
    var localData = {},
        serviceParam = {},//请求服务参数
        async=serviceType=='delete'?false:true;//删除操作，同步执行$.ajax方法

    if(serviceType=='add' || serviceType=='edit'){
        serviceParam.data=entity;
    }
    serviceParam = setService(serviceType, serviceParam, where);//配置服务参数
    $.ajax({
        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/' + serviceParam.serviceName,
        data: serviceParam.data,
        dataType: 'json',
        type: serviceParam.type,
        timeout: 10000,
        async:async,
        contentType: 'application/json',//不加这个会出现错误
        success: function (data) {
            if (data.success) {
                switch (serviceType) {
                    case'add':
                    case'edit':
                    case'delete':
                        $('#tbSickType').datagrid('load',{tablename:"B_Lab_SickType",filerValue:null,labcode:LabCode,time:new Date().getTime()});
                        $('#txtSearch').searchbox('setValue',null);//增删改成功后，重新加载数据请求
                        break;

                    case'search':
                        $('#tbSickType').datagrid('loadData',data);
                        break;
                    case 'validate':
                        var data=eval('('+data.ResultDataValue+')'),
                            total=data.total||0;
                        if(total)
                            $.messager.alert('提示','数据库已存在此编号！不能重复插入','warning');
                        break;
                }
            }else{
                var ErrorInfo=data.ErrorInfo;
                (ErrorInfo.indexOf('此项已对照')>-1)?contrastList.push(where.LabSickTypeNo):null;
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
            serviceParam.serviceName = 'DeleteLabSickTypeModelByID';//删除数据服务名
            serviceParam.type = 'GET';//数据请求方式GET
            serviceParam.data =  where;//发送到服务器的数据，根据ID号进行删除
            break;
        case'add':
            serviceParam.serviceName = 'AddLabSickTypeModel';//增加数据服务名
            serviceParam.type = 'POST';//数据请求方式GET
            break;
        case'edit':
            serviceParam.serviceName = 'UpdateLabSickTypeModelByID';//编辑数据服务名UpdateReportGroupModelByID
            serviceParam.type = 'POST';//数据请求方式GET
            break;
        case'validate':
            serviceParam.serviceName = 'GetPubDict';//修改组套关系服务名UpdateGroupItemModelByID
            serviceParam.type = 'GET';//数据请求方式GET
            serviceParam.data =  where;//发送到服务器的数据，根据ID号进行删除
            break;
    }

    return serviceParam;
}

//获取数据实体（增加，编辑）,形如：{entity:{ID:1,name:'张珊'}}
function getEntity(getType) {
    var result = '';
    result += '{"jsonentity":{';//构造符合格式的字符串

    var LabSickTypeNo=$('#LabSickTypeNo').numberbox('getValue');
    if(!LabSickTypeNo)
        return 1;
    result+='"LabSickTypeNo":'+LabSickTypeNo+',';

    var LabCode=$('#LabCode').textbox('getValue');
    if(!LabCode)
        return 1;
    result+='"LabCode":'+LabCode+',';

    var CName=$('#CName').textbox('getValue');
    if(!CName)
        return 1;
    result+='"CName":"'+CName+'",';

    var ShortCode=$('#ShortCode').textbox('getValue');
    if(!ShortCode)
        return 1;
        result+='"ShortCode":"'+ShortCode+'",';

    var DispOrder=$('#DispOrder').textbox('getValue');
    if(DispOrder)
        result+='"DispOrder":"'+DispOrder+'",';

    var StandCode=$('#StandCode').textbox('getValue');
    if(StandCode)
        result+='"StandCode":"'+StandCode+'",';

    var ZFStandCode=$('#ZFStandCode').textbox('getValue');
    if(ZFStandCode)
        result+='"ZFStandCode":"'+ZFStandCode+'",';

    var HisOrderCode=$('#HisOrderCode').textbox('getValue');
    if(HisOrderCode)
        result+='"HisOrderCode":"'+HisOrderCode+'",';

    var UseFlag=$('#UseFlag').combobox('getValue');
    result+='"UseFlag":'+UseFlag;

    result += '}}';
    return result;
}

//刷新
function refresh(){
    $('#tbSickType').datagrid('load',{tablename:"B_Lab_SickType",filerValue:null,labcode:LabCode,time:new Date().getTime()});
    $('#txtSearch').searchbox('setValue',null);
}

//新增
function addRow(){
    btnType='add';
    $('#dlg').dialog({modal:true});
    $('#dlg').dialog('open').dialog('setTitle','新增');
    $('#dlg').window('center');
    $('#frm').form('clear');
    $('#LabCode').textbox('setValue',LabCode);
    $('#LabSickTypeNo').textbox('enable');
    setCombobox([]);//设置下拉框
}

//修改
function editRow(index){
    var curData = $('#tbSickType').datagrid('getData'),//返回当前页加载完毕的数据
        curRow = curData.rows[index];
    if(curRow){
        btnType='edit';
        $('#dlg').dialog({modal:true});
        $('#dlg').dialog('open').dialog('setTitle','修改');
        $('#dlg').window('center');
        $('#frm').form('clear');
        $('#frm').form('load', curRow);//form表加载数据
        $('#LabSickTypeNo').textbox('disable');
        setCombobox(curRow);//设置下拉框
    }
}

//删除
function deleteRow(){
    var rows = $('#tbSickType').datagrid('getSelections') ||[],
        length=rows.length || 0;

    if (length == 0) {
        $.messager.alert('提示信息', '请选择记录', 'info');
    } else {
        $.messager.confirm('确认', '确认删除吗？', function (btn) {
            if (btn) {
                for (var i = 0; i < length; i++) {

                    askService('delete', null,{LabSickTypeNo:rows[i]['LabSickTypeNo'],labcode:LabCode});
                }
                $('#tbSickType').datagrid('clearSelections');//因为getSelections会记忆选过的记录，所以要清空一下
                if(contrastList.length){
                    var records=contrastList[0];
                    for(var i=1;i<contrastList.length;i++){
                        records+=','+contrastList[i];
                        if((i+1)%5===0){
                            records+='\n';
                        }
                    }
                    $.messager.alert('提示',records+'已存在对照关系,不能删除！','info');
                    contrastList=[];
                }
            }
        });
    }
}

//批量禁用
function disableRecords(){
    var rows = $('#tbSickType').datagrid('getSelections'),
        index;

    if (rows.length == 0) {
        $.messager.alert('提示信息', '请选择记录', 'info');
    } else {
        $.messager.confirm('确认', '确认禁用所选记录吗？', function (btn) {
            if (btn) {
                for (var i = 0; i < rows.length; i++) {
                    if(rows[i]['UseFlag']){
                        index=$('#tbSickType').datagrid('getRowIndex',rows[i]);
                        disableOrEnable(index,0);
                    }
                }
                $('#tbSickType').datagrid('clearSelections');//因为getSelections会记忆选过的记录，所以要清空一下
            }
        });
    }
}

//批量启用
function enableRecords(){
    var rows = $('#tbSickType').datagrid('getSelections'),
        index;

    if (rows.length == 0) {
        $.messager.alert('提示信息', '请选择记录', 'info');
    } else {
        $.messager.confirm('确认', '确认启用所选记录吗？', function (btn) {
            if (btn) {
                for (var i = 0; i < rows.length; i++) {
                    if(!rows[i]['UseFlag']){
                        index=$('#tbSickType').datagrid('getRowIndex',rows[i]);
                        disableOrEnable(index,1);
                    }
                }
                $('#tbSickType').datagrid('clearSelections');//因为getSelections会记忆选过的记录，所以要清空一下
            }
        });
    }
}

//导出Excel表
function exportExcel(){
    $('#ifmExport').attr('src',Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/DictionaryExportExcel?table=B_Lab_SickType&labcode='+LabCode);
}

//上传文件
function uploadFile(){
    $('#fileboxID').filebox({
        buttonText:'选择文件',
        buttonAlign:'right'
    });

    $('#dlgUpload').dialog({modal:true});
    $('#dlgUpload').dialog('open').dialog('setTitle','上传文件(.xls,.xlsx,.xml)');
    $('#dlgUpload').window('center');
}

//单个禁用/启用
function disableRecord(index){
    var curData = $('#tbSickType').datagrid('getData'),//返回当前页加载完毕的数据
        curRow = curData.rows[index],
        useFlag=curRow.UseFlag?0: 1,
        newText=useFlag?'启用':'禁用';

    $.messager.confirm('确认', '确认'+newText+'吗？', function (btn) {
        if (btn) {
            disableOrEnable(index,useFlag);
        }
    });

}

//禁用/启用方法
function disableOrEnable(index,UseFlag){
    var curData = $('#tbSickType').datagrid('getData'),//返回当前页加载完毕的数据
        curRow = curData.rows[index],
        useFlag=UseFlag,
        result = '';

    result += '{"jsonentity":{';//构造符合格式的字符串
    result+='"LabSickTypeNo":'+curRow.LabSickTypeNo+',';
    result+='"LabCode":'+LabCode+',';
    if(!curRow.CName && curRow.CName!==0){}else{
        result+='"CName":"'+curRow.CName+'",';
    }

    if(!curRow.ShortCode && curRow.ShortCode!==0){}else{
        result+='"ShortCode":"'+curRow.ShortCode+'",';
    }

    if(!curRow.DispOrder && curRow.DispOrder!==0){}else{
        result+='"DispOrder":"'+curRow.DispOrder+'",';
    }

    if(!curRow.StandCode && curRow.StandCode!==0){}else{
        result+='"StandCode":"'+curRow.StandCode+'",';
    }

    if(!curRow.ZFStandCode && curRow.ZFStandCode!==0){}else{
        result+='"ZFStandCode":"'+curRow.ZFStandCode+'",';
    }

    if(!curRow.HisOrderCode && curRow.HisOrderCode!==0){}else{
        result+='"HisOrderCode":"'+curRow.HisOrderCode+'",';
    }

    result+='"UseFlag":'+useFlag;

    result += '}}';

    askService('edit',result,null);
}

//查询
function search(value){
    $('#tbSickType').datagrid('load',{tablename:"B_Lab_SickType",labcode:LabCode,filerValue:value,time:new Date().getTime()});
}

//保存
function save(){
    var entity;
    entity= getEntity(btnType);
    if(entity>0){
        $.messager.alert('警告','请检查输入值的完整性','warning');
        return;
    }
    askService(btnType, entity);//请求服务器
    $('#dlg').dialog('close');
}

//取消
function cancel(){
    $('#dlg').dialog('close');
}

//上传保存
function uploadSave(){
    var fileName=$('#fileboxID').filebox('getValue'),
        fileNames=fileName.split('.'),
        length=fileNames.length || 0,
        fileFormat=fileNames[length-1];
    if(fileFormat.toLocaleUpperCase()!=='XLS' && fileFormat.toLocaleUpperCase()!=='XLSX' && fileFormat.toLocaleUpperCase()!=='XML'){
        $.messager.alert('提示','文件格式不符合（.xls,.xlsx,.xml）','info');
        return;
    }

    var entity = '';
    entity += '{';//构造符合格式的字符串
    entity+='"labcode":'+LabCode+',';
    entity+='"table":"'+"B_Lab_SickType"+'",';
    entity+='"fileboxID":"'+"fileboxID"+'"';
    entity += '}';

    $('#frmUpload').form('submit',{
        url:Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/DictionaryUploadFile',
        onSubmit:function(param){
            param.jsonentity=entity;
            return true;
        },
        success:function(data){
            if(data.success){
                $('#tbSickType').datagrid('load',{tablename:"B_Lab_SickType",filerValue:null,labcode:LabCode,time:new Date().getTime()});
                $('#dlgUpload').dialog('close');
            }
        }
    });

}

//上传取消
function uploadCancel(){
    $('#dlgUpload').dialog('close');
}