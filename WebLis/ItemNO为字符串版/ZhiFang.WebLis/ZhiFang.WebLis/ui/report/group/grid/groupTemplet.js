/**
 * @OverView 小组报告模板
 * Created by gwh on 14-11-21.
 */

//dataGrid表对象
var $obj;
//按钮类型
var btnType = null;

//JQuery入口
$(function () {

    //初始化dataGrid数据列表
    $obj = $('#tb').datagrid({
        title: '小组报告模板设定',
        url:Shell.util.Path.rootPath+'/ServiceWCF/DictionaryService.svc/GetAllReportGroupModelSet',
        method:'GET',
        loadMsg: '数据加载中...',
        pagination: true,
        pagePosition: 'bottom',
        fitColumns: true,
        checkOnSelect: true,
        striped: true,
        fit: true,
        rownumbers: true,
        border: false,
        singleSelect: false,

        idField: 'Id',
        toolbar: '#topBars',
        columns: createColumns(),
        loadFilter: function (data) {
            if (data.success) {
                var list = data.list || {},
                    result = {};
                result.total = list.count || 0;
                result.rows = list.list || [];
                return result;
            }
        }
    });

    $('#txtSearch').searchbox('setValue',null);
});

//$.ajax()请求服务
function askService(serviceType, where,entity) {
    var proUrl = Shell.util.Path.rootPath,
        localData = {},//把返回来的数据格式转换成标准格式{'total':1,'rows':[]}
        serviceParam = {},//请求服务参数
        async=serviceType=='delete'?false:true;//删除操作，同步执行$.ajax方法

    if(serviceType=='add' || serviceType=='edit'){
        serviceParam.data=entity;
    }
    serviceParam = setService(serviceType, serviceParam, where);//配置服务参数
    $.ajax({
        url: proUrl + '/ServiceWCF/DictionaryService.svc/' + serviceParam.serviceName,
        data: serviceParam.data,
        dataType: 'json',
        type: serviceParam.type,
        timeout: 10000,
        async:async,
        cache:false,
        contentType: 'application/json',//不加这个会出现错误
        success: function (data) {
            if (data.success) {
                switch (serviceType) {
                    case'delete':
                    case'add':
                    case'edit':
                        var currentTime=new Date().getTime();
                        $('#tb').datagrid('load',{itemkey:null,time:currentTime});//增删改成功后，重新加载数据请求
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
function setService(serviceType, serviceParam, recordID) {

    //数据请求方式（GET,POST）
    switch (serviceType) {
        case 'load':
            serviceParam.serviceName = 'GetAllReportGroupModelSet';//加载数据服务名
            serviceParam.type = 'GET';//数据请求方式GET
            serviceParam.data = {};//发送到服务器的数据
            break;
        case'search':
            serviceParam.serviceName = 'GetAllReportGroupModelSet';//查询数据服务名
            serviceParam.type = 'GET';//数据请求方式GET
            serviceParam.data = {};//发送到服务器的数据,根据ID号查询（这里基本没用，没人记得ID号）
            break;
        case'delete':
            serviceParam.serviceName = 'DelReportGroupModel';//删除数据服务名
            serviceParam.type = 'GET';//数据请求方式GET
            serviceParam.data = {Id: recordID};//发送到服务器的数据，根据ID号进行删除
            break;
        case'add':
            serviceParam.serviceName = 'AddReportGroupModel';//增加数据服务名
            serviceParam.type = 'POST';//数据请求方式GET
            break;
        case'edit':
            serviceParam.serviceName = 'UpdateReportGroupModelByID';//编辑数据服务名UpdateReportGroupModelByID
            serviceParam.type = 'POST';//数据请求方式GET
            break;
    }

    return serviceParam;
}

//获取数据实体（增加，编辑）,形如：{entity:{ID:1,name:'张珊'}}
function getEntity(getType) {
    var result = '',
        key,
        value,
        cmbData,
        cmbLength,//下拉框长度
        textFlag,//文本框的值是否存在下拉框
        flag = 0,//记录第一个字段
        opts,//下拉框属性
        nullFlag= 0,
        columns = $obj.datagrid('getColumnFields'),
        length = columns.length;

    result += '{"jsonentity":{';//构造符合格式的字符串
    for (var i = 0; i < length; i++) {
        if (columns[i] !== 'chk' && columns[i] != 'opt' && columns[i]!='PrintFormatNo' &&columns[i]!='ClientNo' && columns[i]!='SpecialtyItemNo') {//排除复选列和操作列和三个中文列
            if (getType == 'add' && columns[i] == 'Id')//当服务请求类型为增加的时候，不需要提供id
                continue;
            key = columns[i];//获取字段
            var cmbFlag = $('#cmb' + key).attr('type');//若为combobox类型
            if (cmbFlag == 'cmb') {
                if(key=='PrintFormatName'){
                    value = $('#cmb' + key).combobox('getValue');//给后台提供value值
                    opts=$('#cmb' + key).combobox('options');
                    cmbData=$('#cmb' + key).combobox('getData');
                    cmbLength=cmbData.length;
                    for(var j=0;j<cmbLength;j++){
                        if(textFlag=(cmbData[j][opts.valueField]==value))
                        break;
                    }
                    if(textFlag){
                        key='PrintFormatNo';//模板名称绑定的字典索引号是Id,这里不能直接用Id,会和dataGrid表的Id冲突
                    }else{
                        nullFlag++;
                        continue;//如果输入的值不在下拉框内，则不向后台传递此属性
                    }
                }else if(key=='SpecialtyItemName'){
                    value = $('#cmb' + key).combobox('getValue');//给后台提供value值
                    var  text = $('#cmb' + key).combobox('getText');
                    opts=$('#cmb' + key).combobox('options');
                    cmbData=$('#cmb' + key).combobox('getData');
                    cmbLength=cmbData.length;
                    if(text==null || text==''){
                        continue;
                    }else{
                        for(var k=0;k<cmbLength;k++){
                            if(textFlag=(cmbData[k][opts.valueField]==value))
                                break;
                        }
                        if(textFlag){
                            key='SpecialtyItemNo';//特殊项目字典号
                        }else{
                            nullFlag++;
                            continue;
                        }
                    }
                }else if(key=='ClientName'){
                    value = $('#cmb' + key).combobox('getValue');//给后台提供value值
                    var  text = $('#cmb' + key).combobox('getText');
                    opts=$('#cmb' + key).combobox('options');
                    cmbData=$('#cmb' + key).combobox('getData');
                    cmbLength=cmbData.length;
                    if(text==null || text==''){
                        continue;
                    }else {
                        for (var n = 0; i < cmbLength; n++) {
                            if (textFlag = (cmbData[n][opts.valueField] == value))
                                break;
                        }
                        if (textFlag) {
                            key = 'ClientNo';//送检单位字典号
                        } else {
                            nullFlag++;
                            continue;
                        }
                    }
                }
                else {
                    value = $('#cmb' + key).combobox('getValue');//给后台提供value值
                    opts=$('#cmb' + key).combobox('options');
                    cmbData=$('#cmb' + key).combobox('getData');
                    cmbLength=cmbData.length;
                    for(var m=0;m<cmbLength;m++){
                        if(textFlag=(cmbData[m][opts.valueField]==value))
                            break;
                    }
                    if(textFlag){
                        key = opts.valueField;//取出value字段名称
                    }else{
                        nullFlag++;
                        continue;
                    }
                }

            } else {
                value = $("input[name=" + key + "]").val();//获取form表中input的值
                if(!value){
                    nullFlag++;
                    continue;
                }
            }
            flag++;
            if (flag == 1) {
                result += '"' + key + '":' + value;//添加第一个字段
            } else {
                result += ',"' + key + '":' + value;//构造符合格式的字符串，形如：'{"entity":{"SectionName":"group01","PrintFormatName":"模板名称01"}}'
            }
        }
    }
    result += '}}';
    if(nullFlag>0){
        result=nullFlag;
    }
     return result;
}

//设置列宽
function fixWidth(percent) {
    return document.body.clientWidth * percent;
}

//创建数据列
function createColumns() {

    var columns = [
        [
            {field: 'chk', checkbox: true, hidden: false},
            {field: 'Id', title: '编号', hidden: false, width: fixWidth(0.2) },
            {field: 'SectionName', title: '小组名称', width: fixWidth(0.2)},
            {field: 'PrintFormatName', title: '模板名称', width: fixWidth(0.2)},
            {field:'PrintFormatNo',title:'模板名称索引号',width:fixWidth(0.2),hidden:true},
            {field: 'ClientName', title: '送检单位', width: fixWidth(0.2)},
            {field:'ClientNo',title:'送检单位索引号',width:fixWidth(0.2),hidden:true},
            {field: 'SpecialtyItemName', title: '特殊项目', width: fixWidth(0.2)},
            {field:'SpecialtyItemNo',title:'特殊项目索引号',width:fixWidth(0.2),hidden:true},
            {field: 'ItemMinNumber', title: '项目或图片数下限', width: fixWidth(0.2)},
            {field: 'ItemMaxNumber', title: '项目或图片数上限', width: fixWidth(0.2)},
            {field: 'Sort', title: '优先级别', width: fixWidth(0.2)},
            {field: 'UseFlag', title: '使用标志', width: fixWidth(0.2),
                formatter:function(value,row){
                var useCName=row.UseFlag?'是':'否';
                return useCName;
            }},

            {field: 'ImageFlag', title: '是否带图', hidden: true},
            {field: 'AntiFlag', title: '是否有抗生素', hidden: true},
            {field: 'BatchPrint', title: '是否套打', hidden: true},
            {field: 'SickTypeNo', title: '就诊类型',  width: fixWidth(0.2),
                formatter:function(value,row){
                    var SickType;
                    switch (row.SickTypeNo) {
                        case null:
                        case 0:
                            SickType = '无';
                            break;
                        case 1:
                            SickType = '住院';
                            break;
                        case 2:
                            SickType = '门诊';
                            break;
                        case 3:
                            SickType = '体检';
                            break;
                        case 4:
                            SickType = '外送';
                            break;
                    }
                    return SickType;
                }},

            {field: 'ModelTitleType', title: '模板抬头类型',  width: fixWidth(0.2),
                formatter:function(value,row){
                    var ModelTitleType=row.ModelTitleType?'医院':'中心';
                    return ModelTitleType;
                }},

            {field: 'opt', title: '操作', align: 'right', width: fixWidth(0.1), align: 'center',
                formatter: function (value, row, index) {

                    var edit = '<a href="javascript:void(0)"  data-options="iconCls:icon-edit" onclick="editRow(' + index + ',this)">修改</a>';//'+row+'

                    return edit;

                }}
        ]
    ];
    return columns;
}

//下拉框绑定数据
function comboboxLoad(tableName){
    var prjUrl=Shell.util.Path.rootPath,
        result=null,
        serviceParam = {};//请求服务参数

    serviceParam = setFields(tableName,serviceParam);//配置服务参数

    $.ajax({
        url: prjUrl + '/ServiceWCF/DictionaryService.svc/' + serviceParam.serviceName,
        data: serviceParam.data,
        dataType: 'json',
        type: serviceParam.type,
        timeout: 10000,
        async:true,//false同步请求，避免传递的result为null,若异步，可能$.ajax()方法还没执行完，就执行了return result,result为null,但感觉同步请求很耗时间!
        cache:false,
        contentType: 'application/json',//不加这个会出现错误
        success: function (data) {
            if (data.success) {
                //此处要对data进行过滤为数组格式
                //var arrData=[];
                data=eval('('+data.ResultDataValue+')').rows;//eval()把字符串转换成JSON格式
                //arrData.push(data);
                //var myData=[{"SectionNo":-1,"CName":"系统管理员"},{"SectionNo":1,"CName":"管理小组"},{"SectionNo":2,"CName":"生化组"},{"SectionNo":3,"CName":"发光组"}];
                switch (tableName){
                    //小组名称
                    case 'PGROUP':$('#cmbSectionName').combobox('loadData',data);break;
                    //送检单位
                    case 'CLIENTELE':$('#cmbClientName').combobox('loadData',data);break;
                    //模板名称
                    case 'PrintFormat':$('#cmbPrintFormatName').combobox('loadData',data);break;
                    //特殊项目
                    case 'TESTITEM':$('#cmbSpecialtyItemName').combobox('loadData',data);break;
                }
            }
        },
        error: function (data) {
            $.messager.alert('提示信息', data.ErrorInfo, 'error');

        }
    });

}

//配置下拉框字段
function setFields(tableName,serviceParam){

    serviceParam.serviceName='GetPubDict';//服务名
    serviceParam.type='GET';//请求方式

    switch (tableName) {
        case 'PGROUP':
            serviceParam.data = {tablename: 'PGROUP', fields:'SectionNo,CName'};//小组名称对应的数据表'{SectionNo,CName}'}
            break;
        case 'CLIENTELE':
            serviceParam.data = {tablename: 'CLIENTELE', fields: 'ClIENTNO,CNAME'};//送检单位对应的数据表
            break;
        case 'TESTITEM':
            serviceParam.data = {tablename: 'TESTITEM', fields: 'ItemNo,CName'};//特殊项目对应的数据表
            break;
        case 'PrintFormat':
            serviceParam.data = {tablename: 'PrintFormat', fields: 'Id,PrintFormatName'};//模板名称对应的数据表
            break;
    }

    return serviceParam;
}

 //设置下拉列表框的默认值
function setCmbDefault(row) {

        var root=Shell.util.Path.rootPath;
        //var myData=[{'SectionNo':-1,'CName':'系统管理员'},{'SectionNo':1,'CName':'管理小组'},{'SectionNo':2,'CName':'生化组'}];
        //小组名称下拉列表框
        $('#cmbSectionName').combobox({
            valueField: 'SectionNo',
            textField: 'CName',
            data:comboboxLoad('PGROUP'),//请求服务器,小组名称对应表:PGROUP comboboxLoad('PGROUP')
            onLoadSuccess: function () {
                var data = $(this).combobox('getData');
                if (data.length > 0) {
                    if (row.SectionName) {
                        $(this).combobox('select', row.SectionNo);
                    } else {
                        $(this).combobox('select', data[0].SectionNo);//默认第一项的值
                    }
                }
            },
            filter:function(q,row){
                var opts = $(this).combobox('options');
                var value = row[opts.textField] || "";
                return value.indexOf(q) > -1;//返回true,则显示出来
            }
        });

        //模板名称下拉列表框
        $('#cmbPrintFormatName').combobox({
            valueField: 'Id',//后台应该返回一个PrintFormatNo
            textField: 'PrintFormatName',
            data:comboboxLoad('PrintFormat'),
            onLoadSuccess: function () {
                var data = $(this).combobox('getData');
                if (data.length > 0) {
                    if (row.PrintFormatName) {
                        $(this).combobox('select', row.PrintFormatNo);
                    } else {
                        $(this).combobox('select', data[0].Id);//默认第一项的值注意：后台没有返回这个字段：PrintFormatNo
                    }
                }
            },
            filter:function(q,row){
                var opts = $(this).combobox('options');
                var value = row[opts.textField] || "";
                return value.indexOf(q) > -1;//返回true,则显示出来
            }
        });

        //特殊项目下拉列表框
        $('#cmbSpecialtyItemName').combobox({
            valueField: 'ItemNo',
            textField: 'CName',
            data:comboboxLoad('TESTITEM'),
            onLoadSuccess: function () {
                var data = $(this).combobox('getData');
                if (data.length > 0) {
                    if (row.SpecialtyItemName) {
                        $(this).combobox('select', row.SpecialtyItemNo);//SpecialtyItemNo和ItemNo一样吗？
                    } else {
    //                    $(this).combobox('select', data[0].ItemNo);//默认第一项的值
                        $(this).combobox('setValue','');//特殊项目默认为空
                    }
                }
            },
            filter:function(q,row){
                var opts = $(this).combobox('options');
                var value = row[opts.textField] || "";
                return value.indexOf(q) > -1;//返回true,则显示出来
            }
        });

        //送检单位下拉列表框
        $('#cmbClientName').combobox({
            valueField: 'ClIENTNO',
            textField: 'CNAME',
            data:comboboxLoad('CLIENTELE'),//送检单位对应的表:CLIENTELE

            onLoadSuccess: function () {
                var data = $(this).combobox('getData');
                if (data.length > 0) {
                    if (row.ClientName) {
                        $(this).combobox('select', row.ClientNo);//ClientNo注意大小写？
                    } else {
                        $(this).combobox('setValue','');
                        //$(this).combobox('select', data[0].ClIENTNO);//默认第一项的值
                    }
                }
            },
            filter:function(q,row){
                var opts = $(this).combobox('options');

                var value = row[opts.textField] || "";
                return value.indexOf(q) > -1;//返回true,则显示出来
            }
         });

        //使用标志下拉列表框
        $('#cmbUseFlag').combobox({
            valueField: 'UseFlag',
            textField: 'text',
            editable:false,
            data: [
                {UseFlag: 0, text: '否'},
                {UseFlag: 1, text: '是'}
            ],
            onLoadSuccess: function () {
                if (row.UseFlag) {
                    $(this).combobox('select', row.UseFlag);
                } else {
                    $(this).combobox('select', 1);
                }
            }
        });

        //是否带图下拉列表框
        $('#cmbImageFlag').combobox({
            valueField: 'ImageFlag',
            textField: 'text',
            editable:false,
            data: [
                {ImageFlag: 0, text: '否'},
                {ImageFlag: 1, text: '是'}
            ],
            onLoadSuccess: function () {
                if (row.ImageFlag) {
                    $(this).combobox('select', row.ImageFlag);
                } else {
                    $(this).combobox('select', 0);
                }
            }
        });

        //是否有抗生素下拉列表框
        $('#cmbAntiFlag').combobox({
            valueField: 'AntiFlag',
            textField: 'text',
            editable:false,
            data: [
                {AntiFlag: 0, text: '否'},
                {AntiFlag: 1, text: '是'}
            ],
            onLoadSuccess: function () {
                if (row.AntiFlag) {
                    $(this).combobox('select', row.AntiFlag);
                } else {
                    $(this).combobox('select', 0);
                }
            }
        });

        //是否套打下拉列表框
        $('#cmbBatchPrint').combobox({
            valueField: 'BatchPrint',
            textField: 'text',
            editable:false,
            data: [
                {BatchPrint: 0, text: '否'},
                {BatchPrint: 1, text: '是'}
            ],
            onLoadSuccess: function () {
                if (row.BatchPrint) {
                    $(this).combobox('select', row.BatchPrint);
                } else {
                    $(this).combobox('select', 0);
                }
            }
        });

        //就诊类型下拉列表框
        $('#cmbSickTypeNo').combobox({
            valueField: 'SickTypeNo',
            textField: 'text',
            editable:false,
            data: [
                {SickTypeNo: 0, text: '无'},
                {SickTypeNo: 1, text: '住院'},
                {SickTypeNo: 2, text: '门诊'},
                {SickTypeNo: 3, text: '体检'},
                {SickTypeNo: 4, text: '外送'}
            ],
            onLoadSuccess: function () {
                if (row.SickTypeNo) {
                    $(this).combobox('select', row.SickTypeNo);
                } else {
                    $(this).combobox('select', 0);
                }
            }
        });

        //模板抬头类型下拉列表框
        $('#cmbModelTitleType').combobox({
            valueField: 'ModelTitleType',
            textField: 'text',
            editable:false,
            data: [
                {ModelTitleType: 0, text: '中心'},
                {ModelTitleType: 1, text: '医院'}
            ],
            onLoadSuccess: function () {
                if (row.ModelTitleType) {
                    $(this).combobox('select', row.ModelTitleType);
                } else {
                    $(this).combobox('select', 0);
                }
            }
        });

    }

//刷新
function refresh() {
    var currentTime=new Date().getTime();
    $('#tb').datagrid('load',{itemkey:null,time:currentTime});
}

//增加
function addRow() {
    $('#dlg').dialog('open').dialog('setTitle', '新增');
    $('#dlg').window('center');

    $('#fm').form('clear');
    setCmbDefault('');//设置下拉列表的默认值
    btnType = 'add';
}

//删除行
function delRow() {
    var rows = $obj.datagrid('getSelections');

    if (rows.length == 0) {
        $.messager.alert('提示信息', '请选择记录', 'info');
    } else {
        $.messager.confirm('确认', '确认删除吗？', function (btn) {
            if (btn) {
                for (var i = 0; i < rows.length; i++) {
                    askService('delete', rows[i]['Id']);//删一条记录，交互一次服务器，若批量数据大，要求后台提供批量删除服务了，也就是前台传一个ID数组（idArr[]）
                }
                    $obj.datagrid('clearSelections');//因为getSelections会记忆选过的记录，所以要清空一下

            }
        });
    }

}

//编辑行
function editRow(index, obj) {

    var curData = $obj.datagrid('getData'),//返回当前加载完毕的数据，当前页吗？
        curRow = curData.rows[index];
    if (curRow) {
        $('#dlg').dialog('open').dialog('setTitle', '修改');
        $('#dlg').window('center');
        $('#fm').form('clear');
        $('#fm').form('load', curRow);//form表加载数据
        setCmbDefault(curRow);//设置下拉列表的默认值
        btnType = 'edit';
    }
}

//查询
function search(value) {
    var currentTime=new Date().getTime();
    $('#tb').datagrid('load',{itemkey:value,time:currentTime});
}

//保存
function save() {
    var entity;
   entity= getEntity(btnType);
    if(entity>0){
        $.messager.alert('警告','请检查输入值的完整性','warning');
        return;//增加或编辑提交的数据缺失，终止请求服务器
    }
    askService(btnType, null,entity);//请求服务器
    $('#dlg').dialog('close');
}

//取消（关闭）对话框
function cancel() {
    $('#dlg').dialog('close');
}



