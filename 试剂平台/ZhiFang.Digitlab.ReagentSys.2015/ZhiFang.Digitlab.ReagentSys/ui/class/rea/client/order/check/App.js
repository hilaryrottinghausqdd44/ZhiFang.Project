/**
 * @description 订单审核
 * @author longfc
 * @version 2017-11-17
 */
Ext.define('Shell.class.rea.client.order.check.App', {
	extend: 'Shell.class.rea.client.order.basic.App',

	title: '订单审核',

	/**录入:entry/审核:check*/
	OTYPE: "check",
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.OrderGrid.on({
			select: function(RowModel, record) {
				me.loadData(record);
//				JShell.Action.delay(function() {					
//				}, null, 500);
			},
			nodata: function(p) {
				me.nodata();
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.OTYPE = me.OTYPE || "check";
		//自定义按钮功能栏
		me.dockedItems = me.createButtonToolbarItems();
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.OrderGrid = Ext.create('Shell.class.rea.client.order.check.OrderGrid', {
			header: false,
			itemId: 'OrderGrid',
			region: 'west',
			width: 415,
			split: true,
			OTYPE: me.OTYPE,
			collapsible: false,
			collapsed: false
		});
		me.EditPanel = Ext.create('Shell.class.rea.client.order.check.EditPanel', {
			header: false,
			itemId: 'EditPanel',
			region: 'center',
			collapsible: false,
			collapsed: false,
			OTYPE: me.OTYPE
		});
		var appInfos = [me.OrderGrid, me.EditPanel];
		return appInfos;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = [];

		if(items.length > 0) items.push('-');
		items.push({
			xtype: 'button',
			iconCls: 'button-check',
			itemId: "btnCheck",
			text: '审核通过',
			tooltip: '只对当前订单审核通过',
			handler: function() {
				me.onCheckClick();
			}
		}, {
			xtype: 'button',
			iconCls: 'button-reset',
			itemId: "btnUnCheck",
			text: '审核退回',
			tooltip: '只对当前订单审核退回',
			handler: function() {
				me.onUnCheckClick();
			}
		});
		items.push('-', {
			xtype: 'button',
			iconCls: 'button-save',
			itemId: "btnSave",
			text: '保存订单',
			tooltip: '编辑当前选中的订单',
			disabled: true,
			handler: function() {
				me.onSaveClick();
			}
		});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			border: false,
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	clearData: function(record) {
		var me = this;
		me.setBtnDisabled("btnCheck", true);
		me.setBtnDisabled("btnUnCheck", true);
		me.setBtnDisabled("btnSave", true);
	},
	loadData: function(record) {
		var me = this;
		//已申请
		var status = record.get("BmsCenOrderDoc_Status");
		if(status == "1") {
			me.isEdit(record);
		} else {
			me.isShow(record);
		}
	},
	isEdit: function(record) {
		var me = this;
		me.callParent(arguments);
		me.setFormType("edit");
		me.setBtnDisabled("btnSave", false);
		me.setBtnDisabled("btnCheck", true);
		me.setBtnDisabled("btnUnCheck", true);
		var status = record.get("BmsCenOrderDoc_Status");
		switch(status) {
			case "1": //已申请
				me.setBtnDisabled("btnCheck", false);
				me.setBtnDisabled("btnUnCheck", false);
				break;
			default:
				//明细的新增,删除,保存默认禁用
				me.EditPanel.OrderDtlGrid.buttonsDisabled = true;
				me.EditPanel.OrderDtlGrid.setButtonsDisabled(true);
				break;
		}
	},
	isShow: function(record) {
		var me = this;
		me.callParent(arguments);
		me.setFormType("show");
		me.setBtnDisabled("btnCheck", true);
		me.setBtnDisabled("btnUnCheck", true);
		me.setBtnDisabled("btnSave", true);
		var status = record.get("BmsCenOrderDoc_Status");
		switch(status) {
			case "1": //已申请
				me.setBtnDisabled("btnCheck", false);
				me.setBtnDisabled("btnUnCheck", false);
				break;
			default:
				//明细的新增,删除,保存默认禁用
				me.EditPanel.OrderDtlGrid.buttonsDisabled = true;
				me.EditPanel.OrderDtlGrid.setButtonsDisabled(true);
				break;
		}
	},
	/**@description 保存按钮点击处理方法*/
	onSaveClick: function() {
		var me = this;
		var records = me.OrderGrid.getSelectionModel().getSelection();
		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}

		var totalGoodsQty = 0;
		me.EditPanel.OrderDtlGrid.store.each(function(record) {
			var goodsQty = record.get("BmsCenOrderDtl_GoodsQty");
			if(!goodsQty) goodsQty = 0;
			totalGoodsQty += parseInt(goodsQty);
			if(totalGoodsQty > 0) return false;
		});
		if(totalGoodsQty == 0) {
			JShell.Msg.error("当前货品申请数量为0!不能保存!");
			return;
		}

		me.setFormType("edit");
		//需要保存主单及明细
		var params = me.EditPanel.getSaveParams();

		params.entity.Status = me.EditPanel.OrderDtlGrid.Status;
		me.onSaveOfUpdate(params, function(data) {
			if(data.success) {
				JShell.Msg.alert('订单保存成功', null, 1000);
				me.OrderGrid.onSearch();
			} else {
				JShell.Msg.error('订单保存失败！' + data.msg);
			}
		});
	},
	/**@description 审核按钮点击处理方法*/
	onCheckClick: function() {
		var me = this;
		var records = me.OrderGrid.getSelectionModel().getSelection();
		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var DeleteFlag = "" + records[0].get("BmsCenOrderDoc_DeleteFlag");
		if(DeleteFlag == "false" || DeleteFlag == false) DeleteFlag = '0';
		if(DeleteFlag == "1" || DeleteFlag == "true") {
			JShell.Msg.error("当前订单已被禁用!不能审核!");
			return;
		}
		//已申请的状态可以审核
		var status = records[0].get("BmsCenOrderDoc_Status");
		if(status != "1") {
			var statusName = "";
			if(me.OrderGrid.StatusEnum != null)
				statusName = me.OrderGrid.StatusEnum[status];
			JShell.Msg.error("当前订单状态为【" + statusName + "】,不能编辑!");
			return;
		}
		if(me.EditPanel.OrderDtlGrid.store.getCount() == 0) {
			JShell.Msg.error("当前订单货品明细为空!不能审核!");
			return;
		}

		var totalGoodsQty = 0;
		me.EditPanel.OrderDtlGrid.store.each(function(record) {
			var goodsQty = record.get("BmsCenOrderDtl_GoodsQty");
			if(!goodsQty) goodsQty = 0;
			totalGoodsQty += parseInt(goodsQty);
			if(totalGoodsQty > 0) return false;
		});
		if(totalGoodsQty == 0) {
			JShell.Msg.error("当前货品申请数量为0!不能保存!");
			return;
		}
		//处理意见
		JShell.Msg.confirm({
			title: '<div style="text-align:center;">审核意见</div>',
			msg: '',
			closable: false,
			multiline: true //多行输入框
		}, function(but, text) {
			if(but != "ok") return;
			var checkMemo = text;
			if(checkMemo) {
				checkMemo = checkMemo.replace(/\\/g, '&#92');
				checkMemo = checkMemo.replace(/[\r\n]/g, '<br />');
			}
			me.setFormType("edit");
			//需要保存主单及明细
			var params = me.EditPanel.getSaveParams();
			if(!params.entity.Id) params.entity.Id = records[0].get("BmsCenOrderDoc_Id");
			params.entity.Status = 3;
			params.entity.CheckMemo = checkMemo;
			if(!params.fields) {
				params.fields = "Id,Status";
			}
			if(params.entity.TotalPrice && params.fields.indexOf("TotalPrice") < 0) {
				params.fields += ",TotalPrice";
			}
			if(params.fields.indexOf("CheckMemo") < 0) params.fields += ",CheckMemo";
			if(!params.dtEditList) params.dtEditList = [];
			me.onSaveOfUpdate(params, function(data) {
				if(data.success) {
					JShell.Msg.alert('确认审核成功', null, 1000);
					me.OrderGrid.onSearch();
				} else {
					JShell.Msg.error('确认审核失败！' + data.msg);
				}
			});
		});
	},
	/**@description 审核退回按钮点击处理方法*/
	onUnCheckClick: function() {
		var me = this;
		var records = me.OrderGrid.getSelectionModel().getSelection();
		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		//申请
		var status = records[0].get("BmsCenOrderDoc_Status");
		if(status != "1") {
			var statusName = "";
			if(me.OrderGrid.StatusEnum != null)
				statusName = me.OrderGrid.StatusEnum[status];
			JShell.Msg.error("当前订单状态为【" + statusName + "】,不能编辑!");
			return;
		}

		if(me.EditPanel.OrderDtlGrid.store.getCount() == 0) {
			JShell.Msg.error("当前订单货品明细为空!不能审核退回!");
			return;
		}
		//处理意见
		JShell.Msg.confirm({
			title: '<div style="text-align:center;">审核意见</div>',
			msg: '',
			closable: false,
			multiline: true //多行输入框
		}, function(but, text) {
			if(but != "ok") return;
			var checkMemo = text;
			if(checkMemo) {
				checkMemo = checkMemo.replace(/\\/g, '&#92');
				checkMemo = checkMemo.replace(/[\r\n]/g, '<br />');
			}
			me.setFormType("edit");
			//需要保存主单及明细
			var params = me.EditPanel.getSaveParams();
			if(!params.entity.Id) params.entity.Id = records[0].get("BmsCenOrderDoc_Id");
			params.entity.Status = 2;
			params.entity.CheckMemo = checkMemo;
			if(!params.fields) {
				params.fields = "Id,Status";
			}
			if(params.entity.TotalPrice && params.fields.indexOf("TotalPrice") < 0) {
				params.fields += ",TotalPrice";
			}
			if(params.fields.indexOf("CheckMemo") < 0) params.fields += ",CheckMemo";
			if(!params.dtEditList) params.dtEditList = [];
			me.onSaveOfUpdate(params, function(data) {
				if(data.success) {
					JShell.Msg.alert('审核退回成功', null, 1000);
					me.OrderGrid.onSearch();
				} else {
					JShell.Msg.error('审核退回失败！' + data.msg);
				}
			});
		});
	}
});