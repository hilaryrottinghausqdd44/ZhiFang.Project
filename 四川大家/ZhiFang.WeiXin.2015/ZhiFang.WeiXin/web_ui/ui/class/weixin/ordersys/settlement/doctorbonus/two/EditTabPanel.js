/**
 * 奖金结算二审
 * @author longfc
 * @version 2017-03-01
 */
Ext.define('Shell.class.weixin.ordersys.settlement.doctorbonus.two.EditTabPanel', {
	extend: 'Shell.class.weixin.ordersys.settlement.doctorbonus.basic.EditTabPanel',
	header: true,
	activeTab: 0,
	title: '奖金结算二审',
	/**通过按钮显示文字*/
	btnPassText: "二审通过",
	BonusGridClass:'Shell.class.weixin.ordersys.settlement.doctorbonus.two.BonusGrid',
	/**设置各页签的显示标题*/
	setTitles: function() {
		var me = this;
		me.title = "奖金结算二审";
	},
	/**通过处理方法*/
	onPassClick: function() {
		var me = this;
		me.Status="7";
		me.OperationMemo="二审通过";
		me.updateStatus();
	},
	/**@overwrite 退回按钮点击处理方法*/
	onRetractClick: function() {
		var me = this;
		me.Status="8";
		me.OperationMemo="二审退回";
		me.updateStatus();
	}
});