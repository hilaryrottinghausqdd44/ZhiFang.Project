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
	SourceTypeValue: 3,
	OTYPE: "reasale",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('onSaleDocNo');
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this,
			items = me.callParent(arguments);
		items.push({
			fieldLabel: '供货单Id',
			hidden: true,
			name: 'ReaBmsCenSaleDocConfirm_SaleDocID',
			itemId: 'ReaBmsCenSaleDocConfirm_SaleDocID',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		});
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = me.callParent(arguments);
		if(!entity) return null;
		
		var saleDocID = values.ReaBmsCenSaleDocConfirm_SaleDocID;
		if(saleDocID) entity.entity.SaleDocID = saleDocID;
		return entity;
	}
});