/**
 * 移库新增
 * @author liangyl
 * @version 2018-03-12
 */
Ext.define('Shell.class.rea.client.transfer.AddPanel', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '移库新增',
	width: 700,
	height: 480,
	layout: {
		type: 'border'
	},
	/**内容周围距离*/
	bodyPadding: '1px',
	/**新增出库单并更新库存*/
	addUrl: '/ReaManageService.svc/RS_UDTO_AddGoodsReaBmsTransferDoc',
	/**是否要求确认审核人,1是,0否*/
	IsCheck: '1',
	/**表单选中的源库房*/
	SStorageObj: {},
	/**表单选中的目的库房*/
	DStorageObj: {},
	/**是否按权限移库*/
	IsTransferDocIsUse: false,
	TakerID: null,
	/**移库扫码模式(严格模式:1,混合模式：2)*/
	TransferScanCode: '2',
	hasLoadMask: true,
	//按钮是否可点击
	BUTTON_CAN_CLICK:true,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.onListeners();
		me.StockPanel.setScanCodeFocus();
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
		me.Form = Ext.create('Shell.class.rea.client.transfer.Form', {
			region: 'north',
			height: 140,
			header: false,
			itemId: 'Form',
			formtype: 'add',
			split: true,
			collapsible: true,
			collapseMode: 'mini',
			IsCheck: me.IsCheck,
			IsTransferDocIsUse: me.IsTransferDocIsUse
		});
		me.StockPanel = Ext.create('Shell.class.rea.client.transfer.stock.App', {
			region: 'north',
			height: 230,
			header: false,
			itemId: 'StockPanel',
			split: true,
			collapsible: true,
			collapseMode: 'mini',
			/**表单选中的源库房*/
			SStorageObj: me.SStorageObj,
			/**表单选中的目的库房*/
			DStorageObj: me.DStorageObj
		});
		me.DtlGrid = Ext.create('Shell.class.rea.client.transfer.DtlGrid', {
			region: 'center',
			header: false,
			formtype: "add",
			defaultLoad: false,
			itemId: 'DtlGrid',
			TakerID: me.TakerID,
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
				text: '保存',
				itemId: "btnSave",
				iconCls: 'button-save',
				tooltip: '保存',
				handler: function() {
					JShell.Action.delay(function() {
						me.onSave();
					}, null, 500);
				}
			}, '->', {
				text: '显示全部',
				iconCls: 'build-button-arrow-out',
				tooltip: '显示全部',
				hidden: true,
				handler: function() {
					me.onShowAll();
				}
			}, {
				text: '显示原库',
				iconCls: 'build-button-arrow-in',
				tooltip: '只收缩最上面的',
				hidden: true,
				handler: function() {
					me.onShowSource();
				}
			}, {
				text: '显示目标库',
				iconCls: 'build-button-arrow-in',
				tooltip: '上面两个都收缩',
				hidden: true,
				handler: function() {
					me.onShowTarget();
				}
			}]
		};
		return dockedItems;
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
				me.changeSumTotal();
			},
			delclick: function(tab) {
				me.StockPanel.onDelOne(tab);
				me.changeSumTotal();
			},
			onScanCodeShowDtl: function(grid, info) {
				me.ShowDtlPanel(grid, info);
				//条码框重新获取焦点
				//me.StockPanel.focus(true);
				me.StockPanel.setScanCodeFocus();
			}
		});
		me.DtlGrid.store.on({
			datachanged: function(store, eOpts) {
				var bo = false;
				if (store.data.items.length > 0) {
					bo = true;
				}
				me.Form.setStorageReadOnly(bo);
			}
		});
		me.StockPanel.on({
			dbselectclick: function(v, record, barcode) {
				//判断是否已扫码
				if (barcode) {
					var records = me.DtlGrid.store.data.items;
					var len = records.length;
					for (var j = 0; j < len; j++) {
						var ScanCodeList = records[j].get('ReaBmsTransferDtl_CurReaGoodsScanCodeList');
						var BarCodeType = records[j].get('ReaBmsTransferDtl_BarCodeType');
						if (ScanCodeList && BarCodeType == '1') {
							var BarcodeCodeList = Ext.JSON.decode(ScanCodeList);
							for (var i = 0; i < BarcodeCodeList.length; i++) {
								if (barcode == BarcodeCodeList[i].SysPackSerial) {
									var info = "条码为:" + barcode + "已扫码,请不要重复扫码!";
									JShell.Msg.alert(info, null, 2000);
									me.StockPanel.setScanCodeFocus();
									/**
									 * 2.直接移库出库重复扫码时的提示处理调整
									 * 在直接移库登记时,当移库货品出现重复扫码时,提示信息调整为在右下角弹出提示后在5秒后自动关闭,
									 * 在弹出提示信息时,不影响继续进行移库扫码操作;
									 */
									
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
				for (var i = 0; i < itemsTab.length; i++) {
					var tab = itemsTab[i].data.ReaBmsTransferDtl_Tab + "";
					var tab1 = record.get('ReaBmsQtyDtl_Tab');
					if (tab1 === tab) {
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
				if (p) p.close();
				me.close();
			}
		});
	},
	ShowDtlPanel: function(grid, info) {
		var me = this;
		var otype="transfer.basic.DtlInfo";
		var win = Ext.WindowManager.get(otype);
		if(!win) {
			var config = {
				title: "提示信息(6秒后会自动隐藏)",
				resizable: false,
				maximizable: false,
				modal: false,
				closable: true, //关闭功能
				draggable: true, //移动功能
				floating: true, //浮动模式
				width: 280,
				height: 320,
				alwaysOnTop: true,
				itemId: otype,
				id: otype
			};
			win = JShell.Win.open('Shell.class.rea.client.transfer.basic.DtlInfo', config);
			Ext.WindowManager.register(win);
		}
		if(win) {
			//WIN宽高、位置
			var winHeight = me.getHeight();
			var winWidth = me.getWidth();
			var zIndex = me.zIndexManager.zseed + 100;
			var position = me.getPosition();
			var winPosition = [position[0] + winWidth - win.width - 20, winHeight - win.height - 25];
			win.initData(info);
			win.showAt(winPosition);
			if(grid.hideTimes && grid.hideTimes > 0) {
				JcallShell.Action.delay(function() {
					win.hide();
					//条码框重新获取焦点
					me.StockPanel.setScanCodeFocus();
				}, null, grid.hideTimes);
			}
		}
	},
	/**重新计算移库主单金额*/
	changeSumTotal: function() {
		var me = this;
		setTimeout(function() {
			var totalPrice = me.Form.getComponent('ReaBmsTransferDoc_TotalPrice');
			var total = me.DtlGrid.getSumTotal();
			totalPrice.setValue(total);
		}, 300)
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
		if (me.hasLoadMask) {
			me.body.mask(text);
		} //显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if (me.hasLoadMask) {
			me.body.unmask();
		}
	},
	/**确认审核人*/
	showUserForm: function(UseID, UserName) {
		var me = this;
		var config = {
			resizable: false,
			height: 150,
			width: 250,
			SUB_WIN_NO: '1',
			UserName: UserName,
			UserID: UseID,
			listeners: {
				save: function(acceptForm) {
					me.onSaveClick(acceptForm);
				}
			}
		};
		JShell.Win.open('Shell.class.rea.client.out.basic.AcceptForm', config).show();
	},
	onSave: function() {
		var me = this;
		if (!me.Form.getForm().isValid()) return;
		var check = me.DtlGrid.onSaveCheck();
		var values = me.Form.getForm().getValues();
		if (check) {
			if (me.IsCheck == '1') {
				me.showUserForm(values.ReaBmsTransferDoc_CheckID, values.ReaBmsTransferDoc_CheckName);
			} else {
				me.onSaveClick(null);
			}
		}
	},
	clearData: function() {
		var me = this;
		me.StockPanel.clearData();
		if (me.Form.formtype == 'add') me.DtlGrid.store.removeAll();
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
	},
	/**出库保存服务*/
	onSaveClick: function(p) {
		var me = this;		
		if (!me.BUTTON_CAN_CLICK) return;
	
		//获取总单信息
		var transferDoc = me.Form.getAddParams();
		//获取明细
		var DtlInfo = me.DtlGrid.getEditList();
		var url = JShell.System.Path.getUrl(me.addUrl);
		var isEmpTransfer = false;
		var params = Ext.JSON.encode({
			reaBmsTransferDoc: transferDoc,
			listReaBmsTransferDtl: DtlInfo,
			isEmpTransfer: isEmpTransfer
		});
		var btnSave = me.getComponent("buttonsToolbar").getComponent("btnSave");
		//设置保存按钮为隐藏或只读,防止用户多次点保存按钮重复提交setDisabled/setVisible
		if (btnSave) btnSave.setDisabled(true);
		me.showMask("移库保存中...");
		
		me.BUTTON_CAN_CLICK = false; //不可点击		
		JShell.Server.post(url, params, function(data) {
			me.hideMask();
			if (btnSave) btnSave.setDisabled(false);
			me.BUTTON_CAN_CLICK = true; 
			if (data.success) {
				//me.fireEvent('save', p);
				
				//longfc 2019-12-26
				if(p) p.close();
				//表单+移库明细列表
				//清空移库列表信息，防止重复提交
				me.DtlGrid.store.removeAll();
				me.fireEvent('save',me);				
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	}
});
