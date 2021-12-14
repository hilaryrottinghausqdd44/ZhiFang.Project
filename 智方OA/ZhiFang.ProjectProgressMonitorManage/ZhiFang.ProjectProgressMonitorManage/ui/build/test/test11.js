/**
 * 应用类代码执行过程模拟
 * 
 * boeder布局，包含
 * 一个列表
 * 一个表单
 * 
 */
Ext.ns('Ext.build');
Ext.define('Ext.build.TestApp',{
	extend:'Ext.panel.Panel',
	alias: 'widget.testapp',
	getAppInfoServerUrl:getRootPath()+'/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById',
//	title:'AAA',
//	width:800,
//	height:500,
	appInfos:[],
	initComponent:function(){
		var me = this;
		//面板布局
//		me.layout = 'border';
		//初始化布局内部每个应用的虚拟面板
		me.initPanelItems();
		
		me.callParent(arguments);
	},
	/**
	 * 初始化布局内部每个应用的虚拟面板
	 * @private
	 */
	initPanelItems:function(){
		var me = this;
		me.items = [];
		//所有内部应用信息
		var appInfos = me.getAppInfos();
		//添加初始化时的面板
		for(var i in appInfos){
			var par = appInfos[i];
			par.html = '加载中，请稍后...';
			//初始化时的面板
			var panel = Ext.create('Ext.panel.Panel',par);
			//添加内部应用
			me.items.push(panel);
		}
	},
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
				
				var panel = me.getComponent(appInfo.itemId);
				me.remove(panel);
				
				var ModuleOperCode = obj.appInfo.BTDAppComponents_ModuleOperCode;//功能编码
				var ClassCode = obj.appInfo.BTDAppComponents_ClassCode;//类代码
				var cl = eval(ClassCode);
				var newPanel = Ext.create(cl,appInfo);
				me.add(newPanel);
			}else{
				var panel = me.getComponent(appInfo.itemId);
				//显示错误信息
				panel.body.update(obj.ErrorInfo);
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
//		var appInfos = [
//			{appId:'1001',itemId:'a',region:'center',title:'-'},
//			{appId:'1002',itemId:'b',region:'east',title:'aaa',width:300}
//		];
		var me = this;
		var appInfos = me.appInfos;
		return Ext.clone(appInfos);
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
					var appInfo = "";
					if(result.ResultDataValue && result.ResultDataValue != ""){
						appInfo = Ext.JSON.decode(result.ResultDataValue);
					}
					if(Ext.typeOf(callback) == "function"){
						var obj = {success:true,appInfo:appInfo};
						callback(obj);//回调函数
					}
				}else{
					if(Ext.typeOf(callback) == "function"){
						var obj = {success:false,ErrorInfo:'获取应用组件信息失败！错误信息【<b style="color:red">'+ result.ErrorInfo +'</b>】'};
						callback(obj);//回调函数
					}
				}
			},
			failure : function(response,options){
				if(Ext.typeOf(callback) == "function"){
					var obj = {success:false,ErrorInfo:'获取应用组件信息请求失败！'};
					callback(obj);//回调函数
				}
			}
		});
	}
});