/**
 * 医生奖金结算申请记录明细列表
 * @author longfc
 * @version 2017-03-01
 */
Ext.define('Shell.class.weixin.ordersys.settlement.doctorbonus.apply.EditBonusGrid', {
	extend: 'Shell.class.weixin.ordersys.settlement.doctorbonus.basic.EditBonusGrid',
	title: '医生奖金结算申请记录',
	/**默认加载数据*/
	defaultLoad: true,
	OSDoctorBonusList: null,
	/**是否隐藏工具栏查询条件*/
	hiddenbuttonsToolbar: true,
	hasButtontoolbar: false,
	checkOne: true,
	/**导出excel*/
	hasExportExcel: false
});