/**
 * 编辑
 * @author liangyl	
 * @version 2016-12-15
 */
Ext.define('Shell.class.wfm.authorization.ahsingle.apply.EditTabPanel', {
	extend: 'Ext.tab.Panel',
	
	title: '编辑页签',
	width: 560,
	height: 400,
	/**表单参数*/
	FormConfig: null,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.Form.on({
			save: function(Form, id) {
				me.PK = id;
				me.fireEvent('save', me, me.PK);
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.addEvents('save');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this;
		me.Form = Ext.create('Shell.class.wfm.authorization.ahsingle.apply.Form', Ext.apply({
			title: '授权内容',
			PK: me.PK,
			formtype: 'edit'
		}, me.FormConfig));
		me.Interaction = Ext.create('Shell.class.sysbase.scinteraction.App', {
			title: '交流信息',
			FormPosition: 'e',
			PK: me.PK
		});
		me.Operate = Ext.create('Shell.class.wfm.authorization.basic.SCOperation', {
			title: '操作记录',
			classNameSpace: 'ZhiFang.Entity.ProjectProgressMonitorManage', //类域
			className: 'LicenceStatus', //类名
			formtype: 'show',
			itemId: 'Operate',
			hasLoadMask: false,
			PK: me.PK
		});
		return [me.Form, me.Interaction, me.Operate];
	}
});