/**
 * 客户端订单验收
 * @author longfc
 * @version 2017-12-14
 */
Ext.define('Shell.class.rea.client.confirm.reaorder.DtlGrid', {
	extend: 'Shell.class.rea.client.confirm.basic.DtlGrid',
	
	title: '验货单明细列表',
	
	OTYPE: "reaorder",
	/**是否可编辑*/
	canEdit: false,
	
	/**用户UI配置Key*/
	userUIKey: 'confirm.reaorder.DtlGrid',
	/**用户UI配置Name*/
	userUIName: "订单验收明细列表",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建功能 按钮栏*/
	createButtontoolbar: function() {
		var me = this;
		var items=me.createFullscreenItems();
		items.push('-', 'refresh');
		var tempStatus = me.StatusList;
		items.push('-', {
			fieldLabel: '状态',
			labelWidth: 40,
			width: 125,
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			itemId: 'DtlConfirmStatus',
			data: tempStatus,
			value: "",
			listeners: {
				select: function(com, records, eOpts) {
					me.onSearch();
				}
			}
		});
		//查询框信息
		me.searchInfo = {
			width: 200,
			emptyText: '货品名称/货品批号',
			itemId: 'search',
			isLike: true,
			fields: ['reabmscensaledtlconfirm.ReaGoodsName', 'reabmscensaledtlconfirm.LotNo']
		};
		items.push('->', {
			type: 'search',
			info: me.searchInfo
		});
		
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			items: items
		});
	}
});