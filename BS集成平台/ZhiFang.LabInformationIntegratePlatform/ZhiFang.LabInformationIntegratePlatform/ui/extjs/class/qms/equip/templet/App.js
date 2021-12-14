/**
 * 仪器模板维护
 * @author liangyl
 * @version 2016-08-12
 */
Ext.define('Shell.class.qms.equip.templet.App', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '仪器模板维护',
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
		
		me.Tree.on({
			itemdblclick:function(v, record) {
				JShell.Action.delay(function(){
					var id = record.get('tid');
					var name = record.get('text');
					var leaf = record.get('leaf');
					me.Grid.BDictTreeId=id;
		            me.Grid.onSearch();
				},null,500);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					var id = record.get('tid');
					var name = record.get('text');
					var leaf = record.get('leaf');
					me.Grid.BDictTreeId=id;
		            me.Grid.onSearch();
				},null,500);
			}
		});
		
		me.Grid.on({
			onAddClick:function(grid){
				var records = me.Tree.getSelectionModel().getSelection();
				if(records.length != 1) {
					JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
					return;
				}
				var tid=records[0].get('tid');
				var leaf=records[0].get('leaf');
				if(leaf==false){
					JShell.Msg.alert("请选择具体小组添加模板！");
					return;
				}
				me.Grid.openForm(null, 'add',tid,'1');
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.title = me.title || "仪器模板维护";
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
        me.Tree = Ext.create('Shell.class.qms.equip.templet.Tree', {
			region: 'west',
			width: 230,
			header: false,
			itemId: 'Tree',
			split: true,
			collapsible: true,
			collapseMode:'mini'
		});
		me.Grid = Ext.create('Shell.class.qms.equip.templet.Grid', {
			border: true,
			title: '仪器模板列表',
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		return [ me.Tree,me.Grid];
	}
});