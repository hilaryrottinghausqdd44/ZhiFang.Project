/**
 * 简捷出库
 * @author longfc
 * @version 2019-03-30
 */
Ext.define('Shell.class.rea.client.out.simple.DtlGrid', {
	extend: 'Shell.class.rea.client.out.basic.DtlGrid',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],
	title: '出库明细列表',
	width: 800,
	height: 500,
	/**根据货品条码获取货品相关信息*/
	scanCodeUrl: '/ReaManageService.svc/RS_UDTO_SearchReaBmsQtyDtlByBarCode?isPlanish=true',
	/**新增出库单并更新库存*/
	addUrl: '/ReaManageService.svc/RS_UDTO_AddGoodsReaBmsOutDoc',
	/**获取获取库房货架权限服务路径*/
	selectStorageLinkUrl: '/ReaManageService.svc/RS_UDTO_SearchListByStorageAndLinHQL?isPlanish=true',
	/**移库或出库扫码是否允许从所有库房获取库存货品*/
	barCodeIsAllowOfALLStorage:false,
	
	defaultOrderBy: [{
		property: 'ReaBmsOutDtl_InvalidDate',
		direction: 'DESC'
	}],
	/**后台排序*/
	remoteSort: false,
	/**默认加载数据*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize: 5000,
	/**带分页栏*/
	hasPagingtoolbar: false,
	defaultDisableControl: false,
	/**库存新增仪器是否允许为空,1是,0否*/
	IsEquip: '0',
	/**试剂化仪器关系信息*/
	ReaTestEquipVOList: [],
	/**出库类型默认值*/
	defaluteOutType: '1',
	/**出库扫码模式(严格模式:1,混合模式：2)*/
	OutScanCodeModel: '2',
	/**用户UI配置Key*/
	userUIKey: 'out.simple.DtlGrid',
	/**用户UI配置Name*/
	userUIName: "出库明细列表",
	/**是否按出库人权限出库 false否,TRUE是*/
	IsEmpOut: false,
	/**条码类型*/
	barcodeOperType: '7',
	/**货品扫码时,(相同供货商+相同库房+相同货架+相同货品ID+相同货品批号+效期+入库批次)是否按入库批次合并显示库存货品*/
	isMergeInDocNo: true,
	//按钮是否可点击
	BUTTON_CAN_CLICK:true,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		// 出库扫码模式
		me.getOutScanCodeModel(function(val) {});
		//me.store.removeAll();
		//获取库房并默认
		me.loadStorage();
		//移库或出库扫码是否允许从所有库房获取库存货品
		JShell.REA.RunParams.getRunParamsValue("TranOrOutBarCodeIsAllowOfALLStorage", true, function(data) {
			if (data.value && data.value.ParaValue && data.value.ParaValue == 1) {
				me.barCodeIsAllowOfALLStorage=true;
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.addEvents('changeSumTotal');
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1
		});
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [];
		columns.push({
			xtype: 'actioncolumn',
			text: '删除',
			align: 'center',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					return 'button-del hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.store.remove(rec);
				}
			}]
		}, {
			dataIndex: 'ReaBmsOutDtl_CenOrgGoodsNo',
			text: '供应商货品编码',
			width: 95,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_GoodsNo',
			text: '平台编码',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_ProdGoodsNo',
			text: '厂商货品编码',
			hidden: true,
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_BarCodeType',
			text: '条码类型',
			hidden: true,
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_GoodsCName',
			text: '货品名称',
			sortable: false,
			width: 140,
			minWidth: 100,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("ReaBmsOutDtl_BarCodeType");
				if(!barCodeMgr) barCodeMgr = "";
				if(barCodeMgr == "0") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">批</span>&nbsp;&nbsp;';
				} else if(barCodeMgr == "1") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">盒</span>&nbsp;&nbsp;';
				} else if(barCodeMgr == "2") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">无</span>&nbsp;&nbsp;';
				}
				v = barCodeMgr + value;
				if(value.indexOf('"') >= 0) value = value.replace(/\"/g, " ");
				meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaBmsOutDtl_DefaulteGoodsQty',
			text: '货品库存',
			hidden: true,
			width: 80,
			xtype: 'numbercolumn',
			format: '0.00',
			renderer: function(value, meta) {
				var v = value;
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			}
		}, {
			dataIndex: 'ReaBmsOutDtl_SumCurrentQty',
			text: '剩余总库存',
			sortable: false,
			width: 80,
			xtype: 'numbercolumn',
			format: '0.00',
			renderer: function(value, meta) {
				var v = value;
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			}
		}, {
			dataIndex: 'ReaBmsOutDtl_GoodsQty',
			text: '<b style="color:blue;">出库数</b>',
			sortable: false,
			xtype: 'numbercolumn',
			format: '0.00',
			editor: {
				xtype: 'numberfield',
				minValue: 0,
				listeners: {
					focus: function(field, e, eOpts) {
						me.comSetReadOnlyOfBarCodeType(field, e);
					},
					change: function(com, newValue, oldValue, eOpts) {
						var records = me.getSelectionModel().getSelection();
						if(records.length == 0) {
							JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
							return;
						}
						if(newValue < 0) newValue = 0;
						me.setSumTotal(newValue, records[0]);
						me.fireEvent('changeSumTotal');
					}
				}
			},
			width: 80,
			renderer: function(value, meta) {
				var v = value;
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			}
		}, {
			dataIndex: 'ReaBmsOutDtl_TestEquipID',
			text: '仪器Id',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_ReaTestEquipVOList',
			text: '试剂所属仪器信息',
			sortable: false,
			hidden: true,
			width: 100,
			editor: {
				readOnly: true
			},
			defaultRenderer: true
		});
		columns.push(me.createEquipNameColumn());
		columns.push({
			dataIndex: 'ReaBmsOutDtl_GoodsUnit',
			text: '单位',
			sortable: false,
			width: 65,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_UnitMemo',
			text: '规格',
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_Price',
			text: '单价',
			hidden: true,
			sortable: false,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_SumTotal',
			text: '金额',
			hidden: true,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_LotNo',
			text: '货品批号',
			sortable: false,
			width: 120,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_StorageName',
			text: '所属库房',
			hidden: false,
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_PlaceName',
			text: '所属货架',
			width: 85,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_InvalidDate',
			text: '效期',
			sortable: false,
			width: 95,
			isDate: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_ReaCompanyID',
			text: '供应商Id',
			sortable: false,
			hidden: true,
			sortable: false,
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_CompanyName',
			text: '所属供货商',
			sortable: false,
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_RegisterNo',
			text: '注册证号',
			hidden: true,
			width: 120,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_StorageID',
			text: '库房ID',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_PlaceID',
			text: '货架ID',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_GoodsSerial',
			text: '货品条码',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_LotSerial',
			text: '批号条码',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_SysLotSerial',
			text: '系统内部批号条码',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_CompGoodsLinkID',
			text: '货品机构关系ID',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_ReaServerCompCode',
			text: '供应商机平台构码',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_ProdDate',
			text: '生产日期',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_GoodsID',
			text: '货品ID',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_Memo',
			text: 'Memo',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_TaxRate',
			text: 'TaxRate',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_QtyDtlID',
			text: 'QtyDtlID',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_Tab',
			text: '合并标签', //供应商Id+货品Id+批号+库房Id+货架Id
			editor: {
				readOnly: true
			},
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_DefaulteGoodsID',
			text: '原货品ID',
			hideable: false,
			sortable: false,
			hidden: true,
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_ReaGoodsNo',
			text: '货品编码',
			hideable: false,
			sortable: false,
			hidden: true,
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_LotQRCode',
			text: '二维批条码',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_ReaCompCode',
			text: '供货方编码',
			hideable: false,
			sortable: false,
			hidden: true,
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_GoodsSort',
			text: '货品序号',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_CurReaGoodsScanCodeList',
			text: '当前扫码记录',
			sortable: false,
			hidden: true,
			width: 100,
			editor: {
				readOnly: true
			},
			renderer: function(value, meta, record) {
				var v = me.showMemoText(value, meta, record);
				return v;
			}
		}, {
			dataIndex: 'ReaBmsOutDtl_BarCodeQtyDtlID',
			text: '本次扫码库存ID',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_ISmallUnit',
			text: '当前条码是否是小包装',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		});

		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = [{
			text: '刷新库存量',
			tooltip: '刷新库存量',
			iconCls: 'button-search',
			margin: '5px 5px',
			handler: function() {
				me.getGoodsQty();
			}
		}, '-', {
			xtype: 'button',
			iconCls: 'button-del',
			margin: '5px 5px',
			text: '清空',
			tooltip: '清空',
			handler: function() {
				me.store.removeAll();
			}
		}, '-', {
			fieldLabel: '库房选择',
			emptyText: '库房选择',
			labelWidth: 65,
			width: 175,
			margin: '5px 5px',
			name: 'StorageID',
			itemId: 'StorageID',
			xtype: 'uxSimpleComboBox',
			hasStyle: true,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {}
			}
		}];
		items = me.createTestEquipItems(items);
		items.push({
			name: 'txtScanCode',
			itemId: 'txtScanCode',
			margin: '5px 5px',
			emptyText: '扫货品条码出库',
			width: 165,
			hidden: false,
			labelAlign: 'right',
			xtype: 'textfield',
			fieldLabel: '',
			labelWidth: 0,
			enableKeyEvents: true,
			listeners: {
				specialkey: function(field, e) {
					if(e.getKey() == Ext.EventObject.ENTER) {
						//防止扫码时,自动出现触发多个回车事件
						JShell.Action.delay(function() {
							if(!me.getStorageObj().StorageID) {
								JShell.Msg.alert('库房不能为空!', null, 2000);
								return;
							}
							if(!field.getValue()) {
								JShell.Msg.alert("请输入条码号!", null, 2000);
								me.fireEvent('nodata', me);
								return;
							}
							me.onScanCode(field);
						}, null, 30);
					}
				}
			}
		});
		items.push('-', {
			text: '确认出库',
			iconCls: 'button-save',
			itemId: "btnSave",
			tooltip: '保存并完成出库',
			handler: function() {
				JShell.Action.delay(function() {
					me.onSaveClick();
				}, null, 500);
			}
		});
		return items;
	},
	/**获取明细列表数据*/
	getOutDtlInfo: function() {
		var me = this,
			records = me.store.data.items,
			len = records.length;
		var dtlArr = [],
			dtAddList = [];
		for(var i = 0; i < len; i++) {
			var rec = records[i];
			var obj = me.getEntity(rec);
			var barCodeQtyDtlID = rec.get('ReaBmsOutDtl_BarCodeQtyDtlID');
			if(barCodeQtyDtlID) {
				obj.QtyDtlID = barCodeQtyDtlID;
			}
			//扫码明细
			var reaBmsInDtlLink = [];
			var scanCodeList = rec.get('ReaBmsOutDtl_CurReaGoodsScanCodeList');
			if(scanCodeList.length > 0) {
				reaBmsInDtlLink = Ext.JSON.decode(scanCodeList);
			}
			obj.ReaBmsOutDtlLinkList = reaBmsInDtlLink;
			dtlArr.push(obj);
		}
		return dtlArr;
	},
	//总单信息信息保存
	getDocObj: function() {
		var me = this;
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		var deptID = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTID);
		var deptName = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTNAME);
		var storageObj = me.getStorageObj();
		var storageID = storageObj.StorageID;
		var storageName = storageObj.StorageName;

		var entity = {
			Visible: 1,
			OutType: 1,
			OutTypeName: "使用出库",
			CreaterID: userId,
			CreaterName: userName,
			OutBoundID: userId,
			OutBoundName: userName,
			TakerID: userId,
			TakerName: userName,
			CheckID: userId,
			CheckName: userName,
			CheckID: userId,
			CheckName: userName,
			DeptID: deptID,
			DeptName: deptName,
			StorageID: storageID,
			StorageName: storageName
		};
		//
		entity.TotalPrice = me.getSumTotal();
		//时间
		var sysdate = JcallShell.System.Date.getDate();
		var dateStr = JcallShell.Date.toString(sysdate);
		entity.DataUpdateTime = JShell.Date.toServerDate(dateStr) ? JShell.Date.toServerDate(dateStr) : null;
		entity.DataAddTime = JShell.Date.toServerDate(dateStr) ? JShell.Date.toServerDate(dateStr) : null;
		entity.OperDate = JShell.Date.toServerDate(dateStr) ? JShell.Date.toServerDate(dateStr) : null;
		return entity;
	},
	getStorageObj: function() {
		var me = this;
		var storage = me.getComponent("buttonsToolbar").getComponent("StorageID");
		var storageID = storage.getValue();
		var storageName = storage.getRawValue();
		var storageObj = {
			"StorageID": storageID,
			"StorageName": storageName,
		};
		return storageObj;
	},
	//出库登记保存
	onSaveClick: function() {
		var me = this;
		//出库明细验证
		var isAllowZero = false;
		var check = me.onSaveCheck(isAllowZero);
		if(check == false) return;
		
		if (!me.BUTTON_CAN_CLICK) return;
		//获取总单信息
		var outDoc = me.getDocObj();
		//获取明细
		var outDtlList = me.getOutDtlInfo();

		var params = Ext.JSON.encode({
			reaBmsOutDoc: outDoc,
			listReaBmsOutDtl: outDtlList,
			isEmpOut: me.IsEmpOut
		});
		me.showMask("出库保存中...");
		me.BUTTON_CAN_CLICK = false; //
		
		var url = JShell.System.Path.getUrl(me.addUrl);
		var btnSave = me.getComponent("buttonsToolbar").getComponent("btnSave");
		if(btnSave) btnSave.setDisabled(true);
		JShell.Server.post(url, params, function(data) {
			if(btnSave) btnSave.setDisabled(false);
			me.hideMask();
			me.BUTTON_CAN_CLICK = true; 
			
			if(data.success) {
				me.store.removeAll();
				JShell.Msg.alert('确认出库成功', null, 2000);
				me.fireEvent('save', me);
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**获取出库扫码模式参数信息*/
	getOutScanCodeModel: function(callback) {
		var me = this;
		//出库货品扫码 严格模式:1,混合模式：2"
		JcallShell.REA.RunParams.getRunParamsValue("OutScanCode", false, function(data) {
			if(data.success) {
				var paraValue = "2";
				var obj = data.value;
				if(obj.ParaValue) {
					paraValue = obj.ParaValue;
					me.OutScanCodeModel = paraValue; // parseInt(paraValue);
					if(callback) callback(me.OutScanCodeModel);
				}
			}
		});
	},
	/**按登录者权限获取库房*/
	loadStorage: function() {
		var me = this;
		me.StorageData = [];
		var empId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || "";
		me.getStorageJurisdiction(empId, function(data) {
			if(data && data.value) {
				me.StorageData = data.value.list;
				me.setStorageValue();
			}
		});
	},
	setStorageValue: function() {
		var me = this;
		//默认显示
		var id = '',
			name = '';
		if(me.StorageData.length > 0) {
			id = me.StorageData[0].ReaStorage_Id;
			name = me.StorageData[0].ReaStorage_CName;
		}
		var buttonsToolbar = me.getComponent("buttonsToolbar");
		var storageID = buttonsToolbar.getComponent('StorageID');
		if(storageID) {
			storageID.loadData(me.getStorageData(me.StorageData));
			storageID.setValue(id);
		}
	},
	/**获取库房列表*/
	getStorageData: function(list) {
		var me = this,
			data = [];
		for(var i in list) {
			var obj = list[i];
			data.push([obj.ReaStorage_Id, obj.ReaStorage_CName]);
		}
		return data;
	},
	/**获取库房权限关系的Hql*/
	getStorageLinkHql: function(takerId) {
		var me = this;
		var operType = "1";
		var linkHql = "reauserstoragelink.OperType=" + operType;
		linkHql += ' and reauserstoragelink.OperID=' + takerId;
		return linkHql;
	},
	/**获取库房权限关系的Url*/
	getStorageLinkUrl: function(takerId) {
		var me = this;
		var params = [];
		params.push(JShell.System.Path.ROOT + me.selectStorageLinkUrl);
		params.push("fields=ReaStorage_CName,ReaStorage_Id,ReaStorage_IsMainStorage");
		params.push("storageHql=reastorage.Visible=1");
		params.push("linkHql=" + me.getStorageLinkHql(takerId));
		params.push("sort=[{property:'ReaStorage_IsMainStorage',direction:'DESC'},{property:'ReaStorage_DispOrder',direction:'ASC'}]");
		params.push("operType=1");
		return params;
	},
	/**获取库房货架权限的库房信息（按领用人）*/
	getStorageJurisdiction: function(takerId, callback) {
		var me = this;
		if(!takerId) {
			JShell.Msg.alert('登陆者没有权限,请先设置库房权限');
			return;
		}
		var url = me.getStorageLinkUrl(takerId);
		if(url) url = url.join("&");
		JShell.Server.get(url, function(data) {
			if(data.success) {
				callback(data);
			} else {
				JShell.Msg.error(data.msg);
			}
		}, false);
	},
	/**获取当前条码框*/
	getTxtScanCode: function() {
		var me = this;
		var buttonsToolbar = me.getComponent("buttonsToolbar");
		var txtScanCode = buttonsToolbar.getComponent("txtScanCode");
		return txtScanCode;
	},
	getQtyFields: function() {
		var me = this;
		var ields = "ReaBmsQtyDtl_StorageName,ReaBmsQtyDtl_PlaceName,ReaBmsQtyDtl_CompanyName,ReaBmsQtyDtl_CenOrgGoodsNo,ReaBmsQtyDtl_GoodsName,ReaBmsQtyDtl_LotNo,ReaBmsQtyDtl_GoodsNo,ReaBmsQtyDtl_GoodsQty,ReaBmsQtyDtl_GoodsUnit,ReaBmsQtyDtl_UnitMemo,ReaBmsQtyDtl_Price,ReaBmsQtyDtl_InvalidDate,ReaBmsQtyDtl_RegisterNo,ReaBmsQtyDtl_Id,ReaBmsQtyDtl_ReaCompanyID,ReaBmsQtyDtl_GoodsID,ReaBmsQtyDtl_StorageID,ReaBmsQtyDtl_PlaceID,ReaBmsQtyDtl_TaxRate,ReaBmsQtyDtl_GoodsSerial,ReaBmsQtyDtl_LotSerial,ReaBmsQtyDtl_SysLotSerial,ReaBmsQtyDtl_CompGoodsLinkID,ReaBmsQtyDtl_ReaServerCompCode,ReaBmsQtyDtl_BarCodeType,ReaBmsQtyDtl_ProdDate,ReaBmsQtyDtl_InvalidDate,ReaBmsQtyDtl_Tab,ReaBmsQtyDtl_ReaGoodsNo,ReaBmsQtyDtl_CenOrgGoodsNo,ReaBmsQtyDtl_LotQRCode,ReaBmsQtyDtl_ReaCompCode,ReaBmsQtyDtl_GoodsSort,ReaBmsQtyDtl_CurReaGoodsScanCodeList,ReaBmsQtyDtl_JObjectBarCode";
		return ields;
	},
	/**调用服务返回货品
	 * 货品只有一条数据 不需要弹出列表选择
	 * 货品存在多条数据，需要用户选择
	 * */
	onScanCode: function() {
		var me = this;
		if(!me.getStorageObj().StorageID) {
			JShell.Msg.alert('库房不能为空,请先选择!', null, 2000);
			return;
		}
		var txtScanCode = me.getTxtScanCode();
		var barcode = txtScanCode.getValue();
		//批条码已存在明细中不需要调用扫码服务
		var bo = me.getLotNoIsScanCode(barcode, null);
		if(bo) {
			txtScanCode.setValue("");
			return;
		}
		var url = JShell.System.Path.ROOT + me.scanCodeUrl;
		var barCode2=JShell.String.encode(barcode);
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getQtyFields();
		url += '&barcode=' + barCode2 + '&barcodeOperType=' + me.barcodeOperType + '&isMergeInDocNo=' + me.isMergeInDocNo;
		url += '&storageId=' + me.getStorageObj().StorageID;
		//移库或出库扫码是否允许从所有库房获取库存货品
		url += '&isAllowOfALLStorage=' + me.barCodeIsAllowOfALLStorage;
		
		JShell.Server.get(url, function(data) {
			txtScanCode.setValue("");
			if(data.success) {
				if(data && data.value) {
					var list = data.value.list;
					if(list.length == 0) {
						JShell.Msg.alert("库房没有此货品,请重新选择库房后再扫码!");
						return;
					}
					if(list.length > 1) {
						JShell.Msg.alert("库房存在多个相似的库存货品,请重新选择库房后再扫码!");
						return;
					}
					me.addRecordOfBarcode(list, barcode);
				}
			} else {
				//JShell.Msg.error('库房没有此货品,请重新选择库房后再扫码!</br>' + data.msg);				
				var msg='源库房没有此货品,请重新选择源库房后再扫码!</BR>' + data.msg;
				me.msgShow(msg,function(buttonId, text, opt){
					if(buttonId=="yes"){
						me.setScanCodeFocus();
					}
				});	
			}
		}, false);
	},
	/**
	 * 弹出提示后,执行回调处理
	 * @param {Object} msg
	 * @param {Object} callback
	 */
	msgShow: function(msg, callback) {
		var me = this;
		var view=Ext.Msg.show({
			title: JcallShell.Msg.ERROR_TITLE,
			msg: msg,
			modal: true,
			icon: Ext.Msg.ERROR,
			buttons: Ext.Msg.YES,
			fn: function(buttonId, text, opt) {
				if (callback) callback(buttonId, text, opt);
			}
		});
	},
	addRecordOfBarcode: function(list, barcode) {
		var me = this;
		for(var i = 0; i < list.length; i++) {
			var qtyDtlID = list[i].ReaBmsQtyDtl_Id;
			//供应商+货品+批号+库房+货架
			var reaCompanyID = list[i].ReaBmsQtyDtl_ReaCompanyID;
			var goodsID = list[i].ReaBmsQtyDtl_GoodsID;
			var lotNo = list[i].ReaBmsQtyDtl_LotNo;
			var storageID = list[i].ReaBmsQtyDtl_StorageID;
			var placeID = list[i].ReaBmsQtyDtl_PlaceID;
			var idStr = reaCompanyID + goodsID + lotNo + storageID + placeID;
			list[i]['ReaBmsQtyDtl_Tab'] = idStr;
			list[i]['ReaBmsQtyDtl_BarCodeQtyDtlID'] = qtyDtlID;
			//记录当次扫码操作
			var curArr = me.getCurReaGoodsScanCodeList(barcode);
			list[i]['ReaBmsQtyDtl_CurReaGoodsScanCodeList'] = curArr;
		}
		var qtyDtlObj = list[0];
		me.addRecordOne(qtyDtlObj, barcode);
	},
	/**
	 * 条码扫码框重新置空及获取焦点
	 */
	setScanCodeFocus: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		if (buttonsToolbar) {
			txtScanCode = buttonsToolbar.getComponent('txtScanCode');
			//txtScanCode.setValue('');
			if (txtScanCode) {
				txtScanCode.focus(true, 100);
			}
		}
	},
	/**返回当次扫码记录集合*/
	getCurReaGoodsScanCodeList: function(barcode) {
		var me = this;
		//记录当次扫码操作
		var obj = {
			SysPackSerial: barcode,
			OtherPackSerial: barcode,
			UsePackSerial: barcode,
			UsePackQRCode: barcode
		};
		var arr = [];
		arr.push(obj);
		var curArr = Ext.encode(arr);
		return curArr;
	},
	/**创建新增行数据*/
	createRowObj: function(qtyObj, barcode) {
		var me = this;
		var outDtlObj = me.createOutDtlObj(qtyObj);
		var jObjectBarCode = qtyObj['ReaBmsQtyDtl_JObjectBarCode'];
		outDtlObj = me.createBarcodeJObject(outDtlObj, qtyObj, barcode, jObjectBarCode);
		//出库扫码模式判断
		outDtlObj = me.createScanCodeModel(outDtlObj, barcode);
		//仪器处理
		outDtlObj = me.setOutDtlObjTestEquipValue(qtyObj, outDtlObj, qtyObj["ReaBmsQtyDtl_GoodsID"]);
		return outDtlObj;
	}
});