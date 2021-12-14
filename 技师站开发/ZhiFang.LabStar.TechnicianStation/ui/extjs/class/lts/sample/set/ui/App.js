/**
 * 页面显示设置
 * @author Jcall
 * @version 2020-07-09
 */
Ext.define('Shell.class.lts.sample.set.ui.App',{
	extend:'Ext.form.Panel',
	title:'页面显示设置',
	width:500,
	height:300,
	
	//获取系统参数
	getListUrl:'',
    
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
	},
	initComponent:function(){
		var me = this;
		me.callParent(arguments);
	}
});