/**
 * 试剂出库变更台账
 * @author zhaoqi
 * @version 2020-11-18
 */
Ext.define('Shell.class.rea.client.statistics.outchangeaccount.DtlGrid', {
	extend: 'Shell.class.rea.client.statistics.basic.SearchGrid',

	title: '试剂出库变更台账',

	/**获取数据服务路径*/
	selectUrl: '/ReaStatisticalAnalysisService.svc/RS_UDTO_SearchReaBmsOutDtlLotNoAndTransportNoChangeByHQL?isPlanish=true',
	/**出库类型*/
	outTypeList: [],
	/**业务报表类型:对应BTemplateType枚举的key*/
	breportType: 22,
	features: [{
		ftype: 'summary'
	}],
	/**是否启用模糊查询选择类型*/
	hasSearchType: true,
	/**用户UI配置Key*/
	userUIKey: 'statistics.outchangeaccount.DtlGrid',
	/**用户UI配置Name*/
	userUIName: "试剂出库变更台账列表",
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
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'ReaBmsOutDtl_DeptName',
			text: '使用部门',
			width: 100,
			defaultRenderer: true,
			hidden: true,
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
			dataIndex: 'ReaBmsOutDtl_CompanyName',
			text: '供应商',
			width: 90,
			hidden: true,
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
			hidden: true,
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
		}, {
			dataIndex: 'ReaBmsOutDtl_GoodsUnit',
			text: '单位',
			width: 80,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_UnitMemo',
			text: '规格',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_InvalidDate',
			text: '效期',
			width: 85,
			isDate: true,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_GoodsQty',
			text: '出库数',
			width: 80,
			hidden: true,
			renderer: function(value, meta) {
				var v = value;
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			}
		}, {
			dataIndex: 'ReaBmsOutDtl_DataAddTime',
			text: '使用时间',
			width: 135,
			isDate: true,
			hasTime: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_LotNo',
			text: '批号',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_LastLotNo',
			text: '上次出库的批号',
			width: 100,
			hideable: false,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_TransportNo',
			text: '货运单号',
			width: 200,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_LastTransportNo',
			text: '上次出库的货运单号',
			width: 200,
			hideable: false,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_InvalidDate',
			text: '效期',
			width: 85,
			isDate: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_IsChangeLotNo',
			text: '<b style="color:red;">批号是否改变</b>',
			width: 100,
			renderer: function(value,meta,record) {
				// 上次的批号和这次的批号不一样--》是
				if(record.get('ReaBmsOutDtl_LastLotNo') != ''&& record.get('ReaBmsOutDtl_LotNo') != '') {
					var arr = record.get('ReaBmsOutDtl_LastLotNo').split('^');
					if(arr.indexOf(record.get('ReaBmsOutDtl_LotNo')) != -1) { // 在数组里面
						value = '<b style="color:red;">否</b>';
					} else {
						value = '<b style="color:green;">是</b>';
					}
				} else {
					value = '<b style="color:green;">是</b>';
				}
				return value;
			}
		},{
			dataIndex: 'ReaBmsOutDtl_IsChangeTransportNo',
			text: '<b style="color:red;">货运单号是否改变</b>',
			width: 150,
			renderer: function(value,meta,record) {
				// 如果上次的出库的货运单号和这次的货运单号不一样--》是
				// 没有该变的情况是：完全相等或者是他的子集
				if(record.get('ReaBmsOutDtl_LastTransportNo') != ''&& record.get('ReaBmsOutDtl_TransportNo') != '') {
					var arr = record.get('ReaBmsOutDtl_LastTransportNo').split('^');
					if(arr.indexOf(record.get('ReaBmsOutDtl_TransportNo')) != -1) { // 在数组里面
						value = '<b style="color:red;">否</b>';
					} else {
						value = '<b style="color:green;">是</b>';
					}
				} else {
					value = '<b style="color:green;">是</b>';
				}
				
				return value;
			}
		}];

		return columns;
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
		if(!docHql) docHql = "";
		if(!dtlHql) dtlHql = "";
		if(!reaGoodsHql) reaGoodsHql = "";
		var startDate = "",
			endDate = "";
		var dateAre = me.getDateAreaValue();
		startDate = dateAre.start || "";
		endDate = dateAre.end || "";
		url += (url.indexOf('?') == -1 ? '?' : '&') + "docHql=" + JShell.String.encode(docHql);
		url += (url.indexOf('?') == -1 ? '?' : '&') + "dtlHql=" + JShell.String.encode(dtlHql);
		url += (url.indexOf('?') == -1 ? '?' : '&') + "reaGoodsHql=" + JShell.String.encode(reaGoodsHql);
		url += (url.indexOf('?') == -1 ? '?' : '&') + "startDate=" + startDate + "&endDate=" + endDate;
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
	/**获取主单查询条件*/
	getParamsDocHql: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar1');
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
		var companyID = buttonsToolbar.getComponent('CompanyID');
		var reaGoodsNo = buttonsToolbar.getComponent('ReaGoodsNo');
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
	}
});