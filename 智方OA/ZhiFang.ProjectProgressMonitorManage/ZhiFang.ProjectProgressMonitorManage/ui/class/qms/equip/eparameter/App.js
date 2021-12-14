/**
 * 质量记录系统参数
 * @author liangyl
 * @version 2018-05-04
 */
Ext.define('Shell.class.qms.equip.eparameter.App', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '质量记录系统参数',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var Grid = me.getComponent('Grid');
		var Form = me.getComponent('Form');
		Grid.on({
			itemclick: function(grid, record, item, index, e, eOpts) {
				JShell.Action.delay(function() {
					var id = record.get('EParameter_ParaNo');
					var ParaName = record.get('EParameter_ParaName');
					var ParaType = record.get('EParameter_ParaType');
					Form.ParaObj.PK=id;
					Form.ParaObj.ParaName=ParaName;
					Form.ParaObj.ParaType=ParaType;
					Form.isEdit(id);
  				    
				}, null, 200);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					var id = record.get('EParameter_ParaNo');
					var ParaName = record.get('EParameter_ParaName');
					var ParaType = record.get('EParameter_ParaType');
					Form.ParaObj.PK=id;
					Form.ParaObj.ParaName=ParaName;
					Form.ParaObj.ParaType=ParaType;
					Form.isEdit(id);
				}, null, 200);
			},
			nodata: function(p) {
				Form.clearData();
//				Form.isEdit(null);
			}
		});
		Form.on({
			save: function(p, id) {
				Grid.onSearch();
			}
		});
	},
	width: 800,
	initComponent: function() {
		var me = this;
		me.title = me.title || "系统参数";
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		var Grid = Ext.create("Shell.class.qms.equip.eparameter.Grid", {
			region: 'center',
			header: false,
			title: '质量记录参数',
			itemId: 'Grid'
		});
		var Form = Ext.create('Shell.class.qms.equip.eparameter.Form', {
			region: 'east',header: false,itemId: 'Form',
			split: true,width: 420,
			collapsible: true,
			collapseMode:'mini'
		});
		return [Grid, Form];
	}
});