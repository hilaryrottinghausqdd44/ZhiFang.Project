/**
 * 选择模块树
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.module.CheckboxTree',{
    extend:'Shell.ux.tree.CheckPanel',
	
	title:'功能模块',
	width:300,
	height:500,
	
	/**获取数据服务路径*/
	selectUrl:'/RBACService.svc/RBAC_UDTO_SearchModuleTreeBySessionHREmpID',
	/**默认加载数据*/
	defaultLoad:true,
	/**根节点*/
	root:{
		text:'所有模块',
		iconCls:'main-package-16',
		id:0,
		tid:0,
		leaf:false,
		expanded:false
	},
	afterRender:function(){
		var me = this ;
		me.callParent(arguments);
		
		me.store.on({
			load:function(){
				me.getSelectionModel().select(me.root.tid);
			}
		});
	},
	initComponent:function(){
		var me = this;
		
		me.topToolbar = me.topToolbar || ['-','->',{
			iconCls:'button-right',
			tooltip:'<b>收缩面板</b>',
			handler:function(){me.collapse();}
		}];
		
		me.callParent(arguments);
	},
	
	createDockedItems:function(){
		var me = this;
		var dockedItems = me.callParent(arguments);
		
		dockedItems[0].items = dockedItems[0].items.concat(me.topToolbar);
		
		return dockedItems;
	},
	changeData:function(data){
		var me = this;
    	var changeNode = function(node){
    		//图片地址处理
    		if(node['icon'] && node['icon'] != ''){
    			node['icon'] = JShell.System.Path.getModuleIconPathBySize(16) + "/" + node['icon'];
    		}
    		
    		var children = node[me.defaultRootProperty];
    		if(children){
    			changeChildren(children);
    		}
    	};
    	
    	var changeChildren = function(children){
    		Ext.Array.each(children,changeNode);
    	};
    	
    	var children = data[me.defaultRootProperty];
    	changeChildren(children);
    	
    	return data;
	},
	/**获取数据字段*/
	getStoreFields:function(){
		var me = this;
		
		var fields = [
			{name:'checked',type:'bool'},
			{name:'text',type:'auto'},//默认的现实字段
			{name:'expanded',type:'auto'},//是否默认展开
			{name:'leaf',type:'auto'},//是否叶子节点
			{name:'icon',type:'auto'},//图标
			{name:'url',type:'auto'},//地址
			{name:'tid',type:'auto'}//默认ID号
		];
			
		return fields;
	},
	/**获取ROOTURL*/
	getPathRoot:function(){
		return JShell.System.Path.LIIP_ROOT;
	}
});
	