/**
 * 应用类代码执行过程模拟
 * 
 * boeder布局，包含
 * 一个列表
 * 一个表单
 * 
 */
Ext.define('AAA',{
	extend:'Ext.panel.Panel',
	alias: 'widget.testapp',
	getAppInfoServerUrl:getRootPath()+'/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById',
	title:'AAA',
	width:800,
	height:500,
	appInfos:[],//内部应用
	link:'',//联动关系
	comNum:0,//内部应用计数器
	/**
	 * 初始化虚拟面板完毕
	 * @private
	 */
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		//创建真实的内部应用
		me.createItems();
	},
	/**
	 * 创建真实的应用
	 * @private
	 */
	createItems:function(){
		var me = this;
		//所有内部应用信息
		var appInfos = me.getAppInfos();
		for(var i in appInfos){
			//应用ID
			var id = appInfos[i].appId;
			//回调函数
			var callback = me.getCallback(appInfos[i]);
			//从后台获取应用信息
			me.getAppInfoFromServer(id,callback);
		}
	},
	/**
	 * 生成回调函数
	 * @private
	 * @param {} appInfo
	 * @return {}
	 */
	getCallback:function(appInfo){
		var me = this;
		var callback = function(obj){
			if(obj.success && obj.appInfo != ""){
				var ModuleOperCode = obj.appInfo.BTDAppComponents_ModuleOperCode;//功能编码
				var ClassCode = obj.appInfo.BTDAppComponents_ClassCode;//类代码
				var cl = eval(ClassCode);
				
				var callback2 = function(panel){
					me.add(panel);
					//建立联动关系
					me.initLink();
				}
				appInfo.callback = callback2;
				Ext.create(cl,appInfo);
			}else{
				appInfo.html = obj.ErrorInfo;//显示错误信息
				var panel = Ext.create('Ext.panel.Panel',appInfo);
				me.add(panel);
			}
		};
		return callback;
	},
	/**
	 * 获取所有应用的信息
	 * @private
	 * @return {}
	 */
	getAppInfos:function(){
		var me = this;
		var appInfos = me.appInfos;
		for(var i in appInfos){
			if(appInfos[i].title == ''){
				 delete appInfos[i].title;
			}else if(appInfos[i].title == '_'){
				appInfos[i].title = '';
			}
		}
		return Ext.clone(appInfos);
	},
	/**
	 * 建立联动关系
	 * @private
	 */
	initLink:function(){
		var me = this;
		var appInfos = me.getAppInfos();
		var length = appInfos.length;
		me.comNum++;
		if(me.comNum == length){//内部应用全部都加载完毕
			if(me.link){eval(me.link);}
			if(Ext.typeOf(me.callback) == 'function'){me.callback(me);}
		}
	},
	/**
	 * 根据应用ID从后台获取应用信息
	 * @private
	 * @param {} id
	 * @param {} callback
	 */
	getAppInfoFromServer:function(id,callback){
		var me = this;
		var url = me.getAppInfoServerUrl + "?isPlanish=true&id=" + id;
		Ext.Ajax.defaultPostHeader = 'application/json';
		Ext.Ajax.request({
			async:false,//非异步
			url:url,
			method:'GET',
			timeout:2000,
			success:function(response,opts){
				var result = Ext.JSON.decode(response.responseText);
				if(result.success){
					var appInfo = '';
					if(result.ResultDataValue && result.ResultDataValue != ''){
						appInfo = Ext.JSON.decode(result.ResultDataValue);
					}
					if(Ext.typeOf(callback) == 'function'){
						var obj = {success:false,ErrorInfo:'没有获取到应用组件信息!'};
						if(appInfo != ''){
							obj = {success:true,appInfo:appInfo};
						}
						callback(obj);//回调函数
					}
				}else{
					if(Ext.typeOf(callback) == 'function'){
						var obj = {success:false,ErrorInfo:'获取应用组件信息失败！错误信息【<b style="color:red">'+ result.ErrorInfo +'</b>】'};
						callback(obj);//回调函数
					}
				}
			},
			failure : function(response,options){
				if(Ext.typeOf(callback) == 'function'){
					var obj = {success:false,ErrorInfo:'获取应用组件信息请求失败！'};
					callback(obj);//回调函数
				}
			}
		});
	}
});