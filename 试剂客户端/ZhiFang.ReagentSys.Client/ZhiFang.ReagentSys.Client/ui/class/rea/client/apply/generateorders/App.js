/**
 * @description 部门采购生成订单
 * @author longfc
 * @version 2018-02-26
 */
Ext.define('Shell.class.rea.client.apply.generateorders.App', {
	extend: 'Shell.class.rea.client.apply.basic.App',

	title: '生成订单',

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
		me.ApplyGrid = Ext.create('Shell.class.rea.client.apply.generateorders.ApplyGrid', {
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
		me.EditPanel = Ext.create('Shell.class.rea.client.apply.generateorders.EditPanel', {
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
			xtype: 'checkboxfield',
			boxLabel: '常规合并',
			checked: true,
			inputValue: 1,
			itemId: 'cboCommonIsByDeptMerge'
		}, {
			xtype: 'checkboxfield',
			boxLabel: '<b style="color:red;">紧急不合并</b>',
			checked: true,
			inputValue: 1,
			itemId: 'cboUrgentIsByDeptMerge'
		}, {
			xtype: 'button',
			iconCls: 'button-check',
			itemId: "btnGenerateOrders",
			text: '生成订单',
			tooltip: '将当前选择的申请单生成订单',
			handler: function() {
				me.onGenerateOrdersClick();
			}
		}, '-', {
			xtype: 'button',
			itemId: "btnInfo",
			disabled: true,
			text: '说明:<b style="color:blue;">常规合并:指常规申请单生成订单时只按供应商分组合并;紧急不合并:指紧急的申请单先按申请部门分组,再按供货商分组合并生成订单;</b>'
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
		me.setBtnDisabled("btnGenerateOrders", true);
	},
	loadData: function(record) {
		var me = this;
		//临时,审核退回可以修改
		var status = record.get("ReaBmsReqDoc_Status");
		me.isShow(record);
	},
	isShow: function(record) {
		var me = this;
		me.callParent(arguments);
		me.setFormType("show");

		var status = record.get("ReaBmsReqDoc_Status");
		me.EditPanel.ReqDtlGrid.buttonsDisabled = true;
		me.EditPanel.ReqDtlGrid.setButtonsDisabled(true);
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
			msg: '请确认是否生成订单?',
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

		var buttonsToolbar = me.getComponent("buttonsToolbar");
		var commonIsMerge = buttonsToolbar.getComponent("cboCommonIsByDeptMerge").getValue();
		if(commonIsMerge == 1 || commonIsMerge == true)
			commonIsMerge = true;
		else
			commonIsMerge = false;

		var ugentIsMerge = buttonsToolbar.getComponent("cboUrgentIsByDeptMerge").getValue();
		if(ugentIsMerge == 1 || ugentIsMerge == true)
			ugentIsMerge = false;
		else
			ugentIsMerge = true;
		var reaServerLabcCode = JcallShell.REA.System.CENORG_CODE;
		if(!reaServerLabcCode) reaServerLabcCode = "";
		var labcName = JcallShell.REA.System.CENORG_NAME;
		var params = {
			"idStr": idStr,
			"commonIsMerge": commonIsMerge,
			"ugentIsMerge": ugentIsMerge,
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