/**
 * 解锁消费码
 * @author GHX
 * @version 2021-01-22
 */
Ext.define('Shell.class.weixin.sampling.basic.apply.unlock.App', {
	extend:'Shell.ux.panel.AppPanel',
	title:'解锁消费码',	
	//是否加载过数据
	hasLoaded:false,
	account:'',
	WeblisSourceOrgID:'',
	WeblisSourceOrgName:'',
	ConsumerAreaID:'',
	afterRender:function(){
		var me = this;		
		me.callParent(arguments);		
	},
	initComponent:function(){
		var me = this;		
		//创建内部组件
		me.items = me.createItems();
		me.callParent(arguments);
	},
	
	//创建内部组件
	createItems: function () {
		var me = this;
		me.grid = Ext.create('Shell.class.weixin.sampling.basic.apply.unlock.Grid', {
			region: 'center', itemId: 'Grid', 
			header: true, border: false,
			autoScroll: true, split: true,
			collapsible: false,animCollapse: false,
			WeblisSourceOrgID:me.WeblisSourceOrgID,
			WeblisSourceOrgName:me.WeblisSourceOrgName,
			ConsumerAreaID:me.ConsumerAreaID
		});

		return [me.grid];
	}
	
	
});