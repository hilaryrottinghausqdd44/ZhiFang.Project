/***
 *  资质证件管理
 * @author longfc
 * @version 2017-07-14
 */
Ext.define('Shell.class.rea.cenorg.qualification.manage.App', {
	extend: 'Ext.panel.Panel',

	title: '资质证件管理',
	header: false,
	border: false,
	//width:680,
	/**默认加载数据时启用遮罩层*/
	hasLoadMask: true,
	bodyPadding: 1,
	layout: {
		type: 'border'
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.Grid.on({
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					var id = record.get(me.Grid.PKField);
					me.Form.isEdit(id);
				}, null, 500);
			},
			addclick: function(p) {
				me.Form.isAdd(null);
			},
			editclick: function(p) {
				var records = me.Grid.getSelectionModel().getSelection();
				if(!records || records.length != 1) {
					JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
					return;
				}
				var id = records[0].get(me.Grid);
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
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.Grid = Ext.create('Shell.class.rea.cenorg.qualification.manage.Grid', {
			header: false,
			itemId: 'Grid',
			region: 'center',
			split: true,
			collapsible: true,
			collapsed: false,
			hasEdit: false,
			onAddClick: function() {
				me.Grid.fireEvent('addclick', this);
			},
			onEditClick: function() {
				me.Grid.fireEvent('editclick', this);
			}
		});
		me.Form = Ext.create('Shell.class.rea.cenorg.qualification.manage.Form', {
			region: 'east',
			header: false,
			itemId: 'Form',
			split: true,
			collapsible: false,
			collapsed: false,
			width: 280
		});
		var appInfos = [me.Grid, me.Form];
		return appInfos;
	}
});