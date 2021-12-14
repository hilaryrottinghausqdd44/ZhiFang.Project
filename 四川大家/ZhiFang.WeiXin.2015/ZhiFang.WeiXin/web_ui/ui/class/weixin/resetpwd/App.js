/**
 * 微信帐号密码重置
 * @author longfc
 * @version 2017-12-19
 */
Ext.define('Shell.class.weixin.resetpwd.App', {
	extend:'Shell.ux.panel.AppPanel',

	title: '微信帐号密码重置',

	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**设置各页签的显示标题*/
	setTitles: function() {
		var me = this;
	},
	createItems: function() {
		var me = this;
		me.PatientGrid = Ext.create('Shell.class.weixin.resetpwd.PatientGrid', {
			title: '微信帐号密码重置',
			header: true,
			itemId: 'PatientGrid',
			region: 'center',
			defaultLoad: true,
			split: true,
			collapsible: false,
			collapsed: false
		});
		var maxWidth = document.body.clientWidth * 0.92/2;
		if(!maxWidth||maxWidth<480)maxWidth=480;
		me.DoctorGrid = Ext.create('Shell.class.weixin.resetpwd.DoctorGrid', {
			title: '医生微信帐号密码重置',
			region: 'east',
			width:maxWidth,
			header: true,
			itemId: 'DoctorGrid',
			defaultLoad: true,
			split: true,
			collapsible: false,
			collapsed: false
		});
		return [me.DoctorGrid, me.PatientGrid];
	}
});