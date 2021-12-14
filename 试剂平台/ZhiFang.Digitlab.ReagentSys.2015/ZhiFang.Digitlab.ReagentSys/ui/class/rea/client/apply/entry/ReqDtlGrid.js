/**
 * @description 部门采购申请录入申请明细列表
 * @author longfc
 * @version 2017-10-23
 */
Ext.define('Shell.class.rea.client.apply.entry.ReqDtlGrid', {
	extend: 'Shell.class.rea.client.apply.basic.ReqDtlGrid',

	title: '申请明细信息',
	width: 800,
	height: 500,
	/**录入:entry/审核:check/生成订单:create*/
	OTYPE: "entry",
	/**默认加载数据*/
	defaultLoad: false,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**默认每页数量*/
	defaultPageSize: 150,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1
		});
		me.CurDeptId = me.CurDeptId || "";
		me.addEvents('onSaveAllDt');
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		//创建数据集
		me.store = me.createStore();

		me.callParent(arguments);
	}
});