/**
	@name：modules.msg.integrator 消息集成器
	@author：Jcall
	@version 2020-12-22
 */
layui.extend({
	uxutil:'ux/util'
}).define(['uxutil'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		MOD_NAME = 'msgintegrator';
		
		
	//实时通信需要加载的基础文件
	var SIGNALR_FILES = [
		uxutil.path.ROOT + '/ui/src/jquery-1.11.1.min.js',
		uxutil.path.ROOT + '/ui/src/singalR/jquery.signalR-2.4.1.js',
		uxutil.path.ROOT + '/signalr/hubs'
	];
	
	//消息推送业务Map
	var RECEIVE_MESSAGE_MAP = {};
	
	//通信连接成功后通知Map
	var AFTER_CONNECTED_MAP = {};
	//通信连接断开后通知Map
	var AFTER_DISCONNECTED_MAP = {};
	
	//消息集成器
	var integrator = {
		config:{
			//连接丢失后重连时间间隔(秒)
			reconnectTimes:5,
			//实例化正常后触发方法
			done:function(that){},
			//实例化异常时触发方法
			error:function(msg,desc){},
		}
	};
	//声明一个instance对象,单例模式
	var instance = top.ZhiFangLabStarTechnicianStation_MsgintegratorInstance || null;
	
	//构造器
	var Class = function(setings){  
		var me = this;
		me._reconnectCount = 0;//重连次数
		me.config = $.extend({},me.config,integrator.config,setings);
	};
	
	//初始化渲染
	Class.prototype.initRender = function(callback){
		var me = this;
		//加载实时通信基础文件
		me.loadSignalrFiles(function(){
			//开始建立通信连接
			me.initConnection(function(){
				callback && callback();
			});
		});
	};
	//加载实时通信基础文件
	Class.prototype.loadSignalrFiles = function(callback,index){
		var me = this,
			index = index || 0,
			len = SIGNALR_FILES.length;
		
		if(len > 0){
			if(index >= len){
				callback();
			}else{
				var url = SIGNALR_FILES[index];
				$.getScript(url,function(){
					me.loadSignalrFiles(callback,++index);
				});
			}
		}else{
			callback();
		}
	};
	//初始化通信连接
	Class.prototype.initConnection = function(callback){
		var me = this;
		//消息接收处理
		me.onReceiveMessage();
		//开始建立连接
		me.onConnectionStart();
		//连接断开处理
		me.onConnectionDisconnected();
		
		if(callback){callback();}
	};
	//消息接收处理
	Class.prototype.onReceiveMessage = function(){
		var me = this,
			client = jQuery.connection.mainMessageHub.client;
		
		//消息推送业务
		client.ReceiveCommMessage = function(MessageInfo,MessageType,FormUserEmpId,FormUserEmpName){
			//接收到消息后触发注册的消息推送业务回调函数
			//逻辑:业务消息编码数组包含该条消息编码，则调用业务回调函数;
			for(var i in RECEIVE_MESSAGE_MAP){
				RECEIVE_MESSAGE_MAP[i].fun(MessageInfo,MessageType,FormUserEmpId,FormUserEmpName);
			}
		};
		//断开连接
		client.onSignalRStateChange = function(state){
			switch (state){
				case 'stop': integrator.stop();break;
				default:break;
			}
		};
	};
	//开始建立连接
	Class.prototype.onConnectionStart = function(){
		var me = this;
		
		jQuery.connection.hub.start().done(function(){
			me._reconnectCount = 0;
			me.afterConnected();//通信连接成功后触发
		}).fail(function(error){
			//layer.msg(error.message);
		});
	};
	//连接断开处理
	Class.prototype.onConnectionDisconnected = function(){
		var me = this;
		jQuery.connection.hub.disconnected(function(){
			me.afterDisconnected();//通信连接断开后触发
			//尝试重新建立连接
			setTimeout(function(){
				me._reconnectCount++;
				me.onConnectionStart();
			},me.config.reconnectTimes*1000);
		});
	};
	//通信连接成功后触发
	Class.prototype.afterConnected = function(){
		var me = this;
		//逻辑:按注册MAp循环下发;
		for(var i in AFTER_CONNECTED_MAP){
			AFTER_CONNECTED_MAP[i].fun(me);
		}
	};
	//通信连接断开后触发
	Class.prototype.afterDisconnected = function(){
		var me = this;
		//逻辑:按注册MAp循环下发;
		for(var i in AFTER_DISCONNECTED_MAP){
			AFTER_DISCONNECTED_MAP[i].fun(me._reconnectCount);
		}
	};
	
	/* 核心入口
	 * 示例：
	 * integrator.init({
	 *   done:function(instance){},//实例化正常后触发方法,instance:消息集成器实例
	 *   error:function(info){},//实例化异常时触发方法,info:{code:'错误信息编码',msg:'错误信息概要',desc:'错误信息详细'}
	 *   connected:{name:'名称',fun:function(instance){}},//通信连接后触发,instance:消息集成器实例
	 *   disconnected:{name:'名称',fun:function(reconnectCount){}}//通信连接断开后触发,返回尝试重连次数
	 * });
	 */
	integrator.init = function(options){
		if(!instance){
			instance = new Class(options);
			top.ZhiFangLabStarTechnicianStation_MsgintegratorInstance = instance;
			//通信连接后触发
			if(options.connected){
				integrator.registerConnected(options.connected);
			}
			//通信连接断开后触发
			if(options.disconnected){
				integrator.registerDisconnected(options.disconnected);
			}
			
			instance.initRender(function(){
				instance.config.done(instance);
			});
		}else{
			//通信连接后触发
			if(options.connected){
				integrator.registerConnected(options.connected);
			}
			//通信连接断开后触发
			if(options.disconnected){
				integrator.registerDisconnected(options.disconnected);
			}
			options.done(instance);
		}
		
		return instance;
	};
	/* 注册消息推送业务
	 * info={
	 *   "name":"Msg",//消息业务名称，唯一，统一为文件路径命名，例如：ui.layui.views.msg.card.list.html
	 *   "fun":function(data){}//接收到消息后的回调函数
	 * };
	 */
	integrator.register = function(info,callback){
		if(!instance){
			callback && callback("请初始化消息集成器！");
			return;
		}
		
		if(RECEIVE_MESSAGE_MAP[info.name]){
			window.console && console.error && console.error('消息推送业务注册提醒: name=' + info.name + '的推送业务已经被注册覆盖！');
		}
		RECEIVE_MESSAGE_MAP[info.name] = info;
		callback && callback();
	};
	/* 注册"通信连接成功"通知业务
	 * info={
	 *   "name":"Msg",//业务名称，唯一，统一为文件路径命名，例如：ui.layui.views.msg.card.list.html
	 *   "fun":function(data){}//通信连接成功后的回调函数
	 * };
	 */
	integrator.registerConnected = function(info,callback){
		if(!instance){
			callback && callback("请初始化消息集成器！");
			return;
		}
		if(!info || !info.name || typeof(info.fun) != 'function'){
			callback && callback('注册对象结构错误！，正确结构：{"name":"业务名称","fun":function(data){}}');
			return;
		}
		
		if(AFTER_CONNECTED_MAP[info.name]){
			window.console && console.error && console.error('通信连接成功注册提醒: name=' + info.name + '的通知业务已经被注册覆盖！');
		}
		AFTER_CONNECTED_MAP[info.name] = info;
		callback && callback();
	};
	/* 注册"通信断开"通知业务
	 * info={
	 *   "name":"Msg",//业务名称，唯一，统一为文件路径命名，例如：ui.layui.views.msg.card.list.html
	 *   "fun":function(data){}//通信断开后的回调函数
	 * };
	 */
	integrator.registerDisconnected = function(info,callback){
		if(!instance){
			callback && callback("请初始化消息集成器！");
			return;
		}
		if(!info || !info.name || typeof(info.fun) != 'function'){
			callback && callback('注册对象结构错误！，正确结构：{"name":"业务名称","fun":function(data){}}');
			return;
		}
		
		if(AFTER_DISCONNECTED_MAP[info.name]){
			window.console && console.error && console.error('通信断开注册提醒: name=' + info.name + '的通知业务已经被注册覆盖！');
		}
		AFTER_DISCONNECTED_MAP[info.name] = info;
		callback && callback();
	};
	//主动断开连接
	integrator.stop = function(){
		jQuery.connection.hub.stop();
	};
	
	//暴露接口
	exports(MOD_NAME,integrator);
});