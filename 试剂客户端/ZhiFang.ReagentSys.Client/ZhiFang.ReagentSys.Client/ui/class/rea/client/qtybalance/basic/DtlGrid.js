/**
 * 库存结转
 * @author longfc
 * @version 2018-04-13
 */
Ext.define('Shell.class.rea.client.qtybalance.basic.DtlGrid', {
	extend: 'Shell.class.rea.client.basic.DtlGrid',
	requires: [
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '库存结转明细',
	width: 800,
	height: 500,

	/**查询数据*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsQtyBalanceDtlByHQL?isPlanish=true',
	/**默认加载数据*/
	defaultLoad: false,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**同一货品的合并条件*/
	groupByType: 1,
	/**用户UI配置Key*/
	userUIKey: 'qtybalance.basic.DtlGrid',
	/**用户UI配置Name*/
	userUIName: "库存结转明细列表",
	features: [{
		ftype: 'summary'
	}],
	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaBmsQtyBalanceDtl_DataAddTime',
		direction: 'DESC'
	}],
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
		var columns = [{
			dataIndex: 'ReaBmsQtyBalanceDtl_DataAddTime',
			text: '库存日期',
			isDate: true,
			hasTime:true,
			width: 135,
			defaultRenderer: true
		},{
			dataIndex: 'ReaBmsQtyBalanceDtl_BarCodeType',
			text: '条码类型',
			width: 100,
			hidden: true,
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
			dataIndex: 'ReaBmsQtyBalanceDtl_GoodsName',
			text: '货品名称',
			width: 160,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("ReaBmsQtyBalanceDtl_BarCodeType");
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
			xtype: 'actioncolumn',
			text: '操作',
			align: 'center',
			width: 45,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					return 'button-show hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var GoodsID = rec.get('ReaBmsQtyBalanceDtl_GoodsID') + '';
					me.openShowOpForm(GoodsID);
				}
			}]
		}, {
			dataIndex: 'ReaBmsQtyBalanceDtl_GoodsID',
			text: '货品ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyBalanceDtl_LotNo',
			text: '货品批号',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyBalanceDtl_ProdDate',
			text: '生产日期',
			isDate: true,
			width: 85,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyBalanceDtl_InvalidDate',
			text: '有效期至',
			isDate: true,
			width: 85,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyBalanceDtl_CompanyName',
			text: '所属供应商',
			width: 120,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyBalanceDtl_StorageName',
			text: '库房',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyBalanceDtl_PlaceName',
			text: '货架',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyBalanceDtl_GoodsUnit',
			text: '单位',
			width: 65,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyBalanceDtl_StoreLower',
			text: '库存下限',
			width: 70,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyBalanceDtl_StoreUpper',
			text: '库存上限',
			width: 70,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyBalanceDtl_PreGoodsQty',
			text: '上次库存数',
			hidden:true,
			width: 85,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyBalanceDtl_ChangeGoodsQty',
			text: '变化库存数',
			width: 85,
			hidden:true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyBalanceDtl_CalcGoodsQty',
			text: '计算库存数',
			width: 85,
			hidden:true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyBalanceDtl_GoodsQty',
			text: '结转库存数',
			width: 85,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyBalanceDtl_SumTotal',
			text: '结转金额',
			width: 140,
			type: 'float',
			summaryType: 'sum',
			xtype: 'numbercolumn',
			format: '0.00',
			renderer: function(value, meta) {
				var v = value;
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			},
			summaryRenderer: function(value, summaryData, dataIndex) {
				return '<span style="font-weight:bold;font-size:12px;color:blue;">共' + Ext.util.Format.currency(value, '￥', 2) + '元</span>';
			}
		}, {
			dataIndex: 'ReaBmsQtyBalanceDtl_Memo',
			text: '备注',
			flex: 1,
			minWidth: 60,
			defaultRenderer: true
		}];

		return columns;
	},	
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items=me.createFullscreenItems();
		items.push('-','refresh');
		//查询框信息
		me.searchInfo = {
			width: 135,
			isLike: true,
			itemId: 'Search',
			emptyText: '货品名称/批号',
			fields: ['reabmsqtybalancedtl.GoodsName', 'reabmsqtybalancedtl.LotNo']
		};
		items.push('-', {
			fieldLabel: '',
			labelWidth: 0,
			width: 165,
			labelSeparator: '',
			labelAlign: 'right',
			emptyText: '货品选择',
			name: 'GoodsName',
			itemId: 'GoodsName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.client.goods2.basic.CheckGrid',
			classConfig: {
				width: 350,
				checkOne: true,
				title: '货品选择'
			},
			listeners: {
				check: function(p, record) {
					me.onGoodsAccept(p, record);
				}
			}
		}, {
			fieldLabel: '货品主键ID',
			itemId: 'GoodsID',
			name: 'GoodsID',
			xtype: 'textfield',
			hidden: true
		}, {
			fieldLabel: '',
			name: 'CompanyName',
			itemId: 'CompanyName',
			xtype: 'uxCheckTrigger',
			emptyText: '供应商选择',
			width: 165,
			labelWidth: 0,
			labelAlign: 'right',
			className: 'Shell.class.rea.client.reacenorg.CheckTree',
			classConfig: {
				title: '供应商选择',
				resizable: false,
				/**是否显示根节点*/
				rootVisible: false,
				/**机构类型*/
				OrgType: "0"
			},
			listeners: {
				check: function(p, record) {
					me.onCompAccept(p, record);
				}
			}
		}, {
			fieldLabel: '供货商主键ID',
			hidden: true,
			xtype: 'uxCheckTrigger',
			name: 'CompanyID',
			itemId: 'CompanyID'
		});
		items.push('->', {
			type: 'search',
			info: me.searchInfo
		});
		return items;
	},
	/**供货商选择*/
	onCompAccept: function(p, record) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var Id = buttonsToolbar.getComponent('CompanyID');
		var CName = buttonsToolbar.getComponent('CompanyName');
		if(record == null) {
			CName.setValue('');
			Id.setValue('');
			p.close();
			me.onSearch();
			return;
		}
		if(record.data) {
			CName.setValue(record.data ? record.data.text : '');
			Id.setValue(record.data ? record.data.tid : '');
			p.close();
			me.onSearch();
		}
	},
	onGoodsAccept: function(p, record) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var GoodsID = buttonsToolbar.getComponent('GoodsID');
		var GoodsName = buttonsToolbar.getComponent('GoodsName');
		GoodsID.setValue(record ? record.get('ReaGoods_Id') : '');
		GoodsName.setValue(record ? record.get('ReaGoods_CName') : '');
		p.close();
		me.onSearch();
	},
	/**显示操作记录信息*/
	openShowOpForm: function(id) {
		var maxWidth = document.body.clientWidth * 0.99;
		var height = document.body.clientHeight * 0.98;
		JShell.Win.open('Shell.class.rea.client.qtyoperation.ShowGrid', {
			height: height,
			width: maxWidth,
			SUB_WIN_NO: '2',
			GoodsID: id
		}).show();
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			search = null,
			GoodsID = null,
			CompanyID = null,
			params = [];
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		if(!buttonsToolbar) return;
		GoodsID = buttonsToolbar.getComponent('GoodsID').getValue();
		CompanyID = buttonsToolbar.getComponent('CompanyID').getValue();

		me.internalWhere = '';
		if(GoodsID) {
			params.push('reabmsqtybalancedtl.GoodsID=' + GoodsID);
		}
		//供应商	
		if(CompanyID) {
			params.push('reabmsqtybalancedtl.ReaCompanyID=' + CompanyID);
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		if(search) {
			if(me.internalWhere) {
				me.internalWhere += ' and (' + me.getSearchWhere(search) + ')';
			} else {
				me.internalWhere = "("+me.getSearchWhere(search)+")";
			}
		}
		return me.callParent(arguments);
	}
});