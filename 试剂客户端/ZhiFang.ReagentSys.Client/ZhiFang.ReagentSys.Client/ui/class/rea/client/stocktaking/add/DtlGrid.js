/**
 * 盘库管理
 * @author longfc
 * @version 2018-03-20
 */
Ext.define('Shell.class.rea.client.stocktaking.add.DtlGrid', {
	extend: 'Shell.class.rea.client.stocktaking.basic.DtlGrid',
	
	title: '盘库明细列表',

	/**获取数据服务路径*/
	selectUrl: '/ReaManageService.svc/RS_UDTO_SearchReaBmsCheckDtlByHQL?isPlanish=true',

	canEdit: true,
	/**盘库单Id*/
	PK: null,
	/**用户UI配置Key*/
	userUIKey: 'stocktaking.add.DtlGrid',
	/**用户UI配置Name*/
	userUIName: "盘库明细列表",

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
		//数据列
		//me.columns = me.createGridColumns();
		me.callParent(arguments);
	}
});