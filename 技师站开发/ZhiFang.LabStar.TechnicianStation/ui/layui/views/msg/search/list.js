layui.extend({
	uxutil:'ux/util',
	uxtable:'ux/table'
}).use(['uxutil','uxtable','form','laydate'],function(){
	var $=layui.$,
		form = layui.form,
		laydate = layui.laydate,
		uxtable = layui.uxtable,
		uxutil = layui.uxutil;
	
	//获取系统列表
	var GET_SYSTEM_LIST_URL = uxutil.path.LIIP_ROOT + "/ServerWCF/LIIPService.svc/ST_UDTO_SearchIntergrateSystemSetByHQL";
	//获取消息类型列表
	var GET_MSG_TYPE_LIST_URL = uxutil.path.LIIP_ROOT + "/ServerWCF/IMService.svc/ST_UDTO_SearchSCMsgTypeByHQL";
	//获取消息列表服务地址
	var GET_MSG_LIST_URL = uxutil.path.LIIP_ROOT + '/ServerWCF/IMService.svc/ST_UDTO_SearchSCMsgByHQL';
	//获取危急值员工部门列表
	var GET_DEPTIDS_URL = uxutil.path.LIIP_ROOT + "/ServerWCF/IMService.svc/ST_UDTO_SearchCVCriticalValueEmpIdDeptLinkByHQL";
	//获取系统参数列表服务地址
	var GET_PARAMS_LIST_URL = uxutil.path.LIIP_ROOT + '/ServerWCF/SingleTableService.svc/ST_UDTO_SearchBParameterByHQL';
	//获取员工角色列表
	var GET_EMP_ROLE_LIST_URL = uxutil.path.LIIP_ROOT + "/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACEmpRolesByHQL";
	
	//危急值处理后是否调用第三方服务-参数编码
	var AFTER_HANDLE_CODE = "ZF_LIIP_Msg_CV_After_Handle";
	//危急值处理后是否调用第三方服务
	var AFTER_HANDLE = false;
	
	//系统列表
	var ALL_SYSTEM_LIST = [];
	//消息类型列表
	var MSG_TYPE_LIST = [];
	
	//检验之星系统编码
	var SYSTEM_CODE = 'ZF_LAB_START';
	//危急值消息编码
	var MSG_TYPE_CODE = 'ZF_LAB_START_CV';
	//需要加载的LIS字典总数
	var LIS_DICT_COUNT = 4;
	//已加载的LIS字典数量
	var LIS_DICT_NUM = 0;
	
	//危急值员工部门IDS
	var DEPT_IDS = [];
	//就诊类型列表
	var JZTYPE_LIST = null;
	//开单科室列表
	var DEPT_LIST = null;
	//小组列表
	var SENDSECTION_LIST = null;
	//产生用户列表
	var CREATOR_LIST = null;
	
	//状态列表-0无，1超时未确认，2待确认，3待处理，4完结，5未完结，6超时未处理
	var STATUS_LIST = [
		"",
		"scmsg.ConfirmFlag='0' and scmsg.RequireConfirmTime < CONVERT(varchar(100),GETDATE(),20)",
		"scmsg.ConfirmFlag='0' and (scmsg.RequireConfirmTime is null or scmsg.RequireConfirmTime > CONVERT(varchar(100),GETDATE(),20))",
		"scmsg.ConfirmFlag='1' and scmsg.HandleFlag='0' and (scmsg.LastRequireHandleTime is null or scmsg.LastRequireHandleTime >= CONVERT(varchar(100),GETDATE(),20))",
		"scmsg.HandleFlag='1'",
		"scmsg.HandleFlag='0'",
		"scmsg.ConfirmFlag='1' and scmsg.HandleFlag='0' and (scmsg.LastRequireHandleTime is not null and scmsg.LastRequireHandleTime < CONVERT(varchar(100),GETDATE(),20))"
	];
	
	var EMPID = uxutil.cookie.get(uxutil.cookie.map.USERID);
	var DEPTID = uxutil.cookie.get(uxutil.cookie.map.DEPTID);
	
	//医生身份
	var IS_DOCTOR = false;
	//护士身份
	var IS_NURSE = false;
	//技师身份
	var IS_TECH = false;
	
	var config = {
		elem:$("#table"),
		toolbar:'#table-toolbar-top',
		height:'full-40',
		defaultLoad:true,
		page:true,
		initSort: {
			field:'DataAddTime',//排序字段
			type:'desc'
		},
		cols:[[
			{field:'RecDeptName',width:100,title:'开单科室',sort:true},
			{field:'DoctorName',width:100,title:'开单医生',sort:true},
			{field:'RecSickTypeName',width:90,title:'就诊类型'},
			{field:'PatientName',width:90,title:'病人姓名'},
			{field:'PatientSex',width:60,title:'性别'},
			{field:'PatientAge',width:60,title:'年龄'},
			{field:'PatNo',width:100,title:'病历号'},
			{field:'SampleNo',width:100,title:'样本号',sort:true},
			{field:'MsgAll',minWidth:200,title:'危急值消息内容'},
			
			{field:'ConfirmFlag',width:100,title:'状态',templet:function(d){
				var result = '';
				
				if(d.ConfirmFlag == '0'){//未确认过的
					var isConfirmTimeout = false;
					var nowStr = uxutil.date.toString(new Date()).replace(/-/g,'/');
					if(d.RequireConfirmTime && nowStr > d.RequireConfirmTime){
						isConfirmTimeout = true;
					}
					if(isConfirmTimeout){
						result = '<span style="color:#FF5722;">超时未确认</span>';
					}else{
						result = '<span style="color:#1E9FFF;">待确认</span>';
					}
				}else if(d.ConfirmFlag == '1'){
					if(d.HandleFlag == '0'){
						var isHandleTimeout = false;
						var nowStr = uxutil.date.toString(new Date()).replace(/-/g,'/');
						if(d.LastRequireHandleTime && nowStr > d.LastRequireHandleTime){
							isHandleTimeout = true;
						}
						if(isHandleTimeout){
							result = '<span style="color:red;">超时未处理</span>';
						}else{
							result = '<span style="color:#1E9FFF;">待处理</span>';
						}
					}else{
						result = '<span style="color:#e0e0e0;">已处理</span>';
					}
				}
				
				return result;
			}},
			{field:'DataAddTime',width:160,title:'产生时间',sort:true},
			{field:'RequireConfirmTime',width:160,title:'要求确认时间',templet:function(d){
				var result = '';
				var RequireConfirmTime = d.RequireConfirmTime;
				
				if(!d.ConfirmDateTime){//未确认
					var timesStr = uxutil.date.difference(RequireConfirmTime,new Date());
					if(timesStr && timesStr.slice(0,1) != '-'){
						result = '<span style="color:#FF5722;" title="超时' + 
							timesStr + '">' + RequireConfirmTime + '</span>';
					}else{
						result = RequireConfirmTime || '';
					}
				}else{//已确认
					result = RequireConfirmTime || '';
				}
				
				return result;
			}},
			{field:'WarningUpLoadDateTime',width:160,title:'上报时间',sort:true},
			{field:'ConfirmDateTime',width:160,title:'实际确认时间',sort:true,templet:function(d){
				var result = '';
				var RequireConfirmTime = d.RequireConfirmTime;
				var ConfirmDateTime = d.ConfirmDateTime;
				
				var timesStr = uxutil.date.difference(RequireConfirmTime,ConfirmDateTime);
				if(timesStr && timesStr.slice(0,1) != '-'){
					result = '<span style="color:#FF5722;" title="超时' + 
						timesStr + '">' + ConfirmDateTime + '</span>';
				}else{
					result = ConfirmDateTime || '';
				}
				
				return result;
			}},
			{field:'LastRequireHandleTime',width:165,title:'要求处理时间',sort:false,templet:function(d){
				var result = '';
				var LastRequireHandleTime = d.LastRequireHandleTime;
				var isTimeout = false;
				
				if(!d.LastRequireHandleTime){//未处理
					var nowStr = uxutil.date.toString(new Date()).replace(/-/g,'/');
					if(LastRequireHandleTime && nowStr > LastRequireHandleTime){
						isTimeout = true;
					}
					if(isTimeout){
						result = '<span style="color:#FF5722;">' + LastRequireHandleTime + '</span>';
					}else{
						result = LastRequireHandleTime || '';
					}
				}else{//已处理
					result = LastRequireHandleTime || '';
				}
				
				return result;
			}},
			{field:'HandlingDateTime',width:160,title:'处理时间',sort:true},
			{field:'Id',title:'ID',width:180,hide:true},
			{field:'SendSectionName',width:90,title:'小组'},
			{field:'SenderName',width:90,title:'发送者'},
			{field:'MsgTypeCode',width:180,title:'类型代码',hide:true},
			{field:'MsgContent',width:100,title:'消息内容',hide:true},
			{field:'HandleFlag',width:90,title:'处理标志',hide:true}
		]],
		changeData:function(data){
			return changeData(data);
		}
	};
	//列表对象
	var tableInd = null;
	
	//状态下拉监听
	form.on('select(status)', function(data){
		onSearch();
	});
	//就诊类型
	form.on('select(JzType)', function(data){
		onSearch();
	});
	//开单科室
	form.on('select(Dept)', function(data){
		onSearch();
	});
	//小组
	form.on('select(SendSection)', function(data){
		onSearch();
	});
	//产生用户
	form.on('select(Creator)', function(data){
		onSearch();
	});
	
	form.on('select(DateTimeRangeType)', function(data){
		var value = data.value;
		if(value != ""){
			var today = uxutil.date.toString(new Date(),true);
			var start = uxutil.date.toString(uxutil.date.getNextDate(today,1-parseInt(value)),true);
			
			initDateComponent(start + ' - ' + today);
			onSearch();
		}else{
			initDateComponent();
			onSearch();
		}
	});
	
	//日期时间范围
	var LAYDATE_DATES = null;
	function initDateComponent(defaultValue){
		if(!defaultValue){
			$("#dates").val("");
		}
		laydate.render({
			elem:'#dates',
			range:true,
			value:defaultValue,
			done: function(value,date,endDate){
				$('#DateTimeRangeType').val("");
				setTimeout(function(){
					onSearch();
				},100);
			}
		});
	};
	initDateComponent();
	
	//数据处理
	function changeData(data){
		var MsgObjJson = $.parseJSON(data.MsgContent),//消息对象
			MsgContent = MsgObjJson.MSG.MSGCONTENT,//消息内容
			MsgTitle = MsgContent.MSGTITLE,//消息标题
			MsgKey = MsgContent.MSGKEY,//病人信息
			MsgList = MsgContent.MSGBODY.MSG;//消息列表
			
		//XML格式的消息支持一个消息多条内容，如果是单条内容的自动转变成数组
		if(!$.isArray(MsgList)){
			MsgList = [MsgList];
		}
		
		data.SendSectionName = MsgKey.SECTIONNAME;//小组名称
		data.SampleNo = MsgKey.SAMPLENO;//样本号
		data.JzType = MsgKey.SICKTYPENAME;//就诊类型
		data.DeptName = MsgKey.DEPTNAME;//开单科室
		data.DoctorName = MsgKey.DOCTOR;//开单医生
		data.PatNo = MsgKey.PATNO;//病历号
		data.PatientName = MsgKey.CNAME;//病人姓名
		data.PatientSex = MsgKey.GENDERNAME;//性别
		data.PatientAge = MsgKey.AGE + MsgKey.AGEUNITNAME;//年龄
		
		var MsgAll = [];
		for(var i in MsgList){
			var status = MsgList[i].RESULTSTATUS;
			if(status.indexOf("H") != -1){
				status = '<span style="color:red;">' + status + '</span>';
			}if(status.indexOf("L") != -1){
				status = '<span style="color:blue;">' + status + '</span>';
			}
			MsgAll.push(MsgList[i].TESTITEMNAME + ' ' + MsgList[i].REPORTVALUEALL + ' ' + status);
		}
		data.MsgAll = MsgAll.join('</br>');
		
		data.ItemName = "";//检查项目
		data.ResultValue = "";//结果值
		data.ResultStatus = "";//状态
		data.OutTimes = 0;//超时分钟
		
		return data;
	};
	//查询
	function onSearch(systemId){
		var DateTimeRangeType = $('#DateTimeRangeType option:selected').val(),
			dates = $("#dates").val(),
			status = $('#status option:selected').val(),
			JzType = $('#JzType option:selected').val(),
			Dept = $('#Dept option:selected').val(),
			SendSection = $('#SendSection option:selected').val(),
			SendSectionName = $('#SendSection option:selected').text(),
			Creator = $('#Creator option:selected').val(),
			CreatorName = $('#Creator option:selected').text();
			
		//消息过滤条件
		var where = [];
		
		//条件:使用中+危急值类型
		where.push("scmsg.IsUse=1 and scmsg.MsgTypeCode='" + MSG_TYPE_CODE + "'");
		
		//条件：医生+有权限的处理部门 or 护士+有权限的确认部门 or 技师+发送者是本人
		var userWhere = [];
		//过滤条件：医生+有权限的处理部门
		if(IS_DOCTOR){
			userWhere.push("scmsg.RecDeptID in (" + DEPT_IDS.join(',') + ")");
		}
		//过滤条件：护士+有权限的确认部门
		if(IS_NURSE){
			var nurseWhere = [];
			//确认人和处理人区分字段
			nurseWhere.push("scmsg.ConfirmDeptID in (" + DEPT_IDS.join(',') + ")");
			//确认人和处理人不区分字段
			nurseWhere.push("(scmsg.ConfirmDeptID is null and scmsg.RecDeptID in (" + DEPT_IDS.join(',') + "))");
			userWhere.push("(" + nurseWhere.join(" or ") + ")");
		}
		//过滤条件：技师=本人
		if(IS_TECH){
			userWhere.push("scmsg.SenderID=" + EMPID);
		}
		
		if(userWhere.length > 0){
			where.push("(" + userWhere.join(" or ") + ")");
		}
		
		if(dates){
			var splitField = $("#dates").attr("placeholder");
			var dateArr = dates.split(splitField);
			where.push("scmsg.DataAddTime >='" + dateArr[0] +"' and scmsg.DataAddTime <'" + 
				uxutil.date.toString(uxutil.date.getNextDate(dateArr[1]),true) + "'");
		}
		if(status){where.push(STATUS_LIST[status]);}
		if(JzType){where.push("scmsg.RecSickTypeID=" + JzType);}
		if(Dept){where.push("scmsg.RecDeptID=" + Dept);}
		if(SendSection){where.push("scmsg.SendSectionName='" + SendSectionName + "'");}
		if(Creator){where.push("scmsg.SenderName='" + CreatorName + "'");}
			
		onLoad({"where":where.join(' and ')});
		
		$('#DateTimeRangeType').val(DateTimeRangeType);
		LAYDATE_DATES = laydate.render({
			elem:'#dates',
			range:true,
			value:dates,
			done: function(value,date,endDate){
				$('#DateTimeRangeType').val("");
				setTimeout(function(){
					onSearch();
				},100);
			}
		});
		
		$("#status").val(status);
		initLisDict(function(){
			$("#JzType").val(JzType);
			$("#Dept").val(Dept);
			$("#SendSection").val(SendSection);
			$("#Creator").val(Creator);
		});
	};
	//加载数据
	function onLoad(whereObj){
		var cols = config.cols[0],
			fields = [];
			
		for(var i in cols){
			fields.push('SCMsg_' + cols[i].field);
		}
			
		tableInd.reload({
			url:GET_MSG_LIST_URL,
			where:$.extend({},whereObj,{
				fields:fields.join(',')
			})
		});
	};
	//处理页面
	function toDo(obj){
		var data = obj.data;
			
		var urlInfo = getUrlInfo(data.MsgTypeCode);
		var url = urlInfo.url;
		if(url){
			layer.open({
				title:'消息详情',
				type:2,
				content:url + '?isShow=true&id=' + data.Id + '&t=' + new Date().getTime(),
				maxmin:true,
				toolbar:true,
				resize:true,
				area:['95%','95%']
			});
		}else{
			layer.msg(urlInfo.msg.join('</br>'));
		}
	};
	
	//推送第三方
	function toThirdParty(data){
		var index = layer.open({
			title:'危急值推送第三方',
			type:2,
			content:'toThirdParty.html?id=' + data.Id + '&t=' + new Date().getTime(),
			area:['600px','260px']
		});
	};
	//获取处理界面参数
	function getHandleParams(callback){
		var where = "bparameter.ParaNo='" + AFTER_HANDLE_CODE + "'";
		getParamList(where,function(list){
			for(var i in list){
				if(list[i].ParaNo == AFTER_HANDLE_CODE){//危急值处理后是否调用第三方服务
					AFTER_HANDLE = list[i].ParaValue == '1' ? true : false;
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
	
	//获取地址
	function getUrlInfo(MsgTypeCode){
		var url = uxutil.path.LOCAL,
			msg = [],
			systemUrl = ALL_SYSTEM_LIST[0].SystemHost,
			msgTypeUrl = MSG_TYPE_LIST[0].Url,
			systemId = null;
		
		if(!msgTypeUrl){
			url = '';
			msg.push('危急值消息类型没有配置展现程序地址！');
		}
		if(!systemUrl){
			url = '';
			msg.push('该系统没有配置地址！');
		}
		if(msg.length == 0){
			url += '/' + systemUrl + '/' + msgTypeUrl;
		}
		
		return {
			url:url,
			msg:msg
		};
	};
	//获取系统列表
	function loadSystemList(callback){
		var url = GET_SYSTEM_LIST_URL,
			where = [];
		
		url += "?fields=IntergrateSystemSet_Id,IntergrateSystemSet_SystemName,IntergrateSystemSet_SystemCode," +
			"IntergrateSystemSet_SystemHost&where=intergratesystemset.SystemCode='" + SYSTEM_CODE + "'";
			
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data.success){
				ALL_SYSTEM_LIST = data.value.list || [];
				callback();
			}else{
				layer.msg(data.msg);
			}
		});
	};
	//获取消息类型
	function loadMsgTypeList(callback){
		var url = GET_MSG_TYPE_LIST_URL,
			where = ["scmsgtype.Code='" + MSG_TYPE_CODE + "'"];
			
		url += "?fields=SCMsgType_Id,SCMsgType_CName,SCMsgType_Code,SCMsgType_Url,SCMsgType_SystemID" +
			"&where=scmsgtype.Code='" + MSG_TYPE_CODE + "'";
			
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data.success){
				MSG_TYPE_LIST = data.value.list || [];
				callback();
			}else{
				layer.msg(data.msg);
			}
		});
	};
	//获取危急值员工部门列表
	function loadDeptIds(callback){
		var url = GET_DEPTIDS_URL,
			empId = uxutil.cookie.get(uxutil.cookie.map.USERID),
			DEPTID = uxutil.cookie.get(uxutil.cookie.map.DEPTID);
		
		DEPT_IDS = [DEPTID];
			
		url += '?fields=CVCriticalValueEmpIdDeptLink_DeptID,CVCriticalValueEmpIdDeptLink_DeptName' +
			'&where=cvcriticalvalueempiddeptlink.IsUse=1 and cvcriticalvalueempiddeptlink.EmpID=' + empId;
			
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data.success){
				var list = data.value.list || [];
				DEPT_LIST = [];
				for(var i in list){
					if(list[i].DeptID != DEPTID){
						DEPT_IDS.push(list[i].DeptID);
					}
					DEPT_LIST.push({
						Id:list[i].DeptID,
						CName:list[i].DeptName
					});
				}
				callback();
			}else{
				layer.msg(data.msg);
			}
		});
	};
	//获取员工角色列表
	function getEmpRoleList(callback){
		var me = this,
			url = GET_EMP_ROLE_LIST_URL + "?isPlanish=true&fields=RBACEmpRoles_RBACRole_Id";
		
		url += "&where=rbacemproles.IsUse=1 and rbacemproles.HREmployee.Id=" + EMPID;
		
		uxutil.server.ajax({
			url:url,
		},function(data){
			if(data.success){
				var list = data.value.list || [];
				
				for(var i in list){
					switch(list[i].RBACEmpRoles_RBACRole_Id){
						case '1001':IS_TECH = true;break;
						case '2001':IS_NURSE = true;break;
						case '3001':IS_DOCTOR = true;break;
						default:break;
					}
				}
				callback();
			}else{
				layer.msg(data.msg);
			}
		});
	};
	
	//获取Lis字典表信息
	//Doctor开单医生;Department开单科室;PUser用户表（主要包含检验科人员）;PGroup检验小组表;SickType就诊类型;
	function GetLisDictInfo(dictName,where,callback){
		var url = uxutil.path.ROOT + "/ServerWCF/MsgManageService.svc/Msg_GetLisDictInfo";
		url += "?dictName=" + dictName +"&strWhere=" + (where || "");
		
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data.success){
				callback((data.value || {}).list || []);
			}
		});
	};
	//初始化Lis字典表信息
	function initLisDict(callback){
		//就诊类型JzType-RecSickTypeID
		if(JZTYPE_LIST){
			initSelect("JzType","就诊类型",JZTYPE_LIST,callback);
		}else{
			GetLisDictInfo("SickType","",function(list){
				JZTYPE_LIST = list;
				LIS_DICT_NUM++;
				initSelect("JzType","就诊类型",JZTYPE_LIST,callback);
			});
		}
		//开单科室Dept
		if(DEPT_LIST){
			LIS_DICT_NUM++;
			initSelect("Dept","开单科室",DEPT_LIST,callback);
//				}else{
//					GetLisDictInfo("Department","",function(list){
//						DEPT_LIST = list;
//						LIS_DICT_NUM++;
//						initSelect("Dept","开单科室",DEPT_LIST,callback);
//					});
		}
		//小组SendSection
		if(SENDSECTION_LIST){
			initSelect("SendSection","发送小组",SENDSECTION_LIST,callback);
		}else{
			GetLisDictInfo("PGroup","",function(list){
				SENDSECTION_LIST = list;
				LIS_DICT_NUM++;
				initSelect("SendSection","发送小组",SENDSECTION_LIST,callback);
			});
		}
		//产生用户Creator
		if(CREATOR_LIST){
			initSelect("Creator","发送者",CREATOR_LIST,callback);
		}else{
			GetLisDictInfo("PUser","",function(list){
				CREATOR_LIST = list;
				LIS_DICT_NUM++;
				initSelect("Creator","发送者",CREATOR_LIST,callback);
			});
		}
	};
	//初始化select组件
	function initSelect(domId,defaultName,list,callback){
		var len = list.length,
			htmls = ['<option value="">' + defaultName + '</option>'];
			
		for(var i=0;i<len;i++){
			htmls.push('<option value="' + list[i].Id + '">' + list[i].CName + '</option>');
		}
		$("#" + domId).html(htmls.join(""));
		
		//LIS字典全部加载完毕后渲染所有下拉框组件
		if(LIS_DICT_NUM >= LIS_DICT_COUNT){
			form.render('select');
			if(typeof callback == 'function'){callback();}
		}
	};
	
	//初始化页面
	function initHtml(callback){
		//获取处理界面参数
		getHandleParams(function(){
			if(AFTER_HANDLE){
				config.cols[0].push({width:100,title:'操作',align:'center',fixed:'right',toolbar:'#table-operate-bar'});
			}
			
			tableInd = uxtable.render(config);
			//头工具栏事件
			tableInd.table.on('toolbar(table)', function(obj){
				switch(obj.event){
					case 'search':onSearch();break;
				}
			});
			//监听行双击事件
			tableInd.table.on('rowDouble(table)', function(obj){
				toDo(obj);
			});
			//操作栏事件
			tableInd.table.on('tool(table)', function(obj){
				switch(obj.event){
					case 'toThirdParty':toThirdParty(obj.data);break;
				}
			});
			callback();
		});
	};
	//初始化
	function init(){
		//初始化页面
		initHtml(function(){
			//获取系统列表
			loadSystemList(function(){
				//获取消息类型
				loadMsgTypeList(function(){
					//获取危急值员工部门列表
					loadDeptIds(function(){
						//获取员工角色列表
						getEmpRoleList(function(){
							//初始化Lis字典表信息
							initLisDict();
							//默认加载数据
							onSearch();
						});
					});
				});
			});
		});
	};
	
	//初始化
	init();
});