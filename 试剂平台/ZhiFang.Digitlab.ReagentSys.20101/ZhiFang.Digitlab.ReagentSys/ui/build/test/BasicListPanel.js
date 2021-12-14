/**
 * 普通列表构建工具
 * 
 */
Ext.ns('Ext.build');
Ext.define('Ext.build.BasicListPanel',{
	extend:'Ext.panel.Panel',
	alias: 'widget.basiclistpanel',
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
	buildTitle:'普通列表构建工具',
	
	//标题字体设置longfc
    win:null,//创建和弹出选择器窗体
    win2:null,//创建和弹出选择器窗体
    winHeight:270,        //弹出选择器窗体高度像素
    winWidth:460,       //弹出选择器窗体宽度像素
    winTitle:'',        //弹出选择器窗体标题

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
	 * 列类型
	 * @type 
	 */
	columnTypeList:[
		['','默认'],
		['date','日期型'],
		['number','数字型'],
		['bool','布尔型'],
		['combobox','下拉型'],
		['file','文件型']
	],
	/**
	 * 对齐方式
	 * @type 
	 */
	AlignTypeList:[
		['left','左对齐'],
		['center','居中'],
		['right','右对齐']
	],
    /**
     * 面板样式
     * @type 
     */
    panelStyleList:[
    	['','默认'],
    	['red','喜庆红'],
    	['blue','金典蓝'],
    	['pink','温馨粉']
    ],
    /**
     * 列表编辑方式
     * @type String
     */
    panelEditor:[
        ['','默认'],
        ['row','行编辑列表'],
        ['column','单元格编辑列表']
    ],
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
    defaultPanelWidth:500,
    /**
     * 列表默认高度
     * @type Number
     */
    defaultPanelHeight:200,
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
		south.height = 200;
		east.width = 250;
		
		//功能块收缩属性
		east.split = true;
		east.collapsible = true;
		
		south.split = true;
		south.collapsible = true;
		
		//列属性列表是否默认收缩
		//south.collapsed = true;
		
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
				xtype:'grid',
				title:'列表',
				columns:[
					{text:'示例列一'},
					{text:'示例列二'},
					{text:'示例列三'}
				],
				itemId:'center',
				width:me.defaultPanelWidth,
				height:me.defaultPanelHeight
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
			columns:[//列模式的集合
				{xtype:'rownumberer',text:'序号',width:35,align:'center',hidden:true},
				{text:'交互字段',dataIndex:'InteractionField',disabled:true},
				{text:'显示名称',dataIndex:'DisplayName',
					editor: {allowBlank:false} 
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
				{text:'对齐方式',dataIndex:'AlignType',width:60,align:'center',
		            renderer:function(value, p, record){
						var typelist = me.AlignTypeList;
						for(var i=0;i<typelist.length;i++){
							if(value == typelist[i][0]){
								return Ext.String.format(typelist[i][1]);
							}
						}
			 		},
		            editor: new Ext.form.field.ComboBox({
		                mode:'local',editable:false, 
						displayField:'text',valueField:'value',
		                store:new Ext.data.SimpleStore({ 
						    fields:['value','text'], 
						    data:me.AlignTypeList
						}),
		                listClass: 'x-combo-list-small'
		            })
				},
				{text:'列头字体内容',dataIndex:'HeadFont',hidden:true},
				{xtype:'actioncolumn',text:'列头字体',width:60,align:'center',
					items:[{
						iconCls:'build-img-font-configuration hand',
		                tooltip: '列头字体设置',
		                handler: function(grid, rowIndex, colIndex) {
		                    var rec = grid.getStore().getAt(rowIndex);
		                    //longfc 
                            me.OpenCategoryWinTwo(rec,"HeadFont");
		                }
					}]
				},
				{text:'列字体内容',dataIndex:'ColumnFont',hidden:true},
				{xtype:'actioncolumn',text:'列字体',width:50,align:'center',
					items:[{
						iconCls:'build-img-font-configuration hand',
		                tooltip: '列字体设置',
		                handler: function(grid, rowIndex, colIndex) {
		                    var rec = grid.getStore().getAt(rowIndex);
		                    //longfc
                            me.OpenCategoryWinTwo(rec,"ColumnFont");
		                }
					}]
				},
				{text: '列类型',dataIndex:'ColumnType',width:60,align: 'center',
					renderer:function(value, p, record){
						var typelist = me.columnTypeList;
						for(var i=0;i<typelist.length;i++){
							if(value == typelist[i][0]){
								return Ext.String.format(typelist[i][1]);
							}
						}
			 		},
		            editor: new Ext.form.field.ComboBox({
		                mode:'local',editable:false, 
						displayField:'text',valueField:'value',
		                store:new Ext.data.SimpleStore({ 
						    fields:['value','text'], 
						    data:me.columnTypeList
						}),
		                listClass: 'x-combo-list-small'
		            })
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
		//整体样式
		var panelStyle = me.createPanelStyle();
        //列表编辑方式
        var panelEditor=me.createEditorGrid();
		//标题设置
		var title = me.createTitle();
		//数据对象
		var dataObj = me.createDataObj();
		//其他设置
		var other = me.createOther();
		//查询设置
		var search = me.createSearch();
		//表单宽高
		var panelWH = me.createWidthHieght();
		//功能按钮
		var buttons = me.createButtons();
		
		var listParamsPanel = {
			xtype:'form',
			itemId:'center' + me.ParamsPanelItemIdSuffix,
			title:'列表属性配置',
			header:false,
			autoScroll:true,
			border:false,
	        bodyPadding:5,
	        items:[appInfo,panelStyle,panelWH,title,dataObj,other,search,panelEditor,buttons]
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
				xtype:'textfield',fieldLabel:'功能编号',labelWidth:55,anchor:'100%',
	            itemId:'appCode',name:'appCode'
	        },{
				xtype:'textfield',fieldLabel:'中文名称',labelWidth:55,anchor:'100%',
	            itemId:'appCName',name:'appCName'
	        },{
				xtype:'textareafield',fieldLabel:'功能简介',labelWidth:55,anchor:'100%',grow:true,
	            itemId:'appExplain',name:'appExplain'
	        }]
		};
		return com;
	},
    /**
     * 列表表格编辑方式
     * @return {}
     */    
  	createEditorGrid:function(){
        var me = this;
        var com = {
            xtype:'fieldset',title:'列表表格编辑方式',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',defaults:{anchor:'100%'},layout:'anchor',
            itemId:'panelEditorType',
            items:[{
                xtype:'combobox',fieldLabel:'列表编辑方式',
                labelWidth:55,value:'',mode:'local',editable:false,
                displayField:'text',valueField:'value',
                itemId:'panelEditor1',name:'panelEditor1',
                store:new Ext.data.SimpleStore({ 
                    fields:['value','text'], 
                    data:me.panelEditor
                })
            }]
        };
        return com;
    },
    /**
	 * 面板整体样式
	 * @private
	 * @return {}
	 */
	createPanelStyle:function(){
		var me = this;
		var com = {
			xtype:'fieldset',title:'列表样式',padding:'0 5 0 5',collapsible:true,
	        defaultType:'textfield',defaults:{anchor:'100%'},layout:'anchor',
	        itemId:'panelStyle',
	        items:[{
				xtype:'combobox',fieldLabel:'整体样式',
	            labelWidth:55,value:'',mode:'local',editable:false,
				displayField:'text',valueField:'value',
				itemId:'panelStyle',name:'panelStyle',
				store:new Ext.data.SimpleStore({ 
				    fields:['value','text'], 
				    data:me.panelStyleList
				})
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
	        defaultType:'textfield',//defaults:{anchor:'100%'},layout:'anchor',
	        itemId:'title',
	        items:[{
	            xtype:'textfield',fieldLabel:'显示名称',labelWidth:55,value:'列表',anchor:'100%',
	            itemId:'titleText',name:'titleText'
	        },{
	        	xtype:'fieldcontainer',layout:'hbox',
	        	itemId:'titleStyle',
	        	items:[{
		        	xtype:'label',text:'字体设置:',width:55,margin:'2 0 2 0'
	        	},{
	        		xtype:'textfield',hidden:true,value:'',
	        		itemId:'titleStyle',name:'titleStyle'
	        	},{
	        		xtype:'image',itemId:'configuration',
		            imgCls:'build-img-font-configuration hand',
	            	width:16,height:16,
	            	margin:'2 0 2 5',cls:'hand',
	            	listeners:{
	            		click:{
							element:'el',
							fn:function(){
								//longfc
                    			me.OpenCategoryWin();
							}
						}
	            	}
	        	}]
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
	    	xtype:'fieldset',title:'数据对象',padding:'0 5 0 5',collapsible:true,
	        defaultType:'textfield',defaults:{anchor:'100%'},layout:'anchor',
	        itemId:'dataObject',
	        items:[{
				xtype:'combobox',fieldLabel:'数据对象',
	        	itemId:'objectName',name:'objectName',
	        	labelWidth:55,anchor:'100%',
	        	editable:true,typeAhead:true,
				forceSelection:true,
	        	queryMode:'local',
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
	        	dockedItems:[{
	        		xtype:'toolbar',
	        		style:{background:'#fff'},
	        		itemId:'objectPropertyToolbar',
	        		items:[{
	        			xtype:'button',text:'确定',itemId:'objectPropertyOK',
	        			iconCls:'build-button-ok',
						listeners:{
	        				click:function(){
	        					me.objectPropertyOKClick();
	        				}
	        			}
	        		}]
	        	}],
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
				            	
				            	var east = me.getComponent('east').getComponent('center'+me.ParamsPanelItemIdSuffix);
				            	dataObject = east.getComponent('dataObject');
								var objectPropertyTree = dataObject.getComponent('objectPropertyTree');	
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
	        	itemId:'getDataServerUrl',name:'getDataServerUrl',
	        	labelWidth:55,anchor:'100%',
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
				    		var formParamsPanel = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
							var dataObject = formParamsPanel.getComponent('dataObject');
							var objectName = dataObject.getComponent('objectName');
				    		store.proxy.url = me.objectGetDataServerUrl + "?" + me.objectServerParam + "=List" + objectName.value;
				    		
				    	}
				    }
				})
	        },{
	        	xtype:'combobox',fieldLabel:'新增数据',
	        	itemId:'addDataServerUrl',name:'addDataServerUrl',
	        	labelWidth:55,anchor:'100%',
	        	editable:false,typeAhead:true,
				forceSelection:true,mode:'local',
				emptyText:'请选择保存数据服务',
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
				    		var formParamsPanel = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
							var dataObject = formParamsPanel.getComponent('dataObject');
							var objectName = dataObject.getComponent('objectName');
				    		store.proxy.url = me.objectSaveDataServerUrl + "?" + me.objectServerParam + "=" + objectName.value;
				    		
				    	}
				    }
				})
	        },{xtype:'combobox',fieldLabel:'修改数据',
                itemId:'editDataServerUrl',name:'editDataServerUrl',
                labelWidth:55,anchor:'100%',
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
                            var formParamsPanel = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
                            var dataObject = formParamsPanel.getComponent('dataObject');
                            var objectName = dataObject.getComponent('objectName');
                            store.proxy.url = me.objectSaveDataServerUrl + "?" + me.objectServerParam + "=" + objectName.value;
                            
                        }
                        
                    }
                })            
            },
            {
	        	xtype:'textfield',fieldLabel:'默认条件',labelWidth:55,value:'',
	        	itemId:'defaultParams',name:'defaultParams'
	        },{
	        	xtype:'textfield',fieldLabel:'测试条件',labelWidth:55,value:'',
	        	itemId:'testParams',name:'testParams'
	        },{
	        	xtype:'combobox',fieldLabel:'分页模式',value:'1',
	            labelWidth:55,mode:'local',editable:false,
	            itemId:'pageType',name:'pageType',
				displayField:'text',valueField:'value',
				store:new Ext.data.SimpleStore({
				    fields:['value','text'], 
				    data:[['1','无分页'],['2','数字分页'],['3','滚动分页'],['4','进度条分页'],['5','无限分页']]
				}),
				listeners:{
					change:function(owner,newValue,oldValue,eOpts){
						var index = owner.store.find('value',newValue);//是否存在这条记录
						if(newValue && newValue != "" && index != -1){
							me.pageTypeChange(owner,newValue);
						}
					}
				}
	        },{
	        	xtype:'numberfield',fieldLabel:'每页条数',value:25,
	        	labelWidth:55,hidden:true,
	        	minValue:1,maxValue:999,
	        	itemId:'pageSize',name:'pageSize'
	        },{
	        	xtype:'numberfield',fieldLabel:'前后页数',value:1,
	        	labelWidth:55,hidden:true,
	        	minValue:1,maxValue:999,
	        	itemId:'bufferPage',name:'bufferPage'
	        },{
	        	xtype:'combobox',fieldLabel:'默认加载',value:'1',
	            labelWidth:55,mode:'local',editable:false,
	            itemId:'autoLoad',name:'autoLoad',
				displayField:'text',valueField:'value',
				store:new Ext.data.SimpleStore({
				    fields:['value','text'], 
				    data:[['1','是'],['2','否']]
				})
	        }]
	    };
		return com;
	},
	/**
	 * 其他设置
	 * @private
	 * @return {}
	 */
	createOther:function(){
		var com = {
	    	xtype:'fieldset',title:'其他',padding:'0 5 0 5',collapsible:true,
	        defaultType:'textfield',defaults:{anchor:'100%'},layout:'anchor',
	        itemId:'other',
	        items:[{
	        	xtype:'textfield',fieldLabel:'空数据提示',labelWidth:65,value:'没有数据！',
	        	itemId:'emptyText',name:'emptyText'
	        },{
	        	xtype:'textfield',fieldLabel:'加载提示',labelWidth:65,value:'获取数据中，请等待...',
	        	itemId:'loadingText',name:'loadingText'
	        },{
	            xtype:'checkbox',boxLabel:'开启遮罩层',checked:true,
	            fieldLabel:'',hideLabel:true,labelWidth:65,
	            itemId:'hasLoadMask',name:'hasLoadMask'
	        },{
	            xtype:'checkbox',boxLabel:'显示序号',checked:true,
	            fieldLabel:'',hideLabel:true,labelWidth:65,
	            itemId:'hasRowNumberer',name:'hasRowNumberer'
	        },{
	            xtype:'checkbox',boxLabel:'开启复选框',checked:true,
	            fieldLabel:'',hideLabel:true,labelWidth:65,
	            itemId:'hasCheckBox',name:'hasCheckBox'
	        },{
	            xtype:'checkbox',boxLabel:'开启列标头',checked:true,
	            fieldLabel:'',hideLabel:true,labelWidth:65,
	            itemId:'hasColumnHead',name:'hasColumnHead'
	        },{
	            xtype:'checkbox',boxLabel:'开启列小计',checked:true,
	            fieldLabel:'',hideLabel:true,labelWidth:65,
	            itemId:'hasPageCount',name:'hasPageCount'
	        },{
	            xtype:'checkbox',boxLabel:'开启汇总',checked:true,
	            fieldLabel:'',hideLabel:true,labelWidth:65,
	            itemId:'hasAllCount',name:'hasAllCount'
	        },{
	        	xtype:'numberfield',fieldLabel:'列表头高度',
	        	labelWidth:65,emptyText:'默认',
	        	itemId:'headHeight',name:'headHeight'
	        }]
	    };
	    return com;
	},
	/**
	 * 查询设置
	 * @private
	 * @return {}
	 */
	createSearch:function(){
		var com = {
	    	xtype:'fieldset',title:'查询设置',padding:'0 5 0 5',collapsible:true,
	        defaultType:'textfield',defaults:{anchor:'100%'},layout:'anchor',
	        itemId:'searchSet',
	        items:[{
                xtype:'radiogroup',
	        	itemId:'searchPosition',
		        fieldLabel:'查询栏摆放位置',
		       	labelWidth:90,
		        columns:2,
		        vertical:true,
		        items:[
		            {boxLabel:'顶部',name:'searchPosition',inputValue:'top',checked:true},
		            {boxLabel:'底部',name:'searchPosition',inputValue:'bottom'}
		        ]
	        },{
	            xtype:'fieldcontainer',layout:'hbox',
	            itemId:'searchBar',
	            items:[{
	            	xtype:'checkbox',boxLabel:'开启查询栏',checked:true,
	            	fieldLabel:'hasSearchBar',hideLabel:true,width:80,
	            	itemId:'',name:'hasSearchBar'
	            },{
	            	xtype:'image',itemId:'configuration',
	            	imgCls:'build-button-configuration-blue',
	            	width:16,height:16,
	            	margin:'2 0 2 5',cls:'hand',
	            	listeners:{
	            		click:{
							element:'el',
							fn:function(){
								alert("设置查询");
							}
						}
	            	}
	            }]
	        }]
	    };
		return com;
	},
	/**
	 * 设置面板的宽高
	 * @private
	 * @return {}
	 */
	createWidthHieght:function(){
		var me = this;
		var com = {
			xtype:'fieldset',title:'列表宽高',padding:'0 5 0 5',collapsible:true,
	        defaultType:'textfield',defaults:{anchor:'100%'},layout:'anchor',
	        itemId:'WH',
	        items:[{
				xtype:'numberfield',fieldLabel:'列表宽度',labelWidth:55,anchor:'100%',
	            itemId:'Width',name:'Width',value:me.defaultPanelWidth,
	            listeners:{
					blur:function(com,The,eOpts){
						var center = me.getCenterCom();
						center.setWidth(com.value);
					}
				}
	        },{
				xtype:'numberfield',fieldLabel:'列表高度',labelWidth:55,anchor:'100%',
	            itemId:'Height',name:'Height',value:me.defaultPanelHeight,
	            listeners:{
					blur:function(com,The,eOpts){
						var center = me.getCenterCom();
						center.setHeight(com.value);
					}
				}
	        }]
		};
		return com;
	},
	/**
	 * 功能按钮设置
	 * @private
	 * @return {}
	 */
	createButtons:function(){
		var me = this;
		var com = {
			xtype:'funbutfieldset',
			itemId:'buttonsconfig',
			defaults:{anchor:'100%'},
			layout:'anchor',
			padding:'0 5 0 5',
			collapsible:true,
			delServerDisplayField:me.objectServerDisplayField,
			delServerValueField:me.objectServerValueField,
			delServerUrl:me.objectGetDataServerUrl,
			delServerFields:me.objectServerFields,
			keyDisplayField:'DisplayName',
			keyValueField:'InteractionField',
			appId:me.appId//应用ID
		};
		return com;
	},
	//=====================功能按钮栏事件方法=======================
	/**
	 * 浏览处理
	 * @private
	 */
	browse:function(){
		var me = this;
		//交互字段
		me.changeButSetWinForm();
		
		var center = me.getCenterCom();
		var owner = center.ownerCt;
		
		var grid = me.createGrid();
		
		if(grid){
			//删除原先的表单
			owner.remove(center);
			//添加新的表单
			owner.add(grid);
			//更换组件属性面板
			me.changeParamsPanel();
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
		var objectPropertyTree = dataObject.getComponent('objectPropertyTree');
		
		objectPropertyTree.nodeClassName = "";
		objectPropertyTree.CName = owner.rawValue;
		objectPropertyTree.ClassName = newValue;
		
		objectPropertyTree.store.proxy.url = me.objectPropertyUrl + "?" + me.objectPropertyParam + "=" + newValue;
		objectPropertyTree.store.load();
    	
		//获取获取数据服务列表
		var getDataServerUrl = dataObject.getComponent('getDataServerUrl');
        var Urlstr=me.objectGetDataServerUrl + "?" + me.objectServerParam + "=List" + newValue;
		getDataServerUrl.store.proxy.url = me.objectGetDataServerUrl + "?" + me.objectServerParam + "=List" + newValue;
		getDataServerUrl.store.load();
		//获取保存数据服务列表
		var addDataServerUrl = dataObject.getComponent('addDataServerUrl');
		addDataServerUrl.store.proxy.url = me.objectSaveDataServerUrl + "?" + me.objectServerParam + "=" + newValue;
		addDataServerUrl.store.load();
        //获取修改数据服务列表
        var editDataServerUrl = dataObject.getComponent('editDataServerUrl');
        editDataServerUrl.store.proxy.url = me.objectSaveDataServerUrl + "?" + me.objectServerParam + "=" + newValue;
        editDataServerUrl.store.load();
		
	},
	/**
	 * 分页类型改变时处理
	 * @private
	 * @param {} owner
	 * @param {} newValue
	 */
	pageTypeChange:function(owner,newValue){
		var me = this;
		var listParamsPanel = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
		var dataObject = listParamsPanel.getComponent('dataObject');
		var pageSize = dataObject.getComponent('pageSize');
		var bufferPage = dataObject.getComponent('bufferPage');
		if(newValue != "1"){
			pageSize.show();
			if(newValue === "5"){
				bufferPage.show();
			}else{
				bufferPage.hide();
			}
		}else{
			pageSize.hide();
			bufferPage.hide();
		}
	},
	/**
	 * 对象树的勾选完后点击确定按钮处理
	 * @private
	 */
	objectPropertyOKClick:function(){
		var me = this;
		
		var dataObject = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix).getComponent('dataObject');
		var ColumnParams = dataObject.getComponent('objectPropertyTree');//对象属性树
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
						CanSort:false,//可排序sss
						OrderNum:1,//排布顺序
						IsLocked:false,//默认锁定
						AlignType:'left',//对齐方式
				    	Width:100//数据项宽度
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
		var dataObject = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix).getComponent('dataObject');
		var objectPropertyTree = dataObject.getComponent('objectPropertyTree');//对象属性树
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
			changeNodes(0);
		}
	},
	//=====================组件的创建与删除=======================
	/**
	 * 新建列表
	 * @private
	 * @return {}
	 */
	createGrid:function(){
		var me = this;
		
		//配置参数
		var params = me.getPanelParams();
		//列
		var columns = me.createColumns();
		//数据集
		var store = me.createStore();
		//是否有菜单选项
		var sortableColumns = me.isSortableColumns();
		//创建分页组件
		var pagingtoolbar = me.createPagingtoolbar(store);
		//功能栏按钮组
		var buttontoobar = me.createButtonToolbar();
        
		var grid = {
			xtype:'grid',
			itemId:'center',
			autoScroll:true,
			title:params.titleText,
			width:parseInt(params.Width),
			height:parseInt(params.Height),            
            //plugins:[rowEditing],  //插入对象
			columns:columns,
			store:store,
			sortableColumns:sortableColumns,
			resizable:{handles:'s e'}
		};       
        
        var strStyle=me.getpanenEditor();
		//行编辑列表
        if(strStyle==='row')
        {
            var rowEditing=Ext.create('Ext.grid.plugin.RowEditing', 
                {
                  clicksToMoveEditor: 2, autoCancel: false,
                  listeners: {
                              canceledit: function (editor, e, eOpts)
                              {                            
                                  var intID=e.record.data.HREmployee_Id;
                              },
                              edit: function (editor, e, eOpts)
                               {
                                //回调函数
                                var callback = function(){
                                grid.store.load();
                               }
                                var record=e.record.data;
                                //var strtext=Ext.JSON.encode(record);                                
                                 me.saveToTable(record,callback);
                                
                                }
                          }                          
                });            
          
            grid.plugins=[rowEditing];
        }        
       
        //单元格编辑列表
        if(strStyle==='column')
        {
            var rowEditing=Ext.create('Ext.grid.plugin.CellEditing', {
                           clicksToEdit: 1,                            
                           listeners: {
                            edit:function(editor, e, eOpts )
                            {
                                var records=e.record
                            }
                            
                           }
                           
                           });
                                       
            grid.plugins=[rowEditing];
        }
		
        //获取单元格保存按钮 //gettoobarSave  
          var dockedItemsSave=me.gettoobarSave();    
        
		//挂靠
		if(buttontoobar || pagingtoolbar){
			grid.dockedItems = [];
               
          
			//功能栏按钮组
			if(buttontoobar){
                if(dockedItemsSave){
                   buttontoobar.items.push(dockedItemsSave) 
                }
                //选择单元格编辑列表方式功能栏按键才出现               
				grid.dockedItems.push(buttontoobar);  
        
			}else
            {
                if(dockedItemsSave)
                {
                    grid.dockedItems.push(dockedItemsSave); 
                }
            }
			//分页组件
			if(pagingtoolbar){
				grid.dockedItems.push(pagingtoolbar);
			}
		}else{
            grid.dockedItems = [];
            if(dockedItemsSave)
                {
                    grid.dockedItems.push(dockedItemsSave); 
                }
        }
		//列表标题栏点击事件
		grid.header = {
			listeners:{
				click:function(){
					//切换组件属性配置面板
					me.switchParamsPanel('center');
				}
			}
		}; 
		//列表面板事件监听
		grid.listeners = {
//            resize( Ext.Component this, Number width, Number height, Number oldWidth, Number oldHeight, Object eOpts )
			resize:function(com,width,height,oldWidth,oldHeight,eOpts){//列表大小变化
				//列表宽度和高度赋值
				var obj = {Width:width,Height:height};
				me.setPanelParams(obj);
			},
			columnresize:{//列宽度改变
	            fn: function(ct,column,width,e,eOpts){
	            	var dataIndex = column.dataIndex;
					me.setColumnWidth(dataIndex,width);
				}
			},
			columnmove:{//列位置移动
				fn: function(ct,column,fromIdx,toIdx,eOpts){
					var dataIndex = column.dataIndex;
					//alert("dataIndex="+dataIndex+";fromIdx="+fromIdx+";toIdx="+toIdx);
					me.setColumnOrderNum(dataIndex,fromIdx+1,toIdx+1);
				}
			},
            edit:function( editor,  e,  eOpts )
            {
                alert("点击编辑事件");
            },
			 itemclick:function(){
                //切换组件属性配置面板
			    var yy='addid';
//			     var southRecords = me.getSouthRecords();
//			        for(var i in southRecords){
//			            var record = southRecords[i];
//			            //添加组件属性面板
//			            me.addParamsPanel(yy,record.get('DisplayName'));
//			        }
			   me.changeParamsPanel();
               me.switchParamsPanel(yy);
          }
                   
		};
		
		return grid;
	},
	/**
	 * 创建数据集
	 * @private
	 * @return {}
	 */
	createStore:function(){
		var me = this;
		//配置参数
		var params = me.getPanelParams();
		//服务地址
		var url = getRootPath() + "/" + me.getListUrl();
		//HQL串
		var where = "";
		if(params.defaultParams){
			where += params.defaultParams;
		}
		if(params.testParams && params.testParams != ""){
			if(where != ""){
				where += " and " + params.testParams;
			}else{
				where += params.testParams;
			}
		}
		url = url + "&where=" + params.defaultParams;
		//数据代理
		var proxy = me.createProxy(url);
		//数据字段
		var fields = me.createFields();
		
		var PageList = params.pageType;//分页模式
		var PageSize = params.pageSize;//每页条数
		var BufferPage = params.bufferPage;//前后页数
		
		var obj = {
			fields:fields,
			proxy:proxy
		};
		if(PageList === "2" || PageList === "3" || PageList === "4"){//数字、滚动、进度条分页
			obj.pageSize = PageSize;
		}else if(PageList === "5"){//无限分页
			obj.pageSize = PageSize;
			obj.buffered = true;
    		obj.leadingBufferZone = BufferPage * PageSize;
		}
		
		var store = Ext.create('Ext.data.Store',obj);
		
		if(url && url != ""){
			store.load();//加载数据
		}else{
			//alert("没有配置获取数据的服务地址！");
			Ext.Msg.alert("提示","没有配置获取数据的服务地址！");
		}
		
		return store;
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
	/**
	 * 创建分页组件
	 * @private
	 * @param {} store
	 * @return {}
	 */
	createPagingtoolbar:function(store){
		var me = this;
		//配置参数
		var params = me.getPanelParams();
		
		var pagingtoolbar = {
			xtype:'pagingtoolbar',
			store:store,
			dock:'bottom',
			displayInfo:true
		};
		
		var PageList = params.pageType;//分页模式
		if(PageList == "1" || PageList == "5"){
			pagingtoolbar = null;
		}else if(PageList == "3"){//滚动分页
			pagingtoolbar.plugins = Ext.create('Ext.ux.SlidingPager',{});
		}else if(PageList == "4"){//进度条分页
			pagingtoolbar.plugins = Ext.create('Ext.ux.ProgressBarPager',{});
		}
		
		return pagingtoolbar;
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
            cmConfig.width = columnParams[i].Width;
            cmConfig.locked = columnParams[i].IsLocked;
            cmConfig.sortable = columnParams[i].CanSort;
            cmConfig.hidden = columnParams[i].IsHidden;
            cmConfig.hideable = !columnParams[i].CannotSee;
            cmConfig.align = columnParams[i].AlignType;
            cmConfig.editor=columnParams[i].Editor;//设置默认列不允许编辑
             
            if(!cmConfig.hideable){//字段不可见
            	cmConfig.hidden = true;
            }
            //设置列编辑、默认不允许编辑
            if(cmConfig.editor)
            {
              cmConfig.editor={allowBlank:true};
            }
                        
			columns.push(cmConfig);
		}
		//操作列
		var actioncolumn = me.createActionColumn();
		if(actioncolumn){
	        columns.push(actioncolumn);
	    }
		return columns;
	},
	/**
	 * 功能栏按钮组
	 * @private
	 * @return {}
	 */
	createButtonToolbar:function(){
		var me = this;
		var values = me.getButtonsConfigValues();
		var arr = [];
		//刷新按钮
		if(values['toolbar-refresh-checkbox']){
			arr.push({
				type:'refresh',
				text:values['toolbar-refresh-text'],
				order:values['toolbar-refresh-number'],
				iconCls:'build-button-refresh'
			});
		}
		//新增按钮
		if(values['toolbar-add-checkbox']){
			arr.push({
				type:'add',
				text:values['toolbar-add-text'],
				order:values['toolbar-add-number'],
				iconCls:'build-button-add'
			});
		}
		//修改按钮
		if(values['toolbar-edit-checkbox']){
			arr.push({
				type:'edit',
				text:values['toolbar-edit-text'],
				order:values['toolbar-edit-number'],
				iconCls:'build-button-edit'
			});
		}
		//查看按钮
		if(values['toolbar-show-checkbox']){
			arr.push({
				type:'show',
				text:values['toolbar-show-text'],
				order:values['toolbar-show-number'],
				iconCls:'build-button-see'
			});
		}
		//删除按钮
		if(values['toolbar-del-checkbox']){
			arr.push({
				type:'del',
				text:values['toolbar-del-text'],
				order:values['toolbar-del-number'],
				iconCls:'build-button-delete'
			});
		}

		var items = [];
		for(var i=1;i<6;i++){
			for(var j in arr){
				if(arr[j].order == i){
					arr[j].handler = function(but,e){
						var records = but.ownerCt.ownerCt.getSelectionModel().getSelection();
                		if(but.type == "del"){
                			for(var i in records){
                				var id = records[i].get(values['winform-combobox']);
                				var callback = function(){
                					but.ownerCt.ownerCt.store.load();
                				}
                				me.deleteInfo(id,callback);
                			}
                		}else if(but.type == "refresh"){
                			but.ownerCt.ownerCt.store.load();
                		}else if(but.type == "add"){
                			me.openFormWin(but.type,-1);
                		}
                        else{
							if(records.length == 1){
								var id = records[0].get(values['winform-combobox']);
								me.openFormWin(but.type,id);
							}else{
								Ext.Msg.alert("提示","请选择一条数据进行操作！");
							}
                		}
	                };
					items.push(arr[j]);
				}
			}
		}
		var com = null;
		if(items.length > 0){
			com = {
				xtype:'toolbar',
				dock:values['toolbar-position'],
				items:items
			};
		}
		return com;
	},
	/**
	 * 创建操作列
	 * @private
	 * @return {}
	 */
	createActionColumn:function(){
		var me = this;
		var values = me.getButtonsConfigValues();
		var arr = [];
		//修改按钮
		if(values['actioncolumn-edit-checkbox']){
			arr.push({
				type:'edit',
				tooltip:values['actioncolumn-edit-text'],
				order:values['actioncolumn-edit-number'],
				iconCls:'build-button-edit hand'
			});
		}
		//查看按钮
		if(values['actioncolumn-show-checkbox']){
			arr.push({
				type:'show',
				tooltip:values['actioncolumn-show-text'],
				order:values['actioncolumn-show-number'],
				iconCls:'build-button-see hand'
			});
		}
		//删除按钮
		if(values['actioncolumn-del-checkbox']){
			arr.push({
				type:'del',
				tooltip:values['actioncolumn-del-text'],
				order:values['actioncolumn-del-number'],
				iconCls:'build-button-delete hand'
			});
		}
		var items = [];
		for(var i=1;i<4;i++){
			for(var j in arr){
				if(arr[j].order == i){
					arr[j].handler = function(grid,rowIndex,colIndex,item,e,record){
                        var strid=values['winform-combobox'];
	                	var id = record.get(values['winform-combobox']);
                		if(item.type == "del"){
                			var callback = function(){
            					grid.store.load();
            				}
            				me.deleteInfo(id,callback);
                		}else{
                			me.openFormWin(item.type,id);
                		}
	                };
					items.push(arr[j]);
				}
			}
		}
		var com = null;
		if(items.length > 0){
			com = {
				xtype:'actioncolumn',text:'操作',width:60,align:'center',
				items:items
			};
		}
		return com;
	},
	//=====================组件属性面板的创建与删除=======================
	
	//=====================弹出窗口=======================
	/**
     * longfc
     * 打开并操作标题字体设置窗体
     * @param {} 
     */
	OpenCategoryWin:function(){
	    var me=this;
	    var xy=me.getPosition();
	    var myxtype=null;
       	if(!myxtype){
	        myxtype=Ext.create('Ext.zhifangux.FontStyleSet', {
		        // title: '字体属性设置',
		        itemId:'vartestobj_id',
		        titleAlign :"center",
		        autoScroll : true,
		        height:270,        //容器高度像素
		        width:460,      //容器宽度像素
		        bodyCls:'bg-white',//控件主体背景样式,默认值'bg-white',为"css/icon.css"里的.bg-white
		        cls:'bg-white',//控件样式设置,默认值'bg-white',为"css/icon.css"里的.bg-white
		        labelcls:'labelcls',//字体属性设置:label样式
		        btnHidden: false,//确定或者取消按钮的显示false或者隐藏true
		        listeners:{
                    //公开的事件
                    onOKCilck:function(o){
                    	//获取设置当前控件的文字属性结果值
                    	var lastValue=this.GetValue();
                      	var obj ={titleStyle:lastValue};
                      	me.setFormValues(obj);
                      	var a = me.getFormParams();
                      	var bm = Ext.getCmp('MyRDS_wintemp');
                      	bm.close();
                    },
                    //公开的事件
                    onCancelCilck:function(o){
                    	//获取设置当前控件的文字属性结果值
                      	var obj ={titleStyle:''};
                      	me.setFormValues(obj);
                      	var a = me.getFormParams();
                      	var bm = Ext.getCmp('MyRDS_wintemp');
                      	bm.close();
                    }
                }
        	});
		}
		me.win=null;
        me.win = Ext.create('widget.window', {
            title:me.winTitle,
            id:"MyRDS_wintemp",
            autoScroll : true,
            border : false,//边框线显示 true,或隐藏false
            width: me.winWidth,
            height:me.winHeight,// me.SetWinWidth(),
            minWidth: me.winWidth,
            minHeight: me.winHeight,
            maxWidth: me.winWidth+5,
            maxHeight: me.winHeight+10,
            x:xy[0]+238,y:xy[1]+30,
            layout: {
                type: 'border',
                padding: 5
            },
            items: [{
            	xtype:myxtype
            }]
        });
        me.win.show();
    },
    /**
     * longfc
     * 打开并操作标题字体设置窗体
     * @param {} rec
     * @param {} valueParam HeadFont ColumnFont
     */
   	OpenCategoryWinTwo:function(rec,valueParam){
	    var me=this;
	    var xy=me.getPosition();
	    var myxtype=null;
       	if(!myxtype){
	        myxtype=Ext.create('Ext.zhifangux.FontStyleSet', {
		        itemId:'OpenCategoryWinTwo_id',
		        titleAlign :"center",
		        autoScroll : true,
		        height:270,        //容器高度像素
		        width:460,      //容器宽度像素
		        bodyCls:'bg-white',//控件主体背景样式,默认值'bg-white',为"css/icon.css"里的.bg-white
		        cls:'bg-white',//控件样式设置,默认值'bg-white',为"css/icon.css"里的.bg-white
		        labelcls:'labelcls',//字体属性设置:label样式
		        btnHidden: false,//确定或者取消按钮的显示false或者隐藏true
		        listeners:{
                	//公开的事件
                    onOKCilck:function(o){
                    	//获取设置当前控件的文字属性结果值
                      	var lastValue=this.GetValue();
                      	//valueParam传入的参数值为HeadFont,设置的为"列头字体内容";传入的参数值为ColumnFont
                      	rec.set(valueParam,lastValue);
                      	rec.commit();
                      	var bm = Ext.getCmp('OpenCategoryWinTwo_id');
                      	bm.close();
                    },
                    //公开的事件
                    onCancelCilck:function(o){
                    	//获取设置当前控件的文字属性结果值
                     	var bm = Ext.getCmp('OpenCategoryWinTwo_id');
                      	bm.close();
                    }
               	}
                
        	});
		}
        me.win2=null;
        me.win2 = Ext.create('widget.window', {
            title:me.winTitle,
            id:"OpenCategoryWinTwo_id",
            autoScroll : true,
            border : false,//边框线显示 true,或隐藏false
            width: me.winWidth,
            height:me.winHeight,// me.SetWinWidth(),
            minWidth: me.winWidth,
            minHeight: me.winHeight,
            maxWidth: me.winWidth+5,
            maxHeight: me.winHeight+10,
            x:xy[0]+238,y:xy[1]+30,
            layout: {
                type: 'border',
                padding: 5
            },
            items: [{
            xtype:myxtype
            }]
        });
        me.win2.show();
    },
	/**
	 * 弹出表单
	 * @private
	 * @param {} type
	 * @param {} id
	 */
	openFormWin:function(type,id){
		var me = this;
		var values = me.getButtonsConfigValues();
		var winId = values['winform-id'];
		
		var callback = function(appInfo){
			if(appInfo && appInfo != ''){
				var ClassCode = appInfo.BTDAppComponents_ClassCode;
				if(ClassCode&&ClassCode!=''){
					var panelParams = {
						type:type,
						dataId:id,
						modal:true,
						floating:true,
						closable:true,
						draggable:true
					};
					var Class = eval(ClassCode);
					var panel = Ext.create(Class,panelParams).show();
					panel.on({
						saveClick:function(){
							panel.close();
							me.getCenterCom().load();
						}
					});
				}else{
					Ext.Msg.alert('提示','没有类代码！');
				} 
			 }
		}; 
		me.getAppInfoFromServer(winId,callback);
	},
    
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
     * 修改对象名称
     * @return {}
     */
    getmodifyFields:function()
    {
        var me=this;
        //获取组建交互字段名称
        var values=me.getButtonsConfigValues();
        
        var southRecords=me.getSouthRecords();
        var fields=[];
        for(var i in southRecords)
        {
            var records=southRecords[i];
            if(records.get('IsEditer')===true)
            {
               fields.push(records.get('InteractionField'));
            }
        }
        return fields;
        
    },
    /**
     * 修改字段名称转换字符串
     * @param {} key
     * @param {} value
     */
    
    getFieldsToString:function()
    {
        var me=this;
        //获取组建交互字段名称
        var values=me.getButtonsConfigValues();
        
        var southRecords=me.getSouthRecords();
        var fieldsString="";
        for(var i in southRecords)
        {
            var records=southRecords[i];
            if(records.get('IsEditer')===true)
            {
               //fields.push(records.get('InteractionField'));
                fieldsString=fieldsString+"'"+records.get('InteractionField')+"',";
            }
        }
        fieldsString=fieldsString.substring(0,fieldsString.length-1)
        return fieldsString;
        
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
	    	{name:'OrderNum',type:'int'},//排布顺序
	    	{name:'Width',type:'int'},//列表头宽
	    	{name:'IsLocked',type:'bool'},//默认锁定
	    	{name:'AlignType',type:'string'},//对齐方式
	    	{name:'HeadFont',type:'string'},//列头字体
	    	{name:'ColumnFont',type:'string'},//列字体
	    	{name:'ColumnType',type:'string'}//列类型
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
		var paramsPanel = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
        var dataObject = paramsPanel.getComponent('dataObject');
        var objectPropertyTree = dataObject.getComponent('objectPropertyTree');
		
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
			var getDataServerUrl = dataObject.getComponent('getDataServerUrl');
			getDataServerUrl.value = panelParams.getDataServerUrl;
			//获取新增数据服务列表
			var addDataServerUrl = dataObject.getComponent('addDataServerUrl');
			addDataServerUrl.value = panelParams.addDataServerUrl;
			//获取修改数据服务列表
			var editDataServerUrl = dataObject.getComponent('editDataServerUrl');
			editDataServerUrl.value = panelParams.editDataServerUrl;
			//设置删除服务列表下拉框赋值
			var buttonsconfig = paramsPanel.getComponent('buttonsconfig');
	       	var delservercombobox = buttonsconfig.getComponent('del-server-combobox');
			delservercombobox.value = panelParams['del-server-combobox'];
			//按钮设置交互字段
			me.changeButSetWinForm();
			buttonsconfig.setWinFormComboboxValue(panelParams['winform-combobox']);
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
	        record.OrderNum = item.get('OrderNum');
	        record.Width = item.get('Width');
	        record.AlignType = item.get('AlignType');
            record.Editor=item.get('IsEditer');                   
	        
	        myItems.push(record);
			//myItems.push(items[map[i].Index]);
		}
		
		return myItems;
	},
	/**
	 * 菜单选项内容是否可见
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
			
			item.set('Width',width);
			item.commit();
		}
	},
	
	/**
	 * 给数据对象列表赋值
	 * @private
	 */
	setObjData:function(){
		var me = this;
		var paramsPanel = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
		//数据对象
		var dataObject = paramsPanel.getComponent('dataObject');
		//数据对象类
		var objectName = dataObject.getComponent('objectName');
		objectName.store.load();
	},
	/**
	 * 获取按钮组设置信息
	 * @private
	 * @return {}
	 */
	getButtonsConfigValues:function(){
		var me = this;
		var fieldset = me.getFunButFieldSet();
		var values = fieldset.getFieldSetValues();
		return values;
	},
	/**
	 * 获取按钮组设置组件
	 * @private
	 * @return {}
	 */
	getFunButFieldSet:function(){
		var me = this;
		var formParamsPanel = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
		var com = formParamsPanel.getComponent('buttonsconfig');
		return com;
	},
	/**
	 * 按钮组设置赋值
	 * @private
	 * @param {} panelParams
	 */
	setButSetValues:function(panelParams){
		var me = this;
		var funbut = me.getFunButFieldSet();
		funbut.setDelServerValue(panelParams['del-server-combobox']);
		funbut.setWinFormComboboxValue(panelParams['winform-combobox']);
	},
	/**
	 * 交互字段列表变化
	 * @private
	 */
	changeButSetWinForm:function(){
		var me = this;
		var store = me.getComponent('south').store;
		var funbut = me.getFunButFieldSet();
		var s = store;
		funbut.setWinformStore(s);
	},
    //获取列表编辑方式
    getpanenEditor:function(){
        
        var me=this;
        var panenl=me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix).getComponent('panelEditorType');
        
        var editorModel=panenl.getComponent('panelEditor1');
        
        var strModel=editorModel.getValue();
        return strModel;
    },
    
   //设置列表编辑方式 
    getEditorType:function(){
        var me=this,
            style;
        var strStyle=me.getpanenEditor();
        if(strStyle==='')
        {style=Ext.create('Ext.grid.plugin.CellEditing', {
                  clicksToEdit: 1 });}
        if(strStyle==='row')
        {
           style=Ext.create('Ext.grid.plugin.RowEditing', {
                 clicksToMoveEditor: 2,autoCancel: false});
        }
        if(strStyle==='column')
        {
            style=Ext.create('Ext.grid.plugin.CellEditing', {
                  clicksToEdit: 1 });
        }
        return style;
    },
    
    /**
     * 在列编辑方式时添加功能栏保存按钮
     * @return {}
     */
    gettoobarSave:function()
    {
        var me=this;
        var butSave="";
        var strStyle=me.getpanenEditor();
        if(strStyle==='column')
               {
                 butSave={
                xtype:'button',text:'修改保存' ,iconCls:'build-button-save',margin:'0 0 0 2',
                itemId:'save-button',name:'save-button', 
                listeners:{click:function(but,e,eOpts){                   
                                var formParamsPanel = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
                                var dataObject = formParamsPanel.getComponent('dataObject');
                                var objectName = dataObject.getComponent('objectName');
                   
                                  //修改保存（交互字段ID）ID                                   
                                   var editer_id =objectName.value+"_Id";
                                   var editerFields="";
                                   var gridpanel=me.getCenterCom();
                                   var arrFields=me.getmodifyFields();    //获取修改列
                                   if(editer_id===""||editer_id===null)
                                   {
                                     Ext.Msg.alert("提示","请选择交互字段进行操作！");
                                     return;
                                   }else{                                  
                                      arrFields.push(editer_id);
                                   }
                                   var strCount=gridpanel.store.getModifiedRecords();//获取修改后的行记录                                   
                                   for(var i=0;i<strCount.length;i++)
                                   {
                                      for(var j=0;j<arrFields.length;j++)
                                      {
                                        editerFields=editerFields+arrFields[j]+":'"+strCount[i].get(arrFields[j])+"',";
                                      }
                                      editerFields=editerFields.substring(0,editerFields.length-1);
                                      editerFields="{"+editerFields+"}";                                      
                                      var editerJSON=Ext.JSON.decode(editerFields);
                                      
                                      var callback = function(){
                                          but.ownerCt.ownerCt.store.load();
                                      }
                                      me.saveToTable(editerJSON,callback);
                                      editerFields="";
                                   } 
                }
                }};
                //添加保存功能按键
                //buttontoobar.items.push(butSave)
             } 
             return butSave;
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
		var appClass = 
		"Ext.Loader.setPath('Ext.ux',getRootPath()+'/ui/extjs/ux');" + 
		"Ext.define('" + params.appCode + "',{" + 
			"extend:'Ext.grid.Panel'," + 
			"alias:'widget." + params.appCode + "'," + 
			"title:'" + params.titleText + "'," + 
			"width:" + params.Width + "," + 
			"height:" + params.Height + "," + 
			"objectName:'" + params.objectName +"'," + //对象名，用于自动主键匹配
			"internalWhere:'" + params.defaultParams + "'," + //内部hql
			"externalWhere:''," + //外部hql
			//默认选中,true:选中'false:不选;也可以是下标,从0开始,当数字超过最大的数据行时选中最后一行;也可以是负数,例如-1就是选中length-1行数据
			"autoSelect:true," + 
			"deleteIndex:-1," + //被删除的行下标号
			"autoScroll:true," ;
            
            appClass=appClass+"afterRender:function(){" + 
                "var me=this;" + 
                "me.callParent(arguments);" + 
                //加载数据后默认选中第一行
				"me.store.on({" + 
					"load:function(store,records,successful){" + 
						"var autoSelect=me.autoSelect;" + 
						"if(successful&&records.length>0){" + 
							"if(me.deleteIndex&&me.deleteIndex!=''&&me.deleteIndex!=-1){" + 
								"if(records.length-1>me.deleteIndex){" + 
									"me.getSelectionModel().select(me.deleteIndex);" + //选中删除下标的那一行
								"}else{" + 
									"me.getSelectionModel().select(records.length-1);" + //选中最后一行
								"}" + 
								"me.view.getEl().focus();" + //定位光标
								"me.deleteIndex=-1;" + //还原
							"}else{" + 
								"if(autoSelect){" + 
									"if(autoSelect === true){" + 
				                		"me.getSelectionModel().select(0);" + 
				                	"}else{" + //下标
				                		"var num=0;" + 
				                		"if(autoSelect >= 0){" + 
				                			"num=autoSelect%records.length;" + 
				                		"}else{" + 
				                			"num=length-Math.abs(num)%length;" + 
				                		"}" + 
				                		"me.getSelectionModel().select(num);" + 
				                	"}" + 
				                	"me.view.getEl().focus();" + //定位光标
								"}" + 
							"}" + 
		                "}" +  
					"}" + 
				"});" + 
                "if(Ext.typeOf(me.callback)=='function'){me.callback(me);}" + 
               "}," ;
            appClass=appClass+
			"initComponent:function(){" + 
				"var me=this;" + 
				"me.url=getRootPath()+'/" + me.getListUrl() + "';" + 
				//数据集合
				"me.store=" + me.createStoreStr() + ";" + 
				//加载数据的方法
				"me.load=function(where){" + 
					"me.externalWhere=where;" + 
					"var w='';" + 
					"if(me.internalWhere){" + 
						"w+=me.internalWhere;" + 
					"}" + 
					"if(where&&where!=''){" + 
						"if(w!=''){" + 
							"w+=' and '+where;" + 
						"}else{" + 
							"w+=where;" + 
						"}" + 
					"}" + 
					"me.store.proxy.url=me.url+'&where='+w;" + 
					"me.store.load();" + 
				"};" + 
				//删除数据的方法
				"me.deleteInfo=" + me.createDeleteInfoStr() + ";" + 
                
                //行、列编辑的保存方法
                "me.saveToTable="+me.createSaveToTableStr()+";"+
				//弹出表单的方法
				"me.openFormWin=" + me.createOpenFormWinStr() + ";" + 
                //列表编辑方式
                me.createEditorCode()+";"+
				//数据列
			  	"me.columns=" + me.createColumnsStr() + ";";
			  	//挂靠
				var dockedItems = me.createDockedItemsStr();
			  	if(dockedItems != ""){
					appClass = appClass + "me.dockedItems=" + dockedItems + ";";
				}
				//根据ID从后台获取应用信息的方法
				var values = me.getButtonsConfigValues();
				if(values['winform-checkbox']){
					appClass = appClass + 
					"me.getAppInfoServerUrl=getRootPath()+'/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById';" + 
					"me.getAppInfoFromServer=function(id,callback){" + 
						"var me=this;" + 
						"var url=me.getAppInfoServerUrl+'?isPlanish=true&id='+id;" + 
						"Ext.Ajax.defaultPostHeader='application/json';" + 
						"Ext.Ajax.request({" + 
							"async:false," + 
							"url:url," + 
							"method:'GET'," + 
							"timeout:2000," + 
							"success:function(response,opts){" + 
								"var result=Ext.JSON.decode(response.responseText);" + 
								"if(result.success){" + 
									"var appInfo='';" + 
									"if(result.ResultDataValue&&result.ResultDataValue!=''){" + 
										"appInfo=Ext.JSON.decode(result.ResultDataValue);" + 
									"}" + 
									"if(appInfo!=''){" + 
										"if(Ext.typeOf(callback)=='function'){" + 
											"callback(appInfo);" + 
										"}" + 
									"}else{" + 
										"Ext.Msg.alert('提示','没有获取到应用组件信息！');" + 
									"}" + 
								"}else{" + 
									"Ext.Msg.alert('提示','获取应用组件信息失败！错误信息【<b style=\\\"color:red\\\">'+result.ErrorInfo+'</b>】');" + 
								"}" + 
							"}," + 
							"failure:function(response,options){" + 
								"Ext.Msg.alert('提示','获取应用组件信息请求失败！');" + 
							"}" + 
						"});" + 
					"};";
				}
				
				//公开监听事件
				appClass = appClass + me.createEvent();
				appClass = appClass + "this.callParent(arguments);" + 
			"}" + 
		"});";
		return appClass;
	},
	/**
	 * 创建类代码数据集
	 * @private
	 * @return {}
	 */
	createStoreStr:function(){
		var me = this;
		//配置参数
		var params = me.getPanelParams();
		//数据字段
		var fieldsArr = me.createFields();
		var fields = "";
		for(var i in fieldsArr){
			fields = fields + "'" + fieldsArr[i] + "',";
		}
		if(fields.length > 0){
			fields = fields.substring(0,fields.length-1);
		}
		
		var PageList = params.pageType;//分页模式
		var PageSize = params.pageSize;//每页条数
		var BufferPage = params.bufferPage;//前后页数
		
		var autoLoad = (params.autoLoad == '1' ? true : false);
		var storeStr = 
		"Ext.create('Ext.data.Store',{" + 
			"fields:[" + fields + "]," + 
			"autoLoad:" + autoLoad + ",";
		
		if(PageList === "2" || PageList === "3" || PageList === "4"){//数字、滚动、进度条分页
			storeStr = storeStr + 
			"pageSize:" + PageSize + ",";
		}else if(PageList === "5"){//无限分页
			storeStr = storeStr + 
			"pageSize:" + PageSize + "," + 
			"buffered:true," + 
			"leadingBufferZone:" + BufferPage * PageSize + ",";
		}
		storeStr = storeStr +  
			"proxy:{" + 
	            "type:'ajax'," + 
	            "url:getRootPath()+'/" + me.getListUrl() + "'," + 
	            "reader:{" + 
	            	"type:'json'," + 
	            	"root:'list'," + 
	            	"totalProperty:'count'" + 
	            "}," + 
	            //内部数据匹配方法
	            "extractResponseData:function(response){" + 
			    	"var data = Ext.JSON.decode(response.responseText);" + 
			    	"if(data.ResultDataValue && data.ResultDataValue != ''){" + 
			    		"var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);" + 
				    	"data.list = ResultDataValue.list;" + 
				    	"data.count = ResultDataValue.count;" + 
			    	"}else{" + 
			    		"data.list = [];" + 
			    		"data.count = 0;" + 
			    	"}" + 
			    	"response.responseText = Ext.JSON.encode(data);" + 
			    	"return response;" + 
			  	"}" +  
	        "}" + 
	    "})";
		
		return storeStr;
	},
	/**
	 * 创建挂靠
	 * @private
	 * @return {}
	 */
	createDockedItemsStr:function(){
		var me = this;
		//挂靠
		var dockedItemds = "";
		//分页代码
		var pagingtoolbar = me.createPagingtoolbarStr();
		if(pagingtoolbar != ""){
			dockedItemds += pagingtoolbar + ",";
		}
		//生成功能栏按钮组代码
		var funbut = me.createFunButStr();
		if(funbut != ""){
			dockedItemds += funbut + ",";
		}
		
		if(dockedItemds != ""){
			dockedItemds = "[" + dockedItemds.substring(0,dockedItemds.length-1) + "]";
		}
		return dockedItemds;
	},
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
				"width:" + columnParams[i].Width + "," + 
				"locked:" + columnParams[i].IsLocked + "," + 
				"sortable:" + columnParams[i].CanSort + "," + 
				"hidden:" + columnParams[i].IsHidden + "," + 
				"hideable:" + columnParams[i].CannotSee + "," + 
                //"editor:{allowBlank:false},"+   //是否可以修改行或单元格
                colEditer+
				"align:'" + columnParams[i].AlignType + "'" + 
			"}";
			columnsStr = columnsStr + col + ",";
		}
		//操作列代码
		var actioncolumn = me.createActionColumnStr();
		if(actioncolumn != ""){
	        columnsStr += actioncolumn + ",";
	    }
	    
		if(columnsStr != ""){
			columnsStr = columnsStr.substring(0,columnsStr.length-1);
		}
		columnsStr = "[" + columnsStr + "]";
		return columnsStr;
	},
	/**
	 * 分页代码
	 * @private
	 * @return {}
	 */
	createPagingtoolbarStr:function(){
		var me = this;
		var pagingtoolbar = "";
		
		var params = me.getPanelParams();//配置参数
		var PageList = params.pageType;//分页模式
		if(PageList == "2"){//数字分页
			pagingtoolbar =	
			"{" + 
				"xtype:'pagingtoolbar'," + 
				"store:me.store," + 
				"dock:'bottom'," + 
				"displayInfo:true" + 
			"}";
		}else if(PageList == "3"){//滚动分页
			pagingtoolbar =	
			"{" + 
				"xtype:'pagingtoolbar'," + 
				"store:me.store," + 
				"dock:'bottom'," + 
				"plugins:Ext.create('Ext.ux.SlidingPager',{})," + 
				"displayInfo:true" + 
			"}";
		}else if(PageList == "4"){//进度条分页
			pagingtoolbar =	
			"{" + 
				"xtype:'pagingtoolbar'," + 
				"store:me.store," + 
				"dock:'bottom'," + 
				"plugins:Ext.create('Ext.ux.ProgressBarPager',{}),"
				"displayInfo:true" + 
			"}";
		}
		return pagingtoolbar;
	},
	/**
	 * 生成功能栏按钮组代码
	 * @private
	 * @return {}
	 */
	createFunButStr:function(){
		var me = this;
		var values = me.getButtonsConfigValues();
         var strStyle=me.getpanenEditor();
//        if(strStyle===''||strStyle==='row')
//        {
//            var value ="";
//           return  ; 
//        }
		var arr = [];
		//刷新按钮
		if(values['toolbar-refresh-checkbox']){
			var value = 
			"{" +
				"type:'refresh'," + 
				"text:'" + values['toolbar-refresh-text'] + "'," + 
				"iconCls:'build-button-refresh'," + 
				"handler:function(but,e){" + 
					"var com = but.ownerCt.ownerCt;" + 
					"com.store.load(com.externalWhere);" + 
				"}" + 
			"}";
			arr.push({
				key:values['toolbar-refresh-number'],
				value:value
			});
		}
		//新增按钮
		if(values['toolbar-add-checkbox']){
			var value = 
			"{" +
				"type:'add'," + 
				"text:'" + values['toolbar-add-text'] + "'," + 
				"iconCls:'build-button-add'," + 
				"handler:function(but,e){";
			if(values['winform-checkbox']){
				value += 
					"var records = me.getSelectionModel().getSelection();" + "me.openFormWin(but.type,-1);";
			}else{
				value += 
					"me.fireEvent('addClick');";
			}
				value += 
				"}" + 
			"}";
			arr.push({
				key:values['toolbar-add-number'],
				value:value
			});
		}
		//修改按钮
		if(values['toolbar-edit-checkbox']){
			var value = 
			"{" +
				"type:'edit'," + 
				"text:'" + values['toolbar-edit-text'] + "'," + 
				"iconCls:'build-button-edit'," + 
				"handler:function(but,e){";
			if(values['winform-checkbox']){
				value += 
					"var records = me.getSelectionModel().getSelection();" + 
					"if(records.length == 1){" + 
						"var id = records[0].get('" + values['winform-combobox'] + "');" + 
						"me.openFormWin(but.type,id);" + 
					"}else{" + 
						"Ext.Msg.alert('提示','请选择一条数据进行操作！');" + 
					"}";
			}else{
				value += 
					"me.fireEvent('editClick');";
			}
				value += 
				"}" + 
			"}";
			arr.push({
				key:values['toolbar-edit-number'],
				value:value
			});
		}
		//查看按钮
		if(values['toolbar-show-checkbox']){
			var value = 
			"{" +
				"type:'show'," + 
				"text:'" + values['toolbar-show-text'] + "'," + 
				"iconCls:'build-button-see'," + 
				"handler:function(but,e){";
			if(values['winform-checkbox']){
				value += 
					"var records = me.getSelectionModel().getSelection();" + 
					"if(records.length == 1){" + 
						"var id = records[0].get('" + values['winform-combobox'] + "');" + 
						"me.openFormWin(but.type,id);" + 
					"}else{" + 
						"Ext.Msg.alert('提示','请选择一条数据进行操作！');" + 
					"}";
			}else{
				value += 
					"me.fireEvent('showClick');";
			}
				value += 
				"}" + 
			"}";
			arr.push({
				key:values['toolbar-show-number'],
				value:value
			});
		}
		//删除按钮
		if(values['toolbar-del-checkbox']){
			var value = 
			"{" +
				"type:'del'," + 
				"text:'" + values['toolbar-del-text'] + "'," + 
				"iconCls:'build-button-delete'," + 
				"handler:function(but,e){" +      //如果不是单元格编辑公开事件
					"var list = but.ownerCt.ownerCt;" + 
					"var records = list.getSelectionModel().getSelection();" + 
					"if(records.length==1){" + 
						"Ext.Msg.confirm('警告','确定要删除吗？',function (button){" + 
							"if(button == 'yes'){" + 
								"var records = me.getSelectionModel().getSelection();" + 
								"for(var i in records){" + 
									"var id = records[i].get('" + values['winform-combobox'] + "');" + 
									"var callback = function(){" + 
										"me.deleteIndex=rowIndex;me.load();me.fireEvent('delClick');" + 
									"};" + 
									"me.deleteInfo(id,callback);" +  
								"}" + 
							"}" + 
						"});" + 
					"}else{Ext.Msg.alert('提示','请选择一条数据进行操作！');}" + 
				"}" + 
			"}";
			arr.push({
				key:values['toolbar-del-number'],
				value:value
			});
		}
        //单元格编辑才出现保存按钮
        if(strStyle==='column')
         {
            var value = 
            "{" +
            "xtype:'button',text:'修改保存' ,iconCls:'build-button-save',margin:'0 0 0 2',"+
            "itemId:'save-button',name:'save-button',"+
            "listeners:{click:function(but,e,eOpts){ "+
            "var editer_id =me.objectName"+"+"+"'_Id';"+
            "var editerFields='';"+
                     "var arrFields=["+me.getFieldsToString()+"];"+    //获取修改列                     
                       "if(editer_id===''||editer_id===null)"+
                          "{"+
                             "Ext.Msg.alert('提示','请选择交互字段进行操作！');"+
                            "return;"+
                          "}else{"+
                          "arrFields.push(editer_id);}"+
                          "var strCount=me.store.getModifiedRecords(); "+   //获取修改后的行记录    
                          
                          "Ext.Msg.confirm('警告','确定要修改保存吗？',function (button){" + 
                            "if(button == 'yes'){" + 
                          
                               "for(var i=0;i<strCount.length;i++)"+
                                "{"+
                                "   for(var j=0;j<arrFields.length;j++)"+
                                "    {"+                                  
                                      "editerFields=editerFields+arrFields[j]+"+"\\\":'\\\""+"+strCount[i].get(arrFields[j])+"+"\\\"',\\\""+
                                "    }"+
                                "      editerFields=editerFields.substring(0,editerFields.length-1);"+
                                "      editerFields='{'+editerFields+'}';"+  
                                "      var editerJSON=Ext.JSON.decode(editerFields);"+ 
                                "      me.saveToTable(editerJSON);"+
                                "      editerFields='';"+
                                " }"+  
                            
                           "me.store.load();" +   //me.fireEvent('delClick');
                           
                           "}else{me.store.load();}" +                       
                          "});" +    
                          "}}}";
                 
                arr.push({
                key:6,
                value:value
            });
         }       
		var itemsStr = "";
		for(var i=1;i<7;i++){
			for(var j in arr){
				if(arr[j].key == i){
					itemsStr += arr[j].value + ",";
				}
			}
		}
		if(itemsStr != ""){
			itemsStr = itemsStr.substring(0,itemsStr.length-1);
		}
		var toolbarStr = "";
		if(itemsStr != ""){
			toolbarStr = 
			"{" + 
				"xtype:'toolbar'," + 
				"dock:'" + values['toolbar-position'] + "'," + 
				"items:[" + itemsStr + "]" + 
			"}";
		}
		return toolbarStr;
	},
	/**
	 * 操作列代码
	 * @private
	 * @return {}
	 */
	createActionColumnStr:function(){
		var me = this;
		var values = me.getButtonsConfigValues();
		var arr = [];
		//修改按钮
		if(values['actioncolumn-edit-checkbox']){
			var value = 
			"{" +
				"type:'edit'," + 
				"tooltip:'" + values['actioncolumn-edit-text'] + "'," + 
				"iconCls:'build-button-edit hand'," + 
				"handler:function(grid,rowIndex,colIndex,item,e,record){";
			if(values['winform-checkbox']){
				value += 
					"var id = record.get('" + values['winform-combobox'] + "');" + "me.openFormWin(item.type,id);";
			}else{
				value += 
					"me.fireEvent('editClick');";
			}
				value += 
				"}" + 
			"}";
			arr.push({
				key:values['actioncolumn-edit-number'],
				value:value
			});
		}
		//查看按钮
		if(values['actioncolumn-show-checkbox']){
			var value = 
			"{" +
				"type:'show'," + 
				"tooltip:'" + values['actioncolumn-show-text'] + "'," + 
				"iconCls:'build-button-see hand'," + 
				"handler:function(grid,rowIndex,colIndex,item,e,record){";
			if(values['winform-checkbox']){
				value += 
					"var id = record.get('" + values['winform-combobox'] + "');" + "me.openFormWin(item.type,id);";
			}else{
				value += 
					"me.fireEvent('showClick');";
			}
				value += 
				"}" + 
			"}";
			arr.push({
				key:values['actioncolumn-show-number'],
				value:value
			});
		}
		//删除按钮
		if(values['actioncolumn-del-checkbox']){
			var value = 
			"{" +
				"tooltip:'" + values['actioncolumn-del-text'] + "'," + 
				"iconCls:'build-button-delete hand'," + 
				"handler:function(grid,rowIndex,colIndex,item,e,record){" + 
					"Ext.Msg.confirm('警告','确定要删除吗？',function (button){" + 
						"if(button == 'yes'){" + 
							"var id = record.get('" + values['winform-combobox'] + "');" + 
							"var callback=function(){me.deleteIndex=rowIndex;me.load();me.fireEvent('delClick');};" + 
							"me.deleteInfo(id,callback);" + 
						"}" + 
					"});" + 
				"}" + 
			"}";
			arr.push({
				key:values['actioncolumn-del-number'],
				value:value
			});
		}
		
		var itemsStr = "";
		for(var i=1;i<4;i++){
			for(var j in arr){
				if(arr[j].key == i){
					itemsStr += arr[j].value + ",";
				}
			}
		}
		if(itemsStr != ""){
			itemsStr = itemsStr.substring(0,itemsStr.length-1);
		}
		var actioncolumn = "";
		if(itemsStr != ""){
			actioncolumn = 
			"{" + 
				"xtype:'actioncolumn',text:'操作',width:60,align:'center'," + 
				"items:[" + itemsStr + "]" + 
			"}";
		}
		return actioncolumn;
	},
	/**
	 * 弹出表单代码
	 * @private
	 * @return {}
	 */
	createOpenFormWinStr:function(){
		var me = this;
		var values = me.getButtonsConfigValues();
		var winId = values['winform-id'];
		var fun = "function(type,id){};";
		if(values['winform-checkbox']){
			fun = 
			"function(type,id){" + 
				"var winId='" + winId +"';" + 
				"var callback=function(appInfo){" + 
					"if(appInfo&&appInfo!=''){" + 
						"var ClassCode=appInfo.BTDAppComponents_ClassCode;" + 
						"if(ClassCode&&ClassCode!=''){" + 
							"var panelParams = {" + 
								"type:type," + 
								"dataId:id," + 
								"modal:true," + 
								"floating:true," + 
								"closable:true," + 
								"draggable:true" + 
							"};" + 
							"var Class = eval(ClassCode);" + 
							"var panel = Ext.create(Class,panelParams).show();" + 
							"panel.on({saveClick:function(){panel.close();me.load();me.fireEvent('saveClick');}});" + 
						"}else{" + 
							"Ext.Msg.alert('提示','没有类代码！');" + 
						"}" + 
					 "}" + 
				"};" + 
				"me.getAppInfoFromServer(winId,callback);" + 
			"}"; 
		}
		return fun;
	},
	/**
	 * 删除数据代码
	 * @private
	 * @return {}
	 */
	createDeleteInfoStr:function(){
		var me = this;
		var values = me.getButtonsConfigValues();
		var fun = "function(id,callback){}";
		if(values['del-server-combobox']){
			fun = "function(id,callback){" + 
				"var url = getRootPath()+'/" + values['del-server-combobox'].split("?")[0] + "?id='+id;" + 
				"Ext.Ajax.defaultPostHeader = 'application/x-www-form-urlencoded';" + 
				"Ext.Ajax.request({" + 
					"async:false," + 
					"url:url," + 
					"method:'GET'," + 
					"timeout:2000," + 
					"success:function(response,opts){" + 
						"var result = Ext.JSON.decode(response.responseText);" + 
						"if(result.success){" + 
							"if(Ext.typeOf(callback)=='function'){" + 
								"callback();" + 
							"}" + 
						"}else{" + 
							"Ext.Msg.alert('提示','删除信息失败！错误信息【<b style=\\\"color:red\\\">'+result.ErrorInfo+'</b>】');" + 
						"}" + 
					"}," + 
					"failure:function(response,options){ " + 
						"Ext.Msg.alert('提示','删除信息请求失败！');" + 
					"}" + 
				"});" + 
			"}";
		}
		
		
		return fun;
	},
    
    /**
     *行、列编辑保存方法
     * @return {}
     */
    createSaveToTableStr:function()
    {
        var me=this;
        //获取属性面板ID
        var paramsPanel = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
        
        //获取数据对象ID
        var dataObjectgroup=paramsPanel.getComponent("dataObject");
        //获取修改服务URL
        var updateUrl=dataObjectgroup.getComponent("editDataServerUrl");
        
        var myUrl=updateUrl.getValue(me.objectServerValueField);
        
        fun="function(strobj){"+
            "var myUrl='"+myUrl+"';"+
            "if(myUrl!=''){"+
            "var url = getRootPath()+'/"+myUrl+"';"+
            "}else{"+
            "Ext.Msg.alert('提示','<b style=\\\"color:red\\\">'+'【没有配置获取数据服务地址！】</b>');"+
            " return null;"+
            "}"+
            "var values=strobj;"+
            "var maxLength = 0;" + //最大的层数
            "for(var i in values){" + 
                "var arr = i.split('_');" + 
                "if(arr.length > maxLength){" + 
                    "maxLength = arr.length;" + 
                "}" + 
            "}" + 
            
            "var obj = {};" + 
            "var addObj = function(key,num,value){" + 
                "var keyArr = key.split('_');" + //键
                "var ob = 'obj';" + 
                "for(var i=1;i<keyArr.length;i++){" + 
                    "ob = ob + '[\\\"' + keyArr[i] + '\\\"]';" + 
                    "if(!eval(ob)){" + //对象不存在
                        "eval(ob + '={};');" + 
                    "}" +  
                "}" + 
                "if(keyArr.length == num+1){" + //当前层赋值
                    "eval(ob + '=value;');" + 
                "}" + 
            "};" + 
            
            "for(var i=1;i<maxLength;i++){" + 
                "for(var j in values){" + 
                    "var value = values[j];" + //值
                    "addObj(j,i,value);" + //键、层、值
                "}" + 
            "}" + 
            
            "var field = '';" + 
            
            "for(var i in values){" + 
                "var keyArr = i.split('_');" + 
                "field = field + keyArr.slice(1).join('_') + ',';" + 
            "}" + 
            "if(field != ''){" + 
                "field = field.slice(0,-1);" + 
            "}" + 
            
            "Ext.Ajax.defaultPostHeader = 'application/json';" + 
            "Ext.Ajax.request({" + 
                    "async:false," + 
                    "url:url," + 
                    //"params:Ext.JSON.encode(obj),"+
                    "params:Ext.JSON.encode({entity:obj,fields:field}),"+
                    "method:'POST'," + 
                    "timeout:5000," + 
                    "success:function(response,opts){" + 
                        "var result = Ext.JSON.decode(response.responseText);" + 
                        "if(result.success){" + 
                            //"if(Ext.typeOf(callback)=='function'){" + 
                            //    "callback();" + 
                            //"}" + 
                        "Ext.Msg.alert('提示','保存成功！');"+
                        "}else{" + 
                            "Ext.Msg.alert('提示','保存信息失败！错误信息【<b style=\\\"color:red\\\">'+result.ErrorInfo+'</b>】');" + 
                        "}" + 
                    "}," + 
                    "failure:function(response,options){ " + 
                        "Ext.Msg.alert('提示','保存信息请求失败！');" + 
                    "}" + 
                "});" + 
            "}";
            
            return fun;
    },
	/**
	 * 创建监听代码
	 * @private
	 * @return {}
	 */
	createEvent:function(){
		var me = this;
		var values = me.getButtonsConfigValues();
		var com = "me.fireEvent('saveClick');";
		//新增
		if(values['toolbar-add-checkbox']){
			com += "me.addEvents('addClick');";
		}
		//修改
		if(values['toolbar-eidt-checkbox'] || values['actioncolumn-edit-checkbox']){
			com += "me.addEvents('editClick');";
		}
		//查看
		if(values['toolbar-show-checkbox'] || values['actioncolumn-show-checkbox']){
			com += "me.addEvents('showClick');";
		}
		//删除
		if(values['toolbar-del-checkbox'] || values['actioncolumn-del-checkbox']){
			com += "me.addEvents('delClick');";
		}
		return com;
	},
    
    /**
     * 创建编辑方式代码
     * @param {} callback
     */
    createEditorCode:function()
    {
        var me=this;
        var modelcode='';
        var strStyle=me.getpanenEditor();
        if(strStyle==='')
        {
            modelcode="";
           return modelcode; 
        }
        //行编辑列表
        if(strStyle==='row')
        {
            modelcode="var rowEditing=Ext.create('Ext.grid.plugin.RowEditing', {"+
                           "clicksToMoveEditor: 2, autoCancel: false," +                           
                           "listeners: {"+
                           "  canceledit: function (editor, e, eOpts)"+
                           "  {"+
                           "   },"+
                           "   edit: function (editor, e, eOpts){"+                       
                           //"var callback = function(){"+
                           //"me.fireEvent('saveClick');"+                           
                           //"}"+
                           
                           //"var objName={'entity':{ 'CName':'张三','Id': '1','NameF': '三', 'NameL':'张','SName':'张122222' },'fields':'CName,Id,NameF,NameL,SName'};"+
                           //"me.saveToTable(objName);"+
                           "var records=e.record.data;"+
                           "me.saveToTable(records);"+
                           "me.store.load();"+
                           "}}"+
                           "});";
            modelcode=modelcode+"me.plugins=[rowEditing];"
        }
        
        //单元格编辑列表
        if(strStyle==='column')
        {
            modelcode="var rowEditing=Ext.create('Ext.grid.plugin.CellEditing', {"+
                           "clicksToEdit: 1 });";
            modelcode=modelcode+"me.plugins=[rowEditing];";
        }
        
        return modelcode;
        
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
	/**
	 * 根据ID删除数据
	 * @private
	 * @param {} id
	 * @param {} callback
	 */
	deleteInfo:function(id,callback){
		var me = this;
		var values = me.getButtonsConfigValues();
        var urlvalues=values['del-server-combobox'];
		var url = getRootPath()+"/"+values['del-server-combobox'].split("?")[0]+"?id="+id;
		Ext.Ajax.defaultPostHeader = 'application/x-www-form-urlencoded';
		Ext.Ajax.request({
			async:false,//非异步
			url:url,
			method:'GET',
			timeout:2000,
			success:function(response,opts){
				var result = Ext.JSON.decode(response.responseText);
				if(result.success){
					if(Ext.typeOf(callback) == "function"){
						callback();//回调函数
					}
				}else{
					Ext.Msg.alert('提示','删除信息失败！错误信息【<b style="color:red">'+ result.ErrorInfo +"</b>】");
				}
			},
			failure:function(response,options){ 
				Ext.Msg.alert('提示','删除信息请求失败！');
			}
		});
	},
    
    /**
     * 获取JSON串
     * @param {} obj
     * @return {}
     */
    getFieldsJson:function(str){
         var values=str;
              var maxLength = 0;
                for(var i in values)
                  {
                        var arr = i.split('_');
                        if(arr.length > maxLength)
                         {
                            maxLength = arr.length;
                   }}
                 var obj = {};
                 var addObj = function(key,num,value)
                    {
                    var keyArr = key.split('_');
                    var ob = 'obj';
                    for(var i=1;i<keyArr.length;i++){
                          ob = ob + '[\"' + keyArr[i] + '\"]';
                          if(!eval(ob))
                          {
                             eval(ob + '={};');
                          }
                          }
                          if(keyArr.length == num+1)
                            {
                               eval(ob + '=value;');
                            }
                     };
                   for(var i=1;i<maxLength;i++)
                    {
                         for(var j in values)
                            {
                              var value = values[j];
                              addObj(j,i,value);
                            }
                     }
                     var field = '';
                     if(maxLength == 2)
                        {
                           for(var i in values)
                              {
                                  var keyArr = i.split('_');field = field + keyArr[1] + ',';
                              }
                         }
                      if(field != '')
                        {
                            field = field.substring(0,field.length-1);
                         }
            
    },
    /**
     * 将列表行、列编辑数据保存到数据库中
     * @param {} obj
     * @return {}
     */
    saveToTable:function(strobj,callback){
       
        var me = this;
        //获取属性面板ID
        var paramsPanel = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
        
        //获取数据对象ID
        var dataObjectgroup=paramsPanel.getComponent("dataObject");
        //获取修改服务URL
        var updateUrl=dataObjectgroup.getComponent("editDataServerUrl");
        
        var myUrl=updateUrl.getValue(me.objectServerValueField);
        
        if(updateUrl!="")
        {
            myUrl=getRootPath() + "/"+myUrl;
        }
        else
        {
            Ext.Msg.alert('提示','<b style="color:red">'+'【没有配置获取数据服务地址！】</b>');
            return null;
        }
        
        var values=strobj;
        var maxLength = 0;
            for(var i in values)
               {
                        var arr = i.split('_');
                        if(arr.length > maxLength)
                         {
                            maxLength = arr.length;
                   }}
                 var obj = {};
                 var addObj = function(key,num,value)
                    {
                    var keyArr = key.split('_');
                    var ob = 'obj';
                    for(var i=1;i<keyArr.length;i++){
                          ob = ob + '[\"' + keyArr[i] + '\"]';
                          if(!eval(ob))
                          {
                             eval(ob + '={};');
                          }
                          }
                          if(keyArr.length == num+1)
                            {
                               eval(ob + '=value;');
                            }
                     };
                   for(var i=1;i<maxLength;i++)
                    {
                         for(var j in values)
                            {
                              var value = values[j];
                              addObj(j,i,value);
                            }
                     }
                     var field = '';
                     if(maxLength == 2)
                        {
                           for(var i in values)
                              {
                                  var keyArr = i.split('_');field = field + keyArr[1] + ',';
                              }
                         }
                      if(field != '')
                        {
                            field = field.substring(0,field.length-1);
                         }
        
          //var strFilds={"entity":obj,"fields":field};
        //var objstr=Ext.JSON.encode({entity:obj,field:field});
        
        Ext.Ajax.defaultPostHeader = 'application/json';

        //Ext.Ajax.defaultPostHeader = 'application/json; charset=UTF-8';
        Ext.Ajax.request({
            async:false,//非异步
            url:myUrl,
            //params:Ext.JSON.encode(strFilds),
            params:Ext.JSON.encode({entity:obj,fields:field}),
            method:'POST',
            timeout:7000,
            success:function(response,opts){
                var result = Ext.JSON.decode(response.responseText);
                if(result.success){
                    if(Ext.typeOf(callback) == "function"){
                        callback();//回调函数
                    }
                    Ext.Msg.alert('提示','保存成功！');                   
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
	//-----------------------------------------------------------------
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
//                me.OpenedParamsPanel='addid';
                items[i].show();
            }
        }
        for(var i in removeArr){
            east.remove(removeArr[i]);
        }
        //所有组件信息
        var southRecords = me.getSouthRecords();
        for(var i in southRecords){
            var record = southRecords[i];
            //添加组件属性面板
//            me.addParamsPanel(record.get('InteractionField'),record.get('DisplayName'));
            me.addParamsPanel('addid','列表属性配置');
        }
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
            
            //组件属性面板
            var panel = me.getComponent('east').getComponent(panelItemId);
            var basic = panel.getComponent("basicParams");
            var name = basic.getComponent('name');
            name.setValue();
//            name.setValue(record.get('DisplayName'));
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
            items:[{
                xtype:'textfield',fieldLabel:'显示名称',name:'name',labelWidth:55,anchor:'95%', hidden:true,
                itemId:'name'
            },{
                xtype:'radiogroup',
	        	itemId:'listType',
	        	fieldLabel:'列表类型',
		        columns:1,
		        vertical:true,
		        items:[
		            {boxLabel:'普通列表',name:'listType',inputValue:'List',checked:true},
		            {boxLabel:'分组列表',name:'listType',inputValue:'grouping'},
		            {boxLabel:'分层统计',name:'listType',inputValue:'layering'}
		        ]
	        }]
        }];
        return items;
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
    }
});