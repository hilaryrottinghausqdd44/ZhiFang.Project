/**
 * 树面板类
 * @author Jcall
 * @version 2014-09-02
 */
Ext.define('Shell.ux.panel.Tree',{
	extend:'Ext.tree.Panel',
	mixins:[
		'Shell.ux.server.Ajax',
		'Shell.ux.panel.Panel'
	],
	
	/**开启右键菜单*/
	hasContextMenu:false,
	/**默认加载数据*/
	defaultLoad:false,
	/**开启单元格内容提示*/
	tooltip:false,
	/**是否显示错误信息*/
	showErrorInfo:true,
	/**数据集属性*/
	storeConfig:{},
	
	/**主键列*/
	PKColumn:'Id',
	/**查询列*/
	searchFields:[],
	/**获取列表数据服务*/
	selectUrl:'',
	
	/**默认数据条件*/
	defaultWhere:'',
	/**内部数据条件*/
	internalWhere:'',
	/**外部数据条件*/
	externalWhere:'',
	
	/**视图面板属性*/
	viewConfig:{
        emptyText:'没有数据！',
        loadingText:'获取数据中，请等待...'
	},
	
	/**显示根节点*/
	rootVisible:true,
	/**子节点的属性名*/
	defaultRootProperty:'Tree',
	/**根节点*/
	root:{
		text:'根节点',
		leaf:false,
		expanded:true
	},
	/**需要的数据字段*/
	fields:[],
	
	/**重写渲染完毕执行*/
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		//开启右键快捷菜单设置
		me.onContextMenu();
		//视图准备完毕
		me.on({
			boxready:function(){me.boxIsReady();},
			expand:function(p,d){
				if(me.isCollapsed){me.load(null,true);}
				me.isCollapsed = false;
			}
		});
		if(me.defaultLoad){me.store.load();}
	},
	/**初始化面板属性*/
	initComponent:function(){
		var me = this;
		me.addEvents('contextmenu');
		me.store = me.createStore();
		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
	/**创建数据集*/
	createStore:function(){
		var me = this,
			config = {
				defaultRootProperty:me.defaultRootProperty,
				fields:me.getStoreFields(),
				autoLoad:false
			};
			
		if(me.selectUrl){
			config.proxy = {
				type:'ajax',
				url:Shell.util.Path.rootPath + me.selectUrl,
				extractResponseData:function(response){
					var result = Ext.JSON.decode(response.responseText),
						success = result.success;
					if(!success && me.showErrorInfo){me.showError(result.ErrorInfo);}
					return me.responseToTree(response);
				}
			};
		}else{
			config.root = Ext.clone(me.root);
			delete me.root;
		}
		
		return Ext.create('Ext.data.TreeStore',Ext.apply(config,me.storeConfig || {}));
	},
	/**获取数据字段*/
	getStoreFields:function(){
		var me = this,
			fields = me.fields;
			
		return fields;
	},
	/**创建挂靠*/
	createDockedItems:function(){
		var me = this,
			toolbars = me.toolbars || [],
			length = toolbars.length,
			dockedItems = [];
		
		for(var i=0;i<length;i++){
			dockedItems.push(Ext.apply({
				autoScroll:true,
				dock:'top',
				xtype:'uxbuttonstoolbar',
				listeners:{
					click:function(but,type){
						me.onButtonClick(but,type);
					},
					search:function(toolbar,search,value){
						me.searchValue = value;
						me.onSearch();
					}
				}
			},toolbars[i]));
		}
			
		return dockedItems;
	}
});