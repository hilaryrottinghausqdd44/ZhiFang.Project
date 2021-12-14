/**
 * 出库确认
 * @author liangyl
 * @version 2018-10-31
 */
Ext.define('Shell.class.rea.client.out.accept.AddPanel', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '出库确认',
	layout: {
		type: 'border'
	},
	/**内容周围距离*/
	bodyPadding: '1px',
	addUrl: '/ReaManageService.svc/RS_UDTO_UpdateReaBmsOutDocAndDtlOfComp',
	PK: null,
	/**直接出库时是否需要出库确认,1是,0否*/
	IsCheck: '1',
	/**库存新增仪器是否允许为空,1是,0否*/
	IsEquip: '1',
	/**是否按出库人权限出库 false否,TRUE是*/
	IsEmpOut: false,
	/**条码类型*/
	barcodeOperType: '7',
	TakerObj: {},
	/**表单选中的库房*/
	StorageObj: {},
	/**出库扫码模式(严格模式:1,混合模式：2)*/
	OutScanCodeModel: '2',
	//按钮是否可点击
	BUTTON_CAN_CLICK:true,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.onListeners();
		JShell.Action.delay(function() {
			if(me.PK) {
				me.isEdit(me.PK);
			} else {
				me.isAdd();
			}
		}, null, 500);
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
		me.StockPanel.on({
			itemdblclick: function(record, unitArr, barcode) {
				me.DtlGrid.addRecordOne(record,barcode);
			},
			itemdbselectlclick: function(record, barcode) {
				var itemsTab = me.DtlGrid.store.data.items;
				var unitArr = [];
				for(var i = 0; i < itemsTab.length; i++) {
					var tab = itemsTab[i].data.ReaBmsOutDtl_Tab + "";
					var tab1 = record.get('ReaBmsQtyDtl_Tab');
					if(tab1 === tab) {
						JShell.Msg.alert('当前行数据已选择');
						return;
					}
				}
				me.StockPanel.ondbSelect2(record, unitArr, itemsTab);
			},
			scanCodeClick: function(barcode, qtyGrid) {
				var bo = me.DtlGrid.getLotNoIsScanCode(barcode, qtyGrid);
				me.StockPanel.onScanCode(barcode, bo);
			}
		});
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
		me.Form = Ext.create('Shell.class.rea.client.out.accept.Form', {
			region: 'north',
			height: 135,
			header: false,
			itemId: 'Form',
			split: true,
			collapsible: true,
			collapseMode: 'mini',
			IsCheck: me.IsCheck,
			IsEquip: me.IsEquip,
			IsEmpOut: me.IsEmpOut
		});
		me.StockPanel = Ext.create('Shell.class.rea.client.out.stock.App', {
			region: 'north',
			height: 200,
			header: false,
			itemId: 'StockPanel',
			split: true,
			collapsible: true,
			collapseMode: 'mini',
			IsCheck: me.IsCheck,
			IsEquip: me.IsEquip,
			IsEmpOut: me.IsEmpOut,
			barcodeOperType: me.barcodeOperType
		});
		me.DtlGrid = Ext.create('Shell.class.rea.client.out.accept.DtlGrid', {
			region: 'center',
			header: false,
			layout: 'fit',
			defaultLoad: false,
			itemId: 'DtlGrid',
			IsCheck: me.IsCheck,
			IsEquip: me.IsEquip,
			IsEmpOut: me.IsEmpOut,
			OutScanCodeModel: me.OutScanCodeModel,
		});
		return [me.Form, me.StockPanel, me.DtlGrid];
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this;
		var dockedItems = {
			xtype: 'uxButtontoolbar',
			dock: 'top',
			itemId: 'buttonsToolbar',
			items: [{
				text: '确认出库',
				iconCls: 'button-save',
				itemId: "btnSave",
				tooltip: '保存并完成出库',
				handler: function() {
					JShell.Action.delay(function() {
						me.onSaveClick('6', '出库完成', null);
					}, null, 500);
				}
			}]
		};
		return dockedItems;
	},
	loadDatas: function(id, name) {
		var me = this;
		me.StorageObj.StorageID = id;
		me.StorageObj.StorageName = name;
		//清除库存表与出库明细表
		me.clearData();
		me.StockPanel.loadData(me.StorageObj);
	},
	onDelOne: function(tab) {
		var me = this;
		me.StockPanel.onDelOne(tab);
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
	clearData: function() {
		var me = this;
		me.StockPanel.clearData();
		if(me.Form.formtype == 'add') me.DtlGrid.store.removeAll();
	},
	removeData: function() {
		var me = this;
		me.StockPanel.clearData();
		me.DtlGrid.store.removeAll();
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
	isAdd: function() {
		var me = this;
		me.PK = null;
		me.Form.PK = me.PK;

		me.StockApp.clearData();
		me.DtlGrid.PK = me.PK;
		me.DtlGrid.defaultWhere = "";
		me.DtlGrid.store.removeAll();
		me.Form.isAdd();
	},
	isEdit: function(id) {
		var me = this;
		me.PK = id;
		me.Form.PK = me.PK;
		me.DtlGrid.PK = me.PK;
		me.Form.isEdit(me.PK);
		me.DtlGrid.defaultWhere = 'reabmsoutdtl.OutDocID=' + me.PK;
		me.DtlGrid.onSearch();
	},
	/**确认人*/
	showUserForm: function(status, statusName, useID, userName) {
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
					if(!me.Form.getForm().isValid()) return;
					me.onSave('6', '出库完成', p);
				}
			}
		};
		JShell.Win.open('Shell.class.rea.client.out.basic.AcceptForm', config).show();
	},
	/**出库保存服务*/
	onSaveClick: function(status, statusName, p) {
		var me = this;
		if(!me.Form.getForm().isValid()) return;
		
		var isAllowZero = false;
		if(me.PK) isAllowZero = true;
		//出库明细验证
		var check = me.DtlGrid.onSaveCheck(isAllowZero);
		if(check == false) return;

		var values = me.Form.getForm().getValues();
		if(me.IsCheck == '1') {
			me.showUserForm(values.ReaBmsOutDoc_ConfirmId, values.ReaBmsOutDoc_ConfirmName);
		} else {
			me.onSave(status, statusName, p);
		}
	},
	onSave: function(status, statusName, p) {
		var me = this;
		if (!me.BUTTON_CAN_CLICK) return;
		//获取总单信息
		var outDoc = me.getOutDocInfo();
		outDoc.Status = status;
		outDoc.StatusName = statusName;
		outDoc.DataTimeStamp = [0, 0, 0, 0, 0, 0, 0, 0];
		if(me.PK) outDoc.Id = me.PK;

		var dtAddList = me.DtlGrid.getAddList();
		var dtEditList = me.DtlGrid.getEditList();
		//获取明细
		var url = JShell.System.Path.getUrl(me.addUrl);
		var params = Ext.JSON.encode({
			entity: outDoc,
			dtAddList: dtAddList,
			dtEditList: dtEditList,
			//isAllowZero:true,
			isEmpOut: false
		});
		me.showMask("出库保存中...");
		me.BUTTON_CAN_CLICK = false; //不可点击
		
		JShell.Server.post(url, params, function(data) {
			me.hideMask();
			me.BUTTON_CAN_CLICK = true;
			if(data.success) {
				if(p) p.close();
				//清空移库列表信息，防止重复提交
				me.DtlGrid.store.removeAll();
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
		//确认时间
		if(outDoc.ConfirmId) {
			var sysdate = JcallShell.System.Date.getDate();
			sysdate = JcallShell.Date.toString(sysdate);
			outDoc.ConfirmTime = JShell.Date.toServerDate(sysdate);
		}
		return outDoc;
	}
});