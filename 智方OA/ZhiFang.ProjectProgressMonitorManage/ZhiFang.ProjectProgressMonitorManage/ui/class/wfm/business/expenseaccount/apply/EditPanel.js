/**
 * 报销单信息
 * @author liangyl
 * @version 2015-07-27
 */
Ext.define('Shell.class.wfm.business.expenseaccount.apply.EditPanel', {
	extend: 'Shell.class.wfm.business.expenseaccount.apply.EditTabPanel',
	title: '报销单信息',
	width: 750,
	height: 350,
	FormClassName: 'Shell.class.wfm.business.expenseaccount.apply.Form',
	formtype: 'edit',
	/**ID*/
	PK: null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.Form.on({
			save: function(p, id) {
				me.fireEvent('save', me, id);
			}
		});
		me.Attachment.on({
			selectedfilerender: function(p) {
				me.Attachment.save();
			},
			save: function(p) {
				if(me.Attachment.progressMsg != "") {
					JShell.Msg.alert(me.Attachment.progressMsg);
				}
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.addEvents('save');
		//创建挂靠功能栏
		me.dockedItems = me.createDockedItems();

		me.callParent(arguments);
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = [];

		items.push('->', {
			text: '暂存',
			iconCls: 'button-save',
			tooltip: '暂存',
			handler: function() {
				me.onSave(false);
			}
		}, {
			text: '提交',
			iconCls: 'button-save',
			tooltip: '提交',
			handler: function() {
				me.onSave(true);
			}
		});
		items.push('reset', {
			text: '关闭',
			iconCls: 'button-del',
			tooltip: '关闭',
			handler: function() {
				me.close();
			}
		});
		var dockedItems = {
			xtype: 'uxButtontoolbar',
			dock: 'bottom',
			itemId: 'buttonsToolbar',
			items: items
		};
		return dockedItems;
	},
	/**保存按钮点击处理方法*/
	onSave: function(isSubmit) {
		this.Form.onSave(isSubmit);
	}
});