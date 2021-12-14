layui.extend({
	uxutil:'ux/util'
}).use(['uxutil','form','slider'],function(){
	var $ = layui.$,
		uxutil = layui.uxutil,
		form = layui.form,
		slider = layui.slider;
		
	//新增服务
	var ADD_INFO_URL = uxutil.path.ROOT + '/ServerWCF/LIIPService.svc/ST_UDTO_AddIntergrateSystemSet';
	//修改服务
	var UPDATE_INFO_URL = uxutil.path.ROOT + '/ServerWCF/LIIPService.svc/ST_UDTO_UpdateIntergrateSystemSetByField';
	
	var TYPE = null,//类型，值：add/edit
		DEFAULT_DATA = {},
		AFTER_SAVE = function(){};//保存后回调函数
	
	var DispOrderSlider = slider.render({
		elem:'#DispOrder',
		max:9999,
		input:true
	});
	//表单reset重置渲染
	$(document).unbind('reset');//移除事件监听
	$(document).on('reset','.layui-form',function(){
		var filter = $(this).attr('lay-filter');
		setTimeout(function(){
			form.val("form",DEFAULT_DATA);
			DispOrderSlider.setValue(DEFAULT_DATA.DispOrder);//动态给滑块赋值
		},50);
	});
	//数据提交
	form.on('submit(submit)', function(data){
		onSubmit(data.field);
		return false; //阻止表单跳转。如果需要表单跳转，去掉这段即可。
	});
	
	function onSubmit(params){
		var config = {type:'post'};
		params.IsUse = params.IsUse == 'on' ? true : false;
		
		if(TYPE == 'add'){
			config.url = ADD_INFO_URL;
			config.data = JSON.stringify({
				entity:params
			});
		}else if(TYPE == 'edit'){
			config.url = UPDATE_INFO_URL;
			params.Id = DEFAULT_DATA.Id;
			config.data = JSON.stringify({
				entity:params,
				fields:'Id,SystemName,SystemCode,SystemHost,SystemEntryAddress,Memo,IsUse,DispOrder'
			});
		}else{
			layer.alert("非法进入该页面，无法保存！");
			return;
		}
		
		uxutil.server.ajax(config,function(data){
			if(data.success){
				AFTER_SAVE(data.value);
			}else{
				layer.msg(data.msg);
			}
		});
	}
	
	//初始化数据
	window.initData = function(type,data,afterSave){
		TYPE = type;
		if(typeof afterSave == 'function'){
			AFTER_SAVE = afterSave;
		}
		//DEFAULT_DATA = data;
		//因为数据为页面外部调用传入，
		//layui的442行each处理中代码if(obj.constructor === Object)会将data与Object比对返回false
		//所以采用赋值方式创建新的Object，规避该判断
		DEFAULT_DATA = {};
		for(var key in data){
			DEFAULT_DATA[key] = data[key];
		}
		
		form.val("form",DEFAULT_DATA);
	}
});