/**
 * 登录科室记录项结语
 * @author longfc
 * @version 2020-02-11
 */
Ext.define('Shell.class.sysbase.screcordphrase.ofcurdept.App', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '科室记录项结果短语',
	/**是否检验项目对照*/
	ISTESTITEM:true,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.RecordItemtGrid.on({
			itemclick: function(v, record) {
				me.loadGrid(record);
			},
			select: function(RowModel, record) {
				me.loadGrid(record);
			},
			nodata: function(p) {
				me.Form.BObjectId = null;
				me.Grid.clearData();
				me.Form.disableControl();
			}
		});

		me.Grid.on({
			itemclick: function(v, record) {

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

		me.loadGrid();
	},

	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		
		me.RecordItemtGrid = Ext.create('Shell.class.sysbase.screcordtypeitem.SimpleGrid', {
			region: 'west',
			header: false,
			split: true,
			defaultLoad: true,
			collapsible: true,
			itemId: 'RecordItemtGrid'
		});
		
		me.Grid = Ext.create('Shell.class.sysbase.screcordphrase.Grid', {
			region: 'center',
			header: false,
			/**是否检验项目对照*/
			ISTESTITEM:me.ISTESTITEM,
			itemId: 'Grid'
		});
		me.Form = Ext.create('Shell.class.sysbase.screcordphrase.Form', {
			region: 'east',
			header: true,
			itemId: 'Form',
			split: true,
			collapsible: false,
			width: 240
		});

		return [me.RecordItemtGrid, me.Grid, me.Form];
	},
	loadGrid: function(record) {
		var me = this;
		var deptId = JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.DEPTID) || "";
		if(!deptId){
			me.Grid.clearData();
			return ;
		}
		JShell.Action.delay(function() {
			var id = record.get(me.RecordItemtGrid.PKField);
			me.Form.BObjectId = id;
			me.Grid.defaultWhere = 'screcordphrase.TypeObjectId=' + deptId+' and screcordphrase.BObjectId=' + id;
			me.Grid.onSearch();
		}, null, 500);
	},
	loadForm: function(record) {
		var me = this;
		JShell.Action.delay(function() {
			var id = record.get(me.Grid.PKField);
			me.Form.isEdit(id);
		}, null, 500);
	}
});