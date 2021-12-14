/**
 * 客户端订单验收
 * @author longfc
 * @version 2017-12-14
 */
Ext.define('Shell.class.rea.client.confirm.reasale.DocGrid', {
	extend: 'Shell.class.rea.client.confirm.basic.DocGrid',

	title: '验货单信息列表',
	OTYPE: "reaorder",
	/**排序字段*/
	defaultOrderBy: [{
		property: 'BmsCenSaleDocConfirm_DataAddTime',
		direction: 'DESC'
	}],
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//查询框信息
		me.searchInfo = {
			width: "92%",
			emptyText: '供货单号/供货方',
			itemId: 'search',
			isLike: true,
			fields: ['bmscensaledocconfirm.SaleDocNo', 'bmscensaledocconfirm.ReaCompanyName']
		};
		//手工验收单:默认条件为验收单的供货单信息不为空
		me.defaultWhere = "bmscensaledocconfirm.BmsCenSaleDoc.Id is not null";
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
			iconCls: 'button-import',
			itemId: "btnExtract",
			text: '导入',
			tooltip: '导入平台供货单到客户端',
			hidden:true,
			handler: function() {
				me.onExtract();
			}
		}, {
			xtype: 'button',
			iconCls: 'button-add',
			itemId: "btnAdd",
			text: '供货验收',
			tooltip: '供货单验收',
			handler: function() {
				me.onAddClick();
			}
		}, {
			xtype: 'button',
			itemId: 'btnEdit',
			iconCls: 'button-edit',
			text: "继续验收",
			tooltip: "对暂存验货单继续验货",
			handler: function(btn, e) {
				me.onEditClick(btn, e);
			}
		});
		items.push({
			xtype: 'button',
			iconCls: 'button-check',
			itemId: "btnCheck",
			text: '确认验收',
			tooltip: '确认验收',
			handler: function() {
				me.onConfirmClick();
			}
		});
		items.push("-", {
			xtype: 'button',
			iconCls: 'button-accept',
			itemId: "btnStorage",
			text: '入库',
			tooltip: '入库',
			handler: function() {
				me.onStorageClick();
			}
		});
		items.push('->');
		return items;
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = me.callParent(arguments);
		columns.splice(2, 0, {
			dataIndex: 'BmsCenSaleDocConfirm_SaleDocNo',
			text: '供货单号',
			width: 80,
			defaultRenderer: true
		});
		columns.push({
			dataIndex: 'BmsCenSaleDocConfirm_BmsCenSaleDoc_Id',
			text: '供货单Id',
			hidden: true,
			hideable: false,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		});
		return columns;
	},
	onExtract: function() {
		var me = this;
		me.fireEvent('onExtract', me);
	},
	onAddClick: function() {
		var me = this;
		me.fireEvent('onAddClick', me);
	},
	onStorageClick: function() {
		var me = this;
		me.fireEvent('onStorageClick', me);
	}
});