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

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('onFullScreenClick');
		me.getStatusListData();
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1
		});
		me.callParent(arguments);
	},
	/**创建功能 按钮栏*/
	createButtontoolbar: function() {
		var me = this;
		var items = [{
			text:'全屏',
			itemId:'launchFullscreen',
			iconCls:'button-arrow-out',
			handler:function(){
				me.onFullScreenClick();
			}
		}, '-', 'refresh'];
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
			emptyText: '产品名称/产品批号',
			itemId: 'search',
			isLike: true,
			fields: ['bmscensaledtlconfirm.ReaGoodsName', 'bmscensaledtlconfirm.LotNo']
		};
		items.push('->', {
			type: 'search',
			info: me.searchInfo
		});
		//items.push('-', 'save');
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			items: items
		});
	}
});