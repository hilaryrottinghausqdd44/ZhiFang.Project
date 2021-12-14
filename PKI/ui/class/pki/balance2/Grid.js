/**
 * 财务状态列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.balance2.Grid', {
	extend: 'Shell.class.pki.balance2.ItemGrid',

	title: '财务状态列表',
	/**默认不加载*/
	defaultLoad: false,
	/**查询栏参数设置*/
	searchToolbarConfig: {
		/**是否包含财务*/
		hasFinanceLock: true
	},
	//创建挂靠功能栏
	dockedItems: [Ext.create('Shell.class.pki.balance2.SearchToolbar', Ext.apply({}, {
		itemId: 'filterToolbar',
		dock: 'top',
		isLocked: true,
		hasFinanceLock: true,
		height: 105
	}))]
});