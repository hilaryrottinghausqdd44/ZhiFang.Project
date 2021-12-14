layui.extend({
	uxutil:'ux/util',
}).use(['uxutil','form','table'],function(){
	var $ = layui.$,
		form = layui.form,
		uxutil = layui.uxutil;
		
	//危急值上报服务地址
	var UPLOAD_URL = uxutil.path.LIIP_ROOT + "/ServerWCF/IMService.svc/ST_UDTO_SCMsgByWarningUpload";
	//获取机构信息
	var GET_DEPT_INFO_URL = uxutil.path.LIIP_ROOT + "/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHRDeptById";
	//获取当前科室医生列表
	var GET_DEPT_DOCTOR_LIST_URL = uxutil.path.LIIP_ROOT + "/ServerWCF/RBACService.svc/RBAC_UDTO_GetHREmployeeByHRDeptID";
	//获取所有医生列表
	var GET_ALL_DOCTOR_LIST_URL = uxutil.path.LIIP_ROOT + "/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHREmployeeByHQL";
	//获取挂靠医生列表
	var GET_LINK_DOCTOR_LIST_URL = uxutil.path.LIIP_ROOT + "/ServerWCF/IMService.svc/ST_UDTO_SearchCVCriticalValueEmpIdDeptLinkByHQL";
	//获取用户信息
	var GET_EMP_INFO_URL = uxutil.path.LIIP_ROOT + "/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACUserByHQL";
	//判断用户是否有效
	var IS_USER_VALID_URL = uxutil.path.LIIP_ROOT + "/ServerWCF/RBACService.svc/RBAC_BA_Login";
	
	//根据账号密码获取LIS医生护士用户信息
	var GET_USER_INFO_URL = uxutil.path.ROOT + "/ServerWCF/MsgManageService.svc/Msg_GetNPUserInfoByPWD";
	//获取LIS信息列表服务地址
	var GET_LIS_DICT_LIST_URL = uxutil.path.ROOT + "/ServerWCF/MsgManageService.svc/Msg_GetLisDictInfo";
	//医生护士注册服务地址
	var DOCTOR_ON_REG_URL = uxutil.path.LIIP_ROOT + "/ServerWCF/RBACService.svc/CV_AddDoctorOrNurse";
	//获取平台机构信息
	var GET_DEPT_INFO_URL = uxutil.path.LIIP_ROOT + "/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHRDeptById";
	//新增危急值员工和部门关系
	var ADD_LINK_URL = uxutil.path.LIIP_ROOT + '/ServerWCF/IMService.svc/ST_UDTO_AddCVCriticalValueEmpIdDeptLink';
	//密码解密
	var UN_COVERT_PASSWORD = uxutil.path.ROOT + '/ServerWCF/MsgManageService.svc/Msg_UnCovertPasswordCheck';
	
	//外部参数
	var PARAMS = uxutil.params.get(true);
	//消息ID
	var ID = PARAMS.ID;
	//机构ID
	var RECDEPTID = PARAMS.RECDEPTID;
	//机构信息
	var RECDEP_INFO = null;
	
	//当前科室医生列表
	var DEPT_DOCTOR_LIST = null;
	//获取所有医生列表
	var ALL_DOCTOR_LIST = null;
	//仅显示当前科室医生，默认勾选
	var CHECKED = true;
	
	//上报
	$("#submit_button").on("click",function(){
		//判断用户是否有效
		isUserValid($("#account").val(),$("#pwd").val(),function(){
			getEmpInfoByAccount($("#account").val(),function(empInfo){
				onUploadClick(empInfo);
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
	
	//危急值上报
	function onUploadClick(empInfo){
		var doctorOption = $('#doctor option:selected'),
			NPUserNo = doctorOption.val();
			
		if(!NPUserNo){
			layer.msg('请选择接电话人！',{time:1000});
			return;
		}
		
		var account = '',
			pwd = '';
			
		for(var i in ALL_DOCTOR_LIST){
			if(ALL_DOCTOR_LIST[i].NPUserNo == NPUserNo){
				account = ALL_DOCTOR_LIST[i].ShortCode;
				pwd = ALL_DOCTOR_LIST[i].PassWord;
				break;
			}
		}
		//密码解密
		onUnCovertPasswordCheck(pwd,function(value){
			pwd = value;
			//根据账号密码判断用户是否有效
			isUserValid(account,pwd,function(){
				//根据账号获取用户信息
				getEmpInfoByAccount(account,function(info){
					var fields = [
						'Id','RecDeptID','RecDeptPhoneCode','WarningUploaderFlag','WarningUploaderID','WarningUploaderName',
						'WarningUpLoadNotifyNurseID','WarningUpLoadNotifyNurseName','WarningUpLoadDateTime','WarningUpLoadMemo'
					];
					var params = {
						entity:{
							"Id":ID,
							"RecDeptID":RECDEPTID,
							"RecDeptPhoneCode":RECDEP_INFO.Tel,
							"WarningUploaderFlag":"1",
							"WarningUploaderID":empInfo.RBACUser_HREmployee_HRDept_Id,
							"WarningUploaderName":empInfo.RBACUser_HREmployee_CName,
							"WarningUpLoadNotifyNurseID":info.RBACUser_HREmployee_Id,
							"WarningUpLoadNotifyNurseName":info.RBACUser_HREmployee_CName,
							"WarningUpLoadDateTime":uxutil.date.toServerDate(uxutil.server.date.getDate()),
							"WarningUpLoadMemo":$("#Memo").val() || ''
						},
						fields:fields.join(',')
					};
					onUpload(params);
				});
			});
		});
	};
	//危急值上报
	function onUpload(params){
		uxutil.server.ajax({
			url:UPLOAD_URL,
			type:'post',
			data:JSON.stringify(params)
		},function(result){
			if(result.success){
				layer.msg('消息上报成功', {
					time:500
				}, function(){
					parent.layer.closeAll('iframe');
					parent.parent.layer.closeAll('iframe');
					parent.parent.afterUpdate(ID);
				});
			}else{
				layer.msg(result.msg);
			}
		},true);
	};
	//密码解密
	function onUnCovertPasswordCheck(pwd,callback){
		var url = UN_COVERT_PASSWORD + '?userPWD=' + pwd;
		
		uxutil.server.ajax({
			url:url,
		},function(data){
			if(data.success){
				callback(data.value);
			}else{
				layer.msg(data.msg);
			}
		});
	};
	
	
	//获取当前科室医生列表
	function getDeptDoctorList(callback){
		var url = GET_DEPT_DOCTOR_LIST_URL;
			
		url += '?where=id=' + RECDEPTID + 
		'&fields=HREmployee_CName,HREmployee_UseCode,HREmployee_Id';
		
		uxutil.server.ajax({
			url:url,
		},function(data){
			if(data.success){
				var deptDoctorList = data.value.list || [];
				var linkList = getLinkDoctorList(function(list){
					if(list.length > 0 && deptDoctorList.length > 0){
						for(var i in list){
							var inList = false;
							for(var j in deptDoctorList){
								if(list[i].HREmployee_Id == deptDoctorList[j].HREmployee_Id){
									inList = true;
								}
							}
							if(!inList){
								linkList.push(list[i]);
							}
						}
						DEPT_DOCTOR_LIST = list;
					}else{
						DEPT_DOCTOR_LIST = deptDoctorList.concat(list);
					}
					callback();
				});
			}else{
				layer.msg(data.msg);
			}
		});
	};
	//获取挂靠医生列表
	function getLinkDoctorList(callback){
		var url = GET_LINK_DOCTOR_LIST_URL;
			
		url += '?where=cvcriticalvalueempiddeptlink.DeptID=' + RECDEPTID + 
		'&fields=CVCriticalValueEmpIdDeptLink_EmpID';
		
		uxutil.server.ajax({
			url:url,
		},function(data){
			if(data.success){
				var list = data.value.list || [];
				if(list.length > 0){
					var ids = [];
					for(var i in list){
						ids.push(list[i].EmpID);
					}
					var where = 'hremployee.Id in (' + ids.join(',') + ')';
					getDoctorList(where,function(list){
						callback(list || []);
					});
				}else{
					callback([]);
				}
			}else{
				layer.msg(data.msg);
			}
		});
	};
	//获取所有医生列表
	function getAllDoctorList(callback){
		getDoctorList(null,function(list){
			ALL_DOCTOR_LIST = list;
			callback();
		});
	};
	//获取医生列表
	function getDoctorList(where,callback){
		var url = GET_ALL_DOCTOR_LIST_URL + '?fields=HREmployee_CName,HREmployee_UseCode,HREmployee_Id';
		if(where){
			url += '&where=' + where;
		}
		
		uxutil.server.ajax({
			url:url,
		},function(data){
			if(data.success){
				var list = data.value.list || [];
				callback(list);
			}else{
				layer.msg(data.msg);
			}
		});
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
		var url = IS_USER_VALID_URL;
		
		url += '?isValidate=true&strUserAccount=' + account + '&strPassWord=' + pwd;
			
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data){
				callback();
			}else{
				//layer.msg("确认人账号或密码错误！");
				//平台不存在用户信息，从LIS库中获取医生护士用户数据
				getLisUserInfoByAccount(account,pwd,function(info){
					//创建平台用户信息
					createLiipUserInfo(info,function(){
						callback();
					});
				});
			}
		});
	};
	//根据账号密码获取LIS用户信息
	function getLisUserInfoByAccount(account,pwd,callback){
		var url = GET_USER_INFO_URL + '?userName=' + account + '&userPWD=' + pwd;
			
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data){
				var list = (data.value || {}).list || [];
				if(list.length == 0){
					layer.msg('LIS账户或密码错误',{icon:5});
				}else if(list.length > 1){
					layer.msg('存在多个LIS账户数据，请确认账户唯一！',{icon:5});
				}else{
					list[0].PassWord = pwd;
					callback(list[0]);
				}
			}else{
				layer.msg("确认人账号或密码错误！",{icon:5});
			}
		});
	};
	//创建平台用户信息
	function createLiipUserInfo(info,callback){
		//Account=账户名&PWD=密码&Name=姓名&Sex=性别&Phone=电话&Type=医生
		//&DeptName=科室名称&DeptHISCode=科室HIS编码&DeptLISCode=科室LIS编码
		var entity = {
			"Account":info.ShortCode,
			"PWD":info.PassWord,
			"Name":info.CName,
			"Sex":info.Gender == '1' ? '男' : '女',
			"Phone":'',
			"Type":info.Role
		};
		//获取Lis科室表信息
		GetLisDeptInfoByDeptNo(info.DeptNo,function(lisDept){
			if(!lisDept.CName){layer.msg('LIS科室名称不能为空！',{icon:5});return;}
			if(!lisDept.LisCode){layer.msg('LIS科室LIS编码不能为空！',{icon:5});return;}
			if(!lisDept.HisCode){layer.msg('LIS科室HIS编码不能为空！',{icon:5});return;}
			
			entity.DeptName = [lisDept.CName];
			entity.DeptLISCode = [lisDept.LisCode];
			entity.DeptHISCode = [lisDept.HisCode];
			//获取平台机构信息
			getDeptInfoById(RECDEPTID,function(dept){
				if(!dept.CName){layer.msg('平台科室名称不能为空！',{icon:5});return;}
				if(!dept.StandCode){layer.msg('平台科室LIS编码不能为空！',{icon:5});return;}
				if(!dept.DeveCode){layer.msg('平台科室HIS编码不能为空！',{icon:5});return;}
			
				entity.DeptName.push(dept.CName);
				entity.DeptLISCode.push(dept.StandCode);
				entity.DeptHISCode.push(dept.DeveCode);
				
				entity.DeptName = entity.DeptName.join(',');
				entity.DeptLISCode = entity.DeptLISCode.join(',');
				entity.DeptHISCode = entity.DeptHISCode.join(',');
				
				//请求注册接口
				layer.load();
				uxutil.server.ajax({
					url:DOCTOR_ON_REG_URL,
					type:'post',
					data:JSON.stringify({entity:entity})
				},function(data){
					layer.closeAll('loading');
					if(data.success){
						callback();
					}else{
						layer.msg(data.msg,{icon:5});
					}
				});
			});
		});
	};
	
	//获取所有医生列表
	function getAllDoctorList2(callback){
		var url = GET_LIS_DICT_LIST_URL + "?dictName=NPUser&strWhere=Role='医生'";
		
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data.success){
				var list = (data.value || {}).list || [];
				ALL_DOCTOR_LIST = list;
				callback();
			}else{
				layer.msg(data.msg,{icon:5});
			}
		});
	};
	//获取当前科室医生列表
	function getDeptDoctorList2(callback){
		if(ALL_DOCTOR_LIST){
			//获取平台机构信息
			getDeptInfoById(RECDEPTID,function(info){
				var list = [];
				for(var i in ALL_DOCTOR_LIST){
					//科室HIS编码
					if(ALL_DOCTOR_LIST[i].code1 == info.DeveCode){
						list.push(ALL_DOCTOR_LIST[i]);
					}
				}
				DEPT_DOCTOR_LIST = list;
				callback();
			});
		}else{
			//获取所有医生列表
			getAllDoctorList2(function(){
				//获取平台机构信息
				getDeptInfoById(RECDEPTID,function(info){
					var list = [];
					for(var i in ALL_DOCTOR_LIST){
						//科室HIS编码
						if(ALL_DOCTOR_LIST[i].code1 == info.DeveCode){
							list.push(ALL_DOCTOR_LIST[i]);
						}
					}
					DEPT_DOCTOR_LIST = list;
					callback();
				});
			});
		}
	};
	
	
	//获取Lis科室表信息
	function GetLisDeptInfoByDeptNo(DeptNo,callback){
		var url = GET_LIS_DICT_LIST_URL + "?dictName=Department&strWhere=DeptNo=" + DeptNo;
		
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data.success){
				var list = (data.value || {}).list || [];
				if(list.length == 1){
					callback(list[0]);
				}else{
					layer.msg("LIS医生护士的科室数据存在错误！",{icon:5});
				}
			}
		});
	}
	//获取平台机构信息
	function getDeptInfoById(id,callback){
		var url = GET_DEPT_INFO_URL + "?id=" + id + '&fields=HRDept_Id,HRDept_CName,HRDept_Tel,HRDept_StandCode,HRDept_DeveCode';
		
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
	
	//初始化医生HTML
	function initDoctorHtml(){
		if(CHECKED){//当前科室医生
			if(DEPT_DOCTOR_LIST){
				initDoctorSelect(DEPT_DOCTOR_LIST);
			}else{
				//获取当前科室医生列表
				getDeptDoctorList2(function(){
					initDoctorSelect(DEPT_DOCTOR_LIST);
				});
			}
		}else{//所有医生
			if(ALL_DOCTOR_LIST){
				initDoctorSelect(ALL_DOCTOR_LIST);
			}else{
				//获取当前科室医生列表
				getAllDoctorList2(function(){
					initDoctorSelect(ALL_DOCTOR_LIST);
				});
			}
		}
	};
	//初始化医生组件
	function initDoctorSelect(list){
		var doctor = ['<option value="">请选择一个医生</option>'];
		
		for(var i in list){
			doctor.push('<option value="' + list[i].NPUserNo + '">' + list[i].CName +'</option>');
		}
		$("#doctor").html(doctor.join(''));
		form.render('select');
	}
	//初始化
	function init(){
		//获取机构信息
		getDeptInfoById(RECDEPTID,function(info){
			RECDEP_INFO = info;
			//初始化接收科室信息
			$("#RecDeptName").html(info.CName);
			$("#HRDeptTel").val(info.Tel);
		});
		
		//初始化医生组件
		initDoctorHtml();
	};
	
	//初始化
	init();
});