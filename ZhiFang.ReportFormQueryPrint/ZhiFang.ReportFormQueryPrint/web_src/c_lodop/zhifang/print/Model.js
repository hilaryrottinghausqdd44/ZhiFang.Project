/**
 * CLodop打印模板插件
 * @author Jcall
 * @version 2020-12-08
 */
;!function(win){
	//模块对外公开名称
	var MOD_NAME = 'ZFPrintModel';
	//版本号
	var VERSION = '1.0.0.1';
	
	//获取本文件的文件夹地址
	function _getPathUrl(){
		var scripts = document.getElementsByTagName('script'),
			scriptSrc = scripts[scripts.length-1].src,
			fileName = scriptSrc.split('/')[scriptSrc.split('/').length-1];
		return scriptSrc.replace(fileName,'');
	}
	//jquery文件地址
	var JqueryFileUrl = _getPathUrl() + 'jquery/jquery-1.11.1.min.js';
	
	//jquery不存在，则加载
	if(!win.$){
		var head = document.head || document.getElementsByTagName("head")[0] || document.documentElement;
		var oscript = document.createElement('script');
		oscript.type = 'text/javascript';
		oscript.src = CLodop.JqueryFileUrl;
		head.insertBefore(oscript,head.firstChild);
	}
	
	//打印模板
	var ZFPrintModel = {
		//根据模板文件地址和数据获取LODOP代码内容：modelName(模板文件地址),data(替换数据),callback(回调函数)
		getLodopContentByModelFile:function(modelName,data,callback){
			var me = this;
			me._getModelByName(modelName,function(modelContent){
				var content = me._changeModelContent(modelContent,data);
				callback(content);
			});
		},
		//根据模板内容和数据获取LODOP代码内容：modelContent(模板内容),data(替换数据)
		getLodopContentByModel:function(modelContent,data){
			return this._changeModelContent(modelContent,data);
		},
		//获取模板
		_getModelByName:function(modelName,callback){
			var url = modelName + '?t=' + new Date().getTime();
			$.get(url,function(data){
				callback(data);
			});
		},
		//模板内容替换
		_changeModelContent:function(modelContent,data){
			//按数据逐一替换模板内容
			for(var i in data){
				modelContent = modelContent.replace(new RegExp('{' + i + '}','g'),data[i]);
			}
			return modelContent;
		}
	};
	
	//打印功能对外公开
	win[MOD_NAME] = ZFPrintModel;
}(window);