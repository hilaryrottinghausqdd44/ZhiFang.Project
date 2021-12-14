/**
 * 医生奖金结算申请记录明细列表
 * @author longfc
 * @version 2017-03-01
 */
Ext.define('Shell.class.weixin.ordersys.settlement.doctorbonus.one.BonusGrid', {
	extend: 'Shell.class.weixin.ordersys.settlement.doctorbonus.basic.EditBonusGrid',
	title: '医生奖金结算申请记录',
	/**默认加载数据*/
	defaultLoad: false,
	OSDoctorBonusList: null,
	/**是否隐藏工具栏查询条件*/
	hiddenbuttonsToolbar: false,
	hasButtontoolbar: true,
	checkOne: true,
	/**导出excel*/
	hasExportExcel: false,
	
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