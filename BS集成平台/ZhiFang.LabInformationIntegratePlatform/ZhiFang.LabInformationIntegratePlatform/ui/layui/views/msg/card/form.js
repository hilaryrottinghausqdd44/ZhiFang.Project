layui.extend({
	uxutil:'ux/util',
}).use(['uxutil','element','form'],function(){
	var $=layui.$,
		element = layui.element,
		form = layui.form,
		laydate = layui.laydate,
		uxtable = layui.uxtable,
		uxutil = layui.uxutil;
	
	//获取消息内容服务地址
	var GET_MSG_INFO_URL = uxutil.path.ROOT + '/ServerWCF/IMService.svc/ST_UDTO_SearchSCMsgById';
	//消息确认服务地址
	//var CONFIRM_URL = uxutil.path.ROOT + "/ServerWCF/IMService.svc/ST_UDTO_SCMsgByConfirm";
	//消息确认并处理服务地址
	var HANDLE_URL = uxutil.path.LIIP_ROOT + '/ServerWCF/IMService.svc/ST_UDTO_AddSCMsgHandle';
	//获取用户信息
	var GET_EMP_INFO_URL = uxutil.path.ROOT + "/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACUserByHQL";
	//外部参数
	var PARAMS = uxutil.params.get(true);
	//消息ID
	var ID = PARAMS.ID;
	//需要的字段
	var FIELDS = [
		//ID,消息内容,消息类型名称,所属系统名称,发送站点名称,发送IP地址
		'Id','MsgContent','MsgTypeName','SystemCName','SendNodeName','SendIPAddress',
		//消息发送者姓名,发送小组名称,接收站点名称,接收IP地址,接收小组姓名
		'SenderName','SendSectionName','RecNodeName','RecIPAddress','RecSectionName',
		//接收机构名称,接收科室名称,接收者姓名,要求回复时间,拒收消息接收实验类型名称
		'RecLabName','RecDeptName','ReceiverName','RequireReplyTime','UnRecSectorTypeName',
		//已读标志(0未查阅,1已查阅),消息确认标志,消息确认时间,消息确认备注,要求确认时间,确认人姓名
		'ReadFlag','ConfirmFlag','ConfirmDateTime','ConfirmMemo','RequireConfirmTime','ConfirmerName',
		//处理中标志,处理标志,处理时间,处理人姓名,要求处理时间,
		'HandlingFlag','HandleFlag','HandlingDateTime','HandlerName','RequireHandleTime'
	];
	//消息内容
	var INFO = {};
	
	//消息确认并处理
	$("#submit_button").on("click",function(){
		layer.prompt({
			formType:2,
			value:'',
			title:'处理意见',
			yes:function(index,layero){
				var prompt = layero.find('.layui-layer-input');
				var value = prompt.val();
				if(value.length > 500) {
					layer.tips('&#x6700;&#x591A;&#x8F93;&#x5165;'+ 500 +'&#x4E2A;&#x5B57;&#x6570;',prompt,{tips:1});
				}else {
					onSubmitClick(value,function(){
						layer.close(index);
					});
				}
			}
		});
	});
	//取消
	$("#cancel_button").on("click",function(){
		parent.layer.closeAll('iframe');
	});
	//消息确认并处理
	function onSubmitClick(HandleDesc,callback){
		var params = {
			"entity":{
				"MsgID":ID,
				"HandlerID":uxutil.cookie.get(uxutil.cookie.map.USERID),
				"HandlerName":uxutil.cookie.get(uxutil.cookie.map.USERNAME),
				"HandleDeptID":uxutil.cookie.get(uxutil.cookie.map.DEPTID),
				"HandleDesc":HandleDesc || '确认并处理',
				//"Memo":$("#Memo").val() || '',
				"HandleNodeName":"",
				"HandleNodeIPAddress":""
			}
		};
		uxutil.server.ajax({
			url:HANDLE_URL,
			type:'post',
			data:JSON.stringify(params)
		},function(result){
			if(result.success){
				layer.msg('保存成功', {
					time:500
				}, function(){
					callback();
					parent.layer.closeAll('iframe');
					parent.afterUpdate(ID);
				});
			}else{
				layer.msg(result.msg);
			}
		},true);
	};
	//初始化页面
	function initHtml(){
		var html = [];
		//确认信息
		html.push('<button class="layui-btn layui-btn-sm layui-btn-primary">');
		if(INFO.ConfirmFlag == '1'){
			html.push('<span class="layui-badge layui-bg-blue" style="margin-left:0;">已确认</span>');
			if(INFO.ConfirmDateTime){
				html.push(' ' + INFO.ConfirmDateTime);
			}
			if(INFO.ConfirmerName){
				html.push(' ' + INFO.ConfirmerName);
			}
		}else{
			html.push('<span class="layui-badge layui-bg-gray" style="margin-left:0;">未确认</span>');
			if(INFO.RequireConfirmTime){
				html.push(' 要求确认时间：' + INFO.RequireConfirmTime);
			}
		}
		html.push('</button>');
		
		//处理信息
		html.push('<button class="layui-btn layui-btn-sm layui-btn-primary">');
		if(INFO.HandleFlag == '1'){
			html.push('<span class="layui-badge layui-bg-green" style="margin-left:0;">已处理</span>');
			if(INFO.HandlingDateTime){
				html.push(' ' + INFO.HandlingDateTime);
			}
			if(INFO.HandlerName){
				html.push(' ' + INFO.HandlerName);
			}
		}else{
			html.push('<span class="layui-badge layui-bg-gray" style="margin-left:0;">未处理</span>');
			if(INFO.RequireHandleTime){
				html.push(' 要求处理时间：' + INFO.RequireHandleTime);
			}
		}
		html.push('</button>');
		
		$("#MsgStatus").html(html.join(''));
		
		//消息内容
		$("#MsgContent").html(INFO.MsgContent);
		
		if(INFO.ConfirmFlag == '0'){$("#submit_button").removeClass('layui-hide');}
		form.render();
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
	//初始化
	function init(){
		getMsgInfoById(ID,function(){
			initHtml();
		});
	};
	
	//初始化
	init();
});