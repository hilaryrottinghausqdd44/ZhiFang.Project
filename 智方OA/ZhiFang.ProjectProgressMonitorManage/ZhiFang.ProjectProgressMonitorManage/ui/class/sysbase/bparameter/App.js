/**
 * 系统参数应用
 * @author longfc
 * @version 2016-09-27
 */
Ext.define('Shell.class.sysbase.bparameter.App', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '系统参数',

	/**对外公开:字典类型系统参数Id*/
	IDS: "4658943850913198560",
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.listenersGrid();
	},
	width: 800,
	initComponent: function() {
		var me = this;
		me.title = me.title || "系统参数";
		me.IDS = me.IDS || "";
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;

		var PDictGrid = Ext.create("Shell.class.sysbase.bparameter.PDictGrid", {
			region: 'west',
			header: false,
			title: me.title || "系统字典",
			width: 260,
			defaultWhere: '(pdict.PDictType.Id in(' + me.IDS + ") or pdict.PDictType.DictTypeCode='BParameter')",
			itemId: 'PDictGrid'
		});
		var Grid = Ext.create("Shell.class.sysbase.bparameter.Grid", {
			region: 'west',
			header: false,
			title: me.title || "系统参数",
			width: 400,
			itemId: 'Grid'
		});
		var Form = Ext.create('Shell.class.sysbase.bparameter.Form', {
			region: 'center',
			header: true,
			itemId: 'Form',
			split: true,
			height: me.height,
			collapsible: false
		});
		return [PDictGrid, Grid, Form];
	},

	/*程序列表的事件监听**/
	listenersGrid: function() {
		var me = this;
		var PDictGrid = me.getComponent('PDictGrid');
		var Grid = me.getComponent('Grid');
		var Form = me.getComponent('Form');

		PDictGrid.on({
			itemclick: function(grid, record, item, index, e, eOpts) {
				JShell.Action.delay(function() {
					var id = record.get(PDictGrid.PKField);
					Grid.defaultWhere = 'bparameter.PDict.Id=' + id;
					Grid.onSearch();
				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					var id = record.get(PDictGrid.PKField);
					Grid.defaultWhere = 'bparameter.PDict.Id=' + id;
					Grid.onSearch();
				}, null, 500);
			},
			nodata: function(p) {
				Grid.clearData();
				Form.disableControl();
			}
		});

		Grid.on({
			itemclick: function(grid, record, item, index, e, eOpts) {
				JShell.Action.delay(function() {
					var id = record.get(Grid.PKField);
					Form.isEdit(id);
				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					var id = record.get(Grid.PKField);
					Form.isEdit(id);
				}, null, 500);
			},
			onAddClick: function() {
				var recs = Grid.getSelectionModel().getSelection();
				if(recs && recs.length > 0) {
					JShell.Msg.error("当前的系统类型已经存在系统参数信息!");
				} else {
					Form.formtype = "add";
					Form.isAdd();
					var records = PDictGrid.getSelectionModel().getSelection();
					var PDictId = records[0].get(PDictGrid.PKField);
					Form.setPDictIdValue(PDictId);
					Form.setParaNoValue(records[0].get("PDict_Shortcode"));
					Form.setNameValue(records[0].get("PDict_CName"));
				}
			},
			onEditClick: function(grid, record) {
				Form.formtype = "edit";
				var id = record.get("PDict_Id");
				Form.load(id);
			},
			onShowClick: function(grid, record) {
				var records = Grid.getSelectionModel().getSelection();
				if(records && records.length < 1) {
					JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
					return;
				}
			},
			nodata: function(p) {
				Form.getForm().reset();
				Form.disableControl();
			}
		});
		Form.on({
			save: function(p, id) {
				Grid.onSearch();
			}
		});
	}
});