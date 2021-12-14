/**
 * @description 订单审批
 * @author longfc
 * @version 2018-12-05
 */
Ext.define('Shell.class.rea.client.order.approval.App', {
	extend: 'Shell.class.rea.client.order.basic.App',

	title: '订单审批',
	//按钮是否可点击
	BUTTON_CAN_CLICK: true,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.onListeners();
	},
	initComponent: function() {
		var me = this;
		//自定义按钮功能栏
		me.dockedItems = me.createButtonToolbarItems();
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.OrderGrid = Ext.create('Shell.class.rea.client.order.approval.OrderGrid', {
			header: false,
			itemId: 'OrderGrid',
			region: 'west',
			width: 320,
			split: true,
			collapsible: false,
			collapsed: false,
			animCollapse: false,
			animate: false
		});
		me.EditPanel = Ext.create('Shell.class.rea.client.order.approval.EditPanel', {
			header: false,
			itemId: 'EditPanel',
			region: 'center',
			collapsible: false,
			collapsed: false
		});
		var appInfos = [me.OrderGrid, me.EditPanel];
		return appInfos;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = [];

		if (items.length > 0) items.push('-');
		items.push({
			xtype: 'button',
			iconCls: 'button-check',
			itemId: "btnCheck",
			text: '审批通过',
			tooltip: '只对当前订单审批通过',
			handler: function() {
				JcallShell.REA.RunParams.getRunParamsValue("OrderCheckIsUploaded", false, function(data) {
					if (data && data.success == false) {
						JShell.Msg.error('获取系统运行参数[订单审批通过同时直接订单上传]值失败！' + data.msg);
					} else {
						me.onCheckClick();
					}
				});
			}
		}, {
			xtype: 'button',
			iconCls: 'button-reset',
			itemId: "btnUnCheck",
			text: '审批退回',
			tooltip: '只对当前订单审批退回',
			handler: function() {
				me.onUnCheckClick();
			}
		});
		items.push("-");
		items = me.createReportClassButtonItem(items);
		items = me.createTemplate(items);
		items.push({
			xtype: 'button',
			iconCls: 'button-print',
			itemId: "btnPrint",
			text: 'PDF预览',
			tooltip: '预览PDF清单',
			handler: function() {
				me.onPrintClick();
			}
		});
		items.push('-', {
			text: '导出',
			tooltip: 'EXCEL导出',
			iconCls: 'file-excel',
			xtype: 'button',
			width: 60,
			name: 'EXCEL',
			itemId: 'EXCEL',
			handler: function() {
				me.onDownLoadExcel();
			}
		});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			border: false,
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	onListeners: function() {
		var me = this;
		me.OrderGrid.on({
			select: function(RowModel, record) {
				me.loadData(record);
			},
			nodata: function(p) {
				me.nodata();
			}
		});
		//系统运行参数"订单审批金额"
		JShell.REA.RunParams.getRunParamsValue("ReaBmsCenOrderDocApprovalTotalPrice", false, function(data) {
			var approvalTotalPrice = JcallShell.REA.RunParams.Lists.ReaBmsCenOrderDocApprovalTotalPrice.Value;
			if (!approvalTotalPrice) {
				approvalTotalPrice = "100000000";
			}
			var defaultWhere =
				'reabmscenorderdoc.Status!=0 and reabmscenorderdoc.Status!=1 and reabmscenorderdoc.Status!=2 and reabmscenorderdoc.TotalPrice>' +
				approvalTotalPrice;
			me.OrderGrid.defaultWhere = defaultWhere;
			me.OrderGrid.onSearch();
		});
	},
	clearData: function(record) {
		var me = this;
		me.setBtnDisabled("btnCheck", true);
		me.setBtnDisabled("btnUnCheck", true);
	},
	loadData: function(record) {
		var me = this;
		me.setFormType("show");
		me.setBtnDisabled("btnCheck", true);
		me.setBtnDisabled("btnUnCheck", true);
		me.EditPanel.DocForm.getComponent('buttonsToolbar').hide();
		var status = record.get("ReaBmsCenOrderDoc_Status");
		if (status == "3") {
			me.setBtnDisabled("btnCheck", false);
			me.setBtnDisabled("btnUnCheck", false);
		}else if(status=="7"){
			me.setBtnDisabled("btnUnCheck", false);
		}
		me.isShow(record);
	},
	/**@description 保存按钮点击处理方法*/
	onSaveClick: function() {
		var me = this;
		var records = me.OrderGrid.getSelectionModel().getSelection();
		if (records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}

		var totalGoodsQty = 0;
		me.EditPanel.OrderDtlGrid.store.each(function(record) {
			var goodsQty = record.get("ReaBmsCenOrderDtl_GoodsQty");
			if (!goodsQty) goodsQty = 0;
			totalGoodsQty += parseInt(goodsQty);
			if (totalGoodsQty > 0) return false;
		});
		if (totalGoodsQty == 0) {
			JShell.Msg.error("当前货品申请数量为0!不能保存!");
			return;
		}

		me.setFormType("edit");
		//需要保存主单及明细
		var params = me.EditPanel.getSaveParams();

		params.entity.Status = me.EditPanel.OrderDtlGrid.Status;
		me.onSaveOfUpdate(params, function(data) {
			if (data.success) {
				JShell.Msg.alert('订单保存成功', null, 1000);
				me.OrderGrid.onSearch();
			} else {
				JShell.Msg.error('订单保存失败！' + data.msg);
			}
		});
	},
	/**@description 审批按钮点击处理方法*/
	onCheckClick: function() {
		var me = this;
		var records = me.OrderGrid.getSelectionModel().getSelection();
		if (records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var DeleteFlag = "" + records[0].get("ReaBmsCenOrderDoc_DeleteFlag");
		if (DeleteFlag == "false" || DeleteFlag == false) DeleteFlag = '0';
		if (DeleteFlag == "1" || DeleteFlag == "true") {
			JShell.Msg.error("当前订单已被禁用!不能审批!");
			return;
		}
		if (!me.BUTTON_CAN_CLICK) return;


		//已申请的状态可以审批
		var status = records[0].get("ReaBmsCenOrderDoc_Status");
		var StatusEnum = JShell.REA.StatusList.Status[me.OrderGrid.StatusKey].Enum;
		var statusName = "";
		if (StatusEnum)
			statusName = StatusEnum[status];
		//审核通过
		if (status != "3") {
			JShell.Msg.error("当前订单状态为【" + statusName + "】,不能审批!");
			return;
		}
		if (me.EditPanel.OrderDtlGrid.store.getCount() == 0) {
			JShell.Msg.error("当前订单货品明细为空!不能审批!");
			return;
		}

		var totalGoodsQty = 0;
		var info = "";
		me.EditPanel.OrderDtlGrid.store.each(function(record) {
			var goodsQty = record.get("ReaBmsCenOrderDtl_GoodsQty");
			if (!goodsQty) goodsQty = 0;
			goodsQty = parseFloat(goodsQty);
			var price = record.get("ReaBmsCenOrderDtl_Price");
			if (!price) price = 0;
			price = parseFloat(price);
			if (goodsQty <= 0) {
				info = "货品名称为:" + record.get("ReaBmsCenOrderDtl_ReaGoodsName") + "申请数量小于等于0";
				return false;
			}
			if (price < 0) {
				info = "货品名称为:" + record.get("ReaBmsCenOrderDtl_ReaGoodsName") + "单价小于0";
				return false;
			}

			totalGoodsQty += goodsQty;
			if (totalGoodsQty > 0) return false;
		});

		if (info) {
			JShell.Msg.error(info);
			return;
		}
		if (totalGoodsQty == 0) {
			JShell.Msg.error("当前订单明细的申请数量为0!不能保存!");
			return;
		}

		if (!me.BUTTON_CAN_CLICK) return;
		//处理意见
		JShell.Msg.confirm({
			title: '<div style="text-align:center;">审批通过操作</div>',
			msg: '审批意见',
			closable: false,
			multiline: true //多行输入框
		}, function(but, text) {
			if (but != "ok") return;

			var checkMemo = text;
			if (checkMemo) {
				checkMemo = checkMemo.replace(/\\/g, '&#92');
				checkMemo = checkMemo.replace(/[\r\n]/g, '<br />');
			}
			me.setFormType("edit");
			//需要保存主单及明细
			var params = me.EditPanel.getSaveParams();
			if (!params.entity.Id) params.entity.Id = records[0].get("ReaBmsCenOrderDoc_Id");
			params.entity.Status = 12;
			params.entity.ApprovalMemo = checkMemo;

			if (!params.fields) {
				params.fields = "Id,Status";
			}
			if (params.entity.TotalPrice && params.fields.indexOf("TotalPrice") < 0) {
				params.fields += ",TotalPrice";
			}
			if (params.fields.indexOf("ApprovalMemo") < 0) params.fields += ",ApprovalMemo";
			if (!params.dtEditList) params.dtEditList = [];

			me.showMask("审核中...");
			me.BUTTON_CAN_CLICK = false; //不可点击
			me.onSaveOfUpdate(params, function(data) {
				me.hideMask();
				me.BUTTON_CAN_CLICK = true;
				if (data.success) {
					JShell.Msg.alert('确认审批成功', null, 1000);
					me.OrderGrid.onSearch();
				} else {
					JShell.Msg.error('确认审批失败！' + data.msg);
				}
			});
		});
	},
	/**@description 审批退回按钮点击处理方法*/
	onUnCheckClick: function() {
		var me = this;
		var records = me.OrderGrid.getSelectionModel().getSelection();
		if (records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		//申请
		var status = records[0].get("ReaBmsCenOrderDoc_Status");
		var StatusEnum = JShell.REA.StatusList.Status[me.OrderGrid.StatusKey].Enum;
		var statusName = "";
		if (StatusEnum)
			statusName = StatusEnum[status];
		//审核通过
		if (status != "3"&&status!="7") {
			JShell.Msg.error("当前订单状态为【" + statusName + "】,不能审批退回!");
			return;
		}

		if (me.EditPanel.OrderDtlGrid.store.getCount() == 0) {
			JShell.Msg.error("当前订单货品明细为空!不能审批退回!");
			return;
		}
		if (!me.BUTTON_CAN_CLICK) return;
		//处理意见
		JShell.Msg.confirm({
			title: '<div style="text-align:center;">审批退回操作</div>',
			msg: '审批意见',
			closable: false,
			multiline: true //多行输入框
		}, function(but, text) {
			if (but != "ok") return;
			var checkMemo = text;
			if (checkMemo) {
				checkMemo = checkMemo.replace(/\\/g, '&#92');
				checkMemo = checkMemo.replace(/[\r\n]/g, '<br />');
			}
			me.setFormType("edit");
			//需要保存主单及明细
			var params = me.EditPanel.getSaveParams();
			if (!params.entity.Id) params.entity.Id = records[0].get("ReaBmsCenOrderDoc_Id");
			params.entity.Status = 11;
			params.entity.ApprovalMemo = checkMemo;
			if (!params.fields) {
				params.fields = "Id,Status";
			}
			if (params.entity.TotalPrice && params.fields.indexOf("TotalPrice") < 0) {
				params.fields += ",TotalPrice";
			}
			if (params.fields.indexOf("ApprovalMemo") < 0) params.fields += ",ApprovalMemo";
			if (!params.dtEditList) params.dtEditList = [];
			me.showMask("审核中...");
			me.BUTTON_CAN_CLICK = false; //不可点击
			me.onSaveOfUpdate(params, function(data) {
				me.hideMask();
				me.BUTTON_CAN_CLICK = true;
				if (data.success) {
					JShell.Msg.alert('审批退回成功', null, 1000);
					me.OrderGrid.onSearch();
				} else {
					JShell.Msg.error('审批退回失败！' + data.msg);
				}
			});
		});
	}
});
