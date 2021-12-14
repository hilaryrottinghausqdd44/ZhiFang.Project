/**
 * 应用构建类
 * 
 */
Ext.ns('Ext.build');
Ext.define('Ext.build.BuildAppPanel',{
	extend:'Ext.panel.Panel',
	alias: 'widget.buildapppanel',
	//=====================可配参数=======================
	/**
	 * 应用ID
	 * @type 
	 */
	appId:-1,
	/**
	 * 构建工具名称
	 * @type String
	 */
	buildTitle:'应用构建工具',
	/**
	 * 后台数据的属性名
	 * @type String
	 */
	ResultDataValue:'ResultDataValue',
	/**
	 * 获取应用列表的服务地址
	 * @type String
	 */
	getAppListServerUrl:getRootPath()+'/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsByHQL',
   	/**
	 * 新增保存的后台服务地址
	 * @type 
	 */
	addServerUrl:getRootPath()+'/ConstructionService.svc/CS_UDTO_AddBTDAppComponents',
	/**
	 * 修改保存的后台服务地址
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
	 * 临时存放列表元应用ID
	 * @type 
	 */
	testAppId:-1,
	/**
	 * 时间戳，用于修改保存时使用
	 * @type String
	 */
	DataTimeStamp:'',
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
	 * 布局方式 
	 * @type 
	 */
	layoutTypeList:[
		['1','绝对布局'],
		['2','border布局'],
		['3','tab布局'],
		['4','列布局(暂停)']
	],
	/**
	 * 表单样式
	 * @type 
	 */
    FormStyleList:[
    	['','默认'],
    	['red','喜庆红'],
    	['blue','金典蓝'],
    	['pink','温馨粉']
    ],
    /**
     * 联动方法数组
     * @type 
     */
    linkageFunctionArr:[],
    /**
     * 删除联动方法数组
     * @type
     */
    removeLinkageFunctionArr:[],
    /**
     * 解析后的联动代码
     * @type String
     */
    linkageValue:'',
    /**
     * 关系配置代码
     * @type String
     */
    linkageWinvalue:'',
     /**
     * 应用默认宽度
     * @type Number
     */
    defaultPanelWidth:600,
    /**
     * 应用默认高度
     * @type Number
     */
    defaultPanelHeight:300,
    /**
     * border布局的参数对象
     * @type 
     */
    borderObj:{
    	top_show:true,
    	top_split:false,
    	top_collapsed:false,
    	top_border:true,
    	top_priority:4,
    	top_height:80,
    	
    	bottom_show:true,
    	bottom_split:false,
    	bottom_collapsed:false,
    	bottom_border:true,
    	bottom_priority:3,
    	bottom_height:80,
    	
    	left_show:true,
    	left_split:false,
    	left_collapsed:false,
    	left_border:true,
    	left_priority:2,
    	left_width:150,
    	
    	right_show:true,
    	right_split:false,
    	right_collapsed:false,
    	right_border:true,
    	right_priority:1,
    	right_width:150,
    	
    	center_border:true
    },
	/**
	 * 临时记录border布局内容
	 * @type 
	 */
	borderObjTest:{},
    //=====================内部视图渲染=======================
	/**
	 * 初始化表单构建组件
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
		me.bodyPadding = 4;
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
		//效果展示区
		var center = me.createCenter();
		//功能栏
		var north = me.createNorth();
		//属性面板
		var east = me.createEast();
		//应用组件属性列表
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
		south.collapsed = true;
		
		me.items = [north,center,east,south];
	},
	/**
	 * 效果展示面板
	 * @private
	 * @return {}
	 */
	createCenter:function(){
		var me = this;
		var center = {
			xtype:'panel',
			title:'应用',
			autoScroll:true,
			layout:'absolute',
			itemId:'center',
			width:me.defaultPanelWidth,
			height:me.defaultPanelHeight,
			resizable:{handles:'s e'}
		};
		center.listeners = {
            //组件大小变 化监听
            resize:function(com,width,height,oldWidth,oldHeight,eOpts){
                var formParamsPanel = me.getCenterParamPanel();
                //表单宽度和高度赋值
                formParamsPanel.getForm().setValues({width:width,height:height});
            }
        };
        center.header = {
            listeners:{
                click:function(){
                    //切换组件属性配置面板
                    me.switchParamsPanel('center');
                }
            }
        };
		
		var com = {
			xtype:'panel',
			title:'',
			bodyPadding:'2 10 10 2',
			autoScroll:true,
			items:[center]
		};
		return com;
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
				{xtype:'button',text:'应用属性配置',itemId:'appParams',iconCls:'main-build-img-16',margin:'0 4 0 0',
					handler:function(){
						me.switchParamsPanel('center');
					}
				},
				{xtype:'button',text:'浏览',itemId:'browse',iconCls:'build-button-see'},
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
	 * 属性面板
	 * @private
	 * @return {}
	 */
	createEast:function(){
		var me = this;
		//布局方式、应用选中、关系配置
		var layout = me.createLayoutType();
		//保存信息
		var appInfo = me.createAppInfoSet();
		//整体样式
		var formStyle = me.createFormStyleSet();
		//表单宽高
        var panelWH = me.createWidthHieght();
		//标题设置
		var title = me.createTitleSet();
		
		var formParamsPanel = {
			xtype:'form',
			itemId:'center' + me.ParamsPanelItemIdSuffix,
			title:'应用属性配置',
			header:false,
			autoScroll:true,
			border:false,
	        bodyPadding:5,
	        items:[layout,appInfo,formStyle,panelWH,title]
		};
		
		var com = {
			xtype:'form',
			title:'应用属性配置',
			autoScroll:true,
	        items:[formParamsPanel]
		};
		return com;
	},
	/**
	 * 应用组件属性列表
	 * @private
	 * @return {}
	 */
	createSouth:function(){
		var me = this;
		var com = {
			xtype:'grid',
			title:'应用组件属性列表',
			columnLines:true,//在行上增加分割线
			columns:[//列模式的集合
				{xtype:'rownumberer',text:'序号',width:35,align:'center',hidden:true},
				{text:'内部编号',dataIndex:'itemId',width:80},
				{text:'标题文字',dataIndex:'title',width:80},
				{text:'region',dataIndex:'region',hidden:false},//border布局时的区域
				{text:'可收缩',dataIndex:'split',hidden:false},//可收缩
				{text:'可收缩',dataIndex:'collapsible',hidden:false},//可收缩
				{text:'默认收缩',dataIndex:'collapsed',hidden:false},//默认收缩
				{text:'有边框',dataIndex:'border',hidden:false},//有边框
				
				{text:'开启标题',dataIndex:'hasTitle',width:80,align:'center',
                    xtype:'checkcolumn',
                    editor:{
                        xtype:'checkbox',
                        cls:'x-grid-checkheader-editor',
                        listeners:{
                            checkchange:function(com,rowIndex,checked, eOpts ){
//	                            var record = com.ownerCt.editingPlugin.context.record;
//	                            var InteractionField = record.get('InteractionField');
//                                record.set('IsReadOnly',checked);
//                                record.commit();
//	                            me.setComponentReadOnly(InteractionField,checked);
                            }
                        }
                    }
                },
				{text:'x',dataIndex:'x',width:60,align:'center',
					xtype:'numbercolumn',
					format:'0',
					editor:{
		                xtype:'numberfield',
		                allowBlank:false
		            }
				},
				{text:'y',dataIndex:'y',width:60,align:'center',
					xtype:'numbercolumn',
					format:'0',
					editor:{
		                xtype:'numberfield',
		                allowBlank:false
		            }
				},
				{text:'宽度',dataIndex:'width',width:60,align:'center',
					xtype:'numbercolumn',
					format:'0',
					editor:{
		                xtype:'numberfield',
		                allowBlank:false
		            }
				},
				{text:'高度',dataIndex:'height',width:60,align:'center',
					xtype:'numbercolumn',
					format:'0',
					editor:{
		                xtype:'numberfield',
		                allowBlank:false
		            }
				},
				
				{text:'应用组件ID',dataIndex:me.fieldsObj.AppComID,hidden:true},
				{text:'中文名称',dataIndex:me.fieldsObj.CName,hidden:true},
				{text:'英文名称',dataIndex:me.fieldsObj.EName,hidden:true},
				{text:'功能编码',dataIndex:me.fieldsObj.ModuleOperCode,hidden:true},
				{text:'功能简介',dataIndex:me.fieldsObj.ModuleOperInfo,hidden:true},
				{text:'初始化参数',dataIndex:me.fieldsObj.InitParameter,hidden:true},
				{text:'应用类型',dataIndex:me.fieldsObj.AppType,hidden:true},
				{text:'构建类型',dataIndex:me.fieldsObj.BuildType,hidden:true},
				{text:'模块类型',dataIndex:me.fieldsObj.BTDModuleType,hidden:true},
				{text:'执行代码',dataIndex:me.fieldsObj.ExecuteCode,hidden:true},
				{text:'设计代码',dataIndex:me.fieldsObj.DesignCode,hidden:true},
				{text:'类代码',dataIndex:me.fieldsObj.ClassCode,hidden:true},
				{text:'创建者',dataIndex:me.fieldsObj.Creator,hidden:true},
				{text:'修改者',dataIndex:me.fieldsObj.Modifier,hidden:true},
				{text:'汉字拼音字头',dataIndex:me.fieldsObj.PinYinZiTou,hidden:true},
				{text:'数据加入时间',dataIndex:me.fieldsObj.DataAddTime,hidden:true},
				{text:'数据更新时间',dataIndex:me.fieldsObj.DataUpdateTime,hidden:true},
				{text:'实验室ID',dataIndex:me.fieldsObj.LabID,hidden:true},
				{text:'时间戳',dataIndex:me.fieldsObj.DataTimeStamp,hidden:true}
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
	 * 布局方式
	 * @private
	 * @return {}
	 */
	createLayoutType:function(){
		var me = this;
		
		var items = [];
		//布局方式下拉框
		items.push({
			xtype:'combobox',fieldLabel:'布局方式',value:'1',
			labelWidth:55,mode:'local',editable:false,
			displayField:'text',valueField:'value',
			itemId:'layoutType',name:'layoutType',
			store:new Ext.data.SimpleStore({ 
			    fields:['value','text'], 
			    data:me.layoutTypeList
			}),
			toChange:true,
			listeners:{
	        	change:function(owner,newValue,oldValue,eOpts){
	        		var records = me.getSouthRecords();
	        		if(records.length == 0){
	        			me.layoutTypeChange(owner,newValue);
	        		}else{
	        			if(owner.toChange){
		        			Ext.Msg.confirm("警告","布局方式更改后，前面的配置将清空，确定更改布局方式吗？",function (button){
				    			if(button == "yes"){
				    				me.layoutTypeChange(owner,newValue);
				    			}else{
				    				owner.toChange = false;
				    				owner.setValue(oldValue);
				    			}
				    		});
		        		}else{
		        			owner.toChange = true;
		        		}
	        		}
				}
	        }
		});
		//选择应用(绝对布局、tab布局)
		items.push({
        	xtype:'fieldcontainer',layout:'hbox',
        	itemId:'selectApp',
        	items:[{
	        	xtype:'label',text:'选择应用:',width:55,margin:'2 0 2 0'
        	},{
        		xtype:'image',itemId:'selectApp',
	            imgCls:'main-build-img-16 hand',
            	width:16,height:16,
            	margin:'2 0 2 5',cls:'hand',
            	listeners:{
            		click:{
						element:'el',
						fn:function(){
							me.openAppListWin();
						}
					}
            	}
        	}]
        });
        //border布局高级设置
        items.push({
        	xtype:'fieldcontainer',layout:'hbox',hidden:true,
        	itemId:'borderLayoutCon',
        	items:[{
	        	xtype:'label',text:'高级设置:',width:55,margin:'2 0 2 0'
        	},{
        		xtype:'image',itemId:'borderLayoutCon',
	            imgCls:'main-build-img-16 hand',
            	width:16,height:16,
            	margin:'2 0 2 5',cls:'hand',
            	listeners:{
            		click:{
						element:'el',
						fn:function(){
							me.openBorderLayoutConWin();
						}
					}
            	}
        	}]
        });
        //关系配置
        items.push({
        	xtype:'fieldcontainer',layout:'hbox',
        	itemId:'linkageCon',
        	items:[{
	        	xtype:'label',text:'关系配置:',width:55,margin:'2 0 2 0'
        	},{
        		xtype:'textarea',hidden:true,
        		itemId:'linkageValue',name:'linkageValue'
        	},{
        		xtype:'textarea',hidden:true,
        		itemId:'linkageValue2',name:'linkageValue2'
        	},{
        		xtype:'image',itemId:'linkageCon',
	            imgCls:'main-build-img-16 hand',
            	width:16,height:16,
            	margin:'2 0 2 5',cls:'hand',
            	listeners:{
            		click:{
						element:'el',
						fn:function(){
							me.openLinkageConWin();
						}
					}
            	}
        	}]
	    });
		//border布局配置对象
	    items.push({
	    	xtype:'textfield',hidden:true,
	    	itemId:'borderObjStr',name:'borderObjStr',
	    	value:me.getBorderObjStr()
	    });
	    
		var com = {
			xtype:'fieldset',title:'布局及应用选择',padding:'0 5 0 5',collapsible:true,
	        defaultType:'textfield',defaults:{anchor:'100%'},layout:'anchor',
	        itemId:'layoutType',
	        items:items
		};
		return com;
	},
	/**
	 * 功能信息
	 * @private
	 * @return {}
	 */
	createAppInfoSet:function(){
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
	 * 列表整体样式
	 * @private
	 * @return {}
	 */
	createFormStyleSet:function(){
		var me = this;
		var com = {
			xtype:'fieldset',title:'表单样式',padding:'0 5 0 5',collapsible:true,
	        defaultType:'textfield',defaults:{anchor:'100%'},layout:'anchor',
	        itemId:'formStyle',
	        items:[{
				xtype:'combobox',fieldLabel:'整体样式',
	            labelWidth:55,value:'',mode:'local',editable:false,
				displayField:'text',valueField:'value',
				itemId:'formStyle',name:'formStyle',
				store:new Ext.data.SimpleStore({ 
				    fields:['value','text'], 
				    data:me.FormStyleList
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
	createTitleSet:function(){
		var com = {
        	xtype:'fieldset',title:'标题',padding:'0 5 0 5',collapsible:true,
	        defaultType:'textfield',
	        itemId:'title',
	        items:[{
	            xtype:'textfield',fieldLabel:'显示名称',labelWidth:55,value:'应用',anchor:'100%',
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
	            	margin:'2 0 2 5',cls:'hand'
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
            xtype:'fieldset',title:'表单宽高',padding:'0 5 0 5',collapsible:true,
            defaultType:'textfield',defaults:{anchor:'100%'},layout:'anchor',
            itemId:'WH',
            items:[{
                xtype:'numberfield',fieldLabel:'表单宽度',labelWidth:55,anchor:'100%',
                itemId:'width',name:'width',value:me.defaultPanelWidth,
                listeners:{
                    blur:function(com,The,eOpts){
                        var center = me.getCenter();
                        center.setWidth(com.value);
                    }
                }
            },{
                xtype:'numberfield',fieldLabel:'表单高度',labelWidth:55,anchor:'100%',
                itemId:'height',name:'height',value:me.defaultPanelHeight,
                listeners:{
                    blur:function(com,The,eOpts){
                        var center = me.getCenter();
                        center.setHeight(com.value);
                    }
                }
            }]
        };
        return com;
    },
	//=====================事件监听方法=======================
	/**
	 * 布局方式改变时处理
	 * @private
	 * @param {} owner
	 * @param {} newValue
	 */
	layoutTypeChange:function(owner,newValue){
		var me = this;
		//生成新的布局面板
		var newCenter = me.createLayoutPanel(newValue);
		//添加新的布局
		me.changeLayoutPanel(newCenter);
	},
	/**
	 * 打开border布局高级设置
	 * @private
	 * @param {} record
	 */
	openBorderLayoutConWin:function(record){
		var me = this;
		
		var items = [];
		//上
		items.push(
			{xtype:'label',text:'上:',width:20,x:10,y:5},
			{xtype:'checkbox',boxLabel:'展现',name:'top_show',width:45,x:40,y:5,checked:me.borderObj.top_show},
			{xtype:'checkbox',boxLabel:'可收缩',name:'top_split',width:60,x:90,y:5,checked:me.borderObj.top_split},
			{xtype:'checkbox',boxLabel:'默认收缩',name:'top_collapsed',width:70,x:150,y:5,checked:me.borderObj.top_collapsed},
			{xtype:'checkbox',boxLabel:'有边框',name:'top_border',width:60,x:225,y:5,checked:me.borderObj.top_border},
			{xtype:'numberfield',fieldLabel:'优先级',name:'top_priority',width:100,labelWidth:40,x:40,y:30,value:me.borderObj.top_priority},
			{xtype:'numberfield',fieldLabel:'高度',name:'top_height',width:100,labelWidth:30,x:150,y:30,value:me.borderObj.top_height}
		);
		//下
		items.push(
			{xtype:'label',text:'下:',width:20,x:10,y:55},
			{xtype:'checkbox',boxLabel:'展现',name:'bottom_show',width:45,x:40,y:55,checked:me.borderObj.bottom_show},
			{xtype:'checkbox',boxLabel:'可收缩',name:'bottom_split',width:60,x:90,y:55,checked:me.borderObj.bottom_split},
			{xtype:'checkbox',boxLabel:'默认收缩',name:'bottom_collapsed',width:70,x:150,y:55,checked:me.borderObj.bottom_collapsed},
			{xtype:'checkbox',boxLabel:'有边框',name:'bottom_border',width:60,x:225,y:55,checked:me.borderObj.bottom_border},
			{xtype:'numberfield',fieldLabel:'优先级',name:'bottom_priority',width:100,labelWidth:40,x:40,y:80,value:me.borderObj.bottom_priority},
			{xtype:'numberfield',fieldLabel:'高度',name:'bottom_height',width:100,labelWidth:30,x:150,y:80,value:me.borderObj.bottom_height}
		);
		//左
		items.push(
			{xtype:'label',text:'左:',width:20,x:10,y:105},
			{xtype:'checkbox',boxLabel:'展现',name:'left_show',width:45,x:40,y:105,checked:me.borderObj.left_show},
			{xtype:'checkbox',boxLabel:'可收缩',name:'left_split',width:60,x:90,y:105,checked:me.borderObj.left_split},
			{xtype:'checkbox',boxLabel:'默认收缩',name:'left_collapsed',width:70,x:150,y:105,checked:me.borderObj.left_collapsed},
			{xtype:'checkbox',boxLabel:'有边框',name:'left_border',width:60,x:225,y:105,checked:me.borderObj.left_border},
			{xtype:'numberfield',fieldLabel:'优先级',name:'left_priority',width:100,labelWidth:40,x:40,y:130,value:me.borderObj.left_priority},
			{xtype:'numberfield',fieldLabel:'宽度',name:'left_width',width:100,labelWidth:30,x:150,y:130,value:me.borderObj.left_width}
		);
		//右
		items.push(
			{xtype:'label',text:'右:',width:20,x:10,y:155},
			{xtype:'checkbox',boxLabel:'展现',name:'right_show',width:45,x:40,y:155,checked:me.borderObj.right_show},
			{xtype:'checkbox',boxLabel:'可收缩',name:'right_split',width:60,x:90,y:155,checked:me.borderObj.right_split},
			{xtype:'checkbox',boxLabel:'默认收缩',name:'right_collapsed',width:70,x:150,y:155,checked:me.borderObj.right_collapsed},
			{xtype:'checkbox',boxLabel:'有边框',name:'right_border',width:60,x:225,y:155,checked:me.borderObj.right_border},
			{xtype:'numberfield',fieldLabel:'优先级',name:'right_priority',width:100,labelWidth:40,x:40,y:180,value:me.borderObj.right_priority},
			{xtype:'numberfield',fieldLabel:'宽度',name:'right_width',width:100,labelWidth:30,x:150,y:180,value:me.borderObj.right_width}
		);
		//中
		items.push(
			{xtype:'label',text:'中:',width:20,x:10,y:205},
			{xtype:'checkbox',boxLabel:'有边框',name:'center_border',width:60,x:40,y:205,checked:me.borderObj.center_border}
		);
		//效果更改按钮
		items.push({
			xtype:'button',text:'确定更改',x:340,y:190,
			handler:function(button,e){
				var values = button.ownerCt.getForm().getFieldValues();
				me.borderObj = values;
				//记录boder布局配置对象Str
				me.setAppParamsValues({borderObjStr:me.getBorderObjStr()});
				//修改border布局
				me.editBorderLayoutPanel();
				win.close();
			}
		});
		//功能说明
		items.push({
			xtype:'fieldset',title:'配置说明',padding:'0 5 5 5',collapsible:true,
	        defaultType:'textfield',layout:'anchor',
	        html:
	        	'<b>展现</b>：需要这个区域</br>' +
	        	'<b>可收缩</b>：具备收缩功能</br>' +
	        	'<b>默认收缩</b>：默认收缩状态，点击可以展开；</br>' +
	        	'<b>有边框</b>：本区域具有边框</br>' +
	        	'<b>优先级</b>：值越大，优先级越高',
	        margin:'0 5 5 0',x:290,y:5
		});
		//border布局高级设置窗口
		var win = Ext.create('Ext.form.Panel',{
			modal:true,//模态
    		floating:true,//漂浮
			closable:true,//有关闭按钮
			draggable:true,//可移动
			width:450,
			height:260,
			title:'border布局高级设置窗口',
			layout:'absolute',
			bodyPadding:5,
			items:items
		});
		win.show(); 
	},
	/**
	 * 打开应用列表窗口
	 * @private
	 * @param {} callback
	 * @param {} oldRecord
	 */
	openAppListWin:function(callback,oldRecord){
		var me = this;
		
		var appList = Ext.create('Ext.build.AppListPanel',{
    		modal:true,//模态
    		floating:true,//漂浮
			closable:true,//有关闭按钮
			draggable:true,//可移动
			width:500,
			height:300,
			getAppListServerUrl:me.getAppListServerUrl,
			defaultLoad:true,
			readOnly:true,
			pageSize:9//每页数量
    	}).show();
    	
    	var callback1 = callback;
    	if(typeof callback != "function"){
    		callback1 = function(record){
	    		//添加组件记录
				me.addSouthValueByRecord(record);
				//生成应用组件属性面板
				me.addAppParamsPanel(record);
				//添加元应用到应用中
				me.addAppPanel(record);
	    	};
    	}
    	//选中一条应用后处理
    	var doSelectApp = function(id){
    		var call = function(appInfo){
				if(appInfo && appInfo != ""){
					var a = Ext.define('BTDAppComponentsModel',{
					    extend:'Ext.data.Model',
					    fields:me.getSouthStoreFields()
					});
					var record = Ext.create(a,appInfo);
					me.openItemIdWin(record,callback1,oldRecord);//填写内部编号
					appList.close();//关闭应用列表窗口
				}else{
					Ext.Msg.alert('提示','没有获取到应用信息!');
				}
			};
			me.getAppInfoServer(id,call);
    	};
    	
    	appList.on({
    		okClick:function(){
    			var records = appList.getSelectionModel().getSelection();
    			if(records.length == 0){
    				Ext.Msg.alert("提示","请选择一个应用！");
    			}else if(records.length == 1){
    				doSelectApp(records[0].get(me.fieldsObj.AppComID));
    			}
    		},
    		itemdblclick:function(view,record,tem,index,e,eOpts){
		    	doSelectApp(record.get(me.fieldsObj.AppComID));
    		}
    	});
	},
	/**
	 * 打开联动关系配置窗口
	 * @private
	 */
	openLinkageConWin:function(){
		var me = this;
		
		var win = Ext.create('Ext.panel.Panel',{
			width:650,
			height:300,
			title:'关系配置',
			layout:'border',
			model:true,//模态
    		floating:true,//漂浮
			closable:true,//有关闭按钮
			draggable:true,//可移动
			items:[{
				region:'center',
				xtype:'tabpanel',
				itemId:'tabpanel',
				items:[{
					xtype:'textarea',
					itemId:'textarea1',
					title:'简单配置',
					border:false,
					value:me.getAppParamsValues().linkageValue
				},{
					xtype:'textarea',
					itemId:'textarea2',
					title:'复杂配置',
					border:false,
					value:me.getAppParamsValues().linkageValue2
				}]
			},{
				region:'east',
				width:250,
				xtype:'grid',
				title:'内部组件信息列表',
				store:me.getComponent('south').store,
				columnLines:true,
				split:true,
				collapsible:true,
				columns:[
					{text:'标题文字',dataIndex:'title',width:80},
					{text:'中文名称',dataIndex:me.fieldsObj.CName,width:80},
					{text:'内部编号',dataIndex:'itemId',width:60}
				]
			}],
			dockedItems:[{
		        xtype:'toolbar',
		        dock:'bottom',
		        items:['->',{
		            text:'确定',iconCls:'build-button-ok',handler:function(){
		            	var value1 = this.ownerCt.ownerCt.getComponent('tabpanel').getComponent('textarea1').value;
		            	var value2 = this.ownerCt.ownerCt.getComponent('tabpanel').getComponent('textarea2').value;
		            	
		            	//解析处理联动关系
		            	var result = me.linkageResolve(value1);
		            	if(result.success){
		            		//存储修改的值
		            		me.setAppParamsValues({linkageValue:value1,linkageValue2:value2});
		            		var v = me.changeLinkValue(value2);
		            		//追加复杂关系代码
		            		me.linkageValue += ";" + v;
		            		this.ownerCt.ownerCt.close();
		            	}else{
		            		Ext.Msg.alert("提示",result.message);
		            	}
		            }
		        },{
		            text:'取消',iconCls:'build-button-cancel',handler:function(){
		            	this.ownerCt.ownerCt.close();
		            }
		        }]
		    }]
		});
		win.show();
	},
	/**
	 * 填写内部编号
	 * @private
	 * @param {} record
	 * @param {} callback
	 * @param {} oldRecord
	 */
	openItemIdWin:function(record,callback,oldRecord){
		var me =this;
		
		var itemId = record.get(me.fieldsObj.ModuleOperCode);//默认的功能编号
		var title = "";
		
		if(oldRecord){
			itemId = oldRecord.get('itemId');
			title = oldRecord.get('title');
		}
		
		var win = Ext.create('Ext.window.Window',{
			title:'内部编号',
			modal:true,
			width:250,height:120,
			bodyPadding:'5 10 5 10',
			items:[{
				xtype:'textfield',itemId:'itemId',fieldLabel:'内部编号',labelWidth:55,value:itemId
			},{
				xtype:'textfield',itemId:'title',fieldLabel:'标题文字',labelWidth:55,value:title
			}],
			dockedItems:[{//停靠
				xtype:'toolbar',dock:'bottom',
				items:[
					'->',
					{xtype:'button',text:'确定',iconCls:'build-button-ok',
						handler:function(){
							if(oldRecord){
								me.removeSouthValueByRecord(oldRecord);
							}
							
							var itemId = win.getComponent('itemId').value;
							var title = win.getComponent('title').value;
							var rec = me.getSouthRecordByKeyValue('itemId',itemId);
							if(!rec){
								if(itemId && itemId != ""){
									record.set('itemId',itemId);
									record.set('title',title);
									if(typeof callback == "function"){
										callback(record);
									}
				            		this.ownerCt.ownerCt.close();
								}else{
									Ext.Msg.alert("提示","请填写内部编号！");
								}
							}else{
								Ext.Msg.alert("提示","内部编号已存在，请换一个编号！");
							}
						}
					},
					{xtype:'button',text:'取消',iconCls:'delete',
						handler:function(){
							this.ownerCt.ownerCt.close();
						}
					}
				]
			}]
		});
		win.show();
	},
	/**
	 * 添加元应用到应用中
	 * @private
	 * @param {} record
	 * @param {} par
	 */
	addAppPanel:function(record,par){
		var me = this;
		var params = me.getAppParamsValues();
		var center = me.getCenter();
		
		var ClassCode = record.get(me.fieldsObj.ClassCode);
		var Class = eval(ClassCode);
		var itemId = record.get('itemId');
		var title = record.get('title');
		
		var layoutType = params.layoutType;
		
		var panelParams = {
			itemId:itemId,
			listeners:{}
		};
		//内部标题的处理（不写：显示应用原始的标题；"-":不渲染head；其他：显示填写的标题）
		if(title && title != "" && title == "-"){
			panelParams.title = "";
			panelParams.header = false;
		}else if(title && title != "" && title != "-"){
			panelParams.title = title;
		}
		
		if(par){
			for(var i in par){//追加属性
				panelParams[i] = par[i];
			}
		}
		
		if(layoutType == "1"){//绝对布局
			panelParams.draggable = true;
			panelParams.resizable = true;
			//组件拖动监听
			panelParams.listeners.move = function(com,x,y,eOpts){
                me.setSouthRecordByKeyValue(com.itemId,'x',x);
                me.setSouthRecordByKeyValue(com.itemId,'y',y);
			};
			//大小变化监听
			panelParams.listeners.boxready = function(com,width,height,e){
				me.setSouthRecordByKeyValue(com.itemId,'width',width);
                me.setSouthRecordByKeyValue(com.itemId,'height',height);
			};
			//大小变化监听
			panelParams.listeners.resize = function(com,width,height,oldWidth,oldHeight,e){
	        	me.setSouthRecordByKeyValue(com.itemId,'width',width);
                me.setSouthRecordByKeyValue(com.itemId,'height',height);
			};
		}
		
		var menuItems = [{
			text:'属性面板',iconCls:'build-button-configuration-blue',
			tooltip:'点击切换到应用的属性面板',
			handler:function(){
				me.switchParamsPanel(itemId);
			}
		},{
			text:'移除应用',iconCls:'build-button-delete',
			tooltip:'移除现有的应用模块',
			handler:function(){
				//移除组件记录
        		me.removeSouthValueByRecord(record);
        		//删除应用组件 
        		me.removeAppPanel(panel);
        		//删除组件属性面板
        		me.removeAppParamsPanel(record.get('itemId'));
			}
		},{
			text:"替换应用",iconCls:'build-button-edit',
        	tooltip:'重新选择应用替换现有的应用',
        	handler:function(){
        		var callback = function(record1){
        			
	    			var com = me.getCenter().getComponent(record.get('itemId'));
	    			if(com){
	    				par = {
	    					x:com.x,
	    					y:com.y,
	    					width:com.width,
	    					height:com.height
	    				};
	    			}
        			
					//移除组件记录
	        		me.removeSouthValueByRecord(record);
	        		//删除应用组件 
	        		me.removeAppPanel(panel);
	        		//删除组件属性面板
	        		me.removeAppParamsPanel(record.get('itemId'));
					
	        		//添加值
	        		for(var i in par){
	        			if(par[i]){
	        				record1.set(i,par[i]);
	        			}
	        		}
	        		
	        		record1.commit();
					//添加组件记录
					me.addSouthValueByRecord(record1);
					//生成应用组件属性面板
					me.addAppParamsPanel(record1);
					//添加元应用到应用中
					me.addAppPanel(record1,par);
        		};
        		me.openAppListWin(callback,record);
        	}
		}];
		//右键监听
		panelParams.listeners.contextmenu = {
			element:'el',
			fn:function(e,t,eOpts){
				//禁用浏览器的右键相应事件 
        		e.preventDefault();e.stopEvent();
        		//右键菜单
        		new Ext.menu.Menu({
        			items:menuItems
        		}).showAt(e.getXY());//让右键菜单跟随鼠标位置
			}
		};
		
		var panel = Ext.create(Class,panelParams);
		
		center.add(panel);
		
		if(layoutType == "3"){//tab布局
			center.setActiveTab(panel);
		}
	},
	/**
	 * 从应用中删除元应用
	 * @private
	 * @param {} com
	 */
	removeAppPanel:function(com){
		var me = this;
		var center = me.getCenter();
		center.remove(com);
	},
	/**
     * 浏览处理
     * @private
     */
    browse:function(){
        var me = this;
        var center = me.getCenter();
        var owner = center.ownerCt;
        
        //创建布局面板
        var appPanel = me.createAppItems();
        if(appPanel){
            //删除原先的面板
            owner.remove(center);
            //删除原先的功能属性面板
            
            //添加新的面板
            owner.add(appPanel);
            //添加每个功能的属性面板
            var records = me.getSouthRecords();
            for(var i in records){
            	me.addAppParamsPanel(records[i]);
            }
        }
    },
    //=====================生成内部组件=======================
    /**
     * 创建内部组件
     * @private
     */
    createAppItems:function(){
    	var me = this;
    	var params = me.getAppParamsValues();
    	//布局方式
    	var layoutType = params.layoutType;
    	
    	var com;
    	if(layoutType == "1"){//绝对布局
			com = me.createAbsolutePanelItems();
		}else if(layoutType == "2"){//border布局
			com = me.createBorderPanelItems();
		}else if(layoutType == "3"){//tab布局(tabPanel页面方式)
			com = me.createTabPanelItems();
		}else if(layoutType == "4"){//列布局
			com = me.createColumnPanel();
		}
		
		var values = me.getAppParamsValues();
		com.title = values.titleText;
		com.autoScroll = true;
		com.itemId = "center";
		com.width = parseInt(values.width);
		com.height = parseInt(values.height);
		com.resizable = {handles:'s e'};
		//监听
		com.listeners = {
            //组件大小变 化监听
            resize:function(com,width,height,oldWidth,oldHeight,eOpts){
                var formParamsPanel = me.getCenterParamPanel();
                //表单宽度和高度赋值
                formParamsPanel.getForm().setValues({width:width,height:height});
            },
			contextmenu:{
				element:'el',
				fn:function(e,t,eOpts){
					//禁用浏览器的右键相应事件 
	        		e.preventDefault();e.stopEvent();
	        		//右键菜单
	        		new Ext.menu.Menu({
	        			items:[{
	        				text:'属性面板',iconCls:'build-button-edit',
	        				tooltip:'点击切换到应用的属性面板',
	        				handler:function(){
	        					me.switchParamsPanel('center');
	        				}
	        			}]
	        		}).showAt(e.getXY());//让右键菜单跟随鼠标位置
				}
			}
        };
        com.header = {
            listeners:{
                click:function(){
                    //切换组件属性配置面板
                    me.switchParamsPanel('center');
                }
            }
        };
		
		return com;
    },
    /**
     * 创建绝对布局内容
     * @private
     * @return {}
     */
    createAbsolutePanelItems:function(){
    	var me = this;
    	//参数
		var params = me.getAppParamsValues();
    	var records = me.getSouthRecords();
    	var items = [];
    	for(var i in records){
    		var com = me.createAbsoluteItemsByRecord(records[i]);
    		if(com){
    			items.push(com);
    		}
    	}
    	var panel = {};
    	panel.layout = "absolute";
    	panel.items = items;
    	
    	panel.title = params.titleText;
		panel.autoScroll = true;
		panel.itemId = 'center';
		panel.width = parseInt(params.width);
		panel.height = parseInt(params.height);
		panel.resizable = {handles:'s e'};
		
		return panel;
    },
    /**
     * 创建绝对布局内部组件
     * @private
     * @param {} record
     * @return {}
     */
    createAbsoluteItemsByRecord:function(record){
    	var me = this;
    	var ClassCode = record.get(me.fieldsObj.ClassCode);
		var Class = eval(ClassCode);
		var itemId = record.get('itemId');
		var title = record.get('title');
    	var par = {
			itemId:itemId,
			x:record.get('x') ? record.get('x') : 0,
			y:record.get('y') ? record.get('y') : 0,
			listeners:{}
		};
		if(record.get('width') && record.get('width') > 0){
			par.width = record.get('width');
		}
		if(record.get('height') && record.get('height') > 0){
			par.height = record.get('height');
		}
		//内部标题的处理（不写：显示应用原始的标题；"-":不渲染head；其他：显示填写的标题）
		if(title && title != "" && title == "-"){
			par.title = "";
			par.header = false;
		}else if(title && title != "" && title != "-"){
			par.title = title;
		}
		par.draggable = true;
		par.resizable = true;
		
		//组件拖动监听
		par.listeners.move = function(com,x,y,eOpts){
            me.setSouthRecordByKeyValue(com.itemId,'x',x);
            me.setSouthRecordByKeyValue(com.itemId,'y',y);
		};
		//大小变化监听
		par.listeners.boxready = function(com,width,height,e){
			me.setSouthRecordByKeyValue(com.itemId,'width',width);
            me.setSouthRecordByKeyValue(com.itemId,'height',height);
		};
		//大小变化监听
		par.listeners.resize = function(com,width,height,oldWidth,oldHeight,e){
        	me.setSouthRecordByKeyValue(com.itemId,'width',width);
            me.setSouthRecordByKeyValue(com.itemId,'height',height);
		};
		
//		var menuItems = [];
//		//右键菜单中增加删除选项
//		menuItems.push({
//        	text:"移除",iconCls:'delete',
//        	tooltip:'从应用中移除该元应用',
//        	handler:function(){
//        		//移除组件记录
//        		me.removeSouthValueByRecord(record);
//        		//删除应用组件 
//        		me.removeAppPanel(panel);
//        		//删除组件属性面板
//        		me.removeAppParamsPanel(itemId);
//        	}
//		});
		var menuItems = [{
			text:'属性面板',iconCls:'build-button-configuration-blue',
			tooltip:'点击切换到应用的属性面板',
			handler:function(){
				me.switchParamsPanel(itemId);
			}
		},{
			text:'移除应用',iconCls:'build-button-delete',
			tooltip:'移除现有的应用模块',
			handler:function(){
				//移除组件记录
        		me.removeSouthValueByRecord(record);
        		//删除应用组件 
        		me.removeAppPanel(panel);
        		//删除组件属性面板
        		me.removeAppParamsPanel(record.get('itemId'));
			}
		},{
			text:"替换应用",iconCls:'build-button-edit',
        	tooltip:'重新选择应用替换现有的应用',
        	handler:function(){
        		var callback = function(record1){
					//移除组件记录
	        		me.removeSouthValueByRecord(record);
	        		//删除应用组件 
	        		me.removeAppPanel(panel);
	        		//删除组件属性面板
	        		me.removeAppParamsPanel(record.get('itemId'));
					
	        		//添加值
	        		for(var i in par){
	        			if(par[i]){
	        				record1.set(i,par[i]);
	        			}
	        		}
	        		record1.commit();
					//添加组件记录
					me.addSouthValueByRecord(record1);
					//生成应用组件属性面板
					me.addAppParamsPanel(record1);
					//添加元应用到应用中
					me.addAppPanel(record1,par);
        		};
        		me.openAppListWin(callback,record);
        	}
		}];
		//右键监听
		par.listeners.contextmenu = {
			element:'el',
			fn:function(e,t,eOpts){
				//禁用浏览器的右键相应事件 
        		e.preventDefault();e.stopEvent();
        		//右键菜单
        		new Ext.menu.Menu({
        			items:menuItems
        		}).showAt(e.getXY());//让右键菜单跟随鼠标位置
			}
		};
		
		var panel = Ext.create(Class,par);
		return panel;
    },
    /**
     * 创建border布局内容
     * @private
     * @return {}
     */
    createBorderPanelItems:function(){
    	var me = this;
    	//参数
		var params = me.getAppParamsValues();
    	var records = me.getSouthRecords();
    	var items = [];
    	for(var i in records){
    		var com = me.createBorderItemsByRecord(records[i]);
    		if(com){
    			items.push(com);
    		}
    	}
    	var panel = {};
    	panel.layout = "border";
    	panel.items = items;
    	
    	panel.title = params.titleText;
		panel.autoScroll = true;
		panel.itemId = 'center';
		panel.width = parseInt(params.width);
		panel.height = parseInt(params.height);
		panel.resizable = {handles:'s e'};
		
		return panel;
    },
    /**
     * 创建border布局内部组件
     * @private
     * @param {} record
     * @return {}
     */
    createBorderItemsByRecord:function(record){
    	var me = this;
    	var values = me.borderObj;
    	var region = record.get('region');
    	
    	var par = {};
    	if(region == "center"){//中
    		par = {
    			region:'center',
    			border:values.center_border
    		};
    	}else if(region == "north"){//上
    		par = {
    			region:'north',
				split:values.top_split,//可收缩
				collapsible:values.top_split,//可收缩
				collapsed:values.top_collapsed,//默认收缩
				border:values.top_border,//有边框
				height:values.top_height//高度
    		};
    	}else if(region == "south"){//下
    		par = {
    			region:'south',
				split:values.bottom_split,//可收缩
				collapsible:values.bottom_split,//可收缩
				collapsed:values.bottom_collapsed,//默认收缩
				border:values.bottom_border,//有边框
				height:values.bottom_height//高度
    		};
    	}else if(region == "west"){//左
    		par = {
    			region:'west',
				split:values.left_split,//可收缩
				collapsible:values.left_split,//可收缩
				collapsed:values.left_collapsed,//默认收缩
				border:values.left_border,//有边框
				width:values.left_width//宽度
    		};
    	}else if(region == "east"){//右
    		par = {
    			region:'east',
				split:values.right_split,//可收缩
				collapsible:values.right_split,//可收缩
				collapsed:values.right_collapsed,//默认收缩
				border:values.right_border,//有边框
				width:values.right_width//宽度
    		};
    	}
    	
		if(par){
			var ClassCode = record.get(me.fieldsObj.ClassCode);
			var Class = eval(ClassCode);
			var itemId = record.get('itemId');
			var title = record.get('title');
			//内部编号
			par.itemId = itemId;
			//内部标题的处理（不写：显示应用原始的标题；"-":不渲染head；其他：显示填写的标题）
			if(title && title != "" && title == "-"){
				par.title = "";
				par.header = false;
			}else if(title && title != "" && title != "-"){
				par.title = title;
			}
			var menuItems = [{
				text:'属性面板',iconCls:'build-button-configuration-blue',
				tooltip:'点击切换到应用的属性面板',
				handler:function(){
					me.switchParamsPanel(itemId);
				}
			},{
				text:"替换应用",iconCls:'build-button-edit',
	        	tooltip:'重新选择应用替换现有的应用',
	        	handler:function(){
	        		var callback = function(record1){
						//移除组件记录
		        		me.removeSouthValueByRecord(record);
		        		//删除应用组件 
		        		me.removeAppPanel(panel);
		        		//删除组件属性面板
		        		me.removeAppParamsPanel(record.get('itemId'));
						
						//添加值
		        		for(var i in par){
		        			if(par[i] && i != "itemId" && i != "title"){
		        				record1.set(i,par[i]);
		        			}else{
		        				delete par[i];
		        			}
		        		}
		        		record1.commit();
		        		//添加组件记录
						me.addSouthValueByRecord(record1);
						//生成应用组件属性面板
						me.addAppParamsPanel(record1);
						//添加元应用到应用中
						me.addAppPanel(record1,par);
	        		};
	        		me.openAppListWin(callback,record);
	        	}
			}];
			//右键监听
			par.listeners = {
				contextmenu:{
					element:'el',
					fn:function(e,t,eOpts){
						//禁用浏览器的右键相应事件 
		        		e.preventDefault();e.stopEvent();
		        		//右键菜单
		        		new Ext.menu.Menu({
		        			items:menuItems
		        		}).showAt(e.getXY());//让右键菜单跟随鼠标位置
					}
				}
			}
		}
		var panel = Ext.create(Class,par);
    	return panel;
    },
	/**
	 * 创建tab布局内容
	 * @private
	 * @return {}
	 */
	createTabPanelItems:function(){
		var me = this;
    	//参数
		var params = me.getAppParamsValues();
    	var records = me.getSouthRecords();
    	var items = [];
    	for(var i in records){
    		var com = me.createTabItemsByRecord(records[i]);
    		if(com){
    			items.push(com);
    		}
    	}
    	var panel = {};
    	panel.xtype = "tabpanel";
    	panel.items = items;
    	
    	panel.title = params.titleText;
		panel.autoScroll = true;
		panel.itemId = 'center';
		panel.width = parseInt(params.width);
		panel.height = parseInt(params.height);
		panel.resizable = {handles:'s e'};
		
		return panel;
	},
	/**
	 * 创建tab布局内部组件
	 * @private
	 * @param {} record
	 * @return {}
	 */
	createTabItemsByRecord:function(record){
		var me = this;
		var par = {};
		var ClassCode = record.get(me.fieldsObj.ClassCode);
		var Class = eval(ClassCode);
		var itemId = record.get('itemId');
		var title = record.get('title');
		//内部编号
		par.itemId = itemId;
		//内部标题的处理（不写：显示应用原始的标题；"-":不渲染head；其他：显示填写的标题）
		if(title && title != "" && title == "-"){
			par.title = "";
			par.header = false;
		}else if(title && title != "" && title != "-"){
			par.title = title;
		}
		var menuItems = [{
			text:'属性面板',iconCls:'build-button-configuration-blue',
			tooltip:'点击切换到应用的属性面板',
			handler:function(){
				me.switchParamsPanel(itemId);
			}
		},{
			text:'移除应用',iconCls:'build-button-delete',
			tooltip:'删除现有的应用模块',
			handler:function(){
				//移除组件记录
        		me.removeSouthValueByRecord(record);
        		//删除应用组件 
        		me.removeAppPanel(panel);
        		//删除组件属性面板
        		me.removeAppParamsPanel(record.get('itemId'));
			}
		},{
			text:"替换应用",iconCls:'build-button-edit',
        	tooltip:'重新选择应用替换现有的应用',
        	handler:function(){
        		var callback = function(record1){
					//移除组件记录
	        		me.removeSouthValueByRecord(record);
	        		//删除应用组件 
	        		me.removeAppPanel(panel);
	        		//删除组件属性面板
	        		me.removeAppParamsPanel(record.get('itemId'));
					
					//添加值
	        		for(var i in par){
	        			if(par[i]){
	        				record1.set(i,par[i]);
	        			}
	        		}
	        		record1.commit();
	        		//添加组件记录
					me.addSouthValueByRecord(record1);
					//生成应用组件属性面板
					me.addAppParamsPanel(record1);
					//添加元应用到应用中
					me.addAppPanel(record1,par);
        		};
        		me.openAppListWin(callback,record);
        	}
		}];
		//右键监听
		par.listeners = {
			contextmenu:{
				element:'el',
				fn:function(e,t,eOpts){
					//禁用浏览器的右键相应事件 
	        		e.preventDefault();e.stopEvent();
	        		//右键菜单
	        		new Ext.menu.Menu({
	        			items:menuItems
	        		}).showAt(e.getXY());//让右键菜单跟随鼠标位置
				}
			}
		}
		var panel = Ext.create(Class,par);
    	return panel;
	},
    //=====================布局组件=======================
	/**
	 * 创建布局面板
	 * @private
	 * @param {} layoutType
	 */
	createLayoutPanel:function(layoutType){
		var me = this;
		
		var centerParamPanel = me.getCenterParamPanel();
		var selectApp = centerParamPanel.getComponent('layoutType').getComponent('selectApp');
		var borderLayoutCon = centerParamPanel.getComponent('layoutType').getComponent('borderLayoutCon');
		
		var com = null;
		if(layoutType == "1"){//绝对布局
			selectApp.show();
			borderLayoutCon.hide();
			com = me.createAbsolutePanel();
		}else if(layoutType == "2"){//border布局
			selectApp.hide();
			borderLayoutCon.show();
			com = me.createBorderPanel();
		}else if(layoutType == "3"){//tab布局(tabPanel页面方式)
			selectApp.show();
			borderLayoutCon.hide();
			com = me.createTabPanel();
		}else if(layoutType == "4"){//列布局
			selectApp.hide();
			borderLayoutCon.hide();
			com = me.createColumnPanel();
		}
		
		var values = me.getAppParamsValues();
		com.title = values.titleText;
		com.autoScroll = true;
		com.itemId = "center";
		com.width = parseInt(values.width);
		com.height = parseInt(values.height);
		com.resizable = {handles:'s e'};
		//监听
		com.listeners = {
            //组件大小变 化监听
            resize:function(com,width,height,oldWidth,oldHeight,eOpts){
                var formParamsPanel = me.getCenterParamPanel();
                //表单宽度和高度赋值
                formParamsPanel.getForm().setValues({width:width,height:height});
            },
			contextmenu:{
				element:'el',
				fn:function(e,t,eOpts){
					//禁用浏览器的右键相应事件 
	        		e.preventDefault();e.stopEvent();
	        		//右键菜单
	        		new Ext.menu.Menu({
	        			items:[{
	        				text:'属性面板',iconCls:'build-button-edit',
	        				tooltip:'点击切换到应用的属性面板',
	        				handler:function(){
	        					me.switchParamsPanel('center');
	        				}
	        			}]
	        		}).showAt(e.getXY());//让右键菜单跟随鼠标位置
				}
			}
        };
        com.header = {
            listeners:{
                click:function(){
                    //切换组件属性配置面板
                    me.switchParamsPanel('center');
                }
            }
        };
		
		return com;
	},
	/**
	 * 创建绝对布局面板
	 * @private
	 * @return {}
	 */
	createAbsolutePanel:function(){
		var com = {
			xtype:'panel',
			layout:'absolute'
		};
		return com;
	},
	/**
	 * 创建border布局面板
	 * @private
	 * @return {}
	 */
	createBorderPanel:function(){
		var me = this;
		var values = me.borderObj;
		
		var items = [{
			region:'center',
			border:values.center_border,//有边框
			items:[{
				xtype:'button',
				text:'添加应用',
				handler:function(button,e){
					me.addBorderAppByButton(button);
				}
			}]
		}];
		
		if(values.top_show){//展现上部
			items.push({
				region:'north',
				split:values.top_split,//可收缩
				collapsible:values.top_split,//可收缩
				collapsed:values.top_collapsed,//默认收缩
				border:values.top_border,//有边框
				height:values.top_height,//高度
				items:[{
					xtype:'button',
					text:'添加应用',
					handler:function(button,e){
						me.addBorderAppByButton(button);
					}
				}]
			});
		}
		if(values.bottom_show){//展现下部
			items.push({
				region:'south',
				split:values.bottom_split,//可收缩
				collapsible:values.bottom_split,//可收缩
				collapsed:values.bottom_collapsed,//默认收缩
				border:values.bottom_border,//有边框
				height:values.bottom_height,//高度
				items:[{
					xtype:'button',
					text:'添加应用',
					handler:function(button,e){
						me.addBorderAppByButton(button);
					}
				}]
			});
		}
		if(values.left_show){//展现左部
			items.push({
				region:'west',
				split:values.left_split,//可收缩
				collapsible:values.left_split,//可收缩
				collapsed:values.left_collapsed,//默认收缩
				border:values.left_border,//有边框
				width:values.left_width,//宽度
				items:[{
					xtype:'button',
					text:'添加应用',
					handler:function(button,e){
						me.addBorderAppByButton(button);
					}
				}]
			});
		}
		if(values.right_show){//展现右部
			items.push({
				region:'east',
				split:values.right_split,//可收缩
				collapsible:values.right_split,//可收缩
				collapsed:values.right_collapsed,//默认收缩
				border:values.right_border,//有边框
				width:values.right_width,//宽度
				items:[{
					xtype:'button',
					text:'添加应用',
					handler:function(button,e){
						me.addBorderAppByButton(button);
					}
				}]
			});
		}
		
		//布局面板
		var com = {
			xtype:'panel',
			layout:{
				type:'border',
				regionWeights:me.getBorderRegionWeights()
			},
			items:items
		};
		return com;
	},
	/**
	 * 创建tab布局
	 * @private
	 * @return {}
	 */
	createTabPanel:function(){
		var me = this;
		var com = {
			xtype:'tabpanel',
			defaults: { 
            	autoScroll:true
            },
            listeners:{
            	tabchange:function(tabPanel,newCard,oldCard,eOpts){
            		//切换组件属性配置面板
            		me.switchParamsPanel(newCard.itemId);
            	}
            }
		};
		return com;
	},
	/**
	 * 创建列布局
	 * @private
	 * @return {}
	 */
	createColumnPanel:function(){
		var me = this;
		var com = {
			xtype:'panel',
			layout:'column',
			items:[{
				
			}]
		};
		return com;
	},
	/**
	 * 修改border布局
	 * @private
	 * @param {} values
	 */
	editBorderLayoutPanel:function(){
		var me = this;
		//布局面板
		var panel = me.createBorderPanel();
		//参数
		var params = me.getAppParamsValues();
		
		panel.title = params.titleText;
		panel.autoScroll = true;
		panel.itemId = 'center';
		panel.width = parseInt(params.width);
		panel.height = parseInt(params.height);
		panel.resizable = {handles:'s e'};
		//监听
		panel.listeners = {
            //组件大小变 化监听
            resize:function(com,width,height,oldWidth,oldHeight,eOpts){
                var formParamsPanel = me.getCenterParamPanel();
                //表单宽度和高度赋值
                formParamsPanel.getForm().setValues({width:width,height:height});
            }
        };
        panel.header = {
            listeners:{
                click:function(){
                    //切换组件属性配置面板
                    me.switchParamsPanel('center');
                }
            }
        };
		
		me.changeLayoutPanel(panel);
	},
	/**
	 * border布局添加应用
	 * @private
	 * @param {} button
	 */
	addBorderAppByButton:function(button){
		var me = this;
		
		var getParams = function(obj){
			var com = {
				region:obj.region,
				split:obj.split,//可收缩
				collapsible:obj.collapsible,//可收缩
				collapsed:obj.collapsed,//默认收缩
				border:obj.border//有边框
			}
			if(obj.region == "west" || obj.region == "east"){
				com.width = obj.width;//宽度
			}
			if(obj.region == "north" || obj.region == "south"){
				com.height = obj.height;//高度
			}
			return com;
		};
		
		var changeReocrd = function(record,par){
			//记录当前应用的区域及宽高
			record.set('region',par.region);
			record.set('split',par.split);
			record.set('collapsible',par.collapsible);
			record.set('collapsed',par.collapsed);
			record.set('border',par.border);
			if(par.region == "west" || par.region == "east"){
				record.set('width',par.width);
			}else if(par.region == "north" || par.region == "south"){
				record.set('height',par.height);
			}
			record.commit();
		}
		
		var callback = function(record){
			//追加属性
			var par = getParams(button.ownerCt);
			//改变reocrd的值
			changeReocrd(record,par);
			//添加组件记录
			me.addSouthValueByRecord(record);
			//生成应用组件属性面板
			me.addAppParamsPanel(record);
			//删除原有区域
			var parent = button.ownerCt.ownerCt;
			parent.remove(button.ownerCt);
			//添加元应用到应用中
			me.addAppPanel(record,par);
		};
		
		me.openAppListWin(callback);
	},
	
	/**
	 * 修改布局面板
	 * @private
	 * @param {} panel
	 */
	changeLayoutPanel:function(panel){
		var me = this;
		//删除原先的组件属性面板
		var southRecords = me.getSouthRecords();
		for(var i in southRecords){
			var record = southRecords[i];
			me.removeAppParamsPanel(record.get('itemId'));
		}
		//清空组件属性列表
		me.removeAllSouthRecords();
		
		var center = me.getCenter();
		var parent = center.ownerCt;
		//删除原先的布局
		parent.remove(center);
		//添加新的布局
		parent.add(panel);
	},
	/**
	 * 布局方式改变处理
	 * @private
	 */
	changeLayoutSet:function(){
		var me = this;
		var centerParamPanel = me.getCenterParamPanel();
		var layoutTypeSet = centerParamPanel.getComponent('layoutType');
		var layoutType = layoutTypeSet.getComponent('layoutType').value;//布局方式
		var selectApp = layoutTypeSet.getComponent('selectApp');//选中应用
		var borderLayoutCon = layoutTypeSet.getComponent('borderLayoutCon');//border布局配置
		
		if(layoutType == "1"){//绝对布局
			selectApp.show();
			borderLayoutCon.hide();
		}else if(layoutType == "2"){//border布局
			selectApp.hide();
			borderLayoutCon.show();
		}else if(layoutType == "3"){//tab布局(tabPanel页面方式)
			selectApp.show();
			borderLayoutCon.hide();
		}else if(layoutType == "4"){//列布局
			selectApp.hide();
			borderLayoutCon.hide();
		}
	},
	//=====================组件属性面板=======================
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
			
			//me.setParamsPanelValues(componentItemId);//给属性面板赋值
			
			panel.show();//打开
			me.OpenedParamsPanel = componentItemId;
		}
	},
	/**
	 * 添加组件属性面板
	 * @private
	 * @param {} record
	 */
	addAppParamsPanel:function(record){
		var me = this;
		var east = me.getComponent('east');
		//创建组件属性面板
		var panel = me.createAppParamsPanel(record);
		//添加面板
		east.add(panel);
	},
	/**
	 * 删除组件属性面板
	 * @private
	 * @param {} itemId
	 */
	removeAppParamsPanel:function(itemId){
		var me = this;
		var east = me.getComponent('east');
		//属性面板itemId
		var panelItemId = itemId + me.ParamsPanelItemIdSuffix;
		//删除面板
		east.remove(panelItemId);
		//定位
		me.switchParamsPanel('center');
	},
	/**
	 * 创建组件属性面板
	 * @private
	 * @param {} record
	 * @return {}
	 */
	createAppParamsPanel:function(record){
		var me = this;
		//属性面板itemId
		var panelItemId = record.get('itemId') + me.ParamsPanelItemIdSuffix;
		var title = "属性面板【内部编号：" + record.get('itemId') + "】";
		var com = {
			xtype:'form',
			itemId:panelItemId,
			title:title,
			header:false,
			autoScroll:true,
			border:false,
	        bodyPadding:5,
	        items:[],
	        hidden:true
		};
		//组件的属性
		com.items.push(me.createComponentInfo());
		
		return com;
	},
	/**
	 * 组件的属性
	 * @private
	 * @return {}
	 */
	createComponentInfo:function(){
		var com = {
			xtype:'fieldset',title:'基础属性',padding:'0 5 0 5',collapsible:true,
	        defaultType:'textfield',defaults:{anchor:'100%'},layout:'anchor',
	        itemId:'basic',
	        items:[{
	        	xtype:'textfield',fieldLabel:'标题',labelWidth:55,anchor:'100%',
	            itemId:'title',name:'title'
	        },{
                xtype:'checkbox',itemId:'showTitle',name:'showTitle',boxLabel:'显示标题',checked:true
            },{
                xtype:'checkbox',itemId:'hasBorder',name:'hasBorder',boxLabel:'边框',checked:true
            }]
		};
		return com;
	},
	
	//=====================设置获取参数=======================
	/**
	 * 获取应用参数
	 * @private
	 * @return {}
	 */
	getAppParamsValues:function(){
		var me = this;
		var appParamsPanel = me.getCenterParamPanel();
		var params = appParamsPanel.getForm().getValues();
		
		params.width = parseInt(params.width);
		params.height = parseInt(params.height);
		
		return params;
	},
	/**
	 * 设置应用参数
	 * @private
	 * @param {} obj
	 */
	setAppParamsValues:function(obj){
		var me = this;
		var appParamsPanel = me.getCenterParamPanel();
		appParamsPanel.getForm().setValues(obj);
	},
	/**
	 * 添加组件记录
	 * @private
	 * @param {} record
	 */
	addSouthValueByRecord:function(record){
		var me = this;
		var store = me.getComponent('south').store;
		store.add(record);
	},
	/**
	 * 移除组件记录
	 * @private
	 * @param {} record
	 */
	removeSouthValueByRecord:function(record){
		var me = this;
		var store = me.getComponent('south').store;
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
	 * 根据key和value从应用组件属性列表中获取信息
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
		var store = me.getComponent('south').store;
		
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
     * longfc10
     * 获取组件属性列表Fields
     * @private
     * @return {}
     */
    getSouthStoreFields:function(){
        var me = this;
        var fields = [
    		{name:'itemId',type:'string'},//内部编号
    		{name:'title',type:'string'},//标题
    		{name:'region',type:'string'},//border布局时的区域
    		{name:'split',type:'bool'},//可收缩
    		{name:'collapsible',type:'bool'},//可收缩
    		{name:'collapsed',type:'bool'},//默认收缩
    		{name:'border',type:'bool'},//有边框
    		
    		{name:'hasTitle',type:'bool'},//是否有标题
    		{name:'x',type:'int'},//x
    		{name:'y',type:'int'},//y
    		{name:'width',type:'int'},//宽度
    		{name:'height',type:'int'},//高度
    		
            {name:me.fieldsObj.AppComID,type:'string'},//应用组件ID
	    	{name:me.fieldsObj.CName,type:'string'},//中文名称
	    	{name:me.fieldsObj.EName,type:'string'},//英文名称
	    	{name:me.fieldsObj.ModuleOperCode,type:'string'},//功能编码
	    	{name:me.fieldsObj.ModuleOperInfo,type:'string'},//功能简介
	    	{name:me.fieldsObj.InitParameter,type:'string'},//初始化参数
	    	{name:me.fieldsObj.BuildType,type:'string'},//应用类型
	    	{name:me.fieldsObj.BTDModuleType,type:'string'},//构建类型
	    	{name:me.fieldsObj.ExecuteCode,type:'string'},//模块类型
	    	{name:me.fieldsObj.DesignCode,type:'string'},//执行代码
	    	{name:me.fieldsObj.ClassCode,type:'string'},//设计代码
	    	{name:me.fieldsObj.Creator,type:'string'},//类代码
	    	{name:me.fieldsObj.Modifier,type:'string'},//创建者
	    	{name:me.fieldsObj.Modifier,type:'string'},//修改者
	    	{name:me.fieldsObj.PinYinZiTou,type:'string'},//汉字拼音字头
	    	{name:me.fieldsObj.DataAddTime,type:'string '},//数据加入时间
	    	{name:me.fieldsObj.DataUpdateTime,type:'string'},//数据更新时间
	    	{name:me.fieldsObj.LabID,type:'string'},//实验室ID
	    	{name:me.fieldsObj.DataTimeStamp,type:'string'}//时间戳
        ];
        return fields;
    },
	/**
	 * 获取展示区域
	 * @private
	 * @return {}
	 */
	getCenter:function(){
		var me = this;
		var center = me.getComponent('center').getComponent('center');
		return center;
	},
	/**
	 * 获取center属性面板
	 * @private
	 * @return {}
	 */
	getCenterParamPanel:function(){
		var me=  this;
		var panel = me.getComponent('east').getComponent('center' + me.ParamsPanelItemIdSuffix);
		return panel;
	},
	/**
	 * 组件属性列表清空数据
	 * @private
	 */
	removeAllSouthRecords:function(){
		var me = this;
        var store = me.getComponent('south').store;
        store.removeAll();
	},
	/**
     * 给组件属性列表赋值
     * @private
     * @param {} InteractionField
     * @param {} key
     * @param {} value
     */
    setSouthRecordByKeyValue:function(itemId,key,value){
        var me = this;
        var store = me.getComponent('south').store;
        var record = store.findRecord('itemId',itemId);
        if(record != null){//存在
            record.set(key,value);
            record.commit();
        }
    },
	/**
     * 获取设计代码
     * @private
     * @return {}
     */
    getAppParams:function(){
        var me = this;
        
        var panelParams = me.getAppParamsValues();
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
        
        var callback = function(appInfo){
            var appParams = Ext.JSON.decode(appInfo[me.fieldsObj.DesignCode]);
            var panelParams = appParams.panelParams;
            var southParams = appParams.southParams;
            
            me.DataTimeStamp = appInfo[me.fieldsObj.DataTimeStamp];
             //赋值
            me.setAppParamsValues(panelParams);
            me.setSouthRecordByArray(southParams);
            
            var borderObjStr = panelParams.borderObjStr;
            if(borderObjStr && borderObjStr != ""){
            	me.borderObj = eval("(" + borderObjStr + ")");
            }
            
            me.changeLayoutSet();
            //渲染
           	me.browse();
           	var link = function(){
	           	//联动关系处理
	            var value = panelParams.linkageValue;
	            me.linkageResolve(value);
	            var value2 = panelParams.linkageValue2;
	            var v = me.changeLinkValue(value2);
	    		me.linkageValue += ";" + v;
           	}
           	setTimeout(link,500);
        };
        //从后台获取应用信息
        me.getAppInfoServer(me.appId,callback);
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
     * 获取border布局对象字符串
     * @private
     * @return {}
     */
    getBorderObjStr:function(){
    	var me = this;
    	var obj = me.borderObj;
    	
    	var objStr = "{";
    	for(var i in obj){
    		objStr = objStr + i + ":" + obj[i] + ",";
    	}
    	if(objStr.length > 1){
    		objStr = objStr.substring(0,objStr.length-1);
    	}
    	objStr += "}";
    	
    	return objStr;
    },
    /**
     * 获取border布局的优先级
     * @private
     * @return {}
     */
    getBorderRegionWeights:function(){
    	var me = this;
    	var values = me.borderObj;
    	
    	var regionWeights = {};
		if(values.top_show){//上
			regionWeights.north = values.top_priority;
		}
		if(values.bottom_show){//下
			regionWeights.south = values.bottom_priority;
		}
		if(values.left_show){//左
			regionWeights.west = values.left_priority;
		}
		if(values.right_show){//右
			regionWeights.east = values.right_priority;
		}
    	return regionWeights;
    },
    /**
     * 获取border布局的优先级Str
     * @private
     * @return {}
     */
    getBorderRegionWeightsStr:function(){
    	var me = this;
    	var regionWeights = me.getBorderRegionWeights();
    	var regionWeightsStr = me.JsonToStr(regionWeights);
    	return regionWeightsStr;
    },
    //=====================关系配置解析代码====================
	/**
	 * 联动关系代码串转化为代码数组
	 * @private
	 * @param {} value
	 * @return {}
	 */
	linkageStrToArr:function(value){
		var result = [];
		var separator = ";";
		var arr = value.split(separator);
		
		for(var i in arr){
			var str = arr[i].trim();//首尾去掉空格及换行符
			if(str != ""){
				result.push(str);
			}
		}
		
		return result;
	},
	/**
	 * 获取hql结果
	 * @private
	 * @param {} value
	 * @return {}
	 */
	getHql:function(value){
		var me = this;
		var result = {parArr:[],hql:''};//参数数组、hql串
		
		result.hql = value.replace(/\'/g,"\\\'");
		var reg = /(\{.*?\})/g;//提取所有的{}中的字符串
		var arr = value.match(reg);
		result.parArr = arr;
		
		result.hql = "'" + result.hql + "'";
		
		for(var i in arr){
			result.hql = result.hql.replace(arr[i],("'+" + arr[i].slice(1,-1) + "+'"));
			arr[i] = arr[i].slice(1,-1);
		}
		if(result.hql.slice(-3) == "+''"){
			result.hql = result.hql.slice(0,-3);
		}
		return result;
	},
	/**
	 * 联动解析
	 * @private
	 * @param {} value
	 */
	linkageResolve:function(value){
		var me = this;
		var obj = me.resolveLinks(value);
		var objLinkStr = obj.objLinkStr;
		var saveLinkStr = obj.saveLinkStr;
		
		if(obj.success){
			if(me.removeLinkageFunctionArr.length > 0){
				for(var i in me.removeLinkageFunctionArr){
					if(me.removeLinkageFunctionArr[i] && Ext.typeOf(me.removeLinkageFunctionArr[i]) == "function"){
						me.removeLinkageFunctionArr[i]();
					}
				}
			}
			me.removeLinkageFunctionArr = obj.removeLinkageFunctionArr;
			me.linkageValue = obj.saveLinkStr;
		}
		
		return obj;
	},
	/**
	 * 整体联动解析
	 * @private
	 * @param {} value
	 * @return {}
	 */
	resolveLinks:function(value){
		var me = this;
		//是否成功、可视化区域的联动关系代码、需要保存的联动代码、需要删除的关系
		var result = {success:true,objLinkStr:"",saveLinkStr:"",message:'',removeLinkageFunctionArr:[]};
		if(value && value != ""){
			//联动关系代码数组
			var linkageArr = me.linkageStrToArr(value);
			for(var i in linkageArr){
				var obj = me.resolveLink(linkageArr[i]);
				if(obj.success){
					result.objLinkStr += obj.objLinkStr;
					result.saveLinkStr += obj.saveLinkStr;
					result.removeLinkageFunctionArr.push(obj.removeLinkageFunction);
				}else{
					result.success = false;
					result.message += "<b style='color:red'>出错语句：【</b>" + linkageArr[i] + "<b style='color:red'>】</b><br>";
				}
			}
		}
		return result;
	},
	/**
	 * 单个联动解析
	 * @private
	 * @param {} value
	 * @return {}
	 */
	resolveLink:function(value){
		var me = this;
		//是否成功、可视化区域的联动关系代码、需要保存的联动代码、需要删除的关系
		var result = {success:true,objLinkStr:"",saveLinkStr:"",removeLinkageFunction:{}};
		if(value && value != ""){
			//将联动代码分为主动和被动两端代码
			var linkArr = value.split("==");
			if(linkArr.length == 2){
				//去掉=两端的空格
				for(var i in linkArr){
					linkArr[i] = linkArr[i].trim();
				}
				//当前视图的联动关系代码串
				var objLinkStr = me.getLinkStr(linkArr,"me.getCenter()");
				if(objLinkStr.success){
					result.objLinkStr = objLinkStr.value;
				}
				//应用保存后的联动关系代码串
				var saveLinkStr = me.getLinkStr(linkArr,"me");
				if(saveLinkStr.success){
					result.saveLinkStr = saveLinkStr.value;
				}
				//解析成功
				if(!objLinkStr.success || !saveLinkStr.success){
					result.success = false;
				}else{
					result.removeLinkageFunction = me.getLinkObj(linkArr,"me.getCenter()") || {};
				}
			}
		}
		return result;
	},
	/**
	 * 生成联动关系代码串
	 * @private
	 * @param {} linkArr
	 * @param {} objStr
	 * @return {}
	 */
	getLinkStr:function(linkArr,objStr){
		var me = this;
		var result = {success:false,value:""};
		
		var act = linkArr[0].split("(");
		var actArr = act[0].split(".");//主动方
		var funArr = linkArr[1].split(".");//被动方
		
		//需要解析的代码不符合格式
		if(actArr.length < 2 || funArr.length < 2){
			return result;
		}
		
		//主动方代码
		var actObjStr = objStr;
		var actObjName = "";
		for(var i=0;i<actArr.length-1;i++){
			actObjStr += ".getComponent('" + actArr[i] + "')";
			actObjName += "_" + actArr[i];
		}
		actObjStr = "var " + actObjName + "=" + actObjStr + ";";
		
		//被动方代码
		var funObjStr = objStr;
		var funObjName = "";
		for(var i=0;i<funArr.length-1;i++){
			funObjStr += ".getComponent('" + funArr[i] + "')";
			funObjName += "_" + funArr[i];
		}
		funObjStr = "var " + funObjName + "=" + funObjStr + ";";
		
		var actEvent= act.length == 2 ? act[1].slice(0,-1) : "";//参数串
		var actEventName = actArr[actArr.length-1];//主动方事件名
		var actEventParNameArr = actEvent.split(",");//主动方参数数组
		var hqlObj = {};//hql信息对象
		if(actEvent == ""){//没有参数
			actEventParNameArr = "";
		}else{//有参数
			if(actEvent.slice(0,2) == "##"){//hql串
				actEventParNameArr = actEvent;
				hqlObj = me.getHql(actEvent.slice(2));
			}
		}
		
		var funFunName = funArr[funArr.length-1];//被动方方法名
		
		
		var str = "";
		str += actObjStr;
		
		//新增、删除[列表、树]、保存[表单]、重置[高级查询]
		if(actEventName == "addClick" || actEventName == "delClick" || actEventName == "saveClick" || actEventName == "resetClick"){
			str += actObjName + ".on({" + actEventName + ":function(but){";
			str += funObjStr;
			str += funObjName + "." + funFunName + "();";
			str += "}});"
		}else if(actEventName == "itemclick" || actEventName == "itemdblclick"){//行单击、行双击事件（列表、树）
			str += actObjName + ".on({" + actEventName + ":function(view,record){";
			
			if(Ext.typeOf(actEventParNameArr) == "string" && actEventParNameArr == ""){
				str += "var id=record.get(" + actObjName + ".objectName+'_Id');";
				str += funObjStr;
				str += funObjName + "." + funFunName + "(id);";
			}else{
				if(Ext.typeOf(actEventParNameArr) == "string" && actEventParNameArr.slice(0,2) == "##"){//hql串
					for(var i in hqlObj.parArr){
						str +=  "var " + hqlObj.parArr[i] + "=record.get('" + hqlObj.parArr[i] + "');";
					}
					str += "var hql=" + hqlObj.hql + ";";
					str += funObjStr;
					str += funObjName + "." + funFunName + "(hql);";
				}else{
					for(var i in actEventParNameArr){
						str += "var " + actEventParNameArr[i] + "=record.get('" + actEventParNameArr[i] + "');";
					}
					str += funObjStr;
					str += funObjName + "." + funFunName + "(" + actEventParNameArr.join(",") + ");";
				}
			}
			
			str += "}});"
		}else if(actEventName == "editClick" || actEventName == "showClick"){//修改、查看（列表、树）
			str += actObjName + ".on({" + actEventName + ":function(but){";
			str += "var list=" + actObjName + ";";
			str += "var records=list.getSelectionModel().getSelection();";
			str += "if(records.length==1){";
			str += "var record=records[0];";
			if(Ext.typeOf(actEventParNameArr) == "string" && actEventParNameArr == ""){
				str += "var id=record.get(" + actObjName + ".objectName+'_Id');";
				str += funObjStr;
				str += funObjName + "." + funFunName + "(id);";
			}else{
				if(Ext.typeOf(actEventParNameArr) == "string" && actEventParNameArr.slice(0,2) == "##"){//hql串
					for(var i in hqlObj.parArr){
						str +=  "var " + hqlObj.parArr[i] + "=record.get('" + hqlObj.parArr[i] + "');";
					}
					str += "var hql=" + hqlObj.hql + ";";
					str += funObjStr;
					str += funObjName + "." + funFunName + "(hql);";
				}else{
					for(var i in actEventParNameArr){
						str += "var " + actEventParNameArr[i] + "=record.get('" + actEventParNameArr[i] + "');";
					}
					str += funObjStr;
					str += funObjName + "." + funFunName + "(" + actEventParNameArr.join(",") + ");";
				}
			}
			str += "}else{Ext.Msg.alert('提示','请选择一条数据进行操作！');}";
			str += "}});"
		}else if(actEventName == "selectClick"){//查询[高级查询、分组查询]
			str += actObjName + ".on({" + actEventName + ":function(but){";
			str += "var com=" + actObjName + ";";
			str += "var where=com.getValue();";
			str += funObjStr;
			str += funObjName + "." + funFunName + "(where);";
			str += "}});"
		}
		result.success = true;
		result.value = str;
		return result;
	},
	/**
	 * 可见视图联动
	 * @private
	 * @param {} linkArr
	 * @param {} objStr
	 * @return {}
	 */
	getLinkObj:function(linkArr,objStr){
		var me = this;
		var act = linkArr[0].split("(");
		var actArr = act[0].split(".");//主动方
		var funArr = linkArr[1].split(".");//被动方
		
		//需要解析的代码不符合格式
		if(actArr.length < 2 || funArr.length < 2){
			return ;
		}
		
		var actEvent= act.length == 2 ? act[1].slice(0,-1) : "";//参数串
		var actEventName = actArr[actArr.length-1];//主动方事件名
		var actEventParNameArr = actEvent.split(",");//主动方参数数组
		var hqlObj = {};//hql信息对象
		if(actEvent == ""){//没有参数
			actEventParNameArr = "";
		}else{//有参数
			if(actEvent.slice(0,2) == "##"){//hql串
				actEventParNameArr = actEvent;
				hqlObj = me.getHql(actEvent.slice(2));
			}
		}
		
		//主动方代码
		var actObjStr = objStr;
		for(var i=0;i<actArr.length-1;i++){
			actObjStr += ".getComponent('" + actArr[i] + "')";
		}
		
		//被动方代码
		var funObjStr = objStr;
		for(var i=0;i<funArr.length-1;i++){
			funObjStr += ".getComponent('" + funArr[i] + "')";
		}
		//被动方方法名
		var funFunName = funArr[funArr.length-1];
		
		//方法
		var fun = function(){};
		
		//新增、删除[列表、树]、保存[表单]、重置[高级查询]
		if(actEventName == "addClick" || actEventName == "delClick" || actEventName == "saveClick" || actEventName == "resetClick"){
			fun = function(but){
				var functionStr = funObjStr + "." + funFunName + "();";//执行代码串
				eval(functionStr);
			};
		}else if(actEventName == "itemclick" || actEventName == "itemdblclick"){//行单击、行双击事件（列表、树）
			fun = function(view,record){
				var functionStr = "";
				if(Ext.typeOf(actEventParNameArr) == "string" && actEventParNameArr == ""){
					functionStr += "var id=record.get(" + actObjStr + ".objectName+'_Id');";
					functionStr += funObjStr + "." + funFunName + "(id);";
				}else{
					if(Ext.typeOf(actEventParNameArr) == "string" && actEventParNameArr.slice(0,2) == "##"){//hql串
						for(var i in hqlObj.parArr){
							functionStr +=  "var " + hqlObj.parArr[i] + "=record.get('" + hqlObj.parArr[i] + "');";
						}
						functionStr += "var hql=" + hqlObj.hql + ";";
						functionStr += funObjStr + "." + funFunName + "(hql);";
					}else{
						for(var i in actEventParNameArr){
							functionStr += "var " + actEventParNameArr[i] + "=record.get('" + actEventParNameArr[i] + "');";
						}
						functionStr += funObjStr + "." + funFunName + "(" + actEventParNameArr.join(",") + ");";
					}
				}
				eval(functionStr);
			};
		}else if(actEventName == "editClick" || actEventName == "showClick"){//修改、查看（列表、树）
			fun = function(but){
				var functionStr = "var list=" + actObjStr + ";";
				functionStr += "var records=list.getSelectionModel().getSelection();";
				functionStr += "if(records.length==1){";
				functionStr += "var record=records[0];";
				
				if(Ext.typeOf(actEventParNameArr) == "string" && actEventParNameArr == ""){
					functionStr += "var id=record.get(" + actObjStr + ".objectName+'_Id');";
					functionStr += funObjStr + "." + funFunName + "(id);";
				}else{
					if(Ext.typeOf(actEventParNameArr) == "string" && actEventParNameArr.slice(0,2) == "##"){//hql串
						for(var i in hqlObj.parArr){
							functionStr +=  "var " + hqlObj.parArr[i] + "=record.get('" + hqlObj.parArr[i] + "');";
						}
						functionStr += "var hql=" + hqlObj.hql + ";";
						functionStr += funObjStr + "." + funFunName + "(hql);";
					}else{
						for(var i in actEventParNameArr){
							functionStr += "var " + actEventParNameArr[i] + "=record.get('" + actEventParNameArr[i] + "');";
						}
						functionStr += funObjStr + "." + funFunName + "(" + actEventParNameArr.join(",") + ");";
					}
				}
				functionStr += "}else{Ext.Msg.alert('提示','请选择一条数据进行操作！');}";
				eval(functionStr);
			};
		}else if(actEventName == "selectClick"){//查询[高级查询、分组查询]
			fun = function(but){
				var functionStr = "var com=" + actObjStr + ";";
				functionStr += "var where=com.getValue();";
				functionStr += funObjStr + "." + funFunName + "(where);";
				eval(functionStr);
			}
		}
		
		//事件联动方法
		var f = {};
		f[actEventName] = fun;
		//添加联动代码
		eval(actObjStr + ".on(f);");
		//删除联动代码
		var removeLinkageFunction = function(){
			eval(actObjStr+".un(f);");
		};
		return removeLinkageFunction;
	},
	//=====================保存结果代码=======================
	/**
     * 另存按钮事件处理
     * @private
     */
	saveAs:function(){
		var me = this;
		var me = this;
        me.save(false);
	},
	/**
	 * 保存结果代码
	 * @private
	 */
	save:function(bo){
		var me = this;
		var params = me.getAppParamsValues();
		
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
				//InitParameter:params.defaultParams,//初始化参数
				AppType:3,//应用类型
				BuildType:1,//构建类型
				//BTDModuleType:2//,//模块类型(表单)
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
	 * 创建应用类代码
	 * @private
	 * @return {}
	 */
	createAppClass:function(){
		var me = this;
		//面板配置参数
		var params = me.getAppParamsValues();
		//所有组件信息
		var southRecords = me.getSouthRecords();
		//组件类代码
		var ClassCodes = "";
		
		var classArr = {};
		for(var i in southRecords){
			var code = southRecords[i].get(me.fieldsObj.ModuleOperCode);//功能编码
			if(classArr[code] != 1){
				var cl = southRecords[i].get(me.fieldsObj.ClassCode);
				cl = cl.replace(/\\/g,"\\\\");
				cl = cl.replace(/\"/g,"\\\"");
				ClassCodes += cl;
				classArr[code] = 1;
			}
		}
		//内部组件
		var items = me.createItemsStr();
		//联动关系
		var linkage = me.linkageValue;
		
		//继承组件
		var extend = "Ext.panel.Panel";
		//布局方式
		var layout = "'absolute'";
		
		var layoutType = params.layoutType;
		if(layoutType == "1"){//绝对定位
			layout = "'absolute'";
		}else if(layoutType == "2"){//border布局
			layout = "{type:'border',regionWeights:" + me.getBorderRegionWeightsStr() + "}";
		}else if(layoutType == "3"){//tab布局
			extend = "Ext.tab.Panel";
		}else if(layoutType == "4"){//列布局
			layout = "'column'";
		}
		
		//类代码
		var appClass = 
		"Ext.define('" + params.appCode + "',{" + 
			"extend:'" + extend + "'," + 
			"alias:'widget." + params.appCode + "'," + 
			"title:'" + params.titleText + "'," + 
			"width:" + params.width + "," + 
			"height:" + params.height + "," + 
			"autoScroll:true," + 
			"layout:" + layout + "," + 
			"initComponent:function(){" + 
				"var me=this;" + 
				ClassCodes + 
				"me.items=" + items + ";" + 
				"this.callParent(arguments);" + 
			"}";
		if(linkage != ""){
			appClass += 
			",afterRender:function(){" +
				"var me = this;" + linkage + 
				"me.callParent(arguments);" + 
			"}";
		}
			 
		appClass += "});";
		
		return appClass;
	},
	/**
	 * 创建内部组件
	 * @private
	 * @return {}
	 */
	createItemsStr:function(){
		var me = this;
		//所有组件信息
		var records = me.getSouthRecords();
		var items = "[";
		for(var i in records){
			var record = records[i];
			var appComID = record.get(me.fieldsObj.ModuleOperCode);
			var itemId = record.get('itemId');
			var width = record.get('width');
			var height = record.get('height');
			var x = record.get('x');
			var y = record.get('y');
			var title = record.get('title');
			var region = record.get('region');
			var split = record.get('split');
			var collapsible = record.get('collapsible');
			var collapsed = record.get('collapsed');
			var border = record.get('border');
			
			items = items + 
			"{" + 
				((width) ? ("width:" + width + ",") : "") + 
				((height) ? ("height:" + height + ",") : "") + 
				((x) ? ("x:" + x + ",") : "") + 
				((y) ? ("y:" + y + ",") : "") + 
				((title == "-") ? ("header:false,") : "") + 
				((title && title != "" && title != "-") ? ("title:'" + title + "',") : "") + 
				((region) ? ("region:'" + region + "',") : "") + 
				
				((split) ? ("split:" + split + ",") : "") + 
				((collapsible) ? ("collapsible:" + collapsible + ",") : "") + 
				((collapsed) ? ("collapsed:" + collapsed + ",") : "") + 
				((border) ? ("border:" + border + ",") : "") + 
				
				"xtype:'" + appComID + "'," + 
				"itemId:'" + itemId + "'" + 
			"},";
		}
		if(items.length > 1){
			items = items.substring(0,items.length-1);
		}
		
		items += "]";
		
		return items;
	},
	//=====================后台获取&存储=======================
	/**
	 * 从后台获取应用信息
	 * @private
	 * @param {} id
	 * @param {} callback
	 */
	getAppInfoServer:function(id,callback){
		var me = this;
		
		if(id != -1){
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
						var appInfo = "";
						if(result.ResultDataValue && result.ResultDataValue != ""){
							appInfo = Ext.JSON.decode(result.ResultDataValue);
						}
						if(Ext.typeOf(callback) == "function"){
							callback(appInfo);//回调函数
						}
					}else{
						Ext.Msg.alert('提示','获取应用组件信息失败！错误信息【<b style="color:red">'+ result.errorInfo +"</b>】");
					}
				},
				failure : function(response,options){ 
					Ext.Msg.alert('提示','获取应用组件信息请求失败！');
				}
			});
		}
	},
	/**
	 * 将构建结果保存到数据库中
	 * @private
	 * @param {} obj
	 * @param {} callback
	 */
	saveToServer:function(obj,callback){
		var me = this;
		var url = "";
		if(me.appId != -1){
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
			timeout:5000,
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
	 * 一般数据数据适配
	 * @private
	 * @param {} response
	 * @return {}
	 * 
	 * 例子
	 * 后台原始数据
	 * {
	 * 		"ErrorInfo":"",
	 * 		"success":true,
	 * 		"ResultDataFormatType":"JSON",
	 * 		"ResultDataValue":"[{\"CName\":\"查询部门\",\"EName\":\"RBAC_UDTO_SearchHRDept\"},{\"CName\":\"根据HQL条件查询部门\",\"EName\":\"RBAC_UDTO_SearchHRDeptByHQL\"}]"
	 * }
	 * 转化后的数据
	 * {
	 * 		"ErrorInfo":"",
	 * 		"success":true,
	 * 		"ResultDataFormatType":"JSON",
	 * 		"ResultDataValue":[
	 * 			{"CName":"查询部门","EName":"RBAC_UDTO_SearchHRDept"},
	 * 			{"CName":"根据HQL条件查询部门","EName":"RBAC_UDTO_SearchHRDeptByHQL"}
	 * 		]
	 * }
	 */
	changeStoreData: function(response){
		var me = this;
    	var data = Ext.JSON.decode(response.responseText);
    	data[me.ResultDataValue] = Ext.JSON.decode(data[me.ResultDataValue]);
    	response.responseText = Ext.JSON.encode(data);
    	return response;
  	},
  	/**
  	 * 复杂关系配置的语句处理
  	 * @private
  	 * @param {} value
  	 * @return {}
  	 */
  	changeLinkValue:function(value){
  		var v = "";
  		if(value && value != ""){
  			var arr = value.split('\n');//根据换行符
	  		for(var i in arr){
	  			v += Ext.String.trim(arr[i]);
	  		}
  		}
		return v;
  	}
});