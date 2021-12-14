/**
 * 移库领用统计报表
 * @author longfc
 * @version 2018-09-10
 */
Ext.define('Shell.class.rea.client.statistics.transferdtl.DtlGrid', {
	extend: 'Shell.class.rea.client.statistics.basic.SearchGrid',

	title: '移库领用统计报表',

	/**获取数据服务路径*/
	selectUrl: '/ReaStatisticalAnalysisService.svc/RS_UDTO_SearchTransferDtlSummaryByHQL?isPlanish=true',
	/**业务报表类型:对应BTemplateType枚举的key*/
	breportType: 17,
	features: [{
		ftype: 'summary'
	}],
	/**是否启用模糊查询选择类型*/
	hasSearchType: true,
	/**用户UI配置Key*/
	userUIKey: 'statistics.transferdtl.DtlGrid',
	/**用户UI配置Name*/
	userUIName: "移库领用统计报表列表",
	/**后台排序*/
	remoteSort: true,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
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
		//if(me.hasButtontoolbar) items.push(me.createButtontoolbar());
		if(me.hasPagingtoolbar) items.push(me.createPagingtoolbar());
		items.push(me.createButtonToolbar1Items());
		items.push(me.createDateAreaButtonToolbar());
		return items;
	},
	/**默认按钮栏*/
	createButtonToolbar1Items: function() {
		var me = this;
		var items = [];
		items = me.createCompanyNameItems(items);
		items = me.createDeptNameItems(items);
		items = me.createStorageNameItems(items, "目的库房");
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
			dataIndex: 'ReaBmsTransferDtl_DeptName',
			text: '领用部门',
			width: 100,
			defaultRenderer: true,
			doSort: function(state) {
				//var field = this.getSortParam();
				var field="ReaBmsTransferDoc_DeptName";
				me.store.sort({
					property: field,
					direction: state
				});
			}
		}, {
			dataIndex: 'ReaBmsTransferDtl_TakerName',
			text: '领用用人',
			width: 80,
			defaultRenderer: true,
			doSort: function(state) {
				//var field = this.getSortParam();
				var field="ReaBmsTransferDoc_TakerName";
				me.store.sort({
					property: field,
					direction: state
				});
			}
		}, {
			dataIndex: 'ReaBmsTransferDtl_ReaCompanyName',
			text: '供应商',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_ProdGoodsNo',
			text: '厂商编码',
			hidden: true,
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_CenOrgGoodsNo',
			text: '供货商货品编码',
			hidden: true,
			width: 90,
			defaultRenderer: true
		},{
			dataIndex: 'ReaBmsTransferDtl_ReaGoodsNo',
			text: '货品编码',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_GoodsCName',
			text: '货品名称',
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
			dataIndex: 'ReaBmsTransferDtl_BarCodeType',
			text: 'BarCodeType',
			sortable: false,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_GoodsUnit',
			text: '单位',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_UnitMemo',
			text: '规格',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_LotNo',
			text: '批号',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_InvalidDate',
			text: '效期',
			width: 85,
			isDate: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_DataAddTime',
			text: '领用时间',
			width: 135,
			isDate: true,
			hasTime: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_GoodsQty',
			text: '领用数',
			width: 80,
			renderer: function(value, meta) {
				var v = value;
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			}
		}, {
			dataIndex: 'ReaBmsTransferDtl_Price',
			text: '单价',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_SumTotal',
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
		}];

		return columns;
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
		url += (url.indexOf('?') == -1 ? '?' : '&') + "docHql=" + JShell.String.encode(docHql);
		url += (url.indexOf('?') == -1 ? '?' : '&') + "dtlHql=" + JShell.String.encode(dtlHql);
		url += (url.indexOf('?') == -1 ? '?' : '&') + "reaGoodsHql=" + JShell.String.encode(reaGoodsHql);
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
		var companyID = buttonsToolbar.getComponent('CompanyID');
		var deptID = buttonsToolbar.getComponent('DeptID');
		var dateArea = me.getDateAreaValue();
		var docHql = [];
		var labID = JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.LABID) || "";
		docHql.push('reabmstransferdoc.Visible=1');
		if(labID) {
			docHql.push('reabmstransferdoc.LabID=' + labID);
		}
		if(deptID) {
			var value = deptID.getValue();
			if(value) {
				docHql.push('reabmstransferdoc.DeptID=' + value);
			}
		}
		if(dateArea) {
			if(dateArea.start) {
				docHql.push("reabmstransferdoc.DataAddTime>='" + dateArea.start + " 00:00:00'");
			}
			if(dateArea.end) {
				docHql.push("reabmstransferdoc.DataAddTime<='" + dateArea.end + " 23:59:59'");
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
		var dtlHql = [];
		var buttonsToolbar = me.getComponent('buttonsToolbar1');
		var companyID = buttonsToolbar.getComponent('CompanyID');
		var storageID = buttonsToolbar.getComponent('StorageID');
		var reaGoodsNo = buttonsToolbar.getComponent('ReaGoodsNo');
		var labID = JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.LABID) || "";
		if(labID) {
			dtlHql.push('reabmstransferdtl.LabID=' + labID);
		}
		if(companyID) {
			var value = companyID.getValue();
			if(value) {
				docHql.push('reabmstransferdtl.ReaCompanyID=' + value);
			}
		}
		if(storageID) {
			var value = storageID.getValue();
			if(value) {
				dtlHql.push('reabmstransferdtl.DStorageID=' + value);
			}
		}
		if(reaGoodsNo) {
			var value = reaGoodsNo.getValue();
			if(value) {
				dtlHql.push("reabmstransferdtl.ReaGoodsNo='" + value + "'");
			}
		}

		//按业务明细的数据项模糊查询条件		
		var cboSearch = buttonsToolbar.getComponent('cboSearch');
		if(cboSearch && cboSearch.getValue() == "2") {
			var txtSearch = buttonsToolbar.getComponent('txtSearch').getValue();
			if(txtSearch) {
				dtlHql.push("(reabmstransferdtl.LotNo like '%" + txtSearch + "%' or reabmstransferdtl.CenOrgGoodsNo like '%" + txtSearch + "%')");
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