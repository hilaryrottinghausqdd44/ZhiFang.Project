/**
 * 库存列表
 * @author liangyl
 * @version 2018-03-07
 */
Ext.define('Shell.class.rea.client.reastore.Grid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.DateArea'
	],
	
	title: '库存列表',
	width: 800,
	height: 500,

	/**查询数据*/
	selectUrl: '/ReaManageService.svc/RS_UDTO_SearchReaBmsQtyDtlEntityListByGroupType?isPlanish=true',

	features: [{
		ftype: 'summary'
	}],
	/**默认加载数据*/
	defaultLoad: false,
	/**库存查询的合并选择项Key*/
	ReaBmsStatisticalTypeKey: "ReaBmsStatisticalType",
	/**PDF报表模板*/
	pdfFrx: null,
	/**业务报表类型:对应BTemplateType枚举的key*/
	breportType: 6,
	/**模板/报表类型:Frx;Excel*/
	reaReportClass: "Excel",
	/**模板分类:Excel模板,Frx模板*/
	publicTemplateDir: "Excel模板",
	/**库房是否按库房员工权限获取*/
	isEmpPermission: false,
	/**验证状态Key*/
	ReaGoodsLotVerificationStatus: 'ReaGoodsLotVerificationStatus',
	/**库存标志*/
	ReaBmsQtyDtlMark: "ReaBmsQtyDtlMark",
	/**用户UI配置Key*/
	userUIKey: 'reastore.Grid',
	/**用户UI配置Name*/
	userUIName: "库存列表",
	defaultOrderBy: [{
		property: 'ReaGoods_DispOrder',
		direction: 'ASC'
	}],

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initFilterListeners();
		me.initDateArea(-30);
	},
	initComponent: function() {
		var me = this;
		JShell.REA.StatusList.getStatusList(me.ReaBmsStatisticalTypeKey, false, false, null);
		JShell.REA.StatusList.getStatusList(me.ReaGoodsLotVerificationStatus, false, false, null);
		JShell.REA.StatusList.getStatusList(me.ReaBmsQtyDtlMark, false, true, null);
		me.initIsUseEmp();
		me.selectUrl = me.selectUrl + "&isEmpPermission=" + me.isEmpPermission;
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	initIsUseEmp: function() {
		var me = this;
		//系统运行参数"是否启用库存库房权限":1:是;2:否;
		var isUseEmp = JcallShell.REA.RunParams.Lists.ReaBmsQtyDtlIsUseEmp.Value;
		if (!isUseEmp) {
			JShell.REA.RunParams.getRunParamsValue("ReaBmsQtyDtlIsUseEmp", false, function(data) {
				isUseEmp = JcallShell.REA.RunParams.Lists.ReaBmsQtyDtlIsUseEmp.Value;
				if (isUseEmp && (isUseEmp == 1 || isUseEmp == "1" || isUseEmp == "true")) {
					me.isEmpPermission = true;
				}
			});
		} else {
			isUseEmp = "" + isUseEmp;
			if (isUseEmp == 1 || isUseEmp == "1" || isUseEmp == "true") {
				me.isEmpPermission = true;
			}
		}
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'ReaBmsQtyDtl_BarCodeType',
			text: '条码类型',
			width: 65,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_CompanyName',
			text: '供应商',
			flex: 1,
			minWidth: 120,
			defaultRenderer: true,
		}, {
			dataIndex: 'ReaBmsQtyDtl_GoodsID',
			text: '货品ID',
			width: 155,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_ReaGoods_DispOrder',
			text: '货品次序',
			//hidden: true,
			width: 90,
			defaultRenderer: true,
			doSort: function(state) {
				//自定义排序字段,因为联合查询机构货品信息,可按机构货品排序
				var field = "ReaGoods_DispOrder";
				me.store.sort({
					property: field,
					direction: state
				});
			}
		}, {
			dataIndex: 'ReaBmsQtyDtl_GoodsName',
			text: '货品名称',
			flex: 1,
			minWidth: 120,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("ReaBmsQtyDtl_BarCodeType");
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
			dataIndex: 'ReaBmsQtyDtl_SName',
			text: '货品简称',
			width: 90,
			sortable:false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_LotNo',
			text: '货品批号',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_ProdOrgName',
			text: '厂家',
			width: 90,
			sortable:false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_ReaCompanyID',
			text: '本地供应商ID',
			width: 90,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_StorageID',
			text: '库房ID',
			width: 90,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_IsNeedPerformanceTest',
			text: '是否需要验证',
			width: 80,
			align: 'center',
			type: 'bool',
			isBool: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_VerificationStatus',
			width: 80,
			text: '性能验证',
			renderer: function(value, meta) {
				var v = value;
				if (JShell.REA.StatusList.Status[me.ReaGoodsLotVerificationStatus].Enum != null)
					v = JShell.REA.StatusList.Status[me.ReaGoodsLotVerificationStatus].Enum[value];
				var bColor = "";
				if (JShell.REA.StatusList.Status[me.ReaGoodsLotVerificationStatus].BGColor != null)
					bColor = JShell.REA.StatusList.Status[me.ReaGoodsLotVerificationStatus].BGColor[value];
				var fColor = "";
				if (JShell.REA.StatusList.Status[me.ReaGoodsLotVerificationStatus].FColor != null)
					fColor = JShell.REA.StatusList.Status[me.ReaGoodsLotVerificationStatus].FColor[value];
				var style = 'font-weight:bold;';
				if (bColor) {
					style = style + "background-color:" + bColor + ";";
				}
				if (fColor) {
					style = style + "color:" + fColor + ";";
				}
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = style;
				return v;
			}
		}, {
			dataIndex: 'ReaBmsQtyDtl_ProdDate',
			text: '生产日期',
			isDate: true,
			width: 85,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_InvalidDate',
			text: '有效期至',
			//isDate: true,
			width: 85,
			renderer: function(curValue, meta, record, rowIndex, colIndex, s, view) {
				var bgColor = "";
				var value = curValue;
				var qtipValue = curValue;
				if (value) {
					var Sysdate = JShell.System.Date.getDate();
					value = Ext.util.Format.date(value, 'Y-m-d');
					Sysdate = Ext.util.Format.date(Sysdate, 'Y-m-d');
					Sysdate = JShell.Date.getDate(Sysdate);
					var RegisterInvalidDate = value;
					RegisterInvalidDate = JShell.Date.getDate(RegisterInvalidDate);
					var days = parseInt((RegisterInvalidDate - Sysdate) / 1000 / 60 / 60 / 24);
					if (days < 0) {
						bgColor = "red";
						qtipValue = "已失效";
					} else if (days >= 0 && days <= 30) {
						bgColor = "#e97f36";
						qtipValue = "30天内到期";
					} else if (days > 30) {
						bgColor = "#568f36";
					}
				}
				if (curValue) curValue = Ext.util.Format.date(curValue, 'Y-m-d');
				meta.tdAttr = 'data-qtip="' + qtipValue + '"';
				if (bgColor) meta.style = 'background-color:' + bgColor + ';color:#ffffff;';
				return curValue;
			}
		}, {
			dataIndex: 'ReaBmsQtyDtl_GoodsQty',
			text: '库存数',
			width: 95,
			renderer: function(value, meta) {
				var v = value;
				if (v == 0 || v == "0") {
					var style = 'font-weight:bold;';
					style = style + "background-color:red;";
					style = style + "color:#ffffff;";
					meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
					meta.style = style;
				}
				return v;
			}
		}, {
			dataIndex: 'ReaBmsQtyDtl_ReaTestQty',
			text: '理论测试数',
			width: 85,
			hidden: true,
			sortable: false,
			doSort: function(state) {
				var field = "ReaGoods_ReaTestQty";
				me.store.sort({
					property: field,
					direction: state
				});
			},
			renderer: function(value, meta) {
				var v = value;
				if (v == 0 || v == "0") {
					var style = 'font-weight:bold;';
					style = style + "background-color:red;";
					style = style + "color:#ffffff;";
					meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
					meta.style = style;
				}
				return v;
			}
		}, {
			dataIndex: 'ReaBmsQtyDtl_StocSurplusTestQty',
			text: '库存所剩理论测试数',
			width: 125,
			hidden: true,
			sortable: false,
			renderer: function(value, meta) {
				var v = value;
				if (v == 0 || v == "0") {
					var style = 'font-weight:bold;';
					style = style + "background-color:red;";
					style = style + "color:#ffffff;";
					meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
					meta.style = style;
				}
				return v;
			}
		}, {
			dataIndex: 'ReaBmsQtyDtl_GoodsUnit',
			text: '单位',
			width: 65,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_UnitMemo',
			text: '规格',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_Price',
			text: '单价',
			width: 85,
			type: 'float',
			xtype: 'numbercolumn',
			format: '0.00',
			renderer: function(value, meta) {
				var v = value;
				if (v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			}
		}, {
			dataIndex: 'ReaBmsQtyDtl_SumTotal',
			text: '总计金额',
			width: 130,
			type: 'float',
			summaryType: 'sum',
			xtype: 'numbercolumn',
			format: '0.00',
			renderer: function(value, meta) {
				var v = value;
				if (v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			},
			summaryRenderer: function(value, summaryData, dataIndex) {
				return '<span style="font-weight:bold;font-size:12px;color:blue;">共' + Ext.util.Format.currency(value, '￥', 2) +
					'元</span>';
			}
		}, {
			dataIndex: 'ReaBmsQtyDtl_InDocNo',
			text: '入库批次',
			width: 135,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_DataAddTime',
			text: '入库时间',
			isDate: true,
			hasTime: true,
			width: 135,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_LotSerial',
			text: '一维批条码',
			hidden: true,
			width: 135,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_LotQRCode',
			text: '二维批条码',
			hidden: true,
			width: 135,
			defaultRenderer: true
		}, {
			xtype: 'actioncolumn',
			text: '库存变化跟踪',
			align: 'center',
			width: 85,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					return 'button-show hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var goodsID = rec.get('ReaBmsQtyDtl_GoodsID') + '';
					me.openQtyOper(goodsID);
				}
			}]
		}, {
			xtype: 'actioncolumn',
			text: '盒条码操作',
			align: 'center',
			width: 90,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					var barCodeMgr = record.get("ReaBmsQtyDtl_BarCodeType");
					if (!barCodeMgr) barCodeMgr = "";
					if (barCodeMgr == "1") {
						return 'button-show hand';
					} else {
						return '';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.openBarcodeOper(rec);
				}
			}]
		}, {
			xtype: 'actioncolumn',
			text: '盒条码',
			align: 'center',
			width: 90,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					var barCodeMgr = record.get("ReaBmsQtyDtl_BarCodeType");
					if (!barCodeMgr) barCodeMgr = "";
					if (barCodeMgr == "1") {
						var buttonsToolbar = me.getComponent('buttonsToolbar');
						var cbMerge = buttonsToolbar.getComponent('cbMerge').getValue();
						var buttonsToolbar = me.getComponent('buttonsToolbar');
						var groupType = buttonsToolbar.getComponent('cmReaBmsStatisticalType').getValue();
						//按货品批号库房货架:6;按供应商货品批号库房货架:7;按库存记录:9;
						if (groupType == "6" || groupType == "7" || groupType == "9" || cbMerge == false) {
							return 'button-show hand';
						} else {
							return '';
						}
					} else {
						return '';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.openOverBarcodeOper(rec);
				}
			}]
		}, {
			dataIndex: 'ReaBmsQtyDtl_StorageName',
			text: '库房',
			width: 100,
			hidden: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_PlaceName',
			text: '货架',
			width: 100,
			hidden: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_QtyDtlMark',
			width: 250,
			hidden: true,
			text: '库存标志',
			renderer: function(value, meta) {
				var v = value;
				if (JShell.REA.StatusList.Status[me.ReaBmsQtyDtlMark].Enum != null)
					v = JShell.REA.StatusList.Status[me.ReaBmsQtyDtlMark].Enum[value];
				var bColor = "";
				if (JShell.REA.StatusList.Status[me.ReaBmsQtyDtlMark].BGColor != null)
					bColor = JShell.REA.StatusList.Status[me.ReaBmsQtyDtlMark].BGColor[value];
				var fColor = "";
				if (JShell.REA.StatusList.Status[me.ReaBmsQtyDtlMark].FColor != null)
					fColor = JShell.REA.StatusList.Status[me.ReaBmsQtyDtlMark].FColor[value];
				var style = 'font-weight:bold;';
				if (bColor) {
					style = style + "background-color:" + bColor + ";";
				}
				if (fColor) {
					style = style + "color:" + fColor + ";";
				}
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = style;
				return v;
			}
		}, {
			dataIndex: 'ReaBmsQtyDtl_TaxRate',
			text: '税率',
			width: 65,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_ReaGoodsNo',
			text: '货品编码',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_ProdGoodsNo',
			text: '厂商货品编码',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_CenOrgGoodsNo',
			text: '供应商货品编码',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_RegisterNo',
			text: '注册证编号',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_Memo',
			text: '备注',
			flex: 1,
			minWidth: 120,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_Id',
			text: '主键ID',
			hidden: true,
			isKey: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_InDtlID',
			text: '入库明细ID',
			hidden: true,
			hideable: false
		}];

		return columns;
	},
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			buttonsToolbar = me.callParent(arguments);
		buttonsToolbar.border = false;
		return buttonsToolbar;
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.callParent(arguments);
		items.push(me.createButtonToolbar1Items());
		return items;
	},
	/**默认按钮栏*/
	createButtonToolbar1Items: function() {
		var me = this;
		var qtyDtlMarkList = JShell.REA.StatusList.Status[me.ReaBmsQtyDtlMark].List || [];
		var items = [];
		items.push({
			boxLabel: '按日期',
			name: 'cboIsDatearea',
			itemId: 'cboIsDatearea',
			xtype: 'checkboxfield',
			inputValue: false,
			checked: false,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.onSearch();
				}
			}
		}, {
			xtype: 'uxdatearea',
			itemId: 'date',
			width: 185,
			labelWidth: 0,
			labelAlign: 'right',
			fieldLabel: '',
			listeners: {
				enter: function() {
					me.onSearch();
				}
			}
		});
		items.push({
			fieldLabel: '',
			emptyText: '库存标志选择',
			labelWidth: 0,
			width: 245,
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			itemId: 'sReaBmsQtyDtlMark',
			name: 'sReaBmsQtyDtlMark',
			value: "",
			data: qtyDtlMarkList,
			listeners: {
				select: function(com, records, eOpts) {
					me.onSearch();
				}
			}
		});
		items = me.createDeptNameItems(items);
		items.push({
			labelWidth: 0,
			width: 120,
			fieldLabel: '',
			emptyText: '所属品牌',
			emptyText: '货品品牌选择',
			name: 'ReaGoods_ProdOrgName',
			itemId: 'ReaGoods_ProdOrgName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.sysbase.dict.CheckGrid',
			classConfig: {
				title: '品牌选择',
				defaultWhere: "bdict.BDictType.DictTypeCode='ProdOrg'"
			},
			listeners: {
				check: function(p, record) {
					me.onProdOrgAccept(p, record);
				}
			}
		}, {
			fieldLabel: '品牌主键ID',
			itemId: 'ReaGoods_ProdId',
			name: 'ReaGoods_ProdId',
			xtype: 'textfield',
			hidden: true
		});
		//查询框信息
		me.searchInfo = {
			width: 165,
			isLike: true,
			itemId: 'Search',
			emptyText: '货品名称/货品编码/批号',
			fields: ['reabmsqtydtl.GoodsName', 'reabmsqtydtl.ReaGoodsNo', 'reabmsqtydtl.LotNo']
		};
		items.push('->', {
			type: 'search',
			info: me.searchInfo
		});
		items.push({
			xtype: 'button',
			iconCls: 'button-search',
			text: '查询',
			tooltip: '查询操作',
			handler: function() {
				me.onSearch();
			}
		});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			border: false,
			itemId: 'buttonsToolbar1',
			items: items
		});
	},
	/**
	 * 品牌选择后处理
	 * @param {Object} p
	 * @param {Object} record
	 */
	onProdOrgAccept: function(p, record) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar1');
		var Id = buttonsToolbar.getComponent('ReaGoods_ProdId');
		var CName = buttonsToolbar.getComponent('ReaGoods_ProdOrgName');
		Id.setValue(record ? record.get('BDict_Id') : '');
		CName.setValue(record ? record.get('BDict_CName') : '');
		p.close();
		me.onSearch();
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh'];
		var list = JShell.REA.StatusList.Status[me.ReaBmsStatisticalTypeKey].List;
		items.push('-', {
			boxLabel: '库存数为0',
			name: 'cbstoreNum',
			itemId: 'cbstoreNum',
			xtype: 'checkboxfield',
			inputValue: 'true',
			checked: true,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.onSearch();
				}
			}
		}, '-', {
			boxLabel: '合并',
			name: 'cbMerge',
			itemId: 'cbMerge',
			xtype: 'checkboxfield',
			inputValue: true,
			checked: true,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {

				}
			}
		}, {
			fieldLabel: '',
			emptyText: '库存货品合并方式',
			labelWidth: 0,
			width: 175,
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			itemId: 'cmReaBmsStatisticalType',
			name: 'cmReaBmsStatisticalType',
			value: "1",
			data: list,
			listeners: {
				select: function(com, records, eOpts) {
					//先按报表合并方式更新显示列信息
					me.changeGridColumns();
					me.onSearch();
				}
			}
		});
		items.push('-', {
			emptyText: '一级分类',
			labelWidth: 0,
			width: 115,
			itemId: 'GoodsClass',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.client.goodsclass.GoodsCheck',
			classConfig: {
				title: '一级分类',
				ClassType: "GoodsClass"
			},
			listeners: {
				check: function(p, record) {
					me.onGoodsClass(p, record, 'GoodsClass');
				}
			}
		}, {
			emptyText: '二级分类',
			labelWidth: 0,
			width: 135,
			itemId: 'GoodsClassType',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.client.goodsclass.GoodsCheck',
			classConfig: {
				title: '二级分类',
				ClassType: "GoodsClassType"
			},
			listeners: {
				check: function(p, record) {
					me.onGoodsClass(p, record, 'GoodsClassType');
				}
			}
		});
		items.push({
			fieldLabel: '',
			name: 'CompanyName',
			itemId: 'CompanyName',
			xtype: 'uxCheckTrigger',
			emptyText: '供应商选择',
			width: 135,
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
		}, {
			fieldLabel: '',
			labelWidth: 0,
			width: 135,
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
		});
		items.push('-');
		items = me.createTemplate(items);
		items.push('-', {
			text: '导出',
			tooltip: 'EXCEL导出',
			iconCls: 'file-excel',
			xtype: 'button',
			name: 'EXCEL',
			itemId: 'EXCEL',
			handler: function() {
				me.onDownLoadExcel();
			}
		});

		return items;
	},
	/**创建部门*/
	createDeptNameItems: function(items) {
		var me = this;
		if (!items) {
			items = [];
		}
		//根据登录者的部门id 查询
		var deptId = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTID) || "";
		var deptName = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTNAME) || "";
		items.push({
			boxLabel: '按部门货品',
			name: 'cboDeptGoods',
			itemId: 'cboDeptGoods',
			xtype: 'checkboxfield',
			inputValue: true,
			checked: false,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					if (newValue == true) {
						me.getComponent('buttonsToolbar1').getComponent('DeptName').setDisabled(false);
					} else {
						me.getComponent('buttonsToolbar1').getComponent('DeptName').setDisabled(true);
					}
					me.onSearch();
				}
			}
		}, {
			fieldLabel: '',
			emptyText: '按选择部门全部货品过滤',
			name: 'DeptName',
			itemId: 'DeptName',
			width: 120,
			labelWidth: 0,
			snotField: true,
			xtype: 'uxCheckTrigger',
			enableKeyEvents: false,
			editable: false,
			value: deptName,
			className: 'Shell.class.rea.client.CheckOrgTree',
			classConfig: {
				resizable: false,
				/**是否显示根节点*/
				rootVisible: false,
				/**显示所有部门树:false;只显示用户自己的树:true*/
				ISOWN: true
			},
			listeners: {
				check: function(p, record) {
					if (record && record.data && record.data.tid == 0) {
						JShell.Msg.alert('不能选择所有机构根节点', null, 2000);
						return;
					}
					me.onDeptAccept(p, record);
					p.close();
				}
			}
		}, {
			fieldLabel: '部门主键ID',
			xtype: 'textfield',
			hidden: true,
			name: 'DeptID',
			itemId: 'DeptID',
			value: deptId
		});
		return items;
	},
	/**部门选择*/
	onDeptAccept: function(p, record) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar1');
		var Id = buttonsToolbar.getComponent('DeptID'),
			CName = buttonsToolbar.getComponent('DeptName');
		if (!Id) {
			p.close();
			JShell.Msg.overwrite('onDeptAccept');
			return;
		}
		if (record == null) {
			CName.setValue('');
			Id.setValue('');
			p.close();
			return;
		}
		var text = record.data ? record.data.text : '';
		if (text && text.indexOf("]") >= 0) {
			text = text.split("]")[1];
			text = Ext.String.trim(text);
		}
		Id.setValue(record.data ? record.data.tid : '');
		CName.setValue(text);
	},
	onGoodsClass: function(p, record, classType) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var classTypeCom = buttonsToolbar.getComponent(classType);
		classTypeCom.setValue(record ? record.get('ReaGoodsClassVO_CName') : '');
		p.close();
		me.onSearch();
	},
	/**模板选择项*/
	createTemplate: function(items) {
		var me = this;
		if (!items) {
			items = [];
		}
		items.push({
			fieldLabel: '',
			emptyText: '模板选择',
			labelWidth: 0,
			width: 135,
			name: 'cboTemplate',
			itemId: 'cboTemplate',
			xtype: 'uxCheckTrigger',
			classConfig: {
				width: 195,
				height: 460,
				checkOne: true,
				breportType: me.breportType,
				/**模板分类:Excel模板,Frx模板*/
				publicTemplateDir: me.publicTemplateDir
			},
			className: 'Shell.class.rea.client.template.CheckGrid',
			listeners: {
				check: function(p, record) {
					me.onTemplateCheck(p, record);
				}
			}
		});
		return items;
	},
	onTemplateCheck: function(p, record) {
		var me = this;
		var buttonsToolbar = me.getComponent("buttonsToolbar");
		var cbo = buttonsToolbar.getComponent("cboTemplate");
		var cname = "";
		if (record) {
			me.pdfFrx = record.get("FileName");
			cname = record.get("CName");
		}
		if (cbo) {
			cbo.setValue(cname);
		}
		p.close();
	},
	/**选择试剂耗材信息,导出EXCEL*/
	onDownLoadExcel: function() {
		var me = this,
			operateType = '0';

		if (!me.reaReportClass || me.reaReportClass != "Excel") {
			JShell.Msg.error("请先选择Excel模板后再操作!");
			return;
		}
		if (!me.pdfFrx) {
			JShell.Msg.error("请先选择Excel模板后再操作!");
			return;
		}
		var qtyHql = me.getQtyHql();
		if (!qtyHql) {
			JShell.Msg.error("获取导出试剂库存查询条件信息为空!");
			return;
		}
		//部门货品查询条件
		var deptGoodsHql = me.getDeptGoodsHql();
		//机构货品查询条件
		var reaGoodsHql = me.getReaGoodsHql();
		var url = JShell.System.Path.getRootUrl("/ReaManageService.svc/RS_UDTO_SearchReaBmsQtyDtlOfExcelByQtyHql");
		var params = [];

		params.push("operateType=" + operateType);
		params.push("breportType=" + me.breportType);
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var groupType = buttonsToolbar.getComponent('cmReaBmsStatisticalType').getValue();
		if (!groupType) {
			groupType = "0";
		}
		params.push("groupType=" + groupType);
		params.push("qtyHql=" + qtyHql);
		params.push("deptGoodsHql=" + deptGoodsHql);
		params.push("reaGoodsHql=" + reaGoodsHql);
		if (me.pdfFrx) {
			params.push("frx=" + JShell.String.encode(me.pdfFrx));
		}

		url += "?" + params.join("&");
		window.open(url);
	},
	/**订货方选择*/
	onCompAccept: function(p, record) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var Id = buttonsToolbar.getComponent('CompanyID');
		var CName = buttonsToolbar.getComponent('CompanyName');
		if (record == null) {
			CName.setValue('');
			Id.setValue('');
			p.close();
			me.onSearch();
			return;
		}
		if (record.data) {
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
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;

		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
		me.changeGridColumns();
		//库存查询条件
		var qtyHql = me.getQtyHql();
		if (qtyHql) {
			url += '&where=' + JShell.String.encode(qtyHql);
		}
		//部门货品查询条件
		var deptGoodsHql = me.getDeptGoodsHql();
		if (deptGoodsHql) {
			url += '&deptGoodsHql=' + JShell.String.encode(deptGoodsHql);
		}
		//机构货品查询条件
		var reaGoodsHql = me.getReaGoodsHql();
		if (reaGoodsHql) {
			url += '&reaGoodsHql=' + JShell.String.encode(reaGoodsHql);
		}
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		if (buttonsToolbar) {
			var groupType = buttonsToolbar.getComponent('cmReaBmsStatisticalType').getValue();
			if (!groupType) {
				//没有选择内容时
				url += '&groupType=0'
			} else {
				url += '&groupType=' + groupType;
			}
		}
		return url;
	},
	setExternalWhere: function(hql) {
		var me = this;
		me.externalWhere = hql;
	},
	/**库存查询条件*/
	getQtyHql: function() {
		var me = this,
			arr = [];

		//默认条件
		if (me.defaultWhere && me.defaultWhere != '') {
			arr.push(me.defaultWhere);
		}
		//内部条件
		if (me.internalWhere && me.internalWhere != '') {
			arr.push(me.internalWhere);
		}
		//外部条件
		if (me.externalWhere && me.externalWhere != '') {
			arr.push(me.externalWhere);
		}

		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var GoodsID = buttonsToolbar.getComponent('GoodsID').getValue();
		var CompanyID = buttonsToolbar.getComponent('CompanyID').getValue();
		var storeNum = buttonsToolbar.getComponent('cbstoreNum').getValue();

		var buttonsToolbar1 = me.getComponent('buttonsToolbar1');
		var date = buttonsToolbar1.getComponent('date');
		var isDatearea = buttonsToolbar1.getComponent('cboIsDatearea').getValue();
		var qtyDtlMark = "" + buttonsToolbar1.getComponent('sReaBmsQtyDtlMark').getValue();
		if (qtyDtlMark) {
			arr.push('reabmsqtydtl.QtyDtlMark=' + qtyDtlMark);
		}
		//货品	
		if (GoodsID) {
			arr.push('reabmsqtydtl.GoodsID=' + GoodsID);
		}
		//供应商	
		if (CompanyID) {
			arr.push('reabmsqtydtl.ReaCompanyID=' + CompanyID);
		}
		//库存数量大于0
		if (!storeNum) {
			arr.push('reabmsqtydtl.GoodsQty>0');
		} else {
			arr.push('reabmsqtydtl.GoodsQty=0 or reabmsqtydtl.GoodsQty>0');
		}

		if (isDatearea == true && date) {
			var dateValue = date.getValue();
			if (dateValue) {
				if (dateValue.start) {
					arr.push('reabmsqtydtl.DataAddTime' + ">='" + JShell.Date.toString(dateValue.start, true) + " 00:00:00'");
				}
				if (dateValue.end) {
					arr.push('reabmsqtydtl.DataAddTime' + "<'" + JShell.Date.toString(JShell.Date.getNextDate(dateValue.end), true) +
						"'");
				}
			}
		}
		var where = "";
		if (arr && arr.length > 0) where = arr.join(") and (");
		if (where) where = "(" + where + ")";
		return where;
	},
	/**部门货品查询条件*/
	getDeptGoodsHql: function() {
		var me = this,
			arr = [];
		var buttonsToolbar1 = me.getComponent('buttonsToolbar1'),
			deptID = buttonsToolbar1.getComponent('DeptID').getValue(),
			cboDeptGoods = buttonsToolbar1.getComponent('cboDeptGoods').getValue();
		//部门ID	
		if (deptID && cboDeptGoods == true) {
			arr.push('readeptgoods.DeptID=' + deptID);
		}
		var where = "";
		if (arr && arr.length > 0) where = arr.join(") and (");
		if (where) where = "(" + where + ")";
		return where;
	},
	/**机构货品查询条件*/
	getReaGoodsHql: function() {
		var me = this,
			arr = [];
		var buttonsToolbar = me.getComponent('buttonsToolbar'),
			goodsClass = buttonsToolbar.getComponent('GoodsClass').getValue(),
			goodsClassType = buttonsToolbar.getComponent('GoodsClassType').getValue();

		var buttonsToolbar1 = me.getComponent('buttonsToolbar1');
		var prodId = buttonsToolbar1.getComponent('ReaGoods_ProdId').getValue();
		var prodOrgName = buttonsToolbar1.getComponent('ReaGoods_ProdOrgName').getValue();

		//默认加上机构货品条件(需要按机构货品进行排序,要联查机构货品表)
		arr.push(" 1=1 ");
		//一级分类	
		if (goodsClass) {
			arr.push("reagoods.GoodsClass='" + goodsClass + "'");
		}
		//二级分类	
		if (goodsClassType) {
			arr.push("reagoods.GoodsClassType='" + goodsClassType + "'");
		}
		//品牌
		if (prodOrgName) {
			arr.push("reagoods.ProdOrgName='" + prodOrgName + "'");
		}
		var where = "";
		if (arr && arr.length > 0) where = arr.join(") and (");
		if (where) where = "(" + where + ")";
		return where;
	},
	/**显示操作记录信息*/
	openQtyOper: function(goodsID) {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.99;
		var height = document.body.clientHeight * 0.98;
		JShell.Win.open('Shell.class.rea.client.qtyoperation.ShowGrid', {
			height: height,
			width: maxWidth,
			SUB_WIN_NO: '2',
			GoodsID: goodsID,
			listeners: {
				beforeclose: function(p, eOpts) {
					var plugin = p.getPlugin(p.cellpluginId);
					if (plugin) {
						plugin.cancelEdit();
					}
				}
			}
		}).show();
	},
	/**显示库存货品的同库房同批号的全部条码操作记录信息*/
	openBarcodeOper: function(rec) {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.99;
		var height = document.body.clientHeight * 0.98;
		var arr = [];

		if (rec.get("ReaBmsQtyDtl_ReaCompanyID")) {
			arr.push("reagoodsbarcodeoperation.ReaCompanyID=" + rec.get("ReaBmsQtyDtl_ReaCompanyID") + "");
		}
		if (rec.get("ReaBmsQtyDtl_StorageID")) {
			arr.push("reagoodsbarcodeoperation.StorageID=" + rec.get("ReaBmsQtyDtl_StorageID") + "");
		}
		if (rec.get("ReaBmsQtyDtl_PlaceID")) {
			arr.push("reagoodsbarcodeoperation.PlaceID=" + rec.get("ReaBmsQtyDtl_PlaceID") + "");
		}
		if (rec.get("ReaBmsQtyDtl_GoodsID")) {
			arr.push("reagoodsbarcodeoperation.GoodsID=" + rec.get("ReaBmsQtyDtl_GoodsID") + "");
		}
		if (rec.get("ReaBmsQtyDtl_LotNo")) {
			arr.push("reagoodsbarcodeoperation.LotNo='" + rec.get("ReaBmsQtyDtl_LotNo") + "'");
		}
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		if (buttonsToolbar) {
			var groupType = buttonsToolbar.getComponent('cmReaBmsStatisticalType').getValue();
			//按库存记录
			if (groupType == "9") {
				arr.push("reagoodsbarcodeoperation.QtyDtlID=" + rec.get("ReaBmsQtyDtl_Id") + "");
			}
		}
		var where = "";
		if (arr && arr.length > 0) where = arr.join(") and (");
		if (where) where = "(" + where + ")";
		JShell.Win.open('Shell.class.rea.client.barcodeoperation.ShowGrid', {
			height: height,
			width: maxWidth,
			SUB_WIN_NO: '1',
			externalWhere: where,
			listeners: {
				beforeclose: function(p, eOpts) {
					var plugin = p.getPlugin(p.cellpluginId);
					if (plugin) {
						plugin.cancelEdit();
					}
				}
			}
		}).show();
	},
	/**查看库存货品的剩余条码信息*/
	openOverBarcodeOper: function(rec) {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.99;
		var height = document.body.clientHeight * 0.98;

		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var cbMerge = buttonsToolbar.getComponent('cbMerge').getValue();
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var groupType = buttonsToolbar.getComponent('cmReaBmsStatisticalType').getValue();
		var arr = [];
		//按货品批号库房货架:6;按供应商货品批号库房货架:7;按库存记录:9;
		if (groupType == "9" || cbMerge == false) {
			arr.push("reagoodsbarcodeoperation.QtyDtlID=" + rec.get("ReaBmsQtyDtl_Id") + "");
		} else {
			arr.push("reagoodsbarcodeoperation.GoodsID=" + rec.get("ReaBmsQtyDtl_GoodsID") + "");
			arr.push("reagoodsbarcodeoperation.LotNo='" + rec.get("ReaBmsQtyDtl_LotNo") + "'");
			if (rec.get("ReaBmsQtyDtl_StorageID")) {
				arr.push("reagoodsbarcodeoperation.StorageID=" + rec.get("ReaBmsQtyDtl_StorageID") + "");
			}
			if (rec.get("ReaBmsQtyDtl_PlaceID")) {
				arr.push("reagoodsbarcodeoperation.PlaceID=" + rec.get("ReaBmsQtyDtl_PlaceID") + "");
			}
			if (groupType == "7") {
				if (rec.get("ReaBmsQtyDtl_ReaCompanyID")) {
					arr.push("reagoodsbarcodeoperation.ReaCompanyID=" + rec.get("ReaBmsQtyDtl_ReaCompanyID") + "");
				}
			}
		}
		var where = "";
		if (arr && arr.length > 0) where = arr.join(") and (");
		if (where) where = "(" + where + ")";

		JShell.Win.open('Shell.class.rea.client.barcodeoperation.ShowGrid', {
			height: height,
			width: maxWidth,
			SUB_WIN_NO: '1',
			/**获取数据服务路径*/
			selectUrl: '/ReaManageService.svc/RS_UDTO_SearchOverReaGoodsBarcodeOperationByHQL?isPlanish=true',
			externalWhere: where,
			listeners: {
				beforeclose: function(p, eOpts) {
					var plugin = p.getPlugin(p.cellpluginId);
					if (plugin) {
						plugin.cancelEdit();
					}
				}
			}
		}).show();
	},
	initFilterListeners: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var cmReaBmsStatisticalType = buttonsToolbar.getComponent('cmReaBmsStatisticalType');
		var cbMerge = buttonsToolbar.getComponent('cbMerge');
		cmReaBmsStatisticalType.disable();

		cbMerge.on({
			change: function(com, newValue, oldValue, eOpts) {
				if (newValue) {
					cmReaBmsStatisticalType.enable();
				} else {
					cmReaBmsStatisticalType.setValue('');
					cmReaBmsStatisticalType.disable();
					me.onSearch();
				}

			}
		});
	},
	/**加载数据后*/
	onAfterLoad: function(records, successful) {
		var me = this;
		me.callParent(arguments);
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var cmReaBmsStatisticalType = buttonsToolbar.getComponent('cmReaBmsStatisticalType');
		var cbMerge = buttonsToolbar.getComponent('cbMerge');
		if (cbMerge.getValue()) return;
		cmReaBmsStatisticalType.disable();
	},
	/**初始化日期范围*/
	initDateArea: function(day) {
		var me = this;
		if (!day) day = 0;
		var edate = JcallShell.System.Date.getDate();
		var sdate = Ext.Date.add(edate, Ext.Date.DAY, day);
		//sdate=Ext.Date.format(sdate,"Y-m-d");
		//edate=Ext.Date.format(edate,"Y-m-d");
		var dateArea = {
			start: sdate,
			end: edate
		};
		var dateareaToolbar = me.getComponent('buttonsToolbar1'),
			date = dateareaToolbar.getComponent('date');
		if (date && dateArea) date.setValue(dateArea);
	},
	// 需求调整：报表合并下拉框触发，改变数据列显示
	changeGridColumns: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var cmReaBmsStatisticalType = buttonsToolbar.getComponent('cmReaBmsStatisticalType');
		var groupType = cmReaBmsStatisticalType.getValue();
		
		switch (groupType) {
			case "1": //按货品规格
				me.changeGridColumns1();
				break;
			default:
				me.changeGridColumns2();
				break;
		}
	},
	//按货品规格
	changeGridColumns1: function() {
		var me = this;
		var columnsShow = ['ReaBmsQtyDtl_ProdOrgName'];
		var columnsHide = ['ReaBmsQtyDtl_CenOrgGoodsNo', 'ReaBmsQtyDtl_RegisterNo', 'ReaBmsQtyDtl_Memo'];
		var index = 0;
		Ext.Array.forEach(me.columns, function(item) {
			if (Ext.Array.contains(columnsShow, item.dataIndex)) {
				me.columns[index].show();
			} 
			if (Ext.Array.contains(columnsHide, item.dataIndex)){
				me.columns[index].hide();
			}
			index = index + 1;
		});
	},
	//其他
	changeGridColumns2: function() {
		var me = this;
		var columnsShow = ['ReaBmsQtyDtl_CenOrgGoodsNo', 'ReaBmsQtyDtl_RegisterNo', 'ReaBmsQtyDtl_Memo'];
		var columnsHide = ['ReaBmsQtyDtl_ProdOrgName'];
		var index = 0;
		Ext.Array.forEach(me.columns, function(item) {
			if (Ext.Array.contains(columnsShow, item.dataIndex)) {
				me.columns[index].show();
			}
			if (Ext.Array.contains(columnsHide, item.dataIndex)) {
				me.columns[index].hide();
			}
			index = index + 1;
		});
	}
});
