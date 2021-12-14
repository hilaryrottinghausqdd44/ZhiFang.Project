/**
 * 手工录入验收
 * @author liangyl
 * @version 2017-12-05
 */
Ext.define('Shell.class.rea.client.stock.inspection.DocGrid', {
	extend: 'Shell.class.rea.client.stock.basic.DocGrid',

	title: '入库总单',
	OTYPE: "manualinput",
	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaBmsInDoc_DataAddTime',
		direction: 'DESC'
	}],
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [];
		me.internalWhere = me.getInternalWhere();
		return me.callParent(arguments);
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh'];
		items.push("-", {
			xtype: 'button',
			iconCls: 'button-add',
			itemId: "Add",
			text: '新增入库',
			tooltip: '新增入库',
			handler: function() {
				me.fireEvent('onAddClick', me);
//				me.showConfirmForm(null);
			}
		},'del');
		items.push('->');
		return items;
	},
	setBtnDisabled: function(com, disabled) {
		var me = this;
		var buttonsToolbar = me.getComponent("buttonsToolbar");
		if(buttonsToolbar) {
			var btn = buttonsToolbar.getComponent(com);
			if(btn) btn.setDisabled(disabled);
		}
	},
//	onAddClick: function() {
//		var me = this;
//		me.fireEvent('onAddClick', me);
//	},
	onSaveClick: function() {
		var me = this;
		me.fireEvent('onSaveClick', me);
	},
	onStorageClick: function() {
		var me = this;
		me.fireEvent('onStorageClick',me);
	},
	/**验货单入库*/
	showConfirmForm: function(id) {
		var me = this;
		var maxHeight = document.body.clientHeight - 20;
		var maxWidth = document.body.clientWidth - 20;
		var	config = {
			resizable: false,
			width:maxWidth,
			height:maxHeight,
			PK:id,
			formtype : 'add',
			closable: true, //关闭功能
			listeners: {
				save:function(p){
					me.onSearch();
				}
			}
		};
		JShell.Win.open('Shell.class.rea.client.stock.inspection.EditPanel', config).show();
	}
});