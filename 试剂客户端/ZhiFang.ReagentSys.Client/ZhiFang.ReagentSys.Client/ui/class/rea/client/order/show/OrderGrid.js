/**
 * 客户端主订单信息列表
 * @author liangyl
 * @version 2018-10-22
 */
Ext.define('Shell.class.rea.client.order.show.OrderGrid', {
	extend: 'Shell.class.rea.client.order.basic.OrderGrid',

	height: 340,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,	
	/**录入:entry/审核:check*/
	OTYPE: "entry",
	/**下拉状态默认值*/
	defaultStatusValue: "",
	
	/**用户UI配置Key*/
	userUIKey: 'order.show.OrderGrid',
	/**用户UI配置Name*/
	userUIName: "订单查看列表",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initSearchDate(-10);
	},
	initComponent: function() {
		var me = this;
		
		me.callParent(arguments);
	},
	/**状态查询选择项过滤*/
	removeSomeStatusList: function() {
		var me = this;
		var tempList = JShell.JSON.decode(JShell.JSON.encode(JShell.REA.StatusList.Status[me.StatusKey].List));
		var removeArr = [];
		//暂存,已申请
		if(tempList[1]) removeArr.push(tempList[1]);
		if(tempList[2]) removeArr.push(tempList[2]);

		Ext.Array.each(removeArr, function(name, index, countriesItSelf) {
			Ext.Array.remove(tempList, removeArr[index]);
		});
		return tempList;
	}
});