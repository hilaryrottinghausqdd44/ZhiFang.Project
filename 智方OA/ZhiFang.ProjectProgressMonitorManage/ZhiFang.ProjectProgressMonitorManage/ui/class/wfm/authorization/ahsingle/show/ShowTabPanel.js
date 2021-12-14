/**
 * 单站点授权
 * @author longfc
 * @version 2016-12-13
 */
Ext.define('Shell.class.wfm.authorization.ahsingle.show.ShowTabPanel', {
	extend: 'Ext.tab.Panel',
	title: '单站点授权信息查看面板',
	width: 720,
	height: 400,
	/**单站点授权ID*/
	PK: null,
	ContentPanel: 'Shell.class.wfm.authorization.ahsingle.show.ContentPanel',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.bodyPadding=1;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this;
		me.Form = Ext.create(me.ContentPanel, {
			title: '单站点授权内容',
			border: false,
			PK: me.PK
		});
		me.Interaction = Ext.create('Shell.class.sysbase.scinteraction.App', {
			title: '交流信息',
			border: false,
			FormPosition: 'e',
			PK: me.PK
		});
		me.Operate = Ext.create('Shell.class.wfm.authorization.basic.SCOperation', {
			title: '操作记录',
			border: false,
			classNameSpace: 'ZhiFang.Entity.ProjectProgressMonitorManage', //类域
			className: 'LicenceStatus', //类名
			formtype: 'show',
			itemId: 'Operate',
			hasLoadMask: false,
			PK: me.PK
		});
		return [me.Form, me.Interaction,me.Operate];
	}
});