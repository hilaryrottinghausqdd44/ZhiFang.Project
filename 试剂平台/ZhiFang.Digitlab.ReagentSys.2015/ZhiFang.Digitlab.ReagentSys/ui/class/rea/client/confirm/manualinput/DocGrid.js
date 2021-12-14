/**
 * 客户端手工录入验收
 * @author longfc
 * @version 2017-12-05
 */
Ext.define('Shell.class.rea.client.confirm.manualinput.DocGrid', {
	extend: 'Shell.class.rea.client.confirm.basic.DocGrid',

	title: '验货单信息列表',

	/**排序字段*/
	defaultOrderBy: [{
		property: 'BmsCenSaleDocConfirm_DataAddTime',
		direction: 'DESC'
	}],
	/**是否多选行*/
	checkOne: true,
	OTYPE: "manualinput",
	/**默认单据状态*/
	defaultStatusValue: "",
	/**获取数据服务路径*/
	confirmUrl: "/ReaSysManageService.svc/ST_UDTO_UpdateConfirmOfManualInput",
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//查询框信息
		me.searchInfo = {
			width: "92%",
			emptyText: '供货方',
			itemId: 'search',
			isLike: true,
			fields: ['bmscensaledocconfirm.ReaCompanyName']
		};
		//手工验收单:默认条件为验收单的订单信息及供货单信息都为空
		me.defaultWhere = "bmscensaledocconfirm.BmsCenSaleDoc.Id is null and bmscensaledocconfirm.BmsCenOrderDoc.Id is null";
		if(!me.checkOne) me.setCheckboxModel();
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
			itemId: "btnAdd",
			text: '新增验收',
			tooltip: '新增验收',
			handler: function() {
				me.onAddClick();
			}
		}, {
			xtype: 'button',
			itemId: 'btnEdit',
			iconCls: 'button-edit',
			text: "继续验收",
			tooltip: "对待继续验收继续验货",
			handler: function() {
				me.onContinueToAcceptClick();
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
			dataIndex: 'BmsCenSaleDocConfirm_SaleDocConfirmNo',
			text: '验收单号',
			width: 80,
			defaultRenderer: true
		});
		return columns;
	},
	/**确认验收*/
	onConfirmClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var status = records[0].get("BmsCenSaleDocConfirm_Status");
		if(status != "0") {
			var statusName = "";
			if(me.StatusEnum != null)
				statusName = me.StatusEnum[status];
			JShell.Msg.error("当前状态为【" + statusName + "】,不能确认验收!");
			return;
		}
		//验收确认弹出录入
		me.onOpenCheckForm(records[0]);
	},
	onAddClick: function() {
		var me = this;
		me.fireEvent('onAddClick', me);
	}
});