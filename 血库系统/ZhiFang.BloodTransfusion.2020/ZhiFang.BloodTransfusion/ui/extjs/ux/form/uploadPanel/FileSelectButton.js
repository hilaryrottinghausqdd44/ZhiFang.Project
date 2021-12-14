/**
 * @class Shell.ux.form.uploadPanel.FileSelectButton
 * @extends Ext.form.field.File
 * @description 扩展ExtJs filefield组件 支持多选
 * @author longfc
 */
Ext.define("Shell.ux.form.uploadPanel.FileSelectButton", {
	extend: 'Ext.form.field.File',
	xtype: 'fileSelectButton',
	buttonOnly: true,
	buttonText: '选择',
	buttonConfig: {
		iconCls: 'button-add'
	},
	initComponent: function() {
		var me = this;
		me.callParent(arguments);
		//me.addEvents('fileselected');
		me.on('change', me.handlerChange, me);
		me.callParent(arguments);
	},
	afterRender: function() {
		var me = this;
		//IE10及以上版本和其他浏览器
		//if(Ext.isIE == false) 
		me.fileInputEl.dom.setAttribute('multiple', '2');
		me.callParent(arguments);
	},
	handlerChange: function(file, newValue, oldValue, eOpts) {
		var me = this;
		if(!newValue) return;
		if(file.fileInputEl.dom.files) {
			//IE10及以上版本和其他浏览器
			me.fireEvent('fileselected', file, file.fileInputEl.dom.files, newValue);
		} else {
			//IE10以下版本
			me.fireEvent('fileselected', file, [file], newValue);
		}
	}
});