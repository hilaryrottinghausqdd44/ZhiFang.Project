/**
 * @description 订单上传平台
 * @author longfc
 * @version 2017-11-21
 */
Ext.define('Shell.class.rea.client.order.uploadplatform.App', {
	extend: 'Shell.class.rea.client.order.basic.App',

	title: '订单上传',
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
		me.OrderGrid = Ext.create('Shell.class.rea.client.order.uploadplatform.OrderGrid', {
			header: false,
			itemId: 'OrderGrid',
			region: 'west',
			width: 325,
			split: true,
			collapsible: false,
			collapsed: false,
			animCollapse: false,
			animate: false
		});
		me.EditPanel = Ext.create('Shell.class.rea.client.order.uploadplatform.EditPanel', {
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
			itemId: "btnSync",
			hidden: true,
			text: '同步供应商货品编码',
			tooltip: '建议上传订单前先将订单货品与供应商货品进行货品编码同步',
			handler: function() {
				//me.onSync();
			}
		}, '-', {
			xtype: 'button',
			iconCls: 'button-check',
			itemId: "btnUpload",
			text: '订单上传',
			tooltip: '将当前选择订单上传平台或第三方系统',
			//disabled:true,
			handler: function() {
				var orderUploadeType = "" + JcallShell.REA.RunParams.Lists.OrderUploadeType.Value;
				//判断是上传到平台还是上传给第三方业务接口
				if (orderUploadeType == "2") {
					//数据库是否独立部署: 1: 是;2: 否;
					var isDeploy = JShell.REA.RunParams.Lists["ReaDataBaseIsDeploy"].Value;
					if (isDeploy == "1") {
						me.onUploadOfDeploy();
					} else if (isDeploy == "2") {
						me.onUploadPlatform();
					}
				} else if (orderUploadeType == "3") {
					me.onUploadOtherInterface();
				} else if (orderUploadeType == "null") {
					JShell.Msg.error('获取订单上传类型信息为空！');
				}
			}
		}, {
			xtype: 'button',
			iconCls: 'button-cancel',
			itemId: "btnCancelUpload",
			text: '取消上传',
			tooltip: '将已上传平台的订单取消上传',
			//disabled:true,
			handler: function() {
				//数据库是否独立部署: 1: 是;2: 否;
				var isDeploy = JShell.REA.RunParams.Lists["ReaDataBaseIsDeploy"].Value;
				if (isDeploy == "1") {
					//JShell.Msg.alert('独立部署的取消上传功能未实现', null, 2000);
					me.onCancelOfDeploy();
				} else if (isDeploy == "2") {
					me.onCancelUpload();
				}
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
		//系统运行参数"订单上传"
		JShell.REA.RunParams.getRunParamsValue("OrderUploadeType", false, function() {
			var orderUploadeType = "" + JcallShell.REA.RunParams.Lists.OrderUploadeType.Value;
			//判断是上传到平台还是上传给第三方业务接口
			if (orderUploadeType != "2") {
				me.setBtnDisabled("btnCancelUpload", true);
			}
		});
		//系统运行参数"数据库是否独立部署"
		JShell.REA.RunParams.getRunParamsValue("ReaDataBaseIsDeploy", false, null);
		//系统运行参数"供应商确认订单时是否需要强制校验货品编码" 1:是;2:否;(上传订单时需要称将值保存到订单里)
		JShell.REA.RunParams.getRunParamsValue("OrderConfirmIsVerifyGoodsNo", false, null);
	},
	clearData: function(record) {
		var me = this;
		//me.setBtnDisabled("btnUpload", true);
	},
	loadData: function(record) {
		var me = this;
		me.isShow(record);
	},
	isShow: function(record) {
		var me = this;
		me.callParent(arguments);
		//me.setBtnDisabled("btnUpload", true);
	},
	/**@description 平台编码同步按钮点击处理方法*/
	onSync: function() {
		var me = this;
		var records = me.OrderGrid.getSelectionModel().getSelection();
		if (records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
	},
	/**@description 订单上传按钮点击处理方法(客户端与平台为同一数据库)*/
	onUploadPlatform: function() {
		var me = this;
		var records = me.OrderGrid.getSelectionModel().getSelection();
		if (records.length <= 0) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var info = "",
			idStr = "";
		Ext.Array.forEach(records, function(item, index, allItem) {
			idStr = idStr + item.get("ReaBmsCenOrderDoc_Id") + ",";
			var reaServerCompCode = item.get("ReaBmsCenOrderDoc_ReaServerCompCode");
			var IOFlag = item.get("ReaBmsCenOrderDoc_IOFlag");
			if (IOFlag == "1") {
				idStr = "";
				info = "订单号为:" + item.get("ReaBmsCenOrderDoc_OrderDocNo") + "已上传,请不要重复上传!";
				return false;
			}
		});
		if (info) {
			JShell.Msg.error(info);
			return;
		}
		if (!me.BUTTON_CAN_CLICK) return;

		if (idStr) {
			idStr = idStr.substring(0, idStr.length - 1);
			//系统运行参数"供应商确认订单时是否需要强制校验货品编码" 1:是;2:否;(上传订单时需要称将值保存到订单里)
			var isVerifyProdGoodsNo = JcallShell.REA.RunParams.Lists.OrderConfirmIsVerifyGoodsNo.Value == "1" ? true : false;
			var params = {
				"idStr": idStr,
				"isVerifyProdGoodsNo": isVerifyProdGoodsNo
			};
			if (!params) return false;
			var url = "/ZFReaRestfulService.svc/RS_UDTO_UpdateReaBmsCenOrderDocOfUploadByIdStr";
			url = JShell.System.Path.ROOT + url;

			me.showMask("订单上传中...");
			me.BUTTON_CAN_CLICK = false; //不可点击
			JShell.Server.post(url, JShell.JSON.encode(params), function(data) {
				me.hideMask();
				me.BUTTON_CAN_CLICK = true;
				if (data.success) {
					JShell.Msg.alert('订单上传成功', null, 2000);
					me.OrderGrid.onSearch();
				} else {
					JShell.Msg.error('订单上传失败！' + data.msg);
				}
			});
		}
	},
	/**@description 订单取消上传按钮点击处理方法(客户端与平台为同一数据库)*/
	onCancelUpload: function() {
		var me = this;
		var records = me.OrderGrid.getSelectionModel().getSelection();
		if (records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		if (!me.BUTTON_CAN_CLICK) return;

		JShell.Msg.confirm({
			title: '<div style="text-align:center;">订单取消上传操作</div>',
			msg: '订货方备注',
			closable: false,
			multiline: true //多行输入框
		}, function(but, text) {
			if (but != "ok") return;

			var memo = text;
			if (memo) {
				memo = memo.replace(/\\/g, '&#92');
				memo = memo.replace(/[\r\n]/g, '<br />');
			}
			var id = records[0].get("ReaBmsCenOrderDoc_Id");
			var entity = {
				"Id": id,
				"IOFlag": 2,
				"Status": 7, //取消上传
				"LabMemo": memo
			};
			//系统运行参数"供应商确认订单时是否需要强制校验货品编码" 1:是;2:否;(上传订单时需要称将值保存到订单里)
			var isVerifyProdGoodsNo = JcallShell.REA.RunParams.Lists.OrderConfirmIsVerifyGoodsNo.Value == "1" ? true :
				false;
			var params = {
				"entity": entity,
				"fields": "Id,IOFlag,Status,LabMemo",
				"isVerifyProdGoodsNo": isVerifyProdGoodsNo
			};
			var url = JShell.System.Path.ROOT + "/ZFReaRestfulService.svc/RS_UDTO_UpdateReaBmsCenOrderDocOfCancelUpload";
			me.BUTTON_CAN_CLICK = false; //不可点击
			me.showMask("订单上传中...");

			JShell.Server.post(url, JShell.JSON.encode(params), function(data) {
				me.hideMask();
				me.BUTTON_CAN_CLICK = true;
				if (data.success) {
					JShell.Msg.alert('订单取消上传成功', null, 2000);
					me.OrderGrid.onSearch();
				} else {
					JShell.Msg.error('订单取消上传失败！' + data.msg);
				}
			});
		});
	},
	/**@description 订单上传按钮点击处理方法(客户端与平台为不同的数据库)*/
	onUploadOfDeploy: function() {
		var me = this;
		var records = me.OrderGrid.getSelectionModel().getSelection();
		if (records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var IOFlag = records[0].get("ReaBmsCenOrderDoc_IOFlag");
		if (IOFlag == "1") {
			idStr = "";
			info = "当前选择的订单已被上传,请不要重复上传!";
			return false;
		}
		//访问BS平台的URL
		JShell.REA.RunParams.getRunParamsValue("BSPlatformURL", false, function(data) {
			if (JShell.REA.RunParams.Lists["BSPlatformURL"].Value) {
				me.uploadOfDeploy(records[0]);
			} else {
				JShell.Msg.error('客户端上传到平台的系统运行参数(BSPlatformURL)未配置！');
			}
		});
	},
	/**@description 订单上传处理方法(客户端与平台为不同的数据库)*/
	uploadOfDeploy: function(record) {
		var me = this;
		if (!me.BUTTON_CAN_CLICK) return;

		var orderId = record.get("ReaBmsCenOrderDoc_Id");
		//系统运行参数"供应商确认订单时是否需要强制校验货品编码" 1:是;2:否;(上传订单时需要称将值保存到订单里)
		var isVerifyProdGoodsNo = JcallShell.REA.RunParams.Lists.OrderConfirmIsVerifyGoodsNo.Value == "1" ? true : false;
		var empID = JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.USERID) || -1;
		var empName = JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.USERNAME);
		var params = {
			"appkey": "",
			"timestamp": "",
			"token": "",
			"orderId": orderId,
			"empID": empID,
			"empName": empName,
			"isVerifyProdGoodsNo": isVerifyProdGoodsNo,
			"platformUrl": JShell.REA.RunParams.Lists["BSPlatformURL"].Value,
		};
		var url = JShell.System.Path.ROOT + "/ZFReaRestfulService.svc/RS_Client_EditUploadPlatformReaOrderDocAndDtl";

		me.BUTTON_CAN_CLICK = false; //不可点击
		me.showMask("订单上传中...");
		JShell.Server.post(url, JShell.JSON.encode(params), function(data) {
			me.hideMask();
			me.BUTTON_CAN_CLICK = true;
			if (data.success) {
				me.OrderGrid.onSearch();
			} else {
				JShell.Msg.error('订单上传失败！' + data.msg);
			}
		});
	},
	/**@description 订单上传第三方接口*/
	onUploadOtherInterface: function(record) {
		var me = this;
		if (!me.BUTTON_CAN_CLICK) return;

		if (!record) {
			var records = me.OrderGrid.getSelectionModel().getSelection();
			if (records.length != 1) {
				JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
				return;
			}
			record = records[0];
		}
		var id = record.get("ReaBmsCenOrderDoc_Id");
		//判断第三方数据标志同步标志是同步成功或失败
		var isThirdFlag = "" + record.get("ReaBmsCenOrderDoc_IsThirdFlag");
		if (isThirdFlag == "1") {
			info = "订单号为:" + record.get("ReaBmsCenOrderDoc_OrderDocNo") + "已同步成功,请不要重复操作!";
			JShell.Msg.error(info);
			return;
		}
		me.BUTTON_CAN_CLICK = false; //不可点击
		me.showMask("订单上传中...");
		var url = JShell.System.Path.ROOT + "/ReaManageService.svc/RS_UDTO_OrderDocSaveToOtherSystem";
		url = url + "?id=" + id;
		JShell.Server.get(url, function(data) {
			me.hideMask();
			me.BUTTON_CAN_CLICK = true;
			if (data.success) {
				JShell.Msg.alert('订单上传成功', null, 2000);
				me.OrderGrid.onSearch();
			} else {
				JShell.Msg.error('订单上传失败！' + data.msg);
			}
		});
	},
	/**@description 订单取消上传按钮点击处理方法(客户端与平台为不同的数据库)*/
	onCancelOfDeploy: function() {
		var me = this;
		var records = me.OrderGrid.getSelectionModel().getSelection();
		if (records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var IOFlag = records[0].get("ReaBmsCenOrderDoc_IOFlag");
		if (IOFlag != "4"&&IOFlag != "1") {
			idStr = "";
			info = "当前选择的订单不能取消订单!";
			return false;
		}
		//访问BS平台的URL
		JShell.REA.RunParams.getRunParamsValue("BSPlatformURL", false, function(data) {
			if (JShell.REA.RunParams.Lists["BSPlatformURL"].Value) {
				me.cancelOfDeploy(records[0]);
			} else {
				JShell.Msg.error('客户端上传到平台的系统运行参数(BSPlatformURL)未配置！');
			}
		});
	},
	/**@description 订单取消上传处理方法(客户端与平台为不同的数据库)*/
	cancelOfDeploy: function(record) {
		var me = this;
		if (!me.BUTTON_CAN_CLICK) return;
	
		var orderId = record.get("ReaBmsCenOrderDoc_Id");
		//系统运行参数"供应商确认订单时是否需要强制校验货品编码" 1:是;2:否;(取消上传订单时需要称将值保存到订单里)
		var isVerifyProdGoodsNo = JcallShell.REA.RunParams.Lists.OrderConfirmIsVerifyGoodsNo.Value == "1" ? true : false;
		var empID = JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.USERID) || -1;
		var empName = JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.USERNAME);
		var params = {
			"appkey": "",
			"timestamp": "",
			"token": "",
			"orderId": orderId,
			"empID": empID,
			"empName": empName,
			"isVerifyProdGoodsNo": isVerifyProdGoodsNo,
			"platformUrl": JShell.REA.RunParams.Lists["BSPlatformURL"].Value,
		};
		var url = JShell.System.Path.ROOT + "/ZFReaRestfulService.svc/RS_Client_EditCancelUploadPlatformReaOrderDocAndDtl";
	
		me.BUTTON_CAN_CLICK = false; //不可点击
		me.showMask("订单取消上传中...");
		JShell.Server.post(url, JShell.JSON.encode(params), function(data) {
			me.hideMask();
			me.BUTTON_CAN_CLICK = true;
			if (data.success) {
				me.OrderGrid.onSearch();
			} else {
				JShell.Msg.error('订单取消失败！' + data.msg);
			}
		});
	}
});
