/**
 * @description 申请并审核
 * @author longfc
 * @version 2018-02-26
 */
Ext.define('Shell.class.rea.client.apply.applyandreview.ReqDtlGrid', {
	extend: 'Shell.class.rea.client.apply.basic.ReqDtlGrid',

	title: '申请明细信息',
	width: 800,
	height: 500,
	
	/**录入:entry/审核:check/生成订单:create*/
	OTYPE: "applyandreview",
	/**默认加载数据*/
	defaultLoad: false,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	
	/**用户UI配置Key*/
	userUIKey: 'apply.applyandreview.ReqDtlGrid',
	/**用户UI配置Name*/
	userUIName: "申请并审核明细列表",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1
		});
		me.addEvents('onSaveAllDt');
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		//创建数据集
		//me.store = me.createStore();
		me.decreaseUserUI();
		me.callParent(arguments);
	}
});