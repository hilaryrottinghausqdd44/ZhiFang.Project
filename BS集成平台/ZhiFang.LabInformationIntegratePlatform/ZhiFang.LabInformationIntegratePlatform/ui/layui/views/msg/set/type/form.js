layui.extend({
	uxutil:'ux/util'
}).use(['uxutil','form'],function(){
	var $ = layui.$,
		uxutil = layui.uxutil,
		form = layui.form;
	
	//获取系统列表服务
	var GET_SYSTEM_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LIIPService.svc/ST_UDTO_SearchIntergrateSystemSetByHQL';
	//新增服务
	var ADD_INFO_URL = uxutil.path.ROOT + '/ServerWCF/IMService.svc/ST_UDTO_AddSCMsgType';
	//修改服务
	var UPDATE_INFO_URL = uxutil.path.ROOT + '/ServerWCF/IMService.svc/ST_UDTO_UpdateSCMsgTypeByField';
	
	var SYSTEM_LIST = [];//系统列表
		TYPE = null,//类型，值：add/edit
		DEFAULT_DATA = {},
		AFTER_SAVE = function(){};//保存后回调函数
	
	//各个系统的消息类型枚举
	var SYS_ENUM_MAP = {};
	
	//表单reset重置渲染
	$(document).unbind('reset');//移除事件监听
	$(document).on('reset','.layui-form',function(){
		var filter = $(this).attr('lay-filter');
		setTimeout(function(){
			form.val("form",DEFAULT_DATA);
		},50);
	});
	//数据提交
	form.on('submit(submit)', function(data){
		onSubmit(data.field);
		return false; //阻止表单跳转。如果需要表单跳转，去掉这段即可。
	});
	
	function onSubmit(params){
		var System = $('#SystemID option:selected'),
			SystemID = System.val(),
			SystemCName = System.text(),
			SystemCode = System.attr("code"),
			config = {type:'post'};
			
		params.IsUse = params.IsUse == 'on' ? true : false;
		params.SystemID = SystemID;
		params.SystemCName = SystemCName;
		params.SystemCode = SystemCode;
		
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
				fields:'Id,SystemID,SystemCName,SystemCode,CName,Code,EName,Url,ShortCode,Memo,IsUse'
			});
		}
		
		uxutil.server.ajax(config,function(data){
			if(data.success){
				AFTER_SAVE(data.value);
			}else{
				if(data.msg.indexOf('IX_SC_MsgType') > -1){
					data.msg = '消息类型编码已存在！';
				}
				layer.msg(data.msg);
			}
		},true);
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
		initSystemSelect(function(){
			form.val("form",DEFAULT_DATA);
			
			if(DEFAULT_DATA.SystemID){
				changeTypeSelect(DEFAULT_DATA.SystemID,function(){
					form.val("form",DEFAULT_DATA);
				});
			}
		});
	}
	//初始化系统下拉框
	function initSystemSelect(callback){
		selectSystemList(function(){
			var list = SYSTEM_LIST,
				len = list.length,
				htmls = ['<option value="">请选择系统</option>'];
				
			for(var i=0;i<len;i++){
				htmls.push('<option value="' + list[i].Id + '" code="' + list[i].SystemCode + '">' + list[i].SystemName + '</option>');
			}
			$("#SystemID").html(htmls.join(""));
			form.render('select');
			form.on('select(SystemID)', function(data){
				var SystemID = data.value;
				changeTypeSelect(SystemID,function(){});
			});
			
			callback();
		});
	}
	//获取系统列表
	function selectSystemList(callback){
		var fields = ['Id','SystemName','SystemCode','SystemHost'],
			url = GET_SYSTEM_LIST_URL + '?where=intergratesystemset.IsUse=true';
		url += '&fields=IntergrateSystemSet_' + fields.join(',IntergrateSystemSet_');
		
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data){
				SYSTEM_LIST = (data.value || {}).list || [];
				callback();
			}else{
				layer.msg('获取系统列表失败！');
			}
		});
	}
	
	//变更消息类型下拉框
	function changeTypeSelect(SystemID,callback){
		if(SystemID){
			if(SYS_ENUM_MAP[SystemID]){
				initTypeSelect(SYS_ENUM_MAP[SystemID],function(){
					callback();
				});
			}else{
				getTypeEnum(SystemID,function(){
					initTypeSelect(SYS_ENUM_MAP[SystemID],function(){
						callback();
					});
				});
			}
		}else{
			initTypeSelect([],function(){
				callback();
			});
		}
	};
	//初始化消息类型下拉框
	function initTypeSelect(list,callback){
		var len = list.length,
			CNameHtmls = ['<option value="">请选择消息类型</option>'],
			CodeHtmls = ['<option value="">请选择消息类型</option>'];
			
		for(var i=0;i<len;i++){
			CNameHtmls.push('<option value="' + list[i].Name + '" code="' + list[i].Code + '">' + 
				list[i].Name + '【' + list[i].Code + '】' + '</option>');
			CodeHtmls.push('<option value="' + list[i].Code + '" cname="' + list[i].Name + '">' + 
				list[i].Name + '【' + list[i].Code + '】' + '</option>');
		}
		$("#CName").html(CNameHtmls.join(""));
		$("#Code").html(CodeHtmls.join(""));
		form.render('select');
		form.on('select(CName)', function(data){
			var code = $('#CName option:selected').attr("code");
			form.val("form",{Code:code});
		});
		form.on('select(Code)', function(data){
			var cname = $('#Code option:selected').attr("cname");
			form.val("form",{CName:cname});
		});
		
		callback();
	};
	//获取消息类型枚举
	function getTypeEnum(SystemID,callback){
		var SystemHost = '',
			SystemCode = '';
			
		if(!SystemID){
			callback([]);
			return;
		}
		
		for(var i in SYSTEM_LIST){
			if(SYSTEM_LIST[i].Id == SystemID){
				SystemHost = SYSTEM_LIST[i].SystemHost;
				SystemCode = SYSTEM_LIST[i].SystemCode;
				break;
			}
		}
		//如果该系统没有配置过系统主地址，则从平台读取配置
		if(!SystemHost){
			if(SYSTEM_LIST[i].SystemCode == 'ZF_LIIP'){
				SystemHost = SYSTEM_LIST[i].SystemHost;
				SystemCode = SYSTEM_LIST[i].ZF_LIIP;
			}
		}
		var url = uxutil.path.LOCAL + '/' + SystemHost + '/ServerWCF/CommonService.svc/GetClassDic';
		url +='?classname=ZFSCMsgType&classnamespace=';
			
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data.success){
				SYS_ENUM_MAP[SystemID] = data.value || [];
				callback();
			}else{
				//如果在对应的系统中没有找到消息类型枚举，则从集成平台获取
				if(data.msg.indexOf('未找到类字典') > -1 && SystemCode != 'ZF_LIIP'){
					var url = uxutil.path.ROOT + '/ServerWCF/CommonService.svc/GetClassDic';
					url +='?classname=ZFSCMsgType&classnamespace=';
					uxutil.server.ajax({
						url:url
					},function(data){
						if(data.success){
							SYS_ENUM_MAP[SystemID] = data.value || [];
							callback();
						}else{
							layer.msg('获取消息类型枚举失败！');
						}
					});
				}else{
					layer.msg('获取消息类型枚举失败！');
				}
			}
		},true);
	};
});