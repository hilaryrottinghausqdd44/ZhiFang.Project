layui.extend({
	uxutil:'ux/util'
}).use(['uxutil','form','laydate'],function(){
	var $ = layui.$,
		form = layui.form,
		uxutil = layui.uxutil;
		
	//获取关系列表服务
	var GET_LINK_LIST_URL = uxutil.path.ROOT + '/ServerWCF/IMService.svc/ST_UDTO_SearchCVCriticalValueEmpIdDeptLinkByHQL';
	//获取机构列表服务地址
	var GET_DEPT_LIST_URL = uxutil.path.ROOT + "/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHRDeptByHQL";
	//获取员工列表服务地址
	var GET_USER_LIST_URL = uxutil.path.ROOT + "/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHREmployeeByHQL";
	//新增服务地址
	var ADD_URL = uxutil.path.ROOT + "/ServerWCF/IMService.svc/ST_UDTO_AddCVCriticalValueEmpIdDeptLink";
		
	//机构列表
	var DEPT_LIST = [];
	//员工列表
	var USER_LIST = [];
	
	
	//保存
	form.on("submit(submit_button)",function(obj){
		onSubmit();
	});
	//取消
	$("#cancel_button").on("click",function(){
		parent.layer.closeAll('iframe');
	});
	
	//初始化下拉列表
	function initSelect(){
		//员工列表
		var userList = ['<option value="">选择员工</option>'];
		for(var i in USER_LIST){
			userList.push('<option value="' + USER_LIST[i].Id + '">' + USER_LIST[i].CName + '</option>');
		}
		$("#user").html(userList.join(''));
		
		//部门列表
		var deptList = ['<option value="">选择部门</option>'];
		for(var i in DEPT_LIST){
			deptList.push('<option value="' + DEPT_LIST[i].Id + '">' + DEPT_LIST[i].CName + '</option>');
		}
		$("#dept").html(deptList.join(''));
		
		form.render('select');
	};
	
	//获取机构列表
	function getDeptList(callback){
		var url = GET_DEPT_LIST_URL + '?fields=HRDept_Id,HRDept_CName';
		
		layer.load();
		uxutil.server.ajax({
			url:url
		},function(data){
			layer.closeAll('loading');
			if(data.success){
				DEPT_LIST = (data.value || {}).list || [];
				callback();
			}else{
				layer.msg(data.msg);
			}
		});
	};
	//获取员工列表
	function getUserList(callback){
		var url = GET_USER_LIST_URL + '?fields=HREmployee_Id,HREmployee_CName';
		
		layer.load();
		uxutil.server.ajax({
			url:url
		},function(data){
			layer.closeAll('loading');
			if(data.success){
				USER_LIST = (data.value || {}).list || [];
				callback();
			}else{
				layer.msg(data.msg);
			}
		});
	};
	//新增
	function onSubmit(){
		var url = ADD_URL,
			user = $("#user option:selected"),
			dept = $("#dept option:selected");
			
		var params = {
			EmpID:user.val(),
			EmpName:user.text(),
			DeptID:dept.val(),
			DeptName:dept.text(),
			IsUse:true
		};
		
		hasData(params.EmpID,params.DeptID,function(bo){
			if(bo){
				layer.msg('已经存在该部门与员工的关系！');
				return;
			}else{
				layer.load();
				uxutil.server.ajax({
					url:url,
					type:'post',
					data:JSON.stringify({entity:params})
				},function(data){
					layer.closeAll('loading');
					if(data.success){
						layer.msg('保存成功',{
							icon:1,
							time:1000
						},function(){
							parent.layer.closeAll('iframe');
							parent.onSearch();
						});
					}else{
						layer.msg(data.msg);
					}
				});
			}
		});
	};
	//校验时是否已存在
	function hasData(userId,deptId,callback){
		var url = GET_LINK_LIST_URL + '?fields=CVCriticalValueEmpIdDeptLink_Id';
		url += '&where=cvcriticalvalueempiddeptlink.EmpID=' + userId +
			' and cvcriticalvalueempiddeptlink.DeptID=' + deptId;
		
		layer.load();
		uxutil.server.ajax({
			url:url
		},function(data){
			layer.closeAll('loading');
			if(data.success){
				var list = (data.value || {}).list || [];
				var bo = list.length > 0 ? true : false;
				callback(bo);
			}else{
				layer.msg(data.msg);
			}
		});
	};
	//初始化
	function init(){
		//获取员工列表
		getUserList(function(){
			//获取机构列表
			getDeptList(function(){
				//初始化下拉列表
				initSelect();
			});
		});
	};
	init();
});