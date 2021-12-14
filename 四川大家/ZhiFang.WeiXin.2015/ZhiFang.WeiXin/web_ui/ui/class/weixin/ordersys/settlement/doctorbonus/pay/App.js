/**
 * 检查并打款
 * @author longfc
 * @version 2017-02-27
 */
Ext.define('Shell.class.weixin.ordersys.settlement.doctorbonus.pay.App', {
	extend: 'Shell.class.weixin.ordersys.settlement.doctorbonus.basic.App',
	title: '检查并打款',
	GridCalss: 'Shell.class.weixin.ordersys.settlement.doctorbonus.pay.Grid',
	BonusGridCalss: 'Shell.class.weixin.ordersys.settlement.doctorbonus.pay.BonusGrid',
	TabPanelCalss: 'Shell.class.weixin.ordersys.settlement.doctorbonus.basic.TabPanel',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},

	initComponent: function() {
		var me = this;
		me.callParent(arguments);
	}
});