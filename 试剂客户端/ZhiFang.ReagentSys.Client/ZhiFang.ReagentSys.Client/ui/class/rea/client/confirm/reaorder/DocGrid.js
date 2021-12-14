/**
 * 客户端订单验收
 * @author longfc
 * @version 2017-12-14
 */
Ext.define('Shell.class.rea.client.confirm.reaorder.DocGrid', {
	extend: 'Shell.class.rea.client.confirm.basic.DocGrid',

	title: '验货单信息列表',
	
	OTYPE: "reaorder",
	/**默认单据状态*/
	defaultStatusValue: "",
	
	/**用户UI配置Key*/
	userUIKey: 'confirm.reaorder.DocGrid',
	/**用户UI配置Name*/
	userUIName: "订单验收列表",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initSearchDate(0);
	},
	initComponent: function() {
		var me = this;
		me.defaultWhere = "reabmscensaledocconfirm.SourceType=2";
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
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
		var items = ['refresh']; //'Import'
		items.push("-", {
			xtype: 'button',
			iconCls: 'button-add',
			itemId: "btnAdd",
			text: '订单验收',
			tooltip: '订单验收',
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
				me.onContinueToAcceptClick(btn, e);
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
		columns.push({
			dataIndex: 'ReaBmsCenSaleDocConfirm_OrderDocNo',
			text: '订货单号',
			width: 80,
			defaultRenderer: true
		});
		columns.push({
			dataIndex: 'ReaBmsCenSaleDocConfirm_OrderDocID',
			text: '订单Id',
			hidden: true,
			hideable: false,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		});
		return columns;
	},
	onAddClick: function() {
		var me = this;
		me.fireEvent('onAddClick', me);
	},
	/**确认验收*/
	onConfirmClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var status = records[0].get("ReaBmsCenSaleDocConfirm_Status");
		var StatusEnum = JShell.REA.StatusList.Status[me.StatusKey].Enum;
		var statusName = "";
			if(StatusEnum)
				statusName = StatusEnum[status];
		if(status != "0") {
			JShell.Msg.error("当前状态为【" + statusName + "】,不能确认验收!");
			return;
		}
		//验收确认弹出录入
		me.onOpenCheckForm(records[0]);
	}
});