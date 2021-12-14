//formComponentsTreeView
Ext.define("ZhiFang.view.FormComponentsTreeView",{
	extend:'Ext.tree.Panel',
	alias: 'widget.formComponentsTreeView',
	id:'FormComponentsTreeView',
	
	border: true,
	store:'FormComponentsTreeStore',
	//hrefTarget: 'formView',
	autoScroll: true,
	
    enableDD : true,//是否可拖拽
    rootVisible: false,//是否显示根节点
    containerScroll : true,//是否支持滚动条 
    autoScroll: false,//内容溢出的时候是否产生滚动条

	initComponent:function(){
		this.callParent(arguments);
	}
});