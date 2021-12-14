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
	//var GET_MSG_LIST_URL = uxutil.path.LIIP_ROOT + '/ServerWCF/IMService.svc/ST_UDTO_SearchSCMsgByHQL';
	var GET_MSG_LIST_URL = uxutil.path.LIIP_ROOT + '/ServerWCF/IMService.svc/ST_UDTO_SearchSCMsgAndSCMsgHandleByHQL?isPlanish=true';
	//获取危急值员工部门列表
	var GET_DEPTIDS_URL = uxutil.path.LIIP_ROOT + "/ServerWCF/IMService.svc/ST_UDTO_SearchCVCriticalValueEmpIdDeptLinkByHQL";
	//获取系统参数列表服务地址
	var GET_PARAMS_LIST_URL = uxutil.path.LIIP_ROOT + '/ServerWCF/SingleTableService.svc/ST_UDTO_SearchBParameterByHQL';
	//获取机构列表
	var GET_DEPT_LIST_URL = uxutil.path.LIIP_ROOT + '/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHRDeptByHQL';
	//获取不同类型机构列表
	var GET_DEPT_LINK_IST_URL = uxutil.path.LIIP_ROOT + '/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHRDeptIdentityByHQL?isPlanish=true';
	
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
	//病区编码
	var REC_DISTRICT_CODE = '1001102';
	//送检科室编码
	var REC_DEPT_CODE= '1001101';
	//需要加载的LIS字典总数
	var LIS_DICT_COUNT = 3;
	//已加载的LIS字典数量
	var LIS_DICT_NUM = 0;
	
	//就诊类型列表
	var JZTYPE_LIST = null;
	//开单科室列表
	var DEPT_LIST = null;
	//病区列表
	var DISTRICT_LIST = null;
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
	//各个状态
	var STATUS_MAP = {
		'超时未确认':{color:'#FF5722'},
		'待确认':{color:'#1E9FFF'},
		'超时未处理':{color:'red'},
		'待处理':{color:'#1E9FFF'},
		'已处理':{color:'#e0e0e0'},
	};
	
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
			{field:'RecDeptName',width:100,title:'开单科室',sort:true,fixed:'left'},
			{field:'DoctorName',width:100,title:'开单医生',fixed:'left'},
			{field:'HandleTypeName',width:100,title:'紧急情况',sort:true,templet:function(d){
				var result = '';
				if(d.HandleTypeName == '紧急'){
					result = '<span style="color:red;">紧急</span>';
				}else if(d.HandleTypeName == '非紧急'){
					result = '<span style="color:green;">非紧急</span>';
				}
				return result
			}},
			{field:'RecSickTypeName',width:90,title:'就诊类型'},
			{field:'PatientName',width:90,title:'病人姓名'},
			{field:'PatientSex',width:60,title:'性别'},
			{field:'PatientAge',width:60,title:'年龄'},
			{field:'PatNo',width:100,title:'病历号'},
			{field:'SampleNo',width:100,title:'样本号'},
			{field:'msgAll',minWidth:200,title:'危急值消息内容',templet:function(d){
				return d.msgAllShow;
			}},
			{field:'msgAllShow',minWidth:200,title:'危急值消息显示内容',hide:true},
			{field:'satusName',width:100,title:'状态',templet:function(d){
				return '<span style="color:' + STATUS_MAP[d.SatusName] + ';">超时未确认</span>';
			}},
			{field:'SenderName',width:100,title:'发送者'},
			{field:'DataAddTime',width:160,title:'产生时间',sort:true},
			{field:'ConfirmerName',width:100,title:'确认人',sort:true},
			{field:'RequireConfirmTime',width:160,title:'要求确认时间',templet:function(d){
				var result = d.RequireConfirmTime || '';
				if(!d.ConfirmDateTime){//未确认的需要判断颜色
					var timesStr = uxutil.date.difference(d.RequireConfirmTime,new Date());
					if(timesStr && timesStr.slice(0,1) != '-'){
						result = '<span style="color:#FF5722;" title="超时' + timesStr + '">' + d.RequireConfirmTime + '</span>';
					}
				}
				return result;
			}},
			{field:'ConfirmDateTime',width:160,title:'实际确认时间',sort:true,templet:function(d){
				var result = d.ConfirmDateTime || '';
				var timesStr = uxutil.date.difference(d.RequireConfirmTime,d.ConfirmDateTime);
				if(timesStr && timesStr.slice(0,1) != '-'){
					result = '<span style="color:#FF5722;" title="超时' + timesStr + '">' + d.ConfirmDateTime + '</span>';
				}
				return result;
			}},
			{field:'ConfirmNotifyDoctorName',width:100,title:'被通知人',sort:true},
			{field:'NotifyDoctorByEmpName',width:100,title:'通知人',sort:true},
			{field:'NotifyDoctorCount',width:100,title:'通知次数',sort:true},
			{field:'NotifyDoctorLastDateTime',width:160,title:'最后通知时间',sort:true},
			{field:'SCMsgHandle_HandlerName',width:100,title:'处理人',sort:true},
			{field:'LastRequireHandleTime',width:160,title:'通知要求处理时间',sort:true},
			{field:'RequireHandleTime',width:160,title:'要求处理时间',sort:true},
			{field:'HandlingDateTime',width:160,title:'实际处理时间',sort:true},
			{field:'ConfirmUseTime',width:120,title:'产生到确认',sort:true},
			{field:'HandleUseTime',width:120,title:'确认到处理',sort:true},
			{field:'ConfirmHandleUseTime',width:120,title:'产生到处理',sort:true},
			{field:'WarningUpLoadDateTime',width:160,title:'上报时间',sort:true},
			{field:'SendSectionName',width:90,title:'小组'},
			{field:'MsgTypeCode',width:180,title:'类型代码',hide:true},
			{field:'MsgContent',width:100,title:'消息内容',hide:true},
			{field:'ConfirmFlag',width:90,title:'确认标志',hide:true},
			{field:'HandleFlag',width:90,title:'处理标志',hide:true},
			{field:'isConfirmOutTime',width:90,title:'是否确认超时',hide:true},
			{field:'isHandleOutTime',width:90,title:'是否处理超时',hide:true},
			{field:'Id',title:'ID',width:180,hide:true}
		]],
		changeData:function(data){
			return changeData(data);
		},
		exportsWhereFun:function(callback){
			var DateTimeRangeType = $('#DateTimeRangeType option:selected').val(),
				dates = $("#dates").val(),
				status = $('#status option:selected').val(),
				JzType = $('#JzType option:selected').val(),
				District = $('#District option:selected').val(),
				Dept = $('#Dept option:selected').val(),
				SendSection = $('#SendSection option:selected').val(),
				SendSectionName = $('#SendSection option:selected').text(),
				Creator = $('#Creator option:selected').val(),
				CreatorName = $('#Creator option:selected').text();
				
			//消息过滤条件
			var where = [];
			
			//条件:使用中+危急值类型
			where.push("scmsg.IsUse=1 and scmsg.MsgTypeCode='" + MSG_TYPE_CODE + "'");
			
			if(dates){
				var splitField = $("#dates").attr("placeholder");
				var dateArr = dates.split(splitField);
				where.push("scmsg.DataAddTime >='" + dateArr[0] +"' and scmsg.DataAddTime <'" + 
					uxutil.date.toString(uxutil.date.getNextDate(dateArr[1]),true) + "'");
			}
			if(status){where.push(STATUS_LIST[status]);}
			if(JzType){where.push("scmsg.RecSickTypeID=" + JzType);}
			if(District){where.push("scmsg.RecDistrictName='" + District + "'");}
			if(Dept){where.push("scmsg.RecDeptID=" + Dept);}
			if(SendSection){where.push("scmsg.SendSectionName='" + SendSectionName + "'");}
			if(Creator){where.push("scmsg.SenderName='" + CreatorName + "'");}
			
			var cols = config.cols[0],
				fields = [];
				
			for(var i in cols){
				fields.push('SCMsg_' + cols[i].field);
			}
			
			var index = layer.load();
			uxutil.server.ajax({
				url:GET_MSG_LIST_URL,
				data:$.extend({},{"where":where.join(' and ')},{
					fields:fields.join(',')
				})
			},function(data){
				layer.close(index);
				if(data.success){
					var list = (data.value || {}).list || [];
					for(var i in list){
						list[i] = changeData(list[i]);
					}
					callback(list);
				}else{
					layer.msg(data.msg);
				}
			});
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
		//缓存常用开单科室
		onCacheRecDeptList();
		onSearch();
	});
	//病区
	form.on('select(District)', function(data){
		//缓存常用病区
		onCacheRecDistrictList();
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
	
	//缓存常用开单科室
	function onCacheRecDeptList(){
		var RecDeptList = uxutil.localStorage.get('RecDeptList',true) || [],
			selectDept = $('#Dept option:selected'),
			deptInfo = {
				Id:selectDept.val(),
				CName:selectDept.text()
			},
			MAX = 3;
			
		for(var i in RecDeptList){
			if(RecDeptList[i].Id == deptInfo.Id){
				RecDeptList.splice(i,1);
			}
		}
		//最大数量已满，删除最前面的账号信息
		if(RecDeptList.length >= MAX){
			RecDeptList.splice(0,1);
		}
		
		RecDeptList.unshift(deptInfo)
		uxutil.localStorage.set('RecDeptList',JSON.stringify(RecDeptList));
	};
	//缓存常用病区
	function onCacheRecDistrictList(){
		var RecDistrictList = uxutil.localStorage.get('RecDistrictList',true) || [],
			selectDept = $('#District option:selected'),
			deptInfo = {
				CName:selectDept.val()
			},
			MAX = 3;
			
		for(var i in RecDistrictList){
			if(RecDistrictList[i].CName == deptInfo.CName){
				RecDistrictList.splice(i,1);
			}
		}
		//最大数量已满，删除最前面的账号信息
		if(RecDistrictList.length >= MAX){
			RecDistrictList.splice(0,1);
		}
		
		RecDistrictList.unshift(deptInfo)
		uxutil.localStorage.set('RecDistrictList',JSON.stringify(RecDistrictList));
	};
	//数据处理
	function changeData(data){
		//压平数据去掉对象前缀
		for(var i in data){
			data[i.slice(6)] = data[i];
		}
		//产生到确认
		data.ConfirmUseTime = uxutil.date.getDateContentByTimes(parseInt(data.ConfirmUseTime)*1000) || '';
		//确认到处理
		data.HandleUseTime = uxutil.date.getDateContentByTimes(parseInt(data.HandleUseTime)*1000) || '';
		//产生到处理
		data.ConfirmHandleUseTime = uxutil.date.getDateContentByTimes(parseInt(data.ConfirmHandleUseTime)*1000) || '';
		
		//确认超时
		data.isConfirmOutTime = false;
		if(data.ConfirmFlag == '0' && data.RequireConfirmTime){//未确认&&要求确认时间
			var timesStr = uxutil.date.difference(data.RequireConfirmTime,new Date());
			if(timesStr && timesStr.slice(0,1) != '-'){
				data.isConfirmOutTime = true;
			}
		}
		//处理超时
		data.isHandleOutTime = false;
		if(data.HandleFlag == '0' && data.LastRequireHandleTime){//未处理&&要求处理时间
			var timesStr = uxutil.date.difference(data.LastRequireHandleTime,new Date());
			if(timesStr && timesStr.slice(0,1) != '-'){
				data.isHandleOutTime = true;
			}
		}
		
		//状态显示
		data.satusName = '';
		if(data.ConfirmFlag == '0'){//未确认过的
			var isConfirmTimeout = false;
			var nowStr = uxutil.date.toString(new Date()).replace(/-/g,'/');
			if(data.RequireConfirmTime && nowStr > data.RequireConfirmTime){
				isConfirmTimeout = true;
			}
			if(isConfirmTimeout){
				data.satusName = '超时未确认';
			}else{
				data.satusName = '待确认';
			}
		}else if(data.ConfirmFlag == '1'){
			if(data.HandleFlag == '0'){
				var isHandleTimeout = false;
				var nowStr = uxutil.date.toString(new Date()).replace(/-/g,'/');
				if(data.LastRequireHandleTime && nowStr > data.LastRequireHandleTime){
					isHandleTimeout = true;
				}
				if(isHandleTimeout){
					data.satusName = '超时未处理';
				}else{
					data.satusName = '待处理';
				}
			}else{
				data.satusName = '已处理';
			}
		}
		
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
		
		var msgAll = [];
		var msgAllShow = [];
		for(var i in MsgList){
			var status = MsgList[i].RESULTSTATUS;
			if(status.indexOf("H") != -1){
				status = '<span style="color:red;">' + status + '</span>';
			}if(status.indexOf("L") != -1){
				status = '<span style="color:blue;">' + status + '</span>';
			}
			msgAll.push(MsgList[i].TESTITEMNAME + '  ' + MsgList[i].REPORTVALUEALL + '  ' + MsgList[i].RESULTSTATUS);
			msgAllShow.push(MsgList[i].TESTITEMNAME + ' ' + MsgList[i].REPORTVALUEALL + ' ' + status);
		}
		data.msgAll = msgAll.join('  ');
		data.msgAllShow = msgAllShow.join('</br>');
		
		return data;
	};
	//查询
	function onSearch(){
		var DateTimeRangeType = $('#DateTimeRangeType option:selected').val(),
			dates = $("#dates").val(),
			status = $('#status option:selected').val(),
			JzType = $('#JzType option:selected').val(),
			District = $('#District option:selected').val(),
			Dept = $('#Dept option:selected').val(),
			SendSection = $('#SendSection option:selected').val(),
			SendSectionName = $('#SendSection option:selected').text(),
			Creator = $('#Creator option:selected').val(),
			CreatorName = $('#Creator option:selected').text();
			
		//消息过滤条件
		var where = [];
		
		//条件:使用中+危急值类型
		where.push("scmsg.IsUse=1 and scmsg.MsgTypeCode='" + MSG_TYPE_CODE + "'");
		
		if(dates){
			var splitField = $("#dates").attr("placeholder");
			var dateArr = dates.split(splitField);
			where.push("scmsg.DataAddTime >='" + dateArr[0] +"' and scmsg.DataAddTime <'" + 
				uxutil.date.toString(uxutil.date.getNextDate(dateArr[1]),true) + "'");
		}
		if(status){where.push(STATUS_LIST[status]);}
		if(JzType){where.push("scmsg.RecSickTypeID=" + JzType);}
		if(District){where.push("scmsg.RecDistrictName='" + District + "'");}
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
			$("#SendSection").val(SendSection);
			$("#Creator").val(Creator);
		});
		initDeptSelect(function(){
			$("#Dept").val(Dept);
		});
		initDistrictSelect(function(){
			$("#District").val(District);
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
			initSort:config.initSort,
			url:GET_MSG_LIST_URL,
			where:$.extend({},whereObj,{
				sort:JSON.stringify([{property:'SCMsg_'+config.initSort.field,direction:config.initSort.type}]),
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
				content:url + '?id=' + data.Id + '&t=' + new Date().getTime(),
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
		var url = GET_MSG_TYPE_LIST_URL;
			
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
	//获取开单科室列表
	function getDeptList(callback){
		var url = GET_DEPT_LINK_IST_URL;
			
		url += "&fields=HRDeptIdentity_HRDept_CName,HRDeptIdentity_HRDept_Id";
		url += "&where=hrdeptidentity.TSysCode='" + REC_DEPT_CODE + "' and hrdeptidentity.SystemCode='" + SYSTEM_CODE + "'";
			
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data.success){
				var list = data.value.list || [];
				DEPT_LIST = [];
				for(var i in list){
					DEPT_LIST.push({
						Id:list[i].HRDeptIdentity_HRDept_Id,
						CName:list[i].HRDeptIdentity_HRDept_CName
					});
				}
				callback();
			}else{
				layer.msg(data.msg);
			}
		});
	};
	//获取病区列表
	function getDistrictList(callback){
		var url = GET_DEPT_LINK_IST_URL;
			
		url += "&fields=HRDeptIdentity_HRDept_CName,HRDeptIdentity_HRDept_Id";
		url += "&where=hrdeptidentity.TSysCode='" + REC_DISTRICT_CODE + "' and hrdeptidentity.SystemCode='" + SYSTEM_CODE + "'";
			
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data.success){
				var list = data.value.list || [];
				DISTRICT_LIST = [];
				for(var i in list){
					DISTRICT_LIST.push({
						Id:list[i].HRDeptIdentity_HRDept_Id,
						CName:list[i].HRDeptIdentity_HRDept_CName
					});
				}
				callback();
			}else{
				layer.msg(data.msg);
			}
		});
	};
	
	//初始化病区下拉框
	function initDistrictSelect(callback){
		var list = DISTRICT_LIST,
			RecDistrictList = uxutil.localStorage.get('RecDistrictList',true) || [],
			htmls = ['<option value="">病区</option>'];
			
		//缓存常用病区
		if(RecDistrictList.length > 0){
			htmls.push('<optgroup label="最近使用">');
			for(var i in RecDistrictList){
				htmls.push('<option value="' + RecDistrictList[i].CName + '">' + RecDistrictList[i].CName + '</option>');
			}
			htmls.push('</optgroup>');
		}
		
		if(list.length > 0){
			htmls.push('<optgroup label="所有病区">');
			for(var i in list){
				htmls.push('<option value="' + list[i].CName + '">' + list[i].CName + '</option>');
			}
			htmls.push('</optgroup>');
		}
		
		$("#District").html(htmls.join(""));
		
		form.render('select');
		if(typeof callback == 'function'){callback();}
	};
	//初始化开单科室下拉框
	function initDeptSelect(callback){
		var list = DEPT_LIST,
			RecDeptList = uxutil.localStorage.get('RecDeptList',true) || [],
			htmls = ['<option value="">开单科室</option>'];
			
		//缓存常用开单科室
		if(RecDeptList.length > 0){
			htmls.push('<optgroup label="最近使用">');
			for(var i in RecDeptList){
				htmls.push('<option value="' + RecDeptList[i].Id + '">' + RecDeptList[i].CName + '</option>');
			}
			htmls.push('</optgroup>');
		}
		
		if(list.length > 0){
			htmls.push('<optgroup label="所有科室">');
			for(var i in list){
				htmls.push('<option value="' + list[i].Id + '">' + list[i].CName + '</option>');
			}
			htmls.push('</optgroup>');
		}
		
		$("#Dept").html(htmls.join(""));
		
		form.render('select');
		if(typeof callback == 'function'){callback();}
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
//			if(AFTER_HANDLE){
//				config.cols[0].push({width:100,title:'操作',align:'center',fixed:'right',toolbar:'#table-operate-bar'});
//			}
			
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
			//监听排序切换
			tableInd.table.on('sort(table)', function(obj){
				config.initSort = {
					field:obj.field,
					type:obj.type
				};
				onSearch();
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
					//获取开单科室列表
					getDeptList(function(){
						//获取病区列表
						getDistrictList(function(){
							//初始化病区下拉框
							initDistrictSelect();
							//初始化开单科室下拉框
							initDeptSelect();
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