/**
 * @description 部门采购审核
 * @author longfc
 * @version 2017-10-23
 */
Ext.define('Shell.class.rea.client.apply.check.App', {
	extend: 'Shell.class.rea.client.apply.basic.App',

	title: '采购审核',

	/**审核服务地址*/
	checkUrl: '/ReaManageService.svc/ST_UDTO_UpdateReaBmsReqDocAndDtOfCheck',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.ApplyGrid.on({
			selectionchange: function(model, selected, eOpts) {
				JShell.Action.delay(function() {
					me.selectAfter();
				}, null, 500);
			},
			nodata: function(p) {
				me.nodata();
			}
		});
	},
	selectAfter: function() {
		var me = this;
		var selected = me.ApplyGrid.getSelectionModel().getSelection();
		if(selected.length <= 0) {
			return;
		}
		var disabled = false;
		if(selected && selected.length > 0) {
			for(var i = 0; i < selected.length; i++) {
				var record = selected[i];
				var status = "" + record.get("ReaBmsReqDoc_Status");
				if(status != "3") {
					disabled = true;
					break;
				}
			}
			me.loadData(selected[selected.length - 1]);
		} else {
			disabled = true;
		}
		me.setBtnDisabled("btnGenerateOrders", disabled);
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
		me.ApplyGrid = Ext.create('Shell.class.rea.client.apply.check.ApplyGrid', {
			header: false,
			itemId: 'ApplyGrid',
			region: 'west',
			width: 345,
			split: true,
			collapsible: false,
			collapsed: false,
			animCollapse: false,
			animate: false
		});
		me.EditPanel = Ext.create('Shell.class.rea.client.apply.check.EditPanel', {
			header: false,
			itemId: 'EditPanel',
			region: 'center',
			collapsible: false,
			collapsed: false
		});
		var appInfos = [me.ApplyGrid, me.EditPanel];
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
			tooltip: '只对当前申请单进行审核通过',
			handler: function() {
				me.onCheckClick();
			}
		}, {
			xtype: 'button',
			iconCls: 'button-reset',
			itemId: "btnUnCheck",
			text: '审核退回',
			tooltip: '将选中的申请单批量审核退回',
			handler: function() {
				me.onUnCheckClick();
			}
		}, {
			xtype: 'button',
			iconCls: 'button-add',
			itemId: "btnGenerateOrders",
			text: '生成订单',
			tooltip: '将当前选择的申请单生成订单',
			handler: function() {
				me.onGenerateOrdersClick();
			}
		});
		items.push('-', {
			xtype: 'button',
			iconCls: 'button-save',
			itemId: "btnSave",
			text: '保存申请',
			tooltip: '编辑当前选中的申请单',
			disabled: true,
			handler: function() {
				me.onSaveClick();
			}
		});
		items.push("-");
		items = me.createReportClassButtonItem(items);
		items = me.createTemplate(items);
		items.push({
			xtype: 'button',
			iconCls: 'button-print',
			itemId: "btnPrint",
			text: '预览',
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
	nodata: function() {
		var me = this;
		me.callParent(arguments);
		me.setBtnDisabled("btnCheck", true);
		me.setBtnDisabled("btnUnCheck", true);
		me.setBtnDisabled("btnSave", true);
		me.setBtnDisabled("btnGenerateOrders", true);
	},
	loadData: function(record) {
		var me = this;
		//已申请可以修改
		var status = record.get("ReaBmsReqDoc_Status");
		if(status == "2") {
			me.isEdit(record);
		} else {
			me.isShow(record);
		}
	},
	isEdit: function(record) {
		var me = this;
		me.setFormType("edit");
		me.EditPanel.isEdit(record, me.ApplyGrid);
		me.setBtnDisabled("btnSave", false);
		var status = record.get("ReaBmsReqDoc_Status");
		//明细的新增,删除,保存启用
		me.EditPanel.ReqDtlGrid.buttonsDisabled = false;
		me.EditPanel.ReqDtlGrid.setButtonsDisabled(false);
		switch(status) {
			case "2": //已申请
				me.setBtnDisabled("btnCheck", false);
				me.setBtnDisabled("btnUnCheck", false);
				break;
			default:
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

		var status = record.get("ReaBmsReqDoc_Status");
		switch(status) {
			case "2": //已申请
				me.setBtnDisabled("btnCheck", false);
				me.setBtnDisabled("btnUnCheck", false);
				break;
			default:
				//明细的新增,删除,保存默认禁用
				me.EditPanel.ReqDtlGrid.buttonsDisabled = true;
				me.EditPanel.ReqDtlGrid.setButtonsDisabled(true);
				break;
		}
	},
	/**@description 保存按钮点击处理方法*/
	onSaveClick: function() {
		var me = this;
		var records = me.ApplyGrid.getSelectionModel().getSelection();
		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var isBreak = false;
		var msgInfo = "";
		me.EditPanel.ReqDtlGrid.store.each(function(record) {
			var goodsQty = record.get("ReaBmsReqDtl_GoodsQty");
			if(!goodsQty) goodsQty = 0;
			goodsQty = parseFloat(goodsQty);
			if(goodsQty <= 0) {
				isBreak = true;
				var goodName = record.get("ReaBmsReqDtl_GoodsCName");
				msgInfo = "当前货品名为" + goodName + ",其申请数量<=0!不能保存!";
				return false;
			}
		});
		if(isBreak == true) {
			JShell.Msg.error(msgInfo);
			return;
		}

		me.setFormType("edit");
		//需要保存主单及明细
		var params = me.EditPanel.getSaveParams();
		params.entity.Status = me.EditPanel.ReqDtlGrid.Status;
		me.onSaveOfUpdate(params, function(data) {
			if(data.success) {
				JShell.Msg.alert('申请单保存成功', null, 1000);
				me.ApplyGrid.onSearch();
			} else {
				JShell.Msg.error('申请单保存失败！' + data.msg);
			}
		});
	},
	/**@description 审核按钮点击处理方法*/
	onCheckClick: function() {
		var me = this;
		var records = me.ApplyGrid.getSelectionModel().getSelection();
		if(records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		if(records.length != 1) {
			JShell.Msg.error("请选择一个申请单进行操作!");
			return;
		}
		var visible = "" + records[0].get("ReaBmsReqDoc_Visible");
		if(visible == "false" || visible == false) visible = '0';
		if(visible == "0" || visible == "false") {
			JShell.Msg.error("当前申请单已被禁用!不能审核!");
			return;
		}
		//已申请的状态可以审核
		var status = records[0].get("ReaBmsReqDoc_Status");
		if(status != "2") {
			var statusName = "";
			if(JShell.REA.StatusList.Status[me.ApplyGrid.StatusKey])
				statusName = JShell.REA.StatusList.Status[me.ApplyGrid.StatusKey].Enum[status];
			JShell.Msg.error("当前申请单状态为【" + statusName + "】,不能编辑!");
			return;
		}
		if(me.EditPanel.ReqDtlGrid.store.getCount() == 0) {
			JShell.Msg.error("当前申请单货品明细为空!不能审核!");
			return;
		}

		//审核时需要先验收货品明细里的供应商是否存在为空的,有空的供应商时不能审核
		var isBreak = false;
		var msgInfo = "";
		me.EditPanel.ReqDtlGrid.store.each(function(record) {
			var goodsQty = record.get("ReaBmsReqDtl_GoodsQty");
			if(!goodsQty) goodsQty = 0;
			goodsQty = parseFloat(goodsQty);
			if(goodsQty <= 0) {
				isBreak = true;
				var goodName = record.get("ReaBmsReqDtl_GoodsCName");
				msgInfo = "当前货品名为" + goodName + ",其申请数量<=0!不能审核!";
				return false;
			} else if(!record.get("ReaBmsReqDtl_ReaCenOrg_Id")) {
				isBreak = true;
				var goodName = record.get("ReaBmsReqDtl_GoodsCName");
				msgInfo = "当前货品名为" + goodName + ",其供应商信息为空!不能审核!";
				return false;
			}
		});
		if(isBreak == true) {
			JShell.Msg.error(msgInfo);
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
			var reviewMemo = text;
			if(reviewMemo) {
				reviewMemo = reviewMemo.replace(/\\/g, '&#92');
				reviewMemo = reviewMemo.replace(/[\r\n]/g, '<br />');
			}
			if(but != "ok") return;
			me.setFormType("edit");
			//需要保存主单及明细
			var params = me.EditPanel.getSaveParams();
			params.entity.Status = 3;
			params.entity.ReviewMemo = reviewMemo;
			if(!params.fields) params.fields = "Id,Status,ReviewMemo";
			if(!params.dtEditList) params.dtEditList = [];
			me.onSaveOfCheck(params, function(data) {
				if(data.success) {
					JShell.Msg.alert('审核通过操作成功', null, 1000);
					me.ApplyGrid.onSearch();
				} else {
					JShell.Msg.error('审核通过操作失败！' + data.msg);
				}
			});
		});
	},
	/**@description审核处理处理*/
	onSaveOfCheck: function(params, callback) {
		var me = this;
		//需要保存主单及明细
		if(!params) params = me.EditPanel.getSaveParams();
		if(!params) return false;
		var url = me.checkUrl;
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;

		JShell.Server.post(url, Ext.JSON.encode(params), function(data) {
			callback(data);
		});
	},
	/**@description 审核退回按钮点击处理方法*/
	onUnCheckClick: function() {
		var me = this;
		var records = me.ApplyGrid.getSelectionModel().getSelection();
		if(records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}

		if(records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		if(records.length != 1) {
			JShell.Msg.error("请选择一个申请单进行操作!");
			return;
		}
		var visible = "" + records[0].get("ReaBmsReqDoc_Visible");
		if(visible == "false" || visible == false) visible = '0';
		if(visible == "0" || visible == "false") {
			JShell.Msg.error("当前申请单已被禁用!不能审核退回操作!");
			return;
		}
		//已申请的状态可以审核退回
		var status = records[0].get("ReaBmsReqDoc_Status");
		if(status != "2") {
			var statusName = "";
			if(JShell.REA.StatusList.Status[me.ApplyGrid.StatusKey])
				statusName = JShell.REA.StatusList.Status[me.ApplyGrid.StatusKey].Enum[status];
			JShell.Msg.error("当前申请单状态为【" + statusName + "】,不能审核退回!");
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
			var reviewMemo = text;
			if(reviewMemo) {
				reviewMemo = reviewMemo.replace(/\\/g, '&#92');
				reviewMemo = reviewMemo.replace(/[\r\n]/g, '<br />');
			}
			if(but != "ok") return;
			me.setFormType("edit");
			var params = me.EditPanel.getSaveParams();
			params.entity.Status = 4;
			params.entity.ReviewMemo = reviewMemo;
			if(!params.fields) params.fields = "Id,Status,ReviewMemo";
			if(!params.dtEditList) params.dtEditList = [];
			me.onSaveOfCheck(params, function(data) {
				if(data.success) {
					JShell.Msg.alert('审核退回操作成功', null, 1000);
					me.ApplyGrid.onSearch();
				} else {
					JShell.Msg.error('审核退回操作失败！' + data.msg);
				}
			});
		});
	},
	/**@description 生成订单按钮点击处理方法*/
	onGenerateOrdersClick: function() {
		var me = this;
		var records = me.ApplyGrid.getSelectionModel().getSelection();
		if(records.length <= 0) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}

		var idStr = "";
		var len = records.length;
		for(var i = 0; i < len; i++) {
			var rec = records[i];

			var visible = "" + rec.get("ReaBmsReqDoc_Visible");
			if(visible == "false" || visible == false) visible = '0';
			if(visible == "0" || visible == "false") {
				JShell.Msg.error("选择的申请单中包含有已禁用的申请单!不能生成订单!");
				return;
			}
			var status = rec.get("ReaBmsReqDoc_Status");
			//已审核
			if(status == "3") {
				idStr += rec.get("ReaBmsReqDoc_Id") + ",";
			} else {
				idStr = "";
				var statusName = "";
				if(JShell.REA.StatusList.Status[me.ApplyGrid.StatusKey])
					statusName = JShell.REA.StatusList.Status[me.ApplyGrid.StatusKey].Enum[status];
				JShell.Msg.error("选择的申请单中包含有状态为【" + statusName + "】的申请单,不能生成订单!");
				return;
			}
		}

		JShell.Msg.confirm({
			title: '<div style="text-align:center;">生成订单操作确认</div>',
			msg: '请确认是否对当前选择的申请单进行转换生成订单?',
			closable: false
		}, function(but, text) {
			if(but != "ok") return;
			if(idStr) {
				idStr = idStr.substring(0, idStr.length - 1);
				me.confirmGenerateOrders(idStr);
			}
		});
	},
	/**@description 确认生成订单处理方法*/
	confirmGenerateOrders: function(idStr) {
		var me = this;
		if(!idStr) return false;
		var params = {
			"idStr": idStr
		};
		var reaServerLabcCode = JcallShell.REA.System.CENORG_CODE;
		if(!reaServerLabcCode) reaServerLabcCode = "";
		var labcName = JcallShell.REA.System.CENORG_NAME;
		var params = {
			"idStr": idStr,
			"commonIsMerge": true, //常规合并
			"ugentIsMerge": true, //紧急不合并
			"reaServerLabcCode": reaServerLabcCode,
			"labcName": labcName
		};
		var url = JShell.System.Path.ROOT + '/ReaManageService.svc/ST_UDTO_AddReaCenOrgBmsCenOrderDocOfReaBmsReqDocIDStr';
		JShell.Server.post(url, JcallShell.JSON.encode(params), function(data) {
			if(data.success) {
				JShell.Msg.alert('生成订单操作成功', null, 1000);
				me.ApplyGrid.onSearch();
			} else {
				JShell.Msg.error('生成订单操作失败！' + data.msg);
			}
		});
	}
});