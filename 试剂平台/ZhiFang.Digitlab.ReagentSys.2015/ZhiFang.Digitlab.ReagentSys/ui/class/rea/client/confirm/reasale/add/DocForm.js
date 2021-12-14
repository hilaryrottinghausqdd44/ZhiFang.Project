/**
 * 客户端供货单验收
 * @author longfc
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.confirm.reasale.add.DocForm', {
	extend: 'Shell.class.rea.client.confirm.add.DocForm',
	requires: [
		'Shell.ux.form.field.SimpleComboBox'
	],
	title: '验货单信息',

	width: 420,
	height: 180,
	/**验货单数据来源类型*/
	SourceTypeValue:3,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this,
			items = me.callParent(arguments);
		items.push({
			fieldLabel: '供货单Id',
			hidden: true,
			name: 'BmsCenSaleDocConfirm_BmsCenSaleDoc_Id',
			itemId: 'BmsCenSaleDocConfirm_BmsCenSaleDoc_Id',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		});
		return items;
	}
});