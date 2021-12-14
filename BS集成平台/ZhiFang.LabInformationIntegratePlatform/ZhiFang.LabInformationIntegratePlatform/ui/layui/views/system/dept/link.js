layui.extend({
	uxutil:'ux/util',
	uxtable:'ux/table'
}).use(['uxutil','uxtable','layer'], function(){
	var $ = layui.$,
		uxutil = layui.uxutil,
		uxtable = layui.uxtable;
		
	//获取所有身份枚举
	var GET_TYPE_LIST_URL = '/ServerWCF/CommonService.svc/GetClassDic';
	//获取系统列表
	var GET_SYSTEM_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LIIPService.svc/ST_UDTO_SearchIntergrateSystemSetByHQL';
	//获取数据
	var SELECT_LINK_URL = uxutil.path.ROOT + '/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHRDeptIdentityByHQL';
	//新增关系
	var ADD_LINK_URL = uxutil.path.ROOT + '/ServerWCF/RBACService.svc/RBAC_UDTO_AddHRDeptIdentity';
	//删除关系
	var DEL_LINK_URL = uxutil.path.ROOT + '/ServerWCF/RBACService.svc/RBAC_UDTO_DelHRDeptIdentity';
	
	//部门枚举码
	var DEPT_ENUM_CODE = 'DeptSystemType';
	
	//所有系统列表
	var ALL_SYSTEM_LIST = null;
	//所有身份枚举列表
	var ALL_TYPE_LIST = null;
	//身份关系列表
	var LINK_LIST = null;
	
	//选中的系统编码
	var CHECKED_SYSTEM_CODE = null;
	//选中的系统ID
	var CHECKED_SYSTEM_ID = null;
	//选中的系统名称
	var CHECKED_SYSTEM_NAME = null;
	//选中的系统地址
	var CHECKED_SYSTEM_HOST = null;
	//选中的系统TR
	var CHECKED_SYSTEM_TR = null;
	
	//选中的身份ID
	var CHECKED_TSYS_ID = null;
	//选中的身份编码
	var CHECKED_TSYS_CODE = null;
	//选中的身份名称
	var CHECKED_TSYS_NAME = null;
	//选中的身份TR
	var CHECKED_TSYS_CODE_TR = null;
	
	//系统列表实例
	var table0_Ind = uxtable.render({
		elem:'#table0',
		toolbar:'#table0-toolbar-top',
		height:'full-40',
		title:'系统列表',
		initSort:{field:'Id',type:'asc'},
		cols: [[
			{field:'Id',width:180,title:'ID',hide:true},
			{field:'SystemCode',width:150,title:'系统编码'},
			{field:'SystemName',title:'系统名称'},
			{field:'SystemHost',width:180,title:'系统地址',hide:true}
		]],
		loading:false,
		done:function(res,curr,count){
			setTimeout(function(){
				var tr = table0_Ind.instance.config.instance.layBody.find('tr:eq(0)');
				if(tr.length > 0){
					tr.click();
				}
			},0);
		}
	});
	//身份列表实例
	var table1_Ind = uxtable.render({
		elem:'#table1',
		toolbar:'#table1-toolbar-top',
		height:'full-40',
		title:'身份列表',
		initSort:{field:'Id',type:'asc'},
		cols: [[
			{field:'Id',width:180,title:'ID',hide:true},
			{field:'Code',width:150,title:'身份编码'},
			{field:'Name',title:'身份名称'}
		]],
		loading:false,
		done:function(res,curr,count){
			setTimeout(function(){
				var tr = table1_Ind.instance.config.instance.layBody.find('tr:eq(0)');
				if(tr.length > 0){
					tr.click();
				}
			},0);
		}
	});
	//关系列表实例
	var table2_Ind = uxtable.render({
		elem:'#table2',
		toolbar:'#table2-toolbar-top',
		height:'full-40',
		title:'关系列表',
		initSort:{field:'HRDeptIdentity_HRDept_CName',type:'asc'},
		cols: [[
			{type:'checkbox',fixed:'left'},
			{field:'HRDeptIdentity_Id',width:180,title:'ID',hide:true},
			{field:'HRDeptIdentity_HRDept_CName',width:150,title:'部门名称'},
			{field:'HRDeptIdentity_HRDept_StandCode',width:150,title:'标准代码'},
			{field:'HRDeptIdentity_HRDept_DeveCode',width:150,title:'开发商代码'}
		]],
		loading:false,
		limit:1000,
		done:function(res,curr,count){
			setTimeout(function(){
				if(!ALL_TYPE_LIST || ALL_TYPE_LIST.length == 0){
					var buttons = $("div[lay-id='table2']").find("button");
					buttons.each(function(num,el){
						$(this).hide();
					});
				}
			},0);
		}
	});
	//系统列表监听
	table0_Ind.table.on('toolbar(table0)', function(obj){
		switch(obj.event){
			case 'search':onSearch0();break;
		}
	});
	table0_Ind.table.on('row(table0)', function(obj){
		if(CHECKED_SYSTEM_TR){
			CHECKED_SYSTEM_TR.removeClass('row-checked');
		}
		CHECKED_SYSTEM_TR = obj.tr;
		CHECKED_SYSTEM_TR.addClass('row-checked');
		
		CHECKED_SYSTEM_ID = obj.data.Id;
		CHECKED_SYSTEM_CODE = obj.data.SystemCode;
		CHECKED_SYSTEM_NAME = obj.data.SystemName;
		CHECKED_SYSTEM_HOST = obj.data.SystemHost;
		
		onSearch1();
	});
	//身份列表监听
	table1_Ind.table.on('toolbar(table1)', function(obj){
		switch(obj.event){
			case 'search':onSearch1();break;
		}
	});
	table1_Ind.table.on('row(table1)', function(obj){
		if(CHECKED_TSYS_CODE_TR){
			CHECKED_TSYS_CODE_TR.removeClass('row-checked');
		}
		CHECKED_TSYS_CODE_TR = obj.tr;
		CHECKED_TSYS_CODE_TR.addClass('row-checked');
		
		CHECKED_TSYS_ID = obj.data.Id;
		CHECKED_TSYS_CODE = obj.data.Code;
		CHECKED_TSYS_NAME = obj.data.Name;
		onSearch2();
	});
	//关系列表监听
	table2_Ind.table.on('toolbar(table2)', function(obj){
		switch(obj.event){
			case 'search':onSearch2();break;
			case 'add':onAddClick();break;
			case 'del':onDelClick();break;
		}
	});
	
	//系统列表查询
	function onSearch0(){
		table2_Ind.reload({data:[]});//清空关系列表数据
		table1_Ind.reload({data:[]});//清空身份列表数据
		var index = layer.load();
		//获取系统列表
		onLoadSystemList(function(data){
			layer.close(index);
			if(data.success){
				var list = (data.value || {}).list || [];
				for(var i in list){
					if(list[i].SystemCode == 'ZF_PRE'){
						list.splice(i,1);
						break;
					}
				}
				ALL_SYSTEM_LIST = list;
				table0_Ind.reload({data:ALL_SYSTEM_LIST});
			}else{
				ALL_SYSTEM_LIST= [];
				table0_Ind.instance.config.instance.layMain.html('<div class="layui-none">' + data.msg + '</div>');
			}
		});
	};
	//身份列表查询
	function onSearch1(){
		table2_Ind.reload({data:[]});//清空关系列表数据
		if(!CHECKED_SYSTEM_HOST){
			table1_Ind.instance.config.instance.layMain.html('<div class="layui-none">系统编码为' + 
				CHECKED_SYSTEM_CODE + '的系统地址不存在，请维护好再使用本功能！</div>');
			return;
		}
		var index = layer.load();
		//获取所有身份枚举列表
		onLoadTypeList(function(data){
			layer.close(index);
			if(data.success){
				ALL_TYPE_LIST = data.value || [];
				table1_Ind.reload({data:ALL_TYPE_LIST});
			}else{
				ALL_TYPE_LIST = [];
				table1_Ind.instance.config.instance.layMain.html('<div class="layui-none">没有找到数据!</div>');
			}
		});
	};
	//关系列表查询
	function onSearch2(){
		var index = layer.load();
		//获取身份关系列表
		onLoadLinkList(function(data){
			layer.close(index);
			if(data.success){
				LINK_LIST = (data.value || {}).list || [];
				table2_Ind.reload({data:LINK_LIST});
			}else{
				LINK_LIST = [];
				table2_Ind.instance.config.instance.layMain.html('<div class="layui-none">' + data.msg + '</div>');
			}
		});
	};
	//新增关系
	function onAddClick(){
		var url = 'checktree.html?t=' + new Date().getTime() +
			'&TSysId=' + CHECKED_TSYS_ID +
			'&TSysCode=' + CHECKED_TSYS_CODE +
			'&TSysName=' + CHECKED_TSYS_NAME +
			'&SystemId=' + CHECKED_SYSTEM_ID +
			'&SystemCode=' + CHECKED_SYSTEM_CODE +
			'&SystemName=' + CHECKED_SYSTEM_NAME;
			
		layer.open({
			type:2,
			area:['300px','600px'],
			maxmin:false,
			title:'新增关系',
			content:url
		});
	};
	//删除关系
	var DelErrorList = [];
	function onDelClick(){
		//获取选中数据
		var checkStatus = table2_Ind.table.checkStatus('table2'),
			data = checkStatus.data,
			len = data.length;
		
		if(len > 0){
			layer.confirm('确定需要删除吗？', function(index){
				var ids = [];
				for(var i=0;i<len;i++){
					ids.push(data[i].HRDeptIdentity_Id);
				}
				DelErrorList = [];
				delOneInfo(ids,0);
			});
		}else{
			layer.msg("请选择需要删除的数据！",{icon:0});
		}
	}
	//删除数据
	function delOneInfo(ids,index){
		var id = ids[index];
		
		uxutil.server.ajax({
			url:DEL_LINK_URL + '?id=' + id
		},function(data){
			if(!data.success){
				DelErrorList.push(data.msg);
			}
			if(index < ids.length-1){
				delOneInfo(ids,++index);
			}else{
				if(DelErrorList.length > 0){
					layer.msg(DelErrorList.join('</br>'));
				}else{
					onSearch2();
					layer.msg("删除完毕！",{icon:6});
				}
			}
		});
	}
	
	//获取系统列表
	function onLoadSystemList(callback){
		var url = GET_SYSTEM_LIST_URL + "?fields=IntergrateSystemSet_Id,IntergrateSystemSet_SystemCode,IntergrateSystemSet_SystemName,IntergrateSystemSet_SystemHost";
		url += "&where=intergratesystemset.IsUse=1";
		uxutil.server.ajax({
			url:url
		},function(data){
			callback(data);
		});
	};
	//获取所有身份枚举列表
	function onLoadTypeList(callback){
		var url = uxutil.path.LOCAL + '/' + CHECKED_SYSTEM_HOST + GET_TYPE_LIST_URL,
			enumName = '';

		//检验之星、前处理、检验之星6.6都走LabStar项目
		if(CHECKED_SYSTEM_CODE == 'ZF_LabStar' || CHECKED_SYSTEM_CODE == 'ZF_LabStar_Nurse' || 
			CHECKED_SYSTEM_CODE == 'ZF_LabStar6' || CHECKED_SYSTEM_CODE == 'ZF_LAB_START' || 
			CHECKED_SYSTEM_CODE == 'ZF_PRE'){
			enumName = 'LabStar';
		}else{
			var list = CHECKED_SYSTEM_CODE.split('_');
			enumName = list[list.length - 1];
		}
		
		url += '?classnamespace=ZhiFang.Entity.' + enumName + '&classname=' + DEPT_ENUM_CODE;
		
		
		uxutil.server.ajax({
			url:url
		},function(data){
			callback(data);
		});
	};
	//获取身份关系列表
	function onLoadLinkList(callback){
		var url = SELECT_LINK_URL;
		url += "?isPlanish=true&fields=HRDeptIdentity_Id,HRDeptIdentity_HRDept_CName,HRDeptIdentity_HRDept_StandCode,HRDeptIdentity_HRDept_DeveCode" +
		"&where=hrdeptidentity.TSysCode='" + CHECKED_TSYS_CODE + "'" +
		" and hrdeptidentity.SystemCode='" + CHECKED_SYSTEM_CODE + "'";
		
		uxutil.server.ajax({
			url:url
		},function(data){
			callback(data);
		});
	};
	
	//初始化
	function init(){
		onSearch0();
	};
	
	init();
	
	//公开刷新关系列表
	window.onSearch2 = onSearch2;
});