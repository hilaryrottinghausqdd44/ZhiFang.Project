/**
 * @description 注册证将过期报警
 * @author liangyl
 * @version 2018-08-17
 */
Ext.define('Shell.class.rea.client.registerwarning.willexpire.Panel', {
	extend: 'Shell.class.rea.client.registerwarning.basic.Panel',

	title: '注册证将过期报警',
	header: false,
	border: false,
	/**注册证预警默认将过期预警天数*/
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
		me.QtyGrid = Ext.create('Shell.class.rea.client.registerwarning.willexpire.QtyGrid', {
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