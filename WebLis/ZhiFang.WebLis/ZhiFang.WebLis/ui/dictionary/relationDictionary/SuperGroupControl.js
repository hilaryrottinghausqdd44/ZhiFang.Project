/**
 *@OverView 检验大组字典表对照关系表
 * Created by gwh on 14-12-19.
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
//中心检验大组编码
var centerSuperGroupNo;
//实验室对照列表
var labRows;
//中心检验大组名称
var centerName;
//实验室检验大组编码
var labSuperGroupNo;
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

    //初始化实验室检验大组字典表对照关系表
    $('#leftSuperGroup').datagrid({
        title: '检验大组字典表对照关系表',
        url:Shell.util.Path.rootPath+'/ServiceWCF/DictionaryService.svc/GetPubDict',
        queryParams:{
            tablename:"B_Lab_SuperGroupControl",
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
         var rightRows=$('#rightSuperGroup').datagrid('getRows'),
         length=rightRows.length;

            if(matchRow){
                matchRow.css("background-color",'');
            }
         for(var i=0;i<length;i++){
         if(row.SuperGroupNo==rightRows[i].SuperGroupNo){
             $('#datagrid-row-r2-2-'+i).css("background-color","#4afffd");
             matchRow= $('#datagrid-row-r2-2-'+i);
             break;
         }
         }
         }
    });

    //初始化中心检验大组字典列表
    $('#rightSuperGroup').datagrid({
        title: '中心检验大组列表',

        url:Shell.util.Path.rootPath+'/ServiceWCF/DictionaryService.svc/GetPubDict',
        queryParams:{
            tablename:"SuperGroup",
            fields:'SuperGroupNo,CName,ShortCode'
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

    //初始化检验大组模糊匹配列表
    $('#tbMatch').datagrid({
        method:'GET',
        loadMsg:'数据加载...',
        fitColumns:true,
        singleSelect:true,
        striped:true,
        fit:true,
        columns:matchColumns(),
        onDblClickRow:function(index,matchRow){
            var row = $('#rightSuperGroup').datagrid('getSelected') || "";

            leftIndex=$('#leftSuperGroup').datagrid('getRowIndex',matchRow);

            if (row.CName === matchRow.CName) {
                labSuperGroupNo=row.LabSuperGroupNo;
                rightIndex = $('#rightSuperGroup').datagrid('getRowIndex', row);
                centerSuperGroupNo=matchRow.SuperGroupNo;
                var entity = '{"jsonentity":{' + '"SuperGroupNo":' + centerSuperGroupNo + ',"ControlLabNo":' + LabCode + ',"ControlSuperGroupNo":' +labSuperGroupNo + '}}';
                askService('edit', entity);//请求服务器
            }
            $('#dlgMatch').dialog('close');
        }
    });

    //初始化智能对照成功的列表
    $('#dlgSuperGroup').datagrid({
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
            {field: 'LabSuperGroupNo', title: '实验室检验大组编号', width: fixWidth(0.2) },
            {field: 'CName', title: '实验室检验大组名称', width: fixWidth(0.3)},
            {field: 'ShortCode', title: '实验室检验大组简码', width: fixWidth(0.2)},
            {field: 'SuperGroupNo', title: '中心检验大组编码', width: fixWidth(0.2)},
            {field: 'CenterCName', title: '中心检验大组名称', width: fixWidth(0.3)}
        ]
    ];
    return columns;
}

//创建中心数据列
function matchColumns() {
    var columns = [
        [
            {field: 'SuperGroupNo', title: '中心检验大组编号', width: fixWidth(0.2)},
            {field: 'ShortCode', title: '中心检验大组简码', width: fixWidth(0.2)},
            {field: 'CName', title: '中心检验大组名称', width: fixWidth(0.3)}
        ]
    ];
    return columns;
}

//创建对话框数据列
function dlgColumns() {
    var columns = [
        [
            {field: 'LabSuperGroupNo', title: '实验室检验大组编号', width: fixWidth(0.2) },
            {field: 'CName', title: '实验室检验大组名称', width: fixWidth(0.2)},
            {field: 'ShortCode', title: '实验室检验大组简码', width: fixWidth(0.2)},
            {field: 'CenterSuperGroupNo', title: '中心检验大组编码', width: fixWidth(0.2)},
            {field: 'CenterCName', title: '中心检验大组名称', width: fixWidth(0.2)}
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
        tablename:"B_Lab_SuperGroupControl",
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
                    case'edit':
                        $('#leftSuperGroup').datagrid('selectRow', leftIndex);
                        $('#rightSuperGroup').datagrid('selectRow', rightIndex);

                        $('#leftSuperGroup').datagrid('updateRow', {
                            index: leftIndex,
                            row: {SuperGroupNo: centerSuperGroupNo,CenterCName: centerName}
                        });
                        break;
                    case'delete':
                        $('#leftSuperGroup').datagrid('updateRow', {
                            index: leftIndex,
                            row: {SuperGroupNo: null,CenterCName: null}
                        });
                        break;
                    case'leftList':
                        var list = eval('(' + data.ResultDataValue + ')') || {};
                        labRows = list.rows || [];
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
function setService(serviceType, serviceParam, entity) {

    //数据请求方式（GET,POST）
    switch (serviceType) {
        case'add':
            serviceParam.serviceName = 'UpdateOrAddSuperGroupControlModelByID';//增加数据服务名
            serviceParam.type = 'POST';//数据请求方式GET
            serviceParam.data = entity;//向服务器传递的数据
            break;
        case'edit':
            serviceParam.serviceName = 'UpdateOrAddSuperGroupControlModelByID';//编辑数据服务名
            serviceParam.type = 'POST';//数据请求方式POST
            serviceParam.data = entity;//向服务器传递的数据
            break;
        case'delete':
            serviceParam.serviceName = 'DelSuperGroupControlModelByID';//编辑数据服务名
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
        $('#leftSuperGroup').datagrid('load',{
                tablename:"B_Lab_SuperGroupControl",
                selectedflag:labContrast,
                labcode:LabCode,
                time:new Date().getTime()}
        );
    }

    if(matchRow){
        matchRow.css("background-color",'');
    }
    var rows=$('#rightSuperGroup').datagrid('getSelections') || [];
    if(rows.length)
        $('#rightSuperGroup').datagrid('unselectAll');
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
            $('#leftSuperGroup').datagrid('load',{
                    tablename:"B_Lab_SuperGroupControl",
                    selectedflag:labContrast,
                    labcode:LabCode,
                    time:new Date().getTime()}
            );
        }
        var rows=$('#rightSuperGroup').datagrid('getSelections') || [];
        if(rows.length)
            $('#rightSuperGroup').datagrid('unselectAll');
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
        $('#leftSuperGroup').datagrid('load',{
                tablename:"B_Lab_SuperGroupControl",
                selectedflag:labContrast,
                labcode:LabCode,
                time:new Date().getTime()}
        );
    }

    if(matchRow){
        matchRow.css("background-color",'');
    }
    var rows=$('#rightSuperGroup').datagrid('getSelections') || [];
    if(rows.length)
        $('#rightSuperGroup').datagrid('unselectAll');
}

//实验室字典查询
function labSearch(value) {
    $('#leftSuperGroup').datagrid('load', {filervalue: value.trim(),labcode:LabCode,tablename:"B_Lab_SuperGroupControl",selectedflag:labContrast,time:new Date().getTime()});
    if(matchRow){
        matchRow.css("background-color",'');
    }
}

//中心字典查询
function centerSearch(value) {
    if(!value){
        $('#rightSuperGroup').datagrid('loadData',{success: true, ResultDataValue: Shell.util.JSON.encode({total: centerRows.length, rows: centerRows})});
    }else{
        var filterRows=[],
            length=centerRows.length;

        value=value.trim();
        for(var i=0;i<length;i++){
            if((centerRows[i]['SuperGroupNo']).toString().indexOf(value)>-1 || (centerRows[i]['CName'] && (centerRows[i]['CName']).toString().indexOf(value)>-1)){
                filterRows.push(centerRows[i]);
            }
        }
        $('#rightSuperGroup').datagrid('loadData',{success: true, ResultDataValue: Shell.util.JSON.encode({total: filterRows.length, rows: filterRows})});
    }
}

//对照
function matchSingle() {
    var rightRow = $('#rightSuperGroup').datagrid('getSelected') || "",
        rightRows = $('#rightSuperGroup').datagrid('getRows') || [],
        rightLength=rightRows.length,
        leftRow = $('#leftSuperGroup').datagrid('getSelected') || "",
        leftRows=$('#leftSuperGroup').datagrid('getRows') || [],
        leftLength=leftRows.length,
        leftExist=false,
        rightExit=false;

    if(!rightRow || !leftRow){
        $.messager.alert('提示','请选择要进行对照的记录','info');
        return;
    }

    //当前中心列表数据
    for(var i=0;i<rightLength;i++){
        if(leftRow.SuperGroupNo==rightRows[i].SuperGroupNo){

            leftIndex= $('#leftSuperGroup').datagrid('getRowIndex', leftRow);
            //先解除左边的对照关系
            var delID =  'id='+LabCode+'_' +leftRow.SuperGroupNo+'_'+ leftRow.LabSuperGroupNo;
            askService('delete', delID);//请求服务器

            //动态改变原始表数据
            for(var x=0;x<labRows.length;x++){
                if(leftRow.LabSuperGroupNo===labRows[x]['LabSuperGroupNo']){
                    labRows[x]['CenterCName']=null;
                    labRows[x]['SuperGroupNo']=null;
                    break;
                }
            }

            //当前实验室对照关系列表数据
            for(var j=0;j<leftLength;j++){
                if(leftRows[j].SuperGroupNo===rightRow.SuperGroupNo){
                    leftIndex= $('#leftSuperGroup').datagrid('getRowIndex', leftRows[j]);
                    //先解除右边的对照关系
                    var delID =  'id='+LabCode+'_' +leftRows[j].SuperGroupNo+'_'+ leftRows[j].LabSuperGroupNo;
                    askService('delete', delID);//请求服务器

                    //动态改变原始表数据
                    for(var x=0;x<labRows.length;x++){
                        if(leftRows[j].LabSuperGroupNo===labRows[x]['LabSuperGroupNo']){
                            labRows[x]['CenterCName']=null;
                            labRows[x]['SuperGroupNo']=null;
                            break;
                        }
                    }
                    leftExist=true;
                    break;
                }
            }

            //遍历实验室对照关系列表所有数据
            if(!leftExist && leftLength<labRows.length){
                for(var k=0;k<labRows.length;k++){
                    if(labRows[k].SuperGroupNo===rightRow.SuperGroupNo){
                        leftIndex= 0;
                        //先解除右边的对照关系
                        var delID =  'id='+LabCode+'_' +labRows[k].SuperGroupNo+'_'+ labRows[k].LabSuperGroupNo;
                        askService('delete', delID);//请求服务器

                        //动态改变原始表数据
                        labRows[k]['CenterCName']=null;
                        labRows[k]['SuperGroupNo']=null;
                        leftExist=true;
                        break;
                    }
                }
            }
            leftIndex= $('#leftSuperGroup').datagrid('getRowIndex', leftRow);
            centerSuperGroupNo=rightRow.SuperGroupNo;
            centerName=rightRow.CName;
            rightIndex = $('#rightSuperGroup').datagrid('getRowIndex', rightRow);
            labSuperGroupNo=leftRow.LabSuperGroupNo;

            //可能还需要传所有的字段
            var entity = '{"jsonentity":{' + '"SuperGroupNo":' + centerSuperGroupNo + ',"ControlLabNo":' + LabCode + ',"ControlSuperGroupNo":' +labSuperGroupNo + '}}';
            askService('edit', entity);//请求服务器

            //动态改变原始表数据
            for(var x=0;x<labRows.length;x++){
                if(labSuperGroupNo===labRows[x]['LabSuperGroupNo']){
                    labRows[x]['CenterCName']=centerName;
                    labRows[x]['SuperGroupNo']=centerSuperGroupNo;
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
            if(leftRow.SuperGroupNo==centerRows[i].SuperGroupNo){

                leftIndex= $('#leftSuperGroup').datagrid('getRowIndex', leftRow);
                //先解除左边的对照关系
                var delID =  'id='+LabCode+'_' +leftRow.SuperGroupNo+'_'+ leftRow.LabSuperGroupNo;
                askService('delete', delID);//请求服务器

                //动态改变原始表数据
                for(var x=0;x<labRows.length;x++){
                    if(leftRow.LabSuperGroupNo===labRows[x]['LabSuperGroupNo']){
                        labRows[x]['CenterCName']=null;
                        labRows[x]['SuperGroupNo']=null;
                        break;
                    }
                }

                //当前实验室对照关系列表数据
                for(var j=0;j<leftLength;j++){
                    if(leftRows[j].SuperGroupNo===rightRow.SuperGroupNo){
                        leftIndex= $('#leftSuperGroup').datagrid('getRowIndex', leftRows[j]);
                        //先解除右边的对照关系
                        var delID =  'id='+LabCode+'_' +leftRows[j].SuperGroupNo+'_'+ leftRows[j].LabSuperGroupNo;
                        askService('delete', delID);//请求服务器

                        //动态改变原始表数据
                        for(var x=0;x<labRows.length;x++){
                            if(leftRows[j].LabSuperGroupNo===labRows[x]['LabSuperGroupNo']){
                                labRows[x]['CenterCName']=null;
                                labRows[x]['SuperGroupNo']=null;
                                break;
                            }
                        }
                        leftExist=true;
                        break;
                    }
                }

                //遍历实验室对照关系列表所有数据
                if(!leftExist && leftLength<labRows.length){
                    for(var k=0;k<labRows.length;k++){
                        if(labRows[k].SuperGroupNo===rightRow.SuperGroupNo){
                            leftIndex= 0;
                            //先解除右边的对照关系
                            var delID =  'id='+LabCode+'_' +labRows[k].SuperGroupNo+'_'+ labRows[k].LabSuperGroupNo;
                            askService('delete', delID);//请求服务器

                            //动态改变原始表数据
                            labRows[k]['CenterCName']=null;
                            labRows[k]['SuperGroupNo']=null;
                            leftExist=true;
                            break;
                        }
                    }
                }

                centerSuperGroupNo=rightRow.SuperGroupNo;
                centerName=rightRow.CName;
                rightIndex = 0;
                labSuperGroupNo=leftRow.LabSuperGroupNo;

                //可能还需要传所有的字段
                var entity = '{"jsonentity":{' + '"SuperGroupNo":' + centerSuperGroupNo + ',"ControlLabNo":' + LabCode + ',"ControlSuperGroupNo":' +labSuperGroupNo + '}}';
                askService('edit', entity);//请求服务器

                //动态改变原始表数据
                for(var x=0;x<labRows.length;x++){
                    if(labSuperGroupNo===labRows[x]['LabSuperGroupNo']){
                        labRows[x]['CenterCName']=centerName;
                        labRows[x]['SuperGroupNo']=centerSuperGroupNo;
                        break;
                    }
                }
                rightExit=true;
                break;
            }
        }
    }

    //中心编号为空的情况
    if(!rightExit){

        //当前实验室列表数据
        for(var i=0;i<leftLength;i++){
            if(leftRows[i].SuperGroupNo==rightRow.SuperGroupNo){

                leftIndex= $('#leftSuperGroup').datagrid('getRowIndex', leftRows[i]);
                //先解除右边的对照关系
                var delID =  'id='+LabCode+'_' +leftRows[i].SuperGroupNo+'_'+ leftRows[i].LabSuperGroupNo;
                askService('delete', delID);//请求服务器

                //动态改变原始表数据
                for(var x=0;x<labRows.length;x++){
                    if(leftRows[i].LabSuperGroupNo===labRows[x]['LabSuperGroupNo']){
                        labRows[x]['CenterCName']=null;
                        labRows[x]['SuperGroupNo']=null;
                        break;
                    }
                }
                leftExist=true;
                break;
            }
        }

        //遍历实验室列表全部数据
        if(!leftExist && leftLength<labRows.length){
            for(var j=0;j<labRows.length;j++){
                if(labRows[j].SuperGroupNo==rightRow.SuperGroupNo){

                    leftIndex= 0;
                    //先解除右边的对照关系
                    var delID =  'id='+LabCode+'_' +labRows[j].SuperGroupNo+'_'+ labRows[j].LabSuperGroupNo;
                    askService('delete', delID);//请求服务器

                    //动态改变原始表数据
                    labRows[j]['CenterCName']=null;
                    labRows[j]['SuperGroupNo']=null;
                    leftExist=true;
                    break;
                }
            }
        }

        leftIndex = $('#leftSuperGroup').datagrid('getRowIndex', leftRow);
        labSuperGroupNo=leftRow.LabSuperGroupNo;
        rightIndex = $('#rightSuperGroup').datagrid('getRowIndex', rightRow);
        centerSuperGroupNo=rightRow.SuperGroupNo;
        centerName=rightRow.CName;

        //可能还需要传所有的字段
        var entity = '{"jsonentity":{' + '"SuperGroupNo":' + centerSuperGroupNo + ',"ControlLabNo":' + LabCode + ',"ControlSuperGroupNo":' +labSuperGroupNo + '}}';
        askService('edit', entity);//请求服务器

        //动态改变原始表数据
        for(var x=0;x<labRows.length;x++){
            if(labSuperGroupNo===labRows[x]['LabSuperGroupNo']){
                labRows[x]['CenterCName']=centerName;
                labRows[x]['SuperGroupNo']=centerSuperGroupNo;
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
    var leftRow = $('#leftSuperGroup').datagrid('getSelected') || "",
        rightRows=$('#rightSuperGroup').datagrid('getRows') || [],
        length=rightRows.length,
        exit=false;

    if (leftRow.length == 0) {
        $.messager.alert('提示信息', '请选择记录', 'info');
    } else {
        if(leftRow.SuperGroupNo!==0 && !leftRow.SuperGroupNo){
            $.messager.alert('提示','此条记录已经处于未对照状态','info');
            return;
        }

        //匹配的记录显示在当前页的情况
        for(var i=0;i<length;i++){
            if(leftRow.SuperGroupNo==rightRows[i].SuperGroupNo){
                rightIndex = $('#rightSuperGroup').datagrid('getRowIndex', rightRows[i]);
                leftIndex=$('#leftSuperGroup').datagrid('getRowIndex', leftRow);
                centerSuperGroupNo=leftRow.SuperGroupNo;

                var entity =  'id='+LabCode+'_' +centerSuperGroupNo+'_'+ leftRow.LabSuperGroupNo;
                $.messager.confirm('确认', '确认取消所选记录的对照关系吗？', function (btn) {
                    if (btn) {
                        askService('delete', entity);//请求服务器

                        //动态改变原始表数据
                        for(var x=0;x<labRows.length;x++){
                            if(leftRow.LabSuperGroupNo===labRows[x]['LabSuperGroupNo']){
                                labRows[x]['CenterCName']=null;
                                labRows[x]['SuperGroupNo']=null;
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
                if(leftRow.SuperGroupNo==centerRows[i].SuperGroupNo){
                    rightIndex = 0;
                    leftIndex=$('#leftSuperGroup').datagrid('getRowIndex', leftRow);
                    centerSuperGroupNo=leftRow.SuperGroupNo;

                    var entity =  'id='+LabCode+'_' +centerSuperGroupNo+'_'+ leftRow.LabSuperGroupNo;
                    $.messager.confirm('确认', '确认取消所选记录的对照关系吗？', function (btn) {
                        if (btn) {
                            askService('delete', entity);//请求服务器
                        }
                    });

                    //动态改变原始表数据
                    for(var x=0;x<labRows.length;x++){
                        if(leftRow.LabSuperGroupNo===labRows[x]['LabSuperGroupNo']){
                            labRows[x]['CenterCName']=null;
                            labRows[x]['SuperGroupNo']=null;
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
    var rightRows=$('#rightSuperGroup').datagrid('getRows') || [],
        rightLength=rightRows.length || 0,
        leftRows=$('#leftSuperGroup').datagrid('getRows') || [],
        leftLength=leftRows.length || 0,
        match=false,
        rows=[];

    for(var i=0;i<leftLength;i++){
        for(var j=0;j<rightLength;j++){
            if(leftRows[i].SuperGroupNo==rightRows[j].SuperGroupNo){
                break;
            }
            else if(leftRows[i].CName===rightRows[j].CName || (leftRows[i].ShortCode && leftRows[i].ShortCode===rightRows[j].ShortCode)){
                match=true;
                leftIndex=$('#leftSuperGroup').datagrid('getRowIndex', leftRows[i]);
                centerSuperGroupNo=rightRows[j].SuperGroupNo;
                centerName=rightRows[j].CName;
                rightIndex=$('#rightSuperGroup').datagrid('getRowIndex', rightRows[j]);
                labSuperGroupNo=leftRows[i].LabSuperGroupNo;

                leftRows[i].leftIndex=leftIndex;
                leftRows[i].LabSuperGroupNo=labSuperGroupNo;
                leftRows[i].rightIndex=rightIndex;
                leftRows[i].CenterSuperGroupNo=centerSuperGroupNo;
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
        $('#dlgSuperGroup').datagrid('loadData',{total:rows.length,rows:rows});
    }else{
        $.messager.alert('提示','当前数据已对照完成或没有数据','info');
    }
}

//保存
function save() {
    var rows=$('#dlgSuperGroup').datagrid('getRows') || [],
        length=rows.length;
    for(var i=0;i<length;i++){
        leftIndex=rows[i].leftIndex;
        labSuperGroupNo=rows[i].LabSuperGroupNo;
        rightIndex=rows[i].rightIndex;
        centerSuperGroupNo=rows[i].CenterSuperGroupNo;
        centerName=rows[i].CenterCName;
        var entity = '{"jsonentity":{' + '"SuperGroupNo":' + centerSuperGroupNo + ',"ControlLabNo":' + LabCode + ',"ControlSuperGroupNo":' +labSuperGroupNo + '}}';
        askService('edit', entity);//请求服务器
    }
    $('#dlg').dialog('close');
}

//取消
function cancel() {
    $('#dlg').dialog('close');
}