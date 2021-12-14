//高级表单查询--全部与查询条件配置:表单控件类型数据源
Ext.define("ZhiFang.store.FormComponentsTreeStore",{
	extend:'Ext.data.TreeStore',
	fields:['text','url'],
    proxy: {
		type: 'ajax',
		url:'server/GetFormComponentsTree.json'
    },
    defaultRootProperty: 'tree',//子节点的属性名
    root: {
		text: '功能菜单',
		leaf: false,
		expanded: true,
		url: 'about.html'
	}
});