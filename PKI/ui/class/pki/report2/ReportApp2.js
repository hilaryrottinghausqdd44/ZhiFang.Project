/**
 * 实际送样客户数及其汇总报表的父类
 * @author longfc
 * @version 2016-05-25
 */
Ext.define('Shell.class.pki.report2.ReportApp2', {
	extend: 'Ext.panel.Panel',
	title: '报表',

	layout: 'border',
	bodyPadding: 1,

	width: 1200,
	height: 600,

	/**默认加载*/
	defaultLoad: true,
	reportType: '6', //报表类型
	reportTypeTotal: '7', //汇总报表类型
	isChangeColumnsText: false, //是否在每次查询时动态修改列标题显示名称
	/**是否显示导出Excel操作列*/
	isHiddenEXCELActioncolumn: true,
	ReportGridClass: 'Shell.class.pki.report2.ReportGrid',
	ReportTotalGridClass: 'Shell.class.pki.report2.ReportGrid',
	/*汇总或报表明细的查询*/
	//selectTotalUrl: '/StatService.svc/Stat_UDTO_ReportReconciliationDetail',
	/**报表导出Excel*/
	downLoadExcelUrl: '/StatService.svc/Stat_UDTO_ReportDetailToExcel',
	/**计算项目佣金*/
	hasCalcItemCommission: false,
	/***计算项目佣金*/
	calcItemUrl: '/StatService.svc/Stat_UDTO_CalcItemCommission',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var FilterToolbar = me.getComponent('topToolbar').getComponent('FilterToolbar');
		var ReportGrid = me.getComponent('ReportGrid');
		var ReportTotalGrid = me.getComponent('ReportTotalGrid');
		FilterToolbar.on({
			search: function(p, params) {
				ReportGrid.clearData();
				ReportTotalGrid.clearData();
				ReportGrid.params = FilterToolbar.getParams();
				ReportTotalGrid.params = FilterToolbar.getParams();
				ReportGrid.onSearch();
				//明细比主报表慢1秒
				setTimeout(function() {
					ReportTotalGrid.onSearch();
				}, 800);

			}
		});
		if(me.defaultLoad) {
			JShell.Action.delay(function() {
				FilterToolbar.onFilterSearch();
			});
		}
	},

	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.dockedItems = [{
			dock: 'top',
			itemId: 'topToolbar',
			border: false,
			items: [
				Ext.create('Shell.class.pki.report2.SearchToolbar', {
					border: false,
					itemId: 'FilterToolbar'
				})
			]
		}];
		me.dockedItems.push(Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'topToolbarButtons',
			items: ['exp_excel', {
				text: '计算项目佣金',
				itemId: 'CalcItemCommission',
				hidden: !me.hasCalcItemCommission,
				iconCls: 'button-search',
				tooltip: '<b>计算项目佣金</b>',
				handler: function() {
					me.onCalcItemCommissionClick();
				}
			}]
		}));
		me.callParent(arguments);
	},
	/**计算项目佣金*/
	onCalcItemCommissionClick: function() {
		var me = this;
		var url = (me.calcItemUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.calcItemUrl;

		var postParams = me.getReportParams();
		var entity = postParams.entity;
		var params = {
			'entity': entity
		};
		params = Ext.JSON.encode(params);
		Ext.Msg.wait("正在计算中,请稍候...", '提示');
		JShell.Server.post(url, params, function(data) {
			Ext.Msg.hide();
			if(data.success) {
				var FilterToolbar = me.getComponent('topToolbar').getComponent('FilterToolbar');
				FilterToolbar.onFilterSearch();
			} else {
				//me.hideMask(); //隐藏遮罩层
				JShell.Msg.error(data.msg);
			}
		}, false);
	},
	/**导出EXCEL文件*/
	onExpExcelClick: function() {
		var me = this;
		me.onDownLoadExcel();
	},

	/**创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];

		items.push(Ext.create(me.ReportGridClass, {
			region: 'center',
			header: false,
			split: true,
			collapsible: true,
			itemId: 'ReportGrid',
			reportType: me.reportType,
			/**是否显示导出Excel操作列*/
			isHiddenEXCELActioncolumn: me.isHiddenEXCELActioncolumn,
			isChangeColumnsText: me.isChangeColumnsText,
			createGridColumns: me.createGridColumns
		}));
		items.push(Ext.create(me.ReportTotalGridClass, {
			region: 'south',
			header: false,
			split: true,
			collapsible: true,
			height: 200,
			//selectUrl: me.selectTotalUrl,
			itemId: 'ReportTotalGrid',
			reportType: me.reportTypeTotal,
			isHiddenEXCELActioncolumn: me.isHiddenEXCELActioncolumn,
			isChangeColumnsText: me.isChangeColumnsText,
			createGridColumns: me.createGridTotalColumns
		}));

		return items;
	},
	/*
	 *统计报表的列表的列创建
	 * */
	createGridColumns: function() {
		return [];
	},
	/*
	 *汇总报表的列表的列创建
	 * */
	createGridTotalColumns: function() {
		return [];
	},
	getReportParams: function() {
		var me = this;
		var FilterToolbar = me.getComponent('topToolbar').getComponent('FilterToolbar');
		var ReportGrid = me.getComponent('ReportGrid');
		ReportGrid.params = FilterToolbar.getParams();
		ReportGrid.doFilterParams();
		var postParams = ReportGrid.postParams; //params;
		return postParams;
	},
	getReportTotalParams: function() {
		var me = this;
		var FilterToolbar = me.getComponent('topToolbar').getComponent('FilterToolbar');
		var ReportGrid = me.getComponent('ReportTotalGrid');
		ReportGrid.params = FilterToolbar.getParams();
		ReportGrid.doFilterParams();
		var postParams = ReportGrid.postParams;
		return postParams;
	},
	/**导出EXCEL文件*/
	onDownLoadExcel: function() {
		var me = this,
			operateType = '0';
		var url = JShell.System.Path.ROOT + me.downLoadExcelUrl;

		var postParams = me.getReportParams();
		var reportTotalParams = me.getReportTotalParams();
		var entityJson = Ext.JSON.encode(postParams.entity);
		var detailJson = Ext.JSON.encode(reportTotalParams.entity);

		url += "?entityJson=" + entityJson + "&detailJson=" + detailJson + "&reportType=" + me.reportType + "&operateType=" + operateType;
		window.open(url);
	}
});