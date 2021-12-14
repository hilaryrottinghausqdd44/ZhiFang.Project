/**
	@name：modules.msg.card 消息卡片
	@author：Jcall
	@version 2020-04-13
 */
layui.extend({
	uxutil:'ux/util',
	msgintegrator:'modules/msg/integrator',
	system:'ux/zf/system',
	uxdata:'ux/data',
	uxaudio:'ux/audio'
}).define(['uxutil','msgintegrator','system','uxdata','uxaudio','layer'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		msgintegrator = layui.msgintegrator,
		system = layui.system,
		uxdata = layui.uxdata,
		uxaudio = layui.uxaudio,
		MOD_NAME = 'msgcard';
	
	//delphi外壳API
	var JS_DELPHI_API = window.JS_DELPHI_BOM;
	
	//初始化声音组件
	var thatUxaudio = uxaudio.render();
	
	//触发拖动的元素
	var MOVE_CLASS = "mine-move";
	//元素闪烁样式名
	var FLICKER_CLASS_NAME = "flicker";
	//小卡片样式名
	var CARD_CALSS_NAME = 'card_div';
	//样式数组
	var STYLE_LIST = [
		//元素闪烁样式
		'@keyframes fade{from{opacity: 1.0;}50%{opacity:0.4;}to{opacity:1.0;}}',
		'@-webkit-keyframes fade{from{opacity:1.0;}50%{opacity:0.4;}to{opacity:1.0;}}',
		'.' + FLICKER_CLASS_NAME + '{animation:fade 600ms infinite;-webkit-animation:fade 600ms infinite;}',
		//小卡片样式
		'.' + CARD_CALSS_NAME + '{float:left;font-size:12px;border:1px solid #ffffff;padding:9px 1px;text-align:center;box-sizing:border-box;}',
		//右侧菜单面板,动画 从右往左滑入
		'.menu-right{top:0;bottom:0;box-shadow:1px 1px 10px rgba(0,0,0,.1);border-radius:0;overflow:auto;}',
		'@-webkit-keyframes layui-rl{from{-webkit-transform:translate3d(100%,0,0);}to{-webkit-transform:translate3d(0,0,0);}}',
		'@keyframes layui-rl{from{transform:translate3d(100%,0,0);}to{transform:translate3d(0,0,0);}}',
		'.layui-anim-rl{-webkit-animation-name:layui-rl;animation-name:layui-rl;}'
	];
	
	//卡片模板
	var _CARD_DIV_TEMPLET = [
		'<div id="{cardId}" style="width:{width};height:{height}" class="' + MOVE_CLASS + '">',
			'<div id="{cardId}_content" style="display:none;">',
				'<div style="float:left;width:100%;">',
					//待确认
					'<div id="{cardId}_content_ToConfirm" class="' + CARD_CALSS_NAME + '" status="{ToConfirm_status}" title="双击查看详细信息" ',
					'style="background-color:{ToConfirm_bgcolor};color:{ToConfirm_color};width:{ToConfirm_width};height:{ToConfirm_height};">',
						'<div id="{cardId}_content_ToConfirm_text" class="' + FLICKER_CLASS_NAME + '" style="cursor:pointer;">待确认</div>',
						'<div id="{cardId}_content_ToConfirm_count" class="' + FLICKER_CLASS_NAME + '" style="cursor:pointer;">0</div>',
					'</div>',
					//待处理
					'<div id="{cardId}_content_ToHandle" class="' + CARD_CALSS_NAME + '" status="{ToHandle_status}" title="双击查看详细信息" ',
					'style="background-color:{ToHandle_bgcolor};color:{ToHandle_color};width:{ToHandle_width};height:{ToHandle_height};">',
						'<div id="{cardId}_content_ToHandle_text" class="' + FLICKER_CLASS_NAME + '" style="cursor:pointer;">待处理</div>',
						'<div id="{cardId}_content_ToHandle_count" class="' + FLICKER_CLASS_NAME + '" style="cursor:pointer;">0</div>',
					'</div>',
				'</div>',
				'<div style="float:left;width:100%;">',
					//超时未确认
					'<div id="{cardId}_content_ConfirmOutTime" class="' + CARD_CALSS_NAME + '" status="{ConfirmOutTime_status}" title="双击查看详细信息" ',
					'style="background-color:{ConfirmOutTime_bgcolor};color:{ConfirmOutTime_color};width:{ConfirmOutTime_width};height:{ConfirmOutTime_height};">',
						'<div id="{cardId}_content_ConfirmOutTime_text" class="' + FLICKER_CLASS_NAME + '" style="cursor:pointer;">超时未确认</div>',
						'<div id="{cardId}_content_ConfirmOutTime_count" class="' + FLICKER_CLASS_NAME + '" style="cursor:pointer;">0</div>',
					'</div>',
				'</div>',
			'</div>',
			'<div id="{cardId}_info" style="text-align:center;padding:15px;">启动中...</div>',
		'</div>'
	];
	//成功信息模板
	var _SUCCESS_TEMPLET = [
		'<div title="{title}" style="color:green;">{msg}</div>'
	];
	//错误信息模板
	var _ERROR_TEMPLET = [
		'<div title="{title}" style="color:red;">{msg}</div>'
	];
	//菜单项模板
	var _MENU_TEMPLET = [
		'<div id="{id}" style="cursor:pointer;font-size:12px;padding:5px;border-bottom:1px dashed #e0e0e0;">{text}</div>'
	];
	
	//获取危急值员工部门列表
	var GET_EMP_DEPT_URL = uxutil.path.ROOT + "/ServerWCF/IMService.svc/ST_UDTO_SearchCVCriticalValueEmpIdDeptLinkByHQL";
	//获取消息列表
	var GET_MSG_LIST_URL = uxutil.path.ROOT + "/ServerWCF/IMService.svc/ST_UDTO_SearchSCMsgByHQL";
	//获取消息类型列表
	var GET_MSG_TYPE_LIST_URL = uxutil.path.ROOT + "/ServerWCF/IMService.svc/ST_UDTO_SearchSCMsgTypeByHQL";
	//获取系统列表
	var GET_SYSTEM_LIST_URL = uxutil.path.ROOT + "/ServerWCF/LIIPService.svc/ST_UDTO_SearchIntergrateSystemSetByHQL";
	//登录服务
	var ON_LOGIN_URL = uxutil.path.ROOT + "/ServerWCF/RBACService.svc/CV_AddDoctorOrNurse";
	//获取系统参数列表服务地址
	var GET_PARAMS_LIST_URL = uxutil.path.ROOT + "/ServerWCF/SingleTableService.svc/ST_UDTO_SearchBParameterByHQL";
	//获取员工角色列表
	var GET_EMP_ROLE_LIST_URL = uxutil.path.ROOT + "/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACEmpRolesByHQL";
	//显示消息列表地址
	var SHOW_MSG_LIST_URL = uxutil.path.ROOT + "/ui/layui/views/msg/card/list.html";
	
	//消息相关参数编码数组
	var PARAMS_CODES = [
		'Msg_Open','Msg_Audio','Msg_AudioTimes','Msg_ForcedBombScreen',
		'Msg_ForcedBombScreenTimes','ZF_LIIP_Msg_Type_Codes','ZF_LIIP_Msg_Default_Min'
	];
	//获取的消息字段数组
	var MSG_FIELDS = ["Id","MsgContent","MsgTypeCode","SenderID","SenderName","ConfirmFlag","HandleFlag","RequireConfirmTime"];
	
	//消息卡片
	var msgcard = {
		//可设置参数
		config:{
			//是否是layer弹出方式
			isLayer:false,
			//超时扫描间隔时间(秒)
			outTimes:3,
			//整个卡片宽度
			width:100,
			//整个卡片高度
			height:100,
			//浏览器直接弹出
			isWindowOpen:false,
			//菜单数组
			menu:[]
		},
		//layer弹出方式参数
		layerConfig:{
			//类型
			type:1,
			//标题
			title:false,
			//遮罩
			shade:0,
			//关闭按钮
			closeBtn:0,
			//是否允许拉伸
			resize:false,
			//默认位置
			offset:'rt',
			//宽度高度
			area:['100px','100px'],
			//触发拖动的元素
			move:"." + MOVE_CLASS
		}
	};
	
	//构造器
	var Class = function(setings){  
		var me = this;
		
		me.config = $.extend({},me.config,msgcard.config,setings);
		
		//自动生成卡片ID
		me.cardId = 'msgcard_' + new Date().getTime();
		//消息集成器实例
		me.msgintegrator = null;
		
		//实例内部数据
		me._data = {
			//消息类型编码
			msgTypeCodeList:[],
			//消息类型列表
			msgTypeList:[],
			//消息相关参数列表
			paramsList:[],
			//消息相关参数Map
			paramsMap:{},
			//系统列表
			systemList:[],
			//危急值部门列表
			deptIds:[],
			//医生身份
			IS_DOCTOR:false,
			//护士身份
			IS_NURSE:false,
			//技师身份
			IS_TECH:false,
			//消息类型
			MSG_TYPE_MAP:{
				ToConfirm:{
					text:"待确认",color:"#ffffff",bgcolor:"#1E9FFF",
					data:new uxdata.Map(),status:'2',
					width:me.config.width/2 + 'px',
					height:me.config.height/2 + 'px',
					dom:null,textDom:null,countDom:null
				},
				ToHandle:{
					text:"待处理",color:"#ffffff",bgcolor:"#009688",
					data:new uxdata.Map(),status:'3',
					width:me.config.width/2 + 'px',
					height:me.config.height/2 + 'px',
					dom:null,textDom:null,countDom:null
				},
				ConfirmOutTime:{
					text:"超时未确认",color:"#ffffff",bgcolor:"#FF5722",
					data:new uxdata.Map(),status:'1',
					width:me.config.width + 'px',
					height:me.config.height/2 + 'px',
					dom:null,textDom:null,countDom:null
				}
			},
			//layer方式打开时记录layer的id
			layerIndex:null
		};
		//定时器MAP,用于实例销毁时清理实例内部所有正在执行的定时器
		me._setTimeout_map = {
			//超时监听
			onTimeoutListeners:null,
			//声音提醒监听
			onMsgAudioListeners:null,
			//强制弹屏监听
			onMsgForcedBombScreenListeners:null
		};
	};
	
	//初始化卡片HTML
	Class.prototype.initCardHtml = function(){
		var me = this;
		
		var style = '<style>' + STYLE_LIST.join('') + '</style>';
		var html = _CARD_DIV_TEMPLET.join('').replace(/{cardId}/g,me.cardId);
		
		html = html.replace(/{width}/g,me.config.width+'px').replace(/{height}/g,me.config.height+'px');
		//按分类划分小块
		var typeMap = me._data.MSG_TYPE_MAP;
		for(var i in typeMap){
			html = html.replace(new RegExp('{' + i + '_bgcolor}','g'),typeMap[i].bgcolor);
			html = html.replace(new RegExp('{' + i + '_color}','g'),typeMap[i].color);
			html = html.replace(new RegExp('{' + i + '_width}','g'),typeMap[i].width);
			html = html.replace(new RegExp('{' + i + '_height}','g'),typeMap[i].height);
			html = html.replace(new RegExp('{' + i + '_status}','g'),typeMap[i].status);
		}
		if(me.config.isLayer){
			me._data.layerIndex = layer.open($.extend({},msgcard.layerConfig,{
				area:[me.config.width+'px',me.config.height+'px'],
				content:style + html
			}));
		}else{
			$("body").append(style + html);
		}
		
		//保存每个小块的dom元素，减少开销，提升性能
		for(var i in typeMap){
			typeMap[i].dom = $("#" + me.cardId + "_content_" + i);
			typeMap[i].textDom = $("#" + me.cardId + "_content_" + i + "_text");
			typeMap[i].countDom = $("#" + me.cardId + "_content_" + i + "_count");
			
			//小块监听
			(function(_dom){
				_dom.on("dblclick",function(){
					var status = $(this).attr("status");
					me.onOpenInfoWin(status);
				}).on('mousedown', function(e){
					if(e.button == 0 && e.originalEvent.detail >= 2){//左键+双击
						_dom.dblclick();
					}
				});
			})(typeMap[i].dom);
			
		}
		
		var menuList = me.config.menu;
		if(menuList.length > 0){
			var html = [];
			for(var i in menuList){
				var id = me.cardId + '_menu_' + menuList[i].code;
				html.push(_MENU_TEMPLET.join('').replace(/{id}/g,id).replace(/{text}/g,menuList[i].text));
			}
			//右键出现菜单
			$("#" + me.cardId).on("contextmenu",function(e){
				me.MenuLayer = layer.open({
					type:1,
					anim:-1,
					title:false,
					closeBtn:false,
					offset:'r',
					shade:0.1,
					shadeClose:true,
					skin:'layui-anim layui-anim-rl menu-right',
					area:(me.config.width-40)+'px',
					content:html.join(''),
					end:function(){
						me.MenuLayer = false;
					}
				});
				
				for(var i in menuList){
					var dom = $("#" + me.cardId + '_menu_' + menuList[i].code);
					dom.on("click",function(){
						me.onMenuClick($(this).attr("id"));
					});
				}
				
				return false;//冒泡事件和默认事件都阻止
			});
		}
	};
	//菜单点击处理
	Class.prototype.onMenuClick = function(menuId){
		var me = this,
			menuList = me.config.menu,
			menuInfo = {};
		
		for(var i in menuList){
			if((me.cardId + '_menu_' + menuList[i].code) == menuId){
				menuInfo = menuList[i];
				break;
			}
		}
		
		if(menuInfo.openType == "location.href"){
			location.href = menuInfo.url;
		}else if(menuInfo.openType == "window.open"){
			window.open(menuInfo.url);
		}
	};
	
	//显示该类型消息具体内容
	Class.prototype.onOpenInfoWin = function(status){
		var me = this;
		
		var url = SHOW_MSG_LIST_URL + '?status=' + status + '&typeCodes=' + me._data.msgTypeCodeList.join(',') + '&t=' + new Date().getTime();

		//CS外壳打开方式或者浏览器弹出模式
		if(JS_DELPHI_API || me.config.isWindowOpen){
			var width = 1600,
				height = 600,
				maxWidth = screen.availWidth,
				maxHeight = screen.availHeight;
				
			if(width > maxWidth){width = maxWidth;}
			if(height > maxHeight){height = maxHeight;}
			
			var left = (maxWidth - width)/2;
			var top = (maxHeight - height)/2;
			//防止重复打开
			if(!me._openedWin || me._openedWin.closed){
				me._openedWin = window.open(url,'msg_card_list','scrollbars=yes,resizable=yes,toolbar=no' + 
					',left=' + left + ',top=' + top + ',width=' + width + ',height=' + height);
			}
		}else{
			//防止重复打开
			me._openedWin = me._openedWin || (window.top.layer ? window.top.layer.open : layer.open)({
				title:'消息列表',
				type:2,
				content:url,
				maxmin:true,
				toolbar:true,
				resize:true,
				area:['95%','95%'],
				end:function(){
					me._openedWin = null;
				}
			});
		}
	};
	
	//初始化消息集成器
	Class.prototype.initMsgintegrator = function(){
		var me = this;
		//启用消息集成器
		msgintegrator.init({
			//实例化正常后触发方法,instance:消息集成器实例
			done:function(instance){
				me.msgintegrator = instance;
				window.console && console.log && console.log("modules.msg.card：消息集成器已准备就绪");
				//初始化消息卡片内容
				me.initCardContent();
			},
			//实例化异常时触发方法,info:{code:'错误信息编码',msg:'错误信息概要',desc:'错误信息详细'}
			error:function(info){
				me.showError(info.msg,info.desc);
			},
			//通信连接后触发,instance:消息集成器实例
			connected:{
				name:me.cardId,
				fun:function(instance){
					window.console && console.log && console.log("modules.msg.card：通信连接成功");
					//初始化消息卡片内容
					me.initCardContent();
				}
			},
			//通信连接断开后触发,返回尝试重连次数
			disconnected:{
				name:me.cardId,
				fun:function(reconnectCount){
					var msg = '通信中断,重连' + (reconnectCount > 0 ? reconnectCount + '次' : '中...')
					me.showError(msg,msg);
				}
			}
		});
	};
	
	//显示成功信息
	Class.prototype.showSuccess = function(msg,title){
		var me = this;
		var html = _SUCCESS_TEMPLET.join('').replace(/{cardId}/g,me.cardId).replace(/{msg}/g,(msg || '')).replace(/{title}/g,(title || ''));
		me.showInfo(html);
	};
	//显示错误信息
	Class.prototype.showError = function(msg,title){
		var me = this;
		var html = _ERROR_TEMPLET.join('').replace(/{cardId}/g,me.cardId).replace(/{msg}/g,(msg || '')).replace(/{title}/g,(title || ''));
		me.showInfo(html);
	};
	//显示信息
	Class.prototype.showInfo = function(html){
		var me = this;
		var contentDiv = $("#" + me.cardId + "_content");
		var infoDiv = $("#" + me.cardId + "_info");
			
		contentDiv.hide();
		infoDiv.html(html);
		infoDiv.show();
	};
	
	//初始化卡片内容
	Class.prototype.initCardContent = function(){
		var me = this;
		uxutil.action.delay(function(){
			me.initCardContentLogic();
		},100);
	};
	//初始化加载逻辑
	Class.prototype.initCardContentLogic = function(){
		var me = this;
		
		//加载消息相关参数
		me.onLoadParamsList(function(){
			//获取员工角色列表
			me.onLoadEmpRoleList(function(){
				//加载系统列表
				me.onLoadSystemList(function(){
					//加载消息类型
					me.onLoadMsgTypeList(function(){
						//获取危急值员工部门列表
						me.onLoadDeptIds(function(){
							//注册系统设置通知
							me.onRegisterSystemParameter(function(){
								//清空数据
								me.clearData();
								//清空定时器
								me.clearTimeout();
								//注册消息推送业务
								me.onRegisterMsg(function(){
									//获取未读的消息列表
									me.onLoadUnShowMsgs(function(){
										//显示卡片内容
										me.onShowCardContent();
										//初始化监听
										me.initListeners();
									});
								});
							});
						});
					});
				});
			});
		});
	};
	
	//注册消息推送业务
	Class.prototype.onRegisterMsg = function(callback){
		var me = this;
		
		//注册危急值消息业务
		msgintegrator.register({
			"name":MOD_NAME + '_' + me.cardId + '_msg',
			"codes":me._data.msgTypeCodeList,
			fun:function(FormUserEmpId,FormUserEmpName,Message,SCMsgId,SCMsgTypeCode,ZFSCMsgStatus){
				var where = "scmsg.Id=" + SCMsgId;
				//加载消息信息
				me.onLoadMsgs(where,function(list){
					me.insertData(list[0]);
				});
			}
		});
		
		callback && callback();
	};
	//注册系统设置通知
	Class.prototype.onRegisterSystemParameter = function(callback){
		var me = this;
		
		//注册危急值消息业务
		msgintegrator.registerSystemParameter({
			"name":me.cardId + '_system',
			"codes":PARAMS_CODES,
			fun:function(ParameterCode,ParameterJson){
				var value = ParameterJson || '';
				
				if(value.toLocaleUpperCase() == 'TRUE'){
					value = true;
				}else if(value.toLocaleUpperCase() == 'FALSE'){
					value = false;
				}
					
				me._data.paramsMap[ParameterCode] = value;
				
				me.onSystemParameterChange(ParameterCode);
			}
		});
		
		callback && callback();
	};
	//系统设置变化处理
	Class.prototype.onSystemParameterChange = function(ParameterCode){
		var me = this;
		
		switch (ParameterCode){
			//最小化变换处理
			case 'ZF_LIIP_Msg_Default_Min':me.onChangeMsgMin();break;
			//消息开启变换处理
			case 'Msg_Open':me.onChangeMsgOpen();break;
			default:break;
		}
	};
	//最小化变换处理
	Class.prototype.onChangeMsgMin = function(){
		var me = this;
		//CS外壳打开方式
		if(JS_DELPHI_API){
			if(me._data.paramsMap.ZF_LIIP_Msg_Default_Min){
				JS_DELPHI_API.onSizeMin();
			}else{
				JS_DELPHI_API.onShowOriginalFrame();
			}
		}
	};
	//消息开启变换处理
	Class.prototype.onChangeMsgOpen = function(){
		var me = this;
		//消息运行参数是否已开启
		if(me._data.paramsMap.Msg_Open){
			me.initCardContent();//初始化卡片内容
		}else{
			me.showError('消息运行参数未开启','请在“消息运行参数设置”功能中打开消息开关');
		}
	};
	
	//系统参数列表
	Class.prototype.onLoadParamsList = function(callback){
		var me = this,
			paraNoStr = '';
			
		if(PARAMS_CODES && PARAMS_CODES.length > 0){
			paraNoStr = "'" + PARAMS_CODES.join("','") + "'";
		}
		
		uxutil.server.ajax({
			url:GET_PARAMS_LIST_URL,
			data:{
				fields:['BParameter_Id','BParameter_ParaNo','BParameter_ParaValue'].join(','),
				where:"bparameter.ParaNo in(" + paraNoStr + ")"
			}
		},function(data){
			if(data.success){
				var list = (data.value || {}).list || [];
				me._data.paramsList = list;
				
				for(var i in list){
					var ParaValue = list[i].ParaValue || '';
					if(ParaValue.toLocaleUpperCase() == 'TRUE'){
						ParaValue = true;
					}else if(ParaValue.toLocaleUpperCase() == 'FALSE'){
						ParaValue = false;
					}
					me._data.paramsMap[list[i].ParaNo] = ParaValue;
					
					//展现的消息类型编码
					if(list[i].ParaNo == 'ZF_LIIP_Msg_Type_Codes'){
						me._data.msgTypeCodeList = list[i].ParaValue.split(',');
					}
				}
				//消息运行参数是否已开启
				if(me._data.paramsMap.Msg_Open){
					callback();
				}else{
					me.showError('消息运行参数未开启','请在“消息运行参数设置”功能中打开消息开关');
				}
			}else{
				me.showError("系统参数获取失败",data.msg);
			}
		});
	};
	//获取员工角色列表
	Class.prototype.onLoadEmpRoleList = function(callback){
		var me = this;
		
		uxutil.server.ajax({
			url:GET_EMP_ROLE_LIST_URL,
			data:{
				isPlanish:true,
				fields:"RBACEmpRoles_RBACRole_Id",
				where:"rbacemproles.IsUse=1 and rbacemproles.HREmployee.Id=" + uxutil.cookie.get(uxutil.cookie.map.USERID)
			}
		},function(data){
			if(data.success){
				var list = data.value.list || [];
				
				for(var i in list){
					switch(list[i].RBACEmpRoles_RBACRole_Id){
						case '1001':me._data.IS_TECH = true;break;
						case '2001':me._data.IS_NURSE = true;break;
						case '3001':me._data.IS_DOCTOR = true;break;
						default:break;
					}
				}
				callback();
			}else{
				me.showError("获取员工角色失败",data.msg);
				
			}
		});
	};
	//获取系统列表
	Class.prototype.onLoadSystemList = function(callback){
		var me = this;
		
		uxutil.server.ajax({
			url:GET_SYSTEM_LIST_URL,
			data:{
				fields:'IntergrateSystemSet_Id,IntergrateSystemSet_SystemName,IntergrateSystemSet_SystemCode,IntergrateSystemSet_SystemHost'
			}
		},function(data){
			if(data.success){
				me._data.systemList = data.value.list || [];
				callback();
			}else{
				me.showError("获取系统列表失败",data.msg);
			}
		});
	};
	//获取消息类型
	Class.prototype.onLoadMsgTypeList = function(callback){
		var me = this,
			msgTypeCodeList = me._data.msgTypeCodeList,
			codeWhere = [];
			
		for(var i in msgTypeCodeList){
			codeWhere.push("scmsgtype.Code='" + msgTypeCodeList[i] + "'");
		}
		if(codeWhere.length == 0){
			codeWhere.push('1=0');
		}
			
		uxutil.server.ajax({
			url:GET_MSG_TYPE_LIST_URL,
			data:{
				fields:'SCMsgType_Id,SCMsgType_CName,SCMsgType_Code,SCMsgType_Url,SCMsgType_SystemID',
				where:codeWhere.join(' or ')
			}
		},function(data){
			if(data.success){
				me._data.msgTypeList = data.value.list || [];
				callback();
			}else{
				me.showError("获取消息类型失败",data.msg);
			}
		});
	};
	//获取危急值员工部门列表
	Class.prototype.onLoadDeptIds = function(callback){
		var me = this,
			empId = uxutil.cookie.get(uxutil.cookie.map.USERID),
			deptIds = [];
			
		uxutil.server.ajax({
			url:GET_EMP_DEPT_URL,
			data:{
				fields:"CVCriticalValueEmpIdDeptLink_DeptID",
				where:"cvcriticalvalueempiddeptlink.IsUse=1 and cvcriticalvalueempiddeptlink.EmpID=" + empId
			}
		},function(data){
			if(data.success){
				var list = data.value.list || []
				for(var i in list){
					deptIds.push(list[i].DeptID);
				}
				me._data.deptIds = deptIds;
				callback();
			}else{
				me.showError("获取危急值员工部门关系失败",data.msg)
			}
		});
	};
	//获取未读的消息列表
	Class.prototype.onLoadUnShowMsgs = function(callback){
		var me = this,
			empId = uxutil.cookie.get(uxutil.cookie.map.USERID),
			deptIds = me._data.deptIds,
			msgTypeList = me._data.msgTypeList,
			len = msgTypeList.length,
			typeWhere = [];
			
		for(var i=0;i<len;i++){
			typeWhere.push("scmsg.MsgTypeCode='" + msgTypeList[i].Code + "'");
		}
		if(typeWhere.length == 0){
			typeWhere = ['1=2'];
		}
			
		//消息过滤条件
		var where = [];
		
		//过滤条件=使用+未处理
		where.push("scmsg.IsUse=1 and scmsg.HandleFlag='0'");
		//过滤条件：消息类型
		if(typeWhere.length > 0){
			where.push("(" + typeWhere.join(" or ") + ")");
		}
		
		//条件：医生+有权限的处理部门 or 护士+有权限的确认部门 or 技师+发送者是本人
		var userWhere = [];
		//过滤条件：医生+有权限的处理部门
		if(me._data.IS_DOCTOR){
			userWhere.push("scmsg.RecDeptID in (" + deptIds.join(',') + ")");
		}
		//过滤条件：护士+有权限的确认部门
		if(me._data.IS_NURSE){
			var nurseWhere = [];
			//确认人和处理人区分字段
			nurseWhere.push("scmsg.ConfirmDeptID in (" + deptIds.join(',') + ")");
			//确认人和处理人不区分字段
			nurseWhere.push("(scmsg.ConfirmDeptID is null and scmsg.RecDeptID in (" + deptIds.join(',') + "))");
			userWhere.push("(" + nurseWhere.join(" or ") + ")");
		}
		//过滤条件：技师=本人
		if(me._data.IS_TECH){
			//userWhere.push("scmsg.SenderID=" + empId);
			userWhere.push("scmsg.SendDeptID in (" + deptIds.join(',') + ")");
		}
		
		if(userWhere.length > 0){
			where.push("(" + userWhere.join(" or ") + ")");
		}
		//获取消息列表
		me.onLoadMsgs(where.join(" and "),function(list){
			for(var i in list){
				me.insertData(list[i]);
			}
			callback();
		});
	};
	//获取消息列表
	Class.prototype.onLoadMsgs = function(where,callback){
		var me = this;
			
		uxutil.server.ajax({
			url:GET_MSG_LIST_URL,
			data:{
				fields:"SCMsg_" + MSG_FIELDS.join(",SCMsg_"),
				where:where
			}
		},function(data){
			if(data.success){
				callback(data.value.list || []);
			}else{
				me.showError("获取消息失败",data.msg);
			}
		});
	};
	
	//添加消息到内存
	Class.prototype.insertData = function(info){
		var me = this,
			ToConfirmData = me._data.MSG_TYPE_MAP.ToConfirm.data,
			ToHandleData = me._data.MSG_TYPE_MAP.ToHandle.data,
			ConfirmOutTimeData = me._data.MSG_TYPE_MAP.ConfirmOutTime.data,
			serverDateTimes = uxutil.server.date.getTimes(),
			times = new Date(info.RequireConfirmTime).getTime();
		
		if(info.HandleFlag == "1"){//已处理
			//待确认删除
			ToConfirmData.del(info.Id);
			//待处理删除
			ToHandleData.del(info.Id);
			//待确认超时删除
			ConfirmOutTimeData.del(info.Id);
		}else{
			if(info.ConfirmFlag == "1"){//已确认
				//待确认删除
				ToConfirmData.del(info.Id);
				//待处理增加
				ToHandleData.set(info.Id,info);
				//待确认超时删除
				ConfirmOutTimeData.del(info.Id);
			}else{//未确认
				//未确认的消息做超时判断
				if(!info.RequireConfirmTime){//没有要求确认时间的不做超时判断
					//待处理增加
					ToConfirmData.set(info.Id,info);
					//待处理删除
					ToHandleData.del(info.Id);
					//待确认超时删除
					ConfirmOutTimeData.del(info.Id);
				}else if(times > serverDateTimes){//要求确认时间 > 当前服务器时间
					//待确认增加
					ToConfirmData.set(info.Id,info);
					//待处理删除
					ToHandleData.del(info.Id);
					//待确认超时删除
					ConfirmOutTimeData.del(info.Id);
				}else{
					//待确认删除
					ToConfirmData.del(info.Id,info);
					//待处理删除
					ToHandleData.del(info.Id);
					//待确认超时增加
					ConfirmOutTimeData.set(info.Id,info);
				}
			}
		}
		
		//卡片内容变化
		me.changeCardInfo();
	};
	//卡片内容变化
	Class.prototype.changeCardInfo = function(){
		var me = this;
		
		uxutil.action.delay(function(){
			//卡片HTML内容变化
			me.changeCardHtml();
		},100);
	};
	//卡片HTML内容变化
	Class.prototype.changeCardHtml = function(){
		var me = this;
		var typeMap = me._data.MSG_TYPE_MAP;
		
		for(var i in typeMap){
			typeMap[i].countDom.html(typeMap[i].data.size);
			if(typeMap[i].data.size == 0){
				typeMap[i].textDom.removeClass(FLICKER_CLASS_NAME);
				typeMap[i].countDom.removeClass(FLICKER_CLASS_NAME);
			}else{
				typeMap[i].textDom.addClass(FLICKER_CLASS_NAME);
				typeMap[i].countDom.addClass(FLICKER_CLASS_NAME);
			}
		}
	};
	
	//清空数据
	Class.prototype.clearData = function(){
		var me = this;
		me._data.MSG_TYPE_MAP.ToConfirm.data.clear();
		me._data.MSG_TYPE_MAP.ToHandle.data.clear();
		me._data.MSG_TYPE_MAP.ConfirmOutTime.data.clear();
	};
	
	//显示卡片内容
	Class.prototype.onShowCardContent = function(){
		var me = this;
		var contentDiv = $("#" + me.cardId + "_content");
		var infoDiv = $("#" + me.cardId + "_info");
		
		infoDiv.hide();
		contentDiv.show();
	};
	//初始化监听
	Class.prototype.initListeners = function(){
		var me = this;
		//数据变化监听
		me.onDataChangeListeners();
		//超时监听
		me.onTimeoutListeners();
		//声音提醒监听
		me.onMsgAudioListeners();
		//强制弹屏监听
		me.onMsgForcedBombScreenListeners();
		//默认最小化监听
		me.onMsgDefaultMinListeners();
	};
	//数据变化监听
	Class.prototype.onDataChangeListeners = function(){
		var me = this,
			ToConfirmData = me._data.MSG_TYPE_MAP.ToConfirm.data,
			ToHandleData = me._data.MSG_TYPE_MAP.ToHandle.data,
			ToConfirmOutTimeData = me._data.MSG_TYPE_MAP.ConfirmOutTime.data;
			
		//map：字典对象；
		//eventName：动作名称；set/delete/clear
		//value：value/key值；set(Value添加的数据)，delete(key删除的主键)
		//hasKey：是否存在主键
		
		//待确认数据变化监听
		ToConfirmData.listeners.change = function(map,eventName,value,hasKey){
			if(eventName == 'set'){//添加数据
				me.onToConfirmAudioPlay(value.MsgTypeCode);
			}else if(eventName == 'del'){//删除数据
				me.changeCardInfo();
			}else if(eventName == 'clear'){//清空数据
				me.changeCardInfo();
			}
		};
		//待处理数据变化监听
		ToHandleData.listeners.change = function(map,eventName,value,hasKey){
			if(eventName == 'set'){//添加数据
				me.onToHandleAudioPlay(value.MsgTypeCode);
			}else if(eventName == 'del'){//删除数据
				me.changeCardInfo();
			}else if(eventName == 'clear'){//清空数据
				me.changeCardInfo();
			}
		};
		//待确认超时数据变化监听
		ToConfirmOutTimeData.listeners.change = function(map,eventName,value,hasKey){
			if(eventName == 'set'){//添加数据
				me.onTimeoutAudioPlay(value.MsgTypeCode);
			}else if(eventName == 'del'){//删除数据
				me.changeCardInfo();
			}else if(eventName == 'clear'){//清空数据
				me.changeCardInfo();
			}
		};
	};
	//超时监听
	Class.prototype.onTimeoutListeners = function(){
		var me = this,
			ToConfirmData = me._data.MSG_TYPE_MAP.ToConfirm.data,
			ConfirmOutTimeData = me._data.MSG_TYPE_MAP.ConfirmOutTime.data,
			list = ToConfirmData.values(),
			serverDateTimes = uxutil.server.date.getTimes();
			
		me._TEST_MSG_LIST = me._TEST_MSG_LIST || [];
		for(var i in list){
			//已确认||已处理||没有要求确认时间的不进行超时处理
			if(list[i].ConfirmFlag == '1' || list[i].HandleFlag == '1' || !list[i].RequireConfirmTime) continue;
			
			var times = new Date(list[i].RequireConfirmTime).getTime();
			if(times < serverDateTimes){
				//超时增加
				ConfirmOutTimeData.set(list[i].Id,list[i]);
				//待处理删除
				ToConfirmData.del(list[i].Id);
			}
		}
		//记录定时器，用于销毁
		me._setTimeout_map.onTimeoutListeners = setTimeout(function(){
			me.onTimeoutListeners();
		},me.config.outTimes*1000);
	};
	//声音提醒监听
	Class.prototype.onMsgAudioListeners = function(){
		var me = this;
		//声音提醒时间间隔
		if(me._data.paramsMap.Msg_AudioTimes){
			var times = parseInt(me._data.paramsMap.Msg_AudioTimes);
			
			if(times > 0){
				//记录定时器，用于销毁
				me._setTimeout_map.onMsgAudioListeners = setTimeout(function(){
					me.onAudioPlay();
					me.onMsgAudioListeners();
				},times*1000);
			}
		}
	};
	//强制弹屏监听
	Class.prototype.onMsgForcedBombScreenListeners = function(){
		var me = this;
		//开启强制弹屏参数时，进入判断处理
		if(me._data.paramsMap.Msg_ForcedBombScreenTimes){
			var times = parseInt(me._data.paramsMap.Msg_ForcedBombScreenTimes);
			if(times > 0){
				//记录定时器，用于销毁
				me._setTimeout_map.onMsgForcedBombScreenListeners = setTimeout(function(){
					me.onMsgForcedBombScreen();
					me.onMsgForcedBombScreenListeners();
				},times*1000);
			}
		}
	};
	//强制弹屏
	Class.prototype.onMsgForcedBombScreen = function(){
		var me = this,
			ToConfirmCount = me._data.MSG_TYPE_MAP.ToConfirm.data.size,
			ToHandleCount = me._data.MSG_TYPE_MAP.ToHandle.data.size,
			ToConfirmOutTimeCount = me._data.MSG_TYPE_MAP.ConfirmOutTime.data.size;
			
		//强制弹屏参数开启，存在未确认/未处理/超时的都强制弹屏
		if(me._data.paramsMap.Msg_ForcedBombScreen && (ToConfirmCount + ToHandleCount + ToConfirmOutTimeCount) > 0){
			if(me._data.IS_DOCTOR){//医生
				me.onOpenInfoWin(5);
			}else if(me._data.IS_NURSE){//护士
				if(ToConfirmCount > 0 || ToConfirmOutTimeCount > 0){
					me.onOpenInfoWin(5);
				}
			}else if(me._data.IS_TECH){//技师
				if(ToConfirmOutTimeCount > 0){
					me.onOpenInfoWin(1);
				}
			}
		}
	};
	//默认最小化监听
	Class.prototype.onMsgDefaultMinListeners = function(){
		this.onChangeMsgMin();
	};
	//消息待确认播报信息
	Class.prototype.onToConfirmAudioPlay = function(){
		this.onAudioPlay();
	};
	//消息待处理播报信息
	Class.prototype.onToHandleAudioPlay = function(){
		this.onAudioPlay();
	};
	//消息超时播报信息
	Class.prototype.onTimeoutAudioPlay = function(){
		this.onAudioPlay();
	};
	//消息声音播报信息
	Class.prototype.onAudioPlay = function(){
		var me = this,
			ToConfirmCount = me._data.MSG_TYPE_MAP.ToConfirm.data.size,
			ToHandleCount = me._data.MSG_TYPE_MAP.ToHandle.data.size,
			ToConfirmOutTimeCount = me._data.MSG_TYPE_MAP.ConfirmOutTime.data.size;
			
		//声音提醒参数开启
		if(me._data.paramsMap.Msg_Audio && (ToConfirmCount + ToHandleCount + ToConfirmOutTimeCount) > 0){
			uxutil.action.delay(function(){
				var text = [];
				//医生/护士：待确认提示
				if(ToConfirmCount > 0 && (me._data.IS_NURSE || me._data.IS_DOCTOR)){
					text.push('您有' + ToConfirmCount + '个消息待确认');
				}
				//医生：待处理提示
				if(ToHandleCount > 0 && me._data.IS_DOCTOR){
					text.push('您有' + ToHandleCount + '个消息待处理');
				}
				//医生/护士/技师：超时提示
				if(ToConfirmOutTimeCount > 0 && (me._data.IS_NURSE || me._data.IS_DOCTOR || me._data.IS_TECH)){
					text.push('您有' + ToConfirmOutTimeCount + '个消息已超时');
				}
				if(text.length > 0){
					thatUxaudio.playone(text.join(','));
				}
			},200);
		}
	};
	//清空定时器
	Class.prototype.clearTimeout = function(){
		var me = this;
		//销毁正在运行的定时器
		for(var i in me._setTimeout_map){
			if(me._setTimeout_map[i]){
				clearTimeout(me._setTimeout_map[i]);
			}
		}
	};
	//关闭方法
	Class.prototype.close = function(){
		var me = this;
		if(me.config.isLayer){
			layer.close(me._data.layerIndex);
			//清空定时器
			me.clearTimeout();
			me = null;
		}
	};
	
	//核心入口
	msgcard.render = function(options){
		var me = new Class(options);
		
		//开启服务器时间本地化
		system.date.init(function(){
			//初始化卡片HTML
			me.initCardHtml();
			//初始化消息集成器
			me.initMsgintegrator();
		});
		
		return me;
	};
	
	//暴露接口
	exports(MOD_NAME,msgcard);
});