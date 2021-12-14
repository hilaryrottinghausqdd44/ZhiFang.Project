Ext.ns('Ext.manage');
Ext.define('Ext.manage.module.ModuleTree',{
	extend:'Ext.tree.Panel',
	alias: 'widget.moduletree',
	/**
	 * 面板标题
	 * @type String
	 */
	title:'模块管理',
	/**
	 * 模块列表树的服务地址
	 * @type String
	 */
	moduleServerUrl:getRootPath()+'/RBACService.svc/RBAC_UDTO_SearchRBACModuleToTree',
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
	/**
	 * 是否有确定功能按钮
	 * @type Boolean
	 */
	hasOkButton:true,
	//=====================内部变量=======================
    useArrows:true,
    rootVisible:true,
    multiSelect:true,
    columnLines:true,
    /***
     * 是否过滤子节点
     * @type Boolean
     */
    isFilterChildren:false,
    /**
	 * 树字段对象
	 * @type 
	 */
    treeFields:{
    	/**
		 * 基础字段数组
		 * @type 
		 */
		defaultFields:[
			{name:'text',type:'auto'},//默认的现实字段
			{name:'expanded',type:'auto'},//是否默认展开
			{name:'leaf',type:'auto'},//是否叶子节点
			{name:'icon',type:'auto'},//图标
			{name:'url',type:'auto'},//地址
			{name:'tid',type:'auto'},//默认ID号
			{name:'value',type:'auto'}//模块对象
		],
		/**
		 * 模块对象字段数组
		 * @type 
		 */
		moduleFields:[
			{name:'Id',type:'auto'},//模块ID
			{name:'LabID',type:'auto'},//实验室ID
			{name:'ParentID',type:'auto'},//树形结构父级ID
			{name:'LevelNum',type:'auto'},//树形结构层级
			{name:'TreeCatalog',type:'auto'},//树形结构层级Code
			{name:'IsLeaf',type:'auto'},//是否是叶节点
			{name:'ModuleType',type:'auto'},//模块类型
			{name:'PicFile',type:'auto'},//模块图形文件
			{name:'Url',type:'auto'},//模块入口地址
			{name:'Para',type:'auto'},//模块入口参数
			{name:'Owner',type:'auto'},//所有者
			{name:'UseCode',type:'auto'},//代码
			{name:'StandCode',type:'auto'},//标准代码
			{name:'DeveCode',type:'auto'},//开发商标准代码
			{name:'CName',type:'auto'},//名称
			{name:'EName',type:'auto'},//英文名称
			{name:'SName',type:'auto'},//简称
			{name:'Shortcode',type:'auto'},//快捷码
			{name:'PinYinZiTou',type:'auto'},//汉字拼音字头
			{name:'Comment',type:'auto'},//描述
			{name:'IsUse',type:'auto'},//是否使用
			{name:'DispOrder',type:'auto'},//显示次序
			{name:'DataAddTime',type:'auto'},//数据加入时间
			{name:'DataUpdateTime',type:'auto'},//数据更新时间
			{name:'PrimaryKey',type:'auto'},//PrimaryKey
			{name:'DataTimeStamp',type:'auto'}//时间戳
		]
    },
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
		//面板功能栏
		me.tools = me.createTools();
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
			fields:me.getGridFields(),
			proxy:{
				type:'ajax',
				url:me.moduleServerUrl,
				extractResponseData:function(response){return me.changeStoreData(response);}
			},
			defaultRootProperty:me.childrenField,
			root:{
				text:'所有模块',
				leaf:false,
				expanded:true,
				Id:0,
				tid:0,
				CName:'所有模块'
			},
			listeners:{
				load:function(store,node,records,successful,e){
					if(successful){
						me.selectNode();
					}
					me.disableRefresh(false);
				}
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
		
		var items = ['->',{text:'更新',itemId:'refresh',iconCls:'build-button-refresh',handler:function(button){me.load();}}];
		if(me.hasOkButton){
			items.push({text:'确定',iconCls:'build-button-ok',handler:function(button){me.fireEvent('okClick');}});
		}
		
		var dockedItems = [{
			xtype:'toolbar',
			itemId:'bottom',
			dock:'bottom',
			items:items
		}];
		
		return dockedItems;
	},
	/**
	 * 创建面板功能栏
	 * @private
	 * @return {}
	 */
	createTools:function(){
		var me = this;
		var com = [{
			type:'refresh',
			itemId:'refresh',
		    tooltip:'刷新数据',
		    handler:function(){me.load();}
		},{
			type:'plus',
		    tooltip:'展开全部节点',
		    handler:function(){me.expandAll();}
		},{
			type:'minus',
		    tooltip:'收缩全部节点',
		    handler:function(){me.collapseAll();me.getRootNode().expand();}
		}];
		return com;
	},
	/**
	 * 选中默认的一行数据
	 * @private
	 */
	selectNode:function(){
		var me = this;
		var root = me.getRootNode();
		var node = null;
		if(me.selectId == "0"){
			node = root;
		}else{
			node = me.getRootNode().findChild('tid',me.selectId,true);
		}
		//打开节点的所有上级节点
		var openParentNode = function(node){
			var parentNode = node.parentNode;
			if(parentNode){
				parentNode.expand();
				openParentNode(parentNode);
			}else{
				return;
			}
		}
		if(node){
			me.getSelectionModel().select(node);
			openParentNode(node);
		}
	},
	//=====================设置获取参数=======================
	/**
	 * 获取列表的fields
	 * @private
	 * @return {}
	 */
	getGridFields:function(){
		var me =this;
		var treeFields = me.treeFields;
		
		var defaultFields = treeFields.defaultFields;
		var moduleFields = treeFields.moduleFields;
		
		var fields = defaultFields.concat(moduleFields);
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
    	var data = Ext.JSON.decode(response.responseText);

    	var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
    	data[me.childrenField] = ResultDataValue.Tree;
    	var changeNode = function(node){
    		if(node.tid == me.hideNodeId){//需要剔除的节点
    			return true;
    		}
            if(me.isFilterChildren==true){
	            if(node.leaf == true){//所有子节点都需要剔除
	                return true;
	            }
	            if(node.url!=null&&node.url.toString().length>0){//所有有访问地址不为空的也都需要剔除
	                return true;
	            }
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
    		
    	
    	var children = data[me.childrenField];
    	changeChildren(children);
    	
    	response.responseText = Ext.JSON.encode(data);
    	return response;
  	},
  	/**
  	 * 是否禁用刷新按钮
  	 * @private
  	 * @param {} bo
  	 */
  	disableRefresh:function(bo){
  		var me = this;
  		var refresh = me.header.getComponent('refresh');
		var refresh2 = me.getComponent('bottom').getComponent('refresh');
		
		if(bo){
			refresh.disable();
			refresh2.disable();
		}else{
			refresh.enable();
			refresh2.enable();
		}
  	},
  	//=====================对外公开方法=======================
    /**
     * 刷新树
     * @public
     */
    load:function(){
    	var me=this;
        //禁用刷新按钮
       	me.disableRefresh(true);
        //加载数据
    	me.store.load();
    }
});