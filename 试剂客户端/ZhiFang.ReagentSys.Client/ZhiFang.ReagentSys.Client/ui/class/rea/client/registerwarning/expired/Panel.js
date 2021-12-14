/**
 * @description 注册证已过期
 * @author liangyl
 * @version 2018-08-17
 */
Ext.define('Shell.class.rea.client.registerwarning.expired.Panel', {
	extend: 'Shell.class.rea.client.registerwarning.basic.Panel',

	title: '注册证已过期',
	header: false,
	border: false,
	/**效期已过期默认已过期天数*/
	expiredDays: 1,
	
	initComponent: function() {
		var me = this;
		//me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.QtyGrid = Ext.create('Shell.class.rea.client.registerwarning.expired.QtyGrid', {
			header: false,
			itemId: 'QtyGrid',
			region: 'center',
			collapsible: false,
			collapsed: false,
			expiredDays:me.expiredDays
		});
		var appInfos = [me.QtyGrid];
		return appInfos;
	}
});