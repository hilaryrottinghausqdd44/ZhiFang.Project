/**
 * 合同对比关联客户及付款单位
 * @author longfc
 * @version 2017-03-29
 */
Ext.define('Shell.class.wfm.business.associate.contract.contrast.App', {
	extend: 'Shell.class.wfm.business.associate.basic.App',
	title: '合同对比',
	/**修改合同服务地址*/
	editUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePContractByField',

	/**左列表行没有选择时提示信息*/
	alertMsg: "没有选择合同信息!",
	/**操作类型:contrast:对比;check:审核;*/
	operationType: "contrast",
	GridCalss: 'Shell.class.wfm.business.associate.contract.basic.Grid',
	/**更新信息类型:PClient;PayOrg*/
	updateType: 'PClient',
	/**左列表的客户ID列名*/
	PClientID: "PClientID",
	/**左列表的客户名称列名*/
	PClientName: "PClientName",
	/**左列表的收款名称列名*/
	PayOrgName: "PayOrg"
});