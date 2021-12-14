/**
 * 财务状态列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.balance2.Grid', {
	extend: 'Shell.class.pki.balance2.ItemGrid',
	
	title: '财务状态列表',
	
	/**查询栏参数设置*/
	searchToolbarConfig:{
		/**是否包含财务*/
		hasFinanceLock:true
	},
});