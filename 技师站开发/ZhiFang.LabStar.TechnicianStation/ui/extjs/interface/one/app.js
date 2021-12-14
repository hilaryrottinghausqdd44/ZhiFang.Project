Ext.Loader.setConfig({
	enabled:true,
	disableCachingParam:'v',
	//获取当前版本参数
	getDisableCachingParamValue:function(){
		return JShell.System.JS_VERSION;
	},
	paths:{
		'Shell':JShell.System.Path.UI,
		'Ext.ux':'../../../src/extjs/ux'
	}
});
Ext.onReady(function(){	
	Ext.QuickTips.init();//初始化后就会激活提示功能
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
	
	var view = Ext.create('Shell.interface.one.Viewport',{
		listeners:{
			login:function(p){
				onAfterLogin();
			}
		}
	});
	
	function onAfterLogin(){
		var params = JShell.Page.getParams(true),
			panel = null,
			ClassName = '',
			config = {
				header:false,
				border:false
			};
			
		for(var i in params){
			if(i == 'CLASSNAME'){
				ClassName = params.CLASSNAME
			}else{
				config[i] = params[i];
			}
		}
		
		if(ClassName){
			panel = Ext.create(ClassName,config);
		}else{
			panel = Ext.create('Ext.panel.Panel',{
				html:'<div style="margin:20px;text-align:center;color:red;font-weight:bold;">请传递ClassName参数</div>'
			});
		}
		view.add(panel);
		
		//JS文件加载完毕时处理
		JShell.System.afertJSLoading();
	}
});