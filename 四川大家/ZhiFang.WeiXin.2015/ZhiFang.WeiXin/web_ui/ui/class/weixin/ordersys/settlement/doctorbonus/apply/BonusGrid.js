/**
 * 医生奖金结算申请记录明细列表
 * @author longfc
 * @version 2017-03-01
 */
Ext.define('Shell.class.weixin.ordersys.settlement.doctorbonus.apply.BonusGrid', {
	extend: 'Shell.class.weixin.ordersys.settlement.doctorbonus.basic.BonusGrid',
	title: '医生奖金结算记录',
	/**默认加载数据*/
	defaultLoad: true,
	
	checkOne: true,
	hasPagingtoolbar: false,
	hiddenbuttonsToolbar: true,
	hasButtontoolbar: false,
	/**后台排序*/
	remoteSort: false,
	OSDoctorBonusList: null,
	initComponent: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if(me.hasButtontoolbar) items.push(me.createButtontoolbar());
		if(me.hasPagingtoolbar) items.push(me.createPagingtoolbar());
		return items;
	},
	/**@public 根据where条件加载数据*/
	load: function() {
		var me = this,
			collapsed = me.getCollapsed();
		me.defaultLoad = true;
		//收缩的面板不加载数据,展开时再加载，避免加载无效数据
		if(collapsed) {
			me.isCollapsed = true;
			return;
		}
		if(me.OSDoctorBonusList != null) {
			me.store.loadData(me.OSDoctorBonusList);
		} else {
			me.clearData();
		}
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
	}
});