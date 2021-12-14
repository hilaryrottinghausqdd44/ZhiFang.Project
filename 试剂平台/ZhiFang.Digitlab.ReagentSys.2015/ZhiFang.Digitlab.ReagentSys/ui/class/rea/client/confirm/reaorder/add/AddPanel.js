/**
 * 客户端验收
 * @author longfc
 * @version 2017-12-15
 */
Ext.define('Shell.class.rea.client.confirm.reaorder.add.AddPanel', {
	extend: 'Shell.class.rea.client.confirm.add.AddPanel',

	title: '订单验收',

	/**新增服务地址*/
	addUrl: '/ReaSysManageService.svc/ST_UDTO_AddReaSaleDocConfirmOfOrder',
	/**修改服务地址*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaSaleDocConfirmOfOrder',
	/**验收主单ID*/
	PK: null,
	/**选择订单的所属供应商ID*/
	ReaCompID: null,
	/**选择好的订单ID*/
	DocOrderId: null,
	OTYPE: "reaorder",
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//新增选择订单后联动处理
		me.DtlGrid.on({
			onSetConfirmInfo: function(g, objValue) {
				if(objValue) me.DocForm.getForm().setValues(objValue);
			},
			onScanCodeShowDtl: function(grid, info) {
				me.ShowDtlPanel(grid, info);
			}
		});
		//供应商选择改变后
		me.DocForm.on({
			reacompcheck: function(v, record) {
				var ReaCompID = record ? record.get('ReaCenOrg_Id') : '';
				me.DtlGrid.ReaCompID = ReaCompID;
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.addEvents('onFullScreenClick', 'save');
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
		me.DocForm.formtype = "add";
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
		me.DocForm.formtype = "edit";
		me.DocForm.isEdit(me.PK);
		me.DocForm.getComponent('buttonsToolbar').hide();
		me.setReaCompNameReadOnly(true);

		me.DtlGrid.PK = me.PK;
		me.DtlGrid.ReaCompID = me.ReaCompID;
		me.DtlGrid.DocOrderId = me.DocOrderId;
		me.DtlGrid.formtype = "edit";
		//只显示暂存未验收的验收货品明细
		me.DtlGrid.defaultWhere = "bmscensaledtlconfirm.Status=0 and bmscensaledtlconfirm.BmsCenSaleDocConfirm.Id=" + me.PK;
		me.DtlGrid.Status = "0";
		me.DtlGrid.store.removeAll();
		me.DtlGrid.enableControl();
		me.DtlGrid.onSearch();
	},
	onTempSaveClick: function() {
		var me = this;
		me.DocForm.Status = "0";
		me.DtlGrid.Status = "0";
		//验收确认弹出录入
		me.onOpenCheckForm("0");
	},
	onConfirmClick: function() {
		var me = this;
		me.DocForm.Status = "1";
		me.DtlGrid.Status = "1";
		//验收确认弹出录入
		me.onOpenCheckForm("1");
	},
	onSave: function(status, confirmData) {
		var me = this;

		if(!me.DocForm.getForm().isValid()) return;
		var docEntity = me.formtype == 'add' ? me.DocForm.getAddParams() : me.DocForm.getEditParams();

		if(!docEntity.entity) {
			JShell.Msg.alert("获取封装验收细信息为空", null, 2000);
			return;
		}
		if(!me.DtlGrid.validatorSave()) return;

		var entity = docEntity.entity;
		entity.Status = status;
		var params = {
			"entity": entity,
			"secAccepterType": me.secAccepterType,
			"secAccepterAccount": "",
			"secAccepterPwd": "",
			"codeScanningMode": me.DtlGrid.CodeScanningMode
		};
		if(confirmData) {
			//secAccepterType：默认本实验室(1),供应商(2),供应商或实验室(3)
			var invoiceNoValue = confirmData.InvoiceNo;
			if(invoiceNoValue) {
				invoiceNoValue = invoiceNoValue.replace(/"/g, "");
				invoiceNoValue = invoiceNoValue.replace(/\\/g, '');
				invoiceNoValue = invoiceNoValue.replace(/[\r\n]/g, '');
			}
			var accepterMemoValue = confirmData.AcceptMemo;
			if(accepterMemoValue) {
				accepterMemoValue = accepterMemoValue.replace(/"/g, "");
				accepterMemoValue = accepterMemoValue.replace(/\\/g, '');
				accepterMemoValue = accepterMemoValue.replace(/[\r\n]/g, '');
			}
			if(confirmData.Account) confirmData.Account = confirmData.Account.replace(/"/g, '');
			if(confirmData.Pwd) confirmData.Pwd = confirmData.Pwd.replace(/"/g, '');

			params.secAccepterAccount = confirmData.Account;
			params.secAccepterPwd = confirmData.Pwd;

			entity.InvoiceNo = invoiceNoValue;
			entity.AcceptMemo = accepterMemoValue;
			docEntity.fields = docEntity.fields + ",AcceptMemo,InvoiceNo";
		}
		var dtParams = me.DtlGrid.getSaveParams(status);
		switch(me.formtype) {
			case "add":
				params.dtAddList = dtParams.dtAddList;
				if(!params.dtAddList) params.dtAddList = [];
				if(!params.dtAddList || params.dtAddList.length <= 0) {
					JShell.Msg.alert("获取验收货品明细信息为空,不能保存!", null, 2000);
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
					JShell.Msg.alert("获取验收货品明细信息为空,不能保存!", null, 2000);
					return;
				}
				break;
			default:
				break;
		}
		params = JcallShell.JSON.encode(params);
		if(!params) {
			JShell.Msg.alert("封装验收信息出错,不能保存!", null, 2000);
			return;
		}

		var url = me.formtype == 'add' ? me.addUrl : me.editUrl;
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		me.showMask(me.DocForm.saveText); //显示遮罩层
		JShell.Server.post(url, params, function(data) {
			me.hideMask(); //隐藏遮罩层
			if(data.success) {
				me.PK = me.formtype == 'add' ? data.value.id : me.PK;
				var isStorage = me.getcboIsStorageValue();
				if(entity.Status=="0")isStorage=false;
				me.fireEvent('save', me, me.PK, isStorage);
				JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.DocForm.hideTimes);
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	}
});