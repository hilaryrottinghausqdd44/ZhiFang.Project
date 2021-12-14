Ext.ns('Ext.manage');
Ext.define('Ext.manage.module.AppOperateTree',{
	extend:'Ext.tree.Panel',
	alias: 'widget.appoperatetree',
	/**
	 * 面板标题
	 * @type String
	 */
	title:'应用操作树',
	/**
	 * 应用操作树的服务地址
	 * @type String
	 */
	operateServerUrl:getRootPath()+'/ConstructionService.svc/CS_RJ_GetBTDAppComponentsFrameTree',
	/**
	 * 树的子节点字段
	 * @private
	 * @type String
	 */
	childrenField:'Tree',
	/**
	 * 默认选中节点ID
	 * @type 
	 */
	selectId:null,
	/**
	 * 默认隐藏ID，隐藏该节点及所有子孙节点
	 * @type 
	 */
	hideNodeId:null,
	//=====================内部变量=======================
    useArrows:true,
    rootVisible:true,
    multiSelect:true,
    columnLines:true,
    /**
	 * 树字段对象
	 * @type 
	 */
    treeFields:[
		{name:'text',type:'auto'},//默认的现实字段
		{name:'expanded',type:'auto'},//是否默认展开
		{name:'leaf',type:'auto'},//是否叶子节点
		{name:'icon',type:'auto'},//图标
		{name:'url',type:'auto'},//地址
		{name:'tid',type:'auto'},//默认ID号
		{name:'value',type:'auto'},//模块对象
		{name:'objectType',type:'auto'}//节点类型
	],
    //=====================内部视图渲染=======================
	/**
	 * 初始化树
	 */
	initComponent:function(){
		var me = this;
		//数据集合
		me.store = me.createStore();
		//停靠
		me.dockedItems = me.createDockedItems();
		//注册事件
		me.addEvents('okClick');//确定按钮
		me.callParent(arguments);
	},
    /**
	 * 创建数据集
	 * @private
	 * @return {}
	 */
	createStore:function(){
		var me = this;
		var store = Ext.create('Ext.data.TreeStore',{
			fields:me.getFields(),
			proxy:{
				type:'ajax',
				url:me.operateServerUrl,
				extractResponseData:function(response){return me.changeStoreData(response);}
			},
			defaultRootProperty:me.childrenField,
			root:{
				text:'应用操作',
				leaf:false,
				expanded:true,
				id:0,
				CName:'应用操作'
			}
		});
		
		return store;
	},
	/**
	 * 创建停靠的功能
	 * @private
	 * @return {}
	 */
	createDockedItems:function(){
		var me = this;
		var dockedItems = [{
			xtype:'toolbar',
			dock:'bottom',
			items:[
				{text:'更新',tooltip:'刷新数据',iconCls:'build-button-refresh',handler:function(button){me.load();}},
				{text:'全部展开',tooltip:'展开全部节点',iconCls:'build-button-arrow-in',handler:function(button){me.expandAll();}},
				{text:'全部收缩',tooltip:'收缩一级节点',iconCls:'build-button-arrow-out',handler:function(button){me.collapseAll();me.getRootNode().expand();}},
				{text:'确定',tooltip:'确定',iconCls:'build-button-ok',handler:function(button){me.fireEvent('okClick');}}
			]
		}];
		return dockedItems;
	},
	//=====================设置获取参数=======================
	/**
	 * 获取fields
	 * @private
	 * @return {}
	 */
	getFields:function(){
		var me =this;
		var fields = me.treeFields;
		return fields;
	},
	//=====================公共方法代码=======================
	/**
	 * 数据适配
	 * @private
	 * @param {} response
	 * @return {}
	 */
	changeStoreData: function(response){
		var me = this;
    	var result = Ext.JSON.decode(response.responseText);
    	if(result.success){
			result[me.childrenField] = [];
			if(result.ResultDataValue && result.ResultDataValue != ''){
				var ResultDataValue = Ext.JSON.decode(result.ResultDataValue);
	    		result[me.childrenField] = ResultDataValue.Tree;
			}
	    	
	    	var changeNode = function(node){
	    		var value = node['value'];
	    		
	    		if(value.Id == me.hideNodeId){//需要剔除的节点
	    			return true;
	    		}
	    		for(var i in value){
	    			node[i] = value[i];
	    		}
	    		//时间处理
	    		node['DataAddTime'] = getMillisecondsFromStr(node['DataAddTime']);
	    		node['DataUpdateTime'] = getMillisecondsFromStr(node['DataUpdateTime']);
	    		//图片地址处理
	    		if(node['icon'] && node['icon'] != ""){
	    			node['icon'] = getIconRootPathBySize(16) + "/" + node['icon'];
	    		}
	    		
	    		var children = node[me.childrenField];
	    		if(children){
	    			changeChildren(children);
	    		}
	    		return false
	    	};
	    	
	    	var changeChildren = function(children){
	    		for(var i=0;i<children.length;i++){
	    			var bo = changeNode(children[i]);
	    			if(bo){
	    				children.splice(i,1);
	    				i--;
	    			}
	    		}
	    	};
	    	
	    	var children = result[me.childrenField];
	    	changeChildren(children);
    	}else{
    		Ext.Msg.alert('提示','错误信息【<b style="color:red">'+ result.ErrorInfo +'</b>】');
    	} 
    	response.responseText = Ext.JSON.encode(result);
    	return response;
  	},
  	//=====================对外公开方法=======================
    /**
     * 刷新树
     * @public
     */
    load:function(){
    	var me=this;
        var store=me.createStore();
        me.store=null;//预防数据频繁刷新,树的数据显示不同步
        me.store=store;
    }
});