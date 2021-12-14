/**
 * 客户端验收
 * @author longfc
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.confirm.apply.DocGrid', {
	extend: 'Shell.class.rea.client.confirm.basic.DocGrid',

	title: '验货单信息列表',

	/**排序字段*/
	defaultOrderBy: [{
		property: 'BmsCenSaleDocConfirm_DataAddTime',
		direction: 'DESC'
	}],
	/**录入:apply/审核:check*/
	OTYPE: "apply",
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},

	initComponent: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [];
		me.internalWhere = me.getInternalWhere();
		return me.callParent(arguments);;
	}
});