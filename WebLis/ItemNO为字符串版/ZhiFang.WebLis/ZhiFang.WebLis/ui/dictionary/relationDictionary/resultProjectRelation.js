/**
 *@OverView 结果项目字典表对照关系表
 * Created by gwh on 15-9-6.
 */

//实验室列表行索引
var rightIndex;
//中心列表行索引
var leftIndex;
//实验室编号
var LabCode;
//中心字典对照状态
var centerContrast=0;
//实验室字典对照状态
var labContrast;
//中心项目编码
var centerItemNo;
//实验室对照列表
var labRows;
//中心项目名称
var centerName;
//实验室项目编码
var labItemNo;
//实验室项目名称
var labCName;
//实验室项目简码
var labShortCode;
//首次触发单选按钮
var onClick=0;
//中心列表
var centerRows;
//匹配行背景颜色
var matchRow;

//程序入口
$(function () {
    $('#labSearch').searchbox('setValue', null);

    var getParams=Shell.util.Path.getRequestParams();
    LabCode=parseInt(getParams.labCode);
    initRadios();
    getAllData();

    //初始化实验室项目字典表对照关系表
    $('#leftItem').datagrid({
        title: '项目字典表对照关系表',
        url:Shell.util.Path.rootPath+'/ServiceWCF/DictionaryService.svc/GetPubDict',
        queryParams:{
            tablename:"B_Lab_ResultTestItemControl",
            selectedflag:labContrast,
            labcode:LabCode
        },
        method: 'GET',
        loadMsg: '数据加载...',
        //pagination:true,
        fitColumns: true,
        singleSelect: true,
        striped: true,
        fit: true,
        border: false,
        toolbar: '#leftBars',
        checkOnSelect:false,
        selectOnCheck:false,
        columns: createColumns(),
        onBeforeLoad:function(param) {
            if(param.page == 0) return false;
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
        onClickRow:function(index,row){
            var rightRows=$('#rightItem').datagrid('getRows'),
                length=rightRows.length;

            if(matchRow){
                matchRow.css("background-color",'');
            }
            for(var i=0;i<length;i++){
                if(row.CenterItemNo==rightRows[i].ItemNo){
                    $('#datagrid-row-r2-2-'+i).css("background-color","#4afffd");
                    matchRow= $('#datagrid-row-r2-2-'+i);
                    break;
                }
            }
        }
    });

    //初始化中心项目字典列表
    $('#rightItem').datagrid({
        title: '中心项目列表',

        url:Shell.util.Path.rootPath+'/ServiceWCF/DictionaryService.svc/GetPubDict',
        queryParams:{
            tablename:"TestItem",
            fields:'ItemNo,ShortCode,CName'
        },
        method: 'GET',
        loadMsg: '数据加载...',
        fitColumns: true,
        singleSelect: true,
        striped: true,
        fit: true,
        border: false,
        toolbar: '#rightBars',
        columns: matchColumns(),

        onBeforeLoad:function(param) {
            if(param.page == 0) return false;
        },
        loadFilter: function (data) {
            if (data.success) {
                var list = eval('(' + data.ResultDataValue + ')') || {},
                    result = {};

                result.total = list.total || 0;
                result.rows = list.rows || [];
                centerRows=(!centerRows)?list.rows || []:centerRows;
                return result;
            }
        }
    });

    //初始化智能对照成功的列表
    $('#dlgItem').datagrid({
        title:'智能对照成功列表',
        method:'GET',
        loadMsg:'数据加载...',
        fitColumns:true,
        singleSelect:true,
        striped:true,
        fit:true,
        columns:dlgColumns()
    });

});

//设置列宽
function fixWidth(percent) {
    return document.body.clientWidth * percent;
}

//创建实验室数据列
function createColumns() {
    var columns = [
        [
            {field: 'chk', checkbox: true, hidden: true},
            {field: 'LabItemNo', title: '实验室项目编号', width: fixWidth(0.2) },
            {field: 'CName', title: '实验室项目名称', width: fixWidth(0.3)},
            {field: 'ShortCode', title: '实验室项目简码', width: fixWidth(0.2)},
            {field: 'CenterItemNo', title: '中心项目编码', width: fixWidth(0.2)},
            {field: 'CenterCName', title: '中心项目名称', width: fixWidth(0.3)}
        ]
    ];
    return columns;
}

//创建中心数据列
function matchColumns() {
    var columns = [
        [
            {field: 'ItemNo', title: '中心项目编号', width: fixWidth(0.2)},
            {field: 'ShortCode', title: '中心项目简码', width: fixWidth(0.2)},
            {field: 'CName', title: '中心项目名称', width: fixWidth(0.3)}
        ]
    ];
    return columns;
}

//创建对话框数据列
function dlgColumns() {
    var columns = [
        [
            {field: 'LabItemNo', title: '实验室项目编号', width: fixWidth(0.2) },
            {field: 'CName', title: '实验室项目名称', width: fixWidth(0.2)},
            {field: 'ShortCode', title: '实验室项目简码', width: fixWidth(0.2)},
            {field: 'CenterItemNo', title: '中心项目编码', width: fixWidth(0.2)},
            {field: 'CenterCName', title: '中心项目名称', width: fixWidth(0.2)}
        ]
    ];
    return columns;
}

//初始化单选按钮
function initRadios(){
    $('input:radio[name="haveContrast"]').get(0).checked=true;
    haveContrast();
}

//获取全部的数据
function getAllData(){
    //获取左边全部数据
    askService('leftList',{
        tablename:"B_Lab_ResultTestItemControl",
        selectedflag:0,
        labcode:LabCode
    });
}

//$.ajax()请求服务
function askService(serviceType, entity) {
    var localData = {},
        serviceParam = {};//请求服务参数

    serviceParam = setService(serviceType, serviceParam, entity);//配置服务参数
    $.ajax({
        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/' + serviceParam.serviceName,
        data: serviceParam.data,
        dataType: 'json',
        type: serviceParam.type,
        timeout: 10000,
        async: false,
        contentType: 'application/json',//不加这个会出现错误
        success: function (data) {
            if (data.success) {
                switch (serviceType){
                    case'add':
                        $('#leftItem').datagrid('selectRow', leftIndex);
                        $('#rightItem').datagrid('selectRow', rightIndex);

                        $('#leftItem').datagrid('insertRow', {
                            index: leftIndex+1,
                            row: {LabItemNo:labItemNo,CName:labCName,ShortCode:labShortCode,CenterItemNo: centerItemNo,CenterCName: centerName}
                        });
                        break;
                    case'edit':
                        $('#leftItem').datagrid('selectRow', leftIndex);
                        $('#rightItem').datagrid('selectRow', rightIndex);

                        $('#leftItem').datagrid('updateRow', {
                            index: leftIndex,
                            row: {CenterItemNo: centerItemNo,CenterCName: centerName}
                        });
                        break;
                    case'delete':
                        $('#leftItem').datagrid('updateRow', {
                            index: leftIndex,
                            row: {CenterItemNo: null,CenterCName: null}
                        });
                        break;
                    case'leftList':
                        var list = eval('(' + data.ResultDataValue + ')') || {};
                        labRows = list.rows || [];
                        break;
                }
            }
            else {
                if(data.ErrorInfo.indexOf("此中心项目已存在对照关系")>-1){
                    $.messager.alert("提示信息",data.ErrorInfo,'info');
                }
            }
        },
        error: function (data) {
            $.messager.alert('提示信息', data.ErrorInfo, 'error');
        }
    });

}

//配置服务参数
function setService(serviceType, serviceParam, entity) {

    //数据请求方式（GET,POST）
    switch (serviceType) {
        case'add':
            serviceParam.serviceName = 'UpdateOrAddResultTestItemControlModelByID';//增加数据服务名
            serviceParam.type = 'POST';//数据请求方式GET
            serviceParam.data = entity;//向服务器传递的数据
            break;
        case'edit':
            serviceParam.serviceName = 'UpdateOrAddResultTestItemControlModelByID';//编辑数据服务名
            serviceParam.type = 'POST';//数据请求方式POST
            serviceParam.data = entity;//向服务器传递的数据
            break;
        case'delete':
            serviceParam.serviceName = 'DelResultTestItemControlModelByID';//编辑数据服务名
            serviceParam.type = 'GET';//数据请求方式GET
            serviceParam.data = entity;//向服务器传递的数据
            break;
        case'leftList':
            serviceParam.serviceName = 'GetPubDict';//获取数据服务名
            serviceParam.type = 'GET';//数据请求方式GET
            serviceParam.data = entity;//向服务器传递的数据
            break;
    }

    return serviceParam;
}

//对照状态:全部
function allContrast(){
    var boolAll=$('input:radio[name="allContrast"]').is(":checked"),
        valCheck=$('input:radio[name="allContrast"]:checked').val();

    $('#labSearch').searchbox('setValue', null);
    $('input:radio[name="haveContrast"]').get(0).checked=false;
    $('input:radio[name="noContrast"]').get(0).checked=false;
    if(boolAll){
        labContrast=valCheck;
        $('#leftItem').datagrid('load',{
                tablename:"B_Lab_ResultTestItemControl",
                selectedflag:labContrast,
                labcode:LabCode,
                time:new Date().getTime()}
        );
    }

    if(matchRow){
        matchRow.css("background-color",'');
    }
    var rows=$('#rightItem').datagrid('getSelections') || [];
    if(rows.length)
        $('#rightItem').datagrid('unselectAll');
}

//对照状态:已对照
function haveContrast(){
    var boolHave=$('input:radio[name="haveContrast"]').is(":checked"),
        valCheck=$('input:radio[name="haveContrast"]:checked').val();

    $('#labSearch').searchbox('setValue', null);
    labContrast=valCheck;
    if(onClick){
        $('input:radio[name="allContrast"]').get(0).checked=false;
        $('input:radio[name="noContrast"]').get(0).checked=false;
        if(boolHave){
            $('#leftItem').datagrid('load',{
                    tablename:"B_Lab_ResultTestItemControl",
                    selectedflag:labContrast,
                    labcode:LabCode,
                    time:new Date().getTime()}
            );
        }
        var rows=$('#rightItem').datagrid('getSelections') || [];
        if(rows.length)
            $('#rightItem').datagrid('unselectAll');
    }
    onClick++;

    if(matchRow){
        matchRow.css("background-color",'');
    }
}

//对照状态:未对照
function noContrast(){
    var boolNo=$('input:radio[name="noContrast"]').is(":checked"),
        valCheck=$('input:radio[name="noContrast"]:checked').val();

    $('#labSearch').searchbox('setValue', null);
    $('input:radio[name="allContrast"]').get(0).checked=false;
    $('input:radio[name="haveContrast"]').get(0).checked=false;
    if(boolNo){
        labContrast=valCheck;
        $('#leftItem').datagrid('load',{
                tablename:"B_Lab_ResultTestItemControl",
                selectedflag:labContrast,
                labcode:LabCode,
                time:new Date().getTime()}
        );
    }

    if(matchRow){
        matchRow.css("background-color",'');
    }
    var rows=$('#rightItem').datagrid('getSelections') || [];
    if(rows.length)
        $('#rightItem').datagrid('unselectAll');
}

//实验室字典查询
function labSearch(value) {
    $('#leftItem').datagrid('load', {filervalue: value.trim(),labcode:LabCode,tablename:"B_Lab_ResultTestItemControl",selectedflag:labContrast,time:new Date().getTime()});
    if(matchRow){
        matchRow.css("background-color",'');
    }
}

//中心字典查询
function centerSearch(value) {
    if(!value){
        $('#rightItem').datagrid('loadData',{success: true, ResultDataValue: Shell.util.JSON.encode({total: centerRows.length, rows: centerRows})});
    }else{
        var filterRows=[],
            length=centerRows.length;

        value=value.trim();
        for(var i=0;i<length;i++){
            if((centerRows[i]['ItemNo']).toString().indexOf(value)>-1 || (centerRows[i]['CName'] && (centerRows[i]['CName']).toString().indexOf(value)>-1) || (centerRows[i]['ShortCode'] && (centerRows[i]['ShortCode']).toString().indexOf(value)>-1)){
                filterRows.push(centerRows[i]);
            }
        }
        $('#rightItem').datagrid('loadData',{success: true, ResultDataValue: Shell.util.JSON.encode({total: filterRows.length, rows: filterRows})});
    }
}

//对照
function matchSingle() {
    var rightRow = $('#rightItem').datagrid('getSelected') || "",
        rightRows = $('#rightItem').datagrid('getRows') || [],
        rightLength=rightRows.length,
        leftRow = $('#leftItem').datagrid('getSelected') || "",
        leftRows=$('#leftItem').datagrid('getRows') || [],
        leftLength=leftRows.length,
        leftExist=false,
        rightExit=false;

    if(!rightRow || !leftRow){
        $.messager.alert('提示','请选择要进行对照的记录','info');
        return;
    }

    //当前中心列表数据
    for(var i=0;i<rightLength;i++){
        if(leftRow.CenterItemNo==rightRows[i].ItemNo){

            leftIndex= $('#leftItem').datagrid('getRowIndex', leftRow);
            centerItemNo=rightRow.ItemNo;
            centerName=rightRow.CName;
            rightIndex = $('#rightItem').datagrid('getRowIndex', rightRow);
            labItemNo=leftRow.LabItemNo;
            labCName=leftRow.CName;
            labShortCode=leftRow.ShortCode;

            //可能还需要传所有的字段
            var entity = '{"jsonentity":{' + '"ItemNo":' + centerItemNo + ',"ControlLabNo":' + LabCode + ',"ControlItemNo":' +labItemNo + '}}';
            askService('add', entity);//请求服务器

            //动态改变原始表数据
            for(var x=0;x<labRows.length;x++){
                if(labItemNo===labRows[x]['LabItemNo']){
                    labRows.splice(x+1,0,labRows[x]);
                    labRows[x+1]['CenterCName']=centerName;
                    labRows[x+1]['CenterItemNo']=centerItemNo;
                    break;
                }
            }

            rightExit=true;
            break;
        }
    }


    //遍历中心列表所有数据
    if(!rightExit && rightLength<centerRows.length){
        for(var i=0;i<centerRows.length;i++){
            if(leftRow.CenterItemNo==centerRows[i].ItemNo){

                centerItemNo=rightRow.ItemNo;
                centerName=rightRow.CName;
                rightIndex = 0;
                labItemNo=leftRow.LabItemNo;
                labCName=leftRow.CName;
                labShortCode=leftRow.ShortCode;
                //可能还需要传所有的字段
                var entity = '{"jsonentity":{' + '"ItemNo":' + centerItemNo + ',"ControlLabNo":' + LabCode + ',"ControlItemNo":' +labItemNo + '}}';
                askService('add', entity);//请求服务器

                //动态改变原始表数据
                for(var x=0;x<labRows.length;x++){
                    if(labItemNo===labRows[x]['LabItemNo']){
                        labRows.splice(x+1,0,labRows[x]);
                        labRows[x+1]['CenterCName']=centerName;
                        labRows[x+1]['CenterItemNo']=centerItemNo;
                        break;
                    }
                }
                rightExit=true;
                break;
            }
        }
    }

    //(左边)中心编号为空的情况
    if(!rightExit){

        leftIndex = $('#leftItem').datagrid('getRowIndex', leftRow);
        labItemNo=leftRow.LabItemNo;
        rightIndex = $('#rightItem').datagrid('getRowIndex', rightRow);
        centerItemNo=rightRow.ItemNo;
        centerName=rightRow.CName;

        //可能还需要传所有的字段
        var entity = '{"jsonentity":{' + '"ItemNo":' + centerItemNo + ',"ControlLabNo":' + LabCode + ',"ControlItemNo":' +labItemNo + '}}';
        askService('edit', entity);//请求服务器

        //动态改变原始表数据
        for(var x=0;x<labRows.length;x++){
            if(labItemNo===labRows[x]['LabItemNo']){
                labRows[x]['CenterCName']=centerName;
                labRows[x]['CenterItemNo']=centerItemNo;
                break;
            }
        }
        if(matchRow){
            matchRow.css("background-color",'');
        }
    }
}

//取消对照（依据左边所选记录取消对照关系）
function cancelMatch() {
    var leftRow = $('#leftItem').datagrid('getSelected') || "",
        rightRows=$('#rightItem').datagrid('getRows') || [],
        length=rightRows.length,
        exit=false;

    if (leftRow.length == 0) {
        $.messager.alert('提示信息', '请选择记录', 'info');
    } else {
        if(leftRow.CenterItemNo!==0 && !leftRow.CenterItemNo){
            $.messager.alert('提示','此条记录已经处于未对照状态','info');
            return;
        }

        //匹配的记录显示在当前页的情况
        for(var i=0;i<length;i++){
            if(leftRow.CenterItemNo==rightRows[i].ItemNo){
                rightIndex = $('#rightItem').datagrid('getRowIndex', rightRows[i]);
                leftIndex=$('#leftItem').datagrid('getRowIndex', leftRow);
                centerItemNo=leftRow.CenterItemNo;

                var entity =  'id='+LabCode+'_' +centerItemNo+'_'+ leftRow.LabItemNo;
                $.messager.confirm('确认', '确认取消所选记录的对照关系吗？', function (btn) {
                    if (btn) {
                        askService('delete', entity);//请求服务器

                        //动态改变原始表数据
                        for(var x=0;x<labRows.length;x++){
                            if(leftRow.LabItemNo===labRows[x]['LabItemNo']){
                                labRows[x]['CenterCName']=null;
                                labRows[x]['CenterItemNo']=null;
                                break;
                            }
                        }
                    }
                });
                exit=true;
                break;
            }
        }

        //匹配的记录没有显示在当前页的情况
        if(!exit && length<centerRows.length){
            for(var i=0;i<centerRows.length;i++){
                if(leftRow.CenterItemNo==centerRows[i].ItemNo){
                    rightIndex = 0;
                    leftIndex=$('#leftItem').datagrid('getRowIndex', leftRow);
                    centerItemNo=leftRow.CenterItemNo;

                    var entity =  'id='+LabCode+'_' +centerItemNo+'_'+ leftRow.LabItemNo;
                    $.messager.confirm('确认', '确认取消所选记录的对照关系吗？', function (btn) {
                        if (btn) {
                            askService('delete', entity);//请求服务器
                        }
                    });

                    //动态改变原始表数据
                    for(var x=0;x<labRows.length;x++){
                        if(leftRow.LabItemNo===labRows[x]['LabItemNo']){
                            labRows[x]['CenterCName']=null;
                            labRows[x]['CenterItemNo']=null;
                            break;
                        }
                    }
                    break;
                }
            }
        }
    }
}

//智能对照
function matchAll() {
    var rightRows=$('#rightItem').datagrid('getRows') || [],
        rightLength=rightRows.length || 0,
        leftRows=$('#leftItem').datagrid('getRows') || [],
        leftLength=leftRows.length || 0,
        match=false,
        rows=[];

    for(var i=0;i<leftLength;i++){
        for(var j=0;j<rightLength;j++){
            if(leftRows[i].CenterItemNo==rightRows[j].ItemNo){
                break;
            }
            else if(leftRows[i].CName===rightRows[j].CName || (leftRows[i].ShortCode && leftRows[i].ShortCode===rightRows[j].ShortCode)){
                match=true;
                leftIndex=$('#leftItem').datagrid('getRowIndex', leftRows[i]);
                centerItemNo=rightRows[j].ItemNo;
                centerName=rightRows[j].CName;
                rightIndex=$('#rightItem').datagrid('getRowIndex', rightRows[j]);
                labItemNo=leftRows[i].LabItemNo;

                leftRows[i].leftIndex=leftIndex;
                leftRows[i].LabItemNo=labItemNo;
                leftRows[i].rightIndex=rightIndex;
                leftRows[i].CenterItemNo=centerItemNo;
                leftRows[i].CenterCName=centerName;

            }
        }
        if(j>=rightLength && match)
            rows.push(leftRows[i]);
        match=false;
    }

    if(rows.length){
        $('#dlg').dialog({modal:true});
        $('#dlg').dialog('open').dialog('setTitle','确认');
        $('#dlg').window('center');
        $('#dlgItem').datagrid('loadData',{total:rows.length,rows:rows});
    }else{
        $.messager.alert('提示','当前数据已对照完成或没有数据','info');
    }
}

//保存
function save() {
    var rows=$('#dlgItem').datagrid('getRows') || [],
        length=rows.length;
    for(var i=0;i<length;i++){
        leftIndex=rows[i].leftIndex;
        labItemNo=rows[i].LabItemNo;
        rightIndex=rows[i].rightIndex;
        centerItemNo=rows[i].CenterItemNo;
        centerName=rows[i].CenterCName;
        var entity = '{"jsonentity":{' + '"ItemNo":' + centerItemNo + ',"ControlLabNo":' + LabCode + ',"ControlItemNo":' +labItemNo + '}}';
        askService('edit', entity);//请求服务器
    }
    $('#dlg').dialog('close');
}

//取消
function cancel() {
    $('#dlg').dialog('close');
}