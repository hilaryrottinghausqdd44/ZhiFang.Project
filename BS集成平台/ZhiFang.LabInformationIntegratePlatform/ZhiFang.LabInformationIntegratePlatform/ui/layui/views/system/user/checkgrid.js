layui.extend({
	uxutil:'ux/util',
	uxtable:'ux/table'
}).use(['uxutil','uxtable','layer'],function(){
	var $=layui.$,
		uxutil = layui.uxutil,
		uxtable = layui.uxtable;
	
	//获取员工列表服务
	var SELECT_USER_LIST_URL = uxutil.path.ROOT + '/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHREmployeeByHQL';
	//获取身份关系数据
	var SELECT_LINK_URL = uxutil.path.ROOT + '/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHREmpIdentityByHQL';
	//新增服务地址
	var ADD_LINK_URL = uxutil.path.ROOT + '/ServerWCF/RBACService.svc/RBAC_UDTO_AddHREmpIdentity';
	
	//获取页面传递的参数
	var PARAMS = uxutil.params.get(true);
	//系统ID
	var SYSTEM_ID = PARAMS.SYSTEMID;
	//系统编码
	var SYSTEM_CODE = PARAMS.SYSTEMCODE;
	//系统名称
	var SYSTEM_NAME = PARAMS.SYSTEMNAME;
	//身份ID
	var TSYS_ID = PARAMS.TSYSID;
	//身份编码
	var TSYS_CODE = PARAMS.TSYSCODE;
	//身份名称
	var TSYS_NAME = PARAMS.TSYSNAME;
	//身份关系列表
	var LINK_LIST = null;
	//人员列表
	var EMP_LIST = null;
	
	//刷新
	$("#refresh_button").on("click",function(){
		init();
	});
	//确认
	$("#submit_button").on("click",function(){
		onSubmitClick();
	});
	//取消
	$("#cancel_button").on("click",function(){
		parent.layer.closeAll('iframe');
	});
	
	//人员列表实例
	var table_Ind = uxtable.render({
		elem:'#table',
		height:'full-80',
		title:'员工列表',
		initSort:{field:'HREmpIdentity_HREmployee_CName',type:'asc'},
		cols: [[
			{type:'checkbox',fixed:'left'},
			{field:'HREmployee_Id',width:180,title:'ID',hide:true},
			{field:'HREmployee_CName',width:120,title:'员工名称'},
			{field:'HREmployee_HRDept_CName',width:150,title:'隶属部门'}
		]],
		loading:false,
		limit:1000
	});
	
	//确认
	function onSubmitClick(){
		//获取勾选的部门ID数组
		var checkedEmpInfoList = getCheckedEmpInfoList();
		
		var layerIndex = layer.load();
		onSave(checkedEmpInfoList,0,layerIndex,function(layerIndex,errorList){
			layer.close(layerIndex);
			if(errorList.length > 0){
				var msg = errorList.join(',') + '没有保存成功！';
				layer.msg(msg,{iccon:5});
			}else{
				parent.layer.closeAll('iframe');
				parent.onSearch2();
			}
		},[]);
	};
	//保存关系数据
	function onSave(list,index,layerIndex,callback,errorList){
		if(index >= list.length){
			callback(layerIndex,errorList);
			return;
		}
		
		var entity = {
			IsUse:true,
			IdenTypeID:TSYS_ID,
			TSysCode:TSYS_CODE,
			TSysName:TSYS_NAME,
			SystemID:SYSTEM_ID,
			SystemCode:SYSTEM_CODE,
			SystemName:SYSTEM_NAME,
			HREmployee:{Id:list[index].id,DataTimeStamp:[0,0,0,0,0,0,0,0]}
		};
		
		uxutil.server.ajax({
			type:'post',
			url:ADD_LINK_URL,
			data:JSON.stringify({entity:entity})
		},function(data){
			if(data.success){
				onSave(list,++index,layerIndex,callback,errorList);
			}else{
				errorList.push(list[index].name);
				onSave(list,++index,layerIndex,callback,errorList);
			}
		});
	};
	//获取勾选的部门信息数组
	function getCheckedEmpInfoList(){
		//获取选中数据
		var checkStatus = table_Ind.table.checkStatus('table'),
			data = checkStatus.data,
			len = data.length,
			checkedEmpInfoList = [];
			
		
		for(var i=0;i<len;i++){
			checkedEmpInfoList.push({
				id:data[i].HREmployee_Id,
				name:data[i].HREmployee_CName
			});
		};
		
		return checkedEmpInfoList;
	};
	//数据加载
	function onLoad(callback){
		var url = SELECT_USER_LIST_URL + '?isPlanish=true&fields=HREmployee_Id,HREmployee_CName,HREmployee_HRDept_CName'
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data.success){
				EMP_LIST = changeData(data);
				callback();
			}else{
				layer.msg(data.msg,{iccon:5});
			}
		});
	};
	function changeData(data){
		var list = (data.value || {}).list || [],
			len = list.length;
			
		for(var i=len-1;i>=0;i--){
			var hasLink = hasLinkData(list[i].HREmployee_Id);
			if(hasLink){
				list.splice(i,1);
			}
		}
		
		return list;
	};
	//是否已经存在是否关系
	function hasLinkData(empId){
		var hasLink = false;
		for(var i in LINK_LIST){
			if(empId == LINK_LIST[i].HREmpIdentity_HREmployee_Id){
				hasLink = true;
				break;
			}
		}
		return hasLink;
	};
	//获取身份关系列表
	function onLoadLinkList(callback){
		var url = SELECT_LINK_URL;
		url += "?isPlanish=true&fields=HREmpIdentity_HREmployee_Id" +
		"&where=hrempidentity.TSysCode='" + TSYS_CODE + "'" +
		" and hrempidentity.SystemCode='" + SYSTEM_CODE + "'";
			
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data.success){
				LINK_LIST = (data.value || {}).list || [];
				callback();
			}else{
				layer.msg(data.msg,{iccon:5});
			}
		});
	};
	//初始化
	function init(){
		//获取身份关系列表
		onLoadLinkList(function(data){
			//数据加载
			onLoad(function(){
				table_Ind.reload({
					data:EMP_LIST
				});
			});
		});
	};
	init();
});