/**
 * 公共方法
 * @author Jcall
 * @version 2019-12-06
 */

var JcallShell = JcallShell || {};
JcallShell.System = JcallShell.System || {};

/**服务器时间*/
JcallShell.System.Date = {
	/**每隔一段时间向服务器校准时间，单位：秒*/
	seconds:300,
	/**失败时尝试请求的次数*/
	tryTimes:10,
	/**当前的尝试次数*/
	_tryCount:0,
	_sysTime:null,
	_url:'/ServerWCF/ConstructionService.svc/CS_UDTO_GetServerInformation',
	_leftSeconds:null,
	_milliseconds:1000,
	_isError:null,
	/**服务器错误*/
	isError:function(){
		//从集成平台获取服务器时间
		if(top.layui && top.layui.system){
			return top.layui.system.date.isError();
		}
		return this._isError;
	},
	/**获取服务器时间*/
	getDate:function(){
		//从集成平台获取服务器时间
		if(top.layui && top.layui.system){
			return top.layui.system.date.getDate();
		}
		return this._sysTime;
	},
	next:function(){
		var me = this;
		me._leftSeconds--;
		
		if(me._leftSeconds == 0){
			me.init();
		}else{
			me._sysTime = new Date(me._sysTime.getTime() + me._milliseconds);
			setTimeout(function(){me.next();},me._milliseconds);
		}
	},
	init:function(callback){
		//从集成平台获取服务器时间
		if(top.layui && top.layui.system){
			return top.layui.system.date.init(callback);
		}
		
		var me = this;
		me._leftSeconds = me.seconds;
		var url = (this._url.slice(0, 4) == 'http' ? '' :
			JcallShell.System.Path.ROOT) + this._url;
			
		JcallShell.Server.get(url,function(data){
			if(data.success){
				me._isError = false;
				var d = data.value.ServerCurrentTime;
				me._sysTime = JcallShell.Date.getDate(d);
				
				setTimeout(function(){me.next();},me._milliseconds);
				if(callback){callback();}
			}else{
				if(me._tryCount < me.tryTimes){
					me._tryCount++;
					setTimeout(function(){
						JcallShell.System.Date.init(callback);
					},1000);
				}else{
					me._isError = true;
				}
			}
		});
	}
};

