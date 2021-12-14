/**
 * 护士站--血袋接收
 * @author longfc
 * @version 2020-03-17
 */
Ext.define('Shell.class.blood.nursestation.bloodprohandover.ShowPanel', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '血袋信息',
	//发血主单ID
	PK: null,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.onListenersPanel();
	},
	initComponent: function() {
		var me = this;

		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		//发血血袋信息
		me.OutDtlGrid = Ext.create('Shell.class.blood.nursestation.bloodprohandover.out.DtlGrid', {
			region: 'north',
			height: 360,
			header: false,
			border: false,			
			itemId: 'OutDtlGrid',
			split: true,
			collapsible: false
		});
		//血袋接收信息
		me.RecoverGrid = Ext.create('Shell.class.blood.nursestation.bloodprohandover.handover.DtlGrid', {
			region: 'center',
			header: false,
			border: false,
			itemId: 'RecoverGrid',
			split: true,
			collapsible: false
		});
		return [me.OutDtlGrid, me.RecoverGrid];
	},
	onListenersPanel: function() {
		var me = this;
		me.OutDtlGrid.on({
			itemclick: function(grid, record, item, index, e, eOpts) {
				JShell.Action.delay(function() {

				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {

				}, null, 500);
			},
			nodata: function(p) {

			}
		});
	},
	loadData: function(outFormId) {
		var me = this;
		me.PK = outFormId;
		me.OutDtlGrid.PK = outFormId;
		me.RecoverGrid.PK = outFormId;

		me.OutDtlGrid.onSearch();
		me.RecoverGrid.onSearch();
	},
	onNodata: function() {
		var me = this;
		var outFormId = "";
		me.PK = outFormId;
		me.OutDtlGrid.PK = outFormId;
		me.RecoverGrid.PK = outFormId;

		me.OutDtlGrid.onSearch();
		me.RecoverGrid.onSearch();
	}
});
