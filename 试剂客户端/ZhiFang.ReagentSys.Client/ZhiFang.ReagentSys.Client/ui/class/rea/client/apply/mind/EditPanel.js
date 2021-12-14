/**
 * @description 部门采购申请录入
 * @author liuyj
 * @version 2020-12-15
 */
Ext.define('Shell.class.rea.client.apply.mind.EditPanel', {
	extend: 'Shell.class.rea.client.apply.basic.EditPanel',

	title: '采购申请',
	header: false,

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
		me.ReqDtlGrid = Ext.create('Shell.class.rea.client.apply.mind.ReqDtlGrid', {
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
			OTYPE:"entry"
		});
		var appInfos = [me.ReqDtlGrid, me.ApplyForm];
		return appInfos;
	}
});