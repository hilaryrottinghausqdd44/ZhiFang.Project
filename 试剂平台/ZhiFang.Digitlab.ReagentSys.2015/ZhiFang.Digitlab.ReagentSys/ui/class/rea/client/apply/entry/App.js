/**
 * @description 部门采购申请录入
 * @author longfc
 * @version 2017-10-23
 */
Ext.define('Shell.class.rea.client.apply.entry.App', {
	extend: 'Shell.class.rea.client.apply.basic.App',

	title: '采购申请',

	/**录入:entry/审核:check/生成订单:create*/
	OTYPE: "entry",
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.ApplyGrid.on({
			select: function(RowModel, record) {
//				JShell.Action.delay(function() {
//					me.loadData(record);
//				}, null, 500);
			},
			selectionchange: function(model, selected, eOpts) {
				JShell.Action.delay(function() {
					if(selected.length>0)me.loadData(selected[selected.length - 1]);
				}, null, 500);
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
		me.ApplyGrid = Ext.create('Shell.class.rea.client.apply.entry.ApplyGrid', {
			header: false,
			itemId: 'ApplyGrid',
			region: 'west',
			width: 405,
			split: true,
			OTYPE: me.OTYPE,
			collapsible: false,
			collapsed: false
		});
		me.EditPanel = Ext.create('Shell.class.rea.client.apply.entry.EditPanel', {
			header: false,
			itemId: 'EditPanel',
			region: 'center',
			collapsible: false,
			collapsed: false,
			OTYPE: me.OTYPE
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
			iconCls: 'button-check',
			itemId: "btnApply",
			text: '批量申请确认',
			tooltip: '临时,已撤消审核单子批量申请确认',
			handler: function() {
				me.onBatchApplyClick();
			}
		}, {
			xtype: 'button',
			iconCls: 'button-del',
			itemId: "btnUnVisible",
			hidden:true,
			text: '批量禁用',
			tooltip: '将勾选的已启用行的修改为禁用',
			handler: function() {
				me.onBatchUnVisibleClick();
			}
		}, {
			xtype: 'button',
			iconCls: 'button-check',
			itemId: "btnVisible",
			hidden:true,
			text: '批量启用',
			tooltip: '将勾选的已禁用行的修改为启用',
			handler: function() {
				me.onBatchVisibleClick();
			}
		});
		items.push('-', {
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
			itemId: 'btnTempSave',
			iconCls: 'button-save',
			text: "申请暂存",
			tooltip: "暂存不提交申请",
			handler: function(btn, e) {
				me.onTempSaveClick(btn, e);
			}
		}, {
			xtype: 'button',
			itemId: 'btnSave',
			iconCls: 'button-save',
			text: "申请确认",
			tooltip: "申请确认提交",
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
	nodata: function(record) {
		var me = this;
		me.callParent(arguments);
		me.setBtnDisabled("btnApply", true);
		me.setBtnDisabled("btnTempSave", true);
		me.setBtnDisabled("btnSave", true);
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
		me.setBtnDisabled("btnSave", false);
	},
	isEdit: function(record) {
		var me = this;
		me.callParent(arguments);
		me.setFormType("edit");
		me.setBtnDisabled("btnTempSave", false);
		me.setBtnDisabled("btnSave", false);
		var status = record.get("ReaBmsReqDoc_Status");
		switch(status) {
			case "1": //临时
				me.setBtnDisabled("btnApply", false);
				break;
			case "4": //审核退回
				me.setBtnDisabled("btnApply", false);
				break;
			default:
				break;
		}
		//明细的新增,删除,保存启用
		me.EditPanel.ReqDtlGrid.buttonsDisabled = false;
		me.EditPanel.ReqDtlGrid.setButtonsDisabled(false);
	},
	isShow: function(record) {
		var me = this;
		me.callParent(arguments);
		me.setFormType("show");
		me.setBtnDisabled("btnApply", true);
		me.setBtnDisabled("btnTempSave", true);
		me.setBtnDisabled("btnSave", true);
		var status = record.get("ReaBmsReqDoc_Status");
		switch(status) {
			case "1": //临时
				me.setBtnDisabled("btnApply", false);
				break;
			case "4": //审核退回
				me.setBtnDisabled("btnApply", false);
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
			goodsQty = parseInt(goodsQty);
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
	},
	/**@description 批量确认申请按钮点击处理方法*/
	onBatchApplyClick: function() {
		var me = this;
		var records = me.ApplyGrid.getSelectionModel().getSelection();
		if(records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		var dtlList = [];
		var len = records.length;
		for(var i = 0; i < len; i++) {
			var rec = records[i];
			var visible = "" + rec.get("ReaBmsReqDoc_Visible");
			if(visible == "false" || visible == false) visible = '0';
			if(visible == "0" || visible == "false") {
				JShell.Msg.error("申请单中包含有已禁用的申请单!不能操作!");
				return;
			}
			//临时,已撤消审核
			var status = rec.get("ReaBmsReqDoc_Status");
			if(status == "1" || status == "4") {
				dtlList.push(rec.get(me.ApplyGrid.PKField));
			} else {
				dtlList = [];
				var statusName = "";
				if(me.ApplyGrid.StatusEnum != null)
					statusName = me.ApplyGrid.StatusEnum[status];
				JShell.Msg.error("申请单中包含有状态为【" + statusName + "】,不能操作!");
				return;
			}
		}

		var isBreak = false;
		var msgInfo = "";
		me.EditPanel.ReqDtlGrid.store.each(function(record) {
			var goodsQty = record.get("ReaBmsReqDtl_GoodsQty");
			if(!goodsQty) goodsQty = 0;
			goodsQty = parseInt(goodsQty);
			if(goodsQty <= 0) {
				isBreak = true;
				var goodName = record.get("ReaBmsReqDtl_GoodsCName");
				msgInfo = "当前货品名为" + goodName + ",其申请数量<=0!不能确认申请!";
				return false;
			}
		});
		if(isBreak == true) {
			JShell.Msg.error(msgInfo);
			return;
		}

		me.setFormType("edit");
		if(dtlList.length > 0) me.confirmUpdate(dtlList, "Status", 2, "批量确认申请");
	},
	/**@description 批量禁用按钮处理方法*/
	onBatchUnVisibleClick: function() {
		var me = this;
		var records = me.ApplyGrid.getSelectionModel().getSelection();
		if(records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		me.onUupdateVisible(records, 0, function(result) {});
	},
	/**@description 批量启用按钮点击处理方法*/
	onBatchVisibleClick: function() {
		var me = this;
		var records = me.ApplyGrid.getSelectionModel().getSelection();
		if(records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		me.onUupdateVisible(records, 1, function(result) {});
	},
	/**@description 批量启用/禁用处理*/
	onUupdateVisible: function(records, visibleValue, callback) {
		var me = this;
		var msg = (visibleValue == 1 ? "批量启用" : "批量禁用");
		var dtlList = [];
		var len = records.length;
		for(var i = 0; i < len; i++) {
			var rec = records[i];
			//转为订单不能操作
			var status = rec.get("ReaBmsReqDoc_Status");
			if(status != "5") {
				var visible = rec.get("ReaBmsReqDoc_Visible");
				visible = parseInt(visible);
				if(visibleValue != visible) {
					dtlList.push(rec);
				}
			} else {
				dtlList = [];
				var statusName = "";
				if(me.ApplyGrid.StatusEnum != null)
					statusName = me.ApplyGrid.StatusEnum[status];
				JShell.Msg.error("申请单中包含有状态为【" + statusName + "】,不能操作!");
				return;
			}
		}

		if(dtlList.length > 0) {
			JShell.Msg.confirm({
				title: msg
			}, function(but) {
				if(but != "ok") return;
				me.saveErrorCount = 0;
				me.saveCount = 0;
				me.saveLength = dtlList.length;
				me.showMask(me.saveText); //显示遮罩层
				for(var i in dtlList) {
					me.updateVisibleOne(i, dtlList[i], "Visible", visibleValue, msg, callback);
				}
			});
		}
	},
	/**@description 单个启用/禁用处理*/
	updateVisibleOne: function(index, record, key, value, msg, callback) {
		var me = this;
		var url = JShell.System.Path.ROOT + "/ReaSysManageService.svc/ST_UDTO_UpdateReaBmsReqDocByField";
		var Id = record.get(me.ApplyGrid.PKField);
		var params = {
			entity: {
				"Id": Id
			},
			fields: 'Id'
		};
		params.entity[key] = value;
		params.fields += ',' + key;

		setTimeout(function() {
			JShell.Server.post(url, JcallShell.JSON.encode(params), function(data) {
				if(data.success) {
					record.set(key, value);
					record.commit();
					me.saveCount++;
				} else {
					me.saveErrorCount++;
				}
				if(me.saveCount + me.saveErrorCount == me.saveLength) {
					me.hideMask(); //隐藏遮罩层
					if(me.saveErrorCount == 0) {
						JShell.Msg.alert(msg + '操作成功', null, 1000);
						me.ApplyGrid.onSearch();
						callback();
					} else {
						JShell.Msg.error(msg + '操作失败，请重新操作！');
					}
				}
			});
		}, 100 * index);
	}
});