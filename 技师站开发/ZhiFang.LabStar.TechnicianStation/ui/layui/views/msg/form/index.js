layui.extend({
	uxutil:'ux/util',
}).use(['uxutil','element','form','table'],function(){
	var $=layui.$,
		element = layui.element,
		form = layui.form,
		table = layui.table,
		uxutil = layui.uxutil;
	
	//获取消息内容服务地址
	var GET_MSG_INFO_URL = uxutil.path.LIIP_ROOT + '/ServerWCF/IMService.svc/ST_UDTO_SearchSCMsgById';
	//获取消息处理列表服务地址
	var GET_MSG_HANDLE_LIST_URL = uxutil.path.LIIP_ROOT + '/ServerWCF/IMService.svc/ST_UDTO_SearchSCMsgHandleByHQL';
	//获取员工角色列表
	var GET_EMP_ROLE_LIST_URL = uxutil.path.LIIP_ROOT + '/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACEmpRolesByHQL';
	//外部参数
	var PARAMS = uxutil.params.get(true);
	//消息ID
	var ID = PARAMS.ID;
	//查看标志
	var ISSHOW = PARAMS.ISSHOW;
	//发出者本人
	var SELF = PARAMS.SELF;
	//需要的字段
	var FIELDS = [
		//ID,消息内容,消息类型名称,消息类型编码,所属系统名称,发送站点名称,发送IP地址
		'Id','MsgContent','MsgTypeName','MsgTypeCode','SystemCName','SendNodeName','SendIPAddress',
		//消息发送者姓名,发送小组名称,接收站点名称,接收IP地址,接收小组姓名
		'SenderName','SendSectionName','RecNodeName','RecIPAddress','RecSectionName',
		//接收机构名称,接收科室名称,接收科室ID,接收者姓名,要求回复时间,拒收消息接收实验类型名称
		'RecLabName','RecDeptName','RecDeptID','ReceiverName','RequireReplyTime','UnRecSectorTypeName',
		//已读标志(0未查阅,1已查阅),消息确认标志,消息确认时间,消息确认备注,要求确认时间,确认人姓名
		'ReadFlag','ConfirmFlag','ConfirmDateTime','ConfirmMemo','RequireConfirmTime','ConfirmerName',
		//处理中标志,处理标志,处理时间,要求处理时间,
		'HandlingFlag','HandleFlag','HandlingDateTime','LastRequireHandleTime',
		//危急值上报标志,超时处理标志
		'WarningUploaderFlag','TimeOutCallFlag',
		//被通知医生,通知次数,最后通知时间,通知医生的人员名称
		'ConfirmNotifyDoctorName','NotifyDoctorCount','NotifyDoctorLastDateTime','NotifyDoctorByEmpName',
		//新增时间
		'DataAddTime'
	];
	//消息处理需要的字段
	var HANDLE_FIELDS = [
		//处理人,处理时间,处理备注,处理意见
		'HandlerName','HandleTime','Memo','HandleDesc'
	];
	//常规、急诊、质控
	var TEST_TYPE_NO_MAP = {
		"1":"常规",
		"2":"急诊",
		"3":"质控"
	};
	//消息内容
	var INFO = {};
	//消息处理内容
	var HANDLE_INFO = {};
	//医生身份
	var IS_DOCTOR = false;
	//护士身份
	var IS_NURSE = false;
	
	//消息上报
	$("#upload_button").on("click",function(){
		var win = $(window),
			maxWidth = win.width(),
			maxHeight = win.height(),
			width = maxWidth > 600 ? 600 : maxWidth;
			height = maxHeight > 400 ? 400 : maxHeight;
			
		layer.open({
			title:'危急值上报',
			type:2,
			content:'../upload/form.html?id=' + ID + '&RecDeptID=' + INFO.RecDeptID + '&t=' + new Date().getTime(),
			maxmin:true,
			toolbar:true,
			resize:true,
			area:[width+'px',height+'px']
		});
	});
	//超时处理
	$("#timeout_button").on("click",function(){
		var win = $(window),
			maxWidth = win.width(),
			maxHeight = win.height(),
			width = maxWidth > 600 ? 600 : maxWidth;
			height = maxHeight > 400 ? 400 : maxHeight;
			
		layer.open({
			title:'危急值超时处理',
			type:2,
			content:'../timeout/form.html?id=' + ID + '&RecDeptID=' + INFO.RecDeptID + '&t=' + new Date().getTime(),
			maxmin:true,
			toolbar:true,
			resize:true,
			area:[width+'px',height+'px']
		});
	});
	
	//消息确认
	$("#confirm_button").on("click",function(){
		var win = $(window),
			maxWidth = win.width(),
			maxHeight = win.height(),
			width = maxWidth > 500 ? 500 : maxWidth;
			height = maxHeight > 340 ? 340 : maxHeight;
			
		layer.open({
			title:'危急值确认',
			type:2,
			content:'confirm.html?id=' + ID + '&DataAddTime=' + INFO.DataAddTime + '&RecDeptID=' + INFO.RecDeptID + '&MsgTypeCode=' + INFO.MsgTypeCode + '&t=' + new Date().getTime(),
			maxmin:true,
			toolbar:true,
			resize:true,
			area:[width+'px',height+'px']
		});
	});
	//再次确认并通知
	$("#confirm_again_button").on("click",function(){
		var win = $(window),
			maxWidth = win.width(),
			maxHeight = win.height(),
			width = maxWidth > 500 ? 500 : maxWidth;
			height = maxHeight > 340 ? 340 : maxHeight;
			
		layer.open({
			title:'危急值确认',
			type:2,
			content:'confirmAgain.html?id=' + ID + '&RecDeptID=' + INFO.RecDeptID + '&MsgTypeCode=' + INFO.MsgTypeCode + '&t=' + new Date().getTime(),
			maxmin:true,
			toolbar:true,
			resize:true,
			area:[width+'px',height+'px']
		});
	});
	//消息处理
	$("#handle_button").on("click",function(){
		var win = $(window),
			maxWidth = win.width(),
			maxHeight = win.height(),
			width = maxWidth > 500 ? 500 : maxWidth;
			height = maxHeight > 400 ? 400 : maxHeight;
			
		layer.open({
			title:'危急值处理',
			type:2,
			content:'handle.html?id=' + ID + '&RecDeptID=' + INFO.RecDeptID + '&MsgTypeCode=' + INFO.MsgTypeCode + '&t=' + new Date().getTime(),
			maxmin:true,
			toolbar:true,
			resize:true,
			area:[width+'px',height+'px']
		});
	});
	//取消
	$("#cancel_button").on("click",function(){
		parent.layer.closeAll('iframe');
	});
	
	//初始化页面
	function initHtml(){
		var data = INFO,
			MsgObjJson = $.parseJSON(data.MsgContent),//消息对象
			MsgContent = MsgObjJson.MSG.MSGCONTENT,//消息内容
			MsgTitle = MsgContent.MSGTITLE,//消息标题
			MsgKey = MsgContent.MSGKEY,//病人信息
			MsgList = MsgContent.MSGBODY.MSG,//消息列表
			MsgList = MsgList.length ? MsgList : [MsgList],
			msgAll = [];
		
		//样本信息
		for(var i in MsgKey){
			var dom = $("#" + i);
			if(dom[0]){
				dom.html(MsgKey[i]);
			}
		}
		
		//危急值消息内容
		for(var i in MsgList){
			if(MsgList[i].DETAILDESC){
				msgAll.push(MsgList[i].DETAILDESC);
			}
			MsgList[i].TESTTYPENAME = TEST_TYPE_NO_MAP[MsgList[i].TESTTYPENO];
		}
		for(var i in msgAll){
			msgAll[i] = '<div>' + msgAll[i] + '</div>';
		}
		$("#MsgContent").html(msgAll.join(''));
		
		//项目信息
		table.render({
			elem:'#Items',
			cols:[[
				{field:'PARITEMNAME',width:150,title:'医嘱项目名称'},
				{field:'PARITEMNO',width:100,title:'编号'},
				{field:'PARITEMSNAME',width:80,title:'简称'},
				{field:'TESTITEMNAME',width:150,title:'项目名称'},
				{field:'TESTITEMSNAME',width:80,title:'简称'},
				{field:'TESTTYPENAME',width:90,title:'检测类型'},
				{field:'REPORTVALUEALL',width:80,title:'报告值'},
				{field:'UNIT',width:80,title:'单位'},
				{field:'RESULTSTATUS',width:90,title:'结果状态'},
				{field:'REFRANGE',width:90,title:'参考范围'},
				{field:'MASTERDESC',minWidth:90,title:'结果描述'}
			]],
			data:MsgList
		});
		if(SELF){
			//是否超时
			var serverDateTimes = uxutil.server.date.getTimes(),
				isTimeout = false;
			if(!data.ConfirmDateTime && data.RequireConfirmTime){
				var RequireConfirmTime = data.RequireConfirmTime;
				var timesStr = uxutil.date.difference(RequireConfirmTime,serverDateTimes);
				if(timesStr && timesStr.slice(0,1) != '-'){
					isTimeout = true;
				}
			}
			//危急值上报按钮
			if(data.WarningUploaderFlag == '0'){$("#upload_button").removeClass('layui-hide');}
			//危急值超时处理：要求确认时间存在+超时+没有确认+没有做过超时处理
			if(data.RequireConfirmTime && isTimeout && data.HandleFlag == '0' && data.TimeOutCallFlag == '0'){$("#timeout_button").removeClass('layui-hide');}
		}else{
			if(!ISSHOW && IS_NURSE && data.ConfirmFlag == '0'){
				$("#confirm_button").removeClass('layui-hide');
			}
			if(!ISSHOW && IS_DOCTOR && data.HandleFlag == '0'){
				$("#handle_button").removeClass('layui-hide');
			}
			if(!ISSHOW && IS_NURSE && data.ConfirmFlag == '1' && data.HandleFlag == '0'){
				//再次确认并通知按钮
				var serverDateTimes = new Date().getTime();
				var times = new Date(data.LastRequireHandleTime).getTime();
				//处理超时，直接退出判断
				if(times < serverDateTimes){
					$("#confirm_again_button").removeClass('layui-hide');
				}
			}
		}
		//form.render();
	};
	//初始化处理信息
	function initHanldeHtml(){
		//通知信息
		$("#ConfirmNotifyDoctorName").html(INFO.ConfirmNotifyDoctorName || '');
		$("#NotifyDoctorCount").html(INFO.NotifyDoctorCount || '');
		$("#NotifyDoctorLastDateTime").html(INFO.NotifyDoctorLastDateTime || '');
		$("#NotifyDoctorByEmpName").html(INFO.NotifyDoctorByEmpName || '');
		//确认信息
		$("#ConfirmerName").html(INFO.ConfirmerName || '');
		$("#ConfirmDateTime").html(INFO.ConfirmDateTime || '');
		$("#ConfirmMemo").html(INFO.ConfirmMemo || '');
		//处理信息
		for(var i in HANDLE_INFO){
			var dom = $("#" + i);
			if(dom[0]){
				dom.html(HANDLE_INFO[i]);
			}
		}
		$("#HandleMemo").html(HANDLE_INFO.Memo || '');
	};
	//获取消息内容
	function getMsgInfoById(id,callback){
		var fields = FIELDS.length > 0 ? 'SCMsg_' + FIELDS.join(',SCMsg_') : '',
			url = GET_MSG_INFO_URL + '?id=' + id + '&fields=' + fields;
		
		var loadIndex = layer.load();//开启加载层
		uxutil.server.ajax({
			url:url
		},function(data){
			layer.close(loadIndex);//关闭加载层
			if(data.success){
				INFO = data.value || {};
				callback();
			}else{
				layer.msg(data.msg);
			}
		});
	};
	//获取消息处理内容
	function getMsgHandleInfoByMsgId(msgId,callback){
		var fields = HANDLE_FIELDS.length > 0 ? 'SCMsgHandle_' + HANDLE_FIELDS.join(',SCMsgHandle_') : '',
			url = GET_MSG_HANDLE_LIST_URL + '?fields=' + fields + '&where=scmsghandle.MsgID=' + msgId;
		
		var loadIndex = layer.load();//开启加载层
		uxutil.server.ajax({
			url:url
		},function(data){
			layer.close(loadIndex);//关闭加载层
			if(data.success){
				var list = (data.value || {}).list || [];
				if(list.length == 1){
					HANDLE_INFO = list[0];
				}
				callback();
			}else{
				layer.msg(data.msg);
			}
		});
	};
	
	//获取员工角色列表
	function getEmpRoleList(callback){
		var url = GET_EMP_ROLE_LIST_URL + "?isPlanish=true&fields=RBACEmpRoles_RBACRole_Id";
		url += "&where=rbacemproles.IsUse=1 and rbacemproles.HREmployee.Id=" + uxutil.cookie.get(uxutil.cookie.map.USERID);
		
		uxutil.server.ajax({
			url:url,
		},function(data){
			if(data.success){
				var list = data.value.list || [];
				
				for(var i in list){
					switch(list[i].RBACEmpRoles_RBACRole_Id){
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
	
	//初始化
	function init(){
		//获取消息内容
		getMsgInfoById(ID,function(){
			//获取消息处理内容
			getMsgHandleInfoByMsgId(ID,function(){
				//获取员工角色列表
				getEmpRoleList(function(){
					initHtml();
					initHanldeHtml();
				});
			});
		});
	};
	
	//初始化
	init();
});