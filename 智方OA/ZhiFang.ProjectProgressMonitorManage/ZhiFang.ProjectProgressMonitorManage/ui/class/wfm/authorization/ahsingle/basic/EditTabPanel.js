/**
 * 单站点授权
 * @author longfc
 * @version 2016-12-15
 */
Ext.define('Shell.class.wfm.authorization.ahsingle.basic.EditTabPanel', {
	extend: 'Ext.tab.Panel',
	title: '单站点授权',

	width: 600,
	height: 400,

	/**通过按钮文字*/
	OverButtonName: '',
	/**不通过按钮文字*/
	BackButtonName: '',
	/**通过状态文字*/
	OverName: '',
	/**不通过状态文字*/
	BackName: '',
	/**处理意见字段*/
	OperMsgField: '',
	/**处理时间字段*/
	AuditDataTimeMsgField: '',
	/**授权ID*/
	PK: null,
	/**表单参数*/
	FormConfig: null,
	/**是否使用编辑表单*/
	isUseEditForm: false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.Form.on({
			save: function(p, id) {
				me.fireEvent('save', me, id);
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.bodyPadding = 1;
		me.addEvents('save');
		me.items = me.createItems();

		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this;
		if(me.isUseEditForm == true) {
			me.Form = Ext.create('Shell.class.wfm.authorization.ahsingle.oneaudit.Form', Ext.apply({
				title: '授权内容',
				border: false,
				PK: me.PK,
				itemId: 'basicForm',
				formtype: 'edit'
			}, me.FormConfig));
		} else {
			me.Form = Ext.create('Shell.class.wfm.authorization.ahsingle.basic.ActionForm', Ext.apply({
				title: '授权内容',
				border: false,
				itemId: 'basicForm',
				OverButtonName: me.OverButtonName,
				BackButtonName: me.BackButtonName,
				OverName: me.OverName,
				BackName: me.BackName,
				OperMsgField: me.OperMsgField,
				AuditDataTimeMsgField: me.AuditDataTimeMsgField,
				PK: me.PK
			}, me.FormConfig));
		}
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
		return [me.Form, me.Interaction, me.Operate];
	}
});