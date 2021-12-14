/**
 * 合同价格查询列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.contractprice.SearchGrid', {
	extend: 'Shell.class.pki.contractprice.EditGrid',
	title: '合同价格查询列表',
	showSuccessInfo: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**是否带修改价格功能*/
	canEditPrice: false,
	/**导出功能*/
	buttonToolbarItems: ['exp_excel', {
		itemId: 'btnImportOldSysContractIn',
		xtype: 'button',
		hidden: true,
		text: '导入旧财务系统的合同数据',
		iconCls: 'file-excel',
		tooltip: '<b>导入旧财务系统的合同数据</b>'
	}],
	/**下载EXCEL文件服务地址*/
	downLoadExcelUrl: '/StatService.svc/Stat_UDTO_DContractPriceToExcel',
	selectUrl: '/StatService.svc/Stat_UDTO_SearchDContractPrice',
	/**导入旧财务系统的合同数据*/
	doImportOldSysContractInfoUrl: '/StatService.svc/Stat_UDTO_ImportOldSysContractInfo',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var com = me.getComponent('buttonsToolbar').getComponent("btnImportOldSysContractIn");
		var strUserAccount = Ext.util.Cookies.get('000301');
		if (com && strUserAccount === 'admin') {
			com.setVisible(true);
			com.on({
				click: function(obj, e) {
					me.onImportExcelClick();
				}
			});
		}
	},
	/**导入旧财务系统的合同数据*/
	onImportExcelClick: function() {
		var me = this;
		me.onImportOldSysContractInfo();
	},
	/**导入旧财务系统的合同数据*/
	onImportOldSysContractInfo: function() {
		var me = this;
		var url = JShell.System.Path.ROOT + me.doImportOldSysContractInfoUrl;
		me.showMask("正在导入旧财务系统的合同数据"); //显示遮罩层
		JShell.Server.get(url, function(data) {
			me.hideMask(); //隐藏遮罩层
			if (data.success) {
				if (me.showSuccessInfo) {
					JShell.Msg.alert("数据导入完毕！<br />请查看导入的数据，如有疑问，请检查日志文件！", null, me.hideTimes);
				}
				//me.onSearch();
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**导出EXCEL文件*/
	onExpExcelClick: function() {
		var me = this;
		me.onDownLoadExcel();
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

		url += "?entityJson=" + entityJson + "&reportType=" + operateType;
		window.open(url);
	},
	getReportParams: function() {
		var me = this;
		var postParams = me.postParams;
		return postParams;
	},
	getReportTotalParams: function() {
		var me = this;
		var postParams = me.postParams;
		return postParams;
	}
});