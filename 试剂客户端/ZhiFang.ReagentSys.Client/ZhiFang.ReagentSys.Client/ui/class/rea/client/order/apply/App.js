/**
 * 客户端订单申请
 * @author longfc
 * @version 2017-11-15
 */
Ext.define('Shell.class.rea.client.order.apply.App', {
	extend: 'Shell.class.rea.client.order.basic.App',

	title: '订单申请',
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
		me.OrderGrid = Ext.create('Shell.class.rea.client.order.apply.OrderGrid', {
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
		var width = parseInt(document.body.clientWidth - me.OrderGrid.width - 220);
		me.EditPanel = Ext.create('Shell.class.rea.client.order.apply.EditPanel', {
			header: false,
			itemId: 'EditPanel',
			width: width,
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
		items.push("-");
		items = me.createReportClassButtonItem(items);
		items = me.createTemplate(items);
		
		//按菜单选择项选择是单个导出还是多个订单导出
		items = me.createPrintButtons(items);
		//按菜单选择项选择是单个导出还是多个订单导出
		//items = me.createSplitButton(items);

		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			border: false,
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	createPrintButtons: function(items) {
		var me = this;
		
		if (!items) items = [];
		items.push({
			xtype: 'button',
			iconCls: 'button-print',
			itemId: "btnPrint",
			text: 'PDF预览',
			tooltip: '预览PDF清单',
			//hidden:true,
			handler: function() {
				if (me.pdfFrx.indexOf("订单合并报表")>-1||me.pdfFrx.indexOf("合并报表")>-1) {
					me.onMergerePort(1);
				}else{
					me.onPrintClick();
				}
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
				if (me.pdfFrx.indexOf("订单合并报表")>-1||me.pdfFrx.indexOf("合并报表")>-1) {
					me.onMergerePort(2);
				}else{
					me.onDownLoadExcel();
				}
			}
		});

		return items;
	},
	createSplitButton: function(items) {
		var me = this;
		if (!items) items = [];

		items.push('-', {
			text: 'EXCEL导出',
			tooltip: 'EXCEL导出',
			iconCls: 'file-excel',
			//xtype: 'button',
			xtype: 'splitbutton',
			//width: 60,
			name: 'EXCEL',
			itemId: 'EXCEL',
			handler: function() {
				me.onDownLoadExcel();
			},
			menu: new Ext.menu.Menu({
				items: [{
					text: '单个订单导出',
					tooltip: '单个订单导出',
					iconCls: 'file-excel',
					handler: function() {
						me.onDownLoadExcel();
					}
				}, {
					text: '合并订单导出',
					tooltip: '合并报表导出',
					iconCls: 'file-excel',
					handler: function() {
						me.onMergerePort(2);
					}
				}]
			})
		});

		items.push({
			//xtype: 'button',
			xtype: 'splitbutton',
			iconCls: 'button-print',
			itemId: "btnPrint",
			text: '预览打印',
			tooltip: '预览打印',
			handler: function() {
				me.onPrintClick();
			},
			menu: new Ext.menu.Menu({
				items: [{
					text: '单个订单预览打印',
					tooltip: '单个订单预览打印',
					iconCls: 'button-print',
					handler: function() {
						me.onPrintClick();
					}
				}, {
					text: '合并订单预览打印',
					tooltip: '合并订单预览打印',
					iconCls: 'button-print',
					handler: function() {
						me.onMergerePort(1);
					}
				}]
			})
		});
		return items;
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
	},
	clearData: function(record) {
		var me = this;
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
		var status = "" + record.get("ReaBmsCenOrderDoc_Status");
		switch (status) {
			case "0": //临时
				break;
			case "2": //审核退回
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
		me.setBtnDisabled("btnTempSave", true);
		me.setBtnDisabled("btnSave", true);
		var status = record.get("ReaBmsCenOrderDoc_Status");
		switch (status) {
			case "0": //临时

				break;
			case "2": //审核退回

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
		var status = "" + record.get("ReaBmsCenOrderDoc_Status");
		if (status == "0" || status == "2") {
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
		if (!me.EditPanel.DocForm.getForm().isValid()) {
			JShell.Msg.alert('请填写订单基本必要信息', null, 1000);
			return;
		}
		if (me.EditPanel.OrderDtlGrid.store.getCount() <= 0) {
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
		if (!me.EditPanel.DocForm.getForm().isValid()) {
			JShell.Msg.alert('请填写订单基本必要信息', null, 1000);
			return;
		}
		if (me.EditPanel.OrderDtlGrid.store.getCount() <= 0) {
			JShell.Msg.alert('请选择货品明细信息再保存', null, 1000);
			return;
		}
		me.Status = '1';
		me.onSaveClick();
	},
	/**@description 保存按钮点击处理方法*/
	onSaveClick: function() {
		var me = this;
		if (!me.BUTTON_CAN_CLICK) return;

		var totalGoodsQty = 0;
		me.EditPanel.OrderDtlGrid.store.each(function(record) {
			var reqGoodsQty = record.get("ReaBmsCenOrderDtl_ReqGoodsQty");
			var goodsQty = record.get("ReaBmsCenOrderDtl_GoodsQty");

			if (!goodsQty) goodsQty = 0;
			totalGoodsQty += parseFloat(goodsQty);
			if (totalGoodsQty > 0) return false;
		});
		if (totalGoodsQty == 0) {
			JShell.Msg.error("当前货品申请数量为0!不能保存!");
			return;
		}

		me.BUTTON_CAN_CLICK = false; //不可点击
		me.showMask("保存中...");
		//需要保存主单及明细
		var params = me.EditPanel.getSaveParams();
		params.entity.Status = me.Status;
		me.onSaveOfUpdate(params, function(data) {
			me.hideMask();
			me.BUTTON_CAN_CLICK = true;

			if (data.success) {
				JShell.Msg.alert('订单保存成功', null, 1000);
				me.OrderGrid.onSearch();
			} else {
				JShell.Msg.error('订单保存失败！' + data.msg);
			}
		});
	},
	/**@description 合并导出订单*/
	onMergerePort: function(type1) {
		var me = this;
		var records = me.OrderGrid.getSelectionModel().getSelection();
		if (records.length <= 0) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}

		var idStr = "";
		var len = records.length;
		for (var i = 0; i < len; i++) {
			var rec = records[i];

			var visible = "" + rec.get("ReaBmsCenOrderDoc_Visible");
			if (visible == "false" || visible == false) visible = '0';
			if (visible == "0" || visible == "false") {
				JShell.Msg.error("选择的订单中包含有已禁用的订单,不能合并导出!");
				return;
			}
			var status = rec.get("ReaBmsCenOrderDoc_Status");
			//审核通过,订单上传,审批通过
			if (status == "3" || status == "4" || status == "12") {
				idStr += rec.get("ReaBmsCenOrderDoc_Id") + ",";
			} else {
				idStr = "";
				var statusName = "";
				if (JShell.REA.StatusList.Status[me.OrderGrid.StatusKey])
					statusName = JShell.REA.StatusList.Status[me.OrderGrid.StatusKey].Enum[status];
				JShell.Msg.error("选择的订单中包含有状态为【" + statusName + "】的申请单,不能合并导出!");
				return;
			}
		}

		JShell.Msg.confirm({
			title: '<div style="text-align:center;">合并导出</div>',
			msg: '请确认是否对当前选择的订单进行合并导出?',
			closable: false
		}, function(but, text) {
			if (but != "ok") return;
			if (idStr) {
				idStr = idStr.substring(0, idStr.length - 1);
				if (type1 == 1) {
					me.mergerePortPDF(idStr);
				} else {
					me.mergerePortExcel(idStr);
				}
			}
		});
	},
	mergerePortPDF: function(idStr) {
		var me = this,
			operateType = '1';
		if (!me.pdfFrx) {
			JShell.Msg.error("请先选择清单模板后再操作!");
			return;
		}
		if (!me.reaReportClass || me.reaReportClass != "Frx") {
			JShell.Msg.error("请先选择Frx模板后再操作!");
			return;
		}
		var url = JShell.System.Path.getRootUrl("/ReaManageService.svc/RS_UDTO_SearchReaBmsCenOrderDocOfPdfByIdStr");
		var params = [];
		params.push("reaReportClass=" + me.reaReportClass);
		params.push("operateType=" + operateType);
		params.push("idStr=" + idStr);
		params.push("breportType=" + me.breportType);
		if (me.pdfFrx) {
			params.push("frx=" + JShell.String.encode(me.pdfFrx));
		}
		url += "?" + params.join("&");
		window.open(url);
	},
	mergerePortExcel: function(idStr) {
		var me = this,
			operateType = '0';
		if (!me.pdfFrx) {
			JShell.Msg.error("请先选择清单模板后再操作!");
			return;
		}
		if (!me.reaReportClass || me.reaReportClass != "Excel") {
			JShell.Msg.error("请先选择Excel模板后再操作!");
			return;
		}
		var url = JShell.System.Path.getRootUrl("/ReaManageService.svc/RS_UDTO_SearchReaBmsCenOrderDocOfExcelByIdStr");
		var params = [];
		params.push("reaReportClass=" + me.reaReportClass);
		params.push("operateType=" + operateType);
		params.push("idStr=" + idStr);
		params.push("breportType=" + me.breportType);
		if (me.pdfFrx) {
			params.push("frx=" + JShell.String.encode(me.pdfFrx));
		}
		url += "?" + params.join("&");
		window.open(url);
	}

});
