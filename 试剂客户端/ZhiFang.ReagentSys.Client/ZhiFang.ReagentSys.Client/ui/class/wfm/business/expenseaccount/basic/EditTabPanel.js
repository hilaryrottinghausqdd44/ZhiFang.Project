/**
 * 报销信息基础页签页面
 * @author liangyl	
 * @version 2015-07-27
 */
Ext.define('Shell.class.wfm.business.expenseaccount.basic.EditTabPanel', {
	extend: 'Shell.class.wfm.business.expenseaccount.basic.ShowTabPanel',
	title: '报销信息基础页签页面',
	width: 800,
	height: 450,
	/**通过文字*/
	OverName: '报销一审',
	/**退回文字*/
	BackName: '一审退回',
	PK: null,
	/**修改服务地址*/
	editUrl: '/SingleTableService.svc/ST_UDTO_UpdatePExpenseAccountByField',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//创建挂靠功能栏
		me.dockedItems = me.createDockedItems();
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
					me.onSave(true, me.PK);
				}
			}, {
				text: me.BackName,
				iconCls: 'button-save',
				tooltip: me.BackName,
				handler: function() {
					me.onSave(false, me.PK);
				}
			}, {
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
	},
	onSaveClick: function() {
		var me = this;

	}
});