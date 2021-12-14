/**
 * @description 部门采购生成订单
 * @author longfc
 * @version 2018-02-26
 */
Ext.define('Shell.class.rea.client.apply.generateorders.ReqDtlGrid', {
	extend: 'Shell.class.rea.client.apply.basic.ReqDtlGrid',

	title: '生成订单',
	width: 800,
	height: 500,

	/**录入:entry/审核:check/生成订单:create*/
	OTYPE: "create",
	/**是否启用同步库存按钮*/
	hasSyncQty:false,
	
	/**用户UI配置Key*/
	userUIKey: 'apply.generateorders.ReqDtlGrid',
	/**用户UI配置Name*/
	userUIName: "生成订单明细列表",
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