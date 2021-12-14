/**
 * CLodop公共类
 * @author Jcall
 * @version 2020-11-16
 */
Ext.define('Shell.class.lodop.Print',{
	//打印控件地址
	PrintFileUrl:JShell.System.Path.ROOT + '/ui/src/zhifang/print/Print.js?t=' + new Date().getTime(),
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
			head = document.head || document.getElementsByTagName("head")[0] || document.documentElement;
			
		var oscript = document.createElement('script');
		oscript.type = 'text/javascript';
		oscript.src = me.PrintFileUrl;
		
		head.insertBefore(oscript,head.firstChild);
		
		callback && callback();
	},
	//初始化打印控件
	initZFPrint:function(callback){
		var me = this;
		
		if(window.ZFPrint){
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