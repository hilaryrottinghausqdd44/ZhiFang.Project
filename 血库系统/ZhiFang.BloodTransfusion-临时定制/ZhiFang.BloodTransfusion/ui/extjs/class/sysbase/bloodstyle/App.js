/**
 * 血制品维护
 * @author longfc
 * @version 2020-04-10
 */
Ext.define('Shell.class.sysbase.bloodstyle.App', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '血制品维护',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.BloodClassGrid.on({
			itemclick: function(v, record) {
				me.loadData(record);
			},
			select: function(RowModel, record) {
				me.loadData(record);
			},
			nodata: function(p) {
				me.Grid.clearData();
				me.Form.disableControl();
			},
			addclick: function(p) {
				me.openBloodClassForm();
			},
			editclick: function(p, record) {
				me.openBloodClassForm(record.get(me.BloodClassGrid.PKField));
			}
		});

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
				me.Form.getForm().setValues(me.Grid.BloodClass);
			},
			nodata: function(p) {
				me.Form.disableControl();
			}
		});
		me.Form.on({
			save: function(p, id) {
				me.Grid.onSearch();
			}
		});

		me.BloodClassGrid.onSearch();
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;

		me.BloodClassGrid = Ext.create('Shell.class.sysbase.bloodclass.SimpleGrid', {
			region: 'west',
			header: false,
			split: true,
			collapsible: true,
			width: 285,
			itemId: 'BloodClassGrid'
		});
		me.Grid = Ext.create('Shell.class.sysbase.bloodstyle.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		me.Form = Ext.create('Shell.class.sysbase.bloodstyle.Form', {
			region: 'east',
			header: true,
			itemId: 'Form',
			split: true,
			collapsible: false,
			width: 240
		});

		return [me.BloodClassGrid, me.Grid, me.Form];
	},
	loadData: function(record) {
		var me = this;
		JShell.Action.delay(function() {
			var id = record.get(me.BloodClassGrid.PKField);
			me.Form.BloodClassId = id;
			me.Grid.BloodClass = {
				Bloodstyle_BloodClass_Id: id,
				Bloodstyle_BloodClass_CName: record.get("BloodClass_CName")
			};
			me.Grid.defaultWhere = 'bloodstyle.BloodClass.Id=' + id;
			me.Grid.onSearch();
		}, null, 500);
	},
	/**打开表单*/
	openBloodClassForm: function(id) {
		var me = this;
		var config = {
			showSuccessInfo: false,
			resizable: false,
			formtype: 'add',
			width: 260,
			height: 360,
			listeners: {
				save: function(win) {
					me.BloodClassGrid.onSearch();
					win.close();
				}
			}
		};
		if (id) {
			config.formtype = 'edit';
			config.PK = id;
		}
		JShell.Win.open('Shell.class.sysbase.bloodclass.Form', config).show();
	}
});
