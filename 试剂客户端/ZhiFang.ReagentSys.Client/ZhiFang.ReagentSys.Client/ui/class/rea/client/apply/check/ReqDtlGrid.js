/**
 * @description 部门采购审核
 * @author longfc
 * @version 2017-10-23
 */
Ext.define('Shell.class.rea.client.apply.check.ReqDtlGrid', {
	extend: 'Shell.class.rea.client.apply.basic.ReqDtlGrid',

	title: '采购审核',
	width: 800,
	height: 500,

	/**录入:entry/审核:check/生成订单:create*/
	OTYPE: "check",
	
	/**用户UI配置Key*/
	userUIKey: 'apply.check.ReqDtlGrid',
	/**用户UI配置Name*/
	userUIName: "采购审核明细列表",
	
	defaultOrderBy: [{
		property: 'ReaBmsReqDtl_DispOrder',
		direction: 'ASC'
	}],
	
	initComponent: function() {
		var me = this;
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1
		});
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	}
});