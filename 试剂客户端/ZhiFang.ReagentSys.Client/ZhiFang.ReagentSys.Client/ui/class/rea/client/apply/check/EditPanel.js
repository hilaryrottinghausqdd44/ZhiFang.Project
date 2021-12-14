/**
 * @description 部门采购审核
 * @author longfc
 * @version 2017-10-23
 */
Ext.define('Shell.class.rea.client.apply.check.EditPanel', {
	extend: 'Shell.class.rea.client.apply.basic.EditPanel',

	title: '采购审核',
	header: false,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.ReqDtlGrid.on({
			nodata: function(p) {
				me.ReqDtlGrid.enableControl();
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.formtype = me.formtype || "show";
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.ReqDtlGrid = Ext.create('Shell.class.rea.client.apply.check.ReqDtlGrid', {
			header: false,
			itemId: 'ReqDtlGrid',
			region: 'center',
			collapsible: false,
			collapsed: false,
			PK: me.PK,
			formtype: me.formtype
		});
		me.ApplyForm = Ext.create('Shell.class.rea.client.apply.basic.ApplyForm', {
			header: false,
			itemId: 'ApplyForm',
			region: 'north',
			width: me.width,
			height: 185,
			split: false,
			collapsible: false,
			collapsed: false,
			formtype: me.formtype,
			PK: me.PK,
			OTYPE: "check"
		});
		var appInfos = [me.ReqDtlGrid, me.ApplyForm];
		return appInfos;
	}
});