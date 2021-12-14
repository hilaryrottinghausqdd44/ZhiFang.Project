layui.extend({
	uxutil:'ux/util',
	uxtree:'ux/tree'
}).use(['uxutil','uxtree','layer'],function(){
	var $=layui.$,
		uxutil = layui.uxutil,
		uxtree = layui.uxtree;
	
	//服务地址
	var SELECT_URL = uxutil.path.ROOT + '/ServerWCF/RBACService.svc/RBAC_RJ_GetHRDeptFrameListTree';
	//获取身份关系数据
	var SELECT_LINK_URL = uxutil.path.ROOT + '/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHRDeptIdentityByHQL';
	//新增服务地址
	var ADD_LINK_URL = uxutil.path.ROOT + '/ServerWCF/RBACService.svc/RBAC_UDTO_AddHRDeptIdentity';
	
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
	//树数据
	var TREE_DATA = null;
	
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
	
	// 初始化树
	var tree = uxtree.render({
		id:'tree',
		elem:"#tree",
		showCheckbox:true,
		checkParents:false,
		checkChildren:false,
		data:[],
		done:function(res,root,first){}
	});
	
	//确认
	function onSubmitClick(){
		//获取勾选的部门ID数组
		var checkedDeptInfoList = getCheckedDeptInfoList();
		
		var layerIndex = layer.load();
		onSave(checkedDeptInfoList,0,layerIndex,function(layerIndex,errorList){
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
			HRDept:{Id:list[index].id,DataTimeStamp:[0,0,0,0,0,0,0,0]}
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
	function getCheckedDeptInfoList(){
		var nodes = uxtree.getChecked("tree"),
			checkedDeptInfoList = [];
			
		function getcheckedNode(nodes){
			for(var i in nodes){
				if(!nodes[i].disabled){
					checkedDeptInfoList.push({
						id:nodes[i].tid,
						name:nodes[i].text
					});
				}
				if(nodes[i].children && nodes[i].children.length > 0){
					getcheckedNode(nodes[i].children);
				}
			}
		}
		getcheckedNode(nodes);
		
		return checkedDeptInfoList;
	};
	//数据加载
	function onLoad(callback){
		var url = SELECT_URL + '?id=0&fields=HRDept_Id,HRDept_DataTimeStamp,HRDept_UseCode'
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data.success){
				TREE_DATA = changeData(data.value);
				callback();
			}else{
				layer.msg(data.msg,{iccon:5});
			}
		});
	};
	//数据转换
	function changeData(data){
    	var changeNode = function(node){
    		node.id = node.tid;
			node.title = node.text;
			node.children = node.Tree;
			node.spread = true;
			delete node.Tree;
			
			if(node.children){
				var len = node.children.length;
    			for(var i=len-1;i>=0;i--){
    				var hasLink = hasLinkData(node.children[i].tid);
    				if(hasLink){
    					if(node.children[i].Tree && node.children[i].Tree.length > 0){
    						node.children[i].disabled = true;
    						changeNode(node.children[i]);
    					}else{
    						node.children.splice(i,1);
    					}
    				}else{
    					changeNode(node.children[i]);
    				}
    			}
    		}
			
			
    	};
    	changeNode(data);
    	
    	return data;
	};
	//是否已经存在是否关系
	function hasLinkData(deptId){
		var hasLink = false;
		for(var i in LINK_LIST){
			if(deptId == LINK_LIST[i].HRDeptIdentity_HRDept_Id){
				hasLink = true;
				break;
			}
		}
		return hasLink;
	};
	//获取身份关系列表
	function onLoadLinkList(callback){
		var url = SELECT_LINK_URL;
		url += "?isPlanish=true&fields=HRDeptIdentity_HRDept_Id" +
		"&where=hrdeptidentity.TSysCode='" + TSYS_CODE + "'" +
		" and hrdeptidentity.SystemCode='" + SYSTEM_CODE + "'";
			
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
				tree.reload({
					data:TREE_DATA.children
				});
			});
		});
	};
	init();
});