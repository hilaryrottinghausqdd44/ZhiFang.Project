/**
 * @description 订单审核
 * @author longfc
 * @version 2017-11-17
 */
Ext.define('Shell.class.rea.client.order.check.OrderGrid', {
	extend: 'Shell.class.rea.client.order.basic.OrderGrid',

	title: '订单审核',
	width: 800,
	height: 500,
	
	/**是否多选行*/
	checkOne: false,
	
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用查询框*/
	hasSearch: true,
	/**录入:entry/审核:check*/
	OTYPE: "check",
	/**下拉状态默认值*/
	defaultStatusValue: "1",
	/**用户UI配置Key*/
	userUIKey: 'order.check.OrderGrid',
	/**用户UI配置Name*/
	userUIName: "订单审核列表",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initSearchDate(-10);
	},
	initComponent: function() {
		var me = this;
		me.defaultWhere = 'reabmscenorderdoc.Status!=0';
		if(!me.checkOne) me.setCheckboxModel();
		me.callParent(arguments);
	},
	/**状态查询选择项过滤*/
	removeSomeStatusList: function() {
		var me = this;
		var tempList = JShell.JSON.decode(JShell.JSON.encode(JShell.REA.StatusList.Status[me.StatusKey].List));
		var itemArr = [];
		//临时
		if(tempList[1]) itemArr.push(tempList[1]);

		Ext.Array.each(itemArr, function(name, index, countriesItSelf) {
			Ext.Array.remove(tempList, itemArr[index]);
		});
		return tempList;
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = me.callParent(arguments);
		columns.splice(3, 0, {
			dataIndex: 'ReaBmsCenOrderDoc_ReaServerCompCode',
			text: '供货商平台编码',
			width: 95,
			defaultRenderer: true
		});
		return columns;
	}
});