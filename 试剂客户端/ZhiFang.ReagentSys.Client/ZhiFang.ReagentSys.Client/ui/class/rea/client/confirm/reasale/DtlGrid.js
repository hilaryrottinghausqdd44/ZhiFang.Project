/**
 * 客户端订单验收
 * @author longfc
 * @version 2017-12-14
 */
Ext.define('Shell.class.rea.client.confirm.reasale.DtlGrid', {
	extend: 'Shell.class.rea.client.confirm.basic.DtlGrid',
	
	title: '验货单明细列表',
	
	OTYPE: "reasale",
	/**是否可编辑*/
	canEdit: false,
	/**获取数据服务路径*/
	selectUrl: '/ReaManageService.svc/ST_UDTO_SearchReaBmsCenSaleDtlConfirmByHQL?isPlanish=true',
	/**用户UI配置Key*/
	userUIKey: 'confirm.reasale.DtlGrid',
	/**用户UI配置Name*/
	userUIName: "供货验收明细列表",
	
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
		items.push('refresh', '-', {
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
		//items.push('-', 'save');
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = me.callParent(arguments);
		columns.splice(6, 0,  {
			dataIndex: 'ReaBmsCenSaleDtlConfirm_GoodsSName',
			text: '简称',
			width: 90,
			defaultRenderer: true,
			doSort: function(state) {
				var field="ReaGoods_SName";
				me.store.sort({
					property: field,
					direction: state
				});
			}
		},);
		return columns;
	}
});