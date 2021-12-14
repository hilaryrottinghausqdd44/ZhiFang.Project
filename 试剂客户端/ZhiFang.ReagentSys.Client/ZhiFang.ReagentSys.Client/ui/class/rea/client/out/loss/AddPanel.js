/**
 * 报损出库管理
 * @author longfc
 * @version 2019-03-18
 */
Ext.define('Shell.class.rea.client.out.loss.AddPanel', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '新增报损出库',
	width: 700,
	height: 480,
	layout: {
		type: 'border'
	},
	/**内容周围距离*/
	bodyPadding: '1px',
	/**直接出库时是否需要出库确认,1是,0否*/
	IsCheck: '1',
	/**库存新增仪器是否允许为空,1是,0否*/
	IsEquip: '1',
	/**是否按出库人权限出库 false否,TRUE是*/
	IsEmpOut: false,
	/**新增出库单并更新库存*/
	addUrl: '/ReaManageService.svc/RS_UDTO_AddGoodsReaBmsOutDoc',

	TakerObj: {},
	/**表单选中的库房*/
	StorageObj: {},
	/**出库扫码模式(严格模式:1,混合模式：2)*/
	OutScanCodeModel: '2',
	/**货品条码操作类型*/
	barcodeOperType: '14',
	/**出库类型*/
	defaluteOutType: '3',
	//按钮是否可点击
	BUTTON_CAN_CLICK:true,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.onListeners();
	},
	initComponent: function() {
		var me = this;
		//内部组件
		me.items = me.createItems();
		//创建挂靠功能栏
		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this;
		me.Form = Ext.create("Shell.class.rea.client.out.loss.Form", {
			region: 'north',
			height: 135,
			header: false,
			itemId: 'Form',
			formtype: 'add',
			split: true,
			collapsible: true,
			collapseMode: 'mini',
			TakerObj: me.TakerObj,
			/**是否要求确认审核人,1是,0否*/
			IsCheck: me.IsCheck,
			/**库存新增仪器是否允许为空,1是,0否*/
			IsEquip: me.IsEquip,
			/**按领用人权限出库,true 是,false否*/
			IsEmpOut: me.IsEmpOut
		});
		me.QtyDtlGrid = Ext.create('Shell.class.rea.client.out.loss.QtyDtlGrid', {
			region: 'north',
			height: 200,
			header: false,
			itemId: 'QtyDtlGrid',
			split: true,
			collapsible: true,
			collapseMode: 'mini',
			StorageObj: me.StorageObj
		});
		me.DtlGrid = Ext.create('Shell.class.rea.client.out.loss.DtlGrid', {
			region: 'center',
			header: false,
			layout: 'fit',
			defaultLoad: false,
			itemId: 'DtlGrid',
			OutScanCodeModel: me.OutScanCodeModel
		});
		return [me.Form, me.QtyDtlGrid, me.DtlGrid];
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this;
		var dockedItems = {
			xtype: 'uxButtontoolbar',
			dock: 'top',
			itemId: 'buttonsToolbar',
			items: [{
				text: '确认报损出库',
				iconCls: 'button-save',
				itemId: "btnSave",
				tooltip: '保存',
				handler: function() {
					JShell.Action.delay(function() {
						me.onSaveClick(null);
					}, null, 500);
				}
			}]
		};
		return dockedItems;
	},
	/**确认领用人*/
	showUserForm: function(useID, userName) {
		var me = this;
		var config = {
			resizable: false,
			height: 150,
			width: 250,
			SUB_WIN_NO: '1',
			UserID: useID,
			UserName: userName,
			listeners: {
				save: function(p) {
					JShell.Action.delay(function() {
						me.onSave(p);
					}, null, 500);
				}
			}
		};
		JShell.Win.open('Shell.class.rea.client.out.basic.AcceptForm', config).show();
	},
	loadDatas: function(id, name) {
		var me = this;
		me.StorageObj.StorageID = id;
		me.StorageObj.StorageName = name;
		//清除库存表与出库明细表
		me.clearData();
		me.QtyDtlGrid.StorageObj = me.StorageObj;
		me.QtyDtlGrid.onSearch();
	},
	onDelOne: function(tab) {
		var me = this;
	},
	changeSumTotal: function() {
		var me = this;
		var Price = me.Form.getComponent('ReaBmsOutDoc_TotalPrice');
		var Total = me.DtlGrid.getSumTotal();
		Price.setValue(Total);
	},
	datachanged: function(store) {
		var me = this;
		var bo = false;
		if(store.data.items.length > 0) {
			bo = true;
		}
		me.Form.setStorageReadOnly(bo);
	},
	onListeners: function() {
		var me = this;
		me.Form.on({
			setDefaultStorage: function(id, name) {
				JShell.Action.delay(function() {
					me.loadDatas(id, name);
				}, null, 500);
			}
		});
		me.DtlGrid.on({
			changeSumTotal: function() {
				me.changeSumTotal();
			},
			delclick: function(tab) {
				me.onDelOne(tab);
			}
		});
		me.DtlGrid.store.on({
			datachanged: function(store, eOpts) {
				me.datachanged(store);
			}
		});
		me.QtyDtlGrid.on({
			dbselectclick: function(v, record, barcode) {
				me.DtlGrid.addRecordOne(record, barcode);
			},
			itemdblclick: function(v, record, barcode) {
				me.DtlGrid.addRecordOne(record, null);
			},
			scanCodeClick: function(barcode, qtyGrid) {
				var bo = me.DtlGrid.getLotNoIsScanCode(barcode, qtyGrid);
				me.QtyDtlGrid.onScanCode(barcode, bo);
			}
		});
	},
	onSaveClick: function(p) {
		var me = this;
		if(!me.Form.getForm().isValid()) return;
		var check = me.DtlGrid.onSaveCheck();
		if(check == false) return;

		var values = me.Form.getForm().getValues();
		if(me.IsCheck == '1') {
			me.showUserForm(values.ReaBmsOutDoc_ConfirmId, values.ReaBmsOutDoc_ConfirmName);
		} else {
			me.onSave(p);
		}
	},
	/**出库保存服务*/
	onSave: function(p) {
		var me = this;
		//获取总单信息
		var outDoc = me.getOutDocInfo();
		//获取明细
		var DtlInfo = me.DtlGrid.getOutDtlInfo();
		var url = JShell.System.Path.getUrl(me.addUrl);
		var params = Ext.JSON.encode({
			reaBmsOutDoc: outDoc,
			listReaBmsOutDtl: DtlInfo,
			isEmpOut: me.IsEmpOut
		});
		var btnSave = me.getComponent("buttonsToolbar").getComponent("btnSave");
		//设置保存按钮为隐藏或只读,防止用户多次点保存按钮重复提交setDisabled
		if(btnSave) btnSave.setDisabled(true);
		me.showMask("出库保存中...");
		JShell.Server.post(url, params, function(data) {
			me.hideMask();
			if(btnSave) btnSave.setDisabled(false);
			if(data.success) {
				if(p) p.close();
				me.fireEvent('save', me);
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**获取出库主单信息*/
	getOutDocInfo: function() {
		var me = this;
		var outDoc = me.Form.getAddParams();
		outDoc = me.changeOutDoc(outDoc);
		//确认时间
		if(outDoc.ConfirmId) {
			var sysdate = JcallShell.System.Date.getDate();
			sysdate = JcallShell.Date.toString(sysdate);
			outDoc.ConfirmTime = JShell.Date.toServerDate(sysdate);
		}
		return outDoc;
	},
	/**封装出库审核信息*/
	changeOutDoc: function(outDoc) {
		var me = this;
		var sysdate = JcallShell.System.Date.getDate();
		sysdate = JcallShell.Date.toString(sysdate);
		var username = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		var usernId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		outDoc.IsHasCheck = 1;
		outDoc.CheckID = usernId;
		outDoc.CheckName = username;
		outDoc.CheckTime = JShell.Date.toServerDate(sysdate);
		outDoc.IsHasApproval = 1;
		outDoc.ApprovalId = usernId;
		outDoc.ApprovalCName = username;
		outDoc.ApprovalTime = JShell.Date.toServerDate(sysdate);
		return outDoc;
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if(me.hasLoadMask) {
			me.body.mask(text);
		} //显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if(me.hasLoadMask) {
			me.body.unmask();
		}
	},
	/**/
	clearData: function() {
		var me = this;
		me.QtyDtlGrid.store.removeAll();
		me.DtlGrid.store.removeAll();
	}
});