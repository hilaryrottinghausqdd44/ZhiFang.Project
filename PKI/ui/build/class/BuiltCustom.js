/**
 * 定制构建维护
 */
Ext.ns('Ext.build');
Ext.define('Ext.build.BuiltCustom',{
	extend:'Ext.grid.Panel',
	alias:'widget.builtcustom',
	title:'定制构建维护',
    appType:100,//数据行设置的应用类型
	/**
	 * 需要过滤掉的功能编号
	 * @type 
	 */
	filterId:-1,
	/**
	 * 每页数量
	 * @type Number
	 */
	pageSize:25,
	/**
	 * 只读列表,没有任何按钮
	 * @type Boolean
	 */
	readOnly:false,
	/**
	 * 是否默认加载数据
	 * @type Boolean
	 */
	defaultLoad:true,
	/**
	 * 是否默认有修改功能按钮
	 * @type Boolean
	 */
	hasEditButton:true,
	/**
	 * 获取应用列表的服务地址
	 * @type String
	 */
	getAppListServerUrl:getRootPath()+'/ConstructionService.svc/CS_UDTO_SearchRefBTDAppComponentsByHQLAndId',

	/**
	 * 根据ID获取一条应用信息的服务地址
	 * @type 
	 */
	getAppInfoServerUrl:getRootPath()+'',
	/**
	 * 删除应用信息的服务地址
	 * @type 
	 */
	deleteAppServerUrl:getRootPath()+'',
	/**
	 * 默认的排序
	 * @type String
	 */
 	/**
	 * 是否开启远程排序
	 * @type Boolean
	 */
	remoteSort:false,
	/**
	 * 数据根节点
	 * @type String
	 */
	dataRoot:'ResultDataValue',
	/**
	 * 应用字段对象
	 * @type 
	 */
	fieldsObj:{
		AppComID:'AppComID',
		/**
		 * 中文名称
		 * @type String
		 */
		CName:'CName',
		/**
		 * 英文名称
		 * @type String
		 */
		EName:'EName',
		/**
		 * 描述
		 * @type String
		 */
		MComment:'Comment'
		
	},
	/**
	 * 默认打开窗口宽高百分比
	 * @type String
	 */
	defaultWHPercent:'95%',
	/**
	 * 内部hql
	 * @type String
	 */
	internalWhere:'',
	/**
	 * 外部hql
	 * @type String
	 */
	externalWhere:'',

	/**
	 * 初始化应用列表组件
	 * @private
	 */
	initComponent:function(){
		var me = this;
		
		//初始化视图
		me.initView();
		//监听
		me.initListeners();
		//注册事件
		me.addEvents('okClick');//确定按钮
		
		me.callParent(arguments);
	},
	/**
	 * 初始化视图
	 * @private
	 */
	initView:function(){
		var me = this;
		//创建数据集
		var store = me.createStore();
		me.store = store;
		//创建列表对象
		me.createList();
	},
	/**
	 * 列表监听
	 * @private
	 */
	initListeners:function(){
		var me = this;
		
		if(!me.readOnly){
			me.listeners = {
				itemdblclick:function(com,record,item,index,e,eOpts){}
			};
		}
	},
	/**
	 * 渲染完后执行
	 * @private
	 */
	afterRender:function(){
        var me = this;
		me.callParent(arguments);
		//是否加载数据
		if(me.defaultLoad){
			me.load(me.externalWhere);
		}
	},
	/**
	 * 创建数据集
	 * @private
	 * @return {}
	 */
	createStore:function(){
		var me = this;
		var store = Ext.create('Ext.data.Store',{
			fields:me.getFields(),
            data : [
	         {AppComID: '100',CName: '已录入项目',EName:''},
	         {AppComID: '101',CName: '检验项目分类',EName:'inputProject'},
	         {AppComID: '102',CName: '模板录入',EName:'inspectingItem'},
             {AppComID: '103',CName: '添加部门员工查询条件',EName:'adddeptstaff'},
             {AppComID: '104',CName: '申请帐号',EName:''},
             {AppComID: '105',CName: '帐号更新',EName:''},
             {AppComID: '106',CName: '员工帐号(员工维护)',EName:''}
	        ]
		});
		return store;
	},
	/**
	 * 创建列表对象
	 * @private
	 */
	createList:function(){
		var me = this;
		me.viewConfig = {
	        emptyText:'没有数据！',
	        loadingText:'获取数据中，请等待...'
		};
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing',{clicksToEdit:1});
		me.columns = [
        {
			dataIndex:me.fieldsObj.AppComID,text:'类型编码 ',width:150
		},{
			dataIndex:me.fieldsObj.CName,text:'中文名称',width:150
		}];
		me.dockedItems = me.createDockedItems();

		if(!me.readOnly && me.hasEditButton){
			me.columns.push({
				xtype:'actioncolumn',text:'操作',width:60,align:'center',
				items:[{
					iconCls:'build-button-add',
	                tooltip:'新增元应用',
	                handler:function(grid,rowIndex,colIndex,item,e,record){
	                	me.editApp(record);
	                }
				}]
			});
		}
	},
	/**
	 * 创建挂靠对象
	 * @private
	 * @return {}
	 */
	createDockedItems:function(){
		var me = this;
		
		var toolbar ={
			xtype:'toolbar',
			itemId:'toolbar-top',
    		items:[]
		};
		
		//新增
        if(!me.readOnly){
	        toolbar.items.push({
	        	xtype:'button',text:'新增',iconCls:'build-button-add',
	        	tooltip:'按照选中的构建类型进行新增操作',hidden:true,
	        	//menu:menu
                handler:function(button){me.openAppEditWin('');}
	        });
        }
		
		
        //查询
        toolbar.items.push('->');
       
        //模糊查询框
        toolbar.items.push({
        	xtype:'textfield',itemId:'searchText',width:160,
        	emptyText:'功能编号、中文名称',hidden:true,
        	listeners:{
        		render:function(input){
			    	new Ext.KeyMap(input.getEl(),[{
				      	key:Ext.EventObject.ENTER,
				      	fn:function(){me.search();}
			     	}]);
			    }
        	}
        });
        toolbar.items.push({
        	xtype:'button',text:'查询',iconCls:'search-img-16 ',
        	tooltip:'按照中文名称进行查询',hidden:true,
        	handler:function(button){me.search();}
        });
        
		var pagingtoolbar = {//分页栏
			xtype:'pagingtoolbar',
			store:me.store,
			dock:'bottom',
			displayInfo:true
		};
		
		var dockedItems = [toolbar,pagingtoolbar];
		return dockedItems;
	},

    //=====================与后台交互=======================
    /**
     * 根据ID获取一条应用信息
     * @private
     * @param {} id
     * @param {} callback
     */
    getInfoByIdFormServer:function(id,callback){
   		var me = this;
   		var url = me.getAppInfoServerUrl+'?isPlanish=true&id='+id;
   		Ext.Ajax.defaultPostHeader = 'application/json';
   		Ext.Ajax.request({
			async:false,//非异步
			url:url,
			method:'GET',
			timeout:2000,
			success:function(response,opts){
				var result = Ext.JSON.decode(response.responseText);
				if(result.success){
					var appInfo = "";
					if(result.ResultDataValue && result.ResultDataValue != ""){
						result.ResultDataValue =result.ResultDataValue.replace(/\n/g,"\\u000a");
						appInfo = Ext.JSON.decode(result.ResultDataValue);
					}
		    		if(Ext.typeOf(callback) == "function"){
						callback(appInfo);//回调函数
					}
				}else{
					Ext.Msg.alert('提示','获取应用信息失败！');
				}
			},
			failure:function(response,options){ 
				Ext.Msg.alert('提示','获取应用信息请求失败！');
			}
		});
   	},
    //=====================弹出功能窗口=======================
    /**
	 * 打开应用效果窗口
	 * @private
	 * @param {} title
	 * @param {} ClassCode
	 * @param {} id
	 */
	openAppShowWin:function(title,ClassCode,id){
		var me = this;
		var panel = eval(ClassCode);
		var maxHeight = document.body.clientHeight*0.98;
		var maxWidth = document.body.clientWidth*0.98;
		var win = Ext.create(panel,{
			id:id,
			maxWidth:maxWidth,
			autoScroll:true,
    		modal:false,//模态
    		floating:true,//漂浮
			closable:true,//有关闭按钮
			resizable:true,//可变大小
			draggable:true//可移动
    	});
    	
		if(win.height > maxHeight){
			win.height = maxHeight;
		}
    	//解决chrome浏览器的滚动条问题
    	var callback = function(){
    		win.hide();
    		win.show();
    	}
    	win.show(null,callback);
	},
	/**
     * 打开应用设置页面
     * @private
     * @param {} 
     * @param {} id
     */
    openAppEditWin:function(id){
    	var me = this;
    	var title = '';
    	var panel ='';
        var typeId=id;
        if(typeId=='100'){
            panel = 'Ext.build.BasicInputProject';//已录入项目维护构建工具
            me.appId=100;
            title = '已录入项目(定制构建)';
        }else if(typeId=='101'){
            panel = 'Ext.build.BasicTestItemsClassified';//检验项目分类
            me.appId=101;
            title = '检验项目分类(定制构建)';
        }else if(typeId=='102'){
            me.appId=102;
            panel = 'Ext.build.BasicInspectingItem';//模板录入
            title = '模板录入(定制构建)';
        }
        if(title && title != ""){
        	var appId = -1;
	        title = "新增"+title;
	    	
	    	var win = Ext.create(panel,{
	    		title:title,
	    		width:'98%',
	    		height:'98%',
	    		appId:appId,
	    		appType:me.appId,
	    		modal:true,//模态
	    		resizable:true,//可变大小
	    		floating:true,//漂浮
				closable:true,//有关闭按钮
				draggable:true,//可移动
				tools:[{
					type:'maximize',
					itemId:'maximize',
					tooltip:'最大化展示区域',
					handler:function(event,target,owner,tool){
						tool.hide();
						win.getComponent('east').hide();
						win.getComponent('south').hide();
						setTimeout(function(){owner.getComponent('minimize').show();},100);
					}
				},{
					type:'minimize',
					itemId:'minimize',
					tooltip:'恢复展示区域大小',
					hidden:true,
					handler:function(event,target,owner,tool){
						tool.hide();
						win.getComponent('east').show();
						win.getComponent('south').show();
						setTimeout(function(){owner.getComponent('maximize').show();},100);
					}
				}]
	    	}).show();
	    	//保存监听
			win.on({
				saveClick:function(){
					win.close();
					//me.load(me.externalWhere);
				}
			});
        }else{
    		Ext.Msg.alert('提示','选择的构建类型不存在！');
        }
    },
    /**
	 * 修改元应用
	 * @private
	 * @param {} grid
	 * @param {} rowIndex
	 * @param {} colIndex
	 * @param {} item
	 * @param {} e
	 * @param {} record
	 */
	editApp:function(record){
			var me = this;
	        var id = record.get(me.fieldsObj.AppComID);
	        me.openAppEditWin(id);
    },
	//=====================内部方法=======================
    /**
     * 列表中的数据列
     * @private
     * @return {}
     */
    getFields:function(){
    	var me = this;
    	var fields = [
			{name:me.fieldsObj.AppComID,type:'string'},//应用组件ID
			{name:me.fieldsObj.CName,type:'string'},//中文名称
            {name:me.fieldsObj.EName,type:'string'},//英文名称
			{name:me.fieldsObj.MComment,type:'string'}//应用类型

		];
		return fields;
    },
    /**
     * 获取需要的数据列Str
     * @private
     */
    getFieldsStr:function(){
    	var me = this;
    	var fieldsStr = "";
    	var fields = me.getFields();
    	for(var i in fields){
    		if(fields[i].name != "hasBeenDeleted"){
    			fieldsStr += fields[i].name + ",";
    		}
    	}
    	if(fieldsStr != ""){
    		fieldsStr = fieldsStr.substring(0,fieldsStr.length-1);
    	}
    	return fieldsStr;
    },
    /**
     * 检索
     * @private
     */
    search:function(){
    	var me = this;
    	var toolbar = me.getComponent('toolbar-top');
		var appType = toolbar.getComponent('searchAppType').getValue();
		var value = toolbar.getComponent('searchText').getValue();
		
		var buildType = toolbar.getComponent('searchBuildType').getValue();
		
		var where = "";
		if(buildType != -1){
			where += "btdappcomponents.BuildType=" + buildType;
		}
		if(appType > 0){
			where += where == "" ? "" : " and ";
			where += "btdappcomponents.AppType=" + appType;
		}
		if(value && value != ""){
			where += where == "" ? "" : " and ";
			where += "(btdappcomponents.CName like '%25" + value + "%25' or btdappcomponents.ModuleOperCode like '%25" + value + "%25')";
		}
		
		me.store.currentPage = 1;
		
		me.load(where);
    },
    /**
     * 获取默认的路径
     * @private
     */
    getDefaultUrl:function(){
    	var me = this;
    	var url = me.getAppListServerUrl + "?isPlanish=true&fields=" + me.getFieldsStr();
		if(me.filterId && me.filterId != "" && me.filterId != -1){
			url += "&AppId=" + me.filterId;
		}
		return url;
    },
 
    /**
     * 获取带查询参数的URL
     * @private
     * @return {}
     */
    getLoadUrl:function(){
    	var me = this;
    	var url = me.getDefaultUrl();
		if(me.remoteSort && me.sort && me.sort != ""){
			url += "&sort=" + me.sort;
		}
    	
    	var w="";
		if(me.internalWhere){
			w += me.internalWhere;
		}
		if(me.externalWhere && me.externalWhere != ""){
			if(w != ""){
				w += " and " + me.externalWhere;
			}else{
				w += me.externalWhere;
			}
		} 
		
		return (url + "&where=" + w);
    },
    //=====================对外公开方法=======================
    /**
     * 刷新列表
     * @public
     */
    load:function(where){
    	var me = this;
    	me.externalWhere = where;
    	me.store.proxy.url = me.getDefaultUrl();
		me.store.proxy.url = me.getLoadUrl();
    	me.store.load();
    },
    /**
     * 获取选中的值
     * @publics
     * @return {}
     */
    getSelectModel:function(){
    	var me = this;
    	var models = me.getSelectionModel().getSelection();
    	return models;
    }
});
