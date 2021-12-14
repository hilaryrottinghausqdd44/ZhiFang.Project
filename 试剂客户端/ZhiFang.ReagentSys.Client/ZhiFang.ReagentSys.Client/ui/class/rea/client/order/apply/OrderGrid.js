/**
 * 客户端主订单信息列表
 * @author longfc
 * @version 2017-11-15
 */
Ext.define('Shell.class.rea.client.order.apply.OrderGrid', {
	extend: 'Shell.class.rea.client.order.basic.OrderGrid',

	height: 340,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**录入:entry/审核:check*/
	OTYPE: "entry",
	/**下拉状态默认值*/
	defaultStatusValue: "",
	/**是否多选行*/
	checkOne: false,
	
	/**用户UI配置Key*/
	userUIKey: 'order.apply.OrderGrid',
	/**用户UI配置Name*/
	userUIName: "订单申请列表",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		if(!me.checkOne) me.setCheckboxModel();
		me.initSearchDate(-10);
	},
	initComponent: function() {
		var me = this;
		//me.initSetProvider();
		me.callParent(arguments);
	},
	/**状态查询选择项过滤*/
	removeSomeStatusList: function() {
		var me = this;
		var tempList = JShell.JSON.decode(JShell.JSON.encode(JShell.REA.StatusList.Status[me.StatusKey].List));
		return tempList;
	}
});