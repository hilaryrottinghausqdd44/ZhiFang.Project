/**
 * 报销单检查并打款
 * @author liangyl
 * @version 2015-07-27
 */
Ext.define('Shell.class.wfm.business.expenseaccount.pay.EditPanel', {
	extend: 'Shell.class.wfm.business.expenseaccount.basic.EditTabPanel',
	title: '出纳检查并打款',
	FormClassName: 'Shell.class.wfm.business.expenseaccount.basic.ContentPanel',
	formtype: 'show',
	width: 750,
	height: 420,
	/**ID*/
	PK: null,
	/**报销单检查并打款*/
	OverStatus: 11,
	/**通过文字*/
	OverName: '出纳检查并打款',
	PExpenseAccounAmount:'',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('save');
		me.callParent(arguments);
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this;
		var dockedItems = {
			xtype: 'uxButtontoolbar',
			dock: 'bottom',
			itemId: 'buttonsToolbar',
			items: ['->', {
				text: me.OverName,
				iconCls: 'button-save',
				tooltip: me.OverName,
				handler: function() {
					me.onSave(false, me.PK);
				}
			},{
				text: '关闭',
				iconCls: 'button-del',
				tooltip: '关闭',
				handler: function() {
					me.close();
				}
			}]
		};
		return dockedItems;
	},
	/**保存按钮点击处理方法*/
	onSave: function(isSubmit, id) {
		var me = this;
		JShell.Win.open('Shell.class.wfm.business.expenseaccount.pay.Form', {
			resizable: true,
			Status: me.OverStatus,
			PK: id,
			formtype: 'edit',
			SUB_WIN_NO: '11',
			PExpenseAccounAmount:me.PExpenseAccounAmount,
			listeners: {
				save: function(p, id) {
					me.fireEvent('save', me);
					p.close();
				}
			}
		}).show();
	}
});