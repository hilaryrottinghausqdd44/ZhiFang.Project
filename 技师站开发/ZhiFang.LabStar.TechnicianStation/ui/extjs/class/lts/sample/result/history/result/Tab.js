/**
 * 项目历次检验结果TAB
 * @author Jcall
 * @version 20200327
 */
Ext.define('Shell.class.lts.sample.result.history.result.Tab',{
    extend:'Ext.tab.Panel',
    title:'项目历次检验结果',
    activeTab:0,
    
    //查询条件对象
    searchParams:null,
    
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		me.on({
			tabchange:function(tabPanel,newCard,oldCard,eOpts){
				me.activedTab = newCard;
				if(me.activedTab.onSearch){
					me.activedTab.onSearch(me.searchParams);
				}
			}
		});
	},
	initComponent:function(){
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.activedTab = me.Normal = me.Result = Ext.create('Shell.class.lts.sample.result.history.result.Normal',{
			title:'定性定量结果',itemId:'Normal'
		});
//	    me.Micro = Ext.create('Shell.class.lts.sample.result.history.result.Micro', {
//			title:'微生物结果',itemId:'Micro'
//		});
		return [me.Normal];
	},
	//查询数据
	onSearch:function(searchParams){
		var me = this;
		me.searchParams = searchParams;
		if(me.activedTab){
			if(me.activedTab.onSearch){
				me.activedTab.onSearch(me.searchParams);
			}
		}
	},
	//清空数据,禁用功能按钮
	clearData:function(){
		var me = this;
		if(me.activedTab && Ext.typeOf(me.activedTab.clearData) == 'function'){
			me.activedTab.clearData();
		}
	}
});