/**
 * @description 效期已过期
 * @author longfc
 * @version 2018-03-20
 */
Ext.define('Shell.class.rea.client.validitywarning.expired.Panel', {
	extend: 'Shell.class.rea.client.validitywarning.basic.Panel',

	title: '效期已过期',
	header: false,
	border: false,
	/**效期预警默认已过期天数*/
	expiredDays: 1,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.QtyGrid = Ext.create('Shell.class.rea.client.validitywarning.expired.QtyGrid', {
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