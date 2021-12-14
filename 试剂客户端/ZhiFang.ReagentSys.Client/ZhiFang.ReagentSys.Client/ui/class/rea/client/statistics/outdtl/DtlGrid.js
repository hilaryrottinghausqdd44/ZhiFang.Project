/**
 * 使用出库明细统计
 * @author longfc
 * @version 2018-09-10
 */
Ext.define('Shell.class.rea.client.statistics.outdtl.DtlGrid', {
	extend: 'Shell.class.rea.client.statistics.basic.SearchGrid',

	title: '使用出库明细统计',

	/**获取数据服务路径*/
	selectUrl: '/ReaStatisticalAnalysisService.svc/RS_UDTO_SearchReaBmsOutDtlSummaryByHQL?isPlanish=true',
	/**出库类型*/
	outTypeList: [],
	/**业务报表类型:对应BTemplateType枚举的key*/
	breportType: 18,
	features: [{
		ftype: 'summary'
	}],
	/**是否启用模糊查询选择类型*/
	hasSearchType: true,
	/**用户UI配置Key*/
	userUIKey: 'statistics.outdtl.DtlGrid',
	/**用户UI配置Name*/
	userUIName: "使用出库出库明细统计列表",
	/**后台排序*/
	remoteSort: true,
	/**出库明细报表合并选择项Key*/
	ReaBmsStatisticalTypeKey: "ReaBmsOutDtlStatisticalType",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		JShell.REA.StatusList.getStatusList(me.ReaBmsStatisticalTypeKey, false, false, null);
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.getTypeList();
		me.callParent(arguments);
		me.initDateArea(-30);
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if(me.hasPagingtoolbar) items.push(me.createPagingtoolbar());
		items.push(me.createButtonToolbar1Items());
		items.push(me.createDateAreaButtonToolbar());
		items.push(me.createButtonToolbar3());
		return items;
	},
	/**默认按钮栏*/
	createButtonToolbar1Items: function() {
		var me = this;
		var items = [];
		items = me.createOutTypeItems(items);
		items = me.createDeptNameItems(items);
		items = me.createCompanyNameItems(items);
		//items = me.createTestEquipLabNameItems(items);
		//items = me.createStorageNameItems(items);
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
			value: "12",
			data: list,
			listeners: {
				select: function(com, records, eOpts) {
					//先按报表合并方式更新显示列信息
					me.changeGridColumns();
					me.onSearch();
				}
			}
		});
		items = me.createTestEquipLabNameItems(items);
		items = me.createStorageNameItems(items);
		items.push({
			xtype: 'button',
			iconCls: 'button-search',
			text: '查询',
			tooltip: '查询操作',
			handler: function() {
				me.onSearch();
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
		items.push({
			xtype: 'textSearchTrigger',
			name: 'txtSearchProdOrgName',
			itemId: 'txtSearchProdOrgName',
			emptyText: '品牌',
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
			name: 'txtSearchOutDoc',
			itemId: 'txtSearchOutDoc',
			emptyText: '出库单号',
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
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'ReaBmsOutDtl_DeptName',
			text: '使用部门',
			width: 100,
			defaultRenderer: true,
			doSort: function(state) {
				//自定义排序
				//var field = this.getSortParam();
				var field = "ReaBmsOutDoc_DeptName";
				me.store.sort({
					property: field,
					direction: state
				});
			}
		}, {
			dataIndex: 'ReaBmsOutDtl_TestEquipName',
			text: '使用仪器',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_CompanyName',
			text: '供应商',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_ProdGoodsNo',
			text: '厂商编码',
			hidden: true,
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_CenOrgGoodsNo',
			text: '供货商货品编码',
			hidden: true,
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_ReaGoodsNo',
			text: '货品编码',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_GoodsCName',
			text: '货品名称',
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
		},{
			dataIndex: 'ReaGoods_SName',
			text: '简称',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_ProdOrgName',
			text: '品牌',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_BarCodeType',
			text: 'BarCodeType',
			sortable: false,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_GoodsUnit',
			text: '单位',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_UnitMemo',
			text: '规格',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_LotNo',
			text: '批号',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDoc_OutDocNo',
			text: '出库单号',
			width: 120,
			defaultRenderer: true
		},{
			dataIndex: 'ReaBmsOutDtl_TransportNo',
			text: '货运单号',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_InvalidDate',
			text: '效期',
			width: 85,
			isDate: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_DataAddTime',
			text: '使用时间',
			width: 135,
			isDate: true,
			hasTime: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_Price',
			text: '单价',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_GoodsQty',
			text: '出库数',
			width: 80,
			renderer: function(value, meta) {
				var v = value;
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			}
		}, {
			dataIndex: 'ReaBmsOutDtl_SumTotal',
			text: '金额',
			width: 130,
			type: 'float',
			summaryType: 'sum',
			renderer: function(value, meta) {
				var v = value;
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			},
			summaryRenderer: function(value, summaryData, dataIndex) {
				return '<span style="font-weight:bold;font-size:12px;color:blue;">共' + Ext.util.Format.currency(value, '￥', 2) + '元</span>';
			}
		}, {
			dataIndex: 'ReaBmsOutDtl_Memo',
			text: 'Memo',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_TaxRate',
			text: 'TaxRate',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_TestCount',
			text: '测试数',
			hidden: true,
			//sortable:false,
			width: 100,
			defaultRenderer: true
		}];

		return columns;
	},
	/**按合并条件调整数据列*/
	changeGridColumns: function() {
		var me = this;
		var buttonsToolbar3 = me.getComponent('buttonsToolbar3');
		var groupType = buttonsToolbar3.getComponent('cmReaBmsStatisticalType').getValue();
		var txtSearchSName = buttonsToolbar3.getComponent('txtSearchSName');
		var txtSearchProdOrgName=buttonsToolbar3.getComponent('txtSearchProdOrgName');
		var txtSearchOutDoc=buttonsToolbar3.getComponent('txtSearchOutDoc');
		//按使用出库明细:领用部门ID+供应商ID+使用仪器ID+货品产品编码+包装单位+规格+批号+效期+出库人ID+使用时间
		if(!groupType) groupType = "12";
		switch(groupType) {
			case "1": //按货品规格
				me.changeGridColumns1();
				txtSearchSName.show();
				txtSearchProdOrgName.show();
				txtSearchOutDoc.setValue("");
				txtSearchOutDoc.hide();
				break;
			case "2": //按供应商加批号及货品
				me.changeGridColumns2();
				txtSearchSName.show();
				txtSearchProdOrgName.show();
				txtSearchOutDoc.setValue("");
				txtSearchOutDoc.hide();
				break;
			case "13": //按供应商加批号及货品
				me.changeGridColumns13();
				txtSearchSName.hide();
				txtSearchProdOrgName.hide();
				txtSearchSName.setValue("");
				txtSearchProdOrgName.setValue("");
				txtSearchOutDoc.show();
				break;
			default:
				me.changeGridColumns12();
				txtSearchSName.show();
				txtSearchProdOrgName.show();
				txtSearchOutDoc.show();
				break;
		}
	},
	//按货品规格
	changeGridColumns1: function() {
		var me = this;
		var columns = ['ReaBmsOutDtl_ProdGoodsNo', 'ReaBmsOutDtl_ReaGoodsNo',
			'ReaBmsOutDtl_GoodsCName', 'ReaBmsOutDtl_GoodsUnit', 'ReaBmsOutDtl_UnitMemo',
			'ReaBmsOutDtl_Price', 'ReaBmsOutDtl_GoodsQty', 'ReaBmsOutDtl_SumTotal',
			'ReaGoods_SName','ReaGoods_ProdOrgName','ReaGoods_TestCount'
		];
		var index = 0;
		Ext.Array.forEach(me.columns, function(item) {
			if(Ext.Array.contains(columns, item.dataIndex)) {
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
		var columns = ['ReaBmsOutDtl_CompanyName', 'ReaBmsOutDtl_ProdGoodsNo',
			'ReaBmsOutDtl_CenOrgGoodsNo', 'ReaBmsOutDtl_ReaGoodsNo',
			'ReaBmsOutDtl_GoodsCName', 'ReaBmsOutDtl_GoodsUnit', 'ReaBmsOutDtl_UnitMemo',
			'ReaBmsOutDtl_LotNo', 'ReaBmsOutDtl_InvalidDate',
			'ReaBmsOutDtl_Price', 'ReaBmsOutDtl_GoodsQty', 'ReaBmsOutDtl_SumTotal',
			'ReaGoods_SName','ReaGoods_ProdOrgName','ReaGoods_TestCount'
		];
		var index = 0;
		Ext.Array.forEach(me.columns, function(item) {
			if(Ext.Array.contains(columns, item.dataIndex)) {
				me.columns[index].show();
			} else {
				me.columns[index].hide();
			}
			index = index + 1;
		});
	},
	// 新增统计：按出库单汇总统计
	changeGridColumns13: function() {
		var me = this;
		var columns = ['ReaBmsOutDtl_DeptName', 'ReaBmsOutDoc_OutDocNo', 'ReaBmsOutDtl_SumTotal',
			'ReaBmsOutDtl_DataAddTime'
		];
		var index = 0;
		Ext.Array.forEach(me.columns, function(item) {
			if(Ext.Array.contains(columns, item.dataIndex)) {
				me.columns[index].show();
				me.columns[index].width=200;
			} else {
				me.columns[index].hide();
			}
			index = index + 1;
		});
	},
	//默认（按出库明细常规合并）
	// 添加货运单号
	changeGridColumns12: function() {
		var me = this;
		var columns = ['ReaBmsOutDtl_DeptName', 'ReaBmsOutDtl_TestEquipName', 'ReaBmsOutDtl_CompanyName',
			'ReaBmsOutDtl_ProdGoodsNo', 'ReaBmsOutDtl_CenOrgGoodsNo', 'ReaBmsOutDtl_ReaGoodsNo',
			'ReaBmsOutDtl_GoodsCName', 'ReaBmsOutDtl_GoodsUnit', 'ReaBmsOutDtl_UnitMemo',
			'ReaBmsOutDtl_LotNo', 'ReaBmsOutDtl_TransportNo', 'ReaBmsOutDtl_InvalidDate',
			'ReaBmsOutDtl_Price', 'ReaBmsOutDtl_GoodsQty', 'ReaBmsOutDtl_SumTotal',
			'ReaGoods_SName','ReaGoods_ProdOrgName','ReaGoods_TestCount','ReaBmsOutDoc_OutDocNo'
		];
		var index = 0;
		Ext.Array.forEach(me.columns, function(item) {
			if(Ext.Array.contains(columns, item.dataIndex)) {
				me.columns[index].show();
			} else {
				me.columns[index].hide();
			}
			index = index + 1;
		});
	},
	/**出库类型*/
	createOutTypeItems: function(items) {
		var me = this;
		if(!items) {
			items = [];
		}
		items.push({
			fieldLabel: '',
			name: 'OutType',
			itemId: 'OutType',
			emptyText: '出库类型',
			xtype: 'uxSimpleComboBox',
			hasStyle: true,
			labelWidth: 0,
			labelAlign: 'right',
			data: me.outTypeList,
			width: 100,
			value: '',
			listeners: {
				change: function() {
					me.onSearch();
				}
			}
		});
		return items;
	},
	/**客户端出库类型*/
	getTypeList: function(callback) {
		var me = this;
		if(me.outTypeList.length > 0) return;
		var params = {},
			url = JShell.System.Path.getRootUrl(JcallShell.System.ClassDict._classDicListUrl);
		params = Ext.encode({
			"jsonpara": [{
				"classname": "ReaBmsOutDocOutType",
				"classnamespace": "ZhiFang.Entity.ReagentSys.Client"
			}]
		});
		me.outTypeList = [];
		var tempArr = [];
		JcallShell.Server.post(url, params, function(data) {
			if(data.success) {
				if(data.value) {
					if(data.value[0].ReaBmsOutDocOutType.length > 0) {
						me.outTypeList.push(["", '请选择', 'font-weight:bold;text-align:center;']);
						Ext.Array.each(data.value[0].ReaBmsOutDocOutType, function(obj, index) {
							var style = ['font-weight:bold;text-align:center;'];
							if(obj.BGColor) {
								style.push('color:' + obj.BGColor);
							}
							tempArr = [obj.Id, obj.Name, style.join(';')];
							me.outTypeList.push(tempArr);
						});
					}
				}
			}
		}, false);
	},
	/**初始化日期范围*/
	initDateArea: function(day) {
		var me = this;
		if(!day) day = 0;
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
		if(date && dateArea) date.setValue(dateArea);
	},
	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this;
		me.store.removeAll();
		me.store.proxy.url = me.getLoadUrl(); //查询条件
		me.disableControl(); //禁用 所有的操作功能
		if(!me.defaultLoad) return false;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		var url = me.callParent(arguments);
		var docHql = me.getParamsDocHql();
		var dtlHql = me.getParamsDtlHql();
		var reaGoodsHql = me.getReaGoodsHql();
		var buttonsToolbar3 = me.getComponent('buttonsToolbar3');
		var groupType = buttonsToolbar3.getComponent('cmReaBmsStatisticalType').getValue();
		//按使用出库明细:领用部门ID+供应商ID+使用仪器ID+货品产品编码+包装单位+规格+批号+效期+出库人ID+使用时间
		if(!groupType) groupType = 12;
		if(!docHql) docHql = "";
		if(!dtlHql) dtlHql = "";
		if(!reaGoodsHql) reaGoodsHql = "";
		var txtSearchSName = buttonsToolbar3.getComponent('txtSearchSName');
		if (txtSearchSName) {
			txtSearchSName = txtSearchSName.getValue();
			if (txtSearchSName) {
				reaGoodsHql+=" and (reagoods.SName like '%" + txtSearchSName + "%')";
			}
		}
		var txtSearchProdOrgName=buttonsToolbar3.getComponent('txtSearchProdOrgName');
		if (txtSearchProdOrgName) {
			txtSearchProdOrgName = txtSearchProdOrgName.getValue();
			if (txtSearchProdOrgName) {
				reaGoodsHql+=" and (reagoods.ProdOrgName like '%" + txtSearchProdOrgName + "%')";
			}
		}
		var txtSearchOutDoc=buttonsToolbar3.getComponent('txtSearchOutDoc');
		txtSearchOutDoc = txtSearchOutDoc.getValue();
		if(docHql==""){
			if (txtSearchOutDoc!="") {
				docHql += "reabmsoutdoc.OutDocNo='"+txtSearchOutDoc+"'";
			}
		}else{
			if (txtSearchOutDoc!="") {
				docHql += " and reabmsoutdoc.OutDocNo='"+txtSearchOutDoc+"'";
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

		
		me.changeGridColumns();
		url += (url.indexOf('?') == -1 ? '?' : '&') + "groupType=" + groupType;
		return url;
	},
	/**获取日期范围值*/
	getDateAreaValue: function() {
		var me = this;
		var dateareaToolbar = me.getComponent('dateareaToolbar');
		var dateArea = dateareaToolbar.getComponent('date');
		if(dateArea && dateArea.getValue()) {
			var date = dateArea.getValue();
			if(date.start) date.start = Ext.Date.format(date.start, "Y-m-d");
			if(date.end) date.end = Ext.Date.format(date.end, "Y-m-d");
			return date;
		} else {
			return {
				"start": "",
				"end": ""
			};
		}
	},
	/**获取仪器ID组件*/
	getTestEquipLabId: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar3');
		var Id = buttonsToolbar.getComponent('TestEquipLabId');
		return Id;
	},
	/**获取仪器名称组件*/
	getTestEquipLabName: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar3');
		var testEquipLabName = buttonsToolbar.getComponent('TestEquipLabName');
		return testEquipLabName;
	},
	/**获取主单查询条件*/
	getParamsDocHql: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar1');
		var buttonsToolbar3 = me.getComponent('buttonsToolbar3');
		var outType = buttonsToolbar.getComponent('OutType');
		var companyID = buttonsToolbar.getComponent('CompanyID');
		var deptID = buttonsToolbar.getComponent('DeptID');
		var dateArea = me.getDateAreaValue();
		var docHql = [];
		var labID = JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.LABID) || "";
		docHql.push('reabmsoutdoc.Visible=1');
		if(labID) {
			docHql.push('reabmsoutdoc.LabID=' + labID);
		}
		if(outType) {
			var value = outType.getValue();
			if(value) {
				docHql.push('reabmsoutdoc.OutType=' + value);
			}
		}
		if(deptID) {
			var value = deptID.getValue();
			if(value) {
				docHql.push('reabmsoutdoc.DeptID=' + value);
			}
		}

		if(dateArea) {
			if(dateArea.start) {
				docHql.push("reabmsoutdoc.DataAddTime>='" + dateArea.start + " 00:00:00'");
			}
			if(dateArea.end) {
				docHql.push("reabmsoutdoc.DataAddTime<='" + dateArea.end + " 23:59:59'");
			}
		}

		if(docHql && docHql.length > 0) {
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
		var buttonsToolbar3 = me.getComponent('buttonsToolbar3');
		var companyID = buttonsToolbar.getComponent('CompanyID');
		var reaGoodsNo = buttonsToolbar.getComponent('ReaGoodsNo');
		var testEquipLabId = buttonsToolbar3.getComponent('TestEquipLabId');
		var storageID = buttonsToolbar3.getComponent('StorageID');
		var dtlHql = [];
		var labID = JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.LABID) || "";
		if(labID) {
			dtlHql.push('reabmsoutdtl.LabID=' + labID);
		}
		if(companyID) {
			var value = companyID.getValue();
			if(value) {
				dtlHql.push('reabmsoutdtl.ReaCompanyID=' + value);
			}
		}
		if(testEquipLabId) {
			var value = testEquipLabId.getValue();
			if(value) {
				dtlHql.push('reabmsoutdtl.TestEquipID=' + value);
			}
		}
		if(storageID) {
			var value = storageID.getValue();
			if(value) {
				dtlHql.push('reabmsoutdtl.StorageID=' + value);
			}
		}
		if(reaGoodsNo) {
			var value = reaGoodsNo.getValue();
			if(value) {
				dtlHql.push("reabmsoutdtl.ReaGoodsNo='" + value + "'");
			}
		}
		//按业务明细的数据项模糊查询条件		
		var cboSearch = buttonsToolbar.getComponent('cboSearch');
		if(cboSearch && cboSearch.getValue() == "2") {
			var txtSearch = buttonsToolbar.getComponent('txtSearch').getValue();
			if(txtSearch) {
				dtlHql.push("(reabmsoutdtl.LotNo like '%" + txtSearch + "%' or reabmsoutdtl.CenOrgGoodsNo like '%" + txtSearch + "%')");
			}
		}
		if(dtlHql && dtlHql.length > 0) {
			dtlHql = dtlHql.join(" and ");
		} else {
			dtlHql = "";
		}
		return dtlHql;
	},
	/**
	 * 由于库房选择写在了buttonsToolbar3中，导致底层这个方法没有实现
	 * 
	 * */
	onStorageCheck: function(p, record) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar3');
		var storageID = buttonsToolbar.getComponent('StorageID');
		if (!storageID) {
			p.close();
			JShell.Msg.overwrite('onStorageCheck');
			return;
		}
		storageID.setValue(record ? record.get('ReaStorage_Id') : '');
		var storageName = buttonsToolbar.getComponent('StorageName');
		storageName.setValue(record ? record.get('ReaStorage_CName') : '');
		p.close();
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
		var buttonsToolbar3 = me.getComponent('buttonsToolbar3');
		var txtSearchSName = buttonsToolbar3.getComponent('txtSearchSName');
		if (txtSearchSName) {
			txtSearchSName = txtSearchSName.getValue();
			if (txtSearchSName) {
				reaGoodsHql+=" and (reagoods.SName like '%" + txtSearchSName + "%')";
			}
		}
		var txtSearchProdOrgName=buttonsToolbar3.getComponent('txtSearchProdOrgName');
		if (txtSearchProdOrgName) {
			txtSearchProdOrgName = txtSearchProdOrgName.getValue();
			if (txtSearchProdOrgName) {
				reaGoodsHql+=" and (reagoods.ProdOrgName like '%" + txtSearchProdOrgName + "%')";
			}
		}
		var txtSearchOutDoc=buttonsToolbar3.getComponent('txtSearchOutDoc');
		txtSearchOutDoc = txtSearchOutDoc.getValue();
		if(docHql==""){
			if (txtSearchOutDoc!="") {
				docHql += "reabmsoutdoc.OutDocNo='"+txtSearchOutDoc+"'";
			}
		}else{
			if (txtSearchOutDoc!="") {
				docHql += " and reabmsoutdoc.OutDocNo='"+txtSearchOutDoc+"'";
			}
		}
		var startDate = "",
			endDate = "";
		var dateAre = me.getDateAreaValue();
		startDate = dateAre.start || "";
		endDate = dateAre.end || "";
		
		var params = [];
		params.push("reaReportClass=" + me.reaReportClass);
		params.push("breportType=" + me.breportType);
		params.push("frx=" + JShell.String.encode(me.pdfFrx));
		var groupType = buttonsToolbar3.getComponent('cmReaBmsStatisticalType').getValue();
		params.push("groupType=" + groupType) ;
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
		params.push("sort=" + JShell.JSON.encode(curOrderBy));
		return params;
	}
});