/**
 * 审核设置
 * @author liangyl	
 * @version 2020-05-08
 */
Ext.define('Shell.class.lts.operate.App', {
	extend:'Ext.tab.Panel',
     //检验小组ID
    SectionID:1,
    title:'审定范围设置',
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
	    me.on({
			/**页签切换事件处理*/
			tabchange: function(tabPanel, newCard, oldCard, eOpts) {
				var me = this;
				switch(newCard.itemId) {
					case 'CheckerForm':
					    me.CheckerForm.loadDatas(); //数据加载
						break;
					default:
					    me.HandlerForm.loadDatas(); //数据加载
						break
				}
			}
		});
		me.HandlerForm.on({
			save:function(){
			   me.close();
			}
		});
		me.CheckerForm.on({
			save:function(){
			   me.close();
			}
		});
	},
    
	initComponent:function(){
		var me = this;
		me.activeTab = 0;//初始页签
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		me.HandlerForm = Ext.create('Shell.class.lts.operate.HandlerForm',{
			closable:false,
			itemId:'HandlerForm',
			border:false,
			SectionID:me.SectionID,
			title:'检验确认人设置'
		});
		
		me.CheckerForm = Ext.create('Shell.class.lts.operate.CheckerForm',{
			closable:false,
			border:false,
			itemId:'CheckerForm',
			SectionID:me.SectionID,
			title:'审核人设置'
		});
		return [me.HandlerForm,me.CheckerForm];
	}
});