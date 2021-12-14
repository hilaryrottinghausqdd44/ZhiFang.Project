/**
 * 机构货品选择列表
 * @author longfc
 * @version 2017-09-11
 */
Ext.define('Shell.class.rea.client.order.GoodsOrgLinkCheck', {
	extend: 'Shell.class.rea.client.goodsorglink.CheckGrid',
	title: '货品选择列表',
	width: 875,
	height: 540,
	/**是否单选*/
	checkOne: false,
	/**编辑前的行选中集合*/
	SelectionRecords: null,
	
	initComponent: function() {
		var me = this;
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1,
			listeners: {
					beforeedit: function(editor, e, eOpts) {
						me.SelectionRecords = me.getSelectionModel().getSelection();
						if(me.SelectionRecords && me.SelectionRecords.length > 0) {
							me.getSelectionModel().select(me.SelectionRecords, false);
						}
					},
					canceledit: function(editor, e, eOpts) {
						if(me.SelectionRecords && me.SelectionRecords.length > 0) {
							me.getSelectionModel().select(me.SelectionRecords, false);
							me.SelectionRecords = null;
						}
					},
					edit: function(editor, e, eOpts) {
						if(me.SelectionRecords && me.SelectionRecords.length > 0) {
							me.getSelectionModel().select(me.SelectionRecords, false);
							me.SelectionRecords = null;
						}
					}
				}
		});	
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns =me.callParent(arguments);
		columns.splice(1, 0,{
			dataIndex: 'GoodsQty',
			text: '<b style="color:blue;">申请数量</b>',
			width: 65,
			editor: {
				xtype: 'numberfield',
				allowBlank: false,
				minValue: 0,
				listeners: {
					change: function(com, newValue) {
						var record = com.ownerCt.editingPlugin.context.record;
						record.set('GoodsQty', newValue);
						//record.commit();
						me.getView().refresh();
					}
				}
			}
		});
		return columns;
	}
});