/**
 * 奖金结算编辑
 * @author longfc
 * @version 2017-03-01
 */
Ext.define('Shell.class.weixin.ordersys.settlement.doctorbonus.apply.EditTabPanel', {
	extend: 'Shell.class.weixin.ordersys.settlement.doctorbonus.basic.EditTabPanel',
	header: true,
	activeTab: 0,
	title: '奖金结算编辑',
	/**通过按钮显示文字*/
	btnPassText: "暂存",
	/**通过按钮显示文字*/
	btnRetractText: "提交",
	/**设置各页签的显示标题*/
	setTitles: function() {
		var me = this;
		me.title = "奖金结算编辑";
	},
	/**暂存处理方法*/
	onPassClick: function() {
		var me = this;
		me.Status="1";
		me.OperationMemo="暂存";
		me.updateStatus();
	},
	/**@overwrite 提交按钮点击处理方法*/
	onRetractClick: function() {
		var me = this;
		me.Status="2";
		me.OperationMemo="提交";
		me.updateStatus();
	}
});