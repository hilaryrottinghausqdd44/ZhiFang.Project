/**
 * 医生奖金结算二审
 * @author longfc
 * @version 2017-02-27
 */
Ext.define('Shell.class.weixin.ordersys.settlement.doctorbonus.two.App', {
	extend: 'Shell.class.weixin.ordersys.settlement.doctorbonus.basic.App',
	title: '医生奖金结算二审',
	GridCalss: 'Shell.class.weixin.ordersys.settlement.doctorbonus.two.Grid',
	BonusGridCalss: 'Shell.class.weixin.ordersys.settlement.doctorbonus.two.BonusGrid',
	TabPanelCalss: 'Shell.class.weixin.ordersys.settlement.doctorbonus.basic.TabPanel',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.Grid.on({
			onPassClick: function(grid, record) {
				me.onPassClick(record);
			},
			onRetractClick: function(grid, record) {
				me.onRetractClick(record);
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**通过处理方法*/
	onPassClick: function(record) {
		var me = this;
		me.OperationMemo = "二审通过";
		var status = record.get('Status').toString();
		//一审通过,检查并打款退回
		if(status == "4" || status == "10") {			
			JShell.Msg.confirm({
				title: '<div style="text-align:left;">审核操作</div>',
				msg: '请确认是否二审通过',
				closable: true,
				multiline: false //多行输入框
			}, function(but, text) {
				if(but != "ok") return;
				me.Status = "7";
			me.updateStatus(record);
			});
		} else {
			JShell.Msg.alert("当前结算单的状态不能进行" + me.OperationMemo, null, 2000);
		}
	},
	/**@overwrite 退回按钮点击处理方法*/
	onRetractClick: function(record) {
		var me = this;
		me.OperationMemo = "二审退回";
		var status = record.get('Status').toString();
		//一审通过,检查并打款退回
		if(status == "4" || status == "10") {
			JShell.Msg.confirm({
				title: '<div style="text-align:left;">审核操作</div>',
				msg: '请确认是否二审退回',
				closable: true,
				multiline: false //多行输入框
			}, function(but, text) {
				if(but != "ok") return;
				me.Status = "8";
				me.updateStatus(record);
			});
		} else {
			JShell.Msg.alert("当前结算单的状态不能进行" + me.OperationMemo, null, 2000);
		}
	}
});