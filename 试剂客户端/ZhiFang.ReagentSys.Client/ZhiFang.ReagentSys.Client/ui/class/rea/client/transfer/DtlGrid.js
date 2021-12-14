/**
 * 目标库
 * @author liangyl
 * @version 2018-03-12
 */
Ext.define('Shell.class.rea.client.transfer.DtlGrid', {
	extend: 'Shell.class.rea.client.transfer.basic.DtlGrid',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.class.rea.client.shelves.place.ButtonsTab'
	],
	title: '目标库列表',
	width: 800,
	height: 500,
	/**用户UI配置Key*/
	userUIKey: 'transfer.DtlGrid',
	/**用户UI配置Name*/
	userUIName: "移库明细列表",
	/**
	 * 重复扫码时是否显示自动关闭的提示信息
	 */
	isScanCodeShowDtl: true,
	features: [{
		ftype: 'summary'
	}],
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.store.on({
			update: function(store, record) {
				if (record.dirty) {
					var changedObj = record.getChanges();
					for (var modified in changedObj) {
						if (modified == "ReaBmsTransferDtl_GoodsQty")
							me.onGoodsQtyChanged(record);
					}
				}
			}
		});
		me.store.removeAll();
		me.onAddListeners();
	},
	initComponent: function() {
		var me = this;
		me.addEvents('changeSumTotal');
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1,
			pluginId: 'NewsGridEditing'
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
			xtype: 'actioncolumn',
			text: '删除',
			align: 'center',
			width: 35,
			sortable: false,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					var Id = record.get(me.PKField);
					if (!Id) {
						return 'button-del hand';
					} else {
						return '';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.store.remove(rec);
					me.fireEvent('delclick', rec.get('ReaBmsTransferDtl_Tab'));
					me.fireEvent('changeSumTotal');
				}
			}]
		}, {
			dataIndex: 'ReaBmsTransferDtl_SStorageName',
			text: '源库房',
			sortable: false,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_SStorageID',
			text: '源库房ID',
			hidden: true,
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_SPlaceName',
			text: '源货架',
			sortable: false,
			width: 65,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_SPlaceID',
			text: '源货架ID',
			hidden: true,
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_DStorageName',
			text: '目的库房',
			sortable: false,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_DStorageID',
			text: '目的库房ID',
			hidden: true,
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_DPlaceName',
			text: '目的货架',
			sortable: false,
			width: 65,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_DPlaceID',
			text: '目的货架ID',
			hidden: true,
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_GoodsNo',
			text: '货品平台编码',
			hidden: true,
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_ProdGoodsNo',
			text: '厂商货品编码',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_ReaGoodsNo',
			text: '货品编码',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_CenOrgGoodsNo',
			text: '供应商货品编码',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_GoodsCName',
			text: '货品名称',
			sortable: false,
			width: 120,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("ReaBmsTransferDtl_BarCodeType");
				if (!barCodeMgr) barCodeMgr = "";
				if (barCodeMgr == "0") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">批</span>&nbsp;&nbsp;';
				} else if (barCodeMgr == "1") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">盒</span>&nbsp;&nbsp;';
				} else if (barCodeMgr == "2") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">无</span>&nbsp;&nbsp;';
				}
				v = barCodeMgr + value;
				if (value.indexOf('"') >= 0) value = value.replace(/\"/g, " ");
				meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaBmsTransferDtl_ReqGoodsQty',
			text: '申请数',
			sortable: false,
			hidden: true,
			width: 65,
			xtype: 'numbercolumn',
			format: '0.00',
			renderer: function(value, meta) {
				var v = value;
				if (v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			}
		}, {
			dataIndex: 'ReaBmsTransferDtl_GoodsQty',
			text: '<b style="color:blue;">移库数</b>',
			sortable: false,
			width: 70,
			summaryType: 'sum',
			type: 'float', 
			xtype: 'numbercolumn',
			format: '0.00',
			editor: {
				xtype: 'numberfield',
				minValue: 0,
				listeners: {
					focus: function(field, e, eOpts) {
						me.comSetReadOnlyOfBarCodeType(field, e);
					},
					change: function(com, newValue, oldValue, eOpts) {}
				}
			},
			renderer: function(value, meta) {
				var v = value;
				if (v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			},
			summaryRenderer: function(value, summaryData, dataIndex) {
				var v = value;
				if (v) v = parseFloat(Ext.util.Format.round(v, 2));
				return '<span style="font-weight:bold;font-size:12px;color:blue;">共' + v +
					'</span>';
			}
		}, {
			dataIndex: 'ReaBmsTransferDtl_SumCurrentQty',
			text: '现有库存',
			sortable: false,
			width: 70,
			xtype: 'numbercolumn',
			format: '0.00',
			renderer: function(value, meta) {
				var v = value;
				if (v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			}
		},{
			xtype: 'actioncolumn',
			text: '扫码信息',
			align: 'center',
			width: 75,
			sortable: false,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					var curCodeList = record.get("ReaBmsTransferDtl_CurReaGoodsScanCodeList");
					if (curCodeList&&curCodeList.length>0) {
						return 'button-show hand';
					} else {
						return '';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.onShowBarCodePanel(rec);
				}
			}]
		}, {
			dataIndex: 'ReaBmsTransferDtl_GoodsUnit',
			text: '单位',
			width: 65,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_UnitMemo',
			text: '规格',
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_Price',
			text: '单价',
			sortable: false,
			width: 70,
			xtype: 'numbercolumn',
			format: '0.00',
			renderer: function(value, meta) {
				var v = value;
				if (v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			}
		}, {
			dataIndex: 'ReaBmsTransferDtl_SumTotal',
			text: '金额',
			sortable: false,
			width: 80,
			xtype: 'numbercolumn',
			format: '0.00',
			renderer: function(value, meta) {
				var v = value;
				if (v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			}
		}, {
			dataIndex: 'ReaBmsTransferDtl_LotNo',
			text: '批号',
			sortable: false,
			width: 120,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_ProdDate',
			text: '生产日期',
			sortable: false,
			width: 85,
			isDate: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_InvalidDate',
			text: '效期',
			sortable: false,
			width: 85,
			isDate: true,
			defaultRenderer: true
		}, {
			dataIndex: 'DPlaceID',
			text: '目的货架id',
			hidden: true,
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_ReaCompanyName',
			text: '供应商',
			sortable: false,
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_RegisterNo',
			text: '注册证号',
			sortable: false,
			width: 120,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_QtyDtlID',
			text: 'QtyDtlID',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_ReaCompanyID',
			text: 'ReaCompanyID',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_GoodsSerial',
			text: '货品条码',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_LotSerial',
			text: '批号条码',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_SysLotSerial',
			text: '系统内部批号条码',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_CompGoodsLinkID',
			text: '货品机构关系ID',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_ReaServerCompCode',
			text: '供应商机平台构码',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_GoodsID',
			text: '货品iD',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_Memo',
			text: 'Memo',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_TaxRate',
			text: 'TaxRate',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_Tab',
			text: '合并标签',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_LotQRCode',
			text: '二维条码号',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_ReaCompCode',
			text: '供货方编码',
			hideable: false,
			sortable: false,
			hidden: true,
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_GoodsSort',
			text: '货品序号',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_CurReaGoodsScanCodeList',
			text: '当前扫码记录',
			sortable: false,
			hidden: true,
			width: 100,
			editor: {},
			renderer: function(value, meta, record) {
				var v = me.showMemoText(value, meta, record);
				return v;
			}
		}, {
			dataIndex: 'ReaBmsTransferDtl_SQtyDtlID',
			text: '库存ID',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_BarCodeType',
			text: '条码类型',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_BarCodeQtyDtlID',
			text: '本次扫码库存ID',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_DefaulteGoodsID',
			text: '原货品ID',
			hideable: false,
			sortable: false,
			hidden: true,
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_DefaulteLotNo',
			text: '原批号',
			hideable: false,
			sortable: false,
			hidden: true,
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_Id',
			text: '主键ID',
			hidden: true,
			isKey: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_DefaulteGoodsQty2',
			text: '原库存量222',
			hideable: false,
			sortable: false,
			hidden: true,
			width: 150
		}, {
			dataIndex: 'ReaBmsQtyDtl_GoodsUnitTab',
			text: '原始单位及信息',
			hideable: false,
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		});
		return columns;

	},
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items = [];
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			items: [{
				text: '刷新库存量',
				tooltip: '刷新库存量',
				iconCls: 'button-search',
				handler: function() {
					me.getGoodsQty();
				}
			}]
		});
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if (me.hasButtontoolbar) items.push(me.createButtontoolbar());
		if (me.hasPagingtoolbar) items.push(me.createPagingtoolbar());
		return items;
	},
	/**显示当次扫条码信息*/
	onShowBarCodePanel: function(record) {
			var me = this;
			var id = record.get("ReaBmsQtyDtl_Id");
			var curCodeList = record.get("ReaBmsTransferDtl_CurReaGoodsScanCodeList");

			if (curCodeList) curCodeList = Ext.JSON.decode(curCodeList);
			var maxWidth = document.body.clientWidth * 0.76;
			var height = document.body.clientHeight * 0.68;			
			var config = {
				resizable: true,
				PK: id,
				//SUB_WIN_NO: '1',
				CurScanCodeList:curCodeList,
				width: maxWidth,
				height: height,
				listeners: {
					beforeclose: function(p, eOpts) {
						var plugin = p.getPlugin(p.cellpluginId);
						if (plugin) {
							plugin.cancelEdit();
						}
					}
				}
			};
			var win = JShell.Win.open('Shell.class.rea.client.transfer.basic.BarCodeGrid', config);
			win.show();
		},
	/**
	 * @description 移库数值改变后处理
	 * */
	onGoodsQtyChanged: function(record) {
		var me = this;
		var goodsQty = record.get('ReaBmsTransferDtl_GoodsQty');
		if (!goodsQty) goodsQty = 0;
		goodsQty = parseFloat(goodsQty);
		me.setSumTotal(goodsQty, record);
		me.setReqGoodsQty(goodsQty, record);
		record.commit();
		me.fireEvent('changeSumTotal');
	},
	/**
	 * @description 移库确认时新增的移库货品
	 * 在移库数改变后,新增 的移库货品的申请数等于当前移库数
	 * */
	setReqGoodsQty: function(goodsQty, rec) {
		var me = this;
		var id = rec.get("ReaBmsTransferDtl_Id");
		if (!id || id == "" || id == "-1") {
			rec.set("ReaBmsTransferDtl_ReqGoodsQty", goodsQty);
		}
	},
	/**数据改变时刷新现有库存量*/
	onAddListeners: function() {
		var me = this;
		me.store.on({
			datachanged: function() {
				me.getGoodsQty();
			}
		});
	},
	/**
	 * 保存校验
	 * 移库数量，不能为0
	 */
	onSaveCheck: function() {
		var me = this,
			records = me.store.data.items,
			len = records.length;
		var isExec = true;
		var msg = '';
		isExec = true, msg = '';
		if (len == 0) {
			msg = '移库明细不能为空';
			isExec = false;
		}
		for (var i = 0; i < len; i++) {
			if (!records[i].get('ReaBmsTransferDtl_DStorageID')) {
				msg += '货品名称:【' + records[i].get('ReaBmsTransferDtl_GoodsCName') + '】的目标库房\n为空,不能保存<br>';
				isExec = false;
			}
			if (me.PlaceData && me.PlaceData.length > 0) {
				if (!records[i].get('ReaBmsTransferDtl_DPlaceID')) {
					msg += '货品名称:【' + records[i].get('ReaBmsTransferDtl_GoodsCName') + '】的目标货架\n为空,不能保存<br>';
					isExec = false;
				}
			}
			if (records[i].get('ReaBmsTransferDtl_GoodsQty') == '0') {
				msg += '货品名称:【' + records[i].get('ReaBmsTransferDtl_GoodsCName') + '】的移库数量为0,不能保存<br>';
				isExec = false;
			}
			//移库数量
			var GoodsQty = records[i].get('ReaBmsTransferDtl_GoodsQty');
			if (!GoodsQty) {
				msg += '货品名称:【' + records[i].get('ReaBmsTransferDtl_GoodsCName') + '】的移库数量为空,不能保存<br>';
				isExec = false;
			}
			if (!GoodsQty) GoodsQty = 0;
			//库存数量
			var DefaulteGoodsQty = records[i].get('ReaBmsTransferDtl_SumCurrentQty');
			if (!DefaulteGoodsQty) DefaulteGoodsQty = 0;

			if (Number(GoodsQty) < 0) {
				msg += '货品名称:【' + records[i].get('ReaBmsTransferDtl_GoodsCName') + '】的移库数量小于0,不能保存<br>';
				isExec = false;
			}
			if (Number(DefaulteGoodsQty) < Number(GoodsQty)) {
				msg += '货品名称:【' + records[i].get('ReaBmsTransferDtl_GoodsCName') + '】的移库数量大于现有库存量,不能保存<br>';
				isExec = false;
			}

			var SStorageID = records[i].get('ReaBmsTransferDtl_SStorageID');
			var SPlaceID = records[i].get('ReaBmsTransferDtl_SPlaceID');
			var DStorageID = records[i].get('ReaBmsTransferDtl_DStorageID');
			var DPlaceID = records[i].get('ReaBmsTransferDtl_DPlaceID');
			var DStoragePlace = DStorageID + DPlaceID;
			var StoragePlace = SStorageID + SPlaceID;
			if (DStorageID && SStorageID) {
				if (DStoragePlace == StoragePlace) {
					msg += '货品名称:【' + records[i].get('ReaBmsTransferDtl_GoodsCName') + '】的目的库房货架,源库房货架一致,不能保存<br>';
					isExec = false;
				}
			}
		}
		if (!isExec) {
			JShell.Msg.error(msg);
		}
		return isExec;
	},
	/**获取明细列表数据*/
	getEditList: function() {
		var me = this,
			records = me.store.data.items,
			len = records.length;
		var dtEditList = [];
		for (var i = 0; i < len; i++) {
			var rec = records[i];
			var entity = me.getEntity(rec);
			dtEditList.push(entity);
		}
		return dtEditList;
	},
	getEntity: function(rec) {
		var me = this;
		var entity = me.callParent(arguments);

		var Id = rec.get('ReaBmsTransferDtl_Id');
		var reqGoodsQty = rec.get('ReaBmsTransferDtl_ReqGoodsQty');
		if (!reqGoodsQty) reqGoodsQty = 0;
		var goodsQty = rec.get('ReaBmsTransferDtl_GoodsQty');
		if (!goodsQty) goodsQty = 0;
		reqGoodsQty = parseFloat(reqGoodsQty);
		goodsQty = parseFloat(goodsQty);
		if (!reqGoodsQty || reqGoodsQty <= 0 || reqGoodsQty != goodsQty) {
			//移库确认时新增的移库货品
			if (!Id || Id == "" || Id == "-1") reqGoodsQty = goodsQty;
		}
		var price = rec.get('ReaBmsTransferDtl_Price');
		if (!price) price = 0;
		price = parseFloat(price);
		entity.ReqGoodsQty = reqGoodsQty;
		entity.GoodsQty = goodsQty;
		entity.Price = price;
		entity.SumTotal = goodsQty * price;
		if (Id) entity.Id = Id;

		return entity;
	}
});
