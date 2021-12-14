/**
 * 借款单申请
 * @author longfc
 * @version 2016-11-09
 */
Ext.define('Shell.class.oa.ploanbill.apply.FormApp', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '借款单申请',
	formtype:'',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},

	initComponent: function() {
		var me = this;
		me.formtype=me.formtype||"add";
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		var empID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || "";
		me.pempfinanceaccountForm = Ext.create('Shell.class.sysbase.user.pempfinanceaccount.show.Form', {
			region: 'west',
			header: true,
			split: true,
			itemId: 'pempfinanceaccountForm',
			formtype: 'show',
			border: false,
			title: "财务账户信息",
			height: me.height,
			width: 165,
			defaultLoad: true,
			EmpID: empID
		});
		me.basicForm = Ext.create('Shell.class.oa.ploanbill.apply.Form', {
			region: 'center',
			header: false,
			split: true,
			itemId: 'basicForm',
			formtype: me.formtype,
			border: false,
			title: me.basicFormTitle,
			width: me.width - me.pempfinanceaccountForm.width,
			height: me.height,
			PK: me.PK
		});
		return [me.pempfinanceaccountForm, me.basicForm];
	}
});