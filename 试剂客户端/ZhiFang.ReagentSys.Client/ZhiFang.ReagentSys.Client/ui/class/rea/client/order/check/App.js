/**
 * @description 订单审核
 * @author longfc
 * @version 2017-11-17
 * 
 * 
 * @description 功能调整：审批退回调整
 * @author zq
 * @version 2020-11-9
 */
Ext.define('Shell.class.rea.client.order.check.App', {
	extend: 'Shell.class.rea.client.order.basic.App',
	
	title: '订单审核',
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
		me.OrderGrid = Ext.create('Shell.class.rea.client.order.check.OrderGrid', {
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
		me.EditPanel = Ext.create('Shell.class.rea.client.order.check.EditPanel', {
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

		if (items.length > 0) items.push('-');
		items.push({
			xtype: 'button',
			iconCls: 'button-check',
			itemId: "btnCheck",
			text: '审核通过',
			tooltip: '只对当前订单审核通过',
			handler: function() {
				JcallShell.REA.RunParams.getRunParamsValue("OrderCheckIsUploaded", false, function(data) {
					if (data && data.success == false) {
						JShell.Msg.error('获取系统运行参数[订单审核通过同时直接订单上传]值失败！' + data.msg);
					} else {
						me.onCheckClick();
					}
				});
			}
		}, {
			xtype: 'button',
			iconCls: 'button-reset',
			itemId: "btnUnCheck",
			text: '审核退回',
			tooltip: '只对当前订单审核退回',
			handler: function() {
				me.onUnCheckClick();
			}
		});

		items.push('-', {
			xtype: 'button',
			iconCls: 'button-save',
			itemId: "btnSave",
			text: '保存订单',
			tooltip: '编辑当前选中的订单',
			disabled: true,
			handler: function() {
				me.onSaveClick();
			}
		});
		//通过系统运行参数控制是否显示
		items.push('-', {
			xtype: 'button',
			iconCls: 'button-check',
			itemId: "btnUpload",
			text: '订单上传',
			tooltip: '将当前选择订单上传平台或第三方系统',
			disabled: true,
			hidden: true,
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
				}
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
		me.setBtnDisabled("btnCheck", true);
		me.setBtnDisabled("btnUnCheck", true);
		me.setBtnDisabled("btnSave", true);
		me.setBtnDisabled("btnUpload", true);
	},
	loadData: function(record) {
		var me = this;
		me.setBtnDisabled("btnCheck", true);
		me.setBtnDisabled("btnUnCheck", true);
		me.setBtnDisabled("btnSave", true);
		me.setBtnDisabled("btnUpload", true);
		//已申请,取消上传
		var status = record.get("ReaBmsCenOrderDoc_Status");
		/**需求调整：11表示审批退回，当有审批退回时的数据，
		点击时要能对数据进行修改和审核通过这种操作
		加上11后可以实现数据修改*/
		if (status == "1" || status == "7" || status == "11") {
			me.isEdit(record);
		} else {
			me.isShow(record);
		}
	},
	isEdit: function(record) {
		var me = this;
		me.callParent(arguments);

		me.setFormType("edit");
		me.setBtnDisabled("btnSave", false);
		var status = record.get("ReaBmsCenOrderDoc_Status");
		switch (status) {
			case "1": //已申请
				me.setBtnDisabled("btnCheck", false);
				me.setBtnDisabled("btnUnCheck", false);
				break;
			case "7": //取消上传（需求变更）
				me.setBtnDisabled("btnCheck", false);
				me.setBtnDisabled("btnUnCheck", false);
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
		var status = record.get("ReaBmsCenOrderDoc_Status");
		var IOFlag = record.get("ReaBmsCenOrderDoc_IOFlag");
		if (IOFlag) IOFlag = parseInt(IOFlag);
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
	/**@description 保存按钮点击处理方法*/
	onSaveClick: function() {
		var me = this;
		if (!me.BUTTON_CAN_CLICK) return;

		var records = me.OrderGrid.getSelectionModel().getSelection();
		if (records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}

		var totalGoodsQty = 0;
		me.EditPanel.OrderDtlGrid.store.each(function(record) {
			var goodsQty = record.get("ReaBmsCenOrderDtl_GoodsQty");
			if (!goodsQty) goodsQty = 0;
			totalGoodsQty += parseInt(goodsQty);
			if (totalGoodsQty > 0) return false;
		});
		if (totalGoodsQty == 0) {
			JShell.Msg.error("当前货品申请数量为0!不能保存!");
			return;
		}
		me.showMask("保存中...");
		me.BUTTON_CAN_CLICK = false; //不可点击	

		me.setFormType("edit");
		//需要保存主单及明细
		var params = me.EditPanel.getSaveParams();
		params.entity.Status = me.EditPanel.OrderDtlGrid.Status;
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
		if (status != "1" && status != "11" && status!="7") {
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
		if(!me.reaReportClass || me.reaReportClass != "Frx") {
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
		if(!me.reaReportClass || me.reaReportClass != "Excel") {
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
