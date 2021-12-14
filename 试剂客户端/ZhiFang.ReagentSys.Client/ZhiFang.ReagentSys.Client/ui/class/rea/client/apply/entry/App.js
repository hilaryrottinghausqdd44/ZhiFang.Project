/**
 * @description 部门采购申请录入
 * @author longfc
 * @version 2017-10-23
 */
Ext.define('Shell.class.rea.client.apply.entry.App', {
	extend: 'Shell.class.rea.client.apply.basic.App',

	title: '采购申请',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.ApplyGrid.on({
			selectionchange: function(model, selected, eOpts) {
				JShell.Action.delay(function() {
					if(selected.length > 0) me.loadData(selected[selected.length - 1]);
				}, null, 500);
			},
			nodata: function(p) {
				me.nodata();
			}
		});
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
		me.ApplyGrid = Ext.create('Shell.class.rea.client.apply.entry.ApplyGrid', {
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
		me.EditPanel = Ext.create('Shell.class.rea.client.apply.entry.EditPanel', {
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

		items.push({
			xtype: 'button',
			iconCls: 'button-add',
			itemId: "btnAdd",
			text: '新增申请',
			tooltip: '新增申请单',
			handler: function() {
				me.onAddClick();
			}
		}, {
			xtype: 'button',
			iconCls: 'button-add',
			itemId: "btnAddOfCopy",
			text: '复制申请',
			tooltip: '复制并新增申请单',
			handler: function() {
				me.onAddCopyClick();
			}
		}, '-', {
			xtype: 'button',
			itemId: 'btnTempSave',
			iconCls: 'button-save',
			text: "申请暂存",
			tooltip: "暂存不提交申请",
			handler: function(btn, e) {
				me.onTempSaveClick(btn, e);
			}
		}, {
			xtype: 'button',
			itemId: 'btnApplyForConfirm',
			iconCls: 'button-save',
			text: "申请确认",
			tooltip: "申请确认提交",
			handler: function(btn, e) {
				me.onApplyClick(btn, e);
			}
		});
		items.push("-");
		items=me.createReportClassButtonItem(items);
		items=me.createTemplate(items);
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
	nodata: function(record) {
		var me = this;
		me.callParent(arguments);
		me.setBtnDisabled("btnTempSave", true);
		me.setBtnDisabled("btnApplyForConfirm", true);
		me.EditPanel.ReqDtlGrid.buttonsDisabled = true;
	},
	loadData: function(record) {
		var me = this;
		//临时,审核退回可以修改
		var status = record.get("ReaBmsReqDoc_Status");
		if(status == "1" || status == "4") {
			me.isEdit(record);
		} else {
			me.isShow(record);
		}
	},
	isAdd: function(record) {
		var me = this;
		me.callParent(arguments);
		me.setFormType("add");
		me.setBtnDisabled("btnTempSave", false);
		me.setBtnDisabled("btnApplyForConfirm", false);
	},
	isEdit: function(record) {
		var me = this;
		me.callParent(arguments);
		me.setFormType("edit");
		me.setBtnDisabled("btnTempSave", false);
		me.setBtnDisabled("btnApplyForConfirm", false);
		var status = record.get("ReaBmsReqDoc_Status");

		//明细的新增,删除,保存启用
		me.EditPanel.ReqDtlGrid.buttonsDisabled = false;
		me.EditPanel.ReqDtlGrid.setButtonsDisabled(false);
	},
	isShow: function(record) {
		var me = this;
		me.callParent(arguments);
		me.setFormType("show");
		me.setBtnDisabled("btnTempSave", true);
		me.setBtnDisabled("btnApplyForConfirm", true);
		var status = record.get("ReaBmsReqDoc_Status");
		switch(status) {
			case "1": //临时
				break;
			case "4": //审核退回
				break;
			default:
				//明细的新增,删除,保存默认禁用
				me.EditPanel.ReqDtlGrid.buttonsDisabled = true;
				break;
		}
	},
	/**@description 新增按钮点击处理方法*/
	onAddClick: function() {
		var me = this;
		me.setFormType("add");
		me.isAdd();
	},
	/**@description 复制并新增按钮点击处理方法*/
	onAddCopyClick: function() {
		var me = this;
		var records = me.ApplyGrid.getSelectionModel().getSelection();
		if(records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		if(records.length != 1) {
			JShell.Msg.error("请选择一个申请单后再进行操作!");
			return;
		}

		JShell.Msg.confirm({
			title: '<div style="text-align:center;">复制申请操作确认</div>',
			msg: '请确认你是否需要复制新增申请单!点【确定】继续',
			closable: false,
			multiline: false
		}, function(but, text) {
			if(but != "ok") return;
			var id = records[0].get(me.ApplyGrid.PKField);
			var url = '/ReaManageService.svc/ST_UDTO_AddReaBmsReqDocAndDtOfCopyApply';
			url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
			var params = {
				"id": id
			}
			JShell.Server.post(url, Ext.JSON.encode(params), function(data) {
				if(data.success) {
					JShell.Msg.alert('复制申请单成功', null, 1000);
					me.ApplyGrid.onSearch();
				} else {
					JShell.Msg.error('复制申请单失败！' + data.msg);
				}
			});
		});
	},
	onTempSaveClick: function(btn, e) {
		var me = this;
		//先验证是否能保存
		if(!me.EditPanel.ApplyForm.getForm().isValid()) {
			JShell.Msg.alert('请填写申请单基本必要信息', null, 1000);
			return;
		}
		if(me.EditPanel.ReqDtlGrid.store.getCount() <= 0) {
			JShell.Msg.alert('请选择货品明细信息再保存', null, 1000);
			return;
		}
		me.Status = '1';
		me.onSaveClick();
	},
	/**
	 * @description 确认提交
	 */
	onApplyClick: function(btn, e) {
		var me = this;
		//先验证是否能保存
		if(!me.EditPanel.ApplyForm.getForm().isValid()) {
			JShell.Msg.alert('请填写申请单基本必要信息', null, 1000);
			return;
		}
		if(me.EditPanel.ReqDtlGrid.store.getCount() <= 0) {
			JShell.Msg.alert('请选择货品明细信息再保存', null, 1000);
			return;
		}
		var eOpts = {
			isValid: true,
			msgInfo: ""
		};
		me.Status = '2';
		me.onSaveClick();
	},
	/**@description 保存按钮点击处理方法*/
	onSaveClick: function() {
		var me = this;

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

		//需要保存主单及明细
		var params = me.EditPanel.getSaveParams();
		params.entity.Status = me.Status;
		me.onSaveOfUpdate(params, function(data) {
			if(data.success) {
				JShell.Msg.alert('申请单保存成功', null, 1000);
				me.ApplyGrid.onSearch();
			} else {
				JShell.Msg.error('申请单保存失败！' + data.msg);
			}
		});
	}
});