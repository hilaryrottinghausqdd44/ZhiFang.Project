/**
 * 复选组组件
 * @author Jcall
 * @version 2014-08-04
 */
Ext.define('Shell.ux.form.field.CheckboxGroup',{
	extend:'Ext.form.CheckboxGroup',
	alias:'widget.uxcheckboxgroup',
	
	mixins:['Shell.ux.server.Ajax'],
	
	/**数据服务地址*/
	url:null,
	/**显示值字段*/
	displayField:'text',
	/**真实值字段*/
	valueField:'value',
	/**时间戳字段*/
	timestampField:'',
	/**默认选中的真实值*/
	defaultSelect:null,
	/**加载的数据数量*/
	dataSize:10,
	
	/**初始化面板属性*/
	initComponent:function(){
		var me = this;
		me.createItems();
		me.callParent(arguments);
	},
	/**创建内部单选数据*/
	createItems:function(){
		var me = this,
			data = me.data,
			url = me.url;
			
		if(data && data.length > 0){
			me.initThisItems(data);
		}else if(url && url != ''){
			me.initStore();
		}
	},
	/**初始化内部数据*/
	initThisItems:function(data){
		var me = this,
			length = data.length,
			da = null,
			items = [];
			
		for(var i=0;i<length;i++){
			da = data[i];
			items.push({
				name:me.itemId,
				boxLabel:da[me.displayField],
				inputValue:da[me.valueField],
				checked:da.checked || false
			});
		}
		me.items = items;
	},
	/**初始化数据集*/
	initStore:function(){
		var me = this;
			
		me.store = new Ext.data.Store({
			fields:me.getStoreFields(),
			autoLoad:true,
			pageSize:me.dataSize,
			proxy:{
				type:'ajax',
				url:Shell.util.Path.rootPath + me.url,
				reader:{type:'json',totalProperty:'count',root:'list'},
				extractResponseData:me.responseToList
			},
			listeners:{
				load:function(store,records,successful){me.onAfterLoad(records,successful);}
			}
		});
	},
	/**获取数据字段*/
	getStoreFields:function(){
		var me = this,
			displayField = me.displayField,
			valueField = me.valueField,
			timestampField = me.timestampField,
			fields = [];
			
		if(displayField && displayField != ''){fields.push(displayField);}
		if(valueField && valueField != ''){fields.push(valueField);}
		if(timestampField && timestampField != ''){fields.push(timestampField);}
		
		return fields;
	},
	/**加载数据成功后处理*/
	onAfterLoad:function(records,successful){
		var me = this,
			length = records.length,
			data = [];
			
		if(!successful) me.resetItems([]);
			
		for(var i=0;i<length;i++){
			data.push(records[i].data);
		}
		me.resetItems(data);
	},
	/**更新单选组内容*/
	resetItems:function(data){
		var me = this,
			length = data.length,
			da = null,
			items = [];
			
		for(var i=0;i<length;i++){
			da = data[i];
			items.push({
				name:me.itemId,
				boxLabel:da[me.displayField],
				inputValue:da[me.valueField],
				timestamp:da[me.timestampField]
			});
		}
		
		me.removeAll();
		me.add(items);
		//选中默认的选项
		me.initSelect();
	},
	/**选中默认的选项*/
	initSelect:function(){
		var me = this,
			defaultSelect = me.defaultSelect;
		
		if(Ext.typeOf(defaultSelect) == 'array'){
			var v = {};
			v[me.itemId] = defaultSelect;
			me.setValue(v);
		}
	},
	/**取值*/
	getValue:function(bo){
		var me = this,
			value = me.callParent(arguments);
		
		//bo参数：兼容组价内部调用该方法
		if(bo){value = value[me.itemId] || null;}
			
		return value;
	}
});