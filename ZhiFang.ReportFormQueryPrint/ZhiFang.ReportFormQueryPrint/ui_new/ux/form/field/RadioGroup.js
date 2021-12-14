/**
 * 单选组组件
 * @author Jcall
 * @version 2014-08-04
 */
Ext.define('Shell.ux.form.field.RadioGroup',{
	extend:'Ext.form.RadioGroup',
	alias:'widget.uxradiogroup',
	
	mixins:['Shell.ux.form.field.CheckboxGroup'],
	
	/**初始化面板属性*/
	initComponent:function(){
		var me = this;
		me.createItems();
		me.callParent(arguments);
	},
	/**初始化内部数据*/
	initThisItems:function(data){
		var me = this,
			length = data.length,
			da = null,
			checkedIndex = 0,
			items = [];
			
		for(var i=0;i<length;i++){
			da = data[i];
			if(da.checked){checkedIndex = i;}
			items.push({
				name:me.itemId,
				boxLabel:da[me.displayField],
				inputValue:da[me.valueField]
			});
		}
		items[checkedIndex].checked = true;
		me.items = items;
	},
	/**选中默认的选项*/
	initSelect:function(){
		var me = this,
			defaultSelect = me.defaultSelect;
		
		if(defaultSelect != null){
			var v = {};
			v[me.itemId] = defaultSelect;
			me.setValue(v);
			
			var value = me.getValue(true);
			if(value == null){
				var radio = me.child();
				if(radio){radio.setValue(true);}
			}
		}else{
			var radio = me.child();
			if(radio){radio.setValue(true);}
		}
	},
	/**取值*/
	getValue:function(bo){
		var me = this,
			value = me.callParent(arguments);
		
		//bo参数：兼容组价内部调用该方法
		if(bo){
			value = value[me.itemId];
			value = value == null ? null : value;
		}
			
		return value;
	}
});