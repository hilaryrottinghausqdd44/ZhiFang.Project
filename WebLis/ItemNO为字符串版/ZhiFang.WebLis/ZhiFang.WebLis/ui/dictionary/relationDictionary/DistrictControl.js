/**
 *@OverView 病区字典表对照关系表
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
//中心病区编码
var centerDistrictNo;
//中心病区名称
var centerName;
//实验室病区编码
var labDistrictNo;
//首次触发单选按钮
var onClick=0;
//实验室对照列表
var labRows;
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

    //初始化实验室病区字典表对照关系表
    $('#leftDistrict').datagrid({
        title: '病区字典表对照关系表',
        url:Shell.util.Path.rootPath+'/ServiceWCF/DictionaryService.svc/GetPubDict',
        queryParams:{
            tablename:"B_Lab_DistrictControl",
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
            var rightRows=$('#rightDistrict').datagrid('getRows'),
                length=rightRows.length;

            if(matchRow){
                matchRow.css("background-color",'');
            }
            for(var i=0;i<length;i++){
                if(row.DistrictNo==rightRows[i].DistrictNo){
                    $('#datagrid-row-r2-2-'+i).css("background-color","#4afffd");
                    matchRow= $('#datagrid-row-r2-2-'+i);
                    break;
                }
            }
        }
    });

    //初始化中心病区字典列表
    $('#rightDistrict').datagrid({
        title: '中心病区列表',

        url:Shell.util.Path.rootPath+'/ServiceWCF/DictionaryService.svc/GetPubDict',
        queryParams:{
            tablename:"District",
            fields:'DistrictNo,ShortCode,CName'
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

    //初始化病区模糊匹配列表
    $('#tbMatch').datagrid({
        method:'GET',
        loadMsg:'数据加载...',
        fitColumns:true,
        singleSelect:true,
        striped:true,
        fit:true,
        columns:matchColumns(),
        onDblClickRow:function(index,matchRow){
            var row = $('#rightDistrict').datagrid('getSelected') || "";

            leftIndex=$('#leftDistrict').datagrid('getRowIndex',matchRow);

            if (row.CName === matchRow.CName) {
                labDistrictNo=row.LabDistrictNo;
                rightIndex = $('#rightDistrict').datagrid('getRowIndex', row);
                centerDistrictNo=matchRow.DistrictNo;
                var entity = '{"jsonentity":{' + '"DistrictNo":' + centerDistrictNo + ',"ControlLabNo":' + LabCode + ',"ControlDistrictNo":' +labDistrictNo + '}}';
                askService('edit', entity);//请求服务器
            }
            $('#dlgMatch').dialog('close');
        }
    });

    //初始化智能对照成功的列表
    $('#dlgDistrict').datagrid({
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
            {field: 'LabDistrictNo', title: '实验室病区编号', width: fixWidth(0.2) },
            {field: 'CName', title: '实验室病区名称', width: fixWidth(0.3)},
            {field: 'ShortCode', title: '实验室病区简码', width: fixWidth(0.2)},
            {field: 'DistrictNo', title: '中心病区编码', width: fixWidth(0.2)},
            {field: 'CenterCName', title: '中心病区名称', width: fixWidth(0.3)}
        ]
    ];
    return columns;
}

//创建中心数据列
function matchColumns() {
    var columns = [
        [
            {field: 'DistrictNo', title: '中心病区编号', width: fixWidth(0.2)},
            {field: 'ShortCode', title: '中心病区简码', width: fixWidth(0.2)},
            {field: 'CName', title: '中心病区名称', width: fixWidth(0.3)}
        ]
    ];
    return columns;
}

//创建对话框数据列
function dlgColumns() {
    var columns = [
        [
            {field: 'LabDistrictNo', title: '实验室病区编号', width: fixWidth(0.2) },
            {field: 'CName', title: '实验室病区名称', width: fixWidth(0.2)},
            {field: 'ShortCode', title: '实验室病区简码', width: fixWidth(0.2)},
            {field: 'CenterDistrictNo', title: '中心病区编码', width: fixWidth(0.2)},
            {field: 'CenterCName', title: '中心病区名称', width: fixWidth(0.2)}
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
        tablename:"B_Lab_DistrictControl",
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
                        $('#leftDistrict').datagrid('selectRow', leftIndex);
                        $('#rightDistrict').datagrid('selectRow', rightIndex);

                        $('#leftDistrict').datagrid('updateRow', {
                            index: leftIndex,
                            row: {DistrictNo: centerDistrictNo,CenterCName: centerName}
                        });
                        break;
                    case'delete':
                        $('#leftDistrict').datagrid('updateRow', {
                            index: leftIndex,
                            row: {DistrictNo: null,CenterCName: null}
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
            serviceParam.serviceName = 'UpdateOrAddDistrictControlModelByID';//增加数据服务名
            serviceParam.type = 'POST';//数据请求方式GET
            serviceParam.data = entity;//向服务器传递的数据
            break;
        case'edit':
            serviceParam.serviceName = 'UpdateOrAddDistrictControlModelByID';//编辑数据服务名
            serviceParam.type = 'POST';//数据请求方式POST
            serviceParam.data = entity;//向服务器传递的数据
            break;
        case'delete':
            serviceParam.serviceName = 'DelDistrictControlModelByID';//编辑数据服务名
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
        $('#leftDistrict').datagrid('load',{
                tablename:"B_Lab_DistrictControl",
                selectedflag:labContrast,
                labcode:LabCode,
                time:new Date().getTime()}
        );
    }

    if(matchRow){
        matchRow.css("background-color",'');
    }
    var rows=$('#rightDistrict').datagrid('getSelections') || [];
    if(rows.length)
        $('#rightDistrict').datagrid('unselectAll');
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
            $('#leftDistrict').datagrid('load',{
                    tablename:"B_Lab_DistrictControl",
                    selectedflag:labContrast,
                    labcode:LabCode,
                    time:new Date().getTime()}
            );
        }
        var rows=$('#rightDistrict').datagrid('getSelections') || [];
        if(rows.length)
            $('#rightDistrict').datagrid('unselectAll');
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
        $('#leftDistrict').datagrid('load',{
                tablename:"B_Lab_DistrictControl",
                selectedflag:labContrast,
                labcode:LabCode,
                time:new Date().getTime()}
        );
    }

    if(matchRow){
        matchRow.css("background-color",'');
    }
    var rows=$('#rightDistrict').datagrid('getSelections') || [];
    if(rows.length)
        $('#rightDistrict').datagrid('unselectAll');
}

//实验室字典查询
function labSearch(value) {
    $('#leftDistrict').datagrid('load', {filervalue: value.trim(),labcode:LabCode,tablename:"B_Lab_DistrictControl",selectedflag:labContrast,time:new Date().getTime()});
    if(matchRow){
        matchRow.css("background-color",'');
    }
}

//中心字典查询
function centerSearch(value) {
    if(!value){
        $('#rightDistrict').datagrid('loadData',{success: true, ResultDataValue: Shell.util.JSON.encode({total: centerRows.length, rows: centerRows})});
    }else{
        var filterRows=[],
            length=centerRows.length;

        value=value.trim();
        for(var i=0;i<length;i++){
            if((centerRows[i]['DistrictNo']).toString().indexOf(value)>-1 || (centerRows[i]['CName'] && (centerRows[i]['CName']).toString().indexOf(value)>-1 )||(centerRows[i]['ShortCode'] && (centerRows[i]['ShortCode']).toString().indexOf(value)>-1)){
                filterRows.push(centerRows[i]);
            }
        }
        $('#rightDistrict').datagrid('loadData',{success: true, ResultDataValue: Shell.util.JSON.encode({total: filterRows.length, rows: filterRows})});
    }
}

//对照
function matchSingle() {
    var rightRow = $('#rightDistrict').datagrid('getSelected') || "",
        rightRows = $('#rightDistrict').datagrid('getRows') || [],
        rightLength=rightRows.length,
        leftRow = $('#leftDistrict').datagrid('getSelected') || "",
        leftRows=$('#leftDistrict').datagrid('getRows') || [],
        leftLength=leftRows.length,
        leftExist=false,
        rightExit=false;

    if(!rightRow || !leftRow){
        $.messager.alert('提示','请选择要进行对照的记录','info');
        return;
    }

    //当前中心列表数据
    for(var i=0;i<rightLength;i++){
        if(leftRow.DistrictNo==rightRows[i].DistrictNo){

            leftIndex= $('#leftDistrict').datagrid('getRowIndex', leftRow);
            //先解除左边的对照关系
            var delID =  'id='+LabCode+'_' +leftRow.DistrictNo+'_'+ leftRow.LabDistrictNo;
            askService('delete', delID);//请求服务器

            //动态改变原始表数据
            for(var x=0;x<labRows.length;x++){
                if(leftRow.LabDistrictNo===labRows[x]['LabDistrictNo']){
                    labRows[x]['CenterCName']=null;
                    labRows[x]['DistrictNo']=null;
                    break;
                }
            }

            //当前实验室对照关系列表数据
            for(var j=0;j<leftLength;j++){
                if(leftRows[j].DistrictNo===rightRow.DistrictNo){
                    leftIndex= $('#leftDistrict').datagrid('getRowIndex', leftRows[j]);
                    //先解除右边的对照关系
                    var delID =  'id='+LabCode+'_' +leftRows[j].DistrictNo+'_'+ leftRows[j].LabDistrictNo;
                    askService('delete', delID);//请求服务器

                    //动态改变原始表数据
                    for(var x=0;x<labRows.length;x++){
                        if(leftRows[j].LabDistrictNo===labRows[x]['LabDistrictNo']){
                            labRows[x]['CenterCName']=null;
                            labRows[x]['DistrictNo']=null;
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
                    if(labRows[k].DistrictNo===rightRow.DistrictNo){
                        leftIndex= 0;
                        //先解除右边的对照关系
                        var delID =  'id='+LabCode+'_' +labRows[k].DistrictNo+'_'+ labRows[k].LabDistrictNo;
                        askService('delete', delID);//请求服务器

                        //动态改变原始表数据
                        labRows[k]['CenterCName']=null;
                        labRows[k]['DistrictNo']=null;
                        leftExist=true;
                        break;
                    }
                }
            }
            leftIndex= $('#leftDistrict').datagrid('getRowIndex', leftRow);
            centerDistrictNo=rightRow.DistrictNo;
            centerName=rightRow.CName;
            rightIndex = $('#rightDistrict').datagrid('getRowIndex', rightRow);
            labDistrictNo=leftRow.LabDistrictNo;

            //可能还需要传所有的字段
            var entity = '{"jsonentity":{' + '"DistrictNo":' + centerDistrictNo + ',"ControlLabNo":' + LabCode + ',"ControlDistrictNo":' +labDistrictNo + '}}';
            askService('edit', entity);//请求服务器

            //动态改变原始表数据
            for(var x=0;x<labRows.length;x++){
                if(labDistrictNo===labRows[x]['LabDistrictNo']){
                    labRows[x]['CenterCName']=centerName;
                    labRows[x]['DistrictNo']=centerDistrictNo;
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
            if(leftRow.DistrictNo==centerRows[i].DistrictNo){

                leftIndex= $('#leftDistrict').datagrid('getRowIndex', leftRow);
                //先解除左边的对照关系
                var delID =  'id='+LabCode+'_' +leftRow.DistrictNo+'_'+ leftRow.LabDistrictNo;
                askService('delete', delID);//请求服务器

                //动态改变原始表数据
                for(var x=0;x<labRows.length;x++){
                    if(leftRow.LabDistrictNo===labRows[x]['LabDistrictNo']){
                        labRows[x]['CenterCName']=null;
                        labRows[x]['DistrictNo']=null;
                        break;
                    }
                }

                //当前实验室对照关系列表数据
                for(var j=0;j<leftLength;j++){
                    if(leftRows[j].DistrictNo===rightRow.DistrictNo){
                        leftIndex= $('#leftDistrict').datagrid('getRowIndex', leftRows[j]);
                        //先解除右边的对照关系
                        var delID =  'id='+LabCode+'_' +leftRows[j].DistrictNo+'_'+ leftRows[j].LabDistrictNo;
                        askService('delete', delID);//请求服务器

                        //动态改变原始表数据
                        for(var x=0;x<labRows.length;x++){
                            if(leftRows[j].LabDistrictNo===labRows[x]['LabDistrictNo']){
                                labRows[x]['CenterCName']=null;
                                labRows[x]['DistrictNo']=null;
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
                        if(labRows[k].DistrictNo===rightRow.DistrictNo){
                            leftIndex= 0;
                            //先解除右边的对照关系
                            var delID =  'id='+LabCode+'_' +labRows[k].DistrictNo+'_'+ labRows[k].LabDistrictNo;
                            askService('delete', delID);//请求服务器

                            //动态改变原始表数据
                            labRows[k]['CenterCName']=null;
                            labRows[k]['DistrictNo']=null;
                            leftExist=true;
                            break;
                        }
                    }
                }

                centerDistrictNo=rightRow.DistrictNo;
                centerName=rightRow.CName;
                rightIndex = 0;
                labDistrictNo=leftRow.LabDistrictNo;

                //可能还需要传所有的字段
                var entity = '{"jsonentity":{' + '"DistrictNo":' + centerDistrictNo + ',"ControlLabNo":' + LabCode + ',"ControlDistrictNo":' +labDistrictNo + '}}';
                askService('edit', entity);//请求服务器

                //动态改变原始表数据
                for(var x=0;x<labRows.length;x++){
                    if(labDistrictNo===labRows[x]['LabDistrictNo']){
                        labRows[x]['CenterCName']=centerName;
                        labRows[x]['DistrictNo']=centerDistrictNo;
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
            if(leftRows[i].DistrictNo==rightRow.DistrictNo){

                leftIndex= $('#leftDistrict').datagrid('getRowIndex', leftRows[i]);
                //先解除右边的对照关系
                var delID =  'id='+LabCode+'_' +leftRows[i].DistrictNo+'_'+ leftRows[i].LabDistrictNo;
                askService('delete', delID);//请求服务器

                //动态改变原始表数据
                for(var x=0;x<labRows.length;x++){
                    if(leftRows[i].LabDistrictNo===labRows[x]['LabDistrictNo']){
                        labRows[x]['CenterCName']=null;
                        labRows[x]['DistrictNo']=null;
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
                if(labRows[j].DistrictNo==rightRow.DistrictNo){

                    leftIndex= 0;
                    //先解除右边的对照关系
                    var delID =  'id='+LabCode+'_' +labRows[j].DistrictNo+'_'+ labRows[j].LabDistrictNo;
                    askService('delete', delID);//请求服务器

                    //动态改变原始表数据
                    labRows[j]['CenterCName']=null;
                    labRows[j]['DistrictNo']=null;
                    leftExist=true;
                    break;
                }
            }
        }

        leftIndex = $('#leftDistrict').datagrid('getRowIndex', leftRow);
        labDistrictNo=leftRow.LabDistrictNo;
        rightIndex = $('#rightDistrict').datagrid('getRowIndex', rightRow);
        centerDistrictNo=rightRow.DistrictNo;
        centerName=rightRow.CName;

        //可能还需要传所有的字段
        var entity = '{"jsonentity":{' + '"DistrictNo":' + centerDistrictNo + ',"ControlLabNo":' + LabCode + ',"ControlDistrictNo":' +labDistrictNo + '}}';
        askService('edit', entity);//请求服务器

        //动态改变原始表数据
        for(var x=0;x<labRows.length;x++){
            if(labDistrictNo===labRows[x]['LabDistrictNo']){
                labRows[x]['CenterCName']=centerName;
                labRows[x]['DistrictNo']=centerDistrictNo;
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
    var leftRow = $('#leftDistrict').datagrid('getSelected') || "",
        rightRows=$('#rightDistrict').datagrid('getRows') || [],
        length=rightRows.length,
        exit=false;

    if (leftRow.length == 0) {
        $.messager.alert('提示信息', '请选择记录', 'info');
    } else {
        if(leftRow.DistrictNo!==0 && !leftRow.DistrictNo){
            $.messager.alert('提示','此条记录已经处于未对照状态','info');
            return;
        }

        //匹配的记录显示在当前页的情况
        for(var i=0;i<length;i++){
            if(leftRow.DistrictNo==rightRows[i].DistrictNo){
                rightIndex = $('#rightDistrict').datagrid('getRowIndex', rightRows[i]);
                leftIndex=$('#leftDistrict').datagrid('getRowIndex', leftRow);
                centerDistrictNo=leftRow.DistrictNo;

                var entity =  'id='+LabCode+'_' +centerDistrictNo+'_'+ leftRow.LabDistrictNo;
                $.messager.confirm('确认', '确认取消所选记录的对照关系吗？', function (btn) {
                    if (btn) {
                        askService('delete', entity);//请求服务器

                        //动态改变原始表数据
                        for(var x=0;x<labRows.length;x++){
                            if(leftRow.LabDistrictNo===labRows[x]['LabDistrictNo']){
                                labRows[x]['CenterCName']=null;
                                labRows[x]['DistrictNo']=null;
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
                if(leftRow.DistrictNo==centerRows[i].DistrictNo){
                    rightIndex = 0;
                    leftIndex=$('#leftDistrict').datagrid('getRowIndex', leftRow);
                    centerDistrictNo=leftRow.DistrictNo;

                    var entity =  'id='+LabCode+'_' +centerDistrictNo+'_'+ leftRow.LabDistrictNo;
                    $.messager.confirm('确认', '确认取消所选记录的对照关系吗？', function (btn) {
                        if (btn) {
                            askService('delete', entity);//请求服务器
                        }
                    });

                    //动态改变原始表数据
                    for(var x=0;x<labRows.length;x++){
                        if(leftRow.LabDistrictNo===labRows[x]['LabDistrictNo']){
                            labRows[x]['CenterCName']=null;
                            labRows[x]['DistrictNo']=null;
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
    var rightRows=$('#rightDistrict').datagrid('getRows') || [],
        rightLength=rightRows.length || 0,
        leftRows=$('#leftDistrict').datagrid('getRows') || [],
        leftLength=leftRows.length || 0,
        match=false,
        rows=[];

    for(var i=0;i<leftLength;i++){
        for(var j=0;j<rightLength;j++){
            if(leftRows[i].DistrictNo==rightRows[j].DistrictNo){
                break;
            }
            else if(leftRows[i].CName===rightRows[j].CName || (leftRows[i].ShortCode && leftRows[i].ShortCode===rightRows[j].ShortCode)){
                match=true;
                leftIndex=$('#leftDistrict').datagrid('getRowIndex', leftRows[i]);
                centerDistrictNo=rightRows[j].DistrictNo;
                centerName=rightRows[j].CName;
                rightIndex=$('#rightDistrict').datagrid('getRowIndex', rightRows[j]);
                labDistrictNo=leftRows[i].LabDistrictNo;

                leftRows[i].leftIndex=leftIndex;
                leftRows[i].LabDistrictNo=labDistrictNo;
                leftRows[i].rightIndex=rightIndex;
                leftRows[i].CenterDistrictNo=centerDistrictNo;
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
        $('#dlgDistrict').datagrid('loadData',{total:rows.length,rows:rows});
    }else{
        $.messager.alert('提示','当前数据已对照完成或没有数据','info');
    }
}

//保存
function save() {
    var rows=$('#dlgDistrict').datagrid('getRows') || [],
        length=rows.length;
    for(var i=0;i<length;i++){
        leftIndex=rows[i].leftIndex;
        labDistrictNo=rows[i].LabDistrictNo;
        rightIndex=rows[i].rightIndex;
        centerDistrictNo=rows[i].CenterDistrictNo;
        centerName=rows[i].CenterCName;
        var entity = '{"jsonentity":{' + '"DistrictNo":' + centerDistrictNo + ',"ControlLabNo":' + LabCode + ',"ControlDistrictNo":' +labDistrictNo + '}}';
        askService('edit', entity);//请求服务器
    }
    $('#dlg').dialog('close');
}

//取消
function cancel() {
    $('#dlg').dialog('close');
}