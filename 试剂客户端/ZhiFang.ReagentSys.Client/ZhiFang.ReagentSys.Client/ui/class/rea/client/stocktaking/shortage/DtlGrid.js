/**
 * 盘库管理-出库
 * @author longfc
 * @version 2018-03-20
 */
Ext.define('Shell.class.rea.client.stocktaking.shortage.DtlGrid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],
	
	title: '出库明细',
	width: 800,
	height: 500,

	/**查询数据*/
	selectUrl: '/ReaManageService.svc/ST_UDTO_SearchReaBmsOutDtlListOfCheckDocID?isPlanish=true',
	
	defaultOrderBy: [{
		property: 'ReaBmsOutDtl_ReaGoodsNo',
		direction: 'ASC'
	}],
	/**默认加载*/
	defaultLoad: false,
	/**后台排序*/
	remoteSort: false,
	/**是否启用序号列*/
	hasRownumberer: true,

	/**默认每页数量*/
	defaultPageSize: 10000,
	/**分页栏下拉框数据*/
	pageSizeList: [
		[10000, 10000]
	],
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,

	/**库存新增仪器是否允许为空,1是,0否*/
	IsEquip: '1',
	/**序号列宽度*/
	rowNumbererWidth: 40,
	/**默认选中*/
	autoSelect: true,
	/**带功能按钮栏*/
	hasButtontoolbar: false,
	/**盘库单Id*/
	PK: null,
	/**试剂化仪器关系信息*/
	ReaTestEquipVOList: [],
	canEdit: true,
	/**用户UI配置Key*/
	userUIKey: 'stocktaking.shortage.DtlGrid',
	/**用户UI配置Name*/
	userUIName: "盘亏出库明细列表",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			beforeedit: function(editor, e) {
				return me.canEdit;
			}
		});
		me.store.on({
			update: function(store, record) {
				if(record.dirty) {
					var changedObj = record.getChanges();
					for(var modified in changedObj) {
						if(modified == "ReaBmsOutDtl_GoodsQty")
							me.onGoodsQtyChanged(record);
						else if(modified == "ReaBmsOutDtl_Price")
							me.onPriceChanged(record);
					}
				}
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.addEvents('onChangeSumTotal');
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1
		});
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
			dataIndex: 'ReaBmsOutDtl_OutDocID',
			text: '出库总单ID',
			sortable: false,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_QtyDtlID',
			text: '库存ID',
			sortable: false,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_ReaGoodsNo',
			text: '货品编码',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_ProdGoodsNo',
			text: '厂商货品编码',
			width: 90,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_CenOrgGoodsNo',
			text: '供货商货品编码',
			width: 90,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_GoodsNo',
			text: '货品平台编码',
			width: 90,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_BarCodeType',
			text: '条码类型',
			hidden: true,
			width: 100,
			renderer: function(value, meta, record) {
				var v = "";
				if(value == "0") {
					v = "批条码";
					meta.style = "color:green;";
				} else if(value == "1") {
					v = "盒条码";
					meta.style = "color:orange;";
				} else if(value == "2") {
					v = "无条码";
					meta.style = "color:black;";
				}

				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaBmsOutDtl_GoodsCName',
			text: '货品名称',
			sortable: false,
			width: 120,
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
				if(value.indexOf('"')>=0)value=value.replace(/\"/g, " ");
				meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaBmsOutDtl_GoodsUnit',
			text: '包装单位',
			sortable: false,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_UnitMemo',
			text: '规格',
			sortable: false,
			hidden: true,
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_StorageName',
			text: '所属库房',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_PlaceName',
			text: '所属货架',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_CompanyName',
			text: '所属供应商',
			sortable: false,
			width: 105,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_LotNo',
			text: '批号',
			sortable: false,
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_GoodsQty',
			text: '出库数',
			width: 75,
			type: 'float',
			align: 'center',
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_SumTotal',
			text: '总计金额',
			type: 'float',
			align: 'center',
			sortable: false,
			width: 80,
			renderer: function(value, meta) {
				var v = value || '';
				if(v && ("" + v).indexOf(".") >= 0) {
					v = parseFloat(v).toFixed(2);
					meta.tdAttr = 'data-qtip="<b>' + v + '元</b>"';
				}
				return v;
			}
		}, {
			dataIndex: 'ReaBmsOutDtl_Price',
			text: '<b style="color:blue;">单价</b>',
			width: 70,
			type: 'float',
			align: 'right',
			editor: {
				xtype: 'numberfield',
				minValue: 0,
				allowBlank: false
			},
			sortable: false,
			renderer: function(value, meta) {
				var v = value || '';
				if(v && ("" + v).indexOf(".") >= 0) {
					v = parseFloat(v).toFixed(2);
					meta.tdAttr = 'data-qtip="<b>' + v + '元</b>"';
				}
				return v;
			}
		}, {
			dataIndex: 'ReaBmsOutDtl_TestEquipID',
			text: '仪器Id',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_TestEquipName',
			width: 120,
			defaultRenderer: true,
			text: '<b style="color:blue;">使用仪器</b>',
			editor: new Ext.form.field.ComboBox({
				mode: 'local',
				editable: false,
				displayField: 'TestEquipName',
				valueField: 'TestEquipName',
				listClass: 'x-combo-list-small',
				store: new Ext.data.Store({ //Simple
					autoLoad: true,
					fields: ['GoodsID', 'GoodsCName', 'TestEquipID', 'TestEquipName'],
					data: []
				}),
				listeners: {
					expand: function(field, eOpts) {
						var record = field.ownerCt.editingPlugin.context.record;
						var goodId = record.get("ReaBmsOutDtl_GoodsID");
						if(record && field.store.getCount() <= 0) {
							if(!me.ReaTestEquipVOList || me.ReaTestEquipVOList.length == 0) me.getReaTestEquipVOList(true);
							if(me.ReaTestEquipVOList && me.ReaTestEquipVOList.length > 0) {
								for(var i = 0; i < me.ReaTestEquipVOList.length; i++) {
									var item = me.ReaTestEquipVOList[i];
									if(goodId == item.GoodsID) {
										var dataList = item.ReaTestEquipVOList;
										if(dataList) {
											field.store.loadData(dataList);
										}
										break;
									}
								}
							}
						}
					},
					focus: function(field, e, eOpts) {
						//if(me.canEdit)
						//me.comSetReadOnly(field, e);
					},
					select: function(field, records, eOpts) {
						var record = field.ownerCt.editingPlugin.context.record;
						record.set('ReaBmsOutDtl_TestEquipID', records[0].get("TestEquipID"));
						record.set('ReaBmsOutDtl_TestEquipName', records[0].get("TestEquipName"));
						//record.commit();
						me.getView().refresh();
					}
				}
			})
		}, {
			dataIndex: 'ReaBmsOutDtl_InvalidDate',
			text: '<b style="color:blue;">有效期至</b>',
			width: 80,
			type: 'date',
			isDate: true,
			editor: {
				xtype: 'datefield',
				format: 'Y-m-d'
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_ProdDate',
			text: '<b style="color:blue;">生产日期</b>',
			width: 80,
			type: 'date',
			isDate: true,
			editor: {
				xtype: 'datefield',
				format: 'Y-m-d'
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_TaxRate',
			text: '<b style="color:blue;">税率</b>',
			align: 'right',
			width: 60,
			editor: {
				xtype: 'numberfield',
				minValue: 0
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_Memo',
			sortable: false,
			text: '<b style="color:red;">备注信息</b>',
			width: 80,
			hidden: false,
			editor: {
				xtype: 'textarea',
				height: 60
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_ReaCompanyID',
			text: '供应商Id',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_ReaCompCode',
			text: '供应商编码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_StorageID',
			text: '库房ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_PlaceID',
			text: '货架ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_SysLotSerial',
			text: '系统内部批号条码',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_CompGoodsLinkID',
			text: '货品机构关系ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_ReaServerCompCode',
			text: '供应商机构平台码',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_Id',
			sortable: false,
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'ReaBmsOutDtl_GoodsID',
			text: '货品iD',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_LotSerial',
			text: '一维批号条码',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_LotQRCode',
			text: '试剂二维批条码',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_GoodsSort',
			text: '货品序号',
			hidden: true,
			defaultRenderer: true
		});
		for(var i = 0; i < columns.length; i++) {
			if(columns[i].editor) {
				columns[i].editor.listeners = {
					beforeedit: function(editor, e) {
						return me.canEdit;
					}
				}
			}
		}
		return columns;
	},
	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this;
		me.store.removeAll();
		if(!me.PK) {
			var msg = me.msgFormat.replace(/{msg}/, "请选择盘库单后再操作");
			me.getView().update(msg);
			return false;
		}
		me.store.proxy.url = me.getLoadUrl(); //查询条件
		//me.disableControl(); //禁用 所有的操作功能
		if(!me.defaultLoad) return false;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [];
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;

		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
		if(me.PK)
			url += '&id=' + me.PK;
		return url;
	},
	/**@description 获取仪器试剂信息*/
	getReaTestEquipVOList: function(isRefresh) {
		var me = this;
		if(isRefresh == true) me.ReaTestEquipVOList = null;
		if(me.ReaTestEquipVOList != null && me.ReaTestEquipVOList.length > 0) return;

		var url = JShell.System.Path.ROOT + '/ReaManageService.svc/ST_UDTO_SearchReaEquipReagentLinkVOList';
		JShell.Server.get(url, function(data) {
			if(data.success) {
				me.ReaTestEquipVOList = data.value;
			} else {
				me.ReaTestEquipVOList = null;
				JShell.Msg.error(data.msg);
			}
		}, false);
	},
	/**@description 库数值改变后联动*/
	onGoodsQtyChanged: function(record) {
		var me = this;
		var Price = record.get('ReaBmsOutDtl_Price');
		var GoodsQty = record.get('ReaBmsOutDtl_GoodsQty');
		if(GoodsQty) GoodsQty = parseFloat(GoodsQty);
		else GoodsQty = 0;
		if(Price) Price = parseFloat(Price);
		else Price = 0;

		var SumTotal = parseFloat(Price) * parseFloat(GoodsQty);
		SumTotal = SumTotal ? SumTotal : 0;
		record.set('ReaBmsOutDtl_SumTotal', SumTotal);
		record.commit();
		me.onChangeSumTotal();
	},
	/**@description 单价值改变后联动*/
	onPriceChanged: function(record) {
		var me = this;
		var Price = record.get('ReaBmsOutDtl_Price');
		var GoodsQty = record.get('ReaBmsOutDtl_GoodsQty');
		if(GoodsQty) GoodsQty = parseFloat(GoodsQty);
		else GoodsQty = 0;
		if(Price) Price = parseFloat(Price);
		else Price = 0;

		var SumTotal = parseFloat(Price) * parseFloat(GoodsQty);
		SumTotal = SumTotal ? SumTotal : 0;
		record.set('ReaBmsOutDtl_SumTotal', SumTotal);
		record.commit();
		me.onChangeSumTotal();
	},
	onChangeSumTotal: function() {
		var me = this;
		var totalPrice = 0;
		me.store.each(function(record) {
			var sumTotal = record.get("ReaBmsOutDtl_SumTotal");
			if(!sumTotal) sumTotal = 0;
			totalPrice = totalPrice + parseFloat(sumTotal);
		});
		me.fireEvent('onChangeSumTotal', me, totalPrice);
	},
	/**加载数据后*/
	onAfterLoad: function(records, successful) {
		var me = this;
		me.callParent(arguments);
		me.onChangeSumTotal();
	},
	/***
	 * @description 保存前验证
	 */
	validatorSave: function() {
		var me = this;
		var isValid = true;
		var info = "";
		if(me.store.getCount() <= 0)
			if(!LotNo) {
				info = "待入库货品明细为空!";
				isValid = false;
			}
		if(isValid == true)
			me.store.each(function(record) {
				var LotNo = record.get("ReaBmsOutDtl_LotNo");

				var GoodsQty = record.get("ReaBmsOutDtl_GoodsQty");
				var Price = record.get("ReaBmsOutDtl_Price");
				var SumTotal = record.get("ReaBmsOutDtl_SumTotal");
				var BarCodeType = record.get("ReaBmsOutDtl_BarCodeType");
				var ReaGoodsName = record.get("ReaBmsOutDtl_GoodsCName");

				var StorageID = record.get("ReaBmsOutDtl_StorageID");
				//var PlaceID = record.get("ReaBmsOutDtl_PlaceID");
				var ReaCompanyID = record.get("ReaBmsOutDtl_ReaCompanyID");
				var GoodsID = record.get("ReaBmsOutDtl_GoodsID");
				var InvalidDate = record.get("ReaBmsOutDtl_InvalidDate");
				var TestEquipID = record.get("ReaBmsOutDtl_TestEquipID");

				if(GoodsQty) GoodsQty = parseFloat(GoodsQty);
				else GoodsQty = 0;

				if(Price) Price = parseFloat(Price);
				else Price = 0;

				if(!GoodsID) {
					info = "货品不能为空!";
					return false;
				}
				if(!GoodsQty || GoodsQty <= 0) {
					info = "试剂为" + ReaGoodsName + ",入库数不能为空或小于等于0!";
					return false;
				}
				if(Price < 0) {
					info = "试剂为" + ReaGoodsName + ",单价小于零，不能验收！";
					return false;
				}
				if(!LotNo) {
					info = "试剂为" + ReaGoodsName + ",待入库货品的货品批号为空!";
					return false;
				}
				if(!StorageID) {
					info = "试剂为" + ReaGoodsName + ",库房不能为空!";
					return false;
				}
				if(!ReaCompanyID) {
					info = "试剂为" + ReaGoodsName + ",所属供应商不能为空!";
					return false;
				}
				if(!InvalidDate) {
					info = "试剂为" + ReaGoodsName + ",有效期至不能为空!";
					return false;
				}
				//				if(!TestEquipID) {
				//					info = "试剂为" + ReaGoodsName + ",使用仪器不能为空!";
				//					return false;
				//				}
			});

		if(info) {
			isValid = false;
			JShell.Msg.error(info);
		}
		return isValid;
	},
	/**@description 封装保存的信息*/
	getSaveDtlAll: function() {
		var me = this;
		var saveParams = {
			"TotalPrice": 0,
			"dtAddList": []
		};
		var dtAddList = [];
		var totalPrice = 0;

		me.store.each(function(record) {
			totalPrice += parseFloat(record.get("ReaBmsOutDtl_SumTotal"));
			var id = record.get(me.PKField);
			var obj = me.getSaveDtlOne(record);
			dtAddList.push(obj);
		});
		if(dtAddList.length > 0) saveParams.dtAddList = dtAddList;
		saveParams.TotalPrice = totalPrice;
		return saveParams;
	},
	/**@description 获取单个的验收明细的基本封装信息*/
	getSaveDtlOne: function(record) {
		var me = this;
		var id = record.get(me.PKField);
		if(!id) id = -1;

		var entity = {
			GoodsID: record.get('ReaBmsOutDtl_GoodsID'),
			GoodsCName: record.get('ReaBmsOutDtl_GoodsCName'),
			GoodsUnit: record.get('ReaBmsOutDtl_GoodsUnit'),
			UnitMemo: record.get('ReaBmsOutDtl_UnitMemo'),
			LotNo: record.get('ReaBmsOutDtl_LotNo'),

			StorageName: record.get("ReaBmsOutDtl_StorageName"),
			PlaceName: record.get("ReaBmsOutDtl_PlaceName"),
			CompanyName: record.get("ReaBmsOutDtl_CompanyName"),
			LotSerial: record.get("ReaBmsOutDtl_LotSerial"),
			LotQRCode: record.get("ReaBmsOutDtl_LotQRCode"),

			SysLotSerial: record.get("ReaBmsOutDtl_SysLotSerial"),
			ReaServerCompCode: record.get("ReaBmsOutDtl_ReaServerCompCode"),
			Memo: record.get("ReaBmsOutDtl_Memo"),
			ReaServerCompCode: record.get("ReaBmsOutDtl_ReaServerCompCode"),
			TestEquipName: record.get("ReaBmsOutDtl_TestEquipName"),

			ReaGoodsNo: record.get("ReaBmsOutDtl_ReaGoodsNo"),
			ProdGoodsNo: record.get("ReaBmsOutDtl_ProdGoodsNo"),
			CenOrgGoodsNo: record.get("ReaBmsOutDtl_CenOrgGoodsNo"),
			GoodsNo: record.get("ReaBmsOutDtl_GoodsNo")
		}

		var GoodsSort = record.get("ReaBmsOutDtl_GoodsSort");
		if(GoodsSort) {
			entity.GoodsSort = GoodsSort;
		}
		var StorageID = record.get("ReaBmsOutDtl_StorageID");
		if(StorageID) {
			entity.StorageID = StorageID;
		}
		var PlaceID = record.get("ReaBmsOutDtl_PlaceID");
		if(PlaceID) {
			entity.PlaceID = PlaceID;
		}
		var ReaCompanyID = record.get("ReaBmsOutDtl_ReaCompanyID");
		if(ReaCompanyID) {
			entity.ReaCompanyID = ReaCompanyID;
		}
		var CompGoodsLinkID = record.get("ReaBmsOutDtl_CompGoodsLinkID");
		if(CompGoodsLinkID) {
			entity.CompGoodsLinkID = CompGoodsLinkID;
		}
		var ReaCompCode = record.get("ReaBmsOutDtl_ReaCompCode");
		if(ReaCompCode) {
			entity.ReaCompCode = ReaCompCode;
		}
		var ProdDate = record.get("ReaBmsOutDtl_ProdDate");
		var InvalidDate = record.get("ReaBmsOutDtl_InvalidDate");

		if(ProdDate) entity.ProdDate = JShell.Date.toServerDate(ProdDate);
		if(InvalidDate) entity.InvalidDate = JShell.Date.toServerDate(InvalidDate);

		var GoodsQty = record.get("ReaBmsOutDtl_GoodsQty");
		var Price = record.get("ReaBmsOutDtl_Price");
		var SumTotal = record.get("ReaBmsOutDtl_SumTotal");
		var TaxRate = record.get("ReaBmsOutDtl_TaxRate");

		if(GoodsQty) GoodsQty = parseFloat(GoodsQty);
		else GoodsQty = 0;

		if(Price) Price = parseFloat(Price);
		else Price = 0;

		entity.GoodsQty = GoodsQty;
		entity.Price = Price;
		entity.SumTotal = SumTotal;

		if(SumTotal) SumTotal = parseFloat(SumTotal);
		else SumTotal = Price * GoodsQty;
		if(TaxRate) entity.TaxRate = TaxRate;

		var BarCodeType = record.get("ReaBmsOutDtl_BarCodeType");
		if(BarCodeType) {
			entity.BarCodeType = Number(BarCodeType);
		}
		var TestEquipID = record.get("ReaBmsOutDtl_TestEquipID");
		if(TestEquipID) {
			entity.TestEquipID = TestEquipID;
		}
		var QtyDtlID = record.get('ReaBmsOutDtl_QtyDtlID');
		if(QtyDtlID) {
			entity.QtyDtlID = QtyDtlID;
		}
		var OutDocID = record.get("ReaBmsOutDtl_OutDocID");
		if(OutDocID) {
			entity.OutDocID = OutDocID;
		}
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if(userId) {
			entity.CreaterID = userId;
			entity.CreaterName = userName;
		}
		return entity;
	}
});