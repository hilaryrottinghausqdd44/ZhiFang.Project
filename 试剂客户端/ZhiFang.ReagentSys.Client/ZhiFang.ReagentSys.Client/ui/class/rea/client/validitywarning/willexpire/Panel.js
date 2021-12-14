/**
 * @description 效期将过期报警
 * @author longfc
 * @version 2018-03-20
 */
Ext.define('Shell.class.rea.client.validitywarning.willexpire.Panel', {
	extend: 'Shell.class.rea.client.validitywarning.basic.Panel',

	title: '效期将过期报警',
	header: false,
	border: false,
	/**效期预警默认将过期预警天数*/
	willexpireDays: 10,
	
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
		me.QtyGrid = Ext.create('Shell.class.rea.client.validitywarning.willexpire.QtyGrid', {
			header: false,
			itemId: 'QtyGrid',
			region: 'center',
			collapsible: false,
			collapsed: false,
			willexpireDays:me.willexpireDays
		});
		var appInfos = [me.QtyGrid];
		return appInfos;
	}
});