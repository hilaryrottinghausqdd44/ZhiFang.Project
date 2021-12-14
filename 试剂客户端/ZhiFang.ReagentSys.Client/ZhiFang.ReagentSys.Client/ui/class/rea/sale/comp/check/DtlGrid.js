/**
 * 供货明细列表-供应商
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.sale.comp.check.DtlGrid', {
	extend: 'Shell.class.rea.sale.basic.DtlGrid',
	title: '供货明细列表-供应商',
	
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: false,
	/**是否启用删除按钮*/
	hasDel: false,
	/**是否启用保存按钮*/
	hasSave: true
	
	
	/**复选框*/
//	multiSelect: true,
//	selType: 'checkboxmodel',
//	hasDel: true
});