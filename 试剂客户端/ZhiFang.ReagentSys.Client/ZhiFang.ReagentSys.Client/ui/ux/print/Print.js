/**
 * CLodop公共类-打印
 * @author Jcall
 * @version 2020-11-16
 */
Ext.define('Shell.ux.print.Print',{
	//打印控件地址
	PrintFileUrl:JShell.System.Path.ROOT + '/ui/src/zhifang/print/Print.js?v=' + JcallShell.System.JS_VERSION,
	//打印组件
	ZFPrint:null,
	//初始化组件
	init:function(callback){
		var me = this;
		if(window.ZFPrint){
			me.ZFPrint = window.ZFPrint;
			callback && callback(me.ZFPrint);
		}else{
			//加载打印基础文件
			me.loadFile(function(){
				me.initZFPrint(function(){
					me.ZFPrint = window.ZFPrint;
					callback && callback(me.ZFPrint);
				});
			});
		}
		return me;
	},
	//加载打印基础文件
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
	initZFPrint:function(callback){
		var me = this;
		
		if(window.ZFPrint){
			window.ZFPrint.init();
			//重写弹出下载CLodop文件弹框
			window.ZFPrint.openFileUploadWindow = function(type){
				var msg = this.getMsgContent(type);
				JShell.Msg.error(msg);
			};
			callback && callback();
		}else{
			setTimeout(function(){
				me.initZFPrint(callback);
			},100);
		}
	}
});