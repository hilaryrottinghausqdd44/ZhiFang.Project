/**
 * 简单下拉框
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.ux.form.field.SimpleComboBox', {
	extend: 'Ext.form.field.ComboBox',
	alias: 'widget.uxSimpleComboBox',

	mode: 'local',
	editable: false,
	displayField: 'text',
	valueField: 'value',
	hasStyle: false,
	lastQuery:'',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.onChangeContent(me);
	},

	initComponent: function() {
		var me = this,
			data = Ext.clone(me.data) || [],
			len = data.length;

		if (me.hasStyle) {
			for (var i = 0; i < len; i++) {
				if (data[i].length >= 3 && data[i][2]) {
					data[i][1] = '<div style="' + data[i][2] + '">' + data[i][1] + '</div>';
				}
			}
		}

		me.store = Ext.create('Ext.data.ArrayStore', {
			fields: ['value', 'text', 'style'],
			data: data
		});

		me.callParent(arguments);
	},
	/**改变内容*/
	onChangeContent: function(field) {
		var inputEl = field.inputEl;
		
		if(!inputEl) return;
		
		var v = field.inputEl.dom.value;

		var arrValue = v.match(/(\>.*?\<)/g); //提取文字
		if (arrValue && arrValue.length > 0) {
			field.inputEl.dom.value = arrValue[0].slice(1, -1);
		}

		var arrStyle = v.match(/(style=\".*?\")/g); //提取颜色
		var style = '';
		if (arrValue && arrValue.length > 0) {
			style = arrStyle[0].slice(7, -1);
		}

		var arr = style.split(";");
		for (var i in arr) {
			var a = arr[i].split(":");
			if (arr[i]) field.inputEl.dom.style[a[0]] = a[1];
		}
	},
	/**获取选中的值*/
	getValue:function(){
		var me = this;
		if(me.editable){
			return me.callParent(arguments);
		}else{
			return me.value;
		}
	},
	/**重写赋值方法*/
	setValue:function(value, doSelect){
		var me = this;
		var field = me.callParent(arguments);
		if (me.hasStyle) {
			me.onChangeContent(me);
		}
		
		return field;
	},
	/**重写动态赋值方法*/
	loadData:function(data){
		var me = this,
			data = Ext.clone(data) || [],
			len = data.length;
		
		if (me.hasStyle) {
			for (var i = 0; i < len; i++) {
				if (data[i].length >= 3 && data[i][2]) {
					data[i][1] = '<div style="' + data[i][2] + '">' + data[i][1] + '</div>';
				}
			}
		}
		
		me.store.loadData(data);
		me.onChangeContent(me);
	}
});