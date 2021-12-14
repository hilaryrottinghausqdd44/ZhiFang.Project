layui.extend({
	uxutil:'ux/util',
	uxform: 'ux/form'
}).use(['uxutil','uxform','table'],function(){
	var $=layui.$,
		form = layui.uxform,
		uxutil = layui.uxutil;
		
	//危急值处理服务地址
	var HANDLE_URL = uxutil.path.ROOT + '/ServerWCF/IMService.svc/ST_UDTO_AddSCMsgHandle';
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
	var ADD_LINK_URL = uxutil.path.ROOT + "/ServerWCF/IMService.svc/ST_UDTO_AddCVCriticalValueEmpIdDeptLink";
	//获取系统参数列表服务地址
	var GET_PARAMS_LIST_URL = uxutil.path.ROOT + '/ServerWCF/SingleTableService.svc/ST_UDTO_SearchBParameterByHQL';
	//调用第三方服务地址
	var TO_THIRD_PATRY_URL = uxutil.path.ROOT + '/ServerWCF/IMService.svc/ST_UDTO_ReSendSCMsgHandleToHISInterface'
	//处理意见下拉参数维护编码
	var HANDLE_DESC_CODE = "Msg_CV_HandleDesc";
	
	//外部参数
	var PARAMS = uxutil.params.get(true);
	//消息ID
	var ID = PARAMS.ID;
	//接收科室ID
	var RECDEPTID = PARAMS.RECDEPTID;
	//处理意见列表
	var HANDLE_DESC_LIST = [];
	
	//处理ID，调用第三方出错误使用
	var HANDLE_ID = null;
	
	
	$('#USERNAME').html('当前登录账户：' + uxutil.cookie.get(uxutil.cookie.map.USERNAME));
	
	//提交
	$('#submit_button').on('click',function(){
		var account = $("#account").val(),
			pwd = $("#pwd").val();
			
		if($("#HandleDesc").next().first().find('input').val().trim() == ''){
			layer.msg('处理意见不能为空！',{icon:5});
			return;
		}
		if(account == '' || pwd == ''){
			layer.msg('账号和密码不能为空！',{icon:5});
			return;
		}
			
		//判断用户是否有效
		isUserValid(account,pwd,function(){
			getEmpInfoByAccount($("#account").val(),function(empInfo){
				onHandle(empInfo);
			});
		});
	});
	//取消
	$('#cancel_button').on('click',function(){
		parent.layer.closeAll('iframe');
	});
	
	$("#tothirdparty_button").on("click",function(){
		onSubmitToThirdParty();
	});
	//手工调用第三方服务
	function onSubmitToThirdParty(){
		layer.load();
		uxutil.server.ajax({
			url:TO_THIRD_PATRY_URL,
			data:{
				SCMsgID:ID,
				SCMsgHandleID:HANDLE_ID,
				PWD:$("#pwd").val()
			}
		},function(data){
			layer.closeAll('loading');
			if(data.success){
				parent.layer.closeAll('iframe');
				parent.parent.layer.closeAll('iframe');
				//parent.parent.afterUpdate(ID);
			}else{
				layer.msg(data.msg);
			}
		},true);
	};
	
	//危急值处理
	function onHandle(empInfo){
		var entity = {
			"MsgID":ID,
			"HandlerID":empInfo.RBACUser_HREmployee_Id,
			"HandlerName":empInfo.RBACUser_HREmployee_CName,
			"HandleDeptID":RECDEPTID,
			"HandleDesc":$("#HandleDesc").next().first().find('input').val().trim() || '',
			"Memo":$("#Memo").val() || '',
			"HandleNodeName":"",
			"HandleNodeIPAddress":"",
			"HandlerPWD":$("#pwd").val()
		};
		layer.load();
		uxutil.server.ajax({
			url:HANDLE_URL,
			type:'post',
			data:JSON.stringify({entity:entity})
		},function(result){
			layer.closeAll('loading');
			//result.ResultCode= 0：异常1：成功-1：保存失败-2：调用HIS接口失败
			if(result.success){
				layer.msg('保存成功', {
					time:500
				}, function(){
					parent.layer.closeAll('iframe');
					parent.parent.layer.closeAll('iframe');
					parent.parent.afterUpdate(ID);
				});
			}else{
				//调用HIS接口失败，转为手工处理
				if(result.ResultCode == '-2'){
					HANDLE_ID = result.value.SCMsgHandleID;
					
					$("#confirm_button",window.parent.document).hide();
					$("#handle_button",window.parent.document).hide();
					
					$("#submit_button").hide();
					$("#tothirdparty_button").removeClass('layui-hide');
					parent.parent.afterUpdate(ID);
					return;
				}
				
				//layer.msg(result.msg);
				var isOutDept = (result.msg.indexOf('消息处理人不在接收部门内部') == -1 ? false : true);
				if(isOutDept){
					//获取平台机构信息
					GetDeptInfoById(RECDEPTID,function(dept){
						addLink(empInfo.RBACUser_HREmployee_Id,empInfo.RBACUser_HREmployee_CName,RECDEPTID,dept.CName,function(){
							onHandle(empInfo);
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
		
		url += '?isPlanish=true&fields=RBACUser_HREmployee_Id,RBACUser_HREmployee_CName,RBACUser_HREmployee_UseCode';
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
	//获取处理界面参数
	function getHandleParams(callback){
		var where = "bparameter.ParaNo='" + HANDLE_DESC_CODE + "'";
		getParamList(where,function(list){
			for(var i in list){
				if(list[i].ParaNo === HANDLE_DESC_CODE){//处理意见下拉内容
					HANDLE_DESC_LIST = list[i].ParaValue.split('|');
				}
			}
			callback();
		});
	};
	//获取系统参数
	function getParamList(where,callback){
		var url = GET_PARAMS_LIST_URL + "?fields=BParameter_ParaNo,BParameter_ParaValue&where=" + where;
			
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data.success){
				var list = (data.value || {}).list || [];
				callback(list);
			}else{
				layer.msg(data.msg);
			}
		});
	};
	//初始化页面
	function initHtml(){
		//获取处理界面参数
		getHandleParams(function(){
			var html = [];
			html.push('<select name="HandleDesc" id="HandleDesc" lay-filter="HandleDesc" lay-search lay-noclear>');
			html.push('<option value="">处理意见</option>');
			
			for(var i in HANDLE_DESC_LIST){
				html.push('<option value="' + HANDLE_DESC_LIST[i] + '">' + HANDLE_DESC_LIST[i] + '</option>');
			}
			
			html.push('</select>');
			$("#HandleDesc_Div").html(html.join(''));
			form.render();
		});
	};
	initHtml();
});