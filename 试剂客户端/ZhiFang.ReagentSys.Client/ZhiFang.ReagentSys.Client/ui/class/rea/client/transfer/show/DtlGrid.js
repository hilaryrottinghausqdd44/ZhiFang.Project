/**
 * 明细查询
 * @author longfc
 * @version 2019-04-25
 */
Ext.define('Shell.class.rea.client.transfer.show.DtlGrid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	title: '目标库列表',
	width: 800,
	height: 500,

	/**查询数据*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsTransferDtlByHQL?isPlanish=true',
	defaultOrderBy: [{
		property: 'ReaBmsTransferDtl_DataAddTime',
		direction: 'DESC'
	}],
	/**默认加载数据*/
	defaultLoad: false,
	/**是否启用序号列*/
	hasRownumberer: false,
	/**默认每页数量*/
	defaultPageSize: 5000,
	/**带分页栏*/
	hasPagingtoolbar: false,
	defaultDisableControl: false,
	/**序号列宽度*/
	rowNumbererWidth: 40,
	/**用户UI配置Key*/
	userUIKey: 'transfer.show.DtlGrid',
	/**用户UI配置Name*/
	userUIName: "移库明细列表",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},

	initComponent: function() {
		var me = this;
		me.addEvents('changeSumTotal');
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
			dataIndex: 'ReaBmsTransferDtl_SStorageName',
			text: '源库房',
			sortable: false,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_SPlaceName',
			text: '源货架',
			sortable: false,
			width: 65,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_DStorageName',
			text: '目的库房',
			sortable: false,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_DPlaceName',
			text: '目的货架',
			sortable: false,
			width: 65,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_SPlaceID',
			text: '源货架Id',
			hidden: true,
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_SStorageID',
			text: '源库房Id',
			hidden: true,
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_DPlaceID',
			text: '目的货架Id',
			hidden: true,
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_DStorageID',
			text: '目的库房Id',
			hidden: true,
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_GoodsID',
			text: '货品ID',
			hidden: true,
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_ReaGoodsNo',
			text: '货品编码',
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_CenOrgGoodsNo',
			text: '供应商货品编码',
			hidden: true,
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_ProdGoodsNo',
			text: '厂商货品编码',
			hidden: true,
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_GoodsCName',
			text: '货品名称',
			sortable: false,
			width: 120,
			minWidth: 100,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("ReaBmsTransferDtl_BarCodeType");
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
			xtype: 'actioncolumn',
			text: '条码记录',
			align: 'center',
			width: 65,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					var barCodeMgr = record.get("ReaBmsTransferDtl_BarCodeType");
					if(!barCodeMgr) barCodeMgr = "";
					if(barCodeMgr == "1") {
						return 'button-show hand';
					} else {
						return '';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get('ReaBmsTransferDtl_Id') + '';
					me.openShowOpForm(id);
				}
			}]
		}, {
			dataIndex: 'ReaBmsTransferDtl_ReqGoodsQty',
			text: '申请数',
			sortable: false,
			width: 70,
			xtype: 'numbercolumn',
			format: '0.00',
			renderer: function(value, meta) {
				var v = value;
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			}
		}, {
			dataIndex: 'ReaBmsTransferDtl_GoodsQty',
			text: '移库数',			
			sortable: false,
			width: 70,
			xtype: 'numbercolumn',
			format: '0.00',
			renderer: function(value, meta) {
				var v = value;
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			}
		}, {
			dataIndex: 'ReaBmsTransferDtl_GoodsUnit',
			text: '单位',
			sortable: false,
			width: 70,
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
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
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
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			}
		}, {
			dataIndex: 'ReaBmsTransferDtl_LotNo',
			text: '批号',
			sortable: false,
			width: 120,
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
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_ReaCompanyID',
			text: 'ReaCompanyID',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_GoodsSerial',
			text: '货品条码',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_LotSerial',
			text: '批号条码',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_SysLotSerial',
			text: '系统内部批号条码',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_CompGoodsLinkID',
			text: '货品机构关系ID',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_ReaServerCompCode',
			text: '供应商机平台构码',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_GoodsID',
			text: '货品iD',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_Memo',
			text: 'Memo',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_TaxRate',
			text: 'TaxRate',
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
			dataIndex: 'ReaBmsTransferDtl_BarCodeType',
			text: '条码类型',
			sortable: false,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_Id',
			text: '移库明细ID',
			sortable: false,
			hidden: true,
			defaultRenderer: true
		});
		return columns;

	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh', {
			xtype: 'label',
			hidden: true,
			text: '移库明细',
			style: "font-weight:bold;color:blue;",
			margin: '0 0 5 5'
		}];
		return items;
	},
	/**显示操作记录信息*/
	openShowOpForm: function(id) {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.99;
		var height = document.body.clientHeight * 0.98;
		var defaultWhere = "reagoodsbarcodeoperation.BDtlID=" + id;
		var win = JShell.Win.open('Shell.class.rea.client.barcodeoperation.dtloper.Grid', {
			title: '移库货品条码扫码记录',
			height: height,
			width: maxWidth,
			SUB_WIN_NO: '1',
			defaultWhere: defaultWhere,
			PK: id,
			listeners: {
				beforeclose: function(p, eOpts) {
					var plugin = p.getPlugin(p.cellpluginId);
					if(plugin) {
						plugin.cancelEdit();
					}
				}
			}
		}).show();
		win.onSearch();
	}
});