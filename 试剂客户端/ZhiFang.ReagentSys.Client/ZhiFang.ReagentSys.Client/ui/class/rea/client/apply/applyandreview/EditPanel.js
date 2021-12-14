/**
 * @description 申请并审核
 * @author longfc
 * @version 2018-02-26
 */
Ext.define('Shell.class.rea.client.apply.applyandreview.EditPanel', {
	extend: 'Shell.class.rea.client.apply.basic.EditPanel',

	title: '申请并审核',
	header: false,

	/**录入:entry/审核:check/生成订单:create*/
	OTYPE: "applyandreview",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.ReqDtlGrid.on({
			onAddAfter: function(grid) {
				me.setDeptNameReadOnly(true);
			},
			onDelAfter: function(grid) {
				var bo = true;
				if(me.ReqDtlGrid.store.getCount() <= 0) bo = false;
				me.setDeptNameReadOnly(bo);
			},
			onEditAfter: function(grid) {
				JShell.Action.delay(function() {
					me.setDeptNameReadOnly(true);
				}, null, 500);
			},
			nodata: function(p) {
				//if(me.formtype=="add")
				me.ReqDtlGrid.enableControl();
				me.setDeptNameReadOnly(false);
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
		me.ReqDtlGrid = Ext.create('Shell.class.rea.client.apply.applyandreview.ReqDtlGrid', {
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
			PK: me.PK,
			formtype: me.formtype,
			OTYPE: "applyandreview"
		});
		var appInfos = [me.ReqDtlGrid, me.ApplyForm];
		return appInfos;
	}
});