JcallShell.System.getPinYinZiTou = function(value,callback){
	var url = JcallShell.System.Path.ROOT + 
		'/ServerWCF/ConstructionService.svc/GetPinYin?chinese=' + value;
	
	url = JcallShell.String.encode(url,true);
	
	if(!value || value == ""){
		callback("");
		return;
	}
	var result = "";
	JShell.Server.get(url,function(text){
		var data = Ext.JSON.decode(text);
		if(data.success){
			result = data.ResultDataValue;
		}else{
			JcallShell.Msg.error(data.msg,null,500);
		}
		callback(result);
	},null,null,true);
};
/**系统类字典*/
JcallShell.System.ClassDict = {
	//获取类字典服务
	_classDicUrl:'/ServerWCF/CommonService.svc/GetClassDic',
	//获取类字典列表服务
	_classDicListUrl:'/ServerWCF/CommonService.svc/GetClassDicList',
	/** @public
	 * 初始化字典信息，支持单个字典，也支持多个字典
	 * @param {Object} classNameSpace 类域
	 * @param {Object} className 类名
	 * @param {Object} callback 回调函数
	 * @example
	 * 	JcallShell.System.ClassDict.init(
	 * 		'ZhiFang.Entity.ProjectProgressMonitorManage',
	 * 		'PContractStatus',
	 * 		function(){
	 * 			//回调函数处理
	 * 		}
	 * 	);
	 * 	JcallShell.System.ClassDict.init([
	 * 		{classnamespace:'ZhiFang.Entity.ProjectProgressMonitorManage',classname:'PContractStatus'},
	 * 		{classnamespace:'ZhiFang.Entity.ProjectProgressMonitorManage',classname:'PTaskStatus'}
	 * 	],function(){
	 * 		//回调函数处理
	 * 	});
	 */
	init:function(classNameSpace,className,callback){
		var me = this;
		var type = Ext.typeOf(classNameSpace);
		
		if(type == 'string'){
			//单个字典
			if(me[className]){
				if(Ext.typeOf(callback) == 'function'){
					callback();
				}
			}else{
				me.loadClassInfo(classNameSpace,className,callback);
			}
		}else if(type == 'array'){
			var classParamList = classNameSpace,
				callback = className,
				hasData = true;
				
			for(var i in classParamList){
				if(!me[classParamList[i].classname]){
					hasData = false;
					break;
				}
			}
			
			if(hasData){
				if(Ext.typeOf(callback) == 'function'){
					callback();
				}
			}else{
				me.loadClassInfoList(classParamList,callback);
			}
		}
	},
	/**
	 * 加载单个类字典信息
	 * @param {Object} classNameSpace 类域
	 * @param {Object} className 类名
	 * @param {Object} callback 回调函数
	 */
	loadClassInfo:function(classNameSpace,className,callback){
		var me = this;
		var url = JShell.System.Path.getRootUrl(me._classDicUrl);
		url += '?classnamespace=' + classNameSpace + '&classname=' + className;
			
		JShell.Server.get(url,function(data){
			if(data.success){
				me.initClassInfo(className,data.value);
			}else{
				me.initClassInfo(className,null);
			}
			if(Ext.typeOf(callback) == 'function'){
				callback();
			}
		});
	},
	/**
	 * 加载多个类字典信息
	 * @param {Object} classParamList 类字典参数
	 * @param {Object} callback 回调函数
	 * @example
	 * 	JcallShell.System.ClassDict.loadClassInfoList([
	 * 		{classnamespace:'ZhiFang.Entity.ProjectProgressMonitorManage',classname:'PContractStatus'},
	 * 		{classnamespace:'ZhiFang.Entity.ProjectProgressMonitorManage',classname:'PTaskStatus'}
	 * 	],function(){
	 * 		//回调函数处理
	 * 	});
	 */
	loadClassInfoList:function(classParamList,callback){
		var me = this;
		var url = JShell.System.Path.getRootUrl(me._classDicListUrl);
			
		var params = {jsonpara:classParamList};
		JShell.Server.post(url,Ext.JSON.encode(params),function(data){
			if(data.success){
				for(var i in classParamList){
					me.initClassInfo(classParamList[i].classname,data.value[i][classParamList[i].classname]);
				}
			}else{
				for(var i in classParamList){
					me.initClassInfo(classParamList[i].classname,null);
				}
			}
			if(Ext.typeOf(callback) == 'function'){
				callback();
			}
		});
	},
	initClassInfo:function(className,data){
		this[className] = data;
	},
	/** @public
	 * 根据字典内容ID获取字典内容
	 * @param {Object} className
	 * @param {Object} id
	 */
	getClassInfoById:function(className,id){
		var classInfo = this[className],
			data = null;
		
		for(var i in classInfo){
			if(classInfo[i].Id == id){
				data = classInfo[i];
				break;
			}
		}
		
		return data;
	},
	/** @public
	 * 根据字典内容Name获取字典内容
	 * @param {Object} className
	 * @param {Object} name
	 */
	getClassInfoByName:function(className,name){
		var classInfo = this[className],
			data = null;
		
		for(var i in classInfo){
			if(classInfo[i].Name == name){
				data = classInfo[i];
				break;
			}
		}
		
		return data;
	}
};

(function() {
	window.JShell = JcallShell;
	//语言包处理，默认加载中文语言包
	var params = JShell.Page.getParams(true);
	if(params.LANG){
		JcallShell.System.Lang = params.LANG;
	}
	//加载语言
	JcallShell.Page.changeLangage(JcallShell.System.Lang);
})();