/**
 * 按机构货品最大包装单位进行综合统计
 * @author longfc
 * @version 2018-09-11
 */
Ext.define('Shell.class.rea.client.statistics.complex1.DtlGrid', {
	extend: 'Shell.class.rea.client.statistics.basic.SearchGrid',

	title: '试剂耗材综合统计',
	
	/**默认每页数量*/
	defaultPageSize: 50,
	/**获取数据服务路径*/
	selectUrl: '/ReaStatisticalAnalysisService.svc/RS_UDTO_SearchReaGoodsStatisticsOfMaxGonvertQtyEntityListHQL?isPlanish=true',
	
	/**业务报表类型:对应BTemplateType枚举的key*/
	breportType: 19,
	/**用户UI配置Key*/
	userUIKey: 'statistics.complex1.DtlGrid',
	/**用户UI配置Name*/
	userUIName: "试剂耗材综合统计列表",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//自定义按钮功能栏
		//me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
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
		items = me.createTestEquipLabNameItems(items);
		items = me.createGoodsNameItems(items);
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
			dataIndex: 'ReaGoodsOfMaxGonvertQtyVO_ReaGoodsNo',
			text: '货品编码',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOfMaxGonvertQtyVO_ProdOrgName',
			text: '品牌',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOfMaxGonvertQtyVO_GoodsCName',
			text: '货品名称',
			width: 120,
			minWidth: 100,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("ReaGoodsOfMaxGonvertQtyVO_BarCodeType");
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
			dataIndex: 'ReaGoodsOfMaxGonvertQtyVO_GoodsUnit',
			text: '单位',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOfMaxGonvertQtyVO_UnitMemo',
			text: '规格',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOfMaxGonvertQtyVO_GoodsClass',
			text: '一级分类',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOfMaxGonvertQtyVO_GoodsClassType',
			text: '二级分类',
			width: 120,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOfMaxGonvertQtyVO_DeptName',
			text: '部门',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOfMaxGonvertQtyVO_SuitableType',
			text: '适用机型',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOfMaxGonvertQtyVO_ReaCompanyName',
			text: '供应商',
			width: 120,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOfMaxGonvertQtyVO_BarCodeType',
			text: 'BarCodeType',
			sortable: false,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOfMaxGonvertQtyVO_Price',
			text: '单价(元)',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOfMaxGonvertQtyVO_TestCount',
			text: '理论测试数',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOfMaxGonvertQtyVO_MonthlyUsage',
			text: '理论月用量',
			width: 85,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOfMaxGonvertQtyVO_OrderCount',
			text: '订货数',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOfMaxGonvertQtyVO_ConfirmCount',
			text: '验收数',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOfMaxGonvertQtyVO_InCount',
			text: '入库数',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOfMaxGonvertQtyVO_CurQtyCount',
			text: '当前库存量',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOfMaxGonvertQtyVO_StoreLower',
			text: '库存低限',
			width: 70,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOfMaxGonvertQtyVO_UndeliveredCount',
			text: '当前订货未到货数',
			width: 110,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOfMaxGonvertQtyVO_TransferCount',
			text: '移库领用数',
			width: 70,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOfMaxGonvertQtyVO_TestEquipOutCount',
			text: '上机使用数',
			width: 70,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOfMaxGonvertQtyVO_SumTotal',
			text: '项目总收入',
			width: 70,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOfMaxGonvertQtyVO_EquipTestCount',
			text: '对应项目测试数',
			width: 95,
			defaultRenderer: true
		}];

		return columns;
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
		if(docHql) {
			url += (url.indexOf('?') == -1 ? '?' : '&') + docHql;
		}
		return url;
	},
	/**获取查询条件*/
	getParamsDocHql: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar1');
		var dateareaToolbar = me.getComponent('dateareaToolbar');

		var companyID = buttonsToolbar.getComponent('CompanyID');
		var deptID = buttonsToolbar.getComponent('DeptID');
		var testEquipLabId = buttonsToolbar.getComponent('TestEquipLabId');
		var reaGoodsNo = buttonsToolbar.getComponent('ReaGoodsNo');
		var dateArea = dateareaToolbar.getComponent('date');
		var where = "";

		if(companyID) {
			companyID = companyID.getValue();
		} else {
			companyID = "";
		}
		where += "companyID=" + companyID;

		if(deptID) {
			deptID = deptID.getValue();
		} else {
			deptID = "";
		}
		where += "&deptID=" + deptID;

		var testEquipId = "";
		if(testEquipLabId) {
			testEquipId = testEquipLabId.getValue();
		} else {
			testEquipId = "";
		}
		where += "&testEquipId=" + testEquipId;
		
		if(reaGoodsNo) {
			reaGoodsNo = reaGoodsNo.getValue();
		} else {
			reaGoodsNo = "";
		}
		where += "&ReaGoodsNo=" + reaGoodsNo;
		
		var startDate = "",
			endDate = "";
		if(dateArea && dateArea.getValue()) {
			var value = dateArea.getValue();
			if(value.start) {
				startDate = Ext.Date.format(value.start, "Y-m-d");
			}
			if(value.end) {
				endDate = Ext.Date.format(value.end, "Y-m-d");
			}
		}
		where += "&startDate=" + startDate;
		where += "&endDate=" + endDate;

		return where;
	},
	/**获取明细查询条件*/
	getParamsDtlHql: function() {
		//JShell.Msg.overwrite('getParamsDtlHql');
		return "";
	},
	/**@description 选择某一试剂耗材订单,预览PDF清单*/
	onPrintClick: function() {
		var me = this,
			operateType = '1';
		if(!me.reaReportClass || me.reaReportClass != "Frx") {
			JShell.Msg.error("请先选择Frx模板后再操作!");
			return;
		}
		if(!me.pdfFrx) {
			JShell.Msg.error("请先选择Excel模板后再操作!");
			return;
		}
		var docHql = me.getParamsDocHql();
		var dtlHql = me.getParamsDtlHql();
		if(!docHql) {
			docHql = "";
		}
		if(!dtlHql) {
			dtlHql = "";
		}
		if(!docHql && !dtlHql) {
			JShell.Msg.error("请先选择统计条件后再操作!");
			return;
		}
		var url = JShell.System.Path.getRootUrl("/ReaStatisticalAnalysisService.svc/RS_UDTO_SearchReaGoodsStatisticsOfMaxGonvertQtyReportOfPdfByHql");
		var params = [];
		params.push("reaReportClass=" + me.reaReportClass);
		params.push("operateType=" + operateType);
		params.push("breportType=" + me.breportType);
		params.push("frx=" + JShell.String.encode(me.pdfFrx));
		params.push("groupType=1");
		url += "?" + params.join("&");
		url += '&' + docHql;
		window.open(url);
	},
	/**选择某一试剂耗材订单,导出EXCEL*/
	onDownLoadExcel: function() {
		var me = this,
			operateType = '0';
		if(!me.reaReportClass || me.reaReportClass != "Excel") {
			JShell.Msg.error("请先选择Excel模板后再操作!");
			return;
		}
		if(!me.pdfFrx) {
			JShell.Msg.error("请先选择Excel模板后再操作!");
			return;
		}
		var docHql = me.getParamsDocHql();
		var dtlHql = me.getParamsDtlHql();
		if(!docHql) {
			docHql = "";
		}
		if(!dtlHql) {
			dtlHql = "";
		}
		if(!docHql && !dtlHql) {
			JShell.Msg.error("请先选择统计条件后再操作!");
			return;
		}
		var url = JShell.System.Path.getRootUrl("/ReaStatisticalAnalysisService.svc/RS_UDTO_SearchReaGoodsStatisticsOfMaxGonvertQtyReportOfExcelByHql");
		var params = [];
		params.push("operateType=" + operateType);
		params.push("breportType=" + me.breportType);
		params.push("frx=" + JShell.String.encode(me.pdfFrx));
		params.push("groupType=1");
		url += "?" + params.join("&");
		var docHql = me.getParamsDocHql();
		url += '&' + docHql;
		window.open(url);
	}
});