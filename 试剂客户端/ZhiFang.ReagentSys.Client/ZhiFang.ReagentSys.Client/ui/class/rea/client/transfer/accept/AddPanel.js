/**
 * 移库确认
 * @author liangyl
 * @version 2018-10-26
 */
Ext.define('Shell.class.rea.client.transfer.accept.AddPanel', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '移库确认',
	width: 700,
	height: 480,
	layout: {
		type: 'border'
	},
	/**内容周围距离*/
	bodyPadding: '1px',

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
	PK: null,
	IsCheck: '0',
	/**新增出库单并更新库存*/
	addUrl: '/ReaManageService.svc/RS_UDTO_AddGoodsReaBmsTransferDoc',
	editUrl: '/ReaManageService.svc/RS_UDTO_UpdateReaBmsTransferDocAndDtlOfComp',
	//按钮是否可点击
	BUTTON_CAN_CLICK: true,

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
		me.Form = Ext.create('Shell.class.rea.client.transfer.accept.Form', {
			region: 'north',
			height: 140,
			header: false,
			itemId: 'Form',
			formtype: 'edit',
			PK: me.PK,
			split: true,
			collapsible: true,
			collapseMode: 'mini',
			IsCheck: me.IsCheck
		});
		me.StockPanel = Ext.create('Shell.class.rea.client.transfer.stock.App', {
			region: 'north',
			height: 230,
			header: false,
			itemId: 'StockPanel',
			split: true,
			collapsible: true,
			collapseMode: 'mini'
		});
		me.DtlGrid = Ext.create('Shell.class.rea.client.transfer.accept.DtlGrid', {
			region: 'center',
			header: false,
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
				text: '确认移库',
				iconCls: 'button-save',
				tooltip: '保存',
				handler: function() {
					JShell.Action.delay(function() {
						me.onSave();
					}, null, 500);
				}
			}, {
				text: '重置',
				tooltip: '重置',
				iconCls: 'button-reset',
				margin: '0 0 0 10',
				handler: function() {
					me.onResetClick();
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
				var TotalPrice = me.Form.getComponent('ReaBmsTransferDoc_TotalPrice');
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
				save: function(p) {
					me.onSaveClick(p);
				}
			}
		};
		JShell.Win.open('Shell.class.rea.client.out.basic.AcceptForm', config).show();
	},
	onSave: function() {
		var me = this;
		if(!me.Form.getForm().isValid()) return;
		//基本验证
		var check = me.DtlGrid.onSaveCheck();
		if(!check) return;

		//移库数验证
		var result = me.DtlGrid.onCheckGoodsQty();
		if(result.isExec == false) {
			//存在移库数为0的记录,是否继续移库确认,如果是,自动删除移库数为0的记录后再提交
			var msg2 = result.msg + '<b style="color:blue;">存在移库数为0的记录,是否继续移库确认?<br>' +
				"如果是,点击【确定】按钮,系统自动删除移库数为0的货品后再移库确认!<br>" +
				"如果否,点击【取消】按钮,手工删除移库数为0的货品后再移库确认!<br></b>";
			JShell.Msg.confirm({
				title: '移库确认提示',
				msg: msg2,
				closable: false,
				multiline: false //多行输入框
			}, function(but, text) {
				if(but == "ok") {
					//自动删除移库数为0的移库申请明细
					me.DtlGrid.onDeleteGoodsQty0(function(result2) {
						if(result2.isExec == false) {
							me.BUTTON_CAN_CLICK = true;
							JShell.Msg.error("移库确认提交时，自动删除移库数记录出错，请手工删除后再提交！");
						} else {
							me.onSave2();
						}
					});
				} else {
					me.BUTTON_CAN_CLICK = true;
				}
			});
		} else {
			me.onSave2();
		}
	},
	onSave2: function() {
		var me = this;
		//再次作基本验证
		var check = me.DtlGrid.onSaveCheck();
		if(!check) return;

		var values = me.Form.getForm().getValues();
		if(me.IsCheck == '1') {
			me.showUserForm(values.ReaBmsTransferDoc_CheckID, values.ReaBmsTransferDoc_CheckName);
		} else {
			me.onSaveClick(null);
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
	},
	/**保存服务*/
	onSaveClick: function(p) {
		var me = this;
		if(!me.Form.getForm().isValid()) return;
		if(!me.BUTTON_CAN_CLICK) return;

		var check = me.DtlGrid.onSaveCheck();
		if(!check) return;

		var values = me.Form.getForm().getValues();
		//获取总单信息
		var transferDoc = me.Form.getAddParams();
		transferDoc.Status = '6';
		transferDoc.StatusName = '移库完成';
		transferDoc.DataTimeStamp = [0, 0, 0, 0, 0, 0, 0, 0];
		transferDoc.Id = values.ReaBmsTransferDoc_Id;
		var dtAddList = me.DtlGrid.getAddList();
		var dtEditList = me.DtlGrid.getEditList();
		//获取明细
		var url = JShell.System.Path.getUrl(me.editUrl);
		var params = Ext.JSON.encode({
			entity: transferDoc,
			dtAddList: dtAddList,
			dtEditList: dtEditList,
			isEmpTransfer: false
		});
		me.showMask("移库确认中...");
		me.BUTTON_CAN_CLICK = false; //不可点击

		JShell.Server.post(url, params, function(data) {
			me.hideMask();
			me.BUTTON_CAN_CLICK = true;
			if(data.success) {
				//me.fireEvent('save', p);

				//longfc 2019-12-27
				if(p) p.close();
				//清空移库列表信息，防止重复提交
				me.DtlGrid.store.removeAll();
				me.fireEvent('save', me);
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	},
	onResetClick: function() {
		var me = this;
		me.Form.isEdit(me.PK);
		me.DtlGrid.PK = me.PK;
		if(me.Form.formtype == 'add') {
			me.DtlGrid.store.removeAll();
		} else {
			me.DtlGrid.onSearch();
		}
	}
});