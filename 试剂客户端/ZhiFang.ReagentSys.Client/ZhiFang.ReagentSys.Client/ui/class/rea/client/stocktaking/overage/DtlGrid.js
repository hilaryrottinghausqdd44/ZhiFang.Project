/**
 * 盘库管理--盘盈
 * @author longfc
 * @version 2018-03-20
 */
Ext.define('Shell.class.rea.client.stocktaking.overage.DtlGrid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	title: '盘盈入库',

	/**获取数据服务路径*/
	selectUrl: '/ReaManageService.svc/ST_UDTO_SearchReaBmsInDtlListOfCheckDocID?isPlanish=true',

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
	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaBmsInDtl_ReaGoodsNo',
		direction: 'ASC'
	}],

	/**默认选中*/
	autoSelect: true,
	/**带功能按钮栏*/
	hasButtontoolbar: false,
	/**盘库单Id*/
	PK: null,
	canEdit: true,
	
	/**用户UI配置Key*/
	userUIKey: 'stocktaking.overage.DtlGrid',
	/**用户UI配置Name*/
	userUIName: "盘盈入库明细列表",
	
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
						if(modified == "ReaBmsInDtl_GoodsQty")
							me.onGoodsQtyChanged(record);
						else if(modified == "ReaBmsInDtl_Price")
							me.onPriceChanged(record);
					}
				}
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1
		});
		me.addEvents('onChangeSumTotal');
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'ReaBmsInDtl_BarCodeType',
			text: '条码类型',
			hidden: true,
			width: 85,
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
			dataIndex: 'ReaBmsInDtl_ReaGoodsNo',
			text: '货品编码',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_ProdGoodsNo',
			text: '厂商货品编码',
			width: 90,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_CenOrgGoodsNo',
			text: '供货商货品编码',
			width: 90,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_GoodsNo',
			text: '货品平台编号',
			width: 90,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_GoodsCName',
			text: '货品名称',
			width: 150,
			columnCountKey: me.columnCountKey,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("ReaBmsInDtl_BarCodeType");
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
			dataIndex: 'ReaBmsInDtl_GoodsUnit',
			text: '包装单位',
			width: 65,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_ReaGoods_UnitMemo',
			text: '包装规格',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_StorageName',
			text: '所属库房',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_PlaceName',
			text: '所属货架',
			width: 65,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_CompanyName',
			text: '所属供应商',
			width: 95,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_ReaCompCode',
			text: '供应商编码',
			hidden:true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_LotNo',
			text: '货品批号',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_GoodsQty',
			text: '入库数',
			width: 75,
			type: 'float',
			align: 'center',
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_SumTotal',
			sortable: false,
			text: '总计金额',
			align: 'center',
			type: 'float',
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
			dataIndex: 'ReaBmsInDtl_Price',
			text: '<b style="color:blue;">单价</b>',
			width: 65,
			type: 'float',
			align: 'center',
			editor: {
				xtype: 'numberfield',
				minValue: 0,
				allowBlank: false
			},
			renderer: function(value, meta) {
				var v = value || '';
				if(v && ("" + v).indexOf(".") >= 0) {
					v = parseFloat(v).toFixed(2);
					meta.tdAttr = 'data-qtip="<b>' + v + '元</b>"';
				}
				return v;
			}
		}, {
			dataIndex: 'ReaBmsInDtl_InvalidDate',
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
			dataIndex: 'ReaBmsInDtl_ProdDate',
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
			dataIndex: 'ReaBmsInDtl_BiddingNo',
			text: '<b style="color:blue;">招标号</b>',
			width: 65,
			editor: {
				allowBlank: true
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_TaxRate',
			text: '<b style="color:blue;">税率</b>',
			align: 'right',
			width: 60,
			editor: {
				xtype: 'numberfield',
				minValue: 0
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_RegisterNo',
			sortable: false,
			text: '<b style="color:blue;">注册证编号</b>',
			editor: {
				allowBlank: true
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_RegisterInvalidDate',
			text: '<b style="color:blue;">注册证有效期</b>',
			width: 85,
			type: 'date',
			isDate: true,
			editor: {
				xtype: 'datefield',
				format: 'Y-m-d'
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_InDtlNo',
			sortable: false,
			text: '入库明细单号',
			width: 100,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_LotSerial',
			sortable: false,
			text: '一维批号条码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_SysLotSerial',
			sortable: false,
			text: '系统内部批号条码',
			hidden: true,
			defaultRenderer: true
		},{
			dataIndex: 'ReaBmsInDtl_LotQRCode',
			sortable: false,
			text: '二维批号条码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_ApproveDocNo',
			sortable: false,
			text: '批准文号',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_Memo',
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
			dataIndex: 'ReaBmsInDtl_Id',
			sortable: false,
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'ReaBmsInDtl_InDocID',
			sortable: false,
			text: '入库单Id',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_StorageID',
			sortable: false,
			text: '库房Id',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_PlaceID',
			sortable: false,
			text: '货架Id',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_ReaCompanyID',
			sortable: false,
			text: '供应商Id',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_ReaGoods_Id',
			sortable: false,
			text: '货品ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_CompGoodsLinkID',
			sortable: false,
			text: '供应商与货品关系ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_GoodsSerial',
			sortable: false,
			text: '货品条码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_ReaServerCompCode',
			sortable: false,
			text: '供应商平台码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_GoodsSort',
			text: '货品序号',
			hidden: true,
			defaultRenderer: true
		}];
		
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
	/**@description 入库数值改变后联动*/
	onGoodsQtyChanged: function(record) {
		var me = this;
		var Price = record.get('ReaBmsInDtl_Price');
		var GoodsQty = record.get('ReaBmsInDtl_GoodsQty');
		if(GoodsQty) GoodsQty = parseFloat(GoodsQty);
		else GoodsQty = 0;
		if(Price) Price = parseFloat(Price);
		else Price = 0;

		var SumTotal = parseFloat(Price) * parseFloat(GoodsQty);
		SumTotal = SumTotal ? SumTotal : 0;
		record.set('ReaBmsInDtl_SumTotal', SumTotal);
		record.commit();
		me.onChangeSumTotal();
	},
	/**@description 单价值改变后联动*/
	onPriceChanged: function(record) {
		var me = this;
		var Price = record.get('ReaBmsInDtl_Price');
		var GoodsQty = record.get('ReaBmsInDtl_GoodsQty');
		if(GoodsQty) GoodsQty = parseFloat(GoodsQty);
		else GoodsQty = 0;
		if(Price) Price = parseFloat(Price);
		else Price = 0;

		var SumTotal = parseFloat(Price) * parseFloat(GoodsQty);
		SumTotal = SumTotal ? SumTotal : 0;
		record.set('ReaBmsInDtl_SumTotal', SumTotal);
		record.commit();
		me.onChangeSumTotal();
	},
	onChangeSumTotal: function() {
		var me = this;
		var totalPrice = 0;
		me.store.each(function(record) {
			var sumTotal = record.get("ReaBmsInDtl_SumTotal");
			if(!sumTotal) sumTotal = 0;
			totalPrice = totalPrice + parseFloat(sumTotal);
		});
		me.fireEvent('onChangeSumTotal', me, totalPrice);
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
		me.disableControl(); //禁用 所有的操作功能
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
				var LotNo = record.get("ReaBmsInDtl_LotNo");

				var GoodsQty = record.get("ReaBmsInDtl_GoodsQty");
				var Price = record.get("ReaBmsInDtl_Price");
				var SumTotal = record.get("ReaBmsInDtl_SumTotal");
				var BarCodeType = record.get("ReaBmsInDtl_BarCodeType");
				var ReaGoodsName = record.get("ReaBmsInDtl_GoodsCName");

				var StorageID = record.get("ReaBmsInDtl_StorageID");
				var PlaceID = record.get("ReaBmsInDtl_PlaceID");
				var ReaCompanyID = record.get("ReaBmsInDtl_ReaCompanyID");
				var GoodsID = record.get("ReaBmsInDtl_ReaGoods_Id");
				var InvalidDate = record.get("ReaBmsInDtl_InvalidDate");

				if(GoodsQty) GoodsQty = parseFloat(GoodsQty);
				else GoodsQty = 0;
				
				if(Price) Price = parseFloat(Price);
				else Price = 0;

				if(!GoodsID) {
					info = "试剂不能为空!";
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
			totalPrice += parseFloat(record.get("ReaBmsInDtl_SumTotal"));

			var obj = me.getSaveDtlOne(record);
			var BarCodeType = record.get("ReaBmsInDtl_BarCodeType");
			var objvo = {
				"ReaBmsInDtl": obj,
				"BarCodeType": BarCodeType
			};
			dtAddList.push(objvo);
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
			Id: id,
			BarCodeType: record.get("ReaBmsInDtl_BarCodeType"),
			InDtlNo: record.get("ReaBmsInDtl_InDtlNo"),
			//InDocNo: record.get("ReaBmsInDtl_InDocNo"),
			GoodsCName: record.get("ReaBmsInDtl_GoodsCName"),
			GoodsUnit: record.get("ReaBmsInDtl_GoodsUnit"),

			LotNo: record.get("ReaBmsInDtl_LotNo"),
			StorageName: record.get("ReaBmsInDtl_StorageName"),
			PlaceName: record.get("ReaBmsInDtl_PlaceName"),

			CompanyName: record.get("ReaBmsInDtl_CompanyName"),
			BiddingNo: record.get("ReaBmsInDtl_BiddingNo"),
			ApproveDocNo: record.get("ReaBmsInDtl_ApproveDocNo"),
			GoodsSerial: record.get("ReaBmsInDtl_GoodsSerial"),

			LotSerial: record.get("ReaBmsInDtl_LotSerial"),
			LotQRCode: record.get("ReaBmsInDtl_LotQRCode"),			
			SysLotSerial: record.get("ReaBmsInDtl_SysLotSerial"),
			RegisterNo: record.get("ReaBmsInDtl_RegisterNo"),
			ReaServerCompCode: record.get("ReaBmsInDtl_ReaServerCompCode"),

			Memo: record.get("ReaBmsInDtl_Memo"),
			ReaGoodsNo: record.get("ReaBmsInDtl_ReaGoodsNo"),
			ProdGoodsNo: record.get("ReaBmsInDtl_ProdGoodsNo"),
			CenOrgGoodsNo: record.get("ReaBmsInDtl_CenOrgGoodsNo"),
			GoodsNo: record.get("ReaBmsInDtl_GoodsNo")
		};
		
		var GoodsSort = record.get("ReaBmsInDtl_GoodsSort");
		if(GoodsSort) {
			entity.GoodsSort = GoodsSort;
		}
		var StorageID = record.get("ReaBmsInDtl_StorageID");
		if(StorageID){
			entity.StorageID=StorageID;			
		}
		var PlaceID = record.get("ReaBmsInDtl_PlaceID");
		if(PlaceID){
			entity.PlaceID=PlaceID;			
		}
		var ReaCompanyID = record.get("ReaBmsInDtl_ReaCompanyID");
		if(ReaCompanyID){
			entity.ReaCompanyID=ReaCompanyID;			
		}
		var CompGoodsLinkID = record.get("ReaBmsInDtl_CompGoodsLinkID");
		if(CompGoodsLinkID){
			entity.CompGoodsLinkID=CompGoodsLinkID;			
		}
		var ReaCompCode = record.get("ReaBmsInDtl_ReaCompCode");
		if(ReaCompCode){
			entity.ReaCompCode=ReaCompCode;			
		}
		
		var ProdDate = record.get("ReaBmsInDtl_ProdDate");
		var InvalidDate = record.get("ReaBmsInDtl_InvalidDate");
		var RegisterInvalidDate = record.get("ReaBmsInDtl_RegisterInvalidDate");

		if(ProdDate) entity.ProdDate = JShell.Date.toServerDate(ProdDate);
		if(InvalidDate) entity.InvalidDate = JShell.Date.toServerDate(InvalidDate);
		if(RegisterInvalidDate) entity.RegisterInvalidDate = JShell.Date.toServerDate(RegisterInvalidDate);

		var GoodsQty = record.get("ReaBmsInDtl_GoodsQty");
		var Price = record.get("ReaBmsInDtl_Price");
		var SumTotal = record.get("ReaBmsInDtl_SumTotal");
		var TaxRate = record.get("ReaBmsInDtl_TaxRate");

		if(GoodsQty) entity.GoodsQty = GoodsQty;
		if(Price) entity.Price = Price;
		if(SumTotal) entity.SumTotal = SumTotal;

		if(GoodsQty) GoodsQty = parseFloat(GoodsQty);
		else GoodsQty = 0;

		if(Price) Price = parseFloat(Price);
		else Price = 0;
		
		if(SumTotal) SumTotal = parseFloat(SumTotal);
		else SumTotal = Price * GoodsQty;
		
		if(TaxRate) entity.TaxRate = TaxRate;
		var strDataTimeStamp = "1,2,3,4,5,6,7,8";
		//入库主单
		var inDocId = record.get("ReaBmsInDtl_InDocID");
		if(inDocId) {
			entity.InDocID = inDocId;
		}
		var reaGoodsId = record.get("ReaBmsInDtl_ReaGoods_Id");
		if(reaGoodsId) {
			entity.ReaGoods = {
				Id: reaGoodsId,
				DataTimeStamp: strDataTimeStamp.split(',')
			};
		}
		return entity;
	}
});