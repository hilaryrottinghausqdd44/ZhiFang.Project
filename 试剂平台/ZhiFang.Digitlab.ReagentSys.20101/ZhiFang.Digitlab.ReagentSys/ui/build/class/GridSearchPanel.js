/**
 * 高级查询列表形式构建工具
 * 
 */
Ext.ns('Ext.build');
Ext.define('Ext.build.GridSearchPanel',{
	extend:'Ext.panel.Panel',
	alias: 'widget.gridsearchpanel',
	//=====================可配参数=======================
	/**
	 * 应用组件ID
	 */
	appId:-1,
    appType:-1,
	/**
	 * 构建名称
	 */
	buildTitle:'高级查询列表形式构建工具',
    /**
     * 是否刚刚开启页面
     * @type Boolean
     */
    isJustOpen:true,
	//标题字体设置
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
	 * 	返回的json对象：{"ErrorInfo":"","success":true,"ResultDataFormatType":"JSON","ResultDataValue":"{count:1,List:[{a:1}]}"}
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
	 * 	返回的json对象：{"ErrorInfo":"","success":true,"ResultDataFormatType":"JSON","ResultDataValue":"{count:1,List:[{a:1}]}"}
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
    defaultPanelWidth:860,
    /**
     * 列表默认高度
     * @type Number
     */
    defaultPanelHeight:320,
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
   /**
     * 列类型
     * @type 
     */
    columnTypeList:[
        ['string','字符串'],
        ['date','日期型'],
        ['number','数字型']
    ],
    /**
     * 左括号 
     * @type 
     */
    LeftType:[
        [null,'不选'],
        ['(','('],
        ['((','(('],
        ['(((','((('],
        ['((((','((((']
    ],
     /**
     * 右括号 
     * @type 
     */
    RightType:[
        [null,'不选'],
        [')',')'],
        ['))','))'],
        [')))',')))'],
        ['))))','))))']
    ],
    
    /**
     *关系运算关系
     * @type 
     */
    OperationType:[
        ['=', '等于'],
        ['!=', '不等于'],
        ['>', '大于'],
        ['<', '小于'],
        ['<=', '小于等于'],
        ['>=', '大于等于'],
        ['is null','空白'],
        //['is not null','非空白'],
        ['like begin%','以...开头'],
        ['like %end','以...结尾'],
        ['like %in%','包含字符'],
        ['not like %in%','不包含字符'],
        ['in', '包含'],
        ['not in', '不包含']
    ],
    /**
     * 逻辑运算符
     * @type 
     */
    LogicalType:[
        [null,'不选'],
        ['and', '与'],
        ['or', '或']
        //['not', '非']
    ],
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
		
		//属性面板
		var east = me.createEast();
		//列属性列表
		var south = me.createSouth();
		//效果展示区
        var center = me.createCenter();
        
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
        center.height = me.defaultPanelHeight;
        center.width = me.defaultPanelWidth+10;
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
				{xtype:'button',hidden:true,text:'浏览',itemId:'browse',iconCls:'build-button-see',
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
     * 另存按钮事件处理
     * @private
     */
    saveAs:function(){
        var me = this;
        me.save(false);
    },
	/**
	 * 效果展示面板
	 * @private
	 * @return {}
	 */
	createCenter:function(){
        var me = this;
        //是否有菜单选项
        //功能栏按钮组
        var buttontoobar = me.createButtonToolbar();
        var arrTwo=me.createActionColumn();
        var columns = me.createColumns();
        var grid = {
            xtype:'grid',
            itemId:'center',
            title:'查询条件配置(展示区域)',
            columnLines:true,//在行上增加分割线
            columns:columns,
            width:me.defaultPanelWidth,
            height:me.defaultPanelHeight,
            store:Ext.create('Ext.data.Store',{
                fields:me.getSouthStoreFields(), 
                proxy:{
                    type:'memory',
                    reader:{type:'json',root:'list'}
                }
            }),
            plugins:Ext.create('Ext.grid.plugin.CellEditing',{clicksToEdit:1})
        };
        //挂靠
        if(buttontoobar ){
            grid.dockedItems = [];
            //功能栏按钮组
            if(buttontoobar){
                grid.dockedItems.push(buttontoobar);
            }
        }
        //列表标题栏点击事件

        //列表面板事件监听
        grid.listeners = {
            columnresize:{//列宽度改变
                fn: function(ct,column,width,e,eOpts){}
            },
            columnmove:{//列位置移动
                fn: function(ct,column,fromIdx,toIdx,eOpts){}
            }
        }; 
        
        var com = {
            xtype:'panel',
            title:'',
            bodyPadding:'2 10 10 2',
            autoScroll:true,
            items:[grid],
            itemId:'center'
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
            {text:'交互字段集合',dataIndex:'InteractionField',disabled:true,editor:{readOnly:true},width:635}],
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
		//其他设置
		//var other = me.createOther();
		//表单宽高
		var panelWH = me.createWidthHieght();
		//功能按钮
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
				xtype:'textfield',fieldLabel:'功能编号',labelWidth:55,anchor:'100%',
	            itemId:'appCode',name:'appCode',
                labelStyle:"font-weight:bold;font-size:11px;font-family:SimHei;color:#FF0000"
	        },{
				xtype:'textfield',fieldLabel:'中文名称',labelWidth:55,anchor:'100%',
	            itemId:'appCName',name:'appCName',
                labelStyle:"font-weight:bold;font-size:11px;font-family:SimHei;color:#FF0000"
	        },{
				xtype:'textareafield',fieldLabel:'功能简介',labelWidth:55,anchor:'100%',grow:true,
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
	            xtype:'textfield',fieldLabel:'显示名称',labelWidth:55,value:'查询条件配置',anchor:'100%',
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
	    	xtype:'fieldset',title:'数据对象',padding:'0 5 0 5',collapsible:true,
	        defaultType:'textfield',defaults:{anchor:'100%'},layout:'anchor',
            itemId:'dataObject',
	        items:[{
				xtype:'combobox',fieldLabel:'数据对象',
	        	itemId:'objectName',name:'objectName',
                labelStyle:"font-weight:bold;font-size:11px;font-family:SimHei;color:#FF0000",
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
	            xtype:'checkbox',boxLabel:'显示序号',checked:true,
	            fieldLabel:'',hideLabel:true,labelWidth:65,
	            itemId:'hasRowNumberer',name:'hasRowNumberer'
	        },{
	            xtype:'checkbox',boxLabel:'开启复选框',checked:true,
	            fieldLabel:'',hideLabel:true,labelWidth:65,
	            itemId:'hasCheckBox',name:'hasCheckBox'
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
    /***
     * 功能栏按钮
     * @type 
     */
    createbuttonsList:function(){
	    var me=this;
	    var buttonsList=[
	       
	        {xtype:'button',text:'确定',itemId:'saveOK',iconCls:'build-button-save',margin:'0 4 0 0',
	            handler:function(){
	                me.okClick();
	                me.fireEvent('okClick');
	            }
	        },{xtype:'button',text:'取消',itemId:'cancel',iconCls:'',margin:'0 4 0 0',
	            handler:function(){
	                me.cancelClick();
	                me.fireEvent('cancelClick');
	            }
	        }
	            
	    ];
	    return buttonsList;
    },
    /***
     * 功能栏全部清除按钮
     * @type 
     */
    createdeleteAllButtons:function(){
        var me=this;
	    var buttonsActionList=[
	        {xtype:'button',text:'全部清除',itemId:'deleteAll',iconCls:'build-button-delete',margin:'0 4 0 0',
	            handler:function(){
	                me.deleteAllClick();
	                me.fireEvent('deleteAllClick');
	            }
	        }
	            
	    ];
	    return buttonsActionList;
    },
     /***
     * 功能栏删除按钮
     * @type 
     */
    createdeleteButtons:function(){
        var me=this;
        var buttonsActionList=[
            {xtype:'button',text:'删除',itemId:'delete',iconCls:'build-button-delete',margin:'0 4 0 0',
                handler:function(){
                    me.deleteClick();
                    me.fireEvent('deleteClick');
                }
            }
                
        ];
        return buttonsActionList;
    },
    /***
     * 功能栏新增按钮
     * @type 
     */
    createaddButtons:function(){
        var me=this;
        var buttonsActionList=[
            {xtype:'button',text:'新增',itemId:'add',iconCls:'build-button-add',
                handler:function(){
                    me.addClick();
                    me.fireEvent('addClick');
                }
            }   
        ];
        return buttonsActionList;
    },
    /**
     * 新增行记录
     * @private
     */
    addClick:function(){
        var me = this;
        var center = me.getCenterCom();
        var store = center.store;
        var records=me.getSouthRecords();
        if(records.length<1){
        Ext.Msg.alert("提示","<b style='color:red'>请先选择好数据对象再操作！</b>");
        return;
        }else{
        var interactionField=records[0].get("InteractionField");
        //var tempArr=Ext.decode(interactionField);
        var tempArr=Ext.decode(interactionField);
        var rec = ('Ext.data.Model',{
	        InteractionField:tempArr[0].value,
	        Logical:'and',
            LeftBrackets:'null',
            Type:'string',
            Content:'',
            RightBrackets:'null',
	        NumericOp:'=' 
	    });
        store.add(rec);
        }
    },
    /**
     * 确定操作事件
     * @private
     */
    okClick:function(){
        var me = this;
        var center = me.getCenterCom();
        var store = center.store;
        var laststrWhere='1=1 ';
        var tempstrWhere='';
        store.each(function(record){
            var interactionField=record.get("InteractionField");//交互字段
            var leftBrackets=record.get("LeftBrackets");
            var logical=record.get("Logical");//逻辑关系
            var type=record.get("Type");//数据项类型
            var numericOp=record.get("NumericOp");//关系运算符
            var content=record.get("Content");//输入内容
            var rightBrackets=record.get("RightBrackets");//右括号
            
            if(interactionField==''||interactionField==null){
            
            }else{
             //逻辑关系
            if(logical=='null'){//逻辑关系不选择时,默认取and
                tempstrWhere=tempstrWhere+(' '+and+' ');
            }else{
                tempstrWhere=tempstrWhere+(' '+logical+' ');
            }
            //左括号
            if(leftBrackets!='null'){
                tempstrWhere=tempstrWhere+' '+leftBrackets;
            }
            //交互字段,将对象和属性名的下划线转换成小圆点
            //var text=interactionField.split("_");
            //interactionField=interactionField.replace(/_/g,".");
            //tempstrWhere=tempstrWhere+(' '+interactionField);
           
            var defaultValueArr=interactionField.split('_');
          
            var tempStr='';
            for(var j=0;j<defaultValueArr.length-1;j++){
                if(j==0){
                    var tempVlue=defaultValueArr[j];
                    tempStr=tempStr+tempVlue.toLowerCase()+'.';
                }
                else if(j<defaultValueArr.length-1){
                    tempStr=tempStr+defaultValueArr[j]+'.';
                }
            }
            myItemId =tempStr+defaultValueArr[defaultValueArr.length-1];

            tempstrWhere=tempstrWhere+myItemId;
            //关系运算符
            switch(numericOp)
				{
				case 'is null':
				  //tempstrWhere=tempstrWhere+(' '+numericOp);
                  content='is null';
                  tempstrWhere=tempstrWhere+(' '+content);
				  break;
                  case 'like begin%'://以...开头
                  tempstrWhere=tempstrWhere+(' '+'like ');
                  tempstrWhere=tempstrWhere+("'%25"+content+"' ");
                  break;
                  case 'like %end'://以...结尾
                  tempstrWhere=tempstrWhere+(' '+'like ');
                  tempstrWhere=tempstrWhere+("'"+content+"%25' ");
                  break;
                  case 'like %in%'://包含字符
                  tempstrWhere=tempstrWhere+(' '+'like ');
                  tempstrWhere=tempstrWhere+("'%25"+content+"%25' ");
                  break;
                  case 'not like %in%'://不包含字符
                  tempstrWhere=tempstrWhere+(' '+'not like ');
                  tempstrWhere=tempstrWhere+("'%25"+content+"%25' ");
                  break;
                  case 'in'://包含字符
                  tempstrWhere=tempstrWhere+(' '+'in  ');
                  tempstrWhere=tempstrWhere+("("+content+")' ");
                  break;
                  case 'not in'://不包含
                  tempstrWhere=tempstrWhere+(' '+'not in ');
                  tempstrWhere=tempstrWhere+("("+content+") ");
                  break;
				default://等于,不等于,小于等于,大于等于,
				  tempstrWhere=tempstrWhere+(' '+numericOp);
                  tempstrWhere=tempstrWhere+("'"+content+"' ");
				}
            //右括号
            if(rightBrackets!='null'){
                tempstrWhere=tempstrWhere+' '+rightBrackets;
            }
            
            }
        });
        
        laststrWhere=laststrWhere+tempstrWhere;
        alert(laststrWhere);
    },
    /**
     * 取消事件
     * @private
     */
    cancelClick:function(){
    
    },
    /**
     * 删除所有的行记录
     * @private
     */
    deleteAllClick:function(){
        var me = this;
        var center = me.getCenterCom();
        var store = center.store;
            Ext.Msg.confirm('警告','确定要删除吗？',function (button){
                if(button == 'yes'){
	                //没有被删除的才去后台删除
	                store.removeAll();
                }
            });
    },
    /**
     * 删除勾选的行记录
     * @private
     */
    deleteClick:function(){
        var me = this;
        var center = me.getCenterCom();
        var records = center.getSelectionModel().getSelection();
        var store = center.store;
        if(records.length > 0){
            Ext.Msg.confirm('警告','确定要删除吗？',function (button){
                if(button == 'yes'){
                    Ext.Array.each(records,function(record){
                        //没有被删除的才去后台删除
                        store.remove(record);
                    });
                }
            });
        }else{
            Ext.Msg.alert("提示","<b style='color:red'>请勾选需要删除的记录！</b>");
        }
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
        var grid = me.createGrid();
        if(grid){
            //删除原先的表单
            owner.remove(center);
            //添加新的表单
            owner.add(grid);
            //更换组件属性面板
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
				AppType:me.appType,//6,//应用类型(高级查询--列表)
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
        //功能栏按钮组
        var buttontoobar = me.createButtonToolbar();
        var grid = {
            xtype:'grid',
            itemId:'center',
            autoScroll:true,
            title:params.titleText,
            width:parseInt(params.Width),
            height:parseInt(params.Height),
            columns:columns,
            store:Ext.create('Ext.data.Store',{
                fields:me.getSouthStoreFields(), 
                proxy:{
                    type:'memory',
                    reader:{type:'json'}
                }
            }),
            plugins:Ext.create('Ext.grid.plugin.CellEditing',{clicksToEdit:1})
        };
        
        //挂靠
        if(buttontoobar ){
            grid.dockedItems = [];
            //功能栏按钮组
            if(buttontoobar){
                grid.dockedItems.push(buttontoobar);
            }
        }
        //列表面板事件监听
        grid.listeners = {
            columnresize:{//列宽度改变
                fn: function(ct,column,width,e,eOpts){
                }
            },
            columnmove:{}
        };
        
        return grid;
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
        var tempArr='';
        Ext.Array.each(data,function(record){
            if(record.get('leaf')){
                    var interactionField=record.get(me.columnParamsField.InteractionField)
                    tempArr= tempArr+("{'value':'"+interactionField+"','text':'"+record.get('text')+"'},");    
            }
        });
        store.removeAll();
        if(tempArr.length>0){
        tempArr=tempArr.substring(0,tempArr.length-1);
        var rec = ('Ext.data.Model',{
            InteractionField:"["+tempArr+"]"
        });
        store.add(rec);
        }
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
        var nodeArr = [];//没展开的节点数组
        if(nodeArr.length == 0){
            me.isJustOpen = false;
            me.browse();//渲染效果
        }else{
            var count = 0;
            var changeNodes = function(num){
                var callback =function(){
                    if(num == nodeArr.length-1){
                        if(me.appId != -1 && me.isJustOpen){
                            me.isJustOpen = false;
                            me.browse();//渲染效果
                        }
                    }else{
                    }
                }
                expandParentNode(nodeArr[num],callback);
            }
            changeNodes(0);
        }
    },
	//=====================组件的创建与删除=======================

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
	 * 创建列
	 * @private
	 * @return {}
	 */
	createColumns:function(){
		var me = this;
         //操作列
        var actioncolumn = me.createActionColumn();
		var columns = [
               //列模式的集合
               actioncolumn,
               {xtype:'rownumberer',text:'序号',width:35,align:'center',hidden:true},
               {text:'逻辑关系符',dataIndex:'Logical',width:80,align:'center',
                    renderer:function(value, p, record){
                        var typelist = me.LogicalType;
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
                            data:me.LogicalType
                        }),
                        listClass: 'x-combo-list-small'
                    })
                },
              
		      {text:'选择字段',dataIndex:'InteractionField',width:160,align:'center',value:'',
		            editor: new Ext.form.field.ComboBox({
		                mode:'local',
                        editable:true, 
		                displayField:'text',valueField:'value',
                        store:new Ext.data.Store({ 
                            fields:['value','text']
                        }),
		                listClass: 'x-combo-list-small',
                        listeners:{
                            focus:function(com,The,eOpts ){
	                            //获取已选择字段的数组集合
								var records=me.getSouthRecords();
								var interactionField=records[0].get("InteractionField");
								var tempArr=Ext.decode(interactionField);
	                            com.store=new Ext.data.Store({ 
		                            fields:['value','text'], 
		                            data:tempArr
	                            });
                            }
                        }
		            })
		        },
                {text:'数据项类型',dataIndex:'Type',width:60,hidden:true,align: 'center',
                    renderer:function(value, p, record){
                        var typelist = me.columnTypeList;
                        for(var i=0;i<typelist.length;i++){
                            if(value == typelist[i][0]){
                                return Ext.String.format(typelist[i][1]);
                            }
                        }
                    },
                    editor:new Ext.form.field.ComboBox({
                        mode:'local',editable:false, 
                        displayField:'text',valueField:'value',
                        store:new Ext.data.SimpleStore({ 
                            fields:['value','text'], 
                            data:me.columnTypeList
                        }),
                        listClass: 'x-combo-list-small'
                    })
                },
                {text:'左括号',dataIndex:'LeftBrackets',width:60,align:'center',value:'',
                    renderer:function(value, p, record){
                        var typelist = me.LeftType;
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
                            data:me.LeftType
                        }),
                        listClass: 'x-combo-list-small'
                    })
                },
               {text:'运算符',dataIndex:'NumericOp',width:100,align:'center',
                    renderer:function(value, p, record){
                        var typelist = me.OperationType;
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
                            data:me.OperationType
                        }),
                        listClass: 'x-combo-list-small'
                    })
                }, 
                {text:'输入内容',dataIndex:'Content',width:260,
                    editor:{
                        allowBlank:false,
                        listeners:{
                            change:function(com,newValue){
                                var grid = me.getCenterCom();
						        var store = grid.store;
                                store.sync();//与后台数据同步

                            }
                        }
                    }
                },
                {text:'右括号',dataIndex:'RightBrackets',width:60,align:'center',
                    renderer:function(value, p, record){
                        var typelist = me.RightType;
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
                            data:me.RightType
                        }),
                        listClass: 'x-combo-list-small'
                    })
                }   
        ];
		return columns;
	},
	/**
	 * 功能栏按钮组
	 * @private
	 * @return {}
	 */
	createButtonToolbar:function(){
		var me = this;
		var myItems = me.createaddButtons().concat(me.createdeleteAllButtons());
        myItems=myItems.concat(me.createbuttonsList());
		var com = null;
			com = {
				xtype:'toolbar',
				items:myItems
			};
		return com;
	},
	/**
	 * 创建操作列
	 * @private
	 * @return {}
	 */
	createActionColumn:function(){
        var me = this;
        var myItems = me.createaddButtons().concat(me.createdeleteButtons());
        var com = null;
            com = {
                xtype:'actioncolumn',text:'操作',width:60,align:'center',
                items:myItems
            };
        return com;
    },
    
	//=====================组件属性面板的创建与删除=======================
	
	//=====================弹出窗口=======================

	/**
	 * 弹出表单
	 * @private
	 * @param {} type
	 * @param {} id
	 */
	openFormWin:function(type,id){
		var me = this;
		var ClassCode = me.getFromClassCode();
		var panelParams = {
			type:type,
			dataId:id,
    		modal:true,//模态
    		floating:true,//漂浮
			closable:true,//有关闭按钮
			draggable:true//可移动
		};
		if(ClassCode != ""){
			var Class = eval(ClassCode);
			var panel = Ext.create(Class,panelParams);
			panel.show();
			panel.on({
				saveClick:function(){
					panel.close();
					me.getCenterCom().load();
				}
			});
		}else{
			Ext.Msg.alert('提示','弹出表单没有配置！');
		}
	},
    
    //=====================设置获取参数=======================
	/**
	 * 获取展示区域组件
	 * @private
	 * @return {}
	 */
	getCenterCom:function(){
		var me = this;
		var center = me.getComponent('center');
        var myGrid=center.getComponent('center');
		return myGrid;
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
	 * 获取列表的获取数据服务URL
	 * @private
	 * @return {}
	 */
	getDataUrl:function(){
		var me = this;
		//表单配置参数
		var params = me.getPanelParams();
		//前台需要显示的字段
		var fields = me.getGridFields();
		
		if(!fields){
			fields = "";
		}
		
		//数据服务地址
		var url = params.getDataServerUrl;
		
		if(url){
			url = url.split("?")[0];
			url = getRootPath() + "/" + url + "?isPlanish=true&fields=" + fields;
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
            {name:me.columnParamsField.InteractionField,type:'string'},//交互字段
            {name:'DisplayName',type:'string'},//显示名称
            {name:'Logical',type:'string'},//逻辑关系
            {name:'LeftBrackets',type:'string'},//左括号
            {name:'Type',type:'string'},//数据项类型
            {name:'NumericOp',type:'string'},//关系运算符
            {name:'Content',type:'string'},//输入内容
            {name:'RightBrackets',type:'string'}//右括号
             
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
			me.setObjData();
			me.setSouthRecordByArray(southParams);
			me.setPanelParams(panelParams);
			//按钮设置交互字段

		};
		//从后台获取应用信息
		me.getAppInfoFromServer(callback);
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
			record.Logical = item.get('Logical');
	        record.LeftBrackets = item.get('LeftBrackets');
	        record.Type = item.get('Type');
	        record.NumericOp = item.get('NumericOp');
	        record.RightBrackets = item.get('RightBrackets');
	        myItems.push(record);
		}
		return myItems;
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
        
        //获取已选择字段的数组集合
        var records=me.getSouthRecords();
        var interactionField=records[0].get("InteractionField");
        var tempArr=interactionField;
		var appClass = 
		"Ext.Loader.setPath('Ext.ux',getRootPath()+'/ui/extjs/ux');" + 
		"Ext.define('" + params.appCode + "',{" + 
			"extend:'Ext.grid.Panel'," + 
			"alias:'widget." + params.appCode + "'," + 
			"title:'" + params.titleText + "'," + 
			"width:" + params.Width + "," + 
			"height:" + params.Height + "," +
            "laststrWhere:''," +
            "getValue:" + me.getValueStr() + "," +
            "interactionFieldList:" + tempArr + "," + //已选择字段的数组集合
            
			"autoScroll:true," + 
            
            "leftType:" + me.LeftTypeStr() + "," +//左括号集合    
            "logicalType:" + me.LogicalTypeStr() + "," +    
            "columnTypeList:" + me.columnTypeListStr() + "," +    
            "operationType:" + me.OperationTypeStr() + "," +    
            "rightType:" + me.rightTypeStr() + "," + 
            
            
            //方法和事件
            "createaddButtons:" + me.createaddButtonsStr() + "," +
            "createdeleteButtons:" + me.createdeleteButtonsStr() + "," +
            "createdeleteAllButtons:" + me.createdeleteAllButtonsStr() + "," +
            
            "deleteClick:" + me.deleteClickStr() + "," +
            "cancelClick:" + me.cancelClickStr() + "," +
            "addClick:" + me.addClickStr() + "," +
            "deleteAllClick:" + me.deleteAllClickStr() + "," +
            "okClick:" + me.okClickStr() + "," +
            
            "createbuttonsList:" + me.createbuttonsListStr() + "," +
            "createActionColumn:" + me.createActionColumnStr() + "," +
            "createButtonToolbar:" + me.createDockedItemsStr() + "," +//挂靠功能栏按钮组
            
			"myColumns:" + me.createColumnsStr() + "," +
            //添加store
            "store:Ext.create('Ext.data.Store',{" +
			       "fields: [" +
			            "{name:'InteractionField',type:'string'}," +//交互字段
			            "{name:'Logical',type:'string'}," +//逻辑关系
			            "{name:'LeftBrackets',type:'string'}," +//左括号
			            "{name:'Type',type:'string'}," +//数据项类型
			            "{name:'NumericOp',type:'string'}," +//关系运算符
			            "{name:'Content',type:'string'}," +//输入内容
			            "{name:'RightBrackets',type:'string'}" +//右括号 
			        "], " +
			        "proxy:{" +
			                    "type:'memory'," +
			                    "reader:{type:'json',root:'list'}" +
			                "}" +
			            "})," ;
				
           appClass=appClass+"afterRender:function(){" + 
                "var me=this;" + 
                "me.callParent(arguments);" + 
                "if(Ext.typeOf(me.callback)=='function'){me.callback(me);}" + 
               "}," ;
            appClass=appClass+ 
			"initComponent:function(){" + 
				"var me=this;" + 
			
				//加载数据的方法
				"me.load=function(){me.store.load();};" + 
				//数据列
			  	"me.columns=me.myColumns();";
               
			  	//挂靠
				var dockedItems = me.createDockedItemsStr();
			  	if(dockedItems != ""){
					appClass = appClass + "me.dockedItems=me.createButtonToolbar();";
				}
			    appClass = appClass +" me.plugins=Ext.create('Ext.grid.plugin.CellEditing',{clicksToEdit:1});";
				//公开监听事件
				appClass = appClass + me.createEvent();
				appClass = appClass + "this.callParent(arguments);" + 
			"}" + 
		"});";
		return appClass;
	},
	/**
	 * 创建数据列
	 * @private
	 * @return {}
	 */
	createColumnsStr:function(){
        var me=this;
        var fun = 
        "function(){" + 
        "var me = this;" + 
         //操作列
        "var actioncolumn = me.createActionColumn();" + 
        "var columns = [" + 
               //列模式的集合
               "actioncolumn," + 
              "{xtype:'rownumberer',text:'序号',width:35,align:'center',hidden:true}," + 
              "{text:'逻辑关系符',dataIndex:'Logical',width:80,align:'center'," + 
                    "renderer:function(value, p, record){" + 
                       " var typelist = me.logicalType;" + 
                        "for(var i=0;i<typelist.length;i++){" + 
                            "if(value == typelist[i][0]){" + 
                                "return Ext.String.format(typelist[i][1]);" + 
                            "}" + 
                        "}" + 
                    "}," + 
                    "editor: new Ext.form.field.ComboBox({" +
                        "mode:'local',editable:false, " +
                        "displayField:'text',valueField:'value'," +
                        "store:new Ext.data.SimpleStore({ " +
                            "fields:['value','text'], " +
                            "data:me.logicalType" +
                        "})," +
                        "listClass: 'x-combo-list-small'" +
                   " })" +
                "}," +
              "{text:'选择字段',dataIndex:'InteractionField',width:180,align:'center',value:''," + 
                    "editor: new Ext.form.field.ComboBox({" + 
                        "mode:'local',editable:true, " + 
                        "displayField:'text',valueField:'value'," + 
                        "store:new Ext.data.Store({ " + 
                           " fields:['value','text']" + 
                           //" data:me.interactionFieldList" + 
                        "})," + 
                        "listClass: 'x-combo-list-small'," + 
                        "listeners:{" + 
                            "focus:function(com,The,eOpts ){" + 
                                //获取已选择字段的数组集合
                                "var tempArr=me.interactionFieldList;" + 
                                "com.store=new Ext.data.Store({ " + 
                                    "fields:['value','text'], " + 
                                    "data:tempArr" + 
                                "});" + 
                            "}" + 
                       " }" + 
                   " })" + 
                "}," + 
                "{text:'数据项类型',dataIndex:'Type',width:60,hidden:true,align: 'center'," +
                   " renderer:function(value, p, record){" +
                       " var typelist = me.columnTypeList;" +
                        "for(var i=0;i<typelist.length;i++){" +
                            "if(value == typelist[i][0]){" +
                               " return Ext.String.format(typelist[i][1]);" +
                           " }" +
                       " }" +
                    "}," +
                    "editor:new Ext.form.field.ComboBox({" +
                        "mode:'local',editable:false, " +
                        "displayField:'text',valueField:'value'," +
                        "store:new Ext.data.SimpleStore({ " +
                           " fields:['value','text'], " +
                           " data:me.columnTypeList" +
                        "})," +
                        "listClass: 'x-combo-list-small'" +
                   " })" +
                "}," +
                "{text:'左括号',dataIndex:'LeftBrackets',width:60,align:'center',value:''," + 
                    "renderer:function(value, p, record){" + 
                        "var typelist = me.leftType;" + 
                        "for(var i=0;i<typelist.length;i++){" + 
                           " if(value == typelist[i][0]){" + 
                               " return Ext.String.format(typelist[i][1]);" + 
                            "}" + 
                        "}" + 
                    "}," + 
                    "editor: new Ext.form.field.ComboBox({" + 
                       " mode:'local',editable:false, " + 
                       " displayField:'text',valueField:'value'," + 
                        "store:new Ext.data.SimpleStore({ " + 
                          "  fields:['value','text'], " + 
                           " data:me.leftType" + 
                        "})," + 
                        "listClass: 'x-combo-list-small'" + 
                  "  })" + 
               " }," + 
                
               "{text:'运算符',dataIndex:'NumericOp',width:100,align:'center'," +
                    "renderer:function(value, p, record){" +
                        "var typelist = me.operationType;" +
                        "for(var i=0;i<typelist.length;i++){" +
                            "if(value == typelist[i][0]){" +
                                "return Ext.String.format(typelist[i][1]);" +
                            "}" +
                        "}" +
                    "}," +
                    "editor: new Ext.form.field.ComboBox({" +
                        "mode:'local',editable:false, " +
                        "displayField:'text',valueField:'value'," +
                        "store:new Ext.data.SimpleStore({ " +
                            "fields:['value','text'], " +
                           " data:me.operationType" +
                        "})," +
                       " listClass: 'x-combo-list-small'" +
                   " })" +
               " }, " +
                "{text:'输入内容',dataIndex:'Content',width:260," +
                    "editor:{" +
                        "allowBlank:false," +
                        "listeners:{" +
                            "change:function(com,newValue){" +
                                "var store = me.getStore();" +
                                "store.sync();" +//与后台数据同步
                           " }" +
                        "}" +
                   " }" +
                "}," +
                "{text:'右括号',dataIndex:'RightBrackets',width:60,align:'center'," +
                    "renderer:function(value, p, record){" +
                        "var typelist = me.rightType;" +
                        "for(var i=0;i<typelist.length;i++){" +
                            "if(value == typelist[i][0]){" +
                                "return Ext.String.format(typelist[i][1]);" +
                            "}" +
                       " }" +
                    "}," +
                    "editor: new Ext.form.field.ComboBox({" +
                        "mode:'local',editable:false," + 
                        "displayField:'text',valueField:'value'," +
                        "store:new Ext.data.SimpleStore({ " +
                            "fields:['value','text'], " +
                            "data:me.rightType" +
                        "})," +
                        "listClass: 'x-combo-list-small'" +
                    "})" +
                "}" +   
        "];" +
        "return columns;" +
    
        "}";
        return fun;
    },

   /**
     * 创建挂靠功能栏按钮组
     * @private
     * @return {}
     */
    createDockedItemsStr:function(){
        var me=this;
        var fun = 
        "function(){" + 
		    "var me = this;" +
		    "var myItems = me.createaddButtons().concat(me.createdeleteAllButtons());" +
            "myItems =myItems.concat(me.createbuttonsList());" +
		    "var com = null;" +
		        "com = {" +
		            "xtype:'toolbar'," +
		            "items:myItems" +
		        "};" +
		    "return com;" +
        "}";
        return fun;
    },
    /**
     * 创建操作列
     * @private
     * @return {}
     */
    createActionColumnStr:function(){
        var me = this;
        var fun = 
        "function(){" + 
        "var me = this;" + 
        "var myItems = me.createaddButtons().concat(me.createdeleteButtons());" +
        "var com = null;" + 
            "com = {" + 
                "xtype:'actioncolumn',text:'操作',width:60,align:'center'," + 
                "items:myItems" + 
            "};" + 
        "return com;" + 
        "}";
        
        return fun;
    },
    
   /***
     * 功能栏按钮
     * @type 
     */
    createbuttonsListStr:function(){
         var me = this;
        var fun = 
        "function(){" + 
        "var me=this;" + 
        "var buttonsList=[" + 
            "{xtype:'button',text:'确定',itemId:'saveOK',iconCls:'build-button-save',margin:'0 4 0 0'," + 
                "handler:function(){" + 
                    "me.okClick();" + 
                    "me.fireEvent('okClick');" + 
                "}" +
            "},{xtype:'button',text:'取消',itemId:'cancel',iconCls:'',margin:'0 4 0 0'," + 
                "handler:function(){" + 
                    "me.cancelClick();" + 
                    "me.fireEvent('cancelClick');" + 
                "}" + 
           " }" + 
                
       " ];" + 
        "return buttonsList;" + 
        "}";
        
        return fun;
    },
    /***
     * 功能栏新增按钮
     * @type 
     */
    createaddButtonsStr:function(){
         var me = this;
        var fun = 
        "function(){" + 
        "var me=this;" + 
        "var buttonsActionList=[" + 
            "{xtype:'button',text:'新增',itemId:'add',iconCls:'build-button-add'," + 
                "handler:function(){" + 
                    "me.addClick();" + 
                    "me.fireEvent('addClick');" + 
                "}" + 
            "}"+ 
       " ];" + 
        "return buttonsActionList;" + 
        "}";
        
        return fun;
    },
    /***
     * 功能栏清除所有按钮
     * @type 
     */
    createdeleteAllButtonsStr:function(){
         var me = this;
        var fun = 
        "function(){" + 
        "var me=this;" + 
        "var buttonsActionList=[" + 
            "{xtype:'button',text:'清除所有',itemId:'deleteAll',iconCls:'build-button-delete',margin:'0 4 0 0'," + 
                "handler:function(){" + 
                   " me.deleteAllClick();" + 
                    "me.fireEvent('deleteAllClick');" + 
                "}" + 
            "}" +   
       " ];" + 
        "return buttonsActionList;" + 
        "}";
        
        return fun;
    },
   /***
     * 功能栏清除按钮
     * @type 
     */
    createdeleteButtonsStr:function(){
         var me = this;
        var fun = 
        "function(){" + 
        "var me=this;" + 
        "var buttonsActionList=[" + 
            "{xtype:'button',text:'删除',itemId:'delete',iconCls:'build-button-delete',margin:'0 4 0 0'," + 
                "handler:function(){" + 
                   " me.deleteClick();" + 
                    "me.fireEvent('deleteClick');" + 
                "}" + 
            "}" +   
       " ];" + 
        "return buttonsActionList;" + 
        "}";
        
        return fun;
    }, 
    /**
     * 清除所有行记录
     * @private
     */
    deleteAllClickStr:function(){
        var me = this;
        var fun = 
        "function(){" + 
        "var me = this;" + 
       " var store = me.store;" + 
            "Ext.Msg.confirm('警告','确定要删除吗？',function (button){" + 
                "if(button == 'yes'){" + 
                   "store.removeAll();" + 
                "}" + 
           " });" + 
       "}";
        
        return fun;
    },
    /**
     * 新增行记录
     * @private
     */
    addClickStr:function(){
       var me = this;
       var fun = 
        "function(){" + 
        "var me = this;" + 
        "var store = me.store;" + 
        "var rec = ('Ext.data.Model',{" + 
            "InteractionField:me.interactionFieldList[0].value," + 
            "Logical:'and'," + 
            "LeftBrackets:'null'," + 
            "Type:'string'," + 
            "Content:''," + 
            "RightBrackets:'null'," + 
            "NumericOp:'='" + 
            
        "});" + 
        "store.add(rec);" + 
        
        "}";
        
        return fun;
    },
    /**
     * 确定操作事件
     * @private
     */
    okClickStr:function(){
        var me = this;
        var fun = 
        "function(){" + 

        "var me = this;" +

        
        "}";
        
        return fun;
    },
    
    /**
     * 获取where串
     * @private
     */
    getValueStr:function(){
        var me = this;
        var fun = 
        "function(){" + 
        "var me = this;" + 
         
        "var store = me.store;" +
        "me.laststrWhere='';" +
        "me.laststrWhere=me.laststrWhere+" +'\\"'+"1=1"+'\\";'+
        "var tempstrWhere='';" +
        "store.each(function(record){" +
            "var interactionField=record.get('InteractionField');" +//交互字段
            "var leftBrackets=record.get('LeftBrackets');" +
            "var logical=record.get('Logical');" +//逻辑关系
            "var type=record.get('Type');" +//数据项类型
            "var numericOp=record.get('NumericOp');" +//关系运算符
            "var content=record.get('Content');" +//输入内容
            "var rightBrackets=record.get('RightBrackets');" +//右括号
            "if(interactionField==''||interactionField==null){" +
            "}else{" +
             //逻辑关系
            "if(logical=='null'){" +//逻辑关系不选择时,默认取and
                "tempstrWhere=tempstrWhere+(' '+and+' ');" +
            "}else{" +
                "tempstrWhere=tempstrWhere+(' '+logical+' ');" +
            "}" +
            //左括号
            "if(leftBrackets!='null'){" +
                "tempstrWhere=tempstrWhere+' '+leftBrackets;" +
           " }" +
            //交互字段
           
            "var defaultValueArr=interactionField.split('_');"+
           
            "var tempStr='';"+
            "for(var j=0;j<defaultValueArr.length-1;j++){"+
                "if(j==0){"+
                    "var tempVlue=defaultValueArr[j];"+
                    "tempStr=tempStr+tempVlue.toLowerCase()+'.';"+
                "}"+
                "else if(j<defaultValueArr.length-1){"+
                    "tempStr=tempStr+defaultValueArr[j]+'.';"+
                "}"+
            "}"+
            "myItemId =tempStr+defaultValueArr[defaultValueArr.length-1];"+

            "tempstrWhere=tempstrWhere+myItemId;" +
            //"tempstrWhere=tempstrWhere+(' '+"+"'"+"text[text.length-1]"+"');" +

            //关系运算符
            "switch(numericOp)" +
                "{" +
                "case 'is null':" +
                  "tempstrWhere=tempstrWhere+(' '+numericOp);" +
                  "break;" +
                  "case 'like begin%':" +//以...开头
                  "tempstrWhere=tempstrWhere+(' '+'like ');" +
             
                  "tempstrWhere=tempstrWhere+("+'\\"'+"'%25"+'\\"'+"+content+"+'\\"'+"' "+'\\"'+");" +
                        
                  "break;" +
                  "case 'like %end':" +//以...结尾
                  "tempstrWhere=tempstrWhere+(' '+'like ');" +
                   "tempstrWhere=tempstrWhere+("+'\\"'+"'"+'\\"'+"+content+"+'\\"'+"%25' "+'\\"'+");" +
                 
                  "break;" +
                  "case 'like %in%':" +//包含字符
                  "tempstrWhere=tempstrWhere+(' '+'like ');" +
                  "tempstrWhere=tempstrWhere+("+'\\"'+"'%25"+'\\"'+"+content+"+'\\"'+"%25' "+'\\"'+");" +
                  
                  "break;" +
                  "case 'not like %in%':" +//不包含字符
                  "tempstrWhere=tempstrWhere+(' '+'not like ');" +
                  "tempstrWhere=tempstrWhere+("+'\\"'+"'%25"+'\\"'+"+content+"+'\\"'+"%25' "+'\\"'+");" +
                  "break;" +
                  
                  "case 'in':" +//包含字符
                  "tempstrWhere=tempstrWhere+(' '+'in  ');" +
                  "tempstrWhere=tempstrWhere+('('+content+')' );" +
                  "break;" +
                  "case 'not in':" +//不包含
                  "tempstrWhere=tempstrWhere+(' '+'not in ');" +
                  "tempstrWhere=tempstrWhere+('('+content+') ');" +
                  "break;" +
                "default:" +//等于,不等于,小于等于,大于等于,
                  "tempstrWhere=tempstrWhere+(' '+numericOp);" +
                  "tempstrWhere=tempstrWhere+("+'\\"'+"'"+'\\"'+"+content+"+'\\"'+"' "+'\\"'+");" +
                "}" +
            //右括号
            "if(rightBrackets!='null'){" +
                "tempstrWhere=tempstrWhere+' '+rightBrackets;" +
            "}" +
            "}" +
        "});" +
        "me.laststrWhere=me.laststrWhere+tempstrWhere;" +
        //测试用
        //"alert(me.laststrWhere);" +
         
         "laststrWhere=me.laststrWhere;" +
         "return laststrWhere;" +
        
        "}";
        
        return fun;
    },
    /**
     * 取消事件
     * @private
     */
    cancelClickStr:function(){
        var me = this;
        var fun = 
        "function(){" + 
        "var me = this;" +
        
        "}";
        
        return fun;
    },
    /**
     * 删除勾选的行记录
     * @private
     */
    deleteClickStr:function(){
        var me = this;
        var fun = 
        "function(){" + 
        "var me = this;" + 
        "var records = me.getSelectionModel().getSelection();" + 
       " var store = me.store;" + 
        "if(records.length > 0){" + 
            "Ext.Msg.confirm('警告','确定要删除吗？',function (button){" + 
                "if(button == 'yes'){" + 
                   " Ext.Array.each(records,function(record){" + 
                        "store.remove(record);" + 
                    "});" + 
                "}" + 
           " });" + 
       " }else{" + 
            "Ext.Msg.alert('提示','<b style=color:red>请勾选需要删除的记录！</b>');" + 
        "}" + 
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
        var com = "me.fireEvent('deleteAllClick');";
            com += "me.addEvents('addClick');";
            com += "me.addEvents('okClick');";
            com += "me.addEvents('cancelClick');";
            com += "me.addEvents('deleteClick');";
        return com;
    },
    /**
     * 逻辑运算符
     * @private
     */
    LogicalTypeStr:function(){
       var me = this;
       var fun = 
        "[[null,'不选择'],['and', '与'],['or', '或']]" ;//,['not', '非']
        return fun;
    },
    /**
     * 关系运算关系
     * @private
     */
    OperationTypeStr:function(){
       var me = this;
       var fun = 
        "[" + 
        "['=', '等于'],['!=', '不等于'],['>', '大于'],['<', '小于']," + 
        "['is null','空白'],['like begin%','以...开头'],"+
        "['like %end','以...结尾'],['like %in%','包含字符'],['not like %in%','不包含字符'],"+
        "['<=', '小于等于'],['>=', '大于等于'],['in', '包含'],['not in', '不包含']]";
        return fun;
    },
    /**
     * 左括号 
     * @private
     */
    LeftTypeStr:function(){
       var me = this;
       var fun = 
        "[[null,'不选择'],['(','('],['((','(('],['(((','((('],['((((','((((']]";
        return fun;
    },
   /**
     * 右括号 
     * @private
     */
    rightTypeStr:function(){
       var me = this;
       var fun = 
        "[[null,'不选择'],[')',')'],['))','))'],[')))',')))'],['))))','))))']]";;
        return fun;
    },
    /**
     * 列类型
     * @private
     */
    columnTypeListStr:function(){
       var me = this;
       var fun = 
        "[['string','字符串'],['date','日期型'],['number','数字型']]";
        return fun;
    },
	//=====================后台获取&存储=======================
	/**
	 * 从后台获取应用信息
	 * @private
	 * @param {} callback
	 */
	getAppInfoFromServer:function(callback){
		var me = this;
		
		if(me.appId != -1){
			var url = me.getAppInfoServerUrl + "?isPlanish=true&id=" + me.appId;
			Ext.Ajax.defaultPostHeader = 'application/json';
			Ext.Ajax.request({
				async:false,//非异步
				url:url,
				method:'GET',
				timeout:2000,
				success:function(response,opts){
					var result = Ext.JSON.decode(response.responseText);
					if(result.success){
						var appInfo = Ext.JSON.decode(result.ResultDataValue);
						
						if(Ext.typeOf(callback) == "function"){
							callback(appInfo);//回调函数
						}
					}else{
						Ext.Msg.alert('提示','删除信息失败！错误信息【<b style="color:red">'+ result.ErrorInfo +"</b>】");
					}
				},
				failure : function(response,options){ 
					Ext.Msg.alert('提示','删除信息请求失败！');
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
					Ext.Msg.alert('提示','保存成功！');
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
    	var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
    	data.ResultDataValue = ResultDataValue;
    	response.responseText = Ext.JSON.encode(data);
    	return response;
  	}
	
});