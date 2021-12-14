/**
 * @name：layui.ux.zf.system 系统类 
 * @author：Jcall
 * @version 2020-04-08
 */
layui.extend({
	uxutil:'ux/util'
}).define(['uxutil'],function(exports){
    "use strict";

	var uxutil = layui.uxutil,
		MOD_NAME = 'system';

	//外部接口
	var system = {
		//服务器时间
		date: {
			//每隔一段时间向服务器校准时间，单位：秒
			seconds: 300,
			//失败时尝试请求的次数
			tryTimes: 10,
			//当前的尝试次数
			_tryCount: 0,
			_sysTime: null,
			_url: '/ServerWCF/ConstructionService.svc/CS_UDTO_GetServerInformation',
			_leftSeconds: null,
			_milliseconds: 1000,
			_isError: null,
			//已请求同步
			_isLoading:false,
			//控制台显示,开启模式下，每一秒的时间都会显示在控制台，可以在控制台随时开关调试；
			_showConsole:false,
			
			_next: function () {
				var me = this;
				me._leftSeconds--;
				me._showConsole && window.console && console.log(me._sysTime);
				if(me._leftSeconds == 0){
					me._loadData();
				}else{
					me._sysTime = new Date(me._sysTime.getTime() + me._milliseconds);
					setTimeout(function(){me._next();},me._milliseconds);
				}
			},
			_loadData:function(callback){
				var me = this;
				me._leftSeconds = me.seconds;
				
				uxutil.server.ajax({
					url: uxutil.path.ROOT + this._url
				}, function (data) {
					if (data.success) {
						me._isError = false;
						var d = data.value.ServerCurrentTime;
						me._sysTime = new Date(d);
						uxutil.cookie.set({
							name: me._cookieKey,
							value: me._sysTime
						});
						setTimeout(function (){me._next();},me._milliseconds);
						if(callback){callback();}
					} else {
						if (me._tryCount < me.tryTimes) {
							me._tryCount++;
							setTimeout(function () {
								me.init(callback);
							}, 1000);
						} else {
							me._isError = true;
						}
					}
				});
			},
			
			//启动
			init:function(callback){
				var me = this;
				//如果window页面已经初始过服务器时间，则不再执行初始化
				if(!me.isLoading && !me._sysTime){
					me._isLoading = true;
					window.console && console.log && console.log("开始初始化服务器时间");
					me._loadData(callback);
				}else{
					window.console && console.log && console.log("已经初始化服务器时间，不再执行！");
					if(callback){callback();}
				}
			},
			//服务器错误
			isError: function () {
				return this._isError;
			},
			//获取服务器时间对象
			getDate: function () {
				return this._sysTime;
			},
			//获取服务器时间-毫秒数
			getTimes: function () {
				return this._sysTime.getTime();
			}
		}
	};
	//暴露接口
	exports(MOD_NAME,system);
});