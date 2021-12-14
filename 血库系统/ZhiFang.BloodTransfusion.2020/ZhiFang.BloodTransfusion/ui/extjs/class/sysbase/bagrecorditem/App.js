/**
 * 血袋记录项字典
 * @author longfc
 * @version 2020-02-11
 * 
 */
Ext.define('Shell.class.sysbase.bagrecorditem.App', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '血袋记录项字典',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.TypeGrid.on({
			itemclick: function(v, record) {
				me.loadGrid(record);
			},
			select: function(RowModel, record) {
				me.loadGrid(record);
			},
			nodata: function(p) {
				me.Form.ContentTypeID = null;
				me.Form.DictTypeId = null;
				me.Grid.clearData();
				me.Form.getForm().reset();
				me.Form.disableControl();
			}
		});

		me.Grid.on({
			itemclick: function(v, record) {
				me.loadForm(record);
			},
			select: function(RowModel, record) {
				me.loadForm(record);
			},
			addclick: function(p) {
				me.Form.isAdd();
			},
			nodata: function(p) {
				me.Form.clearData();
				me.Form.getForm().reset();
				me.Form.disableControl();
			}
		});
		me.Form.on({
			save: function(p, id) {
				me.Grid.onSearch();
			}
		});

		me.TypeGrid.onSearch();
	},

	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;

		me.TypeGrid = Ext.create('Shell.class.sysbase.bagrecordtype.SimpleGrid', {
			region: 'west',
			header: false,
			split: true,
			collapsible: true,
			/**默认数据条件:过滤不良反应分类*/
			defaultWhere: 'bloodbagrecordtype.ContentTypeID!=2',
			itemId: 'TypeGrid'
		});
		me.Grid = Ext.create('Shell.class.sysbase.bagrecorditem.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		me.Form = Ext.create('Shell.class.sysbase.bagrecorditem.Form', {
			region: 'east',
			header: true,
			itemId: 'Form',
			split: true,
			collapsible: false,
			width: 240
		});

		return [me.TypeGrid, me.Grid, me.Form];
	},
	loadGrid: function(record) {
		var me = this;
		JShell.Action.delay(function() {
			me.Form.ContentTypeID = record.get("BloodBagRecordType_ContentTypeID");
			var id = record.get(me.TypeGrid.PKField);
			me.Form.DictTypeId = id;
			me.Grid.defaultWhere = 'bloodbagrecorditem.BloodBagRecordType.Id=' + id;
			me.Grid.onSearch();
		}, null, 500);
	},
	loadForm: function(record) {
		var me = this;
		JShell.Action.delay(function() {
			me.Form.ContentTypeID = record.get("BloodBagRecordItem_BloodBagRecordType_ContentTypeID");
			var id = record.get(me.Grid.PKField);
			me.Form.isEdit(id);
		}, null, 500);
	}
});