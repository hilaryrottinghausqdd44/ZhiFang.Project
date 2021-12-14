/**
 * 业务接口配置
 * @author longfc	
 * @version 2018-11-19
 */
Ext.define('Shell.class.rea.client.businessinterface.App', {
	extend: 'Ext.panel.Panel',
	
	title: '业务接口配置',
	width: 700,
	height: 480,
	autoScroll: false,
	layout: {
		type: 'border'
	},
	/**内容周围距离*/
	bodyPadding:'1px',
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.Grid.on({
			itemclick: function(v, record) {
				JShell.Action.delay(function() {
					var id = record.get(me.Grid.PKField);
					me.Form.isEdit(id);
				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					var id = record.get(me.Grid.PKField);
					me.Form.isEdit(id);
				}, null, 500);
			},
			addclick: function(p) {
				me.Form.isAdd();
			},
			editclick: function(val, CName) {
				var records = me.getSelectionModel().getSelection();
				if(records.length != 1) {
					JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
					return;
				}
				var id = records[0].get(me.Grid.PKField);
				me.Form.isEdit(id);			
			},
			nodata: function(p) {
				me.Form.clearData();
			}
		});
		me.Form.on({
			save: function(p, id) {
				me.Grid.onSearch();
			}
		});
	},
	initComponent: function() {
		var me = this;
		//内部组件
		me.items = me.createItems();
		
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this;
		me.Form = Ext.create('Shell.class.rea.client.businessinterface.Form', {
			region: 'east',
			title:'业务接口配置',
			width: 420,
			header: false,
			itemId: 'Form',
		    split: true,
			collapsible: true,
			collapseMode:'mini'
		});

		me.Grid = Ext.create('Shell.class.rea.client.businessinterface.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		return [me.Form,me.Grid];
	}
});