/**
 * 客户端供货单验收
 * @author longfc
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.confirm.choose.sale.DtlGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: '供货明细列表',
	
	/**获取数据服务路径*/
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDtlByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReagentSysService.svc/ST_UDTO_DelBmsCenSaleDtl',
	/**新增数据服务路径*/
	addUrl: '/ReagentSysService.svc/ST_UDTO_AddBmsCenSaleDtl',
	/**修改数据服务路径*/
	editUrl: '/ReagentSysService.svc/ST_UDTO_UpdateBmsCenSaleDtlByField',
	/**默认加载*/
	defaultLoad: false,
	/**后台排序*/
	remoteSort: true,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,
	hasButtontoolbar: false,
	/**默认每页数量*/
	defaultPageSize: 10000,
	/**分页栏下拉框数据*/
	pageSizeList:[[10000,10000]],
	
	/**排序字段*/
	defaultOrderBy:[
		{property:'BmsCenSaleDtl_DispOrder',direction:'ASC'},
		{property:'BmsCenSaleDtl_DataAddTime',direction:'ASC'},
		{property:'BmsCenSaleDtl_GoodsName',direction:'ASC'}
	],
	
	/**默认选中*/
	autoSelect:true,

	
	/**主单信息*/
	DocInfo:{},
	/**默认选中*/
	autoSelect: false,

	/**是否可编辑*/
	canEdit: true,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.store.on({
			update:function(store,record){
				me.onPriceOrGoodsQtyChanged(record);
			}
		});		
		me.on({
			beforeedit:function(editor, e) {
				return me.canEdit;
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1
		});
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'BmsCenSaleDtl_Goods_BarCodeMgr',
			text: '条码类型',
			hidden:true,
			width: 60,
			renderer: function(value, meta) {
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
			dataIndex: 'BmsCenSaleDtl_GoodsName',
			text: '产品名称',
			width: 120,
			renderer: function(value, meta,record) {
				var v = "";
				var barCodeMgr=record.get("BmsCenSaleDtl_Goods_BarCodeMgr");
				if(barCodeMgr == "0") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">批</span>&nbsp;&nbsp;';
				} else if(barCodeMgr == "1") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">盒</span>&nbsp;&nbsp;';
				} else if(barCodeMgr == "2") {
					barCodeMgr ='<span style="padding:1px;color:red; border:solid 1px red">无</span>&nbsp;&nbsp;';
				}
				v=barCodeMgr+value;
				meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'BmsCenSaleDtl_Goods_GoodsNo',
			text: '产品编号',
			hidden:true,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtl_ProdGoodsNo',
			text: '厂商产品编号',
			hidden: true,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtl_LotNo',
			sortable: false,
			text: '产品批号',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtl_InvalidDate',
			text: '有效期至',
			width: 80,
			type: 'date',
			isDate: true,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtl_GoodsQty',
			text: '供货数',
			width: 65,
			type: 'int',
			align: 'right'
		}, {
			dataIndex: 'BmsCenSaleDtl_AcceptCount',
			style: 'font-weight:bold;color:#fff;background:#5cb85c;',
			text: '已接收',
			width: 55,
			type: 'int',
			align: 'right'
		}, {
			dataIndex: 'BmsCenSaleDtl_RefuseCount',
			text: '已拒收',
			style: 'font-weight:bold;color:#fff;background:#c9302c;',
			width: 55,
			type: 'int',
			align: 'right'
		}, {
			dataIndex: 'AcceptCount',
			text: '接收数',
			style: 'font-weight:bold;color:#fff;background:#5cb85c;',
			width: 65,
			type: 'int',
			align: 'center',
			editor: {
				xtype: 'numberfield',
				minValue: 0,
				allowBlank: false
			},
			defaultRenderer: true
		}, {
			dataIndex: 'RefuseCount',
			text: '拒收数',
			style: 'font-weight:bold;color:#fff;background:#c9302c;',
			width: 65,
			type: 'int',
			align: 'center',
			editor: {
				xtype: 'numberfield',
				minValue: 0,
				allowBlank: false
			},
			renderer: function(value, meta, record) {
				var barCodeMgr=record.get("BmsCenSaleDtl_Goods_BarCodeMgr");
				//if(barCodeMgr == "1") 
				return value;
			}
		}, {
			xtype: 'actioncolumn',
			text: '备注',
			align: 'center',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			menuDisabled: true,
			items: [{
				iconCls: 'button-edit hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
				}
			}]
		}, {
			dataIndex: 'BmsCenSaleDtl_Price',
			sortable: false,
			text: '单价/单位',
			width: 70,
			type: 'float',
			align: 'right',
			renderer: function(value, meta, record) {
				var v = value;
				var goodsUnit = record.get("BmsCenSaleDtl_GoodsUnit");
				if(goodsUnit) v = v + "/" + goodsUnit;
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'BmsCenSaleDtl_SumTotal',
			sortable: false,
			text: '总计金额',
			align: 'right',
			width: 65,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtl_GoodsUnit',
			sortable: false,
			text: '包装单位',
			hidden: true,
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtl_UnitMemo',
			sortable: false,
			text: '包装规格',
			width:80,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtl_TaxRate',
			sortable: false,
			hidden: true,
			text: '税率',
			align: 'right',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtl_ProdDate',
			sortable: false,
			hidden: true,
			text: '生产日期',
			align: 'center',
			width: 90,
			type: 'date',
			isDate: true
		}, {
			dataIndex: 'BmsCenSaleDtl_BiddingNo',
			sortable: false,
			text: '招标号',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtl_Id',
			sortable: false,
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'BmsCenSaleDtl_GoodsSerial',
			sortable: false,
			text: '产品条码',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtl_LotSerial',
			sortable: false,
			text: '批号条码',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtl_PackSerial',
			sortable: false,
			text: '包装单位条码',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtl_MixSerial',
			sortable: false,
			text: '混合条码',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtl_DataAddTime',
			sortable: false,
			text: '新增时间',
			hidden: true,
			width: 90,
			type: 'date',
			isDate: true,
			hasTime: true
		}, {
			dataIndex: 'BmsCenSaleDtl_Goods_EName',
			sortable: false,
			text: '产品英文名',
			hidden: true,
			hideable: false
		}, {
			dataIndex: 'BmsCenSaleDtl_AccepterErrorMsg',
			sortable: false,
			text: '<b style="color:red;">异常信息</b>',
			width: 80,
			hidden:true,
			editor: {
				xtype: 'textarea',
				height: 60
			},
			defaultRenderer: true
		}];
		for(var i=0;i<columns.length;i++){
			if(columns[i].editor){
				columns[i].editor.listeners = {
					beforeedit : function(editor, e) {
						return me.canEdit;
					}
				}
			}
		}
		return columns;
	},

	onResetData: function() {
		var me = this;
	},
	onAcceptScancode: function() {
		var me = this;
	},
	onRefuseScancode: function() {
		var me = this;
	},
	onAllScancode: function() {
		var me = this;
	},
	/**总金额自动计算*/
	onPriceOrGoodsQtyChanged: function(record) {
		var me = this;
		var Price = record.get('BmsCenSaleDtl_Price');
		var GoodsQty = record.get('BmsCenSaleDtl_GoodsQty');

		var SumTotal = parseFloat(Price) * parseInt(GoodsQty);

		record.set('BmsCenSaleDtl_SumTotal', SumTotal);
	},

	/**刷新数据*/
	onSearch: function() {
		var me = this;
		me.ErrorMsg = '';
		me.canEdit = true;
		this.load(null, true);
	},
	/**只看模式*/
	onSearchOnlyRead: function() {
		var me = this;
		me.ErrorMsg = '';
		me.canEdit = false;
		this.load(null, true);
	},

	/**获取数据错误信息*/
	getDataErrorMsg: function() {
		var me = this,
			records = me.store.getModifiedRecords(), //获取修改过的行记录
			len = records.length,
			errorMsg = null;

		for(var i = 0; i < len; i++) {
			var rec = records[i],
				GoodsQty = rec.get('BmsCenSaleDtl_GoodsQty'),
				AcceptCount = rec.get('BmsCenSaleDtl_AcceptCount'),
				AccepterErrorMsg = rec.get('BmsCenSaleDtl_AccepterErrorMsg');

			if(GoodsQty == AcceptCount && AccepterErrorMsg) {
				errorMsg = '验收数量与供货数量一致时，不能填写异常信息，请删除该条异常信息后再操作！';
				break;
			} else if(AcceptCount > GoodsQty) {
				errorMsg = '验收数量不能大于供货数量，请修改后再操作！';
				break;
			} else if(AcceptCount < GoodsQty && !AccepterErrorMsg) {
				errorMsg = '验收数量小于供货数量时，必须填写异常信息，请修改后再操作！';
				break;
			}
		}

		return errorMsg;
	}
});