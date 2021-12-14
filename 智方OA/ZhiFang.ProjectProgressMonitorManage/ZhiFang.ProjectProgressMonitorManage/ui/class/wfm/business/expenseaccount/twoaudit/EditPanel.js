/**
 * 报销二审信息
 * @author liangyl
 * @version 2015-07-27
 */
Ext.define('Shell.class.wfm.business.expenseaccount.twoaudit.EditPanel', {
	extend: 'Shell.class.wfm.business.expenseaccount.basic.EditTabPanel',
	title: '商务核对',
	FormClassName: 'Shell.class.wfm.business.expenseaccount.basic.ContentPanel',
	width: 750,
	height: 420,
	/**ID*/
	PK: null,
	/**二审通过状态*/
	OverStatus: 5,
	/**二审退回状态*/
	BackStatus: 6,
	/**四审通过状态*/
	FourOverStatus: 9,
	/**通过文字*/
	OverName: '特殊审核',
	/**退回文字*/
	BackName: '退回',
	SUB_WIN_NO: '3',
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
	onSave: function(isSubmit, id, audit) {
		var me = this;
		JShell.Win.open('Shell.class.wfm.business.expenseaccount.twoaudit.Form', {
			resizable: true,
			/**二审通过状态*/
			OverStatus: me.OverStatus,
			/**二审退回状态*/
			BackStatus: me.BackStatus,
			IsSpecially: audit,
			isSubmit: isSubmit,
			PK: id,
			formtype: 'edit',
			SUB_WIN_NO: me.SUB_WIN_NO,
			listeners: {
				save: function(p, id) {
					me.fireEvent('save', me);
					p.close();
				}
			}
		}).show();
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this;
		var dockedItems = {
			xtype: 'uxButtontoolbar',
			dock: 'bottom',
			itemId: 'buttonsToolbar',
			items: ['->', {
				text: '商务核对通过',
				iconCls: 'button-save',
				tooltip: '商务核对通过',
				handler: function() {
					me.SUB_WIN_NO = '5';
					me.onSave(true, me.PK, '4');
				}
			},  {
				text: me.BackName,
				iconCls: 'button-save',
				tooltip: me.BackName,
				handler: function() {
					me.onSave(false, me.PK);
				}
			},{
				text: me.OverName,
				iconCls: 'button-save',
				tooltip: me.OverName,
				handler: function() {
					me.SUB_WIN_NO = '4';
					me.onSave(true, me.PK, '2');
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
	}
});