/**
 * 部门货品维护
 * @author liangyl
 * @version 2016-11-14
 */
Ext.define('Shell.class.rea.client.deptgoods.App', {
	extend: 'Ext.panel.Panel',
	
	title: '部门货品维护',
	width: 700,
	height: 480,
	autoScroll: false,
	layout: {
		type: 'border'
	},
	/**内容周围距离*/
	bodyPadding:'1px',
	/**对外公开，1显示所有部门树，0只显示用户自己的树*/
	ISOWN:'0',
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//
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
		me.Tree = Ext.create('Shell.class.rea.client.deptgoods.DeptTree', {
			region: 'west',
			width: 300,
			header: false,
			ISOWN:me.ISOWN,
			itemId: 'Tree',
			split: true,
			collapsible: true,
			collapseMode:'mini'	
		});

		me.Grid = Ext.create('Shell.class.rea.client.deptgoods.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		return [me.Tree,me.Grid];
	},
	/**
	 * @description 联动处理
	 */
	onListeners:function(){
		var me=this;
		var Tree = me.getComponent('Tree');
		var Grid = me.getComponent('Grid');
		Tree.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					var id = record.get('tid');
					var name = record.get('text1');
					Grid.OrgId=id;
					Grid.OrgName=name;
					Grid.onSearch();
				},null,500);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					var id = record.get('tid');
					var name = record.get('text1');
					Grid.OrgId=id;
					Grid.OrgName=name;
					Grid.onSearch();
				},null,500);
			}
		});
		me.Grid.on({
			itemdblclick: function(view, record) {
				me.Grid.showForm(record.get(me.Grid.PKField));
			}
		});
	}
});