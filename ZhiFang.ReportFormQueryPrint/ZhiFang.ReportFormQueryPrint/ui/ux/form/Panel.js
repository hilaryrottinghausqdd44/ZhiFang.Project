/**
 * 表单面板类
 * @author Jcall
 * @version 2014-08-04
 */
Ext.define('Shell.ux.form.Panel',{
	extend:'Ext.form.Panel',
	
	requires:[
		'Shell.ux.ButtonsToolbar',
		'Shell.ux.Img',
		
		'Shell.ux.form.field.Text',
		'Shell.ux.form.field.Date',
		'Shell.ux.form.field.Time',
		'Shell.ux.form.field.Number',
		'Shell.ux.form.field.Checkbox',
		'Shell.ux.form.field.RadioGroup',
		'Shell.ux.form.field.CheckboxGroup',
		'Shell.ux.form.field.ComboBox',
		'Shell.ux.form.field.ComboGridBox',
		'Shell.ux.form.field.DateTime',
		'Shell.ux.form.field.DateArea',
		'Shell.ux.form.field.TimeArea',
		'Shell.ux.form.field.NumberArea',
		'Shell.ux.form.field.DataTimeStamp'
	],
	mixins:[
		'Shell.ux.server.Ajax',
		'Shell.ux.PanelController'
	],
	
	/**重写渲染完毕执行*/
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		//开启右键快捷菜单设置
		me.onContextMenu();
		//确定所有取值组件
		me.initValueFields(me.items.items);
		//视图准备完毕
		me.on({boxready:function(){me.boxIsReady();}});
	},
	/**重写初始化面板属性*/
	initComponent:function(){
		var me = this;
		me._thisfields = [];
		me.addEvents('contextmenu');
		me.items = me.applyItems();
		me.callParent(arguments);
	},
	/**处理内部组件*/
	applyItems:function(){
		var me = this,
			configs = me.items || [],
			length = configs.length,
			items = [];
			
		for(var i=0;i<length;i++){
			var item = me.createThisField(configs[i]);
			if(item){items.push(item);}
		}
		
		return items;
	},
	//输入框-数字框-日期框-时间框-隐藏值-只读值(不提交)-复选框
	//-文本域-HTML编辑框-
	//文件框-单选组-复选组;
	//-图片-下拉框-下拉列表
	//日期时间框-数字区域-日期区域-时间区域-日期时间区域
	/**创建组件*/
	createThisField:function(config){
		if(!config || !config.xtype) return;
		
		if(config.itemId){config.name = config.itemId};
		
		return config;
	},
	/**赋值*/
	setValues:function(values){
		var me = this,
			fields = me._thisfields,
			length = fields.length,
			values = values || {},
			field,value;
		
		for(var i=0;i<length;i++){
			field = fields[i];
			value = values[field.name];
			if(value != null){
				field.setValue(value,true);
			}
		}
	},
	/**取值*/
	getValues:function(isValid){
		var me = this,
			fields = me._thisfields,
			length = fields.length,
			values = {},
			field;
			
		//校验数据有效性
		if(isValid){
			if(!me.isValid()) return null;
		}
			
		for(var i=0;i<length;i++){
			field = fields[i];
			values[field.name] = field.getValue(true);
		}
		
		return values;
	},
	/**确定所有取值组件*/
	initValueFields:function(items){
		var me = this,
			length = items.length || 0,
			fields = [];
			
		for(var i=0;i<length;i++){
			var item = items[i];
//			if(item.items && Ext.typeOf(item.items.items) == 'array'){
//				me.initValueFields(item.items.items);
//			}else{
//				if(Ext.typeOf(item.getValue) != 'function') continue;
//				me._thisfields.push(item);
//			}
			
			if(Ext.typeOf(item.getValue) == 'function'){
				me._thisfields.push(item);
			}else if(item.items && Ext.typeOf(item.items.items) == 'array'){
				me.initValueFields(item.items.items);
			}
		}
	},
	/**校验数据有效性*/
	isValid:function(){
		var me = this,
			fields = me._thisfields,
			length = fields.length,
			field;
			
		for(var i=0;i<length;i++){
			field = fields[i];
			if(!field.isValid()) return false;
		}
		
		return true;
	}
});