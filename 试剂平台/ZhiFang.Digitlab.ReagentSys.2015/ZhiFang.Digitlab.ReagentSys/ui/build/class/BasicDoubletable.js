﻿/**
 * 普通双表数据移动构建工具
 * 
 */
Ext.ns('Ext.build');
Ext.define('Ext.build.BasicDoubletable',{
	extend:'Ext.panel.Panel',
	alias: 'widget.basicdoubletable',
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
	buildTitle:'普通双表数据移动构建工具',
    
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
	/**
     * 删除服务列表显示属性
     * @type String
     */
    delServerDisplayField:'CName',
    /**
     * 删除服务列表值属性
     * @type String
     */
    delServerValueField:'ServerUrl',
    /**
     * 删除服务列表地址
     * @type String
     */
    delServerUrl:getRootPath()+'/ConstructionService.svc/CS_BA_SearchReturnEntityServiceListByEntityName',
    /**
     * 删除服务列表字段数组
     * @type String
     */
    delServerFields:['CName','ServerUrl'],
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
    defaultPanelWidth:680,
    /**
     * 列表默认高度
     * @type Number
     */
    defaultPanelHeight:280,
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
        var form={
                xtype:'form',
                title:'双表数据移动',
                itemId:'center',
                border:0,
                autoScroll:true,
                bodyPadding:'1 10 10 1',
                width:me.defaultPanelWidth+20,
                height:me.defaultPanelHeight+20,
                resizable:{handles:'s e'}
            };
        var com = {
            xtype:'panel',
            title:'',
            bodyPadding:'2 10 10 2',
            autoScroll:true,
            items:form
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
				{text:'交互字段',dataIndex:'InteractionField',width:135,disabled:true,editor:{readOnly:true}},
				{text:'显示名称',dataIndex:'DisplayName',width:135,
                    editor:{
                        allowBlank:false,
                        listeners:{
                            change:function(com,newValue){
                            }
                        }
                    }
                },
                {text:'所属列表',dataIndex:'Appertain',disabled:true,hidden:false},
                
                {text:'列宽',dataIndex:'Width',width:50,align:'center',
                    xtype:'numbercolumn',
                    format:'0',
                    editor:{
                        xtype:'numberfield',
                        allowBlank:false,
                        minValue:1,
                        maxValue:999
                    }
                },
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
		//右数据对象
		var dataObjR = me.createDataObjR();
		
		//表单宽高
		var panelWH = me.createWidthHieght();
		//其他设置
        var other = me.createOther();
        
		var listParamsPanel = {
			xtype:'form',
			itemId:'center' + me.ParamsPanelItemIdSuffix,
			title:'列表属性配置',
			header:false,
			autoScroll:true,
			border:false,
	        bodyPadding:5,
	        items:[appInfo,title,dataObj,dataObjR,panelWH,other]
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
     * 属性设置
     * @private
     * @return {}
     */
    createOther:function(){
        var me = this;
        var com = {
            xtype:'fieldset',title:'属性设置',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',defaults:{anchor:'100%'},layout:'anchor',
            itemId:'other',
            items:[
            {
                xtype: 'radiogroup',
                itemId:'selectType',
                labelWidth:95,
                fieldLabel:'选择类型',
                columns:2,
                vertical:true,
                listeners:{
                },
                items:[
                    {boxLabel:'多选',name:'selectType',inputValue:'false'},
                    {boxLabel:'单选',name:'selectType',inputValue:'true'}
                ]
            },
            {
                xtype: 'radiogroup',
                itemId:'btnLeft',
                labelWidth:95,
                fieldLabel:'左移按钮',
                columns:2,
                vertical:true,
                listeners:{
                },
                items:[
                    {boxLabel:'显示',name:'btnLeft',inputValue:'false'},
                    {boxLabel:'隐藏',name:'btnLeft',inputValue:'true'}
                ]
            },
            {
                xtype: 'radiogroup',
                itemId:'btnAllLeft',
                labelWidth:95,
                fieldLabel:'全部左移',
                columns:2,
                vertical:true,
                listeners:{
                },
                items:[
                    {boxLabel:'显示',name:'btnAllLeft',inputValue:'false'},
                    {boxLabel:'隐藏',name:'btnAllLeft',inputValue:'true'}
                ]
            },
            {
                xtype: 'radiogroup',
                itemId:'btnRight',
                labelWidth:95,
                fieldLabel:'右移',
                columns:2,
                vertical:true,
                listeners:{
                },
                items:[
                    {boxLabel:'显示',name:'btnRight',inputValue:'false'},
                    {boxLabel:'隐藏',name:'btnRight',inputValue:'true'}
                ]
            },
            {
                xtype: 'radiogroup',
                itemId:'btnAllRight',
                labelWidth:95,
                fieldLabel:'全部右移',
                columns:2,
                vertical:true,
                listeners:{
                },
                items:[
                    {boxLabel:'显示',name:'btnAllRight',inputValue:'false'},
                    {boxLabel:'隐藏',name:'btnAllRight',inputValue:'true'}
                ]
            },
            {
                xtype: 'radiogroup',
                itemId:'filterLeft',
                labelWidth:95,
                fieldLabel:'左过滤栏',
                columns:2,
                vertical:true,
                listeners:{
                },
                items:[
                    {boxLabel:'显示',name:'filterLeft',inputValue:'false'},
                    {boxLabel:'隐藏',name:'filterLeft',inputValue:'true'}
                ]
            },
            {
                xtype: 'radiogroup',
                itemId:'filterRight',
                labelWidth:95,
                fieldLabel:'右过滤栏',
                columns:2,
                vertical:true,
                listeners:{
                },
                items:[
                    {boxLabel:'显示',name:'filterRight',inputValue:'false'},
                    {boxLabel:'隐藏',name:'filterRight',inputValue:'true'}
                ]
            },
            {
                xtype: 'radiogroup',
                itemId:'fieldSetLeft',
                labelWidth:95,
                fieldLabel:'左最外层',
                columns:2,
                vertical:true,
                listeners:{
                },
                items:[
                    {boxLabel:'显示',name:'fieldSetLeft',inputValue:'false'},
                    {boxLabel:'隐藏',name:'fieldSetLeft',inputValue:'true'}
                ]
            },
            {
                xtype: 'radiogroup',
                itemId:'fieldSetRight',
                labelWidth:95,
                fieldLabel:'右最外层',
                columns:2,
                vertical:true,
                listeners:{
                },
                items:[
                    {boxLabel:'显示',name:'fieldSetRight',inputValue:'false'},
                    {boxLabel:'隐藏',name:'fieldSetRight',inputValue:'true'}
                ]
            },
            {
                xtype: 'radiogroup',
                itemId:'btnHidden',
                labelWidth:95,
                fieldLabel:'确定/取消按钮',
                columns:2,
                vertical:true,
                listeners:{
                },
                items:[
                    {boxLabel:'显示',name:'btnHidden',inputValue:'false'},
                    {boxLabel:'隐藏',name:'btnHidden',inputValue:'true'}
                ]
            }
            ]
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
	 * 左数据对象
	 * @private
	 * @return {}
	 */
	createDataObj:function(){
		var me = this;
		var com = {
	    	xtype:'fieldset',title:'左列表配置',padding:'0 5 0 5',collapsible:true,
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
                    select:function(owner,records,eOpts){
                        me.setBasicParamsPanelValues();
                        me.setWH();
                    },
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
	        },
             {
                xtype:'combobox',fieldLabel:'关系对象',
                itemId:'relationObjectName',name:'relationObjectName',
                labelWidth:60,anchor:'100%',
                editable:true,typeAhead:true,
                forceSelection:true,
                queryMode:'local',
                labelStyle:"font-weight:bold;font-size:11px;font-family:SimHei;color:#FF0000",
                emptyText:'请选择关系表一的数据对象',
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
                    select:function(owner,records,eOpts){

                    },
                    change:function(owner,newValue,oldValue,eOpts){

                    }
                }
            },{
	        	xtype:'combobox',fieldLabel:'获取数据',
                labelStyle:"font-weight:bold;font-size:11px;font-family:SimHei;color:#FF0000",
	        	itemId:'getDataServerUrl',name:'getDataServerUrl',
	        	labelWidth:60,anchor:'100%',
	        	editable:true,typeAhead:true,
                forceSelection:true,
                queryMode:'local',
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
	        },{xtype:'combobox',fieldLabel:'新增数据',
                labelStyle:"font-weight:bold;font-size:11px;font-family:SimHei;color:#FF0000",
                itemId:'editDataServerUrl',name:'editDataServerUrl',
                labelWidth:60,anchor:'100%',
                editable:true,typeAhead:true,
                forceSelection:true,
                queryMode:'local',mode:'local',
                emptyText:'请选择新增数据服务',
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
            xtype:'combobox',
            itemId:'delServerUrl',
            labelWidth:60,
            fieldLabel:'删除数据',
            anchor:'100%',
            labelStyle:"font-weight:bold;font-size:11px;font-family:SimHei;color:#FF0000",
            editable:true,typeAhead:true,
            forceSelection:true,
            queryMode:'local',mode:'local',
            emptyText:'请选择删除数据服务',
            displayField:me.delServerDisplayField,
            valueField:me.delServerValueField,
            name:'delServerUrl',
            store:Ext.create('Ext.data.Store',{
                fields:me.delServerFields,
                proxy:{
                    type:'ajax',
                    url:me.delServerUrl+"?EntityName=Bool",
                    reader:{type:'json',root:'ResultDataValue'},
                    extractResponseData:me.changeStoreData
                },autoLoad:true
            })
        },{xtype:'combobox',fieldLabel:'删除字段',
                itemId:'leftPrimaryKey',
                name:'leftPrimaryKey',
                labelWidth:60,anchor:'100%',
                labelStyle:"font-weight:bold;font-size:11px;font-family:SimHei;color:#FF0000",
                editable:false,typeAhead:true,
                forceSelection:true,mode:'local',
                emptyText:'请选择删除左列表的主键字段',
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
                    select:function(owner,records,eOpts){}
                }
             },
             
             {
	            xtype:'fieldcontainer',layout:'hbox',itemId:'leftFilterField-label',
	            items:[{
	                xtype:'label',text:'左列表加载数据的过滤字段',toolstip:'指定加载左列表数据的过滤字段',
                    labelStyle:"font-weight:bold;font-size:11px;font-family:SimHei;color:#FF0000",
	                itemId:'leftFilterField-text',name:'leftFilterField-text'
	            }]
             },
             
             {
                xtype:'fieldcontainer',layout:'hbox',itemId:'newleftFilterField',
                items:[{
                    xtype:'textfield',emptyText:'过滤字段',
                    width:185,readOnly:true,appComID:'',//过滤字段id属性
                    itemId:'newleftFilterField-text',name:'newleftFilterField-text',fieldLabel:'过滤字段',labelWidth:60
                },{
                    xtype:'button',iconCls:'build-button-configuration-blue',
                    tooltip:'过滤字段',margin:'0 0 0 2',readOnly:true,
                    itemId:'newleftFilterField-button',name:'newleftFilterField-button',
                    handler: function(){
                        me.openTreeWin('newleftFilterField-text');
                    }
                }]
            },{
                xtype:'fieldcontainer',layout:'hbox',itemId:'leftMatchField-label',
                items:[{
                    xtype:'label',text:'左列表与右列表的匹配字段',
                    labelStyle:"font-weight:bold;font-size:11px;font-family:SimHei;color:#FF0000",
                    itemId:'leftMatchField-text',name:'leftMatchField-text'
                }]
             },
              
             {
                xtype:'fieldcontainer',layout:'hbox',itemId:'newleftMatchField',
                items:[{
                    xtype:'textfield',emptyText:'匹配字段',readOnly:true,
                    labelStyle:"font-weight:bold;font-size:11px;font-family:SimHei;color:#FF0000",
                    width:185,readOnly:true,appComID:'',//过滤字段id属性
                    itemId:'newleftMatchField-text',name:'newleftMatchField-text',fieldLabel:'匹配字段',labelWidth:60  
                },{
                    xtype:'button',iconCls:'build-button-configuration-blue',
                    tooltip:'匹配字段',margin:'0 0 0 2',
                    itemId:'newleftMatchField-button',name:'newleftMatchField-button',
                    handler: function(){
                        me.openTreeWin('newleftMatchField-text');
                    }
                }]
            },{
                xtype:'textfield',fieldLabel:'容器标题',labelWidth:60,value:'',
                itemId:'leftFieldsetTitle',name:'leftFieldsetTitle'
            }]
	    };
		return com;
	},
	/**
     * 弹出树选择窗口
     * @private
     */
    openTreeWin:function(type){
        var me = this;
        var objectName=me.getobjectNameValue();
        if(objectName==''||objectName==null||objectName==undefined){
            Ext.Msg.alert('提示','请先选择左列表的数据对象');
            return false;
        }
        var maxHeight = document.body.clientHeight*0.98;
        var maxWidth = document.body.clientWidth*0.58;
        
        var tree={
            autoScroll:true,
            width:365,
            height:380,
            xtype:'treepanel',
            itemId:'treepanelTreeWin',
            border:false,
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
                },
                itemdblclick:function(com, record,  item,  index,  e, eOpts ){
	                var id = record.data.InteractionField;
	                if(id && id != ""){
	                  if(type=='newleftFilterField-text'){
	                       var com=me.getnewleftFilterFieldText();
	                       com.setValue(id);
                        }
                     if(type=='newleftMatchField-text'){
                           var com=me.getnewleftMatchFieldText();
                           com.setValue(id);
                        }
                       win.close();
	                }else{
	                    Ext.Msg.alert('提示','选择字段不能为空！');
	                }
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
                            var treepanelTreeWin =me.getobjectPropertyTree(); 
                            if(treepanelTreeWin.nodeClassName != ""){
                                data[me.objectRootProperty] = children;
                            }else{
                                data[me.objectRootProperty] = [{
                                    text:treepanelTreeWin.CName,
                                    InteractionField:treepanelTreeWin.ClassName,
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
            
        };
        
        tree.store.proxy.url = me.objectPropertyUrl + "?" + me.objectPropertyParam + "=" + objectName;
        tree.store.load();
        
        var win = Ext.create('Ext.form.Panel',{
            maxWidth:maxWidth,
            autoScroll:true,
            modal:true,//模态
            floating:true,//漂浮
            closable:true,//有关闭按钮
            draggable:true,//可移动
            width:375,
            height:385,
            title:'字段选择',
            bodyPadding:'0 5 0 5',
            layout:'fit',
            items:tree
        });
        
        if(win.height > maxHeight){
            win.height = maxHeight;
        }
        
        win.show();
    },
	
	/**
	 * 右列表配置:数据对象
	 * @private
	 * @return {}
	 */
	createDataObjR:function(){
		var me = this;
		var com = {
	    	xtype:'fieldset',title:'右列表配置',padding:'0 5 0 5',collapsible:true,
	        defaultType:'textfield',defaults:{anchor:'100%'},layout:'anchor',
            labelStyle:"font-weight:bold;font-size:11px;font-family:SimHei;color:#FF0000",
	        itemId:'dataObjectR',name:'dataObjectR',
	        items:[{
				xtype:'combobox',fieldLabel:'数据对象',
	        	itemId:'objectNameR',name:'objectNameR',
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
							me.objectChangeR(owner,newValue);
						}
					}
				}
	        },{
	        	xtype:'treepanel',name:'objectPropertyTreeR',
                itemId:'objectPropertyTreeR',border:false,
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
								var objectPropertyTree =me.getobjectPropertyTreeR();	
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
	        },{
	        	xtype:'combobox',fieldLabel:'获取数据',
                labelStyle:"font-weight:bold;font-size:11px;font-family:SimHei;color:#FF0000",
	        	itemId:'getDataServerUrlR',
                name:'getDataServerUrlR',
	        	labelWidth:60,anchor:'100%',
	        	editable:true,typeAhead:true,
                forceSelection:true,
                queryMode:'local',mode:'local',
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
							var objectName = me.getobjectNameR();
				    		store.proxy.url = me.objectGetDataServerUrl + "?" + me.objectServerParam + "=List" + objectName.getValue();
				    		
				    	}
				    }
				})
	        },{xtype:'combobox',fieldLabel:'主键字段',
                itemId:'rightPrimaryKey',
                name:'rightPrimaryKey',
                labelWidth:55,anchor:'100%',
                labelStyle:"font-weight:bold;font-size:11px;font-family:SimHei;color:#FF0000",
                editable:true,typeAhead:true,
                forceSelection:true,
                queryMode:'local',mode:'local',
                emptyText:'请选择右列表主键字段',
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
                    select:function(owner,records,eOpts){}
                }
             },{
	        	xtype:'textfield',fieldLabel:'默认条件',labelWidth:60,value:'',
	        	itemId:'defaultParamsR',name:'defaultParamsR',hidden:true
	        },{
                xtype:'textfield',fieldLabel:'容器标题',labelWidth:60,value:'',
                itemId:'rightFieldsetTitle',name:'rightFieldsetTitle'
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
    //#############################属性区域#############################
    //属性区域
    getother:function(){
        var me=this;
        var east = me.getComponent('east').getComponent('center'+me.ParamsPanelItemIdSuffix);
        var other = east.getComponent('other');
        return other;
    },
    /***
     * 左移按钮
     * @return {}
     */
    getbtnLeft:function(){
        var me=this;
        var other = me.getother();
        var btnLeft = other.getComponent('btnLeft');
        return btnLeft;
    },
    /***
     * 左移按钮
     * @return {}
     */
    getbtnLeftValue:function(){
        var me=this;
        var other = me.getother();
        var value = other.getComponent('btnLeft').getValue().btnLeft;
        if(value=='true'||value=='1'||value==true||value=='on'){
            value =true
        }else{
            value =false
        }
        return value;
    },
    /***
     * 全左移移按钮
     * @return {}
     */
    getbtnAllLeft:function(){
        var me=this;
        var other = me.getother();
        var btnAllLeft = other.getComponent('btnAllLeft');
        return btnAllLeft;
    },
    /***
     * 全左移移按钮
     * @return {}
     */
    getbtnAllLeftValue:function(){
        var me=this;
        var other = me.getother();
        var value = other.getComponent('btnAllLeft').getValue().btnAllLeft;
        if(value=='true'||value=='1'||value==true||value=='on'){
            value =true
        }else{
            value =false
        }
        return value;
    },
    /***
     * 右移按钮
     * @return {}
     */
    getbtnRight:function(){
        var me=this;
        var other = me.getother();
        var btnRight = other.getComponent('btnRight');
        return btnRight;
    },
    /***
     * 右移按钮
     * @return {}
     */
    getbtnRightValue:function(){
        var me=this;
        var other = me.getother();
        var value = other.getComponent('btnRight').getValue().btnRight;
        if(value=='true'||value=='1'||value==true||value=='on'){
            value =true
        }else{
            value =false
        }
        return value;
    },
    /***
     * 右移按钮
     * @return {}
     */
    getbtnAllRight:function(){
        var me=this;
        var other = me.getother();
        var btnAllRight = other.getComponent('btnAllRight');
        return btnAllRight;
    },
    /***
     * 右移按钮
     * @return {}
     */
    getbtnAllRightValue:function(){
        var me=this;
        var other = me.getother();
        var value = other.getComponent('btnAllRight').getValue().btnAllRight;
        if(value=='true'||value=='1'||value==true||value=='on'){
            value =true
        }else{
            value =false
        }
        return value;
    },
    /***
     * 控制(确定/取消按钮)
     * @return {}
     */
    getbtnHidden:function(){
        var me=this;
        var other = me.getother();
        var btnHidden = other.getComponent('btnHidden');
        return btnHidden;
    },
    /***
     * 控制(确定/取消按钮)
     * @return {}
     */
    getbtnHiddenValue:function(){
        var me=this;
        var other = me.getother();
        var value = other.getComponent('btnHidden').getValue().btnHidden;
        if(value=='true'||value=='1'||value==true||value=='on'){
            value =true
        }else{
            value =false
        }
        return value;
    },
    /***
     * 选择类型
     * @return {}
     */
    getselectType:function(){
        var me=this;
        var other = me.getother();
        var selectType = other.getComponent('selectType');
        return selectType;
    },
    /***
     * 选择类型
     * @return {}
     */
    getselectTypeValue:function(){
        var me=this;
        var other = me.getother();
        var value = other.getComponent('selectType').getValue().selectType;
        if(value=='true'||value=='1'||value==true||value=='on'){
            value =true
        }else{
            value =false
        }
        return value;
    },
    /***
     * 控制左面板过滤栏的显示
     * @return {}
     */
    getfilterLeft:function(){
        var me=this;
        var other = me.getother();
        var filterLeft = other.getComponent('filterLeft');
        return filterLeft;
    },
    /***
     * 控制左面板过滤栏的显示
     * @return {}
     */
    getfilterLeftValue:function(){
        var me=this;
        var other = me.getother();
        var value = other.getComponent('filterLeft').getValue().filterLeft;
        if(value=='true'||value=='1'||value==true||value=='on'){
            value =true
        }else{
            value =false
        }
        return value;
    },
    /***
     * 控制右面板过滤栏的显示
     * @return {}
     */
    getfilterRight:function(){
        var me=this;
        var other = me.getother();
        var filterRight = other.getComponent('filterRight');
        return filterRight;
    },
    /***
     * 控制右面板过滤栏的显示
     * @return {}
     */
    getfilterRightValue:function(){
        var me=this;
        var other = me.getother();
        var value = other.getComponent('filterRight').getValue().filterRight;
        if(value=='true'||value=='1'||value==true||value=='on'){
            value =true
        }else{
            value =false
        }
        return value;
    },
    /***
     * 左列表最外层的显示
     * @return {}
     */
    getfieldSetLeft:function(){
        var me=this;
        var other = me.getother();
        var fieldSetLeft = other.getComponent('fieldSetLeft');
        return fieldSetLeft;
    },
    /***
     * 左列表最外层的显示
     * @return {}
     */
    getfieldSetLeftValue:function(){
        var me=this;
        var other = me.getother();
        var value = other.getComponent('fieldSetLeft').getValue().fieldSetLeft;
        if(value=='true'||value=='1'||value==true||value=='on'){
            value =true
        }else{
            value =false
        }
        return value;
    },
    /***
     * 右列表最外层的显示
     * @return {}
     */
    getfieldSetRight:function(){
        var me=this;
        var other = me.getother();
        var fieldSetRight = other.getComponent('fieldSetRight');
        return fieldSetRight;
    },
    /***
     * 右列表最外层的显示
     * @return {}
     */
    getfieldSetRightValue:function(){
        var me=this;
        var other = me.getother();
        var value = other.getComponent('fieldSetRight').getValue().fieldSetRight;
        if(value=='true'||value=='1'||value==true||value=='on'){
            value =true
        }else{
            value =false
        }
        return value;
    },
    //#############################属性区域#############################
    //左列表配置
    getwidthValue:function(){
        var me=this;
        var east = me.getComponent('east').getComponent('center'+me.ParamsPanelItemIdSuffix);
        var value = east.getComponent('WH').getComponent('Width').getValue();
        return value;
    },
    getwidthCom:function(){
        var me=this;
        var east = me.getComponent('east').getComponent('center'+me.ParamsPanelItemIdSuffix);
        var width = east.getComponent('WH').getComponent('Width');
        return width;
    },
    //左列表配置
    getheightValue:function(){
        var me=this;
        var east = me.getComponent('east').getComponent('center'+me.ParamsPanelItemIdSuffix);
        var value = east.getComponent('WH').getComponent('Height').getValue();
        return value;
    },
    //左列表配置
    getheightCom:function(){
        var me=this;
        var east = me.getComponent('east').getComponent('center'+me.ParamsPanelItemIdSuffix);
        var height = east.getComponent('WH').getComponent('Height');
        return height;
    },
    //左列表配置
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
    getobjectNameValue:function(){
        var me=this;
        var dataObject = me.getDataObject();
        var value = dataObject.getComponent('objectName').getValue();
        return value;                          
    },
    gettreepanelTreeWin:function(){
        var me=this;
        var reepanelTreeWin = me.getComponent('reepanelTreeWin');
        return reepanelTreeWin;                          
    },
    getrelationObjectName:function(){
        var me=this;
        var dataObject = me.getDataObject();
        var relationObjectName = dataObject.getComponent('relationObjectName');
        return relationObjectName;                          
    },
    getrelationObjectNameValue:function(){
        var me=this;
        var dataObject = me.getDataObject();
        var value = dataObject.getComponent('relationObjectName').getValue();
        return value;                          
    },
    getleftFieldsetTitle:function(){
        var me=this;
        var dataObject = me.getDataObject();
        var leftFieldsetTitle = dataObject.getComponent('leftFieldsetTitle');
        return leftFieldsetTitle;                          
    },
    getobjectPropertyTree:function(){
        var me=this;
        var dataObject = me.getDataObject();
        var objectPropertyTree = dataObject.getComponent('objectPropertyTree');
        return objectPropertyTree;                          
    },
    /***
     * 左列表的外部传入的过滤字段树
     * 
     * @return {}
     */
    getnewleftFilterFieldText:function(){
        var me=this;
        var dataObject = me.getDataObject();
        var newleftFilterField = dataObject.getComponent('newleftFilterField');
        var text = newleftFilterField.getComponent('newleftFilterField-text');
        return text;                          
    },
    /***
     * 左列表的外部传入的过滤字段树
     * 
     * @return {}
     */
    getnewleftFilterFieldTextValue:function(){
        var me=this;
        var dataObject = me.getDataObject();
        var newleftFilterField = dataObject.getComponent('newleftFilterField');
        var text = newleftFilterField.getComponent('newleftFilterField-text').getValue();
        return text;                          
    },
    /***
     * 左列表与右列表的匹配字段
     * @return {}
     */
    getnewleftMatchFieldText:function(){
        var me=this;
        var dataObject = me.getDataObject();
        var newleftMatchField = dataObject.getComponent('newleftMatchField');
        var text = newleftMatchField.getComponent('newleftMatchField-text');
        return text;                          
    },
    /***
     * 左列表与右列表的匹配字段
     * @return {}
     */
    getnewleftMatchFieldTextValue:function(){
        var me=this;
        var dataObject = me.getDataObject();
        var newleftMatchField = dataObject.getComponent('newleftMatchField');
        var text = newleftMatchField.getComponent('newleftMatchField-text').getValue();
        return text;                          
    },
    /***
     * 左列表的获取数据服务
     * @return {}
     */
    getDataServerUrl:function(){
        var me=this;
        var dataObject = me.getDataObject();
        var getDataServerUrl = dataObject.getComponent('getDataServerUrl');
        return getDataServerUrl;                          
    },
    /***
     * 左列表的获取数据服务
     * @return {}
     */
    getDataServerUrlValue:function(){
        var me=this;
        var dataObject = me.getDataObject();
        var value = dataObject.getComponent('getDataServerUrl').getValue();
        return value;                          
    },
    /***
     * 左列表的新增数据服务
     * @return {}
     */
    getEditDataServerUrl:function(){
        var me=this;
        var dataObject = me.getDataObject();
        var editDataServerUrl = dataObject.getComponent('editDataServerUrl');
        return editDataServerUrl;                          
    },
    /***
     * 左列表的新增数据服务
     * @return {}
     */
    getEditDataServerUrlValue:function(){
        var me=this;
        var dataObject = me.getDataObject();
        var value = dataObject.getComponent('editDataServerUrl').getValue();
        return value;                          
    },
    /***
     * 左列表的删除数据服务
     * @return {}
     */
    getdelServerUrl:function(){
        var me=this;
        var dataObject = me.getDataObject();
        var delServerUrl = dataObject.getComponent('delServerUrl');
        return delServerUrl;                          
    },
    /***
     * 左列表的删除数据服务
     * @return {}
     */
    getdelServerUrlValue:function(){
        var me=this;
        var dataObject = me.getDataObject();
        var value = dataObject.getComponent('delServerUrl').getValue();
        return value;                          
    },
    /***
     * 左列表的字段主键id
     * @return {}
     */
    getleftPrimaryKey:function(){
        var me=this;
        var dataObject = me.getDataObject();
        var leftPrimaryKey = dataObject.getComponent('leftPrimaryKey');
        return leftPrimaryKey;                          
    },
    /***
     * 左列表的字段主键id
     * @return {}
     */
    getleftPrimaryKeyValue:function(){
        var me=this;
        var dataObject = me.getDataObject();
        var value = dataObject.getComponent('leftPrimaryKey').getValue();
        return value;                          
    },
    /***
     * 右列表的字段主键id
     * @return {}
     */
    getrightPrimaryKey:function(){
        var me=this;
        var dataObject = me.getDataObjectR();
        var rightPrimaryKey = dataObject.getComponent('rightPrimaryKey');
        return rightPrimaryKey;                          
    },
    /***
     * 右列表的字段主键id
     * @return {}
     */
    getrightPrimaryKeyValue:function(){
        var me=this;
        var dataObject = me.getDataObjectR();
        var value = dataObject.getComponent('rightPrimaryKey').getValue();
        return value;                          
    },
    /***
     * 
     * @return {}
     */
    getDefaultParams:function(){
        var me=this;
        var dataObject = me.getDataObject();
        var defaultParams = dataObject.getComponent('defaultParams');
        return defaultParams;                          
    },
    //右列表配置
	getDataObjectR:function(){
	    var me=this;
	    var east = me.getComponent('east').getComponent('center'+me.ParamsPanelItemIdSuffix);
	    var dataObject = east.getComponent('dataObjectR');
	    return dataObject;
    },
    /***
     * 右列表的数据对象
     * @return {}
     */
    getobjectNameR:function(){
        var me=this;
        var dataObjectR = me.getDataObjectR();
        var objectNameR = dataObjectR.getComponent('objectNameR');
        return objectNameR;                          
    },
    /***
     * 右列表的数据对象
     * @return {}
     */
    getobjectNameRValue:function(){
        var me=this;
        var dataObjectR = me.getDataObjectR();
        var value = dataObjectR.getComponent('objectNameR').getValue();
        return value;                          
    },
    getobjectPropertyTreeR:function(){
	    var me=this;
	    var dataObjectR = me.getDataObjectR();
	    var objectPropertyTree = dataObjectR.getComponent('objectPropertyTreeR');
	    return objectPropertyTree;                          
    },
    getrightFieldsetTitle:function(){
        var me=this;
        var dataObjectR = me.getDataObjectR();
        var rightFieldsetTitle = dataObjectR.getComponent('rightFieldsetTitle');
        return rightFieldsetTitle;                          
    },
    /***
     * 右列表的获取数据服务
     * @return {}
     */
    getDataServerUrlR:function(){
        var me=this;
        var dataObjectR = me.getDataObjectR();
        var getDataServerUrlR = dataObjectR.getComponent('getDataServerUrlR');
        return getDataServerUrlR;                          
    },
    /***
     * 右列表的获取数据服务
     * @return {}
     */
    getDataServerUrlRValue:function(){
        var me=this;
        var dataObjectR = me.getDataObjectR();
        var value = dataObjectR.getComponent('getDataServerUrlR').getValue();
        return value;                          
    },
    getDefaultParamsR:function(){
        var me=this;
        var dataObjectR = me.getDataObjectR();
        var defaultParamsR = dataObjectR.getComponent('defaultParamsR');
        return defaultParamsR;                          
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
	            itemId:'Width',name:'Width'
	        },{
				xtype:'numberfield',fieldLabel:'高度',labelWidth:60,anchor:'100%',
	            itemId:'Height',name:'Height'
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
        var title = "双表数据构建";
        var form = {
            xtype:'panel',
            itemId:'center',
            layout:'absolute',
            autoScroll:true,
            title:title,
            width:me.defaultPanelWidth+20,
            height:me.defaultPanelHeight+20,
            //resizable:{handles:'s e'},
            border:0,
            bodyPadding:'1 10 10 1'
        };

        return form;
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

            owner.add(form);
        }
        var center = me.getCenterCom();
        //表单数据项
        var com = me.createComfield();
        var items=[];
        items.push(com);
            center.add(items);
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
		
        //获取新增数据服务列表
        var editDataServerUrl = me.getEditDataServerUrl();
        editDataServerUrl.store.proxy.url = me.objectSaveDataServerUrl + "?" + me.objectServerParam + "=" + newValue;
        editDataServerUrl.store.load();
        
        //获取删除数据服务列表
        var delServerUrl = me.getdelServerUrl();
        delServerUrl.store.proxy.url = me.delServerUrl+"?EntityName=Bool";
        delServerUrl.store.load();
        
		//获取左列表数据服务主键字段
        var leftPrimaryKey = me.getleftPrimaryKey();
        leftPrimaryKey.store.proxy.url = me.objectPropertyUrl + "?" + me.objectPropertyParam + "=" + newValue;
        leftPrimaryKey.store.load();

	},
    /**
     * 右列表数据对象改变时处理
     * @private
     * @param {} owner
     * @param {} newValue
     */
    objectChangeR:function(owner,newValue){
        var me = this;
        var dataObject = owner.ownerCt;
        //获取对象结构
        var objectPropertyTree2 =me.getobjectPropertyTreeR();
        
        objectPropertyTree2.nodeClassName = "";
        objectPropertyTree2.CName = owner.rawValue;
        objectPropertyTree2.ClassName = newValue;
        
        objectPropertyTree2.store.proxy.url = me.objectPropertyUrl + "?" + me.objectPropertyParam + "=" + newValue;
        objectPropertyTree2.store.load();
        
        //获取获取数据服务列表
        var getDataServerUrl2 =me.getDataServerUrlR();
        var Urlstr=me.objectGetDataServerUrl + "?" + me.objectServerParam + "=List" + newValue;
        getDataServerUrl2.store.proxy.url = me.objectGetDataServerUrl + "?" + me.objectServerParam + "=List" + newValue;
        getDataServerUrl2.store.load();
        
        //获取右列表数据服务主键字段
        var rightPrimaryKey = me.getrightPrimaryKey();
        rightPrimaryKey.store.proxy.url = me.objectPropertyUrl + "?" + me.objectPropertyParam + "=" + newValue;
        rightPrimaryKey.store.load();
    },
 
	/**
	 * 点击确定按钮处理
	 * @private
	 */
	objectPropertyOKClick:function(){
		var me = this;
		var dataObject =me.getDataObject();
        
        var objecName=me.getobjectNameValue();
        if(objecName===null ){
            Ext.Msg.alert('提示','请配置左列表的数据对象');
            return ;
         }
        var objecNameR=me.getobjectNameR();
        if(objecNameR.getValue()===null ){
            Ext.Msg.alert('提示','请配置右列表的数据对象');
            return ;
         }
         
        //获取左列表服务
        var selectServerUrl =me.getDataServerUrlValue();
        if(selectServerUrl===null ){
            Ext.Msg.alert('提示','请配置左列表的获取数据服务');
            return ;
         }
        //获取左列表服务
        var editServerUrl =me.getEditDataServerUrlValue();
        if(editServerUrl===null ){
            Ext.Msg.alert('提示','请配置左列表的修改数据服务');
            return ;
         }
        //获取右列表服务
        var selectServerUrlR =me.getDataServerUrlRValue();
        if(selectServerUrlR===null ){
            Ext.Msg.alert('提示','请配置右列表的获取数据服务');
            return ;
         }
        var leftFilterField2=me.getnewleftFilterFieldTextValue();
		if(leftFilterField2=='' ){
            Ext.Msg.alert('提示','过滤字段只能选择一项,请重新选择');
            return ;
         }
		var leftMatchField2=me.getnewleftMatchFieldTextValue();
        if(leftMatchField2=='' ){
            Ext.Msg.alert('提示','匹配字段只能选择一项,请重新选择');
            return ;
         }
        var store = me.getComponent('south').store;
        
        var leftColumns =me.getobjectPropertyTree();//左列表对象属性树
        var leftData = leftColumns.getChecked();
		//列表中显示被勾选中的对象
		Ext.Array.each(leftData,function(record){
			if(record.get('leaf')){
                var index = store.findExact('InteractionField',record.get(me.columnParamsField.InteractionField));
          
                if(index === -1){//新建不存在的对象
                    var rec = ('Ext.data.Model',{
                        DisplayName:record.get('text'),
                        InteractionField:record.get(me.columnParamsField.InteractionField),
                        Appertain:'左列表',
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
                        Width:100//数据项宽度
                    });
                    store.add(rec);
                }
            }
        });
        
        var rightColumns =me.getobjectPropertyTreeR();//右列表对象属性树
        var rightData = rightColumns.getChecked();
        //列表中显示被勾选中的对象
        Ext.Array.each(rightData,function(record){
            if(record.get('leaf')){
                var index = store.findExact('InteractionField',record.get(me.columnParamsField.InteractionField));
                if(index === -1){//新建不存在的对象
                    var rec = ('Ext.data.Model',{
                        DisplayName:record.get('text'),
                        InteractionField:record.get(me.columnParamsField.InteractionField),
                        Appertain:'右列表',
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
                        Width:100//数据项宽度
                    });
                    store.add(rec);
                }
                
            }
        });
        
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
		var objectPropertyTree = me.getobjectPropertyTree();//对象属性树
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
            //changeNodes(0);
            //延时500毫秒处理
            setTimeout(function(){changeNodes(0);},500);
        }
	},
    /**
     * 对象内容勾选
     * @private
     * @param {} southParams
     */
    changeObjCheckedR:function(southParams3){
        var me = this;
        var objectPropertyTreeR = me.getobjectPropertyTreeR();//对象属性树
        var rootNode = objectPropertyTreeR.getRootNode();
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
        for(var i in southParams3){
            var value = southParams3[i].InteractionField;
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
            //me.isJustOpen = false;
            //me.browse();//渲染效果
        }else{
            var count = 0;
            var changeNodes = function(num){
                var callback =function(){
                    if(num == nodeArr.length-1){
                        if(me.appId != -1 && me.isJustOpen){
                            openNodes(nodeArr);
                            //me.isJustOpen = false;
                        }
                    }else{
                        changeNodes(++num);
                    }
                }
                expandParentNode(nodeArr[num],callback);
            }
            //延时900毫秒处理
            setTimeout(function(){changeNodes(0);},8000);
            //me.browse();//渲染效果
        }
    
    },
	//=====================组件的创建与删除=======================
	/**
	 * 新建列表
	 * @private
	 * @return {}
	 */
	createComfield:function(){
		var me = this;
        var objecName=me.getobjectNameValue();

        var tempLeftUrl=me.getDataServerUrlValue();
        if(tempLeftUrl===null||tempLeftUrl==undefined ){
            Ext.Msg.alert('请配置左列表的获取数据');
            return ;
         }else{
            tempLeftUrl=(getRootPath() + '/' + tempLeftUrl.split('?')[0]);
         }
        var objecNameR=me.getobjectNameRValue();
        if(objecNameR===null||objecNameR==undefined ){
            Ext.Msg.alert('请配置右列表的数据对象');
            return ;
         }
        var delServerUrl2=me.getdelServerUrlValue();
        if(delServerUrl2===null ||delServerUrl2==undefined){
            Ext.Msg.alert('请配置左列表的删除数据服务');
            return ;
         }else{
            delServerUrl2=(getRootPath() + '/' + delServerUrl2.split('?')[0]);
         }
        var leftPrimaryKey=me.getleftPrimaryKeyValue();
        if(leftPrimaryKey===null ||leftPrimaryKey==undefined){
            Ext.Msg.alert('请配置左列表的字段主键id');
            return ;
         }
        var rightPrimaryKey=me.getrightPrimaryKeyValue();
        if(rightPrimaryKey===null||rightPrimaryKey==undefined){
            Ext.Msg.alert('请配置右列表的字段主键id');
            return ;
         }
         
        //获取右列表服务
        var tempRightUrl=me.getDataServerUrlRValue();
        if(tempRightUrl===null ||tempRightUrl==undefined){
            Ext.Msg.alert('请配置右列表的获取数据服务');
            return ;
         }else{
            tempRightUrl=(getRootPath() + '/' + tempRightUrl.split('?')[0]);
         }
        //获取左列表服务
        var tempSaveUrl=me.getEditDataServerUrlValue();
        if(tempSaveUrl===null||tempSaveUrl==undefined){
            Ext.Msg.alert('请配置左列表的修改数据服务');
            return ;
         }else{
            tempSaveUrl=(getRootPath() + '/' + tempSaveUrl.split('?')[0] );
         }

        var rightFieldsetTitle2=me.getrightFieldsetTitle();
        var leftFieldsetTitle2=me.getleftFieldsetTitle();
        var leftInternalWhere2='';
        var rightInternalWhere2='';
        
        var leftFilterField2=me.getnewleftFilterFieldTextValue();
        if(leftFilterField2==''||leftFilterField2==undefined ){
            Ext.Msg.alert('提示','过滤字段只能选择一项,请重新选择');
            return ;
         }
         
        var leftMatchField2=me.getnewleftMatchFieldTextValue();
        if(leftMatchField2==''||leftMatchField2==undefined ){
            Ext.Msg.alert('提示','匹配字段只能选择一项,请重新选择');
            return ;
         }
        var relationObjectName2=me.getrelationObjectNameValue();
        if(relationObjectName2==''||relationObjectName2==undefined ){
            Ext.Msg.alert('提示','请选择关系表一的数据对象');
            return ;
         }
        var width2=me.getwidthValue();//列表宽度
        var height2=me.getheightValue();
        
        var leftColumns=me.createLeftColumns();//左列表的列字段集
        var rightColumns=me.createRightColumns();//右列表的列字段集
        
        var filterLeft2=me.getfilterLeftValue();//左列表的过滤字段
        
        var filterRight2 = me.getfilterRightValue();//右列表的过滤字段
        var btnLeft2 = me.getbtnLeftValue();//左移按钮开关
        var btnAllLeft2 = me.getbtnAllLeftValue();//全左移按钮开关
        var btnRight2=me.getbtnRightValue();//右移按钮开关
        var btnAllRight2 = me.getbtnAllRightValue();//全右移按钮开关
        var btnHidden2 = me.getbtnHiddenValue();
        var selectType2 = me.getselectTypeValue();//单选或多选方式
        var fieldSetLeft2 =me.getfieldSetLeftValue();//左列表的stroe字段集
        var fieldSetRight2 = me.getfieldSetRightValue();//右列表的stroe字段集
        
		var grid = {
			xtype: 'dditems',
			itemId:"'"+objecName+"'",
			autoScroll:true,
			width:width2,
		    height:height2,
            relationObjectName:relationObjectName2,//关系表一的数据对象
            
		    rightFieldsetTitle:rightFieldsetTitle2,//右列表容器标题
		    leftFieldsetTitle:leftFieldsetTitle2,//左列表容器标题名称
		    leftFieldset:fieldSetRight2,//左列表容器开关
            leftFilterField:leftFilterField2,
            leftMatchField:leftMatchField2,
            
            leftInternalWhere:'',
            rightInternalWhere:'',
            rightExternalWhere:'', //右列表的外部hql
            leftExternalWhere:'', //左列表的外部hql
            delServerUrl:delServerUrl2,
            leftPrimaryKey:leftPrimaryKey,
            rightPrimaryKey:rightPrimaryKey,
            
		    selectType:selectType2,//多选方式(列表数据是否允许多选)true:允许多选;false:不允许多选
		    
		    leftServerUrl:tempLeftUrl,//左列表获取后台数据服务地址
		    rightServerUrl:tempRightUrl,//右列表获取后台数据服务地址
		    saveServerUrl:tempSaveUrl,//保存服务地址
            
            rightObjectName:"'"+objecNameR+"'",//左列表的数据对象名称
		    leftObjectName:"'"+objecName+"'",//左列表的数据对象名称
            
		    leftField:'',//左列表的model的Field
		    rightField:'',//右列表的model的Field
		    
		    valueLeftField:leftColumns,//数据列表值字段,可以是外面做好数据适配后传进来
		    valueRightField:rightColumns,//数据列表值字段,可以是外面做好数据适配后传进来
		    
		    btnLeft:btnLeft2,//左移按钮显示(false)/隐藏:true
		    btnAllLeft:btnAllLeft2,//显示(false)/隐藏:true
		    btnRight:btnRight2,//显示(false)/隐藏:true
		    btnAllRight:btnAllRight2,//显示(false)/隐藏:true
		    btnHidden:btnHidden2,//控制(确定/取消按钮)显示:false,隐藏:true
		    
		    filterLeft:filterLeft2,//控制左面板过滤栏的显示:false,隐藏:true
		    filterRight:filterRight2,//控制右面板过滤栏的显示:false,隐藏:true
		  
		    fieldSetLeft:fieldSetLeft2,//控制左列表最外层的显示:false,隐藏:true
		    fieldSetRight:fieldSetRight2//控制右列表最外层的显示:false,隐藏:true
		};

		//列表面板事件监听
		grid.listeners = { 
			resize:function(com,width,height,oldWidth,oldHeight,eOpts){//列表大小变化
				//列表宽度和高度赋值
				var obj = {Width:width,Height:height};
				me.setPanelParams(obj);
			},
			onLeftClick:function(){
		
           },
           onRightClick:function(){

           } 
		};
		
		return grid;
	},
    /**
     * 创建左列列表的列
     * @private
     * @return {}
     */
    createLeftColumns:function(){
        var me = this;
        //列属性(已排序)
        var columnParams = me.getLeftColumnParams();
        var columns = [];
        for(var i in columnParams){
            var cmConfig = {};
            cmConfig.text = columnParams[i].DisplayName;
            cmConfig.dataIndex = columnParams[i].InteractionField;
            cmConfig.width = columnParams[i].Width;
            
            if(columnParams[i].IsLocked){
                cmConfig.locked = columnParams[i].IsLocked;
            }
            
            cmConfig.sortable = columnParams[i].CanSort;
            cmConfig.hidden = (columnParams[i].IsHidden || columnParams[i].CannotSee);
            cmConfig.hideable = !columnParams[i].CannotSee;
            cmConfig.align = columnParams[i].AlignType;
            
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
     * 创建左列表数据列Str
     * @private
     * @return {}
     */
    createLeftColumnsStr:function(){
        var me = this;
        //列属性(已排序)
        var columnParams = me.getLeftColumnParams();
        
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
                "width:" + columnParams[i].Width + ",";
                
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
                colEditer+
                //"align:'" + columnParams[i].AlignType + "'" + 
            "}";
            columnsStr = columnsStr + col + ",";
        }

        if(columnsStr != ""){
            columnsStr = columnsStr.substring(0,columnsStr.length-1);
        }
        
        var cStr = "[";
        cStr += columnsStr + "]";
        return cStr;
    },
    /**
     * 创建右列表数据列Str
     * @private
     * @return {}
     */
    createRightColumnsStr:function(){
        var me = this;
        //列属性(已排序)
        var columnParams = me.getRightColumnParams();
        
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
                "width:" + columnParams[i].Width + ",";
                
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
                colEditer+
                "align:'" + columnParams[i].AlignType + "'" + 
            "}";
            columnsStr = columnsStr + col + ",";
        }

        if(columnsStr != ""){
            columnsStr = columnsStr.substring(0,columnsStr.length-1);
        }
        
        var cStr = "[";
        cStr += columnsStr + "]";
        return cStr;
    },
    /**
     * 获取左列表属性数据(已按列次序排序)
     * @private
     * @return {}
     */
    getLeftColumnParams:function(){
        var myItems = [];
        
        var me = this;
        
        var list = me.getComponent('south');
        var items = list.store.data.items;
        
        var map = [];
        for(var i in items){
            var type=items[i].get('Appertain');
            if(type=='左列表'){
	            var kv = {OrderNum:items[i].get('OrderNum'),Index:i};
	            map.push(kv);
            }
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
            record.Width = item.get('Width');
            //record.AlignType = item.get('AlignType');
            //record.ColumnType=item.get('ColumnType');
            
            myItems.push(record);
        }
        
        return myItems;
    },
    /**
     * 创建右列列表的列
     * @private
     * @return {}
     */
    createRightColumns:function(){
        var me = this;
        //列属性(已排序)
        var columnParams = me.getRightColumnParams();
        var columns = [];
        for(var i in columnParams){
            var cmConfig = {};
            cmConfig.text = columnParams[i].DisplayName;
            cmConfig.dataIndex = columnParams[i].InteractionField;
            cmConfig.width = columnParams[i].Width;
            
            if(columnParams[i].IsLocked){
                cmConfig.locked = columnParams[i].IsLocked;
            }
            
            cmConfig.sortable = columnParams[i].CanSort;
            cmConfig.hidden = (columnParams[i].IsHidden || columnParams[i].CannotSee);
            cmConfig.hideable = !columnParams[i].CannotSee;
            cmConfig.align = columnParams[i].AlignType;
            
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
     * 获取右列表属性数据(已按列次序排序)
     * @private
     * @return {}
     */
    getRightColumnParams:function(){
        var myItems = [];
        
        var me = this;
        
        var list = me.getComponent('south');
        var items = list.store.data.items;
        
        var map = [];
        for(var i in items){
            var type=items[i].get('Appertain');
            if(type=='右列表'){
                var kv = {OrderNum:items[i].get('OrderNum'),Index:i};
                map.push(kv);
            }
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
            record.Width = item.get('Width');
            //record.AlignType = item.get('AlignType');
            //record.ColumnType=item.get('ColumnType');
            
            myItems.push(record);
        }
        
        return myItems;
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
     * 给属性面板的宽高项赋值
     * @private
     * @param {} record
     */
    setWH:function(componentItemId){
        var me = this;
        var height =me.getheightCom();
        var width=me.getwidthCom();
            height.setValue(me.defaultPanelHeight);
            width.setValue(me.defaultPanelWidth);
    },
    
    /**
     * 属性面板基础数据赋值
     * @private
     * @param {} componentItemId
     * @param {} record
     */
    setBasicParamsPanelValues:function(){
        var me = this;
        var filterLeft=me.getfilterLeft();
        
        var filterRight = me.getfilterRight();
        var btnLeft = me.getbtnLeft();
        var btnAllLeft = me.getbtnAllLeft();
        var btnRight=me.getbtnRight();
        var btnAllRight = me.getbtnAllRight();
        var btnHidden = me.getbtnHidden();
        var selectType = me.getselectType();
        var fieldSetLeft =me.getfieldSetLeft();
        var fieldSetRight = me.getfieldSetRight();
        
        //是否显示左过滤:显示--false,隐藏--true
        var arrfilterLeft="'"+false+"'";
        var valuesfilterLeft="{filterLeft:["+arrfilterLeft+"]}";
        var myfilterLeftJson=Ext.decode(valuesfilterLeft);
        filterLeft.setValue(myfilterLeftJson);
        
        //是否显示右过滤栏:显示--false,隐藏--true
        var arrfilterRight="'"+false+"'";
        var valuesfilterRight="{filterRight:["+arrfilterRight+"]}";
        var myfilterRightjson=Ext.decode(valuesfilterRight);
        filterRight.setValue(myfilterRightjson);
        
        //是否显示<按钮:显示--false,隐藏--true
        var arrbtnLeft="'"+false+"'";
        var valuesbtnLeft="{btnLeft:["+arrbtnLeft+"]}";
        var mybtnLeftJson=Ext.decode(valuesbtnLeft);
        btnLeft.setValue(mybtnLeftJson);
        
        //是否显示<<按钮:显示--false,隐藏--true
        var arrbtnAllLeft="'"+false+"'";
        var valuesbtnAllLeft="{btnAllLeft:["+arrbtnAllLeft+"]}";
        var mybtnAllLeftJson=Ext.decode(valuesbtnAllLeft);
        btnAllLeft.setValue(mybtnAllLeftJson);
        
        //是否显示>按钮:显示--false,隐藏--true
        var arrbtnRight="'"+false+"'";
        var valuesbtnRight="{btnRight:["+arrbtnRight+"]}";
        var mybtnRightJson=Ext.decode(valuesbtnRight);
        btnRight.setValue(mybtnRightJson);
        
        //是否显示>>按钮:显示--false,隐藏--true
        var arrbtnAllRight="'"+false+"'";
        var valuesbtnAllRight="{btnAllRight:["+arrbtnAllRight+"]}";
        var mybtnAllRightJson=Ext.decode(valuesbtnAllRight);
        btnAllRight.setValue(mybtnAllRightJson);
        
        //是否确定取消按钮:显示--false,隐藏--true
        var arrbtnHidden="'"+false+"'";
        var valuesbtnHidden="{btnHidden:["+arrbtnHidden+"]}";
        var mybtnHiddenJson=Ext.decode(valuesbtnHidden);
        btnHidden.setValue(mybtnHiddenJson);
        
        //多选方式:false--单选,true--多选
        var arrselectType="'"+false+"'";
        var valuesselectType="{selectType:["+arrselectType+"]}";
        var myselectTypeJson=Ext.decode(valuesselectType);
        selectType.setValue(myselectTypeJson);
        
        //是否显示左列表最外层的显示:显示--false,隐藏--true
        var arrfieldSetLeft="'"+true+"'";
        var valuesfieldSetLeft="{fieldSetLeft:["+arrfieldSetLeft+"]}";
        var myfieldSetLeftJson=Ext.decode(valuesfieldSetLeft);
        fieldSetLeft.setValue(myfieldSetLeftJson);
        
        //是否显示右列表最外层的显示:显示--false,隐藏--true
        var arrfieldSetRight="'"+true+"'";
        var valuesfieldSetRight="{fieldSetRight:["+arrfieldSetRight+"]}";
        var myfieldSetRightJson=Ext.decode(valuesfieldSetRight);
        fieldSetRight.setValue(myfieldSetRightJson);

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
	//=====================弹出窗口=======================
    
    //=====================设置获取参数=======================
	/**
	 * 获取展示区域
	 * @private
	 * @return {}
	 */
	getCenterCom:function(){
		var me = this;
		var center = me.getComponent('center').getComponent('center');
		return center;
	},
    /**
     * 获取展示区域组件
     * @private
     * @return {}
     */
    getCenterDdItems:function(){
        var me = this;
        var objectName=me.getobjectNameValue();
        var com = me.getCenterCom().getComponent(objectName);//getComponent('center');
        return com;
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
     * 获取左列表的所有行信息（简单对象数组）
     * @private
     * @return {}
     */
    getleftSouthRocordArray:function(){
        var me = this;
        var records = me.getSouthRecords();
        var fields = me.getSouthStoreFields();
        
        var arr = [];
        //model转化成简单对象
        var getObjByRecord = function(record){
            var obj = {};
            Ext.Array.each(fields,function(field){
                var appertain=record.get('Appertain');
                if(appertain=='左列表'){
                    obj[field.name] = record.get(field.name);
                }
            });
            return obj;
        };
        
        //组装简单对象数组
        Ext.Array.each(records,function(record){
            var appertain=record.get('Appertain');
                if(appertain=='左列表'){
		            var obj = getObjByRecord(record);
		            arr.push(obj);
                }
        });
        return arr;
    },
    /**
     * 获取右列表的所有行信息（简单对象数组）
     * @private
     * @return {}
     */
    getrightSouthRocordArray:function(){
        var me = this;
        var records = me.getSouthRecords();
        var fields = me.getSouthStoreFields();
        var arr = [];
        //model转化成简单对象
        var getObjByRecord = function(record){
            var obj = {};
            Ext.Array.each(fields,function(field){
                var appertain=record.get('Appertain');
                if(appertain=='右列表'){
                    obj[field.name] = record.get(field.name);
                }
            });
            return obj;
        };
        
        //组装简单对象数组
        Ext.Array.each(records,function(record){
            var appertain=record.get('Appertain');
                if(appertain=='右列表'){
                    var obj = getObjByRecord(record);
                    arr.push(obj);
                }
        });
        return arr;
    },
    /***
     * 左列表加载数据的过滤字段（简单对象数组）
     * @return {}
     */
    getleftFilterFieldSouthArray:function(){
        var me = this;
        var arr = [];
        var leftFilterField=me.getleftFilterField();
        var leftData = leftFilterField.getChecked();
        var fields = me.getSouthStoreFields();
        //model转化成简单对象
        var getObjByRecord = function(record){
            var obj = {};
            Ext.Array.each(fields,function(field){
                    obj[field.name] = record[field.name];
            });
            return obj;
        };
        
        //列表中显示被勾选中的对象
        Ext.Array.each(leftData,function(record){
            if(record.get('leaf')){
                var displayName = '';
                var interactionField = '';
                interactionField = record.get('InteractionField');
                displayName = record.get('text');
                var jsonStr="{InteractionField:'"+interactionField+"',"+"DisplayName:'"+displayName+"'}";
                var obj = getObjByRecord(Ext.decode(jsonStr));
                     arr.push(obj);
            }
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
            {name:'Appertain',type:'string'},//交互字段所属列表(左列表/右列表)
            
            {name:'Width',type:'int'},//列宽
            {name:'hasBeenDeleted',type:'bool'},//删除标记
            {name:'IsLocked',type:'bool'},//默认锁定
            {name:'IsHidden',type:'bool'},//默认隐藏
            {name:'CannotSee',type:'bool'},//不可见
            {name:'CanSort',type:'bool'},//可排序
            {name:'DefaultSort',type:'bool'},//默认排序
            {name:'SortType',type:'string'},//排序方式
            {name:'OrderNum',type:'int'},//排布顺序
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
		var panelParams2 = me.getPanelParams();
		var southParams2 = me.getSouthRocordInfoArray();
		var leftSouthParams2 = me.getleftSouthRocordArray();
        var rightSouthParams2 = me.getrightSouthRocordArray();

		var appParams = {
			panelParams:panelParams2,
			southParams:southParams2,//总的数据行记录
            leftSouthParams:leftSouthParams2,//左列表的数据行
            rightSouthParams:rightSouthParams2//右列表的数据行
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

        var objectPropertyTreeR =me.getobjectPropertyTreeR();
        
		var callback = function(appInfo){
			var appParams = Ext.JSON.decode(appInfo[me.fieldsObj.DesignCode]);
			var panelParams = appParams.panelParams;
			var southParams = appParams.southParams;//总的数据行
            
            var leftSouthParams = appParams.leftSouthParams;//左列表的数据行
            var rightSouthParams = appParams.rightSouthParams;//右列表的数据行
            
			me.DataTimeStamp = appInfo[me.fieldsObj.DataTimeStamp];
			//赋值
            me.setSouthRecordByArray(southParams);//数据项列表赋值
            me.setObjData();//数据对象赋值
            me.setObjDataR();//数据对象赋值
            me.setrelationObjectName();
            
			objectPropertyTree.store.on({
	        	load:function(store,node,records,successful,e){
	        		if(me.appId != -1 && me.isJustOpen && node == objectPropertyTree.getRootNode()){
	        			//左列表树的对象内容勾选
	        			me.changeObjChecked(leftSouthParams);
	        		}
	        	}
	       	});

            objectPropertyTreeR.store.on({
                load:function(store,node,records,successful,e){
                    if(me.appId != -1 && node == objectPropertyTreeR.getRootNode()){
                        //右列表的树的对象内容勾选
                        me.changeObjCheckedR(rightSouthParams);
                    }
                }
            });
	       	me.setPanelParams(panelParams);//属性面板赋值
			
			//获取获取数据服务列表
			var getDataServerUrl =me.getDataServerUrl();
			getDataServerUrl.value = panelParams.getDataServerUrl;
            
            //获取获取数据服务列表
            var getDataServerUrlR =me.getDataServerUrlR();
            getDataServerUrlR.value = panelParams.getDataServerUrlR;
		
			//获取新增数据服务列表
			var editDataServerUrl =me.getEditDataServerUrl();
			editDataServerUrl.value = panelParams.editDataServerUrl;
            
            //获取删除数据服务列表
            var delServerUrl =me.getdelServerUrl();
            delServerUrl.value = panelParams.delServerUrl;
            
            var objectName=me.getobjectName();
            var leftPrimaryKey = me.getleftPrimaryKey();
            leftPrimaryKey.store.proxy.url = me.objectPropertyUrl + "?" + me.objectPropertyParam + "=" + objectName.getValue();
            leftPrimaryKey.store.load();
            leftPrimaryKey.value = panelParams.leftPrimaryKey;
             
            //获取右列表数据服务主键字段
            var objectNameR=me.getobjectNameR();
	        var rightPrimaryKey = me.getrightPrimaryKey();
	        rightPrimaryKey.store.proxy.url = me.objectPropertyUrl + "?" + me.objectPropertyParam + "=" + objectNameR.getValue();
	        rightPrimaryKey.store.load();
            rightPrimaryKey.value = panelParams.rightPrimaryKey;
            if(!panelParams.objectName || panelParams.objectName == ""){
                me.browse();
            }
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
	setrelationObjectName:function(){
		var me = this;
		//数据对象类
		var relationObjectName =me.getrelationObjectName();
		relationObjectName.store.load();
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
	 * 创建类代码
	 * @private
	 * @return {}
	 */
	createAppClass:function(){
		var me = this;
		//表单配置参数
		var params = me.getPanelParams();
        var objecName=me.getobjectNameValue();

        var tempLeftUrl=me.getDataServerUrlValue();
        if(tempLeftUrl===null||tempLeftUrl==undefined ){
            Ext.Msg.alert('请配置左列表的获取数据');
            return ;
         }else{
            tempLeftUrl=(tempLeftUrl.split('?')[0]);
         }
        var objecNameR=me.getobjectNameRValue();
        if(objecNameR===null||objecNameR==undefined ){
            Ext.Msg.alert('请配置右列表的数据对象');
            return ;
         }
        var delServerUrl2=me.getdelServerUrlValue();
        if(delServerUrl2===null ||delServerUrl2==undefined){
            Ext.Msg.alert('请配置左列表的删除数据服务');
            return ;
         }else{
            delServerUrl2=(delServerUrl2.split('?')[0]);
         }
        var leftPrimaryKey=me.getleftPrimaryKeyValue();
        if(leftPrimaryKey===null ||leftPrimaryKey==undefined){
            Ext.Msg.alert('请配置左列表的字段主键id');
            return ;
         }
        var rightPrimaryKey=me.getrightPrimaryKeyValue();
        if(rightPrimaryKey===null||rightPrimaryKey==undefined){
            Ext.Msg.alert('请配置右列表的字段主键id');
            return ;
         }
         
        //获取右列表服务
        var tempRightUrl=me.getDataServerUrlRValue();
        if(tempRightUrl===null ||tempRightUrl==undefined){
            Ext.Msg.alert('请配置右列表的获取数据服务');
            return ;
         }else{
            tempRightUrl=(tempRightUrl.split('?')[0]);
         }
        //获取左列表服务
        var tempSaveUrl=me.getEditDataServerUrlValue();
        if(tempSaveUrl===null||tempSaveUrl==undefined){
            Ext.Msg.alert('请配置左列表的修改数据服务');
            return ;
         }else{
            tempSaveUrl=(tempSaveUrl.split('?')[0] );
         }

        var rightFieldsetTitle2=me.getrightFieldsetTitle();
        var leftFieldsetTitle2=me.getleftFieldsetTitle();
        var leftInternalWhere2=me.getDefaultParams();
        var rightInternalWhere2=me.getDefaultParamsR();
        
        var leftFilterField2=me.getnewleftFilterFieldTextValue();
        if(leftFilterField2==''||leftFilterField2==undefined ){
            Ext.Msg.alert('提示','过滤字段只能选择一项,请重新选择');
            return ;
         }
         
        var leftMatchField2=me.getnewleftMatchFieldTextValue();
        if(leftMatchField2==''||leftMatchField2==undefined ){
            Ext.Msg.alert('提示','匹配字段只能选择一项,请重新选择');
            return ;
         }
         
        var width2=me.getwidthValue();//列表宽度
        var height2=me.getheightValue();
        
        var leftColumns=me.createLeftColumnsStr();//左列表的列字段集
        var rightColumns=me.createRightColumnsStr();//右列表的列字段集
        
        var filterLeft2=me.getfilterLeftValue();//左列表的过滤字段开关
        
        var filterRight2 = me.getfilterRightValue();//右列表的过滤字段开关
        var btnLeft2 = me.getbtnLeftValue();//左移按钮开关
        var btnAllLeft2 = me.getbtnAllLeftValue();//全左移按钮开关
        var btnRight2=me.getbtnRightValue();//右移按钮开关
        var btnAllRight2 = me.getbtnAllRightValue();//全右移按钮开关
        var btnHidden2 = me.getbtnHiddenValue();
        var selectType2 = me.getselectTypeValue();//单选或多选方式
        var fieldSetLeft2 =me.getfieldSetLeftValue();//左列表容器开关
        var fieldSetRight2 = me.getfieldSetRightValue();//右列表容器开关
       
        var relationObjectName=me.getrelationObjectNameValue();
        if(relationObjectName==''||relationObjectName==undefined ){
            Ext.Msg.alert('提示','请选择关系表一的数据对象');
            return ;
         }
		var appClass = 
        "Ext.QuickTips.init();" + 
        "Ext.Loader.setConfig({enabled: true});" + 
		"Ext.Loader.setPath('Ext.ux',getRootPath()+'/ui/extjs/ux');" + 
        
		"Ext.define('" + params.appCode + "',{" + 
		    "extend:'Ext.zhifangux.DdItems'," + 
			"alias:'widget." + params.appCode + "'," + 
			"title:'" + params.titleText + "'," + 
			"width:" + width2 + "," + 
			"height:" + height2+ "," + 
            
            "leftMatchField:'" + leftMatchField2+ "'," + //左列表与右列表的匹配字段
            "leftFilterField:'" + leftFilterField2+ "'," + //左列表的外部传入的过滤字段
            "leftFilterValue:''," + //左列表的外部传入的过滤条件的值
            
            "rightFieldsetTitle:'" + rightFieldsetTitle2.getValue()+ "'," + //右列表容器标题
            "leftFieldsetTitle:'" + leftFieldsetTitle2.getValue()+ "'," + //左列表容器标题名称
            
            "leftInternalWhere:'" +"'," + //左列表的内部hql
            "rightInternalWhere:'" +"'," + //右列表的内部hql
            
            "rightObjectName:'" +objecNameR +"'," +//左列表的数据对象名称
            "leftObjectName:'" +objecName +"'," + //左列表的数据对象名称
            
			"relationObjectName:'" +relationObjectName+"'," + //对象名，用于自动主键匹配
            
            "leftPrimaryKey:'" +leftPrimaryKey+"'," +
            "rightPrimaryKey:'" +rightPrimaryKey+"'," +
            "leftExternalPrimaryKey:''," + //左列表的外部传入的主键id
            
			"leftExternalWhere:''," + //外部hql
			"rightExternalWhere:''," + //外部hql
            
			"autoSelect:true," + 
			"deleteIndex:-1," + //被删除的行下标号
			"autoScroll:true," +
            
            "leftServerUrl:getRootPath()+"+"'/'+"+"'"+tempLeftUrl+ "'," + //左列表获取后台数据服务地址
            "rightServerUrl:getRootPath()+"+"'/'+"+"'"+tempRightUrl+ "'," +//右列表获取后台数据服务地址
            "saveServerUrl:getRootPath()+"+"'/'+"+"'"+tempSaveUrl+ "'," +//保存服务地址
            "delServerUrl:getRootPath()+"+"'/'+"+"'"+delServerUrl2+ "'," +//删除服务地址
            
            "selectType:"+selectType2+ "," +//多选方式(列表数据是否允许多选)true:允许多选;false:不允许多选
            "leftField:[]," +//左列表的model的Field
            "rightField:[]," +//右列表的model的Field
            
            "valueLeftField:"+leftColumns.replace(/"/g,"'")+ "," +//数据列表值字段,可以是外面做好数据适配后传进来
            "valueRightField:"+rightColumns.replace(/"/g,"'")+ "," +//数据列表值字段,可以是外面做好数据适配后传进来
            
            "btnLeft:"+btnLeft2+ "," +//左移按钮显示(false)/隐藏:true
            "btnAllLeft:"+btnAllLeft2+ "," +//显示(false)/隐藏:true
            "btnRight:"+btnRight2+ "," +//显示(false)/隐藏:true
            "btnAllRight:"+btnAllRight2+ "," +//显示(false)/隐藏:true
            "btnHidden:"+btnHidden2+ "," +//控制(确定/取消按钮)显示:false,隐藏:true
            
            "filterLeft:"+filterLeft2+ "," +//控制左面板过滤栏的显示:false,隐藏:true
            "filterRight:"+filterRight2+ "," +//控制右面板过滤栏的显示:false,隐藏:true
          
            "fieldSetLeft:"+fieldSetLeft2+ "," +//控制左列表最外层的显示:false,隐藏:true
            "fieldSetRight:"+fieldSetRight2+ "," ;//控制右列表最外层的显示:false,隐藏:true
            
            appClass=appClass+"afterRender:function(){" + 
                "var me=this;" + 
                "me.callParent(arguments);" + 
                //加载数据后默认选中第一行
	
                "if(Ext.typeOf(me.callback)=='function'){me.callback(me);}" + 
            "}," ;
            
            appClass=appClass+
			"initComponent:function(){" + 
				"var me=this;" + 
                
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