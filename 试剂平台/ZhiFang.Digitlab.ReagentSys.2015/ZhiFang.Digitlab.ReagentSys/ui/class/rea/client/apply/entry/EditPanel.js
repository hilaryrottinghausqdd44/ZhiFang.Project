/**
 * @description 部门采购申请录入
 * @author longfc
 * @version 2017-10-23
 */
Ext.define('Shell.class.rea.client.apply.entry.EditPanel', {
	extend: 'Shell.class.rea.client.apply.basic.EditPanel',

	title: '采购申请',
	header: false,

	/**录入:entry/审核:check/生成订单:create*/
	OTYPE: "entry",

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
		me.ReqDtlGrid = Ext.create('Shell.class.rea.client.apply.entry.ReqDtlGrid', {
			header: false,
			itemId: 'ReqDtlGrid',
			region: 'center',
			collapsible: false,
			collapsed: false,
			PK: me.PK,
			formtype: me.formtype,
			OTYPE: me.OTYPE
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
			PK: me.PK,
			formtype: me.formtype,
			OTYPE: me.OTYPE
		});
		var appInfos = [me.ReqDtlGrid, me.ApplyForm];
		return appInfos;
	}
});