/**
 * 申请并审核-客户端主订单信息列表
 * @author longfc
 * @version 2021-01-13
 */
Ext.define('Shell.class.rea.client.order.applyandreview.OrderGrid', {
	extend: 'Shell.class.rea.client.order.basic.OrderGrid',

	height: 340,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**录入:entry/审核:check*/
	OTYPE: "entry",
	/**下拉状态默认值*/
	defaultStatusValue: "",

	/**用户UI配置Key*/
	userUIKey: 'order.applyandreview.OrderGrid',
	/**用户UI配置Name*/
	userUIName: "订单申请列表",
	/**是否多选行*/
	checkOne: false,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initSearchDate(-10);
	},
	initComponent: function() {
		var me = this;
		if(!me.checkOne) me.setCheckboxModel();
		me.callParent(arguments);
	},
	setCheckboxModel: function() {
		var me = this;
		//复选框
		me.multiSelect = true;
		me.selType = 'checkboxmodel';
	},
	/**状态查询选择项过滤*/
	removeSomeStatusList: function() {
		var me = this;
		var tempList = JShell.JSON.decode(JShell.JSON.encode(JShell.REA.StatusList.Status[me.StatusKey].List));
		return tempList;
	}
});