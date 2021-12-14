Ext.Loader.setConfig({
	enabled:true,
	paths:{'Shell':JShell.System.Path.UI}
});

Ext.onReady(function(){
	Ext.QuickTips.init();//初始化后就会激活提示功能
	Ext.Loader.setConfig({enabled: true});//允许动态加载
	Ext.Loader.setPath('Ext.main','../class');
	
	JShell.Window = Ext.create('Ext.window.Window',{
		layout:'fit',
		header:false,
		border:false,
		margin:0,
		padding:0,
		modal:true,
		plain:true,
		draggable:false,
		resizable:false,
		closeAction:'hide',
		close:function(){return JShell.Window.closeFun();}
	});
	
	//总体布局
	var viewport = Ext.create('Ext.main.View',{
		listeners:{
			contextmenu:{
				element:'el',
				fn:function(e,t,eOpts){
					//禁用浏览器的右键相应事件 
	        		e.preventDefault();e.stopEvent();
				}
			}
		}
	});
});

/**服务器信息*/
var ServerInfo = {
	Date:null
};
//系统时间
(function(){
	//从服务器上获取时间替换当前的系统时间
	var changeServerTime = function(){
		var callback = function(time){if(time){ServerInfo.Time = time;}};
		getServerTime(callback);
	};
	//一分钟一次更正当前系统时间
	window.setInterval(changeServerTime,1000*3600*24);
	//当前系统时间自动累加
	var changeSystemTime = function(){
		if(ServerInfo.Time){
			ServerInfo.Time = new Date(ServerInfo.Time.getTime() + 1000);
		}
	};
	//每秒累加当前系统时间
	window.setInterval(changeSystemTime,1000);
	
	//第一次执行
	changeServerTime();
	changeSystemTime();
})();