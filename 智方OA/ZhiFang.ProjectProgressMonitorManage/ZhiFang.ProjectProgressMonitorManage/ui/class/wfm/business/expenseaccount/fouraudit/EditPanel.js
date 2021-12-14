/**
 * 报销四审信息
 * @author liangyl
 * @version 2015-07-27
 */
Ext.define('Shell.class.wfm.business.expenseaccount.fouraudit.EditPanel', {
	extend: 'Shell.class.wfm.business.expenseaccount.basic.EditTabPanel',
	title: '财务复核',
	FormClassName: 'Shell.class.wfm.business.expenseaccount.basic.ContentPanel',
	formtype: 'show',
	width: 750,
	height: 420,
	/**ID*/
	PK: null,
	/**四审通过状态*/
	OverStatus: 9,
	/**四审退回状态*/
	BackStatus: 10,
	/**通过文字*/
	OverName: '财务复核通过',
	/**退回文字*/
	BackName: '退回',
	/**当前状态*/
	IsSpecially: false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('save');
		me.callParent(arguments);
	},
	/**保存按钮点击处理方法*/
	onSave: function(isSubmit, id) {
		var me = this;
		JShell.Win.open('Shell.class.wfm.business.expenseaccount.fouraudit.Form', {
			resizable: true,
			/**二审通过状态*/
			OverStatus: me.OverStatus,
			/**二审退回状态*/
			BackStatus: me.BackStatus,
			IsSpecially: me.IsSpecially,
			isSubmit: isSubmit,
			PK: id,
			formtype: 'edit',
			SUB_WIN_NO: '9',
			listeners: {
				save: function(p, id) {
					me.fireEvent('save', me);
					p.close();
				}
			}
		}).show();
	}
});