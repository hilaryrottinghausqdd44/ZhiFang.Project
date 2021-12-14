/**
 * 查看应用
 */
Ext.ns('Ext.build');
Ext.define('Ext.build.changkanziyingyong', {
    extend:'Ext.tree.Panel',
    alias:'widget.changkanziyingyong',
    title:'',
    hideNodeId:null,
    type:1,
    columnsStr:[ {
        xtype:'treecolumn',
        text:'中文名称',
        width:200,
        sortable:true,
        dataIndex:'text',
        triStateSort:false
    },{
        text:'应用ID',
        dataIndex:'Id',
        width:150,
        sortable:false,
        hidden:false,
        hideable:true,
        editor:{readOnly:true},
        align:'center'
    },{
        text:'功能编码',
        dataIndex:'ModuleOperCode',
        width:140,
        sortable:false,
        editor:{readOnly:true},
        hidden:false,
        hideable:true,
        align:'center'
    },{
        text:'应用类型',
        dataIndex:'AppType',
        width:80,
        sortable:false,
        hidden:false,
        hideable:true,
        align:'center',
        renderer:function(value, p, record){
			if(value == 1){
				return Ext.String.format("列表");
			}else if(value == 2){
				return Ext.String.format("表单");
			}else if(value == 3){
				return Ext.String.format("应用");
			}
	        else if(value == 4){
	            return Ext.String.format("列表树");
	        }
	        else if(value == 5){
	            return Ext.String.format("图表");
	        }else if(value == 6){
	            return Ext.String.format("高级查询(列表)");
	        }else if(value == 7){
	            return Ext.String.format("高级查询(表单)");
	        }
	        else if(value == 8){
	            return Ext.String.format("分组查询");
	        }
	        else if (value==9){
	            return Ext.String.format("选择器(单/复选组)");
	        }
	        else if(value == 10){
	            return Ext.String.format("单列树");
	        }//
	       else if(value == 11){
	            return Ext.String.format("普通排序");
	        }//
	        else if(value == 12){
	            return Ext.String.format("双表数据移动");
	        }//(定制)
	        else if(value == 100){
	            return Ext.String.format("已录入项目");
	        }//(定制)
	        else if(value == 101){
	            return Ext.String.format("检验项目分类");
	        }//(定制)
	        else if(value == 102){
	            return Ext.String.format("模板录入");
	        }//(定制)
	        else if(value == 103){
	            return Ext.String.format("添加部门员工查询条件");
	        }//(定制)
	        else if(value == 104){
	            return Ext.String.format("申请帐号");
	        }//(定制)
	        else if(value == 105){
	            return Ext.String.format("帐号更新");
	        }//(定制)
	        else if(value == 106){
	            return Ext.String.format("员工帐号(员工维护)");
	        }//(定制)
	        else{
				//return Ext.String.format(value);
	        	return value;
			}
		}
        
    },{
        text:'创建时间',
        dataIndex:'DataAddTime',
        width:100,
        align:'center'
    },{
        dataIndex:'hasBeenDeleted',
        text:'删除标记',
        width:60,
        triStateSort:false,
        hidden:true,
        sortable:true
    },{
        xtype:'actioncolumn',
        text:'操作列',
        width:100,
        align:'center',
        itemId:'Action'
    }],
    getAppInfoServerUrl:getRootPath() + '/' + 'ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById',
    getAppListServerUrl:getRootPath() + '/' + 'ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsByHQL',
    updateFileServerUrl:getRootPath() + '/' + 'ConstructionService.svc/ReceiveModuleIconService',
    editDataServerUrl:getRootPath() + '/' + 'ConstructionService.svc/CS_UDTO_UpdateBTDAppComponentsByField',
    whereFields:'BTDAppComponents_ModuleOperCode,BTDAppComponents_AppType,BTDAppComponents_Id,BTDAppComponents_DataAddTime,BTDAppComponents_ClassCode',
    autoScroll:true,
    childrenField:'Tree',
    viewConfig:{
		emptyText:'没有数据！',
		loadingText:'数据加载中...',
		loadMask:true
	},
    getValue:function() {
        var myTree = this;
        var arrTemp = myTree.getSelectionModel().getSelection();
        return arrTemp;
    },
    internalWhere:'',
    externalWhere:'',
    afterRender:function() {
        var me = this;
        me.callParent(arguments);
        if (Ext.typeOf(me.callback) == 'function') {
            me.callback(me);
        }
    },
    ClassCode:'BTDAppComponents_ClassCode',
    width:800,
    height:300,
    lines:false,
    useArrows:true,
    rootVisible:false,
    isShowAction:true,
    isShowCeneral:true,
    isShowDeleteAlag:true,
    isShowBtn:true,
    isEditBtn:true,
    createStore:function(where) {
        var me = this;
        if(me.type==1){
            var  selectServerUrl=getRootPath() + '/' + 'ConstructionService.svc/CS_RJ_GetBTDAppComponentsFrameListTree';
        }
        if(me.type==0){
            var  selectServerUrl=getRootPath() + '/' + 'ConstructionService.svc/CS_RJ_GetBTDAppComponentsRefTree';
        }
        var w = '?fields=BTDAppComponents_ModuleOperCode,BTDAppComponents_AppType,BTDAppComponents_Id,BTDAppComponents_DataAddTime,BTDAppComponents_ClassCode';
        var myUrl = selectServerUrl + w;
        var store = Ext.create('Ext.data.TreeStore', {
            fields:me.createfield(),
            proxy:{
                type:'ajax',
                url:myUrl,
                extractResponseData:function(response) {
                    return me.changeStoreData(response);
                }
            },
            defaultRootProperty:me.childrenField,
            listeners:{
                load:function(treeStore, node, records, successful, eOpts) {
                    var treeToolbar = me.getComponent('treeToolbar');
                    if (treeToolbar == undefined || treeToolbar == '') {
                        treeToolbar = me.getComponent('treeToolbarTwo');
                    }
                    if (treeToolbar && treeToolbar != undefined) {
                        var refresh = treeToolbar.getComponent('refresh');
                        if (refresh && refresh != undefined) {
                            refresh.disabled = false;
                        }
                    }
                }
            }
        });
        return store;
    },
    createfield:function(){
    	var me=this;
    	var com=[ 
          {name:'text',type:'auto'}, 
          {name:'expanded',type:'auto'},
          {name:'leaf',type:'auto'}, 
          {name:'icon',type:'auto'}, 
          {name:'url',type:'auto'}, 
          {name:'tid',type:'auto'}, 
          {name:'Id',type:'auto'}, 
          {name:'ParentID',type:'auto'},
          {name:'hasBeenDeleted',type:'auto'}, 
          {name:'value', type:'auto'},
          {name:'ModuleOperCode',type:'auto'},
          {name:'AppType',type:'auto'}, 
          {name:'Id',type:'auto'}, 
          {name:'DataAddTime',type:'auto'},
          {name:'ModuleOperCode',type:'auto'}, 
          {name:'AppType', type:'auto'}, 
          {name:'Id',type:'auto'}, 
          {name:'DataAddTime',type:'auto'} 
        ];
    	return com;
    },
    changeStoreData:function(response) {
        var me = this;
        var data = Ext.JSON.decode(response.responseText);
        var ResultDataValue = [];
        if (data.ResultDataValue && data.ResultDataValue != '') {
            ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
        }
        if(ResultDataValue.Tree==undefined){
        	return ;
        }
        data[me.childrenField] = ResultDataValue.Tree;
        var changeNode = function(node) {
            var value = node['value'];
            if (value && value != null && value != '') {
                if (value.Id == me.hideNodeId) {
                    return true;
                }
            }
            for (var i in value) {
                node[i] = value[i];
            }
            if (node['icon'] && node['icon'] != '') {
                node['icon'] = getIconRootPathBySize(16) + '/' + node['icon'];
            }
            var children = node[me.childrenField];
            if (children) {
                changeChildren(children);
            }
            return false;
        };
        var changeChildren = function(children) {
            for (var i = 0; i < children.length; i++) {
                var bo = changeNode(children[i]);
                if (bo) {
                    children.splice(i, 1);
                    i--;
                }
            }
        };
        var children = data[me.childrenField];
        changeChildren(children);
        response.responseText = Ext.JSON.encode(data);
        return response;
    },
    createTreeColumns:function() {
        var me = this;
        return me.columnsStr;
    },
    getInfoByIdFormServer:function(id, callback) {
        var me = this;
        var url = me.getAppInfoServerUrl + '?isPlanish=true&id=' + id;
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            async:false,
            url:url,
            method:'GET',
            timeout:2e3,
            success:function(response, opts) {
                var result = Ext.JSON.decode(response.responseText);
                if (result.success) {
                    var appInfo = '';
                    if (result.ResultDataValue && result.ResultDataValue != '') {
                        appInfo = Ext.JSON.decode(result.ResultDataValue);
                    }
                    if (Ext.typeOf(callback) == 'function') {
                        callback(appInfo);
                    }
                } else {
                    alertError('获取应用信息失败！');
                }
            },
            failure:function(response, options) {
                alertError('获取应用信息请求失败！');
            }
        });
    },
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
    	};
    	win.show(null,callback);
	},
    setActionColumn:function(node) {
        var me = this;
        var itemsArr = [];
        if (me.isEditBtn == true) {
            itemsArr.push(me.createeditBtn());
        }
        if (me.isShowBtn == true) {
            itemsArr.push(me.createshowBtn());
        }
        var actionColumn = {
            xtype:'actioncolumn',
            text:'操作列',
            width:100,
            align:'center',
            itemId:'Action',
            items:itemsArr
        };
        return actionColumn;
    },
    createeditBtn:function(node) {
        var me = this;
        editBtn = {
            iconCls:'build-button-edit hand',
            tooltip:'修改信息',
            handler:function(grid, rowIndex, colIndex, item, e, record) {
        	    me.editApp(record);
            }
        };
        return editBtn;
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
		if(record.get('hasBeenDeleted') != "true"){
			var me = this;
	        var id = record.get('Id');
	        var AppType = record.get('AppType');
	        me.openAppEditWin(AppType,id);
		}else{
			alertError("本条记录已被删除！");
		}
    },
    /**
     * 打开应用设置页面
     * @private
     * @param {} appType
     * @param {} id
     */
    openAppEditWin:function(appType,id){
    	var me = this;
    	//应用类型信息
    	var appTypeInfo = me.getAppTypeInfo(appType);
    	var title = appTypeInfo.text;
    	var panel = appTypeInfo.className;
    	
        if(title && title != ""){
        	var appId = -1;
	    	if(id && id > 0){
	    		title = "修改"+title;
	    		appId = id;
	    	}else{
	    		title = "新增"+title;
	    	}
	    	
	    	var win = Ext.create(panel,{
	    		title:title,
	    		width:'98%',
	    		height:'98%',
	    		appId:appId,
	    		appType:appType,
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
				}],
				 listeners:{
		        	close:function(){
	        	        me.load();
		            }
	            }
	    	}).show();
	    	//保存监听
			win.on({
				saveClick:function(){
					//win.close();
					me.load(me.externalWhere);
				}
			});
        }else{
    		alertError('选择的构建类型不存在！');
        }
    },
    createshowBtn:function(node) {
        var me = this;
        showBtn = {
            iconCls:'build-button-see hand',
            tooltip:'查看',
            handler:function(grid, rowIndex, colIndex, item, e, record) {
	        	//处理代码
				var callback = function(appInfo){
					//中文名称
					var title = record.get('text');
					//类代码
					var ClassCode = "";
					if(appInfo && appInfo != ""){
						ClassCode = appInfo[me.ClassCode];
					}
					if(ClassCode && ClassCode != ""){
						//打开应用效果窗口
						var id = record.get('Id');
						me.openAppShowWin(title,ClassCode,id);
					}else{
						alertError("没有类代码！");
					}
				};
				//ID号
				var id = record.get('Id');
				var p = Ext.WindowManager.get(id);
				if(p){//已经打开了窗口
					Ext.WindowManager.bringToFront(p);
				}else{
					//与后台交互
					me.getInfoByIdFormServer(id,callback);
				}
            }
        };
        return showBtn;
    },
    initComponent:function() {
        var me = this;
        var bb=[];
      //操作列
        if(me.isShowAction==true){
           bb.push(me.setActionColumn());
        }
        me.columns = me.createTreeColumns().concat(bb);
        me.addEvents('editClick');
        me.addEvents('showClick');
        me.store = me.createStore();
        me.load = function(whereStr) {
            var w = '?fields=' + me.whereFields;
            if(me.type==1){
                var  selectServerUrl=getRootPath() + '/' + 'ConstructionService.svc/CS_RJ_GetBTDAppComponentsFrameListTree';
            }
            if(me.type==0){
                var  selectServerUrl=getRootPath() + '/' + 'ConstructionService.svc/CS_RJ_GetBTDAppComponentsRefTree';
            }
            var myUrl =selectServerUrl + w;
            
            me.store.proxy.url = myUrl;
            me.store.load();
        };
        me.listeners = {
            itemdblclick:function(com,record,item,index,e,eOpts){
				//处理代码
				var callback = function(appInfo){
					//中文名称
					var title = record.get('text');
					//类代码
					var ClassCode = "";
					if(appInfo && appInfo != ""){
						ClassCode = appInfo[me.ClassCode];
					}
					if(ClassCode && ClassCode != ""){
						//打开应用效果窗口
						var id = record.get('Id');
						me.openAppShowWin(title,ClassCode,id);
					}else{
						alertError("没有类代码！");
					}
				};
				
				//ID号
				var id = record.get('Id');
				var p = Ext.WindowManager.get(id);
				if(p){//已经打开了窗口
					Ext.WindowManager.bringToFront(p);
				}else{
					//与后台交互
					me.getInfoByIdFormServer(id,callback);
				}
			}	
        };
        me.plugins = Ext.create('Ext.grid.plugin.CellEditing',{clicksToEdit:1});
        this.callParent(arguments);
    },
    /**
	 * 构建类型列表
	 * @type 
	 */
	appTypeList:[
		{text:'列表',appType:1,className:'Ext.build.BasicListPanel'},
		{text:'表单',appType:2,className:'Ext.build.BasicFormPanel'},
		{text:'应用',appType:3,className:'Ext.build.BuildAppPanel'},
		{text:'单列树',appType:10,className:'Ext.build.BasicSingleTree'},
		{text:'列表树',appType:4,className:'Ext.build.BasicTreePanel'},
		{text:'图表',appType:5,className:'Ext.build.BasicChart'},
		{text:'高级查询(列表)',appType:6,className:'Ext.build.GridSearchPanel'},
		{text:'高级查询(表单)',appType:7,className:'Ext.build.BasicSearchPanel'},
		{text:'分组查询',appType:8,className:'Ext.build.GroupingBase'},
		{text:'选择器(单/复选组)',appType:9,className:'Ext.build.BasicSelector'},
        {text:'普通排序(未实现)',appType:11,className:'Ext.build.BasicSortList'},
        {text:'双表数据移动',appType:12,className:'Ext.build.BasicDoubletable'},
        
        //定制构建
        {text:'已录入项目(定制)',appType:100,className:'Ext.build.BasicInputProject'},
        {text:'检验项目分类(定制)',appType:101,className:'Ext.build.BasicTestItemsClassified'},
        {text:'模板录入(定制)',appType:102,className:'Ext.build.BasicInspectingItem'}
	],
	   /**
     * 根据应用类型编号获取应用类型信息
     * @private
     * @param {} appType
     * @return {}
     */
    getAppTypeInfo:function(appType){
    	var me = this;
    	var appTypeList = me.appTypeList;
    	for(var i in appTypeList){
    		if(appTypeList[i].appType == appType){
    			return appTypeList[i];
    		}
    	}
    	return {};
    }
});