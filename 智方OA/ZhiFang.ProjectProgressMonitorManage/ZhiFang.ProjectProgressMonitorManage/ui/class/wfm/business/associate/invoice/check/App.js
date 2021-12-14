/**
 * 发票对比审核
 * @author liangyl
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.business.associate.invoice.check.App', {
	extend: 'Shell.class.wfm.business.associate.invoice.contrast.App',
	title: '发票对比审核',
	/**修改收款服务地址*/
	editUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePInvoiceByFieldManager',

	/**左列表行没有选择时提示信息*/
	alertMsg: "没有选择发票信息!",
	/**操作类型:contrast:对比;check:审核;*/
	operationType: "check",
	GridCalss: 'Shell.class.wfm.business.associate.invoice.check.Grid',
	/**更新信息类型:PClient;PayOrg*/
	updateType: 'PClient',
	/**左列表的客户ID列名*/
	PClientID: "ClientID",
	/**左列表的客户名称列名*/
	PClientName: "ClientName"
});