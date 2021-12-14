/**
 * 历史对比图
 * @author Jcall
 * @version 2020-03-27
 */
Ext.define('Shell.class.lts.sample.result.history.ChartPanel', {
	extend:'Ext.panel.Panel',
	title:'历史对比图',
	
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
		//相关数据变化
		JShell.Msg.alert(me.title + '-数据变化方法');
	}
});