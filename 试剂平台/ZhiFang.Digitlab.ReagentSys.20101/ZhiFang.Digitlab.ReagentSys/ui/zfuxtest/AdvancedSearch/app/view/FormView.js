//高级表单查询--全部与查询条件配置:高级查询的表单视图预览
Ext.define("ZhiFang.view.FormView",{
	extend:'Ext.form.Panel',
	alias: 'widget.formView',
	id:'FormView',
	title:'表单',
	//frame:true,//面板渲染
	layout:'absolute',//绝对定位
	autoScroll: true,
	items:[],
	tools:[{
		id:'toggle'
	}],
	initComponent:function(){
		this.callParent(arguments);
	}
});


