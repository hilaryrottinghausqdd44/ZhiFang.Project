/**
 * 申请并审核-客户端订单申请
 * @author longfc
 * @version 2021-01-13
 */
Ext.define('Shell.class.rea.client.order.applyandreview.App', {
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
		me.OrderGrid = Ext.create('Shell.class.rea.client.order.applyandreview.OrderGrid', {
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
		me.EditPanel = Ext.create('Shell.class.rea.client.order.applyandreview.EditPanel', {
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

		items.push('-', {
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
			//hidden:true,
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
		//系统运行参数"供应商确认订单时是否需要强制校验货品编码" 1:是;2:否;(上传订单时需要称将值保存到订单里)
		JShell.REA.RunParams.getRunParamsValue("OrderConfirmIsVerifyGoodsNo", false, null);
		//系统运行参数"数据库是否独立部署"
		JShell.REA.RunParams.getRunParamsValue("ReaDataBaseIsDeploy", false, null);
		////订单上传类型(1:不上传;2:上传平台;3:上传第三方系统;4:上传平台及上传第三方系统;)
		JcallShell.REA.RunParams.getRunParamsValue("OrderUploadeType", false, function(data) {
			if (data && data.success == true) {
				me.setBtnUpload();
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
			case "1": //已申请
				me.setBtnDisabled("btnCheck", false);
				me.setBtnDisabled("btnUnCheck", false);
				break;
			case "7": //取消上传
				me.setBtnDisabled("btnCheck", false);
				break;
			case "11": //审批退回，该状态下的数据可以点击‘审核通过’和‘审核退回’按钮
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
		me.setBtnDisabled("btnTempSave", true);
		me.setBtnDisabled("btnSave", true);
		var status = record.get("ReaBmsCenOrderDoc_Status");
		switch (status) {
			case "1": //已申请
				me.setBtnDisabled("btnCheck", false);
				me.setBtnDisabled("btnUnCheck", false);
				break;
			case "3": //审核通过
				if (IOFlag != 1)
					me.setBtnDisabled("btnUpload", false);
				break;

			default:
				//明细的新增,删除,保存默认禁用
				me.EditPanel.OrderDtlGrid.buttonsDisabled = true;
				me.EditPanel.OrderDtlGrid.setButtonsDisabled(true);
				break;
		}
	},
	loadData: function(record) {
		var me = this;
		me.setBtnDisabled("btnCheck", true);
		me.setBtnDisabled("btnUnCheck", true);
		me.setBtnDisabled("btnSave", true);
		me.setBtnDisabled("btnUpload", true);
		//临时,审核退回可以修改
		/**需求调整：11表示审批退回，当有审批退回时的数据，
		点击时要能对数据进行修改和审核通过这种操作
		加上11后可以实现数据修改*/
		var status = "" + record.get("ReaBmsCenOrderDoc_Status");
		if (status == "0" || status == "2"||status == "1" || status == "7" || status == "11") {
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
	/**@description 审核按钮点击处理方法*/
	onCheckClick: function() {
		var me = this;
		if (!me.BUTTON_CAN_CLICK) return;

		var records = me.OrderGrid.getSelectionModel().getSelection();
		if (records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var DeleteFlag = "" + records[0].get("ReaBmsCenOrderDoc_DeleteFlag");
		if (DeleteFlag == "false" || DeleteFlag == false) DeleteFlag = '0';
		if (DeleteFlag == "1" || DeleteFlag == "true") {
			JShell.Msg.error("当前订单已被禁用!不能审核!");
			return;
		}

		//已申请的状态可以审核
		var status = records[0].get("ReaBmsCenOrderDoc_Status");
		var StatusEnum = JShell.REA.StatusList.Status[me.OrderGrid.StatusKey].Enum;
		var statusName = "";
		if (StatusEnum)
			statusName = StatusEnum[status];
		//申请,取消上传审批退回
		if (status != "1" && status != "7" && status != "11") {
			JShell.Msg.error("当前订单状态为【" + statusName + "】,不能审核!");
			return;
		}
		if (me.EditPanel.OrderDtlGrid.store.getCount() == 0) {
			JShell.Msg.error("当前订单货品明细为空!不能审核!");
			return;
		}

		var totalGoodsQty = 0;
		var info = "";
		me.EditPanel.OrderDtlGrid.store.each(function(record) {
			var goodsQty = record.get("ReaBmsCenOrderDtl_GoodsQty");
			if (!goodsQty) goodsQty = 0;
			goodsQty = parseFloat(goodsQty);
			var price = record.get("ReaBmsCenOrderDtl_Price");
			if (!price) price = 0;
			price = parseFloat(price);
			if (goodsQty <= 0) {
				info = "货品名称为:" + record.get("ReaBmsCenOrderDtl_ReaGoodsName") + "申请数量小于等于0";
				return false;
			}
			if (price < 0) {
				info = "货品名称为:" + record.get("ReaBmsCenOrderDtl_ReaGoodsName") + "单价小于0";
				return false;
			}

			totalGoodsQty += goodsQty;
			if (totalGoodsQty > 0) return false;
		});

		if (info) {
			JShell.Msg.error(info);
			return;
		}
		if (totalGoodsQty == 0) {
			JShell.Msg.error("当前订单明细的申请数量为0!不能保存!");
			return;
		}

		//处理意见
		JShell.Msg.confirm({
			title: '<div style="text-align:center;">审核通过操作</div>',
			msg: '审核意见',
			closable: false,
			multiline: true //多行输入框
		}, function(but, text) {
			if (but != "ok") return;

			var checkMemo = text;
			if (checkMemo) {
				checkMemo = checkMemo.replace(/\\/g, '&#92');
				checkMemo = checkMemo.replace(/[\r\n]/g, '<br />');
			}
			me.setFormType("edit");
			//需要保存主单及明细
			var params = me.EditPanel.getSaveParams();
			if (!params.entity.Id) params.entity.Id = records[0].get("ReaBmsCenOrderDoc_Id");
			params.entity.Status = 3;
			params.entity.CheckMemo = checkMemo;
			//订单审核通过同时直接订单上传(1:是;2:否;)
			var checkIsUploaded = "" + JcallShell.REA.RunParams.Lists.OrderCheckIsUploaded.Value;
			//订单上传类型(1:不上传;2:上传平台;3:上传第三方系统;4:上传平台及上传第三方系统;)
			if (JcallShell.REA.RunParams.Lists.OrderUploadeType.Value == "1")
				checkIsUploaded = "2";

			params.checkIsUploaded = checkIsUploaded;
			if (!params.fields) {
				params.fields = "Id,Status";
			}
			if (params.entity.TotalPrice && params.fields.indexOf("TotalPrice") < 0) {
				params.fields += ",TotalPrice";
			}
			if (params.fields.indexOf("CheckMemo") < 0) params.fields += ",CheckMemo";
			if (!params.dtEditList) params.dtEditList = [];

			me.showMask("审核中...");
			me.BUTTON_CAN_CLICK = false; //不可点击			
			me.onSaveOfUpdate(params, function(data) {
				me.hideMask();
				me.BUTTON_CAN_CLICK = true;

				if (data.success) {
					if (checkIsUploaded == "1") { //审核成功后需要上传
						var orderUploadeType = "" + JcallShell.REA.RunParams.Lists.OrderUploadeType.Value;
						if (orderUploadeType == "2") { //上传平台供货商
							//如果客户端与平台为不在同一数据库,并且订单审核通过后需要上传到平台
							//数据库是否独立部署: 1: 是;2: 否;
							var isDeploy = "" + JcallShell.REA.RunParams.Lists["ReaDataBaseIsDeploy"].Value;
							if (isDeploy == "1") { //独立部署
								me.onUploadOfDeploy();
							} else if (isDeploy == "2") { //客户端与平台供货商同一数据库
								JShell.Msg.alert('确认审核成功', null, 1000);
								me.OrderGrid.onSearch();
							}
						} else if (orderUploadeType == "3") { //上传给第三方业务接口
							me.onUploadOtherInterface(records[0]);
						}
					} else {
						me.OrderGrid.onSearch();
					}
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
		if (records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		//申请
		var status = records[0].get("ReaBmsCenOrderDoc_Status");
		var StatusEnum = JShell.REA.StatusList.Status[me.OrderGrid.StatusKey].Enum;
		var statusName = "";
		if (StatusEnum)
			statusName = StatusEnum[status];
		//申请,审批退回
		if (status != "1" && status != "11") {
			JShell.Msg.error("当前订单状态为【" + statusName + "】,不能审核退回!");
			return;
		}

		if (me.EditPanel.OrderDtlGrid.store.getCount() == 0) {
			JShell.Msg.error("当前订单货品明细为空!不能审核退回!");
			return;
		}
		if (!me.BUTTON_CAN_CLICK) return;

		//处理意见
		JShell.Msg.confirm({
			title: '<div style="text-align:center;">审核退回操作</div>',
			msg: '审核意见',
			closable: false,
			multiline: true //多行输入框
		}, function(but, text) {
			if (but != "ok") return;

			var checkMemo = text;
			if (checkMemo) {
				checkMemo = checkMemo.replace(/\\/g, '&#92');
				checkMemo = checkMemo.replace(/[\r\n]/g, '<br />');
			}
			me.setFormType("edit");
			//需要保存主单及明细
			var params = me.EditPanel.getSaveParams();
			if (!params.entity.Id) params.entity.Id = records[0].get("ReaBmsCenOrderDoc_Id");
			params.entity.Status = 2;
			params.entity.CheckMemo = checkMemo;
			if (!params.fields) {
				params.fields = "Id,Status";
			}
			if (params.entity.TotalPrice && params.fields.indexOf("TotalPrice") < 0) {
				params.fields += ",TotalPrice";
			}
			if (params.fields.indexOf("CheckMemo") < 0) params.fields += ",CheckMemo";
			if (!params.dtEditList) params.dtEditList = [];
			me.showMask("审核中...");
			me.BUTTON_CAN_CLICK = false; //不可点击	
			me.onSaveOfUpdate(params, function(data) {
				me.hideMask();
				me.BUTTON_CAN_CLICK = true;
				if (data.success) {
					JShell.Msg.alert('审核退回成功', null, 1000);
					me.OrderGrid.onSearch();
				} else {
					JShell.Msg.error('审核退回失败！' + data.msg);
				}
			});
		});
	},
	setBtnUpload: function() {
		var me = this;
		var buttonsToolbar = me.getComponent("buttonsToolbar");
		var btnInfo = buttonsToolbar.getComponent("btnUpload");
		//OrderUploadeType:1:不上传;2:上传平台;3:上传第三方系统;4:上传平台及上传第三方系统;
		var visible = JcallShell.REA.RunParams.Lists.OrderUploadeType.Value == "1" ? false : true;
		btnInfo.setVisible(visible);
		//系统运行参数"供应商确认订单时是否需要强制校验货品编码"1:是;2:否;(上传订单时需要称将值保存到订单里)
		JShell.REA.RunParams.getRunParamsValue("OrderConfirmIsVerifyGoodsNo", false, null);
	},
	/**@description 订单上传按钮点击处理方法(客户端与平台为同一数据库)*/
	onUploadPlatform: function() {
		var me = this;
		if (!me.BUTTON_CAN_CLICK) return;

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
				info = "当前选择的订单存在已上传的订单,请不要重复上传!";
				return false;
			}
		});
		if (info) {
			JShell.Msg.error(info);
			return;
		}
		if (idStr) {
			idStr = idStr.substring(0, idStr.length - 1);
			var isVerifyProdGoodsNo = false;
			isVerifyProdGoodsNo = JcallShell.REA.RunParams.Lists.OrderConfirmIsVerifyGoodsNo.Value == "1" ? true : false;
			var params = {
				"idStr": idStr,
				"isVerifyProdGoodsNo": isVerifyProdGoodsNo
			};
			if (!params) return false;
			var url = "/ZFReaRestfulService.svc/RS_UDTO_UpdateReaBmsCenOrderDocOfUploadByIdStr";
			url = JShell.System.Path.ROOT + url;

			me.showMask("上传中...");
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
	/**@description 订单上传按钮点击处理方法(客户端与平台为不同的数据库)*/
	onUploadOfDeploy: function() {
		var me = this;
		if (!me.BUTTON_CAN_CLICK) return;

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
				me.BUTTON_CAN_CLICK = true;
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
		me.showMask("上传中...");
		me.BUTTON_CAN_CLICK = false; //不可点击	
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
			info = "订单号为:" + item.get("ReaBmsCenOrderDoc_OrderDocNo") + "已同步成功,请不要重复操作!";
			return;
		}
		var url = JShell.System.Path.ROOT + "/ReaManageService.svc/RS_UDTO_OrderDocSaveToOtherSystem";
		url = url + "?id=" + id;
		me.BUTTON_CAN_CLICK = false; //不可点击
		me.showMask("订单上传中...");
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
	}

});
