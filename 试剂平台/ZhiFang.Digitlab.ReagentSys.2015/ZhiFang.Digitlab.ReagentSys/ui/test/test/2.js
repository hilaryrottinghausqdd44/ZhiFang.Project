root:{
	text:'功能菜单',
	iconCls:'main-lefttree-root-img-16',
	leaf:false,
	expanded:false,
	id:0
},
afterRender:function(){
	var me = this;
	me.callParent(arguments);
	//展现数据
	me.getRootNode().expand();
},
store:Ext.create('Ext.data.TreeStore',{
	autoLoad:false,
	....
}),
load:function(){
    var me = this;
    //加载数据
	me.store.load();
},