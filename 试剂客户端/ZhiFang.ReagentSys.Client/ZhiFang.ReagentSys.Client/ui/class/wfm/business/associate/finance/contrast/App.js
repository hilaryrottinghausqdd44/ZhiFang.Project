/**
 * 商务收款对比关联客户及付款单位
 * @author longfc
 * @version 2017-03-29
 */
Ext.define('Shell.class.wfm.business.associate.finance.contrast.App', {
	extend: 'Shell.class.wfm.business.associate.basic.App',
	title: '商务收款对比',
	/**修改合同服务地址*/
	editUrl: '/SingleTableService.svc/ST_UDTO_UpdatePFinanceReceiveOfAssociateByField',

	/**左列表行没有选择时提示信息*/
	alertMsg: "没有选择商务收款信息!",
	/**操作类型:contrast:对比;check:审核;*/
	operationType: "contrast",
	GridCalss: 'Shell.class.wfm.business.associate.finance.basic.Grid',
	/**更新信息类型:PClient;PayOrg*/
	updateType: 'PClient',
	/**左列表的客户ID列名*/
	PClientID: "PClientID",
	/**左列表的客户名称列名*/
	PClientName: "PClientName"
});