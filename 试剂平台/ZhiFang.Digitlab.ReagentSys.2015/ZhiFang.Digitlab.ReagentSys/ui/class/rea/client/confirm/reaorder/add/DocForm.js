/**
 * 客户端供货单验收
 * @author longfc
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.confirm.reaorder.add.DocForm', {
	extend: 'Shell.class.rea.client.confirm.add.DocForm',
	requires: [
		'Shell.ux.form.field.SimpleComboBox'
	],
	title: '验货单信息',

	width: 420,
	height: 180,
	/**验货单数据来源类型*/
	SourceTypeValue: 2,
	OTYPE: "reaorder",
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
		//供货方调整
		items[0].colspan = 2;
		items[0].width = me.defaults.width * 2;
		items[0].readOnly = true;
		items[0].locked = true;

		items.splice(1, 0, {
			fieldLabel: '订货单号',
			name: 'BmsCenSaleDocConfirm_OrderDocNo',
			itemId: 'BmsCenSaleDocConfirm_OrderDocNo',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		});

		items.push({
			fieldLabel: '订单Id',
			hidden: true,
			name: 'BmsCenSaleDocConfirm_BmsCenOrderDoc_Id',
			itemId: 'BmsCenSaleDocConfirm_BmsCenOrderDoc_Id',
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
		entity.entity.OrderDocNo = values.BmsCenSaleDocConfirm_OrderDocNo;
		var orderId = values.BmsCenSaleDocConfirm_BmsCenOrderDoc_Id;
		if(orderId) {
			entity.entity.BmsCenOrderDoc = {
				Id: orderId
			};
			if(me.formtype == "add") {
				var strDataTimeStamp = "1,2,3,4,5,6,7,8";
				entity.entity.BmsCenOrderDoc.DataTimeStamp = strDataTimeStamp.split(',');
			}
		}
		return entity;
	}
});