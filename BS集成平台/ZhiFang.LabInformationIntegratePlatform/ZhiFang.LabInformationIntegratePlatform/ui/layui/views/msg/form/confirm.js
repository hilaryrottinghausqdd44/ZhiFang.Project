layui.extend({
	uxutil:'ux/util',
}).use(['uxutil','form','table'],function(){
	var $ = layui.$,
		form = layui.form,
		uxutil = layui.uxutil;
		
	//危急值确认服务地址
	var CONFIRM_URL = uxutil.path.ROOT + "/ServerWCF/IMService.svc/ST_UDTO_SCMsgByConfirm";
	//获取当前科室医生列表
	var GET_DEPT_DOCTOR_LIST_URL = uxutil.path.ROOT + "/ServerWCF/RBACService.svc/RBAC_UDTO_GetHREmployeeByHRDeptID";
	//获取所有医生列表
	var GET_ALL_DOCTOR_LIST_URL = uxutil.path.ROOT + "/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHREmployeeByHQL";
	//获取挂靠医生列表
	var GET_LINK_DOCTOR_LIST_URL = uxutil.path.ROOT + "/ServerWCF/IMService.svc/ST_UDTO_SearchCVCriticalValueEmpIdDeptLinkByHQL";
	//获取用户信息
	var GET_EMP_INFO_URL = uxutil.path.ROOT + "/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACUserByHQL";
	//消息确认处理身份验证
	var IS_USER_VALID_INTERFACE_URL = uxutil.path.ROOT + "/ServerWCF/RBACService.svc/CheckAccountPWDByInterFace";
	//新增账号
	var ADD_USER_URL = uxutil.path.ROOT + "/ServerWCF/RBACService.svc/RBAC_UDTO_AddRBACUser";
	
	//医生护士注册服务地址
	var DOCTOR_ON_REG_URL = uxutil.path.ROOT + "/ServerWCF/RBACService.svc/CV_AddDoctorOrNurse";
	//获取平台机构信息
	var GET_DEPT_INFO_URL = uxutil.path.ROOT + "/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHRDeptById";
	//新增危急值员工和部门关系
	var ADD_LINK_URL = uxutil.path.ROOT + '/ServerWCF/IMService.svc/ST_UDTO_AddCVCriticalValueEmpIdDeptLink';
	
	//外部参数
	var PARAMS = uxutil.params.get(true);
	//消息ID
	var ID = PARAMS.ID;
	//接收科室ID
	var RECDEPTID = PARAMS.RECDEPTID;
	//当前科室医生列表
	var DEPT_DOCTOR_LIST = null;
	//获取所有医生列表
	var ALL_DOCTOR_LIST = null;
	//仅显示当前科室医生，默认勾选
	var CHECKED = true;
	
	//提交
	$("#submit_button").on("click",function(){
		var account = $("#account").val(),
			pwd = $("#pwd").val();
			
		if(account == '' || pwd == ''){
			layer.msg('账号和密码不能为空！',{icon:5});
			return;
		}
		//判断用户是否有效
		isUserValid(account,pwd,function(){
			getEmpInfoByAccount($("#account").val(),function(empInfo){
				onComfirm(empInfo);
			});
		});
	});
	//取消
	$("#cancel_button").on("click",function(){
		parent.layer.closeAll('iframe');
	});
	form.on('checkbox(isDept)',function(data){
		CHECKED = data.elem.checked;
		//初始化医生HTML
		initDoctorHtml();
	});
	
	//危急值确认
	function onComfirm(empInfo){
		var entity = {
			"Id":ID,
			"ConfirmerID":empInfo.RBACUser_HREmployee_Id,
			"ConfirmerName":empInfo.RBACUser_HREmployee_CName,
			"ConfirmerCode":empInfo.RBACUser_HREmployee_UseCode,
			"ConfirmerCodeHIS":"",
			"ConfirmIPAddress":"",
			"ConfirmMemo":$("#Memo").val() || '',
			"RecDeptID":RECDEPTID
		};
		layer.load();
		uxutil.server.ajax({
			url:CONFIRM_URL,
			type:'post',
			data:JSON.stringify({entity:entity})
		},function(result){
			layer.closeAll('loading');
			if(result.success){
				layer.msg('保存成功', {
					time:500
				}, function(){
					parent.layer.closeAll('iframe');
					parent.parent.layer.closeAll('iframe');
					parent.parent.afterUpdate(ID);
				});
			}else{
				//layer.msg(result.msg);
				var isOutDept = (result.msg.indexOf('消息确认人不在接收部门内部') == -1 ? false : true);
				if(isOutDept){
					//获取平台机构信息
					GetDeptInfoById(RECDEPTID,function(dept){
						addLink(entity.ConfirmerID,entity.ConfirmerName,RECDEPTID,dept.CName,function(){
							onComfirm(entity);
						});
					});
				}else{
					layer.msg(result.msg,{icon:5});
				}
			}
		},true);
	};
	//创建人员部门关系
	function addLink(EmpID,EmpName,DeptID,DeptName,callback){
		var entity = {
			EmpID:EmpID,
			EmpName:EmpName,
			DeptID:DeptID,
			DeptName:DeptName,
			IsUse:true
		};
		
		uxutil.server.ajax({
			url:ADD_LINK_URL,
			type:'post',
			data:JSON.stringify({entity:entity})
		},function(result){
			if(result.success){
				callback();
			}else{
				layer.msg(result.msg);
			}
		},true);
	};
	//根据账号获取用户信息
	function getEmpInfoByAccount(account,callback){
		var url = GET_EMP_INFO_URL;
		
		url += '?isPlanish=true&fields=RBACUser_HREmployee_Id,RBACUser_HREmployee_CName,RBACUser_HREmployee_UseCode,RBACUser_HREmployee_HRDept_Id';
		url += "&where=rbacuser.Account='" + account + "'";
		
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data.success){
				if(data.value.list && data.value.list.length == 1){
					callback(data.value.list[0]);
				}
			}else{
				layer.msg('获取确认人信息错误');
			}
		});
	};
	//根据账号密码判断用户是否有效
	function isUserValid(account,pwd,callback){
		var url = IS_USER_VALID_INTERFACE_URL;
		url += '?Account=' + account + '&PWD=' + pwd;
			
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data.success){
				callback();
			}else{
				if(data.ResultCode == '1'){//需要新增账号
					openUserWin(account,pwd,data.value,callback);
				}else{
					layer.msg(data.msg,{icon:5});
				}
			}
		},true);
	};
	//新增账号密码弹出框
	function openUserWin(account,pwd,empId,callback){
		var html = [];
		html.push('<div class="layui-form layui-form-pane" style="padding:10px 20px 0 20px;">');
		html.push(
			'<div class="layui-form-item" style="margin-bottom:5px;">' +
				'<label class="layui-form-label" style="width:60px;">账号</label>' +
				'<div class="layui-input-block" style="margin-left:60px;">' +
					'<input type="text" placeholder="请输入账号" class="layui-input" value="' + account + '">' +
				'</div>' +
			'</div>'
		);
		html.push(
			'<div class="layui-form-item" style="margin-bottom:5px;">' +
				'<label class="layui-form-label" style="width:60px;">密码</label>' +
				'<div class="layui-input-block" style="margin-left:60px;">' +
					'<input type="text" placeholder="请输入密码" class="layui-input" value="' + pwd + '">' +
				'</div>' +
			'</div>'
		);
		html.push('</div>');
		
		layer.open({
			type:1,
			title:'首次操作，请确认账号密码',
			content:html.join(''),
			btn:['确定'],
			yes:function(index,layero){
				var inputs = layero.find('.layui-input');
				var account = $(inputs[0]).val();
				var pwd = $(inputs[1]).val();
				if(account == '' || pwd == ''){
					layer.msg('账号和密码不能为空！',{icon:5});
				}else{
					addUser(account,pwd,empId,callback);
				}
			}
		});
	};
	//新增账号
	function addUser(account,pwd,empId,callback){
		var params = {
			entity:{
				HREmployee:{Id:empId,DataTimeStamp:[0,0,0,0,0,0,0,0]},
				Account:account,
				EnMPwd:true,//密码可修改
				PwdExprd:true,//密码永不过期
				PWD:pwd
			}
		};
		uxutil.server.ajax({
			url:ADD_USER_URL,
			type:'post',
			data:JSON.stringify(params)
		},function(result){
			if(result.success){
				callback();
			}else{
				layer.msg('新增账号失败！',{icon:5});
			}
		},true);
	};
	//获取平台机构信息
	function GetDeptInfoById(id,callback){
		var url = GET_DEPT_INFO_URL + "?id=" + id + '&fields=HRDept_Id,HRDept_CName,HRDept_StandCode,HRDept_DeveCode';
		
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data.success){
				if(data.value){
					callback(data.value);
				}else{
					layer.msg('当前平台机构未找到！',{icon:5});
				}
			}else{
				layer.msg('获取平台机构错误：'+data.msg,{icon:5});
			}
		});
	};
});