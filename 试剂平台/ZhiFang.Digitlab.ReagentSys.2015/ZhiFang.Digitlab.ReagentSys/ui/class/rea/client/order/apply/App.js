/**
 * 客户端订单申请
 * @author longfc
 * @version 2017-11-15
 */
Ext.define('Shell.class.rea.client.order.apply.App', {
	extend: 'Shell.class.rea.client.order.basic.App',

	title: '订单申请',

	/**录入:entry/审核:check*/
	OTYPE: "entry",
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
		me.OTYPE = me.OTYPE || "entry";
		//自定义按钮功能栏
		me.dockedItems = me.createButtonToolbarItems();
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.OrderGrid = Ext.create('Shell.class.rea.client.order.apply.OrderGrid', {
			header: false,
			itemId: 'OrderGrid',
			region: 'west',
			width: 415,
			split: true,
			OTYPE: me.OTYPE,
			collapsible: false,
			collapsed: false
		});
		me.EditPanel = Ext.create('Shell.class.rea.client.order.apply.EditPanel', {
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
		items.push({
			xtype: 'button',
			iconCls: 'button-check',
			itemId: "btnApply",
			hidden:true,
			text: '批量申请确认',
			tooltip: '将临时,已审核退回订单批量申请确认',
			handler: function() {
				me.onBatchApplyClick();
			}
		});
		items.push('-', {
			xtype: 'button',
			iconCls: 'button-add',
			itemId: "btnAdd",
			text: '新增订单',
			tooltip: '新增订单',
			handler: function() {
				me.onAddClick();
			}
		}, {
			xtype: 'button',
			itemId: 'btnTempSave',
			iconCls: 'button-save',
			text: "订单暂存",
			tooltip: "订单暂存不提交",
			handler: function(btn, e) {
				me.onTempSaveClick(btn, e);
			}
		}, {
			xtype: 'button',
			itemId: 'btnSave',
			iconCls: 'button-save',
			text: "订单提交",
			tooltip: "订单申请确认提交",
			handler: function(btn, e) {
				me.onApplyClick(btn, e);
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
		me.setBtnDisabled("btnApply", true);
		me.setBtnDisabled("btnTempSave", true);
		me.setBtnDisabled("btnSave", true);
		me.EditPanel.OrderDtlGrid.buttonsDisabled = true;
	},
	isAdd: function(record) {
		var me = this;
		me.callParent(arguments);
		me.setFormType("add");
		me.setBtnDisabled("btnTempSave", false);
		me.setBtnDisabled("btnSave", false);
	},
	isEdit: function(record) {
		var me = this;
		me.callParent(arguments);
		me.setFormType("edit");
		me.setBtnDisabled("btnTempSave", false);
		me.setBtnDisabled("btnSave", false);
		var status = record.get("BmsCenOrderDoc_Status");
		switch(status) {
			case "0": //临时
				me.setBtnDisabled("btnApply", false);
				break;
			case "2": //审核退回
				me.setBtnDisabled("btnApply", false);
				break;
			default:
				//明细的新增,删除,保存默认禁用
				me.EditPanel.OrderDtlGrid.buttonsDisabled = true;
				break;
		}
	},
	isShow: function(record) {
		var me = this;
		me.callParent(arguments);
		me.setFormType("show");
		me.setBtnDisabled("btnApply", true);
		me.setBtnDisabled("btnTempSave", true);
		me.setBtnDisabled("btnSave", true);
		var status = record.get("BmsCenOrderDoc_Status");
		switch(status) {
			case "0": //临时
				me.setBtnDisabled("btnApply", false);
				break;
			case "2": //审核退回
				me.setBtnDisabled("btnApply", false);
				break;
			default:
				//明细的新增,删除,保存默认禁用
				me.EditPanel.OrderDtlGrid.buttonsDisabled = true;
				break;
		}
	},
	loadData: function(record) {
		var me = this;
		//临时,审核退回可以修改
		var status = record.get("BmsCenOrderDoc_Status");
		if(status == "0" || status == "2") {
			me.isEdit(record);
		} else {
			me.isShow(record);
		}
	},
	/**@description 新增按钮点击处理方法*/
	onAddClick: function() {
		var me = this;
		me.setFormType("add");
		me.isAdd();
	},
	onTempSaveClick: function(btn, e) {
		var me = this;
		//先验证是否能保存
		if(!me.EditPanel.DocForm.getForm().isValid()) {
			JShell.Msg.alert('请填写订单基本必要信息', null, 1000);
			return;
		}
		if(me.EditPanel.OrderDtlGrid.store.getCount() <= 0) {
			JShell.Msg.alert('请选择货品明细信息再保存', null, 1000);
			return;
		}
		me.Status = '0';
		me.onSaveClick();
	},
	/**
	 * @description 确认提交
	 */
	onApplyClick: function(btn, e) {
		var me = this;
		//先验证是否能保存
		if(!me.EditPanel.DocForm.getForm().isValid()) {
			JShell.Msg.alert('请填写订单基本必要信息', null, 1000);
			return;
		}
		if(me.EditPanel.OrderDtlGrid.store.getCount() <= 0) {
			JShell.Msg.alert('请选择货品明细信息再保存', null, 1000);
			return;
		}
		me.Status = '1';
		me.onSaveClick();
	},
	/**@description 保存按钮点击处理方法*/
	onSaveClick: function() {
		var me = this;

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
		//需要保存主单及明细
		var params = me.EditPanel.getSaveParams();
		params.entity.Status = me.Status;
		me.onSaveOfUpdate(params, function(data) {
			if(data.success) {
				JShell.Msg.alert('订单保存成功', null, 1000);
				me.OrderGrid.onSearch();
			} else {
				JShell.Msg.error('订单保存失败！' + data.msg);
			}
		});
	},
	/**@description 批量确认申请按钮点击处理方法*/
	onBatchApplyClick: function() {
		var me = this;
		var records = me.OrderGrid.getSelectionModel().getSelection();
		if(records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		var dtlList = [];
		var len = records.length;
		for(var i = 0; i < len; i++) {
			var rec = records[i];
			var DeleteFlag = "" + rec.get("BmsCenOrderDoc_DeleteFlag");
			if(DeleteFlag == "false" || DeleteFlag == false) DeleteFlag = '0';
			if(DeleteFlag == "1" || DeleteFlag == "true") {
				JShell.Msg.error("订单中包含有已禁用的订单!不能操作!");
				return;
			}
			//临时,审核退回
			var status = rec.get("BmsCenOrderDoc_Status");
			if(status == "0" || status == "2") {
				dtlList.push(rec.get(me.OrderGrid.PKField));
			} else {
				dtlList = [];
				var statusName = "";
				if(me.OrderGrid.StatusEnum != null)
					statusName = me.OrderGrid.StatusEnum[status];
				JShell.Msg.error("订单中包含有状态为【" + statusName + "】,不能操作!");
				return;
			}
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
		if(dtlList.length > 0) me.confirmUpdate(dtlList, "Status", 1, "批量订单申请提交确认");
	}
});