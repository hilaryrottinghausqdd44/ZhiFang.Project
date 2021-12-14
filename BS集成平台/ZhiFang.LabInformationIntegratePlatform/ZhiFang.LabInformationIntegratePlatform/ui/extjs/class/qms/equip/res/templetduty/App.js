/**
 * 模板职责维护
 * @author liangyl
 * @version 2017-11-23
 */
Ext.define('Shell.class.qms.equip.res.templetduty.App', {
	extend: 'Ext.panel.Panel',
	title: '模板职责维护',
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
	    me.TempletGrid.on({
	    	itemclick:function(v, record) {
				JShell.Action.delay(function(){
				   	var id = record.get('ETemplet_Id');
				   	me.DutyGrid.ETempletId=id;
				   	me.DutyGrid.loadByTempletId(id);
				},null,500);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
				    var id = record.get('ETemplet_Id');
				    me.DutyGrid.ETempletId=id;
				   	me.DutyGrid.loadByTempletId(id);
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
	   me.DutyGrid = Ext.create('Shell.class.qms.equip.res.templetduty.DutyGrid', {
			border: true,
			title: '职责',
			region: 'east',
			split: true,
			collapsible: true,
			collapseMode:'mini',
			defaultLoad: false,
		    width:360,
			header: false,
			itemId: 'DutyGrid'
		});
		me.TempletGrid = Ext.create('Shell.class.qms.equip.res.templetduty.TempletGrid', {
			region: 'center',
			header: false,
			defaultLoad: true,
			itemId: 'TempletGrid'
		});
		return [me.DutyGrid,me.TempletGrid];
	}
});