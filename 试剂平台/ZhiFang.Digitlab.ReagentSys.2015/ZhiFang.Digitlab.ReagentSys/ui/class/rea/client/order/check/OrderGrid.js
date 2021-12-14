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

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用查询框*/
	hasSearch: true,
	/**录入:entry/审核:check*/
	OTYPE: "check",
	/**下拉状态默认值*/
	defaultStatusValue: "1",
	/**审核服务地址*/
	//editUrl: '/ReaSysManageService.svc/',
	initComponent: function() {
		var me = this;
		me.defaultWhere ='bmscenorderdoc.ReaCompID is not null and bmscenorderdoc.Status!=0';

		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**状态查询选择项过滤*/
	removeSomeStatusList: function() {
		var me = this;
		var tempList = me.StatusList;
		var itemArr = [];
		//临时
		if(tempList[1]) itemArr.push(tempList[1]);

		Ext.Array.each(itemArr, function(name, index, countriesItSelf) {
			Ext.Array.remove(tempList, itemArr[index]);
		});
		return tempList;
	}
});