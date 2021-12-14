layui.extend({
	uxutil:'ux/util'
}).use(['uxutil','form','laydate'],function(){
	var $ = layui.$,
		form = layui.form,
		uxutil = layui.uxutil;
	//获取申请列表服务
	var GET_SACCOUNT_REGISTER_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LIIPCommonService.svc/ST_UDTO_SearchSAccountRegisterById';
	//修改服务地址
	var UPDATE_INFO_URL = uxutil.path.ROOT + "/ServerWCF/LIIPCommonService.svc/ST_UDTO_ApprovalSAccountRegister";
	//获取医院列表服务
	var GET_HOSPITAL_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LIIPCommonService.svc/ST_UDTO_SearchBHospitalByHQL?isPlanish=true';
    //获取角色列表服务
	var GET_ROLE_LIST_URL = uxutil.path.ROOT + '/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACRoleByHQL?isPlanish=true';
    //获取员工角色表
    var GET_EMP_ROLE_LIST_URL = uxutil.path.ROOT + '/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACEmpRolesByHQL?isPlanish=true';
	
	var DEFAULT_DATA = {},
	    AFTER_SAVE = function(){};//保存后回调函数
	    
	//审批通过保存 医院与角色不允许为空,审批备注允许为空
	$("#submit").click(function (e) {
		e.preventDefault();
		$('#HospitalIdIDList').attr("lay-verify","required");
		$('#RolesIDList').attr("lay-verify","required");
		$("[name='ApprovalInfo']").attr("lay-verify","");
		form.render('select');
	});
	//审批通过保存
	form.on("submit(submit)",function(data){
       
		onSubmit(getParams(data.field),true);
		return false; //阻止表单跳转。如果需要表单跳转，去掉这段即可。
	});
	//审批退回保存允许医院和角色允许为空,审批备注不允许为空
	$("#submit_back").click(function (e) {
		e.preventDefault();
		$('#HospitalIdIDList').attr("lay-verify","");
		$('#RolesIDList').attr("lay-verify","");
		$("[name='ApprovalInfo']").attr("lay-verify","required");
		form.render('select');
	});
	//审批退回保存
	form.on("submit(submit_back)",function(data){
		//审批退回时，需要验证审批备注不能为空
	    if(!data.field.ApprovalInfo){
	    	layer.msg('审批备注不能为空',{ icon: 5, anim: 6 ,time:2000});
	    	return false; //阻止表单跳转。如果需要表单跳转，去掉这段即可。
	    }

		onSubmit(getParams(data.field),false);
		return false; //阻止表单跳转。如果需要表单跳转，去掉这段即可。
	});
	//取消
	$("#cancel_button").on("click",function(){
		parent.layer.closeAll('iframe');
	});
	//post data
    function getParams(data,bo){
    	
    	var params={
    		ApprovalInfo:data.ApprovalInfo,
            IsPass:bo
    	};
    	
    	if(data.RolesIDList)params.RolesIDList=[data.RolesIDList];
    	if(data.HospitalIdIDList){
    		params.HospitalIdIDList=[data.HospitalIdIDList];
			params.HospitalID = $("#HospitalIdIDList").val();	//医院字典ID
			params.HospitalCode = $("#HospitalIdIDList option:selected").attr("data-HospitalCode");   //医院编码
		}
    	return params;
    }
	function onSubmit(params,IsPass){
		var config = {type:'post'};
		config.url = UPDATE_INFO_URL;
		params.Id = DEFAULT_DATA.Id;
		
        config.data = JSON.stringify({
			entity:params,
			IsPass:IsPass
		});
		
		uxutil.server.ajax(config,function(data){
			if(data.success){
				AFTER_SAVE(data.value);
			}else{
				layer.msg(data.ErrorInfo);
			}
		});
	}
	
	//初始化数据
	window.initData = function(data,afterSave){
		if(typeof afterSave == 'function'){
			AFTER_SAVE = afterSave;
		}
		//DEFAULT_DATA = data;
		//因为数据为页面外部调用传入，
		//layui的442行each处理中代码if(obj.constructor === Object)会将data与Object比对返回false
		//所以采用赋值方式创建新的Object，规避该判断
		DEFAULT_DATA = {};
		for(var key in data){
			//日期格式处理
			if(key == 'Birthday' && data[key])data[key]=uxutil.date.toString(data[key],true);
			DEFAULT_DATA[key] = data[key];
		}
		//审批按钮
		disabledbtn(DEFAULT_DATA);
		//初始化系统下拉框
		initSystemSelect(function(){
			form.val("form",DEFAULT_DATA);
		});
	}

	//获取医院表
	function selectHospitalList(callback){
		var fields = ['Id','HospitalCode','Name'],
			url = GET_HOSPITAL_LIST_URL + '&where=bhospital.IsUse=true';
		url += '&fields=BHospital_' + fields.join(',BHospital_');
		
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data){
				var list = (data.value || {}).list || [];
				callback(list);
			}else{
				layer.msg(data.msg);
			}
		});
	}
	//获取角色列表
	function selectRoleList(callback){
		var fields = ['Id','CName'],
			url = GET_ROLE_LIST_URL + '&where=rbacrole.IsUse=true';
		url += '&fields=RBACRole_' + fields.join(',RBACRole_');
		
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data){
				var list = (data.value || {}).list || [];
				callback(list);
			}else{
				layer.msg(data.msg);
			}
		});
	}
	//获取RBAC_EmpRoles列表
	function selectEmpRoles(empId,callback){
		var fields = ['RBACRole_Id'],
			url = GET_EMP_ROLE_LIST_URL + '&where=rbacemproles.HREmployee.Id='+empId;
		url += '&fields=RBACEmpRoles_' + fields.join(',RBACEmpRoles_');
		
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data){
				var list = (data.value || {}).list || [];
				callback(list);
			}else{
				layer.msg(data.msg);
			}
		});
    }
	//初始化系统下拉框
	function initSystemSelect(callback){
		selectHospitalList(function(list){
			var len = list.length,
				htmls = ['<option value="">请选择医院</option>'];
				
			for(var i=0;i<len;i++){
				htmls.push("<option value='" + list[i].BHospital_Id + "' data-HospitalCode ='" + list[i].BHospital_HospitalCode+"'>" + list[i].BHospital_Name + "</option>");
			}
			$("#HospitalIdIDList").html(htmls.join(""));
			form.render('select');
			//还原医院
			if(DEFAULT_DATA.HospitalID)form.val("form",{HospitalIdIDList:DEFAULT_DATA.HospitalID});
		});
		selectRoleList(function(list){
			var len = list.length,
				htmls = ['<option value="">请选择角色</option>'];
				
			for(var i=0;i<len;i++){
				htmls.push('<option value="' + list[i].RBACRole_Id + '">' + list[i].RBACRole_CName + '</option>');
			}
			$("#RolesIDList").html(htmls.join(""));
			form.render('select');
			if(DEFAULT_DATA.StatusId!='0'){
			    //还原角色（需要用EmpID去员工角色表RBAC_EmpRoles查）
			    if(DEFAULT_DATA.EmpID){
			    	selectEmpRoles(DEFAULT_DATA.EmpID,function(list){
			    		if(list.length>0){
			                form.val("form",{RolesIDList:list[0].RBACEmpRoles_RBACRole_Id});
			    		}
			    	});
			    }
			}
		});
		callback();
	}
	//按钮禁用（不能重复操作
	function disabledbtn(DEFAULT_DATA){
	    if(DEFAULT_DATA.StatusId!='0'){
	    	$("#submit").prop("disabled", "disabled");
            $('#submit').addClass('layui-btn-disabled');
            
            $("#submit_back").prop("disabled", "disabled");
            $('#submit_back').addClass('layui-btn-disabled'); 
	    }
	}
});