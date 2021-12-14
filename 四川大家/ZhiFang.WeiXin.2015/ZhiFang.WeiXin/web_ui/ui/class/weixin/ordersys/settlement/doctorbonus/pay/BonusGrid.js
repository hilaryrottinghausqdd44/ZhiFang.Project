/**
 * 检查并打款
 * @author longfc
 * @version 2017-03-01
 */
Ext.define('Shell.class.weixin.ordersys.settlement.doctorbonus.pay.BonusGrid', {
	extend: 'Shell.class.weixin.ordersys.settlement.doctorbonus.basic.EditBonusGrid',

	/**默认加载数据*/
	defaultLoad: false,
	/**是否隐藏工具栏查询条件*/
	hiddenbuttonsToolbar: false,
	hasButtontoolbar: true,
	checkOne: true,
	/**导出excel*/
	hasExportExcel: true,
	/**状态查询选择项过滤*/
	removeSomeStatusList: function() {
		var me = this;
		var tempList = me.StatusList;
		var itemArr = [];
		//临时
		if(tempList[1]) itemArr.push(tempList[1]);
		//申请
		if(tempList[2]) itemArr.push(tempList[2]);
		Ext.Array.each(itemArr, function(name, index, countriesItSelf) {
			Ext.Array.remove(tempList, itemArr[index]);
		});
		return tempList;
	}
});