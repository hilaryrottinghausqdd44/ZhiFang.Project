layui.extend({
	uxutil:'ux/util'
}).use(['uxutil','form'],function(){
	var $=layui.$,
		form = layui.form,
		uxutil = layui.uxutil;
	
	//获取消息列表服务地址
	var GET_MSG_LIST_URL = uxutil.path.LIIP_ROOT + '/ServerWCF/IMService.svc/ST_UDTO_SearchSCMsgByHQL';
	//获取消息处理列表服务地址
	var GET_MSG_HANDLE_LIST_URL = uxutil.path.LIIP_ROOT + '/ServerWCF/IMService.svc/ST_UDTO_SearchSCMsgHandleByHQL';
	//调用第三方服务地址
	var TO_THIRD_PATRY_URL = uxutil.path.LIIP_ROOT + '/ServerWCF/IMService.svc/ST_UDTO_ReSendSCMsgHandleToHISInterface'
	
	//外部参数
	var PARAMS = uxutil.params.get(true);
	//消息ID
	var ID = PARAMS.ID;
	var HANDLE_FIELDS = ['Id','HandlerID','HandlerName','HandleTime','HandleDesc'];
	//处理记录列表
	var HANDLE_LIST = [];
	
	//提交
	$('#submit_button').on('click',function(){
		onSubmit();
	});
	//取消
	$('#cancel_button').on('click',function(){
		parent.layer.closeAll('iframe');
	});
	
	//提交数据
	function onSubmit(){
		var HandleId = $("#HandleList").val(),
			pwd = $("#pwd").val();
		
		if(!HandleId){
			layer.msg("请选择一个处理记录！");
			return;
		}
		if(!pwd){
			layer.msg("请输入当前处理人密码！");
			return;
		}
		
		uxutil.server.ajax({
			url:TO_THIRD_PATRY_URL,
			data:{
				SCMsgID:ID,
				SCMsgHandleID:HandleId,
				PWD:pwd
			}
		},function(data){
			if(data.success){
				parent.layer.closeAll('iframe');
				parent.parent.layer.closeAll('iframe');
			}else{
				layer.msg(data.msg);
			}
		},true);
	};
	//获取处理数据
	function getHandleInfo(msgId,callback){
		var fields = 'SCMsgHandle_' + HANDLE_FIELDS.join(',SCMsgHandle_');
		var url = GET_MSG_HANDLE_LIST_URL + '?fields=' + fields + '&where=scmsghandle.MsgID=' + msgId;
		
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data.success){
				HANDLE_LIST = (data.value || {}).list || [];
				callback();
			}else{
				layer.msg(data.msg);
			}
		});
	};
	//初始化页面
	function initHtml(){
		var html = [];
			html.push('<select name="HandleList" id="HandleList" lay-filter="HandleList">');
			
		for(var i in HANDLE_LIST){
			var content = HANDLE_LIST[i].HandlerName + ' [' + HANDLE_LIST[i].HandleTime + '] ' + ' (处理意见：' + HANDLE_LIST[i].HandleDesc + ')';
			html.push('<option value="' + HANDLE_LIST[i].Id + '"' + (i == 0 ? 'checked' : '') + '>' + content + '</option>');
		}
		
		html.push('</select>');
		$("#HandleListDiv").html(html.join(""));
		
		form.render();
	};
	//初始化
	function init(){
		//获取处理数据
		getHandleInfo(ID,function(){
			initHtml();
		});
	};
	init();
});