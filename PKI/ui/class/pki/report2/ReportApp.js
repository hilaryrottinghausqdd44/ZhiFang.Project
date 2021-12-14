/**
 * 月度财务锁定报表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.report2.ReportApp', {
	extend: 'Ext.panel.Panel',
	title: '月度财务锁定报表',

	layout: 'border',
	bodyPadding: 1,

	width: 1200,
	height: 600,

	/**默认加载*/
	defaultLoad: true,
	reportType: '3', //报表类型
	isladderPrice: '0', //阶梯价格计算
	ReportGridClass: 'Shell.class.pki.report2.ReportGrid',
	/**下载EXCEL文件服务地址*/
	downLoadExcelUrl: '/StatService.svc/Stat_UDTO_ReportDetailToExcel',
	hasexpAllexcel: false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		var FilterToolbar = me.getComponent('topToolbar').getComponent('FilterToolbar');
		var ReportGrid = me.getComponent('ReportGrid');
		var ReportInfoGrid = me.getComponent('ReportInfoGrid');
		FilterToolbar.on({
			search: function(p, params) {
				ReportGrid.clearData();
				ReportInfoGrid.clearData();
				ReportGrid.params = FilterToolbar.getParams();
				ReportGrid.onSearch();
			}
		});

		ReportGrid.on({
			select: function(rowModel, record) {
				JShell.Action.delay(function() {
					me.showInfo(record);
				});
			},
			itemclick: function(rowModel, record) {
				if(ReportGrid.doActionClick) {
					ReportGrid.doActionClick = false;
					return;
				}
				JShell.Action.delay(function() {
					me.showInfo(record);
				});
			},
			download: function(grid, record) {
				me.onDownLoadExcel(record);
			}
		});

		ReportGrid.store.on({
			load: function(store, records, successful) {
				if(!successful || !records || records.length <= 0) {
					ReportInfoGrid.clearData();
				}
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
		if(me.isladderPrice == '1') {
			me.dockedItems = [{
				dock: 'top',
				itemId: 'topToolbar',
				border: false,
				items: [
					Ext.create('Shell.class.pki.report2.SearchToolbar2', {
						border: false,
						itemId: 'FilterToolbar'
					})
				]
			}];
		} else {
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

		}
		me.dockedItems.push(Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'topToolbarButtons',
			items: [{
				text: '导出全部',
				itemId: 'exp_Allexcel',
				iconCls: 'file-excel',
				hidden: !me.hasexpAllexcel,
				tooltip: '<b>导出全部</b>',
				handler: function() {
					me.onExpExcelClick();
				}
			}]
		}));
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];

		items.push(Ext.create(me.ReportGridClass, {
			region: 'north',
			header: false,
			split: true,
			collapsible: true,
			height: 200,
			itemId: 'ReportGrid',
			reportType: me.reportType,
			createGridColumns: me.createGridColumns
		}));
		items.push(Ext.create('Shell.class.pki.report2.ReportInfoGrid', {
			region: 'center',
			header: false,
			itemId: 'ReportInfoGrid'
		}));

		return items;
	},
	createGridColumns: function() {
		return [];
	},
	showInfo: function(record) {
		var me = this;

		var postParams = me.getInfoPostParams(record);

		if(!postParams) return;

		var ReportInfoGrid = me.getComponent('ReportInfoGrid');
		ReportInfoGrid.postParams = postParams;
		ReportInfoGrid.onSearch();
	},
	getInfoPostParams: function(record) {
		var me = this;
		var parObj = me.getInfoParObj(record);

		if(!parObj) return;

		var ReportGrid = me.getComponent('ReportGrid');

		var postParams = ReportGrid.postParams;
		for(var i in parObj) {
			postParams.entity[i] = parObj[i];
		}

		return postParams;
	},
	/**获取详细信息参数对象*/
	getInfoParObj: function(record) {
		var me = this,
			reportType = me.reportType;

		var SellerID = record.get('DStatClass_SellerID'); //销售
		var DealerID = record.get('DStatClass_DealerID'); //经销商
		var ItemID = record.get('DStatClass_ItemID'); //项目
		var BillingUnitID = record.get('DStatClass_BillingUnitID'); //开票方

		//月度对账报表(1) 查询结果分组：销售，录入时间的月份
		//月度阶梯报表(2) 查询结果分组：经销商，项目，录入时间的月份
		//月度财务锁定报表(3) 查询结果分组：销售，经销商，录入时间的月份
		//开票方对账报表(4) 查询结果分组：开票方，录入时间的月份

		var parObj = {};
		var error = [];
		switch(reportType) {
			case '1':
				parObj.SellerID = SellerID;
				if(!SellerID) error.push('缺少销售');
				break;
			case '2':
				parObj.DealerID = DealerID;
				parObj.ItemID = ItemID;
				if(!DealerID) error.push('缺少经销商');
				if(!ItemID) error.push('缺少项目');
				break;
			case '3':
				parObj.SellerID = SellerID;
				parObj.DealerID = DealerID;
				if(!SellerID) error.push('缺少销售');
				if(!DealerID) error.push('缺少经销商');
				break;
			case '4':
				parObj.BillingUnitID = BillingUnitID;
				parObj.SellerID = SellerID;
				if(!BillingUnitID) error.push('缺少开票方');
				if(!SellerID) error.push('缺少销售');
				break;
			case '5':
				parObj.SellerID = SellerID;
				parObj.DealerID = DealerID;
				parObj.ItemID = ItemID;
				if(!SellerID) error.push('缺少销售');
				if(!DealerID) error.push('缺少经销商');
				if(!ItemID) error.push('缺少项目');
				break;
		}

		if(error.length > 0) {
			JShell.Msg.error(error.join('</br>'));
			return;
		}

		return parObj;
	},
	/**导出EXCEL文件*/
	onDownLoadExcel: function(record) {
		var me = this,
			operateType = '0';

		var url = JShell.System.Path.ROOT + me.downLoadExcelUrl;
		//entityJson={entityJson}&reportFlag={reportFlag}&operateType={operateType}
		//operateType:'1',//直接下载(0),直接打开(1)，默认0

		var postParams = me.getInfoPostParams(record);
		var entityJson = Ext.JSON.encode(postParams.entity);
		//var detailJson = {};
		var reportFileName = record.get('DStatClass_BillingUnitName'); //开票方
		url += "?entityJson=" + entityJson + //"&detailJson=" + detailJson
			"&reportType=" + me.reportType + "&operateType=" + operateType+"&reportFileName="+reportFileName;

		window.open(url);
	}
});