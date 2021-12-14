/**
 * CLodop公共类-模板
 * @author Jcall
 * @version 2021-01-04
 */
Ext.define('Shell.ux.print.Model',{
	//打印模板控件地址
	PrintFileUrl:JShell.System.Path.ROOT + '/ui/src/zhifang/print/Model.js?v=' + JcallShell.System.JS_VERSION,
	//模板组件
	ZFPrintModel:null,
	//初始化组件
	init:function(callback){
		var me = this;
		if(window.ZFPrintModel){
			me.ZFPrintModel = window.ZFPrintModel;
			callback && callback(me.ZFPrintModel);
		}else{
			//加载打印基础文件
			me.loadFile(function(){
				me.initZFPrintModel(function(){
					me.ZFPrintModel = window.ZFPrintModel;
					callback && callback(me.ZFPrintModel);
				});
			});
		}
		return me;
	},
	//加载模板基础文件
	loadFile:function(callback){
		var me = this,
			body = document.body || document.getElementsByTagName("body")[0] || document.documentElement;
			
		var oscript = document.createElement('script');
		oscript.type = 'text/javascript';
		oscript.src = me.PrintFileUrl;
		
		body.appendChild(oscript);
		
		callback && callback();
	},
	//初始化打印控件
	initZFPrintModel:function(callback){
		var me = this;
		
		if(window.ZFPrintModel){
			callback && callback();
		}else{
			setTimeout(function(){
				me.initZFPrintModel(callback);
			},100);
		}
	}
});