/**
 * 客户端供货单验收
 * @author longfc
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.confirm.manualinput.add.DocForm', {
	extend: 'Shell.class.rea.client.confirm.add.DocForm',
	requires: [
		'Shell.ux.form.field.SimpleComboBox'
	],
	title: '验货单信息',

	width: 420,
	height: 180,
	/**验货单数据来源类型*/
	SourceTypeValue:1,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	}
});