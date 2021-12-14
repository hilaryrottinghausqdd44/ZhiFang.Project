/**
 * 应用列表类
 */
Ext.ns('Ext.build');
Ext.define('Ext.build.AppListPanel',{
	extend:'Ext.grid.Panel',
	alias:'widget.applistpanel',
	title:'应用列表',
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
	 * 是否默认有删除应用按钮
	 * @type Boolean
	 */
	hasDeleteButton:true,
	/**
	 * 是否默认有新增列表按钮
	 * @type Boolean
	 */
	hasAddListButton:true,
	/**
	 * 是否默认有新增表单按钮
	 * @type Boolean
	 */
	hasAddFormButton:true,
	/**
	 * 是否默认有新增应用按钮
	 * @type Boolean
	 */
	hasAddAppButton:true,
    /**
     * 是否默认有新增图表按钮
     * @type Boolean
     */
    hasAddChartButton:true,
    /**
     * 是否默认有新增树按钮
     * @type Boolean
     */
    hasAddTreeButton:true,
    /**
     * 是否默认有新增单列树按钮
     * @type Boolean
     */
    hasAddSingleTreeButton:true,
    /**
     * 是否默认有一般查询(全与关系)按钮
     * @type Boolean
     */
    hasAddGeneralEnquiries:true,  
    /**
     * 是否默认有新增高级查询按钮
     * @type Boolean
     */
    hasAddBasicSearch:true,
    /**
     * 是否默认有新增分组查询按钮
     * @type Boolean
     */
    hasAddGroupingButton:true,
    /**
     * 是否默认有构建单选或复选组按钮
     * @type Boolean
     */
    hasAddBasicradiocheckgroup:true,
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
	getAppInfoServerUrl:getRootPath()+'/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById',
	/**
	 * 删除应用信息的服务地址
	 * @type 
	 */
	deleteAppServerUrl:getRootPath()+'/ConstructionService.svc/CS_UDTO_DelBTDAppComponents',
	/**
	 * 默认的排序
	 * @type String
	 */
	//sort:"[{property:'BTDAppComponents_ModuleOperCode',direction:'ASC'},{property:'BTDAppComponents_CName',direction:'ASC'}]",
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
		/**
		 * 应用组件ID
		 * @type String
		 */
		AppComID:'BTDAppComponents_Id',
		/**
		 * 中文名称
		 * @type String
		 */
		CName:'BTDAppComponents_CName',
		/**
		 * 英文名称
		 * @type String
		 */
		EName:'BTDAppComponents_EName',
		/**
		 * 功能编码
		 * @type String
		 */
		ModuleOperCode:'BTDAppComponents_ModuleOperCode',
		/**
		 * 功能简介
		 * @type String
		 */
		ModuleOperInfo:'BTDAppComponents_ModuleOperInfo',
		/**
		 * 初始化参数
		 * @type String
		 */
		InitParameter:'BTDAppComponents_InitParameter',
		/**
		 * 应用类型
		 * @type String
		 */
		AppType:'BTDAppComponents_AppType',
		/**
		 * 构建类型
		 * @type String
		 */
		BuildType:'BTDAppComponents_BuildType',
		/**
		 * 模块类型
		 * @type String
		 */
		BTDModuleType:'BTDAppComponents_BTDModuleType',
		/**
		 * 执行代码
		 * @type String
		 */
		ExecuteCode:'BTDAppComponents_ExecuteCode',
		/**
		 * 设计代码
		 * @type String
		 */
		DesignCode:'BTDAppComponents_DesignCode',
		/**
		 * 类代码
		 * @type String
		 */
		ClassCode:'BTDAppComponents_ClassCode',
		/**
		 * 创建者
		 * @type String
		 */
		Creator:'BTDAppComponents_Creator',
		/**
		 * 修改者
		 * @type String
		 */
		Modifier:'BTDAppComponents_Modifier',
		/**
		 * 汉字拼音字头
		 * @type String
		 */
		PinYinZiTou:'BTDAppComponents_PinYinZiTou',
		/**
		 * 数据加入时间
		 * @type String
		 */
		DataAddTime:'BTDAppComponents_DataAddTime',
		/**
		 * 数据更新时间
		 * @type String
		 */
		DataUpdateTime:'BTDAppComponents_DataUpdateTime',
		/**
		 * 实验室ID
		 * @type String
		 */
		LabID:'BTDAppComponents_LabID',
		/**
		 * 时间戳
		 * @type String
		 */
		DataTimeStamp:'BTDAppComponents_DataTimeStamp'
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
				itemdblclick:function(com,record,item,index,e,eOpts){
					//处理代码
					var callback = function(appInfo){
						//中文名称
						var title = record.get(me.fieldsObj.CName);
						//类代码
						var ClassCode = "";
						if(appInfo && appInfo != ""){
							ClassCode = appInfo[me.fieldsObj.ClassCode];
						}
						
						if(ClassCode && ClassCode != ""){
							//打开应用效果窗口
							var id = appInfo[me.fieldsObj.AppComID];
							me.openAppShowWin(title,ClassCode,id);
						}else{
							Ext.Msg.alert("提示","没有类代码！");
						}
					}
					
					//ID号
					var id = record.get(me.fieldsObj.AppComID);
					var p = Ext.WindowManager.get(id);
					if(p){//已经打开了窗口
						Ext.WindowManager.bringToFront(p);
					}else{
						//与后台交互
						me.getInfoByIdFormServer(id,callback);
					}
				}
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
		var url = me.getDefaultUrl();
		var store = Ext.create('Ext.data.Store',{
			fields:me.getFields(),
			remoteSort:me.remoteSort,
			proxy: {
	            type:'ajax',
	            url:url,
	            reader:{
	            	type:'json',
	            	totalProperty:'count',
	                root:'list'
	            },
	            extractResponseData:function(response){
			    	var result = Ext.JSON.decode(response.responseText);
			    	if(result[me.dataRoot] && result[me.dataRoot] != ""){
			    		var ResultDataValue = Ext.JSON.decode(result[me.dataRoot]);
				    	result.list = ResultDataValue['list'];
				    	result.count = ResultDataValue['count'];
			    	}else{
			    		result.list = [];
			    		result.count = 0;
			    	}
			    	
			    	if(!result.success){
			    		Ext.Msg.alert('提示','获取应用列表失败！错误信息【<b style="color:red">'+result.ErrorInfo+'</b>】');
			    	}
			    	
			    	response.responseText = Ext.JSON.encode(result);
			    	return response;
			  	}
	        },
	        pageSize:me.pageSize || 25,
	        loadPage:function(page,options){
	        	//条件处理
	        	this.proxy.url = me.getDefaultUrl();
	        	this.proxy.url = me.getLoadUrl();
				//原组件的代码
		        this.currentPage = page;
		        // Copy options into a new object so as not to mutate passed in objects
		        options = Ext.apply({
		            page: page,
		            start: (page - 1) * this.pageSize,
		            limit: this.pageSize,
		            addRecords: !this.clearOnPageLoad
		        }, options);
		
		        if (this.buffered) {
		            return this.loadToPrefetch(options);
		        }
		        this.read(options);
	        }
		});
		return store;
	},
	/**
	 * 创建列表对象
	 * @private
	 */
	createList:function(){
		var me = this;
		if(!me.readOnly){
			me.selType = 'checkboxmodel';//复选框
			me.multiSelect = true;//允许多选
		}
		
		me.viewConfig = {
	        emptyText:'没有数据！',
	        loadingText:'获取数据中，请等待...'
		};
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing',{clicksToEdit:1});
		me.columns = [{
			dataIndex:me.fieldsObj.AppComID,text:'应用ID ',width:150,editor:{readOnly:true}
		},{
			dataIndex:me.fieldsObj.ModuleOperCode,text:'功能编码 ',width:150,editor:{readOnly:true}
		},{
			dataIndex:me.fieldsObj.CName,text:'中文名称',width:150
		},{
			dataIndex:me.fieldsObj.AppType,text:'应用类型',
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
					return Ext.String.format(value);
				}
	 		}
		},{
			dataIndex:me.fieldsObj.DataAddTime,
			width:140,
			text:'创建时间'//,
			//xtype:'datecolumn',
			//format:'Y-m-d H:i:s'
		}];
		me.dockedItems = me.createDockedItems();
		
		if(!me.readOnly){
			me.columns.push({
				dataIndex:'hasBeenDeleted',text:'提示',width:60,
		 		renderer:function(value, p, record){
					if(value == "true"){
						return Ext.String.format("<b style='color:gray'>已删除</b>");
					}else if(value == "false"){
						return Ext.String.format("<b style='color:red'>删除失败</b>");
					}else{
						return Ext.String.format("");
					}
		 		}
			});
		}
		
		
		if(!me.readOnly && me.hasEditButton){
			me.columns.push({
				xtype:'actioncolumn',text:'操作',width:60,align:'center',
				items:[{
					iconCls:'build-button-edit hand',
	                tooltip:'修改元应用',
	                handler:function(grid,rowIndex,colIndex,item,e,record){
	                	me.editApp(record);
	                }
				},{
					iconCls:'build-button-see',
	                tooltip:'查看子应用',
	                itemId:'ziAppComponents',
	                handler:function(grid,rowIndex,colIndex,item,e,record){
	                     var id =record.get('BTDAppComponents_Id');
	                     var type=1;
	                     var winTitle='查看子应用';
					     me.OpenWinTree(id,type,winTitle);
				    }
				},{
					iconCls:'search-img-16',	
	                tooltip:'查看父应用',
	                itemId:'fuAppComponents',
	                handler:function(grid,rowIndex,colIndex,item,e,record){
					    var type=0;
	                    var id =record.get('BTDAppComponents_Id');
	                    var winTitle='查看父应用';
					    me.OpenWinTree(id,type,winTitle);
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
        	var menu = me.appTypeList;
        	//监听
        	for(var i in menu){
        		menu[i].listeners = {
        			click:function(but) {
		        		me.openAppEditWin(but.appType);
				    }
        		};
        	}
	        toolbar.items.push({
	        	xtype:'button',text:'新增',iconCls:'build-button-add',
	        	tooltip:'按照选中的构建类型进行新增操作',
	        	menu:menu
	        });
        }
        
		if(!me.readOnly && me.hasDeleteButton){
			toolbar.items.push({
    			xtype:'button',text:'删除',itemId:'DeleteButton',
    			iconCls:'build-button-delete',
    			handler:function(button){me.deleteAppInfo();}
    		});
		}
		
		toolbar.items.push({
			xtype:'button',text:'更新',itemId:'ListLoadButton',
			iconCls:'build-button-refresh',
			handler:function(button){me.load(me.externalWhere);}
		});
		
		if(me.readOnly){
			toolbar.items.push({
    			xtype:'button',text:'确定',itemId:'OkButton',
    			iconCls:'build-button-ok',
    			handler:function(button){me.fireEvent('okClick');}
    		});
		}
		toolbar.items.push({
			xtype:'button',text:'批量更新列表',itemId:'UpdateListClassCodeButton',
			iconCls:'build-button-save',
			handler:function(button){
				var records = me.getSelectionModel().getSelection();
				if(records.length > 0){
					var parserList = Ext.create('Ext.build.ParserList');
					var createCallback = function(id){
						var f = function(){
							alertInfo('组件'+id+'重新保存成功');
						};
						return f;
					};
					for(var i in records){
						var id = records[i].get(me.fieldsObj.AppComID);
						var callback = createCallback(id);
						parserList.updateAppInfoById(id,callback);
					}
				}
			}
		});
        //查询
        toolbar.items.push('->');
        //开发方式
        toolbar.items.push({
        	xtype:'combo',itemId:'searchBuildType',
        	width:90,value:-1,
            mode:'local',
            editable:false, 
            displayField:'text',
            valueField:'value',
            store:new Ext.data.Store({
            	fields:['text','value'],
            	data:[
            		{text:'所有程序',value:-1},
            		{text:'定制程序',value:0},
            		{text:'构建程序',value:1}
            	]
            }),
            listeners:{
            	change:function(){
            		me.search();
            	},
        		render:function(input){
			    	new Ext.KeyMap(input.getEl(),[{
				      	key:Ext.EventObject.ENTER,
				      	fn:function(){me.search();}
			     	}]);
			    }
        	}
        });
        //类型选择
        toolbar.items.push({
        	xtype:'combo',itemId:'searchAppType',
        	width:110,value:0,
            mode:'local',
            editable:false, 
            displayField:'text',
            valueField:'appType',
            store:new Ext.data.Store({
            	fields:['text','appType'],
            	data:[{text:'所有类型',appType:0}].concat(me.appTypeList)
            }),
            listeners:{
            	change:function(){
            		me.search();
            	},
        		render:function(input){
			    	new Ext.KeyMap(input.getEl(),[{
				      	key:Ext.EventObject.ENTER,
				      	fn:function(){me.search();}
			     	}]);
			    }
        	}
        });
        //模糊查询框
        toolbar.items.push({
        	xtype:'textfield',itemId:'searchText',width:160,
        	emptyText:'应用ID/功能编号/中文名称',
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
        	tooltip:'按照应用类型、功能编号、中文名称进行查询',
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
    /**
     * 删除勾选的应用
     * @private
     */
    deleteAppInfo:function(){
    	var me = this;
    	var records = me.getSelectionModel().getSelection();
    	if(records.length > 0){
    		Ext.Msg.confirm("警告","确定要删除吗？",function (button){
    			if(button == "yes"){
    				Ext.Array.each(records,function(record){
		    			//没有被删除的才去后台删除
			    		if(record.get('hasBeenDeleted') != "true"){
			    			me.deleteAppServer(record);
			    		}
		    		});
    			}
    		});
    	}else{
    		Ext.Msg.alert("提示","<b style='color:red'>请勾选需要删除的记录！</b>");
    	}
    },
    //=====================与后台交互=======================
    /**
     * 后台删除应用信息
     * @private
     * @param {} id
     */
    deleteAppServer:function(record){
    	var me = this;
    	var url = me.deleteAppServerUrl + "?id=" + record.get(me.fieldsObj.AppComID);
    	Ext.Ajax.defaultPostHeader = 'application/json';
    	Ext.Ajax.request({
			async:false,//非异步
			url:url,
			method:'GET',
			timeout:2000,
			success:function(response,opts){
				var result = Ext.JSON.decode(response.responseText);
				if(result.success){
		    		record.set("hasBeenDeleted","true");
		    		record.commit();
				}else{
					record.set("hasBeenDeleted","false");
		    		record.commit();
				}
			},
			failure:function(response,options){ 
				record.set("hasBeenDeleted","false");
		    	record.commit();
			}
		});
    },
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
						result.ResultDataValue =result.ResultDataValue.replace(/[\r\n]+/g,'<br/>');
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
				}]
	    	}).show();
	    	//保存监听
			win.on({
				saveClick:function(){
					//win.close();
					me.load(me.externalWhere);
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
		if(record.get('hasBeenDeleted') != "true"){
			var me = this;
	        var id = record.get(me.fieldsObj.AppComID);
	        var appType = record.get(me.fieldsObj.AppType);
	      	
	        me.openAppEditWin(appType,id);
		}else{
			Ext.Msg.alert("提示","<b style='color:red'>本条记录已被删除！</b>");
		}
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
//			{name:me.fieldsObj.EName,type:'string'},//英文名称
			{name:me.fieldsObj.ModuleOperCode,type:'string'},//功能编码
			{name:me.fieldsObj.ModuleOperInfo,type:'string'},//功能简介
//			{name:me.fieldsObj.InitParameter,type:'string'},//初始化参数
			{name:me.fieldsObj.AppType,type:'string'},//应用类型
//			{name:me.fieldsObj.BuildType,type:'string'},//构建类型
//			{name:me.fieldsObj.BTDModuleType,type:'string'},//模块类型
//			{name:me.fieldsObj.ExecuteCode,type:'string'},//执行代码
//			{name:me.fieldsObj.DesignCode,type:'string'},//设计代码
//			{name:me.fieldsObj.ClassCode,type:'string'},//类代码
//			{name:me.fieldsObj.Creator,type:'string'},//创建者
//			{name:me.fieldsObj.Modifier,type:'string'},//修改者
//			{name:me.fieldsObj.PinYinZiTou,type:'string'},//汉字拼音字头
			{name:me.fieldsObj.DataAddTime,type:'string'},//数据加入时间
//			{name:me.fieldsObj.DataUpdateTime,type:'string'},//数据更新时间
//			{name:me.fieldsObj.LabID,type:'string'},//实验室ID
			{name:me.fieldsObj.DataTimeStamp,type:'string'},//时间戳
			
			{name:'hasBeenDeleted',type:'string'}//已经被删的标记
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
			
			where += "(";
			if(value.length==19 && !isNaN(value)){
				where += "btdappcomponents.Id=" + value + " or ";
			}
			where += "btdappcomponents.CName like '%25" + value + "%25' or btdappcomponents.ModuleOperCode like '%25" + value + "%25'";
			where += ")";
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
    },
    /**
     * 
     * 弹出设置窗体
     * @param {} rec
     * @param {} valueParam HeadFont ColumnFont
     */
   	OpenWinTree:function(id,type,winTitle){
	    var me=this;
	    var xy=me.getPosition();
	    var myxtype=null;
       	if(!myxtype){
       	 myxtype=Ext.create('Ext.build.changkanziyingyong',{
       		type:type,
       		root:{
	             text:"",
	             leaf:false,
	             ParentID:0,
	             id:id,
	             tid:0,
	             expanded:true
	         }
       	 });
		}
        me.win2=null;
        me.win2 = Ext.create('widget.window', {
            title:winTitle,
            autoScroll : true,
            border : false,
            layout: {
                type: 'fit',
                padding: 5
            },
            items: [{
                xtype:myxtype
            }]
        });
        me.win2.show();
    }
});
