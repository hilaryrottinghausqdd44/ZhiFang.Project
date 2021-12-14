/**
 * 下拉框
 * @author Jcall
 * @version 2014-08-04
 */
Ext.define('Shell.ux.form.field.ComboBox',{
	extend:'Ext.form.field.ComboBox',
	alias:['widget.uxcombobox','widget.uxcombo'],
	
	mixins:['Shell.ux.server.Ajax'],
	
	/**是否可修改*/
	editable:false,
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
	dataSize:100,
	/**每页数据数量,带分页栏*/
	pageSize:0,
	/**默认勾选第一项*/
	selectFirst:false,
	/**默认勾选最后项*/
	selectLast:false,
	/**数据集属性*/
	storeConfig: {},
    /**结果数据开头附加数据*/
	firstData: [],
    /**结果数据结尾附加数据*/
    lastData:[],
	
	/**初始化面板属性*/
	initComponent:function(){
		var me = this;
		me.addEvents('beforeload');
		if(me.pageSize > 0){me.dataSize = me.pageSize;}
		me.store = me.createStore();
		me.callParent(arguments);
	},
	/**创建数据集*/
	createStore:function(){
		var me = this,
			data = me.data,
			url = me.url,
			storeConfig = me.storeConfig,
			config = {};
			
		config.fields = me.getStoreFields();
			
		if(data && data.length > 0){
			config.data = data;
			if(Ext.typeOf(data[0]) == 'array') return new Ext.data.SimpleStore(config);
		}else if(url && url != ''){
			config.proxy = {
				type:'ajax',
				url:Shell.util.Path.rootPath + url,
				reader:{type:'json',totalProperty:'count',root:'list'},
				extractResponseData: function (response) {
				    var res = me.responseToList(response);

				    if (me.firstData.length + me.lastData.length > 0) {
                        var result = Ext.JSON.decode(res.responseText);
                    
				        result.count += me.firstData.length + me.lastData.length;

				        result.list = me.firstData.concat(result.list).concat(me.lastData);

				        res.responseText = Ext.JSON.encode(result);
				    }

				    return res;
				}
			};
			config.autoLoad = true;
			config.pageSize = me.dataSize;
			config.listeners = {
				beforeload:function(){return me.fireEvent('beforeload',me);},
				load:function(store,records,successful){
					me.onAfterLoad(records,successful);
					if(me.selectFirst || me.selectLast){
						var num = me.selectFirst ? 0 :  me.store.getCount()-1;
						var record = me.store.getAt(num);
						if(record){
							me.setValue(record);
						}else{
							me.clearValue();
						}
					}
				}
			};
		}
		
		return new Ext.data.Store(Ext.apply(config,storeConfig));
	},
	/**获取数据字段*/
	getStoreFields:function(){
		var me = this,
			displayField = me.displayField,
			valueField = me.valueField,
			timestampField = me.timestampField,
			fields = [];
			
		if(displayField){fields.push(displayField);}
		if(valueField){fields.push(valueField);}
		if(timestampField){fields.push(timestampField);}
		
		return fields;
	},
	/**加载数据成功后处理*/
	onAfterLoad:function(records,successful){
		var me = this;
			
		if(successful){
			me.initSelect();
		}
	},
	/**选中默认的选项*/
	initSelect:function(){
		var me = this,
			defaultSelect = me.defaultSelect,
			type = Ext.typeOf(defaultSelect);
		
		if(type == 'array' || type == 'string' || type == 'number'){
			me.setValue(defaultSelect);
		}
	}
});