layui.extend({
	uxutil:'ux/util'
}).use(['form','uxutil','element'],function(){
	var $ = layui.$,
		element = layui.element;
		form = layui.form,
		uxutil = layui.uxutil;
		
	//获取系统参数列表服务地址
	var GET_PARAMS_LIST_URL = uxutil.path.ROOT + '/ServerWCF/SingleTableService.svc/ST_UDTO_SearchBParameterByHQL';
	//新增系统参数服务地址
	var ADD_PARAMS_URL = uxutil.path.ROOT + '/ServerWCF/SingleTableService.svc/ST_UDTO_AddBParameterByParaNo';
	//修改系统参数服务地址
	var EDIT_PARAMS_URL = uxutil.path.ROOT + '/ServerWCF/SingleTableService.svc/ST_UDTO_UpdateBParameterByParaNoAndField';
	//获取消息类型列表服务
	var GET_MSG_TYPE_LIST_URL = uxutil.path.ROOT + '/ServerWCF/IMService.svc/ST_UDTO_SearchSCMsgTypeByHQL';
	
	//可设置参数编码数组
	var PARAMS_CODES = [
		'Msg_Open','Msg_Audio','Msg_AudioTimes','Msg_ForcedBombScreen',
		'Msg_ForcedBombScreenTimes','Msg_CV_HandleDesc','ZF_LIIP_Msg_CV_After_Handle',
		'ZF_LIIP_Msg_Type_Codes','ZF_LIIP_Msg_Default_Min','Msg_CV_ConfirmOutTimes'
	];
	//可设置系统参数列表
	var PARAMS_LIST = null;
	//消息类型列表
	var MSG_TYPE_LIST = null;
	
	form.render();
	
	//开启消息开关监听
	form.on("switch(Msg_Open)",function(data){
		MsgConfigChange(data.elem.checked);
		onParamsChange({"Name":"消息_开启开关","ParaNo":"Msg_Open","ParaValue":data.elem.checked + ''});
	});
	//消息_默认最小
	form.on("switch(ZF_LIIP_Msg_Default_Min)",function(data){
		onParamsChange({"Name":"消息_默认最小","ParaNo":"ZF_LIIP_Msg_Default_Min","ParaValue":data.elem.checked + ''});
	});
	//开启后消息系统的设置项
	function MsgConfigChange(isOpen){
		if(isOpen){
			$("#msg_config_div").show();
		}else{
			$("#msg_config_div").hide();
		}
	};
	
	//声音提醒开关监听
	form.on("switch(Msg_Audio)",function(data){
		AudioTimesChange(data.elem.checked);
		onParamsChange({"Name":"消息_声音提醒","ParaNo":"Msg_Audio","ParaValue":data.elem.checked + ''});
	});
	//开启声音提醒后，时间间隔设置项
	function AudioTimesChange(isOpen){
		if(isOpen){
			$("#audioTimes_div").show();
		}else{
			$("#audioTimes_div").hide();
		}
	};
	//声音提醒-时间间隔数字变换监听
	$("#Msg_AudioTimes").on("change",function(){
		var ParaValue = $(this).val();
		setTimeout(function(){
			onParamsChange({"Name":"消息_声音提醒时间间隔","ParaNo":"Msg_AudioTimes","ParaValue":ParaValue});
		},500);
	}).on("keyup",function(e){//键盘松开
		e.preventDefault();
		if(e.keyCode === 13){//回车键
			var ParaValue = $(this).val();
			onParamsChange({"Name":"消息_声音提醒时间间隔","ParaNo":"Msg_AudioTimes","ParaValue":ParaValue});
		}
	});
	
	//强制弹屏开关监听
	form.on("switch(Msg_ForcedBombScreen)",function(data){
		ForcedBombScreenTimesChange(data.elem.checked);
		onParamsChange({"Name":"消息_强制弹屏","ParaNo":"Msg_ForcedBombScreen","ParaValue":data.elem.checked + ""});
	});
	//开启强制弹屏后，时间间隔设置项
	function ForcedBombScreenTimesChange(isOpen){
		if(isOpen){
			$("#forcedBombScreenTimes_div").show();
		}else{
			$("#forcedBombScreenTimes_div").hide();
		}
	};
	//强制弹屏-时间间隔数字变换监听
	$("#Msg_ForcedBombScreenTimes").on("change",function(){
		var ParaValue = $(this).val();
		setTimeout(function(){
			onParamsChange({"Name":"消息_强制弹屏时间间隔","ParaNo":"Msg_ForcedBombScreenTimes","ParaValue":ParaValue});
		},500);
	}).on('keyup',function(e){//键盘松开
		e.preventDefault();
		if(e.keyCode === 13){//回车键
			var ParaValue = $(this).val();
			onParamsChange({"Name":"消息_强制弹屏时间间隔","ParaNo":"Msg_ForcedBombScreenTimes","ParaValue":ParaValue});
		}
	});
	
	//是否开启处理后调用第三方服务开关监听
	form.on("switch(ZF_LIIP_Msg_CV_After_Handle)",function(data){
		onParamsChange({"Name":"消息_处理回调第三方服务","ParaNo":"ZF_LIIP_Msg_CV_After_Handle","ParaValue":(data.elem.checked ? "1" : "0") + ""});
	});
	
	//消息_超时确认时间
	$("#Msg_CV_ConfirmOutTimes").on("change",function(){
		var ParaValue = $(this).val();
		ParaValue = parseInt(ParaValue);
		setTimeout(function(){
			onParamsChange({"Name":"消息_超时确认时间","ParaNo":"Msg_CV_ConfirmOutTimes","ParaValue":ParaValue});
		},500);
	}).on("keyup",function(e){
		var value = this.value.replace(/[^\d]/g,'');
		if(value != ""){
			value = parseInt(value);
		}
		this.value = value;
		
		e.preventDefault();
		if(e.keyCode === 13){//回车键
			var ParaValue = $(this).val();
			ParaValue = parseInt(ParaValue);
			onParamsChange({"Name":"消息_超时确认时间","ParaNo":"Msg_CV_ConfirmOutTimes","ParaValue":ParaValue});
		}
	});
	
	//消息_危急值_处理意见-内容变换监听
	$("#Msg_CV_HandleDesc").on("change",function(){
		var ParaValue = $(this).val();
		setTimeout(function(){
			onParamsChange({"Name":"消息_危急值_处理意见","ParaNo":"Msg_CV_HandleDesc","ParaValue":ParaValue});
		},500);
	}).on("keyup",function(e){//键盘松开
		e.preventDefault();
		if(e.keyCode === 13){//回车键
			var ParaValue = $(this).val();
			onParamsChange({"Name":"消息_危急值_处理意见","ParaNo":"Msg_CV_HandleDesc","ParaValue":ParaValue});
		}
	});
	
	//参数设置变化处理
	function onParamsChange(data){
		onSaveParams(data);
	};
	
	//保存设置结果
	function onSaveParams(data){
		var hasParams = false;
		for(var i in PARAMS_LIST){
			if(PARAMS_LIST[i].ParaNo == data.ParaNo){
				hasParams = true;
				data.Id = PARAMS_LIST[i].Id;
				break;
			}
		}
		if(hasParams){
			onEditParams(data);
		}else{
			onAddParams(data);
		}
	};
	//保存设置结果-新增
	function onAddParams(data){
		data.IsUse = true;
		var index = layer.load();
		uxutil.server.ajax({
			url:ADD_PARAMS_URL,
			type:'post',
			data:JSON.stringify({
				entity:data
			})
		},function(result){
			layer.close(index);
			if(result.success){
				PARAMS_LIST.push({
					Id:result.value.id,
					ParaValue:data.ParaValue,
					ParaNo:data.ParaNo
				});
				layer.msg('保存成功',{time:500});
			}else{
				layer.msg(data.msg);
			}
		});
	};
	//保存设置结果-修改
	function onEditParams(data){
		data.IsUse = true;
		var index = layer.load();
		uxutil.server.ajax({
			url:EDIT_PARAMS_URL,
			type:'post',
			data:JSON.stringify({
				entity:data,
				fields:'Id,ParaValue,Name,IsUse'
			})
		},function(data){
			layer.close(index);
			if(data.success){
				layer.msg('保存成功',{time:500});
			}else{
				layer.msg(data.msg);
			}
		});
	};
	
	//获取可设置系统参数列表
	function getParamsList(callback){
		var fields = ['BParameter_Id','BParameter_ParaNo','BParameter_ParaValue'];
		var paraNoStr = '';
		if(PARAMS_CODES && PARAMS_CODES.length > 0){
			paraNoStr = "'" + PARAMS_CODES.join("','") + "'";
		}
		var url = GET_PARAMS_LIST_URL + '?fields=' + fields.join(',') + 
			'&where=bparameter.ParaNo in(' + paraNoStr + ')';
			
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data.success){
				PARAMS_LIST = (data.value || {}).list || [];
				callback();
			}else{
				layer.msg(data.msg);
			}
		});
	};
	//获取消息类型列表
	function getMsgTypeList(callback){
		var fields = ['SystemCName','CName','Code'];
		var url = GET_MSG_TYPE_LIST_URL + '?where=scmsgtype.IsUse=1&fields=SCMsgType_' + fields.join(',SCMsgType_');
			
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data.success){
				MSG_TYPE_LIST = (data.value || {}).list || [];
				callback();
			}else{
				layer.msg(data.msg);
			}
		});
	};
	//初始化页面
	function initHtml(){
		var values = {};
		
		for(var i in PARAMS_LIST){
			values[PARAMS_LIST[i].ParaNo] = PARAMS_LIST[i].ParaValue;
			if(values[PARAMS_LIST[i].ParaNo]){
				if(values[PARAMS_LIST[i].ParaNo].toLocaleUpperCase() == 'TRUE'){
					values[PARAMS_LIST[i].ParaNo] = true;
				}else if(values[PARAMS_LIST[i].ParaNo].toLocaleUpperCase() == 'FALSE'){
					values[PARAMS_LIST[i].ParaNo] = false;
				}
			}
			
			if(PARAMS_LIST[i].ParaNo == 'ZF_LIIP_Msg_Type_Codes'){
				var msgTypeCodes = PARAMS_LIST[i].ParaValue.split(',');
				for(var i in msgTypeCodes){
					values[msgTypeCodes[i]] = true;
				}
			}
		}
		values.ZF_LIIP_Msg_CV_After_Handle = (values.ZF_LIIP_Msg_CV_After_Handle == "1" ? true : false);
		form.val("form",values);
		
		MsgConfigChange(values.Msg_Open);
		AudioTimesChange(values.Msg_Audio);
		ForcedBombScreenTimesChange(values.Msg_ForcedBombScreen);
	};
	//初始化消息类型组件
	function initMsgTypeListHtml(){
		//组装消息类型
		var systemMap = {},
			html = [];
			
		for(var i in MSG_TYPE_LIST){
			if(!systemMap[MSG_TYPE_LIST[i].SystemCName]){
				systemMap[MSG_TYPE_LIST[i].SystemCName] = {
					SystemCName:MSG_TYPE_LIST[i].SystemCName,
					TypeList:[]
				};
			}
			systemMap[MSG_TYPE_LIST[i].SystemCName].TypeList.push({
				CName:MSG_TYPE_LIST[i].CName,
				Code:MSG_TYPE_LIST[i].Code
			});
		}
		for(var i in systemMap){
			html.push('<div class="layui-form-item">');
			html.push('<label class="layui-form-label" style="width:120px;padding:9px 0;">' + systemMap[i].SystemCName + '：</label>');
			html.push('<div class="layui-input-block">');
			
			var list = systemMap[i].TypeList;
			for(var j in list){
				html.push('<input type="checkbox" mType="Msg_Type_Codes" name="' + list[j].Code +
				'" lay-filter="' + list[j].Code + '" title="' + list[j].CName + '" lay-skin="primary">');
			}
			
			html.push('</div>');
		}
		
		$("#ZF_LIIP_Msg_Type_Codes").html(html.join(''));
		
		//开启展现的消息类型
		var MsgTypeList = $("#ZF_LIIP_Msg_Type_Codes").find('input[mType="Msg_Type_Codes"]');
		MsgTypeList.each(function(){
			var filter = $(this).attr('lay-filter');
			form.on('checkbox(' + filter + ')',function(data){
				onMsgTypeCodesChange(data,MsgTypeList);
			});
		});
	};
	//开启展现的消息类型勾选变化处理
	function onMsgTypeCodesChange(data,MsgTypeList){
		var value = [];
		for(var i in MsgTypeList){
			if(MsgTypeList[i].checked){
				value.push(MsgTypeList[i].name);
			}
		}
		onParamsChange({"Name":"消息_展现消息类型","ParaNo":"ZF_LIIP_Msg_Type_Codes","ParaValue":value.join(',')});
	};
	
	//初始化
	function init(){
		//获取可设置系统参数列表
		getParamsList(function(){
			//获取消息类型列表
			getMsgTypeList(function(){
				//初始化消息类型组件
				initMsgTypeListHtml();
				//初始化页面
				initHtml();
			});
		});
	};
	init();
});