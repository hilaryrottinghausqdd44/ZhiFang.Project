/**
 * 客户端验收
 * @author longfc
 * @version 2017-12-15
 */
Ext.define('Shell.class.rea.client.confirm.reaorder.add.AddPanel', {
	extend: 'Shell.class.rea.client.confirm.add.AddPanel',

	title: '订单验收',

	/**新增服务地址*/
	addUrl: '/ReaManageService.svc/ST_UDTO_AddReaSaleDocConfirmOfOrder',
	/**修改服务地址*/
	editUrl: '/ReaManageService.svc/ST_UDTO_UpdateReaSaleDocConfirmOfOrder',
	
	/**验收主单ID*/
	PK: null,
	/**选择订单的所属供应商ID*/
	ReaCompID: null,
	/**选择好的订单ID*/
	DocOrderId: null,
	OTYPE: "reaorder",
	//按钮是否可点击
	BUTTON_CAN_CLICK:true,
	
	/**创建功能 按钮栏*/
	createButtonItems: function() {
		var me = this;
		var items = me.callParent(arguments);
		if(!items) {
			items = [];
		}
		items.unshift({
			iconCls: 'button-add',
			name: "btnChoseOrder",
			itemId: "btnChoseOrder",
			text: '订单选择',
			tooltip: '订单选择',
			hidden: !me.PK ? false : true,
			disabled: (!me.DocOrderId ? false : true),
			handler: function() {
				if(me.formtype == "edit") {
					JShell.Msg.alert('请将当前选择的订单验收完再选择!', null, 2000);
					return;
				}
				if(me.DtlGrid.store.getCount() > 0) {
					JShell.Msg.confirm({
						title: '<div style="text-align:center;">订单货品选择提示</div>',
						msg: '是否清空当前选择的订单并重新选择?',
						closable: true,
					}, function(but, text) {
						if(but != "ok") return;
						
						me.DtlGrid.DocOrderId = null;
						me.DtlGrid.fireEvent('choseOrderClick', me);
						me.DtlGrid.ReaOrderDtlOfConfirmVOList = [];
						me.DtlGrid.store.removeAll();
						me.DtlGrid.showChooseOrder();
					});
				} else {
					me.DtlGrid.fireEvent('choseOrderClick', me);
					me.DtlGrid.showChooseOrder();
				}
			}
		},'-', {
			iconCls: 'file-excel',
			name: "btnImportExcel",
			itemId: "btnImportExcel",
			text: '订单供货导入',
			tooltip: '将按订单供货的Excel供货信息导入',
			hidden: !me.PK ? false : true,
			style: {
				marginRight: "5px"
			},
			handler: function() {
				if(me.DocOrderId) {
					JShell.Msg.confirm({
						title: '<div style="text-align:center;">订单供货导入</div>',
						msg: '当前已经选择有待验收订单,是否重新选择订单供货导入?',
						closable: false,
						multiline: false //多行输入框
					}, function(but, text) {
						if(but != "ok") return;
						me.onSupplyImportExcel();
					});
				} else {
					me.onSupplyImportExcel();
				}
			}
		}, '-');
		return items;
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var TotalPrice = me.DocForm.getComponent('ReaBmsCenSaleDocConfirm_TotalPrice');
		//新增选择订单后联动处理
		me.DtlGrid.on({
			onSetConfirmInfo: function(g, objValue) {
				var readOnly=false;
				if(objValue) {
					me.DocForm.getForm().setValues(objValue);
					readOnly=true;
				}
				me.DocForm.setCompReadOnlys(readOnly);
			},
			onScanCodeShowDtl: function(grid, info,IsShowScan) {
				me.ShowDtlPanel(grid, info,IsShowScan);
			},
			changeSumTotal: function(total) {
				TotalPrice.setValue(total);
			},
			choseOrderClick: function() {
				var ComId = me.DocForm.getComponent('ReaBmsCenSaleDocConfirm_ReaCompID').getValue();
				var ComName = me.DocForm.getComponent('ReaBmsCenSaleDocConfirm_ReaCompanyName').getValue();
				me.DtlGrid.ReaCompID = ComId;
				me.DtlGrid.ReaCompCName = ComName;
			}
		});
		//供应商选择改变后
		me.DocForm.on({
			reacompcheck: function(v, objValue) {
				me.DtlGrid.ReaCompID = objValue["ReaCompID"];
				me.DtlGrid.ReaCompCName = objValue["ReaCompCName"];
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.addEvents('save');
		//自定义按钮功能栏
		me.dockedItems = me.createButtonToolbarItems();
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.DtlGrid = Ext.create('Shell.class.rea.client.confirm.reaorder.add.DtlGrid', {
			header: false,
			itemId: 'DtlGrid',
			region: 'center',
			defaultLoad: false,
			split: true,
			collapsible: true,
			collapsed: false,
			formtype: me.formtype
		});
		me.DocForm = Ext.create('Shell.class.rea.client.confirm.reaorder.add.DocForm', {
			header: false,
			itemId: 'DocForm',
			region: 'north',
			width: me.width,
			height: 100,
			split: true,
			collapsible: true,
			collapsed: false,
			formtype: me.formtype
		});
		var appInfos = [me.DtlGrid, me.DocForm];
		return appInfos;
	},
	isAdd: function() {
		var me = this;
		me.PK = null;
		me.formtype = "add";

		me.DocForm.PK = null;
		me.DocForm.Status = "0";
		me.DocForm.isAdd();
		me.DocForm.getComponent('buttonsToolbar').hide();

		me.DtlGrid.PK = null;
		me.DtlGrid.ReaCompID = null;
		me.DtlGrid.DocOrderId = null;
		me.DtlGrid.formtype = "add";
		me.DtlGrid.defaultWhere = "";
		me.DtlGrid.Status = "0";
		me.DtlGrid.store.removeAll();
		me.DtlGrid.enableControl();
	},
	isEdit: function(id) {
		var me = this;
		if(id) me.PK = id;
		me.formtype = "edit";

		me.DocForm.PK = me.PK;
		me.DocForm.Status = "0";
		me.DocForm.isEdit(me.PK);
		me.DocForm.getComponent('buttonsToolbar').hide();
		me.setReaCompNameReadOnly(true);

		me.DtlGrid.PK = me.PK;
		me.DtlGrid.ReaCompID = me.ReaCompID;
		me.DtlGrid.DocOrderId = me.DocOrderId;
		me.DtlGrid.formtype = "edit";
		//只显示暂存未验收的验收货品明细
		me.DtlGrid.defaultWhere = "reabmscensaledtlconfirm.Status=0 and reabmscensaledtlconfirm.SaleDocConfirmID=" + me.PK;
		me.DtlGrid.Status = "0";
		me.DtlGrid.store.removeAll();
		me.DtlGrid.enableControl();
		me.DtlGrid.onSearch();
	},
	clearData: function() {
		var me = this;
		me.ReaCompID = null;
		me.DocOrderId = null;
		me.DocForm.isAdd();

		me.DtlGrid.ReaCompID = null;
		me.DtlGrid.DocOrderId = null;
		me.DtlGrid.store.removeAll();
	},
	onSave: function(status, confirmData) {
		var me = this;

		var docEntity = me.formtype == 'add' ? me.DocForm.getAddParams() : me.DocForm.getEditParams();
		var entity = docEntity.entity;
		entity.Status = status;
		var params = {
			"entity": entity,
			"secAccepterType": me.secAccepterType,
			"secAccepterAccount": "",
			"secAccepterPwd": "",
			"codeScanningMode": me.DtlGrid.CodeScanningMode
		};
		if (confirmData) {
			//secAccepterType：默认本实验室(1),供应商(2),供应商或实验室(3)
			var accepterMemoValue = confirmData.AcceptMemo;
			if (accepterMemoValue) {
				accepterMemoValue = accepterMemoValue.replace(/"/g, "");
			}
			if (confirmData.Account) confirmData.Account = confirmData.Account.replace(/"/g, '');
			if (confirmData.Pwd) confirmData.Pwd = confirmData.Pwd.replace(/"/g, '');
		
			params.secAccepterAccount = confirmData.Account;
			params.secAccepterPwd = confirmData.Pwd;
		
			//entity.InvoiceNo = invoiceNoValue;
			entity.AcceptMemo = accepterMemoValue;
			docEntity.fields = docEntity.fields + ",AcceptMemo";
		}

		var dtParams = me.DtlGrid.getSaveParams(status);
		switch(me.formtype) {
			case "add":
				params.dtAddList = dtParams.dtAddList;
				if(!params.dtAddList) params.dtAddList = [];
				if(!params.dtAddList || params.dtAddList.length <= 0) {
					JShell.Msg.error("获取验收货品明细信息为空,不能保存!");
					return;
				}
				break;
			case "edit":
				params.fields = docEntity.fields;
				params.dtAddList = dtParams.dtAddList;
				if(!params.dtAddList) params.dtAddList = [];

				params.dtEditList = dtParams.dtEditList;
				if(!params.dtEditList) params.dtEditList = [];

				var fieldsDtl = me.DtlGrid.getUpdateFields();
				params.fieldsDtl = fieldsDtl;
				if(params.dtAddList.length <= 0 && params.dtEditList.length <= 0) {
					JShell.Msg.error("获取验收货品明细信息为空,不能保存!");
					return;
				}
				break;
			default:
				break;
		}
		params = Ext.JSON.encode(params);
		if(!params) {
			JShell.Msg.error("封装验收信息出错,不能保存!");
			return;
		}
		
		if (!me.BUTTON_CAN_CLICK) return;
		var url = me.formtype == 'add' ? me.addUrl : me.editUrl;
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		me.showMask(me.DocForm.saveText); //显示遮罩层
		me.BUTTON_CAN_CLICK = false; //不可点击
		JShell.Server.post(url, params, function(data) {
			me.hideMask(); //隐藏遮罩层
			me.BUTTON_CAN_CLICK = true;
			if(data.success) {
				//先清空列表信息
				me.DtlGrid.store.removeAll();
				
				me.PK = me.formtype == 'add' ? data.value.id : me.PK;
				var isStorage = me.getcboIsStorageValue();
				if(entity.Status == "0") isStorage = false;
				//关掉货品弹出提示窗体
				me.DtlGrid.hideTimes = 0;
				var dtlWin = Ext.WindowManager.get(me.OTYPE);
				if(dtlWin) dtlWin.close();
				me.fireEvent('save', me, me.PK, isStorage);
				JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.DocForm.hideTimes);
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	},
	onSupplyImportExcel: function() {
		var me = this;
		me.clearData();

		JShell.Win.open('Shell.class.rea.client.confirm.reaorder.add.UploadPanel', {
			formtype: 'add',
			resizable: false,
			listeners: {
				save: function(p, result) {
					p.close();
					var resultDataValue = "";
					if(result && result.ResultDataValue)
						resultDataValue = result.ResultDataValue;
					if(resultDataValue) {
						//console.log(result.ResultDataValue);
						resultDataValue=Ext.JSON.decode(resultDataValue.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, ''));
						//resultDataValue = Ext.JSON.decode(resultDataValue);
						if(resultDataValue.list.length > 0) {
							var record = resultDataValue.list[0];
							//验收主表单赋值
							var objValue = {
								"ReaBmsCenSaleDocConfirm_OrderDocID": record["ReaOrderDtlOfConfirmVO_OrderDocID"],
								"ReaBmsCenSaleDocConfirm_OrderDocNo": record["ReaOrderDtlOfConfirmVO_OrderDocNo"], //订货总单号	
								"ReaBmsCenSaleDocConfirm_CompID": record["ReaOrderDtlOfConfirmVO_ReaCompID"],
								"ReaBmsCenSaleDocConfirm_CompanyName": record["ReaOrderDtlOfConfirmVO_CompanyName"],
								"ReaBmsCenSaleDocConfirm_ReaCompID": record["ReaOrderDtlOfConfirmVO_ReaCompID"],
								"ReaBmsCenSaleDocConfirm_ReaCompanyName": record["ReaOrderDtlOfConfirmVO_CompanyName"],
								"ReaBmsCenSaleDocConfirm_ReaServerCompCode": record["ReaOrderDtlOfConfirmVO_ReaServerCompCode"],
								"ReaBmsCenSaleDocConfirm_ReaCompCode": record["ReaOrderDtlOfConfirmVO_ReaCompCode"]
							};
							me.DocForm.getForm().setValues(objValue);
							me.DocOrderId = record["ReaOrderDtlOfConfirmVO_OrderDocID"];
							me.ReaCompID = record["ReaOrderDtlOfConfirmVO_ReaCompID"];
							//验收明细列表赋值
							me.DtlGrid.DocOrderId = record["ReaOrderDtlOfConfirmVO_OrderDocID"];
							me.DtlGrid.ReaCompID = record["ReaOrderDtlOfConfirmVO_ReaCompID"];
							me.DtlGrid.ReaCompCName = record["ReaOrderDtlOfConfirmVO_CompanyName"];
							me.DtlGrid.ReaOrderDtlOfConfirmVOList = resultDataValue.list;
							me.DtlGrid.addRecordsOfOrderDtl(resultDataValue.list);
						} else {
							JShell.Msg.alert("获取订单明细信息为空!");
						}
					} else {
						me.clearData();
					}
				}
			}
		}).show();
	}
});