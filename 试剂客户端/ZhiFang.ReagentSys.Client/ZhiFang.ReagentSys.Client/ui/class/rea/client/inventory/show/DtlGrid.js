/**
 * 盘库管理
 * @author longfc
 * @version 2019-01-18
 */
Ext.define('Shell.class.rea.client.inventory.show.DtlGrid', {
	extend: 'Shell.class.rea.client.inventory.basic.DtlGrid',
	title: '盘库明细列表',

	/**获取数据服务路径*/
	selectUrl: '/ReaManageService.svc/RS_UDTO_SearchReaBmsCheckDtlByHQL?isPlanish=true',

	canEdit: true,
	/**盘库单Id*/
	PK: null,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			beforeedit: function(editor, e) {
				return me.canEdit;
			}
		});
		me.store.on({
			update: function(store, record) {
				if(record.dirty) {
					var changedObj = record.getChanges();
					for(var modified in changedObj) {
						if(modified == "ReaBmsCheckDtl_CheckQty")
							me.onCheckQtyChanged(record);
					}
				}
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1
		});
		me.callParent(arguments);
	}
});