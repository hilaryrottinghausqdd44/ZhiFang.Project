/**
 * OverView 性别
 * Created by gwh on 14-12-15.
 */

//保存类型
var btnType;
//实验室编号
var LabCode;

//程序入口
$(function(){

    //初始化实验室
    initLab();

    //初始化性别字典表
    $('#tbAgeUnit').datagrid({
        title:'性别字典',
        //url:'LabAgeUnit.txt',
        method:'GET',
        loadMsg:'数据加载...',
        pagination:true,
        fitColumns:true,
        singleSelect:false,
        striped:true,
        fit:true,
        toolbar:'#topBars',
        columns:createColumns(),
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

    $('#AgeUnitNo').textbox({onChange:function(newValue,oldValue){
        if(btnType=='add')
            askService('validate',null,{fields:"AgeUnitNo",filerValue:newValue.trim(),labcode:LabCode,tablename:"AgeUnit"});//验证主键的唯一性
    }});
});

//设置列宽
function fixWidth(percent) {
    return document.body.clientWidth * percent;
}

//初始化实验室
function initLab(){
    $('#cmbLab').combobox({
        url:Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetPubDict?tablename=CLIENTELE&fields=ClIENTNO,CNAME',
        method:'GET',
        valueField: 'ClIENTNO',
        textField: 'CNAME',
        loadFilter:function(data){
            data=eval('('+data.ResultDataValue+')').rows || [];//eval()把字符串转换成JSON格式
            return data;
        },
        onLoadSuccess: function () {
            var data = $(this).combobox('getData');
            if (data.length > 0) {
                $(this).combobox('select', data[0].ClIENTNO);//默认第一项的值
            }
        },
        filter:function(q,row){
            var opts = $(this).combobox('options');
            return row[opts.textField].indexOf(q) > -1;//返回true,则显示出来
        },
        onSelect:function(record){
            $('#txtSearch').searchbox('setValue',null);
            LabCode=record.ClIENTNO;
            $('#tbAgeUnit').datagrid('options').url=Shell.util.Path.rootPath+'/ServiceWCF/DictionaryService.svc/GetPubDict';
            $('#tbAgeUnit').datagrid('load',{
                tablename:"AgeUnit",
                labcode:record.ClIENTNO
            });
        }
    });
}

//创建数据列
function createColumns() {
    var columns = [
        [
            {field: 'chk', checkbox: true, hidden: false},
            {field: 'AgeUnitNo', title: '编码', width: fixWidth(0.2) },
            {field: 'CName', title: '中文名称', width: fixWidth(0.2)},
            {field: 'ShortCode', title: '简码', width: fixWidth(0.2)},
            {field:'UseFlag',title:'是否在用',width:fixWidth(0.2),
                formatter:function(value,row){
                    var useCName=row.UseFlag?'已启用':'已禁用';
                    return useCName;
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

    //是否显示下拉列表框
    $('#Visible').combobox({
        valueField: 'Visible',
        textField: 'text',
        editable:false,
        panelHeight:50,
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

    //是否使用下拉列表框
    $('#UseFlag').combobox({
        valueField: 'UseFlag',
        textField: 'text',
        editable:false,
        panelHeight:50,
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
                        $('#tbAgeUnit').datagrid('load');//增删改成功后，重新加载数据请求
                        break;
                    case'delete':
                        delCount++;
                        $('#tbAgeUnit').datagrid('load');//增删改成功后，重新加载数据请求
                        break;
                    case'search':
                        $('#tbAgeUnit').datagrid('loadData',data);
                        break;
                    case 'validate':
                        var data=eval('('+data.ResultDataValue+')'),
                            total=data.total||0;
                        if(total)
                            $.messager.alert('提示','数据库已存在此编号！不能重复插入','warning');
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
            serviceParam.serviceName = 'GetReportGroupModelByID';//查询数据服务名
            serviceParam.type = 'GET';//数据请求方式GET
            serviceParam.data = {Id: where};//发送到服务器的数据,根据ID号查询（这里基本没用，没人记得ID号）
            break;
        case'delete':
            serviceParam.serviceName = 'DelReportGroupModel';//删除数据服务名
            serviceParam.type = 'GET';//数据请求方式GET
            serviceParam.data = {Id: where};//发送到服务器的数据，根据ID号进行删除
            break;
        case'add':
            serviceParam.serviceName = 'AddReportGroupModel';//增加数据服务名
            serviceParam.type = 'POST';//数据请求方式GET
            break;
        case'edit':
            serviceParam.serviceName = 'UpdateReportGroupModelByID';//编辑数据服务名UpdateReportGroupModelByID
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

    var AgeUnitNo=$('#AgeUnitNo').val().trim();
    if(!AgeUnitNo)
        return 1;
    result+='"AgeUnitNo":'+AgeUnitNo+',';

    var LabCode=$('#LabCode').val().trim();
    if(!LabCode)
        return 1;
    result+='"LabCode":'+LabCode+',';

    var CName=$('#CName').val().trim();
    if(CName)
        result+='"CName":"'+CName+'",';

    var Visible=$('#Visible').combobox('getValue');
    if(Visible)
        result+='"Visible":'+Visible+',';

    var ShortCode=$('#ShortCode').val().trim();
    if(ShortCode)
        result+='"ShortCode":"'+ShortCode+'",';

    var DispOrder=$('#DispOrder').val().trim();
    if(DispOrder)
        result+='"DispOrder":"'+DispOrder+'",';

    var StandCode=$('#StandCode').val().trim();
    if(StandCode)
        result+='"StandCode":"'+StandCode+'",';

    var ZFStandCode=$('#ZFStandCode').val().trim();
    if(ZFStandCode)
        result+='"ZFStandCode":"'+ZFStandCode+'",';

    var HisOrderCode=$('#HisOrderCode').val().trim();
    if(HisOrderCode)
        result+='"HisOrderCode":"'+HisOrderCode+'",';

    var Code_1=$('#Code_1').val().trim();
    if(Code_1)
        result+='"Code_1":"'+Code_1+'",';

    var Code_2=$('#Code_2').val().trim();
    if(Code_2)
        result+='"Code_2":"'+Code_2+'",';

    var Code_3=$('#Code_3').val().trim();
    if(Code_3)
        result+='"Code_3":"'+Code_3+'",';

    var UseFlag=$('#UseFlag').combobox('getValue');
    result+='"UseFlag":'+UseFlag;

    result += '}}';
    return result;
}

//刷新
function refresh(){
    $('#tbAgeUnit').datagrid('load',{tablename:"AgeUnit",filerValue:null});
    $('#txtSearch').searchbox('setValue',null);
}

//新增
function addRow(){
    btnType='add';
    $('#dlg').dialog('open').dialog('setTitle','新增');
    $('#dlg').window('center');
    $('#frm').form('clear');
    $('#LabCode').textbox('setValue',LabCode);
    $('#AgeUnitNo').textbox('enable');
    setCombobox([]);//设置下拉框
}

//修改
function editRow(index){
    var curData = $('#tbAgeUnit').datagrid('getData'),//返回当前页加载完毕的数据
        curRow = curData.rows[index];
    if(curRow){
        btnType='edit';
        $('#dlg').dialog('open').dialog('setTitle','修改');
        $('#dlg').window('center');
        $('#frm').form('clear');
        $('#frm').form('load', curRow);//form表加载数据
        $('#AgeUnitNo').textbox('disable');
        setCombobox(curRow);//设置下拉框
    }
}

//批量禁用
function disableRecords(){
    var rows = $('#tbAgeUnit').datagrid('getSelections'),
        index;

    if (rows.length == 0) {
        $.messager.alert('提示信息', '请选择记录', 'info');
    } else {
        $.messager.confirm('确认', '确认禁用所选记录吗？', function (btn) {
            if (btn) {
                for (var i = 0; i < rows.length; i++) {
                    if(rows[i]['UseFlag']){
                        index=$('#tbTestItem').datagrid('getRowIndex',rows[i]);
                        disableOrEnable(index,0);
                    }
                }
                $('#tbTestItem').datagrid('clearSelections');//因为getSelections会记忆选过的记录，所以要清空一下
            }
        });
    }
}

//批量启用
function enableRecords(){
    var rows = $('#tbAgeUnit').datagrid('getSelections'),
        index;

    if (rows.length == 0) {
        $.messager.alert('提示信息', '请选择记录', 'info');
    } else {
        $.messager.confirm('确认', '确认启用所选记录吗？', function (btn) {
            if (btn) {
                for (var i = 0; i < rows.length; i++) {
                    if(!rows[i]['UseFlag']){
                        index=$('#tbTestItem').datagrid('getRowIndex',rows[i]);
                        disableOrEnable(index,1);
                    }
                }
                $('#tbTestItem').datagrid('clearSelections');//因为getSelections会记忆选过的记录，所以要清空一下
            }
        });
    }
}

//单个禁用/启用
function disableRecord(index){
    var curData = $('#tbAgeUnit').datagrid('getData'),//返回当前页加载完毕的数据
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
    var curData = $('#tbAgeUnit').datagrid('getData'),//返回当前页加载完毕的数据
        curRow = curData.rows[index],
        useFlag=UseFlag,
        result = '';

    result += '{"jsonentity":{';//构造符合格式的字符串
    result+='"AgeUnitNo":'+curRow.AgeUnitNo+',';
    result+='"LabCode":'+LabCode+',';
    result+='"CName":"'+curRow.CName+'",';
    result+='"Visible":'+curRow.Visible+',';
    result+='"ShortCode":"'+curRow.ShortCode+'",';
    result+='"DispOrder":"'+curRow.DispOrder+'",';
    result+='"StandCode":"'+curRow.StandCode+'",';
    result+='"ZFStandCode":"'+curRow.ZFStandCode+'",';
    result+='"HisOrderCode":"'+curRow.HisOrderCode+'",';
    result+='"Code_1":"'+curRow.Code_1+'",';
    result+='"Code_2":"'+curRow.Code_2+'",';
    result+='"Code_3":"'+curRow.Code_3+'",';
    result+='"UseFlag":'+useFlag;

    askService('edit',result,null);
}

//查询
function search(value){
    $('#tbAgeUnit').datagrid('load',{tablename:"AgeUnit",labcode:LabCode,filerValue:value});
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