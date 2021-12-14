/**
 * 货品批号性能验证
 * @author liangyl
 * @version 2017-10-12
 */
Ext.define('Shell.class.rea.client.goodslot.verification.App', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '货品批号性能验证',
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
		me.Form = Ext.create('Shell.class.rea.client.goodslot.verification.Form', {
			region: 'east',
			width: 380,
			header: false,
			itemId: 'Form',
			split: true,
			collapsible: true,
			collapseMode: 'mini'
		});
		me.Grid = Ext.create('Shell.class.rea.client.goodslot.verification.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		return [me.Form, me.Grid];
	},
	onListeners: function() {
		var me = this;
		me.Grid.on({
			itemclick: function(v, record) {
				JShell.Action.delay(function() {
					var id = record.get('ReaGoodsLot_Id');
					me.Form.isEdit(id);
				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					var id = record.get('ReaGoodsLot_Id');
					me.Form.isEdit(id);
				}, null, 500);
			},
			nodata: function(p) {
				me.Form.clearData();
			}
		});
		me.Form.on({
			save: function() {
				me.Grid.onSearch();
			}
		});
	}
});
