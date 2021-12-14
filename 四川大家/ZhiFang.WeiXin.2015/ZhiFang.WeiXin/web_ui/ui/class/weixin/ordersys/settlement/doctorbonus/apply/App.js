/**
 * 医生奖金结算申请
 * @author longfc
 * @version 2017-02-27
 */
Ext.define('Shell.class.weixin.ordersys.settlement.doctorbonus.apply.App',{
    extend:'Shell.class.weixin.ordersys.settlement.doctorbonus.basic.App',
    title:'医生奖金结算申请',
    GridCalss:'Shell.class.weixin.ordersys.settlement.doctorbonus.apply.Grid',
    TabPanelCalss:'Shell.class.weixin.ordersys.settlement.doctorbonus.basic.TabPanel',
    BonusGridCalss: 'Shell.class.weixin.ordersys.settlement.doctorbonus.apply.EditBonusGrid',
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
	},  
	initComponent:function(){
		var me = this;
		me.callParent(arguments);
	}
});