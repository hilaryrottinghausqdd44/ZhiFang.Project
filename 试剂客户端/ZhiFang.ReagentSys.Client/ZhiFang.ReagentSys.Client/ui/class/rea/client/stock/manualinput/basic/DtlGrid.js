/**
 * 客户端库存初始化(手工入库)
 * @author longfc
 * @version 2018-03-12
 */
Ext.define('Shell.class.rea.client.stock.manualinput.basic.DtlGrid', {
	extend: 'Shell.class.rea.client.basic.DtlGrid',
	title: '入库明细列表',

	/**获取数据服务路径*/
	selectUrl: '/ReaManageService.svc/ST_UDTO_SearchReaBmsInDtlByHQL?isPlanish=true',

	/**默认加载*/
	defaultLoad: false,
	/**后台排序*/
	remoteSort: true,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,

	/**默认每页数量*/
	defaultPageSize: 50,
	/**分页栏下拉框数据*/
	pageSizeList: [
		[50, 50],
		[100, 100],
		[500, 500],
		[10000, 10000]
	],
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaBmsInDtl_ReaGoodsNo',
		direction: 'ASC'
	}],
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**默认选中*/
	autoSelect: true,
	/**入库总单Id*/
	InDocID: null,
	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',

	BarCodeTypeList: [],
	BarCodeTypeEnum: {},
	BarCodeTypeBGColorEnum: {},
	BarCodeTypeFColorEnum: {},
	BarCodeTypeBGColorEnum: {},
	/**用户UI配置Key*/
	userUIKey: 'stock.manualinput.basic.DtlGrid',
	/**用户UI配置Name*/
	userUIName: "入库明细列表",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			nodata: function() {
				me.enableControl(true);
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.getBarCodeTypeListData();
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
			dataIndex: 'ReaBmsInDtl_StorageName',
			text: '所属库房',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_PlaceName',
			text: '所属货架',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_CompanyName',
			text: '所属供应商',
			width: 105,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_ReaCompCode',
			text: '供应商编码',
			hidden: true,
			defaultRenderer: true
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
			dataIndex: 'ReaBmsInDtl_BarCodeType',
			text: '条码类型',
			hidden: true,
			width: 55,
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
				meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				return v;
			}
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
				meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaBmsInDtl_GoodSName',
			text: '简称',
			width: 90,
			defaultRenderer: true,
			doSort: function(state) {
				var field="ReaGoods_SName";
				me.store.sort({
					property: field,
					direction: state
				});
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
					var barCodeMgr = record.get("ReaBmsInDtl_BarCodeType");
					if(!barCodeMgr) barCodeMgr = "";
					if(barCodeMgr == "1") {
						return 'button-show hand';
					} else {
						return '';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var GoodsID = rec.get('ReaBmsInDtl_Id') + '';
					me.openShowOpForm(GoodsID);
				}
			}]
		},{
			dataIndex: 'ReaBmsInDtl_ProdOrgName',
			text: '厂家名称',
			width: 65,
			defaultRenderer: true,
			doSort: function(state) {
				var field="ReaGoods_ProdOrgName";
				me.store.sort({
					property: field,
					direction: state
				});
			}
		},{
			dataIndex: 'ReaBmsInDtl_GoodsUnit',
			text: '单位',
			width: 65,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_ReaGoods_UnitMemo',
			text: '包装规格',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_LotNo',
			text: '货品批号',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_InvalidDate',
			text: '有效期至',
			width: 80,
			type: 'date',
			isDate: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_Price',
			text: '单价',
			width: 70,
			type: 'float',
			align: 'right',
			renderer: function(value, meta) {
				var v = value || '';
				if(v && ("" + v).indexOf(".") >= 0) {
					v = parseFloat(v).toFixed(2);
					meta.tdAttr = 'data-qtip="<b>' + v + '元</b>"';
				}
				return v;
			}
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
			align: 'right',
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
			dataIndex: 'ReaBmsInDtl_InDtlNo',
			sortable: false,
			text: '入库明细单号',
			width: 100,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_InvalidDate',
			text: '有效期至',
			isDate: true,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_ProdDate',
			text: '生产日期',
			width: 80,
			type: 'date',
			isDate: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_TaxRate',
			text: '税率',
			align: 'right',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_RegisterNo',
			sortable: false,
			text: '注册证编号',
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_RegisterInvalidDate',
			text: '注册证有效期',
			width: 85,
			type: 'date',
			isDate: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_Id',
			sortable: false,
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'ReaBmsInDtl_LotSerial',
			sortable: false,
			text: '批号条码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_SysLotSerial',
			sortable: false,
			text: '系统内部批号条码',
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
			dataIndex: 'ReaBmsInDtl_GoodsNo',
			sortable: false,
			text: '货品平台编码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_ApproveDocNo',
			text: '批准文号',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_GoodsSort',
			text: '货品序号',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_OtherDtlNo',
			sortable: false,
			text: '提取第三方数据的明细单号',
			hidden: true,
			editor: {
				readOnly: true
			},
			defaultRenderer: true
		},{
			dataIndex: 'ReaBmsInDtl_GoodsSerial',
			sortable: false,
			text: 'GoodsSerial',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_ZX1',
			sortable: false,
			text: 'ZX1',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_ZX2',
			sortable: false,
			text: 'ZX2',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_ZX3',
			sortable: false,
			text: 'ZX3',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_FactoryOutTemperature',
			text: '厂家出库温度',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_ArrivalTemperature',
			text: '到货温度',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_AppearanceAcceptance',
			text: '外观验收',
			width: 80,
			defaultRenderer: true
		},{
			dataIndex: 'ReaBmsInDtl_SaleDtlID',
			sortable: false,
			text: '供货明细ID',
			hidden: true,
			defaultRenderer: true
		},{
			dataIndex: 'ReaBmsInDtl_SaleDtlConfirmID',
			sortable: false,
			text: '供货验收明细单ID',
			hidden: true,
			defaultRenderer: true
		}];
		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = me.createFullscreenItems();
		items.push('-', 'refresh');

		items.push('-', {
			fieldLabel: '',
			labelWidth: 0,
			width: 100,
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			itemId: 'DocBarCodeType',
			emptyText: '条码类型选择',
			data: me.BarCodeTypeList,
			listeners: {
				select: function(com, records, eOpts) {
					me.onSearch();
				}
			}
		});
		items.push('-', {
			xtype: 'button',
			iconCls: 'button-print',
			itemId: "btnPrint",
			text: '条码打印',
			tooltip: '条码打印',
			handler: function() {
				me.onPrintClick();
			}
		});
		return items;
	},
	onPrintClick: function() {
		var me = this;
		if(!me.PK) {
			JShell.Msg.error("请选择入库单后再操作!");
			return;
		}
		var records = me.getSelectionModel().getSelection();
		if(records.length <= 0) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		me.onShowPrintPanel(records);
	},
	onShowPrintPanel: function(records) {
		var me = this;
		var idStr = [];
		for(var i = 0; i < records.length; i++) {
			idStr.push(records[i].get(me.PKField));
		}
		var maxWidth = document.body.clientWidth * 0.99;
		var height = document.body.clientHeight * 0.98;

		var config = {
			resizable: true,
			PK: me.PK,
			IDStr: idStr.join(","),
			//SUB_WIN_NO: '1',
			width: maxWidth,
			height: height,
			listeners: {
				beforeclose: function(p, eOpts) {
					var plugin = p.getPlugin(p.cellpluginId);
					if(plugin) {
						plugin.cancelEdit();
					}
				}
			}
		};
		var win = JShell.Win.open('Shell.class.rea.client.printbarcode.indoc.Grid', config);
		win.show();
	},
	/**@description 获取条码类型信息*/
	getBarCodeTypeListData: function(callback) {
		var me = this;
		if(me.BarCodeTypeList.length > 0) return;
		var params = {},
			url = JShell.System.Path.getRootUrl(JcallShell.System.ClassDict._classDicListUrl);
		params = Ext.encode({
			"jsonpara": [{
				"classname": "ReaGoodsBarCodeType",
				"classnamespace": "ZhiFang.Entity.ReagentSys.Client"
			}]
		});
		me.BarCodeTypeList = [];
		var tempArr = [];
		JcallShell.Server.post(url, params, function(data) {
			if(data.success) {
				if(data.value) {
					if(data.value[0].ReaGoodsBarCodeType.length > 0) {
						me.BarCodeTypeList.push(["", '请选择', 'font-weight:bold;text-align:center;']);
						Ext.Array.each(data.value[0].ReaGoodsBarCodeType, function(obj, index) {
							var style = ['font-weight:bold;text-align:center;'];
							if(obj.FontColor) {
								me.BarCodeTypeFColorEnum[obj.Id] = obj.FontColor;
							}
							if(obj.BGColor) {
								style.push('color:' + obj.BGColor);
								me.BarCodeTypeBGColorEnum[obj.Id] = obj.BGColor;
							}
							me.BarCodeTypeEnum[obj.Id] = obj.Name;
							tempArr = [obj.Id, obj.Name, style.join(';')];
							me.BarCodeTypeList.push(tempArr);
						});
					}
				}
			}
		}, false);
	},
	getLoadUrl: function() {
		var me = this;
		me.internalWhere = me.getInternalWhere();
		return me.callParent(arguments);
	},
	/**@description 获取内部条件*/
	getInternalWhere: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var barCodeType = buttonsToolbar.getComponent('DocBarCodeType');
		var search = buttonsToolbar.getComponent('Search');

		var where = [];

		if(barCodeType) {
			var value = barCodeType.getValue();
			if(value)
				where.push("reabmsindtl.BarCodeType=" + value);
		}
		if(search) {
			var value = search.getValue();
			if(value) {
				var searchHql = me.getSearchWhere(value);
				if(searchHql) {
					searchHql = "(" + searchHql + ")";
					where.push(searchHql);
				}
			}
		}
		return where.join(" and ");
	},
	/**显示操作记录信息*/
	openShowOpForm: function(id) {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.99;
		var height = document.body.clientHeight * 0.98;
		var defaultWhere = "reagoodsbarcodeoperation.BDtlID=" + id;
		var win = JShell.Win.open('Shell.class.rea.client.barcodeoperation.dtloper.Grid', {
			title: '入库货品条码记录',
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