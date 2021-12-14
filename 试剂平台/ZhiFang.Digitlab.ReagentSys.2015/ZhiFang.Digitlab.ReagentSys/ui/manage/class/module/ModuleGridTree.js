Ext.ns('Ext.manage');
Ext.define('Ext.manage.module.ModuleGridTree',{
	extend:'Ext.tree.Panel',
	alias: 'widget.modulegridtree',
	//=====================可配参数=======================
	/**
	 * 面板标题
	 * @type String
	 */
	title:'模块管理',
	emptyText:'没有数据',
	/**
	 * 模块列表树的服务地址
	 * @type String
	 */
	moduleServerUrl:getRootPath()+'/RBACService.svc/RBAC_UDTO_SearchRBACModuleToListTree',
	/**
	 * 删除模型的服务地址
	 * @type String
	 */
	deleteModuleServerUrl:getRootPath()+'/RBACService.svc/RBAC_UDTO_DelRBACModule',
	/**
	 * 树的子节点字段
	 * @private
	 * @type String
	 */
	childrenField:'Tree',
	//=====================模块面板服务配置=======================
	/**
	 * 获取应用列表服务地址
	 * @type String
	 */
	getAppListServerUrl:getRootPath()+'/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsByHQL',
	/**
	 * 上传图片文件服务地址
	 * @type 
	 */
	updateFileServerUrl:getRootPath()+'/ConstructionService.svc/ReceiveModuleIconService',
	//=====================内部变量=======================
    useArrows:true,
    rootVisible:false,
    multiSelect:true,
    columnLines:true,
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
	 * 初始化列表树
	 */
	initComponent:function(){
		var me = this;
		
		Ext.Loader.setPath('Ext.manage',getRootPath()+'/ui/manage/class');
		Ext.Loader.setPath('Ext.build',getRootPath()+'/ui/build/class');
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing',{clicksToEdit:1});
		//数据集合
		me.store = me.createStore();
		//列表字段
		me.columns = me.createColumns();
		//停靠
		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
	//=====================创建内部元素=======================
	/**
	 * 列表字段
	 * @private
	 * @return {}
	 */
	createColumns:function(){
		var me = this;
		var columns = [{
			xtype:'treecolumn',text:'模块名称',
            width:200,sortable:true,
            dataIndex:'CName',editor:{readOnly:true}
		},{
			text:'模块ID',dataIndex:'Id',width:150,hidden:true,editor:{readOnly:true}
		},{
			text:'入口地址',dataIndex:'Url',width:300,editor:{readOnly:true}
		},{
			text:'时间戳',dataIndex:'DataTimeStamp',hidden:true
		},{
			xtype:'actioncolumn',text:'操作',width:80,align:'center',
			items:[{
				iconCls:'build-button-add hand',tooltip:'新增子模块',
                handler:function(grid,rowIndex,colIndex,item,e,record){
                	record.set('ParentID',record.get('Id'));
                	record.set('ParentName',record.get('CName'));
                	me.itemButtonClick("add",record);
                }
			},{
				iconCls:'build-button-edit hand',tooltip:'修改模块信息',
                handler:function(grid,rowIndex,colIndex,item,e,record){
                	var parent = record.parentNode;
                	record.set('ParentID',parent.get('Id'));
                	record.set('ParentName',parent.get('CName'));
                	me.itemButtonClick("edit",record);
                }
			},{
				iconCls:'build-button-see hand',tooltip:'查看模块信息',
                handler:function(grid,rowIndex,colIndex,item,e,record){
                	me.itemButtonClick("show",record);
                }
			},{
				iconCls:'build-button-delete hand',tooltip:'删除模块信息',
                handler:function(grid,rowIndex,colIndex,item,e,record){
                	me.deleteModule(record);
                }
			}]
		}];
		
		return columns;
	},
	/**
	 * 创建数据集
	 * @private
	 * @return {}
	 */
	createStore:function(){
		var me = this;
		var fields = "RBACModule_Id,RBACModule_CName,RBACModule_Url,RBACModule_DataTimeStamp";
		var store = Ext.create('Ext.data.TreeStore',{
			fields:me.getGridFields(),
			proxy:{
				type:'ajax',
				url:me.moduleServerUrl+"?fields="+fields,
				extractResponseData:function(response){return me.changeStoreData(response);}
			},
			defaultRootProperty:me.childrenField,
			root:{
				text:'所有模块',
				leaf:false,
				expanded:true,
				id:0,
				CName:'所有模块'
			},
			listeners:{
			    	load:function(com,records,successful,eOpts){
			    		var toolbar = me.getComponent('toptoolbar');
    					var refresh = toolbar.getComponent('refresh');
			    		refresh.enable();
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
		var dockedItems = [{
			xtype:'toolbar',
			itemId:'toptoolbar',
			dock:'top',
			items:[
				{text:'更新',itemId:'refresh',tooltip:'刷新数据',iconCls:'build-button-refresh',handler:function(button){me.load();}},
				{text:'新增一级模块',tooltip:'新增一级模块',iconCls:'build-button-add',handler:function(button){me.dockItemsAdd();}},
				{text:'全部展开',tooltip:'展开全部节点',iconCls:'build-button-arrow-in',handler:function(button){me.expandAll();}},
				{text:'全部收缩',tooltip:'收缩一级节点',iconCls:'build-button-arrow-out',handler:function(button){me.collapseAll();me.getRootNode().expand();}}
			]
		}];
		
		return dockedItems;
	},
	//=====================事件处理=======================
	/**
	 * 删除模块
	 * @private
	 */
	deleteModule:function(record){
		var me = this;
		Ext.Msg.confirm("警告","确定要删除吗？",function (button){
			if(button == "yes"){
				var id = record.get('Id');
				//删除后的处理
				var callback = function(){
					var node = me.getRootNode().findChild("Id",id,true);
		    		node.remove();
				}
				if(record.get('Url') == "../../manage/file/modulemanage.html"){
					alertError('不能删除此功能！');
				}else{
					me.deleteModuleServer(id,callback);
				}
			}
		});
	},
	/**
	 * 数据行上的功能按钮点击事件处理
	 * @private
	 * @param {} type
	 * @param {} record
	 */
	itemButtonClick:function(type,record){
		var me = this;
		
		var obj = {
			Id:record.get('Id'),//模块ID
			LevelNum:parseInt(record.get('LevelNum'))+1,//树形结构层级
			TreeCatalog:parseInt(record.get('TreeCatalog'))+1,//树形结构层级Code
			ParentID:record.get('ParentID'),//树形结构父级ID
			ParentName:record.get('ParentName')
		};
		//弹出模块页面
		me.openModuleEditWin(type,obj);
	},
	/**
	 * 新增以及模块按钮处理
	 * @private
	 */
	dockItemsAdd:function(){
		var me = this;
		
		var obj = {
			Id:-1,
			ParentID:0,
			LevelNum:1,
			TreeCatalog:1,
			ParentName:'所有模块'
		};
		//弹出模块页面
		me.openModuleEditWin("add",obj);
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
	//=====================功能页面=======================
	/**
	 * 弹出模块页面
	 * @private
	 * @param {} type
	 * @param {} id
	 */
	openModuleEditWin:function(type,obj){
    	var me = this;
    	var title = "";
    	
    	var Id = -1;
    	var ParentID = obj.ParentID;
    	var LevelNum = obj.LevelNum;
    	var TreeCatalog = obj.TreeCatalog;
    	var ParentName = obj.ParentName;
    	
    	if(type == "add"){
    		title = "新增模块";
    	}else if(type == "edit"){
    		title = "修改模块";
    		Id = obj.Id;
    	}else if(type == "show"){
    		title = "查看模块";
    		Id = obj.Id;
    	}
    	
    	var modelForm = Ext.create('Ext.manage.module.ModuleForm',{
    		modal:true,//模态
    		floating:true,//漂浮
			closable:true,//有关闭按钮
			draggable:true,//可移动
			
    		isWindow:true,//窗口打开
    		
    		getAppListServerUrl:me.getAppListServerUrl,
    		updateFileServerUrl:me.updateFileServerUrl,
    		
    		title:title,
    		type:type,
    		Id:Id,
    		ParentID:ParentID,
    		LevelNum:LevelNum,
    		TreeCatalog:TreeCatalog,
    		ParentName:ParentName
    	}).show();
    	
    	modelForm.on({
    		saveClick:function(){modelForm.close();me.load();},
    		saveAsClick:function(){modelForm.close();me.load();}
    	});
	},
	
	//=====================后台获取&存储=======================
	/**
	 * 从数据库中删除记录
	 * @private
	 * @param {} id
	 */
	deleteModuleServer:function(id,callback){
		var me = this;
    	var url = me.deleteModuleServerUrl + "?id=" + id;
    	
    	Ext.Ajax.request({
			async:false,//非异步
			url:url,
			method:'GET',
			timeout:2000,
			success:function(response,opts){
				var result = Ext.JSON.decode(response.responseText);
				if(result.success){
					callback();
				}else{
					alertError("删除模块失败！错误信息:"+ result.ErrorInfo);
				}
			},
			failure:function(response,options){ 
				alertError("连接删除模块服务出错！");
			}
		});
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
	    		for(var i in value){
	    			node[i] = value[i];
	    		}
	    		//图片地址处理
	    		if(node['icon'] && node['icon'] != ""){
	    			node['icon'] = getIconRootPathBySize(16) + "/" + node['icon'];
	    		}
	    		
	    		var children = node[me.childrenField];
	    		if(children){
	    			changeChildren(children);
	    		}
	    	};
	    	
	    	var changeChildren = function(children){
	    		Ext.Array.each(children,changeNode);
	    	};
	    	
	    	var children = result[me.childrenField];
	    	changeChildren(children);
    	}else{
    		alertError('错误信息:'+ result.ErrorInfo);
    	}
    	response.responseText = Ext.JSON.encode(result);
    	return response;
  	},
	//=====================对外公开方法=======================
    /**
     * 刷新列表树
     * @public
     */
    load:function(){
    	var me=this;
    	var toolbar = me.getComponent('toptoolbar');
    	var refresh = toolbar.getComponent('refresh');
    	refresh.disable();
        me.store.load();
    }
});