/**
 * 客户与授权系统对照审核
 * @author liangyl
 * @version 2017-03-29
 */
Ext.define('Shell.class.wfm.business.associate.client.check.App', {
	extend: 'Shell.class.wfm.business.associate.client.contrast.App',
	title: '客户对照审核',
	
	/**操作类型:contrast:对比;check:审核;*/
	operationType: "check",
	GridCheckCalss: 'Shell.class.wfm.business.associate.client.check.Grid'

});