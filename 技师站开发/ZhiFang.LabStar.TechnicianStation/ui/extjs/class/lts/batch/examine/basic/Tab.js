/**
 * 检验单和消息页签
 * @author liangyl	
 * @version 2021-03-23
 */
Ext.define('Shell.class.lts.batch.examine.basic.Tab', {
	extend:'Ext.tab.Panel',
	requires:[
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.DateArea'
	],
    layout : 'fit',
    //检验小组
    SectionID:null,
    //检验单列表
    TestFormGrid:'Ext.panel.Panel',
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
	},
	initComponent:function(){
		var me = this;
		me.activeTab = 0;//初始页签
		me.items = me.createItems();
		
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		me.TestForm = Ext.create('Shell.class.lts.batch.examine.basic.TestForm',{
			header:false,itemId:'TestForm',closable:false,border:false,
			TestFormGrid:me.TestFormGrid,SectionID:me.SectionID//小组
		});
		
		me.Info = Ext.create('Shell.class.lts.batch.examine.basic.Info',{
			header:false,itemId:'Info',closable:false,border:false,
			SectionID:me.SectionID
		});
		
		return [me.TestForm,me.Info];
	},
	setValueMsg : function(smsg,fmsg){
		this.Info.setValue(smsg,fmsg)
	},
	onSearchTestForm : function(obj){
		this.TestForm.onSearchTestForm(obj)
	},
	getIdList: function(){
		return this.TestForm.getIdList();
	}
});