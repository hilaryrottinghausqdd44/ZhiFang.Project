/**
 * 供货方/订货方
 * @author liangyl
 * @version 2018-01-31
 */
Ext.define('Shell.class.rea.client.reacenorg.basic.App', {
	extend: 'Ext.panel.Panel',
	
	title: '供货方/订货方',
	width: 700,
	height: 480,
	
	autoScroll: false,
	layout: {
		type: 'border'
	},
	/**内容周围距离*/
	bodyPadding: '1px',
	/**机构类型 0供货方，1订货方*/
	OrgType: '0',
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var Tree = me.getComponent('Tree');
		var Grid = me.getComponent('Grid');
		Tree.on({
			itemclick: function(v, record) {
				JShell.Action.delay(function() {
					var id = record.get('Id');
					var name = record.get('text');
					var OrgNo = record ? record.get('OrgNo') : '';
					Grid.POrgName = name;
					Grid.POrgID = id;
					Grid.POrgNo = OrgNo
					Grid.onSearch();
				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					var id = record.get('Id');
					var name = record.get('text');
					var OrgNo = record ? record.get('OrgNo') : '';
					Grid.POrgName = name;
					Grid.POrgID = id;
					Grid.POrgNo = OrgNo
					Grid.onSearch();
				}, null, 500);
			}
		});
		me.Grid.on({
			itemdblclick: function(view, record) {
				me.Grid.showForm(record.get(me.Grid.PKField));
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
		me.Tree = Ext.create('Shell.class.rea.client.reacenorg.basic.Tree', {
			region: 'west',
			width: 300,
			header: false,
			OrgType: me.OrgType,
			itemId: 'Tree',
			split: true,
			collapsible: true,
			animCollapse: false,
			animate: false
		});

		me.Grid = Ext.create('Shell.class.rea.client.reacenorg.basic.Grid', {
			region: 'center',
			header: false,
			OrgType: me.OrgType,
			itemId: 'Grid',
			userUIKey: me.userUIKey,
			/**用户UI配置Name*/
			userUIName: me.userUIName
		});
		return [me.Tree, me.Grid];
	}
});