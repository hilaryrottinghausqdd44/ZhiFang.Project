Ext.define('Ext.zhifang.dynamicgrid',{
	extend:'Ext.grid.Panel',
	alias:'widget.dynamicgrid',
	//-----------------配置参数-----------------
	/**
	 * 列表获取数据服务地址
	 * @type String
	 */
	url:'',
	/**
	 * 是否有分页栏
	 * @type Boolean
	 */
	hasPagingtoolbar:false,
	/**
	 * 分页每页的数据条数
	 * @type Number
	 */
	pageSize:25,
	/**
	 * 条件串
	 * @type String
	 */
	where:'',
	//-----------------------------------------
	initComponent:function(){
		var me = this;
		//初始化视图
		me.initView();
		//创建分页栏
		if(me.hasPagingtoolbar){
			me.createPagingtoolbar();
		}
		me.callParent(arguments);
	},
	/**
	 * 初始化视图
	 * @private
	 */
	initView:function(){
		var me = this;
		me.columns = [];
		var url = me.url;
		var callback = function(obj){me.changeView(obj);};
		me.getDataFromServer(callback);
	},
	/**
	 * 创建分页栏
	 * @private
	 */
	createPagingtoolbar:function(){
		var me = this;
		me.dockedItems = me.dockedItems || [];
		me.dockedItems.push({
			xtype:'pagingtoolbar',
			itemId:'pagingtoolbar',
			store:me.store,
			dock:'bottom',
			displayInfo:true
		});
	},
	/**
	 * 更改视图
	 * @private
	 * @param {} obj
	 */
	changeView:function(obj){
		var me = this;
		var cloumns = obj.columns;
		var fields = obj.fields;
		var data = obj.data;
		me.store = Ext.create('Ext.data.Store',{
			fields:fields,
			pageSize:me.pageSize,
			proxy:{
				type:'memory',
				reader:{
					type:'json',
	            	totalProperty:'count',
	            	root:'list'
	            },
				data:data
			},autoLoad:true
		});
		var pagingtoolbar = me.getComponent('pagingtoolbar');
		pagingtoolbar.bindStore(me.store);
		me.reconfigure(me.store,cloumns);
	},
	
	/**
	 * 获取数据
	 * @private
	 * @param {} callback
	 */
	getDataFromServer:function(callback){
		var me = this;
		var url = me.url;
		url += (url.indexOf("?") != -1 ? "&" : "?") + "where=" + me.where;
		
		Ext.create('Ext.data.Store',{
			fields:[],
			proxy:{
				url:url,
				type:'ajax',
				reader:{
					type:'json',
	            	totalProperty:'data.count',
	            	root:'data.list'
	            },
				extractResponseData:function(response){
					var result = Ext.JSON.decode(response.responseText);
					if(result.success){
						callback(result);
					}else{
						Ext.Msg.alert('提示','错误信息【<b style="color:red">'+result.ErrorInfo+'</b>】');
					}
					
					return response;
				}
			},autoLoad:true
		});
	},
	/**
	 * 更新数据
	 * @public
	 */
	load:function(){
		var me = this;
		var callback = function(obj){me.changeView(obj);};
		me.getDataFromServer(callback);
	}
});