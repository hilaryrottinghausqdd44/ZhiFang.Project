/**
 * 实验室试剂-采购统计
 * @author longfc
 * @version 2018-09-30
 */
Ext.define('Shell.class.rea.client.reasale.lab.statis.GridLeft', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	title: '实验室试剂-采购统计-总计',
	requires: [
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.DateArea',
		'Shell.ux.form.field.CheckTrigger'
	],

	width: 535,
	height: 600,

	/**获取数据服务路径*/
	selectUrl: '/ReaStatisticalAnalysisService.svc/RS_UDTO_SearchReaBmsCenSaleDtlSummaryByHQL?isPlanish=true',
	/**导出Excel数据服务路径*/
	outExcelUrl: '/ReaStatisticalAnalysisService.svc/RS_UDTO_BmsCenSaleDtlStatExcel',

	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**默认加载*/
	defaultLoad: false,
	/**后台排序*/
	remoteSort: true,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**序号列宽度*/
	rowNumbererWidth: 35,

	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaBmsCenSaleDtl_ReaGoodsName',
		direction: 'ASC'
	}],

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;

		me.dockedItems = me.createDockedItems();
		//创建数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if(me.hasPagingtoolbar) items.push(me.createPagingtoolbar());
		items.push(me.createtopToolabr1());
		items.push(me.createtopToolabr2());
		return items;
	},
	/**默认按钮栏*/
	createtopToolabr1: function() {
		var me = this;
		var items = [];
		var end = new Date();
		var start = JShell.Date.getNextDate(end, -30);
		items.push('refresh', '-', {
			xtype: 'uxdatearea',
			itemId: 'date',
			fieldLabel: '日期范围',
			labelAlign: 'right',
			value: {
				start: start,
				end: end
			},
			listeners: {
				enter: function() {
					me.onSearch();
				}
			}
		});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			border: false,
			itemId: 'topToolabr1',
			items: items
		});
	},
	/**默认按钮栏*/
	createtopToolabr2: function() {
		var me = this;
		var items = [];

		items = me.createCompanyNameItems(items);
		items = me.createGoodsNameItems(items);
		items.push('-', 'searchb', '-', {
			text: '导出',
			tooltip: '按条件导出成Excel文件！',
			iconCls: 'file-excel',
			itemId: 'outButton',
			handler: function() {
				me.onOutClick();
			}
		});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			border: false,
			itemId: 'topToolabr2',
			items: items
		});
	},
	createCompanyNameItems: function(items) {
		var me = this;
		if(!items) {
			items = [];
		}
		items.push({
			fieldLabel: '',
			name: 'CompanyName',
			itemId: 'CompanyName',
			xtype: 'uxCheckTrigger',
			emptyText: '供应商',
			width: 120,
			labelWidth: 0,
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
			xtype: 'textfield',
			name: 'CompanyID',
			itemId: 'CompanyID'
		}, {
			fieldLabel: '供货商机构编码',
			xtype: 'textfield',
			hidden: true,
			name: 'ReaCompCode',
			itemId: 'ReaCompCode'
		}, {
			fieldLabel: '供应商机构平台码',
			xtype: 'textfield',
			hidden: true,
			name: 'ReaServerCompCode',
			itemId: 'ReaServerCompCode'
		});
		return items;
	},
	/**创建试剂选择*/
	createGoodsNameItems: function(items) {
		var me = this;
		if(!items) {
			items = [];
		}
		items.push({
			fieldLabel: '',
			emptyText: '试剂选择',
			name: 'GoodsName',
			itemId: 'GoodsName',
			labelWidth: 0,
			width: 200,
			labelAlign: 'right',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.client.goods2.basic.CheckGrid',
			classConfig: {
				title: '试剂选择',
				/**是否单选*/
				checkOne: true,
				width: 300
			},
			listeners: {
				check: function(p, record) {
					me.onGoodsCheck(p, record);
				}
			}
		}, {
			xtype: 'textfield',
			itemId: 'ReaGoodsNo',
			name: 'ReaGoodsNo',
			fieldLabel: '试剂ID',
			hidden: true
		}, {
			xtype: 'textfield',
			itemId: 'GoodsID',
			name: 'GoodsID',
			fieldLabel: '试剂ID',
			hidden: true
		});
		return items;
	},
	/**供应商选择*/
	onCompAccept: function(p, record) {
		var me = this;
		var buttonsToolbar = me.getComponent('topToolabr2');
		var Id = buttonsToolbar.getComponent('CompanyID');
		var CName = buttonsToolbar.getComponent('CompanyName');
		var ReaCompCode = buttonsToolbar.getComponent('ReaCompCode');
		var ReaServerCompCode = buttonsToolbar.getComponent('ReaServerCompCode');
		if(!Id) {
			p.close();
			JShell.Msg.overwrite('onCompAccept');
			return;
		}

		if(record == null) {
			CName.setValue('');
			Id.setValue('');
			p.close();
			return;
		}
		if(record.data) {
			var orgNo = record ? record.data.value.OrgNo : '';
			var platformOrgNo = record ? record.data.value.PlatformOrgNo : '';
			
			CName.setValue(record.data ? record.data.text : '');
			Id.setValue(record.data ? record.data.tid : '');
			ReaCompCode.setValue(orgNo);
			ReaServerCompCode.setValue(platformOrgNo);
			p.close();
		}
	},
	/**试剂选择*/
	onGoodsCheck: function(p, record) {
		var me = this;
		var buttonsToolbar = me.getComponent('topToolabr2');
		var ReaGoodsNo = buttonsToolbar.getComponent('ReaGoodsNo');
		var GoodsID = buttonsToolbar.getComponent('GoodsID');
		if(!ReaGoodsNo) {
			p.close();
			JShell.Msg.overwrite('onGoodsAccept');
			return;
		}
		var GoodsName = buttonsToolbar.getComponent('GoodsName');
		ReaGoodsNo.setValue(record ? record.get('ReaGoods_ReaGoodsNo') : '');
		GoodsName.setValue(record ? record.get('ReaGoods_CName') : '');
		GoodsID.setValue(record ? record.get('ReaGoods_Id') : '');
		p.close();
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'ReaBmsCenSaleDtl_ReaGoodsNo',
			sortable: false,
			text: '货品编码',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtl_ReaGoodsName',
			sortable: false,
			text: '中文名',
			width: 95,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtl_GoodsUnit',
			sortable: false,
			text: '包装单位',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtl_UnitMemo',
			sortable: false,
			text: '包装规格',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtl_GoodsQty',
			sortable: false,
			text: '采购数量',
			align: 'right',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtl_SumTotal',
			sortable: false,
			text: '采购金额',
			align: 'right',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtl_ProdOrgName',
			sortable: false,
			text: '生产厂家',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtl_ReaGoodsID',
			sortable: false,
			text: '试剂ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];

		return columns;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		var url = me.callParent(arguments);
		var docHql = me.getParamsDocHql();
		var dtlHql = me.getParamsDtlHql();
		if(!docHql) {
			docHql = "";
		}
		if(!dtlHql) {
			dtlHql = "";
		}
		url += (url.indexOf('?') == -1 ? '?' : '&') + "docHql=" + JShell.String.encode(docHql);
		url += (url.indexOf('?') == -1 ? '?' : '&') + "dtlHql=" + JShell.String.encode(dtlHql);
		url += "&groupType=1";
		return url;
	},
	/**获取主单查询条件*/
	getParamsDocHql: function() {
		var me = this;
		var topToolabr1 = me.getComponent('topToolabr1'),
			topToolabr2 = me.getComponent('topToolabr2'),
			dateArea = topToolabr1.getComponent('date'),
			CompID = topToolabr2.getComponent('CompanyID');
		var docHql = [];

		if(dateArea && dateArea.getValue()) {
			var value = dateArea.getValue();
			if(value.start) {
				var start = Ext.Date.format(value.start, "Y-m-d");
				if(start) {
					docHql.push("reabmscensaledoc.DataAddTime>='" + start + " 00:00:00'");
				}
			}
			if(value.end) {
				var end = Ext.Date.format(value.end, "Y-m-d");
				if(end) {
					docHql.push("reabmscensaledoc.DataAddTime<='" + end + " 23:59:59'");
				}
			}
		}
		if(CompID) {
			var value = CompID.getValue();
			if(value) {
				docHql.push("reabmscensaledoc.CompID=" + value + "");
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
		var topToolabr1 = me.getComponent('topToolabr1'),
			topToolabr2 = me.getComponent('topToolabr2'),
			GoodsID = topToolabr2.getComponent('GoodsID');
		var dtlHql = [];

		if(GoodsID) {
			var value = GoodsID.getValue();
			if(value) {
				dtlHql.push("reabmscensaledtl.ReaGoodsID=" + value + "");
			}
		}
		if(dtlHql && dtlHql.length > 0) {
			dtlHql = dtlHql.join(" and ");
		} else {
			dtlHql = "";
		}
		return dtlHql;
	},
	/**查询*/
	onSearchBClick: function() {
		var me = this;
		me.onSearch();
	},
	/**导出供货单Excel*/
	onOutClick: function() {
		var me = this;
	}
});