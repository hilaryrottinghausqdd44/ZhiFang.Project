/**
 * 微生物结果，先不做
 * @author Jcall
 * @version 20200327
 */
Ext.define('Shell.class.lts.sample.result.history.result.Micro',{
    extend:'Ext.panel.Panel',
    title:'微生物结果',
    
    //是否加载过数据
	hasLoaded:false,
    //查询条件对象
    searchParams:null,
    
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
	},
	initComponent:function(){
		var me = this;
		me.html = me.title;//正式功能需要注释
		me.callParent(arguments);
	},
	//查询数据
	onSearch:function(searchParams){
		var me = this;
		me.searchParams = searchParams;
		
		if(!me.hasLoaded){
			//相关数据变化
			JShell.Msg.alert(me.title + '-数据变化方法');
			me.hasLoaded = true;
		}
	}
});