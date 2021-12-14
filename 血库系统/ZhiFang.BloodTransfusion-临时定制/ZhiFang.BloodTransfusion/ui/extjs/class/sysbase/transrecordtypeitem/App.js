/**
 * 输血过程记录项字典管理
 * @author longfc
 * @version 2020-02-11
 */
Ext.define('Shell.class.sysbase.transrecordtypeitem.App', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '输血过程记录项字典',

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
				me.Form.disableControl();
			}
		});

		me.Grid.on({
			itemclick: function(v, record) {

			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					me.Form.ContentTypeID = record.get("BloodTransRecordTypeItem_BloodTransRecordType_ContentTypeID");
					var id = record.get(me.Grid.PKField);
					me.Form.isEdit(id);
				}, null, 500);
			},
			addclick: function(p) {
				me.Form.isAdd();
			},
			nodata: function(p) {
				me.Form.clearData();
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

		me.TypeGrid = Ext.create('Shell.class.sysbase.transrecordtype.SimpleGrid', {
			region: 'west',
			header: false,
			split: true,
			collapsible: true,
			/**默认数据条件:过滤不良反应分类*/
			defaultWhere: 'bloodtransrecordtype.ContentTypeID!=2',
			itemId: 'TypeGrid'
		});
		me.Grid = Ext.create('Shell.class.sysbase.transrecordtypeitem.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		me.Form = Ext.create('Shell.class.sysbase.transrecordtypeitem.Form', {
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
			me.Form.ContentTypeID = record.get("BloodTransRecordType_ContentTypeID");
			var id = record.get(me.TypeGrid.PKField);
			me.Form.DictTypeId = id;
			me.Grid.defaultWhere = 'bloodtransrecordtypeitem.BloodTransRecordType.Id=' + id;
			me.Grid.onSearch();
		}, null, 500);
	},
	loadForm: function(record) {
		var me = this;
		JShell.Action.delay(function() {
			me.Form.ContentTypeID = record.get("BloodTransRecordTypeItem_BloodTransRecordType_ContentTypeID");
			var id = record.get(me.Grid.PKField);
			me.Form.isEdit(id);
		}, null, 500);
	}
});