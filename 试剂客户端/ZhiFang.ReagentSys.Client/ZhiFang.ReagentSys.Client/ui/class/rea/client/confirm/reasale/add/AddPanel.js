/**
 * 客户端验收
 * @author longfc
 * @version 2017-12-15
 */
Ext.define('Shell.class.rea.client.confirm.reasale.add.AddPanel', {
	extend: 'Shell.class.rea.client.confirm.add.AddPanel',

	title: '供货单验收',

	/**新增服务地址*/
	addUrl: '/ReaManageService.svc/ST_UDTO_AddReaSaleDocConfirmOfSale',
	/**修改服务地址*/
	editUrl: '/ReaManageService.svc/ST_UDTO_UpdateReaSaleDocConfirmOfSale',
	
	/**验收主单ID*/
	PK: null,
	/**选择供货单的所属供应商ID*/
	ReaCompID: null,
	/**选择好的供货单ID*/
	SaleDocID: null,
	OTYPE: "reasale",
	//按钮是否可点击
	BUTTON_CAN_CLICK:true,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var TotalPrice = me.DocForm.getComponent('ReaBmsCenSaleDocConfirm_TotalPrice');
		//新增选择供货单后联动处理
		me.DtlGrid.on({
			onSetConfirmInfo: function(g, objValue) {
				var readOnly = false;
				if(objValue) {
					me.DocForm.getForm().setValues(objValue);
					readOnly = true;
				}
				me.DocForm.setCompReadOnlys(readOnly);
			},
			onScanCodeShowDtl: function(grid, info,IsShowScan) {
				me.ShowDtlPanel(grid, info , IsShowScan);
			},
			changeSumTotal: function(total) {
				TotalPrice.setValue(total);
			},
			nodata: function(p) {
				me.DocForm.setCompReadOnlys(false);
			},
			onDelAfter: function(p) {
				if(me.DtlGrid.store.getCount() <= 0)
					me.DocForm.setCompReadOnlys(false);
			}
		});

		me.DocForm.on({
			//供应商选择改变后
			reacompcheck: function(v, objValue) {
				//清空原来已选择的供货单
				me.DtlGrid.SaleDocID="";
				me.DtlGrid.store.removeAll();
				me.DtlGrid.setButtonsDisabled();
				
				me.DtlGrid.ReaCompID = objValue["ReaCompID"];
				me.DtlGrid.ReaCompCName = objValue["ReaCompCName"];
				me.DtlGrid.PlatformOrgNo = objValue["PlatformOrgNo"];				
			},
			//通过选择供货商+供货单号查找本地供货单
			onSaleDocNo: function(form, reaComp, field, e) {
				//先获取本地的供货单,如果没有,再提取平台供货单
				me.onGetLocalSaleDocBySaleDocNo(form, reaComp, field, e);
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
		me.DtlGrid = Ext.create('Shell.class.rea.client.confirm.reasale.add.DtlGrid', {
			header: false,
			itemId: 'DtlGrid',
			region: 'center',
			defaultLoad: false,
			split: true,
			collapsible: true,
			collapsed: false
		});
		me.DocForm = Ext.create('Shell.class.rea.client.confirm.reasale.add.DocForm', {
			header: false,
			itemId: 'DocForm',
			region: 'north',
			width: me.width,
			height: 100,
			split: true,
			collapsible: true,
			collapsed: false
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
		me.DocForm.formtype = "add";
		me.DocForm.isAdd();
		me.DocForm.getComponent('buttonsToolbar').hide();

		me.DtlGrid.PK = null;
		me.DtlGrid.ReaCompID = null;
		me.DtlGrid.SaleDocID = null;
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
		me.DtlGrid.SaleDocID = me.SaleDocID;
		me.DtlGrid.formtype = "edit";
		//只显示暂存未验收的验收货品明细
		me.DtlGrid.defaultWhere = "reabmscensaledtlconfirm.Status=0 and reabmscensaledtlconfirm.SaleDocConfirmID=" + me.PK;
		me.DtlGrid.Status = "0";
		me.DtlGrid.store.removeAll();
		me.DtlGrid.enableControl();
		me.DtlGrid.onSearch();
	},
	onSave: function(status, confirmData) {
		var me = this;

		var docEntity = me.formtype == 'add' ? me.DocForm.getAddParams() : me.DocForm.getEditParams();

		var entity = docEntity.entity;
		entity.Status = status;
		var OrderDocNo=me.DtlGrid.getOrderDocNo();
		if(OrderDocNo!=""){
			entity.OrderDocNo=OrderDocNo;
		}
		var OrderDocID=me.DtlGrid.getOrderDocID();
		if(OrderDocID!=""){
			entity.OrderDocID=OrderDocID;
		}
		var params = {
			"entity": entity,
			"secAccepterType": me.secAccepterType,
			"secAccepterAccount": "",
			"secAccepterPwd": "",
			"codeScanningMode": me.DtlGrid.CodeScanningMode
		};
		if(confirmData) {
			//secAccepterType：默认本实验室(1),供应商(2),供应商或实验室(3)
			/* var invoiceNoValue = confirmData.InvoiceNo;
			if(invoiceNoValue) {
				invoiceNoValue = invoiceNoValue.replace(/"/g, "");
			} */
			var accepterMemoValue = confirmData.AcceptMemo;
			if(accepterMemoValue) {
				accepterMemoValue = accepterMemoValue.replace(/"/g, "");
			}
			if(confirmData.Account) confirmData.Account = confirmData.Account.replace(/"/g, '');
			if(confirmData.Pwd) confirmData.Pwd = confirmData.Pwd.replace(/"/g, '');

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
	/**@description 通过选择供货商+供货单号获取供货单信息*/
	onGetLocalSaleDocBySaleDocNo: function(form, reaComp, field, e, callback) {
		var me = this;
		var saleDocNo = field.getValue();
		if(!saleDocNo) {
			JShell.Msg.alert('供货单号为空！', null, 2000);
			return;
		}
		if(!reaComp || !reaComp.ReaCompID) {
			JShell.Msg.alert('获取供应商信息为空！', null, 2000);
			return;
		}
		var url = JShell.System.Path.ROOT + "/ReaManageService.svc/ST_UDTO_GetLocalReaSaleDocOfConfirmBySaleDocNo";
		var reaServerLabcCode = JcallShell.REA.System.CENORG_CODE;
		if(!reaServerLabcCode) reaServerLabcCode = "";

		var params = {
			"reaCompID": reaComp.ReaCompID,
			"reaServerCompCode": reaComp.ReaServerCompCode,
			"saleDocNo": saleDocNo,
			"reaServerLabcCode": reaServerLabcCode
		};
		JcallShell.Action.delay(function() {
			JShell.Server.post(url, JShell.JSON.encode(params), function(data) {
				data = Ext.JSON.decode(data);
				if(data.success) {
					var saleDocID = data.BoolInfo;
					var objValue = {
						"ReaBmsCenSaleDocConfirm_SaleDocID": saleDocID
					};
					me.DocForm.getForm().setValues(objValue);
					me.DtlGrid.SaleDocID = saleDocID;
					me.DtlGrid.store.removeAll();
					me.DtlGrid.loadSaleDocAndDtlVOOfSaleDocID(true);
				} else {
					//本地供货记录不存在,从供货业务接口提取
					if(data.BoolFlag) {
						//数据库是否独立部署: 1: 是;2: 否;
						var isDeploy = "" + JcallShell.REA.RunParams.Lists["ReaDataBaseIsDeploy"].Value;
						if(isDeploy == "1") {
							me.onExtractSaleDocBySaleDocNoOfDeploy(form, reaComp, field, e);
						} else if(isDeploy == "2") {
							//提取平台供货单(客户端与供应商都在试剂平台上)
							me.onExtractSaleDocBySaleDocNo(form, reaComp, field, e);
						}
					} else {
						JShell.Msg.error(data.ErrorInfo);
					}
				}
			}, false, null, true);
		}, null,100);
	},
	/**@description 通过选择供货商+供货单号提取平台供货单(客户端与供应商都在试剂平台上)*/
	onExtractSaleDocBySaleDocNo: function(form, reaComp, field, e) {
		var me = this;
		var saleDocNo = field.getValue();
		if(!saleDocNo) {
			JShell.Msg.alert('供货单号为空！', null, 2000);
			return;
		}
		if(!reaComp || !reaComp.ReaCompID) {
			JShell.Msg.alert('获取供应商信息为空！', null, 2000);
			return;
		}
		var url = JShell.System.Path.ROOT + "/ZFReaRestfulService.svc/RS_UDTO_UpdateReaBmsCenSaleDocOfExtract";
		var reaServerLabcCode = JcallShell.REA.System.CENORG_CODE;
		if(!reaServerLabcCode) reaServerLabcCode = "";

		var params = {
			"saleDocId": -1,
			"reaCompID": reaComp.ReaCompID,
			"reaServerCompCode": reaComp.ReaServerCompCode,
			"saleDocNo": saleDocNo,
			"reaServerLabcCode": reaServerLabcCode
		};
		//先提取供货信息,再获取待验收的供货信息
		JcallShell.Action.delay(function() {
			JShell.Server.post(url, JShell.JSON.encode(params), function(data) {
				data = Ext.JSON.decode(data);
				if(data.success) {
					var saleDocID = data.BoolInfo;
					var objValue = {
						"ReaBmsCenSaleDocConfirm_SaleDocID": saleDocID
					};
					me.DocForm.getForm().setValues(objValue);
					me.DtlGrid.SaleDocID = saleDocID;
					me.DtlGrid.store.removeAll();
					me.DtlGrid.loadSaleDtlOfConfirmVOOfSaleDocID(true);
				} else {
					JShell.Msg.error(data.ErrorInfo);
				}
			}, false, null, true);
		}, null,100);
	},
	/**提取平台供货单到客户端(客户端与平台不在同一数据库)*/
	onExtractSaleDocBySaleDocNoOfDeploy: function(form, reaComp, field, e) {
		var me = this;
		JShell.Msg.alert('客户端独立部署提取试剂平台供货单功能未实现！', null, 2000);
	}
});