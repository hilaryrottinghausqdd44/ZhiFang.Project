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
	//外部参数
	var PARAMS = uxutil.params.get(true);
	//消息类型编码串
	var TYPECODES = PARAMS.TYPECODES || '';
	//状态
	var STATUS = PARAMS.STATUS;
	//系统列表
	ALL_SYSTEM_LIST = [];
	//当前系统列表
	SYSTEM_LIST = [];
	//消息类型列表
	MSG_TYPE_LIST = [];
	//当前系统消息类型列表
	SYSTEM_MSG_TYPE_LIST = [];
	
	//危急值员工部门IDS
	var DEPT_IDS = [];
	
	//状态列表-0无，1超时，2已处理，3已确认，4待处理
	var STATUS_LIST = [
		"",
		"scmsg.ConfirmFlag='0' and scmsg.RequireConfirmTime<CONVERT(varchar(100),GETDATE(),20)",
		"scmsg.HandleFlag='1'",
		"scmsg.HandleFlag='0' and scmsg.ConfirmFlag='1'",
		"scmsg.HandleFlag='0' and scmsg.ConfirmFlag='0'"
	];
	
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
			{field:'IsUse',width:60,title:'使用',align:'center',templet:function(d){
				var result = '';
				if(d.IsUse == '1'){
					result = '<span style="color:#009688;">是</span>';
				}else{
					result = '<span style="color:#FF5722;">否</span>';
				}
				return result;
			}},
			{field:'SendSectionName',width:90,title:'小组'},
			{field:'SenderName',width:90,title:'发送者'},
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
				if(d.HandleFlag == '0' && d.isTimeout){
					result = '<span style="color:#FF5722;">超时未确认</span>';
				}else if(d.HandleFlag == '1'){
					result = '<span style="color:#009688;">已处理</span>';
				}else if(d.ConfirmFlag == '1'){
					result = '<span style="color:#1E9FFF;">已确认</span>';
				}else{
					result = '<span style="color:#393D49;">待处理</span>';
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
			{field:'HandlingDateTime',width:160,title:'处理时间',sort:true},
			{field:'Id',title:'ID',width:180,hide:true},
			{field:'MsgTypeCode',width:180,title:'类型代码',hide:true},
			{field:'MsgContent',width:100,title:'消息内容',hide:true},
			{field:'HandleFlag',width:90,title:'处理标志',hide:true}
		]]
	};
	var tableInd = uxtable.render(config);
	//头工具栏事件
	tableInd.table.on('toolbar(table)', function(obj){
		switch(obj.event){
			case 'search':onSearch();break;
			case 'clear':onClear();break;
		}
	});
	//监听行双击事件
	tableInd.table.on('rowDouble(table)', function(obj){
		toDo(obj);
	});
	
	//状态下拉监听
	form.on('select(status)', function(data){
		onSearch();
	});
	//系统下拉监听
	form.on('select(system)', function(data){
		$('#msgType').val("");
		onSearch(data.value);
	});
	//类型下拉监听
	form.on('select(msgType)', function(data){
		onSearch();
	});
	//使用下拉监听
	form.on('select(isUse)', function(data){
		onSearch();
	});
	
	form.on('select(DateTimeRangeType)', function(data){
		var value = data.value;
		if(value != ""){
			var today = uxutil.date.toString(new Date(),true);
			var start = uxutil.date.toString(uxutil.date.getNextDate(today,1-parseInt(value)),true);
			
			LAYDATE_DATES.config.laydate.setValue(start + ' - ' + today);
			onSearch();
		}else{
			LAYDATE_DATES.config.laydate.setValue();
			onSearch();
		}
	});
	
	//日期时间范围
	var LAYDATE_DATES = laydate.render({
		elem:'#dates',
		range:true,
		done: function(value,date,endDate){
			$('#DateTimeRangeType').val("");
			setTimeout(function(){
				onSearch();
			},100);
		}
	});
	//清空查询条件
	function onClear(){
		$("#DateTimeRangeType").val('');
		$('#dates').val('');
		$('#system').val('');
		$('#msgType').val('');
		$("#status").val('');
		$("#isUse").val('');
		form.render('select');
		onSearch();
	};
	//查询
	function onSearch(systemId){
		var DateTimeRangeType = $('#DateTimeRangeType option:selected').val(),
			dates = $("#dates").val(),
			system = $('#system option:selected').val(),
			msgType = $('#msgType option:selected').val(),
			isUse = $('#isUse option:selected').val(),
			status = $('#status option:selected').val(),
			where = [];
			
		if(dates){
			var splitField = $("#dates").attr("placeholder");
			var dateArr = dates.split(splitField);
			where.push("scmsg.DataAddTime >='" + dateArr[0] +"' and scmsg.DataAddTime <'" + 
				uxutil.date.toString(uxutil.date.getNextDate(dateArr[1]),true) + "'");
		}
		
		if(system){
			where.push("scmsg.SystemID=" + system);
		}
		if(msgType){
			where.push("scmsg.MsgTypeID=" + msgType);
		}
		if(status){
			where.push(STATUS_LIST[status]);
		}
		if(isUse){
			where.push("scmsg.IsUse=" + isUse);
		}
			
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
		
		changeSelectComponent(systemId);
		$('#system').val(system);
		$('#msgType').val(msgType);
		$("#status").val(status);
		$("#isUse").val(isUse);
	};
	//加载数据
	function onLoad(whereObj){
		var cols = config.cols[0],
			fields = [];
			
		for(var i in cols){
			fields.push('SCMsg_' + cols[i].field);
		}
			
		tableInd.instance.reload({
			url:GET_MSG_LIST_URL,
			where:$.extend({},whereObj,{
				fields:fields.join(',')
			})
		});
	};
	//处理页面
	function toDo(obj){
		var data = obj.data;
			
		var urlInfo = getUrlInfo(data.MsgTypeID);
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
	
	//获取地址
	function getUrlInfo(msgTypeId){
		var url = uxutil.path.LOCAL,
			msg = [],
			systemUrl = '',
			msgTypeUrl = '',
			systemId = null;
		
		for(var i in MSG_TYPE_LIST){
			if(MSG_TYPE_LIST[i].Id == msgTypeId){
				systemId = MSG_TYPE_LIST[i].SystemID;
				msgTypeUrl = MSG_TYPE_LIST[i].Url;
				break;
			}
		}
		
		if(msgTypeUrl){
			var regex = /\{(.+?)\}/g;
			var sysArr = msgTypeUrl.match(regex);
			
			if(!sysArr || sysArr.length == 0){
				//以系统ID为准
				for(var i in ALL_SYSTEM_LIST){
					if(ALL_SYSTEM_LIST[i].Id == systemId){
						systemUrl = ALL_SYSTEM_LIST[i].SystemHost ;
						break;
					}
				}
			}else if(sysArr.length == 1){
				//以系统码为准
				msgTypeUrl = msgTypeUrl.split(sysArr[0] + '/')[1];
				for(var i in ALL_SYSTEM_LIST){
					if('{' + ALL_SYSTEM_LIST[i].SystemCode + '}' == sysArr[0]){
						systemUrl = ALL_SYSTEM_LIST[i].SystemHost ;
						break;
					}
				}
			}
		}else{
			url = '';
			msg.push('该消息类型没有配置展现程序地址！');
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
		
		url += '?fields=IntergrateSystemSet_Id,IntergrateSystemSet_SystemName,IntergrateSystemSet_SystemCode,IntergrateSystemSet_SystemHost';
		
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
			where = [];
			
		if(TYPECODES){
			where.push("scmsgtype.Code in ('" + TYPECODES.replace(/,/g,"','") + "')");
		}
			
		url += '?fields=SCMsgType_Id,SCMsgType_CName,SCMsgType_Code,SCMsgType_Url,SCMsgType_SystemID&where=' + where.join(' and ');
			
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
			
		url += '?fields=CVCriticalValueEmpIdDeptLink_DeptID' +
			'&where=cvcriticalvalueempiddeptlink.IsUse=1 and cvcriticalvalueempiddeptlink.EmpID=' + empId;
			
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data.success){
				var list = data.value.list || []
				for(var i in list){
					if(list[i].DeptID != DEPTID){
						DEPT_IDS.push(list[i].DeptID);
					}
				}
				callback();
			}else{
				layer.msg(data.msg);
			}
		});
	};
	//初始化
	function init(){
		//获取系统列表
		loadSystemList(function(){
			//获取消息类型
			loadMsgTypeList(function(){
				//获取危急值员工部门列表
				loadDeptIds(function(){
					//初始化当前系统列表
					initSystemList();
					//下拉组件更改
					changeSelectComponent();
					//默认加载数据
					onSearch();
				});
			});
		});
	};
	//初始化当前系统列表
	function initSystemList(){
		var codes = TYPECODES ? TYPECODES.split(',') : [],
			len = codes.length,
			sysMap = {};
			
		if(len == 0){
			SYSTEM_LIST = ALL_SYSTEM_LIST;
		}else{
			SYSTEM_LIST = [];
			for(var i=0;i<len;i++){
				for(var j in MSG_TYPE_LIST){
					if(codes[i] == MSG_TYPE_LIST[j].Code){
						sysMap[MSG_TYPE_LIST[j].SystemID] = 1;
						break;
					}
				}
			}
			//当前消息类型所属系统
			for(var i in sysMap){
				for(var j in ALL_SYSTEM_LIST){
					if(i == ALL_SYSTEM_LIST[j].Id){
						SYSTEM_LIST.push(ALL_SYSTEM_LIST[j]);
					}
				}
			}
		}
	};
	
	//下拉组件更改
	function changeSelectComponent(systemId){
		if(systemId){
			SYSTEM_MSG_TYPE_LIST = [];
			for(var i in MSG_TYPE_LIST){
				if(MSG_TYPE_LIST[i].SystemID == systemId){
					SYSTEM_MSG_TYPE_LIST.push(MSG_TYPE_LIST[i]);
				}
			}
		}else{
			SYSTEM_MSG_TYPE_LIST = MSG_TYPE_LIST;
		}
		
		changeSystemSelect(SYSTEM_LIST);
		changeMsgTypeSelect(SYSTEM_MSG_TYPE_LIST);
		form.render('select');
	};
	//系统组件更改
	function changeSystemSelect(list){
		var select = ['<option value="">选择系统</option>'];
			
		for(var i in list){
			select.push('<option value="' + list[i].Id + '">' + list[i].SystemName + '</option>');
		}
		$("#system").html(select.join(''));
	};
	//消息类型组件更改
	function changeMsgTypeSelect(list){
		var select = ['<option value="">消息类型</option>'];
			
		for(var i in list){
			select.push('<option value="' + list[i].Id + '">' + list[i].CName + '</option>');
		}
		$("#msgType").html(select.join(''));
	};
	
	//初始化
	init();
});