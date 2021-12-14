/**
 * 【功能说明】
 * 		构建工具-数据项属性基类
 * 【提供的方法】
 * 		
 */
Ext.ns('Ext.build');
Ext.define('Ext.build.BasicSouthPanel',{
    extend:'Ext.tab.Panel',
    alias: 'widget.basicsouthpanel',
    //==========================可配参数=========================
    /**
     * 选中项的内部编号
     * @type String
     */
    activedItemId:'',
    //==========================基础方法=========================
    /**
     * 渲染完后处理
     * @private
     */
    afterRender:function(){
    	var me = this;
    	me.callParent(arguments);
    	//初始化面板监听
    	me.initListenres();
    },
    /**
     * 初始化面板
     * @private
     */
    initComponent:function(){
    	var me = this;
    	//初始化公开事件
    	me.initEvent();
    	//初始化面板属性
    	me.initParams();
    	//初始化视图
    	me.initView();
    	me.callParent(arguments);
    },
    /**
     * 初始化面板监听
     * @private
     */
    initListenres:function(){
    	var me = this;
    },
    /**
     * 初始化公开事件
     * @private
     */
    initEvent:function(){
    	var me = this;
    },
    /**
     * 初始化面板属性
     * @private
     */
    initParams:function(){
    	var me = this;
    },
    /**
     * 初始化视图
     * @private
     */
    initView:function(){
    	var me = this;
    },
    //==========================================================
    /**
     * 根据键值对获取record
     * @public
     * @param {} itemId 内部列表面板ID
     * @param {} key
     * @param {} value
     * @return {}
     */
    getRecordByKeyValue:function(itemId,key,value){
    	var me = this;
    	var grid = me.getComponent(itemId);
		var record = grid.store.findRecord(key,value);
		return record;
    },
    /**
     * 获取列表的records
     * @public
     * @param {} itemId 内部列表面板ID
     * @return {}
     */
    getRecords:function(itemId){
    	var me = this;
    	var grid = me.getComponent(itemId);
    	var store = grid.store;
		
		var records = [];
		store.each(function(record){
			records.push(record);
		});
		
		return records;
    },
    /**
     * 获取简单对象数组
     * @public
     * @param {} itemId 内部列表面板ID
     * @return {}
     */
    getRocordInfoArray:function(itemId){
    	var me = this;
    	var records = me.getRecords();
		var fields = me.getComponent(itemId).fields;
		
		var arr = [];
		//model转化成简单对象
		var getObjByRecord = function(record){
			var obj = {};
			Ext.Array.each(fields,function(field){
				obj[field.name] = record.get(field.name);
			});
			return obj;
		};
		//组装简单对象数组
		Ext.Array.each(records,function(record){
			var obj = getObjByRecord(record);
			arr.push(obj);
		});
		
		return arr;
    },
    /**
     * 给列表中的某条数据列赋值
     * @public
     * @param {} itemId 内部列表面板ID
     * @param {} pk 每条数据的唯一键值
     * @param {} key 赋值的键
     * @param {} value 赋值的值
     */
    setRecordByKeyValue:function(itemId,pk,key,value){
    	var me = this;
    	var grid = me.getComponent(itemId);
    	var record = grid.store.findRecord(grid.pk,pk);
		if(record != null){//存在
			record.set(key,value);
			record.commit();
		}
    },
    /**
     * 给列表数据集增加一条数据
     * @public
     * @param {} itemId 内部列表面板ID
     * @param {} record
     */
    setRecord:function(itemId,record){
    	var me = this;
		var grid = me.getComponent(itemId);
		grid.store.add(record);
    },
   	/**
   	 * 简单对象数组赋值
   	 * @public
   	 * @param {} itemId 内部列表面板ID
   	 * @param {} array
   	 */
    setRecordByArray:function(itemId,array){
		var me = this;
		Ext.Array.each(array,function(obj){
			var rec = ('Ext.data.Model',obj);
			me.setRecord(rec);
		});
	},
	/**
	 * 根据record移除一条记录
	 * @public
	 * @param {} itemId 内部列表面板ID
	 * @param {} record
	 */
	removeSouthValueByRecord:function(itemId,record){
		var me = this;
		var grid = me.getComponent(itemId);
		grid.store.remove(record);
	},
	/**
	 * 根据键值对移除一条记录
	 * @public
	 * @param {} itemId 内部列表面板ID
	 * @param {} key
	 * @param {} value
	 */
	removeSouthValueByKeyValue:function(itemId,key,value){
		var me = this;
		var grid = me.getComponent(itemId);
		var record = me.getRecordByKeyValue(itemId,key,value);
		if(record){
			grid.store.remove(record);
		}
	}
});