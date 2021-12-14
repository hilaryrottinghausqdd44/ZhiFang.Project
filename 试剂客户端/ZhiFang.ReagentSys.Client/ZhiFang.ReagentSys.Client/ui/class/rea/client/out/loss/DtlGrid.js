/**
 * 出库明细
 * @author liangyl
 * @version 2018-03-12
 */
Ext.define('Shell.class.rea.client.out.loss.DtlGrid', {
	extend: 'Shell.class.rea.client.out.basic.DtlGrid',

	title: '出库明细列表',
	width: 800,
	height: 500,
	/**查询库存数据*/
	selectStoreUrl: '/ReaManageService.svc/RS_UDTO_SearchSumReqGoodsQtyAndCurrentQtyByHQL',
	defaultOrderBy: [{
		property: 'ReaBmsOutDtl_InvalidDate',
		direction: 'DESC'
	}],
	/**出库类型*/
	defaluteOutType: '3',
	/**用户UI配置Key*/
	userUIKey: 'out.loss.DtlGrid',
	/**用户UI配置Name*/
	userUIName: "出库明细列表",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
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
			xtype: 'rownumberer',
			text: me.Shell_ux_grid_Panel.NumberText,
			width: me.rowNumbererWidth,
			align: 'center'
		}, {
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
			dataIndex: 'ReaBmsOutDtl_GoodsNo',
			text: '平台编码',
			hidden: true,
			sortable: false,
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
				if(value.indexOf('"') >= 0) value = value.replace(/\"/g, " ");
				meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaBmsOutDtl_GoodsUnit',
			text: '单位',
			sortable: false,
			width: 80,
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
			sortable: false,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_DefaulteGoodsQty',
			text: '货品库存',
			sortable: false,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_SumCurrentQty',
			text: '剩余总库存',
			sortable: false,
			//hidden: true,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_GoodsQty',
			text: '<b style="color:blue;">出库数</b>',
			sortable: false,
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
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_TestEquipID',
			text: '仪器Id',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		});

		columns.push(me.createEquipNameColumn());
		columns.push({
			dataIndex: 'ReaBmsOutDtl_BarCodeType',
			text: '条码类型',
			hidden: true,
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_SumTotal',
			text: '金额',
			sortable: false,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_LotNo',
			text: '批号',
			sortable: false,
			width: 120,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_StorageName',
			text: '库房名称',
			hidden: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_PlaceName',
			text: '货架名称',
			hidden: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_InvalidDate',
			text: '效期',
			sortable: false,
			width: 85,
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
			text: '供应商',
			sortable: false,
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_RegisterNo',
			text: '注册证号',
			sortable: false,
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
			text: '货品iD',
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
			dataIndex: 'ReaBmsOutDtl_CenOrgGoodsNo',
			text: '供应商货品编码',
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
			editor: {},
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
	/**
	 * 出库保存校验
	 * 直接出库时:出库数量不能为0;
	 * 确认出库时:如果是对出库申请进行确认出库,出库数量可以为0;
	 */
	onSaveCheck: function(isAllowZero) {
		var me = this;
		var isExec = me.callParent(arguments);
		return isExec;
	},
	/**获取明细列表数据*/
	getOutDtlInfo: function() {
		var me = this,
			records = me.store.data.items,
			len = records.length;
		var DtlArr = [],
			dtAddList = [];
		for(var i = 0; i < len; i++) {
			var rec = records[i];
			var obj = me.getEntity(rec);
			var reaBmsInDtlLink = [];
			var barCodeQtyDtlID = rec.get('ReaBmsOutDtl_BarCodeQtyDtlID');
			if(barCodeQtyDtlID) {
				obj.QtyDtlID = barCodeQtyDtlID;
			}
			//扫码明细
			var scanCodeList = rec.get('ReaBmsOutDtl_CurReaGoodsScanCodeList');
			if(scanCodeList.length > 0) {
				reaBmsInDtlLink = Ext.JSON.decode(scanCodeList);
			}
			obj.ReaBmsOutDtlLinkList = reaBmsInDtlLink;
			DtlArr.push(obj);
		}
		return DtlArr;
	}
});