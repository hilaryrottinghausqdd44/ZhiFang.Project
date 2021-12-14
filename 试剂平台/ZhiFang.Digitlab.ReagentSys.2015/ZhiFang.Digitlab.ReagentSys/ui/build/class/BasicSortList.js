/**
 * 普通选择器构建工具
 * 【可配参数】
 * 
 * 服务地址配置
 * objectUrl：获取数据对象列表的服务地址
 * objectPropertyUrl：获取数据对象内容的服务地址
 * objectGetDataServerUrl：获取获取服务列表的服务地址
 * objectSaveDataServerUrl：获取保存服务列表的服务地址
 * dictionaryListServerUrl：查询对象属性所属字典服务列表的服务地址
 * addServerUrl：保存新增信息的后台服务地址
 * editServerUrl：保存修改信息的后台服务地址
 * getAppParamsServerUrl：获取参数的后台服务地址
 * 
 * buildTitle：构建名称
 * appId：列表元应用的ID，默认是-1，即新增，大于0的为修改
 * 
 * 
 */
Ext.ns('Ext.build');
Ext.define('Ext.build.BasicSortList',{
    extend:'Ext.panel.Panel',
    alias: 'widget.basicsortlist',
	//=====================可配参数=======================
	/**
	 * 构建的应用类型
	 * @type 
	 */
	appType:-1,
	/**
	 * 应用组件ID
	 */
	appId:-1,
	/**
	 * 构建名称
	 */
	buildTitle:'普通列表排序构建工具',
    
	//数据对象配置private
	/**
	 * 数据对象字段数组
	 * @type 
	 */
	objectFields:['ClassName','CName','EName','SysDic','Description','ShortCode'],
	/**
	 * 获取数据对象列表的服务地址
	 * @type 
	 */
	objectUrl:getRootPath()+'/ConstructionService.svc/CS_BA_GetEntityList',
	/**
	 * 返回数据对象列表的值属性
	 * 例如：
	 * 	返回的json对象：{"ErrorInfo":"","success":true,"ResultDataFormatType":"JSON","ResultDataValue":"{count:1,list:[{a:1}]}"}
	 * 	返回数据对象列表的值属性就是ResultDataValue
	 * @type String
	 */
	objectRoot:'ResultDataValue',
	/**
	 * 数据对象的显示字段
	 * @type String
	 */
	objectDisplayField:'CName',
	/**
	 * 数据对象的值字段
	 * @type String
	 */
	objectValueField:'ClassName',
	
	//对象属性配置private
	/**
	 * 数据对象内容字段数组
	 * @type 
	 */
	objectPropertyFields:['text','InteractionField','RightID','leaf','icon','Tree','tid','checked','FieldClass'],
	/**
	 * 获取数据对象内容时后台接收的参数名称
	 * @type String
	 */
	objectPropertyParam:'EntityName',
	/**
	 * 获取数据对象内容的服务地址
	 * @type 
	 */
	objectPropertyUrl:getRootPath()+'/ConstructionService.svc/CS_BA_GetEntityFrameTree',
	/**
	 * 树的子节点字段
	 * @type String
	 */
	objectRootProperty:'Tree',
	/**
	 * 数据对象内容的显示字段
	 * @type String
	 */
	objectPropertyDisplayField:'text',
	/**
	 * 数据对象内容的值字段
	 * @type String
	 */
	objectPropertyValueField:'InteractionField',
	
	//数据服务配置private
	/**
	 * 数据服务列表字段数组
	 * @type 
	 */
	objectServerFields:['CName','ServerUrl'],
	/**
	 * 返回数据服务列表的值属性
	 * 例如：
	 * 	返回的json对象：{"ErrorInfo":"","success":true,"ResultDataFormatType":"JSON","ResultDataValue":"{count:1,list:[{a:1}]}"}
	 * 	返回数据服务列表的值属性就是ResultDataValue
	 * @type String
	 */
	objectServerRoot:'ResultDataValue',
	/**
	 * 数据服务列表的显示字段
	 * @type String
	 */
	objectServerDisplayField:'CName',
	/**
	 * 数据服务列表的值字段
	 * @type String
	 */
	objectServerValueField:'ServerUrl',
	/**
	 * 获取数据服务列表时后台接收的参数名称
	 * @type String
	 */
	objectServerParam:'EntityName',
	/**
	 * 获取获取服务列表的服务地址
	 * @type 
	 */
	objectGetDataServerUrl:getRootPath()+'/ConstructionService.svc/CS_BA_SearchReturnEntityServiceListByEntityName',
	/**
	 * 获取保存服务列表的服务地址
	 * @type 
	 */
	objectSaveDataServerUrl:getRootPath()+'/ConstructionService.svc/CS_BA_SearchParaEntityServiceListByEntityName',
	
	//查询对象属性所属字典服务列表
	/**
	 * 查询对象属性所属字典服务列表的服务地址
	 * @type 
	 */
	dictionaryListServerUrl:getRootPath()+'/ConstructionService.svc/CS_BA_SearchReturnEntityDictionaryServiceListByEntityPropertynName',
	/**
	 * 查询对象属性所属字典服务列表时后台接收的参数名称
	 * @type String
	 */
	dictionaryListServerParam:'EntityPropertynName',
	
	/**
	 * 保存的后台服务地址
	 * @type 
	 */
	addServerUrl:getRootPath()+'/ConstructionService.svc/CS_UDTO_AddBTDAppComponents',
	/**
	 * 修改的后台服务地址
	 * @type String
	 */
	editServerUrl:getRootPath()+'/ConstructionService.svc/CS_UDTO_UpdateBTDAppComponents',
	/**
	 * 获取应用信息的后台服务地址
	 * @type String
	 */
	getAppInfoServerUrl:getRootPath()+'/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById',
	
	//=====================内部变量=======================
	/**
	 * 时间戳，用于修改保存时使用
	 * @type String
	 */
	DataTimeStamp:'',
	/**
	 * 数据项列表字段
	 * @type 
	 */
	columnParamsField:{
		/**
		 * 交互字段
		 * @type String
		 */
		InteractionField:'InteractionField',
		/**
		 * 显示名称
		 * @type String
		 */
		DisplayName:'DisplayName',
		/**
		 * 组件类型
		 * @type String
		 */
		Type:'Type'
	},
	/**
	 * 属性面板itemId后缀
	 * @type String
	 */
    ParamsPanelItemIdSuffix:'_ParamsPanel',
    /**
     * 当前打开的属性面板
     * @type String
     */
    OpenedParamsPanel:'center',
    /**
     * 列表默认宽度
     * @type Number
     */
    defaultPanelWidth:580,
    /**
     * 列表默认高度
     * @type Number
     */
    defaultPanelHeight:490,
    /**
     * 是否刚刚开启页面
     * @type Boolean
     */
    isJustOpen:true,
    /**
	 * 应用字段对象
	 * @type 
	 */
	fieldsObj:{
		/**
		 * 应用组件ID
		 * @type String
		 */
		AppComID:'BTDAppComponents_AppComID',
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
    //=====================内部视图渲染=======================
	 /**
	 * 初始化列表构建组件
	 */
	initComponent:function(){
		var me = this;
		//初始化内部参数
		me.initParams();
		Ext.Loader.setPath('Ext.ux',getRootPath()+'/ui/extjs/ux');
		Ext.Loader.setPath('Ext.zhifangux',getRootPath()+'/extjs/zhifangux/');
		//初始化视图
		me.initView();
		
		//注册事件
		me.addEvents('saveClick');//保存按钮
		me.addEvents('saveAsClick');//另存按钮
		
		me.callParent(arguments);
	},
	/**
	 * 渲染完后执行
	 * @private
	 */
	afterRender:function(){
        var me = this;
		me.callParent(arguments);
		me.setAppParams();
	},
	/**
	 * 初始化内部参数
	 * @private
	 */
	initParams:function(){
		var me = this;
		//边距
		me.bodyPadding = 2;
		//布局方式
		me.layout = {
			type:'border',
			regionWeights:{south:1,east:2,north:3}
		};
	},
	/**
	 * 初始化视图
	 * @private
	 */
	initView:function(){
		var me = this;
		
		//功能按钮栏
		var north = me.createNorth();
		//效果展示区
		var center = me.createCenter();
		//属性面板
		var east = me.createEast();
		//列属性列表
		var south = me.createSouth();
		
		//功能模块ItemId
		center.itemId = "center";
		north.itemId = "north";
		east.itemId = "east";
		south.itemId = "south";
		
		//功能块位置
		center.region = "center";
		north.region = "north";
		east.region = "east";
		south.region = "south";
		
		//功能块大小
		north.height = 30;
		south.height = 120;
		east.width = 250;
		
		//功能块收缩属性
		east.split = true;
		east.collapsible = true;
		
		south.split = true;
		south.collapsible = true;
        
		me.items = [north,center,east,south];
	},
	/**
	 * 功能栏
	 * @private
	 * @return {}
	 */
	createNorth:function(){
		var me = this;
		var com = {
			xtype:'toolbar',
			border:false,
			items:[
				'<b>'+me.buildTitle+'</b>',
				'->',
				{xtype:'button',text:'浏览',itemId:'browse',iconCls:'build-button-see',
					handler:function(){
						me.browse();
					}
				},
				{xtype:'button',text:'保存',itemId:'save',iconCls:'build-button-save',
					handler:function(){
						me.save(true);
					}
				},
				{xtype:'button',text:'另存',itemId:'saveAs',iconCls:'build-button-save',margin:'0 4 0 0',
					handler:function(){
						me.saveAs();
					}
				}
			]
		};
		return com;
	},
	/**
	 * 效果展示面板
	 * @private
	 * @return {}
	 */
	createCenter:function(){
		var me = this;
		var com = {
			xtype:'panel',
			title:'',
			bodyPadding:'2 10 10 2',
			autoScroll:true,
			items:[{
				xtype:'form',
				title:'列表排序',
				itemId:'center',
				width:me.defaultPanelWidth+50,
				height:me.defaultPanelHeight+30
			}]
		};
		return com;
	},
	/**
	 * 列属性列表
	 * @private
	 * @return {}
	 */
	createSouth:function(){
		var me = this;
		var com = {
			xtype:'grid',
			title:'列属性列表',
			columnLines:true,//在行上增加分割线
			columns:[
            //列模式的集合
				{xtype:'rownumberer',text:'序号',width:35,align:'center',hidden:true},
				{text:'交互字段',dataIndex:'InteractionField',width:135,editor:{readOnly:true},disabled:true},
				{text:'显示名称',dataIndex:'DisplayName',hidden:true},
                {text:'锁定',dataIndex:'IsLocked',width:40,align:'center',
                    xtype:'checkcolumn',
                    editor:{
                        xtype:'checkbox',
                        cls:'x-grid-checkheader-editor'
                    }
                },
                {text:'隐藏',dataIndex:'IsHidden',width:40,align:'center',
                    xtype:'checkcolumn',
                    editor:{
                        xtype:'checkbox',
                        cls:'x-grid-checkheader-editor'
                    }
                },
                {text:'编辑',dataIndex:'IsEditer',width:40,align:'center',
                    xtype:'checkcolumn',
                    editor:{
                        xtype:'checkbox',
                        cls:'x-grid-checkheader-editor'
                    }
                },
                {text:'不可见',dataIndex:'CannotSee',width:50,align:'center',
                    xtype:'checkcolumn',
                    editor:{
                        xtype:'checkbox',
                        cls:'x-grid-checkheader-editor'
                    }
                },
                {text:'可排序',dataIndex:'CanSort',width:50,align:'center',
                    xtype:'checkcolumn',
                    editor:{
                        xtype:'checkbox',
                        cls:'x-grid-checkheader-editor'
                    }
                },
                {text:'默认排序',dataIndex:'DefaultSort',width:60,align:'center',
                    xtype:'checkcolumn',
                    editor:{
                        xtype:'checkbox',
                        cls:'x-grid-checkheader-editor'
                    }
                },
                {text: '排序方式',dataIndex:'SortType',width:60,align:'center',
                    renderer:function(value, p, record){
                        if(value == 'ASC'){
                            return Ext.String.format('正序');
                        }else{
                            return Ext.String.format('倒序');
                        }
                    },
                    editor:new Ext.form.field.ComboBox({
                        mode:'local',editable:false,
                        displayField:'text',valueField:'value',
                        store:new Ext.data.SimpleStore({ 
                            fields:['value','text'], 
                            data:[['ASC','正序'],['DESC','倒序']]
                        }),
                        listClass: 'x-combo-list-small'
                    })
                },
                {text: '列次序',dataIndex:'OrderNum',width:60,align:'center',
                    xtype:'numbercolumn',
                    format:'第0列',
                    editor:{
                        xtype:'numberfield',
                        allowBlank:false,
                        minValue:1,
                        maxValue:999
                    }
                },
                {text:'列宽',dataIndex:'width',width:50,align:'center',
                    xtype:'numbercolumn',
                    format:'0',
                    editor:{
                        xtype:'numberfield',
                        allowBlank:false,
                        minValue:1,
                        maxValue:999
                    }
                }
			],
			store:Ext.create('Ext.data.Store',{
		    	fields:me.getSouthStoreFields(), 
		    	proxy:{
		            type:'memory',
		            reader:{type:'json',root:'list'}
		        }
		    }),
			plugins:Ext.create('Ext.grid.plugin.CellEditing',{clicksToEdit:1})
		};
		
		return com;
	},
	/**
	 * 属性面板
	 * @private
	 * @return {}
	 */
	createEast:function(){
		var me = this;
		//保存信息
		var appInfo = me.createAppInfo();
		//标题设置
		var title = me.createTitle();
		//数据对象
		var dataObj = me.createDataObj();
		//表单宽高
		var panelWH = me.createWidthHieght();
		
		var listParamsPanel = {
			xtype:'form',
			itemId:'center' + me.ParamsPanelItemIdSuffix,
			title:'列表属性配置',
			header:false,
			autoScroll:true,
			border:false,
	        bodyPadding:5,
	        items:[appInfo,panelWH,title,dataObj]
		};
		
		var com = {
			xtype:'form',
			title:'列表属性配置',
			autoScroll:true,
	        items:[listParamsPanel]
		};
		
		return com;
	},
	/**
	 * 功能信息
	 * @private
	 * @return {}
	 */
	createAppInfo:function(){
		var com = {
			xtype:'fieldset',title:'功能信息',padding:'0 5 0 5',collapsible:true,
	        defaultType:'textfield',defaults:{anchor:'100%'},layout:'anchor',
	        itemId:'appInfo',
	        items:[{
				xtype:'textfield',fieldLabel:'功能编号',labelWidth:60,anchor:'100%',
                labelStyle:"font-weight:bold;font-size:11px;font-family:SimHei;color:#FF0000",
	            itemId:'appCode',name:'appCode'
	        },{
				xtype:'textfield',fieldLabel:'中文名称',labelWidth:60,anchor:'100%',
                labelStyle:"font-weight:bold;font-size:11px;font-family:SimHei;color:#FF0000",
	            itemId:'appCName',name:'appCName'
	        },{
				xtype:'textareafield',fieldLabel:'功能简介',labelWidth:60,anchor:'100%',grow:true,
	            itemId:'appExplain',name:'appExplain'
	        }]
		};
		return com;
	},

	/**
	 * 列表标题属性
	 * @private
	 * @return {}
	 */
	createTitle:function(){
		var com = {
        	xtype:'fieldset',title:'标题',padding:'0 5 0 5',collapsible:true,
	        defaultType:'textfield',
	        itemId:'title',
	        items:[{
	            xtype:'textfield',fieldLabel:'显示名称',labelWidth:60,value:'列表',anchor:'100%',
	            itemId:'titleText',name:'titleText'
	        }]
	    };
	    
		return com;
	},
	/**
	 * 数据对象
	 * @private
	 * @return {}
	 */
	createDataObj:function(){
		var me = this;
		var com = {
	    	xtype:'fieldset',title:'列表配置',padding:'0 5 0 5',collapsible:true,
	        defaultType:'textfield',defaults:{anchor:'100%'},layout:'anchor',
            labelStyle:"font-weight:bold;font-size:11px;font-family:SimHei;color:#FF0000",
	        itemId:'dataObject',
	        items:[{
				xtype:'combobox',fieldLabel:'数据对象',
	        	itemId:'objectName',name:'objectName',
	        	labelWidth:60,anchor:'100%',
	        	editable:true,typeAhead:true,
				forceSelection:true,
	        	queryMode:'local',
                labelStyle:"font-weight:bold;font-size:11px;font-family:SimHei;color:#FF0000",
				emptyText:'请选择数据对象',
	        	displayField:me.objectDisplayField,
	        	valueField:me.objectValueField,
	        	store:new Ext.data.Store({
				    fields:me.objectFields,
				    proxy:{
				    	type:'ajax',
				    	url:me.objectUrl,
				    	reader:{type:'json',root:me.objectRoot},
				    	extractResponseData:me.changeStoreData
				    },autoLoad:true
				}),
				listeners:{
					change:function(owner,newValue,oldValue,eOpts){
						var index = owner.store.find(me.objectValueField,newValue);//是否存在这条记录
						if(newValue && newValue != "" && index != -1){
							me.objectChange(owner,newValue);
						}
					}
				}
	        },{
	        	xtype:'treepanel',itemId:'objectPropertyTree',border:false,
			    rootVisible:false,
		        nodeClassName:'',
		        CName:'',
		        ClassName:'',
		        listeners:{
					beforeitemexpand:function(node){
						this.nodeClassName = node.data[me.objectPropertyValueField];
					},
					beforeload:function(store){
						if(this.nodeClassName != ""){
							store.proxy.url = me.objectPropertyUrl + "?" + me.objectPropertyParam + "=" + this.nodeClassName;
						}
					},
					checkchange:function(node,checked){
						//树节点的勾选处理(util公共方法)
						treeNodeCheckedChange(node,checked);
					}
		        },
			    store:new Ext.data.TreeStore({
				    fields:me.objectPropertyFields,
				    proxy:{
			            type:'ajax',
			            url:me.objectPropertyUrl,
			            extractResponseData:function(response){
			            	var data = Ext.JSON.decode(response.responseText);
			            	if(data.ResultDataValue && data.ResultDataValue != ""){
			            		var children = Ext.JSON.decode(data.ResultDataValue);
			            	
				            	for(var i in children){
				            		children[i].checked = false;
				            	}
								var objectPropertyTree =me.getobjectPropertyTree();	
								if(objectPropertyTree.nodeClassName != ""){
									data[me.objectRootProperty] = children;
								}else{
									data[me.objectRootProperty] = [{
					            		text:objectPropertyTree.CName,
					            		InteractionField:objectPropertyTree.ClassName,
					            		leaf:false,
					            		expanded:true,
					            		checked:false,
					            		Tree:children
					            	}];
								}
			            	}
			            	response.responseText = Ext.JSON.encode(data);
			            	return response;
			          	}
			        },
			        defaultRootProperty:me.objectRootProperty,
		          	root:{
						text:'对象结构',
						leaf:false,
						expanded:true
					},
				    autoLoad:false
				})
	        },{xtype:'combobox',fieldLabel:'排序字段',
                itemId:'valuePanelField',
                name:'valuePanelField',
                labelWidth:60,anchor:'100%',
                labelStyle:"font-weight:bold;font-size:11px;font-family:SimHei;color:#FF0000",
                editable:false,typeAhead:true,
                forceSelection:true,mode:'local',
                emptyText:'请选择排序字段',
                displayField:me.objectPropertyDisplayField,
                valueField:me.objectPropertyValueField,
                store:new Ext.data.Store({
                    fields:me.objectPropertyFields,
                    proxy:{
                        type:'ajax',
                        url:me.objectGetDataServerUrl,
                        reader:{type:'json',root:me.objectServerRoot},
                        extractResponseData:me.changeStoreData
                    }
                }),
                listeners:{
                    select:function(owner,records,eOpts){
                    //值字段赋值
                     var objectName=me.getobjectName();
                     var componentItemId=objectName.getValue();
                     var newValue=owner.getValue();
                     if(newValue && newValue != ""){
                            var arr = newValue.split("_");
                            var value = arr[arr.length-2]+"_"+arr[arr.length-1];
                            //值字段赋值
                            me.setColumnParamsRecord(componentItemId,'valueField',value);
                        }
                    }
                }
             },{
	        	xtype:'combobox',fieldLabel:'获取数据',
                labelStyle:"font-weight:bold;font-size:11px;font-family:SimHei;color:#FF0000",
	        	itemId:'getDataServerUrl',name:'getDataServerUrl',
	        	labelWidth:60,anchor:'100%',
	        	editable:false,typeAhead:true,
				forceSelection:true,mode:'local',
				emptyText:'请选择获取数据服务',
	        	displayField:me.objectServerDisplayField,
	        	valueField:me.objectServerValueField,
	        	store:new Ext.data.Store({
				    fields:me.objectServerFields,
				    proxy:{
				    	type:'ajax',
				    	url:me.objectGetDataServerUrl,
				    	reader:{type:'json',root:me.objectServerRoot},
				    	extractResponseData:me.changeStoreData
				    },
				    listeners:{
				    	beforeload:function(store,operation,eOpts){
							var objectName = me.getobjectName();
				    		store.proxy.url = me.objectGetDataServerUrl + "?" + me.objectServerParam + "=List" + objectName.value;
				    	}
				    }
				})
	        },{xtype:'combobox',fieldLabel:'修改数据',
                labelStyle:"font-weight:bold;font-size:11px;font-family:SimHei;color:#FF0000",
                itemId:'editDataServerUrl',name:'editDataServerUrl',
                labelWidth:60,anchor:'100%',
                editable:false,typeAhead:true,
                forceSelection:true,mode:'local',
                emptyText:'请选择修改数据服务',
                displayField:me.objectServerDisplayField,
                valueField:me.objectServerValueField,
                store:new Ext.data.Store({
                    fields:me.objectServerFields,
                       proxy:{
                        type:'ajax',
                        url:me.objectGetDataServerUrl,
                        reader:{type:'json',root:me.objectServerRoot},
                        extractResponseData:me.changeStoreData
                    },
                    listeners:{
                        beforeload:function(store,operation,eOpts){
                            var objectName = me.getobjectName();
                            store.proxy.url = me.objectSaveDataServerUrl + "?" + me.objectServerParam + "=" + objectName.getValue();
                            
                        }
                    }
                })            
            },{
	        	xtype:'textfield',fieldLabel:'默认条件',labelWidth:60,value:'',
	        	itemId:'defaultParams',name:'defaultParams'
	        },{
                    xtype:'toolbar',
                    style:{background:'#fff'},
                    itemId:'objectPropertyToolbar',
                    border:false,
                    items:[{
                        xtype:'button',text:'确定',itemId:'objectPropertyOK',
                        iconCls:'build-button-ok',
                        listeners:{
                            click:function(){
                                me.objectPropertyOKClick();
                            }
                        }
                    }]
                }]
	    };
		return com;
	},
    //列表配置
    getDataObject:function(){
	    var me=this;
	    var east = me.getComponent('east').getComponent('center'+me.ParamsPanelItemIdSuffix);
	    var dataObject = east.getComponent('dataObject');
	    return dataObject;
    },
    getobjectName:function(){
        var me=this;
        var dataObject = me.getDataObject();
        var objectName = dataObject.getComponent('objectName');
        return objectName;                          
    },
    getobjectPropertyTree:function(){
        var me=this;
        var dataObject = me.getDataObject();
        var objectPropertyTree = dataObject.getComponent('objectPropertyTree');
        return objectPropertyTree;                          
    },
    getDataWH:function(){
        var me=this;
        var east = me.getComponent('east').getComponent('center'+me.ParamsPanelItemIdSuffix);
        var wh = east.getComponent('WH');
        return wh;
    },
    getDataServerUrl:function(){
        var me=this;
        var dataObject = me.getDataObject();
        var getDataServerUrl = dataObject.getComponent('getDataServerUrl');
        return getDataServerUrl;                          
    },
    getEastWidth:function(){
        var me=this;
        var dataObject = me.getDataWH();
        var getDataServerUrl = dataObject.getComponent('Width');
        return getDataServerUrl;                          
    },
    getEastHeight:function(){
        var me=this;
        var dataObject = me.getDataWH();
        var getDataServerUrl = dataObject.getComponent('Height');
        return getDataServerUrl;                          
    },
    getEditDataServerUrl:function(){
        var me=this;
        var dataObject = me.getDataObject();
        var editDataServerUrl = dataObject.getComponent('editDataServerUrl');
        return editDataServerUrl;                          
    },
    
    getDefaultParams:function(){
        var me=this;
        var dataObject = me.getDataObject();
        var defaultParams = dataObject.getComponent('defaultParams');
        return defaultParams;                          
    },
    
	/**
	 * 设置面板的宽高
	 * @private
	 * @return {}
	 */
	createWidthHieght:function(){
		var me = this;
		var com = {
			xtype:'fieldset',title:'宽高配置',padding:'0 5 0 5',collapsible:true,
	        defaultType:'textfield',defaults:{anchor:'100%'},layout:'anchor',
	        itemId:'WH',
	        items:[{
				xtype:'numberfield',fieldLabel:'宽度',labelWidth:60,anchor:'100%',
	            itemId:'Width',name:'Width',value:me.defaultPanelWidth,
	            listeners:{
					blur:function(com,The,eOpts){
						var center = me.getCenterCom();
						//center.setWidth(com.value);
                       
					}
				}
	        },{
				xtype:'numberfield',fieldLabel:'高度',labelWidth:60,anchor:'100%',
	            itemId:'Height',name:'Height',value:me.defaultPanelHeight,
	            listeners:{
					blur:function(com,The,eOpts){
						var center = me.getCenterCom();
						//center.setHeight(com.value);
					}
				}
	        }]
		};
		return com;
	},
	/**
     * 新建表单
     * @private
     * @return {}
     */
    createForm:function(){
        var me = this;
        //表单配置参数
        var params = me.getPanelParams();
        //表单数据项
        var title = "列表排序构建";
        var width = parseInt(params.Width);
        var height = parseInt(params.Height);
        var form = {
            xtype:'form',
            itemId:'center',
            layout:'absolute',
            autoScroll:true,
            title:title,
            width:width+20,
            height:height+20,
            resizable:{handles:'s e'}
        };
        //加载数据方法
        if(!params.hasTitle){
            form.preventHeader = false;
        }
        return form;
    },
    /**
     * 创建所有组件
     * @private
     * @return {}
     */
    createComponents:function(){
        var me = this;
        var arr = {
            basicComArr:[],//一般组件
            otherComArr:[]//
        };
        //所有数据项基础属性
        var coms = me.createComponentsBasicInfo();
        for(var i in coms){
            var com = coms[i];
            //com.draggable = false;//注释这一行,改变大小事件失效,拖放事件生效
            arr.basicComArr.push(com);
        }
        //合并组件数组
        var comArr = arr.basicComArr.concat(arr.otherComArr);

        return comArr;
    },

    /**
     * 所有数据项基础属性
     * @private
     * @return {}
     */
    createComponentsBasicInfo:function(){
        var coms = [];
        var me = this
		var com = me.createComfield();
		  coms.push(com);
		return coms;
    },
	//=====================功能按钮栏事件方法=======================
	/**
	 * 浏览处理
	 * @private
	 */
	browse:function(){
        var me = this;
        var center = me.getCenterCom();
        var owner = center.ownerCt;
        var form = me.createForm();
        if(form){
            //删除原先的表单
            owner.remove(center);
            //添加新的表单
            owner.add(form);
        }
        var center = me.getCenterCom();
        //表单数据项
        var items = me.createComponents();
        for(var i in items){
            center.add(items[i]);
        }
	},
	/**
	 * 保存按钮事件处理
	 * @private
	 */
	save:function(bo){
		var me = this;
		
		//表单参数
		var params = me.getPanelParams();
		
		var isOk = true;
		var message = "";
		
		if(params.appCode == ""){
			message += "【<b style='color:red'>功能编号不能为空！</b>】\n";
			isOk = false;
		}
		if(params.appCName == ""){
			message += "【<b style='color:red'>中文名称不能为空！</b>】\n";
			isOk = false;
		}
		
		if(!isOk){
			Ext.Msg.alert("提示",message);
		}else{
			//设计代码（还原代码）
			var appParams = me.getAppParams();
			//类代码
			var appClass = me.createAppClass();
			//应用组件ID
			var id = bo ? me.appId : -1;
			//生成应用对象
			var BTDAppComponents = {
				Id:id,//应用组件ID
				CName:params.appCName,//名称
				ModuleOperCode:params.appCode,//功能编码
				ModuleOperInfo:params.appExplain,//功能简介
				InitParameter:params.defaultParams,//初始化参数
				AppType:me.appType,//应用类型(列表)
				BuildType:1,//构建类型(构建)
				//BTDModuleType:1,//模块类型(列表)
				//ExecuteCode:appStr,//执行代码
				DesignCode:me.JsonToStr(appParams),//设计代码
				ClassCode:appClass//类代码
			};
			
			if(me.DataTimeStamp != ""){
				BTDAppComponents.DataTimeStamp = me.DataTimeStamp;//时间戳
			}
			
			var callback = function(){
				me.fireEvent('saveClick');
			}
			
			//后台保存数据
			me.saveToServer(BTDAppComponents,callback);
		}
	},
	/**
	 * 另存按钮事件处理
	 * @private
	 */
	saveAs:function(){
		var me = this;
		me.save(false);
	},
	//=====================属性面板事件方法=======================
    
	/**
	 * 数据对象改变时处理
	 * @private
	 * @param {} owner
	 * @param {} newValue
	 */
	objectChange:function(owner,newValue){
		var me = this;
		var dataObject = owner.ownerCt;
		//获取对象结构
		var objectPropertyTree =me.getobjectPropertyTree();
		
		objectPropertyTree.nodeClassName = "";
		objectPropertyTree.CName = owner.rawValue;
		objectPropertyTree.ClassName = newValue;
		
		objectPropertyTree.store.proxy.url = me.objectPropertyUrl + "?" + me.objectPropertyParam + "=" + newValue;
		objectPropertyTree.store.load();
    	
		//获取获取数据服务列表
		var getDataServerUrl = me.getDataServerUrl();
        var Urlstr=me.objectGetDataServerUrl + "?" + me.objectServerParam + "=List" + newValue;
		getDataServerUrl.store.proxy.url = me.objectGetDataServerUrl + "?" + me.objectServerParam + "=List" + newValue;
		getDataServerUrl.store.load();
		
        //获取修改数据服务列表
        var editDataServerUrl = me.getEditDataServerUrl();
        editDataServerUrl.store.proxy.url = me.objectSaveDataServerUrl + "?" + me.objectServerParam + "=" + newValue;
        editDataServerUrl.store.load();
        
        //获取新增数据服务列表值字段
        var getInterfact = me.getValuePanelField();
        getInterfact.store.proxy.url = me.objectPropertyUrl + "?" + me.objectPropertyParam + "=" + newValue;
        getInterfact.store.load();
		
	},
    getValuePanelField:function(){
        var me = this;
        var dataObject=me.getDataObject();
        var valuePanelField=dataObject.getComponent('valuePanelField');
        return valuePanelField;
    },
	/**
	 * 对象树的勾选完后点击确定按钮处理
	 * @private
	 */
	objectPropertyOKClick:function(){
		var me = this;
		var dataObject =me.getDataObject();
        
        var objecName=me.getobjectName();
        if(objecName.getValue()===null ){
            Ext.Msg.alert('提示','请配置数据对象');
            return ;
         }
        //获取列表服务
        var selectServerUrl =me.getDataServerUrl();
        if(selectServerUrl.getValue()===null ){
            Ext.Msg.alert('提示','请配置获取数据服务');
            return ;
         }
         //获取列表服务
        var editServerUrl =me.getEditDataServerUrl();
        if(editServerUrl.getValue()===null ){
            Ext.Msg.alert('提示','请配置修改数据服务');
            return ;
         }
		var listorder =me.getValuePanelField();
        
        var store = me.getComponent('south').store;
        store.removeAll();
        
        var ColumnParams = me.getobjectPropertyTree();//对象属性树
        var data = ColumnParams.getChecked();
        var store = me.getComponent('south').store;
        
        //勾选节点数组
        var dataArray = [];
        //列表中显示被勾选中的对象
        Ext.Array.each(data,function(record){
            if(record.get('leaf')){
                var index = store.findExact('InteractionField',record.get(me.columnParamsField.InteractionField));
                dataArray.push(record.get(me.columnParamsField.InteractionField));
                
                if(index === -1){//新建不存在的对象
                    var rec = ('Ext.data.Model',{
                        DisplayName:record.get('text'),
                        InteractionField:record.get(me.columnParamsField.InteractionField),
                        
                        IsLocked:false,//默认锁定
                        IsHidden:false,//默认隐藏
                        IsEditer:false,  //默认编辑
                        CannotSee:false,//不可见
                        CanSort:false,//可排序
                        DefaultSort:false,//默认排序
                        SortType:'ASC',//排序方式
                        OrderNum:1,//排布顺序
                        IsLocked:false,//默认锁定
                        AlignType:'left',//对齐方式
                        width:100//数据项宽度
                        
                    });
                    store.add(rec);
                }
            }
        });
        
        //删除没有被勾选的列
        var bo = false;
        var arrayToRemove = [];//需要被删除的列
        store.each(function(record){
            if(record && record.get('InteractionField') != null && record.get('InteractionField') != ""){
                bo = false;
                for(var i in dataArray){
                    if(record.get('InteractionField') === dataArray[i]){
                        bo = true; break;
                    }
                }
                if(!bo){
                    arrayToRemove.push(record);
                }
            }
        });
        for(var i in arrayToRemove){
            store.remove(arrayToRemove[i]);
        }
        var num = 0;
        store.each(function(record){
            num++;
            record.set('OrderNum',num);
            record.commit();
        });
        
		me.browse();//展示效果
	},
	/**
	 * 对象内容勾选
	 * @private
	 * @param {} southParams
	 */
	changeObjChecked:function(southParams){
        var me = this;
        var objectPropertyTree =me.getobjectPropertyTree();//对象属性树
        var rootNode = objectPropertyTree.getRootNode();
        
        //展开需要展开的所有父节点
        var expandParentNode = function(value,callback){
            var arr = value.split("_");
            if(arr.length >1){
                var v = arr[0];
                var num = 1;
                var open = function(){
                    if(num < arr.length-1){
                        v = v + "_" + arr[num];
                        var n = rootNode.findChild("InteractionField",v,true);
                        if(!n.isExpanded()){//节点没有展开
                            num++;
                            n.expand(false,open);
                        }else{
                            num++;
                            open();
                        }
                    }else{
                        callback();//完成
                    }
                };
                open();
            }else{
                callback();
            }
        }
        
        //选中节点
        var checkedNode = function(value){
            var node = rootNode.findChild("InteractionField",value,true);
            if(node != null){//节点存在
                node.set('checked',true);
                treeNodeCheckedChange(node,true);
            }
        }
        
        var nodeArr = [];//没展开的节点数组
        for(var i in southParams){
            var value = southParams[i].InteractionField;
            var node = rootNode.findChild("InteractionField",value,true);
            if(node != null){//节点存在
                node.set('checked',true);
                treeNodeCheckedChange(node,true);
            }else{//节点不存在
                nodeArr.push(value);
            }
        }
        //勾选展开后的节点
        var openNodes = function(nodes){
            for(var i in nodes){
                checkedNode(nodes[i]);
            }
        }
        if(nodeArr.length == 0){
            me.isJustOpen = false;
            me.browse();//渲染效果
        }else{
            var count = 0;
            var changeNodes = function(num){
                var callback =function(){
                    if(num == nodeArr.length-1){
                        if(me.appId != -1 && me.isJustOpen){
                            openNodes(nodeArr);
                            me.isJustOpen = false;
                            me.browse();//渲染效果
                        }
                    }else{
                        changeNodes(++num);
                    }
                }
                expandParentNode(nodeArr[num],callback);
            }
            //延时500毫秒处理
            setTimeout(function(){changeNodes(0);},500);
        }
    },

	//=====================组件的创建与删除=======================
    /**
     * 获取列属性数据(已按列次序排序)
     * @private
     * @return {}
     */
    getColumnParams:function(){
        var myItems = [];
        var me = this;
        var list = me.getComponent('south');
        var items = list.store.data.items;
        
        var map = [];
        for(var i in items){
            var kv = {OrderNum:items[i].get('OrderNum'),Index:i};
            map.push(kv);
        }
        
        for(var i=0;i<map.length-1;i++){
            for(var j=i+1;j<map.length;j++){
                if(map[i].OrderNum > map[j].OrderNum){
                    var temp = map[i];
                    map[i] = map[j];
                    map[j] = temp;
                }
            }
        }
        
        for(var i in map){
            var record = {};
            var item = items[map[i].Index];
            
            record.InteractionField = item.get('InteractionField');
            record.DisplayName = item.get('DisplayName');
            record.IsLocked = item.get('IsLocked');
            record.IsHidden = item.get('IsHidden');
            record.CannotSee = item.get('CannotSee');
            record.CanSort = item.get('CanSort');
            record.DefaultSort = item.get('DefaultSort');
            record.SortType = item.get('SortType');
            
            record.OrderNum = item.get('OrderNum');
            record.width = item.get('width');
            record.AlignType = item.get('AlignType');
            record.Editor=item.get('IsEditer');
            myItems.push(record);
        }
        
        return myItems;
    },
    /**
     * 创建列
     * @private
     * @return {}
     */
    createColumns:function(){
        var me = this;
        //列属性(已排序)
        var columnParams = me.getColumnParams();
        var columns = [];
        for(var i in columnParams){
            var cmConfig = {};
            cmConfig.text = columnParams[i].DisplayName;
            cmConfig.dataIndex = columnParams[i].InteractionField;
            cmConfig.width = columnParams[i].width;
            
            if(columnParams[i].IsLocked){
                cmConfig.locked = columnParams[i].IsLocked;
            }
            
            cmConfig.sortable = columnParams[i].CanSort;
            cmConfig.hidden = (columnParams[i].IsHidden || columnParams[i].CannotSee);
            cmConfig.hideable = !columnParams[i].CannotSee;
            cmConfig.align = columnParams[i].AlignType;
            cmConfig.editor=columnParams[i].Editor;//设置默认列不允许编辑
            
            //设置列编辑、默认不允许编辑
            if(cmConfig.editor)
            {
              cmConfig.editor={allowBlank:true};
            }
            
            if(columnParams[i].ColumnType == "bool"){
                cmConfig.xtype = "booleancolumn";
                cmConfig.trueText = "是";
                cmConfig.falseText = "否";   
            }
            
            columns.push(cmConfig);
        }

        return columns;
    },
	/**
	 * 新建列表
	 * @private
	 * @return {}
	 */
	createComfield:function(){
		var me = this;
        var objecName=me.getobjectName();
		var record=me.getSouthRecordByKeyValue('InteractionField',objecName.getValue());
        
        var dataServerUrl2=me.getDataServerUrl();
        if(dataServerUrl2.getValue()===null ){
            Ext.Msg.alert('请配置列表的数据对象');
            return ;
         }else{
            dataServerUrl2=(getRootPath() + '/' + dataServerUrl2.getValue().split('?')[0] + '?isPlanish=true&where=');
         }

        //获取左列表服务
        var tempSaveUrl=me.getEditDataServerUrl();
        if(tempSaveUrl.getValue()===null ){
            Ext.Msg.alert('请配置列表的修改数据服务');
            return ;
         }else{
            tempSaveUrl=(getRootPath() + '/' + tempSaveUrl.getValue().split('?')[0] + '?where=');
         }
        var listorder2=me.getValuePanelField();
        //列
        var columns = me.createColumns();
        var params=me.getPanelParams();
        //菜单中是否有排序选项
        var sortableColumns = me.isSortableColumns();
		var grid = {
			xtype: 'sortlist',
			itemId:objecName.getValue(),//'center',
			autoScroll:true,
			width:me.getEastWidth().getValue(),
		    height:me.getEastHeight().getValue(),
		    //saveType:1,//
		    dataServerUrl:dataServerUrl2,//左列表获取后台数据服务地址
		    saveServerUrl:tempSaveUrl,//保存服务地址
		    listorder:listorder2.getValue(),
		    modelField:[],//列表的model的Field
		    valueField:columns,//数据列表值字段,可以是外面做好数据适配后传进来
            sortableColumns:sortableColumns,
            resizable:{handles:'s e'}
		};

		//列表面板事件监听
		grid.listeners = { 
//			resize:function(com,width,height,oldWidth,oldHeight,eOpts){//列表大小变化
//				//列表宽度和高度赋值
//				var obj = {Width:width,Height:height};
//				me.setPanelParams(obj);
//			},
//            columnresize:{//列宽度改变
//                fn: function(ct,column,width,e,eOpts){
//                    var dataIndex = column.dataIndex;
//                    me.setColumnWidth(dataIndex,width);
//                }
//            },
//            columnmove:{//列位置移动
//                fn: function(ct,column,fromIdx,toIdx,eOpts){
//                    var dataIndex = column.dataIndex;
//                    me.setColumnOrderNum(dataIndex,fromIdx+1,toIdx+1);
//                }
//            },
			itemclick:function(){
           }       
		};
		return grid;
	},
    /**
     * 菜单选项内容排序是否可见
     * @private
     * @return {} boolean
     */
    isSortableColumns:function(){
        var me = this;
        //列属性(已排序)
        var columnParams = me.getColumnParams();
        var sortableColumns = false;
        for(var i in columnParams){
            if(columnParams[i].CanSort && !columnParams[i].CannotSee){
                sortableColumns = true;
            }
        }
        
        return sortableColumns;
    },  
    /**
     * 设置列次序
     * @private
     * @param {} dataIndex
     * @param {} fromIdx
     * @param {} toIdx
     */
    setColumnOrderNum:function(dataIndex,fromIdx,toIdx){
        var me = this;
        var list = me.getComponent('south');//列属性列表
        var store = list.store;
        var index = store.findExact('InteractionField',dataIndex);//是否存在这条记录
        if(index != -1){
            var it;
            var ind;
            if(fromIdx < toIdx){
                ind = store.findExact('OrderNum',fromIdx);
                if(ind != -1){
                    it = store.getAt(ind);
                    it.set('OrderNum',toIdx);
                    it.commit();
                }
                for(var i=fromIdx+1;i<toIdx;i++){
                    ind = store.findExact('OrderNum',i);
                    if(ind != -1){
                        it = store.getAt(ind);
                        it.set('OrderNum',i-1);
                        it.commit();
                    }
                }
                ind = store.findExact('OrderNum',toIdx);
                if(ind != -1){
                    it = store.getAt(ind);
                    it.set('OrderNum',toIdx-1);
                    it.commit();
                }
            }else{
                ind = store.findExact('OrderNum',fromIdx);
                if(ind != -1){
                    it = store.getAt(ind);
                    it.set('OrderNum',1111);
                    it.commit();
                }
                for(var i=fromIdx;i>toIdx;i--){
                    ind = store.findExact('OrderNum',i-1);
                    if(ind != -1){
                        it = store.getAt(ind);
                        it.set('OrderNum',i);
                        it.commit();
                    }
                }
                ind = store.findExact('OrderNum',1111);
                if(ind != -1){
                    it = store.getAt(ind);
                    it.set('OrderNum',toIdx);
                    it.commit();
                }
            }
        }
    },
    /**
     * 设置标头宽度
     * @private
     * @param {} dataIndex
     * @param {} width
     */
    setColumnWidth:function(dataIndex,width){
        var me = this;
        var list = me.getComponent('south');//列属性列表
        var store = list.store;
        var index = store.findExact('InteractionField',dataIndex);//是否存在这条记录
        if(index != -1){
            var item = store.getAt(index);
            
            item.set('width',width);
            item.commit();
        }
    },
	/**
	 * 创建字段
	 * @private
	 * @return {}
	 */
	createFields:function(){
		var me = this;
		//列属性(已排序)
		var columnParams = me.getColumnParams();
		var fields = [];
		
		for(var i in columnParams){
			fields.push(columnParams[i].InteractionField);
		}
		
		return fields;
	},
	/**
	 * 创建服务代理
	 * @private
	 * @param {} url
	 * @return {}
	 */
	createProxy:function(url){
		var proxy = {
            type:'ajax',
            url:url,
            reader:{
            	type:'json',
            	root:'list',
            	totalProperty:'count'
            },
            extractResponseData: function(response){
            	var data = Ext.JSON.decode(response.responseText);
            	if(data.ResultDataValue && data.ResultDataValue != ""){
            		var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
	            	data.count = ResultDataValue.count;
	            	data.list = ResultDataValue.list;
            	}else{
	            	data.count = 0;
            		data.list = [];
            	}
            	response.responseText = Ext.JSON.encode(data);
            	return response;
          	}
        };
        return proxy;
	},
	
	//=====================组件属性面板的创建与删除=======================
	 /**
     * 切换组件属性配置面板
     * @private
     * @param {} componentItemId
     */
    switchParamsPanel:function(componentItemId){
        var me = this;
        //属性面板ItemId
        var panelItemId = componentItemId + me.ParamsPanelItemIdSuffix;
        var east = me.getComponent('east');
        var panel = east.getComponent(panelItemId);
        if(panel && componentItemId != me.OpenedParamsPanel){
            var OpenedParamsPanel = east.getComponent(me.OpenedParamsPanel + me.ParamsPanelItemIdSuffix);
            if(OpenedParamsPanel){
                OpenedParamsPanel.hide();//隐藏
            }
            east.setTitle(panel.title);//设置标题
            me.setParamsPanelValues(componentItemId);//给属性面板赋值
          
            panel.show();//打开
            me.OpenedParamsPanel = componentItemId;
        }
  	},
    /**
     * 生成所有的组件属性面板
     * @private
     */
    changeParamsPanel:function(){
        var me = this;
        var centerItemId = 'center' + me.ParamsPanelItemIdSuffix;
        var east = me.getComponent('east');
        var items = east.items.items;
        
        var removeArr = [];//需要删除的属性面板
        
        for(var i in items){
            if(items[i].itemId != centerItemId){
                removeArr.push(items[i].itemId);
            }else{
                east.setTitle(items[i].title);
                me.OpenedParamsPanel = "center";
                items[i].show();
            }
        }
        for(var i in removeArr){
            east.remove(removeArr[i]);
        }
        //添加组件属性面板
        var objectName=me.getobjectName();
        me.addParamsPanel(objectName.getValue(),objectName.getRawValue());
    },
    /**
     * 添加组件属性面板
     * @private
     * @param {} type
     * @param {} componentItemId
     * @param {} title
     */
    addParamsPanel:function(componentItemId,title){
        var me = this;
        var east = me.getComponent('east');
        //创建组件属性面板
        var panel = me.createParamsPanel(componentItemId,title);
        //添加面板
        east.add(panel);
    },
    /**
     * 给属性面板赋值
     * @private
     * @param {} record
     */
    setParamsPanelValues:function(componentItemId){
        var me = this;
        if(componentItemId != "center"){
            var record = me.getSouthRecordByKeyValue('InteractionField',componentItemId);
            //属性面板ItemId
            var panelItemId = componentItemId + me.ParamsPanelItemIdSuffix;

        }
    },
    /**
     * 给数据项属性列表赋值
     * @private
     * @param {} InteractionField
     * @param {} key
     * @param {} value
     */
    setColumnParamsRecord:function(InteractionField,key,value){
       var me = this;
       var grid = me.getComponent('south');
       var store = grid.getStore();
       var record = store.findRecord('InteractionField',InteractionField);
       if(record != null){//存在
           record.set(key,value);
           record.commit();
       }
    },
    /**
     * 创建组件属性面板
     * @private
     * @param {} type
     * @param {} componentItemId
     * @param {} title
     * @return {}
     */
    createParamsPanel:function(componentItemId,title){
        var me = this;
        //属性面板ItemId
        var panelItemId = componentItemId + me.ParamsPanelItemIdSuffix;
    	var params = me.getPanelParams();//配置参数
        var width=parseInt(params.Width);
        var height=parseInt(params.Height);
        var com = {
            xtype:'form',
            itemId:panelItemId,
            title:title,
            header:false,
            autoScroll:true,
            border:false,
            bodyPadding:5,
            hidden:true
        };
        
        //组件基础属性 
        var basicItems =[];
        //显示名称
        basicItems =me.createBasicItems(componentItemId,title);
        //组件特有属性
//        var otherItems = [];
        var items = basicItems.concat();
        
        com.items = items;
        return com;
    },
    
    /**
     * 组件基础属性
     * @private
     * @param {} componentItemId
     * @return {}
     */
    createBasicItems:function(componentItemId){
        var me = this;
        var items = [{
            xtype:'fieldset',title:'组件基础属性',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',
            itemId:'basicParams',
            items:[]
	       
        }];
        return items;
    },
	
	//=====================弹出窗口=======================
    
    //=====================设置获取参数=======================
	/**
	 * 获取展示区域组件
	 * @private
	 * @return {}
	 */
	getCenterCom:function(){
		var me = this;
		var center = me.getComponent('center').getComponent('center');
		return center;
	},
	/**
	 * 获取面板配置参数
	 * @private
	 * @return {}
	 */
	getPanelParams:function(){
		var me = this;
		var formParamsPanel = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
		var params = formParamsPanel.getForm().getValues();
		return params;
	},
	/**
	 * 给你面板配置参数赋值
	 * @private
	 * @param {} obj
	 */
	setPanelParams:function(obj){
		var me = this;
		var formParamsPanel = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
		formParamsPanel.getForm().setValues(obj);
	},
	
	/**
	 * 根据键值对从应用组件属性列表中获取信息
	 * @private
	 * @param {} key
	 * @param {} value
	 * @return {} record or null
	 */
	getSouthRecordByKeyValue:function(key,value){
		var me = this;
		var store = me.getComponent('south').store;
		var record = store.findRecord(key,value);
		
		return record;
	},
	/**
	 * 获取所有组件属性信息
	 * @private
	 * @return {}
	 */
	getSouthRecords:function(){
		var me = this;
		var south = me.getComponent('south');
		var store = south.store;
		
		var records = [];
		store.each(function(record){
			records.push(record);
		});
		
		return records;
	},
	/**
	 * 获取所有组件属性信息（简单对象数组）
	 * @private
	 * @return {}
	 */
	getSouthRocordInfoArray:function(){
		var me = this;
		var records = me.getSouthRecords();
		var fields = me.getSouthStoreFields();
		
		var arr = [];
		//model转化成简单对象
		var getObjByRecord = function(record){
			var obj = {};
			Ext.Array.each(fields,function(field){
				obj[field.name] = record.get(field.name);
			});
			return obj;
		};
		//组装简单对象数组
		Ext.Array.each(records,function(record){
			var obj = getObjByRecord(record);
			arr.push(obj);
		});
		
		return arr;
	},
	/**
	 * 给组件属性列表赋值
	 * @private
	 * @param {} InteractionField
	 * @param {} key
	 * @param {} value
	 */
	setSouthRecordByKeyValue:function(InteractionField,key,value){
		var me = this;
		var store = me.getComponent('south').store;
		var record = store.findRecord('InteractionField',InteractionField);
		if(record != null){//存在
			record.set(key,value);
			record.commit();
		}
	},
	/**
	 * 给组件记录列表赋值
	 * @private
	 * @param {} array
	 */
	setSouthRecordByArray:function(array){
		var me = this;
		Ext.Array.each(array,function(obj){
			var rec = ('Ext.data.Model',obj);
			me.addSouthValueByRecord(rec);//添加组件记录
		});
	},
	/**
	 * 添加组件属性记录
	 * @private
	 * @param {} record
	 */
	addSouthValueByRecord:function(record){
		var me = this;
		var list = me.getComponent('south');//列属性列表
		var store = list.store;
		store.add(record);
	},
	/**
	 * 移除组件属性记录
	 * @private
	 * @param {} record
	 */
	removeSouthValueByRecord:function(record){
		var me = this;
		var list = me.getComponent('south');//列属性列表
		var store = list.store;
		store.remove(record);
	},
	/**
	 * 根据键值对移除组件属性信息
	 * @private
	 * @param {} key
	 * @param {} value
	 */
	removeSouthValueByKeyValue:function(key,value){
		var me = this;
		var store = me.getComponent('south').store;
		var record = me.getSouthRecordByKeyValue(key,value);
		if(record){
			store.remove(record);
		}
	},

	/**
	 * 前台需要的字段
	 * @private
	 * @return {}
	 */
	getGridFields:function(){
		var me =this;
		var southRecords = me.getSouthRecords();
		
		var fields = "";
		for(var i in southRecords){
			var record = southRecords[i];
			fields = fields + record.get('InteractionField') + ",";
		}
		if(fields.length > 0){
			fields = fields.substring(0,fields.length-1);
		}
		return fields;
	},

	
	/**
	 * 根据键值对去掉勾选的数据对象
	 * @private
	 * @param {} key
	 * @param {} value
	 */
	uncheckedObjectTreeNode:function(key,value){
		var me = this;
		var dataObject = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix).getComponent('dataObject');
		var treeStore = dataObject.getComponent('objectPropertyTree').store;
		var items = treeStore.lastOptions.node.childNodes[0].store;
		var record = items.findRecord(key,value);
		if(record != null){
			record.set('checked',false);
			record.commit();
		}
	},
	
	/**
	 * 获取组件属性列表Fields
	 * @private
	 * @return {}
	 */
	getSouthStoreFields:function(){
		var me = this;
		var fields = [
			{name:'DisplayName',type:'string'},//显示名称
	    	{name:'InteractionField',type:'string'},//交互字段
            {name:'IsLocked',type:'bool'},//默认锁定
            {name:'IsHidden',type:'bool'},//默认隐藏
            {name:'IsEditer',type:'bool'},//默认不可编辑
            {name:'CannotSee',type:'bool'},//不可见
            {name:'CanSort',type:'bool'},//可排序
            {name:'DefaultSort',type:'bool'},//默认排序
            {name:'SortType',type:'string'},//排序方式
            {name:'OrderNum',type:'int'},//排布顺序
            {name:'width',type:'int'},//列表头宽
            {name:'IsLocked',type:'bool'}//默认锁定

		];
		
		return fields;
	},
	
	/**
	 * 获取设计代码
	 * @private
	 * @return {}
	 */
	getAppParams:function(){
		var me = this;
		
		var panelParams = me.getPanelParams();
		var southParams = me.getSouthRocordInfoArray();
		
		var appParams = {
			panelParams:panelParams,
			southParams:southParams
		};
		
		return appParams;
	},
	/**
	 * 给设计代码赋值
	 * @private
	 */
	setAppParams:function(){
		var me = this;
		var objectPropertyTree =me.getobjectPropertyTree();
		var callback = function(appInfo){
			var appParams = Ext.JSON.decode(appInfo[me.fieldsObj.DesignCode]);
			var panelParams = appParams.panelParams;
			var southParams = appParams.southParams;
			me.DataTimeStamp = appInfo[me.fieldsObj.DataTimeStamp];
			
			objectPropertyTree.store.on({
	        	load:function(store,node,records,successful,e){
	        		if(me.appId != -1 && me.isJustOpen && node == objectPropertyTree.getRootNode()){
	        			//对象内容勾选
	        			me.changeObjChecked(southParams);
	        		}
	        	}
	       	});

	       	//赋值
	       	me.setSouthRecordByArray(southParams);//数据项列表赋值
	       	me.setObjData();//数据对象赋值
	       	me.setPanelParams(panelParams);//属性面板赋值
			
			//获取获取数据服务列表
			var getDataServerUrl =me.getDataServerUrl();
			getDataServerUrl.value = panelParams.getDataServerUrl;

			//获取修改数据服务列表
			var editDataServerUrl =me.getEditDataServerUrl();
			editDataServerUrl.value = panelParams.editDataServerUrl;
            
            var listorder=me.getValuePanelField();//传入指定的排序字段名
            listorder.value = panelParams.valuePanelField;

		};
		//从后台获取应用信息
		me.getAppInfoFromServer(me.appId,callback);
	},
	/**
	 * 获取列表的获取数据服务URL
	 * @private
	 * @return {}
	 */
	getListUrl:function(){
		var me = this;
		
		//列表配置参数
		var params = me.getPanelParams();
		//前台需要显示的字段
		var fields = me.getListFields();
		
		if(!fields){
			fields = "";
		}
		
		//数据服务地址
		var url = params.getDataServerUrl;
		
		if(url){
			url = url.split("?")[0];
			url = url + "?isPlanish=true&fields=" + fields;
		}else{
			url = "";
		}
		
		return url;
	},
	/**
	 * 前台需要的字段
	 * @private
	 * @return {}
	 */
	getListFields:function(){
		var me =this;
		
		var columnParams = me.getColumnParams();
		var fields = [];
		for(var i in columnParams){
			fields.push(columnParams[i].InteractionField);
		}
		return fields.toString();
	},
	/**
	 * 给数据对象列表赋值
	 * @private
	 */
	setObjData:function(){
		var me = this;
		//数据对象类
		var objectName =me.getobjectName();
		objectName.store.load();
	},
    /**
     * 给数据对象列表赋值
     * @private
     */
    setObjDataR:function(){
        var me = this;
        //数据对象类
        var objectName =me.getobjectNameR();
        objectName.store.load();
    },

	//=====================生成需要保存的代码=======================
    /**
     * 创建数据列
     * @private
     * @return {}
     */
    createColumnsStr:function(){
        var me = this;
        //列属性(已排序)
        var columnParams = me.getColumnParams();
        
        var columnsStr = "";
         var colEditer="";              
        for(var i in columnParams){
            //是否可编辑列
             if(columnParams[i].Editor)
              {
                 colEditer="editor:{allowBlank:false},";
              }
              else
              {
                colEditer="";
              }
            var col = 
            "{" + 
                "text:'" + columnParams[i].DisplayName + "'," + 
                "dataIndex:'" + columnParams[i].InteractionField + "'," + 
                "width:" + columnParams[i].width + ",";
                
                if(columnParams[i].IsLocked){
                    col += "locked:" + columnParams[i].IsLocked + ",";
                }
                if(columnParams[i].ColumnType == "bool"){
                    col += "xtype:'booleancolumn',";
                    col += "trueText:'是',";
                    col += "falseText:'否',";
                }
                
                col += "sortable:" + columnParams[i].CanSort + "," + 
                "hidden:" + (columnParams[i].IsHidden || columnParams[i].CannotSee) + "," + 
                "hideable:" + !columnParams[i].CannotSee + "," + 
                //"editor:{allowBlank:false},"+   //是否可以修改行或单元格
                "align:'" + columnParams[i].AlignType + "'" + 
            "}";
            columnsStr = columnsStr + col + ",";
        }

        if(columnsStr != ""){
            columnsStr = columnsStr.substring(0,columnsStr.length-1);
        }
        
        var cStr = "[";
        
        var params = me.getPanelParams();
        cStr += columnsStr + "]";
        return cStr;
    },
	/**
	 * 创建类代码
	 * @private
	 * @return {}
	 */
	createAppClass:function(){
		var me = this;
		//表单配置参数
        var objecName=me.getobjectName();
        if(objecName.getValue()===null ){
            Ext.Msg.alert('提示','请配置列表的数据对象');
            return ;
         }
        var record=me.getSouthRecordByKeyValue('InteractionField',objecName.getValue());
        
        var dataServerUrl=me.getDataServerUrl();
        if(dataServerUrl.getValue()===null ){
            Ext.Msg.alert('提示','请配置获取数据');
            return ;
         }
       
        //获取列表服务
        var tempSaveUrl=me.getEditDataServerUrl();
        if(tempSaveUrl.getValue()===null ){
            Ext.Msg.alert('提示','请配置修改数据服务');
            return ;
         }

		var params = me.getPanelParams();
        var modelField =[];
        var valueField =me.createColumnsStr();
        var listorder=me.getValuePanelField();//传入指定的排序字段名
        
		var appClass = 
		"Ext.define('" + params.appCode + "',{" + 
		    "extend:'Ext.zhifangux.SortList'," + 
			"alias:'widget." + params.appCode + "'," + 
			"title:'" + params.titleText + "'," + 
			"width:" + params.Width + "," + 
			"height:" + params.Height + "," + 
            
			"objectName:'" +objecName.getValue() +"'," + //需要更新数据的数据对象名称
			"internalWhere:'" + params.defaultParams + "'," + //内部hql
			"externalWhere:''," + //外部hql
			
			"autoSelect:true," + 
			"autoScroll:true," +
            "dataServerUrl:getRootPath()+"+"'/'+"+"'"+dataServerUrl.getValue().split("?")[0]+ "?isPlanish=true'," + //左列表获取后台数据服务地址
            "saveServerUrl:getRootPath()+"+"'/'+"+"'"+tempSaveUrl.getValue().split("?")[0]+ "'," +//保存服务地址
            "listorder:'" +listorder.getValue() +"'," + //传入指定的排序字段名
            //"saveType:"+record.get('saveType')+ "," +//多选方式(列表数据是否允许多选)true:允许多选;false:不允许多选
            "modelField:[]," +//列表的model的Field
            "valueField:"+valueField.replace(/"/g,"'")+ "," ;//数据列表值字段,可以是外面做好数据适配后传进来
      
            appClass=appClass+"afterRender:function(){" + 
                "var me=this;" + 
                "me.callParent(arguments);" + 
                //加载数据后默认选中第一行
	
                "if(Ext.typeOf(me.callback)=='function'){me.callback(me);}" + 
            "}," ;
            appClass=appClass+
			"initComponent:function(){" + 
				"var me=this;" + 
                //"me.listeners=me.listeners||[];" + 

			"this.callParent(arguments);" + 
			"}" + 
		"});";
		return appClass;
	},

	//=====================后台获取&存储=======================
	/**
	 * 从后台获取应用信息
	 * @private
	 * @param {} id
	 * @param {} callback
	 */
	getAppInfoFromServer:function(id,callback){
		var me = this;
		
		if(id && id != -1){
			var url = me.getAppInfoServerUrl + "?isPlanish=true&id=" + id;
			Ext.Ajax.defaultPostHeader = 'application/json';
			Ext.Ajax.request({
				async:false,//非异步
				url:url,
				method:'GET',
				timeout:2000,
				success:function(response,opts){
					var result = Ext.JSON.decode(response.responseText);
					if(result.success){
						if(result.ResultDataValue && result.ResultDataValue != ""){
							var appInfo = Ext.JSON.decode(result.ResultDataValue);
							if(Ext.typeOf(callback) == "function"){
								callback(appInfo);//回调函数
							}
						}else{
							Ext.Msg.alert('提示','没有获取到应用信息！');
						}
					}else{
						Ext.Msg.alert('提示','获取应用信息失败！错误信息【<b style="color:red">'+ result.ErrorInfo +"</b>】");
					}
				},
				failure : function(response,options){ 
					Ext.Msg.alert('提示','获取应用信息请求失败！');
				}
			});
		}
	},
	/**
	 * 将构建结果保存到数据库中
	 * @private
	 * @param {} obj
	 */
	saveToServer:function(obj,callback){
		var me = this;
		var url = "";
		if(obj.Id != -1){
			url = me.editServerUrl;//修改
		}else{
			url = me.addServerUrl;//新增
		}
		Ext.Ajax.defaultPostHeader = 'application/x-www-form-urlencoded';
		Ext.Ajax.request({
			async:false,//非异步
			url:url,
			params:obj,
			method:'POST',
			timeout:2000,
			success:function(response,opts){
				var result = Ext.JSON.decode(response.responseText);
				if(result.success){
					if(Ext.typeOf(callback) == "function"){
						callback();//回调函数
					}
				}else{
					Ext.Msg.alert('提示','保存应用组件失败！错误信息【<b style="color:red">'+ result.ErrorInfo +"</b>】");
				}
			},
			failure : function(response,options){ 
				Ext.Msg.alert('提示','保存应用组件请求失败！');
			}
		});
	},
    
	//=====================公共方法代码=======================
	/**
	 * 将JSON对象转化成字符串
	 * @private
	 * @param {} obj
	 * @return {}
	 */
	JsonToStr:function(obj){
		var str = Ext.JSON.encode(obj);
		str = str.replace(/\\/g,"\\\\");
		str = str.replace(/\"/g,"\\\"");
	    return str;
	},
	/**
	 * 数据适配
	 * @private
	 * @param {} response
	 * @return {}
	 */
	changeStoreData: function(response){
    	var data = Ext.JSON.decode(response.responseText);
    	var ResultDataValue = [];
    	if(data.ResultDataValue && data.ResultDataValue != ""){
    		ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
    	}
    	data.ResultDataValue = ResultDataValue;
    	response.responseText = Ext.JSON.encode(data);
    	return response;
  	},
   
    /**
     * 删除展示区域表单中的图表组件
     * @private
     * @param {} componentItemId
     */
    removeComponent:function(componentItemId){
        var me = this;
        //删除数据项组件
        var center = me.getCenterCom();
        center.remove(componentItemId);
        //删除数据项属性面板
        me.getComponent('east').remove(componentItemId + me.ParamsPanelItemIdSuffix);
        me.switchParamsPanel('center');
        
        //删除数据项属性列表中的当前数据项数据
        me.removeSouthValueByKeyValue('InteractionField',componentItemId);
        //去掉勾选
        me.uncheckedObjectTreeNode('InteractionField',componentItemId);
    },
    /**
     * 删除展示区域表单中的图表组件
     * @private
     * @param {} componentItemId
     */
    removeCenterComponent:function(componentItemId){
        var me = this;
        //删除数据项组件
        var center = me.getCenterCom();
        center.remove(componentItemId);
        //去掉勾选
        me.uncheckedObjectTreeNode('InteractionField',componentItemId);
    },
	/**
	 * 序号列
	 * @private
	 * @param {} grid
	 */
	setRowNumberColumn:function(grid){
		var me = this;
		var params = me.getPanelParams();
		var cloumns = [];
		
		if(params.hasRowNumber){
			cloumns.push({
				xtype:'rownumberer',
				text:'序号',
				width:35,
				align:'center'
			});
		}
		grid.columns = cloumns.concat(grid.columns);
	}
});