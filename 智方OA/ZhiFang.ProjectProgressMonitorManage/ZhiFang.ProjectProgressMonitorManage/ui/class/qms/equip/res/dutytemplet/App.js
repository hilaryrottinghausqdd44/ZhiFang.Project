/**
 * 职责模板维护
 * @author liangyl
 * @version 2017-11-23
 */
Ext.define('Shell.class.qms.equip.res.dutytemplet.App', {
	extend: 'Ext.panel.Panel',
	title: '职责模板维护',
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
				   	me.TempletGrid.EResponsibilityId=id;
				   	me.TempletGrid.loadByDutyId(id);
				},null,500);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
	                var id = record.get(me.DutyGrid.PKField);
	                me.TempletGrid.EResponsibilityId=id;
				   	me.TempletGrid.loadByDutyId(id);
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
	   me.DutyGrid = Ext.create('Shell.class.qms.equip.res.dutytemplet.DutyGrid', {
			border: true,
			title: '职责',
			region: 'west',
			defaultLoad: true,
		    width:340,
		    split: true,
			collapsible: true,
			collapseMode:'mini',
			header: false,
			itemId: 'DutyGrid'
		});
		me.TempletGrid = Ext.create('Shell.class.qms.equip.res.dutytemplet.TempletGrid', {
			region: 'center',
			header: false,
			defaultLoad: false,
			itemId: 'TempletGrid'
		});
		return [me.DutyGrid,me.TempletGrid];
	}
});