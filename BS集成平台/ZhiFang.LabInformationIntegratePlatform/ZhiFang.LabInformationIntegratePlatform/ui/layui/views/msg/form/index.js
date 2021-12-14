layui.extend({
	uxutil:'ux/util',
}).use(['uxutil','element','form','table'],function(){
	var $=layui.$,
		element = layui.element,
		form = layui.form,
		table = layui.table,
		uxutil = layui.uxutil;
	
	//获取消息内容服务地址
	var GET_MSG_INFO_URL = uxutil.path.ROOT + '/ServerWCF/IMService.svc/ST_UDTO_SearchSCMsgById';
	//获取消息处理列表服务地址
	var GET_MSG_HANDLE_LIST_URL = uxutil.path.ROOT + '/ServerWCF/IMService.svc/ST_UDTO_SearchSCMsgHandleByHQL';
	//获取员工角色列表
	var GET_EMP_ROLE_LIST_URL = uxutil.path.ROOT + '/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACEmpRolesByHQL';
	//外部参数
	var PARAMS = uxutil.params.get(true);
	//消息ID
	var ID = PARAMS.ID;
	//查看标志
	var ISSHOW = PARAMS.ISSHOW;
	//分隔符号
	var SEPARATOR = PARAMS.SEPARATOR || '|^|';
	//发出者本人
	var SELF = PARAMS.SELF;
	//需要的字段
	var FIELDS = [
		//ID,消息内容,消息类型名称,所属系统名称,发送站点名称,发送IP地址
		'Id','MsgContent','MsgTypeName','SystemCName','SendNodeName','SendIPAddress',
		//消息发送者姓名,发送小组名称,接收站点名称,接收IP地址,接收小组姓名
		'SenderName','SendSectionName','RecNodeName','RecIPAddress','RecSectionName',
		//接收机构名称,接收科室名称,接收科室ID,接收者姓名,要求回复时间,拒收消息接收实验类型名称
		'RecLabName','RecDeptName','RecDeptID','ReceiverName','RequireReplyTime','UnRecSectorTypeName',
		//已读标志(0未查阅,1已查阅),消息确认标志,消息确认时间,消息确认备注,要求确认时间,确认人姓名
		'ReadFlag','ConfirmFlag','ConfirmDateTime','ConfirmMemo','RequireConfirmTime','ConfirmerName',
		//处理中标志,处理标志,处理时间,要求处理时间,
		'HandlingFlag','HandleFlag','HandlingDateTime','RequireHandleTime',
		//危急值上报标志,超时处理标志
		'WarningUploaderFlag','TimeOutCallFlag'
	];
	//消息处理需要的字段
	var HANDLE_FIELDS = [
		//处理人,处理时间,处理备注,处理意见
		'HandlerName','HandleTime','Memo','HandleDesc'
	];
	//消息内容
	var INFO = {};
	//消息处理内容
	var HANDLE_INFO = {};
	//医生身份
	var IS_DOCTOR = false;
	//护士身份
	var IS_NURSE = false;
	
	//消息确认
	$("#confirm_button").on("click",function(){
		var win = $(window),
			maxWidth = win.width(),
			maxHeight = win.height(),
			width = maxWidth > 500 ? 500 : maxWidth;
			height = maxHeight > 340 ? 340 : maxHeight;
			
		layer.open({
			title:'消息确认',
			type:2,
			content:'confirm.html?id=' + ID + '&RecDeptID=' + INFO.RecDeptID + '&t=' + new Date().getTime(),
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
			height = maxHeight > 350 ? 350 : maxHeight;
			
		layer.open({
			title:'消息处理',
			type:2,
			content:'handle.html?id=' + ID + '&RecDeptID=' + INFO.RecDeptID + '&t=' + new Date().getTime(),
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
		var msgList = INFO.MsgContent.split(SEPARATOR),
			len = msgList.length,
			html = [];
		
		html.push('<div class="layui-row layui-col-space5">');
		for(var i=0;i<len;i++){
			var msg = msgList[i];
			if(msg.slice(0,4) == '[C3]'){
				html.push('<div class="layui-col-xs12 layui-col-sm12 layui-col-md12">' + msg.slice(4) + '</div>');
			}else{
				if(msg.slice(0,2) == '[C'){
					html.push('<div class="layui-col-xs6 layui-col-sm4 layui-col-md3">' + msg.slice(4) + '</div>');
				}else{
					html.push('<div class="layui-col-xs6 layui-col-sm4 layui-col-md3">' + msg + '</div>');
				}
			}
		}
		html.push('</div>');
		
		$("#MsgContent").html(html.join(''));
		
		if(!ISSHOW && INFO.ConfirmFlag == '0' && IS_NURSE){$("#confirm_button").removeClass('layui-hide');}
		if(!ISSHOW && INFO.HandleFlag == '0' && IS_DOCTOR){$("#handle_button").removeClass('layui-hide');}
	};
	//初始化处理信息
	function initHanldeHtml(){
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
		
		uxutil.server.ajax({
			url:url
		},function(data){
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
		
		uxutil.server.ajax({
			url:url
		},function(data){
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