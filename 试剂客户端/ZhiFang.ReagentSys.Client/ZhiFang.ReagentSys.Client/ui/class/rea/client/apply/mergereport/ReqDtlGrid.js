/**
 * @description 采购申请合并报表
 * @author longfc
 * @version 2018-11-27
 */
Ext.define('Shell.class.rea.client.apply.mergereport.ReqDtlGrid', {
	extend: 'Shell.class.rea.client.apply.basic.ReqDtlGrid',

	title: '合并报表',
	width: 800,
	height: 500,

	/**录入:entry/审核:check/生成订单:create*/
	OTYPE: "mergereport",
	
	/**用户UI配置Key*/
	userUIKey: 'apply.mergereport.ReqDtlGrid',
	/**用户UI配置Name*/
	userUIName: "采购申请合并报表明细列表",
	defaultOrderBy: [{
		property: 'ReaBmsReqDtl_DispOrder',
		direction: 'ASC'
	}],
	initComponent: function() {
		var me = this;

		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	}
});