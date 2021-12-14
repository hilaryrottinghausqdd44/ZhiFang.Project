/**
 * @description 订单付款
 * @author longfc
 * @version 2019-01-03
 */
Ext.define('Shell.class.rea.client.order.pay.App', {
	extend: 'Shell.class.rea.client.order.basic.App',

	title: '订单付款',
	//按钮是否可点击
	BUTTON_CAN_CLICK:true,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.OrderGrid.on({
			select: function(RowModel, record) {
				me.loadData(record);
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
		me.OrderGrid = Ext.create('Shell.class.rea.client.order.pay.OrderGrid', {
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
		me.EditPanel = Ext.create('Shell.class.rea.client.order.pay.EditPanel', {
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

		if(items.length > 0) items.push('-');
		items.push({
			xtype: 'button',
			iconCls: 'button-check',
			itemId: "btnCheck",
			text: '确认付款',
			tooltip: '对当前选择的订单进行确认付款',
			handler: function() {
				me.onCheckClick();
			}
		}, {
			xtype: 'button',
			iconCls: 'button-reset',
			itemId: "btnUnCheck",
			text: '撤消确认',
			tooltip: '对当前选择的订单进行撤消确认付款',
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
	clearData: function(record) {
		var me = this;
		me.setBtnDisabled("btnCheck", true);
		me.setBtnDisabled("btnUnCheck", true);
	},
	loadData: function(record) {
		var me = this;
		me.setBtnDisabled("btnCheck", true);
		me.setBtnDisabled("btnUnCheck", true);
		me.isShow(record);
	},
	isShow: function(record) {
		var me = this;
		me.callParent(arguments);
		me.setFormType("show");
		var status = record.get("ReaBmsCenOrderDoc_Status");
		var payStaus = record.get("ReaBmsCenOrderDoc_PayStaus");
		switch(payStaus) {
			case "1": //未付款
				me.setBtnDisabled("btnCheck", false);
				me.setBtnDisabled("btnUnCheck", true);
				break;
			case "2": //已付款
				me.setBtnDisabled("btnCheck", true);
				me.setBtnDisabled("btnUnCheck", false);
				break;
			default:
				me.setBtnDisabled("btnCheck", false);
				me.setBtnDisabled("btnUnCheck", true);
				break;
		}
	},
	/**@description 确认付款按钮点击处理方法*/
	onCheckClick: function() {
		var me = this;
		var records = me.OrderGrid.getSelectionModel().getSelection();
		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var DeleteFlag = "" + records[0].get("ReaBmsCenOrderDoc_DeleteFlag");
		if(DeleteFlag == "false" || DeleteFlag == false) DeleteFlag = '0';
		if(DeleteFlag == "1" || DeleteFlag == "true") {
			JShell.Msg.error("当前订单已被禁用!不能进行确认付款处理!");
			return;
		}
		var payStaus = records[0].get("ReaBmsCenOrderDoc_PayStaus");
		//已付款
		if(payStaus == "2") {
			JShell.Msg.error("当前订单付款状态为【已付款】,不能进行确认付款处理!");
			return;
		}
		//处理意见
		JShell.Msg.confirm({
			title: '<div style="text-align:center;">确认付款操作</div>',
			msg: '付款备注',
			closable: false,
			multiline: true //多行输入框
		}, function(but, text) {
			if(but != "ok") return;

			var payMemo = text;
			if(payMemo) {
				payMemo = payMemo.replace(/\\/g, '&#92');
				payMemo = payMemo.replace(/[\r\n]/g, '<br />');
			}
			//需要保存主单及明细
			var entity = {
				"Id": records[0].get("ReaBmsCenOrderDoc_Id"),
				"Status": records[0].get("ReaBmsCenOrderDoc_Status"),
				"PayStaus": 2,
				"PayMemo": payMemo
			};
			me.onPaySave(entity);
		});
	},
	/**@description 撤消确认付款按钮点击处理方法*/
	onUnCheckClick: function() {
		var me = this;
		var records = me.OrderGrid.getSelectionModel().getSelection();
		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var DeleteFlag = "" + records[0].get("ReaBmsCenOrderDoc_DeleteFlag");
		if(DeleteFlag == "false" || DeleteFlag == false) DeleteFlag = '0';
		if(DeleteFlag == "1" || DeleteFlag == "true") {
			JShell.Msg.error("当前订单已被禁用!不能进行确认付款处理!");
			return;
		}
		var payStaus = records[0].get("ReaBmsCenOrderDoc_PayStaus");
		//未付款
		if(payStaus == "1") {
			JShell.Msg.error("当前订单付款状态为【未付款】,不能进行撤消确认付款处理!");
			return;
		}
		//处理意见
		JShell.Msg.confirm({
			title: '<div style="text-align:center;">撤消确认付款操作</div>',
			msg: '付款备注',
			closable: false,
			multiline: true //多行输入框
		}, function(but, text) {
			if(but != "ok") return;

			var payMemo = text;
			if(payMemo) {
				payMemo = payMemo.replace(/\\/g, '&#92');
				payMemo = payMemo.replace(/[\r\n]/g, '<br />');
			}
			//需要保存主单及明细
			var entity = {
				"Id": records[0].get("ReaBmsCenOrderDoc_Id"),
				"Status": records[0].get("ReaBmsCenOrderDoc_Status"),
				"PayStaus": 1,
				"PayMemo": payMemo
			};
			me.onPaySave(entity);
		});
	},
	onPaySave: function(entity) {
		var me = this;
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = 1;
		me.showMask(me.saveText); //显示遮罩层
		var url = JShell.System.Path.ROOT + "/ReaManageService.svc/RS_UDTO_UpdateReaBmsCenOrderDocByPay";
		var params = {
			entity: entity,
			fields: 'Id,PayStaus,PayUserId,PayUserCName,PayTime,PayMemo'
		};
		JShell.Server.post(url, Ext.JSON.encode(params), function(data) {
			me.hideMask();
			if(data.success) {
				me.OrderGrid.onSearch();
				JShell.Msg.alert('付款处理操作成功', null, 1000);
			} else {
				JShell.Msg.error('操作失败！' + data.msg);
			}
		});
	}
});