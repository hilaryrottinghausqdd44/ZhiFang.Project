/**
 * 人员职责维护
 * @author liangyl
 * @version 2017-11-23
 */
Ext.define('Shell.class.qms.equip.res.empduty.App', {
	extend: 'Ext.panel.Panel',
	title: '人员职责维护',
	width: 700,
	height: 480,
	autoScroll: false,
	layout: {
		type: 'border'
	},
	/**内容周围距离*/
	bodyPadding:'1px',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	   	me.Tree.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
				   var id = record.get('tid');
					me.EmpGrid.loadByDeptId(id);
				},null,500);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					var id = record.get('tid');
					me.EmpGrid.loadByDeptId(id);
				},null,500);
			}
		});
		me.EmpGrid.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					var id = record.get('HREmployee_Id');
					me.Grid.EmpId=id;
					var hql='eresemp.HREmployee.Id='+id;
					me.Grid.load(hql);
				},null,500);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					var id = record.get('HREmployee_Id');
					me.Grid.EmpId=id;
					var hql='eresemp.HREmployee.Id='+id;
					me.Grid.load(hql);
				},null,500);
			}
		});
		me.EmpGrid.store.on({
			load: function(store, records, successful) {
				if (!successful || !records || records.length <= 0) {
					me.Grid.clearData();
				}
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
	  	me.Tree = Ext.create('Shell.class.sysbase.org.Tree', {
			region: 'west',
			header: false,
			/**是否显示根节点*/
        	rootVisible:false,
		    split: true,
			collapsible: true,
			collapseMode:'mini',
			itemId: 'Tree',
			width:210
		});
		me.EmpGrid = Ext.create('Shell.class.qms.equip.res.empduty.EmpGrid', {
			region: 'west',
			width:330,
			header: false,
			/**是否单选*/
	        checkOne:true,
			itemId: 'EmpGrid',
			split: true,
			collapsible: true,
			collapseMode:'mini',
			/**默认加载*/
			defaultLoad:false
		});
		me.Grid = Ext.create('Shell.class.qms.equip.res.empduty.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid',
			/**默认加载*/
			defaultLoad:false
		});
		return [me.Tree,me.EmpGrid,me.Grid];
	}
});