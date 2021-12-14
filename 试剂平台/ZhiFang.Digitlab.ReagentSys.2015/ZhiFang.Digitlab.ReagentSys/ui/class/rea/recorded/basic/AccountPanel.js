/**
 * 入账信息
 * @author liangyl
 * @version 2017-06-02
 */
Ext.define('Shell.class.rea.recorded.basic.AccountPanel', {
	extend: 'Ext.panel.Panel',
	title: '入账单信息',

	layout: 'border',
	bodyPadding: 1,
	/** 新增入帐总单及入帐明细,同时更新供货单的是否入帐标志 */
	addUrl: '/ReagentSysService.svc/ST_UDTO_AddBmsAccountInputAndDtList',
	/**机构Id*/
	CENORG_ID: null,
	/**机构名称*/
	CENORG_NAME: null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var Grid = me.getComponent('Grid');

		Grid.store.on({
			datachanged: function(store, eOpts) {
				var Form = me.getComponent('Form');
				//总金额
				var TotalPrice = Form.getComponent('BmsAccountInput_TotalPrice');
				TotalPrice.setValue(Grid.getTotalPrice());
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this,
			items = [];

		items.push(Ext.create('Shell.class.rea.recorded.basic.accountsaledoc.Grid', {
			region: 'center',
			itemId: 'Grid',
			title: '已选的供货单',
			header: true,
			hasPagingtoolbar: false
		}));
		items.push(Ext.create('Shell.class.rea.recorded.basic.Form', {
			region: 'south',
			height: 145,
			split: false,
			collapsible: true,
			itemId: 'Form',
			CENORG_ID: me.CENORG_ID,
			CENORG_NAME: me.CENORG_NAME,
			header: false
		}));

		return items;
	},
	createDockedItems: function() {
		var me = this,
			items = ['->'];
		items.push({
			iconCls: 'button-save',
			text: '保存',
			tooltip: '保存',
			handler: function() {
				me.onSave();
			}
		});
		var dockedItems = [{
			xtype: 'toolbar',
			dock: 'bottom',
			items: items
		}];

		return dockedItems;
	},
	onSave: function() {
		var me = this,
			url = JShell.System.Path.ROOT + me.addUrl;
		var Form = me.getComponent('Form');
		var Grid = me.getComponent('Grid');
		//验证表单
		if(!Form.getForm().isValid()) {
			Form.fireEvent('isValid', me);
			return;
		}
		var entity = Form.getAddParams();
		entity.saleDocIDStr = Grid.getBmsCenSaleDocIdS();
		//提交数据到后台
		JShell.Server.post(url, Ext.JSON.encode(entity), function(data) {
			if(data.success) {
				me.fireEvent('save', me);
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	}
});