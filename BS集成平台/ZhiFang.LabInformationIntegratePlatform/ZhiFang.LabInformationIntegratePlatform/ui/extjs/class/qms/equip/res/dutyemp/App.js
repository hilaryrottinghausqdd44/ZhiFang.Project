/**
 * 职责人员维护
 * @author liangyl
 * @version 2017-11-23
 */
Ext.define('Shell.class.qms.equip.res.dutyemp.App', {
	extend: 'Ext.panel.Panel',
	title: '职责人员维护',
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
	    me.DutyGrid.on({
	    	itemclick:function(v, record) {
				JShell.Action.delay(function(){
				   	var id = record.get(me.DutyGrid.PKField);
					me.EmpGrid.DutyId=id;
					me.EmpGrid.loadDataId(id);
				},null,500);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
				   	var id = record.get(me.DutyGrid.PKField);
					me.EmpGrid.DutyId=id;
					me.EmpGrid.loadDataId(id);
				},null,500);
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
	   me.EmpGrid = Ext.create('Shell.class.qms.equip.res.dutyemp.EmpGrid', {
			border: true,
			title: '职责人员列表',
			region: 'east',
			split: true,
			collapsible: true,
			collapseMode:'mini',
		    width:360,
			IsCandidaShow:true,
			split: true,
			header: false,
			itemId: 'EmpGrid'
		});
		me.DutyGrid = Ext.create('Shell.class.qms.equip.res.dutyemp.DutyGrid', {
			region: 'center',
			header: false,
			defaultLoad: true,
			itemId: 'Grid'
		});
		return [me.EmpGrid,me.DutyGrid];
	}
});