/**
 * 客户端验收
 * @author longfc
 * @version 2017-12-15
 */
Ext.define('Shell.class.rea.client.confirm.manualinput.add.AddPanel', {
	extend: 'Shell.class.rea.client.confirm.add.AddPanel',

	title: '手工验收',

	/**新增服务地址*/
	addUrl: '/ReaManageService.svc/ST_UDTO_AddReaSaleDocConfirmOfManualInput',
	/**修改服务地址*/
	editUrl: '/ReaManageService.svc/ST_UDTO_UpdateReaSaleDocConfirmOfManualInput',
	ACCEPTMODEL:"1",
	OTYPE: "manualinput",
	//按钮是否可点击
	BUTTON_CAN_CLICK:true,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//供应商选择改变后
		me.DocForm.on({
			reacompcheck: function(v, objValue) {
				me.DtlGrid.ReaCompID = objValue["ReaCompID"];
			}
		});

		me.DtlGrid.on({
			onAddReaGoodsClick: function(grid, records) {
				if (me.DtlGrid.store.getCount() > 0) {
					me.DocForm.setCompReadOnlys(true);
				}
			},
			onScanCodeShowDtl: function(grid, info,IsShowScan) {
				me.ShowDtlPanel(grid, info,IsShowScan);
				if(!IsShowScan){
					me.DtlGrid.setScanCodeFocus();
				}
			},
			changeSumTotal: function(total) {
				var totalPrice = me.DocForm.getComponent('ReaBmsCenSaleDocConfirm_TotalPrice');
				totalPrice.setValue(total);
			},
			nodata: function(p) {
				me.DocForm.setCompReadOnlys(false);
			},
			onDelAfter: function(p) {
				if (me.DtlGrid.store.getCount() <= 0) {
					me.DocForm.setCompReadOnlys(false);
				}
			}
		});
		me.DtlGrid.store.on({
			datachanged: function(store, eOpts) {
				if (me.DtlGrid.store.getCount() > 0) {
					me.DocForm.setCompReadOnlys(true);
				} else {
					me.DocForm.setCompReadOnlys(false);
				}
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
		me.DtlGrid = Ext.create('Shell.class.rea.client.confirm.manualinput.add.DtlGrid', {
			header: false,
			itemId: 'DtlGrid',
			region: 'center',
			defaultLoad: false,
			split: true,
			collapsible: true,
			collapsed: false
		});
		me.DocForm = Ext.create('Shell.class.rea.client.confirm.manualinput.add.DocForm', {
			header: false,
			itemId: 'DocForm',
			region: 'north',
			width: me.width,
			height: 100,
			split: false,
			collapsible: true,
			collapsed: false
		});
		var appInfos = [me.DtlGrid, me.DocForm];
		return appInfos;
	},
	onSave: function(status, confirmData) {
		var me = this;

		if (!me.DocForm.getForm().isValid()) return;
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
		switch (me.formtype) {
			case "add":
				params.dtAddList = dtParams.dtAddList;
				if (!params.dtAddList) params.dtAddList = [];
				if (!params.dtAddList || params.dtAddList.length <= 0) {
					JShell.Msg.error("获取验收货品明细信息为空,不能保存!");
					return;
				}
				break;
			case "edit":
				params.fields = docEntity.fields;
				params.dtAddList = dtParams.dtAddList;
				if (!params.dtAddList) params.dtAddList = [];

				params.dtEditList = dtParams.dtEditList;
				if (!params.dtEditList) params.dtEditList = [];

				var fieldsDtl = me.DtlGrid.getUpdateFields();
				params.fieldsDtl = fieldsDtl;
				if (params.dtAddList.length <= 0 && params.dtEditList.length <= 0) {
					JShell.Msg.error("获取验收货品明细信息为空,不能保存!");
					return;
				}
				break;
			default:
				break;
		}
		params = Ext.JSON.encode(params);
		if (!params) {
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
			if (data.success) {
				//先清空列表信息
				me.DtlGrid.store.removeAll();
				
				me.PK = me.formtype == 'add' ? data.value.id : me.PK;
				var isStorage=true;
				if(me.ACCEPTMODEL!="0"){
					isStorage = me.getcboIsStorageValue();
				}
				if (entity.Status == "0") isStorage = false;

				//关掉货品弹出提示窗体
				me.DtlGrid.hideTimes = 0;
				var dtlWin = Ext.WindowManager.get(me.OTYPE);
				if (dtlWin) dtlWin.close();

				me.fireEvent('save', me, me.PK, isStorage);
				JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.DocForm.hideTimes);
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	}
});
