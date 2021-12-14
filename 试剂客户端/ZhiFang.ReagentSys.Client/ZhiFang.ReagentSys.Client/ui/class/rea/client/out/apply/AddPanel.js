/**
 * 出库申请
 * @author longfc	
 * @version 2019-03-27
 */
Ext.define('Shell.class.rea.client.out.apply.AddPanel', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '出库申请',
	/**新增出库单并更新库存*/
	addUrl: '/ReaManageService.svc/RS_UDTO_AddReaBmsOutDocAndDtl',
	editUrl: '/ReaManageService.svc/RS_UDTO_UpdateReaBmsOutDocAndDtl',
	/**是否按出库人权限出库 false否,TRUE是*/
	IsEmpOut: false,
	/**表单选中的库房*/
	StorageObj: {},
	PK: null,
	/**出库申请类型:1:出库申请;2:出库申请+;*/
	TYPE: "1",
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
		me.Form = Ext.create('Shell.class.rea.client.out.apply.Form', {
			region: 'north',
			height: 135,
			header: false,
			itemId: 'Form',
			split: true,
			collapsible: true,
			collapseMode: 'mini'
		});
		me.StockPanel = Ext.create('Shell.class.rea.client.out.apply.StockApp', {
			region: 'north',
			height: 200,
			header: false,
			itemId: 'StockPanel',
			split: true,
			collapsible: true,
			collapseMode: 'mini'
		});
		me.DtlGrid = Ext.create('Shell.class.rea.client.out.apply.DtlGrid', {
			region: 'center',
			header: false,
			layout: 'fit',
			itemId: 'DtlGrid',
			defaultLoad: false
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
			}, 'Reset']
		};
		return dockedItems;
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
		me.StockPanel.on({
			itemdblclick: function(record, unitArr, barcode) {
				me.DtlGrid.addRecordOne(record,barcode);
			},
			itemdbselectlclick: function(record, barcode) {
				var itemsTab = me.DtlGrid.store.data.items;
				var unitArr = [];
				//判断选择行是否已存在出库明细中
				var istabselect = true;
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
		})
	},
	/**出库保存时出库主单信息进行再封装处理*/
	changeOutDoc: function(outDoc) {
		var me = this;
		var sysdate = JcallShell.System.Date.getDate();
		sysdate = JcallShell.Date.toString(sysdate);
		var username = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		var usernId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);

		outDoc.IsHasCheck = 0;
		outDoc.CheckID = null;
		outDoc.CheckName = "";
		outDoc.CheckTime = null;
		outDoc.IsHasApproval = 0;
		outDoc.ApprovalId = null;
		outDoc.ApprovalCName = "";
		outDoc.ApprovalTime = null;

		//当前为申请+并且出库申请状态为"已申请",当前出库申请单单据状态自动更改为"审批通过"
		if(me.TYPE == "2" && outDoc.Status == "3") {
			outDoc.Status = 7;
			outDoc.IsHasCheck = 1;
			outDoc.CheckID = usernId;
			outDoc.CheckName = username;
			outDoc.CheckTime = JShell.Date.toServerDate(sysdate);

			outDoc.IsHasApproval = 1;
			outDoc.ApprovalId = usernId;
			outDoc.ApprovalCName = username;
			outDoc.ApprovalTime = JShell.Date.toServerDate(sysdate);
		}
		return outDoc;
	},
	onAddSaveClick: function(status, statusName) {
		var me = this;
		if (!me.BUTTON_CAN_CLICK) return;
		
		var isEmpOut = false;
		var isExec = me.DtlGrid.onSaveCheck();
		if(!isExec) return;

		//获取总单信息
		var outDoc = me.Form.getAddParams();
		outDoc.Status = status;
		outDoc.StatusName = statusName;
		outDoc = me.changeOutDoc(outDoc);
		//获取明细
		var DtlInfo = me.DtlGrid.getAddList();
		var url = JShell.System.Path.getUrl(me.addUrl);
		var params = Ext.JSON.encode({
			entity: outDoc,
			dtAddList: DtlInfo,
			isEmpOut: isEmpOut
		});
		me.showMask("出库保存中...");
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
	onEditClick: function(status, statusName) {
		var me = this;
		var isEmpOut = false;
		var values = me.Form.getForm().getValues();
		var isExec = me.DtlGrid.onSaveCheck();
		if(!isExec) return;

		//获取总单信息
		var outDoc = me.Form.getAddParams();
		outDoc.Status = status;
		outDoc.StatusName = statusName;
		outDoc.Id = values.ReaBmsOutDoc_Id;
		outDoc = me.changeOutDoc(outDoc);
		outDoc.DataTimeStamp = [0, 0, 0, 0, 0, 0, 0, 0];

		var fields = me.Form.getEditFields();
		//当前为申请+并且出库申请状态为"已申请"
		if(me.TYPE == "2" && outDoc.Status == "3") {
			fields = fields + ",IsHasCheck,CheckID,CheckName,CheckTime,IsHasApproval,ApprovalId,ApprovalCName,ApprovalTime";
		}
		var dtAddList = me.DtlGrid.getAddList();
		var dtEditList = me.DtlGrid.getEditList();
		var url = JShell.System.Path.getUrl(me.editUrl);
		var params = Ext.JSON.encode({
			entity: outDoc,
			dtAddList: dtAddList,
			dtEditList: dtEditList,
			isEmpOut: isEmpOut
		});
		me.showMask("出库保存中...");
		JShell.Server.post(url, params, function(data) {
			me.hideMask();
			if(data.success) {
				me.fireEvent('save', me);
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	},
	onResetClick: function() {
		var me = this;
		me.Form.PK = me.PK;
		me.DtlGrid.PK = me.PK;
		if(!me.PK) {
			me.Form.isAdd();
			me.DtlGrid.store.removeAll();
		} else {
			me.Form.isEdit(me.PK);
			me.DtlGrid.onSearch();
		}
	},
	isAdd: function() {
		var me = this;
		me.PK = null;
		me.DtlGrid.PK = me.PK;
		me.DtlGrid.defaultWhere ="";
		me.DtlGrid.store.removeAll();
		me.StockPanel.clearData();
		me.Form.PK = me.PK;
		me.Form.isAdd();
	},
	isEdit: function(id) {
		var me = this;
		me.PK = id;
		me.Form.PK = me.PK;		
		me.Form.isEdit(me.PK);
		me.DtlGrid.PK = me.PK;
		me.DtlGrid.defaultWhere = 'reabmsoutdtl.OutDocID=' + me.PK;
		me.DtlGrid.onSearch();
	},
	onDelOne: function(tab) {
		var me = this;
		me.StockPanel.onDelOne(tab);
	},
	datachanged: function(store) {
		var me = this;
		var bo = false;
		if(store.data.items.length > 0) {
			bo = true;
		}
		me.Form.setStorageReadOnly(bo);
	},
	changeSumTotal: function() {
		var me = this;
		var Price = me.Form.getComponent('ReaBmsOutDoc_TotalPrice');
		var Total = me.DtlGrid.getSumTotal();
		Price.setValue(Total);
	},
	loadDatas: function(id, name) {
		var me = this;
		//清除库存表与出库明细表
		me.clearData();
		me.StorageObj.StorageID = id;
		me.StorageObj.StorageName = name;
		me.StockPanel.loadData(me.StorageObj);
	},
	clearData: function() {
		var me = this;
		me.StockPanel.clearData();
		if(!me.PK) me.DtlGrid.store.removeAll();
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
	}
});