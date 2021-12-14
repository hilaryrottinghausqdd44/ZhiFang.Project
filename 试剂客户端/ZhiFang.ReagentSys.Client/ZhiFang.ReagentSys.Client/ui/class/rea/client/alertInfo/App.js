/**
 * 预警设置
 * @author liangyl	
 * @version 2018-02-27
 */
Ext.define('Shell.class.rea.client.alertInfo.App', {
	extend: 'Ext.panel.Panel',

	title: '预警设置',
	width: 700,
	height: 480,
	autoScroll: false,
	layout: {
		type: 'border'
	},
	/**内容周围距离*/
	bodyPadding: '1px',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.onListeners();
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
		me.Form = Ext.create('Shell.class.rea.client.alertInfo.Form', {
			region: 'east',
			title: '预警信息',
			width: 280,
			header: false,
			itemId: 'Form',
			split: true,
			collapsible: true,
			collapseMode: 'mini'
		});

		me.Grid = Ext.create('Shell.class.rea.client.alertInfo.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		return [me.Form, me.Grid];
	},
	/**
	 * @description 联动处理
	 */
	onListeners: function() {
		var me = this;

		me.Grid.on({
			itemclick: function(v, record) {
				JShell.Action.delay(function() {
					var id = record.get(me.Grid.PKField);
					me.Form.GridData = me.Grid.store.data.items;
					me.Form.isEdit(id);
				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					var id = record.get(me.Grid.PKField);
					me.Form.GridData = me.Grid.store.data.items;
					me.Form.isEdit(id);
				}, null, 500);
			},
			addclick: function(p) {
				var buttonsToolbar = me.Grid.getComponent("buttonsToolbar");
				var AlertTypeId = buttonsToolbar.getComponent("ReaAlertInfoSettings_AlertTypeId").getValue();
				me.Form.changeColor('');
				me.Form.GridData = me.Grid.store.data.items;
				me.Form.isAdd();
				//设置默认值
				if (!AlertTypeId) return;
				me.Form.setAlertType(AlertTypeId);

			},
			editclick: function(val, CName) {
				var records = me.getSelectionModel().getSelection();
				if (records.length != 1) {
					JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
					return;
				}
				var id = records[0].get(me.Grid.PKField);
				me.Form.GridData = me.Grid.store.data.items;
				me.Form.isEdit(id);
			},
			nodata: function(p) {
				me.Form.clearData();
			}
		});
		me.Form.on({
			save: function(p, id) {
				me.Grid.onSearch();
			},
			load: function(p, data) {
				if (data.value) {
					p.changeColor(data.value.ReaAlertInfoSettings_AlertColor);
				}
			}
		});
	}
});
