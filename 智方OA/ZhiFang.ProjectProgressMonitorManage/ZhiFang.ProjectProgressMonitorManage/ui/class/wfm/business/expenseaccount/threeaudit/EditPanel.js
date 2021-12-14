/**
 * 报销三审信息
 * @author liangyl
 * @version 2015-07-27
 */
Ext.define('Shell.class.wfm.business.expenseaccount.threeaudit.EditPanel', {
	extend: 'Shell.class.wfm.business.expenseaccount.basic.EditTabPanel',
	title: '特殊审批',
	FormClassName: 'Shell.class.wfm.business.expenseaccount.basic.ContentPanel',
//	FormClassConfig: {
//		formtype: 'edit'
//	},
	formtype: 'show',
	width: 750,
	height: 420,
	/**ID*/
	PK: null,
	/**三审通过状态*/
	OverStatus: 7,
	/**三审退回状态*/
	BackStatus: 8,
	    /**通过文字*/
	OverName:'特殊审批通过',
	/**退回文字*/
	BackName:'退回',
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
	onSave: function(isSubmit,id) {
		var me = this;
			JShell.Win.open('Shell.class.wfm.business.expenseaccount.threeaudit.Form', {
			resizable: true,
			/**二审通过状态*/
			OverStatus: me.OverStatus,
			/**二审退回状态*/
			BackStatus: me.BackStatus,
			isSubmit:isSubmit,
			PK: id,
			formtype: 'edit',
			SUB_WIN_NO: '7',
			listeners: {
				save: function(p, id) {
					me.fireEvent('save', me);
					p.close();
				}
			}
		}).show();
	}
});