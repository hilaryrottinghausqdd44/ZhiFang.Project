/**
 * 移库申请
 * @author liangyl
 * @version 2018-11-05
 */
Ext.define('Shell.class.rea.client.transfer.apply.AddPanel', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '移库申请',
	width: 700,
	height: 480,
	layout: {
		type: 'border'
	},
	/**内容周围距离*/
	bodyPadding: '1px',
	/**新增出库单并更新库存*/
	addUrl: '/ReaManageService.svc/RS_UDTO_AddReaBmsTransferDocAndDtl',
	editUrl: '/ReaManageService.svc/RS_UDTO_UpdateReaBmsTransferDocAndDtl',

	/**表单选中的源库房*/
	SStorageObj: {},
	/**表单选中的目的库房*/
	DStorageObj: {},
	TakerID: null,
	hasLoadMask: true,
	/**申请不需要审核*/
	IsCheck: '0',
	PK: null,
	formtype: 'edit',
	IsShowEdit: false,
	/**是否按权限移库*/
	IsTransferDocIsUse: false,
	/**移库扫码模式(严格模式:1,混合模式：2)*/
	TransferScanCode: '2',
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
		me.Form = Ext.create('Shell.class.rea.client.transfer.apply.Form', {
			region: 'north',
			height: 135,
			header: false,
			itemId: 'Form',
			PK: me.PK,
			split: true,
			collapsible: true,
			collapseMode: 'mini',
			formtype: me.formtype,
			IsCheck: me.IsCheck,
			IsTransferDocIsUse: me.IsTransferDocIsUse
		});
		me.StockPanel = Ext.create('Shell.class.rea.client.transfer.apply.StockApp', {
			region: 'north',
			height: 200,
			header: false,
			itemId: 'StockPanel',
			split: true,
			collapsible: true,
			collapseMode: 'mini'
		});
		me.DtlGrid = Ext.create('Shell.class.rea.client.transfer.apply.DtlGrid', {
			region: 'center',
			header: false,
			layout: 'fit',
			PK: me.PK,
			itemId: 'DtlGrid',
			TransferScanCode: me.TransferScanCode
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
				text: '申请暂存',
				iconCls: 'button-save',
				tooltip: '申请暂存',
				itemId: 'tempSave',
				hidden: me.IsShowEdit == false ? false : true,
				handler: function() {
					JShell.Action.delay(function() {
						if(me.PK) {
							me.onEditClick('1', '暂存');
						} else {
							me.onAddSaveClick('1', '暂存');
						}
					}, null, 500);
				}
			}, {
				text: '申请确认',
				iconCls: 'button-save',
				tooltip: '申请确认',
				itemId: 'Save',
				handler: function() {
					JShell.Action.delay(function() {
						if(me.PK) {
							me.onEditClick('3', '已申请');
						} else {
							me.onAddSaveClick('3', '已申请');
						}
					}, null, 500);
				}
			}, 'Reset', {
				text: '打印出库单',
				iconCls: 'button-print',
				tooltip: '打印出库单',
				hidden: true,
				handler: function() {
					JShell.Msg.alert('打印出库单', null, 1000);
				}
			}]
		};
		return dockedItems;
	},
	/**显示全部*/
	onShowAll: function() {
		var me = this;
		me.Form.expand();
		me.StockPanel.expand();
		me.DtlGrid.expand();
	},
	/**显示库源*/
	onShowSource: function() {
		var me = this;
		me.Form.collapse();
		me.StockPanel.expand();
		me.DtlGrid.expand();
	},
	/**显示目标库*/
	onShowTarget: function() {
		var me = this;
		me.Form.collapse();
		me.StockPanel.collapse();
		me.DtlGrid.expand();
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
	onListeners: function() {
		var me = this;
		var buttonsToolbar = me.DtlGrid.getComponent('buttonsToolbar');
		me.Form.on({
			setSDefaultStorage: function(id, name) {
				me.setSDefaultStorage(id, name);
			},
			setDDefaultStorage: function(id, name) {
				me.setDDefaultStorage(id, name);
			}
		});
		me.DtlGrid.on({
			changeSumTotal: function() {
				var TotalPrice= me.Form.getComponent('ReaBmsTransferDoc_TotalPrice');
				var Total = me.DtlGrid.getSumTotal();
				TotalPrice.setValue(Total);
			},
			delclick: function(tab) {
				me.StockPanel.onDelOne(tab);
			}
		});
		me.DtlGrid.store.on({
			datachanged: function(store, eOpts) {
				var bo = false;
				if(store.data.items.length > 0) {
					bo = true;
				}
				me.Form.setStorageReadOnly(bo);
			}
		});
		me.StockPanel.on({
			dbselectclick: function(v, record, barcode) {
				//判断是否已扫码
				if(barcode) {
					var records = me.DtlGrid.store.data.items;
					var len = records.length;
					for(var j = 0; j < len; j++) {
						var ScanCodeList = records[j].get('ReaBmsTransferDtl_CurReaGoodsScanCodeList');
						var BarCodeType = records[j].get('ReaBmsTransferDtl_BarCodeType');
						if(ScanCodeList && BarCodeType == '1') {
							var BarcodeCodeList = Ext.JSON.decode(ScanCodeList);
							for(var i = 0; i < BarcodeCodeList.length; i++) {
								if(barcode == BarcodeCodeList[i].SysPackSerial) {
									var info = "条码为:" + barcode + "已扫码,请不要重复扫码!";
									JShell.Msg.alert(info, null, 2000);
									return;
								}
							}
						}
					}
				}
				me.DtlGrid.DStorageObj = me.DStorageObj;
				var UnitArr = [],
					ReaGoodsNo = '';
				me.DtlGrid.onAddOne(record, barcode, UnitArr);
			},
			itemdblclick: function(record, UnitArr, barcode) {
				var barCodeQtyDtlID = record.get('ReaBmsQtyDtl_BarCodeQtyDtlID');
				me.DtlGrid.DStorageObj = me.DStorageObj;
				me.DtlGrid.onAddOne(record, barcode, UnitArr, barCodeQtyDtlID);
			},
			itemdbselectlclick: function(record, barcode) {
				var itemsTab = me.DtlGrid.store.data.items;
				var UnitArr = [];
				//判断选择行是否已存在出库明细中
				var istabselect = true;
				for(var i = 0; i < itemsTab.length; i++) {
					var tab = itemsTab[i].data.ReaBmsTransferDtl_Tab + "";
					var tab1 = record.get('ReaBmsQtyDtl_Tab');
					if(tab1 === tab) {
						JShell.Msg.alert('当前行数据已选择');
						return;
					}
				}
				me.StockPanel.ondbSelect2(record, UnitArr, itemsTab);
			},
			scanCodeClick: function(barcode, grid) {
				var bo = me.DtlGrid.getLotNoIsScanCode(barcode, grid);
				me.StockPanel.onScanCode(barcode, bo);
			}
		});
		me.on({
			save: function(p) {
				if(p) p.close();
				me.close();
			}
		});
	},
	onAddSaveClick: function(Status, StatusName) {
		var me = this;
		if(!me.Form.getForm().isValid()) return;
		if (!me.BUTTON_CAN_CLICK) return;
		
		var isEmpOut = false;
		var check = me.DtlGrid.onSaveCheck();
		if(!check) return;
		//获取总单信息
		var bmsindoc = me.Form.getAddParams();
		bmsindoc.Status = Status;
		bmsindoc.StatusName = StatusName;
		bmsindoc.DataTimeStamp = [0, 0, 0, 0, 0, 0, 0, 0];
		//获取明细
		var DtlInfo = me.DtlGrid.getAddList();
		var url = JShell.System.Path.getUrl(me.addUrl);
		var params = Ext.JSON.encode({
			entity: bmsindoc,
			dtAddList: DtlInfo,
			isEmpTransfer: false
		});
		
		me.showMask("移库保存中...");
		me.BUTTON_CAN_CLICK = false; //不可点击
		JShell.Server.post(url, params, function(data) {
			me.hideMask();
			me.BUTTON_CAN_CLICK = true;
			if(data.success) {
				//清空移库明细列表信息
				me.DtlGrid.store.removeAll();
				
				me.fireEvent('save', me);
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**出库保存服务*/
	onEditClick: function(Status, StatusName) {
		var me = this;
		if(!me.Form.getForm().isValid()) return;
		
		var check = me.DtlGrid.onSaveCheck();
		if(!check) return;

		var values = me.Form.getForm().getValues();
		//获取总单信息
		var bmsindoc = me.Form.getAddParams();
		bmsindoc.Status = Status;
		bmsindoc.StatusName = StatusName;
		bmsindoc.DataTimeStamp = [0, 0, 0, 0, 0, 0, 0, 0];
		bmsindoc.Id = values.ReaBmsTransferDoc_Id;
		var dtAddList = me.DtlGrid.getAddList();
		var dtEditList = me.DtlGrid.getEditList();
		var url = JShell.System.Path.getUrl(me.editUrl);
		var params = Ext.JSON.encode({
			entity: bmsindoc,
			dtAddList: dtAddList,
			dtEditList: dtEditList,
			isEmpTransfer: false
		});
		me.showMask("移库保存中...");
		me.BUTTON_CAN_CLICK = false;
		JShell.Server.post(url, params, function(data) {
			me.hideMask();
			me.BUTTON_CAN_CLICK = true;
			if(data.success) {
				//清空移库明细列表信息
				me.DtlGrid.store.removeAll();
				me.fireEvent('save', me);
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	},
	onResetClick: function() {
		var me = this;
		me.BUTTON_CAN_CLICK = true;
		me.Form.isEdit(me.PK);
		me.DtlGrid.PK = me.PK;
		if(!me.PK) {
			me.DtlGrid.store.removeAll();
		} else {
			me.DtlGrid.onSearch();
		}
	},
	clearData: function() {
		var me = this;
		me.StockPanel.clearData();
		if(me.Form.formtype == 'add') me.DtlGrid.store.removeAll();
	},
	setSDefaultStorage: function(id, name) {
		var me = this;
		me.SStorageObj.StorageID = id;
		me.SStorageObj.StorageName = name;
		me.StockPanel.SStorageObj = me.SStorageObj;
		//清除库存表与明细表
		me.clearData();
		me.StockPanel.loadData(me.SStorageObj);
	},
	setDDefaultStorage: function(id, name) {
		var me = this;
		me.DStorageObj.StorageID = id;
		me.DStorageObj.StorageName = name;
		me.DtlGrid.DStorageObj = me.DStorageObj;
		me.StockPanel.onSetDStorageObj(me.DStorageObj);
		me.DtlGrid.onChangePlace(id);
		//改变库房和货架
		me.DtlGrid.onChangeStorageAndPlace(me.DStorageObj);
	}
});