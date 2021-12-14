/**
 * Created by Administrator on 2015/3/18.
 * @OverView:微信用户组
 */

var btnType;  //保存类型

//程序入口
$(function(){

    //初始化微信用户组表
    $('#tbWeiXinUserGroup').datagrid({
        title:'微信用户组',
        url:Shell.util.Path.rootPath+'/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBWeiXinUserGroupByHQL?isPlanish=true',
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
                var list=eval('('+data.ResultDataValue+')'),
                    result={};
                result.total=list.count || 0;
                result.rows=list.list || [];
                return result;
            }
        }
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
                {field: 'BWeiXinUserGroup_Id', title: '微信用户组ID', width: fixWidth(0.2) },
                {field: 'BWeiXinUserGroup_GroupName', title: '用户组名称', width: fixWidth(0.2)},
                {field: 'BWeiXinUserGroup_Comment', title: '用户组备注', width: fixWidth(0.2)},
                {field: 'BWeiXinUserGroup_OperaterId', title: '操作者ID', width: fixWidth(0.2)},
                {field: 'BWeiXinUserGroup_OperaterName', title: '操作者姓名', width: fixWidth(0.2)},
                {field: 'BWeiXinUserGroup_Count', title: '组内用户数', width: fixWidth(0.2)},
                {field: 'opt', title: '操作', align: 'right', width: fixWidth(0.1), align: 'center',
                    formatter: function (value, row, index) {
                        var edit = '<a href="javascript:void(0)"  data-options="iconCls:icon-edit" style="margin-right: 10px" onclick="editRow(' + index + ')">修改</a>';
                        return edit;
                    }}
            ]
        ];
        return columns;
    }


    //$.ajax()请求服务
    function askService(serviceType,entity, where) {
        var  serviceParam = {},
            async=serviceType=='delete'?false:true;//删除操作，同步执行$.ajax方法

        serviceParam = setService(serviceType, serviceParam,entity, where);//配置服务参数
        $.ajax({
            url: Shell.util.Path.rootPath + '/ServerWCF/DictionaryService.svc/' + serviceParam.serviceName,
            data: serviceParam.data,
            dataType: 'json',
            type: serviceParam.type,
            timeout: 10000,
            async:async,
            contentType: 'application/json',//内容类型要匹配后台约定的内容格式
            success: function (data) {
                if (data.success) {
                    switch (serviceType) {
                        case'add':
                        case'edit':
                        case'delete':
                            $('#tbWeiXinUserGroup').datagrid('load');
                            break;
                        case 'validate':
                            var data=eval('('+data.ResultDataValue+')'),
                                total=data.total||0;
                            if(total)
                                $.messager.alert('提示','数据库已存在此编号！不能重复插入','warning');
                            break;
                    }
                }else{
                    var error=data.ErrorInfo;
                    if(error.indexOf('插入重复键')>-1)
                        $.messager.alert('提示','数据库已存在此ID号！不能重复插入','warning');
                }
            },
            error: function (data) {
                $.messager.alert('提示信息', data.ErrorInfo, 'error');
            }
        });
    }

    //配置服务参数
    function setService(serviceType, serviceParam,entity, where) {

        switch (serviceType) {
            case'delete':
                serviceParam.serviceName = 'ST_UDTO_DelBWeiXinUserGroup';
                serviceParam.type = 'GET';
                serviceParam.data =  where;
                break;
            case'add':
                serviceParam.serviceName = 'ST_UDTO_AddBWeiXinUserGroup';
                serviceParam.type = 'POST';
                serviceParam.data =  entity;
                break;
            case'edit':
                serviceParam.serviceName = 'ST_UDTO_UpdateBWeiXinUserGroupByField';
                serviceParam.type = 'POST';
                serviceParam.data =  entity;
                break;
            case'validate':
                serviceParam.serviceName = '';
                serviceParam.type = 'GET';
                serviceParam.data =  where;
                break;
        }
        return serviceParam;
    }

    //获取数据实体（增加，编辑）,形如：{entity:{ID:1,name:'张珊'}}
    function getEntity(getType) {
        var result = '',
            fields=[];

        result += '{"entity":{'; //构造符合格式的字符串

        var B_WeiXinUserGroupID=$('#B_WeiXinUserGroupID').numberbox('getValue');
        if(!B_WeiXinUserGroupID)
            return 1;
        result+='"Id":'+B_WeiXinUserGroupID+',';
        fields.push("Id");

        var GroupName=$('#GroupName').numberbox('getValue');
        if(GroupName){
            result+='"GroupName":"'+GroupName+'",';
            fields.push("GroupName");
        }

        var Comment=$('#Comment').textbox('getValue');
        if(Comment){
            result+='"Comment":"'+Comment+'",';
            fields.push("Comment");
        }

        var OperaterId=$('#OperaterId').numberbox('getValue');
        if(OperaterId){
            result+='"OperaterId":"'+OperaterId+'",';
            fields.push("OperaterId");
        }

        var OperaterName=$('#OperaterName').textbox('getValue');
        if(OperaterName){
            result+='"OperaterName":"'+OperaterName+'",';
            fields.push("OperaterName");
        }


        var Count=$('#Count').textbox('getValue');
        if(Count=="0" || Count){
            result+='"Count":'+Count;
            fields.push("Count");
        }

        if(btnType=='edit')
        {
            result+='},"fields":"'+fields.join(',')+'"}';
        }else{
            result += '}}';
        }

        return result;
    }

    //刷新
    $('#refresh').bind('click',function (){
        $('#tbWeiXinUserGroup').datagrid('load');
        $('#txtSearch').searchbox('setValue',null);
    });

    //日期转换
    function setDate(date) {
        var second=date.getSeconds()>9?date.getSeconds():'0'+date.getSeconds(),
            minutes=date.getMinutes()>9? date.getMinutes():'0'+date.getMinutes(),
            hours=date.getHours()>9?date.getHours():'0'+date.getHours(),
            day = date.getDate() > 9 ? date.getDate() : '0' + date.getDate(),
            month = (date.getMonth() + 1) > 9 ? date.getMonth() + 1 : '0' + (date.getMonth()+1);
        return date.getFullYear() + '-' + month + '-' + day+' '+hours+':'+minutes+':'+second;
    }

    //新增
    $('#addRow').bind('click',function addRow(){
        btnType='add';
        $('#dlg').dialog({modal:true});
        $('#dlg').dialog('open').dialog('setTitle','新增');
        $('#dlg').window('center');
        $('#frm').form('clear');
        $('#B_WeiXinUserGroupID').numberbox('enable');

    });

    //删除
    $('#deleteRow').bind('click',function deleteRow(){
        var rows = $('#tbWeiXinUserGroup').datagrid('getSelections') ||[],
            length=rows.length || 0;

        if (length == 0) {
            $.messager.alert('提示信息', '请选择记录', 'info');
        } else {
            $.messager.confirm('确认', '确认删除吗？', function (btn) {
                if (btn) {
                    for (var i = 0; i < length; i++) {
                        askService('delete', null,{Id:rows[i]['BWeiXinUserGroup_Id']});
                    }
                    $('#tbWeiXinUserGroup').datagrid('clearSelections');//因为getSelections会记忆选过的记录，所以要清空一下
                }
            });
        }
    });

    //查询
    $('#search').bind('searcher',function search(value){
        $('#tbWeiXinUserGroup').datagrid('load',{fields:value});
    });

    //保存
    $('#save').bind('click',function save(){
        var entity;
        entity= getEntity(btnType);
        if(entity>0){
            $.messager.alert('警告','请检查必输项是否为空','warning');
            return;
        }
        askService(btnType, entity);//请求服务器
        $('#dlg').dialog('close');
    });

    //取消
    $('#cancel').bind('click',function cancel(){
        $('#dlg').dialog('close');
    });
});

//修改
function editRow(index){
    var curData = $('#tbWeiXinUserGroup').datagrid('getData'),//返回当前页加载完毕的数据
        curRow = curData.rows[index];
    if(curRow){
        btnType='edit';
        $('#dlg').dialog({modal:true});
        $('#dlg').dialog('open').dialog('setTitle','修改');
        $('#dlg').window('center');
        $('#frm').form('clear');
        $('#B_WeiXinUserGroupID').numberbox('disable');
        $('#frm').form('load', curRow);
    }
}


