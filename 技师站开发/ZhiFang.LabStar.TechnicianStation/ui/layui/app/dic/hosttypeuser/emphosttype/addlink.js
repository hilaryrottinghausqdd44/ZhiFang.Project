/**
   @Name：站点类型关系
   @Author：liangyl
   @version 2021-08-04
 */
layui.extend({
	uxutil: 'ux/util',
	ParameterList:'app/dic/hosttypeuser/list',//站点类型
	ParameterTypeList:'app/dic/hosttypeuser/typelist'//模块
}).use(['uxutil','ParameterList','ParameterTypeList'], function(){
	var $ = layui.$,
		uxutil = layui.uxutil,
		ParameterList = layui.ParameterList,
		ParameterTypeList = layui.ParameterTypeList;
    //模块类型实例
    var table_ind0 = null;
     //站点类型实例
    var table_ind1 = null;
    //新增数据服务
	var ADD_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddBHostTypeUser';
     //外部参数
	var PARAMS = uxutil.params.get(true);
    //人员ID
    var EMPID = PARAMS.EMPID;
    //已选数据，需剔除
    var DEFAULT_DATA = parent.ChildHostTypeData();
    //保存参数
    var saveErrorCount = 0,
		saveCount = 0,
		saveLength = 0;
    //模块类型
	table_ind0 = ParameterTypeList.render({
		elem:'#module_table',
    	title:'模块类型',
    	height:'full-25',
    	size: 'sm', //小尺寸的表格
    	done: function(res, curr, count) {
			setTimeout(function(){
				var tr = table_ind0.instance.config.instance.layBody.find('tr:eq(0)');
				if(tr.length > 0){
					tr.click();
				}else{
					ROW_CHECK_DATA = [];
				}
			},0);
		}
	});
	table_ind0.instance.reload({data:[]});
     //站点类型列表实例
    table_ind1 = ParameterList.render({
		elem:'#para_hosttype_table',
    	title:'站点类型',
    	height:'full-47',
    	size: 'sm', //小尺寸的表格
    	cols:[[
        	{type: 'checkbox', fixed: 'left'},
	        {field:'Id',title:'ID',width:150,sort:true,hide:true},				
			{field:'Code',title:'Code',width:150,sort:true,hide:true},
		    {field:'Name',title:'站点类型',minWidth:150,flex:1}
		]],
    	done: function(res, curr, count) {
			setTimeout(function(){
				var rowIndex = 0;
				var tr = table_ind1.instance.config.instance.layBody.find('tr:eq('+rowIndex+')');
				if(tr.length > 0){
					tr.click();
				}
			},0);
		}
	});
	table_ind1.instance.reload({data:[]}); 
	//模块类型列表
	table_ind0.table.on('row(module_table)', function(obj){
		//标注选中样式
	    obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
		ROW_CHECK_DATA =[obj.data];
		table_ind1.loadData(obj.data.DefaultValue,obj.data.Name,function(list){
			var arr = resultdata(list);
			table_ind1.instance.reload({data:arr}); 
		});
	});
	//按钮事件
	var active = {
		save: function() {//新增关系、
		    var checkStatus = table_ind0.table.checkStatus('para_hosttype_table');
            var list = checkStatus.data;
            if(list.length ==0){ 
            	layer.msg('请先选择数据行');
            	return false;
            }
            onSave(list);//保存操作
		},
		close: function() {//关闭
			parent.layer.closeAll('iframe');
		}
	};
	$('.empselect .layui-btn').on('click', function() {
		var type = $(this).data('type');
		active[type] ? active[type].call(this) : '';
	});
	//单个新增
	function AddLink(entity){
		//显示遮罩层
		var config1 = {
			type: "POST",
			url: ADD_URL,
			data: JSON.stringify({entity: entity})
		};
		var index = layer.load();
		uxutil.server.ajax(config1, function(data) {
			if (data.success) {
			    saveCount++;
			} else {
				saveErrorCount++;
			}				
			if ( saveCount +  saveErrorCount ==  saveLength) {
				if ( saveErrorCount == 0){
					layer.msg('保存成功!',{icon:6,time:2000});
					parent.layer.closeAll('iframe');
					parent.afterUpdate(data);
				}else{
					layer.msg(data.ErrorInfo, { icon: 5});
				}
			}
		});
	}
	//保存
	function onSave(arr){
		saveErrorCount = 0;
		saveCount = 0;
		saveLength = arr.length;
    	for (var i = 0; i < arr.length; i++) {
            var entity = {
                EmpID: EMPID,
                HostTypeID: arr[i].Code,
                IsUse:1
            }
            AddLink(entity);
        }
	}
	//站点类型过滤，已选的不显示
	function resultdata(list){
		var arr = [],isExec=true;
		if(DEFAULT_DATA.length>0){ //剔除已选的人员
			for(var i=0;i<list.length;i++){
				isExec=true;
				for(var j=0;j<DEFAULT_DATA.length;j++){
					if(list[i].Code == DEFAULT_DATA[j].BHostTypeUser_HostTypeID ){
						isExec = false;
						break;
					}
				}
				if(isExec)arr.push(list[i]);
			}
		}
		if(DEFAULT_DATA.length==0)arr =list;
		return arr;
	}
	function init(){
		table_ind0.loadData();
	}
	//初始化 
	init();
});