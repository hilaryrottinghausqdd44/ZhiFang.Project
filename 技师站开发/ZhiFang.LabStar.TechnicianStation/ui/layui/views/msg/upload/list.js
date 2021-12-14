layui.extend({
	uxutil:'ux/util',
	uxtable:'ux/table'
}).use(['uxutil','uxtable','form','laydate'],function(){
	var $=layui.$,
		form = layui.form,
		laydate = layui.laydate,
		uxtable = layui.uxtable,
		uxutil = layui.uxutil;
	
	//获取危急值员工部门列表
	var GET_DEPTIDS_URL = uxutil.path.LIIP_ROOT + "/ServerWCF/IMService.svc/ST_UDTO_SearchCVCriticalValueEmpIdDeptLinkByHQL";
	//获取消息列表服务地址
	var GET_MSG_LIST_URL = uxutil.path.LIIP_ROOT + '/ServerWCF/IMService.svc/ST_UDTO_SearchSCMsgByHQL';
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
	
	//状态列表-0无，1超时，2已处理，3已确认，4待处理
	var STATUS_LIST = [
		"",
		"scmsg.ConfirmFlag='0' and scmsg.RequireConfirmTime<CONVERT(varchar(100),GETDATE(),20)",
		"scmsg.HandleFlag='1'",
		"scmsg.HandleFlag='0' and scmsg.ConfirmFlag='1'",
		"scmsg.HandleFlag='0' and scmsg.ConfirmFlag='0'"
	];
	
	var EMPID = uxutil.cookie.get(uxutil.cookie.map.USERID);
	var DEPTID = uxutil.cookie.get(uxutil.cookie.map.DEPTID);
	
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
			{field:'SendSectionName',width:90,title:'小组'},
			{field:'SenderName',width:90,title:'发送者'},
			{field:'WarningUploaderFlag',width:80,title:'上报',templet:function(d){
				var result = '';
				if(d.WarningUploaderFlag == '1'){
					result = '<span style="color:#009688;">已上报</span>';
				}else{
					result = '<span style="color:#FF5722;">未上报</span>';
				}
				return result;
			}},
			{field:'BUTTON',width:70,align:'center',toolbar:'#table-operate-bar'},
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
			{field:'HandleFlag',width:90,title:'处理标志',hide:true},
			{field:'RecDeptID',width:180,title:'开单科室ID',hide:true}
		]],
		changeData:function(data){
			return changeData(data);
		}
	};
	var tableInd = uxtable.render(config);
	//头工具栏事件
	tableInd.table.on('toolbar(table)', function(obj){
		switch(obj.event){
			case 'search':onSearch();break;
		}
	});
	//监听工具条
	tableInd.table.on('tool(table)', function(obj){
		if(obj.event === 'upload'){
			openUploadWin(obj.data);
		}
	});
	//监听行双击事件
	tableInd.table.on('rowDouble(table)', function(obj){
		toDo(obj);
	});
	
	//上报状态下拉监听
	form.on('select(WarningUploaderFlag)', function(data){
		onSearch();
	});
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
		
		//是否超时
		var serverDateTimes = uxutil.server.date.getTimes(),
			isTimeout = false;
		if(!data.ConfirmDateTime){
			var RequireConfirmTime = data.RequireConfirmTime;
			var timesStr = uxutil.date.difference(RequireConfirmTime,serverDateTimes);
			if(timesStr && timesStr.slice(0,1) != '-'){
				isTimeout = true;
			}
		}
		data.isTimeout = isTimeout;
		
		return data;
	};
	//查询
	function onSearch(systemId){
		var WarningUploaderFlag = $('#WarningUploaderFlag option:selected').val(),
			DateTimeRangeType = $('#DateTimeRangeType option:selected').val(),
			dates = $("#dates").val(),
			status = $('#status option:selected').val(),
			JzType = $('#JzType option:selected').val(),
			Dept = $('#Dept option:selected').val(),
			SendSection = $('#SendSection option:selected').val(),
			Creator = $('#Creator option:selected').val(),
			where = [];
			
		//条件=使用+消息类型编码+发送者//接收科室
		where.push("scmsg.IsUse=1");
		where.push("scmsg.MsgTypeCode='" + MSG_TYPE_CODE + "'");
		//where.push("scmsg.RecDeptID in (" + DEPT_IDS.join(',') + ")");
		where.push("scmsg.SenderID=" + EMPID);
		
		if(WarningUploaderFlag){where.push("scmsg.WarningUploaderFlag='" + WarningUploaderFlag + "'");}
		if(dates){
			var splitField = $("#dates").attr("placeholder");
			var dateArr = dates.split(splitField);
			where.push("scmsg.DataAddTime >='" + dateArr[0] +"' and scmsg.DataAddTime <'" + 
				uxutil.date.toString(uxutil.date.getNextDate(dateArr[1]),true) + "'");
		}
		if(status){where.push(STATUS_LIST[status]);}
		if(JzType){where.push("scmsg.RecSickTypeID=" + JzType);}
		if(Dept){where.push("scmsg.RecDeptID=" + Dept);}
		if(SendSection){where.push("scmsg.SendSectionID=" + SendSection);}
		if(Creator){where.push("scmsg.SenderID=" + Creator);}
			
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
			
		layer.open({
			title:'危急值详情',
			type:2,
			content:'../form/index.html?isShow=true&id=' + data.Id + '&t=' + new Date().getTime(),
			maxmin:true,
			toolbar:true,
			resize:true,
			area:['95%','95%']
		});
	};
	//上报页面
	function openUploadWin(data){
		var win = $(window),
			maxWidth = win.width(),
			maxHeight = win.height(),
			width = maxWidth > 600 ? 600 : maxWidth,
			height = maxHeight > 450 ? 450 : maxHeight;
			
		layer.open({
			title:'危急值上报',
			type:2,
			content:'form.html?id=' + data.Id + '&RecDeptId=' + data.RecDeptID + '&t=' + new Date().getTime(),
			maxmin:true,
			toolbar:true,
			resize:true,
			area:[width+'px',height+'px']
		});
	}
	
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
	}
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
			initSelect("Dept","开单科室",DEPT_LIST,callback);
		}else{
			GetLisDictInfo("Department","",function(list){
				DEPT_LIST = list;
				LIS_DICT_NUM++;
				initSelect("Dept","开单科室",DEPT_LIST,callback);
			});
		}
		//小组SendSection
		if(SENDSECTION_LIST){
			initSelect("SendSection","小组",SENDSECTION_LIST,callback);
		}else{
			GetLisDictInfo("PGroup","",function(list){
				SENDSECTION_LIST = list;
				LIS_DICT_NUM++;
				initSelect("SendSection","小组",SENDSECTION_LIST,callback);
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
	}
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
	}
	//获取危急值员工部门列表
	function loadDeptIds(callback){
		var url = GET_DEPTIDS_URL;
		
		DEPT_IDS = [DEPTID];
			
		url += '?fields=CVCriticalValueEmpIdDeptLink_DeptID' +
			'&where=cvcriticalvalueempiddeptlink.IsUse=1 and cvcriticalvalueempiddeptlink.EmpID=' + EMPID;
			
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
	
	//更新后处理
	function afterUpdate(id){
		var url = GET_MSG_LIST_URL,
			cols = config.cols[0],
			fields = [];
			
		for(var i in cols){
			fields.push('SCMsg_' + cols[i].field);
		}
		
		url += '?fields=' + fields.join(',') + '&where=scmsg.Id=' + id;
		
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data.success){
				var list = data.value.list || [];
				tableInd.updateRowItem(list[0],'Id');
				changeButton(id);
			}else{
				layer.msg(data.msg);
			}
		});
	};
	//更改按钮状态
	function changeButton(id){
		var that = tableInd.instance.config.instance,
			list = tableInd.table.cache[that.key] || [],
			len = list.length,
			index = null;
			
		for(var i=0;i<len;i++){
			if(list[i].Id == id){
				index = i;
				break;
			}
		}
		
		if(index == null){//不存在
			return false;
		}else{
			var tr = that.layBody.find('tr[data-index="'+ index +'"]');
			var td = tr.find('td[data-field="BUTTON"]');
			var button = td.find('.layui-table-cell');
			
			button.html('');
		}
	};
	//初始化
	function init(){
		//获取危急值员工部门列表
		loadDeptIds(function(){
			//初始化Lis字典表信息
			initLisDict();
			//默认加载数据
			onSearch();
		});
	};
	
	window.afterUpdate = afterUpdate;
	//初始化
	init();
});