/**
 * 医生奖金结算申请记录明细列表
 * @author longfc
 * @version 2017-03-01
 */
Ext.define('Shell.class.weixin.ordersys.settlement.doctorbonus.show.BonusGrid', {
	extend: 'Shell.class.weixin.ordersys.settlement.doctorbonus.basic.EditBonusGrid',
	title: '医生奖金结算记录',
	/**默认加载数据*/
	defaultLoad: true,
	isAllowEditing: false,
	checkOne: true,
	initComponent: function() {
		var me = this;
		me.callParent(arguments);
	}
});