/**
 * 仪器维护
 * @author liangyl
 * @version 2016-08-12
 */
Ext.define('Shell.class.qms.equip.App', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '仪器维护',
	layout: {
		type: 'border',
		regionWeights: {
			west: 2,
			north: 1
		}
	},
	width: 1000,
	height: 800,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var Grid = me.getComponent('Grid');
		Grid.on({
			itemdblclick:function(view,record){
				Grid.openForm(record.get('EEquip_Id'),'edit','2');
			},
			/**@overwrite 新增按钮点击处理方法*/
			onAddClick: function() {
				Grid.openForm(null,'add','1');
			},
			/**@overwrite 修改按钮点击处理方法*/
			onEditClick: function() {
				var records = Grid.getSelectionModel().getSelection();
				if(!records || records.length != 1) {
					JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
					return;
				}
				var id = records[0].get(Grid.PKField);
				Form.PK = id;
				Form.isEdit(id);
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.title = me.title || "仪器维护";
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.Grid = Ext.create('Shell.class.qms.equip.Grid', {
			border: true,
			title: '仪器列表',
			region: 'center',
			PKField:'EEquip_Id',
			split: true,
			header: false,
			itemId: 'Grid'
		});
		return [ me.Grid];
	}
});