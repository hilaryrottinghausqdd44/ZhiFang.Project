/**
 * 审定设置
 * @author liangyl	
 * @version 2021-03-19
 */
Ext.define('Shell.class.lts.sample.operate.Tab', {
	extend:'Ext.tab.Panel',
     //检验小组ID
    SectionID:1,
    title:'审定设置',
    //初始页签
    activeTab:0,
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
	},
    
	initComponent:function(){
		var me = this;
		me.activeTab = me.activeTab;//初始页签
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		me.HandlerForm = Ext.create('Shell.class.lts.sample.operate.HandlerForm',{
			closable:false,
			itemId:'HandlerForm',
			border:false,
			SectionID:me.SectionID,
			title:'检验确认人(初审人)设置'
		});
		
		me.CheckerForm = Ext.create('Shell.class.lts.sample.operate.CheckerForm',{
			closable:false,
			itemId:'CheckerForm',
			border:false,
			SectionID:me.SectionID,
			title:'审定人设置'
		});
		
		me.App = Ext.create('Shell.class.lts.sample.set.system.judge.App',{
			closable:false,
			border:false,
			itemId:'App',
			margin:'1px 0px 0px 0px',
			SectionID:me.SectionID,
			title:'智能审核(系统判定)'
		});
		
		return [me.HandlerForm,me.CheckerForm,me.App];
	}
});