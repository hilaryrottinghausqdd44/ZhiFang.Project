/**
 * 选择模块树
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.role.ModuleCheckTree',{
    extend:'Shell.ux.tree.CheckPanel',
	
	title:'功能模块',
	width:300,
	height:500,
	
	/**获取数据服务路径*/
	selectUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_SearchModuleTreeBySessionHREmpID',
	//selectUrl:'/RBACService.svc/RBAC_UDTO_SearchRBACModuleToListTree?fields=RBACModule_DataTimeStamp',
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
	/**默认选中节点*/
	autoSelectIds:null,
	
	afterRender:function(){
		var me = this ;
		me.callParent(arguments);
		
		me.store.on({
			load:function(){
				if(me.autoSelectIds){
					me.changeChecked(me.autoSelectIds);
				}
			}
		});
	},
	initComponent:function(){
		var me = this;
		
		//me.addEvents('save');
		
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
		
		dockedItems.push(Ext.create('Shell.ux.toolbar.Button', {
			dock: 'bottom',
			itemId: 'bottomToolbar',
			items: ['->','save']
		}));
		
		return dockedItems;
	},
	changeData:function(data){
		var me = this;
		me._lastData = data;
    	var changeNode = function(node){
    		//图片地址处理
    		if(node['icon'] && node['icon'] != ''){
    			node['icon'] = JShell.System.Path.getModuleIconPathBySize(16) + "/" + node['icon'];
    		}
    		//node.DataTimeStamp = node.value.DataTimeStamp;
    		
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
			{name:'DataTimeStamp',type:'auto'},//时间戳
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
	/**更改勾选*/
	changeChecked2:function(ids){
		var me = this;
			arr = ids ? ids.split(',') : [],
			len = arr.length;
		
		me.autoSelectIds = ids;
		
		//先将所有节点设置为不选中状态
        me.onCancelCheck();
        //收缩
        me.onMinusClick();
        
		for(var i=0;i<len;i++){
			var node = me.selectNode(arr[i]);
			me.setNodeTrue(node);
			me.setChildStyle(node);
		}
	},
	/**更改勾选*/
	changeChecked:function(ids){
		var me = this;
			arr = ids ? ids.split(',') : [],
			len = arr.length;
		
		me.autoSelectIds = ids;
		
		if(!me._lastData) return;
		
		me.disableControl();//禁用所有的操作功能
		
		var changeNode = function(node){
    		node['checked'] = false;//还原为不选中
    		node['expanded'] = false;//默认收起
    		
    		for(var i=0;i<len;i++){
    			if(node['tid'] == arr[i]){
    				node['checked'] = true;//选中
					
					//if(node['leaf']==false)node['expanded'] = true;//
					
    				break;
    			}
    		}
    		
    		var children = node[me.defaultRootProperty];
    		if(children){
    			changeChildren(children);
    		}
    	};
    	
    	var changeChildren = function(children){
    		Ext.Array.each(children,changeNode);
    	};
		
    	//收缩
    	//me.onMinusClick();
		
		var root = me.root;
		root.expanded = true;//默认展开
		root.Tree = me._lastData.Tree;
		changeChildren(root.Tree);
		me.setRootNode(root);
		
    	me.enableControl();//启用所有的操作功能
	},
	/**保存*/
	onSaveClick:function(){
		var me = this,
			nodes = me.getChecked();
			
		me.fireEvent('save',me,nodes);
	},
	/**启用所有的操作功能*/
	enableControl: function(bo) {
		var me = this,
			enable = bo === false ? false : true,
			toolbars = me.dockedItems.items || [],
			length = toolbars.length,
			items = [];

		for (var i = 0; i < length; i++) {
			if (toolbars[i].xtype == 'header') continue;
			if (toolbars[i].isLocked) continue;
			var fields = toolbars[i].items.items;
			items = items.concat(fields);
		}

		var iLength = items.length;
		for (var i = 0; i < iLength; i++) {
			items[i][enable ? 'enable' : 'disable']();
		}
	},
	/**禁用所有的操作功能*/
	disableControl: function() {
		this.enableControl(false);
	}
});