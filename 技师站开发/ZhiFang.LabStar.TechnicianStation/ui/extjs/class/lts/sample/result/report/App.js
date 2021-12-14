/**
 * 报告
 * @author Jcall
 * @version 2020-03-22
 */
Ext.define('Shell.class.lts.sample.result.report.App', {
	extend:'Shell.ux.panel.AppPanel',
	title:'报告',
	
	//是否加载过数据
	hasLoaded:false,
	
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
	onSearch:function(testFormRecord){
		var me = this;
		if(!me.hasLoaded){
			//相关数据变化
			JShell.Msg.alert(me.title + '-数据变化方法');
			me.hasLoaded = true;
		}
	}
});