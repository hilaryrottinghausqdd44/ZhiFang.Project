/**
 * 入库汇总统计
 * @author longfc
 * @version 2018-09-10
 */
Ext.define('Shell.class.rea.client.statistics.indtl.summary.DtlGrid', {
	extend: 'Shell.class.rea.client.statistics.basic.SearchGrid',

	title: '入库明细汇总统计',

	/**获取数据服务路径*/
	selectUrl: '/ReaStatisticalAnalysisService.svc/RS_UDTO_SearchReaBmsInDtlSummaryByHQL?isPlanish=true',
	/**业务报表类型:对应BTemplateType枚举的key*/
	breportType: 16,
	InDocInTypeList: [],
	features: [{
		ftype: 'summary'
	}],
	/**是否启用模糊查询选择类型*/
	hasSearchType: true,
	/**用户UI配置Key*/
	userUIKey: 'statistics.indtl.summary.DtlGrid',
	/**用户UI配置Name*/
	userUIName: "入库明细汇总统计",
	/**后台排序*/
	remoteSort: true,
	/**入库明细报表合并选择项Key */
	ReaBmsStatisticalTypeKey: "ReaBmsInDtlStatisticalType",

	afterRender: function() {
		var me = this;

		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		// 入库明细报表合并选项
		JShell.REA.StatusList.getStatusList(me.ReaBmsStatisticalTypeKey, false, false, null);
		me.getInDocInTypeList();
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
		me.initDateArea(-30);
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if (me.hasPagingtoolbar) items.push(me.createPagingtoolbar());
		items.push(me.createButtonToolbar1Items());
		items.push(me.createDateAreaButtonToolbar());
		// 需求调整：增加功能栏
		items.push(me.createButtonToolbar3());
		return items;
	},
	/**默认按钮栏*/
	createButtonToolbar1Items: function() {
		var me = this;

		var items = [];
		items.push({
			boxLabel: '合并是否带上冷链信息',
			name: 'isOfColdInfoMerge',
			itemId: 'isOfColdInfoMerge',
			xtype: 'checkboxfield',
			inputValue: 'true',
			checked: false,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.changeCloudInfo();
					me.onSearch();
				}
			}
		}, {
			fieldLabel: '',
			emptyText: '入库类型选择',
			name: 'InType',
			itemId: 'InType',
			xtype: 'uxSimpleComboBox',
			hasStyle: true,
			labelWidth: 0,
			width: 75,
			labelAlign: 'right',
			data: me.InDocInTypeList,
			value: '',
			listeners: {
				change: function() {
					me.onSearch();
				}
			}
		});
		items = me.createCompanyNameItems(items);
		items = me.createStorageNameItems(items);
		items = me.createGoodsClassItems(items);
		items = me.createGoodsClassTypeItems(items);
		items = me.createGoodsNameItems(items);
		items = me.createSearchItems(items);
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
	// 需求调整
	/**按钮栏3*/
	createButtonToolbar3: function() {
		var me = this;
		var items = [];
		var list = JShell.REA.StatusList.Status[me.ReaBmsStatisticalTypeKey].List;

		items.push({
			fieldLabel: '',
			emptyText: '报表合并方式',
			labelWidth: 0,
			width: 175,
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			itemId: 'cmReaBmsStatisticalType',
			name: 'cmReaBmsStatisticalType',
			value: "3",
			data: list,
			listeners: {
				select: function(com, records, eOpts) {
					//先按报表合并方式更新显示列信息
					me.changeGridColumns();
					me.onSearch();
				}
			}
		});
		
		items.push({
			xtype: 'textSearchTrigger',
			name: 'txtSearchInvNo',
			itemId: 'txtSearchInvNo',
			emptyText: '发票号',
			width: 165,
			fieldLabel: '',
			labelWidth: 0,
			enableKeyEvents: true,
			listeners: {
				onSearchClick: function(field, value) {
					me.onSearch();
				},
				specialkey: function(field, e) {
					if (e.getKey() == Ext.EventObject.ENTER) {
						me.onSearch();
					}
				}
			}
		});
		items.push({
			xtype: 'textSearchTrigger',
			name: 'txtSearchInDoc',
			itemId: 'txtSearchInDoc',
			emptyText: '入库单号',
			width: 165,
			fieldLabel: '',
			labelWidth: 0,
			enableKeyEvents: true,
			listeners: {
				onSearchClick: function(field, value) {
					me.onSearch();
				},
				specialkey: function(field, e) {
					if (e.getKey() == Ext.EventObject.ENTER) {
						me.onSearch();
					}
				}
			}
		});
		items.push({
			xtype: 'textSearchTrigger',
			name: 'txtSearchSName',
			itemId: 'txtSearchSName',
			emptyText: '简称',
			width: 165,
			fieldLabel: '',
			labelWidth: 0,
			enableKeyEvents: true,
			listeners: {
				onSearchClick: function(field, value) {
					me.onSearch();
				},
				specialkey: function(field, e) {
					if (e.getKey() == Ext.EventObject.ENTER) {
						me.onSearch();
					}
				}
			}
		});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			border: false,
			itemId: 'buttonsToolbar3',
			items: items
		});
	},
	// 需求调整：报表合并下拉框触发，改变数据列显示
	changeGridColumns: function() {
		var me = this;
		var buttonsToolbar3 = me.getComponent('buttonsToolbar3');
		var groupType = buttonsToolbar3.getComponent('cmReaBmsStatisticalType').getValue();
		var txtSearchInvNo = buttonsToolbar3.getComponent('txtSearchInvNo');
		var txtSearchInDoc=buttonsToolbar3.getComponent('txtSearchInDoc');
		var txtSearchSName=buttonsToolbar3.getComponent('txtSearchSName');
		//按入库明细:领用部门ID+供应商ID+使用仪器ID+货品产品编码+包装单位+规格+批号+效期+出库人ID+使用时间
		if (!groupType) groupType = "3";
		switch (groupType) {
			case "1": //按货品规格
				me.changeGridColumns1();
				txtSearchInvNo.setValue("");
				txtSearchInvNo.hide();
				txtSearchInDoc.setValue("");
				txtSearchInDoc.hide();
				txtSearchSName.show();
				break;
			case "2": //按供应商加批号及货品
				me.changeGridColumns2();
				txtSearchInvNo.setValue("");
				txtSearchInvNo.hide();
				txtSearchInDoc.setValue("");
				txtSearchInDoc.hide();
				txtSearchSName.show();
				break;
			case "4"://入库总单号汇总统计
				me.changeGridColumns4();
				txtSearchInvNo.show();
				txtSearchInDoc.show();
				txtSearchSName.setValue("");
				txtSearchSName.hide();
				break;
			default:
				me.changeGridColumns3();
				txtSearchInvNo.show();
				txtSearchInDoc.show();
				txtSearchSName.show();
				break;
		}
	},
	//按货品规格
	changeGridColumns1: function() {
		var me = this;
		var columns = ['ReaBmsInDtl_ProdGoodsNo', 'ReaBmsInDtl_ReaGoodsNo',
			'ReaBmsInDtl_GoodsCName', 'ReaBmsInDtl_GoodsUnit', 'ReaBmsInDtl_UnitMemo',
			'ReaBmsInDtl_Price', 'ReaBmsInDtl_GoodsQty', 'ReaBmsInDtl_SumTotal',
			'ReaBmsInDtl_GoodSName','ReaBmsInDtl_ProdOrgName'
		];
		var index = 0;
		Ext.Array.forEach(me.columns, function(item) {
			if (Ext.Array.contains(columns, item.dataIndex)) {
				me.columns[index].show();
			} else {
				me.columns[index].hide();
			}
			index = index + 1;
		});
	},
	//按供应商加批号及货品
	changeGridColumns2: function() {
		var me = this;
		var columns = ['ReaBmsInDtl_CompanyName', 'ReaBmsInDtl_ReaGoodsNo', 'ReaBmsInDtl_GoodsCName',
			'ReaBmsInDtl_GoodsUnit', 'ReaBmsInDtl_UnitMemo',
			'ReaBmsInDtl_LotNo', 'ReaBmsInDtl_InvalidDate', 'ReaBmsInDtl_GoodsQty',
			'ReaBmsInDtl_SumTotal','ReaBmsInDtl_GoodSName','ReaBmsInDtl_ProdOrgName'
		];
		var index = 0;
		Ext.Array.forEach(me.columns, function(item) {
			if (Ext.Array.contains(columns, item.dataIndex)) {
				me.columns[index].show();
			} else {
				me.columns[index].hide();
			}
			index = index + 1;
		});
	},
	//默认（按入库明细常规合并）
	changeGridColumns3: function() {
		var me = this;
		var columns = ['ReaBmsInDtl_CompanyName', 'ReaBmsInDtl_ReaGoodsNo', 'ReaBmsInDtl_GoodsCName',
			'ReaBmsInDtl_GoodsUnit', 'ReaBmsInDtl_UnitMemo', 'ReaBmsInDtl_LotNo',
			'ReaBmsInDtl_TransportNo', 'ReaBmsInDtl_InvalidDate', 'ReaBmsInDtl_GoodsQty',
			'ReaBmsInDtl_DataAddTime', 'ReaBmsInDtl_SumTotal','ReaBmsInDoc_InDocNo','ReaBmsInDoc_InvoiceNo',
			'ReaBmsInDtl_OrderDocNo','ReaBmsInDoc_RegisterNo','ReaBmsInDoc_NetGoodsNo',
			'ReaBmsInDtl_GoodSName','ReaBmsInDtl_ProdOrgName'
		];
		var index = 0;
		Ext.Array.forEach(me.columns, function(item) {
			if (Ext.Array.contains(columns, item.dataIndex)) {
				me.columns[index].show();
			} else {
				me.columns[index].hide();
			}
			index = index + 1;
		});
		
		// 当勾选冷链信息后，选择按入库明细常规合并会带有冷链信息
		var buttonsToolbar = me.getComponent('buttonsToolbar1');
		var isOfColdInfoMerge = buttonsToolbar.getComponent('isOfColdInfoMerge').getValue();
		if(isOfColdInfoMerge) {
			me.changeCloudInfo();
		}
	},
	//新增统计：按入库总单号汇总统计
	changeGridColumns4: function() {
		var me = this;
		var columns = ['ReaBmsInDtl_CompanyName', 'ReaBmsInDoc_InDocNo','ReaBmsInDoc_InvoiceNo',
						'ReaBmsInDtl_DataAddTime','ReaBmsInDtl_SumTotal'
		];
		var index = 0;
		Ext.Array.forEach(me.columns, function(item) {
			if (Ext.Array.contains(columns, item.dataIndex)) {
				me.columns[index].show();
				me.columns[index].width=200;
			} else {
				me.columns[index].hide();
			}
			index = index + 1;
		});
	},
	/**创建数据列*/
	// 增加入库时间列
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'ReaBmsInDtl_CompanyName',
			text: '供应商',
			width: 120,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_ProdGoodsNo',
			text: '厂商编码',
			hidden: true,
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_CenOrgGoodsNo',
			text: '供货商货品编码',
			hidden: true,
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_ReaGoodsNo',
			text: '货品编码',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_GoodsCName',
			text: '货品名称',
			width: 160,
			minWidth: 100,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("ReaBmsInDtl_BarCodeType");
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
			dataIndex: 'ReaBmsInDtl_ProdOrgName',
			text: '品牌',
			width: 90,
			defaultRenderer: true,
			sortable: false
		}, {
			dataIndex: 'ReaBmsInDtl_BarCodeType',
			text: 'BarCodeType',
			sortable: false,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_GoodsUnit',
			text: '单位',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_UnitMemo',
			text: '规格',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_LotNo',
			text: '批号',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDoc_InvoiceNo',
			text: '发票号',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDoc_InDocNo',
			text: '入库总单号',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_OrderDocNo',
			text: '订货单号',
			width: 100,
			sortable:false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_TransportNo',
			text: '货运单号',
			width: 100,
			sortable:false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_InvalidDate',
			text: '效期',
			width: 85,
			isDate: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_GoodsQty',
			text: '入库数',
			width: 80,
			renderer: function(value, meta) {
				var v = value;
				if (v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			}
		}, { // 增加入库时间列
			dataIndex: 'ReaBmsInDtl_DataAddTime',
			text: '入库时间',
			width: 135,
			isDate: true,
			hasTime: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_Price',
			text: '单价',
			hidden: true,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_SumTotal',
			text: '金额',
			width: 130,
			type: 'float',
			summaryType: 'sum',
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
			dataIndex: 'ReaBmsInDtl_Memo',
			text: 'Memo',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_TaxRate',
			text: 'TaxRate',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_FactoryOutTemperature',
			text: '厂家出库温度',
			hidden: true,
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_ArrivalTemperature',
			text: '到货温度',
			hidden: true,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_AppearanceAcceptance',
			text: '外观验收',
			hidden: true,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDoc_RegisterNo',
			text: '注册号',
			width: 80,
			defaultRenderer: true,
			doSort: function(state) {
				var field="ReaGoods_RegistNo";
				me.store.sort({
					property: field,
					direction: state
				});
			}
		}, {
			dataIndex: 'ReaBmsInDoc_NetGoodsNo',
			text: '挂网流水号',
			width: 80,
			defaultRenderer: true,
			doSort: function(state) {
				var field="ReaGoods_NetGoodsNo";
				me.store.sort({
					property: field,
					direction: state
				});
			}
		}];

		return columns;
	},
	/**冷链信息显示或隐藏*/
	changeCloudInfo: function() {
		var me = this;
		var arr = ["ReaBmsInDtl_FactoryOutTemperature", "ReaBmsInDtl_ArrivalTemperature",
			"ReaBmsInDtl_AppearanceAcceptance"
		];
		var buttonsToolbar = me.getComponent('buttonsToolbar1');
		var isOfColdInfoMerge = buttonsToolbar.getComponent('isOfColdInfoMerge').getValue();
		Ext.Array.each(me.columns, function(column, index, countriesItSelf) {
			if (arr.indexOf(column.dataIndex) >= 0) {
				if (isOfColdInfoMerge)
					me.columns[index].show();
				else
					me.columns[index].hide();
			}
		});
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
		var dateareaToolbar = me.getComponent('dateareaToolbar'),
			date = dateareaToolbar.getComponent('date');
		if (date && dateArea) date.setValue(dateArea);
	},
	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this;
		me.store.removeAll();

		me.store.proxy.url = me.getLoadUrl(); //查询条件
		me.disableControl(); //禁用 所有的操作功能
		if (!me.defaultLoad) return false;
	},
	/**客户端入库类型*/
	getInDocInTypeList: function(callback) {
		var me = this;
		if (me.InDocInTypeList.length > 0) return;
		var params = {},
			url = JShell.System.Path.getRootUrl(JcallShell.System.ClassDict._classDicListUrl);
		params = Ext.encode({
			"jsonpara": [{
				"classname": "ReaBmsInDocInType",
				"classnamespace": "ZhiFang.Entity.ReagentSys.Client"
			}]
		});
		me.InDocInTypeList = [];
		var tempArr = [];
		JcallShell.Server.post(url, params, function(data) {
			if (data.success) {
				if (data.value) {
					if (data.value[0].ReaBmsInDocInType.length > 0) {
						me.InDocInTypeList.push(["", '请选择', 'font-weight:bold;text-align:center;']);
						Ext.Array.each(data.value[0].ReaBmsInDocInType, function(obj, index) {
							var style = ['font-weight:bold;text-align:center;'];
							if (obj.BGColor) {
								style.push('color:' + obj.BGColor);
							}
							tempArr = [obj.Id, obj.Name, style.join(';')];
							me.InDocInTypeList.push(tempArr);
						});
					}
				}
			}
		}, false);
	},
	
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		var url = me.callParent(arguments);
		var buttonsToolbar = me.getComponent('buttonsToolbar1');
		var isOfColdInfoMerge = buttonsToolbar.getComponent('isOfColdInfoMerge').getValue();
		var docHql = me.getParamsDocHql();
		var dtlHql = me.getParamsDtlHql();
		var reaGoodsHql = me.getReaGoodsHql();
		if (!docHql) docHql = "";
		if (!dtlHql) dtlHql = "";
		if (!reaGoodsHql) reaGoodsHql = "";
		// 需求调整
		var buttonsToolbar3 = me.getComponent('buttonsToolbar3');
		//加入的查询框入库单号和发票号
		var txtSearchInvNo = buttonsToolbar3.getComponent('txtSearchInvNo');
		var txtSearchInDoc=buttonsToolbar3.getComponent('txtSearchInDoc');
		var txtSearchSName=buttonsToolbar3.getComponent('txtSearchSName');
		txtSearchInvNo = txtSearchInvNo.getValue();
		txtSearchSName = txtSearchSName.getValue();
				
		if(docHql==""){
			if (txtSearchInvNo!="") {
				docHql += "reabmsindoc.InvoiceNo='"+txtSearchInvNo+"'";
			}
		}else{
			if (txtSearchInvNo!="") {
				docHql += " and reabmsindoc.InvoiceNo='"+txtSearchInvNo+"'";
			}
		}
		txtSearchInDoc = txtSearchInDoc.getValue();
		if(docHql==""){
			if (txtSearchInDoc!="") {
				docHql += "reabmsindoc.InDocNo='"+txtSearchInDoc+"'";
			}
		}else{
			if (txtSearchInDoc!="") {
				docHql += " and reabmsindoc.InDocNo='"+txtSearchInDoc+"'";
			}
		}
		if(docHql==""){
			if (txtSearchSName!="") {
				docHql += "reagoods.SName='"+txtSearchSName+"'";
			}
		}else{
			if (txtSearchSName!="") {
				docHql += " and reagoods.SName='"+txtSearchSName+"'";
			}
		}
		var startDate = "",
			endDate = "";
		var dateAre = me.getDateAreaValue();
		startDate = dateAre.start || "";
		endDate = dateAre.end || "";
		url += (url.indexOf('?') == -1 ? '?' : '&') + "docHql=" + JShell.String.encode(docHql);
		url += (url.indexOf('?') == -1 ? '?' : '&') + "dtlHql=" + JShell.String.encode(dtlHql);
		url += (url.indexOf('?') == -1 ? '?' : '&') + "reaGoodsHql=" + JShell.String.encode(reaGoodsHql);
		url += (url.indexOf('?') == -1 ? '?' : '&') + "startDate=" + startDate + "&endDate=" + endDate;
		url += (url.indexOf('?') == -1 ? '?' : '&') + "isOfColdInfoMerge=" + isOfColdInfoMerge;
		
		var groupType = buttonsToolbar3.getComponent('cmReaBmsStatisticalType').getValue();
		//按入库明细
		if (!groupType) groupType = 3;
		url += (url.indexOf('?') == -1 ? '?' : '&') + "groupType=" + groupType;
		return url;
	},
	/**获取日期范围值*/
	getDateAreaValue: function() {
		var me = this;
		var dateareaToolbar = me.getComponent('dateareaToolbar');
		var dateArea = dateareaToolbar.getComponent('date');
		if (dateArea && dateArea.getValue()) {
			var date = dateArea.getValue();
			if (date.start) date.start = Ext.Date.format(date.start, "Y-m-d");
			if (date.end) date.end = Ext.Date.format(date.end, "Y-m-d");
			return date;
		} else {
			return {
				"start": "",
				"end": ""
			};
		}
	},
	/**获取主单查询条件*/
	getParamsDocHql: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar1');
		var inType = buttonsToolbar.getComponent('InType');
		var dateArea = me.getDateAreaValue();
		var docHql = [];
		var labID = JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.LABID) || "";
		docHql.push('reabmsindoc.Visible=1');
		if (labID) {
			docHql.push('reabmsindoc.LabID=' + labID);
		}
		if (inType) {
			var value = inType.getValue();
			if (value) {
				docHql.push('reabmsindoc.InType=' + value);
			}
		}
		if (dateArea) {
			if (dateArea.start) {
				docHql.push("reabmsindoc.DataAddTime>='" + dateArea.start + " 00:00:00'");
			}
			if (dateArea.end) {
				docHql.push("reabmsindoc.DataAddTime<='" + dateArea.end + " 23:59:59'");
			}
		}

		if (docHql && docHql.length > 0) {
			docHql = docHql.join(" and ");
		} else {
			docHql = "";
		}
		return docHql;
	},
	/**获取明细查询条件*/
	getParamsDtlHql: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar1');
		var companyID = buttonsToolbar.getComponent('CompanyID');
		var storageID = buttonsToolbar.getComponent('StorageID');
		var reaGoodsNo = buttonsToolbar.getComponent('ReaGoodsNo');
		var dtlHql = [];

		var labID = JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.LABID) || "";
		if (labID) {
			dtlHql.push('reabmsindtl.LabID=' + labID);
		}
		if (companyID) {
			var value = companyID.getValue();
			if (value) {
				dtlHql.push('reabmsindtl.ReaCompanyID=' + value);
			}
		}
		if (storageID) {
			var value = storageID.getValue();
			if (value) {
				dtlHql.push('reabmsindtl.StorageID=' + value);
			}
		}
		if (reaGoodsNo) {
			var value = reaGoodsNo.getValue();
			if (value) {
				dtlHql.push("reabmsindtl.ReaGoodsNo='" + value + "'");
			}
		}
		//按业务明细的数据项模糊查询条件		
		var cboSearch = buttonsToolbar.getComponent('cboSearch');
		if (cboSearch && cboSearch.getValue() == "2") {
			var txtSearch = buttonsToolbar.getComponent('txtSearch').getValue();
			if (txtSearch) {
				dtlHql.push("(reabmsindtl.LotNo like '%" + txtSearch + "%' or reabmsindtl.CenOrgGoodsNo like '%" + txtSearch +
					"%')");
			}
		}
		if (dtlHql && dtlHql.length > 0) {
			dtlHql = dtlHql.join(" and ");
		} else {
			dtlHql = "";
		}
		return dtlHql;
	},
	/**@description 获取公共查询参数*/
	getSearchParams: function() {
		var me = this;
		var docHql = me.getParamsDocHql();
		var dtlHql = me.getParamsDtlHql();
		var reaGoodsHql = me.getReaGoodsHql();
		if (!docHql) docHql = "";
		if (!dtlHql) dtlHql = "";
		if (!reaGoodsHql) reaGoodsHql = "";
		if (!docHql && !dtlHql) {
			JShell.Msg.error("请先选择统计条件后再操作!");
			return [];
		}
		var startDate = "",
			endDate = "";
		var dateAre = me.getDateAreaValue();
		startDate = dateAre.start || "";
		endDate = dateAre.end || "";
	
		var params = [];
		// 需求调整：动态获取groupType的值
		var buttonsToolbar3 = me.getComponent('buttonsToolbar3');
		var groupType = buttonsToolbar3.getComponent('cmReaBmsStatisticalType').getValue();
		params.push("groupType=" + groupType) ;
		
		//加入的查询框入库单号和发票号
		var txtSearchInvNo = buttonsToolbar3.getComponent('txtSearchInvNo');
		var txtSearchInDoc=buttonsToolbar3.getComponent('txtSearchInDoc');
		var txtSearchSName=buttonsToolbar3.getComponent('txtSearchSName');
		txtSearchSName = txtSearchSName.getValue();
		txtSearchInvNo = txtSearchInvNo.getValue();
		if(docHql==""){
			if (txtSearchInvNo!="") {
				docHql += "reabmsindoc.InvoiceNo='"+txtSearchInvNo+"'";
			}
		}else{
			if (txtSearchInvNo!="") {
				docHql += " and reabmsindoc.InvoiceNo='"+txtSearchInvNo+"'";
			}
		}
		txtSearchInDoc = txtSearchInDoc.getValue();
		if(docHql==""){
			if (txtSearchInDoc!="") {
				docHql += "reabmsindoc.InDocNo='"+txtSearchInDoc+"'";
			}
		}else{
			if (txtSearchInDoc!="") {
				docHql += " and reabmsindoc.InDocNo='"+txtSearchInDoc+"'";
			}
		}
		if(docHql==""){
			if (txtSearchSName!="") {
				docHql += "reagoods.SName='"+txtSearchSName+"'";
			}
		}else{
			if (txtSearchSName!="") {
				docHql += " and reagoods.SName='"+txtSearchSName+"'";
			}
		}
		params.push("reaReportClass=" + me.reaReportClass);
		params.push("breportType=" + me.breportType);
		params.push("frx=" + JShell.String.encode(me.pdfFrx));
		
		params.push("startDate=" + startDate);
		params.push("endDate=" + endDate);
		if (docHql)
			params.push("docHql=" + docHql);
		if (dtlHql)
			params.push("dtlHql=" + JShell.String.encode(dtlHql));
		if (reaGoodsHql)
			params.push("reaGoodsHql=" + JShell.String.encode(reaGoodsHql));
	
		var curOrderBy = me.curOrderBy;
		if (curOrderBy.length <= 0 && me.defaultOrderBy && me.defaultOrderBy.length > 0)
			curOrderBy = me.defaultOrderBy;
		for(var i=0;i<curOrderBy.length;i++){
			if(curOrderBy[i].property == "ReaBmsInDtl_GoodSName"){
				curOrderBy[i].property ="ReaGoods_SName";
			}
		}
		params.push("sort=" + JShell.JSON.encode(curOrderBy));
		
		// 根据用户是否勾选冷链，来确定传入isOfColdInfoMerge的值
		if (params && params.length > 0) {
			var buttonsToolbar = me.getComponent('buttonsToolbar1');
			var isOfColdInfoMerge = buttonsToolbar.getComponent('isOfColdInfoMerge').getValue();
			params.push("isOfColdInfoMerge=" + isOfColdInfoMerge);
		}
		return params;
	}

});
