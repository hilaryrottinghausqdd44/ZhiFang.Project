/**
 *@OverView 民族字典表对照关系表
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
//中心民族编码
var centerFolkNo;
//实验室对照列表
var labRows;
//中心民族名称
var centerName;
//实验室民族编码
var labFolkNo;
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

    //初始化实验室民族字典表对照关系表
    $('#leftFolk').datagrid({
        title: '民族字典表对照关系表',
        url:Shell.util.Path.rootPath+'/ServiceWCF/DictionaryService.svc/GetPubDict',
        queryParams:{
            tablename:"B_Lab_FolkTypeControl",
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
         var rightRows=$('#rightFolk').datagrid('getRows'),
         length=rightRows.length;

             if(matchRow){
                 matchRow.css("background-color",'');
             }
         for(var i=0;i<length;i++){
         if(row.FolkNo==rightRows[i].FolkNo){
             $('#datagrid-row-r2-2-'+i).css("background-color","#4afffd");
             matchRow= $('#datagrid-row-r2-2-'+i);
             break;
         }
         }
         }
    });

    //初始化中心民族字典列表
    $('#rightFolk').datagrid({
        title: '中心民族列表',

        url:Shell.util.Path.rootPath+'/ServiceWCF/DictionaryService.svc/GetPubDict',
        queryParams:{
            tablename:"FolkType",
            fields:'FolkNo,ShortCode,CName'
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

    //初始化民族模糊匹配列表
    $('#tbMatch').datagrid({
        method:'GET',
        loadMsg:'数据加载...',
        fitColumns:true,
        singleSelect:true,
        striped:true,
        fit:true,
        columns:matchColumns(),
        onDblClickRow:function(index,matchRow){
            var row = $('#rightFolk').datagrid('getSelected') || "";

            leftIndex=$('#leftFolk').datagrid('getRowIndex',matchRow);

            if (row.CName === matchRow.CName) {
                labFolkNo=row.LabFolkNo;
                rightIndex = $('#rightFolk').datagrid('getRowIndex', row);
                centerFolkNo=matchRow.FolkNo;
                var entity = '{"jsonentity":{' + '"FolkNo":' + centerFolkNo + ',"ControlLabNo":' + LabCode + ',"ControlFolkNo":' +labFolkNo + '}}';
                askService('edit', entity);//请求服务器
            }
            $('#dlgMatch').dialog('close');
        }
    });

    //初始化智能对照成功的列表
    $('#dlgFolk').datagrid({
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
            {field: 'LabFolkNo', title: '实验室民族编号', width: fixWidth(0.2) },
            {field: 'CName', title: '实验室民族名称', width: fixWidth(0.3)},
            {field: 'ShortCode', title: '实验室民族简码', width: fixWidth(0.2)},
            {field: 'FolkNo', title: '中心民族编码', width: fixWidth(0.2)},
            {field: 'CenterCName', title: '中心民族名称', width: fixWidth(0.3)}
        ]
    ];
    return columns;
}

//创建中心数据列
function matchColumns() {
    var columns = [
        [
            {field: 'FolkNo', title: '中心民族编号', width: fixWidth(0.2)},
            {field: 'ShortCode', title: '中心民族简码', width: fixWidth(0.2)},
            {field: 'CName', title: '中心民族名称', width: fixWidth(0.3)}
        ]
    ];
    return columns;
}

//创建对话框数据列
function dlgColumns() {
    var columns = [
        [
            {field: 'LabFolkNo', title: '实验室民族编号', width: fixWidth(0.2) },
            {field: 'CName', title: '实验室民族名称', width: fixWidth(0.2)},
            {field: 'ShortCode', title: '实验室民族简码', width: fixWidth(0.2)},
            {field: 'CenterFolkNo', title: '中心民族编码', width: fixWidth(0.2)},
            {field: 'CenterCName', title: '中心民族名称', width: fixWidth(0.2)}
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
        tablename:"B_Lab_FolkTypeControl",
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
                        $('#leftFolk').datagrid('selectRow', leftIndex);
                        $('#rightFolk').datagrid('selectRow', rightIndex);

                        $('#leftFolk').datagrid('updateRow', {
                            index: leftIndex,
                            row: {FolkNo: centerFolkNo,CenterCName: centerName}
                        });
                        break;
                    case'delete':
                        $('#leftFolk').datagrid('updateRow', {
                            index: leftIndex,
                            row: {FolkNo: null,CenterCName: null}
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
            serviceParam.serviceName = 'UpdateOrAddFolkTypeControlModelByID';//增加数据服务名
            serviceParam.type = 'POST';//数据请求方式GET
            serviceParam.data = entity;//向服务器传递的数据
            break;
        case'edit':
            serviceParam.serviceName = 'UpdateOrAddFolkTypeControlModelByID';//编辑数据服务名
            serviceParam.type = 'POST';//数据请求方式POST
            serviceParam.data = entity;//向服务器传递的数据
            break;
        case'delete':
            serviceParam.serviceName = 'DelFolkTypeControlModelByID';//编辑数据服务名
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
        $('#leftFolk').datagrid('load',{
                tablename:"B_Lab_FolkTypeControl",
                selectedflag:labContrast,
                labcode:LabCode,
                time:new Date().getTime()}
        );
    }

    if(matchRow){
        matchRow.css("background-color",'');
    }
    var rows=$('#rightFolk').datagrid('getSelections') || [];
    if(rows.length)
        $('#rightFolk').datagrid('unselectAll');
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
            $('#leftFolk').datagrid('load',{
                    tablename:"B_Lab_FolkTypeControl",
                    selectedflag:labContrast,
                    labcode:LabCode,
                    time:new Date().getTime()}
            );
        }
        var rows=$('#rightFolk').datagrid('getSelections') || [];
        if(rows.length)
            $('#rightFolk').datagrid('unselectAll');
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
        $('#leftFolk').datagrid('load',{
                tablename:"B_Lab_FolkTypeControl",
                selectedflag:labContrast,
                labcode:LabCode,
                time:new Date().getTime()}
        );
    }

    if(matchRow){
        matchRow.css("background-color",'');
    }
    var rows=$('#rightFolk').datagrid('getSelections') || [];
    if(rows.length)
        $('#rightFolk').datagrid('unselectAll');
}

//实验室字典查询
function labSearch(value) {
    $('#leftFolk').datagrid('load', {filervalue: value.trim(),labcode:LabCode,tablename:"B_Lab_FolkTypeControl",selectedflag:labContrast,time:new Date().getTime()});
    if(matchRow){
        matchRow.css("background-color",'');
    }
}

//中心字典查询
function centerSearch(value) {
    if(!value){
        $('#rightFolk').datagrid('loadData',{success: true, ResultDataValue: Shell.util.JSON.encode({total: centerRows.length, rows: centerRows})});
    }else{
        var filterRows=[],
            length=centerRows.length;

        value=value.trim();
        for(var i=0;i<length;i++){
            if((centerRows[i]['FolkNo']).toString().indexOf(value)>-1 || (centerRows[i]['CName'] && (centerRows[i]['CName']).toString().indexOf(value)>-1) ){
                filterRows.push(centerRows[i]);
            }
        }
        $('#rightFolk').datagrid('loadData',{success: true, ResultDataValue: Shell.util.JSON.encode({total: filterRows.length, rows: filterRows})});
    }
}

//对照
function matchSingle() {
    var rightRow = $('#rightFolk').datagrid('getSelected') || "",
        rightRows = $('#rightFolk').datagrid('getRows') || [],
        rightLength=rightRows.length,
        leftRow = $('#leftFolk').datagrid('getSelected') || "",
        leftRows=$('#leftFolk').datagrid('getRows') || [],
        leftLength=leftRows.length,
        leftExist=false,
        rightExit=false;

    if(!rightRow || !leftRow){
        $.messager.alert('提示','请选择要进行对照的记录','info');
        return;
    }

    //当前中心列表数据
    for(var i=0;i<rightLength;i++){
        if(leftRow.FolkNo==rightRows[i].FolkNo){

            leftIndex= $('#leftFolk').datagrid('getRowIndex', leftRow);
            //先解除左边的对照关系
            var delID =  'id='+LabCode+'_' +leftRow.FolkNo+'_'+ leftRow.LabFolkNo;
            askService('delete', delID);//请求服务器

            //动态改变原始表数据
            for(var x=0;x<labRows.length;x++){
                if(leftRow.LabFolkNo===labRows[x]['LabFolkNo']){
                    labRows[x]['CenterCName']=null;
                    labRows[x]['FolkNo']=null;
                    break;
                }
            }

            //当前实验室对照关系列表数据
            for(var j=0;j<leftLength;j++){
                if(leftRows[j].FolkNo===rightRow.FolkNo){
                    leftIndex= $('#leftFolk').datagrid('getRowIndex', leftRows[j]);
                    //先解除右边的对照关系
                    var delID =  'id='+LabCode+'_' +leftRows[j].FolkNo+'_'+ leftRows[j].LabFolkNo;
                    askService('delete', delID);//请求服务器

                    //动态改变原始表数据
                    for(var x=0;x<labRows.length;x++){
                        if(leftRows[j].LabFolkNo===labRows[x]['LabFolkNo']){
                            labRows[x]['CenterCName']=null;
                            labRows[x]['FolkNo']=null;
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
                    if(labRows[k].FolkNo===rightRow.FolkNo){
                        leftIndex= 0;
                        //先解除右边的对照关系
                        var delID =  'id='+LabCode+'_' +labRows[k].FolkNo+'_'+ labRows[k].LabFolkNo;
                        askService('delete', delID);//请求服务器

                        //动态改变原始表数据
                        labRows[k]['CenterCName']=null;
                        labRows[k]['FolkNo']=null;
                        leftExist=true;
                        break;
                    }
                }
            }
            leftIndex= $('#leftFolk').datagrid('getRowIndex', leftRow);
            centerFolkNo=rightRow.FolkNo;
            centerName=rightRow.CName;
            rightIndex = $('#rightFolk').datagrid('getRowIndex', rightRow);
            labFolkNo=leftRow.LabFolkNo;

            //可能还需要传所有的字段
            var entity = '{"jsonentity":{' + '"FolkNo":' + centerFolkNo + ',"ControlLabNo":' + LabCode + ',"ControlFolkNo":' +labFolkNo + '}}';
            askService('edit', entity);//请求服务器

            //动态改变原始表数据
            for(var x=0;x<labRows.length;x++){
                if(labFolkNo===labRows[x]['LabFolkNo']){
                    labRows[x]['CenterCName']=centerName;
                    labRows[x]['FolkNo']=centerFolkNo;
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
            if(leftRow.FolkNo==centerRows[i].FolkNo){

                leftIndex= $('#leftFolk').datagrid('getRowIndex', leftRow);
                //先解除左边的对照关系
                var delID =  'id='+LabCode+'_' +leftRow.FolkNo+'_'+ leftRow.LabFolkNo;
                askService('delete', delID);//请求服务器

                //动态改变原始表数据
                for(var x=0;x<labRows.length;x++){
                    if(leftRow.LabFolkNo===labRows[x]['LabFolkNo']){
                        labRows[x]['CenterCName']=null;
                        labRows[x]['FolkNo']=null;
                        break;
                    }
                }

                //当前实验室对照关系列表数据
                for(var j=0;j<leftLength;j++){
                    if(leftRows[j].FolkNo===rightRow.FolkNo){
                        leftIndex= $('#leftFolk').datagrid('getRowIndex', leftRows[j]);
                        //先解除右边的对照关系
                        var delID =  'id='+LabCode+'_' +leftRows[j].FolkNo+'_'+ leftRows[j].LabFolkNo;
                        askService('delete', delID);//请求服务器

                        //动态改变原始表数据
                        for(var x=0;x<labRows.length;x++){
                            if(leftRows[j].LabFolkNo===labRows[x]['LabFolkNo']){
                                labRows[x]['CenterCName']=null;
                                labRows[x]['FolkNo']=null;
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
                        if(labRows[k].FolkNo===rightRow.FolkNo){
                            leftIndex= 0;
                            //先解除右边的对照关系
                            var delID =  'id='+LabCode+'_' +labRows[k].FolkNo+'_'+ labRows[k].LabFolkNo;
                            askService('delete', delID);//请求服务器

                            //动态改变原始表数据
                            labRows[k]['CenterCName']=null;
                            labRows[k]['FolkNo']=null;
                            leftExist=true;
                            break;
                        }
                    }
                }

                centerFolkNo=rightRow.FolkNo;
                centerName=rightRow.CName;
                rightIndex = 0;
                labFolkNo=leftRow.LabFolkNo;

                //可能还需要传所有的字段
                var entity = '{"jsonentity":{' + '"FolkNo":' + centerFolkNo + ',"ControlLabNo":' + LabCode + ',"ControlFolkNo":' +labFolkNo + '}}';
                askService('edit', entity);//请求服务器

                //动态改变原始表数据
                for(var x=0;x<labRows.length;x++){
                    if(labFolkNo===labRows[x]['LabFolkNo']){
                        labRows[x]['CenterCName']=centerName;
                        labRows[x]['FolkNo']=centerFolkNo;
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
            if(leftRows[i].FolkNo==rightRow.FolkNo){

                leftIndex= $('#leftFolk').datagrid('getRowIndex', leftRows[i]);
                //先解除右边的对照关系
                var delID =  'id='+LabCode+'_' +leftRows[i].FolkNo+'_'+ leftRows[i].LabFolkNo;
                askService('delete', delID);//请求服务器

                //动态改变原始表数据
                for(var x=0;x<labRows.length;x++){
                    if(leftRows[i].LabFolkNo===labRows[x]['LabFolkNo']){
                        labRows[x]['CenterCName']=null;
                        labRows[x]['FolkNo']=null;
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
                if(labRows[j].FolkNo==rightRow.FolkNo){

                    leftIndex= 0;
                    //先解除右边的对照关系
                    var delID =  'id='+LabCode+'_' +labRows[j].FolkNo+'_'+ labRows[j].LabFolkNo;
                    askService('delete', delID);//请求服务器

                    //动态改变原始表数据
                    labRows[j]['CenterCName']=null;
                    labRows[j]['FolkNo']=null;
                    leftExist=true;
                    break;
                }
            }
        }

        leftIndex = $('#leftFolk').datagrid('getRowIndex', leftRow);
        labFolkNo=leftRow.LabFolkNo;
        rightIndex = $('#rightFolk').datagrid('getRowIndex', rightRow);
        centerFolkNo=rightRow.FolkNo;
        centerName=rightRow.CName;

        //可能还需要传所有的字段
        var entity = '{"jsonentity":{' + '"FolkNo":' + centerFolkNo + ',"ControlLabNo":' + LabCode + ',"ControlFolkNo":' +labFolkNo + '}}';
        askService('edit', entity);//请求服务器

        //动态改变原始表数据
        for(var x=0;x<labRows.length;x++){
            if(labFolkNo===labRows[x]['LabFolkNo']){
                labRows[x]['CenterCName']=centerName;
                labRows[x]['FolkNo']=centerFolkNo;
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
    var leftRow = $('#leftFolk').datagrid('getSelected') || "",
        rightRows=$('#rightFolk').datagrid('getRows') || [],
        length=rightRows.length,
        exit=false;

    if (leftRow.length == 0) {
        $.messager.alert('提示信息', '请选择记录', 'info');
    } else {
        if(leftRow.FolkNo!==0 && !leftRow.FolkNo){
            $.messager.alert('提示','此条记录已经处于未对照状态','info');
            return;
        }

        //匹配的记录显示在当前页的情况
        for(var i=0;i<length;i++){
            if(leftRow.FolkNo==rightRows[i].FolkNo){
                rightIndex = $('#rightFolk').datagrid('getRowIndex', rightRows[i]);
                leftIndex=$('#leftFolk').datagrid('getRowIndex', leftRow);
                centerFolkNo=leftRow.FolkNo;

                var entity =  'id='+LabCode+'_' +centerFolkNo+'_'+ leftRow.LabFolkNo;
                $.messager.confirm('确认', '确认取消所选记录的对照关系吗？', function (btn) {
                    if (btn) {
                        askService('delete', entity);//请求服务器

                        //动态改变原始表数据
                        for(var x=0;x<labRows.length;x++){
                            if(leftRow.LabFolkNo===labRows[x]['LabFolkNo']){
                                labRows[x]['CenterCName']=null;
                                labRows[x]['FolkNo']=null;
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
                if(leftRow.FolkNo==centerRows[i].FolkNo){
                    rightIndex = 0;
                    leftIndex=$('#leftFolk').datagrid('getRowIndex', leftRow);
                    centerFolkNo=leftRow.FolkNo;

                    var entity =  'id='+LabCode+'_' +centerFolkNo+'_'+ leftRow.LabFolkNo;
                    $.messager.confirm('确认', '确认取消所选记录的对照关系吗？', function (btn) {
                        if (btn) {
                            askService('delete', entity);//请求服务器
                        }
                    });

                    //动态改变原始表数据
                    for(var x=0;x<labRows.length;x++){
                        if(leftRow.LabFolkNo===labRows[x]['LabFolkNo']){
                            labRows[x]['CenterCName']=null;
                            labRows[x]['FolkNo']=null;
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
    var rightRows=$('#rightFolk').datagrid('getRows') || [],
        rightLength=rightRows.length || 0,
        leftRows=$('#leftFolk').datagrid('getRows') || [],
        leftLength=leftRows.length || 0,
        match=false,
        rows=[];

    for(var i=0;i<leftLength;i++){
        for(var j=0;j<rightLength;j++){
            if(leftRows[i].FolkNo==rightRows[j].FolkNo){
                break;
            }
            else if(leftRows[i].CName===rightRows[j].CName || (leftRows[i].ShortCode && leftRows[i].ShortCode===rightRows[j].ShortCode)){
                match=true;
                leftIndex=$('#leftFolk').datagrid('getRowIndex', leftRows[i]);
                centerFolkNo=rightRows[j].FolkNo;
                centerName=rightRows[j].CName;
                rightIndex=$('#rightFolk').datagrid('getRowIndex', rightRows[j]);
                labFolkNo=leftRows[i].LabFolkNo;

                leftRows[i].leftIndex=leftIndex;
                leftRows[i].LabFolkNo=labFolkNo;
                leftRows[i].rightIndex=rightIndex;
                leftRows[i].CenterFolkNo=centerFolkNo;
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
        $('#dlgFolk').datagrid('loadData',{total:rows.length,rows:rows});
    }else{
        $.messager.alert('提示','当前数据已对照完成或没有数据','info');
    }
}

//保存
function save() {
    var rows=$('#dlgFolk').datagrid('getRows') || [],
        length=rows.length;
    for(var i=0;i<length;i++){
        leftIndex=rows[i].leftIndex;
        labFolkNo=rows[i].LabFolkNo;
        rightIndex=rows[i].rightIndex;
        centerFolkNo=rows[i].CenterFolkNo;
        centerName=rows[i].CenterCName;
        var entity = '{"jsonentity":{' + '"FolkNo":' + centerFolkNo + ',"ControlLabNo":' + LabCode + ',"ControlFolkNo":' +labFolkNo + '}}';
        askService('edit', entity);//请求服务器
    }
    $('#dlg').dialog('close');
}

//取消
function cancel() {
    $('#dlg').dialog('close');
}