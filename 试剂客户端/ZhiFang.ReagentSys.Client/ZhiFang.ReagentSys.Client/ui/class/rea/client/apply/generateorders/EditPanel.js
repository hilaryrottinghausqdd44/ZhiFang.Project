/**
 * @description 部门采购生成订单
 * @author longfc
 * @version 2018-02-26
 */
Ext.define('Shell.class.rea.client.apply.generateorders.EditPanel', {
	extend: 'Shell.class.rea.client.apply.basic.EditPanel',

	title: '生成订单',
	header: false,
	/**录入:entry/审核:check/生成订单:create*/
	OTYPE: "create",

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
		me.ReqDtlGrid = Ext.create('Shell.class.rea.client.apply.generateorders.ReqDtlGrid', {
			header: false,
			itemId: 'ReqDtlGrid',
			region: 'center',
			collapsible: false,
			PK: me.PK,
			collapsed: false,
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
			OTYPE: "create"
		});
		var appInfos = [me.ReqDtlGrid, me.ApplyForm];
		return appInfos;
	}
});