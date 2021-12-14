/**
 * 功能菜单树
 * 【可配参数】
 * title 树标题，默认“功能菜单”
 * url 服务地址，默认getRootPath()+'/RBACService.svc/RBAC_UDTO_SearchRBACModuleToTree'
 * defaultRootProperty 子节点的属性名，默认“Tree”
 * root 根节点对象，默认
 * {
 *		text:'功能菜单',
 *		iconCls:'main-lefttree-root-img-16',
 *		leaf:false,
 *		expanded:true
 * }
 * fields 每行的数据字段，默认['text','url']
 * hasRefresh 是否需要更新按钮，默认true
 * extractResponseData 修改数据的方法，默认是function(response){return response;}
 * 
 * 【公开方法】
 * load() 更新功能菜单树数据
 */
Ext.ns('Ext.main');
Ext.define('Ext.main.MainTreePanel',{
	extend:'Ext.tree.Panel',
	alias:'widget.maintreepanel',
	//===================可配置参数====================
	/**
	 * 树标题
	 * @type String
	 */
	title:'功能菜单',
	/**
	 * 数据地址
	 * @type String
	 */
	url:getRootPath()+'/RBACService.svc/RBAC_UDTO_SearchModuleTreeBySessionHREmpID',
	//url:getRootPath()+'/RBACService.svc/RBAC_UDTO_SearchRBACModuleToTree',
	/**
	 * 子节点的属性名
	 * @type String
	 */
	defaultRootProperty:'Tree',
	/**
	 * 根节点对象
	 * @type 
	 */
	root:{
		text:'功能菜单',
		iconCls:'main-lefttree-root-img-16',
		leaf:false,
		expanded:false,
		id:0
	},
	/**
	 * 每行的数据字段
	 * @type 
	 */
	fields:['text','url'],
	/**
	 * 是否需要更新按钮
	 * @type Boolean
	 */
	hasRefresh:true,
	/**
	 * 是否以获取到数据
	 * @type 
	 */
	hasResponseData:false,
	//=====================内部变量=======================
	useArrows:true,
	rootVisible:true,//是否显示根节点
	containerScroll:true,//是否支持滚动条 
	autoScroll:false,//内容溢出的时候是否产生滚动条
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
			{name:'value',type:'auto'},//模块对象
			{name:'isComponent',type:'auto'}//是否是组件
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
	//=================================================
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		//展现数据
		me.getRootNode().expand();
	},
	/**
	 * 初始化配置
	 * @private
	 */
	initComponent:function(){
		var me = this;
		me.viewConfig = {
			loadMask:true,
			loadingText:'数据加载中...'
		};
		//me.header = {style:"background:#0C82B3;"};
		//me.bodyStyle = "background-color:#0C82B3;";
		//数据集
		me.store=me.createStore();
		var createDockedItems = me.createDockedItems();
		//停靠栏
		if(createDockedItems != null){
			me.createDockedItems = createDockedItems;
		}
		//面板功能栏
		me.tools = me.createTools();
		
		me.callParent(arguments);
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
		    tooltip:'刷新数据',
            itemId:'refresh',
		    handler:function(event,toolEl,owner,tool){
		    	me.load(tool);
		    }
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
	 * 停靠功能
	 * @private
	 */
	createDockedItems:function(){
		var me = this;
		var items = [];
		if(me.hasRefresh){
			items.push({
				text:'更新',iconCls:'build-button-refresh',handler:function(){me.load();}
			});
		}
		var dockedItems = null;
		if(items.length > 0){
			dockedItems = [{
				xtype:'toolbar',
				dock:'top',
				items:items
			}];
		}
		return dockedItems;
	},
	/**
	 * 创建数据集合
	 * @private
	 * @return {}
	 */
	createStore:function(){
		var me = this;
		var store = null;
		var url = me.url;
		if(url && url != ""){
			store = new Ext.data.TreeStore({
				fields:me.getTreeFields(),
				proxy: {
					type:'ajax',
					url:url,
					extractResponseData:function(response){return me.changeStoreData(response);}
			    },
			    defaultRootProperty:me.defaultRootProperty,
			    root:me.root,
			    autoLoad:false,
			    listeners:{
			    	load:function(store,node,records,successful,eOpts){
			    		var refresh = me.header.getComponent('refresh');
			    		refresh.enable();
			    	}
			    }
			});
		}
		return store;
	},
	//=====================设置获取参数=======================
	/**
	 * 获取列表的fields
	 * @private
	 * @return {}
	 */
	getTreeFields:function(){
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
	changeStoreData:function(response){
		var me = this;
    	var data = Ext.JSON.decode(response.responseText);
    	data[me.defaultRootProperty] = [];
		if(data.ResultDataValue && data.ResultDataValue != ""){
			var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
    		data[me.defaultRootProperty] = ResultDataValue ? ResultDataValue[me.defaultRootProperty] : [];
		}
    	
    	
    	var changeNode = function(node){
    		//图片地址处理
    		if(node['icon'] && node['icon'] != ""){
    			node['icon'] = getIconRootPathBySize(16) + "/" + node['icon'];
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
    	
    	response.responseText = Ext.JSON.encode(data);
    	//已获取到数据
    	me.hasResponseData = true;
    	
    	return response;
  	},
  	//=====================对外公开方法=======================
	/**
     * 刷新列表树
     * @public
     */
    load:function(){
    	var me = this;
    	var refresh = me.header.getComponent('refresh');
		refresh.disable();
        //加载数据
    	me.store.load();
    },
    /**
	 * 根据id找节点,如果存在返回节点,如果不存在返回null
	 * @public
	 * @param {} id
	 * @return {}
	 */
	findNodeById:function(id){
		var me = this;
		var root = me.getRootNode();
		var node = root.findChild('tid',id,true);
		return node;
	},
	/**
	 * 是否以获取到数据
	 * @public
	 * @return {}
	 */
	hasData:function(){
		var me = this;
		return me.hasResponseData;
	}
});