/**
 * Created by Administrator on 2015/1/13.
 */

//颜色列表全部数据
var listColors;
//已选样本类型
var checkedSample;
//全部样本类型数据
var allSample;
//已勾选的样本
var SampleTypeNoList=[];
//当前所选颜色
var currentColorValue;

$(function(){

    askService('loadSample',null,{tablename:'SampleType',page:1,rows:100});

    //初始化颜色列表
    $('#tbColor').datagrid({
        title: '颜色列表',
        url:Shell.util.Path.rootPath+'/ServiceWCF/DictionaryService.svc/GetAllItemColorDict',
        method: 'GET',
        loadMsg: '数据加载...',
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
                listColors=(!listColors)?list.rows || []:listColors;
                return result;
            }
        },
        onLoadSuccess:function(data){
            $(this).datagrid('selectRow',0);
            var row=$(this).datagrid('getSelected'),
                colorID=row.ColorID;

            askService('loadControl',null,colorID);
        },
        onClickRow:function(index,row){
            var colorID=row.ColorID;
            askService('loadControl',null,colorID);
        }
    });
});

//创建实验室数据列
function createColumns() {
    var columns = [
        [
            {field: 'ColorID', title: '颜色编号', width: fixWidth(0.2),hidden:true },
            {field: 'ColorName', title: '颜色名', width: fixWidth(0.2)},
            {field: 'ColorValue', title: '颜色值', width: fixWidth(0.2),
                formatter: function (value, row) {
                    var colValue = '<div style="display:inline;float:left;margin:3px;background-color:' +
                        row.ColorValue + ';width:12px;height:12px;"' + '></div>' + row.ColorValue;
                    return colValue;}}
        ]
    ];
    return columns;
}

//设置列宽
function fixWidth(percent) {
    return document.body.clientWidth * percent;
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
                        var checkedLength=SampleTypeNoList.length || 0,
                            allLength=allSample.length || 0;

                        //清空颜色
                        for(var j=0;j<allLength;j++){
                            $('#div'+allSample[j].SampleTypeNo).css("background","");
                        }
                        //重置颜色
                        for(var i=0;i<checkedLength;i++){
                            $('#div'+SampleTypeNoList[i]).css("background-color",currentColorValue);
                        }
                        break;
                    case 'loadControl':
                        var data=eval('('+data.ResultDataValue+')') || {};
                        showChecked(data);
                        break;
                    case 'loadSample':
                        var data=eval('('+data.ResultDataValue+')') || {};
                        allSample=data.rows;
                        createTable(allSample);
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
            serviceParam.serviceName = 'DeleteLabDepartmentModelByID';//删除数据服务名
            serviceParam.type = 'GET';//数据请求方式GET
            serviceParam.data =  where;//发送到服务器的数据，根据ID号进行删除
            break;
        case'add':
            serviceParam.serviceName = 'AddLabDepartmentModel';//增加数据服务名
            serviceParam.type = 'POST';//数据请求方式GET
            break;
        case'edit':
            serviceParam.serviceName = 'SaveToItemColorAndSampleTypeDetail';//编辑数据服务名UpdateReportGroupModelByID
            serviceParam.type = 'POST';//数据请求方式GET
            break;
        case'loadSample':
            serviceParam.serviceName = 'GetPubDict';//修改组套关系服务名UpdateGroupItemModelByID
            serviceParam.type = 'GET';//数据请求方式GET
            serviceParam.data = where;//发送到服务器的数据，根据ID号进行删除
            break;
        case'loadControl':
            serviceParam.serviceName = 'GetItemColorAndSampleDetail';//修改组套关系服务名UpdateGroupItemModelByID
            serviceParam.type = 'GET';//数据请求方式GET
            serviceParam.data = {ColorID:where};//发送到服务器的数据，根据ID号进行删除
            break;
    }

    return serviceParam;
}

//查询
function colorSearch(value){
    if(!value){
        $('#tbColor').datagrid('loadData',{success: true, ResultDataValue: Shell.util.JSON.encode({total: listColors.length, rows: listColors})});
    }else{
        var filterRows=[],
            length=listColors.length;

        value=value.trim();
        for(var i=0;i<length;i++){
            if((listColors[i]['ColorID']).toString().indexOf(value)>-1 || (listColors[i]['ColorName'] && (listColors[i]['ColorName']).toString().indexOf(value)>-1)){
                filterRows.push(listColors[i]);
            }
        }
        $('#tbColor').datagrid('loadData',{success: true, ResultDataValue: Shell.util.JSON.encode({total: filterRows.length, rows: filterRows})});
    }
}

//动态创建列表
function createTable(rows){
    var table="",
        length=rows.length,
        columns=3;

    if(length){
        table='<table style="width: 100%;">'
        for(var i=0;i<length;i++){
                if(i%columns==0){
                    table+='<tr><td>'+'<input type="checkbox" id='+rows[i].SampleTypeNo+' value='+rows[i].SampleTypeNo+'>'+rows[i].CName+
                    '<div id=div'+rows[i].SampleTypeNo+' style="float:left;margin:3px;width:12px;height:12px;"' + '></div></td>'
                }else{
                    table+='<td>'+'<input type="checkbox" id='+rows[i].SampleTypeNo+' value='+rows[i].SampleTypeNo+'>'+rows[i].CName+
                    '<div id=div'+rows[i].SampleTypeNo+' style="float:left;margin:3px;width:12px;height:12px;"' + '></div></td>'
                }
            if(i==length-1 || i%columns==columns-1){
                table+='</tr>'
            }
        }
        table+='</table>'
    }
    document.getElementById('tbSample').innerHTML=table;
}

//显示存在对照关系的样本
function showChecked(list){
    var SampleTypeNoList=list.SampleTypeNoList || [],
        length=SampleTypeNoList.length,
        listLength=allSample.length || 0,
        row=$('#tbColor').datagrid('getSelected') || [];

    currentColorValue=row.ColorValue;
    //先清空复选框
    for(var i=0;i<listLength;i++){
        $('#'+allSample[i].SampleTypeNo).removeAttr('checked');
        $('#div'+allSample[i].SampleTypeNo).css("background","");
    }

    for(var j=0;j<length;j++){
        $('#'+SampleTypeNoList[j]).prop('checked',true);
        $('#div'+SampleTypeNoList[j]).css("background-color",currentColorValue);
    }
}

//保存
function save(){
    var row=$('#tbColor').datagrid('getSelected'),
        colorID=row.ColorID,
        colorName=row.ColorName,
        length=allSample.length || 0;

    SampleTypeNoList=[];
    for(var i=0;i<length;i++){
        if($('#'+allSample[i].SampleTypeNo).is(':checked')){
            SampleTypeNoList.push(allSample[i].SampleTypeNo);
        }
    }

    var entity = '{"jsonentity":{' + '"UiItemColorSampleTypeNo":[{'+
        '"ColorId":' + colorID + ',"ColorName":"' + colorName + '","SampleTypeNoList":[' +SampleTypeNoList + ']}]}}';
    askService('edit', entity);//请求服务器
}



