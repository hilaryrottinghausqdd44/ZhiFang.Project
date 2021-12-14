/**
 * @description 用户订单(给供应商查看客户端用户已上传的订单信息)
 * @author longfc
 * @version 2018-03-06
 */
Ext.define('Shell.class.rea.client.order.comp.App', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '用户订单',

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
		me.OrderGrid = Ext.create('Shell.class.rea.client.order.comp.OrderGrid', {
			header: false,
			itemId: 'OrderGrid',
			region: 'west',
			width: 325,
			split: true,
			collapsible: true,
			collapsed: false,
			animCollapse: false,
			animate: false
		});
		me.ShowPanel = Ext.create('Shell.class.rea.client.order.comp.ShowPanel', {
			header: false,
			itemId: 'ShowPanel',
			region: 'center',
			split: true,
			collapsible: true,
			collapsed: false,
			border: false
		});
		var appInfos = [me.OrderGrid, me.ShowPanel];
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
			itemId: "btnConfirm",
			text: '供应商确认',
			tooltip: '将当前选择订单供应商确认',
			//disabled: true,
			handler: function() {
				me.onConfirmClick();
			}
		}, {
			xtype: 'button',
			iconCls: 'button-cancel',
			itemId: "btnCancelConfirm",
			text: '取消确认',
			tooltip: '将已上传平台的订单取消确认',
			//disabled: true,
			handler: function() {
				me.onCancelConfirmClick();
			}
		});
		items.push( {
			xtype: 'button',
			iconCls: 'button-import',
			itemId: "btnImport",
			text: '订单转供单',
			tooltip: '从平台订货方的订单转换为供货单',
			handler: function() {
				me.onImportClick();
			}
		});
		// 新增用户订单上传NC
		items.push('-',{
			text: '订单上传至NC',
			tooltip: '订单上传至NC',
			iconCls: 'button-config',
			handler: function() {
				me.orderUpload();
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
		me.ShowPanel.on({
			onLaunchFullScreen: function() {
				me.OrderGrid.collapse();
			},
			onExitFullScreen: function() {
				me.OrderGrid.expand();
			}
		});
		//系统运行参数"数据库是否独立部署"
		JShell.REA.RunParams.getRunParamsValue("ReaDataBaseIsDeploy", false, null);
	},
	clearData: function(record) {
		var me = this;
	},
	loadData: function(record) {
		var me = this;
		me.isShow(record);
	},
	isShow: function(record) {
		var me = this;
		me.setFormType("show");
		me.ShowPanel.isShow(record, me.OrderGrid);
		me.ShowPanel.OrderDtlGrid.buttonsDisabled = true;
	},
	nodata: function(record) {
		var me = this;
		me.setFormType("show");
		me.ShowPanel.clearData();
		me.clearData();
	},
	setFormType: function(formtype) {
		var me = this;
		me.formtype = formtype;
		me.ShowPanel.formtype = formtype;
		me.ShowPanel.DocForm.formtype = formtype;
		me.ShowPanel.OrderDtlGrid.formtype = formtype;
	},
	/**@description 供应商确认按钮点击处理方法*/
	onConfirmClick: function() {
		var me = this;
		var records = me.OrderGrid.getSelectionModel().getSelection();
		if (records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var DeleteFlag = "" + records[0].get("ReaBmsCenOrderDoc_DeleteFlag");
		if (DeleteFlag == "false" || DeleteFlag == false) DeleteFlag = '0';
		if (DeleteFlag == "1" || DeleteFlag == "true") {
			JShell.Msg.error("当前订单已被禁用!不能操作!");
			return;
		}
		//订单的数据标志
		var IOFlag = records[0].get("ReaBmsCenOrderDoc_IOFlag");
		//已上传,取消确认
		if (IOFlag != "1" && IOFlag != "4") {
			var IOFlagEnum = JShell.REA.StatusList.Status[me.OrderGrid.IOFlagKey].Enum;
			var IOFlagName = "";
			if (IOFlagEnum)
				IOFlagName = IOFlagEnum[IOFlag];
			JShell.Msg.error("当前订单数据标志为【" + IOFlagName + "】,不能操作!");
			return;
		}
		//运行参数:供应商确认订单时是否需要校验(实验室的订单货品是否能在供应商的订货方货品关系里对照上)
		//如果需要校验,货品所属供货商标准编码不能为空
		JcallShell.REA.RunParams.getRunParamsValue("OrderConfirmIsVerifyGoodsNo", false, function(data) {
			if (data && data.success == false) {
				JShell.Msg.error('获取系统运行参数[供应商确认订单时是否需要强制校验货品编码]值失败！' + data.msg);
			} else {
				me.onConfirmSave(records[0], 8, 3);
			}
		});
	},
	/**@description 供应商取消确认按钮点击处理方法*/
	onCancelConfirmClick: function() {
		var me = this;
		var records = me.OrderGrid.getSelectionModel().getSelection();
		if (records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var DeleteFlag = "" + records[0].get("ReaBmsCenOrderDoc_DeleteFlag");
		if (DeleteFlag == "false" || DeleteFlag == false) DeleteFlag = '0';
		if (DeleteFlag == "1" || DeleteFlag == "true") {
			JShell.Msg.error("当前订单已被禁用!不能操作!");
			return;
		}
		//订单的数据标志
		var IOFlag = records[0].get("ReaBmsCenOrderDoc_IOFlag");
		//供应商确认
		if (IOFlag != "3") {
			var IOFlagEnum = JShell.REA.StatusList.Status[me.OrderGrid.IOFlagKey].Enum;
			var IOFlagName = "";
			if (IOFlagEnum)
				IOFlagName = IOFlagEnum[IOFlag];
			JShell.Msg.error("当前订单数据标志为【" + IOFlagName + "】,不能取消确认!");
			return;
		}
		me.onConfirmSave(records[0], 9, 4);
	},
	/**@description 保存处理方法*/
	onConfirmSave: function(record, status, ioFlag) {
		var me = this;

		var info = ioFlag == 3 ? "供应商确认" : "供应商取消确认";
		//处理意见
		JShell.Msg.confirm({
			title: '<div style="text-align:center;">' + info + '操作</div>',
			msg: '供货商备注',
			closable: false,
			multiline: true //多行输入框
		}, function(but, text) {
			if (but != "ok") return;
			var compMemo = text;
			if (compMemo) {
				compMemo = compMemo.replace(/\\/g, '&#92');
				compMemo = compMemo.replace(/[\r\n]/g, '<br />');
			}
			var id = record.get("ReaBmsCenOrderDoc_Id");
			var entity = {
				"Id": id,
				"IOFlag": ioFlag,
				"Status": status,
				"CompMemo": compMemo
			};
			var url = JShell.System.Path.ROOT + "/ZFReaRestfulService.svc/RS_UDTO_UpdateReaBmsCenOrderDocOfComp";

			var params = {
				"entity": entity,
				"fields": "Id,IOFlag,Status,CompMemo"
			};

			JShell.Server.post(url, JShell.JSON.encode(params), function(data) {
				if (data.success) {
					JShell.Msg.alert(info + '成功', null, 2000);
					me.OrderGrid.onSearch();
				} else {
					JShell.Msg.error(info + '失败！' + data.msg);
				}
			});
		});
	},
	/**订单转供单提取*/
	onImportClick: function() {
		var me = this;
		var records = me.OrderGrid.getSelectionModel().getSelection();
		if (records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var DeleteFlag = "" + records[0].get("ReaBmsCenOrderDoc_DeleteFlag");
		if (DeleteFlag == "false" || DeleteFlag == false) DeleteFlag = '0';
		if (DeleteFlag == "1" || DeleteFlag == "true") {
			JShell.Msg.error("当前订单已被禁用!不能操作!");
			return;
		}
		//订单的数据标志
		var IOFlag = records[0].get("ReaBmsCenOrderDoc_IOFlag");
		//供应商确认
		if (IOFlag != "3") {
			if(IOFlag!="5"){
				var IOFlagEnum = JShell.REA.StatusList.Status[me.OrderGrid.IOFlagKey].Enum;
				var IOFlagName = "";
				if (IOFlagEnum)
					IOFlagName = IOFlagEnum[IOFlag];
				JShell.Msg.error("当前订单数据标志为【" + IOFlagName + "】,不能订单转供单!");
				return;
			}
		}

		//数据库是否独立部署: 1: 是;2: 否;
		var isDeploy = JShell.REA.RunParams.Lists["ReaDataBaseIsDeploy"].Value;
		if (isDeploy == "1") {
			//访问BS平台的URL
			JcallShell.REA.RunParams.getRunParamsValue("BSPlatformURL", false, function(data) {
				if (data.success) {
					me.onOrderToSupplyOfDeploy();
				} else {
					JShell.Msg.error('获取系统运行参数"访问BS平台的URL"配置项失败！' + data.msg);
				}
			});
		} else if (isDeploy == "2") {
			me.onOrderToSupply(records[0]);
		}
	},
	/**提取订单转供货单(客户端与平台同一数据库)*/
	onOrderToSupply: function(rec) {
		var me = this;
		var id = rec.get("ReaBmsCenOrderDoc_Id");
		var url = JShell.System.Path.ROOT + "/ZFReaRestfulService.svc/RS_UDTO_AddReaBmsCenSaleDocOfOrderToSupply";
		var params = {
			"orderId": id
		};
		JShell.Server.post(url, JShell.JSON.encode(params), function(data) {
			if (data.success) {
				me.OrderGrid.onSearch();
			} else {
				JShell.Msg.error('提取订单转供货单失败！' + data.msg);
			}
		});
	},
	/**提取订单转供货单(客户端与平台不在同一数据库)*/
	onOrderToSupplyOfDeploy: function(rec) {
		var me = this;
	},
	/**新增用户订单上传至NC*/
	orderUpload: function() {
		var me = this;
		var records = me.OrderGrid.getSelectionModel().getSelection();
		if (records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var DeleteFlag = "" + records[0].get("ReaBmsCenOrderDoc_DeleteFlag");
		if (DeleteFlag == "false" || DeleteFlag == false) DeleteFlag = '0';
		if (DeleteFlag == "1" || DeleteFlag == "true") {
			JShell.Msg.error("当前订单已被禁用!不能操作!");
			return;
		}
		
		var id = records[0].get("ReaBmsCenOrderDoc_Id");
		var url = JShell.System.Path.ROOT + "/ReaCustomInterface.svc/RS_SendBmsCenOrderByInterface?orderDocId=" + id;
		
		JShell.Server.get(url, function(data) {
			if (data.success) {
				JShell.Msg.alert('订单上传至NC成功！', null, 1500);
			} else {
				JShell.Msg.error('订单上传至NC失败！' + data.msg);
			}
		});
	}
});
